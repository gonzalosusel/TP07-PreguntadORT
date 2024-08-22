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
}