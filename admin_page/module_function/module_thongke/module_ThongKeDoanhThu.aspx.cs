using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_thongke_module_ThongKeDoanhThu : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    public string TongHD, GiamGia, ThanhToan;
    CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            if (!IsPostBack)
            {
                div_DoanhThu.Visible = false;
            }
        }
        else
        {
            Response.Redirect("/admin-login");
        }
    }
    protected void btnXem_ServerClick(object sender, EventArgs e)
    {
        if (txt_TuNgay.Value == "")
        {
            alert.alert_Warning(Page, "Vui lòng chọn thời gian từ ngày", "");
        }
        else if (txt_DenNgay.Value == "")
        {
            alert.alert_Warning(Page, "Vui lòng chọn thời gian đến ngày", "");
        }
        else
        {
            var getDT = from hd in db.tbHoaDonBanHangs
                        where hd.hoadon_createdate.Value.Date >= Convert.ToDateTime(txt_TuNgay.Value).Date
                        && hd.hoadon_createdate.Value.Date <= Convert.ToDateTime(txt_DenNgay.Value).Date
                        select hd;
            rpDoanhThu.DataSource = getDT;
            rpDoanhThu.DataBind();
            int tonghd = 0;
            foreach (var item in getDT)
            {
                tonghd += Convert.ToInt32(item.hoadon_tongtien);
                //giamgia += Convert.ToInt32(item.hoadon_tongtiengiam);
                //phaitra += Convert.ToInt32(item.hoadon_phaitra);
            }
            TongHD = double.Parse(tonghd.ToString()).ToString("#,###", cul.NumberFormat) + " VND";
            //GiamGia = double.Parse(giamgia.ToString()).ToString("#,###", cul.NumberFormat) + " VND";
            //ThanhToan = double.Parse(phaitra.ToString()).ToString("#,###", cul.NumberFormat) + " VND";
            div_DoanhThu.Visible = true;
        }

    }
}