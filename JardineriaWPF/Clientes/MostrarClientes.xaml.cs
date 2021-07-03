using Entidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Negocio;
using System.Collections.Specialized;

namespace JardineriaWPF.Clientes
{
    /// <summary>
    /// Lógica de interacción para MostrarClientes.xaml
    /// </summary>
    public partial class MostrarClientes : Window
    {

        Tienda tienda = new Tienda();
        List<Empleado> empleados;
        Cliente cliente;
        private ObservableCollection<Cliente> ListaClientes;
        private ObservableCollection<Cliente> ListaClientesNuevos;
        private ObservableCollection<Cliente> ListaClientesModificados;
        private ObservableCollection<Cliente> ListaClientesBorrados;
        private string correo;
        public MostrarClientes(string correo)
        {
            InitializeComponent();
            empleados = tienda.ListarEmpelados();
            this.correo = correo;

            ListaClientes = new ObservableCollection<Cliente>();
            ListaClientesNuevos = new ObservableCollection<Cliente>();
            ListaClientesModificados = new ObservableCollection<Cliente>();
            ListaClientesBorrados = new ObservableCollection<Cliente>();
            getClientes();

            ListaClientes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(CollectionChangedMethod);
            this.DataContext = ListaClientes;
        }

        private void getClientes()
        {
            foreach(Cliente cliente in tienda.ListarClientes())
            {
                ListaClientes.Add(cliente);
            }
        }

        private void CollectionChangedMethod(object sender, NotifyCollectionChangedEventArgs e)
        {
            //Comprobamos las difirentes acciones posibles sobre la lista
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                ListaClientesNuevos.Add((Cliente)(e.NewItems[e.NewItems.Count - 1]));
            }
            if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                ListaClientesModificados.Add(ListaClientes[e.OldStartingIndex]);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                ListaClientesBorrados.Add(ListaClientes[e.OldStartingIndex]);
            }
        }

        private void btnInsertar_Click(object sender, RoutedEventArgs e)
        {
            if (confirmarDatos())
            {
                if(tienda.InsertarCliente(NombreCliente.Text, NombreContacto.Text, ApellidoContacto.Text,
                       telefono.Text, fax.Text, direccion1.Text, direccion2.Text, ciudad.Text,
                       region.Text, pais.Text, codigoPostal.Text, Convert.ToInt32(CodigoRepresentante.Text),
                       Convert.ToDecimal(credito.Text)))
                {
                    MessageBox.Show("Registro insertado correctamente.");
                    ListaClientes.Add(new Cliente(null, NombreCliente.Text, NombreContacto.Text, ApellidoContacto.Text,
                       telefono.Text, fax.Text, direccion1.Text, direccion2.Text, ciudad.Text,
                       region.Text, pais.Text, codigoPostal.Text, Convert.ToInt32(CodigoRepresentante.Text),
                       Convert.ToDecimal(credito.Text)));
                } else
                    MessageBox.Show("No se pudo realizar el registro.");
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (confirmarDatos())
            {
                cliente = (Cliente)ListBox_Maestro.SelectedItem;
                if (tienda.EliminarCliente((int)cliente.codigo_cliente))
                {
                    ListaClientes.Remove(cliente);
                    MessageBox.Show("Cliente eliminado con exito");
                } else
                    MessageBox.Show("No se pudo eliminar el registro.");
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (confirmarDatos())
            {
                cliente = (Cliente)ListBox_Maestro.SelectedItem;
                cliente.nombre_cliente = NombreCliente.Text;
                cliente.nombre_contacto = NombreContacto.Text;
                cliente.apellido_contacto = ApellidoContacto.Text;
                cliente.telefono = telefono.Text;
                cliente.fax = fax.Text;
                cliente.linea_direccion1 = direccion1.Text;
                cliente.linea_direccion2 = direccion2.Text;
                cliente.ciudad = ciudad.Text;
                cliente.pais = pais.Text;
                cliente.region = region.Text;
                cliente.codigo_postal = codigoPostal.Text;
                cliente.codigo_empleado_rep_ventas = Convert.ToInt32(CodigoRepresentante.Text);
                cliente.limite_credito = Convert.ToDecimal(credito.Text);
                tienda.ActualizarCliente(cliente);
                MessageBox.Show("Registro editado correctamente.");
            }
        }

        private bool confirmarDatos()
        {
            bool datosValidos = true;
            string cadenaErrores = "";

            cadenaErrores += ComprobarNombreCliente();
            cadenaErrores += ComprobarNombreContacto();
            cadenaErrores += ComprobarApellidoContacto();
            cadenaErrores += ComprobarTelefono();
            cadenaErrores += ComprobarFax();
            cadenaErrores += ComprobarDireccion();
            cadenaErrores += ComprobarCiudad();
            cadenaErrores += ComprobarCodPostal();
            cadenaErrores += ComprobarCodEmple();
            cadenaErrores += ComprobarLimiteCred();
            cadenaErrores += ComprobarPass();


            if (cadenaErrores != "")
            {
                MessageBox.Show(cadenaErrores);
                datosValidos = false;
            }

            return datosValidos;
        }

        private string ComprobarPass()
        {
            string cadenaErrores = "";
            int resultado = ValidaString(pass1.Password, 50, false);
            resultado = ValidaString(pass2.Password, 50, false);
            if (resultado > 0)
            {
                switch (resultado)
                {
                    case 1:
                        cadenaErrores = "El campo Contraseña no permite más de 50 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Contraseña no puede estar vacío" + Environment.NewLine;
                        break;
                }
            }
            else if (!(pass1.Password == pass2.Password) && !empleados.Exists(x => x.Email == correo && x.Pass == CalculateMD5Hash(pass1.Password)))
            {
                cadenaErrores = "Constraseña inválida";
            }

            return cadenaErrores;
        }
        private string ComprobarLimiteCred()
        {
            string cadenaErrores = "";
            int resultado = ValidaString(credito.Text, 18, false);
            if (resultado > 0)
            {
                switch (resultado)
                {
                    case 1:
                        cadenaErrores = "El campo Limite de Credito no permite más de 18 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Limite de Credito no puede estar vacío" + Environment.NewLine;
                        break;
                }
            }
            else if (Convert.ToDouble(credito.Text) < 1000)
            {
                cadenaErrores = "El campo Limite de Credito debe ser mayor de 1000€" + Environment.NewLine;
            }

            return cadenaErrores;
        }
        private string ComprobarCodEmple()
        {
            string cadenaErrores = "";
            string codigoRepresentante = tienda.ListarEmpleado(CodigoRepresentante.Text).Codigo_Oficina;
            int resultado = ValidaString(codigoRepresentante, 11, false);
            if (resultado > 0)
            {
                switch (resultado)
                {
                    case 1:
                        cadenaErrores = "El campo Codigo Empleado no permite más de 11 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Codigo Empleado no puede estar vacío" + Environment.NewLine;
                        break;
                }
            }

            return cadenaErrores;
        }
        private string ComprobarCodPostal()
        {
            string cadenaErrores = "";
            int resultado = ValidaString(codigoPostal.Text, 10, false);
            if (resultado > 0)
            {
                switch (resultado)
                {
                    case 1:
                        cadenaErrores = "El campo Codigo Postal no permite más de 10 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Codigo Postal no puede estar vacío" + Environment.NewLine;
                        break;
                }
            }

            return cadenaErrores;
        }
        private string ComprobarCiudad()
        {
            string cadenaErrores = "";
            int resultado = ValidaString(ciudad.Text, 50, false);
            if (resultado > 0)
            {
                switch (resultado)
                {
                    case 1:
                        cadenaErrores = "El campo Ciudad no permite más de 50 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Ciudad no puede estar vacío" + Environment.NewLine;
                        break;
                }
            }

            return cadenaErrores;
        }
        private string ComprobarDireccion()
        {
            string cadenaErrores = "";
            int resultado = ValidaString(direccion1.Text, 50, false);
            if (resultado > 0)
            {
                switch (resultado)
                {
                    case 1:
                        cadenaErrores = "El campo Direccion no permite más de 50 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Direccion no puede estar vacío" + Environment.NewLine;
                        break;
                    default: break;
                }
            }

            return cadenaErrores;
        }

        private string ComprobarFax()
        {
            string cadenaErrores = "";
            int resultado = ValidaString(fax.Text, 15, false);
            if (resultado > 0)
            {
                switch (resultado)
                {
                    case 1:
                        cadenaErrores = "El campo Fax no permite más de 15 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Fax no puede estar vacío" + Environment.NewLine;
                        break;
                    default: break;
                }
            }

            return cadenaErrores;
        }
        private string ComprobarTelefono()
        {
            string cadenaErrores = "";
            int resultado = ValidaString(telefono.Text, 15, false);
            if (resultado > 0)
            {
                switch (resultado)
                {
                    case 1:
                        cadenaErrores = "El campo Telefono no permite más de 15 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Telefono no puede estar vacío" + Environment.NewLine;
                        break;
                    default: break;
                }
            }

            return cadenaErrores;
        }
        private string ComprobarApellidoContacto()
        {
            string cadenaErrores = "";
            int resultado = ValidaString(ApellidoContacto.Text, 30, false);
            if (resultado > 0)
            {
                switch (resultado)
                {

                    case 1:
                        cadenaErrores = "El campo Apellido de Contacto no permite más de 30 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Apellido de Contacto no puede estar vacío" + Environment.NewLine;
                        break;
                    default: break;
                }
            }

            return cadenaErrores;
        }

        private string ComprobarNombreContacto()
        {
            string cadenaErrores = "";
            int resultado = ValidaString(NombreContacto.Text, 30, false);
            if (resultado > 0)
            {
                switch (resultado)
                {

                    case 1:
                        cadenaErrores = "El campo Nombre de Contacto no permite más de 30 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Nombre de Contacto no puede estar vacío" + Environment.NewLine;
                        break;
                    default: break;
                }
            }

            return cadenaErrores;
        }

        private string ComprobarNombreCliente()
        {
            string cadenaErrores = "";
            int resultado = ValidaString(NombreCliente.Text, 50, false);
            if (resultado > 0)
            {
                switch (resultado)
                {
                    case 1:
                        cadenaErrores = "El campo Nombre de Cliente no permite más de 50 carácteres" + Environment.NewLine;
                        break;
                    case 2:
                        cadenaErrores = "El campo Nombre de Cliente no puede estar vacío" + Environment.NewLine;
                        break;
                    default: break;
                }
            }

            return cadenaErrores;
        }
        private int ValidaString(string cadena, int longitudMaxima, bool nulo)
        {
            int codigoError = 0;

            if (cadena.Length > longitudMaxima) codigoError = 1;
            if (cadena.Length < 1 && !nulo) codigoError = 2;
            return codigoError;
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
    }
}
