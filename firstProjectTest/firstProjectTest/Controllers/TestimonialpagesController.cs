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
    public class TestimonialpagesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnviroment;

        public TestimonialpagesController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            this.webHostEnviroment = webHostEnviroment; 
        }

        // GET: Testimonialpages
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Testimonialpages.Include(t => t.Homepage).Include(t => t.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Testimonialpages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Testimonialpages == null)
            {
                return NotFound();
            }

            var testimonialpage = await _context.Testimonialpages
                .Include(t => t.Homepage)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (testimonialpage == null)
            {
                return NotFound();
            }

            return View(testimonialpage);
        }

        // GET: Testimonialpages/Create
        public IActionResult Create()
        {
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Testimonialpages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,Name,Status,Title,Content,Image,Reviewmessage,UserId,HomepageId,ImageFile")] Testimonialpage testimonialpage)
        {
            if (ModelState.IsValid)
            {
                if (testimonialpage.ImageFile != null)
                {
                    string wwwrootPath = webHostEnviroment.WebRootPath;//return path of w3root 
                    string fileName = Guid.NewGuid().ToString() + testimonialpage.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await testimonialpage.ImageFile.CopyToAsync(fileStream);
                    }
                    testimonialpage.Image = fileName;
                }
                _context.Add(testimonialpage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId", testimonialpage.HomepageId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", testimonialpage.UserId);
            return View(testimonialpage);
        }

        // GET: Testimonialpages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Testimonialpages == null)
            {
                return NotFound();
            }

            var testimonialpage = await _context.Testimonialpages.FindAsync(id);
            if (testimonialpage == null)
            {
                return NotFound();
            }
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId", testimonialpage.HomepageId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", testimonialpage.UserId);
            return View(testimonialpage);
        }

        // POST: Testimonialpages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("PageId,Name,Status,Title,Content,Image,Reviewmessage,UserId,HomepageId,ImageFile")] Testimonialpage testimonialpage)
        {
            if (id != testimonialpage.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (testimonialpage.ImageFile != null)
                    {
                        string wwwrootPath = webHostEnviroment.WebRootPath;//return path of w3root 
                        string fileName = Guid.NewGuid().ToString() + testimonialpage.ImageFile.FileName;
                        string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await testimonialpage.ImageFile.CopyToAsync(fileStream);
                        }
                        testimonialpage.Image = fileName;
                    }
                    _context.Update(testimonialpage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialpageExists(testimonialpage.PageId))
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
            ViewData["HomepageId"] = new SelectList(_context.Homepages, "PageId", "PageId", testimonialpage.HomepageId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", testimonialpage.UserId);
            return View(testimonialpage);
        }

        // GET: Testimonialpages/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Testimonialpages == null)
            {
                return NotFound();
            }

            var testimonialpage = await _context.Testimonialpages
                .Include(t => t.Homepage)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (testimonialpage == null)
            {
                return NotFound();
            }

            return View(testimonialpage);
        }

        // POST: Testimonialpages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Testimonialpages == null)
            {
                return Problem("Entity set 'ModelContext.Testimonialpages'  is null.");
            }
            var testimonialpage = await _context.Testimonialpages.FindAsync(id);
            if (testimonialpage != null)
            {
                _context.Testimonialpages.Remove(testimonialpage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialpageExists(decimal id)
        {
          return (_context.Testimonialpages?.Any(e => e.PageId == id)).GetValueOrDefault();
        }
    }
}
