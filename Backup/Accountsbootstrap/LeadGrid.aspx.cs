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
using System.IO;
using System.Net;

namespace Billing.Accountsbootstrap
{
    public partial class LeadGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string userid = "";
        string isadmin = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Session["UserID"].ToString();
            isadmin = Session["IsSuperAdmin"].ToString();
            if (!IsPostBack)
            {
               // if (isadmin == "3")
                {

                    DataSet dscheck = objBs.getstatusnew("4");
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        ddlStatus.DataSource = dscheck.Tables[0];
                        ddlStatus.DataValueField = "Statusid";
                        ddlStatus.DataTextField = "StatusName";
                        ddlStatus.DataBind();
                        ddlStatus.Items.Insert(0, "All");
                    }


                    DataSet dscheckk = objBs.getstatusnew("4", "CMP");
                    if (dscheck.Tables[0].Rows.Count > 0)
                    {
                        drpstus.DataSource = dscheckk.Tables[0];
                        drpstus.DataValueField = "Statusid";
                        drpstus.DataTextField = "StatusName";
                        drpstus.DataBind();
                        //  ddlstatus.Items.Insert(0, "Select Status");
                    }


                    gv_Employee.Visible = true;
                    GridView2.Visible = false;

                    DataSet ds_Emp = objBs.showentrygrid("L");
                    if (ds_Emp.Tables[0].Rows.Count > 0)
                    {
                        gv_Employee.DataSource = ds_Emp;
                        gv_Employee.DataBind();
                    }
                    else
                    {
                        gv_Employee.DataSource = null;
                        gv_Employee.DataBind();
                    }
                }
               // else
                {
                    //gv_Employee.Visible = true;
                    //GridView2.Visible = false;

                    //DataSet ds_Emp = objBs.showentrygrid(userid, isadmin, Convert.ToInt32(ddlStatus.SelectedValue));
                    //if (ds_Emp.Tables[0].Rows.Count > 0)
                    //{
                    //    gv_Employee.DataSource = ds_Emp;
                    //    gv_Employee.DataBind();
                    //}
                }
            }
        }

        protected void Search_click(object sender, EventArgs e)
        {
            if (txtsearch.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Search Text.Thank You!!!');", true);
                return;
            }
            else
            {
                DataSet ds_Emp = objBs.showentrygridsearchtype("L", ddlfilter.SelectedValue,txtsearch.Text);
                if (ds_Emp.Tables[0].Rows.Count > 0)
                {
                    gv_Employee.DataSource = ds_Emp;
                    gv_Employee.DataBind();
                }
                else
                {
                    gv_Employee.DataSource = null;
                    gv_Employee.DataBind();
                }
            }

        }

        protected void ddlChangeEvent_Status(object sender, EventArgs e)
        {

            DataSet ds_Emp = objBs.showentrygridsearch("L", ddlStatus.SelectedValue);
            if (ds_Emp.Tables[0].Rows.Count > 0)
            {
                gv_Employee.DataSource = ds_Emp;
                gv_Employee.DataBind();
            }
            else
            {
                gv_Employee.DataSource = null;
                gv_Employee.DataBind();
            }
            
            //if (isadmin == "3")
            //{
            //    gv_Employee.Visible = false;
            //    GridView2.Visible = true;

            //    DataSet ds_Emp = objBs.showentrygrid(userid, isadmin, Convert.ToInt32(ddlStatus.SelectedValue));
            //    if (ds_Emp.Tables[0].Rows.Count > 0)
            //    {
            //        GridView2.DataSource = ds_Emp;
            //        GridView2.DataBind();
            //    }
            //}
            //else
            //{
            //    gv_Employee.Visible = true;
            //    GridView2.Visible = false;

            //    DataSet ds_Emp = objBs.showentrygrid(userid, isadmin, Convert.ToInt32(ddlStatus.SelectedValue));
            //    if (ds_Emp.Tables[0].Rows.Count > 0)
            //    {
            //        gv_Employee.DataSource = ds_Emp;
            //        gv_Employee.DataBind();
            //    }
            //}
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("LeadMaster.aspx");

        }

        protected void btnexcel_click(object sender, EventArgs e)
        {
           // Response.Redirect("TaskDetails.aspx?name=bulk");
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            int rowIndex = gvr.RowIndex;


            if (e.CommandName == "Edit")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string firstArgVal = commandArgs[0];
                string secondArgVal = commandArgs[1];
                if (secondArgVal == "L")
                {
                    if (firstArgVal.ToString() != "")
                    {
                        if (firstArgVal.ToString() != "")
                        {
                            Response.Redirect("LeadMaster.aspx?Id=" + firstArgVal.ToString() + "&Name=EDT");

                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not Allow To Generate as Buyer.Thank You!!!');", true);
                    return;
                }
            }
            else if (e.CommandName == "NXT")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string firstArgVal = commandArgs[0];
                string secondArgVal = commandArgs[1];
                if (secondArgVal == "L")
                {
                    if (firstArgVal.ToString() != "")
                    {
                        if (firstArgVal.ToString() != "")
                        {
                            Response.Redirect("LeadMaster.aspx?LeadId=" + firstArgVal.ToString() + "&Name=NXT");

                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not Allow To Generate as Buyer.Thank You!!!');", true);
                    return;
                }
            }
            else if (e.CommandName == "CLNT")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string firstArgVal = commandArgs[0];
                string secondArgVal = commandArgs[1];
                if (secondArgVal == "L")
                {

                    if (firstArgVal.ToString() != "")
                    {
                        if (e.CommandArgument.ToString() != "")
                        {
                            Response.Redirect("PartyMaster.aspx?leadId=" + firstArgVal.ToString() + "&Name=LEAD");

                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not Allow To Generate as Buyer.Thank You!!!');", true);
                    return;
                }
            }
            else if (e.CommandName == "History")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DataSet dss = objBs.gethistoryforlead(e.CommandArgument.ToString());
                    if (dss.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = dss.Tables[0];
                        GridView1.DataBind();
                    }

                }
                mpe.Show();

            }
           
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gv_Employee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string firstArgVal = commandArgs[0];
                string secondArgVal = commandArgs[1];
                if (secondArgVal == "L")
                {
                    if (firstArgVal.ToString() != "")
                    {
                        if (firstArgVal.ToString() != "")
                        {
                            Response.Redirect("LeadMaster.aspx?Id=" + firstArgVal.ToString() + "&Name=EDT");

                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not Allow To Edit Because Lead Convert as Buyer.Thank You!!!');", true);
                    return;
                }
            }
            else if (e.CommandName == "NXT")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string firstArgVal = commandArgs[0];
                string secondArgVal = commandArgs[1];
                if (secondArgVal == "L")
                {
                    if (firstArgVal.ToString() != "")
                    {
                        if (firstArgVal.ToString() != "")
                        {
                            Response.Redirect("LeadMaster.aspx?Id=" + firstArgVal.ToString() + "&Name=NXT");

                        }

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not Allow To do Next Appointment Because Lead Convert as Buyer.Thank You!!!');", true);
                    return;
                }
            }
            else if (e.CommandName == "CLNT")
            {
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string firstArgVal = commandArgs[0];
                string secondArgVal = commandArgs[1];
                if (secondArgVal == "L")
                {

                    
                        if (firstArgVal.ToString() != "")
                        {
                            Response.Redirect("Vendor.aspx?leadId=" + firstArgVal.ToString()+"&status="+drpstus.SelectedValue+"&Name=LEAD");

                        }

                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Not Allow To Generate as Buyer.Thank You!!!');", true);
                    return;
                }
            }
            else if (e.CommandName == "History")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DataSet dss = objBs.gethistoryforlead(e.CommandArgument.ToString());
                    if (dss.Tables[0].Rows.Count > 0)
                    {
                        lblleaddate.Text = Convert.ToDateTime(dss.Tables[0].Rows[0]["leaddate"]).ToString("dd/MM/yyyy");
                        GridView1.DataSource = dss.Tables[0];
                        GridView1.DataBind();
                    }

                }
                mpe.Show();

            }
           
        }

        protected void gv_Employee_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        //protected void gridivew_rowbound(object sender, GridViewRowEventArgs e)
        //{

        //}

        protected void btnsearch_Click(object sender, EventArgs e)
        {
           
        }
    }
}