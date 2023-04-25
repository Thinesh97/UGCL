<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="POAwaitingApproval.aspx.cs" Inherits="POAwaitingApproval" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            GridPoAwaitingApproval.exportToExcel();
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

                PO Awaiting Approval

            </h3>

        </div>

        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="GridPoAwaitingApproval" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
            <ScrollingSettings ScrollWidth="100%" />
             <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
              <Columns>
                   <ogrid:Column DataField="PONo" HeaderText="PO_NO" runat="server" >
            <TemplateSettings  TemplateId="PaAwaitingApprovalTemplate"/>

        </ogrid:Column> 
         
                <ogrid:Column DataField="PO_DATE" HeaderText="Date" Width="100"></ogrid:Column>
                <ogrid:Column DataField="PaymentTerms" HeaderText="Payment Terms " Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Vendor_name" HeaderText="Vendor  Name" Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Enquiry_Mail_dated" HeaderText="Enquiry Mail Dated" Align="center"  ></ogrid:Column>
                <ogrid:Column DataField="Status" HeaderText="Status" Wrap="true" ></ogrid:Column>
            </Columns>
             <Templates>
        <ogrid:GridTemplate ID="PaAwaitingApprovalTemplate" runat="server">
            <Template>
              <%--  <asp:LinkButton ID="linkPo_No" runat="server" OnClick="linkPo_No_Click" CssClass="gridCB" Text='<%#Container.DataItem["PONo"]%>'>
                </asp:LinkButton>--%>
            <a href='PurchaseOrder.aspx?PoApprovalID=<%#Container.DataItem["PONo"] %>'><%#Container.DataItem["PONo"] %> </a>
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
</asp:Content>
