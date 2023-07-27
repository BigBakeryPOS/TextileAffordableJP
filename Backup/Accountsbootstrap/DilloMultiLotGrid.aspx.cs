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


namespace Billing.Accountsbootstrap
{
    public partial class DilloMultiLotGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double totqty = 0;
        double RecQty = 0;
        double RemQty = 0;
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
                DataSet drpEmpp = objBs.SelectEmpName();
                drpMultiemployee.DataSource = drpEmpp;
                drpMultiemployee.DataTextField = "Name";
                drpMultiemployee.DataValueField = "Employee_Id";
                drpMultiemployee.DataBind();
                drpMultiemployee.Items.Insert(0, "All");


                Employee_changed(sender, e);
                //DataSet ds = objBs.SelectLOTInfoDetGrid();
                //if (ds != null)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        gvcust.DataSource = ds;
                //        gvcust.DataBind();
                //    }

                //    else
                //    {
                //        gvcust.DataSource = null;
                //        gvcust.DataBind();
                //    }
                //}
                //else
                //{
                //    gvcust.DataSource = null;
                //    gvcust.DataBind();
                //}
            }
            Barcode.Focus();
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

        protected void pending_changed(object sender, EventArgs e)
        {
            //DataSet pending = objBs.unitpending(drpunit.SelectedValue, drppending.SelectedValue);
            //if (pending.Tables[0].Rows.Count > 0)
            //{
            //    gvcust.DataSource = pending;
            //    gvcust.DataBind();
            //}
            //else
            //{
            //    gvcust.DataSource = null;
            //    gvcust.DataBind();
            //}
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

                    Response.Redirect("DilloMultipleLot.aspx?name=Receive&lotid=" + muti);
                }

            }

        }

        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataSet ds = new DataSet();
            if (e.CommandName == "Editt")
            {

               

                    Response.Redirect("DilloMultipleLot.aspx?name=Edit&lotid=" + e.CommandArgument.ToString());
            
            }
            else if (e.CommandName == "Received")
            {

              //  ds = objBs.checkprocessthereornot(e.CommandArgument.ToString());
                //if (ds.Tables[0].Rows.Count > 0)
                {
                    Response.Redirect("DilloMultipleLot.aspx?name=Receive&lotid=" + e.CommandArgument.ToString());
                }
                //else
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check This Lot number Because this is used in another Process.Thank You!!!')", true);
                //    return;
                //}
            }
            else if (e.CommandName == "printt")
            {
                Response.Redirect("DilloMultiLotPrint.aspx?lotid=" + e.CommandArgument.ToString());

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
                totqty = totqty + Convert.ToDouble(e.Row.Cells[2].Text.ToString());
                RecQty = RecQty + Convert.ToDouble(e.Row.Cells[3].Text.ToString());
                RemQty = RemQty + Convert.ToDouble(e.Row.Cells[4].Text.ToString());

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total";
                e.Row.Cells[2].Text = totqty.ToString();
                e.Row.Cells[3].Text = RecQty.ToString();
                e.Row.Cells[4].Text = RemQty.ToString();

            }
        }
    }
}