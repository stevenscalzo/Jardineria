using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;
using System.Data.OleDb;
using System.Security.Cryptography;

namespace Jardineria
{
    public partial class Login : Form
    {
        Tienda tienda = new Tienda();
        public Principal principal;
        public Login()
        {
            InitializeComponent();
            this.MaximumSize = new Size(420, 400);
            this.MinimumSize = new Size(420, 400);
            TxtPassword.PasswordChar = '*';
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Entrar_Click(object sender, EventArgs e)
        {
            tienda.InsertarAdmin();
            List<Empleado> empleados = tienda.ListarEmpelados();
            panelError.Visible = false;

            if (empleados.Exists(x => x.Email == TxtUser.Text))
            {
                if (empleados.Exists(x => x.Email == TxtUser.Text && x.Pass == CalculateMD5Hash(TxtPassword.Text)))
                {
                    principal = new Principal(empleados.Find(x => x.Email.Contains(TxtUser.Text)).NombreCompleto,
                        (empleados.Find(x => x.Email.Contains(TxtUser.Text)).Email));
                    principal.Owner = this;
                    principal.Show();
                    this.Hide();
                }
                else
                {
                    panelError.Visible = true;
                    txtError.Text = "Error! Contraseña inválida, escriba nuevamente";
                }
            }
            else
            {
                panelError.Visible = true;
                txtError.Text = "Error! El usuario no es válido, escriba nuevamente";
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

        private void label3_Click(object sender, EventArgs e)
        {
            panelError.Visible = false;
        }
    }
}
