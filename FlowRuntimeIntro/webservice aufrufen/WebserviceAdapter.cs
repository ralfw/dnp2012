using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace webservice_aufrufen
{
    class WebserviceAdapter
    {
        public string Get(string url)
        {
            var req = WebRequest.Create(url);
            req.Method = "GET";

            var resp = req.GetResponse();
            using (var sr = new StreamReader(resp.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }


    [TestFixture]
    public class test_WebserviceAdapter
    {
        [Test]
        public void Get()
        {
            var sut = new WebserviceAdapter();
            Assert.AreEqual("7.4401", sut.Get("http://currencies.apps.grandtrunk.net/getrate/2009-11-15/usd/zar"));
        }
    }
}
