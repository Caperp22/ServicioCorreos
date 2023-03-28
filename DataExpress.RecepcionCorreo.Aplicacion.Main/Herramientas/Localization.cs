using System;
using System.Linq;

namespace DataExpress.RecepcionCorreo.Aplicacion.Main.Herramientas
{
    public class Localization
    {
        private static TimeZoneInfo _timeZone = null;
        public static DateTime GetNistTime()
        {
            DateTime dateTime = DateTime.Now;
            TimeZoneInfo TimeZone = TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(t => t.Id.ToUpper().Contains("SA PACIFIC STANDARD TIME"));
            string dateTime1 = TimeZoneInfo.ConvertTime(dateTime, TimeZone).ToString("s");
            dateTime = Convert.ToDateTime(dateTime1);
            return dateTime;
        }

        public static TimeZoneInfo TimeZone
        {
            get
            {
                return (_timeZone == null) ? TimeZoneInfo.GetSystemTimeZones().FirstOrDefault(t => t.Id.ToUpper().Contains("SA PACIFIC STANDARD TIME")) : _timeZone;
            }
            set
            {
                _timeZone = value;
            }
        }

        public static DateTime Now { get { return GetNistTime(); } }
        public static DateTime Parse(string s)
        {
            var parsed = DateTime.Parse(s);
            return TimeZoneInfo.ConvertTime(parsed, TimeZone);
        }
        public static DateTime Parse(string s, IFormatProvider provider)
        {
            var parsed = DateTime.Parse(s, provider);
            return TimeZoneInfo.ConvertTime(parsed, TimeZone);
        }
        public static DateTime Parse(string s, IFormatProvider provider, System.Globalization.DateTimeStyles styles)
        {
            var parsed = DateTime.Parse(s, provider, styles);
            return TimeZoneInfo.ConvertTime(parsed, TimeZone);
        }
        public static DateTime ParseExact(string s, string format, IFormatProvider provider)
        {
            var parsed = DateTime.ParseExact(s, format, provider);
            return TimeZoneInfo.ConvertTime(parsed, TimeZone);
        }
        public static DateTime ParseExact(string s, string format, IFormatProvider provider, System.Globalization.DateTimeStyles style)
        {
            var parsed = DateTime.ParseExact(s, format, provider, style);
            return TimeZoneInfo.ConvertTime(parsed, TimeZone);
        }
        public static DateTime ParseExact(string s, string[] format, IFormatProvider provider, System.Globalization.DateTimeStyles style)
        {
            var parsed = DateTime.ParseExact(s, format, provider, style);
            return TimeZoneInfo.ConvertTime(parsed, TimeZone);
        }
        public static bool TryParse(string s, out DateTime result)
        {
            var parsed = new DateTime();
            bool status = false;
            try
            {
                parsed = DateTime.Parse(s);
                status = true;
            }
            catch
            {
                status = false;
            }
            result = parsed;
            return status;
        }
        public static bool TryParse(string s, IFormatProvider provider, out DateTime result)
        {
            var parsed = new DateTime();
            bool status = false;
            try
            {
                parsed = DateTime.Parse(s, provider);
                status = true;
            }
            catch
            {
                status = false;
            }
            result = parsed;
            return status;
        }
        public static bool TryParse(string s, IFormatProvider provider, System.Globalization.DateTimeStyles styles, out DateTime result)
        {
            var parsed = new DateTime();
            bool status = false;
            try
            {
                parsed = DateTime.Parse(s, provider, styles);
                status = true;
            }
            catch
            {
                status = false;
            }
            result = parsed;
            return status;
        }
    }
}
