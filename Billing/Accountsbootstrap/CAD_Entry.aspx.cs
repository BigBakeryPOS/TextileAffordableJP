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
    public partial class CAD_Entry : System.Web.UI.Page
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
                txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                #region

                DataSet dsStyle = objBs.GetAllStyleNo_CAD();
                if (dsStyle.Tables[0].Rows.Count > 0)
                {
                    ddlStyles.DataSource = dsStyle.Tables[0];
                    ddlStyles.DataTextField = "StyleNo";
                    ddlStyles.DataValueField = "SamplingCostingId";
                    ddlStyles.DataBind();
                    ddlStyles.Items.Insert(0, "Select StyleNo");
                }

                #endregion

                string cadid = Request.QueryString.Get("cadid");
                if (cadid != "" || cadid != null)
                {
                    DataSet dscadentry = objBs.getCAD_Value(Convert.ToInt32(cadid));
                    if (dscadentry.Tables[0].Rows.Count > 0)
                    {

                        DataSet dsStyle1 = objBs.GetAllStyleNo();
                        if (dsStyle1.Tables[0].Rows.Count > 0)
                        {
                            ddlStyles.DataSource = dsStyle1.Tables[0];
                            ddlStyles.DataTextField = "StyleNo";
                            ddlStyles.DataValueField = "SamplingCostingId";
                            ddlStyles.DataBind();
                            ddlStyles.Items.Insert(0, "Select StyleNo");
                        }

                        lblcadid.Text = cadid;
                        ddlStyles.SelectedValue = dscadentry.Tables[0].Rows[0]["StyleNo"].ToString();
                        ddlStyles.Enabled = false;
                        txtDate.Text = Convert.ToDateTime(dscadentry.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                        chkfit1.Checked = Convert.ToBoolean(dscadentry.Tables[0].Rows[0]["Fit1"]);
                        chkfit2.Checked = Convert.ToBoolean(dscadentry.Tables[0].Rows[0]["Fit2"]);
                        chkfit3.Checked = Convert.ToBoolean(dscadentry.Tables[0].Rows[0]["Fit3"]);

                        txtfit1notes.Text = dscadentry.Tables[0].Rows[0]["Fit1Notes"].ToString();
                        txtfit2notes.Text = dscadentry.Tables[0].Rows[0]["Fit2Notes"].ToString();
                        txtfit3notes.Text = dscadentry.Tables[0].Rows[0]["Fit3Notes"].ToString();

                        lblFile_Path2.Text = dscadentry.Tables[0].Rows[0]["Fit1image"].ToString();
                        lblFile_Path3.Text = dscadentry.Tables[0].Rows[0]["Fit2image"].ToString();
                        lblFile_Path4.Text = dscadentry.Tables[0].Rows[0]["Fit3image"].ToString();
                        //lblFile_Path2.Text = dscadentry.Tables[0].Rows[0]["Fit1image"].ToString();

                        img_Photo2.ImageUrl = dscadentry.Tables[0].Rows[0]["Fit1image"].ToString();
                        img_Photo3.ImageUrl = dscadentry.Tables[0].Rows[0]["Fit2image"].ToString();
                        img_Photo4.ImageUrl = dscadentry.Tables[0].Rows[0]["Fit3image"].ToString();

                        lblFile_Path.Text = dscadentry.Tables[0].Rows[0]["PPImage"].ToString();
                        img_Photo.ImageUrl = dscadentry.Tables[0].Rows[0]["PPImage"].ToString();
                        txtppdesc.Text = dscadentry.Tables[0].Rows[0]["PPNotes"].ToString();


                        lblFile_Path1.Text = dscadentry.Tables[0].Rows[0]["MarkerImage"].ToString();
                        img_Photo1.ImageUrl = dscadentry.Tables[0].Rows[0]["MarkerImage"].ToString();
                        txtmarkerdesc.Text = dscadentry.Tables[0].Rows[0]["MarkerNotes"].ToString();

                        btnSave.Text = "Update";
                       
                    }
                }
            }
        }

        protected void btnSizeRefresh_OnClick(object sender, EventArgs e)
        {
           

        }

        protected void ddlItemTypeGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            

        }

        protected void ddlItemName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
          

        }

        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            if (fp_Upload.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload.PostedFile.FileName);
                fp_Upload.PostedFile.SaveAs(Server.MapPath("~/CADIMAGE/") + fileName.Replace(" ", ""));
                lblFile_Path.Text = "~/CADIMAGE/" + fp_Upload.PostedFile.FileName.Replace(" ", "");
                img_Photo.ImageUrl = "~/CADIMAGE/" + fp_Upload.PostedFile.FileName.Replace(" ", "");
            }
        }

        protected void btnUpload1_OnClick(object sender, EventArgs e)
        {
            if (fp_Upload1.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload1.PostedFile.FileName);
                fp_Upload1.PostedFile.SaveAs(Server.MapPath("~/CADIMAGE/") + fileName.Replace(" ", ""));
                lblFile_Path1.Text = "~/CADIMAGE/" + fp_Upload1.PostedFile.FileName.Replace(" ", "");
                img_Photo1.ImageUrl = "~/CADIMAGE/" + fp_Upload1.PostedFile.FileName.Replace(" ", "");
            }
        }

        protected void btnUpload2_OnClick(object sender, EventArgs e)
        {
            if (fp_Upload2.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload2.PostedFile.FileName);
                fp_Upload2.PostedFile.SaveAs(Server.MapPath("~/CADIMAGE/") + fileName.Replace(" ", ""));
                lblFile_Path2.Text = "~/CADIMAGE/" + fp_Upload2.PostedFile.FileName.Replace(" ", "");
                img_Photo2.ImageUrl = "~/CADIMAGE/" + fp_Upload2.PostedFile.FileName.Replace(" ", "");
            }
        }

        protected void btnUpload3_OnClick(object sender, EventArgs e)
        {
            if (fp_Upload3.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload3.PostedFile.FileName);
                fp_Upload3.PostedFile.SaveAs(Server.MapPath("~/CADIMAGE/") + fileName.Replace(" ", ""));
                lblFile_Path3.Text = "~/CADIMAGE/" + fp_Upload3.PostedFile.FileName.Replace(" ", "");
                img_Photo3.ImageUrl = "~/CADIMAGE/" + fp_Upload3.PostedFile.FileName.Replace(" ", "");
            }
        }

        protected void btnUpload4_OnClick(object sender, EventArgs e)
        {
            if (fp_Upload4.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload4.PostedFile.FileName);
                fp_Upload4.PostedFile.SaveAs(Server.MapPath("~/CADIMAGE/") + fileName.Replace(" ", ""));
                lblFile_Path4.Text = "~/CADIMAGE/" + fp_Upload4.PostedFile.FileName.Replace(" ", "");
                img_Photo4.ImageUrl = "~/CADIMAGE/" + fp_Upload4.PostedFile.FileName.Replace(" ", "");
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {

            if (ddlStyles.SelectedValue == "Select StyleNo")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check UoM Conversation.')", true);
                ddlStyles.Focus();
                return;
            }

            

            //if (txtUoMConversation.Text == "")
            //    txtUoMConversation.Text = "0";

            //if (Convert.ToDouble(txtUoMConversation.Text) == 0)
            //{
            //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check UoM Conversation.')", true);
            //    txtUoMConversation.Focus();
            //    return;
            //}

            DateTime Date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (btnSave.Text == "Save")
            {

                {
                    int iStatus = objBs.Insert_CADENTRY(Convert.ToInt32(ddlStyles.SelectedValue), Date, chkfit1.Checked, txtfit1notes.Text, lblFile_Path2.Text, chkfit2.Checked, txtfit2notes.Text, lblFile_Path3.Text, chkfit3.Checked, txtfit3notes.Text, lblFile_Path4.Text, lblFile_Path.Text, txtppdesc.Text, lblFile_Path1.Text, txtmarkerdesc.Text, lblUserID.Text);
                    Response.Redirect("CAD_Entry_Grid.aspx");
                }
            }
            else
            {
                int iUpdateStatus = objBs.Update_CADENTRY(Convert.ToInt32(ddlStyles.SelectedValue), Date, chkfit1.Checked, txtfit1notes.Text, lblFile_Path2.Text, chkfit2.Checked, txtfit2notes.Text, lblFile_Path3.Text, chkfit3.Checked, txtfit3notes.Text, lblFile_Path4.Text, lblFile_Path.Text, txtppdesc.Text, lblFile_Path1.Text, txtmarkerdesc.Text, lblUserID.Text, lblcadid.Text);
                Response.Redirect("CAD_Entry_Grid.aspx");
            }
            //else
            //{
            //    string ItemMasterId = Request.QueryString.Get("ItemMasterId");

            //    DataSet dsDesc = objBs.ItemMastersrchgrid(txtDescription.Text, Convert.ToInt32(ItemMasterId));
            //    if (dsDesc.Tables[0].Rows.Count > 0)
            //    {
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Description was already Exists.')", true);
            //        txtDescription.Focus();
            //        return;
            //    }
            //    else
            //    {
            //        int iStatus = objBs.updateItemMasterMaster(Convert.ToInt32(ddlItemTypeGroup.SelectedValue), Convert.ToInt32(ddlHead.SelectedValue), Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlItemCode.SelectedValue), txtDescription.Text, Convert.ToInt32(ddlSize.SelectedValue), Convert.ToInt32(ddlSizeUoM.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlOrderingUoM.SelectedValue), Convert.ToDouble(txtUoMConversation.Text), Convert.ToInt32(ddlIssuingUoM.SelectedValue), Date, ddlActive.SelectedValue, lblFile_Path.Text, Convert.ToInt32(ItemMasterId), txtreedpick.Text, txtquality.Text);
            //        Response.Redirect("CAD_Entry_Grid.aspx");
            //    }

            //}

        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CAD_Entry_Grid.aspx");
        }

      

        
        

        

        
        
        
      
      

      

    }
}
