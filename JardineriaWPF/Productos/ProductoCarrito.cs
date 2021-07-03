using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JardineriaWPF.Productos
{
    class ProductoCarrito
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }

        public int Qty { get; set; }
        public ProductoCarrito(string codigo, string nombre, int qty)
        {
            this.Codigo = codigo;
            this.Qty = qty;
            this.Nombre = nombre;
        }
    }

}
