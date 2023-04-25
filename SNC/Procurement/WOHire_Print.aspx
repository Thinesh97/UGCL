<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="~/Procurement/WOHire_Print.aspx.cs" Inherits="WOHire_Print" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .instract {
            font-size: 11px;
            text-align: justify;
            width: 830px;
        }

        .myanux p {
            font-size: 14px;
            margin: 0;
            font-weight: 600;
        }

        .myinst p {
            margin: 0;
        }

        .paddingScopeTable {
            padding-left: 8px;
            padding-right: 5px;
        }

        table#instruct tr td {
            font-size: 14px;
            padding: 10px;
        }

        table#instruct {
            border: none;
        }

        table#instruct1 tr td {
            font-size: 11px;
            padding: 8px;
        }

        table#instruct1 {
            border: none;
        }

        table#ContentPlaceHolder1_GridPrint {
            /*border: 1px 1px solid #fff !important;*/
            border-top: 1px solid #fff;
            border-left: 1px solid #fff;
        }

            table#ContentPlaceHolder1_GridPrint tr th:nth-child(6) {
                border-right: 1px solid #fff;
            }

            table#ContentPlaceHolder1_GridPrint tr td:nth-child(6) {
                border-right: 1px solid #fff;
            }

            table#ContentPlaceHolder1_GridPrint tr td {
                border-right: 1px solid black;
                border-bottom: 1px solid #fff;
            }

        table#ContentPlaceHolder1_PO_GridTax {
            border: 1px solid #fff;
        }

        table#ContentPlaceHolder1_GridPrint {
            margin-top: -2px;
        }

        table#mytbl1 tr th, #mytbl2 tr th, #mytbl3 tr th, #mytbl4 tr th {
            font-size: 14px;
            padding: 0 10px;
        }

        table#mytbl1 tr td, #mytbl2 tr td, #mytbl3 tr td, #mytbl4 tr td {
            font-size: 14px;
            padding: 0 10px;
        }

        invoiceFooter p {
            font-size: 14px;
            font-weight: 600;
        }

        table#mytbl3 tbody td {
            border-bottom: 1px solid #fff;
            padding: 1px;
        }

        table#mytbl1 tr th {
            font-weight: bold;
        }

        td.mysgst p {
            margin: 0;
            padding: 0;
            font-weight: 600;
        }

        @page {
            _size: 8.5in 11in;
            margin-top: 0px;
        }

        @media print {
            table#instruct tr td {
                font-size: 14px;
            }

            table#instruct1 tr td {
                font-size: 12px;
            }

            table#ContentPlaceHolder1_GridPrint {
                border: 1px 1px solid #fff !important;
                border-top: 1px solid #fff !important;
                border-left: 1px solid #fff !important;
            }

                table#ContentPlaceHolder1_GridPrint tr th:nth-child(6) {
                    border-right: 1px solid #fff !important;
                }

            table#ContentPlaceHolder1_PO_GridTax tr td {
                border: 1px solid #fff !important;
            }

            table#ContentPlaceHolder1_GridPrint tr td:nth-child(7) {
                border-right: 1px solid #fff !important;
            }

            table#ContentPlaceHolder1_GridPrint tr td {
                border-bottom: 1px solid #fff !important;
            }

            table#ContentPlaceHolder1_PO_GridTax tr td span {
                border: 1px solid #fff !important;
            }

            table#ContentPlaceHolder1_PO_GridTax {
                border: 1px solid #fff !important;
            }

            table#ContentPlaceHolder1_PO_GridTax {
                border: 1px solid #fff !important;
            }

            table#ContentPlaceHolder1_GridPrint {
                margin-top: -2px !important;
            }

            table#mytbl3 tbody td {
                border-bottom: 1px solid #fff !important;
                padding: 1px !important;
            }

            table#mytbl1 tr th {
                font-weight: normal;
            }
            /*tr {
                page-break-before: always;
            }*/

            .no-print, .no-print * {
                display: none !important;
            }
        }

        @media all {
            .page-break {
                display: none;
            }
        }

        @media print {
            /*table#ContentPlaceHolder1_PO_GridTax {
            border: 1px solid #fff !important;
        }*/
            table#instruct {
                border: none !important;
            }

            table#ContentPlaceHolder1_PO_GridTax {
                border: 1px solid #fff !important;
            }

            table#ContentPlaceHolder1_GridPrint {
                margin-top: -2px !important;
            }

            table#mytbl3 tbody td {
                border-bottom: 1px solid #fff !important;
                padding: 1px !important;
            }

            table#mytbl1 tr th {
                font-weight: normal;
            }

            .page-break {
                /*display: block;*/
                /*page-break-before: always;*/
            }
        }
    </style>

    <style>
        .tbl tr {
            height: 5px;
        }

        #mid, #mid1, #mid2 {
            width: 100%;
            padding: 0;
        }

        .NoMargin {
            margin: 0px;
        }

        .GridCenter th {
            text-align: center;
        }

        .GridCenter2 {
            text-align: right;
        }

        .GridCenter3 {
            text-align: center;
        }
    </style>

    <style>
        @media print {
            tr {
                /*page-break-before: always;*/
            }

            .no-print, .no-print * {
                display: none !important;
            }
        }


        td {
            text-align: justify;
            font-family: Arial;
            font-size: 15px;
            border: none;
            padding-bottom: 5px;
        }

        .firsttd {
            font-weight: bold;
            font-size: 16px;
            vertical-align: top;
            width: 30px;
        }

        .mypageadjust {
            height: 950px;
        }

        .mypageadjusttwo {
            min-height: 880px;
        }

        .mypageadthree {
            min-height: 900px;
        }

        #ContentPlaceHolder1_div_Watermark_Draft {
            opacity: 0.2;
            color: BLACK;
            position: fixed;
            top: 40%;
            left: 15%;
            font-size: 80px;
        }

        #ContentPlaceHolder1_div_Watermark_Cancel {
            opacity: 0.2;
            color: BLACK;
            position: fixed;
            top: 40%;
            left: 30%;
            font-size: 80px;
        }
    </style>


    <div id="divToPrint" runat="server">

        <div id="divTableDataHolder" style="width: 880px">

            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: 1 of 10</b>
            </div>

            <div class="mypageadjust">
                <div runat="server" id="div_Watermark_Draft" visible="false">DRAFT CONFIDENTIAL</div>
                <div runat="server" id="div_Watermark_Cancel" visible="false">Cancelled</div>

                <b>HIRE ORDER</b>

                <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl1">
                    <tr>
                        <td rowspan="4" style="width: 50%"><b><u>Issuer </u></b>
                            <br />
                            <b>United Global Corporation Limited</b>
                            <br />
                            Formerly Known As United Infra Corp. (BLR) Ltd.
                            <br />
                            <asp:Label runat="server" Text="" ID="lblAddressLine1_Company"></asp:Label>
                            <br />
                            <asp:Label runat="server" Text="" ID="lblAddressLine2_Company"></asp:Label>
                            <br />

                            GSTIN :
                            <asp:Label runat="server" Text="" ID="lblGSTIN_Company"></asp:Label><br />
                            TAN :
                            <asp:Label runat="server" Text="" ID="lblTAN_Company"></asp:Label>
                            <br />
                            State Name :
                            <asp:Label runat="server" Text="" ID="lblState_Company"></asp:Label>, Code :
                            <asp:Label runat="server" Text="" ID="lblCode_Company"></asp:Label>
                            <br />
                            Email:
                            <asp:Label runat="server" Text="" ID="lblEmail_Company"></asp:Label>
                        </td>
                        <td style="width: 25%">Hire Order No.<br />
                            <b>
                                <asp:Label runat="server" Text="" ID="lblWONo"></asp:Label></b>
                        </td>
                        <td style="width: 25%">Dated<br />
                            <asp:Label runat="server" Text="" ID="lblWODate"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>Other Reference(s)</td>
                    </tr>
                    <tr>
                        <td>Supplier's quotation No/Date.<br />
                            <%--<asp:Label runat="server" Text="" ID="lblQuotNo"></asp:Label>--%>
                        </td>
                        <td>Destination<br />
                            <asp:Label runat="server" ID="lblDestination"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Despatch through</td>
                        <td style="border-bottom-color: white"></td>
                    </tr>

                    <tr>
                        <%--<td rowspan="4" style="width:50%">
                        Despatch To<br />
                        <b>United Global Corporation Limited</b> <br />
                        Formerly Known As United Infra Corp. (BLR) Ltd. <br />
                        <asp:Label runat="server" Text="" ID="lbldispatchAdvice"></asp:Label> <br />
                        GSTIN : <asp:Label runat="server" Text="" ID="lblGSTIN"></asp:Label><br />
                        State Name : Karnataka, Code : 29
                    </td>--%>
                        <td rowspan="3">
                            <div class="myadddresspart">
                                <b><u>Receiver</u> </b>
                                <br />
                                <b>
                                    <asp:Label runat="server" ID="lblSubcontractorName"></asp:Label></b>
                                <br />
                                <asp:Label ID="lblAddline" runat="server"></asp:Label>,
                            <asp:Label ID="lblcity" runat="server"></asp:Label>
                                -
                            <asp:Label ID="lblPinNo" runat="server"></asp:Label><br />
                                Mob :
                                <asp:Label ID="lblConNo" runat="server"></asp:Label>,<br />
                                GSTIN/UIN :
                                <asp:Label ID="lblSubcontractorGSTNo" runat="server"></asp:Label><br />
                                PAN No:
                                <asp:Label ID="lblPanNo" runat="server"></asp:Label><br />
                                State Name :
                                <asp:Label ID="lblState" runat="server"></asp:Label>, Code :
                                <asp:Label ID="lblStateCode" runat="server"></asp:Label>
                            </div>
                        </td>
                        <td colspan="2">Terms of Delivery &nbsp &nbsp
                        <asp:Label runat="server" ID="lbldelscheduled"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Payment Terms : 
                        <%--<asp:Label runat="server" ID="lblPaymentTerms" Text=""></asp:Label>--%>
                        Payment Terms As Specified in Annexure A
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border-bottom: hidden">Job No : 
                        <b>
                            <asp:Label runat="server" ID="lblJobNo"></asp:Label>&nbsp; - &nbsp</b>
                            <asp:Label runat="server" ID="lblJobDesc"></asp:Label>
                        </td>
                    </tr>

                    <%--<tr>  Extra Fields
                    <td colspan="2">
                        <asp:Label runat="server" ID="lblOtherTerms" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lbindentNo" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lbindentDate" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblQuotationRefDate" Visible="false"></asp:Label>
                    </td>
                </tr>--%>
                </table>

                <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl2">

                    <tr>
                        <td style="padding: 0;">
                            <asp:GridView ID="GridPrint" Width="100%" runat="server" OnRowDataBound="GridPrint_RowDataBound"
                                AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="45px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter3" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of Services" ItemStyle-Width="450px">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblServ" Font-Bold="true" runat="server" Text='<%#Eval("Service_Desc")%>'></asp:Label>--%>
                                            <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("Work_Desc")%>'></asp:Label>--%>
                                            <asp:Label ID="lblItemDesc" Font-Size="13px" runat="server" Text='<%#Eval("Item_Desc")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="Due On">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("Due_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false"/>
                                </asp:TemplateField>--%>

                                    <%--<asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty_required" runat="server" Text='<%#Eval("Qty_required")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false"/>
                                </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Rate" ItemStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" Wrap="false" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UOM" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUom" runat="server" Text='<%#Eval("UOM")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter3" Wrap="false" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter3" Wrap="false" />
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false" HeaderText="Tax Amt">
                                        <ItemTemplate>

                                            <asp:Label ID="lblIgstAmount" runat="server" Text='<%# decimal.Parse(Eval("Igst_Amt").ToString())%>'></asp:Label>
                                            <asp:Label ID="lblCgstAmount" runat="server" Text='<%# decimal.Parse(Eval("Cgst_Amt").ToString())%>'></asp:Label>
                                            <asp:Label ID="lblSgstAmount" runat="server" Text='<%# decimal.Parse(Eval("Sgst_Amt").ToString())%>'></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" />
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" HeaderText="Amount (Rs)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# decimal.Parse(Eval("Total_Amt").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" />
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>

                    <tr runat="server" visible="false">
                        <td>
                            <asp:GridView ID="PO_ItemTaxGrid" Width="100%" runat="server" CssClass="GridCenter" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="50px" Visible="false" HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="50px" HeaderText="Percent">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemTypePerc" runat="server" Text='<%#Eval("TypePerc")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter3" />
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="50px" HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Math.Round(decimal.Parse(Eval("TypeAmt").ToString()))%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" />
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>

                </table>

                <table class="table table-bordered" style="width: 97%; padding: 0; margin-top: -1px;" id="mytbl4">

                    <tr>
                        <td style="text-align: right; width: 496px; font-weight: bold">TAXABLE VALUE
                        </td>
                        <td style="text-align: right" colspan="3">
                            <asp:Label runat="server" ID="lblTaxableAmt"></asp:Label>
                        </td>
                    </tr>

                    <tr runat="server" id="tr_IGST">
                        <td style="text-align: right; font-weight: bold; width: 496px">IGST INPUT @<asp:Label runat="server" ID="lblIgstPerc"></asp:Label>%
                        </td>
                        <td style="text-align: right; width: 100px;">
                            <asp:Label runat="server" ID="lblIgstPerc1"></asp:Label>
                        </td>
                        <td style="text-align: center; width: 60px;">%</td>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblIgstAmt"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_CGST">
                        <td style="text-align: right; font-weight: bold; width: 496px">CGST INPUT @<asp:Label runat="server" ID="lblCgstPerc"></asp:Label>%
                        </td>
                        <td style="text-align: right; width: 100px;">
                            <asp:Label runat="server" ID="lblCgstPerc1"></asp:Label>
                        </td>
                        <td style="text-align: center; width: 60px">%</td>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblCgstAmt"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_SGST">
                        <td style="text-align: right; font-weight: bold">SGST INPUT @<asp:Label runat="server" ID="lblSgstPerc"></asp:Label>%
                        </td>
                        <td style="text-align: right; width: 100px">
                            <asp:Label runat="server" ID="lblSgstPerc1"></asp:Label>
                        </td>
                        <td style="text-align: center;">%</td>
                        <td style="text-align: right">
                            <asp:Label runat="server" ID="lblSgstAmt"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right; font-weight: bold">TOTAL
                        </td>
                        <td style="text-align: right" colspan="3">
                            <asp:Label runat="server" ID="lblAmtAfterTax" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">(-) TDS @ 194C @
                            <asp:Label runat="server" ID="lblTDSPerc"></asp:Label>%
                        </td>
                        <td style="text-align: right" colspan="3">
                            <asp:Label runat="server" ID="lblTDSAmt"></asp:Label>
                        </td>
                    </tr>

                    <tr style="display: none">
                        <td colspan="3" style="padding: 0;">
                            <asp:GridView ID="WO_GridTax" Width="100%" ShowHeader="false" CssClass="GridCenter" runat="server" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="50%" HeaderText="Description of Goods and Services">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPO_Item_Id" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" />
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-Width="50px" HeaderText="Rate %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIndent_No" runat="server" Text='<%#Eval("Type_Perc")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="50px" HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategory_Name" runat="server" Text='<%#decimal.Parse(Eval("Type_Amount").ToString())%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" />
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right; font-weight: bold">NET PAYABLE
                        </td>
                        <td style="text-align: right" colspan="3">
                            <asp:Label runat="server" ID="lblGrandTotal" Font-Bold="true"></asp:Label></td>
                    </tr>

                    <tr>
                        <td colspan="4">Amount Chargeable (in words) : &nbsp;
                        <b>INR
                            <asp:Label runat="server" Text="" ID="lblAmountInWords"></asp:Label></b>
                        </td>
                    </tr>

                    <tr>
                        <td style="border-bottom: 1px solid #fff; border-right: 1px solid #fff;">
                            <p>
                                Terms and Conditions<br />
                                1. Annexure A,B,C,D & E Are Integral Part of This Hire Order
                            </p>
                        </td>
                        <td colspan="5" style="vertical-align: bottom;">
                            <p style="text-align: right; font-weight: 600;">E. & O.E</p>
                        </td>
                    </tr>

                    <tr>
                        <td></td>
                        <td colspan="3" style="text-align: right;">
                            <p><b>For United Global Corporation Limited</b></p>
                            <div style="height: 70px">
                                <asp:Image ID="ImgAuthorisedSign" Height="80" Width="120" Visible="false" BorderColor="White" runat="server" />
                                <%--   <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:220px;"/>--%>
                                <asp:ImageButton ID="imgBtnDigitalSign" Width="150px" Height="80px" runat="server" />
                            </div>
                            <p>Authorised Signatory</p>
                        </td>
                    </tr>

                </table>
            </div>
            <div class="invoiceFooter">
                <p>This is a Computer Generated Document</p>
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
            </div>

        </div>
    </div>

    <div class="page-break"></div>

    <div id="divToPrint1">
        <div id="divTableDataHolder1" style="width: 880px">
            <div style="width: 100%">
                <br />
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: 2 of 10</b>
            </div>

            <div class="mypageadjusttwo">
                <b><u>ANNEXURE-A</u> </b>
                <br />
                <b>COMMERCIAL TERMS and CONDITIONS</b>
                <br />

                <table border="0" class="table table-bordered" style="width: 830px; text-align: justify">
                    <tr>
                        <td style="width: 60px; text-align: center"><b>Sl.No </b></td>
                        <td style="width: 200px; text-align: center"><b>Terms and Conditions </b></td>
                        <td style="text-align: center"><b>Accepted Terms </b></td>
                    </tr>
                    <tr>
                        <td style="text-align: center"><b>1 </b></td>
                        <td><b>Billing Name & Address </b></td>
                        <td>
                            <b>United Global Corporation Limited</b><br />
                            Billing Address: Formerly Known As United Infra Corp. (BLR) Ltd.,<br />
                            <asp:Label runat="server" Text="" ID="lblAddressLine1_Bill"></asp:Label><br />
                            <asp:Label runat="server" Text="" ID="lblAddressLine2_Bill"></asp:Label><br />
                            GSTIN:
                            <asp:Label runat="server" Text="" ID="lblGSTIN_Bill"></asp:Label>, TAN:
                            <asp:Label runat="server" Text="" ID="lblTAN_Bill"></asp:Label>, State Name :
                            <asp:Label runat="server" Text="" ID="lblState_Bill"></asp:Label>,<br />
                            Code :
                            <asp:Label runat="server" Text="" ID="lblCode_Bill"></asp:Label>, E-Mail :
                            <asp:Label runat="server" Text="" ID="lblEmail_Bill"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: center"><b>2 </b></td>
                        <td><b>Contact Person Details 
                        <br />
                            Name
                        <br />
                            Contact No.
                        </b>
                        </td>
                        <td>
                            <br />
                            <asp:Label runat="server" ID="lblContactName" Text=""></asp:Label>
                            <br />
                            <asp:Label runat="server" ID="lblContactNo" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center"><b>3 </b></td>
                        <td><b>Gst </b></td>
                        <td>
                            <asp:Label runat="server" ID="lblGst"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center"><b>4 </b></td>
                        <td><b>Payment Terms </b></td>
                        <td>
                            <asp:Label runat="server" ID="lblPaymentTerms"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center"><b>5 </b></td>
                        <td><b>Description / Nature of Work </b></td>
                        <td>
                            <asp:Label runat="server" ID="lblDesc"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center"><b>6 </b></td>
                        <td><b>Location of Work </b></td>
                        <td>
                            <asp:Label runat="server" ID="lblWorkLocation"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center"><b>7 </b></td>
                        <td><b>Site Working Conditions </b></td>
                        <td>
                            <asp:Label runat="server" ID="Label2" Text="Good"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center"><b>8 </b></td>
                        <td><b>Other Conditions </b></td>
                        <td>
                            <asp:Label runat="server" ID="lblOtherTerms" Text="Good"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="invoiceSignature" style="width: 90%; font-weight: bold">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign1" Width="150px" Height="80px" runat="server" />
                        <%-- <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                    </div>
                </div>
                <div style="float: left">
                    Receiver's Signature
                </div>
                <div style="text-align: right">
                    Authorised Signatory
                </div>
            </div>

            <div class="invoiceFooter">
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
            </div>

        </div>
    </div>

    <div class="page-break"></div>

    <div id="divToPrint4">
        <div id="divTableDataHolder4" style="width: 880px">
            <div style="width: 100%">
                <br />
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: 3 of 10</b>
            </div>
            <div class="mypageadthree">


                <b><u>ANNEXURE-B</u> </b>
                <br />
                <b>SCOPE OF WORK</b>
                <br />

                <table class="table-bordered" style="width: 830px; margin-bottom: 0px; text-align: justify">
                    <tr>
                        <th style="width: 8%; text-align: center">Sl. No.</th>
                        <th style="text-align: center">Description</th>
                    </tr>
                    <tr>
                        <td style="width: 8%"></td>
                        <td class="paddingScopeTable">Following terms under the Scope of the VENDOR:</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">1</td>
                        <td class="paddingScopeTable">Machinery/Equipment will be Hired on Monthly Basis( 28 Days  15 Hours = 420 Hours) Working 420 Hours per month ( 28 days  15 Hours) amount calculated prorate base.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">2</td>
                        <td class="paddingScopeTable">Rental term shall commence after 3 days of commissioning and proven performance of the Machinery/Equipment.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">3</td>
                        <td class="paddingScopeTable">Monthly rent shall be paid within 45 days of submission of the bill.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">4</td>
                        <td class="paddingScopeTable">All necessary documentation including fitment certificate, latest RTO documents, license of operators/drivers shall be submitted by the vendor. Failing which, the order stands terminated.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">5</td>
                        <td class="paddingScopeTable">Two operators/drivers shall be provided along with the Machinery/Equipment.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">6</td>
                        <td class="paddingScopeTable">The Rental Charges during off season/monsoon/other force majeure period will be decided by UGCL during the start of the period which will be either idle charges or pro rata on usage of Machinery/Equipment.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">7</td>
                        <td class="paddingScopeTable">The Breakdown of the Machinery/Equipment should not be beyond 1 day / month. Beyond which proportionate deduction will be done in the rental payable to vendor.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">8</td>
                        <td class="paddingScopeTable">Mobilization and Demobilization charges shall be borne by the vendor.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">9</td>
                        <td class="paddingScopeTable">Vendor shall attend to any operational issues and periodic maintenance of the Machinery/Equipment.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">10</td>
                        <td class="paddingScopeTable">Any long breakdown and the case arises where UGCL has to get the work executed by hiring alternate machinery/equipment it will at the risk and cost of the vendor.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">11</td>
                        <td class="paddingScopeTable">Vendor shall ensure smooth operation of the Machinery/Equipment and address any mechanical or electrical issues with the Machinery/Equipment.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">12</td>
                        <td class="paddingScopeTable">The Machinery/Equipment is not permitted to be shifted from UGCL site without prior permission and notice period of minimum 3 months, any deviation and negligence in this condition appropriate legal action will be taken on the vendor.</td>
                    </tr>
                    <tr>
                        <td style="text-align: center">13</td>
                        <td class="paddingScopeTable">Any working hours of short working or additional hours will be carried forward to the consequent months.</td>
                    </tr>
                </table>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign2" Width="150px" Height="80px" runat="server" />
                        <%-- <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                    </div>
                </div>
                <div style="float: left">
                    Receiver's Signature
                </div>
                <div style="text-align: right">
                    Authorised Signatory
                </div>
            </div>

            <div class="invoiceFooter">
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
            </div>

        </div>
    </div>

    <div class="page-break"></div>


    <div id="divToPrint2">

        <div id="divTableDataHolder2" style="width: 880px">
            <div style="width: 100%">
                <br />
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: 4 of 10</b>
            </div>
            <b><u>ANNEXURE-C</u> </b>
            <br />
            <b>STANDARD TERMS and CONDITIONS</b>


            <table border="0" style="width: 550px; font-weight: bold; border: 1px solid white; display: none">
                <tr>
                    <td style="width: 35%">GTC WODocument No.</td>
                    <td style="width: 5%">:</td>
                    <td style="width: 60%">
                        <asp:Label runat="server" ID="lblWONo1"></asp:Label></td>
                </tr>
                <tr>
                    <td>Contractor</td>
                    <td>:</td>
                    <td>
                        <asp:Label runat="server" ID="lblContractor"></asp:Label></td>
                </tr>
                <tr>
                    <td>Sub-Contractor</td>
                    <td>:</td>
                    <td>
                        <asp:Label runat="server" ID="lblSubcontractor"></asp:Label></td>
                </tr>
                <tr>
                    <td>Date </td>
                    <td>:</td>
                    <td>
                        <asp:Label runat="server" ID="lblWODate1"></asp:Label></td>
                </tr>
            </table>
            <div class="instract">
                <p>
                    <b>GENERAL:</b><br />
                    These general terms and conditions of Work under this Work Order (W.O) by United Global Corporation Limited ("UGCL") are
                                                    mandatory and binding on the Sub-Contractor for rendering of services. The terms and conditions contained herein
                                                    supersede the terms and conditions offered by Sub-Contractor along with their proposal/ offer.The terms mentioned on the
                                                    W.O will supersede these Standard terms and conditions and Memorandum of Understanding signed between UGCL and Sub
                                                    -Contractor.
                </p>
                <p>
                    <b>PRICE:</b><br />
                    The price mentioned in this W.O is final and binding. Price increases or charges not expressly set out in the W.O shall not
be effective unless agreed to in advance in writing by UGCL.
                </p>
                <p>
                    <b>GOODS AND SERVICES TAX & OTHER TAXES :</b>
                    <br />
                    GST shall be paid at actual, against documentary evidence. UGCL may withhold from all amounts payable to Sub-Contractor
                                                        all applicable withholding taxes and to remit those taxes to the applicable governmental authorities as required by
                                                        applicable laws. The Sub-Contractor or the third party assignee shall be liable for all tax payments as required under
                                                        applicable law and for statutory filings and compliances. Failure to make tax payments resulting in UGCL's inability to claim
                                                        tax credits, Sub-Contractor shall be liable to indemnify UGCL and pay such amounts, penalties, interests and/or any other
                                                        sums accruing to UGCL due to such non-compliance and make equivalent payments to UGCL. Any default by the Sub
                                                        -Contractor or a third party assignee shall be deemed as a default of the Sub-Contractor and may result in the cancellation
                                                        of the W.O and all advance payment received, without any deduction shall be refunded by the Sub-Contractor.
                </p>
                <p>
                    <b>ACCEPTANCE OF W.O.:</b>&nbsp;<br />
                    The W.O and the terms herein shall be deemed to be accepted by the Sub-Contractor upon the receipt of the W.O.or
                                                        advance to his account unless expressly rejected by Sub-Contractor in writing or Sub-Contractor has returned the advance
                </p>
                <p>
                    <b>APPLICATION OF WORKS CONTRACT (PRINCIPAL CONTRACT):</b>&nbsp;<br />
                    This W.O shall be read with the terms and conditions contained under the Tender Documents for execution of the Works as
                                                        specified in Annexure III to the extent applicable. Upon acceptance of this W.O, all the provisions of the scope, duration,
                                                        technical performance and other obligations contained in Tender Document of Works Contract to the extent applicable for
                                                        the Works specified in Annexure III shall also be applied to the Sub-Contractor.
                </p>
                <p>
                    <b>ADVANCE:</b>&nbsp;<br />
                    Advance, if any paid by UGCL in accordance with this W.O shall be adjusted against invoice raised by Sub-Contractor for
                                        payment. Advances shall be disbursed within any date agreed between the Parties. Advance may be paid directly to the Sub
                                        -Contractor or to third parties assignees identified and appointed by the Sub-Contractor. In the event that an Advance is
                                        payable to a third party assignee. Sub-Contractor shall issue a Request Letter requesting for Advance to be paid to a third
                                        party assignee for and on behalf of the Sub-Contractor. Notwithstanding anything contained herein, Sub-Contractor
                                        acknowledges that all Advances paid to third party assignees of Sub-Contractor shall be deemed payments made to Sub
                                        -Contractor under this W.O and Sub-Contractor shall be liable for rendering the services or return of Advance received by it
                                        or by third party assignees instructed by it. In case UGCL issues advance payments to a third party assignee of the Sub
                                        -Contractor, the Sub-Contractor shall simultaneously, issue a written confirmation of receipt of payment by the Sub
                                        -Contractor. Interest on Advance, unless waived shall be payable by the Sub -Contractor in Accordance with terms of the W.O. and shall be stated in the W.O. The Advance shall be utilised solely in connection with
                                        this W.O. and cannot be adjusted, Set off or utilised in any manner other than in connection with this W.O unless expressly
                                        agreed by UGCL  
                </p>
                <p>
                    <b>PAYMENT TERMS:</b>&nbsp;<br />
                    Sub-Contractor will issue all invoices on a timely basis. All invoices delivered by Sub-Contractor must meet UGCL's
                                                        requirements. UGCLwill, subject to adjustments on account of Advance, interest, or other deductions, pay the undisputed
                                                        portion of properly rendered invoices withinthirty (30) days from the invoice date issued by Sub-Contractor or if the
                                                        payment is through LC it would be as per LC terms. UGCL shall have the right to withhold payment of any invoiced amounts
                                                        that are disputed in good faith until the parties reach an agreement with respect to such disputed amounts.  
                </p>
                <p><b>FUNCTIONS, DUTIES AND RESPONSIBILITIES OF THE SUB-CONTRACTOR:</b>   </p>
                <p>
                    i) The Sub-Contractor shall perform all the works as specified in Work Order and may engage Engineers, contract labour
                                    who possesses the required qualification for handling the job for the said purpose<br />
                    ii) The Sub-Contractor shall and hereby agrees and confirms to comply with all the provisions of labour laws and
                                industrial laws in respect of the labour employed thereof.<br />
                    iii) The Sub-Contractor shall also require to ensure Basic safety to its employees and laboureres who is executing the
job and take necessary actions required to ensure safety.
                </p>
                <p style="margin: 0;">
                    Upon the occurrence of any force majeure event, the affected party shall provide the other party with a written notice
elaborating reasonable details concerning the force majeure event and its adverse effect on performance of the W.O within
seven (7) days of its commencement. Upon cessation of or changes in the event or condition constituting force majeure,
the affected party shall again notify the other party within seven (7) days of such cessation or change.
Any delay in or failure of performance of the W.O by either party hereto, shall not constitute defaults by such
party or give rise to any claim for damages to the extent of such delay or failure of performance is caused by an event of
force majeure.
                </p>




            </div>
            <div class="footecls" style="margin-top: 0px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 20px;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign3" Width="150px" Height="80px" runat="server" />
                            <%-- <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                        </div>
                    </div>
                    <div style="float: left">
                        Receiver's Signature
                    </div>
                    <div style="text-align: right">
                        Authorised Signatory
                    </div>
                </div>

                <div class="invoiceFooter">
                    <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>
            <div class="instract">

                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 100%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No: 5 of 10</b>
                </div>
                <p style="margin: 0;">
                    The affected party shall takes all reasonable steps to mitigate the adverse impact of this event to minimize any delay. The
                            time for completion and delivery, and all dates for determining delay damages due to delay, shall be extended for the
                            period in which the force majeure event is effective.
                </p>
                <p>
                    iv) The Sub-Contractor shall apply for and obtain license as provided under contract labour (regulation and Abolition)
                                    Act, 1970 for each contract.<br />
                    v) The Sub-Contractor shall be solely responsible for the payment of wages, including overtime wages to the workmen
                                    and ensure its timely payment thereof.<br />
                    vi) The Sub-Contractor shall ensure compliance with all the Labour legislations of land such as Minimum Wages,
                                    Employees Provident Fund, Employees State Insurance, Workmen’s Compensation, etc.<br />
                    vii) The Sub-Contractor shall indemnify the UGCL in so far as liability incurred by UGCL on account of any default by the
                                    Sub-Contractor or its employees or its labourers.<br />
                    viii) The Sub-Contractor shall ensure the projects executed are of good quality and to the satisfaction of UGCL<br />
                    ix) The Sub-Contractor shall ensure strict adherence to Code of Conduct as prescribed in Annexure V to the W.O<br />
                </p>
                <p><b>FORCE-MAJEURE::</b>&nbsp;<br />
                    Circumstances leading to force majeure shall include:-   </p>
                <p>
                    a. act of terrorism;<br />
                    b. riot, war, invasion, act of foreign enemies, hostilities (whether war be declared or not), civil war, rebellion,
                                revolution, insurrection of military or usurped power;<br />
                    c. ionizing radiation or contamination, radio activity from any nuclear fuel or from any nuclear waste from the
                                combustion of nuclear fuel, radioactive toxic explosive or other hazardous properties of any explosive assembly or
                                nuclear component;<br />
                    d. epidemics, earthquakes, flood, fire, hurricanes, typhoons or other physical natural disaster, including severe
                                weather conditions; and<br />
                    e. freight embargoes, strikes at national or state-wide level or industrial disputes at a national or state-wide level in
                                any country where works are performed and which affect an essential portion of the works but excluding any industrial dispute which is specific to the
                                performance of the works or the W.O.<br />

                </p>
                <p>
                    <b>CONTRADICTIONS:</b>&nbsp;<br />
                    In the event Sub-Contractor notices any contradiction in various documents, Sub-Contractor shall bring the same
immediately to the notice of UGCL for a resolution, within a week of receipt of W.O. Any claim made later shall not be
entertained. Such contradiction/ discrepancy/ inconsistency shall be resolved mutually by the parties failing which, it shall
be resolved through dispute resolution mechanism as provided under this W.O.  
                </p>
                <p>
                    <b>CANCELLATION OF W.O:</b>&nbsp;
                    <br />
                    Without prejudice to any other rights or remedies of UGCL, UGCL shall be entitled to terminate this W.O forthwith upon a
breach of terms of the W.O by Sub-Contractor. Termination of this W.O for breach by Sub-Contractor shall not discharge
Sub-Contractor's obligations underany andall otherW.O’s issued by UGCL to Sub-Contractor, or theSub-Contractor’s
obligations under this W.O including refund or advance payments and surviving provisions confidentiality indemnity, taxes,
liquidated damages, governing laws and dispute resolution.   
                </p>
                <p>
                    <b>RISK PURCHASE:</b>&nbsp;<br />
                    During the execution of project under this W.O under the terms specified, if any delay or non-performance of work is
observed due to reasons attributable to the Sub-Contractor, other than that of Force Majeure conditions or reasons
attributable to UGCL or Principal Contractor, which may cause delay in completion of work, UGCL shall be at liberty to
cancel this W.O totally or partially at any point of time without assigning any reasons, whatsoever, and take alternative
measures at the Sub-Contractor’s risk and cost. For this, only a 3 days’ notice will be given without any further
correspondence.
                </p>
                <p>
                    <b>STEP IN RIGHTS:</b>&nbsp;<br />
                    In case the performance of the Sub-Contractor is not found satisfactory, UGCL reserves the right to step in / partial step in
and carry out the works relating to the W.O at the risk and cost of Sub-Contractor after serving a notice of 7 days.If within
the notice period the Sub-Contractor makes up the deficiencies and proceeds earnestly and expeditiously to meet revised
targets set by UGCL to make up the time lost, UGCL at its discretion may permit the Sub-Contractor to carry on and
proceed with the works. However, if in the assessment and opinion of UGCL, there is no significant improvement and the
works related to the W.O, then UGCL shall have the right to terminate this W.O, take over works and execute the balance
work through any other agency at the risk and cost of the Sub-Contractor.

                </p>
                <p>
                    <b>CONSEQUENTIAL DAMAGES :</b>&nbsp;<br />
                    Sub-Contractor agrees and undertakes that in no event shall UGCL be liable for any incidental, punitive, indirect or
consequential damages whatsoever (including but not omitted titled to damages for loss of profits or confidential or other
information,
for any kind of interruption, for personal injury, for loss of privacy, for failure to meet any duty including of good faith or
of reasonable care, negligence, and any other pecuniary or other loss whatever) arising out of or in any way related to the
use of or inability to use and other inputs provided under this Agreement , the provision of or failure to provide support or
other services, information and related content, or otherwise arising out of the use or otherwise under even in the event of
fault, tort (including negligence), misrepresentation, strict or product liability , breach of contract or breach of warranty of
UGCL or any person acting on behalf of UGCL and even if UGCL or Person acting on behalf of UGCL has been advised of the
possibility of such damages.   
                </p>


            </div>
            <div class="footecls" style="margin-top: 130px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 40px;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign4" Width="150px" Height="80px" runat="server" />
                            <%--   <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                        </div>
                    </div>
                    <div style="float: left">
                        Receiver's Signature
                    </div>
                    <div style="text-align: right">
                        Authorised Signatory
                    </div>
                </div>
                <div class="invoiceFooter">
                    <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
                </div>


            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No: 6 of 10</b>
                </div>
                <p>
                    <b>DEFECT LIABILITY PERIOD: </b>&nbsp;<br />
                    Sub-Contractor agrees and undertakes that in no event shall UGCL be liable for any incidental, punitive, indirect or
consequential damages whatsoever (including but not omitted titled to damages for loss of profits or confidential or other
information,
for any kind of interruption, for personal injury, for loss of privacy, for failure to meet any duty including of good faith or
of reasonable care, negligence, and any other pecuniary or other loss whatever) arising out of or in any way related to the
use of or inability to use and other inputs provided under this Agreement , the provision of or failure to provide support or
other services, information and related content, or otherwise arising out of the use or otherwise under even in the event of
fault, tort (including negligence), misrepresentation, strict or product liability , breach of contract or breach of warranty of
UGCL or any person acting on behalf of UGCL and even if UGCL or Person acting on behalf of UGCL has been advised of the
possibility of such damages.
                </p>
                <p>
                    <b>ASSIGNMENT </b>&nbsp;<br />
                    Sub-Contractor may not assign or subcontract the W.O placed under this W.O in whole or in part, without UGCL's prior
written consent.
                </p>
                <p>
                    <b>CONFIDENTIALITY: </b>&nbsp;<br />
                    Sub-Contractor undertakes that it shall, at all times, maintain confidentiality of all the information including but not
limited to drawings and other technical specifications received by it in respect of the W.O and/or disclosed to it by UGCL or
its client and shall not disclose or divulge the same or any part thereof to any third party without the prior written consent
of UGCL. The obligations of this clause shall survive termination of this W.O, regardless of the reasons for termination of
this W.O.
                </p>
                <p><b>ANTI-BRIBERY, CORRUPTION AND MONEY LAUNDERING:: </b></p>
                <p>
                    <b>1)</b> The Sub-Contractor undertakes to UGCL that:<br />
                    i) it will fully comply with, and will procure that all Staff and its Sub-Contractors fully comply with the Anti-Corruption
Laws;<br />
                    ii) it will not do, or omit to do, any act that will cause UGCL to be in breach of the Anti-Corruption Laws;<br />
                    iii) it has in place, and shall maintain in place throughout the term of this Agreement, polices and procedures to ensure
compliance with the the Anti-Corruption Laws and will enforce them where appropriate. At UGCL's request, the Sub
-Contractor will disclose such policies and procedures to UGCL;<br />
                    iv) it will make it clear to those providing services for the Sub-Contractor, including the Staff and other Sub
-Contractors, that the Sub-Contractor does not accept or condone the payment of bribes (including facilitation
payments) on the Sub-Contractor's behalf;<br />
                    v) it will promptly report to UGCL any request or demand for any undue financial or other advantage of any kind
received by the Sub-Contractor in connection with the performance of this W.O;<br />
                    vi) neither the Sub-Contractor nor any of its subsidiaries is or has at any time engaged in any activity, practice or
conduct that would constitute an offense under applicable anti-corruption legislation;<br />
                    vii) no employee or third party of the Sub-Contractor or of any of the subsidiaries has bribed another person intending
to obtain or retain business or an advantage for the Sub-Contractor and/or any of the subsidiaries, and the Sub
-Contractor and each of the subsidiaries has in place adequate procedures designed to prevent their employees or any
third party from undertaking any such conduct;<br />
                    <b>2)</b> The Sub-Contractor shall not offer, give, receive or solicit (and, if an entity, shall cause its personnel not to offer, give,
receive or solicit), directly or indirectly, money or anything of value to or from:<br />
                    i) any third party to influence their actions or functions improperly or to otherwise gain an unfair advantage;<br />
                    ii) any of UGCL 's employees, managers, partners or other personnel in connection with the performance of the Services
to influence their actions or functions improperly or to otherwise gain an unfair advantage;<br />
                    iii) any Government Official. "Government Official" means any Indian or foreign government official or employee (
including employees of a government corporation or public international organization), any political party, candidate
for public office, judicial officer and any Indian public servant (as defined in the Prevention of Corruption Act, 1988).
                </p>
                <p>
                    <b>3)</b>The Sub-Contractor hereby warrants and represents that to the best of its knowledge, following due enquiry, neither the
Sub-Contractor nor any of its Associated Person, wherever applicable, ("Associated Person" in relation to the Sub-Contractor
shall mean a person who performs or has performed services for or on behalf of the Sub-Contractor), has been the subject
of any investigation, inquiry or enforcement proceedings by any governmental, administrative or regulatory body, regarding
any offence or alleged offence under the Anti-Corruption Laws and no such investigation, inquiry or proceedings are
pending or have been threatened and there are no circumstances likely to give rise to any such investigation, inquiry or
proceedings.
                </p>
                <p>
                    <b>4)</b>The Sub-Contractor shall create and maintain precise and accurate books and financial records in connection with the
Services the Sub-Contractor performs under this W.O, and shall retain such books and records for a period of ten (10)
years after termination of such Services. Upon reasonable request, UGCL shall have the right to inspect Sub-Contractor's
books and financial records to determine the Sub-Contractor's compliance with this Clause. Sub-Contractor will fully
cooperate with any such inspection that may be conducted.
                </p>

            </div>
            <div class="footecls" style="margin-top: 150px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 0px;">
                    <div style="height: 60px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign5" Width="150px" Height="80px" runat="server" />
                            <%--<img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                        </div>
                    </div>
                    <div style="float: left">
                        Receiver's Signature
                    </div>
                    <div style="text-align: right">
                        Authorised Signatory
                    </div>
                </div>

                <div class="invoiceFooter">
                    <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No: 7 of 10</b>
                </div>
                <p>
                    5)</b>Sub-Contractor agrees that UGCL may disclose the terms of this W.O, including the Sub-Contractor's identity and the
                        payment terms, to any third party who, in UGCL's judgment, has a legitimate need to know, including Government
                        agencies.
                </p>
                <p>
                    <b>6)</b>The Sub-Contractor shall indemnify, keep indemnified and hold harmless UGCL, the UGCL Member Firm and its and
their partners, directors, officers, employees, consultants and agents from and against all losses, damages, costs (
including but not limited to legal costs and disbursements) arising from or incurred by reason of the Sub-Contractor's, or
any Staff or any Sub-Contractor's breach of the Anti-Corruption Laws<br />
                    <b>7)</b>None of the fees paid pursuant to this W.O will be paid, directly or indirectly, to any of UGCL's employees, managers,
partners or other personnel or a Government Official. 
                </p>
                <p>
                    <b>LIABILITY of SUB-CONTRACTOR : </b>&nbsp;<br />
                    Sub-Contractor shall be liable for all losses resulting to UGCL from non-compliance or breach of any terms of the W.O,
including any loss of profits, to the maximum extent permissible under law. Notwithstanding anything,no limitation or
exclusion of liability shall apply with respect to any claims based on this W.O arising out of the Sub-Contractor's willful
misconduct or gross negligence or with respect to any claims for personal injury or property damage, or to Sub-Contractor's
indemnification obligations stated herein.
                </p>
                <p>
                    <b>GOVERNING LAW & DISPUTE RESOLUTION:: </b>&nbsp; 
                    <br />
                    In case any dispute/s or difference/s arises between the Parties in connection with any matter relating to this W.O
                                including termination thereof then the Parties shall, refer the dispute to arbitration and shall be governed by the provisions
                                of the Arbitration & Conciliation Act, 1996. The arbitration proceedings shall be adjudicated by a sole arbitrator appointed
                                by mutual consent of both the Parties, and the arbitration proceedings shall be held in Bangalore. The language of
                                arbitration shall be English. The decision of the arbitrator shall be final and binding upon the Parties.
                </p>
                <p>
                    <b>CODES & STANDARD: </b>
                    <br />
                    All costs and expenses incurred in connection with W.O. including costs incurred in recovering the advance payments shall
be to the Sub-Contractor's account. 
                </p>
                <p>
                    <b>NON-SOLICITATION: </b>
                    <br />
                    Eighteen (18) months from the date of completion of Project executed, the Sub-Contractor shall not solicit, directly or
indirectly, any employee of UGCL or UGCL Member Firm or customers of UGCL, who was involved in the receipt of the
Services.
                </p>
                <p>
                    <b>SEVERABILITY: </b>
                    <br />
                    If any provision of this W.O or any part of any provision is determined to be partially void or unenforceable by any court or
body of competent jurisdiction or by virtue of any legislation to which it is subject or by virtue of any other reason
whatsoever, it shall be void or unenforceable to that extent only and the validity and enforceability of any of the other
provisions or the remainder of any such provision shall not to be affected. If any clause is rendered void or unenforceable,
whether wholly or in part, the Parties shall endeavour, without delay, to attain the economic and/or other intended result
in another legally permissible manner. 
                </p>
                <p>
                    <b>WORK ORDER SPLITUP: </b>
                    <br />
                    At any point of time during the execution of the order UGCL, with prior consent of Supplier, can split up the order and
place some part of the order from its Group Companies/Associates but will ensure the total order value to the supplier from
UGCL and its associates will not vary from this order
                </p>


            </div>
            <div class="footecls" style="margin-top: 380px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 0;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign6" Width="150px" Height="80px" runat="server" />
                            <%--  <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                        </div>
                    </div>
                    <div style="float: left">
                        Receiver's Signature
                    </div>
                    <div style="text-align: right">
                        Authorised Signatory
                    </div>
                </div>

                <div class="invoiceFooter">
                    <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No: 8 of 10</b>
                </div>
                <div class="myanux" style="text-align: center">
                    <p><u>ANNEXURE-D</u> </p>
                    <p>Sub-Contractor Code of Conduct</p>

                </div>
                <div class="pointscls">
                    <p>
                        <b>BACKGROUND: </b>
                        <br />
                        Sub-Contractor’s performance and adherence to high business standards is an important and integral part of the value
                        chain for UGCL. UGCL promotes and expects the application of high legal, ethical, environmental and employee related
                        standards within our own business and among our Sub-Contractors.
                    </p>
                    <p><b>COMPLIANCE WITH LAWS :
                        <br />
                    </b>The Subcontractor  shall (i) comply with all applicable safety regulations ; (ii) take care for the safety of all persons entitled to be on the Site ; (iii) use reasonable efforts to keep the Site and Works clear of unnecessary obstruction so as to avoid danger to these persons ; (iii) provide fencing, lighting, guarding and watching of the Works until completion and taking over ; (iv) provide any Temporary Works (including roadways, footways, guards and fences) which may be necessary, because of the execution of the Works, for the use and protection  of  the  public and of owners and occupiers of adjacent land and (v) strictly comply with the Employers Agreement  and  Contractor’s manual and instructions for ‘Safety, Health and Environment’.   </p>
                    <p><b>SUB-CONTRACTOR SHALL COMPLY FULLY WITH ALL LAWS AND REGULATIONS APPLICABLE TO IT.</b> </p>
                    <p><b>ENVIRONMENT</b></p>
                    <p>
                        i. UGCL expects Sub-Contractor to demonstrate a clear understanding of the environmental risks, impacts and
                               responsibilities associated with the products and services they provide.<br />
                        ii. Sub-Contractor should have in place an effective environmental policy, statement or program to mitigate these
                            risks, the implementation of which should be evident throughout all levels of the company.<br />
                        iii. Sub-Contractor should have processes in place to ensure its operations conform to all national and other
                            applicable environmental legislation. All required environmental permits, approvals and registrations are to be
                            obtained, maintained and complied with in accordance with the conditions and requirements defined therein.<br />
                        iv. Environmental performance should be measured, monitored and reviewed regularly. Sub-Contractor should
                            endeavour to make continuous improvements in environmental performance through practicable measures and
                            employ best practice where possible.
                            v. Sub-Contractor should make practical efforts to minimise the use of energy, water and raw materials. Where
                                possible, these should be renewable or sustainably sourced. Emissions to air likely to cause pollution or contribute to
                                climate change should be monitored, controlled and minimised where possible.<br />
                        vi. Sub-Contractor shall make practical efforts to eliminate or reduce levels of waste generated and should re-use
                                and recycle waste materials wherever possible. The handling, storage, movement, treatment and disposal of all
                                waste must be carried out in accordance with applicable regulation and in an environmentally responsible manner.<br />
                        vii. Sub-Contractor should consider the environmental credentials and performance of vendors within their own
                                supply chain and require them to operate to a minimum set of standards.<br />
                        viii. Products and services provided to UGCL should include options that offer reduced environmental impact by
                                utilising environmentally sound technologies, processes and sustainable materials etc.<br />
                    </p>
                    <p>
                        <b>HUMAN RIGHTS: </b>
                        <br />
                        UGCL expects Sub-Contractor, and its subcontractors, to respect the rights of its employees and to comply with all
                        relevant legislation, regulations and directives in the country in which it operates. This should include wages, benefits
                        and working conditions, exploitation of child labour (under 14 years of age) or of any other vulnerable group (e.g. illegal
                        immigrants) is totally unacceptable to UGCL.
                    </p>
                    <p>
                        <b>DIVERSITY AND INCLUSIVENESS: </b>
                        <br />
                        Our sourcing decisions, contracts, and management of Sub-Contractor relationship will reflect and promote the
                        principles of the UGCL’s Diversity and Inclusiveness policy (incorporating equal opportunities) in that they will seek to
                        ensure that Sub-Contractor does not victimise, harass or discriminate against any employee or party to the contract due
                        to their sex, gender reassignment, marital or civil partnership status, race, ethnic or national origin, disability, religion,
                        sexual orientation,age or part time status. Sub-Contractor will be required to meet the requirements of any applicable discrimination
                        legislation. Sub-Contractor will be treated fairly and equally during the tendering and purchasing process, with decisions
                        being made on the basis of clear selection criteria.
                    </p>

                </div>


            </div>
            <div class="footecls" style="margin-top: 200px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 0;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign7" Width="150px" Height="80px" runat="server" />
                            <%-- <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                        </div>
                    </div>
                    <div style="float: left">
                        Receiver's Signature
                    </div>
                    <div style="text-align: right">
                        Authorised Signatory
                    </div>
                </div>

                <div class="invoiceFooter">
                    <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No: 9 of 10</b>
                </div>
                <div class="pointscls">
                    <p>
                        <b>HEALTH & SAFETY: </b>
                        <br />
                        Sub-Contractor must ensure that it and/or its Sub-Contractors abide by all local laws, directives and regulations relating
to health and safety in the workplace or in any other location other than the workplace where production or work is
undertaken and that it implements any amendments to these laws, directives or regulations.
                    </p>
                    <p>
                        <b>ETHICS: </b>
                        <br />
                        The highest standards of integrity are expected in all our business dealings. Any and all forms of corruption, extortion,
bribery and embezzlement are strictly prohibited and may/will result in immediate termination and legal action.
Community engagement is encouraged to help foster social development.
                    </p>
                    <p>
                        <b>MONITORING: </b>
                        <br />
                        UGCL expects that Sub-Contractor will actively audit and monitor its day to day management process to ensure
compliance with this Code of Conduct.
                        <b>INSPECTION &TESTING: </b>The Client/Contractor’s representatives or Engineer-In-Charge shall have full power and authority to inspect the works at any time wherever in progress on site and Sub-Contractor shall afford or procure for Employer/Contractor’s representatives or Engineer-In-Charge every facility and assistance to carry out such inspection. Testing of equipment, tools, tackles, materials, etc. or preparation of mock ups or trial patches etc. shall be performed by the Subcontractor at its own costs as per instructions of and to the satisfaction of Contractor’s Authorized Representative or Engineer.
                    </p>
                </div>
            </div>
            <div class="footecls" style="margin-top: 700px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign8" Width="150px" Height="80px" runat="server" />
                            <%--  <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                        </div>
                    </div>
                    <div style="float: left">
                        Receiver's Signature
                    </div>
                    <div style="text-align: right">
                        Authorised Signatory
                    </div>
                </div>

                <div class="invoiceFooter">
                    <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
                </div>
            </div>

            <table border="0" style="width: 830px; text-align: justify; display: none" id="instruct1">
                <tr style="display: none;">
                    <td colspan="2">
                        <b>United Global Corporation Ltd</b> has been entrusted  the  work  “<asp:Label runat="server" ID="lblProjectName"></asp:Label>” by  the United Global Corporation Ltd.
                        <br />
                        <br />
                        These General Terms and Conditions are applicable to all Work Orders issued by <b>M/s. United Global Corporation Ltd.</b> to <b>
                            <asp:Label runat="server" ID="lblSubcontractor1"></asp:Label>
                        </b>during  the tenure of the project and thereafter till defect liability period or O&M handover as applicable from  the  date  of  these  General  Terms  and  Conditions  stated  hereinabove  and shall be read in conjunction with the said Work Order, the Special Conditions, Specifications, drawings and/or any other document attached to or enclosed with the Work Order and shall form an integral and binding part of the Work Order placed on the Subcontractor.
                        <br />
                        <br />
                        Each Work Order, and other documents included therein or attached or enclosed with Work Order, together with terms of these general terms and condition of WorkOrder,constitutes a separate Work Order Contract (hereinafter referred to as ‘Work Order Contract’) that will be effective upon issue of   Work Order to the Sub - Contractor with respect to performance of work as described in the Work Order.
                        <br />
                        <br />
                        Each Work Order shall be governed by theterms of these General Terms and Conditions of Work Order. In the event of any conflict, discrepancy, inconsistency or ambiguity between the terms of these General Terms and Conditions of Work Order and any other documents, than the priority shall be as follows: (1) Employer Work Order& terms; (2) Other enclosure or Annexure or attachment to Work Order; and (3) These General Terms and Conditions of Work Order.
                        <br />
                        <br />
                        The Subcontractor shall return the copy of these General Terms &Conditions of Work Order duly accepted without any deviations for further processing at Contractor’s end. The General Terms and conditions of Work Order would be deemed to be accepted if signed and stamped acceptance/acknowledgement copy is not received within 4 working days the Subcontractor accepts the Work order or receives payments. Failure to return these General Terms & Conditions / Contract doesn't diminish Subcontractor’s responsibility of executing the services required as per the Work Order Contract.
                        <br />
                        <br />
                        Contractor and Subcontractor shall hereinafter individually referred to as Party and collectively referred to as Parties.
                        <br />
                    </td>
                </tr>


                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <div class="mypageadthree" style="min-height: 915px;">
                            <br />
                            <br />
                        </div>
                    </td>
                </tr>

            </table>

        </div>
    </div>

    <div class="page-break"></div>


    <div id="divToPrint3">
        <div id="divTableDataHolder6" style="width: 880px">
            <div style="width: 100%">
                <br />
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: 10 of 10</b>
            </div>
            <b><u>ANNEXURE-E</u> </b>
            <br />
            <b>Format for PF/ESI Declaration</b>
            <br />
            <br />

            <table border="0" style="width: 830px; text-align: justify;" id="instruct">
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>TO WHOM SO EVER IT MAY CONCERN</b><br />
                        <br />
                        This is to confirm that all the statutory payments including PF, ESI, overtime and all other statutory payments that may
be imposed by various Government enactments from time to time to be made to an employee deployed by us and our
associated contractors at UGCL or UGCL’s Customer location executed hire Work Order Number <span>
    <asp:Label runat="server" Text="" ID="lblWONo2"></asp:Label></span> dated <span>
        <asp:Label runat="server" Text="" ID="lblWODate2"></asp:Label></span> is been
remitted and complied with as applicable.We also confirm that the abovementioned compliance and payments of all
statutory dues has been complied with by us with respect to the employees who are deployed by us. The list of
employees is attached herewith.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>Kind Regards</b><br />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>For Sub-Contractor
                        <br />
                        <br />
                        <br />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>Authorized Signatory
                        <br />
                        <br />
                        <br />
                        &nbsp; 
                    </td>
                </tr>

            </table>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />

            <div class="invoiceSignature" style="width: 90%; font-weight: bold; margin-top: 35px;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign9" Width="150px" Height="80px" runat="server" />
                        <%--   <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                    </div>
                </div>
                <div style="float: left">
                    Receiver's Signature
                </div>
                <div style="text-align: right">
                    Authorised Signatory
                </div>
            </div>

            <div class="invoiceFooter">
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
            </div>

        </div>
    </div>


    <script src="../Style/js/jquery-1.11.1.min.js"></script>
    <script src="../Style/js/FileSaver.js"></script>
    <script src="../Style/js/jquery.wordexport.js"></script>

    <script type="text/javascript">
        jQuery(document).ready(function ($) {
            $("a.word-export").click(function (event) {
                $("#divToPrint").wordExport("Purchase Order");
            });
        });
    </script>
    <script type="text/javascript">
        var tableToExcel = (function () {

            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()
    </script>
    <script type="text/javascript">
        function test() {
            debugger;
            window.open('data:application/vnd.ms-excel,' + encodeURIComponent($('div[id$=divToPrint]').html()));
            e.preventDefault();
        }
    </script>
    <script type="text/javascript">
        function PrintDiv() {
            var divToPrint = document.getElementById('divToPrint');
            var popupWin = window.open('', '_blank', 'width=1000,height=1000');
            popupWin.document.open();
            popupWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }
    </script>

</asp:Content>
