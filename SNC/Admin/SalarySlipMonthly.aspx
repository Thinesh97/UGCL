<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="SalarySlipMonthly.aspx.cs" Inherits="SNC.Admin.SalarySlipMonthly" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript"> 
        function exportgrid() {
            Grid_EC.exportToExcel();
        }

        function beforedelete() {
            if (confirm("This record will be deleted. Do you want to proceed?") == false) {
                //alert('Check');
                document.getElementById('<%=HF_Confirm.ClientID%>').value = "false";
                //alert(document.getElementById('<%=HF_Confirm.ClientID%>').value);
                return false;
            }
            else {
                document.getElementById('<%=HF_Confirm.ClientID%>').value = "true";
                return true;
            }
            
        }
    </script>

       <div class="row">
                  <div class="col-md-2">Select Year</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlyear" CssClass="form-control" TabIndex="2">
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
                 
                  <asp:DropDownList runat="server" ID="ddlSalarymonth" CssClass="form-control" AutoPostBack="true" TabIndex="2" OnSelectedIndexChanged="ddlSalarymonth_SelectedIndexChanged">
                  
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
    <div id="dvsalary" runat="server" visible="false">
            <div class="row">
                <%--<ogrid:Grid runat="server" ID="Gridmonthsalary" CallbackMode="false" AutoGenerateColumns="false" 
                    OnRowDataBound="Grid_monthsalary_RowDataBound" 
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"/>
                         <%--ColumnsToExport="EmployeeID,Full_Name,Actual_Date_of_Joining,Designation,Location,Status" />--%>
                    <%--<CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="EmployeeID" HeaderText="Employee ID" Width="190px" >
                             <TemplateSettings  TemplateId="ECNoTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Full_Name" HeaderText="Full Name" Wrap="true"   Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Actual_Date_of_Joining" HeaderText="Actual Date of Joining" Align="center" Width="150px"></ogrid:Column>                 
                        <ogrid:Column DataField="Designation" HeaderText="Designation" Wrap="true" ></ogrid:Column> 
                        <ogrid:Column DataField="Location" HeaderText="Location" Wrap="true" ></ogrid:Column>         
                        <%--<ogrid:Column DataField="Status" HeaderText="Status" Width="100px" ></ogrid:Column>--%> 
                        <%--<ogrid:Column DataField="Basic_Pay" HeaderText="Basic Pay" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="HRA" HeaderText="House Rental Allowance" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Conveyance_Allowance" HeaderText="Conveyance Allowance" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Special_Allowance" HeaderText="Special Allowance" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Sub_Total_A" HeaderText="Sub Total(A)" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="PF_ER" HeaderText="Provident Fund Employer" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Total_Cost_to_Company" HeaderText="Total Cost to the Company" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="PF" HeaderText="Provident Fund Employee" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="PT" HeaderText="Professional Tax" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="Total_Deductions_B" HeaderText="Total Deductions(B)" Wrap="true" ></ogrid:Column>
                        <ogrid:Column DataField="NET_PAYMENT" HeaderText="Take Home (C = A- B)" Wrap="true" ></ogrid:Column>
                        
                        
                        <ogrid:Column DataField="" HeaderText="Download" >
                            <TemplateSettings  TemplateId="GT_EC_Download"/>
                            </ogrid:Column>
                    </Columns>
                 
                    <Templates>
                        <ogrid:GridTemplate ID="ECNoTemplate" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnkSSNo" runat="server" CssClass="Grid_EC"  Text='<%#Container.DataItem["EmployeeID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                         <ogrid:GridTemplate ID="GT_EC_Delete" runat="server">
                        <%--       <ItemTemp late>
                <asp:CheckBox ID="chkRow" runat="server" />
            </ItemTemp>--%>
                            <%-- <Template>
                               <asp:CheckBox ID="chkRow" runat="server" />
                            </Template>
                        </ogrid:GridTemplate>

                        <ogrid:GridTemplate ID="GT_EC_Download" runat="server">
                            <Template>
                               <asp:LinkButton ID="lnkDownload" Text="Download"    CommandArgument='<%# Container.PageRecordIndex %>' runat="server" OnClick="DownloadFile" ></asp:LinkButton>
                                
                                    </ItemTemplate>  
                            </Template>
                        </ogrid:GridTemplate>

                    </Templates>
                </ogrid:Grid>--%>
                <ogrid:Grid runat="server" ID="Gridmonthsalary" CallbackMode="false" AutoGenerateColumns="false" OnUpdateCommand="Gridmonthsalary_UpdateCommand" FolderStyle="../Gridstyles/grand_gray" 
                    AllowAddingRecords="false" AllowFiltering="true" AllowRecordSelection="true" AllowGrouping="true" AllowPaging="true" PageSize="10"  OnRowDataBound="Grid_monthsalary_RowDataBound"  >
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New" />
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  
                        CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
                    <ScrollingSettings ScrollWidth="97%" ScrollHeight="400" />

                     <Columns>
                         <ogrid:Column HeaderText="Check" Width="60px" >
                            <TemplateSettings TemplateId="SelectTemplate"/>
                        </ogrid:Column>
                         

                         <ogrid:Column DataField="Staff_SalaryID" HeaderText="Staff_SalaryID" ReadOnly="true" Visible="false"  runat="server" Width="150px" >
                            
                            </ogrid:Column>

                        <ogrid:Column DataField="EmployeeID" HeaderText="Employee ID" ReadOnly="true" runat="server" Width="150px" >
                            <TemplateSettings  TemplateId="ECNoTemplate"/>
                            </ogrid:Column>
                        <ogrid:Column DataField="Full_Name" HeaderText="Full Name" Width="120px"  ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Designation" HeaderText="Designation" Wrap="true" ReadOnly="true"></ogrid:Column>
                        <ogrid:Column DataField="Assinged_to_Project" HeaderText="Assinged to Project" Width="100px" >
                            <TemplateSettings TemplateId="EditATPTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Basic_Pay" HeaderText="Basic Pay" Width="110px" >
                            <TemplateSettings EditTemplateId="EditBasic_PayTemplate" />
                        </ogrid:Column>
                         
                        <ogrid:Column DataField="HRA" HeaderText="House Rental Allowance" Width="110px">
                            <TemplateSettings EditTemplateId="EditHRATemplate" />
                        </ogrid:Column>
                          <ogrid:Column DataField="Conveyance_Allowance" HeaderText="Conveyance Allowance"  Width="110px" runat="server">  
                            <TemplateSettings EditTemplateId="EditCATemplate" />
                        </ogrid:Column>
                         <%--<ogrid:Column HeaderText="Generate Partial Payment" Width="80px" >
                            <TemplateSettings TemplateId="SelectTemplatePAYIND"/>
                        </ogrid:Column>--%>
                        <ogrid:Column DataField="Special_Allowance" HeaderText="Special Allowance"  Width="100px" runat="server">
                            <TemplateSettings EditTemplateId="EditSpAlTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="Sub_Total_A" HeaderText="Sub Total (A)" ReadOnly="true" Width="100px" runat="server">
                             <TemplateSettings EditTemplateId="EditSub_Total_ATemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="PF_ER" HeaderText="Provident Fund Employer"  Width="100px" runat="server">
                           <TemplateSettings EditTemplateId="EditPF_ERTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="CTC" HeaderText="CTC" Wrap="true" Width="100px" runat="server">
                            <TemplateSettings EditTemplateId="EditCTCTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="PF" HeaderText="Provident Fund Employee" Wrap="true" >
                           
                        </ogrid:Column>
                        <ogrid:Column DataField="PT" HeaderText="Professional Tax" Wrap="true">
                            <TemplateSettings EditTemplateId="EditPTTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="NET_PAYMENT" HeaderText="NET Salary" ReadOnly="true">
                             <TemplateSettings EditTemplateId="EditNET_SalaryTemplate" />
                        </ogrid:Column>
                        
                         

                         <ogrid:Column DataField="No_of_Days_Present" HeaderText="No. of days Present" Width="140px" >
                            <TemplateSettings EditTemplateId="EditNoofdaypresentemplate" />
                        </ogrid:Column>
                         <ogrid:Column DataField="other_deduction" HeaderText="Other Deduction"  Width="150px" Wrap="true" >
                           <TemplateSettings EditTemplateId="EditODedTemplate" />
                        </ogrid:Column>
                           <ogrid:Column DataField="LOP_Amount" HeaderText="LOP Amount" Width="100px" Wrap="true"  >
                            <TemplateSettings EditTemplateId="EditLOPamtTemplate" />
                        </ogrid:Column>
                        <ogrid:Column DataField="LOP_Days" HeaderText="LOP Days" Width="100px" Wrap="true"  >
                            <TemplateSettings EditTemplateId="EditLopdaysTemplate" />
                        </ogrid:Column>
                       
                        

                        
                        <%--<ogrid:Column DataField="Other_Deductions" HeaderText="Other Deductions"  Width="280px" Wrap="true" >
                            <TemplateSettings EditTemplateId="EditODTemplate" />
                        </ogrid:Column>--%>
                            <ogrid:Column AllowEdit="true" Width="90px"></ogrid:Column>
                         <ogrid:Column DataField="Net_Payable" HeaderText="Net Payable" ReadOnly="true"></ogrid:Column>
                         
                     
                        
                        
                          
                    </Columns>
                    <Templates>
                         <ogrid:GridTemplate ID="SelectTemplate" runat="server">
                            <Template>
                                <asp:CheckBox runat="server" ID="chkSelect" />
                                <%--<asp:HiddenField ID="hdn_payIndNo" runat="server" Value='<%# Container.DataItem["PayInd_No"] %>'/>--%>
                                <asp:HiddenField ID="hdn_payIndNo" runat="server" Value='<%# Container.DataItem["EmployeeID"] %>'/>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                    <Templates>
                        <ogrid:GridTemplate runat="server" ID="EditBasic_PayTemplate" ControlID="txtBasicPayAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtBasicPayAmt" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                    <Templates>
                        <ogrid:GridTemplate runat="server" ID="EditHRATemplate" ControlID="txtHRAAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtHRAAmt" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                    <Templates>
                          <ogrid:GridTemplate runat="server" ID="EditCATemplate" ControlID="txtCAAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtCAAmt" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                    <Templates>
                        <ogrid:GridTemplate runat="server" ID="EditSpAlTemplate" ControlID="txtSPallAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtSPallAmt" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate> 
                    </Templates>
                    
                     <Templates>
                        <ogrid:GridTemplate runat="server" ID="EditCTCTemplate" ControlID="TxtCTC" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="TxtCTC" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate> 
                    </Templates>
                    <Templates>
                        
                         <ogrid:GridTemplate runat="server" ID="EditSub_Total_ATemplate" ControlID="TxtSub_Total" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="TxtSub_Total" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate> 
                    </Templates>
                    <Templates>
                        <ogrid:GridTemplate runat="server" ID="EditNET_SalaryTemplate" ControlID="TxtNET_Salary" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="TxtNET_Salary" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server" ReadOnly="true"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate> 
                    </Templates>
                  
                    
                    <Templates>
                         <ogrid:GridTemplate runat="server" ID="EditPF_ERTemplate" ControlID="txtPF_ERAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPF_ERAmt" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                    <Templates>
                        <ogrid:GridTemplate runat="server" ID="EditPTTemplate" ControlID="txtPTTAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPTTAmt" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                    <Templates>
                         <ogrid:GridTemplate runat="server" ID="EditNoofdaypresentemplate" ControlID="txtNODP" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtNODP" Width="120px" runat="server" onkeypress="return allowOnlydecimalNumber(event)"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                      <Templates>
                          <ogrid:GridTemplate runat="server" ID="EditLOPamtTemplate" ControlID="TxtLOPAmount" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="TxtLOPAmount" Width="120px" runat="server" onkeypress="return allowOnlydecimalNumber(event)"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate> 
                    </Templates>
                    <Templates>
                          <ogrid:GridTemplate runat="server" ID="EditLopdaysTemplate" ControlID="txtLopDays" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtLopdays" Width="100px" runat="server" onkeypress="return allowOnlydecimalNumber(event)"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate> 
                    </Templates>
                    <Templates>
                         <ogrid:GridTemplate runat="server" ID="EditODedTemplate" ControlID="txtODed" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtODed" Width="100px" runat="server" onkeypress="return allowOnlydecimalNumber(event)"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>
                  
                    <%--<Templates>
                          
                        
                        
                      
                         

                       
                        <ogrid:GridTemplate runat="server" ID="EditPFEETemplate" ControlID="txtPF_EEAmt" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txtPF_EEAmt" onkeypress="return allowOnlydecimalNumber(event)" Width="100px" runat="server"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate> 
                        
                       
                        

                      
                        
                        
                        <ogrid:GridTemplate runat="server" ID="EditATPTemplate" ControlID="txATPT" ControlPropertyName="value">
                            <Template>
                                <asp:TextBox ID="txATPT" Width="120px" runat="server" onkeypress="return allowOnlydecimalNumber(event)"></asp:TextBox>
                            </Template>
                        </ogrid:GridTemplate>
                       

                         

                        

                      
                       
                        <ogrid:GridTemplate ID="ECNoTemplate" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnkSSNo" runat="server" CssClass="Grid_EC"  Text='<%#Container.DataItem["EmployeeID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                          <ogrid:GridTemplate ID="PayIndNoTemplate_Completed" runat="server">
                            <Template>
                                <asp:HyperLink ID="lnkPayIndNo" runat="server" CssClass="gridCB"  Text='<%#Container.DataItem["EmployeeID"] %>'></asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                    </Templates>--%>


                </ogrid:Grid>
                <br />
                
                </div>
        <br />
        <div class="row">
                <div class="col-md-2">Sum of Net Payment </div>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtnetpay" Style="resize: none"  Width="250px" CssClass="form-control" ></asp:TextBox>

                </div>
            </div>
        <br /><br /><br /><br />
        <asp:HiddenField runat="server" Value="" Id="HF_Confirm"/>
       <asp:Button ID="btnupdate" runat="server" OnClick="btnupdate_Click" Text="Update"  CssClass="btn btn-default" TabIndex="1"></asp:Button>
       <asp:Button ID="Btngenbulk" runat="server" OnClick="Btngenbulk_Click" Text="Generate Bulk Payment Indent"  CssClass="btn btn-default" TabIndex="2"></asp:Button>
        <br /><br /><div> Processed Grid List</div>
         <ogrid:Grid runat="server" ID="Gridprocesed" CallbackMode="false" AutoGenerateColumns="false"  
                    OnRowDataBound="Grid_monthsalary_RowDataBound" 
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"/>
                         <%--ColumnsToExport="EmployeeID,Full_Name,Actual_Date_of_Joining,Designation,Location,Status" />--%>
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>
                        <ogrid:Column DataField="EmployeeID" HeaderText="Salary ID" Width="190px" > 
                        </ogrid:Column>
                        <ogrid:Column DataField="Full_Name" HeaderText="Month " Wrap="true"   Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Actual_Date_of_Joining" HeaderText="Year" Align="center" Width="150px"></ogrid:Column>                 
                        <ogrid:Column DataField="Designation" HeaderText="Total Amount" Wrap="true" ></ogrid:Column> 
                        <ogrid:Column DataField="Location" HeaderText="Payment Indet Status" Wrap="true" ></ogrid:Column>         
                    </Columns>
                    
                </ogrid:Grid>
    </div>

</asp:Content>
