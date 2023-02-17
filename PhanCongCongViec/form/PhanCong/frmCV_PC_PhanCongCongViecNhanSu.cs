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
namespace PhanCongCongViec.form.PhanCong
{
    public partial class frmCV_PC_PhanCongCongViecNhanSu : Form
    {
        public frmCV_PC_PhanCongCongViecNhanSu()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_PC_PhanCongCongViecNhanSu_bandedGridView);
        }
        bool CV_PC_PhanCongCongViecNhanSuAdd = false;
        bool CV_PC_PhanCongCongViecNhanSu_Edit = false;
        bool CV_PC_PhanCongCongViecNhanSu_Coppy = false;

        CV_PC_PhanCongCongViecNhanSuPublic PCCVPublic = new CV_PC_PhanCongCongViecNhanSuPublic();
        CV_PC_PhanCongCongViecNhanSuBLL clsPhanCongCongViecNhanSu = new CV_PC_PhanCongCongViecNhanSuBLL();
        CV_HT_VaiTroCongViecBLL clsVaiTro = new CV_HT_VaiTroCongViecBLL();
        CV_QL_CongViecBLL clsCongViec = new CV_QL_CongViecBLL();
        CV_QL_NhanSuBLL clsNhanSu = new CV_QL_NhanSuBLL();

        #region Cho phép thực hiện thao tác CLICK phải chuột

        void Check_All_Click(object sender, EventArgs e)
        {
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.ClearSelection();
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.FocusedColumn = CV_PC_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_PC_PhanCongCongViecNhanSuChon"];

            CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_PC_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                CV_PC_PhanCongCongViecNhanSu_bandedGridView.SetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon, true);
                CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
        }
        void No_Check_All_Click(object sender, EventArgs e)
        {
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.ClearSelection();
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.FocusedColumn = CV_PC_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_PC_PhanCongCongViecNhanSuChon"];

            CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_PC_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                CV_PC_PhanCongCongViecNhanSu_bandedGridView.SetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon, false);
                CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
        }
        void Ghim_Trai_Click(object sender, EventArgs e)
        {
            if (gridBand_Chung.Fixed == FixedStyle.Left)
            {
                gridBand_Chung.Fixed = FixedStyle.None;
            }
            else
            {
                gridBand_Chung.Fixed = FixedStyle.Left;
            }
        }
        #endregion
        //start 
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
        private void TraVe_DongDangTuongTac(int DongDangTuongTac)
        {
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_PC_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                if (i == DongDangTuongTac)
                {
                    break;
                }
                CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
        }
        private void Lock_Unlock_Control_Input(bool Lock_Control) //Khóa và mở khóa điều khiển nhập dữ liệu
        {
            if (BienToanCuc.Lock_NhapDuLieu == true)
            {
                this.CV_PC_PhanCongCongViecNhanSu_TenCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_IDUser.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_IdVaiTro.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_NgayBatDau.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_NgayKetThuc.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_DanhGia.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_LyDo.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien.OptionsColumn.ReadOnly = !Lock_Control;

            }
        }
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Add.Enabled = Lock_Control;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Sua.Enabled = Lock_Control;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Xoa.Enabled = Lock_Control;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Undo.Enabled = !Lock_Control;
        }
        private void lock_ConTrol_Always()
        { 
                this.CV_PC_PhanCongCongViecNhanSu_TenLoaiCongViec.OptionsColumn.ReadOnly = true;
                this.CV_PC_PhanCongCongViecNhanSu_TenNhomCongViec1.OptionsColumn.ReadOnly = true;
                this.CV_PC_PhanCongCongViecNhanSu_TenNhomCongViec2.OptionsColumn.ReadOnly = true;
                this.CV_PC_PhanCongCongViecNhanSu_MucDoKho.OptionsColumn.ReadOnly = true;
        }
        // check nhap du lieu chua xong
        private bool KiemTra_NhapDuLieu()
        {
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_PC_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_TenLoaiCongViec))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_TenNhomCongViec1))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_TenNhomCongViec2))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_MucDoKho))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_TenCongViec))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_IDUser))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_IdVaiTro))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_NgayBatDau))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_NgayKetThuc))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien)))

                        )
                    )
                {
                    return false;
                }
                CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
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
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_PC_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_PC_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                string s2 = i.ToString();
                string strinng = CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon).ToString();
                if (Convert.ToBoolean(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon))) // == true
                {
                    return true;
                }
                CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
            return false;
        }

        // end
        

        private void frmCV_PC_PhanCongCongViecNhanSu_Load(object sender, EventArgs e)
        {
            // load form ten loai cong viec của bảng phân công công việc nhân sự
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.DisplayMember = "CV_QL_CongViec_TenLoaiCongViec";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.ValueMember = "CV_QL_CongViec_ID";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.PopupWidth = 400;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.ShowFooter = false;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Clear();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));

            // load ten nhom cong viec 1 lookup edit
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.DisplayMember = "CV_QL_CongViec_TenNhomCongViec1";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.ValueMember = "CV_QL_CongViec_ID";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.PopupWidth = 400;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.ShowFooter = false;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Clear();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 200));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));
            
            // load ten nhom cong viec 2 lookup edit
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.DisplayMember = "CV_QL_CongViec_TenNhomCongViec2";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.ValueMember = "CV_QL_CongViec_ID";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.PopupWidth = 400;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.ShowFooter = false;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Clear();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 200));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));

            // load ten cong viec lookup edit
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.DisplayMember = "CV_QL_CongViec_TenCongViec";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.ValueMember = "CV_QL_CongViec_ID";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.PopupWidth = 400;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.ShowFooter = false;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Clear();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 200));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));

            // muc do kho lookup edit
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.DisplayMember = "CV_QL_CongViec_MucDoKho";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.ValueMember = "CV_QL_CongViec_ID";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.PopupWidth = 400;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.ShowFooter = false;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Clear();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 200));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));

            //load vai tro
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_VaiTro.DataSource = clsVaiTro.LoadCV_HT_VaiTroCongViec_LoadAll();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_VaiTro.DisplayMember = "CV_HT_VaiTroCongViec_VaiTroTrongCongViec";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_VaiTro.ValueMember = "CV_HT_VaiTroCongViec_ID";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_VaiTro.PopupWidth = 400;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_VaiTro.ShowFooter = false;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_VaiTro.Columns.Clear();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_VaiTro.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_VaiTroCongViec_VaiTroTrongCongViec", "Vai trò", 200));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_VaiTro.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_VaiTroCongViec_MoTa", "Mô tả", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_VaiTro.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_VaiTroCongViec_GhiChu", "Ghi chú", 150));

            // load nhan su
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.DataSource = clsNhanSu.LoadCV_QL_NhanSu_LoadUser();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.DisplayMember = "CV_QL_NhanSu_HoTen";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.ValueMember = "CV_QL_NhanSu_ID";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.PopupWidth = 400;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.ShowFooter = false;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.Columns.Clear();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhanSu_HoTen", "Họ tên", 200));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhanSu_DonVi", "Đơn vị", 150));
            //CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_NhomThucHien", "Nhóm thực hiện", 250));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhanSu_TrinhDo", "Trình độ", 150));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_NhanSu.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_NhanSu_KhaNangChuyenMon", "Khả năng chuyên môn", 300));


            // start
            CV_PC_PhanCongCongViecNhanSu_gridControl.DataSource = clsPhanCongCongViecNhanSu.LoadCV_PC_PhanCongCongViecNhanSu();
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Luu.Enabled = false;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            lock_ConTrol_Always();
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_PC_PhanCongCongViecNhanSu_Load(sender, e);
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_PC_PhanCongCongViecNhanSu_Coppy = false;
            CV_PC_PhanCongCongViecNhanSuAdd = true;
            CV_PC_PhanCongCongViecNhanSu_Edit = false;
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_ID))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_PC_PhanCongCongViecNhanSuAdd = false;
                CV_PC_PhanCongCongViecNhanSu_Edit = true;
                CV_PC_PhanCongCongViecNhanSu_Coppy = false;
                Lock_Unlock_Control_Input(true); //lock input
                Lock_Unlock_Control(false); // lock nut nhap hien thi nut luu
            }
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
                    for (int i = 0; i < CV_PC_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon))) // == true
                        {
                            string s = Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_ID));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_ID))))
                            {
                                CV_PC_PhanCongCongViecNhanSuPublic Public = new CV_PC_PhanCongCongViecNhanSuPublic();
                                Public.CV_PC_PhanCongCongViecNhanSu_ID = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_ID));
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_PC_PhanCongCongViecNhanSu_DateEditor = DateTime.Now;
                                Public.CV_PC_PhanCongCongViecNhanSu_SuDung = BienToanCuc.HT_USER_Ten;
                                kq = clsPhanCongCongViecNhanSu.CV_PC_PhanCongCongViecNhanSu_Del(Public);
                            }
                            else
                            {
                                MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
                    }
                }
                TraVe_DongDLChon();
                if (kq > 0)
                {
                    MessageBox.Show("Xoá Thành Công!");
                    frmCV_PC_PhanCongCongViecNhanSu_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_PC_PhanCongCongViecNhanSuAdd = false;
            CV_PC_PhanCongCongViecNhanSu_Edit = false;
            CV_PC_PhanCongCongViecNhanSu_Coppy = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_PC_PhanCongCongViecNhanSuAdd == true || CV_PC_PhanCongCongViecNhanSu_Edit == true)
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
                    CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
                    for (int i = 0; i < CV_PC_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon))) // == true
                        {

                            // gán vào đối tượng public
                            CV_PC_PhanCongCongViecNhanSuPublic Public = new CV_PC_PhanCongCongViecNhanSuPublic();
                            Public.CV_PC_PhanCongCongViecNhanSu_HienThi = true;
                            Public.CV_PC_PhanCongCongViecNhanSu_SuDung = BienToanCuc.HT_USER_Ten;
                            Public.CV_PC_PhanCongCongViecNhanSu_IDCongViec = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_TenCongViec));
                            Public.CV_PC_PhanCongCongViecNhanSu_IDUser = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_IDUser));
                            Public.CV_PC_PhanCongCongViecNhanSu_IDVaiTro = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_IdVaiTro));
                            Public.CV_PC_PhanCongCongViecNhanSu_NgayBatDau = Convert.ToDateTime(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_NgayBatDau));
                            Public.CV_PC_PhanCongCongViecNhanSu_NgayKetThuc = Convert.ToDateTime(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_NgayKetThuc));                         
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_DanhGia))))
                            {
                                Public.CV_PC_PhanCongCongViecNhanSu_DanhGia = CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_DanhGia);
                            }
                            Public.CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien= CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_LyDo))))
                            {
                                Public.CV_PC_PhanCongCongViecNhanSu_LyDo = CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_LyDo);
                            }
                            if (CV_PC_PhanCongCongViecNhanSuAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_PC_PhanCongCongViecNhanSu_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = clsPhanCongCongViecNhanSu.CV_PC_PhanCongCongViecNhanSu_Insert(Public);
                            }

                            if (CV_PC_PhanCongCongViecNhanSu_Edit == true)
                            {
                                    Public.HT_USER_Create = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(HT_USER_Create));
                                    Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                    Public.CV_PC_PhanCongCongViecNhanSu_DateCreate = Convert.ToDateTime(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_DateCreate));
                                    Public.CV_PC_PhanCongCongViecNhanSu_DateEditor = DateTime.Now;
                                   Public.CV_PC_PhanCongCongViecNhanSu_ID = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_ID));
                                   kq = clsPhanCongCongViecNhanSu.CV_PC_PhanCongCongViecNhanSu_Update(Public);
                            }
                        }
                        CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_PC_PhanCongCongViecNhanSuAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                    else if (CV_PC_PhanCongCongViecNhanSu_Edit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_PC_PhanCongCongViecNhanSu_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_PC_PhanCongCongViecNhanSuAdd = false;
                CV_PC_PhanCongCongViecNhanSu_Edit = false;
                CV_PC_PhanCongCongViecNhanSu_Coppy = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_PC_PhanCongCongViecNhanSu_gridControl_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadHamDungChung.PreviewPrintableComponent(CV_PC_PhanCongCongViecNhanSu_gridControl, CV_PC_PhanCongCongViecNhanSu_gridControl.LookAndFeel);
        }


        private void CV_PC_PhanCongCongViecNhanSu_bandedGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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

        private void frmCV_PC_PhanCongCongViecNhanSu_Load_1(object sender, EventArgs e)
        {

        }

        //private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{

        //}

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_AutoGenerate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dtCongViec = clsCongViec.LoadCV_QL_CongViec();
            int roundCV = dtCongViec.Rows.Count;
            DataTable dtNhanSu = clsNhanSu.LoadCV_QL_NhanSu_LoadUser();
            int roundNS = dtNhanSu.Rows.Count;
            for (int i = 0; i < roundCV; i++)
            {
                for (int j = 0; j < roundNS; j++)
                {
                    string s = dtNhanSu.Rows[99]["CV_QL_NhanSu_KhaNangChuyenMon"].ToString();
                    string s1 = dtCongViec.Rows[3]["CV_QL_CongViec_KhaNangChuyenMon"].ToString();
                    if (dtNhanSu.Rows[j]["CV_QL_NhanSu_KhaNangChuyenMon"].ToString().CompareTo(dtCongViec.Rows[i]["CV_QL_CongViec_KhaNangChuyenMon"].ToString()) == 0) // == true
                    {
                        PCCVPublic.CV_PC_PhanCongCongViecNhanSu_HienThi = true;
                        PCCVPublic.CV_PC_PhanCongCongViecNhanSu_SuDung = BienToanCuc.HT_USER_Ten;
                        PCCVPublic.CV_PC_PhanCongCongViecNhanSu_IDCongViec = Convert.ToInt32(dtCongViec.Rows[i]["CV_QL_CongViec_ID"].ToString());
                        PCCVPublic.CV_PC_PhanCongCongViecNhanSu_IDUser = Convert.ToInt32(dtNhanSu.Rows[j]["CV_QL_NhanSu_ID"].ToString());
                        CV_PC_PhanCongCongViecNhanSu_bandedGridView.AddNewRow();
                    }
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            int count = CV_PC_PhanCongCongViecNhanSu_bandedGridView.RowCount;
            for (int i = 0; i < count; i++)
            {
                if (Convert.ToBoolean(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon))) // == true
                {
                    CV_PC_PhanCongCongViecNhanSu_bandedGridView.SetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon, false);
                    PCCVPublic.CV_PC_PhanCongCongViecNhanSu_HienThi = true;
                    PCCVPublic.CV_PC_PhanCongCongViecNhanSu_SuDung = BienToanCuc.HT_USER_Ten;
                    PCCVPublic.CV_PC_PhanCongCongViecNhanSu_IDCongViec = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_TenCongViec));
                    PCCVPublic.CV_PC_PhanCongCongViecNhanSu_IDUser = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_IDUser));
                    PCCVPublic.CV_PC_PhanCongCongViecNhanSu_IDVaiTro = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_IdVaiTro));
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_DanhGia))))
                    {
                        PCCVPublic.CV_PC_PhanCongCongViecNhanSu_DanhGia = CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_DanhGia);
                    }
                    PCCVPublic.CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien = CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien);
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_LyDo))))
                    {
                        PCCVPublic.CV_PC_PhanCongCongViecNhanSu_LyDo = CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_LyDo);
                    }
                    CV_PC_PhanCongCongViecNhanSu_bandedGridView.AddNewRow();
                    TraVe_DongDangTuongTac(i);
                }
                CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
            CV_PC_PhanCongCongViecNhanSuAdd = false;
            CV_PC_PhanCongCongViecNhanSu_Edit = false;
            CV_PC_PhanCongCongViecNhanSu_Coppy = true;
        }

        private void CV_PC_PhanCongCongViecNhanSu_bandedGridView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.SetRowCellValue(e.RowHandle, CV_PC_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_PC_PhanCongCongViecNhanSu_IDCongViec"], PCCVPublic.CV_PC_PhanCongCongViecNhanSu_IDCongViec);
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.SetRowCellValue(e.RowHandle, CV_PC_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_PC_PhanCongCongViecNhanSu_IDUser"], PCCVPublic.CV_PC_PhanCongCongViecNhanSu_IDUser);
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.SetRowCellValue(e.RowHandle, CV_PC_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_PC_PhanCongCongViecNhanSu_IdVaiTro"], PCCVPublic.CV_PC_PhanCongCongViecNhanSu_IDVaiTro);
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.SetRowCellValue(e.RowHandle, CV_PC_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_PC_PhanCongCongViecNhanSu_DanhGia"], PCCVPublic.CV_PC_PhanCongCongViecNhanSu_DanhGia);
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.SetRowCellValue(e.RowHandle, CV_PC_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_PC_PhanCongCongViecNhanSu_LyDo"], PCCVPublic.CV_PC_PhanCongCongViecNhanSu_LyDo);
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.SetRowCellValue(e.RowHandle, CV_PC_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien"], PCCVPublic.CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien);
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.SetRowCellValue(e.RowHandle, CV_PC_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_PC_PhanCongCongViecNhanSuChon"], true);

        }
    }
}
