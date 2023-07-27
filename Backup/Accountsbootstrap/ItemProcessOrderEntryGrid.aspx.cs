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
    public partial class ItemProcessOrderEntryGrid : System.Web.UI.Page
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
            IsSuperAdmin = Session["IsSuperAdmin"].ToString();

            if (!IsPostBack)
            {
                DataSet ds = objBs.gridItemProcessOrderEntry();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVItemProcessOrder.DataSource = ds;
                    GVItemProcessOrder.DataBind();
                }

                else
                {
                    GVItemProcessOrder.DataSource = null;
                    GVItemProcessOrder.DataBind();
                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("ItemProcessOrderEntry.aspx");
        }

        protected void GVItemProcessOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("ItemProcessOrderEntry.aspx?ItemEntryId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "Print")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("ItemProcessOrderEntryPrint.aspx?ItemEntryId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "CancelMeter")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    DataSet ds2 = objBs.getTransItemProcessOrderEntry(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        #region

                        DataSet dstd = new DataSet();
                        DataTable dtddd = new DataTable();
                        DataRow drNew;
                        DataColumn dct;
                        DataTable dttt = new DataTable();

                        dct = new DataColumn("TransId");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("PurchaseFor");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("PurchaseForType");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("IssueItem");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("ReceiveItem");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("IssColor");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("RecColor");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Process");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Qty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Shrink");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Rate");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("Amount");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("Remarks");
                        dttt.Columns.Add(dct);

                        dct = new DataColumn("ReceivedQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("CanceledQty");
                        dttt.Columns.Add(dct);
                        dct = new DataColumn("CancelQty");
                        dttt.Columns.Add(dct);

                        dstd.Tables.Add(dttt);

                        foreach (DataRow Dr in ds2.Tables[0].Rows)
                        {
                            drNew = dttt.NewRow();

                            drNew["TransId"] = Dr["TransId"];
                            drNew["PurchaseFor"] = Dr["PurchaseFor"];
                            drNew["PurchaseForType"] = Dr["PurchaseforType"];

                            drNew["IssueItem"] = Dr["IssueItem"];
                            drNew["ReceiveItem"] = Dr["ReceiveItem"];

                            drNew["IssColor"] = Dr["IssueColor"];
                            drNew["RecColor"] = Dr["ReceiveColor"];
                            drNew["Process"] = Dr["Process"];

                            drNew["Qty"] =Convert.ToDouble(Dr["Qty"]).ToString("f2");
                            drNew["Shrink"] = Dr["Shrink"];
                            drNew["Rate"] = Convert.ToDouble(Dr["Rate"]).ToString("f2");
                            drNew["Amount"] = Convert.ToDouble(Dr["Amount"]).ToString("f2");

                            drNew["Remarks"] = Dr["Remarks"];

                            drNew["ReceivedQty"] = Convert.ToDouble(Dr["RecQty"]).ToString("f2");
                            drNew["CanceledQty"] = Convert.ToDouble(Dr["CancelQty"]).ToString("f2");
                            drNew["CancelQty"] = (Convert.ToDouble(Dr["Qty"]) - (Convert.ToDouble(Dr["RecQty"]) + Convert.ToDouble(Dr["CancelQty"]))).ToString("f2");

                            dstd.Tables[0].Rows.Add(drNew);
                            dtddd = dstd.Tables[0];
                        }

                        #endregion

                        ViewState["CurrentTable1"] = dtddd;
                        GVItem.DataSource = dtddd;
                        GVItem.DataBind();


                    }

                    mpecost.Show();
                }


            }
            else if (e.CommandName == "delete1")
            {
                if (IsSuperAdmin == "1")
                {
                    DataSet dsup = objBs.DeleteIPOEntryCheck(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (dsup.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot be Delete,Please Check ItemProcess Order Challan.');", true);
                        return;
                    }
                    else
                    {
                        int iSucess = objBs.DeleteIPOEntry(Convert.ToInt32(e.CommandArgument.ToString()));
                        Response.Redirect("ItemProcessOrderEntryGrid.aspx");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Admin.');", true);
                    return;
                }
            }
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ItemProcessOrderEntryGrid.aspx");
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdTransId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransId");
                HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                HiddenField hdReceivedQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdReceivedQty");
                HiddenField hdCanceledQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdCanceledQty");

                TextBox txtCancelQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtCancelQty");
                if (txtCancelQty.Text == "")
                    txtCancelQty.Text = "0";

                if (Convert.ToDouble(txtCancelQty.Text) > 0)
                {
                    if (Convert.ToDouble(hdQty.Value) < (Convert.ToDouble(hdReceivedQty.Value) + Convert.ToDouble(hdCanceledQty.Value) + Convert.ToDouble(txtCancelQty.Text)))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Cancel Qty in Row " + (vLoop + 1) + ".')", true);
                        txtCancelQty.Focus();
                        mpecost.Show();
                        return;
                      
                    }
                }

            }

            for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            {
                HiddenField hdTransId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransId");
                HiddenField hdQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdQty");
                HiddenField hdReceivedQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdReceivedQty");
                HiddenField hdCanceledQty = (HiddenField)GVItem.Rows[vLoop].FindControl("hdCanceledQty");

                TextBox txtCancelQty = (TextBox)GVItem.Rows[vLoop].FindControl("txtCancelQty");
                if (txtCancelQty.Text == "")
                    txtCancelQty.Text = "0";

                if (Convert.ToDouble(txtCancelQty.Text) > 0)
                {
                    int TransSamplingCostingId = objBs.InsertTransItemProcessEntryCancelQty(hdTransId.Value, Convert.ToDouble(txtCancelQty.Text));
                }
              
            }
            Response.Redirect("ItemProcessOrderEntryGrid.aspx");
        }

    }
}


