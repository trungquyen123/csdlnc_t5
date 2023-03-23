using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for cls_News
/// </summary>
public class cls_NewCate
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public cls_NewCate()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Linq_Them(string newcate_title)
    {
        //var seodata = (from gr in db.tb
        //               where gr.newscate_id == newscate_id
        //               select gr).Single();

        tbNewCate insert = new tbNewCate();
        insert.newcate_title = newcate_title;
        insert.hidden = false;
        //if (news_image != "x")
        //    insert.news_image = news_image;
        //else
        //    insert.news_image = "/web_image/empty.jpg";
        //insert.news_summary = news_summary;
        //insert.news_content = news_content;
        //insert.news_datetime = DateTime.Now.Date;
        //if (SEO_KEYWORD != "")
        //{
        //    insert.meta_keywords = SEO_KEYWORD;
        //}
        //else
        //{
        //    insert.meta_keywords = SEO_KEYWORD + ", " + cls_ToAscii.ToAscii(SEO_KEYWORD.ToLower());
        //}
        //if (SEO_TITLE != "")
        //{
        //    insert.meta_tittle = SEO_TITLE;
        //}
        //else
        //{
        //    insert.meta_tittle = SEO_KEYWORD + " | " + cls_ToAscii.ToAscii(SEO_KEYWORD.ToLower());
        //}

        //if (SEO_DEP != "")
        //{
        //    insert.meta_description = SEO_DEP;
        //}
        //else
        //{
        //    insert.meta_description = SEO_KEYWORD + " | " + cls_ToAscii.ToAscii(SEO_KEYWORD.ToLower());
        //}
        //insert.meta_image = SEO_IMAGE;

        db.tbNewCates.InsertOnSubmit(insert);
        //if (SEO_LINK != "")
        //{
        //    insert.link_seo = SEO_LINK;
        //}
        //else
        //{
        //    insert.link_seo = "tin-tuc" + cls_ToAscii.ToAscii(seodata.newscate_title.ToLower()) + "/" + news_title.ToLower() + "-" + insert.news_id;
        //}
        try
        {
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool Linq_Sua(int newcate_id, string newcate_title)
    {
        var seodata = (from gr in db.tbNewCates
                       where gr.newcate_id == newcate_id
                       select gr).Single();
        tbNewCate update = db.tbNewCates.Where(x => x.newcate_id == newcate_id).FirstOrDefault();
        update.newcate_title = newcate_title;
        //if (news_image != "x")
        //    update.news_image = news_image;
        //update.news_summary = news_summary;
        //update.news_content = news_content;
        //update.newscate_id = newscate_id;
        //update.news_createdate = DateTime.Now.Date;
        //if (SEO_KEYWORD != "")
        //{
        //    update.meta_keywords = SEO_KEYWORD;
        //}
        //else
        //{
        //    update.meta_keywords = SEO_KEYWORD + ", " + cls_ToAscii.ToAscii(SEO_KEYWORD.ToLower());
        //}
        //if (SEO_TITLE != "")
        //{
        //    update.meta_title = SEO_TITLE;
        //}
        //else
        //{
        //    update.meta_title = SEO_KEYWORD + " | " + cls_ToAscii.ToAscii(SEO_KEYWORD.ToLower());
        //}

        //if (SEO_DEP != "")
        //{
        //    update.meta_description = SEO_DEP;
        //}
        //else
        //{
        //    update.meta_description = SEO_KEYWORD + " | " + cls_ToAscii.ToAscii(SEO_KEYWORD.ToLower());
        //}
        //update.meta_image = SEO_IMAGE;
        //if (SEO_LINK != "")
        //{
        //    update.link_seo = SEO_LINK;
        //}
        //else
        //{
        //    update.link_seo = "http://lang-da-non-nuoc.net/" + cls_ToAscii.ToAscii(seodata.newscate_title.ToLower()) + "/" + news_title.ToLower() + "-" + update.news_id;
        //}
        try
        {
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool Linq_Xoa(int newcate_id)
    {
        tbNewCate delete = db.tbNewCates.Where(x => x.newcate_id == newcate_id).FirstOrDefault();
        delete.hidden = true;
        try
        {
            db.SubmitChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}