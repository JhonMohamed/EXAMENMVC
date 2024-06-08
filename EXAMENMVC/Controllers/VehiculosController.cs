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
            var vehiculos = _context.Vehiculos
                .Include(v => v.Modelo)
                .ThenInclude(m => m.Marca);

            return View(await vehiculos.ToListAsync());
        }

        // GET: Vehiculos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
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
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar el vehículo: " + ex.Message);
                }
            }

            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA");
            return View(vehiculo);
        }

        // Acción para obtener modelos basados en la marca seleccionada
        public JsonResult ObtenerModelos(int idMarca)
        {
            var modelos = _context.Modelos
                .Where(m => m.MarcaIDMARCA == idMarca)
                .Select(m => new { idModelo = m.IDMODELO, noM_MODELO = m.NOM_MODELO })
                .ToList();
            return Json(modelos);
        }

        // GET: Vehiculos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehiculo = await _context.Vehiculos
                .Include(v => v.Modelo)
                .ThenInclude(m => m.Marca)
                .FirstOrDefaultAsync(v => v.IDVEHICULO == id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA", vehiculo.Modelo.MarcaIDMARCA);
            ViewBag.Modelos = new SelectList(_context.Modelos.Where(m => m.MarcaIDMARCA == vehiculo.Modelo.MarcaIDMARCA), "IDMODELO", "NOM_MODELO", vehiculo.ModeloIDMODELO);
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
                    return RedirectToAction(nameof(Index));
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
            }

            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA", vehiculo.Modelo.MarcaIDMARCA);
            ViewBag.Modelos = new SelectList(_context.Modelos.Where(m => m.MarcaIDMARCA == vehiculo.Modelo.MarcaIDMARCA), "IDMODELO", "NOM_MODELO", vehiculo.ModeloIDMODELO);
            return View(vehiculo);
        }

        // GET: Vehiculos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
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
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método para verificar la existencia de un vehículo
        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.IDVEHICULO == id);
        }
    }
}