using firstProjectTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace firstProjectTest.Controllers
{
    public class LoginAndRegistrController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnviermoment;
        public LoginAndRegistrController(ModelContext context, IWebHostEnvironment webHostEnviermoment)
        {
            _context = context;
            this.webHostEnviermoment = webHostEnviermoment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Id,Username,Password")] User my_user)//data from form
        {
            var auth = _context.Users.Where(x => x.Username == my_user.Username && x.Password == my_user.Password).FirstOrDefault();//first to retrive one record
                                                                                                                                             //auth if not null have all data in record
            if (auth != null)
            {
                var user = _context.Users.Where(x => x.Id == auth.Id).FirstOrDefault();
                switch (auth.RoleId)
                {
                    case 1:
                        ////set date
                        //HttpContext.Session.SetString("Fname", user.Fname);//(key,value)
                        //HttpContext.Session.SetString("Lname", user.Lname);
                        //HttpContext.Session.SetString("UserName", auth.UserName);
                        //HttpContext.Session.SetInt32("CustomerId", (int)auth.CustomerId);
                        //// HttpContext.Session.SetString("Image", user.ImagePath);

                        return RedirectToAction("Index", "Homepages");

                    case 2:
                        //HttpContext.Session.SetString("Fname", user.Fname);
                        //HttpContext.Session.SetString("Lname", user.Lname);
                        //HttpContext.Session.SetString("UserName", auth.UserName);
                        //HttpContext.Session.SetInt32("CustomerId", (int)auth.CustomerId);
                        //// HttpContext.Session.SetString("Image", user.ImagePath);

                        return RedirectToAction("Index", "Home");

                }
            }
            else
            {
                ViewBag.Error = "Invalid Account";
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Name,Email,Password,Username,PhoneNumber,ImagePath, ImageFile")] User my_user)//e+p
        {
            if (ModelState.IsValid)
            {
                if (my_user.ImageFile != null)
                {
                    string wwwrootPath = webHostEnviermoment.WebRootPath;//return path of w3root 
                    string fileName = Guid.NewGuid().ToString() + my_user.ImageFile.FileName;
                    string path = Path.Combine(wwwrootPath + "/images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await my_user.ImageFile.CopyToAsync(fileStream);
                    }
                    my_user.ProfileImage = fileName;
                    my_user.RoleId = 2;
                }
                //check uniqe:
                var check_user = _context.Users.Where(x => x.Username == my_user.Username).FirstOrDefault();
                //if there is no matching  -->add this  to database
                if (check_user == null)
                {
                    _context.Add(my_user);
                    await _context.SaveChangesAsync();

                    

                    return RedirectToAction("Index", "Home");////action,controller
                }
                else
                {
                    ViewBag.Error = "Username already used , Chose another one!";

                }
            }
            return View(my_user);
        }
    }
}
