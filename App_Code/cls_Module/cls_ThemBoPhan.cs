using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_ThemBoPhan
/// </summary>
public class cls_ThemBoPhan
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public cls_ThemBoPhan()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Linq_Them(string groupuser_name)
    {
        admin_GroupUser insert = new admin_GroupUser();
        insert.groupuser_name = groupuser_name;
        insert.groupuser_active = true;
        db.admin_GroupUsers.InsertOnSubmit(insert);
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
    public bool Linq_Sua(int groupuser_id, string groupuser_name)
    {

        admin_GroupUser update = db.admin_GroupUsers.Where(x => x.groupuser_id == groupuser_id).FirstOrDefault();
        update.groupuser_name = groupuser_name;
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
    public bool Linq_Xoa(int groupuser_id)
    {
        admin_GroupUser delete = db.admin_GroupUsers.Where(x => x.groupuser_id == groupuser_id).FirstOrDefault();
        delete.groupuser_active = false;
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