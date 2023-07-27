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
    public partial class ViewTicket : System.Web.UI.Page
    {
        string LocationMasterID = "";

        DataSet ds = new DataSet();
        BSClass objbs = new BSClass();

        string Issuperadmin = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
          
            string TicketId = "";
            int Id = Convert.ToInt32(Session["Empid"]);
            Issuperadmin = Session["IsSuperAdmin"].ToString();
            if (!IsPostBack)
            {

                //txtdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //textid.Text = Session["VendorID"].ToString();

                //txtname.Text = Session["VendorName"].ToString();
                //txtphoneno.Text = Session["ContactNumber"].ToString();
                if (Issuperadmin == "1")
                {
                    ds = objbs.Select_ServicePerson_ApprovalTicket("","");
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlConcern.DataSource = ds;
                            ddlConcern.DataValueField = "Employee_ID";
                            ddlConcern.DataTextField = "Name";
                            ddlConcern.DataBind();
                        }
                        else
                        {
                            ddlConcern.DataSource = ds;
                            ddlConcern.DataValueField = "Employee_ID";
                            ddlConcern.DataTextField = "Name";
                            ddlConcern.DataBind();
                        }
                    }
                }
                else
                {
                    ds = objbs.Select_ServicePerson_ApprovalTicket("","");
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlConcern.DataSource = ds;
                            ddlConcern.DataValueField = "Employee_ID";
                            ddlConcern.DataTextField = "Name";
                            ddlConcern.DataBind();
                            ddlConcern.SelectedValue = Id.ToString();
                            ddlConcern.Enabled = false;
                        }
                        else
                        {
                            ddlConcern.DataSource = ds;
                            ddlConcern.DataValueField = "Employee_ID";
                            ddlConcern.DataTextField = "Name";
                            ddlConcern.DataBind();
                            ddlConcern.SelectedValue = Id.ToString();
                            ddlConcern.Enabled = false;
                        }
                    }
                }
                string bookingNo = Request.QueryString.Get("TicketId");
                if (bookingNo != null)
                {
                    if (!IsPostBack)
                    {
                        ds = objbs.GridTicketByID(bookingNo);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            lblId.Text = ds.Tables[0].Rows[0]["TicketId"].ToString();
                            txtdate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                            ddlPriorityStatus.SelectedValue = ds.Tables[0].Rows[0]["PriorityStatus"].ToString();
                            txttickect.Text = ds.Tables[0].Rows[0]["TicketNo"].ToString();
                            txtcomment.InnerText = ds.Tables[0].Rows[0]["Comments"].ToString();
                            //ddlConcern.SelectedValue = ds.Tables[0].Rows[0]["ServicePersonId"].ToString();
                            ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                            txtname.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                            txtphoneno.Text = ds.Tables[0].Rows[0]["Phno_No"].ToString();
                            txtSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                        }

                        #region History

                        DataSet gethistory = objbs.GridBind_HistoryTicket(bookingNo);
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

            }  
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime completedDate = DateTime.Now;
            int i = objbs.Update_Ticket(Convert.ToInt32(ddlConcern.SelectedValue), txtcomment.InnerText, Convert.ToInt32(lblId.Text), Convert.ToInt32(ddlStatus.SelectedValue), txtSubject.Text, Convert.ToInt32(ddlPriorityStatus.SelectedValue), completedDate , txtAdminComment.Text,ddlConcern.SelectedItem.Text);
            Response.Redirect("Ticket.aspx");

        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ticket.aspx");
        }
    }
}