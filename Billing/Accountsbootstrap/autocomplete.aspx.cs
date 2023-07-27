using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Services;  
   

namespace Billing.Accountsbootstrap
{
    public partial class autocomplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<string> GetEmpNames(string empName)
        {
            List<string> Emp = new List<string>();
            string query = string.Format("select category from tblcategory category LIKE '%{0}%'", empName);
           // sqlcon.ConnectionString = @"Server=Server;Database=Accounts2;uid=sa;password=12345";
            using (SqlConnection con = new SqlConnection(@"Server=Server;Database=Accounts2;uid=sa;password=12345"))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Emp.Add(reader.GetString(0));
                    }
                }
            }
            return Emp;
        }  
    }
}