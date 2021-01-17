using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Security.Claims;

namespace Auth_.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Protected()
        {
            //Si entramos aqui tenemos la cookie
            return View();
        }

        public IActionResult Login()
        {
            // Identificar si las credenciales son validas

            List<Claim> passportClaims = new List<Claim>
            {
                // Pequeños fragmentos de informacion llamados claims que puede poseer un pasaporte
                new Claim(ClaimTypes.Name,"Juanito Perez"),
                new Claim(ClaimTypes.Email,"Juanito@gmail.com"),
                new Claim("NPasaporte","12345")
            };
            //Creamos la identidad pasaporte
            var passportIDentity = new ClaimsIdentity(passportClaims, "passport");

            List<Claim> nationalIdentificationClaims = new List<Claim>
            {
                  //claims que posee una identificacion nacional
                  new Claim(ClaimTypes.Name,"Juanito Perez"),
                  new Claim("fecha_nac","12/05/1996")
            };
            //Creamos la identidad 
            var nationalIdentity = new ClaimsIdentity(nationalIdentificationClaims, "identificacionNacional");

            //Creamos la identidad princial del usuario que contendra la coleccion de identidades
            var identidad = new ClaimsPrincipal(new List<ClaimsIdentity> { passportIDentity, nationalIdentity });

            //Iniciamos session con la identidad del usuario
            HttpContext.SignInAsync(identidad);


            return RedirectToAction("Index");
        }
    }
}
