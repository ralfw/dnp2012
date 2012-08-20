using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dateisuche.Daten;
using npantarhei.runtime.contract;

namespace Dateisuche
{
    public partial class DlgDateisuche : Form
    {
        public DlgDateisuche()
        {
            InitializeComponent();

            txtWurzelpfad.Text = Environment.CurrentDirectory;
        }

 
        private void btnSuchen_Click(object sender, EventArgs e)
        {
            Dateisuche_starten(new Suchanfrage {Wurzelpfad = txtWurzelpfad.Text, Abfrage = txtAbfrage.Text});
        }


        public event Action<Suchanfrage> Dateisuche_starten;

        [DispatchedMethod]
        public void Status_aktualisieren(Statusmeldung statusmeldung)
        {
            var statusmeldungstext = string.Format("{0}, {1}/{2}{3}, {4}", 
                                                    statusmeldung.Abfrage, 
                                                    statusmeldung.DateienGefunden, statusmeldung.DateienGeprüft, 
                                                    statusmeldung.InBearbeitung ? "..." : "",
                                                    statusmeldung.Verzeichnispfad);

            var auftragNode = Finde_Knoten_zum_Suchauftrag(statusmeldung.SuchauftragId);
            if (auftragNode == null)
            {
                auftragNode = tvSuchvorgänge.Nodes.Add("");
                auftragNode.Tag = statusmeldung.SuchauftragId;
            }
            auftragNode.Text = statusmeldungstext;
        }


        [DispatchedMethod]
        public void Dateifund_anzeigen(Dateifund dateifund)
        {
            var dateifundtext = string.Format("{0}, {1}, {2}", dateifund.Dateiname, dateifund.Veränderungsdatum, dateifund.Dateipfad);

            var auftragNode = Finde_Knoten_zum_Suchauftrag(dateifund.SuchauftragId);
            auftragNode.Nodes.Add(dateifundtext);
        }


        [DispatchedMethod]
        public void Fehler_anzeigen(FlowRuntimeException ex)
        {
            MessageBox.Show(ex.ToString(), "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        
        private TreeNode Finde_Knoten_zum_Suchauftrag(string id)
        {
            TreeNode auftragNode = null;
            foreach (var n in tvSuchvorgänge.Nodes)
                if ((string) ((TreeNode) n).Tag == id)
                    auftragNode = n as TreeNode;
            return auftragNode;
        }
    }
}
