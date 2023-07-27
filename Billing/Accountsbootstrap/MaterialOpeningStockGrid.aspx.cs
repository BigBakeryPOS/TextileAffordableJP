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
    public partial class MaterialOpeningStockGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                lblUser.Text = Session["UserName"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objBs.GetMaterialOpeningStockEntry();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvMaterialOpeningStockEntry.DataSource = ds;
                    gvMaterialOpeningStockEntry.DataBind();
                }

                else
                {
                    gvMaterialOpeningStockEntry.DataSource = null;
                    gvMaterialOpeningStockEntry.DataBind();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("MaterialOpeningStock.aspx");
        }

        protected void gvBuyerOrderCutting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edits")
            {
                DataSet dsCompany = objBs.GetCompanyDetails();
                if (dsCompany.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.DataSource = dsCompany.Tables[0];
                    ddlCompany.DataTextField = "CompanyName";
                    ddlCompany.DataValueField = "ComapanyID";
                    ddlCompany.DataBind();
                }
                DataSet dsProcessOn = objBs.GetCategory_as_Process("ShowPOrder");
                if (dsProcessOn.Tables[0].Rows.Count > 0)
                {
                    ddlProcessOn.DataSource = dsProcessOn.Tables[0];
                    ddlProcessOn.DataTextField = "category";
                    ddlProcessOn.DataValueField = "categoryid";
                    ddlProcessOn.DataBind();
                    ddlProcessOn.Items.Insert(0, "ProcessOn");
                }
                DataSet dsColor = objBs.gridColor();
                if (dsColor.Tables[0].Rows.Count > 0)
                {
                    ddlColor.DataSource = dsColor.Tables[0];
                    ddlColor.DataTextField = "Color";
                    ddlColor.DataValueField = "ColorID";
                    ddlColor.DataBind();
                    ddlColor.Items.Insert(0, "Select Color");
                }



                DataSet ds = objBs.GetMaterialOpeningStockEntry1(Convert.ToInt32(e.CommandArgument.ToString()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlCompany.SelectedValue = ds.Tables[0].Rows[0]["CompanyId"].ToString();
                    ddlCompany.Enabled = false;
                    lblcompany.Text = ds.Tables[0].Rows[0]["CompanyId"].ToString();
                    ddlProcessOn.SelectedValue = ds.Tables[0].Rows[0]["ProcessOnId"].ToString();
                    ddlProcessOn.Enabled = false;
                    ddlColor.SelectedValue = ds.Tables[0].Rows[0]["ColorId"].ToString();
                    ddlColor.Enabled = false;

                    DataSet dsItemforItemProcess = objBs.GetItemforItemProcess(Convert.ToInt32(ddlProcessOn.SelectedValue));
                    if (dsItemforItemProcess.Tables[0].Rows.Count > 0)
                    {
                        ddlItem.DataSource = dsItemforItemProcess.Tables[0];
                        ddlItem.DataTextField = "Description";
                        ddlItem.DataValueField = "ItemMasterId";
                        ddlItem.DataBind();
                        ddlItem.Items.Insert(0, "Select Item");
                    }

                    ddlItem.SelectedValue = ds.Tables[0].Rows[0]["ItemId"].ToString();
                    ddlItem.Enabled = false;

                    txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    txtQty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
                    txtRate.Text = ds.Tables[0].Rows[0]["ItemRate"].ToString();

                    txtAvlStock.Text = ds.Tables[0].Rows[0]["AvlQty"].ToString();

                    hdOPMaterialStockId.Value = ds.Tables[0].Rows[0]["OPMaterialStockId"].ToString();
                }
                else
                {
                    btnSave.Enabled = false;
                }

                mpecost.Show();

            }
            else if (e.CommandName == "deletes")
            {
                DataSet ds = objBs.GetMaterialOpeningStockEntry1(Convert.ToInt32(e.CommandArgument.ToString()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string Companyid = ds.Tables[0].Rows[0]["CompanyId"].ToString();
                    string ProcessOnId = ds.Tables[0].Rows[0]["ProcessOnId"].ToString();
                    string colorid = ds.Tables[0].Rows[0]["ColorId"].ToString();
                    string ItemId = ds.Tables[0].Rows[0]["ItemId"].ToString();
                    string Remarks = ds.Tables[0].Rows[0]["Remarks"].ToString();
                    string Qty = ds.Tables[0].Rows[0]["Qty"].ToString();
                    string OPMaterialStockId = ds.Tables[0].Rows[0]["OPMaterialStockId"].ToString();

                    int Delete = objBs.DeleteMaterialOpeningStockEntry(Convert.ToInt32(OPMaterialStockId), Convert.ToInt32(Companyid), Convert.ToInt32(ProcessOnId), Convert.ToInt32(ItemId), Convert.ToInt32(colorid), Remarks, Convert.ToDouble(Qty));
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Deleted Successfully.Thank You!!!.')", true);
                    DataSet dss = objBs.GetMaterialOpeningStockEntry();
                    if (dss.Tables[0].Rows.Count > 0)
                    {
                        gvMaterialOpeningStockEntry.DataSource = dss;
                        gvMaterialOpeningStockEntry.DataBind();
                    }

                    else
                    {
                        gvMaterialOpeningStockEntry.DataSource = null;
                        gvMaterialOpeningStockEntry.DataBind();
                    }
                    //Response.Redirect("MaterialOpeningStockGrid.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Something Went Wrong.Please Contact Admin.Thank You!!!.')", true);
                    return;
                }
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
                txtQty.Text = "0";
            if (txtRate.Text == "")
                txtRate.Text = "0";

            if (Convert.ToDouble(txtQty.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Fill Data.')", true);
                txtQty.Focus();
                mpecost.Show();
                return;
            }
            DataSet ds = objBs.GetMaterialOpeningStockEntry2(Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlItem.SelectedValue), Convert.ToInt32(ddlColor.SelectedValue), Convert.ToDouble(txtQty.Text), Convert.ToInt32(hdOPMaterialStockId.Value));
            if (ds.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Stock was Low Cannot be Update.')", true);
                txtQty.Focus();
                mpecost.Show();
                return;
            }

            //if (lblcompany.Text == ddlCompany.SelectedValue)
            {

                int Update = objBs.EditMaterialOpeningStockEntry(Convert.ToInt32(hdOPMaterialStockId.Value), Convert.ToInt32(ddlCompany.SelectedValue), Convert.ToInt32(ddlProcessOn.SelectedValue), Convert.ToInt32(ddlItem.SelectedValue), Convert.ToInt32(ddlColor.SelectedValue), txtRemarks.Text, Convert.ToDouble(txtQty.Text),Convert.ToDouble(txtRate.Text));
            }
            
            Response.Redirect("MaterialOpeningStockGrid.aspx");
        }
        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("MaterialOpeningStockGrid.aspx");
        }

    }
}


