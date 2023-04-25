<%@ Page Language="C#"  MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="Bank_Advice_Print.aspx.cs" Inherits="SNC.Procurement.Bank_Advice_Print" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <style>
        .instract {
    font-size: 11px;
    text-align: justify;
    width:830px;
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

        table#ContentPlaceHolder1_Grid_OtherTerms{
            width:100%;
            border:none;
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
            border:  1px solid;
        }
        table#mytbl2a, table#mytbl2b {
            border:hidden;
        }

        invoiceFooter p {
            font-size: 14px;
            font-weight: 600;
        }
        table#mytbl2a tbody td, table#mytbl2b tbody td {
            border:  1px solid;
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
            .page-break { display: none; }
        }

        @media print {

            table#instruct {
                border: none !important;
            }
            table#mytbl3 tbody td {
                border-bottom: 1px solid #fff !important;
                padding:1px !important;
            }
                    table#mytbl1 tr th {
                font-weight: normal;
            }
            .page-break { 
                display:none; 
                /*page-break-before: always;*/
            }
        }
    </style>

    <style>
        .tbl tr {
            height: 5px;
        }

        .GridCenter th{
            text-align: center;
        }

        .GridCenter2 {
            text-align: right;
        }

        .GridCenter3 {
            text-align: center;
        }
        table#ContentPlaceHolder1_GridPrint4 tr td,table#ContentPlaceHolder1_GridPrint3 tr td,table#ContentPlaceHolder1_GridPrint8 tr td,table#ContentPlaceHolder1_GridPrint tr td,table#ContentPlaceHolder1_GridPrint9 tr td,table#ContentPlaceHolder1_GridPrint10 tr td,table#ContentPlaceHolder1_GridPrint11 tr td,table#ContentPlaceHolder1_GridPrint12 tr td,table#ContentPlaceHolder1_GridPrint13 tr td,table#ContentPlaceHolder1_GridPrint14 tr td,table#ContentPlaceHolder1_GridPrint15 tr td,table#ContentPlaceHolder1_GridPrint16 tr td,table#ContentPlaceHolder1_GridPrint17 tr td,table#ContentPlaceHolder1_GridPrint18 tr td,table#ContentPlaceHolder1_GridPrint19 tr td,table#ContentPlaceHolder1_GridPrint20 tr td,table#ContentPlaceHolder1_GridPrint21 tr td,table#ContentPlaceHolder1_GridPrint21 tr td,table#ContentPlaceHolder1_GridPrint22 tr td,table#ContentPlaceHolder1_GridPrint23 tr td,table#ContentPlaceHolder1_GridPrint24 tr td,table#ContentPlaceHolder1_GridPrint25 tr td,table#ContentPlaceHolder1_GridPrint26 tr td {
    border: 1px solid #000;
    text-align: center;
    padding:3px;
}
         table#ContentPlaceHolder1_GridPrint4 tr td:first-child ,table#ContentPlaceHolder1_GridPrint3:first-child tr td,table#ContentPlaceHolder1_GridPrint8:first-child tr td,table#ContentPlaceHolder1_GridPrint tr td:first-child,table#ContentPlaceHolder1_GridPrint9 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint10 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint11 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint12 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint13 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint14 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint15 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint16 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint17 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint18 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint19 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint20 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint21 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint21 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint22 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint23 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint24 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint25 tr td:first-child
         ,table#ContentPlaceHolder1_GridPrint26 tr td:first-child {
       
            text-align:left;
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
        .mypageadjust{
            height:850px;
        }
         .mypageadjustfirst{
            height:960px;
        }
        .mypageadjusttwo{
            min-height:890px;
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


    <div id="divToPrint1" runat="server">

        <div id="divTableDataHolder1" style="width: 880px">

           
            <div class="mypageadjustfirst">
            

         
          
            <table class="table table-bordered" style="width: 97%; margin-bottom:0px;" id="mytbl1">
                <tr>
                    <td  ><span  style="font-size:18px; color:dodgerblue; font-weight:bold;" >United Global Corporation Private Limited</span></td>
                    
                    <td rowspan="4"> <span style="font-size:21px;"><b>PAYMENT ADVICE</b></span>  </td>
               
                </tr>
                <tr>
                    <td style="color:dodgerblue" >E-07, 7th Floor, Solus Jain Heights,No. 2, 1st Cross, J.C.Road, Opp. Jain University,
                        <br />Bengaluru - 560027
                        <br />GSTIN : 29AABCU5251F1ZC
                        <br />TAN : BLRU02926A
                        <br />State Name : Karnataka, Code : 29, Email: accounts@lpgroup.co.in
                    </td>
                </tr>
                <tr>
                  
                </tr>
                <tr></tr>
                <tr></tr>

            </table>
                <br />
            <table class="table table-bordered" style="width: 97%; margin-bottom:0px;" id="mytbl10">
               
                    <tr>
                    <td ><span  style="font-size:12px;">PAYMENT MADE TO (VENDOR/SUBCONTRACTOR) </span></td>
                    <td  ><span  style="font-size:12px;">PAYMENT INDENT NO.</span></td>
                    <td  ><span  style="font-size:12px;">PAYMENT DATE</span></td>
                    </tr>

                <tr>
                    <td><asp:Label runat="server" Text="" ID="lbl_CompanyName"></asp:Label></td>
                     <td><asp:Label runat="server" Text="" ID="lbl_Payind_No"></asp:Label> </td>
                     <td><asp:Label runat="server" Text="" ID="lbl_Payind_Date"></asp:Label></td>
                </tr>
                
             </table>
                <br />
            <table class="table table-bordered" style="width: 97%; margin-bottom:0px;" id="mytbl11">
               
                    <tr>
                    <td ><span  style="font-size:12px;">COMPANY Address As Per Our records </span></td>
                    <td ><span  style="font-size:12px;">Mobile No:</span>
                    </td>
                    <td ><span  style="font-size:12px;">BANK DETAILS</span></td>
                    </tr>

                <tr>
                    <td ><asp:Label runat="server" Text="" ID="lbl_Payind_CompAddr1"></asp:Label><br /></td>
                     <td ><asp:Label runat="server" Text="" ID="lbl_Payind_MobNo"></asp:Label> </td>
                     <td ><span  style="font-size:12px;">Bank Name: </span>
                            <asp:Label runat="server" Text="" ID="lbl_Payind_Bankname"></asp:Label>
                     </td>

                </tr>
                         <tr>
                     <td ><asp:Label runat="server" Text="" ID="lbl_Payind_CompAddr2"></asp:Label><br /></td>
                      <td ><span  style="font-size:12px;">Email: </span></td>
                    <td ><span  style="font-size:12px;">Account No:</span>
                            <asp:Label runat="server" Text="" ID="lbl_Payind_AccNo"></asp:Label>
                     </td>
                         </tr>
                         <tr>
                    <td ><asp:Label runat="server" Text="" ID="lbl_Payind_CompAddr3"></asp:Label><br /></td>
                     <td ><asp:Label runat="server" Text="" ID="lbl_Payind_Email"></asp:Label> </td>
                     <td ><span  style="font-size:12px;">Branch:</span>
                            <asp:Label runat="server" Text="" ID="lbl_Payind_Branch"></asp:Label>
                     </td>

                </tr>
                         <tr>
                             <td ><asp:Label runat="server" Text="" ID="lbl_Payind_CompAddr4"></asp:Label><br /></td>
                     <td ><asp:Label runat="server" Text="" ID="Label13"></asp:Label> </td>
                     <td ><span  style="font-size:12px;">IFSC Code:</span>
                            <asp:Label runat="server" Text="" ID="lbl_Payind_ISFC"></asp:Label>
                     </td>
                         </tr>

             </table>      
                
                <br />
            <table class="table table-bordered" style="width: 97%; margin-bottom:0px;" id="mytbl101">
               
                    <tr>
                    <td colspan="3" ><span  style="font-size:12px;">Payment Mode</span></td>                   
                    </tr>
                <tr>
                    <td colspan="3" ><asp:Label runat="server" Text="" ID="lbl_Payind_PayMethod"></asp:Label></td>
                   
                    </tr>
                <tr>
                    <td style="padding:0;">
                        <asp:GridView ID="Grdbankadvice" Width="100%" runat="server" HeaderStyle-Wrap="false"
                            AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">
                            
                            <Columns>
                                <asp:TemplateField HeaderText="PROJECT NAME" ItemStyle-Width="170px">
                                    <ItemTemplate>
                                        <asp:Label ID="lab_bankAdv_Projcode" runat="server" Text='<%#Eval("Project_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="WO/PO NUMBER" ItemStyle-Width="170px">
                                    <ItemTemplate>
                                                                             
                                        <asp:Label ID="lab_bankAdv_wono" Font-Size="13px" runat="server" Text='<%#Eval("WONo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                

                                <asp:TemplateField HeaderText="WO/PO DATE"  ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lab_bankAdv_wodate" runat="server" Text='<%#Eval("WODate")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" Wrap="false"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="WORK DONE FOR" ItemStyle-Wrap="true" ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:Label ID="lab_bankAdv_wdfor" runat="server" Text='<%#Eval("WorkDoneFor")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" Wrap="false"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="AMOUNT TRANS" ItemStyle-Wrap="true" ItemStyle-Width="300px">
                                    <ItemTemplate>
                                        <asp:Label ID="lab_bankAdv_amtrans" runat="server" Text='<%#Eval("Amt_Approved")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false"/>
                                </asp:TemplateField>

                                

                               
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight:bold">
                        TOTAL:
                        <asp:Label runat="server" ID="lbl_Payind_Total" Font-Bold="true"></asp:Label>
                        .00
                    </td>
                      </tr>

                  <tr>
                    <td colspan="4">
                        Amount Chargeable (in words) : &nbsp;
                        <b><asp:Label runat="server" Text="" ID="lbl_Payind_Totalwords"></asp:Label></b>
                        Only
                    </td>
                </tr>
               

             </table>

                <br />
            <table class="table table-bordered" style="width: 97%; margin-bottom:0px;" id="mytbl2a">
                
               <tr>
                    <td style="padding:0;">
                        
                    </td>
                </tr>

                <tr runat="server" visible="false">
                    <td>
                         
                    </td>
                </tr>
            </table>

            </div>

            <div>
    <p>Note: the Payment is made Against the WO/PO Mentioned above and acceptance of the payment is deemed acceptance of all the terms of the said work order even if not signed by the vendor/Subcontractor</p>
                
            </div>
         
            <div class="invoiceFooter">
                <p style="margin:0">This is a Computer Generated Document, Does not require signature </p>
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height:80px; width:96%"/>
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
