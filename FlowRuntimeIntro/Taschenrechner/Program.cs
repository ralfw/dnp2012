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

                .AddAction<int>("ergebnis_anzeigen", ui.Ergebnis_anzeigen).MakeSync()
                .AddFunc<int, int>("ergebnis_zwischenspeichern", akku.Merken)
                .AddAction<FlowRuntimeException>("fehler_melden", ui.Fehler_melden).MakeSync()
                .AddFunc<Tuple<int, int, Operatoren>, int>("operanden_verknuepfen", Rechenwerk.Operanden_verknüpfen)
                .AddFunc<Rechenauftrag, int>("operation_speichern", operation.Einstellen)
                .AddPushCausality("pushc")
                .AddPopCausality("popc")
                .AddFunc<Tuple<int, int>, Tuple<int, int, Operatoren>>("vormalige_operation_laden", operation.Herausholen)
                .AddEventBasedComponent("akku", akku);

            using (var fr = new FlowRuntime(config))
            {
                fr.Message += Console.WriteLine;

                ui.Berechnen += fr.CreateEventProcessor<Rechenauftrag>(".berechnen");
                fr.UnhandledException += fr.CreateEventProcessor<FlowRuntimeException>(".error");

                Application.Run(ui);
            }
        }
    }
}
