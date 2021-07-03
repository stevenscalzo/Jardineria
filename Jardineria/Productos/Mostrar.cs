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
    public partial class Mostrar : Form
    {
        Tienda tienda = new Tienda();
        List<Producto> productos;
        List<Producto> productosFiltrados;
        Producto productoEditable;

        public Mostrar(string accion = null)
        {
            InitializeComponent();
            productos = tienda.ObtenerListaProductos();
            productosFiltrados = productos;

            if(accion == "mostrar")
            {
                BotonGuardar.Visible = false;
                groupBox2.Visible = false;
            }

            foreach (Gama_Producto gama in tienda.ListarGama())
            {
                txtGama.Items.Add(gama.Gama);
            }
        }

        private void FormProductos_Load(object sender, EventArgs e)
        {
            foreach (Producto producto in productos)
            {
                this.listaProductos.Rows.Add(producto.Nombre, producto.Gama);
            }
        }

        private void TxtNombre_TextChanged(object sender, EventArgs e)
        {
            List<Producto> productoAuxiliar = new List<Producto>();
            foreach (Producto producto in productos)
            {
                if (producto.Nombre.ToLower().Contains(TxtNombre.Text.ToLower()))
                {
                    productoAuxiliar.Add(producto);
                }
            }

            productosFiltrados = productoAuxiliar;
            this.listaProductos.Rows.Clear();
            this.listaProductos.Refresh();

            foreach (Producto producto in productosFiltrados)
            {
                this.listaProductos.Rows.Add(producto.Nombre, producto.Gama);
            }
        }

        private void TxtGama_TextChanged(object sender, EventArgs e)
        {
            List<Producto> productoAuxiliar = new List<Producto>();
            foreach (Producto producto in productosFiltrados)
            {
                if (producto.Gama.ToLower().Contains(txtGama.Text.ToLower()))
                {
                    productoAuxiliar.Add(producto);
                }
            }

            productosFiltrados = productoAuxiliar;
            this.listaProductos.Rows.Clear();
            this.listaProductos.Refresh();

            foreach (Producto producto in productosFiltrados)
            {
                this.listaProductos.Rows.Add(producto.Nombre, producto.Gama);
            }
        }

        private void listaProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            productoEditable = tienda.ListarProducto(this.listaProductos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());

            this.datosDelProducto.Rows.Clear();

            this.datosDelProducto.Rows.Add("Codigo: ", productoEditable.Codigo_Producto);
            this.datosDelProducto.Rows.Add("Nombre: ", productoEditable.Nombre);
            this.datosDelProducto.Rows.Add("Gama: ", productoEditable.Gama);
            this.datosDelProducto.Rows.Add("Dimensiones: ", productoEditable.Dimensiones);
            this.datosDelProducto.Rows.Add("Proveedor: ", productoEditable.Proveedor);
            this.datosDelProducto.Rows.Add("Descripcion: ", productoEditable.Descripcion);
            this.datosDelProducto.Rows.Add("Stock: ", productoEditable.Cantidad_En_Stock.ToString());
            this.datosDelProducto.Rows.Add("Precio de venta: ", productoEditable.Precio_Venta.ToString());
            this.datosDelProducto.Rows.Add("Precio de proveedor: ", productoEditable.Precio_Proveedor.ToString());

        }

        private void DatosProducto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BotonGuardar_Click(object sender, EventArgs e)
        {
            String cadenaErrores = "";
            bool valido = true;
            ToolTip TTIP = new ToolTip();
            if (InputPrecio.Text.Length <= 0)
            {
                valido = false;
                cadenaErrores = "El precio de venta no puede estar vacío" + Environment.NewLine;

                //Mejorar la usabilidad mostrando el error
                InputPrecio.BackColor = Color.LightCoral;
                TTIP.SetToolTip(InputPrecio, cadenaErrores);
                errorProvider1.SetError(InputPrecio, cadenaErrores);
            }

            if (inputProveedor.Text.Length <= 0)
            {
                valido = false;
                cadenaErrores = "El precio de proveedor no puede estar vacío" + Environment.NewLine;

                //Mejorar la usabilidad mostrando el error
                inputProveedor.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputProveedor, cadenaErrores);
                errorProvider2.SetError(inputProveedor, cadenaErrores);
            }

            if (valido)
            {
                //Si el en el campo está todo correcto
                InputPrecio.BackColor = Color.LightGreen;
                TTIP.SetToolTip(InputPrecio, "");
                errorProvider1.SetError(InputPrecio, "");

                inputProveedor.BackColor = Color.LightGreen;
                TTIP.SetToolTip(inputProveedor, "");
                errorProvider1.SetError(inputProveedor, "");

                decimal venta = Convert.ToDecimal(InputPrecio.Text);
                productoEditable.Precio_Venta = venta;

                decimal proveedor = Convert.ToDecimal(inputProveedor.Text);
                productoEditable.Precio_Proveedor = proveedor;

                if(!tienda.ActualizarProducto(productoEditable))
                    MessageBox.Show("Artículo editado correctamente.");
                else
                    MessageBox.Show("No se puedo editar el artículo.");
                this.Close();
            }

        }
    }
}
