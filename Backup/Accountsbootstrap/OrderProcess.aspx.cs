using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Billing.Accountsbootstrap
{
    public partial class OrderProcess : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string iDealer = "";
        string empid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");
            empid = Session["Empid"].ToString();

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();
            if (!IsPostBack)
            {
                btnPrint.Visible = false;
                btnDelete.Visible = false;

                DataSet brandName = new DataSet();
                brandName = objBs.getBrandName();

                if (brandName != null)
                {
                    if (brandName.Tables[0].Rows.Count > 0)
                    {
                        ddlBrand.DataSource = brandName.Tables[0];
                        ddlBrand.DataTextField = "BrandName";
                        ddlBrand.DataValueField = "BrandId";
                        ddlBrand.DataBind();
                        ddlBrand.Items.Insert(0, "Select Brand");
                    }
                }

                DataSet dcompany = objBs.GetCompanyDetails();
                if (dcompany.Tables[0].Rows.Count > 0)
                {
                    drpcompany.DataSource = dcompany.Tables[0];
                    drpcompany.DataTextField = "Companycode";
                    drpcompany.DataValueField = "comapanyId";
                    drpcompany.DataBind();
                  //  ddlBrand.Items.Insert(0, "Select Company");
                }

                DataSet dsagent = objBs.getagenttype();
                if (dsagent.Tables[0].Rows.Count > 0)
                {
                    drpagent.DataSource = dsagent.Tables[0];
                    drpagent.DataTextField = "Ledgername";
                    drpagent.DataValueField = "LedgerID";
                    drpagent.DataBind();
                    drpagent.Items.Insert(0, "Select Agent Name");
                }


                DateTime indianStd = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "India Standard Time");
                string dtaa = Convert.ToDateTime(indianStd).ToString("dd/MM/yyyy");

                DataSet ds = new DataSet();
                ds = objBs.getmaaxBillnofororder();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtinvno.Text = ds.Tables[0].Rows[0]["billId"].ToString();
                    FirstGridViewRow();

                    DataSet dsup = objBs.getnewsupplierforfab();
                    if (dsup.Tables[0].Rows.Count > 0)
                    {

                        drpsupplier.DataSource = dsup.Tables[0];
                        drpsupplier.DataTextField = "LEdgerName";
                        drpsupplier.DataValueField = "LedgerID";
                        drpsupplier.DataBind();
                        drpsupplier.Items.Insert(0, "Select Supplier");
                    }

                    DataSet dst = objBs.hrmgridview();
                    if (dst != null)
                    {
                        if (dst.Tables[0].Rows.Count > 0)
                        {
                            drpchecked.DataSource = dst.Tables[0];
                            drpchecked.DataTextField = "Name";
                            drpchecked.DataValueField = "Employee_Id";
                            drpchecked.DataBind();
                            drpchecked.Items.Insert(0, "Select Employee Name");
                        }
                    }
                    txtregdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    txtinvdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }


                string iCusID = Request.QueryString.Get("iid");
                if (iCusID != "" || iCusID != null)
                {
                    DataSet dsd = objBs.getordernoforid(Convert.ToInt32(iCusID));
                    if (dsd.Tables[0].Rows.Count > 0)
                    {
                        txtregdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        btnadd.Text = "Update";
                        drpsupplier.SelectedValue = Convert.ToInt32(dsd.Tables[0].Rows[0]["Supplierid"]).ToString();
                        txtinvno.Text = Convert.ToString(dsd.Tables[0].Rows[0]["orderno"]).ToString();
                        txtregdate.Text =Convert.ToDateTime(dsd.Tables[0].Rows[0]["OrderDate"].ToString()).ToString("dd/MM/yyyy");
                        ////lblRegisterdate.Text = Convert.ToDateTime(ds2.Tables[0].Rows[0]["RegDate"].ToString()).ToString("dd/MM/yyyy");
                        addressshow.Text = dsd.Tables[0].Rows[0]["addr"].ToString();
                        drpagent.SelectedValue = Convert.ToInt32(dsd.Tables[0].Rows[0]["AgentName"]).ToString();
                        drpcompany.SelectedValue = dsd.Tables[0].Rows[0]["Companyid"].ToString();

                        txtsupplierorderno.Text = dsd.Tables[0].Rows[0]["SupplierOrderno"].ToString();
                        txttotmet.Text = dsd.Tables[0].Rows[0]["TotalMeter"].ToString();

                        DataSet dsdd = objBs.gettransordernoforid(Convert.ToInt32(iCusID));
                        if (dsdd.Tables[0].Rows.Count > 0)
                        {
                            DataTable dttt;
                            DataRow drNew;
                            DataColumn dct;
                            DataSet dstd = new DataSet();
                            dttt = new DataTable();

                            dct = new DataColumn("OrderNo");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Design");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Item");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Color");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("remPiece");
                            dttt.Columns.Add(dct);
                            dct = new DataColumn("Piece");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Meter");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Width");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Rate");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("FileUpload");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Image");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("ImageLabel");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("Cancel");
                            dttt.Columns.Add(dct);

                            dct = new DataColumn("CancelMeter");
                            dttt.Columns.Add(dct);
                            
                            dstd.Tables.Add(dttt);
                            foreach (DataRow dr in dsdd.Tables[0].Rows)
                            {
                                drNew = dttt.NewRow();
                                drNew["OrderNo"] = dr["Orderno"];
                                drNew["Design"] = dr["DesignNo"];
                                drNew["Item"] = dr["ItemName"];
                                drNew["Color"] = dr["Color"];
                                drNew["Piece"] = dr["Piece"];
                                drNew["remPiece"] = dr["retmeter"];
                                drNew["Meter"] = dr["meter"];
                                drNew["Width"] = dr["Width"];
                                drNew["Rate"] = dr["Rate"];
                                drNew["Cancel"] = dr["Cancel"];
                                //drNew["image"] = dr["UnitPrice"];
                                drNew["imageLabel"] = dr["imagepath"];
                                drNew["CancelMeter"] = dr["CancelMeter"];

                                dstd.Tables[0].Rows.Add(drNew);
                            }

                            ViewState["CurrentTable1"] = dttt;

                            gvcustomerorder.DataSource = dstd;
                            gvcustomerorder.DataBind();

                            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                            {
                                TextBox orderno = (TextBox)gvcustomerorder.Rows[vLoop].Cells[0].FindControl("txtno");
                                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");


                                TextBox txtitem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");

                                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                                TextBox txtrempiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrempiece");

                                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");

                                CheckBox cancel = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("chkid");
                                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                                Label imgpath = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");

                                DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

                                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                                TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcancelmeter");
                                //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");

                                orderno.Text = dstd.Tables[0].Rows[vLoop]["orderno"].ToString();
                                txtdesign.Text = dstd.Tables[0].Rows[vLoop]["design"].ToString();
                                txtitem.Text = dstd.Tables[0].Rows[vLoop]["Item"].ToString();
                                txtpiece.Text = Convert.ToInt32(dstd.Tables[0].Rows[vLoop]["piece"]).ToString();
                                txtrempiece.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["remPiece"]).ToString();
                                txtmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["meter"]).ToString("N2");
                                txtrate.Text = dstd.Tables[0].Rows[vLoop]["rate"].ToString();
                                if(dstd.Tables[0].Rows[vLoop]["cancel"].ToString() =="1")
                                {
                                cancel.Checked= true;
                                }
                                else
                                {
                                    cancel.Checked = false;
                                }

                                txtcancelmeter.Text = Convert.ToDouble(dstd.Tables[0].Rows[vLoop]["CancelMeter"]).ToString("N2");

                                drpwid.SelectedValue = dstd.Tables[0].Rows[vLoop]["width"].ToString();
                                txtcolor.Text = dstd.Tables[0].Rows[vLoop]["color"].ToString();
                                //drpcolor.SelectedValue = dstd.Tables[0].Rows[vLoop]["Color"].ToString();
                                imgpath.Text = dstd.Tables[0].Rows[vLoop]["imageLabel"].ToString();
                            }
                        }
                    }
                }
            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void gridbutton_click(object sender, EventArgs e)
        {
            Response.Redirect("salesgrid.aspx");
        }

        protected void newcstomer_click(object sender, EventArgs e)
        {
            Response.Redirect("customermaster.aspx");
        }

        protected void TextBox9_TextChanged(object sender, EventArgs e)
        {
        }

        protected void bblbillto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtbillcheck(object sender, EventArgs e)
        {

        }

        protected void ddlPayMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpsupplier.SelectedValue == "Select Supplier")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Supplier.Thank You!!!');", true);
                return;
            }
            else
            {
                DataSet dcust = objBs.GetCustomerDetails(Convert.ToInt32(drpsupplier.SelectedValue));
                if (dcust.Tables[0].Rows.Count > 0)
                {
                    addressshow.Text = dcust.Tables[0].Rows[0]["Address"].ToString();
                    drpagent.SelectedValue = dcust.Tables[0].Rows[0]["AgentId"].ToString();
                }
            }
        }

        protected void ddlrep_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvcustomerorderchanged(object sender, EventArgs e)
        {
            //Get the selected row
            GridViewRow row = gvcustomerorder.SelectedRow;
            if (row != null)
            {
                //First find the control in template column and then get the value
                //Change the cell index(1) of column as per your design
                // Label2.Text = (row.FindControl("lblLocalTime") as Label).Text;
                //  DropDownList drop = (row.FindControl("lblLocalTime") as DropDownList).Text;
            }
        }

        protected void ddlcustomerID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtmobileno_TextChanged2(object sender, EventArgs e)
        {

        }

        protected void checkbox1_changed(object sender, EventArgs e)
        {
            if (chknewcust.Checked == true)
            {

            }
            else
            {

            }
        }

        protected void chknew_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet dswidth = objBs.GetWidth();

            DataSet dscolor = objBs.gridColor();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtno = (TextBox)e.Row.FindControl("txtno");
                TextBox txtdesign = (TextBox)e.Row.FindControl("txtdesno");
                TextBox txtcolor = (TextBox)e.Row.FindControl("txtcolor");
                //DropDownList drpcolor = (DropDownList)e.Row.FindControl("drpcolor");
                TextBox txtpiece = (TextBox)e.Row.FindControl("txtpiece");
                TextBox txtmeter = (TextBox)e.Row.FindControl("txtmeter");
                TextBox txtcancelmeter = (TextBox)e.Row.FindControl("txtcancelmeter");
                TextBox txtrate = (TextBox)e.Row.FindControl("txtRate");
                DropDownList drpwid = (DropDownList)e.Row.FindControl("drpwid");

                txtno.Text = "1";
                txtdesign.Text = "";
                txtpiece.Text = "";
                txtmeter.Text = "0";
                txtcancelmeter.Text = "0";
               // txtcolor.Text = "";
                txtrate.Text = "0";

                var ddl = (DropDownList)e.Row.FindControl("drpwid");
                ddl.DataSource = dswidth;
                ddl.DataTextField = "Width";
                ddl.DataValueField = "Widthid";
                ddl.DataBind();
                ddl.Items.Insert(0, "Select Width");


                //var ddl1 = (DropDownList)e.Row.FindControl("drpcolor");
                //ddl1.DataSource = dscolor;
                //ddl1.DataTextField = "Color";
                //ddl1.DataValueField = "Colorid";
                //ddl1.DataBind();
                //ddl1.Items.Insert(0, "Select Color");

            }

        }

        protected void ddlDef_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ButtonAdd2_Click1(object sender, EventArgs e)
        {
            int No = 0;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtItem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");
                DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcancelmeter");
                //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");

                if (txtdesign.Text == "")// || drpcolor.SelectedValue == "Select Color")
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
                //DropDownList drpcol = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                if (vLoop == gvcustomerorder.Rows.Count - 1)
                {
                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtdesno");
                    TextBox txtItem = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtItem");
                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtpiece");
                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtmeter");
                    DropDownList drpwidth = (DropDownList)gvcustomerorder.Rows[vLoop - 1].FindControl("drpwid");
                    //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop - 1].FindControl("drpcolor");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
                    TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtcancelmeter");

                    TextBox txtdes = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                    TextBox txtIte = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                    TextBox txtpie = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                    TextBox txtme = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                    TextBox txtra = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    TextBox txtcol = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtColor");
                    TextBox txtcanmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcancelmeter");

                    DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");
                  

                    txtdes.Text = txtdesign.Text;
                    txtIte.Text = txtItem.Text;
                    txtpie.Text = txtpiece.Text;
                    //txtdes.Enabled = false;
                    Regex regex = new Regex(@"^[0-9]+$");
                    if (!regex.IsMatch(txtcolor.Text))
                    {
                        txtcol.Text = "";
                    }
                    else
                    {
                        int iCol = Convert.ToInt32(txtcolor.Text) + 1;
                        txtcol.Text = Convert.ToString(iCol);
                    }

                    txtme.Text = txtmeter.Text;
                    txtra.Text = txtrate.Text;
                    // txtra.Enabled = false;
                    drpwid.SelectedValue = drpwidth.SelectedValue;
                    //drpcol.SelectedValue = drpcolor.SelectedValue;

                    txtcanmeter.Text = txtcancelmeter.Text;
                    drpwid.Enabled = false;


                }

                  txtcolor.Focus();
            }
            txtmeter_textchanged(sender, e);
            txtrrattee_textchanged(sender, e);

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txtco = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                //DropDownList drpcol = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");

                txtco.Focus();
            }



        }

        protected void ButtonAdd1_Click(object sender, EventArgs e)
        {
            int No = 0;
            txtmeter_textchanged(sender, e);
            txtrrattee_textchanged(sender, e);
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtItem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");
                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");
                DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

                TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcancelmeter");

                //TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                //TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                if (txtdesign.Text == "")// || drpcolor.SelectedValue == "Select Color")
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
                TextBox txtItem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");

                txtItem.Focus();
            }

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtItem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtrempiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrempiece");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcancelmeter");


                //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");


                if (txtdesign.Text != "")// || drpcolor.SelectedValue != "Select Color")
                {
                }
                else
                {
                    txtmeter.Text = "0";
                    txtrate.Text = "0";
                    txtcancelmeter.Text = "0";
                }
            }

            // AddNewRow();
            // int Sno = 0;
            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList txttt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpCategory");
            //    DropDownList txttd = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //    //Sno++;
            //    //txtno.Text = Convert.ToString(Sno);
            //    txtno.Focus();
            //    string qty = txtttk.Text;
            //    string rate = txttk.Text;
            //    string tax = txtkt.Text;
            //    string amount = txtkttt.Text;
            //    string stock = txtktt.Text;
            //    string discount = txtktttt.Text;
            //    string no = txtno.Text;

            //    if (qty != "" || rate != "" || tax != "" || amount != "" || stock != "" || discount != "" || no != "")
            //    {

            //    }
            //    else
            //    {
            //        txtttk.Text = "0";
            //        txttk.Text = "0";
            //        txtkt.Text = "0";
            //        txtkttt.Text = "0";
            //        txtktt.Text = "0";
            //        txtktttt.Text = "0";

            //    }

            //}

        }

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                FileUpload FileUpload2 = (FileUpload)gvcustomerorder.Rows[vLoop].FindControl("idupload");
                Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");
                if (FileUpload2.HasFile)
                {
                    FileUpload2.SaveAs(MapPath("~/Files/" + FileUpload2.FileName));
                    imgurl.ImageUrl = imgurl.ImageUrl = "~/Files/" + FileUpload2.FileName;
                    //lbl1.Text = FileUpload2.FileName;
                    txtkttt.Text = imgurl.ImageUrl;
                }
            }
        }

        private void AddNewRow()
        {
            int iq = 1;
            int ii = 1;
            string itemc = string.Empty;
            string itemcd = string.Empty;
            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {
                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                TextBox txtItem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");
                //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpcolor");
                TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");

                DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");

                TextBox txtrempiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrempiece");

                TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcancelmeter");

                int col = vLoop + 1;


                txtno.Focus();

                itemc = txtdesign.Text;
                //itemcd = drpcolor.SelectedValue;
                itemcd = txtcolor.Text;


                if ((itemc == null) || (itemc == ""))
                {
                }
                else
                {
                    for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                    {
                        //  DropDownList txt1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("ProductCode");
                        TextBox txtdesign1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtdesno");
                        TextBox txtcolor1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtcolor");
                        //DropDownList drpcolor1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpcolor");
                        if (txtdesign1.Text == "")
                        {
                        }
                        else
                        {

                            if (ii == iq)
                            {
                            }
                            else
                            {
                                if (itemc == txtdesign1.Text && itemcd == txtcolor1.Text)
                                {
                                    itemc = txtdesign.Text;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemc + "  already exists in the Grid.');", true);
                                    //  txt1.Focus();
                                    return;

                                }
                            }
                            ii = ii + 1;
                        }
                    }
                }
                iq = iq + 1;
                ii = 1;





                if (drpwid.SelectedValue == "Select Width")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Width in row " + col + " ')", true);
                    //  txt.Focus();
                    return;
                }
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
                        TextBox txtrempiece = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[0].FindControl("txtrempiece");
                        TextBox txttno =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");

                        TextBox txtdesign =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtdesno");

                        TextBox txtItem =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtItem");

                        TextBox txtcolor =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtcolor");
                       // DropDownList drpcolor =
                       //(DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpcolor");

                        TextBox txtpiece =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtpiece");

                        TextBox txtmeter =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtmeter");

                        TextBox txtrate =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");

                        DropDownList drpwid =
                         (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpwid");

                        FileUpload fileupload =
                         (FileUpload)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("idupload");

                        Button imgbtn = (Button)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("btnUpload");

                        Image imgpre = (Image)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgurl");
                        CheckBox cancel = (CheckBox)gvcustomerorder.Rows[rowIndex].Cells[0].FindControl("chkid");

                        Label lblimg =
                         (Label)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgpreview");

                        TextBox txtcancelmeter =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtcancelmeter");



                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Design"] = txtdesign.Text;
                        dtCurrentTable.Rows[i - 1]["Item"] = txtItem.Text;
                        //dtCurrentTable.Rows[i - 1]["Color"] = drpcolor.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;
                        dtCurrentTable.Rows[i - 1]["Piece"] = txtpiece.Text;
                        dtCurrentTable.Rows[i - 1]["Meter"] = txtmeter.Text;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtrate.Text;
                        dtCurrentTable.Rows[i - 1]["ImageLabel"] = imgpre.ImageUrl;
                        dtCurrentTable.Rows[i - 1]["ImageLabel"] = lblimg.Text;
                        dtCurrentTable.Rows[i - 1]["Orderno"] = txttno.Text;
                        dtCurrentTable.Rows[i - 1]["Width"] = drpwid.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["rempiece"] = txtrempiece.Text;
                        dtCurrentTable.Rows[i - 1]["cancel"] = cancel.Checked;
                        dtCurrentTable.Rows[i - 1]["CancelMeter"] = txtcancelmeter.Text;
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
                        CheckBox cancel = (CheckBox)gvcustomerorder.Rows[rowIndex].Cells[0].FindControl("chkid");
                        TextBox txttno =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");

                        TextBox txtdesign =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtdesno");

                        TextBox txtItem =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtItem");
                        TextBox txtrempiece = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtrempiece");
                        TextBox txtpiece =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtpiece");

                        TextBox txtmeter =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtmeter");

                        TextBox txtrate =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");

                        TextBox txtcolor =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtcolor");

                     //   DropDownList drpcolor =
                     //(DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpcolor");

                        DropDownList drpwid =
                        (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpwid");

                        FileUpload fileupload =
                         (FileUpload)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("idupload");

                        Button imgbtn = (Button)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("btnUpload");

                        Image imgpre = (Image)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgurl");


                        Label lblimg =
                         (Label)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgpreview");

                        TextBox txtcancelmeter =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtcancelmeter");


                        txtdesign.Text = dt.Rows[i]["design"].ToString();
                        txtrempiece.Text = dt.Rows[i]["rempiece"].ToString();
                        txttno.Text = dt.Rows[i]["OrderNo"].ToString();
                        if (txtdesign.Text != null)
                        {

                            cancel.Checked = false;
                            
                        }
                        else
                        {
                            cancel.Checked = Convert.ToBoolean(dt.Rows[i]["cancel"]);
                        }
                        
                        txtItem.Text = dt.Rows[i]["Item"].ToString();
                        txtcolor.Text = dt.Rows[i]["Color"].ToString();
                        //drpcolor.SelectedValue = dt.Rows[i]["Color"].ToString();
                        txtpiece.Text = dt.Rows[i]["Piece"].ToString();

                        txtmeter.Text = dt.Rows[i]["meter"].ToString();
                        txtrate.Text = dt.Rows[i]["Rate"].ToString();

                        imgpre.ImageUrl = dt.Rows[i]["ImageLabel"].ToString();
                        lblimg.Text = dt.Rows[i]["ImageLabel"].ToString();
                        drpwid.SelectedValue = dt.Rows[i]["Width"].ToString();
                        txtcancelmeter.Text = dt.Rows[i]["CancelMeter"].ToString();


                        rowIndex++;

                    }
                }
            }
        }

        private void FirstGridViewRow()
        {
            DataTable dtt = new DataTable();
            DataRow dr = null;
            dtt.Columns.Add(new DataColumn("OrderNo", typeof(string)));
            dtt.Columns.Add(new DataColumn("Item", typeof(string)));
            dtt.Columns.Add(new DataColumn("Design", typeof(string)));
            dtt.Columns.Add(new DataColumn("Color", typeof(string)));
            dtt.Columns.Add(new DataColumn("Piece", typeof(string)));
            dtt.Columns.Add(new DataColumn("Width", typeof(string)));
            dtt.Columns.Add(new DataColumn("Meter", typeof(string)));
            dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dtt.Columns.Add(new DataColumn("Fileupload", typeof(string)));
            dtt.Columns.Add(new DataColumn("remPiece", typeof(string)));
            dtt.Columns.Add(new DataColumn("Image", typeof(string)));
            dtt.Columns.Add(new DataColumn("ImageLabel", typeof(string)));
            dtt.Columns.Add(new DataColumn("Cancel", typeof(string)));
            dtt.Columns.Add(new DataColumn("CancelMeter", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Rate", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Discount", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Tax", typeof(string)));
            //dtt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dr = dtt.NewRow();
            dr["OrderNo"] = string.Empty;
            dr["Item"] = string.Empty;
            dr["Design"] = string.Empty;
            dr["Color"] = string.Empty;
            dr["Piece"] = string.Empty;
            dr["Meter"] = string.Empty;
            dr["Width"] = string.Empty;
            dr["Rate"] = string.Empty;
            dr["Fileupload"] = string.Empty;
            dr["Image"] = string.Empty;
            dr["ImageLabel"] = string.Empty;
            dr["Cancel"] = string.Empty;
            dr["CancelMeter"] = string.Empty;
            //dr["Rate"] = string.Empty;
            //dr["Discount"] = string.Empty;
            //dr["Tax"] = string.Empty;
            //dr["Amount"] = string.Empty;
            dtt.Rows.Add(dr);

            ViewState["CurrentTable1"] = dtt;

            gvcustomerorder.DataSource = dtt;
            gvcustomerorder.DataBind();

            DataTable dttt;
            DataRow drNew;
            DataColumn dct;
            DataSet dstd = new DataSet();
            dttt = new DataTable();
            dct = new DataColumn("remPiece");
            dttt.Columns.Add(dct);
            dct = new DataColumn("OrderNo");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Design");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Item");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Color");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Piece");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Meter");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Width");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Rate");
            dttt.Columns.Add(dct);

            dct = new DataColumn("FileUpload");
            dttt.Columns.Add(dct);

            dct = new DataColumn("Image");
            dttt.Columns.Add(dct);

            dct = new DataColumn("ImageLabel");
            dttt.Columns.Add(dct);
            dct = new DataColumn("Cancel");
            dttt.Columns.Add(dct);

            dct = new DataColumn("CancelMeter");
            dttt.Columns.Add(dct);

            //dct = new DataColumn("Rate");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Discount");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Tax");
            //dttt.Columns.Add(dct);

            //dct = new DataColumn("Amount");
            //dttt.Columns.Add(dct);

            dstd.Tables.Add(dttt);

            drNew = dttt.NewRow();
            drNew["OrderNo"] = 1;
            drNew["Design"] = "";
            drNew["Item"] = "";
            drNew["Color"] = "";
            drNew["Piece"] = "";
            drNew["Meter"] = 0;
            drNew["Width"] = "";
            drNew["Rate"] = 0;
            drNew["Fileupload"] = 0;
            drNew["image"] = "";
            drNew["imageLabel"] = "";
            drNew["remPiece"] = 0;
            drNew["CancelMeter"] = 0;
            //drNew["Product"] = "";
            //drNew["refno"] = 0;
            //drNew["cerno"] = 0;
            //drNew["Discount"] = 0;
            dstd.Tables[0].Rows.Add(drNew);

            gvcustomerorder.DataSource = dstd;
            gvcustomerorder.DataBind();


            //TextBox txn = (TextBox)grvStudentDetails.Rows[0].Cells[1].FindControl("txtName");
            //txn.Focus();
            //Button btnAdd = (Button)grvStudentDetails.FooterRow.Cells[5].FindControl("ButtonAdd");
            //Page.Form.DefaultFocus = btnAdd.ClientID;

        }

        protected void txtAdvance_TextChanged(object sender, EventArgs e)
        {
            //if (txtDiscount.Text != "")
            //{
            //    decimal dDiscount = Convert.ToDecimal(txtDiscount.Text);
            //    decimal dSubTotal = Convert.ToDecimal(txtSubTotal.Text);
            //    decimal Advance = Convert.ToDecimal(txtAdvance.Text);
            //    decimal dDiscAmt = dSubTotal - ((dDiscount * dSubTotal) / 100);
            //    decimal dAmount = dDiscAmt - Advance;
            //    txttotal.Text = dAmount.ToString("f2");
            //}
            //else
            //{

            //}

        }

        protected void granddiscount(object sender, EventArgs e)
        {
            //if (txtgrandtotal.Text != "")
            //{
            //    double grandtotal = 0;
            //    double tax = 0;
            //    double distotal = 0;
            //    int tottqty = 0;
            //    double mettt = 0;
            //    double r = 0;
            //    double disc = 0;

            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //        TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //        TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //        TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //        TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //        TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //        if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" || txttk.Text == "")
            //        {

            //        }
            //        else
            //        {
            //            double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //            double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

            //            double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //            double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //            double total = tx + DiscountAmount;

            //            //  txtkttt.Text = string.Format("{0:N2}", total);

            //            if (txt.SelectedIndex == -1 || txt.SelectedIndex == 0)
            //            {
            //            }
            //            else
            //            {
            //                //DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue), sTableName);
            //                //double mett = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["meter1"]);
            //                //double totm = Convert.ToDouble(txtttk.Text) * mett;
            //                //mettt = mettt + totm;
            //            }

            //            grandtotal = grandtotal + total;
            //            tax = tax + tx;
            //            distotal = distotal + Discount;
            //            tottqty = tottqty + Convert.ToInt32(txtttk.Text);

            //        }
            //    }
            //    double dFreight = 0;
            //    double dLU = 0;
            //    double sumLUFreight = 0;
            //    if (txtFreight.Text.Trim() != "")
            //    {
            //        dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //    }
            //    if (txtLU.Text.Trim() != "")
            //    {
            //        dLU = Convert.ToDouble(txtLU.Text.Trim());
            //    }
            //    sumLUFreight = dFreight + dLU;

            //    txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //    txtdiscountamount.Text = string.Format("{0:N2}", distotal);
            //    double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //    if (roundoff > 0.5)
            //    {
            //        r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //    }
            //    txtroundoff.Text = Convert.ToString(r);
            //    //  txtTaxamt.Text = string.Format("{0:N2}", tax);
            //    //  txtdiscount.Text = string.Format("{0:N2}", distotal);
            //    //  totqty.Text = tottqty.ToString();
            //    //  totmeter.Text = string.Format("{0:N2}", mettt);
            //    //   txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //    // txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //    // txtRate_TextChanged(sender, e);
            //    // txtQty_TextChanged(sender, e);

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Item.Grand total is Empty!!!.Thank You');", true);
            //    return;
            //}

        }

        protected void txtLchange(object sender, EventArgs e)
        {
            //if (txtgrandtotal.Text != "")
            //{
            //    double grandtotal = 0;
            //    double tax = 0;
            //    double distotal = 0;
            //    int tottqty = 0;
            //    double mettt = 0;
            //    double r = 0;

            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //        TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //        TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //        TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //        TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //        TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //        if (txt.SelectedItem.Text == "Select Product Code" || txtttk.Text == "" || txttk.Text == "")
            //        {

            //        }
            //        else
            //        {
            //            double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //            double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtdiscount.Text) / 100;

            //            double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //            double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //            double total = tx + DiscountAmount;


            //            //  txtkttt.Text = string.Format("{0:N2}", total);

            //            if (txt.SelectedIndex == -1 || txt.SelectedIndex == 0)
            //            {
            //            }
            //            else
            //            {
            //                //DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue), sTableName);
            //                //double mett = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["meter1"]);
            //                //double totm = Convert.ToDouble(txtttk.Text) * mett;
            //                //mettt = mettt + totm;
            //            }

            //            grandtotal = grandtotal + total;
            //            tax = tax + tx;
            //            distotal = distotal + Discount;
            //            tottqty = tottqty + Convert.ToInt32(txtttk.Text);

            //        }
            //    }
            //    double dFreight = 0;
            //    double dLU = 0;
            //    double sumLUFreight = 0;
            //    if (txtFreight.Text.Trim() != "")
            //    {
            //        dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //    }
            //    if (txtLU.Text.Trim() != "")
            //    {
            //        dLU = Convert.ToDouble(txtLU.Text.Trim());
            //    }
            //    sumLUFreight = dFreight + dLU;

            //    txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //    double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //    if (roundoff > 0.5)
            //    {
            //        r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            //    }
            //    else
            //    {
            //        r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //    }
            //    txtroundoff.Text = Convert.ToString(r);
            //    //  txtTaxamt.Text = string.Format("{0:N2}", tax);
            //    //  txtdiscount.Text = string.Format("{0:N2}", distotal);
            //    //  totqty.Text = tottqty.ToString();
            //    //  totmeter.Text = string.Format("{0:N2}", mettt);
            //    //   txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //    // txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //    // txtRate_TextChanged(sender, e);
            //    // txtQty_TextChanged(sender, e);

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Item.Grand total is Empty!!!.Thank You');", true);
            //    return;
            //}

        }

        protected void txttax_textchanged(object sender, EventArgs e)
        {
            //// ButtonAdd1_Click(sender, e);
            //double grandtotal = 0;
            //double tax = 0;
            //double distotal = 0;
            //int tottqty = 0;
            //double mettt = 0;
            //double r = 0;

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //    if (ProductCode.SelectedItem.Text == "Select Product Code" || txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
            //    {

            //    }
            //    else
            //    {

            //        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

            //        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //        double total = tx + DiscountAmount;

            //        txtkttt.Text = string.Format("{0:N2}", total);

            //        if (ProductCode.SelectedItem.Text == "Select Product Code")
            //        {

            //        }
            //        else
            //        {
            //            //DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue), sTableName);
            //            //double mett = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["meter1"]);
            //            //double totm = Convert.ToDouble(txtttk.Text) * mett;
            //            //mettt = mettt + totm;
            //        }


            //        grandtotal = grandtotal + total;
            //        tax = tax + tx;
            //        distotal = distotal + Discount;
            //        tottqty = tottqty + Convert.ToInt32(txtttk.Text);
            //        txttk.Focus();

            //    }

            //}
            //double dFreight = 0;
            //double dLU = 0;
            //double sumLUFreight = 0;
            //if (txtFreight.Text.Trim() != "")
            //{
            //    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //}
            //if (txtLU.Text.Trim() != "")
            //{
            //    dLU = Convert.ToDouble(txtLU.Text.Trim());
            //}
            //sumLUFreight = dFreight + dLU;
            //txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //txtTaxamt.Text = string.Format("{0:N2}", tax);
            ////  txtdiscount.Text = string.Format("{0:N2}", distotal);
            //totqty.Text = tottqty.ToString();
            //totmeter.Text = string.Format("{0:N2}", mettt);
            //txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //if (roundoff > 0.5)
            //{
            //    r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            //}
            //else
            //{
            //    r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //}
            //txtroundoff.Text = Convert.ToString(r);




            ////for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            ////{
            ////    int cnt = gvcustomerorder.Rows.Count;
            ////    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            ////    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            ////    if (vLoop >= 1)
            ////    {
            ////        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            ////        //    oldtxttk.Text = ".00";
            ////        oldtxttk.Focus();
            ////    }
            ////    int tot = cnt - vLoop;
            ////    if (tot == 1)
            ////    {
            ////        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            ////        if (oldtxttk.Text == "0.00")
            ////        {
            ////            oldtxttk.Text = ".00";
            ////            oldtxttk.Focus();
            ////        }
            ////        else
            ////        {
            ////            oldtxttk.Focus();
            ////        }
            ////    }
            ////    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            ////}
            ////Lable Sno:
            ////for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            ////{
            ////    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            ////}

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
            //    txtno.Text = Convert.ToString(i + 1);
            //    // ProductCode.Focus();
            //}
            //granddiscount(sender, e);

        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            //ButtonAdd1_Click(sender, e);
            //double grandtotal = 0;
            //double tax = 0;
            //double distotal = 0;
            //int tottqty = 0;
            //double mettt = 0;
            //double r = 0;

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //    if (ProductCode.SelectedItem.Text == "Select Product Code" || txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
            //    {

            //    }
            //    else
            //    {

            //        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

            //        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //        double total = tx + DiscountAmount;

            //        txtkttt.Text = string.Format("{0:N2}", total);

            //        if (ProductCode.SelectedItem.Text == "Select Product Code")
            //        {

            //        }
            //        else
            //        {
            //            //DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue), sTableName);
            //            //double mett = Convert.ToDouble(dsCategory.Tables[0].Rows[0]["meter1"]);
            //            //double totm = Convert.ToDouble(txtttk.Text) * mett;
            //            //mettt = mettt + totm;
            //        }


            //        grandtotal = grandtotal + total;
            //        tax = tax + tx;
            //        distotal = distotal + Discount;
            //        tottqty = tottqty + Convert.ToInt32(txtttk.Text);
            //        txttk.Focus();

            //    }

            //}
            //double dFreight = 0;
            //double dLU = 0;
            //double sumLUFreight = 0;
            //if (txtFreight.Text.Trim() != "")
            //{
            //    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //}
            //if (txtLU.Text.Trim() != "")
            //{
            //    dLU = Convert.ToDouble(txtLU.Text.Trim());
            //}
            //sumLUFreight = dFreight + dLU;
            //txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //txtTaxamt.Text = string.Format("{0:N2}", tax);
            ////  txtdiscount.Text = string.Format("{0:N2}", distotal);
            //totqty.Text = tottqty.ToString();
            //totmeter.Text = string.Format("{0:N2}", mettt);
            //txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //if (roundoff > 0.5)
            //{
            //    r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            //}
            //else
            //{
            //    r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //}
            //txtroundoff.Text = Convert.ToString(r);




            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    int cnt = gvcustomerorder.Rows.Count;
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    if (vLoop >= 1)
            //    {
            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            //        //    oldtxttk.Text = ".00";
            //        oldtxttk.Focus();
            //    }
            //    int tot = cnt - vLoop;
            //    if (tot == 1)
            //    {
            //        TextBox oldtxttk = (TextBox)gvcustomerorder.Rows[vLoop - 1].FindControl("txtRate");
            //        if (oldtxttk.Text == "0.00")
            //        {
            //            oldtxttk.Text = ".00";
            //            oldtxttk.Focus();
            //        }
            //        else
            //        {
            //            oldtxttk.Focus();
            //        }
            //    }
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");

            //}
            ////Lable Sno:
            ////for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            ////{
            ////    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            ////}

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
            //    txtno.Text = Convert.ToString(i + 1);
            //}
            //granddiscount(sender, e);

        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            //double grandtotal = 0;
            //double tax = 0;
            //double distotal = 0;
            //double r = 0;

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");
            //    if (ProductCode.SelectedItem.Text == "Select Product Code" || txt.SelectedItem.Text == "Select Product" || txtttk.Text == "")
            //    {

            //    }
            //    else
            //    {
            //        if ((txttk.Text == "") || (Convert.ToString(txttk.Text) == ".00"))
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter the Rate')", true);
            //            txttk.Focus();
            //            return;
            //        }
            //        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

            //        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //        double total = tx + DiscountAmount;

            //        txtkttt.Text = string.Format("{0:N2}", total);

            //        grandtotal = grandtotal + total;
            //        tax = tax + tx;
            //        distotal = distotal + Discount;
            //    }
            //}
            //double dFreight = 0;
            //double dLU = 0;
            //double sumLUFreight = 0;
            //if (txtFreight.Text.Trim() != "")
            //{
            //    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //}
            //if (txtLU.Text.Trim() != "")
            //{
            //    dLU = Convert.ToDouble(txtLU.Text.Trim());
            //}
            //sumLUFreight = dFreight + dLU;
            //txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //txtTaxamt.Text = string.Format("{0:N2}", tax);
            ////  txtdiscount.Text = string.Format("{0:N2}", distotal);
            //txtgrandtotal.Text = Convert.ToString(Convert.ToDouble(txtgrandtotal.Text) - Convert.ToDouble(txtdiscount.Text));
            //double roundoff = Convert.ToDouble(txtgrandtotal.Text) - Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //if (roundoff > 0.5)
            //{
            //    r = Math.Round(Convert.ToDouble(txtgrandtotal.Text), MidpointRounding.AwayFromZero);
            //}
            //else
            //{
            //    r = Math.Floor(Convert.ToDouble(txtgrandtotal.Text));
            //}
            //txtroundoff.Text = Convert.ToString(r);

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    // txtno.Focus();
            //}

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
            //    txtno.Text = Convert.ToString(i + 1);
            //}

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    txtno.Focus();
            //}
            ////for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            ////{
            ////    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            ////}
            //granddiscount(sender, e);
        }

        protected void txtmeter_textchanged(object sender, EventArgs e)
        {
            double gndmeter = 0;
            //double tax = 0;
            //double distotal = 0;
            //double r = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                gndmeter = gndmeter + Convert.ToDouble(txtmeter.Text);

                txtrate.Focus();


            }
            txttotmet.Text = gndmeter.ToString();
            //  txtmeter_textchanged(sender, e);
          //  txtrrattee_textchanged(sender, e);
        }

        protected void txtrrattee_textchanged(object sender, EventArgs e)
        {
            double gndmeter = 0;
            //double tax = 0;
            //double distotal = 0;
            //double r = 0;

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");

                gndmeter = gndmeter + (Convert.ToDouble(txtmeter.Text) * Convert.ToDouble(txtrate.Text));

                txtrate.Focus();


            }
            txttoal.Text = gndmeter.ToString();
            //  txtmeter_textchanged(sender, e);
        }

        protected void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            //double grandtotal = 0;
            //double tax = 0;
            //double distotal = 0;

            //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //{
            //    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");
            //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
            //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //    if (ProductCode.SelectedItem.Text == "Select Product Code" || txt.SelectedItem.Text == "Select Product" || txtttk.Text == "" || txttk.Text == "")
            //    {

            //    }
            //    else
            //    {
            //        double iNetAmount = ((Convert.ToDouble(txtttk.Text)) * (Convert.ToDouble(txttk.Text)));

            //        double Discount = Convert.ToDouble(iNetAmount) * Convert.ToDouble(txtktttt.Text) / 100;

            //        double DiscountAmount = Convert.ToDouble(iNetAmount) - Discount;
            //        double tx = Convert.ToDouble(DiscountAmount) * Convert.ToDouble(txtkt.Text) / 100;
            //        double total = tx + DiscountAmount;

            //        txtkttt.Text = string.Format("{0:N2}", total);

            //        grandtotal = grandtotal + total;
            //        tax = tax + tx;
            //        distotal = distotal + Discount;
            //    }
            //}
            //double dFreight = 0;
            //double dLU = 0;
            //double sumLUFreight = 0;
            //if (txtFreight.Text.Trim() != "")
            //{
            //    dFreight = Convert.ToDouble(txtFreight.Text.Trim());
            //}
            //if (txtLU.Text.Trim() != "")
            //{
            //    dLU = Convert.ToDouble(txtLU.Text.Trim());
            //}
            //sumLUFreight = dFreight + dLU;

            //txtgrandtotal.Text = string.Format("{0:N2}", (grandtotal + sumLUFreight));
            //txtTaxamt.Text = string.Format("{0:N2}", tax);
            // txtdiscount.Text = string.Format("{0:N2}", distotal);
        }

        protected void drpCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpCategory");
            //        DropDownList txtt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //        if (txt.SelectedIndex != 0)
            //        {
            //            TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
            //            if (txtno.Text == "")
            //            {
            //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter OrderNo.')", true);
            //                txtno.Focus();
            //                return;
            //            }
            //        }
            //        DropDownList ddl = (DropDownList)sender;
            //        GridViewRow row = (GridViewRow)ddl.NamingContainer;

            //        DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");

            //        DropDownList Def = (DropDownList)row.FindControl("drpItem");

            //        DropDownList procode = (DropDownList)row.FindControl("ProductCode");
            //        TextBox qty = (TextBox)row.FindControl("txtQty");

            //        if (drpCategory.SelectedItem.Text != "Select Category")
            //        {

            //            DataSet dsCategory1 = objBs.selectcategorydecription(Convert.ToInt32(drpCategory.SelectedValue), sTableName);
            //            if (dsCategory1.Tables[0].Rows.Count > 0)
            //            {
            //                Def.Items.Clear();
            //                Def.DataSource = dsCategory1.Tables[0];
            //                Def.DataTextField = "serial_NO";
            //                Def.DataValueField = "categoryuserid";
            //                Def.DataBind();
            //                //   Def.Items.Insert(0, "Select Product");

            //            }
            //            else
            //            {
            //                Def.Items.Clear();
            //                Def.Items.Insert(0, "Select Product");
            //            }
            //        }
            //        else
            //        {
            //        }

            //        if (drpCategory.SelectedItem.Text != "Select Category")
            //        {

            //            DataSet dsCategory1 = objBs.selectcategorydecription(Convert.ToInt32(drpCategory.SelectedValue), sTableName);
            //            if (dsCategory1.Tables[0].Rows.Count > 0)
            //            {
            //                procode.Items.Clear();
            //                procode.DataSource = dsCategory1.Tables[0];
            //                procode.DataTextField = "Definition";
            //                procode.DataValueField = "categoryuserid";
            //                procode.DataBind();
            //                //procode.Items.Insert(0, "Select Product Code");

            //            }
            //            else
            //            {
            //                procode.Items.Clear();
            //                procode.Items.Insert(0, "Select Product Code");
            //            }
            //        }
            //        else
            //        {
            //        }


            //        //if (txt.SelectedIndex != 0)
            //        //{

            //        //    if ((txtt.SelectedValue == "0") || (txtt.SelectedValue == "") || (txtt.SelectedValue == "Select Product"))
            //        //    {
            //        //        DataSet dsCategory1 = objBs.selectcategorydecription(Convert.ToInt32(txt.SelectedValue));
            //        //        if (dsCategory1.Tables[0].Rows.Count > 0)
            //        //        {
            //        //            txtt.Items.Clear();
            //        //            txtt.DataSource = dsCategory1.Tables[0];
            //        //            txtt.DataTextField = "Definition";
            //        //            txtt.DataValueField = "categoryuserid";
            //        //            txtt.DataBind();
            //        //            txtt.Items.Insert(0, "Select Product");

            //        //        }
            //        //        else
            //        //        {
            //        //            txtt.Items.Clear();
            //        //            txtt.Items.Insert(0, "Select Product");
            //        //        }
            //        //    }
            //        //}
            //        qty.Focus();
            //    }
            //}

        }

        protected void orderno(object sender, EventArgs e)
        {
            //int number = 0;
            //int iq = 1;
            //int ii = 1;
            //string itemc = string.Empty;
            //string itemcd = string.Empty;
            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        TextBox txtno1 = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");

            //        itemc = txtno1.Text;
            //        // number = Convert.ToInt32(txtno.Text);
            //        if ((itemc == null) || (itemc == ""))
            //        {

            //        }
            //        else
            //        {
            //            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
            //            {
            //                TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtno");

            //                if (ii == iq)
            //                {
            //                }
            //                else
            //                {
            //                    if (itemc == txtno.Text)
            //                    {
            //                        itemcd = txtno.Text;
            //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
            //                        return;

            //                    }
            //                }
            //                ii = ii + 1;
            //            }
            //        }
            //        iq = iq + 1;
            //        ii = 1;

            //        if (txtno1.Text == "")
            //        {
            //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please enter OrderNo.')", true);
            //            txtno1.Focus();
            //            return;
            //        }

            //    }
            //}
            //ButtonAdd1_Click(sender, e);
        }

        protected void drpItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int iq = 1;
            //int ii = 1;
            //string itemc = string.Empty;
            //string itemcd = string.Empty;
            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList txti = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
            //        TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
            //        TextBox txtqty = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtQty");
            //        TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //        TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");


            //        itemc = txti.Text;


            //        if ((itemc == null) || (itemc == ""))
            //        {
            //        }
            //        else
            //        {
            //            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
            //            {
            //                DropDownList txt1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpItem");
            //                if (txt1.Text == "")
            //                {
            //                }
            //                else
            //                {

            //                    if (ii == iq)
            //                    {
            //                    }
            //                    else
            //                    {
            //                        if (itemc == txt1.Text)
            //                        {
            //                            //itemcd = txti.SelectedItem.Text;
            //                            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
            //                            //txt1.Focus();
            //                            //return;

            //                        }
            //                    }
            //                    ii = ii + 1;
            //                }
            //            }
            //        }
            //        iq = iq + 1;
            //        ii = 1;

            //        //DataSet dsStock = new DataSet();

            //        //if ((txtktt.Text == "") && (txtkt.Text == "") && (txttk.Text == "") || (txtkttt.Text == ""))
            //        //{
            //        //    dsStock = objBs.GetStockDetails(Convert.ToInt32(txt.SelectedValue));

            //        //    if (dsStock.Tables[0].Rows.Count > 0)
            //        //    {
            //        //        txttk.Text = dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString();
            //        //        txtktt.Text = dsStock.Tables[0].Rows[0]["Available_Qty"].ToString();

            //        //        DataSet dsCategory = objBs.GetTax(Convert.ToInt32(txt.SelectedValue));
            //        //        txtkt.Text = dsCategory.Tables[0].Rows[0]["Tax"].ToString();

            //        //        txtkttt.Text = "0";
            //        //    }
            //        //}
            //    }
            //}


            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");


            //DropDownList Defitem = (DropDownList)row.FindControl("drpItem");

            //DropDownList procode = (DropDownList)row.FindControl("ProductCode");

            //if (procode.SelectedItem.Text != "Select Product Code")
            //{

            //    DataSet dsCategory1 = objBs.selectProduct(Convert.ToInt32(Defitem.SelectedValue), sTableName);
            //    if (dsCategory1.Tables[0].Rows.Count > 0)
            //    {
            //        drpCategory.Items.Clear();
            //        drpCategory.DataSource = dsCategory1.Tables[0];
            //        drpCategory.DataTextField = "categoryname";
            //        drpCategory.DataValueField = "categoryid";
            //        drpCategory.DataBind();
            //        //drpCategory.Items.Insert(0, "Select Category");

            //    }
            //    else
            //    {
            //        drpCategory.Items.Clear();
            //        drpCategory.Items.Insert(0, "Select Category");
            //    }
            //}
            //else
            //{
            //}

            //if (procode.SelectedItem.Text != "Select Product")
            //{

            //    DataSet dsCategory1 = objBs.selectProduct(Convert.ToInt32(Defitem.SelectedValue), sTableName);
            //    if (dsCategory1.Tables[0].Rows.Count > 0)
            //    {
            //        procode.SelectedValue = dsCategory1.Tables[0].Rows[0]["CategoryUserID"].ToString();

            //        DataSet dst = new DataSet();
            //        dst = objBs.selectcategoryalldecriptionbranch(sTableName);
            //        procode.Items.Clear();
            //        procode.DataSource = dst.Tables[0];
            //        procode.DataTextField = "Definition";
            //        procode.DataValueField = "categoryuserid";
            //        procode.DataBind();
            //    }
            //    else
            //    {
            //        procode.Items.Clear();
            //        procode.Items.Insert(0, "Select Product Code");
            //    }
            //}
            //else
            //{
            //}

            //TextBox txtRate = (TextBox)row.FindControl("txtRate");
            //TextBox txt = (TextBox)row.FindControl("txtDiscount");
            //TextBox txtTax = (TextBox)row.FindControl("txtTax");
            //DropDownList Def = (DropDownList)row.FindControl("drpItem");
            //DropDownList cate = (DropDownList)row.FindControl("drpCategory");
            //TextBox txtQty = (TextBox)row.FindControl("txtStock");
            //TextBox qty = (TextBox)row.FindControl("txtQty");
            //TextBox refno = (TextBox)row.FindControl("txtrefno");
            //DataSet dsStock = new DataSet();


            //dsStock = objBs.GetStockDetails(Convert.ToInt32(Def.SelectedValue), "tblStock_" + sTableName);

            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    DataSet dsCategory = objBs.GetTax(Convert.ToInt32(Def.SelectedValue), sTableName);

            //    var Itx = Convert.ToDecimal(dsCategory.Tables[0].Rows[0]["Tax"].ToString());
            //    txtTax.Text = Itx.ToString();

            //    decimal ratte = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["UnitPrice"]);
            //    txtRate.Text = Decimal.Round(ratte, 2).ToString("f2");

            //    //Decimal Irate = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["PurchaseRate"].ToString());
            //    //txtRate.Text = Decimal.Round(Irate, 2).ToString("f2");

            //    //if (ddlcustomerID.SelectedValue != "Select Customer")
            //    //{
            //    //    DataSet dsStockd = objBs.getsprice1(ddlcustomerID.SelectedValue, Def.SelectedValue, sTableName);
            //    //    if (dsStockd.Tables[0].Rows.Count > 0)
            //    //    {
            //    //        decimal ratte = Convert.ToDecimal(dsStockd.Tables[0].Rows[0]["Price"]);
            //    //        txtRate.Text = Decimal.Round(ratte, 2).ToString("f2");
            //    //    }
            //    //    else
            //    //    {
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    txtRate.Text = "0.00";
            //    //}

            //    decimal sQty = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
            //    txtQty.Text = sQty.ToString("f2");
            //    cate.SelectedValue = dsCategory.Tables[0].Rows[0]["categoryid"].ToString();

            //    txt.Text = "0";

            //    string value = Def.SelectedValue;
            //    DataSet ds = objBs.itemhistorypopup(sTableName, value);
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        txtitemhis.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
            //    }
            //    else
            //    {
            //        txtitemhis.Text = "";
            //    }


            //    string cust = ddlcustomerID.SelectedValue;
            //    if (cust == "Select Customer")
            //    {
            //    }
            //    else
            //    {
            //        DataSet ds1 = objBs.custhistorypopup(sTableName, value, cust);
            //        if (ds1.Tables[0].Rows.Count > 0)
            //        {
            //            txtcusthis.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
            //        }
            //        else
            //        {
            //            txtcusthis.Text = "";
            //        }
            //    }
            //    refno.Focus();
            //    // txtTamt5.Text = dsCategory.Tables[0].Rows[0]["Meter1"].ToString();
            //}


            ////for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            ////{
            ////    gvcustomerorder.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
            ////}


            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
            //    txtno.Text = Convert.ToString(i + 1);
            //}
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

                    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                    {
                        TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                        txtno.Text = Convert.ToString(i + 1);
                    }

                    txtmeter_textchanged(sender, e);
                    txtrrattee_textchanged(sender, e);
                }
                else if (dt.Rows.Count == 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable1"] = dt;
                    gvcustomerorder.DataSource = dt;
                    gvcustomerorder.DataBind();

                    SetPreviousData();

                    txtmeter_textchanged(sender, e);
                    txtrrattee_textchanged(sender, e);
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
                        TextBox txtrempiece = (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtrempiece");
                        TextBox txttno =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtno");
                        CheckBox cancel = (CheckBox)gvcustomerorder.Rows[rowIndex].Cells[0].FindControl("chkid");
                        TextBox txtdesign =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtdesno");

                        TextBox txtitem =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtItem");

                        TextBox txtcolor =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtcolor");

                      //  DropDownList drpcolor =
                      //(DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpcolor");

                        TextBox txtpiece =
                      (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtpiece");

                        TextBox txtmeter =
                       (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtmeter");

                        DropDownList drpwid =
                       (DropDownList)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("drpwid");

                        TextBox txtrate =
                          (TextBox)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("txtRate");

                        FileUpload fileupload =
                         (FileUpload)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("idupload");

                        Button imgbtn = (Button)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("btnUpload");

                        Image imgpre = (Image)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgurl");


                        Label lblimg =
                         (Label)gvcustomerorder.Rows[rowIndex].Cells[4].FindControl("imgpreview");

                        TextBox txtcancelmeter =
                   (TextBox)gvcustomerorder.Rows[rowIndex].Cells[3].FindControl("txtcancelmeter");

                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["Orderno"] = i + 1;
                        dtCurrentTable.Rows[i - 1]["Design"] = txtdesign.Text;
                        dtCurrentTable.Rows[i - 1]["Item"] = txtitem.Text;
                        dtCurrentTable.Rows[i - 1]["Piece"] = txtpiece.Text;
                        dtCurrentTable.Rows[i - 1]["meter"] = txtmeter.Text;
                        dtCurrentTable.Rows[i - 1]["Rate"] = txtrate.Text;
                        dtCurrentTable.Rows[i - 1]["ImageLabel"] = imgpre.ImageUrl;
                        dtCurrentTable.Rows[i - 1]["ImageLabel"] = lblimg.Text;
                        dtCurrentTable.Rows[i - 1]["Width"] = drpwid.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["Color"] = drpcolor.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Color"] = txtcolor.Text;

                        dtCurrentTable.Rows[i - 1]["rempiece"] = txtrempiece.Text;

                        dtCurrentTable.Rows[i - 1]["cancel"] = cancel.Checked;
                        dtCurrentTable.Rows[i - 1]["CancelMeter"] = txtcancelmeter.Text;
                        //dtCurrentTable.Rows[i - 1]["Orderno"] = txttno.Text;

                        //dtCurrentTable.Rows[i - 1]["Discount"] = txtDiscount.Text;

                        //dtCurrentTable.Rows[i - 1]["Tax"] = txtTax.Text;

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

        protected void gvcustomerorder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataSet dsCategory = new DataSet();

            //DataSet dscat = new DataSet();

            //string OrderNo = Request.QueryString.Get("OrderNo");
            ////if (OrderNo != "")
            ////{
            ////    /// dsCategory = objBs.GetCAT_OrderForm();
            ////}
            ////else
            //dsCategory = objBs.selectcategorybrandcat(sTableName);

            //dscat = objBs.selectcatuser();



            ////else
            ////    dsCategory = objBs.selectcategorymaster_Dealer("tblStock_" + sTableName);


            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    DropDownList ddlCategory1 = (DropDownList)(e.Row.FindControl("ddlCategory1") as DropDownList);
            //    ddlCategory1.Focus();
            //    ddlCategory1.Enabled = true;
            //    ddlCategory1.DataSource = dsCategory.Tables[0];
            //    ddlCategory1.DataTextField = "productname";
            //    ddlCategory1.DataValueField = "CategoryUserID";
            //    ddlCategory1.DataBind();
            //    ddlCategory1.Items.Insert(0, "Select");

            //    DropDownList ddlDef1 = (DropDownList)(e.Row.FindControl("ddlDef1") as DropDownList);
            //    ddlDef1.Focus();
            //    ddlDef1.Enabled = true;
            //    ddlDef1.DataSource = dscat.Tables[0];
            //    ddlDef1.DataTextField = "Definition";
            //    ddlDef1.DataValueField = "categoryuserid";
            //    ddlDef1.DataBind();
            //    ddlDef1.Items.Insert(0, "Select Product");

            //    //DataSet dsCategory1 = objBs.selectcategorydecription(Convert.ToInt32(ddlCategory1.SelectedItem.Text));
            //    //if (dsCategory1.Tables[0].Rows.Count > 0)
            //    //{
            //    //    //DropDownList Def1 = (DropDownList)row.FindControl("ddlDef1");
            //    //    ////Label lblCatID = (Label)row.FindControl("catid");
            //    //    ////lblCatID.Text = dsCategory.Tables[0].Rows[0]["CategoryID"].ToString();

            //    //    ddlDef1.DataSource = dsCategory.Tables[0];
            //    //    ddlDef1.DataTextField = "Definition";
            //    //    ddlDef1.DataValueField = "categoryuserid";
            //    //    ddlDef1.DataBind();
            //    //    ddlDef1.Items.Insert(0, "Select Product");
            //    //    ddlDef1.Focus();
            //    //}

            //    //DataSet dDef = objBs.selectcategorydecription(Convert.ToInt32(ddlCategory1.SelectedValue));
            //    //DropDownList Def = (DropDownList)e.Row.FindControl("ddlDef1");

            //    //Def.DataSource = dDef.Tables[0];
            //    //Def.DataTextField = "Definition";
            //    //Def.DataValueField = "categoryuserid";
            //    //Def.DataBind();
            //    //#region Databind
            //    //string billno = Convert.ToString(Request.QueryString["iSalesID"]);

            //    //if (billno != null)
            //    //{



            //    //    DataSet dBilling = objBs.GetSalesnew("tblSales_" + sTableName, billno);
            //    //    if (dBilling.Tables[0].Rows.Count > 0)
            //    //    {



            //    //        //txtcustomername.Text = dBilling.Tables[0].Rows[0]["CustomerName"].ToString();
            //    //       // txtmobileno.Text = dBilling.Tables[0].Rows[0]["PhoneNo"].ToString();
            //    //        txtSubTotal.Text = dBilling.Tables[0].Rows[0]["Total"].ToString();
            //    //       // txtAgainstAmount.Text = dBilling.Tables[0].Rows[0]["Advance"].ToString();
            //    //        ddlbook.Text = dBilling.Tables[0].Rows[0]["Book"].ToString();
            //    //       // txttotal.Text = dBilling.Tables[0].Rows[0]["Balance"].ToString();
            //    //        int iCount = dBilling.Tables[0].Rows.Count;

            //    //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //    //        DataRow drCurrentRow = null;
            //    //        DataSet dBilling1 = objBs.GettransnewSales("tblTransSales_" + sTableName, billno);
            //    //        for (int i = 0; i < iCount; i++)
            //    //        {

            //    //            TextBox txtRate = (TextBox)e.Row.FindControl("txtRate");
            //    //            TextBox txtQty = (TextBox)e.Row.FindControl("txtQty");
            //    //            TextBox txtAmt = (TextBox)e.Row.FindControl("txtAmount");
            //    //            DropDownList ddlCat = (DropDownList)e.Row.FindControl("ddlCategory");
            //    //           // DropDownList ddlDef = (DropDownList)e.Row.FindControl("ddlDef");

            //    //            ddlCat.SelectedValue = dBilling1.Tables[0].Rows[i]["SubCategoryID"].ToString();


            //    //          //  ddlDef.SelectedValue = dBilling.Tables[0].Rows[i]["SubCategoryID"].ToString();


            //    //            txtQty.Text = dBilling1.Tables[0].Rows[i]["Qty"].ToString();


            //    //            txtRate.Text = dBilling1.Tables[0].Rows[i]["Rate"].ToString();


            //    //            txtAmt.Text = dBilling1.Tables[0].Rows[i]["Amount"].ToString();


            //    //            if (dtCurrentTable.Rows.Count > 0)
            //    //            {
            //    //                for (int j = 1; j <= dtCurrentTable.Rows.Count; j++)
            //    //                {

            //    //                    drCurrentRow = dtCurrentTable.NewRow();
            //    //                    drCurrentRow["sno"] = j + 1;

            //    //                    dtCurrentTable.Rows[j - 1]["SubCategoryID"] = ddlCategory1.Text;
            //    //                   // dtCurrentTable.Rows[j - 1]["Item"] = ddlDef.Text;
            //    //                    dtCurrentTable.Rows[j - 1]["Qty"] = txtQty.Text;
            //    //                    dtCurrentTable.Rows[j - 1]["Rate"] = txtRate.Text;
            //    //                    dtCurrentTable.Rows[j - 1]["Amount"] = txtAmt.Text;


            //    //                }
            //    //                dtCurrentTable.Rows.Add(drCurrentRow);
            //    //                ViewState["CurrentTable"] = dtCurrentTable;

            //    //                gvcustomerorder.DataSource = dtCurrentTable;
            //    //                gvcustomerorder.DataBind();
            //    //            }

            //    //        }
            //    //    }

            //    //}

            //}
        }

        protected void ddlCategort_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //DropDownList ddlCategory1 = (DropDownList)row.FindControl("ddlCategory1");
            //DropDownList ddDef1 = (DropDownList)row.FindControl("ddlDef1");
            //DataSet dsCategory = objBs.selectcategorydecription(Convert.ToInt32(ddlCategory1.SelectedValue));
            //if (dsCategory.Tables[0].Rows.Count > 0)
            //{
            //    DropDownList Def1 = (DropDownList)row.FindControl("ddlDef1");
            //    //Label lblCatID = (Label)row.FindControl("catid");
            //    //lblCatID.Text = dsCategory.Tables[0].Rows[0]["CategoryID"].ToString();
            //    Def1.DataSource = dsCategory.Tables[0];
            //    Def1.DataTextField = "Definition";
            //    Def1.DataValueField = "categoryuserid";
            //    Def1.DataBind();
            //    Def1.Items.Insert(0, "Select Product");
            //    Def1.Focus();
            //    Def1.ClearSelection();
            //}
            //else
            //{

            //    ddDef1.ClearSelection();
            //}
            //ddlCategory1.Focus();
            //ddlCategory1.Enabled = true;

            //    ddlDef_SelectedIndexChanged1(sender, e);
            // ButtonAdd1_Click(sender, e);
        }

        protected void gvcustomerorder_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private int UpdateStockAvailable(int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;
            ////if (sTableName == "admin")
            ////{

            //DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //}
            //int iRemQty = iAQty - iQty;
            //iSuccess = objBs.updateSalesStocknew(iRemQty, iSubCat, "tblStock_" + sTableName);

            ////}
            ////else
            ////{
            ////    DataSet dsStock = objBs.GetStockDetails_Dealer(iSubCat, "tblStock_" + sTableName);
            ////    if (dsStock.Tables[0].Rows.Count > 0)
            ////    {
            ////        iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            ////    }
            ////    int iRemQty = iAQty - iQty;
            ////    iSuccess = objBs.updateSalesStock_dealer(iRemQty, iCat, iSubCat,"tblStock_"+sTableName);
            ////}
            return iSuccess;
        }

        private int InsertStockAvailable(int iCat, int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;
            ////if (sTableName == "admin")
            ////{

            //string iQrySalesID = Request.QueryString.Get("iSalesID");
            //if (iQrySalesID != null)
            //{
            //    DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
            //    if (dsStock.Tables[0].Rows.Count > 0)
            //    {
            //        iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //    }
            //    int iRemQty = iAQty + iQty;
            //    iSuccess = objBs.updateSalesStock(iRemQty, iCat, iSubCat, "tblStock_" + sTableName);
            //}

            ////}
            ////else
            ////{
            ////    DataSet dsStock = objBs.GetStockDetails_Dealer(iSubCat, "tblStock_" + sTableName);
            ////    if (dsStock.Tables[0].Rows.Count > 0)
            ////    {
            ////        iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            ////    }
            ////    int iRemQty = iAQty - iQty;
            ////    iSuccess = objBs.updateSalesStock_dealer(iRemQty, iCat, iSubCat,"tblStock_"+sTableName);
            ////}
            return iSuccess;
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderGrid.aspx");
        }

        protected void Gridview1_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
        {
            // GridViewRow row = gvcustomerorder.SelectedRow;


            //if (e.CommandName == "Select")
            //{

            //    GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;

            //    DropDownList yourTextbox = row.FindControl("drpitem") as DropDownList;
            //    var yourValue = yourTextbox.Text;
            //    if (yourValue == "Select Product")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Product.')", true);
            //        return;

            //    }
            //    string value = yourTextbox.SelectedValue;
            //    DataSet ds = objBs.itemhistorypopup(sTableName, value);
            //    if (ds != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            GridView1.DataSource = ds;
            //            GridView1.DataBind();
            //        }
            //        else
            //        {
            //            GridView1.DataSource = null;
            //            GridView1.DataBind();
            //        }
            //        mpe.Show();
            //    }
            //}
            //else if (e.CommandName == "Select1")
            //{
            //    GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            //    DropDownList yourTextbox = row.FindControl("drpitem") as DropDownList;

            //    var yourValue = yourTextbox.Text;

            //    if (yourValue == "Select Product")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Product.')", true);
            //        return;

            //    }
            //    if (ddlcustomerID.SelectedValue == "Select Customer")
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Customer Name')", true);
            //        return;

            //    }

            //    string value1 = yourTextbox.SelectedValue;

            //    string cust = ddlcustomerID.SelectedValue;
            //    DataSet ds = objBs.custhistorypopup(sTableName, value1, cust);
            //    if (ds != null)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            GridView1.DataSource = ds;
            //            GridView1.DataBind();
            //        }
            //        else
            //        {
            //            GridView1.DataSource = null;
            //            GridView1.DataBind();
            //        }
            //        mpe.Show();
            //    }
            //}
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            int iq = 1;
            int ii = 1;
            int brand = 0;
            string itemc = string.Empty;
            string itemcd = string.Empty;

            //DataSet dss = new DataSet();
            //if (ddlbook.SelectedItem.Value == "1")
            //{
            //    ddlvouchertype.SelectedValue = "2";

            //}
            //else
            //{
            //    ddlvouchertype.SelectedValue = "1";

            //}
            //string iSalesIDnew = Request.QueryString.Get("iSalesID");
            if (btnadd.Text == "Save")
            {
                if (drpsupplier.SelectedValue == "Select Supplier")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Supplier Name');", true);
                    return;
                }

                //if (drpchecked.SelectedValue == "Select Employee Name")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Employee Name');", true);
                //    return;
                //}
                //if (drpwidth.SelectedValue == "Select Width")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Width');", true);
                //    return;
                //}


                for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                {
                    TextBox txtno = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtno");
                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtdesno");
                    TextBox txtItem = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtItem");
                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtpiece");
                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtmeter");
                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                    Image imgurl = (Image)gvcustomerorder.Rows[vLoop].FindControl("imgurl");
                    Label txtkttt = (Label)gvcustomerorder.Rows[vLoop].FindControl("imgpreview");

                    DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpwid");
                    //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpColor");
                    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcolor");

                    CheckBox cancel = (CheckBox)gvcustomerorder.Rows[vLoop].FindControl("chkid");
                    TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcancelmeter");

                    int col = vLoop + 1;

                    if (drpwid.SelectedValue == "Select Width")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please select Width in " + col + " in this row');", true);
                        //  txt1.Focus();
                        return;
                    }


                    txtno.Focus();

                    itemc = txtdesign.Text;
                    //itemcd = drpcolor.SelectedValue;
                    itemcd = txtcolor.Text;

                    if ((itemc == null) || (itemc == ""))
                    {
                    }
                    else
                    {
                        for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
                        {
                            //  DropDownList txt1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("ProductCode");
                            TextBox txtdesign1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtdesno");
                            TextBox txtcolor1 = (TextBox)gvcustomerorder.Rows[vLoop1].FindControl("txtcolor");
                            //DropDownList drpcolor1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("drpColor");
                            if (txtdesign1.Text == "")
                            {
                            }
                            else
                            {

                                if (ii == iq)
                                {
                                }
                                else
                                {
                                    if (itemc == txtdesign1.Text && itemcd == txtcolor1.Text)
                                    {
                                        itemc = txtdesign.Text;
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemc + "  already exists in the Grid.');", true);
                                        //  txt1.Focus();
                                        return;

                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                    }
                    iq = iq + 1;
                    ii = 1;

                }

                DateTime regdate = DateTime.ParseExact(txtregdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime invdate = DateTime.ParseExact(txtinvdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (ddlBrand.SelectedValue == "Select Brand")
                {
                    brand = 0;
                }
                else
                {
                    brand = Convert.ToInt32(ddlBrand.SelectedValue);
                }


                int iStatus23 = objBs.insertorder(txtinvno.Text, drpsupplier.SelectedValue, regdate, drpagent.SelectedValue,txtsupplierorderno.Text,txttotmet.Text,drpcompany.SelectedValue,empid);

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {

                    //Label txtno = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");
                    TextBox txtrempiece = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtrempiece");

                    //string orderno = gvcustomerorder.Rows[i].Cells[0].Text;
                    TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesno");


                    TextBox txtItem = (TextBox)gvcustomerorder.Rows[i].FindControl("txtItem");

                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[i].FindControl("txtpiece");


                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtmeter");


                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                    Label imgpath = (Label)gvcustomerorder.Rows[i].FindControl("imgpreview");

                    DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpwid");

                    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcolor");
                    //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpcolor");


                    CheckBox cancel = (CheckBox)gvcustomerorder.Rows[i].FindControl("chkid");

                    TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcancelmeter");


                    //int iStatus2 = objBs.insertTransOrder(txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, cancel.Checked, txtItem.Text);


                    int iStatus2 = objBs.Insert_TransOrder(txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, cancel.Checked, txtItem.Text, txtcancelmeter.Text);

                    // iStockSuccess = UpdateStockAvailable(Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty));


                }
            }
            else
            {
                DateTime regdate = DateTime.ParseExact(txtregdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime invdate = DateTime.ParseExact(txtinvdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                if (ddlBrand.SelectedValue == "Select Brand")
                {
                    brand = 0;
                }
                else
                {
                    brand = Convert.ToInt32(ddlBrand.SelectedValue);
                }

                string iCusID = Request.QueryString.Get("iid");

                DataSet dsdd = objBs.gettransordernoforid(Convert.ToInt32(iCusID));

                int iStatus123 = objBs.deletetransorder(Convert.ToInt32(iCusID));

                int iStatus23 = objBs.updateorder(txtinvno.Text, drpsupplier.SelectedValue, regdate, drpagent.SelectedValue, Convert.ToInt32(iCusID),txtsupplierorderno.Text,txttotmet.Text,drpcompany.SelectedValue,empid);

                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                {

                    //Label txtno = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");
                    TextBox txtrempiece = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtrempiece");

                    //string orderno = gvcustomerorder.Rows[i].Cells[0].Text;
                    TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                    TextBox txtdesign = (TextBox)gvcustomerorder.Rows[i].FindControl("txtdesno");


                    TextBox txtItem = (TextBox)gvcustomerorder.Rows[i].FindControl("txtItem");

                    TextBox txtpiece = (TextBox)gvcustomerorder.Rows[i].FindControl("txtpiece");


                    TextBox txtmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtmeter");


                    TextBox txtrate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");

                    Label imgpath = (Label)gvcustomerorder.Rows[i].FindControl("imgpreview");

                    DropDownList drpwid = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpwid");
                    TextBox txtcolor = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcolor");
                    //DropDownList drpcolor = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpcolor");


                    CheckBox cancel = (CheckBox)gvcustomerorder.Rows[i].FindControl("chkid");

                    TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[i].FindControl("txtcancelmeter");

                    //int iStatus2 = objBs.UpdateTransOrder(txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, cancel.Checked, txtrempiece.Text, txtItem.Text, iCusID);


                    string canmeter = "";
                    string remqty = "";
                    foreach (DataRow dr in dsdd.Tables[0].Rows)
                    {
                        if (dr["ItemName"].ToString() == txtItem.Text && dr["DesignNo"].ToString() == txtdesign.Text && dr["Color"].ToString() == txtcolor.Text)
                        {
                            decimal canmtr = Convert.ToDecimal(dr["CancelMeter"].ToString());
                            decimal totcanmeter = canmtr + Convert.ToDecimal(txtcancelmeter.Text);
                            canmeter = totcanmeter.ToString("N");
                            decimal remqty1 = canmtr+Convert.ToDecimal(dr["retmeter"].ToString());
                            remqty = remqty1.ToString("N");
                            break;
                        }
                        else
                        {
                            canmeter = txtcancelmeter.Text;
                            remqty = txtmeter.Text;
                        }
                    }

                    int iStatus2 = objBs.Update_TransOrder(txtinvno.Text, orderno.Text, txtdesign.Text, txtcolor.Text, txtpiece.Text, txtmeter.Text, txtrate.Text, imgpath.Text, drpwid.SelectedValue, cancel.Checked, remqty, txtItem.Text, iCusID, canmeter);

                    // iStockSuccess = UpdateStockAvailable(Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty));


                }
            }
                //{

                //    dss = objBs.checkfortempSalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text, txtbillno.Text, "");
                //}
                //else
                //{
                //    dss = objBs.checkfortempSalesBillno("tblSales_" + sTableName, Convert.ToInt32(ddlvouchertype.SelectedValue), btnadd.Text, txtbillno.Text, iSalesIDnew);
                //}
                //if (dss.Tables[0].Rows.Count > 0)
                //{
                //    txtbillno.Focus();
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Billno Number Already Exists.');", true);
                //    return;
                //}
                //else
                //{
                //    //txtvoudate.Focus();
                //}

                //string custid = string.Empty;
                //if (chknewcust.Checked == false)
                //{
                //    if (ddlcustomerID.SelectedValue == "Select Customer")
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Please Select Customer Name')", true);

                //        return;

                //    }
                //}
                //else
                //{
                //    DataSet ds = new DataSet();
                //    ds = objBs.expensivename(txtCustname.Text, sTableName);
                //    if ((ds.Tables[0].Rows.Count) > 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('This Customer Name is all ready exist!');", true);
                //        txtCustname.Focus();
                //        return;
                //        // lblerror.Text = " This Group Name is all ready exist";


                //    }

                //    if (txtCustname.Text == "")
                //    {
                //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert(' Please Enter Customer Name')", true);

                //        return;

                //    }
                //}
                //if (ddlProvince.SelectedValue == "Select Province type")
                //{
                //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Select Province type')", true);

                //    return;
                //}

                //for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
                //{
                //    DropDownList txttt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpCategory");
                //    DropDownList txtd = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
                //    DropDownList txt = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("drpItem");
                //    TextBox txtttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtqty");

                //    TextBox txtref = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrefno");
                //    TextBox txtcer = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtCerno");

                //    TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
                //    TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");
                //    TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtAmount");
                //    TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
                //    TextBox txtktttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

                //    int col = vLoop + 1;
                //    if (txt.Text == "")
                //    {
                //    }
                //    else
                //    {
                //        if (txtd.SelectedItem.Text == "Select Product Code")
                //        {
                //            if (gvcustomerorder.Rows.Count == 1)
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Atleast one product.Thank you!!! ')", true);
                //                return;
                //            }
                //        }


                //        else
                //        {
                //            if (btnadd.Text == "Save")
                //            {
                //                if (txtref.Text != "")
                //                {
                //                    DataSet getrefno = objBs.getallrefnoforsales(sTableName, txtref.Text);
                //                    if (getrefno.Tables[0].Rows.Count > 0)
                //                    {
                //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This reference number already Exists in " + col + " ')", true);

                //                        return;
                //                    }
                //                    else
                //                    {

                //                    }
                //                }
                //                else
                //                {

                //                }
                //            }
                //            else
                //            {
                //                string isalesid = Request.QueryString.Get("iSalesID");

                //                DataSet getrefno = objBs.getallrefnoforsales(sTableName, txtref.Text, isalesid);
                //                if (txtref.Text != "")
                //                {
                //                    if (getrefno.Tables[0].Rows.Count > 0)
                //                    {
                //                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This reference number already Exists in " + col + " ')", true);

                //                        return;
                //                    }
                //                    else
                //                    {

                //                    }
                //                }

                //            }



                //            if (txtd.SelectedValue == "0" || txtd.SelectedValue == "Select Product Code")
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Product Code in row " + col + " ')", true);
                //                txtd.Focus();
                //                return;
                //            }
                //            if (txt.SelectedValue == "0" || txt.SelectedValue == "Select Product")
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Select Product in row " + col + " ')", true);
                //                txt.Focus();
                //                return;
                //            }
                //            if ((txtttk.Text == "") || (Convert.ToInt32(txtttk.Text) == 0))
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter qty in row " + col + " ')", true);
                //                txtttk.Focus();
                //                return;
                //            }
                //            //if ((txtref.Text == ""))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter ref.no in row " + col + " ')", true);
                //            //    txtttk.Focus();
                //            //    return;
                //            //}
                //            //if ((txtcer.Text == ""))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Certificate No in row " + col + " ')", true);
                //            //    txtttk.Focus();
                //            //    return;
                //            //}



                //            double qtyy = Convert.ToDouble(Convert.ToDouble(txtktt.Text));
                //            //double qtyy = Convert.ToDouble(Convert.ToDouble(txtktt.Text) + Convert.ToDouble(txtttk.Text));

                //            //if (Convert.ToDouble(txtttk.Text) > Convert.ToDouble(qtyy))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty greater than the Current stock in row " + col + " ')", true);
                //            //    txtttk.Focus();
                //            //    return;
                //            //}


                //            //double qtyy = Convert.ToDouble(Convert.ToDouble(txtktt.Text) + Convert.ToDouble(txtttk.Text));

                //            //if ( Convert.ToDouble(qtyy) > Convert.ToDouble(txtttk.Text))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty greater than the Current stock in row " + col + " ')", true);
                //            //    txtttk.Focus();
                //            //    return;
                //            //}

                //            //if (Convert.ToInt32(txtttk.Text) > Convert.ToInt32(txtktt.Text))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Qty greater than the Current stock in row " + col + " ')", true);
                //            //    txtttk.Focus();
                //            //    return;
                //            //}
                //            //if ((txtktttt.Text == ""))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Discount% in row " + col + " ')", true);
                //            //    txtktttt.Focus();
                //            //    return;
                //            //}

                //            //if ((Convert.ToDouble(txtktttt.Text) > 100))
                //            //{
                //            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter Discount% within 100 in Row " + col + " ')", true);
                //            //    txtktttt.Focus();
                //            //    return;
                //            //}

                //            if ((txttk.Text == "") || (Convert.ToString(txttk.Text) == ".00"))
                //            {
                //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Enter the Rate " + col + " ')", true);
                //                txttk.Focus();
                //                return;
                //            }
                //        }
                //    }
                //}

                ////  return;

                //DateTime billldate = DateTime.ParseExact(txtvoudate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime lrrdate = DateTime.ParseExact(txtlrdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime orddate = DateTime.ParseExact(txtorderdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime dueedate = DateTime.ParseExact(txtduedate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime vouchdate = DateTime.ParseExact(txtvoudate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ////return;
                //DataSet dsss = objBs.customerid(1, 2);
                //if (dsss.Tables[0].Rows.Count > 0)
                //{
                //    // int iBillNo = ds.Tables[0].Rows[0]["billno"].ToString();
                //    if (dsss.Tables[0].Rows[0]["CustomerId"].ToString() == "")
                //        custid = "1";
                //    else
                //        custid = dsss.Tables[0].Rows[0]["CustomerId"].ToString();
                //}
                //if (btnadd.Text == "Save")
                //{
                //    // ddlPayMode_SelectedIndexChanged(sender, e);
                //    // txtbillcheck(sender,e);

                //    int iStatus2 = 0, iStatus3 = 0, iStatus4 = 0, iStatus5 = 0, iStockSuccess = 0;
                //    int iCustid = 0;
                //    int Id = 0;
                //    // return;

                //    if (chknewcust.Checked == true)
                //    {
                //        //iCustid = objBs.InsertCustBill(Convert.ToInt32(lblUserID.Text), txtCustname.Text, txtmobileno.Text, "0", txtaddress.Text, txtcity.Text, txtpincode.Text, Convert.ToInt32(bblbillto.SelectedValue), bblbillto.SelectedItem.Text);
                //        iCustid = objBs.InsertCustBillnew(txtCustname.Text, "0", "0", "31", txtadd.Text, "509", "0", "0", 1, "Yes", Convert.ToDouble("0.00"), Convert.ToDouble("0.00"), "Credit Note", Convert.ToInt32(1), Convert.ToInt32(custid), "0", "tblAuditMaster_" + sTableName, lblUser.Text, "Customer", "Credit Note", "0", custid, Convert.ToInt32(ddlRepname.SelectedValue), "0", txtTransport.Text, sTableName, Convert.ToInt32(ddlPriceList.SelectedValue), Convert.ToInt32(ddlProvince.SelectedValue));
                //    }
                //    else
                //    {
                //        iCustid = Convert.ToInt32(ddlcustomerID.SelectedValue);
                //    }

                //    DataSet dstt = new DataSet();

                //    if (Convert.ToInt32(ddlPayMode.SelectedValue) == 6)
                //    {
                //        DataTable dttt = new DataTable();

                //        DataColumn dc = new DataColumn("RefNo");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("TransDate");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("DebitorID");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("CreditorID");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("Amount");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("Narration");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("Type");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("Cheque");
                //        dttt.Columns.Add(dc);

                //        dc = new DataColumn("PayMode");
                //        dttt.Columns.Add(dc);

                //        dstt.Tables.Add(dttt);

                //        if (ddlAgainst.SelectedValue != "0" && txtchequedd.Text != "" && txtAgainstAmount.Text != "0")
                //        {
                //            DataRow drd = dstt.Tables[0].NewRow();
                //            drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //            drd["TransDate"] = txtdate1.Text;
                //            drd["DebitorID"] = ddlAgainst.SelectedValue;
                //            drd["CreditorID"] = iCustid;
                //            drd["Amount"] = txtAgainstAmount.Text;
                //            drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //            drd["Cheque"] = txtchequedd.Text;
                //            drd["PayMode"] = 2;
                //            drd["Type"] = "Receipt";
                //            dstt.Tables[0].Rows.Add(drd);
                //        }
                //        if (ddlAgainst1.SelectedValue != "0" && txtchequedd1.Text != "" && txtAgainstAmount1.Text != "0")
                //        {
                //            DataRow drd = dstt.Tables[0].NewRow();
                //            drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //            drd["TransDate"] = txtdate1.Text;
                //            drd["DebitorID"] = ddlAgainst1.SelectedValue;
                //            drd["CreditorID"] = iCustid;
                //            drd["Amount"] = txtAgainstAmount1.Text;
                //            drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //            drd["Cheque"] = txtchequedd1.Text;
                //            drd["PayMode"] = 2;
                //            drd["Type"] = "Receipt";
                //            dstt.Tables[0].Rows.Add(drd);
                //        }
                //        if (!string.IsNullOrEmpty(txtAgainstAmount2.Text) && txtAgainstAmount2.Text != "0")
                //        {
                //            DataRow drd = dstt.Tables[0].NewRow();
                //            drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //            drd["TransDate"] = txtdate1.Text;

                //            DataSet dst = objBs.getCashledgerId("Cash A/C _" + sTableName);
                //            int DID = Convert.ToInt32(dst.Tables[0].Rows[0]["LedgerID"]);

                //            drd["DebitorID"] = DID;
                //            drd["CreditorID"] = iCustid;
                //            drd["Amount"] = txtAgainstAmount2.Text;
                //            drd["Cheque"] = 0;
                //            drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //            drd["PayMode"] = 1;
                //            drd["Type"] = "Receipt";
                //            dstt.Tables[0].Rows.Add(drd);
                //        }
                //    }

                //    if ((Convert.ToInt32(ddlPayMode.SelectedValue) == 1) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 0) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 3) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 5) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 6))
                //    {
                //        Id = 0;
                //    }
                //    else
                //    {
                //        //Id = Convert.ToInt32(ddlBank.SelectedValue);
                //    }

                //    int iStat = objBs.insertsalesnew(sTableName, "tblDayBook_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(lblUserID.Text), txtbillno.Text, billldate, Convert.ToInt32(iCustid), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtTaxamt.Text), Convert.ToDouble(txtdiscount.Text), Convert.ToInt32("0"), Convert.ToInt32(1), Convert.ToDouble("0"), Convert.ToInt32(ddlPayMode.SelectedValue), txtNarration.Text, Id, txtCheque.Text, Convert.ToInt32(ddlbook.SelectedValue), txtvouno.Text, vouchdate, Convert.ToInt32(ddlvouchertype.SelectedValue), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToInt32(ddlCash.SelectedValue), txtlrno.Text, txtTransport.Text, txtdestination.Text, txtorderno.Text, Convert.ToInt32(ddlRepname.SelectedValue), txtpackingslip.Text, Convert.ToString(txtDisc.Text), lrrdate, dueedate, orddate, txtnopackage.Text, txtFreight.Text, txtLU.Text, txtroundoff.Text, txtadd.Text, txtdiscountamount.Text);

                //    //if (Convert.ToInt32(ddlPayMode.SelectedValue) == 6)
                //    //{
                //    //    int iStatus = objBs.InsertCustReceipt(sTableName, "tblReceipt_" + sTableName, "tblDaybook_" + sTableName, txtbillno.Text, Convert.ToInt32(iCustid), txtdate1.Text, dstt, "tblTransReceipt_" + sTableName, Convert.ToDouble(txtgrandtotal.Text));
                //    //}
                //    //int orderno = Convert.ToInt32(gvcustomerorder.Rows[0].Cells[0]);




                //    for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                //    {

                //        //Label txtno = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");
                //        //Label orderno = (Label)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                //        //string orderno = gvcustomerorder.Rows[i].Cells[0].Text;
                //        TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");

                //        DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
                //        DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[i].FindControl("ProductCode");

                //        int idef = Convert.ToInt32(ddldef.SelectedValue);
                //        if (ProductCode.SelectedItem.Text == "Select Product Code")
                //        {

                //        }
                //        else
                //        {
                //            DropDownList ddcategory = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpCategory");
                //            int icat = Convert.ToInt32(ddcategory.SelectedValue);
                //            ddcategory.Focus();
                //            ddcategory.Enabled = true;
                //            //   DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
                //            //int idef = Convert.ToInt32(ddldef.SelectedValue);

                //            TextBox Qty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtQty");
                //            double dQty = Convert.ToDouble(Qty.Text);

                //            // TextBox orderno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
                //            int dorno = Convert.ToInt32(orderno.Text);

                //            TextBox refno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrefno");
                //            string drefno = refno.Text;

                //            TextBox cerno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtCerno");
                //            string dcerno = cerno.Text;

                //            TextBox Dis = (TextBox)gvcustomerorder.Rows[i].FindControl("txtDiscount");
                //            double dDis = Convert.ToDouble(Dis.Text);
                //            TextBox Rate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                //            double DRate = Convert.ToDouble(Rate.Text);
                //            TextBox Tax = (TextBox)gvcustomerorder.Rows[i].FindControl("txtTax");
                //            double DTax = Convert.ToDouble(Tax.Text);

                //            TextBox Amount = (TextBox)gvcustomerorder.Rows[i].FindControl("txtAmount");
                //            double dAmount = Convert.ToDouble(Amount.Text);


                //            iStatus2 = objBs.insertTransSales("tblTransSales_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty), Convert.ToDouble(DRate), Convert.ToDouble(dDis), Convert.ToDouble(dAmount), Convert.ToInt32(ddcategory.SelectedValue), Convert.ToDouble(DTax), txtdiscount.Text, txtgrandtotal.Text, txtTaxamt.Text, totqty.Text, Convert.ToDouble(totmeter.Text), dorno, drefno, dcerno);

                //            iStockSuccess = UpdateStockAvailable(Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty));
                //        }

                //    }
                //    //  ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Bill Saved successfully.');", true);
                //    // return;
                //    System.Threading.Thread.Sleep(3000);
                //    Response.Redirect("cashsales.aspx");
                //}

                //else if (btnadd.Text == "Update")
                //{

                //    int iStatus2 = 0, iStatus3 = 0, iStatus4 = 0, iStatus5 = 0, iStockSuccess = 0;


                //    string iSalesID = Request.QueryString.Get("iSalesID");
                //    iDealer = Request.QueryString.Get("iDealer");

                //    //string Idealer = Request.QueryString.Get("iDealer");
                //    // string iSalesID = Request.QueryString.Get("iSalesID");


                //    if (iSalesID != null)
                //    {

                //        if (txtgrandtotal.Text != "")
                //        {
                //            int isalesid = Convert.ToInt32(txtbillno.Text);

                //            DataSet dsTransSales = objBs.GetTransSales("tblTransSales_" + sTableName, iSalesID);
                //            if (dsTransSales.Tables[0].Rows.Count > 0)
                //            {
                //                for (int i = 0; i < dsTransSales.Tables[0].Rows.Count; i++)
                //                {
                //                    int ddldef = Convert.ToInt32(dsTransSales.Tables[0].Rows[i]["SubCategoryID"]);
                //                    int ddcategory = Convert.ToInt32(dsTransSales.Tables[0].Rows[i]["CategoryId"]);
                //                    int Trid = Convert.ToInt32(dsTransSales.Tables[0].Rows[i]["SalesID"]);
                //                    string Amount = Convert.ToString(dsTransSales.Tables[0].Rows[i]["Amount"]);

                //                    if (Amount != "")
                //                    {
                //                        DataSet ds_stock = objBs.GetPurchaseStok(Convert.ToInt32(ddldef), "tblStock_" + sTableName);
                //                        int qty_stock = Convert.ToInt32(ds_stock.Tables[0].Rows[0]["Available_QTY"].ToString());

                //                        DataSet ds_stockTSR = objBs.Get_TranssalesStock("tblTransSales_" + sTableName, (Convert.ToInt32(ddldef)), iSalesID);
                //                        int qty_current = Convert.ToInt32(ds_stockTSR.Tables[0].Rows[0]["Quantity"].ToString());

                //                        int qty = qty_stock + qty_current;
                //                        int update = objBs.updateSalesStock(qty, Convert.ToInt32(ddcategory), Convert.ToInt32(ddldef), "tblStock_" + sTableName);

                //                    }


                //                }
                //            }

                //            int iTransDelete = objBs.deletesalseordervalues("tblTransSales_" + sTableName, iSalesID);
                //            //  int iDelete = objBs.DeleteSales("tblSales_" + sTableName, iSalesID, "tblDayBook_" + sTableName,"0", "0", "0", sTableName);


                //            try
                //            {
                //                int iCustid = 0;
                //                int Id = 0;

                //                iCustid = Convert.ToInt32(ddlcustomerID.SelectedValue);


                //                DataSet dstt = new DataSet();

                //                if (Convert.ToInt32(ddlPayMode.SelectedValue) == 6)
                //                {
                //                    DataTable dttt = new DataTable();

                //                    DataColumn dc = new DataColumn("RefNo");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("TransDate");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("DebitorID");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("CreditorID");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("Amount");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("Narration");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("Type");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("Cheque");
                //                    dttt.Columns.Add(dc);

                //                    dc = new DataColumn("PayMode");
                //                    dttt.Columns.Add(dc);

                //                    dstt.Tables.Add(dttt);

                //                    if (ddlAgainst.SelectedValue != "0" && txtchequedd.Text != "" && txtAgainstAmount.Text != "0")
                //                    {
                //                        DataRow drd = dstt.Tables[0].NewRow();
                //                        drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //                        drd["TransDate"] = txtdate1.Text;
                //                        drd["DebitorID"] = ddlAgainst.SelectedValue;
                //                        drd["CreditorID"] = iCustid;
                //                        drd["Amount"] = txtAgainstAmount.Text;
                //                        drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //                        drd["Cheque"] = txtchequedd.Text;
                //                        drd["PayMode"] = 2;
                //                        drd["Type"] = "Receipt";
                //                        dstt.Tables[0].Rows.Add(drd);
                //                    }
                //                    if (ddlAgainst1.SelectedValue != "0" && txtchequedd1.Text != "" && txtAgainstAmount1.Text != "0")
                //                    {
                //                        DataRow drd = dstt.Tables[0].NewRow();
                //                        drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //                        drd["TransDate"] = txtdate1.Text;
                //                        drd["DebitorID"] = ddlAgainst1.SelectedValue;
                //                        drd["CreditorID"] = iCustid;
                //                        drd["Amount"] = txtAgainstAmount1.Text;
                //                        drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //                        drd["Cheque"] = txtchequedd1.Text;
                //                        drd["PayMode"] = 2;
                //                        drd["Type"] = "Receipt";
                //                        dstt.Tables[0].Rows.Add(drd);
                //                    }
                //                    if (!string.IsNullOrEmpty(txtAgainstAmount2.Text) && txtAgainstAmount2.Text != "0")
                //                    {
                //                        DataRow drd = dstt.Tables[0].NewRow();
                //                        drd["RefNo"] = Convert.ToInt32(txtbillno.Text);
                //                        drd["TransDate"] = txtdate1.Text;

                //                        DataSet dst = objBs.getCashledgerId("Cash A/C _" + sTableName);
                //                        int DID = Convert.ToInt32(dst.Tables[0].Rows[0]["LedgerID"]);

                //                        drd["DebitorID"] = DID;
                //                        drd["CreditorID"] = iCustid;
                //                        drd["Amount"] = txtAgainstAmount2.Text;
                //                        drd["Cheque"] = 0;
                //                        drd["Narration"] = "For billno" + Convert.ToInt32(txtbillno.Text);
                //                        drd["PayMode"] = 1;
                //                        drd["Type"] = "Receipt";
                //                        dstt.Tables[0].Rows.Add(drd);
                //                    }
                //                }

                //                if ((Convert.ToInt32(ddlPayMode.SelectedValue) == 1) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 0) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 3) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 5) || (Convert.ToInt32(ddlPayMode.SelectedValue) == 6))
                //                {
                //                    Id = 0;
                //                }
                //                else
                //                {

                //                }

                //                int iStat = objBs.updatesales(sTableName, "tblDayBook_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(lblUserID.Text), txtbillno.Text, billldate, Convert.ToInt32(iCustid), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtgrandtotal.Text), Convert.ToDouble(txtTaxamt.Text), Convert.ToDouble(txtdiscount.Text), Convert.ToInt32("0"), Convert.ToInt32(1), Convert.ToDouble("0"), Convert.ToInt32(ddlPayMode.SelectedValue), txtNarration.Text, Id, txtCheque.Text, Convert.ToInt32(ddlbook.SelectedValue), txtvouno.Text, vouchdate, Convert.ToInt32(ddlvouchertype.SelectedValue), Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToInt32(ddlCash.SelectedValue), txtlrno.Text, txtTransport.Text, txtdestination.Text, txtorderno.Text, Convert.ToInt32(ddlRepname.SelectedValue), txtpackingslip.Text, Convert.ToString(txtDisc.Text), lrrdate, dueedate, orddate, txtnopackage.Text, iSalesID, txtFreight.Text, txtLU.Text, txtroundoff.Text, txtadd.Text, txtdiscountamount.Text);

                //                for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
                //                {
                //                    //Label orderno = (Label)gvcustomerorder.Rows[i].FindControl("txtno");
                //                    TextBox orderno = (TextBox)gvcustomerorder.Rows[i].Cells[0].FindControl("txtno");
                //                    //string orderno = gvcustomerorder.Rows[i].Cells[0].Text; 

                //                    DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
                //                    DropDownList ProductCode = (DropDownList)gvcustomerorder.Rows[i].FindControl("ProductCode");
                //                    // int idef = Convert.ToInt32(ddldef.SelectedValue);
                //                    if (ProductCode.SelectedItem.Text == "Select Product Code")
                //                    {

                //                    }
                //                    else
                //                    {
                //                        DropDownList ddcategory = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpCategory");
                //                        int icat = Convert.ToInt32(ddcategory.SelectedValue);
                //                        ddcategory.Focus();
                //                        ddcategory.Enabled = true;
                //                        //    DropDownList ddldef = (DropDownList)gvcustomerorder.Rows[i].FindControl("drpItem");
                //                        int idef = Convert.ToInt32(ddldef.SelectedValue);

                //                        TextBox Qty = (TextBox)gvcustomerorder.Rows[i].FindControl("txtQty");
                //                        double dQty = Convert.ToDouble(Qty.Text);

                //                        //Label orderno1 = (Label)gvcustomerorder.Rows[i].FindControl("txtno");
                //                        int dorno = Convert.ToInt32(orderno.Text);

                //                        TextBox refno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtrefno");
                //                        string drefno = refno.Text;

                //                        TextBox cerno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtCerno");
                //                        string dcerno = cerno.Text;

                //                        TextBox Dis = (TextBox)gvcustomerorder.Rows[i].FindControl("txtDiscount");
                //                        double dDis = Convert.ToDouble(Dis.Text);
                //                        TextBox Rate = (TextBox)gvcustomerorder.Rows[i].FindControl("txtRate");
                //                        double DRate = Convert.ToDouble(Rate.Text);
                //                        TextBox Tax = (TextBox)gvcustomerorder.Rows[i].FindControl("txtTax");
                //                        double DTax = Convert.ToDouble(Tax.Text);

                //                        TextBox Amount = (TextBox)gvcustomerorder.Rows[i].FindControl("txtAmount");
                //                        double dAmount = Convert.ToDouble(Amount.Text);


                //                        iStatus2 = objBs.updateTransSalesnew("tblTransSales_" + sTableName, "tblSales_" + sTableName, Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty), Convert.ToDouble(DRate), Convert.ToDouble(dDis), Convert.ToDouble(dAmount), Convert.ToInt32(ddcategory.SelectedValue), Convert.ToDouble(DTax), txtdiscount.Text, txtgrandtotal.Text, txtTaxamt.Text, totqty.Text, Convert.ToDouble(totmeter.Text), dorno, iSalesID, drefno, dcerno);

                //                        iStockSuccess = UpdateStockAvailable(Convert.ToInt32(ddldef.SelectedValue), Convert.ToInt32(dQty));
                //                    }

                //                }
                //                System.Threading.Thread.Sleep(3000);

                //                Response.Redirect("cashsales.aspx");
                //            }
                //            catch (Exception ex)
                //            {
                //                Response.Write(ex.ToString());
                //            }


                //        }

                //    }

                //}

            //}
            Response.Redirect("ORderGrid.aspx");
        }

        protected void ProductCode_SelectedIndexChanged(object sender, EventArgs e)
        {

            //int iq = 1;
            //int ii = 1;
            //string itemc = string.Empty;
            //string itemcd = string.Empty;
            //if (ViewState["CurrentTable1"] != null)
            //{
            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList txti = (DropDownList)gvcustomerorder.Rows[vLoop].FindControl("ProductCode");
            //        TextBox txttk = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtRate");
            //        TextBox txtkt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtTax");

            //        TextBox txtktt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtStock");
            //        TextBox txtkttt = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtDiscount");

            //        itemc = txti.Text;


            //        if ((itemc == null) || (itemc == ""))
            //        {
            //        }
            //        else
            //        {
            //            for (int vLoop1 = 0; vLoop1 < gvcustomerorder.Rows.Count; vLoop1++)
            //            {
            //                DropDownList txt1 = (DropDownList)gvcustomerorder.Rows[vLoop1].FindControl("ProductCode");
            //                if (txt1.Text == "")
            //                {
            //                }
            //                else
            //                {

            //                    if (ii == iq)
            //                    {
            //                    }
            //                    else
            //                    {
            //                        if (itemc == txt1.Text)
            //                        {
            //                            itemcd = txti.SelectedItem.Text;
            //                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + itemcd + "  already exists in the Grid.');", true);
            //                            txt1.Focus();
            //                            return;

            //                        }
            //                    }
            //                    ii = ii + 1;
            //                }
            //            }
            //        }
            //        iq = iq + 1;
            //        ii = 1;


            //    }
            //}


            //DropDownList ddl = (DropDownList)sender;
            //GridViewRow row = (GridViewRow)ddl.NamingContainer;
            //if (ViewState["CurrentTable1"] != null)
            //{

            //    DataTable dtCurrentTable1 = (DataTable)ViewState["CurrentTable1"];
            //    for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            //    {
            //        DropDownList drpCategory = (DropDownList)row.FindControl("drpCategory");

            //        DropDownList Def = (DropDownList)row.FindControl("drpItem");

            //        TextBox qty = (TextBox)row.FindControl("txtQty");

            //        DropDownList procode = (DropDownList)row.FindControl("ProductCode");

            //        if (procode.SelectedItem.Text != "Select Product Code")
            //        {

            //            DataSet dsCategory1 = objBs.selectProduct(Convert.ToInt32(procode.SelectedValue), sTableName);
            //            if (dsCategory1.Tables[0].Rows.Count > 0)
            //            {
            //                drpCategory.Items.Clear();
            //                drpCategory.DataSource = dsCategory1.Tables[0];
            //                drpCategory.DataTextField = "categoryname";
            //                drpCategory.DataValueField = "categoryid";
            //                drpCategory.DataBind();
            //                //drpCategory.Items.Insert(0, "Select Category");

            //            }
            //            else
            //            {
            //                drpCategory.Items.Clear();
            //                drpCategory.Items.Insert(0, "Select Category");
            //            }
            //        }
            //        else
            //        {
            //        }

            //        if (procode.SelectedItem.Text != "Select Product Code")
            //        {

            //            DataSet dsCategory1 = objBs.selectProduct(Convert.ToInt32(procode.SelectedValue), sTableName);
            //            if (dsCategory1.Tables[0].Rows.Count > 0)
            //            {
            //                DataSet dst = new DataSet();
            //                dst = objBs.selectcategoryalldecriptionbranch(sTableName);
            //                Def.Items.Clear();
            //                Def.DataSource = dst.Tables[0];
            //                Def.DataTextField = "serial_NO";
            //                Def.DataValueField = "categoryuserid";
            //                Def.DataBind();

            //                Def.SelectedValue = dsCategory1.Tables[0].Rows[0]["CategoryUserID"].ToString();

            //            }
            //            else
            //            {
            //                Def.Items.Clear();
            //                Def.Items.Insert(0, "Select Product");
            //            }
            //        }
            //        else
            //        {
            //        }

            //        qty.Focus();
            //    }
            //}


            //TextBox txtRate = (TextBox)row.FindControl("txtRate");
            //TextBox txt = (TextBox)row.FindControl("txtDiscount");
            //TextBox txtTax = (TextBox)row.FindControl("txtTax");
            //DropDownList Defitem = (DropDownList)row.FindControl("drpItem");
            //DropDownList cate = (DropDownList)row.FindControl("drpCategory");
            //DropDownList ProductCode = (DropDownList)row.FindControl("ProductCode");
            //TextBox txtQty = (TextBox)row.FindControl("txtStock");
            //DataSet dsStock = new DataSet();

            //if (ProductCode.SelectedItem.Text != "Select Product Code")
            //{
            //    dsStock = objBs.GetStockDetails(Convert.ToInt32(ProductCode.SelectedValue), "tblStock_" + sTableName);

            //    if (dsStock.Tables[0].Rows.Count > 0)
            //    {
            //        DataSet dsCategory = objBs.GetTax(Convert.ToInt32(ProductCode.SelectedValue), sTableName);

            //        var Itx = Convert.ToDecimal(dsCategory.Tables[0].Rows[0]["Tax"].ToString());
            //        txtTax.Text = Itx.ToString();

            //        decimal rattee = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Unitprice"]);
            //        txtRate.Text = Decimal.Round(rattee, 2).ToString("f2");


            //        decimal sQty = Convert.ToDecimal(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());
            //        txtQty.Text = sQty.ToString("f2");
            //        cate.SelectedValue = dsCategory.Tables[0].Rows[0]["categoryid"].ToString();

            //        txt.Text = "0";

            //        string value = ProductCode.SelectedValue;
            //        DataSet ds = objBs.itemhistorypopup(sTableName, value);
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            txtitemhis.Text = Convert.ToDouble(ds.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
            //        }
            //        else
            //        {
            //            txtitemhis.Text = "";
            //        }


            //        string cust = ddlcustomerID.SelectedValue;
            //        if (cust == "Select Customer")
            //        {
            //        }
            //        else
            //        {
            //            DataSet ds1 = objBs.custhistorypopup(sTableName, value, cust);
            //            if (ds1.Tables[0].Rows.Count > 0)
            //            {
            //                txtcusthis.Text = Convert.ToDouble(ds1.Tables[0].Rows[0]["unitprice"]).ToString("0.00");
            //            }
            //            else
            //            {
            //                txtcusthis.Text = "";
            //            }
            //        }

            //        // txtTamt5.Text = dsCategory.Tables[0].Rows[0]["Meter1"].ToString();
            //    }
            //}

            //for (int i = 0; i < gvcustomerorder.Rows.Count; i++)
            //{
            //    TextBox txtno = (TextBox)gvcustomerorder.Rows[i].FindControl("txtno");
            //    txtno.Text = Convert.ToString(i + 1);
            //}

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //string iSalesID = Request.QueryString.Get("iSalesID");
            //Response.Redirect("Print_Sales.aspx?iSalesID=" + iSalesID);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //string iSalesID = Request.QueryString.Get("iSalesID");
            //int iSucess = objBs.DeleteSales("tblSales_" + sTableName, iSalesID, "tblDayBook_" + sTableName, "tblAuditMaster_" + sTableName, lblUser.Text, btnDelete.Text, sTableName);

            //int isalesid = Convert.ToInt32(iSalesID);

            //DataSet dsTransSales = objBs.GetTransSales("tblTransSales_" + sTableName, iSalesID);
            //if (dsTransSales.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < dsTransSales.Tables[0].Rows.Count; i++)
            //    {
            //        string sddlCat = dsTransSales.Tables[0].Rows[i]["CategoryID"].ToString();
            //        string sddlDef = dsTransSales.Tables[0].Rows[i]["SubCategoryID"].ToString();
            //        string sQty = dsTransSales.Tables[0].Rows[i]["Quantity"].ToString();
            //        int iSuccs = UpdateEditStock(Convert.ToInt32(sddlCat), Convert.ToInt32(sddlDef), Convert.ToInt32(sQty));

            //    }
            //}

            //int iTransDelete = objBs.DeleteTransSales("tblTransSales_" + sTableName, iSalesID);

            //Response.Redirect("salesgrid.aspx");

        }

        private int UpdateEditStock(int iCat, int iSubCat, int iQty)
        {
            int iAQty = 0, iSuccess = 0;
            ////if (sTableName == "admin")
            ////{
            //DataSet dsStock = objBs.GetStockDetails(iSubCat, "tblStock_" + sTableName);
            //if (dsStock.Tables[0].Rows.Count > 0)
            //{
            //    iAQty = Convert.ToInt32(dsStock.Tables[0].Rows[0]["Available_Qty"].ToString());

            //}
            //int iInsQty = iAQty + iQty;
            //iSuccess = objBs.updateSalesStock(iInsQty, iCat, iSubCat, "tblStock_" + sTableName);

            return iSuccess;
        }


        protected void txtcancelmeter_TextChanged(object sender, EventArgs e)
        {

            for (int vLoop = 0; vLoop < gvcustomerorder.Rows.Count; vLoop++)
            {

                TextBox txtcancelmeter = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtcancelmeter");
                TextBox txtrempiece = (TextBox)gvcustomerorder.Rows[vLoop].FindControl("txtrempiece");

                int col = vLoop + 1;
                if (Convert.ToDouble(txtcancelmeter.Text) > Convert.ToDouble(txtrempiece.Text))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Cancel Meter is greater than Remaining Meter in row " + col + " ')", true);
                    txtcancelmeter.Focus();
                }


            }
        
        }

    }
}