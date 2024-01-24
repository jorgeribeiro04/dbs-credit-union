using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAO
    {
        SqlConnection con;
        public DAO()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBSCreditUnion_DB"].ConnectionString);
        }
        public SqlConnection OpenCon()
        {
            if (con.State == ConnectionState.Broken || con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }

        public void CloseCon()
        {
            if (con != null)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
    }
}
