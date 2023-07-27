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
    public partial class Dillo_IronPacking : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        string lot;
        protected void Page_Load(object sender, EventArgs e)
        {
             lot = Request.QueryString.Get("lotid");
            if (!IsPostBack)
            {
                //DataSet dsLotNo = objbs.Select_Lotnewembroiding();//tblCut
                //if (dsLotNo.Tables[0].Rows.Count > 0)
                //{
                //    ddlLotNo.DataSource = dsLotNo.Tables[0];
                //    ddlLotNo.DataTextField = "LotNo";
                //    ddlLotNo.DataValueField = "CutID";
                //    ddlLotNo.DataBind();
                //    ddlLotNo.Items.Insert(0, "Select Lot No");
                //}

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
                    DataSet ds = objbs.SelectIrenetGridView(lot);
                    {
                        if(ds.Tables[0].Rows.Count>0)
                        {
                        //btnadd.Visible = false;
                        btnadd.Text = "Update";
                        ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["unitid"].ToString();

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
                            dr["date"] = ds.Tables[0].Rows[i]["date"].ToString();


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

                            //  dt.Rows.Add(dr);
                            temp.Tables[0].Rows.Add(dr);
                        }

                        ViewState["CurrentTable1"] = dtt;

                        gvcustomerorder.DataSource = temp;
                        gvcustomerorder.DataBind();

                        for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                        {
                            //Label lbltrans = (Label)gvcustomerorder.Rows[i].FindControl("lbltransno");

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


                            // lbltrans.Text = temp.Tables[0].Rows[i]["OrderNo"].ToString();
                            drpprocess.SelectedValue = temp.Tables[0].Rows[i]["Process"].ToString();
                            txtrate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();
                            txtRecQuantity.Text = temp.Tables[0].Rows[i]["RecQuantity"].ToString();
                            drpEmp.SelectedValue = temp.Tables[0].Rows[i]["EmpName"].ToString();
                            date.Text = Convert.ToDateTime(temp.Tables[0].Rows[i]["date"]).ToString("dd/MM/yyyy");

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




                        ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["unitid"].ToString();

                      //  gvcustomerorder.Enabled = false;
                        ddlLotNo.Enabled = false;
                        UnitChange(sender, e);
                        DataSet dsLotNo = objbs.Select_Lotnewironcurrent_forUpdate(Convert.ToInt32(ddlUnit.SelectedValue));//tblCut

                        if (dsLotNo.Tables[0].Rows.Count > 0)
                        {
                            ddlLotNo.DataSource = dsLotNo.Tables[0];
                            ddlLotNo.DataTextField = "LotNo";
                            ddlLotNo.DataValueField = "CutID";
                            ddlLotNo.DataBind();
                            ddlLotNo.Items.Insert(0, "Select Lot No");

                            ddlLotNo.Focus();
                        }
                        ddlLotNo.SelectedValue =ds.Tables[0].Rows[0]["cutid"].ToString();
                        StitchingInfo_Load(sender, e);
                    }
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


            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {

               // DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
               // TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
               // TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
              //  TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                drpEmp.Focus();
            }

        }

        protected void StitchingInfo_Load(object sender, EventArgs e)
        {


            //DataSet dss = new DataSet();
            //dss = objbs.checkcurrentststus(ddlLotNo.SelectedValue);
            //if (dss.Tables[0].Rows.Count > 0)
            //{

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check This Lot no.This Lot is in Stiching process.Thank You!!!')", true);
            //    return;
            //}

            DataSet dcheck = objbs.checkcurrentforalliron(ddlUnit.SelectedValue, ddlLotNo.SelectedValue, "Iron");
             //if (dcheck.Tables[0].Rows.Count > 0)
             //{
             //    int iron =Convert.ToInt32(dcheck.Tables[0].Rows[0]["cnt"]);

             //    if (iron == 0)
             //    {
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
                         txtdesign.Text = dataSet.Tables[0].Rows[0]["DesignNo"].ToString();

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
                         darrived = objbs.SelectProcessTypeironforarrivedProcess(ddlLotNo.SelectedValue);
                         if (darrived.Tables[0].Rows.Count > 0)
                         {
                             txtarrivedQty.Text = darrived.Tables[0].Rows[0]["Qty"].ToString();
                         }

                         DataSet dqty = objbs.getcurreetnqty(ddlLotNo.SelectedValue);
                         if (dqty.Tables[0].Rows.Count > 0)
                         {
                             txtarrivedQty.Text = dqty.Tables[0].Rows[0]["Ironqty"].ToString();
                         }
                         else
                         {

                         }
                         DataSet drpProcess = objbs.SelectProcessTypeLotProcessIron(Convert.ToInt32(ddlLotNo.SelectedValue));
                         GridView2.DataSource = drpProcess;
                         GridView2.DataBind();


                         DataSet dlotprocess = new DataSet();
                         DataSet dssendqty = new DataSet();

                         dlotprocess = objbs.Get_Processdetails(ddlLotNo.SelectedValue, 6);   //6-Ironing


                         if (dlotprocess.Tables[0].Rows.Count > 0)
                         {
                            
                             GridView1.DataSource = dlotprocess;
                             GridView1.DataBind();

                             txtfinishedQty.Text = dlotprocess.Tables[0].Rows[0]["FinishedQty"].ToString();

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


                         if (txtfinishedQty.Text != "")
                         {
                             txtnewfinishedqty.Text = Convert.ToString(Convert.ToInt32(txtfinishedQty.Text) - Convert.ToInt32(txtOldrecQty.Text) + Convert.ToInt32(txtcheckingbalQty.Text));
                         }
                     }
                     else
                     {
                         gvcustomerorder.DataSource = null;
                         gvcustomerorder.DataBind();
                     }
             //    }
             //    else
             //    {
             //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check This Lot no.This Lot is Not Ready to Ironing Process.Thank You!!!')", true);
             //        return;
             //    }
             //}
             //else
             //{
             //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check This Lot no.This Lot is in Another process.Thank You!!!')", true);
             //    return;
             //}
             ddlLotNo.Focus();

             if (txtarrivedQty.Text == "0")
             {
                 btnadd.Visible = false;
                 gvcustomerorder.Enabled = false;
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
            DataSet drpProcess = objbs.SelectProcessTypeironProcess();
            //DataSet drpEmp = new DataSet();
            //if (txtUnitID.Text == "")
            //{
            //    drpEmp = objbs.SelectEmpName();
            //}
            //else
            //{
            //    drpEmp = objbs.SelectIronEmpName(Convert.ToInt32(txtUnitID.Text));
            //}

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
                //if (chckJobWork.Checked == true)
                //{
                //    DataSet dsJobWork = objbs.selectJobWorkDet(6, 2);
                //    ddEmp.DataSource = dsJobWork.Tables[0];
                //    ddEmp.DataTextField = "LedgerName";
                //    ddEmp.DataValueField = "LedgerID";
                //    ddEmp.DataBind();
                //    ddEmp.Items.Insert(0, "Select JobWork Name");
                //}
                //else
                {
                    DataSet dsironandpacking = objbs.selectIronPacking();
                    ddEmp.DataSource = dsironandpacking;
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
            drNew["date"] = DateTime.Now.ToString("dd/MM/yyyy");

            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();

        }

        protected void txtRange_Change(object sender, EventArgs e)
        {

            if (btnadd.Text == "Save")
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
                int test = 0;
                test = Convert.ToInt32(txtTotalQantity.Text);

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

       //     ButtonAdd1_Click(sender, e);
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    int cnt = gvcustomerorder.Rows.Count;
            //   // TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
            //    if (vLoop >= 1)
            //    {
            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("date");
            //        //    oldtxttk.Text = ".00";
            //        oldtxttk.Focus();
            //    }
            //    int tot = cnt - vLoop;
            //    if (tot == 1)
            //    {
            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("date");
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
               // DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            //}
          

        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
          //  AddNewRow();


            int No = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {


                DropDownList ddl_process = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");

                if (ddl_process.SelectedValue != "Select Process")
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

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{

            //    TextBox txt_recQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtQuantity");
            //    txt_recQty.Focus();
            //}

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{

            //    TextBox txt_rate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txt_recQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtQuantity");
            //    TextBox txt_date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
            //    TextBox txt_totq = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttotqty");
            //    TextBox txt_transid = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txttransid");
            //    DropDownList ddl_process = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");

            //    if (txt_totq.Text != "")
            //    {
            //    }
            //    else
            //    {
            //        DataSet ds = new DataSet();
            //        if (ddl_process.SelectedValue != "Select Process Type")
            //        {
            //            ds = objbs.SelectEmbroidingetGridView(lot);
            //            if (ds.Tables[0].Rows.Count > 0)
            //            {
            //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //                {
            //                    txt_rate.Text = Convert.ToDouble(ds.Tables[0].Rows[i]["Rate"]).ToString();
            //                    txt_date.Text = ds.Tables[0].Rows[i]["date"].ToString();
            //                    txt_totq.Text = ds.Tables[0].Rows[i]["TotalQuantity"].ToString();
            //                }
            //            }
            //        }
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
                        if (date.Text == "--Select Date--" || date.Text == "")
                        {
                            dtCurrentTable.Rows[i - 1]["date"] = DateTime.Now.ToString("dd/MM/yyyy");
                        }
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

                        drpProcess.SelectedValue = dt.Rows[i]["Process"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtRecQuantity.Text = dt.Rows[i]["RecQuantity"].ToString();
                        drpEmp.SelectedValue = dt.Rows[i]["EmpName"].ToString();
                        date.Text = dt.Rows[i]["date"].ToString();

                        rowIndex++;

                    }
                }
            }
        }

        protected void Add_LotProcessDetails(object sender, EventArgs e)
        {
            DateTime recdate;
            if (ddlLotNo.SelectedValue == "Select Lot No")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot No.Thank You!!!.')", true);
                return;
            }
            int test = 0;

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                
                test = Convert.ToInt32(txtarrivedQty.Text);
                if (btnadd.Text == "Save")
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
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + drpProcess.SelectedItem.Text + " has enetered Over Quantity!!!.')", true);
                                return;
                            }
                        }
                    }
                }
                else
                {
                   // int test = 0;
                    test = Convert.ToInt32(txtTotalQantity.Text);
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
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type " + drpProcess.SelectedItem.Text + " has enetered Over Quantity!!!.')", true);
                                return;
                            }
                        }
                    }
                }

            }
            if (btnadd.Text == "Save")
            {

                int i36FS = 0, i36HS = 0, i38FS = 0, i38HS = 0, i39FS = 0, i39HS = 0, i40FS = 0, i40HS = 0, i42FS = 0, i42HS = 0, i44FS = 0, i44HS = 0;

                DataSet ds = new DataSet();
                ds = objbs.checkIrenreadyexistornot(ddlLotNo.SelectedValue);
                if (ds.Tables[0].Rows.Count == 0)
                {

                    int iStatus23 = objbs.insertironProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToString(ddlLotNo.SelectedItem.Text), Convert.ToInt32(txtledgerid.Text),
                   Convert.ToInt32(txtbrandid.Text), Convert.ToInt32(txtUnitID.Text), Convert.ToInt32(txtTotalQantity.Text), txtdesign.Text);

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


                        // date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        if (date.Text == "--Select Date--" || date.Text == "")
                        {
                            date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }

                        if (drpprocess.SelectedValue == "Select Process Type")
                        {
                        }
                        else
                        {
                            //ds = objbs.CheckQuantityOverLoadIronProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
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



                                i36FS = Convert.ToInt32(txt_36FS.Text); i36HS = Convert.ToInt32(txt_36HS.Text);
                                i38FS = Convert.ToInt32(txt_38FS.Text); i38HS = Convert.ToInt32(txt_38HS.Text);
                                i39FS = Convert.ToInt32(txt_39FS.Text); i39HS = Convert.ToInt32(txt_39HS.Text);
                                i40FS = Convert.ToInt32(txt_40FS.Text); i40HS = Convert.ToInt32(txt_40HS.Text);
                                i42FS = Convert.ToInt32(txt_42FS.Text); i42HS = Convert.ToInt32(txt_42HS.Text);
                                i44FS = Convert.ToInt32(txt_44FS.Text); i44HS = Convert.ToInt32(txt_44HS.Text);


                                string chk = "";
                                if (chckJobWork.Checked == true)
                                {
                                    chk = "Out";

                                }
                                else
                                {
                                    chk = "In";
                                }

                                DataSet dcheck = objbs.getprocesstype(drpprocess.SelectedValue);

                                if (dcheck.Tables[0].Rows.Count > 0)
                                {
                                    int typeid = Convert.ToInt32(dcheck.Tables[0].Rows[0]["Typeid"]);
                                    if (typeid == 0)
                                    {
                                        int istasHistory = objbs.inserttransironProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(ddlLotNo.SelectedItem.Text), i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);

                                        int istas = objbs.inserttransironProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                                    }
                                    else if (typeid == 1)
                                    {
                                        int istasHistory = objbs.inserttransironProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(ddlLotNo.SelectedItem.Text), i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);

                                        int istas = objbs.inserttransironProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);
                                    }
                                    else if (typeid == 2)
                                    {
                                        int istasHistory = objbs.inserttransironProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(ddlLotNo.SelectedItem.Text), 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);

                                        int istas = objbs.inserttransironProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);
                                    }



                                    //Ironing Process Insert
                                    int res1 = objbs.Insert_CheckingStatusDetails(ddlLotNo.SelectedValue, Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(txtTotalQantity.Text), 0, 0,0, 0, 0, "N", ddlLotNo.SelectedItem.Text);

                                    //Updating Trimming Process
                                         int checkbalqty = Convert.ToInt32(txtfinishedQty.Text) - Convert.ToInt32(txtrecQty.Text);
                                        int res = objbs.Update_CheckingStatus(ddlLotNo.SelectedValue, Convert.ToInt32(txtfinishedQty.Text), Convert.ToInt32(txtrecQty.Text), checkbalqty, Convert.ToInt32(txtfinishedQty.Text), 15);
                                    

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
                else
                {

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


                        //  date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        if (date.Text == "--Select Date--" || date.Text == "")
                        {
                            date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }

                        if (drpprocess.SelectedValue == "Select Process Type")
                        {
                        }
                        else
                        {


                            //ds = objbs.CheckQuantityOverLoadIronProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
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

                                i36FS = Convert.ToInt32(txt_36FS.Text); i36HS = Convert.ToInt32(txt_36HS.Text);
                                i38FS = Convert.ToInt32(txt_38FS.Text); i38HS = Convert.ToInt32(txt_38HS.Text);
                                i39FS = Convert.ToInt32(txt_39FS.Text); i39HS = Convert.ToInt32(txt_39HS.Text);
                                i40FS = Convert.ToInt32(txt_40FS.Text); i40HS = Convert.ToInt32(txt_40HS.Text);
                                i42FS = Convert.ToInt32(txt_42FS.Text); i42HS = Convert.ToInt32(txt_42HS.Text);
                                i44FS = Convert.ToInt32(txt_44FS.Text); i44HS = Convert.ToInt32(txt_44HS.Text);

                                string chk = "";
                                if (chckJobWork.Checked == true)
                                {
                                    chk = "Out";

                                }
                                else
                                {
                                    chk = "In";
                                }

                                //int istasHistory = objbs.inserttransironProcessHistory(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                //    recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(ddlLotNo.SelectedItem.Text));
                                 DataSet dcheck = objbs.getprocesstype(drpprocess.SelectedValue);

                                if (dcheck.Tables[0].Rows.Count > 0)
                                {
                                    int typeid = Convert.ToInt32(dcheck.Tables[0].Rows[0]["Typeid"]);
                                    if (typeid == 0)
                                    {
                                        int istas = objbs.UpdatetransironProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                                    }
                                    else if (typeid==1)
                                    {
                                        int istas = objbs.UpdatetransironProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);
                                    }
                                    else if(typeid==2)
                                    {
                                        int istas = objbs.UpdatetransironProcess(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                        recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);
                                    }


                                    //Updating Iroining Process

                                    int res1 = objbs.Update_CheckingStatusDetails(ddlLotNo.SelectedValue, Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(txtTotalQantity.Text), 0, 0, 0, 0, 0, "N", ddlLotNo.SelectedItem.Text);

                                    //Updating Trimming Process

                                        int checkbalqty = Convert.ToInt32(txtnewfinishedqty.Text) - Convert.ToInt32(txtrecQty.Text);
                                        int res = objbs.Update_CheckingStatus(ddlLotNo.SelectedValue, Convert.ToInt32(txtnewfinishedqty.Text), Convert.ToInt32(txtrecQty.Text), checkbalqty, Convert.ToInt32(txtfinishedQty.Text), 15);



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
        //        {


                  

        //            int iStatus23 = objbs.Update_IronProcess(Convert.ToInt32(lot), Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToString(ddlLotNo.SelectedItem.Text), Convert.ToInt32(txtledgerid.Text),
        //Convert.ToInt32(txtbrandid.Text), Convert.ToInt32(txtUnitID.Text), Convert.ToInt32(txtTotalQantity.Text), txtdesign.Text);


        //            objbs.Delete_transironProcessHistory(Convert.ToInt32(lot));

        //            objbs.Delete_transironProcess(Convert.ToInt32(lot));

        //            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
        //            {

        //                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
        //                DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpEmp");
        //                TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
        //                TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
        //                TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

        //                //  date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //                if (date.Text == "--Select Date--" || date.Text == "")
        //                {
        //                    date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        //                    recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                }
        //                else
        //                {
        //                    //  recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //                    recdate = DateTime.Parse(Convert.ToDateTime(date.Text.Trim()).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
        //                }

        //                if (drpprocess.SelectedValue == "Select Process Type")
        //                {
        //                }
        //                else
        //                {
        //                    //ds = objbs.CheckQuantityOverLoadIronProcess(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToInt32(drpprocess.SelectedValue));
        //                    //string ProcessType = "";
        //                    //int total = 0;
        //                    //if (ds.Tables[0].Rows.Count > 0)
        //                    //{
        //                    //    int test = Convert.ToInt32(ds.Tables[0].Rows[0]["RecQty"]);
        //                    //    ProcessType = ds.Tables[0].Rows[0]["ProcessType"].ToString();
        //                    //    total = test + Convert.ToInt32(txtrecQty.Text);

        //                    //}

        //                    if (txtTotalQantity.Text != "")
        //                    {
        //                        string chk = "";
        //                        if (chckJobWork.Checked == true)
        //                        {
        //                            chk = "Out";

        //                        }
        //                        else
        //                        {
        //                            chk = "In";
        //                        }

        //                        int istasHistory = objbs.update_ironProcessHistory(Convert.ToInt32(lot), Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
        //                           recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk, Convert.ToString(ddlLotNo.SelectedItem.Text));

        //                        int istas = objbs.Update_transironProcess(Convert.ToInt32(lot), Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
        //                            recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), chk);
        //                    }
        //                    else
        //                    {
        //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Selected type  has enetered Over Quantity!!!.')", true);
        //                        return;
        //                    }
        //                }
        //            }
        //        }
            }
            else
            {
                int i36FS = 0, i36HS = 0, i38FS = 0, i38HS = 0, i39FS = 0, i39HS = 0, i40FS = 0, i40HS = 0, i42FS = 0, i42HS = 0, i44FS = 0, i44HS = 0;

                int iscc = objbs.deleteironingprocessneww(ddlLotNo.SelectedValue);

                string chk = "";
                if (chckJobWork.Checked == true)
                {
                    chk = "Out";

                }
                else
                {
                    chk = "In";
                }
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



                    //   date.Text = DateTime.Now.ToString("dd/MM/yyyy");




                    if (drpprocess.SelectedValue == "Select Process Type")
                    {
                    }
                    else
                    {

                        if (date.Text == "--Select Date--" || date.Text == "")
                        {
                            date.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }
                        else
                        {
                            recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        }


                        i36FS = Convert.ToInt32(txt_36FS.Text); i36HS = Convert.ToInt32(txt_36HS.Text);
                        i38FS = Convert.ToInt32(txt_38FS.Text); i38HS = Convert.ToInt32(txt_38HS.Text);
                        i39FS = Convert.ToInt32(txt_39FS.Text); i39HS = Convert.ToInt32(txt_39HS.Text);
                        i40FS = Convert.ToInt32(txt_40FS.Text); i40HS = Convert.ToInt32(txt_40HS.Text);
                        i42FS = Convert.ToInt32(txt_42FS.Text); i42HS = Convert.ToInt32(txt_42HS.Text);
                        i44FS = Convert.ToInt32(txt_44FS.Text); i44HS = Convert.ToInt32(txt_44HS.Text);

                       //  recdate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                         DataSet dcheck = objbs.getprocesstype(drpprocess.SelectedValue);

                         if (dcheck.Tables[0].Rows.Count > 0)
                         {
                             int typeid = Convert.ToInt32(dcheck.Tables[0].Rows[0]["Typeid"]);
                             if (typeid == 0)
                             {
                                 int istasInsertHistory = objbs.updatesironandpackprocessneww(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                                recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), chk, i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                             }
                             else if (typeid == 1)
                             {
                                 int istasInsertHistory = objbs.updatesironandpackprocessneww(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                               recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), chk, i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);
                             }
                             else if (typeid == 2)
                             {
                                 int istasInsertHistory = objbs.updatesironandpackprocessneww(Convert.ToInt32(drpprocess.SelectedValue), Convert.ToInt32(drpEmp.SelectedValue), Convert.ToInt32(txtrecQty.Text),
                               recdate, Convert.ToDecimal(txtrate.Text), ddlLotNo.SelectedValue, Convert.ToInt32(txtTotalQantity.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), chk, 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);
                             }
                         }
                    }
                }
                DataSet ds = objbs.gettranshistoryforupdateeironandpack(ddlLotNo.SelectedValue);
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int ii = 0; ii < ds.Tables[0].Rows.Count; ii++)
                        {

                            string lotprocessid = ds.Tables[0].Rows[ii]["IronandpackProcessID"].ToString();
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



                            int istas = objbs.UpdatetransironandpackProcessnewprocess(Convert.ToInt32(processid), Convert.ToInt32(empid), Convert.ToInt32(RecQty),
                            newdate, Convert.ToDecimal(rate), ddlLotNo.SelectedValue, Convert.ToInt32(totalqty), v36FS, v36HS, v38FS, v38HS, v39FS, v39HS, v40FS, v40HS, v42FS, v42HS, v44FS, v44HS);
                        }

                        DataSet dn = objbs.gettransforupdateeironandpackk(ddlLotNo.SelectedValue);
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

                                int iss = objbs.updatereceiveqtyironandpack(ddlLotNo.SelectedValue, TOtProcid, v36FS, v36HS, v38FS, v38HS, v39FS, v39HS, v40FS, v40HS, v42FS, v42HS, v44FS, v44HS);
                            }



                            for (int j = 0; j < dn.Tables[0].Rows.Count; j++)
                            {
                                
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

                                string Procid = dn.Tables[0].Rows[j]["Processtypeid"].ToString();
                                string recqty = dn.Tables[0].Rows[j]["RecQty"].ToString();

                                int ii = objbs.updatetransironandpackdetails(ddlLotNo.SelectedValue, Procid, recqty, v36FS, v36HS, v38FS, v38HS, v39FS, v39HS, v40FS, v40HS, v42FS, v42HS, v44FS, v44HS);


                            }
                        }

                    }

                }

               // int isss = objbs.update_Iron_LotProcess(Convert.ToInt32(lot));
            }

            System.Threading.Thread.Sleep(3000);
            Response.Redirect("Dillo_Irongrid.aspx");


        }

        protected void UnitChange(object sender, EventArgs e)
        {
            DataSet dmerge = new DataSet();

            if (ddlUnit.SelectedValue == "Select Unit Name")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Unit Name')", true);
                return;

            }

            DataSet dsLotNo = objbs.Select_Lotnewironcurrent(Convert.ToInt32(ddlUnit.SelectedValue));//tblCut


        //    DataSet dsLotNo = objbs.Select_LotNo_ForProcess(8);    // 8-Ironning and packing

            if (dsLotNo.Tables[0].Rows.Count > 0)
            {
                ddlLotNo.DataSource = dsLotNo.Tables[0];
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
                ddlUnit.Focus();
            }


            //DataSet dss = new DataSet();
            //if (dsLotNo.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < dsLotNo.Tables[0].Rows.Count; i++)
            //    {
            //        string cutid = dsLotNo.Tables[0].Rows[i]["Cutid"].ToString();
            //        dss = objbs.checkcurrentststus(cutid);
            //        if (dss.Tables[0].Rows.Count > 0)
            //        {
            //            DataSet dcheck = objbs.checkcurrentforalliron(ddlUnit.SelectedValue, cutid, "Iron");
            //            if (dcheck.Tables[0].Rows.Count > 0)
            //            {
            //                int iron = Convert.ToInt32(dcheck.Tables[0].Rows[0]["cnt"]);

            //                if (iron == 0)
            //                {

            //                }
            //                else
            //                {
            //                    ddlLotNo.Items.Remove(ddlLotNo.Items.FindByValue(cutid));
            //                }
            //            }
            //        }
            //        else
            //        {
            //            ddlLotNo.Items.Remove(ddlLotNo.Items.FindByValue(cutid));
            //        }
            //    }
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


        protected void txt36FS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_38FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt38FS");
            txt_38FS.Focus();

        }

        protected void txt38FS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_39FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt39FS");
            txt_39FS.Focus();

        }


        protected void txt39FS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_40FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt40FS");
            txt_40FS.Focus();
        }

        protected void txt40FS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_42FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt42FS");
            txt_42FS.Focus();

        }


        protected void txt42FS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_44FS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt44FS");
            txt_44FS.Focus();

        }

        protected void txt44FS_TextChanged(object sender, EventArgs e)
        {


            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_36HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt36HS");
            txt_36HS.Focus();
        }


        protected void txt36HS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_38HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt38HS");
            txt_38HS.Focus();

        }


        protected void txt38HS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_39HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt39HS");
            txt_39HS.Focus();

        }


        protected void txt39HS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_40HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt40HS");
            txt_40HS.Focus();
        }


        protected void txt40HS_TextChanged(object sender, EventArgs e)
        {
            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_42HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt42HS");
            txt_42HS.Focus();

        }


        protected void txt42HS_TextChanged(object sender, EventArgs e)
        {

            Total_receivedQty(sender);
            txtRange_Change(sender, e);

            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_44HS = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("txt44HS");
            txt_44HS.Focus();
        }



        protected void txt44HS_TextChanged(object sender, EventArgs e)
        {

            Total_receivedQty(sender);
            txtRange_Change(sender, e);


            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_Date = (TextBox)gvcustomerorder.Rows[rowindex].Cells[1].FindControl("date");
            txt_Date.Focus();

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



    }
}