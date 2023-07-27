using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BusinessLayer;
using DataLayer;
using System.Text;
using System.Data;
namespace Billing.HeaderMaster
{
    public partial class Header : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BSClass objbs = new BSClass();
            string Companyid = "";
            string sUserChk = Session["IsSuperAdmin"].ToString();

            lblUser.Text = "WELCOME : " + " "+Session["UserName"].ToString();
            lblUserID.Text = Session["UserID"].ToString();
            Label1.Text = "DATE : " + DateTime.Now.ToString("dd/MM/yyyy");


            Companyid = Session["CmpyId"].ToString();
           
            DataSet ds1 = objbs.GetSelectLedgerDetails(Convert.ToInt32(Companyid));
            if (ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows[0]["ImagePath"].ToString() != "")
                {
                    imglogo.Src = ds1.Tables[0].Rows[0]["ImagePath"].ToString();
                }
                else
                {
                    imglogo.Src = "";
                }
           
            }

            DataSet ds = objbs.getuseraccess(lblUserID.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string screen = ds.Tables[0].Rows[i]["screencode"].ToString();

                    #region DashBoard
                    if (screen == "dashboard")
                    {
                        dashboard.Visible = true;
                    }
                    #endregion

                    #region MASTER
                    if (screen == "companymaster")
                    {
                        MMaster.Visible = true;
                        //companymaster.Visible = true;
                    }

                    else if (screen == "godownmaster")
                    {
                        MMaster.Visible = true;
                        godownmaster.Visible = true;
                    }

                    else if (screen == "itemhead")
                    {
                        MMaster.Visible = true;
                        itemhead.Visible = true;
                    }

                    else if (screen == "itemgroup")
                    {
                        MMaster.Visible = true;
                        itemgroup.Visible = true;
                    }

                    else if (screen == "itemtype")
                    {
                        MMaster.Visible = true;
                        itemtype.Visible = true;
                    }

                    else if (screen == "measurement")
                    {
                        MMaster.Visible = true;
                        measurement.Visible = true;
                    }

                    else if (screen == "color")
                    {
                        MMaster.Visible = true;
                        color.Visible = true;
                    }

                    else if (screen == "size")
                    {
                        MMaster.Visible = true;
                        size.Visible = true;
                    }

                    else if (screen == "sizerange")
                    {
                        MMaster.Visible = true;
                        sizerange.Visible = true;
                    }

                    else if (screen == "process")
                    {
                        MMaster.Visible = true;
                        process.Visible = true;
                    }

                    else if (screen == "partytype")
                    {
                        MMaster.Visible = true;
                        partytype.Visible = true;
                    }


                    else if (screen == "partymaster")
                    {
                        MMaster.Visible = true;
                        partymaster.Visible = true;
                    }

                    else if (screen == "Category")
                    {
                        MMaster.Visible = true;
                        Category.Visible = true;
                    }

                    else if (screen == "CurrencyMaster")
                    {
                        MMaster.Visible = true;
                        CurrencyMaster.Visible = true;
                    }

                    else if (screen == "deptmaster")
                    {
                        MMaster.Visible = true;
                        deptmaster.Visible = true;
                    }

                    else if (screen == "desg")
                    {
                        MMaster.Visible = true;
                        desg.Visible = true;
                    }

                    else if (screen == "empmaster")
                    {
                        MMaster.Visible = true;
                        empmaster.Visible = true;
                    }
                    #endregion

                    #region PRocess Screen
                    else if (screen == "Leadgenerate")
                    {
                        MMaster.Visible = true;
                        //Leadgenerate.Visible = true;
                    }

                    else if (screen == "ItemMasterGroup")
                    {
                        Msampandcost.Visible = true;
                        ItemMasterGroup.Visible = true;
                    }

                    else if (screen == "SamplingandCosting")
                    {
                        Msampandcost.Visible = true;
                        SamplingandCosting.Visible = true;
                    }

                    else if (screen == "cadentry")
                    {
                        Msampandcost.Visible = true;
                        cadentry.Visible = true;
                    }
                    #endregion

                    #region TIcket Process
                    else if (screen == "RaiseTicket")
                    {
                        //Mticket.Visible = true;
                        //RaiseTicket.Visible = true;
                    }

                    else if (screen == "TicketAdmin")
                    {
                        //Mticket.Visible = true;
                        //TicketAdmin.Visible = true;
                    }

                    #endregion

                    #region Approval Rights
                    else if (screen == "partyapproval")
                    {
                        Mapproval.Visible = true;
                        partyapproval.Visible = true;
                    }

                    else if (screen == "costingapproval")
                    {
                        //Mapproval.Visible = true;
                        //costingapproval.Visible = true;
                    }

                    #endregion

                    #region Order Prrocess

                    else if (screen == "BuyerOrderMaster")
                    {
                        //MorderProcess.Visible = true;
                        //BuyerOrderMaster.Visible = true;
                    }

                    else if (screen == "requirementsheet")
                    {
                        //MorderProcess.Visible = true;
                        //requirementsheet.Visible = true;
                    }

                    #endregion

                    #region OPSTOCK

                    else if (screen == "MaterialOpstock")
                    {
                        //Mopstock.Visible = true;
                        //MaterialOpstock.Visible = true;
                    }

                    else if (screen == "ExcOpstock")
                    {
                        //Mopstock.Visible = true;
                        //ExcOpstock.Visible = true;
                    }

                    #endregion

                    #region POSCREEN

                    else if (screen == "Poorder")
                    {
                        Mposcreen.Visible = true;
                        Poorder.Visible = true;
                    }

                    else if (screen == "poGrn")
                    {
                        Mposcreen.Visible = true;
                        poGrn.Visible = true;
                    }

                    #endregion

                    #region ITEM PROCESS

                    else if (screen == "StockTransfer")
                    {
                        //Mitemprocess.Visible = true;
                        //StockTransfer.Visible = true;
                    }
                    else if (screen == "Itemissue")
                    {
                        //Mitemprocess.Visible = true;
                        //Itemissue.Visible = true;
                    }

                    else if (screen == "Itemreceive")
                    {
                        //Mitemprocess.Visible = true;
                        //Itemreceive.Visible = true;
                    }

                    #endregion

                    #region Cuttin PRocess

                    else if (screen == "precutting")
                    {
                        McuttingProcess.Visible = true;
                        precutting.Visible = true;
                    }
                    else if (screen == "mastercutting")
                    {
                        McuttingProcess.Visible = true;
                        mastercutting.Visible = true;
                    }
                    else if (screen == "precutting")
                    {
                        ProductionOrder.Visible = true;
                        processentry.Visible = true;
                    }
                    else if (screen == "MaterialsIssue")
                    {
                        McuttingProcess.Visible = true;
                        MaterialsIssue.Visible = true;
                    }

                    #endregion

                    #region REPORTS

                    else if (screen == "BuyerOrderSales")
                    {
                        //BuyerOrderSales.Visible = true;
                    }

                    #endregion REPORTS

                    #region ACCOUNTS

                    else if (screen == "journal")
                    {
                        Maccounts.Visible = true;
                        journal.Visible = true;
                    }

                    else if (screen == "cheque")
                    {
                        Maccounts.Visible = true;
                        cheque.Visible = true;
                    }

                    //else if (screen == "CreditNote")
                    //{
                    //    Maccounts.Visible = true;
                    //    CreditNote.Visible = true;
                    //}

                    //else if (screen == "DebitNote")
                    //{
                    //    Maccounts.Visible = true;
                    //    DebitNote.Visible = true;
                    //}

                    #endregion ACCOUNTS



                    #region REPORTS

                    else if (screen == "ProductionStatus")
                    {
                        Mreport.Visible = true;
                        ProductionStatus.Visible = true;
                    }
                    else if (screen == "ChallanIn")
                    {
                        Challan.Visible = true;
                        Mreport.Visible = true;
                       // ChallanIn.Visible = true;
                    }
                    else if (screen == "ChallanOut")
                    {
                        Challan.Visible = true;
                        Mreport.Visible = true;
                       // ChallanOut.Visible = true;
                    }
                    else if (screen == "PartyWiseProductionStatus")
                    {
                        Mreport.Visible = true;
                        PartyWiseProductionStatus.Visible = true;
                    }

                    else if (screen == "Rcostingreport")
                    {
                        Mreport.Visible = true;
                        Rcostingreport.Visible = true;
                    }

                    else if (screen == "RNewcostingreport")
                    {
                        Mreport.Visible = true;
                        RNewcostingreport.Visible = true;
                    }

                    else if (screen == "Gstportalreport")
                    {
                        GstReport.Visible = true;
                        Mreport.Visible = true;
                        Gstportalreport.Visible = true;
                    }



                    else if (screen == "RBuyerorderreport")
                    {
                        BuyerOrderReport.Visible = true;
                        Mreport.Visible = true;
                        RBuyerorderreport.Visible = true;
                    }
                    else if (screen == "BuyerOrderDetails")
                    {
                        BuyerOrderReport.Visible = true;
                        Mreport.Visible = true;
                        BuyerOrderDetails.Visible = true;
                    }
                    else if (screen == "BuyerOrderPendingReport")
                    {
                        BuyerOrderReport.Visible = true;
                        Mreport.Visible = true;
                        BuyerOrderPendingReport.Visible = true;
                    }
                    else if (screen == "BuyerOrderBookingSummary")
                    {
                        BuyerOrderReport.Visible = true;
                        Mreport.Visible = true;
                        BuyerOrderBookingSummary.Visible = true;
                    }
                    else if (screen == "BuyerOrderBookingStyles")
                    {
                        BuyerOrderReport.Visible = true;
                        Mreport.Visible = true;
                        BuyerOrderBookingStyles.Visible = true;
                    }
                    else if (screen == "ItemProcessOrderEntryReport")
                    {
                        ItemProcessOrder.Visible = true;
                        Mreport.Visible = true;
                        ItemProcessOrderEntryReport.Visible = true;
                    }
                    else if (screen == "Ritemissue")
                    {
                        ItemProcessOrder.Visible = true;
                        Mreport.Visible = true;
                        Ritemissue.Visible = true;
                    }

                    else if (screen == "Ritemreceive")
                    {
                        ItemProcessOrder.Visible = true;
                        Mreport.Visible = true;
                        Ritemreceive.Visible = true;
                    }

                    else if (screen == "Rpoorder")
                    {
                        POandGRN.Visible = true;
                        Mreport.Visible = true;
                        Rpoorder.Visible = true;
                    }

                    else if (screen == "RpoGrn")
                    {
                        POandGRN.Visible = true;
                        Mreport.Visible = true;
                      //  RpoGrn.Visible = true;
                    }
                    else if (screen == "PurchaseOrderPendingReport")
                    {
                        POandGRN.Visible = true;
                        Mreport.Visible = true;
                        PurchaseOrderPendingReport.Visible = true;
                    }
                    else if (screen == "RequirementSheetDetails")
                    {
                        Mreport.Visible = true;
                        RequirementSheetDetails.Visible = true;
                    }
                    else if (screen == "SwatchSheet")
                    {
                        Mreport.Visible = true;
                        SwatchSheet.Visible = true;
                    }
                    else if (screen == "MaterialStockReport")
                    {
                        StockReport.Visible = true;
                        Mreport.Visible = true;
                        MaterialStockReport.Visible = true;
                    }
                    else if (screen == "StockLedger")
                    {
                        StockReport.Visible = true;
                        Mreport.Visible = true;
                        StockLedger.Visible = true;
                    }
                    else if (screen == "StockStatement")
                    {
                        StockReport.Visible = true;
                        Mreport.Visible = true;
                        StockStatement.Visible = true;
                    }

                    else if (screen == "CuttingDetails")
                    {
                        CuttingReport.Visible = true;
                        Mreport.Visible = true;
                       // CuttingDetails.Visible = true;
                    }
                    else if (screen == "CuttingQtyDetails")
                    {
                        CuttingReport.Visible = true;
                        Mreport.Visible = true;
                      //  CuttingQtyDetails.Visible = true;
                    }
                    else if (screen == "MasterCuttingDetailReport")
                    {
                        CuttingReport.Visible = true;
                        Mreport.Visible = true;
                       // MasterCuttingDetailReport.Visible = true;
                    }
                    else if (screen == "ProductionReport")
                    {
                        ProductionReports.Visible = true;
                        Mreport.Visible = true;
                        ProductionReport.Visible = true;
                    }
                    else if (screen == "ProductionDetailReport")
                    {
                        ProductionReports.Visible = true;
                        Mreport.Visible = true;
                        ProductionDetailReport.Visible = true;
                    }

                    #endregion

                    #region ADMIN

                    else if (screen == "usercreate")
                    {
                        MAdmn.Visible = true;
                        usercreate.Visible = true;
                    }

                    else if (screen == "usercreate")
                    {
                        MAdmn.Visible = true;
                        usercreate.Visible = true;
                    }

                    #endregion
                }
            }

            string sPageName = Path.GetFileNameWithoutExtension(Request.Path);//.HttpContext.Current.Request.ApplicationPath.
        }
    }
}