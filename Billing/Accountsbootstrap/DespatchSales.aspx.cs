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
using System.IO;


namespace Billing.Accountsbootstrap
{
    public partial class DespatchSales : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string sTableName = "";
        string IsSuperAdmin = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["User"] != null)
                sTableName = Session["User"].ToString();
            else
                Response.Redirect("login.aspx");

            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            IsSuperAdmin = Session["IsSuperAdmin"].ToString();

            if (!IsPostBack)
            {
                DataSet ds = objBs.getSalesInvoiceNo();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlInvoiceno.DataSource = ds;
                    ddlInvoiceno.DataTextField = "FullInvoiceNo";
                    ddlInvoiceno.DataValueField = "BuyerOrderSalesId";
                    ddlInvoiceno.DataBind();
                    ddlInvoiceno.Items.Insert(0, "Select");
                }
                else
                {
                    ddlInvoiceno.Items.Insert(0, "Select");
                }
                if(Request.QueryString.Get("despatchid")!="")
                    {

                    DataSet ds1 = objBs.getdespatchSalesforgrid(Convert.ToInt32(Request.QueryString.Get("despatchid")));
                   
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        ddlInvoiceno.SelectedValue = ds1.Tables[0].Rows[0]["Buyerordersalesid"].ToString();
                       // ddlInvoiceno.SelectedItem.Text = ds1.Tables[0].Rows[0]["FullInvoiceNo"].ToString();
                        ddlInvoiceno.Enabled = false;

                        GVItem.DataSource = ds1;
                        GVItem.DataBind();
                    }
                   
                    for (int i = 0; i < GVItem.Rows.Count; i++)
                    {

                        Label lblbuyerordersalesid = (Label)GVItem.Rows[i].FindControl("lblbuyerordersalesid");

                        Label lblinvoiceno = (Label)GVItem.Rows[i].FindControl("lblinvoiceno");

                        Label lblinvoicedate = (Label)GVItem.Rows[i].FindControl("lblinvoicedate");
                        Label lblbuyername = (Label)GVItem.Rows[i].FindControl("lblbuyername");

                        Label lblqty = (Label)GVItem.Rows[i].FindControl("lblqty");
                        Image imgpreview = (Image)GVItem.Rows[i].FindControl("imgpreview");

                        TextBox txtLRno = (TextBox)GVItem.Rows[i].FindControl("txtLRno");
                        TextBox txtLRdate = (TextBox)GVItem.Rows[i].FindControl("txtLRdate");
                        TextBox txtTransport = (TextBox)GVItem.Rows[i].FindControl("txtTransport");
                        TextBox txtnoofpackage = (TextBox)GVItem.Rows[i].FindControl("txtnoofpackage");
                        Label lblFilePath = (Label)GVItem.Rows[i].FindControl("lblFilePath");
                        Label lblledgerid = (Label)GVItem.Rows[i].FindControl("lblledgerid");
                        lblinvoicedate.Text = Convert.ToDateTime(ds1.Tables[0].Rows[0]["InvoiceDate"]).ToString("dd/MM/yyyy");
                        txtLRno.Text = ds1.Tables[0].Rows[0]["LRNo"].ToString();
                        txtLRdate.Text =Convert.ToDateTime( ds1.Tables[0].Rows[0]["LRDate"]).ToString("dd/MM/yyyy");
                        txtTransport.Text = ds1.Tables[0].Rows[0]["Transport"].ToString();
                        txtnoofpackage.Text = ds1.Tables[0].Rows[0]["noofpackage"].ToString();
                        lblFilePath.Text = ds1.Tables[0].Rows[0]["imagepath"].ToString();
                        imgpreview.ImageUrl= ds1.Tables[0].Rows[0]["imagepath"].ToString();
                        btnSave.Text = "Update";
                    }

                }
               
            }
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            if (ddlInvoiceno.SelectedItem.Text == "Select" || ddlInvoiceno.SelectedItem.Text=="" )
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please Select Invoice No.Thank You!!!');", true);
                return;
            }
            else
            {
                DataSet ds = objBs.getSalesforgrid(Convert.ToInt32(ddlInvoiceno.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVItem.DataSource = ds;
                    GVItem.DataBind();
                }
            }
            }
        protected void ddlInvoiceno_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataSet ds = objBs.getSalesforgrid(Convert.ToInt32(ddlInvoiceno.SelectedValue));
            if (ds.Tables[0].Rows.Count > 0)
            {
                GVItem.DataSource = ds;
                GVItem.DataBind();
            }
        }
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("DespatchSalesGrid.aspx");
        }
            protected void Save_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GVItem.Rows.Count; i++)
            {
                Label lblbuyerordersalesid = (Label)GVItem.Rows[i].FindControl("lblbuyerordersalesid");

                Label lblinvoiceno = (Label)GVItem.Rows[i].FindControl("lblinvoiceno");

                Label lblinvoicedate = (Label)GVItem.Rows[i].FindControl("lblinvoicedate");
                Label lblbuyername = (Label)GVItem.Rows[i].FindControl("lblbuyername");

                Label lblqty = (Label)GVItem.Rows[i].FindControl("lblqty");

                TextBox txtLRno = (TextBox)GVItem.Rows[i].FindControl("txtLRno");
                TextBox txtLRdate = (TextBox)GVItem.Rows[i].FindControl("txtLRdate");
                TextBox txtTransport = (TextBox)GVItem.Rows[i].FindControl("txtTransport");
                TextBox txtnoofpackage = (TextBox)GVItem.Rows[i].FindControl("txtnoofpackage");
                Label lblFilePath = (Label)GVItem.Rows[i].FindControl("lblFilePath");
                Label lblledgerid = (Label)GVItem.Rows[i].FindControl("lblledgerid");
                if (btnSave.Text == "Save")
                {
                    int iStatus = objBs.InsertDespatch(lblbuyerordersalesid.Text, lblinvoiceno.Text, lblinvoicedate.Text, lblbuyername.Text, lblqty.Text, txtLRno.Text, txtLRdate.Text, txtTransport.Text, lblledgerid.Text, txtnoofpackage.Text, lblFilePath.Text);
                }
                if(btnSave.Text=="Update")
                {
                    int iStatus = objBs.UpdateDespatch(lblbuyerordersalesid.Text, lblinvoiceno.Text, lblinvoicedate.Text, lblbuyername.Text, lblqty.Text, txtLRno.Text, txtLRdate.Text, txtTransport.Text, lblledgerid.Text, txtnoofpackage.Text, lblFilePath.Text,Request.QueryString.Get("despatchid").ToString());
                }
            }
            Response.Redirect("DespatchsalesGrid.aspx");
        }
            protected void GVItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.ToLower() == "upload")
            {
                Button btn_Upload = e.CommandSource as Button;
                Response.Write(btn_Upload.Parent.Parent.GetType().ToString());

                System.Web.UI.WebControls.FileUpload fu = btn_Upload.FindControl("fluimg") as System.Web.UI.WebControls.FileUpload;
                Label lblFilePath = btn_Upload.FindControl("lblFilePath") as Label;
                Image imgpreview = btn_Upload.FindControl("imgpreview") as Image;

                if (fu.HasFile)
                {
                    bool upload = true;
                    string fleUpload = Path.GetExtension(fu.FileName.ToString());
                    //if (fleUpload.Trim().ToLower() == ".xls" | fleUpload.Trim().ToLower() == ".xlsx")
                    //{
                    fu.SaveAs(Server.MapPath("~/Files/" + fu.FileName.ToString()));
                    string uploadedFile = (Server.MapPath("~/Files/" + fu.FileName.ToString()));


                    imgpreview.ImageUrl = "~/Files/" + fu.FileName;
                    lblFilePath.Text = fu.FileName;
                    lblFilePath.Text = imgpreview.ImageUrl;


                    //}
                    //else
                    //{
                    //    upload = false;

                    //}
                    //if (upload)
                    //{

                    //}
                }
                else
                {

                }
            }
            else if (e.CommandName.ToLower() == "clear")
            {
                LinkButton linkbtn_Clear = e.CommandSource as LinkButton;

                Response.Write(linkbtn_Clear.Parent.Parent.GetType().ToString());

                Label lblFilePath = linkbtn_Clear.FindControl("lblFilePath") as Label;
                Image imgpreview = linkbtn_Clear.FindControl("imgpreview") as Image;

                lblFilePath.Text = "";
                imgpreview.ImageUrl = "";
            }
            else
            {
                return;
            }

            if (e.CommandName == "edit")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("BuyerOrderSales.aspx?BuyerOrderSalesId=" + e.CommandArgument.ToString());
                }
            }
          else  if (e.CommandName == "Print")
            {
                if (e.CommandArgument.ToString() != "")
                {
                    Response.Redirect("BuyerSalesOrderPrint.aspx?BuyerOrderSalesId=" + e.CommandArgument.ToString());
                }
            }
            else if (e.CommandName == "delete1")
            {
                if (IsSuperAdmin == "1")
                {
                    int iSucess = objBs.DeleteBuyerOrderSalesInv(Convert.ToInt32(e.CommandArgument.ToString()));
                    Response.Redirect("BuyerOrderSalesGrid.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Contact Admin.');", true);
                    return;
                }
            }
        }


       

        //protected void btnUpload_OnClick(object sender, EventArgs e)
        //{

        //        if (flutradeLicense.HasFile)
        //    {
        //        //string fileName = Path.Combine(@"E:\Project\Folders", FileUpload1.FileName);
        //        string fileName = Path.GetFileName(flutradeLicense.PostedFile.FileName);

        //        string extension = Path.GetExtension(fileName);
        //        string contentType = flutradeLicense.PostedFile.ContentType;
        //        HttpPostedFile file = flutradeLicense.PostedFile;
        //        byte[] document = new byte[file.ContentLength];
        //        file.InputStream.Read(document, 0, file.ContentLength);

        //        if (extension == ".jpg")
        //        {
        //        }
        //        else if (extension == ".JPG")
        //        {
        //        }
        //        else if (extension == ".jpeg")
        //        {
        //        }
        //        else if (extension == ".JPEG")
        //        {
        //        }
        //        else if (extension == ".png")
        //        {
        //        }
        //        else if (extension == ".PNG")
        //        {
        //        }
        //        else if (extension == ".pdf")
        //        {
        //        }
        //        else if (extension == ".PDF")
        //        {
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This is Invalid Extension File')", true);
        //            return;
        //        }

        //        flutradeLicense.PostedFile.SaveAs(Server.MapPath("Files/") + txtApplicantName.Text + " " + fileName);
        //        lblFile_Path.Text = "Files/" + txtApplicantName.Text + " " + flutradeLicense.PostedFile.FileName;
        //        // img_Photo.ImageUrl = "Files/" + txtApplicantName.Text + " " + flutradeLicense.PostedFile.FileName;

        //        //string fileName = Path.GetFileName(fp_Upload.PostedFile.FileName);
        //        //fp_Upload.PostedFile.SaveAs(Server.MapPath("Files/") + fileName);
        //        //lblFile_Path.Text = "http://www.blaackforestcakes.co.in/Files/" + fp_Upload.PostedFile.FileName;
        //        //lblFile_Path_img.Text = "Files/" + fp_Upload.PostedFile.FileName;
        //        //img_Photo.ImageUrl = "Files/" + fp_Upload.PostedFile.FileName;

        //    }
        //}

    }
}


