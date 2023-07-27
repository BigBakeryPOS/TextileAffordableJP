
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CommonLayer;
using BusinessLayer;
using DataLayer;
using System.Data;
using System.Text;

namespace Billing.Accountsbootstrap
{
    public partial class Setting : System.Web.UI.Page
    {
        OpeningStockEntry objBs = new OpeningStockEntry();
        BSClass objbs = new BSClass();
        string sTableName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                DataSet ds = objbs.settingnew();
                gridOpening.DataSource = ds;
                gridOpening.DataBind();
            }

            //DataSet dacess = new DataSet();
            //dacess = objbs.getuseraccessscreen(Session["EmpId"].ToString(), "settmaster");
            //if (dacess.Tables[0].Rows.Count > 0)
            //{
            //    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowadd"]) == true)
            //    {
            //        btnadd.Visible = true;
            //    }
            //    else
            //    {
            //        btnadd.Visible = false;
            //    }

            //    //if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowedit"]) == true)
            //    //{
            //    //    gridOpening.Columns[4].Visible = true;
            //    //}
            //    //else
            //    //{
            //    //    gridOpening.Columns[4].Visible = false;
            //    //}

            //    if (Convert.ToBoolean(dacess.Tables[0].Rows[0]["allowdelete"]) == true)
            //    {
            //        gridOpening.Columns[5].Visible = true;
            //    }
            //    else
            //    {
            //        gridOpening.Columns[5].Visible = false;
            //    }
            //}

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Opening.aspx");
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Setting.aspx");
        }

        protected void gridOpening_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                if (e.CommandArgument.ToString() != "")
                {

                    Response.Redirect("Opening.aspx?SettingID=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "Del")
            {
                if (e.CommandArgument.ToString() != "")
                {

                    objbs.DeletesSetting(e.CommandArgument.ToString(), sTableName);
                    Response.Redirect("Setting.aspx");
                }
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
           
        if (ddlfilter.SelectedValue == "1")
            {

                if (txtsearch.Text == "")
                {
                    lblerror.Text = "Please Enter The Details";
                    
                }
                else
                {
                    DataSet ds = objBs.getByCateGoryGrid_New(txtsearch.Text,sTableName);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        gridOpening.DataSource = ds;
                        gridOpening.DataBind();
                    
                    }
                    else
                    {
                      
                        gridOpening.DataSource = null;
                        gridOpening.DataBind();

                    }
                }

                
            }
            else if (ddlfilter.SelectedValue == "2")
            {
                if (txtsearch.Text == "")
                {
                    lblerror.Text = "Please Enter The Details";
                   
                }
                else
                {
                    DataSet ds = objBs.getByItemGrid_New(txtsearch.Text,sTableName);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gridOpening.DataSource = ds;
                        gridOpening.DataBind();
                     
                    }
                    else
                    {
                   
                        gridOpening.DataSource = null;
                        gridOpening.DataBind();
                    }
                }

            }

        else if (ddlfilter.SelectedValue == "3")
        {
            if (txtsearch.Text == "")
            {
                lblerror.Text = "Please Enter The Details";

            }
            else
            {
                DataSet ds = objBs.getByItemGridproduct(txtsearch.Text, sTableName);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gridOpening.DataSource = ds;
                    gridOpening.DataBind();

                }
                else
                {

                    gridOpening.DataSource = null;
                    gridOpening.DataBind();
                }
            }

        }

            else
            {
                //lblerror1.Text = "Please Select Valid Search Field";
                //lblerror.Visible = false;
            }
        }
        protected void openGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();

           
           if (ddlfilter.SelectedValue == "1")
            {
                ds = objBs.getByCateGoryGrid_New(txtsearch.Text,sTableName);
            }
            else if (ddlfilter.SelectedValue == "2")
            {
                ds = objBs.getByItemGrid_New(txtsearch.Text,sTableName);
            }

            else
            {
                ds = objBs.getGrid_New(sTableName);
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    gridOpening.DataSource = ds;
                    gridOpening.PageIndex = e.NewPageIndex;
                    gridOpening.DataBind();
                }
                else
                {
                    gridOpening.DataSource = null;
                    gridOpening.DataBind();
                }
            }
            else
            {
                gridOpening.DataSource = null;
                gridOpening.DataBind();
            }
        }
    }
}