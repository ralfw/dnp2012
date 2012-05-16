using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radikale_Datenmodelle
{
    class Fragebogen
    {
        public IEnumerable<Fragengruppe> Fragengruppen { get; set; }

        public Fragebogen(IEnumerable<Fragengruppe> fragengruppen)
        {
            Fragengruppen = fragengruppen;
        }
    }

    class Fragengruppe
    {
        public string Text { get; set; }
        public IEnumerable<Frage> Fragen { get; set; }

        public Fragengruppe(string text, IEnumerable<Frage> fragen)
        {
            Text = text;
            Fragen = fragen;
        }
    }

    class Frage
    {
        public string Text { get; private set; }
        public IEnumerable<Antwortoption> Antwortoptionen { get; private set; }
        public Antwort RichtigeAntwort { get; set; }

        public Frage(string text, IEnumerable<Antwortoption> antwortoptionen, Antwort richtigeAntwort)
        {
            Text = text;
            Antwortoptionen = antwortoptionen;
            RichtigeAntwort = richtigeAntwort;

            Prüfen_ob_alle_Antwortoptionen_verschieden();
            Prüfen_ob_richtige_Antwort_in_Antwortoptionen_enthalten();
        }

        private void Prüfen_ob_alle_Antwortoptionen_verschieden()
        {
            if (Antwortoptionen.Select(ao => ao.Antwort).Distinct().Count() != Antwortoptionen.Count())
                throw new InvalidOperationException("Die Antworten sind nicht alle verschieden!");
        }

        private void Prüfen_ob_richtige_Antwort_in_Antwortoptionen_enthalten()
        {
            if (Antwortoptionen.FirstOrDefault(ao => ao.Antwort.Equals(RichtigeAntwort)) == null)
                throw new InvalidOperationException("Die richtige Antwort ist nicht unter den Antwortoptionen!");
        }
    }

    class Antwortoption
    {
        public string Text { get; private set; }
        public Antwort Antwort { get; private set; }

        public Antwortoption(string text, Antwort antwort)
        {
            Text = text;
            Antwort = antwort;
        }
    }

    struct Antwort
    {
        public readonly string Id;

        public Antwort(string id) { Id = id; }
    }
}
