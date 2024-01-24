using BIZ;
using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DBS_CreditUnion
{
    /// <summary>
    /// Interaction logic for NewAccount.xaml
    /// </summary>
    public partial class NewAccount : Window
    {
       
        RetrievingFromDB rtDB = new RetrievingFromDB();
        public NewAccount()
        {
            InitializeComponent();

        }

        //Creating new account and sending to database
        private void btnCreateAcc_Click(object sender, RoutedEventArgs e)
        {   
            int accNum = int.Parse(txtAccNum.Text);
            string firstName = txtFn.Text;
            string surname = txtSn.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string address1 = txtAddress1.Text;
            string address2 = txtAddress2.Text;
            string city = txtCity.Text;
            string county = cboCounty.SelectedItem.ToString();
            string accType = "Current";
            string username = firstName + surname;
            if (rdoSavings.IsChecked == true)
            {
                accType = "Savings";
            }
            int sortCode = int.Parse(txtSortCode.Text);

            decimal initialBalance = Balance();
            if(initialBalance > 0)
            {
                decimal overdraft = OverdraftCalculation(initialBalance);

                Account newAcc = new Account(username, firstName, surname, email, phone, address1, address2, city, county, accType, sortCode, initialBalance, overdraft);
                newAcc.CreateAccount();
                MyAccount myAcc = new MyAccount();
                myAcc.txtAccNum.Text = accNum.ToString();
                MessageBox.Show("Account successfully created!");
                myAcc.Show();
                this.Hide();
            }
            else
            {
                    MessageBox.Show("Your initial balance must be greater than 0.");
                    txtInitialBalance.Focus();
                    txtInitialBalance.Clear();
                    txtOverdraftLimit.Text = "0";
            }

            
        }

        //Calculating the Overdraft Limit
        public decimal OverdraftCalculation(decimal initialBalance)
        {
            decimal overdraftLimit = 0;
            if(initialBalance == 0)
            {
                return overdraftLimit;
            }
            else overdraftLimit = initialBalance / 10;

            return overdraftLimit;
        }
        
        //Calculating and displaying overdraft value according to Balance value
        private void txtInitialBalance_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal overdraftLimit;
            if(rdoSavings.IsChecked == true)
            {
                overdraftLimit = 0;
                txtOverdraftLimit.Text = overdraftLimit.ToString();
            }
            else if (decimal.TryParse(txtInitialBalance.Text, out overdraftLimit))
            {
               txtOverdraftLimit.Text = OverdraftCalculation(overdraftLimit).ToString();
            }
            else txtOverdraftLimit.Clear();
        }

        //Populating Fields for when the form is initialized
        private void PopulatingFields()
        {
            txtSortCode.Text = ConfigurationManager.AppSettings.Get("SortCode");
            int nextAccount = rtDB.selectMaxID() + 1;
            cboCounty.ItemsSource = Enum.GetValues(typeof(County));
            txtAccNum.Text = nextAccount.ToString();
        }

        //Checking if Balance value is acceptable
        private decimal Balance()
        {
            decimal initialBalance = 0;
            try
            {
                initialBalance = decimal.Parse(txtInitialBalance.Text);
                return initialBalance;
            }
            catch (FormatException)
            {
                throw new FormatException("Cannot convert string to decimal! Please, enter a numerical value");
            }
        }

        //Setting overdraft limit to 0 if is a savings account
        private void rdoSavings_Checked(object sender, RoutedEventArgs e)
        {
            if(rdoSavings.IsChecked == true)
            {
                txtOverdraftLimit.Text = "0";
            }
        }

        //Setting overdraft if is a Current account
        private void rdoCurrent_Checked(object sender, RoutedEventArgs e)
        {
            if(rdoCurrent.IsChecked == true && txtInitialBalance.Text != "")
            {
                decimal overdraftLimit;
                if(decimal.TryParse(txtInitialBalance.Text, out overdraftLimit))
                {
                    txtOverdraftLimit.Text = OverdraftCalculation(overdraftLimit).ToString();
                }
                else txtOverdraftLimit.Text = "0";
            }
        }

        //Grid Load event
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PopulatingFields();
        }

        //Menu itens click events
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

        private void EditAccount_Click(object sender, RoutedEventArgs e)
        {
            MyAccount myAcc = new MyAccount();
            myAcc.Show();
            this.Hide();
        }

        private void DepositFunds_Click(object sender, RoutedEventArgs e)
        {

            Deposit dep = new Deposit();
            dep.Show();
            this.Hide();
        }

        private void WithdrawFunds_Click(object sender, RoutedEventArgs e)
        {
            Withdraw withd = new Withdraw();
            withd.Show();
            this.Hide();
        }

        private void TransferFunds_Click(object sender, RoutedEventArgs e)
        {
            Transfer trans = new Transfer();
            trans.Show();
            this.Hide();
        }

        private void ViewTransactions_Click(object sender, RoutedEventArgs e)
        {
            Transations transac = new Transations();
            transac.Show();
            this.Hide();
        }
    }
}
