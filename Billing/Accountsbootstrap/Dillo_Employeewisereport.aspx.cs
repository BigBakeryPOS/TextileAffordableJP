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

namespace Billing.Accountsbootstrap
{
    public partial class Dillo_Employeewisereport : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";
        string Ledgername = "All";
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

        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(this.btnExport);
            if (!IsPostBack)
            {

                DataSet drppEmp = objBs.SelectEmpName();
                if (drppEmp.Tables[0].Rows.Count > 0)
                {
                    drpemp.DataSource = drppEmp;
                    drpemp.DataTextField = "Name";
                    drpemp.DataValueField = "Employee_Id";
                    drpemp.DataBind();
                    drpemp.Items.Insert(0, "Select Employee Name");
                }

                txtstartdate.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
                txtenddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void gridPurchase_RowCreated(object sender, GridViewRowEventArgs e)
        {
            #region 1

            //----------start----------//
            bool IsSubTotalRowNeedToAdd = false;
            bool IsGrandTotalRowNeedtoAdd = false;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "empid") != null))
                if (strPreviousRowID != DataBinder.Eval(e.Row.DataItem, "empid").ToString())
                    IsSubTotalRowNeedToAdd = true;
            if ((strPreviousRowID != string.Empty) && (DataBinder.Eval(e.Row.DataItem, "empid") == null))
            {
                IsSubTotalRowNeedToAdd = true;
                IsGrandTotalRowNeedtoAdd = true;
                intSubTotalIndex = 0;
            }
            #region Inserting first Row and populating fist Group Header details
            if ((strPreviousRowID == string.Empty) && (DataBinder.Eval(e.Row.DataItem, "empid") != null))
            {
                GridView gridPurchase = (GridView)sender;
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                TableCell cell = new TableCell();
                cell.Text = "Employee Name : " + DataBinder.Eval(e.Row.DataItem, "name").ToString();
                cell.ColumnSpan = 6;
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
                cell.Text = "Sub Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 3;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column            
                cell = new TableCell();
                cell.Text = string.Format("{0:0}", dblSubTotalQuantity);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);
             //   Adding Unit Price Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalUnitPrice.ToString("N"));
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);
                //Adding Discount Column         
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblSubTotalDiscount.ToString("N"));
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid      
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                intSubTotalIndex++;
                #endregion
                #region Adding Next Group Header Details
                if (DataBinder.Eval(e.Row.DataItem, "empid") != null)
                {
                    row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                    cell = new TableCell();
                    cell.Text = "Employee Name : " + DataBinder.Eval(e.Row.DataItem, "name").ToString();
                    cell.ColumnSpan = 6;
                    cell.CssClass = "GroupHeaderStyle";
                    row.Cells.Add(cell);
                    gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex + intSubTotalIndex, row);
                    intSubTotalIndex++;
                }
                #endregion
                #region Reseting the Sub Total Variables
                dblSubTotalUnitPrice = 0;
                dblSubTotalQuantity = 0;
                dblSubTotalDiscount = 0;

                #endregion
            }
            if (IsGrandTotalRowNeedtoAdd)
            {
                #region Grand Total Row
                GridView gridPurchase = (GridView)sender;
                // Creating a Row      
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
                //Adding Total Cell           
                TableCell cell = new TableCell();
                cell.Text = "Grand Total";
                cell.HorizontalAlign = HorizontalAlign.Left;
                cell.ColumnSpan = 3;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding Quantity Column           
                cell = new TableCell();
                cell.Text = string.Format("{0:0}", dblGrandTotalQuantity);
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
               // Adding Unit Price Column          
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalUnitPrice.ToString("N"));
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);
                cell = new TableCell();
                cell.Text = string.Format("{0:0.00}", dblGrandTotalDiscount.ToString("N"));
                cell.HorizontalAlign = HorizontalAlign.Right;
                cell.CssClass = "GrandTotalRowStyle";
                row.Cells.Add(cell);

                //Adding the Row at the RowIndex position in the Grid     
                gridPurchase.Controls[0].Controls.AddAt(e.Row.RowIndex, row);
                #endregion
            }

            #endregion
        }

        protected void gridPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                strPreviousRowID = DataBinder.Eval(e.Row.DataItem, "empid").ToString();

                double dblQuantity = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "qty").ToString());
                double dblUnitPrice = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "rate").ToString());
                double dblDiscount = Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "ratee").ToString());
                dblSubTotalUnitPrice += dblUnitPrice;
                dblSubTotalQuantity += dblQuantity;
                dblSubTotalDiscount += dblDiscount;
                dblGrandTotalUnitPrice += dblUnitPrice;
                dblGrandTotalQuantity += dblQuantity;
                dblGrandTotalDiscount += dblDiscount;
            }

        }

        protected void btnsearch_click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataSet dss = new DataSet();

            DataTable dt1 = new DataTable();
            DataSet dss1 = new DataSet();
            if (txtstartdate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Start Date!!!.Thanks you!!!')", true);
                return;
            }
            if (txtenddate.Text == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select End date!!!.Thanks You!!!')", true);
                return;
            }
            if (drpemp.SelectedValue == "Select Employee Name")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name!!!.Thanks You!!!')", true);
                return;
            }
            lblemployeename.Text = drpemp.SelectedItem.Text;
            DataSet ds = objBs.getdatewisedetailedreportforemployeewise(txtstartdate.Text, txtenddate.Text,drpemp.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds != null)
                {
                    gridPurchase.DataSource = ds;
                    gridPurchase.DataBind();

                }
            }
            else
            {
                gridPurchase.DataSource = null;
                gridPurchase.DataBind();

            }



            //For JobWorker

            //DataSet ds1 = objBs.getdatewisesummaryforjobwork(txtstartdate.Text, txtenddate.Text);
            //if (ds1.Tables[0].Rows.Count > 0)
            //{
            //    if (ds1 != null)
            //    {
            //        if (ds1.Tables[0].Rows.Count > 0)
            //        {

            //            DataSet dcalculate = new DataSet();

            //            dt1.Columns.Add(new DataColumn("empid"));
            //            dt1.Columns.Add(new DataColumn("name"));
            //            dt1.Columns.Add(new DataColumn("qty"));
            //            dt1.Columns.Add(new DataColumn("rate"));

            //            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //            {
            //                DataRow dr_final12 = dt1.NewRow();
            //                dr_final12["empid"] = ds1.Tables[0].Rows[i]["empid"].ToString();
            //                dr_final12["name"] = ds1.Tables[0].Rows[i]["name"].ToString();
            //                dr_final12["qty"] = ds1.Tables[0].Rows[i]["qty"].ToString();
            //                dr_final12["rate"] = ds1.Tables[0].Rows[i]["rate"].ToString();

            //                dt1.Rows.Add(dr_final12);

            //            }
            //        }

            //        DataTable dtt1 = new DataTable();
            //        dtt1.Columns.Add(new DataColumn("empid"));
            //        dtt1.Columns.Add(new DataColumn("name"));
            //        dtt1.Columns.Add(new DataColumn("qty"));
            //        dtt1.Columns.Add(new DataColumn("rate"));

            //        var result = from r in dt1.AsEnumerable()
            //                     group r by new { empid = r["empid"], empname = r["name"] } into g
            //                     select new
            //                     {
            //                         Group = g.Key.empid,
            //                         namee = g.Key.empname,
            //                         Sum = g.Sum(x => Convert.ToDouble(x["rate"])),
            //                         Sum1 = g.Sum(y => Convert.ToDouble(y["qty"])),
            //                         //s totpri =g.Sum(y=>item.Sum *Convert.ToDouble(x["Price"]))
            //                     };

            //        foreach (var item in result)
            //        {
            //            DataRow dr = dtt1.NewRow();
            //            dr["empid"] = item.Group;
            //            dr["name"] = item.namee;
            //            dr["qty"] = item.Sum1;
            //            dr["rate"] = item.Sum;
            //            dtt1.Rows.Add(dr);
            //        }

            //        IEnumerable<DataRow> rows = dtt1.Rows.Cast<DataRow>().Where(r => r["Qty"].ToString() == "0");
            //        rows.ToList().ForEach(r => r.Delete());
            //        dtt1.AcceptChanges();

            //        dss1.Tables.Add(dtt1);
            //        Gridoverall.DataSource = dss1;
            //        Gridoverall.DataBind();

            //    }


            //    //gridshirt.DataSource = ds;
            //    //gridshirt.DataBind();
            //}
            //else
            //{
            //    Gridoverall.DataSource = null;
            //    Gridoverall.DataBind();
            //    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('No Record Found.Thanks You!!!')", true);
            //    //return;
            //}

            //DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(drpsample.SelectedValue));
            //lblLot.Text = drpsample.SelectedItem.Text;
            //if (dshirtall.Tables[0].Rows.Count > 0)
            //{
            //    Gridoverall.DataSource = dshirtall;
            //    Gridoverall.DataBind();
            //}
            //else
            //{
            //    Gridoverall.DataSource = null;
            //    Gridoverall.DataBind();
            //}
        }
    }
}