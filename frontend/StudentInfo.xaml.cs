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

namespace frontend
{
    /// <summary>
    /// Interaction logic for StudentInfo.xaml
    /// </summary>
    public partial class StudentInfo : Window
    {
        public string Token { get; set; }
        public string Username { get; set; }

        public void SetUsername(string username)
        {
            labelUsername.Content = username;
        }
        public StudentInfo()
        {
            InitializeComponent();
        }

        private void btnMyData_Click(object sender, RoutedEventArgs e)
        {
            StudentData studentData = new StudentData();
            studentData.Token = Token;
            studentData.Username = Username;
            studentData.ShowMyData();
            studentData.Show();
        }
    }
}
