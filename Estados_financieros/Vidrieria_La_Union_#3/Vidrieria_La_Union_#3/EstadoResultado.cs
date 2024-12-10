using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vidrieria_La_Union__3
{
    public class EstadoResultado
    {
        
        public double TotalVentas { get; set; }
        public double TotalCostodeVentas { get; set; }
        public double TotalGastosOperativos { get; set; }
        public double UtilidadBruta { get; set; }
        public double PerdidaBruta { get;set; }
        public double UtilidadNeta { get; set; }
        public double PerdidaNeta {  get; set; }

        //Este método para calcular el estado de resultados basado en los datos de un DataGridView
        public string CalcularEstadoDeResultados(DataGridView dgv)
        {
           
            TotalVentas = 0;
            TotalCostodeVentas = 0;
            TotalGastosOperativos = 0;

        
            foreach (DataGridViewRow row in dgv.Rows)
            {    
                if (row.Cells["Categoria"].Value != null && row.Cells["Monto"].Value != null)
                {             
                    string categoria = row.Cells["Categoria"].Value.ToString();
                  
                    if (double.TryParse(row.Cells["Monto"].Value.ToString(), out double monto))
                    {
                        switch (categoria)
                        {
                            case "Ventas":
                                TotalVentas += monto;
                                break;
                            case "CostodeVentas":
                                TotalCostodeVentas += monto;
                                break;
                            case "GastosOperativos":
                                TotalGastosOperativos += monto;
                                break;
                        }
                    }
                    else
                    {
                        return "Hay un valor inválido en la columna 'Monto'.";
                    }
                }
            }

            UtilidadBruta = TotalVentas - TotalCostodeVentas;
            UtilidadNeta = UtilidadBruta - TotalGastosOperativos;

            string resultado;

            if (UtilidadBruta >= 0)
            {
                resultado = $"La empresa ha obtenido una utilidad bruta de: {UtilidadBruta}.";
            }
            else
            {
                resultado = $"Alerta!!!!!!! esta fracasando la empresa revisar tus finanzas ya la empresa ha registrado una pérdida bruta de: - {Math.Abs(UtilidadBruta)}.";
            }

            if (UtilidadNeta >= 0)
            {
                resultado += $"\nDespués de restar los gastos operativos, la utilidad neta es de: {UtilidadNeta}.";
            }
            else
            {
                resultado += $"\nAlerta!!!!!!! después de restar los gastos operativos, la pérdida neta es de: - {Math.Abs(UtilidadNeta)} Advertencia: Es el momento de revisar mañana tu empresa podria estar en la quiebra.";
            }

            resultado += "\nTotales:";
            resultado += $"\nVentas: {TotalVentas}";
            resultado += $"\nCosto de Ventas: {TotalCostodeVentas}";

            if (UtilidadBruta >= 0)
            {
                resultado += $"\nUtilidad Bruta: {UtilidadBruta}";
            }
            else
            {
                resultado += $"\nPérdida Bruta: - {Math.Abs(UtilidadBruta)}";
            }
            resultado += $"\nGastos Operativos: {TotalGastosOperativos}";
           

            if (UtilidadNeta >= 0)
            {
                resultado += $"\nUtilidad Neta: {UtilidadNeta}";
            }
            else
            {
                resultado += $"\nPérdida Neta: - {Math.Abs(UtilidadNeta)}";
            }
            return resultado;
        }
    }
}
