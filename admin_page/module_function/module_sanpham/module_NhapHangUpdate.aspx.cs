using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_NhapHangUpdate : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public string adminName;
    DataTable dtProduct;
    public int stt = 1;
    int masp;
    string tensp;
    int sl;
    int gianhap;
    int thanhtien;
    cls_Alert alert = new cls_Alert();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            var list = from sp in db.tbProducts
                       where sp.hidden == false
                       select new
                       {
                           sp.product_id,
                           sp.product_title,
                           //kh.tonkho_soluong
                       };
            grvList.DataSource = list;
            grvList.DataBind();
            if (!IsPostBack)
            {
                int _id = Convert.ToInt32(RouteData.Values["id"]);
                var getdataid = (from nh in db.tbNhapHangs where nh.nhaphang_id == _id select nh).SingleOrDefault();
                txtMaNhap.Value = getdataid.nhaphang_code;
                txtNhanVien.Value = (from u in db.admin_Users where u.username_id == getdataid.username_id select u).SingleOrDefault().username_fullname;
                txtNgayNhap.Value = getdataid.nhaphang_createdate.Value.ToString("dd/MM/yyyy").Replace(' ', 'T');
                txtNoiDung.Value = getdataid.nhaphang_content;
                // nếu sestion chi tiet mà co du lieu roi thi khong cho chạy vao nua
                loaddatatable();
                //dtProduct = (DataTable)Session["spChiTiet"];
                //rp_grvChiTiet.DataSource = dtProduct;
                //rp_grvChiTiet.DataBind();
                if (RouteData.Values["id"] != null)
                {
                    //get mã tự tăng của nhập hàng chi tiết
                    var getctnh = (from ctnh in db.tbNhapHang_ChiTiets
                                   join product in db.tbProducts on ctnh.product_id equals product.product_id
                                   where getdataid.nhaphang_code == ctnh.nhaphang_code
                                   select new
                                   {
                                       product.product_id,
                                       product.product_title,
                                       product.product_price,
                                       ctnh.nhaphang_chitiet_id,
                                       ctnh.nhaphang_gianhap,
                                       ctnh.nhaphang_chitiet_soluong,
                                       ctnh.nhaphang_thanhtien
                                   });
                    //loaddata ra datatable
                    foreach (var item in getctnh)
                    {
                        DataRow row = dtProduct.NewRow();
                        row["product_id"] = item.product_id;
                        row["product_title"] = item.product_title;
                        row["nhaphang_chitiet_soluong"] = item.nhaphang_chitiet_soluong;
                        row["nhaphang_gianhap"] = item.nhaphang_gianhap;
                        //row["nhaphang_giaban"] = item.product_price;
                        //row["nhaphang_thanhtien"] = item.nhaphang_thanhtien;
                        dtProduct.Rows.Add(row);
                    };
                    Session["spChiTiet"] = dtProduct;
                    rp_grvChiTiet.DataSource = dtProduct;
                    rp_grvChiTiet.DataBind();
                }
            }
        }
        else
        {
            Response.Redirect("/admin-login");
        }
    }

    public void loaddatatable()
    {
        if (dtProduct == null)
        {
            dtProduct = new DataTable();
            dtProduct.Columns.Add("product_id", typeof(int));
            dtProduct.Columns.Add("product_title", typeof(string));
            dtProduct.Columns.Add("nhaphang_chitiet_soluong", typeof(int));
            dtProduct.Columns.Add("nhaphang_gianhap", typeof(int));
            //    dtProduct.Columns.Add("nhaphang_giaban", typeof(int));
            //    dtProduct.Columns.Add("nhaphang_thanhtien", typeof(int));
        }
    }
    protected void btnChiTiet_ServerClick(object sender, EventArgs e)
    {
        //kiểm tra add 2 lần có thêm vào gridview hay không
        int _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "product_id" }));
        var checkSanPham = (from sp in db.tbProducts where sp.product_id == _id select sp).SingleOrDefault();
        if (Session["spChiTiet"] != null)
        {
            dtProduct = (DataTable)Session["spChiTiet"];
            DataRow[] row_id = dtProduct.Select("product_id = '" + _id + "'");
            if (row_id.Length != 0)
            {
                alert.alert_Warning(Page, "Sản phẩm này đã có trong danh sách nhập", "");
            }
            else
            {
                DataRow row = dtProduct.NewRow();
                row["product_id"] = checkSanPham.product_id;
                row["product_title"] = checkSanPham.product_title;
                row["nhaphang_chitiet_soluong"] = 1;
                row["nhaphang_gianhap"] = 0;
                //row["nhaphang_giaban"] = 0;
                //row["nhaphang_thanhtien"] = Convert.ToInt32(row["nhaphang_chitiet_soluong"]) * Convert.ToInt32(row["nhaphang_gianhap"]);
                dtProduct.Rows.Add(row);
                Session["spChiTiet"] = dtProduct;
            }
        }
        // insert lưu vào database

        //End inseert lưu vào database
        rp_grvChiTiet.DataSource = dtProduct;
        rp_grvChiTiet.DataBind();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        dtProduct = (DataTable)Session["spChiTiet"];
        try
        {
            if (dtProduct.Rows.Count <= 0 || dtProduct == null) alert.alert_Warning(Page, "Bạn chưa có sản phẩm nào", "");
        }
        catch { }
        if (dtProduct == null) alert.alert_Warning(Page, "Bạn chưa có sản phẩm nào", "");
        else
        {
            if (txtNoiDung.Value == "") alert.alert_Warning(Page, "Bạn chưa nhập nội dung", "");
            else
            {
                // lưu dữ liệu vào bảng nhập hàng
                try
                {
                    if (dtProduct.Rows.Count > 0)
                    {
                        // -------------------------thêm vào bảng chi tiết ----------------------------
                        foreach (DataRow row in dtProduct.Rows)
                        {
                            // kiểm tra product này đã có trong bảng chi tiết
                            var checkprdt = (from ctnh in db.tbNhapHang_ChiTiets
                                             where ctnh.nhaphang_code == txtMaNhap.Value
                                             where ctnh.product_id == Convert.ToInt32(row["product_id"])
                                             select ctnh);
                            //checkprdt kho hàng
                            var checkkh = (from kh in db.tbProduct_TonKhos
                                           where kh.product_id == Convert.ToInt32(row["product_id"])
                                           select kh).FirstOrDefault();
                            //nếu sách này đã được nhập trước đó thì cập nhật lại
                            if (checkprdt.Count() > 0)
                            {
                                //update lại sl trong kho
                                //nếu "số lượng lúc thay đổi" nhỏ hơn "số lượng ban đầu trong bảng chi tiết" thì trừ đi
                                if (checkprdt.FirstOrDefault().nhaphang_chitiet_soluong > Convert.ToInt32(row["nhaphang_chitiet_soluong"]))
                                {
                                    checkkh.tonkho_soluong = checkkh.tonkho_soluong - (checkprdt.FirstOrDefault().nhaphang_chitiet_soluong - Convert.ToInt32(row["nhaphang_chitiet_soluong"]));
                                    db.SubmitChanges();
                                }
                                //nếu "số lượng lúc thay đổi" nhỏ hơn "số lượng ban đầu trong bảng chi tiết" thì cộng lại
                                else if (checkprdt.FirstOrDefault().nhaphang_chitiet_soluong < Convert.ToInt32(row["nhaphang_chitiet_soluong"]))
                                {
                                    checkkh.tonkho_soluong = checkkh.tonkho_soluong + (Convert.ToInt32(row["nhaphang_chitiet_soluong"]) - checkprdt.FirstOrDefault().nhaphang_chitiet_soluong);
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    checkkh.tonkho_soluong = checkkh.tonkho_soluong + checkprdt.FirstOrDefault().nhaphang_chitiet_soluong;
                                    db.SubmitChanges();
                                }
                                //update vào bảng chi tiết nhập hàng
                                checkprdt.FirstOrDefault().nhaphang_chitiet_soluong = Convert.ToInt32(row["nhaphang_chitiet_soluong"]);
                                checkprdt.FirstOrDefault().nhaphang_gianhap = Convert.ToInt32(row["nhaphang_gianhap"]);
                                checkprdt.FirstOrDefault().nhaphang_thanhtien = Convert.ToInt32(row["nhaphang_chitiet_soluong"]) * Convert.ToInt32(row["nhaphang_gianhap"]);
                                db.SubmitChanges();
                            }
                            else
                            {
                                // nếu chua thì mình insert vào
                                int id = Convert.ToInt32(RouteData.Values["id"]);
                                var getNH = (from nh in db.tbNhapHangs where nh.nhaphang_id == id select nh).SingleOrDefault();
                                tbNhapHang_ChiTiet insertNHCT = new tbNhapHang_ChiTiet();
                                insertNHCT.nhaphang_id = getNH.nhaphang_id;
                                insertNHCT.product_id = Convert.ToInt32(row["product_id"]);
                                insertNHCT.nhaphang_code = getNH.nhaphang_code;
                                insertNHCT.nhaphang_chitiet_soluong = Convert.ToInt32(row["nhaphang_chitiet_soluong"]);
                                insertNHCT.nhaphang_gianhap = Convert.ToInt32(row["nhaphang_gianhap"]);
                                insertNHCT.nhaphang_thanhtien = Convert.ToInt32(row["nhaphang_chitiet_soluong"]) * Convert.ToInt32(row["nhaphang_gianhap"]);
                                insertNHCT.username_id = getNH.username_id;
                                db.tbNhapHang_ChiTiets.InsertOnSubmit(insertNHCT);
                                db.SubmitChanges();
                                dtProduct = (DataTable)Session["spChiTiet"];
                                rp_grvChiTiet.DataSource = dtProduct;
                                rp_grvChiTiet.DataBind();
                                //-----------------thêm vào bảng kho hàng---------------------------
                                //lưu vào bảng kho hàng
                                if (checkkh == null)
                                {
                                    tbProduct_TonKho insert_slsp = new tbProduct_TonKho();
                                    insert_slsp.product_id = Convert.ToInt32(row["product_id"]);
                                    insert_slsp.tonkho_soluong = Convert.ToInt32(row["nhaphang_chitiet_soluong"]);
                                    db.tbProduct_TonKhos.InsertOnSubmit(insert_slsp);
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    checkkh.tonkho_soluong = checkkh.tonkho_soluong + Convert.ToInt32(row["nhapsach_chitiet_soluong"]);
                                    db.SubmitChanges();
                                }
                            }
                        }
                    }
                    int _id = Convert.ToInt32(RouteData.Values["id"]);
                    var getdataid = (from nh in db.tbNhapHangs where nh.nhaphang_id == _id select nh).SingleOrDefault();
                    getdataid.nhaphang_content = txtNoiDung.Value;
                    db.SubmitChanges();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Nhập hàng thành công!', '','success').then(function(){window.location = '/admin-quan-ly-nhap-hang';})", true);
                }
                catch { }
            }
        }
    }

    protected void NhapHang_ServerClick(object sender, EventArgs e)
    {
        // kiểm tra id
        int _id = Convert.ToInt32(txt_ID.Value);
        if (Session["spChiTiet"] != null)
        {
            dtProduct = (DataTable)Session["spChiTiet"];
            // chạy foreach để lặp lại các row 
            foreach (DataRow row in dtProduct.Rows)
            {
                string product_id = row["product_id"].ToString();
                if (product_id == _id.ToString())
                {
                    // lưu data bằng input đầu vào
                    row.SetField("nhaphang_chitiet_soluong", txt_SoLuong.Value);
                    row.SetField("nhaphang_gianhap", txt_GiaTien.Value);
                    //row.SetField("nhaphang_giaban", txt_GiaBan.Value);
                    //row.SetField("nhaphang_thanhtien", txt_TongTien.Value);
                    rp_grvChiTiet.DataSource = dtProduct;
                    rp_grvChiTiet.DataBind();
                }
            }
        }
    }

    protected void btnXoa_ServerClick(object sender, EventArgs e)
    {
        try
        {
            int _id = Convert.ToInt32(txt_ID.Value);
            dtProduct = (DataTable)Session["spChiTiet"];
            foreach (DataRow row in dtProduct.Rows)
            {
                //---Xóa trong datatable
                string product_id = row["product_id"].ToString();
                if (product_id == _id.ToString())
                {
                    dtProduct.Rows.Remove(row);
                    Session["spChiTiet"] = dtProduct;
                    break;
                }
                /* cập nhật lại sl của sp này trong kho 
                 * và xóa sp này trong table nhập hàng chi tiết
                 */
                var checkprdt = from ctnh in db.tbNhapHang_ChiTiets
                                where ctnh.nhaphang_code == txtMaNhap.Value
                                && ctnh.product_id == _id
                                select ctnh;
                var checkkh = (from kh in db.tbProduct_TonKhos
                               where kh.product_id == _id
                               select kh).FirstOrDefault();
                if (checkprdt.Count() > 0)
                {
                    //update lại sl trong kho
                    checkkh.tonkho_soluong = checkkh.tonkho_soluong - checkprdt.FirstOrDefault().nhaphang_chitiet_soluong;
                    db.SubmitChanges();
                    db.tbNhapHang_ChiTiets.DeleteOnSubmit(checkprdt.First());
                    db.SubmitChanges();
                }
                else { }
                // kiểm tra khi xóa thì sẽ  xóa luốn số lượng trong kho hàng đi.

            }
            rp_grvChiTiet.DataSource = dtProduct;
            rp_grvChiTiet.DataBind();
        }
        catch (Exception) { }
    }
}