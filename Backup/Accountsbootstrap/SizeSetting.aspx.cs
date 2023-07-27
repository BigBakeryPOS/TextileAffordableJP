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
using System.Configuration;
using System.Data.SqlClient;


namespace Billing.Accountsbootstrap
{
    public partial class SizeSetting : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        string empid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            empid = Session["Empid"].ToString();

            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                ds = objBs.Getsizetypefull();
                SizeGrid.DataSource = ds;
                SizeGrid.DataBind();
            }
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Fitid"));
            dt.Columns.Add(new DataColumn("threesix"));
            dt.Columns.Add(new DataColumn("threetwo"));
            dt.Columns.Add(new DataColumn("fourFour"));
            dt.Columns.Add(new DataColumn("FiveFive"));
            dt.Columns.Add(new DataColumn("FiveSix"));
            dt.Columns.Add(new DataColumn("FiveSeven"));
            dt.Columns.Add(new DataColumn("FiveEight"));

            dt.Columns.Add(new DataColumn("FiveFour"));
            dt.Columns.Add(new DataColumn("ThreeZero"));
            dt.Columns.Add(new DataColumn("TwoSeven"));
            dt.Columns.Add(new DataColumn("SixTwo"));
            // string id = gv.DataKeys[e.RowIndex]["projectID"].ToString();
            
            for (int vLoop = 0; vLoop < SizeGrid.Rows.Count; vLoop++)
            {
                Label txttrans = (Label)SizeGrid.Rows[vLoop].FindControl("lblid");
                TextBox txtthreesix = (TextBox)SizeGrid.Rows[vLoop].FindControl("txtts");
                TextBox txt32 = (TextBox)SizeGrid.Rows[vLoop].FindControl("txt32");
                TextBox txtfoureight = (TextBox)SizeGrid.Rows[vLoop].FindControl("txtfe");

                TextBox txtfivefive = (TextBox)SizeGrid.Rows[vLoop].FindControl("txtff");
                TextBox txtfivesix = (TextBox)SizeGrid.Rows[vLoop].FindControl("txtfs");
                TextBox txtfiveseven = (TextBox)SizeGrid.Rows[vLoop].FindControl("txtfse");
                TextBox txtfiveeight = (TextBox)SizeGrid.Rows[vLoop].FindControl("txtfei");

                TextBox txt54 = (TextBox)SizeGrid.Rows[vLoop].FindControl("txt54");
                TextBox txt30 = (TextBox)SizeGrid.Rows[vLoop].FindControl("txt30");
                TextBox txt27 = (TextBox)SizeGrid.Rows[vLoop].FindControl("txt27");
                TextBox txt62 = (TextBox)SizeGrid.Rows[vLoop].FindControl("txt62");

                {
                    DataRow dr_final12 = dt.NewRow();
                    dr_final12["Fitid"] = txttrans.Text;
                    dr_final12["threesix"] = txtthreesix.Text;
                    dr_final12["threetwo"] = txt32.Text;
                    dr_final12["fourFour"] = txtfoureight.Text;
                    dr_final12["FiveFive"] = txtfivefive.Text;
                    dr_final12["FiveSix"] = txtfivesix.Text;
                    dr_final12["FiveSeven"] = txtfiveseven.Text;
                    dr_final12["FiveEight"] = txtfiveeight.Text;

                    dr_final12["FiveFour"] = txt54.Text;
                    dr_final12["ThreeZero"] = txt30.Text;
                    dr_final12["TwoSeven"] = txt27.Text;
                    dr_final12["SixTwo"] = txt62.Text;

                    dt.Rows.Add(dr_final12);
                    // DataSet ds = new DataSet();                 
                }

                int col = vLoop + 1;
            }
            ds.Tables.Add(dt);

            int idel = objBs.deletetsizesetting();

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string sizeid = ds.Tables[0].Rows[i]["Fitid"].ToString();
                    string threesix = ds.Tables[0].Rows[i]["threesix"].ToString();
                    string threetwo = ds.Tables[0].Rows[i]["threetwo"].ToString();
                    string foureight = ds.Tables[0].Rows[i]["fourFour"].ToString();
                    string fivefive = ds.Tables[0].Rows[i]["FiveFive"].ToString();
                    string fivesix = ds.Tables[0].Rows[i]["FiveSix"].ToString();
                    string fiveseven = ds.Tables[0].Rows[i]["FiveSeven"].ToString();
                    string fiveeight = ds.Tables[0].Rows[i]["FiveEight"].ToString();

                    string FiveFour = ds.Tables[0].Rows[i]["FiveFour"].ToString();
                    string ThreeZero = ds.Tables[0].Rows[i]["ThreeZero"].ToString();
                    string TwoSeven = ds.Tables[0].Rows[i]["TwoSeven"].ToString();
                    string SixTwo = ds.Tables[0].Rows[i]["SixTwo"].ToString();

                    int isce = objBs.insertsizesetting(sizeid, threesix, foureight, fivefive, fivesix, fiveseven, fiveeight, empid, FiveFour, ThreeZero, TwoSeven, SixTwo,threetwo);
                }
            }
            Response.Redirect("Sizesetting.aspx");
        }
    }
}