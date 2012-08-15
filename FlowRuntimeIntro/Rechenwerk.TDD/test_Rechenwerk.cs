using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Rechenwerk.TDD
{
    /* Akzeptanzszenario:
     * Input            Output
     * (2,+)            2
     * (3,*)            5
     * (4,=)            20
     * (20,+)           20
     * (22,=)           42
     */
    [TestFixture]
    public class test_Rechenwerk
    {
        [Test]
        public void Erster_Rechenauftrag_liefert_Operanden()
        {
            var sut = new Rechenwerk();
            Assert.AreEqual(2, sut.Berechne(2, "+"));
        }

        [Test]
        public void Zweiter_Rechenauftrag_verknüpft_vorherigen_Operanden_mit_aktuellem_per_vorheriger_Operation()
        {
            var sut = new Rechenwerk();
            sut.Berechne(2, "+");
            Assert.AreEqual(5, sut.Berechne(3, "*"));
        }

        [Test]
        public void Dritter_Auftrag_wird_mit_Zwischenergebnis_und_Operation_vom_zweiten_verknüpft()
        {
            var sut = new Rechenwerk();
            sut.Berechne(2, "+");
            sut.Berechne(3, "*");
            Assert.AreEqual(20, sut.Berechne(4, "="));
        }

        [Test]
        public void Nach_dem_Gleichheitszeichen_geht_es_wieder_von_vorne_los()
        {
            var sut = new Rechenwerk();
            sut.Berechne(2, "+");
            sut.Berechne(3, "*");
            sut.Berechne(4, "=");
            Assert.AreEqual(20, sut.Berechne(20, "+"));
        }
    }
}
