using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for cls_CustomerAccount
/// </summary>
public class cls_CustomerAccount
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public cls_CustomerAccount()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Linq_Them(string acc_name, string acc_phone, string acc_email, string acc_address, string acc_user, string acc_pass)
    {
        tbCustomerAccount insert = new tbCustomerAccount();
        insert.customer_fullname = acc_name;
        insert.customer_phone = acc_phone;
        insert.customer_email = acc_email;
        insert.customer_address = acc_address;
        insert.customer_user = acc_user;
        insert.customer_pass = acc_pass;
        insert.hidden = false;
        db.tbCustomerAccounts.InsertOnSubmit(insert);
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
    public bool Linq_Sua(int acc_id, string acc_name, string acc_phone, string acc_email, string acc_address, string acc_user, string acc_pass)
    {

        tbCustomerAccount update = db.tbCustomerAccounts.Where(x => x.customer_id == acc_id).FirstOrDefault();
        update.customer_fullname = acc_name;
        update.customer_phone = acc_phone;
        update.customer_email = acc_email;
        update.customer_address = acc_address;
        update.customer_user = acc_user;
        update.customer_pass = acc_pass;
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
    public bool Linq_Xoa(int acc_id)
    {
        tbCustomerAccount delete = db.tbCustomerAccounts.Where(x => x.customer_id == acc_id).FirstOrDefault();
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