using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Diagnostics;

namespace DAL
{
    public class RetrievingFromDB : DAO
    {
        SqlDataReader dr;
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(); 
        public bool validUsername(string username)
        {
            int rowCount = 0;

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "stdFreeUsername";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);
            rowCount = (int)cmd.ExecuteScalar();
            CloseCon();

            return (rowCount == 0);
        }

        public int selectMaxID()
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "uspSelectMaxID";
            cmd.CommandType = CommandType.StoredProcedure;

            int accNum = int.Parse(cmd.ExecuteScalar().ToString());
            CloseCon();

            return accNum;
        }

        public void updateBalanceAndOverdraft(decimal newBal, decimal overdraft, int accNum)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "usp_UpdateBalanceAndOverdraft";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@newBal", newBal);
            cmd.Parameters.AddWithValue("@newOverdraft", overdraft);
            cmd.Parameters.AddWithValue("@accNum", accNum);

            cmd.ExecuteNonQuery();

            CloseCon();
        }

        public int selectMaxTransferID()
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "usp_SelectMaxTransfer";
            cmd.CommandType = CommandType.StoredProcedure;

            int transferId = int.Parse(cmd.ExecuteScalar().ToString());
            CloseCon();

            return transferId;
        }

        public int countingAccounts()
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "usp_CountAccount";
            cmd.CommandType = CommandType.StoredProcedure;

            int count = int.Parse(cmd.ExecuteScalar().ToString());
            CloseCon();

            return count;
        }

        public DataTable allAccounts()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "usp_AllAccounts";
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }

        public DataTable myTransactions(int accNum)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "usp_MyTransactions";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accNum", accNum);

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }

        public DataTable allTransactions()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "usp_AllTransactions";
            cmd.CommandType = CommandType.StoredProcedure;

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }

        public DataTable allWithdrawalsOrDeposits(string transType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "usp_WithdrawalsOrDeposits";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@transType", transType);

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }

        public DataTable allTransfers(string transType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "usp_allTransfer";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@transType", transType);

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }

        public DataTable filterByAccType(string accType)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();

            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "usp_FilterByAccType";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@accType", accType);

            da.SelectCommand = cmd;
            da.Fill(dt);
            CloseCon();

            return dt;
        }

        public string validLogn(string name, string password)
        {

                SqlCommand cmd = OpenCon().CreateCommand();
                cmd.CommandText = "usp_CheckPassword";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@user", name);
                cmd.Parameters.AddWithValue("@pass", password);
                string exists = cmd.ExecuteScalar().ToString();
                CloseCon();

            return exists;
            
        }

        public int myAccountID(string username)
        {
            SqlCommand cmd = OpenCon().CreateCommand();
            cmd.CommandText = "usp_SelectAccountNum";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@username", username);

            int accNum = int.Parse(cmd.ExecuteScalar().ToString());

            CloseCon();
            return accNum;
        }
        

    }
}
