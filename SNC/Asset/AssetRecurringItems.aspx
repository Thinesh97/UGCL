<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="AssetRecurringItems.aspx.cs" Inherits="AssetRecurringItems" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            GridRecurring.exportToExcel();
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

              Asset Recurring Items

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="GridRecurring"  AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
            <ScrollingSettings ScrollWidth="100%" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>  
            <Columns>
                <ogrid:Column DataField="Item_Name" HeaderText="Item Name" ></ogrid:Column>
                <ogrid:Column DataField="Item_Code" HeaderText="Item Code" ></ogrid:Column>
                <ogrid:Column DataField="Name" HeaderText="Assigned Asset " ></ogrid:Column>
                <ogrid:Column DataField="Asset_Code" HeaderText="Assigned Asset Code" ></ogrid:Column>
                <ogrid:Column DataField="Standard" HeaderText="Standard Hrs/Kms" ></ogrid:Column>
                <ogrid:Column DataField="TotalDailyRun" HeaderText="Duration" ></ogrid:Column>
                <ogrid:Column DataField="Service_Date" HeaderText="Service" DataFormatString="{0:dd-MM-yyy}" ></ogrid:Column>
            </Columns>
        </ogrid:Grid>
                      <br />
                       <center>
                     
                       <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>
                   <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />    
                </center>
                      </center>
        </div>
</asp:Content>
