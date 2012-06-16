using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

            config.AddFunc<string, string>("toUpper", Program.ToUpper);
            config.AddFunc<string, string>("reverse", Program.Reverse);

            var dlg = new Dialog();
            config.AddAction<string>("display", dlg.Display).MakeSync();

            using (var fr = new FlowRuntime(config))
            {
                dlg.Transform_text += fr.CreateEventProcessor<string>(".transform_text");

                Application.Run(dlg);
            }
        }

        static string ToUpper(string text)
        {
            return text.ToUpper();
        }

        static string Reverse(string text)
        {
            return new string(text.ToCharArray().Reverse().ToArray());
        }
    }
}
