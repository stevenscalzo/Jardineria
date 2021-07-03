using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using System.Windows.Threading;
using Entidades;
using JardineriaWPF.Estadisticas;
using JardineriaWPF.Pedidos;
using JardineriaWPF.UserControls;
using Negocio;

namespace JardineriaWPF.Main
{
    /// <summary>
    /// Lógica de interacción para Principal.xaml
    /// </summary>
    public partial class Principal : Window
    {
        Tienda tienda = new Tienda();
        DateTime hoy = DateTime.Now;
        CultureInfo ci = new CultureInfo("Es-Es");
        Empleado empleado;
        private CollectionViewSource ultimosPedidos10;
        private ObservableCollection<Pedido> ListaPedidos;
        List<Pedido> pedidos;
        string correo;
        public Principal(string correo)
        {
            InitializeComponent();
            this.correo = correo;

            FillData(correo);
            getUltimosPedidos();
        }

        private void FillData(String correo)
        {
            empleado = tienda.ListarEmpleado(correo);
            Correo.Content = empleado.Email;
            Cargo.Content = empleado.Puesto;
            Usuario.Content = empleado.NombreCompleto;
            pedidos = tienda.ObtenerListaPedidos();
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();
            numClientes.Content = tienda.ListarClientes().Count;
            numEmpleados.Content = tienda.ListarEmpelados().Count;
            numProductos.Content = tienda.ObtenerListaProductos().Count;
            numPedidos.Content = tienda.ObtenerListaPedidos().Count;
            numCaja.Content = getCajaDiaria() + "€";
            fecha.Content = ci.DateTimeFormat.GetDayName(hoy.DayOfWeek) + ", " + hoy.Day + " de " + ci.DateTimeFormat.GetMonthName(hoy.Month) +
                " de " + hoy.Year;
            ultimosPedidos10 = (CollectionViewSource)FindResource("ultimosPedidos10");
            ListaPedidos = new ObservableCollection<Pedido>();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ultimosPedidos10.Source = ListaPedidos;
        }

        public void getUltimosPedidos()
        {
            pedidos.Sort((x, y) => DateTime.Compare((DateTime)x.Fecha_Pedido, (DateTime)y.Fecha_Pedido));
            pedidos.Reverse();
            ListaPedidos.Add(new Pedido());

            for (int i = 0; i < 10; i++)
            {
                ListaPedidos.Add(pedidos[i]);
            }

        }
        void timer_Tick(object sender, EventArgs e)
        {
            hora.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        public float getCajaDiaria()
        {
            float caja = 0;
            foreach (Pedido pedido in pedidos)
            {
                if (pedido.Fecha_Pedido == DateTime.Now)
                {
                    foreach (Detalle_Pedido detalle in tienda.ListarDetallesPedido((int)pedido.Codigo_Pedido))
                    {
                        caja += (float)(detalle.precio_unidad * detalle.cantidad);
                    }
                }
            }

            return caja;
        }

        private void minimazar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void maximizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btn_Producto_Click(object sender, RoutedEventArgs e)
        {
            opcionesMenu.Width = 140;
            panel.Width = 90;
            gridPrincipal.Width = 935;
            panelFichas.Width = 935;
            panelTop.Width = 935;
            panelData.Width = 935;
            panelTopContent.Width = 875;

            Thickness myThickness = new Thickness();
            myThickness.Left = 0;
            myThickness.Top = 240;
            myThickness.Right = 0;
            myThickness.Bottom = 0;
            opcionesMenu.Margin = myThickness;

            ProductosOpciones hd1 = new ProductosOpciones();
            opcionesMenu.Children.Clear();
            opcionesMenu.Children.Add(hd1);
        }

        private void tog_bn_Click(object sender, RoutedEventArgs e)
        {
            if (opcionesMenu.Children.Count > 0)
            {
                opcionesMenu.Children.Clear();
                opcionesMenu.Width = 0;
                gridPrincipal.Width = 1155;
                panelFichas.Width = 1155;
                panelTop.Width = 1155;
                panelData.Width = 1155;
                panelTopContent.Width = 1095;
                panel.Width = 90;

            }
        }

        private void btn_Pedido_Click(object sender, RoutedEventArgs e)
        {
            opcionesMenu.Width = 140;
            panel.Width = 90;
            gridPrincipal.Width = 935;
            panelFichas.Width = 935;
            panelTop.Width = 935;
            panelData.Width = 935;
            panelTopContent.Width = 875;

            Thickness myThickness = new Thickness();
            myThickness.Left = 0;
            myThickness.Top = 260;
            myThickness.Right = 0;
            myThickness.Bottom = 0;
            opcionesMenu.Margin = myThickness;

            PedidosOpciones hd1 = new PedidosOpciones();
            opcionesMenu.Children.Clear();
            opcionesMenu.Children.Add(hd1);
        }

        private void btn_Clientes_Click(object sender, RoutedEventArgs e)
        {
            opcionesMenu.Width = 140;
            panel.Width = 90;
            gridPrincipal.Width = 935;
            panelFichas.Width = 935;
            panelTop.Width = 935;
            panelData.Width = 935;
            panelTopContent.Width = 875;

            Thickness myThickness = new Thickness();
            myThickness.Left = 0;
            myThickness.Top = 300;
            myThickness.Right = 0;
            myThickness.Bottom = 0;
            opcionesMenu.Margin = myThickness;

            ClientesOpciones hd1 = new ClientesOpciones(correo);
            opcionesMenu.Children.Clear();
            opcionesMenu.Children.Add(hd1);
        }

        private void btn_Estadísticas_Click(object sender, RoutedEventArgs e)
        {
            opcionesMenu.Width = 140;
            panel.Width = 90;
            gridPrincipal.Width = 935;
            panelFichas.Width = 935;
            panelTop.Width = 935;
            panelData.Width = 935;
            panelTopContent.Width = 875;

            Thickness myThickness = new Thickness();
            myThickness.Left = 0;
            myThickness.Top = 400;
            myThickness.Right = 0;
            myThickness.Bottom = 0;
            opcionesMenu.Margin = myThickness;

            EstadisticasOpciones hd1 = new EstadisticasOpciones();
            opcionesMenu.Children.Clear();
            opcionesMenu.Children.Add(hd1);
        }

        private void ultimosPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pedido elemento = (Pedido)ultimosPedidos.SelectedItem;
            if(elemento.Codigo_Cliente != null) { 


                NuevoPedido pedido = new NuevoPedido(elemento.Codigo_Pedido);
                pedido.Show();
                this.WindowState = WindowState.Minimized;
            
            }
        }

        private void informes_Click(object sender, RoutedEventArgs e)
        {
            //ListaPedidos lista = new ListaPedidos();
           // lista.Show();
        }
    }
}
