using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Entidades;
using Negocio;

namespace Jardineria
{
    public partial class Registro : Form
    {
        Tienda tienda = new Tienda();
        int codigoCliente = 0;
        Cliente cliente;
        public Principal principal;
        string nombre;
        string email;
        public Registro(string email, string accion, string codigo = null, string nombre = null)
        {
            InitializeComponent();
            this.nombre = nombre;
            this.email = email;
            TxtPassword.PasswordChar = '*';

            foreach (Empleado empleado in tienda.ListarEmpelados())
            {
                representantes.Items.Add(empleado.NombreCompleto);
            }


            if (accion == "insertar")
            {
                TxtFormulario.Text = "Registro de clientes";
                BotonFormulario.Text = "Agregar Cliente";
            } else if (accion == "editar")
            {
                cliente = tienda.ListarCliente(codigo);
                TxtFormulario.Text = "Editar cliente";
                BotonFormulario.Text = "Editar";
                inputNombreCliente.Text = cliente.nombre_cliente;
                inputNombreContacto.Text = cliente.nombre_contacto;
                inputApellidoContacto.Text = cliente.apellido_contacto;
                representantes.SelectedIndex = (int) tienda.ListarEmpleado(cliente.codigo_empleado_rep_ventas.ToString()).Codigo_Empleado;
                inputTelefono.Text = cliente.telefono;
                inputFax.Text = cliente.fax;
                inputDireccion1.Text = cliente.linea_direccion1;
                inputDireccion2.Text = cliente.linea_direccion2;
                inputCiudad.Text = cliente.ciudad;
                inputPais.Text = cliente.pais;
                inputRegion.Text = cliente.region;
                inputCodigoPostal.Text = cliente.codigo_postal;
                //inputCodigoEmpleado.Text = cliente.codigo_empleado_rep_ventas.ToString();
                inputLimiteCredito.Text = cliente.limite_credito.ToString();
                codigoCliente = (int)cliente.codigo_cliente;

            } 

            inputNombreCliente.MaxLength = 50;
            inputNombreContacto.MaxLength = 30;
            inputApellidoContacto.MaxLength = 30;
            inputTelefono.MaxLength = 15;
            inputFax.MaxLength = 15;
            inputDireccion1.MaxLength = 50;
            inputDireccion2.MaxLength = 50;
            inputCiudad.MaxLength = 50;
            inputPais.MaxLength = 50;
            inputRegion.MaxLength = 50;
            inputCodigoPostal.MaxLength = 10;
            inputLimiteCredito.MaxLength = 18;
        }

        private int ValidaString(string cadena, int longitudMaxima, bool nulo)
        {
            int codigoError = 0;

            if (cadena.Length > longitudMaxima) codigoError = 1;
            if (cadena.Length < 1 && !nulo) codigoError = 2;
            return codigoError;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void BotonFormulario_Click(object sender, EventArgs e)
        {
            ToolTip TTIP = new ToolTip();
            // Variables para saber si todo ha ido correctamente
            bool valNombreCliente = false;
            bool valNombreContacto = false;
            bool valApellidos = false;
            bool valTelefono = false;
            bool valFax = false;
            bool valDireccion = false;
            bool valCiudad = false;
            bool valCodPostal = false;
            bool valCodEmple = false;
            bool valLimiteCred = false;
            bool valPass = false;

            // Validar el campo NombreCliente
            ComprobarNombreCliente(TTIP, out valNombreCliente);
            ComprobarNombreContacto(TTIP, out valNombreContacto);
            ComprobarApellidoContacto(TTIP, out valApellidos);
            ComprobarTelefono(TTIP, out valTelefono);
            ComprobarDireccion(TTIP, out valDireccion);
            ComprobarCiudad(TTIP, out valCiudad);
            ComprobarFax(TTIP, out valFax);
            ComprobarCodPostal(TTIP, out valCodPostal);
            ComprobarCodEmple(TTIP, out valCodEmple);
            ComprobarLimiteCred(TTIP, out valLimiteCred);
            ComprobarPass(TTIP, out valPass);
         
            if (valNombreCliente && valApellidos && valNombreContacto && valTelefono && valFax && 
                valDireccion && valCiudad && valCodPostal && valCodEmple && valLimiteCred && valPass &&
                DialogResult.Yes == MessageBox.Show("¿Deseas guardar los datos del cliente?", "Nuevo cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
            {
                
                if(cliente.codigo_cliente > 0)
                {
                    cliente.nombre_cliente = inputNombreCliente.Text;
                    cliente.nombre_contacto = inputNombreContacto.Text;
                    cliente.apellido_contacto = inputApellidoContacto.Text;
                    cliente.telefono = inputTelefono.Text;
                    cliente.fax = inputFax.Text;
                    cliente.linea_direccion1 = inputDireccion1.Text;
                    cliente.linea_direccion2 = inputDireccion2.Text;
                    cliente.ciudad = inputCiudad.Text;
                    cliente.pais = inputPais.Text;
                    cliente.region = inputRegion.Text;
                    cliente.codigo_postal = inputCodigoPostal.Text;
                    cliente.codigo_empleado_rep_ventas = tienda.ListarEmpleado(representantes.Text).Codigo_Empleado;
                    cliente.limite_credito = Convert.ToDecimal(inputLimiteCredito.Text);
                    tienda.ActualizarCliente(cliente);

                } else
                {
                    tienda.InsertarCliente(inputNombreCliente.Text, inputNombreContacto.Text, inputApellidoContacto.Text,
                        inputTelefono.Text, inputFax.Text, inputDireccion1.Text, inputDireccion2.Text, inputCiudad.Text,
                        inputRegion.Text, inputPais.Text, inputCodigoPostal.Text, Convert.ToInt32(representantes.Text),
                        Convert.ToDecimal(inputLimiteCredito.Text));
                }
                // Aperaciones a realizar con los datos del formulario.
                MessageBox.Show("Registro insertado correctamente.");
                
                this.Close();

            }
            else
            {
                string texto = "No se han guardado los datos";

                if (!valNombreCliente)
                {
                    texto += "\nNombre del cliente no es válido";
                }
                if (!valNombreContacto)
                {
                    texto += "\nNombre del contacto no es válido";
                }
                if (!valApellidos)
                {
                    texto += "\nApeliido del contacto no es válido";
                }
                if (!valTelefono)
                {
                    texto += "\nTelefono no es válido";
                }
                if (!valFax)
                {
                    texto += "\nFax del cliente no es válido";
                }
                if (!valDireccion)
                {
                    texto += "\nDirección del contacto no es válido";
                }
                if (!valCiudad)
                {
                    texto += "\nCiudad no es válido";
                }
                if (!valCodPostal)
                {
                    texto += "\nCodigo postal del cliente no es válido";
                }
                if (!valCodEmple)
                {
                    texto += "\nCódigo del empleado no es válido";
                }
                if (!valLimiteCred)
                {
                    texto += "\nLímite de crédito no es válido";
                }
                if (!valPass)
                {
                    texto += "\nContraseña inválida";
                }
                MessageBox.Show(texto);
            }
        }

        private void ComprobarNombreCliente( ToolTip TTIP, out bool valNombre)
        {
            String cadenaErrores = "";
            int resultado = ValidaString(inputNombreCliente.Text, 50, false);
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
                }
                //Mejorar la usabilidad mostrando el error
                inputNombreCliente.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputNombreCliente, cadenaErrores);
                errorProvider1.SetError(inputNombreCliente, cadenaErrores);
                valNombre = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                inputNombreCliente.BackColor = Color.LightGreen;
                TTIP.SetToolTip(inputNombreCliente, "");
                errorProvider1.SetError(inputNombreCliente, "");
                valNombre = true;
            }
        }

        private void ComprobarNombreContacto(ToolTip TTIP, out bool valNombreContacto)
        {
            String cadenaErrores = "";
            int resultado = ValidaString(inputNombreContacto.Text, 30, false);
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
                }
                //Mejorar la usabilidad mostrando el error
                inputNombreContacto.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputNombreContacto, cadenaErrores);
                errorProvider2.SetError(inputNombreContacto, cadenaErrores);
                valNombreContacto = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                inputNombreContacto.BackColor = Color.LightGreen;
                TTIP.SetToolTip(inputNombreContacto, "");
                errorProvider2.SetError(inputNombreContacto, "");
                valNombreContacto = true;
            }
        }

        private void ComprobarApellidoContacto(ToolTip TTIP, out bool valApellidos)
        {
            String cadenaErrores = "";
            int resultado = ValidaString(inputApellidoContacto.Text, 30, false);
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
                }
                //Mejorar la usabilidad mostrando el error
                inputApellidoContacto.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputApellidoContacto, cadenaErrores);
                errorProvider3.SetError(inputApellidoContacto, cadenaErrores);
                valApellidos = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                inputApellidoContacto.BackColor = Color.LightGreen;
                TTIP.SetToolTip(inputApellidoContacto, "");
                errorProvider3.SetError(inputApellidoContacto, "");
                valApellidos = true;
            }
        }

        private void ComprobarTelefono(ToolTip TTIP, out bool valTelefono)
        {
            String cadenaErrores = "";
            int resultado = ValidaString(inputTelefono.Text, 15, false);
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
                }
                //Mejorar la usabilidad mostrando el error
                inputTelefono.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputTelefono, cadenaErrores);
                errorProvider4.SetError(inputTelefono, cadenaErrores);
                valTelefono = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                inputTelefono.BackColor = Color.LightGreen;
                TTIP.SetToolTip(inputTelefono, "");
                errorProvider4.SetError(inputTelefono, "");
                valTelefono = true;
            }
        }

        private void ComprobarFax(ToolTip TTIP, out bool valFax)
        {
            String cadenaErrores = "";
            int resultado = ValidaString(inputFax.Text, 15, false);
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
                }
                //Mejorar la usabilidad mostrando el error
                inputFax.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputFax, cadenaErrores);
                errorProvider5.SetError(inputFax, cadenaErrores);
                valFax = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                inputFax.BackColor = Color.LightGreen;
                TTIP.SetToolTip(inputFax, "");
                errorProvider5.SetError(inputFax, "");
                valFax = true;
            }
        }

        private void ComprobarDireccion(ToolTip TTIP, out bool valDireccion)
        {
            String cadenaErrores = "";
            int resultado = ValidaString(inputDireccion1.Text, 50, false);
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
                }
                //Mejorar la usabilidad mostrando el error
                inputDireccion1.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputDireccion1, cadenaErrores);
                errorProvider6.SetError(inputDireccion1, cadenaErrores);
                valDireccion = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                inputDireccion1.BackColor = Color.LightGreen;
                TTIP.SetToolTip(inputDireccion1, "");
                errorProvider6.SetError(inputDireccion1, "");
                valDireccion = true;
            }
        }

        private void ComprobarCiudad(ToolTip TTIP, out bool valCiudad)
        {
            String cadenaErrores = "";
            int resultado = ValidaString(inputCiudad.Text, 50, false);
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
                //Mejorar la usabilidad mostrando el error
                inputCiudad.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputCiudad, cadenaErrores);
                errorProvider7.SetError(inputCiudad, cadenaErrores);
                valCiudad = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                inputCiudad.BackColor = Color.LightGreen;
                TTIP.SetToolTip(inputCiudad, "");
                errorProvider7.SetError(inputCiudad, "");
                valCiudad = true;
            }
        }

        private void ComprobarCodPostal(ToolTip TTIP, out bool valCodPostal)
        {
            String cadenaErrores = "";
            int resultado = ValidaString(inputCodigoPostal.Text, 10, false);
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
                //Mejorar la usabilidad mostrando el error
                inputCodigoPostal.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputCodigoPostal, cadenaErrores);
                errorProvider7.SetError(inputCodigoPostal, cadenaErrores);
                valCodPostal = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                inputCodigoPostal.BackColor = Color.LightGreen;
                TTIP.SetToolTip(inputCodigoPostal, "");
                errorProvider7.SetError(inputCodigoPostal, "");
                valCodPostal = true;
            }
        }

        private void ComprobarCodEmple(ToolTip TTIP, out bool valCodEmple)
        {
            String cadenaErrores = "";
            String codigoRepresentante = tienda.ListarEmpleado(representantes.Text).Codigo_Oficina;
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
                //Mejorar la usabilidad mostrando el error
                representantes.BackColor = Color.LightCoral;
                TTIP.SetToolTip(representantes, cadenaErrores);
                errorProvider8.SetError(representantes, cadenaErrores);
                valCodEmple = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                representantes.BackColor = Color.LightGreen;
                TTIP.SetToolTip(representantes, "");
                errorProvider8.SetError(representantes, "");
                valCodEmple = true;
            }
        }

        private void ComprobarLimiteCred(ToolTip TTIP, out bool valLimiteCred)
        {
            String cadenaErrores = "";
            int resultado = ValidaString(inputLimiteCredito.Text, 18, false);


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
                //Mejorar la usabilidad mostrando el error
                inputLimiteCredito.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputLimiteCredito, cadenaErrores);
                errorProvider8.SetError(inputLimiteCredito, cadenaErrores);
                valLimiteCred = false;
            }
            else if (Convert.ToDouble(inputLimiteCredito.Text) < 1000)
            {
                cadenaErrores = "El campo Limite de Credito debe ser mayor de 1000€" + Environment.NewLine;
                inputLimiteCredito.BackColor = Color.LightCoral;
                TTIP.SetToolTip(inputLimiteCredito, cadenaErrores);
                errorProvider8.SetError(inputLimiteCredito, cadenaErrores);
                valLimiteCred = false;
            }
            else
            {
                //Si el en el campo está todo correcto
                inputLimiteCredito.BackColor = Color.LightGreen;
                TTIP.SetToolTip(inputLimiteCredito, "");
                errorProvider8.SetError(inputLimiteCredito, "");
                valLimiteCred = true;
            }
        }

        private void ComprobarPass(ToolTip TTIP, out bool valPass)
        {
            String cadenaErrores = "";
            List<Empleado> empleados = tienda.ListarEmpelados();
            int resultado = ValidaString(inputNombreCliente.Text, 50, false);
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
                //Mejorar la usabilidad mostrando el error
                TxtPassword.BackColor = Color.LightCoral;
                TTIP.SetToolTip(TxtPassword, cadenaErrores);
                errorProvider9.SetError(TxtPassword, cadenaErrores);
                valPass = false;

            } else if (empleados.Exists(x => x.Email == email && x.Pass == CalculateMD5Hash(TxtPassword.Text)))
            {
                //Si el en el campo está todo correcto
                TxtPassword.BackColor = Color.LightGreen;
                TTIP.SetToolTip(TxtPassword, "");
                errorProvider9.SetError(TxtPassword, "");
                valPass = true;

            } else
            {
                valPass = false;
            }
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
