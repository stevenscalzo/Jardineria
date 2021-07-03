using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entidades;
using JardineriaWPF.Main;
using Negocio;

namespace JardineriaWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Tienda tienda = new Tienda();
        public MainWindow()
        {
            InitializeComponent();
            Error.Visibility = Visibility.Hidden;
        }

        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void minimazar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Acceder_Click(object sender, RoutedEventArgs e)
        {
            tienda.InsertarAdmin();
            List<Empleado> empleados = tienda.ListarEmpelados();

            if (empleados.Exists(x => x.Email == user.Text))
            {
                if (empleados.Exists(x => x.Email == user.Text && x.Pass == CalculateMD5Hash(pass.Password)))
                {
                    Principal principal = new Principal(user.Text);
                    principal.Show();
                    this.Content = WindowState.Minimized;
                }
                else
                {
                    Error.Visibility = Visibility.Visible;
                    Error.Content = "Error! Contraseña inválida, escriba nuevamente";
                }
            }
            else
            {
                Error.Visibility = Visibility.Visible;
                Error.Content = "Error! El usuario no es válido, escriba nuevamente";
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



