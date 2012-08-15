using System;
using System.Collections.Generic;

namespace Rechenwerk.FD
{
    public class Rechenwerk
    {
        class Operationenbuffer
        {
            private readonly Queue<string> _operationen = new Queue<string>();
 
            public void Merken(string op) { _operationen.Enqueue(op); }
            public string Laden() { return _operationen.Dequeue(); }
        }


        class Zwischenergebnis
        {
            private int? _wert;

            public int Merken(int ergebnis)
            {
                _wert = ergebnis;
                return _wert.Value;
            }

            public void Laden(int operand, Action<int, int> es_gab_ein_Zwischenergebnis, Action<int> noch_kein_zwischenergebnis)
            {
                if (_wert == null)
                    noch_kein_zwischenergebnis(operand);
                else
                    es_gab_ein_Zwischenergebnis(_wert.Value, operand);
            }
        }


        private readonly Operationenbuffer _operationen = new Operationenbuffer();
        readonly Zwischenergebnis _akku = new Zwischenergebnis();


        public int Berechne(int operand, string operation)
        {
            var ergebnis = 0;

            _operationen.Merken(operation);
            _akku.Laden(operand,
                        (opLeft, opRight) => { var op = _operationen.Laden();
                                               ergebnis = Verknüpfen(opLeft, opRight, op);
                        },
                        opSingle =>            ergebnis = opSingle
            );

            return _akku.Merken(ergebnis);
        }


        private int Verknüpfen(int opLeft, int opRight, string op)
        {
            switch (op)
            {
                case "+":
                    return opLeft + opRight;
                case "*":
                    return opLeft * opRight;
                case "=":
                    return opRight;
                default:
                    throw new NotImplementedException("Unbekannter Operator: " + op);
            }
        }
    }


    public class Rechenwerk1
    {
        /*
         * Berechne {
         *  . -> Operation_merken -> Zwischenergebnis.operanden -> Gemerkten_Operation_laden -> Verknüpfen -> Zwischenergebnis_merken -> .,
         *  Zwischenergebnis.aktuelle_operand -> Zwischenergebnis_merken
         * }
         */
        public int Berechne(int operand, string operation)
        {
            var ergebnis = 0;

            Operation_merken(operation);
            Zwischenergebnis_laden(
                             operand,
                             (opLeft, opRight) => ergebnis = Mit_Zwischenergebnis_verknüpfen(opRight, opLeft),
                             opSingle =>          ergebnis = Zwischenergebnis_merken(opSingle)
                );

            return ergebnis;
        }


        private int Verknüpfen(int opLeft, int opRight, string op)
        {
            switch (op)
            {
                case "+":
                    return opLeft + opRight;
                case "*":
                    return opLeft * opRight;
                case "=":
                    return opRight;
                default:
                    throw new NotImplementedException("Unbekannter Operator: " + op);
            }
        }


        private int Mit_Zwischenergebnis_verknüpfen(int opRight, int opLeft)
        {
            var op = Gemerkte_Operation_laden();
            int ergebnis = Verknüpfen(opLeft, opRight, op);
            Zwischenergebnis_merken(ergebnis);
            return ergebnis;
        }


        private int? _zwischenergebnis;
        private int Zwischenergebnis_merken(int ergebnis)
        {
            _zwischenergebnis = ergebnis;
            return _zwischenergebnis.Value;
        }
        
        private void Zwischenergebnis_laden(int operand, Action<int, int> zwei_Operanden, Action<int> nur_ein_Operand)
        {
            if (_zwischenergebnis == null)
                nur_ein_Operand(operand);
            else
                zwei_Operanden(_zwischenergebnis.Value, operand);
        }


        private readonly Queue<string> _vorherige_Operation = new Queue<string>();
        private void Operation_merken(string operation)
        {
           _vorherige_Operation.Enqueue(operation);
        }

        private string Gemerkte_Operation_laden()
        {
            return _vorherige_Operation.Dequeue();
        }
    }
}
