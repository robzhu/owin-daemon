using Microsoft.Owin.Hosting;
using Owin;
using Serilog;
using System;
using System.IO;
using System.ServiceProcess;

namespace Owin_Daemon
{
    public partial class Service1 : ServiceBase
    {
        private void InitializeLogging()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var logs = Path.Combine(baseDirectory, "logs");

            Directory.CreateDirectory(logs);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                //.WriteTo.RollingFile(Path.Combine(logs, "{Date}.log"))
                .CreateLogger();
        }

        public Service1()
        {
            InitializeLogging();
        }

        IDisposable _app;

        public void StartConsole()
        {
            Console.WriteLine("Press any key to exit...");
            OnStart(new string[] { });

            Console.ReadKey();
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            Log.Logger.Information("Starting service...");
            _app = WebApp.Start("http://*:12345", app =>
            {
                app.UseWelcomePage("/");
            });

            Log.Logger.Information("Service started on http://localhost:12345");

            //while ( true)
            //{
            //    Thread.Sleep(1000);
            //    Log.Logger.Information("meow");
            //}
        }

        protected override void OnStop()
        {
            Log.Logger.Information("stopping...");
            _app.Dispose();
        }
    }
}
