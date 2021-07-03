using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using Negocio;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jardineria.Estadisticas.Oficinas
{
    public partial class CantidadEmpleados : Form
    {
        Tienda tienda = new Tienda();
        List<Oficina> oficinas;
        List<Empleado> empleados;
        List<Series> datosSeries;
        public CantidadEmpleados()
        {
            InitializeComponent();
            oficinas = tienda.ListarOficinas();
            empleados = tienda.ListarEmpelados();
        }

        private void CantidadEmpleados_Load(object sender, EventArgs e)
        {
            datosSeries = new List<Series>();
            int cantidaClientes;

            foreach (Oficina oficina in oficinas)
            {
                cantidaClientes = 0;

                foreach (Empleado empleado in empleados)
                {
                    if (oficina.Codigo_Oficina == empleado.Codigo_Oficina)
                    {
                        cantidaClientes++;
                    }

                }

               
                datosSeries.Add(new Series { Oficina = oficina.Region, Empleados = cantidaClientes });
            }


            graficaMes.Series.Clear();
            graficaMes.DataBindTable(datosSeries, "Oficina");
            graficaMes.Series[0].ChartType = SeriesChartType.Pie;

        }

        public class Series
        {
            public string Oficina { get; set; }
            public int Empleados { get; set; }
        }
    }
}
