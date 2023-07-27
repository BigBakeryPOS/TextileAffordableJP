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
    public partial class Dillo_WashWorks : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        int totalqty = 0;
        string lot = "";
        protected void Page_Load(object sender, EventArgs e)
        {
             lot = Request.QueryString.Get("lotid");
            if (!IsPostBack)
            {
                DataSet dsLotNo = objbs.getlotnoforcheckbox("Wash");//tblCut
                if (dsLotNo.Tables[0].Rows.Count > 0)
                {
                    chkLotNo.DataSource = dsLotNo.Tables[0];
                    chkLotNo.DataTextField = "LotNo";
                    chkLotNo.DataValueField = "CutID";
                    chkLotNo.DataBind();
                }

                DataSet dss = new DataSet();
                if (dsLotNo.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsLotNo.Tables[0].Rows.Count; i++)
                    {
                        string cutid = dsLotNo.Tables[0].Rows[i]["Cutid"].ToString();
                        dss = objbs.checkcurrentststus(cutid);
                        if (dss.Tables[0].Rows.Count > 0)
                        {

                        }
                        else
                        {
                            chkLotNo.Items.Remove(chkLotNo.Items.FindByValue(cutid));
                        }
                    }
                }

                // divWorkManual.Visible = false;
                DataSet chkEmp = objbs.SelectSupervosorName();
                if (chkEmp.Tables[0].Rows.Count > 0)
                {
                    drpsupervisor.DataSource = chkEmp.Tables[0];
                    drpsupervisor.DataTextField = "Name";
                    drpsupervisor.DataValueField = "Employee_Id";
                    drpsupervisor.DataBind();
                    drpsupervisor.Items.Insert(0, "Select Employee Name");
                }

                DataSet jobwork = objbs.selectJobWorkDet(6, 2);
                if (jobwork.Tables[0].Rows.Count > 0)
                {
                    drpjobwork.DataSource = jobwork.Tables[0];
                    drpjobwork.DataTextField = "Ledgername";
                    drpjobwork.DataValueField = "Ledgerid";
                    drpjobwork.DataBind();
                    drpjobwork.Items.Insert(0, "Select Jobworker Name");
                }



                //DataSet dsUnitName = objbs.Select_UnitFirst();//tblUnit
                //if (dsUnitName.Tables[0].Rows.Count > 0)
                //{
                //    ddlUnit.DataSource = dsUnitName.Tables[0];
                //    ddlUnit.DataTextField = "UnitName";
                //    ddlUnit.DataValueField = "UnitID";
                //    ddlUnit.DataBind();
                //    ddlUnit.Items.Insert(0, "Select Unit Name");
                //}
                if (lot != null)
                {
                    DataSet ds = objbs.SelectWashWork_View(lot);
                    {
                      //  btnadd.Visible = false;
                        btnadd.Text = "Update";
                        // ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["unitid"].ToString();

                        DataSet temp = new DataSet();
                        DataTable dtt = new DataTable();

                        dtt.Columns.Add(new DataColumn("cutid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Lotno", typeof(string)));
                        dtt.Columns.Add(new DataColumn("unitid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Unit", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Brandid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Brand", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Qty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Designno", typeof(string)));
                        temp.Tables.Add(dtt);



                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            DataRow dr = dtt.NewRow();
                            dr["Cutid"] = ds.Tables[0].Rows[i]["Cutid"].ToString();
                            dr["Lotno"] = ds.Tables[0].Rows[i]["Lotno"].ToString();
                            dr["Unitid"] = ds.Tables[0].Rows[i]["unitid"].ToString();
                            dr["Unit"] = ds.Tables[0].Rows[i]["UnitName"].ToString();
                            dr["Brandid"] = ds.Tables[0].Rows[i]["BrandId"].ToString();
                            dr["Brand"] = ds.Tables[0].Rows[i]["BrandName"].ToString();
                            dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                            dr["Qty"] = ds.Tables[0].Rows[i]["TotalQty"].ToString();
                            dr["Designno"] = ds.Tables[0].Rows[i]["DesignNo"].ToString();
                            totalqty = totalqty + Convert.ToInt32(ds.Tables[0].Rows[0]["TotalQty"]);

                            //  dt.Rows.Add(dr);
                            temp.Tables[0].Rows.Add(dr);
                        }

                        ViewState["CurrentTable1"] = dtt;

                        gvcustomerorder.DataSource = temp;
                        gvcustomerorder.DataBind();

                        for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                        {
                            TextBox txt_cutid = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcutid");
                            TextBox txt_lotno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtlotno");
                            TextBox txt_unitid = (TextBox)gvcustomerorder.Rows[i].FindControl("txtunitid");
                            TextBox txt_unitname = (TextBox)gvcustomerorder.Rows[i].FindControl("txtunitname");
                            TextBox txt_brandid = (TextBox)gvcustomerorder.Rows[i].FindControl("txtbrandid");
                            TextBox txt_brandname = (TextBox)gvcustomerorder.Rows[i].FindControl("txtbrandname");
                            TextBox txt_qty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtqty");
                            TextBox txt_rate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                            TextBox txt_design = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesign");


                            txt_cutid.Text = temp.Tables[0].Rows[i]["Cutid"].ToString();
                            txt_lotno.Text = temp.Tables[0].Rows[i]["Cutid"].ToString();
                            txt_unitid.Text = temp.Tables[0].Rows[i]["Unitid"].ToString();
                            txt_unitname.Text = temp.Tables[0].Rows[i]["Unit"].ToString();
                            txt_brandid.Text = temp.Tables[0].Rows[i]["Brandid"].ToString();
                            txt_brandname.Text = temp.Tables[0].Rows[i]["Brand"].ToString();
                            txt_qty.Text = temp.Tables[0].Rows[i]["Qty"].ToString();
                            txt_rate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();
                            txt_design.Text = temp.Tables[0].Rows[i]["DesignNo"].ToString();
                        }

                        //gvcustomerorder.Enabled = false;
                        ////  ddlLotNo.Enabled = false;
                        //// UnitChange(sender, e);
                        ////  ddlLotNo.SelectedValue = ds.Tables[0].Rows[0]["cutid"].ToString();
                        //StitchingInfo_Load(sender, e);


                        txtdcno.Text = ds.Tables[0].Rows[0]["DCno"].ToString();
                        drpjobwork.SelectedValue = ds.Tables[0].Rows[0]["Empid"].ToString();
                        drpsupervisor.SelectedValue = ds.Tables[0].Rows[0]["Supervisor"].ToString();
                        txtsendingdate.Text = ds.Tables[0].Rows[0]["SendingDate"].ToString();
                        txtdcdate.Text = ds.Tables[0].Rows[0]["DcDate"].ToString();


                        chkLotNo.Items.Clear();

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            chkLotNo.DataSource = ds.Tables[0];
                            chkLotNo.DataTextField = "Cutid";
                            chkLotNo.DataValueField = "Cutid";
                            chkLotNo.DataBind();
                        }


                        foreach (ListItem listItem in chkLotNo.Items)
                        {
                            listItem.Selected = true;
                        }

                        chkLotNo.Enabled = false;

                    }
                }
                else
                {
                    DataSet dmaxbill = new DataSet();
                    dmaxbill = objbs.getmaxbillwashwork();
                    if (dmaxbill.Tables[0].Rows.Count > 0)
                    {
                        txtdcno.Text = dmaxbill.Tables[0].Rows[0]["mbill"].ToString();
                        txtdcdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    //  FirstGridViewRow();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

            }
        }

        protected void drpprocess_selected(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //DropDownList ddlprocess = (DropDownList)row.FindControl("drpProcess");
            //DropDownList ddlEmp = (DropDownList)row.FindControl("drpEmp");
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
            //ddlEmp.Focus();

        }

        //protected void UnitChange(object sender, EventArgs e)
        //{
        //    DataSet dsLotNo = objbs.Select_Lotnewkaja(Convert.ToInt32(ddlUnit.SelectedValue));//tblCut
        //    if (dsLotNo.Tables[0].Rows.Count > 0)
        //    {
        //        ddlLotNo.DataSource = dsLotNo.Tables[0];
        //        ddlLotNo.DataTextField = "LotNo";
        //        ddlLotNo.DataValueField = "CutID";
        //        ddlLotNo.DataBind();
        //        ddlLotNo.Items.Insert(0, "Select Lot No");
        //    }
        //}


        private bool IsParticipantExists(string val)
        {
            bool exists = false;
            //Loop through each item in selected participant checkboxlist
            foreach (ListItem item in chkLotNo.Items)
            {
                //Check if item selected already exists in the selected participant checboxlist or not
                if (item.Value == val)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        protected void StitchingInfo_Load(object sender, EventArgs e)
        {
            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            string cond = "";
            string cond1 = "";
            int tot = 0;
            ViewState["Data"] = null;
            DataTable dt = new DataTable();
            divcode.Visible = false;

            dt.Columns.Add("Cutid");
            dt.Columns.Add("Lotno");
            dt.Columns.Add("Unitid");
            dt.Columns.Add("Unit");
            dt.Columns.Add("Brandid");
            dt.Columns.Add("Brand");
            dt.Columns.Add("Rate");
            dt.Columns.Add("Qty");
            dt.Columns.Add("Designno");
            ViewState["Data"] = dt;
            // ViewState["Data"] = null;
            //  ViewState.Clear();
            //  dteo = objBs.getjobcardlistdesign(CheckBoxList1.SelectedValue);
            if (chkLotNo.SelectedIndex >= 0)
            {
                foreach (ListItem item in chkLotNo.Items)
                {
                    //check if item selected
                    if (item.Selected)
                    {
                        // Add participant to the selected list if not alreay added
                        if (!IsParticipantExists(item.Value))
                        {

                        }
                        else
                        {
                            DataSet getvalue = objbs.getLotNoTransDetails(Convert.ToInt32(item.Value));
                            if (getvalue.Tables[0].Rows.Count > 0)
                            {
                                DataTable dCrt = new DataTable();
                                dCrt = (DataTable)ViewState["Data"];


                                DataRow dr = dCrt.NewRow();
                                DataRow drr = dCrt.NewRow();


                                dr["Cutid"] = item.Value;
                                dr["Lotno"] = item.Text;
                                dr["Unitid"] = getvalue.Tables[0].Rows[0]["UnitID"].ToString();
                                dr["Unit"] = getvalue.Tables[0].Rows[0]["UnitName"].ToString();
                                dr["Brandid"] = getvalue.Tables[0].Rows[0]["BrandID"].ToString();
                                dr["Brand"] = getvalue.Tables[0].Rows[0]["BrandName"].ToString();
                                dr["Rate"] = getvalue.Tables[0].Rows[0]["Rate"].ToString();
                                dr["Qty"] = getvalue.Tables[0].Rows[0]["TotalQuantity"].ToString();
                                dr["Designno"] = getvalue.Tables[0].Rows[0]["Designno"].ToString();
                                tot = tot + Convert.ToInt32(getvalue.Tables[0].Rows[0]["TotalQuantity"]);
                                dCrt.Rows.Add(dr);

                                ViewState["CurrentTable1"] = dCrt;
                                gvcustomerorder.DataSource = dCrt;
                                gvcustomerorder.DataBind();

                            }
                        }
                    }
                }
            }
            else
            {
                ViewState["Data"] = null;
                gvcustomerorder.DataSource = null;
                gvcustomerorder.DataBind();
            }
            int toatal = tot;
            txtTotalQantity.Text = toatal.ToString();

            //DataSet dataSet = objbs.getLotNoTransDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
            //if (dataSet.Tables[0].Rows.Count > 0)
            //{
            //    //txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
            //    //txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();

            //    //txtUnitName.Text = dataSet.Tables[0].Rows[0]["UnitName"].ToString();
            //    //txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString();

            //    //txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
            //    //txtbrandid.Text = dataSet.Tables[0].Rows[0]["BrandID"].ToString();
            //    //txtUnitID.Text = dataSet.Tables[0].Rows[0]["UnitID"].ToString();

            //    string lotno = "0";
            //    if (ddlLotNo.SelectedValue == "Select Lot No")
            //    {
            //        lotno = "0";
            //    }
            //    else
            //    {
            //        lotno = ddlLotNo.SelectedValue;
            //    }

            //    DataSet darrived = new DataSet();
            //    darrived = objbs.SelectProcessTypekajaforarrivedProcess(ddlLotNo.SelectedValue);
            //    if (darrived.Tables[0].Rows.Count > 0)
            //    {
            //       // txtarrivedQty.Text = darrived.Tables[0].Rows[0]["Qty"].ToString();
            //    }

            //    //DataSet dlotprocess = new DataSet();
            //    DataSet drpProcess = objbs.SelectProcessTypeLotProcessKaja(Convert.ToInt32(ddlLotNo.SelectedValue));
            //    GridView2.DataSource = drpProcess;
            //    GridView2.DataBind();

            //    DataSet workProcess = objbs.SelectWorkProcessType(Convert.ToInt32(ddlLotNo.SelectedValue));
            //    GridView3.DataSource = workProcess;
            //    GridView3.DataBind();

            //    DataSet workProcessManual = objbs.SelectWorkProcessTypeManual(Convert.ToInt32(ddlLotNo.SelectedValue));
            //    if (workProcessManual.Tables[0].Rows[0]["IsManual"].ToString() == "True")
            //    {
            //        divWorkManual.Visible = true;
            //        divWork.Visible = false;
            //        GridView4.DataSource = workProcessManual;
            //        GridView4.DataBind();
            //    }
            //    else
            //    {
            //        divWorkManual.Visible = false;
            //        divWork.Visible = true;
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
            //string lotno = "0";
            //if (ddlLotNo.SelectedValue == "Select Lot No" || ddlLotNo.SelectedValue == " ")
            //{
            //    lotno = "0";
            //}
            //else
            //{
            //    lotno = ddlLotNo.SelectedValue;
            //}
            //DataSet drpProcess = objbs.SelectProcessTypekajaProcess();
            //DataSet drpEmp = objbs.SelectEmpName();


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    DropDownList drpProcess1 = (DropDownList)e.Row.FindControl("drpProcess");
            //    DropDownList drpEmp1 = (DropDownList)e.Row.FindControl("drpEmp");

            //    var ddProcess = (DropDownList)e.Row.FindControl("drpProcess");
            //    ddProcess.DataSource = drpProcess;
            //    ddProcess.DataTextField = "ProcessType";
            //    ddProcess.DataValueField = "ProcessMasterID";
            //    ddProcess.DataBind();
            //    ddProcess.Items.Insert(0, "Select Process Type");

            //    var ddEmp = (DropDownList)e.Row.FindControl("drpEmp");
            //    //if (chckJobWork.Checked == true)
            //    //{
            //        DataSet dsJobWork = objbs.selectJobWorkDet(6, 2);
            //        ddEmp.DataSource = dsJobWork.Tables[0];
            //        ddEmp.DataTextField = "LedgerName";
            //        ddEmp.DataValueField = "LedgerID";
            //        ddEmp.DataBind();
            //        ddEmp.Items.Insert(0, "Select JobWork Name");
            //    //}
            //    //else
            //    //{
            //    //    ddEmp.DataSource = drpEmp;
            //    //    ddEmp.DataTextField = "Name";
            //    //    ddEmp.DataValueField = "Employee_Id";
            //    //    ddEmp.DataBind();
            //    //    ddEmp.Items.Insert(0, "Select Employee Name");
            //    //}

            //}
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
                    // FirstGridViewRow();
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

                        TextBox cuttid =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtcutid");
                        TextBox lotno =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtlotno");


                        TextBox unitid =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtunitid");
                        TextBox unit =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtunitname");
                        TextBox brand =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtbrandname");
                        TextBox brandid =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtbrandid");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtRate");

                        TextBox txtqty =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtqty");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Cutid"] = cuttid.Text;
                        dtCurrentTable.Rows[i - 1]["Lotno"] = lotno.Text;
                        //dtCurrentTable.Rows[i - 1]["Unit"] = unitid.Text;
                        //dtCurrentTable.Rows[i - 1]["Unitid"] = unit.Text;

                        dtCurrentTable.Rows[i - 1]["Unitid"] = unitid.Text;
                        dtCurrentTable.Rows[i - 1]["Unit"] = unit.Text;

                        dtCurrentTable.Rows[i - 1]["Brand"] = brand.Text;
                        dtCurrentTable.Rows[i - 1]["Brandid"] = brandid.Text;
                        dtCurrentTable.Rows[i - 1]["Qty"] = txtqty.Text;
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
            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("Process", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtt.Columns.Add(new DataColumn("EmpName", typeof(string)));
            dtt.Columns.Add(new DataColumn("RecQuantity", typeof(string)));
            dtt.Columns.Add(new DataColumn("date", typeof(string)));

            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["Process"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["EmpName"] = string.Empty;
            dr["RecQuantity"] = string.Empty;
            dr["date"] = string.Empty;

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

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["OrderNo"] = "";
            drNew["Process"] = "";
            drNew["Rate"] = "";
            drNew["EmpName"] = "";
            drNew["RecQuantity"] = "";
            drNew["date"] = "";

            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();

        }



        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void AddNewRow()
        {
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                //TextBox txttrans = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttransid");
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
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
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

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Process"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["RecQuantity"] = txtRecQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["EmpName"] = drpEmp.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["date"] = date.Text;
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

            for (int i = 1; i <= gvcustomerorder.Rows.Count; i++)
            {
                DropDownList drpProcess1 =
                 (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpProcess");

                drpProcess1.Focus();
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

                        TextBox cuttid =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtcutid");
                        TextBox lotno =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtlotno");


                        TextBox unitid =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtunitid");
                        TextBox unit =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtunitname");
                        TextBox brand =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtbrandname");
                        TextBox brandid =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtbrandid");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtRate");

                        TextBox txtqty =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtqty");

                        cuttid.Text = dt.Rows[i]["Cutid"].ToString();
                        lotno.Text = dt.Rows[i]["Lotno"].ToString();
                        unitid.Text = dt.Rows[i]["Unitid"].ToString();
                        unit.Text = dt.Rows[i]["Unit"].ToString();
                        brandid.Text = dt.Rows[i]["Brandid"].ToString();
                        brand.Text = dt.Rows[i]["Brand"].ToString();
                        txtqty.Text = dt.Rows[i]["Qty"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        rowIndex++;

                    }
                }
            }
        }

        protected void Add_LotProcessDetails(object sender, EventArgs e)
        {
            string cond = "";
            if (drpjobwork.SelectedValue == "Select Jobworker Name")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select JobWorker Name.Thank You!!!.')", true);
                return;
            }
            if (drpsupervisor.SelectedValue == "Select Employee Name")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name.Thank You!!!.')", true);
                return;
            }

            if (txtsendingdate.Text=="")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Sending Date.Thank You!!!.')", true);
                return;
            }

            if (chkLotNo.SelectedIndex >= 0)
            {

                foreach (ListItem listItem in chkLotNo.Items)
                {
                    if (listItem.Text != "All")
                    {
                        if (listItem.Selected)
                        {
                            cond += listItem.Value + ",";
                        }
                    }
                }
                cond = cond.TrimEnd(',');

               
                string design = "";

                for (int rowIndex = 0; rowIndex < gvcustomerorder.Rows.Count; rowIndex++)
                {
                    TextBox txtdesign =
                              (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtdesign");
                    if (design == null)
                    {
                        design = txtdesign.Text;
                    }
                    else
                    {
                        design = design + " " + " , " + txtdesign.Text;
                    }
                }

                if (btnadd.Text == "Save")
                {


                    DateTime deliverydate = DateTime.ParseExact(txtdcdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime sendingdate = DateTime.ParseExact(txtsendingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime receivingdate = DateTime.ParseExact(txtsendingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                   int iscuscc = objbs.insertWashwork(txtdcno.Text, deliverydate, sendingdate, receivingdate, drpsupervisor.SelectedValue, cond, txtTotalQantity.Text, design);

                   

                    for (int rowIndex = 0; rowIndex < gvcustomerorder.Rows.Count; rowIndex++)
                    {
                        TextBox cuttid =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtcutid");
                        TextBox lotno =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtlotno");


                        TextBox unitid =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtunitid");
                        TextBox unit =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtunitname");
                        TextBox brand =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtbrandname");
                        TextBox brandid =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtbrandid");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtRate");

                        TextBox txtqty =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtqty");

                        TextBox txtdesign =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtdesign");

                        int itrans = objbs.inserttransWashwork(cuttid.Text, lotno.Text, drpjobwork.SelectedValue, txtRate.Text, txtqty.Text, unitid.Text, brandid.Text, txtdesign.Text);

                    }

                }
                else if (btnadd.Text == "Update")
                {

                    DateTime deliverydate = DateTime.ParseExact(txtdcdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime sendingdate = DateTime.ParseExact(txtsendingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime receivingdate = DateTime.ParseExact(txtsendingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                  // int isss = objbs.update_Wash_LotProcess(Convert.ToInt32(lot));

                    int iscuscc = objbs.Update_Washwork(Convert.ToInt32(lot), txtdcno.Text, deliverydate, sendingdate, receivingdate, drpsupervisor.SelectedValue, cond, txtTotalQantity.Text, design);


                    objbs.Delete_transWashProcess(Convert.ToInt32(lot));

                    for (int rowIndex = 0; rowIndex < gvcustomerorder.Rows.Count; rowIndex++)
                    {
                        TextBox cuttid =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtcutid");
                        TextBox lotno =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtlotno");


                        TextBox unitid =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtunitid");
                        TextBox unit =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtunitname");
                        TextBox brand =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtbrandname");
                        TextBox brandid =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtbrandid");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtRate");

                        TextBox txtqty =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtqty");

                        TextBox txtdesign =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtdesign");

                        int itrans = objbs.Update_transWashwork(cuttid.Text, lotno.Text, drpjobwork.SelectedValue, txtRate.Text, txtqty.Text, unitid.Text, brandid.Text,Convert.ToInt32(lot), txtdesign.Text);

                    }
                }
                Response.Redirect("Dillo_WashWorkGrid.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot No.Thank You!!!.')", true);
                return;
            }


            //if (ddlLotNo.SelectedValue == "Select Lot No")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot No.Thank You!!!.')", true);
            //    return;
            //}

            //DataSet ds = new DataSet();
            //ds = objbs.checkkajaalreadyexistornot(ddlLotNo.SelectedValue);
            //if (ds.Tables[0].Rows.Count == 0)
            //{
            //    int iStatus23 = objbs.insertKajaProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToString(ddlLotNo.SelectedItem.Text), Convert.ToInt32(txtledgerid.Text),
            //        Convert.ToInt32(txtbrandid.Text), Convert.ToInt32(txtUnitID.Text), Convert.ToInt32(txtTotalQantity.Text));

            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {

            //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //        DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
            //        TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
            //        TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
            //        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

            //        date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //        DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //        if (drpprocess.SelectedValue == "Select Process Type")
            //        {
            //        }
            //        else
            //        {
            //            ds = objbs.CheckQuantityOverLoadkajaProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
            //            string ProcessType = "";
            //            int total = 0;
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                int test = Convert.ToInt32(ds.Tables[0].Rows[0]["RecQty"]);
            //                ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
            //                total = test + Convert.ToInt32(txtrecQty.Text);

            //            }

            //            if (Convert.ToInt32(txtTotalQantity.Text) >= total)
            //            {
            //                string chk = "";
            //                if (chckJobWork.Checked == true)
            //                {
            //                    chk = "Out";

            //                }
            //                else
            //                {
            //                    chk = "In";
            //                }
            //                int istasHistory = objbs.inserttranskajaProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
            //                recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(ddlLotNo.SelectedItem.Text));

            //                int istas = objbs.inserttranskajaProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
            //                recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk);
            //            }
            //            else
            //            {
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + ProcessType + " has enetered Over Quantity!!!.')", true);
            //                return;
            //            }
            //        }
            //    }
            //}
            //else
            //{

            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {

            //        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //        DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
            //        TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
            //        TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
            //        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

            //        date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //        DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //        if (drpprocess.SelectedValue == "Select Process Type")
            //        {
            //        }
            //        else
            //        {
            //            ds = objbs.CheckQuantityOverLoadkajaProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
            //            string ProcessType = "";
            //            int total = 0;
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                int test = Convert.ToInt32(ds.Tables[0].Rows[0]["RecQty"]);
            //                ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
            //                total = test + Convert.ToInt32(txtrecQty.Text);

            //            }

            //            if (Convert.ToInt32(txtTotalQantity.Text) >= total)
            //            {
            //                string chk = "";
            //                if (chckJobWork.Checked == true)
            //                {
            //                    chk = "Out";

            //                }
            //                else
            //                {
            //                    chk = "In";
            //                }
            //                //int istasHistory = objbs.inserttranskajaProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
            //                //recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(ddlLotNo.SelectedItem.Text));

            //                int istas = objbs.UpdatetranskajaProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
            //                recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk);
            //            }
            //            else
            //            {
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + ProcessType + " has enetered Over Quantity!!!.')", true);
            //                return;
            //            }

            //        }
            //    }
            //}


            System.Threading.Thread.Sleep(3000);

            Response.Redirect("Dillo_WashWorkGrid.aspx");


        }

        protected void GridViewRate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string rate = e.Row.Cells[2].Text;
            //    decimal rateTotal = Convert.ToDecimal(rate);

            //    foreach (TableCell gr in e.Row.Cells)
            //    {
            //        if (1 <= rateTotal && rateTotal <= 3)
            //        {
            //            gr.BackColor = System.Drawing.Color.Red;
            //        }

            //        if (4 <= rateTotal && rateTotal <= 6)
            //        {
            //            gr.ForeColor = System.Drawing.Color.Green;
            //        }
            //        if (7 <= rateTotal && rateTotal <= 10)
            //        {
            //            gr.ForeColor = System.Drawing.Color.Gray;
            //        }
            //        if (11 <= rateTotal && rateTotal <= 15)
            //        {
            //            gr.ForeColor = System.Drawing.Color.Blue;
            //        }
            //        if (15 <= rateTotal && rateTotal <= 20)
            //        {
            //            gr.ForeColor = System.Drawing.Color.Pink;
            //        }
            //    }
            //}
        }

        protected void GridViewWork_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    foreach (TableCell gr in e.Row.Cells)
            //    {
            //        if (gr.Text == "YES")
            //        {
            //            gr.BackColor = System.Drawing.Color.Green;
            //        }
            //        else
            //        {
            //            gr.BackColor = System.Drawing.Color.Red;
            //        }

            //        if (gr.Text == "True")
            //        {
            //            gr.BackColor = System.Drawing.Color.Green;
            //        }
            //        else
            //        {
            //            gr.BackColor = System.Drawing.Color.Red;
            //        }
            //    }
            //}
        }

        protected void Change_JobWork(object sender, EventArgs e)
        {
            //CheckBox chck = (CheckBox)sender;
            //DropDownList ddl = (DropDownList)sender;
            //if (chckJobWork.Checked == true)
            //{
            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {
            //        DropDownList ddEmp = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpEmp");

            //        DataSet dsJobWork = objbs.selectJobWorkDet(6, 2);
            //        ddEmp.DataSource = dsJobWork.Tables[0];
            //        ddEmp.DataTextField = "LedgerName";
            //        ddEmp.DataValueField = "LedgerID";
            //        ddEmp.DataBind();
            //        ddEmp.Items.Insert(0, "Select JobWork Name");
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //    {
            //        DropDownList ddEmp = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpEmp");

            //        DataSet drpEmp = objbs.SelectEmpName();
            //        ddEmp.DataSource = drpEmp;
            //        ddEmp.DataTextField = "Name";
            //        ddEmp.DataValueField = "Employee_Id";
            //        ddEmp.DataBind();
            //        ddEmp.Items.Insert(0, "Select Employee Name");
            //    }
            //}
        }
    }
}