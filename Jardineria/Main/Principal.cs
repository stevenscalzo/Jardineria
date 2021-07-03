using Jardineria.Estadisticas.Productos;
using Jardineria.Estadisticas.Oficinas;
using Jardineria.Informes;
using System;
using System.Drawing;
using System.Windows.Forms;
using Jardineria.Main;

namespace Jardineria
{
    public partial class Principal : Form
    {
        public Registro clientes;
        public Modificar cliente;
        public Mostrar productos;
        public Detalles pedidos;
        public Buscar buscar;
        public PorDia estadisticaMes;
        public PorTipo estadisticaPorTipo;
        public VendidosEnUnMes ProductosVendidosMes;
        public StockBajo stockBajo;
        public CantidadClientes cantidadClientes;
        public CantidadEmpleados cantidadEmpleados;
        public Facturacion facturacion;
        public Facturas factura;
        public AcercaDe acercaDe;

        string email;

        public Principal(string nombre, string email)
        {
            InitializeComponent();

            this.MaximumSize = new Size(1280, 960);
            this.MinimumSize = new Size(1280, 960);

            TxtNombre.Text = nombre;
            this.email = email;
        }

        private void insertarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clientes = new Registro(email, "insertar", null, TxtNombre.Text);
            clientes.MdiParent = this;
            clientes.Show();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cliente = new Modificar(null, TxtNombre.Text, email);
            cliente.MdiParent = this;
            cliente.Show();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cliente = new Modificar("eliminar", TxtNombre.Text);
            cliente.MdiParent = this;
            cliente.Show();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            productos = new Mostrar("mostrar");
            productos.MdiParent = this;
            productos.Show();
        }

        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            productos = new Mostrar();
            productos.MdiParent = this;
            productos.Show();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pedidos = new Detalles();
            pedidos.MdiParent = this;
            pedidos.Show();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("¿Está usted seguro de que quiere salir?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {                
                MessageBox.Show("Hasta luego...");
                this.Owner.Close();
                this.Close();
            }
        }

        private void consultaYModificaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buscar = new Buscar();
            buscar.MdiParent = this;
            buscar.Show();
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            buscar = new Buscar("eliminar");
            buscar.MdiParent = this;
            buscar.Show();
        }

        private void porDíaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            estadisticaMes = new PorDia();
            estadisticaMes.MdiParent = this;
            estadisticaMes.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            insertarToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            nuevoToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            facturaToolStripMenuItem_Click(sender, e);
        }

        private void facturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buscar = new Buscar("factura");
            buscar.MdiParent = this;
            buscar.Show();
        }

        private void porTipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            estadisticaPorTipo = new PorTipo();
            estadisticaPorTipo.MdiParent = this;
            estadisticaPorTipo.Show();
        }

        private void vendidosPorMesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductosVendidosMes = new VendidosEnUnMes();
            ProductosVendidosMes.MdiParent = this;
            ProductosVendidosMes.Show();
        }

        private void porStockBajoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stockBajo = new StockBajo();
            stockBajo.MdiParent = this;
            stockBajo.Show();
        }

        private void nºClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cantidadClientes = new CantidadClientes();
            cantidadClientes.MdiParent = this;
            cantidadClientes.Show();
        }

        private void nºEmpleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cantidadEmpleados = new CantidadEmpleados();
            cantidadEmpleados.MdiParent = this;
            cantidadEmpleados.Show();
        }

        private void facturaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            facturacion = new Facturacion();
            facturacion.MdiParent = this;
            facturacion.Show();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            acercaDe = new AcercaDe();
            acercaDe.MdiParent = this;
            acercaDe.Show();
        }
    }
}
