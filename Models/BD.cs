using System.Data.SqlClient;
using Dapper;

public static class BD{
    private static string ConnectionString = "Server=localhost;DataBase=PreguntadORT;Trusted_Connection=True;";
    public static List<Categoria> ObtenerCategorias(){
        using SqlConnection con = new(ConnectionString);
        return con.Query<Categoria>("SELECT * FROM Categorias").ToList();
    }

    public static Categoria? ObtenerCategoria(int IdCategoria){
        using SqlConnection con = new(ConnectionString);
        return con.QueryFirstOrDefault<Categoria>("SELECT * FROM Categorias WHERE IdCategoria=@pIdCategoria", new{pIdCategoria=IdCategoria});
    }

    public static List<Dificultad> ObtenerDificultades(){
        using SqlConnection con = new(ConnectionString);
        return con.Query<Dificultad>("SELECT * FROM Dificultades").ToList();
    }

    public static int ObtenerPuntajeDePregunta(int IdPregunta){
        using SqlConnection con = new(ConnectionString);
        return con.QueryFirstOrDefault<int>("SELECT Puntaje FROM Preguntas p LEFT JOIN Dificultades d ON p.IdDificultad=d.IdDificultad WHERE IdPregunta=@pIdPregunta", new{pIdPregunta=IdPregunta});
    }

    public static List<Pregunta> ObtenerPreguntas(int IdDificultad, int IdCategoria){
        using SqlConnection con = new(ConnectionString);
        List<Pregunta> Datos;

        if(IdCategoria == -1 && IdDificultad == -1)
            Datos = con.Query<Pregunta>("SELECT * FROM Preguntas").ToList();
        else if(IdDificultad == -1)
            Datos = con.Query<Pregunta>("SELECT * FROM Preguntas WHERE IdCategoria=@pIdCategoria", new{pIdCategoria = IdCategoria}).ToList();
        else if(IdCategoria == -1)
            Datos = con.Query<Pregunta>("SELECT * FROM Preguntas WHERE IdDificultad=@pIdDificultad", new{pIdDificultad = IdDificultad}).ToList();
        else 
            Datos = con.Query<Pregunta>("SELECT * FROM Preguntas WHERE IdDificultad=@pIdDificultad AND IdCategoria=@pIdCategoria", new{pIdDificultad = IdDificultad, pIdCategoria = IdCategoria}).ToList();

        return Datos;
    }

    public static List<Respuesta> ObtenerRespuestas(List<Pregunta> Preguntas){
        using SqlConnection con = new(ConnectionString);
        List<Respuesta> Respuestas = new();

        foreach(Pregunta pregunta in Preguntas){
            Respuestas.AddRange(
                con.Query<Respuesta>("SELECT * FROM Respuestas WHERE IdPregunta=@pIdPregunta", new{pIdPregunta=pregunta.IdPregunta})
            );
        }

        return Respuestas;
    }

    public static List<Respuesta> ObtenerRespuestas(int IdPregunta){
        using SqlConnection con = new(ConnectionString);
        return con.Query<Respuesta>("SELECT TOP 4 * FROM Respuestas WHERE IdPregunta=@pIdPregunta", new{pIdPregunta=IdPregunta}).ToList();
    }

    public static Respuesta? ObtenerRespuesta(int IdPregunta, int IdRespuesta){
        using SqlConnection con = new(ConnectionString);
        return con.QueryFirstOrDefault<Respuesta>("SELECT * FROM Respuestas WHERE IdPregunta=@pIdPregunta AND IdRespuesta=@pIdRespuesta", new{pIdPregunta=IdPregunta, pIdRespuesta=IdRespuesta});
    }

    public static Respuesta? ObtenerRespuestaCorrecta(int IdPregunta){
        using SqlConnection con = new(ConnectionString);
        return con.QueryFirstOrDefault<Respuesta>("SELECT * FROM Respuestas WHERE IdPregunta=@pIdPregunta AND Correcta=1", new{pIdPregunta = IdPregunta});
    }

    // True = inició sesión
    public static bool Autenticado(string? Username, string? Password){
        if(Username == null || Password == null) return false;

        using SqlConnection con = new(ConnectionString);
        return con.QueryFirstOrDefault<object>("SELECT * FROM Usuarios WHERE Username=@pUsername AND Password=@pPassword", new{pUsername=Username, pPassword=Password}) != null;
    }

    public static void CrearPregunta(Pregunta pregunta, List<Respuesta> respuestas){
        using SqlConnection con = new(ConnectionString);
        con.Execute("INSERT INTO Preguntas(IdCategoria, IdDificultad, Enunciado, Foto) VALUES(@pIdCategoria, @pIdDificultad, @pEnunciado, @pFoto)",
        new{pIdCategoria=pregunta.IdCategoria, pIdDificultad=pregunta.IdDificultad, pEnunciado=pregunta.Enunciado, pFoto=pregunta.Foto});

        int IdPregunta = con.Query<int>("SELECT IdPregunta FROM Preguntas").ToArray().Max(); // Es un campo autonumérico, de todos los IDs, el más grande es el más recientemente agregado

        foreach(var respuesta in respuestas){
            con.Execute("INSERT INTO Respuestas(IdPregunta, Opcion, Contenido, Correcta) VALUES(@pIdPregunta, @pOpcion, @pContenido, @pCorrecta)",
            new{pIdPregunta=IdPregunta, pOpcion=respuesta.Opcion, pContenido=respuesta.Contenido, pCorrecta=respuesta.Correcta});
        }
    }

    public static List<PuntajeUsuario> ObtenerTablaPuntajes(){
        using SqlConnection con = new(ConnectionString);
        return con.Query<PuntajeUsuario>("SELECT FechaHora, Username, Puntaje FROM Puntajes p INNER JOIN Usuarios u ON p.IdUsuario=u.IdUsuario").OrderByDescending(puntaje => puntaje.Puntaje).ToList() ?? new List<PuntajeUsuario>();
    }

    public static void AñadirPuntaje(PuntajeUsuario puntaje){
        using SqlConnection con = new(ConnectionString);
        con.Execute("EXEC sp_AñadirPuntaje @pFechaHora, @pUsername, @pPuntaje",
        new{pFechaHora=puntaje.FechaHora, pUsername=puntaje.Username, pPuntaje=puntaje.Puntaje});
    }

    public static (string Username, bool EsAdmin, bool InicioSesion) DatosUsuario(string? Username, string? Password){
        if(!Autenticado(Username, Password)) return ("", false, false);

        using SqlConnection con = new(ConnectionString);
        if(con.QueryFirst<int>("SELECT COUNT(*) FROM Usuarios WHERE Username=@pUsername AND Password=@pPassword",
        new{pUsername=Username, pPassword=Password}) == 0) return ("", false, false);

        var Datos = con.QueryFirst<(string Username, bool EsAdmin)>("SELECT Username, EsAdmin FROM Usuarios WHERE Username=@pUsername AND Password=@pPassword",
        new{pUsername=Username, pPassword=Password});

        return (Datos.Username, Datos.EsAdmin, true);
    }

    public static bool RegistrarUsuario(string Username, string Password){
        using SqlConnection con = new(ConnectionString);
        if(con.QueryFirstOrDefault<int>("SELECT COUNT(*) FROM Usuarios WHERE Username=@pUsername", new{pUsername=Username}) > 0) return false;
        con.Execute("INSERT INTO Usuarios(Username, Password, EsAdmin, puntosAcumulados) VALUES(@pUsername, @pPassword, 0, 0)",
        new{pUsername=Username, pPassword=Password});
        return true;
    }
    
    public static int ObtenerPuntos(string Username){
        using SqlConnection con = new(ConnectionString);
        return con.QueryFirstOrDefault<int>("SELECT puntosAcumulados FROM Usuarios WHERE Username=@pUsername", new{pUsername=Username});
    }
}