using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using Negocio;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jardineria.Estadisticas.Oficinas
{
    public partial class Facturacion : Form
    {
        Tienda tienda = new Tienda();
        List<Series> datosSeries;
        List<Oficina> oficinas;
        List<Pedido> pedidos;

        public Facturacion()
        {
            InitializeComponent();
            oficinas = tienda.ListarOficinas();
        }

        private void Facturacion_Load(object sender, EventArgs e)
        {
            fecha.Format = DateTimePickerFormat.Custom;
            fecha.CustomFormat = "MM/yyyy";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            datosSeries = new List<Series>();
            int month = fecha.Value.Month;
            int year = fecha.Value.Year;
            decimal facturacion;

            pedidos = tienda.ListarPedidosPorMes(month, year);

            foreach (Oficina oficina in oficinas)
            {
                facturacion = 0;
                foreach (Pedido pedido in pedidos)
                {
                    Cliente cliente = tienda.BuscarCliente((int)pedido.Codigo_Cliente);
                    List<Detalle_Pedido> detalles = tienda.ListarDetallesPedido((int)pedido.Codigo_Pedido);
                    Empleado empleado = tienda.ListarEmpleado(cliente.codigo_empleado_rep_ventas.ToString());

                    if (empleado.Codigo_Oficina == oficina.Codigo_Oficina)
                    {
                        foreach (Detalle_Pedido detalle in detalles)
                        {
                            facturacion += (decimal)(detalle.cantidad * detalle.precio_unidad);
                        }
                    }
                }

                datosSeries.Add(new Series { Oficina = oficina.Region, Facturacion = facturacion });

            }

            graficaMes.Series.Clear();
            graficaMes.DataBindTable(datosSeries, "Oficina");
            graficaMes.Series[0].ChartType = SeriesChartType.Pie;

        }
        public class Series
        {
            public string Oficina { get; set; }
            public decimal Facturacion { get; set; }
        }
    }
}
