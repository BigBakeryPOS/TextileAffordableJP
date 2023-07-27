using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;


namespace Billing.Accountsbootstrap
{
    public partial class CuttingProcessEntryGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string IsSuperAdmin = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                lblUser.Text = Session["UserName"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            IsSuperAdmin = Session["IsSuperAdmin"].ToString();

            if (!IsPostBack)
            {

                txtFromDate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);



                DataSet dsyear = objBs.GetYear();
                if (dsyear.Tables[0].Rows.Count > 0)
                {
                    drpyear.DataSource = dsyear.Tables[0];
                    drpyear.DataTextField = "yearname";
                    drpyear.DataValueField = "yearname";
                    drpyear.DataBind();
                }
                
                //DataSet ds = objBs.GetCuttingProcessEntryGrid(drpyear.SelectedValue);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    gvCuttingProcessEntry.DataSource = ds;
                //    gvCuttingProcessEntry.DataBind();
                //}

                //else
                //{
                //    gvCuttingProcessEntry.DataSource = null;
                //    gvCuttingProcessEntry.DataBind();
                //}

                DataSet ds = objBs.GetCuttingProcessEntryGrid(drpyear.SelectedValue, From, To);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvCuttingProcessEntry.DataSource = ds;
                    gvCuttingProcessEntry.DataBind();
                }

                else
                {
                    gvCuttingProcessEntry.DataSource = null;
                    gvCuttingProcessEntry.DataBind();
                }
            }
        }

        protected void drpyear_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet ds = objBs.GetCuttingProcessEntryGrid(drpyear.SelectedValue, From, To);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCuttingProcessEntry.DataSource = ds;
                gvCuttingProcessEntry.DataBind();
            }

            else
            {
                gvCuttingProcessEntry.DataSource = null;
                gvCuttingProcessEntry.DataBind();
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet ds = objBs.GetCuttingProcessEntryGrid(drpyear.SelectedValue, From, To);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCuttingProcessEntry.DataSource = ds;
                gvCuttingProcessEntry.DataBind();
            }

            else
            {
                gvCuttingProcessEntry.DataSource = null;
                gvCuttingProcessEntry.DataBind();
            }
        }


        protected void Add_Click(object sender, EventArgs e)
        {

            string button = string.Empty;
            button = btnadd.Text;
            {
                button = btnadd.Text;
                Response.Redirect("CuttingProcessEntry.aspx");
            }
            //  Response.Redirect("../Accountsbootstrap/customermaster.aspx");

        }

        protected void refresh_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/BuyerOrderMasterGrid.aspx");

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvCuttingProcessEntry.DataSource = ds;
                    gvCuttingProcessEntry.DataBind();
                }
                else
                {
                    gvCuttingProcessEntry.DataSource = null;
                    gvCuttingProcessEntry.DataBind();
                }
            }
            else
            {
                gvCuttingProcessEntry.DataSource = null;
                gvCuttingProcessEntry.DataBind();
            }
        }

        protected void gvCuttingProcessEntry_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("CuttingProcessEntry.aspx?TPE=VIEW&&ProcessEntryId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "Receive")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("CuttingProcessEntry.aspx?TPE=REC&&ProcessEntryId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "IssuePrint")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("CuttingProcessEntryPrint.aspx?ProcessEntryId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "ReceivePrint")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("CuttingProcessReceivePrint.aspx?ProcessEntryId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "ProPrint")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("ProcessOrderPrint.aspx?ProcessEntryId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "delete1")
            {
                if (IsSuperAdmin == "1")
                {
                    string[] Spl = e.CommandArgument.ToString().Split('&');

                    DataSet dsD = objBs.DeleteProcessEntryCheck2(Convert.ToInt32(Spl[0]));
                    if (dsD.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Finished Process Cannot be Delete.');", true);
                        return;
                    }
                    DataSet ds = objBs.DeleteProcessEntryCheck(Convert.ToInt32(Spl[0]));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int iSucess = objBs.DeleteProcessEntry(Convert.ToInt32(Spl[0]), Convert.ToInt32(Spl[1]), Convert.ToInt32(Spl[2]), Convert.ToInt32(Spl[3]));
                        Response.Redirect("CuttingProcessEntryGrid.aspx");
                    }
                    else
                    {
                        DataSet ds1 = objBs.DeleteProcessEntryCheck1(Convert.ToInt32(Spl[1]), Convert.ToInt32(Spl[3]));
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            int iSucess = objBs.DeleteProcessEntry(Convert.ToInt32(Spl[0]), Convert.ToInt32(Spl[1]), Convert.ToInt32(Spl[2]), Convert.ToInt32(Spl[3]));
                            Response.Redirect("CuttingProcessEntryGrid.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot be Delete,Please Check Process Entry.');", true);
                            return;
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Admin.');", true);
                    return;
                }
            }
        }
        protected void gvCuttingDetails2_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ToDate1 =Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "ToDate")).ToString("dd/MM/yyyy");
                string Date1 = DateTime.Now.ToString("dd/MM/yyyy");

                DateTime ToDate = DateTime.ParseExact(ToDate1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime Date = DateTime.ParseExact(Date1, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                int Iss = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssued").ToString());
                int Rec = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceived").ToString());
                int Dmg = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamaged").ToString());

                e.Row.Cells[7].ForeColor = System.Drawing.Color.White;

                if (ToDate < Date)
                {
                   // e.Row.Cells[7].BackColor = System.Drawing.ColorTranslator.FromHtml("#e83911d1");


                    if (Iss == (Rec + Dmg))
                    {
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Orange;
                    }
                }
                else
                {
                   

                    if (Iss != (Rec + Dmg))
                    {
                       // e.Row.Cells[7].BackColor = System.Drawing.ColorTranslator.FromHtml("#ec444c");
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                       // e.Row.Cells[7].BackColor = System.Drawing.ColorTranslator.FromHtml("#166907d1");
                        e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
                    }

                }
             

                // e.Row.Cells[0].BackColor = System.Drawing.Color.White;

            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }


    }
}


