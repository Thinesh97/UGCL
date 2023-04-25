<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="RFQ_Print.aspx.cs" Inherits="SNC.Procurement.RFQ_Print" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <style>
        .page-break {
            display: none;
        }

        table#instruct {
            border: none;
        }
        table#instruct tr td {
            padding: 0px;
            font-size: 11px;
            border:none;
        }

        table#mytbl1 tr th, #mytbl2a tr th, #mytbl2b tr th,#mytbl2c tr th, #mytbl2d tr th, #mytbl2e tr th,
            #ContentPlaceHolder1_mytbl3a tr th, #ContentPlaceHolder1_mytbl3b tr th, 
            #ContentPlaceHolder1_mytbl3c tr th, #ContentPlaceHolder1_mytbl3d tr th, #ContentPlaceHolder1_mytbl3e tr th {
            font-size: 14px;
            padding: 0 10px;
        }

        table#mytbl1 tr td {
            font-size: 13px;
        } 
        
        table#mytbl2a tr td, #mytbl2b tr td,#mytbl2c tr td, #mytbl2d tr td, #mytbl2e tr td, 
            #ContentPlaceHolder1_mytbl3a tr td, #ContentPlaceHolder1_mytbl3b tr td, 
            #ContentPlaceHolder1_mytbl3c tr td, #ContentPlaceHolder1_mytbl3d tr td, #ContentPlaceHolder1_mytbl3e tr td {
            font-size: 12px;
            padding: 0 10px;
        }        

        invoiceFooter p {
            font-size: 14px;
            font-weight: 600;
        }

    </style>
	
      <style>         /*Print page style*/
        /*@page {
            _size: 8.5in 11in;
            margin-top: 0px;
        }*/

        @media all {
            .page-break {
                display: none;
            }
        }

        @media print {
              /*tr {
                page-break-before: always;
            }*/

            table#instruct tr td {
                font-size: 14px;
            }

            table#ContentPlaceHolder1_GridPrint1 tr th:nth-child(7),
                #ContentPlaceHolder1_GridPrint2 tr th:nth-child(7),
                #ContentPlaceHolder1_GridPrint3 tr th:nth-child(7),
                #ContentPlaceHolder1_GridPrint4 tr th:nth-child(7),
                #ContentPlaceHolder1_GridPrint5 tr th:nth-child(7),
                #ContentPlaceHolder1_GridPrint1 tr td:nth-child(7),
                #ContentPlaceHolder1_GridPrint2 tr td:nth-child(7),
                #ContentPlaceHolder1_GridPrint3 tr td:nth-child(7),
                #ContentPlaceHolder1_GridPrint4 tr td:nth-child(7),
                #ContentPlaceHolder1_GridPrint5 tr td:nth-child(7) {
                border-right: 1px solid #fff !important;
            }
            
            table#mytbl2a, #mytbl2b, #mytbl2c, #mytbl2d, #mytbl2e {
                margin-top: -1px;
            }

            table#mytbl1 tr th {
                font-weight: normal;
            }

             /*.page-break {
                display: block;
                page-break-before: always;
            }*/

            table#ContentPlaceHolder1_GridPrint1, #ContentPlaceHolder1_GridPrint2, 
                #ContentPlaceHolder1_GridPrint3, #ContentPlaceHolder1_GridPrint4, #ContentPlaceHolder1_GridPrint5  {
                margin-top: -2px !important;
            }

            table#mytbl1 tr th {
                font-weight: normal;
            }

             /*tr {
                page-break-before: always;
            }*/


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
        td {
            text-align: justify;
            font-family: Arial;
            font-size: 15px;
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

        #ContentPlaceHolder1_div_Watermark_Draft{
            opacity: 0.2;
            color: BLACK;
            position: fixed;
            top: 40%;
            left: 15%;
            font-size: 80px;
        }
        #ContentPlaceHolder1_div_Watermark_Cancel{
            opacity: 0.2;
            color: BLACK;
            position: fixed;
            top: 40%;
            left: 30%;
            font-size: 80px;
        }
      
    </style>


     <div id="divToPrint1a" runat="server">

        <div id="divTableDataHolder1a" style="width: 880px">

            <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 120px" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo1a" ></asp:Label></b>
            </div>
            <div class="mypageadjust">
            <div runat="server" id="div_Watermark_Draft" visible="false">DRAFT CONFIDENTIAL</div>
            <div runat="server" id="div_Watermark_Cancel" visible="false">Cancelled</div>
           
            <b>REQUEST FOR QUOTATION (MATERIAL / SERVICES)</b>

            <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl1">
                <tr>
                    <td rowspan="4" style="width: 55%"><b><u>Invoice To :</u></b><br />
                        <b>United Global Corporation Limited</b> <br />
                        Formerly Known As United Infra Corp. (BLR) Ltd. <br />
                        <asp:Label runat="server" Text="" ID="lblAddressLine1_Company"></asp:Label> <br />
                        <asp:Label runat="server" Text="" ID="lblAddressLine2_Company"></asp:Label> <br />

                        GSTIN : <asp:Label runat="server" Text="" ID="lblGSTIN_Company"></asp:Label><br />
                        TAN : <asp:Label runat="server" Text="" ID="lblTAN_Company"></asp:Label> <br />
                        State Name : <asp:Label runat="server" Text="" ID="lblState_Company"></asp:Label>, Code : <asp:Label runat="server" Text="" ID="lblCode_Company"></asp:Label> <br />
                        Email: <asp:Label runat="server" Text="" ID="lblEmail_Company"></asp:Label>
                    </td>
                    <td style="width: 25%">Request for Quotation No. :<br />
                        <b><asp:Label runat="server" Text="" ID="lblPONo1"></asp:Label></b>
                    </td>
                    <td style="width: 25%">Dated :<br />
                        <asp:Label runat="server" Text="" ID="lbldate"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Supplier's Quotation No./Date :<br />
                        <asp:Label runat="server" Text="" ID="lblQuotNo"></asp:Label>
                    </td>
                    <td>Other Reference(s) :</td>
                </tr>
                <tr>
                    <td>Place of Dispatch : <br />
                        <asp:Label runat="server" ID="lblPlaceOfDispatch"></asp:Label>
                    </td>
                    <td>Destination : <br />
                        <asp:Label runat="server" ID="lblDestination"></asp:Label>
                    </td>
                </tr>
                <tr>
                     <td colspan="2">Dispatch Mode : 
                    <asp:Label runat="server" ID="lblDispatchMode"></asp:Label></td>
                </tr>
                <tr>
                    <td rowspan="3" style="width: 50%"> <b><u>Dispatch To : </u></b>  <br />
                        <b>United Global Corporation Limited</b>
                        <br />
                        Formerly Known As United Infra Corp. (BLR) Ltd.
                        <br />
                        <asp:Label runat="server" Text="" ID="lbldispatchAdvice"></asp:Label>
                        <br />
                        GSTIN :
                        <asp:Label runat="server" Text="" ID="lblGSTIN_Company1"></asp:Label><br />
                        State Name : <asp:Label runat="server" Text="" ID="lblState_Company1"></asp:Label>, Code : <asp:Label runat="server" Text="" ID="lblCode_Company1"></asp:Label>
                    </td>
                    <td colspan="2">Terms of Delivery : &nbsp
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
                    <td colspan="2">Job No : 
                            <b><asp:Label runat="server" ID="lblJobNo"></asp:Label>&nbsp; - &nbsp</b>
                        <asp:Label runat="server" ID="lblJobDesc"></asp:Label>
                    </td>
                </tr>
                <%--<tr>
                    <td colspan="2" style="height: 30px"></td>
                </tr>--%>
                <tr>
                    <td>
                        <div class="myadddresspart">
                          <b><u>Supplier : </u></b><br />
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
                            <asp:Label ID="lblVendorGSTNo" runat="server"></asp:Label><br />
                            PAN No:
                            <asp:Label ID="lblVendorPanNo" runat="server"></asp:Label><br />
                            State Name :
                            <asp:Label ID="lblState" runat="server"></asp:Label>, Code :
                            <asp:Label ID="lblStateCode" runat="server"></asp:Label>
                        </div>
                    </td>
                    <td colspan="2"></td>
                </tr>
            </table>

            <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; margin-top:-1px; border:none" id="mytbl2a">

                <tr>
                    <td style="padding: 0; border:none;">
                        <asp:GridView ID="GridPrint1" Width="100%" runat="server" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Goods and Services" ItemStyle-Width="49.8%">
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
                                         <asp:TemplateField ItemStyle-Width="50px" HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUom" runat="server" Text='<%#Eval("UOM")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text=''></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" Wrap="false" />
                                </asp:TemplateField>

                       

                                <asp:TemplateField ItemStyle-Width="95px" ItemStyle-HorizontalAlign="Right" HeaderText="Amount (Rs)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text=''></asp:Label>
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

            <table class="table table-bordered" style="width: 97%; padding: 0; margin-top: -1px;" id="mytbl3a" runat="server">

                <tr>
                    <td style="text-align: right; width: 55%;font-weight:bold">
                        TAXABLE VALUE
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTaxableAmt1" Font-Bold="true"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="3" style="padding: 0;">
                        <asp:GridView ID="PO_GridTax1" Width="100%" ShowHeader="false" OnRowDataBound="PO_GridTax1_RowDataBound" CssClass="GridCenter" runat="server" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField ItemStyle-Width="55%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxDesc" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="32.8%" HeaderText="Rate %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxPerc" runat="server" Text='<%#Eval("Type_Perc")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxAmt" runat="server" Text='<%#decimal.Parse(Eval("Type_Amount").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;font-weight:bold">
                        TOTAL
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblAmtAfterTax1" Font-Bold="true"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">
                        (-) TDS U/S 194 Q @ 0.1%
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTDSAmt1"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right; width: 50%; font-weight:bold">
                        NET PAYABLE
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblGrandTotal1" Font-Bold="true"></asp:Label></td>
                </tr>

                <tr>
                    <td colspan="3">Amount Chargeable (in words)<br />
                        <b><asp:Label runat="server" Text="" ID="lblAmountInWords1"></asp:Label></b>
                    </td>
                </tr>

                <tr>
                    <td style="border-bottom: 1px solid #fff; border-right: 1px solid #fff;">
                        <p>
                            Terms and Conditions<br />
                            1. Annexure A & B Are Integral Part of This RFQ
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
                              <asp:ImageButton ID="imgBtnDigitalSign" Width="150px" Height="80px" runat="server" />
                           <%-- <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:220px;"/>--%>
                        </div>
                        <p>Authorised Signatory</p>
                    </td>
                </tr>

            </table>

            <div runat="server" id="div_Continue1a" style="width:90%; height:30px;">
                <p style="float:right; padding-top:10px">
                    Continued...&nbsp; &nbsp;
                </p>
            </div>
            </div>
            <div class="invoiceFooter">
                <p style="margin:0">This is a Computer Generated Document</p>
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
            </div>

        </div>
    </div>

    <div class="page-break"></div>

    <div id="divToPrint1b" runat="server" visible="false">

        <div id="divTableDataHolder1b" style="width: 880px">

            <div style="width: 100%;">
                <br />
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width:98%; height:110px"/>
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No:  <asp:Label runat="server" ID="lblPageNo1b" ></asp:Label></b>
            </div>
            <div class="mypageadjust">
            <b>REQUEST FOR QUOTATION (MATERIAL / SERVICES) - <asp:label runat="server" ID="lblPONo2"></asp:label></b>

            <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; border:none;" id="mytbl2b">

                <tr>
                    <td style="padding: 0; border:none;">
                        <asp:GridView ID="GridPrint2" Width="100%" runat="server" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Goods / Services" ItemStyle-Width="49.8%">
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
                                      <asp:TemplateField ItemStyle-Width="50px" HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUom" runat="server" Text='<%#Eval("UOM")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text=''></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" Wrap="false" />
                                </asp:TemplateField>

                          

                                <asp:TemplateField ItemStyle-Width="95px" ItemStyle-HorizontalAlign="Right" HeaderText="Amount (Rs)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text=''></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>

            </table>

            <table class="table table-bordered" style="width: 97%; padding: 0; margin-top: -1px;" id="mytbl3b" runat="server">

             <%--   <tr>
                    <td style="text-align: right; width: 55%;font-weight:bold">
                        TAXABLE VALUE
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTaxableAmt2" Font-Bold="true"></asp:Label>
                    </td>
                </tr>--%>
                
                

             <%--   <tr>
                    <td colspan="3" style="padding: 0;">
                        <asp:GridView ID="PO_GridTax2" Width="100%" ShowHeader="false" OnRowDataBound="PO_GridTax1_RowDataBound" CssClass="GridCenter" runat="server" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField ItemStyle-Width="55%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxDesc" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="32.8%" HeaderText="Rate %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxPerc" runat="server" Text='<%#Eval("Type_Perc")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxAmt" runat="server" Text='<%#decimal.Parse(Eval("Type_Amount").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>--%>

           <%--     <tr>
                    <td style="text-align: right;font-weight:bold">
                        TOTAL
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblAmtAfterTax2"></asp:Label>
                    </td>
                </tr>--%>

             <%--   <tr>
                    <td style="text-align: right;">
                        (-) TDS U/S 194 Q @ 0.1%
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTDSAmt2"></asp:Label>
                    </td>
                </tr>--%>

               <%-- <tr>
                    <td style="text-align: right; width: 50%; font-weight:bold">
                        NET PAYABLE
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" Text="" ID="lblGrandTotal2" Font-Bold="true"></asp:Label></td>
                </tr>--%>

               <%-- <tr>
                    <td colspan="3">Amount Chargeable (in words)<br />
                        <b><asp:Label runat="server" Text="" ID="lblAmountInWords2"></asp:Label></b>
                    </td>
                </tr>--%>

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
                            <asp:Image ID="Image1" Height="80" Width="120" Visible="false" BorderColor="White" runat="server" />
                              <asp:ImageButton ID="imgBtnDigitalSign1" Width="150px" Height="80px" runat="server" />
                           <%-- <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:220px;"/>--%>
                        </div>
                        <p>Authorised Signatory</p>
                    </td>
                </tr>

            </table>

            <div runat="server" id="div_Continue1b" style="width:90%; height:30px;">
                <p style="float:right; padding-top:10px">
                    Continued...&nbsp; &nbsp;
                </p>
            </div>
            
            </div>
            <div class="invoiceFooter" style="margin-top:30px;">
                <p style="margin:0;">This is a Computer Generated Document</p>
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
            </div>
          
        </div>
    </div>

    <div class="page-break"></div>

    <div id="divToPrint1c" runat="server" visible="false">

        <div id="divTableDataHolder1c" style="width: 880px">

            <div style="width: 100%;">
                <br />
                
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width:98%; height:110px"/>
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo1c" ></asp:Label></b>
            </div>
            <div class="mypageadjust">
            <b>PURCHASE ORDER - <asp:label runat="server" ID="lblPONo3"></asp:label></b>

            <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; border:none;" id="mytbl2c">

                <tr>
                    <td style="padding: 0; border:none;">
                        <asp:GridView ID="GridPrint3" Width="100%" runat="server" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Goods / Services" ItemStyle-Width="49.8%">
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

            </table>

            <table class="table table-bordered" style="width: 97%; padding: 0; margin-top: -1px;" id="mytbl3c" runat="server">

                <tr>
                    <td style="text-align: right; width: 55%;font-weight:bold">
                        TAXABLE VALUE
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTaxableAmt3" Font-Bold="true"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="3" style="padding: 0;">
                        <asp:GridView ID="PO_GridTax3" Width="100%" ShowHeader="false" OnRowDataBound="PO_GridTax1_RowDataBound" CssClass="GridCenter" runat="server" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField ItemStyle-Width="55%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxDesc" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="32.8%" HeaderText="Rate %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxPerc" runat="server" Text='<%#Eval("Type_Perc")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxAmt" runat="server" Text='<%#decimal.Parse(Eval("Type_Amount").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;font-weight:bold">
                        TOTAL
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblAmtAfterTax3" Font-Bold="true"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">
                        (-) TDS U/S 194 Q @ 0.1%
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTDSAmt3"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right; width: 50%; font-weight:bold">
                        NET PAYABLE
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" Text="" ID="lblGrandTotal3" Font-Bold="true"></asp:Label></td>
                </tr>

                <tr>
                    <td colspan="3">Amount Chargeable (in words)<br />
                        <b><asp:Label runat="server" Text="" ID="lblAmountInWords3"></asp:Label></b>
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
                            <asp:Image ID="Image2" Height="80" Width="120" Visible="false" BorderColor="White" runat="server" />
                              <asp:ImageButton ID="imgBtnDigitalSign2" Width="150px" Height="80px" runat="server" />
                            <%--<img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:220px;"/>--%>
                        </div>
                        <p>Authorised Signatory</p>
                    </td>
                </tr>

            </table>

            <div runat="server" id="div_Continue1c" style="width:90%; height:30px;">
                <p style="float:right; padding-top:10px">
                    Continued...&nbsp; &nbsp;
                </p>
            </div>
            
            </div>
            <div class="invoiceFooter" style="margin-top:30px;">
                <p style="margin:0;">This is a Computer Generated Document</p>
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
            </div>
          
        </div>
    </div>

    <div class="page-break"></div>

    <div id="divToPrint1d" runat="server" visible="false">

        <div id="divTableDataHolder1d" style="width: 880px">

            <div style="width: 100%;">
                <br />
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width:98%; height:110px"/>
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo1d" ></asp:Label></b>
            </div>
            <div class="mypageadjust">
            <b>PURCHASE ORDER - <asp:label runat="server" ID="lblPONo4"></asp:label></b>

            <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; border:none;" id="mytbl2d">

                <tr>
                    <td style="padding: 0; border:none;">
                        <asp:GridView ID="GridPrint4" Width="100%" runat="server" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Goods / Services" ItemStyle-Width="49.8%">
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

            </table>

            <table class="table table-bordered" style="width: 97%; padding: 0; margin-top: -1px;" id="mytbl3d" runat="server">

                <tr>
                    <td style="text-align: right; width: 55%;font-weight:bold">
                        TAXABLE VALUE
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTaxableAmt4" Font-Bold="true"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="3" style="padding: 0;">
                        <asp:GridView ID="PO_GridTax4" Width="100%" ShowHeader="false" OnRowDataBound="PO_GridTax1_RowDataBound" CssClass="GridCenter" runat="server" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField ItemStyle-Width="55%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxDesc" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="32.8%" HeaderText="Rate %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxPerc" runat="server" Text='<%#Eval("Type_Perc")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxAmt" runat="server" Text='<%#decimal.Parse(Eval("Type_Amount").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;font-weight:bold">
                        TOTAL
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblAmtAfterTax4" Font-Bold="true"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">
                        (-) TDS U/S 194 Q @ 0.1%
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTDSAmt4"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right; width: 50%; font-weight:bold">
                        NET PAYABLE
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" Text="" ID="lblGrandTotal4" Font-Bold="true"></asp:Label></td>
                </tr>

                <tr>
                    <td colspan="3">Amount Chargeable (in words)<br />
                        <b><asp:Label runat="server" Text="" ID="lblAmountInWords4"></asp:Label></b>
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
                            <asp:Image ID="Image3" Height="80" Width="120" Visible="false" BorderColor="White" runat="server" />
                              <asp:ImageButton ID="imgBtnDigitalSign3" Width="150px" Height="80px" runat="server" />
                           <%-- <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:220px;"/>--%>
                        </div>
                        <p>Authorised Signatory</p>
                    </td>
                </tr>

            </table>

            <div runat="server" id="div_Continue1d" style="width:90%; height:30px;">
                <p style="float:right; padding-top:10px">
                    Continued...&nbsp; &nbsp;
                </p>
            </div>
            
            </div>
            <div class="invoiceFooter" style="margin-top:30px;">
                <p style="margin:0;">This is a Computer Generated Document</p>
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height:80px; width:97%"/>
            </div>
          
        </div>
    </div>

    <div class="page-break"></div>

    <div id="divToPrint1e" runat="server" visible="false">

        <div id="divTableDataHolder1e" style="width: 880px">

            <div style="width: 100%;">
                <br />
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width:98%; height:110px"/>
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo1e" ></asp:Label></b>
            </div>
            <div class="mypageadjust">
            <b>PURCHASE ORDER - <asp:label runat="server" ID="lblPONo5"></asp:label></b>

            <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; border:none;" id="mytbl2e">

                <tr>
                    <td style="padding: 0; border:none;">
                        <asp:GridView ID="GridPrint5" Width="100%" runat="server" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Goods / Services" ItemStyle-Width="49.8%">
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

            </table>

            <table class="table table-bordered" style="width: 97%; padding: 0; margin-top: -1px;" id="mytbl3e" runat="server">

                <tr>
                    <td style="text-align: right; width: 55%;font-weight:bold">
                        TAXABLE VALUE
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTaxableAmt5" Font-Bold="true"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="3" style="padding: 0;">
                        <asp:GridView ID="PO_GridTax5" Width="100%" ShowHeader="false" OnRowDataBound="PO_GridTax1_RowDataBound" CssClass="GridCenter" runat="server" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField ItemStyle-Width="55%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxDesc" runat="server" Text='<%#Eval("Description")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="32.8%" HeaderText="Rate %">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxPerc" runat="server" Text='<%#Eval("Type_Perc")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPOTaxAmt" runat="server" Text='<%#decimal.Parse(Eval("Type_Amount").ToString())%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;font-weight:bold">
                        TOTAL
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblAmtAfterTax5" Font-Bold="true"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">
                        (-) TDS U/S 194 Q @ 0.1%
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" ID="lblTDSAmt5"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right; width: 50%; font-weight:bold">
                        NET PAYABLE
                    </td>
                    <td style="text-align: right">
                        <asp:Label runat="server" Text="" ID="lblGrandTotal5" Font-Bold="true"></asp:Label></td>
                </tr>

                <tr>
                    <td colspan="3">Amount Chargeable (in words)<br />
                        <b><asp:Label runat="server" Text="" ID="lblAmountInWords5"></asp:Label></b>
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
                            <asp:Image ID="Image4" Height="80" Width="120" Visible="false" BorderColor="White" runat="server" />
                              <asp:ImageButton ID="imgBtnDigitalSign4" Width="150px" Height="80px" runat="server" />
                            <%--<img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:220px;"/>--%>
                        </div>
                        <p>Authorised Signatory</p>
                    </td>
                </tr>

            </table>
            
            </div>
            <div class="invoiceFooter" style="margin-top:30px;">
                <p style="margin:0;">This is a Computer Generated Document</p>
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height:80px; width:97%"/>
            </div>
          
        </div>
    </div>
    
    <div class="page-break"></div>

    <div id="divToPrint2">
        <div id="divTableDataHolder2" style="width: 880px">
            <div style="width: 100%">
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 120px; padding-top:15px;" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo2" ></asp:Label></b>
            </div>


            <div class="mypageadjusttwo">

            
            <b><u>ANNEXURE-A</u> </b>
            <br />
            <b><u>SPECIFIC COMMERCIAL TERMS and CONDITIONS</u></b>
           

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
                        Billing Address: Formerly Known As United Infra Corp. (BLR) Ltd.,<br />
                        <asp:Label runat="server" Text="" ID="lblAddressLine1_Bill"></asp:Label><br />
                        <asp:Label runat="server" Text="" ID="lblAddressLine2_Bill"></asp:Label><br />
                        GSTIN: <asp:Label runat="server" Text="" ID="lblGSTIN_Bill"></asp:Label>, TAN: <asp:Label runat="server" Text="" ID="lblTAN_Bill"></asp:Label>, State Name : <asp:Label runat="server" Text="" ID="lblState_Bill"></asp:Label>,<br />
                        Code : <asp:Label runat="server" Text="" ID="lblCode_Bill"></asp:Label>, E-Mail : <asp:Label runat="server" Text="" ID="lblEmail_Bill"></asp:Label>
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
                    <td><b>GST </b></td>
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
                <tr runat="server" visible="false">
                    <td><b>7 </b></td>
                    <td><b>Packing and Forwarding </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblPackingForwarding"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>7 </b></td>
                    <td><b>Testing, Inspection & Dispatch Clearance</b></td>
                    <td>
                        <asp:Label runat="server" ID="lblTestingInspection" Text="Testing and Inspection will be conducted by Department or Third Party as applicable on issuing Inspection Call by the Supplier with due notice of 10 days to UGCL. Only after Physical Inspection / Virtual Inspection / Valid Inspection Waiver by UGCL the supplier has to plan for dispatch of material after valid written dispatch clearance from UGCL in an e-mail."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>8 </b></td>
                    <td><b>Transit Insurance </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblTransitIns" Text="Inclusive by the Supplier (If Applicable)"></asp:Label>
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
                        <asp:Label runat="server" ID="lblWarranty" Text="Minimum 24 months wherever applicable."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>11 </b></td>
                    <td><b>Other Terms </b></td>
                    <td>
                        <asp:Label runat="server" ID="lblOtherTerms" ></asp:Label>
                    </td>
                </tr>
               
            </table>
            </div>
            <div class="invoiceSignature" style="width: 90%; font-weight: bold">
                <div style="height: 80px">
                    <div style="text-align: right">
                          <asp:ImageButton ID="imgBtnDigitalSign5" Width="150px" Height="80px" runat="server" />
                       <%-- <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
                    </div>
                </div>
                <div style="float: left">
                    <%--Supplier's Signature--%>
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

    <div id="divToPrint3" style="display:none">
        <div id="divTableDataHolder3" style="width: 880px">
            <div style="width: 100%">
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 120px; padding-top:15px;" />
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo3a" ></asp:Label></b>
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
                        <b>1) GENERAL: </b>
                        <br />
                        These general terms and conditions of purchase under this Purchase Order by United Global Corporation Limited ("UGCL")
are mandatory and binding on the Supplier for sale and purchase of finished products/equipment/services ("Product or
Services"). The terms and conditions contained herein supersede the terms and conditions offered by Supplier along with
their proposal/ offer/Purchase Order acceptance of supplier. The terms mentioned on the P.O will supersede any terms signed between UGCL and Supplier earlier if any.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>2) PRICE:</b><br />
                       The price mentioned in this P.O is final and binding during the contractual period and no escalation for any reason is accepted as per the terms of this Purchase Order.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>3) GOODS AND SERVICES TAX & OTHER TAXES:</b>
                        <br />
                        GST shall be paid at actual, against documentary evidence and only after credit appears in UGCL GST Portal (2A). UGCL may withhold from all amounts payable to Supplier all
applicable withholding taxes and to remit those taxes to the applicable governmental authorities as required by
applicable laws. The Supplier or the third party assignee shall be liable for all tax payments as required under applicable
law and for statutory filings and compliances the same shall be submitted by the supplier whenever asked for against this PO. Failure to make tax payments resulting in UGCL's inability to claim tax
credits, Supplier shall be liable to indemnify UGCL and pay such amounts, penalties, interests and/or any other sums
accruing to UGCL due to such non-compliance and make equivalent payments to UGCL. Any default by the Supplier or a
third party assignee shall be deemed as a default of the Supplier and may result in the cancellation of the P.O and all
advance payment received, without any deduction shall be refunded by the Supplier
In case of any increase/decrease applicable in GST (CGST, SGST, IGST as applicable), customs duty, cess and other duties
or new taxes/duties/levies imposed by the Indian Government through Gazatte notification after the date of this P.O but
prior to contractual delivery date, UGCL shall reimburse/adjust the increase/decrease in taxes & duties against
supporting documents. Supplier shall submit the TDS Declaration to the Company under the provisions of the Income Tax Act to avoid TDS deduction at the higher rate of tax. As per Section 194Q, TDS of 0.1% on the Taxable Value will be deducted and deposited to the IT department.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>4) ACCEPTANCE OF P.O.:</b><br />
                        The P.O and the terms as per Annexure A & B herein shall be deemed to be accepted by the Supplier in total upon the receipt / acknowledgement of the P.O. or supply of part material under this P.O or receipt of advance or receipt of LC to supplier account unless expressly rejected by Supplier in writing or supplier has returned the advance or request for cancellation LC issued to the supplier within 3 days of receipt of advance or LC.
                    </td>
                </tr>

                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>5) ADVANCE:</b><br />
                        Advance, if any paid by UGCL in accordance with this P.O shall be adjusted against invoice raised by Supplier for
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
                        <div class="invoiceSignature" style="width: 95%; font-weight: bold;margin-top:120px;">
                <div style="height: 80px">
                    <div style="text-align: right">
                          <asp:ImageButton ID="imgBtnDigitalSign6" Width="150px" Height="80px" runat="server" />
                     <%--   <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;">--%>
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
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td><div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 120px; padding-top:15px;">
            </div>
   <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo3b" ></asp:Label></b>
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>6) STANDARD COMPLIANCE:</b><br />
                        The Product shall be strictly in line with the drawings/ specifications/ QAP and other documents sent along with P.O/enquiry/ UGCL’s Client, wherever needed the relevant IS standards shall be followed.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>7) MANUFACTURING SCHEDULE:</b><br />
                       Supplier shall send UGCL a manufacturing schedule to match the delivery schedule of UGCL within 7 days of this PO. In case, the supplier does not submit the schedule or fails to abide by the agreed manufacturing schedule at any stage, UGCL has the right to cancel the entire / part P.O. on the supplier’s risk and cost, without any cost implication on UGCL and get the supply by a suitable supplier of UGCL choice.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>8) DELIVERY:</b><br />
                       Delivery basis shall be as per PO. If Transportation is in Supplier's scope, material shall be delivered at UGCL or it's client's address and duly accepted by the QC/ Stores. Time is of the essence with respect to delivery of the Product. Product shall be delivered and services performed by the applicable Delivery Schedule stipulated by UGCL, non-availability of Raw Material to Supplier for production nor any payment delay from UGCL shall not be considered as a reason for Supplier to non-adherence of delivery schedule. Supplier must immediately notify UGCL if Supplier is likely to be unable to meet a delivery date under the Delivery Schedule. At any time prior to the date agreed in the Delivery Schedule, UGCL may, upon oral or written notice to Supplier, cancel or change the PO at the risk and cost of supplier, or any portion thereof, for any reason, including, without limitation, for the convenience of UGCL or due to failure of Supplier to comply with this   Agreement, unless otherwise noted.

                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>9) LIQUIDATED DAMAGES PRICE REDUCTION FOR DELAY IN DELIVERY:</b><br />
                       In the event Supplier fails to deliver the Equipment / Materials within the contracted delivery date in accordance with the Delivery Schedule, the price payable in accordance with the P.O will be reduced at the rate of 5% (Five percent) of the total value of P.O per week up to a maximum of 30 %(Thirty percent) of the total value of the P.O. If the Supplier fails to supply the finished goods no later than 60 days from the Delivery schedule, UGCL shall be entitled to cancel the PO and in the event of such cancellation the Supplier shall be liable to pay (a) liquidated damages of 10% of the PO; (b) differential cost incurred in procuring Product from third parties/other suppliers; (c) Advances /LC value with interest @ 36% per annum.
                    </td>
                </tr>

                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>10) PAYMENT TERMS:</b><br />
                       Supplier will issue all invoices on a timely basis. All invoices delivered by Supplier must meet UGCL's requirements. UGCL
will, subject to adjustments on account of Advance, interest, or other deductions, pay the undisputed portion of
properly rendered invoices within forty five (45) days from the invoice date issued by Supplier or if the payment is
through LC it would be as per LC terms of UGCL bankers. UGCL shall have the right to withhold payment of any invoiced amounts that are
disputed in good faith until the parties reach an agreement with respect to such disputed amounts.
                    </td>
                </tr>

                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>11) INSPECTION:</b><br />
                        All delivery of Products and performance of services shall be subject to UGCL's right of inspection. Inspection may be carried out by UGCL / it's client/ appointed inspection agency after submission of materials test certificates for all the materials as per QAP along with all the Internal inspection report. Upon inspection, UGCL, or its client shall either accept the Products or Services or reject them if the goods are not confirming to the approved QAP. UGCL or its client shall have the right to reject any Product that are delivered in excess of the quantity ordered or are damaged or defective. In addition, UGCL shall have the right to reject any Product or Services that are not in conformance with the Specifications stated in the P.O. In case of any defects/issues arising out of materials workmanship / quality of materials, etc, rectification work, if any, shall be carried out within a week from the date of identification of the defect at Supplier's cost. Supplier will give UGCL minimum 10 working days or as specified by Customer, advance notice through email for inspection of raw materials / finished goods. If UGCL or its client reject Products or Services such rejection shall be at Supplier's expense and risk of loss and UGCL shall at its option, either (i) demand full credit or refund of all Advance paid by UGCL to Supplier for the rejected Product or Service with interest @36%; or (ii) or direct the Supplier to replace the Product to be received within the time period specified by UGCL. Supplier shall not deliver Products or Services that were previously rejected on grounds of non -compliance with the P.O, unless delivery of such Products is approved in advance by UGCL or its client.
                    </td>
                </tr>

                
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <div class="invoiceSignature" style="width: 95%; font-weight: bold;margin-top:0px;">
                <div style="height: 80px">
                    <div style="text-align: right">
                          <asp:ImageButton ID="imgBtnDigitalSign7" Width="150px" Height="80px" runat="server" />
                 <%--       <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;">--%>
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
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td><div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 120px; padding-top:15px;">
            </div>
                         <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo3c" ></asp:Label></b>
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>12) DISPATCH:</b><br />
                        Dispatch instructions will be sent to Supplier separately in email or post. Supplier shall dispatch the materials only after getting inspection clearance as well as dispatch clearances in writing from UGCL / UGCL’s customer end.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>13) DISPATCH DOCUMENTS:</b>:&nbsp;The Product shall be dispatched and the following documents shall be submitted to UGCL on dispatch:
                        <ol type="i">
                            <li>Tax Invoice with GST for payment.</li>
                            <li>Weighing slip.</li>
                            <li>Packing list.</li>
                            <li>Inspection report.</li>
                            <li>E-Way Bill.</li>
                            <li>Internal Product Test Reports.</li>
                            <li>Any other documents specifically required by UGCL.</li>
                        </ol>
                    </td>
                </tr>

                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>14) WARRANTY:</b><br />
                        The Product/Service provided by Supplier shall be covered under warranty against bad workmanship, under performance
and Poor quality of materials for a period of 24 months from the date of receipt of the Product or Services. In the event
the equipment does not meet the P.O requirements, Supplier shall rectify and replace at his cost, Supplier warrants to
Buyer that during the Product or Service Warranty Period, all Products or Services provided in accordance with the P.O.
shall be:
                         <ol type="i">
                             <li>of merchantable quality;</li>
                             <li>fit for the purposes intended;</li>
                             <li>unless otherwise agreed to by UGCL;</li>
                             <li>free from defects in design, material and workmanship;</li>
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
                        <b>15) RISK PURCHASE: </b>
                        <br />
                        In the event Supplier delays delivery of the goods covered under this PO, beyond the date agreed in-the Delivery Schedule as mentioned in this PO, UGCL has the right to procure the goods at the risk and cost of the Supplier from any source at its discretion. Any extra cost implication arising out of this on UGCL shall be recovered from Supplier.
                    </td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>16) CONTRADICTIONS: </b>
                        <br />
                       In the event Supplier notices any contradiction in various documents, Supplier shall bring the same immediately to the notice of UGCL for a resolution, within a week of receipt of P.O. Any claim made later shall not be entertained. Such contradiction/ discrepancy/ inconsistency shall be resolved mutually by the parties failing which, it shall be resolved through dispute resolution mechanism as provided under this P.O.

                    </td>


                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>17) CANCELLATION OF P.O: </b>
                        <br />
                        Without prejudice to any other rights or remedies of UGCL, UGCL shall be entitled to terminate this P.O forthwith upon a breach of terms of the P.O by Supplier. Termination of this P.O for breach by Supplier shall not discharge supplier's obligations under any and all other PO’s issued by UGCL to supplier, or the supplier’s obligations under this PO including refund advance payments with interest @36% and surviving provisions confidentiality indemnity, taxes, liquidated damages, governing laws and dispute resolution.
                    </td>
                </tr>
                
                  <tr>
                    <td class="firsttd"></td>
                    <td>
                        <div class="invoiceSignature" style="width: 95%; font-weight: bold;margin-top:30px;">
                <div style="height: 80px">
                    <div style="text-align: right">
                          <asp:ImageButton ID="imgBtnDigitalSign8" Width="150px" Height="80px" runat="server" />
                        <%--<img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;">--%>
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
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td><div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="PO Header" style="width: 98%; height: 120px; padding-top:15px;">
            </div>
                        <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo3d" ></asp:Label></b>
            </div>
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>18) LIABILITY OF SUPPLIER: </b>
                        <br />
                       Supplier shall be liable for all losses resulting to UGCL from non-compliance with the Delivery Schedule or breach of any terms of the P.O, including any loss of profits, to the maximum extent permissible under law. Notwithstanding anything, no limitation or exclusion of liability shall apply with respect to any claims based on this P.O arising out of the Supplier's willful misconduct or gross negligence or with respect to any claims for personal injury or property damage, or to Supplier's indemnification obligations stated herein.

                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>19) CONSEQUENTIAL DAMAGES: </b>
                        <br />
                        UGCL shall in no event be liable for loss of profit, loss of revenues, loss of use, loss of production, costs of capital or costs connected with interruption of operation, loss of anticipated savings or for any special, indirect or consequential damage or loss of any nature whatsoever, accruing to Supplier arising out of execution of this P.O or cancellation of the P.O due to non-performance with respect to quality, quantity & timely supply as specified by UGCL. 
                    </td>
                </tr>
               
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>20) INSURANCE: </b>
                        <br />
                       UGCL shall not be responsible for procuring insurance for and on behalf of the Vendor or the Equipment it is the sole
responsibility of the supplier to keep all the goods and services fully insured in all respects as applicable.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>21) ASSIGNMENT: </b>
                        <br />
                        Supplier may not assign or subcontract the P.O placed under this P.O in whole or in part, without UGCL's prior written consent.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>22) CONFIDENTIALITY: </b>
                        <br />
                        Supplier undertakes that it shall, at all times, maintain confidentiality of all the information including but not limited to drawings and other technical specifications received by it in respect of the P.O and/or disclosed to it by UGCL or its client and shall not disclose or divulge the same or any part thereof to any third party without the prior written consent of UGCL. The obligations of this clause shall survive termination of this P.O at the risk &cost of Supplier, regardless of the reasons for termination of this P.O and the obligation shall subsist for a period of 12 months from the date of termination
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>23) GOVERNING LAW & DISPUTE RESOLUTION: </b>
                        <br />
                        In case any dispute/s or difference/s arises between the Parties in connection with any matter relating to this P.O including termination thereof then the Parties shall, refer the dispute to arbitration and shall be governed by the provisions of the Arbitration & Conciliation Act, 1996. The arbitration proceedings shall be adjudicated by a sole arbitrator appointed by UGCL, and the arbitration proceedings shall be held in Bangalore. All the expenses of arbitration will be to supplier’s account. The language of arbitration shall be English. The decision of the arbitrator shall be final and binding upon the Parties.
                    </td>
                </tr>
                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>24) COSTS AND EXPENSES: </b>
                        <br />
                        All costs and expenses incurred in connection with P.O. including costs incurred in recovering the advance payments with interest shall be to the Supplier's account.
                    </td>
                </tr>

                <tr>
                    <td class="firsttd"></td>
                    <td>
                        <b>25) PO SPLIT-UP: </b>
                        <br />
                        At any point of time during the execution of the order UGCL, without prior information to Supplier UGCL shall split up the P.O and place some part of the P.O from its Group Companies/Associates ensuring the total order value to the supplier from UGCL and its associates will not vary from this order.
                    </td>
                </tr>
            </table>

            <div class="invoiceSignature" style="width: 95%; font-weight: bold;margin-top:200px;">
                <div style="height: 80px">
                    <div style="text-align: right">
                          <asp:ImageButton ID="imgBtnDigitalSign9" Width="150px" Height="80px" runat="server" />
                     <%--   <img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:180px;"/>--%>
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
                <img src="../Style/Images/FooterImg.png" alt="PO Footer" style="height: 80px; width: 97%" />
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

			  