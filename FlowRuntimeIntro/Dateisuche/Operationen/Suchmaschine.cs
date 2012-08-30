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


        public void Prüfung_registrieren(Batch<Tuple<string, string>> batch, Action<Statusmeldung> melden, Action<Batch<Tuple<string, string>>> weitermachen)
        {
            if (batch.IsEmpty) return;

            var suchvorgang = _suchvorgänge[batch.Elements[0].Item1];
            suchvorgang.DateienGeprüft += batch.Elements.Length;

            var status = new Statusmeldung
                            {
                                SuchauftragId = batch.Elements[0].Item1,
                                DateienGefunden = suchvorgang.DateienGefunden,
                                DateienGeprüft = suchvorgang.DateienGeprüft,
                                Abfrage = suchvorgang.Abfrage,
                                InBearbeitung = suchvorgang.InBearbeitung,
                                Verzeichnispfad = Path.GetDirectoryName(batch.Elements[0].Item2)
                            };
            melden(status);

            weitermachen(batch);
        }


        public Batch<Tuple<string, FileInfo, string>> Abfrage_beimischen(Batch<Tuple<string,FileInfo>> dateien)
        {
            var filteraufträge = new Batcher<Tuple<string, FileInfo, string>>(dateien.Elements.Length);
            dateien.ForEach(t =>
                                {
                                    var suchvorgang = _suchvorgänge[t.Item1];
                                    var filterauftrag = new Tuple<string, FileInfo, string>(t.Item1, t.Item2, suchvorgang.Abfrage);
                                    filteraufträge.Add(filterauftrag);
                                });
            return filteraufträge.Grab();
        } 


        public void Filtern(Batch<Tuple<string, FileInfo, string>> aufträge, Action<Tuple<string, FileInfo>> gefunden)
        {
            aufträge.ForEach(t =>
                                 {
                                     var datei = t.Item2;
                                     var abfrage = t.Item3;

                                     if (datei.Name.IndexOf(abfrage) >= 0)
                                         gefunden(new Tuple<string, FileInfo>(t.Item1, datei));
                                 });
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
