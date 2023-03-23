using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_Introduce : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int _id;
    public string image;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Kiểm trả session login nếu khác null thì vào form xử lý
        edtnoidung.Toolbars.Add(HtmlEditorToolbar.CreateStandardToolbar1());
        if (Request.Cookies["userName"] != null)
        {

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
        var getData = from nc in db.tbIntroduces
                      select nc;
        // đẩy dữ liệu vào gridivew
        grvList.DataSource = getData;
        grvList.DataBind();
    }
    //private void setNULL()
    //{
    //    txtTitle.Text = "";
    //    txtTitle1.Text = "";
    //    edtnoidung.Html = "";
    //}
    //protected void btnThem_Click(object sender, EventArgs e)
    //{
    //    // Khi nhấn nút thêm thì mật định session id = 0 để thêm mới
    //    Session["_id"] = 0;
    //    // gọi hàm setNull để trả toàn bộ các control về rỗng
    //    setNULL();
    //    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Insert", "popupControl.Show();", true);
    //    loadData();
    //}
    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        // get value từ việc click vào gridview
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "introduct_id" }));
        // đẩy id vào session
        Session["_id"] = _id;
        var getData = (from nc in db.tbIntroduces
                       where nc.introduct_id == _id
                       select nc).Single();
        txtTitle.Text = getData.introduce_title;
        txtSummary.Value = getData.introduce_summary;
        edtnoidung.Html = getData.introduce_content;
        //SEO_KEYWORD.Text = getData.meta_keywords;
        //SEO_TITLE.Text = getData.meta_tittle;
        //SEO_LINK.Text = getData.link_seo;
        //SEO_DEP.Value = getData.meta_description;
        //SEO_IMAGE.Text = getData.meta_image;
        image = getData.introduce_image;
        txt_Image.Value = getData.introduce_image;
        //if (getData.introduce_image == null || getData.introduce_image == "")
        //    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show(); showImg1_1('" + "admin_images/Preview-icon.png" + "');", true);
        //else
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show(); showImg1_1('" + getData.introduce_image + "');", true);
    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {

        cls_Introduce cls;
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "introduct_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
                cls = new cls_Introduce();

                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                {
                    alert.alert_Success(Page, "Xóa thành công", "");
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
        if (txtSummary.Value != "" || edtnoidung.Html != "")
            return true;
        else return false;
    }
    protected void btnLuu_Click(object sender, EventArgs e)
    {
        cls_Introduce cls = new cls_Introduce();
        String folderUser = Server.MapPath("~/uploadimages/anh_gioithieu/");
        if (!Directory.Exists(folderUser))
        {
            Directory.CreateDirectory(folderUser);
        }
        //HttpFileCollection hfc = Request.Files;
        if (Page.IsValid && FileUpload1.HasFile)
        {
            string ulr = "/uploadimages/anh_gioithieu/";
            //string filename = Path.GetRandomFileName() + Path.GetExtension(FileUpload1.FileName);
            string filename = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + FileUpload1.FileName;
            string fileName_save = Path.Combine(Server.MapPath("~/uploadimages/anh_gioithieu"), filename);
            FileUpload1.SaveAs(fileName_save);
            image = ulr + filename;
        }
        if (checknull() == false)
            alert.alert_Warning(Page, "Hãy nhập đầy đủ thông tin!", "");
        else
        {
            //string KEYWORD = "", TitleSeo = "", Link = "", Dep = "", ImageSeo = "";
            //{
            //    if (SEO_KEYWORD.Text != "")
            //    {
            //        KEYWORD = SEO_KEYWORD.Text;
            //    }
            //    if (SEO_TITLE.Text != "")
            //    {
            //        TitleSeo = SEO_TITLE.Text;
            //    }
            //    if (SEO_LINK.Text != "")
            //    {
            //        Link = SEO_LINK.Text;
            //    }
            //    if (SEO_DEP.Value != "")
            //    {
            //        Dep = SEO_DEP.Value;
            //    }
            //    if (SEO_IMAGE.Text != "")
            //    {
            //        ImageSeo = SEO_IMAGE.Text;
            //    }
            //}
            if (Session["_id"].ToString() == "0")
            {
                if (image == null)
                    image = "/admin_images/up-img.png";
                if (cls.Linq_Them(txtTitle.Text, txtSummary.Value, edtnoidung.Html, image))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Thêm thành công!', '','success').then(function(){window.location = '/admin-introduce';})", true);
                }
                else alert.alert_Error(Page, "Thêm thất bại", "");
            }
            else
            {
                if (image == null)
                    image = txt_Image.Value;
                if (cls.Linq_Sua(Convert.ToInt32(Session["_id"].ToString()), txtTitle.Text, txtSummary.Value, edtnoidung.Html, image))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Cập nhật thành công!', '','success').then(function(){window.location = '/admin-introduce';})", true);
                }
                else alert.alert_Error(Page, "Cập nhật thất bại", "");
            }
        }
        //loadData();
        //popupControl.ShowOnPageLoad = false;
    }
    //public void delete(string sFileName)
    //{
    //    if (sFileName != String.Empty)
    //    {
    //        if (File.Exists(sFileName))

    //            File.Delete(sFileName);
    //    }
    //}
}