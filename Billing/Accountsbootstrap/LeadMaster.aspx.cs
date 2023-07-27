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
using System.Globalization;
using System.Data.OleDb;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class LeadMaster : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string userid = "";
        string Entry_id = "";
        string EntryType = "";
        string isadmin = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            userid = Session["UserID"].ToString();
           // txtLoginID.Text = Session["LoginID"].ToString();
            isadmin = Session["IsSuperAdmin"].ToString();
            string name = Request.QueryString.Get("name");
          
            Entry_id = Request.QueryString.Get("Id");
            EntryType = Request.QueryString.Get("Name");
            if (!Page.IsPostBack)
            {
                BindTime();
                txtxnextappoint.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //DataSet ds_Emp = objBs.showentrygridcurrentdate(userid, isadmin);
                //if (ds_Emp.Tables[0].Rows.Count > 0)
                //{
                //    gv_Employee.DataSource = ds_Emp;
                //    gv_Employee.DataBind();
                //}
                //DataSet ds_follow = objBs.showentrygridpreviousdate(userid, isadmin);
                //if (ds_follow.Tables[0].Rows.Count > 0)
                //{
                //    gridfollowup.DataSource = ds_follow;
                //    gridfollowup.DataBind();
                //}

                DataSet dcheck = objBs.ReferencegridNew();
                if (dcheck.Tables[0].Rows.Count > 0)
                {
                    ddlreference.DataSource = dcheck.Tables[0];
                    ddlreference.DataValueField = "ReferenceId";
                    ddlreference.DataTextField = "ReferenceName";
                    ddlreference.DataBind();
                   // ddlreference.Items.Insert(0, "Select Reference");
                }


                DataSet dscheck = objBs.getstatusnew("4","F");
                if (dscheck.Tables[0].Rows.Count > 0)
                {
                    ddlstatus.DataSource = dscheck.Tables[0];
                    ddlstatus.DataValueField = "Statusid";
                    ddlstatus.DataTextField = "StatusName";
                    ddlstatus.DataBind();
                   // ddlstatus.Items.Insert(0, "Select Status");
                }

                DataSet dEmployee = objBs.BindEmployee();
                if (dEmployee.Tables[0].Rows.Count > 0)
                {
                    ddlEmpname.DataSource = dEmployee.Tables[0];
                    ddlEmpname.DataValueField = "Employee_Id";
                    ddlEmpname.DataTextField = "Name";
                    ddlEmpname.DataBind();
                    // ddlreference.Items.Insert(0, "Select Reference");
                }

                Upte.Visible = false;

                if (Entry_id != null)
                {
                    //if (isadmin == "3")
                    //{
                    //    txtcompanyname.Enabled = false;
                    //   // ddlmanagerStataus.Enabled = true;
                    //    txtcustomername2.Enabled = false;
                    //    txtaddress.Enabled = false;
                    //    txtcustcontactno1.Enabled = false;
                    //    txtxemailid.Enabled = false;
                    //    txtxdesignation.Enabled = false;
                    //    txtxcompanyno.Enabled = false;
                    //    txtxresultofmeet.Enabled = false;
                    //    txtxnextappoint.Enabled = false;
                    //    ddlstatus.Enabled = false;
                    //    ddlreference.Enabled = false;
                    //    txtcustcontactno2.Enabled = false;
                    //    txtcustomername1.Enabled = false;
                    //}
                    //else if (isadmin == "1")
                    //{
                    //    txtcompanyname.Enabled = false;
                    //  //  ddlmanagerStataus.Enabled = true;
                    //   txtcustomername2.Enabled = false;
                    //    txtaddress.Enabled = false;
                    //    txtcustcontactno1.Enabled = false;
                    //    txtxemailid.Enabled = false;
                    //    txtxdesignation.Enabled = false;
                    //    txtxcompanyno.Enabled = false;
                    //    txtxresultofmeet.Enabled = false;
                    //    txtxnextappoint.Enabled = false;
                    //    ddlstatus.Enabled = false;
                    //    ddlreference.Enabled = false;
                    //    txtcustcontactno2.Enabled = false;
                    //    txtcustomername1.Enabled = false;
                    //    //ddlmanagerStataus.Enabled = false;
                    //    btnSubmit.Enabled = false;
                    //}
                    //else
                    //{
                    //   // ddlmanagerStataus.Enabled = false;
                    //}
                    //  Upte.Visible = true;
                    Sve.Visible = true;
                    noexcel.Visible = false;
                    if (EntryType == "EDT")
                    {
                        btnSubmit.Text = "Update";
                    }
                    else if (EntryType == "NXT")
                    {
                        btnSubmit.Text = "Next Meeting";
                    }
                    DataSet ds2 = objBs.GetEntryforupdate(Entry_id);
                    {
                        if (ds2.Tables[0].Rows.Count > 0)
                        {
                            lblleadid.Text = ds2.Tables[0].Rows[0]["leadid"].ToString();
                            txtcompanyname.Text = ds2.Tables[0].Rows[0]["Companyname"].ToString();
                            txtcustomername1.Text = ds2.Tables[0].Rows[0]["contactname1"].ToString();
                            txtcustomername2.Text = ds2.Tables[0].Rows[0]["contactname2"].ToString();
                            txtaddress.Text = ds2.Tables[0].Rows[0]["address"].ToString();
                            txtcustcontactno1.Text = ds2.Tables[0].Rows[0]["primarycontact"].ToString();
                            txtcustcontactno2.Text = ds2.Tables[0].Rows[0]["secondarycontact"].ToString();
                            txtxcompanyno.Text = ds2.Tables[0].Rows[0]["CompanyphoneNo"].ToString();
                            txtxdesignation.Text = ds2.Tables[0].Rows[0]["Designation"].ToString();
                            txtxresultofmeet.Text = ds2.Tables[0].Rows[0]["ResultOfMeet"].ToString();
                            txtxnextappoint.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["nextappointment"]).ToString("dd/MM/yyyy");
                            ddlTimeFrom.SelectedValue = ds2.Tables[0].Rows[0]["appointmenttime"].ToString();
                            ddlreference.SelectedValue = ds2.Tables[0].Rows[0]["referenceType"].ToString();
                            ddlstatus.SelectedValue = ds2.Tables[0].Rows[0]["Status"].ToString();
                            txtxemailid.Text = ds2.Tables[0].Rows[0]["Emailid"].ToString();
                            txtremarks.Text = ds2.Tables[0].Rows[0]["comments"].ToString();
                            ddlEmpname.SelectedValue = ds2.Tables[0].Rows[0]["EmployeeID"].ToString();
                            //txtcustcontactno2.Text = ds2.Tables[0].Rows[0]["Customermobile2"].ToString();

                            DataSet getitem = objBs.GetEntryforupdateforitem(Entry_id);
                            if (getitem.Tables[0].Rows.Count > 0)
                            {
                                DataTable dttt;
                                DataRow drNew;
                                DataColumn dct;
                                DataSet dstd = new DataSet();
                                dttt = new DataTable();

                                dct = new DataColumn("Item");
                                dttt.Columns.Add(dct);


                                dct = new DataColumn("Qty");
                                dttt.Columns.Add(dct);

                                dct = new DataColumn("Rate");
                                dttt.Columns.Add(dct);

                                
                                dstd.Tables.Add(dttt);

                                foreach (DataRow dr in getitem.Tables[0].Rows)
                                {
                                  
                                    drNew = dttt.NewRow();
                                    drNew["Item"] = dr["Itemid"];
                                    drNew["Qty"] = dr["Qty"];
                                    drNew["Rate"] = dr["rate"];
                                    
                                    dstd.Tables[0].Rows.Add(drNew);
                                }

                                ViewState["CurrentTable1"] = dttt;

                                grdiitem.DataSource = dstd;
                                grdiitem.DataBind();


                                for (int vLoop = 0; vLoop < grdiitem.Rows.Count; vLoop++)
                                {
                                    DropDownList drpitem = (DropDownList)grdiitem.Rows[vLoop].FindControl("drpitem");
                                    TextBox txtqty = (TextBox)grdiitem.Rows[vLoop].FindControl("txtqty");
                                    TextBox txtrate = (TextBox)grdiitem.Rows[vLoop].FindControl("txtrate");

                                    drpitem.SelectedValue = dstd.Tables[0].Rows[vLoop]["Item"].ToString();
                                    txtqty.Text = dstd.Tables[0].Rows[vLoop]["Qty"].ToString();
                                    txtrate.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["Rate"]).ToString("0.00");
                                }
                            }

                        }
                    }
                }
                else
                {
                    // Sve.Visible = true;
                    Upte.Visible = false;
                    FirstGridViewRow();
                    // SetInitialRow();
                }
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        #region GRID BINDING
        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;

            dtt.Columns.Add(new DataColumn("Item", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Schema", typeof(string)));
            dtt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            
            dr = dtt.NewRow();

            dr["Item"] = string.Empty;
            dr["Qty"] = string.Empty;
            // dr["Stock"] = string.Empty;
            dr["Rate"] = string.Empty;
            
            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            grdiitem.DataSource = dtt;
            grdiitem.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();

            dct = new DataColumn("Item");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Qty");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);


            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();

            //drNew["Schema"] = "";
            drNew["Item"] = "";
            drNew["Qty"] = 0;
            drNew["Rate"] = 0;


            dstd.Tables[0].Rows.Add(drNew);

            grdiitem.DataSource = dstd;
            grdiitem.DataBind();
        }

        private void SetRowDataitem()
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

                        DropDownList drpitem =
                         (DropDownList)grdiitem.Rows[rowIndex].Cells[1].FindControl("drpitem");
                        //TextBox txtStock =
                        //  (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtStock");
                        TextBox txtqty =
                          (TextBox)grdiitem.Rows[rowIndex].Cells[2].FindControl("txtqty");

                        TextBox txtrate =
                          (TextBox)grdiitem.Rows[rowIndex].Cells[4].FindControl("txtrate");
                       

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["Item"] = drpitem.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Qty"] = txtqty.Text;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtrate.Text;
                        
                        rowIndex++;

                    }

                    ViewState["CurrentTable1"] = dtCurrentTable;
                    grdiitem.DataSource = dtCurrentTable;
                    grdiitem.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataitem();
        }

        private void SetPreviousDataitem()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        

                        DropDownList drpitem =
                        (DropDownList)grdiitem.Rows[rowIndex].Cells[1].FindControl("drpitem");
                        //TextBox txtStock =
                        //  (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtStock");
                        TextBox txtqty =
                          (TextBox)grdiitem.Rows[rowIndex].Cells[2].FindControl("txtqty");

                        TextBox txtrate =
                          (TextBox)grdiitem.Rows[rowIndex].Cells[4].FindControl("txtrate");



                        drpitem.SelectedValue = dt.Rows[i]["Item"].ToString();
                        
                        txtqty.Text = dt.Rows[i]["Qty"].ToString();
                        txtrate.Text = dt.Rows[i]["Rate"].ToString();
                        

                        rowIndex++;

                    }
                }
            }
        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            AddNewRow();
            for (int vLoop = 0; vLoop < grdiitem.Rows.Count; vLoop++)
            {

                DropDownList txttk = (DropDownList)grdiitem.Rows[vLoop].FindControl("drpitem");

                txttk.Focus();
            }
           

        }

        private void AddNewRow()
        {
            for (int vLoop = 0; vLoop < grdiitem.Rows.Count; vLoop++)
            {

                DropDownList drpitem = (DropDownList)grdiitem.Rows[vLoop].FindControl("drpitem");
                TextBox txtrate = (TextBox)grdiitem.Rows[vLoop].FindControl("txtrate");
                TextBox txtqty = (TextBox)grdiitem.Rows[vLoop].FindControl("txtqty");
              

                int col = vLoop + 1;


                if (drpitem.SelectedValue == "0" || drpitem.SelectedValue == "Select Product")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Product in row " + col + " ')", true);
                    drpitem.Focus();
                    return;
                }

              
                //if ((txtqty.Text == "") || (Convert.ToInt32(txtqty.Text) == 0))
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Qty in row " + col + " ')", true);
                //    txtqty.Focus();
                //    return;
                //}

                ////   double qtyy = Convert.ToDouble(Convert.ToDouble(txtktt.Text));

                //if (txtrate.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Rate in row " + col + " ')", true);
                //    txtrate.Focus();
                //    return;
                //}
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

                        DropDownList drpitem =
                       (DropDownList)grdiitem.Rows[rowIndex].Cells[1].FindControl("drpitem");
                        //TextBox txtStock =
                        //  (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtStock");
                        TextBox txtqty =
                          (TextBox)grdiitem.Rows[rowIndex].Cells[2].FindControl("txtqty");

                        TextBox txtrate =
                          (TextBox)grdiitem.Rows[rowIndex].Cells[4].FindControl("txtrate");
                       

                        drCurrentRow = dtCurrentTable.NewRow();


                        dtCurrentTable.Rows[i - 1]["Item"] = drpitem.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["Qty"] = txtqty.Text;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtrate.Text;
                        

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable1"] = dtCurrentTable;

                    grdiitem.DataSource = dtCurrentTable;
                    grdiitem.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
            SetPreviousDataitem();
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            DataSet dst = new DataSet();
            dst = objBs.getallproductQuo();

          




            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var ddlC = (DropDownList)e.Row.FindControl("drpitem");
                ddlC.DataSource = dst;
                ddlC.DataTextField = "StyleNo";
                ddlC.DataValueField = "SamplingCostingId";
                ddlC.DataBind();
                ddlC.Items.Insert(0, "Select Item");
                //ddlC.Items.Insert(0, new ListItem("Select Code", "0"));
              
                TextBox txtttk = (TextBox)e.Row.FindControl("txtqty");
                TextBox txttk = (TextBox)e.Row.FindControl("txtRate");
              
                txtttk.Text = "0";
                txttk.Text = "0";
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowDataitem();
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    grdiitem.DataSource = dt;
                    grdiitem.DataBind();

                    
                    SetPreviousDataitem();

                    
                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    grdiitem.DataSource = dt;
                    grdiitem.DataBind();

                    SetPreviousDataitem();
                    FirstGridViewRow();
                }
            }

        }


        #endregion

        protected void gv_Employee_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("LeadMaster.aspx?Id=" + e.CommandArgument.ToString());


                }
            }
            //if (e.CommandName == "Del")
            //{
            //    if (e.CommandArgument.ToString() != "")
            //    {

            //        int i = objBs.Delete_Employee(Convert.ToInt32(e.CommandArgument.ToString()));
            //        Response.Redirect("Employee_Grid.aspx");

            //    }
            //}
        }
        protected void gv_Employee_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }


        protected void gridfollowup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("LeadMaster.aspx?Id=" + e.CommandArgument.ToString());


                }
            }
            //if (e.CommandName == "Del")
            //{
            //    if (e.CommandArgument.ToString() != "")
            //    {

            //        int i = objBs.Delete_Employee(Convert.ToInt32(e.CommandArgument.ToString()));
            //        Response.Redirect("Employee_Grid.aspx");

            //    }
            //}
        }
        protected void gridfollowup_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        private void SetInitialRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("Address", typeof(string)));
            dt.Columns.Add(new DataColumn("Number", typeof(string)));
            dt.Columns.Add(new DataColumn("Designation", typeof(string)));
            dt.Columns.Add(new DataColumn("Result", typeof(string)));
            dt.Columns.Add(new DataColumn("NextMeet", typeof(string)));
            dt.Columns.Add(new DataColumn("Remark", typeof(string)));
            dt.Columns.Add(new DataColumn("status", typeof(string)));
            //dt.Columns.Add(new DataColumn("Column6", typeof(string)));

            dr = dt.NewRow();
            dr["RowNumber"] = 1;
            dr["name"] = string.Empty;
            dr["address"] = string.Empty;
            dr["number"] = string.Empty;
            dr["designation"] = string.Empty;
            dr["result"] = string.Empty;
            dr["NextMeet"] = string.Empty;
            dr["Remark"] = string.Empty;
            dr["status"] = string.Empty;
            //dr["Column6"] = string.Empty;
            dt.Rows.Add(dr);
            //dr = dt.NewRow();

            //Store the DataTable in ViewState
            ViewState["CurrentTable"] = dt;

            gvProductSpecification.DataSource = dt;
            gvProductSpecification.DataBind();
        }

        protected void AddNeRow(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox txtname = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[1].FindControl("txtcustomername");
                        TextBox txtaddress = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[2].FindControl("txtaddress");
                        TextBox txtnumber = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[3].FindControl("txtno");
                        TextBox txtdest = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[4].FindControl("txtdesignation");
                        TextBox txtresult = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[5].FindControl("txtresult");
                        TextBox txtapp = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[5].FindControl("txtappoint");
                        TextBox txtremark = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[5].FindControl("txtremark");
                        DropDownList drpstatus = (DropDownList)gvProductSpecification.Rows[rowIndex].Cells[5].FindControl("drpstatus");
                        //TextBox box6 = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[3].FindControl("TextBox6");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;

                        dtCurrentTable.Rows[i - 1]["Name"] = txtname.Text;
                        dtCurrentTable.Rows[i - 1]["Address"] = txtaddress.Text;
                        dtCurrentTable.Rows[i - 1]["Number"] = txtnumber.Text;
                        dtCurrentTable.Rows[i - 1]["Designation"] = txtdest.Text;
                        dtCurrentTable.Rows[i - 1]["Result"] = txtresult.Text;
                        dtCurrentTable.Rows[i - 1]["NextMeet"] = txtapp.Text;
                        dtCurrentTable.Rows[i - 1]["Remark"] = txtremark.Text;
                        dtCurrentTable.Rows[i - 1]["status"] = drpstatus.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["Column6"] = box6.Text;

                        rowIndex++;
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    gvProductSpecification.DataSource = dtCurrentTable;
                    gvProductSpecification.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtname = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[1].FindControl("txtcustomername");
                        TextBox txtaddress = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[2].FindControl("txtaddress");
                        TextBox txtnumber = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[3].FindControl("txtno");
                        TextBox txtdest = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[4].FindControl("txtdesignation");
                        TextBox txtresult = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[5].FindControl("txtresult");
                        TextBox txtapp = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[5].FindControl("txtappoint");
                        TextBox txtremark = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[5].FindControl("txtremark");
                        DropDownList drpstatus = (DropDownList)gvProductSpecification.Rows[rowIndex].Cells[5].FindControl("drpstatus");
                        //TextBox box3 = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[3].FindControl("TextBox3");
                        //TextBox box4 = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[1].FindControl("TextBox4");
                        //TextBox box5 = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[2].FindControl("TextBox5");
                        //TextBox box6 = (TextBox)gvProductSpecification.Rows[rowIndex].Cells[3].FindControl("TextBox6");

                        txtname.Text = dt.Rows[i]["Name"].ToString();
                        txtaddress.Text = dt.Rows[i]["Address"].ToString();
                        txtnumber.Text = dt.Rows[i]["Number"].ToString();
                        txtdest.Text = dt.Rows[i]["Designation"].ToString();
                        txtresult.Text = dt.Rows[i]["Result"].ToString();
                        txtapp.Text = dt.Rows[i]["NextMeet"].ToString();
                        txtremark.Text = dt.Rows[i]["Remark"].ToString();
                        drpstatus.SelectedValue = dt.Rows[i]["status"].ToString();
                        //box6.Text = dt.Rows[i]["Column6"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        protected void filedownload(object sender, EventArgs e)
        {
            //if (drptask.SelectedValue != "Select fileName")
            //{
            //    DataSet dsFile = objBs.filetaskdownload(drptask.SelectedValue);
            //    if (dsFile.Tables[0].Rows.Count > 0)
            //    {
            //        string filePath = dsFile.Tables[0].Rows[0]["Document"].ToString();
            //        if (filePath == "")
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('File not uploaded');", true);
            //            return;
            //        }

            //        System.IO.FileInfo _file = new System.IO.FileInfo(filePath);
            //        // if (_file.Exists)
            //        {
            //            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            //            //Response.WriteFile(Server.MapPath(filePath));
            //            //Response.Flush();
            //            Response.TransmitFile(filePath);
            //            Response.End();

            //        }
            //        // else
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('File not uploaded');", true);

            //        }
            //    }
            //}
        }

        private void BindTime()
        {
            // Set the start time (00:00 means 12:00 AM)
            DateTime StartTime = DateTime.ParseExact("00:00", "HH:mm", null);
            // Set the end time (23:55 means 11:55 PM)
            DateTime EndTime = DateTime.ParseExact("23:55", "HH:mm", null);
            //Set 5 minutes interval
            TimeSpan Interval = new TimeSpan(0, 30, 0);
            //To set 1 hour interval
            //TimeSpan Interval = new TimeSpan(1, 0, 0);           
            ddlTimeFrom.Items.Clear();
            // ddlTimeTo.Items.Clear();
            while (StartTime <= EndTime)
            {
                ddlTimeFrom.Items.Add(StartTime.ToShortTimeString());
                //   ddlTimeTo.Items.Add(StartTime.ToShortTimeString());
                StartTime = StartTime.Add(Interval);
            }
           // ddlTimeFrom.Items.Insert(0, new ListItem("Select", "0"));
            //  ddlTimeTo.Items.Insert(0, new ListItem("--Select--", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string taskid = string.Empty;
            DateTime date;

            if (btnSubmit.Text == "Save")
            {


                if (txtcustomername1.Text == "" && txtcustcontactno1.Text == "" && txtxemailid.Text=="")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please enter Any one Customer Name/Contact No/Email id.Thank You!!!');", true);
                    return;
                }
                //if (txtcompanyname.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please enter Company Name.Thank You!!!');", true);
                //    return;
                //}
                //if (txtcustcontactno1.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please enter Customer Contact Number.Thank You!!!');", true);
                //    return;
                //}
                //if (txtxemailid.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please enter Emailid.Thank You!!!');", true);
                //    return;
                //}
                //if (txtxcompanyno.Text == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please enter Company Phone No.Thank You!!!');", true);
                //    return;
                //}
                if (txtxnextappoint.Text == "")
                {
                    txtxnextappoint.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    date = DateTime.ParseExact(txtxnextappoint.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    date = DateTime.ParseExact(txtxnextappoint.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                // CHECK DUPLICATE
                if (txtcompanyname.Text == "")
                {
                    // return;
                    int iStatus2 = objBs.insertleadentry(txtcompanyname.Text, txtcustomername1.Text, txtcustomername2.Text, txtaddress.Text, txtcustcontactno1.Text, txtcustcontactno2.Text, txtxemailid.Text, txtxdesignation.Text, txtxcompanyno.Text, txtxresultofmeet.Text, txtxnextappoint.Text, ddlTimeFrom.SelectedValue, txtremarks.Text, ddlreference.SelectedValue, ddlstatus.SelectedValue,ddlEmpname.SelectedValue);

                    for (int i = 0; i < grdiitem.Rows.Count; i++)
                    {

                        TextBox txtrate = (TextBox)grdiitem.Rows[i].Cells[1].FindControl("txtrate");
                        TextBox txtqty = (TextBox)grdiitem.Rows[i].Cells[1].FindControl("txtqty");
                        DropDownList drpitem = (DropDownList)grdiitem.Rows[i].Cells[1].FindControl("drpitem");
                        if (drpitem.SelectedValue != "Select Item")
                        {
                            int d = objBs.transitementry(drpitem.SelectedValue, txtqty.Text, txtrate.Text);
                        }
                    }
                }
                else if (txtcompanyname.Text != "")
                {
                    DataSet getduplicatecompanyname = objBs.duplicatecompanyname(txtcompanyname.Text);
                    if (getduplicatecompanyname.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Given Company NAme Already Exists.Thank You!!!');", true);
                        return;
                    }
                    else
                    {

                        // return;
                        int iStatus2 = objBs.insertleadentry(txtcompanyname.Text, txtcustomername1.Text, txtcustomername2.Text, txtaddress.Text, txtcustcontactno1.Text, txtcustcontactno2.Text, txtxemailid.Text, txtxdesignation.Text, txtxcompanyno.Text, txtxresultofmeet.Text, txtxnextappoint.Text, ddlTimeFrom.SelectedValue, txtremarks.Text, ddlreference.SelectedValue, ddlstatus.SelectedValue, ddlEmpname.SelectedValue);

                        for (int i = 0; i < grdiitem.Rows.Count; i++)
                        {

                            TextBox txtrate = (TextBox)grdiitem.Rows[i].Cells[1].FindControl("txtrate");
                            TextBox txtqty = (TextBox)grdiitem.Rows[i].Cells[1].FindControl("txtqty");
                            DropDownList drpitem = (DropDownList)grdiitem.Rows[i].Cells[1].FindControl("drpitem");
                            if (drpitem.SelectedValue != "Select Item")
                            {
                                int d = objBs.transitementry(drpitem.SelectedValue, txtqty.Text, txtrate.Text);
                            }
                        }
                    }
                }
               

            }
            else if (btnSubmit.Text == "Next Meeting")
            {
                int iss = objBs.Updatenextmeeting(lblleadid.Text, txtxnextappoint.Text, ddlTimeFrom.SelectedValue, txtxresultofmeet.Text, txtremarks.Text, ddlstatus.SelectedValue);
            }
            else if (btnSubmit.Text == "Update")
            {



                if (txtcustomername1.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please enter Atleast One Row.Thank You!!!');", true);
                    return;
                }
                if (txtxnextappoint.Text == "")
                {
                    txtxnextappoint.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    date = DateTime.ParseExact(txtxnextappoint.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    date = DateTime.ParseExact(txtxnextappoint.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                

                int iupdate = objBs.Updateleadentry(txtcompanyname.Text, txtcustomername1.Text, txtcustomername2.Text, txtaddress.Text, txtcustcontactno1.Text, txtcustcontactno2.Text, txtxemailid.Text, txtxdesignation.Text, txtxcompanyno.Text, txtxresultofmeet.Text, txtxnextappoint.Text, ddlTimeFrom.SelectedValue, txtremarks.Text, ddlreference.SelectedValue, ddlstatus.SelectedValue,lblleadid.Text,ddlEmpname.SelectedValue);

                int idele = objBs.deleteleaditem(lblleadid.Text);

                for (int i = 0; i < grdiitem.Rows.Count; i++)
                {

                    TextBox txtrate = (TextBox)grdiitem.Rows[i].Cells[1].FindControl("txtrate");
                    TextBox txtqty = (TextBox)grdiitem.Rows[i].Cells[1].FindControl("txtqty");
                    DropDownList drpitem = (DropDownList)grdiitem.Rows[i].Cells[1].FindControl("drpitem");
                    if (drpitem.SelectedValue != "Select Item")
                    {
                        int d = objBs.transitementryforupdate(drpitem.SelectedValue, txtqty.Text, txtrate.Text,lblleadid.Text);
                    }
                }

                //if (isadmin == "3")
                //{
                  //  int iStatus2 = objBs.updateentrydetailsemployee(txtcustomername.Text, txtaddress.Text, txtxcompanyno.Text, txtxdesignation.Text, txtxresultofmeet.Text, date, txtremarks.Text, ddlstatus.SelectedValue, userid, taskid, txtcustcontactno.Text, txtxemailid.Text, lblentry.Text, ddlmanagerStataus.SelectedValue, ddlreference.SelectedValue, txtcompanyname.Text, ddlTimeFrom.SelectedValue, txtcustomername1.Text, txtcustcontactno2.Text);
                //}
                //else
                //{
                //    int iStatus2 = objBs.updateentrydetailsemployee(txtcustomername.Text, txtaddress.Text, txtxcompanyno.Text, txtxdesignation.Text, txtxresultofmeet.Text, date, txtremarks.Text, ddlstatus.SelectedValue, userid, taskid, txtcustcontactno.Text, txtxemailid.Text, lblentry.Text, ddlmanagerStataus.SelectedValue, ddlreference.SelectedValue, txtcompanyname.Text, ddlTimeFrom.SelectedValue, txtcustomername1.Text, txtcustcontactno2.Text);
                //}
            }
            Response.Redirect("LeadGrid.aspx");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            

        }
        protected void btnFormat_Click(object sender, EventArgs e)
        {
            bindData();

        }

        public void bindData()
        {
            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();

            //dt.Columns.Add(new DataColumn("Company Name"));
            //dt.Columns.Add(new DataColumn("Customer Name"));
            //dt.Columns.Add(new DataColumn("Customer Address"));
            //dt.Columns.Add(new DataColumn("Customer ContactNo"));
            //dt.Columns.Add(new DataColumn("EmailId"));
            //dt.Columns.Add(new DataColumn("Designation"));
            //dt.Columns.Add(new DataColumn("Company PhoneNo"));
            ////dt.Columns.Add(new DataColumn("ResultofMeet"));
            ////dt.Columns.Add(new DataColumn("Next Appointment"));
            ////dt.Columns.Add(new DataColumn("Appoint Time"));
            ////dt.Columns.Add(new DataColumn("Comments"));
            ////dt.Columns.Add(new DataColumn("Reference"));

            //DataRow dr_final12 = dt.NewRow();
            //dr_final12["Company Name"] = "";
            //dr_final12["Customer Name"] = "";
            //dr_final12["Customer Address"] = "";
            //dr_final12["Customer ContactNo"] = "";
            //dr_final12["EmailId"] = "";
            //dr_final12["Designation"] = "";
            //dr_final12["Company PhoneNo"] = "";
            ////dr_final12["ResultofMeet"] = "";
            ////dr_final12["Next Appointment"] = "";
            ////dr_final12["Appoint Time"] = "";
            ////dr_final12["Comments"] = "";
            ////dr_final12["Reference"] = "";
            //dt.Rows.Add(dr_final12);

            //ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {

            //if (dt.Rows.Count > 0)
            //{
            //    //string filename = "Sales Report.xls";
            //    string filename = "NewmarketingSheet_" + DateTime.Now.ToString() + ".xls";
            //    System.IO.StringWriter tw = new System.IO.StringWriter();
            //    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            //    DataGrid dgGrid = new DataGrid();
            //    dgGrid.DataSource = dt;
            //    dgGrid.DataBind();
            //    //dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            //    //dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
            //    //dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
            //    dgGrid.HeaderStyle.Font.Bold = true;
            //    //Get the HTML for the control.
            //    dgGrid.RenderControl(hw);
            //    //Write the HTML back to the browser.
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            //    this.EnableViewState = false;
            //    Response.Write(tw.ToString());
            //    Response.End();
            //}
        }
        protected void Exit1_Click(object sender, EventArgs e)
        {


            Response.Redirect("LeadGrid.aspx");

        }

    }
}