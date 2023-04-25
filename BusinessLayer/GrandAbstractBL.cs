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
    public class GrandAbstractBL
    {
        #region "Class: GrandAbstractBL Local Declarations"

        public enum eLoadSp
        {
            SELECT_ALL = 0,
            INSERT = 1,
            SELECT_REMARKS =  2


        };


        #endregion
        #region "Class: BudgetBL Sets / Gets"


        public int GRAND_ABS_ID
        {
            get;
            set;
        }

        public int ABS_BID
        {
            get;
            set;
        }
        
        public string Budget_ID
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
        public string Auto_Status
        {
            get;
            set;
        }
        public string Auto_Remarks
        {
            get;
            set;
        }
        //public string Auto_Remarks
        // {
        //     get;
        //     set;
        // }
        public string PlMach_Status
        {
            get;
            set;
        }
        public string PlMach_Remarks
        {
            get;
            set;
        }
        public string Shutter_Mat_Status
        {
            get;
            set;
        }
        public string Shutter_Mat_Remarks
        {
            get;
            set;
        }
        public string Consumable_Status
        {
            get;
            set;
        }
        public string Consumable_Remarks
        {
            get;
            set;
        }
        public string Elec_Status
        {
            get;
            set;
        }
        public string Elec_Remarks
        {
            get;
            set;
        }
        public string HSD_Pet_Status
        {
            get;
            set;
        }
        public string HSD_Pet_Remarks
        {
            get;
            set;
        }
        public string Oil_Lube_Status
        {
            get;
            set;
        }
        public string Oil_Lube_Remarks
        {
            get;
            set;
        }
        public string Hardware_Status
        {
            get;
            set;
        }
        public string Hardware_Remarks
        {
            get;
            set;
        }
        public string Weld_Elec_Status
        {
            get;
            set;
        }
        public string Weld_Elec_Remarks
        {
            get;
            set;
        }
        public string Oxygen_ace_Status
        {
            get;
            set;
        }
        public string Oxygen_ace_Remarks
        {
            get;
            set;
        }
        public string Safety_Item_Status
        {
            get;
            set;
        }
        public string Safety_Item_Remarks
        {
            get;
            set;
        }
        public string Staff_wel_Status
        {

            get;
            set;

        }
        public string Staff_wel_Remarks
        {
            get;
            set;
        }

        public string Mess_Expense_Status
        {
            get;
            set;
        }
        public string Mess_Expense_Remarks
        {
            get;
            set;
        }
        public string Print_Sta_Status
        {
            get;
            set;
        }
        public string Print_Sta_Remarks
        {
            get;
            set;
        }
        public string Repair_Maint_Status
        {
            get;
            set;
        }
        public string Repair_Maint_Remarks
        {
            get;
            set;
        }

        public string BOQ_Status
        {
            get;
            set;
        }


        public string BOQ_Remarks
        {
            get;
            set;
        }


        public string Sanitary_Status
        {
            get;
            set;
        }
        public string Sanitary_Remarks
        {
            get;
            set;
        }
        public string Blast_ma_Status
        {
            get;
            set;
        }
        public string Blast_ma_Remarks
        {
            get;
            set;
        }

        public string FnF_Status
        {
            get;
            set;
        }
        public string FnF_Remarks
        {
            get;
            set;
        }

        public string Fix_Asset_Status
        {
            get;
            set;
        }
        public string Fix_Asset_Remarks
        {
            get;
            set;
        }

        public string Infra_Status
        {
            get;
            set;
        }

        public string Infra_Remarks
        {
            get;
            set;
        }
        public string Sand_Status
        {
            get;
            set;
        }
        public string Sand_Remarks
        {
            get;
            set;
        }
        public string Jelly_Metal_Status
        {
            get;
            set;
        }
        public string Jelly_MetalRemarks
        {
            get;
            set;
        }
        public string Red_Soil_Status
        {
            get;
            set;
        }
        public string Red_Soil_Remarks
        {
            get;
            set;
        }
        public string Cement_Status
        {
            get;
            set;
        }
        public string Cement_Remarks
        {
            get;
            set;
        }
        public string Chem_Status
        {
            get;
            set;
        }
        public string Chem_Remarks
        {
            get;
            set;
        }
        public string Brick_Status
        {
            get;
            set;
        }
        public string Brick_Remarks
        {
            get;
            set;
        }
        public string Steel_Status
        {
            get;
            set;
        }
        public string Steel_Remarks
        {
            get;
            set;
        }
        public string Oth_Const_Status
        {
            get;
            set;
        }
        public string Oth_Const_Remarks
        {
            get;
            set;
        }
        public string Other_Status
        {
            get;
            set;
        }
        public string Other_Remarks
        {
            get;
            set;
        }
        public decimal Auto_amount_Budget
        { get;             
          set; 
        }

        public decimal PlMach_Amount_Budget { get; set; }

        public decimal Shutter_Mat_Amount_Budgeted { get; set; }

        public decimal Consumable_Amount_Budgeted { get; set; }

        public decimal Elec_Amount_Budgeted { get; set; }

        public decimal HSD_Pet_Amount_Budgeted { get; set; }

        public decimal Oil_Lube_Amount_Budgeted { get; set; }

        public decimal Hardware_Amount_Budgeted { get; set; }

        public decimal Weld_Elec_Amount_Budgeted { get; set; }

        public decimal Oxygen_ace_Amount_Budgeted { get; set; }

        public decimal Safety_Item_Amount_Budgeted { get; set; }

        public decimal Staff_wel_Amount_Budgeted { get; set; }

        public decimal Mess_Expense_amount_Budgeted { get; set; }

        public decimal Print_Sta_Amount_Budgeted { get; set; }

        public decimal Repair_Maint_Amount_Budgeted { get; set; }

        public decimal BOQ_Amount_Budgeted { get; set; }

        public decimal Sanitary_Amount_Budgeted { get; set; }

        public decimal Blast_ma_Amount_Budgeted { get; set; }

        public decimal FnF_Status_Budgeted { get; set; }

        public decimal Fix_Asset_Amount_Budgeted { get; set; }

        public decimal Infra_Amount_Budgeted { get; set; }

        public decimal Sand_Amount_Budgeted { get; set; }

        public decimal Jelly_Metal_Amount_Budgeted { get; set; }

        public decimal Red_Soil_amount_Budgeted { get; set; }

        public decimal Cement_amount_Budgeted { get; set; }

        public decimal Chem_Amount_Budgeted { get; set; }

        public decimal Brick_Amount_Budgeted { get; set; }

        public decimal Steel_Amount_Budgeted { get; set; }

        public decimal Oth_Const_Amount_Budgeted { get; set; }

        public decimal Other_Amount_Budgeted { get; set; }


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
                    //clsCountry.CountryName
                    //if (dr["Category_Name"] != DBNull.Value)
                    //{
                    //    this.Category_Name = dr["Category_Name"].ToString();
                    //}


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
                    //if (dr["Category_Name"] != DBNull.Value)
                    //{
                    //    this.Category_Name = dr["Category_Name"].ToString();
                    //}



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
                case eLoadSp.INSERT:
                    return "INSERT_GRAND_ABSTRACT";
                case eLoadSp.SELECT_REMARKS:
                    return "PROC_GET_Remarks_ForBudget_By_BudgetID";
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
                case eLoadSp.SELECT_REMARKS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Budget_ID", this.Budget_ID)
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
				
                new SqlParameter("@ABS_BID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"ABS_BID",DataRowVersion.Current,this.ABS_BID),               
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
                new SqlParameter("@Jelly_Metal_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Jelly_Metal_Amount",DataRowVersion.Current,this.Jelly_Metal_Amount),
                new SqlParameter("@Red_Soil",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Red_Soil",DataRowVersion.Current,this.Red_Soil),
                new SqlParameter("@Cement",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Cement",DataRowVersion.Current,this.Cement),
                new SqlParameter("@Chem_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Chem_Amount",DataRowVersion.Current,this.Chem_Amount),
                new SqlParameter("@Brick_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Brick_Amount",DataRowVersion.Current,this.Brick_Amount),                
                new SqlParameter("@Steel_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Steel_Amount",DataRowVersion.Current,this.Steel_Amount),
                new SqlParameter("@Oth_Const_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Oth_Const_Amount",DataRowVersion.Current,this.Oth_Const_Amount),
                new SqlParameter("@Other_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Other_Amount",DataRowVersion.Current,this.Other_Amount),
                new SqlParameter("@Auto_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Auto_Status",DataRowVersion.Current,this.Auto_Status),
                new SqlParameter("@Auto_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Auto_Remarks",DataRowVersion.Current,this.Auto_Remarks),
                new SqlParameter("@PlMach_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"PlMach_Status",DataRowVersion.Current,this.PlMach_Status),
                new SqlParameter("@PlMach_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"PlMach_Remarks",DataRowVersion.Current,this.PlMach_Remarks),
                new SqlParameter("@Shutter_Mat_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Shutter_Mat_Status",DataRowVersion.Current,this.Shutter_Mat_Status),
                new SqlParameter("@Shutter_Mat_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Shutter_Mat_Remarks",DataRowVersion.Current,this.Shutter_Mat_Remarks),
                new SqlParameter("@Consumable_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Consumable_Status",DataRowVersion.Current,this.Consumable_Status),
                new SqlParameter("@Consumable_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Consumable_Remarks",DataRowVersion.Current,this.Consumable_Remarks),
                new SqlParameter("@Elec_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Elec_Status",DataRowVersion.Current,this.Elec_Status),
                new SqlParameter("@Elec_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Elec_Remarks",DataRowVersion.Current,this.Elec_Remarks),
                new SqlParameter("@HSD_Pet_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"HSD_Pet_Status",DataRowVersion.Current,this.HSD_Pet_Status),
                new SqlParameter("@HSD_Pet_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"HSD_Pet_Remarks",DataRowVersion.Current,this.HSD_Pet_Remarks),
                new SqlParameter("@Oil_Lube_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Oil_Lube_Status",DataRowVersion.Current,this.Oil_Lube_Status),
                new SqlParameter("@Oil_Lube_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Oil_Lube_Remarks",DataRowVersion.Current,this.Oil_Lube_Remarks),
                new SqlParameter("@Hardware_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Hardware_Status",DataRowVersion.Current,this.Hardware_Status),
                new SqlParameter("@Hardware_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Hardware_Remarks",DataRowVersion.Current,this.Hardware_Remarks),
                new SqlParameter("@Weld_Elec_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Weld_Elec_Status",DataRowVersion.Current,this.Weld_Elec_Status),
                new SqlParameter("@Weld_Elec_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Weld_Elec_Remarks",DataRowVersion.Current,this.Weld_Elec_Remarks),
                new SqlParameter("@Oxygen_ace_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Oxygen_ace_Status",DataRowVersion.Current,this.Oxygen_ace_Status),
                new SqlParameter("@Oxygen_ace_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Oxygen_ace_Remarks",DataRowVersion.Current,this.Oxygen_ace_Remarks),
                new SqlParameter("@Safety_Item_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Safety_Item_Status",DataRowVersion.Current,this.Safety_Item_Status),
                new SqlParameter("@Safety_Item_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Safety_Item_Remarks",DataRowVersion.Current,this.Safety_Item_Remarks),
                new SqlParameter("@Staff_wel_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Staff_wel_Status",DataRowVersion.Current,this.Staff_wel_Status),
                new SqlParameter("@Staff_wel_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Staff_wel_Remarks",DataRowVersion.Current,this.Staff_wel_Remarks),
                new SqlParameter("@Mess_Expense_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Mess_Expense_Status",DataRowVersion.Current,this.Mess_Expense_Status),
                new SqlParameter("@Mess_Expense_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Mess_Expense_Remarks",DataRowVersion.Current,this.Mess_Expense_Remarks),
                new SqlParameter("@Print_Sta_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Print_Sta_Status",DataRowVersion.Current,this.Print_Sta_Status),
                new SqlParameter("@Print_Sta_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Print_Sta_Remarks",DataRowVersion.Current,this.Print_Sta_Remarks),
                new SqlParameter("@Repair_Maint_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Repair_Maint_Status",DataRowVersion.Current,this.Repair_Maint_Status),
                new SqlParameter("@Repair_Maint_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Repair_Maint_Remarks",DataRowVersion.Current,this.Repair_Maint_Remarks),
                new SqlParameter("@BOQ_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"BOQ_Status",DataRowVersion.Current,this.BOQ_Status),
                new SqlParameter("@BOQ_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"BOQ_Remarks",DataRowVersion.Current,this.BOQ_Remarks),
                new SqlParameter("@Sanitary_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Sanitary_Status",DataRowVersion.Current,this.Sanitary_Status),
                new SqlParameter("@Sanitary_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Sanitary_Remarks",DataRowVersion.Current,this.Sanitary_Remarks),
                new SqlParameter("@Blast_ma_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Blast_ma_Status",DataRowVersion.Current,this.Blast_ma_Status),
                new SqlParameter("@Blast_ma_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Blast_ma_Remarks",DataRowVersion.Current,this.Blast_ma_Remarks),
                new SqlParameter("@FnF_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"FnF_Status",DataRowVersion.Current,this.FnF_Status),
                new SqlParameter("@FnF_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"FnF_Remarks",DataRowVersion.Current,this.FnF_Remarks),
                new SqlParameter("@Fix_Asset_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Fix_Asset_Status",DataRowVersion.Current,this.Fix_Asset_Status),
                new SqlParameter("@Fix_Asset_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Fix_Asset_Remarks",DataRowVersion.Current,this.Fix_Asset_Remarks),
                new SqlParameter("@Infra_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Infra_Status",DataRowVersion.Current,this.Infra_Status),
                new SqlParameter("@Infra_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Infra_Remarks",DataRowVersion.Current,this.Infra_Remarks),
                new SqlParameter("@Sand_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Sand_Status",DataRowVersion.Current,this.Sand_Status),
                new SqlParameter("@Sand_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Sand_Remarks",DataRowVersion.Current,this.Sand_Remarks),
                new SqlParameter("@Jelly_Metal_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Jelly_Metal_Status",DataRowVersion.Current,this.Jelly_Metal_Status),
                new SqlParameter("@Jelly_MetalRemarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Jelly_MetalRemarks",DataRowVersion.Current,this.Jelly_MetalRemarks),
                new SqlParameter("@Red_Soil_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Red_Soil_Status",DataRowVersion.Current,this.Red_Soil_Status),
                new SqlParameter("@Red_Soil_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Red_Soil_Remarks",DataRowVersion.Current,this.Red_Soil_Remarks),
                new SqlParameter("@Cement_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Cement_Status",DataRowVersion.Current,this.Cement_Status),
                new SqlParameter("@Cement_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Cement_Remarks",DataRowVersion.Current,this.Cement_Remarks),
                new SqlParameter("@Chem_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Chem_Status",DataRowVersion.Current,this.Chem_Status),
                new SqlParameter("@Chem_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Chem_Remarks",DataRowVersion.Current,this.Chem_Remarks),
                new SqlParameter("@Brick_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Brick_Status",DataRowVersion.Current,this.Brick_Status),
                new SqlParameter("@Brick_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Brick_Remarks",DataRowVersion.Current,this.Brick_Remarks),
                new SqlParameter("@Steel_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Steel_Status",DataRowVersion.Current,this.Steel_Status),
                new SqlParameter("@Steel_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Steel_Remarks",DataRowVersion.Current,this.Steel_Remarks),
                new SqlParameter("@Oth_Const_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Oth_Const_Status",DataRowVersion.Current,this.Oth_Const_Status),
                new SqlParameter("@Oth_Const_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Oth_Const_Remarks",DataRowVersion.Current,this.Oth_Const_Remarks),
                new SqlParameter("@Other_Status",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Other_Status",DataRowVersion.Current,this.Other_Status),
                new SqlParameter("@Other_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Other_Remarks",DataRowVersion.Current,this.Other_Remarks),
                new SqlParameter("@Budget_ID",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Budget_ID",DataRowVersion.Current,this.Budget_ID),


                 new SqlParameter("@Auto_amount_Budget",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Auto_amount_Budget",DataRowVersion.Current,this.Auto_amount_Budget),
                 new SqlParameter("@PlMach_Amount_Budget",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"PlMach_Amount_Budget",DataRowVersion.Current,this.PlMach_Amount_Budget),
                 new SqlParameter("@Shutter_Mat_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Shutter_Mat_Amount_Budgeted",DataRowVersion.Current,this.Shutter_Mat_Amount_Budgeted),
                 new SqlParameter("@Consumable_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Consumable_Amount_Budgeted",DataRowVersion.Current,this.Consumable_Amount_Budgeted),
                 new SqlParameter("@Elec_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Elec_Amount_Budgeted",DataRowVersion.Current,this.Elec_Amount_Budgeted),
                 new SqlParameter("@HSD_Pet_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"HSD_Pet_Amount_Budgeted",DataRowVersion.Current,this.HSD_Pet_Amount_Budgeted),
                 new SqlParameter("@Oil_Lube_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Oil_Lube_Amount_Budgeted",DataRowVersion.Current,this.Oil_Lube_Amount_Budgeted),
                 new SqlParameter("@Hardware_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Hardware_Amount_Budgeted",DataRowVersion.Current,this.Hardware_Amount_Budgeted),
                 new SqlParameter("@Weld_Elec_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Weld_Elec_Amount_Budgeted",DataRowVersion.Current,this.Weld_Elec_Amount_Budgeted),
                 new SqlParameter("@Oxygen_ace_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Oxygen_ace_Amount_Budgeted",DataRowVersion.Current,this.Oxygen_ace_Amount_Budgeted),
                 new SqlParameter("@Safety_Item_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Safety_Item_Amount_Budgeted",DataRowVersion.Current,this.Safety_Item_Amount_Budgeted),
                 new SqlParameter("@Staff_wel_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Staff_wel_Amount_Budgeted",DataRowVersion.Current,this.Staff_wel_Amount_Budgeted),
                 new SqlParameter("@Mess_Expense_amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Mess_Expense_amount_Budgeted",DataRowVersion.Current,this.Mess_Expense_amount_Budgeted),
                 new SqlParameter("@Print_Sta_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Print_Sta_Amount_Budgeted",DataRowVersion.Current,this.Print_Sta_Amount_Budgeted),
                 new SqlParameter("@Repair_Maint_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Repair_Maint_Amount_Budgeted",DataRowVersion.Current,this.Repair_Maint_Amount_Budgeted),
                 new SqlParameter("@BOQ_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"BOQ_Amount_Budgeted",DataRowVersion.Current,this.BOQ_Amount_Budgeted),
                 new SqlParameter("@Sanitary_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Sanitary_Amount_Budgeted",DataRowVersion.Current,this.Sanitary_Amount_Budgeted),
                 new SqlParameter("@Blast_ma_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Blast_ma_Amount_Budgeted",DataRowVersion.Current,this.Blast_ma_Amount_Budgeted),
                 new SqlParameter("@FnF_Status_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"FnF_Status_Budgeted",DataRowVersion.Current,this.FnF_Status_Budgeted),
                 new SqlParameter("@Fix_Asset_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Fix_Asset_Amount_Budgeted",DataRowVersion.Current,this.Fix_Asset_Amount_Budgeted),
                 new SqlParameter("@Infra_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Infra_Amount_Budgeted",DataRowVersion.Current,this.Infra_Amount_Budgeted),
                 new SqlParameter("@Sand_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Sand_Amount_Budgeted",DataRowVersion.Current,this.Sand_Amount_Budgeted),
                 new SqlParameter("@Jelly_Metal_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Jelly_Metal_Amount_Budgeted",DataRowVersion.Current,this.Jelly_Metal_Amount_Budgeted),
                 new SqlParameter("@Red_Soil_amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Red_Soil_amount_Budgeted",DataRowVersion.Current,this.Red_Soil_amount_Budgeted),
                 new SqlParameter("@Cement_amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Cement_amount_Budgeted",DataRowVersion.Current,this.Cement_amount_Budgeted),
                 new SqlParameter("@Chem_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Chem_Amount_Budgeted",DataRowVersion.Current,this.Chem_Amount_Budgeted),
                 new SqlParameter("@Brick_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Brick_Amount_Budgeted",DataRowVersion.Current,this.Brick_Amount_Budgeted),
                 new SqlParameter("@Steel_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Steel_Amount_Budgeted",DataRowVersion.Current,this.Steel_Amount_Budgeted),
                 new SqlParameter("@Oth_Const_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Oth_Const_Amount_Budgeted",DataRowVersion.Current,this.Oth_Const_Amount_Budgeted),
                 new SqlParameter("@Other_Amount_Budgeted",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Other_Amount_Budgeted",DataRowVersion.Current,this.Other_Amount_Budgeted),
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
                 new SqlParameter("@Budget_ID",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Budget_ID",DataRowVersion.Current,this.Budget_ID),
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
                new SqlParameter("@Jelly_Metal_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Jelly_Metal_Amount",DataRowVersion.Current,this.Jelly_Metal_Amount),
                new SqlParameter("@Red_Soil",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Red_Soil",DataRowVersion.Current,this.Red_Soil),
                new SqlParameter("@Cement",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Cement",DataRowVersion.Current,this.Cement),
                new SqlParameter("@Chem_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Chem_Amount",DataRowVersion.Current,this.Chem_Amount),
                new SqlParameter("@Brick_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Brick_Amount",DataRowVersion.Current,this.Brick_Amount),                
                new SqlParameter("@Steel_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Steel_Amount",DataRowVersion.Current,this.Steel_Amount),
                new SqlParameter("@Oth_Const_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Oth_Const_Amount",DataRowVersion.Current,this.Oth_Const_Amount),
                new SqlParameter("@Other_Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Other_Amount",DataRowVersion.Current,this.Other_Amount),


                new SqlParameter("@Auto_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Auto_Status",DataRowVersion.Current,this.Auto_Status),
                new SqlParameter("@Auto_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Auto_Remarks",DataRowVersion.Current,this.Auto_Remarks),

                new SqlParameter("@PlMach_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"PlMach_Status",DataRowVersion.Current,this.PlMach_Status),
                new SqlParameter("@PlMach_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"PlMach_Remarks",DataRowVersion.Current,this.PlMach_Remarks),

                new SqlParameter("@Shutter_Mat_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Shutter_Mat_Status",DataRowVersion.Current,this.Shutter_Mat_Status),
                    new SqlParameter("@Shutter_Mat_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Shutter_Mat_Remarks",DataRowVersion.Current,this.Shutter_Mat_Remarks),

                new SqlParameter("@Consumable_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Consumable_Status",DataRowVersion.Current,this.Consumable_Status),
                new SqlParameter("@Consumable_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Consumable_Remarks",DataRowVersion.Current,this.Consumable_Remarks),
                new SqlParameter("@Elec_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Elec_Status",DataRowVersion.Current,this.Elec_Status),
                new SqlParameter("@Elec_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Elec_Remarks",DataRowVersion.Current,this.Elec_Remarks),
                    new SqlParameter("@HSD_Pet_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"HSD_Pet_Status",DataRowVersion.Current,this.HSD_Pet_Status),
                new SqlParameter("@HSD_Pet_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"HSD_Pet_Remarks",DataRowVersion.Current,this.HSD_Pet_Remarks),
                new SqlParameter("@Oil_Lube_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Oil_Lube_Status",DataRowVersion.Current,this.Oil_Lube_Status),
                new SqlParameter("@Oil_Lube_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Oil_Lube_Remarks",DataRowVersion.Current,this.Oil_Lube_Remarks),
                new SqlParameter("@Hardware_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Hardware_Status",DataRowVersion.Current,this.Hardware_Status),
                new SqlParameter("@Hardware_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Hardware_Remarks",DataRowVersion.Current,this.Hardware_Remarks),

                new SqlParameter("@Weld_Elec_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Weld_Elec_Status",DataRowVersion.Current,this.Weld_Elec_Status),
                new SqlParameter("@Weld_Elec_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Weld_Elec_Remarks",DataRowVersion.Current,this.Weld_Elec_Remarks),
                new SqlParameter("@Oxygen_ace_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Oxygen_ace_Status",DataRowVersion.Current,this.Oxygen_ace_Status),
                new SqlParameter("@Oxygen_ace_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Oxygen_ace_Remarks",DataRowVersion.Current,this.Oxygen_ace_Remarks),
                new SqlParameter("@Safety_Item_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Safety_Item_Status",DataRowVersion.Current,this.Safety_Item_Status),
                new SqlParameter("@Safety_Item_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Safety_Item_Remarks",DataRowVersion.Current,this.Safety_Item_Remarks),
                new SqlParameter("@Staff_wel_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Staff_wel_Status",DataRowVersion.Current,this.Staff_wel_Status),
                new SqlParameter("@Staff_wel_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Staff_wel_Remarks",DataRowVersion.Current,this.Staff_wel_Remarks),
                new SqlParameter("@Mess_Expense_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Mess_Expense_Status",DataRowVersion.Current,this.Mess_Expense_Status),
                new SqlParameter("@Mess_Expense_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Mess_Expense_Remarks",DataRowVersion.Current,this.Mess_Expense_Remarks),

                new SqlParameter("@Print_Sta_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Print_Sta_Status",DataRowVersion.Current,this.Print_Sta_Status),
                new SqlParameter("@Print_Sta_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Print_Sta_Remarks",DataRowVersion.Current,this.Print_Sta_Remarks),
                new SqlParameter("@Repair_Maint_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Repair_Maint_Status",DataRowVersion.Current,this.Repair_Maint_Status),
                new SqlParameter("@Repair_Maint_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Repair_Maint_Remarks",DataRowVersion.Current,this.Repair_Maint_Remarks),
                new SqlParameter("@BOQ_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"BOQ_Status",DataRowVersion.Current,this.BOQ_Status),
                new SqlParameter("@BOQ_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"BOQ_Remarks",DataRowVersion.Current,this.BOQ_Remarks),
                new SqlParameter("@Sanitary_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Sanitary_Status",DataRowVersion.Current,this.Sanitary_Status),
                new SqlParameter("@Sanitary_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Sanitary_Remarks",DataRowVersion.Current,this.Sanitary_Remarks),
                new SqlParameter("@Blast_ma_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Blast_ma_Status",DataRowVersion.Current,this.Blast_ma_Status),
                new SqlParameter("@Blast_ma_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Blast_ma_Remarks",DataRowVersion.Current,this.Blast_ma_Remarks),

                new SqlParameter("@FnF_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"FnF_Status",DataRowVersion.Current,this.FnF_Status),
                new SqlParameter("@FnF_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"FnF_Remarks",DataRowVersion.Current,this.FnF_Remarks),
                new SqlParameter("@Fix_Asset_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Fix_Asset_Status",DataRowVersion.Current,this.Fix_Asset_Status),
                new SqlParameter("@Fix_Asset_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Fix_Asset_Remarks",DataRowVersion.Current,this.Fix_Asset_Remarks),
                new SqlParameter("@Infra_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Infra_Status",DataRowVersion.Current,this.Infra_Status),
                new SqlParameter("@Infra_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Infra_Remarks",DataRowVersion.Current,this.Infra_Remarks),
                new SqlParameter("@Sand_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Sand_Status",DataRowVersion.Current,this.Sand_Status),
                new SqlParameter("@Sand_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Sand_Remarks",DataRowVersion.Current,this.Sand_Remarks),
                new SqlParameter("@Jelly_Metal_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Jelly_Metal_Status",DataRowVersion.Current,this.Jelly_Metal_Status),
                new SqlParameter("@Jelly_MetalRemarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Jelly_MetalRemarks",DataRowVersion.Current,this.Jelly_MetalRemarks),

                new SqlParameter("@Red_Soil_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Red_Soil_Status",DataRowVersion.Current,this.Red_Soil_Status),
                new SqlParameter("@Red_Soil_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Red_Soil_Remarks",DataRowVersion.Current,this.Red_Soil_Remarks),
                new SqlParameter("@Cement_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Cement_Status",DataRowVersion.Current,this.Cement_Status),
                new SqlParameter("@Cement_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Cement_Remarks",DataRowVersion.Current,this.Cement_Remarks),
                new SqlParameter("@Chem_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Chem_Status",DataRowVersion.Current,this.Chem_Status),
                new SqlParameter("@Chem_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Chem_Remarks",DataRowVersion.Current,this.Chem_Remarks),
                new SqlParameter("@Brick_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Brick_Status",DataRowVersion.Current,this.Brick_Status),
                new SqlParameter("@Brick_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Brick_Remarks",DataRowVersion.Current,this.Brick_Remarks),
                new SqlParameter("@Steel_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Steel_Status",DataRowVersion.Current,this.Steel_Status),
                new SqlParameter("@Steel_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Steel_Remarks",DataRowVersion.Current,this.Steel_Remarks),

                new SqlParameter("@Oth_Const_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Oth_Const_Status",DataRowVersion.Current,this.Oth_Const_Status),
                new SqlParameter("@Oth_Const_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Oth_Const_Remarks",DataRowVersion.Current,this.Oth_Const_Remarks),
                new SqlParameter("@Other_Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Other_Status",DataRowVersion.Current,this.Other_Status),
                new SqlParameter("@Other_Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Other_Remarks",DataRowVersion.Current,this.Other_Remarks),
 
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


        public GrandAbstractBL()
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



        public string SECTOR_NAME { get; set; }

        public decimal SECTORVALUE { get; set; }
    }
}
