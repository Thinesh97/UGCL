<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Procurement/IndentinProcess.aspx.cs" Inherits="IndentinProcess" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <script type="text/javascript">
          function exportgrid() {
              GridIndentList.exportToExcel();
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

                Indent in Process

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="GridIndentList" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <Columns>
                 <ogrid:Column DataField="Indent_No" HeaderText="Indent No" runat="server" Width="110" >
            <TemplateSettings  TemplateId="IndentNoTemplate"/>
        </ogrid:Column> 
                <%--<ogrid:Column DataField="Indent_No" HeaderText="Indent No" Visible="false"></ogrid:Column> --%>
                <ogrid:Column DataField="Ind_date" HeaderText="Date"></ogrid:Column>                
                <ogrid:Column DataField="Budget_ID" HeaderText="Budget ID" ></ogrid:Column>
                <ogrid:Column DataField="ProjectName" HeaderText="Project Name" ></ogrid:Column>
                <ogrid:Column DataField="PreparedBy" HeaderText="Prepared By" ></ogrid:Column>
                 <ogrid:Column DataField="ProcessedBy" HeaderText="Processed By" ></ogrid:Column>
                <ogrid:Column DataField="IndentStatus" HeaderText="Status" ></ogrid:Column>
                
            </Columns>
               <Templates>
        <ogrid:GridTemplate ID="IndentNoTemplate" runat="server">
            <Template>

               <%-- <asp:LinkButton ID="lnkIndentNo" runat="server"   CssClass="gridCB" OnClick="lnkIndentNo_Click"  Text='<%#Container.DataItem["Indent_No"] %>'>


                </asp:LinkButton>--%>
             <a href='Indent.aspx?IndentNo=<%#Container.DataItem["Indent_No"] %>'><%#Container.DataItem["Indent_No"] %> </a>
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                       <br />
                     
                    <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" ></asp:Button>
                    
                <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />    
                      </center>
        </div>
    </div>
</asp:Content>
