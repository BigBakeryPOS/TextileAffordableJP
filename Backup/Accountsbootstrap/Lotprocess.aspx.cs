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
    public partial class Lotprocess : System.Web.UI.Page
    {

        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        string empid = "";
        double QTIssue = 0; double QTReceive = 0; double QTDamage = 0;
        double FAmount = 0; double FDebitAmount = 0; double FAdvance = 0;

        double Pendingqty = 0;

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

        protected void btnsearch_OnClick1(object sender, EventArgs e)
        {
            DateTime fromDate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ds = objbs.Getlot(drpbranch.SelectedValue, fromDate, toDate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region

                DataSet dss = new DataSet();
                DataTable dt = new DataTable();
                DataColumn dc = new DataColumn("LotNo");
                dt.Columns.Add(dc);

                dc = new DataColumn("LotValue");
                dt.Columns.Add(dc);
                dc = new DataColumn("Date");
                dt.Columns.Add(dc);

                #region
                dc = new DataColumn("StcN");
                dt.Columns.Add(dc);
                dc = new DataColumn("StcI");
                dt.Columns.Add(dc);
                dc = new DataColumn("StcR");
                dt.Columns.Add(dc);
                dc = new DataColumn("StcD");
                dt.Columns.Add(dc);
                dc = new DataColumn("StcP");
                dt.Columns.Add(dc);


                dc = new DataColumn("EmbN");
                dt.Columns.Add(dc);
                dc = new DataColumn("EmbI");
                dt.Columns.Add(dc);
                dc = new DataColumn("EmbR");
                dt.Columns.Add(dc);
                dc = new DataColumn("EmbD");
                dt.Columns.Add(dc);
                dc = new DataColumn("EmbP");
                dt.Columns.Add(dc);

                dc = new DataColumn("KajaN");
                dt.Columns.Add(dc);
                dc = new DataColumn("KajaI");
                dt.Columns.Add(dc);
                dc = new DataColumn("KajaR");
                dt.Columns.Add(dc);
                dc = new DataColumn("KajaD");
                dt.Columns.Add(dc);
                dc = new DataColumn("KajaP");
                dt.Columns.Add(dc);

                dc = new DataColumn("WashN");
                dt.Columns.Add(dc);
                dc = new DataColumn("WashI");
                dt.Columns.Add(dc);
                dc = new DataColumn("WashR");
                dt.Columns.Add(dc);
                dc = new DataColumn("WashD");
                dt.Columns.Add(dc);
                dc = new DataColumn("WashP");
                dt.Columns.Add(dc);

                dc = new DataColumn("PrintN");
                dt.Columns.Add(dc);
                dc = new DataColumn("PrintI");
                dt.Columns.Add(dc);
                dc = new DataColumn("PrintR");
                dt.Columns.Add(dc);
                dc = new DataColumn("PrintD");
                dt.Columns.Add(dc);
                dc = new DataColumn("PrintP");
                dt.Columns.Add(dc);

                dc = new DataColumn("BtagN");
                dt.Columns.Add(dc);
                dc = new DataColumn("BtagI");
                dt.Columns.Add(dc);
                dc = new DataColumn("BtagR");
                dt.Columns.Add(dc);
                dc = new DataColumn("BtagD");
                dt.Columns.Add(dc);
                dc = new DataColumn("BtagP");
                dt.Columns.Add(dc);

                dc = new DataColumn("TrmN");
                dt.Columns.Add(dc);
                dc = new DataColumn("TrmI");
                dt.Columns.Add(dc);
                dc = new DataColumn("TrmR");
                dt.Columns.Add(dc);
                dc = new DataColumn("TrmD");
                dt.Columns.Add(dc);
                dc = new DataColumn("TrmP");
                dt.Columns.Add(dc);


                dc = new DataColumn("CniN");
                dt.Columns.Add(dc);
                dc = new DataColumn("CniI");
                dt.Columns.Add(dc);
                dc = new DataColumn("CniR");
                dt.Columns.Add(dc);
                dc = new DataColumn("CniD");
                dt.Columns.Add(dc);
                dc = new DataColumn("CniP");
                dt.Columns.Add(dc);

                dc = new DataColumn("IronN");
                dt.Columns.Add(dc);
                dc = new DataColumn("IronI");
                dt.Columns.Add(dc);
                dc = new DataColumn("IronR");
                dt.Columns.Add(dc);
                dc = new DataColumn("IronD");
                dt.Columns.Add(dc);
                dc = new DataColumn("IronP");
                dt.Columns.Add(dc);
                dc = new DataColumn("IronA");
                dt.Columns.Add(dc);

                #endregion

                dss.Tables.Add(dt);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {



                    DataRow dr = dt.NewRow();
                    dr["LotNo"] = ds.Tables[0].Rows[i]["CompanyLotNo"].ToString();
                    string MasterId = ds.Tables[0].Rows[i]["Masterid"].ToString();

                    dr["LotValue"] = ds.Tables[0].Rows[i]["Qty"].ToString();
                    dr["Date"] = Convert.ToDateTime(ds.Tables[0].Rows[i]["Date"]).ToString("dd/MM/yyyy");

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
                                string V = "," + "<br />";
                                string NN = dsStc.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsStc.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }

                        dr["StcN"] = N;
                        dr["StcI"] = I;
                        dr["StcR"] = R;
                        dr["StcD"] = D;
                        dr["StcP"] = P;

                    }
                    else
                    {
                        dr["StcN"] = "-";
                        dr["StcI"] = "0";
                        dr["StcR"] = "0";
                        dr["StcD"] = "0";
                        dr["StcP"] = "0";
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
                                string V = "," + "<br />";
                                string NN = dsEmb.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsEmb.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }


                        dr["EmbN"] = N;
                        dr["EmbI"] = I;
                        dr["EmbR"] = R;
                        dr["EmbD"] = D;
                        dr["EmbP"] = P;
                    }
                    else
                    {
                        dr["EmbN"] = "-";
                        dr["EmbI"] = "0";
                        dr["EmbR"] = "0";
                        dr["EmbD"] = "0";
                        dr["EmbP"] = "0";
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
                                string V = "," + "<br />";
                                string NN = dsPrint.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;

                                I += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsPrint.Tables[0].Rows[f]["Pen"].ToString());
                            }

                        }

                        dr["PrintN"] = N;
                        dr["PrintI"] = I;
                        dr["PrintR"] = R;
                        dr["PrintD"] = D;
                        dr["PrintP"] = P;
                    }
                    else
                    {
                        dr["PrintN"] = "-";
                        dr["PrintI"] = "0";
                        dr["PrintR"] = "0";
                        dr["PrintD"] = "0";
                        dr["PrintP"] = "0";
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
                                string V = "," + "<br />";
                                string NN = dsIron.Tables[0].Rows[f]["LedgerName"].ToString();
                                N = N + V + NN;


                                I += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Iss"].ToString());
                                R += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Rec"].ToString());
                                D += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Dam"].ToString());
                                P += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Pen"].ToString());
                                A += Convert.ToInt32(dsIron.Tables[0].Rows[f]["Alt"].ToString());
                            }
                        }

                        dr["IronN"] = N;
                        dr["IronI"] = I;
                        dr["IronR"] = R;
                        dr["IronD"] = D;
                        dr["IronP"] = P;
                        dr["IronA"] = A;


                    }
                    else
                    {
                        dr["IronN"] = "-";
                        dr["IronI"] = "0";
                        dr["IronR"] = "0";
                        dr["IronD"] = "0";
                        dr["IronP"] = "0";
                        dr["IronA"] = "0";

                    }
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


                #endregion
            }
            else
            {
                gvlotprocess.DataSource = null;
                gvlotprocess.DataBind();
            }
        }

        protected void btnsearch_OnClick(object sender, EventArgs e)
        {
            DateTime fromDate = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime toDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet ds = objbs.Getlot(drpbranch.SelectedValue, fromDate, toDate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvlotprocess.DataSource = ds;
                gvlotprocess.DataBind();
            }
            else
            {
                gvlotprocess.DataSource = null;
                gvlotprocess.DataBind();
            }
        }


        public void gvlotprocess_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                GridView gv1 = e.Row.FindControl("gvLiaLedger1") as GridView;
                GridView gv2 = e.Row.FindControl("gvLiaLedger2") as GridView;
                GridView gv3 = e.Row.FindControl("gvLiaLedger3") as GridView;
                GridView gv4 = e.Row.FindControl("gvLiaLedger4") as GridView;
                GridView gv5 = e.Row.FindControl("gvLiaLedger5") as GridView;

                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    string Lotno = gvGroup.DataKeys[e.Row.RowIndex].Value.ToString();

                    DataSet dsstit1 = objbs.processbasedetails("tbljpstiching", "tbltransjpstichinghistory", "stichingid", Lotno);
                    if (dsstit1.Tables[0].Rows.Count > 0)
                    {
                        gv1.DataSource = dsstit1;
                        gv1.DataBind();
                    }

                    DataSet dsstit2 = objbs.processbasedetails("tblJpEmbroiding", "tbltransjpEmbroidinghistory", "Embroidingid", Lotno);
                    if (dsstit2.Tables[0].Rows.Count > 0)
                    {
                        gv2.DataSource = dsstit2;
                        gv2.DataBind();
                    }

                    DataSet dsstit3 = objbs.processbasedetails("tblJpPrinting", "tbltransjpPrintinghistory", "Printingid", Lotno);
                    if (dsstit3.Tables[0].Rows.Count > 0)
                    {
                        gv3.DataSource = dsstit3;
                        gv3.DataBind();
                    }

                    DataSet dsstit4 = objbs.processbasedetailsiron("tbljpironing", "tbltransjpironinghistory", "ironingid", Lotno);
                    if (dsstit4.Tables[0].Rows.Count > 0)
                    {
                        gv4.DataSource = dsstit4;
                        gv4.DataBind();
                    }


                    DataSet dsstit5 = objbs.paydetails(Lotno);
                    if (dsstit5.Tables[0].Rows.Count > 0)
                    {
                      
                        double Amount = 0; double DebitAmount = 0; double Advance = 0;
                        int Count = dsstit5.Tables[0].Rows.Count-1;

                        DataSet temp = new DataSet();
                        DataTable dtt = new DataTable();

                        dtt.Columns.Add(new DataColumn("PaymentNo", typeof(string)));
                        dtt.Columns.Add(new DataColumn("PaymentDate", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Amount", typeof(string)));
                        dtt.Columns.Add(new DataColumn("DebitAmount", typeof(string)));
                        dtt.Columns.Add(new DataColumn("Advance", typeof(string)));
                        dtt.Columns.Add(new DataColumn("ProcessType", typeof(string)));
                        dtt.Columns.Add(new DataColumn("JobWork", typeof(string)));
                        temp.Tables.Add(dtt);


                        for (int i = 0; i < dsstit5.Tables[0].Rows.Count; i++)
                        {
                            if (dsstit5.Tables[0].Rows[i]["Processid"].ToString() == dsstit5.Tables[0].Rows[Count]["Processid"].ToString() && i == Count)
                            {
                                DataRow dr = dtt.NewRow();
                                dr["PaymentNo"] = dsstit5.Tables[0].Rows[i]["PaymentNo"].ToString();
                                dr["PaymentDate"] = Convert.ToDateTime(dsstit5.Tables[0].Rows[i]["PaymentDate"]).ToString("dd/MM/yyyy");
                                dr["Amount"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Amount"]).ToString("f2");
                                dr["DebitAmount"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["DebitAmount"]).ToString("f2");
                                dr["Advance"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Advance"]).ToString("f2");
                                dr["ProcessType"] = dsstit5.Tables[0].Rows[i]["ProcessType"].ToString();
                                dr["JobWork"] = dsstit5.Tables[0].Rows[i]["JobWork"].ToString();

                                Amount += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Amount"].ToString());
                                DebitAmount += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["DebitAmount"].ToString());
                                Advance += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Advance"].ToString());

                                temp.Tables[0].Rows.Add(dr);


                                DataRow dr1 = dtt.NewRow();
                                dr1["PaymentNo"] = "";
                                dr1["PaymentDate"] = "";
                                dr1["Amount"] = Amount.ToString("f2");
                                dr1["DebitAmount"] = DebitAmount.ToString("f2");
                                dr1["Advance"] = Advance.ToString("f2");
                                dr1["ProcessType"] = "Process Ttl:-";
                                dr1["JobWork"] = "";
                                temp.Tables[0].Rows.Add(dr1);

                                FAmount += Amount;
                                FDebitAmount += DebitAmount;
                                FAdvance += Advance;

                                Amount = 0; DebitAmount = 0; Advance = 0;
                            }
                            else if (dsstit5.Tables[0].Rows[i]["Processid"].ToString() == dsstit5.Tables[0].Rows[i + 1]["Processid"].ToString())
                            {
                                DataRow dr = dtt.NewRow();
                                dr["PaymentNo"] = dsstit5.Tables[0].Rows[i]["PaymentNo"].ToString();
                                dr["PaymentDate"] = Convert.ToDateTime(dsstit5.Tables[0].Rows[i]["PaymentDate"]).ToString("dd/MM/yyyy");
                                dr["Amount"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Amount"]).ToString("f2");
                                dr["DebitAmount"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["DebitAmount"]).ToString("f2");
                                dr["Advance"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Advance"]).ToString("f2");
                                dr["ProcessType"] = dsstit5.Tables[0].Rows[i]["ProcessType"].ToString();
                                dr["JobWork"] = dsstit5.Tables[0].Rows[i]["JobWork"].ToString();

                                Amount += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Amount"].ToString());
                                DebitAmount += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["DebitAmount"].ToString());
                                Advance += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Advance"].ToString());

                                temp.Tables[0].Rows.Add(dr);
                            }
                            else if ((Amount + DebitAmount + Advance) == 0)
                            {
                                DataRow dr = dtt.NewRow();
                                dr["PaymentNo"] = dsstit5.Tables[0].Rows[i]["PaymentNo"].ToString();
                                dr["PaymentDate"] = Convert.ToDateTime(dsstit5.Tables[0].Rows[i]["PaymentDate"]).ToString("dd/MM/yyyy");
                                dr["Amount"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Amount"]).ToString("f2");
                                dr["DebitAmount"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["DebitAmount"]).ToString("f2");
                                dr["Advance"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Advance"]).ToString("f2");
                                dr["ProcessType"] = dsstit5.Tables[0].Rows[i]["ProcessType"].ToString();
                                dr["JobWork"] = dsstit5.Tables[0].Rows[i]["JobWork"].ToString();

                                Amount += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Amount"].ToString());
                                DebitAmount += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["DebitAmount"].ToString());
                                Advance += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Advance"].ToString());

                                temp.Tables[0].Rows.Add(dr);


                                DataRow dr1 = dtt.NewRow();
                                dr1["PaymentNo"] = "";
                                dr1["PaymentDate"] = "";
                                dr1["Amount"] = Amount.ToString("f2");
                                dr1["DebitAmount"] = DebitAmount.ToString("f2");
                                dr1["Advance"] = Advance.ToString("f2");
                                dr1["ProcessType"] = "Process Ttl:-";
                                dr1["JobWork"] = "";

                                temp.Tables[0].Rows.Add(dr1);

                                FAmount += Amount;
                                FDebitAmount += DebitAmount;
                                FAdvance += Advance;

                                Amount = 0; DebitAmount = 0; Advance = 0;
                            }
                            else if ((Amount + DebitAmount + Advance) != 0)
                            {
                                DataRow dr = dtt.NewRow();
                                dr["PaymentNo"] = dsstit5.Tables[0].Rows[i]["PaymentNo"].ToString();
                                dr["PaymentDate"] = Convert.ToDateTime(dsstit5.Tables[0].Rows[i]["PaymentDate"]).ToString("dd/MM/yyyy");
                                dr["Amount"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Amount"]).ToString("f2");
                                dr["DebitAmount"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["DebitAmount"]).ToString("f2");
                                dr["Advance"] = Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Advance"]).ToString("f2");
                                dr["ProcessType"] = dsstit5.Tables[0].Rows[i]["ProcessType"].ToString();
                                dr["JobWork"] = dsstit5.Tables[0].Rows[i]["JobWork"].ToString();

                                Amount += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Amount"].ToString());
                                DebitAmount += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["DebitAmount"].ToString());
                                Advance += Convert.ToDouble(dsstit5.Tables[0].Rows[i]["Advance"].ToString());

                                temp.Tables[0].Rows.Add(dr);


                                DataRow dr1 = dtt.NewRow();
                                dr1["PaymentNo"] = "";
                                dr1["PaymentDate"] = "";
                                dr1["Amount"] = Amount.ToString("f2");
                                dr1["DebitAmount"] = DebitAmount.ToString("f2");
                                dr1["Advance"] = Advance.ToString("f2");
                                dr1["ProcessType"] = "Process Ttl:-";
                                dr1["JobWork"] = "";

                                temp.Tables[0].Rows.Add(dr1);

                                FAmount += Amount;
                                FDebitAmount += DebitAmount;
                                FAdvance += Advance;

                                Amount = 0; DebitAmount = 0; Advance = 0;
                            }
                            else
                            {
                                DataRow dr = dtt.NewRow();
                                dr["PaymentNo"] = "";
                                dr["PaymentDate"] = "";
                                dr["Amount"] = Amount.ToString("f2");
                                dr["DebitAmount"] = DebitAmount.ToString("f2");
                                dr["Advance"] = Advance.ToString("f2");
                                dr["ProcessType"] = "Process Ttl:-";
                                dr["JobWork"] = "";

                                temp.Tables[0].Rows.Add(dr);

                                FAmount += Amount;
                                FDebitAmount += DebitAmount;
                                FAdvance += Advance;

                                Amount = 0; DebitAmount = 0; Advance = 0;
                            }




                        }

                        DataRow drf = dtt.NewRow();
                        drf["PaymentNo"] = "";
                        drf["PaymentDate"] = "";
                        drf["Amount"] = FAmount.ToString("f2");
                        drf["DebitAmount"] = FDebitAmount.ToString("f2");
                        drf["Advance"] = FAdvance.ToString("f2");
                        drf["ProcessType"] = "Total:-";
                        drf["JobWork"] = "";
                        temp.Tables[0].Rows.Add(drf);

                        gv5.DataSource = temp;
                        gv5.DataBind();
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.Cells[3].Text = "Amt:-" + FAmount.ToString("f2");
                //e.Row.Cells[4].Text = "Dr.Amt:-" + FDebitAmount.ToString("f2");
                //e.Row.Cells[5].Text = "Adv:-" + FAdvance.ToString("f2");
            }
        }


        public void gvLiaLedger_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string QT = DataBinder.Eval(e.Row.DataItem, "Type").ToString();

                if (QT == "Isu")
                {
                    QTIssue += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Qty"));
                }
                else if (QT == "Rec")
                {
                    QTReceive += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Qty"));
                }
                else
                {
                    QTDamage += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Qty"));
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Isu:-" + QTIssue.ToString();
                e.Row.Cells[2].Text = "Rec:-" + QTReceive.ToString();
                e.Row.Cells[3].Text = "Dmg:-" + QTDamage.ToString();

                QTIssue = 0; QTReceive = 0; QTDamage = 0;
            }
        }

        protected void btnexp_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= ProcessDetails.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            div2.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }
    }
}



