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
using System.Data.OleDb;

namespace Billing.Accountsbootstrap
{
    public partial class FirstStageGrid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {

                DataSet ds = objBs.selectfirststageprocess();
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvcust.DataSource = ds;
                        gvcust.DataBind();
                    }
                    else
                    {
                        gvcust.DataSource = null;
                        gvcust.DataBind();
                    }
                }
                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
        }
        protected void Add_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/FirstStageProcess.aspx?name=Addnew");

        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.selectCheque();
            }
            else
            {
                ds = objBs.searchfilterCheque(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedItem.Value));
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvcust.DataSource = ds;
                    gvcust.PageIndex = e.NewPageIndex;
                    gvcust.DataBind();
                }
                else
                {
                    gvcust.DataSource = null;
                    gvcust.DataBind();
                }
            }
            else
            {
                gvcust.DataSource = null;
                gvcust.DataBind();
            }
        }

        protected void refresh_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/FirstStageGrid.aspx");

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            //DataSet ds = objBs.searchviewprocess(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedItem.Value));
            //if (ds != null)
            //{
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        gvcust.DataSource = ds;
            //        gvcust.DataBind();
            //    }
            //    else
            //    {
            //        gvcust.DataSource = null;
            //        gvcust.DataBind();
            //    }


            //}
            //else
            //{
            //    gvcust.DataSource = null;
            //    gvcust.DataBind();
            //}
        }
        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("EditFirstGrid.aspx?iid=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "delete")
            {
                int iSucess = objBs.deletesamplecodeprocess(e.CommandArgument.ToString());
                Response.Redirect("FirstStageGrid.aspx");
            }
            //else if (e.CommandName == "print")
            //{
            //    Response.Redirect("Print_Fabric.aspx?iSalesID=" + e.CommandArgument.ToString());
            //}
            //else if (e.CommandName == "printCR")
            //{
            //    Response.Redirect("Print_FabricCR.aspx?iSalesID=" + e.CommandArgument.ToString());
            //}
        }

        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string bankid = e.Row.Cells[0].Text;


            //    if (objBs.checkcuttingusedornot(int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
            //    {
            //        ((Image)e.Row.FindControl("img")).Visible = false;
            //        ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;


            //        ((Image)e.Row.FindControl("dlt")).Visible = false;
            //        ((ImageButton)e.Row.FindControl("imgdisable1")).Visible = true;
            //    }

            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ReadExcelFile("Sheet1", "F:/excelformatesuxus.xlsx");
            Response.Redirect("../Accountsbootstrap/FirstStageProcess.aspx?name=bulk");
        }

        private void ReadExcelFile(string sheetName, string path)
        {

            using (OleDbConnection conn = new OleDbConnection())
            {
                DataSet ds = new DataSet();
                string Import_FileName = path;
                //string fileExtension = Path.GetExtension(Import_FileName);
                //if (fileExtension == ".xls")
                //  conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0;HDR=YES;IMEX=2\'";
                conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                          Import_FileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                ////if (fileExtension == ".xlsx")
                //    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + sheetName + "$]";

                    comm.Connection = conn;





                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(ds);
                        int iSuccess = 0;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string Customername = ds.Tables[0].Rows[i]["Supplier"].ToString();
                            string colour = ds.Tables[0].Rows[i]["Color"].ToString();
                            string Item = ds.Tables[0].Rows[i]["Item"].ToString();
                            string Design = ds.Tables[0].Rows[i]["Designno"].ToString();
                            string Order = ds.Tables[0].Rows[i]["Order"].ToString();
                            string Mtrrate = ds.Tables[0].Rows[i]["Mtrrate"].ToString();
                            string WSP = ds.Tables[0].Rows[i]["WSP"].ToString();
                            string MRP = ds.Tables[0].Rows[i]["MRP"].ToString();

                            int ledgerid = objBs.ExelCustcheck(Customername);

                            if (colour.Contains(","))
                            {
                                string[] split = colour.Split(',');

                                for (int L = 0; L < split.Length; L++)
                                {
                                    int status = objBs.inserfirststage(Item, ledgerid.ToString(), Design, split[L], Order, Mtrrate, WSP, MRP);
                                }
                            }
                            else
                            {
                                int status = objBs.inserfirststage(Item, ledgerid.ToString(), Design, colour, Order, Mtrrate, WSP, MRP);

                            }

                        }
                    }

                }
            }
        }
    }
}