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
using System.Web.UI.HtmlControls;


namespace Billing.Accountsbootstrap
{
    public partial class Abu_Manufacture : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        double gndto = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objBs.Select();

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvcustinfo.DataSource = ds;
                        gvcustinfo.DataBind();
                    }
                    else
                    {
                        gvcustinfo.DataSource = null;
                        gvcustinfo.DataBind();
                    }
                }
                else
                {
                    gvcustinfo.DataSource = null;
                    gvcustinfo.DataBind();
                }
            }
        }

        protected void Page_Change(object sender, GridViewPageEventArgs e) { }

        protected void gvcustinfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "edit")
            //{
            //    if (e.CommandArgument.ToString() != "")
            //    {
            //        Response.Redirect("ARFA_Cutting.aspx?CuttingInformationID=" + e.CommandArgument.ToString());
            //    }
            //}

            //else if (e.CommandName == "delete")
            //{
            //    int iSucess = objBs.DeleteCuttingInfo(e.CommandArgument.ToString());

            //    Response.Redirect("ARFA_CuttingGrid.aspx");
            //}
        }

        protected void gvcustinfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //foreach (GridViewRow gr in gvcustinfo.Rows)
            //{
            //    string dueDate = gr.Cells[11].Text;//Due Date Heading

            //    string[] date = dueDate.Split('/');

            //    string dt = date[0];
            //    string Mon = date[1];
            //    string yeat = date[2];

            //    string DDate = yeat + "-" + Mon + "-" + dt;
            //    var errorDate = DateTime.Now.ToString("yyyy-MM-dd");

            //    if (Convert.ToDateTime(DDate) < Convert.ToDateTime(errorDate))
            //    {
            //        gr.ForeColor = System.Drawing.Color.Red;
            //    }
            //    else
            //    {
            //        gr.ForeColor = System.Drawing.Color.Navy;
            //    }

            //    if (gr.Cells[24].Text == "Completed")
            //    {
            //        gr.ForeColor = System.Drawing.Color.Green;
            //    }
            //}

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //Access Cell values.
            //    double total = Convert.ToDouble(e.Row.Cells[21].Text);
            //    gndto = gndto + total;
            //}
            //lbltotal.Text = gndto.ToString("N");

        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployee.SelectedValue == "All Employees")
            {
                DataSet ds_Cutting = objBs.SelectCuttingInfoDetwithEmptableWhere(0);
                if (ds_Cutting != null)
                {
                    if (ds_Cutting.Tables[0].Rows.Count > 0)
                    {
                        gvcustinfo.DataSource = ds_Cutting;
                        gvcustinfo.DataBind();
                    }
                    else
                    {
                        gvcustinfo.DataSource = null;
                        gvcustinfo.DataBind();
                    }
                }
                else
                {
                    gvcustinfo.DataSource = null;
                    gvcustinfo.DataBind();
                }
            }
            else
            {
                DataSet ds_Cutting = objBs.SelectCuttingInfoDetwithEmptableWhere(Convert.ToInt32(ddlEmployee.SelectedValue));
                if (ds_Cutting != null)
                {
                    if (ds_Cutting.Tables[0].Rows.Count > 0)
                    {
                        gvcustinfo.DataSource = ds_Cutting;
                        gvcustinfo.DataBind();
                    }
                    else
                    {
                        gvcustinfo.DataSource = null;
                        gvcustinfo.DataBind();
                    }
                }
                else
                {
                    gvcustinfo.DataSource = null;
                    gvcustinfo.DataBind();
                }
            }
        }

        protected void btnSearch_Data(object sender, EventArgs e)
        {
            if (ddlEmployee.SelectedValue == "All Employees")
            {
                DataSet ds_Cutting = objBs.SelectCuttingInfoDetwithEmptableSearch(txtDesignNo.Text, ddStatus.SelectedValue, txtStitchingFromDate.Text, txtStitchingToDate.Text, Convert.ToInt32(0));
                if (ds_Cutting != null)
                {
                    if (ds_Cutting.Tables[0].Rows.Count > 0)
                    {
                        gvcustinfo.DataSource = ds_Cutting;
                        gvcustinfo.DataBind();
                    }
                    else
                    {
                        gvcustinfo.DataSource = null;
                        gvcustinfo.DataBind();
                    }
                }
                else
                {
                    gvcustinfo.DataSource = null;
                    gvcustinfo.DataBind();
                }
            }
            else
            {
                DataSet ds_Cutting = objBs.SelectCuttingInfoDetwithEmptableSearch(txtDesignNo.Text, ddStatus.SelectedValue, txtStitchingFromDate.Text, txtStitchingToDate.Text, Convert.ToInt32(ddlEmployee.SelectedValue));
                if (ds_Cutting != null)
                {
                    if (ds_Cutting.Tables[0].Rows.Count > 0)
                    {
                        gvcustinfo.DataSource = ds_Cutting;
                        gvcustinfo.DataBind();
                    }
                    else
                    {
                        gvcustinfo.DataSource = null;
                        gvcustinfo.DataBind();
                    }
                }
                else
                {
                    gvcustinfo.DataSource = null;
                    gvcustinfo.DataBind();
                }
            }
        }

        protected void btnClear_Data(object sender, EventArgs e)
        {
            Response.Redirect("ARFA_EmpCuttingReport.aspx");
        }
    }
}