using System;
using System.Text;

namespace Radikale_Datenmodelle
{
    class Feature1_Programmstart
    {
        private readonly Befragungslogik _befragungslogik;
        private readonly Container<Befragung> _befragungContainer;

        public Feature1_Programmstart(Befragungslogik befragungslogik, Container<Befragung> befragungContainer)
        {
            _befragungslogik = befragungslogik;
            _befragungContainer = befragungContainer;
        }

        public dynamic Process()
        {
            var r = new Repository();

            var befragung = r.Befragung_anlegen();
            _befragungContainer.Value = befragung;
            return _befragungslogik.Fragengruppe_laden();
        }
    }
}
