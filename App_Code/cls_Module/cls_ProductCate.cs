using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_ProductCate
/// </summary>
public class cls_ProductCate
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public cls_ProductCate()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Linq_Them(string productcate_title)
    {
        tbProductCate insert = new tbProductCate();
        insert.productcate_title = productcate_title;
        insert.hidden = false;
        db.tbProductCates.InsertOnSubmit(insert);
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
    public bool Linq_Sua(int productcate_id, string productcate_title)
    {

        tbProductCate update = db.tbProductCates.Where(x => x.productcate_id == productcate_id).FirstOrDefault();
        update.productcate_title = productcate_title;
        update.hidden = false;
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
    public bool Linq_Xoa(int productcate_id)
    {
        tbProductCate delete = db.tbProductCates.Where(x => x.productcate_id == productcate_id).FirstOrDefault();
        delete.hidden = true;
        //db.tbProductCates.DeleteOnSubmit(delete);
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