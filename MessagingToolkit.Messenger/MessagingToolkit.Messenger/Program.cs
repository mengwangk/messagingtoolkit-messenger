using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

using MessagingToolkit.Messenger.Log;

namespace MessagingToolkit.Messenger
{
    /// <summary>
    /// Start up program.
    /// </summary>
    static class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (!Environment.UserInteractive)
            {
                ConfigureApp();
                ServiceBase[] servicesToRun;
                servicesToRun = new ServiceBase[]
                {
                   new MessengerService()
                };
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                try
                {
                    EnableConsole();
                    ConfigureApp();
                    var service = new MessengerService();
                    service.StartForeground(args);
                }
                catch (Exception ex)
                {
                    logger.Info(ex);
                    var innerExc = ex.InnerException;
                    while (innerExc != null)
                    {
                        logger.Info(innerExc);
                        innerExc = innerExc.InnerException;
                    }
                }
            }
        }

        private const int ATTACH_PARENT_PROCESS = -1;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        private static extern bool AttachConsole(int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1060:MovePInvokesToNativeMethodsClass")]
        private static extern bool AllocConsole();

        /// <summary>
        /// Enables console mode if it is not started as Windows Service.
        /// </summary>
        public static void EnableConsole()
        {
            if (!AttachConsole(ATTACH_PARENT_PROCESS))
            {
                AllocConsole();
            }
        }


        /// <summary>
        /// Configures the application.
        /// </summary>
        private static void ConfigureApp()
        {
            string currentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            AppDomain.CurrentDomain.SetData("DataDirectory", currentPath);

            //if (Environment.UserInteractive)
            //{
                // Set logging to console by default. Change it to implement NLog or Log4Net
            //    LogManager.SetLogFactory(new ConsoleLogFactory());
            //}
        }

    }
}
