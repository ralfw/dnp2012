using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using npantarhei.runtime.contract;

namespace Dateisuche.Operationen
{
    class Datei
    {
        [ParallelMethod]
        public void Laden(Tuple<string, string> input)
        {
            var id = input.Item1;
            var dateipfad = input.Item2;

            var fi = new FileInfo(dateipfad);

            Geladen(new Tuple<string, FileInfo>(id, fi));
        }

        public event Action<Tuple<string, FileInfo>> Geladen;
    }
}
