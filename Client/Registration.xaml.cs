using Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        readonly AuthenticatorInterface auth;
        public Registration()
        {

            InitializeComponent();
            var tcp = new NetTcpBinding();
            var URL = "net.tcp://localhost:8100/AuthenticationService";
            var chanFactory = new ChannelFactory<AuthenticatorInterface>(tcp, URL);
            auth = chanFactory.CreateChannel();
        }

        // When the user registers succeessfully the returned result will be the text phrase "Succesfully registered" from the 
        // the authentication service.The user will be shown a messagebox and the window will be closed automatically
        private void regClick(object sender, RoutedEventArgs e)
        {
            string name = nameBox.Text;
            string pwd = pwdBox.Password;
            string result = auth.Register(name, pwd);
            if (name.Length == 0 || pwd.Length == 0)
            {
                MessageBox.Show("Name or Password Feilds cannot be empty!!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (result.Equals("Succesfully registered"))
            {
                MessageBox.Show("User Registered Successfully, Please login with your credentials", Title = "Registration Successfull");
                this.Close();
            }
            else
            {
                // if the entered credentials already exist then this error message will be shown
                MessageBox.Show("Registration Unsuccessful!!", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
