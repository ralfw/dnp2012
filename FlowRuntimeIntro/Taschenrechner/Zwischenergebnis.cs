using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taschenrechner
{
    class Zwischenergebnis
    {
        private int? _zwischenergebnis;

        public void Laden(int operand)
        {
            if (_zwischenergebnis == null)
                Kein_Zwischenergebnis(operand);
            else
                Operanden(new Tuple<int, int>(_zwischenergebnis.Value, operand));
        }

        public event Action<int> Kein_Zwischenergebnis;
        public event Action<Tuple<int, int>> Operanden;


        public int Merken(int zwischenergebnis)
        {
            _zwischenergebnis = zwischenergebnis;
            return zwischenergebnis;
        }
    }
}
