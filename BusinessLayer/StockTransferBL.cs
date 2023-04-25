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
    public class StockTransferBL
    {
          #region "Class: StockBL Local Declarations"

        public enum eLoadSp
        {
            SELECT_AVL_QTY_SECTOR_WISE_FROM_STOCK_BY_PROJECT = 0,
            SELECT_SECTORWISE_ITEMS_FOR_STOCK_TRANSFER = 1,
            INSERT_TO_STOCK_TRANSFER = 2,
            SELECT_ALL_STOCK_TRANSFER = 3,
            SELECT_SECTORWISEITEMS_FROM_STOCK_TRANSFER = 4,
            SELECT_STOCK_TRANSFER_REQUEST_FOR_USER = 5,
            SELECT_SECTORWISEITEMS_FROM_STOCK_TRANSFER_FOR_USER = 6,
            UPDATE_STOCK_TRANSFER_STATUS_BY_ID = 7,
            INSERT_TRANSFERRED_ITEMS_TO_STOCK = 8
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
        public string FromProjectCode 
        { 
            get;
            set;
        }
        public string ToProjectCode
        {
            get;
            set;
        }
        public int StockTransfer_ID
        {
            get;
            set;
        }
        public int Stock_Receiver
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
       

        public string Budget_Sector
        {
            get;
            set;
        }


        public decimal AvailableQty
        {
            get;
            set;
        }

        public decimal TransferQty
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }

        public int? Month { get; set; }
        public int? Year { get; set; }
       
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
               
                case eLoadSp.SELECT_AVL_QTY_SECTOR_WISE_FROM_STOCK_BY_PROJECT:
                    return "PROC_GET_AVL_QTY_BY_SECTOR_WISE_FROM_STOCK";
                case eLoadSp.SELECT_SECTORWISE_ITEMS_FOR_STOCK_TRANSFER:
                    return "PROC_SELECT_STOCK_ITEM_DETAILS_BY_SECTOR_FOR_STOCK_TRANSFER";
                case eLoadSp.INSERT_TO_STOCK_TRANSFER:
                    return "PROC_INSERT_TO_STOCK_TRANSFER";
                case eLoadSp.SELECT_ALL_STOCK_TRANSFER:
                    return "PROC_SELECT_ALL_STOCK_TRANSFER";
                case eLoadSp.SELECT_SECTORWISEITEMS_FROM_STOCK_TRANSFER:
                    return "PROC_SELECT_ITEM_DETAILS_FROM_STOCK_TRANSFER";
                case eLoadSp.SELECT_STOCK_TRANSFER_REQUEST_FOR_USER:
                    return "PROC_SELECT_ALL_STOCK_TRANSFER_REQUEST_FOR_USER";
                case eLoadSp.SELECT_SECTORWISEITEMS_FROM_STOCK_TRANSFER_FOR_USER:
                    return "PROC_SELECT_ITEM_DETAILS_FROM_STOCK_TRANSFER_FOR_USER";
                case eLoadSp.UPDATE_STOCK_TRANSFER_STATUS_BY_ID:
                    return "PROC_UPDATE_STOCK_TRANSFER_BY_ID";
                case eLoadSp.INSERT_TRANSFERRED_ITEMS_TO_STOCK:
                    return "PROC_INSERT_TO_STOCK_BASED_ON_STOCK_TRANSFER";
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
                case eLoadSp.SELECT_ALL_STOCK_TRANSFER:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ProjectCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.FromProjectCode)
                };
                    break;
                case eLoadSp.SELECT_AVL_QTY_SECTOR_WISE_FROM_STOCK_BY_PROJECT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@FromProjectCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.FromProjectCode)
                };
                    break;
                case eLoadSp.SELECT_SECTORWISE_ITEMS_FOR_STOCK_TRANSFER:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@FromProjectCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.FromProjectCode),
                     new SqlParameter("@BudgetSector", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Budget_Sector", DataRowVersion.Current, this.Budget_Sector)
                };
                    break;
                case eLoadSp.SELECT_SECTORWISEITEMS_FROM_STOCK_TRANSFER:
                    colParams = new SqlParameter[]
                {
                   
                     new SqlParameter("@BudgetSector", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Budget_Sector", DataRowVersion.Current, this.Budget_Sector),
                     new SqlParameter("@ProjectCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ToProjectCode),
                      new SqlParameter("@FromProjectCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.FromProjectCode)
                };
                    break;
                case eLoadSp.SELECT_STOCK_TRANSFER_REQUEST_FOR_USER:
                    colParams = new SqlParameter[]
                {
                   
                     new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Stock_Receiver", DataRowVersion.Current, this.Stock_Receiver),
                     new SqlParameter("@ProjectCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "FromProjectCode", DataRowVersion.Current, this.FromProjectCode)
                };
                    break;
                case eLoadSp.SELECT_SECTORWISEITEMS_FROM_STOCK_TRANSFER_FOR_USER:
                    colParams = new SqlParameter[]
                {
                   
                     new SqlParameter("@BudgetSector", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Budget_Sector", DataRowVersion.Current, this.Budget_Sector),
                       new SqlParameter("@UserID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Stock_Receiver", DataRowVersion.Current, this.Stock_Receiver),
                        new SqlParameter("@FromProjectCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "FromProjectCode", DataRowVersion.Current, this.FromProjectCode),
                         new SqlParameter("@ProjectCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ToProjectCode", DataRowVersion.Current, this.ToProjectCode)
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
        public bool insertTransferredItemsToStock(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertTransferredItemsToStock(SqlConn, null, enumSpName);
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
        public bool insertTransferredItemsToStock(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insertTransferredItemsToStock(null, SqlTran, enumSpName);
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
        private bool insertTransferredItemsToStock(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = -1;

                SqlParameter[] colParams = new SqlParameter[]
			{
                
                new SqlParameter("@Item_Code", SqlDbType.VarChar,100,  ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code),
              
                new SqlParameter("@StockTransferID", SqlDbType.Int,4,  ParameterDirection.Input, false, 0, 0, "StockTransfer_ID", DataRowVersion.Current, this.StockTransfer_ID),
                 new SqlParameter("@ProjectCode", SqlDbType.VarChar,50,  ParameterDirection.Input, false, 0, 0, "FromProjectCode", DataRowVersion.Current, this.FromProjectCode)
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
        public bool insertToStockTransfer(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertToStockTransfer(SqlConn, null, enumSpName);
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
        public bool insertToStockTransfer(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insertToStockTransfer(null, SqlTran, enumSpName);
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
        private bool insertToStockTransfer(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = -1;

                SqlParameter[] colParams = new SqlParameter[]
			{
                
                new SqlParameter("@Item_Code", SqlDbType.VarChar,100,  ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code),
                new SqlParameter("@AvailableQty", SqlDbType.Decimal, 9,  ParameterDirection.Input, false, 0, 0, "AvailableQty", DataRowVersion.Current, this.AvailableQty),
                new SqlParameter("@TransferQty", SqlDbType.Decimal,9,  ParameterDirection.Input, false, 0, 0, "TransferQty", DataRowVersion.Current, this.TransferQty),
                new SqlParameter("@FromProjectCode", SqlDbType.VarChar,50,  ParameterDirection.Input, false, 0, 0, "From_ProjectCode", DataRowVersion.Current, this.FromProjectCode),
                new SqlParameter("@ToProjectCode", SqlDbType.VarChar,50,  ParameterDirection.Input, false, 0, 0, "To_ProjectCode", DataRowVersion.Current, this.ToProjectCode),
                new SqlParameter("@Stock_Receiver", SqlDbType.Int,4,  ParameterDirection.Input, false, 0, 0, "Stock_Receiver", DataRowVersion.Current, this.Stock_Receiver)

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
        public bool updateStockTransfer(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateStockTransfer(SqlConn, null, enumSpName);
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
        public bool updateStockTransfer(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return updateStockTransfer(null, SqlTran, enumSpName);
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
        private bool updateStockTransfer(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = -1;

                SqlParameter[] colParams = new SqlParameter[]
			{
               
             
                new SqlParameter("@Item_Code", SqlDbType.VarChar,100,  ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.Item_Code),
                new SqlParameter("@StockTransfer_ID", SqlDbType.Int,4,  ParameterDirection.Input, false, 0, 0, "StockTransfer_ID", DataRowVersion.Current, this.StockTransfer_ID),
            

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


        public StockTransferBL()
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
