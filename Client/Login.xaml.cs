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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        readonly AuthenticatorInterface auth;
        User user;
        public Login()
        {
            InitializeComponent();
            var tcp = new NetTcpBinding();
            var URL = "net.tcp://localhost:8100/AuthenticationService";
            var chanFactory = new ChannelFactory<AuthenticatorInterface>(tcp, URL);
            auth = chanFactory.CreateChannel();
        }
        
        //user credentials validation for login and if invalid generate an error message
        private void loginClick(object sender, RoutedEventArgs e)
        {
            string Name = namebox.Text.Trim();
            string Pwd = pwdbox.Password;
            if (Name.Length == 0 || Pwd.Length == 0)
            {
                MessageBox.Show("Name or Password Feilds cannot be empty!!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // If the user is valid the result will have the token generated and it will be stored in the User Class
                // After that the user will navigated to the service page.
                int result = auth.Login(Name, Pwd);
                if (result > 0)
                {
                    user = User.Instance;
                    MessageBox.Show("Loggedin Succesfully!!");
                    user.setToken(result);
                    user.setName(Name);
                    ServicePage servicePage = new ServicePage();
                    this.NavigationService.Navigate(servicePage);
                }
                else
                {
                    //error message if no account is found
                    MessageBox.Show("Dont have an Account!!, Please Create an Account", "Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    namebox.Clear();
                    pwdbox.Clear();
                }
            }
        }
        
        //opens a seperate window to register
        private void registration(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
        }
    }
}
