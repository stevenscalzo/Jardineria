using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Jardineria
{
    public partial class PorDia : Form
    {
        Tienda tienda = new Tienda();
        List<Pedido> pedidos;

        public PorDia()
        {
            InitializeComponent();
        }

        private void FormEstadisticaDelMes_Load(object sender, EventArgs e)
        {
            fecha.Format = DateTimePickerFormat.Custom;
            fecha.CustomFormat = "MM/yyyy";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Series> datosSeries = new List<Series>();

            int month = fecha.Value.Month;
            int year = fecha.Value.Year;
            int days = System.DateTime.DaysInMonth(year, month);
            int cantidaPedidos;

            pedidos = tienda.ListarPedidosPorMes(month, year);
           
            for(int i = 1; i <= days; i++)
            {
                cantidaPedidos = 0;
                foreach (Pedido pedido in pedidos)
                {
                    if (pedido.Fecha_Pedido.Value.Day == i)
                    {
                        cantidaPedidos++;
                    }
                }

                datosSeries.Add(new Series { Dia = i.ToString(), Pedidos = cantidaPedidos });
            }

            graficaMes.Series.Clear();
            graficaMes.DataBindTable(datosSeries, "Dia");
        }

        public class Series
        {
            public string Dia { get; set; }
            public int Pedidos { get; set; }
        }
    }
}
