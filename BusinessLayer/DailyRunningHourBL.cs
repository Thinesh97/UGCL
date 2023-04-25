using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace BusinessLayer
{
    public class DailyRunningHourBL
    {
        #region "Class: DailyRunningHourBL Local Declarations"
        public enum eLoadSp
        {
            SELECT_ALL = 0,
            INSERT = 1,
            UPDATE = 2,
            SELECT_UOM_ALL = 3,
            SELECT_Asset_Name_ALL = 4,
            SELECT_DAILY_RUNNING_HOUR_LIST=5,
            SELECT_DAILY_RUNNING_HOUR_BY_Daily_Run_Id=6,
            Total_Daily_Monthly_Diesel_Report=7,
            Running_DELETE_TYPE_BY_ID=8,
            SELECT_DAILY_MONTHLY_RUNNING_HRS_KMS_REPORT = 9,
            SELECT_YEAR_FROM_DRH = 10,
            SELECT_ASSET_PERFORMANCE_REPORT = 11,
            SELECT_AVL_DIESEL_QTY_FROM_STOCK = 12,
            CHECKING_ITEM_Remaining_Capacity=13,
            Asset_Report_Details_with_Asset_Code=14,
            VehicleWise_Performance_Cost=15,
            Asset_Recuring_Variance_Report=16,
            SELECT_AVL_DIESEL_QTY_FROM_DAILYRUNNING = 17,
           };
        
          #endregion
        #region "Class: DailyRunningHourBL Sets / Gets"

        public int UID
        {
            get;
            set;
        }

        public int? Daily_Run_Id
        {
            get;
            set;
        }

        public DateTime?  Date
        {
            get;
            set;
        }

        public string Asset_Code
        {
            get;
            set;
        }

        public string Item_Code
        {
            get;
            set;
        }
        
        public string Asset_Name
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public decimal? Start_Km
        {
            get;
            set;
        }

        public decimal? End_Km
        {
            get;
            set;
        }

        public decimal? Start_Hour
        {
            get;
            set;
        }

        public decimal? End_Hour
        {
            get;
            set;
        }

        public int? UOM_ID
        {
            get;
            set;
        }

        public string Output
        {
            get;
            set;
        }

        public decimal? Issued_Diesel_Qty
        {
            get;
            set;
        }

        public  string Remarks
        {
            get;
            set;
        }
        public DateTime? StDate
        {
            get;
            set;
        }
        public DateTime? EndDate
        {
            get;
            set;
        }
    
        public string  Project_Code
        {
            get;
            set;
        }

        public string FileUpload
        {
            get;
            set;
        }
        public string Maintance
        {
            get;
            set;
        }

        public string Task
        {
            get;
            set;
        }

        public int Asset_Category
        {
            get;
            set;
        }
        public int? Month { get; set; }
        public int? Year { get; set; }
        public string UserID { get; set; }
        #endregion

        #region "Class: Category Methods"
        /// <summary>
        /// Function Name:  fillCollectionFromDr.
        /// Called By:      Nill.
        /// Description:    Fill the data from data reader. 
        /// Change history: Nill.
        /// </summary>
        private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, DailyRunningHourBL> dicCountry)
        {
            if (dicCountry == null)
            {
                dicCountry = new Dictionary<int, DailyRunningHourBL>();
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
              
                case eLoadSp.INSERT:
                    return "PRO_Tb_Daily_Running_Hours_INSERT";
                case eLoadSp.UPDATE:
                    return "PRO_Tb_Daily_Running_Hours_UPDATE";
                case eLoadSp.SELECT_UOM_ALL:
                    return "PROC_UOM_SELECT";
                case eLoadSp.SELECT_Asset_Name_ALL:
                    return "PROC_SELECT_ASSET_NAME_BY_CATEGORY";
                  case eLoadSp.SELECT_DAILY_RUNNING_HOUR_LIST:
                    return "PRO_DAILY_MACHINE_RUNNING_HOUR_LIST";
                case eLoadSp.SELECT_DAILY_RUNNING_HOUR_BY_Daily_Run_Id:
                    return "PRO_DRH_SELECT_BY_DRHID";
                case eLoadSp.Total_Daily_Monthly_Diesel_Report:
                    return "PROC_Total_Daily_Monthly_Diesel_Report";
                case eLoadSp.Running_DELETE_TYPE_BY_ID:
                    return "PROC_TB_Running_Delete_By_ID";
                case eLoadSp.SELECT_DAILY_MONTHLY_RUNNING_HRS_KMS_REPORT:
                    return "PROC_DAILY_MONTHLY_RUNNING_HRS_KMS_REPORT";
                case eLoadSp.SELECT_YEAR_FROM_DRH:
                    return "PROC_SELECT_YEAR_FROM_DRH";
                case eLoadSp.SELECT_ASSET_PERFORMANCE_REPORT:
                    return "PRO_Asset_Performance_Report";
                case eLoadSp.SELECT_AVL_DIESEL_QTY_FROM_STOCK:
                    return "PROC_SELECT_AVL_DIESEL_QTY_FROM_STOCK";
                case eLoadSp.SELECT_AVL_DIESEL_QTY_FROM_DAILYRUNNING:
                    return "PROC_SELECT_AVL_DIESEL_QTY_FROM_DAILYRUNNING";
                case eLoadSp.CHECKING_ITEM_Remaining_Capacity:
                    return "PROC_AssetRecurringItemsForDailyRunning";
                case eLoadSp.Asset_Report_Details_with_Asset_Code:
                    return "PRO_Asset_Performance_Report_with_Asset_CODE";
                case eLoadSp.VehicleWise_Performance_Cost:
                    return "Proc_VehicleWise_Performance_Cost";
                case eLoadSp.Asset_Recuring_Variance_Report:
                    return "PROC_Asset_Recuring_Variance_Report";
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
               
               
                case eLoadSp.UPDATE:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Asset_Code",this.Asset_Code)
                    
                    };
                    break;
                case eLoadSp.SELECT_AVL_DIESEL_QTY_FROM_STOCK:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Project_Code",this.Project_Code),
                        new SqlParameter("@Date",this.Date)
                    
                    };
                    break;
                case eLoadSp.Running_DELETE_TYPE_BY_ID:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Run_Id",this.Daily_Run_Id),
                         new SqlParameter("@UserID",this.UserID)
                    };
                    break;
               
                case eLoadSp.SELECT_DAILY_RUNNING_HOUR_BY_Daily_Run_Id:
                    colParams = new SqlParameter[]
                    {new SqlParameter("@Daily_Run_Id",this.Daily_Run_Id)

                    };
                    break;
                case eLoadSp.CHECKING_ITEM_Remaining_Capacity:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@AssetCode",this.Asset_Code),
                         new SqlParameter("@Item_Code",this.Item_Code)

                    };
                    break;
                case eLoadSp.Total_Daily_Monthly_Diesel_Report:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@StDate",this.StDate),
                        new SqlParameter("@EndDate",this.EndDate),
                        new SqlParameter("@Project_Code",this.Project_Code)

                    };
                    break;
                case eLoadSp.Asset_Report_Details_with_Asset_Code:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@StartDate",this.StDate?? (Object)DBNull.Value),
                        new SqlParameter("@EndDate",this.EndDate ?? (Object)DBNull.Value),
                      new SqlParameter("@Asset_Code",this.Asset_Code)
                    

                    };
                    break;
                case eLoadSp.SELECT_DAILY_MONTHLY_RUNNING_HRS_KMS_REPORT:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@StartDate",this.StDate ?? (Object)DBNull.Value),
                        new SqlParameter("@EndDate",this.EndDate ?? (Object)DBNull.Value),
                        new SqlParameter("@ProjectCode",this.Project_Code ?? (Object)DBNull.Value)

                    };
                    break;
                case eLoadSp.SELECT_Asset_Name_ALL:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@AssetCategory",this.Asset_Category),
                         new SqlParameter("@UserID",this.UID)
                    };
                    break;
                case eLoadSp.SELECT_ASSET_PERFORMANCE_REPORT:
                    colParams = new SqlParameter[]
                    {
                      
                            new SqlParameter("@StartDate",this.StDate?? (Object)DBNull.Value),
                        new SqlParameter("@EndDate",this.EndDate ?? (Object)DBNull.Value),
                      new SqlParameter("@Project_Code",this.Project_Code)
                    

                    };
                    break;
                case eLoadSp.SELECT_DAILY_RUNNING_HOUR_LIST:
                    colParams = new SqlParameter[]
                    {
                        
                        new SqlParameter("@Project_Code",this.Project_Code)
                    };
                    break;

                case eLoadSp.VehicleWise_Performance_Cost:
                    colParams = new SqlParameter[]
                    {
                      
                            new SqlParameter("@StartDate",this.StDate?? (Object)DBNull.Value),
                        new SqlParameter("@EndDate",this.EndDate ?? (Object)DBNull.Value),
                      new SqlParameter("@ProjectCode",this.Project_Code?? (Object)DBNull.Value),
                       new SqlParameter("@AssetCode",this.Asset_Code ?? (Object)DBNull.Value),
                         new SqlParameter("@Maintance",this.Maintance ?? (Object)DBNull.Value),
                          new SqlParameter("@Task",this.Task ?? (Object)DBNull.Value),
                      
                    

                    };
                    break;

                case eLoadSp.Asset_Recuring_Variance_Report:
                    colParams = new SqlParameter[]
                    {
                      
                        new SqlParameter("@StartDate",this.StDate?? (Object)DBNull.Value),
                        new SqlParameter("@EndDate",this.EndDate ?? (Object)DBNull.Value),
                        new SqlParameter("@AssetCode",this.Asset_Code ?? (Object)DBNull.Value),
                        new SqlParameter("@Item_Code",this.Item_Code)
                      
                    

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
				
               
                new SqlParameter("@Date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Date",DataRowVersion.Current,this.Date ?? (Object)DBNull.Value ),
                new SqlParameter("@Asset_Code",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"Asset_Code",DataRowVersion.Current,this.Asset_Code),
                  // new SqlParameter("@Reg_No",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"Reg_No",DataRowVersion.Current,this.Reg_No),
                new SqlParameter("@Unit",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Unit",DataRowVersion.Current,this.Unit),
                new SqlParameter("@Start_Km",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Start_Km",DataRowVersion.Current,this.Start_Km ?? (Object)DBNull.Value),               
                new SqlParameter("@End_Km",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"End_Km",DataRowVersion.Current,this.End_Km ?? (Object)DBNull.Value),
                new SqlParameter("@Start_Hour",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Start_Hour",DataRowVersion.Current,this.Start_Hour ?? (Object)DBNull.Value),
                new SqlParameter("@End_Hour",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"End_Hour",DataRowVersion.Current,this.End_Hour ?? (Object)DBNull.Value),
                new SqlParameter("@UOM_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"UOM_ID",DataRowVersion.Current,this.UOM_ID ?? (Object)DBNull.Value),               
                new SqlParameter("@Output",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Output",DataRowVersion.Current,this.Output),
                new SqlParameter("@Issued_Diesel_Qty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Issued_Diesel_Qty",DataRowVersion.Current,this.Issued_Diesel_Qty ?? (Object)DBNull.Value),
                new SqlParameter("@Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Remarks",DataRowVersion.Current,this.Remarks ?? (Object)DBNull.Value),
                new SqlParameter("@UserID",SqlDbType.VarChar,30,ParameterDirection.Input,false,0,0,"UserID",DataRowVersion.Current,this.UserID),
                 new SqlParameter("@Project_Code",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Project_Code",DataRowVersion.Current,this.Project_Code),
                  new SqlParameter("@FileUpload",SqlDbType.VarChar,5000,ParameterDirection.Input,false,0,0,"FileUpload",DataRowVersion.Current,this.FileUpload)
               
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
        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, DailyRunningHourBL> diclCountry)
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
                
                    new SqlParameter("@Daily_Run_Id",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Daily_Run_Id",DataRowVersion.Current,this.Daily_Run_Id),               
                    new SqlParameter("@Date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Date",DataRowVersion.Current,this.Date),
                    new SqlParameter("@Asset_Code",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"Asset_Code",DataRowVersion.Current,this.Asset_Code),
                    new SqlParameter("@Unit",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"Unit",DataRowVersion.Current,this.Unit),
                    new SqlParameter("@Start_Km",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Start_Km",DataRowVersion.Current,this.Start_Km),               
                    new SqlParameter("@End_Km",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"End_Km",DataRowVersion.Current,this.End_Km),
                    new SqlParameter("@Start_Hour",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Start_Hour",DataRowVersion.Current,this.Start_Hour),
                    new SqlParameter("@End_Hour",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"End_Hour",DataRowVersion.Current,this.End_Hour),
                    new SqlParameter("@UOM_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"UOM_ID",DataRowVersion.Current,this.UOM_ID),               
                    new SqlParameter("@Output",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Output",DataRowVersion.Current,this.Output),
                    new SqlParameter("@Issued_Diesel_Qty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Issued_Diesel_Qty",DataRowVersion.Current,this.Issued_Diesel_Qty),
                    new SqlParameter("@Remarks",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Remarks",DataRowVersion.Current,this.Remarks)
                
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


        public DailyRunningHourBL()
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

