using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dateisuche.Daten;

namespace Dateisuche.Operationen
{
    class Suchmaschine
    {
        private class Suchvorgang
        {
            public string Abfrage;
            public int DateienGefunden;
            public int DateienGeprüft;
            public bool InBearbeitung;
        }

        private readonly Dictionary<string, Suchvorgang> _suchvorgänge = new Dictionary<string,Suchvorgang>();


        public Tuple<string,string> Suchvorgang_starten(Suchanfrage suchanfrage)
        {
            var id = Guid.NewGuid().ToString();

            _suchvorgänge.Add(id, new Suchvorgang
                                      {
                                          Abfrage = suchanfrage.Abfrage,
                                          DateienGefunden =  0,
                                          DateienGeprüft = 0,
                                          InBearbeitung = true
                                      });

            return new Tuple<string, string>(id, suchanfrage.Wurzelpfad);
        }


        public void Prüfung_registrieren(Tuple<string, string> input, Action<Statusmeldung> melden, Action<Tuple<string, string>> weitermachen)
        {
            var suchvorgang = _suchvorgänge[input.Item1];
            suchvorgang.DateienGeprüft++;

            var status = new Statusmeldung
                             {
                                 SuchauftragId = input.Item1,
                                 DateienGefunden =  suchvorgang.DateienGefunden,
                                 DateienGeprüft = suchvorgang.DateienGeprüft,
                                 Abfrage = suchvorgang.Abfrage,
                                 InBearbeitung = suchvorgang.InBearbeitung,
                                 Verzeichnispfad = Path.GetDirectoryName(input.Item2)
                             };
            melden(status);

            weitermachen(input);
        }


        public Tuple<string, FileInfo, string> Abfrage_beimischen(Tuple<string,FileInfo> input)
        {
            var suchvorgang = _suchvorgänge[input.Item1];

            return new Tuple<string, FileInfo, string>(input.Item1, input.Item2, suchvorgang.Abfrage);
        } 


        public void Filtern(Tuple<string, FileInfo, string> input, Action<Tuple<string, FileInfo>> gefunden)
        {
            var datei = input.Item2;
            var abfrage = input.Item3;

            if (datei.Name.IndexOf(abfrage) >= 0)
                gefunden(new Tuple<string, FileInfo>(input.Item1, datei));
        } 


        public void Fund_registrieren(Tuple<string, FileInfo> input, Action<Statusmeldung> melden, Action<Dateifund> gefunden)
        {
            var suchvorgang = _suchvorgänge[input.Item1];
            suchvorgang.DateienGefunden++;

            var status = new Statusmeldung
            {
                SuchauftragId = input.Item1,
                DateienGefunden = suchvorgang.DateienGefunden,
                DateienGeprüft = suchvorgang.DateienGeprüft,
                Abfrage = suchvorgang.Abfrage,
                InBearbeitung = suchvorgang.InBearbeitung,
                Verzeichnispfad = input.Item2.DirectoryName
            };
            melden(status);

            var datei = input.Item2;
            var fund = new Dateifund
                           {
                               SuchauftragId = input.Item1,
                               Dateiname = datei.Name,
                               Veränderungsdatum = datei.LastWriteTime,
                               Dateipfad = datei.DirectoryName
                           };
            gefunden(fund);
        }
    }


}
