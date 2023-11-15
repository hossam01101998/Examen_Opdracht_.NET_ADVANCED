using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace Examen_Opdracht_.NET_ADVANCED.Users
{
    /// <summary>
    /// Lógica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MyDBContext context = new MyDBContext();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Password;

                var user = context.Users.FirstOrDefault(u => u.Username == username);

                if (user != null)
                {
                    if (VerifyPassword(password, user.PasswordHash))
                    {
                        MessageBox.Show("Login successful!", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                        MainWindow mainWindow = new MainWindow();

                        mainWindow.Show();

                        Close(); 
                    }
                    else
                    {
                        throw new ArgumentException("Invalid password.");
                    }
                }
                else
                {
                    throw new ArgumentException("User not found.");
                }
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
        }

        private bool VerifyPassword(string enteredPassword, string hashedPassword)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convierte la contraseña ingresada a un arreglo de bytes
                byte[] enteredBytes = Encoding.UTF8.GetBytes(enteredPassword);

                // Calcula el hash SHA-256 de la contraseña ingresada
                byte[] enteredHash = sha256.ComputeHash(enteredBytes);

                // Convierte el hash calculado a una cadena hexadecimal
                string enteredHashString = Convert.ToHexString(enteredHash);

                // Compara el hash calculado con el hash almacenado en la base de datos
                return enteredHashString.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
