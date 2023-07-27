using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class Agentmaster : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            string date = DateTime.Now.ToString("dd/MM/yyyy");

            txtdob.Text = date;
            txtAnniversary.Text = date;

            string name = Request.QueryString.Get("name");

            if (name == "Add New")
            {
                add.Visible = true;
                div3.Visible = false;
                head.InnerText = "Agent Master";
            }
            else if (name == "Bulk Addition")
            {
                add.Visible = false;
                div3.Visible = true;
                head.InnerText = "Bulk Customer/Vendor Master Addition";
            }


            if (!IsPostBack)
            {
                divcode.Visible = false;
                DataSet dsContact = objBs.GetCustomerType();
                if (dsContact.Tables[0].Rows.Count > 0)
                {
                    ddlCustomerType.DataSource = dsContact.Tables[0];
                    ddlCustomerType.DataTextField = "ContactType";
                    ddlCustomerType.DataValueField = "ContactID";
                    ddlCustomerType.DataBind();
                    ddlCustomerType.Items.Insert(0, "Select Contact Type");
                }



                //btnadd.Attributes.Add("onclick", "return valchk();"); // Validation Check lines
                DataSet ds = objBs.customerid(1, 2);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                    if (ds.Tables[0].Rows[0]["CustomerId"].ToString() == "")
                        txtCustomerid.Text = "1";
                    else
                        txtCustomerid.Text = ds.Tables[0].Rows[0]["CustomerId"].ToString();


                    lblUser.Text = Session["UserName"].ToString();
                    lblUserID.Text = Session["UserID"].ToString();

                    string iCusID = Request.QueryString.Get("LedgerID");
                    if (iCusID != "" || iCusID != null)
                    {
                        DataSet ds1 = objBs.editCustomer(iCusID);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            btnadd.Text = "Update";
                            div3.Visible = false;
                            head.InnerText = "Customer Master";
                            txtCustomerid.Text = ds1.Tables[0].Rows[0]["CustomerId"].ToString();
                            TextBox1.Text = ds1.Tables[0].Rows[0]["Prefix"].ToString();
                            txtcuscode.Text = ds1.Tables[0].Rows[0]["LedgerID"].ToString();
                            txtcustomername.Text = ds1.Tables[0].Rows[0]["LedgerName"].ToString();
                            txtgstin.Text = ds1.Tables[0].Rows[0]["GSTIN"].ToString();
                            txtmobileno.Text = ds1.Tables[0].Rows[0]["MobileNo"].ToString();
                            txtphoneno.Text = ds1.Tables[0].Rows[0]["PhoneNo"].ToString();
                            txtarea.Text = ds1.Tables[0].Rows[0]["Area"].ToString();
                            txtaddress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();
                            txtcity.Text = ds1.Tables[0].Rows[0]["City"].ToString();
                            txtpincode.Text = ds1.Tables[0].Rows[0]["Pincode"].ToString();
                            txtemail.Text = ds1.Tables[0].Rows[0]["Email"].ToString();

                            txtprintname.Text = ds1.Tables[0].Rows[0]["Printname"].ToString();
                            txtAddressProof.Text = ds1.Tables[0].Rows[0]["AddressProof"].ToString();
                            txtIDProof.Text = ds1.Tables[0].Rows[0]["IDProof"].ToString();
                            txtDesignation.Text = ds1.Tables[0].Rows[0]["Designation"].ToString();
                            txtdob.Text = ds1.Tables[0].Rows[0]["Birthdate"].ToString();
                            txtAnniversary.Text = ds1.Tables[0].Rows[0]["Anniversarydate"].ToString();
                            txtCreditDays.Text = ds1.Tables[0].Rows[0]["CreditDays"].ToString();

                            txtPAN.Text = ds1.Tables[0].Rows[0]["PAN"].ToString();
                            txtDelivery.Text = ds1.Tables[0].Rows[0]["Deliveryaddress"].ToString();
                            txtCST.Text = ds1.Tables[0].Rows[0]["CST"].ToString();

                            // ddlGrade.SelectedValue = ds1.Tables[0].Rows[0]["Grade"].ToString();
                            ddlPrice.SelectedValue = ds1.Tables[0].Rows[0]["Price"].ToString();
                            //  ddlSender.SelectedValue = ds1.Tables[0].Rows[0]["Sender"].ToString();
                            //  ddlSignature.SelectedValue = ds1.Tables[0].Rows[0]["Signature"].ToString();


                            ddlCustomerType.SelectedValue = ds1.Tables[0].Rows[0]["contactTypeID"].ToString();

                            txtTinNO.Text = ds1.Tables[0].Rows[0]["TinNo"].ToString();


                            ddlCDType.SelectedValue = ds1.Tables[0].Rows[0]["Type"].ToString();

                            if (ddlCDType.SelectedValue == "Credit Note")
                            {
                                txtOBalance.Text = ds1.Tables[0].Rows[0]["Open_Credit"].ToString();
                            }
                            else
                            {
                                txtOBalance.Text = ds1.Tables[0].Rows[0]["Open_Depit"].ToString();
                            }

                            // ddlCDType.SelectedItem.Text = ds1.Tables[0].Rows[0]["ContactTypeID"].ToString();
                            ddlIsActive.SelectedItem.Text = ds1.Tables[0].Rows[0]["IsActive"].ToString();


                        }

                    }
                }

            }
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            string openingbalance = txtOBalance.Text;
            int Grupid;
            string Tinno = "0";
            ddlCustomerType.SelectedItem.Text = "Agent";
            if (ddlCustomerType.SelectedItem.Text == "Agent")
            {
                Grupid = 1;

            }
            else
            {
                Grupid = 2;

            }
            Grupid = 1;
            Tinno = txtTinNO.Text;
            DateTime date = DateTime.ParseExact(txtdob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dat = DateTime.ParseExact(txtAnniversary.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnadd.Text == "Save")
            {
                if (ddlCDType.SelectedValue == "Credit Note")
                {
                    string Credite = txtOBalance.Text;

                    //int iStatus = objBs.insertcustomerdetail(Convert.ToInt32(lblUserID.Text), txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Convert.ToInt32(ddlCustomerType.SelectedValue), ddlIsActive.SelectedValue, Convert.ToDouble(Credite), Convert.ToDouble("0"), ddlCDType.SelectedValue);
                    int i = objBs.ledgercr(txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Grupid, ddlIsActive.SelectedValue, Convert.ToDouble(Credite), Convert.ToDouble("0"), ddlCDType.SelectedValue, Convert.ToInt32("8"), Convert.ToInt32(txtCustomerid.Text), Tinno, "tblAuditMaster_" + sTableName, lblUser.Text, "Agent", ddlCDType.SelectedItem.Text, openingbalance, txtCustomerid.Text, TextBox1.Text, txtCST.Text, txtPAN.Text, txtDesignation.Text, txtDelivery.Text, 0, 0, 0, Convert.ToInt32(ddlPrice.SelectedValue), date, dat, Convert.ToInt32(txtCreditDays.Text), "0", txtprintname.Text,txtgstin.Text,"1");
                    Response.Redirect("../Accountsbootstrap/Viewagent.aspx");
                }
                else
                {
                    string Debit = txtOBalance.Text;

                    //int iStatus1 = objBs.insertcustomerdetail(Convert.ToInt32(lblUserID.Text), txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Convert.ToInt32(ddlCustomerType.SelectedValue), ddlIsActive.SelectedValue, Convert.ToDouble("0"), Convert.ToDouble(Debit), ddlCDType.SelectedValue);
                    int j = objBs.ledgercr(txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Grupid, ddlIsActive.SelectedValue, Convert.ToDouble("0"), Convert.ToDouble(Debit), ddlCDType.SelectedValue, Convert.ToInt32("8"), Convert.ToInt32(txtCustomerid.Text), Tinno, "tblAuditMaster_" + sTableName, lblUser.Text, "Agent", ddlCDType.SelectedItem.Text, openingbalance, txtCustomerid.Text, TextBox1.Text, txtCST.Text, txtPAN.Text, txtDesignation.Text, txtDelivery.Text, 0, 0, 0, Convert.ToInt32(ddlPrice.SelectedValue), date, dat, Convert.ToInt32(txtCreditDays.Text), "0", txtprintname.Text, txtgstin.Text, "1");
                    Response.Redirect("../Accountsbootstrap/Viewagent.aspx");
                }
            }
            else
            {
                string iCusID = Request.QueryString.Get("LedgerID");
                if (ddlCDType.SelectedValue == "Credit Note")
                {
                    string Credite = txtOBalance.Text;

                    int iStatus = objBs.updatecustomer(Convert.ToInt32(iCusID), txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Convert.ToInt32("8"), ddlIsActive.SelectedItem.Text, Convert.ToDouble(Credite), Convert.ToDouble("0"), ddlCDType.SelectedValue, Convert.ToInt32(txtCustomerid.Text), Tinno, "tblAuditMaster_" + sTableName, lblUser.Text, "Agent", ddlCDType.SelectedItem.Text, openingbalance, txtCustomerid.Text, TextBox1.Text, txtCST.Text, txtPAN.Text, txtDesignation.Text, txtDelivery.Text, 0, 0, 0, Convert.ToInt32(ddlPrice.SelectedValue), date, dat, Convert.ToInt32(txtCreditDays.Text), "0", txtprintname.Text, txtgstin.Text, "1");
                    Response.Redirect("../Accountsbootstrap/Viewagent.aspx");
                }
                else
                {
                    string Debit = txtOBalance.Text;
                    int iStatus = objBs.updatecustomer(Convert.ToInt32(iCusID), txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Convert.ToInt32("8"), ddlIsActive.SelectedItem.Text, Convert.ToDouble("0"), Convert.ToDouble(Debit), ddlCDType.SelectedValue, Convert.ToInt32(txtCustomerid.Text), Tinno, "tblAuditMaster_" + sTableName, lblUser.Text, "Agent", ddlCDType.SelectedItem.Text, openingbalance, txtCustomerid.Text, TextBox1.Text, txtCST.Text, txtPAN.Text, txtDesignation.Text, txtDelivery.Text, 0, 0, 0, Convert.ToInt32(ddlPrice.SelectedValue), date, dat, Convert.ToInt32(txtCreditDays.Text), "0", txtprintname.Text, txtgstin.Text, "1");
                    Response.Redirect("../Accountsbootstrap/Viewagent.aspx");
                }
            }


            System.Threading.Thread.Sleep(6000);
        }
        //protected void Edit_Click(object sender, EventArgs e)
        //{

        //    Response.Redirect("../Accountsbootstrap/viewcustomer.aspx");
        //}
        //protected void Update_Click(object sender, EventArgs e)
        //{

        //   int iStatus = objBs.updatecustomer(txtcuscode.Text, txtcustomername.Text,txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text);
        //   Response.Redirect("../Accountsbootstrap/customermaster.aspx");
        //}
        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/Viewagent.aspx");
        }

        protected void txtcustomername_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = objBs.expensivename(txtcustomername.Text);
            if ((ds.Tables[0].Rows.Count) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This Agent Name is all ready exist!');", true);
                btnadd.Visible = false;
                return;
            }
            else
            {
                lblerror.Text = "";
                btnadd.Visible = true;
                txtmobileno.Focus();
            }
        }

        protected void ddlCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnFormat_Click(object sender, EventArgs e)
        {
            bindData();

        }

        public void bindData()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Contact name"));
            dt.Columns.Add(new DataColumn("Address"));
            dt.Columns.Add(new DataColumn("area"));
            dt.Columns.Add(new DataColumn("City"));
            dt.Columns.Add(new DataColumn("Pincode"));
            dt.Columns.Add(new DataColumn("Mobile"));
            dt.Columns.Add(new DataColumn("Phone no"));
            dt.Columns.Add(new DataColumn("Email"));
            dt.Columns.Add(new DataColumn("Open CR"));
            dt.Columns.Add(new DataColumn("Open DR"));
            dt.Columns.Add(new DataColumn("Tin NO"));
            dt.Columns.Add(new DataColumn("Prefix"));


            DataRow dr_final12 = dt.NewRow();
            dr_final12["Contact name"] = "";
            dr_final12["Address"] = "";
            dr_final12["area"] = "";
            dr_final12["City"] = "";
            dr_final12["Pincode"] = "";
            dr_final12["Mobile"] = "";
            dr_final12["Phone no"] = "";
            dr_final12["Email"] = "";
            dr_final12["Open CR"] = "";
            dr_final12["Open DR"] = "";
            dr_final12["Tin No"] = "";
            dr_final12["Prefix"] = "";
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
            int custid = 0;
            try
            {
                if (masterradio.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select contact Type.');", true);
                    return;
                }


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

                    if (masterradio.SelectedValue == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select contact Type.');", true);
                        return;
                    }
                    else
                    {
                        custid = Convert.ToInt16(masterradio.SelectedValue);

                        if (custid == 6)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if ((Convert.ToString(dr["Tin No"]) == null) || (Convert.ToString(dr["Tin No"]) == ""))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Tin No is empty.It cannot be left balnk for creating vendor.');", true);
                                    return;
                                }
                            }

                        }

                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Contact name"]) == null) || (Convert.ToString(dr["Contact name"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Contact name is empty');", true);
                            return;
                        }
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Address"]) == null) || (Convert.ToString(dr["Address"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('address is empty');", true);
                            return;
                        }
                    }


                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["area"]) == null) || (Convert.ToString(dr["area"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Area is empty');", true);
                            return;
                        }
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["city"]) == null) || (Convert.ToString(dr["city"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('city Name is empty');", true);
                            return;
                        }
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Pincode"]) == null) || (Convert.ToString(dr["Pincode"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Pincode is empty');", true);
                            return;
                        }
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Mobile"]) == null) || (Convert.ToString(dr["Mobile"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Mobile is empty');", true);
                            return;
                        }
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Phone no"]) == null) || (Convert.ToString(dr["Phone no"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Phone no is empty');", true);
                            return;
                        }
                    }

                    //foreach (DataRow dr in ds.Tables[0].Rows)
                    //{
                    //    if ((Convert.ToString(dr["Email"]) == null) || (Convert.ToString(dr["Email"]) == ""))
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email is empty');", true);
                    //        return;
                    //    }
                    //}
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Open CR"]) == null) || (Convert.ToString(dr["Open CR"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Open CR is empty Otherwise put zero.');", true);
                            return;
                        }
                    }
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Open DR"]) == null) || (Convert.ToString(dr["Open DR"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Open DR is empty Otherwise put zero.');", true);
                            return;
                        }
                    }


                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        bool isEmail = Regex.IsMatch(Convert.ToString(dr["Email"]), @"\A(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?)\Z");
                        string Email = Convert.ToString(dr["Email"]);
                        if ((Convert.ToString(dr["Email"]) == null) || (Convert.ToString(dr["Email"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email is empty');", true);
                            return;
                        }
                        if (!isEmail)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Email Id is invalid " + Email + "')", true);
                            return;
                        }
                    }




                    //check duplicate

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string customer = Convert.ToString(dr["Contact name"]);

                        if (objBs.CheckIfCustomer(customer))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Contact name " + customer + " has already Exists. please enter a new one')", true);
                            return;
                            // lblerror.Text = "These Category has already Exists. please enter a new one";

                        }

                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        if ((Convert.ToString(dr["Contact name"]) != null) || (Convert.ToString(dr["Contact name"]) != ""))
                        {
                            int index = Convert.ToString(dr["Contact name"]).IndexOfAny(specialCharactersArray);
                            //index == -1 no special characters
                            if (index != -1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Special characters not allowed in Contact name');", true);
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
                        cat = Convert.ToString(dr["Contact name"]);

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
                                    if (cat == Convert.ToString(drd["Contact name"]))
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Contact name with  - " + cat + " - already exists in the excel.');", true);
                                        return;
                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                        i = i + 1;
                        ii = 1;
                    }
                    int Grupid;
                    string Tinno = "0";
                    if (masterradio.SelectedItem.Text == "Vendor" || masterradio.SelectedItem.Text == "Service Center")
                    {
                        Grupid = 2;
                        Tinno = txtTinNO.Text;
                    }
                    else
                    {
                        Grupid = 1;
                        Tinno = "0";
                    }

                    objBs.InsertBulkLedgerCustomer(ds, custid, Grupid);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Contact Master Uploaded Successfully');", true);



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