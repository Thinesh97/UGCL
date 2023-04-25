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
    public class ProjectBL
    {


        #region "Class: ProjectBL Local Declarations"

        public enum eLoadSp
        {
            INSERT = 0,
            SELECT_PROJECTTYPE_ALL = 1,
            UPDATE = 2,
            INSERT_PROJECT = 3,
            SELECT_PROJECT_ALL = 4,
            SELECT_PROJECT_DETAILS_BY_ID = 5,
            INSERT_PROJECT_CONTACT = 6,
            SELECT_DEPARTMENT_ALL = 7,
            SELECT_DESIGNATION_ALL = 8,
            SELECT_PROJECT_CONTACT = 9,
            DELETE_PROJECT_CONTACT = 10,
            DELETE_PROJECTTYPE = 11,
            DELETE_Department_Name = 61,
            CHECK_PROJECTNAME_DUPLICATE = 12,
            DELETE_PROJECT_FROM_LIST = 13,
            SELECT_PROJECT_BY_PROJECT_ID = 14,
            UPDATE_PROJECT_TYPE_BY_ID = 15,
            SELECT_PROJECT_BY_Project_Code = 16,
            SELECT_COMPANY_ALL = 17,
            SELECT_COMPANY_BY_ID = 18,
            DELETE_COMPANY = 19,
            INSERT_COMPANY = 20,
            UPDATE_COMPANY_BY_ID = 21,
            SELECT_PROJECT_FILE_ALL = 22,
            DELETE_PROJECT_FILE = 23,
            INSERT_FreshLetter = 24,
            UPDATE_FreshLetter_BY_ID = 25,
            GENERATE_LETTER_ID = 26,
            SELECT_LetterID_ALL = 27,
            UPDATE_ReplayToLetter_BY_ID = 28,
            INSERT_ReplayToLetter = 29,
            SELECT_REPLAYTOLETTER_GRID = 30,
            INSERT_FreshLetter_letRecFrom_Dept = 31,
            UPDATE_FreshLetter_BY_ID_letRecFrom_Dept = 33,
            INSERT_ReplayToLetter_letRecFrom_Dept = 34,
            UPDATE_ReplayToLetter_BY_ID_letRecFrom_Dept = 35,
            SELECT_LetterID_ALL_letRecFrom_Dept = 36,
            SELECT_REPLAYTOLETTER_GRID_letRecFrom_Dept = 37,
            DELETE_FreshLetter = 38,
            DELETE_ReplayLetter = 39,
            GENERATE_LETTER_ID_letRecFrom_Dept = 40,
            INSERT_Variations_Price_Adjustments = 41,
            INSERT_Vendor_Credentials = 42,
            INSERT_Contract_Agreement = 43,
            INSERT_Drawings = 44,
            INSERT_Bill_of_Quantity = 45,
            SELECT_GridDrawings = 46,
            SELECT_GridVariations = 47,
            SELECT_GridQuantity = 48,
            SELECT_Bill_of_Quantity = 49,
            SELECT_GridVendorCredentialsApprovals = 50,
            DELETE_FreshLetter_letRecFrom_Dept = 51,
            DELETE_ReplayLetter_letRecFrom_Dept = 52,
            DELETE_Variations = 53,
            DELETE_Quantity = 54,
            Delete_Drawings = 55,
            Delete_Vendor_Credentials = 56,
            SELECT_Contract_Ag = 57,
            DELETE_Contract_Agreement = 58,
            INSERT_Letter_Recived_Dept = 59,
            SELECT_Letter_Recived_Dept_Name_ALL = 60,
            INSERT_SITE_LOCATION = 62,
            SITE_LOCATION_OPERATIONS = 63,
            SELECT_USER_DETAILS_BY_ID = 64,
            INSERT_BG_DOCUMENTS = 65,
            INSERT_INSURANCE_DOCUMENTS = 66,
            SELECT_Lisence = 67,
            delete_License = 68,
            INSERT_License = 69,
            SELECT_BG_DOC_ALL = 70,
            DELETE_BG_DOC = 71,
            SELECT_INSURANCE_DOC = 72,
            DELETE_INSURANCE_DOC = 73,
            SELECT_BG_EXTENSION_BY_ID = 74,
            INSERT_BG_EXTENSION = 75,
            SELECT_INSURANCERENEWAL_BY_ID = 76,
            INSERT_INSURANCE_RENEWAL = 77,
            INSERT_EXTENSION_LICENSE = 78,
            SELECT_EXTENSION_LICENSE_BY_ID = 79,
            DELETE_LicenseType = 80,
            SELECT_LicenseType_BY_ID = 81,
            INSERT_LicenseType = 82,
            UPDATE_licensetype_BY_ID = 83,
            SELECT_LicenseType_ALL = 84,
            SELECT_BG_TYPE_BY_ALL = 85,
            SELECT_BG_TYPE_BY_ID = 86,
            UPDATE_BG_TYPE_BY_ID = 87,
            INSERT_BG_TYPE = 88,
            DELETE_BG_TYPE_BY_ID = 89,
            SELECT_TB_INSURANCE_TYPE_ALL = 90,
            SELECT_INSURANCE_TYPE_BY_ID = 91,
            UPDATE_INSURANCE_TYPE_BY_ID = 92,
            INSERT_INSURANCE_TYPE = 93,
            DELETE_INSURANCE_TYPE = 94,
            UPDATE_FreshLetter_letRecFrom_Dept = 95,//Added By Prashanth
            SELECT_FRESHLETTER_DETAILS_BY_ID = 96,//added by prashanth
            SELECT_FRESHLETTER_DEPT_DETAILS_BY_ID = 97,//added by prashanth
            SELECT_REPLYTOLETTER_DETAILS_BY_ID = 98, //added by prashanth
            UPDATE_ReplayToLetter_BY_Letter_ID_RTL = 99,//added by prashanth
            INSERT_PAYMENT_TERMS = 100,//added by prashanth
            SELECT_PAYMENT_TERMS_BY_PAYMENTTERMS_ID = 101, //added by prashanth
            DELETE_PAYMENT_TERMS = 102,//added by prashanth
            SELECT_PAYMENT_CATAGORY_ALL = 103,//added by prashanth
            INSERT_PAYMENT_CATAGORY_SUB = 104,//added by prashanth
            SELECT_PAYMENT_SUB_CATGORY_BY_PAYMENTTERMS_ID = 105,//added by prashanth
            DELETE_PAYMENT_SUB_CAT = 106,//added by prashanth
            SELECT_PAYMENT_SUB_CATAGORY_ALL = 107,//added by prashanth
            INSERT_PAYMENT_CATAGORY_TOWARDS = 108,//added by prashanth
            SELECT_PAYMENT_CATGORY_TOWARDS_BY_PAYMENT_TOWARDS_ID = 109,//added by prashanth
            SELECT_PAYMENT_CATAGORY_TOWARDS_ALL = 110,//added by prashanth
            DELETE_PAYMENT_TOWARDS = 111,//added by prashanth
            INSERTBANKINDENT = 112,
            UPDATEBANKINDENT = 113,
            SELECT_BANKINDENT_ALL = 114,
            UPDATE_PAYMENT_CAT_BY_ID = 115,
            UPDATE_PAYMENT_CAT_SUB_BY_ID = 116,
            UPDATE_PAYMENT_CAT_TOWARDS_BY_ID = 117,
            SELECT_Replay_NewDDL_LetterID_ALL = 118,
            SELECT_Ext_Lisence = 119,
            SELECT_PAYMENT_CATAGORY_DROPDOWN = 120,
            SELECT_PAYMENT_SUBCATAGORY_DROPDOWN = 121,
            SELECT_PROJECT_DETAILS_BY_ID_CLOSED_PROJECTS = 122,
            SELECT_PROJECT_ALL_CLOSED_PROJECTS = 123,
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
        #region "Class: ProjectBL Sets / Gets"

        public int UserID { get; set; }
        public int Proj_Type_ID { get; set; }
        public int ID_DocumentName { get; set; }
        public string ProjectTypeName { get; set; }
        public string ProjectTypeCode { get; set; }

        public string txtpaymentTowards { get; set; }

        public string PAYMENT_TOWARDS_ID { get; set; }

        public string PAYMENT_CATAGORY_SUB { get; set; }
        public string PAYMENT_SUB_ID { get; set; }


        public string LetterRecivedFrom_Name { get; set; }
        public string Project_Code
        { get; set; }
        public string Project_Name
        { get; set; }

        public string txtpaymentcat
        { get; set; }
        public int Type
        { get; set; }
        public string State
        { get; set; }
        public int Location
        { get; set; }
        public string BANK_INDENT_No
        { get; set; }
        public int BANK_INDENT_ID
        { get; set; }
        public string AliasProject
        { get; set; }
        public string PAYMENT_ID
        { get; set; }
        public DateTime? StartDate
        { get; set; }
        public DateTime? End_Dt
        { get; set; }
        public DateTime? IndentDate
        { get; set; }

        //public DateTime IndentDate { get; set; }
        public string FYear
        { get; set; }
        public string BankName
        { get; set; }
        public string Bank
        { get; set; }
        public string Branch
        { get; set; }
        public string ISFC
        { get; set; }
        public string PaymentType
        { get; set; }
        public string PaymentCatagary
        { get; set; }
        public string PaymentSubCatagary
        { get; set; }
        public string Towards
        { get; set; }
        public string Refdetails
        { get; set; }
        public string Narration
        { get; set; }
        public string RateOfInterest
        { get; set; }
        public decimal? Amountindent
        { get; set; }
        public string ACCOUNT_NUMBER { get; set; }

        public string Uploaded_File { get; set; }
        public decimal? Proj_Cost
        { get; set; }
        public string Award_By
        { get; set; }
        public string Description
        { get; set; }
        public string Status
        { get; set; }
        public string CompanyName
        { get; set; }
        public string Letter_Recived_Dept
        { get; set; }
        public DateTime? E_Agreement_Date
        { get; set; }
        public string Principal_Contractor
        { get; set; }
        public DateTime? Agreement_Date
        { get; set; }
        public string CompanyCode
        { get; set; }

        public string File1 { get; set; }
        public string txtpaymentsubcat { get; set; }
        public string FileName1 { get; set; }
        public string File2 { get; set; }
        public string FileName2 { get; set; }
        public string File3 { get; set; }
        public string FileName3 { get; set; }
        public string File4 { get; set; }
        public string FileName4 { get; set; }
        public string File5 { get; set; }
        public string FileName5 { get; set; }
        public int ProjectFile_ID { get; set; }
        public string Task { get; set; }



        public int Proj_Con_ID
        { get; set; }
        public int paymentddl
        { get; set; }
        public int paymentsubddl
        { get; set; }
        public string Proj_Code
        { get; set; }
        public string Cont_Type
        { get; set; }
        public string Name
        { get; set; }
        public string Department
        { get; set; }
        public string Designation
        { get; set; }
        public string Cont_No
        { get; set; }
        public string Email_ID
        { get; set; }
        public string Dispatch_Add
        { get; set; }
        public string LicenseName
        { get; set; }
        public string LicenseCode
        {
            get;
            set;
        }
        public string License_Code
        {
            get;
            set;
        }

        // Document Upload
        #region
        public string Letter_ID { get; set; }
        public string StateCode { get; set; }
        public string ProjectCode { get; set; }
        public DateTime? LetterDate { get; set; }
        public string LetterRefNumber { get; set; }
        public string LetterRecdFrom { get; set; }
        public DateTime? LetterReceivedDate { get; set; }
        public DateTime? DateofReceipt { get; set; }
        public DateTime? ReplyByDate { get; set; }
        public string ModeofRecepit { get; set; }
        public string Copyof_Fresh_Letter_Path { get; set; }
        public string FreshLetterSubject { get; set; }

        public int ID { get; set; }
        public string License_Type { get; set; }

        public string Issuer_Name { get; set; }
        public string License_Number { get; set; }


        public decimal? CGSTPerc { get; set; }
        public decimal? SGSTPerc { get; set; }
        public decimal? IGSTPerc { get; set; }
        public decimal? CGSTAmt { get; set; }
        public decimal? SGSTAmt { get; set; }
        public decimal? IGSTAmt { get; set; }
        public decimal? AmountAfterTax { get; set; }

        public decimal? Amount { get; set; }
        public DateTime? License_Date { get; set; }
        public DateTime? Original_Expiry_Date { get; set; }
        public string License_Copy_FilePath { get; set; }
        public string Extension { get; set; }
        public DateTime? Valid_upto { get; set; }
        public string Upload_FilePath { get; set; }
        public string ReplayToLetter_ID_RTL { get; set; }
        public string LetterSentTo_RTL { get; set; }
        public string Subject { get; set; }
        public DateTime? Date_RTL { get; set; }

        public string LetterRefNumber_RTL { get; set; }

        public string ModeofDispatch_RTL { get; set; }

        public string LetterCopy_RTL { get; set; }

        public string AcknowledgementCopy_RTL { get; set; }

        public string ExtenxionFilePath { get; set; }
        /// Variations

        public string Filename_Variations { get; set; }
        public string ApprovalDocumentVariations { get; set; }
        public string Variations_FilePath { get; set; }
        public string ApprovalDocumentVariations_FilePath { get; set; }

        //Vendor Credentials
        public string Filename_Vendor_Credentials { get; set; }
        public string ApprovalDocumentsCredentialsApproval { get; set; }

        public string ApprovalDocumentsCredentialsApproval_FilePath { get; set; }
        public string Vendor_Credentials_FilePath { get; set; }
        //Contract Agreement
        public string Filename_ContractAgreement { get; set; }
        public string ContractAgreement_FilePath { get; set; }
        //Contract Drawings
        public string Filename_Drawings { get; set; }
        public string ApprovalDocumentsDrawings { get; set; }
        public string ApprovalDocumentsDrawings_FilePath { get; set; }
        public string Drawings_FilePath { get; set; }

        //Bill of Quantity
        public string Filename_BillofQuantity { get; set; }
        public string BillofQuantity_FilePath { get; set; }
        /// Letter Received from Department

        public string Letter_ID_RecFrDept { get; set; }
        public string ProjectCode_RecFrDept { get; set; }
        public DateTime? LetterDate_RecFrDept { get; set; }
        public string LetterRefNumber_RecFrDept { get; set; }
        public string LetterRecdFrom_RecFrDept { get; set; }
        public DateTime? LetterReceivedDate_RecFrDept { get; set; }
        public DateTime? DateofReceipt_RecFrDept { get; set; }
        public DateTime? ReplyByDate_RecFrDept { get; set; }
        public string ModeofRecepit_RecFrDept { get; set; }
        public string Copyof_Fresh_Letter_Path_RecFrDept { get; set; }
        public string FreshLetterSubject_RecFrDept { get; set; }
        // BG Documents
        public string AddBGtype { get; set; }
        public string BG_Type { get; set; }
        public string BG_Beificiary { get; set; }
        public string BG_Number { get; set; }
        public decimal BG_Amount { get; set; }
        public DateTime BG_Date { get; set; }
        public DateTime BG_Original_Expiry_Date { get; set; }
        public DateTime Claim_Date { get; set; }
        public string BG_Copy_FilePath { get; set; }
        public string BGExtension { get; set; }
        public DateTime? Valid_Upto { get; set; }
        public string Extension_Copy { get; set; }
        public int BG_ID { get; set; }

        // Insurance 

        public string AddInsuranceType { get; set; }
        public string Insurance_Type { get; set; }
        public string Insurer_Name { get; set; }
        public string Policy_Number { get; set; }
        public decimal Policy_Amount { get; set; }
        public DateTime Policy_Date { get; set; }
        public DateTime Insurance_Original_Expiry_Date { get; set; }
        public string Insurance_Copy_FilePath { get; set; }
        public string Insurance_Name { get; set; }
        public DateTime? Insurance_Renewal_Valid_Upto { get; set; }
        public string Insurance_Renewal_Copy { get; set; }
        public int Insurance_ID { get; set; }

        // Extension_License
        public string License_Extension { get; set; }
        public DateTime? License_Valid_Upto { get; set; }
        public string License_Extension_Copy { get; set; }
        public int License_ID { get; set; }

        ///
        public string ReplayToLetter_ID_RTL_RecFrDept { get; set; }
        public string LetterSentTo_RTL_RecFrDept { get; set; }
        public DateTime? Date_RTL_RecFrDept { get; set; }

        public string LetterRefNumber_RTL_RecFrDept { get; set; }

        public string ModeofDispatch_RTL_RecFrDept { get; set; }

        public string LetterCopy_RTL_RecFrDept { get; set; }

        public string AcknowledgementCopy_RTL_RecFrDept { get; set; }

        #endregion
        // Site location
        public string SiteAddress { get; set; }
        public string SiteMobileNumber { get; set; }
        public string SiteContactPerson { get; set; }
        public int Site_ID { get; set; }
        //Start:Prashanth changes
        public string CC1 { get; set; }
        public string CC2 { get; set; }
        public string CC3 { get; set; }
        public string CC4 { get; set; }
        public string CC5 { get; set; }

        //End:Prashanth changes
        #region "Class: ProjectBL Methods"

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
                    if (dr["Proj_Type"] != DBNull.Value)
                    {
                        this.ProjectTypeName = dr["Proj_Type"].ToString();
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
                    if (dr["Proj_Type"] != DBNull.Value)
                    {
                        this.ProjectTypeName = dr["Proj_Type"].ToString();
                    }
                    if (dr["Proj_Type_Code"] != DBNull.Value)
                    {
                        this.ProjectTypeCode = dr["Proj_Type_Code"].ToString();
                    }
                    if (dr["PAYMENT_TERMS"] != DBNull.Value)
                    {
                        this.txtpaymentcat = dr["PAYMENT_TERMS"].ToString();
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

        private bool fillObjectFromDrBI(SqlDataReader dr)
        {
            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName

                    if (dr["PAYMENT_TERMS"] != DBNull.Value)
                    {
                        this.txtpaymentcat = dr["PAYMENT_TERMS"].ToString();
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
        private bool fillObjectFromDrBIS(SqlDataReader dr)
        {
            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName

                    if (dr["PAYMENT_CATAGORY_SUB"] != DBNull.Value)
                    {
                        this.txtpaymentsubcat = dr["PAYMENT_CATAGORY_SUB"].ToString();
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
        private bool fillObjectFromDrBIT(SqlDataReader dr)
        {
            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    //clsCountry.CountryName

                    if (dr["PAYMENT_CATAGORY_TOWARDS"] != DBNull.Value)
                    {
                        this.txtpaymentTowards = dr["PAYMENT_CATAGORY_TOWARDS"].ToString();
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

        private bool fillObjectFromDrCompany(SqlDataReader dr)
        {
            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    if (dr["Company_Code"] != DBNull.Value)
                    {
                        this.CompanyCode = dr["Company_Code"].ToString();
                    }
                    if (dr["Company_Name"] != DBNull.Value)
                    {
                        this.CompanyName = dr["Company_Name"].ToString();
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
                case eLoadSp.INSERT_INSURANCE_TYPE:
                    return "PRO_INSERT_INSURANCE_TYPE";
                case eLoadSp.INSERT_BG_TYPE:
                    return "PRO_INSERT_BG_TYPE";
                case eLoadSp.INSERT_LicenseType:
                    return "PRO_INSERT_LicenseType";
                case eLoadSp.SELECT_LicenseType_BY_ID:
                    return "PRO_SELECT_LicenseType_BY_ID";
                case eLoadSp.INSERT:
                    return "PRO_ProjectType_INSERT";
                case eLoadSp.INSERTBANKINDENT:
                    return "PROC_BANK_INDENT_INSERT_UPDATE";
                case eLoadSp.INSERT_PAYMENT_TERMS:
                    return "PRO_PAYMENT_CATGORY_INSERT";
                case eLoadSp.INSERT_PAYMENT_CATAGORY_SUB:
                    return "PRO_PAYMENT_CATGORY_SUB_INSERT";
                case eLoadSp.INSERT_PAYMENT_CATAGORY_TOWARDS:
                    return "PRO_PAYMENT_CATGORY_TOWARDS_INSERT";
                case eLoadSp.SELECT_PROJECTTYPE_ALL:
                    return "PRO_SELECT_PROJECT_ALL";
                case eLoadSp.SELECT_PAYMENT_CATAGORY_ALL:
                    return "PRO_SELECT_PAYMENT_CATGORY_ALL";
                case eLoadSp.SELECT_PAYMENT_SUB_CATAGORY_ALL:
                    return "PRO_SELECT_PAYMENT_SUB_CATGORY_ALL";
                case eLoadSp.SELECT_PAYMENT_CATAGORY_TOWARDS_ALL:
                    return "PRO_SELECT_PAYMENT_CATGORY_Towards_ALL";
                case eLoadSp.SELECT_BANKINDENT_ALL:
                    return "PRO_SELECT_BANKINDENT_ALL";
                case eLoadSp.INSERT_PROJECT:
                    return "PRO_PROJECT_INSERT";
                case eLoadSp.SELECT_PROJECT_ALL:
                    return "PRO_SELECT_PROJECT_ALL";
                case eLoadSp.SELECT_PROJECT_ALL_CLOSED_PROJECTS:
                    return "PRO_SELECT_PROJECT_ALL_CLOSED_PROJECTS";

                case eLoadSp.SELECT_USER_DETAILS_BY_ID:
                    return "PROC_SELECT_USER_BY_ID";
                case eLoadSp.SELECT_PROJECT_DETAILS_BY_ID:
                    return "SELECT_PROJECT_DETAILS_BY_ID";
                case eLoadSp.SELECT_PROJECT_DETAILS_BY_ID_CLOSED_PROJECTS:
                    return "PRO_SELECT_PROJECT_DETAILS_BY_ID_CLOSED_PROJECTS";

                case eLoadSp.UPDATE:
                    return "PRO_PROJECT_UPDATE";
                case eLoadSp.UPDATEBANKINDENT:
                    return "PROC_BANK_INDENT_INSERT_UPDATE";
                case eLoadSp.INSERT_PROJECT_CONTACT:
                    return "PRO_INSERT_PROJECT_CONTACT";
                case eLoadSp.SELECT_DEPARTMENT_ALL:
                    return "PROC_TB_Department_SELECT_ALL";
                case eLoadSp.SELECT_DESIGNATION_ALL:
                    return "PROC_TB_Designation_SELECT_ALL";
                case eLoadSp.SELECT_PROJECT_CONTACT:
                    return "PROC_SELECT_PROJECT_CONTACT";
                case eLoadSp.DELETE_PROJECT_CONTACT:
                    return "PROC_DELETE_PROJECT_CONTACT";
                case eLoadSp.DELETE_Department_Name:
                    return "PROC_DELETE_Letter_Recived_Dept_Name_BY_ID";
                case eLoadSp.DELETE_INSURANCE_TYPE:
                    return "PRO_DELETE_INSURANCE_TYPE";
                case eLoadSp.DELETE_LicenseType:
                    return "PRO_LicenseType_COMPANY";
                case eLoadSp.DELETE_BG_TYPE_BY_ID:
                    return "PRO_DELETE_BG_TYPE_BY_ID";
                case eLoadSp.DELETE_PROJECTTYPE:
                    return "PROC_DELETE_PROJECTTYPE_BY_ID";
                case eLoadSp.DELETE_PAYMENT_SUB_CAT:
                    return "PROC_DELETE_PAYMENT_SUB_CAT_ID";
                case eLoadSp.DELETE_PAYMENT_TERMS:
                    return "PROC_DELETE_PAYMENT_CAT_ID";
                case eLoadSp.DELETE_PAYMENT_TOWARDS:
                    return "PROC_DELETE_PAYMENT_TOWARDS_ID";
                case eLoadSp.CHECK_PROJECTNAME_DUPLICATE:
                    return "PRO_CHECK_PROJECTNAME_DUPLICATE";
                case eLoadSp.DELETE_PROJECT_FROM_LIST:
                    return "PRO_DELETE_PROJECT_FROM_LIST";
                case eLoadSp.SELECT_PROJECT_BY_PROJECT_ID:
                    return "PROC_ProjectType_SELECTBYID";
                case eLoadSp.UPDATE_PROJECT_TYPE_BY_ID:
                    return "PROC_ProjectType_ID_UPDATE";
                case eLoadSp.UPDATE_PAYMENT_CAT_BY_ID:
                    return "PROC_PAYMENT_CAT_ID_UPDATE";
                case eLoadSp.UPDATE_PAYMENT_CAT_SUB_BY_ID:
                    return "PROC_PAYMENT_CAT_SUB_ID_UPDATE";
                case eLoadSp.UPDATE_PAYMENT_CAT_TOWARDS_BY_ID:
                    return "PROC_PAYMENT_CAT_TOWARDS_ID_UPDATE";
                case eLoadSp.SELECT_PROJECT_BY_Project_Code:
                    return "PRO_SELECT_PROJECT_ALL_BY_Project_Code";
                case eLoadSp.SELECT_COMPANY_ALL:
                    return "PRO_SELECT_COMPANY_ALL";
                case eLoadSp.SELECT_Letter_Recived_Dept_Name_ALL:
                    return "PRO_SELECT_Letter_Recived_Dept_Name_ALL";
                case eLoadSp.SELECT_COMPANY_BY_ID:
                    return "PRO_SELECT_COMPANY_BY_ID";
                case eLoadSp.DELETE_COMPANY:
                    return "PRO_DELETE_COMPANY";
                case eLoadSp.INSERT_COMPANY:
                    return "PRO_INSERT_COMPANY";
                case eLoadSp.INSERT_Letter_Recived_Dept:
                    return "PRO_INSERT_Letter_Recived_Dept";
                case eLoadSp.UPDATE_COMPANY_BY_ID:
                    return "PRO_UPDATE_COMPANY_BY_ID";
                case eLoadSp.SELECT_PROJECT_FILE_ALL:
                case eLoadSp.DELETE_PROJECT_FILE:
                    return "PRO_PROJECT_FILE";
                case eLoadSp.INSERT_FreshLetter:
                    return "PRO_FreshLetter_INSERT";
                case eLoadSp.INSERT_FreshLetter_letRecFrom_Dept:
                    return "PRO_FreshLetter_INSERT_letRecFrom_Dept";
                case eLoadSp.UPDATE_FreshLetter_letRecFrom_Dept://added by prashanth
                    return "PRO_FreshLetter_UPDATE_letRecFrom_Dept";//added by prashanth

                case eLoadSp.SELECT_FRESHLETTER_DETAILS_BY_ID://added by prashanth
                    return "SELECT_FRESHLETTER_DETAILS_BY_ID";//added by prashanth

                case eLoadSp.SELECT_FRESHLETTER_DEPT_DETAILS_BY_ID://added by prashanth
                    return "SELECT_FRESHLETTER_DEPT_DETAILS_BY_ID";//added by prashanth

                case eLoadSp.SELECT_REPLYTOLETTER_DETAILS_BY_ID://added by prashanth 
                    return "SELECT_REPLYTOLETTER_DETAILS_BY_ID";//added by prashanth
                case eLoadSp.SELECT_PAYMENT_TERMS_BY_PAYMENTTERMS_ID: //added by prashanth
                    return "PROC_PAYMENT_CATAGORY_SELECTBYID";
                case eLoadSp.SELECT_PAYMENT_SUB_CATGORY_BY_PAYMENTTERMS_ID: //added by prashanth
                    return "PROC_PAYMENT_SUB_CATAGORY_SELECTBYID";
                case eLoadSp.SELECT_PAYMENT_CATGORY_TOWARDS_BY_PAYMENT_TOWARDS_ID: //added by prashanth
                    return "PROC_PAYMENT_CATAGORY_TOWARDS_SELECTBYID";
                case eLoadSp.UPDATE_FreshLetter_BY_ID:
                    return "PRO_UPDATE_FreshLetter_BY_ID";
                case eLoadSp.GENERATE_LETTER_ID:
                    return "PROC_AutoGenerate_LetterID";
                case eLoadSp.GENERATE_LETTER_ID_letRecFrom_Dept:
                    return "PROC_AutoGenerate_LetterID_letRecFrom_Dept";
                case eLoadSp.SELECT_Replay_NewDDL_LetterID_ALL:
                    return "PRO_SELECT_Replay_NewDDL_LetterID_ALL";
                case eLoadSp.SELECT_LetterID_ALL:
                    return "PRO_SELECT_Fresh_LetterID_ALL";
                case eLoadSp.SELECT_LetterID_ALL_letRecFrom_Dept:
                    return "PRO_SELECT_Fresh_LetterID_ALL_letRecFrom_Dept";
                case eLoadSp.UPDATE_ReplayToLetter_BY_ID:
                    return "PRO_UPDATE_ReplayToLetter_BY_ID";
                case eLoadSp.UPDATE_ReplayToLetter_BY_ID_letRecFrom_Dept:
                    return "PRO_UPDATE_ReplayToLetter_BY_ID_letRecFrom_Dept";
                case eLoadSp.INSERT_ReplayToLetter:
                    return "PRO_ReplayToLetter_INSERT";
                case eLoadSp.INSERT_ReplayToLetter_letRecFrom_Dept:
                    return "PRO_ReplayToLetter_INSERT_letRecFrom_Dept";
                case eLoadSp.SELECT_REPLAYTOLETTER_GRID:
                    return "PRO_SELECT_ReplayToLetter_ALL";
                case eLoadSp.UPDATE_FreshLetter_BY_ID_letRecFrom_Dept:
                    return "PRO_UPDATE_FreshLetter_BY_ID_letRecFrom_Dept";
                case eLoadSp.SELECT_REPLAYTOLETTER_GRID_letRecFrom_Dept:
                    return "PRO_SELECT_ReplayToLetter_ALL_letRecFrom_Dept";
                case eLoadSp.DELETE_FreshLetter:
                    return "PROC_DELETE_FreshLetter";
                case eLoadSp.DELETE_ReplayLetter:
                    return "PROC_DELETE_ReplayLetter";
                case eLoadSp.INSERT_INSURANCE_DOCUMENTS:
                    return "PRO_INSERT_INSURANCE_DOCUMENTS";
                case eLoadSp.INSERT_BG_DOCUMENTS:
                    return "PRO_INSERT_BG_DOCUMENTS";
                case eLoadSp.INSERT_Variations_Price_Adjustments:
                    return "PRO_Insert_Variations_Price_Adjustments";
                case eLoadSp.INSERT_Vendor_Credentials:
                    return "PRO_Insert_Vendor_Credentials";
                case eLoadSp.INSERT_Contract_Agreement:
                    return "PRO_Insert_Contract_Agreement";
                case eLoadSp.INSERT_Drawings:
                    return "PRO_Insert_Drawings";
                case eLoadSp.INSERT_Bill_of_Quantity:
                    return "PRO_Insert_Billof_Quantity";
                case eLoadSp.SELECT_GridDrawings:
                    return "PRO_SELECT_Drawings";
                case eLoadSp.SELECT_Contract_Ag:
                    return "PRO_SELECT_ContractAgreement";
                case eLoadSp.SELECT_GridVariations:
                    return "PRO_SELECT_Variations_PriceAdjustments";
                case eLoadSp.SELECT_GridQuantity:
                    return "PRO_SELECT_Quantity";
                case eLoadSp.SELECT_Bill_of_Quantity:
                    return "PRO_SELECT_Billof_Quantity";
                case eLoadSp.SELECT_GridVendorCredentialsApprovals:
                    return "PRO_SELECT_Vendor_Credentials";
                case eLoadSp.DELETE_FreshLetter_letRecFrom_Dept:
                    return "PROC_DELETE_FreshLetter_LetRecFrom_Dept";
                case eLoadSp.DELETE_ReplayLetter_letRecFrom_Dept:
                    return "PROC_DELETE_ReplayLetter_LetRecFrom_Dept";
                case eLoadSp.DELETE_Variations:
                    return "PROC_DELETE_Variations";
                case eLoadSp.DELETE_Quantity:
                    return "PROC_DELETE_Quantity";
                case eLoadSp.Delete_Drawings:
                    return "PROC_DELETE_Drawings";
                case eLoadSp.Delete_Vendor_Credentials:
                    return "PRO_DELETE_Vendor_Credentials";
                case eLoadSp.DELETE_Contract_Agreement:
                    return "PRO_DELETE_Contract_Agreement";
                case eLoadSp.INSERT_SITE_LOCATION:
                    return "PRO_INSERT_SITELOCATION";
                case eLoadSp.SITE_LOCATION_OPERATIONS:
                    return "PRO_SITELOCATION_OPERATIONS";
                case eLoadSp.INSERT_License:
                    return "PRO_Insert_License";
                case eLoadSp.INSERT_BG_EXTENSION:
                    return "PRO_INSERT_BG_EXTENSION";
                case eLoadSp.INSERT_INSURANCE_RENEWAL:
                    return "PRO_INSERT_INSURANCE_RENEWAL";
                case eLoadSp.INSERT_EXTENSION_LICENSE:
                    return "PRO_INSERT_EXTENSION_LICENSE";
                case eLoadSp.DELETE_INSURANCE_DOC:
                    return "PRO_DELETE_INSURANCE_DOC";
                case eLoadSp.SELECT_Lisence:
                    return "PRO_SELECT_Lisence";
                case eLoadSp.SELECT_Ext_Lisence:
                    return "PRO_SELECT_Ext_Lisence";
                case eLoadSp.SELECT_INSURANCE_DOC:
                    return "PRO_SELECT_INSURANCE_DOC";
                case eLoadSp.SELECT_BG_DOC_ALL:
                    return "PRO_SELECT_BG_DOC_ALL";
                case eLoadSp.delete_License:
                    return "PRO_DELETE__Lisence";
                case eLoadSp.DELETE_BG_DOC:
                    return "PRO_DELETE_BG_DOC";
                case eLoadSp.SELECT_BG_EXTENSION_BY_ID:
                    return "PRO_SELECT_BG_EXTENSION_BY_ID";
                case eLoadSp.SELECT_INSURANCERENEWAL_BY_ID:
                    return "PRO_SELECT_INSURANCERENEWAL_BY_ID";
                case eLoadSp.SELECT_EXTENSION_LICENSE_BY_ID:
                    return "PRO_SELECT_EXTENSION_LICENSE_BY_ID";
                case eLoadSp.UPDATE_licensetype_BY_ID:
                    return "PRO_UPDATE_LicenseType_BY_ID";
                case eLoadSp.SELECT_INSURANCE_TYPE_BY_ID:
                    return "PRO_SELECT_INSURANCE_TYPE_BY_ID";
                case eLoadSp.SELECT_BG_TYPE_BY_ID:
                    return "PRO_SELECT_BG_TYPE_BY_ID";
                case eLoadSp.UPDATE_INSURANCE_TYPE_BY_ID:
                    return "PRO_UPDATE_INSURANCE_TYPE_BY_ID";
                case eLoadSp.UPDATE_BG_TYPE_BY_ID:
                    return "PRO_UPDATE_BG_TYPE_BY_ID";
                case eLoadSp.SELECT_LicenseType_ALL:
                    return "PRO_SELECT_LicenseType_ALL";
                case eLoadSp.SELECT_BG_TYPE_BY_ALL:
                    return "PRO_SELECT_BG_TYPE_BY_ALL";
                case eLoadSp.SELECT_TB_INSURANCE_TYPE_ALL:
                    return "PRO_SELECT_TB_INSURANCE_TYPE_ALL";
                case eLoadSp.UPDATE_ReplayToLetter_BY_Letter_ID_RTL://Added by prashanth
                    return "PRO_UPDATE_ReplayToLetter_BY_Letter_ID_RTL";//Added by Prashanth
                case eLoadSp.SELECT_PAYMENT_CATAGORY_DROPDOWN://Added by prashanth
                    return "PRO_SELECT_PAYMENT_CATGORY_DDL";//Added by Prashanth
                case eLoadSp.SELECT_PAYMENT_SUBCATAGORY_DROPDOWN://Added by prashanth
                    return "PRO_SELECT_PAYMENT_SUBCATGORY_DDL";//Added by Prashanth
                default:
                    return string.Empty;
            }
        }
        public bool loadInsuranceType(SqlConnection SqlConn, eLoadSp enumSpName)
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

                if (fillObjectFromloadInsuranceType(dr))
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
        public bool loadBGType(SqlConnection SqlConn, eLoadSp enumSpName)
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

                if (fillObjectFromBGType(dr))
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
        public bool loadLicenseType(SqlConnection SqlConn, eLoadSp enumSpName)
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

                if (fillObjectFromDrLicensrType(dr))
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

        private bool fillObjectFromloadInsuranceType(SqlDataReader dr)
        {
            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    if (dr["Insurance_Type"] != DBNull.Value)
                    {
                        this.AddBGtype = dr["Insurance_Type"].ToString();
                    }
                    if (dr["ID"] != DBNull.Value)
                    {
                        this.ID = Convert.ToInt32(dr["ID"].ToString());
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
        private bool fillObjectFromBGType(SqlDataReader dr)
        {
            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    if (dr["BG_Type"] != DBNull.Value)
                    {
                        this.AddBGtype = dr["BG_Type"].ToString();
                    }
                    if (dr["ID"] != DBNull.Value)
                    {
                        this.ID = Convert.ToInt32(dr["ID"].ToString());
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
        private bool fillObjectFromDrLicensrType(SqlDataReader dr)
        {
            try
            {
                // Loop though the data reader
                while (dr.Read())
                {
                    if (dr["License_Code"] != DBNull.Value)
                    {
                        this.LicenseCode = dr["License_Code"].ToString();
                    }
                    if (dr["License_Name"] != DBNull.Value)
                    {
                        this.LicenseName = dr["License_Name"].ToString();
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

                case eLoadSp.SELECT_Replay_NewDDL_LetterID_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_LetterID_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_GridDrawings:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_Bill_of_Quantity:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.DELETE_INSURANCE_TYPE:
                case eLoadSp.DELETE_BG_TYPE_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.DELETE_LicenseType:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@License_Code", this.License_Code)
                };
                    break;
                case eLoadSp.SELECT_LicenseType_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@License_Code", this.License_Code)
                };
                    break;
                case eLoadSp.SELECT_BANKINDENT_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code)
                };
                    break;

                case eLoadSp.SELECT_Contract_Ag:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_BG_DOC_ALL:
                case eLoadSp.SELECT_Lisence:
                case eLoadSp.SELECT_Ext_Lisence:
                case eLoadSp.SELECT_INSURANCE_DOC:
                case eLoadSp.SELECT_GridVendorCredentialsApprovals:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.DELETE_INSURANCE_DOC:
                case eLoadSp.DELETE_BG_DOC:
                case eLoadSp.SELECT_BG_EXTENSION_BY_ID:
                case eLoadSp.SELECT_EXTENSION_LICENSE_BY_ID:
                case eLoadSp.SELECT_INSURANCERENEWAL_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.delete_License:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.SELECT_GridVariations:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_REPLAYTOLETTER_GRID_letRecFrom_Dept:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_REPLAYTOLETTER_GRID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_LetterID_ALL_letRecFrom_Dept:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value)
                };

                    break;
                case eLoadSp.SELECT_USER_DETAILS_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@UserID", this.UserID)
                };
                    break;
                case eLoadSp.SELECT_PROJECT_DETAILS_BY_ID_CLOSED_PROJECTS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code ??  (object)DBNull.Value)
                };
                    break;

                case eLoadSp.SELECT_PROJECT_DETAILS_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code ??  (object)DBNull.Value)
                };
                    break;

                case eLoadSp.SITE_LOCATION_OPERATIONS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Proj_Code ??  (object)DBNull.Value),
                    new SqlParameter("@Task", this.Task ??  (object)DBNull.Value),
                     new SqlParameter("@Site_ID", this.Site_ID)
                };
                    break;
                case eLoadSp.SELECT_PROJECT_CONTACT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Proj_Code ??  (object)DBNull.Value),

                };
                    break;
                case eLoadSp.DELETE_FreshLetter_letRecFrom_Dept:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;

                case eLoadSp.DELETE_Contract_Agreement:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.Delete_Vendor_Credentials:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.Delete_Drawings:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.DELETE_Quantity:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.DELETE_Variations:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.DELETE_ReplayLetter_letRecFrom_Dept:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.DELETE_ReplayLetter:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.DELETE_FreshLetter:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ID", this.ID)
                };
                    break;
                case eLoadSp.DELETE_PROJECT_CONTACT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ProContactID", this.Proj_Con_ID)
                };
                    break;
                case eLoadSp.DELETE_PROJECTTYPE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Proj_Type_ID", this.Proj_Type_ID)
                };
                    break;
                case eLoadSp.DELETE_PAYMENT_SUB_CAT:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PAYMENT_SUB_ID", this.PAYMENT_SUB_ID)
                };
                    break;
                case eLoadSp.DELETE_PAYMENT_TERMS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PAYMENT_ID", this.PAYMENT_ID)
                };
                    break;
                case eLoadSp.DELETE_PAYMENT_TOWARDS:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PAYMENT_TOWARDS_ID", this.PAYMENT_TOWARDS_ID)
                };
                    break;
                case eLoadSp.DELETE_Department_Name:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Letter_ID_RecFrDept", this.ID_DocumentName)
                };
                    break;
                case eLoadSp.DELETE_COMPANY:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Company_Code", this.CompanyCode)
                };
                    break;
                case eLoadSp.DELETE_PROJECT_FROM_LIST:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Proj_Code)
                };
                    break;
                case eLoadSp.CHECK_PROJECTNAME_DUPLICATE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Name", this.Project_Name)
                };
                    break;
                case eLoadSp.SELECT_PROJECT_BY_PROJECT_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Type_ID", this.Proj_Type_ID)
                };
                    break;
                case eLoadSp.SELECT_PAYMENT_TERMS_BY_PAYMENTTERMS_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PAYMENT_ID", this.PAYMENT_ID)
                };
                    break;
                case eLoadSp.SELECT_PAYMENT_SUB_CATGORY_BY_PAYMENTTERMS_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PAYMENT_SUB_ID", this.PAYMENT_SUB_ID)
                };
                    break;
                case eLoadSp.SELECT_PAYMENT_CATGORY_TOWARDS_BY_PAYMENT_TOWARDS_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PAYMENT_TOWARDS_ID", this.PAYMENT_TOWARDS_ID)
                };
                    break;
                case eLoadSp.SELECT_PROJECT_BY_Project_Code:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.Project_Code)
                };
                    break;
                case eLoadSp.SELECT_COMPANY_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Company_Code", this.CompanyCode)
                };
                    break;
                case eLoadSp.SELECT_PROJECT_FILE_ALL:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@Project_Code", this.Proj_Code)
                };
                    break;
                case eLoadSp.DELETE_PROJECT_FILE:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Task", this.Task),
                    new SqlParameter("@ProjectFile_ID", this.ProjectFile_ID)
                };
                    break;

                case eLoadSp.GENERATE_LETTER_ID:
                    colParams = new SqlParameter[]
               {

                    new SqlParameter("@ProjectCode", this.ProjectCode),
               };
                    break;
                case eLoadSp.GENERATE_LETTER_ID_letRecFrom_Dept:
                    colParams = new SqlParameter[]
               {

                    new SqlParameter("@ProjectCode", this.ProjectCode),
               };
                    break;

                //Start:added by prashanth
                case eLoadSp.UPDATE_FreshLetter_letRecFrom_Dept:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Project_Code", this.ProjectCode ??  (object)DBNull.Value)
                };
                    break;

                case eLoadSp.SELECT_FRESHLETTER_DETAILS_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Letter_ID", this.Letter_ID ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_FRESHLETTER_DEPT_DETAILS_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@Letter_ID", this.Letter_ID ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_REPLYTOLETTER_DETAILS_BY_ID:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@ReplayToLetter_ID_RTL", this.ReplayToLetter_ID_RTL ??  (object)DBNull.Value)
                };
                    break;

                case eLoadSp.SELECT_PAYMENT_CATAGORY_DROPDOWN:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PAYMENT_ID", this.PAYMENT_ID ??  (object)DBNull.Value)
                };
                    break;
                case eLoadSp.SELECT_PAYMENT_SUBCATAGORY_DROPDOWN:
                    colParams = new SqlParameter[]
                {
                    new SqlParameter("@PAYMENT_SUB_ID", this.PAYMENT_SUB_ID ??  (object)DBNull.Value)
                };
                    break;
                    //End:added by prasahnth

            }

            return colParams;
        }

        public bool Contactinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Contactinsert(SqlConn, null, enumSpName);
        }

        public bool SiteLocationinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return SiteLocationinsert(SqlConn, null, enumSpName);
        }
        private bool SiteLocationinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Proj_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Proj_Code", DataRowVersion.Current, this.Proj_Code ?? (object)DBNull.Value ),
                new SqlParameter("@SiteContactPerson", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "SiteContactPerson", DataRowVersion.Current, this.SiteContactPerson ?? (object)DBNull.Value ),
                new SqlParameter("@SiteMobileNumber", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "SiteMobileNumber", DataRowVersion.Current, this.SiteMobileNumber ?? (object)DBNull.Value ),
                new SqlParameter("@SiteAddress", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "SiteAddress", DataRowVersion.Current, this.SiteAddress ?? (object)DBNull.Value ),

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
        private bool Contactinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Proj_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Proj_Code", DataRowVersion.Current, this.Proj_Code ?? (object)DBNull.Value ),
                new SqlParameter("@Cont_Type", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, "Cont_Type", DataRowVersion.Current, this.Cont_Type ?? (object)DBNull.Value ),
                new SqlParameter("@Name", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Name", DataRowVersion.Current, this.Name ?? (object)DBNull.Value ),
                new SqlParameter("@Department", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Department", DataRowVersion.Current, this.Department ?? (object)DBNull.Value ),
                new SqlParameter("@Designation", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Designation", DataRowVersion.Current, this.Designation ?? (object)DBNull.Value ),
                new SqlParameter("@Cont_No", SqlDbType.VarChar, 30, ParameterDirection.Input, false, 0, 0, "Cont_No", DataRowVersion.Current, this.Cont_No ?? (object)DBNull.Value ),
                new SqlParameter("@Email_ID", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Email_ID", DataRowVersion.Current, this.Email_ID ?? (object)DBNull.Value ),
                new SqlParameter("@Dispatch_Add", SqlDbType.VarChar, 2000, ParameterDirection.Input, false, 0, 0, "Dispatch_Add", DataRowVersion.Current, this.Dispatch_Add ?? (object)DBNull.Value ),


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
        public bool ProjectTypeinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ProjectTypeinsert(SqlConn, null, enumSpName);
        }

        public bool PaymentTerminsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return PaymentTerminsert(SqlConn, null, enumSpName);
        }
        public bool PaymentTermSubinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return PaymentTermSubinsert(SqlConn, null, enumSpName);
        }

        public bool PaymentcatTowardsinsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return PaymentcatTowardsinsert(SqlConn, null, enumSpName);
        }
        public bool FreshLetter_insert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return FreshLetter_insert(SqlConn, null, enumSpName);
        }

        public bool FreshLetter_insert_letRecFrom_Dept(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return FreshLetter_insert_letRecFrom_Dept(SqlConn, null, enumSpName);
        }
        //Start:Added by prashanth
        public bool FreshLetter_update_letRecFrom_Dept(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return FreshLetter_update_letRecFrom_Dept(SqlConn, null, enumSpName);
        }
        //End: added by prashanth

        public bool Variations_Price_Adjustments(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Variations_Price_Adjustments(SqlConn, null, enumSpName);
        }
        public bool Insert_BG(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Insert_BG(SqlConn, null, enumSpName);
        }
        public bool Insert_Insurance(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Insert_Insurance(SqlConn, null, enumSpName);
        }
        public bool Vendor_Credentials(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Vendor_Credentials(SqlConn, null, enumSpName);
        }
        public bool Contract_Agreement(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Contract_Agreement(SqlConn, null, enumSpName);
        }
        public bool Bill_of_Quantity(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Bill_of_Quantity(SqlConn, null, enumSpName);
        }
        public bool License(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return License(SqlConn, null, enumSpName);
        }
        public bool BG_Extension(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return BG_Extension(SqlConn, null, enumSpName);
        }
        public bool Insurance_Renewal(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Insurance_Renewal(SqlConn, null, enumSpName);
        }
        public bool Extension_License(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Extension_License(SqlConn, null, enumSpName);
        }
        public bool Drawings(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Drawings(SqlConn, null, enumSpName);
        }
        public bool ReplayToLetter_insert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ReplayToLetter_insert(SqlConn, null, enumSpName);
        }

        public bool ReplayToLetter_insert_letRecFrom_Dept(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ReplayToLetter_insert_letRecFrom_Dept(SqlConn, null, enumSpName);
        }

        private bool ReplayToLetter_insert_letRecFrom_Dept(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@ReplayToLetter_ID_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ReplayToLetter_ID_RTL", DataRowVersion.Current, this.ReplayToLetter_ID_RTL_RecFrDept ?? (object)DBNull.Value ),
                new SqlParameter("@LetterSentTo_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterSentTo_RTL", DataRowVersion.Current, this.LetterSentTo_RTL_RecFrDept ?? (object)DBNull.Value ),
               new SqlParameter("@Date_RTL", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "Date_RTL", DataRowVersion.Current, this.Date_RTL_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@LetterRefNumber_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber_RTL", DataRowVersion.Current, this.LetterRefNumber_RTL_RecFrDept ?? (object)DBNull.Value ),
                  new SqlParameter("@ModeofDispatch_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofDispatch_RTL", DataRowVersion.Current, this.ModeofDispatch_RTL_RecFrDept ?? (object)DBNull.Value ),
                   new SqlParameter("@LetterCopy_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterCopy_RTL", DataRowVersion.Current, this.LetterCopy_RTL_RecFrDept ?? (object)DBNull.Value ),
                    new SqlParameter("@AcknowledgementCopy_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "AcknowledgementCopy_RTL", DataRowVersion.Current, this.AcknowledgementCopy_RTL_RecFrDept ?? (object)DBNull.Value ),
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

        private bool Extension_License(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@License_Extension", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "License_Extension", DataRowVersion.Current, this.License_Extension ?? (object)DBNull.Value ),
                new SqlParameter("@License_Valid_Upto", SqlDbType.Date, 500, ParameterDirection.Input, false, 0, 0, "License_Valid_Upto", DataRowVersion.Current, this.License_Valid_Upto ?? (object)DBNull.Value ),
                new SqlParameter("@License_Extension_Copy", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "License_Extension_Copy", DataRowVersion.Current, this.License_Extension_Copy ?? (object)DBNull.Value ),
                new SqlParameter("@License_ID", SqlDbType.Int, 500, ParameterDirection.Input, false, 0, 0, "License_ID", DataRowVersion.Current, this.License_ID),

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
        private bool Insurance_Renewal(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Insurance_Name", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "Insurance_Name", DataRowVersion.Current, this.Insurance_Name ?? (object)DBNull.Value ),
                new SqlParameter("@Insurance_Renewal_Valid_Upto", SqlDbType.Date, 500, ParameterDirection.Input, false, 0, 0, "Insurance_Renewal_Valid_Upto", DataRowVersion.Current, this.Insurance_Renewal_Valid_Upto ?? (object)DBNull.Value ),
                new SqlParameter("@Insurance_Renewal_Copy", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "Insurance_Renewal_Copy", DataRowVersion.Current, this.Insurance_Renewal_Copy ?? (object)DBNull.Value ),
                new SqlParameter("@Insurance_ID", SqlDbType.Int, 500, ParameterDirection.Input, false, 0, 0, "Insurance_ID", DataRowVersion.Current, this.Insurance_ID),

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
        private bool BG_Extension(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@BGExtension", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "BGExtension", DataRowVersion.Current, this.BGExtension ?? (object)DBNull.Value ),
                new SqlParameter("@Valid_Upto", SqlDbType.Date, 500, ParameterDirection.Input, false, 0, 0, "Valid_Upto", DataRowVersion.Current, this.Valid_Upto ?? (object)DBNull.Value ),
                new SqlParameter("@Extension_Copy", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "Extension_Copy", DataRowVersion.Current, this.Extension_Copy ?? (object)DBNull.Value ),
                new SqlParameter("@BG_ID", SqlDbType.Int, 500, ParameterDirection.Input, false, 0, 0, "BG_ID", DataRowVersion.Current, this.BG_ID),

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
        private bool License(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@License_Type", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "License_Type", DataRowVersion.Current, this.License_Type ?? (object)DBNull.Value ),
                new SqlParameter("@Issuer_Name", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "Issuer_Name", DataRowVersion.Current, this.Issuer_Name ?? (object)DBNull.Value ),
                new SqlParameter("@License_Number", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "License_Number", DataRowVersion.Current, this.License_Number ?? (object)DBNull.Value ),
                new SqlParameter("@Amount", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "Amount", DataRowVersion.Current, this.Amount ?? (object)DBNull.Value ),
                new SqlParameter("@License_Date", SqlDbType.Date, 500, ParameterDirection.Input, false, 0, 0, "License_Date", DataRowVersion.Current, this.License_Date ?? (object)DBNull.Value ),
                 new SqlParameter("@Original_Expiry_Date", SqlDbType.Date, 500, ParameterDirection.Input, false, 0, 0, "Original_Expiry_Date", DataRowVersion.Current, this.Original_Expiry_Date ?? (object)DBNull.Value ),
                 new SqlParameter("@License_Copy_FilePath", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "License_Copy_FilePath", DataRowVersion.Current, this.License_Copy_FilePath ?? (object)DBNull.Value ),
                 new SqlParameter("@Extension", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "Extension", DataRowVersion.Current, this.Extension ?? (object)DBNull.Value ),
                 new SqlParameter("@Valid_upto", SqlDbType.Date, 500, ParameterDirection.Input, false, 0, 0, "Valid_upto", DataRowVersion.Current, this.Valid_upto ?? (object)DBNull.Value ),
                 new SqlParameter("@Upload_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ExtenxionFilePath", DataRowVersion.Current, this.ExtenxionFilePath ?? (object)DBNull.Value ),
                 new SqlParameter("@Project_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ?? (object)DBNull.Value ),
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
        private bool ReplayToLetter_insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@ReplayToLetter_ID_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ReplayToLetter_ID_RTL", DataRowVersion.Current, this.ReplayToLetter_ID_RTL ?? (object)DBNull.Value ),
                new SqlParameter("@LetterSentTo_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterSentTo_RTL", DataRowVersion.Current, this.LetterSentTo_RTL ?? (object)DBNull.Value ),
               new SqlParameter("@Date_RTL", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "Date_RTL", DataRowVersion.Current, this.Date_RTL ?? (object)DBNull.Value ),
                 new SqlParameter("@LetterRefNumber_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber_RTL", DataRowVersion.Current, this.LetterRefNumber_RTL ?? (object)DBNull.Value ),
                  new SqlParameter("@ModeofDispatch_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofDispatch_RTL", DataRowVersion.Current, this.ModeofDispatch_RTL ?? (object)DBNull.Value ),
                   new SqlParameter("@LetterCopy_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterCopy_RTL", DataRowVersion.Current, this.LetterCopy_RTL ?? (object)DBNull.Value ),
                    new SqlParameter("@AcknowledgementCopy_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "AcknowledgementCopy_RTL", DataRowVersion.Current, this.AcknowledgementCopy_RTL ?? (object)DBNull.Value ),
                     new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode ?? (object)DBNull.Value ),
                       new SqlParameter("@Subject", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Subject", DataRowVersion.Current, this.Subject ),
                      //Changes Start:written by Prashanth G
                new SqlParameter("@CC1", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "CC1", DataRowVersion.Current, this.CC1 ?? (object)DBNull.Value ),
                       new SqlParameter("@CC3", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "CC3", DataRowVersion.Current, this.CC3 ?? (object)DBNull.Value ),
                       new SqlParameter("@CC4", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "CC4", DataRowVersion.Current, this.CC4 ?? (object)DBNull.Value ),
                       new SqlParameter("@CC5", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "CC5", DataRowVersion.Current, this.CC5 ?? (object)DBNull.Value ),
                       new SqlParameter("@CC2", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "CC2", DataRowVersion.Current, this.CC2 ?? (object)DBNull.Value ),
                    //Changes End:written by Prashanth G
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

        private bool Variations_Price_Adjustments(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Filename_Variations", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Filename_Variations", DataRowVersion.Current, this.Filename_Variations ?? (object)DBNull.Value ),
                new SqlParameter("@Variations_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Variations_FilePath", DataRowVersion.Current, this.Variations_FilePath ?? (object)DBNull.Value ),
                new SqlParameter("@ApprovalDocuments_Variations", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ApprovalDocuments_Variations", DataRowVersion.Current, this.ApprovalDocumentVariations ?? (object)DBNull.Value ),
                new SqlParameter("@ApprovalDocumentVariations_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ApprovalDocumentVariations_FilePath", DataRowVersion.Current, this.ApprovalDocumentVariations_FilePath ?? (object)DBNull.Value ),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode ?? (object)DBNull.Value ),
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

        private bool Insert_BG(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@BG_Type", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "BG_Beificiary", DataRowVersion.Current, this.BG_Type ?? (object)DBNull.Value ),
                new SqlParameter("@BG_Beificiary", SqlDbType.NVarChar, 500, ParameterDirection.Input, false, 0, 0, "BG_Beificiary", DataRowVersion.Current, this.BG_Beificiary ?? (object)DBNull.Value ),
                new SqlParameter("@BG_Number", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "BG_Number", DataRowVersion.Current, this.BG_Number ?? (object)DBNull.Value ),
                 new SqlParameter("@BG_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "BG_Amount", DataRowVersion.Current, this.BG_Amount ),
                  new SqlParameter("@BG_Date", SqlDbType.Date, 12, ParameterDirection.Input, false, 0, 0, "BG_Date", DataRowVersion.Current, this.BG_Date ),
                   new SqlParameter("@Original_Expiry_Date", SqlDbType.Date, 12, ParameterDirection.Input, false, 0, 0, "Original_Expiry_Date", DataRowVersion.Current, this.Original_Expiry_Date ),
                    new SqlParameter("@Claim_Date", SqlDbType.Date, 12, ParameterDirection.Input, false, 0, 0, "Claim_Date", DataRowVersion.Current, this.Claim_Date ),
                      new SqlParameter("@BG_Copy_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "BG_Copy_FilePath", DataRowVersion.Current, this.BG_Copy_FilePath ),
                        new SqlParameter("@Project_Code", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ),
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

        private bool Insert_Insurance(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Insurance_Type", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Insurance_Type", DataRowVersion.Current, this.Insurance_Type ?? (object)DBNull.Value ),
                new SqlParameter("@Insurer_Name", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Insurer_Name", DataRowVersion.Current, this.Insurer_Name ?? (object)DBNull.Value ),
                new SqlParameter("@Policy_Number", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Policy_Number", DataRowVersion.Current, this.Policy_Number ?? (object)DBNull.Value ),
                new SqlParameter("@Policy_Amount", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Policy_Amount", DataRowVersion.Current, this.Policy_Amount  ),
                new SqlParameter("@Policy_Date", SqlDbType.DateTime, 1000, ParameterDirection.Input, false, 0, 0, "BG_Date", DataRowVersion.Current, this.Policy_Date  ),
                  new SqlParameter("@Insurance_Original_Expiry_Date", SqlDbType.DateTime, 1000, ParameterDirection.Input, false, 0, 0, "Insurance_Original_Expiry_Date", DataRowVersion.Current, this.Insurance_Original_Expiry_Date  ),

                       new SqlParameter("@Insurance_Copy_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Insurance_Copy_FilePath", DataRowVersion.Current, this.Insurance_Copy_FilePath  ),
                      new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code  ),
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
        private bool Drawings(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Filename_Drawings", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Filename_Drawings", DataRowVersion.Current, this.Filename_Drawings ?? (object)DBNull.Value ),
                new SqlParameter("@Drawings_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Drawings_FilePath", DataRowVersion.Current, this.Drawings_FilePath ?? (object)DBNull.Value ),
                 new SqlParameter("@ApprovalDocumentsDrawings", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ApprovalDocumentsDrawings", DataRowVersion.Current, this.ApprovalDocumentsDrawings ?? (object)DBNull.Value ),
                  new SqlParameter("@ApprovalDocumentsDrawings_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ApprovalDocumentsDrawings_FilePath", DataRowVersion.Current, this.ApprovalDocumentsDrawings_FilePath ?? (object)DBNull.Value ),
                 new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode ?? (object)DBNull.Value ),

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

        private bool Bill_of_Quantity(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Filename_BillofQuantity", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Filename_BillofQuantity", DataRowVersion.Current, this.Filename_BillofQuantity ?? (object)DBNull.Value ),
                new SqlParameter("@BillofQuantity_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "BillofQuantity_FilePath", DataRowVersion.Current, this.BillofQuantity_FilePath ?? (object)DBNull.Value ),
                 new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode ?? (object)DBNull.Value ),
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
        private bool Contract_Agreement(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Filename_Contract_Agreement", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Filename_Contract_Agreement", DataRowVersion.Current, this.Filename_ContractAgreement ?? (object)DBNull.Value ),
                new SqlParameter("@Contract_Agreement_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Contract_Agreement_FilePath", DataRowVersion.Current, this.ContractAgreement_FilePath ?? (object)DBNull.Value ),
                 new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode ?? (object)DBNull.Value ),
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
        private bool Vendor_Credentials(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Filename_Vendor_Credentials", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Filename_Vendor_Credentials", DataRowVersion.Current, this.Filename_Vendor_Credentials ?? (object)DBNull.Value ),
                new SqlParameter("@Vendor_Credentials_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Vendor_Credentials_FilePath ", DataRowVersion.Current, this.Vendor_Credentials_FilePath ?? (object)DBNull.Value ),
                  new SqlParameter("@ApprovalDocumentsCredentialsApproval", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ApprovalDocumentsCredentialsApproval ", DataRowVersion.Current, this.ApprovalDocumentsCredentialsApproval ?? (object)DBNull.Value ),
                    new SqlParameter("@ApprovalDocumentsCredentialsApproval_FilePath", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ApprovalDocumentsCredentialsApproval_FilePath ", DataRowVersion.Current, this.ApprovalDocumentsCredentialsApproval_FilePath ?? (object)DBNull.Value ),
                 new SqlParameter("@Project_Code", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode ?? (object)DBNull.Value ),
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
        private bool FreshLetter_insert_letRecFrom_Dept(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

                new SqlParameter("@LetterRefNumber", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber", DataRowVersion.Current, this.LetterRefNumber_RecFrDept ?? (object)DBNull.Value ),
               new SqlParameter("@LetterRecdFrom", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRecdFrom", DataRowVersion.Current, this.LetterRecdFrom_RecFrDept ?? (object)DBNull.Value ),
               new SqlParameter("@LetterReceivedDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "LetterReceivedDate", DataRowVersion.Current, this.LetterReceivedDate_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@DateofReceipt", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "DateofReceipt", DataRowVersion.Current, this.DateofReceipt_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@ReplyByDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "ReplyByDate", DataRowVersion.Current, this.ReplyByDate_RecFrDept ?? (object)DBNull.Value ),
                new SqlParameter("@ModeofRecepit", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofRecepit", DataRowVersion.Current, this.ModeofRecepit_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@Copyof_Fresh_Letter_Path", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Copyof_Fresh_Letter_Path", DataRowVersion.Current, this.Copyof_Fresh_Letter_Path_RecFrDept ?? (object)DBNull.Value ),
                new SqlParameter("@FreshLetterSubject", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "FreshLetterSubject", DataRowVersion.Current, this.FreshLetterSubject_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@Project_Code", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode ?? (object)DBNull.Value )

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
        //start:added by prashanth
        private bool FreshLetter_update_letRecFrom_Dept(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

                new SqlParameter("@LetterRefNumber", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber", DataRowVersion.Current, this.LetterRefNumber_RecFrDept ?? (object)DBNull.Value ),
               new SqlParameter("@LetterRecdFrom", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRecdFrom", DataRowVersion.Current, this.LetterRecdFrom_RecFrDept ?? (object)DBNull.Value ),
               new SqlParameter("@LetterReceivedDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "LetterReceivedDate", DataRowVersion.Current, this.LetterReceivedDate_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@DateofReceipt", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "DateofReceipt", DataRowVersion.Current, this.DateofReceipt_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@ReplyByDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "ReplyByDate", DataRowVersion.Current, this.ReplyByDate_RecFrDept ?? (object)DBNull.Value ),
                new SqlParameter("@ModeofRecepit", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofRecepit", DataRowVersion.Current, this.ModeofRecepit_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@Copyof_Fresh_Letter_Path", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Copyof_Fresh_Letter_Path", DataRowVersion.Current, this.Copyof_Fresh_Letter_Path_RecFrDept ?? (object)DBNull.Value ),
                new SqlParameter("@FreshLetterSubject", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "FreshLetterSubject", DataRowVersion.Current, this.FreshLetterSubject_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@Letter_ID", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Letter_ID", DataRowVersion.Current, this.Letter_ID ?? (object)DBNull.Value )

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
        //End:added by prashanth
        private bool FreshLetter_insert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {

                new SqlParameter("@LetterRefNumber", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber", DataRowVersion.Current, this.LetterRefNumber ?? (object)DBNull.Value ),
               new SqlParameter("@LetterRecdFrom", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRecdFrom", DataRowVersion.Current, this.LetterRecdFrom ?? (object)DBNull.Value ),
               new SqlParameter("@LetterReceivedDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "LetterReceivedDate", DataRowVersion.Current, this.LetterReceivedDate ?? (object)DBNull.Value ),
                 new SqlParameter("@DateofReceipt", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "DateofReceipt", DataRowVersion.Current, this.DateofReceipt ?? (object)DBNull.Value ),
                 new SqlParameter("@ReplyByDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "ReplyByDate", DataRowVersion.Current, this.ReplyByDate ?? (object)DBNull.Value ),
                new SqlParameter("@ModeofRecepit", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofRecepit", DataRowVersion.Current, this.ModeofRecepit ?? (object)DBNull.Value ),
                 new SqlParameter("@Copyof_Fresh_Letter_Path", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Copyof_Fresh_Letter_Path", DataRowVersion.Current, this.Copyof_Fresh_Letter_Path ?? (object)DBNull.Value ),
                  new SqlParameter("@Project_Code", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ?? (object)DBNull.Value ),
                new SqlParameter("@FreshLetterSubject", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "FreshLetterSubject", DataRowVersion.Current, this.FreshLetterSubject ?? (object)DBNull.Value ),
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
        private bool ProjectTypeinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Proj_Type", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Proj_Type", DataRowVersion.Current, this.ProjectTypeName ?? (object)DBNull.Value ),
                new SqlParameter("@Proj_Type_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Proj_Type_Code", DataRowVersion.Current, this.ProjectTypeCode ?? (object)DBNull.Value ),
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


        private bool PaymentTerminsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
               // new SqlParameter("@PAYMENT_TERMS", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "PAYMENT_TERMS", DataRowVersion.Current, this.txtpaymentcat ?? (object)DBNull.Value ),
                new SqlParameter("@PAYMENT_TERMS", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "PAYMENT_TERMS", DataRowVersion.Current, this.txtpaymentcat ?? (object)DBNull.Value ),
                //new SqlParameter("@PAYMENT_TERMS", SqlDbType.VarChar, 300, ParameterDirection.Input, false, 0, 0, "PAYMENT_TERMS", DataRowVersion.Current, this.txtpaymentcat),
                
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

        private bool PaymentTermSubinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
               // new SqlParameter("@PAYMENT_TERMS", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "PAYMENT_TERMS", DataRowVersion.Current, this.txtpaymentcat ?? (object)DBNull.Value ),
                new SqlParameter("@PAYMENT_CATAGORY_SUB", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "PAYMENT_CATAGORY_SUB", DataRowVersion.Current, this.txtpaymentsubcat ?? (object)DBNull.Value ),
                new SqlParameter("@PAYMENT_ID", SqlDbType.Int, 30, ParameterDirection.Input, false, 0, 0, "PAYMENT_ID", DataRowVersion.Current, this.paymentddl),

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

        private bool PaymentcatTowardsinsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
               // new SqlParameter("@PAYMENT_TERMS", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "PAYMENT_TERMS", DataRowVersion.Current, this.txtpaymentcat ?? (object)DBNull.Value ),
                new SqlParameter("@PAYMENT_CATAGORY_TOWARDS", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "PAYMENT_CATAGORY_TOWARDS", DataRowVersion.Current, this.txtpaymentTowards ?? (object)DBNull.Value ),
                new SqlParameter("@PAYMENT_SUB_ID", SqlDbType.Int, 30, ParameterDirection.Input, false, 0, 0, "PAYMENT_SUB_ID", DataRowVersion.Current, this.paymentsubddl),

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

        public bool FreshLetterupdate_letRecFrom_Dept(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return FreshLetterupdate_letRecFrom_Dept(SqlConn, null, enumSpName);
        }
        public bool FreshLetterupdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return FreshLetterupdate(SqlConn, null, enumSpName);
        }
        public bool FreshLetterupdateModal(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return FreshLetterupdateModal(SqlConn, null, enumSpName);
        }
        public bool LicenseTypeInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return LicenseTypeInsert(SqlConn, null, enumSpName);
        }
        public bool BGTypeInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return BGTypeInsert(SqlConn, null, enumSpName);
        }
        public bool InsuranceTypeInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return InsuranceTypeInsert(SqlConn, null, enumSpName);
        }
        public bool ReplayToLetterupdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ReplayToLetterupdate(SqlConn, null, enumSpName);
        }
        //Strat:Prashanth
        public bool ReplayToLetterupdateModal(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ReplayToLetterupdateModal(SqlConn, null, enumSpName);
        }
        //End:Prashanth
        public bool ReplayToLetterupdate_letRecFrom_Dept(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ReplayToLetterupdate_letRecFrom_Dept(SqlConn, null, enumSpName);
        }
        public bool loadLetterID(SqlConnection SqlConn, eLoadSp enumSpName, ref DataSet ds)
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


        public bool ProjectTypeupdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return ProjectTypeupdate(SqlConn, null, enumSpName);
        }

        public bool Paymentcat(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Paymentcat(SqlConn, null, enumSpName);
        }

        public bool Paymentcatsub(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Paymentcatsub(SqlConn, null, enumSpName);
        }

        public bool Paymentcattowards(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return Paymentcattowards(SqlConn, null, enumSpName);
        }
        public bool LicenseTypeUpdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return LicenseTypeUpdate(SqlConn, null, enumSpName);
        }
        public bool InsuranceTypeUpdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return InsuranceTypeUpdate(SqlConn, null, enumSpName);
        }
        public bool BGTypeUpdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return BGTypeUpdate(SqlConn, null, enumSpName);
        }

        private bool Paymentcat(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PAYMENT_TERMS", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "PAYMENT_TERMS", DataRowVersion.Current, this.txtpaymentcat ?? (object)DBNull.Value ),
                new SqlParameter("@PAYMENT_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PAYMENT_ID", DataRowVersion.Current, this.PAYMENT_ID),
               // new SqlParameter("@Proj_Type_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Proj_Type_Code", DataRowVersion.Current, this.ProjectTypeCode ?? (object)DBNull.Value ),

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

        private bool Paymentcatsub(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PAYMENT_CATAGORY_SUB", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "PAYMENT_CATAGORY_SUB", DataRowVersion.Current, this.txtpaymentsubcat ?? (object)DBNull.Value ),
                new SqlParameter("@PAYMENT_SUB_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PAYMENT_SUB_ID", DataRowVersion.Current, this.PAYMENT_SUB_ID),
               // new SqlParameter("@Proj_Type_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Proj_Type_Code", DataRowVersion.Current, this.ProjectTypeCode ?? (object)DBNull.Value ),

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

        private bool Paymentcattowards(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@PAYMENT_CATAGORY_TOWARDS", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "PAYMENT_CATAGORY_TOWARDS", DataRowVersion.Current, this.txtpaymentTowards ?? (object)DBNull.Value ),
                new SqlParameter("@PAYMENT_TOWARDS_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PAYMENT_TOWARDS_ID", DataRowVersion.Current, this.PAYMENT_TOWARDS_ID),
               // new SqlParameter("@Proj_Type_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Proj_Type_Code", DataRowVersion.Current, this.ProjectTypeCode ?? (object)DBNull.Value ),

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
        private bool ProjectTypeupdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Proj_Type", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Proj_Type", DataRowVersion.Current, this.ProjectTypeName ?? (object)DBNull.Value ),
                new SqlParameter("@Proj_Type_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Proj_Type_ID", DataRowVersion.Current, this.Proj_Type_ID),
                new SqlParameter("@Proj_Type_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Proj_Type_Code", DataRowVersion.Current, this.ProjectTypeCode ?? (object)DBNull.Value ),

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

        private bool InsuranceTypeInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Insurance_Type", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Insurance_Type", DataRowVersion.Current, this.AddInsuranceType ?? (object)DBNull.Value ),
                new SqlParameter("@Project_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ?? (object)DBNull.Value ),
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
        private bool BGTypeInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@BG_Type", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "BG_Type", DataRowVersion.Current, this.AddBGtype ?? (object)DBNull.Value ),
                new SqlParameter("@Project_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code ?? (object)DBNull.Value ),
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
        private bool LicenseTypeInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@License_Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "License_Name", DataRowVersion.Current, this.LicenseName ?? (object)DBNull.Value ),
                new SqlParameter("@License_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "License_Code", DataRowVersion.Current, this.LicenseCode ?? (object)DBNull.Value ),
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
        private bool LetterID(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                 new SqlParameter("@ProjectCode", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "ProjectCode", DataRowVersion.Current, this.ProjectCode ?? (object)DBNull.Value ),

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

        private bool BGTypeUpdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@BG_Type", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "BG_Type", DataRowVersion.Current, this.AddBGtype ?? (object)DBNull.Value ),
                new SqlParameter("@Project_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code),

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
        private bool InsuranceTypeUpdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Insurance_Type", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Insurance_Type", DataRowVersion.Current, this.AddInsuranceType ?? (object)DBNull.Value ),
                new SqlParameter("@Proj_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Proj_Code", DataRowVersion.Current, this.Proj_Code),

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
        private bool LicenseTypeUpdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@License_Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "License_Name", DataRowVersion.Current, this.LicenseName ?? (object)DBNull.Value ),
                new SqlParameter("@License_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "License_Code", DataRowVersion.Current, this.LicenseCode),

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
        private bool FreshLetterupdate_letRecFrom_Dept(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                 new SqlParameter("@Letter_ID", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Letter_ID", DataRowVersion.Current, this.LetterCopy_RTL_RecFrDept ?? (object)DBNull.Value ),
                new SqlParameter("@LetterRefNumber", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber", DataRowVersion.Current, this.LetterCopy_RTL_RecFrDept ?? (object)DBNull.Value ),
               new SqlParameter("@LetterRecdFrom", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRecdFrom", DataRowVersion.Current, this.LetterRecdFrom_RecFrDept ?? (object)DBNull.Value ),
               new SqlParameter("@LetterReceivedDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "LetterReceivedDate", DataRowVersion.Current, this.LetterReceivedDate_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@DateofReceipt", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "DateofReceipt", DataRowVersion.Current, this.DateofReceipt_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@ReplyByDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "ReplyByDate", DataRowVersion.Current, this.ReplyByDate_RecFrDept ?? (object)DBNull.Value ),
                new SqlParameter("@ModeofRecepit", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofRecepit", DataRowVersion.Current, this.ModeofRecepit_RecFrDept ?? (object)DBNull.Value ),
                 new SqlParameter("@Copyof_Fresh_Letter_Path", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Copyof_Fresh_Letter_Path", DataRowVersion.Current, this.Copyof_Fresh_Letter_Path_RecFrDept ?? (object)DBNull.Value ),
                new SqlParameter("@FreshLetterSubject", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "FreshLetterSubject", DataRowVersion.Current, this.FreshLetterSubject_RecFrDept ?? (object)DBNull.Value ),
                  new SqlParameter("@Project_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode ?? (object)DBNull.Value ),
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
        private bool FreshLetterupdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                 new SqlParameter("@Letter_ID", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Letter_ID", DataRowVersion.Current, this.Letter_ID ?? (object)DBNull.Value ),
                new SqlParameter("@LetterRefNumber", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber", DataRowVersion.Current, this.LetterRefNumber ?? (object)DBNull.Value ),
               new SqlParameter("@LetterRecdFrom", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRecdFrom", DataRowVersion.Current, this.LetterRecdFrom ?? (object)DBNull.Value ),
               new SqlParameter("@LetterReceivedDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "LetterReceivedDate", DataRowVersion.Current, this.LetterReceivedDate ?? (object)DBNull.Value ),
                 new SqlParameter("@DateofReceipt", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "DateofReceipt", DataRowVersion.Current, this.DateofReceipt ?? (object)DBNull.Value ),
                 new SqlParameter("@ReplyByDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "ReplyByDate", DataRowVersion.Current, this.ReplyByDate ?? (object)DBNull.Value ),
                new SqlParameter("@ModeofRecepit", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofRecepit", DataRowVersion.Current, this.ModeofRecepit ?? (object)DBNull.Value ),
                 new SqlParameter("@Copyof_Fresh_Letter_Path", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Copyof_Fresh_Letter_Path", DataRowVersion.Current, this.Copyof_Fresh_Letter_Path ?? (object)DBNull.Value ),
                new SqlParameter("@FreshLetterSubject", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "FreshLetterSubject", DataRowVersion.Current, this.FreshLetterSubject ?? (object)DBNull.Value ),

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


        private bool FreshLetterupdateModal(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                 new SqlParameter("@Letter_ID", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Letter_ID", DataRowVersion.Current, this.Letter_ID ?? (object)DBNull.Value ),
                new SqlParameter("@LetterRefNumber", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber", DataRowVersion.Current, this.LetterRefNumber ?? (object)DBNull.Value ),
               new SqlParameter("@LetterRecdFrom", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRecdFrom", DataRowVersion.Current, this.LetterRecdFrom ?? (object)DBNull.Value ),
               new SqlParameter("@LetterReceivedDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "LetterReceivedDate", DataRowVersion.Current, this.LetterReceivedDate ?? (object)DBNull.Value ),
                 new SqlParameter("@DateofReceipt", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "DateofReceipt", DataRowVersion.Current, this.DateofReceipt ?? (object)DBNull.Value ),
                 new SqlParameter("@ReplyByDate", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "ReplyByDate", DataRowVersion.Current, this.ReplyByDate ?? (object)DBNull.Value ),
                new SqlParameter("@ModeofRecepit", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofRecepit", DataRowVersion.Current, this.ModeofRecepit ?? (object)DBNull.Value ),
                 new SqlParameter("@Copyof_Fresh_Letter_Path", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Copyof_Fresh_Letter_Path", DataRowVersion.Current, this.Copyof_Fresh_Letter_Path ?? (object)DBNull.Value ),
                new SqlParameter("@FreshLetterSubject", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "FreshLetterSubject", DataRowVersion.Current, this.FreshLetterSubject ?? (object)DBNull.Value ),

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
        private bool ReplayToLetterupdate_letRecFrom_Dept(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                 new SqlParameter("@ReplayToLetter_ID_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ReplayToLetter_ID_RTL", DataRowVersion.Current, this.ReplayToLetter_ID_RTL ?? (object)DBNull.Value ),
                new SqlParameter("@LetterSentTo_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterSentTo_RTL", DataRowVersion.Current, this.LetterSentTo_RTL ?? (object)DBNull.Value ),
               new SqlParameter("@Date_RTL", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "Date_RTL", DataRowVersion.Current, this.Date_RTL ?? (object)DBNull.Value ),
                 new SqlParameter("@LetterRefNumber_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber_RTL", DataRowVersion.Current, this.LetterRefNumber_RTL ?? (object)DBNull.Value ),
                  new SqlParameter("@ModeofDispatch_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofDispatch_RTL", DataRowVersion.Current, this.ModeofDispatch_RTL ?? (object)DBNull.Value ),
                   new SqlParameter("@LetterCopy_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterCopy_RTL", DataRowVersion.Current, this.LetterCopy_RTL ?? (object)DBNull.Value ),
                    new SqlParameter("@AcknowledgementCopy_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "AcknowledgementCopy_RTL", DataRowVersion.Current, this.AcknowledgementCopy_RTL ?? (object)DBNull.Value ),
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
        private bool ReplayToLetterupdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                 new SqlParameter("@ReplayToLetter_ID_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ReplayToLetter_ID_RTL", DataRowVersion.Current, this.ReplayToLetter_ID_RTL ?? (object)DBNull.Value ),
                new SqlParameter("@LetterSentTo_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterSentTo_RTL", DataRowVersion.Current, this.LetterSentTo_RTL ?? (object)DBNull.Value ),
               new SqlParameter("@Date_RTL", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "Date_RTL", DataRowVersion.Current, this.Date_RTL ?? (object)DBNull.Value ),
                 new SqlParameter("@LetterRefNumber_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber_RTL", DataRowVersion.Current, this.LetterRefNumber_RTL ?? (object)DBNull.Value ),
                  new SqlParameter("@ModeofDispatch_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofDispatch_RTL", DataRowVersion.Current, this.ModeofDispatch_RTL ?? (object)DBNull.Value ),
                   new SqlParameter("@LetterCopy_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterCopy_RTL", DataRowVersion.Current, this.LetterCopy_RTL ?? (object)DBNull.Value ),
                    new SqlParameter("@AcknowledgementCopy_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "AcknowledgementCopy_RTL", DataRowVersion.Current, this.AcknowledgementCopy_RTL ?? (object)DBNull.Value ),
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
        // by Prashanth :Start 
        private bool ReplayToLetterupdateModal(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                 new SqlParameter("@ReplayToLetter_ID_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ReplayToLetter_ID_RTL", DataRowVersion.Current, this.ReplayToLetter_ID_RTL ?? (object)DBNull.Value ),
                new SqlParameter("@LetterSentTo_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterSentTo_RTL", DataRowVersion.Current, this.LetterSentTo_RTL ?? (object)DBNull.Value ),
               new SqlParameter("@Date_RTL", SqlDbType.DateTime, 20, ParameterDirection.Input, false, 0, 0, "Date_RTL", DataRowVersion.Current, this.Date_RTL ?? (object)DBNull.Value ),
                 new SqlParameter("@LetterRefNumber_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterRefNumber_RTL", DataRowVersion.Current, this.LetterRefNumber_RTL ?? (object)DBNull.Value ),
                  new SqlParameter("@ModeofDispatch_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "ModeofDispatch_RTL", DataRowVersion.Current, this.ModeofDispatch_RTL ?? (object)DBNull.Value ),
                   new SqlParameter("@LetterCopy_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "LetterCopy_RTL", DataRowVersion.Current, this.LetterCopy_RTL ?? (object)DBNull.Value ),
                    new SqlParameter("@AcknowledgementCopy_RTL", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "AcknowledgementCopy_RTL", DataRowVersion.Current, this.AcknowledgementCopy_RTL ?? (object)DBNull.Value ),
                new SqlParameter("@Subject", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Subject", DataRowVersion.Current, this.@Subject ?? (object)DBNull.Value ),
                new SqlParameter("@CC1", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "CC1", DataRowVersion.Current, this.CC1 ?? (object)DBNull.Value ),
                new SqlParameter("@CC2", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "CC2", DataRowVersion.Current, this.CC2 ?? (object)DBNull.Value ),
                new SqlParameter("@CC3", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "CC3", DataRowVersion.Current, this.CC3 ?? (object)DBNull.Value ),
                new SqlParameter("@CC4", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "CC4", DataRowVersion.Current, this.CC4 ?? (object)DBNull.Value ),
                new SqlParameter("@CC5", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "CC5", DataRowVersion.Current, this.CC5 ?? (object)DBNull.Value ),

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
        //By Prashanth:End
        public bool CompanyInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return CompanyInsert(SqlConn, null, enumSpName);
        }
        public bool LetterRecivedDepartmenInsert(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return LetterRecivedDepartmenInsert(SqlConn, null, enumSpName);
        }
        private bool LetterRecivedDepartmenInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Letter_Recived_Dept", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Letter_Recived_Dept", DataRowVersion.Current, this.Letter_Recived_Dept ?? (object)DBNull.Value ),

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
        private bool CompanyInsert(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Company_Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Company_Name", DataRowVersion.Current, this.CompanyName ?? (object)DBNull.Value ),
                new SqlParameter("@Company_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Company_Code", DataRowVersion.Current, this.CompanyCode ?? (object)DBNull.Value ),
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

        public bool CompanyUpdate(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return CompanyUpdate(SqlConn, null, enumSpName);
        }

        private bool CompanyUpdate(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@Company_Name", SqlDbType.NVarChar, 100, ParameterDirection.Input, false, 0, 0, "Company_Name", DataRowVersion.Current, this.CompanyName ?? (object)DBNull.Value ),
                new SqlParameter("@Company_Code", SqlDbType.NVarChar, 10, ParameterDirection.Input, false, 0, 0, "Company_Code", DataRowVersion.Current, this.CompanyCode),

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
        public bool insertBI(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return insertBI(SqlConn, null, enumSpName);
        }
        private bool insertBI(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intIdentityValue = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@BANK_INDENT_No", SqlDbType.VarChar, 100, ParameterDirection.Output, false, 0, 0, "BANK_INDENT_No", DataRowVersion.Current, this.BANK_INDENT_No),
                new SqlParameter("@BANK_INDENT_ID", SqlDbType.Int,100, ParameterDirection.Output, false, 0, 0, "BANK_INDENT_ID", DataRowVersion.Current, this.BANK_INDENT_ID),
                new SqlParameter("@INDENT_Date", SqlDbType.Date, 100, ParameterDirection.Input, false, 0, 0, "IndentDate", DataRowVersion.Current, this.IndentDate),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode),
                new SqlParameter("@FYear", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "FYear", DataRowVersion.Current, this.FYear),
                new SqlParameter("@BANK_NAME", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "BANK_NAME", DataRowVersion.Current, this.BankName),
                new SqlParameter("@Task", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@ACCOUNT_NUMBER", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "ACCOUNT_NUMBER", DataRowVersion.Current, this.ACCOUNT_NUMBER),
                new SqlParameter("@BRANCH_NAME", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "BRANCH_NAME", DataRowVersion.Current, this.Branch),
                new SqlParameter("@ISFC", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "ISFC", DataRowVersion.Current, this.ISFC),
                new SqlParameter("@Bank", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Bank", DataRowVersion.Current, this.Bank),
                new SqlParameter("@PAYMENT_TYPE", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "PAYMENT_TYPE", DataRowVersion.Current, this.PaymentType),
                new SqlParameter("@PAYMENT_CATAGARY", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "PAYMENT_CATAGARY", DataRowVersion.Current, this.PaymentCatagary),
                new SqlParameter("@PAYMENT_SUB_CATAGARY", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "PAYMENT_SUB_CATAGARY", DataRowVersion.Current, this.PaymentSubCatagary),
                new SqlParameter("@TOWARDS", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "TOWARDS", DataRowVersion.Current, this.Towards),
                new SqlParameter("@REFDETAILS", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "REFDETAILS", DataRowVersion.Current, this.Refdetails),
                new SqlParameter("@NARRATION", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "NARRATION", DataRowVersion.Current, this.Narration),
                new SqlParameter("@RATE_OF_INTEREST", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "RATE_OF_INTEREST", DataRowVersion.Current, this.RateOfInterest),
                new SqlParameter("@AMOUNT", SqlDbType.Decimal,1000, ParameterDirection.Input, false, 0, 0, "AMOUNT", DataRowVersion.Current, this.Amount ?? (object)DBNull.Value),
                new SqlParameter("@Uploaded_File", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Uploaded_File", DataRowVersion.Current, this.Uploaded_File ?? (object)DBNull.Value ),
                new SqlParameter("@StateCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "StateCode", DataRowVersion.Current, this.StateCode),

                new SqlParameter("@CGSTPerc", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "CGSTPerc", DataRowVersion.Current, this.CGSTPerc),
                new SqlParameter("@SGSTPerc", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "SGSTPerc", DataRowVersion.Current, this.SGSTPerc),
                new SqlParameter("@IGSTPerc", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "IGSTPerc", DataRowVersion.Current, this.IGSTPerc),
                new SqlParameter("@SGSTAmt", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "SGSTAmt", DataRowVersion.Current, this.SGSTAmt),
                new SqlParameter("@CGSTAmt", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "CGSTAmt", DataRowVersion.Current, this.CGSTAmt),
                new SqlParameter("@IGSTAmt", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "IGSTAmt", DataRowVersion.Current, this.IGSTAmt)

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));

                    string check;
                    check = colParams[0].Value.ToString();
                    //this.BANK_INDENT_ID = (int)colParams[1].Value;
                    //this.BANK_INDENT_No = (string)colParams.First().Value;
                    this.BANK_INDENT_No = (string)colParams.First().Value;
                    this.BANK_INDENT_ID = (int)colParams[1].Value;
                    //string check;
                    //check = colParams[0].Value.ToString();
                    //this.ProjectCode = (string)colParams.First().Value;
                    //this.Proj_Code = (string)colParams.First().Value;
                    //this.BANK_INDENT_No = (int)colParams[1].Value;

                    string a = (string)colParams.First().Value;
                    int bb = (int)colParams[1].Value;
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

                new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Output, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Proj_Code ),
                new SqlParameter("@Project_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project_Name", DataRowVersion.Current, this.Project_Name  ),
                new SqlParameter("@Type", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "Type", DataRowVersion.Current, this.Type  ),
                new SqlParameter("@State", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "State", DataRowVersion.Current, this.State  ),
                new SqlParameter("@Location", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Location", DataRowVersion.Current, this.Location ),
                new SqlParameter("@Alias_Project", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Alias_Project", DataRowVersion.Current, this.AliasProject  ),
                new SqlParameter("@St_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "StartDate", DataRowVersion.Current, this.StartDate ?? (object)DBNull.Value),
                new SqlParameter("@End_Dt", SqlDbType.Date, 3, ParameterDirection.Input, false, 10, 0, "End_Dt", DataRowVersion.Current, this.End_Dt ?? (object)DBNull.Value ),
                new SqlParameter("@Proj_Cost", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Proj_Cost", DataRowVersion.Current, this.Proj_Cost ?? (object)DBNull.Value),
                new SqlParameter("@Award_By", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "Award_By", DataRowVersion.Current, this.Award_By ?? (object)DBNull.Value),
                new SqlParameter("@Description", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Description", DataRowVersion.Current, this.Description ?? (object)DBNull.Value),
                new SqlParameter("@Status", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "Status", DataRowVersion.Current, this.Status  ),
                new SqlParameter("@E_Agreement_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Date", DataRowVersion.Current, this.E_Agreement_Date),
                new SqlParameter("@Principal_Contractor", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Principal_Contractor", DataRowVersion.Current, this.Principal_Contractor),
                new SqlParameter("@Agreement_Date ", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "Date", DataRowVersion.Current, this.Agreement_Date)

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intIdentityValue = Convert.ToInt32(SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams));

                    this.Proj_Code = (string)colParams.First().Value;
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

        public bool loadPC(SqlConnection SqlConn, eLoadSp enumSpName)
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

                if (fillObjectFromDrBI(dr))
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

        public bool loadPSC(SqlConnection SqlConn, eLoadSp enumSpName)
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

                if (fillObjectFromDrBIS(dr))
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
        public bool loadPCT(SqlConnection SqlConn, eLoadSp enumSpName)
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

                if (fillObjectFromDrBIT(dr))
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

        public bool loadCompany(SqlConnection SqlConn, eLoadSp enumSpName)
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

                if (fillObjectFromDrCompany(dr))
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
        public bool updateBI(SqlConnection SqlConn, eLoadSp enumSpName)
        {
            return updateBI(SqlConn, null, enumSpName);
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

                new SqlParameter("@Project_Code", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.Project_Code  ),
                new SqlParameter("@Project_Name", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project_Name", DataRowVersion.Current, this.Project_Name  ),
                new SqlParameter("@Type", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "Type", DataRowVersion.Current, this.Type  ),
                new SqlParameter("@State", SqlDbType.VarChar, 20, ParameterDirection.Input, false, 0, 0, "State", DataRowVersion.Current, this.State),
                new SqlParameter("@Location", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "Location", DataRowVersion.Current, this.Location ),
                new SqlParameter("@St_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "StartDate", DataRowVersion.Current, this.StartDate ?? (object)DBNull.Value),
                new SqlParameter("@End_Dt", SqlDbType.Date, 3, ParameterDirection.Input, false, 10, 0, "End_Dt", DataRowVersion.Current, this.End_Dt  ?? (object)DBNull.Value),
                new SqlParameter("@Alias_Project", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Alias_Project", DataRowVersion.Current, this.AliasProject),
                new SqlParameter("@Proj_Cost", SqlDbType.Decimal, 9, ParameterDirection.Input, false, 0, 0, "Proj_Cost", DataRowVersion.Current, this.Proj_Cost ?? (object)DBNull.Value),
                new SqlParameter("@Award_By", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Award_By", DataRowVersion.Current, this.Award_By ?? (object)DBNull.Value),
                new SqlParameter("@Description", SqlDbType.VarChar, 500, ParameterDirection.Input, false, 10, 0, "Description", DataRowVersion.Current, this.Description ?? (object)DBNull.Value),
                new SqlParameter("@Status", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 10, 0, "Status", DataRowVersion.Current, this.Status),
                new SqlParameter("@E_Agreement_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "E_Agreement_Date", DataRowVersion.Current, this.E_Agreement_Date  ?? (object)DBNull.Value),
                new SqlParameter("@Agreement_Date", SqlDbType.Date, 3, ParameterDirection.Input, false, 0, 0, "AgreementDate", DataRowVersion.Current, this.Agreement_Date ?? (object)DBNull.Value),
                new SqlParameter("@Principal_Contractor", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Principal_Contractor", DataRowVersion.Current, this.Principal_Contractor),

                new SqlParameter("@File1", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "File1", DataRowVersion.Current, this.File1),
                new SqlParameter("@File2", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "File2", DataRowVersion.Current, this.File2),
                new SqlParameter("@File3", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "File3", DataRowVersion.Current, this.File3),
                new SqlParameter("@File4", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "File4", DataRowVersion.Current, this.File4),
                new SqlParameter("@File5", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "File5", DataRowVersion.Current, this.File5),
                new SqlParameter("@FileName1", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "FileName1", DataRowVersion.Current, this.FileName1),
                new SqlParameter("@FileName2", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "FileName2", DataRowVersion.Current, this.FileName2),
                new SqlParameter("@FileName3", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "FileName3", DataRowVersion.Current, this.FileName3),
                new SqlParameter("@FileName4", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "FileName4", DataRowVersion.Current, this.FileName4),
                new SqlParameter("@FileName5", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "FileName5", DataRowVersion.Current, this.FileName5)

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

        private bool updateBI(SqlConnection SqlConn, SqlTransaction SqlTran, eLoadSp enumSpName)
        {
            try
            {
                int intReturnVal = 0;

                SqlParameter[] colParams = new SqlParameter[]
            {
                new SqlParameter("@BANK_INDENT_No", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "BANK_INDENT_No", DataRowVersion.Current, this.BANK_INDENT_No),
                new SqlParameter("@INDENT_Date", SqlDbType.Date, 100, ParameterDirection.Input, false, 0, 0, "IndentDate", DataRowVersion.Current, this.IndentDate),
                new SqlParameter("@Project_Code", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "Project_Code", DataRowVersion.Current, this.ProjectCode),
                new SqlParameter("@FYear", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 0, 0, "FYear", DataRowVersion.Current, this.FYear),
                new SqlParameter("@BANK_NAME", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "BANK_NAME", DataRowVersion.Current, this.BankName),
                new SqlParameter("@ACCOUNT_NUMBER", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "ACCOUNT_NUMBER", DataRowVersion.Current, this.ACCOUNT_NUMBER),
                new SqlParameter("@BRANCH_NAME", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "BRANCH_NAME", DataRowVersion.Current, this.Branch),
                new SqlParameter("@ISFC", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "ISFC", DataRowVersion.Current, this.ISFC),
                new SqlParameter("@Bank", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "Bank", DataRowVersion.Current, this.Bank),
                new SqlParameter("@PAYMENT_TYPE", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "PAYMENT_TYPE", DataRowVersion.Current, this.PaymentType),
                new SqlParameter("@PAYMENT_CATAGARY", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "PAYMENT_CATAGARY", DataRowVersion.Current, this.PaymentCatagary),
                new SqlParameter("@PAYMENT_SUB_CATAGARY", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "PAYMENT_SUB_CATAGARY", DataRowVersion.Current, this.PaymentSubCatagary),
                new SqlParameter("@TOWARDS", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "TOWARDS", DataRowVersion.Current, this.Towards),
                new SqlParameter("@REFDETAILS", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "REFDETAILS", DataRowVersion.Current, this.Refdetails),
                new SqlParameter("@NARRATION", SqlDbType.VarChar, 8000, ParameterDirection.Input, false, 0, 0, "NARRATION", DataRowVersion.Current, this.Narration),
                new SqlParameter("@RATE_OF_INTEREST", SqlDbType.VarChar, 1000, ParameterDirection.Input, false, 0, 0, "RATE_OF_INTEREST", DataRowVersion.Current, this.RateOfInterest),
                new SqlParameter("@AMOUNT", SqlDbType.Decimal,1000, ParameterDirection.Input, false, 0, 0, "AMOUNT", DataRowVersion.Current, this.Amount ?? (object)DBNull.Value),
                new SqlParameter("@Uploaded_File", SqlDbType.NVarChar, 1000, ParameterDirection.Input, false, 0, 0, "Uploaded_File", DataRowVersion.Current, this.Uploaded_File ?? (object)DBNull.Value ),
                new SqlParameter("@Task", SqlDbType.VarChar, 100, ParameterDirection.Input, false, 10, 0, "Task", DataRowVersion.Current, this.Task),
                new SqlParameter("@StateCode", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "StateCode", DataRowVersion.Current, this.StateCode),

                new SqlParameter("@CGSTPerc", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "CGSTPerc", DataRowVersion.Current, this.CGSTPerc),
                new SqlParameter("@SGSTPerc", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "SGSTPerc", DataRowVersion.Current, this.SGSTPerc),
                new SqlParameter("@IGSTPerc", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "IGSTPerc", DataRowVersion.Current, this.IGSTPerc),
                new SqlParameter("@SGSTAmt", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "SGSTAmt", DataRowVersion.Current, this.SGSTAmt),
                new SqlParameter("@CGSTAmt", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "CGSTAmt", DataRowVersion.Current, this.CGSTAmt),
                new SqlParameter("@IGSTAmt", SqlDbType.Decimal, 500, ParameterDirection.Input, false, 0, 0, "IGSTAmt", DataRowVersion.Current, this.IGSTAmt)

            };

                if (SqlConn != null)
                {
                    if (!validate())
                    {
                        return false;
                    }

                    intReturnVal = SqlHelper.ExecuteNonQuery(SqlConn, CommandType.StoredProcedure, getSpName(enumSpName), colParams);
                    //this.BANK_INDENT_No = (string)colParams.First().Value;
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
        public ProjectBL()
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


    }
    #endregion
}
#endregion