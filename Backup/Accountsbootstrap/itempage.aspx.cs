using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using CommonLayer;
using System.Net.NetworkInformation;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class itempage : System.Web.UI.Page
    {
        DataSet tbllogin = new DataSet();
        BSClass objBs = new BSClass();
        OpeningStockEntry objBs1 = new OpeningStockEntry();
        string cust = "";
        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            //int value = 10;
            //txtPCode.Text = String.Format("{0:0000}", value); 
           
            //btnadd.Attributes.Add("onclick", "return valchk();"); // Validation Check lines
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            string name = Request.QueryString.Get("name");

            if (name == "Add New")
            {
                add.Visible = true;
                div1.Visible = false;
                hd1.InnerText = "Product Master";
            }
            else if (name == "Bulk Addition")
            {
                add.Visible = false;
                div1.Visible = true;
                hd1.InnerText = "Bulk Product Master Addition";
            }

            if (!IsPostBack)
            {
                if (sTableName == "admin")
                {
                    purchase.Visible = true;
                }
            DataSet dsCategory = objBs.categorymaster(sTableName);
            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlcategory.DataSource = dsCategory.Tables[0];
                ddlcategory.DataTextField = "category";
                ddlcategory.DataValueField = "CategoryID";
                ddlcategory.DataBind();
                ddlcategory.Items.Insert(0, "Select Category");
                //ddlcategory.Items.Insert(0, "Select Category");


                
                  
             }

            string super = Session["IsSuperAdmin"].ToString();


            if (super == "1")
            {
                drpbranch.Enabled = true;

                DataSet dbraqnch = objBs.GetCompanyDet();
                if (dbraqnch.Tables[0].Rows.Count > 0)
                {
                    drpbranch.DataSource = dbraqnch.Tables[0];
                    drpbranch.DataTextField = "CompanyName";
                    drpbranch.DataValueField = "Comapanyid";
                    drpbranch.DataBind();
                    drpbranch.Items.Insert(0, "Select Branch");
                }
            }
            else
            {

                drpbranch.Enabled = false;
                DataSet dbraqnch = objBs.GetCompanyDet();
                if (dbraqnch.Tables[0].Rows.Count > 0)
                {
                    drpbranch.DataSource = dbraqnch.Tables[0];
                    drpbranch.DataTextField = "CompanyName";
                    drpbranch.DataValueField = "Comapanyid";
                    drpbranch.DataBind();
                    drpbranch.SelectedValue = Session["cmpyid"].ToString();
                }
            }

         

            DataSet meter = objBs.UNITS();
            if (meter.Tables[0].Rows.Count > 0)
            {
                ddlmeter.DataSource = meter.Tables[0];
                ddlmeter.DataTextField = "Units";
                ddlmeter.DataValueField = "UOMID";
                ddlmeter.DataBind();
               // ddlmeter.Items.Insert(0, "Select Units");
               // ddlmeter.SelectedIndex = 1;
            }
            //DataSet brand = objBs.selectBrand();
            //if (brand.Tables[0].Rows.Count > 0)
            //{
            //    ddlBrand.DataSource = brand.Tables[0];
            //    ddlBrand.DataTextField = "BrandName";
            //    ddlBrand.DataValueField = "BrandId";
            //    ddlBrand.DataBind();
            //    ddlBrand.Items.Insert(0, "Select Brand");
            //}



            btnadd.Text = "Save";
            cust = Request.QueryString.Get("cust");
            if (cust != "" || cust != null)
            {

                DataSet ds = objBs.getvalues(cust, sTableName);
                DataSet dq = objBs.getstockforitems(cust, sTableName);
              //  DataSet dopen = objBs.getopeningstock(cust, sTableName);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //brand = objBs.selectBrandlist();
                    //if (brand.Tables[0].Rows.Count > 0)

                    //    ddlBrand.DataSource = brand.Tables[0];
                    //    ddlBrand.DataTextField = "BrandName";
                    //    ddlBrand.DataValueField = "BrandId";
                    //    ddlBrand.DataBind();
                    //    ddlBrand.Items.Insert(0, "Select Brand");

                    DataSet dsCategory1 = objBs.selectcategorylist();
                    if (dsCategory1.Tables[0].Rows.Count > 0)
                    {
                        ddlcategory.DataSource = dsCategory1.Tables[0];
                        ddlcategory.DataTextField = "category";
                        ddlcategory.DataValueField = "CategoryID";
                        ddlcategory.DataBind();
                        ddlcategory.Items.Insert(0, "Select Category");
                    }
                    DataSet dbraqnch1 = objBs.GetCompanyDet();
                    if (dbraqnch1.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch1.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "Select Branch");
                    }

                    btnadd.Text = "Update";
                    div1.Visible = false;
                    hd1.InnerText = "Product Master";
                    txtPCode.Enabled = false;
                    drpbranch.Enabled = false;
                  //  txtstockid.Text = dq.Tables[0].Rows[0]["StockID"].ToString();
                    txtmrp.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Rate"]).ToString("f2");
                    txtdelearprice.Text ="0";
                    txtpurchaseprice.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["PurchaseRate"]).ToString("f2");
                    txtcommodity.Text = ds.Tables[0].Rows[0]["CommodityCode"].ToString();
                    rbdtype.SelectedValue = ds.Tables[0].Rows[0]["Type"].ToString();
                    lblFile_Path.Text = ds.Tables[0].Rows[0]["Image"].ToString();
                    img_Photo.ImageUrl = ds.Tables[0].Rows[0]["Image"].ToString();
                  //  txtopeningstock.Text = dopen.Tables[0].Rows[0]["Nos"].ToString();
                 //   txtopstockid.Text = dopen.Tables[0].Rows[0]["OpenStockID"].ToString();

                    ddlcategory.SelectedValue = ds.Tables[0].Rows[0]["CategoryID"].ToString();
                    drpbranch.SelectedValue = ds.Tables[0].Rows[0]["Companyid"].ToString();
                    //    ddlBrand.SelectedValue = ds.Tables[0].Rows[0]["BrandCode"].ToString();
                    txtPCode.Text = ds.Tables[0].Rows[0]["Definition"].ToString();
                    txtPName.Text = ds.Tables[0].Rows[0]["Serial_No"].ToString();
                    txtDis.Text = dq.Tables[0].Rows[0]["MinQty"].ToString();
                    string Tax = ds.Tables[0].Rows[0]["Tax"].ToString();
                    ddlmeter.SelectedValue = ds.Tables[0].Rows[0]["meter"].ToString();
                    if (Tax == "5")
                    {
                        ddltax.SelectedValue = "1";
                    }
                    else if (Tax == "12")
                    {
                        ddltax.SelectedValue = "2";
                    }
                    else if (Tax == "18")
                    {
                        ddltax.SelectedValue = "4";
                    }
                    else
                    {
                        ddltax.SelectedValue = "5";
                    }
                

                    // ddlIsActive.SelectedItem.Text = ds.Tables[0].Rows[0]["Size"].ToString();
                    string sValue = ds.Tables[0].Rows[0]["IsActive"].ToString();
                    if (sValue == "Yes")

                        ddlIsActive.SelectedValue = "1";
                    else
                        ddlIsActive.SelectedValue = "2";
                }
            }
            if (btnadd.Text == "Save")
            {
                //DataSet getmax = new DataSet();
                //getmax = objBs.getmaxofitemcode(sTableName);
                //if (getmax.Tables[0].Rows.Count > 0)
                //{
                //    int value = Convert.ToInt32(getmax.Tables[0].Rows[0]["code"]);
                //    txtPCode.Text = String.Format("{0:0000}", value); 
                //}
            }

                //string sPage=Request.QueryString.Get("Page");
                //if (sPage == "refresh")
                //{
                //    Response.Redirect("../Accountsbootstrap/itempage.aspx");
                //}

            }
        }

        protected void meter(object sender, EventArgs e)
        {
            //cust = Request.QueryString.Get("cust");
            //if (cust == null || cust == "")
            //{
            //    string met = ddlmeter.SelectedItem.Text;
            //    txtPName.Text = met + ',';
            //}
            //else
            //{
            //     DataSet ds = objBs.getvalues(cust,sTableName);
            //     if (ds.Tables[0].Rows.Count > 0)
            //     {
            //         txtPName.Text = ds.Tables[0].Rows[0]["Serial_No"].ToString();
            //         var RunIDStartTime = txtPName.Text;
            //         var listSplit = RunIDStartTime.Split(',');
            //         string id = (listSplit[0]);
            //         string des = (listSplit[1]);
            //         string met = ddlmeter.SelectedItem.Text;
            //         txtPName.Text = met + ',' + des;
            //     }
            //}
        }


        protected void btnUpload_Clickimg(object sender, EventArgs e)
        {
            if (fp_Upload.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload.PostedFile.FileName);
                fp_Upload.PostedFile.SaveAs(Server.MapPath("~/Files/") + fileName);
                lblFile_Path.Text = "~/Files/" + fp_Upload.PostedFile.FileName;
                img_Photo.ImageUrl = "~/Files/" + fp_Upload.PostedFile.FileName;
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            
            string sValue="";
        if (chkPurchsse.Checked == true)
           sValue = "1";
        else
            sValue = "0";
        if (ddlcategory.SelectedValue == "Select Category")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Category name.Thank You!!!')", true);
            return;
        }

        if (drpbranch.SelectedValue == "Select Branch")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Branch Name.Thank You!!!')", true);
            return;

        }
        if (drpbranch.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Branch Name.Thank You!!!')", true);
            return;

        }

        if (ddlcategory.SelectedValue == "0")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Category name.Thank You!!!')", true);
            return;
        }
        if (txtmrp.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter MRP or else Enter Zero.Thank You!!!')", true);
            return;
        }
        else if (txtpurchaseprice.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Purchase Price or else Enter zero.Thank You!!!')", true);
            return;
        }
        else if (txtdelearprice.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Dealer Price or else Enter Zero.Thank You!!!')", true);
            return;
        }
        else if (txtopeningstock.Text == "")
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Opening Stock or else enter Zero.Thank You!!!')", true);
            return;
        }
        

            cust = Request.QueryString.Get("cust");
            if (cust != "" || cust != null)
            {
                if (btnadd.Text == "Save")
                {
                    //DataSet ds = objBs.defsrchgrid(txtPCode.Text, ddlcategory.SelectedValue);
                    DataSet ds = objBs.itemexists(txtPCode.Text,sTableName);
                    if (ds.Tables[0].Rows.Count==0)
                    {
                        int iStatus = objBs.insertcategorynew(Convert.ToInt32(ddlcategory.SelectedValue), txtPCode.Text, txtPName.Text, sValue, Convert.ToInt32(ddlIsActive.SelectedValue), Convert.ToDouble(ddltax.SelectedItem.Text), Convert.ToInt32("0"), ddlIsActive.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text, "0", ddlcategory.SelectedItem.Text, Convert.ToDouble(txtDis.Text), Convert.ToInt32(ddlmeter.SelectedValue), sTableName, txtcommodity.Text, txthsncode.Text, txtmrp.Text, rbdtype.SelectedValue.ToString(), txtpurchaseprice.Text, lblFile_Path.Text,drpbranch.SelectedValue);

                        DataSet dsget = objBs.getCategoryCodenew(ddlcategory.SelectedValue, sTableName, txtPCode.Text);
                        string sGetID = dsget.Tables[0].Rows[0]["CategoryUserID"].ToString();
                        int iSuccess = objBs.InsertStock1(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlcategory.SelectedValue), Convert.ToInt32(sGetID), Convert.ToInt32(0), "tblAuditMaster_" + sTableName, lblUser.Text, "0", ddlcategory.SelectedItem.Text, ddlIsActive.SelectedItem.Text, txtPCode.Text, ddltax.SelectedItem.Text, "tblStock_" + sTableName, txtmrp.Text, txtdelearprice.Text, txtpurchaseprice.Text, txtDis.Text, rbdtype.SelectedValue);

                        //string date = DateTime.Now.ToString("dd/MM/yyyy");
                        //DateTime billldate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //int iSuccess1 = objBs1.InsertOpeningStock_New(billldate, ddlcategory.SelectedValue, sGetID, Convert.ToInt32(txtopeningstock.Text), "tblAuditMaster_" + sTableName, lblUser.Text, ddlcategory.SelectedItem.Text, sGetID,sTableName);
                        //DataSet dst = objBs1.CheckStock(ddlcategory.SelectedValue,sGetID, "tblStock_" + sTableName);
                        //if (dst.Tables[0].Rows.Count > 0)
                        //{
                        //    string sID = dst.Tables[0].Rows[0]["SubCategoryID"].ToString();
                        //    string sStock = dst.Tables[0].Rows[0]["Available_QTY"].ToString();
                        //    int iTotal = Convert.ToInt32(sStock) + Convert.ToInt32(txtopeningstock.Text);

                        //    int iupdate2 = objBs1.UpdateStock1(sID, iTotal.ToString(), "tblStock_" + sTableName);
                        //}
                    
                    }
                    else
                    {
                       // lblerror.Text = "These Definition has already Excided please enter a new one";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Definition has already Exists please enter a new one')", true);
                        return;
                       
                        
                    }
                }
                else
                {
                    //DataSet ds = objBs.defsrchgrid(txtPCode.Text, ddlcategory.SelectedValue);
                    DataSet ds = objBs.itemexistsupdate(txtPCode.Text, cust,sTableName);
                    if (ds.Tables[0].Rows.Count == 0)
                    {
                        objBs.updatedefinitionnew(txtPCode.Text, cust, txtPName.Text, ddlIsActive.SelectedValue, Convert.ToInt32(sValue), Convert.ToDouble(ddltax.SelectedItem.Text), "0", ddlIsActive.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text, "0", ddlcategory.SelectedItem.Text, Convert.ToDouble(txtDis.Text), Convert.ToInt32(ddlcategory.SelectedValue), Convert.ToInt32(ddlmeter.SelectedValue), txtcommodity.Text, sTableName, txthsncode.Text, txtmrp.Text, rbdtype.SelectedValue.ToString(), txtpurchaseprice.Text, lblFile_Path.Text);
                        DataSet dsget = objBs1.getCategoryCode1(txtPCode.Text);
                        string sGetID = dsget.Tables[0].Rows[0]["CategoryUserID"].ToString();
                        int iupdate2 = objBs.updatestockdetDefinitionnew(Convert.ToInt32(ddlcategory.SelectedValue), Convert.ToInt32(sGetID), cust, Convert.ToInt32(0), "tblStock_" + sTableName, txtmrp.Text, txtdelearprice.Text, txtpurchaseprice.Text, txtDis.Text, Convert.ToInt32(Session["UserID"]), txtDis.Text, rbdtype.SelectedValue);
                        //if (iupdate2 == 0)
                        //{
                        //    int iSuccess = objBs1.InsertStock1(Convert.ToInt32(Session["UserID"]), Convert.ToInt32(ddlcategory.SelectedValue), Convert.ToInt32(sGetID), Convert.ToInt32(0), "tblAuditMaster_" + sTableName, lblUser.Text, "0", ddlcategory.SelectedItem.Text, ddlIsActive.SelectedItem.Text, txtPCode.Text, ddltax.SelectedItem.Text, "tblStock_" + sTableName, txtmrp.Text, txtdelearprice.Text, txtpurchaseprice.Text, txtDis.Text);
                        //}

                        //string date = DateTime.Now.ToString("dd/MM/yyyy");
                        //DateTime billldate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //int iUpdate = objBs1.UpdateStock_New(ddlcategory.SelectedValue, sGetID, txtopeningstock.Text, billldate, txtopstockid.Text, ddlcategory.SelectedItem.Text, sGetID, "tblAuditMaster_" + sTableName, lblUser.Text, "tblStock_" + sTableName);
                        //DataSet dss = objBs1.CheckStock(ddlcategory.SelectedValue, sGetID, "tblStock_" + sTableName);
                        //if (dss.Tables[0].Rows.Count > 0)
                        //{
                        //    string sID = dss.Tables[0].Rows[0]["SubCategoryID"].ToString();
                        //    string sStock = dss.Tables[0].Rows[0]["Available_QTY"].ToString();
                        //    int iTotal = Convert.ToInt32(sStock) + Convert.ToInt32(txtopeningstock.Text);

                        //    int iupdate23 = objBs1.UpdateStock1(sID, iTotal.ToString(), "tblStock_" + sTableName);
                        //}
                    }
                    else
                    {
                        // lblerror.Text = "These Definition has already Excided please enter a new one";
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Definition has already Exists please enter a new one')", true);
                        return;


                    }
                   
                    
                }
                string Mode = Request.QueryString.Get("Mode");
                if (Mode == "Purchase")
                {
                    string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
                    base.Response.Write(close);
                    
                }
                else
                {
                    Response.Redirect("../Accountsbootstrap/Descriptiongrid.aspx");
                }

                string Sales = Request.QueryString.Get("Mode");
                if (Sales == "Sales")
                {
                    string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
                    base.Response.Write(close);
                    //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Close", "window.close()", true);
                }
                else
                {
                    Response.Redirect("../Accountsbootstrap/Descriptiongrid.aspx");
                }
            }

            else
            {
                lblerror.Text = "Please Enter Description";
            }
        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            
          
                Response.Redirect("DescriptionGrid.aspx");
            
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("DescriptionGrid.aspx");
        }

        protected void newvendor_Click(object sender, EventArgs e)
        {
            string url = "Customermaster.aspx";
            string s = "window.open('" + url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
           
        }

        protected void btnFormat_Click(object sender, EventArgs e)
        {
            bindData();

        }

        public void bindData()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Category"));
            dt.Columns.Add(new DataColumn("Product ID"));
            dt.Columns.Add(new DataColumn("Product description"));
            dt.Columns.Add(new DataColumn("Meter"));
            dt.Columns.Add(new DataColumn("Tax"));

            DataRow dr_final12 = dt.NewRow();
            dr_final12["Product ID"] = "";
            dr_final12["Product description"] = "";
            dr_final12["Meter"] = "";
            dr_final12["Category"] = "";
            dr_final12["Tax"] = "";
            dt.Rows.Add(dr_final12);

            ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                //string filename = "Sales Report.xls";
                string filename = "NewProduct _" + DateTime.Now.ToString() + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                //dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                //dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                //dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
                dgGrid.HeaderStyle.Font.Bold = true;
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "";
                string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=" + "\"";
                char[] specialCharactersArray = specialCharacters.ToCharArray();

                if (FileUpload1.HasFile)
                {
                    string datett = DateTime.Now.ToString();
                    string dtaa = Convert.ToDateTime(datett).ToString("dd-MM-yyyy-hh-mm-ss");
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName) + dtaa;
                    string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string fileLocation = Server.MapPath("~/App_Data/" + fileName);
                    FileUpload1.SaveAs(fileLocation);
                    if (fileExtension == ".xls")
                    {
                        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (fileExtension == ".xlsx")
                    {
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                        //OleDbConnection Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Attach Correct Format file Extension.(.xls or .xlsx)');", true);
                        return;
                    }
                    OleDbConnection con = new OleDbConnection(connectionString);
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                    DataTable dtExcelRecords = new DataTable();
                    con.Open();
                    DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                    cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                    dAdapter.SelectCommand = cmd;
                    dAdapter.Fill(dtExcelRecords);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtExcelRecords);

                    if (ds == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Uploading Excel is Empty');", true);
                        return;
                    }

                    // Check Empty

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Product ID"]) == null) || (Convert.ToString(dr["Product ID"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product ID is empty');", true);
                            return;
                        }
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Product description"]) == null) || (Convert.ToString(dr["Product description"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Description is empty');", true);
                            return;
                        }
                    }


                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    if ((Convert.ToString(dr["Meter"]) == null) || (Convert.ToString(dr["Brand"]) == ""))
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Brand Name is empty');", true);
                    //        return;
                    //    }
                    //}

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Category"]) == null) || (Convert.ToString(dr["Category"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Category Name is empty');", true);
                            return;
                        }
                    }

                    //check duplicate

                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    string category = Convert.ToString(dr["Category"]);

                    //    if (objBs.CheckIfCategory(category))
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Category " + category + " has already Exists. please enter a new one')", true);
                    //        return;
                    //        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    //    }

                    //}

                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    string brand = Convert.ToString(dr["Brand"]);

                    //    if (objBs.CheckIfbrand(brand))
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Brand " + brand + " has already Exists. please enter a new one')", true);
                    //        return;
                    //        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    //    }

                    //}

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string product = Convert.ToString(dr["Product ID"]);

                        if (objBs.CheckIfproduct(product,sTableName))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Product " + product + " has already Exists. please enter a new one')", true);
                            return;
                            // lblerror.Text = "These Category has already Exists. please enter a new one";

                        }

                    }


                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        if ((Convert.ToString(dr["Category"]) != null) || (Convert.ToString(dr["Category"]) != ""))
                        {
                            int index = Convert.ToString(dr["Category"]).IndexOfAny(specialCharactersArray);
                            //index == -1 no special characters
                            if (index != -1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Special characters not allowed in Category');", true);
                                return;
                            }
                        }
                    }

                    //already exists in excel sheet

                    int i = 1;
                    int ii = 1;
                    string cat = string.Empty;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        cat = Convert.ToString(dr["Product ID"]);

                        if ((cat == null) || (cat == ""))
                        {
                        }
                        else
                        {
                            foreach (DataRow drd in ds.Tables[0].Rows)
                            {
                                if (ii == i)
                                {
                                }
                                else
                                {
                                    if (cat == Convert.ToString(drd["Product ID"]))
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product ID with  - " + cat + " - already exists in the excel.');", true);
                                        return;
                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                        i = i + 1;
                        ii = 1;
                    }

                    //int ij = 1;
                    //int iij = 1;
                    //string catj = string.Empty;
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    catj = Convert.ToString(dr["Category"]);

                    //    if ((catj == null) || (catj == ""))
                    //    {
                    //    }
                    //    else
                    //    {
                    //        foreach (DataRow drd in ds.Tables[0].Rows)
                    //        {
                    //            if (iij == ij)
                    //            {
                    //            }
                    //            else
                    //            {
                    //                if (catj == Convert.ToString(drd["Category"]))
                    //                {
                    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Category with  - " + catj + " - already exists in the excel.');", true);
                    //                    return;
                    //                }
                    //            }
                    //            iij = iij + 1;
                    //        }
                    //    }
                    //    ij = ij + 1;
                    //    iij = 1;
                    //}

                    //int ik = 1;
                    //int iik = 1;
                    //string catk = string.Empty;
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    catk = Convert.ToString(dr["Brand"]);

                    //    if ((catk == null) || (catk == ""))
                    //    {
                    //    }
                    //    else
                    //    {
                    //        foreach (DataRow drd in ds.Tables[0].Rows)
                    //        {
                    //            if (iik == i)
                    //            {
                    //            }
                    //            else
                    //            {
                    //                if (catk == Convert.ToString(drd["Brand"]))
                    //                {
                    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Brand with  - " + catk + " - already exists in the excel.');", true);
                    //                    return;
                    //                }
                    //            }
                    //            iik = iik + 1;
                    //        }
                    //    }
                    //    ik = ik + 1;
                    //    iik = 1;
                    //}

                    //Given Category exists in category master
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string category = Convert.ToString(dr["category"]);
                        if ((Convert.ToString(dr["category"]) == null) || (Convert.ToString(dr["category"]) == ""))
                        {

                        }
                        else
                        {
                            if (!objBs.CheckIfCategory(category, sTableName))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('category with - " + category + " - does not exists in the Category Master');", true);
                                return;
                            }
                        }
                    }

                    //Given meter exists in meter table
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    string category = Convert.ToString(dr["Meter"]);
                    //    if ((Convert.ToString(dr["Meter"]) == null) || (Convert.ToString(dr["Meter"]) == ""))
                    //    {

                    //    }
                    //    else
                    //    {
                    //        if (!objBs.CheckIfmeter(category))
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Meter with - " + category + " - does not exists in the Category Master');", true);
                    //            return;
                    //        }
                    //    }
                    //}


                    // Given brand exists in brand master
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    string brand = Convert.ToString(dr["meter"]);
                    //    if ((Convert.ToString(dr["meter"]) == null) || (Convert.ToString(dr["meter"]) == ""))
                    //    {

                    //    }
                    //    else
                    //    {
                    //        if (!objBs.CheckIfbrand(brand))
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Meter with - " + brand + " - does not exists in the Brand Master');", true);
                    //            return;
                    //        }
                    //    }
                    //}




                    objBs.InsertBulkProducts(ds, lblUserID.Text, sTableName, "tblStock_" + sTableName);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Product Uploaded Successfully');", true);



                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please upload Xls file.It Cannot be empty.');", true);

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        } 
       

        
    }
       
}