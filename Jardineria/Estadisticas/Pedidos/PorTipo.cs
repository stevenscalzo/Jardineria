using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using Negocio;
using System.Windows.Forms.DataVisualization.Charting;

namespace Jardineria
{
    public partial class PorTipo : Form
    {
        Tienda tienda = new Tienda();
        List<Pedido> pedidos;
        List<Gama_Producto> gamas;
        public PorTipo()
        {
            InitializeComponent();
            gamas = tienda.ListarGama();
        }

        private void FormEstadisticaPorTipo_Load(object sender, EventArgs e)
        {
            fecha.Format = DateTimePickerFormat.Custom;
            fecha.CustomFormat = "MM/yyyy";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Series> datosSeries = new List<Series>();
            int month = fecha.Value.Month;
            int year = fecha.Value.Year;
            int cantidaTipos;

            pedidos = tienda.ListarPedidosPorMes(month, year);
            
            foreach(Gama_Producto gama in gamas)
            {
                cantidaTipos = 0;

                foreach (Pedido pedido in pedidos)
                {
                    List<Detalle_Pedido> detalles = tienda.ListarDetallesPedido((int)pedido.Codigo_Pedido);

                    foreach (Detalle_Pedido detalle in detalles)
                    {
                        Producto producto = tienda.ListarProducto(detalle.codigo_producto);
                        if (producto.Gama == gama.Gama)
                        {
                            cantidaTipos++;
                        }
                    }
                }

                
                datosSeries.Add(new Series { Gama = gama.Gama + ": " + cantidaTipos, Cantidad = cantidaTipos });
            }

            graficaMes.Series.Clear();
            graficaMes.DataBindTable(datosSeries, "Gama");
            graficaMes.Series[0].ChartType = SeriesChartType.Pie;
        }

        public class Series
        {
            public string Gama { get; set; }
            public int Cantidad { get; set; }
        }
    }
}
