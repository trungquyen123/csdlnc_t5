using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_thongke_module_ThongKeXuatSanPham : System.Web.UI.Page
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
                dtProduct.Columns.Add("xuathang_createdate", typeof(DateTime));
                dtProduct.Columns.Add("product_id", typeof(int));
                dtProduct.Columns.Add("product_title", typeof(string));
                dtProduct.Columns.Add("xuathang_chitiet_soluong", typeof(string));
                dtProduct.Columns.Add("xuathang_giaxuat", typeof(string));
                dtProduct.Columns.Add("xuathang_thanhtien", typeof(string));
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
            var getNH = from hd in db.tbXuatHangs
                        where hd.xuathang_createdate.Value.Date >= Convert.ToDateTime(txt_TuNgay.Value).Date
                        && hd.xuathang_createdate.Value.Date <= Convert.ToDateTime(txt_DenNgay.Value).Date
                        select hd;
            loaddatatable();
            int tt = 0;
            foreach (var nh in getNH)
            {
                var getctnh = (from ctxh in db.tbXuatHang_ChiTiets
                               join product in db.tbProducts on ctxh.product_id equals product.product_id
                               join u in db.admin_Users on ctxh.username_id equals u.username_id
                               where ctxh.xuathang_code == nh.xuathang_code
                               select new
                               {
                                   product.product_id,
                                   product.product_title,
                                   ctxh.xuathang_chitiet_id,
                                   ctxh.xuathang_giaxuat,
                                   ctxh.xuathang_chitiet_soluong,
                                   ctxh.xuathang_thanhtien,
                                   u.username_fullname
                               });
                foreach (var item in getctnh)
                {
                    int tongtien = (Convert.ToInt32(item.xuathang_chitiet_soluong) * Convert.ToInt32(item.xuathang_giaxuat));
                    DataRow row = dtProduct.NewRow();
                    row["xuathang_createdate"] = nh.xuathang_createdate;
                    row["product_id"] = item.product_id;
                    row["product_title"] = item.product_title;
                    row["xuathang_chitiet_soluong"] = item.xuathang_chitiet_soluong;
                    row["xuathang_giaxuat"] = double.Parse(item.xuathang_giaxuat.ToString()).ToString("#,###", cul.NumberFormat);
                    row["xuathang_thanhtien"] = double.Parse(tongtien.ToString()).ToString("#,###", cul.NumberFormat);
                    row["username_fullname"] = item.username_fullname;
                    dtProduct.Rows.Add(row);
                    tt += tongtien;
                }
            }
            rpNhapHang.DataSource = dtProduct;
            rpNhapHang.DataBind();
            TongTien = double.Parse(tt.ToString()).ToString("#,###", cul.NumberFormat) + " VND";
            div_DoanhThu.Visible = true;
        }

    }
}