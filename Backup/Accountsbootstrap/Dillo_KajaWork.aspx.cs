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
    public partial class Dillo_KajaWork : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        string lot="";
        protected void Page_Load(object sender, EventArgs e)
        {

            int totQty = 0;
            int Kfull = 0;
            int Khalf = 0;

             lot = Request.QueryString.Get("lotid");
            if (!IsPostBack)
            {




                gvcustomerorder.DataSource = null;
                gvcustomerorder.DataBind();
                //DataSet dsUnitName = objbs.Select_UnitFirst();//tblUnit
                //if (dsUnitName.Tables[0].Rows.Count > 0)
                //{
                //    chkunit.DataSource = dsUnitName.Tables[0];
                //    chkunit.DataTextField = "UnitName";
                //    chkunit.DataValueField = "UnitID";
                //    chkunit.DataBind();
                    
                //}

                DataSet dsLotNo = objbs.getlotnoforcheckbox("kaja");//tblCut
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
                       // dss = objbs.checkcurrentststus(cutid);

                        dss = objbs.CheckCurrentStstus_Checking(cutid);

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

                   
                    DataSet ds = objbs.SelectKajaWork_View(lot);
                    {
                        if(ds.Tables[0].Rows.Count>0)
                        {
                        btnadd.Text="Update";
                        // ddlUnit.SelectedValue = ds.Tables[0].Rows[0]["unitid"].ToString();

                        DataSet temp = new DataSet();
                        DataTable dtt = new DataTable();

                        dtt.Columns.Add(new DataColumn("Cutid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Lotno", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Unitid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Unit", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Brandid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Brand", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Qty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Process", typeof(string)));
                        dtt.Columns.Add(new DataColumn("DesignNo", typeof(string)));

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

                            dr["Cutid"] = ds.Tables[0].Rows[i]["Cutid"].ToString();
                            dr["Lotno"] = ds.Tables[0].Rows[i]["Cutid"].ToString();
                            dr["Unitid"] = ds.Tables[0].Rows[i]["unitid"].ToString();
                            dr["Unit"] = ds.Tables[0].Rows[i]["UnitName"].ToString();
                            dr["Brandid"] = ds.Tables[0].Rows[i]["BrandId"].ToString();
                            dr["Brand"] = ds.Tables[0].Rows[i]["BrandName"].ToString();
                            dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                            dr["Qty"] = ds.Tables[0].Rows[i]["TotalQty"].ToString();
                            dr["Process"] = ds.Tables[0].Rows[i]["Processid"].ToString();
                            dr["DesignNo"] = ds.Tables[0].Rows[i]["DesignNo"].ToString();



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



                            TextBox txt_cutid = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcutid");
                            TextBox txt_lotno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtlotno");
                            TextBox txt_unitid = (TextBox)gvcustomerorder.Rows[i].FindControl("txtunitid");
                            TextBox txt_unitname = (TextBox)gvcustomerorder.Rows[i].FindControl("txtunitname");
                            TextBox txt_process = (TextBox)gvcustomerorder.Rows[i].FindControl("txtprocess");
                            TextBox txt_brandid = (TextBox)gvcustomerorder.Rows[i].FindControl("txtbrandid");
                            TextBox txt_brandname = (TextBox)gvcustomerorder.Rows[i].FindControl("txtbrandname");
                            TextBox txt_qty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtqty");
                            TextBox txt_rate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                            TextBox txt_design = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesign");

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


                            txt_cutid.Text = temp.Tables[0].Rows[i]["Cutid"].ToString();
                            txt_lotno.Text = temp.Tables[0].Rows[i]["Lotno"].ToString();
                            txt_unitid.Text = temp.Tables[0].Rows[i]["Unitid"].ToString();
                            txt_unitname.Text = temp.Tables[0].Rows[i]["Unit"].ToString();
                            txt_process.Text = temp.Tables[0].Rows[i]["Process"].ToString();
                            txt_brandid.Text = temp.Tables[0].Rows[i]["Brandid"].ToString();
                            txt_brandname.Text = temp.Tables[0].Rows[i]["Brand"].ToString();
                            txt_qty.Text = temp.Tables[0].Rows[i]["Qty"].ToString();
                            txt_rate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();
                            txt_design.Text = temp.Tables[0].Rows[i]["DesignNo"].ToString();



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


                            totQty = totQty + Convert.ToInt32(temp.Tables[0].Rows[i]["Qty"]);
                      

                            //if (Convert.ToInt32(ds.Tables[0].Rows[i]["TotalQty"]) == Convert.ToInt32(temp.Tables[0].Rows[i]["FullQty"]))
                            //{
                            if (temp.Tables[0].Rows[i]["Process"].ToString() == "35")
                                {

                                    Kfull = Kfull + Convert.ToInt32(temp.Tables[0].Rows[i]["Qty"]);
                                }
                            //}
                            //if (Convert.ToInt32(ds.Tables[0].Rows[i]["TotalQty"]) == Convert.ToInt32(temp.Tables[0].Rows[i]["HalfQty"]))
                            //{
                            if (temp.Tables[0].Rows[i]["Process"].ToString() == "36")
                                {
                                    Khalf = Khalf + Convert.ToInt32(temp.Tables[0].Rows[i]["Qty"]);
                                }
                            //}



                        }


                        txtTotalQantity.Text = totQty.ToString();
                        txtfull.Text = Kfull.ToString();
                        txtHalf.Text = Khalf.ToString();



                        txtdcno.Text = ds.Tables[0].Rows[0]["DCno"].ToString();
                        drpjobwork.SelectedValue = ds.Tables[0].Rows[0]["Empid"].ToString();
                        drpsupervisor.SelectedValue = ds.Tables[0].Rows[0]["Supervisor"].ToString();
                        txtsendingdate.Text = ds.Tables[0].Rows[0]["SendingDate"].ToString();
                       // txtTotalQantity.Text = ds.Tables[0].Rows[0]["Tqty"].ToString();
                        txtdcdate.Text = ds.Tables[0].Rows[0]["DcDate"].ToString();

                     //   gvcustomerorder.Enabled = false;
                        //  ddlLotNo.Enabled = false;
                        // UnitChange(sender, e);
                        //  ddlLotNo.SelectedValue = ds.Tables[0].Rows[0]["cutid"].ToString();
                    // StitchingInfo_Load(sender, e);

                       //// DateTime deliverydate = DateTime.ParseExact(txtdcdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                       // DateTime deliverydate = DateTime.Parse(Convert.ToDateTime(txtdcdate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));

                       // //DateTime sendingdate = DateTime.ParseExact(txtsendingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                       // DateTime sendingdate = DateTime.Parse(Convert.ToDateTime(txtsendingdate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));


                       //// DateTime receivingdate = DateTime.ParseExact(txtsendingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                       // DateTime receivingdate = DateTime.Parse(Convert.ToDateTime(txtsendingdate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));


                        chkLotNo.Items.Clear();
                        //  DataSet dsLotNo = objbs.getlotnoforcheckbox("kaja");//tblCut
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
                }
                else
                {
                    DataSet dmaxbill = new DataSet();
                    dmaxbill = objbs.getmaxbillkajawork();
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
        protected void chkunit_changed(object sender, EventArgs e)
        {
            DataSet dmerge = new DataSet();

            if (chkunit.SelectedIndex >= 0)
            {
                foreach (ListItem item in chkunit.Items)
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
                            DataSet dsLotNo = objbs.getlotnoforcheckbox("kaja");//tblCut
                            if (dsLotNo.Tables[0].Rows.Count > 0)
                            {
                                chkLotNo.DataSource = dsLotNo.Tables[0];
                                chkLotNo.DataTextField = "LotNo";
                                chkLotNo.DataValueField = "CutID";
                                chkLotNo.DataBind();
                            }
                        }
                    }
                }
            }
        }

        protected void StitchingInfo_Load(object sender, EventArgs e)
        {
            DataSet dssmer = new DataSet();
            DataSet dteo = new DataSet();
            string cond = "";
            string cond1 = "";
            int tot = 0;
            int ghalf = 0;
            int Gfull = 0;
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
            dt.Columns.Add("Process");
            dt.Columns.Add("DesignNo");


            dt.Columns.Add("36FS");
            dt.Columns.Add("36HS");
            dt.Columns.Add("38FS");
            dt.Columns.Add("38HS");
            dt.Columns.Add("39FS");
            dt.Columns.Add("39HS");
            dt.Columns.Add("40FS");
            dt.Columns.Add("40HS");
            dt.Columns.Add("42FS");
            dt.Columns.Add("42HS");
            dt.Columns.Add("44FS");
            dt.Columns.Add("44HS");


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
                            DataSet getvalue = objbs.getLotNoTransDetailsNewforkajajobworkk(Convert.ToInt32(item.Value));
                            if (getvalue.Tables[0].Rows.Count > 0)
                            {
                                DataTable dCrt = new DataTable();
                                dCrt = (DataTable)ViewState["Data"];

                               
                              //  DataRow dr = dCrt.NewRow();
                                DataRow drr = dCrt.NewRow();

                                for (int i = 0; i < getvalue.Tables[0].Rows.Count; i++)
                                {
                                    DataRow dr = dCrt.NewRow();
                                    dr["Cutid"] = item.Value;
                                    dr["Lotno"] = item.Text;
                                    dr["Unitid"] = getvalue.Tables[0].Rows[i]["UnitID"].ToString();
                                    dr["Unit"] = getvalue.Tables[0].Rows[i]["UnitName"].ToString();
                                    dr["Brandid"] = getvalue.Tables[0].Rows[i]["BrandID"].ToString();
                                    dr["Brand"] = getvalue.Tables[0].Rows[i]["BrandName"].ToString();
                                    dr["Rate"] = getvalue.Tables[0].Rows[i]["Rate"].ToString();
                                    dr["Qty"] = getvalue.Tables[0].Rows[i]["TotalQty"].ToString();
                                    dr["Process"] = getvalue.Tables[0].Rows[i]["processtypeid"].ToString();
                                    dr["DesignNo"] = getvalue.Tables[0].Rows[i]["Designno"].ToString();

                                    dr["36FS"] = getvalue.Tables[0].Rows[i]["36FS"].ToString();
                                    dr["36HS"] = getvalue.Tables[0].Rows[i]["36HS"].ToString();
                                    dr["38FS"] = getvalue.Tables[0].Rows[i]["38FS"].ToString();
                                    dr["38HS"] = getvalue.Tables[0].Rows[i]["38HS"].ToString();
                                    dr["39FS"] = getvalue.Tables[0].Rows[i]["39FS"].ToString();
                                    dr["39HS"] = getvalue.Tables[0].Rows[i]["39HS"].ToString();
                                    dr["40FS"] = getvalue.Tables[0].Rows[i]["40FS"].ToString();
                                    dr["40HS"] = getvalue.Tables[0].Rows[i]["40HS"].ToString();
                                    dr["42FS"] = getvalue.Tables[0].Rows[i]["42FS"].ToString();
                                    dr["42HS"] = getvalue.Tables[0].Rows[i]["42HS"].ToString();
                                    dr["44FS"] = getvalue.Tables[0].Rows[i]["44FS"].ToString();
                                    dr["44HS"] = getvalue.Tables[0].Rows[i]["44HS"].ToString();

                                    tot = tot + Convert.ToInt32(getvalue.Tables[0].Rows[i]["TotalQty"]);
                                    if (Convert.ToInt32(getvalue.Tables[0].Rows[i]["TotalQty"]) == Convert.ToInt32(getvalue.Tables[0].Rows[i]["FullQty"]))
                                    {
                                        if (getvalue.Tables[0].Rows[i]["processtypeid"].ToString() == "35")
                                        {

                                            Gfull = Gfull + Convert.ToInt32(getvalue.Tables[0].Rows[i]["FullQty"]);
                                        }
                                    }
                                    if (Convert.ToInt32(getvalue.Tables[0].Rows[i]["TotalQty"]) == Convert.ToInt32(getvalue.Tables[0].Rows[i]["HalfQty"]))
                                    {
                                        if (getvalue.Tables[0].Rows[i]["processtypeid"].ToString() == "36")
                                        {
                                            ghalf = ghalf + Convert.ToInt32(getvalue.Tables[0].Rows[i]["HalfQty"]);
                                        }
                                    }
                                    //txtfull.Text = getvalue.Tables[0].Rows[i]["FullQty"].ToString();
                                    //txtHalf.Text = getvalue.Tables[0].Rows[i]["HalfQty"].ToString();
                                    dCrt.Rows.Add(dr);
                                }

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
            int toatal =tot;
            txtTotalQantity.Text = toatal.ToString();
            txtfull.Text = Gfull.ToString();
            txtHalf.Text = ghalf.ToString();
            drpsupervisor.Focus();

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

            if (txtsendingdate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Sending Date.Thank You!!!.')", true);
               
                return;
                
            }

            if(btnadd.Text=="Save")
            {
                int i36FS = 0, i36HS = 0, i38FS = 0, i38HS = 0, i39FS = 0, i39HS = 0, i40FS = 0, i40HS = 0, i42FS = 0, i42HS = 0, i44FS = 0, i44HS = 0;

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

                DateTime deliverydate = DateTime.ParseExact(txtdcdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime sendingdate = DateTime.ParseExact(txtsendingdate.Text , "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime receivingdate = DateTime.ParseExact(txtsendingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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


                int iscuscc = objbs.insertkajawork(txtdcno.Text, deliverydate, sendingdate, receivingdate, drpsupervisor.SelectedValue, cond, txtTotalQantity.Text, design);
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

                        TextBox txtprocess =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtprocess");

                        TextBox txtqty =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtqty");

                        TextBox txtdesign =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtdesign");


                        TextBox txt_36FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt36FS");
                        TextBox txt_38FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt38FS");
                        TextBox txt_39FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt39FS");
                        TextBox txt_40FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt40FS");
                        TextBox txt_42FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt42FS");
                        TextBox txt_44FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt44FS");

                        TextBox txt_36HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt36HS");
                        TextBox txt_38HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt38HS");
                        TextBox txt_39HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt39HS");
                        TextBox txt_40HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt40HS");
                        TextBox txt_42HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt42HS");
                        TextBox txt_44HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt44HS");

                        i36FS = Convert.ToInt32(txt_36FS.Text); i36HS = Convert.ToInt32(txt_36HS.Text);
                        i38FS = Convert.ToInt32(txt_38FS.Text); i38HS = Convert.ToInt32(txt_38HS.Text);
                        i39FS = Convert.ToInt32(txt_39FS.Text); i39HS = Convert.ToInt32(txt_39HS.Text);
                        i40FS = Convert.ToInt32(txt_40FS.Text); i40HS = Convert.ToInt32(txt_40HS.Text);
                        i42FS = Convert.ToInt32(txt_42FS.Text); i42HS = Convert.ToInt32(txt_42HS.Text);
                        i44FS = Convert.ToInt32(txt_44FS.Text); i44HS = Convert.ToInt32(txt_44HS.Text);

                        int itrans = objbs.inserttranskajawork(cuttid.Text, lotno.Text, drpjobwork.SelectedValue, txtRate.Text, txtqty.Text, unitid.Text, brandid.Text, txtprocess.Text, txtdesign.Text, i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                   
                }


                Response.Redirect("Dillo_KajaWorkGrid.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot No.Thank You!!!.')", true);
                return;
            }

        }



            else if (btnadd.Text == "Update")
            {


                int i36FS = 0, i36HS = 0, i38FS = 0, i38HS = 0, i39FS = 0, i39HS = 0, i40FS = 0, i40HS = 0, i42FS = 0, i42HS = 0, i44FS = 0, i44HS = 0;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Not allow To Update This Lot.Thank You!!!.')", true);
                return;

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

                  //  DateTime deliverydate = DateTime.ParseExact(txtdcdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                  //  //DateTime deliverydate = DateTime.Parse(Convert.ToDateTime(txtdcdate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));

                  //  DateTime sendingdate = DateTime.ParseExact(txtsendingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                  // // DateTime sendingdate = DateTime.Parse(Convert.ToDateTime(txtsendingdate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));

                  //  DateTime receivingdate = DateTime.ParseExact(txtsendingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                  ////  DateTime receivingdate = DateTime.Parse(Convert.ToDateTime(txtsendingdate.Text.Trim()).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture));
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



                 //   int isss = objbs.update_kaja_LotProcess(Convert.ToInt32(lot));

                    //int iscuscc = objbs.Update_kajawork(Convert.ToInt32(lot), txtdcno.Text, deliverydate, sendingdate, receivingdate, drpsupervisor.SelectedValue, cond, txtTotalQantity.Text, design);

                     int iscuscc = objbs.Update_kajawork(Convert.ToInt32(lot), txtdcno.Text, txtdcdate.Text , txtsendingdate.Text , txtsendingdate.Text , drpsupervisor.SelectedValue, cond, txtTotalQantity.Text, design);

                    objbs.Delete_transKajaProcess(Convert.ToInt32(lot));

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

                        TextBox txtprocess =
                            (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtprocess");

                        TextBox txtqty =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtqty");

                        TextBox txtdesign =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtdesign");

                        TextBox txt_36FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt36FS");
                        TextBox txt_38FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt38FS");
                        TextBox txt_39FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt39FS");
                        TextBox txt_40FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt40FS");
                        TextBox txt_42FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt42FS");
                        TextBox txt_44FS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt44FS");

                        TextBox txt_36HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt36HS");
                        TextBox txt_38HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt38HS");
                        TextBox txt_39HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt39HS");
                        TextBox txt_40HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt40HS");
                        TextBox txt_42HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt42HS");
                        TextBox txt_44HS = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txt44HS");

                        i36FS = Convert.ToInt32(txt_36FS.Text); i36HS = Convert.ToInt32(txt_36HS.Text);
                        i38FS = Convert.ToInt32(txt_38FS.Text); i38HS = Convert.ToInt32(txt_38HS.Text);
                        i39FS = Convert.ToInt32(txt_39FS.Text); i39HS = Convert.ToInt32(txt_39HS.Text);
                        i40FS = Convert.ToInt32(txt_40FS.Text); i40HS = Convert.ToInt32(txt_40HS.Text);
                        i42FS = Convert.ToInt32(txt_42FS.Text); i42HS = Convert.ToInt32(txt_42HS.Text);
                        i44FS = Convert.ToInt32(txt_44FS.Text); i44HS = Convert.ToInt32(txt_44HS.Text);


                        int itrans = objbs.update_transkajawork(cuttid.Text, lotno.Text, drpjobwork.SelectedValue, txtRate.Text, txtqty.Text, unitid.Text, brandid.Text, Convert.ToInt32(lot), txtprocess.Text, txtdesign.Text, i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);

                    }


                    Response.Redirect("Dillo_KajaWorkGrid.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot No.Thank You!!!.')", true);
                    return;
                }

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

            Response.Redirect("Dillo_KajaworkGrid.aspx");


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