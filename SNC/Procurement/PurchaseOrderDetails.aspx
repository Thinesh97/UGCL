<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderDetails.aspx.cs" Inherits="SNC.Procurement.PurchaseOrderDetails" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript">
         function exportgrid() {
             gvPODetails.exportToExcel();
         }
       
    </script>
         <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

              PO List

            </h3>

        </div>
             <div class="panel-body">
                  <center>
        <ogrid:Grid runat="server" ID="gvPODetails"  CallbackMode="false" AutoGenerateColumns="false"  FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
           <ScrollingSettings ScrollWidth="100%" ScrollHeight="400" />
          
          
             <ExportingSettings ExportAllPages="true" ExportTemplates="true"  />
            <Columns>
                 <ogrid:Column  HeaderText="PO No" DataField="PONo" runat="server" Width="150px">
            
        </ogrid:Column>      
                  
                <ogrid:Column DataField="PODate" HeaderText="PO Date" Width="125"></ogrid:Column>
                <ogrid:Column DataField="Vendor_name" HeaderText="Vendor  Name" Wrap="true" Width="185px" ></ogrid:Column>
                 <ogrid:Column DataField="Indent_No" HeaderText="Indent No" Wrap="true" Width="185px" ></ogrid:Column>
                <ogrid:Column DataField="NetTotalAmt" HeaderText="PO Amount" ></ogrid:Column>
                  <ogrid:Column DataField="Status" HeaderText="Status " Wrap="true" ></ogrid:Column>
              
            </Columns>
            
        </ogrid:Grid>
                          <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />     
                   <asp:Button ID="btn_GenquotationItemsList"  runat="server" Text="Back" class="btn btn-default" OnClick="btn_GenquotationItemsList_Click"/>  
             
                      </center>
                   

                 </div>
    </div>
</asp:Content>
