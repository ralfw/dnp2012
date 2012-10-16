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

            var results = new List<Batch<string>>();
            var batcher = new Batcher<string>(100);

            sut.Dateien += _ => { };
            sut.Dateien_enummerieren("x", @"..\..", batcher);

            results.Add(batcher.Grab("x"));

            Assert.IsTrue(results.Count > 0);
            Assert.AreEqual("x", results[0].CorrelationId);
            Assert.LessOrEqual(13, results[0].Elements.Length);

            results[0].ForEach(Console.WriteLine);
        }
    }
}
