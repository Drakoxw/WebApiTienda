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
            // [2023-03-16 10:14:49] qa.ERROR: Se detecto inconsitencias en la respuesta de Cifin  
            string path = GetRoute() + "info.log";
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string valueSave = "["+ date + "] QA.INFO: " + value;
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
    }
}
