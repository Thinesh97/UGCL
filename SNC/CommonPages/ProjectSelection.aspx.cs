using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections;
using System.Configuration;
using Bussinesslogic;
using BusinessLayer;
using DataAccess;
using SNC.ErrorLogger;
using Obout.Grid;
using System.Security.Cryptography;
using System.Threading;

public partial class ProjectSelection : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    ProjectBL objProjectBL = null;
    DataSet ds = null;
    public static string html = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                BindProjectList();
                BindProjectList_Closed();
            }
        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }

    protected void BindProjectList()
    {

        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            DataSet datads = null;
            objProjectBL.Project_Code = "P-0049/KA/19-20/WSS/BWSSB";
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_DETAILS_BY_ID, ref datads);
            DataTable Dt1 = new DataTable();
            Dt1 = datads.Tables[0].Clone();
            Dt1.Clear();
            if (Session["UID"] != null)
            {
                objProjectBL.UserID = Convert.ToInt32(Session["UID"]);
                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_USER_DETAILS_BY_ID, ref ds);
                if (ds.Tables[0].Rows[0]["Role"].ToString() == "Other")
                {
                    string str = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                    char[] spearator = { ',' };
                    String[] strlist = str.Split(spearator);
                    foreach (var items in strlist)
                    {
                        objProjectBL.Project_Code = items;

                        objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_DETAILS_BY_ID, ref datads);
                        foreach (DataRow row in datads.Tables[0].Rows)
                        {
                            if (Convert.ToInt32(row.ItemArray[0].ToString()) < 250)
                            {
                                Dt1.Rows.Add(row.ItemArray);
                            }
                        }

                    }
                    html = string.Empty;
                    string childdiv = string.Empty;
                    lblOnGoingProjectCount.Text = datads.Tables[0].Rows.Count.ToString();
                    for (int i = 0; i <= datads.Tables[0].Rows.Count - 1; i++)
                    {
                        var ProjectName = datads.Tables[0].Rows[i]["Project_Name"].ToString();
                        var Project_Code = datads.Tables[0].Rows[i]["Project_Code"].ToString();
                        childdiv += "<a style=\"width:305px;margin-right: 5px\" class=\"btn btn-primary radisresize\" href=\"../CommonPages/Home.aspx?Project_Code=" + Project_Code + "\">" + ProjectName + "</a>";


                    }
                    divOnGoingProject.InnerHtml = childdiv;
                }
                else
                {
                    objProjectBL = new ProjectBL();
                    ds = new DataSet();
                    //objProjectBL.UserID =Convert.ToInt32(Request.QueryString["ID"]);
                    objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL, ref ds);
                    html = string.Empty;
                    string childdiv = string.Empty;
                    lblOnGoingProjectCount.Text = ds.Tables[0].Rows.Count.ToString();
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        var ProjectName = ds.Tables[0].Rows[i]["Project_Name"].ToString();
                        var Project_Code = ds.Tables[0].Rows[i]["Project_Code"].ToString();
                        childdiv += "<a style=\"width:305px;margin-right: 5px\" class=\"btn btn-primary radisresize\" href=\"../CommonPages/Home.aspx?Project_Code=" + Project_Code + "\">" + ProjectName + "</a>";


                    }
                    divOnGoingProject.InnerHtml = childdiv;

                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    protected void BindProjectList_Closed()
    {

        try
        {
            objProjectBL = new ProjectBL();
            ds = new DataSet();
            DataSet datads = null;
            objProjectBL.Project_Code = "P-0049/KA/19-20/WSS/BWSSB";
            objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_DETAILS_BY_ID, ref datads);
            DataTable Dt1 = new DataTable();
            Dt1 = datads.Tables[0].Clone();
            Dt1.Clear();
            if (Session["UID"] != null)
            {
                objProjectBL.UserID = Convert.ToInt32(Session["UID"]);
                objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_USER_DETAILS_BY_ID, ref ds);
                if (ds.Tables[0].Rows[0]["Role"].ToString() == "Other")
                {
                    string str = ds.Tables[0].Rows[0]["Project_Code"].ToString();
                    char[] spearator = { ',' };
                    String[] strlist = str.Split(spearator);
                    foreach (var items in strlist)
                    {
                        objProjectBL.Project_Code = items;

                        objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_DETAILS_BY_ID_CLOSED_PROJECTS, ref datads);
                        foreach (DataRow row in datads.Tables[0].Rows)
                        {
                            if (Convert.ToInt32(row.ItemArray[0].ToString()) < 250)
                            {
                                Dt1.Rows.Add(row.ItemArray);
                            }
                        }

                    }
                    html = string.Empty;
                    string childdiv = string.Empty;
                    lblCompletedProjects.Text = datads.Tables[0].Rows.Count.ToString();
                    for (int i = 0; i <= datads.Tables[0].Rows.Count - 1; i++)
                    {
                        var ProjectName = datads.Tables[0].Rows[i]["Project_Name"].ToString();
                        var Project_Code = datads.Tables[0].Rows[i]["Project_Code"].ToString();
                        childdiv += "<a style=\"width:305px;margin-right: 5px\" class=\"btn btn-primary radisresize\" href=\"../CommonPages/Home.aspx?Project_Code=" + Project_Code + "\">" + ProjectName + "</a>";


                    }
                    Div_CompletedProjects.InnerHtml = childdiv;
                }
                else
                {
                    objProjectBL = new ProjectBL();
                    ds = new DataSet();
                    //objProjectBL.UserID =Convert.ToInt32(Request.QueryString["ID"]);
                    objProjectBL.load(con, ProjectBL.eLoadSp.SELECT_PROJECT_ALL_CLOSED_PROJECTS, ref ds);
                    html = string.Empty;
                    string childdiv = string.Empty;
                    lblCompletedProjects.Text = ds.Tables[0].Rows.Count.ToString();
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        var ProjectName = ds.Tables[0].Rows[i]["Project_Name"].ToString();
                        var Project_Code = ds.Tables[0].Rows[i]["Project_Code"].ToString();
                        childdiv += "<a style=\"width:305px;margin-right: 5px\" class=\"btn btn-primary radisresize\" href=\"../CommonPages/Home.aspx?Project_Code=" + Project_Code + "\">" + ProjectName + "</a>";


                    }
                    Div_CompletedProjects.InnerHtml = childdiv;

                }
            }

        }
        catch (Exception ex)
        {
            ErrorLogger.logError(ErrorLogger.enumErrorTypes.DBSelectError, ex, this.Page.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Session["UID"] != null ? Session["UID"].ToString() : string.Empty);
        }
    }
    //protected void btnPro_Click(object sender, EventArgs e)
    //{
    //    if (Session["Project_Code"] != null)
    //    {
    //        Response.Redirect("~/CommonPages/Home.aspx", true);
    //    }
    //}

    //protected void ddlProjects_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //if (ddlProjects.SelectedIndex != 0)
    //    //{
    //    //    Session["Project_Code"] = ddlProjects.SelectedValue;
    //    //}
    //    //else
    //    //{
    //    //    Session["Project_Code"] = null;
    //    //}
    //}
}
