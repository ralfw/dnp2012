namespace Dateisuche
{
    partial class DlgDateisuche
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtWurzelpfad = new System.Windows.Forms.TextBox();
            this.btnSuchen = new System.Windows.Forms.Button();
            this.txtAbfrage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tvSuchvorgänge = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wurzelpfad";
            // 
            // txtWurzelpfad
            // 
            this.txtWurzelpfad.Location = new System.Drawing.Point(79, 6);
            this.txtWurzelpfad.Name = "txtWurzelpfad";
            this.txtWurzelpfad.Size = new System.Drawing.Size(284, 20);
            this.txtWurzelpfad.TabIndex = 1;
            this.txtWurzelpfad.Text = "c:\\";
            // 
            // btnSuchen
            // 
            this.btnSuchen.Location = new System.Drawing.Point(369, 30);
            this.btnSuchen.Name = "btnSuchen";
            this.btnSuchen.Size = new System.Drawing.Size(75, 23);
            this.btnSuchen.TabIndex = 4;
            this.btnSuchen.Text = "Suchen";
            this.btnSuchen.UseVisualStyleBackColor = true;
            this.btnSuchen.Click += new System.EventHandler(this.btnSuchen_Click);
            // 
            // txtAbfrage
            // 
            this.txtAbfrage.Location = new System.Drawing.Point(79, 32);
            this.txtAbfrage.Name = "txtAbfrage";
            this.txtAbfrage.Size = new System.Drawing.Size(284, 20);
            this.txtAbfrage.TabIndex = 3;
            this.txtAbfrage.Text = "3 10 200";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Abfrage";
            // 
            // tvSuchvorgänge
            // 
            this.tvSuchvorgänge.Location = new System.Drawing.Point(12, 67);
            this.tvSuchvorgänge.Name = "tvSuchvorgänge";
            this.tvSuchvorgänge.Size = new System.Drawing.Size(432, 244);
            this.tvSuchvorgänge.TabIndex = 5;
            // 
            // DlgDateisuche
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 322);
            this.Controls.Add(this.tvSuchvorgänge);
            this.Controls.Add(this.txtAbfrage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSuchen);
            this.Controls.Add(this.txtWurzelpfad);
            this.Controls.Add(this.label1);
            this.Name = "DlgDateisuche";
            this.Text = "Dateisuche";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWurzelpfad;
        private System.Windows.Forms.Button btnSuchen;
        private System.Windows.Forms.TextBox txtAbfrage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView tvSuchvorgänge;
    }
}

