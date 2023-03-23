using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_ChuongTrinhKhuyenMai
/// </summary>
public class cls_ChuongTrinhKhuyenMai
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public cls_ChuongTrinhKhuyenMai()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Linq_Them(string km_name, string km_content, string km_percent, string image, DateTime tungay, DateTime denngay)
    {
        tbChuongTrinhKhuyenMai insert = new tbChuongTrinhKhuyenMai();
        insert.khuyenmai_name = km_name;
        insert.khuyenmai_content = km_content;
        insert.khuyenmai_percent = km_percent;
        insert.khuyenmai_image = image;
        insert.khuyenmai_tungay = tungay;
        insert.khuyenmai_denngay = denngay;
        db.tbChuongTrinhKhuyenMais.InsertOnSubmit(insert);
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
    public bool Linq_Sua(int km_id, string km_name, string km_content, string km_percent, string image, DateTime tungay, DateTime denngay)
    {

        tbChuongTrinhKhuyenMai update = db.tbChuongTrinhKhuyenMais.Where(x => x.khuyenmai_id == km_id).FirstOrDefault();
        update.khuyenmai_name = km_name;
        update.khuyenmai_content = km_content;
        update.khuyenmai_percent = km_percent;
        update.khuyenmai_image = image;
        update.khuyenmai_tungay = tungay;
        update.khuyenmai_denngay = denngay;
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
    public bool Linq_Xoa(int km_id)
    {
        tbChuongTrinhKhuyenMai delete = db.tbChuongTrinhKhuyenMais.Where(x => x.khuyenmai_id == km_id).FirstOrDefault();
        db.tbChuongTrinhKhuyenMais.DeleteOnSubmit(delete);
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