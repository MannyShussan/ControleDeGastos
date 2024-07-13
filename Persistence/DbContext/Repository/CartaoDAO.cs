using Model.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DbContext.Repository
{
    public class CartaoDAO : DbConnection
    {
        public static List<Cartao> RecuperaTodosCartoes()
        {
            List<Cartao> cartoes = new List<Cartao>();
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                _connection.Open();
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    _command.CommandText = @"SELECT * FROM cartao";
                    try
                    {
                        using (SQLiteDataReader _reader = _command.ExecuteReader())
                        {
                            while (_reader.Read())
                            {
                                cartoes.Add(new Cartao(_reader));
                            }
                            _reader.Close();
                        }

                    }
                    catch { return null; }
                    finally { _connection.Close(); }
                }
            }
            return cartoes;
        }

        public static Cartao RecuperaCartaoPorId(int id)
        {
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                _connection.Open();
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    _command.CommandText = @"SELECT * FROM cartao WHERE Id = @id";
                    _command.Parameters.AddWithValue("@id", id);
                    try
                    {
                        using (SQLiteDataReader _reader = _command.ExecuteReader())
                        {
                            if (_reader.Read())
                            {
                                return new Cartao(_reader);
                            }
                        }
                    }
                    catch { return null; }
                }
                return null;
            }
        }
    }
}
