using Model.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DbContext.Repository
{
    public class MesDao : DbConnection
    {

        public static List<Mes> recuperaTodosMeses()
        {
            List<Mes> meses = new List<Mes>();
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                _connection.Open();
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    try
                    {
                        _command.CommandText = "SELECT * FROM mes;";
                        using (SQLiteDataReader _reader = _command.ExecuteReader())
                        {
                            while (_reader.Read())
                            {
                                meses.Add(new Mes(_reader));
                            }
                        }
                    }
                    catch { return null; }
                    finally { _connection.Close(); }
                }
            }
            return meses;
        }

        public static Mes RecuperaMesPorId(int id)
        {
            using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
            {
                _connection.Open();
                using (SQLiteCommand _command = new SQLiteCommand(_connection))
                {
                    try
                    {
                        _command.CommandText = "SELECT * FROM mes WHERE Id = @id;";
                        _command.Parameters.AddWithValue("@id", id);
                        using (SQLiteDataReader _reader = _command.ExecuteReader())
                        {
                            if (_reader.Read())
                            {
                                return new Mes(_reader);
                            }
                        }
                    }
                    catch { }
                    finally { _connection.Close(); }
                }
            }
            return null;
        }

        public static void InsereMesNaTabela(Mes mes)
        {
            if (!(mes is null))
            {
                using (SQLiteConnection _connection = new SQLiteConnection(ConnectionString))
                {
                    using (SQLiteCommand _command = new SQLiteCommand(_connection))
                    {
                        _connection.Open();
                        try
                        {
                            _command.CommandText = "INSERT INTO mes (Nome, Ano) VALUES (@nome, @ano);";
                            _command.Parameters.AddWithValue("@nome", mes.Nome);
                            _command.Parameters.AddWithValue("@ano", mes.Ano);
                            _command.ExecuteNonQuery();
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        finally { _connection.Close(); }
                    }
                }
            }
        }
    }
}
