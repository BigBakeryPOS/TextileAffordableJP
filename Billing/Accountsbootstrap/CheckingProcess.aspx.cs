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
    public partial class CheckingProcess : System.Web.UI.Page
    {

        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();

        int iFinishedQty = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string Checkingid = Request.QueryString.Get("checkingid");
            if (!IsPostBack)
            {
                DataSet dsLotNo = objbs.Select_LotNo_ForProcess(7);    // 7-Checking 
                if (dsLotNo.Tables[0].Rows.Count > 0)
                {
                    ddlLotNo.DataSource = dsLotNo.Tables[0];
                    ddlLotNo.DataTextField = "LotNo";
                    ddlLotNo.DataValueField = "cutid";
                    ddlLotNo.DataBind();
                    ddlLotNo.Items.Insert(0, "Select Lot No");
                }
                divWork.Visible = false;
                if (Checkingid != null)
                {
                    DataSet dgetcheck = objbs.Get_CheckingProcesss(Checkingid);
                    if (dgetcheck.Tables[0].Rows.Count > 0)
                    {
                        btnadd.Visible = true;
                        btnadd.Text = "Update";
                        ddlLotNo.SelectedValue = dgetcheck.Tables[0].Rows[0]["cutid"].ToString();
                        CkeckingInfo_Loadforupdate(sender, e);
                        DataSet ds = objbs.Select_CheckingProcessDetails(Checkingid);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataSet temp = new DataSet();
                            DataTable dtt = new DataTable();

                            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Process", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("EmpName", typeof(string)));
                            dtt.Columns.Add(new DataColumn("RecQuantity", typeof(string)));
                            dtt.Columns.Add(new DataColumn("date", typeof(string)));

                            dtt.Columns.Add(new DataColumn("36FS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("36HS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("38FS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("38HS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("39FS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("39HS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("40FS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("40HS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("42FS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("42HS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("44FS", typeof(string)));
                            dtt.Columns.Add(new DataColumn("44HS", typeof(string)));
                            temp.Tables.Add(dtt);



                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {

                                DataRow dr = dtt.NewRow();

                                dr["OrderNo"] = ds.Tables[0].Rows[i]["Transid"].ToString();
                                dr["Process"] = ds.Tables[0].Rows[i]["processtypeid"].ToString();
                                dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                                dr["EmpName"] = ds.Tables[0].Rows[i]["empid"].ToString();
                                dr["RecQuantity"] = ds.Tables[0].Rows[i]["recqty"].ToString();
                                dr["date"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["date"]).ToString("dd/MM/yyyy");

                                dr["36FS"] = ds.Tables[0].Rows[i]["36FS"].ToString();
                                dr["36HS"] = ds.Tables[0].Rows[i]["36HS"].ToString();
                                dr["38FS"] = ds.Tables[0].Rows[i]["38FS"].ToString();
                                dr["38HS"] = ds.Tables[0].Rows[i]["38HS"].ToString();
                                dr["39FS"] = ds.Tables[0].Rows[i]["39FS"].ToString();
                                dr["39HS"] = ds.Tables[0].Rows[i]["39HS"].ToString();
                                dr["40FS"] = ds.Tables[0].Rows[i]["40FS"].ToString();
                                dr["40HS"] = ds.Tables[0].Rows[i]["40HS"].ToString();
                                dr["42FS"] = ds.Tables[0].Rows[i]["42FS"].ToString();
                                dr["42HS"] = ds.Tables[0].Rows[i]["42HS"].ToString();
                                dr["44FS"] = ds.Tables[0].Rows[i]["44FS"].ToString();
                                dr["44HS"] = ds.Tables[0].Rows[i]["44HS"].ToString();

                                temp.Tables[0].Rows.Add(dr);
                            }

                            ViewState["CurrentTable1"] = dtt;

                            gvcustomerorder.DataSource = temp;
                            gvcustomerorder.DataBind();

                            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                            {
       
                                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");

                                TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                                TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");

                                DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpEmp");

                                TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");


                                TextBox txt_36FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt36FS");
                                TextBox txt_38FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt38FS");
                                TextBox txt_39FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt39FS");
                                TextBox txt_40FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt40FS");
                                TextBox txt_42FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt42FS");
                                TextBox txt_44FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt44FS");

                                TextBox txt_36HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt36HS");
                                TextBox txt_38HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt38HS");
                                TextBox txt_39HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt39HS");
                                TextBox txt_40HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt40HS");
                                TextBox txt_42HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt42HS");
                                TextBox txt_44HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt44HS");


                                date.Enabled = false;
                                txtrate.Enabled = false;

                                drpprocess.SelectedValue = temp.Tables[0].Rows[i]["Process"].ToString();
                                txtrate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();
                                txtRecQuantity.Text = temp.Tables[0].Rows[i]["RecQuantity"].ToString();
                                drpEmp.SelectedValue = temp.Tables[0].Rows[i]["EmpName"].ToString();
                                date.Text = temp.Tables[0].Rows[i]["date"].ToString();

                                txt_36FS.Text = temp.Tables[0].Rows[i]["36FS"].ToString();
                                txt_36HS.Text = temp.Tables[0].Rows[i]["36HS"].ToString();
                                txt_38FS.Text = temp.Tables[0].Rows[i]["38FS"].ToString();
                                txt_38HS.Text = temp.Tables[0].Rows[i]["38HS"].ToString();
                                txt_39FS.Text = temp.Tables[0].Rows[i]["39FS"].ToString();
                                txt_39HS.Text = temp.Tables[0].Rows[i]["39HS"].ToString();
                                txt_40FS.Text = temp.Tables[0].Rows[i]["40FS"].ToString();
                                txt_40HS.Text = temp.Tables[0].Rows[i]["40HS"].ToString();
                                txt_42FS.Text = temp.Tables[0].Rows[i]["42FS"].ToString();
                                txt_42HS.Text = temp.Tables[0].Rows[i]["42HS"].ToString();
                                txt_44FS.Text = temp.Tables[0].Rows[i]["44FS"].ToString();
                                txt_44HS.Text = temp.Tables[0].Rows[i]["44HS"].ToString();
                            }
                        }
                        else
                        {
                            FirstGridViewRow();
                            // mySidenav.Visible = false;
                        }

                        //  gvcustomerorder.Enabled = false;
                        ddlLotNo.Enabled = false;

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

            //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
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
                    if (drpprocess.SelectedValue != "Select Process Type")
                    {
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
                               // if (qty > Convert.ToInt32(txtnewfinishedqty.Text))

                                if (qty > Convert.ToInt32(txtfinishedQty.Text))
                                {
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Received Qty should not greater than Finished Qty')", true);
                                    txtRecQuantity.Focus();
                                    RecQuantity.Text = "0";
                                    return;
                                }
                            }
                        }

                        if (objbs.CheckIfrecqtyinChecking(ddlLotNo.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee + " has Exists received Quantity.')", true);
                            txtRecQuantity.Focus();
                            return;
                        }

             

                        // date.Focus();
                    }


                }
            }
            else
            {

                string curent = string.Empty;
                string namee = string.Empty;
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    if (drpprocess.SelectedValue != "Select Process Type")
                    {
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
           
                        if (objbs.CheckIfrecqtyinstichingupdate(ddlLotNo.SelectedValue, drpprocess.SelectedValue, qty.ToString()))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These " + namee.Trim() + " has Exists received Quantity.')", true);
                            txtRecQuantity.Focus();
                            return;
                       }
                        date.Focus();
                    }
                }
            }

            int No = 0;
            for (int loop = 0; loop < gvcustomerorder.Rows.Count; loop++)
            {

                DropDownList drp_Process = (DropDownList)gvcustomerorder.Rows[loop].FindControl("drpProcess");

                if (drp_Process.SelectedValue == "Select Process Type")
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

            //  AddNewRow();
        }

        protected void drpprocess_selected(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList ddlprocess = (DropDownList)row.FindControl("drpProcess");
            TextBox txtrate = (TextBox)row.FindControl("txtRate");

            TextBox txt_36FS = (TextBox)row.FindControl("txt36FS");
            TextBox txt_38FS = (TextBox)row.FindControl("txt38FS");
            TextBox txt_39FS = (TextBox)row.FindControl("txt39FS");
            TextBox txt_40FS = (TextBox)row.FindControl("txt40FS");
            TextBox txt_42FS = (TextBox)row.FindControl("txt42FS");
            TextBox txt_44FS = (TextBox)row.FindControl("txt44FS");

            TextBox txt_36HS = (TextBox)row.FindControl("txt36HS");
            TextBox txt_38HS = (TextBox)row.FindControl("txt38HS");
            TextBox txt_39HS = (TextBox)row.FindControl("txt39HS");
            TextBox txt_40HS = (TextBox)row.FindControl("txt40HS");
            TextBox txt_42HS = (TextBox)row.FindControl("txt42HS");
            TextBox txt_44HS = (TextBox)row.FindControl("txt44HS");



            DataSet ds = new DataSet();
            if (ddlprocess.SelectedValue != "Select Process Type")
            {
                ds = objbs.Get_Rate(ddlprocess.SelectedValue, ddlLotNo.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtrate.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Rate"]).ToString();
                    if (txt_36FS.Text == "")
                    {
                        txt_36FS.Text = "0";
                    }
                    if (txt_38FS.Text == "")
                    {
                        txt_38FS.Text = "0";
                    }
                    if (txt_39FS.Text == "")
                    {
                        txt_39FS.Text = "0";
                    }
                    if (txt_40FS.Text == "")
                    {
                        txt_40FS.Text = "0";
                    }
                    if (txt_42FS.Text == "")
                    {
                        txt_42FS.Text = "0";
                    }
                    if (txt_44FS.Text == "")
                    {
                        txt_44FS.Text = "0";
                    }

                    if (txt_36HS.Text == "")
                    {
                        txt_36HS.Text = "0";
                    }
                    if (txt_38HS.Text == "")
                    {
                        txt_38HS.Text = "0";
                    }
                    if (txt_39HS.Text == "")
                    {
                        txt_39HS.Text = "0";
                    }
                    if (txt_40HS.Text == "")
                    {
                        txt_40HS.Text = "0";
                    }
                    if (txt_42HS.Text == "")
                    {
                        txt_42HS.Text = "0";
                    }
                    if (txt_44HS.Text == "")
                    {
                        txt_44HS.Text = "0";
                    }
                }
            }

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {

                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                drpprocess.Focus();

            }
            ScriptManager.RegisterStartupScript(this, GetType(), "Myscript", "openNav();", true);
            // ButtonAdd1_Click(sender, e);
        }

        protected void Detail_checked(object sender, EventArgs e)
        {
            if (ddlLotNo.SelectedValue != "Select Lot No")
            {
                DataSet dlotprocess = new DataSet();
                DataSet dssendqty = new DataSet();
                dlotprocess = objbs.Get_Processdetails(ddlLotNo.SelectedValue, 7);   //7-Checking


                if (dlotprocess.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = dlotprocess;
                    GridView1.DataBind();

                    txtfinishedQty.Text=dlotprocess.Tables[0].Rows[0]["FinishedQty"].ToString();

                }


                dssendqty = objbs.Select_CheckingSendQty_ByLotID(Convert.ToInt32(ddlLotNo.SelectedValue));

                if (dssendqty.Tables[0].Rows.Count > 0)
                {
                    txtOldrecQty.Text = dssendqty.Tables[0].Rows[0]["SendQty"].ToString();
                    txtcheckingbalQty.Text = dssendqty.Tables[0].Rows[0]["CheckingBalQty"].ToString();
                }
                else
                {
                    txtOldrecQty.Text = "0";
                    txtcheckingbalQty.Text = "0";
                }



                txtnewfinishedqty.Text = Convert.ToString(Convert.ToInt32(txtfinishedQty.Text)-Convert.ToInt32(txtOldrecQty.Text) + Convert.ToInt32(txtcheckingbalQty.Text));
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
                DataSet drpProcess = objbs.SelectProcessType_LotProcess(Convert.ToInt32(ddlLotNo.SelectedValue),7);    // 7- Checking(ProcessHeadingID)
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


        protected void CkeckingInfo_Loadforupdate(object sender, EventArgs e)
        {
            DataSet dataSet = objbs.getLotNoTransDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
                txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();

                txtUnitName.Text = dataSet.Tables[0].Rows[0]["UnitName"].ToString();
                txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString();

                txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
                txtbrandid.Text = dataSet.Tables[0].Rows[0]["BrandID"].ToString();
                txtUnitID.Text = dataSet.Tables[0].Rows[0]["UnitID"].ToString();
                txtHalf.Text = dataSet.Tables[0].Rows[0]["HalfQty"].ToString();
                txtfull.Text = dataSet.Tables[0].Rows[0]["FullQty"].ToString();
                txtdesignno.Text = dataSet.Tables[0].Rows[0]["Designno"].ToString();
                string processDate = dataSet.Tables[0].Rows[0]["ProcessDate"].ToString();
                if (processDate == "")
                {
                    DateTime date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    txtProcessDate.Text = date.ToString("dd/MM/yyyy");
                }
                else
                {
                    DateTime date = DateTime.ParseExact(Convert.ToDateTime(processDate).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    txtProcessDate.Text = date.ToString("dd/MM/yyyy");
                }

                string lotno = "0";
                if (ddlLotNo.SelectedValue == "Select Lot No")
                {
                    lotno = "0";
                }
                else
                {
                    lotno = ddlLotNo.SelectedValue;
                }

                DataSet drpProcess = objbs.SelectProcessType_LotProcess(Convert.ToInt32(lotno),7);   // 7- CHecking ProcessHeadingID
                DataSet drpEmp = objbs.Select_EmpName(Convert.ToInt32(txtUnitID.Text), 10);   //10- Checking Designation

                DataSet dlotprocess = new DataSet();
                dlotprocess = objbs.Get_Processdetails(lotno,7);    // 7 - Checking
                if (dlotprocess.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = dlotprocess;
                    GridView1.DataBind();
                    txtfinishedQty.Text = dlotprocess.Tables[0].Rows[0]["FinishedQty"].ToString();

                }

                DataSet dssendqty = new DataSet();
                dssendqty = objbs.Select_CheckingSendQty_ByLotID(Convert.ToInt32(ddlLotNo.SelectedValue));

                if (dssendqty.Tables[0].Rows.Count > 0)
                {
                    txtOldrecQty.Text = dssendqty.Tables[0].Rows[0]["SendQty"].ToString();
                    txtcheckingbalQty.Text = dssendqty.Tables[0].Rows[0]["CheckingBalQty"].ToString();
                }
                else
                {
                    txtOldrecQty.Text = "0";
                    txtcheckingbalQty.Text = "0";
                }



                txtnewfinishedqty.Text = Convert.ToString(Convert.ToInt32(txtfinishedQty.Text) - Convert.ToInt32(txtOldrecQty.Text) + Convert.ToInt32(txtcheckingbalQty.Text));

                DataSet workProcessManual = objbs.SelectWorkProcessTypeManual(Convert.ToInt32(ddlLotNo.SelectedValue));
                if (workProcessManual.Tables[0].Rows[0]["IsManual"].ToString() == "True")
                {
                    divWork.Visible = true;
                    GridView3.DataSource = workProcessManual;
                    GridView3.DataBind();
                }
                else
                {
                    divWork.Visible = false;
                }

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {

                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                    drpprocess.Focus();

                }
            }
            else
            {
                gvcustomerorder.DataSource = null;
                gvcustomerorder.DataBind();
            }

            GridView1.Visible = true;
            GridView2.Visible = true;
            processs.Visible = true;
            ratee.Visible = true;
        }

        protected void CheckingInfo_Load(object sender, EventArgs e)
        {
            DataSet dataSet = objbs.getLotNoTransDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
                txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();

                txtUnitName.Text = dataSet.Tables[0].Rows[0]["UnitName"].ToString();
                txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString();

                txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
                txtbrandid.Text = dataSet.Tables[0].Rows[0]["BrandID"].ToString();
                txtUnitID.Text = dataSet.Tables[0].Rows[0]["UnitID"].ToString();
                txtHalf.Text = dataSet.Tables[0].Rows[0]["HalfQty"].ToString();
                txtfull.Text = dataSet.Tables[0].Rows[0]["FullQty"].ToString();
                txtdesignno.Text = dataSet.Tables[0].Rows[0]["DesignNo"].ToString();
                string processDate = dataSet.Tables[0].Rows[0]["ProcessDate"].ToString();
                if (processDate == "")
                {
                    DateTime date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    txtProcessDate.Text = date.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtProcessDate.Text = Convert.ToDateTime(dataSet.Tables[0].Rows[0]["ProcessDate"]).ToString("dd/MM/yyyy");
                }

                string lotno = "0";
                if (ddlLotNo.SelectedValue == "Select Lot No")
                {
                    lotno = "0";
                }
                else
                {
                    lotno = ddlLotNo.SelectedValue;
                }

                DataSet drpProcess = objbs.SelectProcessType_LotProcess(Convert.ToInt32(lotno),7);     //7-Checking
                DropDownList dbrand = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpProcess");

                if (drpProcess.Tables[0].Rows.Count > 0)
                {
                    dbrand.Items.Clear();
                    dbrand.ClearSelection();
                    dbrand.DataSource = drpProcess.Tables[0];
                    dbrand.DataTextField = "ProcessType";
                    dbrand.DataValueField = "ProcessMasterID";
                    dbrand.DataBind();
                    dbrand.Items.Insert(0, "Select Process Type");
                }

                DataSet drpEmpName = new DataSet();
                if (txtUnitID.Text == "")
                {
                    drpEmpName = objbs.SelectEmpName();
                }
                else
                {
                    drpEmpName = objbs.Select_EmpName(Convert.ToInt32(txtUnitID.Text),10);  //10- Checking -designation
                }
                DropDownList dEmp = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpEmp");

                if (drpEmpName.Tables[0].Rows.Count > 0)
                {
                    dEmp.Items.Clear();
                    dEmp.DataSource = drpEmpName.Tables[0];
                    dEmp.DataTextField = "Name";
                    dEmp.DataValueField = "Employee_Id";
                    dEmp.DataBind();
                    dEmp.Items.Insert(0, "Select Employee Name");
                }

                GridView2.DataSource = drpProcess;
                GridView2.DataBind();

                DataSet dlotprocess = new DataSet();
                dlotprocess = objbs.Get_Processdetails(lotno,7);    //7- ProcessHeadingID
                if (dlotprocess.Tables[0].Rows.Count > 0)
                {
                    GridView1.DataSource = dlotprocess;
                    GridView1.DataBind();
                    txtfinishedQty.Text = dlotprocess.Tables[0].Rows[0]["FinishedQty"].ToString();
                }
                DataSet dssendqty = new DataSet();

                dssendqty = objbs.Select_CheckingSendQty_ByLotID(Convert.ToInt32(ddlLotNo.SelectedValue));
                if (dssendqty.Tables[0].Rows.Count > 0)
                {
                    txtOldrecQty.Text = dssendqty.Tables[0].Rows[0]["SendQty"].ToString();
                    txtcheckingbalQty.Text = dssendqty.Tables[0].Rows[0]["CheckingBalQty"].ToString();
                }
                else
                {
                    txtOldrecQty.Text = "0";
                    txtcheckingbalQty.Text = "0";

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not Allow To Processed This Lot No. Thank You.')", true);
                    ddlLotNo.ClearSelection();
                    
                    return;

                }


                if (txtfinishedQty.Text != "")
                {
                    txtnewfinishedqty.Text = Convert.ToString(Convert.ToInt32(txtfinishedQty.Text) - Convert.ToInt32(txtOldrecQty.Text) + Convert.ToInt32(txtcheckingbalQty.Text));
                }



                DataSet workProcessManual = objbs.SelectWorkProcessTypeManual(Convert.ToInt32(ddlLotNo.SelectedValue));
                if (workProcessManual.Tables[0].Rows[0]["IsManual"].ToString() == "True")
                {
                    divWork.Visible = true;
                    GridView3.DataSource = workProcessManual;
                    GridView3.DataBind();
                }
                else
                {
                    divWork.Visible = false;
                }

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {

                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                    drpprocess.Focus();

                }
            }
            else
            {
                gvcustomerorder.DataSource = null;
                gvcustomerorder.DataBind();
            }
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
            DataSet drpProcess = new DataSet();
            string lotno = "0";
            if (ddlLotNo.SelectedValue == "Select Lot No" || ddlLotNo.SelectedValue == "")
            {
                lotno = "0";
            }
            else
            {
                lotno = ddlLotNo.SelectedValue;
            }
            if (btnadd.Text == "Save")
            {

                drpProcess = objbs.SelectProcessType_LotProcess(Convert.ToInt32(lotno),7);    // 7- Checking ProcessHeadingID
            }
            else
            {
                drpProcess = objbs.SelectProcessType_LotProcessupdate(Convert.ToInt32(lotno),7);   // 7- Checking ProcessHeadingID
            }
            DataSet drpEmp = new DataSet();

            if (txtUnitID.Text == "")
            {
                drpEmp = objbs.SelectEmpName();
            }
            else
            {
                drpEmp = objbs.Select_EmpName(Convert.ToInt32(txtUnitID.Text),10);   // 10-Checking Designation
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList drpProcess1 = (DropDownList)e.Row.FindControl("drpProcess");
                DropDownList drpEmp1 = (DropDownList)e.Row.FindControl("drpEmp");

                var ddProcess = (DropDownList)e.Row.FindControl("drpProcess");
                ddProcess.DataSource = drpProcess;
                ddProcess.DataTextField = "ProcessType";
                ddProcess.DataValueField = "ProcessMasterID";
                ddProcess.DataBind();
                ddProcess.Items.Insert(0, "Select Process Type");


                var ddEmp = (DropDownList)e.Row.FindControl("drpEmp");
                ddEmp.DataSource = drpEmp;
                ddEmp.DataTextField = "Name";
                ddEmp.DataValueField = "Employee_Id";
                ddEmp.DataBind();
                ddEmp.Items.Insert(0, "Select Employee Name");
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

                        DropDownList drpProcess =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");

                        TextBox txtRecQuantity =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRecQuantity");

                        DropDownList drpEmp =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpEmp");

                        TextBox date =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("date");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Process"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["RecQuantity"] = txtRecQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["EmpName"] = drpEmp.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["date"] = date.Text;

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
            dtt.Columns.Add(new DataColumn("Process", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtt.Columns.Add(new DataColumn("EmpName", typeof(string)));
            dtt.Columns.Add(new DataColumn("RecQuantity", typeof(string)));
            dtt.Columns.Add(new DataColumn("date", typeof(string)));

            dtt.Columns.Add(new DataColumn("36FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("36HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("38FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("38HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("39FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("39HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("40FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("40HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("42FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("42HS", typeof(string)));
            dtt.Columns.Add(new DataColumn("44FS", typeof(string)));
            dtt.Columns.Add(new DataColumn("44HS", typeof(string)));


            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["Process"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["EmpName"] = string.Empty;
            dr["RecQuantity"] = string.Empty;
            dr["date"] = string.Empty;
            dr["36FS"] = string.Empty;
            dr["36HS"] = string.Empty;
            dr["38FS"] = string.Empty;
            dr["38HS"] = string.Empty;
            dr["39FS"] = string.Empty;
            dr["39HS"] = string.Empty;
            dr["40FS"] = string.Empty;
            dr["40HS"] = string.Empty;
            dr["42FS"] = string.Empty;
            dr["42HS"] = string.Empty;
            dr["44FS"] = string.Empty;
            dr["44HS"] = string.Empty;

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

            dct = new DataColumn("Process");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("EmpName");
            dttt.Columns.Add(dct);

            dct = new DataColumn("RecQuantity");
            dttt.Columns.Add(dct);

            dct = new DataColumn("date");
            dttt.Columns.Add(dct);

            dct = new DataColumn("36FS");
            dttt.Columns.Add(dct);

            dct = new DataColumn("36HS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("38FS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("38HS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("39FS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("39HS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("40FS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("40HS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("42FS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("42HS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("44FS");
            dttt.Columns.Add(dct);


            dct = new DataColumn("44HS");
            dttt.Columns.Add(dct);
            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["OrderNo"] = "";
            drNew["Process"] = "";
            drNew["Rate"] = "";
            drNew["EmpName"] = "";
            drNew["RecQuantity"] = "";
            drNew["date"] = DateTime.Now.ToString("dd/MM/yyyy");
            drNew["36FS"] = 0;
            drNew["36HS"] = 0;
            drNew["38FS"] = 0;
            drNew["38HS"] = 0;
            drNew["39FS"] = 0;
            drNew["39HS"] = 0;
            drNew["40FS"] = 0;
            drNew["40HS"] = 0;
            drNew["42FS"] = 0;
            drNew["42HS"] = 0;
            drNew["44FS"] = 0;
            drNew["44HS"] = 0;
            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();

        }


        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "Myscript", "openNav();", true);
            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                drpprocess.Focus();
            }
        }

        private void AddNewRow()
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRecQuantity");
                DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpEmp");
                TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");

                DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

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

                        DropDownList drpProcess =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txtRate");

                        TextBox txtRecQuantity =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txtRecQuantity");

                        DropDownList drpEmp =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpEmp");

                        TextBox date =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("date");

                        TextBox txt_36FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt36FS");


                        TextBox txt_36HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt36HS");


                        TextBox txt_38FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt38FS");


                        TextBox txt_38HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt38HS");

                        TextBox txt_39FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt39FS");

                        TextBox txt_39HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt39HS");

                        TextBox txt_40FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt40FS");

                        TextBox txt_40HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt40HS");

                        TextBox txt_42FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt42FS");

                        TextBox txt_42HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt42HS");

                        TextBox txt_44FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt44FS");


                        TextBox txt_44HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt44HS");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Process"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["RecQuantity"] = txtRecQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["EmpName"] = drpEmp.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["date"] = date.Text;

                        dtCurrentTable.Rows[i - 1]["36FS"] = txt_36FS.Text;
                        dtCurrentTable.Rows[i - 1]["36HS"] = txt_36HS.Text;
                        dtCurrentTable.Rows[i - 1]["38FS"] = txt_38FS.Text;
                        dtCurrentTable.Rows[i - 1]["38HS"] = txt_38HS.Text;
                        dtCurrentTable.Rows[i - 1]["39FS"] = txt_39FS.Text;
                        dtCurrentTable.Rows[i - 1]["39HS"] = txt_39HS.Text;
                        dtCurrentTable.Rows[i - 1]["40FS"] = txt_40FS.Text;
                        dtCurrentTable.Rows[i - 1]["40HS"] = txt_40HS.Text;
                        dtCurrentTable.Rows[i - 1]["42FS"] = txt_42FS.Text;
                        dtCurrentTable.Rows[i - 1]["42HS"] = txt_42HS.Text;
                        dtCurrentTable.Rows[i - 1]["44FS"] = txt_44FS.Text;
                        dtCurrentTable.Rows[i - 1]["44HS"] = txt_44HS.Text;
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

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("date");
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
                        DropDownList drpProcess =
                     (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtRate");

                        TextBox txtRecQuantity =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtRecQuantity");

                        DropDownList drpEmp =
                     (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpEmp");

                        TextBox date =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("date");


                        TextBox txt_36FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt36FS");


                        TextBox txt_36HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt36HS");


                        TextBox txt_38FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt38FS");


                        TextBox txt_38HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt38HS");

                        TextBox txt_39FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt39FS");

                        TextBox txt_39HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt39HS");

                        TextBox txt_40FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt40FS");

                        TextBox txt_40HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt40HS");

                        TextBox txt_42FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt42FS");

                        TextBox txt_42HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt42HS");

                        TextBox txt_44FS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt44FS");


                        TextBox txt_44HS =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txt44HS");


                        drpProcess.SelectedValue = dt.Rows[i]["Process"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtRecQuantity.Text = dt.Rows[i]["RecQuantity"].ToString();
                        drpEmp.SelectedValue = dt.Rows[i]["EmpName"].ToString();
                        date.Text = dt.Rows[i]["date"].ToString();



                        txt_36FS.Text = dt.Rows[i]["36FS"].ToString();
                        txt_36HS.Text = dt.Rows[i]["36HS"].ToString();
                        txt_38FS.Text = dt.Rows[i]["38FS"].ToString();
                        txt_38HS.Text = dt.Rows[i]["38HS"].ToString();
                        txt_39FS.Text = dt.Rows[i]["39FS"].ToString();
                        txt_39HS.Text = dt.Rows[i]["39HS"].ToString();
                        txt_40FS.Text = dt.Rows[i]["40FS"].ToString();
                        txt_40HS.Text = dt.Rows[i]["40HS"].ToString();
                        txt_42FS.Text = dt.Rows[i]["42FS"].ToString();
                        txt_42HS.Text = dt.Rows[i]["42HS"].ToString();
                        txt_44FS.Text = dt.Rows[i]["44FS"].ToString();
                        txt_44HS.Text = dt.Rows[i]["44HS"].ToString();



                        rowIndex++;
                        drpProcess.Focus();
                    }
                }
            }
        }

        protected void Add_LotProcessDetails(object sender, EventArgs e)
        {
            if (ddlLotNo.SelectedValue == "Select Lot No")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot No.Thank You!!!.')", true);
                return;
            }

            if (txtProcessDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Process Date')", true);
                return;
            }

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
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    if (drpprocess.SelectedValue != "Select Process Type")
                    {
                        DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpEmp");
                        if (drpEmp.SelectedValue == "Select Employee Name")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name.Thank You!!!.')", true);
                            return;
                        }
                        TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRecQuantity");

                        TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                        if (date.Text == "")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                            return;
                        }

                        DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        DateTime recDate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");





                        itemc = drpprocess.SelectedValue;
                        itemd = recDate.ToString("dd/MM/yyyy");
                        iteme = drpEmp.SelectedValue;


                        if ((itemc == null) || (itemc == ""))
                        {
                        }
                        else
                        {
                            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                            {
                                DropDownList drpprocesss = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpProcess");
                                if (drpprocesss.SelectedValue != "Select Process Type")
                                {
                                    DropDownList drpEmpp = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpEmp");
                                    TextBox datee = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("date");
                                    DateTime recDatee = DateTime.ParseExact(datee.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                                    if (drpEmpp.Text == "")
                                    {
                                    }
                                    else
                                    {

                                        if (iii == iq)
                                        {
                                        }
                                        else
                                        {
                                            if (itemc == drpprocesss.Text && iteme == drpEmpp.SelectedValue && itemd == recDatee.ToString("dd/MM/yyyy"))
                                            {
                                                itemcd = drpprocess.SelectedItem.Text;
                                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd.Trim() + "," + drpEmpp.SelectedItem.Text.Trim() + "," + recDatee.ToString("dd/MM/yyyy") + "  already exists in the Grid.');", true);
                                                drpEmpp.Focus();
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


            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                if (drpprocess.SelectedValue != "Select Process Type")
                {
                    DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
                    if (drpEmp.SelectedValue == "Select Employee Name")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name.Thank You!!!.')", true);
                        return;
                    }
                    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                    if (txtrecQty.Text == "")
                    {
                        txtrecQty.Text = "0";
                    }
                    if (txtrecQty.Text == "0")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Quantity.Thank You!!!.')", true);
                        return;
                    }
                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    if (date.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                        return;
                    }

                    DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime recDate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (processDate > recDate)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter Received date greater than process date.Thank You!!!.')", true);
                        return;
                    }

                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                }
            }

            if (btnadd.Text == "Save")
            {
                DataSet ds = new DataSet();
                ds = objbs.Check_CutID_AlreadyExist(ddlLotNo.SelectedValue, "tblCheckingProcess");
                if (ds.Tables[0].Rows.Count == 0)
                {
                    int iStatus23 = objbs.Insert_CheckingProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToString(ddlLotNo.SelectedItem.Text), Convert.ToInt32(txtledgerid.Text),
                        Convert.ToInt32(txtbrandid.Text), Convert.ToInt32(txtUnitID.Text), Convert.ToInt32(txtTotalQantity.Text), txtdesignno.Text);


                    int i36FS = 0, i36HS = 0, i38FS = 0, i38HS = 0, i39FS = 0, i39HS = 0, i40FS = 0, i40HS = 0, i42FS = 0, i42HS = 0, i44FS = 0, i44HS = 0;


                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {

                        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                        DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
                        TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                        TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");


                        TextBox txt_36FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt36FS");
                        TextBox txt_38FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt38FS");
                        TextBox txt_39FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt39FS");
                        TextBox txt_40FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt40FS");
                        TextBox txt_42FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt42FS");
                        TextBox txt_44FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt44FS");

                        TextBox txt_36HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt36HS");
                        TextBox txt_38HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt38HS");
                        TextBox txt_39HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt39HS");
                        TextBox txt_40HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt40HS");
                        TextBox txt_42HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt42HS");
                        TextBox txt_44HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt44HS");


                        if (drpprocess.SelectedValue == "Select Process Type")
                        {
                        }
                        else
                        {

                            i36FS = Convert.ToInt32(txt_36FS.Text); i36HS = Convert.ToInt32(txt_36HS.Text);
                            i38FS = Convert.ToInt32(txt_38FS.Text); i38HS = Convert.ToInt32(txt_38HS.Text);
                            i39FS = Convert.ToInt32(txt_39FS.Text); i39HS = Convert.ToInt32(txt_39HS.Text);
                            i40FS = Convert.ToInt32(txt_40FS.Text); i40HS = Convert.ToInt32(txt_40HS.Text);
                            i42FS = Convert.ToInt32(txt_42FS.Text); i42HS = Convert.ToInt32(txt_42HS.Text);
                            i44FS = Convert.ToInt32(txt_44FS.Text); i44HS = Convert.ToInt32(txt_44HS.Text);



                            DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            ds = objbs.CheckQuantityOverLoad_CheckingProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
                            string ProcessType = "";
                            int total = 0;

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                int test = Convert.ToInt32(ds.Tables[0].Rows[0]["RecQty"]);
                                ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
                                total = test + Convert.ToInt32(txtrecQty.Text);


                            }
                            if (Convert.ToInt32(txtTotalQantity.Text) >= total)
                            {
                                DataSet dcheck = objbs.getprocesstype(drpprocess.SelectedValue);

                                if (dcheck.Tables[0].Rows.Count > 0)
                                {
                                    int typeid = Convert.ToInt32(dcheck.Tables[0].Rows[0]["Typeid"]);
                                    if (typeid == 0)
                                    {


                                        int istasInsertHistory = objbs.Insert_TransCheckingProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                   recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);


                                        int istas = objbs.Insert_TransCheckingProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                     recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                                    }
                                    else if (typeid == 1)
                                    {

                                        int istasInsertHistory = objbs.Insert_TransCheckingProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);

                                        int istas = objbs.Insert_TransCheckingProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                   recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);
                                    }
                                    else if (typeid == 2)
                                    {

                                        int istasInsertHistory = objbs.Insert_TransCheckingProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);

                                        int istas = objbs.Insert_TransCheckingProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                   recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);
                                    }
                                }

                                //int checkbalqty=Convert.ToInt32(txtfinishedQty.Text)-Convert.ToInt32(txtrecQty.Text);
                                //int res = objbs.Update_CheckingStatus(ddlLotNo.SelectedValue, Convert.ToInt32(txtfinishedQty.Text), Convert.ToInt32(txtrecQty.Text), checkbalqty);


                                int checkbalqty = 0, res = 0;


                                //CheckingProcess Insert
                              //  int res1 = objbs.Insert_CheckingStatusDetails(ddlLotNo.SelectedValue, Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(txtTotalQantity.Text), Convert.ToInt32(txtrecQty.Text), 0, Convert.ToInt32(txtrecQty.Text), 0, 0, "N", ddlLotNo.SelectedItem.Text);

                                int res1 = objbs.Insert_CheckingStatusDetails(ddlLotNo.SelectedValue, Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(txtTotalQantity.Text), 0, 0, 0, 0, 0, "N", ddlLotNo.SelectedItem.Text);


                                //Updating Stitching Process
                                //if (drpprocess.SelectedValue == "5")
                                //{
                                     checkbalqty = Convert.ToInt32(txtfinishedQty.Text) - Convert.ToInt32(txtrecQty.Text);
                                     res = objbs.Update_CheckingStatus(ddlLotNo.SelectedValue, Convert.ToInt32(txtfinishedQty.Text), Convert.ToInt32(txtrecQty.Text), checkbalqty,Convert.ToInt32(txtfinishedQty.Text),5);
                                //}
                                //else if (drpprocess.SelectedValue == "6")
                                //{
                                     checkbalqty=Convert.ToInt32(txtfinishedQty.Text)-Convert.ToInt32(txtrecQty.Text);
                                     res = objbs.Update_CheckingStatus(ddlLotNo.SelectedValue, Convert.ToInt32(txtfinishedQty.Text), Convert.ToInt32(txtrecQty.Text), checkbalqty, Convert.ToInt32(txtfinishedQty.Text),6);
                                //}
                                //else if (drpprocess.SelectedValue == "9")
                                //{
                                     checkbalqty=Convert.ToInt32(txtfinishedQty.Text)-Convert.ToInt32(txtrecQty.Text);
                                     res = objbs.Update_CheckingStatus(ddlLotNo.SelectedValue, Convert.ToInt32(txtfinishedQty.Text), Convert.ToInt32(txtrecQty.Text), checkbalqty, Convert.ToInt32(txtfinishedQty.Text),9);
                              //  }


                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type has enetered Over Quantity!!!.')", true);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    int i36FS = 0, i36HS = 0, i38FS = 0, i38HS = 0, i39FS = 0, i39HS = 0, i40FS = 0, i40HS = 0, i42FS = 0, i42HS = 0, i44FS = 0, i44HS = 0;
                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {

                        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                        DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
                        TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                        TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");



                        TextBox txt_36FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt36FS");
                        TextBox txt_38FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt38FS");
                        TextBox txt_39FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt39FS");
                        TextBox txt_40FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt40FS");
                        TextBox txt_42FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt42FS");
                        TextBox txt_44FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt44FS");

                        TextBox txt_36HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt36HS");
                        TextBox txt_38HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt38HS");
                        TextBox txt_39HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt39HS");
                        TextBox txt_40HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt40HS");
                        TextBox txt_42HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt42HS");
                        TextBox txt_44HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt44HS");

                        if (drpprocess.SelectedValue != "Select Process Type")
                        {



                            i36FS = Convert.ToInt32(txt_36FS.Text); i36HS = Convert.ToInt32(txt_36HS.Text);
                            i38FS = Convert.ToInt32(txt_38FS.Text); i38HS = Convert.ToInt32(txt_38HS.Text);
                            i39FS = Convert.ToInt32(txt_39FS.Text); i39HS = Convert.ToInt32(txt_39HS.Text);
                            i40FS = Convert.ToInt32(txt_40FS.Text); i40HS = Convert.ToInt32(txt_40HS.Text);
                            i42FS = Convert.ToInt32(txt_42FS.Text); i42HS = Convert.ToInt32(txt_42HS.Text);
                            i44FS = Convert.ToInt32(txt_44FS.Text); i44HS = Convert.ToInt32(txt_44HS.Text);


                            DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            ds = objbs.CheckQuantityOverLoad_CheckingProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
                            string ProcessType = "";
                            int total = 0;

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                int test = Convert.ToInt32(ds.Tables[0].Rows[0]["RecQty"]);
                                ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
                                total = test + Convert.ToInt32(txtrecQty.Text);


                            }
                            if (Convert.ToInt32(txtTotalQantity.Text) >= total)
                            {
                              
                                DataSet dcheck = objbs.getprocesstype(drpprocess.SelectedValue);

                                if (dcheck.Tables[0].Rows.Count > 0)
                                {
                                    int typeid = Convert.ToInt32(dcheck.Tables[0].Rows[0]["Typeid"]);
                                    if (typeid == 0)
                                    {
                                        int istas = objbs.Update_TransCheckingProcessandHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                                    }
                                    else if (typeid == 1)
                                    {
                                        int istas = objbs.Update_TransCheckingProcessandHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                       recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);
                                    }
                                    else if (typeid == 2)
                                    {
                                        int istas = objbs.Update_TransCheckingProcessandHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                       recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);
                                    }
                                }


                                //int checkbalqty = Convert.ToInt32(txtnewfinishedqty.Text) - Convert.ToInt32(txtrecQty.Text);
                                //int res = objbs.Update_CheckingStatus(ddlLotNo.SelectedValue, Convert.ToInt32(txtnewfinishedqty.Text), Convert.ToInt32(txtrecQty.Text), checkbalqty,Convert.ToInt32(txtfinishedQty.Text));


                                int checkbalqty = 0, res = 0;

                                //Updating Stiching Process

                            //    int res1 = objbs.Update_CheckingStatusDetails(ddlLotNo.SelectedValue, Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(txtTotalQantity.Text), Convert.ToInt32(txtrecQty.Text), 0, Convert.ToInt32(txtrecQty.Text), 0, 0, "N", ddlLotNo.SelectedItem.Text);

                                int res1 = objbs.Update_CheckingStatusDetails(ddlLotNo.SelectedValue, Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(txtTotalQantity.Text), 0, 0, 0, 0, 0, "N", ddlLotNo.SelectedItem.Text);

                                //Updating Stitching Process
                                //if (drpprocess.SelectedValue == "5")
                                //{
                                     checkbalqty = Convert.ToInt32(txtnewfinishedqty.Text) - Convert.ToInt32(txtrecQty.Text);
                                     res = objbs.Update_CheckingStatus(ddlLotNo.SelectedValue, Convert.ToInt32(txtnewfinishedqty.Text), Convert.ToInt32(txtrecQty.Text), checkbalqty, Convert.ToInt32(txtfinishedQty.Text),5);

                                //}
                                //else if (drpprocess.SelectedValue == "6")
                                //{
                                     checkbalqty = Convert.ToInt32(txtnewfinishedqty.Text) - Convert.ToInt32(txtrecQty.Text);
                                     res = objbs.Update_CheckingStatus(ddlLotNo.SelectedValue, Convert.ToInt32(txtnewfinishedqty.Text), Convert.ToInt32(txtrecQty.Text), checkbalqty, Convert.ToInt32(txtfinishedQty.Text),6);

                                //}
                                //else if (drpprocess.SelectedValue == "9")
                                //{
                                     checkbalqty = Convert.ToInt32(txtnewfinishedqty.Text) - Convert.ToInt32(txtrecQty.Text);
                                     res = objbs.Update_CheckingStatus(ddlLotNo.SelectedValue, Convert.ToInt32(txtnewfinishedqty.Text), Convert.ToInt32(txtrecQty.Text), checkbalqty, Convert.ToInt32(txtfinishedQty.Text),9);

                                //}



                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + ProcessType + " has enetered Over Quantity!!!.')", true);
                                return;
                            }
                        }

                    }
                }
            }
            else if (btnadd.Text == "Update")
            {
                int i36FS = 0, i36HS = 0, i38FS = 0, i38HS = 0, i39FS = 0, i39HS = 0, i40FS = 0, i40HS = 0, i42FS = 0, i42HS = 0, i44FS = 0, i44HS = 0;
                int iscc = objbs.Delete_CheckingProcess(ddlLotNo.SelectedValue);
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {

                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                    DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
                    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");



                    TextBox txt_36FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt36FS");
                    TextBox txt_38FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt38FS");
                    TextBox txt_39FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt39FS");
                    TextBox txt_40FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt40FS");
                    TextBox txt_42FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt42FS");
                    TextBox txt_44FS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt44FS");

                    TextBox txt_36HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt36HS");
                    TextBox txt_38HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt38HS");
                    TextBox txt_39HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt39HS");
                    TextBox txt_40HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt40HS");
                    TextBox txt_42HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt42HS");
                    TextBox txt_44HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt44HS");


                    if (drpprocess.SelectedValue == "Select Process Type")
                    {
                    }
                    else
                    {

                        i36FS = Convert.ToInt32(txt_36FS.Text); i36HS = Convert.ToInt32(txt_36HS.Text);
                        i38FS = Convert.ToInt32(txt_38FS.Text); i38HS = Convert.ToInt32(txt_38HS.Text);
                        i39FS = Convert.ToInt32(txt_39FS.Text); i39HS = Convert.ToInt32(txt_39HS.Text);
                        i40FS = Convert.ToInt32(txt_40FS.Text); i40HS = Convert.ToInt32(txt_40HS.Text);
                        i42FS = Convert.ToInt32(txt_42FS.Text); i42HS = Convert.ToInt32(txt_42HS.Text);
                        i44FS = Convert.ToInt32(txt_44FS.Text); i44HS = Convert.ToInt32(txt_44HS.Text);

                        DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        DataSet dcheck = objbs.getprocesstype(drpprocess.SelectedValue);

                        if (dcheck.Tables[0].Rows.Count > 0)
                        {
                            int typeid = Convert.ToInt32(dcheck.Tables[0].Rows[0]["Typeid"]);
                            if (typeid == 0)
                            {
                                int istasInsertHistory = objbs.Update_CheckingProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                             recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                            }
                            else if (typeid == 1)
                            {
                                int istasInsertHistory = objbs.Update_CheckingProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                             recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);
                            }
                            else if (typeid == 2)
                            {
                                int istasInsertHistory = objbs.Update_CheckingProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                             recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);
                            }
                        }


                    }
                }
                DataSet ds = objbs.Get_TransCheckingHistory_Forupdate(ddlLotNo.SelectedValue);
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int ii = 0; ii < ds.Tables[0].Rows.Count; ii++)
                        {

                            string lotprocessid = ds.Tables[0].Rows[ii]["LotProcessID"].ToString();
                            string processid = ds.Tables[0].Rows[ii]["ProcessTypeID"].ToString();
                            string empid = ds.Tables[0].Rows[ii]["EmpID"].ToString();
                            string RecQty = ds.Tables[0].Rows[ii]["RecQty"].ToString();
                            string ndate = Convert.ToDateTime(ds.Tables[0].Rows[ii]["Date"]).ToString("dd/MM/yyyy");
                            DateTime newdate = DateTime.ParseExact(ndate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string rate = ds.Tables[0].Rows[ii]["Rate"].ToString();
                            string totalqty = ds.Tables[0].Rows[ii]["TotalQty"].ToString();



                            int v36FS = Convert.ToInt32(ds.Tables[0].Rows[ii]["36FS"].ToString());
                            int v36HS = Convert.ToInt32(ds.Tables[0].Rows[ii]["36HS"].ToString());
                            int v38FS = Convert.ToInt32(ds.Tables[0].Rows[ii]["38FS"].ToString());
                            int v38HS = Convert.ToInt32(ds.Tables[0].Rows[ii]["38HS"].ToString());
                            int v39FS = Convert.ToInt32(ds.Tables[0].Rows[ii]["39FS"].ToString());
                            int v39HS = Convert.ToInt32(ds.Tables[0].Rows[ii]["39HS"].ToString());
                            int v40FS = Convert.ToInt32(ds.Tables[0].Rows[ii]["40FS"].ToString());
                            int v40HS = Convert.ToInt32(ds.Tables[0].Rows[ii]["40HS"].ToString());
                            int v42FS = Convert.ToInt32(ds.Tables[0].Rows[ii]["42FS"].ToString());
                            int v42HS = Convert.ToInt32(ds.Tables[0].Rows[ii]["42HS"].ToString());
                            int v44FS = Convert.ToInt32(ds.Tables[0].Rows[ii]["44FS"].ToString());
                            int v44HS = Convert.ToInt32(ds.Tables[0].Rows[ii]["44HS"].ToString());


                            int istas = objbs.Update_TransCheckingProcess(Convert.ToInt32(processid), Convert.ToInt32(empid), Convert.ToInt32(RecQty),
                            newdate, Convert.ToDecimal(rate), ddlLotNo.SelectedValue, Convert.ToInt32(totalqty), v36FS, v36HS, v38FS, v38HS, v39FS, v39HS, v40FS, v40HS, v42FS, v42HS, v44FS, v44HS);
                        }

                        DataSet dn = objbs.Get_TransChecking_Forupdate(ddlLotNo.SelectedValue);
                        if (dn.Tables[0].Rows.Count > 0)
                        {
                            for (int k = 0; k < dn.Tables[0].Rows.Count; k++)
                            {


                                int v36FS = Convert.ToInt32(dn.Tables[0].Rows[k]["36FS"].ToString());
                                int v36HS = Convert.ToInt32(dn.Tables[0].Rows[k]["36HS"].ToString());
                                int v38FS = Convert.ToInt32(dn.Tables[0].Rows[k]["38FS"].ToString());
                                int v38HS = Convert.ToInt32(dn.Tables[0].Rows[k]["38HS"].ToString());
                                int v39FS = Convert.ToInt32(dn.Tables[0].Rows[k]["39FS"].ToString());
                                int v39HS = Convert.ToInt32(dn.Tables[0].Rows[k]["39HS"].ToString());
                                int v40FS = Convert.ToInt32(dn.Tables[0].Rows[k]["40FS"].ToString());
                                int v40HS = Convert.ToInt32(dn.Tables[0].Rows[k]["40HS"].ToString());
                                int v42FS = Convert.ToInt32(dn.Tables[0].Rows[k]["42FS"].ToString());
                                int v42HS = Convert.ToInt32(dn.Tables[0].Rows[k]["42HS"].ToString());
                                int v44FS = Convert.ToInt32(dn.Tables[0].Rows[k]["44FS"].ToString());
                                int v44HS = Convert.ToInt32(dn.Tables[0].Rows[k]["44HS"].ToString());

                                string TOtProcid = dn.Tables[0].Rows[k]["Processtypeid"].ToString();

                                int iss = objbs.updatereceiveqty(ddlLotNo.SelectedValue, TOtProcid, v36FS, v36HS, v38FS, v38HS, v39FS, v39HS, v40FS, v40HS, v42FS, v42HS, v44FS, v44HS);
                            }



                            for (int j = 0; j < dn.Tables[0].Rows.Count; j++)
                            {
                                string Procid = dn.Tables[0].Rows[j]["Processtypeid"].ToString();
                                string recqty = dn.Tables[0].Rows[j]["RecQty"].ToString();


                                int v36FS = Convert.ToInt32(dn.Tables[0].Rows[j]["36FS"].ToString());
                                int v36HS = Convert.ToInt32(dn.Tables[0].Rows[j]["36HS"].ToString());
                                int v38FS = Convert.ToInt32(dn.Tables[0].Rows[j]["38FS"].ToString());
                                int v38HS = Convert.ToInt32(dn.Tables[0].Rows[j]["38HS"].ToString());
                                int v39FS = Convert.ToInt32(dn.Tables[0].Rows[j]["39FS"].ToString());
                                int v39HS = Convert.ToInt32(dn.Tables[0].Rows[j]["39HS"].ToString());
                                int v40FS = Convert.ToInt32(dn.Tables[0].Rows[j]["40FS"].ToString());
                                int v40HS = Convert.ToInt32(dn.Tables[0].Rows[j]["40HS"].ToString());
                                int v42FS = Convert.ToInt32(dn.Tables[0].Rows[j]["42FS"].ToString());
                                int v42HS = Convert.ToInt32(dn.Tables[0].Rows[j]["42HS"].ToString());
                                int v44FS = Convert.ToInt32(dn.Tables[0].Rows[j]["44FS"].ToString());
                                int v44HS = Convert.ToInt32(dn.Tables[0].Rows[j]["44HS"].ToString());


                                int ii = objbs.Update_TranslotDetails(ddlLotNo.SelectedValue, Procid, recqty, v36FS, v36HS, v38FS, v38HS, v39FS, v39HS, v40FS, v40HS, v42FS, v42HS, v44FS, v44HS);


                            }
                        }

                    }

                }


            }



            System.Threading.Thread.Sleep(3000);

            Response.Redirect("CheckingProcessGrid.aspx");


        }

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


        protected void txt36FS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_38FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt38FS");
            txt_38FS.Text = "";
            txt_38FS.Focus();

        }

        protected void txt38FS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_39FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt39FS");
            txt_39FS.Text = "";
            txt_39FS.Focus();

        }


        protected void txt39FS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_40FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt40FS");
            txt_40FS.Text = "";
            txt_40FS.Focus();
        }

        protected void txt40FS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_42FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt42FS");
            txt_42FS.Text = "";
            txt_42FS.Focus();

        }


        protected void txt42FS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_44FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt44FS");
            txt_44FS.Text = "";
            txt_44FS.Focus();

        }

        protected void txt44FS_TextChanged(object sender, EventArgs e)
        {


            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_Date = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("date");
            txt_Date.Focus();
           
        }


        protected void txt36HS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_38HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt38HS");
            txt_38HS.Text = "";
            txt_38HS.Focus();

        }


        protected void txt38HS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_39HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt39HS");
            txt_39HS.Text = "";
            txt_39HS.Focus();

        }


        protected void txt39HS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_40HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt40HS");
            txt_40HS.Text = "";
            txt_40HS.Focus();
        }


        protected void txt40HS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_42HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt42HS");
            txt_42HS.Text = "";
            txt_42HS.Focus();

        }


        protected void txt42HS_TextChanged(object sender, EventArgs e)
        {

            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_44HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt44HS");
            txt_44HS.Text = "";
            txt_44HS.Focus();
        }



        protected void txt44HS_TextChanged(object sender, EventArgs e)
        {

            Total_receivedQty(sender);
            txtrecqtychnaged_text(sender, e);


            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_36FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt36FS");
            txt_36FS.Text = "";
            txt_36FS.Focus();
           

        }

        protected void Total_receivedQty(object sender)
        {
            int FSTotal = 0;
            int HSTotal = 0;



            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;



            TextBox txt_36FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt36FS");
            TextBox txt_38FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt38FS");
            TextBox txt_39FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt39FS");
            TextBox txt_40FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt40FS");
            TextBox txt_42FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt42FS");
            TextBox txt_44FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt44FS");
            TextBox txt_36HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt36HS");
            TextBox txt_38HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt38HS");
            TextBox txt_39HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt39HS");
            TextBox txt_40HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt40HS");
            TextBox txt_42HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt42HS");
            TextBox txt_44HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt44HS");
            TextBox txt_RecQuantity = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txtRecQuantity");



            if (txt_36FS.Text != "" && txt_38FS.Text != "" && txt_39FS.Text != "" && txt_40FS.Text != "" && txt_42FS.Text != "" && txt_44FS.Text != "")
            {
                FSTotal = FSTotal + (Convert.ToInt32(txt_36FS.Text) + Convert.ToInt32(txt_38FS.Text) + Convert.ToInt32(txt_39FS.Text) + Convert.ToInt32(txt_40FS.Text) + Convert.ToInt32(txt_42FS.Text) + Convert.ToInt32(txt_44FS.Text));
            }


            if (txt_36HS.Text != "" && txt_38HS.Text != "" && txt_39HS.Text != "" && txt_40HS.Text != "" && txt_42HS.Text != "" && txt_44HS.Text != "")
            {
                HSTotal = HSTotal + (Convert.ToInt32(txt_36HS.Text) + Convert.ToInt32(txt_38HS.Text) + Convert.ToInt32(txt_39HS.Text) + Convert.ToInt32(txt_40HS.Text) + Convert.ToInt32(txt_42HS.Text) + Convert.ToInt32(txt_44HS.Text));
            }

            txt_RecQuantity.Text = Convert.ToString(FSTotal + HSTotal);
        }



        protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //GridView gv = e.Row.FindControl("gvdetails") as GridView;
                //GridView gvGroup = (GridView)sender;


                ////string lotid = e.Row.Cells[1].Text;


                //HiddenField HD_LotDetailID = (HiddenField)e.Row.FindControl("HDLotDetailID");
                //int lotdetailid = Convert.ToInt32(HD_LotDetailID.Value);

                //if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                //{
                //    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);
                //    DataSet ds = objbs.Get_TransLotDetails_ByProcess(groupID, lotdetailid);

                //    if (ds.Tables[0].Rows.Count > 0)
                //    {
                //        gv.DataSource = ds;
                //        gv.DataBind();
                //    }

                //}

            }
        }


    }
}