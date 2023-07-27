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
    public partial class NDillo_Stiching : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            string lot = Request.QueryString.Get("lotid");
            string name = Request.QueryString.Get("name");
            if (!IsPostBack)
            {


                DataSet drpEmp = new DataSet();
                drpEmp = objbs.NewSelectSupervosorName("2");
                if (drpEmp.Tables[0].Rows.Count > 0)
                {
                    drpemployee.DataSource = drpEmp;
                    drpemployee.DataTextField = "Name";
                    drpemployee.DataValueField = "Employee_Id";
                    drpemployee.DataBind();
                    drpemployee.Items.Insert(0, "Select Employee Name");
                }


                // DataSet drpProcess = new DataSet();

                //  divWork.Visible = false;
                if (lot != null)
                {
                    if (name == "Received")
                    {
                        btnadd.Text = "Received";

                        txtnewlotprocess.Text = lot;

                        DataSet dgetcheck = objbs.Stichinginfogridupdate(lot);
                        if (dgetcheck.Tables[0].Rows.Count > 0)
                        {
                            btnadd.Visible = true;
                            // btnadd.Text = "Update";
                            drpemployee.SelectedValue = dgetcheck.Tables[0].Rows[0]["EmployeeId"].ToString();
                            drpemployee.Enabled = false;
                            txtstichdate.Text = Convert.ToDateTime(dgetcheck.Tables[0].Rows[0]["LotDate"]).ToString("dd/MM/yyyy");
                            //  ddlLotNo.SelectedValue = dgetcheck.Tables[0].Rows[0]["cutid"].ToString();
                            //  StitchingInfo_Loadforupdate(sender, e);
                            DataSet ds = objbs.transStichinginfogridupdate(lot);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                DataSet temp = new DataSet();
                                DataTable dtt = new DataTable();

                                dtt.Columns.Add(new DataColumn("LotNo", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                                dtt.Columns.Add(new DataColumn("ProcessType", typeof(string)));
                                dtt.Columns.Add(new DataColumn("AvaQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("SendQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("RemQty", typeof(string)));
                                dtt.Columns.Add(new DataColumn("date", typeof(string)));
                                dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                                temp.Tables.Add(dtt);
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {

                                    DataRow dr = dtt.NewRow();
                                    dr["Transid"] = ds.Tables[0].Rows[i]["transid"].ToString();
                                    dr["LotNo"] = ds.Tables[0].Rows[i]["Lotdetailid"].ToString();
                                    dr["ProcessType"] = ds.Tables[0].Rows[i]["processid"].ToString();
                                    dr["AvaQty"] = ds.Tables[0].Rows[i]["rem"].ToString();
                                    dr["SendQty"] = ds.Tables[0].Rows[i]["RemainQty"].ToString();
                                    dr["RemQty"] = ds.Tables[0].Rows[i]["receivedQty"].ToString();
                                    dr["date"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["date"]).ToString("dd/MM/yyyy");
                                    dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();

                                    //  dt.Rows.Add(dr);
                                    temp.Tables[0].Rows.Add(dr);
                                }

                                ViewState["CurrentTable1"] = dtt;

                                gvcustomerorder.DataSource = temp;
                                gvcustomerorder.DataBind();

                                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                {
                                    Label lbltrans = (Label)gvcustomerorder.Rows[i].FindControl("lbltransid");

                                    DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[i].FindControl("drplotno");
                                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                                    TextBox txtRate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                                    TextBox txtavaQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtavaQuantity");
                                    TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");

                                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                                    TextBox txtsendQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendQuantity");
                                    // drpprocess.Enabled = false;
                                    date.Enabled = true;
                                    // txtrate.Enabled = false;
                                    txtsendQuantity.Enabled = false;
                                    drpLotno.Enabled = false;
                                    drpProcess.Enabled = false;
                                    txtRecQuantity.Enabled = true;
                                    // lbltrans.Text = temp.Tables[0].Rows[i]["OrderNo"].ToString();
                                    lbltrans.Text = temp.Tables[0].Rows[i]["Transid"].ToString();
                                    drpLotno.SelectedValue = temp.Tables[0].Rows[i]["LotNo"].ToString();
                                    drpProcess.SelectedValue = temp.Tables[0].Rows[i]["ProcessType"].ToString();
                                    txtRate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();
                                    txtavaQuantity.Text = temp.Tables[0].Rows[i]["AvaQty"].ToString();
                                    txtsendQuantity.Text = temp.Tables[0].Rows[i]["SendQty"].ToString();
                                    txtRecQuantity.Text = "0";
                                    date.Text = temp.Tables[0].Rows[i]["date"].ToString();
                                }
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Something Went Wrong.Please Check Lot No.Or Contact Administrator.')", true);
                        return;
                    }
                }
                else
                {

                    FirstGridViewRow();
                }
                // Detail_checked(sender, e);
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

        }
        protected void btngvadd_Click(object sender, EventArgs e)
        {
            //{

            //    if ((txtsendqty.Text == "0") || (txtsendqty.Text == ""))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Sending Qty')", true);
            //        txtsendqty.Focus();
            //        return;
            //    }
            //    if ((txtdate.Text == "0") || (txtdate.Text == ""))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check Date')", true);
            //        txtdate.Focus();
            //        return;
            //    }




            //    DataSet dstd = new DataSet();
            //    DataTable dtddd = new DataTable();


            //    DataTable dttt;
            //    DataRow drNew;
            //    DataColumn dct;

            //    dttt = new DataTable();



            //    dct = new DataColumn("SNO");
            //    dttt.Columns.Add(dct);

            //    dct = new DataColumn("LotdetailId");
            //    dttt.Columns.Add(dct);

            //    dct = new DataColumn("LotNo");
            //    dttt.Columns.Add(dct);

            //    dct = new DataColumn("ProcessId");
            //    dttt.Columns.Add(dct);

            //    dct = new DataColumn("ProcessType");
            //    dttt.Columns.Add(dct);

            //    dct = new DataColumn("SendQty");
            //    dttt.Columns.Add(dct);

            //    dct = new DataColumn("ReceivedQty");
            //    dttt.Columns.Add(dct);

            //    dct = new DataColumn("date");
            //    dttt.Columns.Add(dct);

            //    dct = new DataColumn("Rate");
            //    dttt.Columns.Add(dct);

            //    dstd.Tables.Add(dttt);


            //    if (ViewState["CurrentTable1"] != null)
            //    {
            //        DataTable dt = (DataTable)ViewState["CurrentTable1"];

            //        drNew = dttt.NewRow();
            //        drNew["Sno"] = "1";
            //        drNew["LotdetailId"] = drpLotno.SelectedValue;
            //        drNew["LotNo"] = drpLotno.SelectedItem.Text;
            //        drNew["ProcessId"] = drprprocesstype.SelectedValue;
            //        drNew["ProcessType"] = drprprocesstype.SelectedItem.Text;
            //        drNew["SendQty"] = txtsendqty.Text;
            //        drNew["ReceivedQty"] = "0";
            //        drNew["date"] = txtdate.Text;
            //        drNew["Rate"] = txtrate.Text;


            //        dstd.Tables[0].Rows.Add(drNew);
            //        dtddd = dstd.Tables[0];

            //        dtddd.Merge(dt);


            //    }
            //    else
            //    {
            //        drNew = dttt.NewRow();
            //        drNew["Sno"] = "1";
            //        drNew["LotdetailId"] = drpLotno.SelectedValue;
            //        drNew["LotNo"] = drpLotno.SelectedItem.Text;
            //        drNew["ProcessId"] = drprprocesstype.SelectedValue;
            //        drNew["ProcessType"] = drprprocesstype.SelectedItem.Text;
            //        drNew["SendQty"] = txtsendqty.Text;
            //        drNew["ReceivedQty"] = "0";
            //        drNew["date"] = txtdate.Text;
            //        drNew["Rate"] = txtrate.Text;


            //        dstd.Tables[0].Rows.Add(drNew);
            //        dtddd = dstd.Tables[0];
            //    }

            //    ViewState["CurrentTable1"] = dtddd;

            //    gvcustomerorder.DataSource = dtddd;
            //    gvcustomerorder.DataBind();

            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        Label lotdetailid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lbltransid");
            //        TextBox Lotno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Lotno");
            //        TextBox processtype = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("processtype");
            //        TextBox processid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("processid");
            //        TextBox txtsendQuantity = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendQuantity");
            //        TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRecQuantity");
            //        TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
            //        TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

            //        if (dstd.Tables[0].Rows[vLoop]["LotdetailId"].ToString() == "")
            //        {

            //        }
            //        else
            //        {

            //            lotdetailid.Text = dstd.Tables[0].Rows[vLoop]["LotdetailId"].ToString();
            //            Lotno.Text = dstd.Tables[0].Rows[vLoop]["LotNo"].ToString();
            //            processtype.Text = dstd.Tables[0].Rows[vLoop]["ProcessType"].ToString();
            //            processid.Text = dstd.Tables[0].Rows[vLoop]["ProcessId"].ToString();
            //            txtsendQuantity.Text = dstd.Tables[0].Rows[vLoop]["SendQty"].ToString();
            //            txtRecQuantity.Text = dstd.Tables[0].Rows[vLoop]["ReceivedQty"].ToString();
            //            date.Text = dstd.Tables[0].Rows[vLoop]["date"].ToString();
            //            txtRate.Text = dstd.Tables[0].Rows[vLoop]["Rate"].ToString();


            //        }
            //    }


            //    drpLotno.SelectedIndex = 0;
            //    // drprpoduct.SelectedItem.Text;
            //    drprprocesstype.SelectedIndex = 0;
            //    // drpcolor.SelectedItem.Text;

            //    txtavaqty.Text = "0";
            //    txtsendqty.Text = "0";
            //    txtdate.Text = "";
            //    txtrate.Text = "0";
            //    txtSno.Focus();
            //    //txtSno.Text = (Convert.ToInt32(txtSno.Text) + 1).ToString();
            //    //  txtSno.Focus();
            //    //granddiscount();

            //}
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
        protected void LotIndex_Chnaged(object sender, EventArgs e)
        {


            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList drplotno = (DropDownList)row.FindControl("drplotno");
            DropDownList drpProcess = (DropDownList)row.FindControl("drpProcess");
            // TextBox txtrate = (TextBox)row.FindControl("txtRate");


            DataSet ds = new DataSet();
            if (drplotno.SelectedValue != "Select Lot No")
            {
                DataSet dlotno = objbs.NewStichingprocess(drplotno.SelectedValue);
                if (dlotno.Tables[0].Rows.Count > 0)
                {
                    txtCuttingMaster.Text = dlotno.Tables[0].Rows[0]["LedgerName"].ToString();
                    txtBrand.Text = dlotno.Tables[0].Rows[0]["brandname"].ToString();
                    txtProcessDate.Text = dlotno.Tables[0].Rows[0]["processdate"].ToString();
                    txtHalf.Text = dlotno.Tables[0].Rows[0]["halfqty"].ToString();
                    txtfull.Text = dlotno.Tables[0].Rows[0]["fullqty"].ToString();
                    txtTotalQantity.Text = dlotno.Tables[0].Rows[0]["totalquantity"].ToString();
                    txtdesignno.Text = dlotno.Tables[0].Rows[0]["DesignNo"].ToString();
                    txtUnitName.Text = dlotno.Tables[0].Rows[0]["unitname"].ToString();

                    drpProcess.Items.Clear();
                    drpProcess.ClearSelection();
                    drpProcess.DataSource = dlotno;
                    drpProcess.DataTextField = "ProcessType";
                    drpProcess.DataValueField = "ProcessMasterID";
                    drpProcess.DataBind();
                    drpProcess.Items.Insert(0, "Select Process Type");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Something Went Wrong.Please Check Lot Number Or Contact Administrator.')", true);
                    return;
                }
            }

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {

                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                drpprocess.Focus();

            }




            // DataSet dlotno = objbs.NewStichingprocess("1");
            //// l.ledgername,b.brandname,ld.processdate,ld.halfqty,ld.fullqty,ld.totalquantity,ld.DesignNo,pm.processmasterid,pm.processtype

            // if (dlotno.Tables[0].Rows.Count > 0)
            // {
            //     txtCuttingMaster.Text = dlotno.Tables[0].Rows[0]["LedgerName"].ToString();
            //     txtBrand.Text = dlotno.Tables[0].Rows[0]["brandname"].ToString();
            //     txtProcessDate.Text = dlotno.Tables[0].Rows[0]["processdate"].ToString();
            //     txtHalf.Text = dlotno.Tables[0].Rows[0]["halfqty"].ToString();
            //     txtfull.Text = dlotno.Tables[0].Rows[0]["fullqty"].ToString();
            //     txtTotalQantity.Text = dlotno.Tables[0].Rows[0]["totalquantity"].ToString();
            //     txtdesignno.Text = dlotno.Tables[0].Rows[0]["DesignNo"].ToString();
            //     txtUnitName.Text = dlotno.Tables[0].Rows[0]["unitname"].ToString();



            //     //drprprocesstype.DataSource = dlotno;
            //     //drprprocesstype.DataTextField = "ProcessType";
            //     //drprprocesstype.DataValueField = "ProcessMasterID";
            //     //drprprocesstype.DataBind();
            //     //drprprocesstype.Items.Insert(0, "Select Process Type");
            // }
            // else
            // {
            //     ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Something Went Wrong.Please Check Lot Number Or Contact Administrator.')", true);
            //     return;
            // }





        }
        protected void Process_TypeIndex(object sender, EventArgs e)
        {

            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList drplotnoo1 = (DropDownList)row.FindControl("drplotno");
            DropDownList drpProcess = (DropDownList)row.FindControl("drpProcess");
            TextBox txtrate1 = (TextBox)row.FindControl("txtRate");
            TextBox avaqty = (TextBox)row.FindControl("txtavaQuantity");


            DataSet ds = new DataSet();
            if (drplotnoo1.SelectedValue != "Select Lot No")
            {
                if (drpProcess.SelectedValue != "Select Process Type")
                {
                    ds = objbs.GetProcessTypeQty(drplotnoo1.SelectedValue, drpProcess.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        avaqty.Text = ds.Tables[0].Rows[0]["RemainQty"].ToString();
                        txtrate1.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                    }
                }
            }

            int iq = 1;
            int iii = 1;
            int iq1 = 1;
            int iii1 = 1;
            string itemc = string.Empty;

            string iteme = string.Empty;
            string itemcd = string.Empty;
            if (btnadd.Text == "Save")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drplotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");

                    if (drpprocess.SelectedValue != "Select Process Type")
                    {

                        TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendQuantity");



                        //   DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //   DateTime recDate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                        itemc = drpprocess.SelectedValue;

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

                                   // TextBox datee = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("date");


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
            }



            ButtonAdd1_Click(sender, e);



            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                //  TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendQuantity");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtsendQuantity");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtsendQuantity");
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

            //DataSet ds = new DataSet();

            //ds = objbs.GetProcessTypeQty(drpLotno.SelectedValue, drprprocesstype.SelectedValue);
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    txtavaqty.Text = ds.Tables[0].Rows[0]["RemainQty"].ToString();
            //    txtrate.Text= ds.Tables[0].Rows[0]["RemainQty"].ToString();
            //}

        }
        protected void txtrecqtychnaged_text(object sender, EventArgs e)
        {
            //if (btnadd.Text == "Save")
            //{
            //    string curent = string.Empty;
            //    string namee = string.Empty;
            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {
            //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
            //        if (drpprocess.SelectedValue != "Select Process Type")
            //        {
            //            TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
            //            TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
            //            string temp = drpprocess.SelectedValue;
            //            double qty = 0;
            //            for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
            //            {

            //                DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
            //                curent = process.SelectedValue;
            //                namee = process.SelectedItem.Text;
            //                TextBox RecQuantity = (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");

            //                if (process.SelectedValue == temp)
            //                {
            //                    qty = qty + Convert.ToDouble(RecQuantity.Text);
            //                }
            //            }
            //            //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
            //            //{
            //            //}
            //            if (objbs.CheckIfrecqtyinstiching(ddlLotNo.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
            //            {
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee + " has Exists received Quantity.')", true);
            //                txtRecQuantity.Focus();
            //                return;
            //                // lblerror.Text = "These Category has already Exists. please enter a new one";

            //            }
            //            date.Focus();
            //        }


            //    }
            //}
            //else
            //{

            //    string curent = string.Empty;
            //    string namee = string.Empty;
            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {
            //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
            //        if (drpprocess.SelectedValue != "Select Process Type")
            //        {
            //            TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
            //            TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
            //            string temp = drpprocess.SelectedValue;
            //            double qty = 0;
            //            for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
            //            {

            //                DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
            //                curent = process.SelectedValue;
            //                namee = process.SelectedItem.Text;
            //                TextBox RecQuantity = (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");

            //                if (process.SelectedValue == temp)
            //                {
            //                    qty = qty + Convert.ToDouble(RecQuantity.Text);
            //                }
            //            }
            //            //for (int z = 0; z < gvcustomerorder.Rows.Count; z++)
            //            //{
            //            //}
            //            if (objbs.CheckIfrecqtyinstichingupdate(ddlLotNo.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
            //            {
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee.Trim() + " has Exists received Quantity.')", true);
            //                txtRecQuantity.Focus();
            //                return;
            //                // lblerror.Text = "These Category has already Exists. please enter a new one";

            //            }
            //            date.Focus();
            //        }
            //    }
            //}
            //AddNewRow();
        }

        protected void drpprocess_selected(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //DropDownList ddlprocess = (DropDownList)row.FindControl("drpProcess");
            //TextBox txtrate = (TextBox)row.FindControl("txtRate");


            //DataSet ds = new DataSet();
            //if (ddlprocess.SelectedValue != "Select Process Type")
            //{
            //    ds = objbs.getrateforstiching(ddlprocess.SelectedValue, ddlLotNo.SelectedValue);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        txtrate.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Rate"]).ToString();
            //    }
            //}

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{

            //    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //    drpprocess.Focus();

            //}
            //  ScriptManager.RegisterStartupScript(this, GetType(), "Myscript", "openNav();", true);
            // ButtonAdd1_Click(sender, e);
        }
        //protected void Detail_checked(object sender, EventArgs e)
        //{
        //    //if (ddlLotNo.SelectedValue != "Select Lot No")
        //    //{
        //    //    DataSet dlotprocess = new DataSet();
        //    //    dlotprocess = objbs.getprocessdetailsforstic(ddlLotNo.SelectedValue);
        //    //    if (dlotprocess.Tables[0].Rows.Count > 0)
        //    //    {
        //    //        GridView1.DataSource = dlotprocess;
        //    //        GridView1.DataBind();
        //    //    }
        //    //    GridView2.Visible = false;
        //    //    processs.Visible = true;
        //    //    ratee.Visible = false;
        //    //    GridView1.Visible = true;
        //    //    Ratedetail.Checked = false;
        //    //    DetailView.Checked = true;
        //    //    //  mpe.Show();
        //    //    //  DetailView.Checked = false;
        //    //}
        //}

        //protected void RateDetail_checked(object sender, EventArgs e)
        //{
        //    //if (ddlLotNo.SelectedValue != "Select Lot No")
        //    //{
        //    //    DataSet drpProcess = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(ddlLotNo.SelectedValue));
        //    //    if (drpProcess.Tables[0].Rows.Count > 0)
        //    //    {
        //    //        GridView2.DataSource = drpProcess;
        //    //        GridView2.DataBind();
        //    //    }
        //    //    GridView1.Visible = false;
        //    //    GridView2.Visible = true;
        //    //    processs.Visible = false;
        //    //    ratee.Visible = true;
        //    //    //  mpe1.Show();
        //    //    Ratedetail.Checked = true;
        //    //    DetailView.Checked = false;
        //    //}
        //}


        protected void StitchingInfo_Loadforupdate(object sender, EventArgs e)
        {
            ////DataSet dataSet = objbs.getLotNoTransDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
            ////if (dataSet.Tables[0].Rows.Count > 0)
            ////{
            ////    txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
            ////    txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();

            ////    txtUnitName.Text = dataSet.Tables[0].Rows[0]["UnitName"].ToString();
            ////    txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString();

            ////    txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
            ////    txtbrandid.Text = dataSet.Tables[0].Rows[0]["BrandID"].ToString();
            ////    txtUnitID.Text = dataSet.Tables[0].Rows[0]["UnitID"].ToString();
            ////    txtHalf.Text = dataSet.Tables[0].Rows[0]["HalfQty"].ToString();
            ////    txtfull.Text = dataSet.Tables[0].Rows[0]["FullQty"].ToString();
            ////    txtdesignno.Text = dataSet.Tables[0].Rows[0]["Designno"].ToString();
            ////    string processDate = dataSet.Tables[0].Rows[0]["ProcessDate"].ToString();
            ////    if (processDate == "")
            ////    {
            ////        DateTime date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ////        txtProcessDate.Text = date.ToString("dd/MM/yyyy");
            ////    }
            ////    else
            ////    {
            ////        DateTime date = DateTime.ParseExact(Convert.ToDateTime(processDate).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ////        txtProcessDate.Text = date.ToString("dd/MM/yyyy");
            ////        //        CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            ////    }
            //    //txtProcessDate.Text = DateTime.ParseExact(processDate, "dd/MM/yyyy hh:mm:ss tt",
            //    //            CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            //    //DateTime processDate1 = DateTime.ParseExact(processDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //    //txtProcessDate.Text = processDate1.ToString();
            //    string lotno = "0";
            //    //if (ddlLotNo.SelectedValue == "Select Lot No")
            //    //{
            //    //    lotno = "0";
            //    //}
            //    //else
            //    //{
            //    //    lotno = ddlLotNo.SelectedValue;
            //    //}

            //    DataSet drpProcess = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(lotno));
            //    DataSet drpEmp = objbs.SelectStitchingEmpName(Convert.ToInt32(txtUnitID.Text));





            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {

            //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //        drpprocess.Focus();

            //    }
            //}



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

            //    DataSet drpProcess = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(lotno));
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
        }



        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet drpProcess = new DataSet();
            string lotno = "0";


            DataSet drpEmp = new DataSet();
            DataSet dsLotNo = new DataSet();
            if (btnadd.Text == "Save")
            {
                dsLotNo = objbs.Select_Lotnewstich();
            }
            else
            {
                dsLotNo = objbs.Select_LotnewstichReceived("3");
            }
            drpProcess = objbs.SelectProcessType();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList drpProcess1 = (DropDownList)e.Row.FindControl("drpProcess");


                var ddProcess = (DropDownList)e.Row.FindControl("drpProcess");
                ddProcess.DataSource = drpProcess;
                ddProcess.DataTextField = "ProcessType";
                ddProcess.DataValueField = "ProcessMasterID";
                ddProcess.DataBind();
                ddProcess.Items.Insert(0, "Select Process Type");


                var drplot = (DropDownList)e.Row.FindControl("drplotno");
                drplot.DataSource = dsLotNo;
                drplot.DataTextField = "LotNo";
                drplot.DataValueField = "Lotdetailid";
                drplot.DataBind();
                drplot.Items.Insert(0, "Select Lot No");
            }
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

                        DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drplotno");

                        DropDownList drpProcess =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txtRate");

                        TextBox txtavaQuantity = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtavaQuantity");
                        TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRecQuantity");

                        TextBox date = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("date");
                        TextBox txtsendQuantity = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendQuantity");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Lotno"] = drpLotno.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ProcessType"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["AvaQty"] = txtavaQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["SendQty"] = txtsendQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["RemQty"] = txtRecQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["date"] = date.Text;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
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
            dtt.Columns.Add(new DataColumn("LotNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("ProcessType", typeof(string)));
            dtt.Columns.Add(new DataColumn("AvaQty", typeof(string)));
            dtt.Columns.Add(new DataColumn("SendQty", typeof(string)));
            dtt.Columns.Add(new DataColumn("RemQty", typeof(string)));
            dtt.Columns.Add(new DataColumn("date", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));

            dr = dtt.NewRow();
            dr["LotNo"] = string.Empty;
            dr["ProcessType"] = string.Empty;
            dr["AvaQty"] = string.Empty;
            dr["SendQty"] = string.Empty;
            dr["RemQty"] = string.Empty;
            dr["date"] = string.Empty;
            dr["Rate"] = string.Empty;

            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            gvcustomerorder.DataSource = dtt;
            gvcustomerorder.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("LotNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("ProcessType");
            dttt.Columns.Add(dct);

            dct = new DataColumn("AvaQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("SendQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("RemQty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("date");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["LotNo"] = "";
            drNew["ProcessType"] = "";
            drNew["AvaQty"] = "";
            drNew["SendQty"] = "";
            drNew["RemQty"] = "";
            drNew["date"] = DateTime.Now.ToString("dd/MM/yyyy");
            drNew["Rate"] = "";

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
                    DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drplotno");


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
            string dd = DateTime.Now.ToString("dd/MM/yyyy");

            string date1 = DateTime.ParseExact(dd, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drplotno");
                DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtavaQuantity = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtavaQuantity");
                TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRecQuantity");

                TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                TextBox txtsendQuantity = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendQuantity");


                if (drpProcess.SelectedValue == "Select Process")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Process')", true);
                    //  txt.Focus();
                    return;
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

                        DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drplotno");

                        DropDownList drpProcess =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txtRate");

                        TextBox txtavaQuantity = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtavaQuantity");
                        TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRecQuantity");

                        TextBox date = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("date");
                        if (date.Text == "")
                        {
                            //System.Globalization.CultureInfo cultureinfo =
                            //new System.Globalization.CultureInfo("nl-NL");
                            //date1 = DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy"), cultureinfo);
                            // date1 = DateTime.ParseExact(date1.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //  date1 = date1;

                        }
                        else
                        {
                            // System.Globalization.CultureInfo cultureinfo =
                            //new System.Globalization.CultureInfo("nl-NL");
                            // date1 = DateTime.Parse(Convert.ToDateTime(date.Text).ToString("dd/MM/yyyy"), cultureinfo);
                            //date1 = DateTime.ParseExact(Convert.ToDateTime(date.Text).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            date1 = date.Text;
                        }
                        TextBox txtsendQuantity = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendQuantity");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Lotno"] = drpLotno.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ProcessType"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["AvaQty"] = txtavaQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["SendQty"] = txtsendQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["RemQty"] = txtRecQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["date"] = date1;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
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


        protected void txtsendfqtychnaged_text(object sender, EventArgs e)
        {
            double sndqty = 0;
            int iq = 1;
            int iii = 1;
            int iq1 = 1;
            int iii1 = 1;
            string itemc = string.Empty;

            string iteme = string.Empty;
            string itemcd = string.Empty;

            if (btnadd.Text == "Save")
            {
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drplotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");

                    if (drpprocess.SelectedValue != "Select Process Type")
                    {

                        TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendQuantity");

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

                                    TextBox datee = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("date");


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
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendQuantity");
                        // TextBox txtbundle = (TextBox)gvcustomerorder.Rows[i].FindControl("txtBundle");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                        txtrecFQty.Text = "0";
                        txtrecFQty.Enabled = false;

                        sndqty = sndqty + Convert.ToDouble(txtsendFQty.Text);

                        string temp = drpprocess.SelectedValue;
                        // string tempbun = txtbundle.Text;
                        double qty = 0;
                        for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                        {
                            DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                            DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                            // TextBox bundle = (TextBox)gvcustomerorder.Rows[j].FindControl("txtBundle");
                            curent = process.SelectedValue;
                            namee = process.SelectedItem.Text;
                            //  bun = bundle.Text;
                            TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendQuantity");

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
                            if (objbs.CheckIfrecqtyinstichingN(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + drpprocess.SelectedItem.Text + " has Exists received Quantity.')", true);
                                // txtRecQuantity.Focus();
                                txtsendFQty.Focus();
                                return;


                            }

                        }


                    }
                }
            }
            else
            {
                if (btnadd.Text == "Update")
                {
                    //string curent = string.Empty;
                    //string namee = string.Empty;
                    //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    //{
                    //    DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
                    //    if (drpLotprocess.SelectedValue == "Select Lot No")
                    //    {
                    //    }
                    //    else
                    //    {
                    //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    //        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                    //        TextBox txtbundle = (TextBox)gvcustomerorder.Rows[i].FindControl("txtBundle");
                    //        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                    //        txtrecFQty.Text = "0";
                    //        txtrecFQty.Enabled = false;

                    //        sndqty = sndqty + Convert.ToDouble(txtsendFQty.Text);
                    //    }

                    //    txttotalqty.Text = sndqty.ToString();
                    //}
                }
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
                        DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drplotno");

                        DropDownList drpProcess =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txtRate");

                        TextBox txtavaQuantity = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtavaQuantity");
                        TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRecQuantity");

                        TextBox date = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("date");
                        TextBox txtsendQuantity = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendQuantity");


                        drpLotno.SelectedValue = dt.Rows[i]["LotNo"].ToString();
                        drpProcess.SelectedValue = dt.Rows[i]["ProcessType"].ToString();
                        txtavaQuantity.Text = dt.Rows[i]["AvaQty"].ToString();
                        txtsendQuantity.Text = dt.Rows[i]["SendQty"].ToString();
                        txtRecQuantity.Text = dt.Rows[i]["RemQty"].ToString();
                        date.Text = dt.Rows[i]["date"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();

                        rowIndex++;
                        drpProcess.Focus();
                    }
                }
            }
        }

        //protected void txtsendqty(object sender, EventArgs e)
        //{
        //}

        protected void Add_LotProcessDetails(object sender, EventArgs e)
        {



            if (btnadd.Text == "Save")
            {

                if (drpemployee.SelectedValue == "Select Employee Name")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Something Went Wrong.Please Check Employee Name.Or Contact Administrator.')", true);
                    return;
                }

                double sndqty = 0;
                int iq = 1;
                int iii = 1;
                int iq1 = 1;
                int iii1 = 1;
                string itemc = string.Empty;

                string iteme = string.Empty;
                string itemcd = string.Empty;

                if (btnadd.Text == "Save")
                {
                    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                    {
                        DropDownList drplotno = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpLotno");
                        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");

                        if (drpprocess.SelectedValue != "Select Process Type")
                        {

                            TextBox txtsendQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendQuantity");

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

                                        TextBox datee = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("date");


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
                            TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendQuantity");
                            // TextBox txtbundle = (TextBox)gvcustomerorder.Rows[i].FindControl("txtBundle");
                            TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                            txtrecFQty.Text = "0";
                            txtrecFQty.Enabled = false;

                            sndqty = sndqty + Convert.ToDouble(txtsendFQty.Text);

                            string temp = drpprocess.SelectedValue;
                            // string tempbun = txtbundle.Text;
                            double qty = 0;
                            for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                            {
                                DropDownList drpLotprocess1 = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpLotno");
                                DropDownList process = (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                                // TextBox bundle = (TextBox)gvcustomerorder.Rows[j].FindControl("txtBundle");
                                curent = process.SelectedValue;
                                namee = process.SelectedItem.Text;
                                //  bun = bundle.Text;
                                TextBox txtsendFQty1 = (TextBox)gvcustomerorder.Rows[j].FindControl("txtsendQuantity");

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
                                if (objbs.CheckIfrecqtyinstichingN(drpLotprocess.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + drpprocess.SelectedItem.Text + " has Exists received Quantity.')", true);
                                    // txtRecQuantity.Focus();
                                    txtsendFQty.Focus();
                                    return;


                                }

                            }


                        }
                    }
                }

            }

            if (btnadd.Text == "Save")
            {
                DataSet ds = new DataSet();
                // ds = objbs.checkalreadyexistornot(ddlLotNo.SelectedValue);
                // if (ds.Tables[0].Rows.Count == 0)
                {
                    DateTime recdate1 = DateTime.ParseExact(txtstichdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    int iStatus23 = objbs.insertStichingprocessN(drpemployee.SelectedValue, recdate1);

                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {
                        DropDownList drplotno = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drplotno");
                        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");

                        TextBox txtsendQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendQuantity");

                        TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                        //   date.Text = DateTime.Now.ToString("dd/MM/yyyy");


                        if (drpprocess.SelectedValue == "Select Process Type")
                        {
                        }
                        else
                        {
                            DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //ds = objbs.CheckQuantityOverLoadLotProcess(Convert.ToInt32(drplotno.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
                            //string ProcessType = "";
                            //int total = 0;
                            //if (ds.Tables[0].Rows.Count > 0)
                            //{
                            //    int test = Convert.ToInt32(ds.Tables[0].Rows[0]["RecQty"]);
                            //    ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
                            //    total = test + Convert.ToInt32(txtsendQuantity.Text);

                            //}
                            //if (Convert.ToInt32(txtTotalQantity.Text) >= total)
                            {
                                int istasinsert = objbs.insertTransStichingprocessN(drplotno.SelectedValue, drpprocess.SelectedValue, txtsendQuantity.Text, recdate, txtrate.Text);

                                // int istasInsertHistory = objbs.inserttransLotProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                //  recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text));

                                //   int istas = objbs.inserttransLotProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                //   recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text));
                            }
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type has enetered Over Quantity!!!.')", true);
                            //    return;
                            //}
                        }
                    }
                }
            }
            else if (btnadd.Text == "Received")
            {
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    Label lbltrans = (Label)gvcustomerorder.Rows[i].FindControl("lbltransid");

                    DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[i].FindControl("drplotno");
                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtRate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                    TextBox txtavaQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtavaQuantity");
                    TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");

                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    TextBox txtsendQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendQuantity");

                    DateTime recdatee = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (Convert.ToInt32(txtsendQuantity.Text) >= Convert.ToInt32(txtRecQuantity.Text))
                    {


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given '"+drpLotno.SelectedItem.Text+"','" + drpProcess.SelectedItem.Text + "' type has enetered Over Quantity!!!.')", true);
                        return;
                    }
                }


                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    Label lbltrans = (Label)gvcustomerorder.Rows[i].FindControl("lbltransid");

                    DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[i].FindControl("drplotno");
                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    TextBox txtRate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                    TextBox txtavaQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtavaQuantity");
                    TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");

                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    TextBox txtsendQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendQuantity");

                    DateTime recdatee = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    // if (Convert.ToInt32(txtsendQuantity.Text) >= Convert.ToInt32(txtRecQuantity.Text))
                    {

                        int istasinsert = objbs.updateTransStichingprocessN(drpLotno.SelectedValue, drpProcess.SelectedValue, txtsendQuantity.Text, recdatee, txtRate.Text, lbltrans.Text, txtnewlotprocess.Text, txtRecQuantity.Text);
                    }


                }
            }
            //    else
            //    {

            //        for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //        {

            //            DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //            DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
            //            TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
            //            TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
            //            TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

            //            // date.Text = DateTime.Now.ToString("dd/MM/yyyy");


            //            if (drpprocess.SelectedValue != "Select Process Type")
            //            {
            //                DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //                ds = objbs.CheckQuantityOverLoadLotProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
            //                string ProcessType = "";
            //                int total = 0;
            //                if (ds.Tables[0].Rows.Count > 0)
            //                {
            //                    int test = Convert.ToInt32(ds.Tables[0].Rows[0]["RecQty"]);
            //                    ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
            //                    total = test + Convert.ToInt32(txtrecQty.Text);

            //                }
            //                if (Convert.ToInt32(txtTotalQantity.Text) >= total)
            //                {
            //                    //int istasInsertHistory = objbs.inserttransLotProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
            //                    //recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text));

            //                    int istas = objbs.UpdatetransLotProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
            //                recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text));
            //                }
            //                else
            //                {
            //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + ProcessType + " has enetered Over Quantity!!!.')", true);
            //                    return;
            //                }
            //            }

            //        }
            //    }
            //}
            //else if (btnadd.Text == "Update")
            //{
            //    int iscc = objbs.deletestochingprocessneww(ddlLotNo.SelectedValue);
            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {

            //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //        DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
            //        TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
            //        TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
            //        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

            //        //   date.Text = DateTime.Now.ToString("dd/MM/yyyy");


            //        if (drpprocess.SelectedValue == "Select Process Type")
            //        {
            //        }
            //        else
            //        {
            //            DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //            //ds = objbs.CheckQuantityOverLoadLotProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
            //            //string ProcessType = "";
            //            //int total = 0;
            //            //if (ds.Tables[0].Rows.Count > 0)
            //            //{
            //            //    int test = Convert.ToInt32(ds.Tables[0].Rows[0]["RecQty"]);
            //            //    ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
            //            //    total = test + Convert.ToInt32(txtrecQty.Text);

            //            //}
            //            //   if (Convert.ToInt32(txtTotalQantity.Text) >= total)



            //            int istasInsertHistory = objbs.updatestochingprocessneww(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
            //            recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text));

            //        }
            //    }
            //    DataSet ds = objbs.gettranshistoryforupdatee(ddlLotNo.SelectedValue);
            //    {

            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            for (int ii = 0; ii < ds.Tables[0].Rows.Count; ii++)
            //            {

            //                string lotprocessid = ds.Tables[0].Rows[ii]["LotProcessID"].ToString();
            //                string processid = ds.Tables[0].Rows[ii]["ProcessTypeID"].ToString();
            //                string empid = ds.Tables[0].Rows[ii]["EmpID"].ToString();
            //                string RecQty = ds.Tables[0].Rows[ii]["RecQty"].ToString();
            //                string ndate = Convert.ToDateTime(ds.Tables[0].Rows[ii]["Date"]).ToString("dd/MM/yyyy");
            //                DateTime newdate = DateTime.ParseExact(ndate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //                string rate = ds.Tables[0].Rows[ii]["Rate"].ToString();
            //                string totalqty = ds.Tables[0].Rows[ii]["TotalQty"].ToString();


            //                int istas = objbs.UpdatetransLotProcessnewprocess(Convert.ToInt32(processid), Convert.ToInt32(empid), Convert.ToInt32(RecQty),
            //                newdate, Convert.ToDecimal(rate), ddlLotNo.SelectedValue, Convert.ToInt32(totalqty));
            //            }

            //            DataSet dn = objbs.gettransforupdatee(ddlLotNo.SelectedValue);
            //            if (dn.Tables[0].Rows.Count > 0)
            //            {
            //                for (int k = 0; k < dn.Tables[0].Rows.Count; k++)
            //                {
            //                    string TOtProcid = dn.Tables[0].Rows[k]["Processtypeid"].ToString();

            //                    int iss = objbs.updatereceiveqty(ddlLotNo.SelectedValue, TOtProcid);
            //                }



            //                for (int j = 0; j < dn.Tables[0].Rows.Count; j++)
            //                {
            //                    string Procid = dn.Tables[0].Rows[j]["Processtypeid"].ToString();
            //                    string recqty = dn.Tables[0].Rows[j]["RecQty"].ToString();

            //                    int ii = objbs.updatetranslordetails(ddlLotNo.SelectedValue, Procid, recqty);


            //                }
            //            }

            //        }

            //    }


            //}



            System.Threading.Thread.Sleep(3000);

            Response.Redirect("NDilloStiching_Grid.aspx");


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



    }
}