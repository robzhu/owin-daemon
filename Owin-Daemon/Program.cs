using System;
using System.ServiceProcess;

namespace Owin_Daemon
{
    static class Program
    {
        static void Main()
        {
            var service = new Service1();

            if (Environment.UserInteractive)
            {
                service.StartConsole();
            }
            else
            {
                ServiceBase.Run(new ServiceBase[] { service });
            }
        }
    }
}
