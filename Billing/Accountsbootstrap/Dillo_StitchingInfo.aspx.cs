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
    public partial class Dillo_StitchingInfo : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        decimal totalfs = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            string iLotNo = Request.QueryString.Get("LotNo");
            int unitid = 0;int Brandid = 0;int cutmasterid = 0;
            if (!IsPostBack)
            {


                FirstGridViewRow_Size();


                txtProcessDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                divBrand.Visible = false;
                divchcksizeManual.Visible = false;
                divCuttingMaster.Visible = false;
                //divfull.Visible = false;
                //divHalf.Visible = false;
               // divCheckProcess.Visible = false;

                DataSet dsLotNo = objbs.Select_Lot();//tblCut
                if (dsLotNo.Tables[0].Rows.Count > 0)
                {
                    ddlLotNo.DataSource = dsLotNo.Tables[0];
                    ddlLotNo.DataTextField = "LotNo";
                    ddlLotNo.DataValueField = "CutID";
                    ddlLotNo.DataBind();
                    ddlLotNo.Items.Insert(0, "Select Lot No");
                }

                DataSet dsUnitName = objbs.Select_UnitFirst();//tblUnit
                if (dsUnitName.Tables[0].Rows.Count > 0)
                {
                    ddlUnit.DataSource = dsUnitName.Tables[0];
                    ddlUnit.DataTextField = "UnitName";
                    ddlUnit.DataValueField = "UnitID";
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, "Select Unit Name");
                }

                DataSet dst = objbs.Getcuttmastrr();
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        drpCutting.DataSource = dst.Tables[0];
                        drpCutting.DataTextField = "LedgerName";
                        drpCutting.DataValueField = "LedgerID";
                        drpCutting.DataBind();
                        drpCutting.Items.Insert(0, "Select Cutting");
                    }
                }

                DataSet brandName = objbs.getBrandName();
                if (brandName.Tables[0].Rows.Count > 0)
                {
                    ddlBrand.DataSource = brandName.Tables[0];
                    ddlBrand.DataTextField = "BrandName";
                    ddlBrand.DataValueField = "BrandID";
                    ddlBrand.DataBind();
                    ddlBrand.Items.Insert(0, "Select Brand Name");
                }

           
                if (iLotNo != null)
                {
                    ds = objbs.SelectStitchingInfoDetEdit(Convert.ToInt32(iLotNo));
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        btnadd.Text = "Update";
                        ddlLotNo.DataSource = ds.Tables[0];
                        ddlLotNo.DataTextField = "LotNo";
                        ddlLotNo.DataValueField = "CutID";
                        ddlLotNo.DataBind();

                        //ddlUnit.DataSource = ds.Tables[0];
                        //ddlUnit.DataTextField = "UnitName";
                        //ddlUnit.DataValueField = "UnitID";
                        //ddlUnit.DataBind();

                        //drpCutting.DataSource = ds.Tables[0];
                        //drpCutting.DataTextField = "LedgerName";
                        //drpCutting.DataValueField = "LedgerID";
                        //drpCutting.DataBind();

                        //ddlBrand.DataSource = ds.Tables[0];
                        //ddlBrand.DataTextField = "BrandName";
                        //ddlBrand.DataValueField = "BrandID";
                        //ddlBrand.DataBind();

                        txtfull.Text = ds.Tables[0].Rows[0]["FullQty"].ToString();
                        txtHalf.Text = ds.Tables[0].Rows[0]["HalfQty"].ToString();
                        txtTotalQantity.Text = ds.Tables[0].Rows[0]["TotalQuantity"].ToString();
                        txtManualLotno.Text = ds.Tables[0].Rows[0]["CutID"].ToString();
                        txtbrandid.Text = ds.Tables[0].Rows[0]["Brandid1"].ToString();
                        txtledgerid.Text = ds.Tables[0].Rows[0]["Ledgerid"].ToString();
                        txtdesignnumber.Text = ds.Tables[0].Rows[0]["DesignNo"].ToString();

                        unitid = Convert.ToInt32(ds.Tables[0].Rows[0]["UnitID"].ToString());
                        Brandid  = Convert.ToInt32(ds.Tables[0].Rows[0]["BrandID"].ToString());
                        cutmasterid  = Convert.ToInt32(ds.Tables[0].Rows[0]["LedgerID"].ToString());

                      //  txtProcessDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ProcessDate"]).ToString("dd/MM/yyyy");
                        string processDate = ds.Tables[0].Rows[0]["ProcessDate"].ToString();
                        if (processDate == "")
                        {
                            DateTime date = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            txtProcessDate.Text = date.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            DateTime date = DateTime.ParseExact(Convert.ToDateTime(processDate).ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            txtProcessDate.Text = date.ToString("dd/MM/yyyy");
                            //        CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                        }

                        if (ds.Tables[0].Rows[0]["IsManual"].ToString() == "True")
                        {

                            divlotNo.Visible = false;
                            divManualLot.Visible = true;

                            divCuttingMasterText.Visible = false;
                            divCuttingMaster.Visible = true;

                            divBrandText.Visible = false;
                            divBrand.Visible = true;

                            divchcksize.Visible = false;
                            divchcksizeManual.Visible = true;

                            //divCheckProcess.Visible = true;

                            txtTotalQantity.Enabled = false;
                            txtManualLotno.Enabled = false;
                            chckManualLot.Enabled = false;
                        //    CheckEmbroiding.Enabled = false;
                        //    CheckWashing.Enabled = false;
                          //  CheckKaja.Enabled = false;
                            txtTotalQantity.Text = ds.Tables[0].Rows[0]["TotalQuantity"].ToString();
                            txtManualLotno.Text = ds.Tables[0].Rows[0]["CutID"].ToString();
                            
                            if (ds.Tables[0].Rows[0]["IsKaja"].ToString() == "True")
                            {
                                CheckKaja.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["IsEmbroiding"].ToString() == "True")
                            {
                                CheckEmbroiding.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["IsWashing"].ToString() == "True")
                            {
                                CheckWashing.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["IsIron"].ToString() == "True")
                            {
                                CheckIron.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["IsChecking"].ToString() == "True")
                            {
                                CheckChecking.Checked = true;
                            }
                            if (ds.Tables[0].Rows[0]["IsTrimming"].ToString() == "True")
                            {
                                CheckTrimming.Checked = true;
                            }

                        }
                        else
                        {
                            divlotNo.Visible = true;
                            divManualLot.Visible = false;

                            divCuttingMasterText.Visible = true;
                            divCuttingMaster.Visible = false;

                            divBrandText.Visible = true;
                            divBrand.Visible = false;

                            divchcksize.Visible = true;
                            divchcksizeManual.Visible = false;

                            txtTotalQantity.Enabled = false;
                        }



                        DataSet temp1 = new DataSet();
                        DataTable dtt = new DataTable();

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

                        temp1.Tables.Add(dtt);

                        for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                        {

                            DataRow dr = dtt.NewRow();

                            dr["36FS"] = ds.Tables[0].Rows[k]["36FS"].ToString();
                            dr["36HS"] = ds.Tables[0].Rows[k]["36HS"].ToString();
                            dr["38FS"] = ds.Tables[0].Rows[k]["38FS"].ToString();
                            dr["38HS"] = ds.Tables[0].Rows[k]["38HS"].ToString();
                            dr["39FS"] = ds.Tables[0].Rows[k]["39FS"].ToString();
                            dr["39HS"] = ds.Tables[0].Rows[k]["39HS"].ToString();
                            dr["40FS"] = ds.Tables[0].Rows[k]["40FS"].ToString();
                            dr["40HS"] = ds.Tables[0].Rows[k]["40HS"].ToString();
                            dr["42FS"] = ds.Tables[0].Rows[k]["42FS"].ToString();
                            dr["42HS"] = ds.Tables[0].Rows[k]["42HS"].ToString();
                            dr["44FS"] = ds.Tables[0].Rows[k]["44FS"].ToString();
                            dr["44HS"] = ds.Tables[0].Rows[k]["44HS"].ToString();
                     
                            temp1.Tables[0].Rows.Add(dr);
                        }



                        ViewState["CurrentTable2"] = dtt;

                        GV_Size.DataSource = dtt;
                        GV_Size.DataBind();


                        for (int i = 0; i < GV_Size.Rows.Count; i++)
                        {


                            TextBox txt_36HS = (TextBox)GV_Size.Rows[i].FindControl("txt36HS");
                            TextBox txt_38HS = (TextBox)GV_Size.Rows[i].FindControl("txt38HS");
                            TextBox txt_39HS = (TextBox)GV_Size.Rows[i].FindControl("txt39HS");
                            TextBox txt_40HS = (TextBox)GV_Size.Rows[i].FindControl("txt40HS");
                            TextBox txt_42HS = (TextBox)GV_Size.Rows[i].FindControl("txt42HS");
                            TextBox txt_44HS = (TextBox)GV_Size.Rows[i].FindControl("txt44HS");

                            TextBox txt_36FS = (TextBox)GV_Size.Rows[i].FindControl("txt36FS");
                            TextBox txt_38FS = (TextBox)GV_Size.Rows[i].FindControl("txt38FS");
                            TextBox txt_39FS = (TextBox)GV_Size.Rows[i].FindControl("txt39FS");
                            TextBox txt_40FS = (TextBox)GV_Size.Rows[i].FindControl("txt40FS");
                            TextBox txt_42FS = (TextBox)GV_Size.Rows[i].FindControl("txt42FS");
                            TextBox txt_44FS = (TextBox)GV_Size.Rows[i].FindControl("txt44FS");


                            txt_36HS.Text = temp1.Tables[0].Rows[i]["36HS"].ToString();
                            txt_38HS.Text = temp1.Tables[0].Rows[i]["38HS"].ToString();
                            txt_39HS.Text = temp1.Tables[0].Rows[i]["39HS"].ToString();
                            txt_40HS.Text = temp1.Tables[0].Rows[i]["40HS"].ToString();
                            txt_42HS.Text = temp1.Tables[0].Rows[i]["42HS"].ToString();
                            txt_44HS.Text = temp1.Tables[0].Rows[i]["44HS"].ToString();

                            txt_36FS.Text = temp1.Tables[0].Rows[i]["36FS"].ToString();
                            txt_38FS.Text = temp1.Tables[0].Rows[i]["38FS"].ToString();
                            txt_39FS.Text = temp1.Tables[0].Rows[i]["39FS"].ToString();
                            txt_40FS.Text = temp1.Tables[0].Rows[i]["40FS"].ToString();
                            txt_42FS.Text = temp1.Tables[0].Rows[i]["42FS"].ToString();
                            txt_44FS.Text = temp1.Tables[0].Rows[i]["44FS"].ToString();

                        }

               
                    }
                    DataSet dGetstichingDetails = objbs.GetStichingInfo(Convert.ToInt32(iLotNo));
                    if (dGetstichingDetails != null)
                    {
                        if (dGetstichingDetails.Tables[0].Rows.Count > 0)
                        {

                            DataSet temp = new DataSet();
                            DataTable dt = new DataTable();

                            dt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                            dt.Columns.Add(new DataColumn("Process", typeof(string)));
                            dt.Columns.Add(new DataColumn("Rate", typeof(string)));


                            temp.Tables.Add(dt);
                            for (int i = 0; i < dGetstichingDetails.Tables[0].Rows.Count; i++)
                            {

                                DataRow dr = dt.NewRow();

                                dr["OrderNo"] = dGetstichingDetails.Tables[0].Rows[i]["Transid"].ToString();
                                dr["Process"] = dGetstichingDetails.Tables[0].Rows[i]["processtypeid"].ToString();
                                dr["Rate"] = dGetstichingDetails.Tables[0].Rows[i]["Rate"].ToString();
                                //  dt.Rows.Add(dr);
                                temp.Tables[0].Rows.Add(dr);
                            }
                            // temp.Tables.Add(dt);
                            ViewState["CurrentTable1"] = dt;

                            //temp = (DataSet)ViewState["CurrentTable1"];
                            gvcustomerorder.DataSource = temp;
                            gvcustomerorder.DataBind();




                            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                            {
                                Label lbltrans = (Label)gvcustomerorder.Rows[i].FindControl("lbltransno");

                                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");

                                TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                                lbltrans.Text = temp.Tables[0].Rows[i]["OrderNo"].ToString();
                                drpprocess.SelectedValue = temp.Tables[0].Rows[i]["Process"].ToString();
                                txtrate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();
                            }

                            StitchingInfo_Load(sender, e);
                            AddNewRow();

                            DataSet chckLotNoinLotProcess = objbs.chckLotNoinLotProcess(Convert.ToInt32(txtManualLotno.Text));
                            if (chckLotNoinLotProcess.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                {
                                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                                    drpprocess.Enabled = false;
                                }
                                txtfull.Enabled = false;
                                txtHalf.Enabled = false;
                            }

                        }
                        else
                        {
                          //  FirstGridViewRow();
                            DataSet dGetstichingDetailsnew = objbs.getratedeailss();
                             if (dGetstichingDetailsnew != null)
                             {
                                 if (dGetstichingDetailsnew.Tables[0].Rows.Count > 0)
                                 {

                                     DataSet temp = new DataSet();
                                     DataTable dt = new DataTable();

                                     dt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                                     dt.Columns.Add(new DataColumn("Process", typeof(string)));
                                     dt.Columns.Add(new DataColumn("Rate", typeof(string)));


                                     temp.Tables.Add(dt);
                                     for (int i = 0; i < dGetstichingDetailsnew.Tables[0].Rows.Count; i++)
                                     {

                                         DataRow dr = dt.NewRow();

                                         dr["OrderNo"] = "";
                                         dr["Process"] = dGetstichingDetailsnew.Tables[0].Rows[i]["ProcessMasterID"].ToString();
                                         dr["Rate"] = dGetstichingDetailsnew.Tables[0].Rows[i]["Rate"].ToString();
                                         //  dt.Rows.Add(dr);
                                         temp.Tables[0].Rows.Add(dr);
                                     }
                                     // temp.Tables.Add(dt);
                                     ViewState["CurrentTable1"] = dt;

                                     //temp = (DataSet)ViewState["CurrentTable1"];
                                     gvcustomerorder.DataSource = temp;
                                     gvcustomerorder.DataBind();




                                     for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                     {
                                         Label lbltrans = (Label)gvcustomerorder.Rows[i].FindControl("lbltransno");

                                         DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");

                                         TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                                         lbltrans.Text = "";
                                         drpprocess.SelectedValue = temp.Tables[0].Rows[i]["Process"].ToString();
                                         txtrate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();
                                     }

                                    // StitchingInfo_Load(sender, e);
                                     AddNewRow();

                                     DataSet chckLotNoinLotProcess = objbs.chckLotNoinLotProcess(Convert.ToInt32(txtManualLotno.Text));
                                     if (chckLotNoinLotProcess.Tables[0].Rows.Count > 0)
                                     {
                                         for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                         {
                                             DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                                             drpprocess.Enabled = false;
                                         }
                                         txtfull.Enabled = false;
                                         txtHalf.Enabled = false;
                                     }

                                 }
                             }
                          
                        }
                    }

                    int lotno =Convert.ToInt32(ddlLotNo.SelectedValue );
                    string stichstatus = "";
                    DataSet dsstatus = objbs.SelectLotProcessStatus(lotno);
                    if (dsstatus.Tables[0].Rows.Count > 0)
                    {
                        stichstatus = dsstatus.Tables[0].Rows[0]["Status"].ToString();
                    }
                    if (stichstatus == "Y")
                    {
                         ds = objbs.SelectStitchingInfoDetEdit(Convert.ToInt32(iLotNo));
                        if (ds.Tables[0].Rows.Count >= 1)
                        {
                            ddlUnit.DataSource = ds.Tables[0];
                            ddlUnit.DataTextField = "UnitName";
                            ddlUnit.DataValueField = "UnitID";
                            ddlUnit.DataBind();
                            ddlUnit.SelectedValue = Convert.ToString(unitid);

                            drpCutting.DataSource = ds.Tables[0];
                            drpCutting.DataTextField = "LedgerName";
                            drpCutting.DataValueField = "LedgerID";
                            drpCutting.DataBind();
                            drpCutting.SelectedValue = Convert.ToString(cutmasterid );

                            ddlBrand.DataSource = ds.Tables[0];
                            ddlBrand.DataTextField = "BrandName";
                            ddlBrand.DataValueField = "BrandID";
                            ddlBrand.DataBind();
                            ddlBrand.SelectedValue = Convert.ToString(Brandid );


                        }
                    }
                    else
                    {
                        DataSet unit1 = objbs.Select_UnitFirst();
                        if (unit1 != null)
                        {
                            if (unit1.Tables[0].Rows.Count > 0)
                            {
                                ddlUnit.DataSource = unit1.Tables[0];
                                ddlUnit.DataTextField = "UnitName";
                                ddlUnit.DataValueField = "UnitID";
                                ddlUnit.DataBind();
                                ddlUnit.SelectedValue = Convert.ToString(unitid);
                            }
                        }

                         DataSet cutmast1 = objbs.Getcuttmastrr();
                         if (cutmast1 != null)
                         {
                             if (cutmast1.Tables[0].Rows.Count > 0)
                             {
                                 drpCutting.DataSource = cutmast1.Tables[0];
                                 drpCutting.DataTextField = "LedgerName";
                                 drpCutting.DataValueField = "LedgerID";
                                 drpCutting.DataBind();
                                 drpCutting.SelectedValue = Convert.ToString(cutmasterid);
                             }
                         }

                           DataSet brandName1 = objbs.getBrandName();
                           if (brandName1.Tables[0].Rows.Count > 0)
                           {

                               ddlBrand.DataSource = brandName1.Tables[0];
                               ddlBrand.DataTextField = "BrandName";
                               ddlBrand.DataValueField = "BrandID";
                               ddlBrand.DataBind();
                               ddlBrand.SelectedValue = Convert.ToString(Brandid);
                           }

                    }
                }
                else
                {
                   // FirstGridViewRow();
                    {
                        //  FirstGridViewRow();
                        DataSet dGetstichingDetailsnew = objbs.getratedeailss();
                        if (dGetstichingDetailsnew != null)
                        {
                            if (dGetstichingDetailsnew.Tables[0].Rows.Count > 0)
                            {

                                DataSet temp = new DataSet();
                                DataTable dt = new DataTable();

                                dt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                                dt.Columns.Add(new DataColumn("Process", typeof(string)));
                                dt.Columns.Add(new DataColumn("Rate", typeof(string)));


                                temp.Tables.Add(dt);
                                for (int i = 0; i < dGetstichingDetailsnew.Tables[0].Rows.Count; i++)
                                {

                                    DataRow dr = dt.NewRow();

                                    dr["OrderNo"] = "";
                                    dr["Process"] = dGetstichingDetailsnew.Tables[0].Rows[i]["ProcessMasterID"].ToString();
                                    dr["Rate"] = dGetstichingDetailsnew.Tables[0].Rows[i]["Rate"].ToString();
                                    //  dt.Rows.Add(dr);
                                    temp.Tables[0].Rows.Add(dr);
                                }
                                // temp.Tables.Add(dt);
                                ViewState["CurrentTable1"] = dt;

                                //temp = (DataSet)ViewState["CurrentTable1"];
                                gvcustomerorder.DataSource = temp;
                                gvcustomerorder.DataBind();




                                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                {
                                    Label lbltrans = (Label)gvcustomerorder.Rows[i].FindControl("lbltransno");

                                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");

                                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                                    lbltrans.Text = "";
                                    drpprocess.SelectedValue = temp.Tables[0].Rows[i]["Process"].ToString();
                                    txtrate.Text = temp.Tables[0].Rows[i]["Rate"].ToString();
                                }

                                // StitchingInfo_Load(sender, e);
                                AddNewRow();

                            
                                {
                                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                    {
                                        DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
                                        drpprocess.Enabled = false;
                                    }
                                    txtfull.Enabled = false;
                                    txtHalf.Enabled = false;
                                }

                            }
                        }

                    }
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
            }
        }

        protected void StitchingInfo_Load(object sender, EventArgs e)
        {

            DataSet ds23 = objbs.gettotalqtyCuttingprintreportnew(Convert.ToInt32(ddlLotNo.SelectedValue));
            if (ds23.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds23;
                GridView1.DataBind();

                GV_Size.Visible = false;
            }

            DataSet dataSet = objbs.getLotNoDetails(Convert.ToInt32(ddlLotNo.SelectedValue));
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                txtCuttingMaster.Text = dataSet.Tables[0].Rows[0]["LedgerName"].ToString();
                txtBrand.Text = dataSet.Tables[0].Rows[0]["BrandName"].ToString();
                txtledgerid.Text = dataSet.Tables[0].Rows[0]["Ledgerid"].ToString();
                txtbrandid.Text = dataSet.Tables[0].Rows[0]["Brandid1"].ToString();
                //txtTotalQantity.Text = dataSet.Tables[0].Rows[0]["Totalshirt"].ToString();
                DataSet separtqty = objbs.gethsfsqty(Convert.ToInt32(ddlLotNo.SelectedValue));
                if (separtqty.Tables[0].Rows.Count > 0)
                {
                    txtHalf.Text = separtqty.Tables[0].Rows[0]["hs"].ToString();
                    txtfull.Text = separtqty.Tables[0].Rows[0]["fs"].ToString();

                    txtTotalQantity.Text = Convert.ToInt32((Convert.ToInt32(txtHalf.Text) + Convert.ToInt32(txtfull.Text))).ToString();

                }

                bool wash = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["WashLab"]);
                bool emb = Convert.ToBoolean(dataSet.Tables[0].Rows[0]["LogoLab"]);
                CheckKaja.Checked = true;
                CheckIron.Checked = true;
              
                if (wash == true)
                {
                    CheckWashing.Checked = true;
                   // CheckWashing.Enabled = false;
                }
                if (emb == true)
                {
                    CheckEmbroiding.Checked = true;
                  //  CheckEmbroiding.Enabled = false;
                }
                //chkSizes.DataSource = dataSet.Tables[0];
                //chkSizes.DataTextField = "Size";
                //chkSizes.DataValueField = "Sizeid";
                //chkSizes.DataBind();

                //for (int i = 0; i <= dataSet.Tables[0].Rows.Count - 1; i++)
                //{
                //    string size = dataSet.Tables[0].Rows[i]["Size"].ToString();
                //    {
                //        chkSizes.Items.FindByValue(dataSet.Tables[0].Rows[i]["Sizeid"].ToString()).Selected = true;
                //    }
                //}
            }
            else
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Matches Found')", true);
                //return;
            }
        }



     

        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string iLotNo = Request.QueryString.Get("LotNo");
            ds = objbs.SelectStitchingInfoDetEdit(Convert.ToInt32(iLotNo));
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["IsManual"].ToString() == "True")
                {
                }
                else
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        if (e.Row.Cells[7].Text != "&nbsp;")
                        {
                            totalfs = totalfs + Convert.ToDecimal(e.Row.Cells[7].Text);
                        }
                        //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        // e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    }
                    else if (e.Row.RowType == DataControlRowType.Footer)
                    {
                        //  e.Row.Cells[6].Text = "Total:";
                        e.Row.Cells[7].Text = totalfs.ToString();
                        // totalfs = totalfs + Convert.ToDouble(e.Row.Cells[7].Text);
                        //   e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                        e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                    }
                    txtTotalQantity.Text = totalfs.ToString();
                }
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet drpProcess = objbs.SelectProcessType();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // DropDownList drpProcess1 = (DropDownList)e.Row.FindControl("drpProcess");
                var ddl = (DropDownList)e.Row.FindControl("drpProcess");
                ddl.DataSource = drpProcess;
                ddl.DataTextField = "ProcessType";
                ddl.DataValueField = "ProcessMasterID";
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Process Type");


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

        protected void Insert_LotDetails(object sender, EventArgs e)
        {
            int Qty = 0;
            int kajaqty=0, embqty=0, washqty=0, ironqty=0,checkingqty=0,trimmingqty=0;
            if (txtProcessDate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Process Date')", true);
                return;
            }

            DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnadd.Text == "Update")
            {


                int i36FS = 0, i36HS = 0, i38FS = 0, i38HS = 0, i39FS = 0, i39HS = 0, i40FS = 0, i40HS = 0, i42FS = 0, i42HS = 0, i44FS = 0, i44HS = 0;

                for (int j = 0; j < GV_Size.Rows.Count; j++)
                {
                    TextBox txt_36HS = (TextBox)GV_Size.Rows[j].FindControl("txt36HS");
                    TextBox txt_38HS = (TextBox)GV_Size.Rows[j].FindControl("txt38HS");
                    TextBox txt_39HS = (TextBox)GV_Size.Rows[j].FindControl("txt39HS");
                    TextBox txt_40HS = (TextBox)GV_Size.Rows[j].FindControl("txt40HS");
                    TextBox txt_42HS = (TextBox)GV_Size.Rows[j].FindControl("txt42HS");
                    TextBox txt_44HS = (TextBox)GV_Size.Rows[j].FindControl("txt44HS");

                    TextBox txt_36FS = (TextBox)GV_Size.Rows[j].FindControl("txt36FS");
                    TextBox txt_38FS = (TextBox)GV_Size.Rows[j].FindControl("txt38FS");
                    TextBox txt_39FS = (TextBox)GV_Size.Rows[j].FindControl("txt39FS");
                    TextBox txt_40FS = (TextBox)GV_Size.Rows[j].FindControl("txt40FS");
                    TextBox txt_42FS = (TextBox)GV_Size.Rows[j].FindControl("txt42FS");
                    TextBox txt_44FS = (TextBox)GV_Size.Rows[j].FindControl("txt44FS");


                    i36FS = Convert.ToInt32(txt_36FS.Text); i36HS = Convert.ToInt32(txt_36HS.Text);
                    i38FS = Convert.ToInt32(txt_38FS.Text); i38HS = Convert.ToInt32(txt_38HS.Text);
                    i39FS = Convert.ToInt32(txt_39FS.Text); i39HS = Convert.ToInt32(txt_39HS.Text);
                    i40FS = Convert.ToInt32(txt_40FS.Text); i40HS = Convert.ToInt32(txt_40HS.Text);
                    i42FS = Convert.ToInt32(txt_42FS.Text); i42HS = Convert.ToInt32(txt_42HS.Text);
                    i44FS = Convert.ToInt32(txt_44FS.Text); i44HS = Convert.ToInt32(txt_44HS.Text);



                }


                int iUpdateTotalQty = objbs.UpdatetbllotdetailsTotal(Convert.ToInt32(txtManualLotno.Text), Convert.ToInt32(txtfull.Text), Convert.ToInt32(txtHalf.Text), Convert.ToInt32(txtTotalQantity.Text), CheckEmbroiding.Checked, CheckWashing.Checked, CheckKaja.Checked, CheckIron.Checked, ddlUnit.SelectedValue, drpCutting.SelectedValue, ddlBrand.SelectedValue, txtdesignnumber.Text,CheckChecking.Checked, CheckTrimming.Checked, i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);

                if (CheckKaja.Checked == true)
                {
                    kajaqty = Convert.ToInt32(txtTotalQantity.Text);
                }
                else
                {
                    kajaqty = 0;
                }

                if (CheckEmbroiding.Checked == true)
                {
                    embqty = Convert.ToInt32(txtTotalQantity.Text);
                }
                else
                {
                    embqty = 0;
                }

                if (CheckWashing.Checked == true)
                {
                    washqty = Convert.ToInt32(txtTotalQantity.Text);
                }
                else
                {
                    washqty = 0;
                }

                if (CheckIron.Checked == true)
                {
                    ironqty = Convert.ToInt32(txtTotalQantity.Text);
                }
                else
                {
                    ironqty = 0;
                }
                if (CheckChecking.Checked == true)
                {
                    checkingqty = Convert.ToInt32(txtTotalQantity.Text);
                }
                else
                {
                    checkingqty = 0;
                }
                if (CheckTrimming.Checked == true)
                {
                    trimmingqty = Convert.ToInt32(txtTotalQantity.Text);
                }
                else
                {
                    trimmingqty = 0;
                }

                objbs.Update_Lotprocess_TotalQty(Convert.ToInt32(txtManualLotno.Text), Convert.ToInt32(txtTotalQantity.Text), kajaqty, embqty, washqty, ironqty,checkingqty,trimmingqty); 

                int istasDel = objbs.editdeletetbltranslotdetails(Convert.ToInt32(txtManualLotno.Text)); // to delete translotdetails table
                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {
                    Label lbltrans = (Label)gvcustomerorder.Rows[i].FindControl("lbltransno");
                    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");

                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                    if (drpprocess.SelectedValue == "Select Process Type")
                    {
                    }
                    else
                    {
                        DataSet dcheck = new DataSet();
                        dcheck = objbs.getprocesstype(drpprocess.SelectedValue);
                        int istas =0;
                        if (dcheck.Tables[0].Rows.Count > 0)
                        {
                            int type = Convert.ToInt32(dcheck.Tables[0].Rows[0]["Typeid"]);
                            if (type == 0)
                            {
                                Qty = Convert.ToInt32(txtTotalQantity.Text);
                                // To insert Translot Table
                                 istas = objbs.editinserttbltranslotdetails(drpprocess.SelectedValue, txtrate.Text, lbltrans.Text, Convert.ToInt32(txtManualLotno.Text), Qty, i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                            }
                            else if (type == 1)
                            {
                                Qty = Convert.ToInt32(txtfull.Text);
                                 istas = objbs.editinserttbltranslotdetails(drpprocess.SelectedValue, txtrate.Text, lbltrans.Text, Convert.ToInt32(txtManualLotno.Text), Qty, i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);
                            }
                            else if (type == 2)
                            {
                                Qty = Convert.ToInt32(txtHalf.Text);
                                istas = objbs.editinserttbltranslotdetails(drpprocess.SelectedValue, txtrate.Text, lbltrans.Text, Convert.ToInt32(txtManualLotno.Text), Qty, 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);
                            }
                        }
                        
                      
                    }

                }

                if (CheckKaja.Checked == true)
                {
                    DataSet dembb = objbs.checkembandeashlist(txtManualLotno.Text, "Kaja");
                    if (dembb.Tables[0].Rows.Count > 0)
                    {
                        string currentid = dembb.Tables[0].Rows[0]["Currentid"].ToString();
                        int iscu = objbs.Updatecurrntlist(currentid, ddlUnit.SelectedValue, txtManualLotno.Text);
                    }
                    else
                    {
                        int iscc = objbs.insertembcurrntlist(txtManualLotno.Text, txtManualLotno.Text, ddlUnit.SelectedValue, "Kaja");
                    }
                }
                else
                {
                    int isdelte = objbs.deleteembcurrntlist(txtManualLotno.Text, ddlUnit.SelectedValue, "Kaja");
                }

                if (CheckEmbroiding.Checked == true)
                {
                    DataSet dembb = objbs.checkembandeashlist(txtManualLotno.Text, "Emb");
                    if (dembb.Tables[0].Rows.Count > 0)
                    {
                        string currentid = dembb.Tables[0].Rows[0]["Currentid"].ToString();
                        int iscu = objbs.Updatecurrntlist(currentid, ddlUnit.SelectedValue, txtManualLotno.Text);
                    }
                    else
                    {
                        int iscc = objbs.insertembcurrntlist(txtManualLotno.Text, txtManualLotno.Text, ddlUnit.SelectedValue, "Emb");
                    }
                }
                else
                {
                    int isdelte = objbs.deleteembcurrntlist(txtManualLotno.Text, ddlUnit.SelectedValue, "Emb");
                }

                if (CheckWashing.Checked == true)
                {
                    DataSet dembb = objbs.checkembandeashlist(txtManualLotno.Text, "Wash");
                    if (dembb.Tables[0].Rows.Count > 0)
                    {
                        string currentid = dembb.Tables[0].Rows[0]["Currentid"].ToString();
                        int iscu = objbs.Updatecurrntlist(currentid, ddlUnit.SelectedValue, txtManualLotno.Text);
                    }
                    else
                    {
                        int iscc = objbs.insertembcurrntlist(txtManualLotno.Text, txtManualLotno.Text, ddlUnit.SelectedValue, "Wash");
                    }
                }
                else
                {
                    int isdelte = objbs.deleteembcurrntlist(txtManualLotno.Text, ddlUnit.SelectedValue, "Wash");
                }

                if (CheckIron.Checked == true)
                {
                    DataSet dembb = objbs.checkembandeashlist(txtManualLotno.Text, "Iron");
                    if (dembb.Tables[0].Rows.Count > 0)
                    {
                        string currentid = dembb.Tables[0].Rows[0]["Currentid"].ToString();
                        int iscu = objbs.Updatecurrntlist(currentid, ddlUnit.SelectedValue, txtManualLotno.Text);
                    }
                    else
                    {
                        int iscc = objbs.insertembcurrntlist(txtManualLotno.Text, txtManualLotno.Text, ddlUnit.SelectedValue, "Iron");
                    }
                }
                else
                {
                    int isdelte = objbs.deleteembcurrntlist(txtManualLotno.Text, ddlUnit.SelectedValue, "Iron");
                }

                if (CheckChecking.Checked == true)
                {
                    DataSet dembb = objbs.checkembandeashlist(txtManualLotno.Text, "Check");
                    if (dembb.Tables[0].Rows.Count > 0)
                    {
                        string currentid = dembb.Tables[0].Rows[0]["Currentid"].ToString();
                        int iscu = objbs.Updatecurrntlist(currentid, ddlUnit.SelectedValue, txtManualLotno.Text);
                    }
                    else
                    {
                        int iscc = objbs.insertembcurrntlist(txtManualLotno.Text, txtManualLotno.Text, ddlUnit.SelectedValue, "Check");
                    }
                }
                else
                {
                    int isdelte = objbs.deleteembcurrntlist(txtManualLotno.Text, ddlUnit.SelectedValue, "Check");
                }

                if (CheckTrimming.Checked == true)
                {
                    DataSet dembb = objbs.checkembandeashlist(txtManualLotno.Text, "Trim");
                    if (dembb.Tables[0].Rows.Count > 0)
                    {
                        string currentid = dembb.Tables[0].Rows[0]["Currentid"].ToString();
                        int iscu = objbs.Updatecurrntlist(currentid, ddlUnit.SelectedValue, txtManualLotno.Text);
                    }
                    else
                    {
                        int iscc = objbs.insertembcurrntlist(txtManualLotno.Text, txtManualLotno.Text, ddlUnit.SelectedValue, "Trim");
                    }
                }
                else
                {
                    int isdelte = objbs.deleteembcurrntlist(txtManualLotno.Text, ddlUnit.SelectedValue, "Trim");
                }

                Response.Redirect("Dillo_StitchingInfoGrid.aspx");
            }
            else
            {
                //DataSet dcheckk = objbs.checkdesignno(txtdesignnumber.Text);
                //if (dcheckk.Tables[0].Rows.Count > 0)
                //{
                //    if (dcheckk.Tables[0].Rows[0]["DesignNo"].ToString() != "" || dcheckk.Tables[0].Rows[0]["DesignNo"].ToString() != null)
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Design Number already exists')", true);
                //        return;
                //    }
                //}


                 int i36FS = 0, i36HS = 0, i38FS = 0, i38HS = 0, i39FS = 0, i39HS = 0, i40FS = 0, i40HS = 0, i42FS = 0, i42HS = 0, i44FS = 0, i44HS = 0;

                        for (int j = 0; j < GV_Size.Rows.Count; j++)
                        {
                            TextBox txt_36HS = (TextBox)GV_Size.Rows[j].FindControl("txt36HS");
                            TextBox txt_38HS = (TextBox)GV_Size.Rows[j].FindControl("txt38HS");
                            TextBox txt_39HS = (TextBox)GV_Size.Rows[j].FindControl("txt39HS");
                            TextBox txt_40HS = (TextBox)GV_Size.Rows[j].FindControl("txt40HS");
                            TextBox txt_42HS = (TextBox)GV_Size.Rows[j].FindControl("txt42HS");
                            TextBox txt_44HS = (TextBox)GV_Size.Rows[j].FindControl("txt44HS");

                            TextBox txt_36FS = (TextBox)GV_Size.Rows[j].FindControl("txt36FS");
                            TextBox txt_38FS = (TextBox)GV_Size.Rows[j].FindControl("txt38FS");
                            TextBox txt_39FS = (TextBox)GV_Size.Rows[j].FindControl("txt39FS");
                            TextBox txt_40FS = (TextBox)GV_Size.Rows[j].FindControl("txt40FS");
                            TextBox txt_42FS = (TextBox)GV_Size.Rows[j].FindControl("txt42FS");
                            TextBox txt_44FS = (TextBox)GV_Size.Rows[j].FindControl("txt44FS");


                            i36FS = Convert.ToInt32(txt_36FS.Text); i36HS = Convert.ToInt32(txt_36HS.Text);
                            i38FS = Convert.ToInt32(txt_38FS.Text); i38HS = Convert.ToInt32(txt_38HS.Text);
                            i39FS = Convert.ToInt32(txt_39FS.Text); i39HS = Convert.ToInt32(txt_39HS.Text);
                            i40FS = Convert.ToInt32(txt_40FS.Text); i40HS = Convert.ToInt32(txt_40HS.Text);
                            i42FS = Convert.ToInt32(txt_42FS.Text); i42HS = Convert.ToInt32(txt_42HS.Text);
                            i44FS = Convert.ToInt32(txt_44FS.Text); i44HS = Convert.ToInt32(txt_44HS.Text);



                        }


                if (chckManualLot.Checked == true)
                {
                    DataSet dsmanual = objbs.checkalreadyexistornotDetails(txtManualLotno.Text);
                    if (dsmanual.Tables[0].Rows.Count == 0)
                    {
                        
                        //int iStatus23 = objbs.insertLotDetails(Convert.ToInt32(txtManualLotno.Text), Convert.ToString(txtManualLotno.Text), Convert.ToInt32(drpCutting.SelectedValue),
                        //Convert.ToInt32(ddlBrand.SelectedValue), Convert.ToInt32(ddlUnit.SelectedValue), Convert.ToInt32(txtTotalQantity.Text),
                        //chckManualLot.Checked, CheckKaja.Checked, CheckEmbroiding.Checked, CheckWashing.Checked,CheckIron.Checked, Convert.ToInt32(txtHalf.Text), Convert.ToInt32(txtfull.Text), processDate,txtdesignnumber.Text);


                        int iStatus23 = objbs.insertLotDetails(Convert.ToInt32(txtManualLotno.Text), Convert.ToString(txtManualLotno.Text), Convert.ToInt32(drpCutting.SelectedValue),
                      Convert.ToInt32(ddlBrand.SelectedValue), Convert.ToInt32(ddlUnit.SelectedValue), Convert.ToInt32(txtTotalQantity.Text),
                      chckManualLot.Checked, CheckKaja.Checked, CheckEmbroiding.Checked, CheckWashing.Checked, CheckIron.Checked, Convert.ToInt32(txtHalf.Text), Convert.ToInt32(txtfull.Text), processDate, txtdesignnumber.Text,
                      CheckChecking.Checked,CheckTrimming.Checked,i36FS,i36HS,i38FS,i38HS,i39FS,i39HS,i40FS,i40HS,i42FS,i42HS,i44FS,i44HS);

                        for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                        {
                            DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");

                            TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                            if (drpprocess.SelectedValue == "Select Process Type")
                            {
                            }
                            else
                            {
                                DataSet dcheck = new DataSet();
                                dcheck = objbs.getprocesstype(drpprocess.SelectedValue);

                                int istas = 0;
                                if (dcheck.Tables[0].Rows.Count > 0)
                                {
                                    int type = Convert.ToInt32(dcheck.Tables[0].Rows[0]["Typeid"]);
                                    if (type == 0)
                                    {
                                        Qty = Convert.ToInt32(txtTotalQantity.Text);
                                        istas = objbs.inserttransLotDetails(drpprocess.SelectedValue, txtrate.Text, Qty, i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                                    }
                                    else if (type == 1)
                                    {
                                        Qty = Convert.ToInt32(txtfull.Text);
                                        istas = objbs.inserttransLotDetails(drpprocess.SelectedValue, txtrate.Text, Qty, i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);
                                    }
                                    else if (type == 2)
                                    {
                                        Qty = Convert.ToInt32(txtHalf.Text);
                                        istas = objbs.inserttransLotDetails(drpprocess.SelectedValue, txtrate.Text, Qty, 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);

                                    }

                                    
                                }
                            }
                        }

                        Response.Redirect("Dillo_StitchingInfoGrid.aspx");
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Manual Lot no already exists')", true);
                        return;
                    }
                }
                else
                {
                    if (ddlLotNo.SelectedValue == "Select Lot No")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot No.Thank You!!!.')", true);
                        return;
                    }

                    DataSet ds = objbs.checkalreadyexistornotfusingcut(ddlLotNo.SelectedValue, ddlLotNo.SelectedItem.Text);

                    if (ds.Tables[0].Rows.Count == 0)
                    {

                        for (int j = 0; j < GridView1.Rows.Count; j++)
                        {
                            TextBox txtfit = (TextBox)GridView1.Rows[j].FindControl("txtfit");
                            TextBox txt_36 = (TextBox)GridView1.Rows[j].FindControl("txtts");
                            TextBox txt_38 = (TextBox)GridView1.Rows[j].FindControl("txtte");
                            TextBox txt_39 = (TextBox)GridView1.Rows[j].FindControl("txttn");
                            TextBox txt_40 = (TextBox)GridView1.Rows[j].FindControl("txtfz");
                            TextBox txt_42 = (TextBox)GridView1.Rows[j].FindControl("txtft");
                            TextBox txt_44 = (TextBox)GridView1.Rows[j].FindControl("txtff");
                          //  TextBox txt_44HS = (TextBox)GridView1.Rows[j].FindControl("txttot");

                            if (txtfit.Text == "FS")
                            {
                                i36FS = Convert.ToInt32(txt_36.Text);
                                i38FS = Convert.ToInt32(txt_38.Text);
                                i39FS = Convert.ToInt32(txt_39.Text);
                                i40FS = Convert.ToInt32(txt_40.Text);
                                i42FS = Convert.ToInt32(txt_42.Text);
                                i44FS = Convert.ToInt32(txt_44.Text);
                            }
                            else
                            {
                                i36HS = Convert.ToInt32(txt_36.Text);
                                i38HS = Convert.ToInt32(txt_38.Text);
                                i39HS = Convert.ToInt32(txt_39.Text);
                                i40HS = Convert.ToInt32(txt_40.Text);
                                i42HS = Convert.ToInt32(txt_42.Text);
                                i44HS = Convert.ToInt32(txt_44.Text);
                            }


                        }
                       

                           // int iStatus23 = objbs.insertLotDetails(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToString(ddlLotNo.SelectedItem.Text), Convert.ToInt32(txtledgerid.Text),
                           // Convert.ToInt32(txtbrandid.Text), Convert.ToInt32(ddlUnit.SelectedValue), Convert.ToInt32(txtTotalQantity.Text),
                           //chckManualLot.Checked, CheckKaja.Checked, CheckEmbroiding.Checked, CheckWashing.Checked,CheckIron.Checked, Convert.ToInt32(txtHalf.Text), Convert.ToInt32(txtfull.Text), processDate,txtdesignnumber.Text);



                        int iStatus23 = objbs.insertLotDetails(Convert.ToInt32(ddlLotNo.SelectedValue), Convert.ToString(ddlLotNo.SelectedItem.Text), Convert.ToInt32(txtledgerid.Text),
                      Convert.ToInt32(txtbrandid.Text), Convert.ToInt32(ddlUnit.SelectedValue), Convert.ToInt32(txtTotalQantity.Text),
                      chckManualLot.Checked, CheckKaja.Checked, CheckEmbroiding.Checked, CheckWashing.Checked, CheckIron.Checked, Convert.ToInt32(txtHalf.Text), Convert.ToInt32(txtfull.Text), processDate, txtdesignnumber.Text,
                      CheckChecking.Checked, CheckTrimming.Checked, i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);


                            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                            {
                                DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");

                                TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                                if (drpprocess.SelectedValue == "Select Process Type")
                                {
                                }
                                else
                                //{
                                //    int istas = objbs.inserttransLotDetails(drpprocess.SelectedValue, txtrate.Text, Convert.ToInt32(txtTotalQantity.Text), i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                                //}
                                {
                                    DataSet dcheck = new DataSet();
                                    dcheck = objbs.getprocesstype(drpprocess.SelectedValue);

                                    int istas = 0;
                                    if (dcheck.Tables[0].Rows.Count > 0)
                                    {
                                        int type = Convert.ToInt32(dcheck.Tables[0].Rows[0]["Typeid"]);
                                        if (type == 0)
                                        {
                                            Qty = Convert.ToInt32(txtTotalQantity.Text);
                                            istas = objbs.inserttransLotDetails(drpprocess.SelectedValue, txtrate.Text, Qty, i36FS, i36HS, i38FS, i38HS, i39FS, i39HS, i40FS, i40HS, i42FS, i42HS, i44FS, i44HS);
                                        }
                                        else if (type == 1)
                                        {
                                            Qty = Convert.ToInt32(txtfull.Text);
                                            istas = objbs.inserttransLotDetails(drpprocess.SelectedValue, txtrate.Text, Qty, i36FS, 0, i38FS, 0, i39FS, 0, i40FS, 0, i42FS, 0, i44FS, 0);
                                        }
                                        else if (type == 2)
                                        {
                                            Qty = Convert.ToInt32(txtHalf.Text);
                                            istas = objbs.inserttransLotDetails(drpprocess.SelectedValue, txtrate.Text, Qty, 0, i36HS, 0, i38HS, 0, i39HS, 0, i40HS, 0, i42HS, 0, i44HS);

                                        }


                                    }
                                }
                            }
                        }
                    
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Lot no already exists')", true);
                        return;
                    }


                }

                Response.Redirect("Dillo_StitchingInfoGrid.aspx");

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
                        Label lbltrans = (Label)gvcustomerorder.Rows[rowIndex].FindControl("lbltransno");

                        DropDownList drpProcess =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Orderno"] = lbltrans.Text;
                        dtCurrentTable.Rows[i - 1]["Size"] = drpProcess.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtRate.Text;

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

            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["Process"] = string.Empty;
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

            dct = new DataColumn("OrderNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Process");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["OrderNo"] = 1;
            drNew["Process"] = "";
            drNew["Rate"] = "";

            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();

        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            //int No = 0;

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{

            //    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
            //    TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

            //    if (drpProcess.SelectedValue == "Select Process")
            //    {
            //        No = 0;
            //        break;
            //    }
            //    else
            //    {
            //        No = 1;
            //    }
            //}

            //if (No == 1)
            //{
            //if (btnadd.Text == "Save")
            //{
                AddNewRow();
            //}
            //}
            //else
            //{

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
                Label lbltrans = (Label)gvcustomerorder.Rows[vLoop].FindControl("lbltransno");

                DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                //  DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");
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
                        Label lbltrans = (Label)gvcustomerorder.Rows[rowIndex].FindControl("lbltransno");

                        DropDownList drpProcess =
                      (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txtRate");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["Orderno"] = lbltrans.Text; ;
                        dtCurrentTable.Rows[i - 1]["Process"] = drpProcess.SelectedValue;
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
                        Label lbltrans = (Label)gvcustomerorder.Rows[rowIndex].FindControl("lbltransno");

                        DropDownList drpProcess =
                     (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("drpProcess");

                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtRate");

                        drpProcess.SelectedValue = dt.Rows[i]["Process"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        lbltrans.Text = dt.Rows[i]["Orderno"].ToString();

                        rowIndex++;
                        drpProcess.Focus();
                    }
                }
            }
        }

        protected void Change_ManualLot(object sender, EventArgs e)
        {
            if (chckManualLot.Checked == true)
            {
                divlotNo.Visible = false;
                divManualLot.Visible = true;

                divCuttingMasterText.Visible = false;
                divCuttingMaster.Visible = true;

                divBrandText.Visible = false;
                divBrand.Visible = true;

                divchcksize.Visible = false;
                divchcksizeManual.Visible = true;

                txtTotalQantity.Enabled = false;
                //divfull.Visible = true;
                //divHalf.Visible = true;

                CheckEmbroiding.Enabled = true;
                CheckWashing.Enabled = true;

                //divCheckProcess.Visible = true;
            }
            else
            {
                CheckEmbroiding.Enabled = false;
                CheckWashing.Enabled = false;
                divlotNo.Visible = true;
                divManualLot.Visible = false;

                divCuttingMasterText.Visible = true;
                divCuttingMaster.Visible = false;

                divBrandText.Visible = true;
                divBrand.Visible = false;

                divchcksize.Visible = true;
                divchcksizeManual.Visible = false;

                txtTotalQantity.Enabled = false;
                //divfull.Visible = false;
                //divHalf.Visible = false;
               // divCheckProcess.Visible = false;
            }
        }

        protected void brandfill(object sender, EventArgs e)
        {
            DataSet dsFit = objbs.GetFit();
            if (dsFit != null)
            {
                if (dsFit.Tables[0].Rows.Count > 0)
                {

                    //drpFit.DataSource = dsFit.Tables[0];
                    //drpFit.DataTextField = "Fit";
                    //drpFit.DataValueField = "FitID";
                    //drpFit.DataBind();


                    //Sddrrpfit.DataSource = dsFit.Tables[0];
                    //Sddrrpfit.DataTextField = "Fit";
                    //Sddrrpfit.DataValueField = "FitID";
                    //Sddrrpfit.DataBind();

                }
            }





            if (ddlBrand.SelectedValue == "Select Brand Name")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Brand Name');", true);
                return;
            }
            else
            {

            }

            DataSet dbrandcheck = objbs.getbrandnameforcuttprocessnew(ddlBrand.SelectedValue);
            if (dbrandcheck.Tables[0].Rows.Count > 0)
            {
                //drpBrand.DataSource = dbrandcheck.Tables[0];
                //drpBrand.DataTextField = "BrandName";
                //drpBrand.DataValueField = "BrandID";
                //drpBrand.DataBind();
                // drpCustomer.Items.Insert(0, "Select Party Name");
            }
            if (dbrandcheck.Tables[0].Rows.Count > 0)
            {

                string fidit = dbrandcheck.Tables[0].Rows[0]["fitid"].ToString();
                //Sddrrpfit.SelectedValue = dbrandcheck.Tables[0].Rows[0]["fitid"].ToString();
                //drpFit.SelectedValue = dbrandcheck.Tables[0].Rows[0]["fitid"].ToString();
                //drpBrand.SelectedValue = dbrandcheck.Tables[0].Rows[0]["BrandID"].ToString();
                DataSet dsize = objbs.Getsizetypenew(fidit);
                if (dsize != null)
                {
                    if (dsize.Tables[0].Rows.Count > 0)
                    {
                        chkSizes.DataSource = dsize.Tables[0];
                        chkSizes.DataTextField = "Size";
                        chkSizes.DataValueField = "Sizeid";
                        chkSizes.DataBind();
                    }
                }

                if ((dsize.Tables[0].Rows.Count > 0))
                {
                    //Select the checkboxlist items those values are true in database
                    //Loop through the DataTable
                    for (int i = 0; i <= dbrandcheck.Tables[0].Rows.Count - 1; i++)
                    {
                        //You need to change this as per your DB Design
                        string size = dbrandcheck.Tables[0].Rows[i]["Sizeid2"].ToString();

                        {
                            //Find the checkbox list items using FindByValue and select it.
                            chkSizes.Items.FindByValue(dbrandcheck.Tables[0].Rows[i]["Sizeid2"].ToString()).Selected = true;
                        }

                    }

                }
            }
            ddlUnit.Focus();
        }

        protected void Change_TotalFull(object sender, EventArgs e)
        {
            int totalQty = Convert.ToInt32(txtfull.Text) + Convert.ToInt32(txtHalf.Text);
            txtTotalQantity.Text = Convert.ToString(totalQty);
            txtHalf.Focus();
        }

        protected void Change_TotalHalf(object sender, EventArgs e)
        {
            int totalQty = Convert.ToInt32(txtfull.Text) + Convert.ToInt32(txtHalf.Text);
            txtTotalQantity.Text = Convert.ToString(totalQty);
            CheckEmbroiding.Focus();
        }

        protected void btn_Print(object sender, EventArgs e)
        {
            string iLotNo = Request.QueryString.Get("LotNo");
            DataSet dsLotNo = ds = objbs.SelectStitchingInfoDetEdit(Convert.ToInt32(iLotNo));
            string CutID = string.Empty;
            if (dsLotNo.Tables[0].Rows.Count > 0)
            {
                CutID = dsLotNo.Tables[0].Rows[0]["CutID"].ToString();
            }
            Response.Redirect("StitchingDetails_Print.aspx?CutID=" + CutID);
        }


        protected void GV_Size_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }


        private void FirstGridViewRow_Size()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
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

            ViewState["CurrentTable2"] = dtt;

            gvcustomerorder.DataSource = dtt;
            gvcustomerorder.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

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
            drNew["36FS"] = "";
            drNew["36HS"] = "";
            drNew["38FS"] = "";
            drNew["38HS"] = "";
            drNew["39FS"] = "";
            drNew["39HS"] = "";
            drNew["40FS"] = "";
            drNew["40HS"] = "";
            drNew["42FS"] = "";
            drNew["42HS"] = "";
            drNew["44FS"] = "";
            drNew["44HS"] = "";


            dstd.Tables[0].Rows.Add(drNew);

            GV_Size.DataSource = dstd;
            GV_Size.DataBind();

        }
        protected void GV_Size_RowCreated(object sender, GridViewRowEventArgs e)
        {
          
        }



        protected void txt36FS_TextChanged(object sender, EventArgs e)
        {
            Total_FullQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_38FS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt38FS");
            txt_38FS.Focus();


        }

        protected void txt38FS_TextChanged(object sender, EventArgs e)
        {
            Total_FullQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_39FS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt39FS");
            txt_39FS.Focus();
        }


        protected void txt39FS_TextChanged(object sender, EventArgs e)
        {
            Total_FullQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_40FS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt40FS");
            txt_40FS.Focus();

        }

       

        protected void txt40FS_TextChanged(object sender, EventArgs e)
        {
            Total_FullQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_42FS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt42FS");
            txt_42FS.Focus();


        }

        protected void txt42FS_TextChanged(object sender, EventArgs e)
        {
            Total_FullQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_44FS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt44FS");
            txt_44FS.Focus();

        }
        protected void txt44FS_TextChanged(object sender, EventArgs e)
        {


            Total_FullQty();
            
        }



        protected void txt36HS_TextChanged(object sender, EventArgs e)
        {
            Total_HalfQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_38HS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt38HS");
            txt_38HS.Focus();
        }


        protected void txt38HS_TextChanged(object sender, EventArgs e)
        {
            Total_HalfQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_39HS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt39HS");
            txt_39HS.Focus();


        }
        protected void txt39HS_TextChanged(object sender, EventArgs e)
        {
            Total_HalfQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_40HS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt40HS");
            txt_40HS.Focus();

        }



        protected void txt40HS_TextChanged(object sender, EventArgs e)
        {
            Total_HalfQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_42HS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt42HS");
            txt_42HS.Focus();


        }

     

        protected void txt42HS_TextChanged(object sender, EventArgs e)
        {

            Total_HalfQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_44HS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt44HS");
            txt_44HS.Focus();

        }

       

        protected void txt44HS_TextChanged(object sender, EventArgs e)
        {

            Total_HalfQty();
            TextBox tbox = (TextBox)sender;
            GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            int rowindex = row.RowIndex;
            TextBox txt_36FS = (TextBox)GV_Size.Rows[rowindex].Cells[1].FindControl("txt36FS");
            txt_36FS.Focus();
        }

        protected void Total_FullQty()
        {

            int FSTotal = 0;


            //TextBox tbox = (TextBox)sender;
            //GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            //int rowindex = row.RowIndex;
            //TextBox txt_36FS = (TextBox)GV_Size.Rows[vLoop].Cells[1].FindControl("txt36FS");

            for (int vLoop = 0; vLoop < GV_Size.Rows.Count; vLoop++)
            {

                TextBox txt_36FS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt36FS");
                TextBox txt_38FS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt38FS");
                TextBox txt_39FS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt39FS");
                TextBox txt_40FS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt40FS");
                TextBox txt_42FS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt42FS");
                TextBox txt_44FS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt44FS");




                if (txt_36FS.Text != "" && txt_38FS.Text != "" && txt_39FS.Text != "" && txt_40FS.Text != "" && txt_42FS.Text != "" && txt_44FS.Text != "")
                {
                    FSTotal = FSTotal + (Convert.ToInt32(txt_36FS.Text) + Convert.ToInt32(txt_38FS.Text) + Convert.ToInt32(txt_39FS.Text) + Convert.ToInt32(txt_40FS.Text) + Convert.ToInt32(txt_42FS.Text) + Convert.ToInt32(txt_44FS.Text));
                }
            }

            txtfull.Text = Convert.ToString(FSTotal);
            Total_Qty();
        }

        protected void Total_HalfQty()
        {

            int HSTotal = 0;


            //TextBox tbox = (TextBox)sender;
            //GridViewRow row = (GridViewRow)tbox.Parent.Parent;
            //int rowindex = row.RowIndex;

            for (int vLoop = 0; vLoop < GV_Size.Rows.Count; vLoop++)
            {

                TextBox txt_36HS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt36HS");
                TextBox txt_38HS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt38HS");
                TextBox txt_39HS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt39HS");
                TextBox txt_40HS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt40HS");
                TextBox txt_42HS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt42HS");
                TextBox txt_44HS = (TextBox)GV_Size.Rows[vLoop].FindControl("txt44HS");


                if (txt_36HS.Text != "" && txt_38HS.Text != "" && txt_39HS.Text != "" && txt_40HS.Text != "" && txt_42HS.Text != "" && txt_44HS.Text != "")
                {
                    HSTotal = HSTotal + (Convert.ToInt32(txt_36HS.Text) + Convert.ToInt32(txt_38HS.Text) + Convert.ToInt32(txt_39HS.Text) + Convert.ToInt32(txt_40HS.Text) + Convert.ToInt32(txt_42HS.Text) + Convert.ToInt32(txt_44HS.Text));
                }

            }
            txtHalf.Text = Convert.ToString(HSTotal);

            Total_Qty();
        }

        protected void Total_Qty()
        {
            int totalQty = Convert.ToInt32(txtfull.Text) + Convert.ToInt32(txtHalf.Text);
            txtTotalQantity.Text = Convert.ToString(totalQty);
            txtHalf.Focus();

        }







    }
}