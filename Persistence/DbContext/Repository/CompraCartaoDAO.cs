using Model.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DbContext.Repository
{
    public class CompraCartaoDAO : DbConnection
    {
        public static void InsereNovaCompraParcelada(CompraCartaoCredito compra)
        {
            CompraCartaoCredito novaCompra = new CompraCartaoCredito();
            bool inserir = false;
            if (!(compra == null))
            {
                using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
                {
                    _connection.Open();
                    using (SQLiteCommand _command = new SQLiteCommand(_connection))
                    {
                        try
                        {
                            _command.CommandText = @"INSERT INTO compraCartao (IdCartao, QuantidadeDeParcelas) 
                            VALUES (@idCartao, @quantidade)";
                            _command.Parameters.AddWithValue("@idCartao", compra.IdCartao);
                            _command.Parameters.AddWithValue("@quantidade", compra.QuantidadeDeParcelas);
                            _command.ExecuteNonQuery();
                            _command.Parameters.Clear();
                            _command.CommandText = @"SELECT * FROM compraCartao ORDER BY Id DESC;";
                            using (SQLiteDataReader _reader = _command.ExecuteReader())
                            {
                                if (_reader.Read())
                                {
                                    novaCompra = new CompraCartaoCredito()
                                    {
                                        Valor = compra.Valor,
                                        Quantidade = compra.Quantidade,
                                        DataDeCompra = compra.DataDeCompra,
                                        Local = compra.Local,
                                        Estabelecimento = compra.Estabelecimento,
                                        Descricao = compra.Descricao,
                                        IdMes = compra.IdMes,
                                        IdCartao = Int32.Parse(_reader["IdCartao"].ToString()),
                                        IdCompraParcelada = Int32.Parse(_reader["Id"].ToString()),
                                        QuantidadeDeParcelas = compra.QuantidadeDeParcelas
                                    };

                                    inserir = true;
                                }
                            }
                            _connection.Close();
                        }
                        catch { _connection.Close(); }
                        finally
                        {
                            if (inserir) CompraDao.InserirNovaCompraParcelada(novaCompra);
                        }
                    }
                }
            }
        }

        public static CompraCartaoCredito RecuperaUltimaInserida()
        {
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                _connection.Open();
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    _command.CommandText = @"SELECT * FROM compraCartao ORDER BY Id DESC;";
                    using (SQLiteDataReader _reader = _command.ExecuteReader())
                    {
                        if (_reader.Read())
                        {
                            return new CompraCartaoCredito(_reader);
                        }
                    }
                }
            }
            return null;
        }
    }
}
