<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Asset/AssetRegistration.aspx.cs" Inherits="AssetRegistration" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
      
    <script type="text/javascript">
        function beforedelete() {
            if (confirm("Are you sure want to delete this record?") == false) {
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
        
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });
        
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
                    $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });

                }
            });
        };
    </script>
    
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
     
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Asset Registration
            </h3>

        </div>
       
        <div class="panel-body">

            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-1">
                    Type&nbsp;&nbsp;
                    <asp:ImageButton ID="imgbtnAssetType" runat="server" OnClick="imgbtnAssetType_Click" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" /> &nbsp;&nbsp; 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="ddlType" InitialValue="-Select-"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" TabIndex="1">
                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                 <div class="col-md-1">
                        Category&nbsp;&nbsp;
                        <asp:ImageButton ID="imgBtnCategory" runat="server" OnClick="imgBtnCategory_Click" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" /> &nbsp;&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="ddlAssetCategory" InitialValue="-Select-"></asp:RequiredFieldValidator>
                 </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlAssetCategory" CssClass="form-control" runat="server" TabIndex="2">
                           <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                <div class="col-md-1">Owner&nbsp;&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="rblowner"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:RadioButtonList ID="rblowner" AutoPostBack="true"  OnSelectedIndexChanged="rblowner_SelectedIndexChanged" RepeatDirection="Horizontal" runat="server" TabIndex="3">
                        <asp:ListItem Text="UGCL&nbsp;&nbsp;" Value="UGCL" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Hired" Value="Hired"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-1">Asset Code&nbsp;
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="txtAssetCode"></asp:RequiredFieldValidator></div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAssetCode" CssClass="form-control" MaxLength="20" runat="server" TabIndex="4"></asp:TextBox>
                </div>
                <div class="col-md-1">Asset Name&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="txtAssetName"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtAssetName" CssClass="form-control" MaxLength="50"  runat="server" TabIndex="5"></asp:TextBox>
                </div>
                <div class="col-md-1">Registration No</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtRegNo" CssClass="form-control" MaxLength="15" runat="server" TabIndex="6"></asp:TextBox>
                </div>
            </div>
                
            <div class="row">
                <div class="col-md-1">Unit&nbsp;
                    <asp:RequiredFieldValidator ID="rdbinputV" runat="server" ControlToValidate="rdbinput" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" />
                </div>
                    <div class="col-md-3">
                        <asp:RadioButtonList ID="rdbinput" runat="server" RepeatDirection="Horizontal" TabIndex="7">
                            <asp:ListItem Text="Litre/Hour" Value="Litre/Hour" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Kms/Litre" Value="Kms/Litre"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                <div class="col-md-1">Standard Input 
                    <asp:RequiredFieldValidator ID="input" runat="server" ControlToValidate="txtStandardInputHour" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" />
                </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtStandardInputHour" Text="0.00" CssClass="form-control input-pos-float"  runat="server" TabIndex="8"></asp:TextBox>
                    </div>
               <div class="col-md-1">Current Project&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="ddlPresentProjectName" InitialValue="-Select-"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlPresentProjectName" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPresentProjectName_SelectedIndexChanged" TabIndex="9">
                            <asp:ListItem>-Select-</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>
            
            <div class="row">
                 <div class="col-md-1">UOM
                </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlUnit_r" runat="server" CssClass="form-control" TabIndex="10">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                    </div>
                   <div class="col-md-1">Standard Output 
                    <asp:RequiredFieldValidator runat="server" ID="output" ControlToValidate="txtStandardOutPutHour" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" />
                </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtStandardOutPutHour" CssClass="form-control input-pos-float" Text="0.00" runat="server" TabIndex="11"></asp:TextBox>
                    </div>
                    <div class="col-md-1">Location&nbsp;&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="ddlLocation" InitialValue="-Select-"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList ID="ddlLocation" CssClass="form-control" Enabled="false" runat="server" TabIndex="12">
                            <asp:ListItem>-Select-</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            
            <div class="row">
                <div class="col-md-1">Engine Number & Model &nbsp;
                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ControlToValidate="txtEngineNo" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" />--%>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtEngineNo" CssClass="form-control" runat="server" TabIndex="13"></asp:TextBox>
                </div>
                <div class="col-md-1">Insurance Valid Upto &nbsp;
                        <%--<asp:RequiredFieldValidator runat="server ID="RequiredFieldValidator13"" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="ddlLocation"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-3">
                    <asp:TextBox runat="server" ID="txtInsValidDate" onkeypress="javascript: return false;" onPaste="javascript: return false;" CssClass="form-control" TabIndex="14"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="cal2" Format="dd-MM-yyyy" TargetControlID="txtInsValidDate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>
              
            <asp:PlaceHolder ID="placeholderUGCL" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-1">Bill No</div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtBillNo" CssClass="form-control" MaxLength="50" runat="server" TabIndex="16"></asp:TextBox>
                    </div>
                    <div class="col-md-1">Bill Date</div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtBillDate" onkeypress="javascript: return false;" onPaste="javascript: return false;" CssClass="form-control" TabIndex="17"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MM-yyyy" TargetControlID="txtBillDate"></ajaxToolkit:CalendarExtender>
                    </div>
                    <div class="col-md-1">Vendor Name</div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="ddlVendorName" CssClass="form-control" TabIndex="18">
                            <asp:ListItem Text="-Select-" Value=""></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">Invoice Amount</div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtInvoiceAmt" MaxLength="8" CssClass="form-control input-pos-float" TabIndex="19"></asp:TextBox>
                    </div>
                    <div class="col-md-1">Make</div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtMake" CssClass="form-control" MaxLength="50" TabIndex="20"></asp:TextBox>
                    </div>
                </div>
            </asp:PlaceHolder>
                              
            <asp:PlaceHolder ID="placeholderHired" Visible="false" runat="server">
                <div class="row">
                    <div class="col-md-1">HSD Recoverable</div>
                    <div class="col-md-3">
                        <asp:RadioButtonList runat="server" ID="rd_HSD" RepeatDirection="Horizontal" TabIndex="16">
                            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No" Value="No"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="col-md-1">Running Hrs/Kms</div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtRunningHrsKms" CssClass="form-control input-pos-float" TabIndex="17"></asp:TextBox>
                    </div>
                    <div class="col-md-1">Unit</div>
                    <div class="col-md-3">
                        <asp:RadioButtonList ID="rd_Unit" RepeatDirection="Horizontal" runat="server" TabIndex="18">
                            <asp:ListItem Text="Kms" Value="Kms"></asp:ListItem>
                            <asp:ListItem Text="Hrs" Value="Hrs"></asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1">Avg(Per Unit)</div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtAvg"  MaxLength="50" CssClass="form-control input-pos-float" TabIndex="19"></asp:TextBox>
                    </div>
                    <div class="col-md-1">Contractor</div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="ddlContractor" CssClass="form-control" TabIndex="20">
                            <asp:ListItem>-Select-</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                        <div class="col-md-1">Hire Charges(Per Month)</div>
                        <div class="col-md-3">
                            <asp:TextBox runat="server" ID="txtHireCharges" MaxLength="8" CssClass="form-control input-pos-float" TabIndex="21"></asp:TextBox>
                        </div>
                    </div>
                <div class="row">
                    <div class="col-md-1">Asset Received Date</div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" ID="txtAssetRecievedDate" CssClass="form-control" onkeypress="javascript:return false;" onpaste="javascript:return false;" TabIndex="22"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalenderforRedate" TargetControlID="txtAssetRecievedDate" Format="dd-MM-yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                    </div>
                </div>
            </asp:PlaceHolder>

            <div class="row">
                <div class="col-md-1">Condition&nbsp;&nbsp;
                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="ddlCondition" InitialValue="0"></asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md-3">
                        <asp:DropDownList runat="server" ID="ddlCondition" CssClass="form-control" TabIndex="21">
                            <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Idle" Value="Idle"></asp:ListItem>
                            <asp:ListItem Text="Schedule Repair" Value="Schedule Repair"></asp:ListItem>
                            <asp:ListItem Text="Working" Value="Working"></asp:ListItem>
                            <asp:ListItem Text="Breakdown" Value="Breakdown"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                <div class="col-md-1">Remarks</div>
                <div class="col-md-7">
                    <asp:TextBox runat ="server" ID="txtRemarks" TabIndex="25" CssClass="form-control" MaxLength="250" Style="resize: none" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-2">
                    Upload RC Copy
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuRC"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev1" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuRC"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">  
                    </asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblRC" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="cv1" runat="server" ControlToValidate="fuRC"
                        ClientValidationFunction="setUploadButtonState('fuRC','lblRC');">
                    </asp:CustomValidator>
                </div>
                <div runat="server" id="div_AfterUpload1" visible="false">
                    <div class="col-md-2">
                        Uploaded RC Copy
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile1" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br /> <asp:HyperLink runat="server" ID="hyplDownloadFile1" ForeColor="#337ab7"></asp:HyperLink>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Upload FC Copy
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuFC"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev2" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuFC"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">    
                    </asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblFC" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="cv2" runat="server" ControlToValidate="fuFC"
                        ClientValidationFunction="setUploadButtonState('fuFC','lblFC');">
                    </asp:CustomValidator>
                </div>
                <div runat="server" id="div_AfterUpload2" visible="false">
                    <div class="col-md-2">
                        Uploaded FC Copy
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile2" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br /> <asp:HyperLink runat="server" ID="hyplDownloadFile2" ForeColor="#337ab7"></asp:HyperLink> 
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Upload Permit Doc
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuPermit"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev3" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuPermit"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">    
                    </asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblPermit" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="cv3" runat="server" ControlToValidate="fuPermit"
                        ClientValidationFunction="setUploadButtonState('fuPermit','lblPermit');">
                    </asp:CustomValidator>
                </div>
                <div runat="server" id="div_AfterUpload3" visible="false">
                    <div class="col-md-2">
                        Uploaded Permit Doc
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile3" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br /> <asp:HyperLink runat="server" ID="hyplDownloadFile3" ForeColor="#337ab7"></asp:HyperLink>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Upload Insurance Doc
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuInsurance"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev4" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuInsurance"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">    
                    </asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblInsurance" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="cv4" runat="server" ControlToValidate="fuInsurance"
                        ClientValidationFunction="setUploadButtonState('fuInsurance','lblInsurance');">
                    </asp:CustomValidator>
                </div>
                <div runat="server" id="div_AfterUpload4" visible="false">
                    <div class="col-md-2">
                        Uploaded Insurance Doc
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile4" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br /> <asp:HyperLink runat="server" ID="hyplDownloadFile4" ForeColor="#337ab7"></asp:HyperLink>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Upload Misc Doc1
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuMisc1"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev5" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuMisc1"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">    
                    </asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblMisc1" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="cv5" runat="server" ControlToValidate="fuMisc1"
                        ClientValidationFunction="setUploadButtonState('fuMisc1','lblMisc1');">
                    </asp:CustomValidator>
                </div>
                <div runat="server" id="div_AfterUpload5" visible="false">
                    <div class="col-md-2">
                        Uploaded Misc Doc1
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile5" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br /> <asp:HyperLink runat="server" ID="hyplDownloadFile5" ForeColor="#337ab7"></asp:HyperLink>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    Upload Misc Doc2
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuMisc2"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev6" runat="server"
                        ErrorMessage="Only PDF,JPG,JPEG files are allowed." ForeColor="Red" ControlToValidate="fuMisc2"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf|.PDF|.jpg|.jpeg|.JPG|.JPEG)$">    
                    </asp:RegularExpressionValidator>
                    <br />
                    <asp:Label runat="server" ID="lblMisc2" ForeColor="Red"></asp:Label>
                    <asp:CustomValidator ID="cv6" runat="server" ControlToValidate="fuMisc2"
                        ClientValidationFunction="setUploadButtonState('fuMisc2','lblMisc2');">
                    </asp:CustomValidator>
                </div>
                <div runat="server" id="div_AfterUpload6" visible="false">
                    <div class="col-md-2">
                        Uploaded Misc Doc2
                    </div>
                    <div class="col-md-4">
                        <asp:LinkButton runat="server" ID="lnkDownloadFile6" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                        <br /> <asp:HyperLink runat="server" ID="hyplDownloadFile6" ForeColor="#337ab7"></asp:HyperLink>
                    </div>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSaveAsset" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="ValAsset" TabIndex="26" OnClick="btnSaveAsset_Click"></asp:Button>
                    <asp:Button ID="btnCancelAsset" runat="server" Text="Cancel" CssClass="btn btn-default" TabIndex="27" OnClick="btnCancelAsset_Click"></asp:Button>
                </div>

            </div>
           
        </div>
    </div>
    
    <asp:UpdatePanel ID="updatepanelAssetRegistration" runat="server">
        <ContentTemplate>     
                    
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSaveAssetCategory" />
        </Triggers>
    </asp:UpdatePanel>

  

            <!-- ModalPopupExtender -->
    <ajaxToolkit:ModalPopupExtender ID="mpeAssetType" runat="server" PopupControlID="PanelAssetType" TargetControlID="imgbtnAssetType"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelAssetType" runat="server" align="center" style="display:none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h4 id="myModalLabel">Asset Type</h4></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Type&nbsp;
          <asp:RequiredFieldValidator ID="RFV_ct" runat="server" ErrorMessage="*" ControlToValidate="txttype" CssClass="Validation_Text" ValidationGroup="valct"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txttype" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <center>
                               <ogrid:Grid ID="Grid_AssetType" runat="server" OnDeleteCommand="Grid_AssetType_DeleteCommand" CallbackMode="false" AllowPageSizeSelection="false"  FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                               <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                    <ScrollingSettings ScrollWidth="95%" />
                                <Columns>
                                       <ogrid:Column DataField="Asset_Type_ID"  HeaderText="Type ID" Visible="false">
                                          <%-- <TemplateSettings TemplateId="AssetTypeTemplate" />--%>
                                       </ogrid:Column>
                                     <ogrid:Column HeaderText="Asset Type" DataField="Asset_Type" Wrap="true">
                                        
                                    </ogrid:Column>   
                                   <ogrid:Column AllowDelete="true" HeaderText="Delete"></ogrid:Column>
                                  
                                </Columns>
                                   <Templates>
                                        <ogrid:GridTemplate runat="server" ID="AssetTypeTemplate">
                                            <Template>
                                                <asp:LinkButton ID="lnkBtnCategoryID" CausesValidation="false"  Text='<%# Container.DataItem["Asset_Type_ID"] %>'
                                                    CssClass="gridCB" runat="server"></asp:LinkButton>
                                            </Template>
                                        </ogrid:GridTemplate>
                                   </Templates>
                            </ogrid:Grid>

                        </center>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
           <asp:Button ID="btnSaveType" runat="server" Text="Save" OnClick="btnSaveType_Click"  CssClass="btn btn-default" ValidationGroup="valct"  />
                        <asp:Button ID="btnCancelType" runat="server"  Text="Cancel"  CssClass="btn btn-default" ValidationGroup="valct" CausesValidation="false" OnClick="btnCancelType_Click"  />
                 </center>

                </div>
            </div>
        </div>
        
    </asp:Panel>
   
            <!-- ModalPopupExtender -->
    <ajaxToolkit:ModalPopupExtender ID="mpeAssetCategory" runat="server" PopupControlID="PanelAssetCategory" TargetControlID="imgBtnCategory"
        CancelControlID="btnCloseCategory" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelAssetCategory" runat="server" align="center" Style="display: none">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnCloseCategory" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center><h4 id="myModalLabel1">Asset Category</h4></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Type &nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" InitialValue="-Select-"  ControlToValidate="ddlAssetType" CssClass="Validation_Text" ValidationGroup="ValAssetCategory"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList ID="ddlAssetType" CssClass="form-control" runat="server" >
                                <asp:ListItem Text="-Select-"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Category Name &nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtCategoryName" CssClass="Validation_Text" ValidationGroup="ValAssetCategory"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtCategoryName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            Category Prefix &nbsp;
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtCategoryPrefix" CssClass="Validation_Text" ValidationGroup="ValAssetCategory"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txtCategoryPrefix" MaxLength="5" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <center>
                            
                               <ogrid:Grid ID="Grid_Category" runat="server" AllowAddingRecords="false"  AutoGenerateColumns="false" CallbackMode="true"   OnDeleteCommand="Grid_Category_DeleteCommand" AllowPageSizeSelection="true"  FolderStyle="../Gridstyles/grand_gray"  AllowFiltering="true" AllowPaging="true" PageSize="10">
                                <ScrollingSettings ScrollWidth="95%" />
                                   <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                <Columns>
                                       <%--<ogrid:Column DataField="Asset_cat_ID"  HeaderText="Category ID">
                                           <TemplateSettings TemplateId="CategoryTemplate" />
                                       </ogrid:Column>--%>
                                       <ogrid:Column HeaderText="Asset Category Id" DataField="Asset_cat_ID" Wrap="true" Visible="false">  </ogrid:Column>   
                                      <ogrid:Column HeaderText="Asset Type" DataField="Asset_Type" Wrap="true">  </ogrid:Column>   
                                     <ogrid:Column HeaderText="Category Name" DataField="Category_Name" Wrap="true"> </ogrid:Column>   
                                   
                                    <ogrid:Column DataField="Cat_prefix" HeaderText="Prefix"></ogrid:Column>
                                   
                                    <ogrid:Column AllowDelete="true" HeaderText="Delete"></ogrid:Column>
                                </Columns>
                                  
                            </ogrid:Grid>

                        </center>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
           <asp:Button ID="btnSaveAssetCategory" runat="server" Text="Save"  OnClick="btnSaveAssetCategory_Click"  CssClass="btn btn-default" ValidationGroup="ValAssetCategory"/>
                       
                   
                      
                 </center>
                
                </div>
            </div>
        </div>
        </asp:Panel>
            
        
    

</asp:Content>
                   
            
