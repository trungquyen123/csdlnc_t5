using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_DichVu
/// </summary>
public class cls_DichVu
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public cls_DichVu()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Linq_Them(string dichvu_title, string dichvu_content, string dichvu_price, int dvcate_id)
    {
        tbDichVu insert = new tbDichVu();
        insert.dichvu_title = dichvu_title;
        insert.dichvu_content = dichvu_content;
        insert.dichvu_price = dichvu_price;
        //insert.dichvu_image = dichvu_image;
        insert.dvcate_id = dvcate_id;
        db.tbDichVus.InsertOnSubmit(insert);
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
    public bool Linq_Sua(int dichvu_id, string dichvu_title, string dichvu_content, string dichvu_price, int dvcate_id)
    {

        tbDichVu update = db.tbDichVus.Where(x => x.dichvu_id == dichvu_id).FirstOrDefault();
        update.dichvu_title = dichvu_title;
        update.dichvu_content = dichvu_content;
        update.dichvu_price = dichvu_price;
        //update.dichvu_image = dichvu_image;
        update.dvcate_id = dvcate_id;
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
    public bool Linq_Xoa(int dichvu_id)
    {
        tbDichVu delete = db.tbDichVus.Where(x => x.dichvu_id == dichvu_id).FirstOrDefault();
        db.tbDichVus.DeleteOnSubmit(delete);
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