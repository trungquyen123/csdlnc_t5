using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for cls_NewsCate
/// </summary>
public class cls_Introduce
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public cls_Introduce()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Linq_Them(string introduce_title, string introduce_summary, string introduce_content, string image)
    {
        tbIntroduce insert = new tbIntroduce();
        insert.introduce_title = introduce_title;
        insert.introduce_summary = introduce_summary;
        insert.introduce_content = introduce_content;
        insert.introduce_image = image;
        insert.introduce_createdate = DateTime.Now;
        //if (SEO_KEYWORD != "")
        //{
        //    insert.meta_keywords = SEO_KEYWORD;
        //}
        //else
        //{
        //    insert.meta_keywords = introduce_title + ", " + cls_ToAscii.ToAscii(introduce_title.ToLower());
        //}
        //if (SEO_TITLE != "")
        //{
        //    insert.meta_tittle = SEO_TITLE;
        //}
        //else
        //{
        //    insert.meta_tittle = introduce_title + " | " + cls_ToAscii.ToAscii(introduce_title.ToLower());
        //}
        //if (SEO_LINK != "")
        //{
        //    insert.link_seo = SEO_LINK;
        //}
        //else
        //{
        //    insert.link_seo = "gioi-thieu/" + cls_ToAscii.ToAscii(introduce_title.ToLower()) + "-" + insert.introduct_id;
        //}
        //if (SEO_DEP != "")
        //{
        //    insert.meta_description = SEO_DEP;
        //}
        //else
        //{
        //    insert.meta_description = introduce_title + " | " + cls_ToAscii.ToAscii(introduce_title.ToLower());
        //}
        //insert.meta_image = SEO_IMAGE;
        db.tbIntroduces.InsertOnSubmit(insert);
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
    public bool Linq_Sua(int introduce_id, string introduce_title, string introduce_summary, string introduce_content, string image)
    {

        tbIntroduce update = db.tbIntroduces.Where(x => x.introduct_id == introduce_id).FirstOrDefault();
        update.introduce_title = introduce_title;
        update.introduce_summary = introduce_summary;
        update.introduce_content = introduce_content;
            update.introduce_image = image;
        //if (SEO_KEYWORD != "")
        //{
        //    update.meta_keywords = SEO_KEYWORD;
        //}
        //else
        //{
        //    update.meta_keywords = introduce_title + ", " + cls_ToAscii.ToAscii(introduce_title.ToLower());
        //}
        //if (SEO_TITLE != "")
        //{
        //    update.meta_tittle = SEO_TITLE;
        //}
        //else
        //{
        //    update.meta_tittle = introduce_title + " | " + cls_ToAscii.ToAscii(introduce_title.ToLower());
        //}
        //if (SEO_LINK != "")
        //{
        //    update.link_seo = SEO_LINK;
        //}
        //else
        //{
        //    update.link_seo = "gioi-thieu/" + cls_ToAscii.ToAscii(introduce_title.ToLower()) + "-" + update.introduct_id;
        //}
        //if (SEO_DEP != "")
        //{
        //    update.meta_description = SEO_DEP;
        //}
        //else
        //{
        //    update.meta_description = introduce_title + " | " + cls_ToAscii.ToAscii(introduce_title.ToLower());
        //}
        //update.meta_image = SEO_IMAGE;
        update.introduce_update_date = DateTime.Now;
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
    public bool Linq_Xoa(int introduce_id)
    {
        tbIntroduce delete = db.tbIntroduces.Where(x => x.introduct_id == introduce_id).FirstOrDefault();
        db.tbIntroduces.DeleteOnSubmit(delete);
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