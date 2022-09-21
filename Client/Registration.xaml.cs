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
        AuthenticatorInterface foob;
        User user;
        public Registration()
        {

            InitializeComponent();
            var tcp = new NetTcpBinding();
            var URL = "net.tcp://localhost:8100/AuthenticationService";
            var chanFactory = new ChannelFactory<AuthenticatorInterface>(tcp, URL);
            foob = chanFactory.CreateChannel();
        }

        private void regClick(object sender, RoutedEventArgs e)
        {
            string name = namebox.Text;
            string pwd = pwdbox.Password;
            string result = foob.Register(name, pwd);
            if (name.Length == 0 || pwd.Length == 0)
            {
                MessageBox.Show("Name or Password Feilds cannot be empty!!");
            }
            else if (result.Equals("Succesfully registered"))
            {
                MessageBox.Show("User Registered Successfully, Please login with your credentials", Title = "Registration Successfull");
                this.Close();
            }
            else
            {
                MessageBox.Show("Registration Unsuccessful!!");
            }
        }

    }
}
