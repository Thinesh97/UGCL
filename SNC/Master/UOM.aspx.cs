using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using DataAccess;
using SNC.ErrorLogger;


public partial class UOMPage : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
        UOM objUOM = null;
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
                        BindUOMDetails();
                        if (Accessds != null && Accessds.Tables.Count > 0 && Accessds.Tables[0].Rows.Count > 0)
                        {

                            if (Accessds.Tables[0].Rows[0]["Role"].ToString() != "Application Admin" && !Convert.ToBoolean(Accessds.Tables[0].Rows[0]["UOM_View"].ToString()))
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
            catch(Exception ex)
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
                            if (Convert.ToBoolean(Accessds.Tables[0].Rows[0]["UOM_Create"].ToString()))
                            {
                                btnSave.Visible = true;
                            }
                            else
                            {
                                btnSave.Visible = false;
                            }
                            if (!Convert.ToBoolean(Accessds.Tables[0].Rows[0]["UOM_Delete"].ToString()))
                            {


                                Grid_UOM.Columns[3].Visible = false;

                            }
                            else
                            {
                                Grid_UOM.Columns[3].Visible = true;
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



        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objUOM = new UOM();
                objUOM.UOMName = txtUOMName.Text.Trim();
                objUOM.UOMPrefix = txtUOMPrefix.Text.Trim();

                if(btnSave.Text == "Save")
                {
                    if (objUOM.load(con, UOM.eLoadSp.CHECKDUPLICATEUOM) && objUOM.UOM_ID == 0)
                    {
                        if (objUOM.insert(con, UOM.eLoadSp.INSERT))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('UOM has been added successfully');", true);
                            txtUOMName.Text = string.Empty;
                            txtUOMPrefix.Text = string.Empty;
                            BindUOMDetails();
                        }                       
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('UOM by this name already exists.');", true);
                    }
                }
                else
                {
                    objUOM.UOM_ID = Convert.ToInt16(ViewState["UOMID"]);

                    if (objUOM.load(con, UOM.eLoadSp.CHECKDUPLICATEUOM) && objUOM.UOM_ID == Convert.ToInt32(ViewState["UOMID"]))
                    {
                        if (objUOM.update(con, UOM.eLoadSp.UPDATE))
                        {
                            BindUOMDetails();
                            ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('UOM has been updated sucessfully.');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('UOM by this name already exists.');", true);
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBInsertError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
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
                        Grid_UOM.DataSource = dt;
                        Grid_UOM.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }

        }

    

        protected void lnkBtnUOMID_Click(object sender, EventArgs e)
        {

            try
            {
                objUOM = new UOM();
                ds = new DataSet();

                ViewState["UOMID"] = Convert.ToInt16(((LinkButton)sender).Text.ToString());

                objUOM.UOM_ID = Convert.ToInt16(ViewState["UOMID"]);

                if (objUOM.load(con, UOM.eLoadSp.SELECTBYUOMID))
                {
                    txtUOMName.Text = objUOM.UOMName;
                    btnSave.Text = "Update";
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
          
        }

        protected void Grid_UOM_DeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                objUOM = new UOM();
                objUOM.UOM_ID = Convert.ToInt32(e.Record["UOM_ID"].ToString());

                if (objUOM.delete(con,UOM.eLoadSp.DELETEBYUOMID))
                {
                    BindUOMDetails();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert", "Alert.render('UOM has been deleted sucessfully.');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CustomAlert1", "Alert1.render('This UOM exists in one or more materials !');", true);
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBDeleteError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                  txtUOMName.Text = string.Empty;
                  btnSave.Text = "Save";
                  Response.Redirect("../CommonPages/Home.aspx", false);
            }
            catch (Exception ex)
            {
                ErrorLogger.logError(ErrorLogger.enumErrorTypes.AppLogicError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
            }
          
        }

       
    }
