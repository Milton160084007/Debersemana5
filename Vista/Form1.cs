using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using WindowsFormsApp7.DAO;
using WindowsFormsApp7.Modelo;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        private bool editandoRol = false;
        private bool editandoUsuario = false;

        public Form1()
        {
            InitializeComponent();
            CargarRoles();
            CargarUsuarios();
            CargarComboRoles();
        }

        #region Métodos para Roles
        private void CargarRoles()
        {
            dgvRoles.DataSource = RolDAO.GetAll();
        }

        private void btnAgregarRol_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDetalleRol.Text))
            {
                MessageBox.Show("Ingrese el detalle del rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var rol = new Rol { Detalle = txtDetalleRol.Text };

            bool resultado;
            if (editandoRol)
            {
                rol.RolesId = Convert.ToInt32(dgvRoles.CurrentRow.Cells["RolesId"].Value);
                resultado = RolDAO.Update(rol);
            }
            else
            {
                resultado = RolDAO.Add(rol);
            }

            if (resultado)
            {
                LimpiarRol();
                CargarRoles();
                MessageBox.Show(editandoRol ? "Rol actualizado correctamente" : "Rol agregado correctamente",
                              "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editandoRol = false;
                btnAgregarRol.Text = "Agregar Rol";
            }
        }

        private void btnEditarRol_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un rol para editar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            editandoRol = true;
            btnAgregarRol.Text = "Actualizar Rol";
            txtDetalleRol.Text = dgvRoles.CurrentRow.Cells["Detalle"].Value.ToString();
        }

        private void btnEliminarRol_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un rol para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este rol?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvRoles.CurrentRow.Cells["RolesId"].Value);
                if (RolDAO.Delete(id))
                {
                    CargarRoles();
                    MessageBox.Show("Rol eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LimpiarRol()
        {
            txtDetalleRol.Clear();
        }
        #endregion

        #region Métodos para Usuarios
        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = UsuarioDAO.GetAll();
        }

        private void CargarComboRoles()
        {
            cmbRol.DataSource = RolDAO.GetAll();
            cmbRol.DisplayMember = "Detalle";
            cmbRol.ValueMember = "RolesId";
        }

        private void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Todos los campos son requeridos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var usuario = new Usuario
            {
                Nombre = txtNombre.Text,
                Correo = txtCorreo.Text,
                Password = txtPassword.Text,
                RolesId = cmbRol.SelectedValue != null ? (int)cmbRol.SelectedValue : (int?)null
            };

            bool resultado;
            if (editandoUsuario)
            {
                usuario.UsuarioId = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["UsuarioId"].Value);
                resultado = UsuarioDAO.Update(usuario);
            }
            else
            {
                resultado = UsuarioDAO.Add(usuario);
            }

            if (resultado)
            {
                LimpiarUsuario();
                CargarUsuarios();
                MessageBox.Show(editandoUsuario ? "Usuario actualizado correctamente" : "Usuario agregado correctamente",
                              "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editandoUsuario = false;
                btnAgregarUsuario.Text = "Agregar Usuario";
            }
        }

        private void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para editar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            editandoUsuario = true;
            btnAgregarUsuario.Text = "Actualizar Usuario";

            var row = dgvUsuarios.CurrentRow;
            txtNombre.Text = row.Cells["Nombre"].Value.ToString();
            txtCorreo.Text = row.Cells["Correo"].Value.ToString();
            txtPassword.Text = row.Cells["Password"].Value.ToString();

            if (row.Cells["RolesId"].Value != DBNull.Value)
            {
                cmbRol.SelectedValue = Convert.ToInt32(row.Cells["RolesId"].Value);
            }
        }

        private void btnEliminarUsuario_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un usuario para eliminar", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Está seguro de eliminar este usuario?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["UsuarioId"].Value);
                if (UsuarioDAO.Delete(id))
                {
                    CargarUsuarios();
                    MessageBox.Show("Usuario eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void LimpiarUsuario()
        {
            txtNombre.Clear();
            txtCorreo.Clear();
            txtPassword.Clear();
            cmbRol.SelectedIndex = 0;
        }
        #endregion

        private void tabPageRoles_Click(object sender, EventArgs e)
        {

        }
    }
}