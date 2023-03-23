using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_sanpham_module_XuatHang : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int stt = 1;
    private int SoLuong;
    protected void Page_Load(object sender, EventArgs e)
    {
        loaddata();
    }
    private void loaddata()
    {
        var getSanPham = from hd in db.tbHoaDonBanHangs
                         join hdct in db.tbHoaDonBanHangChiTiets on hd.hoadon_id equals hdct.hoadon_id
                         join pr in db.tbProducts on hdct.product_id equals pr.product_id
                         select new
                         {
                             hd.hoadon_id,
                             pr.product_id,
                             pr.product_title,
                             pr.product_image,
                             pr.product_soluong,
                         };
        rpNhapHang.DataSource = getSanPham;
        rpNhapHang.DataBind();
    }

    protected void btnluu_ServerClick(object sender, EventArgs e)
    {
        tbProduct update = db.tbProducts.Where(x => x.product_id == Convert.ToInt32(txtid.Value)).FirstOrDefault();
        if (txtban.Value != "")
        {
            update.product_soluong = Convert.ToInt32(txtsl.Value) - Convert.ToInt32(txtban.Value);
            db.SubmitChanges(); loaddata();
            alert.alert_Success(Page, "Cập nhật thành công", "");
        }
        else
        {
            alert.alert_Error(Page, "Dữ liệu trống", "");
        }
    }
}