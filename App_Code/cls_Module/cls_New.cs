using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_New
/// </summary>
public class cls_New
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public cls_New()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Linq_Them(string new_title, string new_summary, string image, string new_content)
    {
        tbNew insert = new tbNew();
        insert.news_title = new_title;
        insert.news_summary = new_summary;
        insert.news_image = image;
        insert.news_content = new_content;
        //insert.newcate_id = newcate_id;
        insert.active = true;
        insert.hidden = false;
        //if (SEO_KEYWORD != "")
        //{
        //    insert.meta_keywords = SEO_KEYWORD;
        //}
        //else
        //{
        //    insert.meta_keywords = new_title + ", " + cls_ToAscii.ToAscii(new_title.ToLower());
        //}
        //if (SEO_TITLE != "")
        //{
        //    insert.meta_tittle = SEO_TITLE;
        //}
        //else
        //{
        //    insert.meta_tittle = new_title + " | " + cls_ToAscii.ToAscii(new_title.ToLower());
        //}

        //if (SEO_DEP != "")
        //{
        //    insert.meta_description = SEO_DEP;
        //}
        //else
        //{
        //    insert.meta_description = new_title + " | " + cls_ToAscii.ToAscii(new_title.ToLower());
        //}
        //insert.meta_image = SEO_IMAGE;
        //if (SEO_LINK != "")
        //{
        //    insert.link_seo = SEO_LINK;
        //}
        //else
        //{
        //    insert.link_seo = "tin-tuc/" + cls_ToAscii.ToAscii(new_title.ToLower()) + "-" + insert.news_id;
        //}
        insert.news_createdate = DateTime.Now;
        db.tbNews.InsertOnSubmit(insert);
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
    public bool Linq_Sua(int new_id, string new_title, string new_summary, string image, string new_content)
    {

        tbNew update = db.tbNews.Where(x => x.news_id == new_id).FirstOrDefault();
        update.news_title = new_title;
        update.news_summary = new_summary;
        update.news_image = image;
        update.news_content = new_content;
        // update.newcate_id = newcate_id;
        //if (SEO_KEYWORD != "")
        //{
        //    update.meta_keywords = SEO_KEYWORD;
        //}
        //else
        //{
        //    update.meta_keywords = new_title + ", " + cls_ToAscii.ToAscii(new_title.ToLower());
        //}
        //if (SEO_TITLE != "")
        //{
        //    update.meta_tittle = SEO_TITLE;
        //}
        //else
        //{
        //    update.meta_tittle = new_title + " | " + cls_ToAscii.ToAscii(new_title.ToLower());
        //}
        //if (SEO_LINK != "")
        //{
        //    update.link_seo = SEO_LINK;
        //}
        //else
        //{
        //    update.link_seo = "tin-tuc/" + cls_ToAscii.ToAscii(new_title.ToLower()) + "-" + update.news_id;
        //}
        //if (SEO_DEP != "")
        //{
        //    update.meta_description = SEO_DEP;
        //}
        //else
        //{
        //    update.meta_description = new_title + " | " + cls_ToAscii.ToAscii(new_title.ToLower());
        //}
        //update.meta_image = SEO_IMAGE;
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
    public bool Linq_Xoa(int new_id)
    {
        tbNew delete = db.tbNews.Where(x => x.news_id == new_id).FirstOrDefault();
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