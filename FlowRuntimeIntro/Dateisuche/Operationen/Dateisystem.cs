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

        public Dateisystem() : this("*.*") {}
        public Dateisystem(string dateinamenschablone)
        {
            _dateinamenschablone = dateinamenschablone;
        }


        [ParallelMethod]
        public void Enummerieren(Tuple<string, string> input)
        {
            const int BATCH_SIZE = 5000;
            var batcher = new Batcher<string>(BATCH_SIZE);

            Dateien_enummerieren(input.Item1, input.Item2, batcher);

            Dateien(batcher.GrabAsEndOfStream(input.Item1));
        }


        internal void Dateien_enummerieren(string id, string pfad, Batcher<string> batcher)
        {
            var dir = new DirectoryInfo(pfad);

            try
            {
                foreach (var file in dir.GetFiles(_dateinamenschablone))
                    if (batcher.Add(file.FullName) == BatchStatus.Full)
                        Dateien(batcher.Grab(id));

                foreach (var subdir in dir.GetDirectories())
                    Dateien_enummerieren(id, subdir.FullName, batcher);
            }
            catch(UnauthorizedAccessException ex) {}
        }


        public event Action<Batch<string>> Dateien;
    }
}
