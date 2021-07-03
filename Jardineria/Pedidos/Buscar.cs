using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Jardineria.Informes;
using Negocio;

namespace Jardineria
{
    public partial class Buscar : Form
    {
        Tienda tienda = new Tienda();
        List<Pedido> pedidos;
        string accion;
        Detalles formPedidos;
        public Facturas factura;

        public Buscar(string accion = null)
        {
            this.accion = accion;
            InitializeComponent();
            pedidos = tienda.ObtenerListaPedidos();

            foreach (Cliente cliente in tienda.ListarClientes())
            {
                txtCliente.Items.Add(cliente.nombre_cliente);
            }
        }

        private void BuscarPedidos_Load(object sender, EventArgs e)
        {
            foreach (Pedido pedido in pedidos)
            {
                this.dataGridView1.Rows.Add(tienda.BuscarCliente((int)pedido.Codigo_Cliente).nombre_cliente, pedido.Fecha_Pedido);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Pedido> pedidosAuxiliar = new List<Pedido>();
            foreach (Pedido pedido in pedidos)
            {
                if (tienda.BuscarCliente((int)pedido.Codigo_Cliente).nombre_cliente.ToLower().Contains(txtCliente.Text.ToLower()))
                {
                    pedidosAuxiliar.Add(pedido);
                }
            }

            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();

            foreach (Pedido pedido in pedidosAuxiliar)
            {
                this.dataGridView1.Rows.Add(tienda.BuscarCliente((int)pedido.Codigo_Cliente).nombre_cliente, pedido.Fecha_Pedido);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<Pedido> pedidosAuxiliar = new List<Pedido>();
            foreach (Pedido pedido in pedidos)
            {
                if (pedido.Fecha_Pedido.ToString().Contains(txtFecha.Value.ToShortDateString()))
                {
                    pedidosAuxiliar.Add(pedido);
                }
            }

            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();

            foreach (Pedido pedido in pedidosAuxiliar)
            {
                this.dataGridView1.Rows.Add(tienda.BuscarCliente((int)pedido.Codigo_Cliente).nombre_cliente, pedido.Fecha_Pedido);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (accion == "eliminar")
            {
                string nombre = this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                List<Pedido> pedidos = tienda.ListarPedidos((int)tienda.ListarCliente(nombre).codigo_cliente);
                string fecha = this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string[] fechaDividida = fecha.Split('/');
                string[] anyo = fechaDividida[2].Split(' ');

                DateTime date = new DateTime(Convert.ToInt32(anyo[0]), Convert.ToInt32(fechaDividida[1]),
                    Convert.ToInt32(fechaDividida[0]));

                Pedido pedido = pedidos.Find(ped => ped.Fecha_Pedido == date);

                if (DialogResult.Yes == MessageBox.Show("¿Quieres borrar los datos del pedido?", "Borrar Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    List<Detalle_Pedido> detalles = tienda.ListarDetallesPedido((int)pedido.Codigo_Pedido);
                    bool datosBorrados = true;

                    foreach (Detalle_Pedido detalle in detalles)
                    {
                        if (!tienda.EliminarDetallePedido((int)detalle.codigo_pedido, (int)detalle.numero_linea))
                            datosBorrados = false;
                    }
                    if (datosBorrados)
                        if (!tienda.EliminarPedido((int)pedido.Codigo_Pedido))
                            datosBorrados = false;

                    if (datosBorrados)
                        MessageBox.Show("Pedido eliminado");
                    else
                        MessageBox.Show("No se pudo eliminar el pedido");
                }

                this.Close();
                
            }
            else if (accion == "factura")
            {
                factura = new Facturas(this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                factura.MdiParent = this.MdiParent;
                factura.Show();
            }
            else
            {
                formPedidos = new Detalles("editar", this.dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),
                    this.dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                formPedidos.MdiParent = this.MdiParent;
                formPedidos.Show();
                this.Close();
            }
        }
    }
}
