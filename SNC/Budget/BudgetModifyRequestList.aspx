<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Budget/BudgetModifyRequestList.aspx.cs" Inherits="BudgetModifyRequestList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            GridBudgetBindList.exportToExcel();
        }
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                List Of Budget Modification Request

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="GridBudgetBindList" AllowGrouping="true" OnRowDataBound="GridBudgetBindList_RowDataBound" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="80%" />
                <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" ColumnsToExport="Budget_MR_ID,Project_Code,Project_Name,Budget_ID,RequestBy,ApprovedBy,Reason" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <Columns>   
                 <ogrid:column HeaderText="BudgetMRID"  DataField="Budget_MR_ID" Width="120px" >
                    <TemplateSettings  TemplateId="BudgetIDTemplate"/>
                </ogrid:column>          
                 <ogrid:Column DataField="Project_Code" HeaderText="Project Code" ></ogrid:Column>
                 <ogrid:Column DataField="Project_Name" HeaderText="Project_Name" ></ogrid:Column>
                <ogrid:Column DataField="Budget_ID" HeaderText="Budget ID" ></ogrid:Column>
                <ogrid:Column DataField="RequestBy" HeaderText="Requested By" ></ogrid:Column>
                <ogrid:Column DataField="ApprovedBy" HeaderText="Approved By" ></ogrid:Column> 
                <ogrid:Column DataField="Reason" HeaderText="Reason" ></ogrid:Column>
                   
            </Columns>
                     <Templates>
        <ogrid:GridTemplate ID="BudgetIDTemplate" runat="server">
            <Template>                  
                  <asp:HyperLink ID="lnkBudgetMRID" runat="server"     CssClass="gridCB"  Text='<%#Container.DataItem["Budget_MR_ID"] %>'></asp:HyperLink>          
            </Template>
            </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                      <br />
                  <a href="BudgetModifyRequest.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New Request</a>
                    <%--  <asp:LinkButton ID="lnkbtnAdd" Text="Add New Request" CssClass="btn btn-default" PostBackUrl="~/Budget/BudgetModifyRequest.aspx" runat="server"></asp:LinkButton>--%>
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>
               
                  <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" /> 
                      </center>
        </div>
    </div>
</asp:Content>
