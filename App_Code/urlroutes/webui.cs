using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for webui
/// </summary>
public class webui
{
	public webui()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<string> UrlRoutes()
    {
        List<string> list = new List<string>();
        list.Add("webTrangChu|home|~/web_module/module_TrangChu.aspx");
        // Introduce

        //Đăng ký
        list.Add("webDangKy|login|~/web_module/module_Login.aspx");
        //Đăng Nhập
        list.Add("webDangNhap|dang-ky|~/web_module/module_DangKy.aspx");
        //Chi tiết
        list.Add("webchitiet|chi-tiet-{id_ct}|~/web_module/module_ChiTiet.aspx");
        //Sản phẩm
        list.Add("websanpham|san-pham-{id_ctsp}|~/web_module/module_ChiTietSanPham.aspx");
        return list;
    }
}