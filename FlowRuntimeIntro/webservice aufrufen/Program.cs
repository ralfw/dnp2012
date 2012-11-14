using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using npantarhei.runtime;
using npantarhei.runtime.config;

namespace webservice_aufrufen
{
    class Program
    {
        static void Main(string[] args)
        {
            using(FlowRuntimeFactory.Basic)
            {
                //fr.Message += Console.WriteLine;

                UI.Instance.Run();
            }
        }
    }
}
