<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="module_XuatHangUpdate.aspx.cs" Inherits="admin_page_module_function_module_sanpham_module_XuatHangUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headlink" runat="Server">
    <style>
        .table th, .table td, .table thead tr th {
            border: 1px solid #a6a9ab;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="hihead" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="himenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="hibodyhead" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="hibodywrapper" runat="Server">
    <script type="text/javascript"> 
        function checkNULL() {
            var CityName = document.getElementById('<%= txtNoiDung.ClientID%>');

            if (CityName.value.trim() == "") {
                swal('Nội dung không được để trống!', '', 'warning').then(function () { CityName.focus(); });
                return false;
            }
            return true;
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
    <div class="container">
        <div class="card card-block">
            <div class="add_Product row">
                <div class="col-8">
                    <h2>Chi tiết xuất hàng</h2>
                    <div class="col-12 form-group">
                        <label class="form-control-label">Mã Xuất:</label>
                        <input type="text" id="txtMaXuat" runat="server" class="form-control" disabled="disabled" />
                    </div>
                    <div class="col-12 form-group">
                        <label class="form-control-label">Nội Dung:</label>
                        <textarea class="form-control" runat="server" id="txtNoiDung" rows="3" style="width: 90%"></textarea>
                        <%--<input type="text" id="txtNoiDung" runat="server" class="form-control" placeholder="Nhập Nội Dung" />--%>
                    </div>
                    <div class="col-12 form-group ">
                        <label class="form-control-label">Ngày Xuất:</label>
                        <input type="datetime" id="txtNgayNhap" runat="server" class="form-control" disabled="disabled" />
                    </div>
                    <div class="col-12 form-group">
                        <label class="form-control-label">Nhân Viên:</label>
                        <input type="text" id="txtNhanVien" runat="server" class="form-control" disabled="disabled" />
                    </div>
                    <%-- <div class="col-12 form-group">
                    <label class="form-control-label">Giá tiền:</label>
                    <input type="text" id="txtPrice" runat="server" class="form-control" />
                </div>--%>
                </div>
            </div>
        </div>
        <div class="add_Product_Detail row">
            <div class="col-5">
                <asp:UpdatePanel ID="upListProduct" runat="server">
                    <ContentTemplate>
                        <div class="col-12">
                            <dx:ASPxGridView ID="grvList" runat="server" CssClass="table-hover" ClientInstanceName="grvList" KeyFieldName="product_id" Width="100%">
                                <Columns>
                                    <dx:GridViewDataColumn Caption="STT" HeaderStyle-HorizontalAlign="Center" Width="5%">
                                        <DataItemTemplate>
                                            <%#Container.ItemIndex+1 %>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                    <dx:GridViewDataColumn Caption="Tên sản phẩm" FieldName="product_title" HeaderStyle-HorizontalAlign="Center" Width="50%"></dx:GridViewDataColumn>
                                    <%--<dx:GridViewDataColumn Caption="SL trong kho" FieldName="tonkho_soluong" HeaderStyle-HorizontalAlign="Center" Width="10%" CellStyle-CssClass="text-lg-center"></dx:GridViewDataColumn>--%>
                                    <dx:GridViewDataColumn Caption="Chi Tiết" FieldName="xem" HeaderStyle-HorizontalAlign="Center" Width="10%" HeaderStyle-Font-Bold="true">
                                        <DataItemTemplate>
                                            <a href="#" id="btnChiTiet" runat="server" onserverclick="btnChiTiet_ServerClick">Chi tiết</a>
                                        </DataItemTemplate>
                                    </dx:GridViewDataColumn>
                                </Columns>
                                <SettingsSearchPanel Visible="true" />
                                <SettingsBehavior AllowFocusedRow="true" />
                                <SettingsText EmptyDataRow="Trống" SearchPanelEditorNullText="Gõ từ cần tìm kiếm và enter..." />
                                <SettingsLoadingPanel Text="Đang tải..." />
                                <SettingsPager PageSize="10" Summary-Text="Trang {0} / {1} ({2} trang)"></SettingsPager>
                            </dx:ASPxGridView>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-7">
                <div class="Product_Detail card card-block">
                    <h5>xuất hàng chi tiết</h5>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table id="grvChitiet" class="table table-bordered table-hover">
                                <thead>
                                    <tr style="background: #a3a7a199">
                                        <th>STT</th>
                                        <th scope="col">Tên SP</th>
                                        <th scope="col">Số lượng</th>
                                        <%--<th scope="col">Giá nhập</th>
                                        <th scope="col">Giá bán</th>--%>
                                        <th scope="col">Xóa</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rp_grvChiTiet">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%= stt++ %></td>
                                                <td style="width: 150px;">
                                                    <asp:Label ID="product_title" runat="server"><%#Eval("product_title") %></asp:Label>
                                                </td>
                                                <td>
                                                    <input id="<%#Eval("product_id") %>" onchange="myUpdate(<%#Eval("product_id") %>)" class="form-control" style="width: 80px;" value="<%#Eval("xuathang_chitiet_soluong") %>" type="number" min="1" />
                                                </td>
                                                <%--<td>
                                                    <input style="width: 100px" type="text" onchange="myTotal(<%#Eval("product_id") %>),myUpdate(<%#Eval("product_id") %>)" id="txtGiaTien<%#Eval("product_id") %>" class="form-control" value="<%#Eval("nhaphang_gianhap") %>" onkeypress="return isNumberKey(event)" />
                                                </td>
                                                <td hidden="hidden">
                                                    <input style="width: 100px" type="text" id="txtThanhTien<%#Eval("product_id") %>" class="form-control" value="<%#Eval("nhaphang_thanhtien") %>" disabled="disabled" />
                                                </td>
                                                <td>
                                                    <input id="txtGiaBan<%#Eval("product_id") %>" onchange="myUpdate(<%#Eval("product_id") %>)" onkeypress="return isNumberKey(event)" class="form-control" style="width: 80px;" value="<%#Eval("nhaphang_giaban") %>" />
                                                </td>--%>
                                                <td>
                                                    <a href="javascript:void(0)" id="btnXoa<%#Eval("product_id") %>" onclick="Delete(<%#Eval("product_id") %>)">Xóa</a>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="btn btn-primary" Text="Xuất đơn hàng" />
                            <a href="/admin-quan-ly-xuat-hang" class="btn btn-primary">Quay lại</a>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="up_ProductCT" runat="server">
                        <ContentTemplate>
                            <div style="display: none;">
                                <input id="txt_ID" type="text" runat="server" />
                                <input id="txt_SoLuong" type="text" runat="server" />
                                <input id="txt_GiaTien" type="text" runat="server" />
                                <input id="txt_GiaBan" type="text" runat="server" />
                                <input id="txt_TongTien" type="text" runat="server" />
                                <a href="javascript:void(0)" id="NhapHang" type="button" runat="server" onserverclick="NhapHang_ServerClick">Update</a>
                                <%--nút xóa--%>
                                <a href="javascript:void(0)" id="btnXoa" type="button" runat="server" onserverclick="btnXoa_ServerClick">Xóa</a>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <script>
                var a, b, c;
                function myTotal(id) {
                    //var a, b, c;
                    a = document.getElementById(id).value;
                    b = document.getElementById("txtGiaTien" + id).value;
                    c = a * b;
                    document.getElementById("txtThanhTien" + id).value = c;
                }
                // update
                function myUpdate(id) {
                    a = document.getElementById(id).value;
                    // var price_ban = document.getElementById("txtGiaBan" + id).value
                    document.getElementById("<%= txt_ID.ClientID%>").value = id;
                    document.getElementById("<%= txt_SoLuong.ClientID%>").value = a;
                    //document.getElementById("<%= txt_GiaTien.ClientID%>").value = b;
                    //document.getElementById("<%= txt_TongTien.ClientID%>").value = c;
                    //document.getElementById("<%=txt_GiaBan.ClientID%>").value = price_ban;
                    document.getElementById("<%= NhapHang.ClientID%>").click();
                }
                function Delete(id) {
                    swal("Bạn có thực sự muốn xóa sản phẩm này?",
                        "Nếu xóa, dữ liệu không thể khôi phục",
                        "warning",
                        {
                            buttons: true,
                            dangerMode: true
                        }).then(function (value) {
                            if (value == true) {
                                document.getElementById("<%= txt_ID.ClientID%>").value = id;
                                document.getElementById("<%= btnXoa.ClientID%>").click();
                            }
                        });
                }
            </script>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="hibodybottom" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="hifooter" runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="hifootersite" runat="Server">
</asp:Content>

