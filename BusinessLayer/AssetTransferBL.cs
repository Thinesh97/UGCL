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
    public class AssetTransferBL
    {

        #region "Class: AssetTransferBL Local Declarations"
        public enum eLoadSp
        {
            INSERT_ASSET_TRANSFER = 0,
            SELECT_ALL_ASSET_TRANSFER = 1,
            SELECT_BYID_ASSET_TRANSFER = 2,
            UPDATE_ASSET_TRANSFER = 3,
            SELECT_PROJECT_NAME=4,
            SELECT_ASSET_NAME=5,
            SELECT_ASSET_NAME_BY_ASSET_TYPE=6,
            SELECT_ASSET_NAME_BY_ASSET_CATEGORY = 7,
            SELECT_ASSIGNED_PROJECT_NAME_FOR_ASSET = 8,
            SELECT_ASSIGN_TO_PROJECT_NAME_FOR_ASSET = 9,
            DELETE_ASSET_TRANSFER_BY_ID = 10,
            ASSET_TRANSFER_SEARCH_CLICK=11,
            BIND_FROM_TO_PROJECT_LIST=12,
            BIND_ASSET_DETAILS_FROM_TO_Project=13,
            STATUS_UPADTE=14,
            BIND_ASSET_WITH_ACCEPTED=15,
            Binding_Awaiting_Asset_Status=16,
            Update_Awaiting_Asset_Status=17,
            Update_Awaiting_Asset_Status_Reject=18,
            POPULATING_VALUES_TO_PRINT_PAGE=19
           

            
        };

        #endregion
        #region "Class: AssetTransferBL Sets / Gets"


        public int UserID
        {
            get;
            set;
        }
        public int Asset_Reciever
        {
            get;
            set;
        }


        public string Asset_Name
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }
        public string FromProject
        {
            get;
            set;
        }
        public string Project_Code
        {
            get;
            set;
        }
        public int? Asset_cat_ID
        { get; set; }

        public int? Asset_Type
        {
            get;
            set;
        }
        public int? Asset_Type_ID
        {
            get;
            set;
        }
        public int? AssetTran_ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        
        public string Asset_Code
        {
            get;
            set;
        }
      
        public string Assign_To_project
        {
            get;
            set;
        }
        public DateTime Transfer_Date
        {
            get;
            set;
        }
       
        public string Category_Name
        {
            get;
            set;
        }

        public DateTime? Scheduled_Date
        {
            get;
            set;
        }
        public string Condition
        {
            get;
            set;
        }

        public bool DL
        {
            get;
            set;
        }
        public bool RC
        {
            get;
            set;
        }
        public bool Road_Tax_Reciept
        {
            get;
            set;
        }
        public bool INSURANCE
        {
            get;
            set;
        }
        public bool PERMIT
        {
            get;
            set;
        }
        public bool NOC
        {
            get;
            set;
        }
        public bool FC
        {
            get;
            set;
        }
        public bool Way_BILL
        {
            get;
            set;
        }

        public string Remarks
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
        private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, AssetTransferBL> dicCountry)
        {
            if (dicCountry == null)
            {
                dicCountry = new Dictionary<int, AssetTransferBL>();
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
                case eLoadSp.INSERT_ASSET_TRANSFER:
                    return "PRO_INSERT_ASSET_TRANSFER";
                case eLoadSp.SELECT_ALL_ASSET_TRANSFER:
                    return "PRO_SELECT_ALL_ASSET_TRANSFER";
                case eLoadSp.SELECT_BYID_ASSET_TRANSFER:
                    return "PRO_SELECT_BYID_ASSET_TRANSFER";
                case eLoadSp.UPDATE_ASSET_TRANSFER:
                    return "PRO_UPDATE_ASSET_TRANSFER";
                case eLoadSp.SELECT_PROJECT_NAME:
                    return "PROC_SELECT_PROJECT_NAME";
                case eLoadSp.SELECT_ASSET_NAME:
                    return "PROC_ASSETNAME_SELECT";
                 case eLoadSp.SELECT_ASSET_NAME_BY_ASSET_TYPE:
                    return "PROC_SELECT_ASSET_NAME_BY_ASSET_TYPE";
                 case eLoadSp.SELECT_ASSET_NAME_BY_ASSET_CATEGORY:
                    return "PROC_SELECT_ASSET_NAME_BY_ASSET_CATEGORY";
                 case eLoadSp.SELECT_ASSIGNED_PROJECT_NAME_FOR_ASSET:
                    return "PROC_SELECT_ASSIGNED_PROJECT_NAME_FOR_ASSET";
                 case eLoadSp.SELECT_ASSIGN_TO_PROJECT_NAME_FOR_ASSET:
                    return "PROC_SELECT_ASSIGN_TO_PROJECT_NAMES_FOR_ASSET";
                 case eLoadSp.DELETE_ASSET_TRANSFER_BY_ID:
                    return "PROC_DELETE_ASSET_TRANSFER_BY_ID";
                case eLoadSp.ASSET_TRANSFER_SEARCH_CLICK:
                    return "ASSET_TRANSFER_SEARCH";
                case eLoadSp.BIND_FROM_TO_PROJECT_LIST:
                    return "PROC_FROM_TO_PROJECT_LIST";
                case eLoadSp.BIND_ASSET_DETAILS_FROM_TO_Project:
                    return "ASSET_DETAILS_BY_Code_SEARCH";
                case eLoadSp.STATUS_UPADTE:
                    return "PROC_STATUS_UPDATE";
                case eLoadSp.BIND_ASSET_WITH_ACCEPTED:
                    return "PROC_ACCEPTED_ASSET";
                case eLoadSp.Update_Awaiting_Asset_Status:
                    return "Update_Awaiting_Status";
                case eLoadSp.Binding_Awaiting_Asset_Status:
                    return "PROC_AWAITING_ASSET";
                case eLoadSp.Update_Awaiting_Asset_Status_Reject:
                    return "Update_Awaiting_Status_Reject";
                case eLoadSp.POPULATING_VALUES_TO_PRINT_PAGE:
                    return "PROC_POPULATE_PRINT_CLICK";


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
                case eLoadSp.SELECT_BYID_ASSET_TRANSFER:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@AssetTran_ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "AssetTran_ID", DataRowVersion.Current, this.AssetTran_ID),
                };
                    break;
                case eLoadSp.POPULATING_VALUES_TO_PRINT_PAGE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@AssetTran_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "AssetTran_ID", DataRowVersion.Current, this.AssetTran_ID),
                };
                    break;
                case eLoadSp.SELECT_PROJECT_NAME:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Asset_Code",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"Asset_Code",DataRowVersion.Current, this.Asset_Code),
                                      
                    };
                    break;
                case eLoadSp.SELECT_ASSIGNED_PROJECT_NAME_FOR_ASSET:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Asset_Code",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"Asset_Code",DataRowVersion.Current, this.Asset_Code),
                                      
                    };
                    break;
                case eLoadSp.SELECT_ASSET_NAME_BY_ASSET_TYPE:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Asset_Type",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Asset_Type",DataRowVersion.Current, this.Asset_Type),
                                      
                    };
                    break;

                case eLoadSp.SELECT_ASSET_NAME_BY_ASSET_CATEGORY:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Asset_Category",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Asset_cat_ID",DataRowVersion.Current,this.Asset_cat_ID),
                         new SqlParameter("@Project_Code",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Project_Code",DataRowVersion.Current,this.Project_Code),
                         new SqlParameter("@task",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"task",DataRowVersion.Current,this.task)
                                      
                    };
                    break;

               
                case eLoadSp.DELETE_ASSET_TRANSFER_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@AssetTran_ID", SqlDbType.Int, 0, ParameterDirection.Input, false, 0, 0, "AssetTran_ID", DataRowVersion.Current, this.AssetTran_ID),
                };
                    break;

                case eLoadSp.SELECT_ASSIGN_TO_PROJECT_NAME_FOR_ASSET:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ProjectCode", this.Assign_To_project)
                };
                    break;

               
                case eLoadSp.SELECT_ALL_ASSET_TRANSFER:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code",this.Project_Code)
                };
                    break;
                case eLoadSp.BIND_FROM_TO_PROJECT_LIST:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code",this.Project_Code)
                };
                    break;
                case eLoadSp.ASSET_TRANSFER_SEARCH_CLICK:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code",this.Project_Code)
                };
                    break;
                case eLoadSp.BIND_ASSET_DETAILS_FROM_TO_Project:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code",this.Project_Code)
                };
                    break;
                case eLoadSp.Binding_Awaiting_Asset_Status:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@UserID",this.UserID),
                    new SqlParameter("@Project_Code",this.Project_Code)
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
			 
             
              new SqlParameter("@Asset_Code", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Asset_Code", DataRowVersion.Current, this.Asset_Code),  
              new SqlParameter("@Assign_To_project", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Assign_To_project", DataRowVersion.Current, this.Assign_To_project), 
              new SqlParameter("@Transfer_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Transfer_Date", DataRowVersion.Current, this.Transfer_Date),         
             new SqlParameter("@Scheduled_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Scheduled_Date", DataRowVersion.Current, this.Scheduled_Date ?? (Object)DBNull.Value),    
             new SqlParameter("@Condition", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Condition", DataRowVersion.Current, this.Condition), 
             new SqlParameter("@Remarks", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Remarks", DataRowVersion.Current, this.Remarks), 
             new SqlParameter("@FromProject", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "FromProject", DataRowVersion.Current, this.FromProject), 
             new SqlParameter("@Status", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Current, this.Status), 
              new SqlParameter("@Asset_Reciever", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "Asset_Reciever", DataRowVersion.Current, this.Asset_Reciever), 
               new SqlParameter("@DL", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "DL", DataRowVersion.Current, this.DL), 
                new SqlParameter("@RC", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "RC", DataRowVersion.Current, this.RC), 
                 new SqlParameter("@Road_Tax_Reciept", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "Road_Tax_Reciept", DataRowVersion.Current, this.Road_Tax_Reciept), 
                 new SqlParameter("@INSURANCE", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "INSURANCE", DataRowVersion.Current, this.INSURANCE), 
                 new SqlParameter("@PERMIT", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "PERMIT", DataRowVersion.Current, this.PERMIT), 
                 new SqlParameter("@NOC", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "NOC", DataRowVersion.Current, this.NOC), 
                 new SqlParameter("@FC", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "FC", DataRowVersion.Current, this.FC), 
                 new SqlParameter("@Way_BILL", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "Way_BILL", DataRowVersion.Current, this.Way_BILL), 




     
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
        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, AssetTransferBL> diclCountry)
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
        public bool AwaitingStatusupdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return AwaitingStatusupdate(SqlConn, null, enumSpName);
        }
        public bool AwaitingStatusupdate(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return AwaitingStatusupdate(null, SqlTran, enumSpName);
        }
        private bool AwaitingStatusupdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 1;

                SqlParameter[] colParams = new SqlParameter[]
                {
                     
                    new SqlParameter("@AssetTran_ID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "AssetTran_ID", DataRowVersion.Current, this.AssetTran_ID)
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
            catch (Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
















        public bool Statusupdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Statusupdate(SqlConn, null, enumSpName);
        }
        /// <summary>
        /// Fuction Name: update.
        /// Called By:  Nill.
        /// Description: check the sql connection.
        /// Changes history: Nill.
        /// </summary>
        public bool Statusupdate(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return Statusupdate(null, SqlTran, enumSpName);
        }
        private bool Statusupdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 1;

                SqlParameter[] colParams = new SqlParameter[]
                {
                     
                    new SqlParameter("@AssetTran_ID", SqlDbType.Int,4, ParameterDirection.Input, false, 0, 0, "AssetTran_ID", DataRowVersion.Current, this.AssetTran_ID)
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
            catch(Exception ex)
            {
                ex.Message.First();
                return false;
            }
        }
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
        /// 
       



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
                int intReturnVal = 3;

                SqlParameter[] colParams = new SqlParameter[]
			{          
             
             
             new SqlParameter("@Asset_Code", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Asset_Code", DataRowVersion.Current, this.Asset_Code),
              
             new SqlParameter("@Assign_To_project", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Assign_To_project", DataRowVersion.Current, this.Assign_To_project), 
             new SqlParameter("@Transfer_Date", SqlDbType.Date, 100, ParameterDirection.Input, false, 0, 0, "Transfer_Date", DataRowVersion.Current, this.Transfer_Date),         
             new SqlParameter("@Scheduled_Date", SqlDbType.Date, 100, ParameterDirection.Input, false, 0, 0, "Scheduled_Date", DataRowVersion.Current, this.Scheduled_Date),    
             new SqlParameter("@Condition", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Condition", DataRowVersion.Current, this.Condition), 
             new SqlParameter("@Remarks", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Remarks", DataRowVersion.Current, this.Remarks), 
                new SqlParameter("@Asset_Type_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Asset_Type_ID", DataRowVersion.Current, this.Asset_Type_ID), 
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


        public AssetTransferBL()
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

        public string task { get; set; }
    }


}
