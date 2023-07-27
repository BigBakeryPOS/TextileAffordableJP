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
using System.IO;
using System.Globalization;
using System.Net.Mail;
using System.Configuration;
using System.Resources;
using System.Threading;
using System.Reflection;

namespace Billing.Accountsbootstrap
{
    public partial class SalesGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string Sort_Direction = "LedgerName ASC";
        string Sort_Direction1 = "Bill_To ASC";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");


            sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
                ViewState["SortExpr"] = Sort_Direction;
                ViewState["SortExpr"] = Sort_Direction1;
                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();
                DataSet ds = objBs.CustomerSalesGird(Convert.ToInt32(Session["UserID"].ToString()), "tblSales_" + sTableName, "tblDayBook_" + sTableName, sTableName, drpbilltypes.SelectedValue);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvsales.DataSource = ds;
                        gvsales.DataBind();
                    }
                    else
                    {
                        gvsales.DataSource = null;
                        gvsales.DataBind();
                    }
                }
                //DataSet dsCategory = objBs.selectbillno(sTableName);
                //if (dsCategory.Tables[0].Rows.Count > 0)
                //{
                //    ddlbillno.DataSource = dsCategory.Tables[0];
                //    ddlbillno.DataTextField = "Customer";
                //    ddlbillno.DataValueField = "CustomerID";
                //    ddlbillno.DataBind();
                //    ddlbillno.Items.Insert(0, "Search By");


                //}

                //DataSet dsCategory1 = objBs.CustomerNameID(Convert.ToInt32(Session["UserID"].ToString()),"tblSales_" + sTableName);
                //if (dsCategory1.Tables[0].Rows.Count > 0)
                //{
                //    ddlcustomername.DataSource = dsCategory1.Tables[0];
                //    ddlcustomername.DataTextField = "Area";

                //    //ddlcustomername.DataValueField = "CustomerID";
                //    ddlcustomername.DataBind();
                //    ddlcustomername.Items.Insert(0, "Select Customer Name and Area");


                //}


            }
        }
        protected void drpbilltype(object sender, EventArgs e)
        {
            DataSet ds = objBs.CustomerSalesGird(Convert.ToInt32(Session["UserID"].ToString()), "tblSales_" + sTableName, "tblDayBook_" + sTableName, sTableName, drpbilltypes.SelectedValue);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvsales.DataSource = ds;
                    gvsales.DataBind();
                }
                else
                {
                    gvsales.DataSource = null;
                    gvsales.DataBind();
                }
            }

        }


        protected void gvsales_rowdatabound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (e.Row.RowIndex == 0)
            //        e.Row.Style.Add("height", "100px");
            //}
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //if (txtpas.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Password.Thank You!!!.');", true);
            //    return;
            //}
            //else
            //{
            //    DataSet dpass = objBs.securitycheck(sTableName, txtpas.Text);
            //    if (dpass.Tables[0].Rows.Count > 0)
            //    {


            //        Response.Redirect("../Accountsbootstrap/cashsales.aspx");
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Wrong Password.Thank You!!!.');", true);
            //        return;
            //    }


            //}
            Response.Redirect("../Accountsbootstrap/cashsales.aspx");

        }
        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();

            if (ddlbillno.SelectedValue == "0")
            {
                ds = objBs.CustomerSalesGird(Convert.ToInt32(Session["UserID"].ToString()), "tblSales_" + sTableName, "tblDayBook_" + sTableName, sTableName, drpbilltypes.SelectedValue);
            }
            else
            {
                ds = objBs.searchSalesgrid(txtAutoName.Text, Convert.ToInt32(ddlbillno.SelectedValue), Convert.ToInt32(Session["UserID"].ToString()), "tblsales_" + sTableName, "tblDayBook_" + sTableName, sTableName);
            }

            if (Session["SortedView"] != null)
            {
                gvsales.DataSource = Session["SortedView"];
                gvsales.DataBind();
            }
            else
            {
                gvsales.DataSource = ds;
                gvsales.PageIndex = e.NewPageIndex;
                // gvsales.DataBind();
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    gvsales.DataSource = ds;
                    gvsales.PageIndex = e.NewPageIndex;
                    gvsales.DataBind();
                }
                else
                {
                    gvsales.DataSource = null;
                    gvsales.DataBind();
                }
            }

        }

        protected void refresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("salesGrid.aspx");
            //lblUser.Text = Session["UserName"].ToString();
            //lblUserID.Text = Session["UserID"].ToString();
            //sTableName = Session["User"].ToString();
            //txtAutoName.Text = "";
            //DataSet ds = objBs.CustomerSalesGird(Convert.ToInt32(Session["UserID"].ToString()), "tblSales_" + sTableName, "tblDayBook_" + sTableName);
            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        gvsales.DataSource = ds;
            //        gvsales.DataBind();
            //    }
            //    else
            //    {
            //        gvsales.DataSource = null;
            //        gvsales.DataBind();
            //    }
            //}
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            //string sCustomer = ddlcustomername.SelectedValue;
            //string[] sFull = sCustomer.Split('-');
            //string sCustomerName = sFull[0].ToString();
            //string sArea = sFull[1].ToString();

            //DataSet ds = objBs.CustomerSalesGirdnamearea(Convert.ToInt32(ddlbillno.SelectedValue));
            ////DataSet ds = objBs.CustomerSalesGirdbillNo(ddlbillno.SelectedValue);
            //gvsales.DataSource = ds;
            //gvsales.DataBind();

            if (txtAutoName.Text != "" || ddlbillno.SelectedValue != "0")
            {
                if (ddlbillno.SelectedValue == "1" || ddlbillno.SelectedValue == "4")
                {

                    int result;
                    if (int.TryParse(txtAutoName.Text, out result))
                    {
                        ds = objBs.searchSalesgrid(txtAutoName.Text, Convert.ToInt32(ddlbillno.SelectedValue), Convert.ToInt32(Session["UserID"].ToString()), "tblsales_" + sTableName, "tblDayBook_" + sTableName, sTableName);
                        if (ds != null)
                        {
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                gvsales.DataSource = ds;
                                gvsales.DataBind();
                            }
                            else
                            {
                                gvsales.DataSource = null;
                                gvsales.DataBind();
                            }
                        }
                        else
                        {
                            gvsales.DataSource = null;
                            gvsales.DataBind();
                        }

                    }
                    else
                    {
                        gvsales.DataSource = null;
                        gvsales.DataBind();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter only Number')", true);
                        txtAutoName.Focus();
                        return;
                    }
                }
                else
                {
                    ds = objBs.searchSalesgrid(txtAutoName.Text, Convert.ToInt32(ddlbillno.SelectedValue), Convert.ToInt32(Session["UserID"].ToString()), "tblsales_" + sTableName, "tblDayBook_" + sTableName, sTableName);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gvsales.DataSource = ds;
                            gvsales.DataBind();
                        }
                        else
                        {
                            gvsales.DataSource = null;
                            gvsales.DataBind();
                        }
                    }
                    else
                    {
                        gvsales.DataSource = null;
                        gvsales.DataBind();
                    }
                }

            }

            //else if (txtAutoName.Text != "" || ddlbillno.SelectedValue != "0")
            //    {
            //        DataSet ds = objBs.searchSalesgrid(txtAutoName.Text, Convert.ToInt32(ddlbillno.SelectedValue), Convert.ToInt32(Session["UserID"].ToString()), "tblsales_" + sTableName, "tblDayBook_" + sTableName);

            //        if (ds != null)
            //        {
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                gvsales.DataSource = ds;
            //                gvsales.DataBind();
            //            }
            //            else
            //            {
            //                gvsales.DataSource = null;
            //                gvsales.DataBind();
            //            }
            //        }
            //        else
            //        {
            //            gvsales.DataSource = null;
            //            gvsales.DataBind();
            //        }
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select search type Or Enter search value.')", true);

            //        return;
            //    }
        }




        protected void gvsales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editt")
            {
                Response.Redirect("cashsales.aspx?iSalesID=" + e.CommandArgument.ToString());

                //if (txtpas.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Password.Thank You!!!.');", true);
                //    return;
                //}
                //else
                //{
                //    DataSet dpass = objBs.securitycheck(sTableName, txtpas.Text);
                //    if (dpass.Tables[0].Rows.Count > 0)
                //    {


                //        if (e.CommandArgument.ToString() != "")
                //        {
                //            Response.Redirect("cashsales.aspx?iSalesID=" + e.CommandArgument.ToString());
                //        }
                //    }
                //    else
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Wrong Password.Thank You!!!.');", true);
                //        return;
                //    }
                //}
            }
            else if (e.CommandName == "Email")
            {
                SendEmail(Convert.ToString(e.CommandArgument), "", "", "", "");
            }
            else if (e.CommandName == "delete")
            {
                int iSucess = objBs.DeleteSales("tblSales_" + sTableName, e.CommandArgument.ToString(), "tblDayBook_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, e.CommandName, sTableName);

                int isalesid = Convert.ToInt32(e.CommandArgument.ToString());

                DataSet dsTransSales = objBs.GetTransSales("tblTransSales_" + sTableName, e.CommandArgument.ToString());
                if (dsTransSales.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsTransSales.Tables[0].Rows.Count; i++)
                    {
                        string sddlCat = dsTransSales.Tables[0].Rows[i]["CategoryID"].ToString();
                        string sddlDef = dsTransSales.Tables[0].Rows[i]["SubCategoryID"].ToString();
                        string sQty = dsTransSales.Tables[0].Rows[i]["Quantity"].ToString();
                        int iSuccs = UpdateEditStock(Convert.ToInt32(sddlCat), Convert.ToInt32(sddlDef), Convert.ToInt32(sQty));

                    }
                }

                int iTransDelete = objBs.DeleteTransSales("tblTransSales_" + sTableName, e.CommandArgument.ToString());

                Response.Redirect("salesgrid.aspx");
            }

            else if (e.CommandName == "print")
            {
                Response.Redirect("Print_Sales_Invoice_Packing.aspx?iSalesID=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "printtrans")
            {
                Response.Redirect("Print_Sales_Transport.aspx?iSalesID=" + e.CommandArgument.ToString());
            }
            else if (e.CommandName == "DC")
            {
                Response.Redirect("Print_sales_packing.aspx?iSalesID=" + e.CommandArgument.ToString());
            }

            else if (e.CommandName == "btnp")
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                string iinvoiceid = (e.CommandArgument.ToString());
                TextBox txtComments = (TextBox)gvsales.Rows[RowIndex].FindControl("pack");
                int updateComments = objBs.updateCommentsInvoice(sTableName, iinvoiceid, txtComments.Text, "Packing");

                DataSet ds = objBs.CustomerSalesGird(Convert.ToInt32(Session["UserID"].ToString()), "tblSales_" + sTableName, "tblDayBook_" + sTableName, sTableName, drpbilltypes.SelectedValue);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvsales.DataSource = ds;
                        gvsales.DataBind();
                    }
                    else
                    {
                        gvsales.DataSource = null;
                        gvsales.DataBind();
                    }
                }

                // Response.Redirect("Packing_Print.aspx?iSalesID=" + e.CommandArgument.ToString());
                /// Response.Redirect("InvoiceGrid.aspx");
            }
            else if (e.CommandName == "btnc")
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                string iinvoiceid = (e.CommandArgument.ToString());
                TextBox txtComments = (TextBox)gvsales.Rows[RowIndex].FindControl("checkk");
                int updateComments = objBs.updateCommentsInvoice(sTableName, iinvoiceid, txtComments.Text, "CheckSta");

                DataSet ds = objBs.CustomerSalesGird(Convert.ToInt32(Session["UserID"].ToString()), "tblSales_" + sTableName, "tblDayBook_" + sTableName, sTableName, drpbilltypes.SelectedValue);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvsales.DataSource = ds;
                        gvsales.DataBind();
                    }
                    else
                    {
                        gvsales.DataSource = null;
                        gvsales.DataBind();
                    }
                }
                /// Response.Redirect("InvoiceGrid.aspx");
            }

            else if (e.CommandName == "btnr")
            {
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                int RowIndex = gvr.RowIndex;

                string iinvoiceid = (e.CommandArgument.ToString());
                TextBox txtComments = (TextBox)gvsales.Rows[RowIndex].FindControl("recheck");
                int updateComments = objBs.updateCommentsInvoice(sTableName, iinvoiceid, txtComments.Text, "Recheck");

                DataSet ds = objBs.CustomerSalesGird(Convert.ToInt32(Session["UserID"].ToString()), "tblSales_" + sTableName, "tblDayBook_" + sTableName, sTableName, drpbilltypes.SelectedValue);
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvsales.DataSource = ds;
                        gvsales.DataBind();
                    }
                    else
                    {
                        gvsales.DataSource = null;
                        gvsales.DataBind();
                    }
                }

                /// Response.Redirect("InvoiceGrid.aspx");
            }

        }

        protected void txtAutoName_TextChanged(object sender, EventArgs e)
        {
            //DataSet ds = objBs.autoFilterSalesGrid(txtAutoName.Text);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    gvsales.DataSource = ds;
            //    gvsales.DataBind();
            //}
        }

        private int UpdateEditStock(int iCat, int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;
            //if (sTableName == "admin")
            //{
            DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
            if (dsStock.Tables[0].Rows.Count > 0)
            {
                iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            }
            int iInsQty = iAQty + iQty;
            iSuccess = objBs.updateSalesStock(iInsQty, iCat, iSubCat, "tblStock_" + sTableName);

            return iSuccess;
        }

        protected void gridview_Sorting(object sender, GridViewSortEventArgs e)
        {
            string[] SortOrder = ViewState["SortExpr"].ToString().Split(' ');
            if (SortOrder[0] == e.SortExpression)
            {
                if (SortOrder[1] == "ASC")
                {
                    ViewState["SortExpr"] = e.SortExpression + " " + "DESC";
                }
                else
                {
                    ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
                }
            }
            else
            {
                ViewState["SortExpr"] = e.SortExpression + " " + "ASC";
            }
            DataSet ds = objBs.CustomerSalesGird(Convert.ToInt32(Session["UserID"].ToString()), "tblSales_" + sTableName, "tblDayBook_" + sTableName, sTableName, drpbilltypes.SelectedValue);
            DataView dvEmp = ds.Tables[0].DefaultView;
            dvEmp.Sort = ViewState["SortExpr"].ToString();
            Session["SortedView"] = dvEmp;
            gvsales.DataSource = dvEmp;
            gvsales.DataBind();

        }

        private string PopulateBody(string Text)
        {
            string body = string.Empty;
            string Company = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Accountsbootstrap/HTMLPage1.htm")))
            {
                body = reader.ReadToEnd();
            }


            //body = body.Replace("{lblCompany}", ddlCompanyName.SelectedItem.Text);
            body = body.Replace("{Text}", Text);

            return body;
        }


        protected void SendEmail(string Emailid, string nngemail, string Text, string Subject, string body1)
        {
            string body = this.PopulateBody(Text);
            this.SendHtmlFormattedEmail(Emailid, Subject, body);
            // this.SendHtmlFormattedEmail(nngemail, Subject, body1);
        }


        private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
        {
            using (MailMessage mailMessage = new MailMessage())
            {
                string recepients = recepientEmail;

                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["FromAddress"]);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(recepients));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["SmtpHostName"];
                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["FromAddress"];
                NetworkCred.Password = ConfigurationManager.AppSettings["FromPassword"];
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPortNumber"]);
                smtp.Send(mailMessage);

            }
        }
    }
}