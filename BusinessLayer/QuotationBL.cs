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
    public class QuotationBL
    {


        #region "Class: QuotationBL Local Declarations"

        public enum eLoadSp
        {
            INSERT = 0,
            SELECT_QUOTATIONLIST_ALL = 1,
            SELECT_QUOTATIONDETAILS_BY_ID = 2,
            UPDATE = 3,
            ItemINERT = 4,
            SELECT_QUOTATIONITEM_BY_QUTATIONID = 5,
            UPDATE_TAXINFO = 6,
            SELECT_QuotationItem_By_QuotationItemID = 7,
            ItemUpdateNEW = 8,
            TaxItemsInsertion = 9,
            TaxItemsUpdation = 10,
            TaxItemsSelectionByID = 11,
            TaxItemsDeletionByID = 12,
            ItemWiseTaxSelect = 13,
            DELETE_ITEMWISE_TAX = 14,
            DELETE_QUOTATION_ITEM_BY_ID = 15,
            SELECT_INDENT_ITEMS_FOR_QUOTATION = 16,
            Quotation_DELETE = 17,
            SELECT_ALL_QUOTATION_ITEMS_LIST_BY_Project=18,
            SELECT_DUPLICATE_TAX=19,
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
        #region "Class: QuotationBL Sets / Gets"


        public string QuotationNo
        {
            get;
            set;
        }

        public string OutputResult
        {
            get;
            set;
        }
        public bool Specific_quotation
        {
            get;
            set;
        }
        public int Quot_Tax_ID
        {
            get;
            set;
        }
        public string Quot_ID
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public decimal Type_Perc
        {
            get;
            set;
        }
        public decimal Type_Amount
        {
            get;
            set;
        }
        public DateTime QuotationDate
        {
            get;
            set;
        }

        public string VendorID
        {
            get;
            set;
        }

        public string SalesEmp
        {
            get;
            set;
        }

        public string PaymentTerms
        {
            get;
            set;
        }

        public string Delivery
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
        public decimal TotalAmt
        {
            get;
            set;
        }
        public decimal TotalAmt_WithTax
        {
            get;
            set;
        }

        public decimal PFAmt
        {
            get;
            set;
        }

        public bool Extra
        {
            get;
            set;
        }

        public decimal FreightAmt
        {
            get;
            set;
        }
        public decimal NetTotalAmt
        {
            get;
            set;
        }

        public int QuotationItemID
        { get; set; }

        public int CateID
        { get; set; }

        public string ItemCode
        { get; set; }

        public decimal Qty
        { get; set; }

        public decimal Price
        { get; set; }

        public decimal Amt
        { get; set; }

        public decimal CST_Tax
        { get; set; }
        public decimal CST_Amt
        { get; set; }

        public decimal VAT
        {
            get; 
            set; 
        }

        public decimal VAT_Amt
        {
            get;
            set;
        }

        public int Quot_Item_Tax_ID
        { get; set; }

        public string IndentNo
        { get; set; }

        public string Project_Code
        { get; set; }

        // //Quotation Item Details
       
        
        #endregion

        #region "Class: QuotationBL Methods"

        /// <summary>
        /// Function Name:  fillCollectionFromDr.
        /// Called By:      Nill.
        /// Description:    Fill the data from data reader. 
        /// Change history: Nill.
        /// </summary>

        private bool fillCollectionFromDr(SqlDataReader dr, ref Dictionary<int, MailConfig> dicCountry)
        {
            if (dicCountry == null)
            {
                dicCountry = new Dictionary<int, MailConfig>();
            }

            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName
                    //if (dr["CountryName"] != DBNull.Value)
                    //{
                    //    this.CountryName = dr["CountryName"].ToString();
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
                    //if (dr["CountryName"] != DBNull.Value)
                    //{
                    //    this.CountryName = dr["CountryName"].ToString();
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
                    return "PRO_Tb_Quotation_INSERT";
                case eLoadSp.SELECT_QUOTATIONLIST_ALL:
                    return "PRO_Tb_Quotation_SELECT_ALL";
                case eLoadSp.SELECT_QUOTATIONDETAILS_BY_ID:
                    return "PRO_Tb_Quotation_Details_BY_QuotationNo";
                case eLoadSp.UPDATE:
                    return "PRO_Tb_Quotation_UPDATE";
                case eLoadSp.ItemINERT:
                    return "PRO_QUOTATION_ITEM_INSERT";
                case eLoadSp.SELECT_QUOTATIONITEM_BY_QUTATIONID:
                    return "PRO_QUOTATION_ITEM_SELECT_QUOTATION_NO";
                case eLoadSp.SELECT_QuotationItem_By_QuotationItemID:
                    return "PROC_Tb_QuotationItem_By_QuotationItemID";
                case eLoadSp.ItemUpdateNEW:
                    return "PROC_Tb_QuotationItem_By_QuotationItemID_Update";
                case eLoadSp.TaxItemsInsertion:
                    return "PROC_Tb_Quotation_TaxDis_INSERT";
                case eLoadSp.TaxItemsSelectionByID:
                    return "SELECT_Tb_Quotation_TaxDis_BY_Quot_ID";
                case eLoadSp.ItemWiseTaxSelect:
                    return "SELECT_Tb_Tb_Quotation_ItemTax_BY_Quot_ID";
                case eLoadSp.TaxItemsDeletionByID:
                    return "PROC_Tb_Quotation_TaxDis_DELETE";
                case eLoadSp.DELETE_ITEMWISE_TAX:
                    return "PROC_DELETE_ITEM_WISE_TAX";
                case eLoadSp.DELETE_QUOTATION_ITEM_BY_ID:
                    return "PROC_DELETE_QUOTATION_ITEM_BY_ID";
                case eLoadSp.SELECT_INDENT_ITEMS_FOR_QUOTATION:
                    return "PROC_SELECT_INDENT_ITEMS_FOR_QUOTATION";
                case eLoadSp.Quotation_DELETE:
                    return "PRO_DELETE_Quotation_BY_QuotationNo";
                case eLoadSp.SELECT_ALL_QUOTATION_ITEMS_LIST_BY_Project:
                    return "PRO_SELECT_ALL_QUOTATION_ITEMS_LIST_BY_Project";

                case eLoadSp.SELECT_DUPLICATE_TAX:
                    return "PRO_SELECT_SELECT_DUPLICATE_TAX";
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

                case eLoadSp.TaxItemsDeletionByID:

                    colParams = new SqlParameter[]
                {
				    new SqlParameter("@Quot_Tax_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Quot_Tax_ID", DataRowVersion.Current, this.Quot_Tax_ID),
                  
                };
                    break;

                case eLoadSp.SELECT_QUOTATIONDETAILS_BY_ID:

                    colParams = new SqlParameter[]
                {
				    new SqlParameter("@QuotationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "QuotationNo", DataRowVersion.Current, this.QuotationNo),
                  
                };
                    break;
                    
                case eLoadSp.SELECT_ALL_QUOTATION_ITEMS_LIST_BY_Project:
                    colParams = new SqlParameter[]
                {
                     
                     new SqlParameter("@Project_Code", this.Project_Code)
                  
                };
                    break;

                case eLoadSp.SELECT_QuotationItem_By_QuotationItemID:
                case eLoadSp.DELETE_QUOTATION_ITEM_BY_ID:
                    colParams = new SqlParameter[]
                {
				    new SqlParameter("@QuotationItemID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "QuotationItemID", DataRowVersion.Current, this.QuotationItemID),
                  
                };
                    break;

                case eLoadSp.SELECT_QUOTATIONITEM_BY_QUTATIONID:

                    colParams = new SqlParameter[]
                {
				 
				new SqlParameter("@QuoationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "QuoationNo", DataRowVersion.Current, this.QuotationNo),

                  
                };
                    break;
                case eLoadSp.Quotation_DELETE:

                    colParams = new SqlParameter[]
                {
				 
				new SqlParameter("@QuotationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "QuoationNo", DataRowVersion.Current, this.QuotationNo),

                  
                };
                    break;

                case eLoadSp.TaxItemsSelectionByID:
                case eLoadSp.ItemWiseTaxSelect:

                    colParams = new SqlParameter[]
                {
				    new SqlParameter("@Quot_ID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "QuotationNo", DataRowVersion.Current, this.QuotationNo)
                  
                };
                    break;

                case eLoadSp.DELETE_ITEMWISE_TAX:

                    colParams = new SqlParameter[]
                {
				    new SqlParameter("@Quot_Item_Tax_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Quot_Item_Tax_ID", DataRowVersion.Current, this.Quot_Item_Tax_ID)
                  
                };
                    break;

                case eLoadSp.SELECT_INDENT_ITEMS_FOR_QUOTATION:

                    colParams = new SqlParameter[]
                {
				 
				new SqlParameter("@QuoationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "QuoationNo", DataRowVersion.Current, this.QuotationNo),
                new SqlParameter("@IndentNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "IndentNo", DataRowVersion.Current, this.IndentNo  ??  (object)DBNull.Value)
                  
                };
                    break;

                case eLoadSp.SELECT_QUOTATIONLIST_ALL:
                    colParams = new SqlParameter[]
                {
				new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                };
                break;

                case eLoadSp.SELECT_DUPLICATE_TAX:
                colParams = new SqlParameter[]
                {
				new SqlParameter("@Description", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, this.Description),
                new SqlParameter("@Quot_ID", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Quot_ID", DataRowVersion.Current, this.Quot_ID)
                };
                break;


            }


            return colParams;
        }
        //taxitems insertion for the items
        public bool TaxItemsInsertion(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return TaxItemsInsertion(SqlConn, null, enumSpName);
        }

        public bool TaxItemsInsertion(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return TaxItemsInsertion(null, SqlTran, enumSpName);
        }

        private bool TaxItemsInsertion(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@Quot_ID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Quot_ID", DataRowVersion.Current, this.Quot_ID),
                new SqlParameter("@Item_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Item_Code", DataRowVersion.Current, this.ItemCode),
                new SqlParameter("@Type", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Type", DataRowVersion.Current, this.Type),
                new SqlParameter("@Description", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Description", DataRowVersion.Current, this.Description),
                new SqlParameter("@Type_Perc", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Perc", DataRowVersion.Current, this.Type_Perc),
                new SqlParameter("@Type_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Type_Amount", DataRowVersion.Current, this.Type_Amount),
               				
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


        public bool InsertItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return InsertItem(SqlConn, null, enumSpName);
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
        public bool InsertItem(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return InsertItem(null, SqlTran, enumSpName);
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
        private bool InsertItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@QuoationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "QuoationNo", DataRowVersion.Current, this.QuotationNo),
               new SqlParameter("@VendorID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "VendorID", DataRowVersion.Current, this.VendorID),
                 new SqlParameter("@QuotationDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "QuotationDate", DataRowVersion.Current, this.QuotationDate),
            
				
                new SqlParameter("@CateID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "CateID", DataRowVersion.Current, this.CateID),
                new SqlParameter("@ItemCode", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "ItemCode", DataRowVersion.Current, this.ItemCode),
                new SqlParameter("@Qty", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Qty", DataRowVersion.Current, this.Qty),
                new SqlParameter("@Price", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Price", DataRowVersion.Current, this.Price),
                new SqlParameter("@Amt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Amt", DataRowVersion.Current, this.Amt),
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
        public bool INSERT(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return INSERT(SqlConn, null, enumSpName);
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
        public bool INSERT(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return INSERT(null, SqlTran, enumSpName);
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
        private bool INSERT(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
                new SqlParameter("@OutputResult", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, "QuotationNo", DataRowVersion.Current, this.OutputResult),
				new SqlParameter("@QuotationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "QuotationNo", DataRowVersion.Current, this.QuotationNo),
                new SqlParameter("@QuotationDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "QuotationDate", DataRowVersion.Current, this.QuotationDate),
                new SqlParameter("@VendorID", SqlDbType.VarChar, 8, ParameterDirection.Input, false, 0, 0, "VendorID", DataRowVersion.Current, this.VendorID),
                new SqlParameter("@SalesEmp", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "SalesEmp", DataRowVersion.Current, this.SalesEmp),
                new SqlParameter("@PaymentTerms", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "PaymentTerms", DataRowVersion.Current, this.PaymentTerms),
                new SqlParameter("@Delivery", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Delivery", DataRowVersion.Current, this.Delivery),
                new SqlParameter("@TotalAmt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "TotalAmt", DataRowVersion.Current, this.TotalAmt),
                new SqlParameter("@NetTotalAmt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "NetTotalAmt", DataRowVersion.Current, this.NetTotalAmt),
                new SqlParameter("@IndentNo", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "IndentNo", DataRowVersion.Current, this.IndentNo),
               	new SqlParameter("@Remarks", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Remarks", DataRowVersion.Current, this.Remarks)	,		
               	new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code)	,
		          new SqlParameter("@Specific_quotation", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Specific_quotation", DataRowVersion.Current, this.Specific_quotation),
			
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

                    this.OutputResult = (string)colParams.First().Value;


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

                if (fillCollectionFromDr(dr, ref diclCountry))
                {
                    return true;
                }
                else
                {
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
				new SqlParameter("@QuotationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "QuotationNo", DataRowVersion.Current, this.QuotationNo),
                new SqlParameter("@QuotationDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "QuotationDate", DataRowVersion.Current, this.QuotationDate),
                new SqlParameter("@VendorID", SqlDbType.VarChar, 8, ParameterDirection.Input, false, 0, 0, "VendorID", DataRowVersion.Current, this.VendorID),
                new SqlParameter("@SalesEmp", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "SalesEmp", DataRowVersion.Current, this.SalesEmp),
                new SqlParameter("@PaymentTerms", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "PaymentTerms", DataRowVersion.Current, this.PaymentTerms),
                new SqlParameter("@Delivery", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Delivery", DataRowVersion.Current, this.Delivery),              
                new SqlParameter("@IndentNo", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "IndentNo", DataRowVersion.Current, this.IndentNo),              
				new SqlParameter("@Remarks", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Remarks", DataRowVersion.Current, this.Remarks),
                  new SqlParameter("@Specific_quotation", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Specific_quotation", DataRowVersion.Current, this.Specific_quotation),
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

        //for item updation in the popup
        public bool ItemUpdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ItemUpdate(SqlConn, null, enumSpName);
        }

        public bool ItemUpdate(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return ItemUpdate(null, SqlTran, enumSpName);
        }



        private bool ItemUpdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]

			{ 

                new SqlParameter("@QuotationItemID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "QuotationItemID", DataRowVersion.Current,this.QuotationItemID),
                new SqlParameter("@QuoationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "QuoationNo", DataRowVersion.Current, this.QuotationNo),
                     new SqlParameter("@VendorID", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "VendorID", DataRowVersion.Current, this.VendorID),
                 new SqlParameter("@QuotationDate", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "QuotationDate", DataRowVersion.Current, this.QuotationDate),

                new SqlParameter("@CateID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "CateID", DataRowVersion.Current, this.CateID),
                new SqlParameter("@ItemCode", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "ItemCode", DataRowVersion.Current, this.ItemCode),
                new SqlParameter("@Qty", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Qty", DataRowVersion.Current, this.Qty),
                new SqlParameter("@Price", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Price", DataRowVersion.Current, this.Price),
                new SqlParameter("@Amt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Amt", DataRowVersion.Current, this.Amt)               

       
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
        /// </summary>
        /// <param name="SqlConn"></param>
        /// <param name="enumSpName"></param>
        /// <returns></returns>
        public bool updateTax(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateTax(SqlConn, null, enumSpName);
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
        public bool updateTax(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return updateTax(null, SqlTran, enumSpName);
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
        private bool updateTax(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
			{
				new SqlParameter("@QuotationNo", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "QuotationNo", DataRowVersion.Current, this.QuotationNo),               
                new SqlParameter("@PFAmt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "PFAmt", DataRowVersion.Current, this.PFAmt ),
                new SqlParameter("@FreightAmt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "FreightAmt", DataRowVersion.Current, this.FreightAmt ),
                new SqlParameter("@NetTotalAmt", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "NetTotalAmt", DataRowVersion.Current, this.NetTotalAmt ),
                new SqlParameter("@Extra", SqlDbType.Bit, 9, ParameterDirection.Input, false, 0, 0, "Extra", DataRowVersion.Current, this.Extra ),
              
				
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


        public QuotationBL()
        {

        }

        private bool validate()
        {
            return true;
        }




        #endregion


    }
}
