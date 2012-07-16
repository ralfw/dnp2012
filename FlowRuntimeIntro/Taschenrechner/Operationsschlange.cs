using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taschenrechner
{
    class Operationsschlange
    {
        private readonly Queue<Operatoren> _operatoren = new Queue<Operatoren>();
 
        public int Einstellen(Rechenauftrag rechenauftrag)
        {
            _operatoren.Enqueue(rechenauftrag.Operator);
            return rechenauftrag.Operand;
        }

        public Tuple<int,int, Operatoren> Herausholen(Tuple<int,int> operanden)
        {
            var op = _operatoren.Dequeue();
            return new Tuple<int, int, Operatoren>(operanden.Item1, operanden.Item2, op);
        }
    }

    enum Operatoren
    {
        Addition,
        Subtraktion,
        Multiplikation,
        Division,
        Gleichheitszeichen
    }

    class Rechenauftrag
    {
        public int Operand;
        public Operatoren Operator;
    }
}
