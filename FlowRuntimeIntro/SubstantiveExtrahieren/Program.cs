using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using npantarhei.runtime;
using npantarhei.runtime.config;
using npantarhei.runtime.contract;
using npantarhei.runtime.operations;

namespace SubstantiveExtrahieren
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new FlowRuntimeConfiguration()
                                .AddStreamsFrom("SubstantiveExtrahieren.root.flow", Assembly.GetExecutingAssembly())
                                .AddOperations(new AssemblyCrawler(Assembly.GetExecutingAssembly()));

            using(var fr = new FlowRuntime(config, new Schedule_for_sync_depthfirst_processing()))
            {
                fr.Process(".run", args[0]);
            }
        }
    }


    [StaticOperations]
    class Extrahierer
    {
        public static IEnumerable<string> Substantive_extrahieren(IEnumerable<string> zeilen)
        {
            return from z in zeilen
                    from w in z.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                        where w[0] == char.ToUpper(w[0])
                        select w;
        }
    }


    [InstanceOperations]
    class TextdateiAdapter
    {
        private string _dateiname;

        public IEnumerable<string> Textzeilen_lesen(string dateiname)
        {
            _dateiname = dateiname;
            return File.ReadAllLines(dateiname);
        } 

        public void Substantive_schreiben(IEnumerable<string> substantive)
        {
            var dateiname = _dateiname + ".substantive.txt";
            File.WriteAllLines(dateiname, substantive);
        }
    }
}
