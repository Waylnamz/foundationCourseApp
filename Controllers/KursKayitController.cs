using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers

{
    public class KursKayitController : Controller
    {
        private readonly DataContext _context;

        public KursKayitController(DataContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var KursKayitlari = await _context
                                        .KursKayitlari
                                        .Include(x => x.Ogrenci)
                                        .Include(x => x.Kurs)
                                        .ToListAsync();
            return View(KursKayitlari);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit model)
        {

            DateTime localDateTime = DateTime.Now;

            // Convert local time to UTC
            DateTime utcDateTime = localDateTime.ToUniversalTime();

            // Set the KayitTarihi property to the UTC time
            model.KayitTarihi = utcDateTime;

            _context.KursKayitlari.Add(model);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            if(id == null){return NotFound();}
            var krskayit = await _context.KursKayitlari.FindAsync(id);
            if(krskayit == null){return NotFound();}

            return View(krskayit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,KursKayit model)
        {
            if(id != model.KayitId){return NotFound();}
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!_context.KursKayitlari.Any(k =>k.KayitId == model.KayitId)){return NotFound();}
                    else{throw;}
                }
                return RedirectToAction("Index");
            }
            return View(model);

        }
        
        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null){return NotFound();}
            var kurskayit = await _context
                            .KursKayitlari
                            .Include(x => x.Ogrenci)
                            .Include(x => x.Kurs)
                            .FirstOrDefaultAsync(x => x.KayitId == id);
            if(kurskayit==null){return NotFound();}
            return View(kurskayit);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int KayitId)
        {
            var kurskayit = await _context.KursKayitlari.FindAsync(KayitId);
            if(kurskayit==null){return NotFound();}
            _context.KursKayitlari.Remove(kurskayit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}