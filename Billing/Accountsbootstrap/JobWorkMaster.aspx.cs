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
    public partial class JobWorkMaster : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        DataTable dCrt;
        DataTable dCrt1;

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
                Div1.Visible = false;
                head.InnerText = "Job Work";
            }
            else if (name == "Bulk Addition")
            {
                add.Visible = false;
                Div1.Visible = true;
                head.InnerText = "Bulk Customer/Vendor Master Addition";
            }


            if (!IsPostBack)
            {

                DataTable dt = new DataTable();


                dt.Columns.Add("Itemid");
                dt.Columns.Add("ItemCode");
                dt.Columns.Add("Itemname");
                dt.Columns.Add("stc");
                dt.Columns.Add("stcrate");
                dt.Columns.Add("Emb");
                dt.Columns.Add("EmbRate");
                dt.Columns.Add("Print");
                dt.Columns.Add("Printrate");
                dt.Columns.Add("Iron");
                dt.Columns.Add("IronRate");

                ViewState["Data"] = dt;

                string iCusID1 = Request.QueryString.Get("LedgerID");
                #region
                DataSet getitemdistinct = objBs.getitemlistNew();
                if (getitemdistinct.Tables[0].Rows.Count > 0)
                {
                    dCrt = (DataTable)ViewState["Data"];

                    for (int ii = 0; ii < getitemdistinct.Tables[0].Rows.Count; ii++)
                    {
                        #region

                        string itemid = getitemdistinct.Tables[0].Rows[ii]["itemid"].ToString();
                        string itemcode = getitemdistinct.Tables[0].Rows[ii]["itemcode"].ToString();
                        string itemname = getitemdistinct.Tables[0].Rows[ii]["itemname"].ToString();

                        DataRow dr = dCrt.NewRow();
                        dr["Itemid"] = itemid;
                        dr["itemcode"] = itemcode;
                        dr["Itemname"] = itemname;

                        drpitemname.Items.Remove(drpitemname.Items.FindByValue(itemid));

                        DataSet getstcrate = objBs.editCustomerjobrate(iCusID1, itemid, "Stc");
                        if (getstcrate.Tables[0].Rows.Count > 0)
                        {
                            dr["stc"] = "Stc";
                            dr["stcRate"] =Convert.ToDouble(getstcrate.Tables[0].Rows[0]["Rate"]).ToString("f2");

                        }
                        else
                        {
                            dr["stc"] = "Stc";
                            dr["stcRate"] = "0";

                        }

                        DataSet getEmbrate = objBs.editCustomerjobrate(iCusID1, itemid, "Emb");
                        if (getEmbrate.Tables[0].Rows.Count > 0)
                        {
                            dr["Emb"] = "Emb";
                            dr["EmbRate"] = Convert.ToDouble(getEmbrate.Tables[0].Rows[0]["Rate"]).ToString("f2");

                        }
                        else
                        {
                            dr["Emb"] = "Emb";
                            dr["EmbRate"] = "0";

                        }

                        DataSet getPrintrate = objBs.editCustomerjobrate(iCusID1, itemid, "Print");
                        if (getPrintrate.Tables[0].Rows.Count > 0)
                        {
                            dr["Print"] = "Print";
                            dr["PrintRate"] =Convert.ToDouble( getPrintrate.Tables[0].Rows[0]["Rate"]).ToString("f2");

                        }
                        else
                        {
                            dr["Print"] = "Print";
                            dr["PrintRate"] = "0";

                        }

                        DataSet getIronrate = objBs.editCustomerjobrate(iCusID1, itemid, "Iron");
                        if (getIronrate.Tables[0].Rows.Count > 0)
                        {
                            dr["Iron"] = "Iron";
                            dr["IronRate"] = Convert.ToDouble(getIronrate.Tables[0].Rows[0]["Rate"]).ToString("f2");

                        }
                        else
                        {
                            dr["Iron"] = "Iron";
                            dr["IronRate"] = "0";

                        }
                        dCrt.Rows.Add(dr);

                        #endregion

                    }
                    gridprocesstype.DataSource = dCrt;
                    gridprocesstype.DataBind();

                }
                #endregion

               

                DataTable dt1 = new DataTable();


                dt1.Columns.Add("bankname");
                dt1.Columns.Add("bankaccno");
                dt1.Columns.Add("bankdes");
                ViewState["Data1"] = dt1;



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

                DataSet dbraqnch = objBs.griditem();
                if (dbraqnch.Tables[0].Rows.Count > 0)
                {
                    drpitemname.DataSource = dbraqnch.Tables[0];
                    drpitemname.DataTextField = "itemcode";
                    drpitemname.DataValueField = "itemid";
                    drpitemname.DataBind();
                    //drpitemname.Items.Insert(0, "Select Branch");
                }



                DataSet dsagent = objBs.getagenttype();
                if (dsagent.Tables[0].Rows.Count > 0)
                {
                    drpagent.DataSource = dsagent.Tables[0];
                    drpagent.DataTextField = "Ledgername";
                    drpagent.DataValueField = "LedgerID";
                    drpagent.DataBind();
                    drpagent.Items.Insert(0, "Select Agent Name");
                }

                DataSet dsFit = objBs.ProcessMasterHeadingSelect();//tblProcessMaster
                if (dsFit.Tables[0].Rows.Count > 0)
                {
                    ddlHeading.DataSource = dsFit.Tables[0];
                    ddlHeading.DataTextField = "HeadingName";
                    ddlHeading.DataValueField = "ProcessHeadingID";
                    ddlHeading.DataBind();
                    ddlHeading.Items.Insert(0, "Select Process");

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
                        DataSet ds1 = objBs.editCustomerjob(iCusID);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {

                            DataSet dsledger = objBs.getJobWorkid(iCusID);
                            txtgstin.Text = dsledger.Tables[0].Rows[0]["GSTIN"].ToString();

                            btnadd.Text = "Update";
                            Div1.Visible = false;
                            head.InnerText = "Job Work Master";
                            txtCustomerid.Text = ds1.Tables[0].Rows[0]["CustomerId"].ToString();
                            TextBox1.Text = ds1.Tables[0].Rows[0]["Prefix"].ToString();
                            txtcuscode.Text = ds1.Tables[0].Rows[0]["LedgerID"].ToString();
                            txtcustomername.Text = ds1.Tables[0].Rows[0]["LedgerName"].ToString();
                            txtpersonname.Text = ds1.Tables[0].Rows[0]["PersonName"].ToString();

                            txtmobileno.Text = ds1.Tables[0].Rows[0]["MobileNo"].ToString();
                            txtphoneno.Text = ds1.Tables[0].Rows[0]["PhoneNo"].ToString();
                            txtarea.Text = ds1.Tables[0].Rows[0]["Area"].ToString();
                            txtaddress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();
                            txtcity.Text = ds1.Tables[0].Rows[0]["City"].ToString();
                            txtpincode.Text = ds1.Tables[0].Rows[0]["Pincode"].ToString();
                            txtemail.Text = ds1.Tables[0].Rows[0]["Email"].ToString();
                            ddlHeading.SelectedValue = ds1.Tables[0].Rows[0]["HeadingId"].ToString();

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
                            drpagent.SelectedValue = ds1.Tables[0].Rows[0]["agentid"].ToString();
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

                         
                            ddlIsActive.SelectedItem.Text = ds1.Tables[0].Rows[0]["IsActive"].ToString();


                           ////// DataSet getitemdistinct = objBs.getitemlistNew(iCusID);

                            //////DataSet getitemdistinct = objBs.getitemlistNew();
                            //////if (getitemdistinct.Tables[0].Rows.Count > 0)
                            //////{
                            //////    dCrt = (DataTable)ViewState["Data"];

                            //////    for (int ii = 0; ii < getitemdistinct.Tables[0].Rows.Count; ii++)
                            //////    {

                            //////        string itemid = getitemdistinct.Tables[0].Rows[ii]["itemid"].ToString();
                            //////        string itemcode = getitemdistinct.Tables[0].Rows[ii]["itemcode"].ToString();
                            //////        string itemname = getitemdistinct.Tables[0].Rows[ii]["itemname"].ToString();
                                  
                            //////            DataRow dr = dCrt.NewRow();
                            //////            dr["Itemid"] = itemid;
                            //////            dr["itemcode"] = itemcode;
                            //////            dr["Itemname"] = itemname;
                                    
                            //////            drpitemname.Items.Remove(drpitemname.Items.FindByValue(itemid));

                            //////            DataSet getstcrate = objBs.editCustomerjobrate(iCusID, itemid, "Stc");
                            //////            if (getstcrate.Tables[0].Rows.Count >0)
                            //////            {
                            //////                dr["stc"] = "Stc";
                            //////                dr["stcRate"] = getstcrate.Tables[0].Rows[0]["Rate"].ToString();
                                          
                            //////            }
                            //////            else
                            //////            {
                            //////                dr["stc"] = "Stc";
                            //////                dr["stcRate"] = "0";
                                          
                            //////            }

                            //////            DataSet getEmbrate = objBs.editCustomerjobrate(iCusID, itemid, "Emb");
                            //////            if (getEmbrate.Tables[0].Rows.Count > 0)
                            //////            {
                            //////                dr["Emb"] = "Emb";
                            //////                dr["EmbRate"] = getEmbrate.Tables[0].Rows[0]["Rate"].ToString();
                                           
                            //////            }
                            //////            else
                            //////            {
                            //////                dr["Emb"] = "Emb";
                            //////                dr["EmbRate"] = "0";
                                           
                            //////            }

                            //////            DataSet getPrintrate = objBs.editCustomerjobrate(iCusID, itemid, "Print");
                            //////            if (getPrintrate.Tables[0].Rows.Count > 0)
                            //////            {
                            //////                dr["Print"] = "Print";
                            //////                dr["PrintRate"] = getPrintrate.Tables[0].Rows[0]["Rate"].ToString();
                                          
                            //////            }
                            //////            else
                            //////            {
                            //////                dr["Print"] = "Print";
                            //////                dr["PrintRate"] = "0";
                                           
                            //////            }

                            //////            DataSet getIronrate = objBs.editCustomerjobrate(iCusID, itemid, "Iron");
                            //////            if (getIronrate.Tables[0].Rows.Count > 0)
                            //////            {
                            //////                dr["Iron"] = "Iron";
                            //////                dr["IronRate"] = getIronrate.Tables[0].Rows[0]["Rate"].ToString();
                                          
                            //////            }
                            //////            else
                            //////            {
                            //////                dr["Iron"] = "Iron";
                            //////                dr["IronRate"] = "0";
                                           
                            //////            }
                            //////            dCrt.Rows.Add(dr);

                            //////    }
                            //////    gridprocesstype.DataSource = dCrt;
                            //////    gridprocesstype.DataBind();

                            //////}

                            DataSet getbankdetails = objBs.getbankdetails(iCusID);
                            if (getbankdetails.Tables[0].Rows.Count > 0)
                            {
                                dCrt1 = (DataTable)ViewState["Data1"];
                                for (int ii = 0; ii < getbankdetails.Tables[0].Rows.Count; ii++)
                                {
                                    string bankname = getbankdetails.Tables[0].Rows[ii]["BankName"].ToString();
                                    string bankaccno = getbankdetails.Tables[0].Rows[ii]["BankAccno"].ToString();
                                    string bankdes = getbankdetails.Tables[0].Rows[ii]["BankDes"].ToString();

                                    DataRow dr = dCrt1.NewRow();
                                    dr["bankname"] = bankname;
                                    dr["bankaccno"] = bankaccno;
                                    dr["bankdes"] = bankdes;

                                    dCrt1.Rows.Add(dr);
                                }
                                gridbank.DataSource = dCrt1;
                                gridbank.DataBind();
                            }


                        }

                    }
                }

            }
        }

        protected void process_click(object sender, EventArgs e)
        {

            for (int vLoop = 0; vLoop < gridprocesstype.Rows.Count; vLoop++)
            {

                Label lblitemid = (Label)gridprocesstype.Rows[vLoop].FindControl("lblitemid");

                if (lblitemid.Text == drpitemname.SelectedValue)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Item Name Already Exists.Thanks You!!!');", true);
                    return;
                }
            }
            dCrt = (DataTable)ViewState["Data"];

            DataRow dr = dCrt.NewRow();


            dr["Itemid"] = drpitemname.SelectedValue;
            dr["Itemname"] = drpitemname.SelectedItem.Text;
            dr["stc"] = "Stc";

            dr["stcRate"] = txtstc.Text;
            dr["Emb"] = "Emb";
            dr["EmbRate"] = txtemb.Text;

            dr["Print"] = "Print";
            dr["PrintRate"] = txtprint.Text;
            dr["Iron"] = "Iron";
            dr["IronRate"] = txtiron.Text;
            dCrt.Rows.Add(dr);

            gridprocesstype.DataSource = dCrt;
            gridprocesstype.DataBind();
        }

        protected void processbankclick_click(object sender, EventArgs e)
        {

            for (int vLoop = 0; vLoop < gridbank.Rows.Count; vLoop++)
            {

                Label lblitemid = (Label)gridbank.Rows[vLoop].FindControl("lblbankname");
                Label lblbankaccno = (Label)gridbank.Rows[vLoop].FindControl("lblbankaccno");

                if (lblitemid.Text == txtbankname.Text && lblbankaccno.Text == txtbankaccno.Text)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Bank Name Already Exists.Thanks You!!!');", true);
                    return;
                }
            }
            dCrt1 = (DataTable)ViewState["Data1"];

            DataRow dr = dCrt1.NewRow();


            dr["bankname"] = txtbankname.Text;
            dr["bankaccno"] = txtbankaccno.Text;
            dr["bankdes"] = txtbankdesc.Text;

           
            dCrt1.Rows.Add(dr);

            gridbank.DataSource = dCrt1;
            gridbank.DataBind();
        }

        protected void gridbank_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            if (ViewState["Data1"] != null)
            {
                DataSet ds = new DataSet();
                DataTable dt = (DataTable)ViewState["Data1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    ds.Merge(dt);


                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();

                    ViewState["Data1"] = dt;
                    gridbank.DataSource = dt;
                    gridbank.DataBind();

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Data1"] = dt;
                    gridbank.DataSource = dt;
                    gridbank.DataBind();
                }
            }

        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {


            if (ViewState["Data"] != null)
            {
                DataSet ds = new DataSet();
                DataTable dt = (DataTable)ViewState["Data"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    ds.Merge(dt);


                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();

                    ViewState["Data"] = dt;
                    gridprocesstype.DataSource = dt;
                    gridprocesstype.DataBind();

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["Data"] = dt;
                    gridprocesstype.DataSource = dt;
                    gridprocesstype.DataBind();
                }
            }

        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string openingbalance = txtOBalance.Text;
            int Grupid;
            string Tinno = "0";
            if (drpagent.SelectedValue == "Select Agent Name")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please select Agent Name...Thanks You!!!');", true);
                return;

            }
            ddlCustomerType.SelectedItem.Text = "Vendor";
            if (ddlCustomerType.SelectedItem.Text == "Vendor")
            {
                Grupid = 2;

            }
            else
            {
                Grupid = 1;

            }
            Tinno = txtTinNO.Text;
            DateTime date = DateTime.ParseExact(txtdob.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dat = DateTime.ParseExact(txtAnniversary.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnadd.Text == "Save")
            {
                if (ddlCDType.SelectedValue == "Credit Note")
                {
                    string Credite = txtOBalance.Text;

                    int i = objBs.insertJobwork(txtpersonname.Text,txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Grupid, ddlIsActive.SelectedValue, Convert.ToDouble(Credite), Convert.ToDouble("0"), ddlCDType.SelectedValue, Convert.ToInt32("10"), Convert.ToInt32(txtCustomerid.Text), Tinno, "tblAuditMaster_" + sTableName, lblUser.Text, "Vendor", ddlCDType.SelectedItem.Text, openingbalance, txtCustomerid.Text, TextBox1.Text, txtCST.Text, txtPAN.Text, txtDesignation.Text, txtDelivery.Text, 0, 0, 0, Convert.ToInt32(ddlPrice.SelectedValue), date, dat, Convert.ToInt32(txtCreditDays.Text), drpagent.SelectedValue, ddlHeading.SelectedValue, txtgstin.Text);
                   
                }
                else
                {
                    string Debit = txtOBalance.Text;

                    int j = objBs.insertJobwork(txtpersonname.Text, txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Grupid, ddlIsActive.SelectedValue, Convert.ToDouble("0"), Convert.ToDouble(Debit), ddlCDType.SelectedValue, Convert.ToInt32("10"), Convert.ToInt32(txtCustomerid.Text), Tinno, "tblAuditMaster_" + sTableName, lblUser.Text, "Vendor", ddlCDType.SelectedItem.Text, openingbalance, txtCustomerid.Text, TextBox1.Text, txtCST.Text, txtPAN.Text, txtDesignation.Text, txtDelivery.Text, 0, 0, 0, Convert.ToInt32(ddlPrice.SelectedValue), date, dat, Convert.ToInt32(txtCreditDays.Text), drpagent.SelectedValue, ddlHeading.SelectedValue, txtgstin.Text);
                    
                }
                int iscc = 0;
                for (int vLoop1 = 0; vLoop1 < gridprocesstype.Rows.Count; vLoop1++)
                {
                    Label lblitemid = (Label)gridprocesstype.Rows[vLoop1].FindControl("lblitemid");
                   
                    TextBox lblstcrate = (TextBox)gridprocesstype.Rows[vLoop1].FindControl("lblstcrate");
                    if (lblstcrate.Text != "0" || lblstcrate.Text != "" || lblstcrate.Text != null)
                    {
                        iscc = objBs.insertTransJobworkRate(lblitemid.Text, lblstcrate.Text, "Stc");
                    }
                    TextBox lblEmbrate = (TextBox)gridprocesstype.Rows[vLoop1].FindControl("lblEmbrate");
                    if (lblEmbrate.Text != "0" || lblEmbrate.Text != "" || lblEmbrate.Text != null)
                    {
                        iscc = objBs.insertTransJobworkRate(lblitemid.Text, lblEmbrate.Text, "Emb");
                    }

                    TextBox lblPrintrate = (TextBox)gridprocesstype.Rows[vLoop1].FindControl("lblPrintrate");
                    if (lblPrintrate.Text != "0" || lblPrintrate.Text != "" || lblPrintrate.Text != null)
                    {
                        iscc = objBs.insertTransJobworkRate(lblitemid.Text, lblPrintrate.Text, "Print");
                    }

                    TextBox lblIronrate = (TextBox)gridprocesstype.Rows[vLoop1].FindControl("lblIronrate");
                    if (lblIronrate.Text != "0" || lblIronrate.Text != "" || lblIronrate.Text != null)
                    {
                        iscc = objBs.insertTransJobworkRate(lblitemid.Text, lblIronrate.Text, "Iron");
                    }
                }

                for (int vLoop1 = 0; vLoop1 < gridbank.Rows.Count; vLoop1++)
                {
                    Label lblbankname = (Label)gridbank.Rows[vLoop1].FindControl("lblbankname");
                    Label lblbankaccno = (Label)gridbank.Rows[vLoop1].FindControl("lblbankaccno");
                    Label lblbankdes = (Label)gridbank.Rows[vLoop1].FindControl("lblbankdes");


                    if (lblbankname.Text != "0" || lblbankname.Text != "" || lblbankname.Text != null)
                    {
                        iscc = objBs.insertTransJobworkerbank(lblbankname.Text,lblbankaccno.Text,lblbankdes.Text);
                    }
                }

                Response.Redirect("../Accountsbootstrap/viewjobwork.aspx");
            }
            else
            {
                string iCusID = Request.QueryString.Get("LedgerID");
                if (ddlCDType.SelectedValue == "Credit Note")
                {
                    string Credite = txtOBalance.Text;

                    int iStatus = objBs.updateJobcustomer(txtpersonname.Text, Convert.ToInt32(iCusID), txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Convert.ToInt32("6"), ddlIsActive.SelectedItem.Text, Convert.ToDouble(Credite), Convert.ToDouble("0"), ddlCDType.SelectedValue, Convert.ToInt32(txtCustomerid.Text), Tinno, "tblAuditMaster_" + sTableName, lblUser.Text, "Vendor", ddlCDType.SelectedItem.Text, openingbalance, txtCustomerid.Text, TextBox1.Text, txtCST.Text, txtPAN.Text, txtDesignation.Text, txtDelivery.Text, 0, 0, 0, Convert.ToInt32(ddlPrice.SelectedValue), date, dat, Convert.ToInt32(txtCreditDays.Text), drpagent.SelectedValue, ddlHeading.SelectedValue, txtgstin.Text);
                   // Response.Redirect("../Accountsbootstrap/viewjobwork.aspx");
                }
                else
                {
                    string Debit = txtOBalance.Text;
                    int iStatus = objBs.updateJobcustomer(txtpersonname.Text, Convert.ToInt32(iCusID), txtcustomername.Text, txtmobileno.Text, txtphoneno.Text, txtarea.Text, txtaddress.Text, txtcity.Text, txtpincode.Text, txtemail.Text, Convert.ToInt32("6"), ddlIsActive.SelectedItem.Text, Convert.ToDouble("0"), Convert.ToDouble(Debit), ddlCDType.SelectedValue, Convert.ToInt32(txtCustomerid.Text), Tinno, "tblAuditMaster_" + sTableName, lblUser.Text, "Vendor", ddlCDType.SelectedItem.Text, openingbalance, txtCustomerid.Text, TextBox1.Text, txtCST.Text, txtPAN.Text, txtDesignation.Text, txtDelivery.Text, 0, 0, 0, Convert.ToInt32(ddlPrice.SelectedValue), date, dat, Convert.ToInt32(txtCreditDays.Text), drpagent.SelectedValue, ddlHeading.SelectedValue, txtgstin.Text);
                    
                }

                int idelete = objBs.deletejobworkrate(iCusID);

                int idelete1 = objBs.deletejobworkerbank(iCusID);


                int iscc = 0;
                for (int vLoop1 = 0; vLoop1 < gridprocesstype.Rows.Count; vLoop1++)
                {
                    Label lblitemid = (Label)gridprocesstype.Rows[vLoop1].FindControl("lblitemid");
                    //Label  = (Label)gridprocesstype.Rows[vLoop1].FindControl("");

                    // Label = (Label)gridprocesstype.Rows[vLoop1].FindControl("");
                    TextBox lblstcrate = (TextBox)gridprocesstype.Rows[vLoop1].FindControl("lblstcrate");
                    if (lblstcrate.Text != "0" || lblstcrate.Text != "" || lblstcrate.Text != null)
                    {
                        iscc = objBs.UpdateTransJobworkRate(lblitemid.Text, lblstcrate.Text, "Stc",iCusID);
                    }
                    TextBox lblEmbrate = (TextBox)gridprocesstype.Rows[vLoop1].FindControl("lblEmbrate");
                    if (lblEmbrate.Text != "0" || lblEmbrate.Text != "" || lblEmbrate.Text != null)
                    {
                        iscc = objBs.UpdateTransJobworkRate(lblitemid.Text, lblEmbrate.Text, "Emb", iCusID);
                    }

                    TextBox lblPrintrate = (TextBox)gridprocesstype.Rows[vLoop1].FindControl("lblPrintrate");
                    if (lblPrintrate.Text != "0" || lblPrintrate.Text != "" || lblPrintrate.Text != null)
                    {
                        iscc = objBs.UpdateTransJobworkRate(lblitemid.Text, lblPrintrate.Text, "Print", iCusID);
                    }

                    TextBox lblIronrate = (TextBox)gridprocesstype.Rows[vLoop1].FindControl("lblIronrate");
                    if (lblIronrate.Text != "0" || lblIronrate.Text != "" || lblIronrate.Text != null)
                    {
                        iscc = objBs.UpdateTransJobworkRate(lblitemid.Text, lblIronrate.Text, "Iron", iCusID);
                    }
                }

                for (int vLoop1 = 0; vLoop1 < gridbank.Rows.Count; vLoop1++)
                {
                    Label lblbankname = (Label)gridbank.Rows[vLoop1].FindControl("lblbankname");
                    Label lblbankaccno = (Label)gridbank.Rows[vLoop1].FindControl("lblbankaccno");
                    Label lblbankdes = (Label)gridbank.Rows[vLoop1].FindControl("lblbankdes");


                    if (lblbankname.Text != "0" || lblbankname.Text != "" || lblbankname.Text != null)
                    {
                        iscc = objBs.UpdateTransJobworkerbank(lblbankname.Text, lblbankaccno.Text, lblbankdes.Text,iCusID);
                    }
                }

                Response.Redirect("../Accountsbootstrap/viewjobwork.aspx");
            }

            System.Threading.Thread.Sleep(3000);
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/ViewJobWork.aspx");
        }

        protected void txtcustomername_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = objBs.IsExistJobWorkName(txtcustomername.Text);
            if ((ds.Tables[0].Rows.Count) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This Customer Name is all ready exist!');", true);
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
                dgGrid.HeaderStyle.Font.Bold = true;
                dgGrid.RenderControl(hw);
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