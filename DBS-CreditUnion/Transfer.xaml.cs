using BIZ;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Windows.Media.Animation;

namespace DBS_CreditUnion
{
    /// <summary>
    /// Interaction logic for Transfer.xaml
    /// </summary>
    public partial class Transfer : Window
    {
        private int accoNum = 0;
        private decimal overdraft = 0;
        private decimal receiverBalance = 0;
        SqlDataReader dr;
        DAO dao = new DAO();
        AddingToDB addToDB = new AddingToDB();
        RetrievingFromDB rtDB = new RetrievingFromDB();
        public Transfer()
        {
            InitializeComponent();
        }
        private void txtBal_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        //Grid Load Event
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            populateComboBox();
            txtDate.Text = DateTime.Now.ToString();
            txtRefNumber.Text =  (rtDB.selectMaxTransferID() + 1).ToString();
        }

        //Populating sender details
        public void MyAccountDetails()
        {
            accoNum = int.Parse(cboFrom.SelectedItem.ToString());
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

            txtBal.Text = bal.ToString("F2");
            txtAccType.Text = accType;

            if (accType.Equals("Savings"))
            {
                txtSortCode.IsEnabled = false;
                txtSortCode.Text = 101010.ToString(); ;
            }


        }

        //Populating ComboBox with Account Numbers
        public void populateComboBox()
        {
            SqlCommand cmd = dao.OpenCon().CreateCommand();
            cmd.CommandText = "usp_SelectAccNum";
            cmd.CommandType = CommandType.StoredProcedure;

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int acc = int.Parse(dr["AccountId"].ToString());
                cboFrom.Items.Add(acc);
                cboAccNumTT.Items.Add(acc);
                
               
            }
            dao.CloseCon();
        }

        //Populating Receiver Details according to ComboBox selected item
        private void cboAccNumTT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int accNum = int.Parse(cboAccNumTT.SelectedItem.ToString());
            if(accoNum == accNum)
            {
                MessageBox.Show("Sender and receiver account cannot be the same.");
                cboAccNumTT.SelectedIndex = 0;
            }
            else
            {
                string accType = "";

                SqlCommand cmd = dao.OpenCon().CreateCommand();
                cmd.CommandText = "uspMyAccountDetails";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@accNum", accNum);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    accType = dr["AccountType"].ToString();
                    receiverBalance = decimal.Parse(dr["InitialBalance"].ToString());
                }

                txtReceiverAccType.Text = accType;

                dao.CloseCon();
            }
          
        }

        //Transfering funds
        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {
            decimal newBal = 0;
            string senderAccType = txtAccType.Text;
            decimal bal = decimal.Parse(txtBal.Text);
            string receiverAccType = txtReceiverAccType.Text;
            int receiverAccNum = int.Parse(cboAccNumTT.SelectedItem.ToString());
            int sortCode = int.Parse(txtSortCode.Text);
            DateTime date = DateTime.Now;
            int refNumber = int.Parse(txtRefNumber.Text);
            decimal amount;
            try
            {
                amount = decimal.Parse(txtAmount.Text);
            }
            catch(FormatException)
            {
                throw new FormatException("Cannot convert string to decimal!");
            }

            if (amount <= 0)
            {
                MessageBox.Show("You must transfer a value greater than 0.");
                txtAmount.Clear();
                txtAmount.Focus();
            }
            else if (amount > bal + overdraft)
            {
                MessageBox.Show("Insufficient Funds!");
                txtAmount.Clear();
                txtAmount.Focus();
            }
            else
            {
                //Adding Transfer to Tranfer Table
                addToDB.newTransfer(accoNum, senderAccType, bal, receiverAccNum, receiverAccType, sortCode, amount, refNumber, date);
                //Confirming and Tidying up
                MessageBox.Show($"{amount} has been transferred to {receiverAccNum}.");

                //Internal transfer updates sender and receiver account
                if(sortCode == 101010)
                {
                    //Updating sender balance and overdraft in the database
                    newBal = senderNewBalance(bal, overdraft, amount);
                    overdraft = calculatingOverdraft(newBal);
                    addToDB.updateBalanceAndOverdraft(newBal, overdraft, accoNum);

                    //Updating receiver balance and overdraft in the database
                    bal = receiverBalance + amount;
                    overdraft = calculatingOverdraft(bal);
                    addToDB.updateBalanceAndOverdraft(bal, overdraft, receiverAccNum);
                }
                //External transfer, uptdates only sender account
                else
                {
                    //Updating sender balance and overdraft in the database
                    newBal = senderNewBalance(bal, overdraft, amount);
                    overdraft = calculatingOverdraft(newBal);
                    addToDB.updateBalanceAndOverdraft(newBal, overdraft, accoNum);            
                }

                //Tidying up
                txtBal.Text = newBal.ToString();
                txtReceiverAccType.Clear();
                txtSortCode.Clear();
                txtAmount.Clear();
                txtDate.Text = DateTime.Now.ToString();
                txtRefNumber.Text = (refNumber + 1).ToString();
                cboAccNumTT.SelectedIndex = 0;

            }

            
        }

        //Calculating sender new balance
        public decimal senderNewBalance(decimal bal, decimal overdraft, decimal amount)
        {
            decimal newBalance = 0;

            if (amount <= bal)
            {
                newBalance = bal - amount;
                return newBalance;
            }
            else newBalance = (bal + overdraft) - amount;

            return newBalance;
        }

        //Calculating new overdraft value
        public decimal calculatingOverdraft(decimal bal)
        {
            return bal / 10;
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

        private void cboFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MyAccountDetails();

        }
    }
}
