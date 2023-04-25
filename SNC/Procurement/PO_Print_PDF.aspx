<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="PO_Print_PDF.aspx.cs" Inherits="PO_Print_PDF" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .page-break {
            display: none;
        }
        /*.page-break {
            display: block;
            page-break-before: always;
        }*/

        table#instruct tr td {
            padding: 0 !important;
        }
        table#instruct tr td {
            font-size: 14px;
        }

        table#instruct {
            border: none;
        }

        table#ContentPlaceHolder1_GridPrint {
            /*border: 1px 1px solid #fff !important;*/
            border-top: 1px solid #fff;
            border-left: 1px solid #fff;
        }

        table#instruct tr td {
            padding: 10px;
        }

        table#ContentPlaceHolder1_GridPrint tr th:nth-child(7) {
            border-right: 1px solid #fff;
        }

        table#ContentPlaceHolder1_GridPrint tr td:nth-child(7) {
            border-right: 1px solid #fff;
        }

        table#ContentPlaceHolder1_GridPrint tr td {
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

            table#ContentPlaceHolder1_GridPrint {
                border: 1px 1px solid #fff !important;
                border-top: 1px solid #fff !important;
                border-left: 1px solid #fff !important;
            }

                table#ContentPlaceHolder1_GridPrint tr th:nth-child(7) {
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

            tr {
                page-break-before: always;
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
                display: block;
                page-break-before: always;
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
                page-break-before: always;
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
            height: 955px;
        }
        .mypageadjusttwo {
            min-height: 880px;
        }

        #ContentPlaceHolder1_div_Watermark{
            opacity: 0.2;
            color: BLACK;
            position: fixed;
            top: 40%;
            left: 15%;
            font-size: 80px;
        }
      
    </style>


    <div id="divToPrint" runat="server">

        <div id="divTableDataHolder" style="width: 880px">

            <div style="width: 100%;">
                <img src="../Style/Images/logo123.jpg" alt="PO Header" style="width: 100%; height: 120px" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: 1 of 6</b>
            </div>
            <div class="mypageadjust">
            <div runat="server" id="div_Watermark" visible="false">DRAFT CONFIDENTIAL</div>
           
            <b>PURCHASE ORDER</b>

            <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl1">
                <tr>
                    <td rowspan="4" style="width: 50%">Invoice To<br />
                        <b>United Global Corporation Limited</b>
                        <br />
                        Formerly Known As United Infra Corp. (BLR) Ltd.
                        <br />
                        # 399, White Gold ,1st Floor
                        <br />
                        24th Cross, Bsk 2nd Stage, Bangalore-560070
                        <br />

                        GSTIN : 29AABCU5251F1ZC<br />
                        TAN : BLRU02926A
                        <br />
                        State Name : Karnataka, Code : 29
                        <br />
                        Email: accounts@lpgroup.co.in 
                            
                    </td>
                    <td style="width: 25%">Purchase Order No.<br />
                        <asp:Label runat="server" Text="" ID="lblpurchaseorderid"></asp:Label>
                    </td>
                    <td style="width: 25%">Dated<br />
                        <asp:Label runat="server" Text="" ID="lbldate"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>Other Reference(s)</td>
                </tr>
                <tr>
                    <td>Supplier's Ref./Order No.<br />
                        <asp:Label runat="server" Text="" ID="lblQuotNo"></asp:Label>
                    </td>
                    <td>Destination<br />
                        <asp:Label runat="server" ID="lblDestination"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Despatch through
                        
                    </td>
                    <td style="border-bottom-color: white"></td>
                </tr>

                <tr>
                    <td rowspan="4" style="width: 50%">Despatch To<br />
                        <b>United Global Corporation Limited</b>
                        <br />
                        Formerly Known As United Infra Corp. (BLR) Ltd.
                        <br />
                        <asp:Label runat="server" Text="" ID="lbldispatchAdvice"></asp:Label>
                        <br />
                        GSTIN :
                        <asp:Label runat="server" Text="" ID="lblGSTIN"></asp:Label><br />
                        State Name : Karnataka, Code : 29
                    </td>
                    <td colspan="2">Terms of Delivery &nbsp &nbsp
                        <asp:Label runat="server" Text="Label" ID="lbldelscheduled"></asp:Label>
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
                <tr>
                    <td colspan="2" style="height: 30px"></td>
                </tr>
                <tr>
                    <td>
                        <div class="myadddresspart">
                            Supplier<br />
                            <b>
                                <asp:Label runat="server" Text="Label" ID="lblvendorname"></asp:Label></b>
                            <br />
                            <asp:Label ID="lblAddline" runat="server"></asp:Label>,
                            <asp:Label ID="lblcity" runat="server"></asp:Label>
                            -
                            <asp:Label ID="lblPinNo" runat="server"></asp:Label><br />
                            <%--<asp:Label ID="lblcountry" runat="server"></asp:Label>,--%>
                            Mob :
                            <asp:Label ID="lblConNo" runat="server"></asp:Label>,<br />
                            GSTIN/UIN :
                            <asp:Label ID="lblVendorTINNo" runat="server"></asp:Label><br />
                            State Name :
                            <asp:Label ID="lblState" runat="server"></asp:Label>, Code :
                            <asp:Label ID="lblStateCode" runat="server"></asp:Label>
                        </div>
                    </td>
                    <td colspan="2">
                        Other Terms<br />
                        <asp:Label runat="server" ID="lblOtherTerms" ></asp:Label>
                        
                        <%-- Extra Fields --%>
                        <asp:Label runat="server" ID="lbindentNo" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lbindentDate" Visible="false"></asp:Label>
                        <asp:Label runat="server" ID="lblQuotationRefDate" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>

            <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl2">

                <tr>
                    <td style="padding: 0;">
                        <asp:GridView ID="GridPrint" Width="100%" runat="server" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Goods and Services" ItemStyle-Width="45%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem_Name" runat="server" Text='<%#Eval("Item_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Due On">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("Due_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty_required" runat="server" Text='<%#Eval("Qty_required")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" Wrap="false" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUom" runat="server" Text='<%#Eval("UOM")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="95px" ItemStyle-HorizontalAlign="Right" HeaderText="Amount (Rs)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# decimal.Parse(Eval("Amt_With_Tax").ToString())%>'></asp:Label>
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
                    <%-- <td style="width:50%"></td>
                    <td style="width:40%"></td>--%>
                    <td style="text-align: right" colspan="7">
                        <asp:Label runat="server" Text="Label" ID="lbltotamt"></asp:Label></td>
                </tr>

                <tr>
                    <td colspan="3" style="padding: 0;">
                        <asp:GridView ID="PO_GridTax" Width="100%" ShowHeader="false" CssClass="GridCenter" runat="server" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

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
                    <td style="text-align: right; width: 50%;">
                        <p>Total</p>
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" Text="" ID="lblgrandtotal"></asp:Label></td>
                </tr>

                <tr>
                    <td colspan="3">Amount Chargeable (in words)<br />
                        <b><asp:Label runat="server" Text="" ID="lblAmountInWords"></asp:Label></b>
                    </td>
                </tr>

                <tr>
                    <td style="border-bottom: 1px solid #fff; border-right: 1px solid #fff;">
                        <p>
                            Terms and Conditions<br />
                            1. Annexure A & B Are Integral Part of This Purchase Order
                        </p>
                    </td>
                    <td colspan="5" style="vertical-align: bottom;">
                        <p style="text-align: right; font-weight: 600;">E. & O.E</p>
                    </td>
                </tr>

                <tr>
                    <td></td>
                    <td colspan="2" style="text-align: right;">
                        <p><b>For United Global Corporation Limited</b></p>
                        <div style="height: 70px">
                            <asp:Image ID="ImgAuthorisedSign" Height="80" Width="120" Visible="false" BorderColor="White" runat="server" />
                            <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:220px;"/>
                        </div>
                        <p>Authorised Signatory</p>
                    </td>
                </tr>

            </table>
            </div>
            <div class="invoiceFooter">
                <p style="margin:0">This is a Computer Generated Document</p>
                <img src="../Style/Images/bottomlogo.JPG" alt="PO Footer" style="height: 80px; width: 100%" />
            </div>

        </div>
    </div>

    
    <div class="page-break"></div>

    <div id="divToPrint1">
        <div id="divTableDataHolder1" style="width: 880px">
            <div style="width: 85%">
                <img src="../Style/Images/logo123.jpg" alt="PO Header" style="width: 100%" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: 2 of 6</b>
            </div>


            <div class="mypageadjusttwo">

            
            <b><u>ANNEXURE-A</u> </b>
            <br />
            <b><u>COMMERCIAL TERMS and CONDITIONS</u></b>
           

            <table border="0" class="table table-bordered" style="width: 830px; height: 800px; text-align: justify">
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
                        <%--<asp:Label runat="server" ID="Label1"></asp:Label>--%>
                        Billing Address: Formerly Known As United Infra Corp. (BLR) Ltd., # 399, White<br />
                        Gold ,1st Floor, 24th Cross, Bsk 2nd Stage, Bangalore-560070,<br />
                        GSTIN: 29AABCU5251F1ZC, TAN: BLRU02926A, State Name : Karnataka,<br />
                        Code : 29, E-Mail : accounts@lpgroup.co.in
                    </td>
                </tr>
                <tr>
                    <td><b>2 </b></td>
                    <td><b>Delivery Address </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblDeliveryAdd" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>3 </b></td>
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
                    <td><b>4 </b></td>
                    <td><b>Gst </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblGst"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>5 </b></td>
                    <td><b>Cess </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblCess" Text="Not Applicable"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>6 </b></td>
                    <td><b>Transportation </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblTransportation"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>7 </b></td>
                    <td><b>Packing and Forwarding </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblPackingForwarding"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>8 </b></td>
                    <td><b>Transit Insurance </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblTransitIns" Text="Inclusive"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>9 </b></td>
                    <td><b>Payment Terms </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblPaymentTerms"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>10 </b></td>
                    <td><b>Warranty </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblWarranty" Text="Applicable"></asp:Label>
                    </td>
                </tr>
               
            </table>
            </div>
            <div class="invoiceSignature" style="width: 95%; font-weight: bold">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>
                    </div>
                </div>
                <div style="float: left">
                    Supplier's Signature
                </div>
                <div style="text-align: right">
                    Authorised Signatory
                </div>
            </div>

            <div class="invoiceFooter">
                <img src="../Style/Images/bottomlogo.JPG" alt="PO Footer" style="height: 80px; width: 100%" />
            </div>

        </div>
    </div>
   
    <div class="page-break"></div>

    <div id="divToPrint2">
        <div id="divTableDataHolder2" style="width: 880px">
            <div style="width: 85%">
                <img src="../Style/Images/logo123.jpg" alt="PO Header" style="width: 100%" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: 3 of 6</b>
            </div>

            <b><u>ANNEXURE-B</u> </b>
            <br />
            <b>STANDARD TERMS and CONDITIONS</b>
            <br />
            <br />


            <table border="0" style="width: 830px; text-align: justify;" id="instruct">
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>GENERAL: </b>
                        <br />
                        &nbsp;These general terms and conditions of purchase under this Purchase Order by United Global Corporation Limited ("UGCL")
are mandatory and binding on the Supplier for sale and purchase of finished products/equipment/services ("Product or
Services"). The terms and conditions contained herein supersede the terms and conditions offered by Supplier along with
their proposal/ offer. The terms mentioned on the P.O will supersede these Standard terms and conditions or
Memorandum of Understanding signed between UGCL and Supplier.
                    </td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>PRICE:</b><br />
                        &nbsp;The price mentioned in this P.O is final and binding except on the ground of statutory levies or variations of duties/
taxes/ cess after the date of this P.O and during the contractual period.
                    </td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>GOODS AND SERVICES TAX & OTHER TAXES:</b>
                        <br />
                        &nbsp;
                        GST shall be paid at actual, against documentary evidence. UGCL may withhold from all amounts payable to Supplier all
applicable withholding taxes and to remit those taxes to the applicable governmental authorities as required by
applicable laws. The Supplier or the third party assignee shall be liable for all tax payments as required under applicable
law and for statutory filings and compliances. Failure to make tax payments resulting in UGCL's inability to claim tax
credits, Supplier shall be liable to indemnify UGCL and pay such amounts, penalties, interests and/or any other sums
accruing to UGCL due to such non-compliance and make equivalent payments to UGCL. Any default by the Supplier or a
third party assignee shall be deemed as a default of the Supplier and may result in the cancellation of the P.O and all
advance payment received, without any deduction shall be refunded by the Supplier
In case of any increase/decrease applicable in GST (CGST, SGST, IGST as applicable), customs duty, cess and other duties
or new taxes/duties/levies imposed by the Indian Government through Gazatte notification after the date of this P.O but
prior to contractual delivery date, UGCL shall reimburse/adjust the increase/decrease in taxes & duties against
supporting documents.
                    </td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>ACCEPTANCE OF P.O.:</b><br />
                        &nbsp; The P.O and the terms herein shall be deemed to be accepted by the Supplier upon the receipt of the P.O. and advance
to his account unless expressly rejected by Supplier in writing or supplier has returned the advance.
                    </td>
                </tr>

                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>ADVANCE:</b><br />
                        &nbsp;Advance, if any paid by UGCL in accordance with this P.O shall be adjusted against invoice raised by Supplier for
payment for Product or Services. Advances shall be disbursed within any date agreed between the Parties. Advance may
be paid directly to the Supplier or to third parties assignees identified and appointed by the Supplier. In the event that
an Advance is payable to a third party assignee. Supplier shall issue a Request Letter requesting for Advance to be paid
to a third party assignee for and on behalf of the Supplier. Notwithstanding anything contained herein, Supplier
acknowledges that all Advances paid to third party assignees of Supplier shall be deemed payments made to Supplier
under this P.O and Supplier shall be liable for supply of Product or return of Advance received by it or by third party
assignees instructed by it. In case UGCL issues advance payments to a third party assignee of the Supplier, the Supplier
shall simultaneously, issue a written confirmation of receipt of payment by the Supplier. Interest on Advance, unless
waived shall be payable by the Supplier in accordance with terms of the P.O. and shall be stated in the P.O. The Advance
shall be utilised solely in connection with this P.O. and cannot be adjusted, set off or utilised in any manner other than
in connection with this P.O unless expressly agreed by UGCL.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <div class="invoiceSignature" style="width: 95%; font-weight: bold;margin-top:115px;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;">
                    </div>
                </div>
                <div style="float: left">
                    Supplier's Signature
                </div>
                <div style="text-align: right">
                    Authorised Signatory
                </div>
            </div>
                        <div class="invoiceFooter">
                <img src="../Style/Images/bottomlogo.JPG" alt="PO Footer" style="height: 80px; width: 100%" />
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td><div style="width: 100%;">
                <img src="../Style/Images/logo123.jpg" alt="PO Header" style="width: 100%; height: 120px">
            </div>
   <div style="text-align: right; padding-right: 30px">
                <b>Page No: 4 of 6</b>
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>STANDARD COMPLIANCE:</b><br />
                        &nbsp;The Product / works done shall be strictly in line with the drawings/ specifications/ QAP and other documents sent along
with P.O/enquiry wherever needed, the relevant IS standards shall be followed.
                    </td>
                </tr>


                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>MANUFACTURING SCHEDULE:</b><br />
                        &nbsp;Supplier shall send us a manufacturing schedule to match the delivery schedule of our project department within 7 days
of this PO. In case, the supplier fail to abide by the agreed manufacturing schedule at any stage, UGCL has the right to
cancel the entire / part P.O. on you, without any cost implication on us and get the work done by a suitable supplier of
our choice.
                    </td>
                </tr>

                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>DELIVERY:</b><br />
                        &nbsp;Delivery basis shall be as per PO. If Transportation is in Supplier's scope, material shall be delivered at UGCL or it's
client's address and duly accepted by the QC/ Stores. Time is of the essence with respect to delivery of the Product.
Product shall be delivered and services performed by the applicable Delivery Schedule. Supplier must immediately notify
UGCL if Supplier is likely to be unable to meet a delivery date under the Delivery Schedule. At any time prior to the date
agreed in the Delivery Schedule, UGCL may, upon notice to Supplier, cancel or change a PO, or any portion thereof, for
any reason, including, without limitation, for the convenience of UGCL or due to failure of Supplier to comply with this
Agreement, unless otherwise noted.

                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>LIQUIDATED DAMAGES PRICE REDUCTION FOR DELAY IN DELIVERY:</b><br />
                        In the event Supplier fails to deliver the Equipment / Materials within the contracted delivery date in accordance with
the Delivery Schedule, the price payable in accordance with the P.O will be reduced at the rate of 1% (one percent) of
the total value of P.O per week up to a maximum of 10 (ten percent) of the total value of the PO. if the Supplier fails to
supply the finished goods no later than 60 days from the Delivery schedule, UGCL shall be entitled to cancel the PO and
in the event of such cancellation the Supplier shall be liable to pay (a) liquidated damages of 10% of the PO; (b)
differential cost incurred in procuring Product from third parties; (c) Advances with interest, if any.
                    </td>
                </tr>

                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>PAYMENT TERMS:</b><br />
                        &nbsp;Supplier will issue all invoices on a timely basis. All invoices delivered by Supplier must meet UGCL's requirements. UGCL
will, subject to adjustments on account of Advance, interest, or other deductions, pay the undisputed portion of
properly rendered invoices within forty five (45) days from the invoice date issued by Supplier or if the payment is
through LC it would be as per LC terms. UGCL shall have the right to withhold payment of any invoiced amounts that are
disputed in good faith until the parties reach an agreement with respect to such disputed amounts.
                    </td>
                </tr>

                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>INSPECTION:</b><br />
                        &nbsp;All delivery of Products and performance of services shall be subject to UGCL's right of inspection. Inspection may be
carried out by UGCL / it's client/ appointed inspection agency after submission of materials test certificates for all the
materials as per QAP along with all the Internal inspection report. Upon inspection, UGCL, or its client shall either
accept the Products or Services or reject them if the goods are not confirming to the approved QAP. UGCL or its client
shall have the right to reject any Product that are delivered in excess of the quantity ordered or are damaged or
defective. In addition, UGCL shall have the right to reject any Product or Services that are not in conformance with the
Specifications stated in the P.O. In case of any defects/issues arising out of materials workmanship / quality of
materials, etc, rectification work,if any, shall be carried out within a week from the date of identification of the defect at Supplier's cost. Supplier will
give UGCL minimum 7 working days or as specified by Customer, advance notice for inspection of raw materials /
finished goods. If UGCL or its client reject Products or Services such rejection shall be at Supplier's expense and risk of
loss and UGCL's shall at its option, either (i) demand full credit or refund of all Advance paid by UGCL to Supplier for the
rejected Product or Service; or (ii) or direct the Supplier to replace the Product to be received within the time period
specified by UGCL. Supplier shall not deliver Products or Services that were previously rejected on grounds of non
-compliance with the P.O, unless delivery of such Products is approved in advance by UGCL or its client.
                    </td>
                </tr>



                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>DISPATCH:</b><br />
                        &nbsp;Dispatch instructions will be sent to Supplier separately. Supplier shall dispatch the materials only after getting
inspection clearanceas well as dispatch clearances from us / our customer end.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <div class="invoiceSignature" style="width: 95%; font-weight: bold;margin-top:35px;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;">
                    </div>
                </div>
                <div style="float: left">
                    Supplier's Signature
                </div>
                <div style="text-align: right">
                    Authorised Signatory
                </div>
            </div>
                        <div class="invoiceFooter">
                <img src="../Style/Images/bottomlogo.JPG" alt="PO Footer" style="height: 80px; width: 100%" />
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td><div style="width: 100%;">
                <img src="../Style/Images/logo123.jpg" alt="PO Header" style="width: 100%; height: 120px">
            </div>
                         <div style="text-align: right; padding-right: 30px">
                <b>Page No: 5 of 6</b>
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>DISPATCH DOCUMENTS:</b>:&nbsp;The Product shall be dispatched and the following documents shall be submitted to UGCL on dispatch:
                        <ol type="i">
                            <li>Tax Invoice with GST for payment.</li>
                            <li>Weighing slip.</li>
                            <li>Packing list.</li>
                            <li>Inspection report.</li>
                            <li>E-Way Bill</li>
                            <li>Any other documents specifically required by UGCL</li>
                        </ol>
                    </td>
                </tr>

                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>WARRANTY:</b><br />
                        &nbsp;The Product/Service provided by Supplier shall be covered under warranty against bad workmanship, under performance
and Poor quality ofmaterials for a period of 24 months from the date of receipt of the Product or Services. In the event
the equipment does not meet the P.Orequirements, Supplier shall rectify and replace at his cost, Supplier warrants to
Buyer that during the Product or Service Warranty Period, all Products or Services provided in accordance with the P.O.
shall be:
                         <ol type="i">
                             <li>of merchantable quality;</li>
                             <li>fit for the purposes intended;</li>
                             <li>unless otherwise agreed to by UGCL;.</li>
                             <li>free from defects in design, material and workmanship:</li>
                             <li>in strict compliance with the specifications under the P.O;</li>
                             <li>free from any liens or encumbrances on title whatsoever;</li>
                             <li>in conformance with any samples provided to UGCL; and</li>
                             <li>compliant with all applicable federal, provincial, and municipal laws, regulations,standards, and codes.</li>

                         </ol>
                    </td>
                </tr>

             
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>RISK PURCHASE:: </b>
                        <br />
                        &nbsp;In the event Supplier delays delivery of the goods covered under this PO, beyond the date agreed in-the Delivery
Schedule, UGCL has the right to re-procure the goods at the risk and cost of the Supplier from any source at its
discretion. Any extra cost implication arising out of this shall be recovered from Supplier.
                    </td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>CONTRADICTIONS: </b>
                        <br />
                        &nbsp;In the event Supplier notices any contradiction in various documents, Supplier shall bring the same immediately to the
notice of UGCL for a resolution, within a week of receipt of PO. Any claim made later shall not be entertained. Such
contradiction/ discrepancy/ inconsistency shall be resolved mutually by the parties failing which, it shall be resolved
through dispute resolution mechanism as provided under this P.O.
                    </td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>CANCELLATION OF P.O: </b>
                        <br />
                        &nbsp;Without prejudice to any other rights or remedies of UGCL, UGCL shall be entitled to terminate this P.O forthwith upon a
breach of terms of the P.O by Supplier. Termination of this P.O for breach by Supplier shall not discharge supplier's
obligations under any and all other PO’s issued by UGCL to supplier,or the supplier’s obligations under this PO including
refund or advance payments and surviving provisions confidentiality indemnity, taxes, liquidated damages, governing
laws and dispute resolution.
                    </td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>LIABILITY of SUPPLIER: </b>
                        <br />
                        &nbsp;Supplier shall be liable for all losses resulting to UGCL from non-compliance with the Delivery Schedule or breach ofany
terms of the PO, including any loss of profits, to the maximum extent permissible under law. Notwithstanding anything,
no limitation or exclusion of liability shall apply with respect to any claims based on this PO arising out of the Supplier's
willful misconduct or gross negligence or with respect to any claims for personal injury or property damage, or to
Supplier's indemnification obligations stated herein.
                    </td>


                </tr>
                  <tr>
                    <td class="firsttd"></td>
                    <td>
                        <div class="invoiceSignature" style="width: 95%; font-weight: bold;margin-top:70px;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;">
                    </div>
                </div>
                <div style="float: left">
                    Supplier's Signature
                </div>
                <div style="text-align: right">
                    Authorised Signatory
                </div>
            </div>
                        <div class="invoiceFooter">
                <img src="../Style/Images/bottomlogo.JPG" alt="PO Footer" style="height: 80px; width: 100%" />
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td><div style="width: 100%;">
                <img src="../Style/Images/logo123.jpg" alt="PO Header" style="width: 100%; height: 120px">
            </div>
                        <div style="text-align: right; padding-right: 30px">
                <b>Page No: 6 of 6</b>
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>CONSEQUENTIAL DAMAGES: </b>
                        <br />
                        &nbsp;UGCL shall in no event be liable for loss of profit, loss of revenues, loss of use, loss of production, costs of capital or
costs connected with interruption of operation,loss of anticipated savings or for any special, indirect or consequential damage or loss of any nature whatsoever,
accruing to Supplier arising out of execution of this PO or cancellation of the PO due to non-performance. In any event,
UGCL's entire liability for any claim, whether in contract, tort, or any other theory of liability shall be limited to the PO
                    </td>


                </tr>
               
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>INSURANCE: </b>
                        <br />
                        &nbsp;UGCL shall not be responsible for procuring insurance for and on behalf of the Vendor or the Equipment it is the sole
responsibility of the supplier to keep all the goods and services fully insured in all respects.
                    </td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>ASSIGNMENT: </b>
                        <br />
                        &nbsp;Supplier may not assign or subcontract the P.O placed under this P.O in whole or in part, without UGCL's prior written
consent.                    </td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>CONFIDENTIALITY: </b>
                        <br />
                        &nbsp;Supplier undertakes that it shall, at all times, maintain confidentiality of all the information including but not limited to
drawings and other technical specifications received by it in respect of the PO and/or disclosed to it by UGCL or its
client and shall not disclose or divulge the same or any part thereof to any third party without the prior written consent
of UGCL. The obligations of this clause shall survive termination of this P.O, regardless of the reasons for termination of
this P.O.</td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>GOVERNING LAW & DISPUTE RESOLUTION: </b>
                        <br />
                        &nbsp;In case any dispute/s or difference/s arises between the Parties in connection with any matter relating to this PO
including termination thereof then the Parties shall, refer the dispute to arbitration and shall be governed by the
provisions of the Arbitration & Conciliation Act, 1996. The arbitration proceedings shall be adjudicated by a sole
arbitrator appointed by mutual consent of both the Parties, and the arbitration proceedings shall be held in Bangalore.
The language of arbitration shall be English. The decision of the arbitrator shall be final and binding upon the Parties</td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>COSTS AND EXPENSES: </b>
                        <br />
                        &nbsp;All costs and expenses incurred in connection with P.O. including costs incurred in recovering the advance payments shall
be to the Supplier's account.</td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>PO Splitup: </b>
                        <br />
                        &nbsp;At any point of time during the execution of the order UGCL, with prior consent of Supplier, can split up the order and
place some part of the order from its Group Companies/Associates but will ensure the total order value to the supplier
from UGCL and its associates will not vary from this order</td>


                </tr>

                <%--  <tr>
                    <td style="text-align: left;">
                        <b style="width: 15px"></b>
                    </td>
                </tr>  --%>
            </table>

            <div class="invoiceSignature" style="width: 95%; font-weight: bold;margin-top:330px;">
                <div style="height: 80px">
                    <div style="text-align: right">
                        <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>
                    </div>
                </div>
                <div style="float: left">
                    Supplier's Signature
                </div>
                <div style="text-align: right">
                    Authorised Signatory
                </div>
            </div>

            <div class="invoiceFooter">
                <img src="../Style/Images/bottomlogo.JPG" alt="PO Footer" style="height: 80px; width: 100%" />
            </div>

        </div>
    </div>


    <div class="row" style="display: none">
        <div class="col-md-12 text-center">
            <center>     
                    <asp:Button ID="btn_print" runat="server" Text="Print" Visible="false" style="background:#316eb9;color:#fff;" CssClass="btn btn-default no-print" OnClientClick="return PrintDiv();" />
                      &nbsp;&nbsp;  
                    <input type="button"  class="btn btn-default no-print"  style="background:#316eb9;color:#fff;" onclick="tableToExcel('divTableDataHolder', 'Purchase Order')" value="Export to Excel"/>
                </center>
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
