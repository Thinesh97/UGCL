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
    public class WorkOrderBL
    {
        #region "Class: WorkOrderBL Local Declarations"

        public enum eLoadSp
        {
            INSERT = 0,
            UPDATE = 1,
            SELECT_WO_ITEMS_BY_WONO = 2,
            SELECT_WO_ITEM_BY_ITEM_ID = 3,
            PRO_WORK_ORDER_SELECT_ALL = 4,
            SELECT_WODETAILS_BY_WONO = 5,
            INSERT_WO_ITEM = 6,
            UPDATE_WO_ITEM = 7,
            INSERT_WO_TAX = 8,
            SELECT_WO_TAX_BY_WOID = 9,
            UPDATE_WO_NETTOTAL = 10,
            DELETE_WO_ITEM_BY_ID = 11,

            UPDATE_WO_TAX = 14,
            WOAWAITING_APPROVAL = 15,
            UPDATE_APPROVAL_WOAWAITING = 16,
            PROC_SELECT_WO_CREATING_ITEMS = 17,
            SELECT_WO_ITEMWISE_TAX_BY_WO_ID = 18,
            WO_Delete = 19,
            UPDATE_WO_ITEM_QTY_BY_ID = 20,
            //SELECT_WO_ITEMWISE_TAX_BY_WO_ID_FOR_PRINT = 27,
            SELECT_WO_ITEMS_BY_WONO_FOR_PRINT = 28,
            SELECT_WO_TAX_BY_WOID_FOR_PRINT = 29,
            SELECT_WO_TYPE_ALL = 34,
            INSERT_SOW_ITEM = 35,
            //UPDATE_SOW_ITEM = 36,
            SELECT_SOW_ITEMS_BY_WONO = 37,
            SELECT_SOW_ITEM_BY_ITEM_ID = 38,
            DELETE_SOW_ITEM_BY_ID = 39,
            UPDATE_APROVEL_STAUSWO = 40,
            UPDATE_APROVEL_STAUSWHO=41,
            WOHNo_SELECT_BY_SubcotractorId=42,

        };

        #endregion

        #region "Class: WorkOrderBL Sets / Gets"

        public int UserID { get; set; }
        public int WO_ID { get; set; }
        public string WONo { get; set; }
        public DateTime WODate { get; set; }
        public string FYear { get; set; }
        public string Month { get; set; }
        public string SubContractorID { get; set; }
        public int WOTypeID { get; set; }
        public string WorkLocation { get; set; }
        public string DurationOfWork { get; set; }
        public string PaymentTerms { get; set; }
        public int ApprovedBy { get; set; }
        public int LocationID { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }

        public string Others { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public string ScopeOfWork { get; set; }
        public string DispatchMode { get; set; }
        public decimal TDSPerc { get; set; }
        public string OrderType { get; set; }
        public string Uploaded_File { get; set; }

        //WO Item Details
        public int? WO_Item_Id { get; set; }
        public int? Parent_WO_Item_Id { get; set; }
        public string Item_Type { get; set; }
        public string Item_Desc { get; set; }
        public string Sub_Item_Desc { get; set; }
        public decimal? Item_Perc { get; set; }
        public decimal? Item_Rate { get; set; }
        public int? Item_UOM { get; set; }
        public decimal? Item_Qty { get; set; }
        public decimal? Item_Amt { get; set; }
        public bool Gst_Exist { get; set; }
        public decimal Igst_Perc { get; set; }
        public decimal Igst_Amt { get; set; }
        public decimal Cgst_Perc { get; set; }
        public decimal Cgst_Amt { get; set; }
        public decimal Sgst_Perc { get; set; }
        public decimal Sgst_Amt { get; set; }

        //Scope Of Work Details
        public int SOW_Item_Id { get; set; }
        public string Scope_Of_Work { get; set; }
        public decimal? Quantity { get; set; }
        public int UMO { get; set; }
        public decimal? Rate { get; set; }

        //  public decimal Total_Amt { get; set; }
        //  public decimal NetTotalAmt  { get; set; }
        //  public bool Extra { get; set; }

        //  public string ApprovalStatus { get; set; }
        //  public DateTime? Approval_Date { get; set; }
        public string Approver_Com { get; set; }

        public bool IsApproved { get; set; }
        public string Task { get; set; }
        public string GST_No { get; set; }

        //Work Order Item Details
        //  public int WO_Item_Id { get; set; }
        //  public int Mat_Cat_Id
        //  { get; set; }
        //  public string Item_Code
        //  { get; set; }
        //  public bool? BOQ
        //  { get; set; }
        //  public string BOQ_No
        //  { get; set; }
        //  public string Total_Qty_Involved
        //  { get; set; }
        //  public decimal Qty_Available
        //  { get; set; }
        //  public decimal Qty_required
        //  { get; set; }
        //  public DateTime? Tentative_Date
        //  { get; set; }
        //  public bool? Whether_Req_Qty
        //  { get; set; }
        //   public decimal Amount
        //  { get; set; }
        //  public decimal Rate
        //   { get; set; }
        //  public string Remark
        //  { get; set; }
        //  public string UOM
        //  { get; set; }

        //  //Purchase Order Tax Details
        //  public int WO_Tax_ID { get; set; }
        //  public string Type { get; set; }
        //  public string Description { get; set; }
        //  public decimal Type_Perc { get; set; }
        //  public decimal Type_Amount { get; set; }

        //  //Fortnight ReWOrt
        ////  public string WO_ID { get; set; }
        //  public int? Month { get; set; }
        //  public int? Year { get; set; }
        public string ProjectCode { get; set; }
        //  public DateTime StartDate { get; set; }
        //  public DateTime EndDate { get; set; }

        //  public string BudgetSector { get; set; }

        //  public bool TransWOrtCost_Exists { get; set; }

        //  public int Flag { get; set; }

        #endregion

        #region "Class: WorkOrderBL Methods"

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
                        this.WONo = dr["Indent_No"].ToString();
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

                case eLoadSp.INSERT:
                    return "PROC_WO_INSERT_UPDATE";
                case eLoadSp.UPDATE:
                    return "PROC_WO_INSERT_UPDATE";
                case eLoadSp.SELECT_WO_ITEMS_BY_WONO:
                    return "PROC_WO_SELECT_ALL";
                case eLoadSp.SELECT_WO_ITEM_BY_ITEM_ID:
                    return "PROC_WO_ITEM_SELECT";
                case eLoadSp.PRO_WORK_ORDER_SELECT_ALL:
                    return "PRO_WORK_ORDER_SELECT_ALL";
                case eLoadSp.SELECT_WODETAILS_BY_WONO:
                    return "PROC_SELECT_WODETAILS_BY_WONO";
                case eLoadSp.INSERT_WO_ITEM:
                    return "PROC_WO_ITEM_INSERT_UPDATE";
                case eLoadSp.UPDATE_WO_ITEM:
                    return "PROC_WO_ITEM_INSERT_UPDATE";
                case eLoadSp.SELECT_WO_TYPE_ALL:
                    return "PROC_WO_TYPE_ALL";
                //case eLoadSp.INSERT_WO_TAX:
                //    return "PROC_INSERT_WO_TAX";
                //case eLoadSp.SELECT_WO_TAX_BY_WOID:
                //    return "SELECT_WO_TAX_BY_WOID";
                //case eLoadSp.UPDATE_WO_NETTOTAL:
                //    return "PROC_UPDATE_WO_NETTOTAL";
                case eLoadSp.DELETE_WO_ITEM_BY_ID:
                    return "PROC_DELETE_WO_ITEM_BY_ID";
                case eLoadSp.DELETE_SOW_ITEM_BY_ID:
                    return "PROC_DELETE_SOW_ITEM_BY_ID";

                //case eLoadSp.DELETE_WO_TAX_BY_ID:
                //    return "PROC_DELETE_WOTAX_BY_ID";
                //case eLoadSp.SELECT_WO_TAX_BY_TAXID:
                //    return "PROC_SELECT_WO_TAX_BY_TAXID";
                //case eLoadSp.UPDATE_WO_TAX:
                //    return "PROC_UPDATE_WO_TAX_BY_ID";
                //case eLoadSp.WOAWAITING_APPROVAL:
                //    return "PRO_WOAWAITING_APPROVAL";
                //case eLoadSp.UPDATE_APPROVAL_WOAWAITING:
                //    return "PRO_UPDATE_APPROVAL_WOAWAITING";
                //case eLoadSp.PROC_SELECT_WO_CREATING_ITEMS:
                //    return "PROC_SELECT_WO_CREATING_ITEMS_NEW";
                //case eLoadSp.SELECT_WO_ITEMWISE_TAX_BY_WO_ID:
                //    return "PROC_SELECT_WO_ITEMWISE_TAX_BY_WO_ID";
                case eLoadSp.WO_Delete:
                    return "PRO_DELETE_WO_BY_WONO";
                //case eLoadSp.UPDATE_WO_ITEM_QTY_BY_ID:
                //    return "PROC_UPDATE_WO_ITEM_BY_QTY";
                //case eLoadSp.SELECT_EXISTING_QTY_OF_INDENT:
                //    return "PROC_SELECT_EXISTING_QTY_OF_INDENT";
                //case eLoadSp.SELECT_FORTNIGHT_REWORT:
                //    return "PRO_Fortnight_ReWOrt";
                //case eLoadSp.CHECK_WONO_IN_MRN:
                //    return "PROC_CHECK_WONO_IN_MRN";
                //case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REWORT:
                //    return "PROC_SECTORWISE_FORT_NIGHT_REWORT";
                //case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REWORT_ALL:
                //    return "PROC_SECTORWISE_FORT_NIGHT_REWORT_ALL";
                //case eLoadSp.SELECT_FORTNIGHT_REWORT_DATE_BASED:
                //    return "PRO_Fortnight_ReWOrt_Date_Based";
                //case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REWORT_DATE_BASED:
                //    return "PROC_SECTORWISE_Date_Based_FORT_NIGHT_REWORT";
                //case eLoadSp.SELECT_WO_ITEMWISE_TAX_BY_WO_ID_FOR_PRINT:
                //    return "PROC_SELECT_WO_ITEMWISE_TAX_BY_WO_ID_For_Print";
                case eLoadSp.SELECT_WO_ITEMS_BY_WONO_FOR_PRINT:
                    return "PROC_WO_SELECT_For_Print";
                case eLoadSp.SELECT_WO_TAX_BY_WOID_FOR_PRINT:
                    return "SELECT_WO_TAX_BY_WOID_For_Print";
                //case eLoadSp.SELECT_WO_ITEMS_ALL_BY_PROJECT:
                //    return "PROC_WO_ITEMS_SELECT_BY_PROJECT";
                //case eLoadSp.SELECT_TRASWORT:
                //    return "proc_transWOrt";
                //case eLoadSp.UPDATE_WO_ITEM_OVERALL_AMOUNT_BY_ID:
                //    return "Proc_Update_WO_OverallTaxAmount";
                case eLoadSp.INSERT_SOW_ITEM:
                    return "PROC_SOW_ITEM_INSERT_UPDATE";
                //case eLoadSp.UPDATE_SOW_ITEM:
                //    return "PROC_SOW_ITEM_INSERT_UPDATE";
                case eLoadSp.SELECT_SOW_ITEMS_BY_WONO:
                    return "SELECT_SOW_ITEM_BY_ITEM_ID";
                case eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID:
                    return "SELECT_SOW_ITEM_BY_ITEM_ID";
                case eLoadSp.UPDATE_APROVEL_STAUSWO:
                    return "PRO_UPDATE_APROVEL_STAUSWO";
                case eLoadSp.UPDATE_APROVEL_STAUSWHO:
                    return "PRO_UPDATE_APROVEL_STAUSWHO";
                case eLoadSp.WOHNo_SELECT_BY_SubcotractorId:
                    return "PRO_WOHNo_SELECT_BY_SubcotractorId";
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
                    new SqlParameter("@WOID", this.WO_ID)
                     
                };
                    break;

                case eLoadSp.SELECT_WO_ITEMS_BY_WONO:
                case eLoadSp.SELECT_WO_ITEMS_BY_WONO_FOR_PRINT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@WOID", this.WO_ID),
                    new SqlParameter("@Task", this.Task)
                    
                };
                    break;

                case eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID:
                    //case eLoadSp.SELECT_SOW_ITEM_BY_ITEM_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@WOID", this.WO_ID),
                    new SqlParameter("@Task", this.Task)
                    
                };
                    break;

                case eLoadSp.SELECT_WO_ITEM_BY_ITEM_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@WOItemID", this.WO_Item_Id),
                    new SqlParameter("@Task", this.Task)
                     
                };

                    break;

                case eLoadSp.SELECT_WODETAILS_BY_WONO:
                case eLoadSp.WO_Delete:
                    //case eLoadSp.CHECK_WONO_IN_MRN:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@WONo", this.WONo)
                    
                };
                    break;
                case eLoadSp.SELECT_WO_TAX_BY_WOID:
                case eLoadSp.SELECT_WO_ITEMWISE_TAX_BY_WO_ID:
                //case eLoadSp.SELECT_WO_ITEMWISE_TAX_BY_WO_ID_FOR_PRINT:
                case eLoadSp.SELECT_WO_TAX_BY_WOID_FOR_PRINT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@WO_ID", this.WO_ID)
                  
                };

                    break;

                case eLoadSp.DELETE_WO_ITEM_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@WOItemID", this.WO_Item_Id),
                    new SqlParameter("@Task", this.Task)
                };

                    break;
                case eLoadSp.DELETE_SOW_ITEM_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@SOW_Item_Id", this.SOW_Item_Id),
                    new SqlParameter("@Task", this.Task)
                };

                    break;
                    
                         case eLoadSp.UPDATE_APROVEL_STAUSWHO:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@WONo", this.WONo),
                    new SqlParameter("@Approver_Com", this.Approver_Com),

                };

                    break;
                         case eLoadSp.WOHNo_SELECT_BY_SubcotractorId:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Subcontractor_id", this.SubContractorID),


                };

                    break;
                case eLoadSp.UPDATE_APROVEL_STAUSWO:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@WONo", this.WONo),
                    new SqlParameter("@Approver_Com", this.Approver_Com),

                };

                    break;
                case eLoadSp.PRO_WORK_ORDER_SELECT_ALL:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Project_Code", this.ProjectCode),
                        new SqlParameter("@Task", this.Task)
                    };
                    break;

                //case eLoadSp.DELETE_WO_TAX_BY_ID:
                //    colParams = new SqlParameter[]
                //{
                //    new SqlParameter("@WO_Tax_ID", this.WO_Tax_ID)

                //};

                //    break;
                //case eLoadSp.UPDATE_WO_ITEM_OVERALL_AMOUNT_BY_ID:
                //    colParams = new SqlParameter[]
                //{
                //    new SqlParameter("@WO_Tax_ID", this.WO_Tax_ID),
                //    new SqlParameter("@Amount", this.Amount)
                //};

                //    break;
                //case eLoadSp.SELECT_WO_TAX_BY_TAXID:
                //    colParams = new SqlParameter[]
                //{
                //    new SqlParameter("@WO_Tax_ID", this.WO_Tax_ID)

                //};

                //    break;
                case eLoadSp.WOAWAITING_APPROVAL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ApprovedBy", this.ApprovedBy),
                    new SqlParameter("@Project_Code",this.ProjectCode)
                     
                };

                    break;

                //case eLoadSp.PROC_SELECT_WO_CREATING_ITEMS:
                //    colParams = new SqlParameter[]
                //{
                //     new SqlParameter("@Indent_No", this.IndentNo),
                //      new SqlParameter("@VendorID", this.VendorID),
                //       new SqlParameter("@Task", this.Task),
                //       new SqlParameter("@Flag", this.Flag),
                //       new SqlParameter("@Project_Code",this.ProjectCode)
                //};
                //    break;


                //case eLoadSp.SELECT_EXISTING_QTY_OF_INDENT:
                //    colParams = new SqlParameter[]
                //{
                //     new SqlParameter("@IndentNo", this.IndentNo),
                //     new SqlParameter("@WOItemID", this.WO_Item_Id)

                //};
                //    break;
                //case eLoadSp.PRO_PURCHASE_ORDER_SELECT_ALL:
                //    colParams = new SqlParameter[]
                //{
                //    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value),
                //};
                //    break;
                //case eLoadSp.SELECT_FORTNIGHT_REWORT:
                //    colParams = new SqlParameter[]
                //{
                //    new SqlParameter("@ProjectCode", this.ProjectCode ??  (object)DBNull.Value),
                //    new SqlParameter("@Month", this.Month ??  (object)DBNull.Value),
                //    new SqlParameter("@Year", this.Year ??  (object)DBNull.Value)
                //};
                //    break;
                //case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REWORT:
                //    colParams = new SqlParameter[]
                //{
                //    new SqlParameter("@BudgetSector", this.BudgetSector),
                //    new SqlParameter("@ProjectCode", this.ProjectCode),
                //    new SqlParameter("@Month", this.Month ??  (object)DBNull.Value),
                //    new SqlParameter("@Year", this.Year ??  (object)DBNull.Value)

                //};
                //    break;
                //case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REWORT_ALL:
                //    colParams = new SqlParameter[]
                //{                   
                //    new SqlParameter("@ProjectCode", this.ProjectCode),
                //    new SqlParameter("@Month", this.Month ??  (object)DBNull.Value),
                //    new SqlParameter("@Year", this.Year ??  (object)DBNull.Value)

                //};
                //    break;
                //case eLoadSp.SELECT_FORTNIGHT_REWORT_DATE_BASED:
                //    colParams = new SqlParameter[]
                //{
                //    new SqlParameter("@ProjectCode", this.ProjectCode ??  (object)DBNull.Value),
                //    new SqlParameter("@StartDate", this.StartDate),
                //     new SqlParameter("@EndDate", this.EndDate)
                //};
                //    break;
                //case eLoadSp.SELECT_SECTORWISE_FORTNIGHT_REWORT_DATE_BASED:
                //    colParams = new SqlParameter[]
                //{
                //    new SqlParameter("@BudgetSector", this.BudgetSector),
                //     new SqlParameter("@ProjectCode", this.ProjectCode),
                //     new SqlParameter("@StartDate", this.StartDate),
                //     new SqlParameter("@EndDate", this.EndDate)

                //};
                //    break;
                //case eLoadSp.SELECT_WO_ITEMS_ALL_BY_PROJECT:
                //    colParams = new SqlParameter[]
                //{

                //   new SqlParameter("@Project_Code", this.ProjectCode)

                //};
                //    break;
                //case eLoadSp.SELECT_TRASWORT:
                //    colParams = new SqlParameter[]
                //{

                //   new SqlParameter("@WO_ID", this.WO_ID)

                //};
                //    break;

            }

            return colParams;
        }

        public bool WOTaxInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return WOTaxInsert(SqlConn, null, enumSpName);
        }

        private bool WOTaxInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@WO_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "WO_ID", DataRowVersion.Current, this.WO_ID),
               
                //new SqlParameter("@Type", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Type", DataRowVersion.Current, this.Type),
                //new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, this.Description),
                //new SqlParameter("@Type_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Perc", DataRowVersion.Current, this.Type_Perc),
                //new SqlParameter("@Type_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Amount", DataRowVersion.Current, this.Type_Amount)
				
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


        public bool updateWOTax(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateWOTax(SqlConn, null, enumSpName);
        }

        private bool updateWOTax(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                
                //new SqlParameter("@WO_Tax_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "@WO_Tax_ID", DataRowVersion.Current, this.WO_Tax_ID),
              
                // new SqlParameter("@WO_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "WO_ID", DataRowVersion.Current, this.WO_ID),
               
                //new SqlParameter("@Type", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Type", DataRowVersion.Current, this.Type),
                //new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, this.Description),
                //new SqlParameter("@Type_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Perc", DataRowVersion.Current, this.Type_Perc),
                //new SqlParameter("@Type_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Amount", DataRowVersion.Current, this.Type_Amount)
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

        public bool updateWONetToatl(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateWONetToatl(SqlConn, null, enumSpName);
        }

        private bool updateWONetToatl(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                
                new SqlParameter("@WO_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "WO_ID", DataRowVersion.Current, this.WO_ID)
              
				
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


        public bool UpdateApprovalWO(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return UpdateApprovalWO(SqlConn, null, enumSpName);
        }

        public bool UpdateApprovalWO(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return UpdateApprovalWO(null, SqlTran, enumSpName);
        }

        private bool UpdateApprovalWO(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 16;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@WONo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "WONo", DataRowVersion.Current, this.WONo),
                 //new SqlParameter("@Approval_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Approval_Date", DataRowVersion.Current, this.Approval_Date),
                 //new SqlParameter("@Approver_Com", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Approver_Com", DataRowVersion.Current, this.Approver_Com),
                 //new SqlParameter("@ApprovalStatus", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "ApprovalStatus", DataRowVersion.Current, this.ApprovalStatus),
            
				
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
                new SqlParameter("@WONo", SqlDbType.VarChar, 30, ParameterDirection.Output, false, 0, 0, "WONo", DataRowVersion.Current, this.WONo),
                new SqlParameter("@WOID", SqlDbType.Int,4, ParameterDirection.Output, false, 0, 0, "WO_ID", DataRowVersion.Current, this.WO_ID),
                new SqlParameter("@WODate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "WODate", DataRowVersion.Current, this.WODate),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode),
                new SqlParameter("@WOTypeID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "WOTypeID", DataRowVersion.Current, this.WOTypeID),
                new SqlParameter("@SubContractorID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "SubContractorID", DataRowVersion.Current, this.SubContractorID),
                new SqlParameter("@WorkLocation", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "WorkLocation", DataRowVersion.Current, this.WorkLocation),
                new SqlParameter("@DurationOfWork", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "DurationOfWork", DataRowVersion.Current, this.DurationOfWork),
                new SqlParameter("@PaymentTerms", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "PaymentTerms", DataRowVersion.Current, this.PaymentTerms),
                new SqlParameter("@GST_No", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "GST_No", DataRowVersion.Current, this.GST_No),
                new SqlParameter("@ApprovedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "ApprovedBy", DataRowVersion.Current, this.ApprovedBy),
                new SqlParameter("@Status", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Remarks", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Remarks", DataRowVersion.Current, this.Remarks),
                new SqlParameter("@Others", SqlDbType.VarChar,2000, ParameterDirection.Input, false, 10, 0, "Others", DataRowVersion.Current, this.Others),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@Prepared_By", SqlDbType.Int,4, ParameterDirection.Input, false,0, 0, "Prepared_By", DataRowVersion.Current, this.UserID),
                new SqlParameter("@ContactName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactName", DataRowVersion.Current, this.ContactName),
                new SqlParameter("@ContactNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactNo", DataRowVersion.Current, this.ContactNo),
                new SqlParameter("@ScopeOfWork", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 10, 0, "ScopeOfWork", DataRowVersion.Current, this.ScopeOfWork),
                new SqlParameter("@TDSPerc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "TDSPerc", DataRowVersion.Current, this.TDSPerc),
                new SqlParameter("@FYear", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "FYear", DataRowVersion.Current, this.FYear),
                new SqlParameter("@OrderType", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "OrderType", DataRowVersion.Current, this.OrderType),
                new SqlParameter("@Month", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Month", DataRowVersion.Current, this.Month),
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

                    this.WONo = (string)colParams.First().Value;
                    this.WO_ID = (int)colParams[1].Value;
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

        public bool insertItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertItem(SqlConn, null, enumSpName);
        }

        public bool insertItem(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insertItem(null, SqlTran, enumSpName);
        }

        private bool insertItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@WONo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "WONo", DataRowVersion.Current, this.WONo),
                new SqlParameter("@Item_Type", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Item_Type", DataRowVersion.Current, this.Item_Type),
                new SqlParameter("@Item_Desc", SqlDbType.VarChar, 6000, ParameterDirection.Input, false, 0, 0, "Item_Desc", DataRowVersion.Current, this.Item_Desc),
                new SqlParameter("@Item_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Item_Perc", DataRowVersion.Current, this.Item_Perc),
                new SqlParameter("@Rate", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Rate", DataRowVersion.Current, this.Item_Rate),
                new SqlParameter("@UOM", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "UOM", DataRowVersion.Current, this.Item_UOM),
                new SqlParameter("@Item_Qty", SqlDbType.Decimal, 10, ParameterDirection.Input, false, 10, 0, "Item_Qty", DataRowVersion.Current, this.Item_Qty),
                new SqlParameter("@Total_Amt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Total_Amt", DataRowVersion.Current, this.Item_Amt),
                new SqlParameter("@Gst_Exist", SqlDbType.Bit, 9, ParameterDirection.Input, false, 10, 0, "Gst_Exist", DataRowVersion.Current, this.Gst_Exist),
                new SqlParameter("@Igst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Igst_Perc", DataRowVersion.Current, this.Igst_Perc),
                new SqlParameter("@Cgst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Cgst_Perc", DataRowVersion.Current, this.Cgst_Perc),
                new SqlParameter("@Sgst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Sgst_Perc", DataRowVersion.Current, this.Sgst_Perc),
                new SqlParameter("@Sub_Item_Desc", SqlDbType.VarChar, 6000, ParameterDirection.Input, false, 0, 0, "Sub_Item_Desc", DataRowVersion.Current, this.Sub_Item_Desc),
                new SqlParameter("@WO_Item_Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "WO_Item_Id", DataRowVersion.Current, this.WO_Item_Id),
                new SqlParameter("@Parent_WO_Item_Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Parent_WO_Item_Id", DataRowVersion.Current, this.Parent_WO_Item_Id)


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

                    //this.WONo = (string)colParams.First().Value;
                    //this.WO_ID = (int)colParams[1].Value;
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
        public bool insertSOWItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertSOWItem(SqlConn, null, enumSpName);
        }

        public bool insertSOWItem(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insertSOWItem(null, SqlTran, enumSpName);
        }

        private bool insertSOWItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@WO_ID", SqlDbType.Int, 10, ParameterDirection.Input, false, 0, 0, "WO_ID", DataRowVersion.Current, this.WO_ID),
                new SqlParameter("@Scope_Of_Work", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Scope_Of_Work", DataRowVersion.Current, this.Scope_Of_Work),
                new SqlParameter("@Quantity", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Quantity", DataRowVersion.Current, this.Quantity),
                new SqlParameter("@Rate", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Rate", DataRowVersion.Current, this.Rate),
                new SqlParameter("@UOM", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "UOM", DataRowVersion.Current, this.UMO),
                new SqlParameter("@WONo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "WONo", DataRowVersion.Current, this.WONo),
               


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

                    //this.WONo = (string)colParams.First().Value;
                    //this.WO_ID = (int)colParams[1].Value;
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

        public bool updateWOItemQty(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateWOItemQty(SqlConn, null, enumSpName);
        }



        private bool updateWOItemQty(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{               
               //new SqlParameter("@WO_Item_Id", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "WO_Item_Id", DataRowVersion.Current, this.WO_Item_Id),
               //new SqlParameter("@QtyRequired", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Qty_required", DataRowVersion.Current, this.Qty_required),
               //new SqlParameter("@WOID", this.WO_ID),
               //new SqlParameter("@VendorID",this.SubContractorID),
               //new SqlParameter("@Flag",this.Flag),
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

        private bool updateWOOverallAmount(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
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

        public bool updateWOOverallAmount(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateWOOverallAmount(SqlConn, null, enumSpName);
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
                new SqlParameter("@WONo", SqlDbType.VarChar, 50, ParameterDirection.InputOutput, false, 0, 0, "WONo", DataRowVersion.Current, this.WONo),
                new SqlParameter("@WODate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "WODate", DataRowVersion.Current, this.WODate),
                new SqlParameter("@WOTypeID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "WOTypeID", DataRowVersion.Current, this.WOTypeID),
                new SqlParameter("@SubContractorID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "SubContractorID", DataRowVersion.Current, this.SubContractorID),
                new SqlParameter("@WorkLocation", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "WorkLocation", DataRowVersion.Current, this.WorkLocation),
                new SqlParameter("@DurationOfWork", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "DurationOfWork", DataRowVersion.Current, this.DurationOfWork),
                new SqlParameter("@PaymentTerms", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "PaymentTerms", DataRowVersion.Current, this.PaymentTerms),
                new SqlParameter("@GST_No", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "GST_No", DataRowVersion.Current, this.GST_No),
                new SqlParameter("@ApprovedBy", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "ApprovedBy", DataRowVersion.Current, this.ApprovedBy),
                new SqlParameter("@Status", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Remarks", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Remarks", DataRowVersion.Current, this.Remarks),
                new SqlParameter("@Others", SqlDbType.VarChar,2000, ParameterDirection.Input, false, 10, 0, "Others", DataRowVersion.Current, this.Others),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@Prepared_By", SqlDbType.Int,4, ParameterDirection.Input, false,0, 0, "Prepared_By", DataRowVersion.Current, this.UserID),
                new SqlParameter("@ContactName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactName", DataRowVersion.Current, this.ContactName),
                new SqlParameter("@ContactNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "ContactNo", DataRowVersion.Current, this.ContactNo),
                new SqlParameter("@ScopeOfWork", SqlDbType.VarChar, 5000, ParameterDirection.Input, false, 10, 0, "ScopeOfWork", DataRowVersion.Current, this.ScopeOfWork),
                new SqlParameter("@TDSPerc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "TDSPerc", DataRowVersion.Current, this.TDSPerc),
                new SqlParameter("@Uploaded_File", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Uploaded_File", DataRowVersion.Current, this.Uploaded_File),
                new SqlParameter("@FYear", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "FYear", DataRowVersion.Current, this.FYear),
                new SqlParameter("@Month", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Month", DataRowVersion.Current, this.Month),
			};

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    this.WONo = (string)colParams.First().Value;
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

        public bool updateItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateItem(SqlConn, null, enumSpName);
        }

        public bool updateItem(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return updateItem(null, SqlTran, enumSpName);
        }

        private bool updateItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{   
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@WONo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "WONo", DataRowVersion.Current, this.WONo),
                new SqlParameter("@Item_Type", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Item_Type", DataRowVersion.Current, this.Item_Type),
                new SqlParameter("@Item_Desc", SqlDbType.VarChar, 6000, ParameterDirection.Input, false, 0, 0, "Item_Desc", DataRowVersion.Current, this.Item_Desc),
                new SqlParameter("@Item_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Item_Perc", DataRowVersion.Current, this.Item_Perc),
                new SqlParameter("@Rate", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Rate", DataRowVersion.Current, this.Item_Rate),
                new SqlParameter("@UOM", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "UOM", DataRowVersion.Current, this.Item_UOM),
                new SqlParameter("@Item_Qty", SqlDbType.Decimal, 10, ParameterDirection.Input, false, 10, 0, "Item_Qty", DataRowVersion.Current, this.Item_Qty),
                new SqlParameter("@Total_Amt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Total_Amt", DataRowVersion.Current, this.Item_Amt),
                new SqlParameter("@Gst_Exist", SqlDbType.Bit, 9, ParameterDirection.Input, false, 10, 0, "Gst_Exist", DataRowVersion.Current, this.Gst_Exist),
                new SqlParameter("@Igst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Igst_Perc", DataRowVersion.Current, this.Igst_Perc),
                new SqlParameter("@Cgst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Cgst_Perc", DataRowVersion.Current, this.Cgst_Perc),
                new SqlParameter("@Sgst_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Sgst_Perc", DataRowVersion.Current, this.Sgst_Perc)

			};

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    //this.WONo = (string)colParams.First().Value;
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


        public WorkOrderBL()
        {
            //Set default values
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
