using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Examen_Opdracht_.NET_ADVANCED.Data;
using Examen_Opdracht_.NET_ADVANCED.Models;

namespace Examen_Opdracht_.NET_ADVANCED.Users
{
   
    public partial class RegisterWindow : Window
    {
        //private MyDBContext context;
        private MyDBContext context = new MyDBContext();


        public RegisterWindow()
        {
            Initializer.DbSetInitializer(context);

            context = new MyDBContext();
            InitializeComponent();

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Password;
                string confirmPassword = txtConfirmPassword.Password;
                string email = txtEmail.Text;

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(email))
                {
                    throw new ArgumentException("You have to fill in all the boxes.");
                }

                if (password != confirmPassword)
                {
                    throw new ArgumentException("The passwords are not the same.");
                }

                if (!IsValidEmail(email))
                {
                    throw new ArgumentException("The format of email is not correct.");
                }

                if (context.Users.Any(u => u.Username == username))
                {
                    throw new ArgumentException("This username is already in use.");
                }

                if (context.Users.Any(u => u.Email == email))
                {
                    throw new ArgumentException("This email is already in use.");
                }

                var newUser = new User
                {
                    Username = username,
                    PasswordHash = HashPassword(password),
                    Email = email,
                    IsAdmin = false
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                MessageBox.Show("¡You are registerd! Now you can log in.", "Register", MessageBoxButton.OK, MessageBoxImage.Information);


                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();

                Close();

               
            }
            catch (ArgumentException ex)
            {
                txtError.Text = ex.Message;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

            Close();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                string pattern = @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$";
                return Regex.IsMatch(email, pattern);
            }
            catch
            {
                return false;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();

            loginWindow.Show();

            Close();
        }


    }
}
