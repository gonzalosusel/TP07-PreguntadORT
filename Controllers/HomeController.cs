using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP_PreguntadORT.Models;

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

    [HttpPost]
    public IActionResult Comenzar(string Username, int IdDificultad, int IdCategoria){
        if(!Juego.CargarPartida(Username, IdDificultad, IdCategoria)) return ConfigurarJuego();
        
        ViewBag.Pregunta = Juego.ObtenerProximaPregunta();
        if(ViewBag.Pregunta == null) return Fin();
        ViewBag.Respuestas = Juego.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);
        ViewBag.Username = Juego.Username;
        ViewBag.PuntajeActual = Juego.PuntajeActual;
        ViewBag.Progreso = Juego.Progreso;
        return View("Jugar");
    }

    [HttpPost]
    public IActionResult VerificarRespuesta(int IdPregunta, int IdRespuesta){
        ViewBag.EsCorrecta = Juego.VerificarRespuesta(IdPregunta, IdRespuesta);
        ViewBag.RespuestaCorrecta = BD.ObtenerRespuestaCorrecta(IdPregunta);
        return View("Respuesta");
    }

    public IActionResult Fin(){
        ViewBag.Username = Juego.Username;
        ViewBag.PuntajeActual = Juego.PuntajeActual;
        return View();
    }
}
