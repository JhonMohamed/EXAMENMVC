using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EXAMENMVC.Datos;
using EXAMENMVC.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EXAMENMVC.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiculosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehiculos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehiculos.Include(v => v.Modelo.Marca);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vehiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vehiculos == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Modelo)
                .ThenInclude(m => m.Marca)
                .FirstOrDefaultAsync(m => m.IDVEHICULO == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // GET: Vehiculos/Create
        public IActionResult Create()
        {
            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA");
            ViewBag.Modelo = new SelectList(_context.Modelos, "IDMODELO", "NOM_MODELO");
            // Obtén las marcas desde la base de datos
            var marcas = _context.Marcas.ToList();

            // Pasa las marcas a la vista usando ViewBag
            ViewBag.Marcas = new SelectList(marcas, "IDMARCA", "NOM_MARCA");
            return View();
        }

        // POST: Vehiculos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IDVEHICULO,NRO_PLACA,ModeloIDMODELO,año,estado,Color")] Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(vehiculo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Redirigir a la acción Index
                }
                catch (Exception ex)
                {
                    // Manejo de errores si es necesario
                    ModelState.AddModelError("", "Error al guardar el vehículo: " + ex.Message);
                    // Recargar las listas para los dropdowns en caso de error
                    var marcas = _context.Marcas.ToList();
                    ViewBag.Marcas = new SelectList(marcas, "IDMARCA", "NOM_MARCA");
                    return View(vehiculo);
                }
            }

            // Recargar las listas para los dropdowns en caso de error
            var marcasList = _context.Marcas.ToList();
            ViewBag.Marcas = new SelectList(marcasList, "IDMARCA", "NOM_MARCA");

            return View(vehiculo);
        }

        // Método para obtener modelos basados en una marca específica
        //public async Task<IActionResult> ObtenerModelos(int idMarca)
        //{
           
        //        var modelos = await _context.Modelos
        //            .Where(m => m.ID_MARCA == idMarca)
        //            .Select(m => new
        //            {
        //                IDMODELO = m.IDMODELO,
        //                NOM_MODELO = m.NOM_MODELO
        //            })
        //            .ToListAsync();

        //        return Json(modelos);
        //    }
        
        // GET: Vehiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vehiculos == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Modelo)
                .ThenInclude(m => m.Marca)
                .FirstOrDefaultAsync(m => m.IDVEHICULO == id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return View(vehiculo);
        }

        // POST: Vehiculos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IDVEHICULO,NRO_PLACA,ModeloIDMODELO,año,estado,Color")] Vehiculo vehiculo)
        {
            if (id != vehiculo.IDVEHICULO)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(vehiculo.IDVEHICULO))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehiculo);
        }

        // GET: Vehiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vehiculos == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Modelo)
                .ThenInclude(m => m.Marca)
                .FirstOrDefaultAsync(m => m.IDVEHICULO == id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return View(vehiculo);
        }

        // POST: Vehiculos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vehiculos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vehiculos'  is null.");
            }
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiculoExists(int id)
        {
            return (_context.Vehiculos?.Any(e => e.IDVEHICULO == id)).GetValueOrDefault();
        }


        [HttpGet]
        public JsonResult ObtenerModelos(int idMarca)
        {
            var modelos = _context.Modelos
                .Where(m => m.ID_MARCA == idMarca)
                .Select(m => new { idModelo = m.IDMODELO, nomModelo = m.NOM_MODELO })
                .ToList();
            return Json(modelos);
        }

    }
}