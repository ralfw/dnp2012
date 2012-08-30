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
        public void Laden(Batch<Tuple<string, string>> dateipfade)
        {
            if (dateipfade.IsEmpty) return;

            var dateien = new Batcher<Tuple<string, FileInfo>>(dateipfade.Elements.Length/5);

            dateipfade.ForEach(t =>
                                   {
                                       var id = t.Item1;
                                       var dateipfad = t.Item2;

                                       var fi = new FileInfo(dateipfad);

                                       if (dateien.Add(new Tuple<string, FileInfo>(id, fi)) == BatchStatus.Full)
                                           Geladen(dateien.Grab());
                                   });

            Geladen(dateien.Grab());
        }

        public event Action<Batch<Tuple<string, FileInfo>>> Geladen;
    }
}
