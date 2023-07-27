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
    public partial class SwatchSheet : System.Web.UI.Page
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
                DataSet dsExcNo = objBs.RequirementSheetExcNo("All");
                if (dsExcNo.Tables[0].Rows.Count > 0)
                {
                    ddlExcNo.DataSource = dsExcNo.Tables[0];
                    ddlExcNo.DataTextField = "ExcNo";
                    ddlExcNo.DataValueField = "BuyerOrderId";
                    ddlExcNo.DataBind();
                    ddlExcNo.Items.Insert(0, "Select ExcNo");
                }

            }
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "" && ddlExcNo.SelectedValue != "Select ExcNo")
            {
                DataSet ds = objBs.SwatchSheet1(Convert.ToInt32(ddlExcNo.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable DT = new DataTable();

                    DT.Columns.Add(new DataColumn("Column1"));
                    DT.Columns.Add(new DataColumn("Column2"));
                    DT.Columns.Add(new DataColumn("Column3"));
                    DT.Columns.Add(new DataColumn("Column4"));
                    DT.Columns.Add(new DataColumn("Column5"));

                    foreach (DataRow DR in ds.Tables[0].Rows)
                    {
                        #region

                        DataRow DR1 = DT.NewRow();
                        DR1["Column1"] = "Order Code :";
                        DR1["Column2"] = DR["ExcNo"].ToString();
                        DR1["Column4"] = "Order Date :";
                        DR1["Column5"] = Convert.ToDateTime(DR["OrderDate"]).ToString("dd/MM/yyyy");
                        DT.Rows.Add(DR1);

                        DataRow DR2 = DT.NewRow();
                        DR2["Column1"] = "Buyer PoNo :";
                        DR2["Column2"] = DR["BuyerPoNo"].ToString();
                        DR2["Column4"] = "Shipment Date :";
                        DR2["Column5"] = Convert.ToDateTime(DR["ShipmentDate"]).ToString("dd/MM/yyyy");
                        DT.Rows.Add(DR2);

                        DataRow DR3 = DT.NewRow();
                        DR3["Column1"] = "Main Fabric :";
                        DR3["Column2"] = DR["description"].ToString();
                        DR3["Column4"] = "";
                        DR3["Column5"] = "";
                        DT.Rows.Add(DR3);

                        DataRow DR4 = DT.NewRow();
                        DR4["Column1"] = "";
                        DT.Rows.Add(DR4);

                        #endregion
                    }

                    gvSwatchSheet1.DataSource = DT;
                    gvSwatchSheet1.DataBind();

                }

                DataSet dsSS = objBs.SwatchSheet2(Convert.ToInt32(ddlExcNo.SelectedValue), Convert.ToInt32(lblProcessforMasterId.Text));

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Description"));
                dt.Columns.Add(new DataColumn("Color"));

                foreach (DataRow dr in dsSS.Tables[0].Rows)
                {
                    DataRow DRM = dt.NewRow();
                    DRM["Description"] = dr["Description"];
                    DRM["Color"] = dr["Color"];
                    dt.Rows.Add(DRM);
                }

                gvSwatchSheet2.DataSource = dt;
                gvSwatchSheet2.DataBind();

            }
            else
            {
                gvSwatchSheet1.DataSource = null;
                gvSwatchSheet1.DataBind();

                gvSwatchSheet2.DataSource = null;
                gvSwatchSheet2.DataBind();
            }
        }
    }
}


