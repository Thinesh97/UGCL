<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print_Internal_Transfer_Assets.aspx.cs" Inherits="Internal_Transfer_Assets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Shankaranarayana Constructions P Ltd.</title>
    <style>
           html, body, div, span, object, iframe,
h1, h2, h3, h4, h5, h6, p, blockquote, pre,
abbr, address, cite, code,
del, dfn, em, img, ins, kbd, q, samp,
small, strong, sub, sup, var,
b, i,
dl, dt, dd, ol, ul, li,
fieldset, form, label, legend,
table, caption, tbody, tfoot, thead, tr, th, td,
article, aside, canvas, details, figcaption, figure,
footer, header, hgroup, menu, nav, section, summary,
time, mark, audio, video {
    margin: 0;
    padding: 0;
    border: 0;
    outline: 0;
    font-size: 100%;
    vertical-align: baseline;
    background: transparent;
}

body {
    line-height: 1;
}
            body {
                margin: 0 auto;
                width: 100%;
                background: #808080;
                color: #4d4d4d;
                font-family:'Trebuchet MS';
                font-size:14px;
            }

            #mid {
                width: 1024px;
                margin:0 auto;
                height: auto;
                background: #fff;
                padding-bottom:15px;
                font-size: 12px;
                 padding:10px;
            }
         table {
                border-collapse: collapse;
                border-spacing: 0; 
            }
       
         .tbl,.tble{
             width:95%;
             margin:15px;
            
         }
         .tbl td,.tbl th{
             border:1px solid #ccc;
         }
         .tbl td{
             padding:10px;
         }
         .tbl th,.tbl .colr{
             _background:#369ce0;
             _color:#fff;
         }
         .tbl th,.tble th{
             padding:5px;
         }
         p{
             line-height:15px;
         }
         h1,h2,h3{
             line-height:18px;
         }
         h1{
             font-size:20px;
             line-height:30px;
         }

          .clr{
              clear:both;
          }
          p{
              line-height:20px;
              letter-spacing:0.5px;
          }
            .txtIp{
                text-align:center;
                width: 95%;
            }
            .cBx td{
                border:none;
            }
            .cBx input{
                margin-left: 20px;
            }
             .lst{
                margin:20px;
            }
            .lst ul li{
                margin-left:20px;
                line-height:25px;
                letter-spacing:0.5px;
                list-style-type:disc;
            }
            .lst h2{
                font-size:14px;
                margin:20px;
            }
    </style>
</head>
<body>

    <form id="form1" runat="server">
        <div id="mid">      
            <div style="text-align:center;">
                <h1>SNC POWER CORPORATION PVT LTD</h1>
                <h1>SNC HOUSE,No 7,Residency Road , Bangalore - 560025</h1>
            </div>

            <div style="text-align:center;font-size:18px;margin:10px 0 10px 0;">
               <h2>Internal Transfer of Machineries / Vehicles / Equipment / Instruments</h2>
            </div>

            <div>
                <table class="tbl">
                    <tr>
                        <th>Sl.No</th>
                        <th  colspan="4">Details of the sender Site </th>
                    </tr>
                    <tr>
                        <td>1</td>
                        <td colspan="4">Name of the Site:
                             <asp:Label ID="lblSite" runat="server" ></asp:Label>
                        </td>
                       
                    </tr>
                    <tr>
                        <td>2</td>
                        <td colspan="4">Date Of Dispatch:
                            <asp:Label ID="lbldispatch" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="5">3</td>
                        <td rowspan="5">Details of Material / Equipment </td>
                        <td>Manufacture</td>
                        <td colspan="2">
                            <asp:Label ID="lblmanufacture" runat="server"></asp:Label>
                        </td>
                         
                    </tr>

                    <tr>
                        <td>Type</td>
                        <td colspan="2">
                            <asp:Label ID="lblType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Model & Year</td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td>Capacity</td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td>Asset Code & Reg .No</td>
                        <td colspan="2">
                            <asp:Label ID="lblcoderegno" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="3">4</td>
                        <td rowspan="3">Accessories</td>
                        <td>History / Log book</td>
                        <td colspan="2"></td>
                    </tr>
                     <tr>
                        <td>OEM Manual</td>
                        <td colspan="2"></td>
                    </tr>
                     <tr>
                        <td>Tools / Spares</td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td>5</td>
                        <td colspan="4">Condition of the material / Equipment</td>
                       
                    </tr>
                    <tr>
                        <td>6</td>
                        <td colspan="4">Photos</td>
                       
                    </tr>
                    <tr>
                        <td rowspan="2">7</td>
                        <td  colspan="4">RTO Original Documents to confirm as applicable:</td>
                        
                    </tr>
                    <tr>
                        <td  colspan="4">
                            <asp:CheckBox ID="chkDL" runat="server" Text="DL" Enabled="false" CssClass="cBx" />
                            <asp:CheckBox ID="chkRC" runat="server" Text="RC" Enabled="false" CssClass="cBx" />
                            <asp:CheckBox ID="chkROADTAXRECEIPT" runat="server" Text="ROAD TAX RECEIPT" Enabled="false" CssClass="cBx" />
                            <asp:CheckBox ID="chkINSURANCE" runat="server" Text="INSURANCE" CssClass="cBx" Enabled="false" />
                            <asp:CheckBox ID="chkPERMIT" runat="server" Text="PERMIT" CssClass="cBx" Enabled="false" />
                            <asp:CheckBox ID="chkNOC" runat="server" Text="NOC" CssClass="cBx" Enabled="false" />
                            <asp:CheckBox ID="chkFC" runat="server" Text="FC" CssClass="cBx" Enabled="false" />
                            <asp:CheckBox ID="chkWayBILL" runat="server" Text="Way BILL" CssClass="cBx"  Enabled="false"/>

                           
                        </td>
                   </tr>
                    <tr>
                        <td rowspan="2">8</td>
                        <td></td>
                        <td></td>
                        <td></td>
                      <td></td>
                    </tr>
                    <tr>
                        <td>Stores Incharge</td>
                        <td>Admin Incharge</td>
                        <td> P & M site</td>
                        <td>Project Incharge</td>
                    </tr>
                </table>
            </div>

            <p style="margin-left:15px;"><strong>Note:Before dispactch of any P & M , it should be thoroughly inspected , repaired & cleaned</strong></p>
       
            <div>
                <table class="tbl">
                    <tr>
                        <th>Sl.No</th>
                        <th  colspan="4">Details of the Receiver Site </th>
                    </tr>
                    <tr>
                        <td>1</td>
                        <td colspan="4">Name of the Site:
                            <asp:Label ID="lblsite2" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>2</td>
                        <td colspan="4">Date Of Dispatch:
                            <asp:Label ID="lbldispatch2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="5">3</td>
                        <td rowspan="5">Details of Material / Equipment </td>
                        <td>Manufacture</td>
                        <td colspan="2">
                            <asp:Label ID="lblManufacturserver" runat="server"></asp:Label>
                        </td>
                         
                    </tr>

                    <tr>
                        <td>Type</td>
                        <td colspan="2">
                            <asp:Label ID="lbltype2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>Model & Year</td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td>Capacity</td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td>Asset Code & Reg .No</td>
                        <td colspan="2">
                            <asp:Label ID="lblcodeandreg2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="3">4</td>
                        <td rowspan="3">Accessories</td>
                        <td>History / Log book</td>
                        <td colspan="2"></td>
                    </tr>
                     <tr>
                        <td>OEM Manual</td>
                        <td colspan="2"></td>
                    </tr>
                     <tr>
                        <td>Tools / Spares</td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td>5</td>
                        <td colspan="4">Condition of the material / Equipment</td>
                       
                    </tr>
                    <tr>
                        <td>6</td>
                        <td colspan="4">Photos</td>
                       
                    </tr>
                    <tr>
                        <td rowspan="2">7</td>
                        <td  colspan="4">RTO Original Documents to confirm as applicable:</td>
                        
                    </tr>
                    <tr>
                        <td  colspan="4">
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="DL" CssClass="cBx"  Enabled="false"/>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="RC" CssClass="cBx" Enabled="false"/>
                            <asp:CheckBox ID="CheckBox3" runat="server" Text="ROAD TAX RECEIPT" CssClass="cBx" Enabled="false"/>
                            <asp:CheckBox ID="CheckBox4" runat="server" Text="INSURANCE" CssClass="cBx" Enabled="false"/>
                            <asp:CheckBox ID="CheckBox5" runat="server" Text="PERMIT" CssClass="cBx" Enabled="false"/>
                            <asp:CheckBox ID="CheckBox6" runat="server" Text="NOC" CssClass="cBx" Enabled="false"/>
                            <asp:CheckBox ID="CheckBox7" runat="server" Text="FC" CssClass="cBx" Enabled="false"/>
                            <asp:CheckBox ID="CheckBox8" runat="server" Text="Way BILL" CssClass="cBx" Enabled="false" />
                        </td>
                   </tr>
                    <tr>
                        <td rowspan="2">8</td>
                        <td></td>
                        <td></td>
                        <td></td>
                      <td></td>
                    </tr>
                    <tr>
                        <td>Stores Incharge</td>
                        <td>Admin Incharge</td>
                        <td> P & M site</td>
                        <td>Project Incharge</td>
                    </tr>
                </table>
            </div>


            <div class="lst">
                <ul>
                    <li>This form should be sent along with the consignment by the sender site.</li>
                    <li>This form should be sent to HO within 24 Hours on receiving consignment by the receiver site.</li>
                    <li>This from should be sent to HO by the sender after transfer and sender should inform P& M HO prior to transfer.</li>
                    <li>Make sure to obtain temp-permit for passing States in the absence of national permits - 1 KM before such check post.</li>
                    <li> Sender Site to attach an additional brief history note  regarding repair, Maintenance and working status of the transferring P&M at the site for last 6 Months.</li>
                </ul>
            </div>

            <p style="margin-left:15px;"><strong>Cc to: Receiver Site, PC, GM-Adm, GM-HR, Sr.Mgr - P&M,Sr.Mgr-Pur,H.O</strong></p>
        </div>
  </form>
</body>
</html>
