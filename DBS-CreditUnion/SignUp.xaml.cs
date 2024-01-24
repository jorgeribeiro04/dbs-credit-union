using System;
using System.Collections;
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
using BIZ;
using DAL;

namespace DBS_CreditUnion
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        HashCode hc = new HashCode();
        AddingToDB addToDB = new AddingToDB();
        RetrievingFromDB rtDB = new RetrievingFromDB();

        public SignUp()
        {
            InitializeComponent();
        }

        private void btnBackwards_SignUp(object sender, RoutedEventArgs e)
        {
            MainWindow back = new MainWindow();
            back.Show();
            this.Hide();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            if (rtDB.validUsername(username))
            {
                string password = hc.PassHash(pbPassword.Password);

                addToDB.addLoginDetais(username, password);

                MyAccount myAcc = new MyAccount();
                myAcc.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("This username is not available! Please, select a different one.");
                txtUsername.Focus();
                txtUsername.Clear();
            }
            
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            SignIn login = new SignIn();
            login.Show();
            this.Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exiting the application!");
            this.Close();
        }
    }
}
