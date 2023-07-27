using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Net.Mail;



namespace Billing.Accountsbootstrap
{
    public partial class StockReport : System.Web.UI.Page
    {


        BSClass objbs = new BSClass();
        string sTableName = "";



        protected void Page_Load(object sender, EventArgs e)
        {
            lblUser.Text = Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            sTableName = Session["User"].ToString();




            if (!IsPostBack)
            {
                DataSet dcategory = objbs.selectCATnew();
                if (dcategory.Tables[0].Rows.Count > 0)
                {
                    ddlcategory.DataSource = dcategory.Tables[0];
                    ddlcategory.DataTextField = "category";
                    ddlcategory.DataValueField = "categoryid";
                    ddlcategory.DataBind();
                    ddlcategory.Items.Insert(0, "ALL");

                    ddlItem.Items.Insert(0, "ALL");
                }

                ddlcategory_OnSelectedIndexChanged(sender, e);


                var tab = new[] { tab0, tab1, tab2, tab3, tab4, tab5, tab6, tab7, tab8, tab9, tab10, tab11, tab12, tab13, tab14, tab15, tab15, tab15 };
                var itm = new[] { lblitem, lblitem1, lblitem2, lblitem3, lblitem4, lblitem5, lblitem6, lblitem7, lblitem8, lblitem9, lblitem10, lblitem11, lblitem12, lblitem13, lblitem14, lblitem15, lblitem15, lblitem15 };
                var Grid = new[] { gvGateaux, gvSnacks, gvPuddings, gvBeverages, gvSweets, gvcandles, gvMousse, gvCookies, gvcheese, gvStores, gvBday, gvbread, gvSponges, gvReadySp, gvRmCake, gvIce, gvIce, gvIce };

                DataSet dcat = objbs.selectCAT();

                for (int i = 0; i < dcat.Tables[0].Rows.Count; i++)
                // for (int i = 0; i < 15; i++)
                {
                    tab[i].Style.Add("visibility", "visible");
                    itm[i].InnerText = dcat.Tables[0].Rows[i]["category"].ToString();

                    DataSet dGateaux = objbs.DalilystockRequest(int.Parse(dcat.Tables[0].Rows[i]["categoryid"].ToString()), sTableName);
                    Grid[i].DataSource = dGateaux;
                    Grid[i].DataBind();


                    //foreach (GridViewRow gr in Grid[i].Rows)
                    //{

                    //    DataSet dCategory = objbs.getUOM();




                    //    TextBox txtqty = (TextBox)gr.FindControl("txtQty");
                    //    int itemid = Convert.ToInt32(gr.Cells[3].Text);

                    //    DropDownList Units = (DropDownList)gr.FindControl("ddUnits");
                    //    if (dCategory.Tables[0].Rows.Count > 0)
                    //    {
                    //        Units.DataSource = dCategory.Tables[0];
                    //        Units.DataTextField = "Units";
                    //        Units.DataValueField = "UOMID";
                    //        Units.DataBind();
                    //        Units.Items.Insert(0, "uom");
                    //    }

                    //    Units.SelectedValue = gr.Cells[5].Text;

                    //    //decimal avl = Convert.ToDecimal(gr.Cells[4].Text);
                    //    //DataSet ds = objbs.setQty(itemid, Convert.ToInt32(lblUserID.Text));
                    //    //if (ds.Tables[0].Rows.Count > 0)
                    //    //{
                    //    //    decimal dmin = Convert.ToDecimal(ds.Tables[0].Rows[0]["MinQty"].ToString());
                    //    //    decimal set = Convert.ToDecimal(ds.Tables[0].Rows[0]["FixQty"].ToString());


                    //    //    if (avl <= dmin)
                    //    //    {
                    //    //        gr.BackColor = System.Drawing.Color.Yellow;
                    //    //        txtqty.Text = set.ToString("f0");
                    //    //    }
                    //    //}
                    //}
                }


            }

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), "$('.chzn-select').chosen(); $('.chzn-select-deselect').chosen({ allow_single_deselect: true });", true);
        }

        protected void ddlcategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlcategory.SelectedValue != "ALL")
            {
                DataSet itemscategory = objbs.selectCATusernew(Convert.ToInt32(ddlcategory.SelectedValue));
                if (itemscategory.Tables[0].Rows.Count > 0)
                {
                    ddlItem.DataSource = itemscategory.Tables[0];
                    ddlItem.DataTextField = "Definition";
                    ddlItem.DataValueField = "CategoryUserID";
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, "ALL");
                }
                else
                {
                    ddlItem.Items.Clear();
                    ddlItem.Items.Insert(0, "ALL");
                }
            }

            else
            {

                DataSet itemscategory = objbs.selectCATusernewall();
                if (itemscategory.Tables[0].Rows.Count > 0)
                {
                    ddlItem.DataSource = itemscategory.Tables[0];
                    ddlItem.DataTextField = "Definition";
                    ddlItem.DataValueField = "CategoryUserID";
                    ddlItem.DataBind();
                    ddlItem.Items.Insert(0, "ALL");
                }
                else
                {
                    ddlItem.Items.Clear();
                    ddlItem.Items.Insert(0, "ALL");
                }
            }
            DataSet dsselectItemsRep = objbs.selectItemsRep(ddlcategory.SelectedValue, ddlItem.SelectedValue, sTableName);
            if (dsselectItemsRep.Tables[0].Rows.Count > 0)
            {
                gvcategoryiyems.DataSource = dsselectItemsRep;
                gvcategoryiyems.DataBind();

            }
            else
            {
                gvcategoryiyems.DataSource = null;
                gvcategoryiyems.DataBind();
            }
        }

        protected void ddlItem_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsselectItemsRep = objbs.selectItemsRep(ddlcategory.SelectedValue, ddlItem.SelectedValue, sTableName);
            if (dsselectItemsRep.Tables[0].Rows.Count > 0)
            {
                gvcategoryiyems.DataSource = dsselectItemsRep;
                gvcategoryiyems.DataBind();

            }
        }

        protected void btnvalue_Click(object sender, EventArgs e)
        {

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

        }


        #region SendMail Attachment

        public void SendHTMLMail()
        {
            MailMessage Msg = new MailMessage();
            MailAddress fromMail = new MailAddress("bigdbiz@gmail.com");//("administrator@aspdotnet-suresh.com");
            // Sender e-mail address.
            Msg.From = fromMail;
            // Recipient e-mail address.
            Msg.To.Add(new MailAddress("rajar@bigdbiz.in"));
            // Subject of e-mail
            Msg.Subject = "Send Gridivew in EMail";
            Msg.Body += "Please check below data <br/><br/>";

            Msg.Body += "Request From <br/><br/>";
            Msg.Body += "Request No  below data <br/><br/>";
            Msg.Body += "Request Date <br/><br/>";
            Msg.Body += "Request Entry <br/><br/>";

            Msg.Body += GetGridviewData(gvUserInfo);
            Msg.IsBodyHtml = true;
            //////string sSmtpServer = "";
            //////sSmtpServer = "587";
            //////SmtpClient a = new SmtpClient();
            //////a.Host = sSmtpServer;
            //////a.EnableSsl = true;
            //////a.Send(Msg);


            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["SmtpHostName"];
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["FromAddress"];
            NetworkCred.Password = ConfigurationManager.AppSettings["FromPassword"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["SmtpPortNumber"]);
            smtp.Send(Msg);
        }
        // This Method is used to render gridview control
        public string GetGridviewData(GridView gv)
        {
            StringBuilder strBuilder = new StringBuilder();
            System.IO.StringWriter strWriter = new System.IO.StringWriter(strBuilder);
            HtmlTextWriter htw = new HtmlTextWriter(strWriter);
            gv.RenderControl(htw);
            return strBuilder.ToString();
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        #endregion

        protected void ddlvendor_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void gvGateaux_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvSnacks_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvPuddings_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvBeverages_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvSweets_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvcandles_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvMousse_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvCookies_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvcheese_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvStores_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvBday_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvbread_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvSponges_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvEggless_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnexit_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchaseRequestGrid.aspx");
        }
    }
}