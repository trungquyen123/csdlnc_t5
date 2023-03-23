using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_NhapHangInsert : System.Web.UI.Page
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
            //lấy thông tin của tk đang nhập
            var getuser = (from u in db.admin_Users
                           where u.username_id == 1
                           select u).FirstOrDefault();
            txtNhanVien.Value = getuser.username_fullname;
            string matutang = Matutang();
            txtMaNhap.Value = matutang;
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
        var list = from nk in db.tbNhapHangs select nk;
        string s = "NH";
        if (list.Count() <= 0)
            s = "NH00001";
        else
        {
            var list1 = from nk in db.tbNhapHangs orderby nk.nhaphang_code descending select nk;
            string chuoi = list1.First().nhaphang_code;
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
                dtProduct.Columns.Add("nhaphang_chitiet_soluong", typeof(int));
                dtProduct.Columns.Add("nhaphang_gianhap", typeof(int));
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
                row["nhaphang_chitiet_soluong"] = 1;
                row["nhaphang_gianhap"] = 0;
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
                        tbNhapHang insertNH = new tbNhapHang();
                        insertNH.nhaphang_code = txtMaNhap.Value;
                        insertNH.nhaphang_content = txtNoiDung.Value;
                        insertNH.nhaphang_createdate = DateTime.Now;
                        insertNH.username_id = getuser.username_id;
                        db.tbNhapHangs.InsertOnSubmit(insertNH);
                        db.SubmitChanges();
                        //lưu dữ liệu nhập hàng chi tiết trong table vào datatable
                        foreach (DataRow row in dtProduct.Rows)
                        {
                            var checkNhapHang = from nh in db.tbNhapHang_ChiTiets where nh.nhaphang_code == txtMaNhap.Value select nh;
                            if (checkNhapHang != txtMaNhap)
                            {
                                tbNhapHang_ChiTiet insertNHCT = new tbNhapHang_ChiTiet();
                                insertNHCT.nhaphang_id = insertNH.nhaphang_id;
                                insertNHCT.product_id = Convert.ToInt32(row["product_id"]);
                                insertNHCT.nhaphang_code = insertNH.nhaphang_code;
                                insertNHCT.nhaphang_chitiet_soluong = Convert.ToInt32(row["nhaphang_chitiet_soluong"]);
                                insertNHCT.nhaphang_gianhap = Convert.ToInt32(row["nhaphang_gianhap"]);
                                insertNHCT.nhaphang_thanhtien = Convert.ToInt32(row["nhaphang_chitiet_soluong"]) * Convert.ToInt32(row["nhaphang_gianhap"]);
                                insertNHCT.username_id = insertNH.username_id;
                                insertNHCT.nhaphang_soluong_daxuat = 0;
                                insertNHCT.active = true;
                                db.tbNhapHang_ChiTiets.InsertOnSubmit(insertNHCT);
                                db.SubmitChanges();
                                Session["spChiTiet"] = null;
                                dtProduct = (DataTable)Session["spChiTiet"];
                                rp_grvChiTiet.DataSource = dtProduct;
                                rp_grvChiTiet.DataBind();
                                //--- kiểm tra sp và lưu vào trong kho
                                var checkkh = (from kh in db.tbProduct_TonKhos
                                               where kh.product_id == Convert.ToInt32(row["product_id"])
                                               select kh).FirstOrDefault();
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
                                    checkkh.tonkho_soluong = checkkh.tonkho_soluong + Convert.ToInt32(row["nhaphang_chitiet_soluong"]);
                                    db.SubmitChanges();
                                }
                                db.SubmitChanges();
                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Nhập hàng thành công!', '','success').then(function(){window.location = '/admin-quan-ly-nhap-hang';})", true);
                            }
                        }
                    }
                }
                catch { }
                string matutang = Matutang();
                txtMaNhap.Value = matutang;
                txtNoiDung.Value = "";
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
                    //row.SetField("nhaphang_thanhtien", txt_TongTien.Value);
                    rp_grvChiTiet.DataSource = dtProduct;
                    rp_grvChiTiet.DataBind();
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