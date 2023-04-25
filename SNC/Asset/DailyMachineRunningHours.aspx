<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="DailyMachineRunningHours.aspx.cs" Inherits="DailyMachineRunningHours" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Style/Time_Picker/css/timepicki.css" rel="stylesheet" />
    <script src="../Style/Time_Picker/js/timepicki.js"></script>
    <script type="text/javascript">
        function exportgrid() {
            ProjectList_Grid.exportToExcel();
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
                Daily Running Hours/Kms
            </h3>

        </div>

        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->
            <asp:UpdatePanel ID="updatepanelDailyRunningHours" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <div class="col-md-1">
                            Date
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtDate" onkeypress="javascript:return false;" onpaste="javascript:return false;"  CssClass="form-control" runat="server"></asp:TextBox></div>
                         <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" Format="dd-MM-yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                        <div class="col-md-1">
                            Type&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="ddlType" InitialValue="-Select-"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlType" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" TabIndex="2">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            Category&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAsset" ControlToValidate="ddlAssetCategory" InitialValue="-Select-"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAssetCategory" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetCategory_SelectedIndexChanged" TabIndex="3">
                                <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            Asset
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="valDMRH" ControlToValidate="ddlAssetName"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAssetName" CssClass="form-control" runat="server" AutoPostBack="True" TabIndex="4">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                         <div class="col-md-1">
                            Registration Code
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="valDMRH" ControlToValidate="ddlAssetName"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlAssetRegistration" CssClass="form-control" runat="server" AutoPostBack="True" TabIndex="4">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">Unit</div>
                        <div class="col-md-3" tabindex="5">
                            <asp:RadioButtonList ID="rbtUnit" RepeatDirection="Horizontal" runat="server" >
                                <asp:ListItem Selected="True" Text="Hours" Value="Litre/Hour"></asp:ListItem>
                                <asp:ListItem Text="Kms" Value="Kms/Litre"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                        </div>
                    </div>
                    <div class="row">
                        <div id="trKM" runat="server">
                            <div class="col-md-1">Start Km</div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtStartKm" CssClass="form-control input-pos-float" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-1">End Km</div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtEndKm" CssClass="form-control input-pos-float" runat="server"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtEndKm" CssClass="Validation_Text" ValidationGroup="valDMRH" ControlToCompare="txtStartKm" Operator="GreaterThanEqual" Type="Double" runat="server" ErrorMessage="End Km Should not be less than start Km"></asp:CompareValidator>
                            </div>
                        </div>
                        <div runat="server" id="trHrs">
                            <div class="col-md-1">Start Hour</div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtStartHour" TabIndex="6" runat="server" CssClass="form-control input-pos-float" Text="0.00"></asp:TextBox>
                            </div>
                            <div class="col-md-1">End Hour</div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtEndHour" TabIndex="7" runat="server" CssClass="form-control input-pos-float" Text="0.00"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtEndHour" CssClass="Validation_Text" ValidationGroup="valDMRH" ControlToCompare="txtStartHour" Operator="GreaterThanEqual" Type="Double" runat="server" ErrorMessage="End Hour Should not be less than start Hour"></asp:CompareValidator>
                            </div>
                        </div>
                        <div class="col-md-1">
                            UOM
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="valDMRH" ControlToValidate="ddlUOM"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlUOM" CssClass="form-control" runat="server" AutoPostBack="True" TabIndex="8">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">Output</div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtOutput" CssClass="form-control input-pos-float" Text="0.00" runat="server" TabIndex="9"></asp:TextBox>
                        </div>
                        <div class="col-md-1">
                            Issued Diesel (Liter)
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="valDMRH" ControlToValidate="txtIssuedDieselQty"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-3">
                            <asp:TextBox ID="txtIssuedDieselQty" runat="server" CssClass="form-control input-pos-float" TabIndex="10"></asp:TextBox>
                        </div>
                       
                    </div>
                    <div class="row">
                        <div class="col-md-1">Remarks</div>
                        <div class="col-md-11">
                            <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="250" Style="resize: none" runat="server" TextMode="MultiLine" TabIndex="11"></asp:TextBox>
                        </div>
                    </div>
                      <div class="row">
                <div class="col-md-2">
                   View Or Download Uploaded File
                </div>
                <div class="col-md-4">
                 <%--   <asp:ImageButton ID="" value=" " Visible="false"  runat="server" />--%>
                 <%--   <asp:Image ID="imgBtnUploadedFile" Width="300px" Height="180px"  Visible="false" BorderColor="White" runat="server" />--%>
                <asp:ImageButton ID="imgBtnUploadedFile"  Width="300px" Height="180px"  OnClick="imgBtnDigitalSign_Click" runat="server" />
                </div>
            </div>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" ValidationGroup="valDMRH" OnClick="btnSave_Click" TabIndex="12"></asp:Button>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="13"></asp:Button>
            </div>
        </div>

    </div>

</asp:Content>
