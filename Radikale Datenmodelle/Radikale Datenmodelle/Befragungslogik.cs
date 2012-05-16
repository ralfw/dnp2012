using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Radikale_Datenmodelle
{
    class Befragungslogik
    {
        public void Antwort_registrieren(Antwort antwort)
        {
            var geschwisterAntworten = Geschwisterantworten_bestimmen(antwort);
            _befragungContainer.Value.Antworten_löschen(geschwisterAntworten);
            _befragungContainer.Value.Antwort_registrieren(antwort);
        }

        private IEnumerable<Antwort> Geschwisterantworten_bestimmen(Antwort antwort)
        {
            var frageZurAntwort = (from fg in _befragungContainer.Value.Fragebogen.Fragengruppen
                                   from f in fg.Fragen
                                   from ao in f.Antwortoptionen
                                   where ao.Antwort.Equals(antwort)
                                   select f).First();
            return from ao in frageZurAntwort.Antwortoptionen
                   select ao.Antwort;
        }


        private readonly Container<Befragung> _befragungContainer;

        public Befragungslogik(Container<Befragung> befragungContainer)
        {
            _befragungContainer = befragungContainer;
        }


        public dynamic Fragengruppe_laden()
        {
            var fg = _befragungContainer.Value.Fragebogen.Fragengruppen.First();

            dynamic dFg = new ExpandoObject();
            dFg.Text = fg.Text;
            dFg.Fragen = new List<dynamic>();
            foreach(var f in fg.Fragen)
            {
                dynamic dF = new ExpandoObject();
                dFg.Fragen.Add(dF);

                dF.Text = f.Text;
                dF.Antwortoptionen = new List<dynamic>();
                foreach(var ao in f.Antwortoptionen)
                {
                    dynamic dAo = new ExpandoObject();
                    dF.Antwortoptionen.Add(dAo);

                    dAo.Text = ao.Text;
                    dAo.Antwort = ao.Antwort;
                    dAo.AlsAntwortGewählt = _befragungContainer.Value.Antwort_gegeben(ao.Antwort);
                }
            }
            
            return dFg;
        }
    }
}