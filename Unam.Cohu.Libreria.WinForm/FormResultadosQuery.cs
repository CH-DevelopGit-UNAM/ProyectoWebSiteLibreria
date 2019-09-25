using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unam.Cohu.Libreria.WinForm
{
    public partial class FormResultadosQuery : Form
    {
        public List<string> Resultados;

        public FormResultadosQuery()
        {
            InitializeComponent();
        }

        public FormResultadosQuery(List<string> querys)
        {
            InitializeComponent();
            this.Resultados = querys;
        }

        private void ButtonCerrar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormResultadosQuery_Load(object sender, EventArgs e)
        {
            if (Resultados != null)
            {
                foreach (var item in Resultados)
                {
                    this.RichtTxtResultados.Text += (item + Environment.NewLine);
                }
            }
        }

        private void FormResultadosQuery_Shown(object sender, EventArgs e)
        {

        }
    }
}
