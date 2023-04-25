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
    public class AssetRegistrationBL
    {
        #region "Class: AssetRegistrationBL Local Declarations"

        public enum eLoadSp
        {
            INSERT_ASSET_TYPE = 0,
            SELECT_ASSET_TYPE_ALL = 1,
            INSERT_ASSET_CATEGORY = 2,
            SELECT_ASSET_CATEGORY_ALL = 3,
            INSERT_ASSET = 4,
            SELECT_ASSET_ALL = 5,
            SELECT_ASSET_DETAILS_BY_CODE = 6,
            DELETE_ASSET_TYPE_BY_ID=7,
            UPDATE_ASSET_TYPE=8,
            DELETE_ASSET_CATEGORY_DELETE_BY_ID=9,
            SELECT_Asset_BY_ID =10,
            DELETE_ASSET_BY_CODE = 11,
            PROJECT_BASED_LOCATION=12,
            Search_WITH_Project_Code_AND_DATE=13

        };

        #endregion

        #region "Class: AssetRegistrationBL Sets / Gets"

        public string Project_Code
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;

        }
        public decimal Count
        {
            get;
            set;

        }
        //Asset Type
        public int? Asset_Type_ID
        {
            get;
            set;
        }
        public string StandardInput
        {
            get;
            set;
        }
        public int OutputUOM
        {
            get;
            set;
        }
        public int UserID
        {
            get;
            set;
        }
        public string Asset_Type
        { get; set; }
        public string HSD_Recoverable
        { get; set; }
        public DateTime? Recieve_Date
        {
            get;
            set;
        }
        
        // Asset Category
        public int? Asset_cat_ID
        { get; set; }

        public string Category_Name
        {
            get;
            set;
        }
        public string Cat_Prefix
        {
            get;
            set;
        }
    //Asset
        public string Owner
        { get; set; }
        public string Code
        { get; set; }
        public string Name
        { get; set; }

        public string Reg_No
        { get; set; }

        public string Project
        { get; set; }

        public int? Location
        { get; set; }

        public string EngineNo
        { get; set; }

        public DateTime? InsValidDate
        { get; set; }
      
        public string Bill_No
        { get; set; }

        public string Vendor
        { get; set; }
      
        public DateTime? Bill_date
        { get; set; }
        public decimal? Inv_Amount
        { get; set; }

        public decimal StandardInputPerHour
        { get; set; }

        public decimal StandardOutputPerHour
        { get; set; }
        public string Make
        { get; set; }
        public string Condition
        { get; set; }
        public string Remark
        { get; set; }

        public decimal? Diesel
        { get; set; }
        public decimal? HireCharges
        { get; set; }
        public decimal? RunningHrsKms
        { get; set; }
        public string Unit
        { get; set; }
        public string Contractor
        { get; set; }
        public string Average
        { get; set; }

        public string File_RC { get; set; }
        public string File_FC { get; set; }
        public string File_Permit { get; set; }
        public string File_Insurance { get; set; }
        public string File_Misc1 { get; set; }
        public string File_Misc2 { get; set; }

        #endregion

        #region "Class: AssetRegistrationBL Methods"

        /// <summary>
        /// Function Name:  fillCollectionFromDr.
        /// Called By:      Nill.
        /// Description:    Fill the data from data reader. 
        /// Change history: Nill.
        /// </summary>

        private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, ProjectBL> dicCountry)
        {
            if (dicCountry == null)
            {
                dicCountry = new Dictionary<int, ProjectBL>();
            }

            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName
                    if (dr["Indent_No"] != DBNull.Value)
                    {
                        this.Code = dr["Indent_No"].ToString();
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
                while (dr.Read())
                {
                    //clsCountry.CountryName
                    if (dr["UOM"] != DBNull.Value)
                    {
                        //this.UOM = dr["UOM"].ToString();
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
            switch (enumSpName)
            {               
                case eLoadSp.INSERT_ASSET_TYPE:
                    return "PROC_INSERT_ASSET_TYPE";
                case eLoadSp.SELECT_ASSET_TYPE_ALL:
                    return "PROC_SELECT_ALL_ASSET_TYPE";
                case eLoadSp.INSERT_ASSET_CATEGORY:
                    return "PROC_INSERT_ASSET_CATEGORY";
                case eLoadSp.SELECT_ASSET_CATEGORY_ALL:
                    return "PROC_SELECT_ALL_ASSET_CATEGORY";
                case eLoadSp.INSERT_ASSET:
                    return "PROC_INSERT_ASSET";
                case eLoadSp.SELECT_ASSET_ALL:
                    return "PROC_ASSET_SELECT_ALL";
                case eLoadSp.SELECT_ASSET_DETAILS_BY_CODE:
                    return "PROC_SELECT_ASSET_BY_CODE";
                case eLoadSp.DELETE_ASSET_TYPE_BY_ID:
                    return "PROC_ASSET_TYPE_DELETE_BY_ID";
                case eLoadSp.UPDATE_ASSET_TYPE:
                    return "PROC_UPDATE_ASSET_TYPE";

                case eLoadSp.DELETE_ASSET_CATEGORY_DELETE_BY_ID:
                    return "PROC_ASSET_CATEGORY_DELETE_BY_ID";
                case eLoadSp.SELECT_Asset_BY_ID:
                    return "PROC_SELECT_Asset_BY_ID";
                case eLoadSp.DELETE_ASSET_BY_CODE:
                    return "PROC_ASSET_DELETE_BY_ID";
                case eLoadSp.PROJECT_BASED_LOCATION:
                    return "PROJECT_BASED_LOCATION";
                case eLoadSp.Search_WITH_Project_Code_AND_DATE:
                    return "PRO_GETASEET_FOR_DailyrunningHour";
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
                case eLoadSp.SELECT_ASSET_DETAILS_BY_CODE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Asset_Code", this.Code)
                };
                    break;
                case eLoadSp.SELECT_Asset_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Asset_Type", this.Asset_Type)
                };
                    break;
                case eLoadSp.DELETE_ASSET_TYPE_BY_ID:
                    colParams = new SqlParameter[]
                    { 
                       new SqlParameter("@Asset_Type_ID",this.Asset_Type_ID)
                     
                    };
                    break;
                case eLoadSp.UPDATE_ASSET_TYPE:
                    colParams = new SqlParameter[]
                    { new SqlParameter("@Code",this.Code)
                    };
                    break;
                case eLoadSp.DELETE_ASSET_CATEGORY_DELETE_BY_ID:
                    colParams = new SqlParameter[]
                    { new SqlParameter("@Asset_cat_ID",this.Asset_cat_ID)
                    };
                    break;

                case eLoadSp.DELETE_ASSET_BY_CODE:
                    colParams = new SqlParameter[]
                    { 
                       
                      new SqlParameter("@Asset_Code",this.Code)
                    };
                    break;
                case eLoadSp.PROJECT_BASED_LOCATION:
                    colParams = new SqlParameter[]
                    { 
                       
                     new SqlParameter("@Project_Code", this.Project_Code ??  (object)DBNull.Value)
                    };
                    break;
                case eLoadSp.SELECT_ASSET_ALL:
                    colParams = new SqlParameter[]
                    { 
                       
                    
                     new SqlParameter("@Project_Code", this.Project_Code)
                    };
                    break;
                case eLoadSp.Search_WITH_Project_Code_AND_DATE:
                    colParams = new SqlParameter[]
                    { 
                       
                    new SqlParameter("@Date",this.Date),
                     new SqlParameter("@Project_Code", this.Project_Code)
                    };
                    break;
            }

            return colParams;
        }

        public bool AssetTypeInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return AssetTypeInsert(SqlConn, null, enumSpName);
        }

        private bool AssetTypeInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Asset_Type", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Asset_Type", DataRowVersion.Current, this.Asset_Type)
               
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

        public bool AssetCategoryInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return AssetCategoryInsert(SqlConn, null, enumSpName);
        }

        private bool AssetCategoryInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Asset_Type", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Asset_Type", DataRowVersion.Current, this.Asset_Type_ID ?? (Object)DBNull.Value),
                 new SqlParameter("@Category_Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Category_Name", DataRowVersion.Current, this.Category_Name),
                  new SqlParameter("@Cat_Prefix", SqlDbType.VarChar, 5, ParameterDirection.Input, false, 0, 0, "Cat_Prefix", DataRowVersion.Current, this.Cat_Prefix)
               
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
				
                new SqlParameter("@Asset_Type", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Asset_Type", DataRowVersion.Current, this.Asset_Type_ID),
                new SqlParameter("@Asset_category", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Asset_category", DataRowVersion.Current, this.Asset_cat_ID),
                new SqlParameter("@Owner", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "Owner", DataRowVersion.Current, this.Owner),
                new SqlParameter("@Code", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Code", DataRowVersion.Current, this.Code),
                new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, this.Name),
                new SqlParameter("@Reg_No", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 10, 0, "Reg_No", DataRowVersion.Current, this.Reg_No ?? (object)DBNull.Value),
                
                new SqlParameter("@Location", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Location", DataRowVersion.Current, this.Location),
                new SqlParameter("@Bill_No", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Bill_No", DataRowVersion.Current, this.Bill_No ?? (object)DBNull.Value),
                new SqlParameter("@Bill_date", SqlDbType.Date, 3, ParameterDirection.Input, false, 10, 0, "Bill_date", DataRowVersion.Current, this.Bill_date ?? (object)DBNull.Value),
                new SqlParameter("@Vendor", SqlDbType.VarChar, 8, ParameterDirection.Input, false, 0, 0, "Vendor", DataRowVersion.Current, this.Vendor ?? (object)DBNull.Value),
                new SqlParameter("@Inv_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Inv_Amount", DataRowVersion.Current, this.Inv_Amount ?? (object)DBNull.Value),
                new SqlParameter("@Make", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Make", DataRowVersion.Current, this.Make ?? (object)DBNull.Value ?? (object)DBNull.Value),
                new SqlParameter("@HireCharges", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "HireCharges", DataRowVersion.Current, this.HireCharges ?? (object)DBNull.Value),
                new SqlParameter("@Project", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Project", DataRowVersion.Current, this.Project_Code),
                
                new SqlParameter("@Condition", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Condition", DataRowVersion.Current, this.Condition),
                new SqlParameter("@Remark", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Remark", DataRowVersion.Current, this.Remark ?? (object)DBNull.Value ?? (object)DBNull.Value),
               
                new SqlParameter("@RunningHrsKms", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "RunningHrsKms", DataRowVersion.Current, this.RunningHrsKms ?? (object)DBNull.Value),
                new SqlParameter("@Unit", SqlDbType.VarChar, 5, ParameterDirection.Input, false, 0, 0, "Unit", DataRowVersion.Current, this.Unit ?? (object)DBNull.Value),
                new SqlParameter("@Average", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Average", DataRowVersion.Current, this.Average ?? (object)DBNull.Value),
                new SqlParameter("@Contractor", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "Contractor", DataRowVersion.Current, this.Contractor ?? (object)DBNull.Value),
                new SqlParameter("@StandardInputPerHour", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "StandardInputPerHour", DataRowVersion.Current, this.StandardInputPerHour ),
                new SqlParameter("@StandardOutputPerHour", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "StandardOutputPerHour", DataRowVersion.Current, this.StandardOutputPerHour),
                new SqlParameter("@StandardInput", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 10, 0, "StandardInput", DataRowVersion.Current, this.StandardInput),
                new SqlParameter("@OutputUOM", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "OutputUOM", DataRowVersion.Current, this.OutputUOM ),
                new SqlParameter("@HSDRecoverable", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "HSDRecoverable", DataRowVersion.Current, this.HSD_Recoverable),
                new SqlParameter("@RecieveDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 10, 0, "Recieve_Date", DataRowVersion.Current, this.Recieve_Date ?? (object)DBNull.Value),

                new SqlParameter("@EngineNo", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 10, 0, "EngineNo", DataRowVersion.Current, this.EngineNo ?? (object)DBNull.Value),
                new SqlParameter("@InsValidDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 10, 0, "InsValidDate", DataRowVersion.Current, this.InsValidDate ?? (object)DBNull.Value),

                new SqlParameter("@File_RC", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_RC", DataRowVersion.Current, this.File_RC ?? (Object)DBNull.Value),
                new SqlParameter("@File_FC", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_FC", DataRowVersion.Current, this.File_FC ?? (Object)DBNull.Value),
                new SqlParameter("@File_Permit", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_Permit", DataRowVersion.Current, this.File_Permit ?? (Object)DBNull.Value),
               	new SqlParameter("@File_Insurance", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_Insurance", DataRowVersion.Current, this.File_Insurance ?? (Object)DBNull.Value),
                new SqlParameter("@File_Misc1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_Misc1", DataRowVersion.Current, this.File_Misc1 ?? (Object)DBNull.Value),
                new SqlParameter("@File_Misc2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_Misc2", DataRowVersion.Current, this.File_Misc2 ?? (Object)DBNull.Value)


			};

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                  

                   // this.Code = (string)colParams[3].Value.ToString();
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
        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, ProjectBL> diclCountry)
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
				new SqlParameter("@Asset_Type", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Asset_Type", DataRowVersion.Current, this.Asset_Type_ID),
                new SqlParameter("@Asset_category", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Asset_category", DataRowVersion.Current, this.Asset_cat_ID),
                new SqlParameter("@Owner", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "Owner", DataRowVersion.Current, this.Owner),
                new SqlParameter("@Code", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Code", DataRowVersion.Current, this.Code),
                new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, this.Name),
                new SqlParameter("@Reg_No", SqlDbType.VarChar, 15, ParameterDirection.Input, false, 10, 0, "Reg_No", DataRowVersion.Current, this.Reg_No ?? (object)DBNull.Value),
               
                new SqlParameter("@Location", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Location", DataRowVersion.Current, this.Location),
                new SqlParameter("@Bill_No", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Bill_No", DataRowVersion.Current, this.Bill_No ?? (object)DBNull.Value),
                new SqlParameter("@Bill_date", SqlDbType.Date, 3, ParameterDirection.Input, false, 10, 0, "Bill_date", DataRowVersion.Current, this.Bill_date ?? (object)DBNull.Value),
                new SqlParameter("@Vendor", SqlDbType.VarChar, 8, ParameterDirection.Input, false, 0, 0, "Vendor", DataRowVersion.Current, this.Vendor ?? (object)DBNull.Value),
                new SqlParameter("@Inv_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "Inv_Amount", DataRowVersion.Current, this.Inv_Amount ?? (object)DBNull.Value),
                new SqlParameter("@Make", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Make", DataRowVersion.Current, this.Make ?? (object)DBNull.Value ?? (object)DBNull.Value),
                new SqlParameter("@HireCharges", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "HireCharges", DataRowVersion.Current, this.HireCharges ?? (object)DBNull.Value),

                new SqlParameter("@Condition", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Condition", DataRowVersion.Current, this.Condition),
                new SqlParameter("@Remark", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Remark", DataRowVersion.Current, this.Remark ?? (object)DBNull.Value ?? (object)DBNull.Value),
                
                new SqlParameter("@Unit", SqlDbType.VarChar, 5, ParameterDirection.Input, false, 0, 0, "Unit", DataRowVersion.Current, this.Unit ?? (object)DBNull.Value),
                new SqlParameter("@Average", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Average", DataRowVersion.Current, this.Average ?? (object)DBNull.Value),
                new SqlParameter("@Contractor", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "Contractor", DataRowVersion.Current, this.Contractor ?? (object)DBNull.Value),
                new SqlParameter("@StandardInputPerHour", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "StandardInputPerHour", DataRowVersion.Current, this.StandardInputPerHour ),
                new SqlParameter("@StandardOutputPerHour", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 10, 0, "StandardOutputPerHour", DataRowVersion.Current, this.StandardOutputPerHour),
                new SqlParameter("@StandardInput", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 10, 0, "StandardInput", DataRowVersion.Current, this.StandardInput),
                new SqlParameter("@OutputUOM", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "OutputUOM", DataRowVersion.Current, this.OutputUOM),
                new SqlParameter("@HSDRecoverable", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "HSDRecoverable", DataRowVersion.Current, this.HSD_Recoverable),
                new SqlParameter("@RecieveDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 10, 0, "Recieve_Date", DataRowVersion.Current, this.Recieve_Date ?? (object)DBNull.Value),

                new SqlParameter("@EngineNo", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 10, 0, "EngineNo", DataRowVersion.Current, this.EngineNo ?? (object)DBNull.Value),
                new SqlParameter("@InsValidDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 10, 0, "InsValidDate", DataRowVersion.Current, this.InsValidDate ?? (object)DBNull.Value),

                new SqlParameter("@File_RC", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_RC", DataRowVersion.Current, this.File_RC ?? (Object)DBNull.Value),
                new SqlParameter("@File_FC", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_FC", DataRowVersion.Current, this.File_FC ?? (Object)DBNull.Value),
                new SqlParameter("@File_Permit", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_Permit", DataRowVersion.Current, this.File_Permit ?? (Object)DBNull.Value),
               	new SqlParameter("@File_Insurance", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_Insurance", DataRowVersion.Current, this.File_Insurance ?? (Object)DBNull.Value),
                new SqlParameter("@File_Misc1", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_Misc1", DataRowVersion.Current, this.File_Misc1 ?? (Object)DBNull.Value),
                new SqlParameter("@File_Misc2", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "File_Misc2", DataRowVersion.Current, this.File_Misc2 ?? (Object)DBNull.Value)

             
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


        public AssetRegistrationBL()
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
