<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Budget/Local_MRN_List.aspx.cs" Inherits="Local_MRN_List" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            GridBudgetList.exportToExcel();
        }
        function exportgridSubmit() {
            GridSubmitted.exportToExcel();
        }
        function exportgridSendback() {
            GridSendBack.exportToExcel();
        }
        function exportgridApprovedBud() {
            GridApproved.exportToExcel();
        }


    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                List Of Local MRN

            </h3>


        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="GridBudgetList" AllowGrouping="true" AutoGenerateColumns="false" 
            FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" ScrollHeight="400" />
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" 
                 ColumnsToExport="Project_Name,Year,MonthName,Sector_Name,Amount,Status"/>
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; 
                text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black;
                 border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <Columns>
                 <ogrid:Column HeaderText="Local MRN ID" DataField="Local_MRN_ID" runat="server" Width="100px" >
            <TemplateSettings  TemplateId="Abs_BIDTemplate" />
        </ogrid:Column>    
                 <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width="150px" ></ogrid:Column>
                 <ogrid:Column DataField="Year" HeaderText="Year" Width="120" ></ogrid:Column>
                 <ogrid:Column DataField="MonthName" HeaderText="Month Name" Width="120" ></ogrid:Column>               
                 <ogrid:Column DataField="Amount" HeaderText="Amount" Width="120" ></ogrid:Column>
                 <ogrid:Column DataField="Status" HeaderText="Status" Width="120" ></ogrid:Column>
            </Columns>
               <Templates>
        <ogrid:GridTemplate ID="Abs_BIDTemplate" runat="server">
            <Template>
                <a href='Local_MRN.aspx?ID=<%#Container.DataItem["Local_MRN_ID"] %>'><%#Container.DataItem["Local_MRN_ID"] %></a>
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                      <br />
                <a href="Local_MRN.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Create Local MRN</a>
               
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>
                   
                 <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" /> 
                      </center>
        </div>
    </div>


    <br />
    <div class="panel panel-default" style="display: none">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                List Of Submitted Monthly Budget

            </h3>


        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="GridSubmitted" AllowGrouping="true" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" ScrollHeight="400"/>
             <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"/>
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

           <Columns>
                <%-- <ogrid:Column HeaderText="Abstract Budget ID" runat="server" Width="140" >
            <TemplateSettings  TemplateId="SubmitBugTemplate" />
        </ogrid:Column> --%>   
                 <ogrid:Column DataField="Budget_ID" HeaderText="Budget ID" Width="150px"  ></ogrid:Column>
                 <ogrid:Column DataField="Year" HeaderText="Year" Width="80px" ></ogrid:Column>
                <ogrid:Column DataField="MonthName" HeaderText="Month" Width="80px" ></ogrid:Column>                      
                <ogrid:Column DataField="Project_Code" HeaderText="Project Code" Width="120px"  ></ogrid:Column>
                <ogrid:Column DataField="Project_Name" HeaderText="Project Name"  ></ogrid:Column>
                 <ogrid:Column DataField="Total_Amount" HeaderText="Total Amt"  Align="Right" Width="100px" ></ogrid:Column>  
                 <ogrid:Column DataField="Name" HeaderText="Approver Name"  Width="140px"  ></ogrid:Column> 
                <ogrid:Column DataField="Approval_Status" HeaderText="Approval Status"  Width="140px"  ></ogrid:Column>             
                <ogrid:Column DataField="Status" HeaderText="Status"   ></ogrid:Column>                 
            </Columns>
               <Templates>
        <ogrid:GridTemplate ID="SubmitBugTemplate" runat="server">
            <Template>
            <%--    <asp:LinkButton ID="lnkAbs_BID" runat="server"   CssClass="gridCB"   OnClick="lnkBudgetID_Click"  Text='<%#Container.DataItem["Abs_BID"] %>'>
                </asp:LinkButton>--%>
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                      <br />
              
                      <asp:Button ID="btnExportToPdfSubmitMoBudget" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPdfSubmitMoBudget_Click"></asp:Button>
             
                <input onclick="exportgridSubmit()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" /> 
                      </center>
        </div>
    </div>

    <br />
    <div class="panel panel-default" style="display: none">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Recevied For Modification 

            </h3>


        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="GridSendBack" AllowGrouping="true" OnRowDataBound="GridSendBack_RowDataBound" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" ScrollHeight="400" />
             <ExportingSettings ExportAllPages="true" ExportTemplates="true"  ExportedFilesTargetWindow="New"/>
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

         <Columns>
                 <ogrid:Column HeaderText="Abstract Budget ID" DataField="Abs_BID" runat="server" Width="150px" >
            <TemplateSettings  TemplateId="SendBugTemplate" />
        </ogrid:Column>    
                 <ogrid:Column DataField="Budget_ID" HeaderText="Budget ID" Width="120" >
                     <TemplateSettings  TemplateId="BudgetIDTemplate"/>
                 </ogrid:Column>
                 <ogrid:Column DataField="Year" HeaderText="Year" Width="80px" ></ogrid:Column>
                <ogrid:Column DataField="MonthName" HeaderText="Month" Width="80px" ></ogrid:Column>                      
                <ogrid:Column DataField="Project_Code" HeaderText="Project Code" Width="120px" ></ogrid:Column>
                <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width="180px" ></ogrid:Column>
                 <ogrid:Column DataField="Total_Amount" HeaderText="Total Amt" Width="100px" Align="Right" ></ogrid:Column>  
                 <ogrid:Column DataField="Name" HeaderText="Approver Name"  Width="140px"  ></ogrid:Column> 
                  <ogrid:Column DataField="Approval_Status" HeaderText="Approval Status"  Width="130px" ></ogrid:Column> 
                <ogrid:Column DataField="Status" HeaderText="Status" Width="80px" ></ogrid:Column>                 
            </Columns>
               <Templates>
        <ogrid:GridTemplate ID="SendBugTemplate" runat="server">
            <Template>
                  <asp:HyperLink ID="lnkAbsBID" runat="server"     CssClass="gridCB"  Text='<%#Container.DataItem["Abs_BID"] %>'></asp:HyperLink>  
               <%--  <a href='MonthlyBudget.aspx?ID=<%#Container.DataItem["Abs_BID"] %> &BudgetID=<%#Container.DataItem["Budget_ID"] %> '><%#Container.DataItem["Abs_BID"] %></a> --%>             
            </Template>
        </ogrid:GridTemplate>
        <ogrid:GridTemplate ID="BudgetIDTemplate" runat="server">
            <Template>
                <asp:Label ID="lblBudgetID" runat="server" Text='<%#Container.DataItem["Budget_ID"] %>'></asp:Label>
            </Template>
        </ogrid:GridTemplate>
    </Templates>
        </ogrid:Grid>
                      <br />
               
                      <asp:Button ID="btnSendbackforMod" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnSendbackforMod_Click"></asp:Button>
               
                  <input onclick="exportgridSendback()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                      </center>
        </div>
    </div>

    <br />
    <div class="panel panel-default" style="display: none">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Approved Budget

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="GridApproved" AllowGrouping="true" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" ScrollHeight="400" />
             <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

              <Columns>
                 <ogrid:Column HeaderText="Abstract Budget ID" DataField="Abs_BID" runat="server" Width="150px" >
            <TemplateSettings  TemplateId="ApprovedTemplate" />
        </ogrid:Column>    
                 <ogrid:Column DataField="Budget_ID" HeaderText="Budget ID" Width="120" ></ogrid:Column>
                 <ogrid:Column DataField="Year" HeaderText="Year" Width="80px" ></ogrid:Column>
                <ogrid:Column DataField="MonthName" HeaderText="Month" Width="80px" ></ogrid:Column>                      
                <ogrid:Column DataField="Project_Code" HeaderText="Project Code" Width="120px" ></ogrid:Column>
                <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width="180px" ></ogrid:Column>
                <ogrid:Column DataField="Total_Amount" HeaderText="Total Amt" Width="100px" Align="Right" ></ogrid:Column>
                 <%--<ogrid:Column DataField="" HeaderText="Total Purchase From HO" ></ogrid:Column>
                 <ogrid:Column DataField="" HeaderText="Total Purchase From Local" ></ogrid:Column>  --%>
                 <ogrid:Column DataField="Name" HeaderText="Approver Name"  Width="140px"   ></ogrid:Column> 
                  <ogrid:Column DataField="Approval_Status" HeaderText="Approval Status"  Width="130px" ></ogrid:Column> 
                <ogrid:Column DataField="Status" HeaderText="Status" Width="80px" ></ogrid:Column>                 
            </Columns>
               <Templates>
        <ogrid:GridTemplate ID="ApprovedTemplate" runat="server">
            <Template>
             <a href='MonthlyBudget.aspx?Abs_BID=<%#Container.DataItem["Abs_BID"] %>&MFYRQ=<%#Container.DataItem["Budget_MR_ID"].ToString() != string.Empty ? 1 : 0 %>&BudgetID=<%#Container.DataItem["Budget_ID"] %> '><%#Container.DataItem["Abs_BID"] %></a>
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                      <br />
              
                     <asp:Button ID="btnapprovedBudExToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnapprovedBudExToPDF_Click"></asp:Button>

                <input onclick="exportgridApprovedBud()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                      </center>
        </div>
    </div>
</asp:Content>



