using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_sanpham_module_XuatHangUpdate : System.Web.UI.Page
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
            loadListSanPham();
            if (!IsPostBack)
            {
                int _id = Convert.ToInt32(RouteData.Values["id"]);
                var getdataid = (from nh in db.tbXuatHangs where nh.xuathang_id == _id select nh).SingleOrDefault();
                txtMaXuat.Value = getdataid.xuathang_code;
                txtNhanVien.Value = (from u in db.admin_Users where u.username_id == getdataid.username_id select u).SingleOrDefault().username_fullname;
                txtNgayNhap.Value = getdataid.xuathang_createdate.Value.ToString("dd/MM/yyyy").Replace(' ', 'T');
                txtNoiDung.Value = getdataid.xuathang_content;
                // nếu sestion chi tiet mà co du lieu roi thi khong cho chạy vao nua
                loaddatatable();
                //dtProduct = (DataTable)Session["spChiTiet"];
                //rp_grvChiTiet.DataSource = dtProduct;
                //rp_grvChiTiet.DataBind();
                if (RouteData.Values["id"] != null)
                {
                    //get mã tự tăng của nhập hàng chi tiết
                    var getctnh = (from ctnh in db.tbXuatHang_ChiTiets
                                   join product in db.tbProducts on ctnh.product_id equals product.product_id
                                   where getdataid.xuathang_id == ctnh.xuathang_id
                                   select new
                                   {
                                       product.product_id,
                                       product.product_title,
                                       ctnh.xuathang_chitiet_id,
                                       ctnh.xuathang_chitiet_soluong,
                                   });
                    //loaddata ra datatable
                    foreach (var item in getctnh)
                    {
                        //nếu sp này đã có trong datatable thì cộng dồn số lượng lại 
                        //ngược lại thì add bình thường vào data row
                        int product_id = Convert.ToInt32(item.product_id);
                        DataRow[] row_id = dtProduct.Select("product_id = '" + product_id + "'");
                        if (row_id.Length != 0)
                        {
                            foreach (DataRow row in dtProduct.Rows)
                            {
                                string id = row["product_id"].ToString();
                                if (id == product_id.ToString())
                                {
                                    row.SetField("xuathang_chitiet_soluong", Convert.ToInt32(row["xuathang_chitiet_soluong"]) + item.xuathang_chitiet_soluong);
                                }
                            }
                        }
                        else
                        {
                            DataRow row = dtProduct.NewRow();
                            row["product_id"] = item.product_id;
                            row["product_title"] = item.product_title;
                            row["xuathang_chitiet_soluong"] = item.xuathang_chitiet_soluong;
                            dtProduct.Rows.Add(row);
                        }
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
    public void loadListSanPham()
    {
        var list = from sp in db.tbProducts
                   join nh in db.tbNhapHang_ChiTiets on sp.product_id equals nh.product_id
                   where sp.hidden == false && nh.active == true
                   group sp by sp.product_id into g
                   select new
                   {
                       g.Key,
                       product_id = g.First().product_id,
                       product_title = g.First().product_title,
                       //kh.tonkho_soluong
                   };
        grvList.DataSource = list;
        grvList.DataBind();
    }
    public void loaddatatable()
    {
        if (dtProduct == null)
        {
            dtProduct = new DataTable();
            dtProduct.Columns.Add("product_id", typeof(int));
            dtProduct.Columns.Add("product_title", typeof(string));
            dtProduct.Columns.Add("xuathang_chitiet_soluong", typeof(int));
            //dtProduct.Columns.Add("nhaphang_gianhap", typeof(int));
            //dtProduct.Columns.Add("nhaphang_giaban", typeof(int));
            //dtProduct.Columns.Add("nhaphang_thanhtien", typeof(int));
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
                alert.alert_Warning(Page, "Sản phẩm này đã có trong danh sách xuất hàng", "");
            }
            else
            {
                DataRow row = dtProduct.NewRow();
                row["product_id"] = checkSanPham.product_id;
                row["product_title"] = checkSanPham.product_title;
                row["xuathang_chitiet_soluong"] = 1;
                //row["nhaphang_gianhap"] = 0;
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
        //lấy thông tin của tk đang nhập
        var getuser = (from u in db.admin_Users
                       where u.username_username == Request.Cookies["UserName"].Value
                       select u).FirstOrDefault();
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
                    int _id = Convert.ToInt32(RouteData.Values["id"]);
                    var getdataid = (from nh in db.tbXuatHangs where nh.xuathang_id == _id select nh).SingleOrDefault();
                    if (dtProduct.Rows.Count > 0)
                    {
                        // -------------------------thêm vào bảng chi tiết ----------------------------
                        foreach (DataRow row in dtProduct.Rows)
                        {
                            tbXuatHang_ChiTiet insertXHCT = new tbXuatHang_ChiTiet();
                            /* kiểm tra product này đã có trong bảng xuất hàng chi tiết chưa
                             * nếu có rồi thì update lại sl 
                             */
                            var checkprdt = (from ctnh in db.tbXuatHang_ChiTiets
                                             where ctnh.xuathang_id == Convert.ToInt32(RouteData.Values["id"])
                                             && ctnh.product_id == Convert.ToInt32(row["product_id"])
                                             orderby ctnh.xuathang_chitiet_id descending
                                             select ctnh);
                            var checkNH = from sp in db.tbNhapHang_ChiTiets
                                          where sp.product_id == Convert.ToInt32(row["product_id"])
                                          && sp.active == true
                                          select sp;
                            if (checkprdt.Count() > 0)
                            {
                                int slcu = 0;
                                foreach (var sl in checkprdt)
                                {
                                    slcu += Convert.ToInt32(sl.xuathang_chitiet_soluong);
                                }
                                foreach (var sp in checkNH)
                                {
                                    int slx = Convert.ToInt32(row["xuathang_chitiet_soluong"]) - slcu;
                                    int slton = Convert.ToInt32(sp.nhaphang_chitiet_soluong - sp.nhaphang_soluong_daxuat);
                                    if (slx <= slton)
                                    {

                                        if (checkprdt.FirstOrDefault().nhaphang_chitiet_id == sp.nhaphang_chitiet_id)
                                        {
                                            //update lại sl xuất kho trong bảng XHCT
                                            checkprdt.FirstOrDefault().xuathang_chitiet_soluong = Convert.ToInt32(checkprdt.FirstOrDefault().xuathang_chitiet_soluong) + slx;
                                            db.SubmitChanges();
                                            //updata lại sl và trạng thái (nếu có) trong bảng NHCT
                                            sp.nhaphang_soluong_daxuat = Convert.ToInt32(checkprdt.FirstOrDefault().xuathang_chitiet_soluong);
                                            db.SubmitChanges();
                                            if (sp.nhaphang_soluong_daxuat == sp.nhaphang_chitiet_soluong)
                                            {
                                                sp.active = false;
                                                db.SubmitChanges();
                                            }
                                        }
                                        else
                                        {
                                            insertXHCT.xuathang_id = _id;
                                            insertXHCT.product_id = Convert.ToInt32(row["product_id"]);
                                            insertXHCT.xuathang_chitiet_soluong = slx;
                                            insertXHCT.xuathang_giaxuat = Convert.ToInt32(sp.nhaphang_gianhap);
                                            insertXHCT.xuathang_thanhtien = slx * Convert.ToInt32(sp.nhaphang_gianhap);
                                            insertXHCT.username_id = getuser.username_id;
                                            insertXHCT.xuathang_code = getdataid.xuathang_code;
                                            insertXHCT.nhaphang_chitiet_id = sp.nhaphang_chitiet_id;
                                            db.tbXuatHang_ChiTiets.InsertOnSubmit(insertXHCT);
                                            db.SubmitChanges();
                                            //cập nhật lại sl đã xuất trong bảng nhập hàng
                                            sp.nhaphang_soluong_daxuat = Convert.ToInt32(sp.nhaphang_soluong_daxuat + insertXHCT.xuathang_chitiet_soluong);
                                            db.SubmitChanges();
                                            if (sp.nhaphang_chitiet_soluong == sp.nhaphang_soluong_daxuat)
                                            {
                                                sp.active = false;
                                                db.SubmitChanges();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        /* khi sl xuất > sl dư thì update với slxuat bằng sldu và tính ra số dư xuất hiện tại
                                         * và insert thêm 1 bản ghi mới với số lượng còn dư trong datarow
                                         */
                                        checkprdt.FirstOrDefault().xuathang_chitiet_soluong = (Convert.ToInt32(row["xuathang_chitiet_soluong"]) - slton);
                                        db.SubmitChanges();
                                        sp.nhaphang_soluong_daxuat = Convert.ToInt32(sp.nhaphang_chitiet_soluong);
                                        sp.active = false;
                                        db.SubmitChanges();
                                        row["xuathang_chitiet_soluong"] = Convert.ToInt32(row["xuathang_chitiet_soluong"]) - checkprdt.FirstOrDefault().xuathang_chitiet_soluong;
                                    }
                                }
                            }
                            else
                            {
                                //---- trường hợp đây là sp được thêm mớ trong đơn hàng xuất khi updata
                                foreach (var item in checkNH)
                                {
                                    var checksl = from s in db.tbXuatHang_ChiTiets
                                                  where s.product_id == Convert.ToInt32(row["product_id"])
                                                  && s.xuathang_id == _id
                                                  orderby s.xuathang_chitiet_id descending
                                                  select s;
                                    if (checksl.Count() > 0)
                                    {
                                        int slx = Convert.ToInt32(row["xuathang_chitiet_soluong"]);
                                        int slton = Convert.ToInt32(item.nhaphang_chitiet_soluong - item.nhaphang_soluong_daxuat);
                                        if (slx <= slton)
                                        {
                                            insertXHCT.xuathang_id = _id;
                                            insertXHCT.product_id = Convert.ToInt32(row["product_id"]);
                                            insertXHCT.xuathang_chitiet_soluong = slx;
                                            insertXHCT.xuathang_giaxuat = Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.xuathang_thanhtien = slx * Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.username_id = getuser.username_id;
                                            insertXHCT.xuathang_code = getdataid.xuathang_code;
                                            insertXHCT.nhaphang_chitiet_id = item.nhaphang_chitiet_id;
                                            db.tbXuatHang_ChiTiets.InsertOnSubmit(insertXHCT);
                                            db.SubmitChanges();
                                            //update lại sl tồn trong đơn NHCT
                                            item.nhaphang_soluong_daxuat = Convert.ToInt32(item.nhaphang_soluong_daxuat + slx);
                                            db.SubmitChanges();
                                            if (item.nhaphang_chitiet_soluong == item.nhaphang_soluong_daxuat)
                                            {
                                                item.active = false;
                                                db.SubmitChanges();
                                            }
                                        }
                                        else
                                        {
                                            insertXHCT.xuathang_id = _id;
                                            insertXHCT.product_id = Convert.ToInt32(row["product_id"]);
                                            insertXHCT.xuathang_chitiet_soluong = slton;
                                            insertXHCT.xuathang_giaxuat = Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.xuathang_thanhtien = slton * Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.username_id = getuser.username_id;
                                            insertXHCT.xuathang_code = getdataid.xuathang_code;
                                            insertXHCT.nhaphang_chitiet_id = item.nhaphang_chitiet_id;
                                            db.tbXuatHang_ChiTiets.InsertOnSubmit(insertXHCT);
                                            db.SubmitChanges();
                                            //update lại sl đã xuất và trạng thái của xhct
                                            item.nhaphang_soluong_daxuat = item.nhaphang_chitiet_soluong;
                                            item.active = false;
                                            db.SubmitChanges();
                                            row["xuathang_chitiet_soluong"] = Convert.ToInt32(row["xuathang_chitiet_soluong"]) - checkprdt.FirstOrDefault().xuathang_chitiet_soluong;
                                        }
                                    }
                                    else
                                    {
                                        insertXHCT.xuathang_id = _id;
                                        insertXHCT.product_id = Convert.ToInt32(row["product_id"]);
                                        insertXHCT.xuathang_chitiet_soluong = Convert.ToInt32(row["xuathang_chitiet_soluong"]);
                                        insertXHCT.xuathang_giaxuat = Convert.ToInt32(item.nhaphang_gianhap);
                                        insertXHCT.xuathang_thanhtien = Convert.ToInt32(row["xuathang_chitiet_soluong"]) * Convert.ToInt32(item.nhaphang_gianhap);
                                        insertXHCT.username_id = getuser.username_id;
                                        insertXHCT.xuathang_code = getdataid.xuathang_code;
                                        insertXHCT.nhaphang_chitiet_id = item.nhaphang_chitiet_id;
                                        db.tbXuatHang_ChiTiets.InsertOnSubmit(insertXHCT);
                                        db.SubmitChanges();
                                        //update lại sl đã xuất và trạng thái của xhct
                                        item.nhaphang_soluong_daxuat += Convert.ToInt32(row["xuathang_chitiet_soluong"]);
                                        db.SubmitChanges();
                                        if (item.nhaphang_chitiet_soluong == item.nhaphang_soluong_daxuat)
                                        {
                                            item.active = false;
                                            db.SubmitChanges();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    getdataid.xuathang_content = txtNoiDung.Value;
                    db.SubmitChanges();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xuất hàng thành công!', '','success').then(function(){window.location = '/admin-quan-ly-xuat-hang';})", true);
                }
                catch { }
            }
        }
    }

    protected void NhapHang_ServerClick(object sender, EventArgs e)
    {
        try
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
                        //kiểm tra xem sl tồn kho của sp này có nhiều hơn sl input không
                        //nếu tồn kho ít hơn thì báo lỗi
                        var checknh = from sp in db.tbNhapHang_ChiTiets
                                      where sp.product_id == _id
                                      && sp.active == true
                                      select sp;
                        int tongton = 0;
                        foreach (var item in checknh)
                        {
                            tongton += Convert.ToInt32(item.nhaphang_chitiet_soluong) - Convert.ToInt32(item.nhaphang_soluong_daxuat);
                        }
                        int slcu = 0;
                        //lấy sl xuất cũ nếu đã có trong database
                        var checksl = from s in db.tbXuatHang_ChiTiets
                                      where s.product_id == _id
                                      && s.xuathang_id == Convert.ToInt32(RouteData.Values["id"])
                                      select s;
                        if (checksl.Count() > 0)
                        {
                            foreach (var sl in checksl)
                            {
                                slcu += Convert.ToInt32(sl.xuathang_chitiet_soluong);
                            }
                        }
                        if ((tongton) < (Convert.ToInt32(txt_SoLuong.Value) - slcu))
                        {
                            alert.alert_Warning(Page, "Số lượng sản phẩm này trong kho không đủ để xuất", "Vui lòng kiểm tra lại");
                        }
                        else
                        {
                            // lưu data bằng input đầu vào
                            row.SetField("xuathang_chitiet_soluong", txt_SoLuong.Value);
                            //row.SetField("nhaphang_gianhap", txt_GiaTien.Value);
                            //row.SetField("nhaphang_giaban", txt_GiaBan.Value);
                            //row.SetField("nhaphang_thanhtien", txt_TongTien.Value);
                            rp_grvChiTiet.DataSource = dtProduct;
                            rp_grvChiTiet.DataBind();
                        }
                    }
                }
            }
        }
        catch (Exception) { }
    }

    protected void btnXoa_ServerClick(object sender, EventArgs e)
    {
        try
        {
            int _id = Convert.ToInt32(txt_ID.Value);
            dtProduct = (DataTable)Session["spChiTiet"];
            foreach (DataRow row in dtProduct.Rows)
            {
                // kiểm tra sp đã có trong csdl chưa
                //nếu có thì xóa trong bảng xuất hàng và trả lại sl xuất của sp này về cho bảng nhập hàng
                var checkprdt = from ctxh in db.tbXuatHang_ChiTiets
                                where ctxh.xuathang_id == Convert.ToInt32(RouteData.Values["id"])
                                && ctxh.product_id == Convert.ToInt32(txt_ID.Value)
                                select ctxh;
                if (checkprdt.Count() > 0)
                {
                    foreach (var xh in checkprdt)
                    {
                        var checkNH = (from nh in db.tbNhapHang_ChiTiets
                                       where nh.nhaphang_chitiet_id == Convert.ToInt32(xh.nhaphang_chitiet_id)
                                       select nh).FirstOrDefault();
                        checkNH.nhaphang_soluong_daxuat = checkNH.nhaphang_soluong_daxuat - xh.xuathang_chitiet_soluong;
                        db.SubmitChanges();
                        if (checkNH.nhaphang_chitiet_soluong != checkNH.nhaphang_soluong_daxuat)
                        {
                            checkNH.active = true;
                            db.SubmitChanges();
                        }
                        db.tbXuatHang_ChiTiets.DeleteOnSubmit(xh);
                        db.SubmitChanges();
                    }
                }
                else { }
                string product_id = row["product_id"].ToString();
                if (product_id == _id.ToString())
                {
                    dtProduct.Rows.Remove(row);
                    Session["spChiTiet"] = dtProduct;
                    break;
                }
            }
            rp_grvChiTiet.DataSource = dtProduct;
            rp_grvChiTiet.DataBind();
            loadListSanPham();
        }
        catch (Exception)
        {

        }
    }
}