using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_website_module_ChuongTrinhKhuyenMai : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int _id;
    public string image;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            // admin_User logedMember = Session["AdminLogined"] as admin_User;
            //if (logedMember.groupuser_id == 3)
            //    Response.Redirect("/user-home");
            if (!IsPostBack)
            {
                Session["_id"] = 0;

            }
            loadData();
        }
        else
        {
            Response.Redirect("/admin-login");
        }
    }
    private void loadData()
    {
        // load data đổ vào var danh sách
        var getData = from n in db.tbChuongTrinhKhuyenMais
                      where n.khuyenmai_denngay.Value.Date >= DateTime.Now.Date
                      orderby n.khuyenmai_id descending
                      select n;
        // đẩy dữ liệu vào gridivew
        grvList.DataSource = getData;
        grvList.DataBind();
    }
    private void setNULL()
    {
        txt_Name.Text = "";
        txt_Summary.Value = "";
        txt_Percent.Text = "";
        txt_Image.Value = "";
        //SEO_KEYWORD.Text = "";
        //SEO_TITLE.Text = "";
        //SEO_LINK.Text = "";
        //SEO_DEP.Value = "";
        //SEO_IMAGE.Text = "";
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            Session["_id"] = null;
            setNULL();
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Insert", "popupControl.Show()", true);
        }
    }

    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        // get value từ việc click vào gridview
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "khuyenmai_id" }));
        // đẩy id vào session
        Session["_id"] = _id;
        var getData = (from n in db.tbChuongTrinhKhuyenMais
                       where n.khuyenmai_id == _id
                       select n).Single();
        txt_Name.Text = getData.khuyenmai_name;
        txt_Summary.Value = getData.khuyenmai_content;
        txt_Percent.Text = getData.khuyenmai_percent;
        //SEO_KEYWORD.Text = getData.meta_keywords;
        //SEO_TITLE.Text = getData.meta_tittle;
        //SEO_LINK.Text = getData.link_seo;
        //SEO_DEP.Value = getData.meta_description;
        //SEO_IMAGE.Text = getData.meta_image;
        image = getData.khuyenmai_image;
        txt_Image.Value = getData.khuyenmai_image;
        txt_TuNgay.Value = getData.khuyenmai_tungay.Value.ToString("yyyy-MM-dd").Replace(' ', 'T'); ;
        txt_DenNgay.Value = getData.khuyenmai_denngay.Value.ToString("yyyy-MM-dd").Replace(' ', 'T'); ;
        //if (getData.news_image == null || getData.news_image == "")
        //    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show(); showImg1_1('" + "admin_images/Preview-icon.png" + "');", true);
        //else
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show(); showImg1_1('" + getData.khuyenmai_image + "');", true);
    }

    protected void btnXoa_Click(object sender, EventArgs e)
    {
        cls_ChuongTrinhKhuyenMai cls = new cls_ChuongTrinhKhuyenMai();
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "khuyenmai_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                {
                    //----xóa hình trong foder uploadimage
                    tbNew checkImage = (from i in db.tbNews where i.news_id == Convert.ToInt32(item) select i).SingleOrDefault();
                    string pathToFiles = Server.MapPath(checkImage.news_image);
                    delete(pathToFiles);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xóa thành công!', '','success').then(function(){window.location = '/admin-chuong-trinh-khuyen-mai';})", true);
                }
                else
                    alert.alert_Error(Page, "Xóa thất bại", "");
            }
        }
        else
            alert.alert_Warning(Page, "Bạn chưa chọn dữ liệu", "");
    }
    public bool checknull()
    {
        if (txt_Percent.Text != "" && txt_TuNgay.Value != "" && txt_DenNgay.Value != "")
            return true;
        else return false;
    }
    protected void btnLuu_Click(object sender, EventArgs e)
    {
        cls_ChuongTrinhKhuyenMai cls = new cls_ChuongTrinhKhuyenMai();
        if (Page.IsValid && FileUpload1.HasFile)
        {
            String folderUser = Server.MapPath("~/uploadimages/anh_khuyenmai/");
            if (!Directory.Exists(folderUser))
            {
                Directory.CreateDirectory(folderUser);
            }
            //string filename;
            string ulr = "/uploadimages/anh_khuyenmai/";
            HttpFileCollection hfc = Request.Files;
            //string filename = Path.GetRandomFileName() + Path.GetExtension(FileUpload1.FileName);
            string filename = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + FileUpload1.FileName;
            string fileName_save = Path.Combine(Server.MapPath("~/uploadimages/anh_khuyenmai"), filename);
            FileUpload1.SaveAs(fileName_save);
            image = ulr + filename;
        }
        if (checknull() == false)
            alert.alert_Warning(Page, "Hãy nhập đầy đủ thông tin!", "");
        else
        {
            if (Session["_id"] == null)
            {
                if (image == null)
                    image = "/admin_images/up-img.png";
                if (cls.Linq_Them(txt_Name.Text, txt_Summary.Value, txt_Percent.Text, image, Convert.ToDateTime(txt_TuNgay.Value), Convert.ToDateTime(txt_DenNgay.Value)))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Thêm thành công!', '','success').then(function(){window.location = '/admin-chuong-trinh-khuyen-mai';})", true);
                }
                else alert.alert_Error(Page, "Thêm thất bại", "");
            }
            else
            {
                if (image == null)
                    image = txt_Image.Value;
                if (cls.Linq_Sua(Convert.ToInt32(Session["_id"].ToString()), txt_Name.Text, txt_Summary.Value, txt_Percent.Text, image, Convert.ToDateTime(txt_TuNgay.Value), Convert.ToDateTime(txt_DenNgay.Value)))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Cập nhật thành công!', '','success').then(function(){window.location = '/admin-chuong-trinh-khuyen-mai';})", true);
                }
                else alert.alert_Error(Page, "Cập nhật thất bại", "");
            }
        }
    }
    public void delete(string sFileName)
    {
        if (sFileName != String.Empty)
        {
            if (File.Exists(sFileName))

                File.Delete(sFileName);
        }
    }
}