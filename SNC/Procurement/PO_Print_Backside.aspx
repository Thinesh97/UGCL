<%@ Page Language="C#" MasterPageFile="~/MasterPage/SNC_Print.Master" AutoEventWireup="true" CodeBehind="PO_Print_Backside.aspx.cs" Inherits="SNC.Procurement.PO_Print_Backside" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .tbl_1 td:nth-child(4),
        .tbl_1 td:nth-child(5),
        .tbl_1 td:nth-child(6),
        .tbl_1 td:nth-child(7),
        .tbl_2 td:nth-child(3),
        .tbl_2 td:nth-child(4),
        .tbl_3 td:nth-child(2),
        .tbl_3 td:nth-child(3) {
            text-align: right;
        }

        /*@page {
            _size: 8.5in 11in;
            margin-top: 140px;
        }*/

        @media print {
            tr {
                page-break-before: always;
            }

            .no-print, .no-print * {
                display: none !important;
            }
        }


        table {
            width: 825px;
            border: 0;
        }


        td {
            text-align: justify;
            font-family: Arial;
            font-size: 15px;
            border: none;
            padding-bottom: 5px;
        }

            td b {
                font-size: 16px;
            }

        b {
            font-size: 16px;
        }

        #mid {
            width: 100%;
            padding: 0;
        }

        .firsttd {
            font-weight: bold;
            font-size: 16px;
            vertical-align: top;
            width: 30px;
        }
    </style>
    <div id="mid">
        <div id="divToPrint">
            <div id="divTableDataHolder" style="border:1px solid black;width: 880px">
                 <div style="width:85%">
                    <img src="../Style/Images/continuationHeader.JPG" alt="PO Header" width="100%" />

                </div>
                <br />
                <b><u>PURCHASE ORDER</u> </b>
                <br />
                <b><u>TERMS AND CONDITIONS OF PURCHASE</u>	</b>
                <br />
                <br />


                <table border="0" style="width: 830px; text-align: justify">
                    <tr>
                        <td class="firsttd">1.
                        </td>
                        <td>
                            <b>GENERAL: </b>&nbsp;This Purchase Order subject to the following terms and conditions unless others specified.
                        </td>


                    </tr>
                    <tr>
                        <td class="firsttd">2.
                        </td>
                        <td>
                            <b>CONFIRMATION:</b>&nbsp;This purchase order will be  deemed to have been accepted by the supplier, &nbsp;&nbsp;unless otherwise&nbsp;&nbsp;&nbsp; adviced in writing by the supplier within 15 days of the Purchase Order .
                        </td>


                    </tr>
                    <tr>
                        <td class="firsttd">3.
                        </td>
                        <td>
                            <b>POINT OF DELIVERY:</b> &nbsp;All Materials are delivered at SNC Bangalore or work site as specified in Purchase Order.
                        </td>


                    </tr>
                    <tr>
                        <td class="firsttd">4.
                        </td>
                        <td>
                            <b>INSPECTION</b>:&nbsp; All materials are subject to Inspection at incoming stage or in process by SNC Quality Assurance Department for compliance with  standards and specifications mentioned in the purchase order.&nbsp;
                          The supplier at his own cost shall replace any complaint material.&nbsp;SNC will advice the supplier of any rejections within two months from date of supply.
                        </td>
                    </tr>

                    <tr>
                        <td class="firsttd">5.
                        </td>
                        <td>
                            <b>REJECTION</b>:&nbsp;Rejected Material If any ,shall be replaced by the supplier at his own cost or the cost of rejected materials shall be returned to the purchaser.
                        </td>
                    </tr>


                    <tr>
                        <td class="firsttd">6.
                        </td>
                        <td>
                            <b>PACKING</b>:&nbsp;The packing of goods shall be adequate to prevent damage during handling support of goods.&nbsp;The purchase of packing is inclusive unless otherwise ageed.
                        </td>
                    </tr>


                    <tr>
                        <td class="firsttd">7.
                        </td>
                        <td>
                            <b>PRICE</b>:&nbsp;The prices are inclusive of Packing Insurance and Delivery.
                        </td>
                    </tr>

                    <tr>
                        <td class="firsttd">8.
                        </td>
                        <td>
                            <b>TAXES</b>:&nbsp;GST and other Statutory levies are payable at the applicable rates of materials to be supplied and supplier has to generate the required E- way bill as per the GST Act. GST amount of the invoice will 
                            be paid once credit  appears in SNC GST portal .
                        </td>
                    </tr>
                    <tr>
                        <td class="firsttd">9.
                        </td>
                        <td>
                            <b>INVOICE</b>:&nbsp;Tax Invoice shall be furnished along with materials&nbsp;Delay in submission of bills beyond 60 days will not be admitted.
                        </td>
                    </tr>

                    <tr>
                        <td class="firsttd">10.
                        </td>
                        <td>
                            <b>DELIVERY TIME</b>:&nbsp;Time is of essence and supplier shall deliver goods strictly according to the time specified in the purchase order&nbsp;Compensation of late delivery shall be as mutually agreed and stated in the purchase order.
                        </td>
                    </tr>

                    <tr>
                        <td class="firsttd">11.
                        </td>
                        <td>
                            <b>WARRANTY</b>:&nbsp;All materials and goods shall be free from defects(including latent defects) and guaranteed for satisfactoy performance for a period of 12 months from the date of supply.
                        </td>
                    </tr>



                    <tr>
                        <td class="firsttd">12.
                        </td>
                        <td>
                            <b>SPECIAL TERMS AND CONDITIONS OF WAIVERS</b>:&nbsp;This purchase order is subject to additional terms and or waivers if any,&nbsp;if specified in the order.
                        </td>
                    </tr>

                    <tr>
                        <td class="firsttd">13.
                        </td>
                        <td>
                            <b>CONSTRUCTIONS OF THE CONTRACT</b>:&nbsp;This purchase order is the contract under the Laws of Union of India and any disputes arising there shall be settled in the court of Bangalore only.
                        </td>
                    </tr>

                    <tr>
                        <td class="firsttd">14.
                        </td>
                        <td>
                            <b>VALIDITY</b>:&nbsp;The order remains valid till cancelled.If the Purchase Order is not executed within the period mentioned in the order   or abandoned without informing within 7 days of the Purchase Order, liquidated damages will be levied at 15% of the Purchase Order Value. 
                        </td>
                    </tr>

                    <tr>
                        <td class="firsttd">15.
                        </td>
                        <td>No Cash or in kind by way of bribes ,gifts ,parties , etc., shall be offered any of our company employees for any favour . If any such incident comes to our knowledge ,we shall withhold your payment without intimation to you .&nbsp;Also if rates are higher than the prevailing market rate by more than 15% of payment shall be made proportionately
                          and you become disqualified for future supplies/contracts.
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: left;">
                            <b style="width: 15px"></b>
                           

                        </td>
                        <!-- <td style="text-align: right;" >
                             <br /> 
                            <b>For Shankaranarayana Constructions P Ltd.,</b>

                            <br /><br />
                              <asp:Image ID="ImgAuthorisedSign" Height="80" Width="120" Visible="false" BorderColor="White" runat="server" />
                         <br /><br />
                            <b>Authorised Signatory</b>

                        </td> -->
                    </tr>
                    <%--<tr>
                      <td>
                         
                      </td>
                     <td>

                     </td>
                      <td></td>
                      
                      <td>
                          For SNC POWER CORPORATION PVT.LTD,
                      </td>
                  </tr>
                  <tr>

                  </tr>
                  <tr>
                      <td>

                      </td>
                      <td></td>
                      <td>
                          Authorised Signatory
                      </td>
                  </tr>--%>
                </table>
               
            </div>
        </div>
    </div>
</asp:Content>
