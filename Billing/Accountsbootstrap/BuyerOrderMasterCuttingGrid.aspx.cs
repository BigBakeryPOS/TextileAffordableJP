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
    public partial class BuyerOrderMasterCuttingGrid : System.Web.UI.Page
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
                DataSet ds = objBs.GetBuyerOrderCuttingData_Master();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    #region

                    DataSet dsBOCQty = objBs.GetBuyerOrderCuttingQty();

                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("BuyerOrderCuttingId"));
                    dt.Columns.Add(new DataColumn("BuyerOrderMasterCuttingId"));
                    dt.Columns.Add(new DataColumn("ExcNo"));
                    dt.Columns.Add(new DataColumn("MasterCuttingDate"));
                    dt.Columns.Add(new DataColumn("CuttingDate"));
                    dt.Columns.Add(new DataColumn("OrderType"));
                    dt.Columns.Add(new DataColumn("CompanyCode"));
                    dt.Columns.Add(new DataColumn("DeliveryDate"));
                    dt.Columns.Add(new DataColumn("CQty"));
                    dt.Columns.Add(new DataColumn("RecQty"));
                    dt.Columns.Add(new DataColumn("DmgQty"));
                    dt.Columns.Add(new DataColumn("BalQty"));

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow[] RowsBOQty = dsBOCQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' ");
                        if (ddlfilter.SelectedValue == "2")
                        {
                            if ((Convert.ToInt32(RowsBOQty[0]["CQty"]) - (Convert.ToInt32(dr["RecQty"]) + Convert.ToInt32(dr["DmgQty"]))) > 0)
                            {
                                DataRow DR = dt.NewRow();
                                DR["BuyerOrderCuttingId"] = dr["BuyerOrderCuttingId"];
                                DR["BuyerOrderMasterCuttingId"] = dr["BuyerOrderMasterCuttingId"];
                                DR["ExcNo"] = dr["ExcNo"];
                                DR["MasterCuttingDate"] = Convert.ToDateTime(dr["MasterCuttingDate"]).ToString("dd/MM/yyyy");
                                DR["CuttingDate"] = Convert.ToDateTime(dr["CuttingDate"]).ToString("dd/MM/yyyy");
                                DR["OrderType"] = dr["OrderType"];
                                DR["CompanyCode"] = dr["CompanyCode"];
                                DR["DeliveryDate"] = Convert.ToDateTime(dr["DeliveryDate"]).ToString("dd/MM/yyyy");
                                DR["CQty"] = RowsBOQty[0]["CQty"];
                                DR["RecQty"] = dr["RecQty"];
                                DR["DmgQty"] = dr["DmgQty"];

                                DR["BalQty"] = (Convert.ToInt32(RowsBOQty[0]["CQty"]) - (Convert.ToInt32(dr["RecQty"]) + Convert.ToInt32(dr["DmgQty"])));
                                dt.Rows.Add(DR);
                            }
                        }
                    }

                    #endregion

                    gvBuyerOrderMasterCutting.DataSource = dt;
                    gvBuyerOrderMasterCutting.DataBind();
                }
                else
                {
                    gvBuyerOrderMasterCutting.DataSource = null;
                    gvBuyerOrderMasterCutting.DataBind();
                }
            }
        }

        protected void ddlfilter_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objBs.GetBuyerOrderCuttingData_Master();
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region

                DataSet dsBOCQty = objBs.GetBuyerOrderCuttingQty();

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("BuyerOrderCuttingId"));
                dt.Columns.Add(new DataColumn("BuyerOrderMasterCuttingId"));
                dt.Columns.Add(new DataColumn("ExcNo"));
                dt.Columns.Add(new DataColumn("MasterCuttingDate"));
                dt.Columns.Add(new DataColumn("CuttingDate"));
                dt.Columns.Add(new DataColumn("OrderType"));
                dt.Columns.Add(new DataColumn("CompanyCode"));
                dt.Columns.Add(new DataColumn("DeliveryDate"));
                dt.Columns.Add(new DataColumn("CQty"));
                dt.Columns.Add(new DataColumn("RecQty"));
                dt.Columns.Add(new DataColumn("DmgQty"));
                dt.Columns.Add(new DataColumn("BalQty"));

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataRow[] RowsBOQty = dsBOCQty.Tables[0].Select("BuyerOrderId='" + dr["BuyerOrderId"] + "' ");

                    if (ddlfilter.SelectedValue == "1")
                    {
                        DataRow DR = dt.NewRow();
                        DR["BuyerOrderCuttingId"] = dr["BuyerOrderCuttingId"];
                        DR["BuyerOrderMasterCuttingId"] = dr["BuyerOrderMasterCuttingId"];
                        DR["ExcNo"] = dr["ExcNo"];
                        DR["MasterCuttingDate"] = Convert.ToDateTime(dr["MasterCuttingDate"]).ToString("dd/MM/yyyy");
                        DR["CuttingDate"] = Convert.ToDateTime(dr["CuttingDate"]).ToString("dd/MM/yyyy");
                        DR["OrderType"] = dr["OrderType"];
                        DR["CompanyCode"] = dr["CompanyCode"];
                        DR["DeliveryDate"] = Convert.ToDateTime(dr["DeliveryDate"]).ToString("dd/MM/yyyy");
                        DR["CQty"] = RowsBOQty[0]["CQty"];
                        DR["RecQty"] = dr["RecQty"];
                        DR["DmgQty"] = dr["DmgQty"];

                        DR["BalQty"] = (Convert.ToInt32(RowsBOQty[0]["CQty"]) - (Convert.ToInt32(dr["RecQty"]) + Convert.ToInt32(dr["DmgQty"])));
                        dt.Rows.Add(DR);

                    }
                    else if (ddlfilter.SelectedValue == "2")
                    {
                        if ((Convert.ToInt32(RowsBOQty[0]["CQty"]) - (Convert.ToInt32(dr["RecQty"]) + Convert.ToInt32(dr["DmgQty"]))) > 0)
                        {
                            DataRow DR = dt.NewRow();
                            DR["BuyerOrderCuttingId"] = dr["BuyerOrderCuttingId"];
                            DR["BuyerOrderMasterCuttingId"] = dr["BuyerOrderMasterCuttingId"];
                            DR["ExcNo"] = dr["ExcNo"];
                            DR["MasterCuttingDate"] = Convert.ToDateTime(dr["MasterCuttingDate"]).ToString("dd/MM/yyyy");
                            DR["CuttingDate"] = Convert.ToDateTime(dr["CuttingDate"]).ToString("dd/MM/yyyy");
                            DR["OrderType"] = dr["OrderType"];
                            DR["CompanyCode"] = dr["CompanyCode"];
                            DR["DeliveryDate"] = Convert.ToDateTime(dr["DeliveryDate"]).ToString("dd/MM/yyyy");
                            DR["CQty"] = RowsBOQty[0]["CQty"];
                            DR["RecQty"] = dr["RecQty"];
                            DR["DmgQty"] = dr["DmgQty"];

                            DR["BalQty"] = (Convert.ToInt32(RowsBOQty[0]["CQty"]) - (Convert.ToInt32(dr["RecQty"]) + Convert.ToInt32(dr["DmgQty"])));
                            dt.Rows.Add(DR);
                        }
                    }

                    else if (ddlfilter.SelectedValue == "3")
                    {
                        if ((Convert.ToInt32(RowsBOQty[0]["CQty"]) - (Convert.ToInt32(dr["RecQty"]) + Convert.ToInt32(dr["DmgQty"]))) <= 0)
                        {
                            DataRow DR = dt.NewRow();
                            DR["BuyerOrderCuttingId"] = dr["BuyerOrderCuttingId"];
                            DR["BuyerOrderMasterCuttingId"] = dr["BuyerOrderMasterCuttingId"];
                            DR["ExcNo"] = dr["ExcNo"];
                            DR["MasterCuttingDate"] = Convert.ToDateTime(dr["MasterCuttingDate"]).ToString("dd/MM/yyyy");
                            DR["CuttingDate"] = Convert.ToDateTime(dr["CuttingDate"]).ToString("dd/MM/yyyy");
                            DR["OrderType"] = dr["OrderType"];
                            DR["CompanyCode"] = dr["CompanyCode"];
                            DR["DeliveryDate"] = Convert.ToDateTime(dr["DeliveryDate"]).ToString("dd/MM/yyyy");
                            DR["CQty"] = RowsBOQty[0]["CQty"];
                            DR["RecQty"] = dr["RecQty"];
                            DR["DmgQty"] = dr["DmgQty"];

                            DR["BalQty"] = (Convert.ToInt32(RowsBOQty[0]["CQty"]) - (Convert.ToInt32(dr["RecQty"]) + Convert.ToInt32(dr["DmgQty"])));
                            dt.Rows.Add(DR);
                        }
                    }
                }

                #endregion

                gvBuyerOrderMasterCutting.DataSource = dt;
                gvBuyerOrderMasterCutting.DataBind();
            }

            else
            {
                gvBuyerOrderMasterCutting.DataSource = null;
                gvBuyerOrderMasterCutting.DataBind();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("BuyerOrderMasterCutting.aspx");
        }

        protected void gvBuyerOrderCutting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("BuyerOrderMasterCutting.aspx?TYP=VIEW&&BuyerOrderMasterCuttingId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "Receive")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("BuyerOrderMasterCutting.aspx?TYP=REC&&BuyerOrderCutId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "delete1")
            {
                if (IsSuperAdmin == "1")
                {
                    DataSet dsup = objBs.DeleteBOMasterCuttingCheck(Convert.ToInt32(e.CommandArgument.ToString()), Convert.ToInt32(lblProcessforMasterId.Text));
                    if (dsup.Tables[0].Rows.Count > 0)
                    {
                        int iSucess = objBs.DeleteBOMasterCutting(Convert.ToInt32(e.CommandArgument.ToString()));
                        Response.Redirect("BuyerOrderMasterCuttingGrid.aspx");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cannot be Delete,Please Check Process Entry.');", true);
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

    }
}


