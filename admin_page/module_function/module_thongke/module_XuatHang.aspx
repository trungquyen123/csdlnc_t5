<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="module_XuatHang.aspx.cs" Inherits="admin_page_module_function_module_sanpham_module_XuatHang" %>

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
        <div class="container">
            <br />
            <br />
            <center>
                <h1>Quản lý nhập hàng</h1>
            </center>
            <br />
            <br />
            <table class="table">
                <tr>
                    <th>Tên sản phẩm</th>
                    <th>Sản phẩm</th>
                    <th>Số lượng</th>
                    <th>Đã bán</th>
                    <th></th>
                </tr>
                <asp:Repeater runat="server" ID="rpNhapHang">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("product_title") %></td>
                            <td>
                                <img style="width: 20%" src="<%#Eval("product_image") %>" alt="Alternate Text" />
                            </td>
                            <td>
                                <span class="mr-1"><%#Eval("product_soluong") %></span>
                                <input type="text" id="txtsl<%#Eval("product_id") %>" hidden="hidden" name="name" value="<%#Eval("product_soluong") %>" />
                            </td>
                            <td>
                                <input type="text" id="txtban<%#Eval("product_id") %>" style="width: 50px;" name="name" value="" />
                            </td>
                            <td>
                                <a href="#" onclick="getLuu(<%#Eval("product_id") %>)" class="btn btn-primary">Lưu</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="display: none">
            <input type="text" runat="server" id="txtid" name="name" value="" />
            <input type="text" runat="server" id="txtban" name="name" value="" />
            <input type="text" runat="server" id="txtsl" name="name" value="" />
            <a href="#" id="btnluu" runat="server" onserverclick="btnluu_ServerClick">content</a>
        </div>
    </div>
    <script>
        function getLuu(id) {
            document.getElementById("<%=txtid.ClientID%>").value = id;
            document.getElementById("<%=txtban.ClientID%>").value = document.getElementById("txtban" + id).value;
            document.getElementById("<%=txtsl.ClientID%>").value = document.getElementById("txtsl" + id).value;
            document.getElementById("<%=btnluu.ClientID%>").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="hibodybottom" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="hifooter" runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="hifootersite" runat="Server">
</asp:Content>

