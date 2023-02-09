using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCCV.BLL;
using PCCV.Public;
using System.IO;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;
namespace PhanCongCongViec.form.QuanLy
{
    public partial class frmCV_QL_CongViec : Form
    {
        public frmCV_QL_CongViec()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_QL_CongViec_BandedGridview);
        }
        CV_QL_CongViecPublic CongViecPublic = new CV_QL_CongViecPublic();
        //flag edit and flag add
        CV_TT_NhanSuBLL clsNhanSu = new CV_TT_NhanSuBLL();
        bool CV_QL_CongViecEdit = false;
        bool CV_QL_CongViecAdd = false;
        // lock 6 cột cuối
        CV_HT_MucDoKhoBLL clsMucDoKho = new CV_HT_MucDoKhoBLL();
        CV_QL_NhomCongViecBLL clsNhomCongViec = new CV_QL_NhomCongViecBLL();
        CV_QL_CongViecBLL cls = new CV_QL_CongViecBLL();
        CV_HT_LoaiCongViecBLL clsLoaiCongViec = new CV_HT_LoaiCongViecBLL();
        // lock input 

        /* 
         
         public void UnVisibleControls()
        {
            BarButtonItem[] items = new BarButtonItem[] { barButtonItem_Them, barButtonItem_Copy, barButtonItem_Sua, barButtonItem_Xoa, barButtonItem_Luu };

            foreach (BarButtonItem item in items)
            {
                item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        public void VisibleControls()
        {
            BarButtonItem[] items = new BarButtonItem[] { barButtonItem_Them, barButtonItem_Copy, barButtonItem_Sua, barButtonItem_Xoa, barButtonItem_Luu };
            if (BienToanCuc.MaNguoiDung == "0")
            {
                //Quyền quản trị               
                foreach (BarButtonItem item in items)
                {
                    item.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }
            else
            {
                Public_HT_PQ_USER.HT_USER_ID = int.Parse("0" + BienToanCuc.MaNguoiDung);
                //Public.HT_ROOT_Form = this.Name.Replace("btn", "frm");
                Public_HT_PQ_USER.HT_ROOT_Form = this.Name;
                Public_HT_PQ_USER.HT_ROOT_Active = true;

                SqlDataReader dr = cls_HT_PQ_USER.LoadHT_PQ_USER_R_MaND(Public_HT_PQ_USER);
                dr.Read();

                //Toàn quyền - Quyền xem                
                foreach (BarButtonItem item in items)
                {
                    if (Convert.ToBoolean(dr[0]) == true)
                    {
                        item.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    if (Convert.ToBoolean(dr[1]) == true)
                    {
                        item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                }

                //Quyền thêm
                if (Convert.ToBoolean(dr[2]) == true)
                {
                    barButtonItem_Them.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barButtonItem_Copy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barButtonItem_Luu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }

                //Quyền xóa
                if (Convert.ToBoolean(dr[3]) == true)
                {
                    barButtonItem_Xoa.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barButtonItem_Luu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }

                //Quyền sửa
                if (Convert.ToBoolean(dr[4]) == true)
                {
                    barButtonItem_Sua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    barButtonItem_Luu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                dr.Dispose();
                dr.Close();
            }
        }
         
         */
        public void FocusRowCell(string tencot)
        {
            CV_QL_CongViec_BandedGridview.ClearSelection();
            CV_QL_CongViec_BandedGridview.FocusedColumn = CV_QL_CongViec_BandedGridview.Columns[tencot];
        }
        //#region Cho phép thực hiện thao tác CLICK phải chuột

        //void Check_All_Click(object sender, EventArgs e)
        //{
        //    CV_QL_CongViec_BandedGridview.ClearSelection();
        //    CV_QL_CongViec_BandedGridview.FocusedColumn = CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_TenLoaiCongViec"];

        //    CV_QL_CongViec_BandedGridview.MoveFirst();
        //    for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
        //    {
        //        CV_QL_CongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_CongViecChon, true);
        //        CV_QL_CongViec_BandedGridview.MoveNext();
        //    }
        //    CV_QL_CongViec_BandedGridview.MoveFirst();
        //}
        //void No_Check_All_Click(object sender, EventArgs e)
        //{
        //    CV_QL_CongViec_BandedGridview.ClearSelection();
        //    CV_QL_CongViec_BandedGridview.FocusedColumn = CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_TenLoaiCongViec"];

        //    CV_QL_CongViec_BandedGridview.MoveFirst();
        //    for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
        //    {
        //        CV_QL_CongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_CongViecChon, false);
        //        CV_QL_CongViec_BandedGridview.MoveNext();
        //    }
        //    CV_QL_CongViec_BandedGridview.MoveFirst();
        //}
        //void Ghim_Trai_Click(object sender, EventArgs e)
        //{
        //    if (gridBandChung.Fixed == FixedStyle.Left)
        //    {
        //        gridBandChung.Fixed = FixedStyle.None;
        //    }
        //    else
        //    {
        //        gridBandChung.Fixed = FixedStyle.Left;
        //    }
        //}
        //#endregion#region Cho phép thực hiện thao tác CLICK phải chuột

        void Check_All_Click(object sender, EventArgs e)
        {
            CV_QL_CongViec_BandedGridview.ClearSelection();
            CV_QL_CongViec_BandedGridview.FocusedColumn = CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViecChon"];

            CV_QL_CongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
            {
                CV_QL_CongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_CongViecChon, true);
                CV_QL_CongViec_BandedGridview.MoveNext();
            }
            CV_QL_CongViec_BandedGridview.MoveFirst();
        }
        void No_Check_All_Click(object sender, EventArgs e)
        {
            CV_QL_CongViec_BandedGridview.ClearSelection();
            CV_QL_CongViec_BandedGridview.FocusedColumn = CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViecChon"];

            CV_QL_CongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
            {
                CV_QL_CongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_CongViecChon, false);
                CV_QL_CongViec_BandedGridview.MoveNext();
            }
            CV_QL_CongViec_BandedGridview.MoveFirst();
        }
        void Ghim_Trai_Click(object sender, EventArgs e)
        {
            if (gridBandChung.Fixed == FixedStyle.Left)
            {
                gridBandChung.Fixed = FixedStyle.None;
            }
            else
            {
                gridBandChung.Fixed = FixedStyle.Left;
            }
        }

        private void Lock_Unlock_Control_Input(bool Lock_Control) //Khóa và mở khóa điều khiển nhập dữ liệu
        {
            if (BienToanCuc.Lock_NhapDuLieu == true)
            {
                this.CV_QL_CongViec_TenLoaiCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_TenNhomCongViec1.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_TenNhomCongViec2.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_TenCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_MoTaCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_NhomThucHien.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_KhaNangChuyenMon.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_FileDinhKem.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_MucDoKho.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_TongSoPhutThucHien.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_TongSoGioThucHien.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_TongSoNgayThucHien.OptionsColumn.ReadOnly = !Lock_Control;
            }
        }
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_QL_CongViec_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_QL_CongViec_barButtonItem_Add.Enabled = Lock_Control;
            CV_QL_CongViec_barButtonItem_Sua.Enabled = Lock_Control;
            CV_QL_CongViec_barButtonItem_Xoa.Enabled = Lock_Control;
            CV_QL_CongViec_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_QL_CongViec_barButtonItem_Undo.Enabled = !Lock_Control;
        }

        // check nhap du lieu chua xong
        private bool KiemTra_NhapDuLieu()
        {
            CV_QL_CongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViecChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TenLoaiCongViec))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TenNhomCongViec1))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TenNhomCongViec2))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TenCongViec))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_KhaNangChuyenMon))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_MucDoKho)))
                        )
                    )
                {
                    return false;
                }
                CV_QL_CongViec_BandedGridview.MoveNext();
            }
            return true;
        }
        // Hàm read file
        byte[] ReadFile(string sPath)
        {
            //Nếu Upload Logo
            if (!string.IsNullOrWhiteSpace(sPath))
            {
                //Initialize byte array with a null value initially.
                byte[] data = null;

                //Use FileInfo object to get file size.
                FileInfo fInfo = new FileInfo(sPath);
                long numBytes = fInfo.Length;

                //Open FileStream to read file
                FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);

                //Use BinaryReader to read file stream into byte array.
                BinaryReader br = new BinaryReader(fStream);

                //When you use BinaryReader, you need to supply number of bytes to read from file.
                //In this case we want to read entire file. So supplying total number of bytes.
                data = br.ReadBytes((int)numBytes);

                //Close BinaryReader
                br.Close();

                //Close FileStream
                fStream.Close();

                return data;
            }
            //Nếu không Upload Logo
            else
            {
                byte[] data = null;
                return data;
            }
        }
        //trả về dòng đang chọn đầu tiên
        private void TraVe_DongDLChon()
        {
            CV_QL_CongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViecChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_QL_CongViec_BandedGridview.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_QL_CongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViecChon))) // == true
                {
                    return true;
                }
                CV_QL_CongViec_BandedGridview.MoveNext();
            }
            return false;
        }
        private void CV_QL_CongViec_barButtonItem_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_CongViecAdd = true;
            CV_QL_CongViecEdit = false;
            CV_QL_CongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void frmCV_QL_CongViec_Load(object sender, EventArgs e)
        {

            //lookup edit nhom thuc hien 
            CV_QL_CongViec_LookupEdit_NhomThucHien.DataSource = clsNhanSu.LoadCV_TT_NhanSu_LoadSTT();
            CV_QL_CongViec_LookupEdit_NhomThucHien.DisplayMember = "CV_TT_NhanSu_NhomThucHien";
            CV_QL_CongViec_LookupEdit_NhomThucHien.ValueMember = "CV_TT_NhanSu_ID";
            CV_QL_CongViec_LookupEdit_NhomThucHien.PopupWidth = 400;
            CV_QL_CongViec_LookupEdit_NhomThucHien.ShowFooter = false;
            CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Clear();
            CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_HoTen", "Họ tên", 200));
            CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_DonVi", "Đơn vị", 150));
            CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_NhomThucHien", "Nhóm thực hiện", 250));
            CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_TrinhDo", "Trình độ", 150));
            CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_KhaNangChuyenMon", "Khả năng chuyên môn", 300));


            // lookup edit muc do kho
            CV_QL_CongViec_LookupEdit_MucDoKho.DataSource = clsMucDoKho.LoadCV_HT_MucDoKho_LoadAll();
            CV_QL_CongViec_LookupEdit_MucDoKho.DisplayMember = "CV_HT_MucDoKho_DoKhoCongViec";
            CV_QL_CongViec_LookupEdit_MucDoKho.ValueMember = "CV_HT_MucDoKho_DoKhoCongViec";
            CV_QL_CongViec_LookupEdit_MucDoKho.PopupWidth = 400;
            CV_QL_CongViec_LookupEdit_MucDoKho.ShowFooter = false;
            CV_QL_CongViec_LookupEdit_MucDoKho.Columns.Clear();
            CV_QL_CongViec_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_MucDoKho_DoKhoCongViec", "Độ khó công việc", 50));
            CV_QL_CongViec_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_MucDoKho_Mota", "Mô tả", 50));
            CV_QL_CongViec_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_MucDoKho_GhiChu", "Ghi chú", 50));
            // lookup edit nhóm công việc
            CV_QL_CongViec_LookupEdit_NhomCongViec.DataSource = clsNhomCongViec.LoadCV_QL_NhomCongViec_LoadAll();
            CV_QL_CongViec_LookupEdit_NhomCongViec.DisplayMember = "CV_QL_NhomCongViec_TenNhomCongViec1";
            CV_QL_CongViec_LookupEdit_NhomCongViec.ValueMember = "CV_QL_NhomCongViec_TenNhomCongViec1";
            CV_QL_CongViec_LookupEdit_NhomCongViec.PopupWidth = 400;
            CV_QL_CongViec_LookupEdit_NhomCongViec.ShowFooter = false;
            CV_QL_CongViec_LookupEdit_NhomCongViec.Columns.Clear();
            CV_QL_CongViec_LookupEdit_NhomCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhomCongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 50));
            CV_QL_CongViec_LookupEdit_NhomCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhomCongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 50));
            CV_QL_CongViec_LookupEdit_NhomCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhomCongViec_MoTa", "Mô tả", 50));
            CV_QL_CongViec_LookupEdit_NhomCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhomCongViec_GhiChu", "Ghi chú", 50));
            // lookup edit nhóm công việc 2
            CV_QL_CongViec_LookupEdit_NhomCongViec2.DataSource = clsNhomCongViec.LoadCV_QL_NhomCongViec_LoadAll();
            CV_QL_CongViec_LookupEdit_NhomCongViec2.DisplayMember = "CV_QL_NhomCongViec_TenNhomCongViec2";
            CV_QL_CongViec_LookupEdit_NhomCongViec2.ValueMember = "CV_QL_NhomCongViec_TenNhomCongViec2";
            CV_QL_CongViec_LookupEdit_NhomCongViec2.PopupWidth = 400;
            CV_QL_CongViec_LookupEdit_NhomCongViec2.ShowFooter = false;
            CV_QL_CongViec_LookupEdit_NhomCongViec2.Columns.Clear();
            CV_QL_CongViec_LookupEdit_NhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhomCongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 50));
            CV_QL_CongViec_LookupEdit_NhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhomCongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 50));
            CV_QL_CongViec_LookupEdit_NhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhomCongViec_MoTa", "Mô tả", 50));
            CV_QL_CongViec_LookupEdit_NhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhomCongViec_GhiChu", "Ghi chú", 50));
            // lookup edit Loại công việc
            CV_QL_CongViec_LookupEdit_LoaiCongViec.DataSource = clsLoaiCongViec.LoadCV_HT_LoaiCongViec_LoadAll();
            CV_QL_CongViec_LookupEdit_LoaiCongViec.DisplayMember = "CV_HT_LoaiCongViec_TenLoaiCongViec";
            CV_QL_CongViec_LookupEdit_LoaiCongViec.ValueMember = "CV_HT_LoaiCongViec_TenLoaiCongViec";
            CV_QL_CongViec_LookupEdit_LoaiCongViec.PopupWidth = 400;
            CV_QL_CongViec_LookupEdit_LoaiCongViec.ShowFooter = false;
            CV_QL_CongViec_LookupEdit_LoaiCongViec.Columns.Clear();
            CV_QL_CongViec_LookupEdit_LoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_LoaiCongViec_TenLoaiCongViec", "Tên Loại công việc", 50));
            CV_QL_CongViec_LookupEdit_LoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_LoaiCongViec_Mota", "Mô tả", 50));
            CV_QL_CongViec_LookupEdit_LoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_LoaiCongViec_GhiChu", "Ghi chú", 50));

            // load form nhan su
            CV_QL_CongViec_GridControl.DataSource = cls.LoadCV_QL_CongViec();
            this.CV_QL_CongViec_ChiTietCongViec.OptionsColumn.ReadOnly = true;
            CV_QL_CongViec_barButtonItem_Luu.Enabled = false;
            CV_QL_CongViec_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_QL_CongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_QL_CongViec_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_QL_CongViec_Load(sender, e);
        }

        private void CV_QL_CongViec_GridControl_Click(object sender, EventArgs e)
        {

        }

        private void CV_QL_CongViec_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ID))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_QL_CongViecAdd = false;
                CV_QL_CongViecEdit = true;
                Lock_Unlock_Control_Input(true); //lock input
                Lock_Unlock_Control(false); // lock nut nhap hien thi nut luu
            }
        }

        private void CV_QL_CongViec_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                // tránh click nhầm
                int kq = -1;
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    //Xoá từng dòng đã check
                    CV_QL_CongViec_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViecChon))) // == true
                        {
                            string s = Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ID));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ID))))
                            {
                                CV_QL_CongViecPublic Public = new CV_QL_CongViecPublic();
                                Public.Id = Convert.ToInt32(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ID));
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_CongViec_DateEditor = DateTime.Now ;
                                Public.CV_QL_CongViec_SuDung = BienToanCuc.HT_USER_Ten;
                                kq = cls.CV_QL_CongViec_Del(Public);
                            }
                            else
                            {
                                MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        CV_QL_CongViec_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon();
                if (kq > 0)
                {
                    MessageBox.Show("Xoá Thành Công!");
                    frmCV_QL_CongViec_Load(sender, e); // load lai form
                }
                else
                {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
        }

        private void CV_QL_CongViec_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_QL_CongViecAdd == true || CV_QL_CongViecEdit == true)
                {
                    if (KiemTra() == false || KiemTra_NhapDuLieu() == false)
                    {
                        if (KiemTra() == false)
                        {
                            MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (KiemTra_NhapDuLieu() == false)
                        {
                            MessageBox.Show("Bạn phải điền đủ dữ liệu vào các ô", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    // đọc dữ liệu từng dòng
                    CV_QL_CongViec_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViecChon))) // == true
                        {
                            // gán vào đối tượng puhlic
                            CV_QL_CongViecPublic Public = new CV_QL_CongViecPublic();
                            Public.CV_QL_CongViec_HienThi = true;
                            Public.CV_QL_CongViec_SuDung = BienToanCuc.HT_USER_Ten;
                            Public.TenLoaiCongViec = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_TenLoaiCongViec);
                            Public.TenNhomCongViec1 = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_TenNhomCongViec1);
                            Public.TenNhomCongViec2 = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_TenNhomCongViec2);
                            Public.TenCongViec = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_TenCongViec);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ChiTietCongViec))))
                            {
                                Public.ChiTietCongViec = Convert.ToBoolean(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ChiTietCongViec));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_MoTaCongViec))))
                            {
                                Public.MoTaCongViec = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_MoTaCongViec);
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_NhomThucHien))))
                            {
                                Public.NhomThucHien = Convert.ToInt32(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_NhomThucHien));
                            }
                            Public.MucDoKho = CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_MucDoKho).ToString();
                            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem))))
                            {
                                Public.FileDinhKem = null;
                            }
                            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TenFile))))
                            {
                                Public.TenFile = null;
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem))))
                            {
                                //Public.FileDinhKem = (byte[])CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem);
                                Public.FileDinhKem = ReadFile(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem).ToString());
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TenFile))))
                            {
                                Public.TenFile = Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TenFile));
                            }
                            Public.KhaNangChuyenMon = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_KhaNangChuyenMon);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoPhutThucHien))))
                            {
                                Public.SoPhutThucHien = Convert.ToDouble(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoPhutThucHien));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoGioThucHien))))
                            {
                                Public.SoGioThucHien = Convert.ToDouble(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoGioThucHien));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoNgayThucHien))))
                            {
                                Public.SoNgayThucHien = Convert.ToDouble(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoNgayThucHien));
                            }
                            if (CV_QL_CongViecAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_CongViec_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_QL_CongViec_Add(Public);
                            }
                            if (CV_QL_CongViecEdit == true)
                            {
                                Public.HT_USER_Create = Convert.ToInt32(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(HT_USER_Create));
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_CongViec_DateCreate = Convert.ToDateTime(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_DateCreate));
                                Public.CV_QL_CongViec_DateEditor = DateTime.Now;
                                Public.Id = Convert.ToInt32(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ID));
                                kq = cls.CV_QL_CongViec_Edit(Public);
                            }
                        }
                        CV_QL_CongViec_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_QL_CongViecAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                    else if (CV_QL_CongViecEdit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_QL_CongViec_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_QL_CongViecAdd = false;
                CV_QL_CongViecEdit = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_QL_CongViec_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_CongViecAdd = false;
            CV_QL_CongViecEdit = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_QL_CongViec_btnEdit_TenFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //if (KiemTra() == false)
            //{
            //    MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //OpenFileDialog dlg = new OpenFileDialog();
            //DialogResult dlgRes = dlg.ShowDialog();
            //if (dlgRes != DialogResult.Cancel)
            //{
            //    FileInfo Ten_File = new FileInfo(dlg.FileName);
            //    CV_QL_CongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_CongViec_TenFile, Ten_File.Name);
            //    CV_QL_CongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem, ReadFile(dlg.FileName));
            //}

            //Ask user to select file.
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult dlgRes = dlg.ShowDialog();
            if (dlgRes != DialogResult.Cancel)
            {
                //FocusRowCell("CV_QL_CongViec_FileDinhKem");
                CV_QL_CongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem, dlg.FileName);
                FileInfo Ten_File = new FileInfo(dlg.FileName);
                //FocusRowCell("CV_QL_CongViec_TenFile");
                CV_QL_CongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_CongViec_TenFile, Ten_File.Name);
            }
        }

        private void CV_QL_CongViec_barButtonItem_DownloadDinhKem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_CongViecPublic Public = new CV_QL_CongViecPublic();
            try
            {
                //Start - Download
                Public.Id = int.Parse("0" + CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_ID));

                SqlDataReader dr = cls.LoadCV_QL_CongViec_Load_R_Para_File(Public);
                dr.Read();

                string TenFile = "";
                CV_QL_CongViec_SaveFileDinhKem.FileName = Convert.ToString(dr["CV_QL_CongViec_TenFile"].ToString());

                DialogResult DR = CV_QL_CongViec_SaveFileDinhKem.ShowDialog();
                if (DR == DialogResult.OK)
                {
                    TenFile = CV_QL_CongViec_SaveFileDinhKem.FileName;
                    //Get File data from dataset row.
                    byte[] FileDinhKem = (byte[])dr["CV_QL_CongViec_FileDinhKem"];

                    using (FileStream fs = new FileStream(TenFile, FileMode.Create))
                    {
                        fs.Write(FileDinhKem, 0, FileDinhKem.Length);
                        fs.Close();
                    }
                }
                else
                {
                    dr.Dispose();
                    dr.Close();
                    return;
                }

                dr.Dispose();
                dr.Close();

                MessageBox.Show("Tải file thành công!", "Thành công!", MessageBoxButtons.OK, MessageBoxIcon.None);

                //End - Download mẫu lấy thông tin
            }
            catch (Exception)
            {
                MessageBox.Show("Không tồn tại file NỘI DUNG ĐÍNH KÈM! (ID file: " + Public.Id + ")", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }  
        }

        private void CV_QL_CongViec_barButtonItem_In_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadHamDungChung.PreviewPrintableComponent(CV_QL_CongViec_GridControl, CV_QL_CongViec_GridControl.LookAndFeel);
        }

        private void CV_QL_CongViec_BandedGridview_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                GridViewMenu menu = e.Menu as GridViewMenu;
                menu.Items.Clear();

                DXMenuItem Check_All = new DXMenuItem("Check All (Chọn)"); // caption menu
                //itemReload.Image = ImgCollection.Images["refresh2_16x16.png"]; // icon cho menu
                Check_All.Shortcut = Shortcut.Ctrl1; // phím tắt
                Check_All.Click += new EventHandler(Check_All_Click);// thêm sự kiện click
                menu.Items.Add(Check_All);

                DXMenuItem No_Check_All = new DXMenuItem("UnCheck All (Chọn)");
                //No_Check_All.BeginGroup = true;
                //itemAdd.Image = ImgCollection.Images["new_16x16.png"];
                No_Check_All.Shortcut = Shortcut.Ctrl2;
                No_Check_All.Click += new EventHandler(No_Check_All_Click);
                menu.Items.Add(No_Check_All);

                DXMenuItem Ghim_Trai = new DXMenuItem("Ghim/Nhả ghim nhóm cột bên trái");
                Ghim_Trai.Shortcut = Shortcut.Ctrl3;
                Ghim_Trai.Click += new EventHandler(Ghim_Trai_Click);
                menu.Items.Add(Ghim_Trai);
                
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CV_QL_CongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViecChon))) // == true
                {
                    CongViecPublic.TenLoaiCongViec = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_TenLoaiCongViec);
                    CongViecPublic.TenNhomCongViec1 = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_TenNhomCongViec1);
                    CongViecPublic.TenNhomCongViec2 = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_TenNhomCongViec2);
                    CongViecPublic.TenCongViec = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_TenCongViec);
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ChiTietCongViec))))
                    {
                        CongViecPublic.ChiTietCongViec = Convert.ToBoolean(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ChiTietCongViec));
                    }
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_MoTaCongViec))))
                    {
                        CongViecPublic.MoTaCongViec = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_MoTaCongViec);
                    }
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_NhomThucHien))))
                    {
                        CongViecPublic.NhomThucHien = Convert.ToInt32(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_NhomThucHien));
                    }
                    CongViecPublic.MucDoKho = CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_MucDoKho).ToString();
                    if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem))))
                    {
                        CongViecPublic.FileDinhKem = null;
                    }
                    if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TenFile))))
                    {
                        CongViecPublic.TenFile = null;
                    }
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem))))
                    {
                        //Public.FileDinhKem = (byte[])CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem);
                        CongViecPublic.FileDinhKem = ReadFile(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem).ToString());
                    }
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TenFile))))
                    {
                        CongViecPublic.TenFile = Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TenFile));
                    }
                    CongViecPublic.KhaNangChuyenMon = CV_QL_CongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_CongViec_KhaNangChuyenMon);
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoPhutThucHien))))
                    {
                        CongViecPublic.SoPhutThucHien = Convert.ToDouble(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoPhutThucHien));
                    }
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoGioThucHien))))
                    {
                        CongViecPublic.SoGioThucHien = Convert.ToDouble(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoGioThucHien));
                    }
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoNgayThucHien))))
                    {
                        CongViecPublic.SoNgayThucHien = Convert.ToDouble(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_TongSoNgayThucHien));
                    }
                    CV_QL_CongViec_BandedGridview.AddNewRow();
                }
                CV_QL_CongViec_BandedGridview.MoveNext();
            }
        }

        private void CV_QL_CongViec_BandedGridview_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_TenLoaiCongViec"], CongViecPublic.TenLoaiCongViec);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_TenNhomCongViec1"], CongViecPublic.TenNhomCongViec1);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_TenNhomCongViec2"], CongViecPublic.TenNhomCongViec2);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_TenCongViec"], CongViecPublic.TenCongViec);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_ChiTietCongViec"], CongViecPublic.ChiTietCongViec);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_MoTaCongViec"], CongViecPublic.MoTaCongViec);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_NhomThucHien"], CongViecPublic.NhomThucHien);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_KhaNangChuyenMon"], CongViecPublic.KhaNangChuyenMon);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_FileDinhKem"], CongViecPublic.FileDinhKem);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_MucDoKho"], CongViecPublic.MucDoKho);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_TongSoPhutThucHien"], CongViecPublic.SoPhutThucHien);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_TongSoGioThucHien"], CongViecPublic.SoGioThucHien);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViec_TongSoNgayThucHien"], CongViecPublic.SoNgayThucHien);
            CV_QL_CongViec_BandedGridview.SetRowCellValue(e.RowHandle, CV_QL_CongViec_BandedGridview.Columns["CV_QL_CongViecChon"], false);
        }
    }
}
