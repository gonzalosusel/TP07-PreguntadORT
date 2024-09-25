using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_PreguntadORT.Models;
using System.Web;

namespace TP_PreguntadORT.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private static int IdCategoriaElegida = -1;
    public HomeController(ILogger<HomeController> logger) => _logger = logger;
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    private (string Username, bool EsAdmin, bool InicioSesion) GetUserData(HttpRequest Request) => BD.DatosUsuario(Request.Cookies["Username"], Request.Cookies["Password"]);

    public IActionResult Index(){
        ViewBag.DatosUsuario = GetUserData(Request);
        return View();
    }

    public IActionResult ConfigurarJuego(){
        ViewBag.DatosUsuario = GetUserData(Request);
        if(!ViewBag.DatosUsuario.Item3) return Redirect(Url.Action("IniciarSesion", "Home") ?? "");

        Juego.InicializarJuego();
        ViewBag.Categorias = BD.ObtenerCategorias();
        ViewBag.Dificultades = BD.ObtenerDificultades();
        return View();
    }

    public IActionResult Comenzar(int IdDificultad, int IdCategoria){
        ViewBag.DatosUsuario = GetUserData(Request);
        if(!ViewBag.DatosUsuario.Item3) return Redirect(Url.Action("IniciarSesion", "Home") ?? "");

        if(!Juego.CargarPartida(IdDificultad, IdCategoria)) return Redirect(Url.Action("ConfigurarJuego", "Home", new{mensaje="noquestions"}) ?? "");
        
        if(IdCategoria == -1){
            Juego.TodasLasCategorias = true;
            return Redirect(Url.Action("CategoriaRandom", "Home") ?? "");
        }

        return Redirect(Url.Action("Jugar", "Home") ?? "");
    }

    public IActionResult CategoriaRandom(){
        ViewBag.DatosUsuario = GetUserData(Request);
        if(!Juego.EnPartida) return Redirect(Url.Action("ConfigurarJuego", "Home") ?? "");

        // Sólo se debe abrir esta view si se eligieron todas las categorías
        if(!Juego.TodasLasCategorias) return Redirect(Url.Action("Jugar", "Home") ?? "");

        List<Categoria> CategoriasPendientes = Juego.ObtenerCategoriasPendientes();
        Categoria CategoriaElegida = CategoriasPendientes[new Random().Next(CategoriasPendientes.Count)];
        IdCategoriaElegida = CategoriaElegida.IdCategoria;

        ViewBag.CategoriasPendientes = CategoriasPendientes;
        ViewBag.CategoriaElegida = CategoriaElegida;

        return View();
    }
    
    public IActionResult Jugar(){
        ViewBag.DatosUsuario = GetUserData(Request);
        // Si esta view se abrió directamente y sin pasar por el formulario, volver al formulario
        if(!Juego.EnPartida) return Redirect(Url.Action("ConfigurarJuego", "Home") ?? "");

        if(Juego.Progreso == Juego.LimitePreguntas) return Redirect(Url.Action("Fin", "Home") ?? "");

        // Si se eligieron todas las categorias pero no hay una asignada, significa que uno se salteó la ruleta
        if(Juego.TodasLasCategorias && IdCategoriaElegida == -1) return Redirect(Url.Action("CategoriaRandom", "Home") ?? "");

        ViewBag.PuntajeActual = Juego.PuntajeActual;
        ViewBag.Progreso = Juego.Progreso;

        Juego.Tiempo = Juego.DefaultTiempo; // Al principio de cada pregunta poner el timer en 60.000 milisegundos
        ViewBag.Tiempo = Juego.Tiempo;
        
        ViewBag.Pregunta = Juego.TodasLasCategorias ? Juego.ObtenerProximaPregunta(IdCategoriaElegida) : Juego.ObtenerProximaPregunta();
        ViewBag.Respuestas = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);

        if(ViewBag.Pregunta.IdPregunta == -1){
            BD.AñadirPuntaje(new PuntajeUsuario(DateTime.Now, ViewBag.DatosUsuario.Item1, Juego.PuntajeActual));
            return Redirect(Url.Action("Fin", "Home") ?? "");
        }

        return View("Pregunta");
    }

    public IActionResult Fin(){
        ViewBag.DatosUsuario = GetUserData(Request);
        ViewBag.PuntajeActual = Juego.PuntajeActual;
        return View();
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int IdPregunta, int IdRespuesta, int TiempoRestante){
        ViewBag.DatosUsuario = GetUserData(Request);
        ViewBag.EsCorrecta = Juego.VerificarRespuesta(IdPregunta, IdRespuesta, TiempoRestante);
        ViewBag.RespuestaCorrecta = BD.ObtenerRespuestaCorrecta(IdPregunta);
        ViewBag.TodasLasCategorias = Juego.TodasLasCategorias;

        return View("Respuesta");
    }

    public IActionResult MostrarPuntos(){
        ViewBag.DatosUsuario = GetUserData(Request);
        if(!ViewBag.DatosUsuario.Item3) return Redirect(Url.Action("Index", "Home") ?? "");

        ViewBag.Puntos = BD.ObtenerPuntos(ViewBag.DatosUsuario.Item1);
        return View();
    }

    public IActionResult AñadirPregunta(){
        ViewBag.DatosUsuario = GetUserData(Request);
        if(!ViewBag.DatosUsuario.Item2) return Redirect(Url.Action("Index", "Home") ?? "");

        ViewBag.Categorias = BD.ObtenerCategorias();
        ViewBag.Dificultades = BD.ObtenerDificultades();
        return View();
    }

    public IActionResult IniciarSesion(){
        ViewBag.DatosUsuario = GetUserData(Request);
        return View();
    }

    [HttpPost]
    public IActionResult VerificarInicio(string Username, string Password){
        ViewBag.DatosUsuario = GetUserData(Request);
        if(BD.Autenticado(Username, Password)){
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(2);
            cookieOptions.Path = "/"; 

            Response.Cookies.Append("Username", Username, cookieOptions);
            Response.Cookies.Append("Password", Password, cookieOptions);
            return Redirect(Url.Action("Index", "Home") ?? "");
        }

        return Redirect(Url.Action("IniciarSesion", "Home", new{mensaje="wrongpwd"}) ?? "");
    }

    [HttpPost]
    public IActionResult GuardarPreguntaAñadida(string Username, string Password, int IdDificultad, int IdCategoria, string PrEnunciado, string PrFoto, string ContenidoR1, string ContenidoR2, string ContenidoR3, string ContenidoR4, int RCorrecta){
        ViewBag.DatosUsuario = GetUserData(Request);
        if(!ViewBag.DatosUsuario.Item2) return Redirect(Url.Action("Index", "Home") ?? "");
        Pregunta pregunta = new(IdCategoria, IdDificultad, PrEnunciado, PrFoto);
        List<Respuesta> respuestas = new();
        string[] Enunciados = {ContenidoR1, ContenidoR2, ContenidoR3, ContenidoR4};
        for(int i = 0; i < 4; i++){
            Respuesta respuesta = new(-1, -1, i + 1, Enunciados[i], i + 1 == RCorrecta);
            respuestas.Add(respuesta);
        }

        BD.CrearPregunta(pregunta, respuestas);
        return Redirect(Url.Action("AñadirPregunta", "Home", new{mensaje="added"}) ?? "");
    }

    public IActionResult HighScores(){
        ViewBag.DatosUsuario = GetUserData(Request);
        ViewBag.TablaPuntajes = BD.ObtenerTablaPuntajes();
        return View();
    }

    public IActionResult Registrarse(){
        ViewBag.DatosUsuario = GetUserData(Request);
        return View();
    }

    [HttpPost]
    public IActionResult NuevoUsuario(string Username, string Password){
        if(!BD.RegistrarUsuario(Username, Password)) return Redirect(Url.Action("Registrarse", "Home", new{mensaje="userinuse"}) ?? "");

        var cookieOptions = new CookieOptions();
        cookieOptions.Expires = DateTime.Now.AddDays(2);
        cookieOptions.Path = "/"; 

        Response.Cookies.Append("Username", Username, cookieOptions);
        Response.Cookies.Append("Password", Password, cookieOptions);
        return Redirect(Url.Action("Index", "Home") ?? "");
    }
}