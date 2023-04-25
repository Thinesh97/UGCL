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
    public class UOM
    {
        #region "Class: UOMAndMaterial Local Declarations"

        public enum eLoadSp
        {
            SELECT_ALL = 0,
            INSERT = 1,
            UPDATE = 2,
            SELECTBYUOMID = 3,
            DELETEBYUOMID = 4,
           CHECKDUPLICATEUOM = 5


        };


        #endregion

        #region "Class: UOM Sets / Gets"

        public int UOM_ID
        {
            get;
            set;
        }
        public string UOMName
        {
            get;
            set;
        }
        public string UOMPrefix
        {
            get;
            set;
        }
        #endregion

        #region "Class: UOM Methods"

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
				
                if (dr["UOM"] != DBNull.Value)
				{
                    this.UOMName = dr["UOM"].ToString();
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
			
                if (dr["UOM"] != DBNull.Value)
				{
                    this.UOMName = dr["UOM"].ToString();
				}
                if (dr["UOM_ID"] != DBNull.Value)
                {
                    this.UOM_ID = Convert.ToInt32(dr["UOM_ID"]);
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
                return "PROC_UOM_SELECT";
            case eLoadSp.INSERT:
                return "PROC_UOM_INSERT_UPDATE";
            case eLoadSp.UPDATE:
                return "PROC_UOM_INSERT_UPDATE";
            case eLoadSp.SELECTBYUOMID:
                return "PROC_UOM_SELECTBYID";
            case eLoadSp.DELETEBYUOMID:
                return "PROC_UOM_DELETEBYID";
            case eLoadSp.CHECKDUPLICATEUOM:
                return "PROC_UOM_CHECKDUPLICATE";
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
					
				};
				break;
            case eLoadSp.SELECTBYUOMID:
                colParams = new SqlParameter[]
				{
                   new SqlParameter("@UOMID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"UOM_ID",DataRowVersion.Current,this.UOM_ID),
                };
                break;

            case eLoadSp.INSERT:
                colParams = new SqlParameter[]
                {
                   
                };
                break;
            case eLoadSp.DELETEBYUOMID:
                colParams = new SqlParameter[]
				{
                   new SqlParameter("@UOMID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"UOM_ID",DataRowVersion.Current,this.UOM_ID),
                };
                break;
            case eLoadSp.CHECKDUPLICATEUOM:
                colParams = new SqlParameter[]
				{
                   new SqlParameter("@UOM",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"UOM",DataRowVersion.Current,this.UOMName),
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
				new SqlParameter("@UOM", SqlDbType.NVarChar, 30, ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.UOMName),
                new SqlParameter("@UOMPrefix", SqlDbType.VarChar, 5, ParameterDirection.Input, false, 0, 0, "UOMPrefix", DataRowVersion.Current, this.UOMPrefix)
        
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
			    	new SqlParameter("@UOMID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "UOM_ID", DataRowVersion.Current, this.UOM_ID),
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
                new SqlParameter("@UOM", SqlDbType.NVarChar, 30, ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.UOMName),
				new SqlParameter("@UOMID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "UOM_ID", DataRowVersion.Current, this.UOM_ID)
               
           
				
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


    public UOM()
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
