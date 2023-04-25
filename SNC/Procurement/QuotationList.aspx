<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Procurement/QuotationList.aspx.cs" Inherits="QuotationList" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript">
         function exportgrid() {
             QuotationGrid.exportToExcel();
         }
         function exportgridItems() {
             Gv_quotationitemList.exportToExcel();
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

              Quotation List

            </h3>

        </div>
             <div class="panel-body">
                  <center>
        <ogrid:Grid runat="server" ID="QuotationGrid" OnDeleteCommand="QuotationGrid_DeleteCommand" CallbackMode="false" AutoGenerateColumns="false" OnRowDataBound="QuotationGrid_RowDataBound" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
           <ScrollingSettings ScrollWidth="100%" ScrollHeight="400" />
          
            <ClientSideEvents OnBeforeClientDelete="beforedelete" />
             <ExportingSettings ExportAllPages="true" ExportTemplates="true"  ColumnsToExport="QuotationNo,Quotation_Date,VendorID,Vendor_name,SalesEmp,PaymentTerms,Delivery,Remark,TotalAmt,NetTotalAmt" />
            <Columns>
                 <ogrid:Column  HeaderText="Quotation No" DataField="QuotationNo" runat="server" Width="150px">
            <TemplateSettings  TemplateId="QuotationIDTemplate"/>
        </ogrid:Column>      
                  
                <ogrid:Column DataField="Quotation_Date" HeaderText="Quotation Date" Width="125"></ogrid:Column>
                <ogrid:Column DataField="Vendor_name" HeaderText="Vendor  Name" Wrap="true" Width="185px" ></ogrid:Column>
                 <ogrid:Column DataField="VendorID" HeaderText="Vendor  Code" Wrap="true" Width="185px" ></ogrid:Column>
                <ogrid:Column DataField="SalesEmp" HeaderText="Sales Employee" ></ogrid:Column>
                  <ogrid:Column DataField="PaymentTerms" HeaderText="PaymentTerms " Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="Delivery" HeaderText="Delivery" Wrap="true" ></ogrid:Column>
              
                <ogrid:Column DataField="Remark" HeaderText="Remarks " Wrap="true" ></ogrid:Column>
                <ogrid:Column DataField="TotalAmt" HeaderText="Total Amount" Width="130" Align="Right"></ogrid:Column>
                <ogrid:Column DataField="NetTotalAmt" HeaderText="Net Total Amount" Width="140" Align="Right"   ></ogrid:Column>
                 <ogrid:Column DataField="" HeaderText="Delete" Width="80px"  AllowDelete ="true"   ></ogrid:Column>
               
            </Columns>
              <Templates>
        <ogrid:GridTemplate ID="QuotationIDTemplate" runat="server">
            <Template>
               <asp:HyperLink ID="lnkQuotationNo" runat="server"     CssClass="gridCB"  Text='<%#Container.DataItem["QuotationNo"] %>'></asp:HyperLink>
               <%--  <a href='Quotation.aspx?ID=<%#Container.DataItem["QuotationNo"] %>'><%#Container.DataItem["QuotationNo"] %> </a>--%>
            </Template>
        </ogrid:GridTemplate>

    </Templates>
        </ogrid:Grid>
                       <br />
                        <a href="Quotation.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New Quotation</a>
                      <%--<asp:LinkButton ID="lnkbtnAdd" Text="Add New Quotation" PostBackUrl="~/Procurement/Quotation.aspx" CssClass="btn btn-default"  runat="server"></asp:LinkButton>--%>
                 <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" CssClass="btn btn-default" OnClick="btnExportToPDF_Click" ></asp:Button>
                     <asp:Button ID="btn_GenquotationItemsList"  runat="server" Text="Generate Quotation Items List" CssClass="btn btn-default" OnClick="btn_GenquotationItemsList_Click"/>  
                <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />     
                      </center>
                 <br />
            <div id="quotationList_Gv" runat="server" visible="false" >
            <br />


            <center>
                <ogrid:Grid runat="server" ID="Gv_quotationitemList" CallbackMode="true"  AutoGenerateColumns="false"  AllowRecordSelection="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">         
            <ScrollingSettings ScrollWidth="100%" ScrollHeight="400" />
             
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
			 <ExportingSettings ExportAllPages="true" ExportTemplates="true" 
                  ColumnsToExport="QuotationNo,QT_date,Category_Name,Item_Name,UOMPrefix,Qty,Price,Amt,Amt_With_Tax"  />
            <Columns>
                 <ogrid:Column DataField="QuotationNo" HeaderText="Quotation No." Width="100" ></ogrid:Column>
                   <ogrid:Column DataField="QT_date" HeaderText="Quotation Date" Width="100" DataFormatString="{0:d}" ></ogrid:Column>
                   <%-- <ogrid:Column DataField="Budget_Sector" HeaderText="Budget Sector"  ></ogrid:Column>--%>
                 <ogrid:Column DataField="Category_Name" HeaderText="Category Name"  ></ogrid:Column>
                   <ogrid:Column DataField="Item_Name" HeaderText="Item Name"  ></ogrid:Column>
                  <ogrid:Column DataField="UOMPrefix" HeaderText="UOM" Width="100"></ogrid:Column>
                <%-- <ogrid:Column DataField="BOQ" HeaderText="BOQ" Width="80" >
                     <TemplateSettings TemplateId="BOQTemplate" />
                 </ogrid:Column>--%>
              <%--  -------%>
                 <ogrid:Column DataField="Qty" HeaderText="Qty" Width="80px" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Price" HeaderText="Price" Width="100px" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Amt" HeaderText="Amount" Width="100px" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Amt_With_Tax" HeaderText="Amount With Tax" Width="140px" Align="right"></ogrid:Column>
               <%-- ------%>
                <%-- <ogrid:Column DataField="BOQ_No" HeaderText="BOQ No" Width="110" ></ogrid:Column>   --%>           
     <%--           <ogrid:Column DataField="Total_Qty_Involved" HeaderText="Total Qty involved in this"  Width="150" ></ogrid:Column>
                <ogrid:Column DataField="Qty_Available" HeaderText="Available Qty in Stock" Align="right" ></ogrid:Column>
                 <ogrid:Column DataField="Qty_required" HeaderText="Indent Qty" Align="right"></ogrid:Column>
                <ogrid:Column DataField="Total_Qty_Recevied" HeaderText="Total Received Qty" Align="right"></ogrid:Column>
                   <ogrid:Column DataField="Tentative_Date" HeaderText="Tentative Date of Requirement" Align="center" Width="150" ></ogrid:Column>--%>
                <%--  <ogrid:Column DataField="Whether_Req_Qty" HeaderText="Whether Req Qty" Align="center" Width="130" >
                      <TemplateSettings  TemplateId="WhetherReqQty"/>
                  </ogrid:Column>--%>
            </Columns>
              <Templates>
        
        <%--   <ogrid:GridTemplate ID="BOQTemplate" runat="server">
               <Template>
                   <asp:Label ID="lblBOQ" runat="server" Text='<%#Container.DataItem["BOQ"].ToString() !=string.Empty && Container.DataItem["BOQ"].ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
               </Template>
           </ogrid:GridTemplate>--%>
        <%--   <ogrid:GridTemplate ID="WhetherReqQty" runat="server">
               <Template>
                   <asp:Label ID="lblWhetherReqQty" runat="server" Text='<%#Container.DataItem["Whether_Req_Qty"].ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
               </Template>
           </ogrid:GridTemplate>--%>
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
