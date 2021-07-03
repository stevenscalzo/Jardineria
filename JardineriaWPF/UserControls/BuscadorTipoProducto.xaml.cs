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
using JardineriaWPF.UserControls;
using Negocio;

namespace JardineriaWPF.UserControls
{
    /// <summary>
    /// Lógica de interacción para BuscadorTipoProducto.xaml
    /// </summary>
    public partial class BuscadorTipoProducto : UserControl
    {
        Tienda tienda = new Tienda();
        List<Gama_Producto> gamas;
        private CollectionViewSource tipoProductos;
        private ObservableCollection<Gama_Producto> ListaGamas;
        public BuscadorTipoProducto()
        {
            InitializeComponent();
            tipoProductos = (CollectionViewSource)FindResource("tipoProductos");
            ListaGamas = new ObservableCollection<Gama_Producto>();
            gamas = tienda.ListarGama();
            getGamas();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tipoProductos.Source = ListaGamas;
        }

        public void getGamas()
        {
            foreach (Gama_Producto gama in gamas)
            {
                ListaGamas.Add(gama);
            }
        }
    }
}
