<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="module_NhapHang.aspx.cs" Inherits="admin_page_module_function_module_NhapHang" %>

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
                    <th>Giá</th>
                    <th>Giảm giá</th>
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
                                <div style="display: flex">
                                    <span class="mr-1"><%#Eval("product_soluong") %></span>
                                    <input type="text" id="txtSL<%#Eval("product_id") %>" style="width: 50px;" name="name" value="" />
                                </div>
                            </td>
                            <td>
                                <div style="display: flex">
                                    <span class="mr-1"><%#Eval("product_price") %></span>
                                    <input type="text" id="txtGIA<%#Eval("product_id") %>" style="width: 50px;" name="name" value="" />
                                </div>
                            </td>
                            <td>
                                <div style="display: flex">
                                    <span class="mr-1"><%#Eval("product_promotions") %></span>
                                    <input type="text" id="txtGIAMGIA<%#Eval("product_id") %>" style="width: 50px;" name="name" value="" />
                                </div>
                            </td>
                            <td>
                                <a href="#" onclick="getLuu(<%#Eval("product_id") %>,<%#Eval("product_soluong") %>,<%#Eval("product_price") %>,<%#Eval("product_promotions") %>)" class="btn btn-primary">Lưu</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div style="display: none">
            <input type="text" runat="server" id="txtid" name="name" value="" />
            <input type="text" runat="server" id="txtsl" name="name" value="" />
            <input type="text" runat="server" id="txtgia" name="name" value="" />
            <input type="text" runat="server" id="txtgiamgia" name="name" value="" />
            <a href="#" id="btnluu" runat="server" onserverclick="btnluu_ServerClick">content</a>
        </div>
    </div>
    <script>
        function getLuu(id) {
            document.getElementById("<%=txtid.ClientID%>").value = id;
            document.getElementById("<%=txtsl.ClientID%>").value = document.getElementById("txtSL" + id).value;
            document.getElementById("<%=txtgia.ClientID%>").value = document.getElementById("txtGIA" + id).value;
            document.getElementById("<%=txtgiamgia.ClientID%>").value = document.getElementById("txtGIAMGIA" + id).value;
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

