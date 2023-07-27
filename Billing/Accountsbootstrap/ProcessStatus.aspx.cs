using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class ProcessStatus : System.Web.UI.Page
    {

        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtfromdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                #region
                string super = Session["IsSuperAdmin"].ToString();
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
                        drpbranch.Items.Insert(0, "ALL");
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

                    }
                }
                #endregion
            }
        }

        protected void btnsearch_OnClick(object sender, EventArgs e)
        {
            DateTime fromDate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet dss = new DataSet();
            DataTable dt = new DataTable();

            DataSet ds = objbs.Getlotval(drpbranch.SelectedValue, fromDate, toDate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataColumn dc = new DataColumn("LotNo");
                dt.Columns.Add(dc);
                dc = new DataColumn("LotValue");
                dt.Columns.Add(dc);
                dc = new DataColumn("Date");
                dt.Columns.Add(dc);
                dc = new DataColumn("Masterid");
                dt.Columns.Add(dc);

                dc = new DataColumn("UnUse");
                dt.Columns.Add(dc);
                dc = new DataColumn("Use");
                dt.Columns.Add(dc);
                dc = new DataColumn("Aqty");
                dt.Columns.Add(dc);
                dc = new DataColumn("Godown");
                dt.Columns.Add(dc);
                dc = new DataColumn("Despatch");
                dt.Columns.Add(dc);
                dc = new DataColumn("Return");
                dt.Columns.Add(dc);

                dss.Tables.Add(dt);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    DataRow dr = dt.NewRow();

                    #region

                    dr["LotNo"] = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                    dr["LotValue"] = ds.Tables[0].Rows[i]["Qty"].ToString() + "  " + ds.Tables[0].Rows[i]["CompleteStitching"].ToString();
                    dr["Date"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]).ToString("dd/MM/yyyy");
                    dr["Masterid"] = ds.Tables[0].Rows[i]["Masterid"].ToString();
                    string MasterId = ds.Tables[0].Rows[i]["Masterid"].ToString();
                    string LotNo = ds.Tables[0].Rows[i]["CompanyFullLotNo"].ToString();
                    string CompleteStitch = ds.Tables[0].Rows[i]["CompleteStitching"].ToString();
                    #endregion


                    #region
                    if (CompleteStitch == "Yes")
                    {
                        DataSet dsmasterstk = objbs.CheckNottakennew(MasterId);
                        if (dsmasterstk.Tables[0].Rows.Count > 0)
                        {
                            dr["UnUse"] = dsmasterstk.Tables[0].Rows[0]["UnUseQty"].ToString();
                            dr["Use"] = dsmasterstk.Tables[0].Rows[0]["ProcQty"].ToString();
                        }
                    }
                    else
                    {
                        DataSet dsmasterstk = objbs.CheckNottaken(MasterId);
                        if (dsmasterstk.Tables[0].Rows.Count > 0)
                        {
                            dr["UnUse"] = dsmasterstk.Tables[0].Rows[0]["UnUseQty"].ToString();
                            dr["Use"] = dsmasterstk.Tables[0].Rows[0]["ProcQty"].ToString();
                        }
                    }

                    int Godown = 0;
                    DataSet dsgodown = objbs.Getgodown(LotNo);
                    if (dsgodown.Tables[0].Rows.Count > 0)
                    {
                        dr["Godown"] = dsgodown.Tables[0].Rows[0]["Godown"].ToString();
                        Godown = Convert.ToInt32(dsgodown.Tables[0].Rows[0]["Godown"].ToString());
                    }
                    else
                    {
                        dr["Godown"] = "0";
                        Godown = 0;
                    }

                    int Despatch = 0;
                    DataSet dsdespatch = objbs.getDespatch(LotNo);
                    if (dsdespatch.Tables[0].Rows.Count > 0)
                    {
                        dr["Despatch"] = dsdespatch.Tables[0].Rows[0]["DespatchtQty"].ToString();
                        Despatch = Convert.ToInt32(dsdespatch.Tables[0].Rows[0]["DespatchtQty"].ToString());
                    }
                    else
                    {
                        dr["Despatch"] = "0";
                        Despatch = 0;
                    }


                    int Return = 0;
                    DataSet dsReturn = objbs.getReturn(LotNo);
                    if (dsReturn.Tables[0].Rows.Count > 0)
                    {
                        dr["Return"] = dsReturn.Tables[0].Rows[0]["DespatchReturn"].ToString();
                        Return = Convert.ToInt32(dsReturn.Tables[0].Rows[0]["DespatchReturn"].ToString());
                    }
                    else
                    {
                        dr["Return"] = "0";
                        Return = 0;
                    }

                    dr["Aqty"] = (Godown + (Despatch - Return));

                    #endregion

                    dss.Tables[0].Rows.Add(dr);
                }
                if (dss.Tables[0].Rows.Count > 0)
                {
                    gvlotprocess.DataSource = dss;
                    gvlotprocess.DataBind();
                }
                else
                {
                    gvlotprocess.DataSource = null;
                    gvlotprocess.DataBind();
                }
            }
            else
            {
                gvlotprocess.DataSource = null;
                gvlotprocess.DataBind();
            }

        }

        protected void gvexamParticipants_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double AQty = 0;
                AQty = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Godown"));
                if (AQty > 0)
                {
                    e.Row.Cells[7].BorderColor = System.Drawing.Color.Black;
                    e.Row.BackColor = System.Drawing.Color.Green;
                   
                }

                GridView gv = e.Row.FindControl("gvdetails") as GridView;
                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    string MasterId = Convert.ToString(gvGroup.DataKeys[e.Row.RowIndex].Value);


                    #region
                    DataSet dss = new DataSet();
                    DataTable dt = new DataTable();

                    DataColumn dc = new DataColumn("Process");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("Name");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("Iss");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("Rec");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("Dam");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("Pen");
                    dt.Columns.Add(dc);
                    dc = new DataColumn("Alt");
                    dt.Columns.Add(dc);
                    dss.Tables.Add(dt);

                    #endregion

                    #region
                    #region
                    DataSet dsStc = objbs.Getlotvalues(drpbranch.SelectedValue, "tblJpStiching", MasterId);
                    if (dsStc.Tables[0].Rows.Count > 0)
                    {
                        string N = ""; int I = 0; int R = 0; int D = 0; int P = 0;

                        for (int f = 0; f < dsStc.Tables[0].Rows.Count; f++)
                        {
                            if (f == 0)
                            {
                                N = dsStc.Tables[0].Rows[f]["LedgerName"].ToString();
                                I += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Pen"].ToString());
                            }
                            else
                            {
                                //string V = "," + "<br />";
                                string V = ",";
                                string NN = dsStc.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }

                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Stc";
                        dr["Name"] = N;
                        dr["Iss"] = I;
                        dr["Rec"] = R;
                        dr["Dam"] = D;
                        dr["Pen"] = P;
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Stc";
                        dr["Name"] = "-";
                        dr["Iss"] = "0";
                        dr["Rec"] = "0";
                        dr["Dam"] = "0";
                        dr["Pen"] = "0";
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    #endregion

                    #region
                    DataSet dsEmb = objbs.Getlotvalues(drpbranch.SelectedValue, "tblJpEmbroiding", MasterId);
                    if (dsEmb.Tables[0].Rows.Count > 0)
                    {

                        string N = ""; int I = 0; int R = 0; int D = 0; int P = 0;

                        for (int f = 0; f < dsEmb.Tables[0].Rows.Count; f++)
                        {
                            if (f == 0)
                            {
                                N = dsEmb.Tables[0].Rows[f]["LedgerName"].ToString();
                                I += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Pen"].ToString());
                            }
                            else
                            {
                                //string V = "," + "<br />";
                                string V = ",";
                                string NN = dsEmb.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }

                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Emb";
                        dr["Name"] = N;
                        dr["Iss"] = I;
                        dr["Rec"] = R;
                        dr["Dam"] = D;
                        dr["Pen"] = P;
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Emb";
                        dr["Name"] = "-";
                        dr["Iss"] = "0";
                        dr["Rec"] = "0";
                        dr["Dam"] = "0";
                        dr["Pen"] = "0";
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    #endregion

                    #region
                    DataSet dsKaja = objbs.Getlotvalues(drpbranch.SelectedValue, "tblJpKajaButton", MasterId);
                    if (dsKaja.Tables[0].Rows.Count > 0)
                    {
                        string N = ""; int I = 0; int R = 0; int D = 0; int P = 0;

                        for (int f = 0; f < dsKaja.Tables[0].Rows.Count; f++)
                        {
                            if (f == 0)
                            {
                                N = dsKaja.Tables[0].Rows[f]["LedgerName"].ToString();
                                I += Convert.ToInt32(dsKaja.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsKaja.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsKaja.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsKaja.Tables[0].Rows[f]["Pen"].ToString());
                            }
                            else
                            {
                                //string V = "," + "<br />";
                                string V = ",";
                                string NN = dsKaja.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsKaja.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsKaja.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsKaja.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsKaja.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }

                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Kaja";
                        dr["Name"] = N;
                        dr["Iss"] = I;
                        dr["Rec"] = R;
                        dr["Dam"] = D;
                        dr["Pen"] = P;
                        dr["Alt"] = "";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Kaja";
                        dr["Name"] = "-";
                        dr["Iss"] = "0";
                        dr["Rec"] = "0";
                        dr["Dam"] = "0";
                        dr["Pen"] = "0";
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    #endregion

                    #region
                    DataSet dsWash = objbs.Getlotvalues(drpbranch.SelectedValue, "tblJpWashing", MasterId);
                    if (dsWash.Tables[0].Rows.Count > 0)
                    {

                        string N = ""; int I = 0; int R = 0; int D = 0; int P = 0;

                        for (int f = 0; f < dsWash.Tables[0].Rows.Count; f++)
                        {
                            if (f == 0)
                            {
                                N = dsWash.Tables[0].Rows[f]["LedgerName"].ToString();
                                I += Convert.ToInt32(dsWash.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsWash.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsWash.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsWash.Tables[0].Rows[f]["Pen"].ToString());
                            }
                            else
                            {
                                //string V = "," + "<br />";
                                string V = ",";
                                string NN = dsWash.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsWash.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsWash.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsWash.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsWash.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }

                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Wash";
                        dr["Name"] = N;
                        dr["Iss"] = I;
                        dr["Rec"] = R;
                        dr["Dam"] = D;
                        dr["Pen"] = P;
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Wash";
                        dr["Name"] = "-";
                        dr["Iss"] = "0";
                        dr["Rec"] = "0";
                        dr["Dam"] = "0";
                        dr["Pen"] = "0";
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    #endregion

                    #region
                    DataSet dsPrint = objbs.Getlotvalues(drpbranch.SelectedValue, "tblJpPrinting", MasterId);
                    if (dsPrint.Tables[0].Rows.Count > 0)
                    {
                        string N = ""; int I = 0; int R = 0; int D = 0; int P = 0;

                        for (int f = 0; f < dsPrint.Tables[0].Rows.Count; f++)
                        {
                            if (f == 0)
                            {
                                N = dsPrint.Tables[0].Rows[f]["LedgerName"].ToString();
                                I += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Pen"].ToString());
                            }
                            else
                            {
                                //string V = "," + "<br />";
                                string V = ",";
                                string NN = dsPrint.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }

                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Print";
                        dr["Name"] = N;
                        dr["Iss"] = I;
                        dr["Rec"] = R;
                        dr["Dam"] = D;
                        dr["Pen"] = P;
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Print";
                        dr["Name"] = "-";
                        dr["Iss"] = "0";
                        dr["Rec"] = "0";
                        dr["Dam"] = "0";
                        dr["Pen"] = "0";
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    #endregion

                    #region
                    DataSet dsBtag = objbs.Getlotvalues(drpbranch.SelectedValue, "tblJpBarTag", MasterId);
                    if (dsBtag.Tables[0].Rows.Count > 0)
                    {
                        string N = ""; int I = 0; int R = 0; int D = 0; int P = 0;

                        for (int f = 0; f < dsBtag.Tables[0].Rows.Count; f++)
                        {
                            if (f == 0)
                            {
                                N = dsBtag.Tables[0].Rows[f]["LedgerName"].ToString();
                                I += Convert.ToInt32(dsBtag.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsBtag.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsBtag.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsBtag.Tables[0].Rows[f]["Pen"].ToString());
                            }
                            else
                            {
                                //string V = "," + "<br />";
                                string V = ",";
                                string NN = dsBtag.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsBtag.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsBtag.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsBtag.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsBtag.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }
                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Btag";
                        dr["Name"] = N;
                        dr["Iss"] = I;
                        dr["Rec"] = R;
                        dr["Dam"] = D;
                        dr["Pen"] = P;
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Btag";
                        dr["Name"] = "-";
                        dr["Iss"] = "0";
                        dr["Rec"] = "0";
                        dr["Dam"] = "0";
                        dr["Pen"] = "0";
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    #endregion

                    #region
                    DataSet dsTrm = objbs.Getlotvalues(drpbranch.SelectedValue, "tblJpTrimming", MasterId);
                    if (dsTrm.Tables[0].Rows.Count > 0)
                    {
                        string N = ""; int I = 0; int R = 0; int D = 0; int P = 0;

                        for (int f = 0; f < dsTrm.Tables[0].Rows.Count; f++)
                        {
                            if (f == 0)
                            {
                                N = dsTrm.Tables[0].Rows[f]["LedgerName"].ToString();
                                I += Convert.ToInt32(dsTrm.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsTrm.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsTrm.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsTrm.Tables[0].Rows[f]["Pen"].ToString());
                            }
                            else
                            {
                                //string V = "," + "<br />";
                                string V = ",";
                                string NN = dsTrm.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsTrm.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsTrm.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsTrm.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsTrm.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }

                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Trm";
                        dr["Name"] = N;
                        dr["Iss"] = I;
                        dr["Rec"] = R;
                        dr["Dam"] = D;
                        dr["Pen"] = P;
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Trm";
                        dr["Name"] = "-";
                        dr["Iss"] = "0";
                        dr["Rec"] = "0";
                        dr["Dam"] = "0";
                        dr["Pen"] = "0";
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    #endregion

                    #region
                    DataSet dsCni = objbs.Getlotvalues(drpbranch.SelectedValue, "tblJpConsai", MasterId);
                    if (dsCni.Tables[0].Rows.Count > 0)
                    {
                        string N = ""; int I = 0; int R = 0; int D = 0; int P = 0;

                        for (int f = 0; f < dsCni.Tables[0].Rows.Count; f++)
                        {
                            if (f == 0)
                            {
                                N = dsCni.Tables[0].Rows[f]["LedgerName"].ToString();
                                I += Convert.ToInt32(dsCni.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsCni.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsCni.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsCni.Tables[0].Rows[f]["Pen"].ToString());
                            }
                            else
                            {
                                //string V = "," + "<br />";
                                string V = ",";
                                string NN = dsCni.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsCni.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsCni.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsCni.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsCni.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }
                        DataRow dr = dt.NewRow();

                        dr["Process"] = "Cni";
                        dr["Name"] = N;
                        dr["Iss"] = I;
                        dr["Rec"] = R;
                        dr["Dam"] = D;
                        dr["Pen"] = P;
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();

                        dr["Process"] = "Cni";
                        dr["Name"] = "-";
                        dr["Iss"] = "0";
                        dr["Rec"] = "0";
                        dr["Dam"] = "0";
                        dr["Pen"] = "0";
                        dr["Alt"] = "-";
                        dss.Tables[0].Rows.Add(dr);
                    }
                    #endregion

                    #region
                    DataSet dsIron = objbs.Getlotvalues(drpbranch.SelectedValue, "tblJpIroning", MasterId);
                    if (dsIron.Tables[0].Rows.Count > 0)
                    {
                        string N = ""; int I = 0; int R = 0; int D = 0; int P = 0; int A = 0;
                        for (int f = 0; f < dsIron.Tables[0].Rows.Count; f++)
                        {
                            if (f == 0)
                            {

                                N = dsIron.Tables[0].Rows[0]["LedgerName"].ToString();
                                I += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Pen"].ToString());
                                A += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Alt"].ToString());

                            }
                            else
                            {
                                //string V = "," + "<br />";
                                string V = ",";
                                string NN = dsIron.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;


                                I += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Pen"].ToString());
                                A += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Alt"].ToString());
                            }
                        }

                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Iron";
                        dr["Name"] = N;
                        dr["Iss"] = I;
                        dr["Rec"] = R;
                        dr["Dam"] = D;
                        // dr["Pen"] = P;
                        dr["Pen"] = P - A;
                        dr["Alt"] = A;

                        dss.Tables[0].Rows.Add(dr);


                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["Process"] = "Iron";
                        dr["Name"] = "-";
                        dr["Iss"] = "0";
                        dr["Rec"] = "0";
                        dr["Dam"] = "0";
                        dr["Pen"] = "0";
                        dr["Alt"] = "0";

                        dss.Tables[0].Rows.Add(dr);

                    }
                    #endregion

                    #endregion

                    if (dss.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = dss;
                        gv.DataBind();
                    }
                    else
                    {
                        gv.DataSource = null;
                        gv.DataBind();
                    }

                }


            }

        }
    }
}


