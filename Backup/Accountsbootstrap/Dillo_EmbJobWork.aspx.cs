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
    public partial class Dillo_EmbJobWork : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        string lot = "";

        protected void Page_Load(object sender, EventArgs e)
        {
             lot = Request.QueryString.Get("lotid");
            if (!IsPostBack)
            {
                //DataSet dsLotNo = objbs.Select_Lotnewembroid();//tblCut
                //if (dsLotNo.Tables[0].Rows.Count > 0)
                //{
                //    ddlLotNo.DataSource = dsLotNo.Tables[0];
                //    ddlLotNo.DataTextField = "LotNo";
                //    ddlLotNo.DataValueField = "CutID";
                //    ddlLotNo.DataBind();
                //    ddlLotNo.Items.Insert(0, "Select Lot No");
                //}
                DataSet chkEmp = objbs.SelectSupervosorName();
                if (chkEmp.Tables[0].Rows.Count > 0)
                {
                    drpsupervisor.DataSource = chkEmp.Tables[0];
                    drpsupervisor.DataTextField = "Name";
                    drpsupervisor.DataValueField = "Employee_Id";
                    drpsupervisor.DataBind();
                    drpsupervisor.Items.Insert(0, "Select Employee Name");
                }

                DataSet dsJobWork = objbs.selectJobWorkDet(6, 2);
                drpjobwork.DataSource = dsJobWork.Tables[0];
                drpjobwork.DataTextField = "LedgerName";
                drpjobwork.DataValueField = "LedgerID";
                drpjobwork.DataBind();
                drpjobwork.Items.Insert(0, "Select Job Worker");

                DataSet dsUnitName = objbs.Select_UnitFirst();//tblUnit
                if (dsUnitName.Tables[0].Rows.Count > 0)
                {
                    ddlUnit.DataSource = dsUnitName.Tables[0];
                    ddlUnit.DataTextField = "UnitName";
                    ddlUnit.DataValueField = "UnitID";
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, "Select Unit Name");
                }

                if (lot != null)
                {
                    DataSet ds = objbs.SelectEmbroidingetGridView(lot);
                    {
                       // btnadd.Visible = false;

                        btnadd.Text = "Update";
                     
                        DataSet temp = new DataSet();
                       DataTable dtt = new DataTable();

                        dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Process", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("EmpName", typeof(string)));
                        dtt.Columns.Add(new DataColumn("TotalQuantity", typeof(string)));
                        dtt.Columns.Add(new DataColumn("RecQuantity", typeof(string)));
                        dtt.Columns.Add(new DataColumn("date", typeof(string)));
                        temp.Tables.Add(dtt);



                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            DataRow dr = dtt.NewRow();

                            dr["OrderNo"] = ds.Tables[0].Rows[i]["TransID"].ToString();
                            dr["Process"] = ds.Tables[0].Rows[i]["processtypeid"].ToString();
                            dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                            dr["EmpName"] = ds.Tables[0].Rows[i]["empid"].ToString();
                            dr["TotalQuantity"] = ds.Tables[0].Rows[i]["TotalQuantity"].ToString();
                            dr["RecQuantity"] = ds.Tables[0].Rows[i]["recqty"].ToString();
                            dr["date"] = ds.Tables[0].Rows[i]["date"].ToString();


                            //  dt.Rows.Add(dr);
                            temp.Tables[0].Rows.Add(dr);
                        }

                        ViewState["CurrentTable1"] = dtt;
                        ViewState["CurrentTable11"] = dtt;

                        gvcustomerorder.DataSource = temp;
                        gvcustomerorder.DataBind();

                        for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                        {
                            //Label lbltrans = (Label)gvcustomerorder.Rows[i].FindControl("lbltransno");

                            TextBox txttransid = (TextBox)gvcustomerorder.Rows[i].FindControl("txttransid");

                            DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");

                            TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                            TextBox txtTotalQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txttotqty");

                            TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");

                            DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpEmp");

                            TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");

                            // lbltrans.Text = temp.Tables[0].Rows[i]["OrderNo"].ToString();
                            txttransid.Text = temp.Tables[0].Rows[i]["OrderNo"].ToString();
                            drpprocess.SelectedValue = temp.Tables[0].Rows[i]["Process"].ToString();
                            txtrate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();
                            txtTotalQuantity.Text = temp.Tables[0].Rows[i]["TotalQuantity"].ToString();
                            txtRecQuantity.Text = temp.Tables[0].Rows[i]["RecQuantity"].ToString();
                            drpEmp.SelectedValue = temp.Tables[0].Rows[i]["EmpName"].ToString();
                            date.Text = Convert.ToDateTime(temp.Tables[0].Rows[i]["date"]).ToString("dd/MM/yyyy");
                        }


                        drpjobwork.SelectedValue = ds.Tables[0].Rows[0]["embjobworkid"].ToString();
                        txtUnitID.Text = ds.Tables[0].Rows[0]["UnitID"].ToString();
                        ddlUnit.SelectedValue = txtUnitID.Text;

                        DataSet lotno = new DataSet();
                        lotno = objbs.Select_LotnewEmbcurrent_forUpdate(Convert.ToInt32(ddlUnit.SelectedValue));

                        if (lotno.Tables[0].Rows.Count > 0)
                        {

                            ddlLotNo.DataSource = lotno.Tables[0];
                            ddlLotNo.DataTextField = "LotNo";
                            ddlLotNo.DataValueField = "CutID";
                            ddlLotNo.DataBind();
                            ddlLotNo.Items.Insert(0, "Select Lot No");
                            ddlLotNo.Focus();

                        }


                        ddlLotNo.SelectedValue = ds.Tables[0].Rows[0]["cutid"].ToString();
                        txtledgerid.Text = ds.Tables[0].Rows[0]["LedgerID"].ToString();
                        txtCuttingMaster.Text = ds.Tables[0].Rows[0]["LedgerName"].ToString();
                        txtbrandid.Text = ds.Tables[0].Rows[0]["BrandID"].ToString();
                        txtBrand.Text = ds.Tables[0].Rows[0]["BrandName"].ToString();
                       
                      

                        txtTotalQantity.Text = ds.Tables[0].Rows[0]["TotalQuantity"].ToString();


                        if (ds.Tables[0].Rows[0]["Checkstatus"].ToString() == "Out")
                        {
                            chckJobWork.Checked = true;
                        }
                        else if (ds.Tables[0].Rows[0]["Checkstatus"].ToString() == "In")
                        {
                            chckJobWork.Checked = false;
                        }
                        drpsupervisor.SelectedValue = ds.Tables[0].Rows[0]["RecEmployee"].ToString();
                        lbldesignNo.Text = ds.Tables[0].Rows[0]["DesignNo"].ToString();
                       

                     
                        txtsuper.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                        txtdcno.Text = ds.Tables[0].Rows[0]["dcno"].ToString();
                        txtdcdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DcDate"]).ToString("dd/MM/yyyy");
                      //  txtreceivingdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReceivingDate"]).ToString("dd/MM/yyyy");
                        txtreceivingdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ReceivingDate"]).ToString("dd/MM/yyyy");
                     
               

                        DataSet darrived = new DataSet();
                        darrived = objbs.SelectProcessTypeembroforarrivedProcess(ddlLotNo.SelectedValue);
                        if (darrived.Tables[0].Rows.Count > 0)
                        {
                            txtarrivedQty.Text = darrived.Tables[0].Rows[0]["Qty"].ToString();
                        }

                        DataSet dqty = objbs.getcurreetnqty(ddlLotNo.SelectedValue);
                        if (dqty.Tables[0].Rows.Count > 0)
                        {
                            txtarrivedQty.Text = dqty.Tables[0].Rows[0]["Embqty"].ToString();
                        }
                        else
                        {

                        }



                        DataSet drpProcess = objbs.SelectProcessTypeLotProcessEmbroiding(Convert.ToInt32(ddlLotNo.SelectedValue));
                        GridView2.DataSource = drpProcess;
                        GridView2.DataBind();
                        gvcustomerorder.Enabled = true;

                        //  gvcustomerorder.Enabled = false;
                        ddlLotNo.Enabled = false;
                        //  UnitChange(sender, e);
                        //  StitchingInfo_Load(sender, e);
                    }
                }
                else
                {


                    FirstGridViewRow();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
            }
        }

        protected void drpprocess_selected(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList ddlprocess = (DropDownList)row.FindControl("drpProcess");
            DropDownList ddlEmp = (DropDownList)row.FindControl("drpEmp");
            TextBox txtrate = (TextBox)row.FindControl("txtRate");


            DataSet ds = new DataSet();
            if (ddlprocess.SelectedValue != "Select Process Type")
            {
                ds = objbs.getrateforstiching(ddlprocess.SelectedValue, ddlLotNo.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtrate.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Rate"]).ToString();
                }
            }
            ddlEmp.Focus();
        }


        protected void jobworker_indexchanged(object sender, EventArgs e)
        {
            if (drpjobwork.SelectedValue == "Select Job Worker")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Job Worker Name.Thank You!!!')", true);
                return;
            }

            DataSet getallinformation = new DataSet();
            if (getallinformation.Tables.Count > 0)
            {

            }
            getallinformation = objbs.getnewjobworkprocessemb(drpjobwork.SelectedValue);

            if (getallinformation.Tables[0].Rows.Count > 0)
            {

                ddlLotNo.DataSource = getallinformation.Tables[0];
                ddlLotNo.DataTextField = "LotNo";
                ddlLotNo.DataValueField = "CutID";
                ddlLotNo.DataBind();
                ddlLotNo.Items.Insert(0, "Select Lot No");
                ddlLotNo.Focus();

            }
            else
            {
                ddlLotNo.ClearSelection();
                ddlLotNo.Items.Clear();
                drpjobwork.Focus();
            }

            DataSet dss = new DataSet();
            if (getallinformation.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < getallinformation.Tables[0].Rows.Count; i++)
                {
                    string cutid = getallinformation.Tables[0].Rows[i]["Cutid"].ToString();
                    string unit = getallinformation.Tables[0].Rows[i]["Unitid"].ToString();
                    dss = objbs.checkcurrentststus(cutid);
                    if (dss.Tables[0].Rows.Count > 0)
                    {
                        if (chckJobWork.Checked == true)
                        {
                            DataSet dgetunit = objbs.getcurrentunit(cutid, "Emb");
                            if (dgetunit.Tables[0].Rows.Count > 0)
                            {
                                ddlUnit.SelectedValue = dgetunit.Tables[0].Rows[0]["Unit"].ToString();

                                int dcheck = objbs.checkcurrentforalljobwork(unit, cutid, "Emb");
                                if (dcheck == 1)
                                {
                                }
                                else
                                {
                                    ddlLotNo.Items.Remove(ddlLotNo.Items.FindByValue(cutid));
                                }
                            }
                        }
                    }
                    else
                    {
                        ddlLotNo.Items.Remove(ddlLotNo.Items.FindByValue(cutid));
                    }
                }
            }
           
        }

        protected void StitchingInfo_Load(object sender, EventArgs e)
        {

            DataSet dss = new DataSet();
            dss = objbs.checkcurrentststus(ddlLotNo.SelectedValue);
            if (dss.Tables[0].Rows.Count > 0)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check This Lot no.This Lot is in Stiching process.Thank You!!!')", true);
                return;
            }

            if (chckJobWork.Checked == true)
            {
                 DataSet dgetunit = objbs.getcurrentunit(ddlLotNo.SelectedValue,"Emb");
                 if (dgetunit.Tables[0].Rows.Count > 0)
                 {
                     ddlUnit.SelectedValue = dgetunit.Tables[0].Rows[0]["Unit"].ToString();
                     int dcheck = objbs.checkcurrentforalljobwork(ddlUnit.SelectedValue, ddlLotNo.SelectedValue, "Emb");
                     if (dcheck == 1)
                     {
                         DataSet dataSet = objbs.getLotNoTransDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
                         if (dataSet.Tables[0].Rows.Count > 0)
                         {
                             lbldesignNo.Text = dataSet.Tables[0].Rows[0]["DesignNo"].ToString();
                             txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
                             txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();

                             txtUnitName.Text = dataSet.Tables[0].Rows[0]["UnitName"].ToString();
                             txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString();

                             txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
                             txtbrandid.Text = dataSet.Tables[0].Rows[0]["BrandID"].ToString();
                             txtUnitID.Text = dataSet.Tables[0].Rows[0]["UnitID"].ToString();

                             DataSet getdc = objbs.getdcforjobworkEMB(ddlLotNo.SelectedValue);
                             if (getdc.Tables[0].Rows.Count > 0)
                             {
                                 txtdcno.Text = getdc.Tables[0].Rows[0]["Dcno"].ToString();
                                 txtdcdate.Text = Convert.ToDateTime(getdc.Tables[0].Rows[0]["DcDate"]).ToString("dd/MM/yyyy");
                                 txtsuper.Text = getdc.Tables[0].Rows[0]["Name"].ToString();
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

                             DataSet darrived = new DataSet();
                             darrived = objbs.SelectProcessTypeembroforarrivedProcess(ddlLotNo.SelectedValue);
                             if (darrived.Tables[0].Rows.Count > 0)
                             {
                                 txtarrivedQty.Text = darrived.Tables[0].Rows[0]["Qty"].ToString();
                             }

                             DataSet dqty = objbs.getcurreetnqty(ddlLotNo.SelectedValue);
                             if (dqty.Tables[0].Rows.Count > 0)
                             {
                                 txtarrivedQty.Text = dqty.Tables[0].Rows[0]["Embqty"].ToString();
                             }
                             else
                             {

                             }


                             //Get JobWork Process
                             DataSet djobworkload = objbs.getjobworklotforEmbb(ddlLotNo.SelectedValue);
                             if (djobworkload.Tables[0].Rows.Count > 0)
                             {
                                 gvcustomerorder.DataSource = djobworkload.Tables[0];
                                 gvcustomerorder.DataBind();
                                 for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                 {

                                     DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                                     DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
                                     TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                                     TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                                     TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                                     TextBox txttotq = (TextBox)gvcustomerorder.Rows[i].FindControl("txttotqty");

                                     drpEmp.SelectedValue = djobworkload.Tables[0].Rows[0]["empid"].ToString();
                                     txtrecQty.Text = djobworkload.Tables[0].Rows[0]["RemainQty"].ToString();
                                     txtrate.Text = djobworkload.Tables[0].Rows[0]["Rate"].ToString();
                                     txttotq.Text = djobworkload.Tables[0].Rows[0]["TotalQty"].ToString();
                                     date.Text = DateTime.Now.ToString("dd/MM/yyyy");

                                 }

                             }
                             else
                             {
                                 gvcustomerorder.Enabled = false;
                             }

                             DataSet drpProcess = objbs.SelectProcessTypeLotProcessEmbroiding(Convert.ToInt32(ddlLotNo.SelectedValue));
                             GridView2.DataSource = drpProcess;
                             GridView2.DataBind();

                         }
                         else
                         {
                             gvcustomerorder.DataSource = null;
                             gvcustomerorder.DataBind();
                         }
                     }
                     else
                     {
                         DataSet currentcheck = new DataSet();



                         ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check This Lot no.This Lot is in Another process.Thank You!!!')", true);
                         return;
                     }
                 }
            }
            else
            {

                int dcheck = objbs.checkcurrentforall(ddlUnit.SelectedValue, ddlLotNo.SelectedValue, "Emb");
                if (dcheck == 1)
                {
                    DataSet dataSet = objbs.getLotNoTransDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        lbldesignNo.Text = dataSet.Tables[0].Rows[0]["designNo"].ToString();
                        txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
                        txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();

                        txtUnitName.Text = dataSet.Tables[0].Rows[0]["UnitName"].ToString();
                        txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["TotalQuantity"].ToString();

                        txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
                        txtbrandid.Text = dataSet.Tables[0].Rows[0]["BrandID"].ToString();
                        txtUnitID.Text = dataSet.Tables[0].Rows[0]["UnitID"].ToString();

                        string lotno = "0";
                        if (ddlLotNo.SelectedValue == "Select Lot No")
                        {
                            lotno = "0";
                        }
                        else
                        {
                            lotno = ddlLotNo.SelectedValue;
                        }

                        DataSet darrived = new DataSet();
                        darrived = objbs.SelectProcessTypeembroforarrivedProcess(ddlLotNo.SelectedValue);
                        if (darrived.Tables[0].Rows.Count > 0)
                        {
                            txtarrivedQty.Text = darrived.Tables[0].Rows[0]["Qty"].ToString();
                        }

                        DataSet dqty = objbs.getcurreetnqty(ddlLotNo.SelectedValue);
                        if (dqty.Tables[0].Rows.Count > 0)
                        {
                            txtarrivedQty.Text = dqty.Tables[0].Rows[0]["Embqty"].ToString();
                        }
                        else
                        {

                        }

                        //DataSet drpProcess = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(lotno));
                        ////DropDownList ddl = (DropDownList)sender;
                        ////GridViewRow row = (GridViewRow)ddl.NamingContainer;
                        ////DropDownList ddlprocess = (DropDownList)row.FindControl("drpProcess");
                        ////for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count - 1; vLoop++)
                        //{
                        //    DropDownList dbrand = (DropDownList)gvcustomerorder.Rows[0].FindControl("drpProcess");
                        //    if (drpProcess.Tables[0].Rows.Count > 0)
                        //    {
                        //        dbrand.DataSource = drpProcess.Tables[0];
                        //        dbrand.DataTextField = "ProcessType";
                        //        dbrand.DataValueField = "ProcessMasterID";
                        //        dbrand.DataBind();
                        //        // dbrand.Items.Insert(0, "Select Process Type");
                        //    }
                        //}

                        //  gvcustomerorder.DataSource = drpProcess;
                        //   gvcustomerorder.DataBind();


                        //DataSet dlotprocess = new DataSet();
                        //dlotprocess = objbs.getprocessdetailsforstic(lotno);
                        //if (dlotprocess.Tables[0].Rows.Count > 0)
                        //{
                        //    GridView1.DataSource = dlotprocess;
                        //    GridView1.DataBind();
                        //}


                        DataSet drpProcess = objbs.SelectProcessTypeLotProcessEmbroiding(Convert.ToInt32(ddlLotNo.SelectedValue));
                        GridView2.DataSource = drpProcess;
                        GridView2.DataBind();

                    }
                    else
                    {
                        gvcustomerorder.DataSource = null;
                        gvcustomerorder.DataBind();
                    }
                }
                else
                {
                    DataSet currentcheck = new DataSet();



                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check This Lot no.This Lot is in Another process.Thank You!!!')", true);
                    return;
                }
            }

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string lotno = "0";
            if (ddlLotNo.SelectedValue == "Select Lot No" || ddlLotNo.SelectedValue == " ")
            {
                lotno = "0";
            }
            else
            {
                lotno = ddlLotNo.SelectedValue;
            }
            DataSet drpProcess = objbs.SelectProcessTypeembroProcess();
            DataSet drpEmp = objbs.SelectEmpName();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList drpProcess1 = (DropDownList)e.Row.FindControl("drpProcess");
                DropDownList drpEmp1 = (DropDownList)e.Row.FindControl("drpEmp");

                var ddProcess = (DropDownList)e.Row.FindControl("drpProcess");
                ddProcess.DataSource = drpProcess;
                ddProcess.DataTextField = "ProcessType";
                ddProcess.DataValueField = "ProcessMasterID";
                ddProcess.DataBind();
             //   ddProcess.Items.Insert(0, "Select Process Type");


                var ddEmp = (DropDownList)e.Row.FindControl("drpEmp");
                if (chckJobWork.Checked == true)
                {
                    DataSet dsJobWork = objbs.selectJobWorkDet(6, 2);
                    ddEmp.DataSource = dsJobWork.Tables[0];
                    ddEmp.DataTextField = "LedgerName";
                    ddEmp.DataValueField = "LedgerID";
                    ddEmp.DataBind();
                  //  ddEmp.Items.Insert(0, "Select JobWork Name");
                }
                else
                {
                    ddEmp.DataSource = drpEmp;
                    ddEmp.DataTextField = "Name";
                    ddEmp.DataValueField = "Employee_Id";
                    ddEmp.DataBind();
                    ddEmp.Items.Insert(0, "Select Employee Name");
                }
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
                        TextBox txtTransid =
                  (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txttransid");

                        DropDownList drpProcess =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");

                        TextBox txtTotalQuantity =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txttotqty");

                        TextBox txtRecQuantity =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRecQuantity");

                        DropDownList drpEmp =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpEmp");

                        TextBox date =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("date");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Orderno"] = txtTransid.Text;
                        dtCurrentTable.Rows[i - 1]["Process"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["TotalQuantity"] = txtTotalQuantity.Text;
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
            dtt.Columns.Add(new DataColumn("TotalQuantity", typeof(string)));
            dtt.Columns.Add(new DataColumn("RecQuantity", typeof(string)));
            dtt.Columns.Add(new DataColumn("date", typeof(string)));

            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["Process"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["EmpName"] = string.Empty;
            dr["TotalQuantity"] = string.Empty;
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

            dct = new DataColumn("TotalQuantity");
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
            drNew["TotalQuantity"] = "";
            drNew["RecQuantity"] = "";
            drNew["date"] = "";

            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();

        }

        protected void txtRange_Change(object sender, EventArgs e)
        {
           // ButtonAdd1_Click(sender, e);

            int test = 0;
            if (btnadd.Text == "Save")
            {
                test = Convert.ToInt32(txtarrivedQty.Text);

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    int total = 0;
                    DropDownList drpProcess =
                     (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    if (drpProcess.SelectedValue != "Select Process Type")
                    {
                        //ds = objbs.CheckQuantityOverLoadkajaProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpProcess.SelectedValue));
                        string ProcessType = "";

                        //if (ds.Tables[0].Rows.Count > 0)
                        //{

                        //ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
                        for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                        {

                            DropDownList drpProcessCheck =
                             (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                            if (drpProcessCheck.SelectedValue != "Select Process Type")
                            {
                                if (drpProcess.SelectedItem.Text == drpProcessCheck.SelectedItem.Text)
                                {
                                    TextBox txtRecQuantity =
                                            (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");
                                    total = total + Convert.ToInt32(txtRecQuantity.Text);
                                }

                            }
                            //}
                            if (total > test)
                            {

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + ProcessType + " has enetered Over Quantity!!!.')", true);
                                return;
                            }

                        }
                    }

                }
            }
            else
            {
                test = Convert.ToInt32(txtarrivedQty.Text);

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    int total = 0;
                    DropDownList drpProcess =
                     (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    if (drpProcess.SelectedValue != "Select Process Type")
                    {
                        //ds = objbs.CheckQuantityOverLoadkajaProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpProcess.SelectedValue));
                        string ProcessType = "";

                        //if (ds.Tables[0].Rows.Count > 0)
                        //{

                        //ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
                        for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                        {

                            DropDownList drpProcessCheck =
                             (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                            if (drpProcessCheck.SelectedValue != "Select Process Type")
                            {
                                if (drpProcess.SelectedItem.Text == drpProcessCheck.SelectedItem.Text)
                                {
                                    TextBox txtRecQuantity =
                                            (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");
                                    total = total + Convert.ToInt32(txtRecQuantity.Text);
                                }

                            }
                            //}
                            if (total > test)
                            {

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + ProcessType + " has enetered Over Quantity!!!.')", true);
                                return;
                            }

                        }
                    }

                }
            }
        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {

            int No = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txt_rate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txt_recQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRecQuantity");
                TextBox txt_date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                TextBox txt_totq = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttotqty");
                TextBox txt_transid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttransid");
                DropDownList ddl_process = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");

                if (txt_totq.Text == "")
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

                TextBox txt_recQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRecQuantity");
                txt_recQty.Focus();
            }

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txt_rate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txt_recQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRecQuantity");
                TextBox txt_date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                TextBox txt_totq = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttotqty");
                TextBox txt_transid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttransid");
                DropDownList ddl_process = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");

                if (txt_totq.Text != "")
                {
                }
                else
                {
                    DataSet ds = new DataSet();
                    if (ddl_process.SelectedValue != "Select Process Type")
                    {
                        ds = objbs.SelectEmbroidingetGridView(lot);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                txt_rate.Text = Convert.ToDouble(ds.Tables[0].Rows[i]["Rate"]).ToString();
                                txt_date.Text = Convert.ToDateTime(ds.Tables[0].Rows[i]["date"]).ToString("dd/MM/yyyy");
                                txt_totq.Text = ds.Tables[0].Rows[i]["TotalQuantity"].ToString();
                            }
                        }
                    }
                }
            }


           // AddNewRow();
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
                TextBox txttransid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttransid");
                TextBox txtTotalQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttotqty");
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
                        TextBox txttransid =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txttransid");

                        DropDownList drpProcess =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txtRate");

                        TextBox txtTotalQty =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txttotqty");


                        TextBox txtRecQuantity =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txtRecQuantity");

                        DropDownList drpEmp =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpEmp");

                        TextBox date =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("date");

                        drCurrentRow = dtCurrentTable.NewRow();
                      //  drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Orderno"] = txttransid.Text;
                        dtCurrentTable.Rows[i - 1]["Process"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["TotalQuantity"] = txtTotalQty.Text;
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

                        TextBox txttransid =
                     (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txttransid");

                        DropDownList drpProcess =
                     (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtRate");

                        TextBox txtTotalQty =
                   (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txttotqty");

                        TextBox txtRecQuantity =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtRecQuantity");

                        DropDownList drpEmp =
                     (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpEmp");

                        TextBox date =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("date");

                        txttransid.Text = dt.Rows[i]["Orderno"].ToString();
                        drpProcess.SelectedValue = dt.Rows[i]["Process"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtTotalQty.Text = dt.Rows[i]["TotalQuantity"].ToString();
                        txtRecQuantity.Text = dt.Rows[i]["RecQuantity"].ToString();
                        drpEmp.SelectedValue = dt.Rows[i]["EmpName"].ToString();
                        date.Text = dt.Rows[i]["date"].ToString();

                        rowIndex++;

                    }
                }
            }
        }

        protected void UnitChange(object sender, EventArgs e)
        {
            //if (ddlUnit.SelectedValue == "Select Unit Name")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Unit Name')", true);
            //    return;

            //}
            //if (chckJobWork.Checked == true)
            //{
            //    DataSet dsLotNo = objbs.Select_LotnewEmbcurrentforjobwork(Convert.ToInt32(ddlUnit.SelectedValue));//tblCut
            //    if (dsLotNo.Tables[0].Rows.Count > 0)
            //    {
            //        ddlLotNo.DataSource = dsLotNo.Tables[0];
            //        ddlLotNo.DataTextField = "LotNo";
            //        ddlLotNo.DataValueField = "CutID";
            //        ddlLotNo.DataBind();
            //        ddlLotNo.Items.Insert(0, "Select Lot No");
            //    }
            //    else
            //    {
            //        ddlLotNo.ClearSelection();
            //        ddlLotNo.Items.Clear();
            //    }
            //}
            //else
            //{
            //    DataSet dsLotNo = objbs.Select_Lotnewembcurrent(Convert.ToInt32(ddlUnit.SelectedValue));//tblCut
            //    if (dsLotNo.Tables[0].Rows.Count > 0)
            //    {
            //        ddlLotNo.DataSource = dsLotNo.Tables[0];
            //        ddlLotNo.DataTextField = "LotNo";
            //        ddlLotNo.DataValueField = "CutID";
            //        ddlLotNo.DataBind();
            //        ddlLotNo.Items.Insert(0, "Select Lot No");
            //    }
            //    else
            //    {
            //        ddlLotNo.ClearSelection();
            //        ddlLotNo.Items.Clear();
            //    }
            //}
        }

        protected void Add_LotProcessDetails(object sender, EventArgs e)
        {
            string chk = "";
            if (ddlLotNo.SelectedValue == "Select Lot No")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot No.Thank You!!!.')", true);
                return;
            }

            if (txtreceivingdate.Text=="")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Receiving Date.Thank You!!!.')", true);
                return;
            }

          
            if(btnadd.Text=="Save")
            {



                int test = 0;
                test = Convert.ToInt32(txtarrivedQty.Text);

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    int total = 0;
                    DropDownList drpProcess =
                     (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                    if (drpProcess.SelectedValue != "Select Process Type")
                    {
                        //ds = objbs.CheckQuantityOverLoadkajaProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpProcess.SelectedValue));
                        string ProcessType = "";
                        for (int j = 0; j < gvcustomerorder.Rows.Count; j++)
                        {

                            DropDownList drpProcessCheck =
                             (DropDownList)gvcustomerorder.Rows[j].FindControl("drpProcess");
                            if (drpProcessCheck.SelectedValue != "Select Process Type")
                            {
                                if (drpProcess.SelectedItem.Text == drpProcessCheck.SelectedItem.Text)
                                {
                                    TextBox txtRecQuantity =
                                            (TextBox)gvcustomerorder.Rows[j].FindControl("txtRecQuantity");
                                    total = total + Convert.ToInt32(txtRecQuantity.Text);
                                }

                            }
                            //}
                            if (total > test)
                            {

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + drpProcess.SelectedItem.Text + " has enetered Over Quantity!!!.')", true);
                                return;
                            }

                        }
                    }

                }


            DataSet ds = new DataSet();
            ds = objbs.checkembroidalreadyexistornot(ddlLotNo.SelectedValue);

            int embid = 0;
            DateTime receivingdate = DateTime.ParseExact(txtreceivingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (ds.Tables[0].Rows.Count == 0)
            {
                if (chckJobWork.Checked == true)
                {
                    chk = "Out";
                     
                }
                else
                {
                    chk = "In";
                }
                int iStatus23 = objbs.insertembroProcess1(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToString(ddlLotNo.SelectedItem.Text), Convert.ToInt32(txtledgerid.Text),
                    Convert.ToInt32(txtbrandid.Text), Convert.ToInt32(txtUnitID.Text), Convert.ToInt32(txtTotalQantity.Text),chk,drpsupervisor.SelectedValue,Convert.ToString(lbldesignNo.Text),drpjobwork.SelectedValue);

                if (iStatus23==1)
                {
                    int isucc = objbs.updateembwork(Convert.ToInt32(embid), Convert.ToInt32(txtdcno.Text), receivingdate);
                }

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {

                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                    DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
                    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                   // date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (drpprocess.SelectedValue == "Select Process Type")
                    {
                    }
                    else
                    {
                        //ds = objbs.CheckQuantityOverLoadEmbroidingProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
                        //string ProcessType = "";
                        //int total = 0;
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    int test = Convert.ToInt32(ds.Tables[0].Rows[0]["RecQty"]);
                        //    ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
                        //    total = test + Convert.ToInt32(txtrecQty.Text);

                        //}

                        if (txtTotalQantity.Text != "")
                        {
                           
                            if (chckJobWork.Checked == true)
                            {
                                chk = "Out";

                            }
                            else
                            {
                                chk = "In";
                            }

                            int istasHistory = objbs.inserttransembroidProcessHistory1(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpjobwork.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                    recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(ddlLotNo.SelectedItem.Text),Convert.ToString(lbldesignNo.Text));

                            int istas = objbs.inserttransembroidProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpjobwork.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                    recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type  has enetered Over Quantity!!!.')", true);
                            return;
                        }

                    }
                }
            }
            else
            {

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {

                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                    DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
                    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                   // date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (drpprocess.SelectedValue == "Select Process Type")
                    {
                    }
                    else
                    {
                        //ds = objbs.CheckQuantityOverLoadEmbroidingProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
                        //string ProcessType = "";
                        //int total = 0;
                        //if (ds.Tables[0].Rows.Count > 0)
                        //{
                        //    int test = Convert.ToInt32(ds.Tables[0].Rows[0]["RecQty"]);
                        //    ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
                        //    total = test + Convert.ToInt32(txtrecQty.Text);

                        //}

                        if (txtTotalQantity.Text != "")
                        {
                          //  string chk = "";
                            if (chckJobWork.Checked == true)
                            {
                                chk = "Out";

                            }
                            else
                            {
                                chk = "In";
                            }

                            //int istasHistory = objbs.inserttransembroidProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                            //        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(ddlLotNo.SelectedItem.Text));

                            int istas = objbs.UpdatetransembroProcess1(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpjobwork.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                            recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(lbldesignNo.Text));

                            if (istas == 1)
                            {
                                int isucc = objbs.updateembwork(Convert.ToInt32(embid), Convert.ToInt32(txtdcno.Text), receivingdate);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type  has enetered Over Quantity!!!.')", true);
                            return;
                        }

                    }
                }
            }
        }


            else if(btnadd.Text=="Update")
            {
                DateTime receivingdate = DateTime.ParseExact(txtreceivingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                int Transid = 0;
                int ProcessTypeID=0;
                int EmpID=0;
                int RecQty=0;
                string receiveddate = "";
                decimal Rate=0;
                string lotno="0";
                int totalQty=0;
           

                 if (chckJobWork.Checked == true)
                {
                    chk = "Out";
                     
                }
                else
                {
                    chk = "In";
                }


                 if (ViewState["CurrentTable11"] != null)
                 {



                     DataSet ds = new DataSet();
                     DataTable dt = (DataTable)ViewState["CurrentTable11"];

                     for (int i = 0; i < dt.Rows.Count; i++)
                     {
                         if (dt.Rows[i]["Process"].ToString() != "")
                         {
                         Transid = 0;
                          ProcessTypeID = Convert.ToInt32(dt.Rows[i]["Process"].ToString());
                          EmpID = Convert.ToInt32(dt.Rows[i]["EmpName"].ToString());
                          RecQty = Convert.ToInt32(dt.Rows[i]["RecQuantity"].ToString());
                          receiveddate = dt.Rows[i]["date"].ToString();
                          DateTime recdate = DateTime.ParseExact(receiveddate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                          Rate = Convert.ToDecimal(dt.Rows[i]["Rate"].ToString());
                          totalQty = Convert.ToInt32(dt.Rows[i]["TotalQuantity"].ToString());


                          objbs.pre_Updation_Emb(Transid, ProcessTypeID, EmpID, RecQty, recdate, Rate, ddlLotNo.SelectedValue, totalQty, chk, lbldesignNo.Text);
                         }
                     }

                 }


               


             //   DateTime receivingdate = DateTime.ParseExact(txtreceivingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

               for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    TextBox txttransid = (TextBox)gvcustomerorder.Rows[i].FindControl("txttransid");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                    DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
                    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                  //  date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                   // DateTime recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (drpprocess.SelectedValue == "Select Process Type")
                    {
                    }
                    else
                    {
                            if (chckJobWork.Checked == true)
                            {
                                chk = "Out";

                            }
                            else
                            {
                                chk = "In";
                            }

                        //    int istasHistory = objbs.inserttransembroidProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                         //          recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(ddlLotNo.SelectedItem.Text));

                            int istas = objbs.UpdatetransembroProcess1(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpjobwork.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                            recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(lbldesignNo.Text));

                            if (istas == 1)
                            {
                                int isucc = objbs.updateembwork(0, Convert.ToInt32(txtdcno.Text), receivingdate);
                            }
                    }
                }
            }

            System.Threading.Thread.Sleep(3000);
            Response.Redirect("Dillo_Embrodarygrid.aspx");


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
                        gr.ForeColor = System.Drawing.Color.Green;
                    }
                    if (7 <= rateTotal && rateTotal <= 10)
                    {
                        gr.ForeColor = System.Drawing.Color.Gray;
                    }
                    if (11 <= rateTotal && rateTotal <= 15)
                    {
                        gr.ForeColor = System.Drawing.Color.Blue;
                    }
                    if (15 <= rateTotal && rateTotal <= 20)
                    {
                        gr.ForeColor = System.Drawing.Color.Pink;
                    }
                }
            }
        }

        protected void Change_JobWork(object sender, EventArgs e)
        {
            if (chckJobWork.Checked == true)
            {
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList ddEmp = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpEmp");

                    DataSet dsJobWork = objbs.selectJobWorkDet(6, 2);
                    ddEmp.DataSource = dsJobWork.Tables[0];
                    ddEmp.DataTextField = "LedgerName";
                    ddEmp.DataValueField = "LedgerID";
                    ddEmp.DataBind();
                    ddEmp.Items.Insert(0, "Select JobWork Name");
                }
            }
            else
            {
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    DropDownList ddEmp = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpEmp");

                    DataSet drpEmp = objbs.SelectEmpName();
                    ddEmp.DataSource = drpEmp;
                    ddEmp.DataTextField = "Name";
                    ddEmp.DataValueField = "Employee_Id";
                    ddEmp.DataBind();
                    ddEmp.Items.Insert(0, "Select Employee Name");
                }
            }
        }
    }
}