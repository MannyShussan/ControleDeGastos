using Model.Modelos;
using Persistence.DbContext.Repository;
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
            TableMes();
            TableProprietarios();
            TableCartao();
            TableCompra();
            TableCompraCartao();
        }

        private static void TableProprietarios()
        {
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    _connection.Open();
                    try
                    {
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
                    _connection.Open();
                    try
                    {
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
                    _connection.Open();
                    try
                    {
                        _command.CommandText = @"SELECT * FROM compra;";
                        using (SQLiteDataReader reader = _command.ExecuteReader())
                        {
                            if (!reader.HasRows) { reader.Close(); }
                        }
                    }
                    catch
                    {
                        _command.CommandText = @"CREATE TABLE compra (  Id                INTEGER PRIMARY KEY AUTOINCREMENT,
                                                                        Valor             REAL    NOT NULL,
                                                                        Quantidade        REAL    NOT NULL DEFAULT (1),
                                                                        DataDeCompra      TEXT    NOT NULL,
                                                                        Local             TEXT    NOT NULL,
                                                                        Estabelecimento   TEXT    NOT NULL,
                                                                        Descricao         TEXT,
                                                                        DataDeAlteracao   TEXT    NOT NULL,
                                                                        DataDeCriacao     TEXT    NOT NULL,
                                                                        IdMes             INTEGER NOT NULL REFERENCES mes (id),
                                                                        IdCompraParcelada INTEGER REFERENCES compraCartao (Id) DEFAULT (0));";
                        _command.ExecuteNonQuery();
                    }
                    finally { _connection.Close(); }
                }
            }
        }

        private static void TableCompraCartao()
        {
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    _connection.Open();
                    try
                    {
                        _command.CommandText = "SELECT * FROM compraCartao;";
                        using (SQLiteDataReader reader = _command.ExecuteReader())
                        {
                            if (reader.HasRows) { reader.Close(); }
                        }
                    }
                    catch
                    {
                        _command.CommandText = @"CREATE TABLE compraCartao (
                                               Id                   INTEGER PRIMARY KEY AUTOINCREMENT,
                                               IdCartao             INTEGER REFERENCES cartao (Id) 
                                               NOT NULL,
                                               QuantidadeDeParcelas INTEGER NOT NULL
                                               DEFAULT (1));";
                        _command.ExecuteNonQuery();
                    }
                    finally { _connection.Close(); }
                }
            }
        }

        private static void TableMes()
        {
            bool criado = true;
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    _connection.Open();
                    try
                    {
                        _command.CommandText = "SELECT * FROM mes";
                        using (SQLiteDataReader reader = _command.ExecuteReader())
                        {
                            if (reader.HasRows) { reader.Close(); }
                        }
                    }
                    catch
                    {
                        criado = false;
                        _command.CommandText = "CREATE TABLE mes (Id   INTEGER PRIMARY KEY AUTOINCREMENT,     Nome TEXT    NOT NULL,     Ano  TEXT    NOT NULL );";
                        _command.ExecuteNonQuery();
                    }
                    finally
                    {
                        _connection.Close();
                        if (!criado) { PopulaTabelaMes(); }
                    }
                }
            }
        }

        private static void PopulaTabelaMes()
        {
            string[] meses = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
            int ano = 2023;
            for (int i = 0; i < 480; i++)
            {
                int indiceMes = i % 12;
                int indiceAno = (i / 12) + ano;
                MesDao.InsereMesNaTabela(new Mes() { Nome = meses[indiceMes], Ano = indiceAno.ToString() });
            }
        }
    }
}
