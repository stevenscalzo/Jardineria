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
using Negocio;

namespace Jardineria
{
    public partial class Detalles : Form
    {
        Tienda tienda = new Tienda();
        string accion;
        int codigoPedido = 0;

        public Detalles(string accion = null, string nombreCliente = null, string fecha = null)
        {
            InitializeComponent();
            this.accion = accion;
            this.MaximumSize = new Size(790, 570);
            this.MinimumSize = new Size(790, 570);

            estadoPedido.Items.Add("Entregado");
            estadoPedido.Items.Add("Rechazado");
            estadoPedido.Items.Add("Pendiente");


            foreach (Cliente cliente in tienda.ListarClientes())
            {
                comboBox1.Items.Add(cliente.nombre_cliente);
            }

            foreach (Producto producto in tienda.ObtenerListaProductos())
            {
                comboBox2.Items.Add(producto.Nombre);
            }

            if (accion == "editar")
            {
                string[] fechaDividida = fecha.Split('/');
                string[] anyo = fechaDividida[2].Split(' ');

                BotonFinalizar.Text = "Editar Pedido";

                DateTime date = new DateTime(Convert.ToInt32(anyo[0]), Convert.ToInt32(fechaDividida[1]),
                    Convert.ToInt32(fechaDividida[0]));

                List<Pedido> pedidos = tienda.ListarPedidos((int)tienda.ListarCliente(nombreCliente).codigo_cliente);
                Pedido pedido = pedidos.Find(ped => ped.Fecha_Pedido == date);
                this.codigoPedido = (int)pedido.Codigo_Pedido;
                List<Detalle_Pedido> detalles = tienda.ListarDetallesPedido((int)pedido.Codigo_Pedido);
                List<String> productosPedido = new List<string>();
                List<int> productosPedidoCantidad = new List<int>();

                foreach (Detalle_Pedido detallePedido in detalles)
                {
                    int cantidad = (int)detallePedido.cantidad;
                    productosPedido.Add(tienda.ListarProducto(detallePedido.codigo_producto).Nombre);
                    productosPedidoCantidad.Add(cantidad);

                }

                comboBox1.SelectedIndex = comboBox1.FindStringExact(nombreCliente);
                fechaEsperada.Value = date;
                estadoPedido.SelectedItem = pedido.Estado;
                comentarios.Text = pedido.Comentarios;
                FormPedidosTitle.Text = "Editar pedido";
                for (int i = 0; i < productosPedido.Count; i++)
                {
                    dataGridView1.Rows.Add(productosPedido[i], productosPedidoCantidad[i]);
                }

                CalcularPrecios();
            } 
        }

        private void BotonAgregar_Click(object sender, EventArgs e)
        {
            ToolTip TTIP = new ToolTip(); ;
            bool valProducto, valQty;

            ValidarQty(out TTIP, out valQty);
            ValidarProducto(out TTIP, out valProducto);

            if (valProducto && valQty)
            {
                this.dataGridView1.Rows.Add(comboBox2.Text, InputCantidad.Value.ToString());
                CalcularPrecios();
            }
        }

        private void CalcularPrecios()
        {
            List<string> nombreProductos = new List<string>();
            List<int> cantidadProductos = new List<int>();

            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells[0].Value != null)
                {
                    nombreProductos.Add(item.Cells[0].Value.ToString());
                    cantidadProductos.Add(Convert.ToInt32(item.Cells[1].Value));
                }
            }

            List<string> precios = tienda.CalcularPrecios(nombreProductos, cantidadProductos);

            LabelTotal.Text = precios[0];
            LabelIva.Text = precios[1];
            LavelTotalIva.Text = precios[2];
        }

        private void ValidarQty(out ToolTip TTIP, out bool valQty)
        {
            TTIP = new ToolTip();
            string cadenaErrores = "";

            if (InputCantidad.Text.Length <= 0)
            {
                cadenaErrores = "El campo Cantidad no puede estar vacío" + Environment.NewLine;

                //Mejorar la usabilidad mostrando el error
                InputCantidad.BackColor = Color.LightCoral;
                TTIP.SetToolTip(InputCantidad, cadenaErrores);
                errorProvider2.SetError(InputCantidad, cadenaErrores);
                valQty = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                cadenaErrores = "";
                InputCantidad.BackColor = Color.LightGreen;
                TTIP.SetToolTip(InputCantidad, "");
                errorProvider2.SetError(InputCantidad, "");
                valQty = true;
            }
        }

        private void ValidarProducto(out ToolTip TTIP, out bool valProducto)
        {
            TTIP = new ToolTip();
            string cadenaErrores = "";
            valProducto = false;

            if (comboBox2.Text.Length <= 0)
            {
                cadenaErrores = "El campo Productos no puede estar vacío" + Environment.NewLine;

                //Mejorar la usabilidad mostrando el error
                comboBox2.BackColor = Color.LightCoral;
                TTIP.SetToolTip(comboBox2, cadenaErrores);
                errorProvider1.SetError(comboBox2, cadenaErrores);
                valProducto = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                comboBox2.BackColor = Color.LightGreen;
                TTIP.SetToolTip(comboBox2, "");
                errorProvider1.SetError(comboBox2, "");
                valProducto = true;
            }
        }

        private void FormPedidos_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn c in dataGridView1.Columns)
                if (c.Name != "Cantidad") c.ReadOnly = true;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1.CurrentRow.Cells["Cantidad"];
            dataGridView1.BeginEdit(true);
        }

        private void BontonFinalizar_Click(object sender, EventArgs e)
        {
            ToolTip TTIP = new ToolTip(); ;
            bool valCliente, valProductos;

            ValidaCliente(out TTIP, out valCliente);
            ValidarProductos(out TTIP, out valProductos);

            if (valCliente && valProductos)
            {
                Pedido pedido;
                if(accion == "editar")
                {
                    pedido = new Pedido(this.codigoPedido, fechaEsperada.Value, null, null,
                    estadoPedido.Text, comentarios.Text, tienda.ListarCliente(comboBox1.Text).codigo_cliente);
                    tienda.ActualizarPedido(pedido);
                    List<Detalle_Pedido> detalles = tienda.ListarDetallesPedido((int)pedido.Codigo_Pedido);
                    bool datosBorrados = true;

                    foreach (Detalle_Pedido detalle in detalles)
                    {
                        if (!tienda.EliminarDetallePedido((int)detalle.codigo_pedido, (int)detalle.numero_linea))
                            datosBorrados = false;
                    }

                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        if (item.Cells[0].Value != null)
                        {
                            tienda.InsertarDetallePedido(tienda.ListarProducto(item.Cells[0].Value.ToString()), pedido, Convert.ToInt32(item.Cells[1].Value), 0);
                        }
                    }

                    if (datosBorrados)
                        MessageBox.Show("Pedido actuazlizado correctamente.");
                    else
                        MessageBox.Show("No se pudo editar el pedido");
                   

                }
                else
                {
                    pedido = new Pedido(tienda.ListaPedidos.Count + 1, fechaEsperada.Value, null, null,
                    "Pendiente", comentarios.Text, tienda.ListarCliente(comboBox1.Text).codigo_cliente);

                    tienda.InsertarPedido(pedido);

                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        if (item.Cells[0].Value != null)
                        {
                            tienda.InsertarDetallePedido(tienda.ListarProducto(item.Cells[0].Value.ToString()), pedido, Convert.ToInt32(item.Cells[1].Value), 0);
                        }
                    }

                    MessageBox.Show("Pedido realizado correctamente.");
                }
                this.Owner.Show();
                this.Close();
            }

        }

        private void ValidaCliente(out ToolTip TTIP, out bool valCliente)
        {
            TTIP = new ToolTip();
            string cadenaErrores = "";

            if (comboBox1.Text.Length <= 0)
            {
                cadenaErrores = "El campo Cliente no puede estar vacío" + Environment.NewLine;

                //Mejorar la usabilidad mostrando el error
                comboBox1.BackColor = Color.LightCoral;
                TTIP.SetToolTip(comboBox1, cadenaErrores);
                errorProvider3.SetError(comboBox1, cadenaErrores);
                valCliente = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                cadenaErrores = "";
                comboBox1.BackColor = Color.LightGreen;
                TTIP.SetToolTip(comboBox1, "");
                errorProvider3.SetError(comboBox1, "");
                valCliente = true;
            }
        }

        private void ValidarProductos(out ToolTip TTIP, out bool valProductos)
        {
            TTIP = new ToolTip();
            string cadenaErrores = "";
            valProductos = false;

            if (LavelTotalIva.Text == "0")
            {
                cadenaErrores = "La lista de productos no puede estar vacía" + Environment.NewLine;

                //Mejorar la usabilidad mostrando el error
                dataGridView1.BackColor = Color.LightCoral;
                TTIP.SetToolTip(dataGridView1, cadenaErrores);
                errorProvider4.SetError(dataGridView1, cadenaErrores);
                valProductos = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                dataGridView1.BackColor = Color.LightGreen;
                TTIP.SetToolTip(dataGridView1, "");
                errorProvider4.SetError(dataGridView1, "");
                valProductos = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }

            CalcularPrecios();
        }
    }
}
