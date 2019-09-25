namespace Unam.Cohu.Libreria.WinForm
{
    partial class FormBGRMD
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBGRMD));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ButtonAbrir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.LabelPath = new System.Windows.Forms.Label();
            this.CheckEditarCnnStr = new System.Windows.Forms.CheckBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.DataGridProcesados = new System.Windows.Forms.DataGridView();
            this.ButtonActualizar = new System.Windows.Forms.Button();
            this.ButtonGenerarLista = new System.Windows.Forms.Button();
            this.ButtonGenerarQuery = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.LabelEstatusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TextBoxDataSource = new System.Windows.Forms.TextBox();
            this.TextBoxUser = new System.Windows.Forms.TextBox();
            this.TextBoxPassword = new System.Windows.Forms.TextBox();
            this.TextBoxBD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TextBoxRutaPDF = new System.Windows.Forms.TextBox();
            this.TextBoxPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DataGridConvertidos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridProcesados)).BeginInit();
            this.StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridConvertidos)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ButtonAbrir
            // 
            this.ButtonAbrir.Location = new System.Drawing.Point(12, 83);
            this.ButtonAbrir.Name = "ButtonAbrir";
            this.ButtonAbrir.Size = new System.Drawing.Size(75, 23);
            this.ButtonAbrir.TabIndex = 7;
            this.ButtonAbrir.Text = "Abrir...";
            this.ButtonAbrir.UseVisualStyleBackColor = true;
            this.ButtonAbrir.Click += new System.EventHandler(this.ButtonAbrir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Conexión:";
            // 
            // LabelPath
            // 
            this.LabelPath.AutoSize = true;
            this.LabelPath.Location = new System.Drawing.Point(9, 114);
            this.LabelPath.Name = "LabelPath";
            this.LabelPath.Size = new System.Drawing.Size(58, 13);
            this.LabelPath.TabIndex = 2;
            this.LabelPath.Text = "[Directorio]";
            // 
            // CheckEditarCnnStr
            // 
            this.CheckEditarCnnStr.AutoSize = true;
            this.CheckEditarCnnStr.Checked = true;
            this.CheckEditarCnnStr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckEditarCnnStr.Location = new System.Drawing.Point(559, 27);
            this.CheckEditarCnnStr.Name = "CheckEditarCnnStr";
            this.CheckEditarCnnStr.Size = new System.Drawing.Size(70, 17);
            this.CheckEditarCnnStr.TabIndex = 5;
            this.CheckEditarCnnStr.Text = "No Editar";
            this.CheckEditarCnnStr.UseVisualStyleBackColor = true;
            this.CheckEditarCnnStr.Click += new System.EventHandler(this.CheckEditarCnnStr_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // DataGridProcesados
            // 
            this.DataGridProcesados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridProcesados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridProcesados.Location = new System.Drawing.Point(9, 357);
            this.DataGridProcesados.Name = "DataGridProcesados";
            this.DataGridProcesados.Size = new System.Drawing.Size(1082, 273);
            this.DataGridProcesados.TabIndex = 11;
            // 
            // ButtonActualizar
            // 
            this.ButtonActualizar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonActualizar.Enabled = false;
            this.ButtonActualizar.Location = new System.Drawing.Point(389, 636);
            this.ButtonActualizar.Name = "ButtonActualizar";
            this.ButtonActualizar.Size = new System.Drawing.Size(122, 23);
            this.ButtonActualizar.TabIndex = 12;
            this.ButtonActualizar.Text = "Actualizar BD";
            this.ButtonActualizar.UseVisualStyleBackColor = true;
            this.ButtonActualizar.Click += new System.EventHandler(this.ButtonActualizar_Click);
            // 
            // ButtonGenerarLista
            // 
            this.ButtonGenerarLista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonGenerarLista.Enabled = false;
            this.ButtonGenerarLista.Location = new System.Drawing.Point(9, 328);
            this.ButtonGenerarLista.Name = "ButtonGenerarLista";
            this.ButtonGenerarLista.Size = new System.Drawing.Size(129, 23);
            this.ButtonGenerarLista.TabIndex = 10;
            this.ButtonGenerarLista.Text = "Generar Lista";
            this.ButtonGenerarLista.UseVisualStyleBackColor = true;
            this.ButtonGenerarLista.Click += new System.EventHandler(this.ButtonGenerarLista_Click);
            // 
            // ButtonGenerarQuery
            // 
            this.ButtonGenerarQuery.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ButtonGenerarQuery.Enabled = false;
            this.ButtonGenerarQuery.Location = new System.Drawing.Point(529, 636);
            this.ButtonGenerarQuery.Name = "ButtonGenerarQuery";
            this.ButtonGenerarQuery.Size = new System.Drawing.Size(122, 23);
            this.ButtonGenerarQuery.TabIndex = 14;
            this.ButtonGenerarQuery.Text = "Obtener txt";
            this.ButtonGenerarQuery.UseVisualStyleBackColor = true;
            this.ButtonGenerarQuery.Click += new System.EventHandler(this.ButtonGenerarQuery_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelEstatusStrip});
            this.StatusStrip.Location = new System.Drawing.Point(0, 677);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(1103, 22);
            this.StatusStrip.TabIndex = 10;
            this.StatusStrip.Text = "[Proceso]";
            // 
            // LabelEstatusStrip
            // 
            this.LabelEstatusStrip.Name = "LabelEstatusStrip";
            this.LabelEstatusStrip.Size = new System.Drawing.Size(57, 17);
            this.LabelEstatusStrip.Text = "[Proceso]";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::Unam.Cohu.Libreria.WinForm.Properties.Resources.gifAjax;
            this.pictureBox1.InitialImage = global::Unam.Cohu.Libreria.WinForm.Properties.Resources.gifAjax;
            this.pictureBox1.Location = new System.Drawing.Point(144, 328);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(23, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // TextBoxDataSource
            // 
            this.TextBoxDataSource.Enabled = false;
            this.TextBoxDataSource.Location = new System.Drawing.Point(93, 24);
            this.TextBoxDataSource.Name = "TextBoxDataSource";
            this.TextBoxDataSource.Size = new System.Drawing.Size(100, 20);
            this.TextBoxDataSource.TabIndex = 1;
            this.TextBoxDataSource.TextChanged += new System.EventHandler(this.TextBoxDataSource_TextChanged);
            // 
            // TextBoxUser
            // 
            this.TextBoxUser.Enabled = false;
            this.TextBoxUser.Location = new System.Drawing.Point(325, 24);
            this.TextBoxUser.Name = "TextBoxUser";
            this.TextBoxUser.Size = new System.Drawing.Size(100, 20);
            this.TextBoxUser.TabIndex = 3;
            this.TextBoxUser.TextChanged += new System.EventHandler(this.TextBoxUser_TextChanged);
            // 
            // TextBoxPassword
            // 
            this.TextBoxPassword.Enabled = false;
            this.TextBoxPassword.Location = new System.Drawing.Point(442, 24);
            this.TextBoxPassword.Name = "TextBoxPassword";
            this.TextBoxPassword.Size = new System.Drawing.Size(100, 20);
            this.TextBoxPassword.TabIndex = 4;
            this.TextBoxPassword.TextChanged += new System.EventHandler(this.TextBoxPassword_TextChanged);
            // 
            // TextBoxBD
            // 
            this.TextBoxBD.Enabled = false;
            this.TextBoxBD.Location = new System.Drawing.Point(208, 24);
            this.TextBoxBD.Name = "TextBoxBD";
            this.TextBoxBD.Size = new System.Drawing.Size(100, 20);
            this.TextBoxBD.TabIndex = 2;
            this.TextBoxBD.TextChanged += new System.EventHandler(this.TextBoxBD_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(93, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Servidor\\Instancia:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(205, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "BD:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(322, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Usuario:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(439, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Password";
            // 
            // TextBoxRutaPDF
            // 
            this.TextBoxRutaPDF.Location = new System.Drawing.Point(93, 50);
            this.TextBoxRutaPDF.Name = "TextBoxRutaPDF";
            this.TextBoxRutaPDF.Size = new System.Drawing.Size(449, 20);
            this.TextBoxRutaPDF.TabIndex = 6;
            this.TextBoxRutaPDF.Text = "http://www.humanidades.unam.mx/";
            // 
            // TextBoxPath
            // 
            this.TextBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxPath.Enabled = false;
            this.TextBoxPath.Location = new System.Drawing.Point(93, 86);
            this.TextBoxPath.Name = "TextBoxPath";
            this.TextBoxPath.Size = new System.Drawing.Size(995, 20);
            this.TextBoxPath.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "URL PDF:";
            // 
            // DataGridConvertidos
            // 
            this.DataGridConvertidos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridConvertidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridConvertidos.Location = new System.Drawing.Point(9, 130);
            this.DataGridConvertidos.Name = "DataGridConvertidos";
            this.DataGridConvertidos.Size = new System.Drawing.Size(1079, 192);
            this.DataGridConvertidos.TabIndex = 9;
            // 
            // FormBGRMD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 699);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TextBoxRutaPDF);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBoxBD);
            this.Controls.Add(this.TextBoxPassword);
            this.Controls.Add(this.TextBoxUser);
            this.Controls.Add(this.TextBoxDataSource);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.ButtonGenerarQuery);
            this.Controls.Add(this.ButtonGenerarLista);
            this.Controls.Add(this.ButtonActualizar);
            this.Controls.Add(this.DataGridProcesados);
            this.Controls.Add(this.DataGridConvertidos);
            this.Controls.Add(this.TextBoxPath);
            this.Controls.Add(this.CheckEditarCnnStr);
            this.Controls.Add(this.LabelPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonAbrir);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormBGRMD";
            this.Text = "BGRMD SCRIPTORUM";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridProcesados)).EndInit();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridConvertidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ButtonAbrir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LabelPath;
        private System.Windows.Forms.CheckBox CheckEditarCnnStr;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.DataGridView DataGridProcesados;
        private System.Windows.Forms.Button ButtonActualizar;
        private System.Windows.Forms.Button ButtonGenerarLista;
        private System.Windows.Forms.Button ButtonGenerarQuery;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel LabelEstatusStrip;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox TextBoxDataSource;
        private System.Windows.Forms.TextBox TextBoxUser;
        private System.Windows.Forms.TextBox TextBoxPassword;
        private System.Windows.Forms.TextBox TextBoxBD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextBoxRutaPDF;
        private System.Windows.Forms.TextBox TextBoxPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView DataGridConvertidos;
    }
}

