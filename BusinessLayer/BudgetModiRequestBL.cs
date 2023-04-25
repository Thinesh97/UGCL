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
   public  class BudgetModiRequestBL
    {
       
            #region "Class:BudgetModiRequestBL  Local Declarations"

            public enum eLoadSp
            {
                INSERT_BUDMOD_REQUEST=0,
                SELECT_MONTHLYBUDGET=1,
                BINDALL_BUD_MOD_REQUEST=2,
                BUDMOD_REQUEST_BYID=3,
                UPDATE_BUDMOD_REQUEST=4

            };


            #endregion
            #region "Class: BudgetBL Sets / Gets"


            public int Budget_MR_ID
            {
                get;
                set;
            }
         

            public string Project_Code
            {
                get;
                set;
            }
            public string Budget_Sector
            {
                get;
                set;

            }
            public string  Monthly_Budget
            {
                get;
                set;
            }
            public int Requested_By
            {
                get;
                set;
            }
            public int  Approved_By
            {
                get;
                set;

            }
            public int UserID
            {
                get;
                set;

            }
            public string Reason
            {
                get;
                set;
            }
            public string Name
            {
                get;
                set;
            }
         



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
                    case eLoadSp.SELECT_MONTHLYBUDGET:
                        return "PRO_SELECT_MONTHLYBUDGET";
                    case eLoadSp.INSERT_BUDMOD_REQUEST:
                        return "PRO_INSERT_BUDMOD_REQUEST";
                    case eLoadSp.BINDALL_BUD_MOD_REQUEST:
                        return "PRO_BINDALL_BUD_MOD_REQUEST";
                    case eLoadSp.BUDMOD_REQUEST_BYID:
                        return "PRO_BUDMOD_REQUEST_BYID";
                    case eLoadSp.UPDATE_BUDMOD_REQUEST:
                        return "PRO_UPDATE_BUDMOD_REQUEST";
                    
                   
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
                case eLoadSp.BUDMOD_REQUEST_BYID:
                  colParams = new SqlParameter[]
                  {
                      new SqlParameter("@Budget_MR_ID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "Budget_MR_ID", DataRowVersion.Current, this.Budget_MR_ID)
              
                  };
                        break;
                case eLoadSp.BINDALL_BUD_MOD_REQUEST:
                        colParams = new SqlParameter[]
                  {
                      //new SqlParameter("@UserID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "UserID", DataRowVersion.Current, this.UserID)
                      new SqlParameter("@Project_Code", SqlDbType.VarChar,50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code)
              
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

            public string result { get; set; }

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
                 int   intIdentityValue=0;

                    SqlParameter[] colParams = new SqlParameter[]
                         {
                            new SqlParameter("@result", SqlDbType.VarChar, 10, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Current, this.result),
                            new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                            new SqlParameter("@Monthly_Budget ", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Monthly_Budget", DataRowVersion.Current, this.Monthly_Budget),
                            new SqlParameter("@Request_By", SqlDbType.Int, 30, ParameterDirection.Input, false, 0, 0, "Request_By", DataRowVersion.Current, this.Requested_By),
                            new SqlParameter("@Approved_By ", SqlDbType.Int, 30, ParameterDirection.Input, false, 0, 0, "Approved_By", DataRowVersion.Current, this.Approved_By),
                            new SqlParameter("@Reason", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Reason", DataRowVersion.Current, this.Reason),
               
          			
			            };
			

                    if (SqlConn != null)
                    {
                        if (!validate())
                        {
                            return false;
                        }

                        intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));

                         this.result = colParams.First().Value.ToString();
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
                    int intReturnVal = 4;
                   // int viewstbudMrid = int.Parse("ViewSta_Budget_MR_ID");

                    SqlParameter[] colParams = new SqlParameter[]
			{
               
                
                      
            
                  new SqlParameter("@Budget_MR_ID ", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Budget_MR_ID", DataRowVersion.Current, this.Budget_MR_ID), 
                 new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code), 
               
                new SqlParameter("@Monthly_Budget ", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Monthly_Budget", DataRowVersion.Current, this.Monthly_Budget),
                new SqlParameter("@Request_By", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Request_By", DataRowVersion.Current, this.Requested_By),
                new SqlParameter("@Approved_By ", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Approved_By", DataRowVersion.Current, this.Approved_By),
                new SqlParameter("@Reason", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Reason", DataRowVersion.Current, this.Reason),
               
				
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


            public BudgetModiRequestBL()
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

