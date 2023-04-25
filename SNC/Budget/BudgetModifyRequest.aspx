<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Budget/BudgetModifyRequest.aspx.cs" Inherits="BudgetModifyRequest" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    



 <div class="panel panel-default">
       <div class="panel-heading" >
       
    <h3 class="panel-title" >
        <i class="glyphicon glyphicon-envelope" ></i>

        Budget Modification Request

    </h3>

</div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-2">Project Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProjectName" ValidationGroup="ValBudgetModify" InitialValue="-Select-" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlProjectName" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>

                </div>
                 <div class="col-md-2">Monthly Budget
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlMonthlyBudget" ValidationGroup="ValBudgetModify" InitialValue="-Select-" ErrorMessage="*" CssClass="Validation_Text"></asp:RequiredFieldValidator>

                 </div>
                <div class="col-md-4">
                  <asp:DropDownList ID="ddlMonthlyBudget" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlMonthlyBudget_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                      <asp:ListItem Text="-Select-"></asp:ListItem>
                  </asp:DropDownList>

                </div>
         
            </div>
            <div class="row">
               
                <div class="col-md-2">Requested By</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtRequestedBy" CssClass="form-control"  runat="server" TabIndex="10"></asp:TextBox>

                </div>
                 <div class="col-md-2">Approved By</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtApprovedBy" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="row">
               
                <div class="col-md-2">Reason</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtReason" TextMode="MultiLine" style="resize:none" CssClass="form-control" runat="server" TabIndex="5"></asp:TextBox>
                </div>
            </div>
           
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValBudgetModify" CssClass="btn btn-default" OnClick="btnSubmit_Click" TabIndex="6"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnCancel_Click" TabIndex="7"></asp:Button>

                </div>
                
            </div>


             <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
</asp:Content>
