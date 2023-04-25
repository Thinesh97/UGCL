<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="MRN.aspx.cs" Inherits="MRN" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>
   
    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });

            $('.ob_gCFR tr:last').find("td").eq(14).find("div").css("float", "right");
            $('.ob_gCFR tr:last').find("td").eq(14).find("div").text($("#ContentPlaceHolder1_txtTransportationCost").val());

            //$(".ob_gCc2R").text($("#ContentPlaceHolder1_txtTransportationCost").val());
        });

        function CalculateItemAmt() {
            var Qty = document.getElementById("<%=txtreqquantity.ClientID%>").value;

            var IndentQty = document.getElementById("<%=txtIndenQty1.ClientID%>").value;

            if (IndentQty < Qty) {

                document.getElementById("<%=txtreqquantity.ClientID%>").value = "0.00";
                alert("Please enter less then or qual to indent Qty");
            }
            else {

                var Price = document.getElementById("<%=txtPrice.ClientID%>").value;
                var Amount = parseFloat(Qty) * parseFloat(Price);

                document.getElementById("<%=txtamount.ClientID%>").value = Amount.toFixed(2);
            }


        }
        function Ajaxfuncation() {
            var ajaxrequest = new XMLHttpRequest();
        }
        function CalculateItemAmt() {
            var Qty = document.getElementById("<%=txtPOQty.ClientID%>").value;
            var Price = document.getElementById("<%=txtPricePerUnit.ClientID%>").value;
            var Amount = parseFloat(Qty) * parseFloat(Price);
            document.getElementById("<%=txttotal.ClientID%>").value = Amount.toFixed(2);
        }


        function CalculateReceivedQty() {

            debugger;
            var POQty = document.getElementById("<%=txtPOQty.ClientID%>").value;
            var QtyAlreadyReceived = document.getElementById("<%=txtAlreadyReceivedQty.ClientID%>").value;
            var ReceivedQty = document.getElementById("<%=txtReceivedQty.ClientID%>").value;
            var AcceptedQty = document.getElementById("<%=txtAcceptedQty.ClientID%>").value;
            var PricePerUnit = document.getElementById("<%=txtPricePerUnit.ClientID%>").value;
            // 5 % Extra Qty if the PO exists
            var extra5percentQty = ((parseFloat(POQty) * 5) / 100).toFixed(2);
            var RemPOQty = ((parseFloat(POQty) - parseFloat(QtyAlreadyReceived)) + parseFloat(extra5percentQty)).toFixed(2);
            var RemPOQtywithoutExtra = ((parseFloat(POQty) - parseFloat(QtyAlreadyReceived))).toFixed(2);

            if (POQty > 0) {

                if (parseFloat(ReceivedQty) > parseFloat(RemPOQty)) {
                    document.getElementById("<%=txtPendingQty.ClientID%>").value = "0";
                    document.getElementById("<%=txtReceivedQty.ClientID%>").value = "0";
                    document.getElementById("<%=txtAcceptedQty.ClientID%>").value = "0";
                    document.getElementById("<%=txtRejectedQty.ClientID%>").value = "0";
                }
                else {
                    if (parseFloat(AcceptedQty) > parseFloat(ReceivedQty)) {
                        document.getElementById("<%=txtAcceptedQty.ClientID%>").value = "0";
                        document.getElementById("<%=txtRejectedQty.ClientID%>").value = "0";
                        document.getElementById("<%=txtPendingQty.ClientID%>").value = "0";

                    }
                    else {
                        var netRejectedQty = (parseFloat(ReceivedQty) - parseFloat(AcceptedQty));
                        var nettotalQty = ((parseFloat(RemPOQtywithoutExtra) - parseFloat(ReceivedQty)).toFixed(2));

                        document.getElementById("<%=txtRejectedQty.ClientID%>").value = (parseFloat(ReceivedQty) - parseFloat(AcceptedQty)).toFixed(2);
                        document.getElementById("<%=txtPendingQty.ClientID%>").value = (parseFloat(nettotalQty) + parseFloat(netRejectedQty)).toFixed(2);
                        document.getElementById("<%=txttotal.ClientID%>").value = (parseFloat(AcceptedQty) * parseFloat(PricePerUnit)).toFixed(2);
                    }
                }
            }
        }

        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                return false;
            }
            return true;
        }

    </script>
       <script type="text/javascript">
           $(document).ready(function () {
               $('.chosen-select').chosen();
           });
       </script>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>


    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Material Received Note 

            </h3>
        </div>
        <div class="panel-body">
            <!------------------------------------------------------------Body Content-------------------------------------------------------------------->
            <div class="row">
                <div class="col-md-2">
                    Select MRN Type&nbsp;
                </div>
                <div class="col-md-6">
                    <asp:RadioButtonList ID="rblMRNType" AutoPostBack="true"  OnSelectedIndexChanged="rblMRNType_SelectedIndexChanged" RepeatDirection="Horizontal" runat="server" TabIndex="1">
                        <asp:ListItem Text="Purchase MRN" Value="PurchaseMRN"></asp:ListItem>  
                        <asp:ListItem Text="Service MRN" Value="ServiceMRN" ></asp:ListItem>
                         <asp:ListItem Text="Local MRN" Value="LocalMRN"></asp:ListItem> 
                    </asp:RadioButtonList>
                </div>
            </div>
            
            <div runat="server" id="div_AllFields">

            <div class="row">
                <div runat="server" id="div_Vendor">
                    <div class="col-md-2">Vendor Name&nbsp;</div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlVendorName" runat="server"  class="chosen-select form-control" OnSelectedIndexChanged="ddlVendorName_SelectedIndexChanged" AutoPostBack="true" TabIndex="1"> </asp:DropDownList>
                    </div>
                    </div>
                    <div runat="server" id="div_VendorNameLocal">
                    <div class="col-md-2">Vendor Name&nbsp;</div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtVendornameLocal" CssClass="form-control"  runat="server"></asp:TextBox>
                    </div>
                </div>
                <div runat="server" id="div_SubCon">
                    <div class="col-md-2">Sub Contractor Name</div>
                    <div class="col-md-4">
                        <asp:DropDownList runat="server" ID="ddlSubContractor" class="chosen-select form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubContractor_SelectedIndexChanged"  TabIndex="4"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-2">
                    Bill Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBillDate" CssClass="Validation_Text" ValidationGroup="ValMRN" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtBillDate" CssClass="form-control" onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server" TabIndex="2" AutoComplete="Off"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtBillDate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>
            
            <div class="row" runat="server" id="div_PO">
                <div class="col-md-2">
                    PO No&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlPONo" runat="server" OnSelectedIndexChanged="ddlPONo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" TabIndex="3"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    PO Date
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPODate" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="row" runat="server" id="div_WO">
                <div class="col-md-2">
                    WO No&nbsp;
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlWONo" runat="server" OnSelectedIndexChanged="ddlWONo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    WO Date
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtWODate" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                    <%--<ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender4" Format="dd-MM-yyyy" TargetControlID="txtWODate"></ajaxToolkit:CalendarExtender>--%>
                </div>
            </div>

            <%-- <div class="row">
                <div class="col-md-2"> 
                    Indent No &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="-Select-" ControlToValidate="ddlIndentNo" CssClass="Validation_Text" ValidationGroup="ValMRN" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList ID="ddlIndentNo" runat="server" OnSelectedIndexChanged="ddlIndentNo_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" TabIndex="4">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Indent Date 
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtIndentDate" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>
            </div>--%>

            <div class="row">
                <div class="col-md-2">
                 <label id="MRN_No_Lable" runat="server" >  &nbsp;</label>   
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMRNNo" CssClass="Validation_Text" ValidationGroup="ValMRN" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtMRNNo" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <label id="MRN_Date_Lable" runat="server" >  &nbsp;</label> 
                  
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDate" CssClass="Validation_Text" ValidationGroup="ValMRN" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtDate" CssClass="form-control" OnTextChanged="txtDate_TextChanged" AutoPostBack="true" onkeypress="javascript:return false;" onpaste="javascript:return false;" runat="server" TabIndex="5" AutoComplete="Off"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="cal1" Format="dd-MM-yyyy" TargetControlID="txtDate"></ajaxToolkit:CalendarExtender>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2">Invoice No</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtInvoiceNo" CssClass="form-control" MaxLength="30" runat="server" TabIndex="6" AutoComplete="Off"></asp:TextBox>
                </div>
                <div class="col-md-2">Remarks</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtRemarks" CssClass="form-control" MaxLength="250" Style="resize: none" TextMode="MultiLine" runat="server" TabIndex="7"></asp:TextBox>
                </div>
                <%--<div class="col-md-2">Payment Terms</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtPaymentTerms" CssClass="form-control" MaxLength="250" Style="resize: none" TextMode="MultiLine" runat="server" TabIndex="7"></asp:TextBox>--%>
            </div>

            <div class="row" runat="server" visible="false" id="trTransportCost">
                <div class="col-md-2">Transportation Cost</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtTransportationCost" Text="0" CssClass="form-control input-pos-float" MaxLength="18" runat="server" TabIndex="8"></asp:TextBox>
                </div>
            </div>

            <div class="row" runat="server">
                <div class="col-md-2">
                    Ledger Head &nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="-Select-" ControlToValidate="ddlLedgerhead" CssClass="Validation_Text" ValidationGroup="ValMRN" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <a href="#ModelLedgerHeadPopup" data-toggle="modal" role="button">
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/Plus__Orange.png" Height="15px" Width="15px" CausesValidation="false" />
                    </a>
                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlLedgerhead"  class="chosen-select form-control" TabIndex="9"></asp:DropDownList>
                </div>
            </div>
            
            <br />
            <div class="row" runat="server" id="div_upInvoice">
                <div class="col-md-2">
                    Upload Invoice Copy
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuInvoiceCopy"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="rev1" runat="server"
                        ErrorMessage="PDF File Only" ForeColor="Red" ControlToValidate="fuInvoiceCopy"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf)$">  
                    </asp:RegularExpressionValidator>
                </div>
                <div runat="server" id="div_AfterUpload_Invoice" visible="false">
                    <div class="col-md-2">
                        Uploaded Invoice Copy
                    </div>
                    <div class="col-md-4">
                        <%--<asp:HyperLink runat="server" ID="hyplDownloadFile" ForeColor="#337ab7" Visible="false"></asp:HyperLink>--%>
                        <asp:LinkButton runat="server" ID="lnkDownloadFile" OnClick="lnkDownloadFile_Click" ForeColor="#337ab7"></asp:LinkButton>
                    </div>
                </div>
                  
            </div>
               
            <div class="row" runat="server" id="div_upBill">
                <div class="col-md-2">
                    Upload Way Bill Copy
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuWay_Bill_Copy"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                        ErrorMessage="PDF File Only" ForeColor="Red" ControlToValidate="fuWay_Bill_Copy"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf)$">  
                    </asp:RegularExpressionValidator>
                </div>
                <div runat="server" id="div_AfterUpload_WayBill" visible="false">
                    <div class="col-md-2">
                        Uploaded Way Bill Copy
                    </div>
                    <div class="col-md-4">
                        <%--<asp:HyperLink runat="server" ID="hyplDownloadFile" ForeColor="#337ab7" Visible="false"></asp:HyperLink>--%>
                        <asp:LinkButton runat="server" ID="lnkDownloadFile_WayBill" OnClick="lnkDownloadFile_WayBill_Click" ForeColor="#337ab7"></asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="row" runat="server" id="div_upOther">
                <div class="col-md-2">
                    Upload Other
                </div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fuOther_PurchaseMRN"></asp:FileUpload>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                        ErrorMessage="PDF File Only" ForeColor="Red" ControlToValidate="fuOther_PurchaseMRN"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.pdf)$">  
                    </asp:RegularExpressionValidator>
                </div>
                <div runat="server" id="div_AfterUpload_Other" visible="false">
                    <div class="col-md-2">
                        Uploaded Other
                    </div>
                    <div class="col-md-4">
                        <%--<asp:HyperLink runat="server" ID="hyplDownloadFile" ForeColor="#337ab7" Visible="false"></asp:HyperLink>--%>
                        <asp:LinkButton runat="server" ID="lnkDownloadFile_Other" OnClick="lnkDownloadFile_Other_Click" ForeColor="#337ab7"></asp:LinkButton>
                    </div>
                </div>
            </div>

            
            <br />
            <br />
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValMRN" CssClass="btn btn-default" OnClick="btnSubmit_Click" TabIndex="9"></asp:Button>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="10"></asp:Button>
                <asp:Button ID="btnUpdateTransportationCost" runat="server" Text="Update Transportation Cost" Visible="false" CssClass="btn btn-default" TabIndex="11"></asp:Button>
                <asp:Button ID="Btn_Entered" runat="server" Visible="false" Text="Enter MRN Invoice Amount Into Tally" OnClick="Btn_Tally_Entered_Click" CssClass="btn btn-default"></asp:Button>
                 <asp:Button ID="btn_AddLocalItem" runat="server" Visible="false" Text="Add Local MRN Item" OnClick="Btn_AddLocalMRN_Item_Click" CssClass="btn btn-default"></asp:Button>
                <asp:Button ID="btn_AddServiceItem" runat="server" Visible="false" Text="Add Service MRN Item" OnClick="Btn_AddServiceMRN_Item_Click" CssClass="btn btn-default"></asp:Button>
            </div>
            
            
            <br />
            <br />
            <ogrid:Grid runat="server" ID="GridMRNItem" AutoGenerateColumns="false" ShowFooter="true" 
                ShowColumnsFooter="true" CallbackMode="false" OnRowDataBound="GridMRNItem_RowDataBound" 
                OnDeleteCommand="GridMRNItem_DeleteCommand" OnRowCreated="GridMRNItem_RowCreated"   
                FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="50" >
                <ScrollingSettings ScrollWidth="100%" />
                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                <Columns>
                        
                    <ogrid:Column DataField="Category_Name" HeaderText="Category" Width="120" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="Item_Name" HeaderText="Material Item" Width="200"></ogrid:Column>
                    <ogrid:Column DataField="PO_Qty" HeaderText="PO Qty" Align="right" Width="90"></ogrid:Column>
                    <ogrid:Column DataField="MRNReceived_Qty" HeaderText="Received Qty" Align="right" Width="120"></ogrid:Column>
                    <ogrid:Column DataField="MRNAccepted_Qty" HeaderText="Accepted Qty" Align="right" Width="120"></ogrid:Column>
                    <ogrid:Column DataField="MRNRejected_Qty" HeaderText="Rejected Qty" Align="right" Width="120"></ogrid:Column>                 
                    <ogrid:Column DataField="MRNPending_Qty" HeaderText="Pending Qty" Align="right" Width="120"></ogrid:Column>
                    <ogrid:Column DataField="Rate" HeaderText="Rate" Align="right" Width="100" ></ogrid:Column>
                    <ogrid:Column DataField="Amount" HeaderText="Amt" Align="right" Width="110" ></ogrid:Column>
                    <ogrid:Column DataField="TotalTax" HeaderText=" Tax %" Align="right" Width="110" ></ogrid:Column>
                    <ogrid:Column DataField="MRNTaxAmt" HeaderText="Tax Amt" Align="right" Width="110" ></ogrid:Column>
                    <ogrid:Column DataField="MRNDiscountAmt" HeaderText="Discount Amt" Align="right" Width="110" ></ogrid:Column>
                    <ogrid:Column DataField="MRNItem_TransportCost" HeaderText="Transportation Amount" Align="right" Width="180" ></ogrid:Column>
                    <ogrid:Column DataField="MRNInvoiceAmt" HeaderText="Invoice Amount" Align="right" Width="150" ></ogrid:Column>
                    <ogrid:Column DataField="Material_Item_Id" HeaderText="Edit" runat="server" Width="90"  >
                        <TemplateSettings  TemplateId="MRNItemID"/>
                    </ogrid:Column>   
                    <ogrid:Column AllowDelete="true" HeaderText="Delete" Visible="false" Width="90"></ogrid:Column>

            </Columns>
                <Templates>
                    <ogrid:GridTemplate ID="MRNItemID" runat="server">
                        <Template>
                            <asp:LinkButton ID="MRN_ItemID" runat="server" CssClass="gridCB" OnClick="MRN_ItemID_Click" Text="Edit" CommandArgument='<%#Container.DataItem["Item_Name"] + "#" + Container.DataItem["UOMPrefix"] %>'  CommandName='<%#Container.DataItem["Material_Item_Id"] %>' >
                            </asp:LinkButton>
                        </Template>
                    </ogrid:GridTemplate>
                </Templates>
            </ogrid:Grid>

            <ogrid:Grid runat="server" ID="Grid_ServiceMRNItem_WO"  AutoGenerateColumns="false" ShowFooter="true" 
                ShowColumnsFooter="true" CallbackMode="false"   
                FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="50" >
                <ScrollingSettings ScrollWidth="100%" />
                <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                <Columns>
                        
                    <ogrid:Column DataField="Wo_Description" HeaderText="Work Order Description" Wrap="true"></ogrid:Column>
                    <ogrid:Column DataField="Billing_Start_Period" HeaderText="Billing Start Period" Align="right" Width="90"></ogrid:Column>
               <ogrid:Column DataField="Billing_Start_Period" HeaderText="Billing Start Period" Align="right" Width="90"></ogrid:Column>
                     <ogrid:Column DataField="Billing_End_Period" HeaderText="Billing End Period" Align="right" Width="90"></ogrid:Column>
                     <ogrid:Column DataField="Rate" HeaderText="Rate" Align="right" Width="90"></ogrid:Column>
                     <ogrid:Column DataField="Qty" HeaderText="Qty" Align="right" Width="90"></ogrid:Column>
                     <ogrid:Column DataField="UOM" HeaderText="UOM" Align="right" Width="90"></ogrid:Column>
                     <ogrid:Column DataField="Amount" HeaderText="Amount" Align="right" Width="90"></ogrid:Column>
                     <ogrid:Column DataField="TDS" HeaderText="TDS" Align="right" Width="90"></ogrid:Column>
                     <ogrid:Column DataField="TDS_Amount" HeaderText="TDS Amount" Align="right" Width="90"></ogrid:Column>
                    <ogrid:Column DataField="Debit_Amount" HeaderText="Debit Amount" Align="right" Width="90"></ogrid:Column>
                    <ogrid:Column DataField="TDS_Amount" HeaderText="TDS Amount" Align="right" Width="90"></ogrid:Column>
                    <ogrid:Column DataField="Net_Payable" HeaderText="Net Payable Amt" Align="right" Width="90"></ogrid:Column>
                    <ogrid:Column AllowDelete="true" HeaderText="Delete" Visible="false" Width="90"></ogrid:Column>

            </Columns>
               <%-- <Templates>
                    <ogrid:GridTemplate ID="MRNItemID_WO" runat="server">
                        <Template>
                            <asp:LinkButton ID="MRN_ItemID" runat="server" CssClass="gridCB"  Text="Edit" CommandArgument='<%#Container.DataItem["WONo"] %>'  CommandName='<%#Container.DataItem["SRN_No"] %>' >
                            </asp:LinkButton>
                        </Template>
                    </ogrid:GridTemplate>
                </Templates>--%>
            </ogrid:Grid>

            </div>
                

        </div>
    </div>
        <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->



        <ajaxToolkit:ModalPopupExtender ID="ModelLedgerHeadPopup" runat="server" PopupControlID="PanelLedgerHead" TargetControlID="ImageButton2"
            CancelControlID="btnClose2" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelLedgerHead" runat="server" align="center" Style="display: none">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" id="btnClose2" class="close" data-dismiss="modal" aria-hidden="true">×</button>

                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-2">
                                Ledger Head
                                <asp:RequiredFieldValidator ID="RFV_ct" runat="server" ErrorMessage="*" ControlToValidate="txtLedgerHead" CssClass="Validation_Text" ValidationGroup="ValLedgerHead"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtLedgerHead" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <center>
                                <ogrid:Grid ID="Grid_LedgerHead" CallbackMode="true"   AllowPageSizeSelection="false" 
                                    runat="server" OnDeleteCommand="Grid_LedgerHead_DeleteCommand"  
                                    FolderStyle="../Gridstyles/grand_gray" AutoGenerateColumns="false" AllowAddingRecords="false"
                                    AllowFiltering="false" AllowPaging="true" PageSize="5">
                                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                                    <Columns>
                                        <ogrid:Column DataField="Ledger_Head_Name" HeaderText="Ledger Head" Width="200px" Visible="true"></ogrid:Column>  
                                        <ogrid:Column DataField="" HeaderText="Edit" Width="100px">
                                            <TemplateSettings TemplateId="EditTemplate" />
                                        </ogrid:Column>
                                        <ogrid:Column AllowDelete="true" HeaderText="Delete"  ></ogrid:Column>
                                    </Columns>
                                    <Templates>
                                        <ogrid:GridTemplate runat="server" ID="EditTemplate">
                                            <Template>
                                                <asp:LinkButton ID="lnkLedgerHead" CausesValidation="false"
                                                        CommandName='<%# Container.DataItem["Ledger_Head_Name"] %>' 
                                                    OnClick="lnkLedgerHead_Click" Text="Edit" CssClass="gridCB" runat="server"></asp:LinkButton>
                                            </Template>
                                        </ogrid:GridTemplate>
                                    </Templates>
                                </ogrid:Grid>
                            </center>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <asp:Button ID="btnSaveLedgerHead" runat="server" Text="Save"   CssClass="btn btn-default" ValidationGroup="ValLedgerHead" OnClick="btnSaveLedgerHead_Click" />
                            <asp:Button ID="btnCancelLedgerHead" runat="server" Text="Cancel"  CssClass="btn btn-default" ValidationGroup="ValLedgerHead" CausesValidation="false" OnClick="btnCancelLedgerHead_Click"  />
                        </center>

                    </div>
                </div>
            </div>
        </asp:Panel>

        
    
    
    
        <!-- ModalPopupExtender -->
        <asp:Button runat="server" ID="PopTargetID" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeMRNtems" runat="server" PopupControlID="PanelMRNItem" TargetControlID="PopTargetID"
            CancelControlID="btnClose" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelMRNItem" runat="server" align="center" Style="display: none">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <center> <h5 id="myModalLabelcrate">Update MRN Items</h5></center>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-2">Category</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtCategory" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2">Item</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtItem" Enabled="false" CssClass="form-control"></asp:TextBox>
                                 <asp:TextBox runat="server" ID="txtItemCode" Visible="false" Enabled="false" CssClass="form-control"></asp:TextBox>
                                <asp:TextBox runat="server" ID="txtMRN_No" Visible="false" Enabled="false" CssClass="form-control"></asp:TextBox>
                                
                            </div>
                        </div>

                        <div class="row">
                            <%--<div class="col-md-2">Indent Qty</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtIndenQty" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>--%>
                            <div class="col-md-2">PO Qty</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtPOQty" Enabled="false"  autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                        </div>
                          
                        <div class="row">
                            <div class="col-md-2">Price Per Unit</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtPricePerUnit" Enabled="false" onkeyup="CalculateItemAmt()"  autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                            <div class="col-md-2">Total Price</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txttotal" Enabled="false" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-md-2">Already Received Qty</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtAlreadyReceivedQty" Enabled="false" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                            <div class="col-md-2">Received Qty</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtReceivedQty" onkeyup="CalculateReceivedQty()" onpaste="javascript:return false;" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                Accepted Qty  
                                <asp:RequiredFieldValidator ID="rfvAccQty" runat="server" ControlToValidate="txtAcceptedQty" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="ValAddItem"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtAcceptedQty" onkeyup="CalculateReceivedQty()" onpaste="javascript:return false;" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                            <div class="col-md-2">Rejected Qty</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtRejectedQty" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-md-2">Pending Qty</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtPendingQty" autocomplete="off" onpaste="javascript:return false;" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                             <div class="col-md-2">Tax % </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtTaxPercent" onkeypress="LocalMRNTaxprcent()"  autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="row">
                            <div class="col-md-2">Transportation Cost</div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtItemTransportationCost" Text="0" CssClass="form-control input-pos-float" MaxLength="18" runat="server" TabIndex="7"></asp:TextBox>
                            </div>
                             <div class="col-md-2">Discount Amount</div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtDiscountAmount" onkeypress="LocalMRNTaxprcent()"  autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                                <asp:CheckBox runat="server" ID="cbPricefluctUP" Text="Price fluctuations"  TabIndex="26" Visible="false"></asp:CheckBox>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <center>
                            <asp:Button ID="btnUpdate" runat="server" Text="Save"  CssClass="btn btn-default" OnClick="btnUpdate_Click"  ValidationGroup="ValAddItem"   />
                            <asp:Button ID="btnCancelItem" runat="server" Text="Cancel"  CssClass="btn btn-default" CausesValidation="false"   />
                        </center>

                    </div>
                </div>
            </div>

        </asp:Panel>
        <!-- ModalPopupExtender -->
        
    <%--   --------------------------------------------------------------Popup Local-------------------------------------%>
        <!-- ModalPopupExtender -->
        <asp:Button runat="server" ID="PopTargetID1" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeMRNtemsLocal" runat="server" PopupControlID="PanelMRNItemLocal" TargetControlID="PopTargetID1"
            CancelControlID="btnClose1" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelMRNItemLocal" runat="server" align="center" Style="display: none">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" id="btnClose1" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <center> <h5 id="myModalLabelcrate1">Update MRN Local Items</h5></center>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-2">
                                Category   
                                <asp:HiddenField ID="HF_Cat_ID" runat="server" Value="0" />
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtCategory1" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>

                            <div class="col-md-2">
                                Item 
                                <%--<asp:HiddenField ID="HF_Item_ID" runat="server" Value="0" />--%>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtItem1" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-2">
                                Indent Qty   
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtIndenQty1" onpaste="javascript:return false;" onkeydown="javascript:return false;" autocomplete="off" onkeypress="javascript:return false;" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>

                            <div class="col-md-2">
                                Required Qty   
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server"  ID="txtreqquantity" onkeyup="CalculateItemAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                Price 
                                <%-- <asp:TextBox runat="server" ID="txtPrice"  onpaste="javascript:return false;" onkeydown="javascript:return false;" autocomplete="off" onkeypress="javascript:return false;" CssClass="form-control input-pos-float"></asp:TextBox> --%>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtPrice" onkeyup="CalculateItemAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>

                            </div>
                            <div class="col-md-2">
                                Amount 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtamount" onpaste="javascript:return false;" onkeydown="javascript:return false;" autocomplete="off" onkeypress="javascript:return false;" Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>


                            </div>
                        </div>
                          <div class="row">
                            <div class="col-md-2">
                                Tax % 
                                
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtTaxPrecentLocal" onkeypress="LocalMRNTaxprcent()"  autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>

                            </div>
                            <div class="col-md-2">
                               Tax Amount 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtTaxAmt" onpaste="javascript:return false;" onkeypress="LocalMRNTaxamt()"  autocomplete="off"  Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>


                            </div>
                        </div>
                         <div class="row">
                            <div class="col-md-2">
                                Discount % 
                                
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtDisPrecent" onkeypress="LocalMRNDisprcent()"  autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>

                            </div>
                            <div class="col-md-2">
                               Discount Amount 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtDisAmt" onpaste="javascript:return false;" onkeypress="LocalMRNDisamt()"  autocomplete="off"  Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>


                            </div>
                        </div>

                        <div class="row" runat="server" id="D_Pop_Transportation_Cost">

                            <div class="col-md-2">
                                Transportation Cost
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtItemTransportationCostLocal" Text="0" CssClass="form-control input-pos-float" MaxLength="18" TabIndex="7"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <center>
           <asp:Button ID="btnUpdateLocal" runat="server" Text="Save"  CssClass="btn btn-default" OnClick="btnUpdateLocal_Click"    />
                        <asp:Button ID="btnCancelItemLocal" runat="server" Text="Cancel"  CssClass="btn btn-default" CausesValidation="false"   />
                 </center>

                    </div>
                </div>
            </div>

        </asp:Panel>
        <!-- ModalPopupExtender -->

    <!-- ModalPopupExtender -->
        <asp:Button runat="server" ID="PopTargetID22" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="mpeMRNtemsLocalAddItem" runat="server" PopupControlID="PanelMRNItemLocalAddItem" TargetControlID="PopTargetID22"
            CancelControlID="btnClose3" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelMRNItemLocalAddItem" runat="server" align="center" Style="display: none">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" id="btnClose3" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <center> <h5 id="myModalLabelcrate2">Create MRN Local Items</h5></center>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-2">
                                Category   
                                <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                            </div>
                             <div class="col-md-4">
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"  OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="-Select-" Value="-Select-" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            <div class="col-md-2">
                                Item 
                                <%--<asp:HiddenField ID="HF_Item_ID" runat="server" Value="0" />--%>
                            </div>
                            <div class="col-md-4">
                                    <asp:DropDownList ID="ddlItem" runat="server"  AutoPostBack="true" CssClass="form-control">
                                        <asp:ListItem Text="-Select-" Value="-Select-" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                        </div>

                        <div class="row">
                            <div class="col-md-2">
                                Indent Qty   
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtLocalIndentQty" onpaste="javascript:return false;" onkeydown="javascript:return false;" autocomplete="off" onkeypress="javascript:return false;" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>

                            <div class="col-md-2">
                                Required Qty   
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server"  ID="txtLocalRequiredQty" onkeyup="CalculateItemAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                Price 
                                <%-- <asp:TextBox runat="server" ID="txtPrice"  onpaste="javascript:return false;" onkeydown="javascript:return false;" autocomplete="off" onkeypress="javascript:return false;" CssClass="form-control input-pos-float"></asp:TextBox> --%>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtLocalPrice" onkeyup="CalculateItemAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>

                            </div>
                            <div class="col-md-2">
                                Amount 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtLocalAmount" onpaste="javascript:return false;" onkeydown="javascript:return false;" autocomplete="off" onkeypress="javascript:return false;" Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>


                            </div>
                        </div>
                          <div class="row">
                            <div class="col-md-2">
                                Tax % 
                                
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtLocalTaxPercent" onkeypress="LocalMRNTaxprcent()"  autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>

                            </div>
                            <div class="col-md-2">
                               Tax Amount 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtLocalTaxAmount" onpaste="javascript:return false;" onkeypress="LocalMRNTaxamt()"  autocomplete="off"  Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>


                            </div>
                        </div>
                         <div class="row">
                            <div class="col-md-2">
                                Discount % 
                                
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtLocalDiscountPercent" onkeypress="LocalMRNDisprcent()"  autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>

                            </div>
                            <div class="col-md-2">
                               Discount Amount 
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtLocalDiscountAmount" onpaste="javascript:return false;" onkeypress="LocalMRNDisamt()"  autocomplete="off"  Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>


                            </div>
                        </div>

                        <div class="row" runat="server" id="Div1">

                            <div class="col-md-2">
                                Transportation Cost
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txtLocalTransportationCost" Text="0" CssClass="form-control input-pos-float" MaxLength="18" TabIndex="7"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <center>
           <asp:Button ID="Button2" runat="server" Text="Save"  CssClass="btn btn-default" OnClick="btnUpdateLocal_Click"    />
                        <asp:Button ID="Button3" runat="server" Text="Cancel"  CssClass="btn btn-default" CausesValidation="false"   />
                 </center>

                    </div>
                </div>
            </div>

        </asp:Panel>
        <!-- ModalPopupExtender -->

     <!-- ModalPopupExtender -->
        <asp:Button runat="server" ID="PopTargetID33" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupMRNItemService" runat="server" PopupControlID="PanelMRNItemServiceAddItem" TargetControlID="PopTargetID33"
            CancelControlID="btnClose4" BackgroundCssClass="modalBackground">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="PanelMRNItemServiceAddItem" runat="server" align="center" Style="display: none">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" id="btnClose4" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <center> <h5 id="myModalPanelMRNItemService">Add Service MRN Items</h5></center>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-3">
                                WO Description    
                                <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                            </div>
                             <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtWO_Description" CssClass="form-control" TextMode="MultiLine" autocomplete="off"  ></asp:TextBox>
                            </div>
                            </div>
                             <div class="row">
                             <div class="col-md-3">
                               Net Payable Amount    
                            </div>
                            <div class="col-md-8">
                                <asp:TextBox runat="server" ID="txtNet_Payable_ServiceMRN" onpaste="javascript:return false;"  autocomplete="off"  CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
           <asp:Button ID="btnSaveServiceMRN_Items" runat="server" Text="Save"  CssClass="btn btn-default" OnClick="btnSaveServiceMRN_Items_Click"    />
                        <asp:Button ID="btnCancelServiceMRN_Items" runat="server" Text="Cancel"  CssClass="btn btn-default" CausesValidation="false"   />
                 </center>

                    </div>
                </div>
            </div>

        </asp:Panel>
        <!-- ModalPopupExtender -->
    <script type="text/javascript" >

        function LocalMRNTaxprcent() {
            document.getElementById("<%=txtTaxAmt.ClientID%>").value = '0.00';
        }

        function LocalMRNTaxamt() {
            document.getElementById("<%=txtTaxPrecentLocal.ClientID%>").value = '0.00'
        }

        function LocalMRNDisprcent() {
            document.getElementById("<%=txtDisAmt.ClientID%>").value = '0.00';
        }

        function LocalMRNDisamt() {
            document.getElementById("<%=txtDisPrecent.ClientID%>").value = '0.00'
          }

    </script>

</asp:Content>

