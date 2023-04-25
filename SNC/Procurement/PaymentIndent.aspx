<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Procurement/PaymentIndent.aspx.cs" Inherits="PaymentIndent" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });
        $(document).ready(function () {
            var TotalPayable = document.getElementById("<%=txtTotalPayable.ClientID%>").value;
            var Earlier_Payments = document.getElementById("<%=txtAmt_EarlierPayment.ClientID%>").value;
            document.getElementById("<%=txtAmt_PartPayment .ClientID%>").value = Math.round(TotalPayable - Earlier_Payments)
        });

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

        function CalculateItemAmt() {
            var TotalPayable = document.getElementById("<%=txtTotalPayable.ClientID%>").value;
            var Earlier_Payments = document.getElementById("<%=txtAmt_EarlierPayment.ClientID%>").value;
            document.getElementById("<%=txtAmt_PartPayment .ClientID%>").value = Math.round(TotalPayable - Earlier_Payments)
        }

        function CalculateGSTAmout() {
            debugger;
            let Services_Materials_Received = document.getElementById("<%=txtAmt_ServiceMaterial.ClientID%>").value;
            /*Services_Materials_Received = parseFloat(Services_Materials_Received).toFixed(2);*/
            let GST_Percent = document.getElementById("<%=txtGSTPercent.ClientID%>").value;
            let GST_Amount = Math.round(Services_Materials_Received * GST_Percent / 100)
            document.getElementById("<%=txtGSTAmount .ClientID%>").value = Math.round(Services_Materials_Received * GST_Percent / 100);
            /*GST_Amount = parseFloat(GST_Amount).toFixed(2);*/
            var result = parseInt(Services_Materials_Received) + parseInt(GST_Amount);
            document.getElementById("<%=txtBasicTotal .ClientID%>").value = (result);
        }

        function CalculateTDSAmout() {
            debugger;
            var Services_Materials_Received = document.getElementById("<%=txtAmt_ServiceMaterial.ClientID%>").value;
            var TDS_Percent = document.getElementById("<%=ddlTDSPercent.ClientID%>").value;
            var TDSDeducted_Amt = Math.round(Services_Materials_Received * TDS_Percent / 100)
            document.getElementById("<%=txtTDSDeductedAmt .ClientID%>").value = Math.round(Services_Materials_Received * TDS_Percent / 100)
            var OtherDeductions = document.getElementById("<%=txtOtherDeductions.ClientID%>").value;
            if (OtherDeductions.trim().length == 0) {
                OtherDeductions = "0";
            }
            var TDSAndOtherD = parseInt(TDSDeducted_Amt) + parseInt(OtherDeductions);
            var TotalAmount = document.getElementById("<%=txtBasicTotal.ClientID%>").value;
            var result = parseInt(TotalAmount) - parseInt(TDSAndOtherD);
            document.getElementById("<%=txtTotalPayable .ClientID%>").value = Math.round(result)

            <%--var Earlier_Payments = document.getElementById("<%=txtAmt_EarlierPayment.ClientID%>").value;--%>
        }


        function Checkboxval() {
            alert("testing");

            debugger;
            if (document.getElementById("<%= CheckBox_RA_bill.ClientID %>")) {

            }
            if ('#CheckBox_RA_bill'.checked) {
                $('#fuBill').validate({
                    rules: { inputimage: { required: true, extension: "png|jpe?g|gif", filesize: 1048576 } },
                    messages: { inputimage: "File must be JPG, GIF or PNG, less than 1MB" }
                });
            }
        }

    </script>
    
    <script type="text/javascript">
        function openInNewTab() {
            window.document.forms[0].target = '_blank';
            setTimeout(function () { window.document.forms[0].target = ''; }, 0);
        }
    </script>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $('.chosen-select').chosen();
        });
    </script>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Payment Indent

            </h3>
        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->
            <div class="row">
                <div class="col-md-10" style="text-align: center; padding-left: 300px; font-size: 80px">
                    <asp:Label Font-Size="Large" BackColor="WhiteSmoke" runat="server" ID="Status" Enabled="false" CssClass="form-control"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">Payment Indent No</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPayIndNo" Enabled="false" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    Date&nbsp;
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtPayIndDate" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPayIndDate" autocomplete="off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtPayIndDate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">State</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control" TabIndex="3"></asp:DropDownList>
                </div>
                <div class="col-md-2">Project</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProject" runat="server" class="chosen-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="4"></asp:DropDownList>
                    <%--<asp:TextBox runat="server" ID="txtProject" autocomplete= "off" CssClass="form-control" TabIndex="8"></asp:TextBox>--%>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">Financial Year</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlFYear" CssClass="form-control" TabIndex="5">
                        <asp:ListItem Value="2020-21" Text="2020-21"></asp:ListItem>
                        <asp:ListItem Value="2021-22" Text="2021-22"></asp:ListItem>
                        <asp:ListItem Value="2022-23" Selected="True" Text="2022-23"></asp:ListItem>
                        <asp:ListItem Value="2023-24" Text="2023-24"></asp:ListItem>
                        <asp:ListItem Value="2024-25" Text="2024-25"></asp:ListItem>
                        <asp:ListItem Value="2025-26" Text="2025-26"></asp:ListItem>
                        <asp:ListItem Value="2026-27" Text="2026-27"></asp:ListItem>
                        <asp:ListItem Value="2027-28" Text="2027-28"></asp:ListItem>
                        <asp:ListItem Value="2028-29" Text="2028-29"></asp:ListItem>
                        <asp:ListItem Value="2029-30" Text="2029-30"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">Verifier Name</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlVerifier" class="chosen-select form-control" TabIndex="6"></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">Beneficiary Type</div>
                <div class="col-md-4">
                    <asp:RadioButtonList runat="server" ID="rblBenType" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblBenType_SelectedIndexChanged" TabIndex="7">
                        <asp:ListItem Value="Vendor" Text="Vendor" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="SubContractor" Text="Sub Contractor"></asp:ListItem>
                        <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div runat="server" id="Ra_Dvi">
                    <div class="col-md-2">RA Bill</div>
                    <div class="col-md-4">
                        <asp:CheckBox ID="CheckBox_RA_bill" runat="server" />
                    </div>
                </div>
                <div runat="server" id="div_Vendor">
                    <div class="col-md-2">
                        Vendor Name
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="-Select-" ControlToValidate="ddlVendor" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlVendor" AutoPostBack="true" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" class="chosen-select form-control" TabIndex="8"></asp:DropDownList>
                    </div>
                </div>
                <div runat="server" id="div_SubCon">
                    <div class="col-md-2">
                        Sub Contractor Name
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="-Select-" ControlToValidate="ddlSubContractor" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlSubContractor" AutoPostBack="true" OnSelectedIndexChanged="ddlSubContractor_SelectedIndexChanged" class="chosen-select form-control" TabIndex="8"></asp:DropDownList>
                    </div>
                </div>
                <div runat="server" id="div_Other">
                    <div class="col-md-2">
                        Other Name
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue="-Select-" ControlToValidate="ddlOther" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlOther" AutoPostBack="true" OnSelectedIndexChanged="ddlOther_SelectedIndexChanged" CssClass="form-control" TabIndex="8"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">Name Of Work</div>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="-Select-" ControlToValidate="ddlVendor" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtWorkDesc" Enabled="false" TextMode="MultiLine" Rows="2" CssClass="form-control" TabIndex="5"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    Internal Project Code 
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtDespatchAdvise" CssClass="Validation_Text" ValidationGroup="ValPO" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtProjectCode" Enabled="false" CssClass="form-control" TabIndex="6"></asp:TextBox>
                </div>
                <div class="col-md-2">Govt. Department Name</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtAwardedBy" Enabled="false" CssClass="form-control" TabIndex="8"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div style="display: none">
                    <div class="col-md-2">Principal Contractor Name</div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtPrincipalContractor" autocomplete="off" CssClass="form-control" TabIndex="8"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">
                    Legal Name
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtLegalName" Enabled="false" CssClass="form-control" TabIndex="8"></asp:TextBox>
                </div>
                <div runat="server" id="div_POWO1">
                    <div class="col-md-2">
                        PO/WO Issued
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue="-Select-" ControlToValidate="ddlPOWO" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlPOWO" AutoPostBack="true" OnSelectedIndexChanged="ddlPOWO_SelectedIndexChanged" class="chosen-select form-control" TabIndex="2"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <div runat="server" id="div_POWO2">
                <div class="row">
                    <div class="col-md-2">Nature Of Work Executed</div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtNatureOfWork" Enabled="false" autocomplete="off" CssClass="form-control" TabIndex="10"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Nature Of Material Received</div>
                    <div class="col-md-4">
                        <asp:TextBox runat="server" ID="txtNatureOfMaterial" Enabled="false" autocomplete="off" CssClass="form-control" TabIndex="10"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    Ledger &nbsp;
                    <asp:ImageButton ID="ImgBtnLedger" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlLedger" CssClass="form-control" Enabled="false" TabIndex="2"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Work Done For / Item Supplied to
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtWorkDoneFor" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtWorkDoneFor" TextMode="MultiLine" Rows="2" CssClass="form-control" TabIndex="5" autocomplete="off"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    Month of Service/Items Supplied&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtService_Item_Supplied_Month" autocomplete="off" CssClass="form-control" TabIndex="10"></asp:TextBox>
                </div>
                <div id="Supplied_From_ToDateDiv" runat="server">
                    <div class="col-md-2">Service/Items Supplied Period &nbsp; </div>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtService_Items_Date_From" OnTextChanged="CheckIndentSuppliedPeriod_Changed" AutoPostBack="True" autocomplete="off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="2"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtService_Items_Date_From"></ajaxToolkit:CalendarExtender>
                    </div>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" ID="txtService_Items_Date_TO" autocomplete="off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="2"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtService_Items_Date_TO"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4"><b>BANK DETAILS :</b></div>
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <a runat="server" id="btnPrintPOWO" visible="false" class="btn btn-default">PO/WO Print Page</a>
                    <%--<asp:LinkButton runat="server" ID="lnkPrintPOWO" Text=""></asp:LinkButton>--%>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">Bank</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtBank" autocomplete="off" CssClass="form-control" TabIndex="10"></asp:TextBox>
                </div>
                <div class="col-md-2">Branch</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtBranch" autocomplete="off" CssClass="form-control" TabIndex="10"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">Account No.</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtAccNo" autocomplete="off" CssClass="form-control" TabIndex="10"></asp:TextBox>
                </div>
                <div class="col-md-2">IFSC Code</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtIFSC" autocomplete="off" CssClass="form-control" TabIndex="10"></asp:TextBox>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-2">
                    Payment Category &nbsp;
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlPaymentCategory" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="ImageButton_PaymentCategory" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlPaymentCategory" CssClass="form-control" TabIndex="2"></asp:DropDownList>
                </div>
                <div class="col-md-2">Sub Contractor Name / Legal Name</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlPayment_to_SubContractorName" AutoPostBack="true" OnSelectedIndexChanged="ddlPayment_to_SubContractorName_SelectedIndexChanged" CssClass="form-control" TabIndex="3">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4"><b>PAYMENT REQUEST :</b></div>
            </div>

            <div class="row">
                <div class="col-md-4">Payment Type</div>
                <%--<asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtAmount" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlPaymentType" CssClass="form-control">
                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                        <asp:ListItem Value="Advance" Text="Advance"></asp:ListItem>
                        <asp:ListItem Value="Part Payment" Text="Part Payment"></asp:ListItem>
                        <asp:ListItem Value="Final" Text="Final"></asp:ListItem>
                        <asp:ListItem Value="Against Invoice" Text="Against Invoice"></asp:ListItem>
                        <asp:ListItem Value="Other" Text="Other"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">Invoice Date</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtInvoiceDate" CssClass="form-control" AutoPostBack="true" OnTextChanged="CalculateDate" autocomplete="off" runat="server" TabIndex="1"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MM-yyyy" TargetControlID="txtInvoiceDate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">Due By Number Of Day's</div>
                <%--<asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtAmount" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtNoOfDueDate" Enabled="false" autocomplete="off" Text="0" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">Payment Mode</div>
                <%--<asp:RequiredFieldValidator ID="rfv2" runat="server" ControlToValidate="txtAmount" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlPaymentMode" CssClass="form-control" Enabled="false">
                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                        <asp:ListItem Value="RTGS" Text="RTGS"></asp:ListItem>
                        <asp:ListItem Value="NEFT" Text="NEFT"></asp:ListItem>
                        <asp:ListItem Value="IMPS" Text="IMPS"></asp:ListItem>
                        <asp:ListItem Value="CHEQUE" Text="CHEQUE"></asp:ListItem>
                        <asp:ListItem Value="CASH" Text="CASH"></asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>

            <div class="row">
                <div class="col-md-3">
                    <label>Value Of Services/Materials Received</label>
                    <br />
                    <asp:TextBox runat="server" onkeyup="CalculateTDSAmout();CalculateGSTAmout()" ID="txtAmt_ServiceMaterial" CssClass="form-control input-pos-float" AutoComplete="Off"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>GST %</label>
                    <br />
                    <asp:TextBox runat="server" onkeyup="CalculateGSTAmout()" ID="txtGSTPercent" CssClass="form-control input-pos-float" AutoComplete="Off"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>GST Amount</label>
                    <br />
                    <asp:TextBox runat="server" ID="txtGSTAmount" CssClass="form-control input-pos-float" AutoComplete="Off"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>Total Services/Materials Cost</label>
                    <br />
                    <asp:TextBox runat="server" ID="txtBasicTotal" CssClass="form-control input-pos-float" AutoComplete="Off"></asp:TextBox>
                </div>
            </div>


            <div class="row">
                <div class="col-md-3">
                    <label>TDS %</label>
                    <asp:RequiredFieldValidator ID="rfv22" runat="server" ControlToValidate="ddlTDSPercent" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValPI" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:DropDownList runat="server" onchange="CalculateTDSAmout()" ID="ddlTDSPercent" CssClass="form-control">
                        <asp:ListItem Text="-Select-"></asp:ListItem>
                        <asp:ListItem Value="0" Text="0"></asp:ListItem>
                        <asp:ListItem Value="0.1" Text="0.1"></asp:ListItem>
                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <label>TDS Deducted Amount</label>
                    <asp:TextBox runat="server" ID="txtTDSDeductedAmt" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="col-md-3">
                    <label>Other Deductions</label>
                    <asp:TextBox runat="server" onkeyup="CalculateTDSAmout()" ID="txtOtherDeductions" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label>Total Payable</label>
                    <asp:TextBox runat="server" ID="txtTotalPayable" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">Earlier Payments Done</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" onkeyup="CalculateItemAmt()" ID="txtAmt_EarlierPayment" CssClass="form-control input-pos-float" AutoComplete="Off" TabIndex="14"></asp:TextBox>
                </div>
                <div class="col-md-4">Part Payment</div>
            </div>
            <div class="row">
                <div class="col-md-4">Part Payment Sought By Project InCharge</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtAmt_PartPayment" CssClass="form-control input-pos-float" AutoComplete="Off" TabIndex="14"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">Materials Issued From Company On Debitable Basis</div>
                <div class="col-md-4">
                    <asp:Button runat="server" ID="btnAddItem1" Text="Add Items" OnClick="btnAddItem1_Click" />
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">Withhold Amounts As Negotiated</div>
                <div class="col-md-4">
                    <asp:Button runat="server" ID="btnAddItem2" Text="Add Items" OnClick="btnAddItem2_Click" />
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    <br />
                    KYC Available
                </div>
                <div class="col-md-10">
                    <table style="width: 100%; text-align: left">
                        <tr>
                            <td style="width: 33%">GST Registration</td>
                            <td style="width: 33%">PAN Copy</td>
                            <td style="width: 33%">Bank Details</td>
                        </tr>
                        <tr>
                            <td>&nbsp; &nbsp; &nbsp;<asp:Label runat="server" ID="lblGSTRegd" Text="NA"></asp:Label></td>
                            <td>&nbsp; &nbsp; &nbsp;<asp:Label runat="server" ID="lblPANCopy" Text="NA"></asp:Label></td>
                            <td>&nbsp; &nbsp; &nbsp;<asp:Label runat="server" ID="lblBankDetails" Text="NA"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkDownloadFile3" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkDownloadFile4" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lnkDownloadFile5" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />

            <div class="row">
                <div runat="server" id="AproveAmountDiv" visible="false">
                    <div class="col-md-2">Aproved Amount</div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" CssClass="form-control input-pos-float" AutoComplete="Off" ID="txtAprovedAmount" TabIndex="14"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-2">Upload Bill</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlUploadBill" CssClass="form-control" TabIndex="3">
                        <asp:ListItem Value="-Select-" Selected="True" Text="-Select-"></asp:ListItem>
                        <asp:ListItem Value="Quotation" Text="Quotation"></asp:ListItem>
                        <asp:ListItem Value="Proforma Invoice" Text="Proforma Invoice"></asp:ListItem>
                        <asp:ListItem Value="Invoice" Text="Invoice"></asp:ListItem>
                        <asp:ListItem Value="Other Documents" Text="Other Documents"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuBill" AllowMultiple="true"></asp:FileUpload>
                    <%-- <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="*Required Field" ControlToValidate="fuBill">  </asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator ID="rev1" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuBill"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>


            </div>
            <div class="row">
                <div runat="server" id="div_AfterUpload1" visible="false">
                    <div class="col-md-2">Uploaded Bill</div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile1" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br />
                        <asp:HyperLink runat="server" ID="hyplDownloadFile1" ForeColor="#337ab7"></asp:HyperLink>
                    </div>
                </div>
                <div runat="server" id="RemarksDiv" visible="false">
                    <div class="col-md-2">Remarks </div>
                    <div class="col-lg-4">
                        <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" TextMode="MultiLine" AutoComplete="Off" TabIndex="14"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">Upload Site Images</div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuSiteImg" AllowMultiple="true"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev2" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuSiteImg"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                </div>

                <div runat="server" id="div_AfterUpload2" visible="false">
                    <div class="col-md-2">Uploaded Site Images</div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile2" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br />
                        <asp:HyperLink runat="server" ID="hyplDownloadFile2" ForeColor="#337ab7"></asp:HyperLink>
                    </div>
                </div>
            </div>

            <div class="row" runat="server" id="div_Approver" visible="false">
                <div class="col-md-2">Approver Name</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlApprover" CssClass="form-control" TabIndex="20"></asp:DropDownList>
                </div>
            </div>
            <br />

            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Send for Verification" ValidationGroup="ValPI" OnClick="btnSubmit_Click" CssClass="btn btn-default" TabIndex="15"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="16"></asp:Button>
                    <asp:LinkButton ID="lbtnPrint" runat="server" Text="Print" Visible="false" CssClass="btn btn-default" CausesValidation="false" OnClick="btnPrint_Click" OnClientClick="document.forms[0].target = '_blank';" TabIndex="19" />
                    <asp:Button ID="btnAprove" Enabled="false" runat="server" Text="Approve" OnClick="btnApprove_Click" CssClass="btn btn-default" TabIndex="15"></asp:Button>
                    <asp:Button ID="BtnHold" Enabled="false" runat="server" Text="On hold" OnClick="btnHold_Click" CssClass="btn btn-default" TabIndex="15"></asp:Button>
                    <asp:Button ID="btnModification" Enabled="false" runat="server" Text="Send for Modification" OnClick="btnModification_Click" CssClass="btn btn-default" TabIndex="26"></asp:Button>
                    <asp:Button ID="btnReject" Enabled="false" runat="server" Text="Reject" OnClick="btnReject_Click" CssClass="btn btn-default" TabIndex="27"></asp:Button>
                </div>
            </div>
            <br />
            <center>
            <ogrid:Grid runat="server" ID="Grid_Item" CallbackMode="false" PageSize="5"
                OnDeleteCommand="Grid_Item_DeleteCommand"
                AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowPaging="true">
                <ScrollingSettings ScrollWidth="70%" />
                <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />

                <Columns>
                    <ogrid:Column DataField="PayInd_Item_Id" HeaderText="PayInd_Item_Id" Visible="false"></ogrid:Column>
                    <ogrid:Column DataField="Item_Category" HeaderText="Item Category" Width="150px"></ogrid:Column>
                    <ogrid:Column DataField="Item_Name" HeaderText="Item Name" Width="250px" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="Item_Amount" HeaderText="Amount" Width="150px"></ogrid:Column>
                    <ogrid:Column HeaderText="Delete" AllowDelete="true"></ogrid:Column>
                </Columns>

                <%--<Templates>
                        <ogrid:GridTemplate ID="GridTemplate1" runat="server">
                            <Template>
                                <asp:LinkButton ID="lnkDownloadFile" runat="server" Text='<%#Container.DataItem["File_Name"].ToString() %>' OnClick="lnkDownloadFile_Click"  CssClass="gridCB" ForeColor="#337ab7"></asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>--%>
            </ogrid:Grid>
        </center>
            <br />
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
                            <asp:LinkButton ID="lnkDownloadFile" runat="server"  OnClientClick="document.forms[0].target = '_blank';" TabIndex="19"  Text='<%#Container.DataItem["File_Name"].ToString() %>' OnClick="lnkDownloadFile_Click" CssClass="gridCB" ForeColor="#337ab7"></asp:LinkButton>
                        </Template>
                    </ogrid:GridTemplate>
                </Templates>

            </ogrid:Grid>
        </center>
            <br />
            <center>
            <h5>Completed Payment Indent List</h5>
            <ogrid:Grid runat="server" ID="Grid_PaymentIndent_Completed" CallbackMode="false"
                AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowPaging="true">
                <ScrollingSettings ScrollWidth="90%" />

                <Columns>
                    <ogrid:Column DataField="PayInd_No" HeaderText="Payment Indent No."></ogrid:Column>
                     <ogrid:Column DataField="PONo" HeaderText="PO Number"></ogrid:Column>
                    <ogrid:Column DataField="Amt_Transferable" HeaderText="Transfer Amount"></ogrid:Column>
                    <ogrid:Column DataField="Service_Items_Date_From" HeaderText="From Date" DataFormatString="{0:d}"></ogrid:Column>
                    <ogrid:Column DataField="Service_Items_Date_TO" HeaderText="To Date" DataFormatString="{0:d}"></ogrid:Column>
                    <ogrid:Column DataField="Payment_Date" HeaderText="Payment Date" DataFormatString="{0:d}"></ogrid:Column>
                    <ogrid:Column DataField="Payment_Ref_No" HeaderText="Payment Reference No."></ogrid:Column>
                </Columns>
            </ogrid:Grid>
        </center>


            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>

    <asp:Button runat="server" ID="btnAddItem" Style="display: none"></asp:Button>
    <ajaxToolkit:ModalPopupExtender ID="ModalItem" runat="server" PopupControlID="PanelItem" TargetControlID="btnAddItem"
        CancelControlID="BtnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanelItem" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="BtnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>
                        <h5 id="myModalamt11">
                            <asp:Label ID="lblHeader" runat="server" Text="Add Item"></asp:Label></h5>
                    </center>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-2">
                            Item Name
                            <asp:RequiredFieldValidator ID="rfv10" ControlToValidate="txtItemName" CssClass="Validation_Text" ValidationGroup="ValItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control" AutoComplete="Off"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Amount
                            <asp:RequiredFieldValidator ID="rfv11" ControlToValidate="txtItemAmt" CssClass="Validation_Text" ValidationGroup="ValItem" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtItemAmt" runat="server" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveItem" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValItem" OnClick="btnSaveItem_Click" />
                        <asp:Button ID="btnCancelItem" runat="server" Text="Cancel" CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelItem_Click" />
                    </center>

                </div>
            </div>
        </div>

    </asp:Panel>


    <ajaxToolkit:ModalPopupExtender ID="ModelLedgerPopup" runat="server" PopupControlID="PanelLedger" TargetControlID="ImgBtnLedger"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanelLedger" runat="server" align="center" Style="display: none" DefaultButton="btnSaveLedger">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>
                        <h5 id="myModalLabelcrate">Ledger</h5>
                    </center>
                </div>
                <asp:UpdatePanel ID="uppi2" runat="server">
                    <ContentTemplate>


                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    Ledger Name&nbsp;
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtLedgerName" CssClass="Validation_Text" ValidationGroup="ValLedger"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtLedgerName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">

                                <center>
                                    <ogrid:Grid ID="Grid_Ledger" runat="server" CallbackMode="false" AllowPageSizeSelection="false" OnDeleteCommand="Grid_Ledger_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowSorting="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                        <Columns>
                                            <ogrid:Column DataField="Ledger_Name" HeaderText="Ledger Name" Wrap="true"></ogrid:Column>
                                            <ogrid:Column DataField="Ledger_ID" Visible="false"></ogrid:Column>
                                            <ogrid:Column HeaderText="Delete" AllowDelete="true"></ogrid:Column>
                                        </Columns>
                                    </ogrid:Grid>
                                </center>

                            </div>
                        </div>

                        <div class="modal-footer">
                            <center>
                                <asp:Button ID="btnSaveLedger" runat="server" Text="Save" OnClick="btnSaveLedger_Click" CssClass="btn btn-default" ValidationGroup="ValLedger" />
                                <asp:Button ID="btnCancelLedger" runat="server" Text="Cancel" OnClick="btnCancelLedger_Click" CssClass="btn btn-default" ValidationGroup="ValLedger" CausesValidation="false" />
                            </center>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCancelLedger" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnSaveLedger" EventName="Click" />--%>
                        <asp:PostBackTrigger ControlID="btnSaveLedger" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="ModalPopupPaymentCategory" runat="server" PopupControlID="Panel_PaymentCategory" TargetControlID="ImageButton_PaymentCategory"
        CancelControlID="btnClose_PaymentCategory" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="Panel_PaymentCategory" runat="server" align="center" Style="display: none" DefaultButton="btnSavePaymentCategory">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose_PaymentCategory" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>
                        <h5 id="myModalLabelcrate_PaymentCategory">Payment Category</h5>
                    </center>
                </div>
                <asp:UpdatePanel ID="UpdatePanel_PaymentCategory" runat="server">
                    <ContentTemplate>


                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    Payment Category  Name&nbsp;
                                   
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtPaymentCategory" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">

                                <center>
                                    <ogrid:Grid ID="Grid_PaymentCategory" runat="server" CallbackMode="false" AllowPageSizeSelection="false" OnDeleteCommand="Grid_PaymentCategory_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowSorting="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                        <%--<ScrollingSettings ScrollWidth="95%" />--%>
                                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                        <Columns>
                                            <ogrid:Column DataField="PaymentCategory_Name" HeaderText="Payment Category" Wrap="true"></ogrid:Column>
                                            <ogrid:Column DataField="PaymentCategory_ID" Visible="false"></ogrid:Column>
                                            <ogrid:Column HeaderText="Delete" AllowDelete="true"></ogrid:Column>
                                        </Columns>
                                    </ogrid:Grid>
                                </center>

                            </div>
                        </div>

                        <div class="modal-footer">
                            <center>
                                <asp:Button ID="btnSavePaymentCategory" runat="server" Text="Save" OnClick="btnSavePaymentCategory_Click" CssClass="btn btn-default" ValidationGroup="ValPaymentCategory" />
                                <asp:Button ID="btnCancelPaymentCategory" runat="server" Text="Cancel" OnClick="btnCancelPaymentCategory_Click" CssClass="btn btn-default" ValidationGroup="ValPaymentCategory" CausesValidation="false" />
                            </center>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCancelLedger" />
                        <%--<asp:AsyncPostBackTrigger ControlID="btnSaveLedger" EventName="Click" />--%>
                        <asp:PostBackTrigger ControlID="btnSaveLedger" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
    </asp:Panel>

</asp:Content>
