<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Style/digital-clock/assets/css/style.css" rel="stylesheet" />
    <style>
        /*--------------------green-----------------------*/
        .green {
            background-color: #92d050;
            padding: 5px;
            border-top-left-radius: 18px;
            border-top-right-radius: 18px;
            text-align: center;
        }

        .green1 {
            border: 2px solid #92d050;
            border-radius: 20px;
        }

        /*-----------------red--------------------------*/
        .red {
            background-color: #d6575e;
            padding: 5px;
            border-top-left-radius: 18px;
            border-top-right-radius: 18px;
            color: white;
            text-align: center;
        }

        .red1 {
            _border: 2px solid #c00000;
            border: 2px solid #d6575e;
            border-radius: 20px;
        }

        /*-----------------yellow--------------------------*/

        .yellow {
            background-color: #ffc000;
            padding: 5px;
            border-top-left-radius: 18px;
            border-top-right-radius: 18px;
            text-align: center;
        }

        .yellow1 {
            border: 2px solid #ffc000;
            border-radius: 20px;
        }

        /*-----------------blue--------------------------*/

        .blue {
            background-color: #349082;
            padding: 5px;
            border-top-left-radius: 18px;
            border-top-right-radius: 18px;
            text-align: center;
            color: white;
        }

        .blue1 {
            border: 2px solid #349082;
            border-radius: 20px;
        }






        .padding {
            padding: 5px;
        }

        .separatorH {
            background-color: #bfbfbf;
            padding: 5px;
            height: 100%;
        }

        .separatorV {
            background-color: #bfbfbf;
            padding: 5px;
            height: 100%;
        }
    </style>



    <style type="text/css">
        .sector {
            border: 1px solid #ddd4d4;
            margin: 2px;
            width: 330px;
            height: 240px;
            float: left;
            border-radius: 5px;
        }

        .Headerdiv {
            font-size: 11px;
            font-family: Arial;
            width: 100%;
            height: 30px;
            text-align: center;
            font-weight: bold;
            background-color: #349082;
            padding-top: 5px;
            color: white;
        }
    </style>
    <style>
        .color1, .color2, .color3, .color4, .color5, .color6, .color7,.color8,.color9 {
            height: 130px;
            padding: 5px;
            _border-right: 10px solid #efe6e6;
            _border-right: 10px solid #dbd7d7;
            border-right:5px solid white;
            border-left:5px solid white;
            color: white;
            text-align: center;
            vertical-align: bottom;
        }

            .color1 h1, .color2 h1, .color3 h1, .color4 h1, .color5 h1, .color6 h1, .color7 h1 {
                _margin-top: -1px;
            }
        .color1:hover, .color2:hover, .color3:hover, .color4:hover, .color5:hover, .color6:hover, .color7:hover {
        
        opacity:0.9;
        font-size:110%;
        
        }
        .color1,.color8,.color9 {
            background: #f27b53;
        }

        .color2 {
            background: #75b749;
        }

        .color3 {
            background: #0caed4;
        }

        .color4 {
            background: #ffb400;
        }

        .color5 {
            background: #77818a;
        }

        .color6 {
            background: #9d4a9c;
        }

        .color7 {
            background: #df577b;
        }

        h1 i {
            float: left;
            padding-left: 15px;
        }
         .colorapproval{
            background: #2d7f94;
            text-align:left;
            padding:11px 7px;
            color:#ffffff;
            font-size:12px;
            _border:2px solid white;
            width:100%;
            font-weight:bold;
        }
          .colorapproval2{
            background: #2c9fb2;
            text-align:left;
            padding:11px 7px;
            font-weight:bold;
            color:#ffffff;
            font-size:12px;
            _border:2px solid white;
            width:100%;

        }
            .colorapproval a, .colorapproval2 a {
            color:white;
            }
        .colorapproval i,.colorapproval2 i {
        float:right;
        }
        .colorapproval:hover,.colorapproval2:hover {
         _opacity:0.6;
        }
        .row {
        min-height:140px;
        }
        .colorapproval:hover, .colorapproval2:hover {
        font-size:14px;
        font-weight:bold;
        }
    </style>

    <script type="text/javascript">
        window.setInterval(function () {
            window.location.reload();           
        }, 600000);
    </script>

    <div style="background:white;  padding:5px 15px;">
    <div class="row">
        <div class="col-md-2 color1">
            <h1><i class="glyphicon glyphicon-dashboard"></i><asp:LinkButton ID="lbtnCountRunningProject" runat="server" ForeColor="White" OnClick="lbtnCountRunningProject_Click"></asp:LinkButton></h1>
            <br />
          
           <asp:LinkButton ID="lbtnRunningProject" runat="server" ForeColor="White" Text="Running Project" OnClick="lbtnRunningProject_Click"></asp:LinkButton>

        </div>
        <div class="col-md-2 color2">
            <h1><i class="glyphicon glyphicon-credit-card"></i><asp:LinkButton ID="lbtnCountApprovedBudget" runat="server" ForeColor="White" OnClick="lbtnCountApprovedBudget_Click"></asp:LinkButton></h1>
            <br />
           
            
         <asp:LinkButton ID="lbtnApprovedBudget" runat="server" ForeColor="White" Text="Approved Budget Current Month" OnClick="lbtnApprovedBudget_Click"></asp:LinkButton>


        </div>
        <div class="col-md-4 color3">
            <h1><i class="glyphicon glyphicon-credit-card"></i><asp:Label ID="lblUtilisedBudget" Text="0" runat="server" ForeColor="White"  ></asp:Label></h1>
            <br />
            Utilised Budget current month

        </div>
        <div class="col-md-2 color4">
            <h1><i class="glyphicon glyphicon-file"></i> <asp:LinkButton ID="lbtnCountPOProcessed" runat="server" ForeColor="White"  OnClick="lbtnCountPOProcessed_Click" ></asp:LinkButton></h1>
            <br />
          <asp:LinkButton ID="lbtnPOProcessed" runat="server" ForeColor="White" Text="PO Processed Current Month" OnClick="lbtnPOProcessed_Click"></asp:LinkButton>


        </div>
        <div class="col-md-2 color5">
            <h1><i class="glyphicon glyphicon-briefcase"></i> <asp:LinkButton ID="lbtnCountTotalassets" runat="server" ForeColor="White"  OnClick="lbtnCountTotalassets_Click" ></asp:LinkButton></h1>
            <br />           
               <asp:LinkButton ID="lbtnTotalassets" runat="server" ForeColor="White" Text="Total assets" OnClick="lbtnTotalassets_Click"></asp:LinkButton>


        </div>

    </div>
    <div class="row">
        <div class="col-md-4 color7">
            <h1><i class="glyphicon glyphicon-asterisk"></i> <asp:LinkButton ID="lbtnCountDieselIssued" runat="server" ForeColor="White"  OnClick="lbtnCountDieselIssued_Click" ></asp:LinkButton></h1>
            <br />
            <asp:LinkButton ID="lbtnDieselIssued" runat="server" ForeColor="White" Text="Diesel Issued Current Month" OnClick="lbtnDieselIssued_Click"></asp:LinkButton>


        </div>
        <div class="col-md-2 color8">
            <h1><i class="glyphicon glyphicon-eject"></i><asp:Label ID="lblRecurringItems" Text="0" runat="server" ForeColor="White" ></asp:Label></h1>
            <br />
            Recurring Issued Current Month


        </div>
        <div class="col-md-4 color6">
            <h1><i class="glyphicon glyphicon-circle-arrow-right"></i><asp:LinkButton ID="lbtnCountTotalSubcontractor" runat="server" ForeColor="White" OnClick="lbtnCountTotalSubcontractor_Click" ></asp:LinkButton></h1>
            <br />
          
               <asp:LinkButton ID="lbtnTotalSubcontractor" runat="server" ForeColor="White"  Text="Total Subcontractor" OnClick="lbtnTotalSubcontractor_Click"></asp:LinkButton>


        </div>
        <div class="col-md-2 color9">
            <h1><i class="glyphicon glyphicon-user"></i><asp:LinkButton ID="lbtnCountTotalVendor" runat="server" ForeColor="White" OnClick="lbtnCountTotalVendor_Click" > </asp:LinkButton></h1>
            <br />
            
        <asp:LinkButton ID="lbtnTotalVendor" runat="server" ForeColor="White"  Text=" Total Vendor" OnClick="lbtnTotalVendor_Click"></asp:LinkButton>

        </div>


    </div>
    <div class="row">

        <div class="col-md-6 img1" style="background:url(../Style/Images/services_banner.jpg) no-repeat;height:455px;background-size:cover;border:5px solid white ">
            
        </div>
        <div class="col-md-6">
         <div class="row">
         <div class="col-md-12" style="background:url(../Style/Images/clk.jpg) no-repeat;background-size:cover;border:10px solid #f5e6a3;min-height:180px;">
            <!--------------------------------------------------------------------->
            <div id="clock"  class="light" >
			<%--<div class="display">--%>
           <div style="text-align:left;padding-top:25%">
				<%--<div class="weekdays"></div>
				<div class="ampm"></div>
				<div class="alarm"></div>--%>
				<div class="digits"></div>
			</div>
		</div>

		<%--<div class="button-holder">
			<a class="button">Switch Theme</a>
		</div>--%>
            <!---------------------------------------------------------------------->


        </div>
         </div>  
            <div class="row"> 
        <div class="col-md-12" style="background: #0caed4;padding:10px;height:255px;">
             <div class="col-md-12 colorapproval"><a href="../Procurement/PaymentIndentList_Verification.aspx"><b>Payment Indent Awaiting for Verification</b></a> <b>- <asp:Label runat="server" ID="lblPaymentIndWaiting" Text="0"></asp:Label></b> <i class="glyphicon glyphicon-arrow-right"></i> </div>
            <div class="col-md-12 colorapproval2"><a href="../Procurement/PaymentIndentList_Approval.aspx"><b>Payment Indent Awaiting for Approval</b></a> <b>- <asp:Label runat="server" ID="lblPaymentIndWaitingApproval" Text="0"></asp:Label></b><i class="glyphicon glyphicon-arrow-right"></i> </div>
            <div class="col-md-12 colorapproval"><a href="../Procurement/PaymentIndentList.aspx"><b>Approved Payments</b></a>  <b>- <asp:Label runat="server" ID="lblApprovedPaymentInd" Text="0"></asp:Label></b><i class="glyphicon glyphicon-arrow-right"></i></div>
            <div class="col-md-12 colorapproval2"><a href="../Asset/AssetRecurringItems.aspx"><b>Asset recurring items</b></a> <b>- <asp:Label runat="server" ID="lblAssetRecurringItem" Text="0"></asp:Label></b> <i class="glyphicon glyphicon-arrow-right"></i> </div>
            <div class="col-md-12 colorapproval"><a href="../Asset/AwaitingAssetTransfer.aspx"><b> Transferred asset awaiting for acceptance</b></a> <b>- <asp:Label runat="server" ID="Label1" Text="0"></asp:Label></b> <i class="glyphicon glyphicon-arrow-right"></i> </div>
            <div class="col-md-12 colorapproval2"><a href="../Inventory/AwaitingStockTransfer.aspx"><b>Transferred stock awaiting for acceptance </b></a> <b>- <asp:Label runat="server" ID="Lbl_AwaitingStockTransfer" Text="0"></asp:Label></b> <i class="glyphicon glyphicon-arrow-right"></i> </div>

        </div>
         </div>
        </div>
        

    </div>
    </div>







    <script src="http://cdnjs.cloudflare.com/ajax/libs/jquery/1.10.1/jquery.min.js"></script>
		<script src="http://cdnjs.cloudflare.com/ajax/libs/moment.js/2.0.0/moment.min.js"></script>
		
    <script src="../Style/digital-clock/assets/js/script.js"></script>
</asp:Content>
