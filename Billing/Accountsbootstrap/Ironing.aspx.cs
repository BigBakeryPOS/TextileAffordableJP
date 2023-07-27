
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
    public partial class Ironing : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        string empid = "";
        int Companyid = 0;
        double grandtotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            string Ironingid = string.Empty;
            string Requests = string.Empty;
            Ironingid = Request.QueryString.Get("Ironingid");
            ViewState["Ironingid"] = Ironingid;
            Requests = Request.QueryString.Get("name");
            empid = Session["Empid"].ToString();

            Companyid = Convert.ToInt32(Request.QueryString.Get("Companyid"));
            if (!IsPostBack)
            {






                string super = Session["IsSuperAdmin"].ToString();


                DataSet dsize = objbs.Getsizetype();
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


                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objbs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "Select Branch");
                    }
                }
                else
                {

                    drpbranch.Enabled = false;
                    DataSet dbraqnch = objbs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();
                        company_SelectedIndexChnaged(sender, e);
                    }
                }

                DataSet dsironmaterial = objbs.Getironmaterial(drpbranch.SelectedValue);
                gvironstock.DataSource = dsironmaterial;
                gvironstock.DataBind();

                DataSet dsjob = objbs.getjobcardlist();
                DropDownList1.DataSource = dsjob;
                DropDownList1.DataTextField = "ledgerName";
                DropDownList1.DataValueField = "ledgerId";
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, "Select jobwork Name");

                //MultiUnit_SelectedIndex(sender, e);
                DataSet drpEmpp = objbs.Selectname("9");
                ////// DataSet drpEmpp1 = objbs.getledgerforpayiron();
                drpMultiemployee.DataSource = drpEmpp;
                drpMultiemployee.DataTextField = "ledgerName";
                drpMultiemployee.DataValueField = "ledgerId";
                drpMultiemployee.DataBind();
                drpMultiemployee.Items.Insert(0, "Select Name");
                divWork.Visible = false;
                if (Ironingid != null)
                {
                    idType.Visible = true;
                    idConsiderStock.Visible = true;
                    drpbranch.Enabled = false;


                    if (Requests == "Receive")
                    {
                        #region Receive

                        DataSet dgetcheck = objbs.getJpIroningLot(Ironingid);

                        if (dgetcheck.Tables[0].Rows.Count > 0)
                        {
                            txtitemnarration.Text = dgetcheck.Tables[0].Rows[0]["ItemNarrations"].ToString();

                            if (Companyid != 0)
                            {
                                drpbranch.SelectedValue = Companyid.ToString();
                            }
                            DataSet dsLot = objbs.Select_Lot("Iron", drpbranch.SelectedValue);
                            if (dsLot.Tables[0].Rows.Count > 0)
                            {
                                ddlLotNo.DataSource = dsLot.Tables[0];
                                //  ddlLotNo.DataTextField = "LotNo";
                                ddlLotNo.DataTextField = "CompanyLotNo";
                                ddlLotNo.DataValueField = "LotDetailsID";
                                ddlLotNo.DataBind();
                                ddlLotNo.Items.Insert(0, "Select Lot No");
                            } DataSet dlotprocess = new DataSet();
                            dlotprocess = objbs.Get_LotDetails(Convert.ToString(dgetcheck.Tables[0].Rows[0]["LotDetailID"]), "6", "Iron");

                            if (dlotprocess.Tables[0].Rows.Count > 0)
                            {
                                ironmaterial.Visible = true;

                                #region General lot

                                txttotalqty.Text = dlotprocess.Tables[0].Rows[0]["TotalQuantity"].ToString();
                                lblbrandnid.Text = dlotprocess.Tables[0].Rows[0]["brandid"].ToString();
                                lblfitid.Text = dlotprocess.Tables[0].Rows[0]["fitid"].ToString();

                                DataSet dsizee = objbs.Getfitseize(lblfitid.Text, lblbrandnid.Text);
                                if ((dsizee.Tables[0].Rows.Count > 0))
                                {

                                    // for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                                    for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                                    {
                                        //You need to change this as per your DB Design
                                        string size = dsizee.Tables[0].Rows[i]["Sizeid1"].ToString();
                                        {
                                            //Find the checkbox list items using FindByValue and select it.
                                            chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[i]["Sizeid1"].ToString()).Selected = true;
                                        }

                                    }
                                }

                                if (dlotprocess.Tables[0].Rows[0]["Jobwork"].ToString() == "Out")
                                {
                                    DataSet drpEmpp12 = objbs.Selectname("10");
                                    drpMultiemployee.DataSource = drpEmpp12;
                                    drpMultiemployee.DataTextField = "LedgerName";
                                    drpMultiemployee.DataValueField = "LedgerID";
                                    drpMultiemployee.DataBind();
                                    drpMultiemployee.Items.Insert(0, "Select Name");
                                    //emp.Visible = false;
                                    //job.Visible = true;
                                }
                                else if (dlotprocess.Tables[0].Rows[0]["Jobwork"].ToString() == "In")
                                {
                                    DataSet drpEmpp12 = objbs.Selectname("9");
                                    ////// DataSet drpEmpp1 = objbs.getledgerforpayiron();
                                    drpMultiemployee.DataSource = drpEmpp12;
                                    drpMultiemployee.DataTextField = "LedgerName";
                                    drpMultiemployee.DataValueField = "LedgerID";
                                    drpMultiemployee.DataBind();
                                    drpMultiemployee.Items.Insert(0, "Select Name");
                                    //emp.Visible = true;
                                    //job.Visible = false;
                                }
                                btnadd.Visible = true;
                                btnadd.Text = "Received";
                                txtid.Text = dgetcheck.Tables[0].Rows[0]["Ironingid"].ToString();
                                txtmultiid.Text = dgetcheck.Tables[0].Rows[0]["Ironingid"].ToString();
                                txtmultiplecode.Text = dgetcheck.Tables[0].Rows[0]["Ironingid"].ToString();
                                txtmultiplecode.Enabled = false;
                                ddlLotNo.SelectedValue = dgetcheck.Tables[0].Rows[0]["LotDetailID"].ToString();
                                ddlLotNo.Enabled = false;
                                DataSet getitem = objbs.getitenameforprocess(ddlLotNo.SelectedItem.Text, "tbljpstiching", "R");
                                if (getitem.Tables[0].Rows.Count > 0)
                                {
                                    lblitemname.Text = getitem.Tables[0].Rows[0]["ItemName"].ToString() + '(' + getitem.Tables[0].Rows[0]["Itemcode"].ToString() + ')';
                                }
                                lbllotno.Text = dgetcheck.Tables[0].Rows[0]["LotNo"].ToString();
                                drpMultiemployee.SelectedValue = dgetcheck.Tables[0].Rows[0]["WORKERID"].ToString();
                                drpMultiemployee.Enabled = true;
                                txtmultidate.Text = Convert.ToDateTime(dgetcheck.Tables[0].Rows[0]["date"]).ToString("dd/MM/yyyy");
                                txtmultidate.Enabled = false;
                                txttotalqty.Text = dgetcheck.Tables[0].Rows[0]["Totalqty"].ToString();
                                txttotalqty.Enabled = false;
                                txtAmount.Text = dgetcheck.Tables[0].Rows[0]["TotalAmount"].ToString();
                                txtAmount.Enabled = false;
                                DataSet ds = objbs.Get_JpIroning(Convert.ToString(Ironingid), "6", "Iron");

                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    #region new

                                    DataSet temp = new DataSet();
                                    DataTable dtt = new DataTable();
                                    dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("senddate", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("recdate", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("Fitid", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("Patternid", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("ProcessTypeID", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("rate", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("damageqty", typeof(string)));

                                    dtt.Columns.Add(new DataColumn("Designno", typeof(string)));

                                    dtt.Columns.Add(new DataColumn("Color", typeof(string)));

                                    dtt.Columns.Add(new DataColumn("IronTransID", typeof(string)));

                                    //   temp.Tables.Add(dtt);
                                    dtt.Columns.Add(new DataColumn("30hs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("32hs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("34hs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("36hs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("30fs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("32fs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("34fs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("36fs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("shs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("mhs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("lhs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("lfs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("mfs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("sfs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("xlfs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("xxlfs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("3xlfs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("4xlfs", typeof(string)));

                                    dtt.Columns.Add(new DataColumn("xsfs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("xshs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("xlhs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("xxlhs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("3xlhs", typeof(string)));
                                    dtt.Columns.Add(new DataColumn("4xlhs", typeof(string)));

                                    dtt.Columns.Add(new DataColumn("StockRatioId", typeof(string)));
                                    temp.Tables.Add(dtt);


                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        //if (Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"].ToString()) != 0)
                                        {
                                            DataRow dr = dtt.NewRow();

                                            dr["OrderNo"] = "0";
                                            dr["fitid"] = ds.Tables[0].Rows[i]["fitid"].ToString();
                                            ////// dr["fit"] = ds.Tables[0].Rows[i]["Fitname"].ToString();
                                            dr["fit"] = ds.Tables[0].Rows[i]["Fit"].ToString();
                                            dr["ProcessTypeID"] = "5";
                                            dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                                            dr["TotalQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["totalQty"]);
                                            dr["RemainQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"]);
                                            dr["senddate"] = ds.Tables[0].Rows[i]["senddate"].ToString();
                                            dr["RecQty"] = "0";
                                            dr["Recdate"] = "0";
                                            dr["Damageqty"] = "0";
                                            dr["patternid"] = ds.Tables[0].Rows[i]["patternid"].ToString();
                                            dr["pattern"] = ds.Tables[0].Rows[i]["patternname"].ToString();
                                            dr["itemname"] = ds.Tables[0].Rows[i]["itemname"].ToString();

                                            dr["Designno"] = ds.Tables[0].Rows[i]["DesignCode"].ToString();

                                            dr["Color"] = ds.Tables[0].Rows[i]["Onlycolor"].ToString();

                                            dr["IronTransID"] = ds.Tables[0].Rows[i]["TransID"].ToString();

                                            dr["30fs"] = ds.Tables[0].Rows[i]["Rec30fs"].ToString();
                                            dr["30hs"] = ds.Tables[0].Rows[i]["Rec30hs"].ToString();
                                            dr["32fs"] = ds.Tables[0].Rows[i]["Rec32fs"].ToString();
                                            dr["32hs"] = ds.Tables[0].Rows[i]["Rec32hs"].ToString();
                                            dr["34hs"] = ds.Tables[0].Rows[i]["Rec34hs"].ToString();
                                            dr["34fs"] = ds.Tables[0].Rows[i]["Rec34fs"].ToString();
                                            dr["36hs"] = ds.Tables[0].Rows[i]["Rec36hs"].ToString();
                                            dr["36fs"] = ds.Tables[0].Rows[i]["Rec36fs"].ToString();

                                            dr["xshs"] = ds.Tables[0].Rows[i]["Recxshs"].ToString();
                                            dr["xsfs"] = ds.Tables[0].Rows[i]["Recxsfs"].ToString();
                                            dr["shs"] = ds.Tables[0].Rows[i]["Recshs"].ToString();
                                            dr["sfs"] = ds.Tables[0].Rows[i]["Recsfs"].ToString();
                                            dr["mhs"] = ds.Tables[0].Rows[i]["Recmhs"].ToString();
                                            dr["mfs"] = ds.Tables[0].Rows[i]["Recmfs"].ToString();
                                            dr["lhs"] = ds.Tables[0].Rows[i]["Reclhs"].ToString();
                                            dr["lfs"] = ds.Tables[0].Rows[i]["Reclfs"].ToString();
                                            dr["xxlhs"] = ds.Tables[0].Rows[i]["Recxxlhs"].ToString();
                                            dr["xxlfs"] = ds.Tables[0].Rows[i]["Recxxlfs"].ToString();

                                            dr["xlhs"] = ds.Tables[0].Rows[i]["Recxlhs"].ToString();
                                            dr["xlfs"] = ds.Tables[0].Rows[i]["Recxlfs"].ToString();
                                            dr["3xlhs"] = ds.Tables[0].Rows[i]["Rec3xlhs"].ToString();
                                            dr["3xlfs"] = ds.Tables[0].Rows[i]["Rec3xlfs"].ToString();
                                            dr["4xlhs"] = ds.Tables[0].Rows[i]["Rec4xlhs"].ToString();
                                            dr["4xlfs"] = ds.Tables[0].Rows[i]["Rec4xlfs"].ToString();

                                            dr["StockRatioId"] = ds.Tables[0].Rows[i]["StockRatioId"].ToString();
                                            temp.Tables[0].Rows.Add(dr);
                                        }
                                    }

                                    ViewState["CurrentTable1"] = dtt;

                                    gvcustomerorder.DataSource = temp;
                                    gvcustomerorder.DataBind();



                                    if (chkSizes.SelectedIndex >= 0)
                                    {
                                        gvcustomerorder.Columns[7].Visible = false; //30FS
                                        gvcustomerorder.Columns[8].Visible = false; //32FS

                                        gvcustomerorder.Columns[9].Visible = false;//34Fs
                                        gvcustomerorder.Columns[10].Visible = false;//36Fs

                                        gvcustomerorder.Columns[11].Visible = false; //XSFS
                                        gvcustomerorder.Columns[12].Visible = false; //SFS

                                        gvcustomerorder.Columns[13].Visible = false; //MFS
                                        gvcustomerorder.Columns[14].Visible = false; //LFS

                                        gvcustomerorder.Columns[15].Visible = false; //XLFS
                                        gvcustomerorder.Columns[16].Visible = false; //XXLFS

                                        gvcustomerorder.Columns[17].Visible = false; //3XLFS
                                        gvcustomerorder.Columns[18].Visible = false; //4XLFS

                                        gvcustomerorder.Columns[19].Visible = false; //30HS
                                        gvcustomerorder.Columns[20].Visible = false; //32HS

                                        gvcustomerorder.Columns[21].Visible = false; //34Hs
                                        gvcustomerorder.Columns[22].Visible = false; //36Hs

                                        gvcustomerorder.Columns[23].Visible = false; //XSHS
                                        gvcustomerorder.Columns[24].Visible = false; //SHS


                                        gvcustomerorder.Columns[25].Visible = false;  //MHS
                                        gvcustomerorder.Columns[26].Visible = false;  //LHS


                                        gvcustomerorder.Columns[27].Visible = false; //XLHS
                                        gvcustomerorder.Columns[28].Visible = false; //XXLHS

                                        gvcustomerorder.Columns[29].Visible = false; //3XLHS
                                        gvcustomerorder.Columns[30].Visible = false; //4XLHS



                                        int lop = 0;
                                        //Loop through each item of checkboxlist
                                        foreach (ListItem item in chkSizes.Items)
                                        {
                                            //check if item selected

                                            if (item.Selected)
                                            {

                                                {
                                                    if (item.Value == "1") // 30FS
                                                    {

                                                        gvcustomerorder.Columns[7].Visible = true;
                                                    }
                                                    if (item.Value == "3") // 32FS
                                                    {
                                                        gvcustomerorder.Columns[8].Visible = true;
                                                    }
                                                    if (item.Value == "5") //34FS
                                                    {
                                                        gvcustomerorder.Columns[9].Visible = true;
                                                    }
                                                    if (item.Value == "7") //36FS
                                                    {
                                                        gvcustomerorder.Columns[10].Visible = true;
                                                    }

                                                    if (item.Value == "9") //XSFS
                                                    {
                                                        gvcustomerorder.Columns[11].Visible = true;
                                                    }
                                                    if (item.Value == "21") //SFS
                                                    {
                                                        gvcustomerorder.Columns[12].Visible = true;
                                                    }
                                                    if (item.Value == "23") // MFS
                                                    {
                                                        gvcustomerorder.Columns[13].Visible = true;
                                                    }
                                                    if (item.Value == "11") // LFS
                                                    {
                                                        gvcustomerorder.Columns[14].Visible = true;
                                                    }
                                                    if (item.Value == "13") //XLFS
                                                    {
                                                        gvcustomerorder.Columns[15].Visible = true;
                                                    }
                                                    if (item.Value == "15") // XXLFS
                                                    {
                                                        gvcustomerorder.Columns[16].Visible = true;
                                                    }
                                                    if (item.Value == "17") //3XLFS
                                                    {
                                                        gvcustomerorder.Columns[17].Visible = true;
                                                    }
                                                    if (item.Value == "19") // 4XLFS
                                                    {
                                                        gvcustomerorder.Columns[18].Visible = true;
                                                    }





                                                    if (item.Value == "2") // 30HS
                                                    {
                                                        gvcustomerorder.Columns[19].Visible = true;
                                                    }

                                                    if (item.Value == "4") //32HS
                                                    {
                                                        gvcustomerorder.Columns[20].Visible = true;
                                                    }

                                                    if (item.Value == "6") //34HS
                                                    {
                                                        gvcustomerorder.Columns[21].Visible = true;
                                                    }

                                                    if (item.Value == "8") //36HS
                                                    {
                                                        gvcustomerorder.Columns[22].Visible = true;
                                                    }

                                                    if (item.Value == "10") //XSHS
                                                    {
                                                        gvcustomerorder.Columns[23].Visible = true;
                                                    }
                                                    if (item.Value == "22") //SHS
                                                    {
                                                        gvcustomerorder.Columns[24].Visible = true;
                                                    }

                                                    if (item.Value == "24") // MHS
                                                    {
                                                        gvcustomerorder.Columns[25].Visible = true;
                                                    }

                                                    if (item.Value == "12") // LHS
                                                    {
                                                        gvcustomerorder.Columns[26].Visible = true;
                                                    }

                                                    if (item.Value == "14") //XLHS
                                                    {
                                                        gvcustomerorder.Columns[27].Visible = true;
                                                    }

                                                    if (item.Value == "16") // XXLHS
                                                    {
                                                        gvcustomerorder.Columns[28].Visible = true;
                                                    }

                                                    if (item.Value == "18") // 3XLHS
                                                    {
                                                        gvcustomerorder.Columns[29].Visible = true;
                                                    }

                                                    if (item.Value == "20") // 4XLHS
                                                    {
                                                        gvcustomerorder.Columns[30].Visible = true;
                                                    }




                                                    lop++;

                                                }

                                            }
                                        }
                                        //gvcustomerorder.DataSource = dssmer;
                                        //gvcustomerorder.DataBind();
                                    }
                                    else
                                    {
                                        gvcustomerorder.Columns[7].Visible = false; //30FS
                                        gvcustomerorder.Columns[8].Visible = false; //32FS

                                        gvcustomerorder.Columns[9].Visible = false;//34Fs
                                        gvcustomerorder.Columns[10].Visible = false;//36Fs

                                        gvcustomerorder.Columns[11].Visible = false; //XSFS
                                        gvcustomerorder.Columns[12].Visible = false; //SFS

                                        gvcustomerorder.Columns[13].Visible = false; //MFS
                                        gvcustomerorder.Columns[14].Visible = false; //LFS

                                        gvcustomerorder.Columns[15].Visible = false; //XLFS
                                        gvcustomerorder.Columns[16].Visible = false; //XXLFS

                                        gvcustomerorder.Columns[17].Visible = false; //3XLFS
                                        gvcustomerorder.Columns[18].Visible = false; //4XLFS

                                        gvcustomerorder.Columns[19].Visible = false; //30HS
                                        gvcustomerorder.Columns[20].Visible = false; //32HS

                                        gvcustomerorder.Columns[21].Visible = false; //34Hs
                                        gvcustomerorder.Columns[22].Visible = false; //36Hs

                                        gvcustomerorder.Columns[23].Visible = false; //XSHS
                                        gvcustomerorder.Columns[24].Visible = false; //SHS


                                        gvcustomerorder.Columns[25].Visible = false;  //MHS
                                        gvcustomerorder.Columns[26].Visible = false;  //LHS


                                        gvcustomerorder.Columns[27].Visible = false; //XLHS
                                        gvcustomerorder.Columns[28].Visible = false; //XXLHS

                                        gvcustomerorder.Columns[29].Visible = false; //3XLHS
                                        gvcustomerorder.Columns[30].Visible = false; //4XLHS


                                    }


                                    DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");

                                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                    {
                                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                                        Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");
                                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                                        Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");

                                        Label lblonlycolor = (Label)gvcustomerorder.Rows[i].FindControl("lblonlycolor");

                                        TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                                        Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                                        Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                                        Label lbllfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                                        TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                                        TextBox date1 = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                                        TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");
                                        string recdate = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");
                                        string date11 = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");


                                        TextBox txts30fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fsac");
                                        TextBox txts30hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hsac");
                                        TextBox txts32fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fsac");
                                        TextBox txts32hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hsac");
                                        TextBox txts34fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fsac");
                                        TextBox txts34hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hsac");
                                        TextBox txts36fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fsac");
                                        TextBox txts36hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hsac");
                                        TextBox txtsxsfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfsac");
                                        TextBox txtsxshsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshsac");

                                        TextBox txtslfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfsac");
                                        TextBox txtslhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhsac");
                                        TextBox txtssfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfsac");
                                        TextBox txtsshsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshsac");
                                        TextBox txtsmfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfsac");
                                        TextBox txtsmhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhsac");
                                        TextBox txtsxlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfsac");
                                        TextBox txtsxlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhsac");
                                        TextBox txtsxxlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfsac");
                                        TextBox txtsxxlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhsac");
                                        TextBox txts3xlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfsac");
                                        TextBox txts3xlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhsac");
                                        TextBox txts4xlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfsac");
                                        TextBox txts4xlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhsac");



                                        TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                                        TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                                        TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                                        TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                                        TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                                        TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                                        TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                                        TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                                        TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                                        TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");

                                        TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                                        TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                                        TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                                        TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                                        TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                                        TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                                        TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                                        TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                                        TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                                        TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                                        TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                                        TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                                        TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                                        TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");

                                        Label lbldesignno = (Label)gvcustomerorder.Rows[i].FindControl("lbldesignno");
                                        Label lblIronTransID = (Label)gvcustomerorder.Rows[i].FindControl("lblIronTransID");
                                        Label lblStockRatioId = (Label)gvcustomerorder.Rows[i].FindControl("lblStockRatioId");

                                        lbldesignno.Text = temp.Tables[0].Rows[i]["Designno"].ToString();

                                        lblonlycolor.Text = temp.Tables[0].Rows[i]["Color"].ToString();

                                        lblIronTransID.Text = temp.Tables[0].Rows[i]["IronTransID"].ToString();

                                        lblStockRatioId.Text = temp.Tables[0].Rows[i]["StockRatioId"].ToString();

                                        txts30fsac.Text = temp.Tables[0].Rows[i]["30fs"].ToString();
                                        txts30hsac.Text = temp.Tables[0].Rows[i]["30hs"].ToString();
                                        txts32fsac.Text = temp.Tables[0].Rows[i]["32fs"].ToString();
                                        txts32hsac.Text = temp.Tables[0].Rows[i]["32hs"].ToString();
                                        txts34fsac.Text = temp.Tables[0].Rows[i]["34fs"].ToString();
                                        txts34hsac.Text = temp.Tables[0].Rows[i]["34hs"].ToString();
                                        txts36fsac.Text = temp.Tables[0].Rows[i]["36fs"].ToString();
                                        txts36hsac.Text = temp.Tables[0].Rows[i]["36hs"].ToString();
                                        txtsxsfsac.Text = temp.Tables[0].Rows[i]["xsfs"].ToString();
                                        txtsxshsac.Text = temp.Tables[0].Rows[i]["xshs"].ToString();
                                        txtssfsac.Text = temp.Tables[0].Rows[i]["sfs"].ToString();
                                        txtsshsac.Text = temp.Tables[0].Rows[i]["shs"].ToString();
                                        txtsmfsac.Text = temp.Tables[0].Rows[i]["mfs"].ToString();
                                        txtsmhsac.Text = temp.Tables[0].Rows[i]["mhs"].ToString();
                                        txtslfsac.Text = temp.Tables[0].Rows[i]["lfs"].ToString();
                                        txtslhsac.Text = temp.Tables[0].Rows[i]["lhs"].ToString();
                                        txtsxlfsac.Text = temp.Tables[0].Rows[i]["xlfs"].ToString();
                                        txtsxlhsac.Text = temp.Tables[0].Rows[i]["xlhs"].ToString();
                                        txtsxxlfsac.Text = temp.Tables[0].Rows[i]["xxlfs"].ToString();
                                        txtsxxlhsac.Text = temp.Tables[0].Rows[i]["xxlhs"].ToString();
                                        txts3xlfsac.Text = temp.Tables[0].Rows[i]["3xlfs"].ToString();
                                        txts3xlhsac.Text = temp.Tables[0].Rows[i]["3xlhs"].ToString();
                                        txts4xlfsac.Text = temp.Tables[0].Rows[i]["4xlfs"].ToString();
                                        txts4xlhsac.Text = temp.Tables[0].Rows[i]["4xlhs"].ToString();

                                        txts30fs.Text = temp.Tables[0].Rows[i]["30fs"].ToString();
                                        txts30hs.Text = temp.Tables[0].Rows[i]["30hs"].ToString();
                                        txts32fs.Text = temp.Tables[0].Rows[i]["32fs"].ToString();
                                        txts32hs.Text = temp.Tables[0].Rows[i]["32hs"].ToString();
                                        txts34fs.Text = temp.Tables[0].Rows[i]["34fs"].ToString();
                                        txts34hs.Text = temp.Tables[0].Rows[i]["34hs"].ToString();
                                        txts36fs.Text = temp.Tables[0].Rows[i]["36fs"].ToString();
                                        txts36hs.Text = temp.Tables[0].Rows[i]["36hs"].ToString();
                                        txtsxsfs.Text = temp.Tables[0].Rows[i]["xsfs"].ToString();
                                        txtsxshs.Text = temp.Tables[0].Rows[i]["xshs"].ToString();
                                        txtssfs.Text = temp.Tables[0].Rows[i]["sfs"].ToString();
                                        txtsshs.Text = temp.Tables[0].Rows[i]["shs"].ToString();
                                        txtsmfs.Text = temp.Tables[0].Rows[i]["mfs"].ToString();
                                        txtsmhs.Text = temp.Tables[0].Rows[i]["mhs"].ToString();
                                        txtslfs.Text = temp.Tables[0].Rows[i]["lfs"].ToString();
                                        txtslhs.Text = temp.Tables[0].Rows[i]["lhs"].ToString();
                                        txtsxlfs.Text = temp.Tables[0].Rows[i]["xlfs"].ToString();
                                        txtsxlhs.Text = temp.Tables[0].Rows[i]["xlhs"].ToString();
                                        txtsxxlfs.Text = temp.Tables[0].Rows[i]["xxlfs"].ToString();
                                        txtsxxlhs.Text = temp.Tables[0].Rows[i]["xxlhs"].ToString();
                                        txts3xlfs.Text = temp.Tables[0].Rows[i]["3xlfs"].ToString();
                                        txts3xlhs.Text = temp.Tables[0].Rows[i]["3xlhs"].ToString();
                                        txts4xlfs.Text = temp.Tables[0].Rows[i]["4xlfs"].ToString();
                                        txts4xlhs.Text = temp.Tables[0].Rows[i]["4xlhs"].ToString();

                                        //txts30fs.Text = "0";
                                        //txts30hs.Text = "0";
                                        //txts32fs.Text = "0";
                                        //txts32hs.Text = "0";
                                        //txts34fs.Text = "0";
                                        //txts34hs.Text = "0";
                                        //txts36fs.Text = "0";
                                        //txts36hs.Text = "0";
                                        //txtsxsfs.Text = "0";
                                        //txtsxshs.Text = "0";
                                        //txtssfs.Text = "0";
                                        //txtsshs.Text = "0";
                                        //txtsmfs.Text = "0";
                                        //txtsmhs.Text = "0";
                                        //txtslfs.Text = "0";
                                        //txtslhs.Text = "0";
                                        //txtsxlfs.Text = "0";
                                        //txtsxlhs.Text = "0";
                                        //txtsxxlfs.Text = "0";
                                        //txtsxxlhs.Text = "0";
                                        //txts3xlfs.Text = "0";
                                        //txts3xlhs.Text = "0";
                                        //txts4xlfs.Text = "0";
                                        //txts4xlhs.Text = "0";

                                        //Recdate.Enabled = false;
                                        txtrecFQty.Enabled = true;
                                        //drpLotno.Enabled = false;
                                        drpProcess.Enabled = false;
                                        txtsendFQty.Enabled = false;
                                        txtdamageqty.Enabled = true;
                                        // Recdate.Enabled = false;
                                        date1.Enabled = false;

                                        txtsendFQty.Text = temp.Tables[0].Rows[i]["TotalQty"].ToString();
                                        txtRemainQty.Text = temp.Tables[0].Rows[i]["RemainQty"].ToString();
                                        lblfit.Text = temp.Tables[0].Rows[i]["fit"].ToString();
                                        lblitemname.Text = temp.Tables[0].Rows[i]["itemname"].ToString();
                                        lbllfitid.Text = temp.Tables[0].Rows[i]["fitid"].ToString();
                                        txtrate.Text = temp.Tables[0].Rows[i]["rate"].ToString();
                                        drpProcess.SelectedValue = temp.Tables[0].Rows[i]["ProcessTypeID"].ToString();

                                        //DropDownList drplotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");

                                        //if (drplotprocess.SelectedItem.Text != "Select Lot No")
                                        //{

                                        //    DataSet drpProcesss = objbs.SelectProcessTypeLotProcess(Convert.ToInt32(drplotprocess.SelectedValue));
                                        //    if (drpProcesss.Tables[0].Rows.Count > 0)
                                        //    {
                                        //        drpProcess.Items.Clear();
                                        //        drpProcess.DataSource = drpProcesss;
                                        //        drpProcess.DataTextField = "ProcessType";
                                        //        drpProcess.DataValueField = "ProcessMasterID";
                                        //        drpProcess.DataBind();
                                        //        drpProcess.Items.Insert(0, "Select Process Type");

                                        //    }
                                        //    else
                                        //    {
                                        //        drpProcess.Items.Clear();
                                        //        drpProcess.Items.Insert(0, "Select Process Type");
                                        //    }
                                        //}
                                        lblPattern.Text = temp.Tables[0].Rows[i]["Pattern"].ToString();
                                        lblPatternid.Text = temp.Tables[0].Rows[i]["Patternid"].ToString();
                                        txtdamageqty.Text = temp.Tables[0].Rows[i]["damageqty"].ToString();
                                        txtrecFQty.Text = temp.Tables[0].Rows[i]["recqty"].ToString();
                                        date1.Text = Convert.ToDateTime(temp.Tables[0].Rows[i]["senddate"]).ToString("dd/MM/yyyy");
                                        Recdate.Text = recdate.ToString();
                                    }
                                    #endregion


                                    #region old

                                    //DataSet temp = new DataSet();
                                    //DataTable dtt = new DataTable();
                                    //dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("senddate", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("recdate", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("Fitid", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("Patternid", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("ProcessTypeID", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("rate", typeof(string)));
                                    //dtt.Columns.Add(new DataColumn("damageqty", typeof(string)));
                                    //temp.Tables.Add(dtt);



                                    //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    //{
                                    //    if (Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"].ToString()) != 0)
                                    //    {
                                    //        DataRow dr = dtt.NewRow();

                                    //        dr["OrderNo"] = "0";
                                    //        dr["fitid"] = ds.Tables[0].Rows[i]["fitid"].ToString();
                                    //        dr["fit"] = ds.Tables[0].Rows[i]["fit"].ToString();
                                    //        dr["ProcessTypeID"] = ds.Tables[0].Rows[i]["ProcessID"].ToString();
                                    //        dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                                    //        dr["TotalQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["totalQty"]);
                                    //        dr["RemainQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"]);
                                    //        dr["senddate"] = ds.Tables[0].Rows[i]["senddate"].ToString();
                                    //        dr["RecQty"] = "0";
                                    //        dr["Recdate"] = "0";
                                    //        dr["Damageqty"] = "0";
                                    //        dr["patternid"] = ds.Tables[0].Rows[i]["patternid"].ToString();
                                    //        dr["pattern"] = ds.Tables[0].Rows[i]["patternname"].ToString();
                                    //        dr["itemname"] = ds.Tables[0].Rows[i]["itemname"].ToString();
                                    //        temp.Tables[0].Rows.Add(dr);
                                    //    }
                                    //}

                                    //ViewState["CurrentTable1"] = dtt;

                                    //gvcustomerorder.DataSource = temp;
                                    //gvcustomerorder.DataBind();

                                    //DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");

                                    //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                                    //{
                                    //    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                                    //    Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");
                                    //    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                                    //    Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                                    //    TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                                    //    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                                    //    Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                                    //    Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                                    //    Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                                    //    TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                                    //    TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                                    //    TextBox date1 = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                                    //    TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");
                                    //    string recdate = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;
                                    //    string date11 = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;



                                    //    //Recdate.Enabled = false;
                                    //    txtrecFQty.Enabled = true;
                                    //    //drpLotno.Enabled = false;
                                    //    drpProcess.Enabled = false;
                                    //    txtsendFQty.Enabled = false;
                                    //    txtdamageqty.Enabled = true;
                                    //    Recdate.Enabled = false;
                                    //    date1.Enabled = false;

                                    //    txtsendFQty.Text = temp.Tables[0].Rows[i]["TotalQty"].ToString();
                                    //    txtRemainQty.Text = temp.Tables[0].Rows[i]["RemainQty"].ToString();
                                    //    lblfit.Text = temp.Tables[0].Rows[i]["fit"].ToString();
                                    //    lblitemname.Text = temp.Tables[0].Rows[i]["itemname"].ToString();
                                    //    lblfitid.Text = temp.Tables[0].Rows[i]["fitid"].ToString();
                                    //    txtrate.Text = temp.Tables[0].Rows[i]["rate"].ToString();
                                    //    drpProcess.SelectedValue = temp.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                    //    lblPattern.Text = temp.Tables[0].Rows[i]["Pattern"].ToString();
                                    //    lblPatternid.Text = temp.Tables[0].Rows[i]["Patternid"].ToString();
                                    //    txtdamageqty.Text = temp.Tables[0].Rows[i]["damageqty"].ToString();
                                    //    txtrecFQty.Text = temp.Tables[0].Rows[i]["recqty"].ToString();
                                    //    date1.Text = Convert.ToDateTime(temp.Tables[0].Rows[i]["senddate"]).ToString("dd/MM/yyyy");
                                    //    Recdate.Text = recdate.ToString();
                                    //}

                                    #endregion
                                }

                                else
                                {
                                    FirstGridViewRow();

                                }


                                ddlLotNo.Enabled = false;

                                #endregion
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Something Went Wrong Please Contact Administrator.While fetching Lot details. Thank you!!');", true);
                                return;
                            }

                        }


                        DataSet dsbrand = objbs.getbrand(ddlLotNo.SelectedItem.Text);
                        if (dsbrand.Tables[0].Rows.Count > 0)
                        {
                            lblbrand.Text = dsbrand.Tables[0].Rows[0]["BrandName"].ToString();
                        }

                        #endregion
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Something Went Wrong.Please Check Lot No.Or Contact Administrator.')", true);
                        return;
                    }
                }
                else
                {


                }

            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);

        }

        protected void company_SelectedIndexChnaged(object sender, EventArgs e)
        {
            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {

                //DataSet dsLotNo = objbs.Select_LotNo_For(drpbranch.SelectedValue);    // 7-Checking 
                //if (dsLotNo.Tables[0].Rows.Count > 0)
                //{
                //    ddlLotNo.DataSource = dsLotNo.Tables[0];
                //    ddlLotNo.DataTextField = "CompanyLotNo";
                //    ddlLotNo.DataValueField = "lotdetailid";
                //    ddlLotNo.DataBind();
                //    ddlLotNo.Items.Insert(0, "Select Lot No");
                //}

                DataSet dsLotNo = objbs.Get_LotDetailiron(drpbranch.SelectedValue);
                if (dsLotNo.Tables[0].Rows.Count > 0)
                {
                    ddlLotNo.DataSource = dsLotNo.Tables[0];
                    ddlLotNo.DataTextField = "CompanyLotNo";
                    ddlLotNo.DataValueField = "CompanyLotNo";
                    ddlLotNo.DataBind();
                    ddlLotNo.Items.Insert(0, "Select Lot No");
                    //lbllotno.Text = dsLotNo.Tables[0].Rows[0]["LotNo"].ToString();
                }




            }
        }

        protected void Add1_Click(object sender, EventArgs e)
        {
            double tot = 0;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                Label lblfit = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblfit");
                TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                Label lblitemname = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblitemname");
                TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRemainQty");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrate");
                Label lblPattern = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblPattern");
                Label lblPatternid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblPatternid");
                Label lblfitid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblfitid");
                TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdamageqty");
                TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrecFQty");
                TextBox date1 = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Recdate");
                //DateTime recdate = DateTime.ParseExact(Recdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime date11 = DateTime.ParseExact(date1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                tot = tot + (Convert.ToDouble(txtrate.Text) * Convert.ToDouble(txtsendFQty.Text));
            }
            txtAmount.Text = tot.ToString();
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

                            if (process.SelectedValue == temp && bun == tempbun)
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
        protected void Recdate_OnTextChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                TextBox Recdateorg = (TextBox)gvcustomerorder.Rows[0].FindControl("Recdate");
                TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");

                Recdate.Text = Recdateorg.Text;
            }
        }

        protected void txtrecfqtychnaged_text(object sender, EventArgs e)
        {
            //double recqty = 0;
            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    DropDownList drpLotprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpLotno");
            //    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
            //    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
            //    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");

            //    recqty = recqty + Convert.ToDouble(txtrecQty.Text);

            //    if (Convert.ToDouble(txtsendFQty.Text) < Convert.ToDouble(txtrecQty.Text))
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Received Qty is not Greater than That Remain Qty!!!')", true);
            //        txtrecQty.Focus();
            //        return;
            //    }

            //}

            //txtreceivedQty.Text = recqty.ToString();

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


        protected void checkedinoutproceschnaged(object sender, EventArgs e)
        {

            DataSet dcheckmanulaornor = objbs.getalllotalreadyexistsornot(drpbranch.SelectedValue);
            if (dcheckmanulaornor.Tables[0].Rows.Count > 0)
            {


                if (checkinoutprocess.SelectedValue == "Out")
                {
                    DataSet drpEmpp = objbs.Selectname("10");
                    drpMultiemployee.DataSource = drpEmpp;
                    drpMultiemployee.DataTextField = "LedgerName";
                    drpMultiemployee.DataValueField = "LedgerID";
                    drpMultiemployee.DataBind();
                    drpMultiemployee.Items.Insert(0, "Select Name");
                    //emp.Visible = false;
                    //job.Visible = true;
                }
                else if (checkinoutprocess.SelectedValue == "In")
                {
                    DataSet drpEmpp = objbs.Selectname("9");
                    ////// DataSet drpEmpp1 = objbs.getledgerforpayiron();
                    drpMultiemployee.DataSource = drpEmpp;
                    drpMultiemployee.DataTextField = "LedgerName";
                    drpMultiemployee.DataValueField = "LedgerID";
                    drpMultiemployee.DataBind();
                    drpMultiemployee.Items.Insert(0, "Select Name");
                    //emp.Visible = true;
                    //job.Visible = false;
                }


                DataSet dscheck = objbs.getlotalreadyexistsornot(drpbranch.SelectedValue);
                if (dscheck.Tables[0].Rows.Count > 0)
                {
                    txttotalqty.Text = dscheck.Tables[0].Rows[0]["Totalshirt"].ToString();

                }
            }
            else
            {

            }


        }
        protected void sizesetting(object sender, EventArgs e)
        {











        }

        protected void EmployeeRate_chnaged(object sender, EventArgs e)
        {
            if (ddlLotNo.SelectedValue != "Select Lot No")
            {
                double Ironrate = 0;

                if (drpMultiemployee.SelectedValue == "Select Name")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Jobworker/Employee Name For this " + ddlLotNo.SelectedItem.Text + ". Thank you');", true);
                    return;
                }
                else
                {
                    DataSet getrate = objbs.getrateforjobworker(drpMultiemployee.SelectedValue, lblitemid.Text, "Iron");
                    if (getrate.Tables[0].Rows.Count > 0)
                    {
                        Ironrate = Convert.ToDouble(getrate.Tables[0].Rows[0]["Rate"]);



                    }
                    else
                    {

                    }

                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {

                        TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                        //txtrate.Text = temp.Tables[0].Rows[i]["rate"].ToString();
                        txtrate.Text = Ironrate.ToString();

                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Try Again Later.Something Went Wrong to this " + ddlLotNo.SelectedItem.Text + ". Thank you');", true);
                return;
            }


        }


        protected void StitchingInfo_Load(object sender, EventArgs e)
        {
            DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");

            DataSet dsize = objbs.Getsizetype();
            DataSet dlot = new DataSet();
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


            if (ddlLotNo.SelectedValue != "Select Lot No")
            {

                double Ironrate = 0;

                int dcheck = objbs.checkcurrentforiron(ddlLotNo.SelectedValue, "Iron");
                DataSet getironid = objbs.Get_LotDetforiron();

                DataSet lName = new DataSet();


                DataSet dgetlotdetailid = objbs.getlotdetailforironingprocess(ddlLotNo.SelectedValue);
                if (dgetlotdetailid.Tables[0].Rows.Count > 0)
                {
                    lbllotdetailid.Text = dgetlotdetailid.Tables[0].Rows[0]["lotdetailid"].ToString();
                    lbllotno.Text = dgetlotdetailid.Tables[0].Rows[0]["lotno"].ToString();

                    lName = objbs.Getledgernamee(Convert.ToInt32(dgetlotdetailid.Tables[0].Rows[0]["Cutid"].ToString()));
                }
                else
                {
                    lbllotdetailid.Text = "0";
                    lbllotno.Text = "0";
                }

                if (dcheck == 1)
                {
                    #region

                    DataSet getiteminprecut = objbs.getitemidinprecut(Convert.ToString(ddlLotNo.SelectedItem.Text));
                    if (getiteminprecut.Tables[0].Rows.Count > 0)
                    {
                        txtitemnarration.Text = getiteminprecut.Tables[0].Rows[0]["ItemNarrations"].ToString();
                        lblitemid.Text = getiteminprecut.Tables[0].Rows[0]["itemid"].ToString();
                        lblcuttingdate.Text = Convert.ToDateTime(getiteminprecut.Tables[0].Rows[0]["Deliverydate"]).ToString("dd/MM/yyyy");

                    }

                    DataSet getitem = objbs.getitenameforprocess(ddlLotNo.SelectedItem.Text, "tbljpstiching", "R");
                    if (getitem.Tables[0].Rows.Count > 0)
                    {
                        lblitemname.Text = getitem.Tables[0].Rows[0]["ItemName"].ToString() + '(' + getitem.Tables[0].Rows[0]["Itemcode"].ToString() + ')';
                    }

                    //DataSet rateforprecost = objbs.getrateforprecost(Convert.ToString(ddlLotNo.SelectedValue), "IRON");
                    //if (rateforprecost.Tables[0].Rows.Count > 0)
                    //{
                    //    Ironrate = Convert.ToDouble(rateforprecost.Tables[0].Rows[0]["Cost"].ToString());
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please to Entry Cost in Master Cutting for this " + ddlLotNo.SelectedItem.Text + ". Thank you');", true);
                    //    return;
                    //}


                    // Get LotNo Stiching 
                    //////DataSet dsgetLotNoforStiching = objbs.getLotNoforStiching(ddlLotNo.SelectedItem.Text);
                    //////if (dsgetLotNoforStiching.Tables[0].Rows.Count > 0)
                    //////{

                    //////}
                    //////else
                    //////{
                    //////    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Issue Process for Stiching. Thank you');", true);
                    //////    return;
                    //////}


                    DataSet dlotprocess = new DataSet();
                    dlotprocess = objbs.getcurrentstatusironing(Convert.ToString(ddlLotNo.SelectedValue), "Iron");
                    if (dlotprocess.Tables[0].Rows.Count > 0)
                    {
                        checkinoutprocess.Enabled = true;

                        //  txttotalqty.Text = dlotprocess.Tables[0].Rows[0]["TotalQuantity"].ToString();

                        //lblbrandnid.Text = dlotprocess.Tables[0].Rows[0]["Brandid"].ToString();
                        //lblfitid.Text = dlotprocess.Tables[0].Rows[0]["Fitid"].ToString();


                        //DataSet dsizee = objbs.Getfitseize(lblfitid.Text, lblbrandnid.Text);
                        //if ((dsizee.Tables[0].Rows.Count > 0))
                        //{

                        //    // for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                        //    for (int i = 0; i <= dsizee.Tables[0].Rows.Count - 1; i++)
                        //    {
                        //        //You need to change this as per your DB Design
                        //        string size = dsizee.Tables[0].Rows[i]["Sizeid1"].ToString();
                        //        {
                        //            //Find the checkbox list items using FindByValue and select it.
                        //            chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[i]["Sizeid1"].ToString()).Selected = true;
                        //        }

                        //    }
                        //}

                        if (dlotprocess.Tables[0].Rows[0]["Jobwork"].ToString() == "Out")
                        {
                            DataSet drpEmpp = objbs.Selectname("10");

                            drpMultiemployee.DataSource = drpEmpp;
                            drpMultiemployee.DataTextField = "LedgerName";
                            drpMultiemployee.DataValueField = "LedgerID";
                            drpMultiemployee.DataBind();
                            drpMultiemployee.Items.Insert(0, "Select Name");
                            checkinoutprocess.SelectedValue = "Out";

                            ////// drpMultiemployee.SelectedValue = lName.Tables[0].Rows[0]["Ledgerid"].ToString();

                            //emp.Visible = false;
                            //job.Visible = true;
                        }
                        else if (dlotprocess.Tables[0].Rows[0]["Jobwork"].ToString() == "In")
                        {
                            DataSet drpEmpp = objbs.Selectname("9");
                            ////// DataSet drpEmpp1 = objbs.getledgerforpayiron();
                            drpMultiemployee.DataSource = drpEmpp;
                            drpMultiemployee.DataTextField = "LedgerName";
                            drpMultiemployee.DataValueField = "LedgerID";
                            drpMultiemployee.DataBind();
                            drpMultiemployee.Items.Insert(0, "Select Name");
                            checkinoutprocess.SelectedValue = "In";

                            //////drpMultiemployee.SelectedValue = lName.Tables[0].Rows[0]["Ledgerid"].ToString();

                            //emp.Visible = true;
                            //job.Visible = false;
                        }
                    }
                    //DataSet dlot = objbs.Get_LotDet(Convert.ToString(ddlLotNo.SelectedValue), "6", "Iron");
                    dlot = objbs.getlotdetailforiron(Convert.ToString(ddlLotNo.SelectedValue), "N");


                    if (dlot.Tables[0].Rows.Count > 0)
                    {



                        DataSet temp = new DataSet();
                        DataTable dtt = new DataTable();
                        txttotalqty.Text = dlot.Tables[0].Rows[0]["Totalshirt"].ToString();

                        dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Fitid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Patternid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Designno", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Color", typeof(string)));
                        dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ProcessTypeID", typeof(string)));
                        dtt.Columns.Add(new DataColumn("rate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("damageqty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("senddate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("recdate", typeof(string)));
                        //dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("StockRatioId", typeof(string)));

                        dtt.Columns.Add(new DataColumn("30hs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("32hs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("34hs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("36hs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("30fs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("32fs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("34fs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("36fs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("shs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("mhs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("lhs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("lfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("mfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("sfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("xlfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("xxlfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("3xlfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("4xlfs", typeof(string)));

                        dtt.Columns.Add(new DataColumn("xsfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("xshs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("xlhs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("xxlhs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("3xlhs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("4xlhs", typeof(string)));



                        temp.Tables.Add(dtt);


                        double tot = 0;
                        double rate = 0;
                        double TotalQty = 0;
                        double TotalQty1 = 0;
                        for (int i = 0; i < dlot.Tables[0].Rows.Count; i++)
                        {

                            string fitchnaged = dlot.Tables[0].Rows[i]["Fit"].ToString();
                            string brand = dlot.Tables[0].Rows[i]["brandid"].ToString();

                            // DataSet dsizee = objbs.Getfitseize(fitchnaged);
                            DataSet dsizee = objbs.Getfitseize(fitchnaged, brand);
                            if ((dsizee.Tables[0].Rows.Count > 0))
                            {
                                //Select the checkboxlist items those values are true in database
                                //Loop through the DataTable
                                for (int ii = 0; ii <= dsizee.Tables[0].Rows.Count - 1; ii++)
                                {
                                    //You need to change this as per your DB Design
                                    string size = dsizee.Tables[0].Rows[ii]["Sizeid1"].ToString();



                                    //if (size == "39FS" || size == "39HS" || size == "44FS" || size == "44HS")
                                    //{
                                    //}
                                    //else
                                    {
                                        //Find the checkbox list items using FindByValue and select it.
                                        chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[ii]["Sizeid1"].ToString()).Selected = true;
                                    }

                                }
                            }
                            DataRow dr = dtt.NewRow();

                            dr["fit"] = dlot.Tables[0].Rows[i]["fitname"].ToString();
                            dr["itemname"] = dlot.Tables[0].Rows[i]["itemname"].ToString();
                            dr["Designno"] = dlot.Tables[0].Rows[i]["Designcode"].ToString();
                            dr["Color"] = dlot.Tables[0].Rows[i]["Onlycolor"].ToString();
                            dr["TotalQty"] = dlot.Tables[0].Rows[i]["TotalSHIRT"].ToString();
                            dr["RemainQty"] = dlot.Tables[0].Rows[i]["Remainshirt"].ToString();
                            dr["ProcessTypeID"] = getironid.Tables[0].Rows[0]["ProcessmasterID"].ToString();
                            dr["rate"] = "0";
                            dr["damageqty"] = "0";
                            dr["RecQty"] = "0";
                            TotalQty = Convert.ToDouble(dlot.Tables[0].Rows[i]["TotalSHIRT"]);
                            TotalQty1 = TotalQty1 + TotalQty;
                            rate = Convert.ToDouble(0);
                            tot = tot + (TotalQty * rate);
                            dr["Fitid"] = dlot.Tables[0].Rows[i]["Fit"].ToString();
                            dr["Pattern"] = "0";
                            dr["Patternid"] = "1";


                            dr["30fs"] = dlot.Tables[0].Rows[i]["R30fs"].ToString();
                            dr["30hs"] = dlot.Tables[0].Rows[i]["R30hs"].ToString();
                            dr["32fs"] = dlot.Tables[0].Rows[i]["R32fs"].ToString();
                            dr["32hs"] = dlot.Tables[0].Rows[i]["R32hs"].ToString();
                            dr["34hs"] = dlot.Tables[0].Rows[i]["R34hs"].ToString();
                            dr["34fs"] = dlot.Tables[0].Rows[i]["R34fs"].ToString();
                            dr["36hs"] = dlot.Tables[0].Rows[i]["R36hs"].ToString();
                            dr["36fs"] = dlot.Tables[0].Rows[i]["R36fs"].ToString();

                            dr["xshs"] = dlot.Tables[0].Rows[i]["Rxshs"].ToString();
                            dr["xsfs"] = dlot.Tables[0].Rows[i]["Rxsfs"].ToString();
                            dr["shs"] = dlot.Tables[0].Rows[i]["Rshs"].ToString();
                            dr["sfs"] = dlot.Tables[0].Rows[i]["Rsfs"].ToString();
                            dr["mhs"] = dlot.Tables[0].Rows[i]["Rmhs"].ToString();
                            dr["mfs"] = dlot.Tables[0].Rows[i]["Rmfs"].ToString();
                            dr["lhs"] = dlot.Tables[0].Rows[i]["Rlhs"].ToString();
                            dr["lfs"] = dlot.Tables[0].Rows[i]["Rlfs"].ToString();
                            dr["xxlhs"] = dlot.Tables[0].Rows[i]["Rxxlhs"].ToString();
                            dr["xxlfs"] = dlot.Tables[0].Rows[i]["Rxxlfs"].ToString();

                            dr["xlhs"] = dlot.Tables[0].Rows[i]["Rxlhs"].ToString();
                            dr["xlfs"] = dlot.Tables[0].Rows[i]["Rxlfs"].ToString();
                            dr["3xlhs"] = dlot.Tables[0].Rows[i]["R3xlhs"].ToString();
                            dr["3xlfs"] = dlot.Tables[0].Rows[i]["R3xlfs"].ToString();
                            dr["4xlhs"] = dlot.Tables[0].Rows[i]["R4xlhs"].ToString();
                            dr["4xlfs"] = dlot.Tables[0].Rows[i]["R4xlfs"].ToString();

                            dr["StockRatioId"] = dlot.Tables[0].Rows[i]["StockRatioId"].ToString();
                            temp.Tables[0].Rows.Add(dr);

                        }
                        txttotalqty.Text = TotalQty1.ToString("0.00");
                        txtAmount.Text = tot.ToString();

                        ViewState["CurrentTable1"] = dtt;

                        gvcustomerorder.DataSource = temp;
                        gvcustomerorder.DataBind();

                        for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                        {
                            TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                            DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                            Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");
                            TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");


                            Label lblonlycolor = (Label)gvcustomerorder.Rows[i].FindControl("lblonlycolor");

                            Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                            Label lbldesignno = (Label)gvcustomerorder.Rows[i].FindControl("lbldesignno");
                            TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                            TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                            Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                            Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                            Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                            TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                            TextBox date1 = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                            TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");
                            string recdate = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;
                            string date11 = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;

                            TextBox txts30fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fsac");
                            TextBox txts30hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hsac");
                            TextBox txts32fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fsac");
                            TextBox txts32hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hsac");
                            TextBox txts34fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fsac");
                            TextBox txts34hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hsac");
                            TextBox txts36fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fsac");
                            TextBox txts36hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hsac");
                            TextBox txtsxsfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfsac");
                            TextBox txtsxshsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshsac");

                            TextBox txtslfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfsac");
                            TextBox txtslhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhsac");
                            TextBox txtssfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfsac");
                            TextBox txtsshsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshsac");
                            TextBox txtsmfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfsac");
                            TextBox txtsmhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhsac");
                            TextBox txtsxlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfsac");
                            TextBox txtsxlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhsac");
                            TextBox txtsxxlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfsac");
                            TextBox txtsxxlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhsac");
                            TextBox txts3xlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfsac");
                            TextBox txts3xlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhsac");
                            TextBox txts4xlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfsac");
                            TextBox txts4xlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhsac");



                            TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                            TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                            TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                            TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                            TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                            TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                            TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                            TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                            TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                            TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");

                            TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                            TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                            TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                            TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                            TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                            TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                            TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                            TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                            TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                            TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                            TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                            TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                            TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                            TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");

                            Label lblStockRatioId = (Label)gvcustomerorder.Rows[i].FindControl("lblStockRatioId");


                            txts30fsac.Text = temp.Tables[0].Rows[i]["30fs"].ToString();
                            txts30hsac.Text = temp.Tables[0].Rows[i]["30hs"].ToString();
                            txts32fsac.Text = temp.Tables[0].Rows[i]["32fs"].ToString();
                            txts32hsac.Text = temp.Tables[0].Rows[i]["32hs"].ToString();
                            txts34fsac.Text = temp.Tables[0].Rows[i]["34fs"].ToString();
                            txts34hsac.Text = temp.Tables[0].Rows[i]["34hs"].ToString();
                            txts36fsac.Text = temp.Tables[0].Rows[i]["36fs"].ToString();
                            txts36hsac.Text = temp.Tables[0].Rows[i]["36hs"].ToString();
                            txtsxsfsac.Text = temp.Tables[0].Rows[i]["xsfs"].ToString();
                            txtsxshsac.Text = temp.Tables[0].Rows[i]["xshs"].ToString();
                            txtssfsac.Text = temp.Tables[0].Rows[i]["sfs"].ToString();
                            txtsshsac.Text = temp.Tables[0].Rows[i]["shs"].ToString();
                            txtsmfsac.Text = temp.Tables[0].Rows[i]["mfs"].ToString();
                            txtsmhsac.Text = temp.Tables[0].Rows[i]["mhs"].ToString();
                            txtslfsac.Text = temp.Tables[0].Rows[i]["lfs"].ToString();
                            txtslhsac.Text = temp.Tables[0].Rows[i]["lhs"].ToString();
                            txtsxlfsac.Text = temp.Tables[0].Rows[i]["xlfs"].ToString();
                            txtsxlhsac.Text = temp.Tables[0].Rows[i]["xlhs"].ToString();
                            txtsxxlfsac.Text = temp.Tables[0].Rows[i]["xxlfs"].ToString();
                            txtsxxlhsac.Text = temp.Tables[0].Rows[i]["xxlhs"].ToString();
                            txts3xlfsac.Text = temp.Tables[0].Rows[i]["3xlfs"].ToString();
                            txts3xlhsac.Text = temp.Tables[0].Rows[i]["3xlhs"].ToString();
                            txts4xlfsac.Text = temp.Tables[0].Rows[i]["4xlfs"].ToString();
                            txts4xlhsac.Text = temp.Tables[0].Rows[i]["4xlhs"].ToString();


                            txts30fs.Text = temp.Tables[0].Rows[i]["30fs"].ToString();
                            txts30hs.Text = temp.Tables[0].Rows[i]["30hs"].ToString();
                            txts32fs.Text = temp.Tables[0].Rows[i]["32fs"].ToString();
                            txts32hs.Text = temp.Tables[0].Rows[i]["32hs"].ToString();
                            txts34fs.Text = temp.Tables[0].Rows[i]["34fs"].ToString();
                            txts34hs.Text = temp.Tables[0].Rows[i]["34hs"].ToString();
                            txts36fs.Text = temp.Tables[0].Rows[i]["36fs"].ToString();
                            txts36hs.Text = temp.Tables[0].Rows[i]["36hs"].ToString();
                            txtsxsfs.Text = temp.Tables[0].Rows[i]["xsfs"].ToString();
                            txtsxshs.Text = temp.Tables[0].Rows[i]["xshs"].ToString();
                            txtssfs.Text = temp.Tables[0].Rows[i]["sfs"].ToString();
                            txtsshs.Text = temp.Tables[0].Rows[i]["shs"].ToString();
                            txtsmfs.Text = temp.Tables[0].Rows[i]["mfs"].ToString();
                            txtsmhs.Text = temp.Tables[0].Rows[i]["mhs"].ToString();
                            txtslfs.Text = temp.Tables[0].Rows[i]["lfs"].ToString();
                            txtslhs.Text = temp.Tables[0].Rows[i]["lhs"].ToString();
                            txtsxlfs.Text = temp.Tables[0].Rows[i]["xlfs"].ToString();
                            txtsxlhs.Text = temp.Tables[0].Rows[i]["xlhs"].ToString();
                            txtsxxlfs.Text = temp.Tables[0].Rows[i]["xxlfs"].ToString();
                            txtsxxlhs.Text = temp.Tables[0].Rows[i]["xxlhs"].ToString();
                            txts3xlfs.Text = temp.Tables[0].Rows[i]["3xlfs"].ToString();
                            txts3xlhs.Text = temp.Tables[0].Rows[i]["3xlhs"].ToString();
                            txts4xlfs.Text = temp.Tables[0].Rows[i]["4xlfs"].ToString();
                            txts4xlhs.Text = temp.Tables[0].Rows[i]["4xlhs"].ToString();
                            //txts30fs.Text = "0";
                            //txts30hs.Text = "0";
                            //txts32fs.Text = "0";
                            //txts32hs.Text = "0";
                            //txts34fs.Text = "0";
                            //txts34hs.Text = "0";
                            //txts36fs.Text = "0";
                            //txts36hs.Text = "0";
                            //txtsxsfs.Text = "0";
                            //txtsxshs.Text = "0";
                            //txtssfs.Text = "0";
                            //txtsshs.Text = "0";
                            //txtsmfs.Text = "0";
                            //txtsmhs.Text = "0";
                            //txtslfs.Text = "0";
                            //txtslhs.Text = "0";
                            //txtsxlfs.Text = "0";
                            //txtsxlhs.Text = "0";
                            //txtsxxlfs.Text = "0";
                            //txtsxxlhs.Text = "0";
                            //txts3xlfs.Text = "0";
                            //txts3xlhs.Text = "0";
                            //txts4xlfs.Text = "0";
                            //txts4xlhs.Text = "0";

                            txtsendFQty.Text = temp.Tables[0].Rows[i]["TotalQty"].ToString();
                            txtRemainQty.Text = temp.Tables[0].Rows[i]["RemainQty"].ToString();
                            lblfit.Text = temp.Tables[0].Rows[i]["fit"].ToString();
                            lblitemname.Text = temp.Tables[0].Rows[i]["itemname"].ToString();
                            lbldesignno.Text = temp.Tables[0].Rows[i]["Designno"].ToString();
                            lblonlycolor.Text = temp.Tables[0].Rows[i]["color"].ToString();
                            lblfitid.Text = temp.Tables[0].Rows[i]["fitid"].ToString();

                            //////  txtrate.Text = temp.Tables[0].Rows[i]["rate"].ToString();
                            txtrate.Text = Ironrate.ToString();

                            drpProcess.SelectedValue = temp.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                            lblPattern.Text = temp.Tables[0].Rows[i]["Pattern"].ToString();
                            lblPatternid.Text = temp.Tables[0].Rows[i]["Patternid"].ToString();
                            txtdamageqty.Text = temp.Tables[0].Rows[i]["damageqty"].ToString();
                            date1.Text = date11.ToString();
                            Recdate.Text = recdate.ToString();
                            txtrecFQty.Text = temp.Tables[0].Rows[i]["recQty"].ToString();
                            txtdamageqty.Text = "0";
                            txtrecFQty.Text = "0";

                            lblStockRatioId.Text = temp.Tables[0].Rows[i]["StockRatioId"].ToString();

                            if (chkSizes.SelectedIndex >= 0)
                            {
                                gvcustomerorder.Columns[7].Visible = false; //30FS
                                gvcustomerorder.Columns[8].Visible = false; //32FS

                                gvcustomerorder.Columns[9].Visible = false;//34Fs
                                gvcustomerorder.Columns[10].Visible = false;//36Fs

                                gvcustomerorder.Columns[11].Visible = false; //XSFS
                                gvcustomerorder.Columns[12].Visible = false; //SFS

                                gvcustomerorder.Columns[13].Visible = false; //MFS
                                gvcustomerorder.Columns[14].Visible = false; //LFS

                                gvcustomerorder.Columns[15].Visible = false; //XLFS
                                gvcustomerorder.Columns[16].Visible = false; //XXLFS

                                gvcustomerorder.Columns[17].Visible = false; //3XLFS
                                gvcustomerorder.Columns[18].Visible = false; //4XLFS

                                gvcustomerorder.Columns[19].Visible = false; //30HS
                                gvcustomerorder.Columns[20].Visible = false; //32HS

                                gvcustomerorder.Columns[21].Visible = false; //34Hs
                                gvcustomerorder.Columns[22].Visible = false; //36Hs

                                gvcustomerorder.Columns[23].Visible = false; //XSHS
                                gvcustomerorder.Columns[24].Visible = false; //SHS


                                gvcustomerorder.Columns[25].Visible = false;  //MHS
                                gvcustomerorder.Columns[26].Visible = false;  //LHS


                                gvcustomerorder.Columns[27].Visible = false; //XLHS
                                gvcustomerorder.Columns[28].Visible = false; //XXLHS

                                gvcustomerorder.Columns[29].Visible = false; //3XLHS
                                gvcustomerorder.Columns[30].Visible = false; //4XLHS



                                int lop = 0;
                                //Loop through each item of checkboxlist
                                foreach (ListItem item in chkSizes.Items)
                                {
                                    //check if item selected

                                    if (item.Selected)
                                    {

                                        {
                                            if (item.Value == "1") // 30FS
                                            {

                                                gvcustomerorder.Columns[7].Visible = true;
                                            }
                                            if (item.Value == "3") // 32FS
                                            {
                                                gvcustomerorder.Columns[8].Visible = true;
                                            }
                                            if (item.Value == "5") //34FS
                                            {
                                                gvcustomerorder.Columns[9].Visible = true;
                                            }
                                            if (item.Value == "7") //36FS
                                            {
                                                gvcustomerorder.Columns[10].Visible = true;
                                            }

                                            if (item.Value == "9") //XSFS
                                            {
                                                gvcustomerorder.Columns[11].Visible = true;
                                            }
                                            if (item.Value == "21") //SFS
                                            {
                                                gvcustomerorder.Columns[12].Visible = true;
                                            }
                                            if (item.Value == "23") // MFS
                                            {
                                                gvcustomerorder.Columns[13].Visible = true;
                                            }
                                            if (item.Value == "11") // LFS
                                            {
                                                gvcustomerorder.Columns[14].Visible = true;
                                            }
                                            if (item.Value == "13") //XLFS
                                            {
                                                gvcustomerorder.Columns[15].Visible = true;
                                            }
                                            if (item.Value == "15") // XXLFS
                                            {
                                                gvcustomerorder.Columns[16].Visible = true;
                                            }
                                            if (item.Value == "17") //3XLFS
                                            {
                                                gvcustomerorder.Columns[17].Visible = true;
                                            }
                                            if (item.Value == "19") // 4XLFS
                                            {
                                                gvcustomerorder.Columns[18].Visible = true;
                                            }





                                            if (item.Value == "2") // 30HS
                                            {
                                                gvcustomerorder.Columns[19].Visible = true;
                                            }

                                            if (item.Value == "4") //32HS
                                            {
                                                gvcustomerorder.Columns[20].Visible = true;
                                            }

                                            if (item.Value == "6") //34HS
                                            {
                                                gvcustomerorder.Columns[21].Visible = true;
                                            }

                                            if (item.Value == "8") //36HS
                                            {
                                                gvcustomerorder.Columns[22].Visible = true;
                                            }

                                            if (item.Value == "10") //XSHS
                                            {
                                                gvcustomerorder.Columns[23].Visible = true;
                                            }
                                            if (item.Value == "22") //SHS
                                            {
                                                gvcustomerorder.Columns[24].Visible = true;
                                            }

                                            if (item.Value == "24") // MHS
                                            {
                                                gvcustomerorder.Columns[25].Visible = true;
                                            }

                                            if (item.Value == "12") // LHS
                                            {
                                                gvcustomerorder.Columns[26].Visible = true;
                                            }

                                            if (item.Value == "14") //XLHS
                                            {
                                                gvcustomerorder.Columns[27].Visible = true;
                                            }

                                            if (item.Value == "16") // XXLHS
                                            {
                                                gvcustomerorder.Columns[28].Visible = true;
                                            }

                                            if (item.Value == "18") // 3XLHS
                                            {
                                                gvcustomerorder.Columns[29].Visible = true;
                                            }

                                            if (item.Value == "20") // 4XLHS
                                            {
                                                gvcustomerorder.Columns[30].Visible = true;
                                            }




                                            lop++;

                                        }

                                    }
                                }
                                //gvcustomerorder.DataSource = dssmer;
                                //gvcustomerorder.DataBind();
                            }
                            else
                            {
                                gvcustomerorder.Columns[7].Visible = false; //30FS
                                gvcustomerorder.Columns[8].Visible = false; //32FS

                                gvcustomerorder.Columns[9].Visible = false;//34Fs
                                gvcustomerorder.Columns[10].Visible = false;//36Fs

                                gvcustomerorder.Columns[11].Visible = false; //XSFS
                                gvcustomerorder.Columns[12].Visible = false; //SFS

                                gvcustomerorder.Columns[13].Visible = false; //MFS
                                gvcustomerorder.Columns[14].Visible = false; //LFS

                                gvcustomerorder.Columns[15].Visible = false; //XLFS
                                gvcustomerorder.Columns[16].Visible = false; //XXLFS

                                gvcustomerorder.Columns[17].Visible = false; //3XLFS
                                gvcustomerorder.Columns[18].Visible = false; //4XLFS

                                gvcustomerorder.Columns[19].Visible = false; //30HS
                                gvcustomerorder.Columns[20].Visible = false; //32HS

                                gvcustomerorder.Columns[21].Visible = false; //34Hs
                                gvcustomerorder.Columns[22].Visible = false; //36Hs

                                gvcustomerorder.Columns[23].Visible = false; //XSHS
                                gvcustomerorder.Columns[24].Visible = false; //SHS


                                gvcustomerorder.Columns[25].Visible = false;  //MHS
                                gvcustomerorder.Columns[26].Visible = false;  //LHS


                                gvcustomerorder.Columns[27].Visible = false; //XLHS
                                gvcustomerorder.Columns[28].Visible = false; //XXLHS

                                gvcustomerorder.Columns[29].Visible = false; //3XLHS
                                gvcustomerorder.Columns[30].Visible = false; //4XLHS


                            }
                            //gvcustomerorder.Columns[10].Visible = false;
                            //gvcustomerorder.Columns[11].Visible = false;
                            //gvcustomerorder.Columns[13].Visible = false;

                        }
                        btnadd.Enabled = true;
                    }

                    #endregion

                }
                else if (dcheck == 0)
                {
                    #region

                    {
                        checkedinoutproceschnaged(sender, e);


                        dlot = objbs.getlotdetailforiron(Convert.ToString(ddlLotNo.SelectedValue), "Y");

                        if (dlot.Tables[0].Rows.Count > 0)
                        {
                            DataSet temp = new DataSet();
                            DataTable dtt = new DataTable();

                            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Fitid", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Patternid", typeof(string)));
                            dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Designno", typeof(string)));
                            dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                            dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                            dtt.Columns.Add(new DataColumn("ProcessTypeID", typeof(string)));
                            dtt.Columns.Add(new DataColumn("rate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("damageqty", typeof(string)));
                            dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                            dtt.Columns.Add(new DataColumn("senddate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("recdate", typeof(string)));
                            //dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));

                            dtt.Columns.Add(new DataColumn("30hs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("32hs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("34hs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("36hs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("30fs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("32fs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("34fs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("36fs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("shs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("mhs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("lhs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("lfs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("mfs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("sfs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("xlfs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("xxlfs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("3xlfs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("4xlfs", typeof(string)));

                            dtt.Columns.Add(new DataColumn("xsfs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("xshs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("xlhs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("xxlhs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("3xlhs", typeof(string)));
                            dtt.Columns.Add(new DataColumn("4xlhs", typeof(string)));

                            dtt.Columns.Add(new DataColumn("StockRatioId", typeof(string)));
                            temp.Tables.Add(dtt);


                            double tot = 0;
                            double rate = 0;
                            double TotalQty = 0;
                            double TotalQty1 = 0;
                            for (int i = 0; i < dlot.Tables[0].Rows.Count; i++)
                            {

                                string fitchnaged = dlot.Tables[0].Rows[i]["Fit"].ToString();

                                string brand = dlot.Tables[0].Rows[i]["brandid"].ToString();

                                // DataSet dsizee = objbs.Getfitseize(fitchnaged);
                                DataSet dsizee = objbs.Getfitseize(fitchnaged, brand);
                                if ((dsizee.Tables[0].Rows.Count > 0))
                                {
                                    //Select the checkboxlist items those values are true in database
                                    //Loop through the DataTable
                                    for (int ii = 0; ii <= dsizee.Tables[0].Rows.Count - 1; ii++)
                                    {
                                        //You need to change this as per your DB Design
                                        string size = dsizee.Tables[0].Rows[ii]["Sizeid1"].ToString();



                                        //if (size == "39FS" || size == "39HS" || size == "44FS" || size == "44HS")
                                        //{
                                        //}
                                        //else
                                        {
                                            //Find the checkbox list items using FindByValue and select it.
                                            chkSizes.Items.FindByValue(dsizee.Tables[0].Rows[ii]["Sizeid1"].ToString()).Selected = true;
                                        }

                                    }
                                }
                                DataRow dr = dtt.NewRow();

                                dr["fit"] = dlot.Tables[0].Rows[i]["fitname"].ToString();
                                dr["itemname"] = dlot.Tables[0].Rows[i]["itemname"].ToString();
                                dr["Designno"] = dlot.Tables[0].Rows[i]["Designcode"].ToString();
                                dr["TotalQty"] = dlot.Tables[0].Rows[i]["Totalshirt"].ToString();
                                dr["RemainQty"] = dlot.Tables[0].Rows[i]["Remainshirt"].ToString();
                                dr["ProcessTypeID"] = getironid.Tables[0].Rows[0]["ProcessmasterID"].ToString();
                                dr["rate"] = "0";
                                dr["damageqty"] = "0";
                                dr["RecQty"] = "0";
                                TotalQty = Convert.ToDouble(dlot.Tables[0].Rows[i]["Totalshirt"]);
                                TotalQty1 = TotalQty1 + TotalQty;
                                rate = Convert.ToDouble(0);
                                tot = tot + (TotalQty * rate);
                                dr["Fitid"] = dlot.Tables[0].Rows[i]["Fit"].ToString();
                                dr["Pattern"] = "1";
                                dr["Patternid"] = "1";


                                dr["30fs"] = dlot.Tables[0].Rows[i]["R30fs"].ToString();
                                dr["30hs"] = dlot.Tables[0].Rows[i]["R30hs"].ToString();
                                dr["32fs"] = dlot.Tables[0].Rows[i]["R32fs"].ToString();
                                dr["32hs"] = dlot.Tables[0].Rows[i]["R32hs"].ToString();
                                dr["34hs"] = dlot.Tables[0].Rows[i]["R34hs"].ToString();
                                dr["34fs"] = dlot.Tables[0].Rows[i]["R34fs"].ToString();
                                dr["36hs"] = dlot.Tables[0].Rows[i]["R36hs"].ToString();
                                dr["36fs"] = dlot.Tables[0].Rows[i]["R36fs"].ToString();

                                dr["xshs"] = dlot.Tables[0].Rows[i]["Rxshs"].ToString();
                                dr["xsfs"] = dlot.Tables[0].Rows[i]["Rxsfs"].ToString();
                                dr["shs"] = dlot.Tables[0].Rows[i]["Rshs"].ToString();
                                dr["sfs"] = dlot.Tables[0].Rows[i]["Rsfs"].ToString();
                                dr["mhs"] = dlot.Tables[0].Rows[i]["Rmhs"].ToString();
                                dr["mfs"] = dlot.Tables[0].Rows[i]["Rmfs"].ToString();
                                dr["lhs"] = dlot.Tables[0].Rows[i]["Rlhs"].ToString();
                                dr["lfs"] = dlot.Tables[0].Rows[i]["Rlfs"].ToString();
                                dr["xxlhs"] = dlot.Tables[0].Rows[i]["Rxxlhs"].ToString();
                                dr["xxlfs"] = dlot.Tables[0].Rows[i]["Rxxlfs"].ToString();

                                dr["xlhs"] = dlot.Tables[0].Rows[i]["Rxlhs"].ToString();
                                dr["xlfs"] = dlot.Tables[0].Rows[i]["Rxlfs"].ToString();
                                dr["3xlhs"] = dlot.Tables[0].Rows[i]["R3xlhs"].ToString();
                                dr["3xlfs"] = dlot.Tables[0].Rows[i]["R3xlfs"].ToString();
                                dr["4xlhs"] = dlot.Tables[0].Rows[i]["R4xlhs"].ToString();
                                dr["4xlfs"] = dlot.Tables[0].Rows[i]["R4xlfs"].ToString();

                                dr["StockRatioId"] = dlot.Tables[0].Rows[i]["StockRatioId"].ToString();
                                temp.Tables[0].Rows.Add(dr);

                            }
                            txttotalqty.Text = TotalQty1.ToString("0.00");
                            txtAmount.Text = tot.ToString();

                            ViewState["CurrentTable1"] = dtt;

                            gvcustomerorder.DataSource = temp;
                            gvcustomerorder.DataBind();

                            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                            {
                                TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");
                                DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                                Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");
                                TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                                Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                                Label lbldesignno = (Label)gvcustomerorder.Rows[i].FindControl("lbldesignno");
                                TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                                TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                                Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                                Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                                Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                                TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                                TextBox date1 = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                                TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");
                                string recdate = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;
                                string date11 = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy"); ;

                                TextBox txts30fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fsac");
                                TextBox txts30hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hsac");
                                TextBox txts32fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fsac");
                                TextBox txts32hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hsac");
                                TextBox txts34fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fsac");
                                TextBox txts34hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hsac");
                                TextBox txts36fsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fsac");
                                TextBox txts36hsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hsac");
                                TextBox txtsxsfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfsac");
                                TextBox txtsxshsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshsac");

                                TextBox txtslfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfsac");
                                TextBox txtslhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhsac");
                                TextBox txtssfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfsac");
                                TextBox txtsshsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshsac");
                                TextBox txtsmfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfsac");
                                TextBox txtsmhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhsac");
                                TextBox txtsxlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfsac");
                                TextBox txtsxlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhsac");
                                TextBox txtsxxlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfsac");
                                TextBox txtsxxlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhsac");
                                TextBox txts3xlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfsac");
                                TextBox txts3xlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhsac");
                                TextBox txts4xlfsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfsac");
                                TextBox txts4xlhsac = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhsac");

                                TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                                TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                                TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                                TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                                TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                                TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                                TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                                TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                                TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                                TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");

                                TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                                TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                                TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                                TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                                TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                                TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                                TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                                TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                                TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                                TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                                TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                                TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                                TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                                TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");


                                Label lblStockRatioId = (Label)gvcustomerorder.Rows[i].FindControl("lblStockRatioId");

                                txts30fsac.Text = temp.Tables[0].Rows[i]["30fs"].ToString();
                                txts30hsac.Text = temp.Tables[0].Rows[i]["30hs"].ToString();
                                txts32fsac.Text = temp.Tables[0].Rows[i]["32fs"].ToString();
                                txts32hsac.Text = temp.Tables[0].Rows[i]["32hs"].ToString();
                                txts34fsac.Text = temp.Tables[0].Rows[i]["34fs"].ToString();
                                txts34hsac.Text = temp.Tables[0].Rows[i]["34hs"].ToString();
                                txts36fsac.Text = temp.Tables[0].Rows[i]["36fs"].ToString();
                                txts36hsac.Text = temp.Tables[0].Rows[i]["36hs"].ToString();
                                txtsxsfsac.Text = temp.Tables[0].Rows[i]["xsfs"].ToString();
                                txtsxshsac.Text = temp.Tables[0].Rows[i]["xshs"].ToString();
                                txtssfsac.Text = temp.Tables[0].Rows[i]["sfs"].ToString();
                                txtsshsac.Text = temp.Tables[0].Rows[i]["shs"].ToString();
                                txtsmfsac.Text = temp.Tables[0].Rows[i]["mfs"].ToString();
                                txtsmhsac.Text = temp.Tables[0].Rows[i]["mhs"].ToString();
                                txtslfsac.Text = temp.Tables[0].Rows[i]["lfs"].ToString();
                                txtslhsac.Text = temp.Tables[0].Rows[i]["lhs"].ToString();
                                txtsxlfsac.Text = temp.Tables[0].Rows[i]["xlfs"].ToString();
                                txtsxlhsac.Text = temp.Tables[0].Rows[i]["xlhs"].ToString();
                                txtsxxlfsac.Text = temp.Tables[0].Rows[i]["xxlfs"].ToString();
                                txtsxxlhsac.Text = temp.Tables[0].Rows[i]["xxlhs"].ToString();
                                txts3xlfsac.Text = temp.Tables[0].Rows[i]["3xlfs"].ToString();
                                txts3xlhsac.Text = temp.Tables[0].Rows[i]["3xlhs"].ToString();
                                txts4xlfsac.Text = temp.Tables[0].Rows[i]["4xlfs"].ToString();
                                txts4xlhsac.Text = temp.Tables[0].Rows[i]["4xlhs"].ToString();

                                txts30fs.Text = temp.Tables[0].Rows[i]["30fs"].ToString();
                                txts30hs.Text = temp.Tables[0].Rows[i]["30hs"].ToString();
                                txts32fs.Text = temp.Tables[0].Rows[i]["32fs"].ToString();
                                txts32hs.Text = temp.Tables[0].Rows[i]["32hs"].ToString();
                                txts34fs.Text = temp.Tables[0].Rows[i]["34fs"].ToString();
                                txts34hs.Text = temp.Tables[0].Rows[i]["34hs"].ToString();
                                txts36fs.Text = temp.Tables[0].Rows[i]["36fs"].ToString();
                                txts36hs.Text = temp.Tables[0].Rows[i]["36hs"].ToString();
                                txtsxsfs.Text = temp.Tables[0].Rows[i]["xsfs"].ToString();
                                txtsxshs.Text = temp.Tables[0].Rows[i]["xshs"].ToString();
                                txtssfs.Text = temp.Tables[0].Rows[i]["sfs"].ToString();
                                txtsshs.Text = temp.Tables[0].Rows[i]["shs"].ToString();
                                txtsmfs.Text = temp.Tables[0].Rows[i]["mfs"].ToString();
                                txtsmhs.Text = temp.Tables[0].Rows[i]["mhs"].ToString();
                                txtslfs.Text = temp.Tables[0].Rows[i]["lfs"].ToString();
                                txtslhs.Text = temp.Tables[0].Rows[i]["lhs"].ToString();
                                txtsxlfs.Text = temp.Tables[0].Rows[i]["xlfs"].ToString();
                                txtsxlhs.Text = temp.Tables[0].Rows[i]["xlhs"].ToString();
                                txtsxxlfs.Text = temp.Tables[0].Rows[i]["xxlfs"].ToString();
                                txtsxxlhs.Text = temp.Tables[0].Rows[i]["xxlhs"].ToString();
                                txts3xlfs.Text = temp.Tables[0].Rows[i]["3xlfs"].ToString();
                                txts3xlhs.Text = temp.Tables[0].Rows[i]["3xlhs"].ToString();
                                txts4xlfs.Text = temp.Tables[0].Rows[i]["4xlfs"].ToString();
                                txts4xlhs.Text = temp.Tables[0].Rows[i]["4xlhs"].ToString();
                                //txts30fs.Text = "0";
                                //txts30hs.Text = "0";
                                //txts32fs.Text = "0";
                                //txts32hs.Text = "0";
                                //txts34fs.Text = "0";
                                //txts34hs.Text = "0";
                                //txts36fs.Text = "0";
                                //txts36hs.Text = "0";
                                //txtsxsfs.Text = "0";
                                //txtsxshs.Text = "0";
                                //txtssfs.Text = "0";
                                //txtsshs.Text = "0";
                                //txtsmfs.Text = "0";
                                //txtsmhs.Text = "0";
                                //txtslfs.Text = "0";
                                //txtslhs.Text = "0";
                                //txtsxlfs.Text = "0";
                                //txtsxlhs.Text = "0";
                                //txtsxxlfs.Text = "0";
                                //txtsxxlhs.Text = "0";
                                //txts3xlfs.Text = "0";
                                //txts3xlhs.Text = "0";
                                //txts4xlfs.Text = "0";
                                //txts4xlhs.Text = "0";

                                lblStockRatioId.Text = temp.Tables[0].Rows[i]["StockRatioId"].ToString();

                                txtsendFQty.Text = temp.Tables[0].Rows[i]["TotalQty"].ToString();
                                txtRemainQty.Text = temp.Tables[0].Rows[i]["RemainQty"].ToString();
                                lblfit.Text = temp.Tables[0].Rows[i]["fit"].ToString();
                                lblitemname.Text = temp.Tables[0].Rows[i]["itemname"].ToString();
                                lbldesignno.Text = temp.Tables[0].Rows[i]["Designno"].ToString();
                                lblfitid.Text = temp.Tables[0].Rows[i]["fitid"].ToString();
                                txtrate.Text = temp.Tables[0].Rows[i]["rate"].ToString();
                                drpProcess.SelectedValue = temp.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                lblPattern.Text = temp.Tables[0].Rows[i]["Pattern"].ToString();
                                lblPatternid.Text = temp.Tables[0].Rows[i]["Patternid"].ToString();
                                txtdamageqty.Text = temp.Tables[0].Rows[i]["damageqty"].ToString();
                                date1.Text = date11.ToString();
                                Recdate.Text = recdate.ToString();
                                txtrecFQty.Text = temp.Tables[0].Rows[i]["recQty"].ToString();
                                txtdamageqty.Text = "0";
                                txtrecFQty.Text = "0";


                                if (chkSizes.SelectedIndex >= 0)
                                {
                                    gvcustomerorder.Columns[7].Visible = false; //30FS
                                    gvcustomerorder.Columns[8].Visible = false; //32FS

                                    gvcustomerorder.Columns[9].Visible = false;//34Fs
                                    gvcustomerorder.Columns[10].Visible = false;//36Fs

                                    gvcustomerorder.Columns[11].Visible = false; //XSFS
                                    gvcustomerorder.Columns[12].Visible = false; //SFS

                                    gvcustomerorder.Columns[13].Visible = false; //MFS
                                    gvcustomerorder.Columns[14].Visible = false; //LFS

                                    gvcustomerorder.Columns[15].Visible = false; //XLFS
                                    gvcustomerorder.Columns[16].Visible = false; //XXLFS

                                    gvcustomerorder.Columns[17].Visible = false; //3XLFS
                                    gvcustomerorder.Columns[18].Visible = false; //4XLFS

                                    gvcustomerorder.Columns[19].Visible = false; //30HS
                                    gvcustomerorder.Columns[20].Visible = false; //32HS

                                    gvcustomerorder.Columns[21].Visible = false; //34Hs
                                    gvcustomerorder.Columns[22].Visible = false; //36Hs

                                    gvcustomerorder.Columns[23].Visible = false; //XSHS
                                    gvcustomerorder.Columns[24].Visible = false; //SHS


                                    gvcustomerorder.Columns[25].Visible = false;  //MHS
                                    gvcustomerorder.Columns[26].Visible = false;  //LHS


                                    gvcustomerorder.Columns[27].Visible = false; //XLHS
                                    gvcustomerorder.Columns[28].Visible = false; //XXLHS

                                    gvcustomerorder.Columns[29].Visible = false; //3XLHS
                                    gvcustomerorder.Columns[30].Visible = false; //4XLHS



                                    int lop = 0;
                                    //Loop through each item of checkboxlist
                                    foreach (ListItem item in chkSizes.Items)
                                    {
                                        //check if item selected

                                        if (item.Selected)
                                        {

                                            {
                                                if (item.Value == "1") // 30FS
                                                {

                                                    gvcustomerorder.Columns[7].Visible = true;
                                                }
                                                if (item.Value == "3") // 32FS
                                                {
                                                    gvcustomerorder.Columns[8].Visible = true;
                                                }
                                                if (item.Value == "5") //34FS
                                                {
                                                    gvcustomerorder.Columns[9].Visible = true;
                                                }
                                                if (item.Value == "7") //36FS
                                                {
                                                    gvcustomerorder.Columns[10].Visible = true;
                                                }

                                                if (item.Value == "9") //XSFS
                                                {
                                                    gvcustomerorder.Columns[11].Visible = true;
                                                }
                                                if (item.Value == "21") //SFS
                                                {
                                                    gvcustomerorder.Columns[12].Visible = true;
                                                }
                                                if (item.Value == "23") // MFS
                                                {
                                                    gvcustomerorder.Columns[13].Visible = true;
                                                }
                                                if (item.Value == "11") // LFS
                                                {
                                                    gvcustomerorder.Columns[14].Visible = true;
                                                }
                                                if (item.Value == "13") //XLFS
                                                {
                                                    gvcustomerorder.Columns[15].Visible = true;
                                                }
                                                if (item.Value == "15") // XXLFS
                                                {
                                                    gvcustomerorder.Columns[16].Visible = true;
                                                }
                                                if (item.Value == "17") //3XLFS
                                                {
                                                    gvcustomerorder.Columns[17].Visible = true;
                                                }
                                                if (item.Value == "19") // 4XLFS
                                                {
                                                    gvcustomerorder.Columns[18].Visible = true;
                                                }





                                                if (item.Value == "2") // 30HS
                                                {
                                                    gvcustomerorder.Columns[19].Visible = true;
                                                }

                                                if (item.Value == "4") //32HS
                                                {
                                                    gvcustomerorder.Columns[20].Visible = true;
                                                }

                                                if (item.Value == "6") //34HS
                                                {
                                                    gvcustomerorder.Columns[21].Visible = true;
                                                }

                                                if (item.Value == "8") //36HS
                                                {
                                                    gvcustomerorder.Columns[22].Visible = true;
                                                }

                                                if (item.Value == "10") //XSHS
                                                {
                                                    gvcustomerorder.Columns[23].Visible = true;
                                                }
                                                if (item.Value == "22") //SHS
                                                {
                                                    gvcustomerorder.Columns[24].Visible = true;
                                                }

                                                if (item.Value == "24") // MHS
                                                {
                                                    gvcustomerorder.Columns[25].Visible = true;
                                                }

                                                if (item.Value == "12") // LHS
                                                {
                                                    gvcustomerorder.Columns[26].Visible = true;
                                                }

                                                if (item.Value == "14") //XLHS
                                                {
                                                    gvcustomerorder.Columns[27].Visible = true;
                                                }

                                                if (item.Value == "16") // XXLHS
                                                {
                                                    gvcustomerorder.Columns[28].Visible = true;
                                                }

                                                if (item.Value == "18") // 3XLHS
                                                {
                                                    gvcustomerorder.Columns[29].Visible = true;
                                                }

                                                if (item.Value == "20") // 4XLHS
                                                {
                                                    gvcustomerorder.Columns[30].Visible = true;
                                                }




                                                lop++;

                                            }
                                        }
                                    }
                                    //gvcustomerorder.DataSource = dssmer;
                                    //gvcustomerorder.DataBind();
                                }
                                else
                                {
                                    gvcustomerorder.Columns[7].Visible = false; //30FS
                                    gvcustomerorder.Columns[8].Visible = false; //32FS

                                    gvcustomerorder.Columns[9].Visible = false;//34Fs
                                    gvcustomerorder.Columns[10].Visible = false;//36Fs

                                    gvcustomerorder.Columns[11].Visible = false; //XSFS
                                    gvcustomerorder.Columns[12].Visible = false; //SFS

                                    gvcustomerorder.Columns[13].Visible = false; //MFS
                                    gvcustomerorder.Columns[14].Visible = false; //LFS

                                    gvcustomerorder.Columns[15].Visible = false; //XLFS
                                    gvcustomerorder.Columns[16].Visible = false; //XXLFS

                                    gvcustomerorder.Columns[17].Visible = false; //3XLFS
                                    gvcustomerorder.Columns[18].Visible = false; //4XLFS

                                    gvcustomerorder.Columns[19].Visible = false; //30HS
                                    gvcustomerorder.Columns[20].Visible = false; //32HS

                                    gvcustomerorder.Columns[21].Visible = false; //34Hs
                                    gvcustomerorder.Columns[22].Visible = false; //36Hs

                                    gvcustomerorder.Columns[23].Visible = false; //XSHS
                                    gvcustomerorder.Columns[24].Visible = false; //SHS


                                    gvcustomerorder.Columns[25].Visible = false;  //MHS
                                    gvcustomerorder.Columns[26].Visible = false;  //LHS


                                    gvcustomerorder.Columns[27].Visible = false; //XLHS
                                    gvcustomerorder.Columns[28].Visible = false; //XXLHS

                                    gvcustomerorder.Columns[29].Visible = false; //3XLHS
                                    gvcustomerorder.Columns[30].Visible = false; //4XLHS


                                }
                                //gvcustomerorder.Columns[10].Visible = false;
                                //gvcustomerorder.Columns[11].Visible = false;
                                //gvcustomerorder.Columns[13].Visible = false;

                            }
                        }
                        btnadd.Enabled = true;
                    }

                    #endregion
                }
                else
                {
                    gvcustomerorder.DataSource = null;
                    gvcustomerorder.DataBind();
                    btnadd.Enabled = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Not Ready To Ironing Process.Thank You!!!')", true);
                    return;
                }
                DataSet dsbrand = objbs.getbrand(ddlLotNo.SelectedItem.Text);
                if (dsbrand.Tables[0].Rows.Count > 0)
                {
                    lblbrand.Text = dsbrand.Tables[0].Rows[0]["BrandName"].ToString();
                }
                // btncalc_Click(sender,e);
            }
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
        protected void btncalc_Click(object sender, EventArgs e)
        {

            double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;
            double grandtotal = 0;
            double grandtotalamount = 0;

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                #region

                double total = 0;

                TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                TextBox txtRate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");



                TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                F30 = F30 + Convert.ToDouble(txts30fs.Text);
                TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                H30 = H30 + Convert.ToDouble(txts30hs.Text);

                TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                F32 = F32 + Convert.ToDouble(txts32fs.Text);
                TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                H32 = H32 + Convert.ToDouble(txts32hs.Text);

                TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                F34 = F34 + Convert.ToDouble(txts34fs.Text);
                TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                H34 = H34 + Convert.ToDouble(txts34hs.Text);

                TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                F36 = F36 + Convert.ToDouble(txts36fs.Text);
                TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                H36 = H36 + Convert.ToDouble(txts36hs.Text);

                TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                FXS = FXS + Convert.ToDouble(txtsxsfs.Text);
                TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");
                HXS = HXS + Convert.ToDouble(txtsxshs.Text);

                TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                FS = FS + Convert.ToDouble(txtssfs.Text);
                TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                HS = HS + Convert.ToDouble(txtsshs.Text);

                TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                FM = FM + Convert.ToDouble(txtsmfs.Text);
                TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                HM = HM + Convert.ToDouble(txtsmhs.Text);

                TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                FL = FL + Convert.ToDouble(txtslfs.Text);
                TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                HL = HL + Convert.ToDouble(txtslhs.Text);

                TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                FXL = FXL + Convert.ToDouble(txtsxlfs.Text);
                TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                HXL = HXL + Convert.ToDouble(txtsxlhs.Text);

                TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                FXXL = FXXL + Convert.ToDouble(txtsxxlfs.Text);
                TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                HXXL = HXXL + Convert.ToDouble(txtsxxlhs.Text);

                TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                F3XL = F3XL + Convert.ToDouble(txts3xlfs.Text);
                TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                H3XL = H3XL + Convert.ToDouble(txts3xlhs.Text);

                TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                F4XL = F4XL + Convert.ToDouble(txts4xlfs.Text);
                TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");
                H4XL = H4XL + Convert.ToDouble(txts4xlhs.Text);



                total = Convert.ToDouble(txts30fs.Text) + Convert.ToDouble(txts30hs.Text) + Convert.ToDouble(txts32fs.Text) + Convert.ToDouble(txts32hs.Text) + Convert.ToDouble(txts34fs.Text) + Convert.ToDouble(txts34hs.Text) + Convert.ToDouble(txts36fs.Text) + Convert.ToDouble(txts36hs.Text) + Convert.ToDouble(txtsxsfs.Text) + Convert.ToDouble(txtsxshs.Text) + Convert.ToDouble(txtslfs.Text) + Convert.ToDouble(txtslhs.Text) + Convert.ToDouble(txtssfs.Text) + Convert.ToDouble(txtsshs.Text) + Convert.ToDouble(txtsmfs.Text) + Convert.ToDouble(txtsmhs.Text) + Convert.ToDouble(txtsxlfs.Text) + Convert.ToDouble(txtsxlhs.Text) + Convert.ToDouble(txtsxxlfs.Text) + Convert.ToDouble(txtsxxlhs.Text) + Convert.ToDouble(txts3xlfs.Text) + Convert.ToDouble(txts3xlhs.Text) + Convert.ToDouble(txts4xlfs.Text) + Convert.ToDouble(txts4xlhs.Text);
                txtsendFQty.Text = total.ToString();
                grandtotal = grandtotal + total;
                grandtotalamount = grandtotalamount + (Convert.ToDouble(txtsendFQty.Text) * Convert.ToDouble(txtRate.Text));

                if (txtRate.Text == "" || txtRate.Text == "0" || txtRate.Text == "0.0000")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Assign  Rate For this Jobworker Else Contact Administrator.Thank you!!');", true);
                    return;
                }

                if (txtRate.Text == "" || txtRate.Text == "0" || txtRate.Text == "0.0000")
                {



                    if (txtsendFQty.Text == "0")
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate in  " + Convert.ToInt32(i + 1) + " Row . Thank you!!');", true);
                        return;
                    }
                }
                #endregion
            }
            lb30f.Text = F30.ToString();
            lb32f.Text = F32.ToString();
            lb34f.Text = F34.ToString();
            lb36f.Text = F36.ToString();
            lbxsf.Text = FXS.ToString();
            lbsf.Text = FS.ToString();
            lbmf.Text = FM.ToString();
            lblf.Text = FL.ToString();
            lbxlf.Text = FXL.ToString();
            lbxxlf.Text = FXXL.ToString();
            lb3xlf.Text = F3XL.ToString();
            lb4xlf.Text = F4XL.ToString();


            lb30h.Text = H30.ToString();
            lb32h.Text = H32.ToString();
            lb34h.Text = H34.ToString();
            lb36h.Text = H36.ToString();
            lbxsh.Text = HXS.ToString();
            lbsh.Text = HS.ToString();
            lbmh.Text = HM.ToString();
            lblh.Text = HL.ToString();
            lbxlh.Text = HXL.ToString();
            lbxxlh.Text = HXXL.ToString();
            lb3xlh.Text = H3XL.ToString();
            lb4xlh.Text = H4XL.ToString();

            LabelTotal.Text = grandtotal.ToString();
            txtAmount.Text = grandtotalamount.ToString("f2");
            txttotalqty.Text = grandtotal.ToString();

            int Fironacc = 0;
            int Hironacc = 0;

            int orgacc = Convert.ToInt32(txttotalqty.Text);

            Fironacc = Convert.ToInt32(lb30f.Text) + Convert.ToInt32(lb32f.Text) + Convert.ToInt32(lb34f.Text) + Convert.ToInt32(lb36f.Text) + Convert.ToInt32(lbxsf.Text) + Convert.ToInt32(lbsf.Text) + Convert.ToInt32(lbmf.Text) + Convert.ToInt32(lblf.Text) + Convert.ToInt32(lbxlf.Text) + Convert.ToInt32(lbxxlf.Text) + Convert.ToInt32(lb3xlf.Text) + Convert.ToInt32(lb4xlf.Text);
            Hironacc = Convert.ToInt32(lb30h.Text) + Convert.ToInt32(lb32h.Text) + Convert.ToInt32(lb34h.Text) + Convert.ToInt32(lb36h.Text) + Convert.ToInt32(lbxsh.Text) + Convert.ToInt32(lbsh.Text) + Convert.ToInt32(lbmh.Text) + Convert.ToInt32(lblh.Text) + Convert.ToInt32(lbxlh.Text) + Convert.ToInt32(lbxxlh.Text) + Convert.ToInt32(lb3xlh.Text) + Convert.ToInt32(lb4xlh.Text);

            for (int a = 0; a < gvironstock.Rows.Count; a++)
            {
                CheckBox chkiron = (CheckBox)gvironstock.Rows[a].FindControl("chkiron");
                TextBox txtissueironstock = (TextBox)gvironstock.Rows[a].FindControl("txtissueironstock");

                Label lblDefinition = (Label)gvironstock.Rows[a].FindControl("lblDefinition");
                Label txtironstock = (Label)gvironstock.Rows[a].FindControl("txtironstock");
                Label lblCategoryUserID = (Label)gvironstock.Rows[a].FindControl("lblCategoryUserID");

                if (lblCategoryUserID.Text == "451")
                {
                    txtissueironstock.Text = ((Fironacc * 2) + (Hironacc * 3)).ToString();
                }
                else
                {
                    txtissueironstock.Text = (Fironacc + Hironacc).ToString();
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
            //if (btnadd.Text == "Save")
            //{
            //    //if (drpmultiunit.SelectedValue == "All")
            //    //{
            //    //    dsLotNo = objbs.Select_Lotnewstich();//tblCut
            //    //}
            //    //else
            //    {
            //        dsLotNo = objbs.Select_Lotnewstich(drpmultiunit.SelectedValue);//tblCut
            //    }
            //}

            //if (btnadd.Text == "Update")
            //{
            //    dsLotNo = objbs.Select_Lotnewstichupdate(drpmultiunit.SelectedValue);

            //    //if (e.Row.RowType == DataControlRowType.DataRow)
            //    //{
            //    //    if (Convert.ToString(e.Row.Cells[4].Text) != "")
            //    //    {
            //    //        if (Convert.ToDouble(e.Row.Cells[4].Text) == 0)

            //    //            e.Row.Visible = false;
            //    //    }
            //    //}
            //}
            //if (btnadd.Text == "Received")
            //{
            //    dsLotNo = objbs.Select_Lotnewstichupdate(drpmultiunit.SelectedValue);
            //}



            drpProcess = objbs.SelectAllProcessTypeLotProcess("6");



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
                double total = 0;
                // DropDownList drplot = (DropDownList)e.Row.FindControl("drpLotno");
                //  DropDownList drpEmp1 = (DropDownList)e.Row.FindControl("drpEmp");

                var ddProcess = (DropDownList)e.Row.FindControl("drpProcess");
                ddProcess.DataSource = drpProcess;
                ddProcess.DataTextField = "ProcessType";
                ddProcess.DataValueField = "ProcessMasterID";
                ddProcess.DataBind();
                ddProcess.Items.Insert(0, "Select Process Type");

                //var drplot = (DropDownList)e.Row.FindControl("drpLotno");
                //if (dsLotNo.Tables[0].Rows.Count > 0)
                //{
                //    drplot.DataSource = dsLotNo.Tables[0];
                //    drplot.DataTextField = "LotNo";
                //    drplot.DataValueField = "cutid";
                //    drplot.DataBind();
                //    drplot.Items.Insert(0, "Select Lot No");
                //}


                //var ddEmp = (DropDownList)e.Row.FindControl("drpEmp");
                //ddEmp.DataSource = drpEmp;
                //ddEmp.DataTextField = "Name";
                //ddEmp.DataValueField = "Employee_Id";
                //ddEmp.DataBind();
                //ddEmp.Items.Insert(0, "Select Employee Name");

                TextBox txtsendFQty = (TextBox)e.Row.FindControl("txtsendFQty");

                TextBox txts30fs = (TextBox)e.Row.FindControl("txts30fs");
                TextBox txts30hs = (TextBox)e.Row.FindControl("txts30hs");
                TextBox txts32fs = (TextBox)e.Row.FindControl("txts32fs");
                TextBox txts32hs = (TextBox)e.Row.FindControl("txts32hs");
                TextBox txts34fs = (TextBox)e.Row.FindControl("txts34fs");
                TextBox txts34hs = (TextBox)e.Row.FindControl("txts34hs");
                TextBox txts36fs = (TextBox)e.Row.FindControl("txts36fs");
                TextBox txts36hs = (TextBox)e.Row.FindControl("txts36hs");
                TextBox txtsxsfs = (TextBox)e.Row.FindControl("txtsxsfs");
                TextBox txtsxshs = (TextBox)e.Row.FindControl("txtsxshs");

                TextBox txtslfs = (TextBox)e.Row.FindControl("txtslfs");
                TextBox txtslhs = (TextBox)e.Row.FindControl("txtslhs");
                TextBox txtssfs = (TextBox)e.Row.FindControl("txtssfs");
                TextBox txtsshs = (TextBox)e.Row.FindControl("txtsshs");
                TextBox txtsmfs = (TextBox)e.Row.FindControl("txtsmfs");
                TextBox txtsmhs = (TextBox)e.Row.FindControl("txtsmhs");
                TextBox txtsxlfs = (TextBox)e.Row.FindControl("txtsxlfs");
                TextBox txtsxlhs = (TextBox)e.Row.FindControl("txtsxlhs");
                TextBox txtsxxlfs = (TextBox)e.Row.FindControl("txtsxxlfs");
                TextBox txtsxxlhs = (TextBox)e.Row.FindControl("txtsxxlhs");
                TextBox txts3xlfs = (TextBox)e.Row.FindControl("txts3xlfs");
                TextBox txts3xlhs = (TextBox)e.Row.FindControl("txts3xlhs");
                TextBox txts4xlfs = (TextBox)e.Row.FindControl("txts4xlfs");
                TextBox txts4xlhs = (TextBox)e.Row.FindControl("txts4xlhs");

                total = Convert.ToDouble(txts30fs.Text) + Convert.ToDouble(txts30hs.Text) + Convert.ToDouble(txts32fs.Text) + Convert.ToDouble(txts32hs.Text) + Convert.ToDouble(txts34fs.Text) + Convert.ToDouble(txts34hs.Text) + Convert.ToDouble(txts36fs.Text) + Convert.ToDouble(txts36hs.Text) + Convert.ToDouble(txtsxsfs.Text) + Convert.ToDouble(txtsxshs.Text) + Convert.ToDouble(txtslfs.Text) + Convert.ToDouble(txtslhs.Text) + Convert.ToDouble(txtssfs.Text) + Convert.ToDouble(txtsshs.Text) + Convert.ToDouble(txtsmfs.Text) + Convert.ToDouble(txtsmhs.Text) + Convert.ToDouble(txtsxlfs.Text) + Convert.ToDouble(txtsxlhs.Text) + Convert.ToDouble(txtsxxlfs.Text) + Convert.ToDouble(txtsxxlhs.Text) + Convert.ToDouble(txts3xlfs.Text) + Convert.ToDouble(txts3xlhs.Text) + Convert.ToDouble(txts4xlfs.Text) + Convert.ToDouble(txts4xlhs.Text);
                grandtotal = grandtotal + total;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {

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

                        //DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpLotno");
                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendFQty");
                        TextBox txtremainQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRemainQty");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtrecFQty");
                        TextBox txtRate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRate");
                        Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                        Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                        Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");

                        Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                        Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");
                        TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
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
                        //dtCurrentTable.Rows[i - 1]["LotNo"] = drpLotno.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["damageqty"] = txtdamageqty.Text;
                        dtCurrentTable.Rows[i - 1]["fit"] = lblfit.Text;
                        dtCurrentTable.Rows[i - 1]["itemname"] = lblitemname.Text;
                        dtCurrentTable.Rows[i - 1]["Pattern"] = lblPattern.Text;
                        dtCurrentTable.Rows[i - 1]["Patternid"] = lblPatternid.Text;
                        dtCurrentTable.Rows[i - 1]["fitid"] = lblfitid.Text;
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

                        //DropDownList drpLotno = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpLotno");
                        DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[rowIndex].FindControl("drpProcess");
                        TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtsendFQty");
                        TextBox txtremainQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRemainQty");
                        TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtrecFQty");
                        TextBox txtRate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("txtRate");
                        Label lblPattern = (Label)gvcustomerorder.Rows[i].FindControl("lblPattern");
                        Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");
                        Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                        TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                        TextBox date = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("date");
                        TextBox Recdate = (TextBox)gvcustomerorder.Rows[rowIndex].FindControl("Recdate");

                        Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                        Label lblfit = (Label)gvcustomerorder.Rows[i].FindControl("lblfit");

                        drpProcess.SelectedValue = dt.Rows[i]["Process"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtrecFQty.Text = dt.Rows[i]["RecQuantity"].ToString();
                        //drpLotno.SelectedValue = dt.Rows[i]["LotNo"].ToString();
                        date.Text = dt.Rows[i]["date"].ToString();
                        Recdate.Text = dt.Rows[i]["Recdate"].ToString();
                        txtdamageqty.Text = dt.Rows[i]["Damageqty"].ToString();
                        txtsendFQty.Text = dt.Rows[i]["SendQty"].ToString();
                        txtremainQty.Text = dt.Rows[i]["RemainQty"].ToString();
                        lblPattern.Text = dt.Rows[i]["Pattern"].ToString();
                        lblPatternid.Text = dt.Rows[i]["Patternid"].ToString();
                        lblitemname.Text = dt.Rows[i]["itemname"].ToString();
                        lblfit.Text = dt.Rows[i]["fit"].ToString();
                        lblfitid.Text = dt.Rows[i]["fitid"].ToString();
                        rowIndex++;
                        drpProcess.Focus();
                    }
                }
            }
        }

        protected void Add_LotProcessDetails(object sender, EventArgs e)
        {

            #region Calculation
            double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;
            double grandtotal = 0;
            double grandtotalamount = 0;

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {
                #region

                double total = 0;

                TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                TextBox txtRate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");



                TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                F30 = F30 + Convert.ToDouble(txts30fs.Text);
                TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                H30 = H30 + Convert.ToDouble(txts30hs.Text);

                TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                F32 = F32 + Convert.ToDouble(txts32fs.Text);
                TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                H32 = H32 + Convert.ToDouble(txts32hs.Text);

                TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                F34 = F34 + Convert.ToDouble(txts34fs.Text);
                TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                H34 = H34 + Convert.ToDouble(txts34hs.Text);

                TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                F36 = F36 + Convert.ToDouble(txts36fs.Text);
                TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                H36 = H36 + Convert.ToDouble(txts36hs.Text);

                TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                FXS = FXS + Convert.ToDouble(txtsxsfs.Text);
                TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");
                HXS = HXS + Convert.ToDouble(txtsxshs.Text);

                TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                FS = FS + Convert.ToDouble(txtssfs.Text);
                TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                HS = HS + Convert.ToDouble(txtsshs.Text);

                TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                FM = FM + Convert.ToDouble(txtsmfs.Text);
                TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                HM = HM + Convert.ToDouble(txtsmhs.Text);

                TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                FL = FL + Convert.ToDouble(txtslfs.Text);
                TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                HL = HL + Convert.ToDouble(txtslhs.Text);

                TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                FXL = FXL + Convert.ToDouble(txtsxlfs.Text);
                TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                HXL = HXL + Convert.ToDouble(txtsxlhs.Text);

                TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                FXXL = FXXL + Convert.ToDouble(txtsxxlfs.Text);
                TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                HXXL = HXXL + Convert.ToDouble(txtsxxlhs.Text);

                TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                F3XL = F3XL + Convert.ToDouble(txts3xlfs.Text);
                TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                H3XL = H3XL + Convert.ToDouble(txts3xlhs.Text);

                TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                F4XL = F4XL + Convert.ToDouble(txts4xlfs.Text);
                TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");
                H4XL = H4XL + Convert.ToDouble(txts4xlhs.Text);



                total = Convert.ToDouble(txts30fs.Text) + Convert.ToDouble(txts30hs.Text) + Convert.ToDouble(txts32fs.Text) + Convert.ToDouble(txts32hs.Text) + Convert.ToDouble(txts34fs.Text) + Convert.ToDouble(txts34hs.Text) + Convert.ToDouble(txts36fs.Text) + Convert.ToDouble(txts36hs.Text) + Convert.ToDouble(txtsxsfs.Text) + Convert.ToDouble(txtsxshs.Text) + Convert.ToDouble(txtslfs.Text) + Convert.ToDouble(txtslhs.Text) + Convert.ToDouble(txtssfs.Text) + Convert.ToDouble(txtsshs.Text) + Convert.ToDouble(txtsmfs.Text) + Convert.ToDouble(txtsmhs.Text) + Convert.ToDouble(txtsxlfs.Text) + Convert.ToDouble(txtsxlhs.Text) + Convert.ToDouble(txtsxxlfs.Text) + Convert.ToDouble(txtsxxlhs.Text) + Convert.ToDouble(txts3xlfs.Text) + Convert.ToDouble(txts3xlhs.Text) + Convert.ToDouble(txts4xlfs.Text) + Convert.ToDouble(txts4xlhs.Text);
                txtsendFQty.Text = total.ToString();
                grandtotal = grandtotal + total;
                grandtotalamount = grandtotalamount + (Convert.ToDouble(txtsendFQty.Text) * Convert.ToDouble(txtRate.Text));

                if (txtRate.Text == "" || txtRate.Text == "0" || txtRate.Text == "0.0000")
                {
                    if (txtsendFQty.Text == "0")
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Rate in  " + Convert.ToInt32(i + 1) + " Row . Thank you!!');", true);
                        return;
                    }
                }
                #endregion
            }
            lb30f.Text = F30.ToString();
            lb32f.Text = F32.ToString();
            lb34f.Text = F34.ToString();
            lb36f.Text = F36.ToString();
            lbxsf.Text = FXS.ToString();
            lbsf.Text = FS.ToString();
            lbmf.Text = FM.ToString();
            lblf.Text = FL.ToString();
            lbxlf.Text = FXL.ToString();
            lbxxlf.Text = FXXL.ToString();
            lb3xlf.Text = F3XL.ToString();
            lb4xlf.Text = F4XL.ToString();


            lb30h.Text = H30.ToString();
            lb32h.Text = H32.ToString();
            lb34h.Text = H34.ToString();
            lb36h.Text = H36.ToString();
            lbxsh.Text = HXS.ToString();
            lbsh.Text = HS.ToString();
            lbmh.Text = HM.ToString();
            lblh.Text = HL.ToString();
            lbxlh.Text = HXL.ToString();
            lbxxlh.Text = HXXL.ToString();
            lb3xlh.Text = H3XL.ToString();
            lb4xlh.Text = H4XL.ToString();

            LabelTotal.Text = grandtotal.ToString();
            txtAmount.Text = grandtotalamount.ToString("f2");
            txttotalqty.Text = grandtotal.ToString();

            #endregion
            if (btnadd.Text == "Save")
            {
                DateTime MultiDate = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime cutdate = DateTime.ParseExact(lblcuttingdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (Convert.ToDateTime(cutdate) > Convert.ToDateTime(MultiDate))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Cutting Date And Ironing Issue date MisMatch.Thank you!!!');", true);
                    return;

                }
                else
                {

                }
            }
            else if (btnadd.Text == "Received")
            {


                DateTime MultiDate = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                TextBox Recdate = (TextBox)gvcustomerorder.Rows[0].FindControl("Recdate");
                DateTime Rdate = DateTime.ParseExact(Recdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (Convert.ToDateTime(MultiDate) > Convert.ToDateTime(Rdate))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Ironing Issue Date And Ironing Receive date MisMatch.Thank you!!!');", true);
                    return;

                }
                else
                {

                }

            }


            for (int a = 0; a < gvironstock.Rows.Count; a++)
            {
                #region
                CheckBox chkiron = (CheckBox)gvironstock.Rows[a].FindControl("chkiron");
                TextBox txtissueironstock = (TextBox)gvironstock.Rows[a].FindControl("txtissueironstock");

                Label lblDefinition = (Label)gvironstock.Rows[a].FindControl("lblDefinition");
                Label txtironstock = (Label)gvironstock.Rows[a].FindControl("txtironstock");
                Label lblCategoryUserID = (Label)gvironstock.Rows[a].FindControl("lblCategoryUserID");
                if (chkiron.Checked == true)
                {
                    if (Convert.ToInt32(txtissueironstock.Text) > Convert.ToInt32(txtironstock.Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + lblDefinition.Text + " is Greater than the Stock" + ".Thank You!!!.')", true);
                        return;
                    }
                    else
                    {

                    }
                }
                #endregion
            }

            for (int r = 0; r < gvcustomerorder.Rows.Count; r++)
            {



                DataSet dssmaster = new DataSet();
                Label lbldesignno = (Label)gvcustomerorder.Rows[r].FindControl("lbldesignno");
                Label lblStockRatioId = (Label)gvcustomerorder.Rows[r].FindControl("lblStockRatioId");

                if (btnadd.Text == "Save")
                {
                    dssmaster = objbs.getMasterStockRatiovalues(Convert.ToString(ddlLotNo.SelectedValue), lbldesignno.Text, Convert.ToInt32(lblStockRatioId.Text));

                    #region
                    TextBox rtxts30fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts30fs");
                    if (Convert.ToInt32(rtxts30fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R30FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 30FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts30hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts30hs");
                    if (Convert.ToInt32(rtxts30hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R30HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 30HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts32fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts32fs");
                    if (Convert.ToInt32(rtxts32fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R32FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 32FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts32hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts32hs");
                    if (Convert.ToInt32(rtxts32hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R32HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 32HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts34fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts34fs");
                    if (Convert.ToInt32(rtxts34fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R34FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 34FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts34hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts34hs");
                    if (Convert.ToInt32(rtxts34hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R34HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 34HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts36fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts36fs");
                    if (Convert.ToInt32(rtxts36fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R36FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 36FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts36hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts36hs");
                    if (Convert.ToInt32(rtxts36hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R36HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 36HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxsfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxsfs");
                    if (Convert.ToInt32(rtxtsxsfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RXSFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XSFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxshs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxshs");
                    if (Convert.ToInt32(rtxts30fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RXSHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XSHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtssfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtssfs");
                    if (Convert.ToInt32(rtxtssfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RSFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " SFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsshs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsshs");
                    if (Convert.ToInt32(rtxts30fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RSHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " SHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsmfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsmfs");
                    if (Convert.ToInt32(rtxtsmfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RMFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " MFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsmhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsmhs");
                    if (Convert.ToInt32(rtxtsmhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RMHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " MHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtslfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtslfs");
                    if (Convert.ToInt32(rtxtslfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " LFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtslhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtslhs");
                    if (Convert.ToInt32(rtxtslhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " LHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxlfs");
                    if (Convert.ToInt32(rtxtsxlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RXLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XL0FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxlhs");
                    if (Convert.ToInt32(rtxtsxlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RXLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XLHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxxlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxxlfs");
                    if (Convert.ToInt32(rtxtsxxlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RXXLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XXLFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxxlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxxlhs");
                    if (Convert.ToInt32(rtxtsxxlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RXXLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XXLHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts3xlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts3xlfs");
                    if (Convert.ToInt32(rtxts3xlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R3XLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 3XLFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts3xlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts3xlhs");
                    if (Convert.ToInt32(rtxts3xlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R3XLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 3XLHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts4xlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts4xlfs");
                    if (Convert.ToInt32(rtxts4xlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R4XLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 4XLFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts4xlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts4xlhs");
                    if (Convert.ToInt32(rtxts4xlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["R4XLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 4XLHS. Thank you');", true);
                        return;
                    }
                    #endregion
                }
                else if (btnadd.Text == "Received")
                {
                    Label lblIronTransID = (Label)gvcustomerorder.Rows[r].FindControl("lblIronTransID");
                    dssmaster = objbs.getMasterStockRatiovalues123(ViewState["Ironingid"].ToString(), lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));

                    #region
                    TextBox rtxts30fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts30fs");
                    if (Convert.ToInt32(rtxts30fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec30FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 30FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts30hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts30hs");
                    if (Convert.ToInt32(rtxts30hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec30HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 30HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts32fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts32fs");
                    if (Convert.ToInt32(rtxts32fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec32FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 32FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts32hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts32hs");
                    if (Convert.ToInt32(rtxts32hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec32HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 32HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts34fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts34fs");
                    if (Convert.ToInt32(rtxts34fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec34FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 34FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts34hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts34hs");
                    if (Convert.ToInt32(rtxts34hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec34HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 34HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts36fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts36fs");
                    if (Convert.ToInt32(rtxts36fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec36FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 36FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts36hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts36hs");
                    if (Convert.ToInt32(rtxts36hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec36HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 36HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxsfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxsfs");
                    if (Convert.ToInt32(rtxtsxsfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXSFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XSFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxshs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxshs");
                    if (Convert.ToInt32(rtxts30fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXSHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XSHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtssfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtssfs");
                    if (Convert.ToInt32(rtxtssfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecSFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " SFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsshs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsshs");
                    if (Convert.ToInt32(rtxts30fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecSHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " SHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsmfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsmfs");
                    if (Convert.ToInt32(rtxtsmfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecMFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " MFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsmhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsmhs");
                    if (Convert.ToInt32(rtxtsmhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecMHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " MHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtslfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtslfs");
                    if (Convert.ToInt32(rtxtslfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " LFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtslhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtslhs");
                    if (Convert.ToInt32(rtxtslhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " LHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxlfs");
                    if (Convert.ToInt32(rtxtsxlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XL0FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxlhs");
                    if (Convert.ToInt32(rtxtsxlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XLHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxxlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxxlfs");
                    if (Convert.ToInt32(rtxtsxxlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXXLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XXLFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxxlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxxlhs");
                    if (Convert.ToInt32(rtxtsxxlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXXLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XXLHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts3xlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts3xlfs");
                    if (Convert.ToInt32(rtxts3xlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec3XLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 3XLFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts3xlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts3xlhs");
                    if (Convert.ToInt32(rtxts3xlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec3XLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 3XLHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts4xlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts4xlfs");
                    if (Convert.ToInt32(rtxts4xlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec4XLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 4XLFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts4xlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts4xlhs");
                    if (Convert.ToInt32(rtxts4xlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec4XLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 4XLHS. Thank you');", true);
                        return;
                    }
                    #endregion

                }
                else if (btnadd.Text == "Receive")
                {
                    Label lblIronTransID = (Label)gvcustomerorder.Rows[r].FindControl("lblIronTransID");
                    dssmaster = objbs.getMasterStockRatiovaluesnew(ViewState["Ironingid"].ToString(), lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));

                    #region
                    TextBox rtxts30fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts30fs");
                    if (Convert.ToInt32(rtxts30fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec30FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 30FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts30hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts30hs");
                    if (Convert.ToInt32(rtxts30hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec30HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 30HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts32fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts32fs");
                    if (Convert.ToInt32(rtxts32fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec32FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 32FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts32hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts32hs");
                    if (Convert.ToInt32(rtxts32hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec32HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 32HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts34fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts34fs");
                    if (Convert.ToInt32(rtxts34fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec34FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 34FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts34hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts34hs");
                    if (Convert.ToInt32(rtxts34hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec34HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 34HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts36fs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts36fs");
                    if (Convert.ToInt32(rtxts36fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec36FS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 36FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts36hs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts36hs");
                    if (Convert.ToInt32(rtxts36hs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec36HS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 36HS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxsfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxsfs");
                    if (Convert.ToInt32(rtxtsxsfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXSFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XSFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxshs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxshs");
                    if (Convert.ToInt32(rtxts30fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXSHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XSHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtssfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtssfs");
                    if (Convert.ToInt32(rtxtssfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecSFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " SFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsshs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsshs");
                    if (Convert.ToInt32(rtxts30fs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecSHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " SHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsmfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsmfs");
                    if (Convert.ToInt32(rtxtsmfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecMFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " MFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsmhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsmhs");
                    if (Convert.ToInt32(rtxtsmhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecMHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " MHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtslfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtslfs");
                    if (Convert.ToInt32(rtxtslfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " LFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtslhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtslhs");
                    if (Convert.ToInt32(rtxtslhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " LHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxlfs");
                    if (Convert.ToInt32(rtxtsxlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XL0FS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxlhs");
                    if (Convert.ToInt32(rtxtsxlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XLHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxxlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxxlfs");
                    if (Convert.ToInt32(rtxtsxxlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXXLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XXLFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxtsxxlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txtsxxlhs");
                    if (Convert.ToInt32(rtxtsxxlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["RecXXLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " XXLHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts3xlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts3xlfs");
                    if (Convert.ToInt32(rtxts3xlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec3XLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 3XLFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts3xlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts3xlhs");
                    if (Convert.ToInt32(rtxts3xlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec3XLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 3XLHS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts4xlfs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts4xlfs");
                    if (Convert.ToInt32(rtxts4xlfs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec4XLFS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 4XLFS. Thank you');", true);
                        return;
                    }
                    TextBox rtxts4xlhs = (TextBox)gvcustomerorder.Rows[r].FindControl("txts4xlhs");
                    if (Convert.ToInt32(rtxts4xlhs.Text) > Convert.ToDouble(dssmaster.Tables[0].Rows[0]["Rec4XLHS"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Quantity is Greater for " + lbldesignno.Text + " 4XLHS. Thank you');", true);
                        return;
                    }
                    #endregion

                }


            }



            if (btnadd.Text == "Receive")
            {
                if (ddltype.SelectedValue == "Type")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Type.Thank You!!!.')", true);
                    ddltype.Focus();
                    return;
                }

                #region

                int iq = 1;
                int iii = 1;
                string itemc = string.Empty;
                string itemd = string.Empty;
                string iteme = string.Empty;
                string itemcd = string.Empty;
                #region Table
                DataSet temp = new DataSet();
                DataTable dtt = new DataTable();
                dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                dtt.Columns.Add(new DataColumn("senddate", typeof(string)));
                dtt.Columns.Add(new DataColumn("recdate", typeof(string)));
                dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                dtt.Columns.Add(new DataColumn("Fitid", typeof(string)));
                dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                dtt.Columns.Add(new DataColumn("Patternid", typeof(string)));
                dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
                dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                dtt.Columns.Add(new DataColumn("ProcessTypeID", typeof(string)));
                dtt.Columns.Add(new DataColumn("rate", typeof(string)));
                dtt.Columns.Add(new DataColumn("damageqty", typeof(string)));
                //   temp.Tables.Add(dtt);
                dtt.Columns.Add(new DataColumn("30hs", typeof(string)));
                dtt.Columns.Add(new DataColumn("32hs", typeof(string)));
                dtt.Columns.Add(new DataColumn("34hs", typeof(string)));
                dtt.Columns.Add(new DataColumn("36hs", typeof(string)));
                dtt.Columns.Add(new DataColumn("30fs", typeof(string)));
                dtt.Columns.Add(new DataColumn("32fs", typeof(string)));
                dtt.Columns.Add(new DataColumn("34fs", typeof(string)));
                dtt.Columns.Add(new DataColumn("36fs", typeof(string)));
                dtt.Columns.Add(new DataColumn("shs", typeof(string)));
                dtt.Columns.Add(new DataColumn("mhs", typeof(string)));
                dtt.Columns.Add(new DataColumn("lhs", typeof(string)));
                dtt.Columns.Add(new DataColumn("lfs", typeof(string)));
                dtt.Columns.Add(new DataColumn("mfs", typeof(string)));
                dtt.Columns.Add(new DataColumn("sfs", typeof(string)));
                dtt.Columns.Add(new DataColumn("xlfs", typeof(string)));
                dtt.Columns.Add(new DataColumn("xxlfs", typeof(string)));
                dtt.Columns.Add(new DataColumn("3xlfs", typeof(string)));
                dtt.Columns.Add(new DataColumn("4xlfs", typeof(string)));

                dtt.Columns.Add(new DataColumn("xsfs", typeof(string)));
                dtt.Columns.Add(new DataColumn("xshs", typeof(string)));
                dtt.Columns.Add(new DataColumn("xlhs", typeof(string)));
                dtt.Columns.Add(new DataColumn("xxlhs", typeof(string)));
                dtt.Columns.Add(new DataColumn("3xlhs", typeof(string)));
                dtt.Columns.Add(new DataColumn("4xlhs", typeof(string)));

                temp.Tables.Add(dtt);
                #endregion
                ds = objbs.Get_TransIorningDetails(Convert.ToInt32(ViewState["Ironingid"].ToString()));

                int istasHistoryid = objbs.processmaintableironing(Convert.ToInt32(ViewState["Ironingid"].ToString()), Convert.ToInt32(txttotalqty.Text), Convert.ToDouble(txtAmount.Text), ddltype.SelectedItem.Text);


                {
                    string curent = string.Empty;
                    string namee = string.Empty;

                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {

                        int ReceivedQty = 0, SendQty = 0, RemainingQty = 0;
                        TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                        TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                        TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");


                        double qty = 0;
                        double qty1 = 0;
                        qty1 = Convert.ToDouble(txtRemainQty.Text);
                        qty = Convert.ToDouble(txtdamageqty.Text) + Convert.ToDouble(txtrecQty.Text);

                        if (qty1 < qty)
                        {
                            //////ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Received Qty is not Greater than That Remain Qty.Thank You!!!.')", true);
                            //////txtrecQty.Focus();
                            //////return;
                        }

                        #region Load Previous Data from Table
                        // if (Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"].ToString()) != 0)
                        {
                            DataRow dr = dtt.NewRow();

                            dr["OrderNo"] = "0";
                            dr["Transid"] = ds.Tables[0].Rows[i]["Transid"].ToString();
                            dr["fitid"] = ds.Tables[0].Rows[i]["fitid"].ToString();
                            dr["fit"] = ds.Tables[0].Rows[i]["Fitname"].ToString();
                            dr["ProcessTypeID"] = "6";//ds.Tables[0].Rows[i]["ProcessID"].ToString();
                            dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                            dr["RecQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RecQty"]);
                            dr["TotalQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["totalQty"]);
                            dr["RemainQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"]);
                            dr["senddate"] = ds.Tables[0].Rows[i]["senddate"].ToString();
                            dr["RecQty"] = "0";
                            dr["Recdate"] = "0";
                            dr["Damageqty"] = "0";
                            dr["patternid"] = ds.Tables[0].Rows[i]["patternid"].ToString();
                            dr["pattern"] = ds.Tables[0].Rows[i]["patternname"].ToString();
                            dr["itemname"] = ds.Tables[0].Rows[i]["itemname"].ToString();

                            dr["30fs"] = ds.Tables[0].Rows[i]["Rec30fs"].ToString();
                            dr["30hs"] = ds.Tables[0].Rows[i]["Rec30hs"].ToString();
                            dr["32fs"] = ds.Tables[0].Rows[i]["Rec32fs"].ToString();
                            dr["32hs"] = ds.Tables[0].Rows[i]["Rec32hs"].ToString();
                            dr["34hs"] = ds.Tables[0].Rows[i]["Rec34hs"].ToString();
                            dr["34fs"] = ds.Tables[0].Rows[i]["Rec34fs"].ToString();
                            dr["36hs"] = ds.Tables[0].Rows[i]["Rec36hs"].ToString();
                            dr["36fs"] = ds.Tables[0].Rows[i]["Rec36fs"].ToString();

                            dr["xshs"] = ds.Tables[0].Rows[i]["Recxshs"].ToString();
                            dr["xsfs"] = ds.Tables[0].Rows[i]["Recxsfs"].ToString();
                            dr["shs"] = ds.Tables[0].Rows[i]["Recshs"].ToString();
                            dr["sfs"] = ds.Tables[0].Rows[i]["Recsfs"].ToString();
                            dr["mhs"] = ds.Tables[0].Rows[i]["Recmhs"].ToString();
                            dr["mfs"] = ds.Tables[0].Rows[i]["Recmfs"].ToString();
                            dr["lhs"] = ds.Tables[0].Rows[i]["Reclhs"].ToString();
                            dr["lfs"] = ds.Tables[0].Rows[i]["Reclfs"].ToString();
                            dr["xxlhs"] = ds.Tables[0].Rows[i]["Recxxlhs"].ToString();
                            dr["xxlfs"] = ds.Tables[0].Rows[i]["Recxxlfs"].ToString();

                            dr["xlhs"] = ds.Tables[0].Rows[i]["Recxlhs"].ToString();
                            dr["xlfs"] = ds.Tables[0].Rows[i]["Recxlfs"].ToString();
                            dr["3xlhs"] = ds.Tables[0].Rows[i]["Rec3xlhs"].ToString();
                            dr["3xlfs"] = ds.Tables[0].Rows[i]["Rec3xlfs"].ToString();
                            dr["4xlhs"] = ds.Tables[0].Rows[i]["Rec4xlhs"].ToString();
                            dr["4xlfs"] = ds.Tables[0].Rows[i]["Rec4xlfs"].ToString();
                            temp.Tables[0].Rows.Add(dr);

                            int TransId = Convert.ToInt32(temp.Tables[0].Rows[i]["Transid"].ToString());
                            int TRecQty = Convert.ToInt32(temp.Tables[0].Rows[i]["RecQty"].ToString());
                            int T30fs = Convert.ToInt32(temp.Tables[0].Rows[i]["30fs"].ToString());
                            int T30hs = Convert.ToInt32(temp.Tables[0].Rows[i]["30hs"].ToString());
                            int T32fs = Convert.ToInt32(temp.Tables[0].Rows[i]["32fs"].ToString());
                            int T32hs = Convert.ToInt32(temp.Tables[0].Rows[i]["32hs"].ToString());
                            int T34hs = Convert.ToInt32(temp.Tables[0].Rows[i]["34hs"].ToString());
                            int T34fs = Convert.ToInt32(temp.Tables[0].Rows[i]["34fs"].ToString());
                            int T36hs = Convert.ToInt32(temp.Tables[0].Rows[i]["36hs"].ToString());
                            int T36fs = Convert.ToInt32(temp.Tables[0].Rows[i]["36fs"].ToString());

                            int Txshs = Convert.ToInt32(temp.Tables[0].Rows[i]["xshs"].ToString());
                            int Txsfs = Convert.ToInt32(temp.Tables[0].Rows[i]["xsfs"].ToString());
                            int Tshs = Convert.ToInt32(temp.Tables[0].Rows[i]["shs"].ToString());
                            int Tsfs = Convert.ToInt32(temp.Tables[0].Rows[i]["sfs"].ToString());
                            int Tmhs = Convert.ToInt32(temp.Tables[0].Rows[i]["mhs"].ToString());
                            int Tmfs = Convert.ToInt32(temp.Tables[0].Rows[i]["mfs"].ToString());
                            int Tlhs = Convert.ToInt32(temp.Tables[0].Rows[i]["lhs"].ToString());
                            int Tlfs = Convert.ToInt32(temp.Tables[0].Rows[i]["lfs"].ToString());
                            int Txxlhs = Convert.ToInt32(temp.Tables[0].Rows[i]["xxlhs"].ToString());
                            int Txxlfs = Convert.ToInt32(temp.Tables[0].Rows[i]["xxlfs"].ToString());

                            int Txlhs = Convert.ToInt32(temp.Tables[0].Rows[i]["xlhs"].ToString());
                            int Txlfs = Convert.ToInt32(temp.Tables[0].Rows[i]["xlfs"].ToString());
                            int T3xlhs = Convert.ToInt32(temp.Tables[0].Rows[i]["3xlhs"].ToString());
                            int T3xlfs = Convert.ToInt32(temp.Tables[0].Rows[i]["3xlfs"].ToString());
                            int T4xlhs = Convert.ToInt32(temp.Tables[0].Rows[i]["4xlhs"].ToString());
                            int T4xlfs = Convert.ToInt32(temp.Tables[0].Rows[i]["4xlfs"].ToString());

                            SendQty = Convert.ToInt32(T30fs) + Convert.ToInt32(T30hs) + Convert.ToInt32(T32fs) + Convert.ToInt32(T32hs) + Convert.ToInt32(T34fs) + Convert.ToInt32(T34hs) +
                 Convert.ToInt32(T36fs) + Convert.ToInt32(T36hs) + Convert.ToInt32(Txsfs) + Convert.ToInt32(Txshs) + Convert.ToInt32(Tlfs) + Convert.ToInt32(Tlhs) + Convert.ToInt32(Tsfs) + Convert.ToInt32(Tshs) + Convert.ToInt32(Tmfs) + Convert.ToInt32(Tmhs)
                 + Convert.ToInt32(Txlfs) + Convert.ToInt32(Txlhs) + Convert.ToInt32(Txxlfs) + Convert.ToInt32(Txxlhs) + Convert.ToInt32(T3xlfs) + Convert.ToInt32(T3xlhs)
                 + Convert.ToInt32(T4xlfs) + Convert.ToInt32(T4xlhs);

                        #endregion


                            #region Load Data from Grid

                            TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsendFQty");
                            DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpProcess");
                            TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrate");
                            TextBox date1 = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                            TextBox Recdate = (TextBox)gvcustomerorder.Rows[i].FindControl("Recdate");
                            DateTime recdate = DateTime.ParseExact(Recdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            DateTime date11 = DateTime.ParseExact(date1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");
                            Label lblPatternid = (Label)gvcustomerorder.Rows[i].FindControl("lblPatternid");

                            Label lblIronTransID = (Label)gvcustomerorder.Rows[i].FindControl("lblIronTransID");
                            Label lbldesignno = (Label)gvcustomerorder.Rows[i].FindControl("lbldesignno");
                            Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                            TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                            TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                            TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                            TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                            TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                            TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                            TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                            TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                            TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                            TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");

                            TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                            TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                            TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                            TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                            TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                            TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                            TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                            TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                            TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                            TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                            TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                            TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                            TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                            TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");

                            ReceivedQty = Convert.ToInt32(txts30fs.Text) + Convert.ToInt32(txts30hs.Text) + Convert.ToInt32(txts32fs.Text) + Convert.ToInt32(txts32hs.Text) + Convert.ToInt32(txts34fs.Text) + Convert.ToInt32(txts34hs.Text) +
               Convert.ToInt32(txts36fs.Text) + Convert.ToInt32(txts36hs.Text) + Convert.ToInt32(txtsxsfs.Text) + Convert.ToInt32(txtsxshs.Text)
               + Convert.ToInt32(txtslfs.Text) + Convert.ToInt32(txtslhs.Text) +
               Convert.ToInt32(txtssfs.Text) + Convert.ToInt32(txtsshs.Text) + Convert.ToInt32(txtsmfs.Text) + Convert.ToInt32(txtsmhs.Text)
               + Convert.ToInt32(txtsxlfs.Text) + Convert.ToInt32(txtsxlhs.Text) +
               Convert.ToInt32(txtsxxlfs.Text) + Convert.ToInt32(txtsxxlhs.Text) + Convert.ToInt32(txts3xlfs.Text) + Convert.ToInt32(txts3xlhs.Text)
               + Convert.ToInt32(txts4xlfs.Text) + Convert.ToInt32(txts4xlhs.Text);

                            #endregion

                            #region Calculate the Remaining
                            int C30fs, C30hs, C32fs, C32hs, C34hs, C34fs, C36hs, C36fs, Cxshs, Cxsfs, Cshs, Csfs, Cmhs, Cmfs, Clhs, Clfs, Cxxlhs, Cxxlfs, Cxlhs, Cxlfs, C3xlhs, C3xlfs, C4xlhs, C4xlfs = 0;

                            ////C30fs = T30fs - Convert.ToInt32(txts30fs.Text);
                            ////C30hs = T30hs - Convert.ToInt32(txts30hs.Text);
                            ////C32fs = T32fs - Convert.ToInt32(txts32fs.Text);
                            ////C32hs = T32hs - Convert.ToInt32(txts32hs.Text);
                            ////C34fs = T34fs - Convert.ToInt32(txts34fs.Text);
                            ////C34hs = T34hs - Convert.ToInt32(txts34hs.Text);
                            ////C36fs = T36fs - Convert.ToInt32(txts36fs.Text);
                            ////C36hs = T36hs - Convert.ToInt32(txts36hs.Text);
                            ////Cxsfs = Txsfs - Convert.ToInt32(txtsxsfs.Text);
                            ////Cxshs = Txshs - Convert.ToInt32(txtsxshs.Text);
                            ////Csfs = Tsfs - Convert.ToInt32(txtssfs.Text);
                            ////Cshs = Tshs - Convert.ToInt32(txtsshs.Text);
                            ////Cmfs = Tmfs - Convert.ToInt32(txtsmfs.Text);
                            ////Cmhs = Tmhs - Convert.ToInt32(txtsmhs.Text);
                            ////Clfs = Tlfs - Convert.ToInt32(txtslfs.Text);
                            ////Clhs = Tlhs - Convert.ToInt32(txtslhs.Text);
                            ////Cxlfs = Txlfs - Convert.ToInt32(txtsxlfs.Text);
                            ////Cxlhs = Txlhs - Convert.ToInt32(txtsxlhs.Text);
                            ////Cxxlfs = Txxlfs - Convert.ToInt32(txtsxxlfs.Text);
                            ////Cxxlhs = Txxlhs - Convert.ToInt32(txtsxxlhs.Text);
                            ////C3xlfs = T3xlfs - Convert.ToInt32(txts3xlfs.Text);
                            ////C3xlhs = T3xlhs - Convert.ToInt32(txts3xlhs.Text);
                            ////C4xlfs = T4xlfs - Convert.ToInt32(txts4xlfs.Text);
                            ////  C4xlhs = T4xlhs - Convert.ToInt32(txts4xlhs.Text);

                            C30fs = Convert.ToInt32(txts30fs.Text);
                            C30hs = Convert.ToInt32(txts30hs.Text);
                            C32fs = Convert.ToInt32(txts32fs.Text);
                            C32hs = Convert.ToInt32(txts32hs.Text);
                            C34fs = Convert.ToInt32(txts34fs.Text);
                            C34hs = Convert.ToInt32(txts34hs.Text);
                            C36fs = Convert.ToInt32(txts36fs.Text);
                            C36hs = Convert.ToInt32(txts36hs.Text);
                            Cxsfs = Convert.ToInt32(txtsxsfs.Text);
                            Cxshs = Convert.ToInt32(txtsxshs.Text);
                            Csfs = Convert.ToInt32(txtssfs.Text);
                            Cshs = Convert.ToInt32(txtsshs.Text);
                            Cmfs = Convert.ToInt32(txtsmfs.Text);
                            Cmhs = Convert.ToInt32(txtsmhs.Text);
                            Clfs = Convert.ToInt32(txtslfs.Text);
                            Clhs = Convert.ToInt32(txtslhs.Text);
                            Cxlfs = Convert.ToInt32(txtsxlfs.Text);
                            Cxlhs = Convert.ToInt32(txtsxlhs.Text);
                            Cxxlfs = Convert.ToInt32(txtsxxlfs.Text);
                            Cxxlhs = Convert.ToInt32(txtsxxlhs.Text);
                            C3xlfs = Convert.ToInt32(txts3xlfs.Text);
                            C3xlhs = Convert.ToInt32(txts3xlhs.Text);
                            C4xlfs = Convert.ToInt32(txts4xlfs.Text);
                            C4xlhs = Convert.ToInt32(txts4xlhs.Text);

                            #endregion

                            #region Update the Size-wise

                            DataSet getFinishedDetails = objbs.getFinishedRemainingManualStock(Convert.ToString(TransId));
                            if (getFinishedDetails.Tables[0].Rows.Count > 0)
                            {
                                RemainingQty = SendQty - ReceivedQty;
                                ReceivedQty = ReceivedQty + TRecQty;
                                string Status = "N";
                                if (RemainingQty == 0)
                                {
                                    Status = "Y";
                                }


                                if (txtsendFQty.Text != "0")
                                {
                                    #region History
                                    string Ironingidhis = Request.QueryString.Get("Ironingid");

                                    int istasHistory = objbs.inserttransjpIroningHistory1(0, Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(0), date11,
                                                recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text),
                                                Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Ironingidhis, empid
                                                , txts30fs.Text, txts30hs.Text, txts32fs.Text, txts32hs.Text, txts34fs.Text, txts34hs.Text, txts36fs.Text, txts36hs.Text, txtsxsfs.Text, txtsxshs.Text, txtssfs.Text, txtsshs.Text, txtsmfs.Text, txtsmhs.Text, txtslfs.Text, txtslhs.Text, txtsxlfs.Text, txtsxlhs.Text, txtsxxlfs.Text, txtsxxlhs.Text, txts3xlfs.Text, txts3xlhs.Text, txts4xlfs.Text, txts4xlhs.Text, ddltype.SelectedItem.Text, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text), ddlConsiderStock.SelectedItem.Text);

                                    #endregion
                                }

                                int istas = objbs.updatetransJpIroning_ReceivedSize123(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), ReceivedQty, RemainingQty, Status, C30fs, C30hs, C32fs, C32hs, C34fs, C34hs, C36fs, C36hs, Cxsfs, Cxshs, Csfs, Cshs, Cmfs, Cmhs, Clfs, Clhs, Cxlfs, Cxlhs, Cxxlfs, Cxxlhs, C3xlfs, C3xlhs, C4xlfs, C4xlhs, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));

                                if (ddltype.SelectedValue == "Damage")
                                {
                                    int istasDAMAGE = objbs.updatetransJpIroning_ReceivedSize123damage(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), ReceivedQty, RemainingQty, Status, C30fs, C30hs, C32fs, C32hs, C34fs, C34hs, C36fs, C36hs, Cxsfs, Cxshs, Csfs, Cshs, Cmfs, Cmhs, Clfs, Clhs, Cxlfs, Cxlhs, Cxxlfs, Cxxlhs, C3xlfs, C3xlhs, C4xlfs, C4xlhs, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));
                                }
                                else
                                {
                                    if (ddlConsiderStock.SelectedValue == "Yes")
                                    {
                                        #region Insert in Finished Stock Ratio
                                        DataSet dsManualStock_Details = objbs.getRemainingManualStock123(Convert.ToString(lblfitid.Text), Convert.ToString(ddlLotNo.SelectedItem), lbldesignno.Text);
                                        if (getFinishedDetails.Tables[0].Rows.Count > 0)
                                        {
                                            string DesignCode = getFinishedDetails.Tables[0].Rows[0]["DesignCode"].ToString();
                                            string BrandId = getFinishedDetails.Tables[0].Rows[0]["BrandId"].ToString();
                                            string Fit = getFinishedDetails.Tables[0].Rows[0]["Fit"].ToString();
                                            string Itemname = getFinishedDetails.Tables[0].Rows[0]["Itemname"].ToString();
                                            string Companyid = getFinishedDetails.Tables[0].Rows[0]["Companyid"].ToString();
                                            string CompanyLotNo = getFinishedDetails.Tables[0].Rows[0]["CompanyLotNo"].ToString();


                                            string Totalshirt = Convert.ToString(ReceivedQty);
                                            string R30fs = txts30fs.Text;
                                            string R30hs = txts30hs.Text;
                                            string R32fs = txts32fs.Text;
                                            string R32hs = txts32hs.Text;
                                            string R34fs = txts34fs.Text;
                                            string R34hs = txts34hs.Text;
                                            string R36fs = txts36fs.Text;
                                            string R36hs = txts36hs.Text;
                                            string Rxsfs = txtsxsfs.Text;
                                            string Rxshs = txtsxshs.Text;
                                            string Rsfs = txtssfs.Text;
                                            string Rshs = txtsshs.Text;
                                            string Rmfs = txtsmfs.Text;
                                            string Rmhs = txtsmhs.Text;
                                            string Rlfs = txtslfs.Text;
                                            string Rlhs = txtslhs.Text;
                                            string Rxlfs = txtsxlfs.Text;
                                            string Rxlhs = txtsxlhs.Text;
                                            string Rxxlfs = txtsxxlfs.Text;
                                            string Rxxlhs = txtsxxlhs.Text;
                                            string R3xlfs = txts3xlfs.Text;
                                            string R3xlhs = txts3xlhs.Text;
                                            string R4xlfs = txts4xlfs.Text;
                                            string R4xlhs = txts4xlhs.Text;
                                            int istockid = objbs.UPDATEFinishedstockwisestock(Convert.ToString(TransId), Convert.ToString(Totalshirt), txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y");


                                        }
                                        #endregion
                                    }

                                }
                            }
                            else
                            {
                                RemainingQty = SendQty - ReceivedQty;
                                string Status = "N";
                                if (RemainingQty == 0)
                                {
                                    Status = "Y";
                                }


                                if (txtsendFQty.Text != "0")
                                {
                                    #region History
                                    string Ironingidhis = Request.QueryString.Get("Ironingid");

                                    int istasHistory = objbs.inserttransjpIroningHistory1(0, Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(0), date11,
                                                recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text),
                                                Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Ironingidhis, empid
                                                , txts30fs.Text, txts30hs.Text, txts32fs.Text, txts32hs.Text, txts34fs.Text, txts34hs.Text, txts36fs.Text, txts36hs.Text, txtsxsfs.Text, txtsxshs.Text, txtssfs.Text, txtsshs.Text, txtsmfs.Text, txtsmhs.Text, txtslfs.Text, txtslhs.Text, txtsxlfs.Text, txtsxlhs.Text, txtsxxlfs.Text, txtsxxlhs.Text, txts3xlfs.Text, txts3xlhs.Text, txts4xlfs.Text, txts4xlhs.Text, ddltype.SelectedItem.Text, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text), ddlConsiderStock.SelectedItem.Text);

                                    #endregion
                                }

                                int istas = objbs.updatetransJpIroning_ReceivedSize123(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), ReceivedQty, RemainingQty, Status, C30fs, C30hs, C32fs, C32hs, C34fs, C34hs, C36fs, C36hs, Cxsfs, Cxshs, Csfs, Cshs, Cmfs, Cmhs, Clfs, Clhs, Cxlfs, Cxlhs, Cxxlfs, Cxxlhs, C3xlfs, C3xlhs, C4xlfs, C4xlhs, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));

                                if (ddltype.SelectedValue == "Damage")
                                {
                                    int istasDAMAGE = objbs.updatetransJpIroning_ReceivedSize123damage(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), ReceivedQty, RemainingQty, Status, C30fs, C30hs, C32fs, C32hs, C34fs, C34hs, C36fs, C36hs, Cxsfs, Cxshs, Csfs, Cshs, Cmfs, Cmhs, Clfs, Clhs, Cxlfs, Cxlhs, Cxxlfs, Cxxlhs, C3xlfs, C3xlhs, C4xlfs, C4xlhs, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));
                                }
                                else
                                {
                                    if (ddlConsiderStock.SelectedValue == "Yes")
                                    {
                                        #region Insert in Finished Stock Ratio
                                        int Id = objbs.getMaxId_TransJPIron(lblfitid.Text, ViewState["Ironingid"].ToString());

                                        DataSet dsManualStock_Details = objbs.getRemainingManualStock123(Convert.ToString(lblfitid.Text), Convert.ToString(ddlLotNo.SelectedItem), lbldesignno.Text);
                                        if (dsManualStock_Details.Tables[0].Rows.Count > 0)
                                        {
                                            string DesignCode = dsManualStock_Details.Tables[0].Rows[0]["DesignCode"].ToString();
                                            string BrandId = dsManualStock_Details.Tables[0].Rows[0]["BrandId"].ToString();
                                            string Fit = dsManualStock_Details.Tables[0].Rows[0]["Fit"].ToString();
                                            string Itemname = dsManualStock_Details.Tables[0].Rows[0]["Itemname"].ToString();
                                            string Companyid = dsManualStock_Details.Tables[0].Rows[0]["Companyid"].ToString();
                                            string CompanyLotNo = dsManualStock_Details.Tables[0].Rows[0]["CompanyLotNo"].ToString();
                                            string Totalshirt = Convert.ToString(ReceivedQty);
                                            string R30fs = txts30fs.Text;
                                            string R30hs = txts30hs.Text;
                                            string R32fs = txts32fs.Text;
                                            string R32hs = txts32hs.Text;
                                            string R34fs = txts34fs.Text;
                                            string R34hs = txts34hs.Text;
                                            string R36fs = txts36fs.Text;
                                            string R36hs = txts36hs.Text;
                                            string Rxsfs = txtsxsfs.Text;
                                            string Rxshs = txtsxshs.Text;
                                            string Rsfs = txtssfs.Text;
                                            string Rshs = txtsshs.Text;
                                            string Rmfs = txtsmfs.Text;
                                            string Rmhs = txtsmhs.Text;
                                            string Rlfs = txtslfs.Text;
                                            string Rlhs = txtslhs.Text;
                                            string Rxlfs = txtsxlfs.Text;
                                            string Rxlhs = txtsxlhs.Text;
                                            string Rxxlfs = txtsxxlfs.Text;
                                            string Rxxlhs = txtsxxlhs.Text;
                                            string R3xlfs = txts3xlfs.Text;
                                            string R3xlhs = txts3xlhs.Text;
                                            string R4xlfs = txts4xlfs.Text;
                                            string R4xlhs = txts4xlhs.Text;
                                            int istockid = objbs.insertFinishedstockwisestock(Convert.ToString(TransId), "0", "0", "0", DesignCode, BrandId, Fit, Itemname, drpbranch.SelectedValue, CompanyLotNo, Totalshirt, "0", txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y");


                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion

                        }


                    }


                }

                #endregion


                #region Accessories
                for (int a = 0; a < gvironstock.Rows.Count; a++)
                {
                    string ProcessId = Request.QueryString.Get("Ironingid");

                    #region
                    CheckBox chkiron = (CheckBox)gvironstock.Rows[a].FindControl("chkiron");
                    TextBox txtissueironstock = (TextBox)gvironstock.Rows[a].FindControl("txtissueironstock");

                    Label lblDefinition = (Label)gvironstock.Rows[a].FindControl("lblDefinition");
                    Label txtironstock = (Label)gvironstock.Rows[a].FindControl("txtironstock");
                    Label lblCategoryUserID = (Label)gvironstock.Rows[a].FindControl("lblCategoryUserID");

                    if (chkiron.Checked == true)
                    {
                        DataSet dsironmat = objbs.chkdsironmat(Convert.ToInt32(ProcessId), 5, ddlLotNo.SelectedItem.Text, Convert.ToInt32(lblCategoryUserID.Text), Convert.ToInt32(txtissueironstock.Text), Convert.ToInt32(drpbranch.SelectedValue));
                        if (dsironmat.Tables[0].Rows.Count > 0)
                        {
                            int insert = objbs.insertironmaterial(Convert.ToInt32(ProcessId), 5, ddlLotNo.SelectedItem.Text, Convert.ToInt32(lblCategoryUserID.Text), Convert.ToInt32(txtissueironstock.Text), Convert.ToInt32(drpbranch.SelectedValue), "Update");
                        }
                        else
                        {
                            int insert = objbs.insertironmaterial(Convert.ToInt32(ProcessId), 5, ddlLotNo.SelectedItem.Text, Convert.ToInt32(lblCategoryUserID.Text), Convert.ToInt32(txtissueironstock.Text), Convert.ToInt32(drpbranch.SelectedValue), "Insert");
                        }
                    }
                    else
                    {
                        DataSet dsironmat = objbs.chkdsironmat(Convert.ToInt32(ProcessId), 5, ddlLotNo.SelectedItem.Text, Convert.ToInt32(lblCategoryUserID.Text), Convert.ToInt32(txtissueironstock.Text), Convert.ToInt32(drpbranch.SelectedValue));
                        if (dsironmat.Tables[0].Rows.Count > 0)
                        {

                        }
                        else
                        {
                            int insert = objbs.insertironmaterial(Convert.ToInt32(ProcessId), 5, ddlLotNo.SelectedItem.Text, Convert.ToInt32(lblCategoryUserID.Text), Convert.ToInt32(0), Convert.ToInt32(drpbranch.SelectedValue), "Insert");
                        }
                    }
                    #endregion
                }

                #endregion
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
                        //////ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Quantity.Thank You!!!.')", true);
                        //////txtsendQty.Focus();
                        //////return;
                    }
                    TextBox date = (TextBox)gvcustomerorder.Rows[i].FindControl("date");
                    if (date.Text == "")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                        return;
                    }
                }

            }


            if (btnadd.Text == "Save")
            {

                #region

                DateTime MultiDate = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                TextBox RDate = (TextBox)gvcustomerorder.Rows[0].FindControl("Recdate");
                DateTime ReceiveDate = DateTime.ParseExact(RDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                int istatus3 = objbs.insertJpIroning(Convert.ToDouble(txtAmount.Text), Convert.ToInt32(lbllotdetailid.Text), lbllotno.Text, MultiDate, Convert.ToInt32(drpMultiemployee.SelectedValue), Convert.ToDouble(txttotalqty.Text), empid, drpbranch.SelectedValue, ddlLotNo.SelectedItem.Text, txtnarration.Text, Convert.ToInt32(txttotalqty.Text), ReceiveDate, txtitemnarration.Text);


                int tot = 0, Mytotal = 0;
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    int TmpTot = 0;

                    TextBox txtrecFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrecFQty");
                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    Label lblfit = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblfit");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                    Label lblitemname = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblitemname");
                    Label lbldesignno = (Label)gvcustomerorder.Rows[vLoop].FindControl("lbldesignno");
                    Label lblStockRatioId = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblStockRatioId");

                    TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRemainQty");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrate");
                    Label lblPattern = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblPattern");
                    Label lblPatternid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblPatternid");
                    Label lblfitid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblfitid");
                    TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdamageqty");
                    TextBox date1 = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                    TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Recdate");
                    DateTime recdate = DateTime.ParseExact(Recdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime date11 = DateTime.ParseExact(txtmultidate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                    TextBox txts30fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts30fs");
                    TextBox txts30hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts30hs");
                    TextBox txts32fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts32fs");
                    TextBox txts32hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts32hs");
                    TextBox txts34fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts34fs");
                    TextBox txts34hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts34hs");
                    TextBox txts36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts36fs");
                    TextBox txts36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts36hs");
                    TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxsfs");
                    TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxshs");

                    TextBox txtslfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtslfs");
                    TextBox txtslhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtslhs");
                    TextBox txtssfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtssfs");
                    TextBox txtsshs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsshs");
                    TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsmfs");
                    TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsmhs");
                    TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxlfs");
                    TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxlhs");
                    TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxxlfs");
                    TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxxlhs");
                    TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts3xlfs");
                    TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts3xlhs");
                    TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts4xlfs");
                    TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts4xlhs");



                    tot = Convert.ToInt32(txts30fs.Text) + Convert.ToInt32(txts30hs.Text) + Convert.ToInt32(txts32fs.Text) + Convert.ToInt32(txts32hs.Text) + Convert.ToInt32(txts34fs.Text) + Convert.ToInt32(txts34hs.Text) +
            Convert.ToInt32(txts36fs.Text) + Convert.ToInt32(txts36hs.Text) + Convert.ToInt32(txtsxsfs.Text) + Convert.ToInt32(txtsxshs.Text)
            + Convert.ToInt32(txtslfs.Text) + Convert.ToInt32(txtslhs.Text) +
            Convert.ToInt32(txtssfs.Text) + Convert.ToInt32(txtsshs.Text) + Convert.ToInt32(txtsmfs.Text) + Convert.ToInt32(txtsmhs.Text)
            + Convert.ToInt32(txtsxlfs.Text) + Convert.ToInt32(txtsxlhs.Text) +
            Convert.ToInt32(txtsxxlfs.Text) + Convert.ToInt32(txtsxxlhs.Text) + Convert.ToInt32(txts3xlfs.Text) + Convert.ToInt32(txts3xlhs.Text)
            + Convert.ToInt32(txts4xlfs.Text) + Convert.ToInt32(txts4xlhs.Text);

                    TmpTot = tot;

                    if (drpProcess.SelectedValue == "Select Process Type")
                    {

                    }
                    else
                    {
                        int Tot_remainingQty = 0;
                        DataSet dsIornExist = objbs.CheckTransJPIornStock_ByItemName(lblitemname.Text, Convert.ToInt32(lblfitid.Text));
                        if (dsIornExist.Tables[0].Rows.Count > 0)
                        {
                            Tot_remainingQty = Convert.ToInt32(dsIornExist.Tables[0].Rows[0]["RemainQty"]) - Convert.ToInt32(tot);

                        }
                        else
                        {
                            Tot_remainingQty = Convert.ToInt32(txtsendFQty.Text) - Convert.ToInt32(tot);

                        }

                        if (txtsendFQty.Text != "0")
                        {


                            int istasHistory = objbs.inserttransjpIroningHistory1(Tot_remainingQty, Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(tot), date11,
                                      recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text),
                                      Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), "0", empid
                                      , txts30fs.Text, txts30hs.Text, txts32fs.Text, txts32hs.Text, txts34fs.Text, txts34hs.Text, txts36fs.Text, txts36hs.Text, txtsxsfs.Text, txtsxshs.Text, txtssfs.Text, txtsshs.Text, txtsmfs.Text, txtsmhs.Text, txtslfs.Text, txtslhs.Text, txtsxlfs.Text, txtsxlhs.Text, txtsxxlfs.Text, txtsxxlhs.Text, txts3xlfs.Text, txts3xlhs.Text, txts4xlfs.Text, txts4xlhs.Text, "Issue", lbldesignno.Text, Convert.ToInt32(lblStockRatioId.Text), "");

                            //  int istas = objbs.inserttransJpIroning(Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(tot), date11,
                            //           recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text), Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Convert.ToInt32(ddlLotNo.SelectedValue), empid);

                            int istas = objbs.inserttransJpIroning(Tot_remainingQty, Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(tot), date11,
                                      recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text),
                                      Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Convert.ToInt32(lbllotdetailid.Text), empid
                                      , txts30fs.Text, txts30hs.Text, txts32fs.Text, txts32hs.Text, txts34fs.Text, txts34hs.Text, txts36fs.Text, txts36hs.Text, txtsxsfs.Text, txtsxshs.Text, txtssfs.Text, txtsshs.Text, txtsmfs.Text, txtsmhs.Text, txtslfs.Text, txtslhs.Text, txtsxlfs.Text, txtsxlhs.Text, txtsxxlfs.Text, txtsxxlhs.Text, txts3xlfs.Text, txts3xlhs.Text, txts4xlfs.Text, txts4xlhs.Text, lbldesignno.Text, Convert.ToInt32(lblStockRatioId.Text));
                        }

                        #region Update the Master Stock Ratio Table

                        //Get Current Remaining Stock
                        //////DataSet dsManualStock = objbs.getRemainingManualStock(Convert.ToString(lblfitid.Text), Convert.ToString(ddlLotNo.SelectedItem));
                        DataSet dsManualStock = objbs.getRemainingManualStock123get(Convert.ToString(lblfitid.Text), Convert.ToString(ddlLotNo.SelectedItem), lbldesignno.Text, Convert.ToInt32(lblStockRatioId.Text));
                        //Get Current Remaining Stock

                        //Strings   

                        int StockRatioId = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["StockRatioId"].ToString());

                        string DesignCode = dsManualStock.Tables[0].Rows[0]["DesignCode"].ToString();
                        int Qty = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["Remainshirt"].ToString());
                        int s30fs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R30FS"].ToString());
                        int s30hs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R30HS"].ToString());
                        int s32fs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R32FS"].ToString());
                        int s32hs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R32HS"].ToString());
                        int s34fs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R34FS"].ToString());
                        int s34hs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R34HS"].ToString());
                        int s36fs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R36FS"].ToString());
                        int s36hs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R36HS"].ToString());
                        int sxsfs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RXSFS"].ToString());
                        int sxshs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RXSHS"].ToString());

                        int slfs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RLFS"].ToString());
                        int slhs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RLHS"].ToString());
                        int ssfs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RSFS"].ToString());
                        int sshs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RSHS"].ToString());
                        int smfs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RMFS"].ToString());
                        int smhs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RMHS"].ToString());
                        int sxlfs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RXLFS"].ToString());
                        int sxlhs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RXLHS"].ToString());
                        int sxxlfs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RXXLFS"].ToString());
                        int sxxlhs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["RXXLHS"].ToString());
                        int s3xlfs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R3XLFS"].ToString());
                        int s3xlhs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R3XLHS"].ToString());
                        int s4xlfs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R4XLFS"].ToString());
                        int s4xlhs = Convert.ToInt32(dsManualStock.Tables[0].Rows[0]["R4XLHS"].ToString());
                        //string

                        //Calculate
                        int TRemainshirt, Ts30fs, Ts30hs, Ts32fs, Ts32hs, Ts34fs, Ts34hs, Ts36fs, Ts36hs, Tsxsfs, Tsxshs, Tssfs, Tsshs, Tsmfs, Tsmhs, Tslfs, Tslhs, Tsxlfs, Tsxlhs, Tsxxlfs, Tsxxlhs, Ts3xlfs, Ts3xlhs, Ts4xlfs, Ts4xlhs = 0;

                        Ts30fs = s30fs - Convert.ToInt32(txts30fs.Text);
                        Ts30hs = s30hs - Convert.ToInt32(txts30hs.Text);
                        Ts32fs = s32fs - Convert.ToInt32(txts32fs.Text);
                        Ts32hs = s32hs - Convert.ToInt32(txts32hs.Text);
                        Ts34fs = s34fs - Convert.ToInt32(txts34fs.Text);
                        Ts34hs = s34hs - Convert.ToInt32(txts34hs.Text);
                        Ts36fs = s36fs - Convert.ToInt32(txts36fs.Text);
                        Ts36hs = s36hs - Convert.ToInt32(txts36hs.Text);
                        Tsxsfs = sxsfs - Convert.ToInt32(txtsxsfs.Text);
                        Tsxshs = sxshs - Convert.ToInt32(txtsxshs.Text);
                        Tssfs = ssfs - Convert.ToInt32(txtssfs.Text);
                        Tsshs = sshs - Convert.ToInt32(txtsshs.Text);
                        Tsmfs = smfs - Convert.ToInt32(txtsmfs.Text);
                        Tsmhs = smhs - Convert.ToInt32(txtsmhs.Text);
                        Tslfs = slfs - Convert.ToInt32(txtslfs.Text);
                        Tslhs = slhs - Convert.ToInt32(txtslhs.Text);
                        Tsxlfs = sxlfs - Convert.ToInt32(txtsxlfs.Text);
                        Tsxlhs = sxlhs - Convert.ToInt32(txtsxlhs.Text);
                        Tsxxlfs = sxxlfs - Convert.ToInt32(txtsxxlfs.Text);
                        Tsxxlhs = sxxlhs - Convert.ToInt32(txtsxxlhs.Text);
                        Ts3xlfs = s3xlfs - Convert.ToInt32(txts3xlfs.Text);
                        Ts3xlhs = s3xlhs - Convert.ToInt32(txts3xlhs.Text);
                        Ts4xlfs = s4xlfs - Convert.ToInt32(txts4xlfs.Text);
                        Ts4xlhs = s4xlhs - Convert.ToInt32(txts4xlhs.Text);
                        TRemainshirt = Qty - Convert.ToInt32(Ts30fs + Ts30hs + Ts32fs + Ts32hs + Ts34fs + Ts34hs + Ts36fs + Ts36hs + Tsxsfs + Tsxshs + Tssfs + Tsshs + Tsmfs + Tsmhs + Tslfs + Tslhs + Tsxlfs + Tsxlhs + Tsxxlfs + Tsxxlhs + Ts3xlfs + Ts3xlhs + Ts4xlfs + Ts4xlhs);
                        //Calculate

                        //Update 

                        //////     int iMasterStock = objbs.updatestockwisestock(TRemainshirt, Ts30fs, Ts30hs, Ts32fs, Ts32hs, Ts34fs, Ts34hs, Ts36fs, Ts36hs, Tsxsfs, Tsxshs, Tssfs, Tsshs, Tsmfs, Tsmhs, Tslfs, Tslhs, Tsxlfs, Tsxlhs, Tsxxlfs, Tsxxlhs, Ts3xlfs, Ts3xlhs, Ts4xlfs, Ts4xlhs, Convert.ToString(lblfitid.Text), Convert.ToString(ddlLotNo.SelectedItem), DesignCode);
                        int iMasterStock = objbs.updatestockwisestock(TRemainshirt, Ts30fs, Ts30hs, Ts32fs, Ts32hs, Ts34fs, Ts34hs, Ts36fs, Ts36hs, Tsxsfs, Tsxshs, Tssfs, Tsshs, Tsmfs, Tsmhs, Tslfs, Tslhs, Tsxlfs, Tsxlhs, Tsxxlfs, Tsxxlhs, Ts3xlfs, Ts3xlhs, Ts4xlfs, Ts4xlhs, StockRatioId);

                        //Update

                        #endregion
                        // int istas1 = objbs.JpIroning(Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(tot), date11,
                        //         recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text), Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Convert.ToInt32(ddlLotNo.SelectedValue), empid);
                        Mytotal = TmpTot + Mytotal;
                    }
                }
                int istas1 = objbs.updateSendQty_JpIroning(Mytotal);

                if (lbllotdetailid.Text != "0")
                {
                    int istatus3l = objbs.statusJpIroning(Convert.ToInt32(lbllotdetailid.Text));
                }

                #endregion

            }

            else if (btnadd.Text == "Received")
            {
                if (ddltype.SelectedValue == "Type")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Type.Thank You!!!.')", true);
                    ddltype.Focus();
                    return;
                }

                #region

                string Ironingid = Request.QueryString.Get("Ironingid");

                int istasHistoryid = objbs.processmaintableironing(Convert.ToInt32(ViewState["Ironingid"].ToString()), Convert.ToInt32(txttotalqty.Text), Convert.ToDouble(txtAmount.Text), ddltype.SelectedValue);

                ////// int istasHistoryid = objbs.processmaintableironing(Convert.ToInt32(ViewState["Ironingid"].ToString()), Convert.ToInt32(txttotalqty.Text), Convert.ToDouble(txtAmount.Text));
                ////if (ddltype.SelectedValue == "Damage")
                ////{
                ////    int istasHistoryid = objbs.processmaintableironing(Convert.ToInt32(ViewState["Ironingid"].ToString()), Convert.ToInt32(txttotalqty.Text), Convert.ToDouble(txtAmount.Text), ddltype.SelectedValue);
                ////}
                ////else
                ////{
                ////    int istasHistoryid = objbs.processmaintableironing(Convert.ToInt32(ViewState["Ironingid"].ToString()), Convert.ToInt32(txttotalqty.Text), Convert.ToDouble(txtAmount.Text));
                ////}

                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
                    Label lblfit = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblfit");
                    TextBox txtsendFQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsendFQty");
                    Label lblitemname = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblitemname");
                    TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRemainQty");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrate");
                    Label lblPattern = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblPattern");
                    Label lblPatternid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblPatternid");
                    Label lblfitid = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblfitid");
                    TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdamageqty");
                    TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrecFQty");
                    TextBox date1 = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                    TextBox Recdate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("Recdate");
                    DateTime recdate = DateTime.ParseExact(Recdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime date11 = DateTime.ParseExact(date1.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                    TextBox txts30fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts30fs");
                    TextBox txts30hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts30hs");
                    TextBox txts32fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts32fs");
                    TextBox txts32hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts32hs");
                    TextBox txts34fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts34fs");
                    TextBox txts34hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts34hs");
                    TextBox txts36fs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts36fs");
                    TextBox txts36hs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts36hs");
                    TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxsfs");
                    TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxshs");

                    TextBox txtslfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtslfs");
                    TextBox txtslhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtslhs");
                    TextBox txtssfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtssfs");
                    TextBox txtsshs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsshs");
                    TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsmfs");
                    TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsmhs");
                    TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxlfs");
                    TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxlhs");
                    TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxxlfs");
                    TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtsxxlhs");
                    TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts3xlfs");
                    TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts3xlhs");
                    TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts4xlfs");
                    TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txts4xlhs");
                    Label lbldesignno = (Label)gvcustomerorder.Rows[vLoop].FindControl("lbldesignno");
                    Label lblIronTransID = (Label)gvcustomerorder.Rows[vLoop].FindControl("lblIronTransID");



                    if (drpProcess.SelectedValue == "Select Process Type")
                    {

                    }
                    else
                    {
                        if (txtsendFQty.Text != "0")
                        {
                            string Ironingidhis = Request.QueryString.Get("Ironingid");
                            int istasHistory = objbs.inserttransjpIroningHistory1(0, Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(0), recdate,
                                        recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text),
                                        Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Ironingidhis, empid
                                        , txts30fs.Text, txts30hs.Text, txts32fs.Text, txts32hs.Text, txts34fs.Text, txts34hs.Text, txts36fs.Text, txts36hs.Text, txtsxsfs.Text, txtsxshs.Text, txtssfs.Text, txtsshs.Text, txtsmfs.Text, txtsmhs.Text, txtslfs.Text, txtslhs.Text, txtsxlfs.Text, txtsxlhs.Text, txtsxxlfs.Text, txtsxxlhs.Text, txts3xlfs.Text, txts3xlhs.Text, txts4xlfs.Text, txts4xlhs.Text, ddltype.SelectedItem.Text, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text), ddlConsiderStock.SelectedItem.Text);


                        }

                        int istas = objbs.JpIroning(Convert.ToInt32(drpProcess.SelectedValue), Convert.ToInt32(txtrecQty.Text), date11,
                                  recdate, Convert.ToDouble(txtrate.Text), Convert.ToInt32(txtdamageqty.Text), Convert.ToInt32(txtsendFQty.Text), Convert.ToString(lblfitid.Text), Convert.ToString(lblitemname.Text), Convert.ToString(lblPatternid.Text), Convert.ToInt32(ddlLotNo.SelectedValue), empid);
                    }
                }

                int istatus3l = objbs.statusJpIroning(Convert.ToInt32(ddlLotNo.SelectedValue));

                #endregion

                if (ddltype.SelectedValue == "Alter")
                {
                    #region

                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {
                        #region Load Data from Grid

                        int alterQty = 0;

                        Label lblStockRatioId = (Label)gvcustomerorder.Rows[i].FindControl("lblStockRatioId");
                        Label lblIronTransID = (Label)gvcustomerorder.Rows[i].FindControl("lblIronTransID");
                        Label lbldesignno = (Label)gvcustomerorder.Rows[i].FindControl("lbldesignno");
                        Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                        TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                        TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                        TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                        TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                        TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                        TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                        TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                        TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                        TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                        TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");

                        TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                        TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                        TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                        TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                        TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                        TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                        TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                        TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                        TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                        TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                        TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                        TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                        TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                        TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");

                        alterQty = Convert.ToInt32(txts30fs.Text) + Convert.ToInt32(txts30hs.Text) + Convert.ToInt32(txts32fs.Text) + Convert.ToInt32(txts32hs.Text) + Convert.ToInt32(txts34fs.Text) + Convert.ToInt32(txts34hs.Text) +
           Convert.ToInt32(txts36fs.Text) + Convert.ToInt32(txts36hs.Text) + Convert.ToInt32(txtsxsfs.Text) + Convert.ToInt32(txtsxshs.Text)
           + Convert.ToInt32(txtslfs.Text) + Convert.ToInt32(txtslhs.Text) +
           Convert.ToInt32(txtssfs.Text) + Convert.ToInt32(txtsshs.Text) + Convert.ToInt32(txtsmfs.Text) + Convert.ToInt32(txtsmhs.Text)
           + Convert.ToInt32(txtsxlfs.Text) + Convert.ToInt32(txtsxlhs.Text) +
           Convert.ToInt32(txtsxxlfs.Text) + Convert.ToInt32(txtsxxlhs.Text) + Convert.ToInt32(txts3xlfs.Text) + Convert.ToInt32(txts3xlhs.Text)
           + Convert.ToInt32(txts4xlfs.Text) + Convert.ToInt32(txts4xlhs.Text);

                        #endregion

                        int istas = objbs.updatetransJpIroning_ReceivedSize123(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), 0, 0, "N", Convert.ToInt32(txts30fs.Text), Convert.ToInt32(txts30hs.Text), Convert.ToInt32(txts32fs.Text), Convert.ToInt32(txts32hs.Text), Convert.ToInt32(txts34fs.Text), Convert.ToInt32(txts34hs.Text), Convert.ToInt32(txts36fs.Text), Convert.ToInt32(txts36hs.Text), Convert.ToInt32(txtsxsfs.Text), Convert.ToInt32(txtsxshs.Text), Convert.ToInt32(txtssfs.Text), Convert.ToInt32(txtsshs.Text), Convert.ToInt32(txtsmfs.Text), Convert.ToInt32(txtsmhs.Text), Convert.ToInt32(txtslfs.Text), Convert.ToInt32(txtslhs.Text), Convert.ToInt32(txtsxlfs.Text), Convert.ToInt32(txtsxlhs.Text), Convert.ToInt32(txtsxxlfs.Text), Convert.ToInt32(txtsxxlhs.Text), Convert.ToInt32(txts3xlfs.Text), Convert.ToInt32(txts3xlhs.Text), Convert.ToInt32(txts4xlfs.Text), Convert.ToInt32(txts4xlhs.Text), lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));
                        int iMasterStock = objbs.updatestockwisestockalter(Convert.ToInt32(alterQty), Convert.ToInt32(txts30fs.Text), Convert.ToInt32(txts30hs.Text), Convert.ToInt32(txts32fs.Text), Convert.ToInt32(txts32hs.Text), Convert.ToInt32(txts34fs.Text), Convert.ToInt32(txts34hs.Text), Convert.ToInt32(txts36fs.Text), Convert.ToInt32(txts36hs.Text), Convert.ToInt32(txtsxsfs.Text), Convert.ToInt32(txtsxshs.Text), Convert.ToInt32(txtssfs.Text), Convert.ToInt32(txtsshs.Text), Convert.ToInt32(txtsmfs.Text), Convert.ToInt32(txtsmhs.Text), Convert.ToInt32(txtslfs.Text), Convert.ToInt32(txtslhs.Text), Convert.ToInt32(txtsxlfs.Text), Convert.ToInt32(txtsxlhs.Text), Convert.ToInt32(txtsxxlfs.Text), Convert.ToInt32(txtsxxlhs.Text), Convert.ToInt32(txts3xlfs.Text), Convert.ToInt32(txts3xlhs.Text), Convert.ToInt32(txts4xlfs.Text), Convert.ToInt32(txts4xlhs.Text), Convert.ToInt32(lblStockRatioId.Text));
                    }
                    #endregion
                }
                else
                {
                    #region
                    //  if (btnadd.Text == "Receive")
                    {
                        int iq = 1;
                        int iii = 1;
                        string itemc = string.Empty;
                        string itemd = string.Empty;
                        string iteme = string.Empty;
                        string itemcd = string.Empty;
                        #region Table
                        DataSet temp = new DataSet();
                        DataTable dtt = new DataTable();
                        dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("RecQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("senddate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("recdate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Fitid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Fit", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Pattern", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Patternid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("itemname", typeof(string)));
                        dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("RemainQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ProcessTypeID", typeof(string)));
                        dtt.Columns.Add(new DataColumn("rate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("damageqty", typeof(string)));
                        //   temp.Tables.Add(dtt);
                        dtt.Columns.Add(new DataColumn("30hs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("32hs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("34hs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("36hs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("30fs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("32fs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("34fs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("36fs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("shs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("mhs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("lhs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("lfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("mfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("sfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("xlfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("xxlfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("3xlfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("4xlfs", typeof(string)));

                        dtt.Columns.Add(new DataColumn("xsfs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("xshs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("xlhs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("xxlhs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("3xlhs", typeof(string)));
                        dtt.Columns.Add(new DataColumn("4xlhs", typeof(string)));

                        temp.Tables.Add(dtt);
                        #endregion
                        ds = objbs.Get_TransIorningDetails(Convert.ToInt32(ViewState["Ironingid"].ToString()));

                        {
                            string curent = string.Empty;
                            string namee = string.Empty;

                            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                            {

                                int ReceivedQty = 0, SendQty = 0, RemainingQty = 0;
                                TextBox txtRemainQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRemainQty");
                                TextBox txtdamageqty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdamageqty");
                                TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrecFQty");


                                double qty = 0;
                                double qty1 = 0;
                                qty1 = Convert.ToDouble(txtRemainQty.Text);
                                qty = Convert.ToDouble(txtdamageqty.Text) + Convert.ToDouble(txtrecQty.Text);

                                if (qty1 < qty)
                                {
                                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Received Qty is not Greater than That Remain Qty.Thank You!!!.')", true);
                                    //txtrecQty.Focus();
                                    //return;
                                }


                                //if (Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"].ToString()) != 0)
                                {
                                    #region Load Previous Data from Table
                                    DataRow dr = dtt.NewRow();

                                    dr["OrderNo"] = "0";
                                    dr["Transid"] = ds.Tables[0].Rows[i]["Transid"].ToString();
                                    dr["fitid"] = ds.Tables[0].Rows[i]["fitid"].ToString();
                                    dr["fit"] = ds.Tables[0].Rows[i]["Fitname"].ToString();
                                    dr["ProcessTypeID"] = "6";//ds.Tables[0].Rows[i]["ProcessID"].ToString();
                                    dr["Rate"] = ds.Tables[0].Rows[i]["Rate"].ToString();
                                    dr["RecQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RecQty"]);
                                    dr["TotalQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["totalQty"]);
                                    dr["RemainQty"] = Convert.ToInt32(ds.Tables[0].Rows[i]["RemainQty"]);
                                    dr["senddate"] = ds.Tables[0].Rows[i]["senddate"].ToString();
                                    dr["RecQty"] = "0";
                                    dr["Recdate"] = "0";
                                    dr["Damageqty"] = "0";
                                    dr["patternid"] = ds.Tables[0].Rows[i]["patternid"].ToString();
                                    dr["pattern"] = ds.Tables[0].Rows[i]["patternname"].ToString();
                                    dr["itemname"] = ds.Tables[0].Rows[i]["itemname"].ToString();

                                    dr["30fs"] = ds.Tables[0].Rows[i]["Rec30fs"].ToString();
                                    dr["30hs"] = ds.Tables[0].Rows[i]["Rec30hs"].ToString();
                                    dr["32fs"] = ds.Tables[0].Rows[i]["Rec32fs"].ToString();
                                    dr["32hs"] = ds.Tables[0].Rows[i]["Rec32hs"].ToString();
                                    dr["34hs"] = ds.Tables[0].Rows[i]["Rec34hs"].ToString();
                                    dr["34fs"] = ds.Tables[0].Rows[i]["Rec34fs"].ToString();
                                    dr["36hs"] = ds.Tables[0].Rows[i]["Rec36hs"].ToString();
                                    dr["36fs"] = ds.Tables[0].Rows[i]["Rec36fs"].ToString();

                                    dr["xshs"] = ds.Tables[0].Rows[i]["Recxshs"].ToString();
                                    dr["xsfs"] = ds.Tables[0].Rows[i]["Recxsfs"].ToString();
                                    dr["shs"] = ds.Tables[0].Rows[i]["Recshs"].ToString();
                                    dr["sfs"] = ds.Tables[0].Rows[i]["Recsfs"].ToString();
                                    dr["mhs"] = ds.Tables[0].Rows[i]["Recmhs"].ToString();
                                    dr["mfs"] = ds.Tables[0].Rows[i]["Recmfs"].ToString();
                                    dr["lhs"] = ds.Tables[0].Rows[i]["Reclhs"].ToString();
                                    dr["lfs"] = ds.Tables[0].Rows[i]["Reclfs"].ToString();
                                    dr["xxlhs"] = ds.Tables[0].Rows[i]["Recxxlhs"].ToString();
                                    dr["xxlfs"] = ds.Tables[0].Rows[i]["Recxxlfs"].ToString();

                                    dr["xlhs"] = ds.Tables[0].Rows[i]["Recxlhs"].ToString();
                                    dr["xlfs"] = ds.Tables[0].Rows[i]["Recxlfs"].ToString();
                                    dr["3xlhs"] = ds.Tables[0].Rows[i]["Rec3xlhs"].ToString();
                                    dr["3xlfs"] = ds.Tables[0].Rows[i]["Rec3xlfs"].ToString();
                                    dr["4xlhs"] = ds.Tables[0].Rows[i]["Rec4xlhs"].ToString();
                                    dr["4xlfs"] = ds.Tables[0].Rows[i]["Rec4xlfs"].ToString();
                                    temp.Tables[0].Rows.Add(dr);

                                    int TransId = Convert.ToInt32(temp.Tables[0].Rows[i]["Transid"].ToString());
                                    int TRecQty = Convert.ToInt32(temp.Tables[0].Rows[i]["RecQty"].ToString());
                                    int T30fs = Convert.ToInt32(temp.Tables[0].Rows[i]["30fs"].ToString());
                                    int T30hs = Convert.ToInt32(temp.Tables[0].Rows[i]["30hs"].ToString());
                                    int T32fs = Convert.ToInt32(temp.Tables[0].Rows[i]["32fs"].ToString());
                                    int T32hs = Convert.ToInt32(temp.Tables[0].Rows[i]["32hs"].ToString());
                                    int T34hs = Convert.ToInt32(temp.Tables[0].Rows[i]["34hs"].ToString());
                                    int T34fs = Convert.ToInt32(temp.Tables[0].Rows[i]["34fs"].ToString());
                                    int T36hs = Convert.ToInt32(temp.Tables[0].Rows[i]["36hs"].ToString());
                                    int T36fs = Convert.ToInt32(temp.Tables[0].Rows[i]["36fs"].ToString());

                                    int Txshs = Convert.ToInt32(temp.Tables[0].Rows[i]["xshs"].ToString());
                                    int Txsfs = Convert.ToInt32(temp.Tables[0].Rows[i]["xsfs"].ToString());
                                    int Tshs = Convert.ToInt32(temp.Tables[0].Rows[i]["shs"].ToString());
                                    int Tsfs = Convert.ToInt32(temp.Tables[0].Rows[i]["sfs"].ToString());
                                    int Tmhs = Convert.ToInt32(temp.Tables[0].Rows[i]["mhs"].ToString());
                                    int Tmfs = Convert.ToInt32(temp.Tables[0].Rows[i]["mfs"].ToString());
                                    int Tlhs = Convert.ToInt32(temp.Tables[0].Rows[i]["lhs"].ToString());
                                    int Tlfs = Convert.ToInt32(temp.Tables[0].Rows[i]["lfs"].ToString());
                                    int Txxlhs = Convert.ToInt32(temp.Tables[0].Rows[i]["xxlhs"].ToString());
                                    int Txxlfs = Convert.ToInt32(temp.Tables[0].Rows[i]["xxlfs"].ToString());

                                    int Txlhs = Convert.ToInt32(temp.Tables[0].Rows[i]["xlhs"].ToString());
                                    int Txlfs = Convert.ToInt32(temp.Tables[0].Rows[i]["xlfs"].ToString());
                                    int T3xlhs = Convert.ToInt32(temp.Tables[0].Rows[i]["3xlhs"].ToString());
                                    int T3xlfs = Convert.ToInt32(temp.Tables[0].Rows[i]["3xlfs"].ToString());
                                    int T4xlhs = Convert.ToInt32(temp.Tables[0].Rows[i]["4xlhs"].ToString());
                                    int T4xlfs = Convert.ToInt32(temp.Tables[0].Rows[i]["4xlfs"].ToString());

                                    SendQty = Convert.ToInt32(T30fs) + Convert.ToInt32(T30hs) + Convert.ToInt32(T32fs) + Convert.ToInt32(T32hs) + Convert.ToInt32(T34fs) + Convert.ToInt32(T34hs) +
                         Convert.ToInt32(T36fs) + Convert.ToInt32(T36hs) + Convert.ToInt32(Txsfs) + Convert.ToInt32(Txshs) + Convert.ToInt32(Tlfs) + Convert.ToInt32(Tlhs) + Convert.ToInt32(Tsfs) + Convert.ToInt32(Tshs) + Convert.ToInt32(Tmfs) + Convert.ToInt32(Tmhs)
                         + Convert.ToInt32(Txlfs) + Convert.ToInt32(Txlhs) + Convert.ToInt32(Txxlfs) + Convert.ToInt32(Txxlhs) + Convert.ToInt32(T3xlfs) + Convert.ToInt32(T3xlhs)
                         + Convert.ToInt32(T4xlfs) + Convert.ToInt32(T4xlhs);

                                    #endregion

                                    #region Load Data from Grid

                                    Label lblIronTransID = (Label)gvcustomerorder.Rows[i].FindControl("lblIronTransID");
                                    Label lbldesignno = (Label)gvcustomerorder.Rows[i].FindControl("lbldesignno");
                                    Label lblfitid = (Label)gvcustomerorder.Rows[i].FindControl("lblfitid");
                                    TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                                    TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                                    TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                                    TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                                    TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                                    TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                                    TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                                    TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                                    TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                                    TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");

                                    TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                                    TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                                    TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                                    TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                                    TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                                    TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                                    TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                                    TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                                    TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                                    TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                                    TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                                    TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                                    TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                                    TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");

                                    ReceivedQty = Convert.ToInt32(txts30fs.Text) + Convert.ToInt32(txts30hs.Text) + Convert.ToInt32(txts32fs.Text) + Convert.ToInt32(txts32hs.Text) + Convert.ToInt32(txts34fs.Text) + Convert.ToInt32(txts34hs.Text) +
                       Convert.ToInt32(txts36fs.Text) + Convert.ToInt32(txts36hs.Text) + Convert.ToInt32(txtsxsfs.Text) + Convert.ToInt32(txtsxshs.Text)
                       + Convert.ToInt32(txtslfs.Text) + Convert.ToInt32(txtslhs.Text) +
                       Convert.ToInt32(txtssfs.Text) + Convert.ToInt32(txtsshs.Text) + Convert.ToInt32(txtsmfs.Text) + Convert.ToInt32(txtsmhs.Text)
                       + Convert.ToInt32(txtsxlfs.Text) + Convert.ToInt32(txtsxlhs.Text) +
                       Convert.ToInt32(txtsxxlfs.Text) + Convert.ToInt32(txtsxxlhs.Text) + Convert.ToInt32(txts3xlfs.Text) + Convert.ToInt32(txts3xlhs.Text)
                       + Convert.ToInt32(txts4xlfs.Text) + Convert.ToInt32(txts4xlhs.Text);

                                    #endregion

                                    #region Calculate the Remaining
                                    int C30fs, C30hs, C32fs, C32hs, C34hs, C34fs, C36hs, C36fs, Cxshs, Cxsfs, Cshs, Csfs, Cmhs, Cmfs, Clhs, Clfs, Cxxlhs, Cxxlfs, Cxlhs, Cxlfs, C3xlhs, C3xlfs, C4xlhs, C4xlfs = 0;

                                    //////C30fs = T30fs - Convert.ToInt32(txts30fs.Text);
                                    //////C30hs = T30hs - Convert.ToInt32(txts30hs.Text);
                                    //////C32fs = T32fs - Convert.ToInt32(txts32fs.Text);
                                    //////C32hs = T32hs - Convert.ToInt32(txts32hs.Text);
                                    //////C34fs = T34fs - Convert.ToInt32(txts34fs.Text);
                                    //////C34hs = T34hs - Convert.ToInt32(txts34hs.Text);
                                    //////C36fs = T36fs - Convert.ToInt32(txts36fs.Text);
                                    //////C36hs = T36hs - Convert.ToInt32(txts36hs.Text);
                                    //////Cxsfs = Txsfs - Convert.ToInt32(txtsxsfs.Text);
                                    //////Cxshs = Txshs - Convert.ToInt32(txtsxshs.Text);
                                    //////Csfs = Tsfs - Convert.ToInt32(txtssfs.Text);
                                    //////Cshs = Tshs - Convert.ToInt32(txtsshs.Text);
                                    //////Cmfs = Tmfs - Convert.ToInt32(txtsmfs.Text);
                                    //////Cmhs = Tmhs - Convert.ToInt32(txtsmhs.Text);
                                    //////Clfs = Tlfs - Convert.ToInt32(txtslfs.Text);
                                    //////Clhs = Tlhs - Convert.ToInt32(txtslhs.Text);
                                    //////Cxlfs = Txlfs - Convert.ToInt32(txtsxlfs.Text);
                                    //////Cxlhs = Txlhs - Convert.ToInt32(txtsxlhs.Text);
                                    //////Cxxlfs = Txxlfs - Convert.ToInt32(txtsxxlfs.Text);
                                    //////Cxxlhs = Txxlhs - Convert.ToInt32(txtsxxlhs.Text);
                                    //////C3xlfs = T3xlfs - Convert.ToInt32(txts3xlfs.Text);
                                    //////C3xlhs = T3xlhs - Convert.ToInt32(txts3xlhs.Text);
                                    //////C4xlfs = T4xlfs - Convert.ToInt32(txts4xlfs.Text);
                                    //////C4xlhs = T4xlhs - Convert.ToInt32(txts4xlhs.Text);

                                    C30fs = Convert.ToInt32(txts30fs.Text);
                                    C30hs = Convert.ToInt32(txts30hs.Text);
                                    C32fs = Convert.ToInt32(txts32fs.Text);
                                    C32hs = Convert.ToInt32(txts32hs.Text);
                                    C34fs = Convert.ToInt32(txts34fs.Text);
                                    C34hs = Convert.ToInt32(txts34hs.Text);
                                    C36fs = Convert.ToInt32(txts36fs.Text);
                                    C36hs = Convert.ToInt32(txts36hs.Text);
                                    Cxsfs = Convert.ToInt32(txtsxsfs.Text);
                                    Cxshs = Convert.ToInt32(txtsxshs.Text);
                                    Csfs = Convert.ToInt32(txtssfs.Text);
                                    Cshs = Convert.ToInt32(txtsshs.Text);
                                    Cmfs = Convert.ToInt32(txtsmfs.Text);
                                    Cmhs = Convert.ToInt32(txtsmhs.Text);
                                    Clfs = Convert.ToInt32(txtslfs.Text);
                                    Clhs = Convert.ToInt32(txtslhs.Text);
                                    Cxlfs = Convert.ToInt32(txtsxlfs.Text);
                                    Cxlhs = Convert.ToInt32(txtsxlhs.Text);
                                    Cxxlfs = Convert.ToInt32(txtsxxlfs.Text);
                                    Cxxlhs = Convert.ToInt32(txtsxxlhs.Text);
                                    C3xlfs = Convert.ToInt32(txts3xlfs.Text);
                                    C3xlhs = Convert.ToInt32(txts3xlhs.Text);
                                    C4xlfs = Convert.ToInt32(txts4xlfs.Text);
                                    C4xlhs = Convert.ToInt32(txts4xlhs.Text);


                                    #endregion

                                    #region Update the Size-wise



                                    DataSet getFinishedDetailsup = objbs.getFinishedNewup(Convert.ToString(TransId)); // For Avoid add stock Temporary Only 
                                    if (getFinishedDetailsup.Tables[0].Rows.Count > 0)
                                    {
                                        #region
                                        DataSet getFinishedDetails = objbs.getFinishedRemainingManualStock(Convert.ToString(TransId));
                                        if (getFinishedDetails.Tables[0].Rows.Count > 0)
                                        {
                                            RemainingQty = SendQty - ReceivedQty;
                                            ReceivedQty = ReceivedQty + TRecQty;
                                            string Status = "N";
                                            if (RemainingQty == 0)
                                            {
                                                Status = "Y";
                                            }

                                            int istas = objbs.updatetransJpIroning_ReceivedSize123(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), ReceivedQty, RemainingQty, Status, C30fs, C30hs, C32fs, C32hs, C34fs, C34hs, C36fs, C36hs, Cxsfs, Cxshs, Csfs, Cshs, Cmfs, Cmhs, Clfs, Clhs, Cxlfs, Cxlhs, Cxxlfs, Cxxlhs, C3xlfs, C3xlhs, C4xlfs, C4xlhs, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));

                                            if (ddltype.SelectedValue == "Damage")
                                            {
                                                int istasDAMAGE = objbs.updatetransJpIroning_ReceivedSize123damage(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), ReceivedQty, RemainingQty, Status, C30fs, C30hs, C32fs, C32hs, C34fs, C34hs, C36fs, C36hs, Cxsfs, Cxshs, Csfs, Cshs, Cmfs, Cmhs, Clfs, Clhs, Cxlfs, Cxlhs, Cxxlfs, Cxxlhs, C3xlfs, C3xlhs, C4xlfs, C4xlhs, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));
                                            }
                                            else
                                            {
                                                if (ddlConsiderStock.SelectedValue == "Yes")
                                                {
                                                    #region Insert in Finished Stock Ratio
                                                    DataSet dsGetFinishdetails = objbs.getFinishedRemainingManualStock(Convert.ToString(TransId));
                                                    //  DataSet dsGetFinishdetails = objbs.GetFinishdetails(Convert.ToString(lblfitid.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), lbldesignno.Text);
                                                    if (dsGetFinishdetails.Tables[0].Rows.Count > 0)
                                                    {
                                                        #region
                                                        string Totalshirt = Convert.ToString(ReceivedQty);
                                                        string R30fs = txts30fs.Text;
                                                        string R30hs = txts30hs.Text;
                                                        string R32fs = txts32fs.Text;
                                                        string R32hs = txts32hs.Text;
                                                        string R34fs = txts34fs.Text;
                                                        string R34hs = txts34hs.Text;
                                                        string R36fs = txts36fs.Text;
                                                        string R36hs = txts36hs.Text;
                                                        string Rxsfs = txtsxsfs.Text;
                                                        string Rxshs = txtsxshs.Text;
                                                        string Rsfs = txtssfs.Text;
                                                        string Rshs = txtsshs.Text;
                                                        string Rmfs = txtsmfs.Text;
                                                        string Rmhs = txtsmhs.Text;
                                                        string Rlfs = txtslfs.Text;
                                                        string Rlhs = txtslhs.Text;
                                                        string Rxlfs = txtsxlfs.Text;
                                                        string Rxlhs = txtsxlhs.Text;
                                                        string Rxxlfs = txtsxxlfs.Text;
                                                        string Rxxlhs = txtsxxlhs.Text;
                                                        string R3xlfs = txts3xlfs.Text;
                                                        string R3xlhs = txts3xlhs.Text;
                                                        string R4xlfs = txts4xlfs.Text;
                                                        string R4xlhs = txts4xlhs.Text;

                                                        int istockid = objbs.UPDATEFinishedstockwisestock(Convert.ToString(TransId), Convert.ToString(Totalshirt), txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y");

                                                        #endregion
                                                    }

                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                    else
                                    {
                                        #region
                                        DataSet getFinishedDetails = objbs.getFinishedRemainingManualStock(Convert.ToString(TransId));
                                        if (getFinishedDetails.Tables[0].Rows.Count > 0)
                                        {
                                            RemainingQty = SendQty - ReceivedQty;
                                            ReceivedQty = ReceivedQty + TRecQty;
                                            string Status = "N";
                                            if (RemainingQty == 0)
                                            {
                                                Status = "Y";
                                            }

                                            int istas = objbs.updatetransJpIroning_ReceivedSize123(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), ReceivedQty, RemainingQty, Status, C30fs, C30hs, C32fs, C32hs, C34fs, C34hs, C36fs, C36hs, Cxsfs, Cxshs, Csfs, Cshs, Cmfs, Cmhs, Clfs, Clhs, Cxlfs, Cxlhs, Cxxlfs, Cxxlhs, C3xlfs, C3xlhs, C4xlfs, C4xlhs, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));

                                            if (ddltype.SelectedValue == "Damage")
                                            {
                                                int istasDAMAGE = objbs.updatetransJpIroning_ReceivedSize123damage(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), ReceivedQty, RemainingQty, Status, C30fs, C30hs, C32fs, C32hs, C34fs, C34hs, C36fs, C36hs, Cxsfs, Cxshs, Csfs, Cshs, Cmfs, Cmhs, Clfs, Clhs, Cxlfs, Cxlhs, Cxxlfs, Cxxlhs, C3xlfs, C3xlhs, C4xlfs, C4xlhs, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));
                                            }
                                            else
                                            {
                                                if (ddlConsiderStock.SelectedValue == "Yes")
                                                {
                                                    #region Insert in Finished Stock Ratio

                                                    DataSet dsGetFinishdetails = objbs.GetFinishdetails(Convert.ToString(lblfitid.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), lbldesignno.Text);
                                                    if (dsGetFinishdetails.Tables[0].Rows.Count > 0)
                                                    {
                                                        #region




                                                        string Totalshirt = Convert.ToString(ReceivedQty);
                                                        string R30fs = txts30fs.Text;
                                                        string R30hs = txts30hs.Text;
                                                        string R32fs = txts32fs.Text;
                                                        string R32hs = txts32hs.Text;
                                                        string R34fs = txts34fs.Text;
                                                        string R34hs = txts34hs.Text;
                                                        string R36fs = txts36fs.Text;
                                                        string R36hs = txts36hs.Text;
                                                        string Rxsfs = txtsxsfs.Text;
                                                        string Rxshs = txtsxshs.Text;
                                                        string Rsfs = txtssfs.Text;
                                                        string Rshs = txtsshs.Text;
                                                        string Rmfs = txtsmfs.Text;
                                                        string Rmhs = txtsmhs.Text;
                                                        string Rlfs = txtslfs.Text;
                                                        string Rlhs = txtslhs.Text;
                                                        string Rxlfs = txtsxlfs.Text;
                                                        string Rxlhs = txtsxlhs.Text;
                                                        string Rxxlfs = txtsxxlfs.Text;
                                                        string Rxxlhs = txtsxxlhs.Text;
                                                        string R3xlfs = txts3xlfs.Text;
                                                        string R3xlhs = txts3xlhs.Text;
                                                        string R4xlfs = txts4xlfs.Text;
                                                        string R4xlhs = txts4xlhs.Text;
                                                        // int istockid = objbs.UPDATEFinishedstockwisestock(Convert.ToString(TransId), Convert.ToString(Totalshirt), txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y");
                                                        int istockid = objbs.updatenewallstockiro(Convert.ToString(TransId), Convert.ToString(Totalshirt), txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y", Convert.ToString(ddlLotNo.SelectedItem.Text), lbldesignno.Text, Convert.ToString(lblfitid.Text));
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        #region

                                                        DataSet dsManualStock_Details = objbs.getRemainingManualStock123(Convert.ToString(lblfitid.Text), Convert.ToString(ddlLotNo.SelectedItem), lbldesignno.Text);

                                                        //  string DesignCode = dsManualStock_Details.Tables[0].Rows[0]["DesignCode"].ToString();
                                                        string BrandId = dsManualStock_Details.Tables[0].Rows[0]["BrandId"].ToString();
                                                        string Fit = dsManualStock_Details.Tables[0].Rows[0]["Fit"].ToString();
                                                        string Itemname = dsManualStock_Details.Tables[0].Rows[0]["Itemname"].ToString();
                                                        string Companyid = dsManualStock_Details.Tables[0].Rows[0]["Companyid"].ToString();
                                                        string CompanyLotNo = dsManualStock_Details.Tables[0].Rows[0]["CompanyLotNo"].ToString();
                                                        string Totalshirt = Convert.ToString(ReceivedQty);

                                                        string R30fs = txts30fs.Text;
                                                        string R30hs = txts30hs.Text;
                                                        string R32fs = txts32fs.Text;
                                                        string R32hs = txts32hs.Text;
                                                        string R34fs = txts34fs.Text;
                                                        string R34hs = txts34hs.Text;
                                                        string R36fs = txts36fs.Text;
                                                        string R36hs = txts36hs.Text;
                                                        string Rxsfs = txtsxsfs.Text;
                                                        string Rxshs = txtsxshs.Text;
                                                        string Rsfs = txtssfs.Text;
                                                        string Rshs = txtsshs.Text;
                                                        string Rmfs = txtsmfs.Text;
                                                        string Rmhs = txtsmhs.Text;
                                                        string Rlfs = txtslfs.Text;
                                                        string Rlhs = txtslhs.Text;
                                                        string Rxlfs = txtsxlfs.Text;
                                                        string Rxlhs = txtsxlhs.Text;
                                                        string Rxxlfs = txtsxxlfs.Text;
                                                        string Rxxlhs = txtsxxlhs.Text;
                                                        string R3xlfs = txts3xlfs.Text;
                                                        string R3xlhs = txts3xlhs.Text;
                                                        string R4xlfs = txts4xlfs.Text;
                                                        string R4xlhs = txts4xlhs.Text;
                                                        int istockid = objbs.insertFinishedstockwisestock(Convert.ToString(TransId), "0", "0", "0", lbldesignno.Text, BrandId, Fit, Itemname, drpbranch.SelectedValue, CompanyLotNo, Totalshirt, "0", txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y");

                                                        #endregion
                                                    }

                                                    #endregion
                                                }
                                            }
                                        }
                                        else
                                        {
                                            RemainingQty = SendQty - ReceivedQty;
                                            string Status = "N";
                                            if (RemainingQty == 0)
                                            {
                                                Status = "Y";
                                            }

                                            int istas = objbs.updatetransJpIroning_ReceivedSize123(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), ReceivedQty, RemainingQty, Status, C30fs, C30hs, C32fs, C32hs, C34fs, C34hs, C36fs, C36hs, Cxsfs, Cxshs, Csfs, Cshs, Cmfs, Cmhs, Clfs, Clhs, Cxlfs, Cxlhs, Cxxlfs, Cxxlhs, C3xlfs, C3xlhs, C4xlfs, C4xlhs, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));

                                            if (ddltype.SelectedValue == "Damage")
                                            {
                                                int istasDAMAGE = objbs.updatetransJpIroning_ReceivedSize123damage(Convert.ToString(lblfitid.Text), ViewState["Ironingid"].ToString(), ReceivedQty, RemainingQty, Status, C30fs, C30hs, C32fs, C32hs, C34fs, C34hs, C36fs, C36hs, Cxsfs, Cxshs, Csfs, Cshs, Cmfs, Cmhs, Clfs, Clhs, Cxlfs, Cxlhs, Cxxlfs, Cxxlhs, C3xlfs, C3xlhs, C4xlfs, C4xlhs, lbldesignno.Text, Convert.ToInt32(lblIronTransID.Text));
                                            }
                                            else
                                            {
                                                if (ddlConsiderStock.SelectedValue == "Yes")
                                                {
                                                    #region Insert in Finished Stock Ratio

                                                    DataSet dsGetFinishdetails = objbs.GetFinishdetails(Convert.ToString(lblfitid.Text), Convert.ToString(ddlLotNo.SelectedItem.Text), lbldesignno.Text);
                                                    if (dsGetFinishdetails.Tables[0].Rows.Count > 0)
                                                    {
                                                        #region

                                                        string Totalshirt = Convert.ToString(ReceivedQty);
                                                        string R30fs = txts30fs.Text;
                                                        string R30hs = txts30hs.Text;
                                                        string R32fs = txts32fs.Text;
                                                        string R32hs = txts32hs.Text;
                                                        string R34fs = txts34fs.Text;
                                                        string R34hs = txts34hs.Text;
                                                        string R36fs = txts36fs.Text;
                                                        string R36hs = txts36hs.Text;
                                                        string Rxsfs = txtsxsfs.Text;
                                                        string Rxshs = txtsxshs.Text;
                                                        string Rsfs = txtssfs.Text;
                                                        string Rshs = txtsshs.Text;
                                                        string Rmfs = txtsmfs.Text;
                                                        string Rmhs = txtsmhs.Text;
                                                        string Rlfs = txtslfs.Text;
                                                        string Rlhs = txtslhs.Text;
                                                        string Rxlfs = txtsxlfs.Text;
                                                        string Rxlhs = txtsxlhs.Text;
                                                        string Rxxlfs = txtsxxlfs.Text;
                                                        string Rxxlhs = txtsxxlhs.Text;
                                                        string R3xlfs = txts3xlfs.Text;
                                                        string R3xlhs = txts3xlhs.Text;
                                                        string R4xlfs = txts4xlfs.Text;
                                                        string R4xlhs = txts4xlhs.Text;
                                                        // int istockid = objbs.UPDATEFinishedstockwisestock(Convert.ToString(TransId), Convert.ToString(Totalshirt), txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y");
                                                        int istockid = objbs.updatenewallstockiro(Convert.ToString(TransId), Convert.ToString(Totalshirt), txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y", Convert.ToString(ddlLotNo.SelectedItem.Text), lbldesignno.Text, Convert.ToString(lblfitid.Text));
                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        #region

                                                        DataSet dsManualStock_Details = objbs.getRemainingManualStock123(Convert.ToString(lblfitid.Text), Convert.ToString(ddlLotNo.SelectedItem), lbldesignno.Text);

                                                        //  string DesignCode = dsManualStock_Details.Tables[0].Rows[0]["DesignCode"].ToString();
                                                        string BrandId = dsManualStock_Details.Tables[0].Rows[0]["BrandId"].ToString();
                                                        string Fit = dsManualStock_Details.Tables[0].Rows[0]["Fit"].ToString();
                                                        string Itemname = dsManualStock_Details.Tables[0].Rows[0]["Itemname"].ToString();
                                                        string Companyid = dsManualStock_Details.Tables[0].Rows[0]["Companyid"].ToString();
                                                        string CompanyLotNo = dsManualStock_Details.Tables[0].Rows[0]["CompanyLotNo"].ToString();
                                                        string Totalshirt = Convert.ToString(ReceivedQty);

                                                        string R30fs = txts30fs.Text;
                                                        string R30hs = txts30hs.Text;
                                                        string R32fs = txts32fs.Text;
                                                        string R32hs = txts32hs.Text;
                                                        string R34fs = txts34fs.Text;
                                                        string R34hs = txts34hs.Text;
                                                        string R36fs = txts36fs.Text;
                                                        string R36hs = txts36hs.Text;
                                                        string Rxsfs = txtsxsfs.Text;
                                                        string Rxshs = txtsxshs.Text;
                                                        string Rsfs = txtssfs.Text;
                                                        string Rshs = txtsshs.Text;
                                                        string Rmfs = txtsmfs.Text;
                                                        string Rmhs = txtsmhs.Text;
                                                        string Rlfs = txtslfs.Text;
                                                        string Rlhs = txtslhs.Text;
                                                        string Rxlfs = txtsxlfs.Text;
                                                        string Rxlhs = txtsxlhs.Text;
                                                        string Rxxlfs = txtsxxlfs.Text;
                                                        string Rxxlhs = txtsxxlhs.Text;
                                                        string R3xlfs = txts3xlfs.Text;
                                                        string R3xlhs = txts3xlhs.Text;
                                                        string R4xlfs = txts4xlfs.Text;
                                                        string R4xlhs = txts4xlhs.Text;
                                                        int istockid = objbs.insertFinishedstockwisestock(Convert.ToString(TransId), "0", "0", "0", lbldesignno.Text, BrandId, Fit, Itemname, drpbranch.SelectedValue, CompanyLotNo, Totalshirt, "0", txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y");

                                                        #endregion
                                                    }

                                                    #endregion
                                                }
                                            }
                                        }
                                        #endregion
                                    }




                                    #endregion

                                }
                            }


                        }
                    }

                    #endregion

                }

                #region Accessories
                for (int a = 0; a < gvironstock.Rows.Count; a++)
                {
                    string ProcessId = Request.QueryString.Get("Ironingid");

                    #region
                    CheckBox chkiron = (CheckBox)gvironstock.Rows[a].FindControl("chkiron");
                    TextBox txtissueironstock = (TextBox)gvironstock.Rows[a].FindControl("txtissueironstock");

                    Label lblDefinition = (Label)gvironstock.Rows[a].FindControl("lblDefinition");
                    Label txtironstock = (Label)gvironstock.Rows[a].FindControl("txtironstock");
                    Label lblCategoryUserID = (Label)gvironstock.Rows[a].FindControl("lblCategoryUserID");

                    if (chkiron.Checked == true)
                    {
                        DataSet dsironmat = objbs.chkdsironmat(Convert.ToInt32(ProcessId), 5, ddlLotNo.SelectedItem.Text, Convert.ToInt32(lblCategoryUserID.Text), Convert.ToInt32(txtissueironstock.Text), Convert.ToInt32(drpbranch.SelectedValue));
                        if (dsironmat.Tables[0].Rows.Count > 0)
                        {
                            int insert = objbs.insertironmaterial(Convert.ToInt32(ProcessId), 5, ddlLotNo.SelectedItem.Text, Convert.ToInt32(lblCategoryUserID.Text), Convert.ToInt32(txtissueironstock.Text), Convert.ToInt32(drpbranch.SelectedValue), "Update");
                        }
                        else
                        {
                            int insert = objbs.insertironmaterial(Convert.ToInt32(ProcessId), 5, ddlLotNo.SelectedItem.Text, Convert.ToInt32(lblCategoryUserID.Text), Convert.ToInt32(txtissueironstock.Text), Convert.ToInt32(drpbranch.SelectedValue), "Insert");
                        }
                    }
                    else
                    {
                        DataSet dsironmat = objbs.chkdsironmat(Convert.ToInt32(ProcessId), 5, ddlLotNo.SelectedItem.Text, Convert.ToInt32(lblCategoryUserID.Text), Convert.ToInt32(txtissueironstock.Text), Convert.ToInt32(drpbranch.SelectedValue));
                        if (dsironmat.Tables[0].Rows.Count > 0)
                        {

                        }
                        else
                        {
                            int insert = objbs.insertironmaterial(Convert.ToInt32(ProcessId), 5, ddlLotNo.SelectedItem.Text, Convert.ToInt32(lblCategoryUserID.Text), Convert.ToInt32(0), Convert.ToInt32(drpbranch.SelectedValue), "Insert");
                        }
                    }
                    #endregion
                }

                #endregion


            }
            else if (btnadd.Text == "Receive")
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Administrator.Because Some process Received.')", true);
                return;
            }



            System.Threading.Thread.Sleep(3000);

            Response.Redirect("IroningGrid.aspx");


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