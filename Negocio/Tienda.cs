using Capa_Datos;
using Entidades;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Negocio
{
    public class Tienda
    {
        public List<Pedido> ListaPedidos;
        public List<Producto> ListaProductos;


        public Tienda ()
        {
            ListaPedidos = ObtenerListaPedidos();
            ListaProductos = ObtenerListaProductos();
        }

        public Tienda (Pedido pedido, Producto producto, int cantidad, short linea)
        {
            InsertarPedido(pedido);
            InsertarProducto(producto);
            InsertarDetallePedido(producto, pedido, cantidad, linea);
        }

        public Tienda (Tienda tiendaAnterior)
        {
            ListaPedidos = tiendaAnterior.ObtenerListaPedidos();
            ListaProductos = tiendaAnterior.ObtenerListaProductos();
        }

        ~Tienda()
        {
            if(ListaPedidos != null && ListaPedidos.Count > 0)
                ListaPedidos.Clear();
            if(ListaProductos != null && ListaProductos.Count >0)
                ListaProductos.Clear();
        }

        public bool InsertarPedido (Pedido p)
        {
            this.ListaPedidos.Add(p);
            PedidoADO nuevoPedido = new PedidoADO();
            bool resultado = nuevoPedido.InsertarPedido(p.Codigo_Pedido, p.Fecha_Pedido, p.Fecha_Esperada, p.Fecha_Entrega, p.Estado,
                p.Comentarios, p.Codigo_Cliente);

            return resultado;
        }

        public void InsertarProducto (Producto producto)
        {
            this.ListaProductos.Add(producto);
        }

        public bool InsertarDetallePedido (Producto producto, Pedido pedido, int cantidad, short linea)
        {
            Detalle_PedidoADO detallePedido = new Detalle_PedidoADO();
            bool resultado = detallePedido.InsertarDetallePedido((int)pedido.Codigo_Pedido, producto.Codigo_Producto, cantidad, (int)producto.Precio_Venta, linea);

            return resultado;
        }

        public List<string> CalcularPrecios(List<string> nombreProductos, List<int> cantidadProductos)
        {
            List<string> precios = new List<string>();
            Producto producto;
            decimal total = 0;
            decimal iva = 0;

            for(int i = 0; i < nombreProductos.Count; i++)
            {
                producto = this.ListarProducto(nombreProductos[i]);
                total += (decimal)cantidadProductos[i] * (decimal)producto.Precio_Venta;
            }

            iva = (total / 100 * 21);
            precios.Add(total.ToString());
            precios.Add(iva.ToString());
            precios.Add((iva + total).ToString());

            return precios;
        }

        public List<Pedido> ObtenerListaPedidos()
        {
            PedidoADO p = new PedidoADO();

            return p.LeerPedidos();
        }

        public List<Producto> ObtenerListaProductos()
        {
            ProductoADO p = new ProductoADO();

            return p.LeerProductos();
        }


        public List<Pago> ObtenerPagos()
        {
            PagoADO p = new PagoADO();

            return p.LeerPagos();
        }

        public bool InsertarPago (Pago p)
        {
            PagoADO pA = new PagoADO();
            bool resultado = pA.InsertarPago((int)p.Codigo_Cliente, p.Forma_pago, p.Id_Transaccion, (System.DateTime)p.Fecha_Pago, (int)p.Total);

            return resultado;
        }

        public void InsertarAdmin()
        {
            EmpleadoADO eA = new EmpleadoADO();

            List<Empleado> empleados = this.ListarEmpelados();


            if (!empleados.Exists(x => x.Nombre == "admin" && x.Pass == CalculateMD5Hash("1234")))
            {
                eA.InsertarEmpleado(null,"admin", "admin", "admin", "admin", "admin", "TOK-JP", null, "puestoAdmin", CalculateMD5Hash("1234"));
            }
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public bool  InsertarCliente(string nombreCliente, string nombreContacto, string apellidoContacto,
            string telefono, string fax, string lineaDireccion1, string lineaDireccion2, string ciudad,
            string region, string pais, string codigoPostal, int codigoEmpleadosRepVentas, decimal limiteCredito)
        {
            ClienteADO eA = new ClienteADO();
            List<Cliente> clientes = this.ListarClientes();
            return eA.InsertarCliente(null, nombreCliente, nombreContacto, apellidoContacto, telefono, fax, lineaDireccion1, 
                lineaDireccion2, ciudad, region, pais, codigoPostal, codigoEmpleadosRepVentas, limiteCredito);
            
        }

        public List<Empleado> ListarEmpelados()
        {
            EmpleadoADO empleados = new EmpleadoADO();
            return empleados.LeerEmpleados();

        }

        public List<Gama_Producto> ListarGama()
        {
            Gama_ProductoADO gama = new Gama_ProductoADO();
            return gama.LeerGamaProductos();

        }

        public List<Detalle_Pedido> ListarDetallesPedidos()
        {
            Detalle_PedidoADO detalles = new Detalle_PedidoADO();

            return detalles.LeerDetallesPedidos();
        }

        public Cliente BuscarCliente(int codigo)
        {
            List<Cliente> clientes = ListarClientes();

            if (clientes.Find(empleado => empleado.codigo_cliente == codigo) != null)
            {
                return clientes.Find(empleado => empleado.codigo_cliente == codigo);
            }

            return null;
        }

        public Cliente ListarCliente(string codigo)
        {
            List<Cliente> clientes = ListarClientes();

            if(clientes.Find(cliente => cliente.nombre_cliente == codigo) != null){
                return clientes.Find(cliente => cliente.nombre_cliente == codigo);
            } else
            {
                return clientes.Find(cliente => cliente.telefono == codigo);
            }
        }

        public List<Detalle_Pedido> ListarDetallesPedido(int codigo)
        {
            Detalle_PedidoADO detalles = new Detalle_PedidoADO();
            List<Detalle_Pedido> detallesLista = detalles.LeerDetallesPedido(codigo);

            return detallesLista;
        }

        public List<Pedido> ListarPedidosPorMes(int mes, int anyo)
        {
            PedidoADO pedidos = new PedidoADO();
            List<Pedido> pedidosTotal = pedidos.LeerPedidos();
            List<Pedido> pedidosMes = new List<Pedido>();


            foreach (Pedido pedido in pedidosTotal)
            {
                if (pedido.Fecha_Pedido.Value.Month == mes &&
                    pedido.Fecha_Pedido.Value.Year == anyo)
                {
                    pedidosMes.Add(pedido);
                }
            }

            return pedidosMes;
        }

        public Empleado ListarEmpleado(string codigo)
        {
            List<Empleado> empleados = ListarEmpelados();

            if (empleados.Find(empleado => empleado.NombreCompleto == codigo) != null)
            {
                return empleados.Find(empleado => empleado.NombreCompleto == codigo);
            }
            else if (empleados.Find(empleado => empleado.Codigo_Empleado.ToString() == codigo) != null)
            {
                return empleados.Find(empleado => empleado.Codigo_Empleado.ToString() == codigo);
            }
            else if (empleados.Find(empleado => empleado.Email == codigo) != null)
            {
                return empleados.Find(empleado => empleado.Email == codigo);
            }
            else
            {
                return null;
            }
        }

        public Producto ListarProducto(string codigo)
        {
            List<Producto> productos = this.ObtenerListaProductos();

            if (productos.Find(producto => producto.Nombre == codigo) != null)
            {
                return productos.Find(producto => producto.Nombre == codigo);
            }
            else if (productos.Find(producto => producto.Gama == codigo) != null)
            {
                return productos.Find(producto => producto.Gama == codigo);
            }
            else
            {
                return productos.Find(producto => producto.Codigo_Producto == codigo);
            }
        }

        public List<Cliente> ListarClientes()
        {
            ClienteADO clientes = new ClienteADO();
            return clientes.LeerClientes();

        }

        public List<Oficina> ListarOficinas()
        {
            OficinaADO oficinas = new OficinaADO();
            return oficinas.LeerOficinas();

        }
        public Pago ActualizarPago (Pago p)
        {
            PagoADO pA = new PagoADO();
            Pago resultado = pA.ActualizarPago(p);

            return resultado;
        }

        public void ActualizarPedido(Pedido p)
        {
            PedidoADO pA = new PedidoADO();
            pA.ActualizarPedido(p);
        }

        public void ActualizarDetallePedido(Detalle_Pedido p)
        {
            Detalle_PedidoADO pA = new Detalle_PedidoADO();
            pA.ActualizaDetallePedido(p);
        }

        public void ActualizarCliente(Cliente p)
        {
            ClienteADO cC = new ClienteADO();
            cC.ActualizarCliente(p);
        }

        public bool ActualizarProducto(Producto p)
        {
            ProductoADO cC = new ProductoADO();
            bool editado = false;
            if (cC.ActualizarProducto(p) != null)
                editado = true;
            return editado;
        }

        public List<Pedido> ListarPedidos (int codigoCliente)
        {
            PedidoADO pedidos = new PedidoADO();
            List<Pedido> pedidosTotal = pedidos.LeerPedidos();
            List<Pedido> pedidosCliente = new List<Pedido>();


            foreach (Pedido pedido in pedidosTotal)
            {
                if (pedido.Codigo_Cliente == codigoCliente) 
                {
                    pedidosCliente.Add(pedido);
                }
            }

            return pedidosCliente;
        }

        public List<Pago> ListarPagos(int codigoCliente)
        {
            PagoADO pagos = new PagoADO();
            List<Pago> pagosTotal = pagos.LeerPagos();
            List<Pago> pagosCliente = new List<Pago>();


            foreach (Pago pago in pagosTotal)
            {
                if (pago.Codigo_Cliente == codigoCliente)
                {
                    pagosCliente.Add(pago);
                }
            }

            return pagosCliente;
        }

        public Pedido ListarPedido (int codigoPedido)
        {
            return ObtenerListaPedidos().Find(pedido => pedido.Codigo_Pedido == codigoPedido);
        }

        public List<Producto> ListarProductos (int codigo_pedido)
        {
            PedidoADO pedidos = new PedidoADO();
            List<Pedido> listaProductos = pedidos.LeerPedido(codigo_pedido);

            List<Producto> listaProductosPorPedido = new List<Producto>();


            return listaProductosPorPedido;
        }

        public bool EliminarCliente(int codigoCliente)
        {
            ClienteADO cC = new ClienteADO();
            return cC.BorrarCliente(codigoCliente);
        }

        public bool EliminarPedido(int codigoPedido)
        {
            PedidoADO cC = new PedidoADO();
            return cC.BorrarPedido(codigoPedido);
        }

        public bool EliminarPago(string id)
        {
            PagoADO cC = new PagoADO();
            return cC.BorrarPago(id);
        }

        public bool EliminarDetallePedido(int codigoPedido, int numLinea)
        {
            Detalle_PedidoADO cC = new Detalle_PedidoADO();
            return cC.BorrarDetallePedido(codigoPedido, numLinea);
        }

        public override string ToString()
        {
            string productos = "";
            string pedidos = "";

            foreach (Producto producto in this.ListaProductos)
            {
                productos = productos + producto.ToString() + "\n";
            }

            foreach (Pedido pedido in this.ListaPedidos)
            {
                pedidos = pedidos + pedido.ToString() + "\n";
            }

            return productos + pedidos;
        }

    }
}
