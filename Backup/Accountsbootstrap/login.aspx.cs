using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class login : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        HttpCookie userInfo = new HttpCookie("userInfo");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["UserID"] = "";
                Session["UserName"] = "";
                Session["IsSuperAdmin"] = "";
                Session["User"] = "";
                Session["Mode"] = "";
                Session["Empid"] = "";
                Session["CmpyId"] = "";
                Session["YearCode"] = "";
            }
        }
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            Response.Cookies["UserName"].Value = username.Text.Trim();
            Response.Cookies["Password"].Value = password.Text.Trim();

            DataSet dsLogin = objBs.Login(username.Text, password.Text);
            if (dsLogin.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Login Failed, Please try Again!');", true);
                return;
            }
            Session["UserName"] = username.Text;
            string brnch1 = dsLogin.Tables[0].Rows[0]["DefaultBranch"].ToString();
            if (brnch1 == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Login Failed!!.Please Contact Administrator.You are Not a Valid User.');", true);
                return;
            }
            else
            {
                Session["User"] = dsLogin.Tables[0].Rows[0]["DefaultBranch"].ToString();
            }
            string exdate = Convert.ToDateTime(dsLogin.Tables[0].Rows[0]["Expireddate"]).ToString("yyyy-MM-dd");
            string curdate = DateTime.Now.ToString("yyyy-MM-dd");

            string onedate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string three = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime stdate1 = DateTime.ParseExact(exdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime curd = DateTime.ParseExact(curdate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime one = DateTime.ParseExact(onedate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime three1 = DateTime.ParseExact(three, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            double dayss = (stdate1 - three1).TotalDays;
            int days = Convert.ToInt32(dayss);
            bool iac = false;
            if (stdate1 == one)
            {
                iac = true;
            }
            if (days == 3)
            {
                iac = true;
            }
            else if (days == 2)
            {
                iac = true;
            }

            if (dsLogin.Tables[0].Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Login Failed, Please try Again!');", true);
            }
            else
            {
                Session["UserID"] = dsLogin.Tables[0].Rows[0]["UserID"].ToString();
                Session["UserName"] = username.Text;
                Session["IsSuperAdmin"] = dsLogin.Tables[0].Rows[0]["IsSuperAdmin"].ToString();
                Session["Empid"] = dsLogin.Tables[0].Rows[0]["empid"].ToString();
                Session["CmpyId"] = dsLogin.Tables[0].Rows[0]["Companyid"].ToString();
                Session["YearCode"] = dsLogin.Tables[0].Rows[0]["YearCode"].ToString();
                userInfo["YearCode"] = dsLogin.Tables[0].Rows[0]["YearCode"].ToString();
                Session["Yearid"] = "1";
                Response.Cookies.Add(userInfo);                  
                string sUser = dsLogin.Tables[0].Rows[0]["Sales"].ToString();
                string[] sUserDet = sUser.Split('_');
                string brnch = dsLogin.Tables[0].Rows[0]["DefaultBranch"].ToString();
                if (brnch == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Login Failed!!.Please Contact Administrator.You are Not a Valid User.');", true);
                    return;
                }
                else
                {
                    Session["User"] = dsLogin.Tables[0].Rows[0]["DefaultBranch"].ToString();
                }
                Session["Mode"] = "";

                Response.Redirect("../Accountsbootstrap/Home_Page.aspx");
            }

        }

        protected void Registration_Form(object sender, EventArgs e)
        {
            Response.Redirect("../Accountsbootstrap/RegistrationForm.aspx");
        }
    }
}