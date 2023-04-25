<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Budget/BudgetVarianceReport.aspx.cs" Inherits="BudgetVarianceReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_VarianceReport.exportToExcel();
        }

        function exportgrid1() {
            GridBreakup.exportToExcel();
        }

        
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
   

       
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               Budget Variance Report 

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Project Name</div>
                <div class="col-md-4">
                     <asp:DropDownList ID="ddlProjectName" CssClass="form-control" runat="server" TabIndex="1">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">Year</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProjectYear" CssClass="form-control" runat="server" TabIndex="2">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>
             <div class="row">
                <div class="col-md-2">Month</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProjectMonth" CssClass="form-control" runat="server" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>
             
            </div>
         
            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Search" CssClass="btn btn-default" OnClick="btnSubmit_Click" TabIndex="4"></asp:Button>
                      <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" TabIndex="5"></asp:Button>
                   
                   
                </div>

            </div>
            <br />
            <asp:TextBox ID="txtTotalAmt" runat="server" style="display:none;"></asp:TextBox>
            <div class="row">

                   <center>
                        

        <ogrid:Grid runat="server"  ID="Grid_VarianceReport" OnRowCreated="Grid_VarianceReport_RowCreated" OnRowDataBound="Grid_VarianceReport_RowDataBound" AllowGrouping="true"  ShowColumnsFooter="true" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" CallbackMode="true" AllowPaging="true" PageSize="10" >
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"   />
              
        
            <Columns>  
                <ogrid:Column DataField="SINO" HeaderText="SI No" Width="120px" ></ogrid:Column>
                  <ogrid:Column DataField="Section" HeaderText="Particulars" >
                    <TemplateSettings  TemplateId="SectorTemplate"/>
                </ogrid:Column>
               
                <ogrid:Column DataField="AppAmout" HeaderText="Approved Budget Value" Align="Right"></ogrid:Column>
                <ogrid:Column DataField="ActualAmt" HeaderText="Actual Purchase Value" Align="Right" ></ogrid:Column>
                <ogrid:Column DataField="Variance" HeaderText="Variance(+ fav/- unfav)" Align="Right" ></ogrid:Column>
                           
            </Columns>

               <Templates>
                          <ogrid:GridTemplate ID="SectorTemplate"  runat="server">
            <Template>
                <asp:LinkButton runat="server" ID="Sector"  Text='<%#Container.DataItem["Section"] %>'  CommandArgument='<%#Container.DataItem["Section"] %>' OnClick="Sector_Click"></asp:LinkButton>

            </Template>
            </ogrid:GridTemplate>
                              </Templates>
            
        </ogrid:Grid>
                      <br />
                     <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" TabIndex="6"></asp:Button>
                   <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" tabindex="7" />          
                      </center>
            </div>
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->

            <div runat="server" id="divbudgetbreak" visible="false">
                  <ogrid:Grid runat="server"  ID="GridBreakup"  OnRowCreated="GridBreakup_RowCreated" ShowFooter="true"  OnRowDataBound="GridBreakup_RowDataBound"   AllowGrouping="true"  ShowColumnsFooter="true" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" CallbackMode="true" AllowPaging="true" PageSize="10" >
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"   />
              
        <ScrollingSettings ScrollWidth="100%" />
            <Columns>  
                   <ogrid:Column DataField="Bud_type" HeaderText="Sector"></ogrid:Column>
                <ogrid:Column DataField="Item_Code" HeaderText="Item Code" ></ogrid:Column>
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name"  ></ogrid:Column>
                 <ogrid:Column DataField="Budget_Rate" HeaderText="Budget Rate"  ></ogrid:Column>
                 <ogrid:Column DataField="Budget_Qty" HeaderText="Budget Qty"  ></ogrid:Column>
                 <ogrid:Column DataField="BudgetValues" HeaderText="Budget Amount"  ></ogrid:Column>
                 <ogrid:Column DataField="Purcase_AvgRate" HeaderText="Purcase Avg Rate"  ></ogrid:Column>
                 <ogrid:Column DataField="Purchase_Qty" HeaderText="Purchased Qty"  ></ogrid:Column>
                 <ogrid:Column DataField="PurchaseValues" HeaderText="Purchase Amount"  ></ogrid:Column>
                <ogrid:Column DataField="Variance" HeaderText="Variance(+ fav/- unfav)" Align="Right" ></ogrid:Column>
                           
            </Columns>
                      </ogrid:Grid>
                <br />
                <center>
                   <input onclick="exportgrid1()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel"  />  
                    </center>
            </div>
        </div>

    </div>
    
</asp:Content>
