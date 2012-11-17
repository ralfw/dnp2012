using System;
using npantarhei.runtime.contract;

namespace GeneratorServer
{
    [StaticOperations]
    class Generator
    {
        public static void Generate(string req, Action<string> results)
        {
            foreach (var c in req)
            {
                results(string.Format("'{0}'", c));
            }
        }
    }
}