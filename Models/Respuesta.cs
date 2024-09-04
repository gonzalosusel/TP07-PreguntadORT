public class Respuesta{
    public int IdRespuesta, IdPregunta, Opcion;
    public string Contenido;
    public bool Correcta;

    public Respuesta(int IdRespuesta, int IdPregunta, int Opcion, string Contenido, bool Correcta){
        this.IdRespuesta = IdRespuesta;
        this.IdPregunta = IdPregunta;
        this.Opcion = Opcion;
        this.Contenido = Contenido;
        this.Correcta = Correcta;
    }
}