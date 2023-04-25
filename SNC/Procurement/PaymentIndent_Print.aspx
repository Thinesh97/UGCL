<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="PaymentIndent_Print.aspx.cs" Inherits="PaymentIndent_Print" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        table#ContentPlaceHolder1_Grid_Item1 tr td {
            border: 1px solid #000;
        }
        table#ContentPlaceHolder1_Grid_Item2 tr td {
            border: 1px solid #000;
        }

        .page-break {
            display: none;
        }

        table#mytbl1 tr th {
            font-size: 14px;
            padding: 0 10px;
        }

        table#mytbl1 tr td {
            font-size: 14px;
            padding: 0 10px;
        }

        invoiceFooter p {
            font-size: 14px;
            font-weight: 600;
        }

        table#mytbl1 tr th {
            font-weight: bold;
        }

        @page {
            _size: 8.5in 11in;
            margin-top: 0px;
        }

        @media print {
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
            table#mytbl1 tr th {
                font-weight: normal;
            }

            .page-break {
                display: block;
                page-break-before: always;
            }
        }

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
    </style>

    <style>
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


    <div id="divToPrint" runat="server">

        <div id="divTableDataHolder" style="width: 880px">

            <div style="width: 95%;">
                <img src="../Style/Images/logo123.jpg" alt="PO Header" style="width: 100%; height: 120px" />
            </div>
            <div style="text-align: right; padding-right: 30px; height: 50px">
                <b>Page No: 1</b>
            </div>
           
            <b>Payment Indent</b>

            <table class="table table-bordered" style="width: 97%; margin-bottom: 0px;" id="mytbl1">
                <tr>
                    <td style="width: 50%"><b>Payment Indent No. :</b> <br />
                        <asp:Label runat="server" Text="" ID="lblPayIndNo"></asp:Label>
                    </td>
                    <td style="width: 50%"><b>Payment Indent Date :</b> <br />
                        <asp:Label runat="server" Text="" ID="lblPayIndDate"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>For the Year :</b> &nbsp; <asp:Label runat="server" Text="" ID="lblFYr"></asp:Label></td>
                    <td><b>State : </b> &nbsp; <asp:Label runat="server" Text="" ID="lblState"></asp:Label></td>
                </tr>
                <tr>
                    <td><b>Vendor/ SubContractor Name :</b> <br />
                        <asp:Label runat="server" Text="" ID="lblVendorSubcon"></asp:Label>
                    </td>
                    <td><b>Project Code :</b> <br />
                        <asp:Label runat="server" Text="" ID="lblProject"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Employer Name :</b> <br />
                        <asp:Label runat="server" Text="" ID="lblAwardedBy"></asp:Label>
                    </td>
                    <td><b>Work Description :</b> <br />
                        <asp:Label runat="server" Text="" ID="lblWorkDesc"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td><b>Principal Contractor Name :</b> <br />
                        <asp:Label runat="server" Text="N/A" ID="lblPricipalCon"></asp:Label>
                    </td>
                    <td><b>PO/WO Issued :</b> <br />
                        <asp:Label runat="server" Text="" ID="lblPOWO"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td><b>Nature Of Work Executed :</b> <br />
                        <asp:Label runat="server" Text="" ID="lblNatureOfWork"></asp:Label>
                    </td>
                    <td><b>Nature Of Material Received/ Services availabled :</b> <br />
                        <asp:Label runat="server" Text="" ID="lblNatureOfMaterial"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td><b> KYC (Gst Registration) :</b> <br /></td>
                    <td><asp:Label runat="server" ID="lblGSTRegd" Text="NA"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b> KYC (PAN Copy) :</b> <br /></td>
                    <td><asp:Label runat="server" ID="lblPANCopy" Text="NA"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b> KYC (Bank Details) :</b> <br /></td>
                    <td><asp:Label runat="server" ID="lblBankDetails" Text="NA"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td colspan="2"><b>PAYMENT REQUEST :</b></td>
                </tr>
                <tr>
                    <td>a. Value Of Services/Materials Received : <br />
                    </td>
                    <td style="text-align:right"><asp:Label runat="server" ID="txtAmt_ServiceMaterial" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="border-bottom:none;">b. Materials Issued From Company On Debitable Basis :</td>
                </tr>
                <tr>
                    <td colspan="2" style="padding:0px; border:1px solid transparent;">
                        <asp:GridView ID="Grid_Item1" Width="100%" ShowHeader="false" runat="server" CssClass="GridCenter" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                                <Columns>
                                    <%--<asp:TemplateField ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerial" runat="server" Text='<%#Eval("Serial")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ItemStyle-Width="425px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemAmt" runat="server" Text='<%# Math.Round(decimal.Parse(Eval("Item_Amount").ToString()))%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" />
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="border-bottom:none;">c. Withhold Amount As Negotiated :</td>
                </tr>
                <tr>
                    <td colspan="2" style="padding:0px; border:1px solid transparent;">
                        <asp:GridView ID="Grid_Item2" Width="100%" ShowHeader="false" runat="server" CssClass="GridCenter" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                                <Columns>
                                    <%--<asp:TemplateField ItemStyle-Width="30px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSerial" runat="server" Text='<%#Eval("Serial")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField ItemStyle-Width="425px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemAmt" runat="server" Text='<%# Math.Round(decimal.Parse(Eval("Item_Amount").ToString()))%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="GridCenter2" />
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>d. Earlier Payments Done : <br />
                    </td>
                    <td style="text-align:right"><asp:Label runat="server" ID="txtAmt_EarlierPayment" ></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>e. Part Payment Sought By Project InCharge : <br />
                    </td>
                    <td style="text-align:right"><asp:Label runat="server" ID="txtAmt_PartPayment" ></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td style="text-align: right; width: 50%;">
                        <p>Total</p>
                    </td>
                    <td style="text-align: right">
                        <b><asp:Label runat="server" ID="lblTotalAmt"></asp:Label></b></td>
                </tr>

                <tr>
                    <td colspan="3">Amount Chargeable (in words) :<br />
                        <b><asp:Label runat="server" ID="lblAmountInWords"></asp:Label></b>
                    </td>
                </tr>
            </table>
            
            <table class="table table-bordered" style="width: 97%; padding: 0; margin-top: -1px;" id="mytbl4">

                <tr>
                    
                    <td colspan="2" style="text-align: right;">
                        <p><b>For United Global Corporation Limited</b></p>
                        <div style="height: 70px">
                            <asp:Image ID="ImgAuthorisedSign" Height="80" Width="120" Visible="false" BorderColor="White" runat="server" />
                            <%--<img src="../Style/Images/Img_Sign.jpeg" style="Height:70px; Width:220px;"/>--%>
                        </div>
                        <p>Authorised Signatory</p>
                    </td>
                </tr>

            </table>

            <div class="invoiceFooter" style="display:none">
                <p style="margin:0">This is a Computer Generated Document</p>
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
