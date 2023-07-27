using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BusinessLayer;
using CommonLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;
using System.Drawing;

namespace Billing.Accountsbootstrap
{
    public partial class LotCompleteDetailReport : System.Web.UI.Page
    {
        DataSet ds1 = new DataSet();
        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
                string super = Session["IsSuperAdmin"].ToString();
                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "All");
                    }
                }
                else
                {
                    drpbranch.Enabled = false;
                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();

                    }
                }

                //////btnsearchnew_Click(sender, e);


                //////txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //////txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //////DataSet ds = objBs.getMasterStockRatiofull();
                ////////if (dss.Tables[0].Rows.Count > 0)
                ////////{
                ////////    gridcatqty.DataSource = dss;
                ////////    gridcatqty.DataBind();
                ////////}


                //////DataSet temp = new DataSet();
                //////DataTable dtt = new DataTable();

                //////dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                //////dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                //////dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                //////dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                //////dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                //////dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                //////dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                //////dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                //////dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                //////dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                //////dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                //////dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                //////dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));
               

                //////temp.Tables.Add(dtt);


                //////double tot = 0;
                //////double rate = 0;
                //////double TotalQty = 0;
                //////double TotalQty1 = 0;

                //////for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //////{
                //////    //int Masterid =Convert.ToInt32(ds.Tables[0].Rows[i]["Masterid"].ToString());
                //////    //int Cutid =Convert.ToInt32( ds.Tables[0].Rows[i]["Cutid"].ToString());
                //////    //int Transfabid =Convert.ToInt32( ds.Tables[0].Rows[i]["Transfabid"].ToString());

                //////    int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[i]["StockRatioId"].ToString());

                //////    int TotalDiffQty = 0;
                //////    int ReadyForDesPatch = 0;

                //////    int IroningId = 0;
                //////    int Transid = 0;
                //////    string Date = "";
                //////    string  CompanylotNo = "";
                //////    int BalanceQty = 0;

                //////    DataSet ds1 = objBs.getMasterStockRatiofull1(StockRatioId);
                //////    if (ds1.Tables[0].Rows.Count > 0)
                //////    {
                //////        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                //////        {
                //////            int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());
                //////            DataSet ds2 = objBs.getMasterStockRatiofull2(id);
                //////            if (ds2.Tables[0].Rows.Count > 0)
                //////            {
                //////                if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                //////                {
                //////                    string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                //////                    TotalDiffQty =TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                //////                }
                //////                ReadyForDesPatch =ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                //////            }

                //////            IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                //////            Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                //////            Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                //////            CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                //////            BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                //////        }
                //////    }
                //////    DataRow dr = dtt.NewRow(); 

                //////    dr["DesignCode"] = ds.Tables[0].Rows[i]["DesignCode"].ToString();
                //////    dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
                //////    dr["BrandName"] = ds.Tables[0].Rows[i]["BrandName"].ToString();
                //////    dr["CompanyLotNo"] = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                //////   int Val1 =Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString() )- Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());

                //////   // dr["TotalShirt"] = ds.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + Val1 +'-' + ds.Tables[0].Rows[i]["StoreQty"].ToString();
                //////   dr["TotalShirt"] = "Ttl-" + ds.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + ds.Tables[0].Rows[i]["StoreQty"].ToString();
                //////    dr["StoreQty"] = ds.Tables[0].Rows[i]["StoreQty"].ToString();

                //////    dr["IroningId"] = IroningId;
                //////    dr["Transid"] = Transid;
                //////    dr["Date"] = Date;
                //////    dr["CompanylotNo"] = CompanylotNo;
                //////    dr["BalanceQty"] = BalanceQty;
                //////    dr["ReadyForDesPatch"] = ReadyForDesPatch;

                //////    dr["TotalDiffQty"] = TotalDiffQty;

                //////    temp.Tables[0].Rows.Add(dr);
                //////}

                //////gridcatqty.DataSource = temp;
                //////gridcatqty.DataBind();

            }
        }
        protected void rdbtype_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdbtype.SelectedValue == "2")
            {
                #region

                DataSet ds = objBs.getMasterStockRatiofull(drpbranch.SelectedValue);

                DataSet temp = new DataSet();
                DataTable dtt = new DataTable();

                dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                temp.Tables.Add(dtt);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                   
                    int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[i]["StockRatioId"].ToString());
                    string Complete = ds.Tables[0].Rows[i]["Complete"].ToString();
                    string CompanyLotNo = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                    int TotalDiffQty = 0;
                    int ReadyForDesPatch = 0;

                    int IroningId = 0;
                    int Transid = 0;
                    string Date = "";
                    string CompanylotNo = "";
                    int BalanceQty = 0;

                    DataSet ds1 = objBs.getMasterStockRatiofull1(StockRatioId, Complete, CompanyLotNo);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());
                            string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();

                            DataSet ds2 = objBs.getMasterStockRatiofull2(id,CompleteVal);
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                                {
                                    string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                                    TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                                }
                                ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                            }

                            IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                            Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                            Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                            CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                            BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                        }
                    }
                    DataRow dr = dtt.NewRow();

                    dr["DesignCode"] = ds.Tables[0].Rows[i]["DesignCode"].ToString();
                    dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
                    dr["BrandName"] = ds.Tables[0].Rows[i]["BrandName"].ToString();
                    dr["CompanyLotNo"] = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                    int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());

                    dr["TotalShirt"] = "Ttl-" + ds.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + ds.Tables[0].Rows[i]["StoreQty"].ToString();
                    dr["StoreQty"] = ds.Tables[0].Rows[i]["StoreQty"].ToString();

                    dr["IroningId"] = IroningId;
                    dr["Transid"] = Transid;
                    dr["Date"] = Date;
                    dr["CompanylotNo"] = CompanylotNo;
                    dr["BalanceQty"] = BalanceQty;
                    dr["ReadyForDesPatch"] = ReadyForDesPatch;

                    dr["TotalDiffQty"] = TotalDiffQty;

                    temp.Tables[0].Rows.Add(dr);
                }

                gridcatqty.DataSource = temp;
                gridcatqty.DataBind();

                #endregion
            }
            else
            {
                #region

                DataSet ds = objBs.getMasterStockRatiofullnew(txtsearching.Text);
                DataSet dssummary = objBs.getMasterStockRatiofullsummarynew(txtsearching.Text, drpbranch.SelectedValue);

                DataSet temp = new DataSet();
                DataTable dtt = new DataTable();

                dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                temp.Tables.Add(dtt);

                for (int i = 0; i < dssummary.Tables[0].Rows.Count; i++)
                {
                    int TotalDiffQty = 0;
                    int ReadyForDesPatch = 0;

                    int IroningId = 0;
                    int Transid = 0;
                    string Date = "";
                    string CompanylotNo = "";
                    int BalanceQty = 0;

                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[k]["StockRatioId"].ToString());
                        string Complete = ds.Tables[0].Rows[k]["Complete"].ToString();
                        string CompanyLotNo = ds.Tables[0].Rows[k]["CompanyLotNo"].ToString();
                       

                        DataSet ds1 = objBs.getMasterStockRatiofull1(StockRatioId,Complete,CompanyLotNo);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                            {
                                int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());
                                string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();

                                DataSet ds2 = objBs.getMasterStockRatiofull2(id,CompleteVal);
                                if (ds2.Tables[0].Rows.Count > 0)
                                {
                                    if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                                    {
                                        string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                                        TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                                    }
                                    ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                                }

                                IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                                Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                                Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                                CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                                BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                            }
                        }
                    }


                    DataRow dr = dtt.NewRow();
                    dr["DesignCode"] = dssummary.Tables[0].Rows[i]["DesignCode"].ToString();
                    dr["ItemName"] = dssummary.Tables[0].Rows[i]["ItemName"].ToString();
                    dr["BrandName"] = dssummary.Tables[0].Rows[i]["BrandName"].ToString();
                    dr["CompanyLotNo"] = dssummary.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                    int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());
                    dr["TotalShirt"] = "Ttl-" + dssummary.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + dssummary.Tables[0].Rows[i]["StoreQty"].ToString();
                    dr["StoreQty"] = dssummary.Tables[0].Rows[i]["StoreQty"].ToString();

                    dr["IroningId"] = IroningId;
                    dr["Transid"] = Transid;
                    dr["Date"] = Date;
                    dr["CompanylotNo"] = CompanylotNo;
                    dr["BalanceQty"] = BalanceQty;
                    dr["ReadyForDesPatch"] = ReadyForDesPatch;
                    dr["TotalDiffQty"] = TotalDiffQty;
                    temp.Tables[0].Rows.Add(dr);
                }

                gridcatqty.DataSource = temp;
                gridcatqty.DataBind();

                #endregion
            }

        }
        protected void txtsearching_OnTextChanged(object sender, EventArgs e)
        {
            if (rdbtype.SelectedValue == "2")
            {
                #region

                DataSet ds = objBs.getMasterStockRatiofull(drpbranch.SelectedValue);

                DataSet temp = new DataSet();
                DataTable dtt = new DataTable();

                dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                temp.Tables.Add(dtt);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[i]["StockRatioId"].ToString());
                    string Complete = ds.Tables[0].Rows[i]["Complete"].ToString();
                    string CompanyLotNo = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                    int TotalDiffQty = 0;
                    int ReadyForDesPatch = 0;

                    int IroningId = 0;
                    int Transid = 0;
                    string Date = "";
                    string CompanylotNo = "";
                    int BalanceQty = 0;

                    DataSet ds1 = objBs.getMasterStockRatiofull1(StockRatioId,Complete,CompanyLotNo);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                        {
                            int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());
                            string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();

                            DataSet ds2 = objBs.getMasterStockRatiofull2(id, CompleteVal);
                            if (ds2.Tables[0].Rows.Count > 0)
                            {
                                if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                                {
                                    string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                                    TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                                }
                                ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                            }

                            IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                            Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                            Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                            CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                            BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                        }
                    }
                    DataRow dr = dtt.NewRow();

                    dr["DesignCode"] = ds.Tables[0].Rows[i]["DesignCode"].ToString();
                    dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
                    dr["BrandName"] = ds.Tables[0].Rows[i]["BrandName"].ToString();
                    dr["CompanyLotNo"] = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                    int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());

                    dr["TotalShirt"] = "Ttl-" + ds.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + ds.Tables[0].Rows[i]["StoreQty"].ToString();
                    dr["StoreQty"] = ds.Tables[0].Rows[i]["StoreQty"].ToString();

                    dr["IroningId"] = IroningId;
                    dr["Transid"] = Transid;
                    dr["Date"] = Date;
                    dr["CompanylotNo"] = CompanylotNo;
                    dr["BalanceQty"] = BalanceQty;
                    dr["ReadyForDesPatch"] = ReadyForDesPatch;

                    dr["TotalDiffQty"] = TotalDiffQty;

                    temp.Tables[0].Rows.Add(dr);
                }

                gridcatqty.DataSource = temp;
                gridcatqty.DataBind();

                #endregion
            }
            else
            {
                #region

                DataSet ds = objBs.getMasterStockRatiofullnew(txtsearching.Text);
                DataSet dssummary = objBs.getMasterStockRatiofullsummarynew(txtsearching.Text, drpbranch.SelectedValue);

                DataSet temp = new DataSet();
                DataTable dtt = new DataTable();

                dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                temp.Tables.Add(dtt);

                for (int i = 0; i < dssummary.Tables[0].Rows.Count; i++)
                {
                    int TotalDiffQty = 0;
                    int ReadyForDesPatch = 0;

                    int IroningId = 0;
                    int Transid = 0;
                    string Date = "";
                    string CompanylotNo = "";
                    int BalanceQty = 0;

                    for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                    {
                        int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[k]["StockRatioId"].ToString());
                        string Complete = ds.Tables[0].Rows[k]["Complete"].ToString();
                        string CompanyLotNo = ds.Tables[0].Rows[k]["CompanyLotNo"].ToString();


                        DataSet ds1 = objBs.getMasterStockRatiofull1(StockRatioId,Complete,CompanyLotNo);
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                            {
                                int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());
                                string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();
                                DataSet ds2 = objBs.getMasterStockRatiofull2(id,CompleteVal);
                                if (ds2.Tables[0].Rows.Count > 0)
                                {
                                    if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                                    {
                                        string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                                        TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                                    }
                                    ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                                }

                                IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                                Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                                Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                                CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                                BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                            }
                        }
                    }


                    DataRow dr = dtt.NewRow();
                    dr["DesignCode"] = dssummary.Tables[0].Rows[i]["DesignCode"].ToString();
                    dr["ItemName"] = dssummary.Tables[0].Rows[i]["ItemName"].ToString();
                    dr["BrandName"] = dssummary.Tables[0].Rows[i]["BrandName"].ToString();
                    dr["CompanyLotNo"] = dssummary.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                    int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());
                    dr["TotalShirt"] = "Ttl-" + dssummary.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + dssummary.Tables[0].Rows[i]["StoreQty"].ToString();
                    dr["StoreQty"] = dssummary.Tables[0].Rows[i]["StoreQty"].ToString();

                    dr["IroningId"] = IroningId;
                    dr["Transid"] = Transid;
                    dr["Date"] = Date;
                    dr["CompanylotNo"] = CompanylotNo;
                    dr["BalanceQty"] = BalanceQty;
                    dr["ReadyForDesPatch"] = ReadyForDesPatch;
                    dr["TotalDiffQty"] = TotalDiffQty;
                    temp.Tables[0].Rows.Add(dr);
                }

                gridcatqty.DataSource = temp;
                gridcatqty.DataBind();

                #endregion
            }

        }

        protected void btnsearchnew_Click(object sender, EventArgs e)
        {
            
                if(txtsearching.Text=="")
                {
                    #region
                    if (rdbtype.SelectedValue == "2")
                    {
                        #region

                        DataSet ds = objBs.getMasterStockRatiofull(drpbranch.SelectedValue);

                        DataSet temp = new DataSet();
                        DataTable dtt = new DataTable();

                        dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                        dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                        dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                        dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                        dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                        dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                        dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                        temp.Tables.Add(dtt);

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[i]["StockRatioId"].ToString());
                            string Complete = ds.Tables[0].Rows[i]["Complete"].ToString();
                            string CompanyLotNo = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                            string DesignCode = ds.Tables[0].Rows[i]["DesignCode"].ToString();

                            if (CompanyLotNo == "RPL-44")
                            {

                            }
                            int TotalDiffQty = 0;
                            int ReadyForDesPatch = 0;

                            int IroningId = 0;
                            int Transid = 0;
                            string Date = "";
                            string CompanylotNo = "";
                            int BalanceQty = 0;


                            DataSet ds1 = objBs.getMasterStockRatiofull2(StockRatioId, Complete, CompanyLotNo, DesignCode);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                                {
                                    int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());
                                    string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();
                                    DataSet ds2 = objBs.getMasterStockRatiofull2(id,CompleteVal);
                                    if (ds2.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                                        {
                                            string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                                            TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                                        }
                                        ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                                    }

                                    IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                                    Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                                    Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                                    CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                                    BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                                }
                            }
                            DataRow dr = dtt.NewRow();

                            dr["DesignCode"] = ds.Tables[0].Rows[i]["DesignCode"].ToString();
                            dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
                            dr["BrandName"] = ds.Tables[0].Rows[i]["BrandName"].ToString();
                            dr["CompanyLotNo"] = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                            int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());

                            dr["TotalShirt"] = "Ttl-" + ds.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + ds.Tables[0].Rows[i]["StoreQty"].ToString();
                            dr["StoreQty"] = ds.Tables[0].Rows[i]["StoreQty"].ToString();

                            dr["IroningId"] = IroningId;
                            dr["Transid"] = Transid;
                            dr["Date"] = Date;
                            dr["CompanylotNo"] = CompanylotNo;
                            dr["BalanceQty"] = BalanceQty;
                            dr["ReadyForDesPatch"] = ReadyForDesPatch;

                            dr["TotalDiffQty"] = TotalDiffQty;

                            temp.Tables[0].Rows.Add(dr);
                        }

                        gridcatqty.DataSource = temp;
                        gridcatqty.DataBind();

                        #endregion
                    }
                    else
                    {
                        #region

                      //  DataSet ds = objBs.getMasterStockRatiofullnew(txtsearching.Text);
                        DataSet dssummary = objBs.getMasterStockRatiofullsummaryall(drpbranch.SelectedValue);

                        DataSet temp = new DataSet();
                        DataTable dtt = new DataTable();

                        dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                        dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                        dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                        dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                        dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                        dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                        dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                        temp.Tables.Add(dtt);

                        for (int i = 0; i < dssummary.Tables[0].Rows.Count; i++)
                        {
                            int TotalDiffQty = 0;
                            int ReadyForDesPatch = 0;

                            int IroningId = 0;
                            int Transid = 0;
                            string Date = "";
                            string CompanylotNo = "";
                            int BalanceQty = 0;

                            int StockRatioId = Convert.ToInt32(dssummary.Tables[0].Rows[i]["StockRatioId"].ToString());
                            string Complete = dssummary.Tables[0].Rows[i]["Complete"].ToString();
                            string CompanyLotNo = dssummary.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                            DataSet dsBalance = objBs.getunreceivestksummary(StockRatioId, Complete, CompanyLotNo);
                            DataSet dsstock = objBs.getfinishedstksummary(CompanyLotNo);
                            DataSet dsdespatch = objBs.getdespatchedsummary(CompanyLotNo);

                            DataRow dr = dtt.NewRow();
                            dr["DesignCode"] = dssummary.Tables[0].Rows[i]["DesignCode"].ToString();
                            dr["ItemName"] = dssummary.Tables[0].Rows[i]["ItemName"].ToString();
                            dr["BrandName"] = dssummary.Tables[0].Rows[i]["BrandName"].ToString();
                            dr["CompanyLotNo"] = dssummary.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                         
                            dr["TotalShirt"] = "Ttl-" + dssummary.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + dssummary.Tables[0].Rows[i]["StoreQty"].ToString();
                            dr["StoreQty"] = dssummary.Tables[0].Rows[i]["StoreQty"].ToString();

                            dr["IroningId"] = IroningId;
                            dr["Transid"] = Transid;
                            dr["Date"] = Date;
                            dr["CompanylotNo"] = CompanylotNo;

                            dr["BalanceQty"] = dsBalance.Tables[0].Rows[0]["BalanceQty"].ToString();
                            dr["ReadyForDesPatch"] = dsstock.Tables[0].Rows[0]["ReadyForDesPatch"].ToString();
                            dr["TotalDiffQty"] = dsdespatch.Tables[0].Rows[0]["DesPatchedqty"].ToString();
                            temp.Tables[0].Rows.Add(dr);
                        }

                        gridcatqty.DataSource = temp;
                        gridcatqty.DataBind();

                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region
                    if (rdbtype.SelectedValue == "2")
                    {
                        #region

                        DataSet ds = objBs.getStockRatiodetailied(txtsearching.Text, drpbranch.SelectedValue);

                        DataSet temp = new DataSet();
                        DataTable dtt = new DataTable();

                        dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                        dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                        dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                        dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                        dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                        dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                        dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                        temp.Tables.Add(dtt);

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {

                            int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[i]["StockRatioId"].ToString());
                            string Complete = ds.Tables[0].Rows[i]["Complete"].ToString();
                            string CompanyLotNo = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                            string DesignCode = ds.Tables[0].Rows[i]["DesignCode"].ToString();

                            int TotalDiffQty = 0;
                            int ReadyForDesPatch = 0;

                            int IroningId = 0;
                            int Transid = 0;
                            string Date = "";
                            string CompanylotNo = "";
                            int BalanceQty = 0;

                            DataSet ds1 = objBs.getMasterStockRatiofull2(StockRatioId, Complete, CompanyLotNo, DesignCode);
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                                {
                                    string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();
                                    int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());

                                    DataSet ds2 = objBs.getMasterStockRatiofull2(id, CompleteVal);
                                    if (ds2.Tables[0].Rows.Count > 0)
                                    {
                                        if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                                        {
                                            string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                                            TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                                        }
                                        ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                                    }

                                    IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                                    Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                                    Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                                    CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                                    BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                                }
                            }
                            DataRow dr = dtt.NewRow();

                            dr["DesignCode"] = ds.Tables[0].Rows[i]["DesignCode"].ToString();
                            dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
                            dr["BrandName"] = ds.Tables[0].Rows[i]["BrandName"].ToString();
                            dr["CompanyLotNo"] = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                            int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());

                            dr["TotalShirt"] = "Ttl-" + ds.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + ds.Tables[0].Rows[i]["StoreQty"].ToString();
                            dr["StoreQty"] = ds.Tables[0].Rows[i]["StoreQty"].ToString();

                            dr["IroningId"] = IroningId;
                            dr["Transid"] = Transid;
                            dr["Date"] = Date;
                            dr["CompanylotNo"] = CompanylotNo;
                            dr["BalanceQty"] = BalanceQty;
                            dr["ReadyForDesPatch"] = ReadyForDesPatch;

                            dr["TotalDiffQty"] = TotalDiffQty;

                            temp.Tables[0].Rows.Add(dr);
                        }

                        gridcatqty.DataSource = temp;
                        gridcatqty.DataBind();

                        #endregion
                    }
                    else
                    {
                        #region

                        DataSet ds = objBs.getMasterStockRatiofullnew(txtsearching.Text);
                        DataSet dssummary = objBs.getMasterStockRatiofullsummarynew(txtsearching.Text,drpbranch.SelectedValue);

                        DataSet temp = new DataSet();
                        DataTable dtt = new DataTable();

                        dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                        dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                        dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                        dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                        dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                        dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                        dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                        temp.Tables.Add(dtt);

                        for (int i = 0; i < dssummary.Tables[0].Rows.Count; i++)
                        {
                            int TotalDiffQty = 0;
                            int ReadyForDesPatch = 0;

                            int IroningId = 0;
                            int Transid = 0;
                            string Date = "";   
                            string CompanylotNo = "";
                            int BalanceQty = 0;

                            int StockRatioId = Convert.ToInt32(dssummary.Tables[0].Rows[i]["StockRatioId"].ToString());
                            string Complete = dssummary.Tables[0].Rows[i]["Complete"].ToString();
                            string CompanyLotNo = dssummary.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                            DataSet dsBalance = objBs.getunreceivestksummary(StockRatioId, Complete, CompanyLotNo);
                            DataSet dsstock = objBs.getfinishedstksummary(CompanyLotNo);
                            DataSet dsdespatch = objBs.getdespatchedsummary(CompanyLotNo); 

                            //////for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                            //////{
                            //////    int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[k]["StockRatioId"].ToString());
                            //////    string Complete = ds.Tables[0].Rows[k]["Complete"].ToString();
                            //////    string CompanyLotNo = ds.Tables[0].Rows[k]["CompanyLotNo"].ToString();

                            //////    DataSet ds1 = objBs.getMasterStockRatiofull1(StockRatioId, Complete, CompanyLotNo);
                            //////    if (ds1.Tables[0].Rows.Count > 0)
                            //////    {
                            //////        for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                            //////        {
                            //////            int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());
                            //////            string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();

                            //////            DataSet ds2 = objBs.getMasterStockRatiofull2(id,CompleteVal);
                            //////            if (ds2.Tables[0].Rows.Count > 0)
                            //////            {
                            //////                if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                            //////                {
                            //////                    string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                            //////                    TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                            //////                }
                            //////                ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                            //////            }

                            //////            IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                            //////            Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                            //////            Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                            //////            CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                            //////            BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                            //////        }
                            //////    }
                            //////}


                            DataRow dr = dtt.NewRow();
                            dr["DesignCode"] = dssummary.Tables[0].Rows[i]["DesignCode"].ToString();
                            dr["ItemName"] = dssummary.Tables[0].Rows[i]["ItemName"].ToString();
                            dr["BrandName"] = dssummary.Tables[0].Rows[i]["BrandName"].ToString();
                            dr["CompanyLotNo"] = dssummary.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                            //int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());
                            dr["TotalShirt"] = "Ttl-" + dssummary.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + dssummary.Tables[0].Rows[i]["StoreQty"].ToString();
                            dr["StoreQty"] = dssummary.Tables[0].Rows[i]["StoreQty"].ToString();

                            dr["IroningId"] = IroningId;
                            dr["Transid"] = Transid;
                            dr["Date"] = Date;
                            dr["CompanylotNo"] = CompanylotNo;

                            dr["BalanceQty"] = dsBalance.Tables[0].Rows[0]["BalanceQty"].ToString();
                            dr["ReadyForDesPatch"] = dsstock.Tables[0].Rows[0]["ReadyForDesPatch"].ToString();
                            dr["TotalDiffQty"] = dsdespatch.Tables[0].Rows[0]["DesPatchedqty"].ToString();
                            temp.Tables[0].Rows.Add(dr);
                        }

                        gridcatqty.DataSource = temp;
                        gridcatqty.DataBind();

                        #endregion
                    }
                    #endregion
                }



            #region Old Process

                //if (txtsearching.Text == "")
                //{
                //    #region
                //    if (rdbtype.SelectedValue == "2")
                //    {
                //        #region

                //        DataSet ds = objBs.getMasterStockRatiofull(drpbranch.SelectedValue);

                //        DataSet temp = new DataSet();
                //        DataTable dtt = new DataTable();

                //        dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                //        dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                //        dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                //        temp.Tables.Add(dtt);

                //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //        {

                //            int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[i]["StockRatioId"].ToString());
                //            string Complete = ds.Tables[0].Rows[i]["Complete"].ToString();
                //            string CompanyLotNo = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                //            if (CompanyLotNo == "RPL-44")
                //            {

                //            }
                //            int TotalDiffQty = 0;
                //            int ReadyForDesPatch = 0;

                //            int IroningId = 0;
                //            int Transid = 0;
                //            string Date = "";
                //            string CompanylotNo = "";
                //            int BalanceQty = 0;

                //            DataSet ds1 = objBs.getMasterStockRatiofull1(StockRatioId, Complete, CompanyLotNo);
                //            if (ds1.Tables[0].Rows.Count > 0)
                //            {
                //                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                //                {
                //                    int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());
                //                    string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();
                //                    DataSet ds2 = objBs.getMasterStockRatiofull2(id, CompleteVal);
                //                    if (ds2.Tables[0].Rows.Count > 0)
                //                    {
                //                        if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                //                        {
                //                            string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                //                            TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                //                        }
                //                        ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                //                    }

                //                    IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                //                    Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                //                    Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                //                    CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                //                    BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                //                }
                //            }
                //            DataRow dr = dtt.NewRow();

                //            dr["DesignCode"] = ds.Tables[0].Rows[i]["DesignCode"].ToString();
                //            dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
                //            dr["BrandName"] = ds.Tables[0].Rows[i]["BrandName"].ToString();
                //            dr["CompanyLotNo"] = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                //            int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());

                //            dr["TotalShirt"] = "Ttl-" + ds.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + ds.Tables[0].Rows[i]["StoreQty"].ToString();
                //            dr["StoreQty"] = ds.Tables[0].Rows[i]["StoreQty"].ToString();

                //            dr["IroningId"] = IroningId;
                //            dr["Transid"] = Transid;
                //            dr["Date"] = Date;
                //            dr["CompanylotNo"] = CompanylotNo;
                //            dr["BalanceQty"] = BalanceQty;
                //            dr["ReadyForDesPatch"] = ReadyForDesPatch;

                //            dr["TotalDiffQty"] = TotalDiffQty;

                //            temp.Tables[0].Rows.Add(dr);
                //        }

                //        gridcatqty.DataSource = temp;
                //        gridcatqty.DataBind();

                //        #endregion
                //    }
                //    else
                //    {
                //        #region

                //        DataSet ds = objBs.getMasterStockRatiofullnew(txtsearching.Text);
                //        DataSet dssummary = objBs.getMasterStockRatiofullsummarynew(txtsearching.Text);

                //        DataSet temp = new DataSet();
                //        DataTable dtt = new DataTable();

                //        dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                //        dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                //        dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                //        temp.Tables.Add(dtt);

                //        for (int i = 0; i < dssummary.Tables[0].Rows.Count; i++)
                //        {
                //            int TotalDiffQty = 0;
                //            int ReadyForDesPatch = 0;

                //            int IroningId = 0;
                //            int Transid = 0;
                //            string Date = "";
                //            string CompanylotNo = "";
                //            int BalanceQty = 0;

                //            for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                //            {
                //                int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[k]["StockRatioId"].ToString());
                //                string Complete = ds.Tables[0].Rows[k]["Complete"].ToString();
                //                string CompanyLotNo = ds.Tables[0].Rows[k]["CompanyLotNo"].ToString();


                //                DataSet ds1 = objBs.getMasterStockRatiofull1(StockRatioId, Complete, CompanyLotNo);
                //                if (ds1.Tables[0].Rows.Count > 0)
                //                {
                //                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                //                    {
                //                        int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());
                //                        string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();
                //                        DataSet ds2 = objBs.getMasterStockRatiofull2(id, CompleteVal);
                //                        if (ds2.Tables[0].Rows.Count > 0)
                //                        {
                //                            if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                //                            {
                //                                string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                //                                TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                //                            }
                //                            ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                //                        }

                //                        IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                //                        Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                //                        Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                //                        CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                //                        BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                //                    }
                //                }
                //            }


                //            DataRow dr = dtt.NewRow();
                //            dr["DesignCode"] = dssummary.Tables[0].Rows[i]["DesignCode"].ToString();
                //            dr["ItemName"] = dssummary.Tables[0].Rows[i]["ItemName"].ToString();
                //            dr["BrandName"] = dssummary.Tables[0].Rows[i]["BrandName"].ToString();
                //            dr["CompanyLotNo"] = dssummary.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                //            int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());
                //            dr["TotalShirt"] = "Ttl-" + dssummary.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + dssummary.Tables[0].Rows[i]["StoreQty"].ToString();
                //            dr["StoreQty"] = dssummary.Tables[0].Rows[i]["StoreQty"].ToString();

                //            dr["IroningId"] = IroningId;
                //            dr["Transid"] = Transid;
                //            dr["Date"] = Date;
                //            dr["CompanylotNo"] = CompanylotNo;
                //            dr["BalanceQty"] = BalanceQty;
                //            dr["ReadyForDesPatch"] = ReadyForDesPatch;
                //            dr["TotalDiffQty"] = TotalDiffQty;
                //            temp.Tables[0].Rows.Add(dr);
                //        }

                //        gridcatqty.DataSource = temp;
                //        gridcatqty.DataBind();

                //        #endregion
                //    }
                //    #endregion
                //}
                //else
                //{
                //    #region
                //    if (rdbtype.SelectedValue == "2")
                //    {
                //        #region

                //        DataSet ds = objBs.getMasterStockRatiofull(drpbranch.SelectedValue);

                //        DataSet temp = new DataSet();
                //        DataTable dtt = new DataTable();

                //        dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                //        dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                //        dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                //        temp.Tables.Add(dtt);

                //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //        {

                //            int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[i]["StockRatioId"].ToString());
                //            string Complete = ds.Tables[0].Rows[i]["Complete"].ToString();
                //            string CompanyLotNo = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                //            int TotalDiffQty = 0;
                //            int ReadyForDesPatch = 0;

                //            int IroningId = 0;
                //            int Transid = 0;
                //            string Date = "";
                //            string CompanylotNo = "";
                //            int BalanceQty = 0;

                //            DataSet ds1 = objBs.getMasterStockRatiofull1(StockRatioId, Complete, CompanyLotNo);
                //            if (ds1.Tables[0].Rows.Count > 0)
                //            {
                //                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                //                {
                //                    string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();
                //                    int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());

                //                    DataSet ds2 = objBs.getMasterStockRatiofull2(id, CompleteVal);
                //                    if (ds2.Tables[0].Rows.Count > 0)
                //                    {
                //                        if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                //                        {
                //                            string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                //                            TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                //                        }
                //                        ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                //                    }

                //                    IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                //                    Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                //                    Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                //                    CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                //                    BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                //                }
                //            }
                //            DataRow dr = dtt.NewRow();

                //            dr["DesignCode"] = ds.Tables[0].Rows[i]["DesignCode"].ToString();
                //            dr["ItemName"] = ds.Tables[0].Rows[i]["ItemName"].ToString();
                //            dr["BrandName"] = ds.Tables[0].Rows[i]["BrandName"].ToString();
                //            dr["CompanyLotNo"] = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();

                //            int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());

                //            dr["TotalShirt"] = "Ttl-" + ds.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + ds.Tables[0].Rows[i]["StoreQty"].ToString();
                //            dr["StoreQty"] = ds.Tables[0].Rows[i]["StoreQty"].ToString();

                //            dr["IroningId"] = IroningId;
                //            dr["Transid"] = Transid;
                //            dr["Date"] = Date;
                //            dr["CompanylotNo"] = CompanylotNo;
                //            dr["BalanceQty"] = BalanceQty;
                //            dr["ReadyForDesPatch"] = ReadyForDesPatch;

                //            dr["TotalDiffQty"] = TotalDiffQty;

                //            temp.Tables[0].Rows.Add(dr);
                //        }

                //        gridcatqty.DataSource = temp;
                //        gridcatqty.DataBind();

                //        #endregion
                //    }
                //    else
                //    {
                //        #region

                //        DataSet ds = objBs.getMasterStockRatiofullnew(txtsearching.Text);
                //        DataSet dssummary = objBs.getMasterStockRatiofullsummarynew(txtsearching.Text);

                //        DataSet temp = new DataSet();
                //        DataTable dtt = new DataTable();

                //        dtt.Columns.Add(new DataColumn("DesignCode", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("BrandName", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("CompanyLotNo", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("TotalShirt", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("StoreQty", typeof(string)));

                //        dtt.Columns.Add(new DataColumn("IroningId", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("Transid", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("CompanylotNo", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("BalanceQty", typeof(string)));
                //        dtt.Columns.Add(new DataColumn("ReadyForDesPatch", typeof(string)));

                //        dtt.Columns.Add(new DataColumn("TotalDiffQty", typeof(string)));


                //        temp.Tables.Add(dtt);

                //        for (int i = 0; i < dssummary.Tables[0].Rows.Count; i++)
                //        {
                //            int TotalDiffQty = 0;
                //            int ReadyForDesPatch = 0;

                //            int IroningId = 0;
                //            int Transid = 0;
                //            string Date = "";
                //            string CompanylotNo = "";
                //            int BalanceQty = 0;

                //            for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                //            {
                //                int StockRatioId = Convert.ToInt32(ds.Tables[0].Rows[k]["StockRatioId"].ToString());
                //                string Complete = ds.Tables[0].Rows[k]["Complete"].ToString();
                //                string CompanyLotNo = ds.Tables[0].Rows[k]["CompanyLotNo"].ToString();

                //                DataSet ds1 = objBs.getMasterStockRatiofull1(StockRatioId, Complete, CompanyLotNo);
                //                if (ds1.Tables[0].Rows.Count > 0)
                //                {
                //                    for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                //                    {
                //                        int id = Convert.ToInt32(ds1.Tables[0].Rows[j]["Transid"].ToString());
                //                        string CompleteVal = ds1.Tables[0].Rows[j]["Complete"].ToString();

                //                        DataSet ds2 = objBs.getMasterStockRatiofull2(id, CompleteVal);
                //                        if (ds2.Tables[0].Rows.Count > 0)
                //                        {
                //                            if (ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString() != "")
                //                            {
                //                                string TotalDiffQty1 = ds2.Tables[0].Rows[0]["TotalDiffQty"].ToString();
                //                                TotalDiffQty = TotalDiffQty + Convert.ToInt32(TotalDiffQty1);
                //                            }
                //                            ReadyForDesPatch = ReadyForDesPatch + Convert.ToInt32(ds2.Tables[0].Rows[0]["ReadyForDesPatch"].ToString());
                //                        }

                //                        IroningId = Convert.ToInt32(ds1.Tables[0].Rows[0]["IroningId"].ToString());
                //                        Transid = Convert.ToInt32(ds1.Tables[0].Rows[0]["Transid"].ToString());
                //                        Date = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                //                        CompanylotNo = ds1.Tables[0].Rows[0]["CompanylotNo"].ToString();
                //                        BalanceQty = BalanceQty + Convert.ToInt32(ds1.Tables[0].Rows[j]["BalanceQty"].ToString());
                //                    }
                //                }
                //            }


                //            DataRow dr = dtt.NewRow();
                //            dr["DesignCode"] = dssummary.Tables[0].Rows[i]["DesignCode"].ToString();
                //            dr["ItemName"] = dssummary.Tables[0].Rows[i]["ItemName"].ToString();
                //            dr["BrandName"] = dssummary.Tables[0].Rows[i]["BrandName"].ToString();
                //            dr["CompanyLotNo"] = dssummary.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                //            int Val1 = Convert.ToInt32(ds.Tables[0].Rows[i]["TotalShirt"].ToString()) - Convert.ToInt32(ds.Tables[0].Rows[i]["StoreQty"].ToString());
                //            dr["TotalShirt"] = "Ttl-" + dssummary.Tables[0].Rows[i]["TotalShirt"].ToString() + '=' + "Bal-" + dssummary.Tables[0].Rows[i]["StoreQty"].ToString();
                //            dr["StoreQty"] = dssummary.Tables[0].Rows[i]["StoreQty"].ToString();

                //            dr["IroningId"] = IroningId;
                //            dr["Transid"] = Transid;
                //            dr["Date"] = Date;
                //            dr["CompanylotNo"] = CompanylotNo;
                //            dr["BalanceQty"] = BalanceQty;
                //            dr["ReadyForDesPatch"] = ReadyForDesPatch;
                //            dr["TotalDiffQty"] = TotalDiffQty;
                //            temp.Tables[0].Rows.Add(dr);
                //        }

                //        gridcatqty.DataSource = temp;
                //        gridcatqty.DataBind();

                //        #endregion
                //    }
                //    #endregion
                //}
            #endregion

        }
        public void gvCustsales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

            }
            else if (e.CommandName == "print")
            {
                Response.Redirect("DespatchPrint.aspx?DSPid=" + e.CommandArgument.ToString());

                //DataSet dss = objBs.GETPRINTOFDESPATCH(Convert.ToInt32(e.CommandArgument.ToString()));
                //if (dss.Tables[0].Rows.Count > 0)
                //{
                //    string dcno = dss.Tables[0].Rows[0]["DcNo"].ToString();
                //    string dcdate = Convert.ToDateTime(dss.Tables[0].Rows[0]["DcDate"]).ToString("dd/MM/yyyy");
                //    string ledgername = dss.Tables[0].Rows[0]["ledgername"].ToString();
                //    string Narration = dss.Tables[0].Rows[0]["Narration"].ToString();
                //    string TotalQty = dss.Tables[0].Rows[0]["TotalQty"].ToString();
                //    string Customer = dss.Tables[0].Rows[0]["CustomerName"].ToString();

                //    gvprint.Caption = " DcNo :- " + dcno + " , " + " DcDate :- " + dcdate +  " <br />" + " Customer Name :- " + Customer + " , " + " Despatcher Name :- " +  ledgername + " <br />" + " Narration :- " + Narration + " , " + " TotalQty :- " + TotalQty + " <br /> " + " Generate On " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                //    gvprint.DataSource = dss;
                //    gvprint.DataBind();

                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Denomination", "Denomination();", true);
                //}

            }
        }

        protected void btnsearch_OnClick(object sender, EventArgs e)
        {


            DateTime fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            DataSet dss = objBs.serchdeapatch(fromdate, todate,drpbranch.SelectedValue);
            if (dss.Tables[0].Rows.Count > 0)
            {
                string dcno = dss.Tables[0].Rows[0]["DcNo"].ToString();
                string dcdate = Convert.ToDateTime(dss.Tables[0].Rows[0]["DcDate"]).ToString("dd/MM/yyyy");
                string ledgername = dss.Tables[0].Rows[0]["ledgername"].ToString();
                string Narration = dss.Tables[0].Rows[0]["Narration"].ToString();
                string TotalQty = dss.Tables[0].Rows[0]["TotalQty"].ToString();
                string Customer = dss.Tables[0].Rows[0]["CustomerName"].ToString();

            }

            gridcatqty.Caption = " Despatch Detail Report From " + txtFromDate.Text + " To " + txtToDate.Text + " Generate On " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            gridcatqty.DataSource = dss;
            gridcatqty.DataBind();
        }

    }
}