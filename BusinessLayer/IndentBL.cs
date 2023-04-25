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
    public class IndentBL
    {
       
        #region "Class: IndentBL Local Declarations"

        public enum eLoadSp
        {
            INSERT = 0,
            SELECT_BUDGETID_ALL = 1,           
            UPDATE = 2,          
            SELECT_USERNAMES_ALL = 3,
            SELECT_INDENT_ALL = 4,
            SELECT_INDENTDETAILS_BY_ID = 5,
            INSERT_INDENT_ITEMS = 6,
            SELECT_ITEMCODE_BY_CATEGORY_ID = 7,
            SELECT_UOM_BY_ITEMCODE = 8,
            SELECT_INDENT_ITEMS_BY_INDENT_NO = 9,
            SELECT_INDENT_ITEM_BY_INDENT_ID = 10,
            DELETE_INDENT_ITEM_BY_ID = 11,
            UPDATE_INDENT_ITEM_BY_ID = 12,
            SELECT_INDENTPROCESS_By_UserID =  13,
            SELECT_QUOTAION_COMPARSIONS_BY_ITEMORINDENT = 14,
            SELECT_INDENT_ALL_NEW=15,
            DELETE_INDENT_BY_INDENTNO = 17,
            CHECK_INDENTNO_IN_PO = 18,
            SELECT_MATERIAL_FROM_BUDGET_ITEM = 19,
            SELECT_ITEMS_FROM_BUDGET_ITEM = 20,
            SELECT_BUDGET_QUANTITY = 21,
            SELECT_INDENT_FOR_QUOTATIONCOMPARE = 22,
            SELECT_TINNO_FROM_LOCATION_BY_INDENTNO = 23,
            SELECT_INDENT_NO_FOR_PO = 24,
            SELECT_QTY_ALREADY_RECEIVED_IN_MRN = 25,
            SELECT_ALREADY_EXISTING_QTY_FOR_THIS_BUDGET = 26,
            SELECT_BUDGET_SECTORS_BY_ID = 27,
            SELECT_BUDGET_ITEMS_BY_SECTORNAME = 28,
            INSERT_TO_INDENT_ITEM_FROM_BUDGET_ITEM = 29,
            SELECT_ALL_INDENT_ITEMS_LIST_BY_Project = 30,
            GET_PROJECT_BUDGET_QTY = 31,
            GET_CATEGORY_BASED_QTY_FOR_BOQ_ITEMS = 32,
            SELECT_QUOTAION_COMPARSIONS_BY_ITEMS = 33
            ,GetTotalRunningHours,
            SELECT_INDENTDATE_ONINDENTNUMBER=35
        };

        #endregion

        #region "Class: IndentBL Sets / Gets"


        public string Indent_No
        {
            get;
            set;
        }
        public string VendorID
        {
            get;
            set;
        }

        public DateTime Ind_date
        { get; set; }
        public bool Specific_quotation
        {
            get;
            set;
        }
        public int Prep_By
        { get; set; }
        public int? Stock_check_By
        { get; set; }
        public int? HO_approver
        { get; set; }
        public int? Process_By
        { get; set; }
        public int Location
        { get; set; }
        public string Qty_Spec
        {
            get;
            set;
        }
        public string Process_From
        {
            get;
            set;
        }
        public string NOTE
        { get; set; }
        public string Project_Code
        { get; set; }
        public string Budget_ID
        { get; set; }
        public int AbstractBudget_ID
        { get; set; }
        public string Status
        { get; set; }

        public string Remark
        { get; set; }
      
        //Indent Item Details

        public int Indent_Item_Id
        { get; set; }
       
        public int Mat_Cat_Id
        { get; set; }
        public string Item_Code
        { get; set; }
        public bool? BOQ
        { get; set; }
        public string BOQ_No
        { get; set; }
        public string Total_Qty_Invoked
        { get; set; }
        public decimal Qty_Available
        { get; set; }
        public decimal Qty_required
        { get; set; }
        public DateTime? Tentative_Date
        { get; set; }
        public bool Whether_Req_Qty
        { get; set; }
        public string Remarks
        { get; set; }
        public string UOM
        { get; set; }
        public decimal Qty_AlreadyRecevied
        { get; set; }
        public int UserID
        { get; set; }
        public string BudgetSector
        { get; set; }
        public int BudgetItemID
        { get; set; }
        public decimal Total_Qty_Recevied
        { get; set; }
        #endregion


        #region "Class: IndentBL Methods"

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
                        this.Indent_No = dr["Indent_No"].ToString();
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
                        this.UOM = dr["UOMPrefix"].ToString();
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
                    return "PROC_INDENT_INSERT";
                case eLoadSp.SELECT_BUDGETID_ALL:
                    return "PROC_SELECT_BUDGETID_BY_PROJECT";           
                case eLoadSp.UPDATE:
                    return "PROC_INDENT_UPDATE";                
                case eLoadSp.SELECT_USERNAMES_ALL:
                    return "PROC_SELECT_USERNAME_ALL";
                case eLoadSp.SELECT_INDENT_ALL:
                    return "PROC_SELECT_INDENT_ALL";
                case eLoadSp.SELECT_INDENTDETAILS_BY_ID:
                    return "PROC_SELECT_INDENTDETAILS_BY_ID";
                case eLoadSp.INSERT_INDENT_ITEMS:
                    return "PROC_INDENT_ITEM_INSERT";
                case eLoadSp.SELECT_ITEMCODE_BY_CATEGORY_ID:
                    return "PROC_SELECT_ITEMCODE_BY_CATID";
                case eLoadSp.SELECT_UOM_BY_ITEMCODE:
                    return "PROC_SELECT_UOM_BY_ITEMCODE";
                case eLoadSp.SELECT_INDENT_ITEMS_BY_INDENT_NO:
                    return "PROC_SELECT_INDENT_ITEMS";
                case eLoadSp.SELECT_INDENT_ITEM_BY_INDENT_ID:
                    return "PROC_SELECT_INDENT_ITEM_BY_ID";
                case eLoadSp.DELETE_INDENT_ITEM_BY_ID:
                    return "PROC_DELETE_INDENT_ITEM_BY_ID";
                case eLoadSp.UPDATE_INDENT_ITEM_BY_ID:
                    return "PROC_UPDATE_INDENT_ITEM_BY_ID";
                case eLoadSp.SELECT_INDENTPROCESS_By_UserID:
                    return "PRO_Indent_Process_By_ProcessBy";
                case eLoadSp.SELECT_QUOTAION_COMPARSIONS_BY_ITEMORINDENT:
                    return "PRO_QuotationCompartion_By_Indent_OR_Cate_Item";
                case eLoadSp.SELECT_QUOTAION_COMPARSIONS_BY_ITEMS:
                    return "PRO_QuotationCompartion_By_Cate_Item";
                   
                case eLoadSp.SELECT_INDENT_ALL_NEW:
                    return "PROC_SELECT_INDENT_ALL_NEW";
                case eLoadSp.DELETE_INDENT_BY_INDENTNO:
                    return "PRO_INDENT_DELETE_INDENTNo";
                case eLoadSp.CHECK_INDENTNO_IN_PO:
                    return "PROC_CHECK_INDENTNO_IN_PO";
                case eLoadSp.SELECT_MATERIAL_FROM_BUDGET_ITEM:
                    return "PROC_SELECT_MATERIAL_FROM_BUDGET_ITEM";
                case eLoadSp.SELECT_ITEMS_FROM_BUDGET_ITEM:
                    return "PROC_SELECT_BUDGET_ITEMS_BY_ID";
                case eLoadSp.SELECT_BUDGET_QUANTITY:
                    return "PROC_SELECT_BUDGET_QUANTITY_BY_ID";
                case eLoadSp.SELECT_INDENT_FOR_QUOTATIONCOMPARE:
                    return "PRO_SELECT_INDENT_FOR_QUOTATIONCOMPARE";
                case eLoadSp.SELECT_TINNO_FROM_LOCATION_BY_INDENTNO:
                    return "PROC_GET_TINNO_FROM_LOCATION_BY_INDENTNO";
                case eLoadSp.SELECT_INDENT_NO_FOR_PO:
                    return "PROC_SELECT_INDENT_NO_FOR_PO";
                case eLoadSp.SELECT_QTY_ALREADY_RECEIVED_IN_MRN:
                    return "PROC_SELECT_QTY_ALREADY_RECEIVED";
                case eLoadSp.SELECT_ALREADY_EXISTING_QTY_FOR_THIS_BUDGET:
                    return "PROC_SELECT_ALREADY_EXISTING_QTY_FOR_BUDGET";
                case eLoadSp.SELECT_BUDGET_SECTORS_BY_ID:
                    return "PROC_SELECT_BUDGET_SECTOR_BY_ABS_ID";
                case eLoadSp.SELECT_BUDGET_ITEMS_BY_SECTORNAME:
                    return "PROC_SELECT_BUDGET_ITEMS_BY_SECTOR_NAME";
                case eLoadSp.INSERT_TO_INDENT_ITEM_FROM_BUDGET_ITEM:
                    return "PROC_INDENT_ITEM_INSERT_FROM_BUDGET_ITEM";
                case eLoadSp.SELECT_ALL_INDENT_ITEMS_LIST_BY_Project:
                    return "PROC_INDENT_ITEMS_SELECT_ALL_By_PROJECT";
                case eLoadSp.GET_PROJECT_BUDGET_QTY:
                    return "PROC_GET_PROJECT_BUDGET_QTY";
                case eLoadSp.GET_CATEGORY_BASED_QTY_FOR_BOQ_ITEMS:
                    return "PROC_GET_CATEGORY_BASED_QTY_FOR_BOQ_ITEMS";
                case eLoadSp.SELECT_INDENTDATE_ONINDENTNUMBER:
                    return "PROC_SELECT_INDENTDATE_ONINDENTNUMBER";
                case eLoadSp.GetTotalRunningHours:
                    return "PROC_TotalRunningHours";
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
                case eLoadSp.SELECT_BUDGETID_ALL:
                case eLoadSp.SELECT_INDENT_FOR_QUOTATIONCOMPARE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code ??  (object)DBNull.Value)
                };
                    break;

                case eLoadSp.SELECT_INDENT_ITEMS_BY_INDENT_NO:
                case eLoadSp.SELECT_TINNO_FROM_LOCATION_BY_INDENTNO:
                case eLoadSp.SELECT_INDENTDETAILS_BY_ID:
                case eLoadSp.DELETE_INDENT_BY_INDENTNO:
                case eLoadSp.CHECK_INDENTNO_IN_PO:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Indent_No", this.Indent_No ??  (object)DBNull.Value)
                };
                    break;

                case eLoadSp.SELECT_ITEMCODE_BY_CATEGORY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Mat_Cat_Id", this.Mat_Cat_Id)
                };
                    break;

                case eLoadSp.SELECT_UOM_BY_ITEMCODE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Item_Code", this.Item_Code),
                    //new SqlParameter("@AssetCode", this.AssetCode),
                    //new SqlParameter("@Task", this.Task),
                    //new SqlParameter("@date", this.date),
                   //new SqlParameter("@Output", SqlDbType.Decimal, 10, ParameterDirection.Output, false, 0, 0, "Output", DataRowVersion.Current, this.Total_Qty_Recevied)
                };
                    break;             
                  

                case eLoadSp.SELECT_INDENT_ITEM_BY_INDENT_ID:
                case eLoadSp.DELETE_INDENT_ITEM_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Indent_Item_Id", this.Indent_Item_Id)
                };
                    break;
         
                case eLoadSp.SELECT_INDENTPROCESS_By_UserID:
                    colParams = new SqlParameter[]
                {
                  new SqlParameter("@Process_By", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Process_By", DataRowVersion.Current, this.Process_By ?? (object)DBNull.Value),
                   new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),

                };
                    break;
                case eLoadSp.SELECT_QUOTAION_COMPARSIONS_BY_ITEMORINDENT:
                    colParams = new SqlParameter[]
                {
                  
                     new SqlParameter("@Indent_No", this.Indent_No ??  (object)DBNull.Value),
                      new SqlParameter("@Specific_quotation", this.Specific_quotation ),
                       new SqlParameter("@Project_Code", this.Project_Code )
                };
                    break;
                case eLoadSp.SELECT_QUOTAION_COMPARSIONS_BY_ITEMS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ItemCode", this.Item_Code ??  (object)DBNull.Value),
                    new SqlParameter("@CateID", this.Mat_Cat_Id),
                    
                };
                    break;

                case eLoadSp.SELECT_INDENT_ALL_NEW:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@IndentNo", this.Indent_No ??  (object)DBNull.Value),
                      //new SqlParameter("@UserID", this.UserID),
                      new SqlParameter("@Project_Code", this.Project_Code),
                };
                    break;
                case eLoadSp.SELECT_INDENTDATE_ONINDENTNUMBER:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@Indent_No", this.Indent_No ??  (object)DBNull.Value),
                      //new SqlParameter("@UserID", this.UserID),
                   //   new SqlParameter("@Project_Code", this.Project_Code),
                };
                    break;

                case eLoadSp.SELECT_MATERIAL_FROM_BUDGET_ITEM:
            
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@AbstractBudgetID", this.AbstractBudget_ID),
                     new SqlParameter("@BudgetSector", this.BudgetSector)
                };
                    break;
             
                case eLoadSp.SELECT_BUDGET_SECTORS_BY_ID:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@AbstractBudgetID", this.AbstractBudget_ID)
                };
                    break;

                case eLoadSp.SELECT_ITEMS_FROM_BUDGET_ITEM:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@AbstractBudgetID", this.AbstractBudget_ID),
                     new SqlParameter("@MatcatID", this.Mat_Cat_Id)
                };
                    break;
                case eLoadSp.SELECT_BUDGET_QUANTITY:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@AbstractBudgetID", this.AbstractBudget_ID),
                     new SqlParameter("@ItemCode", this.Item_Code)
                };
                    break;
                case eLoadSp.SELECT_QTY_ALREADY_RECEIVED_IN_MRN:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@ProjectCode", this.Project_Code),
                     new SqlParameter("@ItemCode", this.Item_Code),
                     new SqlParameter("@IndentNo", this.Indent_No)
                };
                    break;
                case eLoadSp.SELECT_INDENT_NO_FOR_PO:
                   
                case eLoadSp.SELECT_ALL_INDENT_ITEMS_LIST_BY_Project:
                    colParams = new SqlParameter[]
                {
                     
                     new SqlParameter("@Project_Code", this.Project_Code)
                  
                };
                    break;
                case eLoadSp.SELECT_ALREADY_EXISTING_QTY_FOR_THIS_BUDGET:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@Project_Code", this.Project_Code),
                     new SqlParameter("@Item_Code", this.Item_Code),
                     new SqlParameter("@AbstractBudget_ID", this.AbstractBudget_ID),
                      new SqlParameter("@Indent_No", this.Indent_No),
                       new SqlParameter("@Budget_ID", this.Budget_ID)
                };
                    break;

                case eLoadSp.SELECT_BUDGET_ITEMS_BY_SECTORNAME:

                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@AbstractBudgetID", this.AbstractBudget_ID),
                     new SqlParameter("@BudgetSector", this.BudgetSector)
                };
                    break;

                case eLoadSp.GET_PROJECT_BUDGET_QTY:

                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@Project_Code", this.Project_Code),
                     new SqlParameter("@BudgetSector", this.BudgetSector)
                };
                    break;

                case eLoadSp.GET_CATEGORY_BASED_QTY_FOR_BOQ_ITEMS:

                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@Project_Code", this.Project_Code),
                     new SqlParameter("@BudgetSector", this.BudgetSector),
                     new SqlParameter("@Mat_Cat_Id", this.Mat_Cat_Id)
                };
                    break;

            }

            return colParams;
        }

        public bool ReferBudgetIteminsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ReferBudgetIteminsert(SqlConn, null, enumSpName);
        }

        private bool ReferBudgetIteminsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Indent_No", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Indent_No", DataRowVersion.Current, this.Indent_No),
                new SqlParameter("@BudgetItemID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Bud_Item_Id", DataRowVersion.Current, this.BudgetItemID),
                new SqlParameter("@Item_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code),
                 new SqlParameter("@Project_Code", SqlDbType.VarChar,50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                  new SqlParameter("@AbstractBudget_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AbstractBudget_ID", DataRowVersion.Current, this.AbstractBudget_ID)
              
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

        public bool IndentIteminsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return IndentIteminsert(SqlConn, null, enumSpName);
        }

        private bool IndentIteminsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Indent_No", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Indent_No", DataRowVersion.Current, this.Indent_No),
                new SqlParameter("@Mat_Cat_Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Mat_Cat_Id", DataRowVersion.Current, this.Mat_Cat_Id),
                new SqlParameter("@Item_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code),
                new SqlParameter("@BOQ", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "BOQ", DataRowVersion.Current, this.BOQ ?? (object)DBNull.Value),
                new SqlParameter("@BOQ_No", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "BOQ_No", DataRowVersion.Current, this.BOQ_No ?? (object)DBNull.Value ),
                new SqlParameter("@Recurring", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Recurring", DataRowVersion.Current, this.Recurring ?? (object)DBNull.Value),
                new SqlParameter("@AssetCode", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "AssetCode", DataRowVersion.Current, this.AssetCode ?? (object)DBNull.Value ),
                new SqlParameter("@Total_Qty_Invoked", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Total_Qty_Involved", DataRowVersion.Current, this.Total_Qty_Invoked),
                new SqlParameter("@Qty_Available", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Qty_Available", DataRowVersion.Current, this.Qty_Available),
                new SqlParameter("@Qty_required", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Qty_required", DataRowVersion.Current, this.Qty_required),
                new SqlParameter("@Tentative_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Tentative_Date", DataRowVersion.Current, this.Tentative_Date ?? (object)DBNull.Value ),
                new SqlParameter("@Whether_Req_Qty", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Whether_Req_Qty", DataRowVersion.Current, this.Whether_Req_Qty ),
                new SqlParameter("@Remarks", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "Remarks", DataRowVersion.Current, this.Remarks ?? (object)DBNull.Value),
				new SqlParameter("@AbstractBudgetID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Abs_bud_ID", DataRowVersion.Current, this.AbstractBudget_ID),
                new SqlParameter("@Total_Qty_Recevied", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Total_Qty_Recevied", DataRowVersion.Current, this.Total_Qty_Recevied)
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

        public bool updateIndentItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateIndentItem(SqlConn, null, enumSpName);
        }

        public bool updateIndentItem(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return updateIndentItem(null, SqlTran, enumSpName);
        }
      
        private bool updateIndentItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Indent_Item_Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Indent_Item_Id", DataRowVersion.Current, this.Indent_Item_Id),
                 new SqlParameter("@Indent_No", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Indent_No", DataRowVersion.Current, this.Indent_No),
                new SqlParameter("@Mat_Cat_Id", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Mat_Cat_Id", DataRowVersion.Current, this.Mat_Cat_Id),
                new SqlParameter("@Item_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code),
                new SqlParameter("@BOQ", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "BOQ", DataRowVersion.Current, this.BOQ ?? (object)DBNull.Value ),
                new SqlParameter("@BOQ_No", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "BOQ_No", DataRowVersion.Current, this.BOQ_No ?? (object)DBNull.Value ),
                new SqlParameter("@Recurring", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Recurring", DataRowVersion.Current, this.Recurring ?? (object)DBNull.Value),
                new SqlParameter("@AssetCode", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "AssetCode", DataRowVersion.Current, this.AssetCode ?? (object)DBNull.Value ),
                new SqlParameter("@Total_Qty_Invoked", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Total_Qty_Invoked", DataRowVersion.Current, this.Total_Qty_Invoked),
                new SqlParameter("@Qty_Available", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Qty_Available", DataRowVersion.Current, this.Qty_Available),
                new SqlParameter("@Qty_required", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Qty_required", DataRowVersion.Current, this.Qty_required),
                new SqlParameter("@Tentative_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Tentative_Date", DataRowVersion.Current, this.Tentative_Date ?? (object)DBNull.Value ),
                new SqlParameter("@Whether_Req_Qty", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "Whether_Req_Qty", DataRowVersion.Current, this.Whether_Req_Qty ),
                new SqlParameter("@Remarks", SqlDbType.VarChar, 150, ParameterDirection.Input, false, 0, 0, "Remarks", DataRowVersion.Current, this.Remarks ?? (object)DBNull.Value),
                new SqlParameter("@AbstractBudgetID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Abs_bud_ID", DataRowVersion.Current, this.AbstractBudget_ID),
				new SqlParameter("@Total_Qty_Recevied", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Total_Qty_Recevied", DataRowVersion.Current, this.Total_Qty_Recevied)
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
        /// <summary>
        /// Fuction Name:   Insert.
        /// Called By:      Nill.
        /// Description:    Check the Sql transaction.
        /// Change histroy: Nill.
        /// </summary>
        /// <param name="SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool insert(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insert(null, SqlTran, enumSpName);
        }
        /// <summary>
        /// Fuction Name:   Insert.
        /// Called By:      Nill.
        /// Description:    Inserted values to table
        /// Change histroy: Nill.
        /// </summary>
        /// <param name="SqlTran,SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        private bool insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
				
                new SqlParameter("@Indent_No", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "Indent_No", DataRowVersion.Current, this.Indent_No),
                new SqlParameter("@Ind_date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Ind_date", DataRowVersion.Current, this.Ind_date),
                new SqlParameter("@Prep_By", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "Prep_By", DataRowVersion.Current, this.Prep_By),
                new SqlParameter("@Stock_check_By", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Stock_check_By", DataRowVersion.Current, this.Stock_check_By ?? (object)DBNull.Value),
                new SqlParameter("@Qty_Spec", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Qty_Spec", DataRowVersion.Current, this.Qty_Spec ?? (object)DBNull.Value),
                new SqlParameter("@HO_approver", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "HO_approver", DataRowVersion.Current, this.HO_approver ?? (object)DBNull.Value),
                new SqlParameter("@Process_From", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Process_From", DataRowVersion.Current, this.Process_From),
                new SqlParameter("@Process_By", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Process_By", DataRowVersion.Current, this.Process_By ?? (object)DBNull.Value),
                new SqlParameter("@Location", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "Location", DataRowVersion.Current, this.Location),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@Budget_ID", SqlDbType.VarChar, 12, ParameterDirection.Input, false, 0, 0, "Budget_ID", DataRowVersion.Current, this.Budget_ID),
                new SqlParameter("@Status", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Remark", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Remark", DataRowVersion.Current, this.Remark ?? (object)DBNull.Value),
                new SqlParameter("@NOTE", this.NOTE)


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

                    this.Indent_No = (string)colParams.First().Value;
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
        ////////////////////////////////////////////////////////////////////////

        public bool GetTotalRunningHours(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return GetTotalRunningHours(SqlConn, null, enumSpName);
        }
        public bool GetTotalRunningHours(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return GetTotalRunningHours(null, SqlTran, enumSpName);
        }
        /// <summary>
        /// Fuction Name:   Insert.
        /// Called By:      Nill.
        /// Description:    Inserted values to table
        /// Change histroy: Nill.
        /// </summary>
        /// <param name="SqlTran,SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        private bool GetTotalRunningHours(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Output", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, "Output", DataRowVersion.Current, this.Total_Qty_Invoked),
                new SqlParameter("@date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "date", DataRowVersion.Current, this.date),
                new SqlParameter("@Item_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code ?? (object)DBNull.Value),
                new SqlParameter("@AssetCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "AssetCode", DataRowVersion.Current, this.AssetCode ?? (object)DBNull.Value),

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


                    this.Total_Qty_Invoked = colParams.First().Value.ToString();
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
        /// <summary>
        /// Fuction Name:   load.
        /// Called by:      Nill.
     
        
        
        ///////////////////////////////////////////////////////////////
        
        
        /// <summary>
        /// Fuction Name:   load.
        /// Called by:      Nill.
        /// Description:    Load the values from data reader.
        /// Called By:      Nill.
        /// Changes history:Nill.
        /// </summary>
        /// <param name="SqlConn,"></param>
        /// <param name="enumSpName"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Fuction Name:   load.
        /// Called by:      Nill.
        /// Description:    Load the values from enumSpName parameter.
        /// Called By:      Nill.
        /// Changes history:Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Fuction Name:   load.
        /// Called by:      Nill.
        /// Description:    Load the values without parameter.
        /// Called By:      Nill.
        /// Changes history:Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <param name="dr"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Fuction Name: delete.
        /// Called By:  Nill.
        /// Description: it is used to delete the data from table.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Fuction Name: update.
        /// Called By:  Nill.
        /// Description: check the sql connection.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool update(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return update(SqlConn, null, enumSpName);
        }
        /// <summary>
        /// Fuction Name: update.
        /// Called By:  Nill.
        /// Description: check the sql transaction.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool update(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return update(null, SqlTran, enumSpName);
        }
        /// <summary>
        /// Fuction Name: update.
        /// Called By:  Nill.
        /// Description: Updated the values from table.
        /// Changes history: Nill.
        /// </summary>
        /// <param name="SqlTran,SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        private bool update(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
               
                new SqlParameter("@Indent_No", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Indent_No", DataRowVersion.Current, this.Indent_No),
                new SqlParameter("@Ind_date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Ind_date", DataRowVersion.Current, this.Ind_date),
                new SqlParameter("@Prep_By", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "Prep_By", DataRowVersion.Current, this.Prep_By),
                new SqlParameter("@Stock_check_By", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Stock_check_By", DataRowVersion.Current, this.Stock_check_By ?? (object)DBNull.Value),
                new SqlParameter("@Qty_Spec", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Qty_Spec", DataRowVersion.Current, this.Qty_Spec ?? (object)DBNull.Value),
                new SqlParameter("@HO_approver", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "HO_approver", DataRowVersion.Current, this.HO_approver ?? (object)DBNull.Value),
                new SqlParameter("@Process_From", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Process_From", DataRowVersion.Current, this.Process_From),
                new SqlParameter("@Process_By", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Process_By", DataRowVersion.Current, this.Process_By ?? (object)DBNull.Value),
                new SqlParameter("@Location", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "Location", DataRowVersion.Current, this.Location),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@Budget_ID", SqlDbType.VarChar, 12, ParameterDirection.Input, false, 0, 0, "Budget_ID", DataRowVersion.Current, this.Budget_ID),
                new SqlParameter("@Status", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Remark", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Remark", DataRowVersion.Current, this.Remark ?? (object)DBNull.Value),
                new SqlParameter("@NOTE",this.NOTE)

				
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


        public IndentBL()
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

        public string AssetCode { get; set; }

        public bool? Recurring { get; set; }

        public string Task { get; set; }

        public string date { get; set; }
    }
}
