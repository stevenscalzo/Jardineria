﻿using JardineriaWPF.Productos;
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
    /// Lógica de interacción para ProductosOpciones.xaml
    /// </summary>
    public partial class ProductosOpciones : UserControl
    {
        ConsultarProductos consulta;
        public ProductosOpciones()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            consulta = new ConsultarProductos();
            consulta.Show();
        }
    }
}
