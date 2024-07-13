using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DbContext
{
    public class Migrations : DbConnection
    {

        public static void Criatabelas()
        {
            TableProprietarios();
            TableCartao();
            TableCompra();
        }

        private static void TableProprietarios()
        {
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    try
                    {
                        _connection.Open();
                        _command.CommandText = @"SELECT * FROM propietario;";
                        using (SQLiteDataReader reader = _command.ExecuteReader())
                        {
                            if (reader.HasRows) { reader.Close(); }
                        }
                    }
                    catch (Exception ex)
                    {
                        _command.CommandText = @"CREATE TABLE propietario(Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Nome TEXT NOT NULL)";
                        _command.ExecuteNonQuery();
                    }
                    finally { _connection.Close(); }
                }
            }
        }

        private static void TableCartao()
        {
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    try
                    {
                        _connection.Open();
                        _command.CommandText = @"SELECT * FROM cartao;";
                        using (SQLiteDataReader reader = _command.ExecuteReader())
                        {
                            if (!reader.HasRows) { reader.Close(); }
                        }
                    }
                    catch
                    {
                        _command.CommandText = @"CREATE TABLE cartao (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Nome TEXT NOT NULL, DiaVencimento TEXT NOT NULL, IdPropietario INTEGER REFERENCES propietario (Id) NOT NULL)";
                        _command.ExecuteNonQuery();
                    }
                    finally { _connection.Close(); }
                }
            }
        }

        private static void TableCompra()
        {
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    try
                    {
                        _connection.Open();
                        _command.CommandText = @"SELECT * FROM compra;";
                        using (SQLiteDataReader reader = _command.ExecuteReader())
                        {
                            if (!reader.HasRows){reader.Close();}
                        }
                    }
                    catch
                    {
                        _command.CommandText = @"CREATE TABLE compra (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Valor REAL NOT NULL, Quantidade REAL NOT NULL DEFAULT (1), DataDeCompra TEXT NOT NULL,Local TEXT NOT NULL, Estabelecimento TEXT NOT NULL, Descricao TEXT, DataDeAlteracao TEXT NOT NULL, DataDeCriacao TEXT NOT NULL)";
                        _command.ExecuteNonQuery();
                    }
                    finally { _connection.Close(); }
                }
            }
        }
    }
}
