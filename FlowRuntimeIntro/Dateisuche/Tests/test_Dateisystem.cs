using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dateisuche.Operationen;
using NUnit.Framework;

namespace Dateisuche.Tests
{
    [TestFixture]
    public class test_Dateisystem
    {
        [Test]
        public void Enummerieren()
        {
            var sut = new Dateisystem("*.cs");

            var results = new List<Tuple<string, string>>();
            sut.Dateien_enummerieren(new Tuple<string, string>("x", @"..\.."), results.Add);

            Assert.AreEqual("x", results[0].Item1);
            Assert.LessOrEqual(13, results.Count);

            results.ForEach(r => Console.WriteLine(r.Item2));
        }
    }
}
