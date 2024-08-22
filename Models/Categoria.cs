public class Categoria(int IdCategoria, string Nombre, string? Foto){
    public int IdCategoria = IdCategoria;
    public string Nombre = Nombre, Foto = Foto ?? "";
}