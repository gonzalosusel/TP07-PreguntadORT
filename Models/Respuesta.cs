public class Respuesta(int IdRespuesta, int IdPregunta, int Opcion, string Contenido, bool Correcta, string? Foto){
    public int IdRespuesta = IdRespuesta, IdPregunta = IdPregunta, Opcion = Opcion;
    public string Contenido = Contenido, Foto = Foto ?? "";
    public bool Correcta = Correcta;
}