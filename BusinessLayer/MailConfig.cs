using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using DataAccess;





public class MailConfig
{
#region "Class: MailConfig Local Declarations"

	public enum eLoadSp
	{
		SELECT_ALL = 0,
        UPDATE = 1
       
		
	};

	
	   
 

#endregion
    /// <summary>
    /// Class Name:     fillCollectionFromDr.
    /// Called By:      Nill.
    /// Description:    Get or set the data from parameter value. 
    /// Change history: Nill.
    /// </summary>
    /// <param name="dr"></param>
    /// <param name="dicAssociateMapping"></param>
    /// <returns></returns>
#region "Class: MailConfig Sets / Gets"

	public string CountryName
	{
		get;set;
	}	
    public int CountryID
    {
        get;set;
    }
    public string SMTPHost
    {
        get;set;
    }
    public int PortNo
    {
        get;set;
    }
    public string EmailID
    {
        get;set;
    }
    public string Password
    {
        get;
        set;
    }
    public string SSLAble
    {
        get;
        set;
    }
#endregion

#region "Class: MailConfig Methods"

    /// <summary>
    /// Function Name:  fillCollectionFromDr.
    /// Called By:      Nill.
    /// Description:    Fill the data from data reader. 
    /// Change history: Nill.
    /// </summary>

	private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, MailConfig>  dicCountry) 
	{
		if(dicCountry == null)
		{
			dicCountry = new Dictionary<int, MailConfig>();
		}

		try
		{
			// Loop though the data reader
			while(dr.Read())
			{
				//clsCountry.CountryName
				if(dr["CountryName"] != DBNull.Value)
				{
					this.CountryName = dr["CountryName"].ToString();
				}			

			
			}
			return true;
		}
		catch (System.Exception ex)
		{
            ex.Message.Contains("");  
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
				//clsCountry.CountryName
				if(dr["CountryName"] != DBNull.Value)
				{
					this.CountryName = dr["CountryName"].ToString();
				}

				

			}
			return true;
		}
		catch (System.Exception ex)
		{
            ex.Message.Contains("");  
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
                return "PROC_MailConfig_SELECT";
            case eLoadSp.UPDATE:
                return "PROC_MailConfig_UPDATE";
			 
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

            case eLoadSp.UPDATE:
                colParams = new SqlParameter[]
                {
                    //new SqlParameter("@SMTPHost", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "SMTPHost", DataRowVersion.Current, this.SMTPHost),
                    //new SqlParameter("@SMTPPortNo",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Port_no",DataRowVersion.Current,this.PortNo),
                    // new SqlParameter("@EmailID", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Current, this.SMTPHost),
                    //  new SqlParameter("@Password", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Current, this.SMTPHost),
                    //   new SqlParameter("@SSL", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "SSL_able", DataRowVersion.Current, this.SMTPHost)
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
				new SqlParameter("@CountryName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "CountryName", DataRowVersion.Current, this.CountryName ?? (object)DBNull.Value ),
				//new SqlParameter("@SubCountryName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "SubCountryName", DataRowVersion.Current, this.SubCountryName ?? (object)DBNull.Value )
				//new SqlParameter("@CountryID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "CountryID", DataRowVersion.Current, this.CountryID ?? (object)DBNull.Value )
			};

			if (SqlConn != null)
			{
				if (!validate())
				{
					return false;
				}

				intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
			}

			if (SqlTran != null)
			{
				if (!validate())
				{
					return false;
				}

				intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteScalar(SqlTran, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
			}
			if(intIdentityValue < 1)
			{				
				return false;
			}
			return true;
		}

		catch (System.Exception ex)
        {
            ex.Message.Contains("");  
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
            ex.Message.Contains("");  	
           
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
            ex.Message.Contains("");  
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
	public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, MailConfig> diclCountry)
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
		catch(System.Exception ex) 
        {
            ex.Message.Contains("");  	
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
            ex.Message.Contains("");         
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
            ex.Message.Contains("");           
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
                //new SqlParameter("@CountryName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "CountryName", DataRowVersion.Current, this.CountryName ?? (object)DBNull.Value ),
				//new SqlParameter("@SubCountryName", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "SubCountryName", DataRowVersion.Current, this.SubCountryName ?? (object)DBNull.Value ),
                 new SqlParameter("@SMTPHost", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "SMTPHost", DataRowVersion.Current, this.SMTPHost),
                    new SqlParameter("@SMTPPortNo",SqlDbType.Int, 4,ParameterDirection.Input,false,0,0,"Port_no",DataRowVersion.Current,this.PortNo),
                     new SqlParameter("@EmailID", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Email", DataRowVersion.Current, this.EmailID),
                      new SqlParameter("@Password", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "Password", DataRowVersion.Current, this.Password),
                       new SqlParameter("@SSL", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "SSL_able", DataRowVersion.Current, this.SSLAble)

				
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
            ex.Message.Contains("");  
			return false;
		}
	}


	public MailConfig()
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
		catch (Exception ex)
        {
            ex.Message.Contains("");     
			return false;
		}
	}



#endregion
}
