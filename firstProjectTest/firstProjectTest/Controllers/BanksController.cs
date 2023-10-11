using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using firstProjectTest.Models;

namespace firstProjectTest.Controllers
{
    public class BanksController : Controller
    {
        private readonly ModelContext _context;

        public BanksController(ModelContext context)
        {
            _context = context;
        }

        // GET: Banks
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Banks.Include(b => b.Payment);
            return View(await modelContext.ToListAsync());
        }

        // GET: Banks/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Banks == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // GET: Banks/Create
        public IActionResult Create()
        {
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id");
            return View();
        }

        // POST: Banks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Cardholder,Cardnumber,Balance,Cvv,PaymentId")] Bank bank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", bank.PaymentId);
            return View(bank);
        }

        // GET: Banks/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Banks == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks.FindAsync(id);
            if (bank == null)
            {
                return NotFound();
            }
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", bank.PaymentId);
            return View(bank);
        }

        // POST: Banks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Username,Password,Cardholder,Cardnumber,Balance,Cvv,PaymentId")] Bank bank)
        {
            if (id != bank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BankExists(bank.Id))
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
            ViewData["PaymentId"] = new SelectList(_context.Payments, "Id", "Id", bank.PaymentId);
            return View(bank);
        }

        // GET: Banks/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Banks == null)
            {
                return NotFound();
            }

            var bank = await _context.Banks
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bank == null)
            {
                return NotFound();
            }

            return View(bank);
        }

        // POST: Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Banks == null)
            {
                return Problem("Entity set 'ModelContext.Banks'  is null.");
            }
            var bank = await _context.Banks.FindAsync(id);
            if (bank != null)
            {
                _context.Banks.Remove(bank);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BankExists(decimal id)
        {
          return (_context.Banks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
