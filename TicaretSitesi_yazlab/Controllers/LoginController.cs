using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicaretSitesi_yazlab.Models;
using TicaretSitesi_yazlab.Services;



namespace TicaretSitesi_yazlab.Controllers

{ 
   
    public class LoginController :Controller
    {   

        private readonly LaptopService _loginService;
        public LoginController(LaptopService loginService) =>
       _loginService = loginService;

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }



        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(Login admin)
        {

          
            Login data =  await _loginService.loginControl(admin);
          
            
       

                if (data != null)
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, admin.AdminName),
                      
                    };

                    var userIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties();

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(userIdentity),authProperties);



                    return RedirectToAction("AdminIndex", "Laptops");
                }

            ViewBag.Message = "sifre veya parola yanlis";
            return View("Index");
        }


        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Laptops");
        }
    }
}
