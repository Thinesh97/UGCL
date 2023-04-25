using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLayer
{
    public class RequestForquotationBL
    {

        #region "Class: RequestForquotationBL Local Declarations"

        public enum eLoadSp
        {
            INSERT = 0,
            UPDATE = 1,
            SELECT_PO_ITEMS_BY_PONO = 2,
            SELECT_PO_ITEM_BY_ITEM_ID = 3,
            PRO_PURCHASE_ORDER_SELECT_ALL = 4,
            SELECT_PODETAILS_BY_PONO = 5,
            INSERT_PO_ITEM = 6,
            UPDATE_PO_ITEM = 7,
            INSERT_PO_TAX = 8,
            SELECT_PO_TAX_BY_POID = 9,
            UPDATE_PO_NETTOTAL = 10,
            DELETE_PO_ITEM_BY_ID = 11,
            DELETE_PO_TAX_BY_ID = 12,
            SELECT_PO_TAX_BY_TAXID = 13,
            UPDATE_PO_TAX = 14,
            POAWAITING_APPROVAL = 15,
            UPDATE_APPROVAL_POAWAITING = 16,
            PROC_SELECT_PO_CREATING_ITEMS = 17,
            SELECT_PO_ITEMWISE_TAX_BY_PO_ID = 18,
            PO_Delete = 19,
            UPDATE_PO_ITEM_QTY_BY_ID = 20,
            SELECT_EXISTING_QTY_OF_INDENT = 21,
            SELECT_FORTNIGHT_REPORT = 22,
            CHECK_PONO_IN_MRN = 23,
            SELECT_SECTORWISE_FORTNIGHT_REPORT = 24,
            SELECT_FORTNIGHT_REPORT_DATE_BASED = 25,
            SELECT_SECTORWISE_FORTNIGHT_REPORT_DATE_BASED = 26,
            SELECT_PO_ITEMWISE_TAX_BY_PO_ID_FOR_PRINT = 27,
            SELECT_PO_ITEMS_BY_PONO_FOR_PRINT = 28,
            SELECT_PO_TAX_BY_POID_FOR_PRINT = 29,
            SELECT_PO_ITEMS_ALL_BY_PROJECT = 30,
            SELECT_SECTORWISE_FORTNIGHT_REPORT_ALL = 31,
            SELECT_TRASPORT = 32,
            UPDATE_PO_ITEM_OVERALL_AMOUNT_BY_ID = 33,
            INSERT_DIRECT_RFQ_ITEM = 34,
            UPDATE_DIRECT_RFQ_ITEM = 35,
            DELETE_DIRECT_RFQ_ITEM = 36,
            INSERT_DIRECT_PO_TAX = 37,
            UPDATE_DIRECT_PO_TAX = 38,
            DELETE_DIRECT_PO_TAX = 39,
            UPDATE_ApproveStatus = 40,
            UPDATE_APROVEL_STAUSDPO = 41,
            PONo_SELECT_BY_VendorId = 42,
            REQUEST_FOR_QUOTATION_SELECT_ALL = 43,
            RFQ_Delete = 44,
            SELECT_RFQDETAILS_BY_RFQNO = 45,
            SELECT_RFQ_ITEMS_BY_RFQNO = 46,
            SELECT_RFQ_ITEM_BY_ITEM_ID = 47,
            INSERT_DIRECT_RFQ_TAX = 48,
            UPDATE_DIRECT_RFQ_TAX = 49,
            DELETE_DIRECT_RFQ_TAX = 50,
            SELECT_RFQ_TAX_BY_RFQID = 51,
            SELECT_RFQ_ITEMS_BY_RFQNO_FOR_PRINT = 52,
            SELECT_RFQ_ITEMWISE_TAX_BY_RFQ_ID_FOR_PRINT = 53,
            SELECT_RFQ_TAX_BY_RFQID_FOR_PRINT = 54,
            SELECT_RFQ_No = 55,
            SELECT_RFQ_DETAILS_BY_ID = 56,
            SELECT_RFQ_TAX_BY_RFQNO = 57,
            SELECT_RFQ_ITEM_BY_RFQNo = 58,

        };

        #endregion

        #region "Class: RequestForquotationBL Sets / Gets"

        public int UserID { get; set; }

        public int PO_ID { get; set; }
        public int RFQ_ID { get; set; }

        public string PONo { get; set; }
        public string RFQNo { get; set; }

        public DateTime PODate { get; set; }
        public DateTime RFQDate { get; set; }
        public string IndentNo { get; set; }
        public string DeliverySchedule { get; set; }
        public string FYear { get; set; }
        public string VendorID { get; set; }
        public string DespatchAdvise { get; set; }
        public string QuotationNo { get; set; }
        public string VendorRef { get; set; }
        public DateTime? UGCLRef { get; set; }
        public DateTime? DueDate { get; set; }
        public string PaymentTerms { get; set; }
        public int ApprovedBy { get; set; }
        public int LocationID { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }

        public string Others { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string DispatchMode { get; set; }

        public string Place_of_Dispatch { get; set; }
        public string Destination { get; set; }
        public bool IsApproved { get; set; }
        public decimal TDSPerc { get; set; }
        public string Uploaded_File { get; set; }

        public decimal Total_Amt { get; set; }

        public decimal NetTotalAmt { get; set; }

        public bool Extra { get; set; }

        public string ApprovalStatus { get; set; }
        public DateTime? Approval_Date { get; set; }
        public string Approver_Com { get; set; }
        public string Task { get; set; }

        public string TIN_No { get; set; }

        //Purchase Order Item Details
        public int PO_Item_Id
        { get; set; }

        public int RFQ_Item_Id
        { get; set; }
        public int Mat_Cat_Id
        { get; set; }
        public string Item_Code
        { get; set; }
        public bool? BOQ
        { get; set; }
        public string BOQ_No
        { get; set; }
        public string Total_Qty_Involved
        { get; set; }
        public decimal Qty_Available
        { get; set; }
        public decimal Qty_required
        { get; set; }
        public DateTime? Tentative_Date
        { get; set; }
        public bool? Whether_Req_Qty
        { get; set; }
        public decimal Amount
        { get; set; }
        public decimal Amountrfq
        { get; set; }
        public decimal Rate
        { get; set; }
        public string Remark
        { get; set; }
        public string UOM
        { get; set; }

        //Purchase Order Tax Details
        public int PO_Tax_ID { get; set; }

        public int RFQ_Tax_ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Type_Perc { get; set; }
        public decimal Type_Amount { get; set; }

        //Fortnight Report
        //public string PO_ID { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string ProjectCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string BudgetSector { get; set; }
        public bool TransportCost_Exists { get; set; }
        public int Flag { get; set; }

        //Extra for Direct PO

        public decimal Item_Amount { get; set; }
        public decimal Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Igst_Perc { get; set; }
        public decimal Igst_Amt { get; set; }
        public decimal Cgst_Perc { get; set; }
        public decimal Cgst_Amt { get; set; }
        public decimal Sgst_Perc { get; set; }
        public decimal Sgst_Amt { get; set; }

        public decimal Igstrfq_Perc { get; set; }
        public decimal Igstrfq_Amt { get; set; }
        public decimal Cgstrfq_Perc { get; set; }
        public decimal Cgstrfq_Amt { get; set; }
        public decimal Sgstrfq_Perc { get; set; }
        public decimal Sgstrfq_Amt { get; set; }
        public decimal PackingCost { get; set; }
        public decimal TransportCost { get; set; }
        public string TaxType { get; set; }

        #endregion

        #region "Class: RequestForquotationBL Methods"

        /// <summary>
        /// Function Name:  fillCollectionFromDr.
        /// Called By:      Nill.
        /// Description:    Fill the data from data reader. 
        /// Change history: Nill.
        /// </summary>

        private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, ProjectBL> dicCountry)
        {
            if (dicCountry == null)
            {
                dicCountry = new Dictionary<int, ProjectBL>();
            }

            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName
                    if (dr["Indent_No"] != DBNull.Value)
                    {
                        this.PONo = dr["Indent_No"].ToString();
                    }


                }
                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }

        /// <summary>
        /// Function Name:  fillObjectFromDr.
        /// Called By:      Nill.
        /// Description:    Fill the object from data reader. 
        /// Change history: Nill.
        private bool fillObjectFromDr(SqlDataReader dr)
        {
            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName
                    if (dr["UOM"] != DBNull.Value)
                    {
                        //this.UOM = dr["UOM"].ToString();
                    }

                }
                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
        /// <summary>
        /// Function Name:  getSpName.
        /// Called By:      Nill.
        /// Description:    Get the SpName from enumSpName 
        /// Change history: Nill.
        /// </summary>

        private string getSpName(eLoadSp enumSpName)
        {
            switch (enumSpName)
            {
                case eLoadSp.UPDATE_PO_ITEM_OVERALL_AMOUNT_BY_ID:
                    return "Proc_Update_PO_OverallTaxAmount";
                case eLoadSp.INSERT:
                    return "PROC_RFQ_INSERT_UPDATE";
                case eLoadSp.UPDATE:
                    return "PROC_RFQ_INSERT_UPDATE";
                case eLoadSp.SELECT_PO_ITEMS_BY_PONO:
                    return "PROC_PO_SELECT_ALL";
                case eLoadSp.SELECT_RFQ_ITEMS_BY_RFQNO:
                    return "PROC_RFQ_SELECT_ALL";
                case eLoadSp.SELECT_PO_ITEM_BY_ITEM_ID:
                    return "PROC_PO_ITEM_SELECT";
                case eLoadSp.SELECT_RFQ_ITEM_BY_ITEM_ID:
                    return "PROC_RFQ_ITEM_SELECT";
                case eLoadSp.PRO_PURCHASE_ORDER_SELECT_ALL:
                    return "PRO_PURCHASE_ORDER_SELECT_ALL";
                case eLoadSp.REQUEST_FOR_QUOTATION_SELECT_ALL:
                    return "PRO_REQUEST_FOR_QUOTATION_SELECT_ALL";
                case eLoadSp.SELECT_PODETAILS_BY_PONO:
                    return "PROC_SELECT_PODETAILS_BY_PONO";
                case eLoadSp.SELECT_RFQDETAILS_BY_RFQNO:
                    return "PROC_SELECT_RFQDETAILS_BY_RFQNO";
                case eLoadSp.INSERT_PO_ITEM:
                    return "PROC_PO_ITEM_INSERT";
                case eLoadSp.UPDATE_PO_ITEM:
                    return "PROC_PO_ITEM_UPDATE";
                case eLoadSp.INSERT_PO_TAX:
                    return "PROC_INSERT_PO_TAX";
                case eLoadSp.SELECT_PO_TAX_BY_POID:
                    return "SELECT_PO_TAX_BY_POID";
                case eLoadSp.SELECT_RFQ_TAX_BY_RFQID:
                    return "SELECT_RFQ_TAX_BY_RFQID";
                case eLoadSp.SELECT_RFQ_TAX_BY_RFQNO:
                    return "SELECT_RFQ_TAX_BY_RFQNO";
                case eLoadSp.UPDATE_PO_NETTOTAL:
                    return "PROC_UPDATE_PO_NETTOTAL";
                case eLoadSp.DELETE_PO_ITEM_BY_ID:
                    return "PROC_DELETE_PO_ITEM_BY_ID";
                case eLoadSp.DELETE_PO_TAX_BY_ID:
                    return "PROC_DELETE_POTAX_BY_ID";
                case eLoadSp.SELECT_PO_TAX_BY_TAXID:
                    return "PROC_SELECT_PO_TAX_BY_TAXID";
                case eLoadSp.UPDATE_PO_TAX:
                    return "PROC_UPDATE_PO_TAX_BY_ID";
                case eLoadSp.POAWAITING_APPROVAL:
                    return "PRO_POAWAITING_APPROVAL";
                case eLoadSp.UPDATE_APPROVAL_POAWAITING:
                    return "PRO_UPDATE_APPROVAL_POAWAITING";
                case eLoadSp.PROC_SELECT_PO_CREATING_ITEMS:
                    return "PROC_SELECT_PO_CREATING_ITEMS_NEW";
                case eLoadSp.SELECT_PO_ITEMWISE_TAX_BY_PO_ID:
                    return "PROC_SELECT_PO_ITEMWISE_TAX_BY_PO_ID";
                case eLoadSp.PO_Delete:
                    return "PRO_DELETE_PO_BY_PONO";
                case eLoadSp.RFQ_Delete:
                    return "PRO_DELETE_PO_BY_PONO";
                case eLoadSp.UPDATE_PO_ITEM_QTY_BY_ID:
                    return "PROC_UPDATE_PO_ITEM_BY_QTY";
                case eLoadSp.SELECT_EXISTING_QTY_OF_INDENT:
                    return "PROC_SELECT_EXISTING_QTY_OF_INDENT";
                case eLoadSp.SELECT_FORTNIGHT_REPORT:
                    return "PRO_Fortnight_Report";
                case eLoadSp.CHECK_PONO_IN_MRN:
                    return "PROC_CHECK_PONO_IN_MRN";
                case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REPORT:
                    return "PROC_SECTORWISE_FORT_NIGHT_REPORT";
                case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REPORT_ALL:
                    return "PROC_SECTORWISE_FORT_NIGHT_REPORT_ALL";
                case eLoadSp.SELECT_FORTNIGHT_REPORT_DATE_BASED:
                    return "PRO_Fortnight_Report_Date_Based";
                case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REPORT_DATE_BASED:
                    return "PROC_SECTORWISE_Date_Based_FORT_NIGHT_REPORT";
                case eLoadSp.SELECT_PO_ITEMWISE_TAX_BY_PO_ID_FOR_PRINT:
                    return "PROC_SELECT_PO_ITEMWISE_TAX_BY_PO_ID_For_Print";
                case eLoadSp.SELECT_RFQ_ITEMWISE_TAX_BY_RFQ_ID_FOR_PRINT:
                    return "PROC_SELECT_RFQ_ITEMWISE_TAX_BY_RFQ_ID_For_Print";
                case eLoadSp.SELECT_PO_ITEMS_BY_PONO_FOR_PRINT:
                    return "PROC_PO_SELECT_For_Print";
                case eLoadSp.SELECT_RFQ_ITEMS_BY_RFQNO_FOR_PRINT:
                    return "PROC_RFQ_SELECT_For_Print";
                case eLoadSp.SELECT_PO_TAX_BY_POID_FOR_PRINT:
                    return "SELECT_PO_TAX_BY_POID_For_Print";
                case eLoadSp.SELECT_RFQ_TAX_BY_RFQID_FOR_PRINT:
                    return "SELECT_RFQ_TAX_BY_RFQID_For_Print";
                case eLoadSp.SELECT_PO_ITEMS_ALL_BY_PROJECT:
                    return "PROC_PO_ITEMS_SELECT_BY_PROJECT";
                case eLoadSp.SELECT_TRASPORT:
                    return "proc_transport";
                case eLoadSp.INSERT_DIRECT_RFQ_ITEM:
                    return "PROC_DIRECT_RFQ_ITEM_INSERT_UPDATE";
                case eLoadSp.UPDATE_DIRECT_RFQ_ITEM:
                    return "PROC_DIRECT_RFQ_ITEM_INSERT_UPDATE";
                case eLoadSp.DELETE_DIRECT_RFQ_ITEM:
                    return "PROC_DIRECT_RFQ_ITEM_INSERT_UPDATE";
                case eLoadSp.INSERT_DIRECT_PO_TAX:
                    return "PROC_DIRECT_PO_TAX_INSERT_UPDATE";
                case eLoadSp.INSERT_DIRECT_RFQ_TAX:
                    return "PROC_DIRECT_RFQ_TAX_INSERT_UPDATE";
                case eLoadSp.UPDATE_DIRECT_RFQ_TAX:
                    return "PROC_DIRECT_RFQ_TAX_INSERT_UPDATE";
                case eLoadSp.DELETE_DIRECT_RFQ_TAX:
                    return "PROC_DIRECT_RFQ_TAX_INSERT_UPDATE";
                case eLoadSp.UPDATE_ApproveStatus:
                    return "PROC_PO_INSERT_UPDATE";
                case eLoadSp.UPDATE_APROVEL_STAUSDPO:
                    return "PRO_UPDATE_APROVEL_STAUSDPO";
                case eLoadSp.PONo_SELECT_BY_VendorId:
                    return "PRO_PONo_SELECT_BY_VendorId";
                case eLoadSp.SELECT_RFQ_No:
                    return "PROC_SELECT_RFQ_NO";
                case eLoadSp.SELECT_RFQ_ITEM_BY_RFQNo:
                    return "PROC_SELECT_RFQ_ITEM_BY_RFQNo";

                case eLoadSp.SELECT_RFQ_DETAILS_BY_ID:
                    return "PRO_SELECT_RFQDETAILS_BY_ID";
                default:

                    return string.Empty;
            }
        }

        /// <summary>
        /// Function Name:  getSpParamArray.
        /// Called By:      Nill.
        /// Description:    Get the parameter in enumSpName wise
        /// Change history: Nill.
        /// </summary>
        private SqlParameter[] getSpParamArray(eLoadSp enumSpName)
        {
            SqlParameter[] colParams = new SqlParameter[]
        {
        };

            switch (enumSpName)
            {

                case eLoadSp.INSERT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@RFQ_ID", this.RFQ_ID)

                };
                    break;

                case eLoadSp.SELECT_PO_ITEMS_BY_PONO:
                case eLoadSp.SELECT_PO_ITEMS_BY_PONO_FOR_PRINT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@POID", this.PO_ID)

                };
                    break;


                case eLoadSp.SELECT_RFQ_ITEMS_BY_RFQNO:
                case eLoadSp.SELECT_RFQ_ITEMS_BY_RFQNO_FOR_PRINT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@RFQID", this.RFQ_ID)

                };
                    break;

                case eLoadSp.SELECT_RFQ_ITEM_BY_RFQNo:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@RFQNo", this.RFQNo)

                };
                    break;

                case eLoadSp.SELECT_PO_ITEM_BY_ITEM_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@POItemID", this.PO_Item_Id)

                };

                    break;
                case eLoadSp.SELECT_RFQ_ITEM_BY_ITEM_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@RFQItemID", this.RFQ_Item_Id)

                };

                    break;
                case eLoadSp.PONo_SELECT_BY_VendorId:
                    colParams = new SqlParameter[]
                {

                   new SqlParameter("@Vendor_id", this.VendorID)

                };
                    break;
                case eLoadSp.UPDATE_APROVEL_STAUSDPO:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@PONo", this.PONo),
                         new SqlParameter("@Approver_Com", this.Approver_Com)
                    };
                    break;
                case eLoadSp.SELECT_PODETAILS_BY_PONO:
                case eLoadSp.PO_Delete:
                case eLoadSp.CHECK_PONO_IN_MRN:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PONo", this.PONo)

                };
                    break;

                case eLoadSp.SELECT_RFQDETAILS_BY_RFQNO:
                case eLoadSp.RFQ_Delete:
                    //case eLoadSp.CHECK_PONO_IN_MRN:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@RFQNo", this.RFQNo)

                };
                    break;
                case eLoadSp.SELECT_PO_TAX_BY_POID:
                case eLoadSp.SELECT_PO_ITEMWISE_TAX_BY_PO_ID:
                case eLoadSp.SELECT_PO_ITEMWISE_TAX_BY_PO_ID_FOR_PRINT:
                case eLoadSp.SELECT_PO_TAX_BY_POID_FOR_PRINT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PO_ID", this.PO_ID)

                };

                    break;
                case eLoadSp.SELECT_RFQ_TAX_BY_RFQID:
                // case eLoadSp.SELECT_PO_ITEMWISE_TAX_BY_PO_ID:
                case eLoadSp.SELECT_RFQ_ITEMWISE_TAX_BY_RFQ_ID_FOR_PRINT:
                case eLoadSp.SELECT_RFQ_TAX_BY_RFQID_FOR_PRINT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@RFQ_ID", this.RFQ_ID)

                };
                    break;
                case eLoadSp.SELECT_RFQ_TAX_BY_RFQNO:

                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@RFQNo", this.RFQNo)

                };

                    break;
                case eLoadSp.DELETE_PO_ITEM_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@POItemID", this.PO_Item_Id)

                };

                    break;

                case eLoadSp.DELETE_PO_TAX_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PO_Tax_ID", this.PO_Tax_ID)

                };

                    break;
                case eLoadSp.UPDATE_PO_ITEM_OVERALL_AMOUNT_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PO_Tax_ID", this.PO_Tax_ID),
                    new SqlParameter("@Amount", this.Amount)
                };

                    break;
                case eLoadSp.SELECT_PO_TAX_BY_TAXID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PO_Tax_ID", this.PO_Tax_ID)

                };

                    break;
                case eLoadSp.POAWAITING_APPROVAL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ApprovedBy", this.ApprovedBy),
                    new SqlParameter("@Project_Code",this.ProjectCode)

                };

                    break;

                case eLoadSp.PROC_SELECT_PO_CREATING_ITEMS:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@Indent_No", this.IndentNo),
                      new SqlParameter("@VendorID", this.VendorID),
                       new SqlParameter("@Task", this.Task),
                       new SqlParameter("@Flag", this.Flag),
                       new SqlParameter("@Project_Code",this.ProjectCode)
                };
                    break;


                case eLoadSp.SELECT_EXISTING_QTY_OF_INDENT:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@IndentNo", this.IndentNo),
                     new SqlParameter("@POItemID", this.PO_Item_Id)

                };
                    break;
                case eLoadSp.PRO_PURCHASE_ORDER_SELECT_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value),
                    new SqlParameter("@Task", this.Task ??  (object)DBNull.Value),
                };
                    break;
                case eLoadSp.REQUEST_FOR_QUOTATION_SELECT_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value),
                    new SqlParameter("@Task", this.Task ??  (object)DBNull.Value),
                };
                    break;
                case eLoadSp.SELECT_FORTNIGHT_REPORT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ProjectCode", this.ProjectCode ??  (object)DBNull.Value),
                    new SqlParameter("@Month", this.Month ??  (object)DBNull.Value),
                    new SqlParameter("@Year", this.Year ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REPORT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@BudgetSector", this.BudgetSector),
                    new SqlParameter("@ProjectCode", this.ProjectCode),
                    new SqlParameter("@Month", this.Month ??  (object)DBNull.Value),
                    new SqlParameter("@Year", this.Year ??  (object)DBNull.Value)

                };
                    break;
                case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REPORT_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ProjectCode", this.ProjectCode),
                    new SqlParameter("@Month", this.Month ??  (object)DBNull.Value),
                    new SqlParameter("@Year", this.Year ??  (object)DBNull.Value)

                };
                    break;
                case eLoadSp.SELECT_FORTNIGHT_REPORT_DATE_BASED:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ProjectCode", this.ProjectCode ??  (object)DBNull.Value),
                    new SqlParameter("@StartDate", this.StartDate),
                     new SqlParameter("@EndDate", this.EndDate)
                };
                    break;
                case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REPORT_DATE_BASED:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@BudgetSector", this.BudgetSector),
                     new SqlParameter("@ProjectCode", this.ProjectCode),
                     new SqlParameter("@StartDate", this.StartDate),
                     new SqlParameter("@EndDate", this.EndDate)

                };
                    break;
                case eLoadSp.SELECT_PO_ITEMS_ALL_BY_PROJECT:
                    colParams = new SqlParameter[]
                {

                   new SqlParameter("@Project_Code", this.ProjectCode)

                };
                    break;
                case eLoadSp.SELECT_TRASPORT:
                    colParams = new SqlParameter[]
                {

                   new SqlParameter("@PO_ID", this.PO_ID)

                };
                    break;
                case eLoadSp.SELECT_RFQ_DETAILS_BY_ID:
                    //case eLoadSp.DELETE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@RFQNo", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "RFQNo", DataRowVersion.Current, this.RFQNo ?? (Object)DBNull.Value),
                };
                    break;

            }

            return colParams;
        }

        public bool POTaxInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return POTaxInsert(SqlConn, null, enumSpName);
        }

        private bool POTaxInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PO_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PO_ID", DataRowVersion.Current, this.PO_ID),

                new SqlParameter("@Type", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Type", DataRowVersion.Current, this.Type),
                new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, this.Description),
                new SqlParameter("@Type_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Perc", DataRowVersion.Current, this.Type_Perc),
                new SqlParameter("@Type_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Amount", DataRowVersion.Current, this.Type_Amount)

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                }
                if (intIdentityValue < 1)
                {
                    return false;
                }
                return true;
            }

            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }


        public bool updatePOTax(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updatePOTax(SqlConn, null, enumSpName);
        }

        private bool updatePOTax(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

                new SqlParameter("@PO_Tax_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "@PO_Tax_ID", DataRowVersion.Current, this.PO_Tax_ID),

                 new SqlParameter("@PO_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PO_ID", DataRowVersion.Current, this.PO_ID),

                new SqlParameter("@Type", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Type", DataRowVersion.Current, this.Type),
                new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, this.Description),
                new SqlParameter("@Type_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Perc", DataRowVersion.Current, this.Type_Perc),
                new SqlParameter("@Type_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Amount", DataRowVersion.Current, this.Type_Amount)
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (intReturnVal < 1)
                {
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }


        public bool updatePONetToatl(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updatePONetToatl(SqlConn, null, enumSpName);
        }

        private bool updatePONetToatl(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

                new SqlParameter("@PO_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PO_ID", DataRowVersion.Current, this.PO_ID)


            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (intReturnVal < 1)
                {
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }


        public bool UpdateApprovalPO(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return UpdateApprovalPO(SqlConn, null, enumSpName);
        }

        public bool UpdateApprovalPO(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return UpdateApprovalPO(null, SqlTran, enumSpName);
        }

        private bool UpdateApprovalPO(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 16;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PONo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "PONo", DataRowVersion.Current, this.PONo),
                 new SqlParameter("@Approval_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Approval_Date", DataRowVersion.Current, this.Approval_Date),
                 new SqlParameter("@Approver_Com", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Approver_Com", DataRowVersion.Current, this.Approver_Com),
                 new SqlParameter("@ApprovalStatus", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ApprovalStatus", DataRowVersion.Current, this.ApprovalStatus),


            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (intReturnVal < 1)
                {
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }

        }



        public bool insert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert(SqlConn, null, enumSpName);
        }

        public bool insert(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insert(null, SqlTran, enumSpName);
        }

        private bool insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@RFQNo", SqlDbType.VarChar, 30, ParameterDirection.Output, false, 0, 0, "RFQNo", DataRowVersion.Current, this.RFQNo),
                new SqlParameter("@RFQ_ID", SqlDbType.Int,4, ParameterDirection.Output, false, 0, 0, "RFQ_ID", DataRowVersion.Current, this.RFQ_ID),
                new SqlParameter("@RFQDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "RFQDate", DataRowVersion.Current, this.RFQDate),
                new SqlParameter("@IndentNo", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "IndentNo", DataRowVersion.Current, this.IndentNo),
                new SqlParameter("@DeliverySchedule", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "DeliverySchedule", DataRowVersion.Current, this.DeliverySchedule),
                new SqlParameter("@VendorID", SqlDbType.VarChar, 8, ParameterDirection.Input, false, 0, 0, "VendorID", DataRowVersion.Current, this.VendorID),
                new SqlParameter("@VendorRef", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "VendorRef", DataRowVersion.Current, this.VendorRef),
                new SqlParameter("@DespatchAdvise", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "DespatchAdvise", DataRowVersion.Current, this.DespatchAdvise),
                new SqlParameter("@UGCLRef", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "UGCLRef", DataRowVersion.Current, this.UGCLRef  ?? (object)DBNull.Value),
                new SqlParameter("@PaymentTerms", SqlDbType.VarChar,5000, ParameterDirection.Input, false, 0, 0, "PaymentTerms", DataRowVersion.Current, this.PaymentTerms ?? (object)DBNull.Value),
                new SqlParameter("@ApprovedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "ApprovedBy", DataRowVersion.Current, this.ApprovedBy),
                new SqlParameter("@Status", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@TINNo", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "TIN_No", DataRowVersion.Current, this.TIN_No),
                new SqlParameter("@Remarks", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Remarks", DataRowVersion.Current, this.Remarks ?? (object)DBNull.Value),
                new SqlParameter("@Others", SqlDbType.VarChar,5000, ParameterDirection.Input, false, 10, 0, "Others", DataRowVersion.Current, this.Others ?? (object)DBNull.Value),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@TransportCost_Exists", SqlDbType.Bit,1, ParameterDirection.Input, false, 10, 0, "TransportCost_Exists", DataRowVersion.Current, this.TransportCost_Exists),
                new SqlParameter("@Prepared_By", SqlDbType.Int,4, ParameterDirection.Input, false,0, 0, "Prepared_By", DataRowVersion.Current, this.UserID),
                new SqlParameter("@Flag", SqlDbType.Int,4, ParameterDirection.Input, false,0, 0, "Flag", DataRowVersion.Current, this.Flag),
                new SqlParameter("@ContactName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactName", DataRowVersion.Current, this.ContactName),
                new SqlParameter("@ContactNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactNo", DataRowVersion.Current, this.ContactNo),
                new SqlParameter("@DueDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "DueDate", DataRowVersion.Current, this.DueDate  ?? (object)DBNull.Value),
                new SqlParameter("@QuotationNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "QuotationNo", DataRowVersion.Current, this.QuotationNo),
                new SqlParameter("@ProjectCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ProjectCode", DataRowVersion.Current, this.ProjectCode),
                new SqlParameter("@FYear", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "FYear", DataRowVersion.Current, this.FYear),
                new SqlParameter("@DispatchMode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "DispatchMode", DataRowVersion.Current, this.DispatchMode),
                new SqlParameter("@TDSPerc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "TDSPerc", DataRowVersion.Current, this.TDSPerc),
                new SqlParameter("@Place_of_Dispatch", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Place_of_Dispatch", DataRowVersion.Current, this.Place_of_Dispatch),
                new SqlParameter("@Destination", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Destination", DataRowVersion.Current, this.Destination)


            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    // string check;
                    //check = colParams[0].Value.ToString();

                    this.RFQNo = (string)colParams.First().Value;
                    this.RFQ_ID = (int)colParams[1].Value;
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                }
                if (intIdentityValue < 1)
                {
                    return false;
                }
                return true;
            }

            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }


        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref SqlDataReader dr)
        {
            try
            {
                SqlParameter[] colParams = new SqlParameter[]
            {
            };

                colParams = getSpParamArray(enumSpName);

                if (colParams == null)
                {
                    dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName));
                }
                else
                {
                    dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
            finally
            {
                if (dr != null)
                {
                    if (dr.IsClosed != true)
                    {
                        dr.Close();
                    }
                }
            }
        }

        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref DataSet ds)
        {
            try
            {
                SqlParameter[] colParams = new SqlParameter[]
            {
            };

                colParams = getSpParamArray(enumSpName);

                if (colParams == null)
                {
                    ds = SqlHelper.ExecuteDataset(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName));
                }
                else
                {
                    ds = SqlHelper.ExecuteDataset(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }

        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, ProjectBL> diclCountry)
        {
            SqlDataReader dr = null;

            try
            {
                SqlParameter[] colParams = new SqlParameter[]
            {
            };

                colParams = getSpParamArray(enumSpName);

                if (colParams == null)
                {
                    dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName));
                }
                else
                {
                    dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }
                //
                //Build a collection of objects based on the data reader.
                //
                if (fillCollectionFromDr(dr, ref diclCountry))
                {
                    return true;
                }
                else
                {
                    //clsErrors.Add("VEND_LD");
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
            finally
            {
                if (dr != null)
                {
                    if (dr.IsClosed != true)
                    {
                        dr.Close();
                    }
                }
            }
        }

        public bool load(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            SqlDataReader dr = null;

            try
            {
                SqlParameter[] colParams = new SqlParameter[]
            {
            };

                colParams = getSpParamArray(enumSpName);

                if (colParams == null)
                {
                    dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName));
                }
                else
                {
                    dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (fillObjectFromDr(dr))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.Message.First();
                return false;
            }
            finally
            {
                if (dr != null)
                {
                    if (dr.IsClosed != true)
                    {
                        dr.Close();
                    }
                }
            }
        }


        public bool delete(SqlConnection objConn, eLoadSp enmSPName)
        {
            try
            {
                int _intReturnVal;
                SqlParameter[] colParams = getSpParamArray(enmSPName);
                _intReturnVal = SqlHelper.ExecuteNonQuery(objConn, CommandType.StoredProcedure, getSpName(enmSPName), colParams);
                if (_intReturnVal < 1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }


        public bool updatePOItemQty(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updatePOItemQty(SqlConn, null, enumSpName);
        }

        private bool updatePOItemQty(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
               new SqlParameter("@PO_Item_Id", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "PO_Item_Id", DataRowVersion.Current, this.PO_Item_Id),
               new SqlParameter("@QtyRequired", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Qty_required", DataRowVersion.Current, this.Qty_required),
               new SqlParameter("@POID", this.PO_ID),
               new SqlParameter("@Indent_No",this.IndentNo),
               new SqlParameter("@VendorID",this.VendorID),
               new SqlParameter("@Flag",this.Flag),
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);

                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (intReturnVal < 1)
                {
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }


        private bool updatePOOverallAmount(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = getSpParamArray(enumSpName);

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);

                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (intReturnVal < 1)
                {
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }

        public bool updatePOOverallAmount(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updatePOOverallAmount(SqlConn, null, enumSpName);
        }


        public bool update(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return update(SqlConn, null, enumSpName);
        }

        public bool update(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return update(null, SqlTran, enumSpName);
        }

        private bool update(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

               new SqlParameter("@RFQNo", SqlDbType.VarChar, 30, ParameterDirection.InputOutput, false, 0, 0, "RFQNo", DataRowVersion.Current, this.RFQNo),
                new SqlParameter("@RFQDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "RFQDate", DataRowVersion.Current, this.RFQDate),
                new SqlParameter("@IndentNo", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "IndentNo", DataRowVersion.Current, this.IndentNo),
                new SqlParameter("@DeliverySchedule", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "DeliverySchedule", DataRowVersion.Current, this.DeliverySchedule),
                new SqlParameter("@VendorID", SqlDbType.VarChar, 8, ParameterDirection.Input, false, 0, 0, "VendorID", DataRowVersion.Current, this.VendorID),
                new SqlParameter("@VendorRef", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "VendorRef", DataRowVersion.Current, this.VendorRef),
                new SqlParameter("@DespatchAdvise", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "DespatchAdvise", DataRowVersion.Current, this.DespatchAdvise),
                new SqlParameter("@UGCLRef", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "UGCLRef", DataRowVersion.Current, this.UGCLRef  ?? (object)DBNull.Value),
                new SqlParameter("@PaymentTerms", SqlDbType.VarChar,5000, ParameterDirection.Input, false, 0, 0, "PaymentTerms", DataRowVersion.Current, this.PaymentTerms ?? (object)DBNull.Value),
                new SqlParameter("@ApprovedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "ApprovedBy", DataRowVersion.Current, this.ApprovedBy),
                new SqlParameter("@Status", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@TINNo", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "TIN_No", DataRowVersion.Current, this.TIN_No),
                new SqlParameter("@Remarks", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Remarks", DataRowVersion.Current, this.Remarks ?? (object)DBNull.Value),
                new SqlParameter("@Others", SqlDbType.VarChar,5000, ParameterDirection.Input, false, 10, 0, "Others", DataRowVersion.Current, this.Others ?? (object)DBNull.Value),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@TransportCost_Exists", SqlDbType.Bit, 1, ParameterDirection.Input, false, 10, 0, "TransportCost_Exists", DataRowVersion.Current, this.TransportCost_Exists),
                new SqlParameter("@Prepared_By", SqlDbType.Int,4, ParameterDirection.Input, false,0, 0, "Prepared_By", DataRowVersion.Current, this.UserID),
                new SqlParameter("@Flag", SqlDbType.Int,4, ParameterDirection.Input, false,0, 0, "Flag", DataRowVersion.Current, this.Flag),
                new SqlParameter("@ContactName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactName", DataRowVersion.Current, this.ContactName),
                new SqlParameter("@ContactNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactNo", DataRowVersion.Current, this.ContactNo),
                new SqlParameter("@DueDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "DueDate", DataRowVersion.Current, this.DueDate  ?? (object)DBNull.Value),
                new SqlParameter("@QuotationNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "QuotationNo", DataRowVersion.Current, this.QuotationNo),
                new SqlParameter("@FYear", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "FYear", DataRowVersion.Current, this.FYear),
                new SqlParameter("@DispatchMode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "DispatchMode", DataRowVersion.Current, this.DispatchMode),
                new SqlParameter("@TDSPerc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "TDSPerc", DataRowVersion.Current, this.TDSPerc),
                new SqlParameter("@Uploaded_File", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Uploaded_File", DataRowVersion.Current, this.Uploaded_File),
                new SqlParameter("@Place_of_Dispatch", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Place_of_Dispatch", DataRowVersion.Current, this.Place_of_Dispatch),
                new SqlParameter("@Destination", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Destination", DataRowVersion.Current, this.Destination),
                 //new SqlParameter("@IsApproved", SqlDbType.Bit, 500, ParameterDirection.Input, false, 10, 0, "IsApproved", DataRowVersion.Current, this.IsApproved)
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    this.RFQNo = (string)colParams.First().Value;
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                }

                if (intReturnVal < 1)
                {
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }


        public bool DirectRFQItemInsertUpdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return DirectRFQItemInsertUpdate(SqlConn, null, enumSpName);
        }

        private bool DirectRFQItemInsertUpdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@RFQ_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "RFQ_ID", DataRowVersion.Current, this.RFQ_ID),
                new SqlParameter("@RFQNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "RFQNo", DataRowVersion.Current, this.RFQNo),
                new SqlParameter("@Mat_Cat_Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Mat_Cat_Id", DataRowVersion.Current, this.Mat_Cat_Id),
                new SqlParameter("@Item_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Item_Desc", DataRowVersion.Current, this.Item_Code),

                new SqlParameter("@Price", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Price", DataRowVersion.Current, this.Price),
                new SqlParameter("@Qty", SqlDbType.Decimal, 10, ParameterDirection.Input, false, 10, 0, "Qty", DataRowVersion.Current, this.Qty),
                new SqlParameter("@Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Amount", DataRowVersion.Current, this.Amount),
                new SqlParameter("@Igst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Igst_Perc", DataRowVersion.Current, this.Igst_Perc),
                new SqlParameter("@Cgst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Cgst_Perc", DataRowVersion.Current, this.Cgst_Perc),
                new SqlParameter("@Sgst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Sgst_Perc", DataRowVersion.Current, this.Sgst_Perc),
                //new SqlParameter("@PackingTransport", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "PackingTransport", DataRowVersion.Current, this.PackingTransport),                
                new SqlParameter("@RFQ_Item_Id", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "RFQ_Item_Id", DataRowVersion.Current, this.RFQ_Item_Id),

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                }
                if (intIdentityValue < 1)
                {
                    return false;
                }
                return true;
            }

            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }



        public bool DirectRFQTaxInsertUpdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return DirectRFQTaxInsertUpdate(SqlConn, null, enumSpName);
        }

        public bool DirectRFQTaxInsertUpdate(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return DirectRFQTaxInsertUpdate(null, SqlTran, enumSpName);
        }

        private bool DirectRFQTaxInsertUpdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@RFQNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "RFQNo", DataRowVersion.Current, this.RFQNo),
                new SqlParameter("@TaxType", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "TaxType", DataRowVersion.Current, this.TaxType),                
                new SqlParameter("@Igst_Perc", SqlDbType.Decimal, 15, ParameterDirection.Input, false, 15, 2, "Igst_Perc", DataRowVersion.Current, this.Igstrfq_Perc),
                new SqlParameter("@Igst_Amt", SqlDbType.Decimal, 15, ParameterDirection.Input, false, 15, 2, "Igst_Amt", DataRowVersion.Current, this.Igstrfq_Amt),
                new SqlParameter("@Cgst_Perc", SqlDbType.Decimal, 15, ParameterDirection.Input, false, 15, 2, "Cgst_Perc", DataRowVersion.Current, this.Cgstrfq_Perc),
                new SqlParameter("@Cgst_Amt", SqlDbType.Decimal, 15, ParameterDirection.Input, false, 15, 2, "Cgst_Amt", DataRowVersion.Current, this.Cgstrfq_Amt),
                new SqlParameter("@Sgst_Amt", SqlDbType.Decimal, 15, ParameterDirection.Input, false, 15, 2, "Sgst_Amt", DataRowVersion.Current, this.Sgstrfq_Amt),
                new SqlParameter("@Sgst_Perc", SqlDbType.Decimal, 15, ParameterDirection.Input, false, 15, 2, "Sgst_Perc", DataRowVersion.Current, this.Sgstrfq_Perc),
                new SqlParameter("@PackingCost", SqlDbType.Decimal, 15, ParameterDirection.Input, false, 10, 0, "PackingCost", DataRowVersion.Current, this.PackingCost),
                new SqlParameter("@TransportCost", SqlDbType.Decimal, 15, ParameterDirection.Input, false, 10, 0, "TransportCost", DataRowVersion.Current, this.TransportCost),
                new SqlParameter("@RFQ_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "RFQ_ID", DataRowVersion.Current, this.RFQ_ID),
                new SqlParameter("@RFQ_Tax_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "RFQ_Tax_ID", DataRowVersion.Current, this.RFQ_Tax_ID)

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                }

                if (SqlTran != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                }
                if (intIdentityValue < 1)
                {
                    return false;
                }
                return true;
            }

            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;

            }
        }

        public RequestForquotationBL()
        {
            //
            //Set default values
            //
        }

        private bool validate()
        {
            try
            {

                return true;

            }
            catch (System.Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }

        #endregion

    }
}
