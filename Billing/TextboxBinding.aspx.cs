using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;  
namespace Billing
{
    public partial class TextboxBinding : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

              
                txtcategory.Text = dCat.Tables[0].Rows[0]["Category"].ToString();
            }
        }
        private static List<string> AutoFillProducts(string prefixText)
        {
            List<string> countryNames = new List<string>();
             DataSet dCat = objBs.BingCategory(txtcategory.Text);
            if(dCat.Tables[0].Rows.Count>0)
            {
                            countryNames.Add(sdr["ProductName"].ToString());
            }         
                    return countryNames;


                }

           
       

        protected void txtcategory_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtdescp_TextChanged(object sender, EventArgs e)
        {

        }
    }
} }