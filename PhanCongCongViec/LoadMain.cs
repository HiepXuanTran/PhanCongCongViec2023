using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhanCongCongViec.form.QuanLy;
namespace PhanCongCongViec
{
    class LoadMain
    {
        // load form quan ly cong viec
        static frmCV_QL_CongViec m_frmCV_QL_CongViec = null;
        public static void HienThiCV_QL_CongViec()
        {
            if (m_frmCV_QL_CongViec == null || m_frmCV_QL_CongViec.IsDisposed)
            {
                m_frmCV_QL_CongViec = new frmCV_QL_CongViec();
                m_frmCV_QL_CongViec.MdiParent = frmMain.ActiveForm;
                m_frmCV_QL_CongViec.Show();
            }
            else
                m_frmCV_QL_CongViec.Activate();
        }
    }
    public class BienToanCuc
    {
        public static string CHECK_INTERNET = "google.com.vn";
        public static DateTime Time_Server; //Ngày lấy từ server
        public static DateTime Time_Server_KichHoatDT; //Ngày lấy từ server
        public static string HT_USER_Ten = "Trần Xuân Hiệp"; //Họ tên của người đăng nhập (HT_USER_Ten)
        public static string MaNguoiDung = "Hiepgatk6"; //Tên đăng nhập
        public static int HT_USER_ID = 1 ; //ID người đăng nhập   
        public static bool Lock_NhapDuLieu = true;
        public static int idCongViec = -1;
    }
}
