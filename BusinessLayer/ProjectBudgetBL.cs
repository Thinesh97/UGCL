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
    public class ProjectBudgetBL
    {
        #region "Class:ProjectBudgetBL  Local Declarations"

        public enum eLoadSp
        {
            INSERT_PROJECT_BUDGET = 0,           
            UPDATE_PROJECT_BUDGET = 1,
            INSERT_PROJECT_BUDGET_SECTOR = 2,
            SELECT_ALL_PROJECT_BUDGETS = 3,
            GET_PROJECT_BUDGET_DETAILS_BY_ID = 4,
            SELECT_PROJECT_BUDGET_SECTORS_BY_ID = 5,
            DELETE_BUDGET_SECTORS_BY_ID = 6,
            SELECT_BUDGET_SECTOR_DETAILS_BY_ID = 7,
            UPDATE_PROJECT_BUDGET_SECTOR = 8
        };


        #endregion
        #region "Class: ProjectBudgetBL Sets / Gets"


        public int Proj_Bud_ID
        {
            get;
            set;
        }


        public DateTime Date
        {
            get;
            set;
        }
        public string Project_ID
        {
            get;
            set;

        }
        public int Status
        {
            get;
            set;
        }

        public int Created_By
        {
            get;
            set;
        }

        public int Proj_Budget_Sec_ID
        {
            get;
            set;
        }

        public int Budget_Sector_ID
        {
            get;
            set;
        }

        public int? Category
        {
            get;
            set;
        }

        public decimal Quantity
        {
            get;
            set;
        }

        #endregion

        #region "Class: ProjectBudgetBL Methods"

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
                case eLoadSp.INSERT_PROJECT_BUDGET_SECTOR:
                    return "PROC_INSERT_PROJECT_BUDGET_SECTOR";
                case eLoadSp.INSERT_PROJECT_BUDGET:
                    return "PROC_INSERT_PROJECT_BUDGET";
                case eLoadSp.SELECT_ALL_PROJECT_BUDGETS:
                    return "PROC_SELECT_ALL_PROJECT_BUDGETS";
                case eLoadSp.GET_PROJECT_BUDGET_DETAILS_BY_ID:
                    return "PROC_GET_PROJECT_BUDGET_DETAILS_BY_ID";
                case eLoadSp.UPDATE_PROJECT_BUDGET:
                    return "PROC_UPDATE_PROJECT_BUDGET";
                case eLoadSp.SELECT_PROJECT_BUDGET_SECTORS_BY_ID:
                    return "PROC_SELECT_PROJECT_BUDGET_SECTORS_BY_ID";
                case eLoadSp.DELETE_BUDGET_SECTORS_BY_ID:
                    return "PROC_DELETE_BUDGET_SECTORS_BY_ID";
                case eLoadSp.SELECT_BUDGET_SECTOR_DETAILS_BY_ID:
                    return "PROC_SELECT_BUDGET_SECTOR_DETAILS_BY_ID";
                case eLoadSp.UPDATE_PROJECT_BUDGET_SECTOR:
                    return "PROC_UPDATE_PROJECT_BUDGET_SECTOR_BY_ID";

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
                case eLoadSp.SELECT_ALL_PROJECT_BUDGETS:
                    colParams = new SqlParameter[]
                  {
                      new SqlParameter("@Project_ID", SqlDbType.VarChar,10, ParameterDirection.Input, false, 0, 0, "Project_ID", DataRowVersion.Current, this.Project_ID)
              
                  };
                    break;
                case eLoadSp.GET_PROJECT_BUDGET_DETAILS_BY_ID:
                case eLoadSp.SELECT_PROJECT_BUDGET_SECTORS_BY_ID:
                    colParams = new SqlParameter[]
                  {
                      //new SqlParameter("@UserID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, this.UserID)
                      new SqlParameter("@Proj_Bud_ID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "Proj_Bud_ID", DataRowVersion.Current, this.Proj_Bud_ID)
              
                  };
                    break;
                case eLoadSp.DELETE_BUDGET_SECTORS_BY_ID:
                case eLoadSp.SELECT_BUDGET_SECTOR_DETAILS_BY_ID:
                    colParams = new SqlParameter[]
                  {
                      new SqlParameter("@Proj_Budget_Sec_ID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "Proj_Budget_Sec_ID", DataRowVersion.Current, this.Proj_Budget_Sec_ID)
              
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
                    new SqlParameter("@ProjBud_OutputID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, "Proj_Bud_ID", DataRowVersion.Current, this.Proj_Bud_ID), 
                    new SqlParameter("@Project_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Project_ID", DataRowVersion.Current, this.Project_ID), 
                    new SqlParameter("@Status", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Current, this.Status),
                    new SqlParameter("@Created_By ", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Created_By", DataRowVersion.Current, this.Created_By),
                    new SqlParameter("@Date", SqlDbType.Date,3, ParameterDirection.Input, false, 0, 0, "Date", DataRowVersion.Current, this.Date)
               
			};


                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));

                    if(intIdentityValue != -1)
                    {
                        this.Proj_Bud_ID = (int)colParams.First().Value;
                    }
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
        /// Insert for Budget Sector Deatils
       /// </summary>
       /// <param name="SqlConn"></param>
       /// <param name="enumSpName"></param>
       /// <returns></returns>
        public bool insertBudgetSector(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertBudgetSector(SqlConn, null, enumSpName);
        }

        /// <summary>
        ///  Insert for Budget Sector Deatils
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="SqlTran"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        private bool insertBudgetSector(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
                {  
                    new SqlParameter("@Proj_Bud_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Proj_Bud_ID", DataRowVersion.Current, this.Proj_Bud_ID), 
                    new SqlParameter("@Budget_Sector_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Budget_Sector_ID", DataRowVersion.Current, this.Budget_Sector_ID), 
                    new SqlParameter("@Category", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Category", DataRowVersion.Current, this.Category ?? (object)DBNull.Value),
                    new SqlParameter("@Quantity", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Quantity", DataRowVersion.Current, this.Quantity)
               
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
                // int viewstbudMrid = int.Parse("ViewSta_Budget_MR_ID");

                SqlParameter[] colParams = new SqlParameter[]
			{
               
                  new SqlParameter("@Proj_Bud_ID ", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Proj_Bud_ID", DataRowVersion.Current, this.Proj_Bud_ID), 
                 new SqlParameter("@Project_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Project_ID", DataRowVersion.Current, this.Project_ID), 
                new SqlParameter("@Status", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Created_By ", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Created_By", DataRowVersion.Current, this.Created_By),
                new SqlParameter("@Date", SqlDbType.Date,3, ParameterDirection.Input, false, 0, 0, "Date", DataRowVersion.Current, this.Date)
               
				
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

        /// <summary>
        /// UPDATE PROJECT BUDGET SECTOR
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>

        public bool updateProjectBudgetSector(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateProjectBudgetSector(SqlConn, null, enumSpName);
        }


        private bool updateProjectBudgetSector(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;
                // int viewstbudMrid = int.Parse("ViewSta_Budget_MR_ID");

                SqlParameter[] colParams = new SqlParameter[]
			{
               
                    new SqlParameter("@Proj_Budget_Sec_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Proj_Budget_Sec_ID", DataRowVersion.Current, this.Proj_Budget_Sec_ID), 
                    new SqlParameter("@Budget_Sector_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Budget_Sector_ID", DataRowVersion.Current, this.Budget_Sector_ID), 
                    new SqlParameter("@Category", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Category", DataRowVersion.Current, this.Category ?? (object)DBNull.Value),
                    new SqlParameter("@Quantity", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Quantity", DataRowVersion.Current, this.Quantity)
				
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


        public ProjectBudgetBL()
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
    }
}
