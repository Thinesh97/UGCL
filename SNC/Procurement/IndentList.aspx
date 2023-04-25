<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Procurement/IndentList.aspx.cs" Inherits="IndentList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_Indent.exportToExcel();
        }

        function exportgridItems() {
            Gv_IndItemsList.exportToExcel();
        }

        function ConfirmDelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }
    </script>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Indent List

            </h3>

        </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="Grid_Indent"   CallbackMode="false" AutoGenerateColumns="false"  OnDeleteCommand="Grid_Indent_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" OnRowDataBound="Grid_Indent_RowDataBound" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true"   PageSize="10">
           
              <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
            <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

            <Columns>
                   <ogrid:Column DataField="Indent_No" HeaderText="Indent No" runat="server" Width="110" >
            <TemplateSettings  TemplateId="IndentNoTemplate"/>
        </ogrid:Column>      
                
                <ogrid:Column DataField="Ind_date" HeaderText="Date" Width="110px" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="PROJECT" HeaderText="Project Name" Width="120px" Wrap="true" ReadOnly="true"></ogrid:Column>
                 <ogrid:Column DataField="Budget_ID" HeaderText="Monthly Budget" Width="120px" ReadOnly="true"></ogrid:Column>
                <ogrid:Column DataField="PreparedBy" HeaderText="Prepared By" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="ApprovedBy" HeaderText="Approved By" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="StockCheckBy" HeaderText="Stock Checked By" ReadOnly="true" ></ogrid:Column>
                 <ogrid:Column DataField="Remark" HeaderText="Remarks" Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Status" HeaderText="Status"  Width="120px" ReadOnly="true" ></ogrid:Column>
                <ogrid:Column DataField="ProcessedFrom" HeaderText="Processed From"  Width="140px" ReadOnly="true" ></ogrid:Column>
               
                   <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="70"  ></ogrid:Column>
              
            </Columns>
            
             <Templates>
        <ogrid:GridTemplate ID="IndentNoTemplate" ControlID="abc" runat="server">
            <Template>

                 <%-- <a id="lnkIndentNo" runat="server"  href='Indent.aspx?IndentNo=<%#Container.DataItem["Indent_No"] %>'><%#Container.DataItem["Indent_No"] %> </a>--%>
                   <asp:HyperLink ID="lnkIndentNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["Indent_No"] %>'>
                </asp:HyperLink>
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                       <br />
                <a href="Indent.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New Indent</a>
                   <%--   <asp:LinkButton ID="lnkbtnAdd" Text="Add New Indent" PostBackUrl="~/Procurement/Indent.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>--%>
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Click" CssClass="btn btn-default"></asp:Button>
                 <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
                   <asp:Button ID="btn_GenIndItemsList"  runat="server" Text="Generate Indent Items List" OnClick="btn_GenIndItemsList_Click" CssClass="btn btn-default"/>  
                      </center>


            <div id="ItemList_Gv" runat="server" visible="false" >
            <br />


            <center>
                <ogrid:Grid runat="server" ID="Gv_IndItemsList" CallbackMode="true"  AutoGenerateColumns="false"  AllowRecordSelection="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">         
            <ScrollingSettings ScrollWidth="100%" />
             
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
			 <ExportingSettings ExportAllPages="true" ExportTemplates="true" 
                  ColumnsToExport="Indent_No,Ind_date,Budget_Sector,Category_Name,Item_Name,UOMPrefix,Qty_Available,Qty_required,Total_Qty_Recevied,BOQ,BOQ_No,Total_Qty_Involved,Tentative_Date,Whether_Req_Qty"  />
            <Columns>
                 <ogrid:Column DataField="Indent_No" HeaderText="Indent No." Width="100" ></ogrid:Column>
                   <ogrid:Column DataField="Ind_date" HeaderText="Indent Date" Width="100" DataFormatString="{0:d}" ></ogrid:Column>
                    <ogrid:Column DataField="Budget_Sector" HeaderText="Budget Sector"  ></ogrid:Column>
                 <ogrid:Column DataField="Category_Name" HeaderText="Category Name"  ></ogrid:Column>
                   <ogrid:Column DataField="Item_Name" HeaderText="Item Name"  ></ogrid:Column>
                  <ogrid:Column DataField="UOMPrefix" HeaderText="UOM" Width="100"></ogrid:Column>
                 <ogrid:Column DataField="BOQ" HeaderText="BOQ" Width="80" >
                     <TemplateSettings TemplateId="BOQTemplate" />
                 </ogrid:Column>
                 <ogrid:Column DataField="BOQ_No" HeaderText="BOQ No" Width="110" ></ogrid:Column>              
                <ogrid:Column DataField="Total_Qty_Involved" HeaderText="Total Qty involved in this"  Width="150" ></ogrid:Column>
                <ogrid:Column DataField="Qty_Available" HeaderText="Available Qty in Stock" Align="right" ></ogrid:Column>
                 <ogrid:Column DataField="Qty_required" HeaderText="Indent Qty" Align="right"></ogrid:Column>
                <ogrid:Column DataField="Total_Qty_Recevied" HeaderText="Total Received Qty" Align="right"></ogrid:Column>
                   <ogrid:Column DataField="Tentative_Date" HeaderText="Tentative Date of Requirement" Align="center" Width="150" ></ogrid:Column>
                  <ogrid:Column DataField="Whether_Req_Qty" HeaderText="Whether Req Qty" Align="center" Width="130" >
                      <TemplateSettings  TemplateId="WhetherReqQty"/>
                  </ogrid:Column>
            </Columns>
              <Templates>
        
           <ogrid:GridTemplate ID="BOQTemplate" runat="server">
               <Template>
                   <asp:Label ID="lblBOQ" runat="server" Text='<%#Container.DataItem["BOQ"].ToString() !=string.Empty && Container.DataItem["BOQ"].ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
               </Template>
           </ogrid:GridTemplate>
           <ogrid:GridTemplate ID="WhetherReqQty" runat="server">
               <Template>
                   <asp:Label ID="lblWhetherReqQty" runat="server" Text='<%#Container.DataItem["Whether_Req_Qty"].ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
               </Template>
           </ogrid:GridTemplate>
    </Templates>
                   
        </ogrid:Grid>
            </center>
                <br />

                <center>
                 <input onclick="exportgridItems()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />
                </center>
                      
                </div>


        </div>
    </div>
</asp:Content>
