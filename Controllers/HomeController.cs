using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_PreguntadORT.Models;
using System.Web;

namespace TP_PreguntadORT.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger) =>_logger = logger;
    public IActionResult Index() => View();
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    
    public IActionResult ConfigurarJuego(){
        Juego.InicializarJuego();
        ViewBag.Categorias = BD.ObtenerCategorias();
        ViewBag.Dificultades = BD.ObtenerDificultades();
        return View();
    }

    public IActionResult Jugar(string? Username, int IdDificultad, int IdCategoria){
        // Volver al formulario de inicio si:
        // Se envió un nombre de usuario y hubo un fallo al guardarlo
        // O el juego no tiene ningún nombre de usuario registrado (En caso de que alguien intente abrir la view directamente)
        if((!string.IsNullOrEmpty(Username) && !Juego.CargarPartida(Username, IdDificultad, IdCategoria)) || Juego.Username == "") return View("ConfigurarJuego");

        ViewBag.Username = Juego.Username;
        ViewBag.PuntajeActual = Juego.PuntajeActual;
        ViewBag.Progreso = Juego.Progreso;
        
        ViewBag.Pregunta = Juego.ObtenerProximaPregunta() ?? new Pregunta();
        ViewBag.Respuestas = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta) ?? new List<Respuesta>();

        return View(ViewBag.Pregunta.IdPregunta == -1 ? "Fin" : "Pregunta");
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int IdPregunta, int IdRespuesta){
        ViewBag.EsCorrecta = Juego.VerificarRespuesta(IdPregunta, IdRespuesta);
        ViewBag.RespuestaCorrecta = BD.ObtenerRespuestaCorrecta(IdPregunta);
        return View("Respuesta");
    }

    public IActionResult AñadirPregunta(){
        ViewBag.Categorias = BD.ObtenerCategorias();
        ViewBag.Dificultades = BD.ObtenerDificultades();
        return View();
    }

    [HttpPost]
    public IActionResult Login(string Username, string Password, int IdDificultad, int IdCategoria, string PrEnunciado, string PrFoto){
        if(!BD.Autenticado(Username, Password)) return View("Index");

        Pregunta pregunta = new(IdCategoria, IdDificultad, PrEnunciado, PrFoto);

        return View("AñadirPregunta");
    }
}
