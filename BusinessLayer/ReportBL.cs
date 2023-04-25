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
    public class ReportBL
    {
        #region "Class: ReportBL Local Declarations"

        public enum eLoadSp
        {
            POWO_Register_Report = 1,
            SELECT_PO_ITEMS_BY_PONO = 2,
            SELECT_PO_ITEM_BY_ITEM_ID = 3,
            Asset_Report = 4
        };

        #endregion

        #region "Class: ReportBL Sets / Gets"

        public string Task { get; set; }
        public DateTime? From_Date { get; set; }
        public DateTime? To_Date { get; set; }
        public string OrderType { get; set; }
        public string VendorID { get; set; }
        public string SubContractorID { get; set; }
        public string FromAmount { get; set; }
        public string ToAmount { get; set; }
        public string Projects { get; set; }
        public string Display_Columns { get; set; }

        #endregion

        #region "Class: PurchaseOrderBL Methods"

        /// <summary>
        /// Function Name:  fillCollectionFromDr.
        /// Called By:      Nill.
        /// Description:    Fill the data from data reader. 
        /// Change history: Nill.
        /// </summary>


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
                case eLoadSp.POWO_Register_Report:
                    return "PROC_POWO_Register_Report";
                case eLoadSp.Asset_Report:
                    return "PROC_Asset_Report";
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
                case eLoadSp.POWO_Register_Report:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@OrderType", this.OrderType),
                    new SqlParameter("@VendorID", this.VendorID),
                    new SqlParameter("@SubContractorID", this.SubContractorID),
                    new SqlParameter("@From_Date", this.From_Date),
                    new SqlParameter("@To_Date", this.To_Date),
                    new SqlParameter("@FromAmount", this.FromAmount),
                    new SqlParameter("@ToAmount", this.ToAmount),
                    new SqlParameter("@Projects", this.Projects),
                    new SqlParameter("@Display_Columns", this.Display_Columns)
                };
                    break;
                case eLoadSp.Asset_Report:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@StartDate", this.From_Date),
                    new SqlParameter("@EndDate", this.To_Date),
                    new SqlParameter("@ProjectCode", this.Projects),
                    new SqlParameter("@Display_Columns", this.Display_Columns)
                };
                    break;
            }

            return colParams;
        }

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
                    ds = SqlHelper.ExecuteDataset (SqlConn, CommandType.StoredProcedure, getSpName(enumSpName));
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
                if (fillObjectFromDr(dr))
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

        public ReportBL()
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
