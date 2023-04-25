using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using DataAccess;
using System.Data;
namespace BusinessLayer
{
  public   class StockBL
    {
        
        #region "Class: StockBL Local Declarations"

        public enum eLoadSp
        {
            SELECTALL_STOCK=0,
            SELECT_STOCKBY_ID=1,
            INSERT_STOCK=2,
            UPDATE_STOCK_BYID=3,
            SELECT_YEAR_FROM_STOCK = 4,
            SELECT_AGEWISE_REPORT = 5,
            SELECT_ITEM_DETAILS_BY_CODE = 6,
            SELECT_SECTORS_BY_PROJECT_CODE=7,
            BUDGET_SECTOR_ID_CLICK=8
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
        #region "Class: StockBL Sets / Gets"

        public int UserID
        {
            get;
            set;
        }

        public int Stock_ID
        {
            get;
            set;
        }
        public int Mat_cat_ID
        {
            get;
            set;
        }
        public string Item_Code
        {
            get;
            set;
        }
        public DateTime Stock_Date
        {
            get;
            set;
        }
        public DateTime? Date
        {
            get;
            set;
        }
        public string Adjust_By
        {
            get;
            set;
        }

        public string Budget_Sector
        {
            get;
            set;
        }
      
        public  string MRN_No
        {
            get;
            set;
        }

        public string Sector
        {
            get;
            set;
        }
        public string Periods
        {
            get;
            set;
        }


        public string  Vendor_Id
        {
            get;
            set;
        }
        public int UOM
        {
            get;
            set;
        }

        public int Budget_Sector_ID
        {
            get;
            set;
        }
      
        public decimal Avl_Qty
        {
            get;
            set;
        }
        public decimal Adjust_QTY
        {
            get;
            set;
        }
        public string Adjust_Type
        {
            get;
            set;
        }
        public decimal Updated_Qty
        {
            get;
            set;
        }
        public string Reason
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
        public string Is_Current
        {
            get;
            set;
        }
        public string Bill_No
        {
            get;
            set;
        }
        public decimal Rate
        {
            get;
            set;
        }

        public int? Month { get; set; }
        public int? Year { get; set; }
        public string ProjectCode { get; set; }
        public string Task { get; set; }
        #endregion

        #region "Class: StockBL Methods"

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
                case eLoadSp.INSERT_STOCK:
                    return "PRO_INSERT_STOCK";
                case eLoadSp.SELECTALL_STOCK:
                    return "PRO_SELECTALL_STOCK";
                case eLoadSp.SELECT_STOCKBY_ID:
                    return "PRO_SELECT_STOCKBY_ID";
                case eLoadSp.UPDATE_STOCK_BYID:
                    return "PRO_UPDATE_STOCK_BYID";
                case eLoadSp.SELECT_YEAR_FROM_STOCK:
                    return "PROC_SELECT_YEAR_FROM_STOCK";
                case eLoadSp.SELECT_AGEWISE_REPORT:
                    return "PROC_AGEWISE_REPORT";
                case eLoadSp.SELECT_ITEM_DETAILS_BY_CODE:
                    return "PROC_SELECT_STOCK_ITEM_DETAILS_BY_CODE";
                case eLoadSp.SELECT_SECTORS_BY_PROJECT_CODE:
                    return "SECTOR_BASED_ON_PROJECT_CODE";
                case eLoadSp.BUDGET_SECTOR_ID_CLICK:
                    return "BUDGET_SECTOR_ID_CLICK";
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
                case eLoadSp.SELECT_STOCKBY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Stock_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Stock_ID", DataRowVersion.Current, this.Stock_ID),
                };
                    break;

                case eLoadSp.SELECT_AGEWISE_REPORT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ProjectCode", this.ProjectCode ??  (object)DBNull.Value),
                    new SqlParameter("@Date", this.Date ??  (object)DBNull.Value),
                    new SqlParameter("@Year", this.Year ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_ITEM_DETAILS_BY_CODE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ProjectCode", this.ProjectCode ??  (object)DBNull.Value),
                    new SqlParameter("@Sector", this.Sector ??  (object)DBNull.Value),
                    new SqlParameter("@Periods", this.Periods ??  (object)DBNull.Value),
                    new SqlParameter("@Date", this.Stock_Date),
                    new SqlParameter("@Year", this.Year ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECTALL_STOCK:
                    colParams = new SqlParameter[]
                {                   
                   new SqlParameter("@Project_Code", this.ProjectCode),
                    new SqlParameter("@Task", this.Task)
                };
                    break;
                case eLoadSp.SELECT_SECTORS_BY_PROJECT_CODE:
                    colParams = new SqlParameter[]
                {                   
                   new SqlParameter("@Project_Code", this.ProjectCode),
                   new SqlParameter("@Month", this.Month),
                   new SqlParameter("@Year", this.Year)
                };
                    break;
                case eLoadSp.BUDGET_SECTOR_ID_CLICK:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Budget_Sector_ID", this.Budget_Sector_ID),
                    new SqlParameter("@Project_Code", this.ProjectCode),
                    new SqlParameter("@Month", this.Month),
                    new SqlParameter("@Year", this.Year)
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
                int intIdentityValue = 2;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Stock_ID", SqlDbType.Int, 4, ParameterDirection.Output, false, 0, 0, "Stock_ID", DataRowVersion.Current, this.Stock_ID), 
              	new SqlParameter("@Mat_cat_ID", SqlDbType.Int, 4,  ParameterDirection.Input, false, 0, 0, "Mat_cat_ID", DataRowVersion.Current, this.Mat_cat_ID),	
                new SqlParameter("@Item_Code", SqlDbType.VarChar,100,  ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code),
                new SqlParameter("@Stock_Date", SqlDbType.Date, 3,  ParameterDirection.Input, false, 0, 0, "Stock_Date", DataRowVersion.Current, this.Stock_Date),
                new SqlParameter("@MRN_NO", SqlDbType.VarChar, 100,  ParameterDirection.Input, false, 0, 0, "MRN_NO", DataRowVersion.Current, this.MRN_No),
                new SqlParameter("@Vendor_ID", SqlDbType.VarChar, 8, ParameterDirection.Input, false, 0, 0, "Vendor_ID", DataRowVersion.Current, this.Vendor_Id), 
              	new SqlParameter("@UOM", SqlDbType.Int, 4,  ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.UOM),	
                new SqlParameter("@Avl_Qty", SqlDbType.Decimal,9,  ParameterDirection.Input, false, 0, 0, "Avl_Qty", DataRowVersion.Current, this.Avl_Qty),
                new SqlParameter("@Adjust_QTY", SqlDbType.Decimal, 9,  ParameterDirection.Input, false, 0, 0, "Adjust_QTY", DataRowVersion.Current, this.Adjust_QTY),
                new SqlParameter("@Adjust_Type", SqlDbType.VarChar,20,  ParameterDirection.Input, false, 0, 0, "Adjust_Type", DataRowVersion.Current, this.Adjust_Type),
                new SqlParameter("@Updated_QTY", SqlDbType.Decimal, 9,  ParameterDirection.Input, false, 0, 0, "Updated_QTY", DataRowVersion.Current, this.Updated_Qty),
                new SqlParameter("@Reason", SqlDbType.VarChar,50,  ParameterDirection.Input, false, 0, 0, "Reason", DataRowVersion.Current, this.Reason),
                new SqlParameter("@Remarks", SqlDbType.VarChar,250,  ParameterDirection.Input, false, 0, 0, "Remarks", DataRowVersion.Current, this.Remarks),
                new SqlParameter("@Is_Current", SqlDbType.VarChar, 5,  ParameterDirection.Input, false, 0, 0, "Is_Current", DataRowVersion.Current, this.Is_Current),
                 new SqlParameter("@Bill_No", SqlDbType.VarChar,50,  ParameterDirection.Input, false, 0, 0, "Bill_No", DataRowVersion.Current, this.Bill_No),
                new SqlParameter("@Rate", SqlDbType.Decimal, 9,  ParameterDirection.Input, false, 0, 0, "Rate", DataRowVersion.Current, this.Rate),
                new SqlParameter("@Adjust_By", SqlDbType.VarChar,50,  ParameterDirection.Input, false, 0, 0, "Adjust_By", DataRowVersion.Current, this.Adjust_By),
                 new SqlParameter("@Project_Code", SqlDbType.VarChar,50,  ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode)


			};

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    this.Stock_ID = (int)colParams.First().Value;
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
                int intReturnVal = 3;

                SqlParameter[] colParams = new SqlParameter[]
			{
               
              new SqlParameter("@Stock_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Stock_ID", DataRowVersion.Current, this.Stock_ID), 
              	new SqlParameter("@Mat_cat_ID", SqlDbType.Int, 4,  ParameterDirection.Input, false, 0, 0, "Mat_cat_ID", DataRowVersion.Current, this.Mat_cat_ID),	
                new SqlParameter("@Item_Code", SqlDbType.VarChar,100,  ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code),
                new SqlParameter("@Stock_Date", SqlDbType.Date, 3,  ParameterDirection.Input, false, 0, 0, "Stock_Date", DataRowVersion.Current, this.Stock_Date),
                new SqlParameter("@MRN_NO", SqlDbType.VarChar, 100,  ParameterDirection.Input, false, 0, 0, "MRN_NO", DataRowVersion.Current, this.MRN_No),
                new SqlParameter("@Vendor_ID", SqlDbType.VarChar, 8, ParameterDirection.Input, false, 0, 0, "Vendor_ID", DataRowVersion.Current, this.Vendor_Id), 
              	new SqlParameter("@UOM", SqlDbType.Int, 4,  ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.UOM),	
                new SqlParameter("@Avl_Qty", SqlDbType.Decimal,9,  ParameterDirection.Input, false, 0, 0, "Avl_Qty", DataRowVersion.Current, this.Avl_Qty),
                new SqlParameter("@Adjust_QTY", SqlDbType.Decimal, 9,  ParameterDirection.Input, false, 0, 0, "Adjust_QTY", DataRowVersion.Current, this.Adjust_QTY),
                new SqlParameter("@Adjust_Type", SqlDbType.VarChar,20,  ParameterDirection.Input, false, 0, 0, "Adjust_Type", DataRowVersion.Current, this.Adjust_Type),
                new SqlParameter("@Updated_QTY", SqlDbType.Decimal, 9,  ParameterDirection.Input, false, 0, 0, "Updated_QTY", DataRowVersion.Current, this.Updated_Qty),
                new SqlParameter("@Reason", SqlDbType.VarChar,50,  ParameterDirection.Input, false, 0, 0, "Reason", DataRowVersion.Current, this.Reason),
                new SqlParameter("@Remarks", SqlDbType.VarChar,250,  ParameterDirection.Input, false, 0, 0, "Remarks", DataRowVersion.Current, this.Remarks),
                new SqlParameter("@Is_Current", SqlDbType.VarChar, 5,  ParameterDirection.Input, false, 0, 0, "Is_Current", DataRowVersion.Current, this.Is_Current),
                 new SqlParameter("@Bill_No", SqlDbType.VarChar,50,  ParameterDirection.Input, false, 0, 0, "Bill_No", DataRowVersion.Current, this.Bill_No),
                new SqlParameter("@Rate", SqlDbType.Decimal, 9,  ParameterDirection.Input, false, 0, 0, "Rate", DataRowVersion.Current, this.Rate),
                 new SqlParameter("@Adjust_By", SqlDbType.VarChar,50,  ParameterDirection.Input, false, 0, 0, "Adjust_By", DataRowVersion.Current, this.Adjust_By)

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


        public StockBL()
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
