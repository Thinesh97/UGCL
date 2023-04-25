<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="~/Admin/SalarySlip.aspx.cs" Inherits="SalarySlip" %>
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
                        ddlmonth
                document.getElementById('<%=HF_Confirm.ClientID%>').value = "true";
                        return true;
                    }
                }
            </script>

    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>

                Generate Staff salary

            </h3>

        </div>
        <div class="panel-body">
            <center>
                <div class="row">
                    <div class="col-md-2">Staff Name </div>
                <div class="col-md-4">
                    <%--<asp:DropDownList runat="server" ID="ddlname" CssClass="form-control" TabIndex="2">
                        <asp:ListItem Value="0" Text="-Select-" Selected="True"></asp:ListItem>--%>
                      <asp:DropDownList runat="server" ID="ddlname" class="chosen-select form-control" AutoPostBack="true"  TabIndex="5">
                    </asp:DropDownList>
                    
                </div>
                <div class="col-md-2">Month</div>
                <div class="col-md-4">
                    <asp:DropDownList runat="server" ID="ddlmonth" CssClass="form-control" TabIndex="2">
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
                      
                
                
                <br />
                <center>
                    
                    <asp:Button ID="BtnGenerateSalarySlip" runat="server" class="btn btn-default" Text="Generate salary Slip" OnClick="BtnGenerateSalarySlip_Click" />
                   
                </center>
                      </center>

            <asp:HiddenField runat="server" Value="" Id="HF_Confirm"/>


            


        </div>
    </div>

</asp:Content>
