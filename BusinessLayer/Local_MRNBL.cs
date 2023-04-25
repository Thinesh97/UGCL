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
    public class Local_MRNBL
    {
        #region "Class: Local_MRNBL Local Declarations"

        public enum eLoadSp
        {
            SELECT_MRN_BY_ID = 0,
            Insert =1,
            SELECT_MRN_ALL = 2,
            SELECT_YEAR=3,
            SELECT_SECTOR = 4,
            UPDATE = 5

        }
        #endregion

        #region "Class: Local_MRNBL Sets / Gets"

        public int Local_MRN_ID { get; set; }
        public string Project_Code	 { get; set; }
        public int Month_ID	 { get; set; }
        public int Year	 { get; set; }
        public int Budget_Sector_ID	 { get; set; }
        public decimal Amount	 { get; set; }
        public string Status { get; set; }



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
                case eLoadSp.SELECT_MRN_BY_ID:
                    return "PRO_TB_Local_MRN_SELECT_BY_ID";
                case eLoadSp.Insert:
                    return "PRO_TB_Local_MRN_Insert";
                case eLoadSp.SELECT_MRN_ALL:
                    return "PRO_TB_Local_MRN_SELECT_ALL";
                case eLoadSp.SELECT_YEAR:
                    return "PRO_Tb_MRN_Year_Select";
                case eLoadSp.SELECT_SECTOR:
                    return "PRO_Tb_Sector_Year_Select";
                case eLoadSp.UPDATE:
                    return "PRO_TB_Local_MRN_Update";
                default:
                    return string.Empty;
            }
        }



        private SqlParameter[] getSpParamArray(eLoadSp enumSpName)
        {
            SqlParameter[] colParams = new SqlParameter[]
		{
		};

            switch (enumSpName)
            {
                
                case eLoadSp.SELECT_MRN_BY_ID:
                    colParams = new SqlParameter[]
				{
					new SqlParameter("@Local_MRN_ID", this.Local_MRN_ID)
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
				new SqlParameter("@Local_MRN_ID",SqlDbType.Int,4,ParameterDirection.Output,false,0,0,"Local_MRN_ID",DataRowVersion.Current,this.Local_MRN_ID),
               	new SqlParameter("@Project_Code",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Project_Code",DataRowVersion.Current,this.Project_Code),
				new SqlParameter("@Month_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Month_ID",DataRowVersion.Current,this.Month_ID),
				new SqlParameter("@Year",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Year",DataRowVersion.Current,this.Year),			
				new SqlParameter("@Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Amount",DataRowVersion.Current,this.Amount),
                new SqlParameter("@Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Status",DataRowVersion.Current,this.Status)
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));

                    this.Local_MRN_ID = (int)colParams.First().Value;
                    //this.Budget_ID = (string)colParams[1].Value;

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
        //public bool ItemImport(SqlConnection SqlConn, eLoadSp enumSpName)
        //{
        //    return ItemImport(SqlConn, null, enumSpName);
        //}
        /// <summary>
        /// Fuction Name:   Insert.
        /// Called By:      Nill.
        /// Description:    Check the Sql transaction.
        /// Change histroy: Nill.
        /// </summary>
        /// <param name="SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        //public bool ItemImport(SqlTransaction SqlTran, eLoadSp enumSpName)
        //{
        //    return ItemImport(null, SqlTran, enumSpName);
        //}
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

        //private bool ItemImport(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        //{
        //    try
        //    {
        //        int intIdentityValue = 0;


        //        SqlParameter[] colParams = new SqlParameter[]
        //    {	
        //              new SqlParameter("@ProjectCode", this.Project_Code),
        //              new SqlParameter("@Abs_bud_ID", this.Abs_BID),
        //              new SqlParameter("@CurrentProjectAbstractID", this.CurrentAbs_BID),
        //              new SqlParameter("@BudgetType", this.Bud_type)

                 
        //    };

        //        if (SqlConn != null)
        //        {
        //            if (!validate())
        //            {
        //                return false;
        //            }

        //            intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
        //        }

        //        if (SqlTran != null)
        //        {
        //            if (!validate())
        //            {
        //                return false;
        //            }

        //            intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
        //        }
        //        if (intIdentityValue < 1)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }

        //    catch (System.Exception ex)
        //    {
        //        ex.Message.First();
        //        return false;
        //    }
        //}


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

        public bool update(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return update(SqlConn, null, enumSpName);
        }

        private bool update(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{               
                new SqlParameter("@Local_MRN_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Local_MRN_ID",DataRowVersion.Current,this.Local_MRN_ID),
               	new SqlParameter("@Project_Code",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Project_Code",DataRowVersion.Current,this.Project_Code),
				new SqlParameter("@Month_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Month_ID",DataRowVersion.Current,this.Month_ID),
				new SqlParameter("@Year",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Year",DataRowVersion.Current,this.Year),				
				new SqlParameter("@Amount",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Amount",DataRowVersion.Current,this.Amount),
                new SqlParameter("@Status",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Status",DataRowVersion.Current,this.Status)
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



#endregion
    }
}
