<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="SC_BillPrint.aspx.cs" Inherits="SNC.SubContractorBills.SC_BillPrint" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        table#ContentPlaceHolder1_WorkDoneGrid tr td:last-child {
    text-align: right;   
    padding-right: 27px !important;
}
        table#mytbl1 tr td {
    padding: 0;
    margin: 0;
}
        table#mytbl1 tr td ,table#mytbl2 tr td ,table#mytbl3 tr td,table#mytbl2a tr td{
 
    padding: 0 !important;
}
        table#ContentPlaceHolder1_WorkDoneGrid tr td ,table#ContentPlaceHolder1_Grid_SC_MIN tr td,table#ContentPlaceHolder1_Grid_CompletedPayment tr td{
   padding: 2px;
    border: 1px solid #000;
    text-align: center;
}
        
        #ContentPlaceHolder1_GridNMR tr td {
            border:1px solid;
        }
        #ContentPlaceHolder1_div_NMR tr td {
            border:1px solid;
        }
        table#ContentPlaceHolder1_GridDPR_Items tr td {
    padding: 2px;
    border: 1px solid #000;
    text-align: center;
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
        .table-bordered {
  border: 1px solid #000;
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
                page-break-before: always;
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
                display: block; 
                page-break-before: always;
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
            .mypageadjustfirst1{
            height:960px;           
border: 1px solid #000;
margin-bottom: 41px;
margin-top:20px;
        }
            .mypageadjustfirst2{
            height:960px;           
border: 1px solid #000;
margin-bottom: 41px;
margin-top:20px;
        }
            .mypageadjustfirst3{
            height:960px;           
border: 1px solid #000;
margin-bottom: 41px;
margin-top:20px;
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
        table#anx1 tr td, table#anx2 tr td,#anx3  tr td {
    text-align: center;
    font-weight: 600;
}
table#anx5 {
    margin-left: -263px;
}
table#anx4 tr td {
    border: 1px solid #fff;
   
}
table#anx1 tr td,#anx2 tr td,#anx3 tr td{
    border-top: 1px solid #fff !important;
    border-bottom: 1px solid #fff !important;
}
span#ContentPlaceHolder1_lblSubContactorName {
    font-weight: 600;
}
    </style>


    <div id="divToPrint1" runat="server">

        <div id="divTableDataHolder1" style="width: 880px">

        <%--    <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width:98%; height:110px"/>
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo1" ></asp:Label></b>
            </div>--%>
            <div class="mypageadjustfirst">
            <div runat="server" id="div_Watermark_Draft" visible="false">DRAFT CONFIDENTIAL</div>
            <div runat="server" id="div_Watermark_Cancel" visible="false">Cancelled</div>

            <b>SERIVCE INVOICE</b>
          
            <table class="table table-bordered" style="width: 97%; margin-bottom:0px;" id="mytbl0">
                <tr>
                    <td rowspan="1" style="width:50%"><u><b> From: </b> </u> <br />
                        <asp:Label runat="server" Text="" ID="lblSubContactorName"></asp:Label> <br />
                        <asp:Label runat="server" Text="" ID="lblSubContactorID"></asp:Label> <br />
                        <asp:Label runat="server" Text="" ID="lblSubContactorAddess"></asp:Label><br />
                         <asp:Label runat="server" Text="" ID="lblSubContactorCity"></asp:Label> ,<asp:Label runat="server" Text="" ID="lblSubContactorState"></asp:Label>,<asp:Label runat="server" Text="" ID="lblSubContactorCountry"></asp:Label>  <br />
                        Sub Con Mobile : <asp:Label runat="server" Text="" ID="lblSubContactorMobileNumber"></asp:Label> <br />
                        GST: <asp:Label runat="server" Text="" ID="lblSubContractorGST"></asp:Label>
                           PAN: <asp:Label runat="server" Text="" ID="lblSubContractorPAN"></asp:Label>
                    </td>
                    <td style="width:50%">
                       <b> Service Invoice Number:</b>
                       <b><asp:Label runat="server" Text="" ID="lblServiceInvoiceNumber"></asp:Label> </b> <br />
                       <b>Date: </b> 
                        <asp:Label runat="server" Text="" ID="lblSCBillDate"></asp:Label> <br />
                         <b> Billing Period:</b>
                       <asp:Label runat="server" Text="" ID="lblBillingPeriodFrom"></asp:Label><asp:Label runat="server" Text="" ID="lblBillingPeriodTo"></asp:Label><br />
                         <b>Project: </b> 
                        <asp:Label runat="server" ID="lblProject"></asp:Label>
                    </td>
                   
                </tr>
                
                  <tr>
                       <td colspan="1" style="border-bottom:hidden">
                        <div class="myadddresspart">
                                <span  style="width:50%"><u><b> To : </b> </u> <br />
                        <b>United Global Corporation Limited</b> <br />
                        (Formerly United Infra Corp. (BLR) Ltd.) <br />
                        <asp:Label runat="server" Text="" ID="lblAddressLine1_Company_SC"></asp:Label> <br />
                        <asp:Label runat="server" Text="" ID="lblAddressLine2_Company_SC"></asp:Label> <br />

                        GSTIN : <asp:Label runat="server" Text="" ID="lblGSTIN_Company_SC"></asp:Label><br />
                        TAN : <asp:Label runat="server" Text="" ID="lblTAN_Company_SC"></asp:Label> <br />
                        State Name : <asp:Label runat="server" Text="" ID="lblState_Company_SC"></asp:Label>, Code : <asp:Label runat="server" Text="" ID="lblCode_Company_SC"></asp:Label> <br />
                        Email: <asp:Label runat="server" Text="" ID="lblEmail_Company_SC"></asp:Label>
                    </span>
                        </div>
                    </td> 
                    <%--<td colspan="2">Terms of Delivery &nbsp &nbsp
                        <asp:Label runat="server" ID="lbldelscheduled"></asp:Label> 
                      </td>  --%>
                     <td>
                        <b>Scope of Work </b> 
                         <br />
                           <div  runat="server">  <label runat="server" id="lblMainSCWork"></label> </div> 
                     </td>
                </tr>
               
            </table>

           
                <table class="table table-bordered" style="width: 97%; margin-bottom:0px;" id="mytbl1">
                <tr runat="server" >
                    <td>
                        <asp:GridView ID="WorkDoneGrid" Width="100%" runat="server" CssClass="GridCenter" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                 <asp:TemplateField HeaderText="Sl No."  ItemStyle-Width="45px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Services" ItemStyle-Width="250px">
                                    <ItemTemplate>
                                         <asp:Label ID="lblItemDesc" Font-Size="13px" runat="server" Text='<%#Eval("Discription_Of_Work")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                               

                                <asp:TemplateField  HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypePerc" runat="server" Text='<%#Eval("Quantity")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%#Eval("UOMPrefix").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                <asp:TemplateField  HeaderText="Rate per Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Math.Round(decimal.Parse(Eval("Rate").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                   <asp:TemplateField  HeaderText="Present Bill Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Math.Round(decimal.Parse(Eval("TotalPresent_Progress").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                 <asp:TemplateField  HeaderText="Present Billed Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Math.Round(decimal.Parse(Eval("TotalRate").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>

            </table>
            <table runat="server" class="table table-bordered" style="width: 97%; padding: 0;margin-top: -1px;" id="mytbl3a">

                <tr>
                    <td style="width:426px;">
                       <b> Bank Details:</b>
                    </td>
                    <td colspan="2">
                   <b>  <label>     Total Billing Amount &nbsp; &#x20b9;</label> </b>
                     
                    </td>
                    <td style="text-align:right">
                        <b><label id="lblRateCardTotal" runat="server" style="padding-left:60px"> </label></b> </b>
                    </td>
                </tr>
                 <tr>
                    <td style="width:426px;">
                       Bank Name: <span runat="server" id="lblSUCBankName"> </span>
                    </td>
                    <td colspan="2">
                      Deductions As Per Annexure B   &nbsp; &#x20b9; 
                    </td>
                     <td style="text-align:right">
                        <b>  <label id="lblDeductions" style="padding-left:76px" runat="server">  </label></b>
                     </td>
                </tr>
                 <tr>
                    <td style="width:426px;">
                      Bank Account No: <span runat="server" id="lblSUCBankACNumber"> </span>
                    </td>
                    <td colspan="2">
                       Total Taxable Amount 
                    </td>
                     <td style="text-align:right">
                         <label id="lblTotalTaxableAmt" runat="server"></label>
                     </td>

                </tr>
                 <tr>
                    <td style="width:426px;">
                    Bank Branch IFSC: <span runat="server" id="lblSCIFSCCode"> </span> 
                    </td>
                    <td style="width:150px;">
                     Add:CGST
                    </td>
                      <td >
                     <label runat="server" id="lblSC_CGST"></label><b>&#37;</b>
                    </td>
                    <td style="text-align:right">
                      <label runat="server" id="lblSC_CGST_Amount"></label>
                    </td>
                </tr>
                 <tr>
                    <td style="width:426px;">
                 
                    </td>
                    <td>
                   Add:SGST
                    </td>
                      <td >
                     <label runat="server" id="lblSC_SGST"></label><b>&#37;</b>
                    </td>
                    <td style="text-align:right">
                      <label runat="server" id="lblSC_SGST_Amount"></label>
                    </td>
                     
                </tr>
                 <tr>
                    <td style="width:426px;">
                 
                    </td>
                    <td>
                   Add:IGST
                    </td>
                      <td >
                     <label runat="server" id="lblSC_IGST"></label><b>&#37;</b>
                    </td>

                    <td style="text-align:right">
                      <label runat="server" id="lblSC_IGST_Amount"></label>
                    </td>
                     
                </tr>

                  <tr>
                    <td>
                      Note:  TDS @ 194C @ <label runat="server" id="lblSC_TDS_Note"></label>  <b>&#37;</b>
                    </td>
                    <td colspan="2">
                      Total amount:GST
                    </td>
                      <td style="text-align:right">
                           <label runat="server" id="lblGSTTotal"></label>
                      </td>
                </tr>
                 <tr>
                    <td>
                     Annexure A,B & C are the internal part of this Service Invoice
                    </td>
                    <td colspan="2">
                     <b >Total Amount &nbsp; &#x20b9;</b>
                      
                    </td>
                    <td style="text-align:right">
                      <b>   <label runat="server" id="lblTotalAmont_H"  ></label> </b>
                    </td>
                </tr>
                 <tr>
                    <td>
                 Annexure A Work Done Activity (weekly)
                    </td>
                     <td>
                 TDS Deduction 
                    </td>
                      <td >
                     <label runat="server" id="lblSC_TDS" > </label>  <b>&#37;</b>
                    </td>
                    <td style="text-align:right">
                   <label runat="server" id="lblTDS_amount"></label>
                    </td>
                </tr>
                 <tr>
                    <td>
               Annexure B Material Received on Debitable Basis
                    </td>
                     <td>
                Retention
                    </td>
                      <td >
                     <label runat="server" id="lblRetentionPerc"></label><b>&#37;</b>
                    </td>
                    <td style="text-align:right" >
                     <label runat="server" id="lblRetentionAmont"></label>
                    </td>
                </tr>
                <tr>
                    <td>Annexure C Receipt of Payment as on date of current Invoice</td>
                    
                         <td style="text-align:right;font-weight:bold" colspan="3">
                        <b>Net Amount  &nbsp; &#x20b9; <label style="padding-left:190px" runat="server"  id="lblNetAmount"></label>  </b>
                    </td>
                   
                </tr>
              <%-- <tr>
                    <td style="border-bottom:1px solid #fff;"></td>
                   
                   
                </tr>--%>
                  
                
             <%--   <tr>
                    <td colspan="4">
                        Amount Chargeable (in words) : &nbsp;
                        <b><asp:Label runat="server" Text="" ID="lblAmountInWords"></asp:Label></b>
                    </td>
                </tr>

                <tr>
                    <td style="border-bottom:1px solid #fff;border-right:1px solid #fff;">
                        <p>Terms and Conditions<br /> 
                            <asp:Label runat="server" Text="" ID="lblAnnexure"></asp:Label>
                        </p>
                    </td>
                    <td colspan="5" style="vertical-align: bottom;">
                        <p style="text-align:right;font-weight:600;">E. & O.E</p>
                    </td>
                </tr>--%>

                <tr>
                    <td></td>
                    <td colspan="3" style="text-align:right;">
                        <%--<p><b>For United Global Corporation Limited</b></p>--%>
                        <div style="height:70px">
                         
                                                  </div>
                        <p>Authorised Signatory</p>
                    </td>
                </tr>
            
            </table>
            
        <%--    <div runat="server" id="div_Continue" style="width:90%; height:30px;">
                <p style="float:right; padding-top:10px">
                    Continued...&nbsp; &nbsp;
                </p>
            </div>--%>
            </div>
         
           <%-- <div class="invoiceFooter">
                <p style="margin:0">This is a Computer Generated Document</p>
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height:80px; width:96%"/>
            </div>--%>

        </div>
    </div>
    <div class="page-break"></div>
     <div id="div1" runat="server">

        <div id="Div_PrintNMR" style="width: 880px">

        <%--    <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width:98%; height:110px"/>
            </div>
            <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="lblPageNo1" ></asp:Label></b>
            </div>--%>
            <div class="mypageadjustfirst">
            <div runat="server" id="div25" visible="false">DRAFT CONFIDENTIAL</div>
            <div runat="server" id="div26" visible="false">Cancelled</div>

            <b>SERIVCE INVOICE</b>
          
            <table class="table table-bordered" style="width: 97%; margin-bottom:0px;" id="mytbl0">
                <tr>
                    <td rowspan="1" style="width:50%"><u><b> From: </b> </u> <br />
                        <asp:Label runat="server" Text="" ID="lblSubContactorName_NMR"></asp:Label> <br />
                        <asp:Label runat="server" Text="" ID="lblSubContactorID_NMR"></asp:Label> <br />
                        <asp:Label runat="server" Text="" ID="lblSubContactorAddess_NMR"></asp:Label><br />
                         <asp:Label runat="server" Text="" ID="lblSubContactorCity_NMR"></asp:Label> ,<asp:Label runat="server" Text="" ID="lblSubContactorState_NMR"></asp:Label>,<asp:Label runat="server" Text="" ID="lblSubContactorCountry_NMR"></asp:Label>  <br />
                        Sub Con Mobile : <asp:Label runat="server" Text="" ID="lblSubContactorMobileNumber_NMR"></asp:Label> <br />
                        GST: <asp:Label runat="server" Text="" ID="lblSubContractorGST_NMR"></asp:Label>
                           PAN: <asp:Label runat="server" Text="" ID="lblSubContractorPANr_NMR"></asp:Label>
                    </td>
                    <td style="width:50%">
                       <b> Service Invoice Number:</b>
                       <b><asp:Label runat="server" Text="" ID="lblServiceInvoiceNumber_NMR"></asp:Label> </b> <br />
                       <b>Date: </b> 
                        <asp:Label runat="server" Text="" ID="lblSCBillDate_NMR"></asp:Label> <br />
                         <b> Billing Period:</b>
                       <asp:Label runat="server" Text="" ID="lblBillingPeriodFrom_NMR"></asp:Label><asp:Label runat="server" Text="" ID="lblBillingPeriodTo_NMR"></asp:Label><br />
                         <b>Project: </b> 
                        <asp:Label runat="server" ID="lblProject_NMR"></asp:Label>
                    </td>
                   
                </tr>
                
                  <tr>
                       <td colspan="1" style="border-bottom:hidden">
                        <div class="myadddresspart">
                                <span  style="width:50%"><u><b> To : </b> </u> <br />
                        <b>United Global Corporation Limited</b> <br />
                        (Formerly United Infra Corp. (BLR) Ltd.) <br />
                        <asp:Label runat="server" Text="" ID="lblAddressLine1_Company_SC_NMR"></asp:Label> <br />
                        <asp:Label runat="server" Text="" ID="lblAddressLine2_Company_SC_NMR"></asp:Label> <br />

                        GSTIN : <asp:Label runat="server" Text="" ID="lblGSTIN_Company_SC_NMR"></asp:Label><br />
                        TAN : <asp:Label runat="server" Text="" ID="lblTAN_Company_SC_NMR"></asp:Label> <br />
                        State Name : <asp:Label runat="server" Text="" ID="lblState_Company_SC_NMR"></asp:Label>, Code : <asp:Label runat="server" Text="" ID="lblCode_Company_SC_NMR"></asp:Label> <br />
                        Email: <asp:Label runat="server" Text="" ID="lblEmail_Company_SC_NMR"></asp:Label>
                    </span>
                        </div>
                    </td> 
                    <%--<td colspan="2">Terms of Delivery &nbsp &nbsp
                        <asp:Label runat="server" ID="lbldelscheduled"></asp:Label> 
                      </td>  --%>
                     <td>
                        <b>Scope of Work </b> 
                         <br />
                           <div  runat="server">  <label runat="server" id="lblMainSCWork_NMR"></label> </div> 
                     </td>
                </tr>
               
            </table>

           
                <table class="table table-bordered" style="width: 97%; margin-bottom:0px;" id="mytbl1">
                <tr runat="server" >
                    <td>
                        <asp:GridView ID="GridNMR" Width="100%" runat="server" CssClass="GridCenter" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                              
                                <asp:TemplateField HeaderText="Labour Type" ItemStyle-Width="250px" >
                                    <ItemTemplate>
                                         <asp:Label ID="lblItemDesc" Font-Size="13px" runat="server" Text='<%#Eval("Labour_Type")%>'></asp:Label>
                                    </ItemTemplate>
                                      <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                               

                                <asp:TemplateField  HeaderText="Total Labour">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypePerc" runat="server" Text='<%#Eval("TotalLabour")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%#Eval("TotalLabourCost").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>
                             
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>

            </table>
            <table runat="server" class="table table-bordered" style="width: 97%; padding: 0;margin-top: -1px;" id="Table1">

                <tr>
                    <td style="width:426px;">
                       <b> Bank Details:</b>
                    </td>
                    <td colspan="2">
                   <b>  <label>     Total Billing Amount &nbsp; &#x20b9;</label> </b>
                     
                    </td>
                    <td style="text-align:right">
                        <b><label id="lblRateCardTotal_NMR" runat="server" style="padding-left:60px"> </label></b> </b>
                    </td>
                </tr>
                 <tr>
                    <td style="width:426px;">
                       Bank Name: <span runat="server" id="lblSUCBankName_NMR"> </span>
                    </td>
                    <td colspan="2">
                      Deductions As Per Annexure B   &nbsp; &#x20b9; 
                    </td>
                     <td style="text-align:right">
                        <b>  <label id="lblDeductions_NMR" style="padding-left:76px" runat="server">  </label></b>
                     </td>
                </tr>
                 <tr>
                    <td style="width:426px;">
                      Bank Account No: <span runat="server" id="lblSUCBankACNumber_NMR"> </span>
                    </td>
                    <td colspan="2">
                       Total Taxable Amount 
                    </td>
                     <td style="text-align:right">
                         <label id="lblTotalTaxableAmt_NMR" runat="server"></label>
                     </td>

                </tr>
                 <tr>
                    <td style="width:426px;">
                    Bank Branch IFSC: <span runat="server" id="lblSCIFSCCode_NMR"> </span> 
                    </td>
                    <td style="width:150px;">
                     Add:CGST
                    </td>
                      <td >
                     <label runat="server" id="lblSC_CGST_NMR"></label><b>&#37;</b>
                    </td>
                    <td style="text-align:right">
                      <label runat="server" id="lblSC_CGST_Amount_NMR"></label>
                    </td>
                </tr>
                 <tr>
                    <td style="width:426px;">
                 
                    </td>
                    <td>
                   Add:SGST
                    </td>
                      <td >
                     <label runat="server" id="lblSC_SGST_NMR"></label><b>&#37;</b>
                    </td>
                    <td style="text-align:right">
                      <label runat="server" id="lblSC_SGST_Amount_NMR"></label>
                    </td>
                     
                </tr>
                 <tr>
                    <td style="width:426px;">
                 
                    </td>
                    <td>
                   Add:IGST
                    </td>
                      <td >
                     <label runat="server" id="lblSC_IGST_NMR"></label><b>&#37;</b>
                    </td>

                    <td style="text-align:right">
                      <label runat="server" id="lblSC_IGST_Amount_NMR"></label>
                    </td>
                     
                </tr>

                  <tr>
                    <td>
                      Note:  TDS @ 194C @ <label runat="server" id="lblSC_TDS_Note_NMR"></label>  <b>&#37;</b>
                    </td>
                    <td colspan="2">
                      Total amount:GST
                    </td>
                      <td style="text-align:right">
                           <label runat="server" id="lblGSTTotal_NMR"></label>
                      </td>
                </tr>
                 <tr>
                    <td>
                     Annexure A,B & C are the internal part of this Service Invoice
                    </td>
                    <td colspan="2">
                     <b >Total Amount &nbsp; &#x20b9;</b>
                      
                    </td>
                    <td style="text-align:right">
                      <b>   <label runat="server" id="lblTotalAmont_H_NMR"  ></label> </b>
                    </td>
                </tr>
                 <tr>
                    <td>
                 Annexure A Work Done Activity (weekly)
                    </td>
                     <td>
                 TDS Deduction 
                    </td>
                      <td >
                     <label runat="server" id="lblSC_TDS_NMR" > </label>  <b>&#37;</b>
                    </td>
                    <td style="text-align:right">
                   <label runat="server" id="lblTDS_amount_NMR"></label>
                    </td>
                </tr>
                 <tr>
                    <td>
               Annexure B Material Received on Debitable Basis
                    </td>
                     <td>
                Retention
                    </td>
                      <td >
                     <label runat="server" id="lblRetentionPerc_NMR"></label><b>&#37;</b>
                    </td>
                    <td style="text-align:right" >
                     <label runat="server" id="lblRetentionAmont_NMR"></label>
                    </td>
                </tr>
                <tr>
                    <td>Annexure C Receipt of Payment as on date of current Invoice</td>
                    
                         <td style="text-align:right;font-weight:bold" colspan="3">
                        <b>Net Amount  &nbsp; &#x20b9; <label style="padding-left:190px" runat="server"  id="lblNetAmount_NMR"></label>  </b>
                    </td>
                   
                </tr>
              <%-- <tr>
                    <td style="border-bottom:1px solid #fff;"></td>
                   
                   
                </tr>--%>
                  
                
             <%--   <tr>
                    <td colspan="4">
                        Amount Chargeable (in words) : &nbsp;
                        <b><asp:Label runat="server" Text="" ID="lblAmountInWords"></asp:Label></b>
                    </td>
                </tr>

                <tr>
                    <td style="border-bottom:1px solid #fff;border-right:1px solid #fff;">
                        <p>Terms and Conditions<br /> 
                            <asp:Label runat="server" Text="" ID="lblAnnexure"></asp:Label>
                        </p>
                    </td>
                    <td colspan="5" style="vertical-align: bottom;">
                        <p style="text-align:right;font-weight:600;">E. & O.E</p>
                    </td>
                </tr>--%>

                <tr>
                    <td></td>
                    <td colspan="3" style="text-align:right;">
                        <%--<p><b>For United Global Corporation Limited</b></p>--%>
                        <div style="height:70px">
                         
                                                  </div>
                        <p>Authorised Signatory</p>
                    </td>
                </tr>
            
            </table>
            
        <%--    <div runat="server" id="div_Continue" style="width:90%; height:30px;">
                <p style="float:right; padding-top:10px">
                    Continued...&nbsp; &nbsp;
                </p>
            </div>--%>
            </div>
         
           <%-- <div class="invoiceFooter">
                <p style="margin:0">This is a Computer Generated Document</p>
                <img src="../Style/Images/FooterImg.png" alt="WO Footer" style="height:80px; width:96%"/>
            </div>--%>

        </div>
    </div>
    <div class="page-break"></div>
     <div id="divToPrint2" runat="server" style="width: 880px">
           <%-- <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width:98%; height:110px"/>
            </div>--%>
           <%-- <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="Label1" ></asp:Label></b>
            </div>--%>
            <div class="mypageadjustfirst1">
                  <b>Annexure A (DPR)</b>
                <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; margin-top:-1px; border:none">
                      <tr>
                            <td rowspan="1" style="width:50%">
                                   <div runat="server" id="div2" visible="false"></div>
            <div runat="server" id="div3" visible="false">Cancelled</div>
                <div  runat="server"  id="SCname1"  style="text-align:center"><span style="font-size:large" ><b> <label runat="server" id="lblScnmae2"></label> </b> </span></div>
                 <div  runat="server"  id="Div8"  style="text-align:center">   Work Order Number: <label runat="server" id="lblWOnumber"></label></div>
                                 <div  runat="server"  id="Div5"  style="text-align:center"> Service Invoice Number: <label runat="server" id="lblInvoiceNumber"></label></div>
                 <div  runat="server"  id="Div6"  style="text-align:center"> Scope of Work : <label runat="server" id="lblSCWork"></label></div>
                <div  runat="server"  id="Div7"  style="text-align:center">      Date : <label runat="server" id="lblDate"></label></div>
                      
          
          
            

                            </td>
                          
                     </tr>

                </table>
         
                 
                <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; margin-top:-1px; border:none" id="mytbl2">
                <tr runat="server" >
                    <td>
                        <asp:GridView ID="GridDPR_Items" Width="100%" runat="server" CssClass="GridCenter" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField ItemStyle-Width="50px"  HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Work Done Activity">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypePerc" runat="server" Text='<%#Eval("Work_Done_Activity")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>
                                   <asp:TemplateField ItemStyle-Width="50px" HeaderText="Present Progress">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%#Eval("Present_Progress").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%#Eval("UOM").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Cumulative Progress">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Math.Round(decimal.Parse(Eval("Cumulative_Progress").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                 <asp:TemplateField ItemStyle-Width="50px" HeaderText="Work Location/Chainage">
                                    <ItemTemplate>
                                        <asp:Label ID="lblILocation_Chainage" runat="server" Text='<%#Eval("Location_Chainage").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>
                 
            </table>
                 <br />
                  <br />
                 <div class="invoiceFooter">
                <p style="margin:0">This is a Computer Generated Document</p>
                     <div  style="text-align:right;" >
                        <div style="height:70px">
                          
                                                  </div>
                        <p>Authorised Signatory</p>
                    </div>
            </div>
                    </div>
           </div>
    <div class="page-break"></div>
      <div id="div_NMR" runat="server" style="width: 880px">
           <%-- <div style="width: 100%;">
                <img src="../Style/Images/HeaderImg.png" alt="WO Header" style="width:98%; height:110px"/>
            </div>--%>
           <%-- <div style="text-align: right; padding-right: 30px">
                <b>Page No: <asp:Label runat="server" ID="Label1" ></asp:Label></b>
            </div>--%>
            <div class="mypageadjustfirst1">
                  <b>Annexure B (NMR)</b>
                <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; margin-top:-1px; border:none">
                      <tr>
                            <td rowspan="1" style="width:50%">
                                   <div runat="server" id="div18" visible="false"></div>
            <div runat="server" id="div19" visible="false">Cancelled</div>
                <div  runat="server"  id="Div20"  style="text-align:center"><span style="font-size:large" ><b> <label runat="server" id="lblScnmae2NMR"></label> </b> </span></div>
                 <div  runat="server"  id="Div21"  style="text-align:center">   Work Order Number: <label runat="server" id="lblWOnumber_NMR"></label></div>
                                 <div  runat="server"  id="Div22"  style="text-align:center"> Service Invoice Number: <label runat="server" id="lblInvoiceNumber_NMR"></label></div>
                 <div  runat="server"  id="Div23"  style="text-align:center"> Scope of Work : <label runat="server" id="lblSCWork_NMR"></label></div>
                <div  runat="server"  id="Div24"  style="text-align:center">      Date : <label runat="server" id="lblDate_NMR"></label></div>
                      
          
          
            

                            </td>
                          
                     </tr>

                </table>
         
                 
                <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; margin-top:-1px; border:none" id="mytbl2">
                <tr runat="server" >
                    <td>
                        <asp:GridView ID="Grid_LabourList" Width="100%" runat="server" CssClass="GridCenter" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"   HeaderStyle-HorizontalAlign="Center" HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDescription" runat="server"   Text='<%#Eval("Labour_Date")%>' DataFormatString="{0:MM/dd/yyyy}" ></asp:Label>
                                    </ItemTemplate>
                                     <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Labour Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypePerc" runat="server" Text='<%#Eval("Labour_Type")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>
                                   <asp:TemplateField ItemStyle-Width="50px" HeaderText="No Of Labour's"  ItemStyle-HorizontalAlign="Center"  HeaderStyle-HorizontalAlign="Center" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt"  runat="server" Text='<%#Eval("NoOf_Labour").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                       <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Labour Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%#Eval("Labour_Rate").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Labour Total Cost">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Math.Round(decimal.Parse(Eval("LabourCost_Total").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                               
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>
                 
            </table>
                 <br />
                  <br />
                 <div class="invoiceFooter">
                <p style="margin:0">This is a Computer Generated Document</p>
                     <div  style="text-align:right;" >
                        <div style="height:70px">
                          
                                                  </div>
                        <p>Authorised Signatory</p>
                    </div>
            </div>
                    </div>
           </div>
    <div class="page-break"></div>
    <div id="divToPrint3" runat="server" style="width: 880px">
             <div class="mypageadjustfirst2">
           <b>  Annexure C (MIN)</b>
          <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; margin-top:-1px; border:none">
                      <tr>
                            <td rowspan="1" style="width:50%">
                                <div  runat="server"  id="Div10"  style="text-align:center"><span style="font-size:large"><b> <label runat="server" id="lblScnmae2MIN"> </label>  </b>  </span></div>
                 <div  runat="server"  id="Div11"  style="text-align:center"> Service Invoice Number: <label runat="server" id="lblInvoiceNumberMIN"></label></div>
                 <div  runat="server"  id="Div12"  style="text-align:center"> Scope of Work : <label runat="server" id="lblSCWorkMIN"></label></div>
                <div  runat="server"  id="Div13"  style="text-align:center">      Date : <label runat="server" id="lblDateMIN"></label></div>
                       <div  runat="server"  id="Div14"  style="text-align:center">   Work Order Number: <label runat="server" id="lblWOnumberMIN"></label></div>
                                </td>
                          
                          </tr>
                
                
                </table>
                 <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; margin-top:-1px; border:none" id="mytbl3">
                <tr runat="server" >
                    <td>
                         <asp:GridView ID="Grid_SC_MIN" Width="100%" ShowColumnsFooter="true"  runat="server" CssClass="GridCenter" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField ItemStyle-Width="50px" Visible="false" HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("Item_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypePerc" runat="server" Text='<%#Eval("UOMPrefix")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Requested Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Eval("RequestedQty").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                 <asp:TemplateField ItemStyle-Width="50px" HeaderText="Approved Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Eval("ApprovedQty").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="50px" HeaderText="Rate Per Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Eval("Rate").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                 <asp:TemplateField ItemStyle-Width="50px" HeaderText="Total Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Eval("TotalValueR").ToString()%>'></asp:Label>
                                    
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Eval("Remarks").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>

            </table>
         <br />
                  <br />
                 <div class="invoiceFooter">
                <p style="margin:0">This is a Computer Generated Document</p>
                     <div  style="text-align:right;" >
                        <div style="height:70px">
                           
                                                  </div>
                        <p>Authorised Signatory</p>
                    </div>
            </div>
            
        </div>    
        </div>
         <div class="page-break"></div>
        
         <div id="divToPrint4" runat="server" style="width: 880px">
             <div class="mypageadjustfirst3">
                <b>Annexure D (Completed Payment Details)</b>
                     <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; margin-top:-1px; border:none">
                      <tr>
                            <td rowspan="1" style="width:50%"> 
                                     <div  runat="server"  id="Div4"  style="text-align:center"><span style="font-size:large" ><b>  <label runat="server" id="lblScnmae2PY"></label></b> </span> </div>
                  <div  runat="server"  id="Div17"  style="text-align:center">   Work Order Number: <label runat="server" id="lblWOnumberPY"></label></div>
                                  <div  runat="server"  id="Div9"  style="text-align:center"> Service Invoice Number: <label runat="server" id="lblInvoiceNumberPY"></label></div>
                 <div  runat="server"  id="Div15"  style="text-align:center"> Scope of Work : <label runat="server" id="lblSCWorkPY"></label></div>
                <div  runat="server"  id="Div16"  style="text-align:center">      Date : <label runat="server" id="lblDatePY"></label></div>
                    
                                </td>
                         
                          </tr>
                          </table>
                
              
                 <table class="table table-bordered" style="width: 97%; margin-bottom: 0px; margin-top:-1px; border:none" id="mytbl2a">

                <tr>
                    <td style="padding: 0; border:none;">
                        <asp:GridView ID="GridView3" Width="100%" runat="server" AutoGenerateColumns="false" CssClass="GridCenter" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial_No" runat="server" Text='<%#Eval("Serial_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PayInd No" ItemStyle-Width="49.8%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItem_Name" runat="server" Text='<%#Eval("PayInd_No")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Payment Indent Date">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("PayInd_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transferred Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQty_required" runat="server" Text='<%#Eval("Amt_Approved")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Transaction Detail" HeaderStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRate" runat="server" Text='<%#Eval("Payment_Ref_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" Wrap="false" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Work Order Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUom" runat="server" Text='<%#Eval("WONo")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" Wrap="false" />
                                </asp:TemplateField>
                               
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>

                <tr runat="server" >
                    <td>
                         <asp:GridView ID="Grid_CompletedPayment" Width="100%" runat="server" CssClass="GridCenter" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray">

                            <Columns>
                                <asp:TemplateField ItemStyle-Width="50px" Visible="false" HeaderText="S.No">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("ItemCode")%>'></asp:Label>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderText="PayInd No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemDescription" runat="server" Text='<%#Eval("PayInd_No")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Payment Indent Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypePerc" runat="server" Text='<%#Eval("PayInd_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter3" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Transferred Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Math.Round(decimal.Parse(Eval("Amt_Approved").ToString()))%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                 <asp:TemplateField ItemStyle-Width="50px" HeaderText="Transaction Detail">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Eval("Payment_Ref_No").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="50px" HeaderText="Work Order Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemTypeAmt" runat="server" Text='<%# Eval("WONo").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="GridCenter2" />
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                    </td>
                </tr>

            </table>
                <br />
                <br />
            <table class="table table-bordered" style="width: 67%; margin-bottom:0px;text-align:center" id="anx5">
                             <tr>
                           <td style="border-right:none"> <label style="text-align:center"> <b> Summary</b></label> </td>
                                 <td style="border-left:none"></td>
                           </tr>
                 <tr>
                      <td>Total Billing Amount As on date </td>
                       <td><b>  <label runat="server" id="lblTotalBilledAmont"></label></b></td>
                  </tr>
                
                 <tr>
                      <td>Current Invoice </td>
                      <td><b><label runat="server" id="lblCurrentInvoiceAmount"> </label></b></td>
                  </tr>
                  <tr>
                      <td>Total Amount paid as on Date</td>
                      <td><b><label runat="server" id="lblTotalPaidAmont"> </label></b></td>
                  </tr>
                  
                       <tr>
                      <td>Total Receivable</td>
                       <td><b><label runat="server" id="lblPendingAmount"></label></b> </td>
                  </tr>
            </table>
                 <br />
                  <br />
            <div class="invoiceFooter">
                <p style="margin:0">This is a Computer Generated Document</p>
                     <div  style="text-align:right;" >
                        <div style="height:70px">
                            
                                                  </div>
                        <p>Authorised Signatory</p>
                    </div>
            </div>
              </div>
        </div>
     <div class="page-break"></div>
   


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

