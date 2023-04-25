<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Inventory/Stock.aspx.cs" Inherits="Stock" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    
      <script type="text/javascript">
          $(document).ready(function () {

              $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
              $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
          });

    </script>
    
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Stock Details

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                 <div class="col-md-2">
                            Project Name
                    <asp:RequiredFieldValidator ID="RFVProject" runat="server" InitialValue="-Select-" ControlToValidate="ddlProjectName" CssClass="Validation_Text" ValidationGroup="SaveBudget" ErrorMessage="*"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="form-control"  AutoPostBack="true">
                                <asp:ListItem Text="-Select"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                <div class="col-md-2">Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDate" CssClass="Validation_Text" ValidationGroup="ValStock" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtDate" CssClass="form-control"  onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server" TabIndex="2"></asp:TextBox>
                     <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MM-yyyy" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                </div>

                </div>
            <div class="row">
                 <div class="col-md-2">Budget Sector&nbsp;
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue="-Select-" ControlToValidate="ddlBudgetSector" CssClass="Validation_Text" ValidationGroup="ValStock" ErrorMessage="*"></asp:RequiredFieldValidator>
                 </div>

                 <div class="col-md-4">
                    <asp:DropDownList ID="ddlBudgetSector" runat="server" OnSelectedIndexChanged="ddlBudgetSector_SelectedIndexChanged" AutoPostBack="true"  CssClass="form-control" >
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">Category&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="-Select-" ControlToValidate="ddlCategory" CssClass="Validation_Text" ValidationGroup="ValStock" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                     <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" TabIndex="1" >
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                </div>
             

           

            <div class="row">
                  

                <div class="col-md-2">Item Name &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="-Select-" ControlToValidate="ddlItemName" CssClass="Validation_Text" ValidationGroup="ValStock" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                     <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-md-2">Item Code</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtItemCode" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>

                </div>

               

            </div>
              <div class="row">
                   
                <div class="col-md-2">Vendor Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="-Select-" ControlToValidate="ddlVendorName" CssClass="Validation_Text" ValidationGroup="ValStock" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlVendorName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged" AutoPostBack="true" TabIndex="4" >
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>
                   <div class="col-md-2">Vendor Code</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtVendorCode" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>

                </div>
                

            </div>
              <div class="row">
                 
                <div class="col-md-2">Bill No</div>
                <div class="col-md-4">
                     <asp:TextBox ID="txtBillNo" CssClass="form-control" MaxLength="50" runat="server" TabIndex="5"></asp:TextBox>

                </div>
                  <div class="col-md-2">UOM&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="-Select-" ControlToValidate="ddlUOM" CssClass="Validation_Text" ValidationGroup="ValStock" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                   <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control"  Enabled="false">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                
                </div>
               

            </div>
            <div class="row">
                 <div class="col-md-2">Rate</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtRate" CssClass="form-control input-pos-float" onpaste="javascript:return false;" autocomplete="off" runat="server" TabIndex="6"></asp:TextBox>

                </div>

                <div class="col-md-2">Qty&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" CssClass="Validation_Text" ControlToValidate="txtQty" ValidationGroup="ValStock"  runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                  
                    <asp:TextBox ID="txtQty"  CssClass="form-control input-pos-float" autocomplete="off" onpaste="javascript:return false;"  runat="server" TabIndex="7"></asp:TextBox>

                </div>
                
               

            </div>
          
            <div class="row" >
                 
                <div class="col-md-2">Remarks</div>                
                <div class="col-md-10">
                    <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="250" runat="server" style="resize:none" TextMode="MultiLine" TabIndex="8"></asp:TextBox>
                </div>

            </div>

            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValStock" CssClass="btn btn-default" OnClick="btnSubmit_Click" TabIndex="9"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="10"></asp:Button>                   
                </div>

            </div>
        </div>
    </div>
</asp:Content>
