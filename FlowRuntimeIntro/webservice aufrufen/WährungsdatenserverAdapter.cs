using System.Globalization;
using npantarhei.runtime.contract;

namespace webservice_aufrufen
{
    // Währungsdaten-Webservice: http://currencies.apps.grandtrunk.net/

    [InstanceOperations]
    class WährungsdatenserverAdapter
    {
        readonly WebserviceAdapter _wsa = new WebserviceAdapter();


        public dynamic Konvertierungsrate_ermitteln(dynamic auftrag)
        {
            var ratentext = _wsa.Get(string.Format("http://currencies.apps.grandtrunk.net/getlatest/{0}/{1}", 
                                                   auftrag.Quellwährung, auftrag.Zielwährung));

            auftrag.Konvertierungsrate = double.Parse(ratentext, CultureInfo.CreateSpecificCulture("en-US"));
            
            return auftrag;
        }
    }
}