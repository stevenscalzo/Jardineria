using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using Negocio;
using System.Drawing;

namespace Jardineria.Estadisticas.Productos
{
    public partial class StockBajo : Form
    {
        Tienda tienda = new Tienda();
        List<Producto> productos;
        public StockBajo()
        {
            InitializeComponent();
            this.MaximumSize = new Size(1200, 700);
            this.MinimumSize = new Size(1200, 700);
            productos = tienda.ObtenerListaProductos();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Series> datosSeries = new List<Series>();

            foreach(Producto producto in productos)            
                if(producto.Cantidad_En_Stock <= stock.Value)               
                    datosSeries.Add(new Series { Producto = producto.Nombre, Stock = (short)producto.Cantidad_En_Stock });

            graficaStock.Series.Clear();
            graficaStock.DataBindTable(datosSeries, "Producto");
        }

        public class Series
        {
            public string Producto { get; set; }
            public short Stock { get; set; }
        }
    }
}
