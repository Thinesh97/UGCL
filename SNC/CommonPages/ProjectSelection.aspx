<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Authantication.Master" AutoEventWireup="true" CodeBehind="~/CommonPages/ProjectSelection.aspx.cs" Inherits="ProjectSelection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="scriptmgr1" runat="server"></asp:ScriptManager>
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
        <link rel="stylesheet" href="../lib/font-awesome/css/font-awesome.css"//>
        <script src="../lib/jquery-1.11.1.min.js" type="text/javascript"></script>
        <link rel="stylesheet" type="text/css" href="../stylesheets/theme.css" />
        <link rel="stylesheet" type="text/css" href="../stylesheets/premium.css" />
        <link href="../stylesheets/cdm_home.css" rel="stylesheet" />
        <%-- <link href="../stylesheets/authentication.css" rel="stylesheet" />--%>
        <link href="../stylesheets/textbox_icon_position.css" rel="stylesheet" />
        <link href="../Style/SNC_theme.css" rel="stylesheet" />

        <style>
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
             .divnotification a {
             margin-bottom: 5px;
    color: #fff;
    font-weight: 600;
    font-size: 14px;
    font-family: sans-serif;
}      .radisresize {
    border-radius: 20px;
    margin-bottom:3px;
}
       .divnotification p a {
    margin: 0;
    line-height: 20px;
    text-align: left;
    font-size: 16px;
    
}
       .panel-body {
    padding: 15px;
    background-color: #e3e2e2;
}
       .divnotification,.imggrph {
    height: 294px;
   overflow-y: scroll;
}
        </style>
    </head>
        <body> 
         <div class="col-sm-12">
                   <h3 style="text-align:center;color:white;margin:0px; font-weight:800; font-family:Bank Gothic Medium">United Global Corporation Limited</h3>
              <h5 style="text-align:center;color:white;font-family:Bank Gothic Medium"" >Formerly United Infra Corp. (BLR) Ltd.</h5>
                </div>
           <div class="notificationnpart">
            <div class="row">
                <div class="col-sm-12">
                     <div class="panel panel-default">
                         <div class="panel-heading">
                              <h3 class="panel-title" style="text-align:center">                                   
                                  <b style="font-size:20px"> On Going Project   <span class="badge badgecls;font-family:Bank Gothic Medium"><asp:Label runat="server" ID="lblOnGoingProjectCount" Text="0"></asp:Label></span></b>               
                                </h3>
                        </div>
                        <div class="panel-body">
                            <div class="divnotification" runat="server" id="divOnGoingProject">
                            </div>
                        </div>
              </div>
                </div>
            </div>
        </div>
         <div class="notificationnpart">
            <div class="row">
                <div class="col-sm-12">
                     <div class="panel panel-default">
                         <div class="panel-heading">
                              <h3 class="panel-title" style="text-align:center">                                   
                                  <b style="font-size:20px">Completed Projects  <span class="badge badgecls"><asp:Label runat="server" ID="lblCompletedProjects" Text="0"></asp:Label></span></b>               
                                </h3>
                        </div>
                        <div class="panel-body">
                            <div class="divnotification" runat="server" id="Div_CompletedProjects">
                            </div>
                        </div>
              </div>
                </div>
            </div>
        </div>
            </body>
    </html>
</asp:Content>

