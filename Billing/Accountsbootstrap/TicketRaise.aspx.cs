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
    public partial class TicketRaise : System.Web.UI.Page
    {
        string LocationMasterID = "";
        BSClass objbs= new BSClass();
        DataSet ds = new DataSet();
        string IsSuperadmin = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            string bookingNo = Request.QueryString.Get("TicketId");
            if (bookingNo != null)
            {
                if (!IsPostBack)
                {
                    ds = objbs.GridTicketByID(bookingNo);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataSet ds1 = objbs.Select_ServicePerson_ApprovalTicket(lbldept.Text, lbldesg.Text);
                        if (ds1 != null)
                        {
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                ddlConcern.DataSource = ds1;
                                ddlConcern.DataValueField = "employee_id";
                                ddlConcern.DataTextField = "Name";
                                ddlConcern.DataBind();
                                ddlConcern.Items.Insert(0, "Select Employee");
                            }
                            else
                            {
                                ddlConcern.DataSource = ds;
                                ddlConcern.DataValueField = "employee_id";
                                ddlConcern.DataTextField = "Name";
                                ddlConcern.DataBind();
                                ddlConcern.Items.Insert(0, "Select Employee");
                            }
                        }

                        lblId.Text = ds.Tables[0].Rows[0]["TicketId"].ToString();
                        txtdate.Text = ds.Tables[0].Rows[0]["Date"].ToString();
                        txttickect.Text = ds.Tables[0].Rows[0]["TicketNo"].ToString();
                        txtcomment.InnerText = ds.Tables[0].Rows[0]["Comments"].ToString();
                        //ddlConcern.SelectedValue = ds.Tables[0].Rows[0]["employee_id"].ToString();
                        ddlPriorityStatus.SelectedValue = ds.Tables[0].Rows[0]["PriorityStatus"].ToString();
                        ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                        btnSave.Text = "Update";
                        txtname.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                        txtphoneno.Text = ds.Tables[0].Rows[0]["Phno_No"].ToString();
                        txtSubject.Text = ds.Tables[0].Rows[0]["Subject"].ToString();
                    }
                }
            }
            else
            {
                if (!IsPostBack)
                {
                    DataSet get_tickno = objbs.TicketNoGen();
                    if (get_tickno != null)
                    {
                        if (get_tickno.Tables[0].Rows[0]["Entry"].ToString() != "")
                        {
                            string dd = get_tickno.Tables[0].Rows[0]["Entry"].ToString();
                            string tiketno = "TK - " + dd;
                            txttickect.Text = tiketno;
                        }
                        else
                        {
                            txttickect.Text = "TK - " + "1";
                        }
                    }

                    txtdate.Text = DateTime.Now.ToString();
                    textid.Text = Session["UserID"].ToString();
                    
                    lblempid.Text = Session["Empid"].ToString();

                    ds = objbs.Select_EmployeeName(lblempid.Text);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                txtname.Text = ds.Tables[0].Rows[0]["Name"].ToString(); ;
                                txtphoneno.Text = ds.Tables[0].Rows[0]["Phno_No"].ToString(); ;
                            }
                            else
                            {
                                txtname.Text = "Nil";
                                txtphoneno.Text = "0";
                            }
                        }

                        ds = objbs.Select_ServicePerson_ApprovalTicket(lbldept.Text, lbldesg.Text);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                ddlConcern.DataSource = ds;
                                ddlConcern.DataValueField = "employee_id";
                                ddlConcern.DataTextField = "Name";
                                ddlConcern.DataBind();
                                ddlConcern.Items.Insert(0, "Select Employee");

                                //  ddlConcern.SelectedValue = "1";
                            }
                            else
                            {
                                ddlConcern.DataSource = ds;
                                ddlConcern.DataValueField = "employee_id";
                                ddlConcern.DataTextField = "Name";
                                ddlConcern.DataBind();
                                ddlConcern.Items.Insert(0, "Select Employee");
                            }
                        }

                        for (int i = 0; i < ddlConcern.Items.Count; i++)
                        {
                            if (ddlConcern.Items[i].Value == lblempid.Text)
                            {
                                ddlConcern.Items.RemoveAt(i);
                            }
                        }
                    }
                }
                else
                {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not Allow To Raise Ticket.Thank You!!!.');", true);
                     return;
                     btnSave.Visible = false;
                }
            }
            
            if (btnSave.Text == "Save")
            {
                ddlStatus.SelectedValue = "1";
                ddlStatus.Enabled = false;

            }
            else if (btnSave.Text == "Update")
            {
                ddlStatus.Enabled = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (ddlConcern.SelectedValue == "Select Employee")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Select Concern Person.');", true);
                return;
            }


            if (btnSave.Text == "Update")
            {
                int iSuccess = objbs.Update_Ticket_Emp(Convert.ToInt32(ddlConcern.SelectedValue), txtcomment.InnerText, Convert.ToInt32(lblId.Text), Convert.ToInt32(ddlStatus.SelectedValue), txtSubject.Text, Convert.ToInt32(ddlPriorityStatus.SelectedValue),txtname.Text);
            }
            else
            {
                DataSet get_tickno = objbs.TicketNoGen();
                if (get_tickno != null)
                {
                    if (get_tickno.Tables[0].Rows[0]["Entry"].ToString() != "")
                    {
                        string dd = get_tickno.Tables[0].Rows[0]["Entry"].ToString();
                        string tiketno = "TK - " + dd;
                        txttickect.Text = tiketno;
                    }
                    else
                    {
                        txttickect.Text = "TK - " + "1";
                    }
                }

                int iSuccess = objbs.InsertTicket(Convert.ToInt32(textid.Text), txttickect.Text, Convert.ToDateTime(txtdate.Text), txtcomment.InnerText, Convert.ToInt32(ddlStatus.SelectedValue), Convert.ToInt32(ddlConcern.SelectedValue), txtSubject.Text, Convert.ToInt32(ddlPriorityStatus.SelectedValue),txtname.Text);
            }
            Response.Redirect("TicketGrid.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TicketGrid.aspx");
        }
    }
}