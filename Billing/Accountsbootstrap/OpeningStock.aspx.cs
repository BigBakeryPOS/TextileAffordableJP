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
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class OpeningStock : System.Web.UI.Page
    {
        OpeningStockEntry objBs = new OpeningStockEntry();
        BSClass objbs = new BSClass();
        string sStockID = "";
        string sTableName = "";
        string empid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            empid = Session["Empid"].ToString();
            if (!IsPostBack)
            {

                txtDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                sStockID = Request.QueryString.Get("OpenStockID");


                DataSet dst = new DataSet();
                dst = objbs.selectcategoryalldecriptionbranch(sTableName,"2","0");
                ddlproduct.Items.Clear();
                ddlproduct.DataSource = dst.Tables[0];
                ddlproduct.DataTextField = "serial_NO";
                ddlproduct.DataValueField = "categoryuserid";
                ddlproduct.DataBind();

                ddlItem.Items.Clear();
                ddlItem.DataSource = dst.Tables[0];
                ddlItem.DataTextField = "Definition";
                ddlItem.DataValueField = "categoryuserid";
                ddlItem.DataBind();


                    //DataSet ds = objBs.getCategory_New();
                    //if (ds.Tables[0].Rows.Count > 0)
                    //{
                    //    ddlCategory.DataSource = ds;
                    //    ddlCategory.DataTextField = "category";
                    //    ddlCategory.DataValueField = "categoryid";
                    //    ddlCategory.DataBind();
                    //    ddlCategory.Items.Insert(0, "Select Category");
                    //}


                //DataSet ds = objBs.getBrand();

                //if (ds != null)
                //{
                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        ddlBrand.DataSource = ds;
                //        ddlBrand.DataTextField = "BrandName";
                //        ddlBrand.DataValueField = "BrandId";
                //        ddlBrand.DataBind();
                //        ddlBrand.Items.Insert(0, "Select Brand");
                //    }
                //    else
                //    {
                //        ddlBrand.Items.Insert(0, "Select Brand");
                //    }
                //}
                //else
                //{
                //    ddlBrand.Items.Insert(0, "Select Brand");
                //}

                FirstGridViewRow1();

                //.Items.Insert(0, "Select Category");
                ddlItem.Items.Insert(0, "Select Product");

                if (sStockID != null)
                {
                    edit.Visible = true;
                    add.Visible = false;

                    DataSet ds1 = objBs.getbyOpnStck(Convert.ToInt32(sStockID));

                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        string sDate = ds1.Tables[0].Rows[0]["StockDate"].ToString();
                        DateTime dsDate = Convert.ToDateTime(sDate);
                        txtDate.Text = dsDate.ToString("dd/MM/yyyy");
                        //ddlBrand.SelectedValue = ds1.Tables[0].Rows[0]["StockBrand"].ToString();
                        //ddlCategory.SelectedValue = ds1.Tables[0].Rows[0]["StockCategory"].ToString();
                        ddlItem.SelectedValue = ds1.Tables[0].Rows[0]["StockItem"].ToString();
                        ddlproduct.SelectedValue = ds1.Tables[0].Rows[0]["StockItem"].ToString();
                        txtNos.Text = ds1.Tables[0].Rows[0]["Nos"].ToString();



                        //DataSet ds2 = objBs.getCategory_New(ddlCategory.SelectedValue);
                        //if (ds2.Tables[0].Rows.Count > 0)
                        //{
                        //    ddlCategory.DataSource = ds2;
                        //    ddlCategory.DataTextField = "category";
                        //    ddlCategory.DataValueField = "categoryid";
                        //    ddlCategory.DataBind();
                        //    ddlCategory.Items.Insert(0, "Select Category");
                        //}
                        //DataSet ds3 = objBs.getItem_New(ddlCategory.SelectedValue);
                        //if (ds3.Tables[0].Rows.Count > 0)
                        //{

                        //    ddlItem.DataSource = ds3;
                        //    ddlItem.DataTextField = "Definition";
                        //    ddlItem.DataValueField = "CategoryUserID";
                        //    ddlItem.DataBind();
                        //    ddlItem.Items.Insert(0, "Select Product");
                        //}
                        //DataSet ds4 = objBs.getBrand();
                        //if (ds4.Tables[0].Rows.Count > 0)
                        //{
                        //    ddlBrand.DataSource = ds4;
                        //    ddlBrand.DataTextField = "BrandName";
                        //    ddlBrand.DataValueField = "BrandId";
                        //    ddlBrand.DataBind();
                        //    ddlBrand.Items.Insert(0, "Select Brand");
                        //}
                        btnSave.Text = "Update";
                        ddlItem.Enabled = false;
                    //    ddlCategory.Enabled = false;
                        ddlproduct.Enabled = false;
                       // ddlBrand.Enabled = false;
                    }
                }
                else
                {
                    add.Visible = true;
                    edit.Visible = false;
                }
              
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
         
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            //    {
            //        DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
            //        DropDownList txtt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");

            //        if ((txtt.SelectedValue == "0") || (txtt.SelectedValue == "") || (txtt.SelectedValue == "Select Product"))
            //        {
            //            DataSet dsCategory1 = objbs.selectcategorydecription(Convert.ToInt32(txt.SelectedValue));
            //            if (dsCategory1.Tables[0].Rows.Count > 0)
            //            {
            //                txtt.Items.Clear();
            //                txtt.DataSource = dsCategory1.Tables[0];
            //                txtt.DataTextField = "Definition";
            //                txtt.DataValueField = "categoryuserid";
            //                txtt.DataBind();
            //                txtt.Items.Insert(0, "Select Product");

            //            }
            //            else
            //            {
            //                txtt.Items.Clear();
            //                txtt.Items.Insert(0, "Select Product");
            //            }
            //        }
            //    }
            //}

            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;

            DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");

            DropDownList Def = (DropDownList)row.FindControl("drpItem");
            if (drpCategory.SelectedItem.Text != "Select Category")
            {

                DataSet dsCategory1 = objBs.selectcategorydecription(Convert.ToInt32(drpCategory.SelectedValue));
                if (dsCategory1.Tables[0].Rows.Count > 0)
                {
                    Def.Items.Clear();
                    Def.DataSource = dsCategory1.Tables[0];
                    Def.DataTextField = "Definition";
                    Def.DataValueField = "categoryuserid";
                    Def.DataBind();
                    Def.Items.Insert(0, "Select Product");

                }
                else
                {
                    Def.Items.Clear();
                    Def.Items.Insert(0, "Select Product");
                }
            }
            else
            {
            
            }
            
        }

        protected void drpItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList txti = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                    TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                    TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");

                    TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                    TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                    itemc = txti.Text;


                    if ((itemc == null) || (itemc == ""))
                    {
                    }
                    else
                    {
                        for (int vLoop1 = 0; vLoop1 < GridView2.Rows.Count; vLoop1++)
                        {
                            DropDownList txt1 = (DropDownList)GridView2.Rows[vLoop1].FindControl("drpItem");
                            if (txt1.Text == "")
                            {
                            }
                            else
                            {

                                if (ii == iq)
                                {
                                }
                                else
                                {
                                    if (itemc == txt1.Text)
                                    {
                                        itemcd = txti.SelectedItem.Text;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
                                        txt1.Focus();
                                        return;

                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                    }
                    iq = iq + 1;
                    ii = 1;

                    //DataSet dsStock = new DataSet();

                    //if ((txtktt.Text == "") && (txtkt.Text == "") && (txttk.Text == "") || (txtkttt.Text == ""))
                    //{
                    //    dsStock = objBs.GetStockDetails(Convert.ToInt32(txt.SelectedValue));

                    //    if (dsStock.Tables[0].Rows.Count > 0)
                    //    {
                    //        txttk.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
                    //        txtktt.Text = dsStock.Tables[0].Rows[0]["Available_Qty"].ToString();

                    //        DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue));
                    //        txtkt.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();

                    //        txtkttt.Text = "0";
                    //    }
                    //}
                    txtktt.Focus();
                }
            }


            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");

            DropDownList Defitem = (DropDownList)row.FindControl("drpItem");

            DropDownList procode = (DropDownList)row.FindControl("ProductCode");

            if (procode.SelectedItem.Text != "Select Product Code")
            {

                DataSet dsCategory1 = objbs.selectProduct(Convert.ToInt32(Defitem.SelectedValue), sTableName);
                if (dsCategory1.Tables[0].Rows.Count > 0)
                {
                    drpCategory.Items.Clear();
                    drpCategory.DataSource = dsCategory1.Tables[0];
                    drpCategory.DataTextField = "categoryname";
                    drpCategory.DataValueField = "categoryid";
                    drpCategory.DataBind();
                    //drpCategory.Items.Insert(0, "Select Category");

                }
                else
                {
                    drpCategory.Items.Clear();
                    drpCategory.Items.Insert(0, "Select Category");
                }
            }
            else
            {
            }

            if (procode.SelectedItem.Text != "Select Product")
            {

                DataSet dsCategory1 = objbs.selectProduct(Convert.ToInt32(Defitem.SelectedValue), sTableName);
                if (dsCategory1.Tables[0].Rows.Count > 0)
                {
                    procode.SelectedValue = dsCategory1.Tables[0].Rows[0]["CategoryUserID"].ToString();
                    string cmpyid = Session["cmpyid"].ToString();
                    DataSet dst = new DataSet();
                    dst = objbs.selectcategoryalldecriptionbranch(sTableName,"2",cmpyid);
                    procode.Items.Clear();
                    procode.DataSource = dst.Tables[0];
                    procode.DataTextField = "Definition";
                    procode.DataValueField = "categoryuserid";
                    procode.DataBind();


                }
                else
                {
                    procode.Items.Clear();
                    procode.Items.Insert(0, "Select Product Code");
                }
            }
            else
            {
            }



           
           // TextBox txtBrand = (TextBox)row.FindControl("txtBrand");
            DropDownList Def = (DropDownList)row.FindControl("drpItem");
            TextBox txtQty = (TextBox)row.FindControl("txtStock");
            DataSet dsStock = new DataSet();
            if (Def.SelectedItem.Text != "Select Product")
            {
                dsStock = objbs.GetStockDetails(Convert.ToInt32(Def.SelectedValue), "tblStock_" + sTableName);

                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    DataSet dsCategory = objbs.GetTaxd(Convert.ToInt32(Def.SelectedValue));

                    double sQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
                    txtQty.Text = sQty.ToString("0.00");

                    //txtBrand.Text = dsCategory.Tables[0].Rows[0]["brandname"].ToString();
                }
            }
            else
            {
            
            }

        }


        private void SetPreviousData1()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList drpCategory =
                         (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("drpCategory");
                        TextBox txtOpStock =
                          (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtOpStock");

                        DropDownList ProductCode =
                        (DropDownList)GridView2.Rows[rowIndex].Cells[2].FindControl("ProductCode");

                        DropDownList drpItem =
                          (DropDownList)GridView2.Rows[rowIndex].Cells[2].FindControl("drpItem");
                        TextBox txtDate =
                          (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtDate");
                        //TextBox txtBrand =(TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtBrand");
                        TextBox txtStock =
                          (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtStock");

                        drpCategory.SelectedValue = dt.Rows[i]["Category"].ToString();

                        drpItem.Items.Clear();


                        //if ((drpCategory.SelectedValue != "0") && (drpCategory.SelectedValue != "Select Category"))
                        //{
                        //    DataSet dsCategory = objBs.selectcategorydecription(Convert.ToInt32(drpCategory.SelectedValue));
                        //    drpItem.Items.Add(new ListItem("Select Product", "0"));
                        //    drpItem.DataSource = dsCategory;
                        //    drpItem.DataBind();
                        //    drpItem.DataTextField = "Definition";
                        //    drpItem.DataValueField = "categoryuserid";
                        //}
                        string cmpyid = Session["cmpyid"].ToString();
                        DataSet dst = objbs.selectcategoryalldecriptionbranch(sTableName,"2",cmpyid);
                        drpItem.Items.Add(new ListItem("Select Product Code", "0"));
                        drpItem.DataSource = dst;
                        drpItem.DataBind();
                        drpItem.DataTextField = "Serial_No";
                        drpItem.DataValueField = "categoryuserid";

                        txtDate.Text = dt.Rows[i]["Date"].ToString();
                        ProductCode.Text = dt.Rows[i]["ProductCode"].ToString();
                        txtOpStock.Text = dt.Rows[i]["OpStock"].ToString();
                        drpItem.SelectedValue = dt.Rows[i]["Product"].ToString();
                       // txtBrand.Text = dt.Rows[i]["Brand"].ToString();
                        txtStock.Text = dt.Rows[i]["Stock"].ToString();

                        rowIndex++;

                    }
                }
            }
        }


        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet ds = new DataSet();
            ds = objbs.categorymaster(sTableName);

            DataSet dst = new DataSet();
            string cmpyid = Session["cmpyid"].ToString();
           
            dst = objbs.selectcategoryalldecriptionbranch(sTableName,"2",cmpyid);



            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtttk = (TextBox)e.Row.FindControl("txtStock");
                TextBox txttk = (TextBox)e.Row.FindControl("txtDate");
                TextBox txttkk = (TextBox)e.Row.FindControl("txtOpStock");
                //TextBox txtkt = (TextBox)e.Row.FindControl("txtBrand");

                txtttk.Text = "0";
                txttkk.Text = "0";
                txttk.Text = DateTime.Now.ToString("dd/MM/yyyy");


                var ddl = (DropDownList)e.Row.FindControl("drpCategory");
                ddl.DataSource = ds;
                ddl.DataTextField = "category";
                ddl.DataValueField = "categoryid";
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Category");



                var ddlt = (DropDownList)e.Row.FindControl("drpItem");
                ddlt.DataSource = dst;
                ddlt.DataTextField = "Serial_No";
                ddlt.DataValueField = "categoryuserid";
                ddlt.DataBind();
                ddlt.Items.Insert(0, "Select Product");


                var ddlPcode = (DropDownList)e.Row.FindControl("ProductCode");
                ddlPcode.DataSource = dst;
                ddlPcode.DataTextField = "Definition";
                ddlPcode.DataValueField = "categoryuserid";
                ddlPcode.DataBind();
                ddlPcode.Items.Insert(0, "Select Product Code");


            }


            //DataSet ds = new DataSet();
            //ds = objBs.selectcategorymaster();

            //DataSet dst = new DataSet();
            //dst = objbs.selectcategoryalldecriptionbranch(sTableName);

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    TextBox txtttk = (TextBox)e.Row.FindControl("txtStock");
            //    TextBox txttk = (TextBox)e.Row.FindControl("txtDate");
            //    TextBox txttkk = (TextBox)e.Row.FindControl("txtOpStock");
            //    //TextBox txtkt = (TextBox)e.Row.FindControl("txtBrand");

            //    txtttk.Text = "0";
            //    txttkk.Text = "0";
            //    txttk.Text = DateTime.Now.ToString("dd/MM/yyyy");

            //    var ddl = (DropDownList)e.Row.FindControl("drpCategory");
            //    ddl.DataSource = ds;
            //    ddl.DataTextField = "category";
            //    ddl.DataValueField = "categoryid";
            //    ddl.DataBind();
            //    ddl.Items.Insert(0, "Select Category");

            //    var ddlt = (DropDownList)e.Row.FindControl("drpItem");
            //    ddlt.DataSource = dst;
            //    ddlt.DataTextField = "Definition";
            //    ddlt.DataValueField = "categoryuserid";
            //    ddlt.DataBind();
            //    ddlt.Items.Insert(0, "Select Product");
            //}
        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            AddNewRow1();
            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                DropDownList txttt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
                DropDownList txtd = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");
                TextBox txttkk = (TextBox)GridView2.Rows[vLoop].FindControl("txtOpStock");
                //TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtBrand");

                string stock = txtttk.Text;
                string date = txttk.Text;
                string opstock = txttkk.Text;

                if (stock != "" || date != "" || opstock != "")
                {

                }
                else
                {
                    txtttk.Text = "0";
                    txttkk.Text = "0";
                    txttk.Text = DateTime.Now.ToString("dd/MM/yyyy");
                   
                }
            }
        }

        private void AddNewRow1()
        {
            for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
            {
                DropDownList txttt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
                DropDownList txtd = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtOpStock");
                TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");
                //TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtBrand");
                
                int col = vLoop + 1;

                if ((txtd.SelectedValue == "0") || (txtd.SelectedValue == "Select Product Code"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Category in row " + col + " ')", true);
                    txtd.Focus();
                    return;
                }
                if ((txt.SelectedValue == "0") || (txt.SelectedValue == "Select Product"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Product in row " + col + " ')", true);
                    txt.Focus();
                    return;
                }
                if ((txtttk.Text == "") || (Convert.ToDouble(txtttk.Text) == 0))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please OpStock in row " + col + " ')", true);
                    txtttk.Focus();
                    return;
                }

                if ((txttk.Text == ""))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Date in row " + col + " ')", true);
                    txttk.Focus();
                    return;
                }
                //if (Convert.ToInt32(txtttk.Text) > Convert.ToInt32(txtktt.Text))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty greater than the Current stock in row " + col + " ')", true);
                //    txtttk.Focus();
                //    return;
                //}
                //if ((txtktttt.Text == ""))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Discount in row " + col + " ')", true);
                //    txtttk.Focus();
                //    return;
                //}
            }

            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        DropDownList drpCategory =
                         (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("drpCategory");
                        TextBox txtOpStock =
                          (TextBox)GridView2.Rows[rowIndex].Cells[2].FindControl("txtOpStock");

                        DropDownList ProductCode =
                        (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("ProductCode");

                        DropDownList drpItem =
                         (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("drpItem");
                        TextBox txtDate =
                          (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtDate");
                        TextBox txtStock =
                          (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtStock");
                        //TextBox txtBrand =(TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtBrand");
                        

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Category"] = drpCategory.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["OpStock"] = txtOpStock.Text;
                        dtCurrentTable.Rows[i - 1]["Date"] = txtDate.Text;

                        dtCurrentTable.Rows[i - 1]["Stock"] = txtStock.Text;
                        dtCurrentTable.Rows[i - 1]["ProductCode"] = ProductCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Product"] = drpItem.SelectedValue;
                       // dtCurrentTable.Rows[i - 1]["Brand"] = txtBrand.Text;
                        
                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    GridView2.DataSource = dtCurrentTable;
                    GridView2.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData1();
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData1();
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();




                    //for (int i = 0; i < GridView2.Rows.Count; i++)
                    //{
                    //    GridView2.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    //}
                    SetPreviousData1();

                    //txtRate_TextChanged(sender, e);
                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GridView2.DataSource = dt;
                    GridView2.DataBind();




                    //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    //{
                    //    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    //}
                    SetPreviousData1();

                    FirstGridViewRow1();
                }
            }
        }

        private void SetRowData1()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        DropDownList drpCategory =
                         (DropDownList)GridView2.Rows[rowIndex].Cells[1].FindControl("drpCategory");
                        TextBox txtOpStock =
                          (TextBox)GridView2.Rows[rowIndex].Cells[3].FindControl("txtOpStock");

                        DropDownList ProductCode =
                       (DropDownList)GridView2.Rows[rowIndex].Cells[2].FindControl("ProductCode");

                        DropDownList drpItem =
                          (DropDownList)GridView2.Rows[rowIndex].Cells[2].FindControl("drpItem");
                        //TextBox txtBrand =(TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtBrand");
                        TextBox txtDate =
                         (TextBox)GridView2.Rows[rowIndex].Cells[4].FindControl("txtDate");
                       
                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Category"] = drpCategory.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["Brand"] = txtBrand.Text;
                        dtCurrentTable.Rows[i - 1]["Product"] = drpItem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ProductCode"] = ProductCode.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["OpStock"] = txtOpStock.Text;

                        dtCurrentTable.Rows[i - 1]["Date"] = txtDate.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    GridView2.DataSource = dtCurrentTable;
                    GridView2.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData1();
        }

        private void FirstGridViewRow1()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("Category", typeof(string)));
            dtt.Columns.Add(new DataColumn("ProductCode", typeof(string)));
            dtt.Columns.Add(new DataColumn("Product", typeof(string)));
           // dtt.Columns.Add(new DataColumn("Brand", typeof(string)));
            dtt.Columns.Add(new DataColumn("OpStock", typeof(string)));
            dtt.Columns.Add(new DataColumn("Stock", typeof(string)));
            dtt.Columns.Add(new DataColumn("Date", typeof(string)));
            dr = dtt.NewRow();
            dr["Category"] = string.Empty;
            dr["Product"] = string.Empty;
            dr["ProductCode"] = string.Empty;
           // dr["Brand"] = string.Empty;
            dr["OpStock"] = string.Empty;
            dr["Stock"] = string.Empty;
            dr["Date"] = string.Empty;
            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            GridView2.DataSource = dtt;
            GridView2.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("Category");
            dttt.Columns.Add(dct);

            dct = new DataColumn("ProductCode");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Product");
            dttt.Columns.Add(dct);


           // dct = new DataColumn("Brand");
           // dttt.Columns.Add(dct);

            dct = new DataColumn("OpStock");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Stock");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Date");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["ProductCode"] = "";
            drNew["Product"] = "";
            drNew["Category"] = "";
            //drNew["Brand"] = 0;
            drNew["OpStock"] = 0;
            drNew["Stock"] = 0;
            drNew["Date"] = 0;
            dstd.Tables[0].Rows.Add(drNew);

            GridView2.DataSource = dstd;
            GridView2.DataBind();
        }

      
           
       

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet ds = objBs.getItem_New(ddlCategory.SelectedValue);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
                
            //    ddlItem.DataSource = ds;
            //    ddlItem.DataTextField = "Definition";
            //    ddlItem.DataValueField = "CategoryUserID";
            //    ddlItem.DataBind();
            //    ddlItem.Items.Insert(0, "Select Product");
            //}
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("OpeningStockMaster.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            DateTime date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnSave.Text == "Save")
            {

                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList txttt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
                    DropDownList txtd = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                    DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                    TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtOpStock");
                    TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtDate");
                   // TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtBrand");

                    int col = vLoop + 1;

                    if ((txtd.SelectedValue == "0") || (txtd.SelectedValue == "Select Product Code"))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Category in row " + col + " ')", true);
                        txtd.Focus();
                        return;
                    }
                    if ((txt.SelectedValue == "0") || (txt.SelectedValue == "Select Product"))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Product in row " + col + " ')", true);
                        txt.Focus();
                        return;
                    }
                    if ((txtttk.Text == "") || (Convert.ToDouble(txtttk.Text) == 0))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please OpStock in row " + col + " ')", true);
                        txtttk.Focus();
                        return;
                    }

                    if ((txttk.Text == ""))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Date in row " + col + " ')", true);
                        txttk.Focus();
                        return;
                    }
                }


                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                DataRow dr;

                dt.Columns.Add(new DataColumn("Category", typeof(string)));
                dt.Columns.Add(new DataColumn("ProductCode", typeof(string)));
               // dt.Columns.Add(new DataColumn("Brand", typeof(string)));
                dt.Columns.Add(new DataColumn("Date", typeof(string)));
                dt.Columns.Add(new DataColumn("OpStock", typeof(string)));
                dt.Columns.Add(new DataColumn("Item", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("CategoryName", typeof(string)));
                
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList txtt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
                    DropDownList txtd = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                    DropDownList txt = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                   // TextBox txtttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtBrand");
                    TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtdate");
                    DateTime billldate = DateTime.ParseExact(txttk.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtOpStock");
                    DropDownList txtkt1 = (DropDownList)GridView2.Rows[vLoop].FindControl("drpItem");
                    DropDownList txtkt2 = (DropDownList)GridView2.Rows[vLoop].FindControl("drpCategory");
                    
                    dr = dt.NewRow();
                    dr["Category"] = txtt.SelectedValue;
                   // dr["Brand"] = txtttk.Text;
                    dr["Date"] = billldate;
                    dr["OpStock"] = txtkt.Text;
                    dr["Item"] = txt.SelectedValue;
                    dr["ProductCode"] = txtd.SelectedValue;
                    dr["ItemName"] = txtkt1.SelectedItem.Text;
                    dr["CategoryName"] = txtkt2.SelectedItem.Text;
                    dt.Rows.Add(dr);

                }

                ds.Merge(dt);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drd in ds.Tables[0].Rows)
                    {
                        DataSet dsCategory = objBs.openingstockalreadyexists(drd["Item"].ToString(),sTableName);
                        if (dsCategory.Tables[0].Rows.Count > 0)
                        {

                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Already this item exists in opening stock.')", true);
                            return;
                        }
                    }
                }


                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drd in ds.Tables[0].Rows)
                    {
                       
                            //  DateTime date1 = Convert.ToDateTime(txtDate.Text);
                            //   string sdate1 = date1.ToString("yyyy-MM-dd");

                        int iSuccess = objBs.InsertOpeningStock_New(Convert.ToDateTime(drd["Date"]),drd["category"].ToString(), drd["Item"].ToString(), Convert.ToDouble(drd["OpStock"]), "tblAuditMaster_" + sTableName, lblUser.Text, drd["CategoryName"].ToString(), drd["ItemName"].ToString(),sTableName,empid);
                        DataSet dst = objBs.CheckStock(drd["category"].ToString(), drd["Item"].ToString(), "tblStock_" + sTableName);
                            if (dst.Tables[0].Rows.Count > 0)
                            {
                                string sID = dst.Tables[0].Rows[0]["SubCategoryID"].ToString();
                                string sStock = dst.Tables[0].Rows[0]["Available_QTY"].ToString();
                                double iTotal = Convert.ToDouble(sStock) + Convert.ToDouble(drd["OpStock"]);

                                int iupdate2 = objBs.UpdateStock1(sID, iTotal.ToString(), "tblStock_" + sTableName);
                            }
                        
                    }
                }

                //else
                //{
                //    int iSuccess2 = objBs.InsertStock(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlItem.SelectedValue), Convert.ToInt32(txtNos.Text),ddlBrand.SelectedValue);
                //}
                Response.Redirect("OpeningStockMaster.aspx");
            }
            else
            {

                sStockID = Request.QueryString.Get("OpenStockID");
                if (txtDate.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Plaese select Date.It cannot be left blank.')", true);
                    return;
                }
                //if (ddlBrand.SelectedItem.Value == "Select Brand")
                //{
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Plaese select Brand.It cannot be left blank.')", true);
                //    return;
                //}
                //if (ddlCategory.SelectedItem.Value == "Select Category")
                //{
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Plaese select Category.It cannot be left blank.')", true);
                //    return;
                //}
                if (ddlItem.SelectedItem.Value == "Select Product")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Plaese select Product.It cannot be left blank.')", true);
                    return;
                }
                if (txtNos.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('Please enter Quantity.It cannot be left blank.')", true);
                    return;
                }


                int iUpdate = objBs.UpdateStock_New("", ddlItem.SelectedValue, txtNos.Text, date, sStockID, "", ddlItem.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text, "tblStock_" + sTableName,empid);
                DataSet ds = objBs.CheckStock("", ddlItem.SelectedValue, "tblStock_" + sTableName);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sID = ds.Tables[0].Rows[0]["SubCategoryID"].ToString();
                    string sStock = ds.Tables[0].Rows[0]["Available_QTY"].ToString();
                    double iTotal = Convert.ToDouble(sStock) + Convert.ToDouble(txtNos.Text);

                    int iupdate2 = objBs.UpdateStock1(sID, iTotal.ToString(), "tblStock_" + sTableName);
                }
                Response.Redirect("OpeningStockMaster.aspx");
            }

        }
        protected void ProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {

            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList txti = (DropDownList)GridView2.Rows[vLoop].FindControl("ProductCode");
                    TextBox txttk = (TextBox)GridView2.Rows[vLoop].FindControl("txtRate");
                    TextBox txtkt = (TextBox)GridView2.Rows[vLoop].FindControl("txtTax");

                    TextBox txtktt = (TextBox)GridView2.Rows[vLoop].FindControl("txtStock");
                    TextBox txtkttt = (TextBox)GridView2.Rows[vLoop].FindControl("txtDiscount");

                    itemc = txti.Text;


                    if ((itemc == null) || (itemc == ""))
                    {
                    }
                    else
                    {
                        for (int vLoop1 = 0; vLoop1 < GridView2.Rows.Count; vLoop1++)
                        {
                            DropDownList txt1 = (DropDownList)GridView2.Rows[vLoop1].FindControl("ProductCode");
                            if (txt1.Text == "")
                            {
                            }
                            else
                            {

                                if (ii == iq)
                                {
                                }
                                else
                                {
                                    if (itemc == txt1.Text)
                                    {
                                        itemcd = txti.SelectedItem.Text;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
                                        txt1.Focus();
                                        return;

                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                    }
                    iq = iq + 1;
                    ii = 1;

                    //DataSet dsStock = new DataSet();

                    //if ((txtktt.Text == "") && (txtkt.Text == "") && (txttk.Text == "") || (txtkttt.Text == ""))
                    //{
                    //    dsStock = objBs.GetStockDetails(Convert.ToInt32(txt.SelectedValue));

                    //    if (dsStock.Tables[0].Rows.Count > 0)
                    //    {
                    //        txttk.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
                    //        txtktt.Text = dsStock.Tables[0].Rows[0]["Available_Qty"].ToString();

                    //        DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue));
                    //        txtkt.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();

                    //        txtkttt.Text = "0";
                    //    }
                    //}
                    txtktt.Focus();
                }
            }


            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            if (ViewState["CurrentTable1"] != null)
            {

                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < GridView2.Rows.Count; vLoop++)
                {
                    DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");

                    DropDownList Def = (DropDownList)row.FindControl("drpItem");



                    DropDownList procode = (DropDownList)row.FindControl("ProductCode");

                    if (procode.SelectedItem.Text != "Select Product Code")
                    {

                        DataSet dsCategory1 = objbs.selectProduct(Convert.ToInt32(procode.SelectedValue), sTableName);
                        if (dsCategory1.Tables[0].Rows.Count > 0)
                        {
                            drpCategory.Items.Clear();
                            drpCategory.DataSource = dsCategory1.Tables[0];
                            drpCategory.DataTextField = "categoryname";
                            drpCategory.DataValueField = "categoryid";
                            drpCategory.DataBind();
                            //drpCategory.Items.Insert(0, "Select Category");

                        }
                        else
                        {
                            drpCategory.Items.Clear();
                            drpCategory.Items.Insert(0, "Select Category");
                        }
                    }
                    else
                    {
                    }

                    if (procode.SelectedItem.Text != "Select Product Code")
                    {

                        DataSet dsCategory1 = objbs.selectProduct(Convert.ToInt32(procode.SelectedValue), sTableName);
                        if (dsCategory1.Tables[0].Rows.Count > 0)
                        {
                            string cmpyid = Session["cmpyid"].ToString();
                            DataSet dst = new DataSet();
                            dst = objbs.selectcategoryalldecriptionbranch(sTableName,"2",cmpyid);
                            Def.Items.Clear();
                            Def.DataSource = dst.Tables[0];
                            Def.DataTextField = "serial_NO";
                            Def.DataValueField = "categoryuserid";
                            Def.DataBind();

                            Def.SelectedValue = dsCategory1.Tables[0].Rows[0]["CategoryUserID"].ToString();

                        }
                        else
                        {
                            Def.Items.Clear();
                            Def.Items.Insert(0, "Select Product");
                        }
                    }
                    else
                    {
                    }
                }
            }


            DropDownList ProductCode = (DropDownList)row.FindControl("ProductCode");
        
            TextBox txtQty = (TextBox)row.FindControl("txtStock");
            TextBox txtOpStock = (TextBox)row.FindControl("txtOpStock");
            DataSet dsStock = new DataSet();
            if (ProductCode.SelectedItem.Text != "Select Product Code")
            {
                dsStock = objbs.GetStockDetails(Convert.ToInt32(ProductCode.SelectedValue), "tblStock_" + sTableName);

                if (dsStock.Tables[0].Rows.Count > 0)
                {
                    DataSet dsCategory = objbs.GetTaxd(Convert.ToInt32(ProductCode.SelectedValue));

                    double sQty = Convert.ToDouble(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
                    txtQty.Text = sQty.ToString("0.00");

                    //txtBrand.Text = dsCategory.Tables[0].Rows[0]["brandname"].ToString();
                }
            }
            else
            {

            }
            txtOpStock.Focus();

            //TextBox txtRate = (TextBox)row.FindControl("txtRate");
            //TextBox txt = (TextBox)row.FindControl("txtDiscount");
            //TextBox txtTax = (TextBox)row.FindControl("txtTax");
            //DropDownList Defitem = (DropDownList)row.FindControl("drpItem");
            //DropDownList cate = (DropDownList)row.FindControl("drpCategory");
            //DropDownList ProductCode = (DropDownList)row.FindControl("ProductCode");
            //TextBox txtQty = (TextBox)row.FindControl("txtStock");
            //DataSet dsStock = new DataSet();


            //dsStock = objbs.GetStockDetails(Convert.ToInt32(ProductCode.SelectedValue), "tblStock_" + sTableName);

            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    DataSet dsCategory = objbs.GetTax(Convert.ToInt32(ProductCode.SelectedValue), sTableName);

            //    var Itx = Convert.ToDecimal(dsCategory.Tables[0].Rows[0]["Tax"].ToString());
            //    txtTax.Text = Itx.ToString();

            //    Decimal Irate = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString());
            //    txtRate.Text = Decimal.Round(Irate, 2).ToString("f2");

            //    decimal sQty = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
            //    txtQty.Text = sQty.ToString("f2");
            //    cate.SelectedValue = dsCategory.Tables[0].Rows[0]["categoryid"].ToString();

                //txt.Text = "0";

                //string value = ProductCode.SelectedValue;
                //DataSet ds = objBs.itemhistorypopup(sTableName, value);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    txtitemhis.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
                //}
                //else
                //{
                //    txtitemhis.Text = "";
                //}


                //string cust = ddlcustomerID.SelectedValue;
                //if (cust == "Select Customer")
                //{
                //}
                //else
                //{
                //    DataSet ds1 = objBs.custhistorypopup(sTableName, value, cust);
                //    if (ds1.Tables[0].Rows.Count > 0)
                //    {
                //        txtcusthis.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
                //    }
                //    else
                //    {
                //        txtcusthis.Text = "";
                //    }
                //}

                // txtTamt5.Text = dsCategory.Tables[0].Rows[0]["Meter1"].ToString();
            }


        }
    }

