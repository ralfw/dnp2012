using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dateisuche
{
    static class Log
    {
        private static string _name;

        public static void Create(string name)
        {
            _name = name;
            File.WriteAllText(_name, DateTime.Now.ToString() + "\n");
        }

        public static void Write(string text)
        {
            lock (typeof(Log))
            {
                File.AppendAllText(_name, string.Format("{0}: {1}\n", DateTime.Now, text));
            }
        }
    }
}
