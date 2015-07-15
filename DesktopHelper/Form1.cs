using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace DesktopHelper
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (e == null) throw new ArgumentNullException("e");
            label1.Text = GetPublicIp();
            GetInfoByCity(PublicData.magnitogorsk);
           
        }

        private string GetSymbol(string symbol)
        {
            if (symbol == "Celsius")
            {
                int value = int.Parse("00B0", NumberStyles.HexNumber);
                return char.ConvertFromUtf32(value);
            }
            return null;
        }


        void GetInfoByCity(string cityName)
        {
            try
            {
                string n = GetWeather(cityName).TempC;
                double m = Convert.ToDouble(Convert.ToDouble(n, CultureInfo.InvariantCulture.NumberFormat)) - 273; //Kelvin -> Celcius 
                label2.Text = string.Format("{0} C{1}", m, GetSymbol("Celsius"));
                label3.Text = GetWeather(cityName).Humidity;

            }
            catch (ArgumentException)
            {
                MessageBox.Show(@"Error in arguments exception");

            }
            
        }



        public WeatherTemplate GetWeather(string location)
        {

            WeatherTemplate conditions = WeatherTemplate.GetInstance(location);



            return conditions;

        }

        public string GetPublicIp()
        {
            try
            {
                String direction;
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                
                using (WebResponse response = request.GetResponse())
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        direction = stream.ReadToEnd();
                    }

                //Search for the ip in the html
                const int addressLenght = 9;
                int first = direction.IndexOf("Address: ", StringComparison.Ordinal) + addressLenght;
                int last = direction.LastIndexOf("</body>", StringComparison.Ordinal);
                direction = direction.Substring(first, last - first);

                return direction;
            }
            catch (Exception)
            {
                return "Error...";
            }

           
        }

        //private void Get()
        //{
            
        //    // The string representing the general input.
        //    string input;

        //    // The string that will represent the entered command.
        //    // Will be a substring of input.
            
        //        // Read the command.
        //        Console.Write(">");
        //        input = Console.ReadLine();

        //        command = input.Substring(0, 2);

        //        switch (command.ToUpper())
        //        {
        //            case "CC":
        //                {
        //                    Conditions conditions = new Conditions();
        //                    conditions = Weather.GetCurrentConditions(input.Substring(2, input.Length - 2));

        //                    if (conditions != null)
        //                    {
        //                        Console.WriteLine("Conditions: " + conditions.Condition);
        //                        Console.WriteLine("Temperature (F): " + conditions.TempF);
        //                        Console.WriteLine("Temperature (C): " + conditions.TempC);
        //                        Console.WriteLine("Humidity: " + conditions.Humidity);
        //                        Console.WriteLine("Wind: " + conditions.Wind);
        //                        Console.WriteLine();
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("There was an error processing the request.");
        //                        Console.WriteLine("Please, make sure you are using the correct location or try again later.");
        //                        Console.WriteLine();
        //                    }
        //                    break;
        //                }
        //            case "FC":
        //                {
        //                    List<Conditions> conditions = new List<Conditions>();
        //                    conditions = Weather.GetForecast(input.Substring(2, input.Length - 2));

        //                    if (conditions != null)
        //                    {
        //                        foreach (Conditions c in conditions)
        //                        {
        //                            Console.WriteLine("Day: " + c.DayOfWeek);
        //                            Console.WriteLine("Conditions: " + c.Condition);
        //                            Console.WriteLine("Temperature (High): " + c.High);
        //                            Console.WriteLine("Temperature (Low): " + c.Low);
        //                            Console.WriteLine();
        //                        }
        //                    }
        //                    else
        //                    {
                                
        //                    }
        //                    break;
        //                }
        //            case "EX":
        //                {
        //                    Console.WriteLine("Application closing...");
        //                    break;
        //                }
        //            default:
        //                {
        //                    Console.WriteLine("Unknown command.");
        //                    break;
        //                }
                
        //    }

        //    // Exit the application with exit code 0 (no errors).
        //    Environment.Exit(0);

        //}

        
    }
}
