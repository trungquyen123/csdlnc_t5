using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_NhapHang : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int stt = 1;
    private int _id;
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
                             pr.product_price,
                             pr.product_promotions
                         };
        rpNhapHang.DataSource = getSanPham;
        rpNhapHang.DataBind();
    }



    protected void btnluu_ServerClick(object sender, EventArgs e)
    {
        tbProduct update = db.tbProducts.Where(x => x.product_id == Convert.ToInt32(txtid.Value)).FirstOrDefault();
        if (txtsl.Value != "")
        {
            update.product_soluong = Convert.ToInt32(txtsl.Value);
        }
        if (txtgia.Value != "")
        {
            update.product_price = Convert.ToInt32(txtgia.Value);
        }
        if (txtgiamgia.Value != "")
        {
            update.product_promotions = Convert.ToInt32(txtgiamgia.Value);
        }
        db.SubmitChanges(); loaddata();
        alert.alert_Success(Page, "Cập nhật thành công", "");

    }
}