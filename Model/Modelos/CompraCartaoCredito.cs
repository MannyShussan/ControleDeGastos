using System;
using System.Collections.Generic;
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

        public CompraCartaoCredito(int id, double valor, int quantidade, string dataCompra, string local, string estabelecimento, string descricao, string dataAlteracao, string dataCriacao, int quantidadeParcelas, int idCartao) : base(id, valor, quantidade, dataCompra, local, estabelecimento, descricao, dataAlteracao, dataCriacao)
        {
            QuantidadeDeParcelas = quantidadeParcelas;
            IdCartao = idCartao;
        }
    }
}
