using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;

namespace Billing.Accountsbootstrap
{
    public partial class TicketGrid : System.Web.UI.Page
    {
        BSClass objMC = new BSClass();
        DataSet ds = new DataSet();
        string Product_id = "";
        string Issuperadmin = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(Session["Empid"]);
            Issuperadmin = Session["IsSuperAdmin"].ToString();
            ds = objMC.GridTicket(Id,Issuperadmin);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gv_Product.DataSource = ds;
                gv_Product.DataBind();


                for (int i = 0; i < gv_Product.Rows.Count; i++)
                {
                    string status = ds.Tables[0].Rows[i]["Status"].ToString();
                    string statusPriority = ds.Tables[0].Rows[i]["PriorityStatus"].ToString();

                    if (statusPriority == "1")
                    {
                        gv_Product.Rows[i].BackColor = System.Drawing.Color.Red;
                    }
                    if (status == "4")
                    {
                        gv_Product.Rows[i].BackColor = System.Drawing.Color.LightGreen;
                    }
                }
            }
           
           
            if (Product_id != "" || Product_id != null)
            {
                if (!IsPostBack)
                {
                   
                }
            }
        }


        protected void gv_Product_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            chathistory.Visible = false;
            if (e.CommandName == "Edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("TicketRaise.aspx?TicketId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "History")
            {
                #region History
                chathistory.Visible = true;
                DataSet gethistory = objMC.GridBind_HistoryTicket(e.CommandArgument.ToString());
                if (gethistory.Tables[0].Rows.Count > 0)
                {
                    lblticketno.Text = gethistory.Tables[0].Rows[0]["ticketno"].ToString();
                    lblticketdate.Text = Convert.ToDateTime(gethistory.Tables[0].Rows[0]["date"]).ToString("dd/MM/yyyy HH:mm:ss tt");

                    gvChatDetails.DataSource = gethistory;
                    gvChatDetails.DataBind();
                }
                else
                {
                    lblticketno.Text = "No Ticket Found";
                    lblticketdate.Text = "Nil";

                    gvChatDetails.DataSource = null;
                    gvChatDetails.DataBind();
                }

                #endregion
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("TicketGrid.aspx");
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TicketRaise.aspx");
        }

        protected void gv_Product_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gv_Product_rowdatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (objMC.CheckIfClosed(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    ((Image)e.Row.FindControl("imdedit")).Visible = false;
                    ((Image)e.Row.FindControl("imgdisable")).Visible = true;
                }

            }
        }
    }
}