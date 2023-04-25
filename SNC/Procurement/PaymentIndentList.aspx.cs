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


public partial class PaymentIndentList : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    PaymentIndentBL objPaymentIndentBL = null;
    ProjectBL objProjectBL = null;
    DataSet ds = null;
    DataSet Accessds = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UID"] != null)
            {

                ActionPermission();
                if (!IsPostBack)
                {
                    BindPaymentIndentList();
                    BindPaymentBankingIndentList();
                    
                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {
                        if (Accessds.Tables[0].Rows.Count > 0)
                        {
                            if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_View"].ToString()))
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
                        if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_Create"].ToString()))
                        {
                            lnkbtnAdd.Visible = true;
                        }
                        else
                        {
                            lnkbtnAdd.Visible = false;
                        }
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_Delete"].ToString()))
                        {
                            Grid_PaymentIndent.Columns[11].Visible = false;
                        }
                        else
                        {
                            Grid_PaymentIndent.Columns[11].Visible = true;
                        }
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

    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{

    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        con.Open();
    //        DropDownList DropDownList1 = (e.Row.FindControl("DropDownList1") as DropDownList);


    //        SqlCommand cmd = new SqlCommand("select * from TB_Bank_Details", con);
    //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        sda.Fill(dt);
    //        con.Close();
    //        DropDownList1.DataSource = dt;

    //        DropDownList1.DataTextField = "Bank_Name";
    //        DropDownList1.DataValueField = "Bank_Name";
    //        DropDownList1.DataBind();
    //        DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Qualification--", "0"));


    //    }
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        con.Open();
    //        DropDownList DropDownList1 = (e.Row.FindControl("DropDownList1") as DropDownList);


    //        SqlCommand cmd = new SqlCommand("select * from TB_Bank_Details", con);
    //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        sda.Fill(dt);
    //        con.Close();
    //        DropDownList1.DataSource = dt;

    //        DropDownList1.DataTextField = "Bank_Name";
    //        DropDownList1.DataValueField = "Bank_Name";
    //        DropDownList1.DataBind();
    //        DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Qualification--", "0"));


    //    }

    //}

    //public void BindGrid()
    //{

    //    con.Open();

    //    SqlCommand cmd = new SqlCommand("select * from TB_Bank_Details", con);
    //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
    //    DataTable dt = new DataTable();
    //    sda.Fill(dt);
    //    con.Close();
    //    GridView1.DataSource = dt;
    //    GridView1.DataBind();



    //}

    protected void BindPaymentIndentList()
    {
        try
        { 
           
            if (Session["Role"].ToString() == "Application Admin")
            {
                DataSet DataPay = new DataSet();
                objPaymentIndentBL = new PaymentIndentBL();
                objPaymentIndentBL.Task = "GetAllPaymentIndent";
                DataPay.Clear();
                objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.GET_PAYMENT_INDENT_ALL, ref DataPay);
                if (DataPay != null && DataPay.Tables.Count > 0)
                {
                    Grid_PaymentIndent.DataSource = DataPay.Tables[0];
                    Grid_PaymentIndent.DataBind();

                    Grid_PaymentIndent_Approved.DataSource = DataPay.Tables[1];
                    Grid_PaymentIndent_Approved.DataBind();

                    Grid_PaymentIndent_Completed.DataSource = DataPay.Tables[2];
                    Grid_PaymentIndent_Completed.DataBind();

                    GridPendingBalance.DataSource = DataPay.Tables[3];
                    GridPendingBalance.DataBind();
                }
            }
            else if (Session["Role"].ToString() == "Other")
            {
                DataSet DataPay = new DataSet();
                objPaymentIndentBL = new PaymentIndentBL();
                objPaymentIndentBL.Task = "GetAllPaymentIndent";
                objPaymentIndentBL.User_ID = Convert.ToInt32(Session["UID"].ToString());
                objPaymentIndentBL.Project_Code = Session["Project_Code"].ToString();
                DataPay.Clear();
                objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_PAYMENT_INDENT_ALL, ref DataPay);
                if (DataPay != null && DataPay.Tables.Count > 0)
                {

                    Grid_PaymentIndent.DataSource = DataPay.Tables[0];
                    Grid_PaymentIndent.DataBind();

                    Grid_PaymentIndent_Approved.DataSource = DataPay.Tables[1];
                    Grid_PaymentIndent_Approved.DataBind();

                    Grid_PaymentIndent_Completed.DataSource = DataPay.Tables[2];
                    Grid_PaymentIndent_Completed.DataBind();

                    GridPendingBalance.DataSource = DataPay.Tables[3];
                    GridPendingBalance.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        try
        {
            Grid_PaymentIndent.PageSize = -1;
            Grid_PaymentIndent.DataBind();
            ExportGridToPDF(Grid_PaymentIndent);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    
    protected void btnExportToPDFPendingPartialGrid_Click(object sender, EventArgs e)
    {
        try
        {
            GridPendingBalance.PageSize = -1;
            GridPendingBalance.DataBind();
            ExportGridToPDF(GridPendingBalance);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    
    private void ExportGridToPDF(Obout.Grid.Grid GirdData)
    {
        MemoryStream fileStream = new MemoryStream();
        Document doc = new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 10, 10, 42, 35);
        try
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();
            Font font8 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Color.WHITE);
            Paragraph paragraph = new Paragraph("                                                                                            Payment Indent List");
            PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count - 1);
            PdfTable.WidthPercentage = 90f;
            PdfPCell PdfPCell_Data = null;
            foreach (Obout.Grid.Column col in GirdData.Columns)
            {
                if (col.HeaderText != "Delete")
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
                    if (col.HeaderText != "Delete")
                    {
                        PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(dataItem[col.DataField] != null ? dataItem[col.DataField].ToString() : "", font1)));
                        PdfTable.AddCell(PdfPCell_Data);
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
        Response.AddHeader("content-disposition", "attachment;filename=PaymentIndentList.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_PaymentIndent_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
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
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_Update"].ToString()))
                        {
                            Editlink.NavigateUrl = "";
                        }
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_Delete"].ToString()))
                        {
                            Grid_PaymentIndent.Columns[11].Visible = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnExportToPDF_Approved_Click(object sender, EventArgs e)
    {
        try
        {
            Grid_PaymentIndent_Approved.PageSize = -1;
            Grid_PaymentIndent_Approved.DataBind();
            ExportGridToPDF_Approved(Grid_PaymentIndent_Approved);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void ExportGridToPDF_Approved(Obout.Grid.Grid GridData)
    {
        MemoryStream fileStream = new MemoryStream();
        Document doc = new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 10, 10, 42, 35);
        try
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();
            Font font8 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Color.WHITE);
            Paragraph paragraph = new Paragraph("Approved Payment Indent List");
            PdfPTable PdfTable = new PdfPTable(GridData.Columns.Count);
            PdfTable.WidthPercentage = 90f;
            PdfPCell PdfPCell_Data = null;
            foreach (Obout.Grid.Column col in GridData.Columns)
            {
                PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                PdfTable.AddCell(PdfPCell_Data);
            }
            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                Hashtable dataItem = GridData.Rows[i].ToHashtable();
                Font font1 = FontFactory.GetFont("ARIAL", 7);
                foreach (Obout.Grid.Column col in GridData.Columns)
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
        Response.AddHeader("content-disposition", "attachment;filename=PaymentIndentList_Approved.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
    }

    protected void btnExportToPDF_Completed_Click(object sender, EventArgs e)
    {
        try
        {
            Grid_PaymentIndent_Completed.PageSize = -1;
            Grid_PaymentIndent_Completed.DataBind();
            ExportGridToPDF_Completed(Grid_PaymentIndent_Completed);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void ExportGridToPDF_Completed(Obout.Grid.Grid GridData)
    {
        MemoryStream fileStream = new MemoryStream();
        Document doc = new Document(iTextSharp.text.PageSize.LETTER.Rotate(), 10, 10, 42, 35);
        try
        {
            PdfWriter wri = PdfWriter.GetInstance(doc, fileStream);
            doc.Open();
            Font font8 = FontFactory.GetFont("ARIAL", 7, iTextSharp.text.Color.WHITE);
            Paragraph paragraph = new Paragraph("                                                                                     Approved Payment Indent List");
            PdfPTable PdfTable = new PdfPTable(GridData.Columns.Count);
            PdfTable.WidthPercentage = 90f;
            PdfPCell PdfPCell_Data = null;
            foreach (Obout.Grid.Column col in GridData.Columns)
            {
                PdfPCell_Data = new PdfPCell(new Phrase(new Chunk(col.HeaderText, font8)));
                PdfPCell_Data.BackgroundColor = iTextSharp.text.Color.GRAY;
                PdfTable.AddCell(PdfPCell_Data);
            }
            for (int i = 0; i < GridData.Rows.Count; i++)
            {
                Hashtable dataItem = GridData.Rows[i].ToHashtable();
                Font font1 = FontFactory.GetFont("ARIAL", 7);
                foreach (Obout.Grid.Column col in GridData.Columns)
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
        Response.AddHeader("content-disposition", "attachment;filename=PaymentIndentList_Completed.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
    }


    protected void Grid_PaymentIndent_RowDataBound_Completed(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowState == DataControlRowState.Edit)
            {
                DropDownList ddlCompany_Bank = (e.Row.FindControl("ddlCompany_Bank") as DropDownList);
                ds = new DataSet();
                objPaymentIndentBL = new PaymentIndentBL();
                objPaymentIndentBL.Task = "Select_Bank";
                objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_BANK, ref ds);
                ddlCompany_Bank.DataSource = ds;
                ddlCompany_Bank.DataTextField = "Bank";
                ddlCompany_Bank.DataBind();
                ddlCompany_Bank.Items.Insert(0, "-Select-");
                DataRowView dr = e.Row.DataItem as DataRowView;
                ddlCompany_Bank.SelectedValue = dr["Company_Bank"].ToString();
            }
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {

                HyperLink lnkPayIndNo = e.Row.Cells[0].FindControl("lnkPayIndNo") as HyperLink;
                lnkPayIndNo.NavigateUrl = "~/Procurement/PaymentIndent.aspx?PayInd_No=" + lnkPayIndNo.Text;
                CheckBox chkSelect = e.Row.Cells[23].FindControl("chkSelect") as CheckBox;

                CheckBox SelectTemplatePAYIND = e.Row.Cells[6].FindControl("chkSelectTemplatePAYIND") as CheckBox;
                if (e.Row.Cells[23].Text == "True")
                {
                    SelectTemplatePAYIND.Enabled = false;
                }
                Label Balance_Amount = e.Row.Cells[6].FindControl("lnkBalance_Amount") as Label;
                var Amt_PartPayment =Convert.ToDecimal( e.Row.Cells[26].Text);
                var Amt_Transferable =Convert.ToDecimal(e.Row.Cells[5].Text);
                var Balance_AmountRow =Convert.ToDecimal(Amt_PartPayment - Amt_Transferable);
                //Balance_AmountToGridRow.Text = e.Row.Cells[5].Text;
                Balance_Amount.Text= Convert.ToString(Balance_AmountRow.ToString());
          
              
                //DropDownList ddlCompany_Bank = (e.Row.FindControl("ddlCompany_Bank") as DropDownList);
                //ds = new DataSet();
                //objPaymentIndentBL = new PaymentIndentBL();
                //objPaymentIndentBL.Task = "Select_Bank";
                //objPaymentIndentBL.load(con, PaymentIndentBL.eLoadSp.SELECT_BANK, ref ds);
                //ddlCompany_Bank.DataSource = ds;
                //ddlCompany_Bank.DataTextField = "Bank";
                //ddlCompany_Bank.DataBind();
                //ddlCompany_Bank.Items.Insert(0, "-Select-");
                //DataRowView dr = e.Row.DataItem as DataRowView;
                //ddlCompany_Bank.SelectedValue = dr["Company_Bank"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnReturned_Click(object sender, EventArgs e)
    {

        try
        {

            for (int i = 0; i < Grid_PaymentIndent_Completed.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = Grid_PaymentIndent_Completed.RowsInViewState[i].Cells[22] as GridDataControlFieldCell;
                CheckBox chkSelect = (CheckBox)cell.FindControl("chkSelect");
                HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
                if (chkSelect != null && chkSelect.Checked)
                {
                    objPaymentIndentBL = new PaymentIndentBL();
                    objPaymentIndentBL.PayInd_No = hdnPayIndNo.Value;
                    if (objPaymentIndentBL.PayInd_No != string.Empty)
                    {

                        objPaymentIndentBL.Status = "Returned";
                        objPaymentIndentBL.Task = "Update_PaymentIndent_Status_Return";
                        if (objPaymentIndentBL.updateStatus(con, PaymentIndentBL.eLoadSp.UPDATE_RETURNED_STATUS))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Payment Indent has been Returned sucessfully');", true);
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


    protected void Grid_PaymentIndent_Completed_UpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {

            objPaymentIndentBL = new PaymentIndentBL();
            objPaymentIndentBL.PayInd_No = e.Record["PayInd_No"].ToString();
            //if (e.Record["TDS_Perc"].ToString() != "")
            //{
            //    objPaymentIndentBL.TDS_Perc = Convert.ToDecimal(e.Record["TDS_Perc"].ToString());
            //}
            //if (e.Record["TDS_Amt"].ToString() != "")
            //{
            //    objPaymentIndentBL.TDS_Amt = Convert.ToDecimal(e.Record["TDS_Amt"].ToString());
            //}
            //if (e.Record["Other_Deduction"].ToString() != "")
            //{
            //    objPaymentIndentBL.OtherDeduction = Convert.ToDecimal(e.Record["Other_Deduction"].ToString());
            //}
            if (e.Record["Amt_Transferable"].ToString() != "")
            {
                objPaymentIndentBL.Amt_Transferable = Convert.ToDecimal(e.Record["Amt_Transferable"].ToString());
            }
            if (e.Record["Payment_Ref_No"].ToString() != "")
            {
                objPaymentIndentBL.PaymentRefNo = e.Record["Payment_Ref_No"].ToString();
            }
            if (e.Record["WorkDoneFor_Narration"].ToString() != "")
            {
                objPaymentIndentBL.Narration = e.Record["WorkDoneFor_Narration"].ToString();
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
    
    string MsgBody = "";
    protected void btnNewPAYIND_Click(object sender, EventArgs e)
    {
        try
        {
            string PayIND = "";
            string PageID = "11";
            for (int i = 0; i < Grid_PaymentIndent_Completed.RowsInViewState.Count; i++)
            {
                GridDataControlFieldCell cell = Grid_PaymentIndent_Completed.RowsInViewState[i].Cells[7] as GridDataControlFieldCell;
                CheckBox chkSelectTemplatePAYIND = (CheckBox)cell.FindControl("chkSelectTemplatePAYIND");
                HiddenField hdnPayIndNo = cell.FindControl("hdn_payIndNo") as HiddenField;
                if (chkSelectTemplatePAYIND != null && chkSelectTemplatePAYIND.Checked)
                {
                    PayIND = hdnPayIndNo.Value;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Please select the check box in 7th Colum');", true);
                }
            }
            if (PayIND != "")
            {
                Response.Redirect("~/Procurement/PaymentIndent.aspx?PayInd_No=" + PayIND + "&PageID=" + PageID, false);
            }
            
           
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.FileDownloadError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    
    protected void Grid_PaymentIndentPartialBalance_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
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
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void Grid_PaymentIndentPartialBalance_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
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
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_Update"].ToString()))
                        {
                            Editlink.NavigateUrl = "";
                        }
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_Delete"].ToString()))
                        {
                            Grid_PaymentIndent.Columns[11].Visible = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_Approved_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
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
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_Update"].ToString()))
                        {
                            Editlink.NavigateUrl = "";
                        }
                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["PayInd_Delete"].ToString()))
                        {
                            Grid_PaymentIndent.Columns[11].Visible = false;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
    
    protected void GridBankingIndent_RowDataBound_Completed(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                HyperLink lnkBANK_INDENT_No = e.Row.Cells[0].FindControl("lnkBANK_INDENT_No") as HyperLink;
                lnkBANK_INDENT_No.NavigateUrl = "~/Procurement/BankIndent.aspx?BnkInd_No=" + lnkBANK_INDENT_No.Text;

            }


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindPaymentBankingIndentList()
    {
        try
        {
            objProjectBL = new ProjectBL();
            DataSet ds = new DataSet();
            objProjectBL.Project_Code = Session["Project_Code"].ToString();
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_BANKINDENT_ALL, ref ds);
            GridBankingIndent.DataSource = ds;
            GridBankingIndent.DataBind();

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
}
