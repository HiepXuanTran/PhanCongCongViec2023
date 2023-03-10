using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uneti.Online.form.QuanLy;
using Uneti.Online.form.HeThong;
using Uneti.Online.form.PhanCong;
using System.Windows.Forms;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.LookAndFeel;
using DevExpress.XtraPrinting;
using System.Data;
using System.Net.NetworkInformation;
namespace Uneti.Online
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

        static frmCV_QL_NhanSu m_frmCV_QL_NhanSu = null;
        public static void HienThiCV_QL_NhanSu()
        {
            if (m_frmCV_QL_NhanSu == null || m_frmCV_QL_NhanSu.IsDisposed)
            {
                m_frmCV_QL_NhanSu = new frmCV_QL_NhanSu();
                m_frmCV_QL_NhanSu.MdiParent = frmMain.ActiveForm;
                m_frmCV_QL_NhanSu.Show();
            }
            else
                m_frmCV_QL_NhanSu.Activate();
        }

        static frmCV_QL_ChiTietCongViec m_frmCV_QL_ChiTietCongViec = null;
        public static void HienThiCV_QL_ChiTietCongViec()
        {
            if (m_frmCV_QL_ChiTietCongViec == null || m_frmCV_QL_ChiTietCongViec.IsDisposed)
            {
                m_frmCV_QL_ChiTietCongViec = new frmCV_QL_ChiTietCongViec();
                m_frmCV_QL_ChiTietCongViec.MdiParent = frmMain.ActiveForm;
                m_frmCV_QL_ChiTietCongViec.Show();
            }
            else
                m_frmCV_QL_ChiTietCongViec.Activate();
        }
        static frmCV_QL_CongViec_ImportExcel m_frmCV_QL_CongViec_ImportExcel = null;
        public static void HienThiCV_TT_CongViecImport()
        {
            if (m_frmCV_QL_CongViec_ImportExcel == null || m_frmCV_QL_CongViec_ImportExcel.IsDisposed)
            {
                m_frmCV_QL_CongViec_ImportExcel = new frmCV_QL_CongViec_ImportExcel();
                m_frmCV_QL_CongViec_ImportExcel.MdiParent = frmMain.ActiveForm;
                m_frmCV_QL_CongViec_ImportExcel.Show();
            }
            else
                m_frmCV_QL_CongViec_ImportExcel.Activate();
        }

        static frmCV_PC_PhanCongCongViecNhanSu_ImportExcel m_frmCV_PC_PhanCongCongViecNhanSu_ImportExcel = null;
        public static void HienThiCV_PC_PhanCongCongViecNhanSuImport()
        {
            if (m_frmCV_PC_PhanCongCongViecNhanSu_ImportExcel == null || m_frmCV_PC_PhanCongCongViecNhanSu_ImportExcel.IsDisposed)
            {
                m_frmCV_PC_PhanCongCongViecNhanSu_ImportExcel = new frmCV_PC_PhanCongCongViecNhanSu_ImportExcel();
                m_frmCV_PC_PhanCongCongViecNhanSu_ImportExcel.MdiParent = frmMain.ActiveForm;
                m_frmCV_PC_PhanCongCongViecNhanSu_ImportExcel.Show();
            }
            else
                m_frmCV_PC_PhanCongCongViecNhanSu_ImportExcel.Activate();
        }

        static frmCV_HT_VaiTroCongViec m_frmCV_HT_VaiTroCongViec = null;
        public static void HienThiCV_HT_VaiTroCongViec()
        {
            if (m_frmCV_HT_VaiTroCongViec == null || m_frmCV_HT_VaiTroCongViec.IsDisposed)
            {
                m_frmCV_HT_VaiTroCongViec = new frmCV_HT_VaiTroCongViec();
                m_frmCV_HT_VaiTroCongViec.MdiParent = frmMain.ActiveForm;
                m_frmCV_HT_VaiTroCongViec.Show();
            }
            else
                m_frmCV_HT_VaiTroCongViec.Activate();
        }

        static frmCV_HT_NhomThucHien m_frmCV_HT_NhomThucHien = null;
        public static void HienThiCV_HT_NhomThucHien()
        {
            if (m_frmCV_HT_NhomThucHien == null || m_frmCV_HT_NhomThucHien.IsDisposed)
            {
                m_frmCV_HT_NhomThucHien = new frmCV_HT_NhomThucHien();
                m_frmCV_HT_NhomThucHien.MdiParent = frmMain.ActiveForm;
                m_frmCV_HT_NhomThucHien.Show();
            }
            else
                m_frmCV_HT_NhomThucHien.Activate();
        }
        static frmCV_HT_UuTienCongViec m_frmCV_HT_UuTienCongViec = null;
        public static void HienThiCV_HT_UuTienCongViec()
        {
            if (m_frmCV_HT_UuTienCongViec == null || m_frmCV_HT_UuTienCongViec.IsDisposed)
            {
                m_frmCV_HT_UuTienCongViec = new frmCV_HT_UuTienCongViec();
                m_frmCV_HT_UuTienCongViec.MdiParent = frmMain.ActiveForm;
                m_frmCV_HT_UuTienCongViec.Show();
            }
            else
                m_frmCV_HT_UuTienCongViec.Activate();
        }


        static frmCV_PC_PhanCongCongViecNhanSu m_frmCV_PC_PhanCongCongViecNhanSu = null;
        public static void HienThiCV_PC_PhanCongCongViecNhanSu()
        {
            if (m_frmCV_PC_PhanCongCongViecNhanSu == null || m_frmCV_PC_PhanCongCongViecNhanSu.IsDisposed)
            {
                m_frmCV_PC_PhanCongCongViecNhanSu = new frmCV_PC_PhanCongCongViecNhanSu();
                m_frmCV_PC_PhanCongCongViecNhanSu.MdiParent = frmMain.ActiveForm;
                m_frmCV_PC_PhanCongCongViecNhanSu.Show();
            }
            else
                m_frmCV_PC_PhanCongCongViecNhanSu.Activate();
        }

        static frmCV_QL_NhomCongViec_ImportExcel m_frmCV_QL_NhomCongViec_ImportExcel = null;
        public static void HienThiCV_TT_NhomCongViecImport()
        {
            if (m_frmCV_QL_NhomCongViec_ImportExcel == null || m_frmCV_QL_NhomCongViec_ImportExcel.IsDisposed)
            {
                m_frmCV_QL_NhomCongViec_ImportExcel = new frmCV_QL_NhomCongViec_ImportExcel();
                m_frmCV_QL_NhomCongViec_ImportExcel.MdiParent = frmMain.ActiveForm;
                m_frmCV_QL_NhomCongViec_ImportExcel.Show();
            }
            else
                m_frmCV_QL_NhomCongViec_ImportExcel.Activate();
        }
        // form kha nang chuyen mon
        static frmCV_HT_KhaNangChuyenMon m_frmCV_HT_KhaNangChuyenMon = null;
        public static void HienThiCV_HT_KhaNangChuyenMon()
        {
            if (m_frmCV_HT_KhaNangChuyenMon == null || m_frmCV_HT_KhaNangChuyenMon.IsDisposed)
            {
                m_frmCV_HT_KhaNangChuyenMon = new frmCV_HT_KhaNangChuyenMon();
                m_frmCV_HT_KhaNangChuyenMon.MdiParent = frmMain.ActiveForm;
                m_frmCV_HT_KhaNangChuyenMon.Show();
            }
            else
                m_frmCV_HT_KhaNangChuyenMon.Activate();
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

    public static class LoadHamDungChung
    {
        #region In dữ liệu trên lưới
        public static void PreviewPrintableComponent(IPrintable component, UserLookAndFeel lookAndFeel)
        {
            // Create a link that will print a control.            
            PrintableComponentLink link = new PrintableComponentLink()
            {
                PrintingSystemBase = new PrintingSystemBase(),
                Component = component,
                Landscape = true
                //Margins = new Mar
                //PaperKind = PaperKind.A5,
                //Margins = new Margins(20, 20, 20, 20)
            };

            // Show the report. 
            link.Margins.Bottom = 20;
            link.Margins.Top = 20;
            link.Margins.Left = 20;
            link.Margins.Right = 20;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;

            link.ShowPreview(lookAndFeel);
            //link.ShowRibbonPreview(lookAndFeel);
        }
        #endregion

        #region kiểm tra sự tồn tại của địa chỉ
        //Kiểm tra sự tồn tại của HOST gửi email
        public static bool isAddressAvailable_LoadDungChung(string address)
        {
            Ping ping = new Ping();
            try
            {
                //return ping.Send(address, 100).Status == IPStatus.Success;
                return ping.Send(address, 10000).Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
    #region Load lớp cho phép chỉnh sửa nhiều dòng, ô trên lưới
    public class MultiSelectionEditingHelper
    {
        private GridView _View;
        public MultiSelectionEditingHelper(GridView view)
        {
            _View = view;
            _View.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused;
            _View.MouseUp += _View_MouseUp;
            _View.CellValueChanged += new CellValueChangedEventHandler(_View_CellValueChanged);
            _View.MouseDown += new MouseEventHandler(_View_MouseDown);
        }

        void _View_MouseDown(object sender, MouseEventArgs e)
        {
            if (GetInSelectedCell(e))
            {
                GridHitInfo hi = _View.CalcHitInfo(e.Location);
                if (_View.FocusedRowHandle == hi.RowHandle)
                {
                    _View.FocusedColumn = hi.Column;
                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        void _View_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            OnCellValueChanged(e);
        }

        bool lockEvents;
        private void OnCellValueChanged(CellValueChangedEventArgs e)
        {
            if (lockEvents)
                return;
            lockEvents = true;
            SetSelectedCellsValues(e.Value);
            lockEvents = false;
        }

        private void SetSelectedCellsValues(object value)
        {
            try
            {
                _View.BeginUpdate();
                GridCell[] cells = _View.GetSelectedCells();
                foreach (GridCell cell in cells)
                {
                    _View.SetRowCellValue(cell.RowHandle, cell.Column, value);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _View.EndUpdate();
            }

        }
        private bool GetInSelectedCell(MouseEventArgs e)
        {
            GridHitInfo hi = _View.CalcHitInfo(e.Location);
            return hi.InRowCell && hi.InRowCell && _View.IsCellSelected(hi.RowHandle, hi.Column);
        }

        void _View_MouseUp(object sender, MouseEventArgs e)
        {
            bool inSelectedCell = GetInSelectedCell(e);
            if (inSelectedCell)
            {
                DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                _View.ShowEditorByMouse();
            }
        }
    }
    #endregion
}
