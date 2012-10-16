using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Dateisuche.Daten;
using npantarhei.runtime.contract;

namespace Dateisuche.Operationen
{
    [InstanceOperations]
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


        public void Pruefung_registrieren(Batch<string> batch, Action<Statusmeldung> melden, Action<Batch<string>> weitermachen)
        {
            if (!batch.IsEmpty)
            {
                var suchvorgang = _suchvorgänge[batch.CorrelationId];
                suchvorgang.DateienGeprüft += batch.Elements.Length;

                var status = new Statusmeldung
                                 {
                                     SuchauftragId = batch.CorrelationId,
                                     DateienGefunden = suchvorgang.DateienGefunden,
                                     DateienGeprüft = suchvorgang.DateienGeprüft,
                                     Abfrage = suchvorgang.Abfrage,
                                     InBearbeitung = suchvorgang.InBearbeitung,
                                     Verzeichnispfad = Path.GetDirectoryName(batch.Elements[0])
                                 };
                melden(status);
            }

            weitermachen(batch);
        }


        public Batch<Tuple<FileInfo, string>> Abfrage_beimischen(Batch<FileInfo> dateien)
        {
            var filteraufträge = new Batcher<Tuple<FileInfo, string>>(dateien.Elements.Length);
            dateien.ForEach(t =>
                                {
                                    var suchvorgang = _suchvorgänge[dateien.CorrelationId];
                                    var filterauftrag = new Tuple<FileInfo, string>(t, suchvorgang.Abfrage);
                                    filteraufträge.Add(filterauftrag);
                                });
            if (dateien.GetType() == typeof(EndOfStreamBatch<FileInfo>))
                return filteraufträge.GrabAsEndOfStream(dateien.CorrelationId);
            return filteraufträge.Grab(dateien.CorrelationId);
        } 


        public void Filtern(Batch<Tuple<FileInfo, string>> aufträge, Action<Tuple<string,FileInfo>> gefunden)
        {
            aufträge.ForEach(t =>
                                    {
                                        var datei = t.Item1;
                                        var abfrage = t.Item2;

                                        if (datei.Name.IndexOf(abfrage) >= 0)
                                            gefunden(new Tuple<string, FileInfo>(aufträge.CorrelationId, datei));
                                    });

            if (aufträge.GetType() == typeof(EndOfStreamBatch<Tuple<FileInfo,string>>))
                gefunden(new Tuple<string, FileInfo>(aufträge.CorrelationId, null));
        } 


        public void Ende_des_Suchvorgangs_melden(Tuple<string, FileInfo> input, Action<Statusmeldung> endeMelden, Action<Tuple<string,FileInfo>> gefunden)
        {
            if (input.Item2 == null)
            {
                var suchvorgang = _suchvorgänge[input.Item1];
                suchvorgang.InBearbeitung = false;

                var status = new Statusmeldung
                                    {
                                        SuchauftragId = input.Item1,
                                        DateienGefunden = suchvorgang.DateienGefunden,
                                        DateienGeprüft = suchvorgang.DateienGeprüft,
                                        Abfrage = suchvorgang.Abfrage,
                                        InBearbeitung = suchvorgang.InBearbeitung,
                                        Verzeichnispfad = ""
                                    };
                endeMelden(status);
            }
            else
                gefunden(input);
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
