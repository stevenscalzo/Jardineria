using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JardineriaWPF.Pedidos
{
    class PedidosInfor
    {
        public int? Codigo_Pedido { get; set; }

        public DateTime? Fecha_Pedido { get; set; }

        public int? Codigo_Cliente { get; set; }

        public string Nombre { get; set; }

        public PedidosInfor(int? codigoPedido, DateTime? fechaPedido, 
            string nombre, int? codigoCliente)
        {
            this.Codigo_Pedido = codigoPedido;
            this.Fecha_Pedido = fechaPedido;
            this.Nombre = nombre;
            this.Codigo_Cliente = codigoCliente;
        }
    }
}
