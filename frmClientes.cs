using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EVA1_CARRIONINFANTES
{
    public partial class frmClientes : Form
    {
        DataRow fila;
        public frmClientes(DataRow filaEditar = null)
        {
            InitializeComponent();
            if (filaEditar != null)
            {
                this.fila = filaEditar;
                this.Text = "Editar registro";
                mostrarDatos();
            }
        }
        private void mostrarDatos()
        {
            txtApellidos.Text = this.fila["Apellidos"].ToString();
            txtNombres.Text = this.fila["Nombres"].ToString();
            cboTipo.Text = this.fila["Tipo"].ToString();
            txtCredito.Text = this.fila["LimiteCredito"].ToString();
            txtTelefono.Text = this.fila["Telefono"].ToString();
            txtEmail.Text = this.fila["Email"].ToString();
            txtDireccion.Text = this.fila["Direccion"].ToString();

        }

        private void aceptarCambios(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCredito.Text))
            {
                MessageBox.Show("Ingrese todos los datos", "Sistemas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (cboTipo.SelectedIndex == 0 && decimal.Parse(txtCredito.Text) > 3500)
            {
                MessageBox.Show("No se puede procesar, el monto ingresado no es correcto para PLATINIUM", "Sistemas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }
            if (cboTipo.SelectedIndex == 1 && decimal.Parse(txtCredito.Text) > 2500)
            {
                MessageBox.Show("No se puede procesar, el monto ingresado no es correcto para VIP");
               
                return;
            }

            if (cboTipo.SelectedIndex == 2 && decimal.Parse(txtCredito.Text) > 5000)
            {
                MessageBox.Show("No se puede procesar, el monto ingresado no es correcto para GOLDEN");
                
                return;
            }
            if (string.IsNullOrEmpty(txtNombres.Text))
            {
                MessageBox.Show("Ingrese todos los datos ", "Sistemas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (string.IsNullOrEmpty(txtApellidos.Text))
            {
                MessageBox.Show("Ingrese todos los datos ", "Sistemas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (string.IsNullOrEmpty(txtCredito.Text))
            {
                MessageBox.Show("Ingrese todos los datos ", "Sistemas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Ingrese todos los datos ", "Sistemas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("Ingrese todos los datos ", "Sistemas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                MessageBox.Show("Ingrese todos los datos ", "Sistemas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
            if (string.IsNullOrEmpty(cboTipo.Text))
            {
                MessageBox.Show("Ingrese el tipo de cliente", "Sistemas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
           
        }

        private void Cancelar(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

