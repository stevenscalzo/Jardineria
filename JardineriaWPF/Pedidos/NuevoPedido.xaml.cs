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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entidades;
using JardineriaWPF.Productos;
using JardineriaWPF.UserControls;
using Negocio;

namespace JardineriaWPF.Pedidos
{
    /// <summary>
    /// Lógica de interacción para NuevoPedido.xaml
    /// </summary>
    public partial class NuevoPedido : Window
    {
        Tienda tienda = new Tienda();
        private CollectionViewSource clientes;
        private ObservableCollection<Cliente> ListaClintes;
        private ObservableCollection<Producto> ListaProductos;
        private CollectionViewSource VistaProductos;
        private ObservableCollection<ProductoCarrito> Carrito;
        private CollectionViewSource VistaCarrito;
        private List<Producto> productos;
        private int? codigo_Pedido;
        private List<Detalle_Pedido> detalles;

        public NuevoPedido(int? codigo_Pedido)
        {
            InitializeComponent();
            clientes = (CollectionViewSource)FindResource("clientes");
            ListaClintes = new ObservableCollection<Cliente>();
            getClientes();

            productos = tienda.ListaProductos;

            VistaCarrito = (CollectionViewSource)FindResource("carrito");
            Carrito = new ObservableCollection<ProductoCarrito>();

            VistaProductos = (CollectionViewSource)FindResource("productos");
            ListaProductos = new ObservableCollection<Producto>();
            getProductos();

            clientes.Source = ListaClintes;

            this.codigo_Pedido = codigo_Pedido;
            if(this.codigo_Pedido != null)
            {
                detalles = tienda.ListarDetallesPedido((int)codigo_Pedido);
                Pedido pedido = tienda.ListarPedido((int)codigo_Pedido);
                titulo.Content = "Editar pedido";
                btn_Crear_Pedido.Content = "Guardar";
                fecha_Pedido.SelectedDate = pedido.Fecha_Pedido;
                fecha_Esperada.SelectedDate = pedido.Fecha_Esperada;
                fecha_Entrega.SelectedDate = pedido.Fecha_Entrega;
                comentarios.Text = pedido.Comentarios;

                foreach (Cliente item in cliente_Selector.Items)
                {
                    if (item.codigo_cliente == (int)pedido.Codigo_Cliente)
                    {
                        cliente_Selector.SelectedValue = item;
                        break;
                    }
                }

                foreach (ComboBoxItem item in estado_Selector.Items)
                    if (item.Content.ToString() == pedido.Estado)
                    {
                        estado_Selector.SelectedValue = item;
                        break;
                    }

                foreach (Detalle_Pedido detalle in detalles)
                {
                    Producto producto = tienda.ListarProducto(detalle.codigo_producto);
                    ProductoCarrito carrito = new ProductoCarrito(detalle.codigo_producto, producto.Nombre, (int)detalle.cantidad);
                    Carrito.Add(carrito);
                    CalcularPrecios(producto, (int)detalle.cantidad);
                }
            }
        }

        public void getProductos()
        {
            foreach (Producto producto in productos)
            {
                ListaProductos.Add(producto);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            VistaProductos.Source = ListaProductos;
            VistaCarrito.Source = Carrito;
        }

        public void getClientes()
        {
            foreach (Cliente cliente in tienda.ListarClientes())
            {
                ListaClintes.Add(cliente);
            }
        }

        private void Apregar_Producto(object sender, RoutedEventArgs e)
        {
            Producto prod = (Producto)productosSelector.SelectedItem;
            if (ValidarQty())
            {
                ProductoCarrito producto = new ProductoCarrito(prod.Codigo_Producto, prod.Nombre, Convert.ToInt32(qty.Text));
                Carrito.Add(producto);
                CalcularPrecios(prod, Convert.ToInt32(qty.Text));

            }
            else
                MessageBox.Show("La cantidad indica no es válida");
        }

        private void CalcularPrecios(Producto prod, int qty)
        {

            decimal total = Convert.ToDecimal(totalSinIva.Text) + (decimal)(prod.Precio_Venta * qty);
            decimal iva = (total / 100 * 21);

            totalSinIva.Text = total.ToString();
            totalIva.Text = iva.ToString();
            totalConIva.Text = (total + iva).ToString();
        }

        private bool ValidarQty()
        {
            bool valido = true;

            foreach (Char character in qty.Text.Trim().ToCharArray())
            {
                if (!Char.IsNumber(character))
                {
                    valido = false;
                }
            }

            return valido;
        }

        private void btn_Crear_Pedido_Click(object sender, RoutedEventArgs e)
        {
            Pedido pedido;
            if (btn_Crear_Pedido.Content.ToString() == "Crear Pedido")
            {
                ComboBoxItem item = (ComboBoxItem)estado_Selector.SelectedItem;
                pedido = new Pedido(tienda.ListaPedidos.Count + 1, fecha_Pedido.SelectedDate, fecha_Esperada.SelectedDate, fecha_Entrega.SelectedDate,
                    item.Content.ToString(), comentarios.Text, ((Cliente)cliente_Selector.SelectedItem).codigo_cliente);
                if (tienda.InsertarPedido(pedido))
                {
                    foreach (ProductoCarrito carrito in Carrito)
                    {
                        tienda.InsertarDetallePedido(tienda.ListarProducto(carrito.Codigo), pedido, carrito.Qty, 0);

                    }

                    MessageBox.Show("Pedido realizado correctamente.");

                    this.Close();
                } else
                    MessageBox.Show("No se pudo agregar el pedido");

            } else if(btn_Crear_Pedido.Content.ToString() == "Guardar")
            {
                pedido = new Pedido(this.codigo_Pedido, fecha_Pedido.SelectedDate, fecha_Esperada.SelectedDate, fecha_Entrega.SelectedDate,
                    estado_Selector.SelectedItem.ToString(), comentarios.Text, ((Cliente)cliente_Selector.SelectedItem).codigo_cliente);

                tienda.ActualizarPedido(pedido);
                List<Detalle_Pedido> detalles = tienda.ListarDetallesPedido((int)pedido.Codigo_Pedido);
                bool datosBorrados = true;

                foreach (Detalle_Pedido detalle in detalles)
                {
                    if (!tienda.EliminarDetallePedido((int)detalle.codigo_pedido, (int)detalle.numero_linea))
                        datosBorrados = false;
                }

                foreach (ProductoCarrito carrito in Carrito)
                {
                    tienda.InsertarDetallePedido(tienda.ListarProducto(carrito.Codigo), pedido, carrito.Qty, 0);
                }

                if (datosBorrados)
                    MessageBox.Show("Pedido actuazlizado correctamente.");
                else
                    MessageBox.Show("No se pudo editar el pedido");
            }

        }

        private void btn_Eliminar_Click(object sender, RoutedEventArgs e)
        {
            ProductoCarrito carrito = (ProductoCarrito)listaCarrito.SelectedItem;
            Carrito.Remove(carrito);
            VistaCarrito.Source = Carrito;
            CalcularPrecios(tienda.ListarProducto(carrito.Codigo), -carrito.Qty);
        }
    }
}
