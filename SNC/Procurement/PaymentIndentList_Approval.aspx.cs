using BusinessLayer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SNC.ErrorLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Obout.Grid;
using System.Net.Mail;
using System.Text;

public partial class PaymentIndentList_Approval : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    PaymentIndentBL objPaymentIndentBL = null;
    DataSet ds = null;
    DataSet Accessds = new DataSet();
    private decimal Total_Amount = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] != null)
            {
                ActionPermission();
                if (!IsPostBack)
                {
                    BindVendorContractorOtherList();
                    BindPaymentIndentList();

                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows.Count > 0)
                        {
                            if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_App_View"].ToString()))
                            {
                                Response.Redirect("~/CommonPages/Home.aspx", false);
                            }
                        }
                    }
                    else
                    {
                        Response.Redirect("~/CommonPages/Login.aspx", true);
                    }
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ActionPermission()
    {
        try
        {
            if (Session["ActionAccess"] != null)
            {
                Accessds = (DataSet)Session["ActionAccess"];
            }
            if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
            {
                if (Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                    }
                }
            }
            else
            {
                Response.Redirect("~/CommonPages/Login.aspx", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindVendorContractorOtherList()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetAllVendorContractorOther";
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_VENDOR_CONTRACTOR_OTHER_ALL, ref ds);
            ddlSearch.DataSource = ds;
            ddlSearch.DataTextField = "Names";
            ddlSearch.DataValueField = "Id";
            ddlSearch.DataBind();
            ddlSearch.Items.Insert(0, "-Select-");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindPaymentIndentList()
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetAllPaymentIndent_Approval";
            objPaymentIndentBL.User_ID = Convert.ToInt32(Session["UID"]);
            objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_PAYMENT_INDENT_ALL, ref ds);
            if (ds != null && ds.Tables.Count > 0)
            {
                grid_PendingPayInd.DataSource = ds.Tables[0];
                grid_PendingPayInd.DataBind();
                grid_PendingPayInd.AllowGrouping = true;

                grid_OnHoldPayInd.DataSource = ds.Tables[1];
                grid_OnHoldPayInd.DataBind();
                grid_OnHoldPayInd.AllowGrouping = true;

                grid_ApprovedPayInd.DataSource = ds.Tables[2];
                grid_ApprovedPayInd.DataBind();
                grid_ApprovedPayInd.AllowGrouping = true;

                grid_ReturnedPayInd.DataSource = ds.Tables[3];
                grid_ReturnedPayInd.DataBind();
                grid_ReturnedPayInd.AllowGrouping = true;

                grid_PartialPayment.DataSource = ds.Tables[4];
                grid_PartialPayment.DataBind();
                grid_PartialPayment.AllowGrouping = true;
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void grid_PendingPayInd_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlink = e.Row.Cells[11].FindControl("lnkPayIndNo") as HyperLink;
                Editlink.NavigateUrl = "~/Procurement/PaymentIndent.aspx?PayInd_No=" + Editlink.Text;

                CheckBox chkSelect = e.Row.Cells[8].FindControl("chkSelect") as CheckBox;
                if (Session["UID"].ToString() != e.Row.Cells[17].Text)
                {
                    //chkSelect.Enabled = false;
                }

                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_App_Update"].ToString()))
                        {
                            Editlink.NavigateUrl = "";
                        }
                    }
                }


            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                //if (!string.IsNullOrEmpty(e.Row.Cells[5].Text))
                //{
                //    Total_Amount += decimal.Parse(e.Row.Cells[5].Text);
                //}

            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {

                e.Row.Cells[5].Text = "Total";
                //RadGrid1.MasterTableView.Font.Size = FontUnit.XXSmall;
                //e.Row.Cells[6].Text = Total_Amount.ToString();
                //txtTotal.Text = Total_Amount.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    public string FormattedAmount
    {
        get { return string.Format("{0:C}", Total_Amount); }
    }

    protected void grid_OnHoldPayInd_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            HyperLink Editlink = e.Row.Cells[9].FindControl("lnkPayIndNo") as HyperLink;
            Editlink.NavigateUrl = "~/Procurement/PaymentIndent.aspx?PayInd_No=" + Editlink.Text;
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                CheckBox chkSelect = e.Row.Cells[7].FindControl("chkSelect_Hold") as CheckBox;

                if (Session["UID"].ToString() != e.Row.Cells[15].Text)
                {
                    chkSelect.Enabled = false;
                }
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {

                GridDataControlFieldCell cell1 = e.Row.Cells[6] as GridDataControlFieldCell;
                TextBox txtApproveAmt = (TextBox)cell1.FindControl("txtApproveAmt_Hold");
                Total_Amount += decimal.Parse(txtApproveAmt.Text);
                //FindHoldTotal + = Convert.ToDecimal(txtApproveAmt.Text);

            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[5].Text = "Total";
                e.Row.Cells[6].Text = Total_Amount.ToString();
                //txtTotal.Text = FindHoldTotal.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnExportToPDF1_Click(object sender, EventArgs e)
    {
        try
        {
            grid_PendingPayInd.PageSize = -1;
            grid_PendingPayInd.DataBind();
            ExportGridToPDF(grid_PendingPayInd, "PendingGrid");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnExportToPDF2_Click(object sender, EventArgs e)
    {
        try
        {
            grid_OnHoldPayInd.PageSize = -1;
            grid_OnHoldPayInd.DataBind();
            ExportGridToPDF(grid_OnHoldPayInd, "HoldGrid");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnExportToPDF3_Click(object sender, EventArgs e)
    {
        try
        {
            grid_ApprovedPayInd.PageSize = -1;
            grid_ApprovedPayInd.DataBind();
            ExportGridToPDF(grid_ApprovedPayInd, "ApprovedGrid");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void ExportGridToPDF(Obout.Grid.Grid GirdData, string GridName)
    {
        MemoryStream fileStream = new MemoryStream();
        Document doc = new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 10, 10, 42, 35);
        try
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();
            Font font8 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Color.WHITE);
            Paragraph paragraph = null;
            PdfPTable PdfTable = null;
            PdfPCell PdfPCell_Data = null;
            if (GridName == "PendingGrid")
            {
                paragraph = new Paragraph("                                                            Pending Payment Indent List");
                PdfTable = new PdfPTable(GirdData.Columns.Count - 3);
                PdfTable.WidthPercentage = 90f;
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    if (col.HeaderText != "Approved Amount" & col.HeaderText != "Select" & col.HeaderText != "Approver" & col.HeaderText != "")
                    {
                        PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                        PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                        PdfTable.AddCell(PdfPCell_Data);
                    }
                }
                for (int i = 0; i < GirdData.Rows.Count; i++)
                {
                    Hashtable dataItem = GirdData.Rows[i].ToHashtable();
                    Font font1 = FontFactory.GetFont("ARIAL", 7);
                    foreach (Obout.Grid.Column col in GirdData.Columns)
                    {
                        if (col.HeaderText != "Approved Amount" & col.HeaderText != "Approved Amount" & col.HeaderText != "Select" & col.HeaderText != "Approver" & col.HeaderText != "")
                        {
                            PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField] != null ? dataItem[col.DataField].ToString() : "", font1)));
                            PdfTable.AddCell(PdfPCell_Data);
                        }
                    }
                }
            }
            else if (GridName == "HoldGrid")
            {
                paragraph = new Paragraph("                                                            OnHold Payment Indent List");
                PdfTable = new PdfPTable(GirdData.Columns.Count - 3);
                PdfTable.WidthPercentage = 90f;
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    if (col.HeaderText != "Approved Amount" & col.HeaderText != "Select" & col.HeaderText != "Approver" & col.HeaderText != "")
                    {
                        PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                        PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                        PdfTable.AddCell(PdfPCell_Data);
                    }
                }
                for (int i = 0; i < GirdData.Rows.Count; i++)
                {
                    Hashtable dataItem = GirdData.Rows[i].ToHashtable();
                    Font font1 = FontFactory.GetFont("ARIAL", 7);
                    foreach (Obout.Grid.Column col in GirdData.Columns)
                    {
                        if (col.HeaderText != "Approved Amount" & col.HeaderText != "Select" & col.HeaderText != "Approver" & col.HeaderText != "")
                        {
                            PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField] != null ? dataItem[col.DataField].ToString() : "", font1)));
                            PdfTable.AddCell(PdfPCell_Data);
                        }
                    }
                }
            }
            else
            {
                paragraph = new Paragraph("                                                            Approved Payment Indent List");
                PdfTable = new PdfPTable(GirdData.Columns.Count - 2);
                PdfTable.WidthPercentage = 90f;
                foreach (Obout.Grid.Column col in GirdData.Columns)
                {
                    if (col.HeaderText != "Select" & col.HeaderText != "Approver" & col.HeaderText != "")
                    {
                        PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                        PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                        PdfTable.AddCell(PdfPCell_Data);
                    }
                }
                for (int i = 0; i < GirdData.Rows.Count; i++)
                {
                    Hashtable dataItem = GirdData.Rows[i].ToHashtable();
                    Font font1 = FontFactory.GetFont("ARIAL", 7);
                    foreach (Obout.Grid.Column col in GirdData.Columns)
                    {
                        if (col.HeaderText != "Select" & col.HeaderText != "Approver" & col.HeaderText != "")
                        {
                            PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField] != null ? dataItem[col.DataField].ToString() : "", font1)));
                            PdfTable.AddCell(PdfPCell_Data);
                        }
                    }
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
        if (GridName == "PendingGrid")
        {
            Response.AddHeader("content-disposition", "attachment;filename=PaymentIndentList_Pending.pdf");
        }
        else if (GridName == "HoldGrid")
        {
            Response.AddHeader("content-disposition", "attachment;filename=PaymentIndentList_OnHold.pdf");
        }
        else
        {
            Response.AddHeader("content-disposition", "attachment;filename=PaymentIndentList_Approved.pdf");
        }
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        //string messageBody = "<font>Dear Team, </font><br> <br> <font> </font>Herewith approving the below mentioned payments for your immediate action<br> <br>";
        //try
        //{
        //    for (int i = 0; i < grid_PendingPayInd.RowsInViewState.Count; i++)
        //    {

        //        DataTable EmailTable = new DataTable();

        //        if (grid_PendingPayInd.RowsInViewState.Count == 0)
        //            messageBody = "No Records Aproved";
        //        string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
        //        string htmlTableEnd = "</table>";
        //        string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;\">";
        //        string htmlHeaderRowEnd = "</tr>";
        //        string htmlTrStart = "<tr style =\"color:#555555;\">";
        //        string htmlTrEnd = "</tr>";
        //        string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
        //        string htmlTdEnd = "</td>";

        //        messageBody += htmlTableStart;
        //        messageBody += htmlHeaderRowStart;
        //        messageBody += htmlTdStart + "Column1 " + htmlTdEnd;
        //        messageBody += htmlHeaderRowEnd;

        //        for (int rowcount = 0; rowcount < grid_PendingPayInd.RowsInViewState.Count; rowcount++)
        //        {
        //            GridDataControlFieldCell cell = grid_PendingPayInd.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
        //            CheckBox chkSelect = (CheckBox)cell.FindControl("chkSelect");
        //            HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
        //            if (chkSelect != null && chkSelect.Checked)
        //            {
        //                GridDataControlFieldCell cell1 = grid_PendingPayInd.RowsInViewState[i].Cells[6] as GridDataControlFieldCell;
        //                TextBox txtApproveAmt = (TextBox)cell1.FindControl("txtApproveAmt");
        //                DataView dtS = (DataView)grid_PendingPayInd.DataSource;
        //                //string PaymentIndNumber = Convert.ToString(dtS.Table.Rows[1][1]);
        //                string PaymentIndNumber = Convert.ToString(hdnPayIndNo.Value);

        //                dtS.RowFilter = "PayInd_No='" + PaymentIndNumber + "'";
        //                if (chkSelect != null && chkSelect.Checked)
        //                {
        //                    if (Convert.ToString(hdnPayIndNo.Value) == PaymentIndNumber)
        //                    {
        //                        messageBody = messageBody + htmlTrStart;
        //                        messageBody = messageBody + htmlTdStart + Convert.ToString(dtS.Table.Columns[0]) + htmlTdEnd;
        //                        messageBody = messageBody + htmlTdStart + Convert.ToString(dtS.Table.Columns[1]) + htmlTdEnd;
        //                        messageBody = messageBody + htmlTdStart + Convert.ToString(dtS.Table.Columns[2]) + htmlTdEnd;
        //                        messageBody = messageBody + htmlTdStart + Convert.ToString(dtS.Table.Columns[3]) + htmlTdEnd;
        //                        messageBody = messageBody + htmlTdStart + Convert.ToString(dtS.Table.Columns[4]) + htmlTdEnd;
        //                        messageBody = messageBody + htmlTdStart + Convert.ToString(dtS.Table.Columns[5]) + htmlTdEnd;
        //                        messageBody = messageBody + htmlTdStart + Convert.ToString(dtS.Table.Columns[6]) + htmlTdEnd;
        //                        messageBody = messageBody + htmlTrEnd;
        //                    }
        //                }
        //            }
        //        }
        //        messageBody = messageBody + htmlTableEnd;

        //        string to = "jeromeiv09@gmail.com"; //To address    
        //        string from = "simprodevteam@gmail.com"; //From address    
        //        MailMessage message = new MailMessage(from, to);

        //        string mailbody = messageBody;
        //        message.Subject = "Sending Email Using Asp.Net & C#";
        //        message.Body = mailbody;
        //        message.BodyEncoding = Encoding.UTF8;
        //        message.IsBodyHtml = true;
        //        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
        //        System.Net.NetworkCredential basicCredential1 = new
        //        System.Net.NetworkCredential("simprodevteam@gmail.com", "s!mpr0@123$");
        //        client.EnableSsl = true;
        //        client.UseDefaultCredentials = false;
        //        client.Credentials = basicCredential1;
        //        try
        //        {
        //            client.Send(message);
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //}
        
        try
        {
            
            for (int i = 0; i < grid_PendingPayInd.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = grid_PendingPayInd.RowsInViewState[i].Cells[8] as GridDataControlFieldCell;
                CheckBox chkSelect = (CheckBox)cell.FindControl("chkSelect");
                HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
                GridDataControlFieldCell cell1 = grid_PendingPayInd.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                TextBox txtApproveAmt = (TextBox)cell1.FindControl("txtApproveAmt");
                if (chkSelect != null && chkSelect.Checked)
                {
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.PayInd_No = hdnPayIndNo.Value;
                    if (txtApproveAmt.Text.Trim() != string.Empty)
                    {
                        objPaymentIndentBL.Amt_Approved = Convert.ToDecimal(txtApproveAmt.Text.Trim());
                        objPaymentIndentBL.Payment_Approved_Date = Convert.ToDateTime(DateTime.Now);
                        objPaymentIndentBL.Status = "Approved";
                        objPaymentIndentBL.Task = "Update_PaymentIndent_Status";
                        if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_STATUS))
                        {
                            BindPaymentIndentList();
                            Btn_upddec_Click();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Approved Amount is not entered');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnHold_Partial_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grid_PartialPayment.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = grid_PartialPayment.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                CheckBox chkSelect = (CheckBox)cell.FindControl("chkSelect");
                HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
                GridDataControlFieldCell cell1 = grid_PartialPayment.RowsInViewState[i].Cells[6] as GridDataControlFieldCell;
                TextBox txtApproveAmt = (TextBox)cell1.FindControl("txtApproveAmt");
                if (chkSelect != null && chkSelect.Checked)
                {
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.PayInd_No = hdnPayIndNo.Value;
                    if (txtApproveAmt.Text.Trim() != string.Empty)
                    {
                        objPaymentIndentBL.Amt_Approved = Convert.ToDecimal(txtApproveAmt.Text.Trim());
                    }
                    objPaymentIndentBL.Status = "Hold";
                    objPaymentIndentBL.Task = "Update_PaymentIndent_Status";
                    if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_STATUS))
                    {
                        BindPaymentIndentList();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnHold_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grid_PendingPayInd.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = grid_PendingPayInd.RowsInViewState[i].Cells[8] as GridDataControlFieldCell;
                CheckBox chkSelect = (CheckBox)cell.FindControl("chkSelect");
                HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
                GridDataControlFieldCell cell1 = grid_PendingPayInd.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                TextBox txtApproveAmt = (TextBox)cell1.FindControl("txtApproveAmt");
                if (chkSelect != null && chkSelect.Checked)
                {
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.PayInd_No = hdnPayIndNo.Value;
                    if (txtApproveAmt.Text.Trim() != string.Empty)
                    {
                        objPaymentIndentBL.Amt_Approved = Convert.ToDecimal(txtApproveAmt.Text.Trim());
                    }
                    objPaymentIndentBL.Status = "Hold";
                    objPaymentIndentBL.Task = "Update_PaymentIndent_Status";
                    if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_STATUS))
                    {
                        BindPaymentIndentList();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnApprove_Hold_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grid_OnHoldPayInd.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = grid_OnHoldPayInd.RowsInViewState[i].Cells[8] as GridDataControlFieldCell;
                CheckBox chkSelect_Hold = (CheckBox)cell.FindControl("chkSelect_Hold");
                HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
                GridDataControlFieldCell cell1 = grid_OnHoldPayInd.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                TextBox txtApproveAmt = (TextBox)cell1.FindControl("txtApproveAmt_Hold");
                if (chkSelect_Hold != null && chkSelect_Hold.Checked)
                {
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.PayInd_No = hdnPayIndNo.Value;
                    if (txtApproveAmt.Text.Trim() != string.Empty)
                    {
                        objPaymentIndentBL.Amt_Approved = Convert.ToDecimal(txtApproveAmt.Text.Trim());
                        objPaymentIndentBL.Status = "Approved";
                        objPaymentIndentBL.Task = "Update_PaymentIndent_Status";
                        if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_STATUS))
                        {
                            BindPaymentIndentList();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Approved Amount is not entered');", true);
                    }
                }
            }
            if (ddlSearch.SelectedIndex > 0)
            {
                ddlSearch_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void grid_Approved_Payment_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlink = e.Row.Cells[0].FindControl("lnkPayIndNo") as HyperLink;
                Editlink.NavigateUrl = "~/Procurement/PaymentIndent.aspx?PayInd_No=" + Editlink.Text;

                CheckBox chkSelect = e.Row.Cells[5].FindControl("chkSelect") as CheckBox;
                //if (Session["UID"].ToString() == "88" || Session["UID"].ToString() == "86")
                //{
                //    chkSelect.Enabled = true;
                //}
                //else
                //{
                //    chkSelect.Enabled = false;
                //}
                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_App_Update"].ToString()))
                        {
                            Editlink.NavigateUrl = "";
                        }
                        //if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_App_Delete"].ToString()))
                        //{
                        //    grid_PendingPayInd.Columns[11].Visible = false;
                        //}
                    }
                }


            }
            //if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            //{
            //    //if (!string.IsNullOrEmpty(e.Row.Cells[5].Text))
            //    //{
            //    //    Total_Amount += decimal.Parse(e.Row.Cells[5].Text);
            //    //}

            //}
            //else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            //{

            //    e.Row.Cells[5].Text = "Total";
            //    //RadGrid1.MasterTableView.Font.Size = FontUnit.XXSmall;
            //    //e.Row.Cells[6].Text = Total_Amount.ToString();
            //    //txtTotal.Text = Total_Amount.ToString();
            //}
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void grid_Returned_Payment_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlink = e.Row.Cells[0].FindControl("lnkPayIndNo") as HyperLink;
                Editlink.NavigateUrl = "~/Procurement/PaymentIndent.aspx?PayInd_No=" + Editlink.Text;
                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_App_Update"].ToString()))
                        {
                            Editlink.NavigateUrl = "";
                        }
                        //if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_App_Delete"].ToString()))
                        //{
                        //    grid_PendingPayInd.Columns[11].Visible = false;
                        //}
                    }
                }


            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void grid_ApprovedPayInd_UpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PayInd_No = e.Record["PayInd_No"].ToString();
            if (e.Record["TDS_Perc"].ToString() != "")
            {
                objPaymentIndentBL.TDS_Perc = Convert.ToDecimal(e.Record["TDS_Perc"].ToString());
            }
            if (e.Record["TDS_Amt"].ToString() != "")
            {
                objPaymentIndentBL.TDS_Amt = Convert.ToDecimal(e.Record["TDS_Amt"].ToString());
            }
            if (e.Record["Other_Deduction"].ToString() != "")
            {
                objPaymentIndentBL.OtherDeduction = Convert.ToDecimal(e.Record["Other_Deduction"].ToString());
            }
            if (e.Record["Amt_Transferable"].ToString() != "")
            {
                objPaymentIndentBL.Amt_Transferable = Convert.ToDecimal(e.Record["Amt_Transferable"].ToString());
            }
            if (e.Record["Payment_Ref_No"].ToString() != "")
            {
                objPaymentIndentBL.PaymentRefNo = e.Record["Payment_Ref_No"].ToString();
            }
            if (e.Record["Narration"].ToString() != "")
            {
                objPaymentIndentBL.Narration = e.Record["Narration"].ToString();
            }
            if (e.Record["Payment_Date"].ToString() != "")
            {
                objPaymentIndentBL.PaymentDate = Convert.ToDateTime(e.Record["Payment_Date"].ToString());
            }
            if (e.Record["Payment_Date"].ToString() != "")
            {
                objPaymentIndentBL.PaymentDate = Convert.ToDateTime(e.Record["Payment_Date"].ToString());
            }
            //objPaymentIndentBL.Invoice_Date =Convert.ToDateTime("");
            objPaymentIndentBL.Payment_Approval_Status = true;
            objPaymentIndentBL.Task = "Update_ApprovedPaymentIndent";
            if (objPaymentIndentBL.update_Aprovel(con, PaymentIndentBL.eLoadSp.UPDATE))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Update successfull');", true);
                BindPaymentIndentList();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void grid_ReturnedPayInd_UpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {

            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PayInd_No = e.Record["PayInd_No"].ToString();
            if (e.Record["TDS_Perc"].ToString() != "")
            {
                objPaymentIndentBL.TDS_Perc = Convert.ToDecimal(e.Record["TDS_Perc"].ToString());
            }
            if (e.Record["TDS_Amt"].ToString() != "")
            {
                objPaymentIndentBL.TDS_Amt = Convert.ToDecimal(e.Record["TDS_Amt"].ToString());
            }
            if (e.Record["Other_Deduction"].ToString() != "")
            {
                objPaymentIndentBL.OtherDeduction = Convert.ToDecimal(e.Record["Other_Deduction"].ToString());
            }
            if (e.Record["Amt_Transferable"].ToString() != "")
            {
                objPaymentIndentBL.Amt_Transferable = Convert.ToDecimal(e.Record["Amt_Transferable"].ToString());
            }
            if (e.Record["Payment_Ref_No"].ToString() != "")
            {
                objPaymentIndentBL.PaymentRefNo = e.Record["Payment_Ref_No"].ToString();
            }
            if (e.Record["Narration"].ToString() != "")
            {
                objPaymentIndentBL.Narration = e.Record["Narration"].ToString();
            }
            if (e.Record["Payment_Date"].ToString() != "")
            {
                objPaymentIndentBL.PaymentDate = Convert.ToDateTime(e.Record["Payment_Date"].ToString());
            }
            if (e.Record["Payment_Date"].ToString() != "")
            {
                objPaymentIndentBL.PaymentDate = Convert.ToDateTime(e.Record["Payment_Date"].ToString());
            }
            //objPaymentIndentBL.Invoice_Date =Convert.ToDateTime("");
            objPaymentIndentBL.Payment_Approval_Status = true;
            objPaymentIndentBL.Task = "Update_ApprovedPaymentIndent";
            if (objPaymentIndentBL.update_Aprovel(con, PaymentIndentBL.eLoadSp.UPDATE))
            {
                objPaymentIndentBL.Status = "Approved";
                objPaymentIndentBL.Task = "Update_PaymentIndent_Status_Return";
                if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_RETURNED_STATUS))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent has been Update sucessfully');", true);
                }
                //ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Update successfull');", true);
                BindPaymentIndentList();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void btnMake_Completed_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grid_ApprovedPayInd.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = grid_ApprovedPayInd.RowsInViewState[i].Cells[05] as GridDataControlFieldCell;
                CheckBox chkSelect = (CheckBox)cell.FindControl("chkSelect");
                HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
                if (chkSelect != null && chkSelect.Checked)
                {
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.PayInd_No = hdnPayIndNo.Value;
                    if (objPaymentIndentBL.PayInd_No != string.Empty)
                    {

                        objPaymentIndentBL.Payment_Approval_Status = true;
                        objPaymentIndentBL.Task = "Update_MoveTOComplete";
                        if (objPaymentIndentBL.move_To_Complete(con, PaymentIndentBL.eLoadSp.UPDATE))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent has been Moved to Completed sucessfully');", true);
                            BindPaymentIndentList();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Return Failed');", true);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void grid_ReturnedPayInd_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlink = e.Row.Cells[10].FindControl("lnkPayIndNo") as HyperLink;
                Editlink.NavigateUrl = "~/Procurement/PaymentIndent.aspx?PayInd_No=" + Editlink.Text;

                CheckBox chkSelect = e.Row.Cells[7].FindControl("chkSelect") as CheckBox;
                //if (Session["UID"].ToString() != e.Row.Cells[16].Text)
                //{
                //    chkSelect.Enabled = false;
                //}

                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_App_Update"].ToString()))
                        {
                            Editlink.NavigateUrl = "";
                        }
                        //if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_App_Delete"].ToString()))
                        //{
                        //    grid_PendingPayInd.Columns[11].Visible = false;
                        //}
                    }
                }
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                //if (!string.IsNullOrEmpty(e.Row.Cells[5].Text))
                //{
                //    Total_Amount += decimal.Parse(e.Row.Cells[5].Text);
                //}
            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {
                e.Row.Cells[5].Text = "Total";
                //RadGrid1.MasterTableView.Font.Size = FontUnit.XXSmall;
                //e.Row.Cells[6].Text = Total_Amount.ToString();
                //txtTotal.Text = Total_Amount.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnApprove_Returned_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < grid_ReturnedPayInd.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = grid_ReturnedPayInd.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                CheckBox chkSelect = (CheckBox)cell.FindControl("chkSelect");
                HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
                GridDataControlFieldCell cell1 = grid_ReturnedPayInd.RowsInViewState[i].Cells[6] as GridDataControlFieldCell;
                TextBox txtApproveAmt = (TextBox)cell1.FindControl("txtApproveAmt");
                if (chkSelect != null && chkSelect.Checked)
                {
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.PayInd_No = hdnPayIndNo.Value;
                    if (txtApproveAmt.Text.Trim() != string.Empty)
                    {
                        objPaymentIndentBL.Amt_Approved = Convert.ToDecimal(txtApproveAmt.Text.Trim());
                        objPaymentIndentBL.Payment_Approved_Date = Convert.ToDateTime(DateTime.Now);
                        objPaymentIndentBL.Status = "Approved";
                        objPaymentIndentBL.Task = "Update_PaymentIndent_Status";
                        if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_STATUS))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent has been Approved sucessfully');", true);
                            BindPaymentIndentList();
                            Btn_upddec_Click();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Approved Amount is not entered');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Btn_upddec_Click()
    {

    }

    protected void grid_Pending_PartialPayment_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink Editlink = e.Row.Cells[10].FindControl("lnkPayIndNo") as HyperLink;
                Editlink.NavigateUrl = "~/Procurement/PaymentIndent.aspx?PayInd_No=" + Editlink.Text;

                CheckBox chkSelect = e.Row.Cells[7].FindControl("chkSelect") as CheckBox;
                if (Session["UID"].ToString() != e.Row.Cells[16].Text)
                {
                    chkSelect.Enabled = false;
                }

                if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_App_Update"].ToString()))
                        {
                            Editlink.NavigateUrl = "";
                        }
                        //if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_App_Delete"].ToString()))
                        //{
                        //    grid_PendingPayInd.Columns[11].Visible = false;
                        //}
                    }
                }
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                //if (!string.IsNullOrEmpty(e.Row.Cells[5].Text))
                //{
                //    Total_Amount += decimal.Parse(e.Row.Cells[5].Text);
                //}
            }
            else if (e.Row.RowType == Obout.Grid.GridRowType.ColumnFooter)
            {

                e.Row.Cells[5].Text = "Total";
                //RadGrid1.MasterTableView.Font.Size = FontUnit.XXSmall;
                //e.Row.Cells[6].Text = Total_Amount.ToString();
                //txtTotal.Text = Total_Amount.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnExportToPDF_Partial_Click(object sender, EventArgs e)
    {
        try
        {
            grid_PartialPayment.PageSize = -1;
            grid_PartialPayment.DataBind();
            ExportGridToPDF(grid_PartialPayment, "PartialPayment");
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnApprove_Partial_Click(object sender, EventArgs e)
    {

        try
        {

            for (int i = 0; i < grid_PartialPayment.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = grid_PartialPayment.RowsInViewState[i].Cells[8] as GridDataControlFieldCell;
                CheckBox chkSelect = (CheckBox)cell.FindControl("chkSelect");
                HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
                GridDataControlFieldCell cell1 = grid_PartialPayment.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                TextBox txtApproveAmt = (TextBox)cell1.FindControl("txtApproveAmt");
                if (chkSelect != null && chkSelect.Checked)
                {
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.PayInd_No = hdnPayIndNo.Value;
                    if (txtApproveAmt.Text.Trim() != string.Empty)
                    {
                        objPaymentIndentBL.Amt_Approved = Convert.ToDecimal(txtApproveAmt.Text.Trim());
                        objPaymentIndentBL.Payment_Approved_Date = Convert.ToDateTime(DateTime.Now);
                        objPaymentIndentBL.Status = "Approved";
                        objPaymentIndentBL.Task = "Update_PaymentIndent_Status";
                        if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_STATUS))
                        {
                            BindPaymentIndentList();
                            Btn_upddec_Click();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Approved Amount is not entered');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkDownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            string filePath = Server.MapPath("~\\UploadedFiles\\" + (sender as LinkButton).Text.Replace("/", "-"));
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Flush();
            Response.TransmitFile(filePath);
            //Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["Emp_No"] != null ? Session["Emp_No"].ToString() : string.Empty);
        }
    }

    protected void btnyes_Click(object sender, EventArgs e)
    {

        try
        {
            if (grid_ApprovedPayInd.SelectedRecords != null)
            {
                string sText = "";
                foreach (Hashtable oRecord in grid_ApprovedPayInd.SelectedRecords)
                {
                    sText += oRecord["PayInd_No"];

                }
                objPaymentIndentBL = new PaymentIndentBL();
                objPaymentIndentBL.PayInd_No = sText;
                objPaymentIndentBL.Status = "SendForApproval";
                objPaymentIndentBL.Task = "Update_PaymentIndent_Status_Reverse";
                if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_STATUS))
                {
                    BindPaymentIndentList();

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please Select a Row to Proceed.');", true);
            }


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void Grid_File_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "DeleteFile";
            objPaymentIndentBL.PayInd_No = ViewState["PayInd_No"].ToString();
            objPaymentIndentBL.File_Name = e.Record["File_Name"].ToString();

            if (objPaymentIndentBL.delete(con, PaymentIndentBL.eLoadSp.DELETE_FILE_BY_FILENAME))
            {
                //BindFileGrid();
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('File has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Failed to delete File !.');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkDownloadDocumentItem_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetPaymentIndentFile";
            ViewState["PayInd_No"] = Convert.ToString(((LinkButton)sender).CommandName);
            objPaymentIndentBL.PayInd_No = Convert.ToString(((LinkButton)sender).CommandName);
            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENTINDENTDETAILS, ref ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Grid_File.DataSource = ds;
                Grid_File.DataBind();
            }
            ModalWOSubItem.Show();
            lblPendingGrid_DocDownload.Text = "Document's Of Indent Number - " + objPaymentIndentBL.PayInd_No;
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();

            if (ddlSearch.SelectedItem.Text != "-Select-")
            {
                objPaymentIndentBL = new PaymentIndentBL();
                if (ddlSearch.SelectedValue.Substring(0, 3) == "VEN")
                {
                    objPaymentIndentBL.Vendor_ID = ddlSearch.SelectedValue.Substring(4, ddlSearch.SelectedValue.Length - 4);
                }
                else if (ddlSearch.SelectedValue.Substring(0, 3) == "SUB")
                {
                    objPaymentIndentBL.SubCon_ID = ddlSearch.SelectedValue.Substring(4, ddlSearch.SelectedValue.Length - 4);
                }
                else
                {
                    objPaymentIndentBL.Other_ID = ddlSearch.SelectedValue.Substring(4, ddlSearch.SelectedValue.Length - 4);
                }

                objPaymentIndentBL.Task = "GetAllPaymentIndent_WithFilter";
                objPaymentIndentBL.User_ID = Convert.ToInt32(Session["UID"]);
                objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
                objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENT_INDENT_WITH_FILTER, ref ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    grid_PendingPayInd.DataSource = ds.Tables[0];
                    grid_PendingPayInd.DataBind();
                    grid_PendingPayInd.AllowGrouping = false;

                    grid_OnHoldPayInd.DataSource = ds.Tables[1];
                    grid_OnHoldPayInd.DataBind();
                    grid_PendingPayInd.AllowGrouping = false;

                    grid_ApprovedPayInd.DataSource = ds.Tables[2];
                    grid_ApprovedPayInd.DataBind();
                    grid_PendingPayInd.AllowGrouping = false;

                    grid_ReturnedPayInd.DataSource = ds.Tables[3];
                    grid_ReturnedPayInd.DataBind();
                    grid_PendingPayInd.AllowGrouping = false;

                    grid_PartialPayment.DataSource = ds.Tables[4];
                    grid_PartialPayment.DataBind();
                    grid_PendingPayInd.AllowGrouping = false;
                }
            }
            else
            {
                BindPaymentIndentList();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnClearFilter_Click(object sender, EventArgs e)
    {
        try
        {
            ddlSearch.SelectedIndex = 0;
            BindPaymentIndentList();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }


























    }

    protected void lnkBtnPaymentAdvice_Click(object sender, EventArgs e)
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();

            ViewState["PayInd_No"] = Convert.ToString(((LinkButton)sender).CommandName);
            objPaymentIndentBL.PayInd_No = Convert.ToString(((LinkButton)sender).CommandName);

            objPaymentIndentBL = new PaymentIndentBL();
            //objPaymentIndentBL.PayInd_No = PayInd_No;
            ViewState["PayInd_No"] = Convert.ToString(((LinkButton)sender).CommandName);
            objPaymentIndentBL.PayInd_No = Convert.ToString(((LinkButton)sender).CommandName);
            //objPaymentIndentBL.PayInd_No = ViewState["PayInd_No"].ToString();
            //ViewState["PayInd_No"] = ((LinkButton)sender).CommandName.ToString();
            Response.Redirect("~/Procurement/Bank_Advice_Print.aspx?PayInd_No=" + (((LinkButton)sender).CommandName).ToString(), false);

            // Response.Redirect("~/Procurement/PaymentIndent.aspx?PayInd_No=" + PayInd_No + "&PageID=" + PageID, false);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    protected void BindGridpayadvice(string PayIbd_No)
    {
        try
        {
            ds = new DataSet();
            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.Task = "GetAllPaymentIndent_PayINDID";
            objPaymentIndentBL.PayInd_No = PayIbd_No;

            objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENT_INDENT_ALL, ref ds);

            if (ds != null && ds.Tables.Count > 0)
            {
                Gridpayadvice.DataSource = ds.Tables[0];
                Gridpayadvice.DataBind();
                Gridpayadvice.AllowGrouping = true;

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }


    }

































































}
