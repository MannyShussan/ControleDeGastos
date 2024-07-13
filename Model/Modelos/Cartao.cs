using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Modelos
{
    public class Cartao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdProprietario { get; set; }
        public string DiaVencimento { get; set; }

        public Cartao(){}

        public Cartao(SQLiteDataReader _reader)
        {
            Id = Int32.Parse(_reader["Id"].ToString());
            Nome = _reader["Nome"].ToString();
            DiaVencimento = _reader["DIaVencimento"].ToString();
            IdProprietario = Int32.Parse(_reader["IdPropietario"].ToString());
        }
    }
}
