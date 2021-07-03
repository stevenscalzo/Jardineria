using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Jardineria
{
    public partial class Modificar : Form
    {
        Tienda tienda = new Tienda();
        public Registro clientesForm;
        List<Cliente> clientes;
        List<Cliente> clientesFiltrados;
        string accion;
        string nombre;
        string email;

        public Modificar(string accion = null, string nombre = null, string email = null)
        {
            InitializeComponent();
            clientes = tienda.ListarClientes();
            clientesFiltrados = clientes;
            this.accion = accion;
            this.nombre = nombre;
            this.email = email;
        }

        private void ClienteModificar_Load(object sender, EventArgs e)
        {
            foreach(Cliente cliente in clientes)
            {
                this.dataGridView1.Rows.Add(cliente.nombre_cliente, cliente.telefono);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Cliente> clientesAuxiliar = new List<Cliente>();
            foreach (Cliente cliente in clientes)
            {
                if (cliente.nombre_cliente.ToLower().Contains(TxtNombre.Text.ToLower()))
                {
                    clientesAuxiliar.Add(cliente);
                }
            }

            clientesFiltrados = clientesAuxiliar;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();

            foreach (Cliente cliente in clientesFiltrados)
            {
                this.dataGridView1.Rows.Add(cliente.nombre_cliente, cliente.telefono);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            List<Cliente> clientesAuxiliar = new List<Cliente>();
            foreach (Cliente cliente in clientesFiltrados)
            {
                if (cliente.telefono.Contains(TxtTelefono.Text))
                {
                    clientesAuxiliar.Add(cliente);
                }
            }

            clientesFiltrados = clientesAuxiliar;
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Refresh();

            foreach (Cliente cliente in clientesFiltrados)
            {
                this.dataGridView1.Rows.Add(cliente.nombre_cliente, cliente.telefono);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (accion == "eliminar")
            {
                Cliente clienteEliminar = tienda.ListarCliente(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

                if (DialogResult.Yes == MessageBox.Show("¿Quieres borrar los datos del cliente " + clienteEliminar.nombre_cliente + "?", "Borrar Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    List<Pedido> pedidos = tienda.ListarPedidos((int)clienteEliminar.codigo_cliente);
                    bool datosBorrados = true;

                    foreach (Pedido pedido in pedidos)
                    {
                        List<Detalle_Pedido> detalles = tienda.ListarDetallesPedido((int)pedido.Codigo_Pedido);

                        foreach (Detalle_Pedido detalle in detalles)
                        {
                            if (!tienda.EliminarDetallePedido((int)detalle.codigo_pedido, (int)detalle.numero_linea))
                                datosBorrados = false;
                        }
                        if (datosBorrados)
                            if(!tienda.EliminarPedido((int)pedido.Codigo_Pedido))
                                datosBorrados = false;
                    }
                    if (datosBorrados)
                    {
                        List<Pago> pagos = tienda.ListarPagos((int)clienteEliminar.codigo_cliente);

                        foreach(Pago pago in pagos)
                            if (!tienda.EliminarPago(pago.Id_Transaccion))
                                datosBorrados = false;
                        if (datosBorrados)
                            if (!tienda.EliminarCliente((int)clienteEliminar.codigo_cliente))
                                datosBorrados = false;
                    }
                    if (datosBorrados)
                        MessageBox.Show("Cliente eliminado");
                    else
                        MessageBox.Show("No se pudo eliminar el cliente");
                }

                this.Close();
            } else
            {
                string codigo = tienda.ListarCliente(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()).nombre_cliente;
                clientesForm = new Registro(email, "editar", codigo , nombre);
                clientesForm.MdiParent = this.MdiParent;
                clientesForm.Show();
                this.Close();
            }
        }
    }
}
