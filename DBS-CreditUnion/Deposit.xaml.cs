using BIZ;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace DBS_CreditUnion
{
    /// <summary>
    /// Interaction logic for Deposit.xaml
    /// </summary>
    public partial class Deposit : Window
    {
        private decimal overdraft = 0;
        private int accoNum = 0;
        SqlDataReader dr;
        DAO dao = new DAO();
        AddingToDB addToDb = new AddingToDB();
        RetrievingFromDB rtDB = new RetrievingFromDB();
        public Deposit()
        {
            InitializeComponent();
        }

        private void btnDeposit_Click(object sender, RoutedEventArgs e)
        {
            decimal depositAmt = 0;
            decimal balance = decimal.Parse(txtBalance.Text);
            try
            {
                depositAmt = decimal.Parse(txtAmount.Text);
            }
            catch (FormatException)
            {
                throw new FormatException("Cannot convert string to decimal! You must enter a number.");
            }

            if(depositAmt <= 0)
            {
                MessageBox.Show("Your deposit amount must be greater than 0.");
            }
            else
            {
                string accType = txtAccType.Text;
                decimal newBal = newBalance(balance, depositAmt);
                overdraft = newOverdraft(newBal);
                addToDb.updateBalanceAndOverdraft(newBal, overdraft, accoNum);
                addToDb.newDeposit(accoNum, accType, balance, depositAmt, newBal);
                MessageBox.Show($"Successfully deposited {depositAmt} in your account!\nNew Balance: {newBal}");
                txtAmount.Clear();
                txtBalance.Text = newBal.ToString();
            }
        }

        //Calculating new Balance
        private decimal newBalance(decimal bal, decimal depAmt)
        {
            return bal + depAmt;
        }

        //Calculating new overdraft value
        private decimal newOverdraft(decimal bal)
        {
            return bal / 10;
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
                    cboDeposit.Items.Add(acc);
                }
            }
            
            dao.CloseCon();
        }

        //Menu itens click events
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

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logged out successfully!");
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Hide();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exiting the application.");
            this.Close();
        }

        public void MyAccountDetails(int accNumber)
        {
            accoNum = accNumber;
                     
            string accType = "";
            decimal bal = 0;

            SqlCommand cmd2 = dao.OpenCon().CreateCommand();
            cmd2.CommandText = "uspMyAccountDetails";
            cmd2.CommandType = CommandType.StoredProcedure;

            cmd2.Parameters.AddWithValue("@accNum", accoNum);

            dr = cmd2.ExecuteReader();

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

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            populateComboBox();
        }

        private void cboDeposit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int number = int.Parse(cboDeposit.SelectedItem.ToString());
            MyAccountDetails(number);
        }
    }
}
