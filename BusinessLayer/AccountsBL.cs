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
    public class AccountsBL
    {
        #region "Class: MRNBL Local Declarations"

        public enum eLoadSp
        {
            SELECT_AccountList_Vendor_ALL = 1,

        };

        #endregion


        #region "Class: MRNBL Sets / Gets"
        public string Project_Code { get; set; }
        public string Vendor_SubCon_ID { get; set; }
        public string Task { get; set; }
        
        #endregion
        #region "Class: Category Methods"


        private string getSpName(eLoadSp enumSpName)
        {
            switch (enumSpName)
            {
                case eLoadSp.SELECT_AccountList_Vendor_ALL:
                    return "PRO_ACCOUNTSOPERATIONS";
                default:
                    return string.Empty;
            }
        }


        private SqlParameter[] getSpParamArray(eLoadSp enumSpName)
        {
            SqlParameter[] colParams = new SqlParameter[]
        {
        };

            switch (enumSpName)
            {

                case eLoadSp.SELECT_AccountList_Vendor_ALL:
                    colParams = new SqlParameter[]
                   {
                        new SqlParameter("@Project_Code", this.Project_Code),
                        new SqlParameter("@Vendor_SubCon_ID", this.Vendor_SubCon_ID),
                        new SqlParameter("@Task", this.Task)
                   };
                    break;
            }
            return colParams;
        }
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
