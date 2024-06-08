using EXAMENMVC.Datos;
using EXAMENMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EXAMENMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _contexto;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext contexto)
        {
            _logger = logger;
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var marcas = _contexto.Marcas.ToList();
            var vehiculos = _contexto.Vehiculos.Include(v => v.Modelo).ThenInclude(m => m.Marca).ToList();

            var vm = new DropDownsVM
            {
                Marcas = marcas,
                Vehiculos = vehiculos
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult ObtenerModelos(int IDMARCA) // Ajusta el nombre del parámetro a IDMARCA
        {
            var modelos = _contexto.Modelos.Where(x => x.MarcaIDMARCA == IDMARCA).ToList();
            return Json(modelos);
        }
    }
}
