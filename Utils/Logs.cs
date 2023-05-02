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

        public static void SaveLog(string value)
        {
            string path = GetRoute() + "info.log";
            if (!File.Exists(path))
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    file.WriteLine(value);
                }
            } else {
                using (StreamWriter file = new StreamWriter(path, true))
                {
                    file.WriteLine(value);
                }
            }

        }
    }
}
