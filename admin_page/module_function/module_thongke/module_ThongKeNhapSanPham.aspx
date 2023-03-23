<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="module_ThongKeNhapSanPham.aspx.cs" Inherits="admin_page_module_function_module_thongke_module_ThongKeNhapSanPham" %>

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
        <div class="doanhthu col-12">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="search">
                        <span>Từ ngày:
                        <input type="date" value="" runat="server" id="txt_TuNgay" />
                        </span>
                        <span>Đến ngày:
                        <input type="date" value="" runat="server" id="txt_DenNgay" />
                        </span>
                        <a href="javascript:void(0)" class="btn btn-primary" runat="server" id="btnXem" onserverclick="btnXem_ServerClick">Xem</a>
                    </div>
                    <div runat="server" id="div_DoanhThu">
                        <table class="table table-striped mt-1">
                            <thead>
                                <tr>
                                    <th style="width: 50px;">#</th>
                                    <th scope="col">Ngày nhập</th>
                                    <th scope="col">Tên sản phẩm</th>
                                    <th scope="col" style="text-align:center">Số lượng</th>
                                    <th scope="col">Đơn giá</th>
                                    <th scope="col">Thành tiền</th>
                                    <th scope="col">Nhân viên</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="rpNhapHang">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Container.ItemIndex+1 %></td>
                                            <td>
                                                <%#Eval("nhaphang_createdate", "{0: dd/MM/yyyy}") %>
                                            </td>
                                            <td>
                                                <%#Eval("product_title") %>
                                            </td>
                                            <td style="text-align:center">
                                                <%#Eval("nhaphang_chitiet_soluong") %>
                                            </td>
                                            <td>
                                                <%#Eval("nhaphang_gianhap") %>
                                            </td>
                                            <td>
                                                <%#Eval("nhaphang_thanhtien") %>
                                            </td>
                                            <td>
                                                <%#Eval("username_fullname") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr style="font-weight: 600">
                                    <td></td>
                                    <td>Tổng cộng:</td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td><%=TongTien%></td>
                                    <td></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="hibodybottom" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="hifooter" runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="hifootersite" runat="Server">
</asp:Content>

