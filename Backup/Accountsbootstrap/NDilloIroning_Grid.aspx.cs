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
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class NDilloIroning_Grid : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";

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
                DataSet ds = objBs.IronGridNew();
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
            if (e.CommandName == "edit")
            {
                //if (e.CommandArgument.ToString() != "")
                //{
                //    Response.Redirect("NDillo_Ironing.aspx?LotId=" + e.CommandArgument.ToString() + "&name=" + e.CommandName);
                //}
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This Functionality is in Process.Thank You!!!!');", true);
                return;
            }

            else if (e.CommandName == "Received")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("NDillo_Ironing.aspx?LotId=" + e.CommandArgument.ToString() + "&name=" + e.CommandName);
                }
            }

            else if (e.CommandName == "delete")
            {
                //   int iSucess = objBs.DeleteCuttingInfo(e.CommandArgument.ToString());

                //  Response.Redirect("CuttingInfoGrid.aspx");
            }

            else if (e.CommandName == "copy")
            {
                // Response.Redirect("DilloStitchingInfoGrid.aspx?CuttingInformationID=" + e.CommandArgument.ToString() + "&Name=" + e.CommandName);

                //Response.Redirect("ARFA_CuttingInfoPrint.aspx?CuttingInformationID=" + cuttingInformationID + "&Name=" + ddlSignature.SelectedItem.Text + "&EmpImage=" + imgEmp.ImageUrl);
            }
        }

        protected void gvcustinfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //foreach (GridViewRow gr in gvcustinfo.Rows)
            //{

            //    //string pendingQty = gr.Cells[9].Text;
            //    //string Qty = gr.Cells[8].Text;
            //    //string rate = gr.Cells[16].Text;
            //    //decimal rateQty = Convert.ToDecimal(rate);
            //    //if (pendingQty != "0")
            //    //{
            //    //    int pendingQtyTotal = Convert.ToInt32(Qty) - Convert.ToInt32(pendingQty);
            //    //    decimal pendingTotal = pendingQtyTotal * rateQty;
            //    //    gr.Cells[20].Text = Convert.ToString(pendingTotal);
            //    //}

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
        }

        protected void btnAddNew_entry(object sender, EventArgs e)
        {
            Response.Redirect("NDillo_Ironing.aspx");
        }
    }
}