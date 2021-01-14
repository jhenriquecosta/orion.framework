using System;
using System.Globalization;
using Orion.Framework.Validations;


namespace Orion.Framework.Helpers
{
    public class ConvertHelper
    {

        public static int ToInteiro(object valor)
        {
            var valorConvertido = valor.ToString();
            return ToInteiro(valorConvertido);
        }

        public static T ToCast<T>(object valor)
        {
            var valorConvertido = valor.ToString();
            return (T)TypeConvert.To<T>(valorConvertido);
        }

        public static decimal ToDecimal(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? 0 : decimal.Parse(valor);
        }

        public static int ToInteiro(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? 0 : int.Parse(valor);
        }

        public static float ToFloat(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? 0 : float.Parse(valor);
        }

        public static DateTime? ToDateTime(object valor)
        {
            return ToDateTime(valor.ToString());
        }

        public static DateTime? ToDateTime(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor) || ToInteiro(valor) == 0)
            {
                return null;
            }
            if ( valor.Length == 8 && !Checks.IsDate(valor))
            {
               
                //00000000
                var year =ToInteiro(valor.Substring(0, 4));
                var month = ToInteiro(valor.Substring(4, 2));
                var day = ToInteiro(valor.Substring(6, 2));
                return new DateTime(year,month,day);
            }



            return DateTime.Parse(valor);

        }

        

        public static bool ToBoolean(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                return false;
            }

            if ((valor == "True") || (valor == "true") || (valor == "S") || (valor == "Sim") || (valor == "1"))
            {
                return true;
            }

            if ((valor == "False") || (valor == "false") || (valor == "N") || (valor == "Não") || valor == "0")
            {
                return false;
            }


            return true;
        }

        public bool IsDate(string date)
        {
            return DateTime.TryParse(date, out var temp) &&
                   temp.Hour == 0 &&
                   temp.Minute == 0 &&
                   temp.Second == 0 &&
                   temp.Millisecond == 0 &&
                   temp > DateTime.MinValue;
        }
    }
}