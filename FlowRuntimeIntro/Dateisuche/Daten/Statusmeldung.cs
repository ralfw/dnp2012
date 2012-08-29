namespace Dateisuche.Daten
{
    public class Statusmeldung
    {
        public string SuchauftragId;
        public string Abfrage;
        public string Verzeichnispfad;
        public int DateienGefunden;
        public int DateienGeprüft;
        public bool InBearbeitung;

        public override string ToString()
        {
            return string.Format("Statusmeldung(Id:{0})", SuchauftragId);
        }
    }
}