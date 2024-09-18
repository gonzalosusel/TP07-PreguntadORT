public class PuntajeUsuario{
    public string Username;
    public int Puntaje;
    public DateTime FechaHora;

    public PuntajeUsuario(DateTime FechaHora, string Username, int Puntaje){
        this.Username = Username;
        this.Puntaje = Puntaje;
        this.FechaHora = FechaHora;
    }
}