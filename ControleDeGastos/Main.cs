using Persistence.DbContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ControleDeGastos
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Migrations.Criatabelas();
            editarToolStripButton.ToolTipText = "Editar";
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Lvl_Lista.View = View.Details;
            Lvl_Lista.FullRowSelect = true;
            Lvl_Lista.GridLines = true;
            Lvl_Lista.LabelEdit = true;
        }
    }
}
