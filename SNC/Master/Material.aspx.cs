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
using System.Web.UI.WebControls;


public partial class Material : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    Category objCategory = null;
    UOM objUOM = null;
    MaterialBL objMaterial = null;
    DataSet ds = null;
    DataTable dt = null;
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
                    BindBudgetSectors();
                    BindUOMDetails();
                    BindCategoryDetails();
                    BindMaterialDetails();
                    txtItemCode.Enabled = false;

                    if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                    {

                        if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Material_View"].ToString()))
                        {
                            Response.Redirect("~/CommonPages/Home.aspx", false);
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
                        if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Material_Create"].ToString()))
                        {
                            btnSave.Visible = true;
                        }
                        else
                        {
                            btnSave.Visible = false;
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


    private void BindBudgetSectors()
    {
        objMaterial = new MaterialBL();
        ds = new DataSet();
        DataTable DatafilterDt;
        DataTable dtSector = new DataTable();
        DataTable dtCategorySector = new DataTable();
        objMaterial.load(con, MaterialBL.eLoadSp.SELECT_BUDGET_SECTOR_ALL, ref ds);
        if (ds.Tables.Count > 0)
        {
            DatafilterDt = ds.Tables[0];
            dtSector = DatafilterDt.Clone();
            dtCategorySector = DatafilterDt.Clone();

            if (btnBudgetSector.Text != "Update")
            {
                foreach (DataRow row in DatafilterDt.Rows)
                {
                    object value = row["Sector_Prefix"];
                    if (value == DBNull.Value)
                    {
                        dtSector.Rows.Add(row.ItemArray);
                    }
                    else
                    {
                        dtCategorySector.Rows.Add(row.ItemArray);
                    }
                }
                if (dtSector.Rows.Count > 0)
                {
                    ddlSector.DataSource = dtSector;
                    ddlSector.DataTextField = "Sector_Name";
                    ddlSector.DataValueField = "Budget_Sector_ID";
                    ddlSector.DataBind();
                    ddlSector.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlSector.Items.Insert(0, "-Select-");
                }

                if (dtCategorySector.Rows.Count > 0)
                {
                    ddlCategoryBudgetSector.DataSource = dtCategorySector;
                    ddlCategoryBudgetSector.DataTextField = "Sector_Name";
                    ddlCategoryBudgetSector.DataValueField = "Budget_Sector_ID";
                    ddlCategoryBudgetSector.DataBind();
                    ddlCategoryBudgetSector.Items.Insert(0, "-Select-");
                }
                else
                {
                    ddlCategoryBudgetSector.Items.Insert(0, "-Select-");
                }
            }
            else
            {
                ddlSector.DataSource = ds.Tables[0];
                ddlSector.DataTextField = "Sector_Name";
                ddlSector.DataValueField = "Budget_Sector_ID";
                ddlSector.DataBind();
                ddlSector.Items.Insert(0, "-Select-");
            }


            ddlBudgetSector.DataSource = ds.Tables[0];
            ddlBudgetSector.DataTextField = "Sector_Name";
            ddlBudgetSector.DataValueField = "Budget_Sector_ID";
            ddlBudgetSector.DataBind();
            ddlBudgetSector.Items.Insert(0, "-Select-");

            Grid_Sector.DataSource = ds.Tables[0];
            Grid_Sector.DataBind();
        }
    }
    private void BindUOMDetails()
    {
        try
        {
            objUOM = new UOM();
            ds = new DataSet();
            if (objUOM.load(con, UOM.eLoadSp.SELECT_ALL, ref ds))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count >= 0)
                {
                    ddlUOM.DataSource = dt;
                    ddlUOM.DataTextField = "UOMPrefix";
                    ddlUOM.DataValueField = "UOM_ID";
                    ddlUOM.DataBind();
                    ddlUOM.Items.Insert(0, "-Select-");
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }
    private void BindCategoryDetails()
    {

        try
        {
            objCategory = new Category();
            ds = new DataSet();
            DataTable DatafilterDt;
            bool exists;
            int BudgetSectorID;
            if (objCategory.load(con, Category.eLoadSp.SELECT_ALL, ref ds))
            {
                dt = ds.Tables[0];
                if (ddlBudgetSector.SelectedIndex != 0)
                {
                    BudgetSectorID = Convert.ToInt16(ddlBudgetSector.SelectedValue.ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DatafilterDt = ds.Tables[0];


                        exists = DatafilterDt.AsEnumerable().Where(c => c.Field<int>("Budget_Sector_ID").Equals(BudgetSectorID)).Count() > 0;
                        if (exists)
                        {
                            DataTable SectorIDdt = DatafilterDt.AsEnumerable()
                                         .Where(r => r.Field<int>("Budget_Sector_ID") == BudgetSectorID)
                                         .CopyToDataTable();

                            ddlCategoryName.DataSource = SectorIDdt;
                            ddlCategoryName.DataValueField = "Mat_cat_ID";
                            ddlCategoryName.DataTextField = "Category_Name";
                            ddlCategoryName.DataBind();

                        }
                        else
                        {
                            ddlCategoryName.Items.Clear();
                            ddlCategoryName.DataSource = null;
                            ddlCategoryName.DataBind();
                        }
                        exists = false;

                    }
                    ddlCategoryName.Items.Insert(0, "-Select-");
                }

                Grid_Category.DataSource = dt;
                Grid_Category.DataBind();
               
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }

    }

    protected void btnSaveCategory_Click(object sender, EventArgs e)
    {
        try
        {
            objCategory = new Category();
            objCategory.Category_Name = txtCategoryName.Text.Trim();
            objCategory.Cat_prefix = txtCatPrefix.Text.Trim();
            objCategory.Budget_Sector_ID = Convert.ToInt16(ddlCategoryBudgetSector.SelectedValue);
            if (btnSaveCategory.Text == "Save")
            {

                if (objCategory.insert(con, Category.eLoadSp.INSERT))
                {
                    BindCategoryDetails();
                    txtCategoryName.Text = string.Empty;
                    txtCatPrefix.Text = string.Empty;
                    ddlCategoryBudgetSector.SelectedValue = ddlCategoryBudgetSector.Items.FindByText("-Select-").Value;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Category has been added successfully.');", true);

                    ModelCategoryPopup.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Category Prefix or Name already exists.');", true);
                }
            }
            else
            {
                objCategory.Mat_cat_ID = Convert.ToInt16(ViewState["CategoryID"]);
                if (objCategory.update(con, Category.eLoadSp.UPDATE))
                {
                    BindCategoryDetails();
                    txtCategoryName.Text = string.Empty;
                    txtCatPrefix.Text = string.Empty;
                    txtCatPrefix.Enabled = true;
                    btnSaveCategory.Text = "Save";
                    ddlCategoryBudgetSector.SelectedValue = ddlCategoryBudgetSector.Items.FindByText("-Select-").Value;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Category has been updated successfully.');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_Category_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objCategory = new Category();
            objCategory.Mat_cat_ID = Convert.ToInt32(e.Record["Mat_cat_ID"].ToString());

            if (objCategory.delete(con, Category.eLoadSp.DELETEBYCATEGORYID))
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Category has been deleted sucessfully.');", true);
                BindCategoryDetails();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Category is exists in one or more materials !');", true);
            }
            ModelCategoryPopup.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkBtnCategoryID_Click(object sender, EventArgs e)
    {
        try
        {
            objCategory = new Category();

            ViewState["CategoryID"] = Convert.ToInt16(((LinkButton)sender).CommandName.ToString());
            objCategory.Mat_cat_ID = Convert.ToInt16(ViewState["CategoryID"]);

            if (objCategory.load(con, Category.eLoadSp.SELECTBYCATEGORYID))
            {
                txtCategoryName.Text = objCategory.Category_Name.ToString();
                txtCatPrefix.Text = objCategory.Cat_prefix.ToString();
                ddlCategoryBudgetSector.SelectedValue = objCategory.Budget_Sector_ID.ToString();
                txtCatPrefix.Enabled = false;
                btnSaveCategory.Text = "Update";

                ModelCategoryPopup.Show();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            objMaterial = new MaterialBL();

            objMaterial.Cat_ID = Convert.ToInt16(ddlCategoryName.SelectedValue.Trim());

            objMaterial.Item_Code = txtItemCode.Text.Trim();

            objMaterial.Item_Name = txtItemName.Text.Trim();
            objMaterial.UOM = Convert.ToInt16(ddlUOM.SelectedValue.Trim());         

            objMaterial.Budget_Sector_ID = Convert.ToInt16(ddlBudgetSector.SelectedValue);
            if (btnSave.Text == "Save")
            {

                if (objMaterial.insert(con, MaterialBL.eLoadSp.INSERT))
                {


                    txtItemCode.Text = objMaterial.Item_Code.ToString();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Material has been Added sucessfully.');", true);
                
                    BindMaterialDetails();
                    ResetMaterailFields();
                }
                else
                {
                    string msg = objMaterial.Item_Code.ToString();
                    if (msg == "NoPrefix")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Budget Sector does not have prefix!');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Item Name already exists!');", true);
                    }

                }
            }
            else
            {

                if (objMaterial.update(con, MaterialBL.eLoadSp.UPDATE))
                {
                    BindMaterialDetails();
                    ddlCategoryName.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Material has been updated sucessfully.');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Item Name is already exists !');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    private void ResetMaterailFields()
    {
        txtItemName.Text = string.Empty;
        ddlCategoryName.SelectedValue = ddlCategoryName.Items.FindByText("-Select-").Value;
        ddlUOM.SelectedValue = ddlUOM.Items.FindByText("-Select-").Value;        
        ddlBudgetSector.SelectedValue = ddlBudgetSector.Items.FindByText("-Select-").Value;
    }

    private void BindMaterialDetails()
    {
        try
        {
            objMaterial = new MaterialBL();
            ds = new DataSet();
            if (objMaterial.load(con, MaterialBL.eLoadSp.SELECT_ALL, ref ds))
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count >= 0)
                {
                    Grid_Material.DataSource = dt;
                    Grid_Material.DataBind();
                }

            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void Grid_Material_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            objMaterial = new MaterialBL();
            objMaterial.Item_Code = e.Record["Item_Code"].ToString();

            if (objMaterial.delete(con, MaterialBL.eLoadSp.DELETEBYITEMCODE))
            {
                BindMaterialDetails();
                ResetMaterailFields();
                ddlCategoryName.Enabled = true;
                btnSave.Text = "Save";
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Material has been deleted sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This Material is exists in one or more Processes !');", true);
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void lnkBtnItemcode_Click(object sender, EventArgs e)
    {
        try
        {
            objMaterial = new MaterialBL();

            ViewState["Itemcode"] = ((LinkButton)sender).Text.ToString();

            objMaterial.Item_Code = ViewState["Itemcode"].ToString();

            if (objMaterial.load(con, MaterialBL.eLoadSp.SELECTBYITEMCODE))
            {
                ddlBudgetSector.SelectedValue = objMaterial.Budget_Sector_ID.ToString();

                txtItemCode.Text = objMaterial.Item_Code.ToString();
                txtItemName.Text = objMaterial.Item_Name.ToString();
                ddlUOM.SelectedValue = objMaterial.UOM.ToString();              
                txtItemCode.Enabled = false;
                ddlCategoryName.Enabled = false;
                ddlBudgetSector.Enabled = false;
                phItemCode.Visible = true;
                btnSave.Text = "Update";
                BindCategoryDetails();
                ddlCategoryName.SelectedValue = objMaterial.Cat_ID.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Master/Material.aspx", false);
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelCategory_Click(object sender, EventArgs e)
    {
        txtCategoryName.Text = string.Empty;
        txtCatPrefix.Text = string.Empty;
        txtCatPrefix.Enabled = true;
        btnSaveCategory.Text = "Save";
        ddlCategoryBudgetSector.SelectedValue = ddlCategoryBudgetSector.Items.FindByText("-Select-").Value;

        ModelCategoryPopup.Hide();
        Response.Redirect("..//Master/Material.aspx", false);
    }

    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        try
        {
            Grid_Material.PageSize = -1;
            Grid_Material.DataBind();
            ExportGridToPDF(Grid_Material);

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);

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
            Paragraph paragraph = new Paragraph("                                                                       Material Details");
            PdfPTable PdfTable = new PdfPTable(GirdData.Columns.Count - 1);
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
        Response.AddHeader("content-disposition", "attachment;filename=MaterialDetails.pdf");
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(fileStream.ToArray());
        // Response.End();
        HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

    }

    protected void Grid_Material_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
            {
                if (Accessds != null && Accessds.Tables.Count > 0 &&  Accessds.Tables[0].Rows.Count > 0)
                {
                    if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin")
                    {
                        LinkButton Editlik = e.Row.Cells[0].FindControl("lnkBtnItemcode") as LinkButton;

                        if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["Material_Update"].ToString()))
                        {
                            Editlik.Attributes.Add("onClick", "return false;");
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

    protected void btnBudgetSector_Click(object sender, EventArgs e)
    {
        try
        {
            objMaterial = new MaterialBL();
            objMaterial.Budget_Sector_ID = Convert.ToInt16(ddlSector.SelectedValue);
            objMaterial.Sector_Prefix = txtBudgetPrefix.Text.Trim();
            if (btnBudgetSector.Text == "Save")
            {
                if (objMaterial.updateBudgetSector(con, MaterialBL.eLoadSp.UPDATE_BUDGET_SECTOR))
                {
                    BindBudgetSectors();
                    txtBudgetPrefix.Text = string.Empty;
                    ddlSector.SelectedValue = ddlSector.Items.FindByText("-Select-").Value;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Budget Sector has been updated successfully');", true);
                    mpeBudgetSector.Show();
                }
                else
                {
                    string msg = objMaterial.OutputResult.ToString();
                    if (msg == "PrefixExists")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Budget Sector Prefix is already exists!.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Budget Sector Prefix is already referred in Item Code!.');", true);
                    }
                }
            }
            else
            {
                if (objMaterial.updateBudgetSector(con, MaterialBL.eLoadSp.UPDATE_BUDGET_SECTOR))
                {
                    btnBudgetSector.Text = "Save";
                    ddlSector.Enabled = true;
                    BindBudgetSectors();
                    txtBudgetPrefix.Text = string.Empty;
                    ddlSector.SelectedValue = ddlSector.Items.FindByText("-Select-").Value;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('Budget Sector has been updated sucessfully.');", true);
                }
                else
                {
                    string msg = objMaterial.OutputResult.ToString();
                    if (msg == "PrefixExists")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Budget Sector Prefix is already exists!.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('Budget Sector Prefix is already referred in Item Code!.');", true);
                    }
                    mpeBudgetSector.Show();
                }
            }


        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void btnCancelBudgetSector_Click(object sender, EventArgs e)
    {
        mpeBudgetSector.Hide();
    }

    protected void lnkBudgetSector_Click(object sender, EventArgs e)
    {
        try
        {
            ViewState["SectorID"] = Convert.ToInt16(((LinkButton)sender).CommandArgument.ToString());

            txtBudgetPrefix.Text = ((LinkButton)sender).CommandName.ToString();
            btnBudgetSector.Text = "Update";
            ddlSector.Enabled = false;
            BindBudgetSectors();
            ddlSector.SelectedValue = ViewState["SectorID"].ToString();
            mpeBudgetSector.Show();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void ddlBudgetSector_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindCategoryDetails();
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

}
