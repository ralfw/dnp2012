using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using npantarhei.runtime.contract;

namespace Taschenrechner
{
    internal partial class WinMain : Form
    {
        public WinMain()
        {
            InitializeComponent();
        }


        private void btnOp_Click(object sender, EventArgs e)
        {
            Rechenauftrag_absetzen(((Button)sender).Text);
        }

        private void WinMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("+-*/=".IndexOf(e.KeyChar.ToString()) >= 0)
            {
                Rechenauftrag_absetzen(e.KeyChar.ToString());
                e.Handled = true;
            }
        }

        private void Rechenauftrag_absetzen(string opText)
        {
            Operatoren op;

            switch (opText)
            {
                case "+":
                    op = Operatoren.Addition;
                    break;
                case "-":
                    op = Operatoren.Subtraktion;
                    break;
                case "*":
                    op = Operatoren.Multiplikation;
                    break;
                case "/":
                    op = Operatoren.Division;
                    break;
                case "=":
                    op = Operatoren.Gleichheitszeichen;
                    break;
                default:
                    throw new ArgumentException("Unbekannter Operator!");
            }

            Berechnen(new Rechenauftrag { Operand = int.Parse(txtErgebnis.Text), Operator = op });
        }


        [DispatchedMethod]
        public void Ergebnis_anzeigen(int ergebnis)
        {
            txtErgebnis.Text = ergebnis.ToString();
            txtErgebnis.SelectAll();
            txtErgebnis.Focus();
        }

        //[DispatchedMethod]
        public void Fehler_melden(FlowRuntimeException ex)
        {
            MessageBox.Show(string.Format("Es ist ein Fehler aufgetreten:\n{0}\n\nAuslösende Nachricht:\n{1}", ex.InnerException.Message, ex.Context), "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public event Action<Rechenauftrag> Berechnen;

    }
}
