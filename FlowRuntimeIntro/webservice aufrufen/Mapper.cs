using npantarhei.runtime.contract;

namespace webservice_aufrufen
{
    [StaticOperations]
    class Mapper
    {
        public static string Formatieren(dynamic auftrag)
        {
            return string.Format("{0:0.00} {1} = {2:0.00} {3}", auftrag.Betrag, auftrag.Quellwährung.ToUpper(),
                                                                auftrag.UmgerechneterBetrag, auftrag.Zielwährung.ToUpper());
        }
    }
}