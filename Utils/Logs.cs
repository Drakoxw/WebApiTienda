using System;
using System.Collections;

namespace WebApiTienda.Utils
{
    public class Logs
    {
        private static string GetRoute()
        {
            string routeBase = AppDomain.CurrentDomain.BaseDirectory;
            routeBase = routeBase.Replace("bin\\Debug\\net6.0\\", "Statics\\Logs\\");
            return routeBase;
        }

        //public static void EnvPrint()
        //{
        //    foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
        //    {
        //        Info(Convert.ToString(de.Key) + ":                  "+Convert.ToString(de.Value), false);
        //    }
        //    // ASPNETCORE_ENVIRONMENT:                  Development
        //}


        private static void Main(string value, string type)
        {
            string typeEnt = Convert.ToString(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "").ToUpper();
            string path = GetRoute() + type + ".log";
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string valueSave = "["+ date + "] " + typeEnt + "."+ type.ToUpper() +": " + value;
            if (!File.Exists(path))
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    file.WriteLine(valueSave);
                }
            } else {
                using (StreamWriter file = new StreamWriter(path, true))
                {
                    file.WriteLine(valueSave);
                }
            }

        }

        public static void Alert(string value, bool saveDB)
        {
            if (saveDB)
            {
                
            } else
            {
                Main(value, "alert");
            }

        }
        public static void Error(string value, bool saveDB)
        {
            if (saveDB)
            {
                
            } else
            {
                Main(value, "error");
            }

        }

        public static void Warning(string value, bool saveDB)
        {
            if (saveDB)
            {
                
            } else
            {
                Main(value, "warning");
            }

        }

        public static void Info(string value, bool saveDB)
        {
            if (saveDB)
            {
                
            } else
            {
                Main(value, "info");
            }

        }

    }

}
