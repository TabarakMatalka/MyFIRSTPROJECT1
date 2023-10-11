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
    public class AboutuspagesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnviroment;
        public AboutuspagesController(ModelContext context,IWebHostEnvironment webHostEnviermoment)
        {
            _context = context;
            this.webHostEnviroment = webHostEnviermoment;
        }

        // GET: Aboutuspages
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Aboutuspages.Include(a => a.Homepage);
            return View(await modelContext.ToListAsync());
        }

        // GET: Aboutuspages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Aboutuspages == null)
            {
                return NotFound();
            }

            var aboutuspage = await _context.Aboutuspages
                .Include(a => a.Homepage)
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (aboutuspage == null)
            {
                return NotFound();
            }

            return View(aboutuspage);
        }

        // GET: Aboutuspages/Create
        public IActionResult Create()
        {
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId");
            return View();
        }

        // POST: Aboutuspages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,Title,Paragraph,Location,Image,Email,PhoneNumber,HomepageId,ImageFile")] Aboutuspage aboutuspage)
        {
            if (aboutuspage.ImageFile != null)
            {
                string wwwrootPath = webHostEnviroment.WebRootPath;//return path of w3root 
                string fileName = Guid.NewGuid().ToString() + aboutuspage.ImageFile.FileName;
                string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await aboutuspage.ImageFile.CopyToAsync(fileStream);
                }
                aboutuspage.Image = fileName;
            }
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId", aboutuspage.HomepageId);
            return View(aboutuspage);
        }

        // GET: Aboutuspages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Aboutuspages == null)
            {
                return NotFound();
            }

            var aboutuspage = await _context.Aboutuspages.FindAsync(id);
            if (aboutuspage == null)
            {
                return NotFound();
            }
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId", aboutuspage.HomepageId);
            return View(aboutuspage);
        }

        // POST: Aboutuspages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("PageId,Title,Paragraph,Location,Image,Email,PhoneNumber,HomepageId,ImageFile")] Aboutuspage aboutuspage)
        {
            if (id != aboutuspage.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutuspage.ImageFile != null)
                    {
                        string wwwrootPath = webHostEnviroment.WebRootPath;//return path of w3root 
                        string fileName = Guid.NewGuid().ToString() + aboutuspage.ImageFile.FileName;
                        string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await aboutuspage.ImageFile.CopyToAsync(fileStream);
                        }
                        aboutuspage.Image = fileName;
                    }
                    _context.Update(aboutuspage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutuspageExists(aboutuspage.PageId))
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
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId", aboutuspage.HomepageId);
            return View(aboutuspage);
        }

        // GET: Aboutuspages/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Aboutuspages == null)
            {
                return NotFound();
            }

            var aboutuspage = await _context.Aboutuspages
                .Include(a => a.Homepage)
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (aboutuspage == null)
            {
                return NotFound();
            }

            return View(aboutuspage);
        }

        // POST: Aboutuspages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Aboutuspages == null)
            {
                return Problem("Entity set 'ModelContext.Aboutuspages'  is null.");
            }
            var aboutuspage = await _context.Aboutuspages.FindAsync(id);
            if (aboutuspage != null)
            {
                _context.Aboutuspages.Remove(aboutuspage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AboutuspageExists(decimal id)
        {
          return (_context.Aboutuspages?.Any(e => e.PageId == id)).GetValueOrDefault();
        }
    }
}
