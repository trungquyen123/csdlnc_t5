using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_website_module_TaiKhoanKhachHang : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int _id;
    public int STT = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            //admin_User logedMember = Session["AdminLogined"] as admin_User;
            //if (logedMember.groupuser_id == 3)
            //    Response.Redirect("/user-home");
            if (!IsPostBack)
            {
                Session["_id"] = 0;

            }
            loadData();
        }
        else
        {
            Response.Redirect("/admin-login");
        }
    }
    private void loadData()
    {
        // load data đổ vào var danh sách
        var getData = from n in db.tbCustomerAccounts
                      where n.hidden == false && n.customer_id > 10
                      orderby n.customer_id descending
                      select n;
        // đẩy dữ liệu vào gridivew
        grvList.DataSource = getData;
        grvList.DataBind();
    }
    private void setNULL()
    {
        txt_Name.Text = "";
        txt_Phone.Text = "";
        txt_Email.Text = "";
        txt_Address.Text = "";
        txt_user.Text = "";
        txt_pass.Text = "";
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            Session["_id"] = null;
            setNULL();
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Insert", "popupControl.Show();", true);
        }
        else
        {
            Response.Redirect("/admin-login");
        }
    }

    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        // get value từ việc click vào gridview
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "customer_id" }));
        // đẩy id vào session
        Session["_id"] = _id;
        var getData = (from n in db.tbCustomerAccounts
                       where n.customer_id == _id
                       select n).Single();
        txt_Name.Text = getData.customer_fullname;
        txt_Phone.Text = getData.customer_phone;
        txt_Email.Text = getData.customer_email;
        txt_Address.Text = getData.customer_address;
        txt_user.Text = getData.customer_user;
        txt_pass.Text = getData.customer_pass;
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show();", true);
    }

    protected void btnXoa_Click(object sender, EventArgs e)
    {
        cls_CustomerAccount cls = new cls_CustomerAccount();
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "customer_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xóa thành công!', '','success').then(function(){window.location = '/admin-tai-khoan-khach-hang';})", true);
                }
                else
                    alert.alert_Error(Page, "Xóa thất bại", "");
            }
        }
        else
            alert.alert_Warning(Page, "Bạn chưa chọn dữ liệu", "");
    }
    public bool checknull()
    {
        if (txt_Phone.Text != "")
            return true;
        else return false;
    }
    protected void btnLuu_Click(object sender, EventArgs e)
    {
        cls_CustomerAccount cls = new cls_CustomerAccount();
        if (checknull() == false)
            alert.alert_Warning(Page, "Hãy nhập đầy đủ thông tin!", "");
        else
        {
            if (Session["_id"] == null)
            {
                if (cls.Linq_Them(txt_Name.Text, txt_Phone.Text, txt_Email.Text, txt_Address.Text, txt_user.Text, txt_pass.Text))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Thêm thành công!', '','success').then(function(){window.location = '/admin-tai-khoan-khach-hang';})", true);
                }
                else alert.alert_Error(Page, "Thêm thất bại", "");
            }
            else
            {
                if (cls.Linq_Sua(Convert.ToInt32(Session["_id"].ToString()), txt_Name.Text, txt_Phone.Text, txt_Email.Text, txt_Address.Text, txt_user.Text, txt_pass.Text))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Cập nhật thành công!', '','success').then(function(){window.location = '/admin-tai-khoan-khach-hang';})", true);
                }
                else alert.alert_Error(Page, "Cập nhật thất bại", "");
            }
        }
    }
}