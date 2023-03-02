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
using System.Data.OleDb;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Columns;
namespace PhanCongCongViec.form.QuanLy
{
    public partial class frmCV_QL_CongViec_ImportExcel : Form
    {
        public frmCV_QL_CongViec_ImportExcel()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_QL_CongViec_BandedGridview);
        }

        string FileName_Import = "";
        CV_QL_CongViecBLL cls = new CV_QL_CongViecBLL();
        CV_QL_NhanSuBLL clsNhanSu = new CV_QL_NhanSuBLL();
        CV_HT_MucDoKhoBLL clsMucDoKho = new CV_HT_MucDoKhoBLL();
        CV_QL_NhomCongViecBLL clsNhomCongViec = new CV_QL_NhomCongViecBLL();
        CV_HT_LoaiCongViecBLL clsLoaiCongViec = new CV_HT_LoaiCongViecBLL();
        bool CV_QL_ImportAdd = false;
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
        private bool ValidInput()
        {
            if (FileName_Import.Trim() == "")
            {
                MessageBox.Show("Xin vui lòng chọn tập tin excel cần import!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_QL_CongViec_barButtonItem_Import.Enabled = Lock_Control;
            CV_QL_CongViec_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_QL_CongViec_barButtonItem_Undo.Enabled = !Lock_Control;
        }
        private void frmCV_QL_CongViec_ImportExcel_Load(object sender, EventArgs e)
        {

            //lookup edit nhom thuc hien 
            //CV_QL_CongViec_LookupEdit_NhomThucHien.DataSource = clsNhanSu.LoadCV_TT_NhanSu_LoadSTT();
            //CV_QL_CongViec_LookupEdit_NhomThucHien.DisplayMember = "CV_TT_NhanSu_NhomThucHien";
            //CV_QL_CongViec_LookupEdit_NhomThucHien.ValueMember = "CV_TT_NhanSu_ID";
            //CV_QL_CongViec_LookupEdit_NhomThucHien.PopupWidth = 400;
            //CV_QL_CongViec_LookupEdit_NhomThucHien.ShowFooter = false;
            //CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Clear();
            //CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_HoTen", "Họ tên", 200));
            //CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_DonVi", "Đơn vị", 150));
            //CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_NhomThucHien", "Nhóm thực hiện", 250));
            //CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_TrinhDo", "Trình độ", 150));
            //CV_QL_CongViec_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_KhaNangChuyenMon", "Khả năng chuyên môn", 300));


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
            CV_QL_CongViec_GridControl.DataSource = null;
            this.CV_QL_CongViec_ChiTietCongViec.OptionsColumn.ReadOnly = true;
            CV_QL_CongViec_barButtonItem_Luu.Enabled = false;
            CV_QL_CongViec_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_QL_CongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
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
        private void TraVe_DongDangTuongTac(int DongDangTuongTac)
        {
            CV_QL_CongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_CongViec_BandedGridview.RowCount; i++)
            {
                if (i == DongDangTuongTac)
                {
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
        System.Data.DataTable dt = new System.Data.DataTable();
        private void CV_QL_CongViec_barButtonItem_Import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                dt.Columns.Add("Loại Công Việc", typeof(string));
                dt.Columns.Add("Nhóm Công Việc Cha", typeof(string));
                dt.Columns.Add("Nhóm Công Việc Con", typeof(string));
                dt.Columns.Add("Tên Công Việc", typeof(string));
                //dt.Columns.Add("Chi tiết công việc", typeof(bool));
                dt.Columns.Add("Mô Tả Công Việc", typeof(string));
                dt.Columns.Add("Nhóm Thực Hiện", typeof(int));
                dt.Columns.Add("Khả năng Chuyên Môn", typeof(string));
                dt.Columns.Add("Mức độ khó", typeof(string));
                dt.Columns.Add("Số Ngày Thực Hiện", typeof(double));
                dt.Columns.Add("Số Giờ Thực Hiện", typeof(double));
                dt.Columns.Add("Số Phút Thực Hiện", typeof(double));

                //----Start SqlDataReader----- Đọc dữ liệu trả về trên nhiều dòng
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dt.Rows.Add(
                                    new object[] 
                                            {   
                                                //dr["Chọn"],
                                                dr["Loại công việc"],
                                                dr["Nhóm Công Việc Cha"],
                                                dr["Nhóm Công Việc Con"],
                                                dr["Tên Công Việc"],
                                                dr["Mô Tả Công Việc"],
                                                dr["Nhóm Thực Hiện"],
                                                dr["Khả năng Chuyên Môn"],
                                                dr["Mức độ khó"],
                                                dr["Số Ngày Thực Hiện"],
                                                dr["Số Giờ Thực Hiện"],
                                                dr["Số Phút Thực Hiện"]
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
                dt.Columns.Add("CV_QL_CongViecChon", typeof(bool), "True");
                dt.Columns.Add("CV_QL_CongViec_ID", typeof(int));
                dt.Columns.Add("Chi tiết công việc",typeof(bool));
                dt.Columns.Add("CV_QL_CongViec_TenFile", typeof(string));
                ////
                CV_QL_CongViec_GridControl.DataSource = dt;
                

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

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_QL_CongViec_ImportExcel_Load(sender, e);
        }

        private void CV_QL_CongViec_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            CV_QL_ImportAdd = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_QL_CongViec_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                                Public.NhomThucHien = CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_NhomThucHien).ToString();
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
                            if (CV_QL_ImportAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_CongViec_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_QL_CongViec_Add(Public);
                            }
                        }
                        CV_QL_CongViec_BandedGridview.MoveNext();
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
                frmCV_QL_CongViec_ImportExcel_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_QL_ImportAdd = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
