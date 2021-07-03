using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using Microsoft.Reporting.WinForms;
using Negocio;

namespace Jardineria.Informes
{
    public partial class Facturas : Form
    {
        public string nombre;
        public string fecha;

        public Facturas(string nombre, string fecha)
        {
            InitializeComponent();
            this.nombre = nombre;
            this.fecha = fecha;
        }

        private void Facturas_Load(object sender, EventArgs e)
        {

            Tienda tienda = new Tienda();
            Cliente cliente = tienda.ListarCliente(nombre);

            string[] fechaDividida = fecha.Split('/');
            string[] anyo = fechaDividida[2].Split(' ');
            DateTime date = new DateTime(Convert.ToInt32(anyo[0]), Convert.ToInt32(fechaDividida[1]),
                    Convert.ToInt32(fechaDividida[0]));
            List<Pedido> pedidos = tienda.ListarPedidos((int)tienda.ListarCliente(nombre).codigo_cliente);
            Pedido pedido = pedidos.Find(ped => ped.Fecha_Pedido == date);

            List<Detalle_Pedido> detalles = tienda.ListarDetallesPedido((int)pedido.Codigo_Pedido);

            decimal total = 0;

            foreach (Detalle_Pedido detalle in detalles)
                total += (decimal) (detalle.cantidad * detalle.precio_unidad);

            reportViewer1.LocalReport.DataSources.Clear();



            ReportParameter[] data = new ReportParameter[]
            {
                new ReportParameter("nombreCliente", cliente.nombre_cliente),
                new ReportParameter("telefono", cliente.telefono),
                new ReportParameter("codigoPedido", pedido.Codigo_Pedido.ToString()),
                new ReportParameter("direccion", cliente.linea_direccion1 + cliente.linea_direccion2),
                new ReportParameter("fecha", pedido.Fecha_Pedido.ToString()),
                new ReportParameter("total", total.ToString())


            };

            
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("detalle_pedido", detalles));
            
            reportViewer1.LocalReport.SetParameters(data);

            reportViewer1.RefreshReport();
        }
    }
}
