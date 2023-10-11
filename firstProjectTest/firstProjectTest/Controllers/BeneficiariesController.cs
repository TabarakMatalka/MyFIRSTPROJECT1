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
    public class BeneficiariesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnviroment;

        public BeneficiariesController(ModelContext context, IWebHostEnvironment webHostEnviermoment)
        {
            _context = context;
            this.webHostEnviroment = webHostEnviermoment;
        }

        // GET: Beneficiaries
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Beneficiaries.Include(b => b.Subscription);
            return View(await modelContext.ToListAsync());
        }

        // GET: Beneficiaries/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .Include(b => b.Subscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // GET: Beneficiaries/Create
        public IActionResult Create()
        {
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id");
            return View();
        }

        // POST: Beneficiaries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Relationship,Age,Status,ProofImage,SubscriptionId,ImageFile")] Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                if (beneficiary.ImageFile != null)
                {
                    string wwwrootPath = webHostEnviroment.WebRootPath;//return path of w3root 
                    string fileName = Guid.NewGuid().ToString() + beneficiary.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await beneficiary.ImageFile.CopyToAsync(fileStream);
                    }
                    beneficiary.ProofImage = fileName;
                }
                _context.Add(beneficiary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id", beneficiary.SubscriptionId);
            return View(beneficiary);
        }

        // GET: Beneficiaries/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary == null)
            {
                return NotFound();
            }
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id", beneficiary.SubscriptionId);
            return View(beneficiary);
        }

        // POST: Beneficiaries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Relationship,Age,Status,ProofImage,SubscriptionId,ImageFile")] Beneficiary beneficiary)
        {
            if (id != beneficiary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (beneficiary.ImageFile != null)
                    {
                        string wwwrootPath = webHostEnviroment.WebRootPath;//return path of w3root 
                        string fileName = Guid.NewGuid().ToString() + beneficiary.ImageFile.FileName;
                        string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await beneficiary.ImageFile.CopyToAsync(fileStream);
                        }
                        beneficiary.ProofImage = fileName;
                    }
                    _context.Update(beneficiary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeneficiaryExists(beneficiary.Id))
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
            ViewData["SubscriptionId"] = new SelectList(_context.Subscriptions, "Id", "Id", beneficiary.SubscriptionId);
            return View(beneficiary);
        }

        // GET: Beneficiaries/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Beneficiaries == null)
            {
                return NotFound();
            }

            var beneficiary = await _context.Beneficiaries
                .Include(b => b.Subscription)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beneficiary == null)
            {
                return NotFound();
            }

            return View(beneficiary);
        }

        // POST: Beneficiaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Beneficiaries == null)
            {
                return Problem("Entity set 'ModelContext.Beneficiaries'  is null.");
            }
            var beneficiary = await _context.Beneficiaries.FindAsync(id);
            if (beneficiary != null)
            {
                _context.Beneficiaries.Remove(beneficiary);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeneficiaryExists(decimal id)
        {
          return (_context.Beneficiaries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
