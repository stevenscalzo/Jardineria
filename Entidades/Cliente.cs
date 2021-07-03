///<author> Steven Scalzo </author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Entidades
{
    /// <summary>
    /// Clase que crea al objeto de tipo Cliente
    /// </summary>
    public class Cliente
    {
        private int? _codigoCliente;
        private string _nombreCliente;
        private string _nombreContacto;
        private string _apellidoContacto;
        private string _telefono;
        private string _fax;
        private string _lineaDireccion1;
        private string _lineaDireccion2;
        private string _ciudad;
        private string _region;
        private string _pais;
        private string _codigoPostal;
        private int? _codigoEmpleadosRepVentas;
        private decimal? _limiteCredito;

        /// <summary>
        /// Constructor vacío que asigna valores iniciales null al objeto
        /// </summary>
        public Cliente()
        {
            this.codigo_cliente = null;
            this.nombre_cliente = "";
            this.nombre_contacto = "";
            this.apellido_contacto = "";
            this.telefono = "";
            this.fax = "";
            this.linea_direccion1 = "";
            this.linea_direccion2 = "";
            this.ciudad = "";
            this.region = "";
            this.pais = "";
            this.codigo_postal = "";
            this.codigo_empleado_rep_ventas = null;
            this.limite_credito = null;
        }

        /// <summary>
        /// Constructor que recive datos e inicializa sus valores
        /// </summary>
        /// <param name="codigoCliente">Codigo del cliente</param>
        /// <param name="nombreCliente">Nombre del cliente</param>
        /// <param name="nombreContacto">Nombre del contacto</param>
        /// <param name="apellidoContacto">Apellido del contacto</param>
        /// <param name="telefono">Telefono</param>
        /// <param name="fax">Fax</param>
        /// <param name="lineaDireccion1">Direccion linea 1</param>
        /// <param name="lineaDireccion2">Direccion linea 2</param>
        /// <param name="ciudad">Ciudad</param>
        /// <param name="region">Region</param>
        /// <param name="pais">País</param>
        /// <param name="codigoPostal">Código postal</param>
        /// <param name="codigoEmpleadosRepVentas">Código del representante de ventas</param>
        /// <param name="limiteCredito">Límite de crédito</param>
        public Cliente(int? codigoCliente, string nombreCliente, string nombreContacto, string apellidoContacto,
            string telefono, string fax, string lineaDireccion1, string lineaDireccion2, string ciudad,
            string region, string pais, string codigoPostal, int? codigoEmpleadosRepVentas, decimal? limiteCredito)
        {
            this.codigo_cliente = codigoCliente;
            this.nombre_cliente = nombreCliente;
            this.nombre_contacto = nombreContacto;
            this.apellido_contacto = apellidoContacto;
            this.telefono = telefono;
            this.fax = fax;
            this.linea_direccion1 = lineaDireccion1;
            this.linea_direccion2 = lineaDireccion2;
            this.ciudad = ciudad;
            this.region = region;
            this.pais = pais;
            this.codigo_postal = codigoPostal;
            this.codigo_empleado_rep_ventas = codigoEmpleadosRepVentas;
            this.limite_credito = limiteCredito;
        }

        /// <summary>
        /// Destructor que settea los valores del cliente a null
        /// </summary>
        ~Cliente()
        {
            System.Diagnostics.Trace.WriteLine("Destructor de la clase Cliente ha sido llamado");
            this.codigo_cliente = null;
            this.nombre_cliente = "";
            this.nombre_contacto = "";
            this.apellido_contacto = "";
            this.telefono = "";
            this.fax = "";
            this.linea_direccion1 = "";
            this.linea_direccion2 = "";
            this.ciudad = "";
            this.region = "";
            this.pais = "";
            this.codigo_postal = "";
            this.codigo_empleado_rep_ventas = null;
            this.limite_credito = null;
        }

        /// <summary>
        /// Propiedades código cliente
        /// </summary>
        /// <param>Codigo del cliente</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.codigo_cliente = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.codigo_cliente);
        /// </code>
        /// </example>
        /// <return>Valor codigo cliente</return>
        [StringLength(256, ErrorMessage = "La ruta de la imagen no puede exceder de 256 caractéres")]
        public int? codigo_cliente {
            get { return _codigoCliente; }
            set
            {
                _codigoCliente = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades nombre cliente
        /// </summary>
        /// <param>Nombre del cliente</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.nombre_cliente = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.nombre_cliente);
        /// </code>
        /// </example>
        /// <return>Valor nombre cliente</return>
        [StringLength(50, ErrorMessage = "Nombre invalido")]
        public string nombre_cliente {
            get { return _nombreCliente; }
            set
            {
                _nombreCliente = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades nombre contacto
        /// </summary>
        /// <param>Nombre del contacto</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.nombre_contacto = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.nombre_contacto);
        /// </code>
        /// </example>
        /// <return>Valor nombre contacto</return>
        [StringLength(30, ErrorMessage = "Nombre invalido")]
        public string nombre_contacto {
            get { return _nombreContacto; }
            set
            {
                _nombreContacto = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades apellido contacto
        /// </summary>
        /// <param>Apellido del contacto</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.apellido_contacto = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.apellido_contacto);
        /// </code>
        /// </example>
        /// <return>Valor nomapellidobre contacto</return>
        [StringLength(50, ErrorMessage = "Apellido invalido")]
        public string apellido_contacto {
            get { return _apellidoContacto; }
            set
            {
                _apellidoContacto = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades telefono
        /// </summary>
        /// <param>Telefono</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.telefono = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.telefono);
        /// </code>
        /// </example>
        /// <return>Valor telefono</return>
        [StringLength(15, ErrorMessage = "Telefono invalido")]
        public string telefono {
            get { return _telefono; }
            set
            {
                _telefono = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades fax
        /// </summary>
        /// <param>Fax</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.fax = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.fax);
        /// </code>
        /// </example>
        /// <return>Valor fax</return>
        [StringLength(15, ErrorMessage = "Nombre invalido")]
        public string fax {
            get { return _fax; }
            set
            {
                _fax = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades linea_direccion1
        /// </summary>
        /// <param>linea_direccion1</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.linea_direccion1 = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.linea_direccion1);
        /// </code>
        /// </example>
        /// <return>Valor linea_direccion1</return>
        [StringLength(50, ErrorMessage = "Direccion invalida")]
        public string linea_direccion1 {
            get { return _lineaDireccion1; }
            set
            {
                _lineaDireccion1 = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades linea_direccion2
        /// </summary>
        /// <param>linea_direccion2</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.linea_direccion2 = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.linea_direccion2);
        /// </code>
        /// </example>
        /// <return>Valor linea_direccion2</return>
        [StringLength(50, ErrorMessage = "Direccion invalida")]
        public string linea_direccion2 {
            get { return _lineaDireccion2; }
            set
            {
                _lineaDireccion2 = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades ciudad
        /// </summary>
        /// <param>ciudad</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.ciudad = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.ciudad);
        /// </code>
        /// </example>
        /// <return>Valor ciudad</return>
        [StringLength(50, ErrorMessage = "Ciudad invalida")]
        public string ciudad {
            get { return _ciudad; }
            set
            {
                _ciudad = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades region
        /// </summary>
        /// <param>region</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.region = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.region);
        /// </code>
        /// </example>
        /// <return>Valor region</return>
        [StringLength(50, ErrorMessage = "region invalida")]
        public string region {
            get { return _region; }
            set
            {
                _region = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades pais
        /// </summary>
        /// <param>pais</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.pais = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.pais);
        /// </code>
        /// </example>
        /// <return>Valor pais</return>
        [StringLength(50, ErrorMessage = "pais invalida")]
        public string pais {
            get { return _pais; }
            set
            {
                _pais = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades codigo_postal
        /// </summary>
        /// <param>codigo_postal</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.codigo_postal = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.codigo_postal);
        /// </code>
        /// </example>
        /// <return>Valor codigo_postal</return>
        [StringLength(10, ErrorMessage = "Codigo postal invalido")]
        public string codigo_postal {
            get { return _codigoPostal; }
            set
            {
                _codigoPostal = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades código del representante de ventas
        /// </summary>
        /// <param>codigo_empleado_rep_ventas</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.codigo_empleado_rep_ventas = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.codigo_empleado_rep_ventas);
        /// </code>
        /// </example>
        /// <return>Valor codigo_empleado_rep_ventas</return>
        public int? codigo_empleado_rep_ventas {
            get { return _codigoEmpleadosRepVentas; }
            set
            {
                _codigoEmpleadosRepVentas = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedades limite_credito
        /// </summary>
        /// <param>limite_credito</param>
        /// <example>
        /// Para asignar un valor
        /// <code>
        /// cliente.limite_credito = value;
        /// </code>        
        /// </example>
        /// <example>
        /// Para obtener un valor e imprimirlo
        /// <code>
        /// Console.WriteLine(cliente.limite_credito);
        /// </code>
        /// </example>
        /// <return>Valor limite_credito</return>
        [RegularExpression(@"\d+(\.\d{15,2})?", ErrorMessage = "Precio invalido")]
        public decimal? limite_credito {
            get { return _limiteCredito; }
            set
            {
                _limiteCredito = value;
                // Call OnPropertyChanged whenever the property is updated
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Propiedada PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Método OnPropertyChanged, realiza el cambio en tiempo real de alguna propiedad
        /// </summary>
        /// <param name="name"></param>
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// Método toString
        /// </summary>
        /// <returns>String con todos los datos del cliente separados por " # "</returns>
        public override string ToString()
        {
            return codigo_cliente.ToString() + " # " + nombre_cliente + " # " + nombre_contacto + " # " + apellido_contacto +
                " # " + telefono + " # " + fax + " # " + linea_direccion1 + " # " + linea_direccion2 + " # " + ciudad + " # " +
                region + " # " + codigo_postal + " # " + codigo_empleado_rep_ventas.ToString() + " # " + limite_credito.ToString();
        }
    }
}
