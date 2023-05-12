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
using System.Windows.Shapes;

namespace frontend
{
    /// <summary>
    /// Interaction logic for StudentGrade.xaml
    /// </summary>
    public partial class StudentGrade : Window
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public void SetUsernameToLabel(string username)
        {
            labelUsername.Content = username;
        }
        public StudentGrade()
        {
            InitializeComponent();
        }
        static async Task<string> GetStudentGradesFromApi(string Token, string Username)
        {
            var response = string.Empty;
            var url = "http://localhost:5153/studentdata/" + Username;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", Token);
            HttpResponseMessage result = await client.GetAsync(url);
            response = await result.Content.ReadAsStringAsync();
            return response;
        }

        public void ShowMyGrades()
        {
            var data = Task.Run(() => GetStudentGradesFromApi(Token,Username));
            data.Wait();
            Console.WriteLine(data.Result);
            if (data.Result.Length > 3) //Result is not []
            {
                dynamic grades = JsonConvert.DeserializeObject(data.Result);

                gridMyGrades.ItemsSource = grades;//writes the data to DataGrid
            }
            else
            {
                MessageBox.Show("There is no grades");
            }
        }
    }
}
