using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVA1_CARRIONINFANTES
{
    public partial class frmListadoClientes : Form
    {
        string cadenaConexion = @"Server=localhost\MSSQLSERVERDEVEL; DataBase=Comercial; user= sa; password=1234";
        SqlDataAdapter adaptador;
        SqlConnection conexion;
        DataSet datos;
        public frmListadoClientes()
        {
            InitializeComponent();
            dgvListado.AutoGenerateColumns = false;

            // Creacion de la instancia de la Conexion
            conexion = new SqlConnection(cadenaConexion);

            // Creacion de la instancia del Adaptador
            adaptador = new SqlDataAdapter();

            // Creacion de la instancia del DataSet
            datos = new DataSet();

            // Configurar métodos del adaptador
            adaptador.SelectCommand = new SqlCommand("SELECT * FROM Cliente", conexion);

            adaptador.InsertCommand = new SqlCommand("INSERT INTO Cliente(Apellidos, Nombres, Direccion, Telefono, Email, Tipo, LimiteCredito) " +
                "VALUES(@apellidos, @nombres, @direccion, @telefono, @email, @tipo, @limitecredito)", conexion);
            //ADAPTADOR INSERT
            adaptador.InsertCommand.Parameters.Add("@apellidos", SqlDbType.VarChar, 50, "Apellidos");
            adaptador.InsertCommand.Parameters.Add("@nombres", SqlDbType.VarChar, 50, "Nombres");
            adaptador.InsertCommand.Parameters.Add("@direccion", SqlDbType.VarChar, 80, "Direccion");
            adaptador.InsertCommand.Parameters.Add("@telefono", SqlDbType.VarChar, 20, "Telefono");
            adaptador.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar, 40, "Email");
            adaptador.InsertCommand.Parameters.Add("@tipo", SqlDbType.VarChar, 10, "Tipo");
            adaptador.InsertCommand.Parameters.Add("@limitecredito", SqlDbType.Decimal, 50, "LimiteCredito");
            adaptador.InsertCommand.Connection = conexion;
            //ADAPTADOR UPDATE
            adaptador.UpdateCommand = new SqlCommand("UPDATE Cliente SET Nombres = @nombres WHERE ID = @id", conexion);
            adaptador.UpdateCommand.Parameters.Add("@nombres", SqlDbType.VarChar, 20, "Nombres");
            adaptador.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 1, "ID");
            adaptador.UpdateCommand.Connection = conexion;
            //ADAPTADOR DELETE
            adaptador.DeleteCommand = new SqlCommand("DELETE FROM Cliente WHERE ID = @id", conexion);
            adaptador.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 0, "ID");
            adaptador.DeleteCommand.Connection = conexion;
        }

        private void cargarFormulario(object sender, EventArgs e)
        {
            mostrarDatos();
        }

        private void mostrarDatos()
        {
            //LlENAR DATOS AL DATASET
            adaptador.Fill(datos, "Cliente");
            //Enlazar datos al DATAGRIDVIEW
            if (dgvListado != null)
            {
                dgvListado.DataSource = datos.Tables["Cliente"];
            }
        }
        private void nuevoRegistro(object sender, EventArgs e)
        {
            var frm = new frmClientes();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var nuevaFila = datos.Tables["Cliente"].NewRow();
                nuevaFila["Apellidos"] = frm.Controls["txtApellidos"].Text;
                nuevaFila["Nombres"] = frm.Controls["txtNombres"].Text;
                nuevaFila["Tipo"] = frm.Controls["cboTipo"].Text;
                nuevaFila["LimiteCredito"] = frm.Controls["txtCredito"].Text;
                nuevaFila["Telefono"] = frm.Controls["txtTelefono"].Text;
                nuevaFila["Email"] = frm.Controls["txtEmail"].Text;
                nuevaFila["Direccion"] = frm.Controls["txtDireccion"].Text;
                datos.Tables["Cliente"].
                    Rows.Add(nuevaFila);
                adaptador.Update(datos.Tables["Cliente"]);
            }
        }

        private void editarRegistro(object sender, EventArgs e)
        {
            var filaActual = dgvListado.CurrentRow;
            if (filaActual != null)
            {
                var ID = filaActual.Cells[0].Value.ToString();
                DataRow fila = datos.Tables["Cliente"].Select($"ID={ID}").FirstOrDefault();

                var frm = new frmClientes(fila);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    fila["Apellidos"] = frm.Controls["txtApellidos"].Text;
                    fila["Nombres"] = frm.Controls["txtNombres"].Text;
                    fila["Tipo"] = frm.Controls["cboTipo"].Text;
                    fila["LimiteCredito"] = frm.Controls["txtCredito"].Text;
                    fila["Telefono"] = frm.Controls["txtTelefono"].Text;
                    fila["Email"] = frm.Controls["txtEmail"].Text;
                    fila["Direccion"] = frm.Controls["txtDireccion"].Text;


                }
            }
        }
        
        private void eliminarRegistro(object sender, EventArgs e)
        {
            var filaActual = dgvListado.CurrentRow;
            if (filaActual != null)
            {
                var ID = filaActual.Cells[0].Value.ToString();
                var fila = datos.Tables["Cliente"].Select($"ID={ID}").FirstOrDefault();
                if (fila != null)
                {
                    fila.Delete();
                    adaptador.Update(datos.Tables["Cliente"]);
                }
            }

        }

        private void Salir(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActualizarBD(object sender, EventArgs e)
        {
            adaptador.Update(datos.Tables["Cliente"]);
            datos.Tables["Cliente"].Clear();
            mostrarDatos();
        }
    }
}
