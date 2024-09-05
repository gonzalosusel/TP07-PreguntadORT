using System.Data.SqlClient;
using Dapper;

public static class BD{
    private static string ConnectionString = "Server=localhost;DataBase=PreguntadORT;Trusted_Connection=True;";
    public static List<Categoria> ObtenerCategorias(){
        using SqlConnection con = new(ConnectionString);
        return con.Query<Categoria>("SELECT * FROM Categorias").ToList();
    }

    public static List<Dificultad> ObtenerDificultades(){
        using SqlConnection con = new(ConnectionString);
        return con.Query<Dificultad>("SELECT * FROM Dificultades").ToList();
    }

    public static int ObtenerDificultadDePregunta(int IdPregunta){
        using SqlConnection con = new(ConnectionString);
        return con.QueryFirstOrDefault<int>("select Puntaje from Preguntas left join Dificultades on Preguntas.IdDificultad=Dificultades.IdDificultad where IdPregunta=@pIdPregunta;", new{pIdPregunta=IdPregunta});
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
        return con.Query<Respuesta>("SELECT * FROM Respuestas WHERE IdPregunta=@pIdPregunta", new{pIdPregunta=IdPregunta}).ToList();
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
    public static bool Autenticado(string Usuario, string Password){
        using SqlConnection con = new(ConnectionString);
        return con.QueryFirstOrDefault<object>("SELECT * FROM Usuarios WHERE Nombre=@pUsuario AND Password=@pPassword", new{pUsuario=Usuario, pPassword=Password}) != null;
    }

    public static void AñadirPregunta(Pregunta pregunta, List<Respuesta> respuestas){
        using SqlConnection con = new(ConnectionString);
        con.Execute("INSERT INTO Preguntas(IdCategoria, IdDificultad, Enunciado, Foto) VALUES(@pIdCategoria, @pIdDificultad, @pEnunciado, @pFoto)",
        new{pIdCategoria=pregunta.IdCategoria, pIdDificultad=pregunta.IdDificultad, pEnunciado=pregunta.Enunciado, pFoto=pregunta.Foto});

        int IdPregunta = con.Query<int>("SELECT IdPregunta FROM Preguntas").ToArray().Max(); // Es un campo autonumérico, de todos los IDs, el más grande es el más recientemente agregado

        foreach(var respuesta in respuestas){
            con.Execute("INSERT INTO Respuestas(IdPregunta, Opcion, Contenido, Correcta) VALUES(@pIdPregunta, @pOpcion, @pContenido, @pCorrecta)",
            new{pIdPregunta=IdPregunta, pOpcion=respuesta.Opcion, pContenido=respuesta.Contenido, pCorrecta=respuesta.Correcta});
        }
    }
}