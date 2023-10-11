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
    public class UsersController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnviroment;
        
        public UsersController(ModelContext context, IWebHostEnvironment webHostEnviermoment)
        {
            _context = context;
            this.webHostEnviroment = webHostEnviermoment;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Users.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,ProfileImage,PhoneNumber,Username,Password,RoleId,ImageFile")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.ImageFile != null)
                {
                    string wwwrootPath = webHostEnviroment.WebRootPath;//return path of w3root 
                    string fileName = Guid.NewGuid().ToString() + user.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(fileStream);
                    }
                    user.ProfileImage= fileName;
                }
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Email,ProfileImage,PhoneNumber,Username,Password,RoleId,ImageFile")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.ImageFile != null)
                    {
                        string wwwrootPath = webHostEnviroment.WebRootPath;//return path of w3root 
                        string fileName = Guid.NewGuid().ToString() + user.ImageFile.FileName;
                        string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await user.ImageFile.CopyToAsync(fileStream);
                        }
                        user.ProfileImage = fileName;
                    }
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ModelContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(decimal id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
