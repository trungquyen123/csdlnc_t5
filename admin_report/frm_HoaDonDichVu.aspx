<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="frm_HoaDonDichVu.aspx.cs" Inherits="admin_report_frm_HoaDonDichVu" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headlink" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="hihead" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="himenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="hibodyhead" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="hibodywrapper" runat="Server">
    <script>
        function printDiv(divName) {
            var printContents = document.getElementById(divName).innerHTML;
            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
    <div style="width: 100%; height: auto; background-color: #fff" id="printableArea">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Visible="true" Width="100%" Height="100%" ShowPrintButton="true">
        </rsweb:ReportViewer>
        <%--<rsweb:ReportViewer ID="ReportViewer2" runat="server" AsyncRendering="true" SizeToReportContent="true" Font-Names="Arial"
            Height="675px" Widths="750px" ProcessingMode="Remote" ShowPageNavigationControls="True" ShowPrintButton="True">
        </rsweb:ReportViewer>--%>
        <%--<rsweb:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Width="3500px" Height="4000px" ProcessingMode="Local" ShowPageNavigationControls="True" ShowPrintButton="True"></rsweb:ReportViewer>--%>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="hibodybottom" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="hifooter" runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="hifootersite" runat="Server">
</asp:Content>

