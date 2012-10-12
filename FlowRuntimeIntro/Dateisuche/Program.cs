using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Dateisuche.Daten;
using Dateisuche.Operationen;
using npantarhei.runtime;
using npantarhei.runtime.config;
using npantarhei.runtime.contract;
using npantarhei.runtime.operations;

namespace Dateisuche
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var config = new FlowRuntimeConfiguration()
                            .AddStreamsFrom("Dateisuche.root.flow", Assembly.GetExecutingAssembly())
                            .AddOperations(new AssemblyCrawler(Assembly.GetExecutingAssembly()));

            //var schedule = new Schedule_for_async_depthfirst_processing();
            var schedule = new Schedule_for_sync_depthfirst_processing();

            using (var fr = new FlowRuntime(config, schedule))
            {
                fr.Throttle(20);

                fr.Message += Console.WriteLine;
                fr.UnhandledException += fr.CreateEventProcessor<FlowRuntimeException>(".error");

                Application.Run(GUI.Instance);
            }
        }
    }
}
