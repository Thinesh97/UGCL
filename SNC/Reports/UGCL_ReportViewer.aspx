<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/SNC_Authantication.Master" AutoEventWireup="true" CodeBehind="UGCL_ReportViewer.aspx.cs" Inherits="SNC.Reports.UGCL_ReportViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <asp:UpdatePanel runat="server" ID="Upd">
            <ContentTemplate>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" BackColor="#f9e79f" Height="650px" HyperlinkTarget="_blank"
                    PageCountMode="Actual">
                    <LocalReport EnableHyperlinks="True">
                    </LocalReport>
                </rsweb:ReportViewer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
