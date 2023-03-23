using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_thongke_module_ThongKeNhapSanPham : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    public string TongTien;
    CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
    DataTable dtProduct;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            if (!IsPostBack)
            {
                //div_DoanhThu.Visible = false;
            }
        }
        else
        {
            Response.Redirect("/admin-login");
        }
    }
    public void loaddatatable()
    {
        try
        {
            if (dtProduct == null)
            {
                dtProduct = new DataTable();
                dtProduct.Columns.Add("nhaphang_createdate", typeof(DateTime));
                dtProduct.Columns.Add("product_id", typeof(int));
                dtProduct.Columns.Add("product_title", typeof(string));
                dtProduct.Columns.Add("nhaphang_chitiet_soluong", typeof(string));
                dtProduct.Columns.Add("nhaphang_gianhap", typeof(string));
                dtProduct.Columns.Add("nhaphang_thanhtien", typeof(string));
                dtProduct.Columns.Add("username_fullname", typeof(string));
            }
        }
        catch { }
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
            var getNH = from hd in db.tbNhapHangs
                        where hd.nhaphang_createdate.Value.Date >= Convert.ToDateTime(txt_TuNgay.Value).Date
                        && hd.nhaphang_createdate.Value.Date <= Convert.ToDateTime(txt_DenNgay.Value).Date
                        select hd;
            loaddatatable();
            int tt = 0;
            foreach (var nh in getNH)
            {
                var getctnh = (from ctnh in db.tbNhapHang_ChiTiets
                               join product in db.tbProducts on ctnh.product_id equals product.product_id
                               join u in db.admin_Users on ctnh.username_id equals u.username_id
                               where ctnh.nhaphang_code == nh.nhaphang_code
                               select new
                               {
                                   product.product_id,
                                   product.product_title,
                                   ctnh.nhaphang_chitiet_id,
                                   ctnh.nhaphang_gianhap,
                                   ctnh.nhaphang_chitiet_soluong,
                                   ctnh.nhaphang_thanhtien,
                                   u.username_fullname
                               });
                foreach (var item in getctnh)
                {
                    DataRow row = dtProduct.NewRow();
                    row["nhaphang_createdate"] = nh.nhaphang_createdate;
                    row["product_id"] = item.product_id;
                    row["product_title"] = item.product_title;
                    row["nhaphang_chitiet_soluong"] = item.nhaphang_chitiet_soluong;
                    row["nhaphang_gianhap"] = double.Parse(item.nhaphang_gianhap.ToString()).ToString("#,###", cul.NumberFormat);
                    row["nhaphang_thanhtien"] = double.Parse(item.nhaphang_thanhtien.ToString()).ToString("#,###", cul.NumberFormat);
                    row["username_fullname"] = item.username_fullname;
                    dtProduct.Rows.Add(row);
                    tt += Convert.ToInt32(item.nhaphang_thanhtien);
                }
            }
            rpNhapHang.DataSource = dtProduct;
            rpNhapHang.DataBind();
            TongTien = double.Parse(tt.ToString()).ToString("#,###", cul.NumberFormat) + " VND";
            div_DoanhThu.Visible = true;
        }

    }
}