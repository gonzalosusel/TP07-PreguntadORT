public static class Juego{
    public static string Username {get; private set;} = "";
    public static int PuntajeActual {get; private set;}
    public static int Progreso {get; private set;}
    private static int cantidadPreguntasCorrectas;
    private static List<Pregunta> Preguntas = new();
    private static List<Respuesta> Respuestas = new();
 
    public static void InicializarJuego(){
        Username = "";
        PuntajeActual = 0;
        cantidadPreguntasCorrectas = 0;
        Progreso = 1;
        Preguntas = new();
        Respuestas = new();
    }

    public static bool CargarPartida(string _Username, int IdDificultad, int IdCategoria){
        Username = _Username;
        Preguntas = BD.ObtenerPreguntas(IdDificultad, IdCategoria);
        Respuestas = BD.ObtenerRespuestas(Preguntas);
        return Preguntas.Count > 0;
    }

    public static Pregunta? ObtenerProximaPregunta(){
        if(Preguntas.Count == 0) return null;
        Progreso++;
        return Preguntas[new Random().Next(0, Preguntas.Count)];
    }

    public static List<Respuesta> ObtenerProximasRespuestas(int IdPregunta) => IdPregunta == -1 ? new List<Respuesta>() : BD.ObtenerRespuestas(IdPregunta);

    public static bool VerificarRespuesta(int IdPregunta, int IdRespuesta){
        Respuesta? respuesta = BD.ObtenerRespuesta(IdPregunta, IdRespuesta);
        if (respuesta == null) return false;
        bool EsCorrecta = respuesta.Correcta;

        if(EsCorrecta){
            cantidadPreguntasCorrectas++;
            PuntajeActual += BD.ObtenerPuntajeDePregunta(IdPregunta);
        }

        Preguntas.RemoveAll(pregunta => pregunta.IdPregunta == IdPregunta);
        return EsCorrecta;
    }
}