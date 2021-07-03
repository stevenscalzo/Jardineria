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
using JardineriaWPF.UserControls;
using Negocio;

namespace JardineriaWPF.Productos
{
    /// <summary>
    /// Lógica de interacción para ConsultarProductos.xaml
    /// </summary>
    public partial class ConsultarProductos : Window
    {
        private ObservableCollection<Producto> ListaProductos;
        private CollectionViewSource MiVista;
        private List<Producto> productos;
        List<Gama_Producto> gamas;
        private CollectionViewSource tipoProductos;
        private ObservableCollection<Gama_Producto> ListaGamas;
        private CollectionViewSource productoVista;
        Tienda tienda = new Tienda();
        String gamaSelected;
        String productoSelected;

        public ConsultarProductos()
        {
            InitializeComponent();

            productos = tienda.ListaProductos;

            MiVista = (CollectionViewSource)FindResource("listaProductos");
            ListaProductos = new ObservableCollection<Producto>();
            getProductos();

            tipoProductos = (CollectionViewSource)FindResource("tipoProductos");
            ListaGamas = new ObservableCollection<Gama_Producto>();
            gamas = tienda.ListarGama();
            getGamas();

            productoVista = (CollectionViewSource)FindResource("producto");
            
        }

        public void getProductos()
        {
            foreach(Producto producto in productos)
            {
                ListaProductos.Add(producto);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tipoProductos.Source = ListaGamas;
        }

        public void addProductos()
        {
            MiVista.Source = ListaProductos;
            productoVista.Source = ListaProductos;
        }

        public void getGamas()
        {
            foreach (Gama_Producto gama in gamas)
            {
                ListaGamas.Add(gama);
            }
        }

        private void gamaPedidos_Selected(object sender, SelectionChangedEventArgs e)
        {

            if (gamaPedidos != null)
            {
                String [] gamaS = gamaPedidos.SelectedItem.ToString().Split(' ');
                gamaSelected = gamaS[0];
                addProductos();
                MiVista.Filter += filtrarPorGama;
                if (listaProductos.Items.Count > 0)
                    listaProductos.SelectedItem = listaProductos.Items.GetItemAt(0);
            }
        }

        public void filtrarPorGama(object sender, FilterEventArgs f)
        {
            Producto producto = (Producto)f.Item;

            if (producto != null)
            {
                if (producto.Gama == gamaSelected)

                    f.Accepted = true;
                else
                    f.Accepted = false;

            }
        }

        private void listaProductos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listaProductos != null)
            {
                if (listaProductos.Items.Count > 0)
                {
                    String[] productos = listaProductos.SelectedItem.ToString().Split(' ');
                    productoSelected = productos[0];
                    productoVista.Filter += filtrarPorNombre;

                } else
                {
                    productoVista.Filter += listaVacia;
                }
            }
        }

        public void listaVacia(object sender, FilterEventArgs f)
        {
            Producto producto = (Producto)f.Item;
            if (producto != null)
            {
                f.Accepted = false;
            }
        }

        public void filtrarPorNombre(object sender, FilterEventArgs f)
        {
            Producto producto = (Producto)f.Item;

            if (producto != null)
            {
                if (producto.Codigo_Producto == productoSelected)

                    f.Accepted = true;
                else
                    f.Accepted = false;

            }
        }
    }
}
