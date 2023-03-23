using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_sanpham_module_XuatHangInsert : System.Web.UI.Page
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
                       join nh in db.tbNhapHang_ChiTiets on sp.product_id equals nh.product_id
                       where sp.hidden == false && nh.active==true
                       group sp by sp.product_id into g
                       select new
                       {
                           g.Key,
                           product_id = g.First().product_id,
                           product_title =g.First().product_title,
                           //kh.tonkho_soluong
                       };
            grvList.DataSource = list;
            grvList.DataBind();
            //lấy thông tin của tk đang nhập
            var getuser = (from u in db.admin_Users
                           where u.username_id == 1
                           select u).FirstOrDefault();
            txtNhanVien.Value = getuser.username_fullname;
            string matutang = Matutang();
            txtMaXuat.Value = matutang;
            loaddatatable();
            dtProduct = (DataTable)Session["spChiTiet"];
            rp_grvChiTiet.DataSource = dtProduct;
            rp_grvChiTiet.DataBind();
            //hiện ngày tháng 
            //var new_day = DateTime.Now.ToString("dd/MM/yyyy");
            txtNgayNhap.Value = DateTime.Now.ToString("dd/MM/yyyy");
        }
        else
        {
            Response.Redirect("/admin-login");
        }
    }
    //Hàm tự tăng
    public string Matutang()
    {
        int year = DateTime.Now.Year;
        var list = from nk in db.tbXuatHangs select nk;
        string s = "XH";
        if (list.Count() <= 0)
            s = "XH00001";
        else
        {
            var list1 = from nk in db.tbXuatHangs orderby nk.xuathang_code descending select nk;
            string chuoi = list1.First().xuathang_code;
            int k;
            k = Convert.ToInt32(chuoi.Substring(2, 5));
            k = k + 1;
            if (k < 10) s = s + "0000";
            else if (k < 100)
                s = s + "000";
            else if (k < 1000)
                s = s + "00";
            else if (k < 10000)
                s = s + "0";
            s = s + k.ToString();
        }
        return s;
    }
    public void loaddatatable()
    {
        try
        {
            if (dtProduct == null)
            {
                dtProduct = new DataTable();
                dtProduct.Columns.Add("product_id", typeof(int));
                dtProduct.Columns.Add("product_title", typeof(string));
                dtProduct.Columns.Add("xuathang_chitiet_soluong", typeof(int));
                //dtProduct.Columns.Add("xuathang_giaxuat", typeof(int));
                //dtProduct.Columns.Add("nhaphang_giaban", typeof(int));
                //dtProduct.Columns.Add("nhaphang_thanhtien", typeof(int));
            }
        }
        catch { }
    }
    protected void btnChiTiet_ServerClick(object sender, EventArgs e)
    {
        try
        {
            //kiểm tra add 2 lần có thêm vào gridview hay không
            int _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "product_id" }));
            var checkSanPham = (from sp in db.tbProducts where sp.product_id == _id select sp).SingleOrDefault();
            var getGiaNhap = from s in db.tbNhapHang_ChiTiets
                             where s.product_id == checkSanPham.product_id
                             select s;
            int giaxuat = 0;
            int tongtien = 0;
            int tongsl = 0;
            foreach (var item in getGiaNhap)
            {
                tongtien += Convert.ToInt32(item.nhaphang_thanhtien);
                tongsl += Convert.ToInt32(item.nhaphang_chitiet_soluong);
            }
            giaxuat = tongtien / tongsl;
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
                    //row["xuathang_giaxuat"] = giaxuat;
                    //row["nhaphang_giaban"] = 0;
                    //row["nhaphang_thanhtien"] = Convert.ToInt32(row["nhaphang_chitiet_soluong"]) * Convert.ToInt32(row["nhaphang_gianhap"]);
                    dtProduct.Rows.Add(row);
                    Session["spChiTiet"] = dtProduct;
                }
            }
            else
            {
                loaddatatable();
                DataRow row = dtProduct.NewRow();
                row["product_id"] = checkSanPham.product_id;
                row["product_title"] = checkSanPham.product_title;
                row["xuathang_chitiet_soluong"] = 1;
                //row["xuathang_giaxuat"] = giaxuat;
                //row["nhaphang_giaban"] = 0;
                //row["nhaphang_thanhtien"] = Convert.ToInt32(row["nhaphang_chitiet_soluong"]) * Convert.ToInt32(row["nhaphang_gianhap"]);
                dtProduct.Rows.Add(row);
                Session["spChiTiet"] = dtProduct;
            }
            rp_grvChiTiet.DataSource = dtProduct;
            rp_grvChiTiet.DataBind();
            btnSave.Visible = true;
        }
        catch { }
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
            if (txtNoiDung.Value == "")
            {
                alert.alert_Warning(Page, "Bạn chưa nhập nội dung", "");
            }
            else
            {
                // lưu dữ liệu vào bảng nhập hàng
                try
                {
                    if (dtProduct.Rows.Count > 0)
                    {
                        tbXuatHang insertXH = new tbXuatHang();
                        insertXH.xuathang_code = txtMaXuat.Value;
                        insertXH.xuathang_content = txtNoiDung.Value;
                        insertXH.xuathang_createdate = DateTime.Now;
                        insertXH.username_id = getuser.username_id;
                        db.tbXuatHangs.InsertOnSubmit(insertXH);
                        db.SubmitChanges();
                        //lưu dữ liệu nhập hàng chi tiết trong table vào datatable
                        foreach (DataRow row in dtProduct.Rows)
                        {
                            //kiểm tra sp trong nhập hàng để lấy giá xuất đúng với giá nhập về
                            var checkPrd = from sp in db.tbNhapHang_ChiTiets
                                           where sp.product_id == Convert.ToInt32(row["product_id"])
                                           && sp.active == true
                                           select sp;
                            foreach (var item in checkPrd)
                            {
                                tbXuatHang_ChiTiet insertXHCT = new tbXuatHang_ChiTiet();
                                /*nếu sl xuất cuối cùng mà ít hơn sl nhập 
                                 *thì lấy số dư còn lại của lần nhập đó và lấy giá
                                 * rồi mới lấy sl xuất còn lại theo mức giá của lần nhập tiếp theo
                                 */
                                //kiểm tra sl xuất lần trước ntn với sl nhập để tính giá 
                                var checksl = from s in db.tbXuatHang_ChiTiets
                                              where s.product_id == Convert.ToInt32(row["product_id"])
                                              orderby s.xuathang_chitiet_id descending
                                              select s;
                                if (checksl.Count() > 0)
                                {
                                    int soluong = Convert.ToInt32(checksl.First().xuathang_chitiet_soluong);
                                    if (soluong < item.nhaphang_chitiet_soluong)
                                    {
                                        int slxuat = Convert.ToInt32(item.nhaphang_chitiet_soluong - soluong);
                                        if (slxuat <= Convert.ToInt32(row["xuathang_chitiet_soluong"]))
                                        {
                                            insertXHCT.xuathang_id = insertXH.xuathang_id;
                                            insertXHCT.product_id = Convert.ToInt32(row["product_id"]);
                                            insertXHCT.xuathang_chitiet_soluong = slxuat;
                                            insertXHCT.xuathang_giaxuat = Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.xuathang_thanhtien = slxuat * Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.username_id = insertXH.username_id;
                                            insertXHCT.xuathang_code = insertXH.xuathang_code;
                                            insertXHCT.nhaphang_chitiet_id = item.nhaphang_chitiet_id;
                                            db.tbXuatHang_ChiTiets.InsertOnSubmit(insertXHCT);
                                            db.SubmitChanges();
                                            /*kiểm tra xem tổng sl xuất đã bằng với tổng sl nhập của đợt này chưa
                                             * nếu rồi thì cho lần nhập này về false ngược lại thì thôi
                                            */
                                            
                                            item.nhaphang_soluong_daxuat = Convert.ToInt32(item.nhaphang_soluong_daxuat + insertXHCT.xuathang_chitiet_soluong);
                                            db.SubmitChanges();
                                            if (item.nhaphang_chitiet_soluong == item.nhaphang_soluong_daxuat)
                                            {
                                                item.active = false;
                                                db.SubmitChanges();
                                            }
                                            //sl xuất còn lại
                                            row["xuathang_chitiet_soluong"] = Convert.ToInt32(row["xuathang_chitiet_soluong"]) - insertXHCT.xuathang_chitiet_soluong;
                                        }
                                        else
                                        {
                                            insertXHCT.xuathang_id = insertXH.xuathang_id;
                                            insertXHCT.product_id = Convert.ToInt32(row["product_id"]);
                                            insertXHCT.xuathang_chitiet_soluong = Convert.ToInt32(row["xuathang_chitiet_soluong"]);
                                            insertXHCT.xuathang_giaxuat = Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.xuathang_thanhtien = Convert.ToInt32(row["xuathang_chitiet_soluong"]) * Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.username_id = insertXH.username_id;
                                            insertXHCT.xuathang_code = insertXH.xuathang_code;
                                            insertXHCT.nhaphang_chitiet_id = item.nhaphang_chitiet_id;
                                            db.tbXuatHang_ChiTiets.InsertOnSubmit(insertXHCT);
                                            db.SubmitChanges();
                                            item.nhaphang_soluong_daxuat = Convert.ToInt32(item.nhaphang_soluong_daxuat + insertXHCT.xuathang_chitiet_soluong);
                                            db.SubmitChanges();
                                            if (item.nhaphang_chitiet_soluong == item.nhaphang_soluong_daxuat)
                                            {
                                                item.active = false;
                                                db.SubmitChanges();
                                            }
                                            break;
                                        }

                                    }
                                    else if (Convert.ToInt32(row["xuathang_chitiet_soluong"]) > 0)
                                    {
                                        if (Convert.ToInt32(row["xuathang_chitiet_soluong"]) >= Convert.ToInt32(item.nhaphang_chitiet_soluong))
                                        {
                                            //nếu sl xuất > sl nhập trong foreach thì lưu với giá nhập của từng lần nhập
                                            insertXHCT.xuathang_id = insertXH.xuathang_id;
                                            insertXHCT.product_id = Convert.ToInt32(row["product_id"]);
                                            insertXHCT.xuathang_chitiet_soluong = Convert.ToInt32(item.nhaphang_chitiet_soluong);
                                            insertXHCT.xuathang_giaxuat = Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.xuathang_thanhtien = Convert.ToInt32(item.nhaphang_chitiet_soluong) * Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.username_id = insertXH.username_id;
                                            insertXHCT.xuathang_code = insertXH.xuathang_code;
                                            insertXHCT.nhaphang_chitiet_id = item.nhaphang_chitiet_id;
                                            db.tbXuatHang_ChiTiets.InsertOnSubmit(insertXHCT);
                                            db.SubmitChanges();
                                            //tính lại sl còn lại để chuyển sang foreach mới lấy giá mới
                                            row["xuathang_chitiet_soluong"] = Convert.ToInt32(row["xuathang_chitiet_soluong"]) - Convert.ToInt32(item.nhaphang_chitiet_soluong);
                                            //cập nhật lại trạng với nhập hàng chi tiết
                                            item.nhaphang_soluong_daxuat = Convert.ToInt32(item.nhaphang_soluong_daxuat + insertXHCT.xuathang_chitiet_soluong);
                                            db.SubmitChanges();
                                            if (item.nhaphang_chitiet_soluong == item.nhaphang_soluong_daxuat)
                                            {
                                                item.active = false;
                                                db.SubmitChanges();
                                            }
                                        }
                                        else
                                        {
                                            insertXHCT.xuathang_id = insertXH.xuathang_id;
                                            insertXHCT.product_id = Convert.ToInt32(row["product_id"]);
                                            insertXHCT.xuathang_chitiet_soluong = Convert.ToInt32(row["xuathang_chitiet_soluong"]);
                                            insertXHCT.xuathang_giaxuat = Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.xuathang_thanhtien = Convert.ToInt32(row["xuathang_chitiet_soluong"]) * Convert.ToInt32(item.nhaphang_gianhap);
                                            insertXHCT.username_id = insertXH.username_id;
                                            insertXHCT.xuathang_code = insertXH.xuathang_code;
                                            insertXHCT.nhaphang_chitiet_id = item.nhaphang_chitiet_id;
                                            db.tbXuatHang_ChiTiets.InsertOnSubmit(insertXHCT);
                                            db.SubmitChanges();
                                            item.nhaphang_soluong_daxuat = Convert.ToInt32(item.nhaphang_soluong_daxuat + insertXHCT.xuathang_chitiet_soluong);
                                            db.SubmitChanges();
                                            if (item.nhaphang_chitiet_soluong == item.nhaphang_soluong_daxuat)
                                            {
                                                item.active = false;
                                                db.SubmitChanges();
                                            }
                                            row["xuathang_chitiet_soluong"] = Convert.ToInt32(row["xuathang_chitiet_soluong"]) - Convert.ToInt32(insertXHCT.xuathang_chitiet_soluong);
                                            break;
                                        }
                                    }
                                    else { }
                                }
                                else
                                {
                                    //nếu sp chưa có trong xuất hàng thì kiểm tra sl nhập và xuất bt
                                    if (Convert.ToInt32(row["xuathang_chitiet_soluong"]) >= Convert.ToInt32(item.nhaphang_chitiet_soluong))
                                    {
                                        //nếu sl xuất > sl nhập trong foreach thì lưu với giá nhập của từng lần nhập
                                        insertXHCT.xuathang_id = insertXH.xuathang_id;
                                        insertXHCT.product_id = Convert.ToInt32(row["product_id"]);
                                        insertXHCT.xuathang_chitiet_soluong = Convert.ToInt32(item.nhaphang_chitiet_soluong);
                                        insertXHCT.xuathang_giaxuat = Convert.ToInt32(item.nhaphang_gianhap);
                                        insertXHCT.xuathang_thanhtien = Convert.ToInt32(item.nhaphang_chitiet_soluong) * Convert.ToInt32(item.nhaphang_gianhap);
                                        insertXHCT.username_id = insertXH.username_id;
                                        insertXHCT.xuathang_code = insertXH.xuathang_code;
                                        insertXHCT.nhaphang_chitiet_id = item.nhaphang_chitiet_id;
                                        db.tbXuatHang_ChiTiets.InsertOnSubmit(insertXHCT);
                                        db.SubmitChanges();
                                        //tính lại sl còn lại để chuyển sang foreach mới lấy giá mới
                                        row["xuathang_chitiet_soluong"] = Convert.ToInt32(row["xuathang_chitiet_soluong"]) - Convert.ToInt32(item.nhaphang_chitiet_soluong);
                                        //cập nhật lại trạng với nhập hàng chi tiết
                                        item.nhaphang_soluong_daxuat = Convert.ToInt32(item.nhaphang_soluong_daxuat + insertXHCT.xuathang_chitiet_soluong);
                                        db.SubmitChanges();
                                        if (item.nhaphang_chitiet_soluong == item.nhaphang_soluong_daxuat)
                                        {
                                            item.active = false;
                                            db.SubmitChanges();
                                        }
                                    }
                                    else
                                    {
                                        insertXHCT.xuathang_id = insertXH.xuathang_id;
                                        insertXHCT.product_id = Convert.ToInt32(row["product_id"]);
                                        insertXHCT.xuathang_chitiet_soluong = Convert.ToInt32(row["xuathang_chitiet_soluong"]);
                                        insertXHCT.xuathang_giaxuat = Convert.ToInt32(item.nhaphang_gianhap);
                                        insertXHCT.xuathang_thanhtien = Convert.ToInt32(row["xuathang_chitiet_soluong"]) * Convert.ToInt32(item.nhaphang_gianhap);
                                        insertXHCT.username_id = insertXH.username_id;
                                        insertXHCT.xuathang_code = insertXH.xuathang_code;
                                        insertXHCT.nhaphang_chitiet_id = item.nhaphang_chitiet_id;
                                        db.tbXuatHang_ChiTiets.InsertOnSubmit(insertXHCT);
                                        db.SubmitChanges();
                                        item.nhaphang_soluong_daxuat = Convert.ToInt32(item.nhaphang_soluong_daxuat + insertXHCT.xuathang_chitiet_soluong);
                                        db.SubmitChanges();
                                        if (item.nhaphang_chitiet_soluong == item.nhaphang_soluong_daxuat)
                                        {
                                            item.active = false;
                                            db.SubmitChanges();
                                        }
                                        break;
                                    }
                                }
                            }
                            db.SubmitChanges();
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xuất hàng thành công!', '','success').then(function(){window.location = '/admin-quan-ly-xuat-hang';})", true);
                        }
                    }
                }
                catch { }
                string matutang = Matutang();
                txtMaXuat.Value = matutang;
                txtNoiDung.Value = "";
            }
        }
    }

    //lưu lại số lượng khi thay đổi
    protected void btnXuatHang_ServerClick(object sender, EventArgs e)
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
                                  //&& sp.active == true
                                  select sp;
                    var checkxh = from sp in db.tbXuatHang_ChiTiets
                                  where sp.product_id == _id
                                  //&& sp.active == true
                                  select sp;
                    int tongnhap = 0, tongxuat = 0;
                    foreach (var item in checknh)
                    {
                        tongnhap += Convert.ToInt32(item.nhaphang_chitiet_soluong);
                    }
                    foreach (var item in checkxh)
                    {
                        tongxuat += Convert.ToInt32(item.xuathang_chitiet_soluong);
                    }
                    if ((tongnhap - tongxuat) < Convert.ToInt32(txt_SoLuong.Value))
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

    protected void btnXoa_ServerClick(object sender, EventArgs e)
    {
        int _id = Convert.ToInt32(txt_ID.Value);
        dtProduct = (DataTable)Session["spChiTiet"];
        foreach (DataRow row in dtProduct.Rows)
        {
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
        //alert.alert_Success(Page, "Xóa thành công", "");
    }
}