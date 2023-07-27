using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using BusinessLayer;
using CommonLayer;
using DataLayer;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Billing.Accountsbootstrap
{
    public partial class Employee_details : System.Web.UI.Page
    {

        BSClass objbs = new BSClass();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
            {
                ds = objbs.Select_department();

                ddldesignation.DataSource = ds;
                ddldesignation.DataValueField = "DesiginationId";
                ddldesignation.DataTextField = "DesiginationName";
                ddldesignation.DataBind();
                ddldesignation.Items.Insert(0, "Select Desigination");


                DataSet dsdept = objbs.Select_depart();

                ddldepartment.DataSource = dsdept;
                ddldepartment.DataValueField = "DeptId";
                ddldepartment.DataTextField = "DeptName";
                ddldepartment.DataBind();
                ddldepartment.Items.Insert(0, "Select Department");

                ds = objbs.Select_UnitFirst();
                ddlUnit.DataSource = ds;
                ddlUnit.DataValueField = "UnitID";
                ddlUnit.DataTextField = "UnitName";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, "All");

                string sEmpID = Request.QueryString.Get("Employee_Id");
                if (sEmpID != null)
                {
                    DataSet dsEmp = objbs.GetEmpDet(sEmpID);
                    if (dsEmp.Tables.Count > 0)
                    {

                        if (dsEmp.Tables[0].Rows.Count > 0)
                        {
                            btnsubmit.Text = "Update";
                            ds = objbs.Select_department();

                            ddldesignation.DataSource = ds;
                            ddldesignation.DataValueField = "DesiginationId";
                            ddldesignation.DataTextField = "DesiginationName";
                            ddldesignation.DataBind();
                            ddldesignation.Items.Insert(0, "Select Desigination");


                            DataSet dsdept1 = objbs.Select_depart();

                           ddldepartment.DataSource = dsdept1;
                           ddldepartment.DataValueField = "DeptId";
                           ddldepartment.DataTextField = "DeptName";
                           ddldepartment.DataBind();
                           ddldepartment.Items.Insert(0, "Select Department"); ;

                            ds = objbs.Select_UnitFirst();
                            ddlUnit.DataSource = ds;
                            ddlUnit.DataValueField = "UnitID";
                            ddlUnit.DataTextField = "UnitName";
                            ddlUnit.DataBind();
                            ddlUnit.Items.Insert(0, "All");

                            txtemployid.Text = dsEmp.Tables[0].Rows[0]["Employee_Id"].ToString();
                            txtemploycode.Text = dsEmp.Tables[0].Rows[0]["Emp_code"].ToString();
                            txtname.Text = dsEmp.Tables[0].Rows[0]["Name"].ToString();
                            txtdob.Text = dsEmp.Tables[0].Rows[0]["DOB"].ToString();

                            txtphno.Text = dsEmp.Tables[0].Rows[0]["Phno_No"].ToString();
                            txtemail.Text = dsEmp.Tables[0].Rows[0]["Email_Id"].ToString();
                            txtpwd.Text = dsEmp.Tables[0].Rows[0]["Pssword"].ToString();
                            ddldesignation.SelectedValue = dsEmp.Tables[0].Rows[0]["Desigination"].ToString();
                            ddlservice.SelectedValue = dsEmp.Tables[0].Rows[0]["Service"].ToString();
                            txtDocumentsSubmitted.Text = dsEmp.Tables[0].Rows[0]["Documents_Submitted"].ToString();
                            txtpfno.Text = dsEmp.Tables[0].Rows[0]["PF_NO"].ToString();
                            txtESINO.Text = dsEmp.Tables[0].Rows[0]["ESI_NO"].ToString();
                            txtsalary.Text = dsEmp.Tables[0].Rows[0]["Salary"].ToString();
                            txtannulasal.Text = dsEmp.Tables[0].Rows[0]["AnnualCTC"].ToString();
                            ddlbranches.SelectedValue = dsEmp.Tables[0].Rows[0]["Branch"].ToString();
                            ddljobtype.SelectedValue = dsEmp.Tables[0].Rows[0]["JobType"].ToString();
                            txtdoj.Text = dsEmp.Tables[0].Rows[0]["DOJ"].ToString();
                            txtaddress.Text = dsEmp.Tables[0].Rows[0]["Address"].ToString();

                            txtDepartment.Text = dsEmp.Tables[0].Rows[0]["Department"].ToString();
                            txtEmpCategory.Text = dsEmp.Tables[0].Rows[0]["EmpCategory"].ToString();

                            txtpwd.Text = dsEmp.Tables[0].Rows[0]["Pssword"].ToString();

                            ddlStatus.SelectedValue = dsEmp.Tables[0].Rows[0]["Status"].ToString();
                            ttDateofLeaving.Text = dsEmp.Tables[0].Rows[0]["DOL"].ToString();

                            ddlUnit.SelectedValue = dsEmp.Tables[0].Rows[0]["UnitID"].ToString();
                            ddlSalary.SelectedValue = dsEmp.Tables[0].Rows[0]["SalaryTypeID"].ToString();

                            txtaltphono.Text = dsEmp.Tables[0].Rows[0]["Mobile1"].ToString();
                            ddldepartment.SelectedValue = dsEmp.Tables[0].Rows[0]["Deptid"].ToString();

                            txtfathername.Text = dsEmp.Tables[0].Rows[0]["FatherName"].ToString();

                            lblFile_Path.Text = dsEmp.Tables[0].Rows[0]["UploadImage"].ToString();
                            img_Photo.ImageUrl = dsEmp.Tables[0].Rows[0]["UploadImage"].ToString();

                            Label2.Text = dsEmp.Tables[0].Rows[0]["UploadDoc1"].ToString();
                            Image1.ImageUrl = dsEmp.Tables[0].Rows[0]["UploadDoc1"].ToString();

                            Label3.Text = dsEmp.Tables[0].Rows[0]["UploadDoc2"].ToString();
                            Image2.ImageUrl = dsEmp.Tables[0].Rows[0]["UploadDoc2"].ToString();

                            txtSalaryAmount.Text = dsEmp.Tables[0].Rows[0]["SalaryAmount"].ToString();

                            string checlogin = dsEmp.Tables[0].Rows[0]["CheckLogin"].ToString();

                            ddlMaritalStatus.SelectedValue = dsEmp.Tables[0].Rows[0]["IsMarried"].ToString();
                            ddlIsMotherWife.SelectedValue = dsEmp.Tables[0].Rows[0]["IsMotherWife"].ToString();
                            txtMotherWife.Text = dsEmp.Tables[0].Rows[0]["MotherWife"].ToString();
                            txtPermanentAddress.Text = dsEmp.Tables[0].Rows[0]["Address1"].ToString();

                            if (checlogin == "Y")
                            {
                                chklogin.Checked = true;
                            }
                            else
                            {
                                chklogin.Checked = false;
                            }


                            string appticket = dsEmp.Tables[0].Rows[0]["CheckTicketApproval"].ToString();

                            if (appticket == "Y")
                            {
                                chkticketapproval.Checked = true;
                            }
                            else
                            {
                                chkticketapproval.Checked = false;
                            }
                            txtUnactivedate.Text = dsEmp.Tables[0].Rows[0]["UnActiveDate"].ToString();
                            ddlFather.SelectedValue = dsEmp.Tables[0].Rows[0]["IsHusbandFather"].ToString();
                            ddlOT.SelectedValue = dsEmp.Tables[0].Rows[0]["OtAllow"].ToString();
                        }
                    }
                }
                else
                {
                    //ds = objbs.SelectMaxId();
                    DataSet ds1 = objbs.SelectMaxId();
                    string a = ds1.Tables[0].Rows[0]["Employee_Id"].ToString();
                    if (a == "" || a == null)
                    {
                        txtemployid.Text = "1";

                    }
                    else
                    {
                        txtemployid.Text = ds1.Tables[0].Rows[0]["Employee_Id"].ToString();
                    }
                }
            }
        }

        protected void btnUpload_OnClick(object sender, EventArgs e)
        {

            if (fp_Upload.PostedFile != null)
            {
                // Check the extension of image  
                string extension = Path.GetExtension(fp_Upload.FileName);
                if (extension.ToLower() == ".png" || extension.ToLower() == ".jpg")
                {
                    Stream strm = fp_Upload.PostedFile.InputStream;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        // Print Original Size of file (Height or Width)   
                        Label1.Text = image.Size.ToString();
                        int newWidth = 240; // New Width of Image in Pixel  
                        int newHeight = 240; // New Height of Image in Pixel  
                        var thumbImg = new Bitmap(newWidth, newHeight);
                        var thumbGraph = Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);
                        // Save the file  
                        string targetPath = Server.MapPath(@"~\Sampling\") + fp_Upload.FileName;
                        thumbImg.Save(targetPath, image.RawFormat);
                        // Print new Size of file (height or Width)  
                        lblFile_Path.Text = thumbImg.Size.ToString();
                        //Show Image  
                        img_Photo.ImageUrl = @"~\Sampling\" + fp_Upload.FileName;
                       
                    }
                }
            } 

            //if (fp_Upload.HasFile)
            //{
                string fileName = Path.GetFileName(fp_Upload.PostedFile.FileName);
                fp_Upload.PostedFile.SaveAs(Server.MapPath("~/Sampling/") + fileName.Replace(" ", ""));
                lblFile_Path.Text = "~/Sampling/" + fp_Upload.PostedFile.FileName.Replace(" ", "");
                img_Photo.ImageUrl = "~/Sampling/" + fp_Upload.PostedFile.FileName.Replace(" ", "");
                lblfilename.Text = fileName;

            //    //System.Drawing.Image image = System.Drawing.Image.FromFile("FilePath");
            //    //int newwidthimg = 50;
            //    //float AspectRatio = (float)image.Size.Width / (float)image.Size.Height;
            //    //int newHeight = Convert.ToInt32(newwidthimg / AspectRatio);
            //    //Bitmap thumbnailBitmap = new Bitmap(newwidthimg, newHeight);
            //    //Graphics thumbnailGraph = Graphics.FromImage(thumbnailBitmap);
            //    //thumbnailGraph.CompositingQuality = CompositingQuality.HighQuality;
            //    //thumbnailGraph.SmoothingMode = SmoothingMode.HighQuality;
            //    //thumbnailGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    //var imageRectangle = new Rectangle(0, 0, newwidthimg, newHeight);
            //    //thumbnailGraph.DrawImage(image, imageRectangle);
            //    //thumbnailBitmap.Save("FilePath", ImageFormat.Jpeg);
            //    //thumbnailGraph.Dispose();
            //    //thumbnailBitmap.Dispose();
            //    //image.Dispose();

            //}
        }

        protected void btnUpload1_OnClick(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Sampling/") + fileName.Replace(" ", ""));
                Label2.Text = "~/Sampling/" + FileUpload1.PostedFile.FileName.Replace(" ", "");
                Image1.ImageUrl = "~/Sampling/" + FileUpload1.PostedFile.FileName.Replace(" ", "");
                lblfilename2.Text = fileName;
            }
        }

        protected void btnUpload2_OnClick(object sender, EventArgs e)
        {
            if (FileUpload2.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
                FileUpload2.PostedFile.SaveAs(Server.MapPath("~/Sampling/") + fileName.Replace(" ", ""));
                Label3.Text = "~/Sampling/" + FileUpload2.PostedFile.FileName.Replace(" ", "");
                Image2.ImageUrl = "~/Sampling/" + FileUpload2.PostedFile.FileName.Replace(" ", "");
                lblfilename3.Text = fileName;
            }
        }

        protected void ddlservice_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlservice.SelectedValue != "--Select Service--")
            //{

            //    ds = objbs.desigination1(Convert.ToInt32(ddlservice.SelectedValue));
            //    ddldesignation.DataSource = ds;
            //    ddldesignation.DataValueField = "DesiginationId";
            //    ddldesignation.DataTextField = "DesiginationName";
            //    ddldesignation.DataBind();

            //}
        }
      
        protected void ddlbranches_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlbranches.SelectedItem.Text != "--Select Branch--")
            //{


            //}
        }

        protected void ddljobtype_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            //{

            //    if (ddljobtype.SelectedItem.Text == "Contract")
            //    {
            //        txtcontract.Visible = true;
            //        lblcontract.Visible = true;
            //        txtcontract.Text = "";
            //    }
            //    else
            //    {
            //        txtcontract.Visible = false;
            //        lblcontract.Visible = false;
            //        txtcontract.Text = "-";
            //    }

            //}
        }

        protected void btnsubmit_Click1(object sender, EventArgs e)
        {
            int unit = 0;
            string checklogin = "N";
            string AppTicket = "N";

            #region

            if (ddlUnit.SelectedValue == "All")
            {
                unit = 0;
            }
            else
            {
                unit = Convert.ToInt32(ddlUnit.SelectedValue);
            }

            if (chklogin.Checked == true)
                checklogin = "Y";

            if (chkticketapproval.Checked == true)
                AppTicket = "Y";

            #endregion

            if (btnsubmit.Text == "SUBMIT")
            {
                if (ddldesignation.SelectedItem.Text == "Select Desigination")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Enter Contract Period.');", true);
                    return;
                }

                int i = objbs.FetchRecords(txtname.Text,txtdob.Text, txtdoj.Text, txtaddress.Text, txtphno.Text, txtemail.Text, txtpwd.Text,
                   Convert.ToInt32(ddldesignation.SelectedValue), Convert.ToInt32(0), txtDocumentsSubmitted.Text, txtemploycode.Text, Convert.ToInt32(0), Convert.ToDouble(0.00),
                    Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(0), "",Convert.ToDouble(0.00),Convert.ToInt32( ddlStatus.SelectedValue),
                    ttDateofLeaving.Text, txtDepartment.Text, txtEmpCategory.Text, unit, Convert.ToInt32(ddlSalary.SelectedValue), ddlSalary.SelectedItem.Text, txtaltphono.Text, ddldepartment.SelectedValue, lblFile_Path.Text, Label2.Text, Label3.Text, txtfathername.Text, checklogin, AppTicket, Convert.ToDouble(txtSalaryAmount.Text), Convert.ToInt32(ddlMaritalStatus.SelectedValue), Convert.ToInt32(ddlIsMotherWife.SelectedValue), txtMotherWife.Text, txtPermanentAddress.Text, ddlFather.SelectedValue, txtUnactivedate.Text, ddlOT.SelectedValue);
    
            }
            else
            {
                if (ddldesignation.SelectedItem.Text == "Select Desigination")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Desigination.');", true);
                    return;
                }

                int i = objbs.hrm(Convert.ToInt32(txtemployid.Text), txtname.Text, txtdob.Text, txtdoj.Text, txtaddress.Text, txtphno.Text, txtemail.Text, txtpwd.Text,
                    Convert.ToInt32(ddldesignation.SelectedValue), Convert.ToInt32(0), txtDocumentsSubmitted.Text, txtemploycode.Text,
                    Convert.ToInt32(0), Convert.ToDouble(0.00), Convert.ToInt32(0), Convert.ToInt32(0), Convert.ToInt32(0), "", Convert.ToDouble(0.00),
                    Convert.ToInt32(ddlStatus.SelectedValue), ttDateofLeaving.Text, txtDepartment.Text, txtEmpCategory.Text, unit, Convert.ToInt32(ddlSalary.SelectedValue), ddlSalary.SelectedItem.Text, txtaltphono.Text, ddldepartment.SelectedValue, lblFile_Path.Text, Label2.Text, Label3.Text, txtfathername.Text, checklogin, AppTicket, Convert.ToDouble(txtSalaryAmount.Text), Convert.ToInt32(ddlMaritalStatus.SelectedValue), Convert.ToInt32(ddlIsMotherWife.SelectedValue), txtMotherWife.Text, txtPermanentAddress.Text, ddlFather.SelectedValue, txtUnactivedate.Text, ddlOT.SelectedValue);
             
            }

            Response.Redirect("Emp_gird.aspx");

            System.Threading.Thread.Sleep(3000);
        }

        protected void txtemploycode_TextChanged(object sender, EventArgs e)
        {
            ds = objbs.CheckEmp_code(txtemploycode.Text);
            if ((ds.Tables[0].Rows.Count) > 0)
            {
                lblerror.Text = "Employee Code already Exists";
                btnsubmit.Visible = false;
                txtemploycode.Focus();

            }
            else
            {
                lblerror.Text = "";
                btnsubmit.Visible = true;
                txtname.Focus();

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("Emp_gird.aspx");
        }

       
       
        
    }
}