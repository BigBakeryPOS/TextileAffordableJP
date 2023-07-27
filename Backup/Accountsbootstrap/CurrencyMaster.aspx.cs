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
    public partial class CurrencyMaster : System.Web.UI.Page
    {

        BSClass objBs = new BSClass();

        string sTableName = "";
        string IsSuperAdmin = "";

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
                DataSet ds = objBs.gridCurrency();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridview.DataSource = ds;
                    gridview.DataBind();
                }
                else
                {
                    gridview.DataSource = null;
                    gridview.DataBind();
                }
            }
        }

        protected void gvCurrencyDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CurrencyHistory")
            {
                mpecost.Show();
                DataSet dsView = objBs.getCurrencyHistory(e.CommandArgument.ToString());
                if (dsView.Tables[0].Rows.Count > 0)
                {
                    gvCurrencyDetails.DataSource = dsView;
                    gvCurrencyDetails.DataBind();
                }
                else
                {
                    gvCurrencyDetails.DataSource = null;
                    gvCurrencyDetails.DataBind();
                }
            }
        }

        protected void gridview_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            lblName.Text = "Update Currency";

            if (gridview.SelectedDataKey.Value != null && gridview.SelectedDataKey.Value.ToString() != "")
            {
                string id = gridview.SelectedDataKey.Value.ToString();

                DataSet ds = objBs.getiCurrencyvalues(id);
                if (ds.Tables[0].Rows.Count > 0)

                txtCurrencyID.Text = ds.Tables[0].Rows[0]["CurrencyID"].ToString();

                txtCurrency.Text = ds.Tables[0].Rows[0]["CurrencyName"].ToString();
                txtValue.Text = ds.Tables[0].Rows[0]["Value"].ToString();
                ddlIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();

                Button1.Text = "Update";
            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            if (txtValue.Text == "")
                txtValue.Text = "0";

            if (Convert.ToDouble(txtValue.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check the Value.')", true);
                txtValue.Focus();
                return;
            }

            if (Button1.Text == "Save")
            {
                DataSet dsCurrency = objBs.Currencysrchgrid(txtCurrency.Text, 0);
                if (dsCurrency.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This CurrencyName was already Exists.')", true);
                    txtCurrency.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.InsertCurrency(txtCurrency.Text, Convert.ToDouble(txtValue.Text), ddlIsActive.SelectedValue, Button1.Text);
                    Response.Redirect("CurrencyMaster.aspx");
                }
            }
            else
            {
                DataSet dsCurrency = objBs.Currencysrchgrid(txtCurrency.Text, Convert.ToInt32(txtCurrencyID.Text));
                if (dsCurrency.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This CurrencyName was already Exists.')", true);
                    txtCurrency.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.updateCurrencyMaster(txtCurrency.Text, Convert.ToDouble(txtValue.Text), ddlIsActive.SelectedValue, Button1.Text, Convert.ToInt32(txtCurrencyID.Text));
                    Response.Redirect("CurrencyMaster.aspx");
                }

            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCurrencyID.Text = "";

            txtCurrency.Text = "";
            txtValue.Text = "";

            ddlIsActive.ClearSelection();

            Button1.Text = "Save";
            lblName.Text = "Add Currency";
        }
        protected void btnresret_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CurrencyMaster.aspx");
        }

    }
}
