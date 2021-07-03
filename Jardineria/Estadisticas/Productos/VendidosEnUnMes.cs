using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using Negocio;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jardineria.Estadisticas.Productos
{
    public partial class VendidosEnUnMes : Form
    {
        Tienda tienda = new Tienda();
        List<Pedido> pedidos;
        List<Producto> productos;
        public VendidosEnUnMes()
        {
            InitializeComponent();
            productos = tienda.ObtenerListaProductos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Series> datosSeries = new List<Series>();
            int month = fecha.Value.Month;
            int year = fecha.Value.Year;
            int cantidaProductos;

            pedidos = tienda.ListarPedidosPorMes(month, year);

            foreach(Producto producto in productos)
            {
                cantidaProductos = 0;

                foreach (Pedido pedido in pedidos)
                {
                    List<Detalle_Pedido> detalles = tienda.ListarDetallesPedido((int)pedido.Codigo_Pedido);

                    foreach (Detalle_Pedido detalle in detalles)
                    {                   
                        if (detalle.codigo_producto == producto.Codigo_Producto)
                        {
                            cantidaProductos++;
                        }
                    }
                }

                
                datosSeries.Add(new Series { Producto = producto.Nombre + ": " + cantidaProductos, Cantidad = cantidaProductos });

            }

            graficaVendido.Series.Clear();
            graficaVendido.DataBindTable(datosSeries, "Producto");
            graficaVendido.Series[0].ChartType = SeriesChartType.Pie;
        }

        private void VendidosEnUnMes_Load(object sender, EventArgs e)
        {
            fecha.Format = DateTimePickerFormat.Custom;
            fecha.CustomFormat = "MM/yyyy";
        }

        public class Series
        {
            public string Producto { get; set; }
            public int Cantidad { get; set; }
        }
    }
}
