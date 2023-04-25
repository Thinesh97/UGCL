<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true"  CodeBehind="SC_BillsList.aspx.cs"  Inherits="SNC.SubContractorBills.SC_BillsList"  %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function exportgrid() {
            Grid_WO.exportToExcel();
        }

        function exportgridWOItems() {
            Gv_WOItemsList.exportToExcel();
        }

        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                //alert('Check');
                document.getElementById('<%=HF_Confirm.ClientID%>').value = "false";
                //alert(document.getElementById('<%=HF_Confirm.ClientID%>').value);
                return false;
            }
            else {
                document.getElementById('<%=HF_Confirm.ClientID%>').value = "true";
                return true;
            }
        }
    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Sub Contractor Bill List

            </h3>

        </div>
        <div class="panel-body">
            <center>
                <ogrid:Grid runat="server" ID="Grid_Sub_Contractor_Bill" CallbackMode="false" AutoGenerateColumns="false" 
                    OnRowDataBound="Grid_SC_RowDataBound" 
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"
                         ColumnsToExport="WONo,WODate,Subcon_name,Type_Name,DurationOfWork,Status,Name,Prepared_By,Other_Terms,Remarks" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="RA_Bill_No" HeaderText="RA Bill Number" Width="190px" >
                             <TemplateSettings  TemplateId="RA_Bill_NoTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="SC_Bill_Date" HeaderText="SC Bill Date" Width="120px" ></ogrid:Column>
                        <ogrid:Column DataField="Project_Code" HeaderText="Project Name" Wrap="false"></ogrid:Column>
                        <ogrid:Column DataField="Subcon_name" HeaderText="Sub Contractor Name" Width="180px" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="WONo" HeaderText="SC_WONo" Wrap="true"   Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="SC_BillingFrom_Date" HeaderText="Billing From" Align="center" Width="150px"></ogrid:Column>                 
                        <ogrid:Column DataField="SC_Billing_To_Date" HeaderText="Billing To" Width="100px" ></ogrid:Column>
                        <ogrid:Column DataField="SC_Work_Description" HeaderText="Work Description" Wrap="true" Width="120"></ogrid:Column>
                        <ogrid:Column DataField="Financial_Year" HeaderText="Financial Year" Wrap="true" Width="120"></ogrid:Column>
                        <ogrid:Column DataField="" HeaderText="Delete" >
                             <TemplateSettings  TemplateId="GT_SC_Delete"/>
                        </ogrid:Column>
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="RA_Bill_NoTemplate" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnkRA_Bill_No" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["RA_Bill_No"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                         <ogrid:GridTemplate ID="GT_SC_Delete" runat="server">
                            <Template>
                               <asp:LinkButton ID="lnkSC_Delete" runat="server" CssClass="gridCB" OnClientClick="beforedelete();" CommandArgument='<%# Container.PageRecordIndex %>'  OnCommand="lnkSC_Delete_Click" Text="Delete">
                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>

                    </Templates>
                </ogrid:Grid>
                      
                
                
                <br />
                <center>
                    <a href="SC_Bill.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New SC Bill</a> 
                    <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                </center>
                      </center>
            <asp:HiddenField runat="server" Value="" Id="HF_Confirm"/>


            <div id="ItemList_Gv" runat="server" visible="false" >
            <br />


            <center>
                <ogrid:Grid runat="server" ID="Gv_WOItemsList" CallbackMode="true"  AutoGenerateColumns="false"  AllowRecordSelection="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">         
            <ScrollingSettings ScrollWidth="100%" />
             
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
			 <ExportingSettings ExportAllPages="true" ExportTemplates="true" 
                  ColumnsToExport="Indent_No,WONo,WODate,Category_Name,Item_Name,UOM,Total_Qty_Involved,Qty_required,Price,Amount,Amt_With_Tax,Referred_VendorCount,BOQ,BOQ_No,Tentative_Date,Whether_Req_Qty"  />
            <Columns>
              
                  <ogrid:Column DataField="Indent_No" HeaderText="Indent No" Width="100"></ogrid:Column>
                    <ogrid:Column DataField="WONo" HeaderText="WO No" Width="150"></ogrid:Column>
                <ogrid:Column DataField="WODate" HeaderText="Date"  Width="100"></ogrid:Column>
                <ogrid:Column DataField="Category_Name" HeaderText="Category"  Width="120"></ogrid:Column>
                 <ogrid:Column DataField="Item_Name" HeaderText="Item" Width="120"></ogrid:Column>
                 <ogrid:Column DataField="BOQ" HeaderText="BOQ" Width="80" >
                     <TemplateSettings TemplateId="BOQTemplate" />
                 </ogrid:Column>
                 <ogrid:Column DataField="BOQ_No" HeaderText="BOQ No" Width="100" ></ogrid:Column>
                <ogrid:Column DataField="UOM" HeaderText="UOM" Width="100px" ></ogrid:Column>
                <ogrid:Column DataField="Total_Qty_Involved" HeaderText="Qty involved in this"  Width="150" ></ogrid:Column>              
                  <ogrid:Column DataField="Qty_required" HeaderText="Required Qty" Align="right" Width="120"></ogrid:Column> 
               <ogrid:Column DataField="Price" HeaderText="Price" Width="100" Align="right"></ogrid:Column>    
                <ogrid:Column DataField="Amount" HeaderText="Amount" Width="120" Align="right"></ogrid:Column>            
                <ogrid:Column DataField="Amt_With_Tax" HeaderText="Amt With Tax" Width="120" Align="right"></ogrid:Column>    
                <ogrid:Column DataField="Referred_VendorCount" HeaderText="Referred Vendor" Width="135" Align="center"></ogrid:Column>             
                <ogrid:Column DataField="Tentative_Date" HeaderText="Tentative Date of Requirement" Align="center" Width="200" DataFormatString="{0:dd-MM-yyy}"  ></ogrid:Column>
                  <ogrid:Column DataField="Whether_Req_Qty" HeaderText="Whether Req Qty"  Align="center" Width="140" >
                      <TemplateSettings TemplateId="WhetherReqQtyTemplate" />
                  </ogrid:Column> 
            </Columns>
              <Templates>
        
           <ogrid:GridTemplate ID="BOQTemplate" runat="server">
            <Template>
                <asp:Label ID="lblBOQ" runat="server" Text='<%#Container.DataItem["BOQ"].ToString() != string.Empty && Container.DataItem["BOQ"].ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
            </Template>
        </ogrid:GridTemplate>
             <ogrid:GridTemplate ID="WhetherReqQtyTemplate" runat="server">
                 <Template>
                     <asp:Label ID="lblWhetherReqQty" runat="server" Text='<%#Container.DataItem["Whether_Req_Qty"].ToString() == "True" ? "Yes" :"No" %>'></asp:Label>
                 </Template>
             </ogrid:GridTemplate>
    </Templates>
                   
        </ogrid:Grid>
            </center>
                <br />

                <center>
                 <input onclick="exportgridWOItems()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />
                </center>
                      
                </div>


        </div>
    </div>
</asp:Content>
