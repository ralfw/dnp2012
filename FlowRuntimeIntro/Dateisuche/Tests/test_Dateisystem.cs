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

            var results = new List<Batch<Tuple<string, string>>>();
            sut.Dateien_enummerieren(new Tuple<string, string>("x", @"..\.."), results.Add);

            Assert.AreEqual("x", results[0].Elements[0].Item1);
            Assert.LessOrEqual(13, results[0].Elements.Length);

            results[0].ForEach(r => Console.WriteLine(r.Item2));
        }
    }
}
