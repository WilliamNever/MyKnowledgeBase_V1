namespace Fast8Calculting
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppHost.SetupHost();

            //var svrs = AppHost.GetSerivces<Base8MethodService>();
            //var svr = svrs.First(x => x.Identity == F8C.Core.Enums.Create8Method.Simple_HH_MM);
            //var dsvr = svr.GetMethodClass<Simple_HH_MM_Service>();
            //var dIsvr = svr.GetIMethodInterface<Simple_HH_MM_Model>();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}