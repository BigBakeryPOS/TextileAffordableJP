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
using System.Net.NetworkInformation;
using System.Data.OleDb;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

namespace Billing.Accountsbootstrap
{
    public partial class BrandMaster : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"].ToString() != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();

            string name = Request.QueryString.Get("name");

            if (name == "Add New")
            {
                add.Visible = true;
                div1.Visible = false;
                hd1.InnerText = "Brand Master";
            }
            else if (name == "Bulk Addition")
            {
                add.Visible = false;
                div1.Visible = true;
                hd1.InnerText = "Bulk Brand Addition";
            }

            if (!IsPostBack)
            {
                divcode.Visible = false;
                DataSet dsFit = objBs.Fit();//tblFit
                if (dsFit.Tables[0].Rows.Count > 0)
                {
                    ddlFit.DataSource = dsFit.Tables[0];
                    ddlFit.DataTextField = "Fit";
                    ddlFit.DataValueField = "FitID";
                    ddlFit.DataBind();
                    ddlFit.Items.Insert(0, "Select Fit");
                }

                DataSet dsSizeno = objBs.getSizeNumber();
                if (dsSizeno != null)
                {
                    if (dsSizeno.Tables[0].Rows.Count > 0)
                    {
                        chkSize.DataSource = dsSizeno.Tables[0];
                        chkSize.DataTextField = "Size";
                        chkSize.DataValueField = "Sizeid";
                        chkSize.DataBind();
                    }
                }

                int iCusID = Convert.ToInt32(Request.QueryString.Get("iCusID"));

                if (Convert.ToString(iCusID) != "" || iCusID != null)
                {

                    DataSet ds1 = objBs.getselectBrand(iCusID);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        btnadd.Text = "Update";
                        txtBrandcode.Text = ds1.Tables[0].Rows[0]["Brandid"].ToString();
                        txtBrandname.Text = ds1.Tables[0].Rows[0]["BrandName"].ToString();
                        dllsizetype.SelectedValue = ds1.Tables[0].Rows[0]["BrandCode"].ToString();
                        ddlIsActive.SelectedValue = ds1.Tables[0].Rows[0]["IsActive"].ToString();
                        ddlFit.SelectedValue = ds1.Tables[0].Rows[0]["FitID"].ToString();
                        //chkSize.SelectedValue = ds1.Tables[0].Rows[0]["Size"].ToString();

                        //if (ds1 != null)
                        //{
                        //    if (ds1.Tables[0].Rows.Count > 0)
                        //    {
                        //        chkSize.DataSource = ds1.Tables[0];
                        //        chkSize.DataTextField = "Size";
                        //        chkSize.DataValueField = "Sizeid";
                        //        chkSize.DataBind();
                        //    }
                        //}

                        add.Visible = true;
                        div1.Visible = false;
                        hd1.InnerText = "Brand Master";


                        for (int i=0; i < chkSize.Items.Count; i++)
                        {
                            for (int j=0; j < ds1.Tables[0].Rows.Count; j++)
                            {
                                if (chkSize.Items[i].ToString() == ds1.Tables[0].Rows[j]["Size"].ToString())
                                {
                                    chkSize.Items[i].Selected = true;
                                }
                            }
                        }
                    }


                }
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            string Mode = Request.QueryString.Get("Mode");

            if (btnadd.Text == "Save")
            {
                DataSet dsCategory = objBs.brandsrchgrid(txtBrandname.Text,ddlFit.SelectedValue);
                if (dsCategory.Tables[0].Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Brand and fit has already Exists. please enter a new one')", true);
                    return;
                }
                else
                {
                    int iStatus = objBs.insertBrand(Convert.ToInt32(lblUserID.Text), txtBrandname.Text, Convert.ToInt32(ddlFit.SelectedValue),
                        ddlIsActive.SelectedValue, "tblAuditMaster_" + sTableName, lblUser.Text, dllsizetype.SelectedValue);
                    foreach (ListItem item in chkSize.Items)
                    {
                        if (item.Selected)
                        {
                            int iStatus1 = objBs.inserttransBrand(Convert.ToInt32(lblUserID.Text), Convert.ToInt32(item.Value),
                                "tblAuditMaster_" + sTableName, lblUser.Text, dllsizetype.SelectedValue);
                        }
                    }

                    Response.Redirect("../Accountsbootstrap/viewbrands.aspx");
                }

            }
            else
            {
                DataSet IsExistBrand = objBs.IsExistBrandPreCutting(Convert.ToInt32(txtBrandcode.Text));

                if (IsExistBrand.Tables[0].Rows.Count != 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Size was used in Pre-Cutting not able to update')", true);
                    return;
                }
                else
                {

                    int iStatus = objBs.updateBrand(Convert.ToInt32(txtBrandcode.Text), txtBrandname.Text, Convert.ToInt32(ddlFit.SelectedValue), ddlIsActive.SelectedValue, "tblAuditMaster_" + sTableName, lblUser.Text, dllsizetype.SelectedValue);
                    int iDel = objBs.DeleteTransBrand(Convert.ToInt32(txtBrandcode.Text));

                    foreach (ListItem item in chkSize.Items)
                    {
                        if (item.Selected)
                        {
                            int iStatus1 = objBs.insertupdatetransBrand(Convert.ToInt32(txtBrandcode.Text), Convert.ToInt32(lblUserID.Text), Convert.ToInt32(item.Value),
                                    "tblAuditMaster_" + sTableName, lblUser.Text, dllsizetype.SelectedValue);
                        }
                    }
                }
                Response.Redirect("../Accountsbootstrap/viewbrands.aspx");
            }

            System.Threading.Thread.Sleep(3000);
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewbrands.aspx");
        }

        protected void Exit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/viewbrands.aspx");
        }

        protected void btnFormat_Click(object sender, EventArgs e)
        {
            bindData();

        }

        public void bindData()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Brand"));

            DataRow dr_final12 = dt.NewRow();
            dr_final12["Brand"] = "";
            dt.Rows.Add(dr_final12);

            ExportToExcel(dt);
        }

        public void ExportToExcel(DataTable dt)
        {

            if (dt.Rows.Count > 0)
            {
                //string filename = "Sales Report.xls";
                string filename = "NewBrand _" + DateTime.Now.ToString() + ".xls";
                System.IO.StringWriter tw = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();
                dgGrid.DataSource = dt;
                dgGrid.DataBind();
                //dgGrid.HeaderStyle.ForeColor = System.Drawing.Color.Black;
                //dgGrid.HeaderStyle.BackColor = System.Drawing.Color.LightSkyBlue;
                //dgGrid.HeaderStyle.BorderColor = System.Drawing.Color.RoyalBlue;
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

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "";
                string specialCharacters = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=" + "\"";
                char[] specialCharactersArray = specialCharacters.ToCharArray();

                if (FileUpload1.HasFile)
                {
                    string datett = DateTime.Now.ToString();
                    string dtaa = Convert.ToDateTime(datett).ToString("dd-MM-yyyy-hh-mm-ss");
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName) + dtaa;
                    string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    string fileLocation = Server.MapPath("~/App_Data/" + fileName);
                    FileUpload1.SaveAs(fileLocation);
                    if (fileExtension == ".xls")
                    {
                        connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (fileExtension == ".xlsx")
                    {
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                        //OleDbConnection Conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\";");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Attach Correct Format file Extension.(.xls or .xlsx)');", true);
                        return;
                    }
                    OleDbConnection con = new OleDbConnection(connectionString);
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = con;
                    OleDbDataAdapter dAdapter = new OleDbDataAdapter(cmd);
                    DataTable dtExcelRecords = new DataTable();
                    con.Open();
                    DataTable dtExcelSheetName = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string getExcelSheetName = dtExcelSheetName.Rows[0]["Table_Name"].ToString();
                    cmd.CommandText = "SELECT * FROM [" + getExcelSheetName + "]";
                    dAdapter.SelectCommand = cmd;
                    dAdapter.Fill(dtExcelRecords);
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dtExcelRecords);

                    if (ds == null)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Uploading Excel is Empty');", true);
                        return;
                    }


                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((Convert.ToString(dr["Brand"]) == null) || (Convert.ToString(dr["Brand"]) == ""))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Brand Name is empty');", true);
                            return;
                        }
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        string brand = Convert.ToString(dr["Brand"]);

                        if (objBs.CheckIfbrand(brand))
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('These Brand " + brand + " has already Exists. please enter a new one')", true);
                            return;
                            // lblerror.Text = "These Category has already Exists. please enter a new one";

                        }

                    }


                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        if ((Convert.ToString(dr["Brand"]) != null) || (Convert.ToString(dr["Brand"]) != ""))
                        {
                            int index = Convert.ToString(dr["Brand"]).IndexOfAny(specialCharactersArray);
                            //index == -1 no special characters
                            if (index != -1)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Special characters not allowed in Brand');", true);
                                return;
                            }
                        }
                    }

                    int i = 1;
                    int ii = 1;
                    string brand1 = string.Empty;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        brand1 = Convert.ToString(dr["Brand"]);

                        if ((brand1 == null) || (brand1 == ""))
                        {
                        }
                        else
                        {
                            foreach (DataRow drd in ds.Tables[0].Rows)
                            {
                                if (ii == i)
                                {
                                }
                                else
                                {
                                    if (brand1 == Convert.ToString(drd["Brand"]))
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Brand with  - " + brand1 + " - already exists in the excel.');", true);
                                        return;
                                    }
                                }
                                ii = ii + 1;
                            }
                        }
                        i = i + 1;
                        ii = 1;
                    }

                    objBs.InsertBulkbrand(ds);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Brand Uploaded Successfully');", true);



                    con.Close();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please upload Xls file.It Cannot be empty.');", true);

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}