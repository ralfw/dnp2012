namespace Radikale_Datenmodelle
{
    class Feature4_Frage_beantworten
    {
        private readonly Befragungslogik _befragungslogik;

        public Feature4_Frage_beantworten(Befragungslogik befragungslogik)
        {
            _befragungslogik = befragungslogik;
        }

        public dynamic Process(Antwort antwort)
        {
            _befragungslogik.Antwort_registrieren(antwort);
            return _befragungslogik.Fragengruppe_laden();
        }
    }
}