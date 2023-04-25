<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Authantication.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8"/>
        <title>UGCL</title>
        <meta content="IE=edge,chrome=1" http-equiv="X-UA-Compatible"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
        <meta name="description" content=""/>
        <meta name="author" content=""/>
        <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,700' rel='stylesheet' type='text/css'/>
        <link rel="stylesheet" type="text/css" href="../lib/bootstrap/css/bootstrap.css"/>
        <link rel="stylesheet" href="../lib/font-awesome/css/font-awesome.css"/>
        <script src="../lib/jquery-1.11.1.min.js" type="text/javascript"></script>
        <link rel="stylesheet" type="text/css" href="../stylesheets/theme.css" />
        <link rel="stylesheet" type="text/css" href="../stylesheets/premium.css" />
        <link href="../stylesheets/cdm_home.css" rel="stylesheet" />
        <link href="../stylesheets/textbox_icon_position.css" rel="stylesheet" />
        <style>
           nav.navbar.navbar-fixed-top.headerpart {
    display: none;
}
            /*.headerpart{
                display: none;
            }*/
            .sucess {
                font-weight: bold;
                font-family: cooper;
                font-size: 13px;
                color: green;
            }

            .form-control {
                width: 100%;
                height: 34px;
            }
          
        </style>
    </head>
        <body>
                    <div class="logincencls">
            
            <div class=""  runat="server" id="LoginPannel"  >
        <div class="">        
         <asp:Label ID="lblStatus" runat="server" CssClass="success"></asp:Label>
         <asp:Login ID="LoginSNC" runat="server" OnAuthenticate="LoginSNC_Authenticate" FailureText="**Authentication failed, check username and password." DisplayRememberMe="False">
         <LayoutTemplate >
            <div class="row">
                <div class="logoimgcls">
                    <img src="../Style/Images/logo123.png" style="position:relative; _left:-15px; top:0px; border-radius:10px; width:520px;height:150px; _border:6px solid #9e121b;box-shadow: 0px 3px 16px rgba(0, 0, 0, 0.35);"/>
                </div>
                <div class="logincls12">
                     <div class="form-group input-group inlinelogincls">          
                          <asp:TextBox  runat="server" CssClass="form-control logfwidth" required="required" placeholder="User Name" ID="UserName"></asp:TextBox>
                    </div>
                
                    <div class="form-group input-group inlinelogincls">           
                      <asp:TextBox runat="server" CssClass="form-control logfwidth" required="required" placeholder="Password" ID="Password" TextMode="Password" ></asp:TextBox>
                    </div>
                    <div class="form-group input-group inlinelogincls">
                         <asp:Button runat="server" id="btnLogin" CommandName="Login" CssClass="btn btn-danger"  Text="Login"/>
                </div>
                </div>
                
            </div>
           
           
         
   </LayoutTemplate>
        </asp:Login>
        </div>
        </div>
            <div class="forgotcls">
          <a href="Forget_Password.aspx">Forgot Password ?</a>
      </div>
        
        </div>
      </body>

    </html>

</asp:Content>

