using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Entidades;
using Negocio;
using LiveCharts.Wpf;
using System.Globalization;

namespace JardineriaWPF.Estadisticas
{
    /// <summary>
    /// Lógica de interacción para PorPedidos.xaml
    /// </summary>
    public partial class PorPedidos : Window
    {
        SeriesCollection serie1;
        SeriesCollection serie2;
        Tienda tienda = new Tienda();
        List<Pedido> pedidos;
        List<Producto> listaProductos;
        List<Gama_Producto> gamas;
        List<Detalle_Pedido> detallesPedidos;
        CultureInfo ci = new CultureInfo("Es-Es");
        public PorPedidos()
        {
            InitializeComponent();
            serie1 = new SeriesCollection();
            serie2 = new SeriesCollection();
            gamas = tienda.ListarGama();
            detallesPedidos = tienda.ListarDetallesPedidos();
            listaProductos = tienda.ObtenerListaProductos();
        }

        private void filtro_Fecha_Click(object sender, RoutedEventArgs e)
        {
            setGrafico1();
            setGrafico2();
        }

        private void setGrafico1()
        {
            serie1.Clear();
            DateTime date = (DateTime)fecha.SelectedDate;
            int month = date.Month;
            int year = date.Year;
            int days = System.DateTime.DaysInMonth(year, month);
            g1_eje_x.MaxValue = days;
            int cantidaPedidos;
            int maxPedidos = 0;
            ChartValues<double> values = new ChartValues<double>();
            string[] labels = new string[days];

            pedidos = tienda.ListarPedidosPorMes(month, year);

            for (int i = 0; i < days; i++)
            {
                cantidaPedidos = 0;
                foreach (Pedido pedido in pedidos)
                {
                    if (pedido.Fecha_Pedido.Value.Day == i)
                    {
                        cantidaPedidos++;
                    }
                }
                values.Add(cantidaPedidos);
                labels[i] = i + "";
                if (cantidaPedidos > maxPedidos)
                    maxPedidos = cantidaPedidos;

            }

            serie1.Add(new ColumnSeries
            {
                Title = ci.DateTimeFormat.GetMonthName(month),
                Values = values
            });

            g1_eje_y.MaxValue = maxPedidos + 2;
            g1_eje_x.Labels = labels;
            grafico1.Series = serie1;
        }

        private void setGrafico2()
        {
            serie2.Clear();
            int cantidaTipos;
            foreach (Gama_Producto gama in gamas)
            {
                cantidaTipos = 0;

                foreach (Pedido pedido in pedidos)
                {
                    List<Detalle_Pedido> detalles = detallesPedidos.FindAll(e => e.codigo_pedido == pedido.Codigo_Pedido);

                    foreach (Detalle_Pedido detalle in detalles)
                    {
                        Producto producto = listaProductos.Find(e => e.Codigo_Producto == detalle.codigo_producto);
                        if (producto.Gama == gama.Gama)
                        {
                            cantidaTipos++;
                        }
                    }
                }

                serie2.Add(new PieSeries
                {
                    Title = gama.Gama,
                    Values = new ChartValues<double> { cantidaTipos }
                });
            }

            grafico2.Series = serie2;
        }
    }
}
