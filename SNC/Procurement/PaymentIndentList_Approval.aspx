<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Procurement/PaymentIndentList_Approval.aspx.cs" Inherits="PaymentIndentList_Approval" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript">
        //const { debug } = require("node:util");

        function exportgrid1() {
            grid_PendingPayInd.exportToExcel();
        }
        function Partialexportgrid1() {
            grid_PartialPayment.exportToExcel();
        }
        function exportgrid2() {
            grid_OnHoldPayInd.exportToExcel();
        }
        function exportgrid3() {
            grid_ApprovedPayInd.exportToExcel();
        }
        function ConfirmDelete() {
            if (confirm("Are you want to Delete this record?") == false) {
                return false;
            }
            return true;
        }

        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }

        function CheckAmt_Partial(txtApproveAmt) {
            debugger;
            var approveAmt = txtApproveAmt.value;
            var requestedAmt = $(txtApproveAmt).closest('tr').find('td:eq(6)').text();
            var billAmt = $(txtApproveAmt).closest('tr').find('td:eq(5)').text();
            if (parseFloat(approveAmt) > parseFloat(requestedAmt)) {
                alert("Approve amount should not be greater than requested amount");
                txtApproveAmt.value = "";
                $(txtApproveAmt).closest('tr').find("input[id*='txtBalAmt']").val("");
            }
            else {
                $(txtApproveAmt).closest('tr').find("input[id*='txtBalAmt']").val(parseFloat(billAmt) - parseFloat(approveAmt));
            }
        }

        function CheckAmt(txtApproveAmt) {
            debugger;
            var approveAmt = txtApproveAmt.value;
            var requestedAmt = $(txtApproveAmt).closest('tr').find('td:eq(7)').text();
            var billAmt = $(txtApproveAmt).closest('tr').find('td:eq(6)').text();
            if (parseFloat(approveAmt) > parseFloat(requestedAmt)) {
                alert("Approve amount should not be greater than requested amount");
                txtApproveAmt.value = "";
                $(txtApproveAmt).closest('tr').find("input[id*='txtBalAmt']").val("");
            }
            else {
                $(txtApproveAmt).closest('tr').find("input[id*='txtBalAmt']").val(parseFloat(billAmt) - parseFloat(approveAmt));
            }
        }

        function CheckAmt_Hold(txtApproveAmt) {
            debugger;
            var approveAmt = txtApproveAmt.value;
            var requestedAmt = $(txtApproveAmt).closest('tr').find('td:eq(6)').text();
            if (parseFloat(approveAmt) > parseFloat(requestedAmt)) {
                alert("Approve amount should not be greater than requested amount");
                txtApproveAmt.value = "";
            }
        }

        function CalculateTDSAmt(txt) {

            var approvedAmt = jQuery(txt).closest('tr').find('td:eq(4)').text();
            var tdsPerc = jQuery(txt).closest('tr').find("input[id*='txtTDSPerc']").val();
            var tdsAmt = ((parseFloat(approvedAmt) * parseFloat(tdsPerc) / 100).toFixed(2));
            if (tdsAmt != NaN && tdsAmt >= 0) {
                jQuery(txt).closest('tr').find("input[id*='txtTDSAmt']").val(tdsAmt);
            }
            else {
                jQuery(txt).closest('tr').find("input[id*='txtTDSAmt']").val("0.00");
            }
        }

        function CalculateTDSAmt_Returned(txt) {

            var approvedAmt = jQuery(txt).closest('tr').find('td:eq(4)').text();
            var tdsPerc = jQuery(txt).closest('tr').find("input[id*='txtTDSPerc_Returned']").val();
            var tdsAmt = ((parseFloat(approvedAmt) * parseFloat(tdsPerc) / 100).toFixed(2));
            if (tdsAmt != NaN && tdsAmt >= 0) {
                jQuery(txt).closest('tr').find("input[id*='txtTDSAmt_Returned']").val(tdsAmt);
            }
            else {
                jQuery(txt).closest('tr').find("input[id*='txtTDSAmt_Returned']").val("0.00");
            }
        }

        function calcTotalAmt(chkSelect) {

            var totalAmt = 0.00;
            var FormatedtotalAmt = 0.00;
            var grid = jQuery(chkSelect).closest('table');

            jQuery(grid).find('input[type="checkbox"]').each(function () {
                if (jQuery(this).is(":checked")) {
                    var amt = jQuery(this).closest('tr').find('td:eq(7)').find('[id*="txtApproveAmt"]').val();
                    totalAmt = parseFloat(totalAmt) + parseFloat(amt);
                    totalAmt.toFixed(3);
                }
            });
            FormatedtotalAmt = totalAmt.toFixed(2);
            jQuery(grid).parent('div').parent('div').next('div').find('table tr td:eq(7)').css({ textAlign: 'center' });
            jQuery(grid).parent('div').parent('div').next('div').find('table tr td:eq(7)').html(FormatedtotalAmt);
        }

        function calcTotalAmt_Partial(chkSelect) {

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

        function calcTotalAmtApproveAmt_Hold(chkSelect) {
            debugger;
            var totalAmt_Hold = 0;
            var FormatedtotalAmt_Hold = 0.00;
            var grid = jQuery(chkSelect).closest('table');

            jQuery(grid).find('[id*="chkSelect_Hold"]').each(function () {
                if (jQuery(this).is(":checked", false)) {
                    var amt = jQuery(this).closest('tr').find('td:eq(6)').find('[id*="txtApproveAmt_Hold"]').val();
                    totalAmt_Hold = parseFloat(totalAmt_Hold) + parseFloat(amt);

                }
            });

            FormatedtotalAmt_Hold = totalAmt_Hold.toFixed(2);
            jQuery(grid).parent('div').parent('div').next('div').find('table tr td:eq(6)').css({ textAlign: 'center' });
            jQuery(grid).parent('div').parent('div').next('div').find('table tr td:eq(6)').html(FormatedtotalAmt_Hold);
        }

        function ShowPopup() {
            $('#myModal').modal('show');
        }

        $(document).ready(function () {
            $('.chosen-select').chosen();
        });
    </script>

    <style>
        .ob_gC {
            font-size: 12px !important;
        }

        .ob_gCFR.ob_gC.ob_gCFR .ob_gCW {
            width: 130px;
            text-align: center;
            font-size: 16px;
        }

        element.style {
            width: 130px;
            text-align: center;
            font-size: 16px;
        }

        .ob_gMCont {
            width: 97% !important;
        }

        #ContentPlaceHolder1_grid_PendingPayInd_ob_grid_PendingPayIndMainContainer {
            width: 97% !important;
        }

        .contrctwidth {
            width: 100% !important;
            padding: 0px !important;
            border-radius: 15px;
            padding-left: 50px;
            padding-right: 50px;
        }
    </style>
    <style>
        .panel-heading a:after {
            font-family: 'Glyphicons Halflings';
            content: "\e114";
            float: right;
            color: grey;
        }

        .panel-heading a.collapsed:after {
            content: "\e080";
        }
    </style>

    <div class="panel panel-default">
        <div class="row">
            <div class="col-md-2" style="text-align: center; font-size: large;">Filter : </div>
            <div class="col-md-8">
                <asp:DropDownList ID="ddlSearch" runat="server" class="chosen-select form-control" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnClearFilter" runat="server" Text="Clear Filter" OnClick="btnClearFilter_Click" CssClass="btn btn-default"></asp:Button>
            </div>
        </div>

        <button type="button" class="panel-heading contrctwidth" data-toggle="collapse" data-target="#page-PendingPayInd">
            <h4 style="text-align: center">Pending Payment Indent List</h4>
        </button>
        <div id="page-PendingPayInd" class="panel-collapse panel-body collapse out">
            <div class="panel-body" id="demo">
                <center>
                <ogrid:Grid runat="server" ID="grid_PendingPayInd" CallbackMode="false" AutoGenerateColumns="false" GroupBy="Priority_Satus,Project_Name,Payment_Category" FolderStyle="../Gridstyles/grand_gray" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowGrouping="True" AllowPaging="true" PageSize="100" ShowColumnsFooter="true" Width="97%"
                    OnRowDataBound="grid_PendingPayInd_RowDataBound" ShowCollapsedGroups="True" FilterShowButton_TemplateId="Project_Name" PageSizeOptions="100">
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings  CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />
                    
                    <Columns>
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Date" Width="120px" DataFormatString="{0:d}"></ogrid:Column>
                        <ogrid:Column DataField="State_Name" HeaderText="State" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Payment_Category" Visible="false" HeaderText="Payment Category" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Project_Name" Visible="false" HeaderText="Project Name" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Ben_Name" HeaderText="Company Name" Wrap="true"  Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="WorkDoneFor" HeaderText="Work Done For/Item Supplied to" Width="100px"></ogrid:Column>
                        <ogrid:Column DataField="Amt_PartPayment" HeaderText="Bill Amount" Width="100px"></ogrid:Column>
                        <ogrid:Column DataField="Amt_PartPayment" HeaderText="Amount" Width="100px"></ogrid:Column>
                        <ogrid:Column HeaderText="Approved Amount" AllowEdit="true" Align="Right"  width="145px" ItemStyle-Font-Size="16px" >
                            <TemplateSettings TemplateId="AmtTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column HeaderText="Select" Width="60px" >
                            <TemplateSettings TemplateId="SelectTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column HeaderText="Balance Amount">
                            <TemplateSettings TemplateId="BalAmtTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Name" HeaderText="Prepared By"></ogrid:Column>
                        <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No" Width="150px" >
                            <TemplateSettings  TemplateId="PayIndNoTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Bank_Name" HeaderText="Bank Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Branch" HeaderText="Branch Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Account" HeaderText="Account Number"></ogrid:Column>
                        <ogrid:Column DataField="Bank_IFSC" HeaderText="IFSC Code"></ogrid:Column>
                        <ogrid:Column DataField="Status" HeaderText="Status" Visible="false"></ogrid:Column>
                        <ogrid:Column DataField="Approver" HeaderText="Approver" Visible="false"></ogrid:Column>
                        <ogrid:Column  HeaderText="Download Document" Width="200px" Wrap="true" >
                            <TemplateSettings TemplateId="lnkDownloadDocumentTemplate" />
                        </ogrid:Column>
                         <ogrid:Column DataField="Priority_Satus" HeaderText="Priority Satus" ></ogrid:Column>
                    </Columns>
                    <ScrollingSettings NumberOFFixedColumns="3" ScrollWidth="1000" FixedColumnsPosition="Left"/>
                    <Templates>
                        <ogrid:GridTemplate ID="SelectTemplate" runat="server">
                            <Template>
                                <asp:CheckBox runat="server" ID="chkSelect"  />
                                <asp:HiddenField ID="hdn_payIndNo" runat="server" Value='<%# Container.DataItem["PayInd_No"] %>'/>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="AmtTemplate" runat="server">
                            <Template>
                                <asp:TextBox runat="server"  Value='<%# Container.DataItem["Amt_PartPayment"] %>' ID="txtApproveAmt" OnKeyUp="CheckAmt(this);" AutoComplete="Off" CssClass="gridCB" Width="100px"></asp:TextBox>
                                <%--<footer>
                                    <asp:Label runat="server" ID="lblTotalApproveAmt" Text="100"></asp:Label>
                                </footer>--%>
                            </Template>
                            
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="BalAmtTemplate" runat="server">
                            <Template>
                                <asp:TextBox runat="server" ID="txtBalAmt" Enabled="false" CssClass="gridCB"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="PayIndNoTemplate" runat="server">
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
                    </Templates>

                </ogrid:Grid>
                <br />
                <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="btnHold" runat="server" Text="Hold"  OnClick="btnHold_Click" CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="btnExportToPDF1" runat="server" Text="Export To PDF" OnClick="btnExportToPDF1_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid1()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
            </center>
            </div>

        </div>

        <button type="button" class="panel-heading contrctwidth" data-toggle="collapse" data-target="#page-OnHoldPayInd">
            <h4 style="text-align: center">On Hold Payment Indent List</h4>
        </button>
        <div id="page-OnHoldPayInd" class="panel-collapse panel-body collapse out">
            <div class="panel-body">
                <center>
                <ogrid:Grid runat="server" ID="grid_OnHoldPayInd" CallbackMode="false" GroupBy="Project_Name,Payment_Category" AllowGrouping="true" AutoGenerateColumns="false"  FolderStyle="../Gridstyles/grand_gray" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="100" ShowColumnsFooter="true"
                    OnRowDataBound="grid_OnHoldPayInd_RowDataBound" ShowCollapsedGroups="True" PageSizeOptions="100">
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

                    <Columns>
                        
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Date" Width="90px" DataFormatString="{0:d}"></ogrid:Column>
                        <ogrid:Column DataField="Project_Name" Visible="false" HeaderText="Project Name" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Payment_Category" Visible="false" HeaderText="Payment Category" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Ben_Name" HeaderText="Company Name" Wrap="true"  Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="WorkDoneFor" HeaderText="Work Done For/Item Supplied to" Width="100px"></ogrid:Column>
                        <ogrid:Column DataField="Amt_ServiceMaterial" HeaderText="Bill Amount" Width="100px"></ogrid:Column>
                        <ogrid:Column DataField="Amt_PartPayment" HeaderText="Amount" Align="Right" Width="90px"></ogrid:Column>

                        <ogrid:Column DataField="Amt_Approved" HeaderText="Approved Amount" Align="right" width="145px"  AllowEdit="true">
                            <TemplateSettings TemplateId="AmtTemplate_Hold"/>
                        </ogrid:Column>
                          <%-- <ogrid:CheckBoxSelectColumn ShowHeaderCheckBox="true" HeaderText="" ID="chkSelect_Hold" OnClick="calcTotalAmtApproveAmt_Hold"    HeaderAlign="left" Align="center"></ogrid:CheckBoxSelectColumn>
                            --%>
                        <ogrid:Column HeaderText="Select" Width="100px" >
                            <TemplateSettings TemplateId="SelectTemplate_Hold"/>
                              
                        </ogrid:Column>
                        
                        <ogrid:Column DataField="Name" HeaderText="Prepared By"></ogrid:Column>
                        <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No" Width="150px">
                             <TemplateSettings  TemplateId="PayIndNoTemplate_Hold"/>
                        </ogrid:Column>
                      
                        <ogrid:Column DataField="Bank_Name" HeaderText="Bank Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Branch" HeaderText="Branch Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Account" HeaderText="Account Number"></ogrid:Column>
                        <ogrid:Column DataField="Bank_IFSC" HeaderText="IFSC Code"></ogrid:Column>
                        <ogrid:Column DataField="Status" HeaderText="Status" Visible="false"></ogrid:Column>
                        <ogrid:Column DataField="Approver" HeaderText="Approver" Visible="false"></ogrid:Column>
                         <ogrid:Column  HeaderText="Download Document" Width="200px" Wrap="true" >
                            <TemplateSettings TemplateId="lnkDownloadDocument_OnHold_Template" />
                        </ogrid:Column>

                    </Columns>

                    <Templates>
                        <ogrid:GridTemplate ID="AmtTemplate_Hold" runat="server">
                            <Template>
                                <asp:TextBox runat="server" ID="txtApproveAmt_Hold" Text='<%# Container.DataItem["Amt_Approved"] %>' OnKeyUp="CheckAmt_Hold(this);" CssClass="gridCB" AutoComplete="Off"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                       <%-- <ogrid:GridTemplate>
                                <HeaderTemplate  ID="AmtTemplate_Hold_Select_All" runat="server">
                                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="SelectAll(this);" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectItem" runat="server" />
                                </ItemTemplate>
                            </ogrid:GridTemplate>--%>
                          <ogrid:GridTemplate ID="PayIndNoTemplate_Hold" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["PayInd_No"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="SelectTemplate_Hold" runat="server">
                            <Template>
                                <asp:CheckBox runat="server" ID="chkSelect_Hold" onChange="calcTotalAmtApproveAmt_Hold(this)" />
                                <asp:HiddenField ID="hdn_payIndNo" runat="server" Value='<%# Container.DataItem["PayInd_No"] %>'/>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="lnkDownloadDocument_OnHold_Template" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnkWOItem" Text="View Document" CommandArgument='<%#Container.DataItem["PayInd_No"] %>' CommandName='<%#Container.DataItem["PayInd_No"] %>'  OnClick="lnkDownloadDocumentItem_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>
                <br />
                <asp:Button ID="btnApprove_Hold" runat="server" Text="Approve" OnClick="btnApprove_Hold_Click" CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="btnExportToPDF2" runat="server" Text="Export To PDF" OnClick="btnExportToPDF2_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid2()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
            </center>
            </div>
        </div>

        <button type="button" class="panel-heading contrctwidth" data-toggle="collapse" data-target="#page-PartialPayment">
            <h4 style="text-align: center">Pending Partial Payment Indent </h4>
        </button>
        <div id="page-PartialPayment" class="panel-collapse panel-body collapse out">
            <div class="panel-body">
                <center>
                <ogrid:Grid runat="server" ID="grid_PartialPayment" CallbackMode="false" GroupBy="Project_Name,Payment_Category" AllowGrouping="true"  AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="100" ShowColumnsFooter="true" Width="97%"
                    OnRowDataBound="grid_Pending_PartialPayment_RowDataBound" ShowCollapsedGroups="True" PageSizeOptions="100">
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings  CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />
                    
                    <Columns>
                       
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Date" Width="120px" DataFormatString="{0:d}">      </ogrid:Column>
                        <ogrid:Column DataField="Project_Name" Visible="false" HeaderText="Project Name" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Payment_Category" Visible="false" HeaderText="Payment Category" Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Ben_Name" HeaderText="Company Name" Wrap="true"  Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="WorkDoneFor" HeaderText="Work Done For/Item Supplied to" Width="100px"></ogrid:Column>
                        <ogrid:Column DataField="Amt_ServiceMaterial" HeaderText="Bill Amount" Width="100px"></ogrid:Column>
                        <ogrid:Column DataField="Amt_PartPayment" HeaderText="Amount" Width="100px"></ogrid:Column>
                        <ogrid:Column HeaderText="Approved Amount" AllowEdit="true" Align="Right"  width="145px" ItemStyle-Font-Size="16px" >
                            <TemplateSettings TemplateId="AmtTemplate_Partial"/>
                        </ogrid:Column>
                         <ogrid:Column HeaderText="Select" Width="60px" >
                            <TemplateSettings TemplateId="SelectTemplate_Partial"/>
                        </ogrid:Column>
                        <ogrid:Column HeaderText="Balance Amount">
                            <TemplateSettings TemplateId="BalAmtTemplate_Partial"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Name" HeaderText="Prepared By"></ogrid:Column>
                        <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No" Width="150px" >
                            <TemplateSettings  TemplateId="PayIndNoTemplate_Partial"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Bank_Name" HeaderText="Bank Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Branch" HeaderText="Branch Name" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Account" HeaderText="Account Number"></ogrid:Column>
                        <ogrid:Column DataField="Bank_IFSC" HeaderText="IFSC Code"></ogrid:Column>
                        <ogrid:Column DataField="Status" HeaderText="Status" Visible="false"></ogrid:Column>
                        <ogrid:Column DataField="Approver" HeaderText="Approver" Visible="false"></ogrid:Column>
                         <ogrid:Column  HeaderText="Download Document" Width="200px" Wrap="true" >
                            <TemplateSettings TemplateId="lnkDownloadDocument_PartialPayment_Template" />
                        </ogrid:Column>

                    </Columns>
                    <ScrollingSettings NumberOFFixedColumns="3" ScrollWidth="1000" FixedColumnsPosition="Left"/>
                    <Templates>
                        <ogrid:GridTemplate ID="SelectTemplate_Partial" runat="server">
                            <Template>
                                <asp:CheckBox runat="server" ID="chkSelect" onChange="calcTotalAmt_Partial(this)" />
                                <asp:HiddenField ID="hdn_payIndNo" runat="server" Value='<%# Container.DataItem["PayInd_No"] %>'/>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="AmtTemplate_Partial" runat="server">
                            <Template>
                                <asp:TextBox runat="server"  Value='<%# Container.DataItem["Amt_PartPayment"] %>' ID="txtApproveAmt" OnKeyUp="CheckAmt_Partial(this);" AutoComplete="Off" CssClass="gridCB" Width="100px"></asp:TextBox>
                                <%--<footer>
                                    <asp:Label runat="server" ID="lblTotalApproveAmt" Text="100"></asp:Label>
                                </footer>--%>
                            </Template>
                            
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="BalAmtTemplate_Partial" runat="server">
                            <Template>
                                <asp:TextBox runat="server" ID="txtBalAmt" Enabled="false" CssClass="gridCB"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="PayIndNoTemplate_Partial" runat="server">
                            <Template>
                                <asp:LinkButton  ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["PayInd_No"] %>'></asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                         <ogrid:GridTemplate ID="lnkDownloadDocument_PartialPayment_Template" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnkWOItem" Text="View Document" CommandArgument='<%#Container.DataItem["PayInd_No"] %>' CommandName='<%#Container.DataItem["PayInd_No"] %>'  OnClick="lnkDownloadDocumentItem_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>
                <br />
                <asp:Button ID="btnPartialAprove" runat="server" Text="Approve" OnClick="btnApprove_Partial_Click" CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="btnPartialHold" runat="server" Text="Hold"  OnClick="btnHold_Partial_Click" CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="btnPartialExportToPDF" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Partial_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="Partialexportgrid1()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
            </center>
            </div>
        </div>

        <button type="button" class="panel-heading contrctwidth" data-toggle="collapse" data-target="#page-ApprovedPayInd">
            <h4 style="text-align: center">Approved Payment Indent List </h4>
        </button>
        <div id="page-ApprovedPayInd" class="panel-collapse panel-body collapse out">
            <div class="panel-body">
                <center>
                <ogrid:Grid runat="server" ID="grid_ApprovedPayInd" CallbackMode="false"  AllowGrouping="true" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" ShowColumnsFooter="true" PageSize="10" OnUpdateCommand="grid_ApprovedPayInd_UpdateCommand"  OnRowDataBound="grid_Approved_Payment_RowDataBound">
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true"   ExportedFilesTargetWindow="New" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

                    <Columns>
                        <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No" runat="server" Width="150px" ReadOnly="true">
                            <TemplateSettings  TemplateId="PayIndNoTemplateComplete"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Request Date" Width="120px" DataFormatString="{0:d}" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Ben_Name" HeaderText="Vendor/SubContractor" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <%--<ogrid:Column DataField="Order_Type" HeaderText="Order Type" Width="100px" ReadOnly="true"></ogrid:Column>--%>
                        <ogrid:Column DataField="Amt_Approved" HeaderText="Approved Amount" id="Amt_Approved" ReadOnly="true"></ogrid:Column>
                         <ogrid:Column HeaderText="Select to Complete" Width="60px" >
                            <TemplateSettings TemplateId="SelectTemplate_MakeComplete"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="TDS_Perc" HeaderText="TDS Deducted %" Width="140px" runat="server">
                            <TemplateSettings EditTemplateId="EditTDSPercTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="TDS_Amt" HeaderText="TDS Deducted Amt" Width="140px" runat="server">
                            <TemplateSettings EditTemplateId="EditTDSAmtTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Other_Deduction" HeaderText="Other Deduction (If Any)" Width="140px" runat="server">
                            <TemplateSettings EditTemplateId="EditOtherDedTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Amt_Transferable" HeaderText="Transferable Amount" Width="140px" runat="server">
                            <TemplateSettings EditTemplateId="EditTransAmtTemplate" />
                        </ogrid:Column>

                        <ogrid:Column DataField="Bank_Name" HeaderText="Beneficiary Bank" Wrap="true" ReadOnly="true"></ogrid:Column>
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
                        <ogrid:Column DataField="Narration" HeaderText="Narration"  Width="280px">
                            <TemplateSettings EditTemplateId="EditNarrationTemplate" />
                        </ogrid:Column>
                         <ogrid:Column DataField="Name" HeaderText="Prepared By" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column AllowEdit="true" Width="90px"></ogrid:Column>
                        <ogrid:Column DataField="Approver" HeaderText="Approver" Visible="false"></ogrid:Column>
                    </Columns>

                    <Templates>
                        <ogrid:GridTemplate runat="server" ID="EditTDSPercTemplate" ControlID="txtTDSPerc" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTDSPerc" onkeypress="return allowOnlydecimalNumber(event)" OnKeyUp="return CalculateTDSAmt(this)" Width="70px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditTDSAmtTemplate" ControlID="txtTDSAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTDSAmt" onkeypress="return allowOnlydecimalNumber(event)" Enabled="false" Width="80px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditOtherDedTemplate" ControlID="txtOtherDed" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtOtherDed" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
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
                            <TemplateSelectTemplate_MakeComplete
                                <asp:TextBox ID="txtNarration" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="SelectTemplate_MakeComplete" runat="server">
                            <Template>
                                <asp:CheckBox runat="server" ID="chkSelect" />
                                <asp:HiddenField ID="hdn_payIndNo" runat="server" Value='<%# Container.DataItem["PayInd_No"] %>'/>
                            </Template>
                        </ogrid:GridTemplate>
                        
                        <ogrid:GridTemplate ID="PayIndNoTemplateComplete" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["PayInd_No"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>
                <br />
                <a href="#StatusReverse" data-toggle="modal" role="button">
                  <asp:Button ID="BTN_AddReverse" runat="server" CssClass="btn btn-default"
                        Text="Reverse"/>
                  </a>
                 <asp:Button ID="BtnMake_Completed" runat="server" Text="Move To Completed" OnClick="btnMake_Completed_Click" CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="btnExportToPDF3" runat="server" Text="Export To PDF" OnClick="btnExportToPDF3_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid3()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
            </center>
            </div>
        </div>

        <div class="modal small fade popupposition" id="StatusReverse" tabindex="-1" role="dialog" aria-labelledby="addpackLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h5 id="addpackLabel">Are you sure to reverse this transcation!</h5>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnyes" runat="server" Text="Yes" CssClass="btn btn-default" ValidationGroup="valpack" OnClick="btnyes_Click" />
                        <asp:Button ID="btnno" runat="server" Text="No" CssClass="btn btn-default" ValidationGroup="valpack" />
                    </div>
                </div>
            </div>
        </div>


        <button type="button" class="panel-heading contrctwidth" data-toggle="collapse" data-target="#page-GridReturnedPayInd">
            <h4 style="text-align: center">Returned Payment Indent List </h4>
        </button>
        <div id="page-GridReturnedPayInd" class="panel-collapse panel-body collapse out">
            <div class="panel-body">
                <center>
                <ogrid:Grid runat="server" ID="grid_ReturnedPayInd" CallbackMode="false" AllowGrouping="true"  AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10" OnUpdateCommand="grid_ReturnedPayInd_UpdateCommand"  OnRowDataBound="grid_Returned_Payment_RowDataBound">
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

                    <Columns>
                        <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No" runat="server" Width="150px" ReadOnly="true">
                            <TemplateSettings  TemplateId="PayIndNoTemplateReturned"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="PayInd_Date" HeaderText="Request Date" Width="120px" DataFormatString="{0:d}" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Ben_Name" HeaderText="Vendor/SubContractor" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Order_Type" HeaderText="Order Type" Width="100px" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Amt_Approved" HeaderText="Approved Amount" ReadOnly="true"></ogrid:Column>

                        <ogrid:Column DataField="TDS_Perc" HeaderText="TDS Deducted %" Width="140px" runat="server">
                            <TemplateSettings EditTemplateId="EditTDSPercTemplate_Returned" />
                        </ogrid:Column>
                        <ogrid:Column DataField="TDS_Amt" HeaderText="TDS Deducted Amt" Width="140px" runat="server">
                            <TemplateSettings EditTemplateId="EditTDSAmtTemplate_Returned" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Other_Deduction" HeaderText="Other Deduction (If Any)" Width="140px" runat="server">
                            <TemplateSettings EditTemplateId="EditOtherDedTemplate_Returned" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Amt_Transferable" HeaderText="Transferable Amount" Width="140px" runat="server">
                            <TemplateSettings EditTemplateId="EditTransAmtTemplate_Returned" />
                        </ogrid:Column>

                        <ogrid:Column DataField="Bank_Name" HeaderText="Beneficiary Bank" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Account" HeaderText="Beneficiary Account Number" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_IFSC" HeaderText="Beneficiary IFSC Code" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Bank_Branch" HeaderText="Beneficiary Branch" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Payment_Mode" HeaderText="Payment Mode" Width="140px"  ReadOnly="true"></ogrid:Column>
                         <ogrid:Column DataField="Company_Bank" HeaderText="Company Bank" Width="140px" >
                            <TemplateSettings TemplateId="EditCompany_BankTemplate_Returned" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Payment_Date" HeaderText="Payment Date" Width="140px"  DataFormatString="{0:d}">
                            <TemplateSettings EditTemplateId="EditPaymentDateTemplate_Returned" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Payment_Ref_No" HeaderText="Payment Reference No." Width="140px" >
                            <TemplateSettings EditTemplateId="EditPaymentRefNoTemplate_Returned" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Narration" HeaderText="Narration"  Width="280px">
                            <TemplateSettings EditTemplateId="EditNarrationTemplate_Returned" />
                        </ogrid:Column>
                         <ogrid:Column DataField="Name" HeaderText="Prepared By" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column AllowEdit="true" Width="90px"></ogrid:Column>
                        <ogrid:Column DataField="Approver" HeaderText="Approver" Visible="false"></ogrid:Column>
                         <ogrid:Column  HeaderText="Download Document" Width="200px" Wrap="true" >
                            <TemplateSettings TemplateId="lnkDownloadDocument_Returned_Template" />
                        </ogrid:Column>

                    </Columns>

                    <Templates>
                        <ogrid:GridTemplate ID="PayIndNoTemplateReturned" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["PayInd_No"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditTDSPercTemplate_Returned" ControlID="txtTDSPerc_Returned" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTDSPerc_Returned" onkeypress="return allowOnlydecimalNumber(event)" OnKeyUp="return CalculateTDSAmt_Returned(this)" Width="70px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditTDSAmtTemplate_Returned" ControlID="txtTDSAmt_Returned" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTDSAmt_Returned" onkeypress="return allowOnlydecimalNumber(event)" Enabled="false" Width="80px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditOtherDedTemplate_Returned" ControlID="txtOtherDed_Returned" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtOtherDed_Returned" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplate_Returned" ControlID="txtTransAmt_Returned" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTransAmt_Returned" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditCompany_BankTemplate_Returned" ControlID="ddlCompany_Bank_Returned" ControlPropertyName="value">
                            <Template>
                                <asp:DropDownList  ID="ddlCompany_Bank_Returned" Width="120px" runat="server" ></asp:DropDownList>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditPaymentDateTemplate_Returned" ControlID="txtPaymentDate_Returned" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPaymentDate_Returned" Width="120px" runat="server" TextMode="Date" Text='<%# Container.DataItem["Payment_Date"] %>'></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditPaymentRefNoTemplate_Returned" ControlID="txtPaymentRefNo_Returned" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPaymentRefNo_Returned" Width="150px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditNarrationTemplate_Returned" ControlID="txtNarration_Returned" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtNarration_Returned" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="lnkDownloadDocument_Returned_Template" runat="server">
                            <Template>
                            <asp:LinkButton ID="lnkWOItem" Text="View Document" CommandArgument='<%#Container.DataItem["PayInd_No"] %>' CommandName='<%#Container.DataItem["PayInd_No"] %>'  OnClick="lnkDownloadDocumentItem_Click" runat="server" CssClass="gridCB">
                                        </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>
                <br />
                <asp:Button ID="Button1" runat="server" Text="Export To PDF" OnClick="btnExportToPDF3_Click" CssClass="btn btn-default"></asp:Button>
                <input onclick="exportgrid3()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />   
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

     </div>

    </asp:Panel>

    <asp:Button runat="server" ID="btnpayadvice" Style="display: none"></asp:Button>
     <ajaxToolkit:ModalPopupExtender ID="ModalPaymentAdvice" runat="server" PopupControlID="paymentadvice" TargetControlID="btnpayadvice"
        CancelControlID="BtnClose10" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    
    <asp:Panel ID="paymentadvice" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose10" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h5>PAYMENT ADVICE</h5></center>
                </div>
                <div class="modal-body">

                 
                    
                    
                     <div class="row">
                  <h4 style="text-align:center"></h4>
                <center>
                    <ogrid:Grid ID="Gridpayadvice" CallbackMode="false" runat="server"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                        <ScrollingSettings ScrollWidth="95%" />
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <Columns>
                             <ogrid:Column DataField="PayInd_No" HeaderText="PayInd No" Wrap="true" Width="100px"></ogrid:Column>
                            <ogrid:Column DataField="PayInd_Date" HeaderText="PayInd Date" Wrap="true" Width="100px" Visible="false"></ogrid:Column>
                            <ogrid:Column DataField="Beneficiary_Type" HeaderText="Beneficiary Type" Wrap="true" Width="100px" Visible="false"></ogrid:Column>
                            <ogrid:Column DataField="Amt_Approved" HeaderText="Amount Approved" Wrap="true" Width="100px"></ogrid:Column>
                            <ogrid:Column DataField="Status" HeaderText="Status" Wrap="true" Width="100px"></ogrid:Column>
                            <ogrid:Column DataField="Project_Code" HeaderText="Project Code" Width="130px" Wrap="true" Visible="false"></ogrid:Column>
                            <ogrid:Column HeaderText="Edit" Width="100px">
                                        <TemplateSettings TemplateId="EditLCT" />
                                    </ogrid:Column>

                               
                                
                             
                        </Columns>
                        <Templates>
                            <ogrid:GridTemplate runat="server" ID="EditLCT">
                                        <Template>
              <asp:LinkButton ID="lnkBtnPaymentAdvice" CausesValidation="false" CommandName='<%# Container.DataItem["PayInd_No"] %>' OnClick="lnkBtnPaymentAdvice_Click" Text="Payment Advice" CssClass="gridCB" runat="server"></asp:LinkButton>
                                        </Template>
                                    </ogrid:GridTemplate>
                         </Templates>
                       
                    </ogrid:Grid>
                </center>
            </div>
                </div>
               
            </div>
        </div>

    </asp:Panel>
</asp:Content>
