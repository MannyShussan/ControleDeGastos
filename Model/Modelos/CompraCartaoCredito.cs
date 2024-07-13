using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Modelos
{
    public class CompraCartaoCredito : Compra
    {
        public int QuantidadeDeParcelas { get; set; }
        public int IdCartao { get; set; }

        public CompraCartaoCredito() { }

        public CompraCartaoCredito(int id, double valor, int quantidade, string dataCompra, string local, string estabelecimento, string descricao, string dataAlteracao, string dataCriacao, int quantidadeParcelas, int idCartao, int idCompraParcelada) : base(id, valor, quantidade, dataCompra, local, estabelecimento, descricao, dataAlteracao, dataCriacao, idCartao)
        {
            QuantidadeDeParcelas = quantidadeParcelas;
            IdCartao = idCartao;
        }

        public CompraCartaoCredito(SQLiteDataReader _reader) : base(_reader)
        {
            QuantidadeDeParcelas = Int32.Parse(_reader["QuantidadeDeParcelas"].ToString());
            IdCartao = Int32.Parse(_reader["Id"].ToString());
        }
    }
}
