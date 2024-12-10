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

namespace Vidrieria_La_Union__3
{
    public partial class BalanceGeneralForm : Form
    {
        private string conexionString = "Server=XAN\\SQLEXPRESS;Database=EmpresaFinanzas;Trusted_Connection=True;";

        public BalanceGeneralForm()
        {
            InitializeComponent();
            cmbCategoria.Items.AddRange(new string[] { "Activo", "Pasivo", "Capital" });
            cmbSubCategoria.Items.AddRange(new string[] { "Activo", "Circulante", "Fijo", "Diferido", "Otros activos", "Pasivo:", "Circulante", "No circulante", "Capital:", "Contribuido", "Ganado" });
            CargarDatos();
        }
        private void CargarDatos()
        {
            using (SqlConnection conn = new SqlConnection(conexionString))
            {
                conn.Open();
                string query = "SELECT * FROM BalanceGeneral";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvBalanceGeneral.DataSource = dt;
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(conexionString))
            {
                conn.Open();
                string query = "INSERT INTO BalanceGeneral (Categoria, SubCategoria, NombreCuenta, Monto) VALUES (@Categoria, @SubCategoria, @NombreCuenta, @Monto)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Categoria", cmbCategoria.Text);
                cmd.Parameters.AddWithValue("@SubCategoria", cmbSubCategoria.Text);
                cmd.Parameters.AddWithValue("@NombreCuenta", (txtNombreCuenta.Text));
                cmd.Parameters.AddWithValue("@Monto", double.Parse(txtMonto.Text));
                cmd.ExecuteNonQuery();
            }
            CargarDatos();

            LimpiarCampos();

            MessageBox.Show("Cuenta guardada correctamente.");
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

              
                using (SqlConnection conn = new SqlConnection(conexionString))
                {
                    conn.Open();
                    string query = "DELETE FROM BalanceGeneral WHERE Id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
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
                LimpiarCampos();

                CargarDatos();
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
                BalanceGeneral mibalanceGeneral = new BalanceGeneral();

                string resultado = mibalanceGeneral.CalcularBalance(dgvBalanceGeneral);

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
            cmbSubCategoria.SelectedIndex = -1;
            txtNombreCuenta.Clear(); 
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



