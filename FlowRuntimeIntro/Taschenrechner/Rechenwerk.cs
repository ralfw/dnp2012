using System;

namespace Taschenrechner
{
    class Rechenwerk
    {
        public static int Operanden_verknüpfen(Tuple<int, int, Operatoren> operanden)
        {
            switch (operanden.Item3)
            {
                case Operatoren.Addition:
                    return operanden.Item1 + operanden.Item2;
                case Operatoren.Subtraktion:
                    return operanden.Item1 - operanden.Item2;
                case Operatoren.Multiplikation:
                    return operanden.Item1 * operanden.Item2;
                case Operatoren.Division:
                    return operanden.Item1 / operanden.Item2;
                default:
                    return operanden.Item2;
            }
        }
    }
}