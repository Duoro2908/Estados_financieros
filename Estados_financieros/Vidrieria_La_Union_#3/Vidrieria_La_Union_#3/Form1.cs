using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vidrieria_La_Union__3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generarBalanceGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BalanceGeneralForm balanceForm = new BalanceGeneralForm();
            balanceForm.ShowDialog(); // Abre de forma modal no puedo interactuar con el formulario principal hasta que cierre la ventana
        }

        private void generarEstadoDeResultadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EstadoResultadosForm estadoForm = new EstadoResultadosForm();
            estadoForm.ShowDialog(); 
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
