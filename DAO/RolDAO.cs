using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp7.Modelo;
using WindowsFormsApp7.Util;

namespace WindowsFormsApp7.DAO
{
    public class RolDAO
    {
        public static List<Rol> GetAll()
        {
            var lista = new List<Rol>();
            using (var con = Conexion.GetConnection())
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM roles", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Rol
                        {
                            RolesId = reader.GetInt32("RolesId"),
                            Detalle = reader.GetString("Detalle")
                        });
                    }
                }
            }
            return lista;
        }

        public static bool Add(Rol rol)
        {
            using (var con = Conexion.GetConnection())
            {
                con.Open();
                var cmd = new MySqlCommand("INSERT INTO roles (Detalle) VALUES (@detalle)", con);
                cmd.Parameters.AddWithValue("@detalle", rol.Detalle);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Update(Rol rol)
        {
            using (var con = Conexion.GetConnection())
            {
                con.Open();
                var cmd = new MySqlCommand("UPDATE roles SET Detalle = @detalle WHERE RolesId = @id", con);
                cmd.Parameters.AddWithValue("@detalle", rol.Detalle);
                cmd.Parameters.AddWithValue("@id", rol.RolesId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Delete(int id)
        {
            using (var con = Conexion.GetConnection())
            {
                con.Open();
                var cmd = new MySqlCommand("DELETE FROM roles WHERE RolesId = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}