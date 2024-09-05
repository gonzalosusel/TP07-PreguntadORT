public class Pregunta{
    public int IdPregunta = -1, IdCategoria, IdDificultad;
    public string? Enunciado, Foto;

    public Pregunta(){}

    public Pregunta(int IdPregunta, int IdCategoria, int IdDificultad, string Enunciado, string? Foto){
        this.IdPregunta = IdPregunta;
        this.IdCategoria = IdCategoria;
        this.IdDificultad = IdDificultad;
        this.Enunciado = Enunciado;
        this.Foto = Foto ?? "";
    }

    public Pregunta(int IdCategoria, int IdDificultad, string Enunciado, string? Foto){
        this.IdPregunta = 0;
        this.IdCategoria = IdCategoria;
        this.IdDificultad = IdDificultad;
        this.Enunciado = Enunciado;
        this.Foto = Foto ?? "";
    }
}