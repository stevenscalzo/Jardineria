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
using JardineriaWPF.Pedidos;
using Negocio;

namespace JardineriaWPF.Estadisticas
{
    /// <summary>
    /// Lógica de interacción para ListaPedidos.xaml
    /// </summary>
    public partial class ListaPedidos : Window
    {
        private ObservableCollection<PedidosInfor> ListaDePedidos;
        private CollectionViewSource VistaPedidos;
        Tienda tienda = new Tienda();
        List<Pedido> pedidos;
        List<Cliente> clientes;
        //Informe informe;
        public ListaPedidos()
        {
            InitializeComponent();
            pedidos = tienda.ObtenerListaPedidos();
            clientes = tienda.ListarClientes();

            VistaPedidos = (CollectionViewSource)FindResource("listaPedidos");

            ListaDePedidos = new ObservableCollection<PedidosInfor>();
            getPedidos();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VistaPedidos.Source = ListaDePedidos;
        }

        private void getPedidos()
        {
            foreach (Pedido pedido in pedidos)
            {
                ListaDePedidos.Add(new PedidosInfor(pedido.Codigo_Pedido, pedido.Fecha_Pedido, getNombre((int)pedido.Codigo_Cliente), (int)pedido.Codigo_Cliente));
            }
        }

        private string getNombre(int codigo)
        {
            Cliente cliente = clientes.Find(x => x.codigo_cliente == codigo);
            return cliente.nombre_cliente;
        }

        private void mostrar_informe_Click(object sender, RoutedEventArgs e)
        {
           // PedidosInfor elemento = (PedidosInfor)listaPedidos.SelectedItem;
          //  Informe informe = new Informe();
           // this.Close();
        }
    }
}
