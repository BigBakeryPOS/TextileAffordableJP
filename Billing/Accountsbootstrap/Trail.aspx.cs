using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;

using System.Text;
using System.Globalization;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class Trail : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        public double debitTotal = 0;
        public double creditTotal = 0;
        string sTableName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            if (!IsPostBack)
            {
                txtfrmdate.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                string UserName = Session["UserName"].ToString();
                string UserID = Session["UserID"].ToString();
                sTableName = Session["User"].ToString();
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

        protected void btnreport_Click(object sender, EventArgs e)
        {
            TrialBalance();
        }

        public void TrialBalance()
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

            DataSet mainDs = objBs.GetTrailGroups();
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
                            strCredit = "0";

                            if (totalSum < 0)
                            {
                                strCredit = Convert.ToString(Math.Abs(totalSum));
                                strDebit = "0";
                            }
                        }
                        else
                        {
                            totalSum = creditSum - debitSum;
                            strCredit = Convert.ToString(totalSum.ToString("f2"));
                            strDebit = "0";
                            if (totalSum < 0)
                            {
                                strCredit = "0";
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
            gvTrailBalance.DataSource = grdDs;

           

            gvTrailBalance.DataBind();

            idt.Visible = true;
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

        public void gvTrailBalance_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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

                lblDebitTotal.Text = debitTotal.ToString("f2");
                lblCreditTotal.Text = creditTotal.ToString("f2");

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
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;

                    e.Row.Cells[0].Text = "Total";
                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[1].Text = debitTotal.ToString("f2");
                    e.Row.Cells[2].Text = creditTotal.ToString("f2");
                    e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
                    e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                }
           
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            gvTrailBalance.PagerSettings.Visible = false;
            //  GridView1.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gvTrailBalance.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'")
                .Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            //  sb.Append("test1");
            sb.Append("printWin.document.write(\"");
            sb.Append("ARK </br>");
            sb.Append("Trail Balance </br>");
            sb.Append(" </br></br>");
            sb.Append("</br>");

            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            gvTrailBalance.PagerSettings.Visible = true;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}