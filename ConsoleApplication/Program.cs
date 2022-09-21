using Authenticator;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            User user = User.Instance;
            int token = -1;
            /*
             * The user will be in  a continous loop where if the user can decide to login or register, if user registers the user will be logged in auotmatically.
             * The user can log out and if the user wants to access the portal again the user will still be instide the loop.
             */
            while (token <= 0)
            {
                token = mainMenu();
                if (token > 0)
                {
                    user.setToken(token);
                    int option = 1;
                    while (option != 3)
                    {
                        option = serviceMenu();
                        switch (option)
                        {
                            case 1:
                                 publishService();
                                 continue;
                        
                            case 2:
                                 unpublishService();
                                continue;

                            case 3:
                                 token = -1;
                                Console.WriteLine("==============================================");
                                Console.WriteLine("User Logged Out");
                                Console.WriteLine("==============================================\n");
                                break;

                            default:
                                Console.WriteLine("==============================================");
                                Console.WriteLine("Invalid Option!!! Please Try Again !!!");
                                Console.WriteLine("==============================================\n");
                                continue;

                        }
                    }
                    continue;
                }
                else if (token == -1)
                {
                    Console.WriteLine("==============================================");
                    Console.WriteLine("User Regitraton Failed!! Please Try Again!!");
                    Console.WriteLine("==============================================\n");
                    continue;
                }
                else if (token == 0)
                {
                    Console.WriteLine("==============================================");
                    Console.WriteLine("Too Many Incorrect Attempts!!! Please Register Again!!!");
                    Console.WriteLine("==============================================\n");
                    continue;
                }
                else if (token == -98)
                {
                    break;
                }
                else if (token == -99)
                {
                    continue;
                }
            }
                       
        }
             
        public static int mainMenu()
        {
            InterfaceChannel iChannel = new InterfaceChannel();
            AuthenticatorInterface iserverChannel;
            Console.WriteLine("===== Login / Registration Portal =====\n");
            Console.WriteLine("Select an Option : \n1 -> Register\t2 -> Login\t3 -> Exit\n");
            Console.WriteLine("Enter Your Selection (1/2/3) :");
            int selection = Convert.ToInt32(Console.ReadLine());
            switch (selection)
            {
                case 1:
                    Console.WriteLine("Please Enter Your Name : ");
                    string Name = Console.ReadLine();
                    Console.WriteLine("Please Enter your Password: ");
                    string Pwd = Console.ReadLine();
                    iserverChannel = iChannel.generateChannel();
                    int token;
                    string result = iserverChannel.Register(Name, Pwd);
                    if (result.Equals("Succesfully registered"))
                    {
                        Console.WriteLine("==============================================");
                        Console.WriteLine("User Succesfully Registered !!");
                        Console.WriteLine("==============================================\n");
                        token = iserverChannel.Login(Name, Pwd);
                        return token;
                    }
                    else
                    {
                        return -1;
                    }

                case 2:
                    int count = 0;
                    while (count < 3)
                    {
                        Console.WriteLine("Please Enter Your Name : ");
                        Name = Console.ReadLine();
                        Console.WriteLine("Please Enter your Password: ");
                        Pwd = Console.ReadLine();
                        iserverChannel = iChannel.generateChannel();
                        token = iserverChannel.Login(Name, Pwd);
                        if (token > 0)
                        {
                            Console.WriteLine("==============================================");
                            Console.WriteLine("Successfully Logged in as " + Name );
                            Console.WriteLine("==============================================\n");
                            return token;
                        }
                        else
                        {
                            Console.WriteLine("==============================================");
                            Console.WriteLine("Username or password is incorrect!! Please Try Again!!");
                            Console.WriteLine("==============================================\n");
                            count++;
                        }
                    }
                    return 0;


                case 3:

                    Console.WriteLine("Exiting Portal ...");
                    return -98;

                default:
                    Console.WriteLine("==============================================");
                    Console.WriteLine("\nInvalid Selection Please try again");
                    Console.WriteLine("==============================================\n");
                    return -99;
            }
            
        }

        public static int serviceMenu()
        {
            Console.WriteLine("\n===== Select a Service =====\n");
            Console.WriteLine("1 -> Publish a service\t2 -> Unpublish a service\t3 -> logout");
            Console.WriteLine("Select an option (1/2/3):");
            int option = Convert.ToInt32(Console.ReadLine());
            return option;
        }
        public static void unpublishService()
        {
            User user = User.Instance;
            Console.WriteLine("\n===== Unpublish Portal =====\n");
            Console.WriteLine("Enter Service Endpoint : ");
            string endpoint = Console.ReadLine();

            //API call to unpublish the service with matching endpoint.
            string url = @"http://localhost:14698/api/UnPublish/?endpoint=" + endpoint + "&token=" + user.getToken();
            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Get(request);

            Console.WriteLine("==============================================");
            Console.WriteLine("Service Removed : " + response.Content.ToString());
            Console.WriteLine("==============================================");
        }

        public static void publishService()
        {
            /*
             * user inputs are taken which intialises a Services object which then be serialized and sent via the url 
             * and save as a published service in the services.txt file.
             */
            User user = User.Instance;
            Console.WriteLine("\n===== Publish Portal =====\n");
            Console.WriteLine("Enter Name Service : ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Service Description : ");
            string desc = Console.ReadLine();
            Console.WriteLine("Enter API Endpoint : ");
            string apiEndPoint = Console.ReadLine();
            Console.WriteLine("Enter Number of Operands : ");
            int noOfOperands = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter Operand Type");
            string operandType = Console.ReadLine();
            Services services = new Services(name, desc, apiEndPoint, noOfOperands, operandType);

            string json = JsonConvert.SerializeObject(services);

            //API call to publish the service
            string url = @"http://localhost:14698/api/Publish/?token=" + user.getToken();

            RestClient clinet = new RestClient(url);

            //Passing the json in the request body
            RestRequest request = new RestRequest().AddJsonBody(JsonConvert.SerializeObject(json));
            RestResponse response = clinet.Post(request);
            Console.WriteLine("==============================================");
            Console.WriteLine("Service Data Added: " + response.Content.ToString());
            Console.WriteLine("==============================================");
        }
        
        
    }
}
   
