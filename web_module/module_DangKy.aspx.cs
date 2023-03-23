using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class web_module_module_DangKy : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnDangKy_ServerClick(object sender, EventArgs e)
    {
        if (txtHoTen.Value == "" || txtSdt.Value == "" || txtUser.Value == "" || txtPass.Value == "")
        {
            alert.alert_Warning(Page, "Vui lòng nhập đủ thông tin!", "text");
        }
        else
        {
            tbCustomerAccount insert = new tbCustomerAccount();
            insert.customer_fullname = txtHoTen.Value;
            insert.customer_phone = txtSdt.Value;
            insert.customer_user = txtUser.Value;
            insert.customer_pass = txtPass.Value;
            db.tbCustomerAccounts.InsertOnSubmit(insert);
            try
            {
                db.SubmitChanges();
                Response.Redirect("/login");
            }
            catch
            {
            }
        }
    }
}