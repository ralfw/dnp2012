using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radikale_Datenmodelle
{
    class Befragung
    {
        public void Antworten_löschen(IEnumerable<Antwort> geschwisterAntworten)
        {
            geschwisterAntworten.ToList()
                .ForEach(a => _antwortbogen.Antwort_löschen(a));
        }

        public bool Antwort_gegeben(Antwort antwort)
        {
            return _antwortbogen.Antworten.Contains(antwort);
        }


        public Fragebogen Fragebogen { get; private set; }
        private Antwortbogen _antwortbogen;

        public Befragung(Fragebogen fragebogen)
        {
            Fragebogen = fragebogen;
            _antwortbogen = new Antwortbogen();
        }

        public void Antwort_registrieren(Antwort antwort)
        {
            Prüfen_ob_Antwort_zum_Fragebogen_gehört(antwort);
            _antwortbogen.Antwort_registrieren(antwort);
        }

        private void Prüfen_ob_Antwort_zum_Fragebogen_gehört(Antwort antwort)
        {
            var antwortGefundenInFragebogen =
                from fg in Fragebogen.Fragengruppen
                    from f in fg.Fragen
                        from ao in f.Antwortoptionen
                        where ao.Antwort.Equals(antwort)
                        select ao.Antwort;

            if (!antwortGefundenInFragebogen.Any())
                throw new InvalidOperationException("Antwort gehört nicht zum Fragebogen!");
        }
    }
}
