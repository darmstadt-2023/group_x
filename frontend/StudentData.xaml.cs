using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
using System.Windows.Shapes;
using System.Net.Http;

namespace frontend
{
    /// <summary>
    /// Interaction logic for StudentData.xaml
    /// </summary>
    public partial class StudentData : Window
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public StudentData()
        {
            InitializeComponent();
        }

        static async Task<string> GetStudentDataFromApi(string Token, string Username)
        {
            var response = string.Empty;
            var url = "http://localhost:5153/student/"+Username;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", Token);
            HttpResponseMessage result = await client.GetAsync(url);
            response = await result.Content.ReadAsStringAsync();
            return response;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Token == null)
            {
                MessageBox.Show("You have to login first");
            }
            else
            {
                var data = Task.Run(() => GetStudentDataFromApi(Token, Username));
                data.Wait();
                Console.WriteLine(data.Result);
                if (data.Result.Length > 3) //Result is not []
                {
                    JObject j = JObject.Parse(data.Result);
                    tbStudentData.Text = j["fname"].ToString()+"\r\n";
                    tbStudentData.Text += j["lname"].ToString();
                }
                else
                {
                    MessageBox.Show("There is no books");
                }
            }
        }


    }
}
