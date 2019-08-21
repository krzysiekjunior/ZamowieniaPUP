using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZamowieniaPUP.Data;
using ZamowieniaPUP.Models;

namespace ZamowieniaPUP.Controllers
{
    public class RokZamowieniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RokZamowieniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RokZamowienies
        public async Task<IActionResult> Index()
        {
         //   var zamowienie = _context.Zamowienie.Include(z => z.CenaNetto);
         //   ViewBag.Total = zamowienie.Sum(x => x.CenaNetto);

            return View(await _context.RokZamowienie.ToListAsync());
        }

        // GET: RokZamowienies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RokZamowienie rokZamowienie = await _context.RokZamowienie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rokZamowienie == null)
            {
                return NotFound();
            }

            var zamowienie = _context.Zamowienie.Include(z => z.CenaNetto);         //Sumowanie ceny netto
            ViewBag.Total = zamowienie.Sum(x => x.CenaNetto);
            //Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Zamowienie, float> zamowienie = _context.Zamowienie.Include(z => z.CenaNetto);
            //ViewBag.Total = zamowienie.Sum(x => x.CenaNetto);

            ZamowienieViewModel viewModel = await GetZamowienieViewModelFromRokZamowienie(rokZamowienie);

            return View(viewModel);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details([Bind("RokID,UslugaDostawa,Ilosc,CenaNetto,Kontrahent,CzyUmowa")] ZamowienieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Zamowienie zamowienie = new Zamowienie();

                zamowienie.UslugaDostawa = viewModel.UslugaDostawa;
                zamowienie.Ilosc = viewModel.Ilosc;
                zamowienie.CenaNetto = viewModel.CenaNetto;
                zamowienie.Kontrahent = viewModel.Kontrahent;
                zamowienie.CzyUmowa = viewModel.CzyUmowa;

                RokZamowienie rokZamowienie = await _context.RokZamowienie
               .FirstOrDefaultAsync(m => m.Id == viewModel.RokID);
                if (rokZamowienie == null)
                {
                    return NotFound();
                }

                zamowienie.TenRokZamowienie = rokZamowienie;
                _context.Zamowienie.Add(zamowienie);
                await _context.SaveChangesAsync();

                viewModel = await GetZamowienieViewModelFromRokZamowienie(rokZamowienie);
            }

            return View(viewModel);
        }

        private async Task<ZamowienieViewModel> GetZamowienieViewModelFromRokZamowienie(RokZamowienie rokZamowienie)
        {
            ZamowienieViewModel viewModel = new ZamowienieViewModel();

            viewModel.RokZamowienie = rokZamowienie;


            //sprawdzic przy relacjach między tabelami
            List<Zamowienie> zamowienia = await _context.Zamowienie
                .Where(m => m.TenRokZamowienie == rokZamowienie).ToListAsync();

            viewModel.Zamowienia = zamowienia;
            return viewModel;
        }



        // GET: RokZamowienies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RokZamowienies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rok")] RokZamowienie rokZamowienie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rokZamowienie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rokZamowienie);
        }

        // GET: RokZamowienies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rokZamowienie = await _context.RokZamowienie.FindAsync(id);
            if (rokZamowienie == null)
            {
                return NotFound();
            }
            return View(rokZamowienie);
        }

        // POST: RokZamowienies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rok")] RokZamowienie rokZamowienie)
        {
            if (id != rokZamowienie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rokZamowienie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RokZamowienieExists(rokZamowienie.Id))
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
            return View(rokZamowienie);
        }

        // GET: RokZamowienies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rokZamowienie = await _context.RokZamowienie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rokZamowienie == null)
            {
                return NotFound();
            }

            return View(rokZamowienie);
        }

        // POST: RokZamowienies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rokZamowienie = await _context.RokZamowienie.FindAsync(id);
            _context.RokZamowienie.Remove(rokZamowienie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RokZamowienieExists(int id)
        {
            return _context.RokZamowienie.Any(e => e.Id == id);
        }
    }
}
