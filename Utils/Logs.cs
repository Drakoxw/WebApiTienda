
using WebApiTienda.Models;

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

        public static string GetEnv()
        {
            return Convert.ToString(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "").ToUpper();
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
            string typeEnt = GetEnv();
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

        public static void Alert(string value)
        {
            Main(value, "alert");
        }

        public static void Error(string value)
        {
            Main(value, "error");
        }

        public static void Warning(string value)
        {
            Main(value, "warning");
        }

        public static void Info(string value)
        {
            Main(value, "info");
        }

        public static LogsModel CreateLog(string mensaje, int idUser, string tipoLog, string data, string evento)
        {
            if (tipoLog == null || tipoLog == "") { tipoLog = "info"; }

            LogsModel log = new LogsModel(){ 
                Mensaje = mensaje,
                Tipo = tipoLog,
                IdUser = idUser,
                Data = data,
                Evento = evento
            };

            return log;
        }

    }

}
