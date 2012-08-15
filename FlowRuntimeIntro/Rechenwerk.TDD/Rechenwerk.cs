using System;

namespace Rechenwerk.TDD
{
    public class Rechenwerk
    {
        private string vorherige_operation = "=";
        private int vorheriger_operand;

        public int Berechne(int operand, string operation)
        {
            if (Erster_Rechenauftrag())
                Rechenauftrag_merken(operand, operation);
            else
                Vorherigen_Rechenauftrag_ausführen(operand, operation);

            return vorheriger_operand;
        }


        private bool Erster_Rechenauftrag()
        {
            return vorherige_operation == "=";
        }

        private void Rechenauftrag_merken(int operand, string operation)
        {
            vorherige_operation = operation;
            vorheriger_operand = operand;
        }
        
        private void Vorherigen_Rechenauftrag_ausführen(int operand, string operation)
        {
            var op = vorherige_operation;
            vorherige_operation = operation;

            var opLeft = vorheriger_operand;
            var opRight = operand;

            vorheriger_operand = Verknüpfen(opLeft, opRight, op);
        }


        private int Verknüpfen(int opLeft, int opRight, string op)
        {
            switch (op)
            {
                case "+":
                    return opLeft + opRight;
                case "*":
                    return opLeft * opRight;
                default:
                    throw new NotImplementedException("Unbekannter Operator: " + op);
            }
        }
    }


    public class Rechenwerk4
    {
        private string vorherige_operation = "=";
        private int vorheriger_operand;

        public int Berechne(int operand, string operation)
        {
            if (vorherige_operation == "=")
            {
                vorherige_operation = operation;
                vorheriger_operand = operand;
            }
            else
            {
                var op = vorherige_operation;
                vorherige_operation = operation;

                var opLeft = vorheriger_operand;
                var opRight = operand;

                vorheriger_operand = Verknüpfen(opLeft, opRight, op);
            }

            return vorheriger_operand;
        }

        private int Verknüpfen(int opLeft, int opRight, string op)
        {
            switch (op)
            {
                case "+":
                    return opLeft + opRight;
                case "*":
                    return opLeft * opRight;
                default:
                    throw new NotImplementedException("Unbekannter Operator: " + op);
            }
        }
    }


    public class Rechenwerk3
    {
        private string vorherige_operation;
        private int vorheriger_operand;

        public int Berechne(int operand, string operation)
        {
            if (vorherige_operation == null)
            {
                vorherige_operation = operation;
                vorheriger_operand = operand;
            }
            else
            {
                var op = vorherige_operation;
                vorherige_operation = operation;

                var opLeft = vorheriger_operand;
                var opRight = operand;

                vorheriger_operand = Verknüpfen(opLeft, opRight, op);
            }

            return vorheriger_operand;
        }

        private int Verknüpfen(int opLeft, int opRight, string op)
        {
            switch(op)
            {
                case "+":
                    return opLeft + opRight;
                case "*":
                    return opLeft * opRight;
                default:
                    throw new NotImplementedException("Unbekannter Operator: " + op);
            }
        }
    }


    public class Rechenwerk2
    {
        private string vorherige_operation;
        private int vorheriger_operand;

        public int Berechne(int operand, string operation)
        {
            if (vorherige_operation == null)
            {
                vorherige_operation = operation;
                vorheriger_operand = operand;

                return operand;
            }
            else
            {
                var op = vorherige_operation;
                var opLeft = vorheriger_operand;
                var opRight = operand;

                return Verknüpfen(opLeft, opRight, op);
            }
        }

        private int Verknüpfen(int opLeft, int opRight, string op)
        {
            return opLeft + opRight;
        }
    }


    public class Rechenwerk1
    {
        public int Berechne(int operand, string operation)
        {
            return operand;
        }
    }
}
