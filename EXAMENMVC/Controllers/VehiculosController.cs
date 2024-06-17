using EXAMENMVC.Datos;
using EXAMENMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EXAMENMVC.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment entorno;

        public VehiculosController(ApplicationDbContext context, IWebHostEnvironment entorno)
        {
            _context = context;
            this.entorno = entorno;
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
        public async Task<IActionResult> Create(VehiculoVM vehiculoVM)
        {
            if (vehiculoVM.ImagenFile == null)
            {
                ModelState.AddModelError("ImagenFile", "El archivo de imagen es obligatorio");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA");
                return View(vehiculoVM);
            }
            string nuevoNombreArchivo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            nuevoNombreArchivo += Path.GetExtension(vehiculoVM.ImagenFile.FileName);

            string rutaCompletaImagen = Path.Combine(entorno.WebRootPath, "imagenes", nuevoNombreArchivo);
            using (var stream = System.IO.File.Create(rutaCompletaImagen))
            {
                await vehiculoVM.ImagenFile.CopyToAsync(stream);
            }

            var modelo = await _context.Modelos.FindAsync(vehiculoVM.ModeloIDMODELO);
            Vehiculo v = new Vehiculo()
            {
                NRO_PLACA = vehiculoVM.NRO_PLACA,
                año = vehiculoVM.año,
                Color = vehiculoVM.Color,
                estado = vehiculoVM.estado,
                Imagen = nuevoNombreArchivo,
                ModeloIDMODELO = vehiculoVM.ModeloIDMODELO,
                Modelo = modelo
            };
            _context.Vehiculos.Add(v);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
            return View(new VehiculoVM
            {
                IDVEHICULO = vehiculo.IDVEHICULO,
                NRO_PLACA = vehiculo.NRO_PLACA,
                año = vehiculo.año,
                estado = vehiculo.estado,
                Color = vehiculo.Color,
                ModeloIDMODELO = vehiculo.ModeloIDMODELO,
                // ImagenFile se inicializará vacío aquí ya que no se almacena el archivo en sí.
            });
        }

        // POST: Vehiculos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehiculoVM viewModel)
        {
            if (id != viewModel.IDVEHICULO)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var vehiculo = await _context.Vehiculos.FindAsync(id);
                    if (vehiculo == null)
                    {
                        return NotFound();
                    }

                    vehiculo.NRO_PLACA = viewModel.NRO_PLACA;
                    vehiculo.año = viewModel.año;
                    vehiculo.estado = viewModel.estado;
                    vehiculo.Color = viewModel.Color;
                    vehiculo.ModeloIDMODELO = viewModel.ModeloIDMODELO;

                    if (viewModel.ImagenFile != null)
                    {
                        string wwwRootPath = entorno.WebRootPath;
                        string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(viewModel.ImagenFile.FileName);
                        string path = Path.Combine(wwwRootPath + "/imagenes/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await viewModel.ImagenFile.CopyToAsync(fileStream);
                        }
                        vehiculo.Imagen = fileName;
                    }

                    _context.Update(vehiculo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiculoExists(viewModel.IDVEHICULO))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            var modelo = await _context.Modelos.FindAsync(viewModel.ModeloIDMODELO);
            var marcaIDMARCA = modelo?.MarcaIDMARCA ?? 0;

            ViewBag.Marcas = new SelectList(_context.Marcas, "IDMARCA", "NOM_MARCA", marcaIDMARCA);
            ViewBag.Modelos = new SelectList(_context.Modelos.Where(m => m.MarcaIDMARCA == marcaIDMARCA), "IDMODELO", "NOM_MODELO", viewModel.ModeloIDMODELO);
            return View(viewModel);
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

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.IDVEHICULO == id);
        }
    }
}