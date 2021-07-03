using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace JardineriaWPF.Pedidos
{
    /// <summary>
    /// Lógica de interacción para BuscadorPedido.xaml
    /// </summary>
    public partial class BuscadorPedido : Window
    {
        private ObservableCollection<PedidosInfor> ListaPedidos;
        private CollectionViewSource VistaPedidos;
        Tienda tienda = new Tienda();
        List<Pedido> pedidos;
        List<Cliente> clientes;

        public BuscadorPedido()
        {
            InitializeComponent();

            pedidos = tienda.ObtenerListaPedidos();
            clientes = tienda.ListarClientes();

            VistaPedidos = (CollectionViewSource)FindResource("listaPedidos");

            ListaPedidos = new ObservableCollection<PedidosInfor>();
            getPedidos();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VistaPedidos.Source = ListaPedidos;
        }
        private void getPedidos()
        {
            foreach(Pedido pedido in pedidos)
            {
                ListaPedidos.Add(new PedidosInfor(pedido.Codigo_Pedido, pedido.Fecha_Pedido, getNombre((int)pedido.Codigo_Cliente), (int)pedido.Codigo_Cliente));
            }
        }

        private string getNombre(int codigo)
        {
            Cliente cliente = clientes.Find(x => x.codigo_cliente == codigo);
            return cliente.nombre_cliente;
        }
        private void filtro_Fecha_Click(object sender, RoutedEventArgs e)
        {
            VistaPedidos.Filter -= FiltrarPorCliente;
            VistaPedidos.Filter += FiltrarPorFecha;
        }

        private void FiltrarPorCliente(object sender, FilterEventArgs e)
        {
            PedidosInfor elemento = (PedidosInfor)e.Item;
            if (elemento != null)
            {
                if (elemento.Nombre.Contains(nombre_Cliente.Text))
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }

        }

        private void FiltrarPorFecha(object sender, FilterEventArgs e)
        {
            PedidosInfor elemento = (PedidosInfor)e.Item;
            if (elemento != null)
            {
                if (elemento.Fecha_Pedido == fecha_Pedido.SelectedDate)
                    e.Accepted = true;
                else
                    e.Accepted = false;
            }

        }

        private void filtro_Nombre_Click(object sender, RoutedEventArgs e)
        {
            VistaPedidos.Filter -= FiltrarPorFecha;
            VistaPedidos.Filter += FiltrarPorCliente;
        }

        private void borrar_filtros_Click(object sender, RoutedEventArgs e)
        {
            VistaPedidos.Filter -= FiltrarPorCliente;
            VistaPedidos.Filter -= FiltrarPorFecha;
        }

        private void mostrar_Datos_Click(object sender, RoutedEventArgs e)
        {
            PedidosInfor elemento = (PedidosInfor)listaPedidos.SelectedItem;

            NuevoPedido pedido = new NuevoPedido(elemento.Codigo_Pedido);
            pedido.Show();
            this.Close();
        }
    }
}
