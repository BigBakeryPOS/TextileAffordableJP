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
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class Sheet : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        public double debitTotal = 0;
        public double creditTotal = 0;

        public double debitTotaln = 0;
        public double creditTotaln = 0;
        public Double damt = 0.0;
        public Double camt = 0.0;
        public Double dDiffamt = 0.0;
        public Double cDiffamt = 0.0;
        double OpBalance = 0.0;
        string sTableName = "";
        double inqty = 0;
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

        double opCr = 0;
        double opDr = 0;
        double netOp = 0;
        double tot = 0;
        string strPreviousRowID = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex = 1;
        double dblSubTotalUnitPrice = 0;
        double dblSubTotalQuantity = 0;
        double dblSubTotalDiscount = 0;
        double dblSubTotalAmount = 0;
        // To temporarily store Grand Total    
        double dblGrandTotalUnitPrice = 0;
        double dblGrandTotalQuantity = 0;
        double dblGrandTotalDiscount = 0;
        double dblGrandTotalAmount = 0;

        double opday = 0;
        decimal totalDebit1 = 0;
        decimal totalCredit1 = 0;
        int totalItems1 = 0;
        string sAdmin1 = "";
        double opCr1 = 0;
        double opDr1 = 0;
        double netOp1 = 0;

        string strPreviousRowID1 = string.Empty;
        // To keep track the Index of Group Total    
        int intSubTotalIndex1 = 1;
        double dblSubTotalUnitPrice1 = 0;
        double dblSubTotalQuantity1 = 0;
        double dblSubTotalDiscount1 = 0;
        double dblSubTotalAmount1 = 0;
        // To temporarily store Grand Total    
        double dblGrandTotalUnitPrice1 = 0;
        double dblGrandTotalQuantity1 = 0;
        double dblGrandTotalDiscount1 = 0;
        double dblGrandTotalAmount1 = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
                string UserName = Session["UserName"].ToString();
                string UserID = Session["UserID"].ToString();
                sTableName = Session["User"].ToString();

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


            //lblDebitTotal.Text = debitTotal.ToString("f2");
            //lblCreditTotal.Text = creditTotal.ToString("f2");

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //}

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[0].Text = "Net Profit";

                double doub = (debitTotal + debitTotal1) - (creditTotal + creditTotal1);

                if (doub > 0)
                {
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = doub.ToString("f2");
                }
                else
                {
                    e.Row.Cells[1].Text = (-doub).ToString("f2");
                    e.Row.Cells[2].Text = "";
                }



                //e.Row.Cells[3].Text = doub.ToString("f2");
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                //e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;


                double tot;
                double tot1;
                if (doub > 0)
                {
                    tot = debitTotal1 + doub;
                }
                else
                {
                    doub = -doub;
                    tot = debitTotal1 + doub;
                }

                double tt = debitTotal - creditTotal;
                if (tt > 0)
                {
                    tot1 = tt + creditTotal1;
                }
                else
                {
                    tot1 = (-tt) + creditTotal1;
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

                            tablecell.Text = Convert.ToString(tot);
                            tablecell.ForeColor = System.Drawing.Color.Red;
                            tablecell.HorizontalAlign = HorizontalAlign.Right;
                        }
                    }
                    //   else
                    {
                        if (i == 2)
                        {

                            tablecell.Text = Convert.ToString(tot1);
                            tablecell.ForeColor = System.Drawing.Color.Red;
                            tablecell.HorizontalAlign = HorizontalAlign.Right;
                        }

                    }




                    row.Cells.Add(tablecell);
                }
                this.GridView1.Controls[0].Controls.Add(row);
            }

        }
        public void gBalance1_RowDataBound(object sender, GridViewRowEventArgs e)
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


            //lblDebitTotal.Text = debitTotal.ToString("f2");
            //lblCreditTotal.Text = creditTotal.ToString("f2");

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //}

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = debitTotal.ToString("f2");
                e.Row.Cells[2].Text = creditTotal.ToString("f2");
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

                            tablecell.Text = Convert.ToString(doub);
                            tablecell.ForeColor = System.Drawing.Color.Red;
                            tablecell.HorizontalAlign = HorizontalAlign.Right;
                        }
                    }
                    else
                    {
                        if (i == 2)
                        {

                            tablecell.Text = Convert.ToString(-doub);
                            tablecell.ForeColor = System.Drawing.Color.Red;
                        }

                    }




                    row.Cells.Add(tablecell);
                }
                this.gvLiaLedger1.Controls[0].Controls.Add(row);
                //  e.Row.Cells[2].Text

                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
            }

        }
        public void sheetlist()
        {
            DataTable grdDt = new DataTable();
            string bnch = string.Empty;
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
            lblMessage.Text = " Balance Sheet Report From  '" + txtfrmdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
            //DateTime startDate = Convert.ToDateTime(txtfrmdate.Text);
            //DateTime endDate = Convert.ToDateTime(txttodate.Text);
            DataSet dss1 = new DataSet();
            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            string TrailFlag = string.Empty;

            DataSet mainDs = objBs.GetsheetGroups();
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
                            strDebit = Convert.ToString(totalSum.ToString("f2"));
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
                            strCredit = Convert.ToString(totalSum.ToString("f2"));
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
            DataSet dsss1 = new DataSet();
            foreach (DataRow dr in grdDs.Tables[0].Rows)
            {
                GridView gv = FindControl("gvLiaLedger") as GridView;
                String groupid = dr["Groupid"].ToString();
                if (groupid != "" && groupid != "9" && groupid != "7")
                {
                    if (groupid == "18")
                    {
                        DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, ddloutlet.SelectedValue);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                            {
                                DataTable dt1;
                                DataRow drNew1;
                                DataColumn dc1;
                                DataSet dstd1 = new DataSet();

                                dt1 = new DataTable();
                                dc1 = new DataColumn("LedgerID");
                                dt1.Columns.Add(dc1);

                                dc1 = new DataColumn("LedgerName");
                                dt1.Columns.Add(dc1);

                                dc1 = new DataColumn("Branch");
                                dt1.Columns.Add(dc1);

                                dc1 = new DataColumn("Folionumber");
                                dt1.Columns.Add(dc1);

                                dc1 = new DataColumn("Debit");
                                dt1.Columns.Add(dc1);

                                dc1 = new DataColumn("Credit");
                                dt1.Columns.Add(dc1);

                                dstd1.Tables.Add(dt1);
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    drNew1 = dt1.NewRow();
                                    drNew1["LedgerID"] = ds.Tables[0].Rows[i]["Ledgerid"].ToString();
                                    drNew1["LedgerName"] = ds.Tables[0].Rows[i]["Ledgername"].ToString();
                                    drNew1["Branch"] = ds.Tables[0].Rows[i]["Branch"].ToString();
                                  //  if ((ds.Tables[0].Rows[i]["Ledgername"].ToString() == "Apm Tenkasi") || (ds.Tables[0].Rows[i]["Ledgername"].ToString() == "Apm Chennai") || (ds.Tables[0].Rows[i]["Ledgername"].ToString() == "Apm Coimbatore"))
                                    {
                                        if (ds.Tables[0].Rows[i]["Credit"].ToString() != "")
                                        {

                                            if (Convert.ToDouble(ds.Tables[0].Rows[i]["Credit"]) > 0)
                                            {

                                                drNew1["Credit"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Credit"]);
                                            }
                                        }
                                        if (ds.Tables[0].Rows[i]["Debit"].ToString() != "")
                                        {

                                            if (Convert.ToDouble(ds.Tables[0].Rows[i]["Debit"]) > 0)
                                            {

                                                drNew1["Debit"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Debit"]);
                                            }
                                        }
                                    }
                                    //else
                                    //{
                                    //    if (ds.Tables[0].Rows[i]["Credit"].ToString() != "")
                                    //    {

                                    //        if (Convert.ToDouble(ds.Tables[0].Rows[i]["Credit"]) > 0)
                                    //        {
                                    //            drNew1["Debit"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Credit"]);
                                    //            drNew1["Credit"] = "";
                                    //        }

                                    //        else
                                    //        {
                                    //            drNew1["Credit"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Debit"]);

                                    //            drNew1["Debit"] = "";
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        drNew1["Credit"] = Convert.ToDouble(ds.Tables[0].Rows[i]["Debit"]);

                                    //        drNew1["Debit"] = "";
                                    //    }
                                    //}
                                    drNew1["Folionumber"] = "";

                                    dstd1.Tables[0].Rows.Add(drNew1);
                                }
                                dss.Merge(dstd1);
                            }
                        }
                    }

                    else
                    {



                        DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, ddloutlet.SelectedValue);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                            {
                                //if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "Transfer Tenkasi")
                                //{
                                //    dss.Merge(ds);
                                //}
                                //else if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "Transfer Coimbatore")
                                //{
                                //    dss.Merge(ds);
                                //}
                                //else if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "Transfer Chennai")
                                //{
                                    dss.Merge(ds);
                                //}
                            }
                        }
                    }
                }
                else if (groupid == "7")
                {
                    BSClass objbs = new BSClass();


                    string Branch = ddloutlet.SelectedValue;
                    if (Branch != "All")
                    {

                        //////Calculate();

                        ////////BSClass objbs = new BSClass();

                        //////lblMessage.Text = "APM Motor Balance Sheet Report From  '" + txtfrmdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
                        ////////DateTime stdt = Convert.ToDateTime(txtfromdate.Text);
                        ////////DateTime etdt = Convert.ToDateTime(txttodate.Text);
                        ////////string Branch = ddloutlet.SelectedValue;
                        //////DateTime stdt = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //////DateTime etdt = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //////string dateee = string.Empty;

                        //////DataSet ddaybookdate = new DataSet();
                        //////ddaybookdate = objBs.getdaybookdate(Session["Yearid"].ToString());
                        //////if (ddaybookdate.Tables[0].Rows.Count > 0)
                        //////{
                        //////    dateee = ddaybookdate.Tables[0].Rows[0]["DayBookDate"].ToString();
                        //////}
                        //////else
                        //////{
                        //////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Daybook Not Exists.So Please Contact Administrator!!!');", true);
                        //////    return;
                        //////}

                        //////DateTime startDate1 = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        //////DateTime Checkdate = DateTime.ParseExact(dateee, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ////////////DataSet dsday = objbs.generateDayBook2(stdt, etdt, Branch);

                        //////////////double obaldr = objbs.getLedgerOpeningBalanceday(0, "debit", Branch);
                        //////////////double obalcr = objbs.getLedgerOpeningBalanceday(0, "credit", Branch);

                        ////////////double obaldr = objbs.getLedgerOpBalanceday(0, "debit", Branch, "Cash A/C _" + Branch);
                        ////////////double obalcr = objbs.getLedgerOpBalanceday(0, "credit", Branch, "Cash A/C _" + Branch);

                        ////////////if (dsday.Tables[0].Rows.Count > 0)
                        ////////////{


                        ////////////    foreach (DataRow dday in dsday.Tables[0].Rows)
                        ////////////    {
                        ////////////        if (dday["Debit"] != "")
                        ////////////        {
                        ////////////            opDr = opDr + Convert.ToDouble(dday["Debit"]);
                        ////////////        }

                        ////////////        if (dday["Credit"] != "")
                        ////////////        {
                        ////////////            opCr = opCr + Convert.ToDouble(dday["Credit"]);
                        ////////////        }

                        ////////////    }
                        ////////////}

                        ////////////opCr = opCr + obalcr;
                        ////////////opDr = opDr + obaldr;

                        ////////////netOp = opDr - opCr;
                        //////if (stdt <= Checkdate)
                        //////{
                        //////    opday = netOp1;
                        //////}
                        //////if (opday > 0)
                        //////{
                        //////    dblSubTotalQuantity = dblSubTotalQuantity + opday;

                        //////}
                        //////else
                        //////{
                        //////    dblSubTotalUnitPrice = dblSubTotalUnitPrice + (-(opday));
                        //////}



                        ////////opCr = objBs.getOpening(0, 0, 0, "credit", startDate, ddloutlet.SelectedValue);
                        ////////opDr = objBs.getOpening(0, 0, 0, "debit", startDate, ddloutlet.SelectedValue);


                        ////////if (opDr > opCr)
                        ////////{
                        ////////    netOp = opDr - opCr;
                        ////////    //lblOBDR.Text = netOp.ToString("f2");
                        ////////    //lblOBCR.Text = "0.00";
                        ////////}
                        ////////else
                        ////////{
                        ////////    netOp = opCr - opDr;
                        ////////    //lblOBDR.Text = "0.00";
                        ////////    //lblOBCR.Text = netOp.ToString("f2");
                        ////////}


                        //////DataSet ds = objbs.generateDayBook(stdt, etdt, Branch);

                        //////DataSet dstd = new DataSet();

                        //////if (ds != null)
                        //////{
                        //////    DataTable dt;
                        //////    DataRow drNew;
                        //////    DataColumn dc;

                        //////    dt = new DataTable();
                        //////    dc = new DataColumn("Date");
                        //////    dt.Columns.Add(dc);

                        //////    dc = new DataColumn("Branchcode");
                        //////    dt.Columns.Add(dc);

                        //////    dc = new DataColumn("Particulars");
                        //////    dt.Columns.Add(dc);

                        //////    dc = new DataColumn("Narration");
                        //////    dt.Columns.Add(dc);

                        //////    dc = new DataColumn("Debit");
                        //////    dt.Columns.Add(dc);

                        //////    dc = new DataColumn("Credit");
                        //////    dt.Columns.Add(dc);

                        //////    dstd.Tables.Add(dt);

                        //////    if (ds.Tables[0].Rows.Count > 0)
                        //////    {
                        //////        foreach (DataRow dr1 in ds.Tables[0].Rows)
                        //////        {
                        //////            if (dr1["type"].ToString() == "Sales" || dr1["type"].ToString() == "Purchase")
                        //////            {
                        //////                drNew = dt.NewRow();
                        //////                drNew["Narration"] = dr1["Narration"];
                        //////                drNew["Branchcode"] = dr1["Branchcode"];
                        //////                drNew["Date"] = dr1["Date"];
                        //////                if (dr1["Debit"] != "")
                        //////                {
                        //////                    drNew["Debit"] = Convert.ToDouble(dr1["Debit"]).ToString("f2");
                        //////                }
                        //////                else
                        //////                {
                        //////                    drNew["Debit"] = "";

                        //////                }
                        //////                drNew["Credit"] = "";
                        //////                drNew["Particulars"] = dr1["Debitor"].ToString();
                        //////                if (dr1["Debitor"].ToString() != "")
                        //////                {
                        //////                    dstd.Tables[0].Rows.Add(drNew);
                        //////                }

                        //////                drNew = dt.NewRow();
                        //////                drNew["Narration"] = dr1["Narration"];
                        //////                drNew["Branchcode"] = dr1["Branchcode"];
                        //////                drNew["Date"] = dr1["Date"];
                        //////                drNew["Debit"] = "";
                        //////                if (dr1["Credit"] != "")
                        //////                {
                        //////                    drNew["Credit"] = Convert.ToDouble(dr1["Credit"]).ToString("f2");
                        //////                }
                        //////                else
                        //////                {
                        //////                    drNew["Credit"] = "";

                        //////                }
                        //////                drNew["Particulars"] = dr1["Creditor"].ToString();
                        //////                if (dr1["Creditor"].ToString() != "")
                        //////                {
                        //////                    dstd.Tables[0].Rows.Add(drNew);
                        //////                }
                        //////            }
                        //////            else
                        //////            {
                        //////                drNew = dt.NewRow();
                        //////                drNew["Narration"] = dr1["Narration"];
                        //////                drNew["Branchcode"] = dr1["Branchcode"];
                        //////                drNew["Date"] = dr1["Date"];
                        //////                drNew["Debit"] = "";
                        //////                if (dr1["Credit"] != "")
                        //////                {
                        //////                    drNew["Credit"] = Convert.ToDouble(dr1["Credit"]).ToString("f2");
                        //////                }
                        //////                else
                        //////                {
                        //////                    drNew["Credit"] = "";
                        //////                }
                        //////                drNew["Particulars"] = dr1["Creditor"].ToString();
                        //////                if (dr1["Creditor"].ToString() != "")
                        //////                {
                        //////                    dstd.Tables[0].Rows.Add(drNew);
                        //////                }

                        //////                drNew = dt.NewRow();
                        //////                drNew["Narration"] = dr1["Narration"];
                        //////                drNew["Branchcode"] = dr1["Branchcode"];
                        //////                drNew["Date"] = dr1["Date"];
                        //////                if (dr1["Debit"] != "")
                        //////                {
                        //////                    drNew["Debit"] = Convert.ToDouble(dr1["Debit"]).ToString("f2");

                        //////                }
                        //////                drNew["Credit"] = "";
                        //////                drNew["Particulars"] = dr1["Debitor"].ToString();
                        //////                if (dr1["Debitor"].ToString() != "")
                        //////                {
                        //////                    dstd.Tables[0].Rows.Add(drNew);
                        //////                }

                        //////            }
                        //////        }
                        //////    }
                        //////}


                        //////gvLedger.DataSource = dstd;
                        //////gvLedger.DataBind();




                        ////////Calculate();
                        //////idt.Visible = true;
                        //////ViewState["MyDataSet"] = dstd;
                        ////////DataSet ds1 = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, ddloutlet.SelectedValue);
                        ////////if (ds1.Tables[0].Rows.Count > 0)
                        ////////{
                        ////////    if (ds1.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                        ////////    {
                        ////////        dss1.Merge(ds1);
                        ////////    }
                        ////////}
                        //////DataSet dsNew = new DataSet();
                        //////DataTable dtNew1 = new DataTable();

                        //////DataColumn dcNew = new DataColumn();
                        //////DataRow drNew1;
                        //////dcNew = new DataColumn("LedgerName");
                        //////dtNew1.Columns.Add(dcNew);
                        //////dcNew = new DataColumn("LedgerID");
                        //////dtNew1.Columns.Add(dcNew);
                        //////dcNew = new DataColumn("Folionumber");
                        //////dtNew1.Columns.Add(dcNew);
                        //////dcNew = new DataColumn("Branch");
                        //////dtNew1.Columns.Add(dcNew);
                        //////dcNew = new DataColumn("Debit");
                        //////dtNew1.Columns.Add(dcNew);
                        //////dcNew = new DataColumn("Credit");
                        //////dtNew1.Columns.Add(dcNew);


                        //////dsNew.Tables.Add(dtNew1);
                        //////DataSet ds1 = objbs.getbranchlistforprint(Branch);
                        //////string branchname1 = string.Empty;
                        //////for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        //////{
                        //////    branchname1 = ds1.Tables[0].Rows[i]["Branchcode"].ToString();

                        //////}
                        //////drNew1 = dtNew1.NewRow();
                        //////drNew1["LedgerID"] = "";
                        //////drNew1["LedgerName"] = "Cash A/C _" + branchname1;
                        //////drNew1["Branch"] = Branch;
                        //////if (tot > 0.0)
                        //////{

                        //////    drNew1["Credit"] = tot;
                        //////}

                        //////else
                        //////{
                        //////    drNew1["Debit"] = -tot;
                        //////}
                        //////drNew1["Folionumber"] = "";

                        //////dsNew.Tables[0].Rows.Add(drNew1);
                        //////dss1.Merge(dsNew);

                        DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(7), startDate, endDate, ddloutlet.SelectedValue);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                            {
                                //if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "Transfer Tenkasi")
                                //{
                                //    dss.Merge(ds);
                                //}
                                //else if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "Transfer Coimbatore")
                                //{
                                //    dss.Merge(ds);
                                //}
                                //else if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "Transfer Chennai")
                                //{
                                dss1.Merge(ds);
                                //}
                            }
                        }
                    }

                    else
                    {
                        //DataSet dsNew = new DataSet();
                        //DataTable dtNew1 = new DataTable();

                        //DataColumn dcNew = new DataColumn();
                        //DataRow drNew1;
                        //dcNew = new DataColumn("LedgerName");
                        //dtNew1.Columns.Add(dcNew);
                        //dcNew = new DataColumn("LedgerID");
                        //dtNew1.Columns.Add(dcNew);
                        //dcNew = new DataColumn("Folionumber");
                        //dtNew1.Columns.Add(dcNew);
                        //dcNew = new DataColumn("Branch");
                        //dtNew1.Columns.Add(dcNew);
                        //dcNew = new DataColumn("Debit");
                        //dtNew1.Columns.Add(dcNew);
                        //dcNew = new DataColumn("Credit");
                        //dtNew1.Columns.Add(dcNew);


                        //dsNew.Tables.Add(dtNew1);


                        DataSet ds1 = objbs.selectBranch();

                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            string sbranch = ds1.Tables[0].Rows[i]["Branchcode"].ToString();
                            string scity = ds1.Tables[0].Rows[i]["City"].ToString();


                            //////Calculate();

                            ////////BSClass objbs = new BSClass();

                            //////lblMessage.Text = "APM Motor Balance Sheet Report From  '" + txtfrmdate.Text + "'  To  '" + txttodate.Text + "'  for  " + ddloutlet.SelectedItem.Text;
                            ////////DateTime stdt = Convert.ToDateTime(txtfromdate.Text);
                            ////////DateTime etdt = Convert.ToDateTime(txttodate.Text);
                            ////////string Branch = ddloutlet.SelectedValue;
                            //////DateTime stdt = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //////DateTime etdt = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            //////string dateee = string.Empty;

                            //////DataSet ddaybookdate = new DataSet();
                            //////ddaybookdate = objBs.getdaybookdate(Session["Yearid"].ToString());
                            //////if (ddaybookdate.Tables[0].Rows.Count > 0)
                            //////{
                            //////    dateee = ddaybookdate.Tables[0].Rows[0]["DayBookDate"].ToString();
                            //////}
                            //////else
                            //////{
                            //////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Daybook Not Exists.So Please Contact Administrator!!!');", true);
                            //////    return;
                            //////}

                            //////DateTime startDate1 = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                            //////DateTime Checkdate = DateTime.ParseExact(dateee, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            ////////////DataSet dsday = objbs.generateDayBook2(stdt, etdt, Branch);

                            //////////////double obaldr = objbs.getLedgerOpeningBalanceday(0, "debit", Branch);
                            //////////////double obalcr = objbs.getLedgerOpeningBalanceday(0, "credit", Branch);

                            ////////////double obaldr = objbs.getLedgerOpBalanceday(0, "debit", Branch, "Cash A/C _" + Branch);
                            ////////////double obalcr = objbs.getLedgerOpBalanceday(0, "credit", Branch, "Cash A/C _" + Branch);

                            ////////////if (dsday.Tables[0].Rows.Count > 0)
                            ////////////{


                            ////////////    foreach (DataRow dday in dsday.Tables[0].Rows)
                            ////////////    {
                            ////////////        if (dday["Debit"] != "")
                            ////////////        {
                            ////////////            opDr = opDr + Convert.ToDouble(dday["Debit"]);
                            ////////////        }

                            ////////////        if (dday["Credit"] != "")
                            ////////////        {
                            ////////////            opCr = opCr + Convert.ToDouble(dday["Credit"]);
                            ////////////        }

                            ////////////    }
                            ////////////}

                            ////////////opCr = opCr + obalcr;
                            ////////////opDr = opDr + obaldr;

                            ////////////netOp = opDr - opCr;
                            //////if (stdt <= Checkdate)
                            //////{
                            //////    opday = netOp1;
                            //////}
                            //////if (opday > 0)
                            //////{
                            //////    dblSubTotalQuantity = dblSubTotalQuantity + opday;

                            //////}
                            //////else
                            //////{
                            //////    dblSubTotalUnitPrice = dblSubTotalUnitPrice + (-(opday));
                            //////}



                            ////////opCr = objBs.getOpening(0, 0, 0, "credit", startDate, ddloutlet.SelectedValue);
                            ////////opDr = objBs.getOpening(0, 0, 0, "debit", startDate, ddloutlet.SelectedValue);


                            ////////if (opDr > opCr)
                            ////////{
                            ////////    netOp = opDr - opCr;
                            ////////    //lblOBDR.Text = netOp.ToString("f2");
                            ////////    //lblOBCR.Text = "0.00";
                            ////////}
                            ////////else
                            ////////{
                            ////////    netOp = opCr - opDr;
                            ////////    //lblOBDR.Text = "0.00";
                            ////////    //lblOBCR.Text = netOp.ToString("f2");
                            ////////}


                            //////DataSet ds = objbs.generateDayBook(stdt, etdt, Branch);

                            //////DataSet dstd = new DataSet();

                            //////if (ds != null)
                            //////{
                            //////    DataTable dt;
                            //////    DataRow drNew;
                            //////    DataColumn dc;

                            //////    dt = new DataTable();
                            //////    dc = new DataColumn("Date");
                            //////    dt.Columns.Add(dc);

                            //////    dc = new DataColumn("Branchcode");
                            //////    dt.Columns.Add(dc);

                            //////    dc = new DataColumn("Particulars");
                            //////    dt.Columns.Add(dc);

                            //////    dc = new DataColumn("Narration");
                            //////    dt.Columns.Add(dc);

                            //////    dc = new DataColumn("Debit");
                            //////    dt.Columns.Add(dc);

                            //////    dc = new DataColumn("Credit");
                            //////    dt.Columns.Add(dc);

                            //////    dstd.Tables.Add(dt);

                            //////    if (ds.Tables[0].Rows.Count > 0)
                            //////    {
                            //////        foreach (DataRow drh in ds.Tables[0].Rows)
                            //////        {
                            //////            if (drh["type"].ToString() == "Sales" || drh["type"].ToString() == "Purchase")
                            //////            {
                            //////                drNew = dt.NewRow();
                            //////                drNew["Narration"] = drh["Narration"];
                            //////                drNew["Branchcode"] = drh["Branchcode"];
                            //////                drNew["Date"] = drh["Date"];
                            //////                if (drh["Debit"] != "")
                            //////                {
                            //////                    drNew["Debit"] = Convert.ToDouble(drh["Debit"]).ToString("f2");
                            //////                }
                            //////                else
                            //////                {
                            //////                    drNew["Debit"] = "";

                            //////                }
                            //////                drNew["Credit"] = "";
                            //////                drNew["Particulars"] = drh["Debitor"].ToString();
                            //////                if (drh["Debitor"].ToString() != "")
                            //////                {
                            //////                    dstd.Tables[0].Rows.Add(drNew);
                            //////                }

                            //////                drNew = dt.NewRow();
                            //////                drNew["Narration"] = drh["Narration"];
                            //////                drNew["Branchcode"] = drh["Branchcode"];
                            //////                drNew["Date"] = drh["Date"];
                            //////                drNew["Debit"] = "";
                            //////                if (drh["Credit"] != "")
                            //////                {
                            //////                    drNew["Credit"] = Convert.ToDouble(drh["Credit"]).ToString("f2");
                            //////                }
                            //////                else
                            //////                {
                            //////                    drNew["Credit"] = "";

                            //////                }
                            //////                drNew["Particulars"] = drh["Creditor"].ToString();
                            //////                if (drh["Creditor"].ToString() != "")
                            //////                {
                            //////                    dstd.Tables[0].Rows.Add(drNew);
                            //////                }
                            //////            }
                            //////            else
                            //////            {
                            //////                drNew = dt.NewRow();
                            //////                drNew["Narration"] = drh["Narration"];
                            //////                drNew["Branchcode"] = drh["Branchcode"];
                            //////                drNew["Date"] = drh["Date"];
                            //////                drNew["Debit"] = "";
                            //////                if (drh["Credit"] != "")
                            //////                {
                            //////                    drNew["Credit"] = Convert.ToDouble(drh["Credit"]).ToString("f2");
                            //////                }
                            //////                else
                            //////                {
                            //////                    drNew["Credit"] = "";
                            //////                }
                            //////                drNew["Particulars"] = drh["Creditor"].ToString();
                            //////                if (drh["Creditor"].ToString() != "")
                            //////                {
                            //////                    dstd.Tables[0].Rows.Add(drNew);
                            //////                }

                            //////                drNew = dt.NewRow();
                            //////                drNew["Narration"] = drh["Narration"];
                            //////                drNew["Branchcode"] = drh["Branchcode"];
                            //////                drNew["Date"] = drh["Date"];
                            //////                if (drh["Debit"] != "")
                            //////                {
                            //////                    drNew["Debit"] = Convert.ToDouble(drh["Debit"]).ToString("f2");

                            //////                }
                            //////                drNew["Credit"] = "";
                            //////                drNew["Particulars"] = drh["Debitor"].ToString();
                            //////                if (drh["Debitor"].ToString() != "")
                            //////                {
                            //////                    dstd.Tables[0].Rows.Add(drNew);
                            //////                }

                            //////            }
                            //////        }
                            //////    }
                            //////}


                            //////gvLedger.DataSource = dstd;
                            //////gvLedger.DataBind();




                            ////////Calculate();
                            //////idt.Visible = true;
                            //////ViewState["MyDataSet"] = dstd;
                            //////opDr = 0;
                            //////opCr = 0;
                            //////netOp = 0;
                            //////dblSubTotalUnitPrice = 0;
                            //////dblSubTotalQuantity = 0;
                            //////dblGrandTotalUnitPrice = 0;
                            //////dblGrandTotalQuantity = 0;

                            ////////DataSet ds1 = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, ddloutlet.SelectedValue);
                            ////////if (ds1.Tables[0].Rows.Count > 0)
                            ////////{
                            ////////    if (ds1.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                            ////////    {
                            ////////        dss1.Merge(ds1);
                            ////////    }
                            ////////}


                            //////drNew1 = dtNew1.NewRow();
                            //////drNew1["LedgerID"] = "";
                            //////drNew1["LedgerName"] = "Cash A/C _" + scity;
                            //////drNew1["Branch"] = sbranch;
                            //////if (tot > 0)
                            //////{

                            //////    drNew1["Credit"] = tot;
                            //////}

                            //////else
                            //////{
                            //////    drNew1["Debit"] = -tot;
                            //////}
                            //////drNew1["Folionumber"] = "";

                            //////dsNew.Tables[0].Rows.Add(drNew1);

                            DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, sbranch);
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                                {
                                    //if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "Transfer Tenkasi")
                                    //{
                                    //    dss.Merge(ds);
                                    //}
                                    //else if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "Transfer Coimbatore")
                                    //{
                                    //    dss.Merge(ds);
                                    //}
                                    //else if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "Transfer Chennai")
                                    //{
                                    dss1.Merge(ds);
                                    //}
                                }
                            }

                        }
                        //dss1.Merge(dsNew);

                    }


                }



                else if (groupid == "9")
                {
                    DataTable grdDt1 = new DataTable();
                    DataSet grdDs1 = new DataSet();
                    DataTable dtNew1 = new DataTable();
                    int groupID1 = 0;
                    double debitSum1 = 0.0d;
                    double creditSum1 = 0.0d;
                    double totalSum1 = 0.0d;
                    string strParticulars1 = string.Empty;
                    string strDebit1 = string.Empty;
                    string strCredit1 = string.Empty;
                    string sGroupName1 = string.Empty;
                    dtNew1 = GenerateDs("", "", "", "", "");
                    grdDs1.Tables.Add(dtNew1);

                    //////DateTime startDate = Convert.ToDateTime(txtfrmdate.Text);
                    //////DateTime endDate = Convert.ToDateTime(txttodate.Text);

                    //////DateTime startDate1 = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    //////DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    ////string TrailFlag1 = string.Empty;

                    ////DataSet mainDs1 = objBs.GetpnGroups();
                    ////if (mainDs1 != null)
                    ////{
                    ////    if (mainDs1.Tables[0].Rows.Count > 0)
                    ////    {
                    ////        foreach (DataRow mainRow in mainDs1.Tables[0].Rows)
                    ////        {
                    ////            groupID1 = Convert.ToInt32(mainRow["GroupID"]);
                    ////            sGroupName1 = mainRow["GroupName"].ToString();

                    ////            debitSum1 = objBs.GetDebitSum(groupID1, startDate, endDate, ddloutlet.SelectedValue);
                    ////            creditSum1 = objBs.GetCreditSum(groupID1, startDate, endDate, ddloutlet.SelectedValue);

                    ////            TrailFlag1 = Convert.ToString(mainRow["TrailBalance"]);
                    ////            strParticulars1 = Convert.ToString(mainRow["GroupName"]);
                    ////            if (TrailFlag1 == "Debit")
                    ////            {
                    ////                totalSum1 = debitSum1 - creditSum1;
                    ////                strDebit1 = Convert.ToString(totalSum1.ToString("f2"));
                    ////                strCredit1 = "";

                    ////                if (totalSum1 < 0)
                    ////                {
                    ////                    strCredit1 = Convert.ToString(Math.Abs(totalSum1));
                    ////                    strDebit1 = "";
                    ////                }
                    ////            }
                    ////            else
                    ////            {
                    ////                totalSum1 = creditSum1 - debitSum1;
                    ////                strCredit1 = Convert.ToString(totalSum1.ToString("f2"));
                    ////                strDebit1 = "";
                    ////                if (totalSum1 < 0)
                    ////                {
                    ////                    strCredit1 = "";
                    ////                    strDebit1 = Convert.ToString(Math.Abs(totalSum));
                    ////                }
                    ////            }

                    ////            grdDt1 = GenerateDs(sGroupName1, strParticulars1, strDebit1, strCredit1, groupID1.ToString());

                    ////            if (grdDt1 != null)
                    ////            {
                    ////                for (int k = 0; k <= grdDt1.Rows.Count - 1; k++)
                    ////                {

                    ////                    if (grdDt1 != null && grdDt1.Rows.Count > 0)
                    ////                        grdDs1.Tables[0].ImportRow(grdDt1.Rows[k]);
                    ////                }
                    ////            }
                    ////        }
                    ////    }
                    ////}
                    ////grdDs1.Tables[0].Rows[0].Delete();
                    ////DataSet dss1 = new DataSet();
                    ////for (int j = 0; j < grdDs1.Tables[0].Rows.Count; j++)
                    ////{
                    ////    //  GridView gv1 = FindControl("gvLiaLedger") as GridView;
                    ////    String groupid1 = grdDs1.Tables[0].Rows[j]["Groupid"].ToString();
                    ////    if (groupid1 != "" && groupid1 != "9")
                    ////    {


                    ////        DataSet ds1 = objBs.getLedgerTransaction(Convert.ToInt32(groupid1), startDate, endDate, ddloutlet.SelectedValue);
                    ////        if (ds1.Tables[0].Rows.Count > 0)
                    ////        {
                    ////            if (ds1.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                    ////            {
                    ////                dsss1.Merge(ds1);
                    ////            }
                    ////        }

                    ////    }

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
                            dr1["Debit"] = "";

                            dr1["Credit"] = dledgername.Tables[0].Rows[i]["ClosingValue"].ToString();
                        }
                        else
                        {
                            dr1["Debit"] = dledgername.Tables[0].Rows[i]["ClosingValue"].ToString();

                            dr1["Credit"] = "";
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
                    ////DataSet dledgername = objBs.getledgerforprofitloss(groupid1, ddloutlet.SelectedValue);
                    ////if (dledgername.Tables[0].Rows.Count == 0)
                    ////{
                    ////    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Closing Stock Ledger Not Exists!!!');", true);
                    ////    return;
                    ////}
                    ////double closingtotal = 0;
                    ////DataSet allproductt = objBs.getproductt();
                    ////if (allproductt.Tables[0].Rows.Count > 0)
                    ////{
                    ////    for (int i = 0; i < allproductt.Tables[0].Rows.Count; i++)
                    ////    {
                    ////        string product = allproductt.Tables[0].Rows[i]["categoryuserid"].ToString();
                    ////        double openingStock = 0;
                    ////        double salestock = 0;
                    ////        double purchasestock = 0;
                    ////        double purchasereturn = 0;
                    ////        double salesreturn = 0;
                    ////        double deliveryout = 0;
                    ////        double deliveryin = 0;
                    ////        double stockinward = 0;
                    ////        double stockoutward = 0;
                    ////        double overallstock = 0;
                    ////        double currentstock = 0;

                    ////        openingStock = (Convert.ToDouble(objBs.getOpeningStock(product, ddloutlet.SelectedValue)));
                    ////        purchasestock = (Convert.ToDouble(objBs.getOpeningStockPurchase(product, startDate, ddloutlet.SelectedValue)));
                    ////        salestock = (Convert.ToDouble(objBs.getOpeningStockSales(product, startDate, ddloutlet.SelectedValue)));
                    ////        salesreturn = (Convert.ToDouble(objBs.getOpeningStocksalesreturn(product, startDate, ddloutlet.SelectedValue)));
                    ////        purchasereturn = (Convert.ToDouble(objBs.getOpeningStockPurchasereturn(product, startDate, ddloutlet.SelectedValue)));
                    ////        deliveryin = (Convert.ToDouble(objBs.getOpeningStockdeliveryin(product, startDate, ddloutlet.SelectedValue)));
                    ////        deliveryout = (Convert.ToDouble(objBs.getOpeningStockdeliveryout(product, startDate, ddloutlet.SelectedValue)));
                    ////        stockinward = (Convert.ToDouble(objBs.getOpeningStockin(product, startDate, ddloutlet.SelectedValue)));
                    ////        stockoutward = (Convert.ToDouble(objBs.getOpeningStockout(product, startDate, ddloutlet.SelectedValue)));
                    ////        string openingstockk = Convert.ToString(openingStock);
                    ////        currentstock = (Convert.ToDouble(objBs.getcurrentstockpurchase(product, "tblStock_" + ddloutlet.SelectedValue)));
                    ////        // lblcurrentstock.Text = Convert.ToString(currentstock);

                    ////        overallstock = openingStock + purchasestock - salestock + deliveryin - deliveryout + salesreturn - purchasereturn - stockoutward + stockinward;

                    ////        DataSet ds = objBs.generatestockreport(startDate, endDate, ddloutlet.SelectedValue, product, ddloutlet.SelectedValue);
                    ////        DataView dds = ds.Tables[0].DefaultView;
                    ////        dds.Sort = "TransDate ASC";
                    ////        //  gvLedger.DataSource = dds;

                    ////        ViewState["Dateset"] = ds;
                    ////        if (ds.Tables[0].Rows.Count > 0)
                    ////        {

                    ////            if (ddloutlet.SelectedValue == "CO1")
                    ////            {
                    ////                bnch = "TSI";
                    ////            }
                    ////            else if (ddloutlet.SelectedValue == "CO2")
                    ////            {
                    ////                bnch = "CBE";
                    ////            }
                    ////            else
                    ////            {
                    ////                bnch = "MAS";
                    ////            }
                    ////        }
                    ////        string lblpurchase = "";
                    ////        string lblsalereturn = "";
                    ////        string lblsales = "";
                    ////        string lblpurchasereturn = "";
                    ////        string lbldeliveryin = "";
                    ////        string lbldeliveryout = "";
                    ////        string lblstockinward = "";
                    ////        string lblstockoutward = "";
                    ////        inqty = overallstock;

                    ////        //gvLedger.DataBind();

                    ////        if (lblsalereturn == "")
                    ////        {
                    ////            lblsalereturn = "0";
                    ////        }
                    ////        if (lblpurchasereturn == "")
                    ////        {
                    ////            lblpurchasereturn = "0";
                    ////        }
                    ////        if (lblpurchase == "")
                    ////        {
                    ////            lblpurchase = "0";
                    ////        }
                    ////        if (lblsales == "")
                    ////        {
                    ////            lblsales = "0";
                    ////        }
                    ////        if (lblstockinward == "")
                    ////        {
                    ////            lblstockinward = "0";
                    ////        }
                    ////        if (lblstockoutward == "")
                    ////        {
                    ////            lblstockoutward = "0";
                    ////        }
                    ////        if (lbldeliveryin == "")
                    ////        {
                    ////            lbldeliveryin = "0";
                    ////        }
                    ////        if (lbldeliveryout == "")
                    ////        {
                    ////            lbldeliveryout = "0";
                    ////        }

                    ////        double drr = (overallstock + purchasestock1 - salesstock - deliveryout1 + deliveryin1 - purchasereturn1 + salesreturn1 - stockoutward1 + stockinward1);



                    ////        closingtotal = closingtotal + (drr * currentstock);
                    ////        //DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, ddloutlet.SelectedValue);
                    ////        //if (ds.Tables[0].Rows.Count > 0)
                    ////        //{
                    ////        //    if (ds.Tables[0].Rows[0]["Ledgername"].ToString() != "")
                    ////        //    {
                    ////        //        dss.Merge(ds);
                    ////        //    }
                    ////        //}

                    ////    }
                    ////    DataSet ds1 = new DataSet();
                    ////    DataTable dt = new DataTable();

                    ////    DataColumn dc;
                    ////    DataRow dr11;

                    ////    dc = new DataColumn("LedgerName");
                    ////    dt.Columns.Add(dc);

                    ////    dc = new DataColumn("LedgerID");
                    ////    dt.Columns.Add(dc);

                    ////    dc = new DataColumn("Folionumber");
                    ////    dt.Columns.Add(dc);

                    ////    dc = new DataColumn("Branch");
                    ////    dt.Columns.Add(dc);

                    ////    dc = new DataColumn("Debit");
                    ////    dt.Columns.Add(dc);

                    ////    dc = new DataColumn("Credit");
                    ////    dt.Columns.Add(dc);

                    ////    dr11 = dt.NewRow();
                    ////    dr11["LedgerName"] = dledgername.Tables[0].Rows[0]["PrintName"].ToString();
                    ////    dr11["LedgerID"] = dledgername.Tables[0].Rows[0]["LEdgerId"].ToString();

                    ////    dr11["Debit"] = "";

                    ////    dr11["Credit"] = closingtotal;

                    ////    dr11["Folionumber"] = "";
                    ////    dr11["Branch"] = bnch;
                    ////    dt.Rows.Add(dr11);
                    ////    ds1.Tables.Add(dt);
                    ////    if (ds1.Tables[0].Rows.Count > 0)
                    ////    {

                    ////        {
                    ////            dss.Merge(ds1);
                    ////            dsss1.Merge(ds1);
                    ////        }
                    ////    }
                }

            }

            //GENERAL EXPENSE METHOD::
            //////DataTable grdDt2 = new DataTable();
            //////DataSet grdDs2 = new DataSet();
            //////DataTable dtNew2 = new DataTable();
            //////int groupID2 = 0;
            //////double debitSum2 = 0.0d;
            //////double creditSum2 = 0.0d;
            //////double totalSum2 = 0.0d;
            //////string strParticulars2 = string.Empty;
            //////string strDebit2 = string.Empty;
            //////string strCredit2 = string.Empty;
            //////string sGroupName2 = string.Empty;
            //////dtNew2 = GenerateDs("", "", "", "", "");
            //////grdDs2.Tables.Add(dtNew2);

            //////string TrailFlag2 = string.Empty;

            //////DataSet mainDs2 = objBs.GetpnGroups1();
            //////if (mainDs2 != null)
            //////{
            //////    if (mainDs2.Tables[0].Rows.Count > 0)
            //////    {
            //////        foreach (DataRow mainRow2 in mainDs2.Tables[0].Rows)
            //////        {
            //////            groupID2 = Convert.ToInt32(mainRow2["GroupID"]);
            //////            sGroupName2 = mainRow2["GroupName"].ToString();

            //////            debitSum2 = objBs.GetDebitSum(groupID2, startDate, endDate, ddloutlet.SelectedValue);
            //////            creditSum2 = objBs.GetCreditSum(groupID2, startDate, endDate, ddloutlet.SelectedValue);

            //////            TrailFlag2 = Convert.ToString(mainRow2["TrailBalance"]);
            //////            strParticulars2 = Convert.ToString(mainRow2["GroupName"]);
            //////            if (TrailFlag2 == "Debit")
            //////            {
            //////                totalSum2 = debitSum2 - creditSum2;
            //////                strDebit2 = Convert.ToString(totalSum2.ToString("f2"));
            //////                strCredit2 = "";

            //////                if (totalSum2 < 0)
            //////                {
            //////                    strCredit2 = Convert.ToString(Math.Abs(totalSum2));
            //////                    strDebit2 = "";
            //////                }
            //////            }
            //////            else
            //////            {
            //////                totalSum2 = creditSum2 - debitSum2;
            //////                strCredit2 = Convert.ToString(totalSum2.ToString("f2"));
            //////                strDebit2 = "";
            //////                if (totalSum2 < 0)
            //////                {
            //////                    strCredit2 = "";
            //////                    strDebit2 = Convert.ToString(Math.Abs(totalSum2));
            //////                }
            //////            }

            //////            grdDt2 = GenerateDs(sGroupName2, strParticulars2, strDebit2, strCredit2, groupID2.ToString());

            //////            if (grdDt2 != null)
            //////            {
            //////                for (int k = 0; k <= grdDt2.Rows.Count - 1; k++)
            //////                {

            //////                    if (grdDt2 != null && grdDt2.Rows.Count > 0)
            //////                        grdDs2.Tables[0].ImportRow(grdDt2.Rows[k]);
            //////                }
            //////            }
            //////        }
            //////    }
            //////}
            //////grdDs2.Tables[0].Rows[0].Delete();
            //////DataSet dss2 = new DataSet();
            //////foreach (DataRow dr2 in grdDs2.Tables[0].Rows)
            //////{
            //////    GridView gv2 = FindControl("gvLiaLedger") as GridView;
            //////    String groupid2 = dr2["Groupid"].ToString();
            //////    if (groupid2 != "")
            //////    {


            //////        DataSet ds2 = objBs.getLedgerTransaction(Convert.ToInt32(groupid2), startDate, endDate, ddloutlet.SelectedValue);
            //////        if (ds2.Tables[0].Rows.Count > 0)
            //////        {
            //////            if (ds2.Tables[0].Rows[0]["Ledgername"].ToString() != "")
            //////            {
            //////                dss2.Merge(ds2);
            //////                dsss1.Merge(ds2);
            //////            }
            //////        }

            //////    }
            //////}


            string branch1 = ddloutlet.SelectedValue;
            if (branch1 != "All")
            {
                DataSet dledgername = objBs.getledgerforprofitloss("10", ddloutlet.SelectedValue);
                if (dledgername.Tables[0].Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Profit and Loss Ledger Not Exists!!!');", true);
                    return;
                }

                DataSet ds1 = new DataSet();
                DataTable dt = new DataTable();

                DataColumn dc;
                DataRow dr11;

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

                double debititot = 0;
                double crdittot = 0;
                double overall = 0;
                int ii = 0;

                pnlist(branch1); pnlist1g(branch1);

                pnlist1(branch1);


                dr11 = dt.NewRow();
                dr11["LedgerName"] = dledgername.Tables[0].Rows[0]["PrintName"].ToString();
                dr11["LedgerID"] = dledgername.Tables[0].Rows[0]["LedgerId"].ToString();

                double doub = (debitTotal + debitTotal1 + debitTotalg) - (creditTotal + creditTotal1 + creditTotalg);

                if (doub > 0)
                {
                    dr11["Debit"] = doub.ToString("f2");
                    dr11["Credit"] = 0;
                }
                else
                {
                    dr11["Debit"] = 0;
                    dr11["Credit"] = Convert.ToDouble(-doub).ToString("f2");
                }

                dr11["Folionumber"] = "";
                dr11["Branch"] = bnch;
                dt.Rows.Add(dr11);
                ds1.Tables.Add(dt);
                if (ds1.Tables[0].Rows.Count > 0)
                {

                    {
                        dss.Merge(ds1);
                    }
                }

                //}

            }
            else
            {
                DataSet ds1 = objBs.selectBranch();
                DataSet ds11 = new DataSet();
                DataTable dt = new DataTable();

                DataColumn dc;
                DataRow dr11;

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
                ds11.Tables.Add(dt);
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    string sbranch = ds1.Tables[0].Rows[i]["Branchcode"].ToString();
                    string scity = ds1.Tables[0].Rows[i]["City"].ToString();

                    DataSet dledgername = objBs.getledgerforprofitloss1("10", sbranch);
                    if (dledgername.Tables[0].Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Profit and Loss Ledger Not Exists!!!');", true);
                        return;
                    }

                    

                    double debititot = 0;
                    double crdittot = 0;
                    double overall = 0;
                    int ii = 0;

                    pnlist(sbranch); pnlist1g(sbranch);
                    pnlist1(sbranch);


                    dr11 = dt.NewRow();
                    dr11["LedgerName"] = dledgername.Tables[0].Rows[0]["PrintName"].ToString();
                    dr11["LedgerID"] = dledgername.Tables[0].Rows[0]["LedgerId"].ToString();

                    double doub = (debitTotal + debitTotal1 + debitTotalg) - (creditTotal + creditTotal1 + creditTotalg);

                    if (doub > 0)
                    {
                        dr11["Debit"] = doub;
                        dr11["Credit"] = 0;
                    }
                    else
                    {
                        dr11["Debit"] = 0;
                        dr11["Credit"] = -doub;
                    }

                    dr11["Folionumber"] = "";
                    dr11["Branch"] = bnch;
                    ds11.Tables[0].Rows.Add(dr11);

                    debitTotal = 0; debitTotal1 = 0; debitTotalg = 0; creditTotal = 0; creditTotal1 = 0; creditTotalg = 0;
                } if (ds11.Tables[0].Rows.Count > 0)
                {

                    {
                        dss.Merge(ds11);
                    }
                }
            }

            //}
            if (dss.Tables.Count > 0)
            {
                //for (int i = 0; i < dss.Tables[0].Rows.Count; i++)
                //{
                //    if (dss.Tables[0].Rows[i]["Ledgername"].ToString() == "Transfer Tenkasi" || dss.Tables[0].Rows[i]["Ledgername"].ToString() == "Transfer Chennai" || dss.Tables[0].Rows[i]["Ledgername"].ToString() == "Transfer Coimbatore")
                //    {
                      
                //            dss.Tables[0].Rows[i].Delete();
                //    }
                //}
                //dss.Tables[0].AcceptChanges();
                DataView dv = dss.Tables[0].DefaultView;
                dv.Sort = "LedgerName asc";
                DataSet dnew = new DataSet();
                dnew.Merge(dv.ToTable());
                dnew.Merge(dss1);
                gvLiaLedger.DataSource = dnew;
                gvLiaLedger.DataBind();
            }
            else
            {
                gvLiaLedger.DataSource = null;
                gvLiaLedger.DataBind();
            }
            //gvTrailBalance.DataSource = grdDs;
            // gvTrailBalance.DataBind();

            idt.Visible = true;
        }

        public void pnlist(string branch)
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

                        debitSum = objBs.GetDebitSum(groupID, startDate, endDate, branch);
                        creditSum = objBs.GetCreditSum(groupID, startDate, endDate, branch);

                        TrailFlag = Convert.ToString(mainRow["TrailBalance"]);
                        strParticulars = Convert.ToString(mainRow["GroupName"]);
                        if (TrailFlag == "Debit")
                        {
                            totalSum = debitSum - creditSum;
                            strDebit = Convert.ToString(totalSum.ToString("f2"));
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
                            strCredit = Convert.ToString(totalSum.ToString("f2"));
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


                    DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, branch);
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
                    DataSet dledgername = objBs.selectClosing(branch);
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
                gvLiaLedger1.DataSource = dss;
                gvLiaLedger1.DataBind();
            }
            else
            {
                gvLiaLedger1.DataSource = null;
                gvLiaLedger1.DataBind();
            }
            //gvTrailBalance.DataSource = grdDs;
            // gvTrailBalance.DataBind();

            //idt.Visible = true;
        }
        public void pnlist1(string branch)
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

                        debitSum = objBs.GetDebitSum(groupID, startDate, endDate, branch);
                        creditSum = objBs.GetCreditSum(groupID, startDate, endDate, branch);

                        TrailFlag = Convert.ToString(mainRow["TrailBalance"]);
                        strParticulars = Convert.ToString(mainRow["GroupName"]);
                        if (TrailFlag == "Debit")
                        {
                            totalSum = debitSum - creditSum;
                            strDebit = Convert.ToString(totalSum.ToString("f2"));
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
                            strCredit = Convert.ToString(totalSum.ToString("f2"));
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


                    DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, branch);
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
        public void pnlist1g(string branch)
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

                        debitSum = objBs.GetDebitSum(groupID, startDate, endDate, branch);
                        creditSum = objBs.GetCreditSum(groupID, startDate, endDate, branch);

                        TrailFlag = Convert.ToString(mainRow["TrailBalance"]);
                        strParticulars = Convert.ToString(mainRow["GroupName"]);
                        if (TrailFlag == "Debit")
                        {
                            totalSum = debitSum - creditSum;
                            strDebit = Convert.ToString(totalSum.ToString("f2"));
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
                            strCredit = Convert.ToString(totalSum.ToString("f2"));
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


                    DataSet ds = objBs.getLedgerTransaction(Convert.ToInt32(groupid), startDate, endDate, branch);
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
                GridView2.DataSource = dss;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
            }
            //gvTrailBalance.DataSource = grdDs;
            // gvTrailBalance.DataBind();

            //idt.Visible = true;
        }
        public void gBalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double debit = 0;
            double credit = 0;
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
                {
                    debit = Convert.ToDouble(lblDebit.Text);
                    debitTotaln = debitTotaln + Convert.ToDouble(lblDebit.Text);
                }
                if (lblCredit != null && lblCredit.Text != "")
                {
                    credit = Convert.ToDouble(lblCredit.Text);
                    creditTotaln = creditTotaln + Convert.ToDouble(lblCredit.Text);
                }

                damt = damt + debit;

                camt = camt + credit;

                //lblDebitSum.Text = damt.ToString("f2");
                //lblCreditSum.Text = camt.ToString("f2");

                dDiffamt = damt - camt;
                cDiffamt = camt - damt;
                Label lblBal = (Label)e.Row.FindControl("lbltotal");
                if (dDiffamt >= 0)
                {
                    //lblDebitDiff.Text = dDiffamt.ToString("f2");
                    //lblCreditDiff.Text = "0.00";

                    dDiffamt = dDiffamt + OpBalance;
                    if (dDiffamt >= 0)
                    {
                        lblBal.Text = dDiffamt.ToString("f2") + " BHD";
                        lblBal.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        lblBal.Text = Math.Abs(dDiffamt).ToString("f2") + " BHD";
                        lblBal.ForeColor = System.Drawing.Color.Red;
                    }


                }
                if (cDiffamt > 0)
                {
                    //lblDebitDiff.Text = "0.00";
                    //lblCreditDiff.Text = cDiffamt.ToString("f2");

                    cDiffamt = cDiffamt - OpBalance;
                    if (cDiffamt > 0)
                    {
                        lblBal.Text = cDiffamt.ToString("f2") + " BHD";
                        lblBal.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lblBal.Text = Math.Abs(cDiffamt).ToString("f2") + " BHD";
                        lblBal.ForeColor = System.Drawing.Color.Blue;
                    }

                }



            }


            //lblDebitTotal.Text = debitTotal.ToString("f2");
            //lblCreditTotal.Text = creditTotal.ToString("f2");

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{


            //}

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[1].Text = debitTotaln.ToString("f2");
                e.Row.Cells[2].Text = creditTotaln.ToString("f2");
                double doub = debitTotaln - creditTotaln;
                e.Row.Cells[3].Text = doub.ToString("f2");
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
            }

        }

        public void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            double debit = 0;
            double credit = 0;
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
                {
                    debit = Convert.ToDouble(lblDebit.Text);
                    debitTotalg = debitTotalg + Convert.ToDouble(lblDebit.Text);
                }
                if (lblCredit != null && lblCredit.Text != "")
                {
                    credit = Convert.ToDouble(lblCredit.Text);
                    creditTotalg = creditTotalg + Convert.ToDouble(lblCredit.Text);
                }


            }


            else if (e.Row.RowType == DataControlRowType.Footer)
            {
               
            }

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            gvLiaLedger.PagerSettings.Visible = false;
            ////Table1.PagerSettings.Visible = false;
            //  GridView1.DataBind();

            //StringWriter sw1 = new StringWriter();
            //HtmlTextWriter hw1 = new HtmlTextWriter(sw1);
            //Table1.RenderControl(hw1);
            //string tbl = sw1.ToString().Replace("\"", "'")
            //    .Replace(System.Environment.NewLine, "");
            //StringBuilder sb1 = new StringBuilder();
            //sb1.Append("<script type = 'text/javascript'>");
            //sb1.Append("window.onload = new function(){");
            //sb1.Append("var printWin = window.open('', '', 'left=0");
            //sb1.Append(",top=0,width=1000,height=600,status=0');");
            ////  sb.Append("test1");
            //sb1.Append("printWin.document.write(\"");
            //sb1.Append("APM MOTORS </br>");
            //sb1.Append("Trail Balance Type2 </br>");
            //sb1.Append(" </br></br>");
            //sb1.Append("</br>");

            //sb1.Append(tbl);
            //sb1.Append("\");");
            //sb1.Append("printWin.document.close();");
            //sb1.Append("printWin.focus();");
            //sb1.Append("printWin.print();");
            //sb1.Append("printWin.close();};");
            //sb1.Append("</script>");
            //ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb1.ToString());
            ////Table1.PagerSettings.Visible = true;

            StringWriter sw1 = new StringWriter();
            HtmlTextWriter hw1 = new HtmlTextWriter(sw1);

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            Table1.RenderControl(hw1);

            gvLiaLedger.RenderControl(hw);
            string gridtbl = sw1.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            //  sb.Append("test1");
            sb.Append("printWin.document.write(\"");
            //sb.Append("APM MOTORS </br>");
            sb.Append("Trail Balance Type2 </br>");
            sb.Append(" </br></br>");
            sb.Append("</br>");
            sb.Append(gridtbl);
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvLiaLedger.PagerSettings.Visible = true;
        }
        public DataTable GenerateDs(string GroupName, string strParticulars, string strDebit, string strCredit, string iGroupID)
        {
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
        protected void btnreport_Click(object sender, EventArgs e)
        {
            double sumTotal = 0;
            double dTot = 0;
            double assTot = 0;
            double liaTot = 0;

            //DateTime startDate, endDate;

            //startDate = Convert.ToDateTime(txtfrmdate.Text);
            //endDate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            sheetlist();
            idt.Visible = true;
            DataSet assetDs = GetBalanceSheet("Asset");
            DataSet liabilityDs = GetBalanceSheet("Liability");

            if (assetDs != null)
            {
                if (assetDs.Tables[0].Rows.Count > 0)
                {
                    assetDs.Tables[0].Rows[0].Delete();
                    gvAssetBalance.DataSource = assetDs;
                    gvAssetBalance.DataBind();
                    foreach (DataRow sumDr in assetDs.Tables[0].Rows)
                    {
                        assTot = assTot + Convert.ToDouble(sumDr["sum"]);
                    }
                }
            }
            if (liabilityDs != null)
            {
                if (liabilityDs.Tables[0].Rows.Count > 0)
                {
                    liabilityDs.Tables[0].Rows[0].Delete();
                    gvLiabilityBalance.DataSource = liabilityDs;
                    gvLiabilityBalance.DataBind();
                    foreach (DataRow sumCr in liabilityDs.Tables[0].Rows)
                    {
                        liaTot = liaTot + Convert.ToDouble(sumCr["sum"]);
                    }
                }
            }

            #region PL

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

            openingStockTotal = objBs.GetPurchaseOpValue(startDate, ddloutlet.SelectedValue);
            salesReturnTot = objBs.GetSalesReturnBetween(startDate, endDate, ddloutlet.SelectedValue);
            //closingstockTotal = openingStockTotal + (objBs.GetPurchaseBetween(startDate, endDate, ddloutlet.SelectedValue) - sumNet) + salesReturnTot;
            closingstockTotal = (objBs.GetPurchaseBetween(startDate, endDate, ddloutlet.SelectedValue)) + salesReturnTot;

            purchaseTot = objBs.GetPurchaseBetween(startDate, endDate, ddloutlet.SelectedValue);

            purchaseReturnTot = objBs.GetPurchaseReturnBetween(startDate, endDate, ddloutlet.SelectedValue);
            salesTot = objBs.GetSalesBetween(startDate, endDate, ddloutlet.SelectedValue);


            DExpensesTot = objBs.GetExpenseIncomeTotal(startDate, endDate, "DX", ddloutlet.SelectedValue);
            IDExpensesTot = objBs.GetExpenseIncomeTotal(startDate, endDate, "IDX", ddloutlet.SelectedValue);
            DIncomeTot = objBs.GetExpenseIncomeTotal(startDate, endDate, "", ddloutlet.SelectedValue);
            IDIncomeTot = objBs.GetExpenseIncomeTotal(startDate, endDate, "IDI", ddloutlet.SelectedValue);

            gPurchase = openingStockTotal + (purchaseTot - purchaseReturnTot);
            gSales = closingstockTotal + (salesTot - salesReturnTot);

            totExpense = gPurchase + (DExpensesTot);

            totIncome = gSales + (DIncomeTot);
            grProfitLoss = totIncome - totExpense;


            if (grProfitLoss > 0)
                dGp = totIncome - totExpense;
            else
                dGl = totExpense - totIncome;


            //lblGP.Text = dGp.ToString("f2");
            //lblGL.Text = dGl.ToString("f2");

            //lblGrossProfitBF.Text = dGp.ToString("f2");
            //lblGrossLossBF.Text = dGl.ToString("f2");
            double captot = 0;
            DataSet capitalDs = GetBalanceSheetCapital("Liability","Capital");
            if (capitalDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow sumDr in capitalDs.Tables[0].Rows)
                {
                    if (sumDr["sum"].ToString() != "")
                    {
                        captot = captot + Convert.ToDouble(sumDr["sum"]);
                    }
                }
            }

            if (dGp > 0)
            {
                netProfitLoss = dGp + (IDIncomeTot - IDExpensesTot);
                if (netProfitLoss > 0)
                {
                    lblNetProfit.Text = "Capital + NetProfit: " + (captot + netProfitLoss).ToString("f2");
                    lblNetLoss.Text = "";
                    lblNetLoss.Visible = false;
                    liaTot = liaTot + (captot + netProfitLoss);
                }
                else
                {
                    lblNetLoss.Text = "Capital - NetLoss: " + (captot - netProfitLoss).ToString("f2");
                    lblNetProfit.Text = "";
                    lblNetProfit.Visible = false;
                    liaTot = liaTot + (captot-netProfitLoss);
                }
            }
            else
            {
                netProfitLoss = dGl + (IDExpensesTot - IDIncomeTot);
                if (netProfitLoss > 0)
                {
                    lblNetLoss.Text = "Capital - NetLoss: " + (captot - netProfitLoss).ToString("f2");
                    lblNetProfit.Visible = false;
                    liaTot = liaTot + (captot - netProfitLoss);
                }
                else
                {
                    lblNetProfit.Text = "Capital + NetProfit: " + (captot + netProfitLoss).ToString("f2");
                    lblNetLoss.Visible = false;
                    liaTot = liaTot + (captot + netProfitLoss);
                }
            }
            #endregion


            sumTotal = assTot - liaTot;
            if (sumTotal > 0)
            {
                pnlLib.Visible = true;
                pnlAst.Visible = false;
                sumTotal = Math.Abs(sumTotal);
                lblLib.Text = sumTotal.ToString("f2");
                dTot = liaTot + sumTotal;
                lblCreditTotal.Text = dTot.ToString("f2");
                lblDebitTotal.Text = assTot.ToString("f2");
            }
            else
            {
                pnlLib.Visible = false;
                pnlAst.Visible = true;
                sumTotal = Math.Abs(sumTotal);
                lblAst.Text = sumTotal.ToString("f2");
                dTot = assTot + sumTotal;
                lblDebitTotal.Text = dTot.ToString("f2");
                lblCreditTotal.Text = liaTot.ToString("f2");
            }
        }

        public DataSet GetBalanceSheet(string btype)
        {

            DataSet mainDs = new DataSet();
            DataSet GroupDs = new DataSet();
            DataTable grdDt = new DataTable();
            DataTable dtNew = new DataTable();

            //DateTime startDate, endDate;

            //startDate = Convert.ToDateTime(txtfrmdate.Text);
            //endDate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet grdDs = new DataSet();

            Double debitSum = 0;
            Double creditSum = 0;
            Double totalSum = 0;
            Double netSum = 0;
            string sHeading = string.Empty;
            int iHeading = 0;
            int groupID = 0;

            dtNew = GenerateDs("", "", "");
            grdDs.Tables.Add(dtNew);

            mainDs = objBs.GetSheetHeadings(btype);
            if (mainDs != null)
            {
                if (mainDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mainRow in mainDs.Tables[0].Rows)
                    {
                        netSum = 0;
                        totalSum = 0;
                        debitSum = 0;
                        creditSum = 0;
                        if (mainRow["Heading"] != null)
                        {
                            sHeading = Convert.ToString(mainRow["Heading"]);
                        }
                        if (mainRow["HeadingID"] != null)
                        {
                            iHeading = Convert.ToInt32(mainRow["HeadingID"]);
                            GroupDs = objBs.GetGroupsForHeadiing(iHeading);
                            if (GroupDs != null)
                            {
                                if (GroupDs.Tables[0].Rows.Count > 0)
                                {


                                    foreach (DataRow groupRow in GroupDs.Tables[0].Rows)
                                    {
                                        if (groupRow["GroupID"] != null)
                                        {
                                            groupID = Convert.ToInt32(groupRow["GroupID"]);

                                            debitSum = objBs.GetDebitSum(groupID, startDate, endDate, ddloutlet.SelectedValue);
                                            creditSum = objBs.GetCreditSum(groupID, startDate, endDate, ddloutlet.SelectedValue);

                                            if (btype == "Asset")
                                            {
                                                totalSum = debitSum - creditSum;
                                            }
                                            else
                                            {
                                                totalSum = creditSum - debitSum;
                                            }

                                        }


                                        netSum = netSum + totalSum;

                                    }


                                }
                            }

                            grdDt = GenerateDs(iHeading.ToString(), sHeading, netSum.ToString("f2"));

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
            }

            return grdDs;
        }

        public DataSet GetBalanceSheetCapital(string btype,string head)
        {

            DataSet mainDs = new DataSet();
            DataSet GroupDs = new DataSet();
            DataTable grdDt = new DataTable();
            DataTable dtNew = new DataTable();

            //DateTime startDate, endDate;

            //startDate = Convert.ToDateTime(txtfrmdate.Text);
            //endDate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            DataSet grdDs = new DataSet();

            Double debitSum = 0;
            Double creditSum = 0;
            Double totalSum = 0;
            Double netSum = 0;
            string sHeading = string.Empty;
            int iHeading = 0;
            int groupID = 0;

            dtNew = GenerateDs("", "", "");
            grdDs.Tables.Add(dtNew);

            mainDs = objBs.GetSheetHeadingsCapital(btype,head);
            if (mainDs != null)
            {
                if (mainDs.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow mainRow in mainDs.Tables[0].Rows)
                    {
                        netSum = 0;
                        totalSum = 0;
                        debitSum = 0;
                        creditSum = 0;
                        if (mainRow["Heading"] != null)
                        {
                            sHeading = Convert.ToString(mainRow["Heading"]);
                        }
                        if (mainRow["HeadingID"] != null)
                        {
                            iHeading = Convert.ToInt32(mainRow["HeadingID"]);
                            GroupDs = objBs.GetGroupsForHeadiing(iHeading);
                            if (GroupDs != null)
                            {
                                if (GroupDs.Tables[0].Rows.Count > 0)
                                {


                                    foreach (DataRow groupRow in GroupDs.Tables[0].Rows)
                                    {
                                        if (groupRow["GroupID"] != null)
                                        {
                                            groupID = Convert.ToInt32(groupRow["GroupID"]);

                                            debitSum = objBs.GetDebitSum(groupID, startDate, endDate, ddloutlet.SelectedValue);
                                            creditSum = objBs.GetCreditSum(groupID, startDate, endDate, ddloutlet.SelectedValue);

                                            if (btype == "Asset")
                                            {
                                                totalSum = debitSum - creditSum;
                                            }
                                            else
                                            {
                                                totalSum = creditSum - debitSum;
                                            }

                                        }


                                        netSum = netSum + totalSum;

                                    }


                                }
                            }

                            grdDt = GenerateDs(iHeading.ToString(), sHeading, netSum.ToString("f2"));

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
            }

            return grdDs;
        }


        public DataTable GenerateDs(string headingID, string HeadingName, string strSum)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            DataColumn dc;
            DataRow dr;

            dc = new DataColumn("HeadingID");
            dt.Columns.Add(dc);

            dc = new DataColumn("HeadingName");
            dt.Columns.Add(dc);

            dc = new DataColumn("sum");
            dt.Columns.Add(dc);


            dr = dt.NewRow();
            dr["HeadingID"] = headingID;
            dr["HeadingName"] = HeadingName;

            dr["sum"] = strSum;

            dt.Rows.Add(dr);

            return dt;
        }

        public void gvAssetBalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSum = (Label)e.Row.FindControl("lblSum");

                if (lblSum != null && lblSum.Text != "")
                    debitTotal = debitTotal + Convert.ToDouble(lblSum.Text);


                lblDebitTotal.Text = debitTotal.ToString("f2");

                GridView gv = e.Row.FindControl("gvAssetGroup") as GridView;
                if (gvAssetBalance.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int headID = Convert.ToInt32(gvAssetBalance.DataKeys[e.Row.RowIndex].Value);

                    DataSet ds = GetBalanceSheet2(headID);
                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ds.Tables[0].Rows[0].Delete();
                            gv.DataSource = ds;
                            gv.DataBind();
                        }
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = debitTotal.ToString("f2");

            }
        }

        public void gvLiabilityBalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblSum = (Label)e.Row.FindControl("lblSum");

                if (lblSum != null && lblSum.Text != "")
                    creditTotal = creditTotal + Convert.ToDouble(lblSum.Text);


                lblCreditTotal.Text = creditTotal.ToString("f2");

                GridView gv = e.Row.FindControl("gvLiaGroup") as GridView;
                if (gvLiabilityBalance.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int headID = Convert.ToInt32(gvLiabilityBalance.DataKeys[e.Row.RowIndex].Value);

                    DataSet ds = GetBalanceSheet2(headID);
                    if (ds != null)
                    {
                        ds.Tables[0].Rows[0].Delete();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gv.DataSource = ds;
                            gv.DataBind();
                        }
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = creditTotal.ToString("f2");
            }

        }

        public DataSet GetBalanceSheet2(int HeadingID)
        {

            DataSet mainDs = new DataSet();
            DataSet GroupDs = new DataSet();
            DataTable grdDt = new DataTable();
            DataTable dtNew = new DataTable();

            DataSet grdDs = new DataSet();

            Double debitSum = 0;
            Double creditSum = 0;
            Double totalSum = 0;
            Double netSum = 0;
            string sGroup = string.Empty;
            int iHeading = HeadingID;
            int groupID = 0;

            dtNew = GenerateDs2("", "", "");
            grdDs.Tables.Add(dtNew);

            //DateTime startDate, endDate;

            //startDate = Convert.ToDateTime(txtfrmdate.Text);
            //endDate = Convert.ToDateTime(txttodate.Text);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            GroupDs = objBs.GetGroupsForHeadiing(iHeading);
            if (GroupDs != null)
            {
                if (GroupDs.Tables[0].Rows.Count > 0)
                {


                    foreach (DataRow groupRow in GroupDs.Tables[0].Rows)
                    {
                        if (groupRow["GroupID"] != null)
                        {
                            groupID = Convert.ToInt32(groupRow["GroupID"]);
                            debitSum = objBs.GetDebitSum(groupID, startDate, endDate, ddloutlet.SelectedValue);
                            creditSum = objBs.GetCreditSum(groupID, startDate, endDate, ddloutlet.SelectedValue);

                            totalSum = debitSum - creditSum;


                        }
                        if (groupRow["GroupName"] != null)
                        {
                            sGroup = groupRow["GroupName"].ToString();
                        }

                        if (totalSum < 0)
                            totalSum = Math.Abs(totalSum);
                        netSum = netSum + totalSum;
                        grdDt = GenerateDs2(groupID.ToString(), sGroup, totalSum.ToString("f2"));
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


            return grdDs;
        }


        public DataTable GenerateDs2(string groupID, string GroupName, string strSum)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            DataColumn dc;
            DataRow dr;

            dc = new DataColumn("GroupID");
            dt.Columns.Add(dc);

            dc = new DataColumn("GroupName");
            dt.Columns.Add(dc);

            dc = new DataColumn("sum");
            dt.Columns.Add(dc);


            dr = dt.NewRow();
            dr["GroupID"] = groupID;
            dr["GroupName"] = GroupName;

            dr["sum"] = strSum;

            dt.Rows.Add(dr);

            return dt;
        }

        public void gvLiaGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //DateTime startDate, endDate;

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);


            //startDate = Convert.ToDateTime(txtfrmdate.Text);
            //endDate = Convert.ToDateTime(txttodate.Text);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSum = (Label)e.Row.FindControl("lblSum");

                if (lblSum != null && lblSum.Text != "")
                    debitTotal = debitTotal + Convert.ToDouble(lblSum.Text);

            }
            if (debitTotal > 0)
                lblDebitTotal.Text = debitTotal.ToString("f2");
            else
            {
                debitTotal = Math.Abs(debitTotal);
                lblDebitTotal.Text = debitTotal.ToString("f2");
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gv = e.Row.FindControl("gvLiaLedger") as GridView;
                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);
                    DataSet ds = objBs.getLedgerTransaction(groupID, startDate, endDate, ddloutlet.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        gv.DataBind();
                    }
                }

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[1].Text = debitTotal.ToString("f2");

            }

        }

        public void gvAssetGroup_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //DateTime startDate, endDate;

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //startDate = Convert.ToDateTime(txtfrmdate.Text);
            //endDate = Convert.ToDateTime(txttodate.Text);

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSum = (Label)e.Row.FindControl("lblSum");

                if (lblSum != null && lblSum.Text != "")
                    debitTotal = debitTotal + Convert.ToDouble(lblSum.Text);


                if (debitTotal > 0)
                    lblDebitTotal.Text = debitTotal.ToString("f2");
                else
                {
                    debitTotal = Math.Abs(debitTotal);
                    lblDebitTotal.Text = debitTotal.ToString("f2");
                }

                GridView gv = e.Row.FindControl("gvAssetLedger") as GridView;
                GridView gvGroup = (GridView)sender;
                if (gvGroup.DataKeys[e.Row.RowIndex].Value != "")
                {
                    int groupID = Convert.ToInt32(gvGroup.DataKeys[e.Row.RowIndex].Value);

                    DataSet ds = objBs.getLedgerTransaction(groupID, startDate, endDate, ddloutlet.SelectedValue);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gv.DataSource = ds;
                        gv.DataBind();
                    }
                }

            }

        }
        protected void gridPur_RowCreated(object sender, GridViewRowEventArgs e)
        {


            #region 1

            //----------start----------//
            bool IsSubTotalRowNeedToAdd1 = false;
            bool IsGrandTotalRowNeedtoAdd1 = false;
            bool IsTotalRowNeedToAdd1 = false;
            if ((strPreviousRowID1 != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") != null))
                if (strPreviousRowID1 != DataBinder.Eval(e.Row.DataItem, "Date").ToString())
                {
                    IsSubTotalRowNeedToAdd1 = true;
                    IsTotalRowNeedToAdd1 = true;
                }
            if ((strPreviousRowID1 != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") == null))
            {
                IsSubTotalRowNeedToAdd1 = true;
                IsTotalRowNeedToAdd1 = true;
                intSubTotalIndex1 = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID1 == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") != null))
            {
                GridView gridPurchase = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                cell.ColumnSpan = 3;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column            
                cell = new TableCell();
                if (netOp1 > 0)
                {
                    cell.Text = string.Format("{0:0.00}", netOp1);
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                //Adding Unit Price Column          
                cell = new TableCell();
                if (netOp1 < 0)
                {

                    cell.Text = string.Format("{0:0.00}", -(netOp1));
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);

                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                intSubTotalIndex1++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd1)
            {
                #region Adding Sub Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Grand Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 3;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column            
                cell = new TableCell();
                if (dblSubTotalQuantity1 > 0)
                {
                    cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity1);

                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Unit Price Column          
                cell = new TableCell();
                if (dblSubTotalUnitPrice1 > 0)
                {
                    cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice1);
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid      
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                intSubTotalIndex1++;
                #endregion
                #region Adding Next Group Header Details
                //if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                //{
                //    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //    cell = new TableCell();
                //    cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                //    cell.ColumnSpan = 9;
                //    cell.CssClass = "GroupHeaderStyle";
                //    row.Cells.Add(cell);
                //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                //    intSubTotalIndex++;
                //}
                #endregion
                #region Reseting the Sub Total Variables
                //dblSubTotalUnitPrice = 0;
                //dblSubTotalQuantity = 0;
                //dblSubTotalDiscount = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {


                    // Creating a Row      
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //  cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //  cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    //Adding Quantity Column           


                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                    intSubTotalIndex1++;



                }
                #endregion
            }
            if (IsTotalRowNeedToAdd1)
            {
                #region Adding Sub Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Balance";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 3;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                int i = 0;
                double ii = dblSubTotalQuantity1 - dblSubTotalUnitPrice1;

                //Adding Quantity Column            
                cell = new TableCell();
                if (ii > 0)
                {
                    opday = ii;
                    cell.Text = string.Format("{0:0.00}", ii);
                    i = 1;
                    dblSubTotalQuantity1 = ii;
                    dblSubTotalUnitPrice1 = 0;
                }
                else
                {
                    opday = ii;
                    if (ii < 0)
                    {
                        ii = -(ii);
                    }
                    cell.Text = string.Format("{0:0.00}", "");
                    i = 0;
                    dblSubTotalQuantity1 = 0;
                    dblSubTotalUnitPrice1 = ii;
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                //Adding Unit Price Column          
                cell = new TableCell();
                if (i == 1)
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", ii);
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid      
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                intSubTotalIndex1++;
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {


                    // Creating a Row      
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    ///cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    //Adding Quantity Column           


                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                    intSubTotalIndex1++;



                }
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);

                    //Adding Quantity Column            
                    cell = new TableCell();
                    if (i == 1)
                    {
                        cell.Text = string.Format("{0:0.00}", ii);
                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    //Adding Unit Price Column          
                    cell = new TableCell();
                    if (i == 0)
                    {
                        cell.Text = string.Format("{0:0.00}", ii);
                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);

                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                    intSubTotalIndex1++;
                }
                else
                {
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                }
                #endregion
                #region Reseting the Sub Total Variables
                //dblSubTotalUnitPrice = 0;
                //dblSubTotalQuantity = 0;

                #endregion
            }
            if (IsSubTotalRowNeedToAdd1 == false && IsTotalRowNeedToAdd1 == false)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {

                    GridView gridPurchase = (GridView)sender;
                    // Creating a Row      
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    TableCell cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //  cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    cell = new TableCell();

                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    // cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    cell = new TableCell();

                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    //Adding Quantity Column           


                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                    intSubTotalIndex1++;
                }
            }
            if (IsGrandTotalRowNeedtoAdd1)
            {
                #region Grand Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                cell.Text = "";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 6;
                cell.CssClass = "GrandTotalRowStyle1";
                row.Cells.Add(cell);

                //Adding Quantity Column           


                //Adding the Row at the RowIndex position in the Grid 
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex1, row);
                intSubTotalIndex1++;

                #endregion
            }

            #endregion
        }
        protected void gvLed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID1 = DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                double dblQuantity1 = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Debit").ToString() == "")
                {

                    dblQuantity1 = 0;

                }
                else
                {

                    dblQuantity1 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit").ToString());
                }
                double dblUnitPrice1 = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Credit").ToString() == "")
                {
                    dblUnitPrice1 = 0;
                }
                else
                {
                    dblUnitPrice1 = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit").ToString());
                }
                dblSubTotalUnitPrice1 += dblUnitPrice1;
                dblSubTotalQuantity1 += dblQuantity1;
                dblGrandTotalUnitPrice1 += dblUnitPrice1;
                dblGrandTotalQuantity1 += dblQuantity1;

            }
        }
        protected void gvLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                double dblQuantity = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Debit").ToString() == "")
                {

                    dblQuantity = 0;

                }
                else
                {

                    dblQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Debit").ToString());
                }
                double dblUnitPrice = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Credit").ToString() == "")
                {
                    dblUnitPrice = 0;
                }
                else
                {
                    dblUnitPrice = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Credit").ToString());
                }
                dblSubTotalUnitPrice += dblUnitPrice;
                dblSubTotalQuantity += dblQuantity;
                dblGrandTotalUnitPrice += dblUnitPrice;
                dblGrandTotalQuantity += dblQuantity;

            }
        }
        //public DataTable GenerateDs(string GroupName, string strParticulars, string strDebit, string strCredit, string iGroupID)
        //{
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();

        //    DataColumn dc;
        //    DataRow dr;

        //    dc = new DataColumn("Particulars");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("Debit");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("Credit");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("GroupID");
        //    dt.Columns.Add(dc);

        //    dc = new DataColumn("GroupName");
        //    dt.Columns.Add(dc);

        //    dr = dt.NewRow();
        //    dr["Particulars"] = strParticulars;

        //    dr["Debit"] = strDebit;

        //    dr["Credit"] = strCredit;

        //    dr["GroupID"] = iGroupID;
        //    dr["GroupName"] = GroupName;
        //    dt.Rows.Add(dr);

        //    return dt;
        //}

        protected void gridPurchase_RowCreated(object sender, GridViewRowEventArgs e)
        {


            #region 1

            //----------start----------//
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            bool IsTotalRowNeedToAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "Date").ToString())
                {
                    IsSubTotalRowNeedToAdd = true;
                    IsTotalRowNeedToAdd = true;
                }
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsTotalRowNeedToAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "Date") != null))
            {
                GridView gridPurchase = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                cell.ColumnSpan = 3;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column            
                cell = new TableCell();
                if (netOp > 0)
                {
                    cell.Text = string.Format("{0:0.00}", netOp);
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);
                //Adding Unit Price Column          
                cell = new TableCell();
                if (netOp < 0)
                {

                    cell.Text = string.Format("{0:0.00}", -(netOp));
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GroupHeaderStyle";
                row.Cells.Add(cell);

                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
            }
            #endregion
            if (IsSubTotalRowNeedToAdd)
            {
                #region Adding Sub Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Grand Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 3;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column            
                cell = new TableCell();
                if (dblSubTotalQuantity > 0)
                {
                    cell.Text = string.Format("{0:0.00}", dblSubTotalQuantity);

                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Unit Price Column          
                cell = new TableCell();
                if (dblSubTotalUnitPrice > 0)
                {
                    cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice);
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid      
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                //if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                //{
                //    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //    cell = new TableCell();
                //    cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                //    cell.ColumnSpan = 9;
                //    cell.CssClass = "GroupHeaderStyle";
                //    row.Cells.Add(cell);
                //    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                //    intSubTotalIndex++;
                //}
                #endregion
                #region Reseting the Sub Total Variables
                //dblSubTotalUnitPrice = 0;
                //dblSubTotalQuantity = 0;
                //dblSubTotalDiscount = 0;
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {


                    // Creating a Row      
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //  cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //  cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    //Adding Quantity Column           


                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;



                }
                #endregion
            }
            if (IsTotalRowNeedToAdd)
            {
                #region Adding Sub Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row          
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell          
                TableCell cell = new TableCell();
                cell.Text = "Balance";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 3;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                int i = 0;
                double ii = dblSubTotalQuantity - dblSubTotalUnitPrice;

                //Adding Quantity Column            
                cell = new TableCell();
                if (ii > 0)
                {
                    tot = ii;
                    cell.Text = string.Format("{0:0.00}", ii);
                    i = 1;
                    dblSubTotalQuantity = ii;
                    dblSubTotalUnitPrice = 0;
                }
                else
                {
                    tot = ii;
                    if (ii < 0)
                    {
                        ii = -(ii);
                    }
                    cell.Text = string.Format("{0:0.00}", "");
                    i = 0;
                    dblSubTotalQuantity = 0;
                    dblSubTotalUnitPrice = ii;
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                //Adding Unit Price Column          
                cell = new TableCell();
                if (i == 1)
                {
                    cell.Text = string.Format("{0:0.00}", "");
                }
                else
                {
                    cell.Text = string.Format("{0:0.00}", ii);
                }
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid      
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {


                    // Creating a Row      
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    ///cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 3;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    //Adding Quantity Column           


                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;



                }
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "Date : " + DataBinder.Eval(e.Row.DataItem, "Date").ToString();
                    cell.ColumnSpan = 3;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);

                    //Adding Quantity Column            
                    cell = new TableCell();
                    if (i == 1)
                    {
                        cell.Text = string.Format("{0:0.00}", ii);
                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    //Adding Unit Price Column          
                    cell = new TableCell();
                    if (i == 0)
                    {
                        cell.Text = string.Format("{0:0.00}", ii);
                    }
                    else
                    {
                        cell.Text = string.Format("{0:0.00}", "");
                    }
                    cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);

                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                else
                {
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                }
                #endregion
                #region Reseting the Sub Total Variables
                //dblSubTotalUnitPrice = 0;
                //dblSubTotalQuantity = 0;

                #endregion
            }
            if (IsSubTotalRowNeedToAdd == false && IsTotalRowNeedToAdd == false)
            {
                if (DataBinder.Eval(e.Row.DataItem, "Date") != null)
                {

                    GridView gridPurchase = (GridView)sender;
                    // Creating a Row      
                    GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    //Adding Total Cell           
                    TableCell cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //  cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    cell = new TableCell();

                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    // cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    cell = new TableCell();

                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);

                    cell = new TableCell();
                    cell.Text = "";
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    //cell.ColumnSpan = 6;
                    cell.CssClass = "GrandTotalRowStyle1";
                    row.Cells.Add(cell);
                    //Adding Quantity Column           


                    //Adding the Row at the RowIndex position in the Grid 
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                #region Grand Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                cell.Text = "";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 6;
                cell.CssClass = "GrandTotalRowStyle1";
                row.Cells.Add(cell);

                //Adding Quantity Column           


                //Adding the Row at the RowIndex position in the Grid 
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;

                #endregion
            }

            #endregion
        }

        protected void Calculate()
        {
            string Branch = ddloutlet.SelectedValue;
            DateTime stdt = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime etdt = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string varib = "01/04/2018"; DateTime stdt1 = DateTime.ParseExact(varib, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime todate = stdt.AddDays(-1);

            DateTime startDate = DateTime.ParseExact(txtfrmdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataSet dsday = objBs.generateDayBook2(stdt1, todate, Branch);

            //double obaldr = objbs.getLedgerOpeningBalanceday(0, "debit", Branch);
            //double obalcr = objbs.getLedgerOpeningBalanceday(0, "credit", Branch);

            double obaldr1 = objBs.getLedgerOpBalancedaynew(0, "debit", Branch, "Cash A/C _" + Branch);
            double obalcr1 = objBs.getLedgerOpBalancedaynew(0, "credit", Branch, "Cash A/C _" + Branch);

            if (dsday.Tables[0].Rows.Count > 0)
            {


                foreach (DataRow dday in dsday.Tables[0].Rows)
                {
                    if (dday["Debit"] != "")
                    {
                        opDr1 = opDr1 + Convert.ToDouble(dday["Debit"]);
                    }

                    if (dday["Credit"] != "")
                    {
                        opCr1 = opCr1 + Convert.ToDouble(dday["Credit"]);
                    }

                }
            }

            opCr1 = opCr1 + obalcr1;
            opDr1 = opDr1 + obaldr1;

            netOp1 = opDr1 - opCr1;

            if (netOp1 > 0)
            {
                dblSubTotalQuantity1 = dblSubTotalQuantity1 + netOp1;

            }
            else
            {
                dblSubTotalUnitPrice1 = dblSubTotalUnitPrice1 + (-(netOp1));
            }



            //opCr = objBs.getOpening(0, 0, 0, "credit", startDate, ddloutlet.SelectedValue);
            //opDr = objBs.getOpening(0, 0, 0, "debit", startDate, ddloutlet.SelectedValue);


            //if (opDr > opCr)
            //{
            //    netOp = opDr - opCr;
            //    //lblOBDR.Text = netOp.ToString("f2");
            //    //lblOBCR.Text = "0.00";
            //}
            //else
            //{
            //    netOp = opCr - opDr;
            //    //lblOBDR.Text = "0.00";
            //    //lblOBCR.Text = netOp.ToString("f2");
            //}


            DataSet ds = objBs.generateDayBook(stdt1, todate, Branch);

            DataSet dstd = new DataSet();

            if (ds != null)
            {
                DataTable dt;
                DataRow drNew;
                DataColumn dc;

                dt = new DataTable();
                dc = new DataColumn("Date");
                dt.Columns.Add(dc);

                dc = new DataColumn("Branchcode");
                dt.Columns.Add(dc);

                dc = new DataColumn("Particulars");
                dt.Columns.Add(dc);

                dc = new DataColumn("Narration");
                dt.Columns.Add(dc);

                dc = new DataColumn("Debit");
                dt.Columns.Add(dc);

                dc = new DataColumn("Credit");
                dt.Columns.Add(dc);

                dstd.Tables.Add(dt);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (dr["type"].ToString() == "Sales" || dr["type"].ToString() == "Purchase")
                        {
                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            if (dr["Debit"] != "")
                            {
                                drNew["Debit"] = Convert.ToDouble(dr["Debit"]).ToString("f2");
                            }
                            else
                            {
                                drNew["Debit"] = "";

                            }
                            drNew["Credit"] = "";
                            drNew["Particulars"] = dr["Debitor"].ToString();
                            if (dr["Debitor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            drNew["Debit"] = "";
                            if (dr["Credit"] != "")
                            {
                                drNew["Credit"] = Convert.ToDouble(dr["Credit"]).ToString("f2");
                            }
                            else
                            {
                                drNew["Credit"] = "";

                            }
                            drNew["Particulars"] = dr["Creditor"].ToString();
                            if (dr["Creditor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }
                        }
                        else
                        {
                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            drNew["Debit"] = "";
                            if (dr["Credit"] != "")
                            {
                                drNew["Credit"] = Convert.ToDouble(dr["Credit"]).ToString("f2");
                            }
                            else
                            {
                                drNew["Credit"] = "";
                            }
                            drNew["Particulars"] = dr["Creditor"].ToString();
                            if (dr["Creditor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            drNew = dt.NewRow();
                            drNew["Narration"] = dr["Narration"];
                            drNew["Branchcode"] = dr["Branchcode"];
                            drNew["Date"] = dr["Date"];
                            if (dr["Debit"] != "")
                            {
                                drNew["Debit"] = Convert.ToDouble(dr["Debit"]).ToString("f2");

                            }
                            drNew["Credit"] = "";
                            drNew["Particulars"] = dr["Debitor"].ToString();
                            if (dr["Debitor"].ToString() != "")
                            {
                                dstd.Tables[0].Rows.Add(drNew);
                            }

                        }
                    }
                }
            }


            gvLed.DataSource = dstd;
            gvLed.DataBind();



            //   DateTime stDate;
            ////   stDate = Convert.ToDateTime(txtfromdate.Text.Trim());
            //   BSClass objbs = new BSClass();

            //   DateTime stdt = DateTime.ParseExact(txtfromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //   DateTime etdt = DateTime.ParseExact(txttodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);



            //   string Branch = ddloutlet.SelectedValue;

            //   lblOB.Text = objbs.GetDayBookOB(stdt, Branch).ToString("N2");
        }
    }
}