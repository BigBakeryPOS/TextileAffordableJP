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
using System.Globalization;


namespace Billing.Accountsbootstrap
{
    public partial class IroningGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double totqty = 0;
        double RecQty = 0;
        double RemQty = 0;
        int TotalIssue = 0; int TotalReceive = 0; int TotalDamage = 0; int AlterQty = 0; double TotalAmount = 0; double PaidAmount = 0; double DebitAmount = 0; double Miscellaneous = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();


            if (!IsPostBack)
            {

                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                DataSet dst = objBs.Getjobworkmastrriro();
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddljobworker.DataSource = dst.Tables[0];
                        ddljobworker.DataTextField = "LedgerName";
                        ddljobworker.DataValueField = "LedgerID";
                        ddljobworker.DataBind();
                        ddljobworker.Items.Insert(0, "ALL");
                    }
                }

                DataSet drpEmpp = objBs.SelectEmpName();
                drpMultiemployee.DataSource = drpEmpp;
                drpMultiemployee.DataTextField = "Name";
                drpMultiemployee.DataValueField = "Employee_Id";
                drpMultiemployee.DataBind();
                drpMultiemployee.Items.Insert(0, "All");


                string super = Session["IsSuperAdmin"].ToString();


                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "All");
                    }
                }
                else
                {

                    drpbranch.Enabled = false;
                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();

                    }
                }


                //Employee_changed(sender, e);
                // DataSet dunit = objBs.JpEmbroidingformulti();
                DataSet pending = objBs.pendinglotdetail123("tblJpIroning", "tbltransJpIroning", drpbranch.SelectedValue, drppending.SelectedValue, "IroningId");
                if (pending.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = pending;
                    gvcust.DataBind();
                }
                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
            //Barcode.Focus();
        }

        protected void Employee_changed(object sender, EventArgs e)
        {
            string value = "All";
            if (drpMultiemployee.SelectedItem.Text == "All")
            {
                value = "All";
            }
            else
            {
                value = drpMultiemployee.SelectedValue;
            }
            DataSet dunit = objBs.employeependingformulti(value);
            if (dunit.Tables[0].Rows.Count > 0)
            {
                gvcust.DataSource = dunit;
                gvcust.DataBind();
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }

        protected void company_SelectedIndexChnaged(object sender, EventArgs e)
        {

            Date_OnTextChanged(sender, e);
            //////if (drpbranch.SelectedValue == "Select Branch")
            //////{
            //////    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
            //////    return;

            //////}
            //////else
            //////{
            //////    DataSet pending = objBs.pendinglotdetail123("tblJpIroning", "tbltransJpIroning", drpbranch.SelectedValue, drppending.SelectedValue, "IroningId");
            //////    if (pending.Tables[0].Rows.Count > 0)
            //////    {
            //////        gvcust.DataSource = pending;
            //////        gvcust.DataBind();
            //////    }
            //////    else
            //////    {
            //////        gvcust.DataSource = null;
            //////        gvcust.DataBind();
            //////    }
            //////}
        }

        protected void pending_changed(object sender, EventArgs e)
        {
            Date_OnTextChanged(sender, e);
            //////DataSet pending = objBs.pendinglotdetail123("tblJpIroning", "tbltransJpIroning", drpbranch.SelectedValue, drppending.SelectedValue, "IroningId");
            //////if (pending.Tables[0].Rows.Count > 0)
            //////{
            //////    gvcust.DataSource = pending;
            //////    gvcust.DataBind();
            //////}
            //////else
            //////{
            //////    gvcust.DataSource = null;
            //////    gvcust.DataBind();
            //////}
        }
        protected void Barcode_indexChanged(object sender, EventArgs e)
        {
            if (Barcode.Text == "")
            {
            }
            else
            {
                DataSet dss = objBs.getbarcodereader(Barcode.Text);
                if (dss.Tables[0].Rows.Count > 0)
                {
                    string muti = dss.Tables[0].Rows[0]["multiid"].ToString();

                    Response.Redirect("Ironing.aspx?name=Receive&lotid=" + muti);
                }

            }

        }

        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataSet ds = new DataSet();
            if (e.CommandName == "Editt")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                int RowIndex = gvr.RowIndex;



                //Access Cell values.
                int Companyid = int.Parse(gvcust.Rows[RowIndex].Cells[0].Text);


                Response.Redirect("Ironing.aspx?name=Edit&Ironingid=" + e.CommandArgument.ToString() + "&Companyid=" + Companyid);

            }
            else if (e.CommandName == "Received")
            {

                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                int RowIndex = gvr.RowIndex;

                //Determine the RowIndex of the Row whose Button was clicked.

                //int rowIndex = gvcust.SelectedRow.RowIndex;// Convert.ToInt32(e.CommandArgument);

                ////// int Companyid = DataBinder.Eval(e.Row.DataItem, "Companyid");

                //Access Cell values.DataBinder.Eval(e.Row.DataItem, "30fs")
                int Companyid = int.Parse(gvcust.Rows[RowIndex].Cells[1].Text);
                //  ds = objBs.checkprocessthereornot(e.CommandArgument.ToString());
                //if (ds.Tables[0].Rows.Count > 0)
                {
                    Response.Redirect("Ironing.aspx?name=Receive&Ironingid=" + e.CommandArgument.ToString() + "&Companyid=" + Companyid);
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check This Lot number Because this is used in another Process.Thank You!!!')", true);
                //    return;
                //}
            }
            else if (e.CommandName == "Payment")
            {

                //  ds = objBs.checkprocessthereornot(e.CommandArgument.ToString());
                //if (ds.Tables[0].Rows.Count > 0)
                {
                    Response.Redirect("PaymentScreen.aspx?name=Iron&id=" + e.CommandArgument.ToString());
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check This Lot number Because this is used in another Process.Thank You!!!')", true);
                //    return;
                //}
            }
            else if (e.CommandName == "printt")
            {
                string YourURL = "IroningG.aspx?Ironingid=" + e.CommandArgument.ToString() + "&Type=Issue";

                Response.Write("<script> window.open('" + YourURL + "','_blank'); </script>");

            }
            else if (e.CommandName == "printtRec")
            {
                string YourURL = "IroningG.aspx?IroningidRec=" + e.CommandArgument.ToString() + "&Type=Receive";

                Response.Write("<script> window.open('" + YourURL + "','_blank'); </script>");

            }
            else if (e.CommandName == "printt1")
            {
                Response.Redirect("Iron.aspx?Ironingid=" + e.CommandArgument.ToString());

            }
        }

        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvLiaLedger") as GridView;
                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);
                    DataSet ds = objBs.getdetailedlotgrid(groupID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;

                        gv.DataBind();
                    }
                }

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvLiaLedger1") as GridView;
                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);
                    DataSet ds = objBs.getdetailedlotgridfortotal(groupID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;

                        gv.DataBind();
                    }
                }

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvLiaLedger2") as GridView;
                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);
                    DataSet ds = objBs.getdetailedlotgridfortotalreceived(groupID);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;

                        gv.DataBind();
                    }
                }

            }


        }

        protected void gvRowdatabound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string cutid = e.Row.Cells[5].Text;

                GridView gv = e.Row.FindControl("gvfabfetails") as GridView;

                DataSet ds = objBs.getfabdetailsforcutting(cutid);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = ds;
                    gv.DataBind();
                }

                TotalIssue = TotalIssue + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalIssue"));
                TotalReceive = TotalReceive + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalReceive"));
                TotalDamage = TotalDamage + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalDamage"));
                AlterQty = AlterQty + Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AlterQty"));

                TotalAmount = TotalAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "TotalAmount"));
                PaidAmount = PaidAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "PaidAmount"));

                DebitAmount = DebitAmount + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DebitAmount"));
                Miscellaneous = Miscellaneous + Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Miscellaneous"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[8].Text = "Total";

                e.Row.Cells[9].Text = TotalIssue.ToString();
                e.Row.Cells[10].Text = TotalReceive.ToString();
                e.Row.Cells[11].Text = TotalDamage.ToString();
                e.Row.Cells[12].Text = AlterQty.ToString();

                e.Row.Cells[13].Text = TotalAmount.ToString("f2");
                e.Row.Cells[14].Text = PaidAmount.ToString("f2");

                e.Row.Cells[15].Text = DebitAmount.ToString("f2");
                e.Row.Cells[16].Text = Miscellaneous.ToString("f2");

            }
        }

        protected void Date_OnTextChanged(object sender, EventArgs e)
        {
            DateTime FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet pending = objBs.getallProcessdetails("tbljpIroning", "tbltransjpIroning", "Ironingid", drpbranch.SelectedValue, drppending.SelectedValue, ddljobworker.SelectedValue, FromDate, ToDate);
            if (pending.Tables[0].Rows.Count > 0)
            {
                gvcust.DataSource = pending;
                gvcust.DataBind();
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }

        protected void ddljobworker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Date_OnTextChanged(sender, e);
        }
    }
}
