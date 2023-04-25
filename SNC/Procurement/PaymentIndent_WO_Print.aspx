<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="PaymentIndent_WO_Print.aspx.cs" Inherits="PaymentIndent_WO_Print" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .instract {
            font-size: 11px;
            text-align: justify;
            width: 830px;
        }

        td {
            text-align: justify;
            font-family: Arial;
            font-size: 15px;
            border: none;
            padding-bottom: 5px;
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

        table#ContentPlaceHolder1_Grid_OtherTerms {
            width: 100%;
            border: none;
        }

        table#mytbl1 tr th, #mytbl2a tr th, #mytbl2b tr th, #ContentPlaceHolder1_mytbl3a tr th, #mytbl3b tr th {
            font-size: 14px !important;
            padding: 0 10px;
        }

        table#mytbl1 tr td, #mytbl2a tr td, #mytbl2b tr td, #ContentPlaceHolder1_mytbl3a tr td, #mytbl3b tr td {
            font-size: 14px !important;
            padding: 0 10px;
        }

        table#mytbl1 tr th {
            font-weight: bold;
        }

        table#mytbl2a tbody td, table#mytbl2b tbody td {
            border: 1px solid;
        }

        table#mytbl2a, table#mytbl2b {
            border: hidden;
        }

        invoiceFooter p {
            font-size: 14px;
            font-weight: 600;
        }

        table#mytbl2a tbody td, table#mytbl2b tbody td {
            border: 1px solid;
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

            table#mytbl1 tr th {
                font-weight: normal;
            }

            tr {
                page-break-before: inherit;
            }

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

            table#instruct {
                border: none !important;
            }

            table#mytbl3 tbody td {
                border-bottom: 1px solid #fff !important;
                padding: 1px !important;
            }

            table#mytbl1 tr th {
                font-weight: normal;
            }

            .page-break {
                display: none;
                /*page-break-before: always;*/
            }
        }
    </style>

    <style>
        .tbl tr {
            height: 5px;
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

        table#ContentPlaceHolder1_GridPrint4 tr td, table#ContentPlaceHolder1_GridPrint3 tr td, table#ContentPlaceHolder1_GridPrint8 tr td, table#ContentPlaceHolder1_GridPrint tr td, table#ContentPlaceHolder1_GridPrint9 tr td, table#ContentPlaceHolder1_GridPrint10 tr td, table#ContentPlaceHolder1_GridPrint11 tr td, table#ContentPlaceHolder1_GridPrint12 tr td, table#ContentPlaceHolder1_GridPrint13 tr td, table#ContentPlaceHolder1_GridPrint14 tr td, table#ContentPlaceHolder1_GridPrint15 tr td, table#ContentPlaceHolder1_GridPrint16 tr td, table#ContentPlaceHolder1_GridPrint17 tr td, table#ContentPlaceHolder1_GridPrint18 tr td, table#ContentPlaceHolder1_GridPrint19 tr td, table#ContentPlaceHolder1_GridPrint20 tr td, table#ContentPlaceHolder1_GridPrint21 tr td, table#ContentPlaceHolder1_GridPrint21 tr td, table#ContentPlaceHolder1_GridPrint22 tr td, table#ContentPlaceHolder1_GridPrint23 tr td, table#ContentPlaceHolder1_GridPrint24 tr td, table#ContentPlaceHolder1_GridPrint25 tr td, table#ContentPlaceHolder1_GridPrint26 tr td {
            border: 1px solid #000;
            text-align: center;
            padding: 3px;
        }

            table#ContentPlaceHolder1_GridPrint4 tr td:first-child, table#ContentPlaceHolder1_GridPrint3:first-child tr td, table#ContentPlaceHolder1_GridPrint8:first-child tr td, table#ContentPlaceHolder1_GridPrint tr td:first-child, table#ContentPlaceHolder1_GridPrint9 tr td:first-child, table#ContentPlaceHolder1_GridPrint10 tr td:first-child, table#ContentPlaceHolder1_GridPrint11 tr td:first-child, table#ContentPlaceHolder1_GridPrint12 tr td:first-child, table#ContentPlaceHolder1_GridPrint13 tr td:first-child, table#ContentPlaceHolder1_GridPrint14 tr td:first-child, table#ContentPlaceHolder1_GridPrint15 tr td:first-child, table#ContentPlaceHolder1_GridPrint16 tr td:first-child, table#ContentPlaceHolder1_GridPrint17 tr td:first-child, table#ContentPlaceHolder1_GridPrint18 tr td:first-child, table#ContentPlaceHolder1_GridPrint19 tr td:first-child, table#ContentPlaceHolder1_GridPrint20 tr td:first-child, table#ContentPlaceHolder1_GridPrint21 tr td:first-child, table#ContentPlaceHolder1_GridPrint21 tr td:first-child, table#ContentPlaceHolder1_GridPrint22 tr td:first-child, table#ContentPlaceHolder1_GridPrint23 tr td:first-child, table#ContentPlaceHolder1_GridPrint24 tr td:first-child, table#ContentPlaceHolder1_GridPrint25 tr td:first-child, table#ContentPlaceHolder1_GridPrint26 tr td:first-child {
                text-align: left;
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

        .firsttd {
            font-weight: bold;
            font-size: 16px;
            vertical-align: top;
            width: 30px;
        }

        .mypageadjust {
            height: 850px;
        }

        .mypageadjustfirst {
            height: 960px;
        }

        .mypageadjusttwo {
            min-height: 890px;
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


    <div id="divToPrint1" runat="server">
        <div id="divTableDataHolder1" style="width: 880px">

            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo1"></asp:Label></b>
            </div>
            <div class="mypageadjustfirst">
                <div runat="server" id="div_Watermark_Draft" visible="false">DRAFT CONFIDENTIAL</div>
                <div runat="server" id="div_Watermark_Cancel" visible="false">Cancelled</div>

                <b>WORK ORDER</b>

                <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl1">
                    <tr>
                        <td rowspan="2" style="width: 50%"><u><b>Issuer : </b></u>
                            <br />
                            <b>United Global Corporation Limited</b>
                            <br />
                            (Formerly United Infra Corp. (BLR) Ltd.)
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
                        <td style="width: 25%">Work Order No.<br />
                            <b>
                                <asp:Label runat="server" Text="" ID="lblWONo"></asp:Label>
                            </b>
                        </td>
                        <td style="width: 25%">Dated<br />
                            <asp:Label runat="server" Text="" ID="lblWODate"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Contractor's Quotation No./  Date<br />
                            <%--<asp:Label runat="server" Text="" ID="lblQuotNo"></asp:Label>--%>
                        </td>
                        <td>Work Location<br />
                            <asp:Label runat="server" ID="lblDestination"></asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                    <td>Despatch through</td>
                    <td style="border-bottom-color:white"></td>
                </tr>--%>

                    <tr>
                        <%--<td rowspan="4" style="width:50%">
                        Despatch To<br />
                        <b>United Global Corporation Limited</b> <br />
                        Formerly Known As United Infra Corp. (BLR) Ltd. <br />
                        <asp:Label runat="server" Text="" ID="lbldispatchAdvice"></asp:Label> <br />
                        GSTIN : <asp:Label runat="server" Text="" ID="lblGSTIN"></asp:Label><br />
                        State Name : Karnataka, Code : 29
                    </td>--%>
                        <td rowspan="2" style="border-bottom: hidden">
                            <div class="myadddresspart">
                                <u><b>Receiver : </b></u>
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
                                PAN No :
                                <asp:Label ID="lblPanNo" runat="server"></asp:Label><br />
                                State Name :
                                <asp:Label ID="lblState" runat="server"></asp:Label>, Code :
                                <asp:Label ID="lblStateCode" runat="server"></asp:Label>
                            </div>
                        </td>
                        <%--<td colspan="2">Terms of Delivery &nbsp &nbsp
                        <asp:Label runat="server" ID="lbldelscheduled"></asp:Label> 
                      </td>  --%>
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
                </table>

                <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl2a">

                    <tr>
                        <td style="padding: 0;">
                            <asp:GridView ID="GridPrint1" Width="100%" runat="server" OnRowDataBound="GridPrint_RowDataBound"
                                AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="45px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter3" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of Services" ItemStyle-Width="470px">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lbl" Font-Bold="true" runat="server" Text='<%#Eval("Service_Desc")%>'></asp:Label>--%>
                                            <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("Work_Desc")%>'></asp:Label>--%>
                                            <asp:Label ID="lblItemDesc" Font-Size="13px" runat="server" Text='<%#Eval("WO_Desc")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="Due On">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("Due_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false"/>
                                </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" Wrap="false" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" Wrap="false" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUom" runat="server" Text='<%#Eval("UOM")%>'></asp:Label>
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

                                    <asp:TemplateField ItemStyle-Width="125px" ItemStyle-HorizontalAlign="Right" HeaderText="Amount (Rs)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Total_Amt")%>'></asp:Label>
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

                <table runat="server" class="table table-bordered" style="width: 97%; padding: 0; margin-top: -1px;" id="mytbl3a">

                    <tr>
                        <td style="text-align: right; width: 515px; font-weight: bold">TAXABLE VALUE
                        </td>
                        <td colspan="2" style="text-align: right">
                            <asp:Label runat="server" ID="lblTaxableAmt"></asp:Label>
                        </td>
                    </tr>



                    <tr runat="server" id="tr_IGST">
                        <td style="text-align: right; font-weight: bold; width: 515px">IGST @ &nbsp;<asp:Label runat="server" ID="lblIgstPerc"></asp:Label>%
                        </td>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblIgstPerc1"></asp:Label>
                            %</td>
                        <td style="text-align: right; width: 125px">
                            <asp:Label runat="server" ID="lblIgstAmt"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_CGST">
                        <td style="text-align: right; font-weight: bold; width: 515px">CGST @ &nbsp;<asp:Label runat="server" ID="lblCgstPerc"></asp:Label>%
                        </td>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblCgstPerc1"></asp:Label>%</td>
                        <td style="text-align: right; width: 125px">
                            <asp:Label runat="server" ID="lblCgstAmt"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_SGST">
                        <td style="text-align: right; font-weight: bold">SGST @ &nbsp;<asp:Label runat="server" ID="lblSgstPerc"></asp:Label>%
                        </td>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblSgstPerc1"></asp:Label>%</td>
                        <td style="text-align: right">
                            <asp:Label runat="server" ID="lblSgstAmt"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right; font-weight: bold">TOTAL
                        </td>
                        <td colspan="2" style="text-align: right">
                            <asp:Label runat="server" ID="lblAmtAfterTax" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">(-) TDS @ 194C @
                            <asp:Label runat="server" ID="lblTDSPerc"></asp:Label>%
                        </td>
                        <td colspan="2" style="text-align: right">
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
                        <td colspan="2" style="text-align: right">
                            <asp:Label runat="server" ID="lblGrandTotal" Font-Bold="true"></asp:Label></td>
                    </tr>

                    <tr>
                        <td colspan="4">Amount Chargeable (in words) : &nbsp;
                        <b>
                            <asp:Label runat="server" Text="" ID="lblAmountInWords"></asp:Label></b>
                        </td>
                    </tr>

                    <tr>
                        <td style="border-bottom: 1px solid #fff; border-right: 1px solid #fff;">
                            <p>
                                Terms and Conditions<br />
                                <asp:Label runat="server" Text="" ID="lblAnnexure"></asp:Label>
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
                                <asp:ImageButton ID="imgBtnDigitalSign" Width="150px" Height="80px" runat="server" />
                                <%-- <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:220px;"/>--%>
                            </div>
                            <p>Authorised Signatory</p>
                        </td>
                    </tr>

                </table>

                <div runat="server" id="div_Continue" style="width: 90%; height: 30px;">
                    <p style="float: right; padding-top: 10px">
                        Continued...&nbsp; &nbsp;
                    </p>
                </div>
            </div>

            <div class="invoiceFooter">
                <p style="margin: 0">This is a Computer Generated Document</p>
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 96%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <div id="divToPrint2" runat="server">
        <div id="divTableDataHolder2" style="width: 880px">
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 100%; height: 110px; padding-top: 30px;">
            </div>
            <%-- <div style="width:40%;">
                <br />
                 <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width:98%; height:110px"/>
            </div>--%>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo2"></asp:Label></b>
            </div>
            <div class="mypageadjustfirst">
                <b>WORK ORDER -
                    <asp:Label runat="server" ID="lblWONo2"></asp:Label></b>

                <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl2b">

                    <tr>
                        <td style="padding: 0;">
                            <asp:GridView ID="GridPrint2" ShowHeader="false" Width="100%" runat="server" OnRowDataBound="GridPrint_RowDataBound"
                                AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="45px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter3" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description of Services" ItemStyle-Width="500px">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblItemDesc" Font-Bold="true" runat="server" Text='<%#Eval("Service_Desc")%>'></asp:Label>--%>
                                            <%--<asp:Label ID="Label1" runat="server" Text='<%#Eval("Work_Desc")%>'></asp:Label>--%>
                                            <asp:Label ID="lblItemDesc" Font-Size="13px" runat="server" Text='<%#Eval("WO_Desc")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="Due On">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("Due_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false"/>
                                </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" Wrap="false" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Rate")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" Wrap="false" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUom" runat="server" Text='<%#Eval("UOM")%>'></asp:Label>
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

                                    <asp:TemplateField ItemStyle-Width="125px" ItemStyle-HorizontalAlign="Right" HeaderText="Amount (Rs)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Total_Amt")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" />
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </td>
                    </tr>

                </table>

                <table class="table table-bordered" style="width: 97%; padding: 0; margin-top: -1px;" id="mytbl3b">

                    <tr>
                        <td style="text-align: right; width: 515px; font-weight: bold">TAXABLE VALUE
                        </td>
                        <td colspan="2" style="text-align: right">
                            <asp:Label runat="server" ID="lblTaxableAmt_Page2"></asp:Label>
                        </td>
                    </tr>



                    <tr runat="server" id="tr_IGST_Page2">
                        <td style="text-align: right; font-weight: bold; width: 515px">IGST @ &nbsp;<asp:Label runat="server" ID="lblIgstPerc_Page2"></asp:Label>%
                        </td>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblIgstPerc1_Page2"></asp:Label>
                            %</td>
                        <td style="text-align: right; width: 125px">
                            <asp:Label runat="server" ID="lblIgstAmt_Page2"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_CGST_Page2">
                        <td style="text-align: right; font-weight: bold; width: 515px">CGST @ &nbsp;<asp:Label runat="server" ID="lblCgstPerc_Page2"></asp:Label>%
                        </td>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblCgstPerc1_Page2"></asp:Label>%</td>
                        <td style="text-align: right; width: 125px">
                            <asp:Label runat="server" ID="lblCgstAmt_Page2"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_SGST_Page2">
                        <td style="text-align: right; font-weight: bold">SGST @ &nbsp;<asp:Label runat="server" ID="lblSgstPerc_Page2"></asp:Label>%
                        </td>
                        <td style="text-align: right;">
                            <asp:Label runat="server" ID="lblSgstPerc1_Page2"></asp:Label>%</td>
                        <td style="text-align: right">
                            <asp:Label runat="server" ID="lblSgstAmt_Page2"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right; font-weight: bold">TOTAL
                        </td>
                        <td colspan="2" style="text-align: right">
                            <asp:Label runat="server" ID="lblAmtAfterTax_Page2" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">(-) TDS @ 194C @
                            <asp:Label runat="server" ID="lblTDSPerc_Page2"></asp:Label>
                        </td>
                        <td colspan="2" style="text-align: right">
                            <asp:Label runat="server" ID="lblTDSAmt_Page2"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right; font-weight: bold">NET PAYABLE
                        </td>
                        <td colspan="2" style="text-align: right">
                            <asp:Label runat="server" ID="lblGrandTotal_Page2" Font-Bold="true"></asp:Label></td>
                    </tr>

                    <tr>
                        <td colspan="4">Amount Chargeable (in words) : &nbsp;
                        <b>
                            <asp:Label runat="server" Text="" ID="lblAmountInWords_Page2"></asp:Label></b>
                        </td>
                    </tr>

                    <tr>
                        <td style="border-bottom: 1px solid #fff; border-right: 1px solid #fff;">
                            <p>
                                Terms and Conditions<br />
                                <asp:Label runat="server" Text="" ID="lblAnnexure1"></asp:Label>
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
                                <asp:Image ID="Image1" Height="80" Width="120" Visible="false" BorderColor="White" runat="server" />
                                <asp:ImageButton ID="imgBtnDigitalSign1" Width="150px" Height="80px" runat="server" />
                                <%--  <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:220px;"/>--%>
                            </div>
                            <p>Authorised Signatory</p>
                        </td>
                    </tr>

                </table>
            </div>
            <div class="invoiceFooter" style="margin-top: 30px;">
                <p style="margin: 0;">This is a Computer Generated Document</p>
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
            </div>

        </div>
    </div>

    <div id="divToPrint3">
        <div id="divTableDataHolder3" style="width: 880px">
            <div style="width: 100%">
                <br />
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo3"></asp:Label></b>
            </div>

            <div class="mypageadjusttwo">
                <b><u>ANNEXURE-A</u> </b>
                <br />
                <b>COMMERCIAL TERMS and CONDITIONS</b>


                <table border="0" class="table table-bordered" style="width: 830px; text-align: justify">
                    <tr>
                        <td style="width: 60px; text-align: center"><b>Sl.No </b></td>
                        <td style="width: 200px; text-align: center"><b>Terms and Conditions </b></td>
                        <td style="text-align: center"><b>Accepted Terms </b></td>
                    </tr>
                    <tr>
                        <td><b>1 </b></td>
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
                        <td><b>2 </b></td>
                        <td><b>UGCL Contact Details : 
                        <br />
                            Name 
                        <br />
                            Contact No 
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
                        <%--style="border-bottom: hidden"--%>
                        <td><b>3 </b></td>
                        <td><b>GST</b></td>
                        <td>
                            <asp:Label runat="server" ID="lblGst"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>4 </b></td>
                        <td><b>Payment Terms </b></td>
                        <td>
                            <asp:Label runat="server" ID="lblPaymentTerms"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>5 </b></td>
                        <td><b>Description / Nature of Work </b></td>
                        <td>
                            <asp:Label runat="server" ID="lblDesc"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>6 </b></td>
                        <td><b>Location of Work </b></td>
                        <td>
                            <asp:Label runat="server" ID="lblWorkLocation"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>7 </b></td>
                        <td><b>Site Working Conditions </b></td>
                        <td>
                            <asp:Label runat="server" ID="Label2" Text="Good"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td><b>8 </b></td>
                        <td><b>Other Conditions </b></td>
                        <td style="padding: 0;">
                            <%--<asp:Label runat="server" ID="lblOtherTerms" Text="Good"></asp:Label>--%>
                            <asp:GridView runat="server" ID="Grid_OtherTerms" ShowHeader="false" ShowFooter="false" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="OtherTerms" HeaderText="Other Terms" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>

                <div style="display: none">

                    <b><u>ANNEXURE-B</u> </b>
                    <br />
                    <b>PRICING SCHEDULE</b>
                    <br />
                    <table border="0" class="table table-bordered" style="width: 830px; text-align: justify;">
                        <tr>
                            <td style="text-align: center; width: 8%">
                                <b>Sl. No</b>
                            </td>
                            <td style="text-align: center">
                                <b>Description</b>
                            </td>
                            <td style="text-align: center; width: 20%">
                                <b>Amount (in INR)</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">1
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblItemDesc"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                <asp:Label runat="server" ID="lblRateUOM"></asp:Label>
                            </td>
                        </tr>

                    </table>


                    <b><u>ANNEXURE-C</u> </b>
                    <br />
                    <b>SCOPE OF WORK</b>
                    <br />
                    <table border="0" class="table table-bordered" style="width: 830px; text-align: justify;">
                        <tr>
                            <td style="text-align: center; width: 8%">
                                <b>Sl. No</b>
                            </td>
                            <td style="text-align: center">
                                <b>Description</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">1
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblItemDesc1"></asp:Label>
                            </td>
                        </tr>

                    </table>

                </div>
            </div>

            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
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
                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo4"></asp:Label></b>
            </div>
            <b><u>ANNEXURE-B</u> </b>
            <br />
            <b>GENERAL TERMS and CONDITIONS</b>

            <table border="0" style="width: 550px; font-weight: bold; border: 1px solid white">
                <tr>
                    <td style="width: 35%">GTC WO Document No.</td>
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
            <div class="
                ">
                <p><b>United Global Corporation Ltd</b> has been entrusted  the  work  “<asp:Label runat="server" ID="lblProjectName"></asp:Label>” by the
                    <asp:Label runat="server" ID="lblPrincipalContractor"></asp:Label>
                    /
                    <asp:Label runat="server" ID="lblAward_By"></asp:Label>
                    . </p>

                <p>These General Terms and Conditions are applicable to all Work Orders issued by <b>M/s. United Global Corporation Ltd.</b> to <b>
                    <asp:Label runat="server" ID="lblSubcontractor1"></asp:Label>
                </b>during  the tenure of the project and thereafter till defect liability period or O&M handover as applicable from  the  date  of  these  General  Terms  and  Conditions  stated  hereinabove  and shall be read in conjunction with the said Work Order, the Special Conditions, Specifications, drawings and/or any other document attached to or enclosed with the Work Order and shall form an integral and binding part of the Work Order placed on the Subcontractor. </p>

                <p>Each Work Order, and other documents included therein or attached or enclosed with Work Order, together with terms of these general terms and condition of Work Order,constitutes a separate Work Order Contract (hereinafter referred to as ‘Work Order Contract’) that will be effective upon issue of   Work Order to the Sub - Contractor with respect to performance of work as described in the Work Order. </p>
                <p>Each Work Order shall be governed by the terms of these General Terms and Conditions of Work Order. In the event of any conflict, discrepancy, inconsistency or ambiguity between the terms of these General Terms and Conditions of Work Order and any other documents, than the priority shall be as follows: (1) Employer Work Order & terms; (2) Other enclosure or Annexure or attachment to Work Order; and (3) These General Terms and Conditions of Work Order.</p>
                <p>The Subcontractor shall return the copy of these General Terms & Conditions of Work Order duly accepted without any deviations for further processing at Contractor’s end. The General Terms and conditions of Work Order would be deemed to be accepted if signed and stamped acceptance/acknowledgement copy is not received within 4 working days the Subcontractor accepts the Work order or receives payments. Failure to return these General Terms & Conditions / Contract doesn't diminish Subcontractor’s responsibility of executing the services required as per the Work Order Contract.</p>
                <p>Contractor and Subcontractor shall hereinafter individually referred to as Party and collectively referred to as Parties.                       </p>
                <p>
                    <b>1.DEFINITIONS :</b>&nbsp; In these General Conditions and in any Work  Order   issued  by  Contractor  to  Subcontractor,  the following words and expressions shall have the meanings hereby assigned to them:
                        <br />
                    <b>ACCEPTED SUBCONTRACT AMOUNT :</b>&nbsp; ‘Accepted Subcontract Amount’ means either the Lump Sum Amount or the total sum derived from the multiplication of quantities of such item and its rates accepted by the Contractor and after deduction of all rebates/debits as per the Accepted Priced Bill of Quantities of the Subcontract stated in or enclosed with the Work Order for the execution and completion of Work fit for the purpose including the remedying of defects therein in accordance with Clause 7 -   ‘The Scope of Work’ of these Conditions.
                        <br />
                    <b>APPLICABLE LAWS :</b>&nbsp; ‘Applicable Laws’ mean all laws, brought in to force and effect by Government of India or the State Government or the local bodies including rules, regulations and notifications made there under and judgments, decrees, injunctions, writs and orders of any court ofrecord, applicable to  the  Work Order Contract  and the exercise, performance and discharge of the respective rights and obligations of the Parties hereunder, as may be in force and effect during the subsistence of to  the  Work Order Contract   and shall include its statutory modifications, amendments, re- enactments, consolidations and substitutions as may be inforce from time to time
                        <br />
                    <b>CLIENT:</b>&nbsp; ‘Client’ means the Government/ Government Body/ Company/ Association of Persons/ Proprietary Firm/ Partnership Firm/ Individual or any other person, as notified to the Subcontractor from time to time by the Contractor and includes (Employer/ EPC  Contractor) and the legal successors in title to, or assignees of such person.
                        <br />
                    <b>ENGINEER/ CONSULTANT:</b>&nbsp; “Engineer” means the Independent Engineer or Consultant or Representatives or project monitoring team appointed or authorized by the Contractor or Employer.
                        <br />
                    <b>MAIN CONTRACT:</b>&nbsp; Main Contract means the Agreement dated<span> <u>
                        <asp:Label ID="lblEmployerAgreementDate" runat="server"></asp:Label></u></span> entered into between United Global Corporation Ltd and <span>
                            <asp:Label ID="lblPrincipalContractor1" runat="server"></asp:Label>
                        </span>read together with (i) Agreement dated <span><u>
                            <asp:Label ID="lblAgreementdate1" runat="server"></asp:Label></u></span> executed by <span>
                                <asp:Label ID="lblPrincipalContractor2" runat="server"></asp:Label></span> & Employer along with all its recitals, schedules and any amendments thereto made in accordance with the provisions contained in  Concession Agreement.
                        <br />
                    <br />
                </p>
            </div>
            <div class="footecls" style="margin-top: 150px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold;">
                    <div style="height: 52px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign3" Width="150px" Height="80px" runat="server" />
                            <%-- <img src="../Style/Images/Img_Sign.jpeg" style="Height:55px; Width:180px;"/>--%>
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>
                <div class="page-break"></div>
            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 100%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo5"></asp:Label></b>
                </div>
                <p>
                    <b>CONTRACTOR:</b>&nbsp; ‘Contractor’ means United Global Corporation Ltd, having its Office situated at No.E-07, 7th Floor, Solus Jain Heights,No. 2, 1st Cross,J.C.Road, Opp. Jain University, Bengaluru – 560027 and shall deem to mean and include its successors and assigns.
                        <br />
                    <b>CONTRACTOR’S AUTHORIZED REPRESENTATIVE:</b>&nbsp; ‘Contractor’s Authorized Representative’ means the Contractor’s Project In- Charge or any other person appointedor authorized by Contractor’s Project In- Charge.
                        <br />
                    <b>CONTRACTOR’S AUTHORIZED SIGNATORY:</b>&nbsp; ‘Contractor’s Authorized Signatory’ means the Contractor’s Authorized Officer authorized to sign on behalf of Contractor.
                        <br />
                    <b>CONTRACTOR’S ENGINEER:</b>&nbsp; ‘Contractor’s Engineer’ means the ‘Project In Charge’ of the Contractor and/or the person specifically appointed and authorized by the Contractor to act as Contractor’s Engineer for the purposes of the Work Order or other person appointed from time to time by the Contractor and notified to the Subcontractor.
                        <br />
                    <b>PERMANENT WORKS:</b>&nbsp; ‘Permanent Works’ mean the permanent works to be executed (including without limitation, all permanent structures and all work intended to form a continuing function after completion of the Works) in accordance with the Work Order.
                        <br />
                    <b>SPECIFICATIONS:</b>&nbsp; ‘Specifications’ shall mean the various technical and other specifications attached and referred to in the Enquiry or Main Contract or Work Order or these General terms and conditions and any additions or modifications to the Specifications in accordance with the Main Contract and shall include the directions, requirements and provisions furnished or approved in writing by the Contractor’s Engineer or the Engineer as the case may be. It shall also include the latest editions including all addenda / corrigenda or relevant Indian Standards specifications & Bureau of Indian Standards, etc. as applicable.
                        <br />
                    <b>SUB CONTRACT WORKS OR  WORKS :</b>&nbsp; ‘Subcontract Works’ or ‘Works’ mean the work(s) to be executed, items and / or activities to be carried out and / or services to be performed in accordance with the Work Order or part(s) thereof as the case may be and shall include all permanent and temporary works or extra or additional or altered or substituted works as required for the purposes of completion of the entire work contemplated under the work order and performance of the work order and urgent measures which in the opinion of the Contractor become necessary during the process of work to obviate any risk of accident or failure and shall include completion of the Works fit for the purpose as per the Main Contract entered in to between the Client and the Contractor with respect to the performance of work  as  described  in  the  Work  Order  including the remedying of defects therein.
                        <br />
                    <b>SUB CONTRACTOR:</b>&nbsp; ‘Subcontractor’ means <span><b>
                        <asp:Label runat="server" ID="lblSubcontractorName1"></asp:Label>
                    </b></span>having its Office situated at <span><b>
                        <asp:Label ID="lblAddline1" runat="server"></asp:Label>
                        <asp:Label ID="lblCity1" runat="server"></asp:Label>
                        -
                        <asp:Label ID="lblPinNo1" runat="server"></asp:Label></b> </span>whose offer has been accepted by the Contractor and named as Subcontractor in the Work Order and includes Subcontractor’s legal heirs, representatives, successors the legal successors in title to this (these) person(s), but not (except with the prior written consent of the Contractor) any assignee of this (these) person(s).
                        <br />
                    <b>SUB CONTRACTOR’S DOCUMENTS:</b>&nbsp; ‘Subcontractor’s Documents’ mean the calculations, computer programs and other software, drawings, manuals, models and other documents of a technical nature, if any, supplied by the Subcontractor under the Work Order.
                        <br />
                    <b>SUB CONTRACTOR EQUIPMENT:</b>&nbsp; Subcontractor equipment mean all apparatus, plant, equipment, machinery, vehicles, tools, tackles, and other things required for the execution and completion of the Subcontract Works fit for the purpose and the remedying of defects therein and which belong to and/or is provided by the Subcontractor. However, Subcontractor’s equipment excludes temporary works, Employer’s and/or the Contractor’s equipment, if any; plant, materials and any other things intended to form or forming part of the Permanent Works.
                        <br />
                    <b>TEMPORARY WORKS:</b>&nbsp; ‘Temporary Works’ mean all temporary works of every kind (other than Subcontractor’s Equipment) required on Site for the execution and completion of the permanent works and the remedying of defects therein.
                        <br />
                    <b>TIME FOR COMPLETION:</b>&nbsp; Subcontractor’s ‘Time for Completion’ means the time for completing the Subcontract Works fit for the purpose or a Section/Milestone thereof (as the case may be) where Milestone completion dates are stipulated, as stated in the Work Order and shall always include the mobilization period and monsoon period as well.
                        <br />
                    <b>WORK  ORDER :</b>&nbsp; ‘Work  Order’  means  work  order issued from time to time  by  Contractor  to  sub-contractor  during  the  period  from  the tenure of the project and thereafter till defect liability period or O&M handover as applicable from  the  date  of  these  General  Terms  and  Conditions of  Work  Order  for  execution  and  completion  of  the  specified  part  or  portion  of  the  scope  of  the  Main  Contract  and  includes   all documents like the Work Order placed on the subcontractor , the special conditions of the  Work  Order , the  accepted  rates  or  the Accepted Priced Bill of Quantities of the Work  Order and the further documents  and  enclosures or  annexure or schedules , if any, which are listed in or attached  to the Work Order and other documents connected with the issue of work order and orders, acceptance and the written specifications, drawings, technical data and other documents instructions, change orders, directions issued by the Contractor / Employer / Engineer   for the execution and completion of the specified part  or  portion of the scope of the Main Contract.
                        <br />
                    <b>WORK  ORDER  CONTRACT :</b>&nbsp; ‘Work Order  Contract’  shall mean and includes  (i)  Work Order, the Special Conditions of the  Work Order and the further documents  and  enclosures or  annexure or schedules , if any, which are listed in or attached  to the Work Order or  any  other documents connected with the issue of Work Order  , the  accepted  rates  or  the Accepted Priced Bill of Quantities of the Work  Order, acceptance and the written specifications, drawings, technical data and other documents  instructions, change orders, directions issued by the Contractor / Employer / Engineer   for the execution and completion of the specified part  or  portion of the scope of the Main Contract and  (ii)  these  General Terms and Conditions of  Work  Order. Each  Work Order,  together  with  other documents included therein or attached with  Work Order  together with these  general  terms  and  condition  of  Work Order ,  constitutes  a  separate  Work Order  Contract ( hereinafter referred to as ‘Work Order Contract’) that  will  be  effective  upon  issue  of  Work Order  to  the  Subcontractor  with respect to performance of work as described in the Work Order.
                            
                </p>
            </div>
            <div class="footecls" style="margin-top: 165px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold;">
                    <div style="height: 60px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign4" Width="150px" Height="80px" runat="server" />
                            <%--   <img src="../Style/Images/Img_Sign.jpeg" style="Height:65px; Width:180px;"/>--%>
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>
            </div>

            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo6"></asp:Label></b>
                </div>
                <p><b>2.INTERPRETATION :</b>&nbsp; In these General  Terms  and  Conditions, except where the context requires otherwise, words imparting the singular also include the plural and vice versa. The marginal words and other headings shall not be taken into consideration in the interpretation of these Conditions. Any reference to a statutory provision shall include such provision as is from time to time modified or amended or re-enacted or consolidated or substituted so far as such modification or amendment or re-enactment or consolidation or substitution applies or is capable of applying to any transactions entered into hereunder. The rule of construction or interpretation, if any, that a contract should be interpreted against the Party/Parties responsible for drafting and preparing the Contract shall not apply. </p>
                <p><b>3.PRIORITY OF DOCUMENTS :</b>&nbsp;The documents forming the Work Order Contract, listed in the Work Order are to be taken as mutually explanatory of one another unless otherwise provided in the Work Order. For the purposes of interpretation, the priority of the documents shall be in accordance with the following sequence : (i) The Work Order & Employer Work Order terms; (ii) The Special Conditions of Work Order, if any; (iii)The Specifications of Subcontract (shall include Technical Specifications with respect to the Subcontract Works); (iv) The Subcontract Drawings; (v) The Priced Bill of Quantities; (vi) Any other document forming part of the Work Order; (vii) These General Conditions of Work Order. If an ambiguity or discrepancy is found in the documents, the Contractor’s Engineer shall issue any necessary clarification or instruction which shall prevail.</p>
                <p>
                    <b>4.REPRESENTATIONS AND WARRANTIES :</b>&nbsp; Unless  otherwise  intimated  in  writing  by  the  sub-contractor  to  the  Contractor  during  the  tenure  of  these  General  Terms  and  Condition,  the Subcontractor hereby  , represents, warrants and confirms to the Contractor that :
                       
                        it is duly organized and validly existing under the Laws of India and has full power and authority to execute and perform its obligations under  the  Work Order Contract   and to carry out the transactions contemplated thereby;
                       
                        it has taken all necessary corporate and other action under the Applicable Laws and its constitutional documents to authorize the execution, delivery and performance of  the  Work Order Contract;it has the financial standing and capacity to undertakethe    executionof Works  subcontracted to it under the  Work Order Contract constitutes its legal, valid and binding obligation enforceable against it in accordance with the terms hereof; and its obligations under  the  Work Order Contract  shall be legally valid, binding and enforceable obligations against it in accordance with the terms hereof; it is subject to the Laws of India, and hereby expressly and irrevocably waives any immunity in any jurisdiction in respect of  the  Work Order Contract or matters arising there under including any obligation, duty, responsibility or liability hereof;
                        
                        the execution, delivery and performance of  the  Work Order Contract  will not conflict with, result in the breach of, constitute a default under or accelerate performance required by any of the terms of the constitutional documents of the Subcontractor or any Applicable Laws or any covenant, contract agreement, understanding, decree or order to which it is a party or by which it or any of its properties or assets is bound or affected ; there are no actions, suits, proceedings, or investigations pending or, to the Subcontractor’s knowledge, threatened against it under the Applicable Laws or in equity before any court or before any other judicial, quasi-judicial or other authority, the outcome of which individually or in the aggregate may result in the breach of or constitute a default of the Subcontractor  under  the  Work Order Contract  or which individually or in the aggregate may result in any material impairment of its ability to perform any of its obligations, duties, responsibilities or liabilities under Work Order Contract .
                    
                        there are no actions, suits, proceedings or investigations pending to the Subcontractor’s knowledge, threatened against it under the Applicable Laws or in equity before any court or before any other judicial, quasi-judicial or other authority, the outcome of which individually or in the aggregate may result in any material adverse effect on its business, properties or assets or its condition, financial or otherwise, or in any material impairment of its ability to perform any of its obligations, duties, responsibilities or liabilities under  the  Work Order Contract  ;
                     
                        it has no knowledge of any violation or default with respect to any order, writ, injunction or decree of any court or any legally binding order of any Government or Government Instrumentalities which may result in any material adverse effect on its ability to perform its obligations, duties, responsibilities or liabilities under to  the  Work Order Contract  and no fact or circumstance exists which may give rise to such proceedings that would adversely affect the performance of its obligations, duties, responsibilities or liabilities under  the  Work Order Contract  ;
                        
                        it has complied with the Applicable Laws in all material respects and has not been subject to any fines, penalties, injunctive relief or any other civil or criminal liabilities which in the aggregate have or may have a material adverse effect on its ability to perform its obligations, duties, responsibilities or liabilities under  the  Work Order Contract  ;
                       
                        no representation or warranty by the Subcontractor contained herein or in any other document furnished by it to the Contractor or to any Government or Government Instrumentalities in relation to applicable permits, licenses or approvals contains or will contain any untrue statement of material fact or omits or will omit to state a material fact necessary to make such representation or warranty not misleading;
                       
                        Sub-Contractor hereby admits and acknowledges that it has gone through all the above main contract documents executed between (i) Employer / EPC Contractor and Contractor, which shall govern the execution of works, thoroughly and understood the timeline, risks and costs involved in execution of Works; and hereby agrees to bear all kinds of risks, liabilities, costs and consequences arising out of execution of works under this agreement to the extent of scope of works.
                      
                </p>
                <p>the Subcontractor acknowledges that prior to accepting  work  order  issued  by  Contractor  to  sub-contractor  under  these  General  terms  and  conditions  , the Subcontractor shall   carry  out  a complete and careful examination, make an independent evaluation of the Work Order , the Scope of the Work, the Specifications and the Standards, the drawings, the Site, local conditions, physical properties/ characteristics of ground, subsoil and geology; traffic volumes and all information provided by the Contractor and shall  determined to its satisfaction the accuracy or otherwise thereof and the nature and extent of difficulties, risks and hazards as are likely to arise or may be faced by it in the course of performance of its obligations, duties, responsibilities or liabilities there under. The Subcontractor acknowledges and hereby accepts the risk of inadequacy, mistake or error in or relating to any of the matters set forth above and hereby acknowledges and agrees that the Contractor shall not be liable for the same in any manner whatsoever to the Subcontractor or any person claiming through or under any of them.</p>
                <p>In the event that any of the representations or warranties made/given by the Subcontractor ceases to be true or stands changed, it shall be the responsibility of the Subcontractor to promptly notify the Contractor of the same in writing. The Subcontractor shall indemnify and hold the Contractor harmless against and from the consequences of any such failure and any such failure shall constitute an event of default by the Subcontractor and the consequences there under shall be entirely to the account of the Subcontractor. </p>

            </div>
            <div class="footecls" style="margin-top: 80px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 50px;">
                    <div style="height: 85px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign5" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo7"></asp:Label></b>
                </div>
                <p>


                    <b>5. SUB CONTRACTOR’S GENERAL OBLIGATIONS :</b>&nbsp;The Subcontractor’s general obligations, duties, responsibilities and liabilities shall include, but not limited to, the following:
                        <br />
                    The Subcontractor shall comply with all Applicable Laws in the performance of its obligations, duties, responsibilities and liabilities under the Work Order Contract.  The Subcontractor shall comply with all applicable permits, licenses and approvals in accordance with Applicable Laws in the performance of its obligations, duties, responsibilities or liabilities under the Work Order Contract. The Subcontractor shall make or cause to be made, necessary applications to the relevant Government and other Government Instrumentalities with such particulars and details, as may be required for obtaining all applicable permits, licenses or approvals under Work Order and obtain and keep in force and effect all such applicable permits, licenses or approvals in conformity with the Applicable Laws. The Subcontractor shall, not later than 30 (thirty) days from the date of issue of the Work Order, obtain all such applicable permits, licenses or approvals under  the Work Order from  time  to  time  unconditionally or if subject to conditions then all such conditions shall have been satisfied in full and such applicable permits, licenses or approvals  shall be kept in full force and effect for the relevant period during the subsistence of the Work Order.  The Subcontractor shall be solely responsible and undertakes the obligation to ensure all statutory and legal compliances (including but not limited to obligations under all the labour and industrial Laws). Notwithstanding any other provision contained herein to the contrary or otherwise, all statutory and legal compliances shall be sole responsibility and obligation as well as liability of the Subcontractor.  In the event of any failure of the Subcontractor as regards any of the statutory or legal compliances, the Subcontractor shall indemnify and hold the Contractor / Client harmless against and from the consequences of any such failure and any such failure shall constitute an event of default by the Subcontractor and the consequences there under shall be entirely to the account of the Subcontractor.
                        <br />
                    The Subcontractor after receiving possession of the site or part thereof, ensure that such site remains free from all encroachments and take all steps necessary to remove encroachments, if any. The Subcontractor shall be fully responsible for the security and presence on or around or entry in or around the project site or any other interference with or affecting the project site or the execution of the Works by or caused by any protestor or trespasser or for the act, omission, or default of any such person during the contract period for the land handed over to the Subcontractor. Any such interference shall not be a breach of the obligations of the Contractor to provide access to the Project Site.
                        <br />
                    <b>DRAWING & DOCUMENTATION: </b>&nbsp;All works under the Work Order shall be executed as per the specification and the drawings issued for construction. Drawings shall be issued progressively to meet the project requirement. Sub-Contractor shall have no right for any claim arising out of delay in release of drawings. Nothing extra shall be paid for this and this shall be deemed to have been taken in account in the quoted rates. Sub-Contractor shall treat all documents, specifications, drawings and contents therein as private and confidential.
                        <br />
                    <b>CODES & STANDARD: </b>The sub-contract works shall be executed in full compliance with the specifications, relevant codes, standards and to the entire satisfaction and approval of the Employer/ Engineer. Workmanship should be ensured it is the best in the industry. The sub-contract works shall be carried out as per the specifications and approved drawing laid down by these General Terms and Conditions / Work Order / by the Employer / Consultants. In the absence of specifications, and after approval of Contractor’s Engineer, relevant Indian Standard Code of practice prescribed by relevant regulatory authority includingbut not limited to MoRTH , IRC , BIS etc. together with their latest revisions/amendments as applicable on the date of Work  Order shall be followed. In the absence of the relevant I.S. code of practice, the instructions of the Contractor’s Engineer and after approval of Contractor’s Engineer, standard engineering practice shall be adopted. In case of contradictions/conflicts between the specifications, the interpretation of the Contractor’s Engineer shall be final and binding. Method statement shall be prepared for all items of work and Contractor’s Engineer shall approve the same. Standby machinery shall be maintained at site for any breakdown requirements. Subcontractor shall prepare shop drawings for all such items of work desired by Contractor’s Engineer /Employer/Consultant, which shall be duly approved before commencement of such items of work. Sub-Contractor shall co-operate completely to bring about overall quality of the project.
                        <br />
                </p>
                <p>
                    The Subcontractor shall give prompt prior notice to the Contractor of any error, omission, fault or other defect in the design of or the Specification for the Subcontract Works which it may discover when reviewing the Work Order and/or the Main Contract or executing the Subcontract Works. The Subcontractor shall be responsible for the adequacy, stability and safety of design (to the extent specified), all methods of construction and all Site operations. The Subcontractor shall be responsible for all Subcontractor’s Documents, Subcontractor’s Equipment, Temporary Works, and such design of each item as is required for the item to be in accordance with the Work Order Contract.
                        <br />
                    The Subcontractor shall, with due care and diligence, design (to the extent of scope of the Work Order), execute and complete the Works fit for the purpose in accordance with the Work Order and with the Contractor’s Engineer’s instructions and shall remedy any defects in the Works. The Subcontractor shall execute the Subcontract Works as per the terms and conditions of the Work Order Contract,specification, the Drawings, the Bill of Quantities, and best workmanship. Any modifications / additions/ changes necessary as per the opinion of the Contractor’s Engineer and/or the Specifications or drawing shall be conveyed to the Subcontractor and the same shall be adhered to by the Subcontractor. The Subcontractor shall not make any changes, whatsoever, unless instructed/ permitted in writing by the Contractor save and except changes necessary to correct errors or omissions.
                        <br />
                </p>
                <p>
                </p>

            </div>
            <div class="footecls" style="margin-top: 180px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 55px; }">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign6" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>


            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo8"></asp:Label></b>
                </div>
                <p>
                    <b>QUALITY ASSURANCE: </b>The subcontractor shall strictly follow the Quality Control measures established by Contractor / Employer. The Subcontractor shall institute a Quality Assurance System to demonstrate compliance with the requirements of the Work Order. The System shall be in accordance with   the   details   stated   in   the Work Order Contract. The Contractor’s Engineer/ Engineer / the Consultant/ the Employer shall be entitled to audit any aspect of the system. Details of all procedures and compliance documents shall be submitted to the Contractor’s Engineer before   each   design   and   execution   stage is commenced. When any document of a technical nature is issued to the Contractor’s Engineer, evidence of the prior approval by the Subcontractor itself shall be apparent on the document itself. However, notwithstanding submission of the above documents by the Subcontractor or any review and comments/observations by the Contractor’s Engineer or failure of the Contractor’s Engineer to review or provide comments/observations thereon; the Subcontractor shall remain solely responsible for and shall not be relieved or absolved in any manner whatsoever of its obligations, duties, responsibilities or liabilities under  the  Work Order Contract  and that the Contractor shall not be liable for the same in any manner whatsoever.
                        <br />
                    The Subcontractor may take up the execution of the Works itself or be entitled, in turn, to engage its subcontractors to undertake the execution of the Works subject to aggregate sum of all such subcontracted works by it shall not exceed 25% (Twenty Five Percent) of the Accepted Subcontract Amount. For the purpose of this Clause, subcontracting will not include provision of workmen/labour, purchase of materials and Transportation of Materials. Provided that, appointment of any such subcontractor or supplier shall be only with the prior written consent of the Contractor. The Subcontractor shall submit to the Contractor particulars of its subcontractors or suppliers it proposes to employ and the extent of scope of each subcontractor/supplier’s gross order value to enable the Contractor to provide his approval or consent or otherwise. Subcontracting of Works in excess of the above specified limit at any stage of the Projector not obtaining prior written approval or consent from the Contractor shall constitute breach of the Work Order Contract by the Subcontractor. However, notwithstanding any such submission of the above particulars by the Subcontractor or any approval or consent or disapproval by the Contractor’s Engineer or failure of the Contractor’s Engineer to approve or give consent or otherwise, the Subcontractor shall remain  solely  responsible for and shall not be relieved or absolved in any manner whatsoever of its obligations, duties, responsibilities or liabilities under  the  Work Order Contract and that the Contractor shall not be liable for the same in any manner whatsoever. Further the Subcontractor shall be responsible for the acts, defaults, omissions, and neglects of any of its subcontractor or supplier, their agents, or workmen, fully and to the extent as if they were the acts, defaults, omissions, or neglects of the Subcontractor itself.
                        <br />
                    <b>SAFETY : </b>The Subcontractor  shall (i) comply with all applicable safety regulations ; (ii) take care for the safety of all persons entitled to be on the Site ; (iii) use reasonable efforts to keep the Site and Works clear of unnecessary obstruction so as to avoid danger to these persons ; (iii) provide fencing, lighting, guarding and watching of the Works until completion and taking over ; (iv) provide any Temporary Works (including roadways, footways, guards and fences) which may be necessary, because of the execution of the Works, for the use and protection  of  the  public and of owners and occupiers of adjacent land and (v) strictly comply with the Employers Agreement  and  Contractor’s manual and instructions for ‘Safety, Health and Environment’. 
                        <br />
                    <b>ENVIRONMENTAL PROTECTION: </b>In addition to the strict compliance of all the Applicable Laws,Employers Agreement   and the Contractor’s Manual and instructions for ‘Safety, Health and Environment’; the Subcontractor shall ensure and take all required steps to protect the environment (both on and off the Site) and to limit damage and nuisance to people and property resulting from pollution, noise and other results of its operations. The Subcontractor shall ensure that emissions, surface discharges and effluent from the Subcontractor’s activities shall not exceed the values stated in the Specification or prescribed by the Applicable Laws.

                </p>
                <p>
                    <b>HOUSE KEEPING: </b>At all times during the execution of Works, the Sub contractor shall, at its costs, keep the site reasonably free from all unnecessary obstruction and shall arrange storage of materials at site in orderly manner or dispose of any unwanted materials. On substantial completion of works or upon completion of works or earlier determination, the Subcontractor shall, at its costs, clear away and remove from site all surplus materials, rubbish, debris etc. and leave the whole of the site and works clean to the complete satisfaction of the Contractor. If the Subcontractor fails to remove the surplus materials, rubbish, debris etc., the Contractor shall engage additional manpower to clean the same and the amount so incurred shall, without prejudice to any other method of recovery, be deducted by the Contractor from any amount due or which may become due to the Subcontractor or shall be recovered as a debt. For this purpose, the Contractor shall also have the right to invoke Cross Fall Breach and Set Off provisions of Clause 23.
                        <br />

                    <b>SUB CONTRACTOR’S PERSONNEL: </b>Subcontractor shall provide required manpower to complete the work as per specified time schedule. The Subcontractor’s Personnel shall be appropriately qualified, skilled, competent, experienced and trained in their respective trades or occupations. Prior to posting key personnel, the Subcontractor shall obtain approval or consent of the Contractor’s Engineer. However, notwithstanding any such approval or consent by the Contractor’s Engineer or failure to give approval or consent, the Subcontractor shall remain solely responsible for and shall not be relieved or absolved in any manner whatsoever of its obligation’s duties, responsibilities or liabilities under the Work Order Contract and that the Contractor shall not be liable for the same in any manner whatsoever. The Contractor’s Engineer may require the Subcontractor to remove (or cause to be removed) any person employed on the Site or Works, including the Subcontractor’s Representative, if applicable, who (i)  persists in any misconduct or misbehavior or lack of care or (ii) carries out duties incompetently or negligently or (iii) fails to conform with any provisions of the Work Order Contract   or (iv) persists in any conduct which is prejudicial to safety, health, or the protection of the environment. If appropriate, the Subcontractor shall; then appoint (or cause to be appointed) a suitable replacement person.
                        <br />
                    <b>SUB CONTRACTOR’S EQUIPMENT: </b>The Subcontractor shall be responsible for all Subcontractor’s Equipment. When brought on to the Site, Subcontractor’s Equipment shall not be more than (5) years in age, shall be complete in all respects with all accessories and necessary spares and shall be in good working condition. The Subcontractor shall maintain adequate inventory of spares at site all the time. When brought on to the Site, Subcontractor’s Equipment shall be deemed to be exclusively intended for the execution of the Subcontract Works. The Subcontractor shall not remove from the Site any item of Subcontractor’s Equipment without the written consent of the Contractor’s Engineer. However, no such consent shall be required for vehicles transporting Goods or Subcontractor’s Personnel off Site. The Sub-contractor shall guard its own resources i.e., Subcontractor’s materials & equipment from any theft, damage, pilferage etc. and Contractor shall not entertain any issues and claims on account of this.
                        <br />


                </p>

            </div>
            <div class="footecls">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 80px;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign7" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>
            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo9"></asp:Label></b>
                </div>
                <p><b>LABOUR LAWS: </b>The Subcontractor shall in connection with the work order strictly comply with all the relevant labor Laws of the State Government, Central Government and Local Authorities as applicable to the place of work including but not limited to the Indian Workmen’s Compensation Act, The Payment of Wages Act, 1936. The Minimum Wages Act, 1948, The Contract Labour (Regulations & Abolition) Act 1970, Employees Provident Funds & Miscellaneous Provisions Act, 1952, Employees  State  Insurance  Act  and schemes framed thereunder and  any other labour laws of Central and/or State Governments in force from time to time and the rules made thereunder  as applicable from time to time and in so far they are applicable to the Subcontractor’s establishment, Subcontractor personnel, staff, employees and labour including Laws relating to their on-site as well as off-site employment, health, safety, welfare, immigration and emigration, and shall allow them all their legal rights. The Subcontractor shall require its employees to obey all Applicable Laws, including those concerning safety at work. The Sub-Contractor shall be required to take a Workmen's Compensation Insurance policy to cover the risk of all his workmen engaged by him for the work.  The Subcontractor shall be liable for paying compensation, if any, for any claims arising out of accidents to his labour under the Indian Workmen’s Compensation Act. The Contractor / Employer shall not be liable for reimbursement of medical expenses of the Subcontractor and / or his labour. The Subcontractor shall in the manner provided by Law, pay to every employee/ workman employed by him in connection with the Work Order, wages at rate not less than the minimum rate of wages fixed by the appropriate Government, under the Minimum Wages Act, for the class of employees engaged in the works of similar to the one under the Work Order. The subcontractor shall obtain his own independent PF code.  The Subcontractor shall have to cover their workmen under PF act and the necessary documentary proof for same shall be submitted to Contractor on monthly basis, prior to bills payment and PF liability for Subcontractor’s workmen for employee and employer portion is in sub-contractor scope only. All records to be maintained under these laws shall be maintained by the subcontractor and produced to the concerned authorities as and when directed to do so. No extra payment shall be made to comply with these labour laws by Contractor or Employer. The Subcontractor shall indemnify and hold the Contractor harmless against and from the consequences of any such failure and any such failure shall constitute an event of default by the Subcontractor and the consequences there under shall be entirely to the account of the Subcontractor. In the event of non-payment of the any above, and any liability or recovery imposed on Contractor or the Employer, Contractor reserves the right to pay the same to the respective authorities and deduct the same from Subcontractor’s dues and shall without prejudice to any other method of recovery, be deducted by the Contractor from any amount due or which may become due to the Subcontractor or shall be recovered as a debt. </p>
                <p>
                    <b>INSPECTION & TESTING: </b>The Client/Contractor’s representatives or Engineer-In-Charge shall have full power and authority to inspect the works at any time wherever in progress on site and Sub-Contractor shall afford or procure for Employer/Contractor’s representatives or Engineer-In-Charge every facility and assistance to carry out such inspection. Testing of equipment, tools, tackles, materials, etc. or preparation of mock ups or trial patches etc. shall be performed by the Subcontractor at its own costs as per instructions of and to the satisfaction of Contractor’s Authorized Representative or Engineer.
                        <br />
                    <b>WORK PROGRAMME: </b>Subcontractor shall be coordinating with Contractor’s site management and make detailed program, which shall be agreed upon. This program is to be adhered to without any slippage, and the same shall be reviewed periodically by Contractor or Engineer. The Subcontractor shall submit (in editable soft copies and required number of hard copies) to the Contractor’s Engineer, within 7 (Seven) days from the date of Work Order, a detailed Work Programme including Construction Methodology and other documents. The Subcontractor shall also submit a revised Work Programme, whenever the previous Programme is rendered inconsistent with actual progress or with the Subcontractor’s obligations. The Works Programme shall include Construction Methodology, anticipated monthly and cumulative billing for achieving the Completion of the Work  Order  and on which  the actual cumulative progress shall be superimposed, a Quality Assurance Plan (QAP) covering all aspects of the work to be adopted for this Works for effective assurance, control  and management of Quality in conformity with the Specifications, an Environmental Management Plan , a Safety Management Programme including an Emergency Response Protocol, a Traffic Diversion & Management Plan, if applicable, The sequence of all Tests during  execution  and  on Completion. The Contractor’s Engineer shall, within 14 (Fourteen) days of the receipt of the Work Programme, review the same and convey its comments/observations to the Subcontractor with a particular reference to the conformity or otherwise with the requirements under the Work Order.  However, notwithstanding any review and comments/observations by the Contractor’s Engineer on the above documents or failure of the Contractor’sEngineer to review or provide comments/observations thereon, the Subcontractor shall remain  solely  responsible for and shall not be relieved or absolved in any manner whatsoever of its obligations, duties, responsibilities or liabilities under  the  Work Order Contract   and that the Contractor shall not be liable for the same in any manner what so ever. 
                        <br />
                    <b>INSURANCE:</b>The Subcontractor shall obtain insurance for all the material, machineries,Equipments, tools & tackles supplied, workmen engaged by subcontractor to execute the job and keep these insurances alive till the completion of the work. Comprehensive insurance coverage with third party liability for all Plant & Machinery, Materials, policy for manpower and any other equipment deployed at site for the purpose of the stated Subcontract Works shall be obtained. This insurance shall be effective from the date on which work is proposed to be commenced at site until the date of issue of the Performance Certificate for the Works to provide cover for loss or damage for which the Subcontractor is liable arising from a cause occurring prior to the issue of the Performance Certificate and for loss or damage caused by the Subcontractor in the course of any other operations including under Defects Liability. Transit insurance shall be in the scope of the Sub-contractor. The Subcontractor shall insure the Subcontractor’s Equipment for not less than the full replacement value, including delivery to Site. For each item of the Subcontractor’s Equipment, the insurance shall be effective while it is being transported to the Site and until it is no longer required as the Subcontractor’s Equipment. The Subcontractor shall effect and maintain insuranceagainst liability for claims, damages, losses and expenses (including legal fees and expenses) arising from injury, sickness, disease or death of any person employed by the Subcontractor or any other of the Subcontractor’s Personnel. The insurance shall be maintained in full force and effect during the whole time that these personnel are assisting in the execution of the Works.
                        <br />
                    The Subcontractor shall provide all Subcontractor’s Documents, Subcontractor’s Equipment, Subcontractor’s Personnel, materials, goods, consumables and other things and services, whether of a temporary or permanent nature, required in and for execution and completion of Subcontract Works  fit for the purpose and the remedying of any defects.
                        <br />

                </p>


            </div>
            <div class="footecls" style="margin-top: 60px;">

                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 75px;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign8" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo10"></asp:Label></b>
                </div>
                <p>
                    Notwithstanding the submission of list of resources proposed to be deployed by the Subcontractor, the Subcontractor shall from time to time, bring additional resources as required at site to achieve desired rate of progress as also augment resources, from time to time, to make good shortfall in progress, if any without any additional costs to the Contractor.
                        <br />
                    Any and all materials issued by the Contractor, if any, to the Subcontractor shall be deemed to be in the possession and custody of the Subcontractor upon issue of Material Issue Note to the Subcontractor.  The Subcontractor shall ensure that all materials issued by the Contractor, shall be secured and so stored as to ensure the preservation of the quality and fitness for the Works. The Subcontractor shall confirm to all applicable national and local safety and other codes. All materials issued by the Contractor, if any, to the Subcontractor shall be used by the Subcontractor only for the purpose of and with respect to the Works for which they are intended and for no other purpose. The Subcontractor shall ensure that Plant or Equipment issued by the Contractor, if any shall be used with due care and after such use promptly returned to the Contractor in working condition. Unless otherwise specified in the Work Order, in case, if any material / consumable / tool etc., is issued by Contractor for carrying out the works under sub-contractor scope, the same shall be charged and  shall be  deducted  from  the invoices or dues  of  sub-contractors.
                        <br />
                    The Subcontractor shall disclose all information including confidential information as may be reasonably required in order to verify compliance with the Work Order Contract and allow its proper implementation.
                        <br />
                    The Subcontractor shall not be entitled to assign the Work Order or the rights, benefits and obligations here under save and except with prior written consent of the Contractor.
                        <br />
                    The Subcontractor shall, in the event of termination of the Work Order during the Contract Period, handover the subcontractor documents and project site in the form required by the Contractor and /or the Client.
                        <br />
                </p>
                <p>
                    If required under the Main Contract, the Subcontractor shall ensure, provide and comply with any and all obligations, and additional warranties, guarantees and all other requirements there under.
                       
                </p>
                <p>
                    <b>SUB CONTRACTOR’S SUPERINTENDENCE: </b>Throughout the execution of the Works, and as long thereafter as is necessary to fulfill the Subcontractor’s obligations, the Subcontractor shall provide all necessary Superintendence to plan, arrange, direct, execute, manage, inspect and test the works. Superintendence shall be given by sufficient number of persons having adequate knowledge of the operations to be carried out (including the methods and techniques required, the hazards likely to be encountered and methods of preventing accidents) for the satisfactory and safe execution of the Works
                </p>
                <p><b>6.APPROVAL OF CONTRACTOR/ CONSULTANT/EMPLOYER :</b>&nbsp;All Subcontractor’s Documents, Subcontractor’s Equipment, materials and services to be incorporated in or required for the Works and all Subcontract Works executed by the Subcontractor and its workmanship shall be subject to the approval of the Contractor/ Consultant/ Employer. However, notwithstanding such approval by the Contractor/ Consultant/ Employer; the Subcontractor shall remain solely responsible for and shall not be relieved or absolved in any manner whatsoever of its obligations, duties, responsibilities or liabilities under the Work Order Contract and that the Contractor shall not be liable for the same in any manner whatsoever.                </p>
                <p><b>7.THE SCOPE OF WORK :</b>&nbsp; The Scope of Work shall be as more particularly described in the Work Order and shall include all ancillary, connected and incidental works whether explicitly stated or otherwise and shall mean during the Contract Period and Defect Liability Period (i)  Construction of all works set forth in the Work Order  in conformity with the Specifications and Standards , approved drawing/s & methodology and instructions given by Contractor  or Engineer in line with the approved Specification  and  drawings fit for the purpose ; (ii) Performance and fulfillment of all other obligations, duties, responsibilities and liabilities of the Subcontractor in accordance with the provisions of  the  Work Order Contract  and matters incidental thereto or necessary for the performance of any or all of the obligations, duties, responsibilities and liabilities of the Subcontractor under the Work Order  ; (iii) Execution of all works required to remedy the defects or damage, as may be notified by the Contractor or  Engineer  on or before the expiry date of the Defects Liability  Period for the Works. The Contractor reserves the right to amend / modify the above scope and delete any item of work from subcontractor’s scope. </p>
                <p><b>8.SUFFICIENCY OF THE ACCEPTED SUBCONTRACT AMOUNT :</b>&nbsp;The Accepted Subcontract Amount shall cover all the obligations, duties, responsibilities and liabilities of the Subcontractor under the Work Order Contract and all the things (including all incidental and connected things whether explicitly mentioned or otherwise) necessary for the proper execution and completion of the Works fit for the purpose and the remedying of any defects. The Subcontractor shall be deemed to have satisfied itself as to the correctness and sufficiency of the Accepted Subcontract Amount and deemed to have based the Accepted Subcontract Amount on the data, interpretations, necessary information, inspections, examinations and satisfaction as to all relevant matters. The Subcontractor acknowledges that prior to the execution of the  Work  Order  , the Subcontractor shall, after a complete and careful examination, make an independent evaluation of the Scope of the Works, Specifications and Standards, Site, local conditions, physical qualities of ground, subsoil and geology, traffic volumes and all information provided by Contractor or obtained, procured or gathered otherwise, and has determined to its satisfaction the sufficiency, accuracy or otherwise thereof and the nature and extent of difficulties, risks and hazards as are likely to arise or may be faced by it in the course of performance of its obligations thereunder. Contractor makes no representation whatsoever, express, implicit or otherwise, regarding the accuracy, adequacy, correctness, reliability and/or completeness of any assessment, assumptions, statement or information provided by it and the Subcontractor confirms that it shall have no claim whatsoever against Contractor / Client in this regard. The Subcontractor acknowledges and hereby accepts the risk of inadequacy, mistake or error in or relating to any of the matters set forth and hereby acknowledges and agrees that Contractor   shall not be liable for the same in any manner whatsoever to the Subcontractor or any person claiming through or under any of them. Accepted Subcontract Amount including rates shall be valid for entire contract duration of works. </p>

            </div>
            <div class="footecls" style="margin-top: 100px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 10px;">
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>
            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo11"></asp:Label></b>
                </div>
                <p><b>9.PRICE & VALIDITY :</b>&nbsp;Unless otherwise specified in  the  Work  Order, all Subcontract Works, covered in the scope of the  work  order shall be completed in all respect and any material , labour and equipment’s required for total completion of work shall be included in the rates, specifically mentioned or not in the schedule of work.  The rates or  prices mentioned in the  Work  Order  are inclusive of cost of all materials , all labor, plant & machinery, equipment  hiring charges, cost  of  consumables, stores, tools and tackles,  the cost of water, if any , royalty  including  advance payment towards royalty, transportation, loading / unloading of material , shifting of material, inter carting, shuttering & formwork material, work insurance, labour insurance, equipment insurance, workmen compensation insurance and miscellaneous / Incidental / sundry cost, all expenses in relation to compliance with labour laws regulations, the safety and environmental regulations ,  taxes and duties etc., as applicable. All the liaisoning required for getting the permit for the borrowed material shall be in subcontractor’s scope of work, and the related expenses, if any, shall be included in above rates. After the completions of work, rehabilitation of borrow area and obtaining NOC from the land owner shall be the responsibility of the Subcontractor, no additional payment shall be made in this regard. </p>
                <p>The unit rates shall cover all minor and major accessories and work including minor / major change in alignments or any other portion even though not specifically mentioned in the specification and/or drawing but are required for completion of the work, as per Contractor’s requirement. No idling or mobilization & demobilization charges shall be paid to the subcontractor on any account whatsoever. In case ,  any  of  the  above  cost  is  incurred  by  the  Contractor for carrying out the works under sub-contractor’s scope, the same shall be charged and  deducted  from  the invoices or dues  of  sub-contractors.</p>
                <p><b>10.ESCALATION :</b>&nbsp;Unless otherwise specified in the Work Order, the Accepted Subcontract Amount shall be firm till the completion of the Subcontract Works and shall not be subject to escalation of any kind whatsoever.</p>
                <p><b>11.ESTIMATED QUANTITIES :</b>&nbsp;It shall be clearly understood that Contractor does not accept any responsibility for the correctness or completeness of schedule of items covering the scope of the Work Order and the same is liable to alterations, deductions or additions at the discretion of Contractor / Employer or as set forth elsewhere in the Work Order. The Contractor reserves the right to amend / modify the above scope and delete any item of work from subcontractor’s scope. The quantity mentioned in the bill of quantity is tentative and shall be subjected to variation from time to time. If the quantities vary downward, the rates shall be firm & fixed throughout the execution period for the revised quantity. The quantities shown in the Work Order are estimates only and are not guaranteed and may be increased or decreased by the Contractor and the Subcontractor shall be entitled only to payment for the amount of work actually executed by the Subcontractor / Certified by the Contractor’s Authorized Representative   at the rates accepted in the Work Order and the Subcontractor shall not be entitled to any compensation or other payments in respect of such variation in quantities.    </p>
                <p><b>12.JOINT MEASUREMENTS :</b>&nbsp;The Contractor’s Authorized Representative or Contractor’s Engineer in the presence of the Subcontractor or his Authorized Representative shall take measurements. Subcontractor shall maintain all documentation and record of work done. All measurements shall be entered in a Measurement Book maintained by the Contractor at site and signed both by the Contractor’s Authorized Representative and the Subcontractor as a token of acceptance. Measurements jointly taken and recorded as above shall be the sole, conclusive and final record for payments and payments shall be made accordingly in full and final settlement of the dues arising out of the Work Order, inclusive of any interim payments which may be made to the Subcontractor.    </p>
                <p>
                    <b>13.BILLING, TAXES,DUTIES,LEVIES,CESS,ROYALTY ETC. :</b>&nbsp;
                        <br />
                    Unless otherwise specified in the Work Order, the Subcontractor shall submit monthly tax invoices to the Contractor in triplicate together with detailed joint measurements, supporting documents and Reconciliation statement of materials, if any. Unless otherwise specified in the work order, Accepted Subcontract Amount shall be inclusive of all taxes, duties, levies, royalties, cess, octroi etc.  but excluding GST.  The breakup of all taxes, duties, levies, royalties, cess, octroi, GST etc. shall be made available by sub-contractor to Contractor along with tax invoice.
                        <br />
                    If the Subcontractor is  a  Registered  Dealer  under  GST  Act  ,  all invoices of the Subcontractor shall be in the form of Tax Invoices containing  all  particulars  as  per  GST  Act / Rules and shall be raised from the State in which the Site is located and taxes prevailing shall apply and compliance  to  the  Goods  &  Service  Tax  Act / Rules  shall be the responsibility and liability of the Subcontractor. The Subcontractor shall issue serially numbered Tax Invoice as required under the Goods  &  Service  Tax  Act  within prescribed time as  stipulated  in Clause  13.1  indicating the name and address of the Subcontractor and Contractor, the GST  Registration no. of the Subcontractor, the GST  Registration no.  of  the  Contractor,  Description of Supply/Services, HSN/SAC  Code under which supply/services are provided and details of IGST , CGST, SGST  separately.  The  Subcontractor  shall  be  solely  responsible  for  procedure  relating  to  e-way  bills  under  GST law wherever  required. The Sub-contractor shall ensure that the available concessions/exemptions as per the GST Acts shall be availed by him and further the concerned benefit of exemption /concession shall be passed on to the Contractor so as to minimize the work cost.
                        <br />
                    The  sub-contractor  undertakes  that  it  shall  upload  all  the  tax  invoices  raised  on  Contractor  in  GST  return on  GST  Portal  and  file  all  returns  under  GST  Act & Rules  within  stipulated  due  date.  The Subcontractor shall also submit any other documents as necessary to enable the Contractor to claim Input Tax Credit under GST and sub-contract billing amount as allowable deduction under the Income Tax Act. In case the Subcontractor fails  to  upload  the  tax  invoices  raised  on  Contractor properly  or fails to  file GST  Return  on  GST  portal  or  submit  documents as stated above  resulting  in  non-availability  of  ITC  credit  of  GST  to  Contractor, amount  of  GST  paid  by  Contractor  to  subcontractor  on  subcontractor’s  tax  invoices,   shall be paid by the Subcontractor to the Contractor or shall without prejudice to any other method of recovery, be deducted by the Contractor from any amount due or which may become due to the Subcontractor or shall be recovered as a debt  and  amount  of  GST  charged  on  subsequent  invoices  raised  by  subcontractor  shall  be  withheld  until  the  invoices  issued  by  subcontractor  on  Contractor  are  uploaded properly  in  GST  Return  on  GST  portal.  For this purpose, the Contractor shall also have the right to invoke Cross Fall Breach and Set Off provisions of Clause 23.
                        <br />
                    If the Subcontractor is not a Registered Dealer under GST Act because of its taxable turnover being lower than threshold limits as specified in GST Act or Rules, the same shall be mentioned on every invoice raised by the Subcontractor along with aggregate value of all taxable service till the date of last invoice issued in a Financial Year.
                </p>
            </div>
            <div class="footecls" style="margin-top: 60px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 30px;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign10" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo12"></asp:Label></b>
                </div>
                <p>
                    Income Tax shall be deducted as per the provisions of Income Tax Act, 1961 as applicable from time to time.
                        <br />

                    The Subcontractor undertakes to comply with all applicable Tax Laws as in force from time to time and Contractor orEmployer shall not be held responsible in any manner whatsoever for the Subcontractor’s non-compliance of various Tax Laws & Rules, being framed from time to time. In the event, rates are inclusive of royalty, the same shall be paid only to the extent of submission of proof of deposit of royalty with the concerned Government Department by the subcontractor.  In case any statutory levies like labour cess, royalties, Works Contract Tax or  any  other  taxes, levies, cess or  indirect tax   etc. are recovered by the Employer from Contractor’s Bills or in case such where  the same are paid by the Contractor, recoveries attributed  to  subcontractor’s  work  shall be made by the Contractor from the Subcontractor’s invoices or  shall be paid by the Subcontractor to the Contractor or shall without prejudice to any other method of recovery, be deducted by the Contractor from any amount due or which may become due to the Subcontractor or shall be recovered as a debt.  Any indirect tax liability incurred or paid or payable by Contractor or Employer due to such failure on the part of the Subcontractor including interest & penalty charges, if any, shall be paid by the Subcontractor to the Contractor or shall without prejudice to any other method of recovery, be deducted by the Contractor from any amount due or which may become due to the Subcontractor or shall be recovered as a debt. For this purpose, the Contractor shall also have the right to invoke Cross Fall Breach and Set Off provisions of Clause 23.
                        <br />
                    The Subcontractor shall have its books of accounts audited by a qualified, competent and independent Chartered Accountant and submit certificate of audit report / certificate from such independent Chartered Accountant.
                </p>
                <p>


                    <b>14.PAYMENT :</b>&nbsp;
                        <br />
                    The amount payable to the Subcontractor against the bill shall be released only on his submitting the following document (a) Tax Invoice as per Clause 13.1 (b) No Claim Undertaking confirming that the Subcontractor has no claims of any nature against the work order and/or the work done against the work order. (c) Unconditional acceptance of the final bill and measurements entered therein. (d) Unconditional Undertaking that the Subcontractor has complied in full with the Legal / Statutory obligations with regard to the labour engaged by him on the job including payment of terminal benefits, if any, and that he indemnifies Contractor / Employer of any obligation / liability that may arise owing to any representation of any workmen employed by him.  (e) A Clearance Certificate from Contractor stores confirming no shortfall of any material issued to him for the purpose of the work.
                        <br />
                    Payments which would otherwise be due to Subcontractor under  the  Work Order Contract  or any other contractual arrangement between Contractor and Subcontractor may be withheld or offset in whole or in part by Contractor on account of: (a) defective materials or work by Subcontractor, (b) claims or liens, or any notice thereof, whether accurate or spurious against Subcontractor, (c) any breach by Subcontractor of any provision or obligation  the  Work Order Contract  or any other contractual arrangement, (d) any doubt that the Subcontract Work can be completed for the balance of the unpaid Contract Amount, or  (e) any reasonable doubt by Contractor that the Subcontractor, for any reason, is unable to complete the Subcontract Work. If the foregoing causes are remedied or adjusted to Contractor's satisfaction, the withheld payments shall be made. If the said causes are not so remedied or adjusted, Contractor may, in addition to any other remedy including termination of the Work Order, remedy the same from Subcontractor's account and charge the entire cost thereof to Subcontractor.
                        <br />
                    Unless otherwise specified in the  Work Order, eligible payment will be released to the Subcontractor by the Contractor within 30 (Thirty ) days after receiving corresponding payment from the Employer/EPC Contractor subject to any statutory and other deductions and monies owed by the Subcontractor to the Contractor including recoveries against mobilization or plant advance, if any; interest on mobilization or plant advance, if any; cash retention, security deposit, deduction of monies due to the Contractor towards any plant, machinery, materials or services arranged by the Contractor on behalf of the Subcontractor and damages/costs levied by the Contractor / the Client / Government  Authority, if any. Notwithstanding any other provision contained herein to the contrary or otherwise, no payment shall be made to the Subcontractor unless corresponding payment against the Work Order has been received by the Contractor from the Client.  Any Payment by the Contractor to the Subcontractor shall be ‘on Account’ payment and no payment made by the Contractor hereunder shall be deemed to constitute acceptance by the Contractor of the Works or any part thereof.  The Contractor reserves its right to withhold payments due to the Subcontractor, if the Subcontractor has failed to perform in accordance with the Work Order Contract and/or has failed to remedy the defects to the satisfaction of the Contractor.  
                        <br />
                    For final payment, when the work covered by the  Work  Order  has been completed, Sub-Contractor shall prepare a final abstract showing the total amount of work done and its value under and according to the terms of the  Work  Order Contract. Sub-Contractor shall attach copies of Contractor’s completion certificates with its final abstract. From the total value thus arrived, all previous payments shall be deducted and all deductions made in accordance with the provisions of   the  Work Order Contract   and the remaining shall be paid by Contractor to Sub-Contractor within  one month of the date of submission of Sub-Contractor’s Final Bill  subject  to  receipt  of  corresponding payment against  the  Work Order  by the Contractor from the Employer/EPC Contractor. The Final Payment shall be subject to the Taking Over Certificate for the Subcontract Works.
                </p>
                <p>
                    <b>15.RETENTION :</b>&nbsp;Unless otherwise specified in the  Work Order , an amount equivalent to 5% ( five percent ) of the gross value (including escalation payable, if applicable ) of all invoices, running account bill / interim progress bill shall be deducted from all invoices / running account bill / interim progress bills towards Retention. The retention money so withheld shall be returned without interest to the Subcontractor after completion of Defect Liability Period or O&M Period whichever applicable and Subcontractor’s fulfilling all its obligations, duties, responsibilities and liabilities under the Work Order Contract and signing of ‘No Claim Certificate’ as per the format of the Contractor. In case of failure on the part of the Subcontractor to remedy the defects, the Contractor reserves its right to utilize this retention amount for carrying out the rectification works or discharging obligations, duties, responsibilities or liabilities of the Subcontractor and for this purpose the Contractor shall also have the right to invoke Cross Fall Breach and Set Off provisions of Clause 23.
                </p>
                <p><b>16.CLAIMS :</b>&nbsp;Notice for Claims, if any, shall be served by the Subcontractor to Authorized Signatory of the Contractor at its office situated at Bangalore within 14 (Fourteen) days of the occurrence of the event. Claims, if any, shall be substantiated with supporting documents along with the statement which shall state the amount due and payable to the Subcontractor under the Work Order. If the Subcontractor do not submit its claim(s) for whatsoever reason within the stipulated time as above to the Authorized Signatory at its Office, the same shall be deemed to be waived. Notwithstanding any other provision contained herein to the contrary or otherwise, no claims of the Subcontractor shall be admitted (nor deemed to be payable) unless the Employer admits and pays the corresponding claims of the Contractor. Notwithstanding any other provision contained herein to the contrary or otherwise, the Subcontractor shall not be paid any idling charges whatsoever under whatever nomenclature called.    </p>


            </div>
            <div class="footecls" style="margin-top: 0px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 10px;">
                    <div style="height: 70px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign11" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo13"></asp:Label></b>
                </div>
                <p><b>17.TIME FOR COMPLETION :</b>&nbsp;Time shall be of the Essence of Subcontract Work. The Subcontractor shall commence the execution of the Works within 7 (Seven) days of date of the Work Order and shall proceed with the Works with due expedition and without any delay. The Subcontractor shall complete the whole of the Works fit for the purpose or a Section/Milestone thereof (as the case may be) where Milestone completion dates are stipulated, as stated in the Work Order and shall always include the mobilization period and monsoon period as well as, including achieving the passing of the tests during  execution and  on Completion, and  completing all work which is stated in the Work Order  as being required for the Works or Section/Milestone thereof to be considered to be completed for the purposes of taking-over. In the event of any delay, the Subcontractor shall at all times ensure that  (i)  it uses and continues to use all reasonable endeavors to avoid or reduce the effect of any delay on the completion of the Works or a Section/Milestone thereof (as the case may be) where Milestone completion dates are stipulated in Work  Order  and (ii)  subcontractor or vendor appointed by it shall re-Programme or adjust their performance for subcontracted works or services and take measures to avoid or reduce the effect of any delay on the completion of Works or a Section/Milestone thereof (as the case may be) where Milestone completion dates are stipulated in  Work  Order. The Subcontractor shall not be entitled to an extension of time in respect of any cause of delay nor for any period of delay.    </p>
                <p><b>18.EXTENSION OF TIME FOR COMPLETION :</b>&nbsp;No extension of time shall be granted to the Subcontractor except in the events (a) the Contractor is not being given possession of the Project Site or any part thereof in accordance with requirement and necessity of such Project Site or any part thereof ; (b) conditions constituting Force Majeure Events; except where such delays are due to any act, omission, negligence, default, or breach of the Subcontractor itself or any of its subcontractor or supplier or any of their servants or agents ; (c) a Variation is instructed (provided such variation is not capable of execution as simultaneous/parallel activity) except  where such Variation is necessitated as  a consequence of any default or breach of the Agreement by the Subcontractor.  Any extension of time shall be at the discretion of the Contractor and subject to invocation and continuation of any or all of the contractual or legal rights of the Contractor Notwithstanding any other provision contained herein to the contrary or otherwise, no extension of time shall be granted (nor deemed to be granted) unless the Contractor receives corresponding extension of time from the Employer.  Time shall continue to be of the Essence of the Subcontract also to any such extension granted by the Contractor.    </p>
                <p><b>19.TAKING OVER OF THE WORKS :</b>&nbsp;Until the issuance of Taking Over Certificate for the entire Project or in part by the Employer, no sections or part thereof done by the Subcontractor shall be deemed to be completed or deemed to be accepted or deemed to be taken over by the Contractor.    </p>
                <p><b>20.DELAY DAMAGES :</b>&nbsp;The Subcontractor shall complete the whole of the subcontracted works fit for the purpose and each Section/Milestone thereof within the stipulated time mentioned in work order for completion of Works. Unless otherwise specified in the Work Order, in the event of delay in completion of work beyond the contractual time period of completion stipulated in the Work Order schedule which covers time & milestone schedule, the Subcontractor shall pay delay damages to the Contractor for this default at 2% of Accepted Subcontract Amount per week beyond stipulated time for completion. Delay damages, without prejudice to any other method of recovery, be deducted by the Contractor from any amount due or which may become due to the Subcontractor or shall be recovered as a debt. For this purpose, the Contractor shall also have the right to invoke Cross Fall Breach and Set Off provisions of Clause 23.</p>
                <p><b>21.DEFECT LIABILITY PERIOD/O&M PERIOD :</b>&nbsp;Unless otherwise specified in the Work Order, Defects Liability Period (DLP) shall be 12 (twelve) months from the date of completion & O&M shall be 60 (Sixty) months from the date of completion of Subcontract Works. The Subcontractor, during such period, shall be liable for any defects in the material, equipment or workmanship and the removal, proper rectification / repair and replacement of the same as shall be necessary, notwithstanding any previous tests thereof, if in the opinion of the Contractor the same is not in accordance with the Work Order. The Subcontractor shall carry out all the removal, repairs, rectification and replacement on this account at no extra cost to the Contractor whatsoever. In the event the Subcontractor does not remedy the defects within a reasonable time, the Contractor reserves its right to carry out such rectification either on its own or through any other party at the risk, cost and consequences of the Subcontractor as well as recover all costs and losses from the Subcontractor utilizing the retention money and/or invoking any or all Bank Guarantee (ies), (i.e. Performance or Advance or Retention or Security Deposit) and/or adjusting against any payments due to the Subcontractor without prejudice to any other method of recovery, be deducted by the Contractor from any amount due or which may become due to the Subcontractor  or shall be recovered as a debt by any other means as deemed fit and for this  purpose  the Contractor shall also have right to invoke Cross Fall Breach and Set Off provisions of Clause 23.</p>
                <p><b>22.NON-PERFORMANCE :</b>&nbsp;In case the Subcontractor does not commence the work within dates stipulated in work Order or does not mobilize adequate resources, or does not progress satisfactorily, or does not make good shortfall in achievement of required progress or does not follow safe practices or does not , to  the  Satisfaction of  Engineer , execute work in accordance with the specification drawings , codes and standards or does not protect the environment or does not maintain quality of work or neglects or does not comply with instructions, or do not make payments to their workmen and other creditors, or do not comply and observe statutory Laws of the place of work or fails to comply with any of the terms and condition of Work Order Contract  or in case of any other form or non-performance by the Subcontractor, then the Contractor reserves its rights to take any or all of the following remedial measures (not necessarily in the same order) by serving 7 (Seven) day Notice; at the risk, cost and consequences of  the Subcontractor (a) Curtail or reduce the Scope Of Work ; or/and (b) Retain the Subcontractor’s Equipment and carry out the balance work in full or in part (including any rectification in work already executed by the Subcontractor) either on its own or through any other party at the risk, cost and consequences of the Subcontractor  as well as recover all costs and losses from the Subcontractor ; or/and (c) Confiscate Cash Retention money/ Security Deposit ; or/and (d) Invoke any or all Bank Guarantee (ies) (i.e. Performance or Advance or Retention or Security Deposit) ; or/and (e)  Terminate the Work Order  in accordance with Clause 30 ; or/and (f)  Any other method as appropriate for due performance.  For this purpose, the Contractor shall also have the right to invoke Cross Fall Breach and Set Off provisions of Clause 23.</p>


            </div>
            <div class="footecls" style="margin-top: 50px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 90px;">
                    <div style="height: 75px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign12" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo14"></asp:Label></b>
                </div>
                <p><b>23.CROSS FALL BREACH AND SET OFF :</b>&nbsp;The Subcontractor shall be bound by the terms of the Work Order Contract   as also any or all other Work Orders or Subcontract or Agreements or Contracts with the Contractor. The breach or default by the Subcontractor under any of the Work Orders or Subcontract or Agreements or Contracts shall be a default under all the Work Orders or Subcontract or Agreements or Contracts and the Contractor shall have the right to exercise all rights that it may be entitled to in the event of breach or default by the Subcontractor under any and all the Work Orders or Subcontract or Agreements or Contracts.The Subcontractor further agrees and confirms that, the Contractor shall at all times have the right to deduct or adjust or set off all or any monies from any due (s) payable to the Subcontractor under the Work Order and/or under any other Work Orders or Subcontract or Agreements or Contracts with the Subcontractor or any of the account of the Subcontractor or any of the Advance or Retention or Security Deposit or Bank Guarantees of the Subcontractor.    </p>
                <p><b>24.STEP IN RIGHTS OF THE CONTRACTOR :</b>&nbsp;Notwithstanding any other provision contained herein   to   the   contrary   or   otherwise,the Contractor  and  the  Subcontractor agree that, the Contractor has the right and shall be entitled to step-in with respect to the Work Order Contract , in its sole discretion, in substitution/ replacement of the Subcontractor upon the occurrence or non-occurrence of an event, which in the opinion of the Contractor, would require such step-in, or in the event of any default by the Subcontractor or breach of Representation and Warranties or Suspension or Termination of the Work Order Contract. Furthermore, the Contractor shall have lien rights over the Subcontractor’s Documents, Equipment and materials. Notwithstanding any other provision contained herein to the contrary or otherwise, the Contractor may enter upon the Site and complete the outstanding Works either on its own or through any other party at the risk, cost and consequences of the Subcontractor as well as recover all costs and losses from the Subcontractor. The Contractor may, to the exclusion of any right of the Subcontractor over the same, take over and use without any payment to the Subcontractor any of the Subcontractor’s Equipment as may be available on the Site in connection with the Works for such period as the Contractor considers expedient for the execution and completion of the Works. Upon completion of the Works or at such earlier date as the Contractor deems appropriate, the Contractor shall inform the Subcontractor that such Subcontractor’s Equipment will be returned to the Subcontractor at the Site. In the event that, Subcontractor’s Equipment is hypothecated or under a lien of any kind, the Subcontractor confirms and undertakes to inform and get the prior approvals from its lender(s) with respect to the above provision. For this purpose, the Contractor shall also have the right to invoke Cross Fall Breach and Set Off provisions of Clause 23.    </p>
                <p><b>25.INDEMNITY :</b>&nbsp;The Subcontractor hereby indemnifies and keeps indemnified and hold harmless the Contractor / Employer against and from all claims, actions, suits, demands, liabilities, charges, and any / all proceedings and any/ all losses or damages or cost or expenses (including legal fees and expenses) including third party claims that may be suffered, incurred or expected to be so suffered or incurred by  the  Contractor  / Employer on account of anything done or omitted to be done by the Subcontractor in connection with the Work Order  or any part thereof, the Works and performance of its obligations, duties, responsibilities or liabilities under  the  Work Order Contract  or otherwise.    </p>
                <p><b>26.EXCLUSIONS :</b>&nbsp;The Sub Contractor  has clearly understood that the terms and conditions herein contained, has been negotiated by and entered into on behalf of the Contractor / Contractors project team and the Sub Contractor understands and accepts that the other Partner/Director of the Contractor or any of  the Accounts Officer of the Contractor has no part to play in the negotiations and execution of these presents and accordingly is not liable at all in contract, tort or crime in respect of theWork Order Contract or any acts pertaining thereto including prior to its execution. The Sub Contractor accordingly undertakes that under no circumstances shall he/she/it sue and/or prosecute the Partner/Director of the Contractor or any of the Accounts Officer of the Contractor in respect of the breach of any terms and conditions herein or in respect of any acts, deeds or statements that preceded to the Work Order Contract and pertaining to it.    </p>
                <p><b>27.INSTRUCTIONS IN WRITING :</b>&nbsp;Instructions given by the Contractor  or  Contractor’s Engineer or  Engineer  shall be in writing, provided that, if for any reason, the Contractor  or  Contractor’s Engineer or  Engineer  considers it necessary to give any such instruction orally, the Subcontractor shall comply with such instruction. Confirmation by subcontractor in writing of such oral instruction given by the Contractor’s Engineer or Engineer, whether before or after the carrying out of the instruction, shall be deemed to be an instruction within the meaning of this Clause.  </p>
                <p><b>28.ASSIGNMENT BY THE CONTRACTOR :</b>&nbsp;The Contractor may, at any time, assign or transfer or enter into an agreement to assign or transfer the Work Order (in full or in part) or any / all of the obligations, duties, responsibilities, liabilities, rights and benefits in or under the Work Order Contract to any person without obtaining prior consent of and/or without any prior intimation to the Subcontractor. It is hereby clarified that the Contractor may, as security in favour of bank or financial institution, assign its rights to any money due or to become due under the Subcontract. The Subcontractor hereby agrees and confirms to any assignment of the Contractor of its rights and obligations to any third party(ies). The Subcontractor concurs and confirms and remains bound by any such assignment by the Contractor. </p>
                <p><b>29.NO PRIVITY OF CONTRACT OF THE SUB CONTRACTOR WITH THE EMPLOYER :</b>&nbsp;Nothing contained in Work Order Contract shall be construed as creating any privity of contract between the Subcontractor and the Employer. If the Subcontractor commits any breach of the Work Order Contract, Subcontractor shall indemnify the Contractor / Employer against any damages for which the Contractor / Employer may become liable under the Main Contract as a result of such breaches. In such an event, the Contractor shall, without prejudice to any other method of recovery, recover such damages from any amount due or which may become due to the Subcontractor or shall be recovered as a debt. For this purpose, the Contractor shall also have the right to invoke Cross Fall Breach and Set Off provisions of Clause 23 of these General Terms and Conditions.</p>
            </div>
            <div class="footecls" style="margin-top: 150px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 45px;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign13" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 70px; width: 97%" />
                </div>


            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo15"></asp:Label></b>
                </div>

                <p>
                    <b>TERMINATION :</b>&nbsp;
                        <br />
                    Notwithstanding any other provision contained herein to the contrary or otherwise and without prejudice to any other rights and remedies available to the Contractor under  the  Work Order Contract  and the Applicable Laws, the Contractor shall be entitled to terminate the Work Order , if  (i) the Main Contract is terminated for any reason whatsoever by the Employer or (ii)  Sub-contractor  abandons the Works or otherwise plainly demonstrates the intention not to continue performance of his obligations under the Contract  or (iii)  Sub-contractor’s  non  performance  as  per  Clause  22  (iv)  fails  to comply with a notice issued for Rejection or Remedial Work within 15 days after receiving it or (v)  Sub-contractor subcontracts the whole of the Works or assigns the Contract without Contractor’s consent or (vi)  Sub-contractor becomes bankrupt or insolvent, goes into liquidation, has a receiving or administration order made against him, compounds with his creditors, or carries on business under a receiver, trustee or manager for the benefit of his creditors, or if any act is done or event occurs which (under applicable Laws) has a similar effect to any of these acts or events, or (vii) Sub-contractor gives or offers to give (directly or indirectly) to any person any bribe, gift, gratuity, commission or other thing of value, as an inducement or reward: (a) for doing or forbearing to do any action in relation to the Contract. or (b) for showing or forbearing to show favor or disfavor to any person in relation to the Contract. (c) Or if any of the Subcontractor's Personnel, agent or Sub-Subcontractor gives or offers to give (directly or indirectly) to any person any such inducement or reward. However, lawful inducements and rewards to Subcontractor's Personnel shall not entitle termination.  (viii)  in case of breach by Sub-contractor (a) of any of the Representation or Warranties by the Subcontractor (b) undertakings or obligations by the Subcontractor (ix) in the event of any breach of any of terms of the Work Order Contract by the Subcontractor.
                </p>
                <p>
                    <b>30.</b> On occurrence of any of the events or circumstances stipulated in Clause 30.1 above, Contractor may, upon giving 14 days' notice to the Subcontractor, terminate the Work Order Contract and expel the Subcontractor from the Site. However, in the case of sub-paragraph (vi) or (vii), Contractor may by notice terminate the Contract immediately. Contractor 's election to terminate the Work Order Contract shall not prejudice any other rights of the Contractor. After termination, Contractor may complete the Works and/or arrange for any other entities to do so.
                        <br />
                    Upon termination, the Subcontractor shall (i) cease all further work, except for such work as the Contractor may specify for the sole purpose of protecting that part of the Works already executed, except those to be assigned to the Contractor, (ii) deliver to the Contractor, the parts of the Works executed by the Subcontractor up to the date of Termination (iii) assign to the Contractor, all right, title and benefit of the Subcontractor to the Works as at the date of Termination, and, as may be required by the Contractor , (iv) deliver to the Contractor, all Subcontractor’s Documents including drawings, specifications and standards and other documents prepared by the Subcontractor  in  respect  of  the  Work Order Contract  , if any as at the date of termination.
                        <br />
                    No amount shall be due and payable to the Subcontractor in the event of termination of the Work Order Contract  unless and until the Works as contemplated herein are completed in entirety, Taking Over Certificate for the entire Works have been issued and all payments finally due on any account to the Contractor and / or other contractor(s) engaged in respect of the outstanding Works have been finally paid and settled and the Contractor has been discharged from all liabilities in respect thereof. On completion of the outstanding Works by the Contractor and / or other contractor(s), the cost, expenses, charges (including damages) incurred for completing such outstanding Works either itself or through other contractor(s), shall be determined and final termination amount payable or recoverable shall be ascertained in following manner:
                        <br />
                    (i) The sum which is already paid to the Subcontractor under the Work Order Contract; plus the costs, expenses, charges (including damages paid to the Client) subsequently incurred by the Contractor in completing the outstanding Works, either itself or through other contractor(s), exceeds the Accepted  Subcontract Amount (corresponding to the certified work executed by the Subcontractor till the date of termination as duly audited by the Auditor of the Contractor), then the Subcontractor  shall be liable for paying such excess amounts, which is over and above the Subcontract Amount (corresponding to the certified work executed by the Subcontractor  till the date of termination as duly audited by the Auditor of the Contractor), to the Contractor. In  such  event,  The  Contractor  may  Confiscate Retention money/ Security Deposit and  shall, without prejudice to any other method of recovery, be deducted by the Contractor from any amount due or which may become due to the Subcontractor  or shall be recovered as a debt. For this purpose, the Contractor shall also have right to invoke Cross Fall Breach and Set Off provisions of Clause 23. The Subcontractor undertakes to pay the such balance amount within 30 (thirty) days of receipt of the demand notice issued by the Contractor.
                        <br />
                    (ii) the sum which is already paid to the Subcontractor under the Work Order Contract  plus the costs, expenses, charges (including damages paid to the Client) subsequently incurred by the Contractor in completing the outstanding Works, either itself or through other contractor(s), equals the Accepted Subcontract Amount (corresponding to the certified work executed by the Subcontractor  till the date of termination as duly audited by the Auditor of the Contractor), then no payment shall be due and payable by either Party to the other.
                        <br />
                    (iii) the sum which is already paid to the Subcontractor under the Work Order Contract  ; plus the costs, expenses, charges (including damages paid to the Client) subsequently incurred by the Contractor in completing the outstanding Works, either itself or through other contractor(s), is less than the Accepted Subcontract Amount (corresponding to the certified work executed by the Subcontractor till the date of termination as duly audited by the Auditor of the Contractor), then the Contractor shall pay such amount to the Subcontractor  within 90 (ninety) days of receipt of the demand notice issued by the Subcontractor.
                        <br />
                    Notwithstanding any other provision contained herein to the contrary or otherwise and without prejudice to any other rights and remedies available to the Contractor under the Work Order Contract and the Applicable Laws, no amount shall be due and payable to the Subcontractor in case any of Subcontractor’s breach or default results in the termination of the Work Order Contract and /or the Main Contract. The Subcontractor undertakes that in the event of Termination of the Work Order Contract  , the Performance Security shall be kept valid and subsisting until the expiry of 60 (Sixty) days beyond the Defect Liability Period / O&M Period from the completion of the entire Works. For this purpose, the Contractor shall also have right to invoke Cross Fall Breach and Set Off provisions of Clause 23.
                </p>
            </div>
            <div class="footecls" style="margin-top: 120px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 38px;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign14" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>
            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo16"></asp:Label></b>
                </div>
                <p><b>31.WO SPLIT-UP:</b> &nbsp;At any point of time during the execution of the order, the Contractor shall split up the W.O. without prior information to Sub Contractor and Issue some part of the W.O from its Group /Associates /Companies Where contractor has made financing/partnership arragement ensuring the total order value to the Sub Contractor from Contractor and its associates will not vary from this order/Main Contract.    </p>
                <p><b>32.FORCE MAJEURE :</b>&nbsp;In this Clause, ‘Force Majeure’ means an exceptional event or circumstance  (a)  which is beyond a Party’s control, (b) which such Party could not reasonably have provided against before entering into the Contract, (c) which, having arisen, such Party could not reasonably have avoided or overcome, and (d) which is not substantially attributable to the other Party. Force Majeure may include, but is not limited to, exceptional events or circumstances of the kind listed hereinafter, so long as conditions (a) to (d) above are satisfied  (i) war, hostilities (whether war be declared or not), invasion, act of foreign enemies, (ii) rebellion, terrorism, sabotage by persons other than the Subcontractor’s Personnel, revolution, insurrection, military or usurped power, or civil war, (iii) riot, commotion, disorder, strike or  lockout  by persons other than the Subcontractor’s Personnel, (iv) munitions of war, explosive materials, ionizing radiation or contamination by radio-activity, except as may be attributable to the Subcontractor’s use of such munitions, explosives, radiation or radio- activity, and (v)  natural catastrophes such as earthquake, hurricane, typhoon or volcanic activity.  If a Party is or will be prevented from performing its substantial obligations under the Work Order by Force Majeure, then it shall give notice to the other Party of the event or circumstances constituting the Force Majeure and shall specify the obligations, the performance of which is or will be prevented. The notice shall be given within 7 (Seven) days after the Party became aware, or should have become aware, of the relevant event or circumstance constituting Force Majeure. Each Party shall at all times use all reasonable endeavors to minimize any delay in the performance of the Contract as a result of Force Majeure. A Party shall give notice to the other Party when it ceases to be affected by the Force Majeure.</p>
                <p><b>33.NOTICES, APPROVALS, CERTIFICATES, CONSENTS, DETERMINATIONS, REQUESTS AND DISCHARGES AND OTHER COMMUNICATION :</b>&nbsp;Wherever these General terms and Conditions provide for the giving or issuing of any notices, approvals, certificates, consents, determinations, requests and discharges, unless otherwise specified; such notices, approvals, certificates, consents, determinations, requests and discharges shall be in writing and the words ‘notify’, ‘approve’, ‘certify’, ‘confirm’ ‘determine’, ‘request’ or ‘discharge’ shall be construed accordingly. All communication from the Contractor to the Subcontractor shall be delivered by hand (against receipt), or sent by Registered A. D. Post or by courier service or through official electronic mail to the Authorized Officer of the Subcontractor as notified by the Subcontractor  from time to time. All communication from the Subcontractor to the Contractor shall be delivered by hand (against receipt), or sent by Registered A. D. Post or by courier service or through official electronic mail.  Day to day communication shall be addressed to the Authorized Representative of the Contractor at the Project Site. Contractual correspondence or notice for claim which may involve financial implications either now or at a later date shall be addressed to the Authorized Signatory of the Contractor at its Office situated at United Global Corporation Ltd., No.399, 1st Floor, 24th Cross, Banashankari 2nd Stage, Bangalore,India – 560070.  Further it is expressly agreed that no notice as regards claims, if any, from the Subcontractor which may involve financial implications either now or at a later date shall be treated as valid unless the same is served within 14 (Fourteen) days of occurrence of the event and served to the Authorized Signatory of the Contractor at its  Office situated at  Bangalore and that any notice not served within the above specified time limit or not served to the Authorized Signatory of the Contractor at its Office situated  at Bangalore shall be treated as invalid.     </p>
                <p><b>34.APPLICABLE LANGUAGE :</b>&nbsp;The ruling language of the Work Order including all documents thereof and for all further correspondence between the Contractor and the Subcontractor shall be English.    </p>
                <p><b>35.GOVERNING LAWS :</b>&nbsp;The Work Order shall be governed by the Laws of India and courts in Bangalore shall have exclusive jurisdiction over all matters arising out of or relating to the Work Order Contract. Notwithstanding the place where the Work Order is signed or the place where the work under the Work Order is to be executed, it is mutually understood and agreed by and between the Parties hereto, that the Work Order Contract shall be deemed to have been entered into by the Parties concerned in the City of Bangalore.  </p>
                <p><b>36.PRINCIPAL TO PRINCIPAL :</b>&nbsp;It is agreed and understood that, as between the Contractor / Employer and the Subcontractor, the legal relationship is strictly on a principal-to-principal basis. Nothing is deemed to constitute or imply any other legal relationship such as principal- agent, master-servant or otherwise. It is expressly agreed that there shall be no principal- agent, master-servant or any other relationship between the Contractor and the Subcontractor under these General Terms and Conditions and Work Order and no representation to any such effect would be made by the Subcontractor to anyone.  The Subcontractor shall indemnify the Contractor / Employer against any claims, expenses, liabilities and losses and for any third-party claims regarding and/or arising under or in connection with the relationship and/or misrepresentation thereby by the Subcontractor. </p>
                <p><b>37.CONFIDENTIAL DETAILS :</b>&nbsp;All matters related to the Work Order Contract, the Works and all other documents shall be regarded by the Subcontractor as being highly confidential and shall not be disclosed to any party, person or entity. The Subcontractor shall at no time hereinafter use any technical information or intellectual property, in relation to the Work Order Contract and the Main Contract. The Subcontractor shall not have the right to advertise or otherwise permit the dissemination of publicity concerning the Work Order Contract.This Confidentiality obligation shall survive expiry or termination of the Work Order.  </p>
            </div>
            <div class="footecls" style="margin-top: 180px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 20px;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign15" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>

            <div class="instract">
                <div style="width: 100%">
                    <br />
                    <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
                </div>
                <div style="text-align: right; padding-right: 30px">
                    <b>Page No:
                        <asp:Label runat="server" ID="lblPageNo17"></asp:Label></b>
                </div>
                <p><b>38.DISPUTERE SOLUTION AMICABLE SETTLEMENT AND ARBITRATION :</b>&nbsp;In case of any difference or dispute, both Parties shall attempt to settle the difference or dispute amicably before the commencement of Arbitration. Except where otherwise provided for in  the  Work Order Contract   , all questions and disputes relating to the meaning of the specifications, designs, drawings and instructions herein before mentioned and as to the quality of workmanship or materials used on the work or as to any other question, claim, right, matter or thing whatsoever in any way arising out of or relating to the Work Order Contract  , designs drawings, specifications, estimates, instructions, orders or these conditions of otherwise concerning the works, or the execution or failure to execute the same whether arising during the progress of the work or after the completion or abandonment thereof; after written notice by either Party to the Contract, shall be referred to a  person  appointed  by  the  Employer  to  act  as arbitrator in accordance with the terms of the Contract. It is also a term of the Work Order Contract that no person other than a person appointed by Contractor as aforesaid should act as arbitrator and if for any reason, that is not possible, the matter is not be referred to arbitration at all. It is a term of the Work Order Contract that the Party invoking arbitration shall specify the dispute or disputes to be referred to arbitration under this Clause together with the amount or amounts claimed in respect of each such dispute. The arbitrator may from time to time with consent of the Parties enlarge the time, for making and publishing the award. The work under the Work Order shall, if reasonably possible, continue during the arbitration proceedings. The venue of arbitration shall be Bangalore. The award of the arbitrator shall be final, conclusive and binding on all Parties to these General terms and Conditions. The cost of arbitration shall be borne by the parties to the dispute, as may be decided by the arbitrator. Subject as aforesaid, the provisions of the Arbitration and Conciliation Act, 1996, or any statutory modification or re-enactment thereof and the rules made there under and for the time being in force shall apply to the arbitration proceeding under this Clause.     </p>
                <p><b>39.ACCEPTANCE OF W.O. :</b>&nbsp;The W.O and the terms herein shall be deemed to be accepted by the Sub-Contractor upon the receipt of the W.O. or advance payment to his account unless expressly rejected by Sub-Contractor in writing or Sub-Contractor has returned the advance received for the work.    </p>
                <p>
                    <b>40.PARTIES HERETO AGREES THAT UNLESS OTHERWISE SPECIFICALLY MENTIONED IN THE WORK ORDER:</b>
                    <br />
                    (i)	These general terms and conditions shall form part of any resulting Work Order placed by Contractor on Subcontractor.  
                        <br />
                    (ii)	All the work order  issued  by  Contractor  to  sub-contractor  during  the  period of the work  from  the  date  of  acceptance  of  these  General  Terms and  Conditions  of  Work  Order  shall  be  strictly  governed  by  the  terms  and  conditions  of  these  General Terms and Conditions.
                        <br />
                    (iii)	No term or condition stated in any subcontractor solicitation shall become part of work order or shall otherwise be binding on Contractor.
                        <br />
                    (iv)	Contractor’s failure to object to any term or condition contained in any communication from subcontractor shall not be construed as Contractor’s consent to such term or condition, nor shall it be deemed a waiver by Contractor of any term or condition set forth herein.
                        <br />
                </p>


            </div>
            <div class="footecls" style="margin-top: 525px;">
                <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 52px;">
                    <div style="height: 80px">
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign16" Width="150px" Height="80px" runat="server" />
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
                    <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                </div>

            </div>




            <table border="0" style="width: 830px; text-align: justify; display: none" id="instruct1">







                <tr>
                    <td class="firsttd"></td>
                    <td>



                        <div class="invoiceSignature" style="width: 95%; font-weight: bold; margin-top: 125px;">
                            <div style="height: 80px">
                                <div style="text-align: right">
                                    <asp:ImageButton ID="imgBtnDigitalSign17" Width="150px" Height="80px" runat="server" />
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
                            <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 97%" />
                        </div>

                    </td>
                </tr>




            </table>
            <div style="width: 100%">
                <br />
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 80px;" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo18"></asp:Label></b>
            </div>
            <table border="0" style="width: 550px; border: 1px solid white">

                <tr>
                    <td colspan="2" style="text-align: center; font-weight: bold;">AGREED AND ACCEPTED
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%; font-weight: bold; padding-left: 30px">For Sub-Contractor
                        <div style="height: 50px; width: 180px;"></div>
                    </td>

                    <td style="width: 50%; font-weight: bold; padding-left: 30px; text-align: center">For Contractor
                        <div style="text-align: right">
                            <asp:ImageButton ID="imgBtnDigitalSign18" Width="150px" Height="80px" runat="server" />
                            <%--  <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                        </div>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="padding-left: 30px">Name:</td>
                    <td style="padding-left: 105px">Name:</td>
                </tr>
                <tr>
                    <td style="padding-left: 30px">Designation:</td>
                    <td style="padding-left: 105px">Designation:</td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <br />

            <div class="invoiceSignature" style="width: 90%; font-weight: bold; margin-top: 700px;">
                <div style="height: 4px">
                </div>
                <div style="float: left">
                    Receiver's Signature
                </div>
            </div>

            <div class="invoiceFooter">
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 100%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <div id="divToPrint5">
        <div id="divTableDataHolder5" style="width: 880px">
            <div style="width: 100%">
                <br />
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 110px;" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo19"></asp:Label></b>
            </div>
            <b><u>ANNEXURE-C</u> </b>
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
    associated contractors at UGCL or UGCL’s Customer location executed wide Work Order Number<span>
        <asp:Label runat="server" Text="" ID="lblWONo3"></asp:Label></span>  dated <span>
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

            <div class="invoiceSignature" style="width: 90%; font-weight: bold; margin-top: 40px;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign19" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <div id="divToPrint6" runat="server">
        <div id="divTableDataHolder6" style="width: 880px">

            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo20"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <b>ANNEXURE-D</b>
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl490">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint3" AllowPaging="true" PageSize="9000" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign20" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <div id="divToPrint7" runat="server">
        <div id="divTableDataHolder7" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo21"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint4" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true" Align="ce"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign21" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 2nd page --%>
    <div id="divToPrint8" runat="server">
        <div id="divTableDataHolder8" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo22"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint8" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign22" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 3rd page --%>
    <div id="divToPrint9" runat="server">
        <div id="divTableDataHolder9" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo23"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint9" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign23" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 4th page --%>
    <div id="divToPrint10" runat="server">
        <div id="divTableDataHolder10" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo24"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint10" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign24" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 5th Page --%>
    <div id="divToPrint11" runat="server">
        <div id="divTableDataHolder11" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo25"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint11" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign25" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 6th Page --%>
    <div id="divToPrint12" runat="server">
        <div id="divTableDataHolder12" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo26"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint12" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign26" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 7th Page --%>
    <div id="divToPrint13" runat="server">
        <div id="divTableDataHolder13" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo27"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint13" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign27" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 8th Page --%>
    <div id="divToPrint14" runat="server">
        <div id="divTableDataHolder14" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo28"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint14" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign28" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 9th Page --%>
    <div id="divToPrint15" runat="server">
        <div id="divTableDataHolder15" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo29"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint15" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign29" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 10th Page --%>
    <div id="divToPrint16" runat="server">
        <div id="divTableDataHolder16" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo30"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint16" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign30" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 11 Page--%>
    <div id="divToPrint17" runat="server">
        <div id="divTableDataHolder17" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo31"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint17" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign31" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 12 Page--%>
    <div id="divToPrint18" runat="server">
        <div id="divTableDataHolder18" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo32"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint18" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign32" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 13 Page--%>
    <div id="divToPrint19" runat="server">
        <div id="divTableDataHolder19" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo33"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint19" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign41" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 13 Page--%>
    <div id="divToPrint20" runat="server">
        <div id="divTableDataHolder20" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo34"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl4">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint20" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign33" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 14 Page--%>
    <div id="divToPrint21" runat="server">
        <div id="divTableDataHolder21" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo35"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl21">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint21" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign34" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 15 Page--%>
    <div id="divToPrint22" runat="server">
        <div id="divTableDataHolder22" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo36"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl36">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint22" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign35" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 16 Page--%>
    <div id="divToPrint23" runat="server">
        <div id="divTableDataHolder23" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo37"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl37">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint23" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign36" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 17 Page--%>
    <div id="divToPrint24" runat="server">
        <div id="divTableDataHolder24" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo38"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl38">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint24" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign37" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 18 Page--%>
    <div id="divToPrint25" runat="server">
        <div id="divTableDataHolder25" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo39"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl39">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint25" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign38" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 19 Page--%>
    <div id="divToPrint26" runat="server">
        <div id="divTableDataHolder26" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo40"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl40">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint26" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign39" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>

    <%-- 20 Page--%>
    <div id="divToPrint27" runat="server">
        <div id="divTableDataHolder27" style="width: 880px">

            <br />
            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width: 98%; height: 110px; padding-top: 30px;">
            </div>
            <div style="text-align: right; padding-right: 30px">

                <b>Page No:
                    <asp:Label runat="server" ID="lblPageNo41"></asp:Label></b>
            </div>
            <div class="mypageadjustthree">
                <br />
                <b>SCOPE OF WORK</b>
                <br />
                <br />
                <div class="mypageadjust">
                    <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl41">

                        <tr>
                            <td style="padding: 0;">
                                <asp:GridView ID="GridPrint27" runat="server" Width="853.59px" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                                    <Columns>
                                        <ogrid:Column DataField="Scope_Of_Work" HeaderText="Description" ItemStyle-Width="590px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="97.5px" Wrap="true"></ogrid:Column>
                                        <ogrid:Column DataField="Rate" HeaderText="Rate" ItemStyle-Width="81.53px"></ogrid:Column>
                                        <ogrid:Column DataField="UOM_Name" HeaderText="UOM" ItemStyle-Width="84.7px" Wrap="true"></ogrid:Column>

                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <asp:ImageButton ID="imgBtnDigitalSign40" Width="150px" Height="80px" runat="server" />
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
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height: 80px; width: 90%" />
            </div>

        </div>
    </div>
    <div class="page-break"></div>


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

