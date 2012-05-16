using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Radikale_Datenmodelle
{
    class Antwortbogen
    {
        private readonly HashSet<Antwort> _antworten = new HashSet<Antwort>();
        public IEnumerable<Antwort> Antworten { get { return _antworten; } }

        public void Antwort_registrieren(Antwort antwort)
        {
            _antworten.Add(antwort);
        }

        public void Antwort_löschen(Antwort antwort)
        {
            _antworten.Remove(antwort);
        }
    }
}
