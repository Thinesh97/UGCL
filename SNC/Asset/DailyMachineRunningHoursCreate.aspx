<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC.Master" AutoEventWireup="true" CodeBehind="DailyMachineRunningHoursCreate.aspx.cs" Inherits="DailyMachineRunningHoursCreate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="ogrid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../Style/Time_Picker/css/timepicki.css" rel="stylesheet" />
    <script src="../Style/Time_Picker/js/timepicki.js"></script>
    <style type="text/css">

       .tbl1 tr:first-child {
           background: #b42525;
           color: white;
       }

       .hideGridColumn
       {
           display:none;
       }
      .tbl1 tr:last-child{ background: white;color: black;}
         .gvwCasesPager a {
            margin-left: 5px;
            margin-right: 5px;
        }
    </style>
    <style type="text/css">  
            .scrolling {  
                position: absolute;  
            }  
              
            .gvWidthHight {  
                overflow: scroll;  
                height: 250px;  
                width: 600px;  
            }  
        </style>  
    <script type="text/javascript">
        function exportgrid() {
            ProjectList_Grid.exportToExcel();
        }

        function calcOutput(obj) {
            debugger;
            var txtStart = $(obj).closest('tr').find("input[id*='txtStartKM']").val();
            var txtEnd = $(obj).closest('tr').find("input[id*='txtEndKM']").val();
            
            if (txtStart != "" && txtEnd != "") {
                $(obj).closest('tr').find("input[id*='txtOutput']").val(parseFloat(txtEnd) - parseFloat(txtStart));
            }
            else {
                $(obj).closest('tr').find("input[id*='txtOutput']").val("0.00");
            }
        }

        $(document).ready(function () {

            $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
            $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });
        });

        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {

                    $(".input-pos-int").limitkeypress({ rexp: /^[+]?\d*$/ });
                    $(".input-pos-float").limitkeypress({ rexp: /^[$0-9]?\d*\.?\d{0,2}$/ });

                }
            });
        };

    </script>

    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></ajaxToolkit:ToolkitScriptManager>

    <!--Making Changes In the Page starts here -->
    <div class="panel panel-default">
        <div class="panel-heading">

            <h3 class="panel-title">
                <i class="glyphicon glyphicon-envelope"></i>
                Daily Running Hours/Kms
            </h3>

        </div>
        
        <div class="panel-body">
            <!---------------------------------------------------------------Body Content-------------------------------------------------------------------->

            <div class="row">
                <div class="col-md-1">
                    Projects
                        <asp:RequiredFieldValidator ID="RFVddlprojectName" runat="server" CssClass="Validation_Text" InitialValue="-Select" ControlToValidate="ddlProjectNames"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlProjectNames" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    Date
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="Validation_Text" ValidationGroup="valSearch" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtDate" AutoPostBack="true" OnTextChanged="txtDate_TextChanged" CssClass="form-control" onkeypress="javascript: return false;" onPaste="javascript: return false;" AutoComplete="Off" runat="server" ></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" Format="dd-MM-yyyy" runat="server"></ajaxToolkit:CalendarExtender>
                </div>

                <div class="col-md-1">
                    Available Diesel (Liter)
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtIssuedDieselQty" Enabled="false" runat="server" CssClass="form-control input-pos-float" TabIndex="10"></asp:TextBox>
                </div>
            </div>
            
            <div class="row">
                <div class="col-md-12 text-center">
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" ValidationGroup="valSearch" CssClass="btn btn-default" />
                </div>
            </div>

            <center>
                           
                <div style="width:100%" class="gvWidthHight">
                    <asp:GridView ID="gvDailyRunningHours"   AllowPaging="true" PagerStyle-HorizontalAlign="Right"    OnPageIndexChanging="gvDailyRunningHours_PageIndexChanging"  OnRowDataBound="gvDailyRunningHours_RowDataBound"  PageSize="10" ShowHeaderWhenEmpty="true" ShowHeader="true" EmptyDataText="No Record Found"  runat="server" ShowFooter="true" CssClass="table tbl1" AutoGenerateColumns="false" >
                         <PagerStyle CssClass="gvwCasesPager" />
                        <Columns>
                            <asp:BoundField DataField="AssetName" HeaderText="Asset Name"  />
                            <asp:TemplateField HeaderText="Code" Visible="false" >
                                <ItemTemplate >
                                    <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code") %>'  />
                                </ItemTemplate>
                            </asp:TemplateField>
                          
                            <asp:BoundField DataField="Asset_Type" HeaderText="Asset Type" />
                            <asp:BoundField DataField="Reg_No" HeaderText="Registration Number" />
                            <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                            <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate >
                                    <asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Unit") %>'  />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start KM / Hr">
                                <ItemTemplate >
                                    <asp:TextBox ID="txtStartKM" runat="server" Width="50px" Text="0.00" OnKeyUp="calcOutput(this);" CssClass="form-control input-pos-float"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End KM / Hr" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEndKM" runat="server" Width="50px" Text="0.00" OnKeyUp="calcOutput(this)" CssClass="form-control input-pos-float"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control"  Width="80px"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Output">
                                <ItemTemplate>
                                <asp:TextBox ID="txtOutput" runat="server"  Width="80px" Text="0.00"  CssClass="form-control input-pos-float"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Issued Diesel">
                                <ItemTemplate>
                                <asp:TextBox ID="txtIssuedDiesel" runat="server"  Width="80px" Text="0.00"  CssClass="form-control input-pos-float"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                <asp:TextBox ID="txtRemarks" runat="server"  Width="100px" TextMode="MultiLine" style="resize:none"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                          <asp:TemplateField HeaderText="File Upload" >
                                            <ItemTemplate>
                                                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                                                                              
                                                    </ContentTemplate>                                                                                                                                               
                                                    </asp:UpdatePanel>     --%>
                                                <asp:FileUpload ID="FUDocs"  runat="server"/> 
                                            </ItemTemplate>
                                        </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                     
                </div>
                      
            </center>


        </div>

        <div class="row">
            <div class="col-md-12 text-center">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btnSave_Click" ></asp:Button>
                <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CssClass="btn btn-default" ></asp:Button>
            </div>
        </div>

    </div>
    <!-- Ends here-->
    
</asp:Content>
