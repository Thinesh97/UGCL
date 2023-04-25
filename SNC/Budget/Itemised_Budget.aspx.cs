using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using SNC.ErrorLogger;



public partial class Itemised_Budget : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["SNC_DB_Connection"].ConnectionString);
    DataSet ds = null;
    BudgetBL objBudgetBL = new BudgetBL();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UID"] != null)
            {
                if (Request.QueryString["ID"] != null && Request.QueryString["Type"] != null)
                {
                    if (Request.QueryString["Proj"] != null)
                    {
                        lblprojectName.Text = Request.QueryString["Proj"].ToString();
                    }


                    objBudgetBL = new BudgetBL();
                    ds = new DataSet();
                    bool exists;
                    DataTable DatafilterDt = new DataTable();
                    objBudgetBL.Abs_bud_ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
                    objBudgetBL.load(con, BudgetBL.eLoadSp.SELECT_BUDGETITEM_BY_ABS_BUDID, ref ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        lblMonth.Text = ds.Tables[0].Rows[0]["Month"].ToString();
                        lblYear.Text = ds.Tables[0].Rows[0]["Year"].ToString();
                       
                        DatafilterDt = ds.Tables[0];
                        if (Request.QueryString["Type"] == "Shutter_Print")
                        {

                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Shuttering Materials")).Count() > 0;
                            if (exists)
                            {
                                DataTable Sutterdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Shuttering Materials")
                                            .CopyToDataTable();

                                lblItemisedBudget.Text = "Shuttering Materials";
                                BudgetGridPrint.DataSource = Sutterdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }


                        else if (Request.QueryString["Type"] == "AutoMobiles_Print")
                        {

                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Automobiles")).Count() > 0;
                            if (exists)
                            {
                                DataTable Automobilesdt = DatafilterDt.AsEnumerable()
                                             .Where(r => r.Field<string>("Bud_type") == "Automobiles")
                                             .CopyToDataTable();
                                lblItemisedBudget.Text = "Automobiles";
                                BudgetGridPrint.DataSource = Automobilesdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }

                        else if (Request.QueryString["Type"] == "PlantMachinery_Print")
                        {
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Plant & Machinery")).Count() > 0;
                            if (exists)
                            {
                                DataTable Plantdt = DatafilterDt.AsEnumerable()
                                             .Where(r => r.Field<string>("Bud_type") == "Plant & Machinery")
                                             .CopyToDataTable();
                                lblItemisedBudget.Text = "Plant & Machinery";
                                BudgetGridPrint.DataSource = Plantdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "ConsumableItems_Print")
                        {
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Consumable Items")).Count() > 0;
                            if (exists)
                            {
                                DataTable Consumdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Consumable Items")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Consumable Items";
                                BudgetGridPrint.DataSource = Consumdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }


                        else if (Request.QueryString["Type"] == "ElectricalItems_Print")
                        {

                            // Grid_Elect.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Electrical Items")).Count() > 0;
                            if (exists)
                            {
                                DataTable Electdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Electrical Items")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Electrical Items";
                                BudgetGridPrint.DataSource = Electdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }

                        else if (Request.QueryString["Type"] == "HSDPetrol_Print")
                        {
                            //  Grid_HSD.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("HSD")).Count() > 0;
                            if (exists)
                            {

                                DataTable HSDdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "HSD")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "HSD";
                                BudgetGridPrint.DataSource = HSDdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "OIL_Print")
                        {

                            //  Grid_Printing.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Petrol,Oil & Lubricants")).Count() > 0;
                            if (exists)
                            {

                                DataTable Oildt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Petrol,Oil & Lubricants")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Petrol,Oil & Lubricants";
                                BudgetGridPrint.DataSource = Oildt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }

                        else if (Request.QueryString["Type"] == "Hardware_Print")
                        {
                            //  Grid_Hardware.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Hardware Items")).Count() > 0;
                            if (exists)
                            {

                                DataTable Harddt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Hardware Items")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Hardware Items";
                                BudgetGridPrint.DataSource = Harddt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "Welding_Print")
                        {
                            // Grid_Welding.Visible = true;

                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Welding Electrodes")).Count() > 0;
                            if (exists)
                            {

                                DataTable Weldingdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Welding Electrodes")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Welding Electrodes";
                                BudgetGridPrint.DataSource = Weldingdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "Oxygen_Print")
                        {
                            //Grid_Oxygen.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Oxygen & Acetylene Gas")).Count() > 0;
                            if (exists)
                            {

                                DataTable Oxygendt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Oxygen & Acetylene Gas")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Oxygen & Acetylene Gas";
                                BudgetGridPrint.DataSource = Oxygendt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "Safety_Print")
                        {
                            //Grid_Safety.Visible = true;

                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Safety Items")).Count() > 0;
                            if (exists)
                            {
                                DataTable Safetydt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Safety Items")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Safety Items";
                                BudgetGridPrint.DataSource = Safetydt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "Staff_Print")
                        {
                            //Grid_Staff.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Staff Welfare")).Count() > 0;
                            if (exists)
                            {

                                DataTable Nitter = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Staff Welfare")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Staff Welfare";
                                BudgetGridPrint.DataSource = Nitter;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "Mess_Print")
                        {
                            //Grid_Mess.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Mess Expenditure")).Count() > 0;
                            if (exists)
                            {

                                DataTable Messdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Mess Expenditure")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Mess Expenditure";
                                BudgetGridPrint.DataSource = Messdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;


                        }
                        else if (Request.QueryString["Type"] == "Printing_Print")
                        {
                            //Grid_Printing.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Printing & Stationery")).Count() > 0;
                            if (exists)
                            {

                                DataTable Printingdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Printing & Stationery")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Printing & Stationery";
                                BudgetGridPrint.DataSource = Printingdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "Repairs_Print")
                        {
                            // Grid_Repairs.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Repairs & Maintenance")).Count() > 0;
                            if (exists)
                            {

                                DataTable Repairsdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Repairs & Maintenance")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Repairs & Maintenance";
                                BudgetGridPrint.DataSource = Repairsdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "BOQ_Print")
                        {
                            // Grid_BOQ.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("BOQ Items")).Count() > 0;
                            if (exists)
                            {

                                DataTable BOQdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "BOQ Items")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "BOQ Items";
                                BudgetGridPrint.DataSource = BOQdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "Sanitary_Print")
                        {
                            // Grid_Sanitary.Visible = true;

                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Sanitary Materials")).Count() > 0;
                            if (exists)
                            {

                                DataTable Sanitarydt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Sanitary Materials")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Sanitary Materials";
                                BudgetGridPrint.DataSource = Sanitarydt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "Blasting_Print")
                        {
                            //Grid_Blasting.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Blasting Materials")).Count() > 0;
                            if (exists)
                            {

                                DataTable Blastingdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Blasting Materials")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Blasting Materials";
                                BudgetGridPrint.DataSource = Blastingdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "Furnitures_Print")
                        {
                            //Grid_Furnitures.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Furnitures & Fixtures")).Count() > 0;
                            if (exists)
                            {

                                DataTable Furnituresdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Furnitures & Fixtures")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Furnitures & Fixtures";
                                BudgetGridPrint.DataSource = Furnituresdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "Fixed_Print")
                        {
                            //Grid_Fixed_Assets.Visible = true;

                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Fixed Assets")).Count() > 0;
                            if (exists)
                            {

                                DataTable Fixed_Assetsdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Fixed Assets")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Fixed Assets";
                                BudgetGridPrint.DataSource = Fixed_Assetsdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "Infrastructure_Print")
                        {
                            //  Grid_Infrastructure.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Infrastructure Items")).Count() > 0;
                            if (exists)
                            {

                                DataTable Infrastructuredt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Infrastructure Items")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Infrastructure Items";
                                BudgetGridPrint.DataSource = Infrastructuredt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "Sand_Print")
                        {
                            // Grid_Sand.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Sand")).Count() > 0;
                            if (exists)
                            {

                                DataTable Sanddt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Sand")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Sand";
                                BudgetGridPrint.DataSource = Sanddt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "Jelly_Print")
                        {
                            //   Grid_Jelly.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Jelly/Metal/Aggregates")).Count() > 0;
                            if (exists)
                            {

                                DataTable Jellydt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Jelly/Metal/Aggregates")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Jelly/Metal/Aggregates";
                                BudgetGridPrint.DataSource = Jellydt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "RedSoil_Print")
                        {
                            // Grid_RedSoil.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Red Soil")).Count() > 0;
                            if (exists)
                            {

                                DataTable RedSoildt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Red Soil")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Red Soil";
                                BudgetGridPrint.DataSource = RedSoildt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "Cement_Print")
                        {
                            //Grid_Cement.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Cement")).Count() > 0;
                            if (exists)
                            {

                                DataTable Cementdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Cement")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Cement";
                                BudgetGridPrint.DataSource = Cementdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "Chemicals_Print")
                        {
                            //Grid_Chemicals.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Chemicals")).Count() > 0;
                            if (exists)
                            {

                                DataTable Chemicalsdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Chemicals")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Chemicals";
                                BudgetGridPrint.DataSource = Chemicalsdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "Bricks_Print")
                        {
                            ///  Grid_Bricks.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Bricks")).Count() > 0;
                            if (exists)
                            {

                                DataTable Bricksdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Bricks")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Bricks";
                                BudgetGridPrint.DataSource = Bricksdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "Steels_Print")
                        {
                            //Grid_Steels.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Steels")).Count() > 0;
                            if (exists)
                            {

                                DataTable Steelsdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Steels")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Steels";
                                BudgetGridPrint.DataSource = Steelsdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;

                        }
                        else if (Request.QueryString["Type"] == "OtherConstruction_Print")
                        {
                            // Grid_Other_Construction.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Other Construction Materials")).Count() > 0;
                            if (exists)
                            {
                                DataTable OtherConstructiondt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Other Construction Materials")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Other Construction Materials";
                                BudgetGridPrint.DataSource = OtherConstructiondt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else if (Request.QueryString["Type"] == "Other_Print")
                        {
                            // Grid_Others.Visible = true;
                            exists = DatafilterDt.AsEnumerable().Where(c => c.Field<string>("Bud_type").Equals("Others")).Count() > 0;
                            if (exists)
                            {

                                DataTable Othersdt = DatafilterDt.AsEnumerable()
                                            .Where(r => r.Field<string>("Bud_type") == "Others")
                                            .CopyToDataTable();
                                lblItemisedBudget.Text = "Others";
                                BudgetGridPrint.DataSource = Othersdt;
                                BudgetGridPrint.DataBind();
                            }
                            exists = false;
                        }
                        else
                        {
                            BudgetGridPrint.DataSource = null;
                            BudgetGridPrint.DataBind();
                        }

                    }
                }
            }
            else
            {
                Response.Redirect("../CommonPages/Login.aspx", false);
            }
        }


    }
    decimal TotalPurcValues = Convert.ToDecimal("0.00");
    protected void BudgetGridPrint_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTotalPurchaseValues = (Label)e.Row.FindControl("lblPurchaseValues");
            if (lblTotalPurchaseValues != null)
            {
                TotalPurcValues = TotalPurcValues + Convert.ToDecimal(lblTotalPurchaseValues.Text);
            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalPOV = (Label)e.Row.FindControl("lblTotalPOV");
            if (lblTotalPOV != null)
            {
                TotalPurcValues = Convert.ToDecimal(TotalPurcValues);
                lblTotalPOV.Text = TotalPurcValues.ToString();
            }

        }
    }

}








