<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Authantication.Master" AutoEventWireup="true" CodeBehind="GenerateSalarySlip.aspx.cs" Inherits="SNC.Admin.GenerateSalarySlip" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  
    <div>

        <table class="table table-condensed table-bordered complinceone" style="font-size:10px;"> 
            <tr>
                <td colspan="24"><img src="../Images/Logo.jpg" width="1300px" height="100px"/></td>
                </tr>
            <tr>
                <td colspan="24" style="font-size:17px; font-weight:bold; text-align:center;">PAYSLIP FOR THE MONTH OF SEPTEMBER 2022</td>
            </tr>
                <tr>
                    <th colspan="6">Name</th>
                    <th colspan="6"><asp:Label ID="LblName" runat="server"></asp:Label></></th>
                    <th colspan="6">Salary Mode</th>
                    <th colspan="6"><asp:Label ID="LblSalaryMode" runat="server">NEFT / IMPS</asp:Label></th>
                </tr>
             <tr>
                    <th colspan="6">Designation.</th>
                    <th colspan="6"><asp:Label ID="LblDestination" runat="server"></asp:Label></th>
                    <th colspan="6">Bank</th>
                    <th colspan="6"><asp:Label ID="LblBank" runat="server"></asp:Label></th>
                </tr>
             <tr>
                     <th colspan="6">Date of Joining</th>
                  <th colspan="6"><asp:Label ID="LblDateOfJoining" runat="server"></asp:Label></th>
                    <th colspan="6">Account Number</th>
                    <th colspan="6"><asp:Label ID="LblAccountNumber" runat="server"></asp:Label></th>
                </tr>
             <tr>
                      <th colspan="6">Month Days</th>
                    <th colspan="6"><asp:Label ID="LblMonthDays" runat="server"></asp:Label></th>
                    <th colspan="6">No. of Days Presents</th>
                    <th colspan="6"><asp:Label ID="LblDaysPresent" runat="server"></asp:Label></th>
                </tr>
             <tr>
                    <th colspan="6">Payslip for the month of</th>
                    <th colspan="6"><asp:Label ID="LblPayslip" runat="server"></asp:Label></th>
                    <th colspan="6">Employee ID</th>
                    <th colspan="6"><asp:Label ID="LblEmployeeId" runat="server"></asp:Label></th>
                </tr>
            <%--<tr>
                <td colspan="24"  style="font-size:17px; font-weight:bold; text-align:center;"></td>
            </tr>--%>
             <tr>
                    <th colspan="6" style="font-size:14px; font-weight:bold;"> EARNINGS </th>
                  <th colspan="6" style="font-size:14px; font-weight:bold;">ACTUAL</th>
                     <th colspan="6" style="font-size:14px; font-weight:bold;">EARNED</th>
                  <th colspan="12" style="font-size:14px; font-weight:bold;">DEDUCTIONS AMOUNT </th>
                    
                </tr>
                 <tr>
                    <th colspan="6">Basic</th>
                    <th colspan="6"><asp:Label ID="LblBasicActual" runat="server"></asp:Label></th>
                    <th colspan="5"><asp:Label ID="LblBasicEarned" runat="server"></asp:Label></th>
                    <th colspan="2" style="width:150px">PF AMOUNT</th>
                      <th colspan="2" style="width:150px"><asp:Label ID="LblPFAmount" runat="server"></asp:Label></th>
                </tr>
             <tr>
                     <th colspan="6">HRA</th>
                    <th colspan="6"><asp:Label ID="LblActualHRA" runat="server"></asp:Label></th>
                    <th colspan="5"style="width:150px"><asp:Label ID="LblEarndHRA" runat="server"></asp:Label></th>
                    <th colspan="2"style="width:150px">TDS</th>
                      <th colspan="2"><asp:Label ID="LblTDS" runat="server"></asp:Label></th>
                </tr>
             <tr>
                     <th colspan="6">CONVEYANCE</th>
                  <th colspan="6"><asp:Label ID="LblCONVEYANCEActual" runat="server"></asp:Label></th>
                     <th colspan="5"style="width:150px"><asp:Label ID="LblCONVEYANCEearned" runat="server"></asp:Label></th>
                    <th colspan="2"style="width:150px">PROFESSIONAL TAX</th>
                 <th colspan="2"><asp:Label ID="LblProfessionalTax" runat="server"></asp:Label></th>
                </tr>
             <tr>
                      <th colspan="6">MEDICAL</th>
                    <th colspan="6"><asp:Label ID="LblMedicalActual" runat="server"></asp:Label></th>
                    <th colspan="5"style="width:150px"><asp:Label ID="LblMedicalEarned" runat="server"></asp:Label></th>
                    <th colspan="2"style="width:150px">ADVANCE & RECOVERIES</th>
                 <th colspan="2"><asp:Label ID="LblAdvanceRecovery" runat="server"></asp:Label></th>
                </tr>
             <tr>
                    <th colspan="6">SPECIAL CONVEYANCE </th>
                    <th colspan="6"><asp:Label ID="LblSPECIALActual" runat="server"></asp:Label></th>
                    <th colspan="5"style="width:150px"><asp:Label ID="LblSpecialEarned" runat="server"></asp:Label></th>
                    <th colspan="2"style="width:150px">LOSS OF PAY (IF ANY)</th>
                 <th colspan="2"><asp:Label ID="LblLossofpay" runat="server"></asp:Label></th>
                </tr>
              <tr>
                    <th colspan="6">CCA</th>
                   <th colspan="6"></th>
                    <th colspan="6"> </th>
                    <th colspan="6"></th>
                    
                </tr>
            <tr>
                    <th colspan="6" style="font-size:14px; font-weight:bold;" >GROSS TOTAL </th>
                    <th colspan="6"><asp:Label ID="LblGROSSActual" runat="server"></asp:Label></th>
                    <th colspan="5"style="width:150px"><asp:Label ID="LblGrossEarned" runat="server"></asp:Label></th>
                    <th colspan="2"style="width:150px; font-size:14px; font-weight:bold;">TOTAL DEDUCTION</th>
                 <th colspan="2"><asp:Label ID="LblTotaldeduction" runat="server"></asp:Label></th>
                </tr>
            
            
             <%--<tr>
                <td colspan="24"  style="font-size:17px; font-weight:bold; text-align:center;"></td>
            </tr>--%>
             <tr>
                    <th colspan="6" style="font-size:14px; font-weight:bold;"> NET PAY </th>
                  <%--<th colspan="6" style="font-size:14px; font-weight:bold;">ACTUAL</th>
                     <th colspan="6" style="font-size:14px; font-weight:bold;"></th>
                  <th colspan="12" style="font-size:14px; font-weight:bold;"> </th>--%>
                    
                </tr>
            <tr><th colspan="24"style="width:500px"><asp:Label ID="Label1" runat="server">In Words:</asp:Label></th>
                </tr
              </table>
        
    </div>
                <br />
                <br />
        
</asp:Content>
