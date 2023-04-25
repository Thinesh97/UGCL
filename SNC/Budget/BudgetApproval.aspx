<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="BudgetApproval.aspx.cs" Inherits="BudgetApproval" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            GridBudgetApproval.exportToExcel();
        }
        function exportgrid1() {
            GridPrimaryApproval.exportToExcel();
        }
    </script>
     <script type="text/javascript">
         window.setInterval(function () {
             window.location.reload();            
         }, 600000);
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Budget Awaiting for Reporting Approval
            </h3>


        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="GridBudgetApproval" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
             <ExportingSettings ExportAllPages="true" ExportTemplates="true" />
            <Columns>
                 <ogrid:Column HeaderText="Budget ID" runat="server" DataField="Budget_ID" Width="140" >
            <TemplateSettings  TemplateId="Abs_BIDTemplate" />
        </ogrid:Column> 
                 <ogrid:Column DataField="Year" HeaderText="Year" Width="80px" ></ogrid:Column>
                <ogrid:Column DataField="MonthName" HeaderText="Month" Width="80px" ></ogrid:Column>                      
                <ogrid:Column DataField="Project_Code" HeaderText="Project Code" Width="120px" ></ogrid:Column>
                <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width="200px" ></ogrid:Column>
                 <ogrid:Column DataField="Total_Amount" HeaderText="Total Amt" Align="Right" Width="140px" ></ogrid:Column>  
                 <ogrid:Column DataField="Name" HeaderText="Primary Approver"  Width="140px"  ></ogrid:Column> 
           
                <ogrid:Column DataField="P_Status" HeaderText="Status" Width="80px" ></ogrid:Column>                 
            </Columns>
               <Templates>
   
        <ogrid:GridTemplate ID="Abs_BIDTemplate" runat="server">
            <Template>
                <a href="GrandAbstract.aspx?ApprovedID=<%#Container.DataItem["Abs_BID"]%>&BudgetID=<%#Container.DataItem["Budget_ID"]%>"  > <%#Container.DataItem["Budget_ID"] %></a>

             
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                      <br />
                
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>
                 
                   <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" /> 
                      </center>
        </div>
    </div>


    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Budget Awaiting For Primary Approval
            </h3>


        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="GridPrimaryApproval" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
             <ExportingSettings ExportAllPages="true" ExportTemplates="true" />
            <Columns>
                 <ogrid:Column HeaderText="Budget ID" runat="server" Width="140" >
            <TemplateSettings  TemplateId="Abs_BIDTemplate1" />
        </ogrid:Column> 
                <ogrid:Column DataField="Year" HeaderText="Year" Width="80px" ></ogrid:Column>
                <ogrid:Column DataField="MonthName" HeaderText="Month" Width="80px" ></ogrid:Column>                      
                <ogrid:Column DataField="Project_Code" HeaderText="Project Code" Width="120px" ></ogrid:Column>
                <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width="200px" ></ogrid:Column>
                 <ogrid:Column DataField="Total_Amount" HeaderText="Total Amt" Align="Right" Width="140px" ></ogrid:Column>  
                 <ogrid:Column DataField="Name" HeaderText="Primary Approver"  Width="140px"  ></ogrid:Column> 
           
                <ogrid:Column DataField="P_Status" HeaderText="Status" Width="80px" ></ogrid:Column>                 
            </Columns>
               <Templates>
        <ogrid:GridTemplate ID="Abs_BIDTemplate1" runat="server">
            <Template>
             <a href="GrandAbstract.aspx?ApprovedID=<%#Container.DataItem["Abs_BID"]%>&BudgetID=<%#Container.DataItem["Budget_ID"]%>"  > <%#Container.DataItem["Budget_ID"] %></a>
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                      <br />
                
                      <asp:Button ID="btnExportPDFPrimarryapproval" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportPDFPrimarryapproval_Click"></asp:Button>
                 
                   <input onclick="exportgrid1()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" /> 
                      </center>
        </div>
    </div>
</asp:Content>



