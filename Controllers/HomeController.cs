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
    public IActionResult GuardarPreguntaAñadida(string Username, string Password, int IdDificultad, int IdCategoria, string PrEnunciado, string PrFoto, string ContenidoR1, string ContenidoR2, string ContenidoR3, string ContenidoR4, int RCorrecta){
        if(!BD.Autenticado(Username, Password)) return Redirect(Url.Action("Index", "Home"));;
        Pregunta pregunta = new(IdCategoria, IdDificultad, PrEnunciado, PrFoto);
        List<Respuesta> respuestas = new();
        string[] Enunciados = {ContenidoR1, ContenidoR2, ContenidoR3, ContenidoR4};
        for(int i = 0; i < 4; i++){
            Respuesta respuesta = new(-1, -1, i + 1, Enunciados[i], i + 1 == RCorrecta);
            respuestas.Add(respuesta);
        }

        BD.AñadirPregunta(pregunta, respuestas);
        return Redirect(Url.Action("AñadirPregunta", "Home"));
    }
}
