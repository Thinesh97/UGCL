<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="MINList.aspx.cs" Inherits="MINList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            GridMINList.exportToExcel();
        }
        function exportgridMin() {
            ItemList_Grid.exportToExcel();
        }
    </script>

    <div class="panel panel-default">
        <div   class="panel-body">
            <div class="col-lg-12" style="text-align:center">  
            <a href="MINRequest.aspx" runat="server" id="A1" class="btn btn-default">Add new Martial Request</a>  
        <%--      <a style="padding-left:30px" href="MIN.aspx" runat="server" id="A2" class="btn btn-default">Issue Martial Note    </a>  --%>
                </div>
        </div>


       <%-- <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Material Issue Note List

            </h3>

        </div>--%>
          <div class="panel-body" id="Div1" runat="server">
            <center>
        <ogrid:Grid runat="server" ID="Grid_PendingMSR" AutoGenerateColumns="false" OnRowDataBound="Grid_PendingMSR_RowDataBound" CallbackMode="false" FolderStyle="../Gridstyles/grand_gray"  AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings  ScrollWidth="100%" ScrollHeight="400" />
              <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true" 
                   ColumnsToExport="MIN_No,MIN_Date,Project_Name,Issue_To,Subcon_name,IssueToFor,Recoverable,Remarks"  />

                                                                                                                      
            <Columns>
                 <ogrid:Column  DataField="MIN_No" HeaderText="MIN No" runat="server" Wrap="true" Width="160px" >
            <TemplateSettings  TemplateId="MIN_Pending_NoTemplate"/>
        </ogrid:Column> 
         
                <ogrid:Column DataField="MIN_Date" HeaderText="MIN Date" Width="100"  ></ogrid:Column>
                <ogrid:Column DataField="Project_Name" HeaderText="Project Name" ></ogrid:Column>
                <ogrid:Column DataField="Status" HeaderText="Status" Width="120"></ogrid:Column>
                 <ogrid:Column DataField="Issue_To" HeaderText="Issue To" Width="120"></ogrid:Column>
                   <ogrid:Column DataField="Subcon_name" HeaderText="Sub Contractor"  Width="180px"></ogrid:Column> 
                   <ogrid:Column DataField="IssueToFor" HeaderText="Issue To/For" Width="120"></ogrid:Column>
                <ogrid:Column DataField="Remarks" HeaderText="Remarks" Width="120"></ogrid:Column>
                   <ogrid:Column  DataField="Recoverable" HeaderText="Recoverable" Width="160px" >
            <TemplateSettings  TemplateId="RecoverableTemplate_Pending"/>
        </ogrid:Column>
             
               
                            
            </Columns>
            <Templates>
        <ogrid:GridTemplate ID="MIN_Pending_NoTemplate" runat="server">
            <Template>   
                  <asp:HyperLink ID="lnkMINNo" runat="server"     CssClass="gridCB"  Text='<%#Container.DataItem["MIN_No"] %>' > </asp:HyperLink> 
            </Template>
        </ogrid:GridTemplate>
        <ogrid:GridTemplate ID="RecoverableTemplate_Pending" runat="server">
            <Template>   
               <asp:Label runat="server" ID="lblRecoverable" Text='<%#Container.DataItem["Recoverable"].ToString() == "True" ? "Yes" : "No" %>'  > </asp:Label>
            </Template>
        </ogrid:GridTemplate>

    </Templates>
          
    
        </ogrid:Grid>
                      <br />
               <%-- <a href="MIN.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New MIN</a>--%>                  
                      <asp:Button ID="Button1" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>                   
                 <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                <asp:Button ID="Button2"  runat="server" Text="Generate MIN Items List" OnClick="GenMinItem_Click" CssClass="btn btn-default"/>    
                      </center>
              </div>
        <div class="panel-body" id="diff" runat="server">
            <center>
        <ogrid:Grid runat="server" ID="GridMINList" AutoGenerateColumns="false" OnRowDataBound="GridMINList_RowDataBound" CallbackMode="false" FolderStyle="../Gridstyles/grand_gray"  AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <ScrollingSettings  ScrollWidth="100%" ScrollHeight="400" />
              <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
               <ExportingSettings ExportAllPages="true" ExportTemplates="true" 
                   ColumnsToExport="MIN_No,MIN_Date,Project_Name,Issue_To,Subcon_name,IssueToFor,Recoverable,Remarks"  />

                                                                                                                      
            <Columns>
                 <ogrid:Column  DataField="MIN_No" HeaderText="MIN No" runat="server" Wrap="true" Width="160px" >
            <TemplateSettings  TemplateId="MIN_NoTemplate"/>
        </ogrid:Column> 
         
                <ogrid:Column DataField="MIN_Date" HeaderText="MIN Date" Width="100"  ></ogrid:Column>
                <ogrid:Column DataField="Project_Name" HeaderText="Project Name" ></ogrid:Column>
                 <ogrid:Column DataField="Status" HeaderText="Status" Width="120"></ogrid:Column>
                <ogrid:Column DataField="Issue_To" HeaderText="Issue To" Width="120"></ogrid:Column>               
                   <ogrid:Column DataField="Subcon_name" HeaderText="Sub Contractor"  Width="180px"></ogrid:Column> 
                   <ogrid:Column DataField="IssueToFor" HeaderText="Issue To/For" Width="120"></ogrid:Column>
                <ogrid:Column DataField="Remarks" HeaderText="Remarks" Width="120"></ogrid:Column>
                   <ogrid:Column  DataField="Recoverable" HeaderText="Recoverable" Width="160px" >
            <TemplateSettings  TemplateId="RecoverableTemplate"/>
        </ogrid:Column>
             
               
                            
            </Columns>
            <Templates>
        <ogrid:GridTemplate ID="MIN_NoTemplate" runat="server">
            <Template>   
                  <asp:HyperLink ID="lnkMINNo" runat="server"     CssClass="gridCB"  Text='<%#Container.DataItem["MIN_No"] %>' > </asp:HyperLink> 
            </Template>
        </ogrid:GridTemplate>
        <ogrid:GridTemplate ID="RecoverableTemplate" runat="server">
            <Template>   
               <asp:Label runat="server" ID="lblRecoverable" Text='<%#Container.DataItem["Recoverable"].ToString() == "True" ? "Yes" : "No" %>'  > </asp:Label>
            </Template>
        </ogrid:GridTemplate>

    </Templates>
          
    
        </ogrid:Grid>
                      <br />
               <%-- <a href="MIN.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New MIN</a>--%>                  
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>                   
                 <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                <asp:Button ID="GenMinItem"  runat="server" Text="Generate MIN Items List" OnClick="GenMinItem_Click" CssClass="btn btn-default"/>    
                      </center>
            <div id="ItemList_Gv" runat="server" visible="false" >
            <br />


            <center>
                <ogrid:Grid runat="server" ID="ItemList_Grid" CallbackMode="true"  AutoGenerateColumns="false"  AllowRecordSelection="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">         
            <ScrollingSettings ScrollWidth="100%" ScrollHeight="400" />
             
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
			 <ExportingSettings ExportAllPages="true" ExportTemplates="true" 
                   ColumnsToExport="MIN_No,DATE,Sector_Name,Item_Name,Category_Name,UOMName,Recurring,Quantity,Asset_Code,Standard,Maintenance,Service_Date"  />
            <Columns>
                
         
                 <ogrid:Column DataField="MIN_No" HeaderText="MIN No" ></ogrid:Column>
                <ogrid:Column DataField="DATE" HeaderText="MIN Date" DataFormatString="{0:d}" ></ogrid:Column>
                <ogrid:Column DataField="Subcon_name_To" HeaderText="Issue To"  Width="180px"></ogrid:Column> 
                 <ogrid:Column DataField="Sector_Name" HeaderText="Budget Sector"  Visible="true"></ogrid:Column>            
                <ogrid:Column DataField="Item_Name" HeaderText="Item" Width="110" ></ogrid:Column>
                <ogrid:Column DataField="Category_Name" HeaderText="Category_Name" Width="130"></ogrid:Column>
                 <ogrid:Column DataField="UOMName" HeaderText="Unit" Width="90"></ogrid:Column>   
                 <ogrid:Column DataField="Recurring" HeaderText="Recurring" Width="100"></ogrid:Column>
                  <ogrid:Column DataField="Quantity" HeaderText="Quantity" Align="right" Width="110"></ogrid:Column>
                <ogrid:Column DataField="Asset_Code" HeaderText="Asset Code" Width="100"></ogrid:Column>
                <ogrid:Column DataField="Standard" HeaderText="Standard" Align="right" Width="100"></ogrid:Column>
                  <ogrid:Column DataField="Maintenance" HeaderText="Maintenance" Width="120"></ogrid:Column>
                  <ogrid:Column DataField="Service_Date"  HeaderText="Service Date" Width="110" DataFormatString="{0:d}" ></ogrid:Column>  
                            
            </Columns>
             <Templates>

    </Templates>
                   
        </ogrid:Grid>
            </center>
                <br />

                <center>
                 <input onclick="exportgridMin()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />
                </center>
                      
                </div>
        </div>
    </div>
</asp:Content>


