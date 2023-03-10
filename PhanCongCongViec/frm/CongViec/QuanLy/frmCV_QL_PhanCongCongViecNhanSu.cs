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
    public partial class frmCV_QL_PhanCongCongViecNhanSu : Form
    {
        public frmCV_QL_PhanCongCongViecNhanSu()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_QL_PhanCongCongViecNhanSu_bandedGridView);
        }
        bool CV_QL_PhanCongCongViecNhanSuAdd = false;
        bool CV_QL_PhanCongCongViecNhanSu_Edit = false;
        CV_QL_PhanCongCongViecNhanSuBLL clsPhanCongCongViecNhanSu = new CV_QL_PhanCongCongViecNhanSuBLL();
        CV_QL_CongViecBLL clsCongViec = new CV_QL_CongViecBLL();
        CV_TT_NhanSuBLL clsNhanSu = new CV_TT_NhanSuBLL();

        #region Cho phép thực hiện thao tác CLICK phải chuột

        void Check_All_Click(object sender, EventArgs e)
        {
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.ClearSelection();
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.FocusedColumn = CV_QL_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_QL_PhanCongCongViecNhanSuChon"];

            CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                CV_QL_PhanCongCongViecNhanSu_bandedGridView.SetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSuChon, true);
                CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
        }
        void No_Check_All_Click(object sender, EventArgs e)
        {
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.ClearSelection();
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.FocusedColumn = CV_QL_PhanCongCongViecNhanSu_bandedGridView.Columns["CV_QL_PhanCongCongViecNhanSuChon"];

            CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                CV_QL_PhanCongCongViecNhanSu_bandedGridView.SetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSuChon, false);
                CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
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
        private void Lock_Unlock_Control_Input(bool Lock_Control) //Khóa và mở khóa điều khiển nhập dữ liệu
        {
            if (BienToanCuc.Lock_NhapDuLieu == true)
            {
                this.CV_QL_PhanCongCongViecNhanSu_TenCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_PhanCongCongViecNhanSu_NhanSuPhuTrach.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_PhanCongCongViecNhanSu_NhanSuThucHien.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_PhanCongCongViecNhanSu_NhanSuPhoiHop.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_PhanCongCongViecNhanSu_NhanSuKiemTra.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_PhanCongCongViecNhanSu_NgayBatDau.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_PhanCongCongViecNhanSu_NgayKetThuc.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_PhanCongCongViecNhanSu_DanhGia.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_PhanCongCongViecNhanSu_LyDo.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien.OptionsColumn.ReadOnly = !Lock_Control;

            }
        }
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_QL_PhanCongCongViecNhanSu_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_QL_PhanCongCongViecNhanSu_barButtonItem_Add.Enabled = Lock_Control;
            CV_QL_PhanCongCongViecNhanSu_barButtonItem_Sua.Enabled = Lock_Control;
            CV_QL_PhanCongCongViecNhanSu_barButtonItem_Xoa.Enabled = Lock_Control;
            CV_QL_PhanCongCongViecNhanSu_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_QL_PhanCongCongViecNhanSu_barButtonItem_Undo.Enabled = !Lock_Control;
        }
        private void lock_ConTrol_Always()
        { 
                this.CV_QL_PhanCongCongViecNhanSu_TenLoaiCongViec.OptionsColumn.ReadOnly = true;
                this.CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec1.OptionsColumn.ReadOnly = true;
                this.CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec2.OptionsColumn.ReadOnly = true;
                this.CV_QL_PhanCongCongViecNhanSu_MucDoKho.OptionsColumn.ReadOnly = true;
        }
        // check nhap du lieu chua xong
        private bool KiemTra_NhapDuLieu()
        {
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSuChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_TenLoaiCongViec))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec1))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec2))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_MucDoKho))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_TenCongViec))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_NhanSuPhuTrach))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_NhanSuThucHien))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_NgayBatDau))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_NgayKetThuc))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien)))

                        )
                    )
                {
                    return false;
                }
                CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
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
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSuChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                string s2 = i.ToString();
                string strinng = CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSuChon).ToString();
                if (Convert.ToBoolean(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSuChon))) // == true
                {
                    return true;
                }
                CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
            return false;
        }

        // end
        

        private void frmCV_QL_PhanCongCongViecNhanSu_Load(object sender, EventArgs e)
        {
            // load form ten loai cong viec của bảng phân công công việc nhân sự
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.DisplayMember = "CV_QL_CongViec_TenLoaiCongViec";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.ValueMember = "CV_QL_CongViec_ID";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.PopupWidth = 400;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.ShowFooter = false;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Clear();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenLoaiCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));

            // load ten nhom cong viec 1 lookup edit
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.DisplayMember = "CV_QL_CongViec_TenNhomCongViec1";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.ValueMember = "CV_QL_CongViec_ID";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.PopupWidth = 400;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.ShowFooter = false;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Clear();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 200));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec1.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));
            
            // load ten nhom cong viec 2 lookup edit
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.DisplayMember = "CV_QL_CongViec_TenNhomCongViec2";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.ValueMember = "CV_QL_CongViec_ID";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.PopupWidth = 400;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.ShowFooter = false;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Clear();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 200));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenNhomCongViec2.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));

            // load ten cong viec lookup edit
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.DisplayMember = "CV_QL_CongViec_TenCongViec";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.ValueMember = "CV_QL_CongViec_ID";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.PopupWidth = 400;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.ShowFooter = false;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Clear();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 200));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));

            // muc do kho lookup edit
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.DisplayMember = "CV_QL_CongViec_MucDoKho";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.ValueMember = "CV_QL_CongViec_ID";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.PopupWidth = 400;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.ShowFooter = false;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Clear();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 200));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));

            // load nhan su
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.DataSource = clsNhanSu.LoadCV_TT_NhanSu_LoadUser();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.DisplayMember = "HT_USER_Ten";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.ValueMember = "HT_USER_ID";
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.PopupWidth = 400;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.ShowFooter = false;
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.Columns.Clear();
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HT_USER_Ten", "Họ tên", 200));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HT_PB_Ten", "Đơn vị", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_NhomThucHien", "Nhóm thực hiện", 250));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HT_CV_Ten", "Trình độ", 150));
            CV_QL_PhanCongCongViecNhanSu_LookupEdit_NhanSuPhuTrach.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("HT_USER_KhaNangChuyenMon", "Khả năng chuyên môn", 300));


            // start
            CV_QL_PhanCongCongViecNhanSu_gridControl.DataSource = clsPhanCongCongViecNhanSu.LoadCV_QL_PhanCongCongViecNhanSu();
            CV_QL_PhanCongCongViecNhanSu_barButtonItem_Luu.Enabled = false;
            CV_QL_PhanCongCongViecNhanSu_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            lock_ConTrol_Always();
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
        private void CV_QL_PhanCongCongViecNhanSu_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_QL_PhanCongCongViecNhanSu_Load(sender, e);
        }

        private void CV_QL_PhanCongCongViecNhanSu_barButtonItem_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_PhanCongCongViecNhanSuAdd = true;
            CV_QL_PhanCongCongViecNhanSu_Edit = false;
            CV_QL_PhanCongCongViecNhanSu_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void CV_QL_PhanCongCongViecNhanSu_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_ID))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_QL_PhanCongCongViecNhanSuAdd = false;
                CV_QL_PhanCongCongViecNhanSu_Edit = true;
                Lock_Unlock_Control_Input(true); //lock input
                Lock_Unlock_Control(false); // lock nut nhap hien thi nut luu
            }
        }

        private void CV_QL_PhanCongCongViecNhanSu_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
                    for (int i = 0; i < CV_QL_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSuChon))) // == true
                        {
                            string s = Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_ID));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_ID))))
                            {
                                CV_QL_PhanCongCongViecNhanSuPublic Public = new CV_QL_PhanCongCongViecNhanSuPublic();
                                Public.CV_QL_PhanCongCongViecNhanSu_ID = Convert.ToInt32(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_ID));
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_PhanCongCongViecNhanSu_DateEditor = DateTime.Now;
                                Public.CV_QL_PhanCongCongViecNhanSu_SuDung = BienToanCuc.HT_USER_Ten;
                                kq = clsPhanCongCongViecNhanSu.CV_QL_PhanCongCongViecNhanSu_Del(Public);
                            }
                            else
                            {
                                MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
                    }
                }
                TraVe_DongDLChon();
                if (kq > 0)
                {
                    MessageBox.Show("Xoá Thành Công!");
                    frmCV_QL_PhanCongCongViecNhanSu_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
        }

        private void CV_QL_PhanCongCongViecNhanSu_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_PhanCongCongViecNhanSuAdd = false;
            CV_QL_PhanCongCongViecNhanSu_Edit = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_QL_PhanCongCongViecNhanSu_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_QL_PhanCongCongViecNhanSuAdd == true || CV_QL_PhanCongCongViecNhanSu_Edit == true)
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
                    CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
                    for (int i = 0; i < CV_QL_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSuChon))) // == true
                        {

                            // gán vào đối tượng public
                            CV_QL_PhanCongCongViecNhanSuPublic Public = new CV_QL_PhanCongCongViecNhanSuPublic();
                            Public.CV_QL_PhanCongCongViecNhanSu_HienThi = true;
                            Public.CV_QL_PhanCongCongViecNhanSu_SuDung = BienToanCuc.HT_USER_Ten;
                            Public.CV_QL_PhanCongCongViecNhanSu_IDCongViec = Convert.ToInt32(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_TenCongViec));
                            Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuPhuTrach = Convert.ToInt32(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_NhanSuPhuTrach));
                            Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuThucHien = Convert.ToInt32(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_NhanSuThucHien));
                            Public.CV_QL_PhanCongCongViecNhanSu_NgayBatDau = Convert.ToDateTime(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_PhanCongCongViecNhanSu_NgayBatDau));
                            Public.CV_QL_PhanCongCongViecNhanSu_NgayKetThuc = Convert.ToDateTime(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_PhanCongCongViecNhanSu_NgayKetThuc));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_NhanSuPhoiHop))))
                            {
                                Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuPhoiHop = Convert.ToInt32(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_NhanSuPhoiHop));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_NhanSuKiemTra))))
                            {
                                Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuKiemTra = Convert.ToInt32(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_NhanSuKiemTra));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_DanhGia))))
                            {
                                Public.CV_QL_PhanCongCongViecNhanSu_DanhGia = CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_PhanCongCongViecNhanSu_DanhGia);
                            }
                            Public.CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien= CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_LyDo))))
                            {
                                Public.CV_QL_PhanCongCongViecNhanSu_LyDo = CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_PhanCongCongViecNhanSu_LyDo);
                            }
                            if (CV_QL_PhanCongCongViecNhanSuAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_PhanCongCongViecNhanSu_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = clsPhanCongCongViecNhanSu.CV_QL_PhanCongCongViecNhanSu_Insert(Public);
                            }

                            if (CV_QL_PhanCongCongViecNhanSu_Edit == true)
                            {
                                    Public.HT_USER_Create = Convert.ToInt32(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(HT_USER_Create));
                                    Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                    Public.CV_QL_PhanCongCongViecNhanSu_DateCreate = Convert.ToDateTime(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_DateCreate));
                                    Public.CV_QL_PhanCongCongViecNhanSu_DateEditor = DateTime.Now;
                                   Public.CV_QL_PhanCongCongViecNhanSu_ID = Convert.ToInt32(CV_QL_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_QL_PhanCongCongViecNhanSu_ID));
                                   kq = clsPhanCongCongViecNhanSu.CV_QL_PhanCongCongViecNhanSu_Update(Public);
                            }
                        }
                        CV_QL_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_QL_PhanCongCongViecNhanSuAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                    else if (CV_QL_PhanCongCongViecNhanSu_Edit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_QL_PhanCongCongViecNhanSu_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_QL_PhanCongCongViecNhanSuAdd = false;
                CV_QL_PhanCongCongViecNhanSu_Edit = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_QL_PhanCongCongViecNhanSu_gridControl_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadHamDungChung.PreviewPrintableComponent(CV_QL_PhanCongCongViecNhanSu_gridControl, CV_QL_PhanCongCongViecNhanSu_gridControl.LookAndFeel);
        }


        private void CV_QL_PhanCongCongViecNhanSu_bandedGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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
    }
}
