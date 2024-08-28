public class Pregunta{
    public int IdPregunta, IdCategoria, IdDificultad;
    public string Enunciado, Foto;

    public Pregunta(int IdPregunta, int IdCategoria, int IdDificultad, string Enunciado, string? Foto){
        this.IdPregunta = IdPregunta;
        this.IdCategoria = IdCategoria;
        this.IdDificultad = IdDificultad;
        this.Enunciado = Enunciado;
        this.Foto = Foto ?? "";
    }
}