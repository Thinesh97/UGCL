<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Master/SubContractor.aspx.cs" Inherits="SubContractor" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .chkbox {
            width: 40px;
            text-align: center;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        </style>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
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
                Contractor Details
            </h3>
        </div>
        
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-1">
                    Contractor ID
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtContractorID" CssClass="form-control" runat="server" Enabled="false" MaxLength="10"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    Contractor Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="txtContractorName"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtContractorName" CssClass="form-control" runat="server" MaxLength="100" TabIndex="1"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    Legal Name&nbsp;
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtLegalName" CssClass="form-control" runat="server" MaxLength="100" TabIndex="2"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>--%>
                        <div class="col-md-1">
                            Contractor Type&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="rd_usepan"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:RadioButtonList ID="rblContractorType" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="rblContractorType_SelectedIndexChanged" TabIndex="3">
                                <asp:ListItem Value="1" Text="Register" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Unregister"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-1">
                            Registration No&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="txtRegistrationNo"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtRegistrationNo" CssClass="form-control" MaxLength="50" runat="server" TabIndex="4"></asp:TextBox>
                        </div>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>

            </div>

            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="col-md-1">
                            Use PAN&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="rd_usepan"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:RadioButtonList ID="rd_usepan" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="rd_usepan_SelectedIndexChanged" TabIndex="5">
                                <asp:ListItem Value="1"> Yes</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">No (TDS 20% on Gross Bill)</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-1">PAN No&nbsp;</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtPANNo" Enabled="false" AutoComplete="off" CssClass="form-control" MaxLength="10" runat="server" TabIndex="6"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-md-1">Service Tax No</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtServiceTax" CssClass="form-control input-pos-int" runat="server" MaxLength="15" TabIndex="7"></asp:TextBox>
                &nbsp;
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">PF Registartion No</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPFRegistartion" CssClass="form-control" MaxLength="30" runat="server" TabIndex="10"></asp:TextBox>
                </div>
                <div class="col-md-1">Labour License No</div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtLabourLicenceNo" MaxLength="30" CssClass="form-control" TabIndex="11"></asp:TextBox>
                </div>
                <div class="col-md-1">Contact Person</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtContactPerson" CssClass="form-control" MaxLength="50" runat="server" TabIndex="12"></asp:TextBox>
                </div>
                </div>
            <div class="row">
                <div class="col-md-1">Contact No</div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtContactNo" MaxLength="50" CssClass="form-control" TabIndex="13"></asp:TextBox>
                </div>
                <div class="col-md-1">Email ID</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtEmailID" CssClass="form-control" MaxLength="100" runat="server" TabIndex="14"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server"
                        ControlToValidate="txtEmailID" CssClass="Validation_Text"
                        ErrorMessage="Enter Valid Email" Display="Dynamic"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.([com in org net])+$">Enter valid Email ID</asp:RegularExpressionValidator>
                </div>
                <div class="col-md-1">
                    Address Line 1
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="txtaddr"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtaddr" MaxLength="100" runat="server" class="form-control" TabIndex="15"></asp:TextBox>
                </div>
            </div>
            <div class="row">
      
                <div class="col-md-1">Landmark</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtLandmark" runat="server" MaxLength="100" class="form-control" TabIndex="16"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    City
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" MaxLength="50" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtCity" runat="server" class="form-control" TabIndex="17"></asp:TextBox>
                </div>
                 <div class="col-md-1">
                    Country
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="ddlCountry"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="ImgBtnCountry" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlCountry" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" runat="server" TabIndex="18">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row">
            <div class="col-md-1">
                    State
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="ddlState"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="ImgBtnState" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server" TabIndex="19"></asp:DropDownList>
                </div>
                <div class="col-md-1">Pin No</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtPinNo" MaxLength="6" runat="server" class="form-control input-pos-int" TabIndex="20"></asp:TextBox>
                </div>
                <div class="col-md-1">Nature of Work</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtNatureOfWork" CssClass="form-control" runat="server" MaxLength="50" TabIndex="25"></asp:TextBox>
                </div>
            </div>

            <%--Bank1 Details--%>
            <div class="row">
                <div class="col-md-1">
                    Bank Name
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" InitialValue="-Select-" ControlToValidate="ddlBankName"></asp:RequiredFieldValidator>
                    <a href="#myModal" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlBankName" CssClass="form-control" runat="server" TabIndex="21">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    Branch Name
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="txtBranchName"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtBranchName" CssClass="form-control" MaxLength="50" runat="server" TabIndex="22"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    Account Type
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="rd_accountype"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:RadioButtonList ID="rd_accountype" runat="server" RepeatDirection="Horizontal" TabIndex="23">
                       <asp:ListItem Text="SB" Value="SB"></asp:ListItem>
                        <asp:ListItem Text="CA" Value="CA"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    Account No
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="txtAccountNo"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAccountNo" CssClass="form-control" MaxLength="50" runat="server" TabIndex="24"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    IFSC Code
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ContractotInfo" ControlToValidate="txtIFSCCode"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIFSCCode" CssClass="form-control" MaxLength="20" runat="server" TabIndex="26"></asp:TextBox>
                </div>
            </div>

            <%--Bank2 Details--%>
            <div class="row">
                <div class="col-md-1">
                    Bank Name2
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlBankName2" CssClass="form-control" runat="server" TabIndex="27">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-1">
                    Branch Name2
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtBranchName2" CssClass="form-control" runat="server" MaxLength="100" TabIndex="29"></asp:TextBox>
                </div>         
                <div class="col-md-1">
                    Account Type2
                </div>
                <div class="col-md-3">
                    <asp:RadioButtonList ID="rblAccountType2" runat="server" RepeatDirection="Horizontal" TabIndex="30">
                        <asp:ListItem Text="SB" Value="SB"></asp:ListItem>
                        <asp:ListItem Text="CA" Value="CA"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1">
                    Account No2
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAccNo2" CssClass="form-control" runat="server" MaxLength="50" TabIndex="31"></asp:TextBox>
                </div>   
                <div class="col-md-1">
                    IFSC Code2 
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIFSCCode2" CssClass="form-control" runat="server" MaxLength="20" TabIndex="32"></asp:TextBox>
                </div>          
                
                </div>
            
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate> 
                        <div class="col-md-1">Ref Person</div>
                        <div class="col-md-3">
                            <asp:CheckBox ID="chkRef" runat="server" AutoPostBack="true" OnCheckedChanged="chkRef_CheckedChanged" TabIndex="34"/>
                        </div>
                        <div class="col-md-1">Person Name</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtRefPerson" CssClass="form-control" Enabled="false" runat="server" MaxLength="20" TabIndex="35"/>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="col-md-1">Remarks</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtRemarks" CssClass="form-control" Style="resize: none" runat="server" onkeypress="LimtCharacters(this,500,'lbl_charlimit');" TextMode="MultiLine" TabIndex="33"></asp:TextBox>
                    <label id="lbl_charlimit" style="background-color: #E2EEF1; color: Blue; font-weight: normal; font-size: xx-small; float: left">Character left:500</label>
                </div>
            </div>

            <div class="row" runat="server" id="div_GSTUpload">
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
            <div class="row" runat="server" id="div_PANUpload">
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
            <div class="row" runat="server" id="div_BankUpload">
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
            <div class="row" runat="server" id="div_IDProofUpload" visible="false">
                <div class="col-md-2">
                    Upload ID Proof
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuIDProof"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev4" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuIDProof"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">    
                    </asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblIDProof" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="cv4" runat="server" ControlToValidate="fuIDProof"
                        ClientValidationFunction="setUploadButtonState('fuIDProof','lblIDProof');">
                    </asp:CustomValidator>
                </div>
                <div runat="server" id="div_AfterUpload4" visible="false">
                    <div class="col-md-2">
                        Uploaded ID Proof
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile4" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br /> <asp:HyperLink runat="server" ID="HyperLink1" ForeColor="#337ab7"></asp:HyperLink>
                    </div>
                </div>
            </div>

        <br />
        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ContractotInfo" CssClass="btn btn-default" OnClick="btnSubmit_Click" TabIndex="27"></asp:Button>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="28"></asp:Button>
                 <asp:Button ID="btnSub_Contractor" Text="Add Sub Contractor" CssClass="btn btn-default" runat="server" OnClick="btnAddSub_Contractor_Click" CausesValidation="false" TabIndex="22" />
            </div>
            <br />
            <br />
                <center>
                       <h4>Sub Contractor Details</h4>
                <ogrid:Grid runat="server" ID="Grid_SubContractorDetails"  OnDeleteCommand="Grid_SubContractorDetails_DeleteCommand" 
                      AutoGenerateColumns="false"   
                    CallbackMode="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true">
                    <ScrollingSettings ScrollWidth="100%" />
                       <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ClientSideEvents OnBeforeClientDelete="ConfirmDelete" />
                    <Columns>
                       
                        <ogrid:Column DataField="SubContractor_Name" HeaderText="Sub Contractor name" Align="center" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="Address" HeaderText="Address" Align="center" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Bank_Name" HeaderText="Bank name" Align="center" Wrap="true"></ogrid:Column> 
                        <ogrid:Column DataField="Account_Number" HeaderText="Account Number" Align="center" Wrap="true"></ogrid:Column>    
                        <ogrid:Column DataField="Branch" HeaderText="Branch" Align="center" Wrap="true"></ogrid:Column>
                        <ogrid:Column DataField="IFC_code" HeaderText="IFC code" Align="center" Wrap="true"></ogrid:Column>
                         <ogrid:Column HeaderText="Delete" AllowDelete="true" Align="center" Wrap="true"></ogrid:Column> 
                        <ogrid:Column DataField="ID"  HeaderText="ID"  Visible="false" Align="center" Wrap="true"></ogrid:Column> 
                         <ogrid:Column DataField="Subcon_ID"  HeaderText="Subcon_ID"  Visible="false" Align="center" Wrap="true"></ogrid:Column> 
                    </Columns>

                </ogrid:Grid>
                </center>
        </div>
        <br />

        <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
    </div>

    </div>
    
    
    
    <!-------------------------------------------------------------popup------------------------------------------------------------------>

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
          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="txtCountryName" CssClass="Validation_Text" ValidationGroup="ValCountry"></asp:RequiredFieldValidator>
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
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-3">
                                    Country Name&nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="ddlCountryName" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValState"></asp:RequiredFieldValidator>
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
          <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*" ControlToValidate="txtStateName" CssClass="Validation_Text" ValidationGroup="ValState"></asp:RequiredFieldValidator>
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
                    <center>   <h5 id="myModalLabelcrate"><asp:Label ID="lblAddItems" runat="server" Text="Add Sub Contractor Details"></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                  
               
                    <div class="row">
                        <div class="col-md-2">
                            Sub Vendor Name&nbsp;
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtSubContractorName" CssClass="Validation_Text" ValidationGroup="ValSubVendorBankDetails" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtSubContractorName" autocomplete="off"  CssClass="form-control" ></asp:TextBox>
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
                        <asp:Button ID="btnSaveSubContractor_Bank_Details" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValAddItem" OnClick="btnSubContractor_Bank_Details_Click"    /> 
                        <asp:Button ID="btnCancelSubContractorBankDetail" runat="server"  Text="Cancel"   CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelSubContractorBankDetail_Click" />
                 </center>

                </div>
            </div>

        </div>

    </asp:Panel>
</asp:Content>

