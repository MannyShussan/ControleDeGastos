using Model.Modelos;
using Persistence.DbContext;
using Persistence.DbContext.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ControleDeGastos
{
    public partial class Main : Form
    {
        private List<string> nomes { get; set; }
        public Main()
        {
            InitializeComponent();
            Migrations.Criatabelas();
            editarToolStripButton.ToolTipText = "Editar";
            nomes = CompraDao.NomeDasColunas();
            foreach (string nome in nomes)
            {
                int width = nome.Length <= 6 ? nome.Length * 12 : nome.Length * 8;
                Lvl_Lista.Columns.Add(nome, width, HorizontalAlignment.Left);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            testa();
            Lvl_Lista.View = View.Details;
            Lvl_Lista.FullRowSelect = true;
            Lvl_Lista.GridLines = true;
            Lvl_Lista.LabelEdit = true;
            List<Compra> compras = CompraDao.RecuperaTodasCompras();
            foreach (Compra compra in compras) { Lvl_Lista.Items.Add(CreateListViewItem(compra)); }
        }

        private ListViewItem CreateListViewItem<T>(T obj)
        {
            var propriedades = obj.GetType().GetProperties();
            ListViewItem item = new ListViewItem(propriedades[0].GetValue(obj).ToString());

            for (int i = 1; i < propriedades.Length; i++)
            {
                item.SubItems.Add(propriedades[i].GetValue(obj).ToString());
            }

            return item;
        }

        private void testa()
        {

            //CompraCartaoDAO.InsereNovaCompraParcelada(new CompraCartaoCredito()
            //{
            //    Valor = 330.0,
            //    Quantidade = 1,
            //    DataDeCompra = "Qualquer data",
            //    Local = "Em casa",
            //    Estabelecimento = "Nenhum",
            //    Descricao = "um texto qualquer",
            //    QuantidadeDeParcelas = 4,
            //    IdMes = 18,
            //    IdCartao = 1
            //});
        }
    }
}
