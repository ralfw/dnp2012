using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Dateisuche.Daten;
using npantarhei.runtime;

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

            var config = new FlowRuntimeConfiguration()
                            .AddStreamsFrom("Dateisuche.root.flow", Assembly.GetExecutingAssembly())

                            .AddEventBasedComponent("gui", gui)
                            .AddAction<Suchanfrage, Statusmeldung, Dateifund>("Dateisuche", Dateisuche)
                                .MakeParallel();

            using (var fr = new FlowRuntime(config))
            {
                fr.UnhandledException += Console.WriteLine;

                Application.Run(gui);
            }
        }


        static void Dateisuche(Suchanfrage suchanfrage, Action<Statusmeldung> datei_geprüft, Action<Dateifund> datei_gefunden)
        {
            var abfrageteile = suchanfrage.Abfrage.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var zuFinden = int.Parse(abfrageteile[0]);
            var zuPrüfen = int.Parse(abfrageteile[1]);
            var verzögerung = int.Parse(abfrageteile[2]);

            var id = Guid.NewGuid().ToString();

            var gefunden = 0;
            for (var p = 1; p <= zuPrüfen; p++)
            {
                if (p % zuFinden == 0) gefunden++;
                datei_geprüft(new Statusmeldung
                                  {
                                      SuchauftragId = id,
                                      Abfrage = suchanfrage.Abfrage, 
                                      DateienGefunden = gefunden, 
                                      DateienGeprüft = p, 
                                      InBearbeitung = true, 
                                      Verzeichnispfad = suchanfrage.Wurzelpfad
                                  });
                if (p % zuFinden == 0)
                    datei_gefunden(new Dateifund
                                       {
                                           SuchauftragId = id,
                                           Dateiname = "file" + gefunden, 
                                           Dateipfad = suchanfrage.Wurzelpfad + @"\" + p.ToString(), 
                                           Veränderungsdatum = DateTime.Now
                                       });
                
                Thread.Sleep(verzögerung);
            }
        }
    }
}
