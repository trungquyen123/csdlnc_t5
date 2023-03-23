using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_hoadon_module_ChiTietHoaDon : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        var getDonHang = from hdct in db.tbHoaDonBanHangChiTiets
                         join pr in db.tbProducts on hdct.product_id equals pr.product_id
                         join prc in db.tbProductCates on pr.productcate_id equals prc.productcate_id
                         where hdct.hoadon_id == Convert.ToInt32(RouteData.Values["_idDonHang"])
                         select new
                         {
                             pr.product_image,
                             pr.product_title,
                             pr.product_price,
                             prc.productcate_title,
                             t = Convert.ToInt32(pr.product_price),
                         };
        rpChiTietDonHang.DataSource = getDonHang;
        rpChiTietDonHang.DataBind();
    }
}