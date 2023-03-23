using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_HoaDonDichVu
/// </summary>
public class cls_HoaDonDichVu
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public cls_HoaDonDichVu()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Linq_Xoa(int hd_id)
    {
        tbHoaDonBanHang delete = db.tbHoaDonBanHangs.Where(x => x.hoadon_id == hd_id).FirstOrDefault();
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