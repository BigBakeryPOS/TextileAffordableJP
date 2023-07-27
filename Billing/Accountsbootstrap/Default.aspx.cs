using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

namespace Billing.Accountsbootstrap
{
    public partial class Default : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = objBs.GetCustNameDefault();
            GridView1.DataSource = ds;
            GridView1.DataBind();



        }
    }
}