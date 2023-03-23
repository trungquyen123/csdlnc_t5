using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_sanpham_module_ListSanPham : System.Web.UI.Page
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
        var getData = from n in db.tbProducts
                      join nc in db.tbProductCates on n.productcate_id equals nc.productcate_id
                      orderby n.product_id descending
                      select new
                      {
                          n.product_id,
                          n.product_title,
                          n.product_summary,
                          n.product_image,
                          nc.productcate_title
                      };
        // đẩy dữ liệu vào gridivew
        grvList.DataSource = getData;
        grvList.DataBind();

        var getCate = from c in db.tbProductCates
                      where c.hidden == false
                      select c;
        ddlloaisanpham.DataSource = getCate;
        ddlloaisanpham.DataBind();

    }
    private void setNULL()
    {
        //ddlloaisanpham.Text = "";
        txt_Tensanpham.Text = "";
        txt_Summary.Value = "";
        edtnoidung.Html = "";
        //SEO_KEYWORD.Text = "";
        //SEO_TITLE.Text = "";
        //SEO_LINK.Text = "";
        //SEO_DEP.Value = "";
        //SEO_IMAGE.Text = "";
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        Session["_id"] = null;
        setNULL();
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Insert", "popupControl.Show();showImg('');", true);
    }

    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        // get value từ việc click vào gridview
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "product_id" }));
        // đẩy id vào session
        Session["_id"] = _id;
        var getData = (from sp in db.tbProducts
                       join c in db.tbProductCates on sp.productcate_id equals c.productcate_id
                       where sp.product_id == _id
                       select new
                       {
                           sp.product_id,
                           sp.product_title,
                           sp.product_summary,
                           sp.product_content,
                           sp.product_image,
                           sp.product_price,
                           c.productcate_title

                       }).Single();
        txt_Tensanpham.Text = getData.product_title;
        txt_Summary.Value = getData.product_summary;
        edtnoidung.Html = getData.product_content;
        ddlloaisanpham.Text = getData.productcate_title;
        txt_Price.Text = getData.product_price+"";
        //SEO_KEYWORD.Text = getData.meta_keywords;
        //SEO_TITLE.Text = getData.meta_tittle;
        //SEO_LINK.Text = getData.link_seo;
        //SEO_DEP.Value = getData.meta_description;
        //SEO_IMAGE.Text = getData.meta_image;
        image = getData.product_image;
        txt_Image.Value = getData.product_image;
        //if (getData.news_image == null || getData.news_image == "")
        //    ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show(); showImg1_1('" + "admin_images/Preview-icon.png" + "');", true);
        //else
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show(); showImg1_1('" + getData.product_image + "');", true);
    }

    protected void btnXoa_Click(object sender, EventArgs e)
    {
        cls_Product cls = new cls_Product();
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "product_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                {
                    //----xóa hình trong foder uploadimage
                    tbProduct checkImage = (from i in db.tbProducts where i.product_id == Convert.ToInt32(item) select i).SingleOrDefault();
                    string pathToFiles = Server.MapPath(checkImage.product_image);
                    delete(pathToFiles);
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xóa thành công!', '','success').then(function(){window.location = '/admin-quan-ly-san-pham';})", true);
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
        if (edtnoidung.Html != "" || txt_Summary.Value != "")
            return true;
        else return false;
    }
    protected void btnLuu_Click(object sender, EventArgs e)
    {
        cls_Product cls = new cls_Product();
        if (Page.IsValid && FileUpload1.HasFile)
        {
            String folderUser = Server.MapPath("~/uploadimages/anh_sanpham/");
            if (!Directory.Exists(folderUser))
            {
                Directory.CreateDirectory(folderUser);
            }
            //string filename;
            string ulr = "/uploadimages/anh_sanpham/";
            HttpFileCollection hfc = Request.Files;
            //string filename = Path.GetRandomFileName() + Path.GetExtension(FileUpload1.FileName);
            string filename = DateTime.Now.ToString("ddMMyyyy_hhmmss_tt_") + FileUpload1.FileName;
            string fileName_save = Path.Combine(Server.MapPath("~/uploadimages/anh_sanpham"), filename);
            FileUpload1.SaveAs(fileName_save);
            image = ulr + filename;
        }
        if (checknull() == false)
            alert.alert_Warning(Page, "Hãy nhập đầy đủ thông tin!", "");
        else if (Convert.ToInt32(ddlloaisanpham.Value) == 0)
        {
            alert.alert_Warning(Page, "Vui lòng chọn loại sản phẩm!", "");
        }
        else if (txt_Price.Text == "")
        {
            alert.alert_Warning(Page, "Vui lòng nhập giá bán cho sản phẩm!", "");
        }
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
            if (Session["_id"] == null)
            {
                if (image == null)
                    image = "/admin_images/up-img.png";
                if (cls.Linq_Them(txt_Tensanpham.Text, txt_Summary.Value, image, edtnoidung.Html, Convert.ToInt32(ddlloaisanpham.Value), Convert.ToInt32(txt_Price.Text), Convert.ToInt32(txt_Promotions.Text)))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Thêm thành công!', '','success').then(function(){window.location = '/admin-quan-ly-san-pham';})", true);
                }
                else alert.alert_Error(Page, "Thêm thất bại", "");
            }
            else
            {
                if (image == null)
                    image = txt_Image.Value;
                if (cls.Linq_Sua(Convert.ToInt32(Session["_id"].ToString()), txt_Tensanpham.Text, txt_Summary.Value, image, edtnoidung.Html, Convert.ToInt32(ddlloaisanpham.Value), Convert.ToInt32(txt_Price.Text), Convert.ToInt32(txt_Promotions.Text)))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Cập nhật thành công!', '','success').then(function(){window.location = '/admin-quan-ly-san-pham';})", true);
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