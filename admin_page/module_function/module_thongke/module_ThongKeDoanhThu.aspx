<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="module_ThongKeDoanhThu.aspx.cs" Inherits="admin_page_module_function_module_thongke_module_ThongKeDoanhThu" %>

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
                                    <th scope="col">Ngày</th>
                                    <th scope="col">Tên KH</th>
                                    <th scope="col">Tổng HĐ</th>
                                    <th>Chi Tiết</th>
                                    <%--<th scope="col">Giảm giá</th>
                                    <th scope="col">Phải trả</th>--%>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater runat="server" ID="rpDoanhThu">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Container.ItemIndex+1 %></td>
                                            <td>
                                                <%#Convert.ToDateTime(Eval("hoadon_createdate")).ToShortDateString()%>
                                            </td>
                                            <td>
                                                <%#Eval("khachhang_name") %>
                                            </td>
                                            <td>
                                                <%#Eval("hoadon_tongtien") %>
                                            </td>
                                            <td>
                                                <a href="/admin-chi-tiet-hoa-don-<%#Eval("hoadon_id") %>" class="btn btn-primary">Chi Tết</a>
                                            </td>
                                            <%--<td>
                                                <%#Eval("hoadon_tongtiengiam") %>
                                            </td>
                                            <td>
                                                <%#Eval("hoadon_phaitra") %>
                                            </td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr style="font-weight: 600">
                                    <td></td>
                                    <td>Tổng cộng:</td>
                                    <td></td>
                                    <td><%=TongHD%></td>
                                    <%--<td><%=GiamGia%></td>
                                    <td><%=ThanhToan%></td>--%>
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

