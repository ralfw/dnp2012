using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dateisuche.Operationen
{
    class Dateisystem
    {
        private readonly string _dateinamenschablone;

        public Dateisystem() : this("*") {}
        public Dateisystem(string dateinamenschablone)
        {
            _dateinamenschablone = dateinamenschablone;
        }


        public void Dateien_enummerieren(Tuple<string,string> input, Action<Tuple<string,string>> fürJedeDatei)
        {
            var id = input.Item1;
            var wurzelpfad = input.Item2;

            var dateipfade = Directory.GetFiles(wurzelpfad, _dateinamenschablone, SearchOption.AllDirectories);

            dateipfade.ToList().ForEach(dpf => fürJedeDatei(new Tuple<string, string>(id, dpf)));
        } 
    }
}
