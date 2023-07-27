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


namespace Billing.Accountsbootstrap
{
    public partial class viewcheques : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string id = string.Empty;
        int EmpId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            EmpId = Convert.ToInt32(Session["EmpId"].ToString());
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                divcode.Visible = false;
                DataSet ds = objBs.Chequeno("tblCheque");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ID"].ToString() == "")
                        TextBox3.Text = "1";
                    else
                        TextBox3.Text = ds.Tables[0].Rows[0]["ID"].ToString();

                    btnSave.Text = "Save";
                }

                DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 4, sTableName);
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddlBank.DataSource = dst.Tables[0];
                        ddlBank.DataTextField = "LedgerName";
                        ddlBank.DataValueField = "LedgerID";
                        ddlBank.DataBind();
                        ddlBank.Items.Insert(0, "Select Bank");
                    }
                }

                ds = objBs.selectCheque();
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

                DataSet dacess = new DataSet();
                dacess = objBs.getuseraccessscreen(Session["EmpId"].ToString(), "chqbook");
                if (dacess.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowadd"]) == true)
                    {
                        btnadd.Visible = true;
                        btnSave.Visible = true;
                    }
                    else
                    {
                        btnadd.Visible = false;
                        btnSave.Visible = false;
                    }

                    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowedit"]) == true)
                    {
                        gvcust.Columns[5].Visible = true;
                        btnSave.Visible = true;
                    }
                    else
                    {
                        gvcust.Columns[5].Visible = false;
                        btnSave.Visible = false;
                    }

                    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowdelete"]) == true)
                    {
                        gvcust.Columns[6].Visible = true;
                    }
                    else
                    {
                        gvcust.Columns[6].Visible = false;
                    }
                }

                DataSet dsed = objBs.CheckEditDelete(EmpId);
                if (dsed.Tables[0].Rows.Count > 0)
                {
                    //if (dsed.Tables[0].Rows[0]["allowedit"].ToString() == "1")
                    //{
                    //    //btnadd.Visible = true;
                    //    gvcust.Columns[5].Visible = true;
                    //}
                    //else
                    //{
                    //    gvcust.Columns[5].Visible = false;
                    //}

                    //if (dsed.Tables[0].Rows[0]["allowdelete"].ToString() == "1")
                    //{
                    //    gvcust.Columns[6].Visible = true;
                    //}
                    //else
                    //{
                    //    gvcust.Columns[6].Visible = false;
                    //}
                }
            }
        }
        protected void Add_Click(object sender, EventArgs e)
        {

            Response.Redirect("../Accountsbootstrap/Cheque.aspx");

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

            Response.Redirect("../Accountsbootstrap/viewcheques.aspx");

        }
        protected void Search_Click(object sender, EventArgs e)
        {
            DataSet ds = objBs.searchfilterCheque(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedItem.Value));
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
        protected void gvcust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "edit")
            //{
            //    if (e.CommandArgument.ToString() != "")
            //    {
            //        Response.Redirect("Cheque.aspx?iid=" + e.CommandArgument.ToString());
            //    }
            //}

             if (e.CommandName == "delete")
            {
                int iSucess = objBs.deleteCheque(Convert.ToInt32(e.CommandArgument.ToString()), "tblAuditMaster_" + sTableName, lblUser.Text);
                Response.Redirect("viewcheques.aspx");
            }
        }

        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string bankid = e.Row.Cells[1].Text;


                if (objBs.CeckIfChequeno(Convert.ToInt32(bankid), int.Parse(((HiddenField)e.Row.FindControl("ldgID")).Value)))
                {
                    ((Image)e.Row.FindControl("img")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable")).Visible = true;


                    ((Image)e.Row.FindControl("dlt")).Visible = false;
                    ((ImageButton)e.Row.FindControl("imgdisable1")).Visible = true;
                }

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtID.Text ="";
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            ddlBank.ClearSelection();
            btnSave.Text = "Save";
            lblName.Text = "Add Cheque";

            DataSet ds = objBs.Chequeno("tblCheque");
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"].ToString() == "")
                    TextBox3.Text = "1";
                else
                    TextBox3.Text = ds.Tables[0].Rows[0]["ID"].ToString();

                btnSave.Text = "Save";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString.Get("Mode");

            if (TextBox1.Text == TextBox2.Text)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('From Cheque No and To Cheque No not be same.!!! ')", true);
                return;
            }

            if (Convert.ToInt32(TextBox1.Text) > Convert.ToInt32(TextBox2.Text))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('ToCheque No Cannot be Less than FromChequeNo');", true);
                return;
            }

            if (objBs.ChequeLeafUsed(Convert.ToInt32(TextBox1.Text), Convert.ToInt32(TextBox2.Text), ddlBank.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Given Cheque No already entered for this bank');", true);
                return;
            }
            if (btnSave.Text == "Save")
            {

                int iStatus = objBs.insertCheque(Convert.ToInt32(lblUserID.Text), TextBox3.Text, Convert.ToInt32(TextBox1.Text), Convert.ToInt32(TextBox2.Text), Convert.ToInt32(ddlBank.SelectedValue), ddlBank.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text);
                DataSet ds = objBs.selectCheque();
                gvcust.DataSource = ds;
                gvcust.DataBind();
                Response.Redirect("../Accountsbootstrap/viewcheques.aspx");


            }
            else
            {
                int iStatus = objBs.updatecheque(Convert.ToInt32(txtID.Text), TextBox3.Text, Convert.ToInt32(TextBox1.Text), Convert.ToInt32(TextBox2.Text), Convert.ToInt32(ddlBank.SelectedValue), ddlBank.SelectedItem.Text, "tblAuditMaster_" + sTableName, lblUser.Text);
                DataSet ds = objBs.selectCheque();
                gvcust.DataSource = ds;
                gvcust.DataBind();
                Response.Redirect("../Accountsbootstrap/viewcheques.aspx");
            }
        }

        protected void gvcust_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblName.Text = "Update Cheque";

            if (gvcust.SelectedDataKey.Value != null && gvcust.SelectedDataKey.Value.ToString() != "")
                id = gvcust.SelectedDataKey.Value.ToString();
            {
                DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 4, sTableName);
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddlBank.DataSource = dst.Tables[0];
                        ddlBank.DataTextField = "LedgerName";
                        ddlBank.DataValueField = "LedgerID";
                        ddlBank.DataBind();
                        ddlBank.Items.Insert(0, "Select Bank");
                    }
                }

                DataSet ds1 = objBs.getselectCheque(Convert.ToInt32(id));
                if (ds1 != null)
                {
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        btnSave.Text = "Update";
                        txtID.Text = ds1.Tables[0].Rows[0]["id"].ToString();
                        TextBox3.Text = ds1.Tables[0].Rows[0]["id"].ToString();
                        TextBox1.Text = ds1.Tables[0].Rows[0]["fromcheque"].ToString();
                        TextBox2.Text = ds1.Tables[0].Rows[0]["tocheque"].ToString();

                        ddlBank.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["bankid"]).ToString();
                    }

                }


            }
        }
    }
}