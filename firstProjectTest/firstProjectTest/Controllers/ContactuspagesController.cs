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
    public class ContactuspagesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnviroment;
        public ContactuspagesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Contactuspages
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Contactuspages.Include(c => c.Homepage);
            return View(await modelContext.ToListAsync());
        }

        // GET: Contactuspages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Contactuspages == null)
            {
                return NotFound();
            }

            var contactuspage = await _context.Contactuspages
                .Include(c => c.Homepage)
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (contactuspage == null)
            {
                return NotFound();
            }

            return View(contactuspage);
        }

        // GET: Contactuspages/Create
        public IActionResult Create()
        {
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId");
            return View();
        }

        // POST: Contactuspages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,Message,Name,Email,HomepageId")] Contactuspage contactuspage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactuspage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId", contactuspage.HomepageId);
            return View(contactuspage);
        }

        // GET: Contactuspages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Contactuspages == null)
            {
                return NotFound();
            }

            var contactuspage = await _context.Contactuspages.FindAsync(id);
            if (contactuspage == null)
            {
                return NotFound();
            }
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId", contactuspage.HomepageId);
            return View(contactuspage);
        }

        // POST: Contactuspages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("PageId,Message,Name,Email,HomepageId")] Contactuspage contactuspage)
        {
            if (id != contactuspage.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactuspage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactuspageExists(contactuspage.PageId))
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
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId", contactuspage.HomepageId);
            return View(contactuspage);
        }

        // GET: Contactuspages/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Contactuspages == null)
            {
                return NotFound();
            }

            var contactuspage = await _context.Contactuspages
                .Include(c => c.Homepage)
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (contactuspage == null)
            {
                return NotFound();
            }

            return View(contactuspage);
        }

        // POST: Contactuspages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Contactuspages == null)
            {
                return Problem("Entity set 'ModelContext.Contactuspages'  is null.");
            }
            var contactuspage = await _context.Contactuspages.FindAsync(id);
            if (contactuspage != null)
            {
                _context.Contactuspages.Remove(contactuspage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactuspageExists(decimal id)
        {
          return (_context.Contactuspages?.Any(e => e.PageId == id)).GetValueOrDefault();
        }
    }
}
