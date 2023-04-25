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
    public class PaymentIndentBL
    {
        #region "Class: PaymentIndentBL Local Declarations"

        public enum eLoadSp
        {
            INSERT = 0,
            UPDATE = 1,
            SELECT_POWO = 3, // can be removed
            SELECT_PAYMENTINDENTDETAILS = 4, // can be removed
            FILE_INSERT = 5,
            FILE_COUNT = 10,
            SELECT_PAYMENT_INDENT_ALL = 6,
            DELETE_PAYMENT_INDENT_BY_PAYINDNO = 7,
            SELECT_PAYMENTINDENT_FILE = 8,
            DELETE_FILE_BY_FILENAME = 9,
            ITEM_INSERT_UPDATE = 11,
            DELETE_ITEM_BY_ID = 12,
            UPDATE_STATUS = 13,
            SELECT_ALL_LEDGER = 14,
            DELETE_LEDGER = 15,
            INSERT_LEDGER = 16,
            SELECT_Bank_ALL = 17,
            GET_ORDER_TYPE = 18,
            GET_EARLIER_PAYMENT = 19, // can be removed
            UPDATE_RETURNED_STATUS = 20,
            INSERT_PAYMENTCATEGORY = 21,
            SELECT_ALL_PAYMENTCATEGORY = 22,
            DELETE_PAYMENTCATEGORY = 23,
            SELECT_BANK = 24,
            UPDATE_PARTPAYMENT_STATUS = 25,
            SUBCONTRACTOR_DETAILS_SELECT_ALL = 26, // can be removed
            SUBCONTRACTOR_DETAILS_SELECT_ALL_BY_ID = 27, // can be removed
            CHECK_INDENT_SUPPLIED_PERIOD = 28,
            SELECT_BANKINDENT_ALL_BY_BNKIND_ID = 29,
            SELECT_VENDOR_CONTRACTOR_OTHER_ALL = 30,
            SELECT_PAYMENT_INDENT_WITH_FILTER = 31,

            //Getting data from all DB
            GET_STATE = 32,
            GET_PROJECT_BY_STATE = 33,
            GET_VENDOR_BY_STATE = 34,
            GET_SUBCON_BY_STATE = 35,
            GET_OTHER_BY_STATE = 36,
            SELECT_PROJECT_DETAILS_BY_ID = 37,
            GET_BENEFICIARY_DETAIL = 38,
            GET_POWO_DETAILS = 39,
            SELECT_PAYMENT_INDENT_DETAILS = 40,
            POWO_PRINT = 41,
            GET_PAYMENT_INDENT_ALL = 42,
            SELECT_PAYIND_DETAILS_BY_PAYINDNO = 43,
        };

        #endregion

        #region "Class: PaymentIndentBL Sets / Gets"

        public string Project_Code { get; set; }
        public string Vendor_ID { get; set; }
        public int Vendor_SubCon_ID { get; set; }

        public string SubCon_ID { get; set; }
        public string Other_ID { get; set; }
        public int POWO_ID { get; set; }
        public string POWO_Number { get; set; }
        public string WO_Type { get; set; }

        public DateTime? PayIndDate { get; set; }
        public string FYear { get; set; }
        public int Approver_ID { get; set; }
        public int Verifier_ID { get; set; }
        public string BeneficiaryType { get; set; }
        public string Remarks { get; set; }

        public int No_Of_Due_Date { get; set; }
        public DateTime? Invoice_Date { get; set; }

        public string Bank_Name { get; set; }
        public string Bank_Branch { get; set; }
        public string Bank_Account { get; set; }
        public string Bank_IFSC { get; set; }
        public string Payment_Type { get; set; }
        public string Payment_Mode { get; set; }
        //public string State { get; set; }
        //public string WorkDesc { get; set; }
        //public string AwardedBy { get; set; }
        //public string PrincipalContractor { get; set; }
        public decimal? Amt_ServiceMaterial { get; set; }
        public decimal? Amt_EarlierPayment { get; set; }
        public decimal? Amt_PartPayment { get; set; }
        public decimal? Amt_Approved { get; set; }
        public DateTime? Payment_Approved_Date { get; set; }

        public string Status { get; set; }

        public string ChRA_bill { get; set; }
        public DateTime? Service_Items_Date_From { get; set; }
        public DateTime? Service_Items_Date_TO { get; set; }
        public string Payment_To_SubContractorName { get; set; }
        public string Payment_Category { get; set; }
        public int User_ID { get; set; }
        public string Task { get; set; }
        public string BANK_INDENT_No { get; set; }
        public string PayInd_No { get; set; }

        public string PayInd_No_Partial { get; set; }
        public int? PayInd_ID { get; set; }
        public string Priority_Satus { get; set; }

        public decimal? GST_Percent { get; set; }
        public decimal? GST_Amount { get; set; }
        public decimal? Other_Deductions { get; set; }
        public decimal? Total_Payable { get; set; }

        public decimal? TDS_Perc { get; set; }
        public decimal? TDS_Amt { get; set; }
        public decimal? OtherDeduction { get; set; }
        public decimal? Amt_Transferable { get; set; }

        public DateTime? PaymentDate { get; set; }
        public string PaymentRefNo { get; set; }
        public string Narration { get; set; }
        public bool Payment_Approval_Status { get; set; }
        public bool BalanceGrid { get; set; }
        public string File_Bill { get; set; }
        public string File_SiteImg { get; set; }
        public string File_Type { get; set; }
        public string File_Type_Like { get; set; }
        public string File_Name { get; set; }

        public int Item_ID { get; set; }
        public string Item_Category { get; set; }
        public string Item_Name { get; set; }
        public decimal? Item_Amount { get; set; }

        public string PaymentCategory_Name { get; set; }
        public int PaymentCategory_ID { get; set; }
        public int Ledger_ID { get; set; }
        public string Ledger_Name { get; set; }
        public string WorkDoneFor { get; set; }

        public string Service_Item_Supplied_Month { get; set; }
        public string Project { get; set; }

        public string StateCode { get; set; }

        #endregion

        #region "Class: PaymentIndentBL Methods"

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
                        this.PayInd_No = dr["Indent_No"].ToString();
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

        private string getSpName(eLoadSp enumSpName)
        {
            switch (enumSpName)
            {
                case eLoadSp.INSERT:
                case eLoadSp.UPDATE:
                case eLoadSp.FILE_INSERT:
                case eLoadSp.DELETE_PAYMENT_INDENT_BY_PAYINDNO:
                case eLoadSp.DELETE_FILE_BY_FILENAME:
                case eLoadSp.ITEM_INSERT_UPDATE:
                case eLoadSp.DELETE_ITEM_BY_ID:
                case eLoadSp.UPDATE_STATUS:
                case eLoadSp.INSERT_LEDGER:
                case eLoadSp.DELETE_LEDGER:
                case eLoadSp.DELETE_PAYMENTCATEGORY:
                case eLoadSp.INSERT_PAYMENTCATEGORY:
                case eLoadSp.UPDATE_RETURNED_STATUS:
                case eLoadSp.SELECT_BANK:
                case eLoadSp.UPDATE_PARTPAYMENT_STATUS:
                    return "PROC_PAYMENT_INDENT_INSERT_UPDATE";
                case eLoadSp.SELECT_POWO:
                case eLoadSp.SELECT_PAYMENTINDENTDETAILS:
                case eLoadSp.SELECT_PAYMENT_INDENT_ALL:
                case eLoadSp.SELECT_PAYMENTINDENT_FILE:
                case eLoadSp.SELECT_Bank_ALL:
                case eLoadSp.FILE_COUNT:
                case eLoadSp.SELECT_ALL_LEDGER:
                case eLoadSp.GET_ORDER_TYPE:
                case eLoadSp.SELECT_ALL_PAYMENTCATEGORY:
                case eLoadSp.SELECT_VENDOR_CONTRACTOR_OTHER_ALL:
                case eLoadSp.SELECT_PAYMENT_INDENT_WITH_FILTER:
                    return "PROC_PAYMENT_INDENT";
                case eLoadSp.GET_EARLIER_PAYMENT:
                    return "PROC_GET_EARLIER_PAYMENT";
                case eLoadSp.SUBCONTRACTOR_DETAILS_SELECT_ALL:
                    return "PRO_SUBCONTRACTOR_DETAILS_SELECT_ALL";
                case eLoadSp.SUBCONTRACTOR_DETAILS_SELECT_ALL_BY_ID:
                    return "PRO_SUBCONTRACTOR_DETAILS_SELECT_ALL_BY_ID";
                case eLoadSp.CHECK_INDENT_SUPPLIED_PERIOD:
                    return "PRO_CHECK_INDENT_SUPPLIED_PERIOD";
                case eLoadSp.SELECT_BANKINDENT_ALL_BY_BNKIND_ID:
                    return "PRO_SELECT_BANKINDENT_ALL_BY_BNKIND_ID";
                case eLoadSp.SELECT_PAYIND_DETAILS_BY_PAYINDNO:
                    return "PRO_SELECT_PAYIND_DETAILS_BY_PAYINDNO";


                case eLoadSp.GET_STATE:
                case eLoadSp.GET_PROJECT_BY_STATE:
                case eLoadSp.GET_VENDOR_BY_STATE:
                case eLoadSp.GET_SUBCON_BY_STATE:
                case eLoadSp.GET_OTHER_BY_STATE:
                case eLoadSp.SELECT_PROJECT_DETAILS_BY_ID:
                case eLoadSp.GET_BENEFICIARY_DETAIL:
                case eLoadSp.GET_POWO_DETAILS:
                case eLoadSp.SELECT_PAYMENT_INDENT_DETAILS:
                case eLoadSp.GET_PAYMENT_INDENT_ALL:
                    return "PROC_PAYMENT_INDENT_ALL_DB";
                case eLoadSp.POWO_PRINT:
                    return "PROC_POWO_PRINT_ALL_DB";
                default:
                    return string.Empty;
            }
        }


        private SqlParameter[] getSpParamArray(eLoadSp enumSpName)
        {
            SqlParameter[] colParams = new SqlParameter[] { };


            switch (enumSpName)
            {
                case eLoadSp.SELECT_BANK:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),

                };
                    break;

                case eLoadSp.UPDATE_PARTPAYMENT_STATUS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@PayInd_No", this.PayInd_No_Partial)
                };
                    break;
                case eLoadSp.SELECT_POWO:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@Vendor_ID", this.Vendor_ID),
                    new SqlParameter("@Subcon_ID", this.SubCon_ID),
                    new SqlParameter("@Other_ID", this.Other_ID),
                    new SqlParameter("@POWO_ID", this.POWO_ID),
                    new SqlParameter("@WO_Type", this.WO_Type)
                };
                    break;
                case eLoadSp.GET_ORDER_TYPE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@POWO_ID", this.POWO_ID)
                };
                    break;
                case eLoadSp.GET_EARLIER_PAYMENT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@POWO_ID", this.POWO_ID)
                };
                    break;
                case eLoadSp.SELECT_PAYMENTINDENTDETAILS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@PayInd_No",this.PayInd_No)
                };
                    break;

                case eLoadSp.FILE_INSERT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@PayInd_No",this.PayInd_No),
                    new SqlParameter("@File_Bill", this.File_Bill),
                    new SqlParameter("@File_Type", this.File_Type),
                    new SqlParameter("@File_SiteImg", this.File_SiteImg)
                };
                    break;

                case eLoadSp.UPDATE_STATUS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@PayInd_No",this.PayInd_No),
                    new SqlParameter("@Amt_Approved", this.Amt_Approved),
                     new SqlParameter("@Verifier_Remarks", this.Remarks),
                    new SqlParameter("@Status", this.Status)
                };
                    break;

                case eLoadSp.SELECT_PAYMENT_INDENT_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@Project_Code",this.Project_Code),
					new SqlParameter("@PayInd_No",this.PayInd_No),				  
                    new SqlParameter("@UID",this.User_ID)
                };
                    break;

                case eLoadSp.SELECT_PAYMENT_INDENT_WITH_FILTER:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@Project_Code",this.Project_Code),
                    new SqlParameter("@UID",this.User_ID),
                     new SqlParameter("@Vendor_ID", this.Vendor_ID),
                    new SqlParameter("@Subcon_ID", this.SubCon_ID),
                    new SqlParameter("@Other_ID", this.Other_ID)
                };
                    break;

                case eLoadSp.SELECT_Bank_ALL:
                case eLoadSp.SELECT_VENDOR_CONTRACTOR_OTHER_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),

                };
                    break;
                case eLoadSp.DELETE_PAYMENT_INDENT_BY_PAYINDNO:
                case eLoadSp.SELECT_PAYMENTINDENT_FILE:
                case eLoadSp.FILE_COUNT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@File_Type", this.File_Type_Like),
                    new SqlParameter("@PayInd_No",this.PayInd_No),
                };
                    break;

                case eLoadSp.DELETE_FILE_BY_FILENAME:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@PayInd_No",this.PayInd_No),
                    new SqlParameter("@File_Name",this.File_Name)
                };
                    break;

                case eLoadSp.ITEM_INSERT_UPDATE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@PayInd_No",this.PayInd_No),
                    new SqlParameter("@Item_Category",this.Item_Category),
                    new SqlParameter("@Item_Name", this.Item_Name),
                    new SqlParameter("@Item_Amount", this.Item_Amount)
                };
                    break;

                case eLoadSp.DELETE_ITEM_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@PayInd_No",this.PayInd_No),
                    new SqlParameter("@Item_ID",this.Item_ID)
                };
                    break;

                case eLoadSp.SELECT_ALL_LEDGER:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task)

                };
                    break;
                case eLoadSp.SELECT_ALL_PAYMENTCATEGORY:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task)

                };
                    break;
                case eLoadSp.SELECT_BANKINDENT_ALL_BY_BNKIND_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@BANK_INDENT_No", this.BANK_INDENT_No),
                };
                    break;
                case eLoadSp.SUBCONTRACTOR_DETAILS_SELECT_ALL_BY_ID:
                case eLoadSp.SUBCONTRACTOR_DETAILS_SELECT_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Subcon_ID", this.SubCon_ID)

                };
                    break;
                case eLoadSp.INSERT_LEDGER:
                case eLoadSp.DELETE_LEDGER:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@Ledger_Name", this.Ledger_Name),
                    new SqlParameter("@Ledger_ID", this.Ledger_ID),

                };
                    break;
                case eLoadSp.DELETE_PAYMENTCATEGORY:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@PaymentCategory_Name", this.PaymentCategory_Name),
                    new SqlParameter("@PaymentCategory_ID", this.PaymentCategory_ID),

                };
                    break;
                case eLoadSp.SELECT_PAYIND_DETAILS_BY_PAYINDNO:
                    colParams = new SqlParameter[]
                {
                   new SqlParameter("@PayInd_No", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "PayInd_No", DataRowVersion.Current, this.PayInd_No),
																							   
																				

                };
                    break;
                case eLoadSp.CHECK_INDENT_SUPPLIED_PERIOD:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@POWO_ID", this.POWO_ID),
                    new SqlParameter("@Service_Items_Date_From", this.Service_Items_Date_From),
                    new SqlParameter("@Vendor_SubCon_ID", this.Vendor_SubCon_ID)

                };
                    break;

                case eLoadSp.GET_STATE:
                case eLoadSp.GET_PAYMENT_INDENT_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                };
                    break;
                case eLoadSp.GET_PROJECT_BY_STATE:
                case eLoadSp.GET_VENDOR_BY_STATE:
                case eLoadSp.GET_SUBCON_BY_STATE:
                case eLoadSp.GET_OTHER_BY_STATE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@StateCode", this.StateCode),
                };
                    break;
                case eLoadSp.SELECT_PROJECT_DETAILS_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@StateCode", this.StateCode),
                    new SqlParameter("@ProjectCode", this.Project_Code ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.GET_BENEFICIARY_DETAIL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@StateCode", this.StateCode),
                    new SqlParameter("@Vendor_ID", this.Vendor_ID),
                    new SqlParameter("@Subcon_ID", this.SubCon_ID),
                    new SqlParameter("@Other_ID", this.Other_ID),
                };
                    break;
                case eLoadSp.GET_POWO_DETAILS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@StateCode", this.StateCode),
                    new SqlParameter("@POWO_ID", this.POWO_ID.ToString()),
                    new SqlParameter("@POWO_No", this.POWO_Number),
                    new SqlParameter("@WO_Type", this.WO_Type),
                };
                    break;
                case eLoadSp.SELECT_PAYMENT_INDENT_DETAILS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@PayInd_No",this.PayInd_No)
                };
                    break;
                case eLoadSp.POWO_PRINT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@StateCode",this.StateCode),
                    new SqlParameter("@POWO_ID", this.POWO_ID.ToString()),
                    new SqlParameter("@POWO_No", this.POWO_Number)
                };
                    break;
            }

            return colParams;
        }

        public bool insert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insert(SqlConn, null, enumSpName);
        }

        public bool insert(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insert(null, SqlTran, enumSpName);
        }

        private bool insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PayInd_No", SqlDbType.VarChar, 30, ParameterDirection.Output, false, 0, 0, "PayInd_No", DataRowVersion.Current, this.PayInd_No),
                new SqlParameter("@PayInd_ID", SqlDbType.Int,4, ParameterDirection.Output, false, 0, 0, "PayInd_ID", DataRowVersion.Current, this.PayInd_ID),
                new SqlParameter("@PayInd_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "PayIndDate", DataRowVersion.Current, this.PayIndDate),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@FYear", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "FYear", DataRowVersion.Current, this.FYear),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@Created_By", SqlDbType.Int,4, ParameterDirection.Input, false,0, 0, "Created_By", DataRowVersion.Current, this.User_ID),
                new SqlParameter("@BeneficiaryType", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "BeneficiaryType", DataRowVersion.Current, this.BeneficiaryType),
                new SqlParameter("@SubCon_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "SubCon_ID", DataRowVersion.Current, this.SubCon_ID),
                new SqlParameter("@Vendor_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Vendor_ID", DataRowVersion.Current, this.Vendor_ID),
                new SqlParameter("@Other_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Other_ID", DataRowVersion.Current, this.Other_ID),
                new SqlParameter("@POWO_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "POWO_ID", DataRowVersion.Current, this.POWO_ID),
                new SqlParameter("@WO_Type", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "WO_Type", DataRowVersion.Current, this.WO_Type),
                new SqlParameter("@Amt_ServiceMaterial", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Amt_ServiceMaterial", DataRowVersion.Current, this.Amt_ServiceMaterial),
                new SqlParameter("@Amt_EarlierPayment", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Amt_EarlierPayment", DataRowVersion.Current, this.Amt_EarlierPayment),
                new SqlParameter("@Amt_PartPayment", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Amt_PartPayment", DataRowVersion.Current, this.Amt_PartPayment),
                 new SqlParameter("@TDS_Perc", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "TDS_Perc", DataRowVersion.Current, this.TDS_Perc),
                new SqlParameter("@TDS_Amt", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "TDS_Amt", DataRowVersion.Current, this.TDS_Amt),
                new SqlParameter("@Approver", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Approver", DataRowVersion.Current, this.Approver_ID),
                new SqlParameter("@Bank_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Bank_Name", DataRowVersion.Current, this.Bank_Name),
                new SqlParameter("@Bank_Branch", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Bank_Branch", DataRowVersion.Current, this.Bank_Branch),
                new SqlParameter("@Bank_Account", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Bank_Account", DataRowVersion.Current, this.Bank_Account),
                new SqlParameter("@Bank_IFSC", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Bank_IFSC", DataRowVersion.Current, this.Bank_IFSC),
                new SqlParameter("@Payment_Type", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Payment_Type", DataRowVersion.Current, this.Payment_Type),
                new SqlParameter("@Payment_Mode", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Payment_Mode", DataRowVersion.Current, this.Payment_Mode),
                new SqlParameter("@Ledger_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Ledger_Name", DataRowVersion.Current, this.Ledger_Name),
                new SqlParameter("@WorkDoneFor", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "WorkDoneFor", DataRowVersion.Current, this.WorkDoneFor),
                new SqlParameter("@Service_Item_Supplied_Month", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Service_Item_Supplied_Month", DataRowVersion.Current, this.Service_Item_Supplied_Month),
                new SqlParameter("@Project", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project", DataRowVersion.Current, this.Project),
                new SqlParameter("@Payment_Approval_Status", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "Payment_Approval_Status", DataRowVersion.Current, this.Payment_Approval_Status),
                new SqlParameter("@Invoice_Date", SqlDbType.DateTime,3, ParameterDirection.Input, false, 0, 0, "Invoice_Date", DataRowVersion.Current, this.Invoice_Date),
                new SqlParameter("@No_Of_Due_Date", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "No_Of_Due_Date", DataRowVersion.Current, this.No_Of_Due_Date),
                new SqlParameter("@Status", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@ChRA_bill", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChRA_bill", DataRowVersion.Current, this.ChRA_bill),
                new SqlParameter("@Service_Items_Date_From",SqlDbType.DateTime,3, ParameterDirection.Input, false, 0, 0, "Service_Items_Date_From", DataRowVersion.Current, this.Service_Items_Date_From),
                new SqlParameter("@Service_Items_Date_TO", SqlDbType.DateTime,3, ParameterDirection.Input, false, 0, 0, "Service_Items_Date_TO", DataRowVersion.Current, this.Service_Items_Date_TO),
                new SqlParameter("@Verifier", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Verifier", DataRowVersion.Current, this.Verifier_ID),
                new SqlParameter("@Payment_To_SubContractorName", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Payment_To_SubContractorName", DataRowVersion.Current, this.Payment_To_SubContractorName),
                new SqlParameter("@INDBalance_Grid", SqlDbType.Bit, 1, ParameterDirection.Input, false, 0, 0, "INDBalance_Grid", DataRowVersion.Current, this.BalanceGrid),
                new SqlParameter("@Payment_Category", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Payment_Category", DataRowVersion.Current, this.Payment_Category),
                new SqlParameter("@GST_Percent", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "GST_Percent", DataRowVersion.Current, this.GST_Percent),
                new SqlParameter("@GST_Amount", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "GST_Amount", DataRowVersion.Current, this.GST_Amount),
                new SqlParameter("@Other_Deductions", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "Other_Deductions", DataRowVersion.Current, this.Other_Deductions),
                new SqlParameter("@Total_Payable", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "Total_Payable", DataRowVersion.Current, this.Total_Payable),
                new SqlParameter("@StateCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "StateCode", DataRowVersion.Current, this.StateCode)
                //new SqlParameter("@File_Bill", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "File_Bill", DataRowVersion.Current, this.File_Bill),
                //new SqlParameter("@File_SiteImg", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "File_SiteImg", DataRowVersion.Current, this.File_SiteImg)

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));

                    this.PayInd_No = (string)colParams.First().Value;
                    this.PayInd_ID = (int)colParams[1].Value;
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

        public bool insertFile(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertFile(SqlConn, null, enumSpName);
        }

        public bool insertFile(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insertFile(null, SqlTran, enumSpName);
        }

        private bool insertFile(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PayInd_No", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "PayInd_No", DataRowVersion.Current, this.PayInd_No),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@File_Bill", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "File_Bill", DataRowVersion.Current, this.File_Bill),
                 new SqlParameter("@File_Type", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "File_Type", DataRowVersion.Current, this.File_Type),
                new SqlParameter("@File_SiteImg", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "File_SiteImg", DataRowVersion.Current, this.File_SiteImg)

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

        public bool insertItem(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertItem(SqlConn, null, enumSpName);
        }

        public bool insertItem(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insertItem(null, SqlTran, enumSpName);
        }

        private bool insertItem(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PayInd_No", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PayInd_No", DataRowVersion.Current, this.PayInd_No),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@Item_Category", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Item_Category", DataRowVersion.Current, this.Item_Category),
                new SqlParameter("@Item_Name", SqlDbType.VarChar, 200, ParameterDirection.Input, false, 0, 0, "Item_Name", DataRowVersion.Current, this.Item_Name),
                new SqlParameter("@Item_Amount", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Item_Amount", DataRowVersion.Current, this.Item_Amount)

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
        public bool insertPaymentCategory(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertPaymentCategory(SqlConn, null, enumSpName);
        }
        public bool insertLedger(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertLedger(SqlConn, null, enumSpName);
        }

        public bool insertLedger(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return insertLedger(null, SqlTran, enumSpName);
        }

        private bool insertPaymentCategory(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PaymentCategory_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "PaymentCategory_Name", DataRowVersion.Current, this.PaymentCategory_Name),
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
        private bool insertLedger(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Ledger_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Ladger_Name", DataRowVersion.Current, this.Ledger_Name),
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


        public bool updateStatus(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateStatus(SqlConn, null, enumSpName);
        }

        public bool updateStatus(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return updateStatus(null, SqlTran, enumSpName);
        }

        private bool updateStatus(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PayInd_No", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "PayInd_No", DataRowVersion.Current, this.PayInd_No),
                new SqlParameter("@Amt_Approved", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Amt_Approved", DataRowVersion.Current, this.Amt_Approved),
                new SqlParameter("@Status", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Verifier_Remarks", SqlDbType.VarChar, 2000, ParameterDirection.Input, false, 0, 0, "Verifier_Remarks", DataRowVersion.Current, this.Remarks),
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


        public bool update(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return update(SqlConn, null, enumSpName);
        }
        public bool update_Aprovel(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return update_Aprovel(SqlConn, null, enumSpName);
        }
        public bool move_To_Complete(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return move_To_Complete(SqlConn, null, enumSpName);
        }
        private bool move_To_Complete(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PayInd_No", SqlDbType.VarChar, 30, ParameterDirection.InputOutput, false, 0, 0, "PayInd_No", DataRowVersion.Current, this.PayInd_No),
                       new SqlParameter("@Payment_Approval_Status", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "Payment_Approval_Status", DataRowVersion.Current, this.Payment_Approval_Status),
                        new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    this.PayInd_No = (string)colParams.First().Value;
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
        public bool update(SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            return update(null, SqlTran, enumSpName);
        }

        private bool update_Aprovel(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PayInd_No", SqlDbType.VarChar, 30, ParameterDirection.InputOutput, false, 0, 0, "PayInd_No", DataRowVersion.Current, this.PayInd_No),
                 new SqlParameter("@TDS_Perc", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "TDS_Perc", DataRowVersion.Current, this.TDS_Perc),
                  new SqlParameter("@TDS_Amt", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "TDS_Amt", DataRowVersion.Current, this.TDS_Amt),
                    new SqlParameter("@OtherDeduction", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "OtherDeduction", DataRowVersion.Current, this.OtherDeduction),
                    new SqlParameter("@Amt_Transferable", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Amt_Transferable", DataRowVersion.Current, this.Amt_Transferable),
                     new SqlParameter("@PaymentRefNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PaymentRefNo", DataRowVersion.Current, this.PaymentRefNo),
                     new SqlParameter("@Narration", SqlDbType.VarChar, 3000, ParameterDirection.Input, false, 0, 0, "Narration", DataRowVersion.Current, this.Narration),
                     new SqlParameter("@PaymentDate", SqlDbType.DateTime, 3, ParameterDirection.Input, false, 0, 0, "PaymentDate", DataRowVersion.Current, this.PaymentDate),
                       new SqlParameter("@Payment_Approval_Status", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "Payment_Approval_Status", DataRowVersion.Current, this.Payment_Approval_Status),
                        new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    this.PayInd_No = (string)colParams.First().Value;
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

        private bool update(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PayInd_No", SqlDbType.VarChar, 30, ParameterDirection.InputOutput, false, 0, 0, "PayInd_No", DataRowVersion.Current, this.PayInd_No),
                new SqlParameter("@PayInd_ID", SqlDbType.Int,4, ParameterDirection.Output, false, 0, 0, "PayInd_ID", DataRowVersion.Current, this.PayInd_ID),
                new SqlParameter("@PayInd_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "PayIndDate", DataRowVersion.Current, this.PayIndDate),
                new SqlParameter("@Task", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@FYear", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 10, 0, "FYear", DataRowVersion.Current, this.FYear),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),
                new SqlParameter("@Created_By", SqlDbType.Int,4, ParameterDirection.Input, false,0, 0, "Created_By", DataRowVersion.Current, this.User_ID),
                new SqlParameter("@BeneficiaryType", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "BeneficiaryType", DataRowVersion.Current, this.BeneficiaryType),
                new SqlParameter("@SubCon_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "SubCon_ID", DataRowVersion.Current, this.SubCon_ID),
                new SqlParameter("@Vendor_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Vendor_ID", DataRowVersion.Current, this.Vendor_ID),
                new SqlParameter("@Other_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Other_ID", DataRowVersion.Current, this.Other_ID),
                new SqlParameter("@POWO_ID", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "POWO_ID", DataRowVersion.Current, this.POWO_ID),
                new SqlParameter("@Amt_ServiceMaterial", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Amt_ServiceMaterial", DataRowVersion.Current, this.Amt_ServiceMaterial),
                new SqlParameter("@Amt_EarlierPayment", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Amt_EarlierPayment", DataRowVersion.Current, this.Amt_EarlierPayment),
                new SqlParameter("@Amt_PartPayment", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Amt_PartPayment", DataRowVersion.Current, this.Amt_PartPayment),
                new SqlParameter("@Payment_Mode", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Payment_Mode", DataRowVersion.Current, this.Payment_Mode),
                new SqlParameter("@TDS_Perc", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "TDS_Perc", DataRowVersion.Current, this.TDS_Perc),
                new SqlParameter("@TDS_Amt", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "TDS_Amt", DataRowVersion.Current, this.TDS_Amt),
                new SqlParameter("@OtherDeduction", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "OtherDeduction", DataRowVersion.Current, this.OtherDeduction),
                new SqlParameter("@Amt_Transferable", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Amt_Transferable", DataRowVersion.Current, this.Amt_Transferable),
                new SqlParameter("@PaymentDate", SqlDbType.DateTime, 3, ParameterDirection.Input, false, 0, 0, "PaymentDate", DataRowVersion.Current, this.PaymentDate),
                new SqlParameter("@PaymentRefNo", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PaymentRefNo", DataRowVersion.Current, this.PaymentRefNo),
                new SqlParameter("@Narration", SqlDbType.VarChar, 3000, ParameterDirection.Input, false, 0, 0, "Narration", DataRowVersion.Current, this.Narration),
                 new SqlParameter("@Status", SqlDbType.VarChar,100, ParameterDirection.Input, false, 0, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@Approver", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Approver", DataRowVersion.Current, this.Approver_ID),
                new SqlParameter("@Bank_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Bank_Name", DataRowVersion.Current, this.Bank_Name),
                new SqlParameter("@Bank_Branch", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Bank_Branch", DataRowVersion.Current, this.Bank_Branch),
                new SqlParameter("@Bank_Account", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Bank_Account", DataRowVersion.Current, this.Bank_Account),
                new SqlParameter("@Bank_IFSC", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Bank_IFSC", DataRowVersion.Current, this.Bank_IFSC),
                new SqlParameter("@Payment_Type", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Payment_Type", DataRowVersion.Current, this.Payment_Type),

                new SqlParameter("@Ledger_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Ledger_Name", DataRowVersion.Current, this.Ledger_Name),
                new SqlParameter("@WorkDoneFor", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "WorkDoneFor", DataRowVersion.Current, this.WorkDoneFor),
                 new SqlParameter("@Service_Item_Supplied_Month", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Service_Item_Supplied_Month", DataRowVersion.Current, this.Service_Item_Supplied_Month),
                new SqlParameter("@Project", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project", DataRowVersion.Current, this.Project),
                new SqlParameter("@Payment_Approval_Status", SqlDbType.Bit,1, ParameterDirection.Input, false, 0, 0, "Payment_Approval_Status", DataRowVersion.Current, this.Payment_Approval_Status),
                new SqlParameter("@Invoice_Date", SqlDbType.DateTime,3, ParameterDirection.Input, false, 0, 0, "Invoice_Date", DataRowVersion.Current, this.Invoice_Date),
                new SqlParameter("@No_Of_Due_Date", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "No_Of_Due_Date", DataRowVersion.Current, this.No_Of_Due_Date),
                new SqlParameter("@ChRA_bill", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ChRA_bill", DataRowVersion.Current, this.ChRA_bill),
                 new SqlParameter("@Service_Items_Date_From",SqlDbType.DateTime,3, ParameterDirection.Input, false, 0, 0, "Service_Items_Date_From", DataRowVersion.Current, this.Service_Items_Date_From),
                new SqlParameter("@Service_Items_Date_TO", SqlDbType.DateTime,3, ParameterDirection.Input, false, 0, 0, "Service_Items_Date_TO", DataRowVersion.Current, this.Service_Items_Date_TO),
                new SqlParameter("@Verifier", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Verifier", DataRowVersion.Current, this.Verifier_ID),
                new SqlParameter("@Verifier_Remarks", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Verifier_Remarks", DataRowVersion.Current, this.Remarks),
                new SqlParameter("@Payment_To_SubContractorName", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Payment_To_SubContractorName", DataRowVersion.Current, this.Payment_To_SubContractorName),
                 new SqlParameter("@INDBalance_Grid", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "INDBalance_Grid", DataRowVersion.Current, this.BalanceGrid),
                 new SqlParameter("@Payment_Category", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Payment_Category", DataRowVersion.Current, this.Payment_Category),
                 new SqlParameter("@GST_Percent", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "GST_Percent", DataRowVersion.Current, this.GST_Percent),
                    new SqlParameter("@GST_Amount", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "GST_Amount", DataRowVersion.Current, this.GST_Amount),
                     new SqlParameter("@Other_Deductions", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "Other_Deductions", DataRowVersion.Current, this.Other_Deductions),
                      new SqlParameter("@Total_Payable", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "Total_Payable", DataRowVersion.Current, this.Total_Payable),
                        new SqlParameter("@Priority_Satus", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 0, 0, "Priority_Satus", DataRowVersion.Current, this.Priority_Satus),
                           new SqlParameter("@StateCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "StateCode", DataRowVersion.Current, this.StateCode),
            };
                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    this.PayInd_No = (string)colParams.First().Value;
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

        public PaymentIndentBL()
        {
            //Set default values
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
