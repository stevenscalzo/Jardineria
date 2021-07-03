using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Pago
    {
        public int? Codigo_Cliente { get; set; }

        [StringLength(40, ErrorMessage = "forma de pago invalido")]
        public string Forma_pago { get; set; }

        [StringLength(50, ErrorMessage = "Id transaccion invalido")]
        public string Id_Transaccion { get; set; }

        public DateTime? Fecha_Pago { get; set; }

        [RegularExpression(@"\d+(\.\d{15,2})?", ErrorMessage = "Precio invalido")]
        public decimal? Total { get; set; }

        Pago()
        {
            this.Codigo_Cliente = null;
            this.Forma_pago = "";
            this.Id_Transaccion = "";
            this.Fecha_Pago = null;
            this.Total = null;
        }
        public Pago(int? codigo, string formaPago, string transaccion, DateTime fechaPago, decimal total)
        {
            this.Codigo_Cliente = codigo;
            this.Forma_pago = formaPago;
            this.Id_Transaccion = transaccion;
            this.Fecha_Pago = fechaPago;
            this.Total = total;
        }

        ~Pago()
        {
            System.Diagnostics.Trace.WriteLine("Destructor de la clase Pago ha sido llamado");
            this.Codigo_Cliente = null;
            this.Forma_pago = "";
            this.Id_Transaccion = "";
            this.Fecha_Pago = null;
            this.Total = null;
        }

        public override string ToString()
        {
            return Codigo_Cliente.ToString() + " - " + Forma_pago + " - " + Id_Transaccion + " - " + Fecha_Pago.ToString() + " - " + Total.ToString();
        }
    }
}
