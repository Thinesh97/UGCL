<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Master/VendorDetails.aspx.cs" Inherits="VendorDetails" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });
        $(function () {

            $('#<%=txtPANNo.ClientID%>').keyup(function () {
                var yourInput = $(this).val();
                re = /[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi;
                var isSplChar = re.test(yourInput);
                if (isSplChar) {
                    var no_spl_char = yourInput.replace(/[`~!@#$%^&*()_|+\-=?;:'",.<>\{\}\[\]\\\/]/gi, '');
                    $(this).val(no_spl_char);
                }
            });

        });
        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }
        function ConfirmDelete() {
            if (confirm("Are you want to Delete this record?") == false) {
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
    </script>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>


    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Vendor Details

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-1">
                    Vendor ID
           
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtVendorID" CssClass="form-control" runat="server" MaxLength="8" Enabled="false"></asp:TextBox>

                </div>
                <div class="col-md-1">
                    Vendor Name
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="txtVendorName"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtVendorName" CssClass="form-control" runat="server" MaxLength="100" TabIndex="1"></asp:TextBox>

                </div>
                <div class="col-md-1">
                    Registration No
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="txtRegistrationNo"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtRegistrationNo" CssClass="form-control" MaxLength="50" TabIndex="2"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <asp:UpdatePanel ID="UPDL_Pan" runat="server">
                    <ContentTemplate>
                        <div class="col-md-1">
                            Use PAN
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="rd_usepan"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:RadioButtonList ID="rd_usepan" runat="server" RepeatDirection="Vertical" AutoPostBack="true" OnSelectedIndexChanged="rd_usepan_SelectedIndexChanged" TabIndex="3">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No (TDS 20% on Gross Bill)" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-1">
                            PAN No
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="txtPANNo"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtPANNo" ReadOnly="true" AutoComplete="off" CssClass="form-control" runat="server" MaxLength="10" TabIndex="4"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-md-1">Service Tax No</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtServiceTax" CssClass="form-control input-pos-int" runat="server" MaxLength="15" TabIndex="5"></asp:TextBox>
                </div>

            </div>
            <div class="row">
                <div class="col-md-1">PF Registartion No</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPFRegistartion" CssClass="form-control" MaxLength="30" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">Labour License No</div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtLabourLicenceNo" MaxLength="30" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-md-1">Contact Person</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtContactPerson" CssClass="form-control" MaxLength="50" runat="server" TabIndex="6"></asp:TextBox>
                </div>
            </div>
            <div class="row">

                <div class="col-md-1">Contact No</div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtContactNo" CssClass="form-control" MaxLength="50" TabIndex="7"></asp:TextBox>
                </div>

                <div class="col-md-1">Email ID</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtEmailID" CssClass="form-control" runat="server" MaxLength="100" TabIndex="8"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                        ControlToValidate="txtEmailID" CssClass="Validation_Text" ValidationGroup="ValVendorInfo"
                        ErrorMessage="Enter Valid Email" Display="Dynamic"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.([com in org net])+$">Enter valid Email ID</asp:RegularExpressionValidator>
                </div>
                <div class="col-md-1">
                    Address Line 1
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="txtaddr"></asp:RequiredFieldValidator>
                </div>

                <div class="col-md-3">
                    <asp:TextBox ID="txtaddr" runat="server" class="form-control" MaxLength="100" TabIndex="9"></asp:TextBox>
                </div>

            </div>
            <div class="row">

                <div class="col-md-1">Landmark</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtLandnark" runat="server" class="form-control" MaxLength="100" TabIndex="10"></asp:TextBox>

                </div>
                <div class="col-md-1">
                    City
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="txtCity"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCity" runat="server" class="form-control" MaxLength="50" TabIndex="11"></asp:TextBox>

                </div>
                <div class="col-md-1">
                    Country
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="ImgBtnCountry" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlCountry" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" runat="server" TabIndex="12">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>

                </div>

            </div>

            <div class="row">

                <div class="col-md-1">
                    State
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="ImgBtnState" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server" TabIndex="13">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="col-md-1">Pin No</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPInNo" runat="server" MaxLength="6" class="form-control input-pos-int" TabIndex="14"></asp:TextBox>

                </div>
                <div class="col-md-1">
                    Bank Name1
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" InitialValue="-Select-" ControlToValidate="ddlBankName"></asp:RequiredFieldValidator>
                    <a href="#myModal" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlBankName" CssClass="form-control" runat="server" TabIndex="15">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>

                </div>


            </div>


            <div class="row">

                <div class="col-md-1">
                    Branch Name1
            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="txtBranchName"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtBranchName" CssClass="form-control" runat="server" MaxLength="100" TabIndex="16"></asp:TextBox>

                </div>
                <div class="col-md-1">
                    Account Type1
            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="rd_accountype"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-3">
                    <asp:RadioButtonList ID="rd_accountype" runat="server" RepeatDirection="Horizontal" TabIndex="17">
                        <asp:ListItem Text="SB" Value="SB"></asp:ListItem>
                        <asp:ListItem Text="CA" Value="CA"></asp:ListItem>
                    </asp:RadioButtonList>

                </div>
                <div class="col-md-1">
                    Account No1
            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="txtAccountNo"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAccountNo" CssClass="form-control" runat="server" MaxLength="50" TabIndex="18"></asp:TextBox>

                </div>

            </div>
            <div class="row">

                <div class="col-md-1">
                    IFSC Code1
            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValVendorInfo" ControlToValidate="txtIFSCCode"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIFSCCode" CssClass="form-control" runat="server" MaxLength="20" TabIndex="19"></asp:TextBox>

                </div>
                <div class="col-md-1">Nature of Work</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtNatureOfWork" CssClass="form-control" runat="server" MaxLength="50" TabIndex="20"></asp:TextBox>

                </div>
                <div class="col-md-1">
                    Bank Name2
        
                 
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlBankName2" CssClass="form-control" runat="server" TabIndex="21">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>

                </div>

            </div>

            <div class="row">

                <div class="col-md-1">
                    Branch Name2
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtBranchName2" CssClass="form-control" runat="server" MaxLength="100" TabIndex="22"></asp:TextBox>

                </div>
                <div class="col-md-1">
                    Account Type2
                </div>
                <div class="col-md-3">
                    <asp:RadioButtonList ID="rblAccountType2" runat="server" RepeatDirection="Horizontal" TabIndex="23">
                        <asp:ListItem Text="SB" Value="SB"></asp:ListItem>
                        <asp:ListItem Text="CA" Value="CA"></asp:ListItem>
                    </asp:RadioButtonList>

                </div>
                <div class="col-md-1">
                    Account No2
            

                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAccNo2" CssClass="form-control" runat="server" MaxLength="50" TabIndex="24"></asp:TextBox>

                </div>

            </div>

            <div class="row">

                <div class="col-md-1">
                    IFSC Code2
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIFSCCode2" CssClass="form-control" runat="server" MaxLength="20" TabIndex="25"></asp:TextBox>
                </div>
                <div class="col-md-1">Ref Person</div>
                <div class="col-md-3">
                    <asp:CheckBox ID="chkRef" runat="server" AutoPostBack="true" OnCheckedChanged="chkRef_CheckedChanged" TabIndex="27" />
                </div>
                <div id="refperson" class="row" runat="server" visible="false">

                    <div class="col-md-1">Person Name</div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtRefPerson" CssClass="form-control" runat="server" MaxLength="20" />
                    </div>

                </div>
                </div>
            <div class="row">
                <div class="col-md-1">Remarks</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server" onkeypress="LimtCharacters(this,500,'lbl_charlimit');" Style="resize: none" TextMode="MultiLine" Wrap="true" TabIndex="26"></asp:TextBox>
                    <label id="lbl_charlimit" style="background-color: #E2EEF1; color: Blue; font-weight: normal; font-size: xx-small; float: left">Character left:500</label>
                </div>
                <div class="col-md-1">Legal Name</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtLegalName" CssClass="form-control" runat="server" TabIndex="27"></asp:TextBox>
                </div>
                <div class="col-md-1">Is Asset</div>
                <div class="col-md-3">
                    <asp:CheckBox ID="chIsAsset" runat="server" TabIndex="28" />
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">
                    Upload GST Registration Certificate
                </div>
                <div class="col-md-4">
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
                <div runat="server" id="div_AfterUpload1" visible="false">
                    <div class="col-md-2">
                        Uploaded GST Registration Certificate
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile1" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br /> <asp:HyperLink runat="server" ID="hyplDownloadFile1" ForeColor="#337ab7"></asp:HyperLink>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Upload PAN Copy
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuPANCopy"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev2" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuPANCopy"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">    
                    </asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblPANCopy" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="cv2" runat="server" ControlToValidate="fuPANCopy"
                        ClientValidationFunction="setUploadButtonState('fuPANCopy','lblPANCopy');">
                    </asp:CustomValidator>
                </div>
                <div runat="server" id="div_AfterUpload2" visible="false">
                    <div class="col-md-2">
                        Uploaded PAN Copy
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile2" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br /> <asp:HyperLink runat="server" ID="hyplDownloadFile2" ForeColor="#337ab7"></asp:HyperLink> 
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Upload Bank Details
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuBankDetails"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev3" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuBankDetails"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">    
                    </asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblBankDetails" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="cv3" runat="server" ControlToValidate="fuBankDetails"
                        ClientValidationFunction="setUploadButtonState('fuBankDetails','lblBankDetails');">
                    </asp:CustomValidator>
                </div>
                <div runat="server" id="div_AfterUpload3" visible="false">
                    <div class="col-md-2">
                        Uploaded Bank Details
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile3" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br /> <asp:HyperLink runat="server" ID="hyplDownloadFile3" ForeColor="#337ab7"></asp:HyperLink>
                    </div>
                </div>
            </div>

            <div class="row">
                  
                <br />
                <div class="row">
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-default" ValidationGroup="ValVendorInfo" TabIndex="29"></asp:Button>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" CausesValidation="false" TabIndex="30"></asp:Button>
                        <asp:Button ID="Button1" Text="Add Sub Vendor" CssClass="btn btn-default" runat="server" OnClick="btnAddVendorBank_Click" CausesValidation="false" TabIndex="22" />

                    </div>

                </div>
                <br />
                <br />
                 <center>
                     <h4>Sub Vendor Details</h4>
                <ogrid:Grid runat="server" ID="Grid_VendorBankDetails"  OnDeleteCommand="Grid_SubVendorDetails_DeleteCommand" 
                     AutoGenerateColumns="false"   
                    CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true">
                    <ScrollingSettings ScrollWidth="100%" />
                       <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
                    <Columns>
                       
                        <ogrid:Column DataField="Vendor_Name" HeaderText="Sub Vendor name" Align="center" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Address" HeaderText="Address" Align="center" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Bank_Name" HeaderText="Bank name" Align="center"></ogrid:Column> 
                        <ogrid:Column DataField="Account_Number" HeaderText="Account Number" Align="center"></ogrid:Column>    
                        <ogrid:Column DataField="Branch" HeaderText="Branch" Align="center"></ogrid:Column>
                        <ogrid:Column DataField="IFC_code" HeaderText="IFC code"  Align="center"></ogrid:Column>
                         <ogrid:Column HeaderText="Delete" AllowDelete="true" Align="center" ></ogrid:Column> 
                        <ogrid:Column DataField="ID"  HeaderText="ID"  Visible="false" ></ogrid:Column> 
                        <ogrid:Column DataField="Vendor_ID"  HeaderText="Vendor_ID"  Visible="false" ></ogrid:Column> 
                    </Columns>

                </ogrid:Grid>
                </center>
            </div>

                <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
            </div>
    </div>
       
    <ajaxToolkit:ModalPopupExtender ID="mpeDesig" runat="server" PopupControlID="PanelDesignation" TargetControlID="ImageButton2"
        CancelControlID="btnCloseDesignation" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelDesignation" runat="server" align="center">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseDesignation" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="myModalLabel">Bank</h5></center>
                </div>
                <div class="modal-body">


                    <div class="row">
                        <div class="col-md-2">
                            Bank Name&nbsp;
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox ID="txtbankname" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <center>
            <asp:Button ID="btnSaveBank" runat="server" Text="Save"   CssClass="btn btn-default" ValidationGroup="ValDesignation" OnClick="btnSaveBank_Click"/>
                          
                        <asp:Button ID="btnCancelBank" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValDesignation" CausesValidation="false" />
                    </center>

                </div>
            </div>
        </div>

</asp:Panel>
    
    <ajaxToolkit:ModalPopupExtender ID="ModelCountryPopup" runat="server" PopupControlID="PanelCountry" TargetControlID="ImgBtnCountry"
        CancelControlID="btnCloseCountry" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelCountry" runat="server" align="center" Style="display: none">

        <div class="modal-dialog" style="width: 40%">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseCountry" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="myModalLabelCountry">Add Country</h5></center>
                </div>
                <asp:UpdatePanel ID="upCountry" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">



                            <div class="row">



                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    Country Name&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtCountryName" CssClass="Validation_Text" ValidationGroup="ValCountry"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtCountryName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">

                                <center>
                               <ogrid:Grid ID="Grid_Country"  runat="server"  CallbackMode="false" AllowPageSizeSelection="false" OnDeleteCommand="Grid_Country_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                <ScrollingSettings ScrollWidth="95%" />
                                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                       
                                     <ogrid:Column  DataField="Country" HeaderText="Country Name" Wrap="true"></ogrid:Column> 
                                    
                                     <ogrid:Column AllowDelete="true" HeaderText="Delete"></ogrid:Column>
                                </Columns>
                                 
                            </ogrid:Grid>
                              </center>


                            </div>
                        </div>

                        <div class="modal-footer">
                            <center>
           <asp:Button ID="btnSaveCountry" runat="server" Text="Save"  OnClick="btnSaveCountry_Click"    CssClass="btn btn-default" ValidationGroup="ValCountry"  />
                        <asp:Button ID="btnCancelCountry" runat="server"  OnClick="btnCancelCountry_Click" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValCountry" CausesValidation="false"  />
                 </center>

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveCountry" />    
                    </Triggers>
                </asp:UpdatePanel>

            </div>

        </div>
    </asp:Panel>

    <ajaxToolkit:ModalPopupExtender ID="mpeState" runat="server" PopupControlID="PanelState" TargetControlID="ImgBtnState"
        CancelControlID="btnCloseState" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelState" runat="server" align="center" Style="display: none">

        <div class="modal-dialog" style="width: 40%;">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseState" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>  <h5 id="myModalLabelState">Add State</h5></center>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    Country Name&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" ControlToValidate="ddlCountryName" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValState"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-5">
                                    <asp:DropDownList ID="ddlCountryName" CssClass="form-control" runat="server">
                                        <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    State Name&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" ControlToValidate="txtStateName" CssClass="Validation_Text" ValidationGroup="ValState"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtStateName" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">

                                <center>
                               <ogrid:Grid ID="Grid_State"  runat="server"  CallbackMode="false" AllowPageSizeSelection="false" OnDeleteCommand="Grid_State_DeleteCommand" FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="5">
                                <ScrollingSettings ScrollWidth="95%" />
                                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                       
                                     <ogrid:Column  DataField="Country" HeaderText="Country Name" Wrap="true"></ogrid:Column> 
                                    <ogrid:Column  DataField="State" HeaderText="State Name" Wrap="true"></ogrid:Column> 
                                     <ogrid:Column AllowDelete="true" HeaderText="Delete"></ogrid:Column>
                                </Columns>
                                 
                            </ogrid:Grid>
                              </center>


                            </div>
                        </div>

                        <div class="modal-footer">
                            <center>
           <asp:Button ID="btnSaveState" runat="server" Text="Save" OnClick="btnSaveState_Click"  CssClass="btn btn-default" ValidationGroup="ValState"  />
                        <asp:Button ID="btnStateCancel" runat="server" Text="Cancel" OnClick="btnStateCancel_Click"  CssClass="btn btn-default" ValidationGroup="ValState" CausesValidation="false"  />
                                

                 </center>

                        </div>
                       
                    </ContentTemplate>
                    <Triggers>

                        <asp:PostBackTrigger ControlID="btnSaveState" />

                    </Triggers>
                </asp:UpdatePanel>
                  
            </div>

        </div>
    </asp:Panel>
       <asp:Button ID="btnDummy" runat="server" Style="display: none" Text="Button" />

    <ajaxToolkit:ModalPopupExtender ID="ModelItemPopup" runat="server" PopupControlID="PanelItem" TargetControlID="btnDummy"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelItem" runat="server" align="center" Style="display: none">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>   <h5 id="myModalLabelcrate"><asp:Label ID="lblAddItems" runat="server" Text="Sub Vendor Details"></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                  
               
                    <div class="row">
                        <div class="col-md-2">
                            Sub Vendor Name&nbsp;
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtSubVendorName" CssClass="Validation_Text" ValidationGroup="ValSubVendorBankDetails" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtSubVendorName" autocomplete="off"  CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Address&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtAddress" CssClass="Validation_Text" ValidationGroup="ValSubVendorBankDetails" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtAddress"  autocomplete="off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Bank Name&nbsp;
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtVendorBankName" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtVendorBankName" autocomplete="off"  CssClass="form-control" ></asp:TextBox>
                        
                        </div>
                        <div class="col-md-2">
                            Account Number&nbsp;
                        </div>
                        <div class="col-md-4">
                             <asp:TextBox runat="server" ID="txtAccountNumber" autocomplete="off"  CssClass="form-control" ></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Branch&nbsp;
                        </div>
                        <div class="col-md-4">
                             <asp:TextBox runat="server" ID="txtBranch" autocomplete="off"  CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            IFC code
                        </div>
                        <div class="col-md-4">
                             <asp:TextBox runat="server" ID="txtIFCCode" autocomplete="off"  CssClass="form-control"    ></asp:TextBox>
                        </div>
                    </div>
                  
                    
                </div>
                <div class="modal-footer">
                    <center>
                        <asp:Button ID="btnSaveVendorBankDetails" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValAddItem" OnClick="btnSaveSubVendorDetail_Click"    />
                        <asp:Button ID="btnCancelVendorBankDetail" runat="server"  Text="Cancel"   CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelSubVendorDetail_Click" />
                 </center>

                </div>
            </div>

        </div>

    </asp:Panel>

    
</asp:Content>

