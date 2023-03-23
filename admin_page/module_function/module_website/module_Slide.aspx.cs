using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_Slide : System.Web.UI.Page
{

    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int _id;
    public string image;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.Cookies["userName"] != null)
        {
            //admin_User logedMember = Session["AdminLogined"] as admin_User;
            ////if (logedMember.groupuser_id == 3)
            ////    Response.Redirect("/user-home");
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
        var getData = from tb in db.tbSlides
                      where tb.hidden == null
                      select tb;
        grvList.DataSource = getData;
        grvList.DataBind();

    }
    //private void setNULL()
    //{
    //    txttensanpham.Text = "";

    //}
    protected void btnThem_Click(object sender, EventArgs e)
    {
        Session["_id"] = 0;
        //setNULL();
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Insert", "popupControl.Show();showImg('');", true);
    }
    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "slide_id" }));
        Session["_id"] = _id;
        var getData = (from tb in db.tbSlides
                       where tb.slide_id == _id
                       select tb).Single();
        //txttensanpham.Text = getData.slide_link;
        txt_Image.Value = getData.slide_image;
        image = getData.slide_image;
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show(); showImg1_1('" + image + "');", true);
    }
    public void delete(string sFileName)
    {
        if (sFileName != String.Empty)
        {
            if (File.Exists(sFileName))

                File.Delete(sFileName);
        }
    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        cls_Slide cls;
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "slide_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
                cls = new cls_Slide();
                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                {
                    //---xóa hình ảnh trong folder uploadimage
                    tbSlide checkImage = (from i in db.tbSlides where i.slide_id == Convert.ToInt32(item) select i).SingleOrDefault();
                    string pathToFiles = Server.MapPath(checkImage.slide_image);
                    delete(pathToFiles);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xóa thành công!', '','success').then(function(){window.location = '/admin-slide';})", true);
                }
                else
                    alert.alert_Error(Page, "Xóa thất bại", "");
            }
        }
        else
            alert.alert_Warning(Page, "Bạn chưa chọn dữ liệu", "");
    }

    //public bool checknull()
    //{
    //    if (txttensanpham.Text != "")
    //        return true;
    //    else return false;
    //}

    protected void btnLuu_Click(object sender, EventArgs e)
    {
        //cls_UploadImage uploadImg = new cls_UploadImage();
        //HttpFileCollection fileCollection = Request.Files;
        //string fileName = uploadImg.uploadSingle(fileCollection);
        cls_Slide cls = new cls_Slide();
        //if (checknull() == false)
        //    alert.alert_Warning(Page, "Hảy nhập đầy đủ thông tin!", "");
        //else
        //{
        if (Page.IsValid && FileUpload1.HasFile)
        {
            String folderUser = Server.MapPath("~/uploadimages/anh_slide/");
            if (!Directory.Exists(folderUser))
            {
                Directory.CreateDirectory(folderUser);
            }
            //string filename;
            string ulr = "/web_module/image/";
            HttpFileCollection hfc = Request.Files;
            //string filename = Path.GetRandomFileName() + Path.GetExtension(FileUpload1.FileName);
            string filename = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + FileUpload1.FileName;
            string fileName_save = Path.Combine(Server.MapPath("~/web_module/image"), filename);
            FileUpload1.SaveAs(fileName_save);
            image = ulr + filename;
        }
        if (Session["_id"].ToString() == "0")
        {
            if (image == null)
                image = "/admin_images/up-img.png";
            if (cls.Linq_Them(image, txt_title.Text, txt_title1.Text))
            {
                popupControl.ShowOnPageLoad = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Thêm thành công!', '','success').then(function(){window.location = '/admin-slide';})", true);
            }
            else alert.alert_Error(Page, "Thêm thất bại", "");
        }
        else
        {
            if (image == null)
                image = txt_Image.Value;
            if (cls.Linq_Sua(Convert.ToInt32(Session["_id"].ToString()), image, txt_title.Text, txt_title1.Text))
            {
                popupControl.ShowOnPageLoad = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Cập nhật thành công!', '','success').then(function(){window.location = '/admin-slide';})", true);
            }
            else alert.alert_Error(Page, "Cập nhật thất bại", "");
        }
        //}
    }
}