using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLayer;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Text;

namespace Billing.Accountsbootstrap
{
    public partial class OpeningStockMaster : System.Web.UI.Page
    {
        OpeningStockEntry objBs = new OpeningStockEntry();
        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objBs.getGrid_New(sTableName);
                gridOpening.DataSource = ds;
                gridOpening.DataBind();
            }

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("OpeningStock.aspx");
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            Response.Redirect("OpeningStockMaster.aspx");
        }

        protected void gridOpening_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                if (e.CommandArgument.ToString() != "")
                {

                    Response.Redirect("OpeningStock.aspx?OpenStockID=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "Del")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DataSet ds = objBs.getbyquery(e.CommandArgument.ToString());
                    string sItemID = ds.Tables[0].Rows[0]["StockItem"].ToString();
                    string sQty = ds.Tables[0].Rows[0]["Nos"].ToString();
                    DataSet ds1 = objBs.getbyquery2(sItemID, "tblStock_" + sTableName);
                    string sItemID1 = ds1.Tables[0].Rows[0]["SubCategoryID"].ToString();
                    string sQty2 = ds1.Tables[0].Rows[0]["Available_QTY"].ToString();
                    int j = Convert.ToInt32(sQty2) - Convert.ToInt32(sQty);
                    int iupdate = objBs.updatestock2(j, sItemID1, "tblStock_" + sTableName);

                    objBs.DeleteOpeningStock(e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text,sTableName);
                    Response.Redirect("OpeningStockMaster.aspx");
                }
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
           
        if (ddlfilter.SelectedValue == "1")
            {

                if (txtsearch.Text == "")
                {
                    lblerror.Text = "Please Enter The Details";
                    
                }
                else
                {
                    DataSet ds = objBs.getByCateGoryGrid_New(txtsearch.Text,sTableName);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        gridOpening.DataSource = ds;
                        gridOpening.DataBind();
                    
                    }
                    else
                    {
                      
                        gridOpening.DataSource = null;
                        gridOpening.DataBind();

                    }
                }

                
            }
            else if (ddlfilter.SelectedValue == "2")
            {
                if (txtsearch.Text == "")
                {
                    lblerror.Text = "Please Enter The Details";
                   
                }
                else
                {
                    DataSet ds = objBs.getByItemGrid_New(txtsearch.Text,sTableName);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gridOpening.DataSource = ds;
                        gridOpening.DataBind();
                     
                    }
                    else
                    {
                   
                        gridOpening.DataSource = null;
                        gridOpening.DataBind();
                    }
                }

            }

        else if (ddlfilter.SelectedValue == "3")
        {
            if (txtsearch.Text == "")
            {
                lblerror.Text = "Please Enter The Details";

            }
            else
            {
                DataSet ds = objBs.getByItemGridproduct(txtsearch.Text, sTableName);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridOpening.DataSource = ds;
                    gridOpening.DataBind();

                }
                else
                {

                    gridOpening.DataSource = null;
                    gridOpening.DataBind();
                }
            }

        }

            else
            {
                //lblerror1.Text = "Please Select Valid Search Field";
                //lblerror.Visible = false;
            }
        }
        protected void openGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();

           
           if (ddlfilter.SelectedValue == "1")
            {
                ds = objBs.getByCateGoryGrid_New(txtsearch.Text,sTableName);
            }
            else if (ddlfilter.SelectedValue == "2")
            {
                ds = objBs.getByItemGrid_New(txtsearch.Text,sTableName);
            }

            else
            {
                ds = objBs.getGrid_New(sTableName);
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    gridOpening.DataSource = ds;
                    gridOpening.PageIndex = e.NewPageIndex;
                    gridOpening.DataBind();
                }
                else
                {
                    gridOpening.DataSource = null;
                    gridOpening.DataBind();
                }
            }
            else
            {
                gridOpening.DataSource = null;
                gridOpening.DataBind();
            }
        }
    }
}