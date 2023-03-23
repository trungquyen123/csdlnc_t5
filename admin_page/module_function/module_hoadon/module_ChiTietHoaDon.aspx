<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="module_ChiTietHoaDon.aspx.cs" Inherits="admin_page_module_function_module_hoadon_module_ChiTietHoaDon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headlink" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="hihead" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="himenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="hibodyhead" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="hibodywrapper" runat="Server">
    <div class="card card-block">
        <center>
            <h2> <b> Chi Tiết Đơn Hàng</b></h2>
            <br />
            <br />
        </center>
        <table class="table col-7">
            <tr>
                <th>STT</th>
                <th style="width: 30%">Sản phẩm</th>
                <th>Tên sản phẩm</th>
                <th>Nhóm sản phẩm</th>
                <th>Giá</th>
            </tr>
            <asp:Repeater runat="server" ID="rpChiTietDonHang">
                <ItemTemplate>
                    <tr>
                        <td><%#Container.ItemIndex+1 %></td>
                        <td>
                            <img style="width: 50%" src="<%#Eval("product_image") %>" alt=""></td>
                        <td><%#Eval("product_title") %></td>
                        <td><%#Eval("productcate_title") %></td>
                        <td><%#Eval("t") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>

    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="hibodybottom" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="hifooter" runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="hifootersite" runat="Server">
</asp:Content>

