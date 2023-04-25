<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="StaffMonthlySalaryList.aspx.cs" Inherits="SNC.Admin.StaffMonthlySalaryList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
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


        $(document).ready(function () {

            var Basic_Pay = $('#ctl00$ContentPlaceHolder1$Grid_PaymentIndent_Completed$EditTransAmtTemplate$ctl00$txtTransAmt').val();
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
            $('.CalculateTotalDeductions').on('change', function () {
                var PF_Emp = $('#ContentPlaceHolder1_txtPF').val();
                var PT = $('#ContentPlaceHolder1_txtPT').val();
                var Deductions = parseInt(PF_Emp || '0') + parseInt(PT || '0');
                $('#ContentPlaceHolder1_txtTotal_Deductions_B').val(Deductions);
                var YearlyDeductions = parseInt(Deductions * 12);
                $('#ContentPlaceHolder1_txtTotal_Deductions_BY').val(YearlyDeductions);
            });

            $('#ContentPlaceHolder1_txtNET_PAYMENT').click('change', function () {
                var SubtotalA = $('#ContentPlaceHolder1_txtSub_Total_A').val();
                var Deductions_B = $('#ContentPlaceHolder1_txtTotal_Deductions_B').val();
                var netpay = parseInt(SubtotalA || '0') - parseInt(Deductions_B || '0');
                $('#ContentPlaceHolder1_txtNET_PAYMENT').val(netpay);
                var Yearlynetpay = parseInt(netpay * 12);
                $('#ContentPlaceHolder1_txtNET_PAYMENTY').val(Yearlynetpay);

            });

        });


    </script>
        <div class="panel panel-default">
      <div class="panel-heading">
            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Payment Indent List

            </h3>
        </div>
    <div class="panel-body">
        
        
            <center>
                 <h4>Completed Payment Indent List</h4>
                
                  <div class="panel-body">
                      <div>
                          <div class="row">
                              <div class="col-md-12">
                                  <div class="col-md-2">Select Year</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlyear" CssClass="form-control" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
                        <%--<asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="2010 "></asp:ListItem>
                        <asp:ListItem Value="2" Text="2011"></asp:ListItem>
                        <asp:ListItem Value="3" Text="2012"></asp:ListItem>
                        <asp:ListItem Value="4" Text="2013"></asp:ListItem>
                        <asp:ListItem Value="5" Text="2014"></asp:ListItem>
                        <asp:ListItem Value="6" Text="2015"></asp:ListItem>
                        <asp:ListItem Value="7" Text="2016"></asp:ListItem>
                        <asp:ListItem Value="8" Text="2017"></asp:ListItem>
                        <asp:ListItem Value="9" Text="2018"></asp:ListItem>
                        <asp:ListItem Value="10" Text="2019"></asp:ListItem>
                        <asp:ListItem Value="11" Text="2020"></asp:ListItem>
                        <asp:ListItem Value="12" Text="December"></asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
              
               
                <div class="col-md-2">select Month</div>
                <div class="col-md-4">
                 
                  <asp:DropDownList runat="server" ID="ddlSalarymonth" CssClass="form-control" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlSalarymonth_SelectedIndexChanged" >
                  
                        <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1" Text="January "></asp:ListItem>
                        <asp:ListItem Value="2" Text="February"></asp:ListItem>
                        <asp:ListItem Value="3" Text="March"></asp:ListItem>
                        <asp:ListItem Value="4" Text="April"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="june"></asp:ListItem>
                        <asp:ListItem Value="7" Text="July"></asp:ListItem>
                        <asp:ListItem Value="8" Text="August"></asp:ListItem>
                        <asp:ListItem Value="9" Text="September"></asp:ListItem>
                        <asp:ListItem Value="10" Text="october"></asp:ListItem>
                        <asp:ListItem Value="11" Text="November"></asp:ListItem>
                        <asp:ListItem Value="12" Text="December"></asp:ListItem>
                    </asp:DropDownList>
                      
                  </div>
                              </div>
                  
            </div>
                      </div>
                 
                      <div>
                          
                              <div id="DvMonthlySalary" runat="server" visible="false">
                                  <ogrid:Grid runat="server" ID="Grid_PaymentIndent_Completed" CallbackMode="false" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowRecordSelection="true" AllowGrouping="true" AllowPaging="true" PageSize="10" OnUpdateCommand="Grid_PaymentIndent_Completed_UpdateCommand"  OnRowDataBound="Grid_PaymentIndent_RowDataBound_Completed"  OnDeleteCommand="Grid_PaymentIndent_DeleteCommand">
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

                     <Columns>
                        <ogrid:Column DataField="Staff_SalaryID" HeaderText="Staff_SalaryID" ReadOnly="true" runat="server" Width="150px" Visible="false" >
                            <TemplateSettings  TemplateId="PayIndNoTemplate_Completed"/>
                            </ogrid:Column>
                         <ogrid:Column DataField="EmployeeID" HeaderText="EmployeeID" Wrap="true" ReadOnly="true"></ogrid:Column>
                           <ogrid:Column DataField="Designation" HeaderText="Designation" Wrap="true" ReadOnly="true"></ogrid:Column>
                           <ogrid:Column DataField="Actual_Date_of_Joining" HeaderText="Actual_Date_of_Joining" Wrap="true" ReadOnly="true"></ogrid:Column>
                           <ogrid:Column DataField="Location" HeaderText="Location" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Full_Name" HeaderText="Employee Name" Wrap="true" ReadOnly="true"></ogrid:Column>
                         <ogrid:Column DataField="Total_Cost_to_Company" HeaderText="Total_Cost_to_Company" Wrap="true" ReadOnly="true"></ogrid:Column>
                          <ogrid:Column DataField="Assinged_to_Project" HeaderText="Assinged_to_Project" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Status" HeaderText="Status" Width="100px" ReadOnly="true"></ogrid:Column>
                        
                        <ogrid:Column DataField="Basic_Pay" HeaderText="Basic_Pay" Width="110px" runat="server">
                            <TemplateSettings EditTemplateId="EditTransAmtTemplate" />
                        </ogrid:Column>
                          <ogrid:Column DataField="HRA" HeaderText="HRA"  Width="110px" runat="server">  
                               <TemplateSettings EditTemplateId="EditTransAmtTemplateHRA" />
                        </ogrid:Column>
                   
                    
                        <ogrid:Column DataField="Conveyance_Allowance" HeaderText="Conveyance_Allowance" Width="100px" runat="server">
                              <TemplateSettings EditTemplateId="EditTransAmtTemplateCA" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Special_Allowance" HeaderText="Special_Allowance" Width="100px" runat="server">
                           <TemplateSettings EditTemplateId="EditTransAmtTemplateSA" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Sub_Total_A" HeaderText="Sub Total" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="PF_ER" HeaderText="Provident Fund Employer" Wrap="true" >
                               <TemplateSettings EditTemplateId="EditTransAmtTemplatePF_ER" />
                        </ogrid:Column>
                        <ogrid:Column DataField="CTC" HeaderText="CTC" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="PF" HeaderText="Provident Fund Employee" >
                              <TemplateSettings EditTemplateId="EditTransAmtTemplatePF" />
                        </ogrid:Column>
                        <ogrid:Column DataField="PT" HeaderText="Professional Tax" Width="140px">
                             <TemplateSettings EditTemplateId="EditTransAmtTemplatePT" />
                        </ogrid:Column>
                      <ogrid:Column DataField="Net_Salary" HeaderText="NET Salary" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="No_of_Days_Present" HeaderText="No_of_Days_Present" Width="140px" >
                            <TemplateSettings EditTemplateId="EditPaymentRefNoTemplate" />
                        </ogrid:Column>
                        
                         <ogrid:Column DataField="LOP_Days" HeaderText="LOP_Days">
                              <TemplateSettings EditTemplateId="EditPaymentRefNoTemplateLOP" />
                         </ogrid:Column>
                          <ogrid:Column DataField="LOP_Amount" HeaderText="LOP_Amount" Width="140px">
                             <TemplateSettings EditTemplateId="EditTransAmtTemplateLOPAmount" />
                        </ogrid:Column>
                         <ogrid:Column DataField="Deductions" HeaderText="other_deduction" Width="140px">
                             <TemplateSettings EditTemplateId="EditTransAmtTemplateother_deduction" />
                        </ogrid:Column>
                           <ogrid:Column DataField="NET_PAYMENT" HeaderText="NET_PAYMENT" >
                                <TemplateSettings EditTemplateId="EditTransAmtTemplateNET_PAYMENT" />
                           </ogrid:Column>
                        <ogrid:Column AllowEdit="true" Width="90px"></ogrid:Column>
                       
                        <ogrid:Column HeaderText="Select" Width="60px" >
                            <TemplateSettings TemplateId="SelectTemplate"/>
                        </ogrid:Column>
                          <ogrid:Column DataField="" HeaderText="Pay Slip" Width="190px" >
                             <TemplateSettings  TemplateId="ECNoTemplate"/>
                        </ogrid:Column>
                         
                    </Columns>

                    <Templates>
                       
                        <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplate" ControlID="txtTransAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTransAmt" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplateHRA" ControlID="txtTransAmtHRA" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTransAmtHRA" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplateCA" ControlID="txtTransAmtCA" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTransAmtCA" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplateSA" ControlID="txtTransAmtSA" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTransAmtSA" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplatePF_ER" ControlID="txtTransAmtPF_ER" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTransAmtPF_ER" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                         <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplatePF" ControlID="txtTransAmtPF" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTransAmtPF" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplatePT" ControlID="txtTransAmtPT" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtTransAmtPT" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditPaymentRefNoTemplate" ControlID="txtPaymentRefNo" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPaymentRefNo" Width="150px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate runat="server" ID="EditPaymentRefNoTemplateLOP" ControlID="txtPaymentRefNoLOP" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPaymentRefNoLOP" Width="150px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                           <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplateLOPAmount" ControlID="txtPaymentRefNoLOPAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPaymentRefNoLOPAmt" Width="150px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                         <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplateother_deduction" ControlID="txtPaymentRefNodeduction" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPaymentRefNodeduction" Width="150px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>

                         <ogrid:GridTemplate runat="server" ID="EditTransAmtTemplateNET_PAYMENT" ControlID="TxtNET_PAYMENT">
                            <Template>
                             <%--    <asp:TextBox ID="TxtNET_PAYMENT" Width="150px" runat="server"  Value='<%# Container.DataItem["NET_PAYMENT"] %>'></asp:TextBox>--%>
                                <%--<asp:HiddenField ID="hdn_NET_PAYMENT" runat="server" Value='<%# Container.DataItem["NET_PAYMENT"] %>'/>--%>
                                 <asp:TextBox runat="server"  Value='<%# Container.DataItem["NET_PAYMENT"] %>' ID="TxtNET_PAYMENT" AutoComplete="Off"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>

                         <ogrid:GridTemplate ID="SelectTemplate" runat="server">
                            <Template>
                                <asp:CheckBox runat="server" ID="chkSelect" />
                                <asp:HiddenField ID="hdn_payIndNo" runat="server" Value='<%# Container.DataItem["Staff_SalaryID"] %>'/>
                            </Template>
                        </ogrid:GridTemplate>
                        
                          <ogrid:GridTemplate ID="PayIndNoTemplate_Completed" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["Staff_SalaryID"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                        <ogrid:GridTemplate ID="ECNoTemplate" runat="server">
                            <Template>
                               
                                    <asp:LinkButton ID="lnkDownload" runat="server"   CssClass="Grid_EC" OnClick="lnkDownload_Click" Text='<%#Container.DataItem["Staff_SalaryID"] %>'></asp:LinkButton>
                        
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>

                </ogrid:Grid>
                              </div>
                <br />
                <div id="DvButtons" runat="server" visible="false">
                                  <asp:Button runat="server" id="lnkUpdate" class="btn btn-default" Text="Update" OnClick="lnkUpdate_Click" />
                <asp:Button runat="server" id="BtnGenerateBulkPaymentIndent" class="btn btn-default" Text="Generate Bulk Payment Indent" OnClick="BtnGenerateBulkPaymentIndent_Click"/>
                    
                             </div>
                          <br />
                          <div id="DvStatus" runat="server" visible="false">
                <ogrid:Grid runat="server" ID="GvStatus" CallbackMode="false" AutoGenerateColumns="false" FolderStyle="../Gridstyles/grand_gray" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowRecordSelection="true" AllowGrouping="true" AllowPaging="true" PageSize="10">
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

                     <Columns>
                         <ogrid:Column DataField="SalaryID" HeaderText="Salary ID" Wrap="true" ReadOnly="true" Visible="false"></ogrid:Column>
                         <ogrid:Column DataField="Salary_Id_number" HeaderText="Salary Id" Wrap="true" ReadOnly="true"></ogrid:Column>
                          <ogrid:Column DataField="Month" HeaderText="Month" Wrap="true" ReadOnly="true"></ogrid:Column>
                         
                           <ogrid:Column DataField="Year" HeaderText="Year" Wrap="true" ReadOnly="true"></ogrid:Column>
                           <ogrid:Column DataField="Total_Amount" HeaderText="Total Amount" Wrap="true" ReadOnly="true"></ogrid:Column>
                           <ogrid:Column DataField="payment_indent_status" HeaderText="Payment Indet Status" Wrap="true" ReadOnly="true"></ogrid:Column>
                         </Columns>
                 </ogrid:Grid>
            </div>
                      </div>
            <div>
                             
                  
                    
            </div>
        </div>
            </div>
            
            
             
            </div>
</asp:Content>
