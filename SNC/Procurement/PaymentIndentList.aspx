<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="PaymentIndentList.aspx.cs" Inherits="PaymentIndentList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript">
        function exportgrid1() {
            debugger;
            Grid_PaymentIndent.exportToExcel();
        }

        function exportPendingPartialGrid() {
            debugger;
            GridPendingBalance.exportToExcel();
        }
        function exportgrid2() {
            debugger;
            Grid_PaymentIndent_Approved.exportToExcel();
        }
        function exportgrid3() {
            debugger;
            Grid_PaymentIndent_Completed.exportToExcel();
        }

        function ConfirmDelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }

        function calcTotalAmt(chkSelect) {

            var totalAmt = 0.00;
            var FormatedtotalAmt = 0.00;
            var grid = jQuery(chkSelect).closest('table');

            jQuery(grid).find('input[type="checkbox"]').each(function () {
                if (jQuery(this).is(":checked")) {
                    var amt = jQuery(this).closest('tr').find('td:eq(6)').find('[id*="txtApproveAmt"]').val();
                    totalAmt = parseFloat(totalAmt) + parseFloat(amt);
                    totalAmt.toFixed(3);
                }
            });
            FormatedtotalAmt = totalAmt.toFixed(2);
            jQuery(grid).parent('div').parent('div').next('div').find('table tr td:eq(6)').css({ textAlign: 'center' });
            jQuery(grid).parent('div').parent('div').next('div').find('table tr td:eq(6)').html(FormatedtotalAmt);
        }


        //function CalculateTDSAmt(txt) {

        //    var approvedAmt = jQuery(txt).closest('tr').find('td:eq(4)').text();
        //    var tdsPerc = jQuery(txt).closest('tr').find("input[id*='txtTDSPerc']").val();
        //    var tdsAmt = ((parseFloat(approvedAmt) * parseFloat(tdsPerc) / 100).toFixed(2));
        //    if (tdsAmt != NaN && tdsAmt >= 0) {
        //        jQuery(txt).closest('tr').find("input[id*='txtTDSAmt']").val(tdsAmt);
        //    }
        //    else {
        //        jQuery(txt).closest('tr').find("input[id*='txtTDSAmt']").val("0.00");
        //    }
        //}
        debugger;
        $(Grid_PaymentIndent_Completed).on('click', 'input[type="checkbox"]', function () {
            $('input[type="checkbox"]').not(this).prop('checked', false);
        });
    </script>


    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Payment Indent List

            </h3>
        </div>

        <div class="panel-body">
            <center>
                <ogrid:Grid runat="server" ID="Grid_PaymentIndent" CallbackMode="false" AutoGenerateColumns="false"  FolderStyle="../Gridstyles/grand_gray"
                    AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10"  AllowGrouping="true"
                    OnDeleteCommand="Grid_PaymentIndent_DeleteCommand" OnRowDataBound="Grid_PaymentIndent_RowDataBound" >
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"/>
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            
                    <Columns>
                        <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No" runat="server" Width="150px" >
                            <TemplateSettings  TemplateId="PayIndNoTemplate"/>
                        </ogrid:Column>      
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Date" Width="110px" DataFormatString="{0:d}"></ogrid:Column>
                        <ogrid:Column DataField="State_Name" HeaderText="State" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width="200px"></ogrid:Column>
                        <ogrid:Column DataField="Ben_Name" HeaderText="Company Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Name" HeaderText="Bank Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Branch" HeaderText="Branch Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Account" HeaderText="Account Number"></ogrid:Column>
                        <ogrid:Column DataField="Bank_IFSC" HeaderText="IFSC Code"></ogrid:Column>
                        <ogrid:Column DataField="Amt_PartPayment" HeaderText="Amount"></ogrid:Column>
                        <ogrid:Column DataField="WorkDoneFor" HeaderText="Work Done For/Item Supplied to"></ogrid:Column>
                        <ogrid:Column DataField="Status" HeaderText="Status"></ogrid:Column>
                           <ogrid:Column DataField="Name" HeaderText="Prepared By"></ogrid:Column>
                        <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="70"></ogrid:Column>
                    </Columns>
                
                    <Templates>
                        <ogrid:GridTemplate ID="PayIndNoTemplate" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["PayInd_No"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                
                <br />
                <a href="PaymentIndent.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New Payment Indent</a>
                <a href="BankIndent.aspx" runat="server" id="lnkbtnBank" class="btn btn-default">Add New Banking Indent</a>
                <asp:Button ID="btnExportToPDF" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid1()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
                <br />
                <br />
                <h4 style="text-align:center">  Pending Balance Partial Payment Indent List</h4>
                <div>
                         <ogrid:Grid runat="server" ID="GridPendingBalance" CallbackMode="false" AutoGenerateColumns="false"  FolderStyle="../Gridstyles/grand_gray"
                    AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10"
                    OnDeleteCommand="Grid_PaymentIndentPartialBalance_DeleteCommand"  AllowGrouping="true" OnRowDataBound="Grid_PaymentIndentPartialBalance_RowDataBound" >
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"/>
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
            
                    <Columns>
                        <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No" runat="server" Width="150px" >
                            <TemplateSettings  TemplateId="PayIndNoTemplatePartialBalance"/>
                        </ogrid:Column>      
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Date" Width="110px" DataFormatString="{0:d}"></ogrid:Column>
                        <ogrid:Column DataField="State_Name" HeaderText="State" Width="150px" ></ogrid:Column>
                        <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width="200px"></ogrid:Column>
                        <ogrid:Column DataField="Ben_Name" HeaderText="Company Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Name" HeaderText="Bank Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Branch" HeaderText="Branch Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Account" HeaderText="Account Number"></ogrid:Column>
                        <ogrid:Column DataField="Bank_IFSC" HeaderText="IFSC Code"></ogrid:Column>
                        <ogrid:Column DataField="Amt_PartPayment" HeaderText="Amount"></ogrid:Column>
                        <ogrid:Column DataField="WorkDoneFor" HeaderText="Work Done For/Item Supplied to"></ogrid:Column>
                        <ogrid:Column DataField="Status" HeaderText="Status"></ogrid:Column>
                        <ogrid:Column DataField="Name" HeaderText="Prepared By"></ogrid:Column>
                        <ogrid:Column  HeaderText="Delete" AllowDelete="true" Width="70"></ogrid:Column>
                    </Columns>
                
                    <Templates>
                        <ogrid:GridTemplate ID="PayIndNoTemplatePartialBalance" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["PayInd_No"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                </ogrid:Grid>
                
                <br />
                <asp:Button ID="btnPendingPartialGrid" runat="server" Text="Export To PDF" OnClick="btnExportToPDFPendingPartialGrid_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportPendingPartialGrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
                <br />
                <br />
                </div>
                <h4>Approved Payment Indent List</h4>
                <ogrid:Grid runat="server" ID="Grid_PaymentIndent_Approved" CallbackMode="false" AutoGenerateColumns="false"  FolderStyle="../Gridstyles/grand_gray" OnRowDataBound="Grid_Approved_RowDataBound"
                    AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true"  AllowGrouping="true" PageSize="10">
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"/>
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />
            
                    <Columns>
                        <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No" runat="server" Width="150px" >
                            <TemplateSettings  TemplateId="PayIndNoTemplateApproved"/>
                        </ogrid:Column>      
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Date" Width="110px" DataFormatString="{0:d}"></ogrid:Column>
                        <ogrid:Column DataField="State_Name" HeaderText="State" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Project_Name" HeaderText="Project Name" Width="200px"></ogrid:Column>
                        <ogrid:Column DataField="Ben_Name" HeaderText="Company Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Name" HeaderText="Bank Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Branch" HeaderText="Branch Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Account" HeaderText="Account Number"></ogrid:Column>
                        <ogrid:Column DataField="Bank_IFSC" HeaderText="IFSC Code"></ogrid:Column>
                        <ogrid:Column DataField="Amt_PartPayment" HeaderText="Requested Amount"></ogrid:Column>
                        <ogrid:Column DataField="Amt_Approved" HeaderText="Approved Amount"></ogrid:Column>
                        <ogrid:Column DataField="WorkDoneFor" HeaderText="Work Done For/Item Supplied to"></ogrid:Column>
                        <ogrid:Column DataField="Name" HeaderText="Prepared By"></ogrid:Column>
                        <ogrid:Column DataField="Payment_Approved_Date" HeaderText="Approved Date Time"></ogrid:Column>
                        <ogrid:Column DataField="Status" HeaderText="Status"></ogrid:Column>
                      <%--  <ogrid:Column HeaderText="Select to Return" Width="60px" >
                            <TemplateSettings TemplateId="SelectTemplate_MakeComplete"/>
                        </ogrid:Column>--%>
                    </Columns>
                <%--  <Templates>
                         <ogrid:GridTemplate ID="SelectTemplate_MakeComplete" runat="server">
                            <Template>
                                <asp:CheckBox runat="server" ID="chkSelect" />
                                <asp:HiddenField ID="hdn_payIndNo" runat="server" Value='<%# Container.DataItem["PayInd_No"] %>'/>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>--%>
                    <Templates>
                        
                    <ogrid:GridTemplate ID="PayIndNoTemplateApproved" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["PayInd_No"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>

                <br />
                
                <asp:Button ID="btnExportToPDF_Approved" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Approved_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid2()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
                <br />
                <br />


                
                <br />
                <h4>Completed Payment Indent List</h4>
                
                  <div class="panel-body">
                 
            
                      <center>

                          
      
         
                <ogrid:Grid runat="server" ID="Grid_PaymentIndent_Completed" CallbackMode="false" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowRecordSelection="true" AllowGrouping="true" AllowPaging="true" PageSize="10" OnUpdateCommand="Grid_PaymentIndent_Completed_UpdateCommand"  OnRowDataBound="Grid_PaymentIndent_RowDataBound_Completed"  OnDeleteCommand="Grid_PaymentIndent_DeleteCommand">
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

                     <Columns>
                        <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No" ReadOnly="true" runat="server" Width="150px" >
                            <TemplateSettings  TemplateId="PayIndNoTemplate_Completed"/>
                            </ogrid:Column>
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Request Date" Width="120px" DataFormatString="{0:d}" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="State_Name" HeaderText="State" Width="150px" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Ben_Name" HeaderText="Vendor/SubContractor" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <%-- <ogrid:Column DataField="Order_Type" HeaderText="Order Type" Width="100px" ReadOnly="true"></ogrid:Column>--%>
                        <ogrid:Column DataField="Amt_Approved" HeaderText="Approved Amount" Width="110px" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Amt_Transferable" HeaderText="Transferred  Amount" Width="110px" runat="server">
                            <TemplateSettings EditTemplateId="EditTransAmtTemplate" />
                        </ogrid:Column>
                          <ogrid:Column DataField="Balance_Amount" HeaderText="Balance Amount" ReadOnly="true" Width="110px" runat="server">                        
                         <TemplateSettings TemplateId="Balance_Amount"/>
                          </ogrid:Column>
                         <ogrid:Column HeaderText="Generate Partial Payment" Width="80px" >
                            <TemplateSettings TemplateId="SelectTemplatePAYIND"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="TDS_Perc" HeaderText="TDS Deducted %" ReadOnly="true" Width="100px" runat="server">
                        </ogrid:Column>
                        <ogrid:Column DataField="TDS_Amt" HeaderText="TDS Deducted Amt" ReadOnly="true" Width="100px" runat="server">
                            
                        </ogrid:Column>
                        <ogrid:Column DataField="Other_Deductions" HeaderText="Other Deduction (If Any)" ReadOnly="true" Width="100px" runat="server">
                           
                        </ogrid:Column>
                        <ogrid:Column DataField="Bank_Name"  HeaderText="Beneficiary Bank" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Account" HeaderText="Beneficiary Account Number" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_IFSC" HeaderText="Beneficiary IFSC Code" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Branch" HeaderText="Beneficiary Branch" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Payment_Mode" HeaderText="Payment Mode" Width="140px"  ReadOnly="true"></ogrid:Column>
                         <ogrid:Column DataField="Company_Bank" HeaderText="Company Bank" Width="140px" >
                            <TemplateSettings TemplateId="EditCompany_BankTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Payment_Date" HeaderText="Payment Date" Width="140px"  DataFormatString="{0:d}">
                            <TemplateSettings EditTemplateId="EditPaymentDateTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Payment_Ref_No" HeaderText="Payment Reference No." Width="140px" >
                            <TemplateSettings EditTemplateId="EditPaymentRefNoTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="WorkDoneFor_Narration" HeaderText="Narration"  Width="280px">
                            <TemplateSettings EditTemplateId="EditNarrationTemplate" />
                        </ogrid:Column>
                         <ogrid:Column DataField="Name" HeaderText="Prepared By" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column AllowEdit="true" Width="90px"></ogrid:Column>
                        <ogrid:Column DataField="Approver" HeaderText="Approver" Visible="false"></ogrid:Column>
                        <ogrid:Column HeaderText="Return" Width="60px" >
                            <TemplateSettings TemplateId="SelectTemplate"/>
                        </ogrid:Column>
                          <ogrid:Column DataField="Is_PartialPayment" HeaderText="Is_PartialPayment" Visible="false"></ogrid:Column>
                     <ogrid:Column  HeaderText="Download Document" Width="200px" Wrap="true" >
                            <TemplateSettings TemplateId="lnkDownloadDocumentTemplate" />
                        </ogrid:Column>
                          <ogrid:Column DataField="Amt_PartPayment" HeaderText="Amt_PartPayment" Visible="false">
                          </ogrid:Column>

                     </Columns>

                    <Templates>
                        <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplate" ControlID="txtTransAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTransAmt" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditCompany_BankTemplate" ControlID="ddlCompany_Bank" ControlPropertyName="value">
                            <Template>
                                <asp:DropDownList  ID="ddlCompany_Bank" Width="120px" runat="server" ></asp:DropDownList>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditPaymentDateTemplate" ControlID="txtPaymentDate" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPaymentDate" Width="120px" runat="server" TextMode="Date" Text='<%# Container.DataItem["Payment_Date"] %>'></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditPaymentRefNoTemplate" ControlID="txtPaymentRefNo" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPaymentRefNo" Width="150px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditNarrationTemplate" ControlID="txtNarration" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtNarration" runat="server" Value='<%# Container.DataItem["WorkDoneFor"] %>' TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                         <ogrid:GridTemplate ID="SelectTemplate" runat="server">
                            <Template>
                                <asp:CheckBox runat="server" ID="chkSelect" />
                                <asp:HiddenField ID="hdn_payIndNo" runat="server" Value='<%# Container.DataItem["PayInd_No"] %>'/>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="SelectTemplatePAYIND" runat="server">
                            <Template>
                                <asp:CheckBox runat="server" ID="chkSelectTemplatePAYIND" />
                                <asp:HiddenField ID="hdn_payIndNo" runat="server" Value='<%# Container.DataItem["PayInd_No"] %>'/>
                            </Template>
                        </ogrid:GridTemplate>
                          <ogrid:GridTemplate ID="PayIndNoTemplate_Completed" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["PayInd_No"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="lnkDownloadDocumentTemplate" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnkWOItem" Text="View Document" CommandArgument='<%#Container.DataItem["PayInd_No"] %>' CommandName='<%#Container.DataItem["PayInd_No"] %>'  OnClick="lnkDownloadDocumentItem_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                           <ogrid:GridTemplate ID="Balance_Amount" runat="server">
                            <Template>
                            <asp:Label  ID="lnkBalance_Amount"    runat="server" CssClass="gridCB">
                                        </asp:Label>
                            </Template>
                        </ogrid:GridTemplate>
                        
                    </Templates>

                </ogrid:Grid>
                <br />
                 <asp:Button ID="btnReturned" runat="server" Text="Return" OnClick="btnReturned_Click" CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="btnExportToPDF_Completed" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Completed_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid3()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                 <asp:Button ID="btnNewPAYIND" runat="server" Text="Generate Partial Payment Indent" OnClick="btnNewPAYIND_Click" CssClass="btn btn-default"></asp:Button>
                <br />

            </center>

        </div>

                <br />
                <br />


                
                <br />
                <h4>Banking  Indent List</h4>

                 <div class="panel-body">
                 
            
                      <center>
                <ogrid:Grid runat="server" ID="GridBankingIndent" CallbackMode="false" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray"  OnRowDataBound="GridBankingIndent_RowDataBound_Completed" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowRecordSelection="true" AllowGrouping="true" AllowPaging="true" PageSize="10" >
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

                     <Columns>
                        <ogrid:Column DataField="BANK_INDENT_No" HeaderText="Bank Indent No" ReadOnly="true" runat="server" Width="150px" >
                            <TemplateSettings  TemplateId="BANK_INDENT_NoTemplate_Completed"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="INDENT_Date" HeaderText="Date" Width="120px" DataFormatString="{0:d}" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="State_Name" HeaderText="State" Width="150px" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Project_Code" HeaderText="Project Code" Width="200px" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank" HeaderText="Bank name" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <%-- <ogrid:Column DataField="Order_Type" HeaderText="Order Type" Width="100px" ReadOnly="true"></ogrid:Column>--%>
                        <ogrid:Column DataField="PAYMENT_TYPE" HeaderText="Payment Type" Width="120px" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="PAYMENT_TERMS" HeaderText="Payment Category" Width="150px" runat="server"> </ogrid:Column>
                        <ogrid:Column DataField="PAYMENT_CATAGORY_TOWARDS" HeaderText="Towards" ReadOnly="true" Width="150px" runat="server">   </ogrid:Column>
                        <ogrid:Column DataField="REFDETAILS" HeaderText="Ref Details" ReadOnly="true" Width="150px" runat="server">   </ogrid:Column>
                        <ogrid:Column DataField="NARRATION" HeaderText="Narration" ReadOnly="true" Width="250px" runat="server">   </ogrid:Column>                     
                        
                    </Columns>
                      <Templates>
                           <ogrid:GridTemplate ID="BANK_INDENT_NoTemplate_Completed" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkBANK_INDENT_No" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["BANK_INDENT_No"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                          </Templates>
                         </ogrid:Grid>


                <br />
                 <%--<asp:Button ID="Button1" runat="server" Text="Return" OnClick="btnReturned_Click" CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="Button2" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Completed_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid3()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                 <asp:Button ID="Button3" runat="server" Text="Generate Partial Payment Indent" OnClick="btnNewPAYIND_Click" CssClass="btn btn-default"></asp:Button>--%>
                <br />

            </center>

        </div>
        </div>
    </div>

    <asp:Button runat="server" ID="btnAddSubItem" Style="display: none"></asp:Button>
    <ajaxToolkit:ModalPopupExtender ID="ModalWOSubItem" runat="server" PopupControlID="PanelWOSubItem" TargetControlID="btnAddSubItem"
        CancelControlID="BtnClose1" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanelWOSubItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose1" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5 id="myModalamt11"><asp:Label ID="lblPendingGrid_DocDownload" runat="server" ></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                    <center>
            <ogrid:Grid runat="server" ID="Grid_File" CallbackMode="false" PageSize="5"
                OnDeleteCommand="Grid_File_DeleteCommand"
                AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowPaging="true">
                <ScrollingSettings ScrollWidth="70%" />
                <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />

                <Columns>
                    <ogrid:Column DataField="File_Type" HeaderText="File Type" Width="150px"></ogrid:Column>
                    <ogrid:Column DataField="File_Name" HeaderText="File Name" Width="400px">
                        <TemplateSettings TemplateId="FileTemplate" />
                    </ogrid:Column>
                    <ogrid:Column HeaderText="Delete" AllowDelete="true"></ogrid:Column>

                </Columns>

                <Templates>
                    <ogrid:GridTemplate ID="FileTemplate" runat="server">
                        <Template>
                            <asp:LinkButton ID="lnkDownloadFile" runat="server" Text='<%#Container.DataItem["File_Name"].ToString() %>' OnClick="lnkDownloadFile_Click" CssClass="gridCB" ForeColor="#337ab7"></asp:LinkButton>
                        </Template>
                    </ogrid:GridTemplate>
                </Templates>

            </ogrid:Grid>
        </center>

                </div>
                <%-- <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveWOSubItem" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValSubItem" OnClick="btnSaveWOSubItem_Click" />
                        <asp:Button ID="btnCancelSubItem" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelWOSubItem_Click" />        
                 </center>

                </div>--%>
            </div>
        </div>

    </asp:Panel>




    <asp:SqlDataSource ID="DDLCompanyBanks" runat="server" ConnectionString="<%$ ConnectionStrings:SNC_DB_Connection%>"
        SelectCommand="select * from TB_Bank_Details"></asp:SqlDataSource>
</asp:Content>
