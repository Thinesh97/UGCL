<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="Stock List.aspx.cs" Inherits="Stock_List" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_StockDetails.exportToExcel();
        }

        function exportMRNStockGrid() {
            Grid_MRNStockList.exportToExcel();
        }
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Stock List

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Grid_StockDetails" AutoGenerateColumns="false" OnRowDataBound="Grid_StockDetails_RowDataBound" FolderStyle="../Gridstyles/grand_gray"  AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings  ScrollWidth="100%" ScrollHeight="400"  />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"  ExportedFilesTargetWindow="New" 
                   ColumnsToExport="Stock_ID,StockDate,Sector_Name,Category_Name,Item_Name,Item_Code,Vendor_ID,Vendor_name,Project_Name,UOMName,Bill_No,Rate,Updated_QTY,Remarks"  />
            <Columns>
                      <ogrid:Column HeaderText="Stock Id" DataField="Stock_ID" runat="server" Width="90" >
            <TemplateSettings  TemplateId="StockIDTemplate"/>
        </ogrid:Column>    
                <ogrid:Column DataField="StockDate" HeaderText="Stock Date" Width="100"></ogrid:Column>
                  <ogrid:Column DataField="Sector_Name" HeaderText="Sector" Width="130"></ogrid:Column>
                 <ogrid:Column DataField="Category_Name" HeaderText="Category Name" Width="130"></ogrid:Column>
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name" Width="110"></ogrid:Column>
                <ogrid:Column DataField="Item_Code"  HeaderText="Item Code" Width="110"></ogrid:Column>
                <ogrid:Column DataField="Vendor_name" HeaderText="Vendor Name" Width="180px" ></ogrid:Column>
                <ogrid:Column DataField="Vendor_ID" HeaderText="Vendor Code" Width="180px" ></ogrid:Column>
                   <ogrid:Column DataField="Project_Name" HeaderText="Project Name" ></ogrid:Column>
                <ogrid:Column DataField="UOMName" HeaderText="UOM" Width="90"></ogrid:Column>
                <ogrid:Column DataField="Bill_No" HeaderText="Bill Number" Width="110"></ogrid:Column>
                 <ogrid:Column DataField="Rate" HeaderText="Rate" Align="right" Width="90"></ogrid:Column>
                 <ogrid:Column DataField="Updated_QTY" HeaderText="Available Qty" Align="right" Width="120"></ogrid:Column>
                <ogrid:Column DataField="Remarks" HeaderText="Remarks" ></ogrid:Column>              
                       </Columns>
                 <Templates>
        <ogrid:GridTemplate ID="StockIDTemplate" runat="server">
            <Template>           
                  <asp:HyperLink ID="lnkStockID" runat="server"     CssClass="gridCB"  Text='<%#Container.DataItem["Stock_ID"] %>'></asp:HyperLink>     
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                      <br />
                <a href="Stock.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New Stock</a>
                   
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Click" CssClass="btn btn-default"></asp:Button>
                    <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />      
                      </center>
        </div>
        <br />
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

              MRN Stock List

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Grid_MRNStockList" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray"  AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings ScrollWidth="100%" ScrollHeight="400"  />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true"   />
            <Columns>
                <ogrid:Column DataField="MRN_NO" HeaderText="MRN NO" Width="110px"></ogrid:Column> 
                <ogrid:Column HeaderText="Stock Id" DataField="Stock_ID" runat="server" Width="90px" > </ogrid:Column>    
                <ogrid:Column DataField="StockDate" HeaderText="Stock Date" Width="100px"></ogrid:Column>
                <ogrid:Column DataField="Sector_Name" HeaderText="Sector" Width="130"></ogrid:Column>
                <ogrid:Column DataField="Category_Name" HeaderText="Category Name" Width="130px"></ogrid:Column>
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name" Wrap="true" Width="180px"></ogrid:Column>
                <ogrid:Column DataField="Vendor_name" HeaderText="Vendor Name" Width="220px" Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="UOMName" HeaderText="UOM" Width="90px"></ogrid:Column>
                <ogrid:Column DataField="Accepted_Qty" HeaderText="Available Qty" Align="right" Width="120px"></ogrid:Column>
    
            </Columns>
                 <Templates>
       
    </Templates>
        </ogrid:Grid>
                      <br />
                   
                      <asp:Button ID="btnMRNStockListExportToPDF" runat="server" Text="Export To PDF" OnClick="btnMRNStockListExportToPDF_Click"  CssClass="btn btn-default"></asp:Button>
                    <input onclick="exportMRNStockGrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />      
                      </center>
        </div>
         <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

              MRN Consolidated Stock List

            </h3>

        </div>
        <div class="panel-body">
            <center>
     <ogrid:Grid runat="server" ID="Grid_Consolidated_Stock" AutoGenerateColumns="false" OnRowDataBound="Grid_StockDetails_RowDataBound" FolderStyle="../Gridstyles/grand_gray"  AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings  ScrollWidth="100%" ScrollHeight="400"  />
           <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
             <ScrollingSettings ScrollWidth="100%" ScrollHeight="400"  />
            <Columns>
                  <ogrid:Column DataField="Category_Name" HeaderText="Category Name " Width="100"></ogrid:Column>
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name " Width="100"></ogrid:Column>
                  <ogrid:Column DataField="UOMName" HeaderText="UOM" Width="130"></ogrid:Column>
                 <ogrid:Column DataField="Accepted_Qty" HeaderText="Total Available Qty" Width="130"></ogrid:Column>
               
                       </Columns>
                 <Templates>
    </Templates>
        </ogrid:Grid>
                      <br />
                   
                      <asp:Button ID="Button1" runat="server" Text="Export To PDF" OnClick="btnMRNStockListExportToPDF_Click"  CssClass="btn btn-default"></asp:Button>
                    <input onclick="exportMRNStockGrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />      
                      </center>
        </div>

    </div>
</asp:Content>
