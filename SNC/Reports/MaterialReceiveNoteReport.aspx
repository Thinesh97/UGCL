<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="MaterialReceiveNoteReport.aspx.cs" Inherits="MaterialReciveNoteReport" %>
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
                Material Receive Note Report
            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Report Type</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlreportType" CssClass="form-control" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlreportType_SelectedIndexChanged">
                        <asp:ListItem Value="PM" Text="Purchase MRN"></asp:ListItem>
                        <asp:ListItem Value="SM" Text="Sevice MRN"></asp:ListItem>
                        
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Vendor <asp:Label runat="server" ID="lblVendorOrSubCon" Text="Wise"></asp:Label>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlVendorWise" CssClass="form-control" TabIndex="2" AutoPostBack="true" OnSelectedIndexChanged="ddlVendorWise_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList runat="server" ID="ddlSubContractor" CssClass="form-control" TabIndex="2" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlSubContractor_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>

             <div class="row">
                                <div class="col-md-2">
                    Select <asp:Label runat="server" ID="Label2" Text="PO/WO"></asp:Label>
                </div>
                <div class="col-md-4">
                    
                    <asp:DropDownList runat="server" ID="ddlpowo" CssClass="form-control" TabIndex="2" ></asp:DropDownList>
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
                <div class="col-md-2">Select Project</div>
                <div class="col-md-4">
                    <asp:ListBox runat="server" ID="lstProject" SelectionMode="Multiple" CssClass="form-control" TabIndex="10" Rows="5"></asp:ListBox>
                </div>
                
            </div>
            <br />
            
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"  ValidationGroup="ValPO"  CssClass="btn btn-default" TabIndex="15" OnClick="btnSubmit_Click"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Reset"  CssClass="btn btn-default" TabIndex="16" OnClick="btnCancel_Click"></asp:Button>
                </div>
            </div>
            <br />
            <br />
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>

</asp:Content>
