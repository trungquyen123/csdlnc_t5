using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for adminmodule
/// </summary>
public class adminmodule
{
    public adminmodule()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public List<string> UrlRoutes()
    {
        List<string> list = new List<string>();
        //Module SEO
        list.Add("moduleseo|admin-seo|~/admin_page/module_function/module_SEO.aspx");
        //Module Language
        list.Add("modulelanguage|admin-ngon-ngu|~/admin_page/module_function/admin_LanguagePage.aspx");


        //Quản lý sản phẩm
        list.Add("modulesnhomanpham|admin-quan-ly-nhom-san-pham|~/admin_page/module_function/module_sanpham/module_NhomSanPham.aspx");
        list.Add("modulesanpham|admin-quan-ly-san-pham|~/admin_page/module_function/module_sanpham/module_ListSanPham.aspx");
        list.Add("modulenhaphang|admin-quan-ly-nhap-hang|~/admin_page/module_function/module_sanpham/module_NhapHang.aspx");
        list.Add("modulenhaphangthemmoi|admin-nhap-hang|~/admin_page/module_function/module_sanpham/module_NhapHangInsert.aspx");
        list.Add("modulenhaphangupdate|admin-nhap-hang-{id}|~/admin_page/module_function/module_sanpham/module_NhapHangUpdate.aspx");
        list.Add("modulexuathang|admin-quan-ly-xuat-hang|~/admin_page/module_function/module_sanpham/module_XuatHang.aspx");
        list.Add("modulexuathangthemmoi|admin-xuat-hang|~/admin_page/module_function/module_sanpham/module_XuatHangInsert.aspx");
        list.Add("modulexuathangupdate|admin-xuat-hang-{id}|~/admin_page/module_function/module_sanpham/module_XuatHangUpdate.aspx");
        //Quản lý website
        list.Add("moduleintroduce|admin-introduce|~/admin_page/module_function/module_website/module_Introduce.aspx");
        list.Add("moduleslide|admin-slide|~/admin_page/module_function/module_website/module_Slide.aspx");
        list.Add("modulenewcate|admin-new-cate|~/admin_page/module_function/module_website/module_NewCate.aspx");
        list.Add("modulenew|admin-news|~/admin_page/module_function/module_website/module_New.aspx");
        list.Add("modulechuongtrinhkm|admin-chuong-trinh-khuyen-mai|~/admin_page/module_function/module_website/module_ChuongTrinhKhuyenMai.aspx");
        list.Add("moduletaikhoankhachhang|admin-tai-khoan-khach-hang|~/admin_page/module_function/module_website/module_TaiKhoanKhachHang.aspx");


        //Quản lý dịch vụ
        list.Add("moduledichvu|admin-quan-ly-dich-vu|~/admin_page/module_function/module_dichvu/module_DichVu.aspx");


        //Quản lý hóa đơn
        list.Add("modulehoadonbanhang|admin-hoa-don-ban-hang|~/admin_page/module_function/module_hoadon/module_DanhSachHoaDon.aspx");
        list.Add("modulehoadonthemmoi|admin-them-moi-hoa-don|~/admin_page/module_function/module_hoadon/module_HoaDonThemMoi.aspx");
        list.Add("modulequanlyhoadon|admin-quan-ly-hoa-don|~/admin_page/module_function/module_hoadon/module_QuanLyHoaDon.aspx");
        list.Add("modulechitiethoadon|admin-chi-tiet-hoa-don-{_idDonHang}|~/admin_page/module_function/module_hoadon/module_ChiTietHoaDon.aspx");

        //xuất hóa đơn
        list.Add("modulexuathoadon|admin-xuat-hoa-don-{id}|~/admin_page/module_function/module_hoadon/module_InHoaDonDichVu.aspx");


        //chi phí hoạt động
       
        list.Add("modulenhomcphd|admin-quan-ly-nhom-chi-phi-hoat-dong|~/admin_page/module_function/module_chiphihoatdong/module_NhomChiPhiHoatDong.aspx");
        list.Add("modulequanlycphd|admin-quan-ly-chi-phi-hoat-dong|~/admin_page/module_function/module_chiphihoatdong/module_ChiPhiHoatDong.aspx");

        //thống kê doanh thu
        list.Add("modulethongkedoanhthu|admin-thong-ke-doanh-thu|~/admin_page/module_function/module_thongke/module_ThongKeDoanhThu.aspx");
        return list;
    }
}