///<author> Steven Scalzo </author>

using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Pedido : IComparable<Pedido>
    {

        public int? Codigo_Pedido { get; set; }

        public DateTime? Fecha_Pedido { get; set; }

        public DateTime? Fecha_Esperada { get; set; }

        public DateTime? Fecha_Entrega { get; set; }

        [StringLength(15, ErrorMessage = "Estado invalido")]
        public string Estado { get; set; }

        public string Comentarios { get; set; }

        public int? Codigo_Cliente { get; set; }

        public Pedido()
        {
            this.Codigo_Pedido = null;
            this.Fecha_Pedido = null;
            this.Fecha_Esperada = null;
            this.Fecha_Entrega = null;
            this.Estado = null;
            this.Comentarios = "";
            this.Codigo_Cliente = null;
        }

        public Pedido(int? codigoPedido, DateTime? fechaPedido, DateTime? fechaEsperada, DateTime? fechaEntrega,
            string estado, string comentarios, int? codigoCliente)
        {
            this.Codigo_Pedido = codigoPedido;
            this.Fecha_Pedido = fechaPedido;
            this.Fecha_Esperada = fechaEsperada;
            this.Fecha_Entrega = fechaEntrega;
            this.Estado = estado;
            this.Comentarios = comentarios;
            this.Codigo_Cliente = codigoCliente;
        }

        ~Pedido()
        {
            System.Diagnostics.Trace.WriteLine("Destructor de la clase Pedido ha sido llamado");
            this.Codigo_Pedido = null;
            this.Fecha_Pedido = null;
            this.Fecha_Esperada = null;
            this.Fecha_Entrega = null;
            this.Estado = null;
            this.Comentarios = "";
            this.Codigo_Cliente = null;
        }

        public override string ToString()
        {
            return Codigo_Pedido.ToString() + " # " + Fecha_Pedido.ToString() + " # " + Fecha_Esperada.ToString() +
                " # " + Fecha_Entrega.ToString() + " # " + Estado + " # " + Comentarios + " # " + Codigo_Cliente.ToString();
        }

        public int CompareTo(Pedido pedido)
        {
            if (this.Fecha_Pedido < pedido.Fecha_Pedido)
            {
                return -1;
            }
            else if (this.Fecha_Pedido > pedido.Fecha_Pedido)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
