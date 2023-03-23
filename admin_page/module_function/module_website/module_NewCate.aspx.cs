using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_News : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int _id;
    protected void Page_Load(object sender, EventArgs e)
    {
        //edtnoidung.Toolbars.Add(HtmlEditorToolbar.CreateStandardToolbar1());
        if (Session["AdminLogined"] != null)
        {
            admin_User logedMember = Session["AdminLogined"] as admin_User;
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
        var getData = from n in db.tbNewCates
                      where n.hidden==false
                      orderby n.newcate_id descending
                      select new
                      {
                          n.newcate_id,
                          n.newcate_title,
                      };
        grvList.DataSource = getData;
        grvList.DataBind();
        //ddlloaisanpham.DataSource = from tb in db.tbNewsCates
        //                            select tb;
        //ddlloaisanpham.DataBind();

    }
    private void setNULL()
    {
        txttensanpham.Text = "";
        //edtnoidung.Html = "";
        //txttomtat.Text = "";
        //SEO_KEYWORD.Text = "";
        //SEO_TITLE.Text = "";
        //SEO_LINK.Text = "";
        //SEO_DEP.Text = "";
        //SEO_IMAGE.Text = "";
        //imgPreview.Src = "";
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        Session["_id"] = 0;
        setNULL();
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Insert", "popupControl.Show();showImg('');", true);
    }
    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "newcate_id" }));
        Session["_id"] = _id;
        var getData = (from n in db.tbNewCates
                       where n.newcate_id == _id
                       select new
                       {
                           n.newcate_id,
                           n.newcate_title,
                       }).Single();
        txttensanpham.Text = getData.newcate_title;
        //ddlloaisanpham.Text = getData.newscate_title;
        //edtnoidung.Html = getData.news_content;
        //txttomtat.Text = getData.news_summary;
        //dteDate.Value = getData.news_createdate.ToString();
        //imgPreview.Src = getData.news_image;
        //SEO_KEYWORD.Text = getData.meta_keywords;
        //SEO_TITLE.Text = getData.meta_title;
        //SEO_LINK.Text = getData.link_seo;
        //SEO_DEP.Text = getData.meta_description;
        //SEO_IMAGE.Text = getData.meta_image;
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show();", true);
    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        cls_NewCate cls;
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "newcate_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
                cls = new cls_NewCate();
                tbNewCate checkImage = (from i in db.tbNewCates where i.newcate_id == Convert.ToInt32(item) select i).SingleOrDefault();
                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xóa thành công!', '','success').then(function(){window.location = '/admin-new-cate';})", true);
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
    //    if (txttensanpham.Text != "" || edtnoidung.Html != "")
    //        return true;
    //    else return false;
    //}

    protected void btnLuu_Click(object sender, EventArgs e)
    {
        //cls_UploadImage uploadImg = new cls_UploadImage();
        //HttpFileCollection fileCollection = Request.Files;
        //string fileName = uploadImg.uploadSingle(fileCollection);
        cls_NewCate cls = new cls_NewCate();
        //if (checknull() == false)
        //    alert.alert_Warning(Page, "Hãy nhập đầy đủ thông tin!", "");
        //else
        //{
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
        //    if (SEO_DEP.Text != "")
        //    {
        //        Dep = SEO_DEP.Text;
        //    }
        //    if (SEO_IMAGE.Text != "")
        //    {
        //        ImageSeo = SEO_IMAGE.Text;
        //    }
        //}
        if (Session["_id"].ToString() == "0")
        {
            //if (dteDate.Value == "")
            //{
            //    dteDate.Value = DateTime.Now.ToString();
            //}

            if (cls.Linq_Them(txttensanpham.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Thêm thành công!', '','success').then(function(){window.location = '/admin-new-cate';})", true);
            }
            else
                alert.alert_Error(Page, "Thêm thất bại", "");
        }
        else
        {
            //if (dteDate.Value == "")
            //{
            //    dteDate.Value = DateTime.Now.ToString();
            //}
            if (cls.Linq_Sua(Convert.ToInt32(Session["_id"].ToString()), txttensanpham.Text))
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Cập nhật thành công!', '','success').then(function(){window.location = '/admin-new-cate';})", true);
            else alert.alert_Error(Page, "Cập nhật thất bại", "");
        }
        //popupControl.ShowOnPageLoad = false;
        //}
    }
}