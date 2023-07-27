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
using System.Text.RegularExpressions;
using System.Net;
using System.Globalization;


namespace Billing.Accountsbootstrap
{
    public partial class PartyMaster : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();

        string sTableName = "";
        string leadid = "0";
        string stus = "0";
        string nametype = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            nametype = Request.QueryString.Get("Name");


            string name = Request.QueryString.Get("name");

            if (nametype == "LEAD")
            {
                leadid = Request.QueryString.Get("leadid");
                stus = Request.QueryString.Get("status");
            }
            else
            {
                leadid = "0";
            }


            if (!IsPostBack)
            {
                #region

                DataSet dsGroup = objBs.getselectHeadingall();
                if (dsGroup.Tables[0].Rows.Count > 0)
                {
                    ddlGroup.DataSource = dsGroup.Tables[0];
                    ddlGroup.DataTextField = "GroupName";
                    ddlGroup.DataValueField = "GroupID";
                    ddlGroup.DataBind();
                }

                DataSet dsContactType = objBs.gridPartyType();
                if (dsContactType.Tables[0].Rows.Count > 0)
                {
                    ddlPartyType.DataSource = dsContactType.Tables[0];
                    ddlPartyType.DataTextField = "PartyType";
                    ddlPartyType.DataValueField = "PartyTypeID";
                    ddlPartyType.DataBind();
                    ddlPartyType.Items.Insert(0, "Select PartyType");
                }

                DataSet dsset = objBs.getLedger("9");
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    drpfollowedby.DataSource = dsset.Tables[0];
                    drpfollowedby.DataTextField = "LEdgerName";
                    drpfollowedby.DataValueField = "LedgerID";
                    drpfollowedby.DataBind();
                    drpfollowedby.Items.Insert(0, "Select FollowedBy");
                }

                DataSet dstate = objBs.GetAllCountry();
                if (dstate.Tables[0].Rows.Count > 0)
                {
                    ddlCountry.DataSource = dstate.Tables[0];
                    ddlCountry.DataTextField = "Name";
                    ddlCountry.DataValueField = "ID";
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, "Select Country");
                }


                DataSet dsCurrency = objBs.gridCurrency();
                if (dsCurrency.Tables[0].Rows.Count > 0)
                {
                    ddlCurrency.DataSource = dsCurrency.Tables[0];
                    ddlCurrency.DataTextField = "CurrencyName";
                    ddlCurrency.DataValueField = "CurrencyId";
                    ddlCurrency.DataBind();
                    ddlCurrency.Items.Insert(0, "Select CurrencyName");
                }

                DataSet dsShipmentMode = objBs.GetShipmentMode();
                if (dsShipmentMode.Tables[0].Rows.Count > 0)
                {
                    ddlShipmentMode.DataSource = dsShipmentMode.Tables[0];
                    ddlShipmentMode.DataTextField = "ShipmentMode";
                    ddlShipmentMode.DataValueField = "ShipmentId";
                    ddlShipmentMode.DataBind();
                }

                #endregion

                ddlState.Items.Insert(0, "Select State");
                ddlCity.Items.Insert(0, "Select City");


                FirstGridViewRow1();

                string LedgerID = Request.QueryString.Get("LedgerID");
                if (LedgerID != "" && LedgerID != null)
                {
                    DataSet ds = objBs.GetLedgerCheck(Convert.ToInt32(LedgerID));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        #region

                        ddlCountry.SelectedValue = ds.Tables[0].Rows[0]["CountryId"].ToString();
                        DataSet dsDistrict = objBs.GetAllCountryState(Convert.ToInt32(ddlCountry.SelectedValue));
                        if (dsDistrict.Tables[0].Rows.Count > 0)
                        {

                            ddlState.DataSource = dsDistrict.Tables[0];
                            ddlState.DataTextField = "State";
                            ddlState.DataValueField = "ID";
                            ddlState.DataBind();
                            ddlState.Items.Insert(0, "Select State");
                        }
                        else
                        {
                            ddlState.Items.Clear();
                            ddlState.Items.Insert(0, "Select State");
                            ddlCity.Items.Clear();
                            ddlCity.Items.Insert(0, "Select City");
                        }

                        ddlState.SelectedValue = ds.Tables[0].Rows[0]["StateId"].ToString();
                        DataSet dsCity = objBs.GetAllStateCity(Convert.ToInt32(ddlState.SelectedValue));
                        if (dsCity.Tables[0].Rows.Count > 0)
                        {
                            ddlCity.DataSource = dsCity.Tables[0];
                            ddlCity.DataTextField = "City";
                            ddlCity.DataValueField = "ID";
                            ddlCity.DataBind();
                            ddlCity.Items.Insert(0, "Select City");
                        }
                        else
                        {
                            ddlCity.Items.Clear();
                            ddlCity.Items.Insert(0, "Select City");
                        }

                        ddlPartyType.SelectedValue = ds.Tables[0].Rows[0]["ContactTypeID"].ToString();
                        ddlPartyType.Enabled = false;


                        drpfollowedby.SelectedValue = ds.Tables[0].Rows[0]["Followedby"].ToString();

                        txtCompanyName.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                        txtCompanyCode.Text = ds.Tables[0].Rows[0]["CompanyCode"].ToString();
                        txtPhone.Text = ds.Tables[0].Rows[0]["PhoneNo"].ToString();
                        txtMobile.Text = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        txtaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                        ddlGroup.SelectedValue = ds.Tables[0].Rows[0]["GroupID"].ToString();
                        ddlCity.SelectedValue = ds.Tables[0].Rows[0]["CityID"].ToString();

                        txtpincode.Text = ds.Tables[0].Rows[0]["Pincode"].ToString();
                        txtFax.Text = ds.Tables[0].Rows[0]["Fax"].ToString();
                        txtemail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
                        txtWebSite.Text = ds.Tables[0].Rows[0]["WebSite"].ToString();

                        txtPaymentMode.Text = ds.Tables[0].Rows[0]["PaymentMode"].ToString();
                        txtTerms.Text = ds.Tables[0].Rows[0]["Terms"].ToString();
                        txtDays.Text = ds.Tables[0].Rows[0]["Days"].ToString();
                        txtNatureofContract.Text = ds.Tables[0].Rows[0]["NatureofContract"].ToString();
                        txtStrength.Text = ds.Tables[0].Rows[0]["Strength"].ToString();
                        txtFinalDestination.Text = ds.Tables[0].Rows[0]["FinalDestination"].ToString();
                        ddlShipmentMode.SelectedValue = ds.Tables[0].Rows[0]["ShipmentMode"].ToString();
                        txtACCode.Text = ds.Tables[0].Rows[0]["ACCode"].ToString();

                        ddlIsActive.SelectedValue = ds.Tables[0].Rows[0]["IsActive"].ToString();
                        ddlIsForeign.SelectedValue = ds.Tables[0].Rows[0]["IsForeign"].ToString();
                        ddlCurrency.SelectedValue = ds.Tables[0].Rows[0]["CurrencyId"].ToString();
                        ddlProvince.SelectedValue = ds.Tables[0].Rows[0]["Province"].ToString();
                        txtcontactpersonname.Text = ds.Tables[0].Rows[0]["ContacrPerson"].ToString();

                        btnadd.Text = "Update";

                        #endregion

                        DataSet ds2 = objBs.GetLedgerPersons(Convert.ToInt32(LedgerID));
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            #region

                            DataTable dtt;
                            DataRow drNew;
                            DataColumn dct;
                            DataSet dstd = new DataSet();
                            dtt = new DataTable();

                            dct = new DataColumn("PersonName");
                            dtt.Columns.Add(dct);
                            dct = new DataColumn("eMail1");
                            dtt.Columns.Add(dct);

                            dct = new DataColumn("Phone1");
                            dtt.Columns.Add(dct);
                            dct = new DataColumn("Phone2");
                            dtt.Columns.Add(dct);
                            dct = new DataColumn("Mobile1");
                            dtt.Columns.Add(dct);

                            dstd.Tables.Add(dtt);

                            foreach (DataRow Dr in ds2.Tables[0].Rows)
                            {
                                drNew = dtt.NewRow();

                                drNew["PersonName"] = Dr["PersonName"];
                                drNew["eMail1"] = Dr["EMail"];

                                drNew["Phone1"] = Dr["Phone1"];
                                drNew["Phone2"] = Dr["Phone2"];
                                drNew["Mobile1"] = Dr["Mobile"];

                                dstd.Tables[0].Rows.Add(drNew);
                            }


                            ViewState["CurrentTable1"] = dtt;

                            GVPerson.DataSource = dstd;
                            GVPerson.DataBind();

                            for (int vLoop = 0; vLoop < GVPerson.Rows.Count; vLoop++)
                            {
                                TextBox txtPersonName = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPersonName");
                                TextBox txteMail1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txteMail1");

                                TextBox txtPhone1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPhone1");
                                TextBox txtPhone2 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPhone2");
                                TextBox txtMobile1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtMobile1");

                                txtPersonName.Text = dstd.Tables[0].Rows[vLoop]["PersonName"].ToString();
                                txteMail1.Text = dstd.Tables[0].Rows[vLoop]["eMail1"].ToString();

                                txtPhone1.Text = dstd.Tables[0].Rows[vLoop]["Phone1"].ToString();
                                txtPhone2.Text = dstd.Tables[0].Rows[vLoop]["Phone2"].ToString();
                                txtMobile1.Text = dstd.Tables[0].Rows[vLoop]["Mobile1"].ToString();
                            }

                            #endregion
                        }
                    }

                }
                else
                {
                    if (leadid == "0")
                    {
                    }
                    else
                    {
                        // get DATA FROMLEAD
                        DataSet getlead = objBs.GetEntryforupdate(leadid);
                        if (getlead.Tables[0].Rows.Count > 0)
                        {
                            txtCompanyName.Text = getlead.Tables[0].Rows[0]["Companyname"].ToString();
                            txtMobile.Text = getlead.Tables[0].Rows[0]["primarycontact"].ToString();
                            txtPhone.Text = getlead.Tables[0].Rows[0]["secondarycontact"].ToString();
                            txtaddress.Text = getlead.Tables[0].Rows[0]["address"].ToString();
                            txtcontactpersonname.Text = getlead.Tables[0].Rows[0]["contactname1"].ToString();
                            //txtSecondary_Contact.Text = getlead.Tables[0].Rows[0]["contactname2"].ToString();
                           // txtDesignation.Text = getlead.Tables[0].Rows[0]["Designation"].ToString();
                            txtemail.Text = getlead.Tables[0].Rows[0]["emailid"].ToString();
                        }

                    }
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void Party_chnaged(object sender, EventArgs e)
        {

            if (ddlPartyType.SelectedValue == "Select PartyType")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Party Type.');", true);
                ddlPartyType.Focus();
                return;
            }

            // check Party Type Need Approval or not
            DataSet dcheck = objBs.checkapprovallist(ddlPartyType.SelectedValue);
            if (dcheck.Tables[0].Rows.Count > 0)
            {
                ddlGroup.SelectedValue = dcheck.Tables[0].Rows[0]["groupid"].ToString();
                string partytype = dcheck.Tables[0].Rows[0]["ManagementApproval"].ToString();
                lblpartytype.Text = partytype;
                if (partytype == "Y")
                {
                    lblblinktext.Text = "Management Approval Needed.Please Fill All Mandatory Details";
                }
                else
                {
                    lblblinktext.Text = "";
                }

            }


        }

        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            int Row = 1;
            for (int vLoop = 0; vLoop < GVPerson.Rows.Count; vLoop++)
            {
                TextBox txtPersonName = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPersonName");
                TextBox txteMail1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txteMail1");

                TextBox txtPhone1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPhone1");
                TextBox txtPhone2 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPhone2");
                TextBox txtMobile1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtMobile1");


                if (txtPhone1.Text == "")
                    txtPhone1.Text = "0";
                if (txtPhone2.Text == "")
                    txtPhone2.Text = "0";
                if (txtMobile1.Text == "")
                    txtMobile1.Text = "0";



                //if (txtPersonName.Text == "" || txtPersonName.Text == "0" || txtPersonName.Text == "Select PersonName")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select PersonName in Row " + Row + " ')", true);
                //    txtPersonName.Focus();
                //    return;
                //}
                //if (txteMail1.Text == "" || txteMail1.Text == "0" || txteMail1.Text == "Select eMail1")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select eMail1 in Row " + Row + " ')", true);
                //    txtPersonName.Focus();
                //    return;
                //}

                //if (Convert.ToDouble(txtPhone1.Text) == 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Phone1 in Row " + Row + " ')", true);
                //    txtPhone1.Focus();
                //    return;
                //}
                //if (Convert.ToDouble(txtPhone2.Text) == 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Phone2 in Row " + Row + " ')", true);
                //    txtPhone2.Focus();
                //    return;
                //}
                //if (Convert.ToDouble(txtMobile1.Text) == 0)
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Mobile1 in Row " + Row + " ')", true);
                //    txtMobile1.Focus();
                //    return;
                //}
                Row++;
            }
            AddNewRow1();
        }
        private void FirstGridViewRow1()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("PersonName", typeof(string)));
            dt.Columns.Add(new DataColumn("eMail1", typeof(string)));
            dt.Columns.Add(new DataColumn("Phone1", typeof(string)));
            dt.Columns.Add(new DataColumn("Phone2", typeof(string)));
            dt.Columns.Add(new DataColumn("Mobile1", typeof(string)));

            dr = dt.NewRow();
            dr["PersonName"] = string.Empty;
            dr["eMail1"] = string.Empty;
            dr["Phone1"] = string.Empty;
            dr["Phone2"] = string.Empty;
            dr["Mobile1"] = string.Empty;

            dt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dt;

            GVPerson.DataSource = dt;
            GVPerson.DataBind();

            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("PersonName");
            dtt.Columns.Add(dct);
            dct = new DataColumn("eMail1");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Phone1");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Phone2");
            dtt.Columns.Add(dct);
            dct = new DataColumn("Mobile1");
            dtt.Columns.Add(dct);

            dstd.Tables.Add(dtt);

            drNew = dtt.NewRow();
            drNew["PersonName"] = 0;
            drNew["eMail1"] = "";

            drNew["Phone1"] = "";
            drNew["Phone2"] = "";
            drNew["Mobile1"] = "";

            dstd.Tables[0].Rows.Add(drNew);

            GVPerson.DataSource = dstd;
            GVPerson.DataBind();

        }
        private void AddNewRow1()
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
                        TextBox txtPersonName = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txtPersonName");
                        TextBox txteMail1 = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txteMail1");

                        TextBox txtPhone1 = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txtPhone1");
                        TextBox txtPhone2 = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txtPhone2");
                        TextBox txtMobile1 = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txtMobile1");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["PersonName"] = txtPersonName.Text;
                        dtCurrentTable.Rows[i - 1]["eMail1"] = txteMail1.Text;

                        dtCurrentTable.Rows[i - 1]["Phone1"] = txtPhone1.Text;
                        dtCurrentTable.Rows[i - 1]["Phone2"] = txtPhone2.Text;
                        dtCurrentTable.Rows[i - 1]["Mobile1"] = txtMobile1.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    GVPerson.DataSource = dtCurrentTable;
                    GVPerson.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData1();

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
                        TextBox txtPersonName = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txtPersonName");
                        TextBox txteMail1 = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txteMail1");

                        TextBox txtPhone1 = (TextBox)GVPerson.Rows[rowIndex].Cells[3].FindControl("txtPhone1");
                        TextBox txtPhone2 = (TextBox)GVPerson.Rows[rowIndex].Cells[3].FindControl("txtPhone2");
                        TextBox txtMobile1 = (TextBox)GVPerson.Rows[rowIndex].Cells[3].FindControl("txtMobile1");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["PersonName"] = txtPersonName.Text;
                        dtCurrentTable.Rows[i - 1]["eMail1"] = txteMail1.Text;

                        dtCurrentTable.Rows[i - 1]["Phone1"] = txtPhone1.Text;
                        dtCurrentTable.Rows[i - 1]["Phone2"] = txtPhone2.Text;
                        dtCurrentTable.Rows[i - 1]["Mobile1"] = txtMobile1.Text;

                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    GVPerson.DataSource = dtCurrentTable;
                    GVPerson.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData1();
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
                        TextBox txtPersonName = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txtPersonName");
                        TextBox txteMail1 = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txteMail1");

                        TextBox txtPhone1 = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txtPhone1");
                        TextBox txtPhone2 = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txtPhone2");
                        TextBox txtMobile1 = (TextBox)GVPerson.Rows[rowIndex].Cells[1].FindControl("txtMobile1");

                        txtPersonName.Text = dt.Rows[i]["PersonName"].ToString();
                        txteMail1.Text = dt.Rows[i]["eMail1"].ToString();

                        txtPhone1.Text = dt.Rows[i]["Phone1"].ToString();
                        txtPhone2.Text = dt.Rows[i]["Phone2"].ToString();
                        txtMobile1.Text = dt.Rows[i]["Mobile1"].ToString();

                        rowIndex++;

                    }
                }
            }
        }
        protected void GVPerson_RowDeleting(object sender, GridViewDeleteEventArgs e)
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
                    GVPerson.DataSource = dt;
                    GVPerson.DataBind();

                    SetPreviousData1();

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    GVPerson.DataSource = dt;
                    GVPerson.DataBind();

                    SetPreviousData1();
                    FirstGridViewRow1();
                }
            }
        }

        protected void ddlCountry_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlState.Items.Clear();
            ddlState.Items.Insert(0, "Select State");
            ddlCity.Items.Clear();
            ddlCity.Items.Insert(0, "Select City");

            if (ddlCountry.SelectedValue == "" || ddlCountry.SelectedValue == "0" || ddlCountry.SelectedValue == "Select Country")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Country.')", true);
                ddlCountry.Focus();

                ddlState.Items.Clear();
                ddlState.Items.Insert(0, "Select State");
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, "Select City");

                return;
            }

            DataSet dsDistrict = objBs.GetAllCountryState(Convert.ToInt32(ddlCountry.SelectedValue));
            if (dsDistrict.Tables[0].Rows.Count > 0)
            {

                ddlState.DataSource = dsDistrict.Tables[0];
                ddlState.DataTextField = "State";
                ddlState.DataValueField = "ID";
                ddlState.DataBind();
                ddlState.Items.Insert(0, "Select State");
            }
            else
            {
                ddlState.Items.Clear();
                ddlState.Items.Insert(0, "Select State");
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, "Select City");
            }

        }
        protected void ddlState_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlState.SelectedValue == "" || ddlState.SelectedValue == "0" || ddlState.SelectedValue == "Select State")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select State.')", true);
                ddlState.Focus();

                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, "Select City");

                return;
            }


            DataSet dsCity = objBs.GetAllStateCity(Convert.ToInt32(ddlState.SelectedValue));
            if (dsCity.Tables[0].Rows.Count > 0)
            {
                ddlCity.DataSource = dsCity.Tables[0];
                ddlCity.DataTextField = "City";
                ddlCity.DataValueField = "ID";
                ddlCity.DataBind();
                ddlCity.Items.Insert(0, "Select City");
            }
            else
            {
                ddlCity.Items.Clear();
                ddlCity.Items.Insert(0, "Select City");
            }
        }

        protected void chkDistrict_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDistrict.Checked == true)
            {
                ddlState.Visible = false;
                txtstate.Visible = true;


                CompareValidator3.EnableClientScript = false;

                chkCity.Checked = true;
                chkCity_CheckedChanged(sender, e);
                chkCity.Enabled = false;

                txtstate.Focus();

            }
            else
            {
                ddlState.Visible = true;
                txtstate.Visible = false;


                CompareValidator3.EnableClientScript = true;

                chkCity.Checked = false;
                chkCity_CheckedChanged(sender, e);
                chkCity.Enabled = true;

                ddlState.Focus();
            }

        }
        protected void chkCity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCity.Checked == true)
            {
                ddlCity.Visible = false;
                txtcity.Visible = true;


                CompareValidator5.EnableClientScript = false;
                RequiredFieldValidator4.EnableClientScript = true;

                txtcity.Focus();
            }
            else
            {
                ddlCity.Visible = true;
                txtcity.Visible = false;


                CompareValidator5.EnableClientScript = true;
                RequiredFieldValidator4.EnableClientScript = false;

                ddlCity.Focus();
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string LedgerID = Request.QueryString.Get("LedgerID");

            string your_String = txtCompanyName.Text;
            string my_String = Regex.Replace(your_String, @"[^0-9a-zA-Z]+", "");
            //DataSet dsItemname = objBs.CheckDuplicate_ItemName(my_String);
            DataSet dsItemname = objBs.CheckDuplicate_PartyName(my_String, LedgerID);
            {
                if (dsItemname.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Party  Name Already Exists. Please enter a new one')", true);
                    return;
                }
            }

            int Stateid = 0; string State = "";
            if (chkDistrict.Checked == true)
            {
                DataSet dsState = objBs.CheckState(Convert.ToInt32(ddlCountry.SelectedValue), txtstate.Text);
                if (dsState.Tables[0].Rows.Count > 0)
                {
                    Stateid = Convert.ToInt32(dsState.Tables[0].Rows[0]["ID"].ToString());
                    State = dsState.Tables[0].Rows[0]["State"].ToString();
                }
                else
                {
                    Stateid = objBs.insert_State(txtcity.Text, Convert.ToInt32(ddlState.SelectedValue));
                    State = txtcity.Text;

                }
            }
            else
            {
                Stateid = Convert.ToInt32(ddlState.SelectedValue);
                State = ddlState.SelectedItem.Text;
            }

            int Cityid = 0; string City = "";
            if (chkCity.Checked == true)
            {
                DataSet dsCity = objBs.CityCheck(Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(Stateid), txtcity.Text);
                if (dsCity.Tables[0].Rows.Count > 0)
                {
                    Cityid = Convert.ToInt32(dsCity.Tables[0].Rows[0]["ID"].ToString());
                    City = dsCity.Tables[0].Rows[0]["City"].ToString();
                }
                else
                {
                    Cityid = objBs.insertCity(txtcity.Text, Convert.ToInt32(ddlState.SelectedValue));
                    City = txtcity.Text;
                }
            }
            else
            {
                Cityid = Convert.ToInt32(ddlCity.SelectedValue);
                City = ddlCity.SelectedItem.Text;
            }

            string Status = string.Empty;
            string ApprovedBy = string.Empty;
            string Narration = string.Empty;
            string EntryBy = string.Empty;

            if (lblpartytype.Text == "Y")
            {
                Status = "N";
                ApprovedBy = "";
                Narration = "Waiting For Approval...";
                EntryBy = lblUser.Text;
            }
            else
            {
                Status = "Y";
                ApprovedBy = lblUser.Text;
                EntryBy = lblUser.Text;
                Narration = "Self Approved Not in Party Type as Management Approval";

            }



            if (btnadd.Text == "Save")
            {
                #region

                // DataSet dsLedgerName = objBs.LedgerCheck(txtCompanyName.Text, 0);
                DataSet dsCompanyCode = objBs.Companycode_Checkk(txtCompanyCode.Text);
                //if (dsLedgerName.Tables[0].Rows.Count > 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This CompanyName Name was Already Exists.');", true);
                //    txtCompanyName.Focus();
                //    return;
                //}
                //else
                if (dsCompanyCode.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This CompanyCode was Already Exists.');", true);
                    txtCompanyCode.Focus();
                    return;
                }
                else
                {
                    int ledgerID = objBs.insertLedger(Convert.ToInt32(ddlPartyType.SelectedValue), txtCompanyName.Text, txtCompanyCode.Text, txtPhone.Text, txtMobile.Text, txtaddress.Text, Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Cityid, ddlState.SelectedItem.Text, City, txtpincode.Text, txtFax.Text, txtemail.Text, txtWebSite.Text, txtPaymentMode.Text, txtTerms.Text, txtDays.Text, txtNatureofContract.Text, txtStrength.Text, txtFinalDestination.Text, ddlShipmentMode.SelectedValue, txtACCode.Text, ddlIsActive.SelectedValue, ddlIsForeign.SelectedValue, Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToInt32(lblUserID.Text), txtcontactpersonname.Text, Status, ApprovedBy, Narration, EntryBy, my_String, drpfollowedby.SelectedValue, leadid, stus, Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(drpGSTType.SelectedValue));

                    for (int vLoop = 0; vLoop < GVPerson.Rows.Count; vLoop++)
                    {
                        TextBox txtPersonName = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPersonName");
                        TextBox txteMail1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txteMail1");

                        TextBox txtPhone1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPhone1");
                        TextBox txtPhone2 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPhone2");
                        TextBox txtMobile1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtMobile1");


                        if (txtPersonName.Text != "" && txteMail1.Text != "" && txtPhone1.Text != "" && txtPhone2.Text != "" && txtMobile1.Text != "")
                        {
                            int iSuccess = objBs.insertTransLedgerperson(ledgerID, txtPersonName.Text, txteMail1.Text, txtPhone1.Text, txtPhone2.Text, txtMobile1.Text);
                        }

                    }

                    Response.Redirect("PartyMasterGrid.aspx");
                }

                #endregion
            }
            else
            {
                #region

                DataSet dsLedgerName = objBs.LedgerCheck(txtCompanyName.Text, Convert.ToInt32(LedgerID));
                DataSet dsCompanyCode = objBs.LedgerCompanyCodeCheck(txtCompanyCode.Text, Convert.ToInt32(LedgerID));
                if (dsLedgerName.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This CompanyName Name was Already Exists.');", true);
                    txtCompanyName.Focus();
                    return;
                }
                else if (dsCompanyCode.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This CompanyCode was Already Exists.');", true);
                    txtCompanyCode.Focus();
                    return;
                }
                else
                {
                    int UpLedgerID = objBs.UpdateLedgerNew(Convert.ToInt32(ddlPartyType.SelectedValue), txtCompanyName.Text, txtCompanyCode.Text, txtPhone.Text, txtMobile.Text, txtaddress.Text, Convert.ToInt32(ddlCountry.SelectedValue), Convert.ToInt32(ddlState.SelectedValue), Cityid, ddlState.SelectedItem.Text, City, txtpincode.Text, txtFax.Text, txtemail.Text, txtWebSite.Text, txtPaymentMode.Text, txtTerms.Text, txtDays.Text, txtNatureofContract.Text, txtStrength.Text, txtFinalDestination.Text, ddlShipmentMode.SelectedValue, txtACCode.Text, ddlIsActive.SelectedValue, ddlIsForeign.SelectedValue, Convert.ToInt32(ddlCurrency.SelectedValue), Convert.ToInt32(lblUserID.Text), Convert.ToInt32(LedgerID), txtcontactpersonname.Text, my_String, drpfollowedby.SelectedValue,Convert.ToInt32(ddlProvince.SelectedValue),Convert.ToInt32(ddlGroup.SelectedValue), Convert.ToInt32(drpGSTType.SelectedValue));

                    for (int vLoop = 0; vLoop < GVPerson.Rows.Count; vLoop++)
                    {
                        TextBox txtPersonName = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPersonName");
                        TextBox txteMail1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txteMail1");

                        TextBox txtPhone1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPhone1");
                        TextBox txtPhone2 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtPhone2");
                        TextBox txtMobile1 = (TextBox)GVPerson.Rows[vLoop].FindControl("txtMobile1");


                        if (txtPersonName.Text != "" && txteMail1.Text != "" && txtPhone1.Text != "" && txtPhone2.Text != "" && txtMobile1.Text != "")
                        {
                            int iSuccess = objBs.insertTransLedgerperson(Convert.ToInt32(LedgerID), txtPersonName.Text, txteMail1.Text, txtPhone1.Text, txtPhone2.Text, txtMobile1.Text);
                        }

                    }

                    Response.Redirect("PartyMasterGrid.aspx");
                }

                #endregion

            }

            Response.Redirect("PartyMasterGrid.aspx");
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("PartyMasterGrid.aspx");
        }
    }
}