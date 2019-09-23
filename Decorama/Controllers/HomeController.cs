using Decorama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace Decorama.Controllers
{   
    public class HomeController : Controller
    {
        decoramaEntities db = new decoramaEntities();

        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            model.SeccionEquipo = db.Seccion.First(x => x.Tipo == "Equipo");
            model.SeccionInicio = db.Seccion.First(x => x.Tipo == "Inicio");
            model.SeccionNosotros = db.Seccion.First(x => x.Tipo == "Nosotros");

            model.ImagenesEquipo = db.Imagen.Where(x => x.Tipo == "Equipo").ToList();
            model.ImagenesServicios = db.Imagen.Where(x => x.Tipo == "Servicios").ToList();
            model.ImagenesSlider = db.Imagen.Where(x => x.Tipo == "Slider").ToList();
            model.ImagenInicio = db.Imagen.FirstOrDefault(x => x.Tipo == "Inicio");

            SessionH.Imagenes = db.Imagen.ToList();

            return View(model);
        }

        public ActionResult Contacto(ContactoModel Form)
        {
            string Cuerpo = "Estimados: <br><br>";
            Cuerpo += "Tiene un nuevo mensaje de contacto de: <br>";
            Cuerpo += "Nombre: " + Form.Nombre + "<br>";
            Cuerpo += "Email: " + Form.Email + "<br>";
            Cuerpo += "Mensaje: " + Form.Mensaje + "<br><br>";
            Cuerpo += "<br><br><br><i>Este es un correo generado automáticamente</i><br>";
            Cuerpo += "<br>www.chilesystems.com<br><hr>";
            MailMessage correo = new MailMessage("general@chilesystems.com", "carolinamoralesvalin@gmail.com", "Contacto sitio web www.decoramaeventos.cl", Cuerpo);
            correo.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtpout.secureserver.net");
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Port = 25;
            smtp.Credentials = new System.Net.NetworkCredential("general@chilesystems.com", "o9imoyax");
            try
            {
                smtp.Send(correo);
            }
            catch { }
            return RedirectToAction("Index");
        }
    }
}
