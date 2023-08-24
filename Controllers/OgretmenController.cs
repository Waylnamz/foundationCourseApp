using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace efcoreApp.Controllers
{
    public class OgretmenController : Controller
    {

        private readonly DataContext _context;
        public OgretmenController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogretmenler.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Ogretmen model)
        {

            DateTime localDateTime = model.BaslamaTarihi.AddDays(+1); // Replace with your DateTime value
            DateTime utcDateTime = localDateTime.ToUniversalTime();
            model.BaslamaTarihi = utcDateTime;
            _context.Ogretmenler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }
            var ogrtm = await _context.Ogretmenler.FindAsync(id);
            if (ogrtm == null) { return NotFound(); }
            return View(ogrtm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogretmen model)
        {
            if (id != model.OgretmenId) { return NotFound(); }
            if (ModelState.IsValid)
            {
                try
                {
                    DateTime localDateTime = model.BaslamaTarihi.AddDays(+1); // Replace with your DateTime value
                    DateTime utcDateTime = localDateTime.ToUniversalTime();
                    model.BaslamaTarihi = utcDateTime;
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId))
                    {
                        return NotFound();
                    }

                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }
            var ogrtm = await _context.Ogretmenler.FindAsync(id);
            if (ogrtm == null) { return NotFound(); }
            return View(ogrtm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int OgretmenId)
        {
            var ogretmen = await _context.Ogretmenler.FindAsync(OgretmenId);
            if (ogretmen == null) { return NotFound(); }
            _context.Ogretmenler.Remove(ogretmen);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}