<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="POWORegisterReport.aspx.cs" Inherits="POWORegisterReport" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });

        function ConfirmDelete() {
            if (confirm("Are you want to Delete this record?") == false) {
                return false;
            }
            return true;
        }

        function Ajaxfuncation() {
            var ajaxrequest = new XMLHttpRequest();
        }
    </script>

    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Purchase / Work Order Register
            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Order Type</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlOrderType" CssClass="form-control" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged">
                        <asp:ListItem Value="PO" Text="Purchase Order"></asp:ListItem>
                        <asp:ListItem Value="WO" Text="Work Order"></asp:ListItem>
                        <asp:ListItem Value="WOHire" Text="Hire Order"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Select <asp:Label runat="server" ID="lblVendorOrSubCon" Text="Vendor"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlVendor" CssClass="form-control" TabIndex="2"></asp:DropDownList>
                    <asp:DropDownList runat="server" ID="ddlSubContractor" CssClass="form-control" TabIndex="2" Visible="false"></asp:DropDownList>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">From Date</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtFromDate" autocomplete= "off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="7"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender3" Format="dd-MM-yyyy" TargetControlID="txtFromDate"></ajaxToolkit:CalendarExtender>
                </div>
                <div class="col-md-2">To Date</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtToDate" autocomplete= "off" onpaste="javascript:return false;" onkeypress="javascript:return false;" CssClass="form-control" TabIndex="7"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtToDate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">Value Range From</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtFromAmount" autocomplete= "off" CssClass="form-control input-pos-float" TabIndex="8"></asp:TextBox>
                </div>
                <div class="col-md-2">Value Range To</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtToAmount" MaxLength="10" autocomplete= "off" CssClass="form-control input-pos-float" TabIndex="9"></asp:TextBox>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2">Project</div>
                <div class="col-md-4">
                    <asp:ListBox runat="server" ID="lstProject" SelectionMode="Multiple" CssClass="form-control" TabIndex="10" Rows="5"></asp:ListBox>
                </div>
                <div class="col-md-2">Display Columns</div>
                <div class="col-md-4">
                    <asp:ListBox runat="server" ID="lstColumns" SelectionMode="Multiple" CssClass="form-control" TabIndex="11" Rows="6"></asp:ListBox>
                </div>
            </div>
            <br />
            
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Generate" OnClick="btnGenerate_Click" ValidationGroup="ValPO"  CssClass="btn btn-default" TabIndex="15"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn btn-default" TabIndex="16"></asp:Button>
                </div>
            </div>
            <br />
            <br />
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>

</asp:Content>
