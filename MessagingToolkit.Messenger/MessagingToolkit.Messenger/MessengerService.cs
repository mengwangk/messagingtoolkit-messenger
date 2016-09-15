using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;
using System.Configuration.Install;
using System.Collections;
using System.Reflection;
using System.Threading;
using MessagingToolkit.Messenger.Log;
using MessagingToolkit.Messenger.Database;
using MessagingToolkit.Messenger.Model;

using MessagingToolkit.Core;
using MessagingToolkit.Core.Base;
using MessagingToolkit.Core.Mobile;
using MessagingToolkit.Core.Service;
using MessagingToolkit.Core.Mobile.Event;
using MessagingToolkit.Core.Mobile.Message;
using MessagingToolkit.Messenger.Polling;

namespace MessagingToolkit.Messenger
{

    /// <summary>
    /// Windows service to receive a SMS and send a reply.
    /// </summary>
    public partial class MessengerService : ServiceBase
    {
        /// <summary>
        /// Logger
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name);

        /// <summary>
        /// Database context
        /// </summary>
        private MessengerContext database = null;

        /// <summary>
        /// Messenger
        /// </summary>
        private IMobileGateway messenger = MobileGatewayFactory.Default;

        /// <summary>
        /// Thread to start the service
        /// </summary>
        private Thread serviceControlThread;

        /// <summary>
        /// Worker threads
        /// </summary>
        private List<Thread> workerThreads;

        /// <summary>
        /// Message pollers
        /// </summary>
        private List<Poller> pollers;

        public MessengerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                if (serviceControlThread != null && serviceControlThread.IsAlive)
                {
                    serviceControlThread.Abort();
                    serviceControlThread = null;
                }

                serviceControlThread = new Thread(ConfigureService);
                serviceControlThread.IsBackground = true;
                serviceControlThread.Start();
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error starting service", ex.Message), ex);
                throw ex;
            }
        }

        protected override void OnStop()
        {
            StopService();
        }

        /// <summary>
        /// Start the program in foreground
        /// </summary>
        /// <param name="args">The args.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void StartForeground(string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "/install":
                    case "-install":
                    case "--install":
                        {
                            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                            if (args.Length > 1)
                            {
                                directory = Path.GetFullPath(args[1]);
                            }
                            if (!Directory.Exists(directory))
                                throw new ArgumentException(String.Format("The directory {0} doesn't exists.", directory));

                            var transactedInstaller = new TransactedInstaller();
                            var serviceInstaller = new ServiceInstaller();
                            transactedInstaller.Installers.Add(serviceInstaller);
                            var ctx = new InstallContext();
                            ctx.Parameters["assemblypath"] = String.Format("{0} \"{1}\"", Assembly.GetExecutingAssembly().Location, directory);
                            transactedInstaller.Context = ctx;
                            transactedInstaller.Install(new Hashtable());

                            logger.Info("The service is installed.");
                        }
                        return;
                    case "/uninstall":
                    case "-uninstall":
                    case "--uninstall":
                        {
                            var transactedInstaller = new TransactedInstaller();
                            var serviceInstaller = new ServiceInstaller();
                            transactedInstaller.Installers.Add(serviceInstaller);
                            var ctx = new InstallContext();
                            ctx.Parameters["assemblypath"] = String.Format("{0}", Assembly.GetExecutingAssembly().Location);
                            transactedInstaller.Context = ctx;
                            transactedInstaller.Uninstall(null);

                            logger.Info("The service is uninstalled.");
                        }
                        return;
                    default:
                        if (args[0][0] != '/' &&
                            args[0][0] != '-')
                            throw new ArgumentException(string.Format("The argument {0} isn't supported.", args[0]));
                        break;
                }
            }

            // Invoke service OnStart method
            OnStart(args);
            logger.Info("Press ENTER to exit...");
            Console.ReadLine();
        }

        private void ConfigureService()
        {
            ConfigureApp();
            StartMessenger();
            StartPoller();
        }

        private void ConfigureApp()
        {
            this.database = new MessengerContext();
        }

        private void StartMessenger()
        {
            try
            {
                logger.Info("Start messenger");

                // TODO

                logger.Info("Messenger started successfully");
            }
            catch (Exception ex)
            {
                logger.Error("Error starting messenger", ex);
            }
        }

        private void StartPoller()
        {
            try
            {
                logger.Debug("Starting outgoing message poller");

                // Stop all pollers, just in case
                StopPoller();

                // Start to poll incoming message
                if (workerThreads == null) workerThreads = new List<Thread>(1);
                if (pollers == null) pollers = new List<Poller>(1);

                OutgoingMessagePoller outgoingMessagePoller = new OutgoingMessagePoller(this.messenger);
                outgoingMessagePoller.Name = GlobalConstants.OutgoingMsgPollerName;
                pollers.Add(outgoingMessagePoller);

                Thread worker = new Thread(new ThreadStart(outgoingMessagePoller.StartTimer));
                worker.IsBackground = true;
                worker.Name = GlobalConstants.OutgoingMsgPollerName;
                workerThreads.Add(worker);
                worker.Start();

                logger.Info("Outgoing message poller is started successfully");

            }
            catch (Exception ex)
            {
                logger.Error("Error starting poller", ex);
                StopPoller(); // calls StopPollers
            }
        }

        private void StopMessenger()
        {
            try
            {
                if (serviceControlThread != null && serviceControlThread.IsAlive)
                {
                    serviceControlThread.Abort();
                    serviceControlThread = null;
                }

                // Stop messenger
                if (messenger != null)
                {
                    messenger.Disconnect();
                    messenger.Dispose();
                    messenger = null;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error stoppping messenger", ex);
            }
        }

        private void StopPoller()
        {
            try
            {                
                if (pollers == null) return;
                foreach (Poller poller in pollers)
                {
                    logger.InfoFormat("Stopping {0}", poller.Name);
                    poller.StopTimer();
                }
                pollers.Clear();
                pollers = null;

                // Stop all threads
                foreach (Thread t in workerThreads)
                {
                    try
                    {
                        logger.InfoFormat("Stopping thread {0}", t.Name);
                        t.Abort();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(string.Format("Error aborting thread [{0}]", t.Name), ex);
                    }
                }                
                workerThreads.Clear();
                workerThreads = null;
            }
            catch (Exception ex)
            {
                logger.Error("Error stoppping poller", ex);
            }

        }

        private void StopService()
        {
            try
            {
                logger.Info("Stopping messenger");
                StopMessenger();

                logger.Info("Stop poller");
                StopPoller();

                // Dispose database
                if (database != null)
                {
                    database.Dispose();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error stopping service", ex);
            }
        }
    }
}
