<%@ Page Title="" Language="C#" MasterPageFile="~/Admin_MasterPage.master" AutoEventWireup="true" CodeFile="module_NhomSanPham.aspx.cs" Inherits="admin_page_module_function_module_sanpham_module_NhomSanPham" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headlink" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="hihead" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="himenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="hibodyhead" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="hibodywrapper" runat="Server">
    <script type="text/javascript">
        function func() {
            grvList.Refresh();
            popupControl.Hide();
        }
        function btnChiTiet() {
            document.getElementById('<%=btnChiTiet.ClientID%>').click();
        }
        function popupHide() {
            document.getElementById('btnClosePopup').click();
        }
        function checkNULL() {
            var CityName = document.getElementById('<%= txttensanpham.ClientID%>');

            if (CityName.value.trim() == "") {
                swal('Dữ liệu không được để trống!', '', 'warning').then(function () { CityName.focus(); });
                return false;
            }
            return true;
        }
        function confirmDel() {
            swal("Bạn có thực sự muốn xóa?",
                "Nếu xóa, dữ liệu sẽ không thể khôi phục.",
                "warning",
                {
                    buttons: true,
                    dangerMode: true
                }).then(function (value) {
                    if (value == true) {
                        var xoa = document.getElementById('<%=btnXoa.ClientID%>');
                        xoa.click();
                    }
                });
        }
    </script>
    <div class="card card-block">
        <div class="form-group row">
            <div class="col-sm-10">
                <asp:UpdatePanel ID="udButton" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnThem" runat="server" Text="Thêm" CssClass="btn btn-primary" OnClick="btnThem_Click" />
                        <asp:Button ID="btnChiTiet" runat="server" Text="Chi tiết" CssClass="btn btn-primary" OnClick="btnChiTiet_Click" />
                        <input type="submit" class="btn btn-primary" value="Xóa" onclick="confirmDel()" />
                        <asp:Button ID="btnXoa" runat="server" CssClass="invisible" OnClick="btnXoa_Click" />
                        <%--<asp:Button ID="btnHienThi" runat="server" Text="Hiển thị trang chủ" CssClass="btn btn-primary" OnClick="btnHienThi_Click" />--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group table-responsive">
            <dx:ASPxGridView ID="grvList" runat="server" ClientInstanceName="grvList" KeyFieldName="productcate_id" Width="75%">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" VisibleIndex="0" Width="2%">
                    </dx:GridViewCommandColumn>
                    <dx:GridViewDataColumn Caption="Tên nhóm sản phẩm" FieldName="productcate_title" HeaderStyle-HorizontalAlign="Center" Width="20%"></dx:GridViewDataColumn>
                </Columns>
                <ClientSideEvents RowDblClick="btnChiTiet" />
                <SettingsSearchPanel Visible="true" />
                <SettingsBehavior AllowFocusedRow="true" />
                <SettingsText EmptyDataRow="Không có dữ liệu" SearchPanelEditorNullText="Gỏ từ cần tìm kiếm và enter..." />
                <SettingsLoadingPanel Text="Đang tải..." />
                <SettingsPager PageSize="10" Summary-Text="Trang {0} / {1} ({2} trang)"></SettingsPager>
            </dx:ASPxGridView>
        </div>
    </div>
    <dx:ASPxPopupControl ID="popupControl" runat="server" Width="500px" Height="300px" CloseAction="CloseButton" ShowCollapseButton="True" ShowMaximizeButton="True" ScrollBars="Auto" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="popupControl" ShowFooter="true"
        HeaderText="Nhóm sản phẩm" AllowDragging="True" PopupAnimationType="Fade" EnableViewState="False" AutoUpdatePosition="true" ClientSideEvents-CloseUp="function(s,e){grvList.Refresh();}">
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <asp:UpdatePanel ID="udPopup" runat="server">
                    <ContentTemplate>
                        <div class="popup-main">
                            <div class="div_content col-12">
                                <div class="col-12">
                                    <div class="col-12 form-group">
                                        <label class="col-2 form-control-label">Tên nhóm sản phẩm:</label>
                                        <div class="col-10">
                                            <asp:TextBox ID="txttensanpham" runat="server" ClientIDMode="Static" CssClass="form-control boxed" Width="90%"> </asp:TextBox>
                                        </div>
                                    </div>
                                    <%--<div class="col-12 form-group">
                                            <label class="col-2 form-control-label">Tóm tắt:</label>
                                            <div class="col-10">
                                                <asp:TextBox ID="txtSummary" runat="server" ClientIDMode="Static" CssClass="form-control boxed" Width="90%"> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="text-color1"><i>Kích thước ảnh tiêu chuẩn 800x800</i></div>
                                            <div class="colum-5 form-group">
                                                <label class="form-control-label">Hình ảnh :</label>
                                                <div id="up1" class="">
                                                    <asp:FileUpload CssClass="hidden-xs-up" ID="FileUpload1" runat="server" onchange="showPreview1(this)" />
                                                    <button type="button" class="btn-chang" onclick="clickavatar1()">
                                                        <img id="imgPreview1" src="/admin_images/up-img.png" style="width: 100%" />
                                                    </button>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-12">
                                            <div class="col-12 form-group">
                                                <label class="col-12 form-control-label">Nội dung:</label>
                                            </div>
                                            <div class="col-12 form-group">
                                                <div class="col-12">
                                                    <dx:ASPxHtmlEditor ID="edtnoidung" ClientInstanceName="edtnoidung" runat="server" Width="100%" Height="400px" Border-BorderStyle="Solid" Border-BorderWidth="1px" Border-BorderColor="#dddddd">
                                                        <SettingsHtmlEditing EnablePasteOptions="true" />
                                                        <Settings AllowHtmlView="true" AllowContextMenu="Default" />
                                                        <settingsimageupload uploadfolder="~/editorimages"></settingsimageupload>
                                                        <Toolbars>
                                                            <dx:HtmlEditorToolbar>
                                                                <Items>
                                                                    <dx:ToolbarCustomCssEdit Width="120px">
                                                                        <Items>
                                                                            <dx:ToolbarCustomCssListEditItem TagName="" Text="Clear Style" CssClass="" />
                                                                            <dx:ToolbarCustomCssListEditItem TagName="H1" Text="Title" CssClass="CommonTitle">
                                                                                <PreviewStyle CssClass="CommonTitlePreview" />
                                                                            </dx:ToolbarCustomCssListEditItem>
                                                                            <dx:ToolbarCustomCssListEditItem TagName="H3" Text="Header1" CssClass="CommonHeader1">
                                                                                <PreviewStyle CssClass="CommonHeader1Preview" />
                                                                            </dx:ToolbarCustomCssListEditItem>
                                                                            <dx:ToolbarCustomCssListEditItem TagName="H4" Text="Header2" CssClass="CommonHeader2">
                                                                                <PreviewStyle CssClass="CommonHeader2Preview" />
                                                                            </dx:ToolbarCustomCssListEditItem>
                                                                            <dx:ToolbarCustomCssListEditItem TagName="Div" Text="Content" CssClass="CommonContent">
                                                                                <PreviewStyle CssClass="CommonContentPreview" />
                                                                            </dx:ToolbarCustomCssListEditItem>
                                                                            <dx:ToolbarCustomCssListEditItem TagName="Strong" Text="Features" CssClass="CommonFeatures">
                                                                                <PreviewStyle CssClass="CommonFeaturesPreview" />
                                                                            </dx:ToolbarCustomCssListEditItem>
                                                                            <dx:ToolbarCustomCssListEditItem TagName="Div" Text="Footer" CssClass="CommonFooter">
                                                                                <PreviewStyle CssClass="CommonFooterPreview" />
                                                                            </dx:ToolbarCustomCssListEditItem>
                                                                            <dx:ToolbarCustomCssListEditItem TagName="" Text="Link" CssClass="Link">
                                                                                <PreviewStyle CssClass="LinkPreview" />
                                                                            </dx:ToolbarCustomCssListEditItem>
                                                                            <dx:ToolbarCustomCssListEditItem TagName="EM" Text="ImageTitle" CssClass="ImageTitle">
                                                                                <PreviewStyle CssClass="ImageTitlePreview" />
                                                                            </dx:ToolbarCustomCssListEditItem>
                                                                            <dx:ToolbarCustomCssListEditItem TagName="" Text="ImageMargin" CssClass="ImageMargin">
                                                                                <PreviewStyle CssClass="ImageMarginPreview" />
                                                                            </dx:ToolbarCustomCssListEditItem>
                                                                        </Items>
                                                                    </dx:ToolbarCustomCssEdit>
                                                                    <dx:ToolbarParagraphFormattingEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Normal" Value="p" />
                                                                            <dx:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                                                            <dx:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                                                            <dx:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                                                            <dx:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                                                            <dx:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                                                            <dx:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                                                            <dx:ToolbarListEditItem Text="Address" Value="address" />
                                                                            <dx:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                                                        </Items>
                                                                    </dx:ToolbarParagraphFormattingEdit>
                                                                    <dx:ToolbarFontNameEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Value="Times New Roman" Text="Times New Roman"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="Tahoma" Text="Tahoma"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="Verdana" Text="Verdana"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="Arial" Text="Arial"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="MS Sans Serif" Text="MS Sans Serif"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="Courier" Text="Courier"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="bodoni MT" Text="bodoni MT"></dx:ToolbarListEditItem>
                                                                        </Items>
                                                                    </dx:ToolbarFontNameEdit>
                                                                    <dx:ToolbarFontSizeEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Value="1" Text="1 (8pt)"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="2" Text="2 (10pt)"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="3" Text="3 (12pt)"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="4" Text="4 (14pt)"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="5" Text="5 (18pt)"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="6" Text="6 (24pt)"></dx:ToolbarListEditItem>
                                                                            <dx:ToolbarListEditItem Value="7" Text="7 (36pt)"></dx:ToolbarListEditItem>
                                                                        </Items>
                                                                    </dx:ToolbarFontSizeEdit>
                                                                    <dx:ToolbarBoldButton BeginGroup="True" />
                                                                    <dx:ToolbarItalicButton />
                                                                    <dx:ToolbarUnderlineButton />
                                                                    <dx:ToolbarStrikethroughButton />
                                                                    <dx:ToolbarJustifyLeftButton BeginGroup="True" />
                                                                    <dx:ToolbarJustifyCenterButton />
                                                                    <dx:ToolbarJustifyRightButton />
                                                                    <dx:ToolbarJustifyFullButton />
                                                                    <dx:ToolbarBackColorButton BeginGroup="True" />
                                                                    <dx:ToolbarFontColorButton />
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                        </Toolbars>
                                                    </dx:ASPxHtmlEditor>
                                                </div>
                                            </div>
                                        </div>--%>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </dx:PopupControlContentControl>
        </ContentCollection>
        <FooterContentTemplate>
            <div class="mar_but button">
                <asp:Button ID="btnLuu" runat="server" ClientIDMode="Static" Text="Lưu" CssClass="btn btn-primary" OnClientClick="return checkNULL()" OnClick="btnLuu_Click" />
            </div>
        </FooterContentTemplate>
        <ContentStyle>
            <Paddings PaddingBottom="0px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="hibodybottom" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="hifooter" runat="Server">
</asp:Content>
<asp:Content ID="Content8" ContentPlaceHolderID="hifootersite" runat="Server">
</asp:Content>

