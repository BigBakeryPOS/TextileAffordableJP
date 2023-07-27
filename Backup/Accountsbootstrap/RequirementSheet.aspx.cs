using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class RequirementSheet : System.Web.UI.Page
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
                //DataSet dsRequirementSheet = objBs.GetRequirementSheet();
                //if (dsRequirementSheet.Tables[0].Rows.Count > 0)
                //{
                //    GVRequirementSheet.DataSource = dsRequirementSheet;
                //    GVRequirementSheet.DataBind();
                //}
                //else
                //{
                //    GVRequirementSheet.DataSource = null;
                //    GVRequirementSheet.DataBind();
                //}

                DataSet dsexcno = objBs.getexcno("t1.approved", "true");
                if (dsexcno.Tables[0].Rows.Count > 0)
                {

                    drpexclist.DataSource = dsexcno.Tables[0];
                    drpexclist.DataTextField = "ExcNo";
                    drpexclist.DataValueField = "BuyerOrderId";
                    drpexclist.DataBind();
                    drpexclist.Items.Insert(0, "Select ExcNo");

                }

                DataSet dcompany = objBs.GetCompanyDetails();
                if (dcompany.Tables[0].Rows.Count > 0)
                {
                    drpcompany.DataSource = dcompany.Tables[0];
                    drpcompany.DataTextField = "Companyname";
                    drpcompany.DataValueField = "comapanyId";
                    drpcompany.DataBind();
                    //  ddlBrand.Items.Insert(0, "Select Company");
                }

                DataSet dsDCConditions = objBs.GetProcess();
                if (dsDCConditions.Tables[0].Rows.Count > 0)
                {
                    chkpcsprocess.DataSource = dsDCConditions.Tables[0];
                    chkpcsprocess.DataTextField = "Process";
                    chkpcsprocess.DataValueField = "Processid";
                    chkpcsprocess.DataBind();
                    chkpcsprocess.SelectedValue = "5";
                    foreach (ListItem li in chkpcsprocess.Items)
                    {
                        if (li.Selected)
                        {
                           // chkpcsprocess.Enabled = false;
                           // chkpcsprocess.Items.FindByValue("Processid").Enabled = false;
                            li.Enabled = false;

                        }
                    }       
                }

                string Reqid = Request.QueryString.Get("ReqID");
                if (Reqid != "" && Reqid != null)
                {
                    // Getting Exc No

                    btnSave.Text = "Update";
                    DataSet getrequirmentsheet = objBs.getrequirmentsheet(Reqid);
                    if (getrequirmentsheet.Tables[0].Rows.Count > 0)
                    {

                        string excno = getrequirmentsheet.Tables[0].Rows[0]["ExcNo"].ToString();


                        DataSet dsexcno_All = objBs.getexcno_All();
                        if (dsexcno_All.Tables[0].Rows.Count > 0)
                        {

                            drpexclist.DataSource = dsexcno_All.Tables[0];
                            drpexclist.DataTextField = "ExcNo";
                            drpexclist.DataValueField = "BuyerOrderId";
                            drpexclist.DataBind();
                            drpexclist.Items.Insert(0, "Select ExcNo");
                            drpexclist.SelectedValue = excno;
                            drpexclist.Enabled = false;

                        }
                        if (drpexclist.SelectedValue == "Select ExcNo")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Exc.No.Thank You.');", true);
                            return;
                        }
                        else
                        {
                            // in buyer order get shipment date

                            DataSet getbuyerorder = objBs.getshipmentdate(drpexclist.SelectedValue);
                            if (getbuyerorder.Tables[0].Rows.Count > 0)
                            {
                                //   divpcsprocess.Visible = true;
                                lblshipmentdate.Text = Convert.ToDateTime(getbuyerorder.Tables[0].Rows[0]["ShipmentDate"]).ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Valid Exc.No, Record Not Found.Thank You.');", true);
                                return;
                            }
                        }

                        #region TAB 1

                        #region Bind TAB 1
                        // Bind Style no
                        DataSet dstyleno = objBs.getstyledescr(drpexclist.SelectedValue);
                        if (dstyleno.Tables[0].Rows.Count > 0)
                        {
                            gvstylewithcolor.DataSource = dstyleno;
                            gvstylewithcolor.DataBind();
                        }
                        else
                        {
                            gvstylewithcolor.DataSource = null;
                            gvstylewithcolor.DataBind();
                        }

                        // Bind change color

                        DataSet getcolorchnagedetails = objBs.getcolorchnagedescription_Update(drpexclist.SelectedValue);
                        if (getcolorchnagedetails.Tables[0].Rows.Count > 0)
                        {
                            gridviewstyle.DataSource = getcolorchnagedetails;
                            gridviewstyle.DataBind();

                            for (int vLoop = 0; vLoop < gridviewstyle.Rows.Count; vLoop++)
                            {
                                Label lblcolorid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblcolorid");

                                DropDownList drpcolorlist = (DropDownList)gridviewstyle.Rows[vLoop].FindControl("drpcolorlist");
                                drpcolorlist.SelectedValue = lblcolorid.Text;
                            }
                        }
                        else
                        {
                            gridviewstyle.DataSource = null;
                            gridviewstyle.DataBind();
                        }

                        if (dstyleno.Tables[0].Rows.Count > 0)
                        {
                            color_change(sender, e);
                        }

                        #endregion


                        #endregion

                        #region PCS PROCESS

                        DataSet getrequirmentprocess = objBs.getrequirmentsheet_Process(Reqid);
                        if (getrequirmentprocess.Tables[0].Rows.Count > 0)
                        {

                            for (int i = 0; i <= getrequirmentprocess.Tables[0].Rows.Count - 1; i++)
                            {
                                {
                                    //Find the checkbox list items using FindByValue and select it.
                                    chkpcsprocess.Items.FindByValue(getrequirmentprocess.Tables[0].Rows[i]["ProcessId"].ToString()).Selected = true;
                                }
                            }
                        }



                        gvPcsProcessDetails.DataSource = null;
                        gvPcsProcessDetails.DataBind();


                        DataTable dtt;
                        DataRow drNew;
                        DataColumn dct;
                        DataSet dstd = new DataSet();
                        dtt = new DataTable();

                        dct = new DataColumn("Pid");
                        dtt.Columns.Add(dct);
                        dct = new DataColumn("Pname");
                        dtt.Columns.Add(dct);

                        dstd.Tables.Add(dtt);


                        if (chkpcsprocess.SelectedIndex >= 0)
                        {
                            foreach (ListItem listItem in chkpcsprocess.Items)
                            {
                                if (listItem.Selected)
                                {
                                    drNew = dtt.NewRow();
                                    drNew["Pid"] = listItem.Value;
                                    drNew["Pname"] = listItem.Text;
                                    dstd.Tables[0].Rows.Add(drNew);
                                }
                            }

                            gvPcsProcessDetails.DataSource = dstd;
                            gvPcsProcessDetails.DataBind();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Atleast One Process.Thank You.');", true);
                            return;
                        }

                        #endregion

                    }

                }

            }

            selected_tab.Value = Request.Form[selected_tab.UniqueID];
        }

        protected void Prod_click(object sender, EventArgs e)
        {

        }

        protected void Cmpny_chnaged(object sender, EventArgs e)
        {
            #region GET AVALIABLE STOCK

            if (gridstockdetails.Rows.Count > 0)
            {

                for (int vLoop = 0; vLoop < gridstockdetails.Rows.Count; vLoop++)
                {
                    Label lblitemid = (Label)gridstockdetails.Rows[vLoop].FindControl("lblitemid");
                    Label lblitemcolorid = (Label)gridstockdetails.Rows[vLoop].FindControl("lblitemcolorid");

                    Label lblprodavg = (Label)gridstockdetails.Rows[vLoop].FindControl("lblprodavg");
                    Label lblavlstock = (Label)gridstockdetails.Rows[vLoop].FindControl("lblavlstock");

                    Label lblpurchasestock = (Label)gridstockdetails.Rows[vLoop].FindControl("lblpurchasestock");

                    // Get STock

                    DataSet dsstock = objBs.GetAvlStock(lblitemid.Text, lblitemcolorid.Text, drpcompany.SelectedValue);
                    if (dsstock.Tables[0].Rows.Count > 0)
                    {
                        lblavlstock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                    }
                    else
                    {
                        lblavlstock.Text = "0.00";

                    }

                    double getremainstock = Convert.ToDouble(lblprodavg.Text) - Convert.ToDouble(lblavlstock.Text);
                    if (getremainstock < 0)
                    {
                        lblpurchasestock.Text = "0.00";
                    }
                    else
                    {
                        lblpurchasestock.Text = getremainstock.ToString("0.00");
                    }


                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Valid Exc.No.Thank You.');", true);
                return;
            }


            #endregion

            updpanel1.Update();

        }

        protected void color_change(object sender, EventArgs e)
        {
            DataTable dtt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dtt = new DataTable();

            dct = new DataColumn("styleno");
            dtt.Columns.Add(dct);
            dct = new DataColumn("SamplingCostingId");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dtt.Columns.Add(dct);

            dct = new DataColumn("AffectedQty");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Description");
            dtt.Columns.Add(dct);
            dct = new DataColumn("itemmasterid");
            dtt.Columns.Add(dct);


            dct = new DataColumn("Itemcolor");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Itemcolorid");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Quantity");
            dtt.Columns.Add(dct);

            dct = new DataColumn("PQuantity");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Units");
            dtt.Columns.Add(dct);


            dct = new DataColumn("Itemgroupname");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Itemgroupid");
            dtt.Columns.Add(dct);

            dct = new DataColumn("STotalpcs");
            dtt.Columns.Add(dct);

            dct = new DataColumn("PTotalpcs");
            dtt.Columns.Add(dct);

            dct = new DataColumn("Unitsid");
            dtt.Columns.Add(dct);

            //dct = new DataColumn("Units");
            //dtt.Columns.Add(dct);

            dstd.Tables.Add(dtt);

            for (int vLoop = 0; vLoop < gridviewstyle.Rows.Count; vLoop++)
            {
                Label lblstyleno = (Label)gridviewstyle.Rows[vLoop].FindControl("lblstyleno");
                Label lblstyleid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblstyleid");
                Label lblitemcolor = (Label)gridviewstyle.Rows[vLoop].FindControl("lblitemcolor");
                Label lblitemtype = (Label)gridviewstyle.Rows[vLoop].FindControl("lblitemtype");
                Label lblItemgroupId = (Label)gridviewstyle.Rows[vLoop].FindControl("lblItemgroupId");
                Label lblcategory = (Label)gridviewstyle.Rows[vLoop].FindControl("lblcategory");
                Label lblitemname = (Label)gridviewstyle.Rows[vLoop].FindControl("lblitemname");
                Label lblitemid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblitemid");
                Label lblcolorid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblcolorid");
                Label lblsampleavg = (Label)gridviewstyle.Rows[vLoop].FindControl("lblsampleavg");
                TextBox txtprodavg = (TextBox)gridviewstyle.Rows[vLoop].FindControl("txtprodavg");

                Label lbluom = (Label)gridviewstyle.Rows[vLoop].FindControl("lbluom");

                Label lblCqty = (Label)gridviewstyle.Rows[vLoop].FindControl("lblCqty");
                Label lblBQty = (Label)gridviewstyle.Rows[vLoop].FindControl("lblBQty");
                Label lblStotpcs = (Label)gridviewstyle.Rows[vLoop].FindControl("lblStotpcs");
                Label lblPtotpcs = (Label)gridviewstyle.Rows[vLoop].FindControl("lblPtotpcs");

                Label lblunitsid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblunitsid");



                DropDownList drpcolorlist = (DropDownList)gridviewstyle.Rows[vLoop].FindControl("drpcolorlist");
                //drpcolorlist.SelectedValue = lblcolorid.Text;


                if (lblsampleavg.Text == "")
                    lblsampleavg.Text = "0";

                if (txtprodavg.Text == "")
                    txtprodavg.Text = "0";

                if (lblCqty.Text == "")
                    lblCqty.Text = "0";

                drNew = dtt.NewRow();

                drNew["styleno"] = lblstyleno.Text;
                drNew["SamplingCostingId"] = lblstyleid.Text;
                drNew["Color"] = lblitemcolor.Text;
                drNew["AffectedQty"] = lblCqty.Text;
                drNew["Description"] = lblitemname.Text;
                drNew["itemmasterid"] = lblitemid.Text;
                drNew["Itemcolor"] = drpcolorlist.SelectedItem.Text;
                drNew["Itemcolorid"] = drpcolorlist.SelectedValue;
                drNew["Quantity"] = lblsampleavg.Text;
                drNew["PQuantity"] = txtprodavg.Text;
                drNew["Units"] = lbluom.Text;
                drNew["Unitsid"] = lblunitsid.Text;
                drNew["Itemgroupname"] = lblitemtype.Text;
                drNew["Itemgroupid"] = lblItemgroupId.Text;

                drNew["STotalpcs"] = (Convert.ToDouble(lblCqty.Text) * Convert.ToDouble(lblsampleavg.Text));
                drNew["PTotalpcs"] = (Convert.ToDouble(lblCqty.Text) * Convert.ToDouble(txtprodavg.Text)); ;
                //drNew[""] = ;




                dstd.Tables[0].Rows.Add(drNew);




            }

            #region Style Wise Item/Color Wise Details

            if (dstd.Tables[0].Rows.Count > 0)
            {
                gvstylewiseitemcolor.DataSource = dstd;
                gvstylewiseitemcolor.DataBind();
            }
            else
            {
                gvstylewiseitemcolor.DataSource = null;
                gvstylewiseitemcolor.DataBind();
            }

            #endregion

            DataSet ds1;
            DataTable dtt1;
            DataRow drNew1;
            DataColumn dc;
            ds1 = new DataSet();
            dtt1 = new DataTable();
            dc = new DataColumn("Itemgroupname");
            dtt1.Columns.Add(dc);
            dc = new DataColumn("Itemgroupid");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Description");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Totalpcs");
            dtt1.Columns.Add(dc);


            dc = new DataColumn("itemmasterid");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Itemcolor");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Itemcolorid");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("STotalpcs");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("PTotalpcs");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Units");
            dtt1.Columns.Add(dc);

            dc = new DataColumn("Unitsid");
            dtt1.Columns.Add(dc);

            ds1.Tables.Add(dtt1);

            #region Color Wise Item Details /Change Item Category/Calculate Required Item

            var result = from r in dstd.Tables[0].AsEnumerable()
                         group r by new
                         {
                             itemmasterid = r["itemmasterid"],
                             Itemgroupid = r["Itemgroupid"],
                             Itemcolorid = r["Itemcolorid"],
                             Itemcolor = r["Itemcolor"],
                             Itemgroupname = r["Itemgroupname"],
                             Description = r["Description"],
                             Units = r["Units"],
                             Unitsid = r["Unitsid"]
                         } into g
                         select new
                         {
                             Itemgroupname = g.Key.Itemgroupname,
                             Itemgroupid = g.Key.Itemgroupid,

                             Description = g.Key.Description,
                             itemmasterid = g.Key.itemmasterid,

                             Itemcolor = g.Key.Itemcolor,
                             Itemcolorid = g.Key.Itemcolorid,

                             Units = g.Key.Units,
                             Unitsid = g.Key.Unitsid,
                             STotalpcs = g.Sum(x => Convert.ToDouble(x["STotalpcs"])),
                             PTotalpcs = g.Sum(x => Convert.ToDouble(x["PTotalpcs"])),
                             Totalpcs = g.Sum(x => Convert.ToDouble(x["AffectedQty"])),
                         };

            foreach (var g in result)
            {
                drNew1 = dtt1.NewRow();
                drNew1["Itemgroupname"] = g.Itemgroupname;
                drNew1["Itemgroupid"] = g.Itemgroupid;

                drNew1["Description"] = g.Description;
                drNew1["itemmasterid"] = g.itemmasterid;

                drNew1["Itemcolor"] = g.Itemcolor;
                drNew1["Itemcolorid"] = g.Itemcolorid;

                drNew1["Units"] = g.Units;
                drNew1["Unitsid"] = g.Unitsid;

                drNew1["STotalpcs"] = g.STotalpcs;
                drNew1["PTotalpcs"] = g.PTotalpcs;

                drNew1["Totalpcs"] = g.Totalpcs;

                ds1.Tables[0].Rows.Add(drNew1);
            }

            if (ds1.Tables[0].Rows.Count > 0)
            {
                gvcolorwiseitem.DataSource = ds1;
                gvcolorwiseitem.DataBind();
            }
            else
            {
                gvcolorwiseitem.DataSource = null;
                gvcolorwiseitem.DataBind();
            }

            if (ds1.Tables[0].Rows.Count > 0)
            {
                gvitemcategory.DataSource = ds1;
                gvitemcategory.DataBind();
            }
            else
            {
                gvitemcategory.DataSource = null;
                gvitemcategory.DataBind();
            }

            if (ds1.Tables[0].Rows.Count > 0)
            {
                gvrequireditem.DataSource = ds1;
                gvrequireditem.DataBind();
            }
            else
            {
                gvrequireditem.DataSource = null;
                gvrequireditem.DataBind();
            }




            #endregion


            #region GET AVALIABLE STOCK


            if (ds1.Tables[0].Rows.Count > 0)
            {
                gridstockdetails.DataSource = ds1;
                gridstockdetails.DataBind();

                for (int vLoop = 0; vLoop < gridstockdetails.Rows.Count; vLoop++)
                {
                    Label lblitemid = (Label)gridstockdetails.Rows[vLoop].FindControl("lblitemid");
                    Label lblitemcolorid = (Label)gridstockdetails.Rows[vLoop].FindControl("lblitemcolorid");

                    Label lblprodavg = (Label)gridstockdetails.Rows[vLoop].FindControl("lblprodavg");
                    Label lblavlstock = (Label)gridstockdetails.Rows[vLoop].FindControl("lblavlstock");

                    Label lblpurchasestock = (Label)gridstockdetails.Rows[vLoop].FindControl("lblpurchasestock");

                    // Get STock

                    DataSet dsstock = objBs.GetAvlStock(lblitemid.Text, lblitemcolorid.Text, drpcompany.SelectedValue);
                    if (dsstock.Tables[0].Rows.Count > 0)
                    {
                        lblavlstock.Text = Convert.ToDouble(dsstock.Tables[0].Rows[0]["Qty"]).ToString("0.00");
                    }
                    else
                    {
                        lblavlstock.Text = "0.00";

                    }

                    double getremainstock = Convert.ToDouble(lblprodavg.Text) - Convert.ToDouble(lblavlstock.Text);
                    if (getremainstock < 0)
                    {
                        lblpurchasestock.Text = "0.00";
                    }
                    else
                    {
                        lblpurchasestock.Text = getremainstock.ToString("0.00");
                    }


                }
            }
            else
            {
                gridstockdetails.DataSource = null;
                gridstockdetails.DataBind();
            }

            #endregion

        }

        protected void Pcs_process_Click(object sender, EventArgs e)
        {

            gvPcsProcessDetails.DataSource = null;
            gvPcsProcessDetails.DataBind();


            if (btnSave.Text == "Save")
            {

                DataTable dtt;
                DataRow drNew;
                DataColumn dct;
                DataSet dstd = new DataSet();
                dtt = new DataTable();

                dct = new DataColumn("Pid");
                dtt.Columns.Add(dct);
                dct = new DataColumn("Pname");
                dtt.Columns.Add(dct);

                dct = new DataColumn("sts");
                dtt.Columns.Add(dct);

                dstd.Tables.Add(dtt);


                if (chkpcsprocess.SelectedIndex >= 0)
                {
                    foreach (ListItem listItem in chkpcsprocess.Items)
                    {
                        if (listItem.Selected)
                        {
                            drNew = dtt.NewRow();
                            drNew["Pid"] = listItem.Value;
                            drNew["Pname"] = listItem.Text;
                            drNew["sts"] = "NIL";
                            dstd.Tables[0].Rows.Add(drNew);
                        }
                    }

                    gvPcsProcessDetails.DataSource = dstd;
                    gvPcsProcessDetails.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Atleast One Process.Thank You.');", true);
                    return;
                }
            }
            else
            {

            }


        }



        protected void exc_selected_click(object sender, EventArgs e)
        {
            //  divpcsprocess.Visible = false;
            if (drpexclist.SelectedValue == "Select ExcNo")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Exc.No.Thank You.');", true);
                return;
            }
            else
            {
                // in buyer order get shipment date

                DataSet getbuyerorder = objBs.getshipmentdate(drpexclist.SelectedValue);
                if (getbuyerorder.Tables[0].Rows.Count > 0)
                {
                    //   divpcsprocess.Visible = true;
                    lblshipmentdate.Text = Convert.ToDateTime(getbuyerorder.Tables[0].Rows[0]["ShipmentDate"]).ToString("dd/MM/yyyy");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Valid Exc.No, Record Not Found.Thank You.');", true);
                    return;
                }



                #region Bind TAB 1
                // Bind Style no
                DataSet dstyleno = objBs.getstyledescr(drpexclist.SelectedValue);
                if (dstyleno.Tables[0].Rows.Count > 0)
                {
                    gvstylewithcolor.DataSource = dstyleno;
                    gvstylewithcolor.DataBind();
                }
                else
                {
                    gvstylewithcolor.DataSource = null;
                    gvstylewithcolor.DataBind();
                }

                // Bind change color

                DataSet getcolorchnagedetails = objBs.getcolorchnagedescription(drpexclist.SelectedValue);
                if (getcolorchnagedetails.Tables[0].Rows.Count > 0)
                {
                    gridviewstyle.DataSource = getcolorchnagedetails;
                    gridviewstyle.DataBind();

                    //for (int vLoop = 0; vLoop < gridviewstyle.Rows.Count; vLoop++)
                    //{
                    //    Label lblcolorid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblcolorid");
                    //    DropDownList drpcolorlist = (DropDownList)gridviewstyle.Rows[vLoop].FindControl("drpcolorlist");
                    //    drpcolorlist.SelectedValue = lblcolorid.Text;
                    //}
                }
                else
                {
                    gridviewstyle.DataSource = null;
                    gridviewstyle.DataBind();
                }

                if (dstyleno.Tables[0].Rows.Count > 0)
                {
                    color_change(sender, e);
                }

                #endregion


                //#region TAB 2
                //// Sample Avg And Production Avg

                //// Getting All Style no
                //string cond1 = "";
                //for (int vLoop = 0; vLoop < gvstylewithcolor.Rows.Count; vLoop++)
                //{


                //    Label lblstyleid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblstyleid");

                //    cond1 += " c.samplingcostingid='" + lblstyleid.Text + "' ,";

                //}


                //cond1 = cond1.TrimEnd(',');
                //cond1 = cond1.Replace(",", "or");
                //DataSet getsampleavgandprodavg = objBs.getsampleavgandprodavg(cond1);
                //if (getsampleavgandprodavg.Tables[0].Rows.Count > 0)
                //{
                //    griditemselectedstyle.DataSource = getsampleavgandprodavg;
                //    griditemselectedstyle.DataBind();
                //}
                //else
                //{
                //    griditemselectedstyle.DataSource = null;
                //    griditemselectedstyle.DataBind();
                //}

                //#endregion


                //#region TAB 3

                //DataSet getstyleitemcolorwise = objBs.getstylewiseitemcolor(drpexclist.SelectedValue);
                //if (getstyleitemcolorwise.Tables[0].Rows.Count > 0)
                //{
                //    gvstylewiseitemcolor.DataSource = getstyleitemcolorwise;
                //    gvstylewiseitemcolor.DataBind();

                //    gvcolorwiseitem.DataSource = getstyleitemcolorwise;
                //    gvcolorwiseitem.DataBind();

                //    gvitemcategory.DataSource = getstyleitemcolorwise;
                //    gvitemcategory.DataBind();

                //    gvrequireditem.DataSource = getstyleitemcolorwise;
                //    gvrequireditem.DataBind();
                //}
                //else
                //{
                //    gvstylewiseitemcolor.DataSource = null;
                //    gvstylewiseitemcolor.DataBind();


                //    gvcolorwiseitem.DataSource = null;
                //    gvcolorwiseitem.DataBind();

                //    gvitemcategory.DataSource = null;
                //    gvitemcategory.DataBind();


                //    gvrequireditem.DataSource = null;
                //    gvrequireditem.DataBind();
                //}

                //#endregion


            }

        }
        protected void GricViewStyle_Color(object sender, GridViewRowEventArgs e)
        {
            DataSet dsColor = objBs.gridColor();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddlColor = (DropDownList)e.Row.FindControl("drpcolorlist");
                ddlColor.DataSource = dsColor.Tables[0];
                ddlColor.DataTextField = "Color";
                ddlColor.DataValueField = "ColorID";
                ddlColor.DataBind();
                if (btnSave.Text == "Save")
                {
                    ddlColor.SelectedValue = lblColorId.Text;
                }
                else
                {
                    ddlColor.Items.Insert(0, "Select Color");
                }
            }

        }

        protected void txtRate_OnTextChanged(object sender, EventArgs e)
        {
            //TextBox ddl = (TextBox)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //TextBox txtRate = (TextBox)row.FindControl("txtRate");

            //double ItemCost = 0;
            //for (int i = 0; i < GVItem.Rows.Count; i++)
            //{
            //    TextBox txtRate1 = (TextBox)GVItem.Rows[i].FindControl("txtRate");
            //    TextBox txtQuantity1 = (TextBox)GVItem.Rows[i].FindControl("txtQuantity");
            //    TextBox txtCost1 = (TextBox)GVItem.Rows[i].FindControl("txtCost");

            //    if (txtRate1.Text == "")
            //        txtRate1.Text = "0";
            //    if (txtQuantity1.Text == "")
            //        txtQuantity1.Text = "0";

            //    txtCost1.Text = (Convert.ToDouble(txtRate1.Text) * Convert.ToDouble(txtQuantity1.Text)).ToString("f2");
            //    ItemCost += (Convert.ToDouble(txtCost1.Text));
            //}


            // txtRate.Focus();
        }
        protected void txtQuantity_OnTextChanged(object sender, EventArgs e)
        {
            //TextBox ddl = (TextBox)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");

            //double ItemCost = 0;
            //for (int i = 0; i < GVItem.Rows.Count; i++)
            //{
            //    TextBox txtRate1 = (TextBox)GVItem.Rows[i].FindControl("txtRate");
            //    TextBox txtQuantity1 = (TextBox)GVItem.Rows[i].FindControl("txtQuantity");
            //    TextBox txtCost1 = (TextBox)GVItem.Rows[i].FindControl("txtCost");

            //    if (txtRate1.Text == "")
            //        txtRate1.Text = "0";
            //    if (txtQuantity1.Text == "")
            //        txtQuantity1.Text = "0";

            //    txtCost1.Text = (Convert.ToDouble(txtRate1.Text) * Convert.ToDouble(txtQuantity1.Text)).ToString("f2");
            //    ItemCost += (Convert.ToDouble(txtCost1.Text));
            //}


            //txtQuantity.Focus();
        }

        protected void GVdsItemCodeDescriptionItemType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Select")
            //{
            //    DataSet dsstyle = objBs.GetStyleReqirements(Convert.ToInt32(e.CommandArgument.ToString()));
            //    if (dsstyle.Tables[0].Rows.Count > 0)
            //    {
            //        GVStyle.DataSource = dsstyle;
            //        GVStyle.DataBind();
            //    }
            //    else
            //    {
            //        GVStyle.DataSource = null;
            //        GVStyle.DataBind();
            //    }


            //    DataSet ds = objBs.GetBuyerOrderItems(Convert.ToInt32(e.CommandArgument.ToString()));
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        #region

            //        DataTable dtt;
            //        DataRow drNew;
            //        DataColumn dct;
            //        DataSet dstd = new DataSet();
            //        dtt = new DataTable();

            //        dct = new DataColumn("TransBuyerOrderId");
            //        dtt.Columns.Add(dct);

            //        dct = new DataColumn("ItemgroupId");
            //        dtt.Columns.Add(dct);
            //        dct = new DataColumn("ItemGroupCode");
            //        dtt.Columns.Add(dct);
            //        dct = new DataColumn("Itemgroupname");
            //        dtt.Columns.Add(dct);

            //        dct = new DataColumn("ColorID");
            //        dtt.Columns.Add(dct);
            //        dct = new DataColumn("Description");
            //        dtt.Columns.Add(dct);

            //        dstd.Tables.Add(dtt);

            //        foreach (DataRow Dr in ds.Tables[0].Rows)
            //        {
            //            drNew = dtt.NewRow();
            //            drNew["TransBuyerOrderId"] = Dr["TransBuyerOrderId"];

            //            drNew["ItemgroupId"] = Dr["ItemgroupId"];
            //            drNew["ItemGroupCode"] = Dr["ItemGroupCode"];
            //            drNew["Itemgroupname"] = Dr["Itemgroupname"];
            //            drNew["ColorID"] = Dr["ColorID"];
            //            drNew["Description"] = Dr["Description"];

            //            dstd.Tables[0].Rows.Add(drNew);
            //        }

            //        ViewState["CurrentTable2"] = dtt;

            //        GVItem.DataSource = dstd;
            //        GVItem.DataBind();

            //        for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            //        {
            //            HiddenField hdTransBuyerOrderId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdTransBuyerOrderId");
            //            HiddenField hdItemgroupId = (HiddenField)GVItem.Rows[vLoop].FindControl("hdItemgroupId");
            //            DropDownList ddlColor = (DropDownList)GVItem.Rows[vLoop].FindControl("ddlColor");

            //            hdTransBuyerOrderId.Value = dstd.Tables[0].Rows[vLoop]["TransBuyerOrderId"].ToString();
            //            hdItemgroupId.Value = dstd.Tables[0].Rows[vLoop]["ItemgroupId"].ToString();
            //            ddlColor.SelectedValue = dstd.Tables[0].Rows[vLoop]["ColorID"].ToString();

            //        }

            //        #endregion
            //    }

            //    //Items in the Selected Style
            //    DataSet dsGVItemsintheSelectedStyle = objBs.GetGVItemsintheSelectedStyle(Convert.ToInt32(e.CommandArgument.ToString()));
            //    if (dsGVItemsintheSelectedStyle.Tables[0].Rows.Count > 0)
            //    {
            //        GVItemsintheSelectedStyle.DataSource = dsGVItemsintheSelectedStyle;
            //        GVItemsintheSelectedStyle.DataBind();
            //    }
            //    else
            //    {
            //        GVItemsintheSelectedStyle.DataSource = null;
            //        GVItemsintheSelectedStyle.DataBind();
            //    }




            //    //Items in the Selected Style
            //    DataSet dsStyleWiseItemAndColorDetails = objBs.GetStyleWiseItemAndColorDetails(Convert.ToInt32(e.CommandArgument.ToString()));
            //    if (dsStyleWiseItemAndColorDetails.Tables[0].Rows.Count > 0)
            //    {
            //        GVStyleWiseItemAndColorDetails.DataSource = dsStyleWiseItemAndColorDetails;
            //        GVStyleWiseItemAndColorDetails.DataBind();
            //    }
            //    else
            //    {
            //        GVStyleWiseItemAndColorDetails.DataSource = null;
            //        GVStyleWiseItemAndColorDetails.DataBind();
            //    }
            //}
        }

        protected void ddlItemCode_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList TX = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)TX.NamingContainer;

            //DropDownList ddlItemCode = (DropDownList)row.FindControl("ddlItemCode");
            //DropDownList ddlDescription = (DropDownList)row.FindControl("ddlDescription");
            //DropDownList ddlItemType = (DropDownList)row.FindControl("ddlItemType");

            //if (ddlItemCode.SelectedValue != "" && ddlItemCode.SelectedValue != "0" && ddlItemCode.SelectedValue != "Select ItemCode")
            //{
            //    ddlDescription.SelectedValue = ddlItemCode.SelectedValue;
            //    ddlItemType.SelectedValue = ddlItemCode.SelectedValue;
            //}
            //else
            //{
            //    ddlDescription.ClearSelection();
            //    ddlItemType.ClearSelection();
            //}

        }
        protected void ddlDescription_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList TX = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)TX.NamingContainer;

            //DropDownList ddlItemCode = (DropDownList)row.FindControl("ddlItemCode");
            //DropDownList ddlDescription = (DropDownList)row.FindControl("ddlDescription");
            //DropDownList ddlItemType = (DropDownList)row.FindControl("ddlItemType");

            //if (ddlDescription.SelectedValue != "" && ddlDescription.SelectedValue != "0" && ddlDescription.SelectedValue != "Select Description")
            //{
            //    ddlItemCode.SelectedValue = ddlDescription.SelectedValue;
            //    ddlItemType.SelectedValue = ddlDescription.SelectedValue;
            //}
            //else
            //{
            //    ddlItemCode.ClearSelection();
            //    ddlItemType.ClearSelection();
            //}

        }
        protected void ddlItemType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList TX = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)TX.NamingContainer;

            //DropDownList ddlItemCode = (DropDownList)row.FindControl("ddlItemCode");
            //DropDownList ddlDescription = (DropDownList)row.FindControl("ddlDescription");
            //DropDownList ddlItemType = (DropDownList)row.FindControl("ddlItemType");

            //if (ddlItemType.SelectedValue != "" && ddlItemType.SelectedValue != "0" && ddlItemType.SelectedValue != "Select ItemType")
            //{
            //    ddlItemCode.SelectedValue = ddlItemType.SelectedValue;
            //    ddlDescription.SelectedValue = ddlItemType.SelectedValue;
            //}
            //else
            //{
            //    ddlItemCode.ClearSelection();
            //    ddlDescription.ClearSelection();
            //}

        }


        protected void btnAdd1_Click(object sender, EventArgs e)
        {
            //int Row = 1;
            //for (int vLoop = 0; vLoop < GVItem.Rows.Count; vLoop++)
            //{
            //    DropDownList ddlItemCode = (DropDownList)GVItem.Rows[vLoop].FindControl("ddlItemCode");
            //    DropDownList ddlColor = (DropDownList)GVItem.Rows[vLoop].FindControl("ddlColor");

            //    TextBox txtRate = (TextBox)GVItem.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtQuantity = (TextBox)GVItem.Rows[vLoop].FindControl("txtQuantity");
            //    TextBox txtCost = (TextBox)GVItem.Rows[vLoop].FindControl("txtCost");


            //    if (txtRate.Text == "")
            //        txtRate.Text = "0";
            //    if (txtQuantity.Text == "")
            //        txtQuantity.Text = "0";
            //    if (txtCost.Text == "")
            //        txtCost.Text = "0";



            //    if (ddlItemCode.SelectedValue == "" || ddlItemCode.SelectedValue == "0" || ddlItemCode.SelectedValue == "Select ItemCode")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select ItemCode in Row " + Row + " ')", true);
            //        ddlItemCode.Focus();
            //        return;
            //    }
            //    if (ddlColor.SelectedValue == "" || ddlColor.SelectedValue == "0" || ddlColor.SelectedValue == "Select Color")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Color in Row " + Row + " ')", true);
            //        ddlItemCode.Focus();
            //        return;
            //    }

            //    if (Convert.ToDouble(txtRate.Text) == 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Rate in Row " + Row + " ')", true);
            //        txtRate.Focus();
            //        return;
            //    }
            //    if (Convert.ToDouble(txtQuantity.Text) == 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Quantity in Row " + Row + " ')", true);
            //        txtQuantity.Focus();
            //        return;
            //    }
            //    if (Convert.ToDouble(txtCost.Text) == 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Cost in Row " + Row + " ')", true);
            //        txtCost.Focus();
            //        return;
            //    }
            //    Row++;
            //}
            //AddNewRow1();
        }
        private void FirstGridViewRow1()
        {
            //DataTable dt = new DataTable();
            //DataRow dr = null;

            //dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
            //dt.Columns.Add(new DataColumn("Color", typeof(string)));
            //dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            //dt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            //dt.Columns.Add(new DataColumn("Cost", typeof(string)));

            //dr = dt.NewRow();
            //dr["ItemCode"] = string.Empty;
            //dr["Color"] = string.Empty;
            //dr["Rate"] = string.Empty;
            //dr["Quantity"] = string.Empty;
            //dr["Cost"] = string.Empty;

            //dt.Rows.Add(dr);

            //ViewState["CurrentTable2"] = dt;

            //GVItem.DataSource = dt;
            //GVItem.DataBind();

            //DataTable dtt;
            //DataRow drNew;
            //DataColumn dct;
            //DataSet dstd = new DataSet();
            //dtt = new DataTable();

            //dct = new DataColumn("ItemCode");
            //dtt.Columns.Add(dct);
            //dct = new DataColumn("Color");
            //dtt.Columns.Add(dct);

            //dct = new DataColumn("Rate");
            //dtt.Columns.Add(dct);
            //dct = new DataColumn("Quantity");
            //dtt.Columns.Add(dct);
            //dct = new DataColumn("Cost");
            //dtt.Columns.Add(dct);

            //dstd.Tables.Add(dtt);

            //drNew = dtt.NewRow();
            //drNew["ItemCode"] = 0;
            //drNew["Color"] = "";

            //drNew["Rate"] = "";
            //drNew["Quantity"] = "";
            //drNew["Cost"] = "";

            //dstd.Tables[0].Rows.Add(drNew);

            //GVItem.DataSource = dstd;
            //GVItem.DataBind();

        }
        private void AddNewRow1()
        {
            //int rowIndex = 0;
            //if (ViewState["CurrentTable2"] != null)
            //{
            //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable2"];
            //    DataRow drCurrentRow = null;
            //    if (dtCurrentTable.Rows.Count > 0)
            //    {
            //        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //        {
            //            DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
            //            DropDownList ddlDescription = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlDescription");
            //            DropDownList ddlItemType = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemType");

            //            DropDownList ddlColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlColor");

            //            TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
            //            TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
            //            TextBox txtCost = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtCost");

            //            drCurrentRow = dtCurrentTable.NewRow();

            //            dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlItemCode.SelectedValue;
            //            dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlDescription.SelectedValue;
            //            dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlItemType.SelectedValue;

            //            dtCurrentTable.Rows[i - 1]["Color"] = ddlColor.SelectedValue;

            //            dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
            //            dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
            //            dtCurrentTable.Rows[i - 1]["Cost"] = txtCost.Text;

            //            rowIndex++;
            //        }
            //        dtCurrentTable.Rows.Add(drCurrentRow);
            //        ViewState["CurrentTable2"] = dtCurrentTable;

            //        GVItem.DataSource = dtCurrentTable;
            //        GVItem.DataBind();
            //    }
            //}
            //else
            //{
            //    Response.Write("ViewState is null");
            //}

            //SetPreviousData1();

        }
        private void SetRowData1()
        {
            //int rowIndex = 0;

            //if (ViewState["CurrentTable2"] != null)
            //{
            //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable2"];
            //    DataRow drCurrentRow = null;
            //    if (dtCurrentTable.Rows.Count > 0)
            //    {
            //        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //        {
            //            DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
            //            DropDownList ddlDescription = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlDescription");
            //            DropDownList ddlItemType = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemType");

            //            DropDownList ddlColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlColor");

            //            TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtRate");
            //            TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtQuantity");
            //            TextBox txtCost = (TextBox)GVItem.Rows[rowIndex].Cells[3].FindControl("txtCost");

            //            drCurrentRow = dtCurrentTable.NewRow();

            //            dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlItemCode.SelectedValue;
            //            dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlDescription.SelectedValue;
            //            dtCurrentTable.Rows[i - 1]["ItemCode"] = ddlItemType.SelectedValue;

            //            dtCurrentTable.Rows[i - 1]["Color"] = ddlColor.SelectedValue;

            //            dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
            //            dtCurrentTable.Rows[i - 1]["Quantity"] = txtQuantity.Text;
            //            dtCurrentTable.Rows[i - 1]["Cost"] = txtCost.Text;

            //            rowIndex++;

            //        }

            //        ViewState["CurrentTable2"] = dtCurrentTable;
            //        GVItem.DataSource = dtCurrentTable;
            //        GVItem.DataBind();
            //    }
            //}
            //else
            //{
            //    Response.Write("ViewState is null");
            //}
            //SetPreviousData1();
        }
        private void SetPreviousData1()
        {
            //double ItemCost = 0;
            //int rowIndex = 0;
            //if (ViewState["CurrentTable2"] != null)
            //{
            //    DataTable dt = (DataTable)ViewState["CurrentTable2"];
            //    if (dt.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            DropDownList ddlItemCode = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemCode");
            //            DropDownList ddlDescription = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlDescription");
            //            DropDownList ddlItemType = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlItemType");

            //            DropDownList ddlColor = (DropDownList)GVItem.Rows[rowIndex].Cells[1].FindControl("ddlColor");

            //            TextBox txtRate = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtRate");
            //            TextBox txtQuantity = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtQuantity");
            //            TextBox txtCost = (TextBox)GVItem.Rows[rowIndex].Cells[1].FindControl("txtCost");

            //            ddlItemCode.SelectedValue = dt.Rows[i]["ItemCode"].ToString();
            //            ddlDescription.SelectedValue = dt.Rows[i]["ItemCode"].ToString();
            //            ddlItemType.SelectedValue = dt.Rows[i]["ItemCode"].ToString();

            //            ddlColor.SelectedValue = dt.Rows[i]["Color"].ToString();

            //            txtRate.Text = dt.Rows[i]["Rate"].ToString();
            //            txtQuantity.Text = dt.Rows[i]["Quantity"].ToString();
            //            txtCost.Text = dt.Rows[i]["Cost"].ToString();

            //            if (txtCost.Text == "")
            //                txtCost.Text = "0";

            //            ItemCost += Convert.ToDouble(txtCost.Text);

            //            rowIndex++;

            //        }
            //    }
            //}


        }

        protected void GVItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //DataSet dsColor = objBs.gridColor();

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    var ddlColor = (DropDownList)e.Row.FindControl("ddlColor");
            //    ddlColor.DataSource = dsColor.Tables[0];
            //    ddlColor.DataTextField = "Color";
            //    ddlColor.DataValueField = "ColorID";
            //    ddlColor.DataBind();

            //}

        }
        protected void GVItem_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //SetRowData1();
            //if (ViewState["CurrentTable2"] != null)
            //{
            //    DataTable dt = (DataTable)ViewState["CurrentTable2"];
            //    DataRow drCurrentRow = null;
            //    int rowIndex = Convert.ToInt32(e.RowIndex);
            //    if (dt.Rows.Count > 1)
            //    {
            //        dt.Rows.Remove(dt.Rows[rowIndex]);
            //        drCurrentRow = dt.NewRow();
            //        ViewState["CurrentTable2"] = dt;
            //        GVItem.DataSource = dt;
            //        GVItem.DataBind();

            //        SetPreviousData1();

            //    }
            //    else if (dt.Rows.Count == 1)
            //    {
            //        dt.Rows.Remove(dt.Rows[rowIndex]);
            //        drCurrentRow = dt.NewRow();
            //        ViewState["CurrentTable2"] = dt;
            //        GVItem.DataSource = dt;
            //        GVItem.DataBind();

            //        SetPreviousData1();
            //        FirstGridViewRow1();
            //    }
            //}

        }

        protected void btnProcess_OnClick(object sender, EventArgs e)
        {
            // mpecost.Show();
        }
        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            //  mpecost.Hide();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {

            if (drpexclist.SelectedValue == "Select ExcNo")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Employee.It cannot be left blank.');", true);
                drpexclist.Focus();
                return;
            }


            if (btnSave.Text == "Save")
            {

                for (int vLoop = 0; vLoop < gridviewstyle.Rows.Count; vLoop++)
                {
                    int cnt = vLoop + 1;

                    DropDownList drpcolor = (DropDownList)gridviewstyle.Rows[vLoop].FindControl("drpcolorlist");

                    if (drpcolor.SelectedValue == "Select Color")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Valid Color in Row " + cnt + " .Thank You!!!.');", true);
                        drpcolor.Focus();
                        return;
                    }
                }


                DateTime ShipmentDate = DateTime.ParseExact(lblshipmentdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                int Requirementid = objBs.InsertRequirement(drpexclist.SelectedValue, ShipmentDate, lblUserID.Text);


                #region TAB 1
                // INSERT INTO TAB 1 Styleno and Process
                for (int vLoop = 0; vLoop < gvstylewithcolor.Rows.Count; vLoop++)
                {
                    Label lblstyleid = (Label)gvstylewithcolor.Rows[vLoop].FindControl("lblstyleid");
                    Label lblcolorid = (Label)gvstylewithcolor.Rows[vLoop].FindControl("lblcolorid");


                    int tabistyleno = objBs.Inserttab1styleno(Requirementid.ToString(), lblstyleid.Text, lblcolorid.Text);
                }


                for (int vLoop = 0; vLoop < gvPcsProcessDetails.Rows.Count; vLoop++)
                {
                    Label lblpid = (Label)gvPcsProcessDetails.Rows[vLoop].FindControl("lblpid");

                    int tabiprocess = objBs.Inserttab1processno(Requirementid.ToString(), lblpid.Text);
                }


                for (int vLoop = 0; vLoop < gridviewstyle.Rows.Count; vLoop++)
                {
                    Label lblstyleid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblstyleid");
                    Label lblitemid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblitemid");
                    Label lblsampleavg = (Label)gridviewstyle.Rows[vLoop].FindControl("lblsampleavg");

                    Label lblCqty = (Label)gridviewstyle.Rows[vLoop].FindControl("lblCqty");

                    Label lblBQty = (Label)gridviewstyle.Rows[vLoop].FindControl("lblBQty");

                    Label lblPtotpcs = (Label)gridviewstyle.Rows[vLoop].FindControl("lblPtotpcs");


                    TextBox txtprodavg = (TextBox)gridviewstyle.Rows[vLoop].FindControl("txtprodavg");

                    DropDownList drpcolor = (DropDownList)gridviewstyle.Rows[vLoop].FindControl("drpcolorlist");

                    Label lblstylecolorid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblstylecolorid");

                    if (txtprodavg.Text == "")
                        txtprodavg.Text = "0";

                    int tabistyleno = objBs.Inserttab1itemavg(Requirementid.ToString(), lblstyleid.Text, lblitemid.Text, drpcolor.SelectedValue, Convert.ToDouble(lblsampleavg.Text), Convert.ToDouble(txtprodavg.Text), Convert.ToDouble(lblCqty.Text), Convert.ToDouble(lblPtotpcs.Text), Convert.ToDouble(lblBQty.Text), Convert.ToInt32(lblstylecolorid.Text));
                }

                #endregion

                #region Tab 2 Save Calculated Items
                for (int vLoop = 0; vLoop < gvrequireditem.Rows.Count; vLoop++)
                {
                    Label lblitemid = (Label)gvrequireditem.Rows[vLoop].FindControl("lblitemid");
                    Label lblItemgroupId = (Label)gvrequireditem.Rows[vLoop].FindControl("lblItemgroupId");

                    Label lbltotpcs = (Label)gvrequireditem.Rows[vLoop].FindControl("lbltotpcs");

                    Label lblsampavg = (Label)gvrequireditem.Rows[vLoop].FindControl("lblsampavg");
                    Label lblprodavg = (Label)gvrequireditem.Rows[vLoop].FindControl("lblprodavg");
                    Label lblitemcolorid = (Label)gvrequireditem.Rows[vLoop].FindControl("lblitemcolorid");

                    int tabistyleno = objBs.Inserttab2ReqItemDetails(Requirementid.ToString(), lblitemid.Text, lblItemgroupId.Text, Convert.ToDouble(lbltotpcs.Text), lblitemcolorid.Text, Convert.ToDouble(lblsampavg.Text), Convert.ToDouble(lblprodavg.Text));

                }

                #endregion


                Response.Redirect("RequirementSheetGrid.aspx");

            }
            else
            {

                string Reqid = Request.QueryString.Get("ReqID");

                DataSet ds_RSChecking = objBs.PreCuttingCheckfor_RS(Convert.ToInt32(Reqid));
                if (ds_RSChecking.Tables[0].Rows.Count > 0)
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('BuyerOrderPreCutting was Assigned cannot be Update.')", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Buyer Pre-Cutting was Assigned cannot be Update.');", true);
                    return;
                }

                for (int vLoop = 0; vLoop < gridviewstyle.Rows.Count; vLoop++)
                {
                    int cnt = vLoop + 1;

                    DropDownList drpcolor = (DropDownList)gridviewstyle.Rows[vLoop].FindControl("drpcolorlist");

                    if (drpcolor.SelectedValue == "Select Color")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Valid Color in Row " + cnt + " .Thank You!!!.');", true);
                        drpcolor.Focus();
                        return;
                    }
                }


                // Delete All Thar Tab1,2,Process

                int idelete = objBs.Deletealltranstable(Reqid);

                // Inert All table
                #region TAB 1
                // INSERT INTO TAB 1 Styleno and Process
                for (int vLoop = 0; vLoop < gvstylewithcolor.Rows.Count; vLoop++)
                {
                    Label lblstyleid = (Label)gvstylewithcolor.Rows[vLoop].FindControl("lblstyleid");
                    Label lblcolorid = (Label)gvstylewithcolor.Rows[vLoop].FindControl("lblcolorid");


                    int tabistyleno = objBs.Inserttab1styleno(Reqid.ToString(), lblstyleid.Text, lblcolorid.Text);
                }


                for (int vLoop = 0; vLoop < gvPcsProcessDetails.Rows.Count; vLoop++)
                {
                    Label lblpid = (Label)gvPcsProcessDetails.Rows[vLoop].FindControl("lblpid");

                    int tabiprocess = objBs.Inserttab1processno(Reqid.ToString(), lblpid.Text);
                }


                for (int vLoop = 0; vLoop < gridviewstyle.Rows.Count; vLoop++)
                {
                    Label lblstyleid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblstyleid");
                    Label lblitemid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblitemid");
                    Label lblsampleavg = (Label)gridviewstyle.Rows[vLoop].FindControl("lblsampleavg");

                    Label lblCqty = (Label)gridviewstyle.Rows[vLoop].FindControl("lblCqty");

                    Label lblBQty = (Label)gridviewstyle.Rows[vLoop].FindControl("lblBQty");

                    Label lblPtotpcs = (Label)gridviewstyle.Rows[vLoop].FindControl("lblPtotpcs");


                    TextBox txtprodavg = (TextBox)gridviewstyle.Rows[vLoop].FindControl("txtprodavg");

                    DropDownList drpcolor = (DropDownList)gridviewstyle.Rows[vLoop].FindControl("drpcolorlist");

                    Label lblstylecolorid = (Label)gridviewstyle.Rows[vLoop].FindControl("lblstylecolorid");


                    if (txtprodavg.Text == "")
                        txtprodavg.Text = "0";

                    int tabistyleno = objBs.Inserttab1itemavg(Reqid.ToString(), lblstyleid.Text, lblitemid.Text, drpcolor.SelectedValue, Convert.ToDouble(lblsampleavg.Text), Convert.ToDouble(txtprodavg.Text), Convert.ToDouble(lblCqty.Text), Convert.ToDouble(lblPtotpcs.Text), Convert.ToDouble(lblBQty.Text), Convert.ToInt32(lblstylecolorid.Text));
                }

                #endregion

                #region Tab 2 Save Calculated Items
                for (int vLoop = 0; vLoop < gvrequireditem.Rows.Count; vLoop++)
                {
                    Label lblitemid = (Label)gvrequireditem.Rows[vLoop].FindControl("lblitemid");
                    Label lblItemgroupId = (Label)gvrequireditem.Rows[vLoop].FindControl("lblItemgroupId");

                    Label lbltotpcs = (Label)gvrequireditem.Rows[vLoop].FindControl("lbltotpcs");

                    Label lblsampavg = (Label)gvrequireditem.Rows[vLoop].FindControl("lblsampavg");
                    Label lblprodavg = (Label)gvrequireditem.Rows[vLoop].FindControl("lblprodavg");
                    Label lblitemcolorid = (Label)gvrequireditem.Rows[vLoop].FindControl("lblitemcolorid");

                    int tabistyleno = objBs.Inserttab2ReqItemDetails(Reqid.ToString(), lblitemid.Text, lblItemgroupId.Text, Convert.ToDouble(lbltotpcs.Text), lblitemcolorid.Text, Convert.ToDouble(lblsampavg.Text), Convert.ToDouble(lblprodavg.Text));

                }

                #endregion


                Response.Redirect("RequirementSheetGrid.aspx");
            }


        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("RequirementSheetGrid.aspx");
        }



    }
}
