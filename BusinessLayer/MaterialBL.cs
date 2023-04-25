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
    public class MaterialBL
    {
         #region "Class: MaterialBL Local Declarations"

        public enum eLoadSp
        {
            SELECT_ALL = 0,
            INSERT = 1,
            UPDATE = 2,
            SELECTBYITEMCODE = 3,
            DELETEBYITEMCODE = 4,
            SELECT_BUDGET_SECTOR_ALL = 5,
            UPDATE_BUDGET_SECTOR = 6,
            SELECT_ITEMS_BY_SEARCH=7,
            SEARCH_BY_ITEM_CODE = 8,
            SELECT_BUDGET_SECTOR = 9
 
        };


        #endregion
        #region "Class: MaterialBL Sets / Gets"

       
        public int Cat_ID
        {
            get;
            set;
        }

        public int UOM
        {
            get;
            set;
        }
      
        public string Item_Code
        {
            get;
            set;
        }
        public string Item_Name
        {
            get;
            set;
        }
        public decimal? St_rate
        {
            get;
            set;
        }
        public int Budget_Sector_ID
        {
            get;
            set;
        }
        public string Sector_Prefix
        {
            get;
            set;
        }
        public string OutputResult
        {
            get;
            set;
        }
        #endregion

        #region "Class: MaterialBL Methods"

        /// <summary>
    /// Function Name:  fillCollectionFromDr.
    /// Called By:      Nill.
    /// Description:    Fill the data from data reader. 
    /// Change history: Nill.
    /// </summary>

	private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, UOM>  dicCountry) 
	{
		if(dicCountry == null)
		{
            dicCountry = new Dictionary<int, UOM>();
		}

		try
		{
			// Loop though the data reader
			while(dr.Read())
			{
				//clsCountry.CountryName
                if (dr["Item_Code"] != DBNull.Value)
				{
                    this.Item_Code = dr["Item_Code"].ToString();
				}
                if (dr["Item_Name"] != DBNull.Value)
                {
                    this.Item_Name = dr["Item_Name"].ToString();
                }
                if (dr["Cat_ID"] != DBNull.Value)
                {
                    this.Cat_ID = Convert.ToInt16(dr["Cat_ID"].ToString());
                }
                if (dr["UOM"] != DBNull.Value)
                {
                    this.UOM = Convert.ToInt16(dr["UOM"].ToString());
                }
                if (dr["St_rate"] != DBNull.Value)
                {
                    this.St_rate = Convert.ToDecimal(dr["St_rate"].ToString());
                }
                if (dr["Budget_Sector_ID"] != DBNull.Value)
                {
                    this.Budget_Sector_ID = Convert.ToInt16(dr["Budget_Sector_ID"].ToString());
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
			while(dr.Read())
			{
				
                if (dr["Item_Code"] != DBNull.Value)
				{
                    this.Item_Code = dr["Item_Code"].ToString();
				}

                if (dr["Item_Name"] != DBNull.Value)
                {
                    this.Item_Name = dr["Item_Name"].ToString();
                }
                if (dr["Cat_ID"] != DBNull.Value)
                {
                    this.Cat_ID = Convert.ToInt16(dr["Cat_ID"].ToString());
                }
                if (dr["UOM"] != DBNull.Value)
                {
                    this.UOM = Convert.ToInt16(dr["UOM"].ToString());
                }
                if (dr["St_rate"] != DBNull.Value)
                {
                    this.St_rate = Convert.ToDecimal(dr["St_rate"].ToString());
                }
                if (dr["Budget_Sector_ID"] != DBNull.Value)
                {
                    this.Budget_Sector_ID = Convert.ToInt16(dr["Budget_Sector_ID"].ToString());
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
		switch(enumSpName)
		{
			case eLoadSp.SELECT_ALL:
                return "PROC_MATERIAL_SELECT";
            case eLoadSp.INSERT:
                return "PROC_MATERIAL_INSERT";
            case eLoadSp.UPDATE:
                return "PROC_MATERIAL_UPDATE";
            case eLoadSp.SELECTBYITEMCODE:
                return "PROC_MATERAIL_SELECTBYITEMCODE";
            case eLoadSp.DELETEBYITEMCODE:
                return "PROC_MATERIAL_DELETEBYITEMCODE";
            case eLoadSp.SELECT_BUDGET_SECTOR_ALL:
                return "PROC_SELECT_BUDGET_SECTOR_ALL";
            case eLoadSp.UPDATE_BUDGET_SECTOR:
                return "PROC_UPDATE_BUDGET_SECTOR";
            case eLoadSp.SELECT_ITEMS_BY_SEARCH:
                return "SEARCH_BY_SECTOR";
            case eLoadSp.SEARCH_BY_ITEM_CODE:
                return "PROC_SEARCH_BASED_ON_ITEM_CODE";
                return "PROC_MATERIAL_DELETEBYITEMCODE";
            case eLoadSp.SELECT_BUDGET_SECTOR:
                return "PROC_SELECT_BUDGET_SECTOR";
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

		switch(enumSpName)
		{
			case eLoadSp.SELECT_ALL:
				colParams = new SqlParameter[]
				{
					//new SqlParameter("@SPParamName", this.[PropertyName] ??  (object)DBNull.Value))
				};
				break;
            case eLoadSp.SELECTBYITEMCODE:
                colParams = new SqlParameter[]
				{
                   new SqlParameter("@ITEMCODE",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Item_Code",DataRowVersion.Current,this.Item_Code),
                };
                break;

            case eLoadSp.INSERT:
                colParams = new SqlParameter[]
                {
            
                };
                break;

            case eLoadSp.DELETEBYITEMCODE:
                colParams = new SqlParameter[]
				{
                   new SqlParameter("@ITEMCODE",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Item_Code",DataRowVersion.Current,this.Item_Code),
                };
                break;
            case eLoadSp.SELECT_ITEMS_BY_SEARCH:
                colParams = new SqlParameter[]
                {
             new SqlParameter("@Budget_Sector_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Budget_Sector_ID",DataRowVersion.Current,this.Budget_Sector_ID),
             new SqlParameter("@Cat_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Cat_ID",DataRowVersion.Current,this.Cat_ID),
                };
                break;
            case eLoadSp.SEARCH_BY_ITEM_CODE:
                colParams = new SqlParameter[]
                 {
                    
                      new SqlParameter("@Item_Code", this.Item_Code)

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
	private bool insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName )
	{
		try
		{
			int intIdentityValue = 0;


			SqlParameter[] colParams = new SqlParameter[]
			{
               
                new SqlParameter("@ITEMCODE", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code),
                 new SqlParameter("@ITEMNAME", this.Item_Name),
                //new SqlParameter("@ITEMNAME", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Item_Name", DataRowVersion.Current, this.Item_Name),
                 new SqlParameter("@CATID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Cat_ID", DataRowVersion.Current, this.Cat_ID),
                new SqlParameter("@UOMID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "UOM", DataRowVersion.Current, this.UOM),
                 new SqlParameter("@STDRATE",SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "St_rate", DataRowVersion.Current, this.St_rate),
                 new SqlParameter("@BudgetSectorID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Budget_Sector_ID", DataRowVersion.Current, this.Budget_Sector_ID)
                  
			};

			if (SqlConn != null)
			{
				if (!validate())
				{
					return false;
				}

				intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                if (intIdentityValue > 0)
                {
                    this.Item_Code = (string)colParams.First().Value;
                }
                else
                {
                    this.Item_Code = (string)colParams.First().Value;
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
			if(intIdentityValue < 1)
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

			if(colParams == null) 
			{
				dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName));
			}
			else
			{
				dr = SqlHelper.ExecuteReader(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
			}		

			return true;
		}
		catch(System.Exception ex) 
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
		catch(System.Exception ex) 
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
	public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, UOM> diclCountry)
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
				
				return false;
			}				
		}
		catch(System.Exception ex) 
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
		return update (SqlConn, null, enumSpName);
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
                 new SqlParameter("@ITEMCODE", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code),
                  new SqlParameter("@ITEMNAME", this.Item_Name),
                //new SqlParameter("@ITEMNAME", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Item_Name", DataRowVersion.Current, this.Item_Name),
                 new SqlParameter("@CATID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Cat_ID", DataRowVersion.Current, this.Cat_ID),
                new SqlParameter("@UOMID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "UOM", DataRowVersion.Current, this.UOM),
                new SqlParameter("@STDRATE",SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "St_rate", DataRowVersion.Current, this.St_rate),
                 new SqlParameter("@BudgetSectorID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Budget_Sector_ID", DataRowVersion.Current, this.Budget_Sector_ID)
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

			if(intReturnVal < 1)
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

    public bool updateBudgetSector(SqlConnection SqlConn, eLoadSp enumSpName)
    {
        return updateBudgetSector(SqlConn, null, enumSpName);
    }
    private bool updateBudgetSector(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
    {
        try
        {
            int intReturnVal = 0;

            SqlParameter[] colParams = new SqlParameter[]
			{
                 new SqlParameter("@OutputResult", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, "OutputResult", DataRowVersion.Current, this.OutputResult),
                 new SqlParameter("@Budget_Sector_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Budget_Sector_ID", DataRowVersion.Current, this.Budget_Sector_ID),
                new SqlParameter("@Sector_Prefix", SqlDbType.VarChar, 5, ParameterDirection.Input, false, 0, 0, "Sector_Prefix", DataRowVersion.Current, this.Sector_Prefix)
              
			};

            if (SqlConn != null)
            {
                if (!validate())
                {
                    return false;
                }

                intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                if(intReturnVal < 1)
                {
                    this.OutputResult = colParams.First().Value.ToString();
                }
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

    public MaterialBL()
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
