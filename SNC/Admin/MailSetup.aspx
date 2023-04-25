<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Admin/MailSetup.aspx.cs" Inherits="MailSetup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    


    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               Mail Configuration

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

             <div class="row">
                        <div class="col-md-2">SMTP Host
                   <asp:RequiredFieldValidator ID="rfvSMTPhost" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValMailSetup" ControlToValidate="txtSMTPhost"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4"><asp:TextBox ID="txtSMTPhost" runat="server" class="form-control" TabIndex="1"></asp:TextBox></div>
                         <div class="col-md-2">SMTP Port No
                         <asp:RequiredFieldValidator ID="rfvSMTPNo" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValMailSetup" ControlToValidate="txtSMTPNo" ></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtSMTPNo" Display="Dynamic" CssClass="Validation_Text" ErrorMessage="Number only" ValidationExpression="^[+]?\d+(\.\d+)?$"></asp:RegularExpressionValidator>

                         </div>
                        <div class="col-md-4"><asp:TextBox ID="txtSMTPNo" runat="server" class="form-control"  onkeypress="return allowOnlyNumber(event);" onPaste="javascript:return false" TabIndex="2"></asp:TextBox></div>
                      
                    </div>
             <div class="row">
                        <div class="col-md-2">Email ID
                       <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValMailSetup" ControlToValidate="txtEmailID" ></asp:RequiredFieldValidator>
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailID" CssClass="Validation_Text" 
                    ErrorMessage="Enter Valid Email"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                  Display="Dynamic">Enter Valid Email</asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-4"><asp:TextBox ID="txtEmailID" runat="server" class="form-control" TabIndex="3"></asp:TextBox></div>
                         <div class="col-md-2">Password
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="ValMailSetup" runat="server" 
                    CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="txtPassword" ></asp:RequiredFieldValidator>
    
                         </div>
                        <div class="col-md-4"><asp:TextBox ID="txtPassword" runat="server" class="form-control" TextMode="Password" TabIndex="4" ></asp:TextBox></div>
                      
                    </div>
             <div class="row">
                        <div class="col-md-2">SSL
                     <asp:RequiredFieldValidator ID="rf1" runat="server" CssClass="Validation_Text" ErrorMessage="*" ValidationGroup="ValMailSetup" ControlToValidate="rblSSL" ></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4"><asp:RadioButtonList ID="rblSSL" runat="server" RepeatDirection="Horizontal" CssClass="radiostyle" TabIndex="5">
            <asp:ListItem Value="Yes">Enabled&nbsp;</asp:ListItem>
            <asp:ListItem Value="No">Not Enabled</asp:ListItem>
            </asp:RadioButtonList></div>
                         
               

            </div>
            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Update" ValidationGroup="ValMailSetup" OnClick="btnSubmit_Click" CssClass="btn btn-default" TabIndex="6"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-default" TabIndex="7"></asp:Button>
                 
                </div>

            </div>
            <br />
            
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
    





</asp:Content>
