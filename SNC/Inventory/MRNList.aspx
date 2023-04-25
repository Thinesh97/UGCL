<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Inventory/MRNList.aspx.cs" Inherits="MRNList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function exportgrid() {
            MRNGrid.exportToExcel();
        }
        function exportgridMin() {
            ItemList_Grid.exportToExcel();
        }
    </script>


    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Material Received Note List

            </h3>

                       
            </div>
        <div class="panel-body">
            <center>
        <ogrid:Grid runat="server" ID="MRNGrid" AutoGenerateColumns="false" OnRowDataBound="MRNGrid_RowDataBound" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10" AllowRecordSelection="False">
           
            <ExportingSettings ExportAllPages="true"  ExportTemplates="true"
                 ColumnsToExport="MRN_No,Date,Process_From,Vendor_name,Indent_No,Ind_date,Po_No,PODate,Invoice_No,Bill_Date,InvoiceAmt,Remarks" ExportedFilesTargetWindow="New"/>
             <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
             <ScrollingSettings ScrollWidth="100%" ScrollHeight="400"  />
            <Columns>
                <ogrid:Column DataField="MRN_No" HeaderText="MRN No" Width="100px" Wrap="true" >
                      <TemplateSettings  TemplateId="MRNNoTemplate"/>
                </ogrid:Column>
                <ogrid:Column DataField="Date" HeaderText="MRN Date"  Width="100px" >

                   <TemplateSettings TemplateId="dateformat" />
                </ogrid:Column>   
                 <ogrid:Column DataField="Process_From" HeaderText="Process From" Width="120" Wrap="true"></ogrid:Column>            
                <ogrid:Column DataField="Vendor_name" HeaderText="Vendor  Name" Width="180" Wrap="true"></ogrid:Column>
                 <ogrid:Column DataField="Indent_No" HeaderText="Indent No" Width="100px"></ogrid:Column>
                <ogrid:Column DataField="Ind_date" HeaderText="Indent Date" Width="110px"></ogrid:Column>
                 <ogrid:Column DataField="Po_No" HeaderText="PO No" Wrap="true"></ogrid:Column>
                  <ogrid:Column DataField="PODate" HeaderText="PO Date" Width="100px" ></ogrid:Column>              
                 <ogrid:Column DataField="Invoice_No" HeaderText="Invoice No" Wrap="true" Width="100px"></ogrid:Column>
               <ogrid:Column DataField="Bill_Date" HeaderText="Bill Date" Width="110px"></ogrid:Column>
                 <ogrid:Column DataField="InvoiceAmt" HeaderText="Invoice Amount" Wrap="true" Width="130px"></ogrid:Column>              
                <ogrid:Column DataField="Remarks" HeaderText="Remarks"  />
                <ogrid:Column DataField="Ledger_Head" HeaderText="Ledger Head"  />
               <ogrid:Column DataField="Enter" HeaderText="Enter Into Tally" Width="150px" Wrap="true"  />
            
                
            </Columns>
            <Templates>


                <ogrid:GridTemplate ID="dateformat" runat="server">
                    <Template>
                        
					    <%# Container.Value != String.Empty && Container.Value != "&#160;" ? Convert.ToDateTime(Container.Value.Replace("&#160;", " ")).ToString("dd-MM-yyyy") : ""%>
					
				
                    </Template>
                </ogrid:GridTemplate>


        <ogrid:GridTemplate ID="MRNNoTemplate" runat="server">
            <Template>
               <%--  <a href='MRN.aspx?MRN_No=<%#Container.DataItem["MRN_No"] %>'><%#Container.DataItem["MRN_No"] %></a>     --%> 
                 <asp:HyperLink ID="lnkMRNNo" runat="server"     CssClass="gridCB"  Text='<%#Container.DataItem["MRN_No"] %>'></asp:HyperLink>     
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                      <br />
                <a href="MRN.aspx" id="lnkbtnAdd" runat="server" class="btn btn-default">Add New MRN</a>
                   <%--  <asp:LinkButton ID="lnkbtnAdd" Text="Add New MRN" PostBackUrl="~/Inventory/MRN.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>--%>
                      <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>
                     <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />    
                  <asp:Button ID="GenMinItem"  runat="server" Text="Generate MRN Items List" OnClick="GenMinItem_Click" CssClass="btn btn-default"/>  
                      </center>      
                 </center>
            <br />
            <br />
            <h4 style="text-align:center"> Service  Material Received Note List</h4>
            <center>
        <ogrid:Grid runat="server" ID="Grid_ServiceMRN" AutoGenerateColumns="false" OnRowDataBound="ServiceMRNGrid_RowDataBound" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10" AllowRecordSelection="False">
           
            <ExportingSettings ExportAllPages="true"  ExportTemplates="true" 
                 ColumnsToExport="MRN_No,Date,Process_From,Vendor_name,Indent_No,Ind_date,Po_No,PODate,Invoice_No,Bill_Date,InvoiceAmt,Remarks" ExportedFilesTargetWindow="New"/>
             <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
             <ScrollingSettings ScrollWidth="100%" ScrollHeight="400"   />
            <Columns>
                <ogrid:Column DataField="MRN_No" HeaderText="MRN No" Width="100px" Wrap="true" >
                      <TemplateSettings  TemplateId="MRNNoTemplate_Ser"/>
                </ogrid:Column>
             
                  <ogrid:Column DataField="Subcon_name" HeaderText="Sub Contractor" Width="180" Wrap="true"></ogrid:Column>
                 <ogrid:Column DataField="Bill_Date" HeaderText="Bill Date" Width="120" Wrap="true"></ogrid:Column>   
                 <ogrid:Column DataField="Invoice_No" HeaderText="Invoice No" Width="100px"></ogrid:Column>
                 <ogrid:Column DataField="Ledger_Head" HeaderText="Ledger Head" Wrap="true"></ogrid:Column>
                <ogrid:Column DataField="WO_No" Wrap="true" HeaderText="Work Order Number" Width="110px"></ogrid:Column>
                 </Columns>
            <Templates>


                <ogrid:GridTemplate ID="GridTemplate1" runat="server">
                    <Template>
                        
					    <%# Container.Value != String.Empty && Container.Value != "&#160;" ? Convert.ToDateTime(Container.Value.Replace("&#160;", " ")).ToString("dd-MM-yyyy") : ""%>
					
				
                    </Template>
                </ogrid:GridTemplate>


        <ogrid:GridTemplate ID="MRNNoTemplate_Ser" runat="server">
            <Template>
               <%--  <a href='MRN.aspx?MRN_No=<%#Container.DataItem["MRN_No"] %>'><%#Container.DataItem["MRN_No"] %></a>     --%> 
                 <asp:HyperLink ID="lnkMRNNo" runat="server"     CssClass="gridCB"  Text='<%#Container.DataItem["MRN_No"] %>'></asp:HyperLink>     
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                      <br />
                <a href="MRN.aspx" id="A1" runat="server" class="btn btn-default">Add New MRN</a>
                   <%--  <asp:LinkButton ID="lnkbtnAdd" Text="Add New MRN" PostBackUrl="~/Inventory/MRN.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>--%>
                      <asp:Button ID="Button1" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click"></asp:Button>
                     <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />    
                  <asp:Button ID="Button2"  runat="server" Text="Generate MRN Items List" OnClick="GenMinItem_Click" CssClass="btn btn-default"/>  
                      </center>      
                 </center>

            <div id="ItemList_Gv" runat="server" visible="false" >
            <br />


            <center>
                <ogrid:Grid runat="server" ID="ItemList_Grid" CallbackMode="true"  AutoGenerateColumns="false"  AllowRecordSelection="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">         
            <ScrollingSettings ScrollWidth="100%" ScrollHeight="400" />
             
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
			 <ExportingSettings ExportAllPages="true" ExportTemplates="true" 
                  ColumnsToExport="MRN_No,Date,Item_Name,Indent_No,Ind_date,Indent_Qty,PO_Qty,Received_Qty,Accepted_Qty,Rejected_Qty,Pending_Qty"  />
            <Columns>
                 <ogrid:Column DataField="MRN_No" HeaderText="MRN No." ></ogrid:Column>
                   <ogrid:Column DataField="Date" HeaderText="MRN Date" DataFormatString="{0:d}" ></ogrid:Column>
                   <ogrid:Column DataField="Item_Name" HeaderText="Item Name"  ></ogrid:Column>
                  <ogrid:Column DataField="Indent_No" HeaderText="Indent No." ></ogrid:Column>
                 <ogrid:Column DataField="Ind_date" HeaderText="Indent Date"   DataFormatString="{0:d}"></ogrid:Column>
         
                <ogrid:Column DataField="Indent_Qty" HeaderText="Indent Qty" ></ogrid:Column>
                 <ogrid:Column DataField="PO_Qty" HeaderText="PO Qty" ></ogrid:Column>
                <ogrid:Column DataField="Received_Qty" HeaderText="Received Qty" ></ogrid:Column>
                <ogrid:Column DataField="Accepted_Qty" HeaderText="Accepted Qty" ></ogrid:Column>
                <ogrid:Column DataField="Rejected_Qty" HeaderText="Rejected Qty" ></ogrid:Column>                 
                <ogrid:Column DataField="Pending_Qty" HeaderText="Pending Qty" ></ogrid:Column>
                     
                            
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

