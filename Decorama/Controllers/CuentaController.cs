using Decorama.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Decorama.Controllers
{
    public class CuentaController : Controller
    {
        // GET: Cuenta
        decoramaEntities db = new decoramaEntities();

        public ActionResult Index()
        {
            if (!SessionH.Logueado)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Seccion()
        {
            if (!SessionH.Logueado)
            {
                return RedirectToAction("Login");
            }
            SeccionViewModel model = new SeccionViewModel();
            model.Secciones = db.Seccion.ToList();
            return View(model);
        }

        public ActionResult Imagenes()
        {
            if (!SessionH.Logueado)
            {
                return RedirectToAction("Login");
            }
            ImagenesViewModel model = new ImagenesViewModel();
            model.Imagenes = db.Imagen.ToList();
            return View(model);
        }
        public ActionResult Eliminar(int id)
        {
            db.Imagen.Remove(db.Imagen.First(x => x.Id == id));
            db.SaveChanges();
            return RedirectToAction("Imagenes");
        }

        [HttpPost]
        public ActionResult NuevaImagen(ImagenFormModel Form)
        {
            //if (!SessionH.Logueado) return RedirectToAction("Index", "Home");
            var path = string.Empty;
            string fileName = string.Empty;
            if (Request.Files.Count > 0 && Form.Imagen != null)
            {
                string carpetaCliente = Server.MapPath("~/img/Publicaciones/");
                if (!Directory.Exists(carpetaCliente))
                {
                    Directory.CreateDirectory(carpetaCliente);
                }
                //CI Front
                var CIFront = Request.Files[0];
                if (CIFront != null && CIFront.ContentLength > 0)
                {
                    fileName = DateTime.Now.Ticks.ToString() + CIFront.FileName;
                    path = Path.Combine(carpetaCliente, fileName);
                    CIFront.SaveAs(path);
                }
            }
            else
            {
                Form.Mensaje = "Es obligatorio subir una fotografia";
                return View("ModificarImagen", Form);
            }
            path = "Publicaciones/" + fileName;
            Form.PathImagen = path;
            try
            {
                Imagen _imagen = new Imagen();
                _imagen.Fecha = DateTime.Now;
                _imagen.Nombre = Form.Nombre;
                _imagen.Ruta = Form.PathImagen;
                _imagen.Tipo = Form.Tipo;

                db.Imagen.Add(_imagen);

                db.SaveChanges();
                Form.Mensaje = "Imagen guardada correctamente";
            }
            catch (Exception ex)
            {
                Form.Mensaje = ex.Message;
            }
            return View("ModificarImagen", Form);
        }

        [HttpPost]
        public ActionResult ModificarImagen(ImagenFormModel Form)
        {
            //if (!SessionH.Logueado) return RedirectToAction("Index", "Home");
            var path = string.Empty;
            string fileName = string.Empty;
            if (Request.Files.Count > 0 && Form.Imagen != null)
            {
                string carpetaCliente = Server.MapPath("~/img/Publicaciones/");
                if (!Directory.Exists(carpetaCliente))
                {
                    Directory.CreateDirectory(carpetaCliente);
                }
                //CI Front
                var CIFront = Request.Files[0];
                if (CIFront != null && CIFront.ContentLength > 0)
                {
                    fileName = DateTime.Now.Ticks.ToString() + CIFront.FileName;
                    path = Path.Combine(carpetaCliente, fileName);
                    CIFront.SaveAs(path);

                }
            }
            if (Form.Imagen != null)
            {
                path = "Publicaciones/" + fileName;
                Form.PathImagen = path;
            }
            try
            {
                var _imagen = db.Imagen.First(x => x.Id == Form.Id);
                _imagen.Nombre = Form.Nombre;
                _imagen.Tipo = Form.Tipo;
                _imagen.Ruta = Form.PathImagen != null ? Form.PathImagen : _imagen.Ruta;
                _imagen.Fecha = DateTime.Now;
                db.Entry(_imagen).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Form.Mensaje = "Imagen modificada correctamente";
            }
            catch (Exception ex)
            {
                Form.Mensaje = ex.Message;
            }
            return View("ModificarImagen", Form);
        }

        public ActionResult Servicio()
        {
            if (!SessionH.Logueado)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            LoginOwnViewModel model = new LoginOwnViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginOwnViewModel model)
        {
            if (model.Usuario == "admin" && model.Pass == "admin")
            {
                SessionH.Logueado = true;
               
                return RedirectToAction("Seccion");
            }
            return View(model);
        }

        public ActionResult Portafolio()
        {
            if (!SessionH.Logueado)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        public JsonResult GetSeccion(int id)
        {
            var seccion = db.Seccion.First(x => x.Id == id);
            return new JsonResult() { Data = seccion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult GetImagen(int id)
        {
            var seccion = db.Imagen.First(x => x.Id == id);
            return new JsonResult() { Data = seccion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [ValidateInput(false)]
        [HttpPost]
        public JsonResult SaveSeccion(Seccion model)
        {
            var seccion = db.Seccion.First(x => x.Id == model.Id);
            seccion.Titulo = model.Titulo;
            seccion.Contenido = model.Contenido;
            seccion.Fecha = DateTime.Now;

            db.Entry(seccion).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return new JsonResult() { Data = seccion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult DeleteSeccion(int id)
        {
            var seccion = db.Seccion.First(x => x.Id == id);
            db.Seccion.Remove(seccion);
            return new JsonResult() { Data = seccion, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
