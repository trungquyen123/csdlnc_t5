using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    public string TongTien, TongTienGiam, PhaiTra, TongHD, GiamGia, ThanhToan;
    CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            if (!IsPostBack)
            {
            }
            loadDoanhThuHomNay();
        }
        else
        {
            Response.Redirect("/admin-login");
        }
    }
    public void loadDoanhThuHomNay()
    {
        try
        {
            var getDTHN = from hd in db.tbHoaDonBanHangs
                              where hd.hoadon_createdate.Value.Date == DateTime.Now.Date
                              //&& hd.hidden==false
                          select hd;
            if (getDTHN.Count() > 0)
            {
                rpHoaDonHomNay.DataSource = getDTHN;
                rpHoaDonHomNay.DataBind();
                int tonghd = 0/*, giamgia = 0, phaitra = 0*/;
                foreach (var item in getDTHN)
                {
                    tonghd += Convert.ToInt32(item.hoadon_tongtien);
                    //giamgia += Convert.ToInt32(item.hoadon_tongtiengiam);
                    //phaitra += Convert.ToInt32(item.hoadon_phaitra);
                }
                TongTien = double.Parse(tonghd.ToString()).ToString("#,###", cul.NumberFormat) + " VND";
                //TongTienGiam = double.Parse(giamgia.ToString()).ToString("#,###", cul.NumberFormat) + " VND";
                //PhaiTra = double.Parse(phaitra.ToString()).ToString("#,###", cul.NumberFormat) + " VND";
            }
            else
            {
                div_doanhthuhomnay.Visible = false;
            }
        }
        catch (Exception) { }
    }

}