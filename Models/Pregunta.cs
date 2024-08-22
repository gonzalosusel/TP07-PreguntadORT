public class Pregunta(int IdPregunta, int IdCategoria, int IdDificultad, string Enunciado, string? Foto){
    public int IdPregunta = IdPregunta, IdCategoria = IdCategoria, IdDificultad = IdDificultad;
    public string Enunciado = Enunciado, Foto = Foto ?? "";
}