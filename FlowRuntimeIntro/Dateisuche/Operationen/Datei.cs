using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dateisuche.Operationen
{
    class Datei
    {
        public Tuple<string, FileInfo> Laden(Tuple<string, string> input)
        {
            var id = input.Item1;
            var dateipfad = input.Item2;

            var fi = new FileInfo(dateipfad);

            return new Tuple<string, FileInfo>(id, fi);
        }
    }
}
