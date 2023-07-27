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
    public partial class TotalSamplingRequirement : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";

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

                DataSet dsset = objBs.getLedger(lblContactTypeId.Text);
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlBuyerCode.DataSource = dsset.Tables[0];
                    ddlBuyerCode.DataTextField = "CompanyCode";
                    ddlBuyerCode.DataValueField = "LedgerID";
                    ddlBuyerCode.DataBind();
                    ddlBuyerCode.Items.Insert(0, "All");

                }

                DataSet dsExcNo = objBs.getRequirementExcNo();
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    chkExcNo.DataSource = dsExcNo.Tables[0];
                    chkExcNo.DataTextField = "ExcNo";
                    chkExcNo.DataValueField = "BuyerOrderId";
                    chkExcNo.DataBind();
                }

            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string BuyerOrderId = "";
            string IsFirst = "Yes";

            foreach (ListItem listItem in chkExcNo.Items)
            {
                #region
                if (chkExcNo.SelectedIndex < 0)
                {
                    if (IsFirst == "Yes")
                    {
                        BuyerOrderId = listItem.Value;
                        IsFirst = "No";
                    }
                    else
                    {
                        BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                    }
                }
                else
                {
                    if (listItem.Selected)
                    {
                        if (IsFirst == "Yes")
                        {
                            BuyerOrderId = listItem.Value;
                            IsFirst = "No";
                        }
                        else
                        {
                            BuyerOrderId = BuyerOrderId + "," + listItem.Value;
                        }
                    }
                }

                #endregion
            }

            DataSet ds = objBs.getRequirement(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ExcNo"));
                dt.Columns.Add(new DataColumn("Date"));
                dt.Columns.Add(new DataColumn("ItemType"));
                dt.Columns.Add(new DataColumn("ItemName"));
                dt.Columns.Add(new DataColumn("ColorPrint"));
                dt.Columns.Add(new DataColumn("Qty"));
                dt.Columns.Add(new DataColumn("UoM"));

                string PrevoiusValue = "";
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    if (PrevoiusValue != "" & PrevoiusValue != dr["ExcNo"].ToString())
                    {
                        DataRow DRM1 = dt.NewRow();
                        DRM1["ExcNo"] = "";
                        DRM1["Date"] = "";
                        DRM1["ItemType"] = "";
                        dt.Rows.Add(DRM1);

                        DataRow DRM = dt.NewRow();
                        PrevoiusValue = dr["ExcNo"].ToString();

                        DRM["ExcNo"] = dr["ExcNo"];
                        DRM["Date"] = Convert.ToDateTime(dr["RequirementDate"]).ToString("dd/MM/yyyy");
                        DRM["ItemType"] = dr["ItemDescription"];
                        DRM["ItemName"] = dr["Description"];
                        DRM["ColorPrint"] = dr["Color"];
                        DRM["Qty"] = dr["ProductionQty"];
                        DRM["UoM"] = dr["Units"];

                        dt.Rows.Add(DRM);

                    }
                    else
                    {
                        DataRow DRM = dt.NewRow();
                        PrevoiusValue = dr["ExcNo"].ToString();

                        DRM["ExcNo"] = dr["ExcNo"];
                        DRM["Date"] = Convert.ToDateTime(dr["RequirementDate"]).ToString("dd/MM/yyyy");
                        DRM["ItemType"] = dr["ItemDescription"];
                        DRM["ItemName"] = dr["Description"];
                        DRM["ColorPrint"] = dr["Color"];
                        DRM["Qty"] = dr["ProductionQty"];
                        DRM["UoM"] = dr["Units"];

                        dt.Rows.Add(DRM);
                    }
                }

                //DataRow DRM1 = dt.NewRow();
                //DRM1["Color"] = "";
                //DRM1["OrderQty"] = "";
                //DRM1["Total"] = "";
                //dt.Rows.Add(DRM1);


                #endregion

                if (dt.Rows.Count > 0)
                {
                    gvRequirement.DataSource = dt;
                    gvRequirement.DataBind();
                }
                else
                {
                    gvRequirement.DataSource = null;
                    gvRequirement.DataBind();
                }
            }
            else
            {
                gvRequirement.DataSource = null;
                gvRequirement.DataBind();
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= TotalSamplingRequirement.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Excel.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

    }
}


