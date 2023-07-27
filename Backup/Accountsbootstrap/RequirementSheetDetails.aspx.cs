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
    public partial class RequirementSheetDetails : System.Web.UI.Page
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

                    ddlBuyerName.DataSource = dsset.Tables[0];
                    ddlBuyerName.DataTextField = "LedgerName";
                    ddlBuyerName.DataValueField = "LedgerID";
                    ddlBuyerName.DataBind();
                    ddlBuyerName.Items.Insert(0, "All");
                }

                DataSet dsExcNo = objBs.RequirementSheetExcNo(ddlBuyerCode.SelectedValue);
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    chkExcNo.DataSource = dsExcNo.Tables[0];
                    chkExcNo.DataTextField = "ExcNo";
                    chkExcNo.DataValueField = "BuyerOrderId";                   
                    chkExcNo.DataBind();
                }
                DataSet dsItemHead = objBs.selectcategorymaster();
                if (dsItemHead.Tables[0].Rows.Count > 0)
                {
                    chkItemHead.DataSource = dsItemHead.Tables[0];                   
                    chkItemHead.DataTextField = "category";
                    chkItemHead.DataValueField = "categoryId"; 
                    chkItemHead.DataBind();
                }
            }
        }

        protected void buyer_code(object sender, EventArgs e)
        {
            DataSet dsExcNo = objBs.RequirementSheetExcNo(ddlBuyerCode.SelectedValue);
            if (dsExcNo.Tables[0].Rows.Count > 0)
            {
                chkExcNo.DataSource = dsExcNo.Tables[0];
                chkExcNo.DataTextField = "ExcNo";
                chkExcNo.DataValueField = "BuyerOrderId";
                chkExcNo.DataBind();
            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            #region

            string First = "Yes";
            string ItemHeadId = "";
            foreach (ListItem listItem in chkItemHead.Items)
            {
                #region

                if (listItem.Selected)
                {
                    if (First == "Yes")
                    {
                        ItemHeadId = listItem.Value;
                        First = "No";
                    }
                    else
                    {
                        ItemHeadId = ItemHeadId + "," + listItem.Value;
                    }
                }

                #endregion
            }

            string IsFirst = "Yes";
            string BuyerOrderId = "";
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

            DateTime From = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime To = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dsRS = objBs.RequirementSheetDetails1(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue);
            if (dsRS.Tables[0].Rows.Count > 0)
            {
                DataSet dsRSStyle = objBs.RequirementSheetDetails2(BuyerOrderId, chkUseDate.Checked, From, To, ddlBuyerCode.SelectedValue, ItemHeadId,ddlType.SelectedValue);

                #region

                DataTable DT = new DataTable();

                DT.Columns.Add(new DataColumn("Column1"));
                DT.Columns.Add(new DataColumn("Column2"));
                DT.Columns.Add(new DataColumn("Column3"));
                DT.Columns.Add(new DataColumn("Column4"));
                DT.Columns.Add(new DataColumn("Column5"));

                foreach (DataRow DR in dsRS.Tables[0].Rows)
                {
                    DataRow DR1 = DT.NewRow();
                    DR1["Column1"] = "Order Code :";
                    DR1["Column2"] = DR["ExcNo"].ToString();
                    DR1["Column3"] = "Shipment Date :";
                    DR1["Column4"] = Convert.ToDateTime(DR["ShipmentDate"]).ToString("dd/MM/yyyy");
                    DT.Rows.Add(DR1);

                    DataRow DR2 = DT.NewRow();
                    DR2["Column1"] = "Buyer PoNo :";
                    DR2["Column2"] = DR["BuyerPoNo"].ToString();
                    DR2["Column3"] = "Issue Date :";
                    DR2["Column4"] = Convert.ToDateTime(DR["OrderDate"]).ToString("dd/MM/yyyy");
                    DT.Rows.Add(DR2);

                    DataRow DR3 = DT.NewRow();
                    DR3["Column1"] = "Main Fabric :";
                    DR3["Column2"] = DR["description"].ToString();
                    DR3["Column3"] = "";
                    DR3["Column4"] = "";
                    DT.Rows.Add(DR3);

                    DataRow DR4 = DT.NewRow();
                    DR4["Column1"] = "";
                    DT.Rows.Add(DR4);

                    DataRow[] Rows = dsRSStyle.Tables[0].Select("RequirementId='" + DR["RequirementId"] + "'");
                    for (int i = 0; i < Rows.Length; i++)
                    {
                        DataRow DR5 = DT.NewRow();
                        DR5["Column1"] = Rows[i]["Itemgroupname"].ToString();
                        DR5["Column2"] = Rows[i]["Description"].ToString();
                        DR5["Column3"] = Rows[i]["Color"].ToString();
                        DR5["Column4"] = Rows[i]["ProductionQty"].ToString();
                        DR5["Column5"] = Rows[i]["Units"].ToString();
                        DT.Rows.Add(DR5);
                    }

                    DataRow DR6 = DT.NewRow();
                    DR6["Column1"] = "";
                    DT.Rows.Add(DR6);

                    DataRow DR7 = DT.NewRow();
                    DR7["Column1"] = "";
                    DT.Rows.Add(DR7);

                }

                #endregion

                gvRequirementSheetDetails.Caption = "RequirementSheet Details";
                gvRequirementSheetDetails.DataSource = DT;
                gvRequirementSheetDetails.DataBind();
            }
            else
            {
                gvRequirementSheetDetails.DataSource = null;
                gvRequirementSheetDetails.DataBind();
            }

            #endregion
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= RequirementSheetDetails.xls");
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


