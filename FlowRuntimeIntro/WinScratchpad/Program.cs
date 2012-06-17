using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using npantarhei.runtime;

namespace WinScratchpad
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

            var config = new FlowRuntimeConfiguration();

            config.AddStreamsFrom("WinScratchpad.flows.flow", Assembly.GetExecutingAssembly());

            config.AddFunc<string, string>("toUpper", Program.ToUpper).MakeParallel();
            config.AddFunc<string, string>("reverse", Program.Reverse);

            var dlg = new Dialog();
            config.AddAction<Correlation>("display", dlg.Display).MakeSync();
            config.AddOperation(new Correlator());

            using (var fr = new FlowRuntime(config))
            {
                dlg.Transform_text += fr.CreateEventProcessor<Correlation>(".transform_text");

                Application.Run(dlg);
            }
        }

        static string ToUpper(string text)
        {
            Thread.Sleep((DateTime.Now.Second % 5 + 1)*1000);
            return text.ToUpper();
        }

        static string Reverse(string text)
        {
            return new string(text.ToCharArray().Reverse().ToArray());
        }
    }
}
