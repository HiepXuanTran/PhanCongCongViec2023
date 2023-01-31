using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhanCongCongViec.form.QuanLy;
using PhanCongCongViec.form.Hệ_thống;
using PhanCongCongViec.form.HeThong;
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

        // load form quan ly nhom cong viec
        static frmCV_QL_NhomCongViec m_frmCV_QL_NhomCongViec = null;
        public static void HienThiCV_QL_NhomCongViec()
        {
            if (m_frmCV_QL_NhomCongViec == null || m_frmCV_QL_NhomCongViec.IsDisposed)
            {
                m_frmCV_QL_NhomCongViec = new frmCV_QL_NhomCongViec();
                m_frmCV_QL_NhomCongViec.MdiParent = frmMain.ActiveForm;
                m_frmCV_QL_NhomCongViec.Show();
            }
            else
                m_frmCV_QL_NhomCongViec.Activate();
        }

        // load form quan ly loai cong viec
        static frmCV_HT_LoaiCongViec m_frmCV_HT_LoaiCongViec = null;
        public static void HienThiCV_HT_LoaiCongViec()
        {
            if (m_frmCV_HT_LoaiCongViec == null || m_frmCV_HT_LoaiCongViec.IsDisposed)
            {
                m_frmCV_HT_LoaiCongViec = new frmCV_HT_LoaiCongViec();
                m_frmCV_HT_LoaiCongViec.MdiParent = frmMain.ActiveForm;
                m_frmCV_HT_LoaiCongViec.Show();
            }
            else
                m_frmCV_HT_LoaiCongViec.Activate();
        }

        // load form quan ly muc do kho
        static frmCV_HT_MucDoKho m_frmCV_HT_MucDoKho = null;
        public static void HienThiCV_HT_MucDoKho()
        {
            if (m_frmCV_HT_MucDoKho == null || m_frmCV_HT_MucDoKho.IsDisposed)
            {
                m_frmCV_HT_MucDoKho = new frmCV_HT_MucDoKho();
                m_frmCV_HT_MucDoKho.MdiParent = frmMain.ActiveForm;
                m_frmCV_HT_MucDoKho.Show();
            }
            else
                m_frmCV_HT_MucDoKho.Activate();
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
