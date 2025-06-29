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
    public class UsuarioDAO
    {
        public static List<Usuario> GetAll()
        {
            var lista = new List<Usuario>();
            using (var con = Conexion.GetConnection())
            {
                con.Open();
                var cmd = new MySqlCommand("SELECT * FROM usuarios", con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Usuario
                        {
                            UsuarioId = reader.GetInt32("UsuarioId"),
                            Nombre = reader.GetString("Nombre"),
                            Correo = reader.GetString("correo"),
                            Password = reader.GetString("password"),
                            RolesId = reader.IsDBNull(reader.GetOrdinal("RolesId")) ? (int?)null : reader.GetInt32("RolesId")
                        });
                    }
                }
            }
            return lista;
        }

        public static bool Add(Usuario usuario)
        {
            using (var con = Conexion.GetConnection())
            {
                con.Open();
                var cmd = new MySqlCommand("INSERT INTO usuarios (Nombre, correo, password, RolesId) VALUES (@nombre, @correo, @pass, @rol)", con);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@pass", usuario.Password);
                cmd.Parameters.AddWithValue("@rol", usuario.RolesId ?? (object)DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Update(Usuario usuario)
        {
            using (var con = Conexion.GetConnection())
            {
                con.Open();
                var cmd = new MySqlCommand("UPDATE usuarios SET Nombre = @nombre, correo = @correo, password = @pass, RolesId = @rol WHERE UsuarioId = @id", con);
                cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@pass", usuario.Password);
                cmd.Parameters.AddWithValue("@rol", usuario.RolesId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@id", usuario.UsuarioId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public static bool Delete(int id)
        {
            using (var con = Conexion.GetConnection())
            {
                con.Open();
                var cmd = new MySqlCommand("DELETE FROM usuarios WHERE UsuarioId = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}