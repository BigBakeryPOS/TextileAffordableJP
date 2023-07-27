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
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class DilloStitchingDetails : System.Web.UI.Page
    {
        BSClass objectBaseClass = new BSClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblTodaysDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddlSignature.DataSource = objectBaseClass.Select_EmpolyeeDetails();
                ddlSignature.DataValueField = "Employee_Id";
                ddlSignature.DataTextField = "Name";
                ddlSignature.DataBind();
                ddlSignature.Items.Insert(0, "Please Select Employees");

                string cuttingInformationID = Request.QueryString.Get("CuttingInformationID");
                string Name = Request.QueryString.Get("Name");
                if (cuttingInformationID != "" || cuttingInformationID != null)
                {
                    DataSet dataSet = objectBaseClass.EditCustomerInfo(cuttingInformationID);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (Name == "copy")
                        {
                            btnadd.Text = "Save";
                            txtDesignNo.Text = dataSet.Tables[0].Rows[0]["DesignNumber"].ToString();
                            txtDesignNo1.Text = dataSet.Tables[0].Rows[0]["DesignNumber1"].ToString();
                            txtDesignNo2.Text = dataSet.Tables[0].Rows[0]["DesignNumber2"].ToString();
                            txtDesignNo3.Text = dataSet.Tables[0].Rows[0]["DesignNumber3"].ToString();
                            txtDesignNo4.Text = dataSet.Tables[0].Rows[0]["DesignNumber4"].ToString();
                            ddlSignature.SelectedValue = dataSet.Tables[0].Rows[0]["Employee_Id"].ToString();
                            txtOperatorName.Text = dataSet.Tables[0].Rows[0]["OperatorName"].ToString();
                            txtItem.Text = dataSet.Tables[0].Rows[0]["Item"].ToString();
                            txtRange.Text = dataSet.Tables[0].Rows[0]["Range"].ToString();
                            txtQuantity.Text = dataSet.Tables[0].Rows[0]["Quantity"].ToString();
                            txtPendingQuantity.Text = dataSet.Tables[0].Rows[0]["PendingQuantity"].ToString();
                            txtStitchingDate.Text = dataSet.Tables[0].Rows[0]["StitchingDate"].ToString();
                            txtDueDate.Text = dataSet.Tables[0].Rows[0]["DueDate"].ToString();
                            txtRemarks.Text = dataSet.Tables[0].Rows[0]["Remarks"].ToString();

                            string shirt = dataSet.Tables[0].Rows[0]["Shirt"].ToString();
                            if (shirt == "True")
                            {
                                chckShirt.Checked = true;
                            }

                            string pant = dataSet.Tables[0].Rows[0]["Pant"].ToString();
                            if (pant == "True")
                            {
                                ChckPant.Checked = true;
                            }

                            string coat = dataSet.Tables[0].Rows[0]["Coat"].ToString();
                            if (coat == "True")
                            {
                                ChckCoat.Checked = true;
                            }

                            string Tshrt = dataSet.Tables[0].Rows[0]["Tshirt"].ToString();
                            if (Tshrt == "True")
                            {
                                ChckTshrt.Checked = true;
                            }

                            txtReceivedDate.Text = dataSet.Tables[0].Rows[0]["ReceivedDate"].ToString();
                            txtRate.Text = dataSet.Tables[0].Rows[0]["Rate"].ToString();
                            txtSellingPrice.Text = dataSet.Tables[0].Rows[0]["SellingPrice"].ToString();
                            txtSamplePrice.Text = dataSet.Tables[0].Rows[0]["SamplePrice"].ToString();

                            ddStatus.SelectedValue = dataSet.Tables[0].Rows[0]["Status"].ToString();

                            txtRemarks.Text = dataSet.Tables[0].Rows[0]["Remarks"].ToString();
                            txtPendingQuantity.Text = dataSet.Tables[0].Rows[0]["PendingQuantity"].ToString();


                            lblRemarks.Visible = false;
                            lblPendingQuantity.Visible = false;

                            if (ddStatus.SelectedValue == "3")
                            {
                                //txtDesignNo.Enabled = false;
                                //txtDesignNo1.Enabled = false;
                                //txtDesignNo2.Enabled = false;
                                //txtDesignNo3.Enabled = false;
                                //txtDesignNo4.Enabled = false;
                                //txtOperatorName.Enabled = false;
                                //ddlItem.Enabled = false;
                                //txtRange.Enabled = false;
                                //txtQuantity.Enabled = false;
                                //txtRate.Enabled = false;
                                //txtStitchingDate.Enabled = false;
                                //txtDueDate.Enabled = false;
                                //txtReceivedDate.Enabled = false;
                                //ChckPant.Enabled = false;
                                //chckShirt.Enabled = false;
                                //ddlSignature.Enabled = false;

                                lblRemarks.Visible = true;
                                lblPendingQuantity.Visible = true;
                            }

                            imgEmp.ImageUrl = dataSet.Tables[0].Rows[0]["EmpImagePage"].ToString();
                            imageSample.ImageUrl = dataSet.Tables[0].Rows[0]["ImagePath"].ToString();
                            imageSample1.ImageUrl = dataSet.Tables[0].Rows[0]["ImagePath1"].ToString();
                            imageSample2.ImageUrl = dataSet.Tables[0].Rows[0]["ImagePath2"].ToString();
                            imageSample3.ImageUrl = dataSet.Tables[0].Rows[0]["ImagePath3"].ToString();
                            if (imgEmp.ImageUrl != "")
                            {
                                imgEmp.ImageUrl = dataSet.Tables[0].Rows[0]["EmpImagePage"].ToString();
                            }
                            else
                            {
                                imgEmp.ImageUrl = "../images/default_image.jpg";
                            }
                        }
                        else
                        {
                            btnadd.Text = "Update";
                            txtDesignNo.Text = dataSet.Tables[0].Rows[0]["DesignNumber"].ToString();
                            txtDesignNo1.Text = dataSet.Tables[0].Rows[0]["DesignNumber1"].ToString();
                            txtDesignNo2.Text = dataSet.Tables[0].Rows[0]["DesignNumber2"].ToString();
                            txtDesignNo3.Text = dataSet.Tables[0].Rows[0]["DesignNumber3"].ToString();
                            txtDesignNo4.Text = dataSet.Tables[0].Rows[0]["DesignNumber4"].ToString();
                            ddlSignature.SelectedValue = dataSet.Tables[0].Rows[0]["Employee_Id"].ToString();
                            txtOperatorName.Text = dataSet.Tables[0].Rows[0]["OperatorName"].ToString();
                            txtItem.Text = dataSet.Tables[0].Rows[0]["Item"].ToString();
                            txtRange.Text = dataSet.Tables[0].Rows[0]["Range"].ToString();
                            txtQuantity.Text = dataSet.Tables[0].Rows[0]["Quantity"].ToString();
                            txtStitchingDate.Text = dataSet.Tables[0].Rows[0]["StitchingDate"].ToString();
                            txtDueDate.Text = dataSet.Tables[0].Rows[0]["DueDate"].ToString();
                            txtPendingQuantity.Text = dataSet.Tables[0].Rows[0]["PendingQuantity"].ToString();
                            txtRemarks.Text = dataSet.Tables[0].Rows[0]["Remarks"].ToString();

                            string shirt = dataSet.Tables[0].Rows[0]["Shirt"].ToString();
                            if (shirt == "True")
                            {
                                chckShirt.Checked = true;
                            }

                            string pant = dataSet.Tables[0].Rows[0]["Pant"].ToString();
                            if (pant == "True")
                            {
                                ChckPant.Checked = true;
                            }

                            string coat = dataSet.Tables[0].Rows[0]["Coat"].ToString();
                            if (coat == "True")
                            {
                                ChckCoat.Checked = true;
                            }

                            string Tshrt = dataSet.Tables[0].Rows[0]["Tshirt"].ToString();
                            if (Tshrt == "True")
                            {
                                ChckTshrt.Checked = true;
                            }

                            txtReceivedDate.Text = dataSet.Tables[0].Rows[0]["ReceivedDate"].ToString();
                            txtRate.Text = dataSet.Tables[0].Rows[0]["Rate"].ToString();
                            txtSellingPrice.Text = dataSet.Tables[0].Rows[0]["SellingPrice"].ToString();
                            txtSamplePrice.Text = dataSet.Tables[0].Rows[0]["SamplePrice"].ToString();

                            ddStatus.SelectedValue = dataSet.Tables[0].Rows[0]["Status"].ToString();

                            txtRemarks.Text = dataSet.Tables[0].Rows[0]["Remarks"].ToString();
                            txtPendingQuantity.Text = dataSet.Tables[0].Rows[0]["PendingQuantity"].ToString();


                            lblRemarks.Visible = false;
                            lblPendingQuantity.Visible = false;

                            if (ddStatus.SelectedValue == "3")
                            {
                                //txtDesignNo.Enabled = false;
                                //txtDesignNo1.Enabled = false;
                                //txtDesignNo2.Enabled = false;
                                //txtDesignNo3.Enabled = false;
                                //txtDesignNo4.Enabled = false;
                                //txtOperatorName.Enabled = false;
                                //ddlItem.Enabled = false;
                                //txtRange.Enabled = false;
                                //txtQuantity.Enabled = false;
                                //txtRate.Enabled = false;
                                //txtStitchingDate.Enabled = false;
                                //txtDueDate.Enabled = false;
                                //txtReceivedDate.Enabled = false;
                                //ChckPant.Enabled = false;
                                //chckShirt.Enabled = false;
                                //ddlSignature.Enabled = false;

                                lblRemarks.Visible = true;
                                lblPendingQuantity.Visible = true;
                            }

                            imgEmp.ImageUrl = dataSet.Tables[0].Rows[0]["EmpImagePage"].ToString();
                            imageSample.ImageUrl = dataSet.Tables[0].Rows[0]["ImagePath"].ToString();
                            imageSample1.ImageUrl = dataSet.Tables[0].Rows[0]["ImagePath1"].ToString();
                            imageSample2.ImageUrl = dataSet.Tables[0].Rows[0]["ImagePath2"].ToString();
                            imageSample3.ImageUrl = dataSet.Tables[0].Rows[0]["ImagePath3"].ToString();
                            if (imgEmp.ImageUrl != "")
                            {
                                imgEmp.ImageUrl = dataSet.Tables[0].Rows[0]["EmpImagePage"].ToString();
                            }
                            else
                            {
                                imgEmp.ImageUrl = "../images/default_image.jpg";
                            }

                            txtEmpCode.Text = dataSet.Tables[0].Rows[0]["EmployeeCode"].ToString();
                        }
                    }
                    else
                    {
                        imgEmp.ImageUrl = "../images/default_image.jpg";
                        lblRemarks.Visible = false;
                        lblPendingQuantity.Visible = false;
                    }
                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string shirt = "";
            string pant = "";
            string coat = "";
            string Tshirt = "";

            string cuttingInformationID = Request.QueryString.Get("CuttingInformationID");
            DateTime stitchingDate = DateTime.ParseExact(txtStitchingDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dueDate = DateTime.ParseExact(txtDueDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string quantity = txtQuantity.Text;
            string rate = txtRate.Text;
            decimal total = Convert.ToDecimal(quantity) * Convert.ToDecimal(rate);

            if (txtPendingQuantity.Text == "")
            {
                txtPendingQuantity.Text = "0";
            }

            if (btnadd.Text == "Save")
            {
                if (chckShirt.Checked == true)
                {
                    shirt = "True";
                }
                else
                {
                    shirt = "False";
                }

                if (ChckPant.Checked == true)
                {
                    pant = "True";
                }
                else
                {
                    pant = "False";
                }

                if (ChckCoat.Checked == true)
                {
                    coat = "True";
                }
                else
                {
                    coat = "False";
                }

                if (ChckTshrt.Checked == true)
                {
                    Tshirt = "True";
                }
                else
                {
                    Tshirt = "False";
                }

                //if (stitchingDate < DateTime.Today)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please check Stitching Date will be Today or Upcoming Date!');", true);
                //    return;
                //}

                if (stitchingDate <= dueDate)
                {
                    int insertDetails = objectBaseClass.InsertEmployeeCuttingInformation(txtDesignNo.Text, txtDesignNo1.Text, txtDesignNo2.Text, txtDesignNo3.Text, txtDesignNo4.Text, ddlSignature.SelectedValue, txtOperatorName.Text,
                        txtItem.Text, txtRange.Text, Convert.ToInt32(txtQuantity.Text), Convert.ToInt32(txtPendingQuantity.Text), txtStitchingDate.Text, txtDueDate.Text, shirt,
                        pant, coat, Tshirt, txtReceivedDate.Text, txtRate.Text, txtSellingPrice.Text, txtSamplePrice.Text, total, ddStatus.SelectedValue, txtRemarks.Text, lblSampleImage.Text,
                        lblSampleImage1.Text, lblSampleImage2.Text, lblSampleImage3.Text,0,"0");
                    Response.Redirect("DilloStitchingInfoGrid.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please check Due Date will be greater than Stitching Date!');", true);
                }
            }
            else
            {
                if (chckShirt.Checked == true)
                {
                    shirt = "True";
                }
                else
                {
                    shirt = "False";
                }

                if (ChckPant.Checked == true)
                {
                    pant = "True";
                }
                else
                {
                    pant = "False";
                }

                if (ChckCoat.Checked == true)
                {
                    coat = "True";
                }
                else
                {
                    coat = "False";
                }

                if (ChckTshrt.Checked == true)
                {
                    Tshirt = "True";
                }
                else
                {
                    Tshirt = "False";
                }



                int pendingQty = Convert.ToInt32(txtPendingQuantity.Text);
                int qty = Convert.ToInt32(txtQuantity.Text);
                //int totalPending = 0;
                decimal pendingRate = Convert.ToDecimal(txtRate.Text);
                if (pendingQty != 0)
                {
                    int qtyTotal = qty - pendingQty;
                    total = qtyTotal * pendingRate;
                }


                if (stitchingDate <= dueDate)
                {
                    int updateDetails = objectBaseClass.UpdateEmployeeCuttingInformation(Convert.ToInt32(cuttingInformationID), txtDesignNo.Text, txtDesignNo1.Text, txtDesignNo2.Text, txtDesignNo3.Text, txtDesignNo4.Text, ddlSignature.SelectedValue, txtOperatorName.Text,
                        txtItem.Text, txtRange.Text, Convert.ToInt32(txtQuantity.Text), Convert.ToInt32(txtPendingQuantity.Text), txtStitchingDate.Text, txtDueDate.Text, shirt,
                        pant, coat, Tshirt, txtReceivedDate.Text, txtRate.Text, txtSellingPrice.Text, txtSamplePrice.Text, total, ddStatus.Text, txtRemarks.Text, lblSampleImage.Text,
                        lblSampleImage1.Text, lblSampleImage2.Text, lblSampleImage3.Text,0, "0");

                    Response.Redirect("DilloStitchingInfoGrid.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Please check Due Date will be greater than Stitching Date!');", true);
                }
            }
        }

        protected void ddlSignature_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlSignature.SelectedValue != "Please Select Employees")
            {
                DataSet dataSet = objectBaseClass.Select_EmpolyeeDetailsWhere(Convert.ToInt32(ddlSignature.SelectedValue));

                if (dataSet.Tables[0].Rows[0]["ImagePath"].ToString() != "" && dataSet.Tables[0].Rows.Count > 0 && dataSet != null)
                {
                    imgEmp.ImageUrl = dataSet.Tables[0].Rows[0]["ImagePath"].ToString();
                }
                else
                {
                    imgEmp.ImageUrl = "../images/default_image.jpg";
                }
            }
            else
            {
                imgEmp.ImageUrl = "../images/default_image.jpg";
            }


        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("ARFA_CuttingGrid.aspx");
        }

        protected void ddlStatus_Changed(object sender, EventArgs e)
        {
            if (ddStatus.SelectedValue == "3")
            {
                lblRemarks.Visible = true;
                lblPendingQuantity.Visible = true;
            }
            else
            {
                //txtDesignNo.Enabled = true;
                //txtDesignNo1.Enabled = true;
                //txtDesignNo2.Enabled = true;
                //txtDesignNo3.Enabled = true;
                //txtDesignNo4.Enabled = true;
                //txtOperatorName.Enabled = true;
                //ddlItem.Enabled = true;
                //txtRange.Enabled = true;
                //txtQuantity.Enabled = true;
                //txtRate.Enabled = true;
                //txtStitchingDate.Enabled = true;
                //txtDueDate.Enabled = true;
                //txtReceivedDate.Enabled = true;
                //ChckPant.Enabled = true;
                //chckShirt.Enabled = true;
                //ddlSignature.Enabled = true;

                lblRemarks.Visible = false;
                lblPendingQuantity.Visible = false;
            }

        }

        //protected void ddlItem_Changed(object sender, EventArgs e)
        //{
        //if (ddlItem.SelectedValue == "2*2")
        //{
        //    txtQuantity.Text = "2";
        //}
        //else if (ddlItem.SelectedValue == "2*3")
        //{
        //    txtQuantity.Text = "3";
        //}
        //else if (ddlItem.SelectedValue == "2*4")
        //{
        //    txtQuantity.Text = "4";
        //}
        //else if (ddlItem.SelectedValue == "2*5")
        //{
        //    txtQuantity.Text = "5";
        //}
        //else if (ddlItem.SelectedValue == "2*6")
        //{
        //    txtQuantity.Text = "6";
        //}
        //else if (ddlItem.SelectedValue == "2*7")
        //{
        //    txtQuantity.Text = "7";
        //}
        //else if (ddlItem.SelectedValue == "2*8")
        //{
        //    txtQuantity.Text = "8";
        //}
        //else if (ddlItem.SelectedValue == "M.L.XL")
        //{
        //    txtQuantity.Text = "3";
        //}
        //}

        protected void btnPrint_Data(object sender, EventArgs e)
        {
            string cuttingInformationID = Request.QueryString.Get("CuttingInformationID");
            Response.Redirect("ARFA_CuttingInfoPrint.aspx?CuttingInformationID=" + cuttingInformationID + "&Name=" + ddlSignature.SelectedItem.Text + "&EmpImage=" + imgEmp.ImageUrl + "&EmpCode=" + txtEmpCode.Text);
        }

        protected void btnUpload1_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                {
                    FileUpload1.SaveAs(MapPath("~/Files/" + FileUpload1.FileName));
                    imageSample.ImageUrl = imageSample.ImageUrl = "~/Files/" + FileUpload1.FileName;
                    lblSampleImage.Text = imageSample.ImageUrl;
                    UpdatePanel1.Update();
                }
            }
        }

        protected void btnUpload2_Click(object sender, EventArgs e)
        {
            if (FileUpload2.HasFile)
            {
                {
                    FileUpload2.SaveAs(MapPath("~/Files/" + FileUpload2.FileName));
                    imageSample1.ImageUrl = imageSample1.ImageUrl = "~/Files/" + FileUpload2.FileName;
                    lblSampleImage1.Text = imageSample1.ImageUrl;
                    UpdatePanel2.Update();
                }
            }
        }

        protected void btnUpload3_Click(object sender, EventArgs e)
        {
            if (FileUpload3.HasFile)
            {
                {
                    FileUpload3.SaveAs(MapPath("~/Files/" + FileUpload3.FileName));
                    imageSample2.ImageUrl = imageSample2.ImageUrl = "~/Files/" + FileUpload3.FileName;
                    lblSampleImage2.Text = imageSample2.ImageUrl;
                    UpdatePanel3.Update();
                }
            }
        }

        protected void btnUpload4_Click(object sender, EventArgs e)
        {
            if (FileUpload4.HasFile)
            {
                {
                    FileUpload4.SaveAs(MapPath("~/Files/" + FileUpload4.FileName));
                    imageSample3.ImageUrl = imageSample3.ImageUrl = "~/Files/" + FileUpload4.FileName;
                    lblSampleImage3.Text = imageSample3.ImageUrl;
                    UpdatePanel4.Update();
                }
            }
        }
    }
}