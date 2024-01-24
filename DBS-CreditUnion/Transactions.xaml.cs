using BIZ;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for Transations.xaml
    /// </summary>
    public partial class Transations : Window
    {
        private int accoNum = 0;
        RetrievingFromDB rtDB = new RetrievingFromDB();
        CollectionViewSource cs = new CollectionViewSource();
        
        public Transations()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        //Grid Load event
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            populatingWindow();
        }

        //Populating fields when grid is loaded
        private void populatingWindow()
        {
            cs.Source = rtDB.allTransactions().DefaultView;
            dgvTransactions.ItemsSource = cs.View;
            cboFirst.ItemsSource = Enum.GetValues(typeof(Filters));
            
        }

        //menu itens click event
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

        //Showing all transactions
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cs.Source = rtDB.allTransactions().DefaultView;
            dgvTransactions.ItemsSource = cs.View;
        }

        //Filtering using combobox
        private void cboFirst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = cboFirst.SelectedItem.ToString();
            switch (selectedItem)
            {
                case "Current":
                    selectedItem = "Current";
                    cs.Source = rtDB.filterByAccType(selectedItem);
                    dgvTransactions.ItemsSource = cs.View;
                    break;
                case "Savings":
                    cs.Source = rtDB.filterByAccType(selectedItem);
                    dgvTransactions.ItemsSource = cs.View;
                    break;
                case "Deposit":
                    cs.Source = rtDB.allWithdrawalsOrDeposits(selectedItem);
                    dgvTransactions.ItemsSource = cs.View;
                    break;
                case "Withdraw":
                    cs.Source = rtDB.allWithdrawalsOrDeposits(selectedItem);
                    dgvTransactions.ItemsSource = cs.View;
                    break;
                case "Transfer":
                    cs.Source = rtDB.allTransfers(selectedItem);
                    dgvTransactions.ItemsSource = cs.View;
                    break;
            }

        }

       
        
    }
}
 