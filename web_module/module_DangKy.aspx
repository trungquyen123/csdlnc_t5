<%@ Page Title="" Language="C#" MasterPageFile="~/Web_MasterPage.master" AutoEventWireup="true" CodeFile="module_DangKy.aspx.cs" Inherits="web_module_module_DangKy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .form-login {
            display: grid;
            justify-items: center;
        }

            .form-login h3 {
                font-size: 30px;
            }

            .form-login h3, h5 {
                font-weight: 400;
            }

            .form-login p {
                font-size: larger;
            }

        .form-input {
            width: 30%;
        }

        .input-dangnhap {
            width: 30%;
            text-align: center;
            background: #cd6420;
            padding: 7px;
            font-size: 20px;
            color: white;
            border-radius: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <br />
    <br />
    <div class="form-login">
        <h3 class="text-center">ĐĂNG KÝ TÀI KHOẢN</h3>
        <p class="text-center">Bạn đã có tài khoản? <a href="/login">Đăng nhập tại đây</a></p>
        <br />
        <br />
        <h5 class="text-center">THÔNG TIN CÁ NHÂN</h5>
        <div class="form-input">
            <label>Họ và tên <span style="color: orangered">*</span></label>
            <input type="text" name="name" runat="server" id="txtHoTen" class="form-control" value="" placeholder="Nhập họ và tên" />
        </div>
        <br />
        <div class="form-input">
            <label>Số điện thoại <span style="color: orangered">*</span></label>
            <input type="text" name="name" class="form-control" runat="server" id="txtSdt" value="" placeholder="Nhập số điện thoại" />
        </div>
        <br />
        <div class="form-input">
            <label>Tên đăng nhập <span style="color: orangered">*</span></label>
            <input type="text" name="name" class="form-control" runat="server" id="txtUser" value="" placeholder="Nhập tên đăng nhập" />
        </div>
        <br />
        <div class="form-input">
            <label>Mật khẩu <span style="color: orangered">*</span></label>
            <input type="password" name="name" class="form-control" value="" runat="server" id="txtPass" placeholder="Nhập mật khẩu" />
        </div>
        <br />
        <a href="#" class="input-dangnhap" runat="server" id="btnDangKy" onserverclick="btnDangKy_ServerClick">Đăng ký</a>
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>

