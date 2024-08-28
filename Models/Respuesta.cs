public class Respuesta{
    public int IdRespuesta, IdPregunta, Opcion;
    public string Contenido, Foto;
    public bool Correcta;

    public Respuesta(int IdRespuesta, int IdPregunta, int Opcion, string Contenido, bool Correcta, string? Foto){
        this.IdRespuesta = IdRespuesta;
        this.IdPregunta = IdPregunta;
        this.Opcion = Opcion;
        this.Contenido = Contenido;
        this.Foto = Foto ?? "";
        this.Correcta = Correcta;
    }
}