using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using npantarhei.runtime.contract;

namespace webservice_aufrufen
{
    [EventBasedComponent]
    class UI
    {
        public static UI Instance;
        public UI() { Instance = this; }


        public void Run()
        {
            Console.WriteLine("[Währungsumrechnung]\n");

            while(true)
            {
                Console.Write("Konvertiere (z.B. 42 EUR): "); var from = Console.ReadLine();
                if (from == "") return;
                Console.Write("  nach (z.B. USD): "); var to = Console.ReadLine();

                Konvertierungsangaben(from + ", " + to);
            }
        }


        public void Zeige_Konvertierung(string konvertierung)
        {
            Console.WriteLine("\n  Konvertierung: {0}\n", konvertierung);
        }


        public event Action<string> Konvertierungsangaben;
    }
}
