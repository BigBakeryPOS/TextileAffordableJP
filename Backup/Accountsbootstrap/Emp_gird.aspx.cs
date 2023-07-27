using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataLayer;
using CommonLayer;
using System.Text;
using System.Data;


namespace Billing.Accountsbootstrap
{
    public partial class Emp_gird : System.Web.UI.Page
    {
        BSClass objbs = new BSClass();
        int TotalCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblempid.Text = Session["UserID"].ToString();
                lblempname.Text = Session["UserName"].ToString();

                DataSet hrm = objbs.EmployeeGrid();

                DataView dv = new DataView(hrm.Tables[0]);
                dv.Sort = "Emp_code ASC";

                var GridDescView = dv;
                gridviewhrm.DataSource = GridDescView;
                gridviewhrm.DataBind();

                DataSet dsdept = objbs.Select_depart();
                ddldepartment.DataSource = dsdept;
                ddldepartment.DataValueField = "DeptId";
                ddldepartment.DataTextField = "DeptName";
                ddldepartment.DataBind();
                ddldepartment.Items.Insert(0, "All");


                DataSet ds = objbs.Select_UnitFirst();
                ddlUnit.DataSource = ds;
                ddlUnit.DataValueField = "UnitID";
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "All");

                DataSet dsDesigin = objbs.EmployeeGridDesg1(ddlUnit.SelectedValue, ddldepartment.SelectedValue, ddlStatus.SelectedValue);
                if (dsDesigin.Tables[0].Rows.Count > 0)
                {
                    gvDesignation.DataSource = dsDesigin;
                    gvDesignation.DataBind();
                }
                else
                {
                    gvDesignation.DataSource = null;
                    gvDesignation.DataBind();
                }

            }
        }

        protected void ddldepartment_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = objbs.EmployeeGrid1(ddlUnit.SelectedValue, ddldepartment.SelectedValue, ddlStatus.SelectedValue);

            DataView dv = new DataView(ds.Tables[0]);
            dv.Sort = "Emp_code ASC";

            if (ds.Tables[0].Rows.Count > 0)
            {
                var GridDescView = dv;
                gridviewhrm.DataSource = GridDescView;
                gridviewhrm.DataBind();
            }
            else
            {
                gridviewhrm.DataSource = null;
                gridviewhrm.DataBind();
            }

            DataSet dsDesigin = objbs.EmployeeGridDesg1(ddlUnit.SelectedValue, ddldepartment.SelectedValue, ddlStatus.SelectedValue);
            if (dsDesigin.Tables[0].Rows.Count > 0)
            {
                gvDesignation.DataSource = dsDesigin;
                gvDesignation.DataBind();
            }
            else
            {
                gvDesignation.DataSource = null;
                gvDesignation.DataBind();
            }
        }

        protected void gridviewhrm_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Employee_details.aspx?Employee_Id=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "Del")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    int i = objbs.deletehrm(Convert.ToString(e.CommandArgument.ToString()));
                    Response.Redirect("Emp_gird.aspx");
                }
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employee_details.aspx");
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            DataSet dsDesigin = objbs.EmployeeGridDesg1(ddlUnit.SelectedValue, ddldepartment.SelectedValue, ddlStatus.SelectedValue);
            if (dsDesigin.Tables[0].Rows.Count > 0)
            {
                gvDesignationExcel.DataSource = dsDesigin;
                gvDesignationExcel.DataBind();
            }
            else
            {
                gvDesignationExcel.DataSource = null;
                gvDesignationExcel.DataBind();
            }

           // DataSet ds = objbs.getalldetailsforemployee(ddlUnit.SelectedValue, ddldepartment.SelectedValue, ddlStatus.SelectedValue);
            DataSet ds = objbs.EmployeeGrid1(ddlUnit.SelectedValue, ddldepartment.SelectedValue, ddlStatus.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region

                DataTable dt_2 = new DataTable();
                dt_2.Columns.Add(new DataColumn("empcode"));
                dt_2.Columns.Add(new DataColumn("name"));
                dt_2.Columns.Add(new DataColumn("dob"));

                dt_2.Columns.Add(new DataColumn("phno_No"));
                dt_2.Columns.Add(new DataColumn("email_id"));
                dt_2.Columns.Add(new DataColumn("DOJ"));
                dt_2.Columns.Add(new DataColumn("CurrentAddress"));
                dt_2.Columns.Add(new DataColumn("PermanentAddress"));
                dt_2.Columns.Add(new DataColumn("fathername"));
                dt_2.Columns.Add(new DataColumn("MaritalStatus"));
                dt_2.Columns.Add(new DataColumn("IsMotherWife"));
                dt_2.Columns.Add(new DataColumn("MotherWife"));
                dt_2.Columns.Add(new DataColumn("mobile1"));
                dt_2.Columns.Add(new DataColumn("salarytype"));
                dt_2.Columns.Add(new DataColumn("UnitName"));

                dt_2.Columns.Add(new DataColumn("DesiginationName"));
                dt_2.Columns.Add(new DataColumn("DeptName"));
                dt_2.Columns.Add(new DataColumn("uploadimage"));
                dt_2.Columns.Add(new DataColumn("EmployeeStatus"));

                foreach (DataRow DR_2 in ds.Tables[0].Rows)
                {
                    DataRow DR7 = dt_2.NewRow();
                    DR7["empcode"] = DR_2["emp_code"];
                    DR7["name"] = DR_2["name"];
                    DR7["dob"] = DR_2["dob"];
                    DR7["phno_No"] = DR_2["phno_No"];
                    DR7["email_id"] = DR_2["email_id"];
                    DR7["DOJ"] = DR_2["DOJ"];
                    DR7["CurrentAddress"] = DR_2["Address"];
                    DR7["PermanentAddress"] = DR_2["Address1"];
                    DR7["fathername"] = DR_2["fathername"];
                    DR7["MaritalStatus"] = DR_2["MaritalStatus"];
                    DR7["IsMotherWife"] = DR_2["IsMotherWife"];
                    DR7["MotherWife"] = DR_2["MotherWife"];
                    DR7["mobile1"] = DR_2["mobile1"];
                    DR7["salarytype"] = DR_2["salarytype"];
                    DR7["UnitName"] = DR_2["UnitName"];
                    DR7["DesiginationName"] = DR_2["DesiginationName"];
                    DR7["DeptName"] = DR_2["DeptName"];
                    DR7["uploadimage"] = ("http://" + Request.Url.Authority + DR_2["uploadimage"].ToString().Replace("~", string.Empty));
                    DR7["EmployeeStatus"] = DR_2["EmployeeStatus"];
                    dt_2.Rows.Add(DR7);

                }

                #endregion

                DataView dv = new DataView(dt_2);
                dv.Sort = "empcode ASC";
                var GridDescView = dv;
                gridempdetails.Caption = "Employee Details";
                gridempdetails.DataSource = GridDescView;
                gridempdetails.DataBind();
                btnExcel_OnClick(sender, e);
            }
            else
            {
                gridempdetails.DataSource = null;
                gridempdetails.DataBind();
            }
        }

        protected void btnExcel_OnClick(object sender, EventArgs e)
        {
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename= EmployeeDetails.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            Excel.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        protected void gvDesignation_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotalCount += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Count"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total";
                e.Row.Cells[2].Text = TotalCount.ToString("f0");
            }
        }
        protected void gvDesignationExcel_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TotalCount += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Count"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "Total";
                e.Row.Cells[2].Text = TotalCount.ToString("f0");
            }
        }

    }

}
