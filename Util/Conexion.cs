using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data.MySqlClient;

namespace WindowsFormsApp7.Util
{
    public static class Conexion
    {
        private static string cadena = "Server=localhost;Database=cuarto_maysep2025;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(cadena);
        }
    }
}