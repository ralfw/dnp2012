using System;
using System.Collections.Generic;
using System.Linq;
using GeneratorContract;
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


        public static void GenerateException(string req, Action<string> results)
        {
            throw new ApplicationException("aaarghhh!");
        }


        public static Answer GenerateAll(Question question)
        {
            var parts = question.Text.Select(c => string.Format("'{0}'", c)).ToList();
            return new Answer {Parts = parts};
        }
    }
}