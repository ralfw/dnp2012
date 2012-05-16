using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;

namespace Radikale_Datenmodelle
{
    class Program
    {
        static void Main()
        {
            var befragungContainer = new Container<Befragung>();
            var befragungslogik = new Befragungslogik(befragungContainer);
            var feature1 = new Feature1_Programmstart(befragungslogik, befragungContainer);

            dynamic dFg = feature1.Process();
            Display(dFg);

            var feature4 = new Feature4_Frage_beantworten(befragungslogik);
            dFg = feature4.Process(new Antwort("10"));
            dFg = feature4.Process(new Antwort("20"));
            dFg = feature4.Process(new Antwort("11")); // ersetzt Antwort("10")
            Display(dFg);
        }

        private static void Display(dynamic dFg)
        {
            Console.WriteLine("{0}", dFg.Text);
            foreach (dynamic dF in dFg.Fragen)
            {
                Console.WriteLine("  {0}", dF.Text);
                foreach (dynamic dAo in dF.Antwortoptionen)
                    Console.WriteLine("    ({0}) {1} ({2})", dAo.AlsAntwortGewählt ? "X" : " ", dAo.Text, dAo.Antwort.Id);
            }
        }


        static void Main2(string[] args)
        {
            var ab = new Antwortbogen();
            ab.Antwort_registrieren(new Antwort("10"));
            ab.Antwort_registrieren(new Antwort("11"));
            ab.Antwort_registrieren(new Antwort("10"));

            ab.Antworten.ToList()
              .ForEach(a => Console.WriteLine(a.Id));


            Console.WriteLine(ab.Antworten.Count());

            var fb = new Fragebogen(
                new[] {
                        new Fragengruppe("Allgemeines",
                                         new[] {
                                                 new Frage("Welches Tier ist kein Säugetier?",
                                                           new[] {
                                                                new Antwortoption("Katze", new Antwort("10")), 
                                                                new Antwortoption("Hund", new Antwort("11")),
                                                                new Antwortoption("Ameise", new Antwort("12")),
                                                                new Antwortoption("Hamster", new Antwort("13")),
                                                           },
                                                           new Antwort("12")
                                                     ),
                                                  new Frage("Welches Tier bellt?",
                                                           new[] {
                                                                new Antwortoption("Katze", new Antwort("20")), 
                                                                new Antwortoption("Hund", new Antwort("21")),
                                                                new Antwortoption("Hamster", new Antwort("22")),
                                                           },
                                                           new Antwort("21")
                                                     ),
                                             }
                            ),
                        new Fragengruppe("Hunde", 
                                         new[] {
                                                 new Frage("Wieviele Zähne hat ein Hund?",
                                                           new[] {
                                                                new Antwortoption("28", new Antwort("40")), 
                                                                new Antwortoption("42", new Antwort("41")),
                                                                new Antwortoption("36", new Antwort("42")),
                                                                new Antwortoption("58", new Antwort("43")),
                                                           },
                                                           new Antwort("41")
                                                     )
                                             }
                            ) 
                    }
                );


            var b = new Befragung(fb);
            b.Antwort_registrieren(new Antwort("10"));
            //b.Antwort_registrieren(new Antwort("x")); // Fehler!
        }
    }

    class AntwortbogenX
    {
        
    }

    class FragebogenX
    {
        private List<Fragengruppe> _fragengruppen = new List<Fragengruppe>();

        public dynamic FragengruppeSelektieren(int index)
        {
            var fg = _fragengruppen[index];

            dynamic fragengruppe = new ExpandoObject();
            fragengruppe.Titel = fg.Titel;
            fragengruppe.Fragen = new List<dynamic>();

            dynamic frage = new ExpandoObject();
            frage.Fragentext = "Wieviele Beine hat eine Katze?";
            frage.Antwortoptionen = new List<dynamic>();
            dynamic option = new ExpandoObject();
            option.Id = "x";
            option.Gewählt = true;
            option.Text = "4";
            frage.Antwortoptionen.Add(option);

            option.Id = "y";
            option.Gewählt = false;
            option.Text = "3";
            frage.Antwortoptionen.Add(option);

            fragengruppe.Fragen.Add(frage);

            return fragengruppe;
        }

        /*
         * Fragebogendatei:
         *      Titel ist der Dateiname ohne Extension
         *      Dateiaufbau: Leerzeilen irrelevant
         *          Fragengruppentitel
         *          ?Fragentext
         *          -Option
         *          -*Option    // richtige Antwort
         *          ...
         *          -Option
         *          ?Fragentext
         *          -*Option
         *          ...
         *          -Option
         *          Fragengruppentitel
         *          ...
         */
        public void Parse(Stream source)
        {
            using(var r = new StreamReader(source))
            {
                while(!r.EndOfStream)
                {
                    var line = r.ReadLine().Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    switch (line[0])
                    {
                        case '?': // neue Frage
                            _fragengruppen.Last().Fragen.Add(new Frage { Text = line.Substring(1) });
                            break;
                        case '-': // neue Antwortoption
                            var o = new Antwortoption {Text = line.Substring(1)};
                            o.IstAntwort = o.Text.StartsWith("*");
                            if (o.IstAntwort) o.Text = o.Text.Substring(1);
                            break;
                        default:  // neue Fragengruppe
                            _fragengruppen.Add(new Fragengruppe { Titel = line });
                            break;
                    }

                }
            }
        }
   
        public class Fragengruppe
        {
            public string Titel;
            public List<Frage> Fragen = new List<Frage>();
        }

        public class Frage
        {
            public string Text;
            public List<Antwortoption> Antwortoptionen = new List<Antwortoption>();
        }

        public class Antwortoption
        {
            public string Text;
            public bool IstAntwort;
        }
    }

    class BefragungX
    {
        private Fragebogen _fragebogen;
        private Antwortbogen _antwortbogen;

        public BefragungX(Fragebogen fragebogen)
        {
            _fragebogen = fragebogen;
            _antwortbogen = new Antwortbogen();
        }

        public dynamic Fragengruppe(int index)
        {
            throw new NotImplementedException();
        }
    }
}
