public class Categoria{
    public int IdCategoria;
    public string Nombre, Foto;
    
    public Categoria(int IdCategoria, string Nombre, string? Foto){
        this.IdCategoria = IdCategoria;
        this.Nombre = Nombre;
        this.Foto = Foto ?? "";
    }
}