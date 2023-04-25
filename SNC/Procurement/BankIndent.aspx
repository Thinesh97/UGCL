<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Procurement/BankIndent.aspx.cs" Inherits="SNC.Procurement.BankIndent" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
            CalcTotalAmt();
        });

        function beforedelete() {

            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }

        function setUploadButtonState(id_fu, id_lbl) {
            var maxFileSize = 5242880; // 5MB -> 5 * 1024 * 1024
            var fileUpload = $("#ContentPlaceHolder1_" + id_fu);
            var lblmsg = $("#ContentPlaceHolder1_" + id_lbl);
            debugger;

            if (fileUpload[0].value == '') {
                return false;
            }
            else {
                if (fileUpload[0].files[0].size < maxFileSize) {
                    lblmsg.text('');
                    $("#ContentPlaceHolder1_btnSubmit").css('opacity', '1');
                    return true;
                } else {
                    lblmsg.text('Size should not be greater than 5mb.');
                    $("#ContentPlaceHolder1_btnSubmit").css('opacity', '0');
                    return false;
                }
            }
        }

        function CalculateItemTaxAmt() {

            var itemAmt = document.getElementById("<%=txtamount.ClientID%>").value;

            var igstPerc = document.getElementById("<%=txtIGSTperc.ClientID%>").value;
            var cgstPerc = document.getElementById("<%=txtCGSTperc.ClientID%>").value;
            var sgstPerc = document.getElementById("<%=txtSGSTperc.ClientID%>").value;

            var igstAmt = parseFloat(parseFloat(itemAmt) * parseFloat(igstPerc)) / 100;
            var cgstAmt = parseFloat(parseFloat(itemAmt) * parseFloat(cgstPerc)) / 100;
            var sgstAmt = parseFloat(parseFloat(itemAmt) * parseFloat(sgstPerc)) / 100;

            document.getElementById("<%=txtIGSTamt.ClientID%>").value = igstAmt.toFixed(2);
            document.getElementById("<%=txtCGSTamt.ClientID%>").value = cgstAmt.toFixed(2);
            document.getElementById("<%=txtSGSTamt.ClientID%>").value = sgstAmt.toFixed(2);
        }

        function CalcTotalAmt() {

            var itemAmt = document.getElementById("<%=txtamount.ClientID%>").value;

            var igstAmt = document.getElementById("<%=txtIGSTamt.ClientID%>").value;
            var cgstAmt = document.getElementById("<%=txtCGSTamt.ClientID%>").value;
            var sgstAmt = document.getElementById("<%=txtSGSTamt.ClientID%>").value;

            var totalGSTAmt = parseFloat(parseFloat(igstAmt) + parseFloat(cgstAmt) + parseFloat(sgstAmt));
            var afterGSTAmt = parseFloat(parseFloat(itemAmt) + parseFloat(totalGSTAmt));
            document.getElementById("<%=txttaxtotal.ClientID%>").value = totalGSTAmt.toFixed(2);
            document.getElementById("<%=txtamountAfterGST.ClientID%>").value = afterGSTAmt.toFixed(2);
        }

    </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Banking Indent

            </h3>
        </div>

        <div class="panel-body">
            <div class="row">
                <div class="col-md-2">Banking Indent No</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtBankIndNo" Enabled="false" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>

                <div class="col-md-2">
                    Date&nbsp;
                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtBankIndDate" CssClass="Validation_Text" ValidationGroup="ValBInd" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtBankIndDate" autocomplete="off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtBankIndDate"></ajaxToolkit:CalendarExtender>
                </div>

            </div>

            <div class="row">
                <div class="col-md-2">State</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlState" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" CssClass="form-control" TabIndex="3"></asp:DropDownList>
                </div>
                <div class="col-md-2">Project</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProject" runat="server" class="chosen-select form-control"></asp:DropDownList>
                </div>

            </div>

            <div class="row">
                <div class="col-md-2">Financial Year</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlFYear" CssClass="form-control" TabIndex="3">
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
                <div class="col-md-2">Select Bank</div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlbank" runat="server" class="chosen-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlbank_SelectedIndexChanged"></asp:DropDownList>
                </div>

            </div>

            <div class="row">
                <div class="col-md-2">Bank Name</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtbank" Enabled="false" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    Branch&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtbranch" Enabled="false" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">Account No</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtaccountno" Enabled="false" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>

                <div class="col-md-2">
                    ISFC Code&nbsp;
                    
                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtisfc" Enabled="false" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>

            </div>

            <div class="row">
                <div class="col-md-4">Payment Type</div>
                <div class="col-md-6" style="text-align: center">
                    <asp:RadioButtonList ID="rblpaytype" RepeatDirection="Horizontal" runat="server" TabIndex="12">
                        <asp:ListItem Text="Debit" Selected="True" Value="Debit"></asp:ListItem>
                        <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    Payment Category
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlpaymentcat" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValBInd" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <a href="#myModal" data-toggle="modal" role="button">
                        <asp:ImageButton ID="Imgpaymentcat" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlpaymentcat" CssClass="form-control" runat="server" TabIndex="2" OnSelectedIndexChanged="ddlpaymentcat_SelectedIndexChanged1" AutoPostBack="true" ></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    Payment Sub Category
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlpaymentsubcat" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValBInd" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <a href="#myModal" data-toggle="modal" role="button">
                        <asp:ImageButton ID="Imagepaysubcat" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>&nbsp;
                </div>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlpaymentsubcat" CssClass="form-control" runat="server" TabIndex="2" OnSelectedIndexChanged="ddlpaymentsubcat_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    Towards
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlpaymenttowards" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValBInd" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <a href="#myModal" data-toggle="modal" role="button">
                        <asp:ImageButton ID="imdbtnpaymenttowards" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                </div>
                <div class="col-md-6">
                    <asp:DropDownList ID="ddlpaymenttowards" CssClass="form-control" runat="server" TabIndex="2"></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    Refence Details&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtrefdetails" ssClass="Validation_Text" ValidationGroup="ValBInd" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtrefdetails" Enabled="true" CssClass="form-control" MaxLength="50"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    Narration&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtrefdetails" ssClass="Validation_Text" ValidationGroup="ValBInd" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtNarration" Enabled="true" CssClass="form-control" MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    Rate of Interest 
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtrateofinterest" Enabled="true" CssClass="form-control input-pos-float" AutoComplete="Off"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    Amount&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtrefdetails" ssClass="Validation_Text" ValidationGroup="ValBInd" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" onkeyup="CalculateItemTaxAmt(),CalcTotalAmt()" onPaste="javascript:return false"
                        ID="txtamount" Enabled="true" autocomplete="off" Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-1">
                    CGST 
                </div>
                <div class="col-md-1">
                    CGST %age:
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtCGSTperc" onkeyup="CalculateItemTaxAmt(),CalcTotalAmt()" Text="0.00" onPaste="javascript:return false" Enabled="true" CssClass="form-control input-pos-float" AutoComplete="Off" Width="50px"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    CGST Amt:
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtCGSTamt" Enabled="false" CssClass="form-control input-pos-float" AutoComplete="Off" Width="100px"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-1">
                    SGST 
                </div>
                <div class="col-md-1">
                    SGST %age:
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtSGSTperc" onkeyup="CalculateItemTaxAmt(),CalcTotalAmt()" Text="0.00" onPaste="javascript:return false" Enabled="true" CssClass="form-control input-pos-float" AutoComplete="Off" Width="50px"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    SGST Amt:
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtSGSTamt" Enabled="false" CssClass="form-control input-pos-float" AutoComplete="Off" Width="100px"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-1">
                    IGST 
                </div>
                <div class="col-md-1">
                    IGST %age:
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtIGSTperc" onkeyup="CalculateItemTaxAmt(),CalcTotalAmt()" Text="0.00" onPaste="javascript:return false" Enabled="true" CssClass="form-control input-pos-float" AutoComplete="Off" Width="50px"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    IGST Amt:
                </div>
                <div class="col-md-2">
                    <asp:TextBox runat="server" ID="txtIGSTamt" Enabled="false" CssClass="form-control input-pos-float" AutoComplete="Off" Width="100px"></asp:TextBox>
                </div>
            </div>

            <div class="row" style="display:none">
                <div class="col-md-4"></div>
                <div class="col-md-2">
                    Total Tax Deduction:
                </div>
                <div class="col-md-5" >
                    <asp:TextBox runat="server" ID="txttaxtotal" Width="100px" CssClass="form-control input-pos-float" AutoComplete="Off" Enabled="false"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    Total Amount 
                     &nbsp;
                    
                </div>
                <div class="col-md-6">
                    <asp:TextBox runat="server" ID="txtamountAfterGST" Enabled="true" CssClass="form-control input-pos-float" AutoComplete="Off"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    Upload Documents 
                    &nbsp;
                    
                </div>
                <div class="col-md-8">
                    <asp:FileUpload runat="server" ID="fuGSTRegd"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev1" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuGSTRegd"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblGSTRegd" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="cv1" runat="server" ControlToValidate="fuGSTRegd"
                        ClientValidationFunction="setUploadButtonState('fuGSTRegd','lblGSTRegd');">
                    </asp:CustomValidator>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValBInd" OnClick="btnSubmit_Click" CssClass="btn btn-default" TabIndex="15"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-default" OnClick="btnCancel_Click" TabIndex="16"></asp:Button>
                </div>
            </div>







            <ajaxToolkit:ModalPopupExtender ID="ModelPaymentPopup" runat="server" PopupControlID="PanelPayment" TargetControlID="Imgpaymentcat"
                CancelControlID="btnClose1" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelPayment" runat="server" align="center" Style="display: none">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" id="btnClose1" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-2">
                                    Payment Category
                            <asp:RequiredFieldValidator ID="RFV_ct" runat="server" ErrorMessage="*" ControlToValidate="txtpaymentcat" CssClass="Validation_Text" ValidationGroup="ValProjectType"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtpaymentcat" runat="server" MaxLength="50" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <center>
                                    <cc1:Grid ID="Grid_PaymentTerms" CallbackMode="true" AllowPageSizeSelection="false" runat="server" OnDeleteCommand="Grid_PaymentTerms_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="5">
                                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                        <Columns>

                                            <cc1:Column DataField="PAYMENT_TERMS" HeaderText="Payment Terms" Width="100px"></cc1:Column>
                                            <cc1:Column DataField="PAYMENT_ID" HeaderText="Edit" Width="100px">
                                                <TemplateSettings TemplateId="EditTemplatePaymentTerms" />
                                            </cc1:Column>
                                            <cc1:Column AllowDelete="true" HeaderText="Delete"></cc1:Column>
                                        </Columns>
                                        <Templates>
                                            <cc1:GridTemplate runat="server" ID="EditTemplatePaymentTerms">
                                                <Template>
                                                    <asp:LinkButton ID="lnkbtnpayterms" CausesValidation="false" CommandName='<%# Container.DataItem["PAYMENT_ID"] %>' OnClick="lnkbtnpayterms_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>

                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                    </cc1:Grid>
                                </center>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <center>

                                <asp:Button ID="btnsavepaymentcat" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValProjectType" CausesValidation="false" OnClick="btnsavepaymentcat_Click" />
                                <asp:Button ID="btnCancelpayment" runat="server" Text="Cancel" CssClass="btn btn-default" ValidationGroup="ValProjectType" CausesValidation="false" OnClick="btnCancelpayment_Click" />
                            </center>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <ajaxToolkit:ModalPopupExtender ID="ModalPaymentSubCatPopup" runat="server" PopupControlID="PanelPaymentSub" TargetControlID="Imagepaysubcat"
                CancelControlID="btnClose10" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelPaymentSub" runat="server" align="center" Style="display: none">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" id="btnClose10" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-2">
                                     Payment Category 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" ControlToValidate="ddlpaycat" CssClass="Validation_Text" ValidationGroup="ValProjectType"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList ID="ddlpaycat" runat="server" class="chosen-select form-control"   TabIndex="4"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-2">
                                     Payment Sub Category 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtpaymentsubcat" CssClass="Validation_Text" ValidationGroup="ValProjectType"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtpaymentsubcat" runat="server" MaxLength="50" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            

                            <div class="row">
                                <center>
                                    <cc1:Grid ID="Grid_PaymentSubCat" CallbackMode="true" AllowPageSizeSelection="false" runat="server" OnDeleteCommand="Grid_PaymentSubCat_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="5">
                                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                        <Columns>

                                            <cc1:Column DataField="PAYMENT_CATAGORY_SUB" HeaderText="Payment Sub Catagory" Width="100px"></cc1:Column>
                                            <cc1:Column DataField="PAYMENT_SUB_ID" HeaderText="Edit" Width="100px">
                                                <TemplateSettings TemplateId="EditTemplatePaymentTermsSub" />
                                            </cc1:Column>
                                            <cc1:Column AllowDelete="true" HeaderText="Delete"></cc1:Column>
                                        </Columns>
                                        <Templates>
                                            <cc1:GridTemplate runat="server" ID="EditTemplatePaymentTermsSub">
                                                <Template>
                                                    <asp:LinkButton ID="lnkbtnpaySubCatagory" CausesValidation="false" CommandName='<%# Container.DataItem["PAYMENT_SUB_ID"] %>' OnClick="lnkbtnpaySubCatagory_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>

                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                    </cc1:Grid>
                                </center>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <center>

                                <asp:Button ID="btnpaymentsubcat" runat="server" Text="Save" CausesValidation="false" CssClass="btn btn-default" ValidationGroup="ValProjectType" OnClick="btnpaymentsubcat_Click" />
                                <asp:Button ID="btnpaymentsubcatcancel" runat="server" Text="Cancel" CssClass="btn btn-default" ValidationGroup="ValProjectType" CausesValidation="false" OnClick="btnpaymentsubcatcancel_Click" />
                            </center>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <ajaxToolkit:ModalPopupExtender ID="ModalPaymentTowardsPopup" runat="server" PopupControlID="PanelPaymentTowards" TargetControlID="imdbtnpaymenttowards"
                CancelControlID="btnClose11" BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="PanelPaymentTowards" runat="server" align="center" Style="display: none">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" id="btnClose11" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-2">
                                     Payment Sub Category 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" ControlToValidate="ddlpaysubcat" CssClass="Validation_Text" ValidationGroup="ValProjectType"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList ID="ddlpaysubcat" runat="server" class="chosen-select form-control"   TabIndex="4"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-2">
                                    Towards 
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="txtpaymentTowards" CssClass="Validation_Text" ValidationGroup="ValProjectType"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtpaymentTowards" runat="server" MaxLength="50" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <center>
                                    <cc1:Grid ID="Grid_PyamentTowards" CallbackMode="true" AllowPageSizeSelection="false" runat="server" OnDeleteCommand="Grid_PyamentTowards_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="false" AllowPaging="true" PageSize="5">
                                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                        <Columns>

                                            <cc1:Column DataField="PAYMENT_CATAGORY_TOWARDS" HeaderText="Towards" Width="100px"></cc1:Column>
                                            <cc1:Column DataField="PAYMENT_TOWARDS_ID" HeaderText="Edit" Width="100px">
                                                <TemplateSettings TemplateId="EditTemplatePaymentTermsTowards" />
                                            </cc1:Column>
                                            <cc1:Column AllowDelete="true" HeaderText="Delete"></cc1:Column>
                                        </Columns>
                                        <Templates>
                                            <cc1:GridTemplate runat="server" ID="EditTemplatePaymentTermsTowards">
                                                <Template>
                                                    <asp:LinkButton ID="lnkbtnpaytowards" CausesValidation="false" CommandName='<%# Container.DataItem["PAYMENT_TOWARDS_ID"] %>' OnClick="lnkbtnpaytowards_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>

                                                </Template>
                                            </cc1:GridTemplate>
                                        </Templates>
                                    </cc1:Grid>
                                </center>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <center>

                                <asp:Button ID="btnsavetowards" runat="server" Text="Save" CausesValidation="false" CssClass="btn btn-default" ValidationGroup="ValProjectType" OnClick="btnsavetowards_Click" />
                                <asp:Button ID="btncanceltowards" runat="server" Text="Cancel" CssClass="btn btn-default" ValidationGroup="ValProjectType" CausesValidation="false" OnClick="btncanceltowards_Click" />
                            </center>
                        </div>
                    </div>
                </div>
            </asp:Panel>


        </div>
    </div>

</asp:Content>
