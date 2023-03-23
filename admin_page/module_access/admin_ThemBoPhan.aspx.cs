using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_access_admin_ThemBoPhan : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int _id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"].Value != null)
        {
            //admin_User logedMember = Session["AdminLogined"] as admin_User;
            if (!IsPostBack)
            {
                Session["_id"] = 0;
            }
        }
        else
        {
            Response.Redirect("/admin-login");
        }
        loadData();
    }
    // Dành cho root
    private void loadData()
    {
        var getData = from gr in db.admin_GroupUsers
                      where gr.groupuser_id != 1 && gr.groupuser_id != 2 && gr.groupuser_active == true
                      select gr;
        grvList.DataSource = getData;
        grvList.DataBind();
    }
    private void setNULL()
    {
        txtTenBP.Text = "";
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        Session["_id"] = 0;
        setNULL();
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Insert", "popupControl.Show();", true);
    }
    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "groupuser_id" }));
        Session["_id"] = _id;
        var getData = (from gr in db.admin_GroupUsers
                       where gr.groupuser_id == _id
                       select gr).Single();
        txtTenBP.Text = getData.groupuser_name;
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show();", true);
    }
    protected void btnXoa_Click(object sender, EventArgs e)
    {

        cls_ThemBoPhan cls = new cls_ThemBoPhan();
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "groupuser_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xóa thành công!', '','success').then(function(){window.location = '/admin-groupuser';})", true);
                else
                    alert.alert_Error(Page, "Xóa thất bại", "");
            }
        }
        else
            alert.alert_Warning(Page, "Bạn chưa chọn dữ liệu", "");
    }
    protected void btnLuu_Click(object sender, EventArgs e)
    {
        cls_ThemBoPhan cls = new cls_ThemBoPhan();
        //Kiểm tra email có tồn tại
        if (Session["_id"].ToString() == "0")
        {

            if (cls.Linq_Them(txtTenBP.Text))
            {
                popupControl.ShowOnPageLoad = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Thêm thành công!', '','success').then(function(){window.location = '/admin-groupuser';})", true);
            }
            else alert.alert_Error(Page, "Thêm thất bại", "");
        }
        else
        {
            if (cls.Linq_Sua(Convert.ToInt32(Session["_id"].ToString()), txtTenBP.Text))
            {
                popupControl.ShowOnPageLoad = false;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Cập nhật thành công!', '','success').then(function(){window.location = '/admin-groupuser';})", true);
            }
            else alert.alert_Error(Page, "Cập nhật thất bại", "");
        }
    }
}