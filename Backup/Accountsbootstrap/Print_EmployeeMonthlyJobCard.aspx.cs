using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Billing.Accountsbootstrap
{
    public partial class Print_EmployeeMonthlyJobCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Print", "javascript:window.print();", true);
            }

        }


        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {

        }
    }
}