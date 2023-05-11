using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string token = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string u = txtUsername.Text;
            string p = txtPassword.Text;

            var data = Task.Run(() => LoginHttp(u, p));
            data.Wait();
            labelResult.Content = data.Result;
            string response = data.Result;
            if (response.Length < 50)
            {
                MessageBox.Show("No connection to server");
            }
            else
            {
                if (string.Compare(response, "false") == 0)
                {
                    labelResult.Content = "Wrong username/password";
                }
                else
                {
                    labelResult.Content = "OK";
                    token = "Bearer " + response;
                    StudentInfo studentInfo = new StudentInfo();
                    studentInfo.Token = token;
                    studentInfo.Username = u;
                    studentInfo.SetUsername(u);
                    studentInfo.Show();
                }

            }
        }
        static async Task<string> LoginHttp(string u, string p)
        {

            var response = string.Empty;

            User objectUser = new User(u, p);

            var json = JsonConvert.SerializeObject(objectUser);
            var postData = new StringContent(json, Encoding.UTF8, "application/json");

            var url = "http://localhost:5153/login/";

            var client = new HttpClient();
            try
            {
                HttpResponseMessage result = await client.PostAsync(url, postData);
                response = await result.Content.ReadAsStringAsync();
                return response;
            }
            catch (Exception)
            {
                return "-1";
            }

        }
    }
}
