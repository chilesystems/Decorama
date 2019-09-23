using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Decorama.Models
{
    public static class Helpers
    {
        public static String convertToString(this Enum eff)
        {
            return Enum.GetName(eff.GetType(), eff);
        }

        public static EnumType converToEnum<EnumType>(this String enumValue)
        {
            return (EnumType)Enum.Parse(typeof(EnumType), enumValue);
        }
    }

    public class SessionH
    {
        private const string VAR_LOGUEADO = "logueado";
        private const string VAR_IMAGENES = "imagenes";

        private static T Lee<T>(string variable)
        {
            object valor = HttpContext.Current.Session[variable];
            if (valor == null)
                return default(T);
            else return ((T)valor);
        }

        private static void Escribe(string variable, object valor)
        {
            HttpContext.Current.Session[variable] = valor;
        }

        public static bool Logueado
        {
            get { return Lee<bool>(VAR_LOGUEADO); }
            set { Escribe(VAR_LOGUEADO, value); }
        }

        public static List<Imagen> Imagenes
        {
            get { return Lee <List<Imagen>>(VAR_IMAGENES); }
            set { Escribe(VAR_IMAGENES, value); }
        }
    }
}