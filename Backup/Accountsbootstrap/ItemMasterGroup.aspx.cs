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
    public partial class ItemMasterGroup : System.Web.UI.Page
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

                DataSet dsset = objBs.griditemgroup();
                if (dsset.Tables[0].Rows.Count > 0)
                {
                    ddlItemTypeGroup.DataSource = dsset.Tables[0];
                    ddlItemTypeGroup.DataTextField = "Itemgroupname";
                    ddlItemTypeGroup.DataValueField = "ItemgroupId";
                    ddlItemTypeGroup.DataBind();
                    ddlItemTypeGroup.Items.Insert(0, "Select ItemGroup");
                }
                DataSet dsCategory = objBs.gridCategory();
                if (dsCategory.Tables[0].Rows.Count > 0)
                {
                    ddlCategory.DataSource = dsCategory.Tables[0];
                    ddlCategory.DataTextField = "Category";
                    ddlCategory.DataValueField = "CategoryID";
                    ddlCategory.DataBind();
                }

                DataSet dstax = objBs.SelectTax();
                if (dstax.Tables[0].Rows.Count > 0)
                {
                    ddltax.DataSource = dstax.Tables[0];
                    ddltax.DataTextField = "tax";
                    ddltax.DataValueField = "taxid";
                    ddltax.DataBind();
                    ddltax.Items.Insert(0, "Select GST");
                    //ddlcategory.Items.Insert(0, "Select Category");
                }



                DataSet dsUOM = objBs.getUOM();
                if (dsUOM.Tables[0].Rows.Count > 0)
                {
                    ddlSizeUoM.DataSource = dsUOM.Tables[0];
                    ddlSizeUoM.DataTextField = "Units";
                    ddlSizeUoM.DataValueField = "UOMID";
                    ddlSizeUoM.DataBind();

                    ddlOrderingUoM.DataSource = dsUOM.Tables[0];
                    ddlOrderingUoM.DataTextField = "Units";
                    ddlOrderingUoM.DataValueField = "UOMID";
                    ddlOrderingUoM.DataBind();

                    ddlIssuingUoM.DataSource = dsUOM.Tables[0];
                    ddlIssuingUoM.DataTextField = "Units";
                    ddlIssuingUoM.DataValueField = "UOMID";
                    ddlIssuingUoM.DataBind();
                }


                DataSet dsWidth = objBs.selectsize();
                if (dsWidth.Tables[0].Rows.Count > 0)
                {
                    ddlSize.DataSource = dsWidth.Tables[0];
                    ddlSize.DataTextField = "Size";
                    ddlSize.DataValueField = "SizeId";
                    ddlSize.DataBind();
                }

                ddlHead.Items.Insert(0, "ItemHead");
                ddlItemName.Items.Insert(0, "ItemName");
                ddlItemCode.Items.Insert(0, "ItemCode");

                #endregion

                string ItemMasterId = Request.QueryString.Get("ItemMasterId");
                if (ItemMasterId != "" || ItemMasterId != null)
                {
                    DataSet dsItemMaster = objBs.getiItemMastervalues(Convert.ToInt32(ItemMasterId));
                    if (dsItemMaster.Tables[0].Rows.Count > 0)
                    {
                        #region

                        ddlItemTypeGroup.SelectedValue = dsItemMaster.Tables[0].Rows[0]["ItemGroupId"].ToString();
                        #region

                        DataSet ds = objBs.GetItemGroupandHead(Convert.ToInt32(ddlItemTypeGroup.SelectedValue));
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ddlHead.DataSource = ds.Tables[0];
                            ddlHead.DataTextField = "Category";
                            ddlHead.DataValueField = "Categoryid";
                            ddlHead.DataBind();
                        }
                        else
                        {
                            ddlHead.Items.Insert(0, "ItemHead");
                        }

                        DataSet dsItem = objBs.getiItemType(Convert.ToInt32(ddlItemTypeGroup.SelectedValue));
                        if (dsItem.Tables[0].Rows.Count > 0)
                        {
                            ddlItemName.DataSource = dsItem.Tables[0];
                            ddlItemName.DataTextField = "ItemDescription";
                            ddlItemName.DataValueField = "ItemId";
                            ddlItemName.DataBind();
                            ddlItemName.Items.Insert(0, "ItemName");

                            ddlItemCode.Items.Clear();
                            ddlItemCode.DataSource = dsItem.Tables[0];
                            ddlItemCode.DataTextField = "ItemCode";
                            ddlItemCode.DataValueField = "ItemId";
                            ddlItemCode.DataBind();
                            ddlItemCode.Items.Insert(0, "ItemCode");
                        }
                        else
                        {
                            ddlItemName.Items.Insert(0, "ItemName");
                            ddlItemCode.Items.Insert(0, "ItemCode");
                        }
                        #endregion

                        ddlHead.SelectedValue = dsItemMaster.Tables[0].Rows[0]["ItemHeadId"].ToString();

                        ddlItemName.SelectedValue = dsItemMaster.Tables[0].Rows[0]["ItemNameId"].ToString();
                        ddlItemCode.SelectedValue = ddlItemName.SelectedValue;
                        ddltax.SelectedValue = dsItemMaster.Tables[0].Rows[0]["TaxID"].ToString();
                        txtHSNCode.Text = dsItemMaster.Tables[0].Rows[0]["HSNCode"].ToString();
                        ddlItemCode.SelectedValue = dsItemMaster.Tables[0].Rows[0]["ItemCodeId"].ToString();
                        txtDescription.Text = dsItemMaster.Tables[0].Rows[0]["Description"].ToString();
                        ddlSize.SelectedValue = dsItemMaster.Tables[0].Rows[0]["SizeId"].ToString();
                        ddlSizeUoM.SelectedValue = dsItemMaster.Tables[0].Rows[0]["SizeUoMId"].ToString();
                        ddlCategory.SelectedValue = dsItemMaster.Tables[0].Rows[0]["CategoryId"].ToString();
                        ddlOrderingUoM.SelectedValue = dsItemMaster.Tables[0].Rows[0]["OrderingUoMId"].ToString();
                        txtUoMConversation.Text = dsItemMaster.Tables[0].Rows[0]["UoMConversation"].ToString();
                        ddlIssuingUoM.SelectedValue = dsItemMaster.Tables[0].Rows[0]["IssuingUoMId"].ToString();
                        txtDate.Text = Convert.ToDateTime(dsItemMaster.Tables[0].Rows[0]["Date"]).ToString("dd/MM/yyyy");
                        ddlActive.SelectedValue = dsItemMaster.Tables[0].Rows[0]["IsActive"].ToString();

                        lblFile_Path.Text = dsItemMaster.Tables[0].Rows[0]["ItemImage"].ToString();
                        img_Photo.ImageUrl = dsItemMaster.Tables[0].Rows[0]["ItemImage"].ToString();

                        txtreedpick.Text = dsItemMaster.Tables[0].Rows[0]["ReedPick"].ToString();
                        txtquality.Text = dsItemMaster.Tables[0].Rows[0]["Quality"].ToString();

                        btnSave.Text = "Update";

                        #endregion
                    }
                }
            }
        }

        protected void btnSizeRefresh_OnClick(object sender, EventArgs e)
        {
            ddlSize.Items.Clear();

            DataSet dssize = objBs.selectsize();
            if (dssize.Tables[0].Rows.Count > 0)
            {
                ddlSize.DataSource = dssize.Tables[0];
                ddlSize.DataTextField = "Size";
                ddlSize.DataValueField = "SizeId";
                ddlSize.DataBind();
            }

        }

        protected void ddlItemTypeGroup_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ddlHead.Items.Clear();
            ddlItemName.Items.Clear();
            ddlItemCode.Items.Clear();

            if (ddlItemTypeGroup.SelectedValue == "" || ddlItemTypeGroup.SelectedValue == "0" || ddlItemTypeGroup.SelectedValue == "Select ItemGroup")
            {
                ddlHead.Items.Insert(0, "ItemHead");
                ddlItemName.Items.Insert(0, "ItemName");
                ddlItemCode.Items.Insert(0, "ItemCode");
            }
            else
            {
                DataSet ds = objBs.GetItemGroupandHead(Convert.ToInt32(ddlItemTypeGroup.SelectedValue));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddlHead.DataSource = ds.Tables[0];
                    ddlHead.DataTextField = "Category";
                    ddlHead.DataValueField = "Categoryid";
                    ddlHead.DataBind();
                }
                else
                {
                    ddlHead.Items.Insert(0, "ItemHead");
                }

                DataSet dsItem = objBs.getiItemType(Convert.ToInt32(ddlItemTypeGroup.SelectedValue));
                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    ddlItemName.DataSource = dsItem.Tables[0];
                    ddlItemName.DataTextField = "ItemDescription";
                    ddlItemName.DataValueField = "ItemId";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, "ItemName");

                    ddlItemCode.Items.Clear();
                    ddlItemCode.DataSource = dsItem.Tables[0];
                    ddlItemCode.DataTextField = "ItemCode";
                    ddlItemCode.DataValueField = "ItemId";
                    ddlItemCode.DataBind();
                    ddlItemCode.Items.Insert(0, "ItemCode");
                }
                else
                {
                    ddlItemName.Items.Insert(0, "ItemName");
                    ddlItemCode.Items.Insert(0, "ItemCode");
                }
            }

        }

        protected void ddlItemName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string Description = "";
            if (ddlItemName.SelectedValue == "" || ddlItemName.SelectedValue == "0" || ddlItemName.SelectedValue == "ItemName")
            {
                ddlItemCode.ClearSelection();
            }
            else
            {
                ddlItemCode.SelectedValue = ddlItemName.SelectedValue;
               // Description = ddlItemName.SelectedItem.Text + " " + ddlSize.SelectedItem.Text + " " + ddlSizeUoM.SelectedItem.Text + " " + ddlCategory.SelectedItem.Text;
                Description = ddlItemName.SelectedItem.Text + " " + txtreedpick.Text + " " + txtquality.Text + " " + ddlSize.SelectedItem.Text + " " + ddlSizeUoM.SelectedItem.Text + " " + ddlCategory.SelectedItem.Text;
            }
            txtDescription.Text = Description;

            //if (ddlItemTypeGroup.SelectedValue != "" && ddlItemTypeGroup.SelectedValue != "0" && ddlItemTypeGroup.SelectedValue != "Select ItemGroup")
            //{
            //    Description = ddlItemTypeGroup.SelectedItem.Text + " " + ddlSize.SelectedItem.Text + " " + ddlSizeUoM.SelectedItem.Text + " " + ddlCategory.SelectedItem.Text;
            //}
            //if (ddlSize.SelectedValue != "" && ddlSize.SelectedValue != "0" && ddlSize.SelectedValue != "Select ItemGroup")
            //{
            //    txtDescription.Text = ddlItemTypeGroup.Text;
            //}
            //if (ddlSizeUoM.SelectedValue != "" && ddlSizeUoM.SelectedValue != "0" && ddlSizeUoM.SelectedValue != "Select ItemGroup")
            //{
            //    txtDescription.Text = ddlItemTypeGroup.Text;
            //}
            //if (ddlCategory.SelectedValue != "" && ddlCategory.SelectedValue != "0" && ddlCategory.SelectedValue != "Select ItemGroup")
            //{
            //    txtDescription.Text = ddlItemTypeGroup.Text;
            //}


        }

        protected void btnUpload_OnClick(object sender, EventArgs e)
        {
            if (fp_Upload.HasFile)
            {
                string fileName = Path.GetFileName(fp_Upload.PostedFile.FileName);
                fp_Upload.PostedFile.SaveAs(Server.MapPath("~/Sampling/") + fileName.Replace(" ", ""));
                lblFile_Path.Text = "~/Sampling/" + fp_Upload.PostedFile.FileName.Replace(" ", "");
                img_Photo.ImageUrl = "~/Sampling/" + fp_Upload.PostedFile.FileName.Replace(" ", "");
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {

            if (txtUoMConversation.Text == "")
                txtUoMConversation.Text = "0";

            if (Convert.ToDouble(txtUoMConversation.Text) == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('Please Check UoM Conversation.')", true);
                txtUoMConversation.Focus();
                return;
            }

            DateTime Date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (btnSave.Text == "Save")
            {
                DataSet dsDesc = objBs.ItemMastersrchgrid(txtDescription.Text, 0);
                if (dsDesc.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Description was already Exists.')", true);
                    txtDescription.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.InsertItemMaster(Convert.ToInt32(ddlItemTypeGroup.SelectedValue), Convert.ToInt32(ddlHead.SelectedValue), Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlItemCode.SelectedValue), txtDescription.Text, Convert.ToInt32(ddlSize.SelectedValue), Convert.ToInt32(ddlSizeUoM.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlOrderingUoM.SelectedValue), Convert.ToDouble(txtUoMConversation.Text), Convert.ToInt32(ddlIssuingUoM.SelectedValue), Date, ddlActive.SelectedValue, lblFile_Path.Text,txtreedpick.Text,txtquality.Text,txtHSNCode.Text,Convert.ToInt32(ddltax.SelectedValue));
                    Response.Redirect("ItemMasterGroupGrid.aspx");
                }
            }
            else
            {
                string ItemMasterId = Request.QueryString.Get("ItemMasterId");

                DataSet dsDesc = objBs.ItemMastersrchgrid(txtDescription.Text, Convert.ToInt32(ItemMasterId));
                if (dsDesc.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('This Description was already Exists.')", true);
                    txtDescription.Focus();
                    return;
                }
                else
                {
                    int iStatus = objBs.updateItemMasterMaster(Convert.ToInt32(ddlItemTypeGroup.SelectedValue), Convert.ToInt32(ddlHead.SelectedValue), Convert.ToInt32(ddlItemName.SelectedValue), Convert.ToInt32(ddlItemCode.SelectedValue), txtDescription.Text, Convert.ToInt32(ddlSize.SelectedValue), Convert.ToInt32(ddlSizeUoM.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), Convert.ToInt32(ddlOrderingUoM.SelectedValue), Convert.ToDouble(txtUoMConversation.Text), Convert.ToInt32(ddlIssuingUoM.SelectedValue), Date, ddlActive.SelectedValue, lblFile_Path.Text, Convert.ToInt32(ItemMasterId), txtreedpick.Text, txtquality.Text,txtHSNCode.Text,Convert.ToInt32(ddltax.SelectedValue));
                    Response.Redirect("ItemMasterGroupGrid.aspx");
                }

            }

        }
        protected void btnExit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("ItemMasterGroupGrid.aspx");
        }

        protected void btnItemTypeGroup_OnClick(object sender, EventArgs e)
        {
            string yourUrl = "Itemgroup.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow1", "window.open('" + yourUrl + "');", true);
        }
        protected void btnItemTypeGroupRef_OnClick(object sender, EventArgs e)
        {
            DataSet dsset = objBs.griditemgroup();
            if (dsset.Tables[0].Rows.Count > 0)
            {
                ddlItemTypeGroup.DataSource = dsset.Tables[0];
                ddlItemTypeGroup.DataTextField = "Itemgroupname";
                ddlItemTypeGroup.DataValueField = "ItemgroupId";
                ddlItemTypeGroup.DataBind();
                ddlItemTypeGroup.Items.Insert(0, "Select ItemGroup");
            }

            ddlHead.Items.Clear();
            ddlItemName.Items.Clear();
            ddlItemCode.Items.Clear();

            ddlHead.Items.Insert(0, "ItemHead");
            ddlItemName.Items.Insert(0, "ItemName");
            ddlItemCode.Items.Insert(0, "ItemCode");
        }

        protected void ItemName_OnClick(object sender, EventArgs e)
        {
            string yourUrl = "ItemType.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow1", "window.open('" + yourUrl + "');", true);
        }
        protected void ItemNameRef_OnClick(object sender, EventArgs e)
        {

            ddlItemName.Items.Clear();
            ddlItemCode.Items.Clear();

            if (ddlItemTypeGroup.SelectedValue == "" || ddlItemTypeGroup.SelectedValue == "0" || ddlItemTypeGroup.SelectedValue == "Select ItemGroup")
            {
                ddlItemName.Items.Insert(0, "ItemName");
                ddlItemCode.Items.Insert(0, "ItemCode");
            }
            else
            {
                DataSet dsItem = objBs.getiItemType(Convert.ToInt32(ddlItemTypeGroup.SelectedValue));
                if (dsItem.Tables[0].Rows.Count > 0)
                {
                    ddlItemName.DataSource = dsItem.Tables[0];
                    ddlItemName.DataTextField = "ItemDescription";
                    ddlItemName.DataValueField = "ItemId";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, "ItemName");

                    ddlItemCode.Items.Clear();
                    ddlItemCode.DataSource = dsItem.Tables[0];
                    ddlItemCode.DataTextField = "ItemCode";
                    ddlItemCode.DataValueField = "ItemId";
                    ddlItemCode.DataBind();
                    ddlItemCode.Items.Insert(0, "ItemCode");
                }
                else
                {
                    ddlItemName.Items.Insert(0, "ItemName");
                    ddlItemCode.Items.Insert(0, "ItemCode");
                }
            }
        }

        protected void btnCategory_OnClick(object sender, EventArgs e)
        {
            string yourUrl = "Category.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow1", "window.open('" + yourUrl + "');", true);
        }
        protected void btnCategoryRef_OnClick(object sender, EventArgs e)
        {
            ddlCategory.Items.Clear();

            DataSet dsCategory = objBs.gridCategory();
            if (dsCategory.Tables[0].Rows.Count > 0)
            {
                ddlCategory.DataSource = dsCategory.Tables[0];
                ddlCategory.DataTextField = "Category";
                ddlCategory.DataValueField = "CategoryID";
                ddlCategory.DataBind();
            }

        }

        protected void btnUoM_OnClick(object sender, EventArgs e)
        {
            string yourUrl = "UoM.aspx";
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow1", "window.open('" + yourUrl + "');", true);
        }
        protected void btnSizeUoMRef_OnClick(object sender, EventArgs e)
        {
            DataSet dsUOM = objBs.getUOM();
            if (dsUOM.Tables[0].Rows.Count > 0)
            {
                ddlSizeUoM.DataSource = dsUOM.Tables[0];
                ddlSizeUoM.DataTextField = "Units";
                ddlSizeUoM.DataValueField = "UOMID";
                ddlSizeUoM.DataBind();
            }
        }
        protected void btnOrderingUoMRef_OnClick(object sender, EventArgs e)
        {
            DataSet dsUOM = objBs.getUOM();
            if (dsUOM.Tables[0].Rows.Count > 0)
            {
                ddlOrderingUoM.DataSource = dsUOM.Tables[0];
                ddlOrderingUoM.DataTextField = "Units";
                ddlOrderingUoM.DataValueField = "UOMID";
                ddlOrderingUoM.DataBind();
            }
        }
        protected void btnIssuingUoMMRef_OnClick(object sender, EventArgs e)
        {
            DataSet dsUOM = objBs.getUOM();
            if (dsUOM.Tables[0].Rows.Count > 0)
            {
                ddlIssuingUoM.DataSource = dsUOM.Tables[0];
                ddlIssuingUoM.DataTextField = "Units";
                ddlIssuingUoM.DataValueField = "UOMID";
                ddlIssuingUoM.DataBind();
            }
        }

        protected void Description_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string Description = "";
            if (ddlItemName.SelectedValue != "" && ddlItemName.SelectedValue != "0" && ddlItemName.SelectedValue != "ItemName")
            {
               // Description = ddlItemName.SelectedItem.Text + " " + ddlSize.SelectedItem.Text + " " + ddlSizeUoM.SelectedItem.Text + " " + ddlCategory.SelectedItem.Text + " " +txtreedpick.Text +" "+ txtquality.Text;
                Description = ddlItemName.SelectedItem.Text + " " + txtreedpick.Text + " " + txtquality.Text + " " + ddlSize.SelectedItem.Text + " " + ddlSizeUoM.SelectedItem.Text + " " + ddlCategory.SelectedItem.Text;
            }
            txtDescription.Text = Description;

        }


        protected void Reed_Pick(object sender, EventArgs e)
        {
            string Description = "";
            if (ddlItemName.SelectedValue != "" && ddlItemName.SelectedValue != "0" && ddlItemName.SelectedValue != "ItemName")
            {
              //  Description = ddlItemName.SelectedItem.Text + " " + ddlSize.SelectedItem.Text + " " + ddlSizeUoM.SelectedItem.Text + " " + ddlCategory.SelectedItem.Text + " " + txtreedpick.Text + " " + txtquality.Text;
                Description = ddlItemName.SelectedItem.Text + " " + txtreedpick.Text + " " + txtquality.Text + " " + ddlSize.SelectedItem.Text + " " + ddlSizeUoM.SelectedItem.Text + " " + ddlCategory.SelectedItem.Text;
            }
            txtDescription.Text = Description;

            txtquality.Focus();

        }


        protected void Quality_Chnaged(object sender, EventArgs e)
        {
            string Description = "";
            if (ddlItemName.SelectedValue != "" && ddlItemName.SelectedValue != "0" && ddlItemName.SelectedValue != "ItemName")
            {
                Description = ddlItemName.SelectedItem.Text + " " + txtreedpick.Text + " " + txtquality.Text + " " + ddlSize.SelectedItem.Text + " " + ddlSizeUoM.SelectedItem.Text + " " + ddlCategory.SelectedItem.Text;
            }
            txtDescription.Text = Description;
            txtreedpick.Focus();
        }

    }
}
