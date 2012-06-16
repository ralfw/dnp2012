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
            Transform_text(textBox1.Text);
        }


        public void Display(string text)
        {
            listBox1.Items.Insert(0, text);
        }


        public event Action<string> Transform_text;
    }
}
