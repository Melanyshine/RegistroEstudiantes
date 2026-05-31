using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RegistroEmpleados
{
    public partial class Form1 : Form
    {
        ErrorProvider error = new ErrorProvider();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ValidarCampos();

                MessageBox.Show("Datos guardados correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    "Error de validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void ValidarCampos()
        {
            error.Clear();

            ValidarNombre();
            ValidarEdad();
            ValidarCorreo();
        }


        private void ValidarNombre()
        {
            if (txtName.Text.Trim() == "")
            {
                error.SetError(txtName, "El nombre es obligatorio");
                throw new Exception("Debe ingresar el nombre completo");
            }
        }


        private void ValidarEdad()
        {
            int edad;

            if (int.TryParse(txtEdad.Text, out edad) == false)
            {
                error.SetError(txtEdad, "Edad inválida");
                throw new Exception("La edad debe ser un número entero");
            }

            if (edad <= 0 || edad > 120)
            {
                error.SetError(txtEdad, "Edad fuera de rango");
                throw new Exception("Edad debe estar entre 1 y 120");
            }
        }


        private void ValidarCorreo()
        {
            string correo = txtCorreo.Text.Trim();

            if (correo == "")
            {
                error.SetError(txtCorreo, "Correo obligatorio");
                throw new Exception("Debe ingresar un correo electrónico");
            }


            if (correo.Contains("@") == false || correo.Contains(".") == false)
            {
                error.SetError(txtCorreo, "Debe contener @ y .");
                throw new Exception("El correo debe contener '@' y '.'");
            }

            if (!(correo.EndsWith("@gmail.com") ||
                  correo.EndsWith("@hotmail.com") ||
                  correo.EndsWith("@outlook.com")))
            {
                error.SetError(txtCorreo, "Dominio no válido");
                throw new Exception("Solo se permiten gmail, hotmail o outlook");
            }

            string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(correo, patron))
            {
                error.SetError(txtCorreo, "Formato incorrecto");
                throw new Exception("Formato de correo inválido");
            }
        }


        private void LimpiarCampos()
        {
            txtName.Clear();
            txtEdad.Clear();
            txtCorreo.Clear();

            error.Clear();

            txtName.Focus();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "¿Deseas salir de la aplicación?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
