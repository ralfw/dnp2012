using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using npantarhei.runtime;
using npantarhei.runtime.contract;

namespace Taschenrechner
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

            var operation = new Operationsschlange();
            var akku = new Zwischenergebnis();
            var ui = new WinMain();

            var config = new FlowRuntimeConfiguration()
                .AddStreamsFrom("Taschenrechner.root.flow", Assembly.GetExecutingAssembly())

                .AddFunc<int, int>("ergebnis_zwischenspeichern", akku.Merken)
                .AddFunc<Tuple<int, int, Operatoren>, int>("operanden_verknuepfen", Rechenwerk.Operanden_verknüpfen)
                .AddFunc<Rechenauftrag, int>("operation_speichern", operation.Einstellen)
                .AddPushCausality("pushc")
                .AddPopCausality("popc")
                .AddFunc<Tuple<int, int>, Tuple<int, int, Operatoren>>("vormalige_operation_laden", operation.Herausholen)
                .AddEventBasedComponent("akku", akku)
                .AddEventBasedComponent("ui", ui);

            using (var fr = new FlowRuntime(config))
            {
                fr.Message += Console.WriteLine;

                fr.UnhandledException += fr.CreateEventProcessor<FlowRuntimeException>(".error");

                Application.Run(ui);
            }
        }
    }
}
