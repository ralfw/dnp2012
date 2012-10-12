using System;
using System.IO;
using System.Linq;
using System.Text;
using npantarhei.runtime.contract;

namespace Dateisuche.Operationen
{
    [EventBasedComponent]
    class Dateisystem
    {
        private readonly string _dateinamenschablone;

        public Dateisystem() : this("*") {}
        public Dateisystem(string dateinamenschablone)
        {
            _dateinamenschablone = dateinamenschablone;
        }


        public void Dateien_enummerieren(Tuple<string,string> input, Action<Batch<Tuple<string, string>>> fürJedenDateistapel)
        {
            var id = input.Item1;
            var wurzelpfad = input.Item2;

            var dateipfade = Directory.GetFiles(wurzelpfad, _dateinamenschablone, SearchOption.AllDirectories);

            const int BATCH_SIZE = 50;
            var batcher = new Batcher<Tuple<string, string>>(BATCH_SIZE);
            foreach (var dpf in dateipfade)
            {
                if (batcher.Add(new Tuple<string, string>(id, dpf)) == BatchStatus.Full)
                    fürJedenDateistapel(batcher.Grab());
            }
            fürJedenDateistapel(batcher.Grab());
        }


        [ParallelMethod]
        public void Enummerieren(Tuple<string,string> input)
        {
            Dateien_enummerieren(input, Dateien);
        }

        public event Action<Batch<Tuple<string, string>>> Dateien;
    }
}
