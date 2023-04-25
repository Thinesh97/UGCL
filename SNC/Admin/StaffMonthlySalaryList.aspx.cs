using BusinessLayer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Obout.Grid;
using SNC.ErrorLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SNC.Admin
{
    public partial class StaffMonthlySalaryList : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        PaymentIndentBL objPaymentIndentBL = null;


        DataSet ds = null;
        DataSet Accessds = new DataSet();
        StaffSalariesBL ObjStaffSalariesBL = null;
        private String SalaryNo = "0000";
        private string SalaryCode = "UGCL/EMP/22-23/";
        protected void Page_Load(object sender, EventArgs e)
        {
            try

            {
                if (Session["UID"] != null)
                {
                    if (!IsPostBack)
                    {
                       // BindPaymentIndentList();
                        ddlyear.Items.Add("-Select-");

                        BindYear();
                        // EmployeeContractBindList();
                    }
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindPaymentIndentList()
        {
            try
            {


                ds = new DataSet();

                ObjStaffSalariesBL = new StaffSalariesBL();
                ObjStaffSalariesBL.month = ddlSalarymonth.SelectedValue;
                ObjStaffSalariesBL.year = ddlyear.SelectedItem.ToString();
                ObjStaffSalariesBL.Task = "StaffSalaryListByMonth";
                ObjStaffSalariesBL.Task = "StaffSalaryList";
                ObjStaffSalariesBL.load(con, StaffSalariesBL.eLoadSp.PRO_Staff_Salaries_SELECT_ALL, ref ds);
                Grid_PaymentIndent_Completed.DataSource = ds;
                Grid_PaymentIndent_Completed.DataBind();

                ObjStaffSalariesBL.Task = "StaffSalaryListByStatus";
                ObjStaffSalariesBL.load(con, StaffSalariesBL.eLoadSp.PRO_Staff_Salaries_SELECT_ALL, ref ds);
                GvStatus.DataSource = ds;
                GvStatus.DataBind();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DvStatus.Visible = true;
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }
        protected void BindYear()
        {

            int StartYear = 2010;
            int EndYear = 2040;
            string year = "";

            for (int i = 2010; i < EndYear; i++)
            {
                year = i.ToString();
                ddlyear.Items.Add(year);

            }

        }






        protected void Grid_PaymentIndent_RowDataBound_Completed(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {

                    HyperLink lnkPayIndNo = e.Row.Cells[0].FindControl("lnkPayIndNo") as HyperLink;
                    lnkPayIndNo.NavigateUrl = "~/Procurement/PaymentIndent.aspx?PayInd_No=" + lnkPayIndNo.Text;
                    CheckBox chkSelect = e.Row.Cells[22].FindControl("chkSelect") as CheckBox;

                    CheckBox SelectTemplatePAYIND = e.Row.Cells[6].FindControl("chkSelectTemplatePAYIND") as CheckBox;
                    if (e.Row.Cells[23].Text == "True")
                    {
                        SelectTemplatePAYIND.Enabled = false;
                    }
                    DropDownList ddlCompany_Bank = (e.Row.Cells[15].FindControl("ddlCompany_Bank") as DropDownList);
                    ds = new DataSet();
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.Task = "Select_Bank";
                    objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_BANK, ref ds);
                    ddlCompany_Bank.DataSource = ds;
                    ddlCompany_Bank.DataTextField = "Bank";
                    ddlCompany_Bank.DataBind();
                    ddlCompany_Bank.Items.Insert(0, "-Select-");

                }
            }
            catch (Exception ex)
            {
                //  ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }


        protected void Grid_PaymentIndent_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objPaymentIndentBL = new PaymentIndentBL();
                objPaymentIndentBL.Task = "Delete_PaymentIndent";
                objPaymentIndentBL.PayInd_No = Convert.ToString(e.Record["PayInd_No"].ToString());
                if (objPaymentIndentBL.delete(con, PaymentIndentBL.eLoadSp.DELETE_PAYMENT_INDENT_BY_PAYINDNO))
                {
                    BindPaymentIndentList();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent has been deleted sucessfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete Payment Indent ');", true);
                }
            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }


        protected void Grid_PaymentIndent_Completed_UpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                ObjStaffSalariesBL = new StaffSalariesBL();

                ObjStaffSalariesBL.Staff_SalaryID = Convert.ToInt32(e.Record["Staff_SalaryID"].ToString());
                
                ObjStaffSalariesBL.Full_Name = (e.Record["Full_Name"].ToString());
                ObjStaffSalariesBL.EmployeeID = (e.Record["EmployeeID"].ToString());
                ObjStaffSalariesBL.Designation = (e.Record["Designation"].ToString());
                ObjStaffSalariesBL.Actual_Date_of_Joining = Convert.ToDateTime(e.Record["Actual_Date_of_Joining"].ToString());
                ObjStaffSalariesBL.Total_Cost_to_Company = Convert.ToDecimal(e.Record["Total_Cost_to_Company"].ToString());
                ObjStaffSalariesBL.Status = (e.Record["Status"].ToString());
                if (e.Record["Basic_Pay"].ToString() != "")
                {
                    ObjStaffSalariesBL.Basic_Pay = Convert.ToDecimal(e.Record["Basic_Pay"].ToString());
                }
                if (e.Record["HRA"].ToString() != "")
                {
                    ObjStaffSalariesBL.HRA = Convert.ToDecimal(e.Record["HRA"].ToString());
                }
                if (e.Record["Conveyance_Allowance"].ToString() != "")
                {
                    ObjStaffSalariesBL.Conveyance_Allowance = Convert.ToDecimal(e.Record["Conveyance_Allowance"].ToString());
                }
                if (e.Record["Special_Allowance"].ToString() != "")
                {
                    ObjStaffSalariesBL.Special_Allowance = Convert.ToDecimal(e.Record["Special_Allowance"].ToString());
                }
                if (e.Record["PF_ER"].ToString() != "")
                {
                    ObjStaffSalariesBL.PF_ER = Convert.ToDecimal(e.Record["PF_ER"].ToString());
                }
                if (e.Record["PF"].ToString() != "")
                {
                    ObjStaffSalariesBL.PF = Convert.ToDecimal(e.Record["PF"].ToString());
                }
                if (e.Record["PT"].ToString() != "")
                {
                    ObjStaffSalariesBL.PT = Convert.ToDecimal(e.Record["PT"].ToString());
                }
                if (e.Record["No_of_Days_Present"].ToString() != "")
                {
                    ObjStaffSalariesBL.No_of_Days_Present = Convert.ToInt32(e.Record["No_of_Days_Present"].ToString());
                }
                if (e.Record["LOP_Days"].ToString() != "")
                {
                    ObjStaffSalariesBL.LOP_Days = Convert.ToInt32(e.Record["LOP_Days"].ToString());
                }
                if (e.Record["LOP_Amount"].ToString() != "")
                {
                    ObjStaffSalariesBL.LOP_Amount = Convert.ToDecimal(e.Record["LOP_Amount"].ToString());
                }
                if (e.Record["Total_Cost_to_Company"].ToString() != "")
                {
                    ObjStaffSalariesBL.Total_Cost_to_Company = Convert.ToDecimal(e.Record["Total_Cost_to_Company"].ToString());
                }
                if (e.Record["Deductions"].ToString() != "")
                {
                    ObjStaffSalariesBL.Deductions = Convert.ToDecimal(e.Record["Deductions"].ToString());
                }
                if (e.Record["Net_Salary"].ToString() != "")
                {
                    ObjStaffSalariesBL.Net_Salary = Convert.ToDecimal(e.Record["Net_Salary"].ToString());
                }
                if (e.Record["NET_PAYMENT"].ToString() != "")
                {
                    ObjStaffSalariesBL.NET_PAYMENT = Convert.ToDecimal(e.Record["NET_PAYMENT"].ToString());
                }
                ObjStaffSalariesBL.CTC = Convert.ToDecimal(e.Record["Total_Cost_to_Company"])*12;
                ObjStaffSalariesBL.Net_Salary = Convert.ToDecimal(e.Record["Basic_Pay"])+ Convert.ToDecimal(e.Record["HRA"])+ Convert.ToDecimal(e.Record["Conveyance_Allowance"])+ Convert.ToDecimal(e.Record["Special_Allowance"]);
                ObjStaffSalariesBL.NET_PAYMENT = Convert.ToDecimal(e.Record["Total_Cost_to_Company"]) - Convert.ToDecimal(e.Record["Deductions"]);
                if (e.Record["No_of_Days_Present"].ToString() != "")
                {
                    decimal OndaySalary = ObjStaffSalariesBL.Net_Salary / (Convert.ToDecimal(e.Record["No_of_Days_Present"]));
                    if (e.Record["LOP_Days"].ToString() != "")
                    {
                        ObjStaffSalariesBL.LOP_Amount = OndaySalary * (Convert.ToDecimal(e.Record["LOP_Days"]));
                    }
                }
                    
               
                ObjStaffSalariesBL.Task = "UpdateSS";
                if (ObjStaffSalariesBL.update(con, StaffSalariesBL.eLoadSp.UPDATE))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Update successfull');", true);
                    BindPaymentIndentList();
                }
            }
            catch (Exception ex)
            {
                // ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
            }
        }
        string MsgBody = "";
        protected void btnNewPAYIND_Click(object sender, EventArgs e)
        {

            try
            {
                string PayIND = "";
                string PageID = "11";
                for (int i = 0; i < Grid_PaymentIndent_Completed.RowsInViewState.Count; i++)
                {
                    GridDataControlFieldCell cell = Grid_PaymentIndent_Completed.RowsInViewState[i].Cells[6] as GridDataControlFieldCell;
                    CheckBox chkSelectTemplatePAYIND = (CheckBox)cell.FindControl("chkSelectTemplatePAYIND");
                    HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
                    if (chkSelectTemplatePAYIND != null && chkSelectTemplatePAYIND.Checked)
                    {
                        PayIND = hdnPayIndNo.Value;
                    }
                }
                if (PayIND != "")
                {
                    Response.Redirect("~/Procurement/PaymentIndent.aspx?PayInd_No=" + PayIND + "&PageID=" + PageID, false);
                }

            }

            catch (Exception ex)
            {
                //  ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }



        }

        protected void ddlSalarymonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlyear.SelectedItem.ToString()!="-Select-" && ddlSalarymonth.SelectedItem.ToString() != "-Select-")
            {
                DvMonthlySalary.Visible = true;
                DvButtons.Visible = true;
                BindPaymentIndentList();
            }
            else 
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Year !.');", true);
            }
           
        }
        protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlyear.SelectedItem.ToString() != "-Select-" && ddlSalarymonth.SelectedItem.ToString() != "-Select-")
            {
                DvMonthlySalary.Visible = true;
                DvButtons.Visible = true;
                BindPaymentIndentList();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select Month !.');", true);
            }
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                DvStatus.Visible = true;
                //BindPaymentIndentList();
                ObjStaffSalariesBL = new StaffSalariesBL();
                string SalaryNo = GetSalaryIdNumber();
                ObjStaffSalariesBL.Salary_Id_number = SalaryNo;
                ObjStaffSalariesBL.month = ddlSalarymonth.SelectedItem.ToString();
                ObjStaffSalariesBL.year = ddlyear.SelectedItem.ToString();

                ObjStaffSalariesBL.payment_indent_status = "Open";
                
                decimal TotalNetSalary = 0;
                string Checked = "";
                for (int i = 0; i < Grid_PaymentIndent_Completed.RowsInViewState.Count; i++)
                {
                    
                    GridDataControlFieldCell cell = Grid_PaymentIndent_Completed.RowsInViewState[i].Cells[25] as GridDataControlFieldCell;
                    //GridDataControlFieldCell cell1 = Grid_PaymentIndent_Completed.RowsInViewState[i].Cells[22] as GridDataControlFieldCell;
                    CheckBox chkSelect = (CheckBox)cell.FindControl("chkSelect");
                    //TextBox NetSalary = (TextBox)cell1.FindControl("TxtNET_PAYMENT");
                    //string aa = NetSalary.Text;
                  
                    HiddenField Staff_SalaryID = cell.FindControl("hdn_payIndNo") as HiddenField;
                    // HiddenField TotalSalary = cell1.FindControl("hdn_NET_PAYMENT") as HiddenField;
                    //GridDataControlFieldCell cell1 = Grid_PaymentIndent_Completed.RowsInViewState[i].Cells[23] as GridDataControlFieldCell;
                    //TextBox TxtNET_PAYMENT = (TextBox)cell1.FindControl("TxtNET_PAYMENT");
                    //string abc = TxtNET_PAYMENT.Text;

                   
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        Checked = "True";
                        ObjStaffSalariesBL.Staff_SalaryID = Convert.ToInt32(Staff_SalaryID.Value.ToString());
                        TotalNetSalary = 0000;
                       
                        

                    }
                    

                }
                ObjStaffSalariesBL.Sum_of_Net_Salary = TotalNetSalary;
                if (Checked != "")
                {
                    if (ObjStaffSalariesBL.INSERT_Salary_Status(con, StaffSalariesBL.eLoadSp.INSERT_Salary_Status))
                    {

                        BindPaymentIndentList();


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add Employee Salary details !.');", true);

                    }
                }
                else 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Employee');", true);
                }
               
            }
            catch (Exception ex)
            {
              //  ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

          
           

        }
        protected string GetSalaryIdNumber() 
        {
            string FinalSalaryNumber = "";
            DataTable dt;
            ObjStaffSalariesBL = new StaffSalariesBL();
            ObjStaffSalariesBL.month = ddlSalarymonth.SelectedValue;
            ObjStaffSalariesBL.year = ddlyear.SelectedItem.ToString();
           
            ObjStaffSalariesBL.Task = "GetSaleryID";
            ObjStaffSalariesBL.load(con, StaffSalariesBL.eLoadSp.PRO_Staff_Salaries_SELECT_ALL, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string SalaryNumber = ds.Tables[0].Rows[0]["Salary_Id_number"].ToString();
                
                SalaryNumber = SalaryNumber.Substring(15, 4);
                SalaryNumber = (Convert.ToInt32(SalaryNumber) + 1).ToString("0000");
                FinalSalaryNumber= SalaryCode + SalaryNumber;
            }
            else
            {
                FinalSalaryNumber= SalaryCode + SalaryNo;
            }

            return FinalSalaryNumber;

        }

        protected void BtnGenerateBulkPaymentIndent_Click(object sender, EventArgs e)
        {

            try
            {
                DvStatus.Visible = true;
                //BindPaymentIndentList();
                ObjStaffSalariesBL = new StaffSalariesBL();
                string SalaryNo = GetSalaryIdNumber();
                ObjStaffSalariesBL.Salary_Id_number = SalaryNo;
                ObjStaffSalariesBL.month = ddlSalarymonth.SelectedItem.ToString();
                ObjStaffSalariesBL.year = ddlyear.SelectedItem.ToString();

                ObjStaffSalariesBL.payment_indent_status = "Processed";

                decimal TotalNetSalary = 0;
                string Checked = "";
                for (int i = 0; i < Grid_PaymentIndent_Completed.RowsInViewState.Count; i++)
                {

                    GridDataControlFieldCell cell = Grid_PaymentIndent_Completed.RowsInViewState[i].Cells[25] as GridDataControlFieldCell;
                    //GridDataControlFieldCell cell1 = Grid_PaymentIndent_Completed.RowsInViewState[i].Cells[22] as GridDataControlFieldCell;
                    CheckBox chkSelect = (CheckBox)cell.FindControl("chkSelect");
                    //TextBox NetSalary = (TextBox)cell1.FindControl("TxtNET_PAYMENT");
                    //string aa = NetSalary.Text;

                    HiddenField Staff_SalaryID = cell.FindControl("hdn_payIndNo") as HiddenField;
                    // HiddenField TotalSalary = cell1.FindControl("hdn_NET_PAYMENT") as HiddenField;
                    //GridDataControlFieldCell cell1 = Grid_PaymentIndent_Completed.RowsInViewState[i].Cells[23] as GridDataControlFieldCell;
                    //TextBox TxtNET_PAYMENT = (TextBox)cell1.FindControl("TxtNET_PAYMENT");
                    //string abc = TxtNET_PAYMENT.Text;


                    if (chkSelect != null && chkSelect.Checked)
                    {
                        Checked = "True";
                        ObjStaffSalariesBL.Staff_SalaryID = Convert.ToInt32(Staff_SalaryID.Value.ToString());
                        TotalNetSalary = 0000;



                    }


                }
                ObjStaffSalariesBL.Sum_of_Net_Salary = TotalNetSalary;
                if (Checked != "")
                {
                    if (ObjStaffSalariesBL.INSERT_Salary_Status(con, StaffSalariesBL.eLoadSp.INSERT_Salary_Status))
                    {

                        BindPaymentIndentList();


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to add Employee Salary details !.');", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select Employee');", true);
                }

            }
            catch (Exception ex)
            {
                //  ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e )
        {
            try
            {
                string StaffId = ((LinkButton)sender).Text.ToString();
                ds = new DataSet();

                ObjStaffSalariesBL = new StaffSalariesBL();
                ObjStaffSalariesBL.Staff_SalaryID = Convert.ToInt32(StaffId);
                ObjStaffSalariesBL.month = ddlSalarymonth.SelectedValue;
                ObjStaffSalariesBL.year = ddlyear.SelectedItem.ToString();
                ObjStaffSalariesBL.Task = "DownloadPaySlip";
                ObjStaffSalariesBL.load(con, StaffSalariesBL.eLoadSp.PRO_Staff_Salaries_SELECT_ALL, ref ds);
                Grid_PaymentIndent_Completed.DataSource = ds;
                Grid_PaymentIndent_Completed.DataBind();

                //Grid_PaymentIndent_Completed.PageSize = -1;
                //Grid_PaymentIndent_Completed.DataBind();
                ExportGridToPDF(Grid_PaymentIndent_Completed);

            }
            catch (Exception ex)
            {
                //ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

            }

        }
        private void ExportGridToPDF(Obout.Grid.Grid GirdData)
        {

            MemoryStream fileStream = new MemoryStream();
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            try
            {
                PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
                doc.Open();
                Font font8 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Color.WHITE);
                Paragraph paragraph = new Paragraph("PaySlip");
                PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count);
                PdfPCell PdfPCell_Data = null;
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    string a=col.ToString();
                    PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                    PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                    PdfTable.AddCell(PdfPCell_Data);
                }
                for (int i = 0; i < GirdData.Rows.Count; i++)
                {
                    Hashtable dataItem = GirdData.Rows[i].ToHashtable();
                    Font font1 = FontFactory.GetFont("ARIAL", 7);
                    foreach (Obout.Grid.Column col in GirdData.Columns)
                    {
                        PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField] != null ? dataItem[col.DataField].ToString() : "", font1)));
                        PdfTable.AddCell(PdfPCell_Data);
                    }
                }

                PdfTable.SpacingBefore = 15f;
                doc.Add(paragraph);
                doc.Add(PdfTable);
            }
            finally
            {
                doc.Close();
            }

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=PaySlip.pdf");
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(fileStream.ToArray());
            // Response.End();
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

        }
    }
}