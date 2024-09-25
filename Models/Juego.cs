public static class Juego{
    public static string Username {get; private set;} = "";
    public static int PuntajeActual {get; private set;}
    public static int Progreso {get; private set;}
    private static int cantidadPreguntasCorrectas;
    private static List<Pregunta> Preguntas = new();
    private static List<Respuesta> Respuestas = new();
    public static bool TodasLasCategorias = false;
    public static int Tiempo;
    public const int DefaultTiempo = 60000;
    public const int LimitePreguntas = 7;
    public static bool EnPartida {get; private set;}

    public static void InicializarJuego(){
        Username = "";
        PuntajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        Progreso = 1;
        Preguntas = new();
        Respuestas = new();
        Tiempo = DefaultTiempo;
        EnPartida = false;
    }

    public static bool CargarPartida(int IdDificultad, int IdCategoria){
        Preguntas = BD.ObtenerPreguntas(IdDificultad, IdCategoria);
        Respuestas = BD.ObtenerRespuestas(Preguntas);
        EnPartida = true;
        return Preguntas.Count > 0;
    }

    // Recibir lista de cada categoría de cada pregunta que aún se puede responder
    public static List<Categoria> ObtenerCategoriasPendientes(){
        List<Categoria> Categorias = new();
        List<int> Usadas = new();
        Categoria? CategoriaActual;

        foreach(Pregunta pregunta in Preguntas){
            CategoriaActual = BD.ObtenerCategoria(pregunta.IdCategoria);
            // Evitar el añadir valores nulos o repetidos
            if(CategoriaActual != null && !Usadas.Contains(CategoriaActual.IdCategoria)) Categorias.Add(CategoriaActual);
            Usadas.Add(CategoriaActual!.IdCategoria);
        }

        return Categorias;
    }

    public static Pregunta ObtenerProximaPregunta(int IdCategoriaElegida = -1){
        if(IdCategoriaElegida == -1){
            if(Preguntas.Count == 0) return new Pregunta();
            Progreso++;

            return Preguntas[new Random().Next(Preguntas.Count)];
        } else {
            List<Pregunta> PreguntasCategoria = Preguntas.Where(pregunta => pregunta.IdCategoria == IdCategoriaElegida).ToList();
            
            if(PreguntasCategoria.Count == 0) return new Pregunta();
            Progreso++;

            return PreguntasCategoria[new Random().Next(PreguntasCategoria.Count)];
        }
    }

    public static List<Respuesta> ObtenerProximasRespuestas(int IdPregunta) => IdPregunta == -1 ? new List<Respuesta>() : BD.ObtenerRespuestas(IdPregunta);

    public static bool VerificarRespuesta(int IdPregunta, int IdRespuesta, int TiempoRestante){
        Respuesta? respuesta = BD.ObtenerRespuesta(IdPregunta, IdRespuesta);
        if (respuesta == null) return false;
        bool EsCorrecta = respuesta.Correcta;

        if(EsCorrecta){
            cantidadPreguntasCorrectas++;
            PuntajeActual += (int) Math.Ceiling((double) BD.ObtenerPuntajeDePregunta(IdPregunta) * TiempoRestante / 100);
        }

        Preguntas.RemoveAll(pregunta => pregunta.IdPregunta == IdPregunta);
        return EsCorrecta;
    }
}