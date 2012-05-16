namespace Radikale_Datenmodelle
{
    class Repository
    {
        public Befragung Befragung_anlegen()
        {
            var fb = new Fragebogen(
                new[] {
                          new Fragengruppe("Allgemeines",
                                           new[] {
                                                     new Frage("Welches Tier ist kein Säugetier?",
                                                               new[] {
                                                                         new Antwortoption("Katze", new Antwort("10")), 
                                                                         new Antwortoption("Hund", new Antwort("11")),
                                                                         new Antwortoption("Ameise", new Antwort("12")),
                                                                         new Antwortoption("Hamster", new Antwort("13")),
                                                                     },
                                                               new Antwort("12")
                                                         ),
                                                     new Frage("Welches Tier bellt?",
                                                               new[] {
                                                                         new Antwortoption("Katze", new Antwort("20")), 
                                                                         new Antwortoption("Hund", new Antwort("21")),
                                                                         new Antwortoption("Hamster", new Antwort("22")),
                                                                     },
                                                               new Antwort("21")
                                                         ),
                                                 }
                              ),
                          new Fragengruppe("Hunde", 
                                           new[] {
                                                     new Frage("Wieviele Zähne hat ein Hund?",
                                                               new[] {
                                                                         new Antwortoption("28", new Antwort("40")), 
                                                                         new Antwortoption("42", new Antwort("41")),
                                                                         new Antwortoption("36", new Antwort("42")),
                                                                         new Antwortoption("58", new Antwort("43")),
                                                                     },
                                                               new Antwort("41")
                                                         )
                                                 }
                              ) 
                      }
                );


            return new Befragung(fb);
        }
    }
}