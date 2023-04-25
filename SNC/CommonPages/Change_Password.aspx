<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Authantication.Master" AutoEventWireup="true" CodeBehind="Change_Password.aspx.cs" Inherits="Change_Password" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .rounddiv1 {
            border-radius: 20px;
            width: 320px;
            min-height: 320px;
            box-shadow: 0px 3px 16px rgba(0, 0, 0, 0.35);
            padding: 5px;
        }

        .rounddiv2 {
            border-radius: 20px;
            width: 310px;
            min-height: 310px;
            box-shadow: 0px 3px 16px rgba(0, 0, 0, 0.35) inset;
            padding: 20px;
            _background: #9e121b;
        }

        .autheading {
            color: #9e121b;
            font-weight: bold;
            font-size: 30px;
            font-family: JasmineUPC;

        }
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
         <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <center>
        <asp:Label ID="Labelcheck" runat="server" Visible="false" CssClass="sucess"></asp:Label>
      
        <br /><br />
        <div class="rounddiv1">
        <div class="rounddiv2">

            <%--<img src="../Style/Images/SNC_logo.jpg" style="position:relative; _left:-15px; top:-40px; border-radius:10px; width:90%; _border:6px solid #9e121b;box-shadow: 0px 3px 16px rgba(0, 0, 0, 0.35);"/>--%>
           
            <h3>

               Change Password

                <i class="glyphicon glyphicon-lock" style="font-size:20px;">
                    
                </i>
            </h3>
         
             <div class="form-group input-group">

               <span class="input-group-addon">
                        <i class="glyphicon glyphicon-user"></i>
               </span>
                     <asp:TextBox id="txtuid" runat="server" CssClass="form-control" Enabled="false" placeholder="User ID"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rf1TB_Old" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="txtuid" ValidationGroup="Forget"></asp:RequiredFieldValidator>
        
            </div>

               <div class="form-group input-group">

               <span class="input-group-addon">
                        <i class="glyphicon glyphicon-lock"></i>
               </span>
                      <asp:TextBox ID="txtold" runat="server" CssClass="form-control" TextMode="Password" placeholder="Old Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="txtold" ValidationGroup="Forget"></asp:RequiredFieldValidator>
            </div>
             <div class="form-group input-group">

               <span class="input-group-addon">
                        <i class="glyphicon glyphicon-lock"></i>
               </span>
                      <asp:TextBox ID="txtnew" runat="server" CssClass="form-control" TextMode="Password" placeholder="New Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="txtnew" ValidationGroup="Forget"></asp:RequiredFieldValidator>
            </div>
             <div class="form-group input-group">

               <span class="input-group-addon">
                        <i class="glyphicon glyphicon-lock"></i>
               </span>
                      <asp:TextBox ID="txt_confirm" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm Password"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="Validation_Text" ErrorMessage="*" ControlToValidate="txt_confirm" ValidationGroup="Forget"></asp:RequiredFieldValidator>
            </div>
            <div class="modal-footer">
                <center>
                   <asp:Button runat="server" id="btn_ChangePassword" CssClass="btn btn-danger" Text="Change Password" OnClick="btn_ChangePassword_Click" ValidationGroup="Forget"/>
                <asp:Button runat="server" id="likBack" CssClass="btn btn-danger" Text="Back" PostBackUrl="~/CommonPages/Home.aspx"/>
                </center>
            </div>

        </div>
            
     <asp:Label ID="Label_loggedinperson" runat="server" Visible="false"></asp:Label>

          <asp:Label ID="Label_name" runat="server" Visible="false"></asp:Label>
        </div>
        <br />

        
    <footer>
  
        <center><p>© 2016 Inventory System &nbsp;by&nbsp;<a href="http://www.simpro.co.in" target="_blank">Simpro</a></p></center>
    </footer>
         <script src="../lib/bootstrap/js/bootstrap.js" type="text/javascript"></script>
    </center>

</asp:Content>
