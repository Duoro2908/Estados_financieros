using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;// Para trabajar con la base de datos 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vidrieria_La_Union__3
{
    public partial class EstadoResultadosForm : Form
    {
        //conexion de la base de datos 
        private string conexionString = "Server=XAN\\SQLEXPRESS;Database=EmpresaFinanzas;Trusted_Connection=True;";
        public EstadoResultadosForm()
        {
            InitializeComponent();
            cmbCategoria.Items.AddRange(new string[] { "Ventas", "CostodeVentas", "GastosOperativos" });
            CargarDatos();
        }
        private void CargarDatos()
        {       
            using (SqlConnection conn = new SqlConnection(conexionString))
            {
                conn.Open();//Abrir conexion
                string query = "SELECT * FROM EstadoResultados";//consulta SQL
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);// Adaptador para ejecutar la consulta
                DataTable dt = new DataTable();//Tabla donde se almacenaran los datos 
                adapter.Fill(dt);// Lleno la tabla con los datos de la base de datos
                dgvEstadoResultados.DataSource = dt;//Asignacion de los datos al DataGridView
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(conexionString))
            {
                conn.Open();
                string query = "INSERT INTO EstadoResultados (Categoria, Monto) VALUES (@Categoria, @Monto)";// Consulta para insertar los datos
                SqlCommand cmd = new SqlCommand(query, conn);//comando para ejecutar consulta
                cmd.Parameters.AddWithValue("@Categoria", cmbCategoria.SelectedItem);// Los valores que se insertan en la consulta son obtenidos del formulario
                cmd.Parameters.AddWithValue("@Monto", double.Parse(txtMonto.Text));
                cmd.ExecuteNonQuery();// Ejecuto la consulta, que no devuelve resultados
            }
            CargarDatos();
            LimpiarCampos();
            MessageBox.Show("Datos guardados correctamente.");
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtId.Text))
                {
                    MessageBox.Show("Por favor, ingresa el Id de la cuenta a eliminar.");
                    return;
                }

                int id;
                if (!int.TryParse(txtId.Text, out id))
                {
                    MessageBox.Show("El Id debe ser un número válido.");
                    return;
                }

                // Conexión a la base de datos
                using (SqlConnection conn = new SqlConnection(conexionString))
                {
                    conn.Open();
                    string query = "DELETE FROM EstadoResultados WHERE Id = @Id";// Consulta para eliminar el registro
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);// Añadimos el ID como parámetro
                        int rowsAffected = cmd.ExecuteNonQuery();// Ejecutamos la consulta
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cuenta eliminada exitosamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró ninguna cuenta con ese Id.");
                        }
                    }
                }
                CargarDatos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                EstadoResultado miestadoDeResultados = new EstadoResultado();

                string resultado = miestadoDeResultados.CalcularEstadoDeResultados(dgvEstadoResultados);

                MessageBox.Show(resultado);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error: {ex.Message}");
            }
        }
        private void LimpiarCampos()
        {
            cmbCategoria.SelectedIndex = -1; 
            txtMonto.Clear();
            txtId.Clear();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar salir: {ex.Message}");
            }
        }
    }
}
       
 

