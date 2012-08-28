using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using npantarhei.runtime.contract;

namespace Dateisuche.Operationen
{
    public class Dateisystem
    {
        private readonly string _dateinamenschablone;

        public Dateisystem() : this("*") {}
        public Dateisystem(string dateinamenschablone)
        {
            _dateinamenschablone = dateinamenschablone;
        }


        //[AsyncMethod("Enummerieren")]
        public void Dateien_enummerieren(Tuple<string,string> input)
        {
            var id = input.Item1;
            var wurzelpfad = input.Item2;

            var dateipfade = Directory.GetFiles(wurzelpfad, _dateinamenschablone, SearchOption.AllDirectories);

            dateipfade.ToList().ForEach(dpf =>
                                            {
                                                Log.Write(dpf);
                                                Datei(new Tuple<string, string>(id, dpf));
                                            });
        }


        public event Action<Tuple<string, string>> Datei;
    }
}
