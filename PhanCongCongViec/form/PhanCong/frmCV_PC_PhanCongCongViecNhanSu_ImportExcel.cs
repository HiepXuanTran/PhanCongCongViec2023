using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Data.OleDb;

using System.Data.SqlClient;

using PCCV.Public;
using PCCV.BLL;

namespace PhanCongCongViec.form.QuanLy
{
    public partial class frmCV_PC_PhanCongCongViecNhanSu_ImportExcel : Form
    {
        public frmCV_PC_PhanCongCongViecNhanSu_ImportExcel()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_PC_PhanCongCongViecNhanSu_bandedGridView);
        }

        
        #region Dữ liệu dùng chung
        CV_PC_PhanCongCongViecNhanSuPublic PCCVPublic = new CV_PC_PhanCongCongViecNhanSuPublic();
        CV_PC_PhanCongCongViecNhanSuBLL clsPhanCongCongViecNhanSu = new CV_PC_PhanCongCongViecNhanSuBLL();
        CV_HT_VaiTroCongViecBLL clsVaiTro = new CV_HT_VaiTroCongViecBLL();
        CV_QL_CongViecBLL clsCongViec = new CV_QL_CongViecBLL();
        CV_QL_NhanSuBLL clsNhanSu = new CV_QL_NhanSuBLL();
        CV_HT_MucDoKhoBLL clsMucDoKho = new CV_HT_MucDoKhoBLL();

        //Hàm dùng chung
        bool CV_PC_ImportAdd = false;

        private void Lock_Unlock_Control_Input(bool Lock_Control) //Khóa và mở khóa điều khiển nhập dữ liệu
        {
            if (BienToanCuc.Lock_NhapDuLieu == true)
            {
                this.CV_PC_PhanCongCongViecNhanSu_TenLoaiCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_TenNhomCongViec1.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_TenNhomCongViec2.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_TenCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_MucDoKho.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_IDNhanSu.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_IDVaiTro.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_NgayBatDau.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_NgayKetThuc.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_DanhGia.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_LyDo.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien.OptionsColumn.ReadOnly = !Lock_Control;
            }
        }

        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_QL_NhomCongViec_barButtonItem_Import.Enabled = Lock_Control;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Undo.Enabled = !Lock_Control;
        }

        //Trả về dòng đang chọn đầu tiên
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
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_PC_PhanCongCongViecNhanSu_bandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_PC_PhanCongCongViecNhanSu_bandedGridView.GetFocusedRowCellValue(CV_PC_PhanCongCongViecNhanSuChon))) // == true
                {
                    return true;
                }
                CV_PC_PhanCongCongViecNhanSu_bandedGridView.MoveNext();
            }
            return false;
        }
        #endregion Dữ liệu dùng chung

        #region Button Import
        string FileName_Import = "";

        private bool ValidInput()
        {
            if (FileName_Import.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng chọn tập tin excel cần import!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        System.Data.DataTable dt = new System.Data.DataTable();
        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // Browse đến file cần import
            // Lấy đường dẫn file import vừa chọn
            ofd.Filter = "Excel Files (*.xls)|*.xls"; //Chỉ lựa chọn file excel *.xls ofd.Filter = "Excel Files (*.xls)|*.xls;*.xlsx";
            FileName_Import = ofd.ShowDialog() == DialogResult.OK ? ofd.FileName : "";

            //Kiểm tra file cần import có tồn tại ko
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
                //1
                //dt.Columns.Add("Chọn", typeof(bool));
                dt.Columns.Add("Tên loại công việc", typeof(string));
                dt.Columns.Add("Tên nhóm công việc 1", typeof(string));
                dt.Columns.Add("Tên nhóm công việc 2", typeof(string));
                dt.Columns.Add("Tên công việc", typeof(string));
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
                        dt.Rows.Add(
                                    new object[] 
                                            {   
                                                //2
                                                //dr["Chọn"],
                                                dr["Tên loại công việc"],
                                                dr["Tên nhóm công việc 1"],
                                                dr["Tên nhóm công việc 2"],
                                                dr["Tên công việc"],
                                                dr["Mức độ khó"],
                                                dr["Nhân sự"],
                                                dr["Vai trò"],
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
            CV_PC_ImportAdd = true;
            Lock_Unlock_Control_Input(true); //Mở khóa điều khiển nhập dữ liệu
            Lock_Unlock_Control(false); //Mở khóa lưu dữ liệu
        }
        #endregion Button Import

        private void frmCV_PC_PhanCongCongViecNhanSu_ImportExcel_Load(object sender, EventArgs e)
        {
            // Lookup Edit Tên Loại Công Việc
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

            // Lookup Edit Mức Độ Khó
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.DataSource = clsMucDoKho.LoadCV_HT_MucDoKho_LoadAll();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.DisplayMember = "CV_HT_MucDoKho_DoKhoCongViec";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.ValueMember = "CV_HT_MucDoKho_DoKhoCongViec";
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.PopupWidth = 400;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.ShowFooter = false;
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Clear();
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_MucDoKho_DoKhoCongViec", "Độ khó công việc", 50));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_MucDoKho_Mota", "Mô tả", 50));
            CV_PC_PhanCongCongViecNhanSu_LookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_MucDoKho_GhiChu", "Ghi chú", 50));

            // Lookup Edit Nhân Sự
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

            // start
            CV_PC_PhanCongCongViecNhanSu_gridControl.DataSource = clsPhanCongCongViecNhanSu.LoadCV_PC_PhanCongCongViecNhanSu();
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Luu.Enabled = false;
            CV_PC_PhanCongCongViecNhanSu_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            //lock_ConTrol_Always();
            CV_PC_PhanCongCongViecNhanSu_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Luu_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Undo_Click(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_PC_ImportAdd = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_PC_PhanCongCongViecNhanSu_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_PC_PhanCongCongViecNhanSu_ImportExcel_Load(sender, e);
        }
    }
}
