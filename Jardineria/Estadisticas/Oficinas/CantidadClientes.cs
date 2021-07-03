using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using Negocio;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jardineria.Estadisticas.Oficinas
{
    public partial class CantidadClientes : Form
    {
        Tienda tienda = new Tienda();
        List<Cliente> clientes;
        List<Oficina> oficinas;
        List<Empleado> empleados;
        List<Series> datosSeries;
        public CantidadClientes()
        {
            InitializeComponent();
            clientes = tienda.ListarClientes();
            oficinas = tienda.ListarOficinas();
            empleados = tienda.ListarEmpelados();
        }

        private void CantidadClientes_Load(object sender, EventArgs e)
        {
            datosSeries = new List<Series>();
            int cantidaClientes;

            foreach (Oficina oficina in oficinas)              
            {
                cantidaClientes = 0;

                foreach (Empleado empleado in empleados)
                {
                    if(oficina.Codigo_Oficina == empleado.Codigo_Oficina)
                    {
                        foreach (Cliente cliente in clientes)                
                            if (empleado.Codigo_Empleado == cliente.codigo_empleado_rep_ventas)
                                cantidaClientes++;
                    }

                }

               
                    datosSeries.Add(new Series { Oficina = oficina.Region, Clientes = cantidaClientes });
            }


            graficaMes.Series.Clear();
            graficaMes.DataBindTable(datosSeries, "Oficina");
            graficaMes.Series[0].ChartType = SeriesChartType.Pie;
        }

        public class Series
        {
            public string Oficina { get; set; }
            public int Clientes { get; set; }
        }
    }
}
