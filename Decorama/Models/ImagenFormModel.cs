using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decorama.Models
{
    public class ImagenFormModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public HttpPostedFileBase Imagen { get; set; }
        public string Tipo { get; set; }
        public string PathImagen { get; set; }

        public string Mensaje { get; set; }
    }
}