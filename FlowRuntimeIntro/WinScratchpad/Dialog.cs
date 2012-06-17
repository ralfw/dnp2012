using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinScratchpad
{
    public partial class Dialog : Form
    {
        public Dialog()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var corr = new Correlation(textBox1.Text);

            var lvi = new ListViewItem(textBox1.Text);
            lvi.Tag = corr.Id;
            lvi.BackColor = Color.LightYellow;
            listView1.Items.Insert(0, lvi);

            Transform_text(corr);
        }


        public void Display(Correlation corr)
        {
            for(var i =0; i<listView1.Items.Count; i++)
            {
                var lvi = listView1.Items[i];
                if ((Guid)lvi.Tag == corr.Id)
                {
                    lvi.SubItems.Add((string) corr.Data);
                    lvi.BackColor = Color.LightGreen;
                }
            }
        }


        public event Action<Correlation> Transform_text;
    }
}
