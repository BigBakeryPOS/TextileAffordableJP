
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Text;
using System.Data;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class DilloMultipleLot : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            string lot = string.Empty;
            string Requests = string.Empty;
            lot = Request.QueryString.Get("lotid");

            Requests = Request.QueryString.Get("name");


            if (!IsPostBack)
            {

                DataSet dsUnitName = objbs.Select_UnitFirst();//tblUnit
                if (dsUnitName.Tables[0].Rows.Count > 0)
                {
                    drpmultiunit.DataSource = dsUnitName.Tables[0];
                    drpmultiunit.DataTextField = "UnitName";
                    drpmultiunit.DataValueField = "UnitID";
                    drpmultiunit.DataBind();
                    drpmultiunit.Items.Insert(0, "Select Unit");
                }

               MultiUnit_SelectedIndex(sender, e);
                DataSet drpEmpp = objbs.SelectEmpName();
                drpMultiemployee.DataSource = drpEmpp;
                drpMultiemployee.DataTextField = "Name";
                drpMultiemployee.DataValueField = "Employee_Id";
                drpMultiemployee.DataBind();
                drpMultiemployee.Items.Insert(0, "Select Employee Name");
                divWork.Visible = false;
                if (lot != null)
                {
                    if (Requests == "Edit")
                    {
                        DataSet dgetcheck = objbs.getMultipleprocesss(lot);
                        if (dgetcheck.Tables[0].Rows.Count > 0)
                        {
                            btnadd.Visible = true;
                            btnadd.Text = "Update";
                            txtid.Text = dgetcheck.Tables[0].Rows[0]["Multiid"].ToString();
                            txtmultiid.Text = dgetcheck.Tables[0].Rows[0]["MultiNo"].ToString();
                            txtmultiplecode.Text = dgetcheck.Tables[0].Rows[0]["FullNo"].ToString();
                            drpmultiunit.SelectedValue = dgetcheck.Tables[0].Rows[0]["Unit"].ToString();
                            drpMultiemployee.SelectedValue = dgetcheck.Tables[0].Rows[0]["Employee"].ToString();
                            txtmultidate.Text = Convert.ToDateTime(dgetcheck.Tables[0].Rows[0]["date"]).ToString("dd/MM/yyyy");
                            txttotalqty.Text = dgetcheck.Tables[0].Rows[0]["TotalQty"].ToString();
                            DataSet ds = objbs.SelectLOTInfoDetGridViewformultiple(lot);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                               

                                   
                                    DataSet temp = new DataSet();
                                    DataTable dtt = new DataTable();

                                    dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("LotNo", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("Process", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("SendQty", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("RecQuantity", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("date", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("Recdate", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("Bundle", typeof(string)));
                                    temp.Tables.Add(dtt);



                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        if (Convert.ToDouble(ds.Tables[0].Rows[i]["RemainQty"].ToString()) != 0.00)
                                        {
                                            DataRow dr = dtt.NewRow();

                                            dr["OrderNo"] = "0";

                                            dr["LotNo"] = ds.Tables[0].Rows[i]["LotNo"].ToString();
                                            dr["Process"] = ds.Tables[0].Rows[i]["Processid"].ToString();
                                            dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                                            dr["SendQty"] = Convert.ToDouble(ds.Tables[0].Rows[i]["SendFQty"]).ToString("0.00");
                                            dr["RemainQty"] = Convert.ToDouble(ds.Tables[0].Rows[i]["RemainQty"]).ToString("0.00");
                                            dr["date"] = ds.Tables[0].Rows[i]["Senddate"].ToString();
                                            dr["RecQuantity"] = Convert.ToDouble(ds.Tables[0].Rows[i]["ReceivedFQty"]).ToString("0.00");
                                            dr["Recdate"] = ds.Tables[0].Rows[i]["REceivedDate"];
                                            dr["Bundle"] = ds.Tables[0].Rows[i]["BundleNo"].ToString();



                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }



                                    ViewState["CurrentTable1"] = dtt;

                                    gvcustomerorder.DataSource = temp;
                                    gvcustomerorder.DataBind();

                                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                    {
                                        DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                                        TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                                        TextBox txtRate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                                        TextBox txtBundle = (TextBox)gvcustomerorder.Rows[i].FindControl("txtBundle");
                                        TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                                        TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");

                                        Recdate.Enabled = false;
                                        txtrecFQty.Enabled = false;
                                        drpLotno.SelectedValue = temp.Tables[0].Rows[i]["LotNo"].ToString();
                                        drpProcess.SelectedValue = temp.Tables[0].Rows[i]["Process"].ToString();
                                        txtsendFQty.Text = temp.Tables[0].Rows[i]["SendQty"].ToString();
                                        txtRemainQty.Text = temp.Tables[0].Rows[i]["RemainQty"].ToString();
                                        txtrecFQty.Text = temp.Tables[0].Rows[i]["RecQuantity"].ToString();
                                        txtRate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();

                                        txtBundle.Text = temp.Tables[0].Rows[i]["Bundle"].ToString();
                                        date.Text = temp.Tables[0].Rows[i]["date"].ToString();
                                        Recdate.Text = temp.Tables[0].Rows[i]["Recdate"].ToString();

                                        if (Convert.ToDouble(txtRemainQty.Text) <= 0.00)
                                        {
                                            txtsendFQty.Enabled = false;
                                        }


                                        if (Convert.ToDouble(txtrecFQty.Text) > 0.00)
                                        {
                                            txtsendFQty.Enabled = false;
                                        }
                                }
                            }
                            else
                            {
                                FirstGridViewRow();

                            }


                            ddlLotNo.Enabled = false;

                        }
                    }
                    else if (Requests == "Receive")
                    {
                        DataSet dgetcheck = objbs.getMultipleprocesss(lot);
                        if (dgetcheck.Tables[0].Rows.Count > 0)
                        {
                            btnadd.Visible = true;
                            btnadd.Text = "Received";
                            txtid.Text = dgetcheck.Tables[0].Rows[0]["Multiid"].ToString();
                            txtmultiid.Text = dgetcheck.Tables[0].Rows[0]["MultiNo"].ToString();
                            txtmultiplecode.Text = dgetcheck.Tables[0].Rows[0]["FullNo"].ToString();
                            txtmultiplecode.Enabled = false;
                            drpmultiunit.SelectedValue = dgetcheck.Tables[0].Rows[0]["Unit"].ToString();
                            drpmultiunit.Enabled = false;
                            drpMultiemployee.SelectedValue = dgetcheck.Tables[0].Rows[0]["Employee"].ToString();
                            drpMultiemployee.Enabled = false;
                            txtmultidate.Text = Convert.ToDateTime(dgetcheck.Tables[0].Rows[0]["date"]).ToString("dd/MM/yyyy");
                            txtmultidate.Enabled = false;
                            txttotalqty.Text = dgetcheck.Tables[0].Rows[0]["TotalQty"].ToString();
                            txttotalqty.Enabled = false;
                            DataSet ds = objbs.SelectLOTInfoDetGridViewformultiple(lot);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();

                              //  dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                                dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                                dtt.Columns.Add(new DataColumn("LotNo", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Process", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("SendQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("RecQuantity", typeof(string)));
                                dtt.Columns.Add(new DataColumn("date", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Recdate", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Bundle", typeof(string)));
                                temp.Tables.Add(dtt);



                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    if (Convert.ToDouble(ds.Tables[0].Rows[i]["RemainQty"].ToString()) != 0.00)
                                    {
                                        DataRow dr = dtt.NewRow();

                                        dr["OrderNo"] = "0";
                                        //  dr["Transid"] = ds.Tables[0].Rows[i]["Transid"].ToString();
                                        dr["LotNo"] = ds.Tables[0].Rows[i]["LotNo"].ToString();
                                        dr["Process"] = ds.Tables[0].Rows[i]["Processid"].ToString();
                                        dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                                        dr["SendQty"] = Convert.ToDouble(ds.Tables[0].Rows[i]["SendFQty"]).ToString("0.00");
                                        dr["RemainQty"] = Convert.ToDouble(ds.Tables[0].Rows[i]["RemainQty"]).ToString("0.00");
                                        dr["date"] = ds.Tables[0].Rows[i]["Senddate"].ToString();
                                        dr["RecQuantity"] = "0";
                                        dr["Recdate"] = ds.Tables[0].Rows[i]["REceivedDate"];
                                        dr["Bundle"] = ds.Tables[0].Rows[i]["BundleNo"].ToString();

                                        temp.Tables[0].Rows.Add(dr);
                                    }
                                }

                                ViewState["CurrentTable1"] = dtt;

                                gvcustomerorder.DataSource = temp;
                                gvcustomerorder.DataBind();

                                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                {
                                    DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                                    TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                                    TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                                    TextBox txtRate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                                    TextBox txtBundle = (TextBox)gvcustomerorder.Rows[i].FindControl("txtBundle");
                                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                                    TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");
                                //    Label trans = (Label)gvcustomerorder.Rows[i].FindControl("lbltransid");
                                    Recdate.Enabled = false;
                                    txtrecFQty.Enabled = true;
                                    drpLotno.Enabled = false;
                                    drpProcess.Enabled = false;
                                    txtsendFQty.Enabled = false;
                                    txtRate.Enabled = false;
                                    txtBundle.Enabled = false;
                                    date.Enabled = false;
                                    
                                    drpLotno.SelectedValue = temp.Tables[0].Rows[i]["LotNo"].ToString();
                                    drpProcess.SelectedValue = temp.Tables[0].Rows[i]["Process"].ToString();
                                    txtsendFQty.Text = temp.Tables[0].Rows[i]["SendQty"].ToString();
                                    txtRemainQty.Text = temp.Tables[0].Rows[i]["RemainQty"].ToString();
                                    txtrecFQty.Text = temp.Tables[0].Rows[i]["RecQuantity"].ToString();
                                    txtRate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();
                                   // trans.Text = temp.Tables[0].Rows[i]["Transid"].ToString();
                                    txtBundle.Text = temp.Tables[0].Rows[i]["Bundle"].ToString();
                                    date.Text = temp.Tables[0].Rows[i]["date"].ToString();
                                    Recdate.Text = temp.Tables[0].Rows[i]["Recdate"].ToString();
                                }
                            }
                            else
                            {
                                FirstGridViewRow();

                            }


                            ddlLotNo.Enabled = false;

                        }
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Something Went Wrong.Please Check Lot No.Or Contact Administrator.')", true);
                        return;
                    }
                }
                else
                {

                    //  FirstGridViewRow();
                }
                // Detail_checked(sender, e);
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

        }
        protected void MultiUnit_SelectedIndex(object sender, EventArgs e)
        {

            if (drpmultiunit.SelectedValue == "Select Unit")
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Unit Name.Thank You!!!.')", true);
                //drpmultiunit.Focus();
                //return;
            }
            else
            {

                DataSet dmaxbill = objbs.getmaxbillformulti(drpmultiunit.SelectedValue);
                if (dmaxbill.Tables[0].Rows.Count > 0)
                {
                    string bill = dmaxbill.Tables[0].Rows[0]["bill"].ToString();

                    char F = drpmultiunit.SelectedItem.Text.First();

                    char L = drpmultiunit.SelectedItem.Text.Last();


                    txtmultiplecode.Text = F.ToString() + L.ToString() + '/' + bill;
                    txtmultiid.Text = bill.ToString();

                }


                DataSet drpEmp = objbs.SelectEmpName(drpmultiunit.SelectedValue);
                drpMultiemployee.DataSource = drpEmp;
                drpMultiemployee.DataTextField = "Name";
                drpMultiemployee.DataValueField = "Employee_Id";
                drpMultiemployee.DataBind();
                drpMultiemployee.Items.Insert(0, "Select Employee Name");


                FirstGridViewRow();
                ScriptManager.RegisterStartupScript(this, GetType(), "Myscript", "closeNav();", true);
            }
            //  gvcustomerorder.DataBind();

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    DropDownList drpLprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
            //    DataSet dsLotNo = objbs.Select_Lotnewstich(drpmultiunit.SelectedValue);
            //    if (dsLotNo.Tables[0].Rows.Count > 0)
            //    {
            //        drpLprocess.DataSource = dsLotNo.Tables[0];
            //        drpLprocess.DataTextField = "LotNo";
            //        drpLprocess.DataValueField = "cutid";
            //        drpLprocess.DataBind();
            //        drpLprocess.Items.Insert(0, "Select Lot No");
            //    }
            //    else
            //    {
            //        drpLprocess.Items.Clear();
            //        drpLprocess.Items.Insert(0, "Select Lot No");
            //    }
            //}

            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;

            //DropDownList drplotprocess = (DropDownList)row.FindControl("drpLotno");

            //DropDownList drpProcess = (DropDownList)row.FindControl("drpProcess");



            //if (drplotprocess.SelectedItem.Text != "Select Lot No")
            //{

            //    DataSet dsLotNo = objbs.Select_Lotnewstich(drpmultiunit.SelectedValue);
            //    if (dsLotNo.Tables[0].Rows.Count > 0)
            //    {
            //        drplotprocess.DataSource = dsLotNo.Tables[0];
            //        drplotprocess.DataTextField = "LotNo";
            //        drplotprocess.DataValueField = "cutid";
            //        drplotprocess.DataBind();
            //        drplotprocess.Items.Insert(0, "Select Lot No");
            //    }
            //    else
            //    {
            //        drplotprocess.Items.Clear();
            //        drplotprocess.Items.Insert(0, "Select Lot No");
            //    }
            //}
            //else
            //{
            //}

        }
        protected void txtrecqtychnaged_text(object sender, EventArgs e)
        {
            if (btnadd.Text == "Save")
            {
                string curent = string.Empty;
                string namee = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    string temp = drpprocess.SelectedValue;
                    double qty = 0;
                    for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                    {

                        DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                        curent = process.SelectedValue;
                        namee = process.SelectedItem.Text;
                        TextBox RecQuantity = (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");

                        if (process.SelectedValue == temp)
                        {
                            qty = qty + Convert.ToDouble(RecQuantity.Text);
                        }
                    }
                    //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                    //{
                    //}
                    if (objbs.CheckIfrecqtyinstiching(ddlLotNo.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee + " has Exists received Quantity.')", true);
                        txtRecQuantity.Focus();
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    date.Focus();


                }
            }
            else
            {

                string curent = string.Empty;
                string namee = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    string temp = drpprocess.SelectedValue;
                    double qty = 0;
                    for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                    {

                        DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                        curent = process.SelectedValue;
                        namee = process.SelectedItem.Text;
                        TextBox RecQuantity = (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");

                        if (process.SelectedValue == temp)
                        {
                            qty = qty + Convert.ToDouble(RecQuantity.Text);
                        }
                    }
                    //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                    //{
                    //}
                    if (objbs.CheckIfrecqtyinstichingupdate(ddlLotNo.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee.Trim() + " has Exists received Quantity.')", true);
                        txtRecQuantity.Focus();
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    date.Focus();
                }
            }
            //  AddNewRow();
        }

        protected void drpLot_selected(object sender, EventArgs e)
        {
            drpmultiunit.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "Myscript", "closeNav();", true);

            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;

            DropDownList drplotprocess = (DropDownList)row.FindControl("drpLotno");

            DropDownList drpProcess = (DropDownList)row.FindControl("drpProcess");



            if (drplotprocess.SelectedItem.Text != "Select Lot No")
            {

                DataSet drpProcesss = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(drplotprocess.SelectedValue));
                if (drpProcesss.Tables[0].Rows.Count > 0)
                {
                    drpProcess.Items.Clear();
                    drpProcess.DataSource = drpProcesss;
                    drpProcess.DataTextField = "ProcessType";
                    drpProcess.DataValueField = "ProcessMasterID";
                    drpProcess.DataBind();
                    drpProcess.Items.Insert(0, "Select Process Type");

                }
                else
                {
                    drpProcess.Items.Clear();
                    drpProcess.Items.Insert(0, "Select Process Type");
                }
            }
            else
            {
            }

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                DropDownList drpProce = (DropDownList)row.FindControl("drpProcess");
                drpProce.Focus();
            }

        }
        protected void txtsendfqtychnaged_text(object sender, EventArgs e)
        {
            double sndqty = 0;
            int iq = 1;
            int iii = 1;
            int iq1 = 1;
            int iii1 = 1;
            string itemc = string.Empty;
            string itemd = string.Empty;
            string iteme = string.Empty;
            string itemcd = string.Empty;

            if (btnadd.Text == "Save")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drplotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    TextBox txtBundle = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
                    if (drpprocess.SelectedValue != "Select Process Type")
                    {

                        TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");

                        TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                        if (date.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                            return;
                        }

                        //   DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //   DateTime recDate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                        itemc = drpprocess.SelectedValue;
                        itemd = txtBundle.Text;
                        iteme = drplotno.SelectedValue;


                        if ((itemc == null) || (itemc == ""))
                        {
                        }
                        else
                        {
                            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                            {
                                DropDownList drplotno1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpLotno");
                                DropDownList drpprocesss = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpProcess");
                                TextBox txtBundle1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtBundle");
                                if (drpprocesss.SelectedValue != "Select Process Type")
                                {
                                    // DropDownList drpEmpp = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpEmp");
                                    TextBox datee = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("date");
                                    // DateTime recDatee = DateTime.ParseExact(datee.Text, "dd/MM/yyyy HH:MM:ss", CultureInfo.InvariantCulture);

                                    {

                                        if (iii == iq)
                                        {
                                        }
                                        else
                                        {
                                            if (itemc == drpprocesss.Text && iteme == drplotno1.SelectedValue && itemd == txtBundle1.Text)
                                            {
                                                itemcd = drpprocess.SelectedItem.Text;
                                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "," + drpEmpp.SelectedItem.Text + ",'" + recDatee.ToString("dd/MM/yyyy") + "'  already exists in the Grid.');", true);
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd.Trim() + "," + drplotno1.SelectedItem.Text.Trim() + "already exists in the Grid.');", true);
                                                drpprocess.Focus();
                                                // drpEmpp.Focus();
                                                return;

                                            }
                                        }
                                        iii = iii + 1;
                                    }
                                }
                            }
                        }
                        iq = iq + 1;
                        iii = 1;
                    }
                }
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drplotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    TextBox txtBundle = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
                    if (drpprocess.SelectedValue != "Select Process Type")
                    {

                        TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");

                        TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                        if (date.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                            return;
                        }

                        //   DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //   DateTime recDate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                        itemc = drpprocess.SelectedValue;
                         itemd = txtBundle.Text;
                        iteme = drplotno.SelectedValue;


                        if ((itemc == null) || (itemc == ""))
                        {
                        }
                        else
                        {
                            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                            {
                                DropDownList drplotno1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpLotno");
                                DropDownList drpprocesss = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpProcess");
                                TextBox txtBundle1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtBundle");
                                if (drpprocesss.SelectedValue != "Select Process Type")
                                {
                                    // DropDownList drpEmpp = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpEmp");
                                    TextBox datee = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("date");
                                    // DateTime recDatee = DateTime.ParseExact(datee.Text, "dd/MM/yyyy HH:MM:ss", CultureInfo.InvariantCulture);

                                    {

                                        if (iii1 == iq1)
                                        {
                                        }
                                        else
                                        {
                                            if (itemc == drpprocesss.Text && iteme == drplotno1.SelectedValue && itemd == txtBundle1.Text)
                                            {
                                                itemcd = drpprocess.SelectedItem.Text;
                                                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "," + drpEmpp.SelectedItem.Text + ",'" + recDatee.ToString("dd/MM/yyyy") + "'  already exists in the Grid.');", true);
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd.Trim() + "," + drplotno1.SelectedItem.Text.Trim() + "already exists in the Grid.');", true);
                                                drpprocess.Focus();
                                                // drpEmpp.Focus();
                                                return;

                                            }
                                        }
                                        iii1 = iii1 + 1;
                                    }
                                }
                            }
                        }
                        iq1 = iq1 + 1;
                        iii1 = 1;
                    }
                }

                string curent = string.Empty;
                string namee = string.Empty;
                string bun = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                    if (drpLotprocess.SelectedValue == "Select Lot No")
                    {
                    }
                    else
                    {
                        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                        TextBox txtbundle = (TextBox)gvcustomerorder.Rows[i].FindControl("txtBundle");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                        txtrecFQty.Text = "0";
                        txtrecFQty.Enabled = false;

                        sndqty = sndqty + Convert.ToDouble(txtsendFQty.Text);
                        //  TextBox txtsendHQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendHQty");

                        //TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                        string temp = drpprocess.SelectedValue;
                        string tempbun = txtbundle.Text;
                        double qty = 0;
                        for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                        {
                            DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                            DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                             TextBox bundle = (TextBox)gvcustomerorder.Rows[j].FindControl("txtBundle");
                            curent = process.SelectedValue;
                            namee = process.SelectedItem.Text;
                            bun = bundle.Text;
                            TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendFQty");
                            //   TextBox txtsendHQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendHQty");

                            if (process.SelectedValue == temp && bun==tempbun)
                            {
                                qty = qty + Convert.ToDouble(txtsendFQty1.Text);
                            }
                        }
                        //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                        //{
                        //}

                        if (drpLotprocess.SelectedValue == "Select Lot No")
                        {
                        }
                        else
                        {
                            if (objbs.CheckIfrecqtyinstiching(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + drpprocess.SelectedItem.Text + " has Exists received Quantity.')", true);
                                // txtRecQuantity.Focus();
                                txtsendFQty.Focus();
                                return;
                                // lblerror.Text = "These Category has already Exists. please enter a new one";

                            }

                        }
                        // date.Focus();
                        //  txtsendHQty.Focus();
                        //txtbundle.Focus();

                    }

                    txttotalqty.Text = sndqty.ToString();
                }
            }
            else
            {
                if (btnadd.Text == "Update")
                {
                    string curent = string.Empty;
                    string namee = string.Empty;
                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {
                        DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                        if (drpLotprocess.SelectedValue == "Select Lot No")
                        {
                        }
                        else
                        {
                            DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                            TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                            TextBox txtbundle = (TextBox)gvcustomerorder.Rows[i].FindControl("txtBundle");
                            TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                            txtrecFQty.Text = "0";
                            txtrecFQty.Enabled = false;

                            sndqty = sndqty + Convert.ToDouble(txtsendFQty.Text);
                            //  TextBox txtsendHQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendHQty");

                            //TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                            //string temp = drpprocess.SelectedValue;
                            //double qty = 0;
                            //for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                            //{
                            //    DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                            //    DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                            //    curent = process.SelectedValue;
                            //    namee = process.SelectedItem.Text;
                            //    TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendFQty");
                            //    //   TextBox txtsendHQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendHQty");

                            //    if (process.SelectedValue == temp)
                            //    {
                            //        qty = qty + Convert.ToDouble(txtsendFQty1.Text);
                            //    }
                            //}
                            //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                            //{
                            //}

                            //if (drpLotprocess.SelectedValue == "Select Lot No")
                            //{
                            //}
                            //else
                            //{
                            //    if (objbs.CheckIfrecqtyinstiching(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                            //    {
                            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + drpprocess.SelectedItem.Text + " has Exists received Quantity.')", true);

                            //        txtsendFQty.Focus();
                            //        return;

                            //    }
                            //}

                            txtbundle.Focus();

                        }

                        txttotalqty.Text = sndqty.ToString();
                    }
                }
            }



            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                //  TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtBundle");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtBundle");
                    if (oldtxttk.Text == "")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            }


            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    int cnt = gvcustomerorder.Rows.Count;
            //    //  TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
            //    if (vLoop >= 1)
            //    {
            //        //TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtBundle");

            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");

            //        //    oldtxttk.Text = ".00";
            //        oldtxttk.Focus();
            //    }
            //    int tot = cnt - vLoop;
            //    if (tot == 1)
            //    {
            //        //TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtBundle");

            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
            //        if (oldtxttk.Text == "")
            //        {
            //            oldtxttk.Text = "";
            //            oldtxttk.Focus();
            //        }
            //        else
            //        {
            //            oldtxttk.Focus();
            //        }
            //    }
            //    //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            //}

            // AddNewRow();

        }
        protected void txtsendhqtychnaged_text(object sender, EventArgs e)
        {
            if (btnadd.Text == "Save")
            {
                string curent = string.Empty;
                string namee = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                    TextBox txtsendHQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendHQty");

                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    string temp = drpprocess.SelectedValue;
                    double qty = 0;
                    for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                    {
                        DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                        DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                        curent = process.SelectedValue;
                        namee = process.SelectedItem.Text;
                        TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendFQty");
                        TextBox txtsendHQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendHQty");

                        if (process.SelectedValue == temp)
                        {
                            qty = qty + Convert.ToDouble(txtsendFQty1.Text) + Convert.ToDouble(txtsendHQty1.Text); ;
                        }
                    }
                    //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                    //{
                    //}
                    if (objbs.CheckIfrecqtyinstiching(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee + " has Exists received Quantity.')", true);
                        // txtRecQuantity.Focus();
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    // date.Focus();
                    date.Focus();

                }
            }
            else
            {

                string curent = string.Empty;
                string namee = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                    TextBox txtsendHQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendHQty");

                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    string temp = drpprocess.SelectedValue;
                    double qty = 0;
                    for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                    {
                        DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                        DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                        curent = process.SelectedValue;
                        namee = process.SelectedItem.Text;
                        TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendFQty");
                        TextBox txtsendHQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendHQty");

                        if (process.SelectedValue == temp)
                        {
                            qty = qty + Convert.ToDouble(txtsendHQty1.Text) + Convert.ToDouble(txtsendFQty1.Text);
                        }
                    }
                    //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
                    //{
                    //}
                    if (objbs.CheckIfrecqtyinstiching(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee + " has Exists received Quantity.')", true);
                        // txtRecQuantity.Focus();
                        return;
                        // lblerror.Text = "These Category has already Exists. please enter a new one";

                    }
                    date.Focus();
                    // txtsendHQty.Focus();

                }
            }
            // AddNewRow();
        }
        protected void txtrecfqtychnaged_text(object sender, EventArgs e)
        {
            double recqty = 0;
            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");

                recqty = recqty + Convert.ToDouble(txtrecQty.Text);

                if (Convert.ToDouble(txtsendFQty.Text) < Convert.ToDouble(txtrecQty.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Received Qty is not Greater than That Remain Qty!!!')", true);
                    txtrecQty.Focus();
                    return;
                }

            }

            txtreceivedQty.Text = recqty.ToString();

        }
        protected void txtrechqtychnaged_text(object sender, EventArgs e)
        {

        }

        protected void drpprocess_selected(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList ddlprocess = (DropDownList)row.FindControl("drpProcess");
            DropDownList drplotprocess = (DropDownList)row.FindControl("drpLotno");
            TextBox txtrate = (TextBox)row.FindControl("txtRate");


            DataSet ds = new DataSet();
            if (ddlprocess.SelectedValue != "Select Process Type")
            {
                ds = objbs.getrateforstiching(ddlprocess.SelectedValue, drplotprocess.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtrate.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Rate"]).ToString();
                }
                DataSet dlotprocess = new DataSet();
                dlotprocess = objbs.getprocessdetailsforsticparticular(drplotprocess.SelectedValue, ddlprocess.SelectedValue);
                if (dlotprocess.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = dlotprocess;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }

                DataSet drpRateProcess = objbs.SelectProcessTypeLotProcessforpartiuclarr(Convert.ToInt32(drplotprocess.SelectedValue), ddlprocess.SelectedValue);
                if (drpRateProcess.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = drpRateProcess;
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
            }



            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {

                TextBox txtsendfFqty = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtsendFQty");
                txtsendfFqty.Focus();

            }

            //ButtonAdd1_Click(sender, e);
            ScriptManager.RegisterStartupScript(this, GetType(), "Myscript", "openNav();", true);

            ButtonAdd1_Click(sender, e);



            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                //  TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtsendFQty");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtsendFQty");
                    if (oldtxttk.Text == "")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            }

        }
        protected void Detail_checked(object sender, EventArgs e)
        {
            if (ddlLotNo.SelectedValue != "Select Lot No")
            {
                DataSet dlotprocess = new DataSet();
                dlotprocess = objbs.getprocessdetailsforstic(ddlLotNo.SelectedValue);
                if (dlotprocess.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = dlotprocess;
                    GridView1.DataBind();
                }
                GridView2.Visible = false;
                processs.Visible = true;
                ratee.Visible = false;
                GridView1.Visible = true;
                Ratedetail.Checked = false;
                DetailView.Checked = true;
                //  mpe.Show();
                //  DetailView.Checked = false;
            }
        }

        protected void RateDetail_checked(object sender, EventArgs e)
        {
            if (ddlLotNo.SelectedValue != "Select Lot No")
            {
                DataSet drpProcess = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(ddlLotNo.SelectedValue));
                if (drpProcess.Tables[0].Rows.Count > 0)
                {
                    GridView2.DataSource = drpProcess;
                    GridView2.DataBind();
                }
                GridView1.Visible = false;
                GridView2.Visible = true;
                processs.Visible = false;
                ratee.Visible = true;
                //  mpe1.Show();
                Ratedetail.Checked = true;
                DetailView.Checked = false;
            }
        }


        protected void StitchingInfo_Loadforupdate(object sender, EventArgs e)
        {
            //DataSet dataSet = objbs.getLotNoTransDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
            //if (dataSet.Tables[0].Rows.Count > 0)
            //{
            //    txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
            //    txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();

            //    txtUnitName.Text = dataSet.Tables[0].Rows[0]["UnitName"].ToString();
            //    txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString();

            //    txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
            //    txtbrandid.Text = dataSet.Tables[0].Rows[0]["BrandID"].ToString();
            //    txtUnitID.Text = dataSet.Tables[0].Rows[0]["UnitID"].ToString();
            //    txtHalf.Text = dataSet.Tables[0].Rows[0]["HalfQty"].ToString();
            //    txtfull.Text = dataSet.Tables[0].Rows[0]["FullQty"].ToString();
            //    txtdesignno.Text = dataSet.Tables[0].Rows[0]["Designno"].ToString();
            //    string processDate = dataSet.Tables[0].Rows[0]["ProcessDate"].ToString();
            //    if (processDate == "")
            //    {
            //        DateTime date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //        txtProcessDate.Text = date.ToString("dd/MM/yyyy");
            //    }
            //    else
            //    {
            //        DateTime date = DateTime.ParseExact(Convert.ToDateTime(processDate).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //        txtProcessDate.Text = date.ToString("dd/MM/yyyy");
            //        //        CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            //    }
            //    //txtProcessDate.Text = DateTime.ParseExact(processDate, "dd/MM/yyyy hh:mm:ss tt",
            //    //            CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            //    //DateTime processDate1 = DateTime.ParseExact(processDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //txtProcessDate.Text = processDate1.ToString();
            //    string lotno = "0";
            //    if (ddlLotNo.SelectedValue == "Select Lot No")
            //    {
            //        lotno = "0";
            //    }
            //    else
            //    {
            //        lotno = ddlLotNo.SelectedValue;
            //    }

            //    DataSet drpProcess = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(lotno));
            //    DataSet drpEmp = objbs.SelectStitchingEmpName(Convert.ToInt32(txtUnitID.Text));

            //    //DropDownList dbrand = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpProcess");
            //    //DropDownList dEmp = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpEmp");
            //    //if (drpProcess.Tables[0].Rows.Count > 0)
            //    //{
            //    //    dbrand.Items.Clear();
            //    //    dbrand.ClearSelection();
            //    //    dbrand.DataSource = drpProcess.Tables[0];
            //    //    dbrand.DataTextField = "ProcessType";
            //    //    dbrand.DataValueField = "ProcessMasterID";
            //    //    dbrand.DataBind();
            //    //    dbrand.Items.Insert(0, "Select Process Type");

            //    //    dEmp.DataSource = drpEmp;
            //    //    dEmp.DataTextField = "Name";
            //    //    dEmp.DataValueField = "Employee_Id";
            //    //    dEmp.DataBind();
            //    //    dEmp.Items.Insert(0, "Select Employee Name");
            //    //}
            //    //   GridView2.DataSource = drpProcess;
            //    //    GridView2.DataBind();

            //    DataSet dlotprocess = new DataSet();
            //    dlotprocess = objbs.getprocessdetailsforstic(lotno);
            //    if (dlotprocess.Tables[0].Rows.Count > 0)
            //    {
            //        GridView1.DataSource = dlotprocess;
            //        GridView1.DataBind();
            //    }

            //    DataSet workProcessManual = objbs.SelectWorkProcessTypeManual(Convert.ToInt32(ddlLotNo.SelectedValue));
            //    if (workProcessManual.Tables[0].Rows[0]["IsManual"].ToString() == "True")
            //    {
            //        divWork.Visible = true;
            //        GridView3.DataSource = workProcessManual;
            //        GridView3.DataBind();
            //    }
            //    else
            //    {
            //        divWork.Visible = false;
            //    }

            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {

            //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //        drpprocess.Focus();

            //    }
            //}
            //else
            //{
            //    gvcustomerorder.DataSource = null;
            //    gvcustomerorder.DataBind();
            //}

            //GridView1.Visible = true;
            //GridView2.Visible = true;
            //processs.Visible = true;
            //ratee.Visible = true;
        }

        protected void StitchingInfo_Load(object sender, EventArgs e)
        {
            //DataSet dataSet = objbs.getLotNoTransDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
            //if (dataSet.Tables[0].Rows.Count > 0)
            //{
            //    txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
            //    txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();

            //    txtUnitName.Text = dataSet.Tables[0].Rows[0]["UnitName"].ToString();
            //    txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString();

            //    txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
            //    txtbrandid.Text = dataSet.Tables[0].Rows[0]["BrandID"].ToString();
            //    txtUnitID.Text = dataSet.Tables[0].Rows[0]["UnitID"].ToString();
            //    txtHalf.Text = dataSet.Tables[0].Rows[0]["HalfQty"].ToString();
            //    txtfull.Text = dataSet.Tables[0].Rows[0]["FullQty"].ToString();
            //    txtdesignno.Text = dataSet.Tables[0].Rows[0]["DesignNo"].ToString();
            //    string processDate = dataSet.Tables[0].Rows[0]["ProcessDate"].ToString();
            //    if (processDate == "")
            //    {
            //        DateTime date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //        txtProcessDate.Text = date.ToString("dd/MM/yyyy");
            //    }
            //    else
            //    {
            //        txtProcessDate.Text = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["ProcessDate"]).ToString("dd/MM/yyyy");
            //    }
            //    //txtProcessDate.Text = DateTime.ParseExact(processDate, "dd/MM/yyyy hh:mm:ss tt",
            //    //            CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            //    //DateTime processDate1 = DateTime.ParseExact(processDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //txtProcessDate.Text = processDate1.ToString();
            //    string lotno = "0";
            //    if (ddlLotNo.SelectedValue == "Select Lot No")
            //    {
            //        lotno = "0";
            //    }
            //    else
            //    {
            //        lotno = ddlLotNo.SelectedValue;
            //    }


            //    DropDownList dbrand = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpProcess");

            //    if (drpProcess.Tables[0].Rows.Count > 0)
            //    {
            //        dbrand.Items.Clear();
            //        dbrand.ClearSelection();
            //        dbrand.DataSource = drpProcess.Tables[0];
            //        dbrand.DataTextField = "ProcessType";
            //        dbrand.DataValueField = "ProcessMasterID";
            //        dbrand.DataBind();
            //        dbrand.Items.Insert(0, "Select Process Type");
            //    }

            //    DataSet drpEmpName = new DataSet();
            //    if (txtUnitID.Text == "")
            //    {
            //        drpEmpName = objbs.SelectEmpName();
            //    }
            //    else
            //    {
            //        drpEmpName = objbs.SelectStitchingEmpName(Convert.ToInt32(txtUnitID.Text));
            //    }
            //    DropDownList dEmp = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpEmp");

            //    if (drpEmpName.Tables[0].Rows.Count > 0)
            //    {
            //        dEmp.Items.Clear();
            //        dEmp.DataSource = drpEmpName.Tables[0];
            //        dEmp.DataTextField = "Name";
            //        dEmp.DataValueField = "Employee_Id";
            //        dEmp.DataBind();
            //        dEmp.Items.Insert(0, "Select Employee Name");
            //    }

            //    GridView2.DataSource = drpProcess;
            //    GridView2.DataBind();



            //    DataSet workProcessManual = objbs.SelectWorkProcessTypeManual(Convert.ToInt32(ddlLotNo.SelectedValue));
            //    if (workProcessManual.Tables[0].Rows[0]["IsManual"].ToString() == "True")
            //    {
            //        divWork.Visible = true;
            //        GridView3.DataSource = workProcessManual;
            //        GridView3.DataBind();
            //    }
            //    else
            //    {
            //        divWork.Visible = false;
            //    }

            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {

            //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //        drpprocess.Focus();

            //    }
            //}
            //else
            //{
            //    gvcustomerorder.DataSource = null;
            //    gvcustomerorder.DataBind();
            //}
        }

        protected void GridViewRate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string rate = e.Row.Cells[2].Text;
                decimal rateTotal = Convert.ToDecimal(rate);

                foreach (TableCell gr in e.Row.Cells)
                {
                    if (1 <= rateTotal && rateTotal <= 3)
                    {
                        gr.BackColor = System.Drawing.Color.Red;
                    }

                    if (4 <= rateTotal && rateTotal <= 6)
                    {
                        gr.BackColor = System.Drawing.Color.GreenYellow;
                    }
                    if (7 <= rateTotal && rateTotal <= 10)
                    {
                        gr.BackColor = System.Drawing.Color.Gold;
                    }
                    if (11 <= rateTotal && rateTotal <= 15)
                    {
                        gr.BackColor = System.Drawing.Color.BlueViolet;
                    }
                    if (15 <= rateTotal && rateTotal <= 20)
                    {
                        gr.BackColor = System.Drawing.Color.RosyBrown;
                    }
                }
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (drpmultiunit.SelectedValue == "Select Unit")
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot Number.Thank You!!!')", true);
                //return;
            }

            DataSet drpProcess = new DataSet();

            DataSet drpEmp = new DataSet();
            DataSet dsLotNo = new DataSet();
            if (btnadd.Text == "Save")
            {
                //if (drpmultiunit.SelectedValue == "All")
                //{
                //    dsLotNo = objbs.Select_Lotnewstich();//tblCut
                //}
                //else
                {
                    dsLotNo = objbs.Select_Lotnewstich(drpmultiunit.SelectedValue);//tblCut
                }
            }

            if (btnadd.Text == "Update")
            {
                dsLotNo = objbs.Select_Lotnewstichupdate(drpmultiunit.SelectedValue);

                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //    if (Convert.ToString(e.Row.Cells[4].Text) != "")
                //    {
                //        if (Convert.ToDouble(e.Row.Cells[4].Text) == 0)

                //            e.Row.Visible = false;
                //    }
                //}
            }
            if (btnadd.Text == "Received")
            {
                dsLotNo = objbs.Select_Lotnewstichupdate(drpmultiunit.SelectedValue);
            }



            drpProcess = objbs.SelectAllProcessTypeLotProcess("3");



            //if (drpmultiunit.SelectedValue == "All")
            //{
            //    drpEmp = objbs.SelectEmpName();
            //}
            //else
            //{
            //    drpEmp = objbs.SelectStitchingEmpName(Convert.ToInt32(txtUnitID.Text));
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // DropDownList drplot = (DropDownList)e.Row.FindControl("drpLotno");
                //  DropDownList drpEmp1 = (DropDownList)e.Row.FindControl("drpEmp");

                var ddProcess = (DropDownList)e.Row.FindControl("drpProcess");
                ddProcess.DataSource = drpProcess;
                ddProcess.DataTextField = "ProcessType";
                ddProcess.DataValueField = "ProcessMasterID";
                ddProcess.DataBind();
                ddProcess.Items.Insert(0, "Select Process Type");

                var drplot = (DropDownList)e.Row.FindControl("drpLotno");
                if (dsLotNo.Tables[0].Rows.Count > 0)
                {
                    drplot.DataSource = dsLotNo.Tables[0];
                    drplot.DataTextField = "LotNo";
                    drplot.DataValueField = "cutid";
                    drplot.DataBind();
                    drplot.Items.Insert(0, "Select Lot No");
                }


                //var ddEmp = (DropDownList)e.Row.FindControl("drpEmp");
                //ddEmp.DataSource = drpEmp;
                //ddEmp.DataTextField = "Name";
                //ddEmp.DataValueField = "Employee_Id";
                //ddEmp.DataBind();
                //ddEmp.Items.Insert(0, "Select Employee Name");
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (btnadd.Text == "Received")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not Allow To Delete This Entry.Thank You')", true);
                return;
            }
            else
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
                        drpmultiunit.Enabled = true;
                    }
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

                        DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpLotno");
                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendFQty");
                        TextBox txtremainQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRemainQty");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtrecFQty");
                        TextBox txtRate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRate");
                        TextBox txtBundle = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtBundle");
                        //   DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpEmp");
                        TextBox date = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("date");
                        TextBox Recdate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("Recdate");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Process"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["RecQuantity"] = txtrecFQty.Text;
                        dtCurrentTable.Rows[i - 1]["SendQty"] = txtsendFQty.Text;
                        dtCurrentTable.Rows[i - 1]["RemainQty"] = txtremainQty.Text;
                        dtCurrentTable.Rows[i - 1]["date"] = date.Text;
                        dtCurrentTable.Rows[i - 1]["Recdate"] = date.Text;
                        dtCurrentTable.Rows[i - 1]["LotNo"] = drpLotno.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Bundle"] = txtBundle.Text;
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

        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("LotNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("Process", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtt.Columns.Add(new DataColumn("SendQty", typeof(string)));
            dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
            dtt.Columns.Add(new DataColumn("RecQuantity", typeof(string)));
            dtt.Columns.Add(new DataColumn("date", typeof(string)));
            dtt.Columns.Add(new DataColumn("Recdate", typeof(string)));
            dtt.Columns.Add(new DataColumn("Bundle", typeof(string)));

            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["LotNo"] = string.Empty;
            dr["Process"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["SendQty"] = string.Empty;
            dr["RemainQty"] = string.Empty;
            dr["RecQuantity"] = string.Empty;
            dr["date"] = string.Empty;
            dr["Recdate"] = string.Empty;
            dr["Bundle"] = string.Empty;

            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            gvcustomerorder.DataSource = dtt;
            gvcustomerorder.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("OrderNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("LotNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Process");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SendQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("RemainQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("RecQuantity");
            dttt.Columns.Add(dct);

            dct = new DataColumn("date");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Recdate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Bundle");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["OrderNo"] = "";
            drNew["LotNo"] = "";
            drNew["Process"] = "";
            drNew["Rate"] = "";
            drNew["SendQty"] = "";
            drNew["RemainQty"] = "";
            drNew["Bundle"] = "";
            drNew["RecQuantity"] = "";
            drNew["date"] = DateTime.Now.ToString("dd/MM/yyyy");
            drNew["Recdate"] = DateTime.Now.ToString("dd/MM/yyyy");

            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();

        }

        //protected void txtRange_Change(object sender, EventArgs e)
        //{
        //    ButtonAdd1_Click(sender, e);

        //    int test = 0;


        //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
        //    {
        //        int total = 0;
        //        DropDownList drpProcess =
        //         (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
        //        if (drpProcess.SelectedValue != "Select Process Type")
        //        {
        //            ds = objbs.CheckQuantityOverLoad(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpProcess.SelectedValue));
        //            string ProcessType = "";
        //            test = ds.Tables[0].Rows[0]["ProcessType"].ToString();

        //            if (ds.Tables[0].Rows.Count > 0)
        //            {

        //                ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
        //                for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
        //                {

        //                    DropDownList drpProcessCheck =
        //                     (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
        //                    if (drpProcessCheck.SelectedValue != "Select Process Type")
        //                    {
        //                        if (ProcessType == drpProcessCheck.SelectedItem.Text)
        //                        {
        //                            TextBox txtRecQuantity =
        //                                    (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");
        //                            total = total + Convert.ToInt32(txtRecQuantity.Text);
        //                        }
        //                    }


        //                }
        //                if (total > test)
        //                {
        //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + ProcessType + " has enetered Over Quantity!!!.')", true);
        //                    return;
        //                }
        //            }
        //        }

        //    }

        //}

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {

            if (btnadd.Text == "Received")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not Allow To Add New Row.Thank You!!!')", true);
                return;
            }
            else
            {

                int No = 0;
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");


                    if (drpProcess.SelectedItem.Text == "Select Process Type" && drpLotno.SelectedItem.Text == "Select Lot No")
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
            }
        }

        private void AddNewRow()
        {
            DateTime Recdate1 = DateTime.Now;
            DateTime date1 = DateTime.Now;
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                TextBox txtremainQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRemainQty");
                TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrecFQty");
                TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtBundle = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
                //   DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpEmp");
                TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Recdate");

                // DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

                if (drpProcess.SelectedValue == "Select Process Type")
                {

                }
            }

            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {

                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpLotno");
                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendFQty");
                        TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRemainQty");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtrecFQty");
                        TextBox txtRate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRate");
                        TextBox txtBundle = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtBundle");
                        //   DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpEmp");
                        TextBox date = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("date");
                        if (date.Text == "")
                        {
                            System.Globalization.CultureInfo cultureinfo =
                            new System.Globalization.CultureInfo("nl-NL");
                            date1 = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), cultureinfo);

                        }
                        else
                        {
                            System.Globalization.CultureInfo cultureinfo =
                           new System.Globalization.CultureInfo("nl-NL");
                            date1 = DateTime.Parse(Convert.ToDateTime(date.Text).ToString("dd/MM/yyyy HH:mm:ss"), cultureinfo);
                        }
                        TextBox Recdate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("Recdate");
                        if (Recdate.Text == "")
                        {
                            System.Globalization.CultureInfo cultureinfo =
                            new System.Globalization.CultureInfo("nl-NL");
                            Recdate1 = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), cultureinfo);

                        }
                        else
                        {
                            System.Globalization.CultureInfo cultureinfo =
                          new System.Globalization.CultureInfo("nl-NL");
                            Recdate1 = DateTime.Parse(Convert.ToDateTime(Recdate.Text).ToString("dd/MM/yyyy HH:mm:ss"), cultureinfo);
                        }

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Process"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["RecQuantity"] = txtrecFQty.Text;
                        dtCurrentTable.Rows[i - 1]["SendQty"] = txtsendFQty.Text;
                        dtCurrentTable.Rows[i - 1]["RemainQty"] = txtsendFQty.Text;
                        dtCurrentTable.Rows[i - 1]["date"] = date1.ToString();
                        dtCurrentTable.Rows[i - 1]["Recdate"] = Recdate1;
                        dtCurrentTable.Rows[i - 1]["LotNo"] = drpLotno.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Bundle"] = txtBundle.Text;
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

            //for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //{
            //    DropDownList drpProcess =
            // (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpProcess");

            //    drpProcess.Focus();
            //}
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                //  TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("date");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("date");
                    if (oldtxttk.Text == "")
                    {
                        oldtxttk.Text = "";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }
                //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            }
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

                        DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpLotno");
                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendFQty");
                        TextBox txtremainQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRemainQty");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtrecFQty");
                        TextBox txtRate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRate");
                        TextBox txtBundle = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtBundle");
                        //  DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpEmp");
                        TextBox date = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("date");
                        TextBox Recdate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("Recdate");

                        drpProcess.SelectedValue = dt.Rows[i]["Process"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtrecFQty.Text = dt.Rows[i]["RecQuantity"].ToString();
                        drpLotno.SelectedValue = dt.Rows[i]["LotNo"].ToString();
                        date.Text = dt.Rows[i]["date"].ToString();
                        Recdate.Text = dt.Rows[i]["Recdate"].ToString();
                        txtBundle.Text = dt.Rows[i]["Bundle"].ToString();
                        txtsendFQty.Text = dt.Rows[i]["SendQty"].ToString();
                        txtremainQty.Text = dt.Rows[i]["RemainQty"].ToString();

                        rowIndex++;
                        drpLotno.Focus();
                    }
                }
            }
        }

        protected void Add_LotProcessDetails(object sender, EventArgs e)
        {
            if (drpmultiunit.SelectedValue == "Select Unit")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Unit Name.Thank You!!!')", true);
                return;
            }

            string drec = DateTime.Now.ToString("dd/MM/yyyy");
           // DateTime dateonly = DateTime.ParseExact(drec, "dd/MM/yyyy", CultureInfo.InvariantCulture);

           // DateTime dateonly = DateTime.ParseExact(drec.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

           DateTime dateonly= DateTime.Parse(Convert.ToDateTime (drec.Trim()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));


            if (drpMultiemployee.SelectedValue == "Select Employee Name")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name.Thank You!!!.')", true);
                return;
            }

            if (txtmultidate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Date')", true);
                return;
            }

            if (btnadd.Text == "Update")
            {
                //int isss = objbs.PreupdatemultiTransProcess(txtid.Text);

            }
            if (btnadd.Text == "Received")
            {


                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");

                    if (Convert.ToDouble(txtsendFQty.Text) < Convert.ToDouble(txtrecQty.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Received Qty is not Greater than That Remain Qty!!!')", true);
                        txtrecQty.Focus();
                        return;
                    }

                }
            }

            if (btnadd.Text != "Received")
            {
                int iq = 1;
                int iii = 1;
                string itemc = string.Empty;
                string itemd = string.Empty;
                string iteme = string.Empty;
                string itemcd = string.Empty;
                if (ViewState["CurrentTable1"] != null)
                {
                    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                    {
                        DropDownList drplotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                        if (drpprocess.SelectedValue != "Select Process Type")
                        {

                            TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");

                            TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                            if (date.Text == "")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                                return;
                            }

                            //   DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //   DateTime recDate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                            itemc = drpprocess.SelectedValue;
                            // itemd = recDate.ToString("dd/MM/yyyy");
                            iteme = drplotno.SelectedValue;


                            if ((itemc == null) || (itemc == ""))
                            {
                            }
                            else
                            {
                                for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                                {
                                    DropDownList drplotno1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpLotno");
                                    DropDownList drpprocesss = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpProcess");
                                    if (drpprocesss.SelectedValue != "Select Process Type")
                                    {
                                        // DropDownList drpEmpp = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpEmp");
                                        TextBox datee = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("date");
                                        // DateTime recDatee = DateTime.ParseExact(datee.Text, "dd/MM/yyyy HH:MM:ss", CultureInfo.InvariantCulture);

                                        {

                                            if (iii == iq)
                                            {
                                            }
                                            else
                                            {
                                                if (itemc == drpprocesss.Text && iteme == drplotno1.SelectedValue)
                                                {
                                                    itemcd = drpprocess.SelectedItem.Text;
                                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "," + drpEmpp.SelectedItem.Text + ",'" + recDatee.ToString("dd/MM/yyyy") + "'  already exists in the Grid.');", true);
                                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd.Trim() + "," + drplotno1.SelectedItem.Text.Trim() + "already exists in the Grid.');", true);
                                                    drpprocess.Focus();
                                                    // drpEmpp.Focus();
                                                    return;

                                                }
                                            }
                                            iii = iii + 1;
                                        }
                                    }
                                }
                            }
                            iq = iq + 1;
                            iii = 1;
                        }
                    }


                    string curent = string.Empty;
                    string namee = string.Empty;
                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {
                        DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                        if (drpLotprocess.SelectedValue == "Select Lot No")
                        {
                        }
                        else
                        {
                            DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                            TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                            TextBox txtbundle = (TextBox)gvcustomerorder.Rows[i].FindControl("txtBundle");
                            TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                            txtrecFQty.Text = "0";
                            txtrecFQty.Enabled = false;
                            string temp = drpprocess.SelectedValue;
                            double qty = 0;
                            for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                            {
                                DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                                DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                                curent = process.SelectedValue;
                                namee = process.SelectedItem.Text;
                                TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendFQty");


                                if (process.SelectedValue == temp)
                                {
                                    qty = qty + Convert.ToDouble(txtsendFQty1.Text);
                                }
                            }

                            if (drpLotprocess.SelectedValue == "Select Lot No")
                            {
                            }
                            else
                            {
                                if (objbs.CheckIfrecqtyinstiching(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + drpprocess.SelectedItem.Text + " has Exists received Quantity.')", true);
                                    txtsendFQty.Focus();
                                    return;


                                }
                            }
                        }

                    }
                }


                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                    if (drpprocess.SelectedValue != "Select Process Type")
                    {

                        TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                        if (txtsendQty.Text == "")
                        {
                            txtsendQty.Text = "0";
                        }
                        if (txtsendQty.Text == "0")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Quantity.Thank You!!!.')", true);
                            txtsendQty.Focus();
                            return;
                        }
                        TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                        if (date.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                            return;
                        }
                    }
                }
            }

            if (btnadd.Text == "Save")
            {
                DateTime MultiDate = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                int istatus3 = objbs.insertmultiProcess(txtmultiid.Text, txtmultiplecode.Text, drpmultiunit.SelectedValue, MultiDate, drpMultiemployee.SelectedValue, txttotalqty.Text);



                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                    TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRemainQty");
                    TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrecFQty");
                    TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txtBundle = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");
                    TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                    TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Recdate");
                    if (drpLotno.SelectedValue == "Select Lot No")
                    {

                    }
                    else
                    {
                        int transsales = objbs.insertmultiTransProcess(drpLotno.SelectedItem.Text, drpLotno.SelectedValue, drpProcess.SelectedValue, txtsendFQty.Text, date.Text, txtBundle.Text, txtrecFQty.Text, Recdate.Text, txtRate.Text, txtsendFQty.Text);
                    }
                }



            }
            else if (btnadd.Text == "Update")
            {
                DataSet dcheckk = objbs.getrecevedornot(txtid.Text);
                if (dcheckk.Tables[0].Rows.Count > 0)
                {

                    int isss = objbs.PreupdatemultiTransProcess(txtid.Text);


                    DateTime MultiDate = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    int istatus3 = objbs.UpdatemultiProcess(txtmultiid.Text, txtmultiplecode.Text, drpmultiunit.SelectedValue, MultiDate, drpMultiemployee.SelectedValue, txttotalqty.Text, txtid.Text);
                    int ii = objbs.DeletemultiTransProcess(txtid.Text);
                    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                    {
                        DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrecFQty");
                        TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                        TextBox txtBundle = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");

                        TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                        TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Recdate");



                        if (drpLotno.SelectedValue == "Select Lot No")
                        {

                        }
                        else
                        {
                            int transsales = objbs.updatemultiTransProcess(drpLotno.SelectedItem.Text, drpLotno.SelectedValue, drpProcess.SelectedValue, txtsendFQty.Text, date.Text, txtBundle.Text, txtrecFQty.Text, Recdate.Text, txtRate.Text, txtid.Text);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Administrator.Because Already Received some qty for this Process.')", true);
                    return;
                }
            }
            else if (btnadd.Text == "Received")
            {
                //DataSet ds = new DataSet();
                //DataTable dt = new DataTable();

                //DataRow dr;

               
                //dt.Columns.Add(new DataColumn("LotNo", typeof(int)));
                //dt.Columns.Add(new DataColumn("Cutid", typeof(int)));
                //dt.Columns.Add(new DataColumn("Processid", typeof(string)));
                //dt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                //dt.Columns.Add(new DataColumn("RecDate", typeof(int)));
                int istatus3 = objbs.ReceivedmultiProcess(txtreceivedQty.Text, txtid.Text);

                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                    TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrecFQty");
                    TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txtBundle = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtBundle");

                    TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                    TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Recdate");
                    Recdate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                    if (drpLotno.SelectedValue != "Select Lot No")
                    {



                        int iscc = objbs.ReceivemultiTransProcess(txtid.Text, drpmultiunit.SelectedValue, drpMultiemployee.SelectedValue, drpLotno.SelectedItem.Text, drpLotno.SelectedValue, drpProcess.SelectedValue, txtrecFQty.Text, Recdate.Text, txtRate.Text, dateonly);

                        int transsales = objbs.insertmultiTransProcesshistory(drpLotno.SelectedItem.Text, drpLotno.SelectedValue, drpProcess.SelectedValue, txtsendFQty.Text, date.Text, txtBundle.Text, txtrecFQty.Text, Recdate.Text, txtRate.Text, txtsendFQty.Text,txtid.Text);

                        //dr = dt.NewRow();
                        //dr["LotNo"] = drpLotno.SelectedItem.Text;
                        //dr["Cutid"] = drpLotno.SelectedValue;
                        //dr["Processid"] = drpProcess.SelectedValue;
                        //dr["RecQty"] = txtrecFQty.Text;
                        //dr["RecDate"] = date1;
                        //dt.Rows.Add(dr);
                    }
                }

              //  ds.Merge(dt);



            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Administrator.Because Some process Received.')", true);
                return;
            }



            System.Threading.Thread.Sleep(3000);

            Response.Redirect("DilloMultiLotGrid.aspx");


        }

        //protected void drpprocess_selected(object sender, EventArgs e)
        //{
        //    DropDownList ddl = (DropDownList)sender;
        //    GridViewRow row = (GridViewRow)ddl.NamingContainer;
        //    DropDownList ddlprocess = (DropDownList)row.FindControl("drpProcess");
        //    TextBox txtrate = (TextBox)row.FindControl("txtRate");


        //    DataSet ds = new DataSet();
        //    if (ddlprocess.SelectedValue != "Select Process Type")
        //    {
        //        ds = objbs.getrateforstiching(ddlprocess.SelectedValue, ddlLotNo.SelectedValue);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            txtrate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
        //        }
        //    }

        //}

        protected void GridViewWork_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell gr in e.Row.Cells)
                {
                    if (gr.Text == "YES")
                    {
                        gr.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        gr.BackColor = System.Drawing.Color.Red;
                    }

                    if (gr.Text == "True")
                    {
                        gr.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        gr.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

    }
}