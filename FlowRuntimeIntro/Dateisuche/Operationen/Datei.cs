using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using npantarhei.runtime.contract;

namespace Dateisuche.Operationen
{
    [EventBasedComponent]
    class Datei
    {
        public void Laden(Batch<string> dateipfade)
        {
            var dateien = new Batcher<FileInfo>(dateipfade.Elements.Length/5);

            dateipfade.ForEach(t => {
                                        var fi = new FileInfo(t);

                                        if (dateien.Add(fi) == BatchStatus.Full)
                                            Geladen(dateien.Grab(dateipfade.CorrelationId));
                                    });

            if (dateipfade.GetType() == typeof(EndOfStreamBatch<string>))
                Geladen(dateien.GrabAsEndOfStream(dateipfade.CorrelationId));
            else
                Geladen(dateien.Grab(dateipfade.CorrelationId));
        }

        public event Action<Batch<FileInfo>> Geladen;
    }
}
