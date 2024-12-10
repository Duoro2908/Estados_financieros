using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vidrieria_La_Union__3
{
    public class BalanceGeneral
    {
        public double TotalActivos { get; set; }
        public double TotalPasivos { get; set; }
        public double TotalCapital { get; set; }

        public string CalcularBalance(DataGridView dgv)
        {
            TotalActivos = 0;
            TotalPasivos = 0;
            TotalCapital = 0;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["Categoria"].Value != null && row.Cells["Monto"].Value != null)
                {
                    string categoria = row.Cells["Categoria"].Value.ToString();

                    if (double.TryParse(row.Cells["Monto"].Value.ToString(), out double monto))
                    {
                        switch (categoria)
                        {
                            case "Activo":
                                TotalActivos += monto;
                                break;
                            case "Pasivo":
                                TotalPasivos += monto;
                                break;
                            case "Capital":
                                TotalCapital += monto; 
                                break;
                        }
                    }
                    else
                    {
                        return "Hay un valor inválido en la columna 'Monto'.";
                    }
                }
            }

            double Suma_Pasivo_Y_Capital = TotalPasivos + TotalCapital;

            if (TotalActivos == Suma_Pasivo_Y_Capital)
            {
                return $"¡Felicidades! Su empresa está trabajando excelente ya que la ecuación contable dice que los activos deben ser iguales a la suma de pasivo y capital.\n\n" +
                       $"Totales:\nActivos: {TotalActivos}\nPasivos: {TotalPasivos}\n" +
                       $"Capital: {TotalCapital}\nSuma de Pasivos y Capital: {Suma_Pasivo_Y_Capital}";
            }
            else
            {
                return $"Haz fracasado con tus finanzas. Alerta!! debes mejorar algo anda mal.\n\n" +
                       $"Totales:\nActivos: {TotalActivos}\nPasivos: {TotalPasivos}\n" +
                       $"Capital: {TotalCapital}\nSuma de Pasivos y Capital: {Suma_Pasivo_Y_Capital}";
            }
        }
    }
}

