using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Modelos
{
    public class Mes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Ano { get; set; }

        public Mes() { }

        public Mes(SQLiteDataReader _reader)
        {
            Id = Int32.Parse(_reader["Id"].ToString());
            Nome = _reader["Nome"].ToString();
            Ano = _reader["Ano"].ToString();
        }
    }
}
