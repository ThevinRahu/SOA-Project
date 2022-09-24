using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Client
{
    /// <summary>
    /// Interaction logic for ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        // Gets user details from the User class
        public ServicePage()
        {
            InitializeComponent();
            User user = User.Instance;
            userName.Content = "User Name : " + user.getName(); 
            userToken.Content ="Token: " + user.getToken();
        }

        // When the token is invalid the user will be automatically logged out
        private void logout(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            MessageBox.Show("User logged out SuccessFully");
            User user = User.Instance;
            user.setToken(0);
            this.NavigationService.Navigate(login);
        }

        // When the user token has expired the user will be logged out 
        public void forcelogout()
        {
            MessageBox.Show("Token Expired, You Will Be Logged Out Now!!");
            Login loginandReg = new Login();
            this.NavigationService.Navigate(loginandReg);
        }

        // The API call is made and all the services will be displayed if the token is valid 
        private void GetAll_Services(object sender, RoutedEventArgs e)
        {
            Services services;
            ProgressBar1.Dispatcher.Invoke(() => ProgressBar1.Value = 0, DispatcherPriority.Background);
            User user = User.Instance;
            string url = @"http://localhost:14698/api/AllServices/?token=" + user.getToken(); ;
            var clnt = new RestClient(url);
            var req = new RestRequest();
            var resp = clnt.Get(req);

            List<string> data = new List<string>();
            data = JsonConvert.DeserializeObject<List<string>>(resp.Content.ToString());
            if(data.Count != 0)
            {
                List<Services> gridData = new List<Services>();
                foreach (string value in data)
                {
                    services = JsonConvert.DeserializeObject<Services>(value);
                    // Checks if the user has a valid token
                    if (services.Status.Equals("Denied"))
                    {
                        forcelogout();
                        break;
                    }
                    gridData.Add(services);
                }
                // To show the progress bar
                for (int i = 1; i < 100; i++)
                {
                    ProgressBar1.Dispatcher.Invoke(() => ProgressBar1.Value = i, DispatcherPriority.Background);
                    Thread.Sleep(100);
                }
                // Placing the services in the GUI Table
                serviceInfo.ItemsSource = gridData;
            }
            else
            {
                MessageBox.Show("No Available Services!!");
            }
            
        }

        // When a search string is entered into the text box the matching services will be displayed on the Grid.
        // When there a no matching services it will displayed to user
        private async void Search(object sender, RoutedEventArgs e)
        {
            ProgressBar1.Dispatcher.Invoke(() => ProgressBar1.Value = 0, DispatcherPriority.Background);
            if (searchtxt.Text.Length == 0)
            {
                MessageBox.Show("Search Field Cannot be left Empty!!");
            }
            else
            {
                Task<List<string>> task = new Task<List<string>>(SearchDB);
                task.Start();
                for (int i = 25; i < 100; i++)
                {
                    ProgressBar1.Dispatcher.Invoke(() => ProgressBar1.Value = i, DispatcherPriority.Background);
                    Thread.Sleep(100);
                }
                if (task.Wait(TimeSpan.FromSeconds(40)))
                {
                    List<string> client = await task;
                    UpdateGui(client);
                }
                else
                {
                    MessageBox.Show("Search Timmed Out");

                }
            }
        }

        // Search the for the service with the matching description
        private List<string> SearchDB()
        {
            int token = 0;
            string srch = null;
            User user = User.Instance;
            this.Dispatcher.Invoke((Action)(() =>
            {
                srch = searchtxt.Text;
            }));
            token = user.getToken();
            string url = @"http://localhost:14698/api/Search/?description=" + srch + "&token=" + token;
            RestClient clnt = new RestClient(url);
            RestRequest request = new RestRequest();
            RestResponse response = clnt.Post(request);
            List<string> data = new List<string>();
            data = JsonConvert.DeserializeObject<List<string>>(response.Content.ToString());
            return data;
        }

        // Updates the GUI based on the results from the search
        private void UpdateGui(List<string> ti)
        {
            Services services;
            List<Services> gridData = new List<Services>();
            if(ti != null)
            {
                foreach (string line in ti)
                {
                    services = JsonConvert.DeserializeObject<Services>(line);
                    if (services.Status.Equals("Denied"))
                    {
                        forcelogout();
                        break;
                    }
                    gridData.Add(services);
                }
                serviceInfo.ItemsSource = gridData;
            }
            else
            {
                MessageBox.Show("No Available Services Match the Search!!");
            }
        }

        // When a service is selected the fields will be made visible based on the selected service
        private void servicePreview(object sender, MouseButtonEventArgs e)
        {
            Services selected = serviceInfo.SelectedItem as Services;
            if (selected != null)
            {
                if (selected.Name == null)
                {
                    MessageBox.Show("Selected Feild is Empty", Title = "Empty Feild Selected");
                }
                else
                {
                    nameLabel.Content = selected.Name;
                    descLabel.Content = selected.Description;
                    endPointLabel.Content = selected.APIEndPoint;
                    noOfOperandsLabel.Content = selected.NoOfOperands;
                }
                // Dynamic text fields and test button implementation
                if (selected.NoOfOperands == 2)
                {
                    txtBox1.Visibility = Visibility.Visible;
                    txtBox2.Visibility = Visibility.Visible;
                    txtBox3.Visibility = Visibility.Hidden;
                    testButton.Visibility = Visibility.Visible;
                }
                else if (selected.NoOfOperands == 3)
                {
                    txtBox1.Visibility = Visibility.Visible;
                    txtBox2.Visibility = Visibility.Visible;
                    txtBox3.Visibility = Visibility.Visible;
                    testButton.Visibility = Visibility.Visible;
                }
                else if (selected.NoOfOperands == 1)
                {
                    txtBox1.Visibility = Visibility.Visible;
                    txtBox2.Visibility = Visibility.Hidden;
                    txtBox3.Visibility = Visibility.Hidden;
                    testButton.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("Selected Field is Empty", Title = "Empty Field Selected");
            }
        }
        
        // Invoking the appropriate service and getting the result
        private void Test(object sender, RoutedEventArgs e)
        {
            if (nameLabel.Content.Equals("ADDTwoNumbers") && (checkFields(2) == false))
            {
                CheckResponse apiResp = callingEndpoint(2);

                if (apiResp.Status.Equals("Denied"))
                {
                    forcelogout();
                }
                else
                {
                    result.Content = apiResp.result;
                }
            }
            else if (nameLabel.Content.ToString().Equals("ADDThreeNumbers") && (checkFields(3) == false))
            {
                CheckResponse apiResp = callingEndpoint(3);

                if (apiResp.Status.Equals("Denied"))
                {
                    forcelogout();
                }
                else
                {
                    result.Content = apiResp.result;
                }
            }
            else if (nameLabel.Content.ToString().Equals("MulTwoNumbers") && (checkFields(2) == false))
            {
                CheckResponse apiResp = callingEndpoint(2);

                if (apiResp.Status.Equals("Denied"))
                {
                    forcelogout();
                }
                else
                {
                    result.Content = apiResp.result;
                }

            }
            else if (nameLabel.Content.ToString().Equals("MulThreeNumbers") && (checkFields(3) == false))
            {
                CheckResponse apiResp = callingEndpoint(3);

                if (apiResp.Status.Equals("Denied"))
                {
                    forcelogout();
                }
                else
                {
                    result.Content = apiResp.result;
                }

            }
            else
            {
                MessageBox.Show("Not a Valid Service!!");
            }
        }

        // Calling Appropriate Endpoint
        private CheckResponse callingEndpoint(int fields)
        {
            User user = User.Instance;
            string endpoint = endPointLabel.Content.ToString();
            string url;
            if (fields == 3)
            {
                url = endpoint + @"/" + txtBox1.Text + "/" + txtBox2.Text + "/" + txtBox3.Text + "/" + user.getToken();
            }
            else
            {
                url = endpoint + @"/" + txtBox1.Text + "/" + txtBox2.Text + "/" + user.getToken();
            }
            var clnt = new RestClient(url);
            var request = new RestRequest();
            var response = clnt.Get(request);
            CheckResponse apiResp = JsonConvert.DeserializeObject<CheckResponse>(response.Content.ToString());
            return apiResp;
        }
        
        // To check if the input fields are empty
        private Boolean checkFields(int fields) 
        {
            if (fields == 3)
            {
                if (txtBox1.Text.Length == 0 || txtBox2.Text.Length == 0 || txtBox3.Text.Length == 0)
                {
                    MessageBox.Show("TextFeilds Cannot be empty");
                    return true;
                }
                else
                    return false;

            }
            else
            {
                if (txtBox1.Text.Length == 0 || txtBox2.Text.Length == 0)
                {
                    MessageBox.Show("TextFeilds Cannot be empty");
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
