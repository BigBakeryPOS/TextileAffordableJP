using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
namespace Billing.Accountsbootstrap
{
    public partial class PurchaseEntryGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string Sort_Direction = "VendorName1 ASC";
        string Sort_Direction1 = "PaymentMode ASC";
        string sTableName = "";
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

                DataSet dVendorName = objBs.VendorName("tblPurchase_" + sTableName);
                ddlVendor.DataSource = dVendorName.Tables[0];
                ddlVendor.DataTextField = "CustomerName";
                ddlVendor.DataValueField = "VendorID";
                ddlVendor.DataBind();
                ddlVendor.Items.Insert(0, "Select Vendor");

                DataSet dPoEntryGrid = objBs.PurchaseGrid("tblPurchase_" + sTableName,sTableName);
                gvPurchaseEntry.DataSource = dPoEntryGrid;
                gvPurchaseEntry.DataBind();
            }
        }

       


        protected void gvPurchaseEntry_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string[] arg = new string[1];
            arg = e.CommandArgument.ToString().Split(',');

            if (e.CommandName == "edit")
            {
                Response.Redirect("Purchase.aspx?DC_NO=" + arg[0]);
            }

            else if (e.CommandName == "print")
            {
                Response.Redirect("Print_Purchase.aspx?DC_NO=" + arg[0]);
            }
            else if (e.CommandName == "delete")
            {
                DataSet dsDaybookId = objBs.selectDaybookid("tblPurchase_" + sTableName, arg[0]);

                         string daybookid = dsDaybookId.Tables[0].Rows[0]["DayBookTransNo"].ToString();

                         //int iDelete = objBs.deletepurchaseentry("tblDayBook_" + sTableName, "tblPurchase_" + sTableName, arg[0], daybookid, e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text, e.CommandName);
                         DataSet dsTransSales = objBs.GetTransPurchase("tblTransPurchase_" + sTableName, arg[0].ToString());
                            if (dsTransSales.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsTransSales.Tables[0].Rows.Count; i++)
                                {
                                    string sddlCat = dsTransSales.Tables[0].Rows[i]["CategoryId"].ToString();
                                    string sddlDef = dsTransSales.Tables[0].Rows[i]["DescriptionId"].ToString();
                                    string sQty = dsTransSales.Tables[0].Rows[i]["Qty"].ToString();
                                    int iSuccs = UpdateEditStock(Convert.ToInt32(sddlCat), Convert.ToInt32(sddlDef), Convert.ToDouble(sQty));

                                }
                            }
                            int iTransDelete = objBs.DeleteTransPurchase("tblTransPurchase_" + sTableName, arg[0], Convert.ToInt32(arg[1]), "tblTransPO_" + sTableName, "tblPurchase_" + sTableName);
                            int iDelete = objBs.deletepurchaseentry("tblDayBook_" + sTableName, "tblPurchase_" + sTableName, arg[0], daybookid, e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text, e.CommandName,sTableName);
                            Response.Redirect("PurchaseEntryGrid.aspx");

                        }
            
            
        }

        //protected void btnsearch_Click(object sender, EventArgs e)
        //{
        //    DataSet dVendorName = objBs.VendorNameSearch(Convert.ToInt32(ddlVendor.SelectedValue));
        //    gvPurchaseEntry.DataSource = dVendorName;
        //    gvPurchaseEntry.DataBind();
        //}

       

        protected void gvPurchaseEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet dPoEntryGrid = objBs.PurchaseGrid("tblPurchase_" + sTableName, sTableName);
            if (Session["SortedView"] != null)
            {
                gvPurchaseEntry.DataSource = Session["SortedView"];
                gvPurchaseEntry.DataBind();
            }
            else
            {
                gvPurchaseEntry.DataSource = dPoEntryGrid;
                gvPurchaseEntry.PageIndex = e.NewPageIndex;
                // gvsales.DataBind();
            }

            gvPurchaseEntry.PageIndex = e.NewPageIndex;
            gvPurchaseEntry.DataSource = dPoEntryGrid;
            gvPurchaseEntry.DataBind();


        }

        protected void btnresret_Click(object sender, EventArgs e)
        {

            Response.Redirect("PurchaseEntryGrid.aspx");
        }

       

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Purchase.aspx");
        }
   
     private int UpdateEditStock(int iCat, int iSubCat, double iQty)
        {
            double iAQty = 0; 
         int iSuccess = 0;

            DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
            if (dsStock.Tables[0].Rows.Count > 0)
            {
                iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            }
            double iInsQty = iAQty - iQty;
            iSuccess = objBs.updateSalesStock(iInsQty, iCat, iSubCat, "tblStock_" + sTableName);

            return iSuccess;
        }

     protected void btnsearch_Click2(object sender, EventArgs e)
     {
         DataSet ds = objBs.searchPurchase("tblPurchase_" + sTableName, Convert.ToInt32(ddlbillno.SelectedValue),txtsearch.Text,sTableName);

         if (ds != null)
         {
             if (ds.Tables[0].Rows.Count > 0)
             {
                 gvPurchaseEntry.DataSource = ds;
                 gvPurchaseEntry.DataBind();
             }
             else
             {
                 gvPurchaseEntry.DataSource = null;
                 gvPurchaseEntry.DataBind();
             }
         }
         else
         {
             gvPurchaseEntry.DataSource = null;
             gvPurchaseEntry.DataBind();
         }
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
         DataSet dPoEntryGrid = objBs.PurchaseGrid("tblPurchase_" + sTableName, sTableName);
         DataView dvEmp = dPoEntryGrid.Tables[0].DefaultView;
         dvEmp.Sort = ViewState["SortExpr"].ToString();
         Session["SortedView"] = dvEmp;
         gvPurchaseEntry.DataSource = dvEmp;
         gvPurchaseEntry.DataBind();

     }
}
}