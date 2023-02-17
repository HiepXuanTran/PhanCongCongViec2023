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

namespace PhanCongCongViec.form.QuanLy
{
    public partial class frmCV_PC_PhanCongCongViecNhanSu_ImportExcel : Form
    {
        public frmCV_PC_PhanCongCongViecNhanSu_ImportExcel()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_PC_PhanCongCongViecNhanSu_bandedGridView);
        }

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
            CV_QL_NhomCongViec_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_QL_NhomCongViec_barButtonItem_Undo.Enabled = !Lock_Control;
        }

        private void CV_QL_NhomCongViec_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //
        }

        
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
        DataTable dt = new DataTable();
        private void CV_QL_NhomCongViec_barButtonItem_Import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                        dt.Rows.Add(
                                    new object[] 
                                            {   
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

        }
    }
}
