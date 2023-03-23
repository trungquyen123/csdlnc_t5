using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_Product
/// </summary>
public class cls_Product
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public cls_Product()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Linq_Them(string product_title, string product_summary, string image, string product_content, int productcate_id, int product_price, int product_promotions)
    {
        tbProduct insert = new tbProduct();
        insert.product_title = product_title;
        insert.product_summary = product_summary;
        insert.product_image = image;
        insert.product_content = product_content;
        insert.productcate_id = productcate_id;
        insert.product_price = product_price;
        insert.product_promotions = product_promotions;
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
        db.tbProducts.InsertOnSubmit(insert);
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
    public bool Linq_Sua(int product_id, string product_title, string product_summary, string image, string product_content, int productcate_id, int product_price, int product_promotions)
    {

        tbProduct update = db.tbProducts.Where(x => x.product_id == product_id).FirstOrDefault();
        update.product_title = product_title;
        update.product_summary = product_summary;
        update.product_image = image;
        update.product_content = product_content;
        update.productcate_id = productcate_id;
        update.product_price = product_price;
        update.product_promotions = product_promotions;
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
    public bool Linq_Xoa(int product_id)
    {
        tbProduct delete = db.tbProducts.Where(x => x.product_id == product_id).FirstOrDefault();
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