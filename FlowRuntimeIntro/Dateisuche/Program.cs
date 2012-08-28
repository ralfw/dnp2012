﻿using System;
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
                            .AddEventBasedComponent("suchmaschine", suchmaschine)

                            .AddFunc<Suchanfrage, Tuple<string, string>>("Suchvorgang_starten", suchmaschine.Suchvorgang_starten)
                            .AddAction<Tuple<string,string>>("Dateien_enummerieren", dateisystem.Dateien_enummerieren)
                                //.MakeAsync("Enummerieren2")
                            .AddAction<Tuple<string, string>>("Laden", datei.Laden)
                                //.MakeAsync("Laden2")
                            .AddFunc<Tuple<string, FileInfo>, Tuple<string, FileInfo, string>>("Abfrage_beimischen", suchmaschine.Abfrage_beimischen)
                            .AddAction<Tuple<string, FileInfo, string>, Tuple<string, FileInfo>>("Filtern", suchmaschine.Filtern)
                               ;//.MakeAsync("Filtern");

            using (var fr = new FlowRuntime(config))
            {
                Log.Create("log.txt");
                fr.Message += _ => Log.Write(_.ToString());

                fr.UnhandledException += fr.CreateEventProcessor<FlowRuntimeException>(".error");

                Application.Run(gui);
            }
        }
    }
}
