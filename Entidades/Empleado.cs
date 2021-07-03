///<author> Steven Scalzo </author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Empleado
    {
        public int? Codigo_Empleado { get; set; }

        [StringLength(50, ErrorMessage = "Nombre invalido")]
        public string Nombre { get; set; }

        [StringLength(50, ErrorMessage = "Apellido 1 invalido")]
        public string Apellido1 { get; set; }

        [StringLength(50, ErrorMessage = "Apellido 2 invalido")]
        public string Apellido2 { get; set; }

        [StringLength(10, ErrorMessage = "Extension invalida")]
        public string Extension { get; set; }

        [StringLength(100, ErrorMessage = "Email invalido")]
        public string Email { get; set; }

        [StringLength(10, ErrorMessage = "Codigo Oficina invalido")]
        public string Codigo_Oficina { get; set; }

        public int? Codigo_Jefe { get; set; }

        [StringLength(50, ErrorMessage = "Puesto invalido")]
        public string Puesto { get; set; }

        public string Pass { get; set; }

        public Empleado()
        {
            this.Codigo_Empleado = null;
            this.Nombre = "";
            this.Apellido1 = "";
            this.Apellido2 = "";
            this.Extension = "";
            this.Email = "";
            this.Codigo_Oficina = "";
            this.Codigo_Jefe = null;
            this.Puesto = "";
            this.Pass = "";
        }

        public Empleado(int? codigoEmpleado, string nombre, string apellido1, string apellido2, string extension,
            string email, string codigoOficina, int? codigoJefe, string puesto, string pass)
        {
            this.Codigo_Empleado = codigoEmpleado;
            this.Nombre = nombre;
            this.Apellido1 = apellido1;
            this.Apellido2 = apellido2;
            this.Extension = extension;
            this.Email = email;
            this.Codigo_Oficina = codigoOficina;
            this.Codigo_Jefe = codigoJefe;
            this.Puesto = puesto;
            this.Pass = pass;
        }

        ~Empleado()
        {
            System.Diagnostics.Trace.WriteLine("Destructor de la clase Empleado ha sido llamado");

            this.Codigo_Empleado = null;
            this.Nombre = "";
            this.Apellido1 = "";
            this.Apellido2 = "";
            this.Extension = "";
            this.Email = "";
            this.Codigo_Oficina = "";
            this.Codigo_Jefe = null;
            this.Puesto = "";
            this.Pass = "";
        }

        public string NombreCompleto
        {
            get
            {
                return Nombre + " " + Apellido1 + " " + Apellido2;
            }
        }

        public override string ToString()
        {
            return Codigo_Empleado.ToString() + " # " + Nombre + " # " + Apellido1 + " # " + Apellido2 + " # " + Extension +
                " # " + Email + " # " + Codigo_Oficina + " # " + Codigo_Jefe.ToString() + " # " + Puesto;
        }
    }
}
