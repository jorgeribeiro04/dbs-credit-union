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
using System.Data.SqlClient;
using DAL;
using BIZ;

namespace DBS_CreditUnion
{
    /// <summary>
    /// Interaction logic for SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        RetrievingFromDB rtDB = new RetrievingFromDB();
        HashCode hc = new HashCode();
        public SignIn()
        {
            InitializeComponent();
        }

        private void btnBackwards_SignIn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Hide();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You are already on the Login screen!");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exiting the application!");
            this.Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            string username = txtUsername.Text;
            string password = hc.PassHash(pbPass.Password);
            string exists = rtDB.validLogn(username, password);

            if (exists.Equals("true"))
            {
                MessageBox.Show("Sucessfully logged in");
                MyAccount myAcc = new MyAccount();
                myAcc.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong username and passowrd. Please try again");
                txtUsername.Clear();
                pbPass.Clear();
            }
        }
    }
}
