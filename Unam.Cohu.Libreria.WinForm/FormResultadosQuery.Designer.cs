namespace Unam.Cohu.Libreria.WinForm
{
    partial class FormResultadosQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormResultadosQuery));
            this.RichtTxtResultados = new System.Windows.Forms.RichTextBox();
            this.ButtonCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RichtTxtResultados
            // 
            this.RichtTxtResultados.Location = new System.Drawing.Point(12, 12);
            this.RichtTxtResultados.Name = "RichtTxtResultados";
            this.RichtTxtResultados.Size = new System.Drawing.Size(548, 217);
            this.RichtTxtResultados.TabIndex = 0;
            this.RichtTxtResultados.Text = "";
            // 
            // ButtonCerrar
            // 
            this.ButtonCerrar.Location = new System.Drawing.Point(252, 235);
            this.ButtonCerrar.Name = "ButtonCerrar";
            this.ButtonCerrar.Size = new System.Drawing.Size(75, 23);
            this.ButtonCerrar.TabIndex = 1;
            this.ButtonCerrar.Text = "Cerrar";
            this.ButtonCerrar.UseVisualStyleBackColor = true;
            this.ButtonCerrar.Click += new System.EventHandler(this.ButtonCerrar_Click);
            // 
            // FormResultadosQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 261);
            this.Controls.Add(this.ButtonCerrar);
            this.Controls.Add(this.RichtTxtResultados);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormResultadosQuery";
            this.Text = "FormResultadosQuery";
            this.Load += new System.EventHandler(this.FormResultadosQuery_Load);
            this.Shown += new System.EventHandler(this.FormResultadosQuery_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RichtTxtResultados;
        private System.Windows.Forms.Button ButtonCerrar;
    }
}