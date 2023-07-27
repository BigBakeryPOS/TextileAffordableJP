using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BusinessLayer;
using CommonLayer;
using DataLayer;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;
using System.Drawing;

namespace Billing.Accountsbootstrap
{
    public partial class GodownOpeningStock : System.Web.UI.Page
    {
        DataSet ds1 = new DataSet();
        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();
        string sTableName = "";
        double F30 = 0; double F32 = 0; double F34 = 0; double F36 = 0; double FXS = 0; double FS = 0; double FM = 0; double FL = 0; double FXL = 0; double FXXL = 0; double F3XL = 0; double F4XL = 0; double H30 = 0; double H32 = 0; double H34 = 0; double H36 = 0; double HXS = 0; double HS = 0; double HM = 0; double HL = 0; double HXL = 0; double HXXL = 0; double H3XL = 0; double H4XL = 0; double TOTAL = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            sTableName = Session["User"].ToString();

            if (!IsPostBack)
            {
                txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                string super = Session["IsSuperAdmin"].ToString();
                if (super == "1")
                {
                    drpbranch.Enabled = true;

                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.Items.Insert(0, "All");
                    }
                }
                else
                {
                    drpbranch.Enabled = false;
                    DataSet dbraqnch = objBs.GetCompanyDet();
                    if (dbraqnch.Tables[0].Rows.Count > 0)
                    {
                        drpbranch.DataSource = dbraqnch.Tables[0];
                        drpbranch.DataTextField = "CompanyName";
                        drpbranch.DataValueField = "Comapanyid";
                        drpbranch.DataBind();
                        drpbranch.SelectedValue = Session["cmpyid"].ToString();

                    }
                }


                DataSet dsFabricType = objBs.gettblFabricType();
                if (dsFabricType.Tables[0].Rows.Count > 0)
                {
                    ddlItem.DataSource = dsFabricType.Tables[0];
                    ddlItem.DataTextField = "FabricType";
                    ddlItem.DataValueField = "FabricTypeID";
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, "Select Item");
                }

                DataSet dsBrandName = objBs.getBrandName();
                if (dsBrandName.Tables[0].Rows.Count > 0)
                {
                    ddlbrand.DataSource = dsBrandName.Tables[0];
                    ddlbrand.DataTextField = "name";
                    ddlbrand.DataValueField = "BrandID";
                    ddlbrand.DataBind();
                    ddlbrand.Items.Insert(0, "Select Brand");
                }

                DataSet dsFit = objBs.Fit();
                if (dsFit.Tables[0].Rows.Count > 0)
                {
                    ddlfit.DataSource = dsFit.Tables[0];
                    ddlfit.DataTextField = "fit";
                    ddlfit.DataValueField = "fitid";
                    ddlfit.DataBind();
                    ddlfit.Items.Insert(0, "Select Fit");
                }

            }
        }
        protected void Companylotchecked(object sender, EventArgs e)
        {
            DataSet ds = objBs.getFinishedStockRatiolot(txtlotno.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This LotNo Already Exists. Thank you');", true);
                return;
            }

        }
        protected void btnadd_OnCLick(object sender, EventArgs e)
        {
            DataSet dstd = new DataSet();
            DataTable dtddd = new DataTable();

            if (ddlItem.SelectedValue == "Select Item")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Item. Thank you');", true);
                ddlItem.Focus();
                return;
               
            }
            if (txtdesignno.Text == "" || txtdesignno.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter DesignNo. Thank you');", true);
                txtdesignno.Focus();
                return;
            }
            if (txtcolor.Text == "" || txtcolor.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter Color. Thank you');", true);
                txtcolor.Focus();
                return;
            }

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            dttt = new DataTable();

            dct = new DataColumn("DesignCode");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Itemname");
            dttt.Columns.Add(dct);

            dct = new DataColumn("F30");
            dttt.Columns.Add(dct);
            dct = new DataColumn("F32");
            dttt.Columns.Add(dct);
            dct = new DataColumn("F34");
            dttt.Columns.Add(dct);
            dct = new DataColumn("F36");
            dttt.Columns.Add(dct);
            dct = new DataColumn("FXS");
            dttt.Columns.Add(dct);
            dct = new DataColumn("FS");
            dttt.Columns.Add(dct);
            dct = new DataColumn("FM");
            dttt.Columns.Add(dct);
            dct = new DataColumn("FL");
            dttt.Columns.Add(dct);
            dct = new DataColumn("FXL");
            dttt.Columns.Add(dct);
            dct = new DataColumn("FXXL");
            dttt.Columns.Add(dct);
            dct = new DataColumn("F3XL");
            dttt.Columns.Add(dct);
            dct = new DataColumn("F4XL");
            dttt.Columns.Add(dct);

            dct = new DataColumn("H30");
            dttt.Columns.Add(dct);
            dct = new DataColumn("H32");
            dttt.Columns.Add(dct);
            dct = new DataColumn("H34");
            dttt.Columns.Add(dct);
            dct = new DataColumn("H36");
            dttt.Columns.Add(dct);
            dct = new DataColumn("HXS");
            dttt.Columns.Add(dct);
            dct = new DataColumn("HS");
            dttt.Columns.Add(dct);
            dct = new DataColumn("HM");
            dttt.Columns.Add(dct);
            dct = new DataColumn("HL");
            dttt.Columns.Add(dct);
            dct = new DataColumn("HXL");
            dttt.Columns.Add(dct);
            dct = new DataColumn("HXXL");
            dttt.Columns.Add(dct);
            dct = new DataColumn("H3XL");
            dttt.Columns.Add(dct);
            dct = new DataColumn("H4XL");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Total");
            dttt.Columns.Add(dct);
            dstd.Tables.Add(dttt);


            if (ViewState["CurrentTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable1"];

                drNew = dttt.NewRow();
                drNew["DesignCode"] = ddlItem.SelectedItem.Text+"-"+txtdesignno.Text+"-"+txtcolor.Text;
                drNew["Itemname"] = ddlItem.SelectedItem.Text;
            
                drNew["F30"] = txtf30.Text;
                drNew["F32"] = txtf32.Text;
                drNew["F34"] = txtf34.Text;
                drNew["F36"] = txtf36.Text;
                drNew["FXS"] = txtfxs.Text;
                drNew["FS"] = txtfs.Text;
                drNew["FM"] = txtfm.Text;
                drNew["FL"] = txtfl.Text;
                drNew["FXL"] = txtfxl.Text;
                drNew["FXXL"] = txtfxxl.Text;
                drNew["F3XL"] = txtf3xl.Text;
                drNew["F4XL"] = txtf4xl.Text;

                drNew["H30"] = txth30.Text;
                drNew["H32"] = txth32.Text;
                drNew["H34"] = txth34.Text;
                drNew["H36"] = txth36.Text;
                drNew["HXS"] = txthxs.Text;
                drNew["HS"] = txths.Text;
                drNew["HM"] = txthm.Text;
                drNew["HL"] = txthl.Text;
                drNew["HXL"] = txthxl.Text;
                drNew["HXXL"] = txthxxl.Text;
                drNew["H3XL"] = txth3xl.Text;
                drNew["H4XL"] = txth4xl.Text;

                int Total = Convert.ToInt32(txtf30.Text) + Convert.ToInt32(txtf32.Text) + Convert.ToInt32(txtf34.Text) + Convert.ToInt32(txtf36.Text) + Convert.ToInt32(txtfxs.Text) + Convert.ToInt32(txtfs.Text) +
                            Convert.ToInt32(txtfm.Text) + Convert.ToInt32(txtfl.Text) + Convert.ToInt32(txtfxl.Text) + Convert.ToInt32(txtfxxl.Text) + Convert.ToInt32(txtf3xl.Text) + Convert.ToInt32(txtf4xl.Text) +
                            Convert.ToInt32(txth30.Text) + Convert.ToInt32(txth32.Text) + Convert.ToInt32(txth34.Text) + Convert.ToInt32(txth36.Text) + Convert.ToInt32(txthxs.Text) + Convert.ToInt32(txths.Text) +
                            Convert.ToInt32(txthm.Text) + Convert.ToInt32(txthl.Text) + Convert.ToInt32(txthxl.Text) + Convert.ToInt32(txthxxl.Text) + Convert.ToInt32(txth3xl.Text) + Convert.ToInt32(txth4xl.Text);

                drNew["Total"] = Total.ToString();

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];

                dtddd.Merge(dt);
            }
            else
            {
                drNew = dttt.NewRow();
                drNew["DesignCode"] = ddlItem.SelectedItem.Text + "-" + txtdesignno.Text + "-" + txtcolor.Text;
                drNew["Itemname"] = ddlItem.SelectedItem.Text;

                drNew["F30"] = txtf30.Text;
                drNew["F32"] = txtf32.Text;
                drNew["F34"] = txtf34.Text;
                drNew["F36"] = txtf36.Text;
                drNew["FXS"] = txtfxs.Text;
                drNew["FS"] = txtfs.Text;
                drNew["FM"] = txtfm.Text;
                drNew["FL"] = txtfl.Text;
                drNew["FXL"] = txtfxl.Text;
                drNew["FXXL"] = txtfxxl.Text;
                drNew["F3XL"] = txtf3xl.Text;
                drNew["F4XL"] = txtf4xl.Text;

                drNew["H30"] = txth30.Text;
                drNew["H32"] = txth32.Text;
                drNew["H34"] = txth34.Text;
                drNew["H36"] = txth36.Text;
                drNew["HXS"] = txthxs.Text;
                drNew["HS"] = txths.Text;
                drNew["HM"] = txthm.Text;
                drNew["HL"] = txthl.Text;
                drNew["HXL"] = txthxl.Text;
                drNew["HXXL"] = txthxxl.Text;
                drNew["H3XL"] = txth3xl.Text;
                drNew["H4XL"] = txth4xl.Text;

                int Total = Convert.ToInt32(txtf30.Text) + Convert.ToInt32(txtf32.Text) + Convert.ToInt32(txtf34.Text) + Convert.ToInt32(txtf36.Text) + Convert.ToInt32(txtfxs.Text) + Convert.ToInt32(txtfs.Text) +
                            Convert.ToInt32(txtfm.Text) + Convert.ToInt32(txtfl.Text) + Convert.ToInt32(txtfxl.Text) + Convert.ToInt32(txtfxxl.Text) + Convert.ToInt32(txtf3xl.Text) + Convert.ToInt32(txtf4xl.Text) +
                            Convert.ToInt32(txth30.Text) + Convert.ToInt32(txth32.Text) + Convert.ToInt32(txth34.Text) + Convert.ToInt32(txth36.Text) + Convert.ToInt32(txthxs.Text) + Convert.ToInt32(txths.Text) +
                            Convert.ToInt32(txthm.Text) + Convert.ToInt32(txthl.Text) + Convert.ToInt32(txthxl.Text) + Convert.ToInt32(txthxxl.Text) + Convert.ToInt32(txth3xl.Text) + Convert.ToInt32(txth4xl.Text);

                drNew["Total"] = Total.ToString();

                dstd.Tables[0].Rows.Add(drNew);
                dtddd = dstd.Tables[0];

            }




            ViewState["CurrentTable1"] = dtddd;

            gvcustomerorder.DataSource = dtddd;
            gvcustomerorder.DataBind();

            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {

                #region Load Data from Grid

                Label lbldesignno = (Label)gvcustomerorder.Rows[i].FindControl("lbldesignno");
                Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");

               

                TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");
                

                TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");

                Label lblTotal = (Label)gvcustomerorder.Rows[i].FindControl("lblTotal");

                lbldesignno.Text = dstd.Tables[0].Rows[i]["DesignCode"].ToString();
                lblitemname.Text = dstd.Tables[0].Rows[i]["Itemname"].ToString();
                txts30fs.Text = dstd.Tables[0].Rows[i]["F30"].ToString();
                txts30hs.Text = dstd.Tables[0].Rows[i]["H30"].ToString();

                txts32fs.Text = dstd.Tables[0].Rows[i]["F32"].ToString();
                txts32hs.Text = dstd.Tables[0].Rows[i]["H32"].ToString();

                txts34fs.Text = dstd.Tables[0].Rows[i]["F34"].ToString();
                txts34hs.Text = dstd.Tables[0].Rows[i]["H34"].ToString();

                txts36fs.Text = dstd.Tables[0].Rows[i]["F36"].ToString();
                txts36hs.Text = dstd.Tables[0].Rows[i]["H36"].ToString();

                txtsxsfs.Text = dstd.Tables[0].Rows[i]["FXS"].ToString();
                txtsxshs.Text = dstd.Tables[0].Rows[i]["HXS"].ToString();

                txtslfs.Text = dstd.Tables[0].Rows[i]["FL"].ToString();
                txtslhs.Text = dstd.Tables[0].Rows[i]["HL"].ToString();

                txtssfs.Text = dstd.Tables[0].Rows[i]["FS"].ToString();
                txtsshs.Text = dstd.Tables[0].Rows[i]["HS"].ToString();

                txtsmfs.Text = dstd.Tables[0].Rows[i]["FM"].ToString();
                txtsmhs.Text = dstd.Tables[0].Rows[i]["HM"].ToString();

                txtsxlfs.Text = dstd.Tables[0].Rows[i]["FXL"].ToString();
                txtsxlhs.Text = dstd.Tables[0].Rows[i]["HXL"].ToString();

                txtsxxlfs.Text = dstd.Tables[0].Rows[i]["FXXL"].ToString();
                txtsxxlhs.Text = dstd.Tables[0].Rows[i]["HXXL"].ToString();

                txts3xlfs.Text = dstd.Tables[0].Rows[i]["F3XL"].ToString();
                txts3xlhs.Text = dstd.Tables[0].Rows[i]["H3XL"].ToString();

                txts4xlfs.Text = dstd.Tables[0].Rows[i]["F4XL"].ToString();
                txts4xlhs.Text = dstd.Tables[0].Rows[i]["H4XL"].ToString();

                lblTotal.Text = dstd.Tables[0].Rows[i]["Total"].ToString();

                #endregion


            }

          //  ddlItem.SelectedIndex = 0;
            txtdesignno.Text = "";
            txtcolor.Text = "";

           txtf30.Text="0";
           txtf32.Text = "0";
           txtf34.Text = "0";
           txtf36.Text = "0";
           txtfxs.Text = "0";
           txtfs.Text = "0";
           txtfm.Text = "0";
           txtfl.Text = "0";
           txtfxl.Text = "0";
           txtfxxl.Text = "0";
           txtf3xl.Text = "0";
           txtf4xl.Text = "0";

           txth30.Text = "0";
           txth32.Text = "0";
           txth34.Text = "0";
           txth36.Text = "0";
           txthxs.Text = "0";
           txths.Text = "0";
           txthm.Text = "0";
           txthl.Text = "0";
           txthxl.Text = "0";
           txthxxl.Text = "0";
           txth3xl.Text = "0";
           txth4xl.Text = "0";

        }
        protected void btnsave_OnCLick(object sender, EventArgs e)
        {
            if (ddlbrand.SelectedValue == "Select Brand")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Brand. Thank you');", true);
                ddlbrand.Focus();
                return;
            }
            if (ddlfit.SelectedValue == "Select Fit")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Fit. Thank you');", true);
                ddlfit.Focus();
                return;
            }
            if (txtlotno.Text == "" || txtlotno.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Enter LotNo. Thank you');", true);
                txtlotno.Focus();
                return;
            }
            for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            {

                #region Load Data from Grid

                Label lbldesignno = (Label)gvcustomerorder.Rows[i].FindControl("lbldesignno");
                Label lblitemname = (Label)gvcustomerorder.Rows[i].FindControl("lblitemname");



                TextBox txts30fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30fs");
                TextBox txts30hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts30hs");
                TextBox txts32fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32fs");
                TextBox txts32hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts32hs");
                TextBox txts34fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34fs");
                TextBox txts34hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts34hs");
                TextBox txts36fs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36fs");
                TextBox txts36hs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts36hs");
                TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxsfs");
                TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxshs");


                TextBox txtslfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslfs");
                TextBox txtslhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtslhs");
                TextBox txtssfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtssfs");
                TextBox txtsshs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsshs");
                TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmfs");
                TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsmhs");
                TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlfs");
                TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxlhs");
                TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlfs");
                TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txtsxxlhs");
                TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlfs");
                TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts3xlhs");
                TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlfs");
                TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[i].FindControl("txts4xlhs");

                Label lblTotal = (Label)gvcustomerorder.Rows[i].FindControl("lblTotal");

                DateTime Date = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                 DataSet dsGetFinishdetails = objBs.GetFinishdetails(Convert.ToString(ddlfit.SelectedValue), Convert.ToString(txtlotno.Text), lbldesignno.Text);
                 if (dsGetFinishdetails.Tables[0].Rows.Count > 0)
                 {
                     int istockid = objBs.updatenewallstockiro("0", Convert.ToString(lblTotal.Text), txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y", Convert.ToString(txtlotno.Text), lbldesignno.Text, Convert.ToString(ddlfit.SelectedValue));
                 }
                 else
                 {
                     int istockid = objBs.insertFinishedstockwisestock("0", "0", "0", "0", lbldesignno.Text, ddlbrand.SelectedValue, ddlfit.SelectedValue, lblitemname.Text, drpbranch.SelectedValue, txtlotno.Text, lblTotal.Text, "0", txts30fs.Text, txts32fs.Text, txts34fs.Text, txts36fs.Text, txtsxsfs.Text, txtssfs.Text, txtsmfs.Text, txtslfs.Text, txtsxlfs.Text, txtsxxlfs.Text, txts3xlfs.Text, txts4xlfs.Text, txts30hs.Text, txts32hs.Text, txts34hs.Text, txts36hs.Text, txtsxshs.Text, txtsshs.Text, txtsmhs.Text, txtslhs.Text, txtsxlhs.Text, txtsxxlhs.Text, txts3xlhs.Text, txts4xlhs.Text, "Y");
                 }
                 
                #endregion
            }
            System.Threading.Thread.Sleep(3000);
            Response.Redirect("Home_Page.aspx");
        }
        protected void gridprint_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                #region

                if (e.Row.Cells[5].Text != "-" && e.Row.Cells[5].Text != "")
                {
                    F30 = F30 + Convert.ToDouble(e.Row.Cells[5].Text);
                }
                if (e.Row.Cells[6].Text != "-" && e.Row.Cells[6].Text != "")
                {
                    F32 = F32 + Convert.ToDouble(e.Row.Cells[6].Text);
                }
                if (e.Row.Cells[7].Text != "-" && e.Row.Cells[7].Text != "")
                {
                    F34 = F34 + Convert.ToDouble(e.Row.Cells[7].Text);
                }
                if (e.Row.Cells[8].Text != "-" && e.Row.Cells[8].Text != "")
                {
                    F36 = F36 + Convert.ToDouble(e.Row.Cells[8].Text);
                }
                if (e.Row.Cells[9].Text != "-" && e.Row.Cells[9].Text != "")
                {
                    FXS = FXS + Convert.ToDouble(e.Row.Cells[9].Text);
                }
                if (e.Row.Cells[10].Text != "-" && e.Row.Cells[10].Text != "")
                {
                    FS = FS + Convert.ToDouble(e.Row.Cells[10].Text);
                }
                if (e.Row.Cells[11].Text != "-" && e.Row.Cells[11].Text != "")
                {
                    FM = FM + Convert.ToDouble(e.Row.Cells[11].Text);
                }
                if (e.Row.Cells[12].Text != "-" && e.Row.Cells[12].Text != "")
                {
                    FL = FL + Convert.ToDouble(e.Row.Cells[12].Text);
                }
                if (e.Row.Cells[13].Text != "-" && e.Row.Cells[13].Text != "")
                {
                    FXL = FXL + Convert.ToDouble(e.Row.Cells[13].Text);
                }
                if (e.Row.Cells[14].Text != "-" && e.Row.Cells[14].Text != "")
                {
                    FXXL = FXXL + Convert.ToDouble(e.Row.Cells[14].Text);
                }
                if (e.Row.Cells[15].Text != "-" && e.Row.Cells[15].Text != "")
                {
                    F3XL = F3XL + Convert.ToDouble(e.Row.Cells[15].Text);
                }
                if (e.Row.Cells[16].Text != "-" && e.Row.Cells[16].Text != "")
                {
                    F4XL = F4XL + Convert.ToDouble(e.Row.Cells[16].Text);
                }
                if (e.Row.Cells[17].Text != "-" && e.Row.Cells[17].Text != "")
                {

                    H30 = H30 + Convert.ToDouble(e.Row.Cells[17].Text);
                }
                if (e.Row.Cells[18].Text != "-" && e.Row.Cells[18].Text != "")
                {
                    H32 = H32 + Convert.ToDouble(e.Row.Cells[18].Text);
                }
                if (e.Row.Cells[19].Text != "-" && e.Row.Cells[19].Text != "")
                {
                    H34 = H34 + Convert.ToDouble(e.Row.Cells[19].Text);
                }
                if (e.Row.Cells[20].Text != "-" && e.Row.Cells[20].Text != "")
                {
                    H36 = H36 + Convert.ToDouble(e.Row.Cells[20].Text);
                }
                if (e.Row.Cells[21].Text != "-" && e.Row.Cells[21].Text != "")
                {
                    HXS = HXS + Convert.ToDouble(e.Row.Cells[21].Text);
                }
                if (e.Row.Cells[22].Text != "-" && e.Row.Cells[22].Text != "")
                {
                    HS = HS + Convert.ToDouble(e.Row.Cells[22].Text);
                }
                if (e.Row.Cells[23].Text != "-" && e.Row.Cells[23].Text != "")
                {
                    HM = HM + Convert.ToDouble(e.Row.Cells[23].Text);
                }
                if (e.Row.Cells[24].Text != "-" && e.Row.Cells[24].Text != "")
                {
                    HL = HL + Convert.ToDouble(e.Row.Cells[24].Text);
                }
                if (e.Row.Cells[25].Text != "-" && e.Row.Cells[25].Text != "")
                {
                    HXL = HXL + Convert.ToDouble(e.Row.Cells[25].Text);
                }
                if (e.Row.Cells[26].Text != "-" && e.Row.Cells[26].Text != "")
                {
                    HXXL = HXXL + Convert.ToDouble(e.Row.Cells[26].Text);
                }
                if (e.Row.Cells[27].Text != "-" && e.Row.Cells[27].Text != "")
                {
                    H3XL = H3XL + Convert.ToDouble(e.Row.Cells[27].Text);
                }
                if (e.Row.Cells[28].Text != "-" && e.Row.Cells[28].Text != "")
                {
                    H4XL = H4XL + Convert.ToDouble(e.Row.Cells[28].Text);
                }
                TOTAL = TOTAL + Convert.ToDouble(e.Row.Cells[29].Text);

                #endregion

              

             

            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                #region
                e.Row.Cells[3].Text = F30.ToString();
                e.Row.Cells[4].Text = F32.ToString();
                e.Row.Cells[5].Text = F34.ToString();
                e.Row.Cells[6].Text = F36.ToString();
                e.Row.Cells[7].Text = FXS.ToString();
                e.Row.Cells[8].Text = FS.ToString();
                e.Row.Cells[9].Text = FM.ToString();
                e.Row.Cells[10].Text = FL.ToString();
                e.Row.Cells[11].Text = FXL.ToString();
                e.Row.Cells[12].Text = FXXL.ToString();
                e.Row.Cells[13].Text = F3XL.ToString();
                e.Row.Cells[14].Text = F4XL.ToString();

                e.Row.Cells[15].Text = H30.ToString();
                e.Row.Cells[16].Text = H32.ToString();
                e.Row.Cells[17].Text = H34.ToString();
                e.Row.Cells[18].Text = H36.ToString();
                e.Row.Cells[19].Text = HXS.ToString();
                e.Row.Cells[20].Text = HS.ToString();
                e.Row.Cells[21].Text = HM.ToString();
                e.Row.Cells[22].Text = HL.ToString();
                e.Row.Cells[23].Text = HXL.ToString();
                e.Row.Cells[24].Text = HXXL.ToString();
                e.Row.Cells[25].Text = H3XL.ToString();
                e.Row.Cells[26].Text = H3XL.ToString();

                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[13].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[14].HorizontalAlign = HorizontalAlign.Right;

                e.Row.Cells[15].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[16].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[17].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[18].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[19].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[20].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[21].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[22].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[23].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[24].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[25].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[26].HorizontalAlign = HorizontalAlign.Right;


                e.Row.Cells[3].Font.Bold = true;
                e.Row.Cells[4].Font.Bold = true;
                e.Row.Cells[5].Font.Bold = true;
                e.Row.Cells[6].Font.Bold = true;
                e.Row.Cells[7].Font.Bold = true;
                e.Row.Cells[8].Font.Bold = true;
                e.Row.Cells[9].Font.Bold = true;
                e.Row.Cells[10].Font.Bold = true;
                e.Row.Cells[11].Font.Bold = true;
                e.Row.Cells[12].Font.Bold = true;
                e.Row.Cells[13].Font.Bold = true;
                e.Row.Cells[14].Font.Bold = true;

                e.Row.Cells[15].Font.Bold = true;
                e.Row.Cells[16].Font.Bold = true;
                e.Row.Cells[17].Font.Bold = true;
                e.Row.Cells[18].Font.Bold = true;
                e.Row.Cells[19].Font.Bold = true;
                e.Row.Cells[20].Font.Bold = true;
                e.Row.Cells[21].Font.Bold = true;
                e.Row.Cells[22].Font.Bold = true;
                e.Row.Cells[23].Font.Bold = true;
                e.Row.Cells[24].Font.Bold = true;
                e.Row.Cells[25].Font.Bold = true;
                e.Row.Cells[26].Font.Bold = true;

                e.Row.Cells[4].Text = "TOTAL :";
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[4].Font.Bold = true;

                e.Row.Cells[29].Text = TOTAL.ToString();
                e.Row.Cells[29].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[29].Font.Bold = true;


                #endregion
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           // SetRowData();

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

                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();

                    SetPreviousData();
                   // FirstGridViewRow();
                }
            }

        }
        private void SetRowData()
        {
            //int rowIndex = 0;

            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable1"];
            //    DataRow drCurrentRow = null;
            //    if (dtCurrentTable.Rows.Count > 0)
            //    {
            //        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            //        {

            //            TextBox lbldesignno =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("lbldesignno");
            //            TextBox lblitemname =
            //             (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("lblitemname");

                      
            //            drCurrentRow = dtCurrentTable.NewRow();
            //            drCurrentRow["Orderno"] = i + 1;
            //            dtCurrentTable.Rows[i - 1]["prodname"] = txtprodname.Text;

            //            dtCurrentTable.Rows[i - 1]["Quantity"] = txtRecQuantity.Text;
            //            dtCurrentTable.Rows[i - 1]["prod"] = txtprod.Text;
            //            dtCurrentTable.Rows[i - 1]["stock"] = txtstock.Text;

            //            rowIndex++;

            //        }

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

                        Label lbldesignno =
                         (Label)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("lbldesignno");
                        Label lblitemname =
                         (Label)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("lblitemname");

                        TextBox txts30fs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts30fs");
                        TextBox txts30hs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts30hs");
                        TextBox txts32fs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts32fs");
                        TextBox txts32hs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts32hs");
                        TextBox txts34fs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts34fs");
                        TextBox txts34hs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts34hs");
                        TextBox txts36fs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts36fs");
                        TextBox txts36hs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts36hs");
                        TextBox txtsxsfs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtsxsfs");
                        TextBox txtsxshs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtsxshs");


                        TextBox txtslfs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtslfs");
                        TextBox txtslhs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtslhs");
                        TextBox txtssfs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtssfs");
                        TextBox txtsshs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtsshs");
                        TextBox txtsmfs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtsmfs");
                        TextBox txtsmhs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtsmhs");
                        TextBox txtsxlfs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtsxlfs");
                        TextBox txtsxlhs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtsxlhs");
                        TextBox txtsxxlfs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtsxxlfs");
                        TextBox txtsxxlhs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txtsxxlhs");
                        TextBox txts3xlfs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts3xlfs");
                        TextBox txts3xlhs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts3xlhs");
                        TextBox txts4xlfs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts4xlfs");
                        TextBox txts4xlhs = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[2].FindControl("txts4xlhs");

                        Label lblTotal = (Label)gvcustomerorder.Rows[i].FindControl("lblTotal");

                       // txtProdname.Text = dt.Rows[i]["Prodname"].ToString();

                        lbldesignno.Text = dt.Rows[i]["DesignCode"].ToString();
                        lblitemname.Text = dt.Rows[i]["Itemname"].ToString();

                        txts30fs.Text = dt.Rows[i]["F30"].ToString();
                        txts30hs.Text = dt.Rows[i]["H30"].ToString();

                        txts32fs.Text = dt.Rows[i]["F32"].ToString();
                        txts32hs.Text = dt.Rows[i]["H32"].ToString();

                        txts34fs.Text = dt.Rows[i]["F34"].ToString();
                        txts34hs.Text = dt.Rows[i]["H34"].ToString();

                        txts36fs.Text = dt.Rows[i]["F36"].ToString();
                        txts36hs.Text = dt.Rows[i]["H36"].ToString();

                        txtsxsfs.Text = dt.Rows[i]["FXS"].ToString();
                        txtsxshs.Text = dt.Rows[i]["HXS"].ToString();

                        txtslfs.Text = dt.Rows[i]["FL"].ToString();
                        txtslhs.Text = dt.Rows[i]["HL"].ToString();

                        txtssfs.Text = dt.Rows[i]["FS"].ToString();
                        txtsshs.Text = dt.Rows[i]["HS"].ToString();

                        txtsmfs.Text = dt.Rows[i]["FM"].ToString();
                        txtsmhs.Text = dt.Rows[i]["HM"].ToString();

                        txtsxlfs.Text = dt.Rows[i]["FXL"].ToString();
                        txtsxlhs.Text = dt.Rows[i]["HXL"].ToString();

                        txtsxxlfs.Text = dt.Rows[i]["FXXL"].ToString();
                        txtsxxlhs.Text = dt.Rows[i]["HXXL"].ToString();

                        txts3xlfs.Text = dt.Rows[i]["F3XL"].ToString();
                        txts3xlhs.Text = dt.Rows[i]["H3XL"].ToString();

                        txts4xlfs.Text = dt.Rows[i]["F4XL"].ToString();
                        txts4xlhs.Text = dt.Rows[i]["H4XL"].ToString();

                        lblTotal.Text = dt.Rows[i]["Total"].ToString();


                        //rowIndex++;
                        //drpProd.Focus();
                    }
                }
            }
        }
    }
}