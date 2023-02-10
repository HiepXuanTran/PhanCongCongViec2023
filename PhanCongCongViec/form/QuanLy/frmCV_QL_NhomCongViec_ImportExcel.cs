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
    public partial class frmCV_QL_NhomCongViec_ImportExcel : Form
    {
        public frmCV_QL_NhomCongViec_ImportExcel()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_QL_NhomCongViec_bandedGridView);
        }


        string FileName_Import = "";
        CV_QL_NhomCongViecPublic NhomCongViecPublic = new CV_QL_NhomCongViecPublic();
        CV_QL_NhomCongViecBLL cls = new CV_QL_NhomCongViecBLL();
        bool CV_QL_ImportAdd = false;

        void Check_All_Click(object sender, EventArgs e)
        {
            CV_QL_NhomCongViec_bandedGridView.ClearSelection();
            CV_QL_NhomCongViec_bandedGridView.FocusedColumn = CV_QL_NhomCongViec_bandedGridView.Columns["CV_QL_NhomCongViecChon"];

            CV_QL_NhomCongViec_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_NhomCongViec_bandedGridView.RowCount; i++)
            {
                CV_QL_NhomCongViec_bandedGridView.SetFocusedRowCellValue(CV_QL_NhomCongViecChon, true);
                CV_QL_NhomCongViec_bandedGridView.MoveNext();
            }
            CV_QL_NhomCongViec_bandedGridView.MoveFirst();
        }
        void No_Check_All_Click(object sender, EventArgs e)
        {
            CV_QL_NhomCongViec_bandedGridView.ClearSelection();
            CV_QL_NhomCongViec_bandedGridView.FocusedColumn = CV_QL_NhomCongViec_bandedGridView.Columns["CV_QL_NhomCongViecChon"];

            CV_QL_NhomCongViec_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_NhomCongViec_bandedGridView.RowCount; i++)
            {
                CV_QL_NhomCongViec_bandedGridView.SetFocusedRowCellValue(CV_QL_NhomCongViecChon, false);
                CV_QL_NhomCongViec_bandedGridView.MoveNext();
            }
            CV_QL_NhomCongViec_bandedGridView.MoveFirst();
        }

        private void Lock_Unlock_Control_Input(bool Lock_Control) //Khóa và mở khóa điều khiển nhập dữ liệu
        {
            if (BienToanCuc.Lock_NhapDuLieu == true)
            {
                this.CV_QL_NhomCongViec_TenNhomCongViec1.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_NhomCongViec_TenNhomCongViec2.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_NhomCongViec_TenNhomCongViec2.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_NhomCongViec_MoTa.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_NhomCongViec_GhiChu.OptionsColumn.ReadOnly = !Lock_Control;
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
            CV_QL_NhomCongViec_barButtonItem_Import.Enabled = Lock_Control;
            CV_QL_NhomCongViec_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_QL_NhomCongViec_barButtonItem_Undo.Enabled = !Lock_Control;
        }

        private bool KiemTra_NhapDuLieu()
        {
            CV_QL_NhomCongViec_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_NhomCongViec_bandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellValue(CV_QL_NhomCongViecChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_TenNhomCongViec1))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_TenNhomCongViec2)))
                        )
                    )
                {
                    return false;
                }
                CV_QL_NhomCongViec_bandedGridView.MoveNext();
            }
            return true;
        }


        private void TraVe_DongDLChon()
        {
            CV_QL_NhomCongViec_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_NhomCongViec_bandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellValue(CV_QL_NhomCongViecChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_QL_NhomCongViec_bandedGridView.MoveNext();
            }
        }

        private void TraVe_DongDangTuongTac(int DongDangTuongTac)
        {
            CV_QL_NhomCongViec_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_NhomCongViec_bandedGridView.RowCount; i++)
            {
                if (i == DongDangTuongTac)
                {
                    break;
                }
                CV_QL_NhomCongViec_bandedGridView.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_QL_NhomCongViec_bandedGridView.MoveFirst();
            for (int i = 0; i < CV_QL_NhomCongViec_bandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellValue(CV_QL_NhomCongViecChon))) // == true
                {
                    return true;
                }
                CV_QL_NhomCongViec_bandedGridView.MoveNext();
            }
            return false;
        }



        private void frmCV_QL_NhomCongViec_ImportExcel_Load(object sender, EventArgs e)
        {
            CV_QL_NhomCongViec_GridControl.DataSource = null;
            CV_QL_NhomCongViec_barButtonItem_Luu.Enabled = false;
            CV_QL_NhomCongViec_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_QL_NhomCongViec_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }
        System.Data.DataTable dt = new System.Data.DataTable();
        private void CV_QL_NhomCongViec_barButtonItem_Import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

                OleDbCommand command = new OleDbCommand("SELECT	* FROM [Sheet1$]", oledbConn);
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                //DataSet ds = new DataSet();

                //Start: Tạo bảng và Nạp dữ liệu
                //dt.Columns.Add("Chọn", typeof(bool));
                dt.Columns.Add("Nhóm Công Việc Cha", typeof(string));
                dt.Columns.Add("Nhóm Công Việc Con", typeof(string));
                dt.Columns.Add("Mô Tả", typeof(string));
                dt.Columns.Add("Ghi Chú", typeof(string));
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
                                                dr["Nhóm Công Việc Cha"],
                                                dr["Nhóm Công Việc Con"],
                                                dr["Mô Tả"],
                                                dr["Ghi Chú"]
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
                dt.Columns.Add("CV_QL_NhomCongViecChon", typeof(bool), "True");
                dt.Columns.Add("CV_QL_NhomCongViec_ID", typeof(int));
                ////
                CV_QL_NhomCongViec_GridControl.DataSource = dt;

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

        private void CV_QL_NhomCongViec_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    CV_QL_NhomCongViec_bandedGridView.MoveFirst();
                    for (int i = 0; i < CV_QL_NhomCongViec_bandedGridView.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellValue(CV_QL_NhomCongViecChon))) // == true
                        {
                            CV_QL_NhomCongViecPublic Public = new CV_QL_NhomCongViecPublic();



                            Public.CV_QL_NhomCongViec_HienThi = true;
                            Public.CV_QL_NhomCongViec_SuDung = BienToanCuc.HT_USER_Ten;
                            Public.CV_QL_NhomCongViec_TenNhomCongViec1 = CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_TenNhomCongViec1);
                            Public.CV_QL_NhomCongViec_TenNhomCongViec2 = CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_TenNhomCongViec2);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_MoTa))))
                            {
                                Public.CV_QL_NhomCongViec_MoTa = CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_MoTa);
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_GhiChu))))
                            {
                                Public.CV_QL_NhomCongViec_GhiChu = CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_GhiChu);
                            }
                            if (CV_QL_ImportAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_NhomCongViec_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_QL_NhomCongViec_Add(Public);
                            }
                        }
                       CV_QL_NhomCongViec_bandedGridView.MoveNext();
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
                frmCV_QL_NhomCongViec_ImportExcel_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_QL_ImportAdd = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_QL_NhomCongViec_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_ImportAdd = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void barButtonItem1CV_QL_NhomCongViec_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_QL_NhomCongViec_ImportExcel_Load(sender, e);
        }

        private void CV_QL_NhomCongViec_bandedGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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

                

            }
        
        }


               
    }
}
