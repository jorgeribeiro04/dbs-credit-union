using BIZ;
using Microsoft.SqlServer.Server;
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
using System.Data;
using System.Security.Policy;

namespace DBS_CreditUnion
{
    /// <summary>
    /// Interaction logic for Withdraw.xaml
    /// </summary>
    public partial class Withdraw : Window
    {
        private int accoNum = 0;
        SqlDataReader dr;
        DAO dao = new DAO();
        private decimal overdraft = 0;
        AddingToDB addToDB = new AddingToDB();
        public Withdraw()
        {
            InitializeComponent();
        }

        //Grid load event
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            populateComboBox();
        }

        //Withdraw button
        private void btnWithdrawal_Click(object sender, RoutedEventArgs e)
        {
            decimal balance = decimal.Parse(txtBalance.Text);
            decimal withdrawAmt = 0;
            decimal newBal = 0;
            string accType = txtAccType.Text;

            try
            {
                withdrawAmt = decimal.Parse(txtExpandedAmountAmount.Text);
            }
            catch (FormatException)
            {
                throw new FormatException("Cannot convert string to decimal! You must enter a number.");            
            }

            if (withdrawAmt <= 0)
            {
                MessageBox.Show("Your withdraw amount must be greater than 0!");
            }
            else if (withdrawAmt > balance + overdraft)
            {
                MessageBox.Show("Insufficient funds!");
            }
            else
            {
                newBal = newBalance(balance, overdraft, withdrawAmt);
                decimal newOverdraft = calculatingNewOverdraft(newBal);
                addToDB.updateBalanceAndOverdraft(newBal, newOverdraft, accoNum);
                addToDB.newWithdraw(accoNum,accType,balance,withdrawAmt,newBal);
                MessageBox.Show($"Amount Withdrawn: {withdrawAmt}\nNew Balance: {newBal}");
                txtExpandedAmountAmount.Clear();
                txtBalance.Text = newBal.ToString();
            }

            

        }

        //Pre-populating fields
        public void MyAccountDetails()
        {
            accoNum = int.Parse(cboWith.SelectedItem.ToString());

            string accType = "";
            decimal bal = 0;

            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "uspMyAccountDetails";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accoNum);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                accType = dr["AccountType"].ToString();
                bal = decimal.Parse(dr["InitialBalance"].ToString());
                overdraft = decimal.Parse(dr["OverdraftLimit"].ToString());
            }
            dao.CloseCon();

            txtBalance.Text = bal.ToString("F2");
            txtAccType.Text = accType;


        }

        public void populateComboBox()
        {
            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "usp_SelectAccNum";
            cmd.CommandType = CommandType.StoredProcedure;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int acc = int.Parse(dr["AccountId"].ToString());
                //Adding all the accounts to the combobox, except the senders account
                if (acc != accoNum)
                {
                    cboWith.Items.Add(acc);
                }
            }

            dao.CloseCon();
        }

        //Calculating value of new balance
        public decimal newBalance(decimal bal, decimal overdraft, decimal withdraw)
        {
            decimal newBal = 0;

            if (withdraw <= bal)
            {
                newBal = bal - withdraw;
                return newBal;
            }
            else newBal = (bal + overdraft) - withdraw;

            return newBal;
        }

        //Calculating new overdraft value
        public decimal calculatingNewOverdraft(decimal bal)
        {
            decimal newOverdraft = bal / 10;
            return newOverdraft;
        }

        //Menu itens click events
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logged out sucessfully!");
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exiting the application!");
            this.Close();
        }

        private void NewAccount_Click(object sender, RoutedEventArgs e)
        {
            NewAccount acc = new NewAccount();
            acc.Show();
            this.Hide();
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

     

        private void txtAccType_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void cboWith_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyAccountDetails();
        }
    }
}
