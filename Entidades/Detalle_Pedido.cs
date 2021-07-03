///<author> Steven Scalzo </author>

using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Detalle_Pedido
    {

        public Detalle_Pedido()
        {
            this.codigo_pedido = null;
            this.codigo_producto = "";
            this.cantidad = null;
            this.precio_unidad = null;
            this.numero_linea = null;
        }

        public int? codigo_pedido { get; set; }

        [StringLength(15, ErrorMessage = "Codigo Producto invalido")]
        public string codigo_producto { get; set; }

        public int? cantidad { get; set; }

        [RegularExpression(@"\d+(\.\d{15,2})?", ErrorMessage = "Precio invalido")]
        public decimal? precio_unidad { get; set; }
        public short? numero_linea { get; set; }

        
        public Detalle_Pedido(int? codigoPedido, string codigoProducto, int? cantidad, decimal? precioUnidad, short? numeroLinea)
        {
            this.codigo_pedido = codigoPedido;
            this.codigo_producto = codigoProducto;
            this.cantidad = cantidad;
            this.precio_unidad = precioUnidad;
            this.numero_linea = numeroLinea;
        }

        ~Detalle_Pedido()
        {
            System.Diagnostics.Trace.WriteLine("Destructor de la clase Detalle Pedido ha sido llamado");

            this.codigo_pedido = null;
            this.codigo_producto = "";
            this.cantidad = null;
            this.precio_unidad = null;
            this.numero_linea = null;

        }

        public override string ToString()
        {
            return codigo_pedido.ToString() + " # " + codigo_producto.ToString() + " # " + cantidad.ToString() + " # " +
                precio_unidad.ToString() + " # " + numero_linea.ToString();
        }
    }
}
