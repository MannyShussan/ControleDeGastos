using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Modelos
{
    public class Compra
    {
        public int Id { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
        public string DataDeCompra { get; set; }
        public string Local { get; set; }
        public string Estabelecimento { get; set; }
        public string Descricao { get; set; }
        public string DataDeAlteracao { get; set; }
        public string DataDeCriacao { get; set; }

        public Compra() { }

        public Compra(int id, double valor, int quantidade, string dataCompra, string local, string estabelecimento, string descricao, string dataAlteracao, string dataCriacao)
        {
            Id = id;
            Valor = valor;
            Quantidade = quantidade;
            DataDeCompra = dataCompra;
            Local = local;
            Estabelecimento = estabelecimento;
            Descricao = descricao;
            DataDeAlteracao = dataAlteracao;
            DataDeCriacao = dataCriacao;
        }

        public Compra(SQLiteDataReader _reader)
        {
            Id = Int32.Parse(_reader["Id"].ToString());
            Valor = Double.Parse(_reader["Valor"].ToString());
            Quantidade = Int32.Parse(_reader["Quantidade"].ToString());
            DataDeCompra = _reader["DataDeCompra"].ToString();
            Local = _reader["Local"].ToString();
            Estabelecimento = _reader["Estabelecimento"].ToString();
            Descricao = _reader["Descricao"].ToString();
            DataDeAlteracao = _reader["DataAlteracao"].ToString();
            DataDeCriacao = _reader["DataDeCriacao"].ToString();
        }
    }
}
