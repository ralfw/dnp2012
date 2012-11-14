using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using npantarhei.runtime.contract;

namespace webservice_aufrufen
{
    [StaticOperations]
    class Konvertierer
    {
        public static dynamic Angaben_parsen(string angaben)
        {
            var match = Regex.Match(angaben, @"(?<amount>\d+(,\d+)?)\s*(?<fromCode>\w+),\s*(?<toCode>\w+)");

            if (match.Success)
            {
                dynamic auftrag = new ExpandoObject();
                auftrag.Betrag = double.Parse(match.Groups["amount"].Value);
                auftrag.Quellwährung = match.Groups["fromCode"].Value;
                auftrag.Zielwährung = match.Groups["toCode"].Value;

                return auftrag;
            }
            
            throw new ArgumentException("Ungültiger Konvertierungsauftrag! Erwartete Form: Betrag Quellwährung, Zielwährung");
        }


        public static dynamic Umrechnen(dynamic auftrag)
        {
            auftrag.UmgerechneterBetrag = auftrag.Betrag * auftrag.Konvertierungsrate;
            return auftrag;
        }
    }
}
