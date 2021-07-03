///<author> Steven Scalzo </author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Oficina
    {
        [StringLength(10, ErrorMessage = "Codigo oficina invalida")]
        public string Codigo_Oficina { get; set; }

        [StringLength(30, ErrorMessage = "Ciudad invalida")]
        public string Ciudad { get; set; }

        [StringLength(50, ErrorMessage = "Pais invalido")]
        public string Pais { get; set; }

        [StringLength(50, ErrorMessage = "Region invalida")]
        public string Region { get; set; }

        [StringLength(10, ErrorMessage = "Codigo postal invalido")]
        public string Codigo_Postal { get; set; }

        [StringLength(20, ErrorMessage = "Telefono invalido")]
        public string Telefono { get; set; }

        [StringLength(50, ErrorMessage = "Linea Direccion 1 invalido")]
        public string Linea_Direccion_1 { get; set; }

        [StringLength(50, ErrorMessage = "Linea Direccion 2 invalido")]
        public string Linea_Direccion_2 { get; set; }



        public Oficina()
        {
            this.Codigo_Oficina = "";
            this.Ciudad = "";
            this.Pais = "";
            this.Region = "";
            this.Codigo_Postal = "";
            this.Telefono = "";
            this.Linea_Direccion_1 = "";
            this.Linea_Direccion_2 = "";
        }

        public Oficina(string codigoOficina, string ciudad, string pais, string region, string codigoPostal,
            string telefono, string lineaDireccion1, string lineaDireciion2)
        {
            this.Codigo_Oficina = codigoOficina;
            this.Ciudad = ciudad;
            this.Pais = pais;
            this.Region = region;
            this.Codigo_Postal = codigoPostal;
            this.Telefono = telefono;
            this.Linea_Direccion_1 = lineaDireccion1;
            this.Linea_Direccion_2 = lineaDireciion2;
        }

        ~Oficina()
        {
            System.Diagnostics.Trace.WriteLine("Destructor de la clase Oficina ha sido llamado");
            this.Codigo_Oficina = "";
            this.Ciudad = "";
            this.Pais = "";
            this.Region = "";
            this.Codigo_Postal = "";
            this.Telefono = "";
            this.Linea_Direccion_1 = "";
            this.Linea_Direccion_2 = "";
        }

        public override string ToString()
        {
            return Codigo_Oficina + " # " + Ciudad + " # " + Pais + " # " + Region + " # " + Codigo_Postal +
                Telefono + " # " + Linea_Direccion_1 + " # " + Linea_Direccion_2;
        }
    }
}
