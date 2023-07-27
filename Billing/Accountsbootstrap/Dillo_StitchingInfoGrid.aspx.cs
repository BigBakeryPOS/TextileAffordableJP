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
    public partial class Dillo_StitchingInfoGrid : System.Web.UI.Page
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
                DataSet ds = objBs.SelectStitchingInfoDetGrid();
               
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
                txtstartdate.Text = DateTime.Now.AddDays(-30).ToString("dd/MM/yyyy");
                txtenddate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }


          

        }




        protected void Add_Click(object sender, EventArgs e)
        {

            //string button = string.Empty;
            //button = btnadd.Text;
            //{
            //    button = btnadd.Text;
            //    Response.Redirect("Suppliermaster.aspx?name=" + button.ToString());
            //}
            Response.Redirect("../Accountsbootstrap/Dillo_StitchingInfo.aspx");

        }

        protected void Page_Change(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            if (ddlfilter.SelectedValue == "0")
            {
                ds = objBs.selectcustomerDet(6, 2);
            }
            else
            {
                ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);
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

            Response.Redirect("../Accountsbootstrap/viewsupplier.aspx");

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            //DataSet ds = objBs.searchCustomerMaster(txtsearch.Text, Convert.ToInt32(ddlfilter.SelectedValue), 1, 2);

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


            DataSet ds = objBs.SelectStitchingInfoDetGrid_ByDate(txtstartdate.Text, txtenddate.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds != null)
                {
                    gvcust.DataSource = ds;
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
            DataSet dgett = new DataSet();
            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("Dillo_StitchingInfo.aspx?LotNo=" + e.CommandArgument.ToString());
                }
            }

            else if (e.CommandName == "delete")
            {
              //int iSucess = objBs.deletecustomer(e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text);

               int iSucess = objBs.Delete_StitchingInfo_All(e.CommandArgument.ToString(), "tblAuditMaster_" + sTableName, lblUser.Text);

              //  Response.Redirect("viewsupplier.aspx");

                   Response.Redirect("Dillo_StitchingInfoGrid.aspx");

                   DataSet ds = objBs.SelectStitchingInfoDetGrid();
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
            else if (e.CommandName == "Status")
            {
                // if (button.Text == "All")
                {
                    //STICHING PROCESS
                    divLot1.Visible = true;
                    dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "3");
                    if (dgett.Tables[0].Rows.Count > 0)
                    {
                        string cuttid = dgett.Tables[0].Rows[0]["Cutid"].ToString();

                        DataSet dstat = objBs.getstatusreport(Convert.ToInt32(cuttid));
                        DataSet dstich = objBs.checkcurrentststus(cuttid);
                        if (dstich.Tables[0].Rows.Count > 0)
                        {
                            lblstiching.BackColor = System.Drawing.Color.Green;
                        }
                        else
                        {
                            lblstiching.BackColor = System.Drawing.Color.Red;
                        }
                      //  else
                        {
                           

                            if (dstat.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dstat.Tables[0].Rows.Count; i++)
                                {
                                    string currentststus = dstat.Tables[0].Rows[i]["Currentstatus"].ToString();
                                    string ststus = dstat.Tables[0].Rows[i]["Status"].ToString();
                                    string screen = dstat.Tables[0].Rows[i]["Screen"].ToString();

                                    if (screen == "Kaja")
                                    {
                                        if (ststus == "Y")
                                        {
                                            lblKaja.BackColor = System.Drawing.Color.Green;
                                        }
                                        else if (currentststus == "Y")
                                        {
                                            lblKaja.BackColor = System.Drawing.Color.Red;
                                        }
                                        else
                                        {
                                            lblKaja.BackColor = System.Drawing.Color.Yellow;
                                        }
                                    }
                                    if (screen == "Emb")
                                    {
                                        if (ststus == "Y")
                                        {
                                            lblemb.BackColor = System.Drawing.Color.Green;
                                        }
                                        else if (currentststus == "Y")
                                        {
                                            lblemb.BackColor = System.Drawing.Color.Red;
                                        }
                                        else
                                        {
                                            lblemb.BackColor = System.Drawing.Color.Yellow;
                                        }
                                    }
                                    if (screen == "Wash")
                                    {
                                        if (ststus == "Y")
                                        {
                                            lblwash.BackColor = System.Drawing.Color.Green;
                                        }
                                        else if (currentststus == "Y")
                                        {
                                            lblwash.BackColor = System.Drawing.Color.Red;
                                        }
                                        else
                                        {
                                            lblwash.BackColor = System.Drawing.Color.Yellow;
                                        }
                                    }
                                    if (screen == "Iron")
                                    {
                                        if (ststus == "Y")
                                        {
                                            lbliron.BackColor = System.Drawing.Color.Green;
                                        }
                                        else if (currentststus == "Y")
                                        {
                                            lbliron.BackColor = System.Drawing.Color.Red;
                                        }
                                        else
                                        {
                                            lbliron.BackColor = System.Drawing.Color.Yellow;
                                        }
                                    }
                                }

                            }
                        }


                        DataSet dshirtall = objBs.getLotNoReportGenerateNew(Convert.ToInt32(cuttid));
                        lblLot.Text = cuttid;
                        if (dgett.Tables[0].Rows.Count > 0)
                        {
                            //reportDetails.Visible = true;
                            DataSet temp = new DataSet();
                            DataTable dtt = new DataTable();

                            dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                            // dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                            dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                            dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                            //  dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                            // dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                            // dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                            temp.Tables.Add(dtt);



                            for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                            {
                                int pending = 0;
                                int k = 0;
                                int total = 0;

                                string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                {
                                    if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString())
                                    {
                                        DataRow dr = dtt.NewRow();
                                        dr["checked"] = dshirtall.Tables[0].Rows[j]["checked"].ToString();
                                        //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                        dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                        dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                        dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();
                                        //   dr["PerRate"] = dshirtall.Tables[0].Rows[j]["PerRate"].ToString();
                                        //   dr["Date"] = Convert.ToDateTime(dshirtall.Tables[0].Rows[j]["Date"]).ToString("dd/MM/yyyy");

                                        //if (k == 0)
                                        //{
                                        //    dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                        //    // total = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]);
                                        //    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        //    pending = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                        //    dr["Pending"] = pending.ToString();
                                        //    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);

                                        //    k = 1;
                                        //    //  dt.Rows.Add(dr);

                                        //}
                                        //else
                                        //{
                                        //    dr["TotalQty"] = pending.ToString();
                                        //    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        //    pending = Convert.ToInt32(pending) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                        //    dr["Pending"] = pending.ToString();
                                        //    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);
                                        //}
                                        temp.Tables[0].Rows.Add(dr);
                                    }
                                }

                            }
                            Gridoverall.DataSource = temp;
                            Gridoverall.DataBind();
                        }
                        else
                        {
                            Gridoverall.DataSource = null;
                            Gridoverall.DataBind();
                        }



                        //KAJA PROCESS

                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "5");

                        // DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables[0].Rows.Count > 0)
                        {
                            //reportDetails.Visible = true;
                            DataSet temp = new DataSet();
                            DataTable dtt = new DataTable();

                            dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                            dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                            dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                            dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                            temp.Tables.Add(dtt);



                            for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                            {
                                int pending = 0;
                                int k = 0;
                                int total = 0;

                                string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                {
                                    if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString())
                                    {
                                        DataRow dr = dtt.NewRow();
                                        dr["checked"] = dshirtall.Tables[0].Rows[j]["checked"].ToString();
                                        //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                        dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                        dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                        dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();

                                        //if (k == 0)
                                        //{
                                        //    dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                        //    // total = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]);
                                        //    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        //    pending = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                        //    dr["Pending"] = pending.ToString();
                                        //    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);

                                        //    k = 1;
                                        //    //  dt.Rows.Add(dr);

                                        //}
                                        //else
                                        //{
                                        //    dr["TotalQty"] = pending.ToString();
                                        //    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        //    pending = Convert.ToInt32(pending) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                        //    dr["Pending"] = pending.ToString();
                                        //    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);
                                        //}
                                        temp.Tables[0].Rows.Add(dr);
                                    }
                                }

                            }
                            //Gridoverall.DataSource = temp;
                            //Gridoverall.DataBind();
                            gridkaja.DataSource = temp;
                            gridkaja.DataBind();
                        }
                        else
                        {
                            gridkaja.DataSource = null;
                            gridkaja.DataBind();
                        }

                        //EMBROIDING

                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "6");

                        //   DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables[0].Rows.Count > 0)
                        {
                            //  reportDetails.Visible = true;
                            DataSet temp = new DataSet();
                            DataTable dtt = new DataTable();

                            dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                            dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                            dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                            dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                            temp.Tables.Add(dtt);



                            for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                            {
                                int pending = 0;
                                int k = 0;
                                int total = 0;

                                string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                {
                                    if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString())
                                    {
                                        DataRow dr = dtt.NewRow();
                                        dr["checked"] = dshirtall.Tables[0].Rows[j]["checked"].ToString();
                                        //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                        dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                        dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                        dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();

                                        //if (k == 0)
                                        //{
                                        //    dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                        //    // total = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]);
                                        //    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        //    pending = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                        //    dr["Pending"] = pending.ToString();
                                        //    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);

                                        //    k = 1;
                                        //    //  dt.Rows.Add(dr);

                                        //}
                                        //else
                                        //{
                                        //    dr["TotalQty"] = pending.ToString();
                                        //    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        //    pending = Convert.ToInt32(pending) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                        //    dr["Pending"] = pending.ToString();
                                        //    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);
                                        //}
                                        temp.Tables[0].Rows.Add(dr);
                                    }
                                }

                            }
                            //Gridoverall.DataSource = temp;
                            //Gridoverall.DataBind();
                            gridemb.DataSource = temp;
                            gridemb.DataBind();
                        }
                        else
                        {
                            //Gridoverall.DataSource = null;
                            //Gridoverall.DataBind();
                            gridemb.DataSource = null;
                            gridemb.DataBind();
                        }


                        //WASHING

                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "7");

                        //    DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables[0].Rows.Count > 0)
                        {
                            //  reportDetails.Visible = true;
                            DataSet temp = new DataSet();
                            DataTable dtt = new DataTable();

                            dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                            dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                            dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                            dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                            temp.Tables.Add(dtt);



                            for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                            {
                                int pending = 0;
                                int k = 0;
                                int total = 0;

                                string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                {
                                    if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString())
                                    {
                                        DataRow dr = dtt.NewRow();
                                        dr["checked"] = dshirtall.Tables[0].Rows[j]["checked"].ToString();
                                        //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                        dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                        dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                        dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();

                                        //if (k == 0)
                                        //{
                                        //    dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                        //    // total = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]);
                                        //    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        //    pending = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                        //    dr["Pending"] = pending.ToString();
                                        //    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);

                                        //    k = 1;
                                        //    //  dt.Rows.Add(dr);

                                        //}
                                        //else
                                        //{
                                        //    dr["TotalQty"] = pending.ToString();
                                        //    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        //    pending = Convert.ToInt32(pending) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                        //    dr["Pending"] = pending.ToString();
                                        //    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);
                                        //}
                                        temp.Tables[0].Rows.Add(dr);
                                    }
                                }

                            }
                            gridwash.DataSource = temp;
                            gridwash.DataBind();
                            //Gridoverall.DataSource = temp;
                            //Gridoverall.DataBind();
                        }
                        else
                        {
                            gridwash.DataSource = null;
                            gridwash.DataBind();
                            //Gridoverall.DataSource = null;
                            //Gridoverall.DataBind();
                        }


                        //IRON AND PACKING
                        dgett = objBs.getalldetailsforcalculation(Convert.ToInt32(e.CommandArgument), "8");

                        //   DataSet dshirtall = objBs.getLotNoReportGenerate(Convert.ToInt32(ddlLotNo.SelectedValue));
                        lblLot.Text = e.CommandArgument.ToString();
                        if (dgett.Tables[0].Rows.Count > 0)
                        {
                            // reportDetails.Visible = true;
                            DataSet temp = new DataSet();
                            DataTable dtt = new DataTable();

                            dtt.Columns.Add(new DataColumn("checked", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Name", typeof(string)));
                            dtt.Columns.Add(new DataColumn("processtype", typeof(string)));
                            dtt.Columns.Add(new DataColumn("TotalQty", typeof(string)));
                            dtt.Columns.Add(new DataColumn("PerRate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("ReceivedQuantity", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Pending", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Date", typeof(string)));
                            temp.Tables.Add(dtt);



                            for (int i = 0; i < dgett.Tables[0].Rows.Count; i++)
                            {
                                int pending = 0;
                                int k = 0;
                                int total = 0;

                                string processtypeid = dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString();
                                for (int j = 0; j < dshirtall.Tables[0].Rows.Count; j++)
                                {
                                    if (dgett.Tables[0].Rows[i]["ProcessTypeID"].ToString() == dshirtall.Tables[0].Rows[j]["ProcessTypeID"].ToString())
                                    {
                                        DataRow dr = dtt.NewRow();
                                        dr["checked"] = dshirtall.Tables[0].Rows[j]["checked"].ToString();
                                        //    dr["Name"] = dshirtall.Tables[0].Rows[j]["Name"].ToString();
                                        dr["processtype"] = dshirtall.Tables[0].Rows[j]["processtype"].ToString();
                                        dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                        dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        dr["Pending"] = dshirtall.Tables[0].Rows[j]["Pending"].ToString();

                                        //if (k == 0)
                                        //{
                                        //    dr["TotalQty"] = dshirtall.Tables[0].Rows[j]["TotalQty"].ToString();
                                        //    // total = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]);
                                        //    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        //    pending = Convert.ToInt32(dshirtall.Tables[0].Rows[j]["TotalQty"]) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                        //    dr["Pending"] = pending.ToString();
                                        //    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);

                                        //    k = 1;
                                        //    //  dt.Rows.Add(dr);

                                        //}
                                        //else
                                        //{
                                        //    dr["TotalQty"] = pending.ToString();
                                        //    dr["ReceivedQuantity"] = dshirtall.Tables[0].Rows[j]["ReceivedQuantity"].ToString();
                                        //    pending = Convert.ToInt32(pending) - Convert.ToInt32(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]);
                                        //    dr["Pending"] = pending.ToString();
                                        //    dr["Rate"] = Convert.ToDouble(dshirtall.Tables[0].Rows[j]["ReceivedQuantity"]) * Convert.ToDouble(dshirtall.Tables[0].Rows[j]["PerRate"]);
                                        //}
                                        temp.Tables[0].Rows.Add(dr);
                                    }
                                }

                            }
                            gridiron.DataSource = temp;
                            gridiron.DataBind();
                            //Gridoverall.DataSource = temp;
                            //Gridoverall.DataBind();
                        }
                        else
                        {
                            gridiron.DataSource = null;
                            gridiron.DataBind();
                            //Gridoverall.DataSource = null;
                            //Gridoverall.DataBind();
                        }
                    }
                }
                mpe.Show();
            }
        }

        protected void gvcust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string unit = e.Row.Cells[3].Text;

                foreach (TableCell gr in e.Row.Cells)
                {
                    if (unit == "UNIT 2")
                    {
                        gr.BackColor = System.Drawing.Color.Yellow;
                    }
                    else if (unit == "UNIT 1")
                    {
                        gr.BackColor = System.Drawing.Color.Tomato;
                    }
                }
            }
        }

        protected void btnFormat_Click(object sender, EventArgs e)
        {
            string button = string.Empty;
            button = Button3.Text;
            {
                button = Button3.Text;
                //Response.Redirect("categorymaster.aspx");
                Response.Redirect("Suppliermaster.aspx?name=" + button.ToString());
            }

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            HtmlForm form = new HtmlForm();
            Response.Clear();
            Response.Buffer = true;
            string filename = "SupplierMaster_" + DateTime.Now.ToString() + ".xls";



            DataSet ds = new DataSet();



            ds = objBs.selectcustomerDet(6, 2);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("CustomerName"));
                    dt.Columns.Add(new DataColumn("MobileNo"));
                    dt.Columns.Add(new DataColumn("Email"));
                    dt.Columns.Add(new DataColumn("Type"));
                    dt.Columns.Add(new DataColumn("Address"));
                    // dt.Columns.Add(new DataColumn("Area"));
                    //dt.Columns.Add(new DataColumn("City"));
                    dt.Columns.Add(new DataColumn("IsActive"));
                    dt.Columns.Add(new DataColumn("Open-Credit"));
                    dt.Columns.Add(new DataColumn("Open-Debit"));



                    //DataRow dr_export1 = dt.NewRow();
                    //dt.Rows.Add(dr_export1);

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DataRow dr_export = dt.NewRow();
                        dr_export["CustomerName"] = dr["LedgerName"];
                        dr_export["MobileNo"] = dr["MobileNo"];
                        dr_export["Email"] = dr["Email"];
                        dr_export["Type"] = dr["contacttypename"];
                        dr_export["Address"] = dr["Address"];
                        //dr_export["Area"] = dr["Area"];
                        //dr_export["City"] = dr["City"];
                        dr_export["IsActive"] = dr["IsActive"];
                        dr_export["Open-Credit"] = dr["Open_Credit"];
                        dr_export["Open-Debit"] = dr["Open_Depit"];
                        dt.Rows.Add(dr_export);
                    }

                    ExportToExcel(filename, dt);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Button), "MyScript", "alert('No Data Found');", true);
            }
        }

        public void ExportToExcel(string filename, DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
                dgGrid.HeaderStyle.Font.Bold = true;
                //Get the HTML for the control.
                dgGrid.RenderControl(hw);
                //Write the HTML back to the browser.
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
                this.EnableViewState = false;
                Response.Write(tw.ToString());
                Response.End();
            }
        }
    }
}