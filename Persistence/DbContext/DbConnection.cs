using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DbContext
{
    public class DbConnection
    {
        protected static string _path = $"{Directory.GetCurrentDirectory().ToString()}\\banco.sqlite";
        protected static string ConnectionString = $"Data Source={_path}; Version=3;";
        public DbConnection() { }

        public static void CriaArquivo()
        {
            if (!File.Exists(_path))
            {
                File.Create(_path);
            }
        }
    }
}

