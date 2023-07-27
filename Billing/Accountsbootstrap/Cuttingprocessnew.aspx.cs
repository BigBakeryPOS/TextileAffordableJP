using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using CommonLayer;
using System.IO;
using System.Globalization;


namespace Billing.Accountsbootstrap
{
    public partial class Cuttingprocessnew : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            iid = Request.QueryString.Get("CuttingID");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                if (radbtn.SelectedValue == "1")
                {
                    multiple.Visible = false;
                    single.Visible = true;
                }
                else
                {
                    multiple.Visible = true;
                    single.Visible = false;
                }

                //sTableName = Session["User"].ToString();

                divcode.Visible = false;
                DataSet dst = objBs.GetLedgers(Convert.ToInt32(lblUserID.Text), 1);
                if (dst != null)
                {
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        ddlSupplier.DataSource = dst.Tables[0];
                        ddlSupplier.DataTextField = "LedgerName";
                        ddlSupplier.DataValueField = "LedgerID";
                        ddlSupplier.DataBind();
                        ddlSupplier.Items.Insert(0, "Select Party Name");

                        chkSupplier.DataSource = dst.Tables[0];
                        chkSupplier.DataTextField = "LedgerName";
                        chkSupplier.DataValueField = "LedgerID";
                        chkSupplier.DataBind();
                        // ddlSupplier.Items.Insert(0, "Select Party Name");
                    }
                }

                DataSet dsDNo = objBs.GetDNo();
                if (dsDNo != null)
                {
                    if (dsDNo.Tables[0].Rows.Count > 0)
                    {
                        ddlDNo.DataSource = dsDNo.Tables[0];
                        ddlDNo.DataTextField = "Dno";
                        ddlDNo.DataValueField = "ProcessID";
                        ddlDNo.DataBind();
                        ddlDNo.Items.Insert(0, "Select Design");
                    }
                }

                DataSet dsFit = objBs.GetFit();
                if (dsFit != null)
                {
                    if (dsFit.Tables[0].Rows.Count > 0)
                    {
                        ddlFit.DataSource = dsFit.Tables[0];
                        ddlFit.DataTextField = "Fit";
                        ddlFit.DataValueField = "FitID";
                        ddlFit.DataBind();
                        ddlFit.Items.Insert(0, "Select Fit");
                    }
                }

                string date = DateTime.Now.ToString("dd/MM/yyyy");

                // txtdate.Text = date;

                lblUser.Text = Session["UserName"].ToString();
                lblUserID.Text = Session["UserID"].ToString();


                if (iid != null)
                {
                    DataSet ds1 = objBs.getCuttingProcess(Convert.ToInt32(iid));
                    if (ds1 != null)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            DataSet dsDNo1 = objBs.allGetDNo();
                            if (dsDNo1 != null)
                            {
                                if (dsDNo1.Tables[0].Rows.Count > 0)
                                {
                                    ddlDNo.DataSource = dsDNo1.Tables[0];
                                    ddlDNo.DataTextField = "Dno";
                                    ddlDNo.DataValueField = "ProcessID";
                                    ddlDNo.DataBind();
                                    ddlDNo.Items.Insert(0, "Select Design");
                                }
                            }

                            btnadd.Text = "Update";
                            double totmeter = Convert.ToDouble(ds1.Tables[0].Rows[0]["Req_Meter"]) + Convert.ToDouble(ds1.Tables[0].Rows[0]["met"]);
                            txtID.Text = ds1.Tables[0].Rows[0]["CuttingID"].ToString();
                            TextBox3.Text = ds1.Tables[0].Rows[0]["CuttingID"].ToString();
                            txtreq_meter.Text = ds1.Tables[0].Rows[0]["Req_Meter"].ToString();
                            ddlDNo.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["DNo"]).ToString();
                            txtLotNo.Text = ds1.Tables[0].Rows[0]["LotNo"].ToString();
                            txtMeter.Text = totmeter.ToString();
                            txtRate.Text = ds1.Tables[0].Rows[0]["Rate"].ToString();
                            txtColor.Text = ds1.Tables[0].Rows[0]["Color"].ToString();
                            radbtn.SelectedValue = ds1.Tables[0].Rows[0]["Type"].ToString();
                            if (radbtn.SelectedValue == "1")
                            {
                                ddlSupplier.SelectedValue = Convert.ToInt32(ds1.Tables[0].Rows[0]["PartyName"]).ToString();
                                single.Visible = true;
                                multiple.Visible = false;
                            }
                            else
                            {
                                single.Visible = false;
                                multiple.Visible = true;
                                string str = ds1.Tables[0].Rows[0]["PartyName"].ToString();
                                string[] strList = str.Split(',');


                                foreach (string s in strList)
                                {
                                    foreach (ListItem item in chkSupplier.Items)
                                    {
                                        if (item.Value == s)
                                        {
                                            item.Selected = true;
                                            break;
                                        }

                                    }

                                }

                            }
                            txtWidth.Text = ds1.Tables[0].Rows[0]["WidthID"].ToString();
                            ddlFit.SelectedValue = ds1.Tables[0].Rows[0]["Fit"].ToString();
                            txtdate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["Deliverydate"]).ToString("dd/MM/yyyy");
                        }
                    }
                }
                else
                {

                    DataSet ds = objBs.CuttingID();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["CuttingID"].ToString() == "")
                            TextBox3.Text = "1";
                        else
                            TextBox3.Text = ds.Tables[0].Rows[0]["CuttingID"].ToString();

                        btnadd.Text = "Save";
                        FirstGridViewRow();
                    }

                }
            }
        }



        protected void Add_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString.Get("Mode");

            DateTime deliverydate = DateTime.ParseExact(txtdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (btnadd.Text == "Save")
            {
                int iStatus = 0;
                if (radbtn.SelectedValue == "1")
                {
                    double meter = Convert.ToDouble(txtMeter.Text);
                    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

                    double number = meter - reqmeter;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
                        return;
                    }

                    //    string condno = getCond();
                    //   string condname = getCondname();

                    //  return;


                    iStatus = objBs.insertCuttingprocess(Convert.ToInt32(txtLotNo.Text), ddlSupplier.SelectedValue, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), txtreq_meter.Text, ddlSupplier.SelectedItem.Text, radbtn.SelectedValue, deliverydate);
                    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
                }
                else
                {


                    double meter = Convert.ToDouble(txtMeter.Text);
                    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

                    double number = meter - reqmeter;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
                        return;
                    }

                    string condno = getCond();
                    string condname = getCondname();

                    //  return;


                    iStatus = objBs.insertCuttingprocess(Convert.ToInt32(txtLotNo.Text), condno, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), txtreq_meter.Text, condname, radbtn.SelectedValue, deliverydate);
                    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");

                }
            }
            else
            {
                int iStatus = 0;
                if (radbtn.SelectedValue == "1")
                {
                    double meter = Convert.ToDouble(txtMeter.Text);
                    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

                    double number = meter - reqmeter;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
                        return;
                    }


                    iStatus = objBs.UpdateCuttingprocess(Convert.ToInt32(txtLotNo.Text), ddlSupplier.SelectedValue, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), Convert.ToInt32(iid), txtreq_meter.Text, ddlSupplier.SelectedItem.Text, radbtn.SelectedValue, deliverydate);
                    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
                }
                else
                {
                    double meter = Convert.ToDouble(txtMeter.Text);
                    double reqmeter = Convert.ToDouble(txtreq_meter.Text);

                    double number = meter - reqmeter;
                    if (number < 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Required Meter is Greater than Avaliable Meter. Thank you');", true);
                        return;
                    }
                    string condno = getCond();
                    string condname = getCondname();

                    iStatus = objBs.UpdateCuttingprocess(Convert.ToInt32(txtLotNo.Text), condno, ddlDNo.SelectedValue, number.ToString(), Convert.ToDouble(txtRate.Text), txtColor.Text, Convert.ToInt32(txtWidth.Text), Convert.ToInt32(ddlFit.SelectedValue), Convert.ToInt32(iid), txtreq_meter.Text, condname, radbtn.SelectedValue, deliverydate);
                    Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
                }
            }
        }

        protected string getCond()
        {
            string cond = "";

            foreach (ListItem listItem in chkSupplier.Items)
            {
                if (listItem.Text != "All")
                {
                    if (listItem.Selected)
                    {
                        cond += listItem.Value + ",";
                    }
                }
            }
            cond = cond.TrimEnd(',');
            //   cond = cond.Replace(",", ",");
            return cond;
        }

        protected string getCondname()
        {
            string cond = "";

            foreach (ListItem listItem in chkSupplier.Items)
            {
                if (listItem.Text != "All")
                {
                    if (listItem.Selected)
                    {
                        cond += listItem.Text + ",";
                    }
                }
            }
            // cond = cond.TrimEnd(',');
            //   cond = cond.Replace(",", ",");
            cond = cond.TrimEnd(',');
            return cond;
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewcutting.aspx");
        }

        protected void ddlDNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds_Cutting = objBs.SelectDesign_CuttingProcess(ddlDNo.SelectedValue);
            int Width_Id = Convert.ToInt32(ds_Cutting.Tables[0].Rows[0]["WidthID"].ToString());

            DataSet ds_Width = objBs.editwidth(Width_Id);
            txtWidth.Text = ds_Width.Tables[0].Rows[0]["Width"].ToString();
            txtRate.Text = ds_Cutting.Tables[0].Rows[0]["Rate"].ToString();
            txtMeter.Text = ds_Cutting.Tables[0].Rows[0]["AvaliableMeter"].ToString();

            txtreq_meter.Focus();
        }

        protected void radchecked(object sender, EventArgs e)
        {
            if (radbtn.SelectedValue == "1")
            {
                single.Visible = true;
                multiple.Visible = false;
            }
            else
            {
                multiple.Visible = true;
                single.Visible = false;

            }
        }

        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("Designno", typeof(string)));
            dtt.Columns.Add(new DataColumn("Ameter", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rmeter", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtt.Columns.Add(new DataColumn("Color", typeof(string)));
            dtt.Columns.Add(new DataColumn("Width", typeof(string)));
        
           
            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["Designno"] = string.Empty;
            dr["Ameter"] = string.Empty;
            dr["Rmeter"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["Color"] = string.Empty;
            dr["Width"] = string.Empty;
          
          
            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            gvcustomerorder.DataSource = dtt;
            gvcustomerorder.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("OrderNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Designno");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Ameter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rmeter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Width");
            dttt.Columns.Add(dct);

           

           

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
          
            drNew["OrderNo"] = 0;
            drNew["Designno"] = "";
            drNew["Ameter"] = 0;
            drNew["Rmeter"] = 0;
            drNew["Rate"] = 0;
            drNew["Color"] = 0;
            drNew["Width"] = "";
          
           
           
            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();


            //TextBox txn = (TextBox)grvStudentDetails.Rows[0].Cells[1].FindControl("txtName");
            //txn.Focus();
            //Button btnAdd = (Button)grvStudentDetails.FooterRow.Cells[5].FindControl("ButtonAdd");
            //Page.Form.DefaultFocus = btnAdd.ClientID;

        }
        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsDNo = objBs.GetDNo();

            DataSet dsFit = objBs.GetFit();



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //TextBox txtno = (TextBox)e.Row.FindControl("txtno");

                //TextBox txtttk = (TextBox)e.Row.FindControl("txtqty");
                //TextBox txttk = (TextBox)e.Row.FindControl("txtRate");
                //TextBox txtkt = (TextBox)e.Row.FindControl("txtTax");
                //TextBox txtkttt = (TextBox)e.Row.FindControl("txtAmount");
                //TextBox txtktt = (TextBox)e.Row.FindControl("txtStock");
                //TextBox txtktttt = (TextBox)e.Row.FindControl("txtDiscount");

                //txtno.Text = "1";
                //txtttk.Text = "0";
                //txttk.Text = "0";
                //txtkt.Text = "0";
                //txtkttt.Text = "0";
                //txtktt.Text = "0";
                //txtktttt.Text = "0";
                // txtno.Text = "1";


                var ddl = (DropDownList)e.Row.FindControl("drpItem");
                ddl.DataSource = dsDNo;
                ddl.DataTextField = "Dno";
                ddl.DataValueField = "processid";
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Design");



                //var ddlt = (DropDownList)e.Row.FindControl("drpfit");
                //ddlt.DataSource = dsFit;
                //ddlt.DataTextField = "Fit";
                //ddlt.DataValueField = "fitid";
                //ddlt.DataBind();
                //ddlt.Items.Insert(0, "Select Fit");


              

            }

        }

        protected void gvcustomerorder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dsCategory = new DataSet();

            DataSet dscat = new DataSet();

            string OrderNo = Request.QueryString.Get("OrderNo");
            //if (OrderNo != "")
            //{
            //    /// dsCategory = objBs.GetCAT_OrderForm();
            //}
            //else
        //    dsCategory = objBs.selectcategorybrandcat(sTableName);

            DataSet dsDNo = objBs.GetDNo();

            DataSet dsFit = objBs.GetFit();



            //else
            //    dsCategory = objBs.selectcategorymaster_Dealer("tblStock_" + sTableName);


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlCategory1 = (DropDownList)(e.Row.FindControl("drpItem") as DropDownList);
                ddlCategory1.Focus();
                ddlCategory1.Enabled = true;
                ddlCategory1.DataSource = dsDNo.Tables[0];
                ddlCategory1.DataTextField = "Dno";
                ddlCategory1.DataValueField = "ProcessID";
                ddlCategory1.DataBind();
                ddlCategory1.Items.Insert(0, "Select Design");

                //DropDownList ddlDef1 = (DropDownList)(e.Row.FindControl("drpfit") as DropDownList);
                //ddlDef1.Focus();
                //ddlDef1.Enabled = true;
                //ddlDef1.DataSource = dsFit.Tables[0];
                //ddlDef1.DataTextField = "Fit";
                //ddlDef1.DataValueField = "fitid";
                //ddlDef1.DataBind();
                //ddlDef1.Items.Insert(0, "Select Fit");

               

            }
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                      
                        TextBox txtameter =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtaemeter");

                     
                        DropDownList drpItem =
                          (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");

                        TextBox txtrmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrmeter");

                        

                     
                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");
                        TextBox txtcolor =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");
                        TextBox txtwidth =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtwidth");

                        TextBox txttno =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");

                   
                      

                     

                        drpItem.Items.Clear();

                        DataSet dst = objBs.GetDNo();
                        drpItem.Items.Add(new ListItem("Select Design", "0"));
                        drpItem.DataSource = dst;
                        drpItem.DataBind();
                        drpItem.DataTextField = "Dno";
                        drpItem.DataValueField = "Processid";


                        txtameter.Text = dt.Rows[i]["ameter"].ToString();
                        txtrmeter.Text = dt.Rows[i]["rmeter"].ToString();
                       
                        drpItem.SelectedValue = dt.Rows[i]["Designno"].ToString();
                        txttno.Text = dt.Rows[i]["OrderNo"].ToString();
                        txtRate.Text = dt.Rows[i]["Rate"].ToString();
                        txtcolor.Text = dt.Rows[i]["color"].ToString();
                        txtwidth.Text = dt.Rows[i]["width"].ToString();
                        txtwidth.Text = dt.Rows[i]["width"].ToString();
                     

                        rowIndex++;

                    }
                }
            }
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {


                        TextBox txtameter =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtaemeter");


                        DropDownList drpItem =
                          (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");

                        TextBox txtrmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrmeter");




                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");
                        TextBox txtcolor =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");
                        TextBox txtwidth =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtwidth");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Designno"] = drpItem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Ameter"] = txtameter.Text;
                        dtCurrentTable.Rows[i - 1]["Rmeter"] = txtrmeter.Text;
                  
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
                        dtCurrentTable.Rows[i - 1]["Width"] = txtwidth.Text;

                      

                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    gvcustomerorder.DataSource = dtCurrentTable;
                    gvcustomerorder.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        protected void drpItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.NamingContainer;
            DropDownList drpCategory = (DropDownList)row.FindControl("drpItem");


            //DropDownList Defitem = (DropDownList)row.FindControl("drpItem");

            //DropDownList procode = (DropDownList)row.FindControl("ProductCode");


            DataSet ds_Cutting = objBs.SelectDesign_CuttingProcess(drpCategory.SelectedValue);
            int Width_Id = Convert.ToInt32(ds_Cutting.Tables[0].Rows[0]["WidthID"].ToString());

            DataSet ds_Width = objBs.editwidth(Width_Id);
          



            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");

                    TextBox txtameter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtaemeter");
                    TextBox txtrmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                    TextBox txtwidth = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtwidth");
                    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");


                    txtwidth.Text = ds_Width.Tables[0].Rows[0]["Width"].ToString();
                    txtrate.Text = ds_Cutting.Tables[0].Rows[0]["Rate"].ToString();
                    txtameter.Text = ds_Cutting.Tables[0].Rows[0]["AvaliableMeter"].ToString();

                  
                }
            }
        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int No = 0;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");

                TextBox txtameter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtaemeter");
                TextBox txtrmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtwidth = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtwidth");
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
             

                if (ProductCode.SelectedItem.Text == "Select Design")
                {
                    No = 0;
                    break;
                }
                else
                {
                    No = 1;
                }
            }

            if (No == 1)
            {

                AddNewRow();
            }
            else
            {

            }
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
              //  TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
              //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                txtno.Focus();
            }

           

        }

        private void AddNewRow()
        {
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");

                TextBox txtameter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtaemeter");
                TextBox txtrmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtwidth = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtwidth");
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                // Label txtno = (Label)gvcustomerorder.Rows[vLoop].FindControl("txtno");
              

                int col = vLoop + 1;


                txtno.Focus();
            }

            int rowIndex = 0;

            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        TextBox txtameter =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtaemeter");


                        DropDownList drpItem =
                          (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("drpItem");

                        TextBox txtrmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrmeter");




                        TextBox txtRate =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");
                        TextBox txtcolor =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");
                        TextBox txtwidth =
                           (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtwidth");


                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Designno"] = drpItem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Ameter"] = txtameter.Text;
                        dtCurrentTable.Rows[i - 1]["Rmeter"] = txtrmeter.Text;

                        dtCurrentTable.Rows[i - 1]["Rate"] = txtRate.Text;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
                        dtCurrentTable.Rows[i - 1]["Width"] = txtwidth.Text;



                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    gvcustomerorder.DataSource = dtCurrentTable;
                    gvcustomerorder.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousData();
        }

        protected void reqmeter(object sender, EventArgs e)
        {
            ButtonAdd1_Click(sender, e);

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                int cnt = gvcustomerorder.Rows.Count;
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                if (vLoop >= 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
                    //    oldtxttk.Text = ".00";
                    oldtxttk.Focus();
                }
                int tot = cnt - vLoop;
                if (tot == 1)
                {
                    TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
                    if (oldtxttk.Text == "0.00")
                    {
                        oldtxttk.Text = ".00";
                        oldtxttk.Focus();
                    }
                    else
                    {
                        oldtxttk.Focus();
                    }
                }
              //  DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            }
        }
    }
}