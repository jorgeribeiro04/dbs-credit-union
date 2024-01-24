using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIZ
{
    public class Account
    {
        AddingToDB addToDb = new AddingToDB();

        public string username;
        public string firstname;
        public string surname;
        public string email;
        public string phone;
        public string address1;
        public string address2;
        public string city;
        public string county;
        public string accType;
        public int sortCode;
        public decimal initialBalance;
        public decimal overdraftLimit;

        public Account(string user, string fn, string sn, string email, string phone, string add1, string add2,string city, string county, string accType, int sortCode, decimal initBalance, decimal overdraft)
        {
            username = user;
            firstname = fn;
            surname = sn;
            this.email = email;
            this.phone = phone;
            address1 = add1;
            address2 = add2;
            this.city = city;
            this.county = county;
            this.sortCode = sortCode;
            this.accType = accType;
            initialBalance = initBalance;
            overdraftLimit = overdraft;
        }

        public void CreateAccount()
        {
            addToDb.CreateAccount(username, firstname, surname, email, phone,
                address1, address2, city, county, accType, sortCode, initialBalance,
                overdraftLimit);
        }
    }
}
