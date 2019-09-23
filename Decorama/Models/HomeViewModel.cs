using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decorama.Models
{
    public class HomeViewModel
    {
        public Seccion SeccionInicio { get; set; }
        public Seccion SeccionNosotros { get; set; }
        public Seccion SeccionEquipo { get; set; }

        public List<Imagen> ImagenesSlider { get; set; }
        public Imagen ImagenInicio { get; set; }
        public List<Imagen> ImagenesEquipo { get; set; }
        public List<Imagen> ImagenesServicios { get; set; }
    }
}