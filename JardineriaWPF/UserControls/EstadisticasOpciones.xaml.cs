using JardineriaWPF.Estadisticas;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JardineriaWPF.UserControls
{
    /// <summary>
    /// Lógica de interacción para EstadisticasOpciones.xaml
    /// </summary>
    public partial class EstadisticasOpciones : UserControl
    {
        public EstadisticasOpciones()
        {
            InitializeComponent();
        }

        private void pedidos_Click(object sender, RoutedEventArgs e)
        {
            PorPedidos p = new PorPedidos();
            p.Show();
        }

        private void oficina_Click(object sender, RoutedEventArgs e)
        {
            PorOficina p = new PorOficina();
            p.Show();
        }
    }
}
