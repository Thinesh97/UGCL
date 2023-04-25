<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Admin/StaffSalariesList.aspx.cs" Inherits="StaffSalariesList" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <script type="text/javascript">
        function exportgrid() {
            Grid_EC.exportToExcel();
        }

        function exportgridWOItems() {
            Gv_ECItemsList.exportToExcel();
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

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

               Staff salaries Master

            </h3>

        </div>
        <div class="panel-body">
            <center>
                <ogrid:Grid runat="server" ID="Grid_SS" CallbackMode="false" AutoGenerateColumns="false" 
                    OnRowDataBound="Grid_SS_RowDataBound" 
                    FolderStyle="../Gridstyles/grand_gray" AllowFiltering="true" AllowAddingRecords="false" AllowPaging="true" PageSize="10" >
                    <ScrollingSettings ScrollWidth="100%"  ScrollHeight="400"/>
                      <ClientSideEvents OnBeforeClientDelete="beforedelete" />
                    <ExportingSettings ExportAllPages="true" ExportTemplates="true" ExportedFilesTargetWindow="New"/>
                         <%--ColumnsToExport="EmployeeID,Full_Name,Actual_Date_of_Joining,Designation,Location,Status" />--%>
                    <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    

                    <Columns>

                          <ogrid:Column DataField="Staff_SalaryID" HeaderText="Staff_SalaryID" Wrap="true" Visible="false"  Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="EmployeeID" HeaderText="Employee ID" Width="190px" >
                             <TemplateSettings  TemplateId="ECNoTemplate"/>
                        </ogrid:Column>
                        <ogrid:Column DataField="Full_Name" HeaderText="Full Name" Wrap="true"   Width="150px"></ogrid:Column>
                        <ogrid:Column DataField="Actual_Date_of_Joining" HeaderText="Actual Date of Joining" Align="center" Width="150px"></ogrid:Column>                 
                        <ogrid:Column DataField="Designation" HeaderText="Designation" Wrap="true" ></ogrid:Column> 
                        <ogrid:Column DataField="Location" HeaderText="Location" Wrap="true" ></ogrid:Column>         
                        <%--<ogrid:Column DataField="Status" HeaderText="Status" Width="100px" ></ogrid:Column>--%> 
                        <ogrid:Column DataField="Basic_Pay" HeaderText="Basic Pay" Wrap="true" ></ogrid:Column>
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
                        
                        <ogrid:Column DataField="" HeaderText="Delete" >
                             <TemplateSettings  TemplateId="GT_EC_Delete"/>
                        </ogrid:Column>
                        
                    </Columns>
                    <Templates>
                        <ogrid:GridTemplate ID="ECNoTemplate" runat="server">
                            <Template>
                               <asp:HyperLink ID="lnkSSNo" runat="server" CssClass="Grid_EC"   Text='<%#Container.DataItem["EmployeeID"] %>'>
                        </asp:HyperLink>
                            </Template>
                        </ogrid:GridTemplate>
                         <ogrid:GridTemplate ID="GT_EC_Delete" runat="server">
                            <Template>
                               <asp:LinkButton ID="lnkSS_Delete" runat="server" CssClass="Grid_EC" OnClientClick="beforedelete();" CommandArgument='<%# Container.PageRecordIndex %>' OnCommand="lnkSS_Delete_Command"  Text="Delete"></asp:LinkButton>
                                
                                    </ItemTemplate>  
                            </Template>
                        </ogrid:GridTemplate>

                        


                    </Templates>
                </ogrid:Grid>
                      
                
                
                <br />
                <center>
                    <a href="StaffSalaries.aspx" runat="server" id="lnkbtnAdd" class="btn btn-default">Add New Staff</a>               
                   <input onclick="exportgrid()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />  
                    <a href="SalarySlip.aspx" runat="server" id="btn_salary" class="btn btn-default">Salary Slip</a>
                    <a href="StaffMonthlySalaryList.aspx" runat="server" id="A1" class="btn btn-default">Generate Monthly Salary</a>
                </center>
                      </center>

            <asp:HiddenField runat="server" Value="" Id="HF_Confirm"/>


            <div id="ItemList_Gv" runat="server" visible="false" >
            <br />


            <center>
                <ogrid:Grid runat="server" ID="Gv_ECItemsList" CallbackMode="true"  AutoGenerateColumns="false"  AllowRecordSelection="false" FolderStyle="../Gridstyles/grand_gray" AllowAddingRecords="false" AllowFiltering="true" AllowPaging="true" PageSize="10">         
            <ScrollingSettings ScrollWidth="100%" />
             
            <CssSettings CSSExportHeaderCellStyle="font-weight: bold;font-size:16px; font-name:Trebuchet; text-decoration: underline;color: white; border-style:solid; border-width:medium; background-color:gray;"  CSSExportCellStyle="font-weight: normal;font-size:14px; font-name:Trebuchet; font-style: regular;color: black; border-style:solid; border-width:thin; border-color:black; background-color:White;"/>    
			 <ExportingSettings ExportAllPages="true" ExportTemplates="true" 
                  ColumnsToExport="Employee_ContractID,Full_Name,EmployeeID,FatherName,Employee_Permanent_Address,Email_ID,Qualification,Gender,Employee_Mobile_No,Date_of_Birth,Employment_Type,Project,Designation,Actual_Date_of_Joining,Location,Reporting_Manager,Status"  />
            <Columns>
              
                  <ogrid:Column DataField="Employee_ContractID" HeaderText="Employee ContractID" Width="100"></ogrid:Column>
                    <ogrid:Column DataField="Full_Name" HeaderText="Full Name" Width="150"></ogrid:Column>
                <ogrid:Column DataField="EmployeeID" HeaderText="Employee ID"  Width="100">

                </ogrid:Column>
                <ogrid:Column DataField="FatherName" HeaderText="Father Name"  Width="120"></ogrid:Column>
                 <ogrid:Column DataField="Employee_Permanent_Address" HeaderText="Employee Permanent Address" Width="120"></ogrid:Column>
                <ogrid:Column DataField="Email_ID" HeaderText="Email ID " Width="120"></ogrid:Column>
                 <ogrid:Column DataField="Qualification" HeaderText="Qualification" Width="100px" ></ogrid:Column>
                <ogrid:Column DataField="Gender" HeaderText="Gender"  Width="150" ></ogrid:Column>              
                  <ogrid:Column DataField="Employee_Mobile_No" HeaderText="Employee Mobile No" Align="right" Width="120"></ogrid:Column> 
                 <ogrid:Column DataField="Date_of_Birth" HeaderText="Date of Birth" Align="center" Width="200" DataFormatString="{0:dd-MM-yyy}"  ></ogrid:Column>
                <ogrid:Column DataField="Employment_Type" HeaderText="Employment Type" Width="120" Align="right"></ogrid:Column>            
                <ogrid:Column DataField="Project" HeaderText="Project" Width="120" Align="right"></ogrid:Column>    
                <ogrid:Column DataField="Designation" HeaderText="Designation" Width="135" Align="center"></ogrid:Column>             
                <ogrid:Column DataField="Actual_Date_of_Joining" HeaderText="Actual Date of Joining" Align="center" Width="200" DataFormatString="{0:dd-MM-yyy}"  ></ogrid:Column>
                 <ogrid:Column DataField="Location" HeaderText="Location" Width="135" Align="center"></ogrid:Column> 
                 <ogrid:Column DataField="Reporting_Manager" HeaderText="Reporting Manager" Width="135" Align="center"></ogrid:Column> 
                  <ogrid:Column DataField="Status" HeaderText="Status"  Align="center" Width="140" >
                     
                      <TemplateSettings TemplateId="WhetherReqQtyTemplate" />
                  </ogrid:Column> 
            </Columns>
              <Templates>
        
          <ogrid:GridTemplate ID="BOQTemplate" runat="server">
            <Template>
                <asp:Label ID="lblBOQ" runat="server" Text='<%#Container.DataItem["BOQ"].ToString() != string.Empty && Container.DataItem["BOQ"].ToString() == "True" ? "Yes" : "No" %>'></asp:Label>
            </Template>
        </ogrid:GridTemplate>
             <ogrid:GridTemplate ID="WhetherReqQtyTemplate" runat="server">
                 <Template>
                     <asp:Label ID="lblWhetherReqQty" runat="server" Text='<%#Container.DataItem["Whether_Req_Qty"].ToString() == "True" ? "Yes" :"No" %>'></asp:Label>
                 </Template>
             </ogrid:GridTemplate>
    </Templates>
                   
        </ogrid:Grid>
            </center>
                <br />

                <center>
                 <input onclick="exportgridECItems()" type="button" class="btn btn-default" name="Export To Excel" value=" Export To Excel" />
                </center>
                      
                </div>


        </div>
    </div>
</asp:Content>    


