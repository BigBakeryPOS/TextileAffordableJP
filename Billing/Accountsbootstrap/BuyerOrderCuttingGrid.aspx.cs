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


namespace Billing.Accountsbootstrap
{
    public partial class BuyerOrderCuttingGrid : System.Web.UI.Page
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

                // Get Year
                DataSet dsyear = objBs.GetYear();
                if (dsyear.Tables[0].Rows.Count > 0)
                {
                    drpyear.DataSource = dsyear.Tables[0];
                    drpyear.DataTextField = "yearname";
                    drpyear.DataValueField = "yearname";
                    drpyear.DataBind();
                    //drpyear.Items.Insert(0, "CompanyName");
                }

                DataSet ds = objBs.GetBuyerOrderCuttingData("2",drpyear.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvBuyerOrderCutting.DataSource = ds;
                    gvBuyerOrderCutting.DataBind();
                }

                else
                {
                    gvBuyerOrderCutting.DataSource = null;
                    gvBuyerOrderCutting.DataBind();
                }
            }
        }

        protected void Year_selected(object sender, EventArgs e)
        {
            DataSet ds = objBs.GetBuyerOrderCuttingData(ddlfilter.SelectedValue, drpyear.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBuyerOrderCutting.DataSource = ds;
                gvBuyerOrderCutting.DataBind();
            }

            else
            {
                gvBuyerOrderCutting.DataSource = null;
                gvBuyerOrderCutting.DataBind();
            }
        }

        protected void ddlfilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objBs.GetBuyerOrderCuttingData(ddlfilter.SelectedValue,drpyear.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBuyerOrderCutting.DataSource = ds;
                gvBuyerOrderCutting.DataBind();
            }

            else
            {
                gvBuyerOrderCutting.DataSource = null;
                gvBuyerOrderCutting.DataBind();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {

            string button = string.Empty;
            button = btnadd.Text;
            {
                button = btnadd.Text;
                Response.Redirect("BuyerOrderCutting.aspx");
            }
            //  Response.Redirect("../Accountsbootstrap/customermaster.aspx");

        }

        protected void gvBuyerOrderCutting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("BuyerOrderCutting.aspx?BuyerOrderCuttingId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "AddMaterials")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DataSet ds = objBs.GetBuyerOrderCutting(Convert.ToInt32(e.CommandArgument.ToString()));

                    hdBuyerOrderCuttingId.Value = e.CommandArgument.ToString();
                    hdCompanyId.Value = ds.Tables[0].Rows[0]["CompanyId"].ToString();

                    DataSet dsFab = objBs.GetAdditionalFabforPreCut(Convert.ToInt32(Convert.ToInt32(e.CommandArgument.ToString())));
                    if (dsFab.Tables[0].Rows.Count > 0)
                    {
                        GVFabricDetails.DataSource = dsFab;
                        GVFabricDetails.DataBind();
                    }
                    else
                    {
                        GVFabricDetails.DataSource = null;
                        GVFabricDetails.DataBind();
                    }

                    #region GET AVALIABLE STOCK FabricDetails

                    if (GVFabricDetails.Rows.Count > 0)
                    {
                        for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
                        {
                            HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                            HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                            Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");

                            DataSet dsstock = objBs.GetAvlStock(hdItemId.Value, hdColorId.Value, ds.Tables[0].Rows[0]["CompanyId"].ToString());
                            if (dsstock.Tables[0].Rows.Count > 0)
                            {
                                lblAvlStock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                            }
                            else
                            {
                                lblAvlStock.Text = "0";
                            }
                        }
                    }

                    #endregion

                    mpecost.Show();
                }
            }
            else if (e.CommandName == "Print")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("BuyerOrderCuttingPrint.aspx?BuyerOrderCuttingId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "delete1")
            {
                if (IsSuperAdmin == "1")
                {
                    DataSet dsup = objBs.DeleteBOCuttingCheck(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (dsup.Tables[0].Rows.Count > 0)
                    {
                        int iSucess = objBs.DeleteBOCutting(Convert.ToInt32(e.CommandArgument.ToString()));
                        Response.Redirect("BuyerOrderCuttingGrid.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot be Delete,Please Check BuyerOrder Master Cutting.');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Admin.');", true);
                    return;
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }


        protected void btnSave_OnClick(object sender, EventArgs e)
        {

            for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
            {
                HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                HiddenField hdRequiredStock = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdRequiredStock");
                HiddenField hdIssueStock = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdIssueStock");

                Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");
                TextBox txtIssueQty = (TextBox)GVFabricDetails.Rows[vLoop].FindControl("txtIssueQty");

                if (txtIssueQty.Text == "")
                    txtIssueQty.Text = "0";

                if (Convert.ToDouble(txtIssueQty.Text) > 0)
                {
                    if (Convert.ToDouble(lblAvlStock.Text) < Convert.ToDouble(txtIssueQty.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Avl.Stock  in Row " + (Convert.ToInt32(vLoop) + 1) + ".')", true);
                        txtIssueQty.Focus();
                        mpecost.Show();
                        return;
                    }
                    //////else if (Convert.ToDouble(hdRequiredStock.Value) < (Convert.ToDouble(hdIssueStock.Value) + Convert.ToDouble(txtIssueQty.Text)))
                    //////{
                    //////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Issue Qty in Row " + (Convert.ToInt32(vLoop) + 1) + ".')", true);
                    //////    txtIssueQty.Focus();
                    //////    mpecost.Show();
                    //////    return;
                    //////}
                }

               
            }

            for (int vLoop = 0; vLoop < GVFabricDetails.Rows.Count; vLoop++)
            {
                HiddenField hdItemId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdItemId");
                HiddenField hdColorId = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdColorId");
                HiddenField hdRequiredStock = (HiddenField)GVFabricDetails.Rows[vLoop].FindControl("hdRequiredStock");

                Label lblAvlStock = (Label)GVFabricDetails.Rows[vLoop].FindControl("lblAvlStock");
                TextBox txtIssueQty = (TextBox)GVFabricDetails.Rows[vLoop].FindControl("txtIssueQty");

                if (txtIssueQty.Text == "")
                    txtIssueQty.Text = "0";

                int CuttingFabricId = objBs.InsertAdditionalFabforPreCut(Convert.ToInt32(hdBuyerOrderCuttingId.Value), Convert.ToInt32(hdItemId.Value), Convert.ToInt32(hdColorId.Value), Convert.ToDouble(lblAvlStock.Text), Convert.ToDouble(hdRequiredStock.Value), Convert.ToDouble(txtIssueQty.Text), Convert.ToInt32(hdCompanyId.Value),"","","","","","","");

            }
            Response.Redirect("BuyerOrderCuttingGrid.aspx");
        }
        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("BuyerOrderCuttingGrid.aspx");
        }


    }
}


