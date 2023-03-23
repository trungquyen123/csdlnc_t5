using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Printing;
using System.Drawing;

public partial class admin_report_frm_HoaDonDichVu : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int IDHD = Convert.ToInt32(RouteData.Values["id"].ToString());
                var getHD = (from hd in db.tbHoaDonBanHangs
                             where hd.hoadon_id == IDHD
                             select hd).FirstOrDefault();
                ReportViewer1.ProcessingMode = ProcessingMode.Local;
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/admin_report/rpHoaDonDichVu.rdlc");
                dsHoaDonDichVu dsHoaDonDichVu = getHoaDonDichVu("select hoadon_code, khachhang_name,dichvu_title as dichvu_name, hoadon_createdate, username_fullname,hoadon_giamgia as khuyenmai_name, hdct_soluong, Replace(CONVERT(VARCHAR, CONVERT(MONEY, hdct_price),1),'.00','')  as hdct_rice ,Replace(CONVERT(VARCHAR, CONVERT(MONEY,  hdct_soluong * hdct_price ),1),'.00','') as hdct_thanhtien, Replace(CONVERT(VARCHAR, CONVERT(MONEY, hoadon_tongtien),1),'.00','') as hoadon_tongtien, Replace(CONVERT(VARCHAR, CONVERT(MONEY, hoadon_phaitra),1),'.00','') as hoadon_phaitra  " +
                                                                " from admin_User, tbHoaDonBanHang, tbHoaDonBanHangChiTiet, tbCustomerAccount, tbDichVu " +
                                                                " where tbHoaDonBanHangChiTiet.hoadon_id=tbHoaDonBanHang.hoadon_id " +
                                                                " and tbHoaDonBanHang.hoadon_code='" + getHD.hoadon_code + "'" +
                                                                " and tbHoaDonBanHang.nhanvien_id=admin_User.username_id" +
                                                                " and tbCustomerAccount.customer_id = tbHoaDonBanHang.khachhang_id" +
                                                                 " and tbDichVu.dichvu_id = tbHoaDonBanHangChiTiet.dichvu_id and tbHoaDonBanHangChiTiet.hidden=0");
                ReportDataSource datasource = new ReportDataSource("dsHoaDonDichVu", dsHoaDonDichVu.Tables[0]);
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.DataSources.Add(datasource);
            }
            catch (Exception) { }
        }
    }
    private dsHoaDonDichVu getHoaDonDichVu(string query)
    {
        string conString = ConfigurationManager.ConnectionStrings["db_NailsConnectionString2"].ConnectionString;

        SqlCommand cmd = new SqlCommand(query);

        using (SqlConnection con = new SqlConnection(conString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (dsHoaDonDichVu dsHoaDonDichVu = new dsHoaDonDichVu())
                {
                    sda.Fill(dsHoaDonDichVu, "dtHoaDonDichVu");
                    return dsHoaDonDichVu;
                }
            }
        }
    }
    protected void btnPrint_ServerClick(object sender, EventArgs e)
    {
        //ReportPrintDocument rp = new ReportPrintDocument(ReportViewer1.ServerReport);
        //System.Drawing.Printing rp = new System.Drawing.Printing();
    }
}