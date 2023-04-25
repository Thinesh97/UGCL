<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Authantication.Master" AutoEventWireup="true" CodeBehind="Forget_Password.aspx.cs" Inherits="Forget_Password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
   
    <center>
      <br /><br /><br /><br />
        <div class="rounddiv1">
        <div class="rounddiv2">

            <img src="../Style/Images/SNC_logo1.jpg" style="position:relative; _left:-15px; top:-40px; border-radius:10px; width:90%; _border:6px solid #9e121b;box-shadow: 0px 3px 16px rgba(0, 0, 0, 0.35);"/>
        

    
    <div class="row">
        
        <center>
           
            <h3>

                Forgot Password 

                <i class="glyphicon glyphicon-lock" style="font-size:20px;">
                    
                </i>
            </h3>
        </center>
        <div class="form-group input-group">
            <span class="input-group-addon">
                <i class="glyphicon glyphicon-user"></i>
            </span>
            <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="User Id"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtUserId" CssClass="Validation_Text" 
                    ErrorMessage="Required!" ValidationGroup="valuser">*</asp:RequiredFieldValidator>
    </div>
    

</div>
    <div class="row">

    
    <div class="form-group input-group">
        <span class="input-group-addon">
             <i class="glyphicon glyphicon-envelope"></i>
        </span>

       <asp:TextBox ID="txtEmailId" runat="server" CssClass="form-control" placeholder="Email Id"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtEmailId" CssClass="Validation_Text" 
                    ErrorMessage="Required!" ValidationGroup="valuser">*</asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtEmailId" CssClass="Validation_Text" 
                    ErrorMessage="Enter Valid Email" Display="Dynamic" 
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.([com in org net])+$" 
                    ValidationGroup="valuser">Please enter a Valid Email Address</asp:RegularExpressionValidator>
        </div>
       
        
    </div>
    <div class="row text-center">
        <asp:Button runat="server" id="btnSendmail" CssClass="btn btn-danger" Text="Send Mail" OnClick="btnSendmail_Click" ValidationGroup="valuser"/>
        <asp:Button runat="server" id="btnBack" CssClass="btn btn-danger" Text="Back" PostBackUrl="~/CommonPages/Login.aspx" OnClick="btnBack_Click1" CausesValidation="false"/>
    </div>
    
            </div>
            </div>


    </center>
</asp:Content>
