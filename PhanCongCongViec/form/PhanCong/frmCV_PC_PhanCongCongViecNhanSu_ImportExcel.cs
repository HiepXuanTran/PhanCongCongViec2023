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
using System.Data.OleDb;

namespace PhanCongCongViec.form.PhanCong
{
    public partial class frmCV_PC_PhanCongCongViecNhanSu_ImportExcel : Form
    {
        public frmCV_PC_PhanCongCongViecNhanSu_ImportExcel()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_PC_PhanCongCongViecNhanSu_bandedGridView);
        }

        string FileName_Import = "";
        CV_PC_PhanCongCongViecNhanSuPublic PCCVPublic = new CV_PC_PhanCongCongViecNhanSuPublic();
        CV_PC_PhanCongCongViecNhanSuBLL clsPhanCongCongViecNhanSu = new CV_PC_PhanCongCongViecNhanSuBLL();
        CV_HT_VaiTroCongViecBLL clsVaiTro = new CV_HT_VaiTroCongViecBLL();
        CV_QL_CongViecBLL clsCongViec = new CV_QL_CongViecBLL();
        CV_QL_NhanSuBLL clsNhanSu = new CV_QL_NhanSuBLL();
        CV_QL_CongViecPublic CVPublic = new CV_QL_CongViecPublic();
        CV_HT_VaiTroCongViecPublic VTPublic = new CV_HT_VaiTroCongViecPublic();
        CV_QL_NhanSuPublic NSPublic = new CV_QL_NhanSuPublic();
        bool CV_QL_ImportAdd = false;
        private bool ValidInput()
        {
            if (FileName_Import.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng chọn tập tin excel cần import!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }



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
        

        System.Data.DataTable dt = new System.Data.DataTable();

        private void frmCV_PC_PhanCongCongViecNhanSu_ImportExcel_Load(object sender, EventArgs e)
        {
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
            CV_PC_PhanCongCongViecNhanSu_gridControl.DataSource = null;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Luu.Enabled = false;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            lock_ConTrol_Always();
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Browse đến file cần import
            // Lấy đường dẫn file import vừa chọn
            ofd.Filter = "Excel Files (*.xls)|*.xls"; //Chỉ lựa chọn file excel *.xls ofd.Filter = "Excel Files (*.xls)|*.xls;*.xlsx";
            FileName_Import = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : "";

            if (!ValidInput())
                return;

            // Ðọc dữ liệu từ tập tin excel trả về DataTable
            string connectionString = "";
            if (Path.GetExtension(FileName_Import) == ".xls")
            {   //For Excel 97-03

                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName_Import.Trim() + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
                //string conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + s + ";Password=password;Extended Properties='Excel 8.0;HDR=YES'";
            }
            else if (Path.GetExtension(FileName_Import) == ".xlsx")
            {    //For Excel 07 and greater
                //connection string for that file which extantion is .xlsx  
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName_Import.Trim() + ";Extended Properties=\"Excel 12.0;HDR=Yes\"";
                //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName_Import.Trim() + ";Extended Properties="Excel 12.0 Xml;HDR=YES"";
                //string conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + s + ";Password=password;Extended Properties='Excel 8.0;HDR=YES'";
            }
            else
            {
                MessageBox.Show("Dữ liệu file lựa chọn không phải là file excel", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Tạo đối tượng kết nối
            OleDbConnection oledbConn = new OleDbConnection(connectionString);
            try
            {

                // Mở kết nối
                oledbConn.Open();

                OleDbCommand command = new OleDbCommand("SELECT	* FROM [db$]", oledbConn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                //DataSet ds = new DataSet();

                //Start: Tạo bảng và Nạp dữ liệu
                //dt.Columns.Add("Chọn", typeof(bool));
                dt.Columns.Add("Tên loại công việc", typeof(string));
                dt.Columns.Add("Tên nhóm công việc 1", typeof(string));
                dt.Columns.Add("Tên nhóm công việc 2", typeof(string));
                dt.Columns.Add("Tên công việc", typeof(string));
                //dt.Columns.Add("Chi tiết công việc", typeof(bool));
                dt.Columns.Add("Mức độ khó", typeof(string));
                dt.Columns.Add("Nhân sự", typeof(string));
                dt.Columns.Add("Vai trò", typeof(string));
                dt.Columns.Add("Ngày bắt đầu", typeof(DateTime));
                dt.Columns.Add("Ngày kết thúc", typeof(DateTime));
                dt.Columns.Add("Đánh giá", typeof(string));
                dt.Columns.Add("Lý do", typeof(string));
                dt.Columns.Add("Địa điểm thực hiện", typeof(string));

                //----Start SqlDataReader----- Đọc dữ liệu trả về trên nhiều dòng
                OleDbDataReader dr = command.ExecuteReader();

                while (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        string TenLoaiCongViec = dr["Tên loại công việc"].ToString();
                        string TenNhomCongViec1 = dr["Tên nhóm công việc 1"].ToString();
                        string TenNhomCongViec2 = dr["Tên nhóm công việc 2"].ToString();
                        string TenCongViec = dr["Tên công việc"].ToString();
                        string MucDoKho = dr["Mức độ khó"].ToString();
                        CVPublic.TenLoaiCongViec = TenLoaiCongViec;
                        CVPublic.TenNhomCongViec1 = TenNhomCongViec1;
                        CVPublic.TenNhomCongViec2 = TenNhomCongViec2;
                        CVPublic.TenCongViec = TenCongViec;
                        CVPublic.MucDoKho = MucDoKho;
                        DataTable dtid = clsCongViec.CV_QL_CongViec_ReturnID(CVPublic);// gọi hàm trả về id từ 5 biến trên
                        int id = -1;

                        string TenVaiTro = dr["Vai trò"].ToString();
                        VTPublic.CV_HT_VaiTroCongViec_VaiTroTrongCongViec = TenVaiTro;
                        DataTable dtidvt = clsVaiTro.CV_HT_VaiTroCongViec_ReturnID(VTPublic);
                        int idvt = -1;

                        string TenNhanSu = dr["Nhân sự"].ToString();
                        NSPublic.CV_QL_NhanSu_HoTen = TenNhanSu;
                        DataTable dtidns = clsNhanSu.CV_QL_NhanSu_ReturnID(NSPublic);
                        int idns = -1;

                        for (int i = 0; i < dtid.Rows.Count; i++)
                        {
                            id =Convert.ToInt32(dtid.Rows[i]["CV_QL_CongViec_ID"].ToString());
                        }
                        for (int i = 0; i < dtid.Rows.Count; i++)
                        {
                            idvt = Convert.ToInt32(dtidvt.Rows[i]["CV_HT_VaiTroCongViec_ID"].ToString());
                        }

                        for (int i = 0; i < dtid.Rows.Count; i++)
                        {
                            idns = Convert.ToInt32(dtidns.Rows[i]["CV_QL_NhanSu_ID"].ToString());
                        }
                            dt.Rows.Add(
                                        new object[] 
                                            {   
                                                id,
                                                id,
                                                id,
                                                id,
                                                id,
                                                idns,
                                                idvt,
                                                dr["Ngày bắt đầu"],
                                                dr["Ngày kết thúc"],
                                                dr["Đánh giá"],
                                                dr["Lý do"],
                                                dr["Địa điểm thực hiện"]
                                            }
                                        );
                    }
                    dr.NextResult();
                }
                dr.Dispose();
                dr.Close();
                //----End SqlDataReader----- Đọc dữ liệu trả về trên nhiều dòng
                //
                ////
                dt.Columns.Add("CV_PC_PhanCongCongViecNhanSuChon", typeof(bool), "True");
                dt.Columns.Add("CV_PC_PhanCongCongViecNhanSu_ID", typeof(int));
                ////
                CV_PC_PhanCongCongViecNhanSu_gridControl.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show("Có lỗi xảy ra khi thực hiện impord dữ liệu! \n Vui lòng kiểm tra lại: \n 1. Mẫu file excel import dữ liệu (mẫu được cung cấp sẵn từ phần mềm và có định dạng: *.xls), hoặc; \n 2. Liên hệ với quản trị để được hỗ trợ.", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                // Đóng chuỗi kết nối
                oledbConn.Close();
            }
            CV_QL_ImportAdd = true;
            Lock_Unlock_Control_Input(true); //Mở khóa điều khiển nhập dữ liệu
            Lock_Unlock_Control(false); //Mở khóa lưu dữ liệu
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_QL_ImportAdd == true)
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
                            // gán vào đối tượng puhlic
                            CV_PC_PhanCongCongViecNhanSuPublic Public = new CV_PC_PhanCongCongViecNhanSuPublic();
                            Public.CV_PC_PhanCongCongViecNhanSu_HienThi = true;
                            Public.CV_PC_PhanCongCongViecNhanSu_SuDung = BienToanCuc.HT_USER_Ten;
                            Public.CV_PC_PhanCongCongViecNhanSu_IDCongViec = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_TenCongViec));
                            Public.CV_PC_PhanCongCongViecNhanSu_IDUser = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_IDUser));
                            Public.CV_PC_PhanCongCongViecNhanSu_IDVaiTro = Convert.ToInt32(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_IdVaiTro));
                            Public.CV_PC_PhanCongCongViecNhanSu_NgayBatDau = Convert.ToDateTime(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_NgayBatDau).ToString());
                            Public.CV_PC_PhanCongCongViecNhanSu_NgayKetThuc = Convert.ToDateTime(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_NgayKetThuc).ToString());
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_DanhGia))))
                            {
                                Public.CV_PC_PhanCongCongViecNhanSu_DanhGia = CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_DanhGia);
                            }
                            Public.CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien = CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSu_LyDo))))
                            {
                                Public.CV_PC_PhanCongCongViecNhanSu_LyDo = CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellDisplayText(CV_PC_PhanCongCongViecNhanSu_LyDo);
                            }
                            if (CV_QL_ImportAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_PC_PhanCongCongViecNhanSu_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = clsPhanCongCongViecNhanSu.CV_PC_PhanCongCongViecNhanSu_Insert(Public);
                            }
                        }
                        CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_QL_ImportAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                }
                frmCV_PC_PhanCongCongViecNhanSu_ImportExcel_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_QL_ImportAdd = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_ImportAdd = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_PC_PhanCongCongViecNhanSu_ImportExcel_Load(sender, e);
        }


    }
}
