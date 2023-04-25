<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="StaffSalaries.aspx.cs" Inherits="StaffSalaries" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
        <script type="text/javascript">
        $(document).ready(function () {
            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });

        });

        function ConfirmDelete() {
            if (confirm("Are you sure want to delete this record?") == false) {
                return false;
            }
            return true;
        }
        //Hey this is testing

        $(document).ready(function () {
            var Basic_Pay = $('#ContentPlaceHolder1_txtBasic_Pay').val();
            var YearlyBasic_PayY = parseInt(Basic_Pay * 12);
            $('#ContentPlaceHolder1_txtBasic_PayY').val(YearlyBasic_PayY);

            var Rental_Allowance = $('#ContentPlaceHolder1_txtHouse_Rental_Allowance').val();
            var YearlyRental_Allowance = parseInt(Rental_Allowance * 12);
            $('#ContentPlaceHolder1_txtHouse_Rental_AllowanceY').val(YearlyRental_Allowance);

            var Conveyance_Allowance = $('#ContentPlaceHolder1_txtConveyance_Allowance').val();
            var YearlyConveyance_AllowanceY = parseInt(Conveyance_Allowance * 12);
            $('#ContentPlaceHolder1_txtConveyance_AllowanceY').val(YearlyConveyance_AllowanceY);

            var Special_Allowance = $('#ContentPlaceHolder1_txtSpecial_Allowance').val();
            var YearlySpecial_Allowance = parseInt(Special_Allowance * 12);
            $('#ContentPlaceHolder1_txtSpecial_AllowanceY').val(YearlySpecial_Allowance);

            var Sub_Total_A = $('#ContentPlaceHolder1_txtSub_Total_A').val();
            var YearlySub_Total_A = parseInt(Sub_Total_A * 12);
            $('#ContentPlaceHolder1_txtSub_Total_AY').val(YearlySub_Total_A);

            var PF_ER = $('#ContentPlaceHolder1_txtPF_ER').val();
            var YearlyPF_ER = parseInt(PF_ER * 12);
            $('#ContentPlaceHolder1_txtPF_ERY').val(YearlyPF_ER);

            var Cost_to_Company = $('#ContentPlaceHolder1_txtTotal_Cost_to_Company').val();
            var YearlyCost_to_Company = parseInt(Cost_to_Company * 12);
            $('#ContentPlaceHolder1_txtTotal_Cost_to_CompanyY').val(YearlyCost_to_Company);

            var PF = $('#ContentPlaceHolder1_txtPF').val();
            var YearlyPF = parseInt(PF * 12);
            $('#ContentPlaceHolder1_txtPFY').val(YearlyPF);

            var PT = $('#ContentPlaceHolder1_txtPT').val();
            var YearlyPT = parseInt(PT * 12);
            $('#ContentPlaceHolder1_txtPTY').val(YearlyPT);

            var Deductions_B = $('#ContentPlaceHolder1_txtTotal_Deductions_B').val();
            var YearlyDeductions_B = parseInt(Deductions_B * 12);
            $('#ContentPlaceHolder1_txtTotal_Deductions_BY').val(YearlyDeductions_B);

            var NET_PAYMENT = $('#ContentPlaceHolder1_txtNET_PAYMENT').val();
            var YearlyNET_PAYMENT = parseInt(NET_PAYMENT * 12);
            $('#ContentPlaceHolder1_txtNET_PAYMENTY').val(YearlyNET_PAYMENT);

        });

        $(document).ready(function () {
            $("#ContentPlaceHolder1_txtBasic_Pay").change(function () {
                var Basic_Pay = $('#ContentPlaceHolder1_txtBasic_Pay').val();
                var Yearly = parseInt(Basic_Pay * 12);
                $('#ContentPlaceHolder1_txtBasic_PayY').val(Yearly);

            });
            $("#ContentPlaceHolder1_txtHouse_Rental_Allowance").change(function () {
                var Rental_Allowance = $('#ContentPlaceHolder1_txtHouse_Rental_Allowance').val();
                var Yearly = parseInt(Rental_Allowance * 12);
                $('#ContentPlaceHolder1_txtHouse_Rental_AllowanceY').val(Yearly);

            });

            $("#ContentPlaceHolder1_txtConveyance_Allowance").change(function () {
                var Conveyance_Allowance = $('#ContentPlaceHolder1_txtConveyance_Allowance').val();
                var Yearly = parseInt(Conveyance_Allowance * 12);
                $('#ContentPlaceHolder1_txtConveyance_AllowanceY').val(Yearly);

            });

            $("#ContentPlaceHolder1_txtSpecial_Allowance").change(function () {
                var Special_Allowance = $('#ContentPlaceHolder1_txtSpecial_Allowance').val();
                var Yearly = parseInt(Special_Allowance * 12);
                $('#ContentPlaceHolder1_txtSpecial_AllowanceY').val(Yearly);

            });
            $("#ContentPlaceHolder1_txtPF_ER").change(function () {
                var PF_ER = $('#ContentPlaceHolder1_txtPF_ER').val();
                var YearlyPF_ER = parseInt(PF_ER * 12);
                $('#ContentPlaceHolder1_txtPF_ERY').val(YearlyPF_ER);

            });
            $("#ContentPlaceHolder1_txtPF").change(function () {
                var PF_E = $('#ContentPlaceHolder1_txtPF').val();
                var YearlyPF_E = parseInt(PF_E * 12);
                $('#ContentPlaceHolder1_txtPFY').val(YearlyPF_E);

            });
            $("#ContentPlaceHolder1_txtPT").change(function () {
                var PF_PT = $('#ContentPlaceHolder1_txtPT').val();
                var YearlyPF_PT = parseInt(PF_PT * 12);
                $('#ContentPlaceHolder1_txtPTY').val(YearlyPF_PT);

            });
            $("#ContentPlaceHolder1_txtPF").change(function () {
                var PF_Emp = $('#ContentPlaceHolder1_txtPF').val();
                var YearlyPF_Emp = parseInt(PF_Emp * 12);
                $('#ContentPlaceHolder1_txtPFY').val(YearlyPF_Emp);

            });
            $("#ContentPlaceHolder1_txtPT").change(function () {
                var PT = $('#ContentPlaceHolder1_txtPT').val();
                var YearlyPT = parseInt(PT * 12);
                $('#ContentPlaceHolder1_txtPTY').val(YearlyPT);

            });


            $('.CalculateSubTotal').on('change', function () {
                var Basic_Pay = $('#ContentPlaceHolder1_txtBasic_Pay').val();
                var Rental_Allowance = $('#ContentPlaceHolder1_txtHouse_Rental_Allowance').val();
                var Conveyance_Allowance = $('#ContentPlaceHolder1_txtConveyance_Allowance').val();
                var Special_Allowance = $('#ContentPlaceHolder1_txtSpecial_Allowance').val();
                var SubtotalA = parseInt(Basic_Pay || '0') + parseInt(Rental_Allowance || '0') + parseInt(Conveyance_Allowance || '0') + parseInt(Special_Allowance || '0');
                $('#ContentPlaceHolder1_txtSub_Total_A').val(SubtotalA);
                var YearlySubtotalA = parseInt(SubtotalA * 12);
                $('#ContentPlaceHolder1_txtSub_Total_AY').val(YearlySubtotalA);
            });
            $('.CalculateSubTotal').on('change', function () {
                var Basic_Pay = $('#ContentPlaceHolder1_txtBasic_Pay').val();
                var Rental_Allowance = $('#ContentPlaceHolder1_txtHouse_Rental_Allowance').val();
                var Conveyance_Allowance = $('#ContentPlaceHolder1_txtConveyance_Allowance').val();
                var Special_Allowance = $('#ContentPlaceHolder1_txtSpecial_Allowance').val();
                var PF_ER = $('#ContentPlaceHolder1_txtPF_ER').val();
                var Subtotal = parseInt(Basic_Pay || '0') + parseInt(Rental_Allowance || '0') + parseInt(Conveyance_Allowance || '0') + parseInt(Special_Allowance || '0') + parseInt(PF_ER || '0');
                $('#ContentPlaceHolder1_txtTotal_Cost_to_Company').val(Subtotal);
                var YearlySubtotal = parseInt(Subtotal * 12);
                $('#ContentPlaceHolder1_txtTotal_Cost_to_CompanyY').val(YearlySubtotal);
            });
            $('.CalculateDedution').on('change', function () {
                
                var PF_Emp = $('#ContentPlaceHolder1_txtPF').val();
                var PT = $('#ContentPlaceHolder1_txtPT').val();
                var Deductions = parseInt(PF_Emp || '0') + parseInt(PT || '0');
                $('#ContentPlaceHolder1_txtTotal_Deductions_B').val(Deductions);
                var YearlyDeductions = parseInt(Deductions * 12);
                $('#ContentPlaceHolder1_txtTotal_Deductions_BY').val(YearlyDeductions);
            });

            $('.CalculateNetPayment').click('change', function () {
                var SubtotalA = $('#ContentPlaceHolder1_txtTotal_Cost_to_CompanyY').val();
               
                var Deductions_B = $('#ContentPlaceHolder1_txtTotal_Deductions_BY').val();
              
                //var Basic_Pay = $('#ContentPlaceHolder1_txtBasic_Pay').val();
                //var Rental_Allowance = $('#ContentPlaceHolder1_txtHouse_Rental_Allowance').val();
                //var Conveyance_Allowance = $('#ContentPlaceHolder1_txtConveyance_Allowance').val();
                //var Special_Allowance = $('#ContentPlaceHolder1_txtSpecial_Allowance').val();
                //var PF_ER = $('#ContentPlaceHolder1_txtPF_ER').val();
                //var Subtotal = parseInt(Basic_Pay || '0') + parseInt(Rental_Allowance || '0') + parseInt(Conveyance_Allowance || '0') + parseInt(Special_Allowance || '0') + parseInt(PF_ER || '0');
                //var PF_Emp = $('#ContentPlaceHolder1_txtPF').val();
                //var PT = $('#ContentPlaceHolder1_txtPT').val();
                //var Deductions = parseInt(PF_Emp || '0') + parseInt(PT || '0');
                var netpay = parseInt(SubtotalA || '0') - parseInt(Deductions_B || '0');
                $('#ContentPlaceHolder1_txtNET_PAYMENT').val(netpay);
                var Yearlynetpay = parseInt(netpay * 12);
                $('#ContentPlaceHolder1_txtNET_PAYMENTY').val(Yearlynetpay);



            });

        });


        </script>

    <style>
        .center {
            margin-left: auto;
            margin-right: auto;
        }
    </style>
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Staff Salaries
            </h3>

        </div>
        
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

             <div class="row">
                  <div class="col-md-2">Full Name</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtFull_Name" autocomplete= "off" CssClass="form-control" TabIndex="7"></asp:TextBox>
                </div>
                <div class="col-md-2">Employee ID</div>
                <div class="col-md-4">
                  <asp:TextBox runat="server" ID="txtEmployeeID" autocomplete= "off" CssClass="form-control" TabIndex="7"></asp:TextBox>
                      <asp:HiddenField ID="hdfEmployee_ContractID" value="0" runat="server" />
                  </div>
            </div>
            
            
            
 
             <div class="row">
                  <div class="col-md-2">Designation</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDesignation"  autocomplete= "off" CssClass="form-control" TabIndex="9"></asp:TextBox>
                </div>
                 <div class="col-md-2">Actual Date of Joining</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtActual_Date_of_Joining" autocomplete= "off" CssClass="form-control" onkeypress="javascript:return false;" onpaste="javascript:return false;" TabIndex="8"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd-MM-yyyy" TargetControlID="txtActual_Date_of_Joining"></ajaxToolkit:CalendarExtender>
                </div>
                
               
            </div>
               <div class="row"><div class="col-md-2">Location</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtLocation"  autocomplete= "off" CssClass="form-control" TabIndex="9"></asp:TextBox>
                </div>
                   <div class="col-md-2">Status</div>
                <div class="col-md-4">
                    <asp:RadioButtonList ID="rblStatus" RepeatDirection="Horizontal" runat="server" TabIndex="12">
                        <asp:ListItem Text="Active" Selected="True" Value="Active"></asp:ListItem>
                         <asp:ListItem Text="Terminated" Value="Terminated"></asp:ListItem>
                        <asp:ListItem Text="Relived" Value="Relived"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                   <div class="row">
                       <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtFatherName" autocomplete= "off" CssClass="form-control" TabIndex="7" Visible="false"></asp:TextBox>
                </div>
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtEmployee_Permanent_Address" autocomplete= "off" CssClass="form-control" TabIndex="7" Visible="false"></asp:TextBox>
                </div>
            </div>

            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtEmail_ID" autocomplete= "off" CssClass="form-control" TabIndex="7" Visible="false"></asp:TextBox>
                </div>
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtQualification" autocomplete= "off" CssClass="form-control" TabIndex="7" Visible="false"></asp:TextBox>
                </div>
            </div>
                   
                
                
            </div>

            <div class="row">
               <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtReporting_Manager"  autocomplete= "off" CssClass="form-control" TabIndex="9" Visible="false"></asp:TextBox>
                </div>
                <div class="col-md-2"></div>
                <div class="col-md-4">
                     <asp:DropDownList ID="ddlProjects" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                    <asp:TextBox runat="server" ID="txtProject" autocomplete= "off" CssClass="form-control" TabIndex="8" Visible="false"></asp:TextBox>
                </div>
                
            </div>
                       <div class="row">
                <div class="col-md-2"></div>
                    <div class="col-md-4">
                       <asp:dropdownlist runat="server" id="ddlGender" CssClass="form-control" TabIndex="11" Visible="false"> 
                          <asp:ListItem>-Select-</asp:ListItem>
                        <asp:listitem  text="Male" value="Male"></asp:listitem>
                        <asp:listitem text="Female" value="Female"></asp:listitem>
                     </asp:dropdownlist>
                </div>
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtEmployee_Mobile_No" Visible="false" MaxLength="10" autocomplete= "off" CssClass="form-control input-pos-int" TabIndex="9"></asp:TextBox>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-2"></div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" ID="txtDate_of_Birth" autocomplete= "off" CssClass="form-control" onkeypress="javascript:return false;" onpaste="javascript:return false;" TabIndex="8" Visible="false"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtDate_of_Birth"></ajaxToolkit:CalendarExtender>
                </div>
                <div class="col-md-2"></div>
                <div class="col-md-4">
                     <asp:dropdownlist runat="server" id="ddlEmployment_Type" Visible="false" CssClass="form-control" TabIndex="11"> 
                          <asp:ListItem>-Select-</asp:ListItem>
                        <asp:listitem  text="Permanent" value="Permanent"></asp:listitem>
                        <asp:listitem text="On Contract" value="On Contract"></asp:listitem>
                         <asp:listitem text="Temprory" value="Temprory"></asp:listitem>
                     </asp:dropdownlist>
                </div>

            </div>
             </div>


          

            <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Salary Component
            </h3>
        </div>
                <br />
                <br />
                 <table class="Paytable, center">
               <thead>
                   <tr>
                       <td style="font-size:14px"> <b>Salary Component</b></td>
                       <td style="  text-align:center;font-size:14px"> <b>Monthly</b></td>
                       <%--<td style="  text-align:center;font-size:14px" visible="false"><b>Yearly</b></td>--%>
                   </tr>
               </thead>
                <tbody>
                    <tr>
                        <td>Basic Pay</td>
                            <td class="col-md-4"><asp:TextBox runat="server" ID="txtBasic_Pay" autocomplete= "off" CssClass="form-control CalculateSubTotal CalculateNetPayment" TabIndex="8"></asp:TextBox></td >
                            <td class="col-md-4"><asp:TextBox runat="server" ID="txtBasic_PayY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td>House Rental Allowance</td>
                         <td class="col-md-4"><asp:TextBox runat="server" ID="txtHouse_Rental_Allowance" autocomplete= "off" CssClass="form-control CalculateSubTotal CalculateNetPayment" TabIndex="8"></asp:TextBox></td >
                         <td class="col-md-4"><asp:TextBox runat="server" ID="txtHouse_Rental_AllowanceY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox></td >
                    </tr>
                     <tr>
                        <td>Conveyance Allowance</td>
                       <td class="col-md-4"><asp:TextBox runat="server" ID="txtConveyance_Allowance" autocomplete= "off" CssClass="form-control CalculateSubTotal CalculateNetPayment" TabIndex="8"></asp:TextBox></td >
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtConveyance_AllowanceY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox></td >
                    </tr>
                    <tr>
                        <td>Special Allowance</td>
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtSpecial_Allowance" autocomplete= "off" CssClass="form-control CalculateSubTotal CalculateNetPayment" TabIndex="8"></asp:TextBox></td >
                         <td class="col-md-4"><asp:TextBox runat="server" ID="txtSpecial_AllowanceY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox></td >
                        
                    </tr>
                    <tr>
                        <td style="font-size:14px"><b>Sub Total (A)</b></td>
                       <td class="col-md-4"><asp:TextBox runat="server" ID="txtSub_Total_A" autocomplete= "off" CssClass="form-control" TabIndex="8"></asp:TextBox></td >
                         <td class="col-md-4"><asp:TextBox runat="server" ID="txtSub_Total_AY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox></td >

                    </tr>
                    <tr>
                        <td>Provident Fund Employer</td>
                         <td class="col-md-4"><asp:TextBox runat="server" ID="txtPF_ER" autocomplete= "off" CssClass="form-control CalculateSubTotal" TabIndex="8"></asp:TextBox></td >
                         <td class="col-md-4"><asp:TextBox runat="server" ID="txtPF_ERY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox></td >
                    </tr>
                    <tr>
                        <td style="font-size:14px"><b>Total Cost to the Company</b></td>
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtTotal_Cost_to_Company" autocomplete= "off" CssClass="form-control" TabIndex="8"></asp:TextBox></td >
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtTotal_Cost_to_CompanyY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox></td >
                    </tr>
                    <tr>
                        <td style="font-size:14px"><b>Deductions</b></td>
                        
                    </tr>
                    <tr>
                        <td>Provident Fund Employee</td>
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtPF" autocomplete= "off" CssClass="form-control CalculateDedution " TabIndex="8"></asp:TextBox></td >
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtPFY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox ></td >
                    </tr>
                    <tr>
                        <td>Professional Tax</td>
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtPT" autocomplete= "off" CssClass="form-control CalculateDedution" TabIndex="8"></asp:TextBox></td >
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtPTY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox></td >
                    </tr>
                    <tr>
                        <td style="font-size:14px"><b>Total Deductions (B)</b></td>
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtTotal_Deductions_B" autocomplete= "off" CssClass="form-control CalculateNetPayment" TabIndex="8"></asp:TextBox></td >
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtTotal_Deductions_BY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox></td >

                    </tr>
                    <tr>
                        <td style="font-size:14px"><b>Take Home (C = A- B)</b></td>
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtNET_PAYMENT" autocomplete= "off" CssClass="form-control" TabIndex="8"></asp:TextBox></td >
                        <td class="col-md-4"><asp:TextBox runat="server" ID="txtNET_PAYMENTY" autocomplete= "off" CssClass="form-control" TabIndex="8" visible="false"></asp:TextBox></td >
                    </tr>
                     
                </tbody>
           </table>
                 <%--<div class="panel-heading" visible="false">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Upload Documents
            </h3>
                      </div>--%>
                     <div class="row">
                   <div class="col-md-2"><%--Resume--%></div>
                <div class="col-md-4">
                     <asp:FileUpload runat="server" ID="fupResume" visible="false"></asp:FileUpload>
                </div>
                      <div class="col-md-2"><%--ID Proof--%></div>
                <div class="col-md-4">
                    <asp:FileUpload runat="server" ID="fupIDProof" visible="false"></asp:FileUpload>
                </div>
        </div>
                      <div class="row">
                   <div class="col-md-2"><%--Bank Details--%></div>
                <div class="col-md-4">
                      <asp:FileUpload runat="server" ID="fupBankDetails" visible="false"></asp:FileUpload>
                </div>
                      <div class="col-md-2"><%--Qualification Certificates--%></div>
                <div class="col-md-4">
                      <asp:FileUpload runat="server" ID="fupQualificationCertificates" visible="false"></asp:FileUpload>
                </div>
        </div>
                      <div class="row">
                   <div class="col-md-2"><%--Pay Slips--%></div>
                <div class="col-md-4">
                      <asp:FileUpload runat="server" ID="fupPaySlips" visible="false"></asp:FileUpload>
                </div>
                      <div class="col-md-2"><%--Other Documents--%></div>
                <div class="col-md-4">
                      <asp:FileUpload runat="server" ID="fupOtherDocuments" visible="false"></asp:FileUpload>
                    <label runat="server"  id="fupOtherDocumentsBind" visible="false"></label>
                </div>

            <br />
            
            <div class="row">
                <div class="col-md-12 text-center">
                     <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="ValWO" OnClick="btnSubmit_Click" CssClass="btn btn-default" TabIndex="15"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-default" TabIndex="16"></asp:Button>

                    <a runat="server" id="btnPrint" visible="false" class="btn btn-default" tabindex="18">Print </a>
                </div>
            </div>
            <br />
            
            <!---------------------------------------------------------------/Body Content-------------------------------------------------------------------->
        </div>

    </div>
</asp:Content>
