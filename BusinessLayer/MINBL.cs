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
    public class MINBL
    {

        #region "Class: MINBL Local Declarations"

        public enum eLoadSp
        {
            SELECT_ALL = 0,
            INSERT = 1,
            UPDATE = 2,
            SELECT_BY_ID = 3,
            Iteminsert = 4,
            SelectItemsAll = 5,
            SELECT_UOM_BY_ITEM_CODE = 6,
            DELETE_MINITEM_BY_ID = 7,
            SELECT_CATEGORY_FROM_STOCK = 8,
            SELECT_ITEMCODE_BY_CATID_FROM_STOCK = 9,
            SELECT_Count_RecurringItems = 10,
            SELECT_Category_From_BudgetSector = 11,
            ASSET_RecurringItems = 12,
            SELECT_AVLQTY_FROM_STOCK = 13,
            SelectItemsAll_BY_PROJECT = 14,
            DEDUCT_STOCK_BASED_ON_ISSUE_QTY = 15,
            UPDATE_STOCK_QTY=16,
            SELECT_ITEMCODE_ALL=17,
           Update_MIN_Items_BY_ID=18,
        };


        #endregion
        #region "Class: MINBL Sets / Gets"

        public int UID
        {
            get;
            set;
        }
        public string MIN_No
        {
            get;
            set;

        }
        public int Budget_Sector_ID
        {
            get;
            set;
        }
        public int? BudgetID
        {
            get;
            set;
        }
        public string IssueToFor
        {
            get;
            set;

        }
        public DateTime DATE
        {
            get;
            set;

        }
        public decimal Avl_Qty { get; set; }
        public int Stock_ID
        {
            get;
            set;

        }
        public string Task
        {
            get;
            set;

        }
        public string Project_Code
        {
            get;
            set;
        }
        public DateTime Stock_Date
        {
            get;
            set;
        }
        public string Issue_To
        {
            get;
            set;
        }
        public string Department
        {
            get;
            set;
        }
        public int? Employee_ID
        {
            get;
            set;

        }
        public string Subcon_ID
        {
            get;
            set;
        }
        public bool Recoverable
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
        public string WONo
        {
            get;
            set;
        }
        public int MIN_Item_ID
        {
            get;
            set;
        }
        public decimal ApproveQty
        {
            get;
            set;
        }
        
        public string Budget_Sector
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
        public bool Recurring
        {
            get;
            set;
        }
        public string Asset_Code
        {
            get;
            set;
        }
        public string Maintenance
        {
            get;
            set;
        }
        public decimal? Standard
        {
            get;
            set;
        }

        public DateTime? Service_Date
        {
            get;
            set;
        }

        public int Unit
        {
            get;
            set;

        }

        public decimal? Quantity
        {
            get;
            set;
        }
        public decimal? Avaliable_Qty
        {
            get;
            set;
        }
        public decimal? Issue_Qty
        {
            get;
            set;
        }
        public decimal? Final_Qty
        {
            get;
            set;
        }
        public string UserID
        {
            get;
            set;
        }
        public decimal RequestedQty
        {
            get;
            set;
        }

        public string Approver
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

        private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, MINBL> dicMINBL)
        {
            if (dicMINBL == null)
            {
                dicMINBL = new Dictionary<int, MINBL>();
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

                    if (dr["UOM"] != DBNull.Value)
                    {
                        this.Unit = Convert.ToInt32(dr["UOM"].ToString());
                    }
                    if (dr["AvailableQty"] != DBNull.Value)
                    {
                        this.Quantity = Convert.ToDecimal(dr["AvailableQty"].ToString());
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
                case eLoadSp.SELECT_ALL:
                    return "PROC_TB_MIN_SELECT_ALL";
                case eLoadSp.INSERT:
                    return "PROC_TB_MIN_INSERT";
                case eLoadSp.Iteminsert:
                    return "PRO_Tb_MIN_Items_InsertItems";
                case eLoadSp.UPDATE:
                    return "PROC_TB_MIN_UPDATE";
                case eLoadSp.SELECT_BY_ID:
                    return "PROC_TB_MIN_SELECT_BY_ID";
                case eLoadSp.Update_MIN_Items_BY_ID:
                    return "PROC_Update_MIN_Items_BY_ID";
                case eLoadSp.SelectItemsAll:
                    return "PRO_Tb_MIN_Items_SelectALLItems";
                case eLoadSp.SELECT_UOM_BY_ITEM_CODE:
                    return "PROC_SELECT_UOM_FROM_STOCK";
                case eLoadSp.DELETE_MINITEM_BY_ID:
                    return "PROC_DELETE_MIN_ITEM_BY_ID";
                case eLoadSp.SELECT_CATEGORY_FROM_STOCK:
                    return "PROC_SELECT_CATEGORY_FROM_STOCK";
                case eLoadSp.SELECT_ITEMCODE_BY_CATID_FROM_STOCK:
                    return "PROC_SELECT_ITEMCODE_BY_CATID_FROM_STOCK";
                case eLoadSp.SELECT_ITEMCODE_ALL:
                    return "PROC_SELECT_ITEMCODE_ALL";
                case eLoadSp.SELECT_Count_RecurringItems:
                    return "PROC_Count_RecurringItems";
                case eLoadSp.SELECT_Category_From_BudgetSector:
                    return "PROC_SELECT_Category_From_BudgetSector";
                case eLoadSp.ASSET_RecurringItems:
                    return "PROC_AssetRecurringItems";
                case eLoadSp.SELECT_AVLQTY_FROM_STOCK:
                    return "PROC_SELECT_AVLQTY_FROM_STOCK";
                case eLoadSp.SelectItemsAll_BY_PROJECT:
                    return "PRO_Tb_MIN_Items_SelectALLItems_By_Project";
                case eLoadSp.DEDUCT_STOCK_BASED_ON_ISSUE_QTY:
                    return "PRO_DEDUCT_STOCK_BASED_ON_ISSUE_QTY";
                case eLoadSp.UPDATE_STOCK_QTY:
                    return "PRO_DEDUCT_STOCK_BASED_ON_ISSUE_QTY";
                    
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
                case eLoadSp.SELECT_BY_ID:
                case eLoadSp.SelectItemsAll:
                    colParams = new SqlParameter[]
				{
					new SqlParameter("@MIN_No", this.MIN_No)
				};
                    break;
                case eLoadSp.SELECT_UOM_BY_ITEM_CODE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Item_Code", this.Item_Code),                   
                      new SqlParameter("@AbstractBudget_ID", this.BudgetID),
                       new SqlParameter("@Project_Code", this.Project_Code)
                };
                    break;
                case eLoadSp.Update_MIN_Items_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@MIN_Item_ID", this.MIN_Item_ID),                   
                       new SqlParameter("@ApproveQty", this.ApproveQty), 
                         new SqlParameter("@MIN_No", this.MIN_No), 
                         new SqlParameter("@Task", this.Task)
                };
                    break;
                case eLoadSp.SELECT_AVLQTY_FROM_STOCK:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Item_Code", this.Item_Code),                   
                    new SqlParameter("@Project_Code", this.Project_Code),
                    new SqlParameter("@DATE",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"DATE",DataRowVersion.Current,this.DATE),

                };
                    break;
                case eLoadSp.SELECT_Count_RecurringItems:
                case eLoadSp.ASSET_RecurringItems:
                    colParams = new SqlParameter[]
                {
                   
                       new SqlParameter("@Project_Code", this.Project_Code)
                };
                    break;
                case eLoadSp.DELETE_MINITEM_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@MINItemID", this.MIN_Item_ID),
                     new SqlParameter("@UserID", this.UserID)
                };
                    break;

                case eLoadSp.UPDATE_STOCK_QTY:
                    colParams = new SqlParameter[]
                {
                     new SqlParameter("@Avl_Qty", this.Issue_Qty),
                    new SqlParameter("@Stock_ID", this.Stock_ID),
                    new SqlParameter("@Task", this.Task)

                };

                    break;
                case eLoadSp.DEDUCT_STOCK_BASED_ON_ISSUE_QTY:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Item_Code", this.Item_Code),
                    new SqlParameter("@Project_Code", this.Project_Code),
                    new SqlParameter("@Task", this.Task)

                };
                    break;
                case eLoadSp.SELECT_ITEMCODE_BY_CATID_FROM_STOCK:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Mat_Cat_Id", this.Mat_cat_ID)
                };
                    break;
                case eLoadSp.SELECT_Category_From_BudgetSector:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@BudgetSector_ID", this.Budget_Sector_ID)
                };
                    break;
                case eLoadSp.SELECT_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code),
                    new SqlParameter("@Task", this.Task)
                };
                    break;
                case eLoadSp.SelectItemsAll_BY_PROJECT:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@ProjectCode", this.Project_Code)
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
				
                new SqlParameter("@MIN_No", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, "MIN_No", DataRowVersion.Current, this.MIN_No),
                new SqlParameter("@DATE",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"DATE",DataRowVersion.Current,this.DATE),  
                new SqlParameter("@Project_Code",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Project_Code",DataRowVersion.Current,this.Project_Code),
                new SqlParameter("@Issue_To",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"Issue_To",DataRowVersion.Current,this.Issue_To),
                new SqlParameter("@IssueToFor",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"IssueToFor",DataRowVersion.Current,this.IssueToFor ?? (object)DBNull.Value),
                //new SqlParameter("@Department",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Department",DataRowVersion.Current,this.Department ?? (object)DBNull.Value),
                //new SqlParameter("@Employee_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Employee_ID",DataRowVersion.Current,this.Employee_ID ?? (object)DBNull.Value),
                new SqlParameter("@Subcon_ID",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Subcon_ID",DataRowVersion.Current,this.Subcon_ID ?? (object)DBNull.Value),
                new SqlParameter("@Recoverable",SqlDbType.Bit,1,ParameterDirection.Input,false,0,0,"Recoverable",DataRowVersion.Current,this.Recoverable),
                new SqlParameter("@Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Remarks",DataRowVersion.Current,this.Remarks ?? (object)DBNull.Value),
                new SqlParameter("@Approver",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Approver",DataRowVersion.Current,this.Approver ?? (object)DBNull.Value),
                new SqlParameter("@WONo",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"WONo",DataRowVersion.Current,this.WONo ?? (object)DBNull.Value),
                
			};

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }


                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    this.MIN_No = (string)colParams.First().Value;
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

        //my New insertion code*************
        public bool Iteminsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Iteminsert(SqlConn, null, enumSpName);
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
        public bool Iteminsert(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return Iteminsert(null, SqlTran, enumSpName);
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
        private bool Iteminsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;


                SqlParameter[] colParams = new SqlParameter[]
			{
				
                new SqlParameter("@Budget_Sector", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Budget_Sector", DataRowVersion.Current, this.Budget_Sector),
                new SqlParameter("@Mat_cat_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Mat_cat_ID",DataRowVersion.Current,this.Mat_cat_ID),
                new SqlParameter("@Item_Code",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Item_Code",DataRowVersion.Current,this.Item_Code),
                new SqlParameter("@Recurring",SqlDbType.Bit,1,ParameterDirection.Input,false,0,0,"Recurring",DataRowVersion.Current,this.Recurring),
                new SqlParameter("@Asset_Code",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"Asset_Code",DataRowVersion.Current,this.Asset_Code ?? (object)DBNull.Value),
                new SqlParameter("@Maintenance",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"Maintenance",DataRowVersion.Current,this.Maintenance ?? (object)DBNull.Value),
                new SqlParameter("@Standard",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Standard",DataRowVersion.Current,this.Standard ?? (object)DBNull.Value),
                new SqlParameter("@Service_Date",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"Service_Date",DataRowVersion.Current,this.Service_Date ?? (object)DBNull.Value),
                new SqlParameter("@Unit",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Unit",DataRowVersion.Current,this.Unit),
                new SqlParameter("@Quantity",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"Quantity",DataRowVersion.Current,this.Quantity),
                new SqlParameter("@MIN_No",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"MIN_No",DataRowVersion.Current,this.MIN_No),
                new SqlParameter("@UserID",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"UserID",DataRowVersion.Current,this.UserID ?? (object)DBNull.Value),
                new SqlParameter("@RequestedQty",SqlDbType.Decimal,9,ParameterDirection.Input,false,0,0,"RequestedQty",DataRowVersion.Current,this.RequestedQty),
                new SqlParameter("@WONo",SqlDbType.VarChar,100,ParameterDirection.Input,false,0,0,"WONo",DataRowVersion.Current,this.WONo),
                new SqlParameter("@Date",this.DATE)
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
        public bool load(SqlConnection SqlConn, eLoadSp enumSpName, ref Dictionary<int, MINBL> dicMINBL)
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
                if (fillCollectionFromDr(dr, ref dicMINBL))
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

                new SqlParameter("@MIN_No", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "MIN_No", DataRowVersion.Current, this.MIN_No),
                new SqlParameter("@DATE",SqlDbType.Date,3,ParameterDirection.Input,false,0,0,"DATE",DataRowVersion.Current,this.DATE),
                new SqlParameter("@Project_Code",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Project_Code",DataRowVersion.Current,this.Project_Code  ??(object)DBNull.Value),
                new SqlParameter("@Issue_To",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"Issue_To",DataRowVersion.Current,this.Issue_To),
                 new SqlParameter("@IssueToFor",SqlDbType.VarChar,20,ParameterDirection.Input,false,0,0,"IssueToFor",DataRowVersion.Current,this.IssueToFor ?? (object)DBNull.Value),
                //new SqlParameter("@Department",SqlDbType.VarChar,50,ParameterDirection.Input,false,0,0,"Department",DataRowVersion.Current,this.Department  ??(object)DBNull.Value),
                //new SqlParameter("@Employee_ID",SqlDbType.Int,4,ParameterDirection.Input,false,0,0,"Employee_ID",DataRowVersion.Current,this.Employee_ID  ??(object)DBNull.Value),
                new SqlParameter("@Subcon_ID",SqlDbType.VarChar,10,ParameterDirection.Input,false,0,0,"Subcon_ID",DataRowVersion.Current,this.Subcon_ID ??(object)DBNull.Value),
                new SqlParameter("@Recoverable",SqlDbType.Bit,1,ParameterDirection.Input,false,0,0,"Recoverable",DataRowVersion.Current,this.Recoverable),
                new SqlParameter("@Remarks",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"Remarks",DataRowVersion.Current,this.Remarks ??(object)DBNull.Value),
                  new SqlParameter("@WONo",SqlDbType.VarChar,250,ParameterDirection.Input,false,0,0,"WONo",DataRowVersion.Current,this.WONo ??(object)DBNull.Value),

               
           
				
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


        public MINBL()
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





