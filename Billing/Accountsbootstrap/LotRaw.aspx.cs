using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Text;
using System.Data;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class LotRaw : System.Web.UI.Page
    {

        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";
        string empid = "";

        int iFinishedQty = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            empid = Session["Empid"].ToString();

            string rawid = Request.QueryString.Get("rawid");
            if (!IsPostBack)
            {


                string super = Session["IsSuperAdmin"].ToString();


                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objbs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "Select Branch");
                    }
                }
                else
                {

                    drpbranch.Enabled = false;
                    DataSet dbraqnch = objbs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();
                        company_SelectedIndexChnaged(sender, e);
                    }
                }





               

             
                if (rawid != null)
                {
                    DataSet dgetcheck = objbs.Get_raw(rawid);
                    if (dgetcheck.Tables[0].Rows.Count > 0)
                    {


                        DataSet dsLotNo1 = objbs.Select_LotNo_Fornew(dgetcheck.Tables[0].Rows[0]["lotno"].ToString());    // 7-Checking 
                        if (dsLotNo1.Tables[0].Rows.Count > 0)
                        {
                            ddlLotNo.DataSource = dsLotNo1.Tables[0];
                            ddlLotNo.DataTextField = "CompanyLotNo";
                            ddlLotNo.DataValueField = "lotdetailid";
                            ddlLotNo.DataBind();
                            ddlLotNo.Items.Insert(0, "Select Lot No");
                        }

                        btnadd.Visible = true;
                        btnadd.Text = "Update";
                        DataSet drpProcess11 = objbs.selectcategoryalldecription(btnadd.Text, "CO1", drpbranch.SelectedValue);
                        if (drpProcess11.Tables[0].Rows.Count > 0)
                        {
                            drpProd.DataSource = drpProcess11.Tables[0];
                            drpProd.DataTextField = "definition";
                            drpProd.DataValueField = "CategoryUserID";
                            drpProd.DataBind();
                            drpProd.Items.Insert(0, "Select Definition");
                        }

                        ddlLotNo.SelectedValue = dgetcheck.Tables[0].Rows[0]["lotno"].ToString();

                        txthalfsleev.Text = dgetcheck.Tables[0].Rows[0]["HalfSL"].ToString();
                        txtfullsleev.Text = dgetcheck.Tables[0].Rows[0]["FullSL"].ToString();
                       
                        //CkeckingInfo_Loadforupdate(sender, e);
                        DataSet ds = objbs.Select_LotRawDetails(rawid);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataSet temp = new DataSet();
                            DataTable dtt = new DataTable();

                            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Prod", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Prodname", typeof(string)));
                            dtt.Columns.Add(new DataColumn("Quantity", typeof(string)));
                            dtt.Columns.Add(new DataColumn("stock", typeof(string)));

                    
                            temp.Tables.Add(dtt);



                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {

                                DataRow dr = dtt.NewRow();

                                dr["OrderNo"] = "";
                                dr["Prod"] = ds.Tables[0].Rows[i]["Prod"].ToString();
                                dr["stock"] = "";
                                dr["Prodname"] = ds.Tables[0].Rows[i]["Prodname"].ToString();
                                dr["Quantity"] = ds.Tables[0].Rows[i]["qty"].ToString();
                                
                                temp.Tables[0].Rows.Add(dr);
                            }

                            ViewState["CurrentTable1"] = dtt;

                            gvcustomerorder.DataSource = temp;
                            gvcustomerorder.DataBind();

                            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                            {
       
                                TextBox txtstock = (TextBox)gvcustomerorder.Rows[i].FindControl("txtstock");

                                TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");

                                TextBox txtprod = (TextBox)gvcustomerorder.Rows[i].FindControl("txtprod");



                                TextBox txtprodname = (TextBox)gvcustomerorder.Rows[i].FindControl("txtprodname");
                                //TextBox txt_38HS = (TextBox)gvcustomerorder.Rows[i].FindControl("txt38HS");

                                txtprod.Text = temp.Tables[0].Rows[i]["Prod"].ToString();
                                txtprodname.Text = temp.Tables[0].Rows[i]["Prodname"].ToString();
                                txtRecQuantity.Text = temp.Tables[0].Rows[i]["Quantity"].ToString();

                                txtstock.Text = temp.Tables[0].Rows[i]["stock"].ToString();

                            
                            }
                        }
                        else
                        {
                            FirstGridViewRow();
                            // mySidenav.Visible = false;
                        }

                        //  gvcustomerorder.Enabled = false;
                        ddlLotNo.Enabled = false;

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Something Went Wrong.Please Check Lot No.Or Contact Administrator.')", true);
                        return;
                    }
                }
                else
                {

                    FirstGridViewRow();
                }
                // Detail_checked(sender, e);
            }

              ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void company_SelectedIndexChnaged(object sender, EventArgs e)
        {
            if (drpbranch.SelectedValue == "Select Branch")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Branch. Thank you');", true);
                return;

            }
            else
            {

                DataSet dsLotNo = objbs.Select_LotNo_For(drpbranch.SelectedValue);    // 7-Checking 
                if (dsLotNo.Tables[0].Rows.Count > 0)
                {
                    ddlLotNo.DataSource = dsLotNo.Tables[0];
                    ddlLotNo.DataTextField = "CompanyLotNo";
                    ddlLotNo.DataValueField = "lotdetailid";
                    ddlLotNo.DataBind();
                    ddlLotNo.Items.Insert(0, "Select Lot No");
                }

                DataSet drpProcess1 = objbs.selectcategoryalldecription(btnadd.Text, "CO1",drpbranch.SelectedValue);
                if (drpProcess1.Tables[0].Rows.Count > 0)
                {
                    drpProd.DataSource = drpProcess1.Tables[0];
                    drpProd.DataTextField = "definition";
                    drpProd.DataValueField = "CategoryUserID";
                    drpProd.DataBind();
                    drpProd.Items.Insert(0, "Select Definition");
                }
                else
                {
                    drpProd.Items.Clear();
                }


            }
        }



        protected void txtrecqtychnaged_text(object sender, EventArgs e)
        {
            double avaqty = 0;
            double recqty = 0;
            if (txtQty1.Text == "")
            {
                txtQty1.Text = "0";
            }

                 avaqty = Convert.ToDouble(txtstock1.Text);

                 recqty = Convert.ToDouble(txtQty1.Text);




                 if (avaqty < recqty)
                 {
                     ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Exists Avaliable Qty.Thank you!!!. ')", true);
                     txtQty1.Focus();
                     return;
                 }
                 else
                 {
                     Button1.Focus();
                 }
            
            
        }

        protected void drpProd_selected(object sender, EventArgs e)
        {
            DataSet ds1 = objbs.GetStockDetails(Convert.ToInt32(drpProd.SelectedValue), "tblStock_"+sTableName+"");
            if (ds1.Tables[0].Rows.Count > 0)
            {
                txtstock1.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["Available_Qty"]).ToString();
                txtQty1.Focus();

            }

            DataSet dpreview = objbs.GetTax(Convert.ToInt32(drpProd.SelectedValue));
            if (dpreview.Tables[0].Rows.Count > 0)
            {
                previewimage.ImageUrl = dpreview.Tables[0].Rows[0]["Image"].ToString();
            }

        }

        protected void drpprocess_selected(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //DropDownList ddlprocess = (DropDownList)row.FindControl("drpProcess");
            //TextBox txtrate = (TextBox)row.FindControl("txtRate");

            //TextBox txt_36FS = (TextBox)row.FindControl("txt36FS");
            //TextBox txt_38FS = (TextBox)row.FindControl("txt38FS");
            //TextBox txt_39FS = (TextBox)row.FindControl("txt39FS");
            //TextBox txt_40FS = (TextBox)row.FindControl("txt40FS");
            //TextBox txt_42FS = (TextBox)row.FindControl("txt42FS");
            //TextBox txt_44FS = (TextBox)row.FindControl("txt44FS");

            //TextBox txt_36HS = (TextBox)row.FindControl("txt36HS");
            //TextBox txt_38HS = (TextBox)row.FindControl("txt38HS");
            //TextBox txt_39HS = (TextBox)row.FindControl("txt39HS");
            //TextBox txt_40HS = (TextBox)row.FindControl("txt40HS");
            //TextBox txt_42HS = (TextBox)row.FindControl("txt42HS");
            //TextBox txt_44HS = (TextBox)row.FindControl("txt44HS");



            //DataSet ds = new DataSet();
            //if (ddlprocess.SelectedValue != "Select Process Type")
            //{
            //    ds = objbs.Get_Rate(ddlprocess.SelectedValue, ddlLotNo.SelectedValue);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        txtrate.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["Rate"]).ToString();
            //        if (txt_36FS.Text == "")
            //        {
            //            txt_36FS.Text = "0";
            //        }
            //        if (txt_38FS.Text == "")
            //        {
            //            txt_38FS.Text = "0";
            //        }
            //        if (txt_39FS.Text == "")
            //        {
            //            txt_39FS.Text = "0";
            //        }
            //        if (txt_40FS.Text == "")
            //        {
            //            txt_40FS.Text = "0";
            //        }
            //        if (txt_42FS.Text == "")
            //        {
            //            txt_42FS.Text = "0";
            //        }
            //        if (txt_44FS.Text == "")
            //        {
            //            txt_44FS.Text = "0";
            //        }

            //        if (txt_36HS.Text == "")
            //        {
            //            txt_36HS.Text = "0";
            //        }
            //        if (txt_38HS.Text == "")
            //        {
            //            txt_38HS.Text = "0";
            //        }
            //        if (txt_39HS.Text == "")
            //        {
            //            txt_39HS.Text = "0";
            //        }
            //        if (txt_40HS.Text == "")
            //        {
            //            txt_40HS.Text = "0";
            //        }
            //        if (txt_42HS.Text == "")
            //        {
            //            txt_42HS.Text = "0";
            //        }
            //        if (txt_44HS.Text == "")
            //        {
            //            txt_44HS.Text = "0";
            //        }
            //    }
            //}

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{

            //    DropDownList drpprocess = (DropDownList)gvcustomerorder.Rows[i].Cells[0].FindControl("drpProcess");
            //    drpprocess.Focus();

            //}
            //ScriptManager.RegisterStartupScript(this, GetType(), "Myscript", "openNav();", true);
            // ButtonAdd1_Click(sender, e);
        }

        protected void Detail_checked(object sender, EventArgs e)
        {
            //if (ddlLotNo.SelectedValue != "Select Lot No")
            //{
            //    DataSet dlotprocess = new DataSet();
            //    DataSet dssendqty = new DataSet();
            //    dlotprocess = objbs.Get_Processdetails(ddlLotNo.SelectedValue, 7);   //7-Checking


            //    if (dlotprocess.Tables[0].Rows.Count > 0)
            //    {
            //        GridView1.DataSource = dlotprocess;
            //        GridView1.DataBind();

            //        txtfinishedQty.Text=dlotprocess.Tables[0].Rows[0]["FinishedQty"].ToString();

            //    }


            //    dssendqty = objbs.Select_CheckingSendQty_ByLotID(Convert.ToInt32(ddlLotNo.SelectedValue));

            //    if (dssendqty.Tables[0].Rows.Count > 0)
            //    {
            //        txtOldrecQty.Text = dssendqty.Tables[0].Rows[0]["SendQty"].ToString();
            //        txtcheckingbalQty.Text = dssendqty.Tables[0].Rows[0]["CheckingBalQty"].ToString();
            //    }
            //    else
            //    {
            //        txtOldrecQty.Text = "0";
            //        txtcheckingbalQty.Text = "0";
            //    }



            //    txtnewfinishedqty.Text = Convert.ToString(Convert.ToInt32(txtfinishedQty.Text)-Convert.ToInt32(txtOldrecQty.Text) + Convert.ToInt32(txtcheckingbalQty.Text));
            //    GridView2.Visible = false;
            //    processs.Visible = true;
            //    ratee.Visible = false;
            //    GridView1.Visible = true;
            //    Ratedetail.Checked = false;
            //    DetailView.Checked = true;
            //    //  mpe.Show();
            //    //  DetailView.Checked = false;
            //}
        }

        protected void RateDetail_checked(object sender, EventArgs e)
        {
            
        }


        protected void CkeckingInfo_Loadforupdate(object sender, EventArgs e)
        {
          
        }

        protected void CheckingInfo_Load(object sender, EventArgs e)
        {
            double hs = 0;
            double fs = 0;
            DataSet dataSet = objbs.gethalffullqty(Convert.ToInt32(ddlLotNo.SelectedValue));
            if (dataSet.Tables[0].Rows.Count > 0)
            {
               
                txthalfsleev.Text = dataSet.Tables[0].Rows[0]["HalfQty"].ToString();
                txtfullsleev.Text = dataSet.Tables[0].Rows[0]["FullQty"].ToString();

                if(txthalfsleev.Text=="")
                {
                    hs =0;
                }
                else
                {
                 hs = Convert.ToDouble(txthalfsleev.Text);
                }

                if (txtfullsleev.Text == "")
                {
                    fs = 0;
                }
                else
                {
                    fs = Convert.ToDouble(txtfullsleev.Text);
                }


                txttotsleev.Text = (hs + fs).ToString("0.00");
                txtsno.Focus();
            }
         
        }

        protected void GridViewRate_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SetRowData();

            if (ViewState["CurrentTable1"] != null)
            {
                DataSet ds = new DataSet();
                DataTable dt = (DataTable)ViewState["CurrentTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {

                    ds.Merge(dt);


                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();

                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();

                    SetPreviousData();

                    //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    //{
                    //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                    //    txtno.Text = Convert.ToString(i + 1);
                    //}
                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();

                    SetPreviousData();
                    FirstGridViewRow();
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

                        TextBox txtprodname =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtprodname");

                        TextBox txtRecQuantity =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRecQuantity");

                        TextBox txtprod =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtprod");

                        TextBox txtstock =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtstock");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["prodname"] = txtprodname.Text;

                        dtCurrentTable.Rows[i - 1]["Quantity"] = txtRecQuantity.Text;
                        dtCurrentTable.Rows[i - 1]["prod"] = txtprod.Text;
                        dtCurrentTable.Rows[i - 1]["stock"] = txtstock.Text;

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

        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("Prod", typeof(string)));
            dtt.Columns.Add(new DataColumn("Prodname", typeof(string)));
            dtt.Columns.Add(new DataColumn("stock", typeof(string)));
            dtt.Columns.Add(new DataColumn("Quantity", typeof(string)));
            

            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["Prod"] = string.Empty;

            dr["Prodname"] = string.Empty;
            dr["Quantity"] = string.Empty;
            dr["stock"] = string.Empty;
           

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

            dct = new DataColumn("Prod");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Prodname");
            dttt.Columns.Add(dct);


            dct = new DataColumn("Quantity");
            dttt.Columns.Add(dct);

            dct = new DataColumn("stock");
            dttt.Columns.Add(dct);

       
            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["OrderNo"] = "";
            drNew["Prod"] = "";
            drNew["Prodname"] = "";
            drNew["stock"] = "";
            drNew["Quantity"] = "";
            
            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();

        }


        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {

        
        }

        private void AddNewRow()
        {
            //DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            //int iq = 1;
            //int ii = 1;
            //string itemc = string.Empty;
            //string itemcd = string.Empty;
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList drpProcess = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpProcess");
            //    TextBox txtRate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRecQuantity");
            //    DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpEmp");
            //    TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");

            //    DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

            //    if (drpProcess.SelectedValue == "Select Process")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Process')", true);
            //        //  txt.Focus();
            //        return;
            //    }
            //}

            //int rowIndex = 0;

            //if (ViewState["CurrentTable1"] != null)
            //{

            //    DataRow drCurrentRow = null;
            //    if (dtCurrentTable.Rows.Count > 0)
            //    {
            //        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //        {

                    
            //            rowIndex++;
            //        }

            //        dtCurrentTable.Rows.Add(drCurrentRow);
            //        ViewState["CurrentTable1"] = dtCurrentTable;

            //        gvcustomerorder.DataSource = dtCurrentTable;
            //        gvcustomerorder.DataBind();

            //    }
            //}
            //else
            //{
            //    Response.Write("ViewState is null");
            //}
            //SetPreviousData();

            
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

                        TextBox txtProdname =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtProdname");

                        TextBox txtRecQuantity =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtRecQuantity");

                        TextBox txtProd =
                         (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtProd");


                        TextBox txtstock =
                        (TextBox)gvcustomerorder.Rows[rowIndex].Cells[1].FindControl("txtstock");



                        txtProdname.Text = dt.Rows[i]["Prodname"].ToString();
                        txtProd.Text = dt.Rows[i]["prod"].ToString();
                        txtRecQuantity.Text = dt.Rows[i]["Quantity"].ToString();
                     

                        rowIndex++;
                        drpProd.Focus();
                    }
                }
            }
        }
        
        protected void Add_Lot(object sender, EventArgs e)
        {
            DataSet dstd = new DataSet();
            DataTable dtddd = new DataTable();


            DataTable dttt;
            DataRow drNew;
            DataColumn dct;

            dttt = new DataTable();



            dct = new DataColumn("OrderNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Quantity");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Prodname");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Prod");
            dttt.Columns.Add(dct);



            dct = new DataColumn("Stock");
            dttt.Columns.Add(dct);

          
            dstd.Tables.Add(dttt);


            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                drNew = dttt.NewRow();
                drNew["Quantity"] = txtQty1.Text;
                drNew["OrderNo"] = "";
                drNew["Prod"] = drpProd.SelectedValue;
                drNew["Prodname"] = drpProd.SelectedItem.Text;
                drNew["Stock"] = txtstock1.Text;

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];

                dtddd.Merge(dt);
            }
            else
            {
                drNew = dttt.NewRow();
                drNew["Quantity"] = txtQty1.Text;
                drNew["OrderNo"] = "";
                drNew["Prod"] = drpProd.SelectedValue;
                drNew["Prodname"] = drpProd.SelectedItem.Text;

                drNew["Stock"] = txtstock1.Text;
               

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];
            }




            ViewState["CurrentTable1"] = dtddd;

            gvcustomerorder.DataSource = dtddd;
            gvcustomerorder.DataBind();

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtprod = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtprod");
                //TextBox txtstock = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtstock");
                TextBox txtprodname = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtprodname");
                TextBox txtRecQuantity = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRecQuantity");
                TextBox txtstock = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtstock");


                txtstock.Text = dstd.Tables[0].Rows[vLoop]["Stock"].ToString();
                    txtprodname.Text = dstd.Tables[0].Rows[vLoop]["prodname"].ToString();
                    txtprod.Text = dstd.Tables[0].Rows[vLoop]["prod"].ToString();
                    txtRecQuantity.Text = dstd.Tables[0].Rows[vLoop]["Quantity"].ToString();
                   
                
            }

            txtstock1.Text = "";
            
            txtQty1.Text = "";
            txtsno.Focus();
            drpProd.ClearSelection();
       
        }

        protected void Add_LotProcessDetails(object sender, EventArgs e)
        {
            if (ddlLotNo.SelectedValue == "Select Lot No")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Lot No.Thank You!!!.')", true);
                return;
            }

            //if (txtProcessDate.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Process Date')", true);
            //    return;
            //}

            int iq = 1;
            int iii = 1;
            string itemc = string.Empty;
            string itemd = string.Empty;
            string iteme = string.Empty;
            string itemcd = string.Empty;
            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox drpprocess = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtProd");
                    if (drpprocess.Text != "")
                    {
                        //DropDownList drpEmp = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpEmp");
                        //if (drpEmp.SelectedValue == "Select Employee Name")
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name.Thank You!!!.')", true);
                        //    return;
                        //}
                        TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRecQuantity");

                        //TextBox date = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("date");
                        //if (date.Text == "")
                        //{
                        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Date.Thank You!!!.')", true);
                        //    return;
                        //}

                        //DateTime processDate = DateTime.ParseExact(txtProcessDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //DateTime recDate = DateTime.ParseExact(date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");





                        //itemc = drpprocess.SelectedValue;
                        //itemd = recDate.ToString("dd/MM/yyyy");
                        //iteme = drpEmp.SelectedValue;


                        //if ((itemc == null) || (itemc == ""))
                        //{
                        //}
                        //else
                        //{
                        //    for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                        //    {
                        //        DropDownList drpprocesss = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpProcess");
                        //        if (drpprocesss.SelectedValue != "Select Process Type")
                        //        {
                        //            DropDownList drpEmpp = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpEmp");
                        //            TextBox datee = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("date");
                        //            DateTime recDatee = DateTime.ParseExact(datee.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //            if (drpEmpp.Text == "")
                        //            {
                        //            }
                        //            else
                        //            {

                        //                if (iii == iq)
                        //                {
                        //                }
                        //                else
                        //                {
                        //                    if (itemc == drpprocesss.Text && iteme == drpEmpp.SelectedValue && itemd == recDatee.ToString("dd/MM/yyyy"))
                        //                    {
                        //                        itemcd = drpprocess.SelectedItem.Text;
                        //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd.Trim() + "," + drpEmpp.SelectedItem.Text.Trim() + "," + recDatee.ToString("dd/MM/yyyy") + "  already exists in the Grid.');", true);
                        //                        drpEmpp.Focus();
                        //                        return;

                        //                    }
                        //                }
                        //                iii = iii + 1;
                        //            }
                        //        }
                        //    }
                        //}
                        //iq = iq + 1;
                        //iii = 1;
                    }
                }
            }


           
                     double total= 0;
                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {

                        TextBox txtrecQty1 = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                        TextBox txtprod1 = (TextBox)gvcustomerorder.Rows[i].FindControl("txtprod");
                        TextBox txtprodname1 = (TextBox)gvcustomerorder.Rows[i].FindControl("txtprodname");




                        if (txtprodname1.Text == "")
                        {
                        }
                        else
                        {
                            total=total+Convert.ToDouble(txtrecQty1.Text);
                        }
                    }


                    if (btnadd.Text == "Save")
                    {
                        DataSet ds = new DataSet();
                        //ds = objbs.Check_CutID_AlreadyExist(ddlLotNo.SelectedValue, "tblCheckingProcess");
                        //if (ds.Tables[0].Rows.Count == 0)
                        //{
                        int iStatus23 = objbs.Insert_LotRaw(Convert.ToString(ddlLotNo.SelectedValue), Convert.ToString(ddlLotNo.SelectedItem.Text), Convert.ToDouble(txthalfsleev.Text), Convert.ToDouble(txtfullsleev.Text), Convert.ToDouble(txttotsleev.Text), empid,drpbranch.SelectedValue);


                        for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                        {

                            TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                            TextBox txtprod = (TextBox)gvcustomerorder.Rows[i].FindControl("txtprod");
                            TextBox txtprodname = (TextBox)gvcustomerorder.Rows[i].FindControl("txtprodname");




                            if (txtprodname.Text == "")
                            {
                            }
                            else
                            {



                                int istasInsertHistory = objbs.Insert_TransLotRaw(Convert.ToDouble(txtrecQty.Text), Convert.ToString(txtprodname.Text), Convert.ToString(txtprod.Text), sTableName);


                            }
                        }
                        //}
                    }
                    else
                    {
                        string rawid = Request.QueryString.Get("rawid");
                        int iStatus23 = objbs.Update_LotRaw(Convert.ToString(ddlLotNo.SelectedValue), Convert.ToString(ddlLotNo.SelectedItem.Text), Convert.ToDouble(txthalfsleev.Text), Convert.ToDouble(txtfullsleev.Text), Convert.ToDouble(txttotsleev.Text), rawid,empid);

                        DataSet dss = objbs.GetrawgridnewNN(rawid);
                        if (dss.Tables[0].Rows.Count > 0)
                        {
                            for(int i =0 ; i < dss.Tables[0].Rows.Count; i++)
                            {


                            string proedctid =dss.Tables[0].Rows[i]["Prod"].ToString();
                            double Qty = Convert.ToDouble(dss.Tables[0].Rows[i]["Qty"]);

                            int ii = objbs.Updatelotraw(proedctid,Qty,"Co1");
                            }
                        }


                        int istasInsertHistory = objbs.deleteTransLotRaw(Convert.ToInt32(rawid));

                        for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                        {

                            TextBox txtrecQty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRecQuantity");
                            TextBox txtprod = (TextBox)gvcustomerorder.Rows[i].FindControl("txtprod");
                            TextBox txtprodname = (TextBox)gvcustomerorder.Rows[i].FindControl("txtprodname");




                            if (txtprodname.Text == "")
                            {
                            }
                            else
                            {

                                int istasInsertHistory1 = objbs.Update_TransLotRaw(Convert.ToDouble(txtrecQty.Text), Convert.ToString(txtprodname.Text), Convert.ToString(txtprod.Text),rawid);


                            }
                        }
                    }



                    Response.Redirect("LotRawGrid.aspx");


        }

        protected void GridViewWork_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                foreach (TableCell gr in e.Row.Cells)
                {
                    if (gr.Text == "YES")
                    {
                        gr.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        gr.BackColor = System.Drawing.Color.Red;
                    }

                    if (gr.Text == "True")
                    {
                        gr.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        gr.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }


        protected void txt36FS_TextChanged(object sender, EventArgs e)
        {
           

        }

        protected void txt38FS_TextChanged(object sender, EventArgs e)
        {
          

        }


        protected void txt39FS_TextChanged(object sender, EventArgs e)
        {
           
        }

        protected void txt40FS_TextChanged(object sender, EventArgs e)
        {
          

        }


        protected void txt42FS_TextChanged(object sender, EventArgs e)
        {
           

        }

        protected void txt44FS_TextChanged(object sender, EventArgs e)
        {


           
        }


        protected void txt36HS_TextChanged(object sender, EventArgs e)
        {
          

        }


        protected void txt38HS_TextChanged(object sender, EventArgs e)
        {
            

        }


        protected void txt39HS_TextChanged(object sender, EventArgs e)
        {
        
        }


        protected void txt40HS_TextChanged(object sender, EventArgs e)
        {
           

        }


        protected void txt42HS_TextChanged(object sender, EventArgs e)
        {

          
        }



        protected void txt44HS_TextChanged(object sender, EventArgs e)
        {

           
           

        }

        protected void Total_receivedQty(object sender)
        {
           
        }



        protected void Gridview1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }


    }
}