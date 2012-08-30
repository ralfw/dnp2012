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
using npantarhei.runtime.contract;

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

            var gui = new DlgDateisuche();
            var suchmaschine = new Suchmaschine();
            var dateisystem = new Dateisystem();
            var datei = new Datei();

            var config = new FlowRuntimeConfiguration()
                            .AddStreamsFrom("Dateisuche.root.flow", Assembly.GetExecutingAssembly())

                            .AddEventBasedComponent("gui", gui)
                            .AddEventBasedComponent("dateisystem", dateisystem)
                            .AddEventBasedComponent("datei", datei)

                            .AddFunc<Suchanfrage, Tuple<string, string>>("Suchvorgang_starten", suchmaschine.Suchvorgang_starten)

                            .AddAction<Batch<Tuple<string, string>>, Statusmeldung, Batch<Tuple<string, string>>>("Pruefung_registrieren", suchmaschine.Prüfung_registrieren)
                            .AddFunc<Batch<Tuple<string, FileInfo>>, Batch<Tuple<string, FileInfo, string>>>("Abfrage_beimischen", suchmaschine.Abfrage_beimischen)
                            .AddAction<Batch<Tuple<string, FileInfo, string>>, Tuple<string, FileInfo>>("Filtern", suchmaschine.Filtern)
                            .AddAction<Tuple<string, FileInfo>, Statusmeldung, Dateifund>("Fund_registrieren", suchmaschine.Fund_registrieren);

            using (var fr = new FlowRuntime(config))
            {
                //fr.Throttle(200);

                fr.Message += Console.WriteLine;
                fr.UnhandledException += fr.CreateEventProcessor<FlowRuntimeException>(".error");

                Application.Run(gui);
            }
        }
    }
}
