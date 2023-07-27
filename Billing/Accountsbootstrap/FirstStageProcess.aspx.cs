using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace Billing.Accountsbootstrap
{
    public partial class FirstStageProcess : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iDealer = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            //btnadd.Attributes.Add("onclick", "return valchk();"); // Validation Check lines
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {

                string name = Request.QueryString.Get("name");
                if (name == "Addnew")
                {
                    add.Visible = true;
                    bulk.Visible = false;
                }
                else
                {
                    add.Visible = false;
                    bulk.Visible = true;
                }
                //   txtbillno.Enabled = true;

                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                //DataSet ds = objBs.SalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text);
                //   DataSet ds = objBs.getSalesBillno(Convert.ToInt32(ddlPayMode.SelectedValue), btnadd.Text, "Sales");
                DataSet ds = new DataSet();
                ds = objBs.getmaaxBillno();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    FirstGridViewRow();
                 
                }

               
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
            
        }


        protected void gridbutton_click(object sender, EventArgs e)
        {
          //  Response.Redirect("salesgrid.aspx");
        }

        //protected void Refbutton_click(object sender, EventArgs e)
        //{
        //    string url = "http://www.bigdbiz.com";
        //    string s = "window.open('" + url + "', 'popup_window', 'width=900,height=500,resizable=yes');";
        //    ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        //}

        protected void newcstomer_click(object sender, EventArgs e)
        {
           // Response.Redirect("customermaster.aspx");
        }

        protected void TextBox9_TextChanged(object sender, EventArgs e)
        {
        }
        protected void bblbillto_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet dsCustomer = objBs.GetCustName(Convert.ToInt32(bblbillto.SelectedValue), sTableName);
            //if (bblbillto.SelectedValue == "1")
            //{
            //    if (chknew.Checked == true)
            //    {
            //        txtCustname.Visible = true;
            //        ddlcustomerID.Visible = false;
            //        txtcustomername.Text = "";
            //    }
            //    else
            //    {
            //        txtCustname.Visible = false;
            //        ddlcustomerID.Visible = true;
            //        txtcustomername.Text = "";
            //    }
            //    txtaddress.Text = "";
            //    txtcity.Text = "";
            //    //txtarea.Text = "";
            //    txtpincode.Text = "";
            //    txtcuscode.Text = "";
            //    ////advance.Visible = true;
            //    //tax.Visible = false;
            //}
            //else
            //{
            //    if (chknew.Checked == true)
            //    {
            //        txtCustname.Visible = true;
            //        ddlcustomerID.Visible = false;
            //        txtcustomername.Text = "";
            //    }
            //    else
            //    {
            //        txtCustname.Visible = false;
            //        ddlcustomerID.Visible = true;
            //        txtcustomername.Text = "";
            //    }
            //    //advance.Visible = false;

            //    if (dsCustomer.Tables[0].Rows.Count > 0)
            //    {
            //        ddlcustomerID.DataSource = dsCustomer.Tables[0];
            //        ddlcustomerID.DataTextField = "CustomerName";
            //        ddlcustomerID.DataValueField = "CustomerID";
            //        ddlcustomerID.DataBind();
            //        ddlcustomerID.Items.Insert(0, "Select Dealer");

            //        // ddlcustomerID.Text = dsCustomer.Tables[0].Rows[0]["CustomerID"].ToString();

            //    }
            //}
        }
     
        protected void gvcustomerorderchanged(object sender, EventArgs e)
        {
            //Get the selected row
            GridViewRow row = gvcustomerorder.SelectedRow;
            if (row != null)
            {
                //First find the control in template column and then get the value
                //Change the cell index(1) of column as per your design
                // Label2.Text = (row.FindControl("lblLocalTime") as Label).Text;
                //  DropDownList drop = (row.FindControl("lblLocalTime") as DropDownList).Text;
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataSet ds = new DataSet();
            //ds = objBs.categorymaster(sTableName);

            //DataSet dst = new DataSet();
            //dst = objBs.selectcategoryalldecriptionbranch(sTableName);

            DataSet dswidth = objBs.GetWidth();

            DataSet dsup = objBs.getnewsupplierforfab();




            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              
                TextBox txtdesign = (TextBox)e.Row.FindControl("txtdesno");
                TextBox txtcolor = (TextBox)e.Row.FindControl("txtcolor");
                TextBox txtmeter = (TextBox)e.Row.FindControl("txtmeter");
                TextBox txtwsp = (TextBox)e.Row.FindControl("txtWSP");
                TextBox txtmrp = (TextBox)e.Row.FindControl("txtMRP");
                
                DropDownList drpsupp = (DropDownList)e.Row.FindControl("drpsupplier");

              
                txtdesign.Text = "";
                txtmeter.Text = "0";
                txtcolor.Text = "";
                txtwsp.Text = "0";
                txtmrp.Text = "0";

              

                var ddl1 = (DropDownList)e.Row.FindControl("drpsupplier");
                ddl1.DataSource = dsup;
                ddl1.DataTextField = "LedgerName";
                ddl1.DataValueField = "Ledgerid";
                ddl1.DataBind();
                ddl1.Items.Insert(0, "Select Supplier");

            }

        }


        protected void ButtonAdd2_Click1(object sender, EventArgs e)
        {
            int No = 0;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                DropDownList drpitem = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpitem");
                DropDownList drpsupplier = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpsupplier");
                DropDownList drporder = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpord");

                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtwsp = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtWSP");
                TextBox txtmrp = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtMRP");

                if (txtdesign.Text == "" && txtcolor.Text == "")
                {
                    No = 0;
                    break;
                }
                else
                {
                    No = 1;
                }
            }

            if (No == 1)
            {

                AddNewRow();
            }
            else
            {

            }
           

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
               
                TextBox txtcolor1 = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                if (vLoop == gvcustomerorder.Rows.Count - 1)
                {
                    DropDownList drpitem = (DropDownList)gvcustomerorder.Rows[vLoop-1].FindControl("drpitem");
                    DropDownList drpsupplier = (DropDownList)gvcustomerorder.Rows[vLoop-1].FindControl("drpsupplier");
                    DropDownList drporder = (DropDownList)gvcustomerorder.Rows[vLoop - 1].FindControl("drpord");
                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtdesno");
                    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtcolor");
                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtmeter");
                    TextBox txtwsp = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtWSP");
                    TextBox txtmrp = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtMRP");

                    DropDownList odrpitem = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpitem");
                    DropDownList odrpsupplier = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpsupplier");
                    DropDownList odrporder = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpord");

                    TextBox otxtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                    TextBox otxtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                    TextBox otxtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                    TextBox otxtwsp = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtWSP");
                    TextBox otxtmrp = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtMRP");

                    odrpitem.SelectedValue = drpitem.SelectedValue;
                    odrpsupplier.SelectedValue = drpsupplier.SelectedValue;
                    otxtdesign.Text = txtdesign.Text;

                    
                    Regex r = new Regex("a|A|B|b|C|c|D|d|E|e|F|f|G|g|H|h|I|i|J|j|K|k|L|l|M|m|N|n|O|o|P|p|Q|q|R|r|S|s|T|t|U|u|V|v|W|w|X|x|Y|y|Z|z");
                    bool containsAny = r.IsMatch(txtcolor.Text);
                    if (containsAny == true)
                    {
                        otxtcolor.Text = "";
                    }
                    else
                    {

                        int iCol = Convert.ToInt32(txtcolor.Text) + 1;
                        otxtcolor.Text = Convert.ToString(iCol);
                    }

                    otxtmeter.Text = txtmeter.Text;
                    otxtwsp.Text = txtwsp.Text;
                    otxtmrp.Text = txtmrp.Text;
                 
                }

                txtcolor1.Focus();




            }
          

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtcolor1 = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");

                txtcolor1.Focus();
            }



        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("FirstStageGrid.aspx");
        }
        protected void Async_Upload_File(object sender, EventArgs e)
        {
        }

        protected void Upload_File(object sender, EventArgs e)
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
                  //  ds.Tables[0].Select("Item", null);

                   // return;
                    if (ds == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Uploading Excel is Empty');", true);
                        return;
                    }


                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Item"]) == null) || (Convert.ToString(dr["Item"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Item Name is empty');", true);
                            return;
                        }
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Supplier"]) == null) || (Convert.ToString(dr["Supplier"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Supplier Name is empty');", true);
                            return;
                        }
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Designno"]) == null) || (Convert.ToString(dr["Designno"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Design No Name is empty');", true);
                            return;
                        }
                    }


                   
                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{

                    //    if ((Convert.ToString(dr["Category"]) != null) || (Convert.ToString(dr["Category"]) != ""))
                    //    {
                    //        int index = Convert.ToString(dr["Category"]).IndexOfAny(specialCharactersArray);
                    //        //index == -1 no special characters
                    //        if (index != -1)
                    //        {
                    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Special characters not allowed in Category');", true);
                    //            return;
                    //        }
                    //    }
                    //}

                   

                    int iSuccess = 0;
                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                    {
                        string Customername = ds.Tables[0].Rows[j]["Supplier"].ToString();
                        string colour = ds.Tables[0].Rows[j]["Color"].ToString();
                        string Item = ds.Tables[0].Rows[j]["Item"].ToString();
                        string Design = ds.Tables[0].Rows[j]["Designno"].ToString();
                        string Order = ds.Tables[0].Rows[j]["Order"].ToString();
                        string Mtrrate = ds.Tables[0].Rows[j]["Mtrrate"].ToString();
                        string WSP = ds.Tables[0].Rows[j]["WSP"].ToString();
                        string MRP = ds.Tables[0].Rows[j]["MRP"].ToString();

                        int ledgerid = objBs.ExelCustcheck(Customername);

                        if (colour.Contains(","))
                        {
                            string[] split = colour.Split(',');

                            for (int L = 0; L < split.Length; L++)
                            {
                                int status = objBs.inserfirststage(Item, ledgerid.ToString(), Design, split[L], Order, Mtrrate, WSP, MRP);
                            }
                        }
                        else
                        {
                            int status = objBs.inserfirststage(Item, ledgerid.ToString(), Design, colour, Order, Mtrrate, WSP, MRP);

                        }

                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Category Uploaded Successfully');", true);

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

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int No = 0;
         
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                DropDownList drpitem = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpitem");
                DropDownList drpsupplier = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpsupplier");
                DropDownList drporder = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpord");

                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtwsp = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtWSP");
                TextBox txtmrp = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtMRP");
              
                

                if (txtdesign.Text == "" && txtcolor.Text == "")
                {
                    No = 0;
                    break;
                }
                else
                {
                    No = 1;
                }
            }

            if (No == 1)
            {

                AddNewRow();
            }
            else
            {

            }

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList drpitem = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpitem");
               

                drpitem.Focus();
            }

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{

            //    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
            //    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
            //    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
            //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");


            //    if (txtdesign.Text != "" || txtcolor.Text != "")
            //    {
            //    }
            //    else
            //    {
            //        txtmeter.Text = "0";
            //        txtrate.Text = "0";
            //    }
            //}
        }
       

        private void AddNewRow()
        {
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList drpitem = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpitem");
                DropDownList drpsupplier = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpsupplier");
                DropDownList drporder = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpord");

                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtwsp = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtWSP");
                TextBox txtmrp = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtMRP");

                int col = vLoop + 1;

                if (txtdesign.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter Design code in row " + col + " ')", true);
                    txtdesign.Focus();
                    return;
                }
                if (txtcolor.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter Colour/Shade code in row " + col + " ')", true);
                    txtcolor.Focus();
                    return;
                }
                if (drpsupplier.SelectedValue == "Select Supplier")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Supplier  in row " + col + " ')", true);
                    drpsupplier.Focus();
                    return;
                }
                if (txtwsp.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter WSP in row " + col + " ')", true);
                    txtwsp.Focus();
                    return;
                }
                if (txtmrp.Text == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter MRP in row " + col + " ')", true);
                    txtmrp.Focus();
                    return;
                }

              
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

                        DropDownList drpitem =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpitem");

                        DropDownList drpsuplier =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpsupplier");
                   
                        TextBox txtdesign =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtdesno");

                        TextBox txtcolor =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtcolor");

                        DropDownList drporder =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpord");

                        TextBox txtmeter =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtmeter");

                        TextBox txtwsp =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtWSP");

                        TextBox txtmrp =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtMRP");
                      

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Item"] = drpitem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Supplier"] = drpsuplier.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Design"] = txtdesign.Text;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
                        dtCurrentTable.Rows[i - 1]["Meter"] = txtmeter.Text;
                        dtCurrentTable.Rows[i - 1]["Order"] = drporder.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["WSP"] = txtwsp.Text;
                        dtCurrentTable.Rows[i - 1]["MRP"] = txtmrp.Text;
                      

                        rowIndex++;


                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    gvcustomerorder.DataSource = dtCurrentTable;
                    gvcustomerorder.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList drpitem =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpitem");

                        DropDownList drpsuplier =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpsupplier");

                        TextBox txtdesign =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtdesno");

                        TextBox txtcolor =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtcolor");

                        DropDownList drporder =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpord");

                        TextBox txtmeter =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtmeter");

                        TextBox txtwsp =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtWSP");

                        TextBox txtmrp =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtMRP");



                        drpitem.SelectedValue = dt.Rows[i]["Item"].ToString();
                        drpsuplier.SelectedValue = dt.Rows[i]["Supplier"].ToString();
                        drporder.SelectedValue = dt.Rows[i]["Order"].ToString();
                        txtdesign.Text = dt.Rows[i]["design"].ToString();
                        txtcolor.Text = dt.Rows[i]["Color"].ToString();

                        txtmeter.Text = dt.Rows[i]["meter"].ToString();
                        txtwsp.Text = dt.Rows[i]["WSP"].ToString();
                        txtmrp.Text = dt.Rows[i]["MRP"].ToString();

                    
                      


                        rowIndex++;

                    }
                }
            }
        }


        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("Item", typeof(string)));
            dtt.Columns.Add(new DataColumn("Supplier", typeof(string)));
            dtt.Columns.Add(new DataColumn("Design", typeof(string)));
            dtt.Columns.Add(new DataColumn("Color", typeof(string)));
            dtt.Columns.Add(new DataColumn("Order", typeof(string)));
            dtt.Columns.Add(new DataColumn("Meter", typeof(string)));
            dtt.Columns.Add(new DataColumn("WSP", typeof(string)));
            dtt.Columns.Add(new DataColumn("MRP", typeof(string)));
          
            //dtt.Columns.Add(new DataColumn("Qty", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Discount", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Tax", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dr = dtt.NewRow();
            dr["Item"] = string.Empty;
            dr["Supplier"] = string.Empty;
            dr["Design"] = string.Empty;
            dr["Color"] = string.Empty;
            dr["Meter"] = string.Empty;
            dr["Order"] = string.Empty;
            dr["WSP"] = string.Empty;
            dr["MRP"] = string.Empty;
            
            //dr["Qty"] = string.Empty;
            //dr["Rate"] = string.Empty;
            //dr["Discount"] = string.Empty;
            //dr["Tax"] = string.Empty;
            //dr["Amount"] = string.Empty;
            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            gvcustomerorder.DataSource = dtt;
            gvcustomerorder.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("Item");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Supplier");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Design");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Meter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Order");
            dttt.Columns.Add(dct);

            dct = new DataColumn("WSP");
            dttt.Columns.Add(dct);

            dct = new DataColumn("MRP");
            dttt.Columns.Add(dct);

            //dct = new DataColumn("Qty");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Rate");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Discount");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Tax");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Amount");
            //dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["Item"] = "";
            drNew["Supplier"] = "";
            drNew["Design"] = "";
            drNew["Color"] = "";
            drNew["Meter"] = 0;
            drNew["Order"] = "";
            drNew["WSP"] = 0;
            drNew["MRP"] = 0;
            
            //drNew["ProductCode"] = "";
            //drNew["Product"] = "";
            //drNew["refno"] = 0;
            //drNew["cerno"] = 0;
            //drNew["Discount"] = 0;
            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();


            //TextBox txn = (TextBox)grvStudentDetails.Rows[0].Cells[1].FindControl("txtName");
            //txn.Focus();
            //Button btnAdd = (Button)grvStudentDetails.FooterRow.Cells[5].FindControl("ButtonAdd");
            //Page.Form.DefaultFocus = btnAdd.ClientID;

        }



        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();

            if (ViewState["CurrentTable1"] != null)
            {
                DataSet ds = new DataSet();
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {

                    ds.Merge(dt);


                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();

                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();

                    SetPreviousData();

                    //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    //{
                    //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                    //    txtno.Text = Convert.ToString(i + 1);
                    //}

                  
                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();

                    SetPreviousData();

                
                    FirstGridViewRow();
                }
            }

        }
        private void SetRowData()
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

                        DropDownList drpitem =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpitem");

                        DropDownList drpsuplier =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpsupplier");

                        TextBox txtdesign =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtdesno");

                        TextBox txtcolor =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtcolor");

                        DropDownList drporder =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpord");

                        TextBox txtmeter =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtmeter");

                        TextBox txtwsp =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtWSP");

                        TextBox txtmrp =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtMRP");

                        drCurrentRow = dtCurrentTable.NewRow();
                        
                        dtCurrentTable.Rows[i - 1]["Design"] = txtdesign.Text;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
                        dtCurrentTable.Rows[i - 1]["meter"] = txtmeter.Text;
                        dtCurrentTable.Rows[i - 1]["WSP"] = txtwsp.Text;
                        dtCurrentTable.Rows[i - 1]["MRP"] = txtmrp.Text;
                        dtCurrentTable.Rows[i - 1]["Item"] = drpitem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Supplier"] = drpsuplier.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Order"] = drporder.SelectedValue;


                        //dtCurrentTable.Rows[i - 1]["Qty"] = txtQty.Text;
                        //dtCurrentTable.Rows[i - 1]["Orderno"] = txttno.Text;

                        //dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;

                        //dtCurrentTable.Rows[i - 1]["Tax"] = txtTax.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    gvcustomerorder.DataSource = dtCurrentTable;
                    gvcustomerorder.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        protected void gvcustomerorder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataSet dsCategory = new DataSet();

            //DataSet dscat = new DataSet();

            //string OrderNo = Request.QueryString.Get("OrderNo");
            ////if (OrderNo != "")
            ////{
            ////    /// dsCategory = objBs.GetCAT_OrderForm();
            ////}
            ////else
            //dsCategory = objBs.selectcategorybrandcat(sTableName);

            //dscat = objBs.selectcatuser();



            ////else
            ////    dsCategory = objBs.selectcategorymaster_Dealer("tblStock_" + sTableName);


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    DropDownList ddlCategory1 = (DropDownList)(e.Row.FindControl("ddlCategory1") as DropDownList);
            //    ddlCategory1.Focus();
            //    ddlCategory1.Enabled = true;
            //    ddlCategory1.DataSource = dsCategory.Tables[0];
            //    ddlCategory1.DataTextField = "productname";
            //    ddlCategory1.DataValueField = "CategoryUserID";
            //    ddlCategory1.DataBind();
            //    ddlCategory1.Items.Insert(0, "Select");

            //    DropDownList ddlDef1 = (DropDownList)(e.Row.FindControl("ddlDef1") as DropDownList);
            //    ddlDef1.Focus();
            //    ddlDef1.Enabled = true;
            //    ddlDef1.DataSource = dscat.Tables[0];
            //    ddlDef1.DataTextField = "Definition";
            //    ddlDef1.DataValueField = "categoryuserid";
            //    ddlDef1.DataBind();
            //    ddlDef1.Items.Insert(0, "Select Product");

            //    //DataSet dsCategory1 = objBs.selectcategorydecription(Convert.ToInt32(ddlCategory1.SelectedItem.Text));
            //    //if (dsCategory1.Tables[0].Rows.Count > 0)
            //    //{
            //    //    //DropDownList Def1 = (DropDownList)row.FindControl("ddlDef1");
            //    //    ////Label lblCatID = (Label)row.FindControl("catid");
            //    //    ////lblCatID.Text = dsCategory.Tables[0].Rows[0]["CategoryID"].ToString();

            //    //    ddlDef1.DataSource = dsCategory.Tables[0];
            //    //    ddlDef1.DataTextField = "Definition";
            //    //    ddlDef1.DataValueField = "categoryuserid";
            //    //    ddlDef1.DataBind();
            //    //    ddlDef1.Items.Insert(0, "Select Product");
            //    //    ddlDef1.Focus();
            //    //}

            //    //DataSet dDef = objBs.selectcategorydecription(Convert.ToInt32(ddlCategory1.SelectedValue));
            //    //DropDownList Def = (DropDownList)e.Row.FindControl("ddlDef1");

            //    //Def.DataSource = dDef.Tables[0];
            //    //Def.DataTextField = "Definition";
            //    //Def.DataValueField = "categoryuserid";
            //    //Def.DataBind();
            //    //#region Databind
            //    //string billno = Convert.ToString(Request.QueryString["iSalesID"]);

            //    //if (billno != null)
            //    //{



            //    //    DataSet dBilling = objBs.GetSalesnew("tblSales_" + sTableName, billno);
            //    //    if (dBilling.Tables[0].Rows.Count > 0)
            //    //    {



            //    //        //txtcustomername.Text = dBilling.Tables[0].Rows[0]["CustomerName"].ToString();
            //    //       // txtmobileno.Text = dBilling.Tables[0].Rows[0]["PhoneNo"].ToString();
            //    //        txtSubTotal.Text = dBilling.Tables[0].Rows[0]["Total"].ToString();
            //    //       // txtAgainstAmount.Text = dBilling.Tables[0].Rows[0]["Advance"].ToString();
            //    //        ddlbook.Text = dBilling.Tables[0].Rows[0]["Book"].ToString();
            //    //       // txttotal.Text = dBilling.Tables[0].Rows[0]["Balance"].ToString();
            //    //        int iCount = dBilling.Tables[0].Rows.Count;

            //    //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //    //        DataRow drCurrentRow = null;
            //    //        DataSet dBilling1 = objBs.GettransnewSales("tblTransSales_" + sTableName, billno);
            //    //        for (int i = 0; i < iCount; i++)
            //    //        {

            //    //            TextBox txtRate = (TextBox)e.Row.FindControl("txtRate");
            //    //            TextBox txtQty = (TextBox)e.Row.FindControl("txtQty");
            //    //            TextBox txtAmt = (TextBox)e.Row.FindControl("txtAmount");
            //    //            DropDownList ddlCat = (DropDownList)e.Row.FindControl("ddlCategory");
            //    //           // DropDownList ddlDef = (DropDownList)e.Row.FindControl("ddlDef");

            //    //            ddlCat.SelectedValue = dBilling1.Tables[0].Rows[i]["SubCategoryID"].ToString();


            //    //          //  ddlDef.SelectedValue = dBilling.Tables[0].Rows[i]["SubCategoryID"].ToString();


            //    //            txtQty.Text = dBilling1.Tables[0].Rows[i]["Qty"].ToString();


            //    //            txtRate.Text = dBilling1.Tables[0].Rows[i]["Rate"].ToString();


            //    //            txtAmt.Text = dBilling1.Tables[0].Rows[i]["Amount"].ToString();


            //    //            if (dtCurrentTable.Rows.Count > 0)
            //    //            {
            //    //                for (int j = 1; j <= dtCurrentTable.Rows.Count; j++)
            //    //                {

            //    //                    drCurrentRow = dtCurrentTable.NewRow();
            //    //                    drCurrentRow["sno"] = j + 1;

            //    //                    dtCurrentTable.Rows[j - 1]["SubCategoryID"] = ddlCategory1.Text;
            //    //                   // dtCurrentTable.Rows[j - 1]["Item"] = ddlDef.Text;
            //    //                    dtCurrentTable.Rows[j - 1]["Qty"] = txtQty.Text;
            //    //                    dtCurrentTable.Rows[j - 1]["Rate"] = txtRate.Text;
            //    //                    dtCurrentTable.Rows[j - 1]["Amount"] = txtAmt.Text;


            //    //                }
            //    //                dtCurrentTable.Rows.Add(drCurrentRow);
            //    //                ViewState["CurrentTable"] = dtCurrentTable;

            //    //                gvcustomerorder.DataSource = dtCurrentTable;
            //    //                gvcustomerorder.DataBind();
            //    //            }

            //    //        }
            //    //    }

            //    //}

            //}
        }


       

        protected void gvcustomerorder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     
 

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("FirstStageGrid.aspx");
        }

        protected void Gridview1_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            // GridViewRow row = gvcustomerorder.SelectedRow;


            //if (e.CommandName == "Select")
            //{

            //    GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;

            //    DropDownList yourTextbox = row.FindControl("drpitem") as DropDownList;
            //    var yourValue = yourTextbox.Text;
            //    if (yourValue == "Select Product")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Product.')", true);
            //        return;

            //    }
            //    string value = yourTextbox.SelectedValue;
            //    DataSet ds = objBs.itemhistorypopup(sTableName, value);
            //    if (ds != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            GridView1.DataSource = ds;
            //            GridView1.DataBind();
            //        }
            //        else
            //        {
            //            GridView1.DataSource = null;
            //            GridView1.DataBind();
            //        }
            //        mpe.Show();
            //    }
            //}
            //else if (e.CommandName == "Select1")
            //{
            //    GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            //    DropDownList yourTextbox = row.FindControl("drpitem") as DropDownList;

            //    var yourValue = yourTextbox.Text;

            //    if (yourValue == "Select Product")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Product.')", true);
            //        return;

            //    }
            //    if (ddlcustomerID.SelectedValue == "Select Customer")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Customer Name')", true);
            //        return;

            //    }

            //    string value1 = yourTextbox.SelectedValue;

            //    string cust = ddlcustomerID.SelectedValue;
            //    DataSet ds = objBs.custhistorypopup(sTableName, value1, cust);
            //    if (ds != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            GridView1.DataSource = ds;
            //            GridView1.DataBind();
            //        }
            //        else
            //        {
            //            GridView1.DataSource = null;
            //            GridView1.DataBind();
            //        }
            //        mpe.Show();
            //    }
            //}
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;

         
            if (btnadd.Text == "Save")
            {
                //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                //{
                //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                //    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                //    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                //    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                //    Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                //    Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");

                //    DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");



                //    int col = vLoop + 1;

                //    if (drpwid.SelectedValue == "Select Width")
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Width in " + col + " in this row');", true);
                //        //  txt1.Focus();
                //        return;
                //    }


                //    txtno.Focus();

                //    itemc = txtdesign.Text;
                //    itemcd = txtcolor.Text;


                //    if ((itemc == null) || (itemc == ""))
                //    {
                //    }
                //    else
                //    {
                //        for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                //        {
                //            //  DropDownList txt1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("ProductCode");
                //            TextBox txtdesign1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtdesno");
                //            TextBox txtcolor1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtcolor");
                //            if (txtdesign1.Text == "")
                //            {
                //            }
                //            else
                //            {

                //                if (ii == iq)
                //                {
                //                }
                //                else
                //                {
                //                    if (itemc == txtdesign1.Text && itemcd == txtcolor1.Text)
                //                    {
                //                        itemc = txtdesign.Text;
                //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemc + "  already exists in the Grid.');", true);
                //                        //  txt1.Focus();
                //                        return;

                //                    }
                //                }
                //                ii = ii + 1;
                //            }
                //        }
                //    }
                //    iq = iq + 1;
                //    ii = 1;

                //}

                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drpitem = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpitem");
                    DropDownList drpsupplier = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpsupplier");
                    DropDownList drporder = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpord");

                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                    TextBox txtwsp = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtWSP");
                    TextBox txtmrp = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtMRP");

                    int col = vLoop + 1;

                    if (txtdesign.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter Design code in row " + col + " ')", true);
                        txtdesign.Focus();
                        return;
                    }
                    if (txtcolor.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter Colour/Shade code in row " + col + " ')", true);
                        txtcolor.Focus();
                        return;
                    }
                    if (drpsupplier.SelectedValue == "Select Supplier")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Supplier  in row " + col + " ')", true);
                        drpsupplier.Focus();
                        return;
                    }
                    if (txtwsp.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter WSP in row " + col + " ')", true);
                        txtwsp.Focus();
                        return;
                    }
                    if (txtmrp.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter MRP in row " + col + " ')", true);
                        txtmrp.Focus();
                        return;
                    }


                }

                //DateTime regdate = DateTime.ParseExact(txtregdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime invdate = DateTime.ParseExact(txtinvdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                //int iStatus23 = objBs.insertfab(txtinvno.Text, drpsupplier.SelectedValue, regdate, drpchecked.SelectedValue, txtinrefno.Text, invdate, txttotmet.Text, "0", txtDelChalan.Text, txttoal.Text);

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpitem =
                     (DropDownList)gvcustomerorder.Rows[i].FindControl("drpitem");

                    DropDownList drpsuplier =
                   (DropDownList)gvcustomerorder.Rows[i].FindControl("drpsupplier");

                    TextBox txtdesign =
                      (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesno");

                    TextBox txtcolor =
                  (TextBox)gvcustomerorder.Rows[i].FindControl("txtcolor");

                    DropDownList drporder =
                  (DropDownList)gvcustomerorder.Rows[i].FindControl("drpord");

                    TextBox txtmeter =
                   (TextBox)gvcustomerorder.Rows[i].FindControl("txtmeter");

                    TextBox txtwsp =
                      (TextBox)gvcustomerorder.Rows[i].FindControl("txtWSP");

                    TextBox txtmrp =
                        (TextBox)gvcustomerorder.Rows[i].FindControl("txtMRP");



                    int status = objBs.inserfirststage(drpitem.SelectedValue, drpsuplier.SelectedValue, txtdesign.Text, txtcolor.Text, drporder.SelectedValue, txtmeter.Text, txtwsp.Text, txtmrp.Text);

                    //Label txtno = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");
                    //Label orderno = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                    //string orderno = gvcustomerorder.Rows[i].Cells[0].Text;
                    //TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                    //TextBox txtdesign = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesno");


                    //TextBox txtcolor = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcolor");


                    //TextBox txtmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtmeter");


                    //TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                    //Label imgpath = (Label)gvcustomerorder.Rows[i].FindControl("imgpreview");

                    //DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpwid");






                //    int iStatus2 = objBs.insertTransfab(txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue);

                    // iStockSuccess = UpdateStockAvailable(Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty));


                }

                Response.Redirect("Firststagegrid.aspx");
                //{

                //    dss = objBs.checkfortempSalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text, txtbillno.Text, "");
                //}
                //else
                //{
                //    dss = objBs.checkfortempSalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text, txtbillno.Text, iSalesIDnew);
                //}
                //if (dss.Tables[0].Rows.Count > 0)
                //{
                //    txtbillno.Focus();
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Billno Number Already Exists.');", true);
                //    return;
                //}
                //else
                //{
                //    //txtvoudate.Focus();
                //}

                //string custid = string.Empty;
                //if (chknewcust.Checked == false)
                //{
                //    if (ddlcustomerID.SelectedValue == "Select Customer")
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Please Select Customer Name')", true);

                //        return;

                //    }
                //}
                //else
                //{
                //    DataSet ds = new DataSet();
                //    ds = objBs.expensivename(txtCustname.Text, sTableName);
                //    if ((ds.Tables[0].Rows.Count) > 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This Customer Name is all ready exist!');", true);
                //        txtCustname.Focus();
                //        return;
                //        // lblerror.Text = " This Group Name is all ready exist";


                //    }

                //    if (txtCustname.Text == "")
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Please Enter Customer Name')", true);

                //        return;

                //    }
                //}
                //if (ddlProvince.SelectedValue == "Select Province type")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Province type')", true);

                //    return;
                //}

                //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                //{
                //    DropDownList txttt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpCategory");
                //    DropDownList txtd = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");

                //    TextBox txtref = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrefno");
                //    TextBox txtcer = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtCerno");

                //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                //    int col = vLoop + 1;
                //    if (txt.Text == "")
                //    {
                //    }
                //    else
                //    {
                //        if (txtd.SelectedItem.Text == "Select Product Code")
                //        {
                //            if (gvcustomerorder.Rows.Count == 1)
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Atleast one product.Thank you!!! ')", true);
                //                return;
                //            }
                //        }


                //        else
                //        {
                //            if (btnadd.Text == "Save")
                //            {
                //                if (txtref.Text != "")
                //                {
                //                    DataSet getrefno = objBs.getallrefnoforsales(sTableName, txtref.Text);
                //                    if (getrefno.Tables[0].Rows.Count > 0)
                //                    {
                //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This reference number already Exists in " + col + " ')", true);

                //                        return;
                //                    }
                //                    else
                //                    {

                //                    }
                //                }
                //                else
                //                {

                //                }
                //            }
                //            else
                //            {
                //                string isalesid = Request.QueryString.Get("iSalesID");

                //                DataSet getrefno = objBs.getallrefnoforsales(sTableName, txtref.Text, isalesid);
                //                if (txtref.Text != "")
                //                {
                //                    if (getrefno.Tables[0].Rows.Count > 0)
                //                    {
                //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This reference number already Exists in " + col + " ')", true);

                //                        return;
                //                    }
                //                    else
                //                    {

                //                    }
                //                }

                //            }



                //            if (txtd.SelectedValue == "0" || txtd.SelectedValue == "Select Product Code")
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Product Code in row " + col + " ')", true);
                //                txtd.Focus();
                //                return;
                //            }
                //            if (txt.SelectedValue == "0" || txt.SelectedValue == "Select Product")
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Product in row " + col + " ')", true);
                //                txt.Focus();
                //                return;
                //            }
                //            if ((txtttk.Text == "") || (Convert.ToInt32(txtttk.Text) == 0))
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter qty in row " + col + " ')", true);
                //                txtttk.Focus();
                //                return;
                //            }
                //            //if ((txtref.Text == ""))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter ref.no in row " + col + " ')", true);
                //            //    txtttk.Focus();
                //            //    return;
                //            //}
                //            //if ((txtcer.Text == ""))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Certificate No in row " + col + " ')", true);
                //            //    txtttk.Focus();
                //            //    return;
                //            //}



                //            double qtyy = Convert.ToDouble(Convert.ToDouble(txtktt.Text));
                //            //double qtyy = Convert.ToDouble(Convert.ToDouble(txtktt.Text) + Convert.ToDouble(txtttk.Text));

                //            //if (Convert.ToDouble(txtttk.Text) > Convert.ToDouble(qtyy))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty greater than the Current stock in row " + col + " ')", true);
                //            //    txtttk.Focus();
                //            //    return;
                //            //}


                //            //double qtyy = Convert.ToDouble(Convert.ToDouble(txtktt.Text) + Convert.ToDouble(txtttk.Text));

                //            //if ( Convert.ToDouble(qtyy) > Convert.ToDouble(txtttk.Text))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty greater than the Current stock in row " + col + " ')", true);
                //            //    txtttk.Focus();
                //            //    return;
                //            //}

                //            //if (Convert.ToInt32(txtttk.Text) > Convert.ToInt32(txtktt.Text))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty greater than the Current stock in row " + col + " ')", true);
                //            //    txtttk.Focus();
                //            //    return;
                //            //}
                //            //if ((txtktttt.Text == ""))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Discount% in row " + col + " ')", true);
                //            //    txtktttt.Focus();
                //            //    return;
                //            //}

                //            //if ((Convert.ToDouble(txtktttt.Text) > 100))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Discount% within 100 in Row " + col + " ')", true);
                //            //    txtktttt.Focus();
                //            //    return;
                //            //}

                //            if ((txttk.Text == "") || (Convert.ToString(txttk.Text) == ".00"))
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter the Rate " + col + " ')", true);
                //                txttk.Focus();
                //                return;
                //            }
                //        }
                //    }
                //}

                ////  return;

                //DateTime billldate = DateTime.ParseExact(txtvoudate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime lrrdate = DateTime.ParseExact(txtlrdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime orddate = DateTime.ParseExact(txtorderdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime dueedate = DateTime.ParseExact(txtduedate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime vouchdate = DateTime.ParseExact(txtvoudate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ////return;
                //DataSet dsss = objBs.customerid(1, 2);
                //if (dsss.Tables[0].Rows.Count > 0)
                //{
                //    // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                //    if (dsss.Tables[0].Rows[0]["CustomerId"].ToString() == "")
                //        custid = "1";
                //    else
                //        custid = dsss.Tables[0].Rows[0]["CustomerId"].ToString();
                //}
                //if (btnadd.Text == "Save")
                //{
                //    // ddlPayMode_SelectedIndexChanged(sender, e);
                //    // txtbillcheck(sender,e);

                //    int iStatus2 = 0, iStatus3 = 0, iStatus4 = 0, iStatus5 = 0, iStockSuccess = 0;
                //    int iCustid = 0;
                //    int Id = 0;
                //    // return;

                //    if (chknewcust.Checked == true)
                //    {
                //        //iCustid = objBs.InsertCustBill(Convert.ToInt32(lblUserID.Text), txtCustname.Text, txtmobileno.Text, "0", txtaddress.Text, txtcity.Text, txtpincode.Text, Convert.ToInt32(bblbillto.SelectedValue), bblbillto.SelectedItem.Text);
                //        iCustid = objBs.InsertCustBillnew(txtCustname.Text, "0", "0", "31", txtadd.Text, "509", "0", "0", 1, "Yes", Convert.ToDouble("0.00"), Convert.ToDouble("0.00"), "Credit Note", Convert.ToInt32(1), Convert.ToInt32(custid), "0", "tblAuditMaster_" + sTableName, lblUser.Text, "Customer", "Credit Note", "0", custid, Convert.ToInt32(ddlRepname.SelectedValue), "0", txtTransport.Text, sTableName, Convert.ToInt32(ddlPriceList.SelectedValue), Convert.ToInt32(ddlProvince.SelectedValue));
                //    }
                //    else
                //    {
                //        iCustid = Convert.ToInt32(ddlcustomerID.SelectedValue);
                //    }

                //    DataSet dstt = new DataSet();

                //    if (Convert.ToInt32(ddlPayMode.SelectedValue) == 6)
                //    {
                //        DataTable dttt = new DataTable();

                //        DataColumn dc = new DataColumn("RefNo");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("TransDate");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("DebitorID");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("CreditorID");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("Amount");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("Narration");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("Type");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("Cheque");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("PayMode");
                //        dttt.Columns.Add(dc);

                //        dstt.Tables.Add(dttt);

                //        if (ddlAgainst.SelectedValue != "0" && txtchequedd.Text != "" && txtAgainstAmount.Text != "0")
                //        {
                //            DataRow drd = dstt.Tables[0].NewRow();
                //            drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //            drd["TransDate"] = txtdate1.Text;
                //            drd["DebitorID"] = ddlAgainst.SelectedValue;
                //            drd["CreditorID"] = iCustid;
                //            drd["Amount"] = txtAgainstAmount.Text;
                //            drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //            drd["Cheque"] = txtchequedd.Text;
                //            drd["PayMode"] = 2;
                //            drd["Type"] = "Receipt";
                //            dstt.Tables[0].Rows.Add(drd);
                //        }
                //        if (ddlAgainst1.SelectedValue != "0" && txtchequedd1.Text != "" && txtAgainstAmount1.Text != "0")
                //        {
                //            DataRow drd = dstt.Tables[0].NewRow();
                //            drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //            drd["TransDate"] = txtdate1.Text;
                //            drd["DebitorID"] = ddlAgainst1.SelectedValue;
                //            drd["CreditorID"] = iCustid;
                //            drd["Amount"] = txtAgainstAmount1.Text;
                //            drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //            drd["Cheque"] = txtchequedd1.Text;
                //            drd["PayMode"] = 2;
                //            drd["Type"] = "Receipt";
                //            dstt.Tables[0].Rows.Add(drd);
                //        }
                //        if (!string.IsNullOrEmpty(txtAgainstAmount2.Text) && txtAgainstAmount2.Text != "0")
                //        {
                //            DataRow drd = dstt.Tables[0].NewRow();
                //            drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //            drd["TransDate"] = txtdate1.Text;

                //            DataSet dst = objBs.getCashledgerId("Cash A/C _" + sTableName);
                //            int DID = Convert.ToInt32(dst.Tables[0].Rows[0]["LedgerID"]);

                //            drd["DebitorID"] = DID;
                //            drd["CreditorID"] = iCustid;
                //            drd["Amount"] = txtAgainstAmount2.Text;
                //            drd["Cheque"] = 0;
                //            drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //            drd["PayMode"] = 1;
                //            drd["Type"] = "Receipt";
                //            dstt.Tables[0].Rows.Add(drd);
                //        }
                //    }

                //    if ((Convert.ToInt32(ddlPayMode.SelectedValue) == 1) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 0) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 3) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 5) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 6))
                //    {
                //        Id = 0;
                //    }
                //    else
                //    {
                //        //Id = Convert.ToInt32(ddlBank.SelectedValue);
                //    }

                //    int iStat = objBs.insertsalesnew(sTableName, "tblDayBook_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(lblUserID.Text), txtbillno.Text, billldate, Convert.ToInt32(iCustid), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtTaxamt.Text), Convert.ToDouble(txtdiscount.Text), Convert.ToInt32("0"), Convert.ToInt32(1), Convert.ToDouble("0"), Convert.ToInt32(ddlPayMode.SelectedValue), txtNarration.Text, Id, txtCheque.Text, Convert.ToInt32(ddlbook.SelectedValue), txtvouno.Text, vouchdate, Convert.ToInt32(ddlvouchertype.SelectedValue), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToInt32(ddlCash.SelectedValue), txtlrno.Text, txtTransport.Text, txtdestination.Text, txtorderno.Text, Convert.ToInt32(ddlRepname.SelectedValue), txtpackingslip.Text, Convert.ToString(txtDisc.Text), lrrdate, dueedate, orddate, txtnopackage.Text, txtFreight.Text, txtLU.Text, txtroundoff.Text, txtadd.Text, txtdiscountamount.Text);

                //    //if (Convert.ToInt32(ddlPayMode.SelectedValue) == 6)
                //    //{
                //    //    int iStatus = objBs.InsertCustReceipt(sTableName, "tblReceipt_" + sTableName, "tblDaybook_" + sTableName, txtbillno.Text, Convert.ToInt32(iCustid), txtdate1.Text, dstt, "tblTransReceipt_" + sTableName, Convert.ToDouble(txtgrandtotal.Text));
                //    //}
                //    //int orderno = Convert.ToInt32(gvcustomerorder.Rows[0].Cells[0]);




                //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                //    {

                //        //Label txtno = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");
                //        //Label orderno = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                //        //string orderno = gvcustomerorder.Rows[i].Cells[0].Text;
                //        TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                //        DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
                //        DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[i].FindControl("ProductCode");

                //        int idef = Convert.ToInt32(ddldef.SelectedValue);
                //        if (ProductCode.SelectedItem.Text == "Select Product Code")
                //        {

                //        }
                //        else
                //        {
                //            DropDownList ddcategory = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpCategory");
                //            int icat = Convert.ToInt32(ddcategory.SelectedValue);
                //            ddcategory.Focus();
                //            ddcategory.Enabled = true;
                //            //   DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
                //            //int idef = Convert.ToInt32(ddldef.SelectedValue);

                //            TextBox Qty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtQty");
                //            double dQty = Convert.ToDouble(Qty.Text);

                //            // TextBox orderno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                //            int dorno = Convert.ToInt32(orderno.Text);

                //            TextBox refno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrefno");
                //            string drefno = refno.Text;

                //            TextBox cerno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtCerno");
                //            string dcerno = cerno.Text;

                //            TextBox Dis = (TextBox)gvcustomerorder.Rows[i].FindControl("txtDiscount");
                //            double dDis = Convert.ToDouble(Dis.Text);
                //            TextBox Rate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                //            double DRate = Convert.ToDouble(Rate.Text);
                //            TextBox Tax = (TextBox)gvcustomerorder.Rows[i].FindControl("txtTax");
                //            double DTax = Convert.ToDouble(Tax.Text);

                //            TextBox Amount = (TextBox)gvcustomerorder.Rows[i].FindControl("txtAmount");
                //            double dAmount = Convert.ToDouble(Amount.Text);


                //            iStatus2 = objBs.insertTransSales("tblTransSales_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty), Convert.ToDouble(DRate), Convert.ToDouble(dDis), Convert.ToDouble(dAmount), Convert.ToInt32(ddcategory.SelectedValue), Convert.ToDouble(DTax), txtdiscount.Text, txtgrandtotal.Text, txtTaxamt.Text, totqty.Text, Convert.ToDouble(totmeter.Text), dorno, drefno, dcerno);

                //            iStockSuccess = UpdateStockAvailable(Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty));
                //        }

                //    }
                //    //  ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Saved successfully.');", true);
                //    // return;
                //    System.Threading.Thread.Sleep(3000);
                //    Response.Redirect("cashsales.aspx");
                //}

                //else if (btnadd.Text == "Update")
                //{

                //    int iStatus2 = 0, iStatus3 = 0, iStatus4 = 0, iStatus5 = 0, iStockSuccess = 0;


                //    string iSalesID = Request.QueryString.Get("iSalesID");
                //    iDealer = Request.QueryString.Get("iDealer");

                //    //string Idealer = Request.QueryString.Get("iDealer");
                //    // string iSalesID = Request.QueryString.Get("iSalesID");


                //    if (iSalesID != null)
                //    {

                //        if (txtgrandtotal.Text != "")
                //        {
                //            int isalesid = Convert.ToInt32(txtbillno.Text);

                //            DataSet dsTransSales = objBs.GetTransSales("tblTransSales_" + sTableName, iSalesID);
                //            if (dsTransSales.Tables[0].Rows.Count > 0)
                //            {
                //                for (int i = 0; i < dsTransSales.Tables[0].Rows.Count; i++)
                //                {
                //                    int ddldef = Convert.ToInt32(dsTransSales.Tables[0].Rows[i]["SubCategoryID"]);
                //                    int ddcategory = Convert.ToInt32(dsTransSales.Tables[0].Rows[i]["CategoryId"]);
                //                    int Trid = Convert.ToInt32(dsTransSales.Tables[0].Rows[i]["SalesID"]);
                //                    string Amount = Convert.ToString(dsTransSales.Tables[0].Rows[i]["Amount"]);

                //                    if (Amount != "")
                //                    {
                //                        DataSet ds_stock = objBs.GetPurchaseStok(Convert.ToInt32(ddldef), "tblStock_" + sTableName);
                //                        int qty_stock = Convert.ToInt32(ds_stock.Tables[0].Rows[0]["Available_QTY"].ToString());

                //                        DataSet ds_stockTSR = objBs.Get_TranssalesStock("tblTransSales_" + sTableName, (Convert.ToInt32(ddldef)), iSalesID);
                //                        int qty_current = Convert.ToInt32(ds_stockTSR.Tables[0].Rows[0]["Quantity"].ToString());

                //                        int qty = qty_stock + qty_current;
                //                        int update = objBs.updateSalesStock(qty, Convert.ToInt32(ddcategory), Convert.ToInt32(ddldef), "tblStock_" + sTableName);

                //                    }


                //                }
                //            }

                //            int iTransDelete = objBs.deletesalseordervalues("tblTransSales_" + sTableName, iSalesID);
                //            //  int iDelete = objBs.DeleteSales("tblSales_" + sTableName, iSalesID, "tblDayBook_" + sTableName,"0", "0", "0", sTableName);


                //            try
                //            {
                //                int iCustid = 0;
                //                int Id = 0;

                //                iCustid = Convert.ToInt32(ddlcustomerID.SelectedValue);


                //                DataSet dstt = new DataSet();

                //                if (Convert.ToInt32(ddlPayMode.SelectedValue) == 6)
                //                {
                //                    DataTable dttt = new DataTable();

                //                    DataColumn dc = new DataColumn("RefNo");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("TransDate");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("DebitorID");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("CreditorID");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("Amount");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("Narration");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("Type");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("Cheque");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("PayMode");
                //                    dttt.Columns.Add(dc);

                //                    dstt.Tables.Add(dttt);

                //                    if (ddlAgainst.SelectedValue != "0" && txtchequedd.Text != "" && txtAgainstAmount.Text != "0")
                //                    {
                //                        DataRow drd = dstt.Tables[0].NewRow();
                //                        drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //                        drd["TransDate"] = txtdate1.Text;
                //                        drd["DebitorID"] = ddlAgainst.SelectedValue;
                //                        drd["CreditorID"] = iCustid;
                //                        drd["Amount"] = txtAgainstAmount.Text;
                //                        drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //                        drd["Cheque"] = txtchequedd.Text;
                //                        drd["PayMode"] = 2;
                //                        drd["Type"] = "Receipt";
                //                        dstt.Tables[0].Rows.Add(drd);
                //                    }
                //                    if (ddlAgainst1.SelectedValue != "0" && txtchequedd1.Text != "" && txtAgainstAmount1.Text != "0")
                //                    {
                //                        DataRow drd = dstt.Tables[0].NewRow();
                //                        drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //                        drd["TransDate"] = txtdate1.Text;
                //                        drd["DebitorID"] = ddlAgainst1.SelectedValue;
                //                        drd["CreditorID"] = iCustid;
                //                        drd["Amount"] = txtAgainstAmount1.Text;
                //                        drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //                        drd["Cheque"] = txtchequedd1.Text;
                //                        drd["PayMode"] = 2;
                //                        drd["Type"] = "Receipt";
                //                        dstt.Tables[0].Rows.Add(drd);
                //                    }
                //                    if (!string.IsNullOrEmpty(txtAgainstAmount2.Text) && txtAgainstAmount2.Text != "0")
                //                    {
                //                        DataRow drd = dstt.Tables[0].NewRow();
                //                        drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //                        drd["TransDate"] = txtdate1.Text;

                //                        DataSet dst = objBs.getCashledgerId("Cash A/C _" + sTableName);
                //                        int DID = Convert.ToInt32(dst.Tables[0].Rows[0]["LedgerID"]);

                //                        drd["DebitorID"] = DID;
                //                        drd["CreditorID"] = iCustid;
                //                        drd["Amount"] = txtAgainstAmount2.Text;
                //                        drd["Cheque"] = 0;
                //                        drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //                        drd["PayMode"] = 1;
                //                        drd["Type"] = "Receipt";
                //                        dstt.Tables[0].Rows.Add(drd);
                //                    }
                //                }

                //                if ((Convert.ToInt32(ddlPayMode.SelectedValue) == 1) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 0) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 3) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 5) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 6))
                //                {
                //                    Id = 0;
                //                }
                //                else
                //                {

                //                }

                //                int iStat = objBs.updatesales(sTableName, "tblDayBook_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(lblUserID.Text), txtbillno.Text, billldate, Convert.ToInt32(iCustid), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtTaxamt.Text), Convert.ToDouble(txtdiscount.Text), Convert.ToInt32("0"), Convert.ToInt32(1), Convert.ToDouble("0"), Convert.ToInt32(ddlPayMode.SelectedValue), txtNarration.Text, Id, txtCheque.Text, Convert.ToInt32(ddlbook.SelectedValue), txtvouno.Text, vouchdate, Convert.ToInt32(ddlvouchertype.SelectedValue), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToInt32(ddlCash.SelectedValue), txtlrno.Text, txtTransport.Text, txtdestination.Text, txtorderno.Text, Convert.ToInt32(ddlRepname.SelectedValue), txtpackingslip.Text, Convert.ToString(txtDisc.Text), lrrdate, dueedate, orddate, txtnopackage.Text, iSalesID, txtFreight.Text, txtLU.Text, txtroundoff.Text, txtadd.Text, txtdiscountamount.Text);

                //                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                //                {
                //                    //Label orderno = (Label)gvcustomerorder.Rows[i].FindControl("txtno");
                //                    TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");
                //                    //string orderno = gvcustomerorder.Rows[i].Cells[0].Text; 

                //                    DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
                //                    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[i].FindControl("ProductCode");
                //                    // int idef = Convert.ToInt32(ddldef.SelectedValue);
                //                    if (ProductCode.SelectedItem.Text == "Select Product Code")
                //                    {

                //                    }
                //                    else
                //                    {
                //                        DropDownList ddcategory = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpCategory");
                //                        int icat = Convert.ToInt32(ddcategory.SelectedValue);
                //                        ddcategory.Focus();
                //                        ddcategory.Enabled = true;
                //                        //    DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
                //                        int idef = Convert.ToInt32(ddldef.SelectedValue);

                //                        TextBox Qty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtQty");
                //                        double dQty = Convert.ToDouble(Qty.Text);

                //                        //Label orderno1 = (Label)gvcustomerorder.Rows[i].FindControl("txtno");
                //                        int dorno = Convert.ToInt32(orderno.Text);

                //                        TextBox refno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrefno");
                //                        string drefno = refno.Text;

                //                        TextBox cerno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtCerno");
                //                        string dcerno = cerno.Text;

                //                        TextBox Dis = (TextBox)gvcustomerorder.Rows[i].FindControl("txtDiscount");
                //                        double dDis = Convert.ToDouble(Dis.Text);
                //                        TextBox Rate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                //                        double DRate = Convert.ToDouble(Rate.Text);
                //                        TextBox Tax = (TextBox)gvcustomerorder.Rows[i].FindControl("txtTax");
                //                        double DTax = Convert.ToDouble(Tax.Text);

                //                        TextBox Amount = (TextBox)gvcustomerorder.Rows[i].FindControl("txtAmount");
                //                        double dAmount = Convert.ToDouble(Amount.Text);


                //                        iStatus2 = objBs.updateTransSalesnew("tblTransSales_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty), Convert.ToDouble(DRate), Convert.ToDouble(dDis), Convert.ToDouble(dAmount), Convert.ToInt32(ddcategory.SelectedValue), Convert.ToDouble(DTax), txtdiscount.Text, txtgrandtotal.Text, txtTaxamt.Text, totqty.Text, Convert.ToDouble(totmeter.Text), dorno, iSalesID, drefno, dcerno);

                //                        iStockSuccess = UpdateStockAvailable(Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty));
                //                    }

                //                }
                //                System.Threading.Thread.Sleep(3000);

                //                Response.Redirect("cashsales.aspx");
                //            }
                //            catch (Exception ex)
                //            {
                //                Response.Write(ex.ToString());
                //            }


                //        }

                //    }

                //}

            }
            Response.Redirect("ViewProcess.aspx");
        }
     

  

      

      
     

    }
}