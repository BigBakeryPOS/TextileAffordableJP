using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Globalization;

namespace Billing.Accountsbootstrap
{
    public partial class StitchingDetails_Print : System.Web.UI.Page
    {
        BSClass objBs = new BSClass();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            string CutID = Request.QueryString.Get("CutID");
            if (!IsPostBack)
            {
                DataSet dsStitchingPrint = objBs.getStitchingDetailsPrint(Convert.ToInt32(CutID));
                if (dsStitchingPrint.Tables[0].Rows.Count > 0)
                {
                    lblProcessDate.Text = Convert.ToDateTime(dsStitchingPrint.Tables[0].Rows[0]["ProcessDate"]).ToString("dd/MM/yyyy");
                    lblLotNo.Text = dsStitchingPrint.Tables[0].Rows[0]["LotNo"].ToString();
                    lblBrand.Text = dsStitchingPrint.Tables[0].Rows[0]["BrandName"].ToString();
                    lblCuttingMaster.Text = dsStitchingPrint.Tables[0].Rows[0]["LedgerName"].ToString();
                    lblUnit.Text = dsStitchingPrint.Tables[0].Rows[0]["UnitName"].ToString();
                    lblTotalQty.Text = dsStitchingPrint.Tables[0].Rows[0]["TotalQuantity"].ToString();
                    lblFullQty.Text = dsStitchingPrint.Tables[0].Rows[0]["FullQty"].ToString();
                    lblHalfQty.Text = dsStitchingPrint.Tables[0].Rows[0]["HalfQty"].ToString();
                    lblKaja.Text = dsStitchingPrint.Tables[0].Rows[0]["IsKaja"].ToString();
                    lblEmb.Text = dsStitchingPrint.Tables[0].Rows[0]["IsEmbroiding"].ToString();
                    lblWhasing.Text = dsStitchingPrint.Tables[0].Rows[0]["IsWashing"].ToString();
                    gridPurchase.DataSource = dsStitchingPrint;
                    gridPurchase.DataBind();

                }
            }
        }
    }
}