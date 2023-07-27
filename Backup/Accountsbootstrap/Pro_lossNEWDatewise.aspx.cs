using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class Pro_lossNEWDatewise : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        public double debitTotal = 0;
        public double creditTotal = 0;
        public double purchasetotal = 0;
        public double salesdeb = 0;
        double inqty = 0;
        string sTableName = "";
        double salesstock = 0;
        double purchasestock1 = 0;
        double salesreturn1 = 0;
        double purchasereturn1 = 0;
        double deliveryin1 = 0;
        double deliveryout1 = 0;
        double stockinward1 = 0;
        double stockoutward1 = 0;
        double drrivedd1 = 0;

        public double debitTotal1 = 0;
        public double creditTotal1 = 0;

        public double debitTotalg = 0;
        public double creditTotalg = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string sTableName = string.Empty;
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                txtfrmdate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                string super = Session["IsSuperAdmin"].ToString();
                //string sTableName = Session["User"].ToString();

                if (super == "1")
                {
                    ddloutlet.Enabled = true;
                    DataSet dsbranchto = objBs.Branchto();
                    ddloutlet.DataSource = dsbranchto.Tables[0];
                    ddloutlet.DataTextField = "branchName";
                    ddloutlet.DataValueField = "branchcode";
                    ddloutlet.DataBind();
                    ddloutlet.Items.Insert(0, "All");
                }
                else
                {
                    DataSet dsbranch = new DataSet();
                    dsbranch = objBs.Branchfrom(sTableName);
                    ddloutlet.DataSource = dsbranch.Tables[0];
                    ddloutlet.DataTextField = "branchName";
                    ddloutlet.DataValueField = "branchcode";
                    ddloutlet.DataBind();
                    ddloutlet.Enabled = false;
                }

                //DataSet dsbranch = objBs.selectBranch();
                //ddloutlet.DataSource = dsbranch.Tables[0];
                //ddloutlet.DataValueField = "Branchcode";
                //ddloutlet.DataTextField = "Branchcode";
                //ddloutlet.DataBind();
                //ddloutlet.Items.Insert(0, "All");
            }

        }
        public DataTable GenerateDs(string GroupName, string strParticulars, string strDebit, string strCredit, string iGroupID)
        {
            lblMessage.Text = " Profit and Loss Report From  '" + txtfrmdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
            //lblMessage.Text = " Profit and Loss Report for  " + ddloutlet.SelectedItem.Text;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            DataColumn dc;
            DataRow dr;

            dc = new DataColumn("Particulars");
            dt.Columns.Add(dc);

            dc = new DataColumn("Debit");
            dt.Columns.Add(dc);

            dc = new DataColumn("Credit");
            dt.Columns.Add(dc);

            dc = new DataColumn("GroupID");
            dt.Columns.Add(dc);

            dc = new DataColumn("GroupName");
            dt.Columns.Add(dc);

            dr = dt.NewRow();
            dr["Particulars"] = strParticulars;

            dr["Debit"] = strDebit;

            dr["Credit"] = strCredit;

            dr["GroupID"] = iGroupID;
            dr["GroupName"] = GroupName;
            dt.Rows.Add(dr);

            return dt;
        }
        public void gBalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //debitTotal = 0;
            //creditTotal = 0;
            //DateTime startDate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime endDate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDebit = (Label)e.Row.FindControl("lblDebit");
                Label lblCredit = (Label)e.Row.FindControl("lblCredit");
                if (lblDebit != null && lblDebit.Text != "")
                    debitTotal = debitTotal + Convert.ToDouble(lblDebit.Text);
                if (lblCredit != null && lblCredit.Text != "")
                    creditTotal = creditTotal + Convert.ToDouble(lblCredit.Text);
            }


            //lblDebitTotal.Text = debitTotal.ToString("f3");
            //lblCreditTotal.Text = creditTotal.ToString("f3");

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //}

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = debitTotal.ToString("f3");
                e.Row.Cells[2].Text = creditTotal.ToString("f3");
                double doub = debitTotal - creditTotal;


                int RowIndex = e.Row.RowIndex;
                int DataItemIndex = e.Row.DataItemIndex;
                int Columnscount = gvLiaLedger.Columns.Count;
                GridViewRow row = new GridViewRow(RowIndex, DataItemIndex, DataControlRowType.Footer, DataControlRowState.Normal);
                for (int i = 0; i < 6; i++)
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    TableCell tablecell = new TableCell();
                    //   tablecell.Text = "dynamic footer" + i;
                    if (i == 0)
                    {
                        tablecell.HorizontalAlign = HorizontalAlign.Center;

                        tablecell.Text = "Gross Profit";
                        tablecell.ForeColor = System.Drawing.Color.Red;
                    }

                    if (debitTotal > creditTotal)
                    {
                        if (i == 1)
                        {

                            tablecell.Text = Convert.ToDouble(doub).ToString("f3");// Convert.ToString(doub);
                            tablecell.ForeColor = System.Drawing.Color.Red;
                            tablecell.HorizontalAlign = HorizontalAlign.Right;
                        }
                    }
                    else
                    {
                        if (i == 2)
                        {

                            tablecell.Text = Convert.ToDouble(-doub).ToString("f3");// Convert.ToString(-doub);
                            tablecell.ForeColor = System.Drawing.Color.Red;
                        }

                    }




                    row.Cells.Add(tablecell);
                }
                this.gvLiaLedger.Controls[0].Controls.Add(row);
                //  e.Row.Cells[2].Text

                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
            }

        }

        public void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //debitTotal = 0;
            //creditTotal = 0;
            //DateTime startDate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime endDate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDebit = (Label)e.Row.FindControl("lblDebit");
                Label lblCredit = (Label)e.Row.FindControl("lblCredit");
                if (lblDebit != null && lblDebit.Text != "")
                    debitTotalg = debitTotalg + Convert.ToDouble(lblDebit.Text);
                if (lblCredit != null && lblCredit.Text != "")
                    creditTotalg = creditTotalg + Convert.ToDouble(lblCredit.Text);
            }



            else if (e.Row.RowType == DataControlRowType.Footer)
            {

            }

        }


        protected void btnreport_Click(object sender, EventArgs e)
        {
            //string Branch = ddloutlet.SelectedValue;
            //DateTime frmdate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime todate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            pnlist();
            pnlist1g();
            pnlist1();
       //     DataSet purchasedeb = objBs.Purchasedebit("tblPurchasenew_" + ddloutlet.SelectedValue, ddloutlet.SelectedValue, "tblTransPurchasenew_" + ddloutlet.SelectedValue);
            DataSet purchasedeb = objBs.Purchasedebit("tblPurchase_" + ddloutlet.SelectedValue, ddloutlet.SelectedValue, "tblTransPurchase_" + ddloutlet.SelectedValue);
            if (purchasedeb.Tables.Count > 0)
            {
                GridView2.DataSource = purchasedeb;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }

         //   DataSet salesdeb = objBs.salesdebit("tblSalesnew_" + ddloutlet.SelectedValue, ddloutlet.SelectedValue, "tblTransSalesnew_" + ddloutlet.SelectedValue);
            DataSet salesdeb = objBs.salesdebit("tblSales_" + ddloutlet.SelectedValue, ddloutlet.SelectedValue, "tblTransSales_" + ddloutlet.SelectedValue);
            if (salesdeb.Tables.Count > 0)
            {
                GridView3.DataSource = salesdeb;
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = null;
                GridView3.DataBind();
            }

            PL();
            idt.Visible = true;
        }
        public void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //debitTotal = 0;
            //creditTotal = 0;
            //DateTime startDate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime endDate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblDebit = (Label)e.Row.FindControl("lblDebit");
                Label lblCredit = (Label)e.Row.FindControl("lblCredit");
                if (lblDebit != null && lblDebit.Text != "")
                    debitTotal1 = debitTotal1 + Convert.ToDouble(lblDebit.Text);
                if (lblCredit != null && lblCredit.Text != "")
                    creditTotal1 = creditTotal1 + Convert.ToDouble(lblCredit.Text);
            }


            //lblDebitTotal.Text = debitTotal.ToString("f3");
            //lblCreditTotal.Text = creditTotal.ToString("f3");

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //}

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[0].Text = "Net Profit";

                double doub = (debitTotal + debitTotal1 + debitTotalg) - (creditTotal + creditTotal1 + creditTotalg);

                if (doub > 0)
                {
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = doub.ToString("f3");
                }
                else
                {
                    e.Row.Cells[1].Text = (-doub).ToString("f3");
                    e.Row.Cells[2].Text = "";
                }



                //e.Row.Cells[3].Text = doub.ToString("f3");
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                //e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;


                double tot;
                double tot1;
                if (doub > 0)
                {
                    tot = (debitTotal1 + debitTotalg) + doub;
                }
                else
                {
                    doub = -doub;
                    tot = (debitTotal1 + debitTotalg) + doub;
                }

                double tt = debitTotal - creditTotal;
                if (tt > 0)
                {
                    tot1 = tt + (creditTotal1 + creditTotalg);
                }
                else
                {
                    tot1 = (-tt) + (creditTotal1 + creditTotalg);
                }


                int RowIndex = e.Row.RowIndex;
                int DataItemIndex = e.Row.DataItemIndex;
                int Columnscount = gvLiaLedger.Columns.Count;
                GridViewRow row = new GridViewRow(RowIndex, DataItemIndex, DataControlRowType.Footer, DataControlRowState.Normal);
                for (int i = 0; i < 6; i++)
                {
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    TableCell tablecell = new TableCell();
                    //   tablecell.Text = "dynamic footer" + i;
                    if (i == 0)
                    {
                        tablecell.HorizontalAlign = HorizontalAlign.Center;

                        tablecell.Text = "Total ";
                        tablecell.ForeColor = System.Drawing.Color.Red;
                    }

                    //  if (debitTotal > creditTotal)
                    {
                        if (i == 1)
                        {

                            tablecell.Text = Convert.ToDouble(tot).ToString("f3"); //Convert.ToString(tot);
                            tablecell.ForeColor = System.Drawing.Color.Red;
                            tablecell.HorizontalAlign = HorizontalAlign.Right;
                        }
                    }
                    //   else
                    {
                        if (i == 2)
                        {

                            tablecell.Text = Convert.ToDouble(tot1).ToString("f3"); //Convert.ToString(tot); Convert.ToString(tot1);
                            tablecell.ForeColor = System.Drawing.Color.Red;
                            tablecell.HorizontalAlign = HorizontalAlign.Right;
                        }

                    }




                    row.Cells.Add(tablecell);
                }
                this.GridView1.Controls[0].Controls.Add(row);
            }

        }
        public void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double lblDebit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "aempty").ToString());

                purchasetotal = purchasetotal + Convert.ToDouble(lblDebit);

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;


                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = purchasetotal.ToString("f3");

                double doub = purchasetotal - creditTotal;

                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
            }
        }
        public void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double lblDebit = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "aempty").ToString());

                salesdeb = salesdeb + Convert.ToDouble(lblDebit);

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;


                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = salesdeb.ToString("f3");

                double doub = salesdeb - creditTotal;

                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;

                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
            }
        }
        //protected void btnreport_Click(object sender, EventArgs e)
        //{
        //    //string Branch = ddloutlet.SelectedValue;
        //    //DateTime frmdate = Convert.ToDateTime(txtfrmdate.Text);
        //    //DateTime todate = Convert.ToDateTime(txttodate.Text);

        //    DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    pnlist();
        //    pnlist1();
        //    //PL();
        //    //idt.Visible = true;
        //}
        public void pnlist()
        {
            DataTable grdDt = new DataTable();
            DataSet grdDs = new DataSet();
            DataTable dtNew = new DataTable();
            int groupID = 0;
            double debitSum = 0.0d;
            double creditSum = 0.0d;
            double totalSum = 0.0d;
            string strParticulars = string.Empty;
            string strDebit = string.Empty;
            string strCredit = string.Empty;
            string sGroupName = string.Empty;
            dtNew = GenerateDs("", "", "", "", "");
            grdDs.Tables.Add(dtNew);

            //DateTime startDate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime endDate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string TrailFlag = string.Empty;

            DataSet mainDs = objBs.GetpnGroups();
            if (mainDs != null)
            {
                if (mainDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mainRow in mainDs.Tables[0].Rows)
                    {
                        groupID = Convert.ToInt32(mainRow["GroupID"]);
                        sGroupName = mainRow["GroupName"].ToString();

                        debitSum = objBs.GetDebitSum(groupID, startDate, endDate, ddloutlet.SelectedValue);
                        creditSum = objBs.GetCreditSum(groupID, startDate, endDate, ddloutlet.SelectedValue);

                        TrailFlag = Convert.ToString(mainRow["TrailBalance"]);
                        strParticulars = Convert.ToString(mainRow["GroupName"]);
                        if (TrailFlag == "Debit")
                        {
                            totalSum = debitSum - creditSum;
                            strDebit = Convert.ToString(totalSum.ToString("f3"));
                            strCredit = "";

                            if (totalSum < 0)
                            {
                                strCredit = Convert.ToString(Math.Abs(totalSum));
                                strDebit = "";
                            }
                        }
                        else
                        {
                            totalSum = creditSum - debitSum;
                            strCredit = Convert.ToString(totalSum.ToString("f3"));
                            strDebit = "";
                            if (totalSum < 0)
                            {
                                strCredit = "";
                                strDebit = Convert.ToString(Math.Abs(totalSum));
                            }
                        }

                        grdDt = GenerateDs(sGroupName, strParticulars, strDebit, strCredit, groupID.ToString());

                        if (grdDt != null)
                        {
                            for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                            {

                                if (grdDt != null && grdDt.Rows.Count > 0)
                                    grdDs.Tables[0].ImportRow(grdDt.Rows[k]);
                            }
                        }
                    }
                }
            }
            grdDs.Tables[0].Rows[0].Delete();
            DataSet dss = new DataSet();
            foreach (DataRow dr in grdDs.Tables[0].Rows)
            {
                GridView gv = FindControl("gvLiaLedger") as GridView;
                String groupid = dr["Groupid"].ToString();
                if (groupid != "" && groupid != "9")
                {


                    DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, ddloutlet.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                        {
                            dss.Merge(ds);
                        }
                    }

                }
                else if (groupid == "9")
                {
                    DataSet dledgername = objBs.selectClosing(ddloutlet.SelectedValue);
                    DataSet ds1 = new DataSet();
                    DataTable dt = new DataTable();

                    DataColumn dc;
                    DataRow dr1;

                    dc = new DataColumn("LedgerName");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("LedgerID");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Folionumber");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Branch");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Debit");
                    dt.Columns.Add(dc);

                    dc = new DataColumn("Credit");
                    dt.Columns.Add(dc);
                    for (int i = 0; i < dledgername.Tables[0].Rows.Count; i++)
                    {
                        dr1 = dt.NewRow();
                        dr1["LedgerName"] = dledgername.Tables[0].Rows[i]["PrintName"].ToString();
                        dr1["LedgerID"] = dledgername.Tables[0].Rows[i]["LedgerName"].ToString();
                        if (dledgername.Tables[0].Rows[i]["ChooseType"].ToString() == "1")
                        {
                            dr1["Debit"] = dledgername.Tables[0].Rows[i]["ClosingValue"].ToString();

                            dr1["Credit"] = "";
                        }
                        else
                        {
                            dr1["Debit"] = "";

                            dr1["Credit"] = dledgername.Tables[0].Rows[i]["ClosingValue"].ToString();
                        }
                        dr1["Folionumber"] = "";
                        dr1["Branch"] = ddloutlet.SelectedValue;
                        dt.Rows.Add(dr1);
                    }
                    ds1.Tables.Add(dt);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {

                        {
                            dss.Merge(ds1);
                        }
                    }
                    //string bnch = string.Empty;
                    //DataSet dledgername = objBs.getledgerforprofitloss(groupid,  ddloutlet.SelectedValue);
                    //if (dledgername.Tables[0].Rows.Count == 0)
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Closing Stock Ledger Not Exists!!!');", true);
                    //    return;
                    //}
                    //double closingtotal = 0;
                    //DataSet allproductt = objBs.getproductt();
                    //if (allproductt.Tables[0].Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < allproductt.Tables[0].Rows.Count; i++)
                    //    {
                    //        string product = allproductt.Tables[0].Rows[i]["categoryuserid"].ToString();
                    //        double openingStock = 0;
                    //        double salestock = 0;
                    //        double purchasestock = 0;
                    //        double purchasereturn = 0;
                    //        double salesreturn = 0;
                    //        double deliveryout = 0;
                    //        double deliveryin = 0;
                    //        double stockinward = 0;
                    //        double stockoutward = 0;
                    //        double overallstock = 0;
                    //        double currentstock = 0;

                    //        openingStock = (Convert.ToDouble(objBs.getOpeningStock(product, ddloutlet.SelectedValue)));
                    //        purchasestock = (Convert.ToDouble(objBs.getOpeningStockPurchase(product, startDate, ddloutlet.SelectedValue)));
                    //        salestock = (Convert.ToDouble(objBs.getOpeningStockSales(product, startDate, ddloutlet.SelectedValue)));
                    //        salesreturn = (Convert.ToDouble(objBs.getOpeningStocksalesreturn(product, startDate, ddloutlet.SelectedValue)));
                    //        purchasereturn = (Convert.ToDouble(objBs.getOpeningStockPurchasereturn(product, startDate, ddloutlet.SelectedValue)));
                    //        deliveryin = (Convert.ToDouble(objBs.getOpeningStockdeliveryin(product, startDate, ddloutlet.SelectedValue)));
                    //        deliveryout = (Convert.ToDouble(objBs.getOpeningStockdeliveryout(product, startDate, ddloutlet.SelectedValue)));
                    //        stockinward = (Convert.ToDouble(objBs.getOpeningStockin(product, startDate, ddloutlet.SelectedValue)));
                    //        stockoutward = (Convert.ToDouble(objBs.getOpeningStockout(product, startDate, ddloutlet.SelectedValue)));
                    //        string openingstockk = Convert.ToString(openingStock);
                    //        currentstock = (Convert.ToDouble(objBs.getcurrentstockpurchase(product, "tblStock_"+ddloutlet.SelectedValue)));
                    //        // lblcurrentstock.Text = Convert.ToString(currentstock);

                    //        overallstock = openingStock + purchasestock - salestock + deliveryin - deliveryout + salesreturn - purchasereturn - stockoutward + stockinward;

                    //        DataSet ds = objBs.generatestockreport(startDate, endDate, ddloutlet.SelectedValue, product, ddloutlet.SelectedValue);
                    //        DataView dds = ds.Tables[0].DefaultView;
                    //        dds.Sort = "TransDate ASC";
                    //      //  gvLedger.DataSource = dds;
                    //        ViewState["Dateset"] = ds;
                    //        if (ds.Tables[0].Rows.Count > 0)
                    //        {
                    //           // string bnch = string.Empty;
                    //            if (ddloutlet.SelectedValue == "CO1")
                    //            {
                    //                bnch = "TSI";
                    //            }
                    //            else if (ddloutlet.SelectedValue == "CO2")
                    //            {
                    //                bnch = "CBE";
                    //            }
                    //            else
                    //            {
                    //                bnch = "MAS";
                    //            }
                    //        }
                    //       string lblpurchase = "";
                    //      string  lblsalereturn = "";
                    //      string  lblsales = "";
                    //       string lblpurchasereturn = "";
                    //       string lbldeliveryin = "";
                    //       string lbldeliveryout = "";
                    //       string lblstockinward = "";
                    //       string lblstockoutward = "";
                    //        inqty = overallstock;

                    //        //gvLedger.DataBind();

                    //        if (lblsalereturn == "")
                    //        {
                    //            lblsalereturn = "0";
                    //        }
                    //        if (lblpurchasereturn == "")
                    //        {
                    //            lblpurchasereturn = "0";
                    //        }
                    //        if (lblpurchase == "")
                    //        {
                    //            lblpurchase = "0";
                    //        }
                    //        if (lblsales == "")
                    //        {
                    //            lblsales = "0";
                    //        }
                    //        if (lblstockinward == "")
                    //        {
                    //            lblstockinward = "0";
                    //        }
                    //        if (lblstockoutward == "")
                    //        {
                    //            lblstockoutward = "0";
                    //        }
                    //        if (lbldeliveryin == "")
                    //        {
                    //            lbldeliveryin = "0";
                    //        }
                    //        if (lbldeliveryout == "")
                    //        {
                    //            lbldeliveryout = "0";
                    //        }

                    //        double drr = (overallstock + purchasestock1 - salesstock - deliveryout1 + deliveryin1 - purchasereturn1 + salesreturn1 - stockoutward1 + stockinward1);



                    //        closingtotal = closingtotal + (drr * currentstock);
                    //        //DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, ddloutlet.SelectedValue);
                    //        //if (ds.Tables[0].Rows.Count > 0)
                    //        //{
                    //        //    if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                    //        //    {
                    //        //        dss.Merge(ds);
                    //        //    }
                    //        //}

                    //    }
                    //    DataSet ds1 = new DataSet();
                    //    DataTable dt = new DataTable();

                    //    DataColumn dc;
                    //    DataRow dr1;

                    //    dc = new DataColumn("LedgerName");
                    //    dt.Columns.Add(dc);

                    //    dc = new DataColumn("LedgerID");
                    //    dt.Columns.Add(dc);

                    //    dc = new DataColumn("Folionumber");
                    //    dt.Columns.Add(dc);

                    //    dc = new DataColumn("Branch");
                    //    dt.Columns.Add(dc);

                    //    dc = new DataColumn("Debit");
                    //    dt.Columns.Add(dc);

                    //    dc = new DataColumn("Credit");
                    //    dt.Columns.Add(dc);

                    //    dr1 = dt.NewRow();
                    //    dr1["LedgerName"] = dledgername.Tables[0].Rows[0]["PrintName"].ToString();
                    //    dr1["LedgerID"] = dledgername.Tables[0].Rows[0]["LedgerId"].ToString();

                    //    dr1["Debit"] = "";

                    //    dr1["Credit"] = closingtotal;

                    //    dr1["Folionumber"] = "";
                    //    dr1["Branch"] = bnch;
                    //    dt.Rows.Add(dr1);
                    //    ds1.Tables.Add(dt);
                    //    if (ds1.Tables[0].Rows.Count > 0)
                    //    {

                    //        {
                    //            dss.Merge(ds1);
                    //        }
                    //    }
                    //}
                }
            }
            if (dss.Tables.Count > 0)
            {
                gvLiaLedger.DataSource = dss;
                gvLiaLedger.DataBind();
            }
            else
            {
                gvLiaLedger.DataSource = null;
                gvLiaLedger.DataBind();
            }
            //gvTrailBalance.DataSource = grdDs;
            // gvTrailBalance.DataBind();

            //idt.Visible = true;
        }
        public void pnlist1()
        {
            DataTable grdDt = new DataTable();
            DataSet grdDs = new DataSet();
            DataTable dtNew = new DataTable();
            int groupID = 0;
            double debitSum = 0.0d;
            double creditSum = 0.0d;
            double totalSum = 0.0d;
            string strParticulars = string.Empty;
            string strDebit = string.Empty;
            string strCredit = string.Empty;
            string sGroupName = string.Empty;
            dtNew = GenerateDs("", "", "", "", "");
            grdDs.Tables.Add(dtNew);

            //DateTime startDate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime endDate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string TrailFlag = string.Empty;

            DataSet mainDs = objBs.GetpnGroups1();
            if (mainDs != null)
            {
                if (mainDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mainRow in mainDs.Tables[0].Rows)
                    {
                        groupID = Convert.ToInt32(mainRow["GroupID"]);
                        sGroupName = mainRow["GroupName"].ToString();

                        debitSum = objBs.GetDebitSum(groupID, startDate, endDate, ddloutlet.SelectedValue);
                        creditSum = objBs.GetCreditSum(groupID, startDate, endDate, ddloutlet.SelectedValue);

                        TrailFlag = Convert.ToString(mainRow["TrailBalance"]);
                        strParticulars = Convert.ToString(mainRow["GroupName"]);
                        if (TrailFlag == "Debit")
                        {
                            totalSum = debitSum - creditSum;
                            strDebit = Convert.ToString(totalSum.ToString("f3"));
                            strCredit = "";

                            if (totalSum < 0)
                            {
                                strCredit = Convert.ToString(Math.Abs(totalSum));
                                strDebit = "";
                            }
                        }
                        else
                        {
                            totalSum = creditSum - debitSum;
                            strCredit = Convert.ToString(totalSum.ToString("f3"));
                            strDebit = "";
                            if (totalSum < 0)
                            {
                                strCredit = "";
                                strDebit = Convert.ToString(Math.Abs(totalSum));
                            }
                        }

                        grdDt = GenerateDs(sGroupName, strParticulars, strDebit, strCredit, groupID.ToString());

                        if (grdDt != null)
                        {
                            for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                            {

                                if (grdDt != null && grdDt.Rows.Count > 0)
                                    grdDs.Tables[0].ImportRow(grdDt.Rows[k]);
                            }
                        }
                    }
                }
            }
            grdDs.Tables[0].Rows[0].Delete();
            DataSet dss = new DataSet();
            foreach (DataRow dr in grdDs.Tables[0].Rows)
            {
                GridView gv = FindControl("gvLiaLedger") as GridView;
                String groupid = dr["Groupid"].ToString();
                if (groupid != "")
                {


                    DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, ddloutlet.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                        {
                            dss.Merge(ds);
                        }
                    }

                }
            }
            if (dss.Tables.Count > 0)
            {
                GridView1.DataSource = dss;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            //gvTrailBalance.DataSource = grdDs;
            // gvTrailBalance.DataBind();

            //idt.Visible = true;
        }

        public void pnlist1g()
        {
            DataTable grdDt = new DataTable();
            DataSet grdDs = new DataSet();
            DataTable dtNew = new DataTable();
            int groupID = 0;
            double debitSum = 0.0d;
            double creditSum = 0.0d;
            double totalSum = 0.0d;
            string strParticulars = string.Empty;
            string strDebit = string.Empty;
            string strCredit = string.Empty;
            string sGroupName = string.Empty;
            dtNew = GenerateDs("", "", "", "", "");
            grdDs.Tables.Add(dtNew);

            //DateTime startDate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime endDate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string TrailFlag = string.Empty;

            DataSet mainDs = objBs.GetpnGroups1g();
            if (mainDs != null)
            {
                if (mainDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mainRow in mainDs.Tables[0].Rows)
                    {
                        groupID = Convert.ToInt32(mainRow["GroupID"]);
                        sGroupName = mainRow["GroupName"].ToString();

                        debitSum = objBs.GetDebitSum(groupID, startDate, endDate, ddloutlet.SelectedValue);
                        creditSum = objBs.GetCreditSum(groupID, startDate, endDate, ddloutlet.SelectedValue);

                        TrailFlag = Convert.ToString(mainRow["TrailBalance"]);
                        strParticulars = Convert.ToString(mainRow["GroupName"]);
                        if (TrailFlag == "Debit")
                        {
                            totalSum = debitSum - creditSum;
                            strDebit = Convert.ToString(totalSum.ToString("f3"));
                            strCredit = "";

                            if (totalSum < 0)
                            {
                                strCredit = Convert.ToString(Math.Abs(totalSum));
                                strDebit = "";
                            }
                        }
                        else
                        {
                            totalSum = creditSum - debitSum;
                            strCredit = Convert.ToString(totalSum.ToString("f3"));
                            strDebit = "";
                            if (totalSum < 0)
                            {
                                strCredit = "";
                                strDebit = Convert.ToString(Math.Abs(totalSum));
                            }
                        }

                        grdDt = GenerateDs(sGroupName, strParticulars, strDebit, strCredit, groupID.ToString());

                        if (grdDt != null)
                        {
                            for (int k = 0; k <= grdDt.Rows.Count - 1; k++)
                            {

                                if (grdDt != null && grdDt.Rows.Count > 0)
                                    grdDs.Tables[0].ImportRow(grdDt.Rows[k]);
                            }
                        }
                    }
                }
            }
            grdDs.Tables[0].Rows[0].Delete();
            DataSet dss = new DataSet();
            foreach (DataRow dr in grdDs.Tables[0].Rows)
            {
                GridView gv = FindControl("gvLiaLedger") as GridView;
                String groupid = dr["Groupid"].ToString();
                if (groupid != "")
                {


                    DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, ddloutlet.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                        {
                            dss.Merge(ds);
                        }
                    }

                }
            }
            if (dss.Tables.Count > 0)
            {
                GridView4.DataSource = dss;
                GridView4.DataBind();
            }
            else
            {
                GridView4.DataSource = null;
                GridView4.DataBind();
            }
            //gvTrailBalance.DataSource = grdDs;
            // gvTrailBalance.DataBind();

            //idt.Visible = true;
        }



        public void PL()
        {
            double openingStockTotal = 0;
            double purchaseTot = 0.0;
            double purchaseReturnTot = 0.0;
            double salesTot = 0.0;
            double salesReturnTot = 0.0;
            double DExpensesTot = 0.0d;
            double IDExpensesTot = 0.0d;
            double closingstockTotal = 0.0;
            double DIncomeTot = 0.0d;
            double IDIncomeTot = 0.0d;
            double dGp = 0.0;
            double dGl = 0.0;
            double gPurchase = 0.0;
            double gSales = 0.0;
            double totExpense = 0.0;
            double totIncome = 0.0;
            double grProfitLoss = 0.0;
            double netProfitLoss = 0.0;

            DataSet purchaseRateDs = GetPurchaseForSales();

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataRow[] SelectDataRow = purchaseRateDs.Tables[0].Select("SalesBillDate>='" + startDate + "' and SalesBillDate<='" + endDate + "'");

            openingStockTotal = objBs.GetPurchaseOpValue(startDate, ddloutlet.SelectedValue);

            double sumRate = 0;
            double sumQty = 0;
            double netRate = 0;
            double netQty = 0;
            double sumNet = 0;
            foreach (DataRow dr in SelectDataRow)
            {
                if (dr["PurchaseRate"] != null)
                {
                    sumRate = Convert.ToDouble(dr["PurchaseRate"]);
                }
                if (dr["SalesQty"] != null)
                {
                    sumQty = Convert.ToDouble(dr["SalesQty"]);
                }
                netRate = sumRate * sumQty;
                sumNet = sumNet + netRate;
            }

            salesReturnTot = objBs.GetSalesReturnBetween(startDate, endDate, ddloutlet.SelectedValue);
            //closingstockTotal = openingStockTotal + (objBs.GetPurchaseBetween(startDate, endDate, ddloutlet.SelectedValue) - sumNet) + salesReturnTot;
            closingstockTotal = (objBs.GetPurchaseBetween(startDate, endDate, ddloutlet.SelectedValue)) + salesReturnTot;

            purchaseTot = objBs.GetPurchaseBetween(startDate, endDate, ddloutlet.SelectedValue);

            purchaseReturnTot = objBs.GetPurchaseReturnBetween(startDate, endDate, ddloutlet.SelectedValue);
            salesTot = objBs.GetSalesBetween(startDate, endDate, ddloutlet.SelectedValue);


            DExpensesTot = objBs.GetExpenseIncomeTotalDateWise(startDate, endDate, "DX", ddloutlet.SelectedValue);
            IDExpensesTot = objBs.GetExpenseIncomeTotalDateWise(startDate, endDate, "IDX", ddloutlet.SelectedValue);
            DIncomeTot = objBs.GetExpenseIncomeTotalDateWise(startDate, endDate, "", ddloutlet.SelectedValue);
            IDIncomeTot = objBs.GetExpenseIncomeTotalDateWise(startDate, endDate, "IDI", ddloutlet.SelectedValue);

            gPurchase = openingStockTotal + (purchaseTot - purchaseReturnTot);
            gSales = closingstockTotal + (salesTot - salesReturnTot);

            totExpense = gPurchase + (DExpensesTot);

            totIncome = gSales + (DIncomeTot);
            grProfitLoss = totIncome - totExpense;


            if (grProfitLoss > 0)
                dGp = totIncome - totExpense;
            else
                dGl = totExpense - totIncome;


            lblGP.Text = dGp.ToString("f3");
            lblGL.Text = dGl.ToString("f3");

            lblGrossProfitBF.Text = dGp.ToString("f3");
            lblGrossLossBF.Text = dGl.ToString("f3");



            if (dGp > 0)
            {
                netProfitLoss = dGp + (IDIncomeTot - IDExpensesTot);
                if (netProfitLoss > 0)
                {
                    lblNetProfit.Text = netProfitLoss.ToString("f3");
                    lblNetLoss.Text = "";
                }
                else
                {
                    lblNetLoss.Text = Math.Abs(netProfitLoss).ToString("f3");
                    lblNetProfit.Text = "";
                }
            }
            else
            {
                netProfitLoss = dGl + (IDExpensesTot - IDIncomeTot);
                if (netProfitLoss > 0)
                    lblNetLoss.Text = Math.Abs(netProfitLoss).ToString("f3");
                else
                    lblNetProfit.Text = netProfitLoss.ToString("f3");
            }

            lblOpeningStock.Text = openingStockTotal.ToString("f3");
            lblClosingStock.Text = closingstockTotal.ToString("f3");

            lblPurchaseTotal.Text = purchaseTot.ToString("f3");
            lblPurchaseReturnTotal.Text = purchaseReturnTot.ToString("f3");
            lblSalesTotal.Text = salesTot.ToString("f3");
            lblSalesReturnTotal.Text = salesReturnTot.ToString("f3");

            //lblDXTotal.Text = (DExpensesTot - salesReturnTot).ToString("f3");
            lblDXTotal.Text = (DExpensesTot).ToString("f3");
            gvIDirectExp.DataSource = objBs.plGetExpenseIncomeSplitDatewise("IDX", ddloutlet.SelectedValue, startDate, endDate);
            gvIDirectExp.DataBind();

            gvDirectExp.DataSource = objBs.plGetExpenseIncomeSplitDatewise("DX", ddloutlet.SelectedValue, startDate, endDate);
            gvDirectExp.DataBind();

            gvDirectInc.DataSource = objBs.plGetExpenseIncomeSplitDatewise(" ", ddloutlet.SelectedValue,startDate, endDate);
            gvDirectInc.DataBind();

            gvIDirectInc.DataSource = objBs.plGetExpenseIncomeSplitDatewise("IDI", ddloutlet.SelectedValue, startDate, endDate);
            gvIDirectInc.DataBind();

            lblIDXExptotal.Text = IDExpensesTot.ToString("f3");

            //lblDIncome.Text = (DIncomeTot - salesReturnTot).ToString("f3");
            lblDIncome.Text = (DIncomeTot).ToString("f3");
            lblIDIncome.Text = IDIncomeTot.ToString("f3");


            lblFirstMidTotal.Text = gPurchase.ToString("f3");
            lblSecondMidTotal.Text = gSales.ToString("f3");

            //lblDbTot.Text = (gPurchase + DExpensesTot + dGp).ToString("f3");
            //lblCrTotal.Text = (gSales + (DIncomeTot - salesReturnTot) + (dGl)).ToString("f3");

            lblDbTot.Text = Convert.ToDouble(Convert.ToDouble(lblFirstMidTotal.Text) + Convert.ToDouble(lblDXTotal.Text) + Convert.ToDouble(lblGP.Text)).ToString("f3");
            lblCrTotal.Text = Convert.ToDouble(Convert.ToDouble(lblSecondMidTotal.Text) + Convert.ToDouble(lblDIncome.Text) + Convert.ToDouble(lblGL.Text)).ToString("f3");
            

            if (lblGrossLossBF.Text == "") lblGrossLossBF.Text = "0";
            if (lblIDXExptotal.Text == "") lblIDXExptotal.Text = "0";
            if (lblNetProfit.Text == "") lblNetProfit.Text = "0";


            if (lblGrossProfitBF.Text == "") lblGrossProfitBF.Text = "0";
            if (lblIDIncome.Text == "") lblIDIncome.Text = "0";
            if (lblNetLoss.Text == "") lblNetLoss.Text = "0";

            lblDbNetTotal.Text = Convert.ToDouble(Convert.ToDouble(lblGrossLossBF.Text) + Convert.ToDouble(lblIDXExptotal.Text) + Convert.ToDouble(lblNetProfit.Text)).ToString("f3");
            lblCrNetTotal.Text = Convert.ToDouble(Convert.ToDouble(lblGrossProfitBF.Text) + Convert.ToDouble(lblIDIncome.Text) + Convert.ToDouble(lblNetLoss.Text)).ToString("f3");
            DataSet ds = new DataSet();

        }
        public DataSet GetPurchaseForSales()
        {
            double salesQty = 0;
            string salesItemCode = string.Empty;
            double salesRate = 0;
            double salesDiscount = 0;
            double salesVat = 0;
            double salesCst = 0;
            double salesLoading = 0;
            double salesFreight = 0;

            string salesExecutive = string.Empty;
            int salesCustomerID = 0;
            string salesCustomer = string.Empty;

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DateTime salesBillDate = new DateTime();

            int purchaseCnt = 0;
            DataRow[] SearchProductRow;
            DataRow[] LFRrow;
            string company = string.Empty;
            DataSet salesDs = objBs.GetAllSales(ddloutlet.SelectedValue);
            DataSet productDs = new DataSet();
            DataSet dsCharges = new DataSet();
            DataSet purchaseDs = objBs.GetAllPurchase(ddloutlet.SelectedValue);
            DataSet openingStockDs = objBs.GetAllOpeningStock(ddloutlet.SelectedValue);
            productDs = objBs.GetProductGPForId(ddloutlet.SelectedValue);
            dsCharges = objBs.GetPurchaseChargesTotal(ddloutlet.SelectedValue);
            int opCnt = 0;
            double opQty = 0;
            double purchaseQty = 0;
            DataSet GrossProfitDs = new DataSet();
            DataTable dt;
            DataRow dr;
            DataColumn dc, dateDc;
            DateTime billDate = new DateTime();
            try
            {
                dt = new DataTable();



                dc = new DataColumn("ItemCode");
                dt.Columns.Add(dc);


                dc = new DataColumn("SalesQty");
                dt.Columns.Add(dc);

                dateDc = new DataColumn("SalesBillDate");
                dateDc.DataType = Type.GetType("System.DateTime");
                dt.Columns.Add(dateDc);



                dc = new DataColumn("PurchaseRate");
                dt.Columns.Add(dc);



                GrossProfitDs.Tables.Add(dt);



                if (salesDs != null)
                {
                    if (salesDs.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow SalesRow in salesDs.Tables[0].Rows)
                        {
                            salesQty = 0;
                            salesItemCode = string.Empty;


                            opCnt = 0;
                            if (SalesRow["ItemCode"] != null)
                            {
                                salesItemCode = Convert.ToString(SalesRow["ItemCode"]).Trim();
                            }
                            if (SalesRow["Qty"] != null)
                            {
                                salesQty = Convert.ToDouble(SalesRow["Qty"]);
                            }
                            if (SalesRow["BillDate"] != null)
                            {
                                salesBillDate = Convert.ToDateTime(SalesRow["BillDate"]);
                            }



                            dr = GrossProfitDs.Tables[0].NewRow();

                            dr["ItemCode"] = salesItemCode;





                            dr["SalesBillDate"] = salesBillDate;



                            if (Request.Cookies["Company"] != null)
                                company = Request.Cookies["Company"].Value;
                            if (openingStockDs != null)
                            {
                                foreach (DataRow OpRow in openingStockDs.Tables[0].Rows)
                                {
                                    if (salesItemCode.Trim() == Convert.ToString(OpRow["ItemCode"]).Trim())
                                    {
                                        opQty = Convert.ToDouble(OpRow["OpeningStock"]);
                                        if (opQty > 0)
                                        {
                                            if (salesQty >= opQty)
                                            {


                                                SearchProductRow = productDs.Tables[0].Select("Itemcode='" + salesItemCode.Trim() + "'");

                                                salesQty = salesQty - opQty;
                                                if (SearchProductRow != null)
                                                {

                                                    dr["SalesQty"] = opQty.ToString();
                                                    if (SearchProductRow[0]["Rate"] != null)
                                                    {
                                                        if (SearchProductRow[0]["Rate"] != DBNull.Value)
                                                        {
                                                            if (Convert.ToString(SearchProductRow[0]["Rate"]) != "")
                                                                dr["PurchaseRate"] = Convert.ToString(SearchProductRow[0]["Rate"]);
                                                            else
                                                                dr["PurchaseRate"] = "0";
                                                        }
                                                    }

                                                    GrossProfitDs.Tables[0].Rows.Add(dr);

                                                }

                                                opQty = 0;
                                                openingStockDs.Tables[0].Rows[opCnt].BeginEdit();
                                                openingStockDs.Tables[0].Rows[opCnt]["OpeningStock"] = opQty;
                                                openingStockDs.Tables[0].Rows[opCnt].EndEdit();
                                                openingStockDs.Tables[0].Rows[opCnt].AcceptChanges();
                                            }
                                            else
                                            {

                                                SearchProductRow = productDs.Tables[0].Select("Itemcode='" + salesItemCode.Trim() + "'");
                                                opQty = opQty - salesQty;
                                                if (SearchProductRow != null)
                                                {
                                                    dr["SalesQty"] = salesQty.ToString();

                                                    if (SearchProductRow[0]["Rate"] != null)
                                                    {
                                                        if (SearchProductRow[0]["Rate"] != DBNull.Value)
                                                        {
                                                            if (Convert.ToString(SearchProductRow[0]["Rate"]) != "")
                                                                dr["PurchaseRate"] = Convert.ToString(SearchProductRow[0]["Rate"]);
                                                            else
                                                                dr["PurchaseRate"] = "0";
                                                        }
                                                    }
                                                }

                                                GrossProfitDs.Tables[0].Rows.Add(dr);

                                                //bl.InsertGP(dr);

                                                openingStockDs.Tables[0].Rows[opCnt].BeginEdit();
                                                openingStockDs.Tables[0].Rows[opCnt]["OpeningStock"] = opQty;
                                                openingStockDs.Tables[0].Rows[opCnt].EndEdit();
                                                openingStockDs.Tables[0].Rows[opCnt].AcceptChanges();
                                                salesQty = 0;

                                            }
                                        }
                                        break;
                                    }
                                    opCnt = opCnt + 1;
                                }
                            }
                            if (salesQty > 0)
                            {

                                if (purchaseDs != null)
                                {
                                    if (purchaseDs.Tables[0].Rows.Count > 0)
                                    {

                                        foreach (DataRow purchaseRow in purchaseDs.Tables[0].Rows)
                                        {
                                            if (salesQty > 0)
                                            {

                                                if (salesItemCode.Trim() == Convert.ToString(purchaseRow["ItemCode"]).Trim())
                                                {
                                                    purchaseQty = Convert.ToDouble(purchaseRow["Qty"]);
                                                    if (purchaseQty > 0)
                                                    {

                                                        dr = GrossProfitDs.Tables[0].NewRow();

                                                        dr["ItemCode"] = salesItemCode;
                                                        dr["SalesBillDate"] = salesBillDate;

                                                        if (salesQty >= purchaseQty)
                                                        {

                                                            salesQty = salesQty - purchaseQty;
                                                            dr["SalesQty"] = purchaseQty.ToString();

                                                            dr["PurchaseRate"] = Convert.ToString(purchaseRow["Rate"]);





                                                            GrossProfitDs.Tables[0].Rows.Add(dr);

                                                            //bl.InsertGP(dr);

                                                            purchaseDs.Tables[0].Rows[purchaseCnt].BeginEdit();
                                                            purchaseDs.Tables[0].Rows[purchaseCnt]["Qty"] = "0";
                                                            purchaseDs.Tables[0].Rows[purchaseCnt].EndEdit();
                                                            purchaseDs.Tables[0].Rows[purchaseCnt].AcceptChanges();
                                                            //purchaseDs.Tables[0].Rows[purchaseCnt].Delete();
                                                            purchaseCnt = purchaseCnt + 1;
                                                        }
                                                        else
                                                        {
                                                            purchaseQty = purchaseQty - salesQty;
                                                            dr["SalesQty"] = salesQty.ToString();

                                                            dr["PurchaseRate"] = Convert.ToString(purchaseRow["Rate"]);


                                                            GrossProfitDs.Tables[0].Rows.Add(dr);

                                                            //bl.InsertGP(dr);

                                                            purchaseDs.Tables[0].Rows[purchaseCnt].BeginEdit();
                                                            purchaseDs.Tables[0].Rows[purchaseCnt]["Qty"] = purchaseQty;
                                                            purchaseDs.Tables[0].Rows[purchaseCnt].EndEdit();
                                                            purchaseDs.Tables[0].Rows[purchaseCnt].AcceptChanges();
                                                            salesQty = 0;
                                                            break;
                                                        }
                                                        //GrossProfitDs.Tables[0].Rows.Add(dr);
                                                    }
                                                    else
                                                    {
                                                        continue;
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                break;
                                            }

                                        }


                                    }
                                }



                            }

                        }
                    }
                }
                //bl.InsertGP(GrossProfitDs);

                return GrossProfitDs;
            }
            catch (Exception ex)
            {
                throw ex;

                return GrossProfitDs;
            }
        }
    }
}