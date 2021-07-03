///<author> Steven Scalzo </author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Producto : IComparable<Producto>
    {

        [StringLength(15, ErrorMessage = "Estado invalido")]
        public string Codigo_Producto { get; set; }

        [StringLength(70, ErrorMessage = "Estado invalido")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "Estado invalido")]
        public string Gama { get; set; }

        [StringLength(25, ErrorMessage = "Estado invalido")]
        public string Dimensiones { get; set; }

        [StringLength(50, ErrorMessage = "Estado invalido")]
        public string Proveedor { get; set; }

        public string Descripcion { get; set; }

        public short? Cantidad_En_Stock { get; set; }

        [RegularExpression(@"\d+(\.\d{15,2})?", ErrorMessage = "Precio Venta invalido")]
        public decimal? Precio_Venta { get; set; }

        [RegularExpression(@"\d+(\.\d{15,2})?", ErrorMessage = "Precio Proveedor invalido")]
        public decimal? Precio_Proveedor { get; set; }


        public Producto()
        {
            this.Codigo_Producto = "";
            this.Nombre = "";
            this.Gama = "";
            this.Dimensiones = "";
            this.Proveedor = "";
            this.Descripcion = "";
            this.Cantidad_En_Stock = null;
            this.Precio_Venta = null;
            this.Precio_Proveedor = null;
        }

        public Producto(string codigoProducto, string nombre, string gama, string dimensiones,
            string proveedor, string descripcion, short? stock, decimal precioVenta,
            decimal precioProveedor)
        {
            this.Codigo_Producto = codigoProducto;
            this.Nombre = nombre;
            this.Gama = gama;
            this.Dimensiones = dimensiones;
            this.Proveedor = proveedor;
            this.Descripcion = descripcion;
            this.Cantidad_En_Stock = stock;
            this.Precio_Venta = precioVenta;
            this.Precio_Proveedor = precioProveedor;
        }

        ~Producto()
        {
            System.Diagnostics.Trace.WriteLine("Destructor de la clase Producto ha sido llamado");
            this.Codigo_Producto = "";
            this.Nombre = "";
            this.Gama = "";
            this.Dimensiones = "";
            this.Proveedor = "";
            this.Descripcion = "";
            this.Cantidad_En_Stock = null;
            this.Precio_Venta = null;
            this.Precio_Proveedor = null;
        }

        public override string ToString()
        {
            return Codigo_Producto + " # " + Nombre + " # " + Gama + " # " + Dimensiones + " # " +
                Proveedor + " # " + Descripcion + " # " + Cantidad_En_Stock.ToString() + " # " + Precio_Venta.ToString() +
                " # " + Precio_Proveedor.ToString();
        }

        public int CompareTo(Producto producto)
        {
            return String.Compare(this.Nombre, producto.Nombre);
        }
    }
}
