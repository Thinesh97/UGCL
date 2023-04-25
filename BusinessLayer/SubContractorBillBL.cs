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
   public class SubContractorBillBL
    {
        #region "Class: MRNBL Local Declarations"

        public enum eLoadSp
        {
            SELECT_CONTRACTOR_ALL=1,
            SELECT_WO_NUMBER=2,
            INSERT=3,
            SUB_CONTRACTOR_BILL_OPERATIONS=4,
            INSERT_DPR_ENTRY=5,
            SELECT_UOM_ALL=6,
            SELECT_DISCRIPTIONOFWORK = 7,
            DELETE_WORK_DESCRIPTION = 8,
            INSERT_WORK_DESCRIPTION=9,
            SELECT_DPR_DETAILS = 10,
            SELECT_PAYMENT_INDENT = 11,
            INSERT_SUB_CONTRACTOR_BILL=12,
            GET_SC_BILL_DETAIL=13,
            SC_DELETE=14,
            INSERT_NMR=15,
            UPDATE_DPR=16,
            SELECT_DPRREPORT_BY_DPR_No=17,
            SELECT_NMR_BYID=18,
            SELECT_NMR=19,
            INSERT_RC=20,
            RATECARD_INSERT_UPDATE=21,
            GET_SC_BILL_DETAIL_RateCard_Items = 22,
            SELECT_DPR_DETAILS_SC_PRINT=23,
            GETTAX_SC=24,
            INSERT_LABOURTYPE=25,
            InsertTB_NMR_Labour=26,
            Select_NMR_Labour_All=27,
            SUB_CONTRACTOR_BILL_LABOUR_LIST=28,
            GET_SC_BILL_DETAIL_RateCard_Items_NMR=29,
        };

        #endregion


        #region "Class: MRNBL Sets / Gets"
        public string Task { get; set; }
        public string Project_Code { get; set; }
        public string SubContractorID { get; set; }
        public string WONo  { get; set; }
       // Rate Crad
        public string RateCard_ID { get; set; }
        public DateTime DateRC { get; set; }
        public string WO_Type_RC { get; set; }
        public string FixedOrRecurring { get; set; }
        public string WO_Description { get; set; }

        public string Discription_Of_WorkRCItem { get; set; }
        public decimal QuantityRCItem { get; set; }
        public int UOMRCItem { get; set; }
        public decimal RateRCItem { get; set; }
        public decimal AmountRCItem { get; set; }
           
           
           
           
        // DPR 
        public string DPR_ID { get; set; }
        public string DPR_No { get; set; }
        public string Financial_Year { get; set; }
        public string Work_Description { get; set; }
        public DateTime BillingFrom_Date { get; set; }
        public DateTime Billing_To_Date { get; set; }
        public int SubWork_ID { get; set; }
        public int Work_Name_ID { get; set; }
        public int Location_ID { get; set; }

        // DPR Daily_Entry
        public string Location_Chainage  { get; set; }
        public string Work_Done_Activity { get; set; }
        public string Discription_of_Work { get; set; }
        public decimal Present_Progress  { get; set; }
        public decimal Cumulative_Progress { get; set; }
        public string UOM { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public string DPRFile_Path { get; set; }
        public decimal No_Men { get; set; }
        public decimal No_Women { get; set; }
        public decimal No_Heplers { get; set; }
        public int NMR_ID { get; set; }
        // Work_Description 
        public int Work_DescriptionID { get; set; }
        #endregion

       //SC Bill
        public string SC_Bill_ID { get; set; }
        public string RA_Bill_No { get; set; }
        public DateTime SC_Bill_Date { get; set; }
        public string SC_Project_Code { get; set; }
        public string SC_SubContractorID { get; set; }
        public string SC_WONo { get; set; }
        public DateTime SC_BillingFrom_Date { get; set; }
        public DateTime SC_Billing_To_Date { get; set; }
        public string SC_Work_Description { get; set; }
        public string SC_Financial_Year { get; set; }
        public decimal IGST { get; set; }
        public decimal CGST { get; set; }
        public decimal SGST { get; set; }
        public decimal TDS { get; set; }
        public decimal RetentionPerc { get; set; }
        public decimal RetentionAmount { get; set; }
        public string RetentionType{ get; set; }
        public bool NMR { get; set; }
        public bool DPR { get; set; }
       //Add Labour 
        public DateTime Labour_Date { get; set; }
        public string Labour_Type_Labour { get; set; }
        public decimal NoOf_Labour { get; set; }
        public decimal Labour_Rate { get; set; }
        public decimal LabourCost_Total { get; set; }
       //Labour Type
        public string Labour_Type { get; set; }
       // NMR
        public int ID { get; set; }
        public string NMR_No { get; set; }
        public DateTime BillDate_NMR { get; set; }
        public string Project_NMR { get; set; }
        public string WO_NMR { get; set; }
        public string WorkDescription_NMR { get; set; }
        //public decimal NoOf_Men { get; set; }
        //public decimal Men_Rate { get; set; }
        //public decimal Men_Total { get; set; }
        //public decimal NoOf_Women { get; set; }
        //public decimal Women_Rate { get; set; }
        //public decimal Women_Total { get; set; }
        //public decimal NoOf_Helpers { get; set; }
        //public decimal Helpers_Rate { get; set; }
        public decimal Helpers_Total { get; set; }
        public decimal TotalCost { get; set; }
        public string WorkDone_At { get; set; }
        public string SubContractor_NMR { get; set; }
       // MIN 
        public string MIN_Item_ID { get; set; }
       public decimal MIN_Item_Rate{ get; set; }
       public decimal Retention { get; set; }
       
        #region "Class: Category Methods"


        private string getSpName(eLoadSp enumSpName)
        {
            switch (enumSpName)
            {
                case eLoadSp.INSERT_WORK_DESCRIPTION:
                    return "PRO_DISCRIPTION_OF_WORK";
                case eLoadSp.SELECT_CONTRACTOR_ALL:
                    return "PRO_contractor_SELECT_ALL";
                case eLoadSp.SELECT_WO_NUMBER:
               
                case eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS:
                case eLoadSp.GET_SC_BILL_DETAIL:
                case eLoadSp.SELECT_DISCRIPTIONOFWORK:
                case eLoadSp.DELETE_WORK_DESCRIPTION:
                case eLoadSp.SELECT_PAYMENT_INDENT:
                case eLoadSp.SC_DELETE:
                case eLoadSp.SELECT_DPRREPORT_BY_DPR_No:
                case eLoadSp.SELECT_NMR_BYID:
                case eLoadSp.SELECT_NMR:
                case eLoadSp.GET_SC_BILL_DETAIL_RateCard_Items:
                case eLoadSp.GET_SC_BILL_DETAIL_RateCard_Items_NMR:
                case eLoadSp.SELECT_DPR_DETAILS_SC_PRINT:
                case eLoadSp.GETTAX_SC:
                case eLoadSp.INSERT_LABOURTYPE:
                case  eLoadSp.InsertTB_NMR_Labour:
                case eLoadSp.Select_NMR_Labour_All:
                case eLoadSp.SUB_CONTRACTOR_BILL_LABOUR_LIST:
                    return "PRO_SUBCONTRACTORBILLS_OPERATIONS";
                default:
                    return string.Empty;
                case eLoadSp.INSERT:
                    return "PROC_DPR_INSERT_UPDATE";
                case eLoadSp.INSERT_RC:
                    return "PRO_RC_INSERT";
                case eLoadSp.RATECARD_INSERT_UPDATE:
                    return "PROC_RATECARD_INSERT_UPDATE";
                case eLoadSp.INSERT_DPR_ENTRY:
                    return "PROC_DPR_Report_INSERT_UPDATE";
                case eLoadSp.SELECT_UOM_ALL:
                    return "PROC_UOM_SELECT";
                case eLoadSp.INSERT_SUB_CONTRACTOR_BILL:
                    return "PROC_INSERT_SUB_CONTRACTOR_BILL";
                case eLoadSp.INSERT_NMR:
                    return "PROC_INSERT_NMR";
                case eLoadSp.UPDATE_DPR:
                    return "PROC_DPR_UPDATE";
                case eLoadSp.SELECT_DPR_DETAILS:
                    return "PRO_SELECT_DPR_ENTRY";
            }
        }
        private SqlParameter[] getSpParamArray(eLoadSp enumSpName)
        {
            SqlParameter[] colParams = new SqlParameter[]
        {
        };

            switch (enumSpName)
            {

                case eLoadSp.Select_NMR_Labour_All:
                     colParams = new SqlParameter[]
                    {
                        new SqlParameter("@NMR_No", this.NMR_No),
                        new SqlParameter("@Task", this.Task)
                    };
                    break;
                case eLoadSp.SC_DELETE:
                     colParams = new SqlParameter[]
                    {
                        new SqlParameter("@RA_Bill_No", this.RA_Bill_No),
                        new SqlParameter("@Task", this.Task)
                    };
                    break;
                case eLoadSp.SELECT_NMR_BYID:
                    colParams = new SqlParameter[]
                   {
                        new SqlParameter("@ID", this.ID),
                        new SqlParameter("@Task", this.Task)
                   };
                    break;

                case eLoadSp.GETTAX_SC:
                    colParams = new SqlParameter[]
                   {
                       new SqlParameter("@RA_Bill_No", this.RA_Bill_No),
                        new SqlParameter("@Task", this.Task)
                   };
                    break;
                case eLoadSp.GET_SC_BILL_DETAIL_RateCard_Items:
                    colParams = new SqlParameter[]
                   {
                       new SqlParameter("@WONo", this.WONo),
                        new SqlParameter("@RateCard_ID", this.RateCard_ID),
                        new SqlParameter("@Task", this.Task)
                   };
                    break;
                case eLoadSp.GET_SC_BILL_DETAIL_RateCard_Items_NMR:
                    colParams = new SqlParameter[]
                   {
                       new SqlParameter("@WO_NMR", this.WO_NMR),
                        new SqlParameter("@BillingFrom_Date", this.BillingFrom_Date),
                        new SqlParameter("@Billing_To_Date  ", this.Billing_To_Date),
                         new SqlParameter("@Task", this.Task)
                   };
                    break;
                case eLoadSp.SELECT_NMR:
                    colParams = new SqlParameter[]
                   {
                        new SqlParameter("@Project_Code", this.Project_Code),
                        new SqlParameter("@Task", this.Task)
                   };
                    break;
                case eLoadSp.GET_SC_BILL_DETAIL:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@RA_Bill_No", this.RA_Bill_No),
                        new SqlParameter("@Task", this.Task)
                    };
                    break;
                case eLoadSp.DELETE_WORK_DESCRIPTION:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Project_Code", this.Project_Code),
                        new SqlParameter("@Task", this.Task)
                    };
                    break;

                case eLoadSp.SUB_CONTRACTOR_BILL_LABOUR_LIST:
                    colParams = new SqlParameter[]
                    {
                         new SqlParameter("@BillingFrom_Date", this.BillingFrom_Date),
                        new SqlParameter("@Billing_To_Date", this.Billing_To_Date),
                         new SqlParameter("@SubContractorID", this.SubContractorID),
                        new SqlParameter("@Project_Code", this.Project_Code),
                         new SqlParameter("@WONo ", this.WONo),
                        new SqlParameter("@Task", this.Task)
                    };
                    break;
                case eLoadSp.SELECT_PAYMENT_INDENT:
                    colParams = new SqlParameter[]
                    {
                         new SqlParameter("@BillingFrom_Date", this.BillingFrom_Date),
                        new SqlParameter("@Billing_To_Date", this.Billing_To_Date),
                         new SqlParameter("@SubContractorID", this.SubContractorID),
                        new SqlParameter("@Project_Code", this.Project_Code),
                         new SqlParameter("@WONo ", this.WONo),
                        new SqlParameter("@Task", this.Task)
                    };
                    break;
                case eLoadSp.SELECT_DISCRIPTIONOFWORK:
                    colParams = new SqlParameter[]
                    {
                         new SqlParameter("@WONo", this.WONo),
                        new SqlParameter("@Project_Code", this.Project_Code),
                        new SqlParameter("@Task", this.Task),
                         new SqlParameter("@ID", this.ID),
                    };
                    break;
                case eLoadSp.SELECT_DPR_DETAILS:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@BillingFrom_Date", this.BillingFrom_Date),
                        new SqlParameter("@Billing_To_Date", this.Billing_To_Date),
                         new SqlParameter("@WONo", this.WONo)
                       
                    };
                    break;
                case eLoadSp.SELECT_DPR_DETAILS_SC_PRINT:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@BillingFrom_Date", this.BillingFrom_Date),
                        new SqlParameter("@Billing_To_Date", this.Billing_To_Date),
                            new SqlParameter("@Task", this.Task),
                         new SqlParameter("@WONo", this.WONo),
                       
                    };
                    break;
                case eLoadSp.SELECT_WO_NUMBER:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Project_Code", this.Project_Code),
                        new SqlParameter("@SubContractorID", this.SubContractorID),
                         new SqlParameter("@WONo", this.WONo),
                        new SqlParameter("@Task", this.Task)
                    };
                    break;

                case eLoadSp.SELECT_DPRREPORT_BY_DPR_No:
                    colParams = new SqlParameter[]
                     {
                         new SqlParameter("@ID", this.ID),
                        new SqlParameter("@Task", this.Task)
                    };
                    break;
                case eLoadSp.SUB_CONTRACTOR_BILL_OPERATIONS:
                    colParams = new SqlParameter[]
                    {
                        new SqlParameter("@Project_Code", this.Project_Code),
                        new SqlParameter("@DPR_No", this.DPR_No),
                         new SqlParameter("@NMR_No", this.NMR_No),
                          new SqlParameter("@RateCard_ID", this.RateCard_ID),
                         new SqlParameter("@ID", this.ID),
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
        public bool insert_Sub_Contractor_Bill(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert_Sub_Contractor_Bill(SqlConn, null, enumSpName);
        }
        public bool Update_Sub_Contractor_Bill(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Update_Sub_Contractor_Bill(SqlConn, null, enumSpName);
        }
        public bool insert_Sub_Contractor_Bill_Tax(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert_Sub_Contractor_Bill_Tax(SqlConn, null, enumSpName);
        }
        public bool insert_Sub_Contractor_Bill_Retention(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert_Sub_Contractor_Bill_Retention(SqlConn, null, enumSpName);
        }
        public bool Update_MIN_Rate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Update_MIN_Rate(SqlConn, null, enumSpName);
        }
        private bool insert_Sub_Contractor_Bill_Retention(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@RetentionType", SqlDbType.VarChar,1000, ParameterDirection.Input, false, 0, 0, "RetentionType", DataRowVersion.Current, this.RetentionType),
                new SqlParameter("@RetentionAmount", SqlDbType.Decimal,1000, ParameterDirection.Input, false, 0, 0, "RetentionAmount", DataRowVersion.Current, this.RetentionAmount),
                  new SqlParameter("@RetentionPerc", SqlDbType.Decimal,1000, ParameterDirection.Input, false, 0, 0, "RetentionPerc", DataRowVersion.Current, this.RetentionPerc),
                new SqlParameter("@SC_Bill_ID", SqlDbType.VarChar,1000, ParameterDirection.Input, false, 0, 0, "SC_Bill_ID", DataRowVersion.Current, this.SC_Bill_ID),
                new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task)

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
        private bool Update_MIN_Rate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                
                new SqlParameter("@Rate", SqlDbType.Decimal,1000, ParameterDirection.Input, false, 0, 0, "Rate", DataRowVersion.Current, this.MIN_Item_Rate),
                new SqlParameter("@MIN_Item_ID", SqlDbType.VarChar,1000, ParameterDirection.Input, false, 0, 0, "MIN_Item_ID", DataRowVersion.Current, this.MIN_Item_ID),

          
                new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task)

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
        private bool insert_Sub_Contractor_Bill_Tax(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                
                new SqlParameter("@IGST", SqlDbType.Decimal,1000, ParameterDirection.Input, false, 0, 0, "IGST", DataRowVersion.Current, this.IGST),
                new SqlParameter("@CGST", SqlDbType.Decimal,1000, ParameterDirection.Input, false, 0, 0, "CGST", DataRowVersion.Current, this.CGST),
                new SqlParameter("@SGST", SqlDbType.Decimal,1000, ParameterDirection.Input, false, 0, 0, "SGST", DataRowVersion.Current, this.SGST),
                new SqlParameter("@TDS", SqlDbType.Decimal,1000, ParameterDirection.Input, false, 0, 0, "TDS", DataRowVersion.Current, this.TDS),
                new SqlParameter("@SC_Bill_ID", SqlDbType.Decimal,1000, ParameterDirection.Input, false, 0, 0, "SC_Bill_ID", DataRowVersion.Current, this.SC_Bill_ID),
                new SqlParameter("@Task", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task)

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

        private bool Update_Sub_Contractor_Bill(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                 new SqlParameter("@SC_Bill_ID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "SC_Bill_ID", DataRowVersion.Current, this.SC_Bill_ID),
                new SqlParameter("@RA_Bill_No", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "RA_Bill_No", DataRowVersion.Current, this.RA_Bill_No),
                new SqlParameter("@SC_Bill_Date", SqlDbType.DateTime, 50, ParameterDirection.Input, false, 0, 0, "SC_Bill_Date", DataRowVersion.Current, this.SC_Bill_Date),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@SubContractorID", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "SubContractorID", DataRowVersion.Current, this.SubContractorID),
                new SqlParameter("@SC_WONo", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "SC_WONo", DataRowVersion.Current, this.SC_WONo),
                new SqlParameter("@SC_BillingFrom_Date", SqlDbType.DateTime,50, ParameterDirection.Input, false, 0, 0, "SC_BillingFrom_Date", DataRowVersion.Current, this.SC_BillingFrom_Date),
                new SqlParameter("@SC_Billing_To_Date", SqlDbType.DateTime,50, ParameterDirection.Input, false, 0, 0, "SC_Billing_To_Date", DataRowVersion.Current, this.SC_Billing_To_Date),
                new SqlParameter("@Financial_Year", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Financial_Year", DataRowVersion.Current, this.Financial_Year),
                new SqlParameter("@SC_Work_Description", SqlDbType.VarChar,5000, ParameterDirection.Input, false, 0, 0, "SC_Work_Description", DataRowVersion.Current, this.SC_Work_Description),
                  new SqlParameter("@RetentionAmount", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "RetentionAmount", DataRowVersion.Current, this.RetentionAmount),
                  new SqlParameter("@SGST", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "SGST", DataRowVersion.Current, this.SGST),
                  new SqlParameter("@IGST", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "IGST", DataRowVersion.Current, this.IGST),
                    new SqlParameter("@CGST", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "CGST", DataRowVersion.Current, this.CGST),
                     new SqlParameter("@DPR", SqlDbType.Bit,100, ParameterDirection.Input, false, 0, 0, "@DPR", DataRowVersion.Current, this.DPR),
                      new SqlParameter("@NMR", SqlDbType.Bit,100, ParameterDirection.Input, false, 0, 0, "@NMR", DataRowVersion.Current, this.NMR),  
                    new SqlParameter("@RetentionType", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "@RetentionType", DataRowVersion.Current, this.Retention),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task)

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    this.RA_Bill_No = (string)colParams.First().Value;
                   
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
        private bool insert_Sub_Contractor_Bill(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                 //new SqlParameter("@SC_Bill_ID", SqlDbType.VarChar, 50, ParameterDirection.Output, false, 0, 0, "SC_Bill_ID", DataRowVersion.Current, this.SC_Bill_ID),
                new SqlParameter("@RA_Bill_No", SqlDbType.VarChar, 50, ParameterDirection.Output, false, 0, 0, "RA_Bill_No", DataRowVersion.Current, this.RA_Bill_No),
                new SqlParameter("@SC_Bill_Date", SqlDbType.DateTime, 50, ParameterDirection.Input, false, 0, 0, "SC_Bill_Date", DataRowVersion.Current, this.SC_Bill_Date),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@SubContractorID", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "SubContractorID", DataRowVersion.Current, this.SubContractorID),
                new SqlParameter("@SC_WONo", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "SC_WONo", DataRowVersion.Current, this.SC_WONo),
                new SqlParameter("@SC_BillingFrom_Date", SqlDbType.DateTime,50, ParameterDirection.Input, false, 0, 0, "SC_BillingFrom_Date", DataRowVersion.Current, this.SC_BillingFrom_Date),
                new SqlParameter("@SC_Billing_To_Date", SqlDbType.DateTime,50, ParameterDirection.Input, false, 0, 0, "SC_Billing_To_Date", DataRowVersion.Current, this.SC_Billing_To_Date),
                new SqlParameter("@Financial_Year", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Financial_Year", DataRowVersion.Current, this.Financial_Year),
                new SqlParameter("@SC_Work_Description", SqlDbType.VarChar,5000, ParameterDirection.Input, false, 0, 0, "SC_Work_Description", DataRowVersion.Current, this.SC_Work_Description),
                  new SqlParameter("@RetentionAmount", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "RetentionAmount", DataRowVersion.Current, this.RetentionAmount),
                  new SqlParameter("@SGST", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "SGST", DataRowVersion.Current, this.SGST),
                  new SqlParameter("@IGST", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "IGST", DataRowVersion.Current, this.IGST),
                    new SqlParameter("@CGST", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "CGST", DataRowVersion.Current, this.CGST),
                      new SqlParameter("@RetentionType", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "@RetentionType", DataRowVersion.Current, this.Retention),
                      new SqlParameter("@DPR", SqlDbType.Bit,100, ParameterDirection.Input, false, 0, 0, "@DPR", DataRowVersion.Current, this.DPR),
                      new SqlParameter("@NMR", SqlDbType.Bit,100, ParameterDirection.Input, false, 0, 0, "@NMR", DataRowVersion.Current, this.NMR),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task)

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    this.RA_Bill_No = (string)colParams.First().Value;
                   
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
       public bool insertNMR(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertNMR(SqlConn, null, enumSpName);
        }
       public bool InsertLabour_Type(SqlConnection SqlConn, eLoadSp enumSpName)
       {
           return InsertLabour_Type(SqlConn, null, enumSpName);
       }
       public bool InsertTB_NMR_LABOUR(SqlConnection SqlConn, eLoadSp enumSpName)
       {
           return InsertTB_NMR_LABOUR(SqlConn, null, enumSpName);
       }
       private bool InsertTB_NMR_LABOUR(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
       {
           try
           {
               int intIdentityValue = 0;

               SqlParameter[] colParams = new SqlParameter[]
            {
                  new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@WO_NMR", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "WO_NMR", DataRowVersion.Current, this.WO_NMR),
                  new SqlParameter("@NMR_No", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "NMR_No", DataRowVersion.Current, this.NMR_No),
                    new SqlParameter("@Labour_Date", SqlDbType.Date, 1000, ParameterDirection.Input, false, 0, 0, "Labour_Date", DataRowVersion.Current, this.Labour_Date),
                      new SqlParameter("@NoOf_Labour", SqlDbType.Decimal, 1000, ParameterDirection.Input, false, 0, 0, "NoOf_Labour", DataRowVersion.Current, this.NoOf_Labour),
                        new SqlParameter("@Labour_Rate", SqlDbType.Decimal, 1000, ParameterDirection.Input, false, 0, 0, "Labour_Rate", DataRowVersion.Current, this.Labour_Rate),
                          new SqlParameter("@Labour_Type_Labour", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Labour_Type_Labour", DataRowVersion.Current, this.Labour_Type_Labour),
                            new SqlParameter("@LabourCost_Total", SqlDbType.Decimal, 1000, ParameterDirection.Input, false, 0, 0, "LabourCost_Total", DataRowVersion.Current, this.LabourCost_Total),
                      new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
               
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
       private bool InsertLabour_Type(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
       {
           try
           {
               int intIdentityValue = 0;

               SqlParameter[] colParams = new SqlParameter[]
            {
                  new SqlParameter("@Labour_Type", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Labour_Type", DataRowVersion.Current, this.Labour_Type),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                      new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
               
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
       private bool insertNMR(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
       {
           try
           {
               int intIdentityValue = 0;

               SqlParameter[] colParams = new SqlParameter[]
            {
                  new SqlParameter("@NMR_No", SqlDbType.VarChar, 50, ParameterDirection.Output, false, 0, 0, "NMR_No", DataRowVersion.Current, this.NMR_No),
                new SqlParameter("@BillDate_NMR", SqlDbType.Date, 50, ParameterDirection.Input, false, 0, 0, "BillDate_NMR", DataRowVersion.Current, this.BillDate_NMR),
                new SqlParameter("@Project_NMR", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_NMR", DataRowVersion.Current, this.Project_NMR),
                new SqlParameter("@SubContractor_NMR", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "SubContractor_NMR", DataRowVersion.Current, this.SubContractor_NMR),
                new SqlParameter("@WO_NMR", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "WO_NMR", DataRowVersion.Current, this.WO_NMR),
                new SqlParameter("@WorkDescription_NMR", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "WorkDescription_NMR", DataRowVersion.Current, this.WorkDescription_NMR),
                //new SqlParameter("@NoOf_Men", SqlDbType.Decimal, 8000, ParameterDirection.Input, false, 0, 0, "NoOf_Men", DataRowVersion.Current, this.NoOf_Men),
                //new SqlParameter("@Men_Rate", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "Men_Rate", DataRowVersion.Current, this.Men_Rate),
                // new SqlParameter("@Men_Total", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "Men_Total", DataRowVersion.Current, this.Men_Total),
                //  new SqlParameter("@NoOf_Women", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "NoOf_Women", DataRowVersion.Current, this.NoOf_Women),
                //   new SqlParameter("@Women_Rate", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "Women_Rate", DataRowVersion.Current, this.Women_Rate),
                //    new SqlParameter("@Women_Total", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "Women_Total", DataRowVersion.Current, this.Women_Total),
                //     new SqlParameter("@NoOf_Helpers", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "NoOf_Helpers", DataRowVersion.Current, this.NoOf_Helpers),
                //      new SqlParameter("@Helpers_Rate", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "Helpers_Rate", DataRowVersion.Current, this.Helpers_Rate),
                //      new SqlParameter("@Helpers_Total", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "Helpers_Total", DataRowVersion.Current, this.Helpers_Total),
                      new SqlParameter("@WorkDone_At", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "WorkDone_At", DataRowVersion.Current, this.WorkDone_At),
                         new SqlParameter("@TotalCost", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "TotalCost", DataRowVersion.Current, this.TotalCost),
                      new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
               
            };

               if (SqlConn != null)
               {
                   if (!validate())
                   {
                       return false;
                   }

                   intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                   this.NMR_No = (string)colParams.First().Value;
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
       public bool DPR_UPDATE(SqlConnection SqlConn, eLoadSp enumSpName)
       {
           return DPR_UPDATE(SqlConn, null, enumSpName);
       }
        public bool insert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert(SqlConn, null, enumSpName);
        }
        public bool insertRC(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertRC(SqlConn, null, enumSpName);
        }
        public bool insert_Discription_Of_Work(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert_Discription_Of_Work(SqlConn, null, enumSpName);
        }
        private bool insert_Discription_Of_Work(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                
                new SqlParameter("@Work_Description", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "Work_Description", DataRowVersion.Current, this.Work_Description),
                new SqlParameter("@Project_Code", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code )
                

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    //this.DPR_No = (string)colParams.First().Value;
                    //this.DPR_ID = (int)colParams[1].Value;
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
        public bool insert_RC_Entry(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert_RC_Entry(SqlConn, null, enumSpName);
        }
        public bool insert_DPR_Entry(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert_DPR_Entry(SqlConn, null, enumSpName);
        }
        private bool insert_DPR_Entry(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

                new SqlParameter("@DPR_No", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "DPR_No", DataRowVersion.Current, this.DPR_No),
                new SqlParameter("@Location_Chainage", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Location_Chainage", DataRowVersion.Current, this.Location_Chainage),
                new SqlParameter("@Work_Done_Activity", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Work_Done_Activity", DataRowVersion.Current, this.Work_Done_Activity),
                new SqlParameter("@Work_Description", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "Work_Description", DataRowVersion.Current, this.Work_Description),
                new SqlParameter("@Present_Progress", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "Present_Progress", DataRowVersion.Current, this.Present_Progress ),
                 new SqlParameter("@Cumulative_Progress", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "Cumulative_Progress", DataRowVersion.Current, this.Cumulative_Progress ),
                new SqlParameter("@UOM", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.UOM  ),
                new SqlParameter("@Date", SqlDbType.Date,100, ParameterDirection.Input, false, 0, 0, "Date", DataRowVersion.Current, this.Date),
                new SqlParameter("@Remarks", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Remarks", DataRowVersion.Current, this.Remarks),
                new SqlParameter("@DPRFile_Path", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "DPRFile_Path", DataRowVersion.Current, this.DPRFile_Path ),
                 new SqlParameter("@ID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "ID", DataRowVersion.Current, this.ID),
                  new SqlParameter("@No_Men", SqlDbType.Decimal, 50, ParameterDirection.Input, false, 0, 0, "No_Men", DataRowVersion.Current, this.No_Men),
                   new SqlParameter("@No_Women", SqlDbType.Decimal, 50, ParameterDirection.Input, false, 0, 0, "No_Women", DataRowVersion.Current, this.No_Women),
                    new SqlParameter("@No_Heplers", SqlDbType.Decimal, 50, ParameterDirection.Input, false, 0, 0, "No_Heplers", DataRowVersion.Current, this.No_Heplers),
                    new SqlParameter("@NMR_ID", SqlDbType.Decimal, 50, ParameterDirection.Input, false, 0, 0, "NMR_ID", DataRowVersion.Current, this.NMR_ID),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    //this.DPR_No = (string)colParams.First().Value;
                    //this.DPR_ID = (int)colParams[1].Value;
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
        private bool insert_RC_Entry(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

                new SqlParameter("@RateCard_ID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "RateCard_ID", DataRowVersion.Current, this.RateCard_ID),
                new SqlParameter("@Discription_Of_Work", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "Discription_Of_Work", DataRowVersion.Current, this.Discription_Of_WorkRCItem),
                new SqlParameter("@Quantity", SqlDbType.Decimal, 250, ParameterDirection.Input, false, 10, 0, "Quantity", DataRowVersion.Current, this.QuantityRCItem),
                new SqlParameter("@UOM", SqlDbType.Int, 100, ParameterDirection.Input, false, 0, 0, "UOM", DataRowVersion.Current, this.UOMRCItem),
                new SqlParameter("@Rate", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "Rate", DataRowVersion.Current, this.RateRCItem ),
                 new SqlParameter("@Amount", SqlDbType.Decimal,100, ParameterDirection.Input, false, 0, 0, "Amount", DataRowVersion.Current, this.AmountRCItem ),
                 new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    //this.DPR_No = (string)colParams.First().Value;
                    //this.DPR_ID = (int)colParams[1].Value;
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
        private bool DPR_UPDATE(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                  //new SqlParameter("@DPR_ID", SqlDbType.VarChar, 50, ParameterDirection.Output, false, 0, 0, "DPR_ID", DataRowVersion.Current, this.DPR_ID),
                new SqlParameter("@DPR_No", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "DPR_No", DataRowVersion.Current, this.DPR_No),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@SubContractorID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "SubContractorID", DataRowVersion.Current, this.SubContractorID),
                new SqlParameter("@WONo", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "WONo", DataRowVersion.Current, this.WONo),
                new SqlParameter("@Work_Description", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "Work_Description", DataRowVersion.Current, this.Work_Description),
                new SqlParameter("@Financial_Year", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Financial_Year", DataRowVersion.Current, this.Financial_Year),
                 new SqlParameter("@Location_ID", SqlDbType.Int,100, ParameterDirection.Input, false, 0, 0, "Location_ID", DataRowVersion.Current, this.Location_ID),
                      new SqlParameter("@Work_Name_ID", SqlDbType.Int,100, ParameterDirection.Input, false, 0, 0, "Work_Name_ID", DataRowVersion.Current, this.Work_Name_ID),
                         new SqlParameter("@SubWork_ID", SqlDbType.Int,100, ParameterDirection.Input, false, 0, 0, "SubWork_ID", DataRowVersion.Current, this.SubWork_ID),
                new SqlParameter("@Task", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
               
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    this.DPR_ID = (string)colParams.First().Value;
                    this.DPR_No =(string)colParams[1].Value; 
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
        private bool insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                  new SqlParameter("@DPR_ID", SqlDbType.VarChar, 50, ParameterDirection.Output, false, 0, 0, "DPR_ID", DataRowVersion.Current, this.DPR_ID),
                new SqlParameter("@DPR_No", SqlDbType.VarChar, 50, ParameterDirection.Output, false, 0, 0, "DPR_No", DataRowVersion.Current, this.DPR_No),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@SubContractorID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "SubContractorID", DataRowVersion.Current, this.SubContractorID),
                new SqlParameter("@WONo", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "WONo", DataRowVersion.Current, this.WONo),
                new SqlParameter("@Work_Description", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "Work_Description", DataRowVersion.Current, this.Work_Description),
                new SqlParameter("@Financial_Year", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Financial_Year", DataRowVersion.Current, this.Financial_Year),
                   new SqlParameter("@Location_ID", SqlDbType.Int,100, ParameterDirection.Input, false, 0, 0, "Location_ID", DataRowVersion.Current, this.Location_ID),
                      new SqlParameter("@Work_Name_ID", SqlDbType.Int,100, ParameterDirection.Input, false, 0, 0, "Work_Name_ID", DataRowVersion.Current, this.Work_Name_ID),
                         new SqlParameter("@SubWork_ID", SqlDbType.Int,100, ParameterDirection.Input, false, 0, 0, "SubWork_ID", DataRowVersion.Current, this.SubWork_ID),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
               
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    this.DPR_ID = (string)colParams.First().Value;
                    this.DPR_No =(string)colParams[1].Value; 
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
        private bool insertRC(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                  new SqlParameter("@RateCard_ID", SqlDbType.VarChar, 500, ParameterDirection.Output, false, 0, 0, "RateCard_ID", DataRowVersion.Current, this.RateCard_ID),
                new SqlParameter("@Date", SqlDbType.Date, 50, ParameterDirection.Input, false, 0, 0, "Date", DataRowVersion.Current, this.DateRC),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@SubContractorID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "SubContractorID", DataRowVersion.Current, this.SubContractorID),
                new SqlParameter("@WO_No", SqlDbType.VarChar, 250, ParameterDirection.Input, false, 10, 0, "WO_No", DataRowVersion.Current, this.WONo),
                new SqlParameter("@WO_Description", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "WO_Description", DataRowVersion.Current, this.Work_Description),
                new SqlParameter("@FixedOrRecurring", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "FixedOrRecurring", DataRowVersion.Current, this.FixedOrRecurring),
                new SqlParameter("@WO_Type", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "WO_Type", DataRowVersion.Current, this.WO_Type_RC),
                new SqlParameter("@Financial_Year", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Financial_Year", DataRowVersion.Current, this.Financial_Year),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
               
            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));
                    this.RateCard_ID = (string)colParams.First().Value;
                    
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
