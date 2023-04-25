<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="DailyMachineRunningHoursList.aspx.cs" Inherits="DailyMachineRunningHoursList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <script type="text/javascript">
          function exportgrid() {
              DRHGrid.exportToExcel();
          }
          function beforedelete() {
              if (confirm("Are you sure want to delete this record?") == false) {
                  return false;
              }
              return true;
          }
    </script>
     <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

              Daily Machine Running Hours/Kms List

            </h3>

        </div>
             <div class="panel-body">
                  <center>
                       
        <ogrid:Grid runat="server" ID="DRHGrid" AutoGenerateColumns="false" OnRowDataBound="DRHGrid_RowDataBound"  CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10" OnDeleteCommand="DRHGrid_DeleteCommand">
            <ScrollingSettings ScrollWidth="100%" />
            <ClientSideEvents  OnBeforeClientDelete="beforedelete"/>
            <ExportingSettings ExportAllPages="true"  ExportTemplates="true" ExportedFilesTargetWindow="New"  
                ColumnsToExport="Date,Name,Asset_Type,Category_Name,Code,Reg_No,Unit,Start_Hour,End_Hour,Hour_Duration,Start_Km,End_Km,Distance_Duration,Issued_Diesel_Qty,UOM,Output,Remarks" />
             <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
           <Columns>
                   <ogrid:Column DataField="Daily_Run_Id"  HeaderText="Edit" runat="server" Width="70" >
            <TemplateSettings  TemplateId="DailyRunIdTemplate"/>
        </ogrid:Column>         
                 <ogrid:Column DataField="Date" HeaderText="Date" Width="90px" Wrap="true"></ogrid:Column>
                 <ogrid:Column DataField="Name" HeaderText="Asset Name" Width="150px" Wrap="true" ></ogrid:Column>
                 <ogrid:Column DataField="Asset_Type" HeaderText="Asset Type" Width="150px" Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Category_Name" HeaderText="Category_Name" Width="150px" Wrap="true" ></ogrid:Column>
                 <ogrid:Column DataField="Code" HeaderText="Asset Code" Width="110px" Wrap="true" ></ogrid:Column>   
                 <ogrid:Column DataField="Reg_No" HeaderText="Reg No" Width="110px" Wrap="true" ></ogrid:Column>                
               <ogrid:Column DataField="Unit" HeaderText="Unit" Width="110px" Wrap="true" ></ogrid:Column>     
                 <ogrid:Column DataField="Start_Hour" HeaderText="Start Hour" Width ="100px" Wrap="true" Align="right"></ogrid:Column>
                 <ogrid:Column DataField="End_Hour" HeaderText="End Hour" Width ="90px" Wrap="true" Align="right"></ogrid:Column>
                <ogrid:Column DataField="Hour_Duration" HeaderText="Hour Duration" Width="120px"  Align="right"  ></ogrid:Column>
                <ogrid:Column DataField="Start_Km" HeaderText="Start Km" Width ="90px" Wrap="true" Align="right"></ogrid:Column>
                <ogrid:Column DataField="End_Km" HeaderText="End Km" Width ="80px" Wrap="true" Align="right"></ogrid:Column>
               <ogrid:Column DataField="Distance_Duration" HeaderText="Distance Duration" Width="135px"  Align="right" ></ogrid:Column>
                <ogrid:Column DataField="Issued_Diesel_Qty" HeaderText="Issued Diesel" Width="120px" Wrap="true" Align="right"></ogrid:Column>
                <ogrid:Column DataField="UOM" HeaderText="UOM"  Width ="70px" Wrap="true"></ogrid:Column>
                <ogrid:Column DataField="Output" HeaderText="Output"  Width ="100px" Wrap="true"></ogrid:Column>
               <ogrid:Column  DataField="Remarks" HeaderText="Remarks"  Wrap="true"></ogrid:Column>
               <ogrid:Column  AllowDelete="true" HeaderText="Delete" Width="90"   Wrap="true"></ogrid:Column>
            </Columns>
              <Templates>
        <ogrid:GridTemplate ID="DailyRunIdTemplate" runat="server">
            <Template>      
             
                  <asp:HyperLink ID="lnkDailyRunId" runat="server"     CssClass="gridCB"  Text='<%#Container.DataItem["Daily_Run_Id"] %>'>Edit</asp:HyperLink> 
            </Template>
        </ogrid:GridTemplate>

    </Templates>

        </ogrid:Grid>
                       <br />
                        <a href="DailyMachineRunningHoursCreate.aspx" id="lnkbtnAdd" runat="server" class="btn btn-default">Add New</a>
                 <%--    <asp:LinkButton ID="lnkbtnAdd" Text="Add New" PostBackUrl="~/Asset/DailyMachineRunningHours.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>--%>
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>
                     <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
                      </center>
                 </div>
         </div>
</asp:Content>