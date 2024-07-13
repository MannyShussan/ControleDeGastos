using Model.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DbContext.Repository
{
    public class CompraDao : DbConnection
    {
        public static List<Compra> RecuperaTodasCompras()
        {
            List<Compra> compras = new List<Compra>();
            using (SQLiteConnection _connectio = new SQLiteConnection(ConnectionString))
            {
                _connectio.Open();
                using (SQLiteCommand _command = new SQLiteCommand(_connectio))
                {
                    _command.CommandText = "SELECT * FROM compra;";
                    try
                    {
                        using (SQLiteDataReader _reader = _command.ExecuteReader())
                        {
                            while (_reader.Read())
                            {
                                compras.Add(new Compra(_reader));
                            }
                            _reader.Close();
                        }
                    }
                    catch { return null; }
                    finally { _connectio.Close(); }
                }
            }
            return compras;
        }

        public static void InsereNovaCompra(Compra c, int idParcela)
        {
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                _connection.Open();
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    try
                    {
                        _command.CommandText = @"INSERT INTO compra (Valor, Quantidade, DataDeCompra, Local, Estabelecimento, Descricao, DataDeAlteracao, DataDeCriacao, IdMes, IdCompraParcelada) VALUES 
                        (@valor, @quantidade, @dataDecompra, @local, @estabelecimento, @descricao, @dataAlteracao, @dataCriacao, @idMes, @idCompraParcelada);";
                        _command.Parameters.AddWithValue("@valor", c.Valor);
                        _command.Parameters.AddWithValue("@quantidade", c.Quantidade);
                        _command.Parameters.AddWithValue("dataDecompra", c.DataDeCompra);
                        _command.Parameters.AddWithValue("@local", c.Local);
                        _command.Parameters.AddWithValue("@estabelecimento", c.Estabelecimento);
                        _command.Parameters.AddWithValue("@descricao", c.Descricao);
                        _command.Parameters.AddWithValue("@dataAlteracao", DateTime.Now.ToString());
                        _command.Parameters.AddWithValue("@dataCriacao", DateTime.Now.ToString());
                        _command.Parameters.AddWithValue("@idMes", c.IdMes);
                        _command.Parameters.AddWithValue("@idCompraParcelada", idParcela);
                        _command.ExecuteNonQuery();
                    }
                    catch { }
                    finally { _connection.Close(); }
                }
            }

        }

        public static void InsereNovaCompra(Compra c)
        {
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                _connection.Open();
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    try
                    {
                        _command.CommandText = @"INSERT INTO compra (Valor, Quantidade, DataDeCompra, Local, Estabelecimento, Descricao, DataDeAlteracao, DataDeCriacao, IdMes, IdCompraParcelada) VALUES 
                        (@valor, @quantidade, @dataDecompra, @local, @estabelecimento, @descricao, @dataAlteracao, @dataCriacao, @idMes, @idCompraParcelada);";
                        _command.Parameters.AddWithValue("@valor", c.Valor);
                        _command.Parameters.AddWithValue("@quantidade", c.Quantidade);
                        _command.Parameters.AddWithValue("dataDecompra", c.DataDeCompra);
                        _command.Parameters.AddWithValue("@local", c.Local);
                        _command.Parameters.AddWithValue("@estabelecimento", c.Estabelecimento);
                        _command.Parameters.AddWithValue("@descricao", c.Descricao);
                        _command.Parameters.AddWithValue("@dataAlteracao", DateTime.Now.ToString());
                        _command.Parameters.AddWithValue("@dataCriacao", DateTime.Now.ToString());
                        _command.Parameters.AddWithValue("@idMes", c.IdMes);
                        _command.Parameters.AddWithValue("@idCompraParcelada", 1);
                        _command.ExecuteNonQuery();
                    }
                    catch { }
                    finally { _connection.Close(); }
                }
            }

        }

        public static void InserirNovaCompraParcelada(CompraCartaoCredito cc)
        {
            int id = cc.IdCompraParcelada;
            for (int i = 0; i < cc.QuantidadeDeParcelas; i++)
            {
                InsereNovaCompra(new Compra()
                {
                    Id = cc.Id,
                    Valor = cc.Valor,
                    Quantidade = cc.Quantidade,
                    DataDeCompra = cc.DataDeCompra,
                    Local = cc.Local,
                    Estabelecimento = cc.Estabelecimento,
                    Descricao = cc.Descricao,
                    DataDeAlteracao = DateTime.Now.ToString(),
                    DataDeCriacao = DateTime.Now.ToString(),
                    IdMes = (cc.IdMes + i)
                }, id);
            }
        }

        public static List<string> NomeDasColunas()
        {
            List<string> colunas = new List<string>();
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                _connection.Open();
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    try
                    {
                        _command.CommandText = "SELECT name FROM pragma_table_info('compra');";
                        using (SQLiteDataReader _reader = _command.ExecuteReader())
                        {
                            while (_reader.Read()) { colunas.Add(_reader["name"].ToString()); }
                            _reader.Close();
                        }
                    }
                    catch { return null; }
                    finally { _connection.Close(); }
                }
            }
            return colunas;
        }
    }
}
