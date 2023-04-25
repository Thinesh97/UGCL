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
    public class BudgetBL
    {

        #region "Class: BudgetBL Local Declarations"

        public enum eLoadSp
        {
            SELECT_ALL = 0,
            INSERT = 1,
            UPDATE = 2,
            SELECT_BY_ID = 3,
            SELECT_MONTH = 4,
            INSERT_ITEM = 5,
            SELECT_BUDGETITEM_BY_ABS_BUDID = 6,
            BUDGET_AWAITING_APPROVAL = 7,
            SELECT_YEAR = 8,
            BUDGET_SEARCH = 9,
            BUDGET_Variance_SEARCH = 10,
            PRINT_BUDGET_BY_ID = 11,
            DELETE_BUDGETITEM_BY_ID = 12,
            SELECT_BUDGET_ID_BY_PROJECT_NAME = 13,
            SELECT_GRID_BY_SEARCH_Click = 14,
            SELECT_BudgetId_By_ID = 15,
            BudgetItemImport = 16,
            INSERT_FROM_POPUP = 17,
            BUDGET_Variance_BreakUp = 18,
            GET_BUDGET_VALIDATION = 19,
            Budget_Item_Project_Cost = 20,
            PoAmountApproval=21,
            BUDGET_SEARCH_All=22,
            BUDGET_SEARCH_Project_Wise=23,

        };


        #endregion
        #region "Class: BudgetBL Sets / Gets"

        public int USERID
        { get; set; }

        public int CurrentAbs_BID
        {
            get;
            set;
        }

        public int Abs_BID
        {
            get;
            set;
        }
        public int SenBack_Abs_BID
        {
            get;
            set;
        }
        public string Task
        {
            get;
            set;
        }
        public string Budget_ID
        {
            get;
            set;
        }
        public string SenBack_Budget_ID
        {
            get;
            set;
        }

        public string Project_Code
        {
            get;
            set;
        }
        public int Month
        {
            get;
            set;

        }
        public int Year
        {
            get;
            set;
        }
        public int Primary_Person
        {
            get;
            set;
        }
        public string Report_Person
        {
            get;
            set;

        }
        public DateTime Creation_Date
        {
            get;
            set;
        }
        public DateTime Closing_date
        {
            get;
            set;

        }
        public string Status
        {
            get;
            set;

        }
        public string Description
        {
            get;
            set;
        }
        public decimal Total_Amount
        {
            get;
            set;
        }
        public string Approval_Status
        {
            get;
            set;

        }
        public string Rev_Status
        {
            get;
            set;

        }
        public string Approver_Comment
        {
            get;
            set;
        }
        public DateTime Approval_Date
        {
            get;
            set;
        }
        public decimal Auto_amount
        {
            get;
            set;
        }
        public decimal PlMach_Amount
        {
            get;
            set;
        }
        public decimal Shutter_Mat_Amount
        {
            get;
            set;

        }
        public decimal Consumable_Amount
        {
            get;
            set;
        }
        public decimal Elec_Amount
        {
            get;
            set;
        }
        public decimal HSD_Pet_Amount
        {
            get;
            set;
        }
        public decimal Oil_Lube_Amount
        {
            get;
            set;
        }
        public decimal Hardware_Amount
        {
            get;
            set;
        }
        public decimal Weld_Elec_Amount
        {
            get;
            set;
        }
        public decimal Oxygen_ace_Amount
        {
            get;
            set;
        }
        public decimal Safety_Item
        {
            get;
            set;
        }
        public decimal Staff_wel_Amount
        {
            get;
            set;
        }
        public decimal Mess_Expense_amount
        {
            get;
            set;
        }
        public decimal Print_Sta_Amount
        {
            get;
            set;
        }
        public decimal Repair_Maint_Amount
        {
            get;
            set;
        }
        public decimal BOQ_Amount
        {
            get;
            set;
        }
        public decimal Sanitary_Amount
        {
            get;
            set;
        }
        public decimal Blast_ma_Amount
        {
            get;
            set;
        }
        public decimal FnF_Amount
        {
            get;
            set;
        }
        public decimal Fix_Asset_Amount
        {
            get;
            set;
        }
        public decimal Infra_Amount
        {
            get;
            set;
        }
        public decimal Sand_Amount
        {
            get;
            set;
        }
        public decimal Jelly_Metal_Amount
        {
            get;
            set;
        }
        public decimal Red_Soil
        {
            get;
            set;
        }
        public decimal Cement
        {
            get;
            set;
        }
        public decimal Chem_Amount
        {
            get;
            set;
        }
        public decimal Brick_Amount
        {
            get;
            set;
        }
        public decimal Steel_Amount
        {
            get;
            set;
        }
        public decimal Oth_Const_Amount
        {
            get;
            set;
        }
        public decimal Other_Amount
        {
            get;
            set;
        }




        // Budget Item Properties

        public int Bud_Item_Id
        { get; set; }

        public int Abs_bud_ID
        {
            get;
            set;

        }
        public int CategoryID
        { get; set; }

        public string ItemCode
        {
            get;
            set;
        }

        public string Bud_type
        {
            get;
            set;
        }

        public string Asset_ID
        { get; set; }


        public string Part_No
        {
            get;
            set;
        }
        public int UOM
        {
            get;
            set;
        }
        public bool Recurr_mat
        { get; set; }

        public decimal Avl_Qty
        { get; set; }

        public decimal Req_Qty
        {
            get;
            set;
        }
        public decimal Rate
        { get; set; }

        public decimal Purc_Value
        { get; set; }
        public decimal HO
        { get; set; }
        public decimal Local
        { get; set; }

      

        //


        #endregion

        #region "Class: Category Methods"

        /// <summary>
        /// Function Name:  fillCollectionFromDr.
        /// Called By:      Nill.
        /// Description:    Fill the data from data reader. 
        /// Change history: Nill.
        /// </summary>

        private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, BudgetBL> dicCountry)
        {
            if (dicCountry == null)
            {
                dicCountry = new Dictionary<int, BudgetBL>();
            }

            try
            {
                // Loop though the data reader
                while (dr.Read())
                {

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
                case eLoadSp.SELECT_ALL:
                    return "PRO_TB_Abstract_SELECT_ALL";
                case eLoadSp.SELECT_BudgetId_By_ID:
                    return "PRO_TB_Budget_Item_SELECT_By_Bud_Item_Id";
                case eLoadSp.INSERT:
                    return "PRO_TB_Abstract_Budget_Insert";
                case eLoadSp.UPDATE:
                    return "PRO_TB_Abstract_Budget_Update";
                case eLoadSp.SELECT_BY_ID:
                    return "PRO_TB_Abstract_Budget_Select_By_Abs_BID";
                case eLoadSp.SELECT_MONTH:
                    return "PRO_Tb_Month_Select";
                case eLoadSp.INSERT_ITEM:
                    return "PRO_TB_Budget_Item_INSERT";
                case eLoadSp.SELECT_BUDGETITEM_BY_ABS_BUDID:
                    return "PRO_TB_Budget_Item_Select_By_Abs_bud_ID";
                case eLoadSp.Budget_Item_Project_Cost:
                    return "PRO_TB_Budget_Item_Project_Cost";
                case eLoadSp.BUDGET_AWAITING_APPROVAL:
                    return "PRO_BUDGET_AWAITING_APPROVAL";
                case eLoadSp.SELECT_YEAR:
                    return "PRO_Tb_Year_Select";
                case eLoadSp.BUDGET_SEARCH:
                    return "PRO_Budget_Report_Budgeted_To_Approved";
                case eLoadSp.BUDGET_SEARCH_All:
                    return "PRO_Budget_Report_Budgeted_All";
                case eLoadSp.BUDGET_Variance_SEARCH:
                    return "PRO_Budget_Report_Approved_To_Actual";
                case eLoadSp.PRINT_BUDGET_BY_ID:
                    return "PRO_Budget_Print_By_Abs_bud_ID ";
                case eLoadSp.DELETE_BUDGETITEM_BY_ID:
                    return "PROC_DELETE_BUDGET_ITEM_BY_ID ";
                case eLoadSp.SELECT_BUDGET_ID_BY_PROJECT_NAME:
                    return "PROC_BUDGET_SELECTION_FROM_PROJECT_NAME";
                case eLoadSp.SELECT_GRID_BY_SEARCH_Click:
                    return "PRO_TB_Abstract_SELECT_ALL_For_ProjectName";
                case eLoadSp.BudgetItemImport:
                    return "PROC_IMPORT_FROM_BUDGET_ITEM";
                case eLoadSp.INSERT_FROM_POPUP:
                    return "PRO_TB_Budget_Item_INSERT_FROM_POPUP";
                case eLoadSp.BUDGET_Variance_BreakUp:
                    return "PROC_Budget_Variance_Breakup";
                case eLoadSp.GET_BUDGET_VALIDATION:
                    return "PRO_SELECT_BUDEGT_VALIDATON";
                case eLoadSp.PoAmountApproval:
                    return "PRO_SELECT_PO_AmountApproval";
                case eLoadSp.BUDGET_SEARCH_Project_Wise:
                    return "PRO_Budget_Report_Budgeted_ProjectWise";
                    
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
                case eLoadSp.BUDGET_Variance_BreakUp:
                    colParams = new SqlParameter[]
				{
					new SqlParameter("@Project_Code", this.Project_Code),
                    new SqlParameter("@Month", this.Month ),
                    new SqlParameter("@Year", this.Year ),
                    new SqlParameter("@Bud_type", this.Bud_type ),
                  
				};
                    break;
                case eLoadSp.SELECT_BY_ID:
                    colParams = new SqlParameter[]
				{
					new SqlParameter("@Abs_BID", this.Abs_BID)
				};
                    break;
                case eLoadSp.SELECT_BUDGETITEM_BY_ABS_BUDID:
                    colParams = new SqlParameter[]
				{
					new SqlParameter("@Abs_bud_ID", this.Abs_bud_ID)
				};
                    break;
                case eLoadSp.Budget_Item_Project_Cost:
                    colParams = new SqlParameter[]
				{
					new SqlParameter("@Budget_ID", this.Budget_ID),
					new SqlParameter("@Project_Code", this.Project_Code),
				};
                    break;
                case eLoadSp.PoAmountApproval:
                    colParams = new SqlParameter[]
				{
					new SqlParameter("@Budget_ID", this.Budget_ID),
					new SqlParameter("@Project_Code", this.Project_Code),
                    new SqlParameter("@SECTOR_NAME", this.SECTOR_NAME),
				};
                    break;
                case eLoadSp.BUDGET_SEARCH:
                case eLoadSp.BUDGET_SEARCH_All:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code),
                  new SqlParameter("@Month", this.Month ),
                    new SqlParameter("@Year", this.Year )
                };
                    break;
                case eLoadSp.BUDGET_SEARCH_Project_Wise:
                
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code),
                  new SqlParameter("@Month", this.Month ),
                    new SqlParameter("@Year", this.Year )
                };
                    break;

                    

                case eLoadSp.BUDGET_Variance_SEARCH:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code),
                  new SqlParameter("@Month", this.Month ),
                    new SqlParameter("@Year", this.Year )
                };
                    break;
                case eLoadSp.PRINT_BUDGET_BY_ID:
                    colParams = new SqlParameter[]
				{
					new SqlParameter("@Abs_BID", this.Abs_BID)
				};
                    break;
                case eLoadSp.DELETE_BUDGETITEM_BY_ID:
                    colParams = new SqlParameter[]
                 {
                      new SqlParameter("@BudgetItemID", this.Bud_Item_Id)

                 };
                    break;

                case eLoadSp.SELECT_ALL:
                    colParams = new SqlParameter[]
                 {
                      //new SqlParameter("@UserID", this.USERID)
                       new SqlParameter("@Project_Code", this.Project_Code)

                 };
                    break;
                case eLoadSp.BUDGET_AWAITING_APPROVAL:
                    colParams = new SqlParameter[]
                 {
                      //new SqlParameter("@UserID", this.USERID)
                       new SqlParameter("@Project_Code", this.Project_Code)

                 };
                    break;
                case eLoadSp.SELECT_BUDGET_ID_BY_PROJECT_NAME:
                    colParams = new SqlParameter[]
                 {
                      new SqlParameter("@ProjectCode", this.Project_Code)

                 };
                    break;
                case eLoadSp.SELECT_GRID_BY_SEARCH_Click:
                    colParams = new SqlParameter[]
                 {
                    //  new SqlParameter("@UserID", this.USERID),
                    new SqlParameter("@Abs_BID",this.Abs_BID),
                      new SqlParameter("@ProjectCode", this.Project_Code)

                 };
                    break;

              
                case eLoadSp.SELECT_BudgetId_By_ID:
                    colParams = new SqlParameter[]
                 {
                    
                      new SqlParameter("@buditemid", this.Bud_Item_Id)

                 };

                    break;

                case eLoadSp.GET_BUDGET_VALIDATION:
                    colParams = new SqlParameter[]
                 {
                    
                      new SqlParameter("@Abs_BID",this.ABS_BID),
                      new SqlParameter("@Budget_ID", this.Budget_ID),
                       new SqlParameter("@SECTOR_NAME",this.SECTOR_NAME),
                      new SqlParameter("@SECTORVALUE", this.SECTORVALUE)

                 };

                    break;
              

            }

            return colParams;
        }
        /// <summary>
        /// Fuction Name:   Insert.
        /// Called By:      Nill.
        /// Description:    Check the Sql conncetion.
        /// Change histroy: Nill.
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
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
				new SqlParameter("@Abs_BID",SqlDbType.Int,4,ParameterDirection.Output,false,0,0,"Abs_BID",DataRowVersion.Current,this.Abs_BID),
                new SqlParameter("@Budget_ID",SqlDbType.VarChar,100,ParameterDirection.Output,false,0,0,"Budget_ID",DataRowVersion.Current,this.Budget_ID),
                new SqlParameter("@SenBack_Budget_ID",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"",DataRowVersion.Current,this.SenBack_Budget_ID),
                new SqlParameter("@SenBack_Abs_BID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"",DataRowVersion.Current,this.SenBack_Abs_BID),
                new SqlParameter("@Project_Code",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Project_Code",DataRowVersion.Current,this.Project_Code),
                new SqlParameter("@Month",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Month",DataRowVersion.Current,this.Month),
                new SqlParameter("@Year",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Year",DataRowVersion.Current,this.Year),
                new SqlParameter("@Primary_Person",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Primary_Person",DataRowVersion.Current,this.Primary_Person),
                new SqlParameter("@Report_Person",SqlDbType.VarChar,500,ParameterDirection.Input,false,0,0,"Report_Person",DataRowVersion.Current,this.Report_Person),
                new SqlParameter("@Creation_Date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Creation_Date",DataRowVersion.Current,this.Creation_Date),
                new SqlParameter("@Closing_date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Closing_date",DataRowVersion.Current,this.Closing_date),
                new SqlParameter("@Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Status",DataRowVersion.Current,this.Status),
                new SqlParameter("@Description",SqlDbType.VarChar,500,ParameterDirection.Input,false,0,0,"Description",DataRowVersion.Current,this.Description),
                new SqlParameter("@Total_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Total_Amount",DataRowVersion.Current,this.Total_Amount),
                new SqlParameter("@Approval_Status",SqlDbType.VarChar,25,ParameterDirection.Input,false,0,0,"Approval_Status",DataRowVersion.Current,this.Approval_Status),
                new SqlParameter("@Rev_Status",SqlDbType.VarChar,5,ParameterDirection.Input,false,0,0,"Rev_Status",DataRowVersion.Current,this.Rev_Status),
                new SqlParameter("@Approver_Comment",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Approver_Comment",DataRowVersion.Current,this.Approver_Comment),
                new SqlParameter("@Approval_Date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Approval_Date",DataRowVersion.Current,this.Approval_Date),
                new SqlParameter("@Auto_amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Auto_amount",DataRowVersion.Current,this.Auto_amount),
                new SqlParameter("@PlMach_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"PlMach_Amount",DataRowVersion.Current,this.PlMach_Amount),
                new SqlParameter("@Shutter_Mat_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Shutter_Mat_Amount",DataRowVersion.Current,this.Shutter_Mat_Amount),
                new SqlParameter("@Consumable_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Consumable_Amount",DataRowVersion.Current,this.Consumable_Amount),
                new SqlParameter("@Elec_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Elec_Amount",DataRowVersion.Current,this.Elec_Amount),
                new SqlParameter("@HSD_Pet_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"HSD_Pet_Amount",DataRowVersion.Current,this.HSD_Pet_Amount),
                new SqlParameter("@Oil_Lube_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Oil_Lube_Amount",DataRowVersion.Current,this.Oil_Lube_Amount),
                new SqlParameter("@Hardware_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Hardware_Amount",DataRowVersion.Current,this.Hardware_Amount),
                new SqlParameter("@Weld_Elec_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Weld_Elec_Amount",DataRowVersion.Current,this.Weld_Elec_Amount),
                new SqlParameter("@Oxygen_ace_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Oxygen_ace_Amount",DataRowVersion.Current,this.Oxygen_ace_Amount),
                new SqlParameter("@Safety_Item",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Safety_Item",DataRowVersion.Current,this.Safety_Item),
                new SqlParameter("@Staff_wel_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Staff_wel_Amount",DataRowVersion.Current,this.Staff_wel_Amount),
                new SqlParameter("@Mess_Expense_amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Mess_Expense_amount",DataRowVersion.Current,this.Mess_Expense_amount),
                new SqlParameter("@Print_Sta_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Print_Sta_Amount",DataRowVersion.Current,this.Print_Sta_Amount),
                new SqlParameter("@Repair_Maint_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Repair_Maint_Amount",DataRowVersion.Current,this.Repair_Maint_Amount),
                new SqlParameter("@BOQ_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"BOQ_Amount",DataRowVersion.Current,this.BOQ_Amount),
                new SqlParameter("@Sanitary_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Sanitary_Amount",DataRowVersion.Current,this.Sanitary_Amount),
                new SqlParameter("@Blast_ma_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Blast_ma_Amount",DataRowVersion.Current,this.Blast_ma_Amount),
                new SqlParameter("@FnF_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"FnF_Amount",DataRowVersion.Current,this.FnF_Amount),
                new SqlParameter("@Fix_Asset_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Fix_Asset_Amount",DataRowVersion.Current,this.Fix_Asset_Amount),
                new SqlParameter("@Infra_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Infra_Amount",DataRowVersion.Current,this.Infra_Amount),
                new SqlParameter("@Sand_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Sand_Amount",DataRowVersion.Current,this.Sand_Amount),
                new SqlParameter("@Red_Soil",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Red_Soil",DataRowVersion.Current,this.Red_Soil),
                 new SqlParameter("@Jelly_Metal_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Jelly_Metal_Amount",DataRowVersion.Current,this.Jelly_Metal_Amount),
                new SqlParameter("@Cement",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Cement",DataRowVersion.Current,this.Cement),
                new SqlParameter("@Chem_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Chem_Amount",DataRowVersion.Current,this.Chem_Amount),
                new SqlParameter("@Brick_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Brick_Amount",DataRowVersion.Current,this.Brick_Amount),
                new SqlParameter("@Steel_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Steel_Amount",DataRowVersion.Current,this.Steel_Amount),
                new SqlParameter("@Oth_Const_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Oth_Const_Amount",DataRowVersion.Current,this.Oth_Const_Amount),
                new SqlParameter("@Other_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Other_Amount",DataRowVersion.Current,this.Other_Amount)
			};

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));

                    this.Abs_BID = (int)colParams.First().Value;                    
                    this.Budget_ID = (string)colParams[1].Value;
                   
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
        public bool ItemImport(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ItemImport(SqlConn, null, enumSpName);
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
        public bool ItemImport(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return ItemImport(null, SqlTran, enumSpName);
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
        /// 

        private bool ItemImport(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;


                SqlParameter[] colParams = new SqlParameter[]
			{	
                      new SqlParameter("@ProjectCode", this.Project_Code),
                      new SqlParameter("@Abs_bud_ID", this.Abs_BID),
                      new SqlParameter("@CurrentProjectAbstractID", this.CurrentAbs_BID),
                      new SqlParameter("@BudgetType", this.Bud_type)

                 
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

        //ITEMPOPUP
        public bool ItemInsertPOPUP(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ItemInsertPOPUP(SqlConn, null, enumSpName);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool ItemInsertPOPUP(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return ItemInsertPOPUP(null, SqlTran, enumSpName);
        }


        /// <summary>
        /// //
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        private bool ItemInsertPOPUP(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;


                SqlParameter[] colParams = new SqlParameter[]
			{		
              
                new SqlParameter("@Abs_bud_ID",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Abs_bud_ID",DataRowVersion.Current,this.Abs_bud_ID),               
                new SqlParameter("@Bud_Sector",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Bud_type",DataRowVersion.Current,this.Bud_type),
                new SqlParameter("@Item_Code",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Item_Code",DataRowVersion.Current,this.ItemCode),
                 new SqlParameter("@Task",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Task",DataRowVersion.Current,this.Task)
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





























        public bool ItemInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ItemInsert(SqlConn, null, enumSpName);
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
        public bool ItemInsert(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return ItemInsert(null, SqlTran, enumSpName);
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
        /// 
        
              
        private bool ItemInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;


                SqlParameter[] colParams = new SqlParameter[]
			{		
              
                new SqlParameter("@Abs_bud_ID",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Abs_bud_ID",DataRowVersion.Current,this.Abs_BID),               
                new SqlParameter("@Bud_type",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Bud_type",DataRowVersion.Current,this.Bud_type),
                new SqlParameter("@Asset_ID",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Asset_ID",DataRowVersion.Current,this.Asset_ID),
                new SqlParameter("@Description",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Description",DataRowVersion.Current,this.Description),

                new SqlParameter("@Mat_cat_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Mat_cat_ID",DataRowVersion.Current,this.CategoryID ),
                new SqlParameter("@Item_Code",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Item_Code",DataRowVersion.Current,this.ItemCode),


                new SqlParameter("@Part_No",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Part_No",DataRowVersion.Current,this.Part_No),
                new SqlParameter("@Recurr_mat",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Recurr_mat",DataRowVersion.Current,this.Recurr_mat),
                new SqlParameter("@UOM",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"UOM",DataRowVersion.Current,this.UOM),               
                new SqlParameter("@Req_Qty",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Req_Qty",DataRowVersion.Current,this.Req_Qty),
                new SqlParameter("@Rate",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Rate",DataRowVersion.Current,this.Rate),
                new SqlParameter("@Purc_Value",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Purc_Value",DataRowVersion.Current,this.Purc_Value),
                new SqlParameter("@HO_Value",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"HO_Value",DataRowVersion.Current,this.HO),
                new SqlParameter("@Local_Value",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Local_Value",DataRowVersion.Current,this.Local),
                 new SqlParameter("@Bud_Item_Id",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Bud_Item_Id",DataRowVersion.Current,this.Bud_Item_Id),
                 new SqlParameter("@Task",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Task",DataRowVersion.Current,this.Task)
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
        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, BudgetBL> diclCountry)
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
                new SqlParameter("@Abs_BID", this.Abs_BID),  
                new SqlParameter("@Task", this.Task),  
                new SqlParameter("@Creation_Date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Creation_Date",DataRowVersion.Current,this.Creation_Date),
                new SqlParameter("@Closing_date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Closing_date",DataRowVersion.Current,this.Closing_date),               
                new SqlParameter("@Description",SqlDbType.VarChar,500,ParameterDirection.Input,false,0,0,"Description",DataRowVersion.Current,this.Description),
                new SqlParameter("@Primary_Person",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Primary_Person",DataRowVersion.Current,this.Primary_Person),               
                new SqlParameter("@Report_Person",SqlDbType.VarChar,500,ParameterDirection.Input,false,0,0,"Report_Person",DataRowVersion.Current,this.Report_Person),
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


        public BudgetBL()
        {
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

        public int ABS_BID { get; set; }

        public string SECTOR_NAME { get; set; }

        public decimal SECTORVALUE { get; set; }
    }
}
