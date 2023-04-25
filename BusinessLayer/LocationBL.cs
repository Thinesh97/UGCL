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
    public class LocationBL
    {
        #region "Class: LocationBL Local Declarations"

        public enum eLoadSp
        {
            SELECT_ALL = 0,
            INSERT = 1,
            UPDATE = 2,
            DELETE = 3,
            SELECTBYID = 4,
            INSERT_COUNTRY = 5,
            DELETE_COUNTRY = 6,
            INSERT_STATE = 7,
            DELETE_STATE = 8,
             INSERT_Bank=9,
            DELETE_Bank=10,
            INSERT_Bank_Details=11,
            DELETE_Bank_Details=12,
        };


        #endregion
        #region "Class: LocationBL Sets / Gets"


        public int Location_ID
        {
            get;
            set;
        }

        public string Location_Name
        {
            get;
            set;
        }
        public string Short_Name
        {
            get;
            set;
        }
        public string TIN
        {
            get;
            set;
        }
        public string GST
        {
            get;
            set;
        }
        public string Address_Line1
        {
            get;
            set;
        }
        public string Landmark
        {
            get;
            set;
        }
        public string City
        {
            get;
            set;
        }
        public string State
        {
            get;
            set;
        }
        
             public string Bank { get; set; }
        public string ID { get; set; }

        // Add bank
        public string Bank_Name { get; set; }
        public string Branch { get; set; }
        public string Account_No { get; set; }
        public string IFSC { get; set; }
        public string MICR { get; set; }
        public bool Make_Default_Bank { get; set; }
        public string RTGS { get; set; }
        public string SWIFT { get; set; }    

        public string Country
        {
            get;
            set;
        }
       
        public int? PIN
        {
            get;
            set;
        }
        public string Loc_Type
        {
            get;
            set;
        }

        public string Contact_Name
        {
            get;
            set;
        }

        public string Contact_No
        {
            get;
            set;
        }
      
        #endregion

        #region "Class: LocationBL Methods"

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
                if (dr["Location_Name"] != DBNull.Value)
				{
                    this.Location_Name = dr["Location_Name"].ToString();
				}
                if (dr["Loc_Type"] != DBNull.Value)
                {
                    this.Loc_Type = dr["Loc_Type"].ToString();
                }
                if (dr["Short_Name"] != DBNull.Value)
                {
                    this.Short_Name = dr["Short_Name"].ToString();
                }
                if (dr["TIN"] != DBNull.Value)
                {
                    this.TIN = dr["TIN"].ToString();
                }
                if (dr["GST"] != DBNull.Value)
                {
                    this.GST = dr["GST"].ToString();
                }
                if (dr["Address_Line1"] != DBNull.Value)
                {
                    this.Address_Line1 = dr["Address_Line1"].ToString();
                }
                if (dr["Landmark"] != DBNull.Value)
                {
                    this.Landmark = dr["Landmark"].ToString();
                }
                if (dr["City"] != DBNull.Value)
                {
                    this.City = dr["City"].ToString();
                }
                if (dr["State"] != DBNull.Value)
                {
                    this.State = dr["State"].ToString();
                }
                if (dr["Country"] != DBNull.Value)
                {
                    this.Country = dr["Country"].ToString();
                }
                if (dr["PIN"] != DBNull.Value)
                {
                    this.PIN = Convert.ToInt32(dr["PIN"].ToString());
                }
                if (dr["Contact_Name"] != DBNull.Value)
                {
                    this.Contact_Name = dr["Contact_Name"].ToString();
                }
                if (dr["Contact_No"] != DBNull.Value)
                {
                    this.Contact_No = dr["Contact_No"].ToString();
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
				//clsCountry.CountryName
                if (dr["Location_Name"] != DBNull.Value)
                {
                    this.Location_Name = dr["Location_Name"].ToString();
                }
                if (dr["Loc_Type"] != DBNull.Value)
                {
                    this.Loc_Type = dr["Loc_Type"].ToString();
                }
                if (dr["Short_Name"] != DBNull.Value)
                {
                    this.Short_Name = dr["Short_Name"].ToString();
                }
                if (dr["TIN"] != DBNull.Value)
                {
                    this.TIN = dr["TIN"].ToString();
                }
                if (dr["GST"] != DBNull.Value)
                {
                    this.GST = dr["GST"].ToString();
                }
                if (dr["Address_Line1"] != DBNull.Value)
                {
                    this.Address_Line1 = dr["Address_Line1"].ToString();
                }
                if (dr["Landmark"] != DBNull.Value)
                {
                    this.Landmark = dr["Landmark"].ToString();
                }
                if (dr["City"] != DBNull.Value)
                {
                    this.City = dr["City"].ToString();
                }
                if (dr["State"] != DBNull.Value)
                {
                    this.State = dr["State"].ToString();
                }
                if (dr["Country"] != DBNull.Value)
                {
                    this.Country = dr["Country"].ToString();
                }
                if (dr["PIN"] != DBNull.Value)
                {
                    this.PIN = Convert.ToInt32(dr["PIN"].ToString());
                }
                if (dr["Contact_Name"] != DBNull.Value)
                {
                    this.Contact_Name = dr["Contact_Name"].ToString();
                }
                if (dr["Contact_No"] != DBNull.Value)
                {
                    this.Contact_No = dr["Contact_No"].ToString();
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
                return "PROC_Location_SELECT";
            case eLoadSp.INSERT:
                return "PROC_LOCATION_INSERT";
            case eLoadSp.UPDATE:
                return "PROC_Location_UPDATE";
            case eLoadSp.DELETE:
                return "PROC_Location_DELETE_By_ID";
            case eLoadSp.SELECTBYID:
                return "PROC_Location_SELECT_By_ID";
            case eLoadSp.INSERT_COUNTRY:
                return "PROC_INSERT_COUNTRY";
            case eLoadSp.DELETE_COUNTRY:
                return "PROC_DELETE_COUNTRY";
                case eLoadSp.DELETE_Bank:
                    return "PROC_DELETE_Bank";
                case eLoadSp.INSERT_STATE:
                return "PROC_INSERT_STATE";
            case eLoadSp.DELETE_STATE:
                return "PROC_DELETE_STATE";
                case eLoadSp.INSERT_Bank:
                    return "PROC_INSERT_BANK_Add_New_Bank";
                case eLoadSp.INSERT_Bank_Details:
                    return "PROC_INSERT_BANK_Add_New_Bank_Details";
                case eLoadSp.DELETE_Bank_Details:
                    return "PROC_DELETE_Bank_Details";
                    

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
            case eLoadSp.DELETE:
                colParams = new SqlParameter[]
				{
					new SqlParameter("@Location_ID", this.Location_ID )
				};
                break;

            case eLoadSp.SELECTBYID:
                colParams = new SqlParameter[]
				{
					new SqlParameter("@Location_ID", this.Location_ID )
				};
                break;
            case eLoadSp.DELETE_COUNTRY:
                colParams = new SqlParameter[]
				{
					new SqlParameter("@CountryName", this.Country)
				};
                break;
                case eLoadSp.DELETE_Bank:
                    colParams = new SqlParameter[]
                    {
                    new SqlParameter("@Bank_Name", this.Bank)
                    };
                    break;
                case eLoadSp.DELETE_Bank_Details:
                    colParams = new SqlParameter[]
                    {
                    new SqlParameter("@ID", this.ID)
                    };
                    break;
                case eLoadSp.DELETE_STATE:
                colParams = new SqlParameter[]
				{
					new SqlParameter("@StateName", this.State)
				};
                break;
		}

		return colParams;
	}

        public bool insertBank(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertBank(SqlConn, null, enumSpName);
        }
        private bool insertBank(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
                {
               new SqlParameter("@Bank_Name", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Bank_Name", DataRowVersion.Current, this.Bank)


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

        public bool insertBank_Details(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertBank_Details(SqlConn, null, enumSpName);
        }
        private bool insertBank_Details(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
                {
               new SqlParameter("@Bank_Name", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Bank_Name", DataRowVersion.Current, this.Bank_Name),
                new SqlParameter("@Branch", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Branch", DataRowVersion.Current, this.Branch),
                new SqlParameter("@Account_No", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Account_No", DataRowVersion.Current, this.Account_No),
                 new SqlParameter("@IFSC", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "IFSC", DataRowVersion.Current, this.IFSC),
                new SqlParameter("@MICR", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "MICR", DataRowVersion.Current, this.MICR),
                new SqlParameter("@RTGS", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "RTGS", DataRowVersion.Current, this.RTGS),
                 new SqlParameter("@SWIFT", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "SWIFT", DataRowVersion.Current, this.SWIFT),
                new SqlParameter("@Make_Default_Bank", SqlDbType.Bit, 500, ParameterDirection.Input, false, 10, 0, "Make_Default_Bank", DataRowVersion.Current, this.Make_Default_Bank),



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


        public bool insertCountry(SqlConnection SqlConn, eLoadSp enumSpName)
    {
        return insertCountry(SqlConn, null, enumSpName);
    }

    private bool insertCountry(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
    {
        try
        {
            int intIdentityValue = 0;

            SqlParameter[] colParams = new SqlParameter[]
			{				             
               new SqlParameter("@CountryName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Country", DataRowVersion.Current, this.Country)
             
             
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

    public bool insertState(SqlConnection SqlConn, eLoadSp enumSpName)
    {
        return insertState(SqlConn, null, enumSpName);
    }

    private bool insertState(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
    {
        try
        {
            int intIdentityValue = 0;

            SqlParameter[] colParams = new SqlParameter[]
			{				             
               new SqlParameter("@CountryName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Country", DataRowVersion.Current, this.Country),
               new SqlParameter("@StateName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "State", DataRowVersion.Current, this.State)
             
             
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
               new SqlParameter("@Location_Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Location_Name", DataRowVersion.Current, this.Location_Name),
               new SqlParameter("@Short_Name", SqlDbType.VarChar,10, ParameterDirection.Input, false, 0, 0, "Short_Name", DataRowVersion.Current, this.Short_Name),
               new SqlParameter("@TIN", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "TIN", DataRowVersion.Current, this.TIN ?? (object)DBNull.Value),
               new SqlParameter("@GST", SqlDbType.VarChar, 20, ParameterDirection.Input, true,10, 0, "GST", DataRowVersion.Current, this.GST ?? (object)DBNull.Value),
               new SqlParameter("@Address_Line1", SqlDbType.VarChar, 1000, ParameterDirection.Input, true,10, 0, "Address_Line1", DataRowVersion.Current, this.Address_Line1 ?? (object)DBNull.Value),
               new SqlParameter("@Landmark", SqlDbType.VarChar, 100, ParameterDirection.Input, true,10, 0, "Landmark", DataRowVersion.Current, this.Landmark ?? (object)DBNull.Value),
               new SqlParameter("@City", SqlDbType.VarChar, 50, ParameterDirection.Input, true,10, 0, "City", DataRowVersion.Current, this.City ?? (object)DBNull.Value),
               new SqlParameter("@State", SqlDbType.VarChar, 50, ParameterDirection.Input, true,10, 0, "State", DataRowVersion.Current, this.State ?? (object)DBNull.Value),
               new SqlParameter("@Country", SqlDbType.VarChar, 50, ParameterDirection.Input, true,10, 0, "Country", DataRowVersion.Current, this.Country ?? (object)DBNull.Value),
               new SqlParameter("@PIN", SqlDbType.Int, 10, ParameterDirection.Input, true,10, 0, "PIN", DataRowVersion.Current, this.PIN ?? (object)DBNull.Value),
               new SqlParameter("@Loc_Type", SqlDbType.VarChar, 20, ParameterDirection.Input, true,10, 0, "Loc_Type", DataRowVersion.Current, this.Loc_Type ?? (object)DBNull.Value),
                new SqlParameter("@Contact_Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, true,10, 0, "Contact_Name", DataRowVersion.Current, this.Contact_Name ?? (object)DBNull.Value),
               new SqlParameter("@Contact_No", SqlDbType.NVarChar, 30, ParameterDirection.Input, true,10, 0, "Contact_No", DataRowVersion.Current, this.Contact_No ?? (object)DBNull.Value)
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
				//clsErrors.Add("VEND_LD");
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
                new SqlParameter("@Location_ID", SqlDbType.Int, 50, ParameterDirection.Input, false, 10, 0, "Location_ID", DataRowVersion.Current, this.Location_ID),
                new SqlParameter("@Location_Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Location_Name", DataRowVersion.Current, this.Location_Name),
               new SqlParameter("@Short_Name", SqlDbType.VarChar,10, ParameterDirection.Input, false, 0, 0, "Short_Name", DataRowVersion.Current, this.Short_Name),
               new SqlParameter("@TIN", SqlDbType.VarChar, 20, ParameterDirection.Input, true, 10, 0, "TIN", DataRowVersion.Current, this.TIN ?? (object)DBNull.Value),
               new SqlParameter("@GST", SqlDbType.VarChar, 20, ParameterDirection.Input, true,10, 0, "GST", DataRowVersion.Current, this.GST ?? (object)DBNull.Value),
               new SqlParameter("@Address_Line1", SqlDbType.VarChar, 1000, ParameterDirection.Input, true,10, 0, "Address_Line1", DataRowVersion.Current, this.Address_Line1 ?? (object)DBNull.Value),
               new SqlParameter("@Landmark", SqlDbType.VarChar, 100, ParameterDirection.Input, true,10, 0, "Landmark", DataRowVersion.Current, this.Landmark ?? (object)DBNull.Value),
               new SqlParameter("@City", SqlDbType.VarChar, 50, ParameterDirection.Input, true,10, 0, "City", DataRowVersion.Current, this.City ?? (object)DBNull.Value),
               new SqlParameter("@State", SqlDbType.VarChar, 50, ParameterDirection.Input, true,10, 0, "State", DataRowVersion.Current, this.State ?? (object)DBNull.Value),
               new SqlParameter("@Country", SqlDbType.VarChar, 50, ParameterDirection.Input, true,10, 0, "Country", DataRowVersion.Current, this.Country ?? (object)DBNull.Value),
               new SqlParameter("@PIN", SqlDbType.Int, 10, ParameterDirection.Input, true,10, 0, "PIN", DataRowVersion.Current, this.PIN ?? (object)DBNull.Value),
               new SqlParameter("@Loc_Type", SqlDbType.VarChar, 20, ParameterDirection.Input, true,10, 0, "Loc_Type", DataRowVersion.Current, this.Loc_Type ?? (object)DBNull.Value),
            new SqlParameter("@Contact_Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, true,10, 0, "Contact_Name", DataRowVersion.Current, this.Contact_Name ?? (object)DBNull.Value),
               new SqlParameter("@Contact_No", SqlDbType.NVarChar, 30, ParameterDirection.Input, true,10, 0, "Contact_No", DataRowVersion.Current, this.Contact_No ?? (object)DBNull.Value)
				
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


    public LocationBL()
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
