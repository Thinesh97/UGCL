<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Procurement/Quotation.aspx.cs" Inherits="Quotation" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <script src="../Style/js/jquery-1.11.1.min.js"></script>
     <script src="../Style/js/jquery.limitkeypress.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });

       

    function beforedelete() {
            if (confirm("Are you sure want to delete this record?") == false) {
                return false;
            }
            return true;
        }
        function CalculateItemTaxAmt() {

            var AmtItemTax = document.getElementById("<%=txtAmtWithTax.ClientID%>").value;
            var Percent = document.getElementById("<%=txtTaxDiscountPercent1.ClientID%>").value;
            var AmtPercent = parseFloat(parseFloat(AmtItemTax) * parseFloat(Percent)) / 100;

            document.getElementById("<%=txtTaxDiscountAmount1.ClientID%>").value = AmtPercent.toFixed(2);

            document.getElementById("<%=txtTaxDiscountAmount1.ClientID%>").value = "0.00";
        }

        function CalculateTaxAmt() {

            var TotalAmt = document.getElementById("<%=txtNetTotal.ClientID%>").value;
            var Percent = document.getElementById("<%=txtTaxDiscountPercent.ClientID%>").value;
            var AmtPercent = parseFloat(parseFloat(TotalAmt) * parseFloat(Percent)) / 100;

            document.getElementById("<%=txtTaxDiscountAmount.ClientID%>").value = AmtPercent.toFixed(2);

            document.getElementById("<%=txtTaxDiscountAmount.ClientID%>").value = "0.00";
        }
        function CalculateItemAmt() {
            var Qty = document.getElementById("<%=txtQty.ClientID%>").value;
            var Price = document.getElementById("<%=txtPrice.ClientID%>").value;
            var Amount = parseFloat(Qty) * parseFloat(Price);

            document.getElementById("<%=txtAmount.ClientID%>").value = Amount.toFixed(2);


        }

        function DirectEnter() {
            document.getElementById("<%=txtTaxDiscountPercent1.ClientID%>").value = 0;
            document.getElementById("<%=txtTaxDiscountPercent.ClientID%>").value = 0;

            document.getElementById("<%=txtpackingamount.ClientID%>").value = 0;
            document.getElementById("<%=txttransportamount.ClientID%>").value = 0;

        }

        function DirectEnter_Packing_Forward() {
            document.getElementById("<%=txtPacking_Perc.ClientID%>").value = 0;

        }

        function Calculate_Packing_Forward_Amt() {

            var TotalAmt = document.getElementById("<%=txtTotalAmt.ClientID%>").value;
            var Percent = document.getElementById("<%=txtPacking_Perc.ClientID%>").value;
            var AmtPercent = parseFloat(parseFloat(TotalAmt) * parseFloat(Percent)) / 100;
           // document.getElementById("<%=txtpackingamount.ClientID%>").value = AmtPercent.toFixed(2);
        }
    </script>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
    </style>

    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>


    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Quotation

            </h3>

        </div>
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->



            <div class="row">
                <div class="col-md-2">
                    Quotation No&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQuotation" CssClass="Validation_Text" ValidationGroup="ValQuotation" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtQuotation" CssClass="form-control" MaxLength="30" TabIndex="1"></asp:TextBox>

                </div>
                <div class="col-md-2">
                    Quotation Date&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuotationDate" CssClass="Validation_Text" ValidationGroup="ValQuotation" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtQuotationDate" onkeypress="javascript:return false;" onPaste="javascript:return false" CssClass="form-control" runat="server" TabIndex="2"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="calQuotationDate" Format="dd-MM-yyyy" TargetControlID="txtQuotationDate"></ajaxToolkit:CalendarExtender>
                </div>

            </div>


            <div class="row">

                <div class="col-md-2">
                    Vendor Name&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="-Select-" ControlToValidate="ddlVendor" CssClass="Validation_Text" ValidationGroup="ValQuotation" ErrorMessage="*"></asp:RequiredFieldValidator>

                </div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlVendor" CssClass="form-control" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" AutoPostBack="true" TabIndex="3">
                        <asp:ListItem>-Select-</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">Vendor Code</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtVendorCode" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>

                </div>

            </div>
            <div class="row">

                <div class="col-md-2">Sales Employee</div>
                <div class="col-md-4">
                    <asp:TextBox ID="txtSaleEmployee" CssClass="form-control" Enabled="false" runat="server" MaxLength="100"></asp:TextBox>

                </div>
                <div class="col-md-2">Payment Terms</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtPayment" CssClass="form-control" MaxLength="100" TabIndex="4"></asp:TextBox>
                </div>
            </div>


            <div class="row">

                <div class="col-md-2">Delivery </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDelivery" CssClass="form-control" MaxLength="50" TabIndex="5"></asp:TextBox>
                </div>
                <div class="col-md-2">Refer Indent</div>



                <div class="col-md-4">
                    <asp:CheckBox ID="chkReferIndent" runat="server" AutoPostBack="true" OnCheckedChanged="chkReferIndent_CheckedChanged" />
                </div>

            </div>
            <div class="row" visible="false">
                <div class="col-md-2">Remarks </div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtRemarks" CssClass="form-control" MaxLength="500" TextMode="MultiLine" Style="resize: none" TabIndex="6"></asp:TextBox>
                </div>



                <div class="col-md-2" id="trIndentNo" runat="server" visible="false">
                    Indent No&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue="-Select-" ControlToValidate="ddlIndentNo" CssClass="Validation_Text" ValidationGroup="ValQuotation" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-4" id="tdIndentNo" runat="server" visible="false">
                    <asp:DropDownList ID="ddlIndentNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlIndentNo_SelectedIndexChanged" CssClass="form-control">
                        <asp:ListItem Text="-Select-" Value="-Select-"></asp:ListItem>
                    </asp:DropDownList>
                </div>

            </div>

            <div class="row">


                <div class="col-md-2">Specific Quotation</div>



                <div class="col-md-4">
                    <asp:CheckBox ID="chkspecificquotation" runat="server" />
                </div>

            </div>

            <br />
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValQuotation" CssClass="btn btn-default" OnClick="btnSubmit_Click" TabIndex="6"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Clear" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="7"></asp:Button>
                    <asp:Button ID="btnAddItem" Text="Add Item" CssClass="btn btn-default" runat="server" OnClick="btnAddItem_Click" CausesValidation="false" Visible="false" TabIndex="8" />
                    <asp:Button ID="btnAddTax" Text="Add Tax" CssClass="btn btn-default" runat="server" CausesValidation="false" OnClick="btnAddTax_Click" Visible="false" TabIndex="9" />

                </div>

            </div>
            <asp:TextBox ID="txtAmtWithTax" Text="0" runat="server" Style="display: none"></asp:TextBox>
            <asp:TextBox ID="txtTotalAmt" Text="0" runat="server" Style="display: none"></asp:TextBox>
            <asp:TextBox ID="txtNetTotal" Text="0" runat="server" Style="display: none"></asp:TextBox>
            <br />
            <div align="center" id="HideGrid" runat="server" visible="false">
                <ogrid:Grid runat="server" ID="QuotationItemGrid" CallbackMode="false" OnRowCreated="QuotationItemGrid_RowCreated" OnDeleteCommand="QuotationItemGrid_DeleteCommand" ShowColumnsFooter="true" OnRowDataBound="QuotationItemGrid_RowDataBound" AutoGenerateColumns="false" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">
                    <ScrollingSettings ScrollWidth="100%" />
                    <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <Columns>

                        <ogrid:Column DataField="QuotationItemID" HeaderText="QuotationItemID" Visible="false"></ogrid:Column>
                        <ogrid:Column DataField="Category_Name" HeaderText="Category" Width="180px"></ogrid:Column>
                        <ogrid:Column DataField="Item_Name" HeaderText="Item" Width="180px"></ogrid:Column>
                        <ogrid:Column DataField="UOM" HeaderText="UOM" Width="80px"></ogrid:Column>
                        <ogrid:Column DataField="Qty" HeaderText="Qty" Width="80px" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Price" HeaderText="Price" Width="100px" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Amt" HeaderText="Amount" Width="100px" Align="right"></ogrid:Column>
                        <ogrid:Column DataField="Amt_With_Tax" HeaderText="Amount With Tax" Width="140px" Align="right"></ogrid:Column>
                        <ogrid:Column HeaderText="Edit" runat="server" Width="80px">
                            <TemplateSettings TemplateId="QuotationItemGridTemplate" />
                        </ogrid:Column>
                        <ogrid:Column HeaderText="Add Item Tax" runat="server" Width="130px">
                            <TemplateSettings TemplateId="ItemTaxTemplate" />
                        </ogrid:Column>
                        <ogrid:Column AllowDelete="true" HeaderText="Delete" Width="100px"></ogrid:Column>
                        <ogrid:Column DataField="NetTotalAmt" HeaderText="Net TotalAmt" Visible="false" Width="100px" Align="right"></ogrid:Column>
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="QuotationItemGridTemplate" runat="server">
                            <Template>
                                <asp:LinkButton ID="lnkQuotationItem" Text="Edit" runat="server" CssClass="gridCB" OnClick="lnkQuotationItem_Click" CommandName='<%#Container.DataItem["QuotationItemID"] %>'>
                                </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                    <Templates>
                        <ogrid:GridTemplate ID="ItemTaxTemplate" runat="server">
                            <Template>
                                <asp:LinkButton ID="lnkAddTax" Text="Add Tax" CommandArgument='<%#Container.DataItem["Amt_With_Tax"] %>' CommandName='<%#Container.DataItem["Item_Code"] %>' OnClick="lnkAddTax_Click" runat="server" CssClass="gridCB">
                                </asp:LinkButton>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>


                <br />
                <br />
                <div>
                    <ogrid:Grid ID="GridItemWiseTax" runat="server" CallbackMode="false" OnDeleteCommand="GridItemWiseTax_DeleteCommand" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" AllowAddingRecords="false">
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />

                        <ScrollingSettings ScrollWidth="100%" />
                        <Columns>
                            <ogrid:Column DataField="Item_Name" HeaderText="Item Name" Width="130" ReadOnly="true"></ogrid:Column>
                            <ogrid:Column DataField="Item_Code" HeaderText="Item_Code" Width="150" ReadOnly="true"></ogrid:Column>
                            <ogrid:Column DataField="Type" HeaderText="Type" Width="130"></ogrid:Column>
                            <ogrid:Column DataField="Description" HeaderText="Description" Width="130" ReadOnly="true"></ogrid:Column>
                            <ogrid:Column DataField="Type_Perc" HeaderText="Percentage" Align="right" Width="120" ReadOnly="true"></ogrid:Column>
                            <ogrid:Column DataField="Type_Amount" HeaderText="Amount" Align="right" Width="100" ReadOnly="true"></ogrid:Column>
                            <ogrid:Column AllowDelete="true" HeaderText="Delete" Width="100"></ogrid:Column>
                            <ogrid:Column DataField="Quot_Item_Tax_ID" HeaderText="TAX ID" Visible="false"></ogrid:Column>
                            <ogrid:Column DataField="Quot_ID" HeaderText="Quot ID" Visible="false"></ogrid:Column>
                        </Columns>
                    </ogrid:Grid>


                </div>
                <br />
                <br />
                <div>
                    <ogrid:Grid ID="Grid_Tax" runat="server" ShowColumnsFooter="true" CallbackMode="false" OnRowDataBound="Grid_Tax_RowDataBound" OnDeleteCommand="Grid_Tax_DeleteCommand" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" AllowAddingRecords="false">
                        <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                        <ScrollingSettings ScrollWidth="65%" />
                        <Columns>
                            <ogrid:Column DataField="Type" HeaderText="Type" Width="150"></ogrid:Column>
                            <ogrid:Column DataField="Description" HeaderText="Description" Width="150" ReadOnly="true"></ogrid:Column>
                            <ogrid:Column DataField="Type_Perc" HeaderText="Percentage" Align="right" Width="110" ReadOnly="true"></ogrid:Column>
                            <ogrid:Column DataField="Type_Amount" HeaderText="Amount" Align="right" Width="110" ReadOnly="true"></ogrid:Column>
                            <ogrid:Column AllowDelete="true" HeaderText="Delete" Width="100"></ogrid:Column>
                            <ogrid:Column DataField="Quot_Tax_ID" HeaderText="TAX ID" Visible="false"></ogrid:Column>
                            <ogrid:Column DataField="Quot_ID" HeaderText="Quot ID" Visible="false"></ogrid:Column>
                            <ogrid:Column DataField="NetTotalAmt" HeaderText="Net Total Amt" Visible="false"></ogrid:Column>
                        </Columns>
                    </ogrid:Grid>

                </div>
            </div>


        </div>
    </div>
    <asp:Button ID="btnDummy" runat="server" Style="display: none" Text="Button" />

    <ajaxToolkit:ModalPopupExtender ID="ModelItemPopup" runat="server" PopupControlID="PanelItem" TargetControlID="btnDummy"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="PanelItem" runat="server" align="center" Style="display: none">

        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>   <h5 id="myModalLabelcrate"><asp:Label ID="lblAddItems" runat="server" Text="Add Items"></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-2">
                            Category&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCategory" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Item&nbsp;
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlItem" InitialValue="-Select-" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>

                        </div>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlItem" CssClass="form-control" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>-Select-</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            UOM &nbsp;
                 
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtUOM" CssClass="form-control" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            Qty
      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtQty" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox runat="server" ID="txtQty" onkeyup="CalculateItemAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>


                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Price
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPrice" CssClass="Validation_Text" ValidationGroup="ValAddItem" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox runat="server" ID="txtPrice" onkeyup="CalculateItemAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount
                        </div>
                        <div class="col-md-4">

                            <asp:TextBox runat="server" ID="txtAmount" onkeydown="javascript: return false" onkeypress="javascript: return false" onpaste="javascript: return false" autocomplete="off" Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <center>
                        
           <asp:Button ID="btnSaveItem" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValAddItem" OnClick="btnSaveItem_Click"    />
                        <asp:Button ID="btnCancelItem" runat="server"  Text="Cancel"   CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelItem_Click" />
                 </center>

                </div>
            </div>

        </div>

    </asp:Panel>







    <ajaxToolkit:ModalPopupExtender ID="ModelAddTotalTax" runat="server" PopupControlID="PanelTax" TargetControlID="btnAddTax"
        CancelControlID="BtnClose" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanelTax" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="Button" id="BtnClose" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>   <h5 id="myModalTax"><asp:Label ID="lblTax" runat="server" Text="Add Tax"></asp:Label></h5></center>
                </div>
                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-2">
                            Type&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorforradiobuttons" runat="server" ControlToValidate="rbtntype" CssClass="Validation_Text" ValidationGroup="ValSaveTax" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-10">

                            <asp:RadioButtonList runat="server" ID="rbtntype" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtntype_SelectedIndexChanged">
                                <asp:ListItem Text="Tax" Value="Tax" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Discount" Value="Discount"></asp:ListItem>
                                <asp:ListItem Text="Transport" Value="Transport"></asp:ListItem>
                                <asp:ListItem Text="Packing & Forwarding Cost" Value="PackingForwardingCost"></asp:ListItem>

                            </asp:RadioButtonList>
                        </div>

                    </div>



                    <div class="row" id="percentamount" runat="server">
                        <div class="col-md-2">
                            Percent &nbsp;
                 
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtTaxDiscountPercent" runat="server" onkeyup="CalculateTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount
        <asp:RequiredFieldValidator ID="RequiredFieldValidatorforamount" runat="server" ControlToValidate="txtTaxDiscountAmount" CssClass="Validation_Text" ValidationGroup="ValSaveTax" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtTaxDiscountAmount" onkeypress="DirectEnter();" AutoComplete="off" Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>


                    </div>

                    <div class="row" id="decsription" runat="server">
                        <div class="col-md-2">
                            Description
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorfordescription"  runat="server" ControlToValidate="txtdescription" CssClass="Validation_Text" ValidationGroup="ValSaveTax" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-10">

                            <asp:TextBox runat="server" ID="txtdescription"  CssClass="form-control" Style="resize: none; text-transform:uppercase" TextMode="MultiLine"></asp:TextBox>
                        </div>


                    </div>
                    <div class="row" id="TransportCost" runat="server" visible="false">
                        <div class="col-md-2">
                            Transport Cost
       
                        </div>
                        <div class="col-md-4">
                            <asp:RadioButtonList runat="server" ID="RdbTransportCost" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RdbTransportCost_SelectedIndexChanged">
                                <asp:ListItem Text="Amount" Value="Amount"></asp:ListItem>
                                <asp:ListItem Text="Extra" Value="Extra"></asp:ListItem>


                            </asp:RadioButtonList>
                        </div>
                        <asp:PlaceHolder ID="PHTransport" Visible="false" runat="server">
                            <div class="col-md-2">
                                Amount&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="txttransportamount" CssClass="Validation_Text" ValidationGroup="ValSaveTax" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox runat="server" ID="txttransportamount" onkeypress="DirectEnter();" CssClass="form-control-float"></asp:TextBox>
                            </div>
                        </asp:PlaceHolder>
                    </div>
                    <div class="row" id="packandforwardcost" runat="server" visible="false">
                        <div class="col-md-2">
                            Packing and Forwarding Cost 
       
                        </div>
                        <div class="col-md-4">
                            <asp:RadioButtonList runat="server" ID="RdbPackingCost" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RdbPackingCost_SelectedIndexChanged">
                                <asp:ListItem Text="Amount & Percentage" Value="Amount"></asp:ListItem>
                                <asp:ListItem Text="Extra" Value="Extra"></asp:ListItem>

                            </asp:RadioButtonList>
                        </div>

                        <asp:PlaceHolder ID="phPacking" runat="server" Visible="false">
                            <div class="col-md-2">
                                Amount
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtpackingamount" runat="server" Text="0.00" onkeypress="DirectEnter_Packing_Forward();" onPaste="javascript:return false" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="ph_Packing_Perc" runat="server" Visible="false">
                            <div class="col-md-2">
                                Percent
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtPacking_Perc" runat="server" onkeyup="Calculate_Packing_Forward_Amt();" Text="0.00" onPaste="javascript:return false" autocomplete="off" CssClass="form-control input-pos-float"></asp:TextBox>
                            </div>
                        </asp:PlaceHolder>

                    </div>

                </div>
                <div class="modal-footer">
                    <center>
                        
           <asp:Button ID="btnSaveTax" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValSaveTax" OnClick="btnSaveTax_Click"/>
             <asp:Button ID="btnCancelTax" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false" OnClick="btnCancelTax_Click"   />        
                 </center>

                </div>
            </div>
        </div>
    </asp:Panel>



    <asp:Button runat="server" ID="BtnTarket" Style="display: none"></asp:Button>
    <ajaxToolkit:ModalPopupExtender ID="ModelItemTax" runat="server" PopupControlID="PanelItemTax" TargetControlID="BtnTarket"
        CancelControlID="BtnClose1" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="PanelItemTax" runat="server" align="center" Style="display: none">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="Button" id="BtnClose1" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <center>   <h5 id="myModalTax1"><asp:Label ID="Label1" runat="server" Text="Add Tax"></asp:Label></h5></center>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-2">
                            Type&nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="rbtntype" CssClass="Validation_Text" ValidationGroup="ValSaveTax" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">

                            <asp:RadioButtonList runat="server" ID="rbtntype1" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Tax" Value="Tax" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Discount" Value="Discount"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="row" id="Div1" runat="server">
                        <div class="col-md-2">
                            Percent &nbsp;
                 
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox ID="txtTaxDiscountPercent1" runat="server" onkeyup="CalculateItemTaxAmt()" autocomplete="off" Text="0.00" onPaste="javascript:return false" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            Amount
        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTaxDiscountAmount1" CssClass="Validation_Text" ValidationGroup="ValItemSaveTax" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-4">
                            <asp:TextBox runat="server" ID="txtTaxDiscountAmount1" onkeyup="DirectEnter()" AutoComplete="off" Text="0.00" CssClass="form-control input-pos-float"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            Description
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtdescription1" CssClass="Validation_Text" ValidationGroup="ValItemSaveTax" ErrorMessage="*"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="txtdescription1" CssClass="form-control" Style="resize: none" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <center>
                        
         <asp:Button ID="btnItemTaxSave" runat="server" Text="Save"  CssClass="btn btn-default" ValidationGroup="ValItemSaveTax" OnClick="btnItemTaxSave_Click" />
             <asp:Button ID="Button2" runat="server"  Text="Cancel"  CssClass="btn btn-default" CausesValidation="false"    />        
                 </center>

                </div>
            </div>
        </div>

    </asp:Panel>

</asp:Content>
