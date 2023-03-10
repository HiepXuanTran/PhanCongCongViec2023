using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Uneti.BLL;
using Uneti.Public;
using System.IO;
using System.Data.SqlClient;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;
using System.Data.OleDb;
namespace Uneti.Online.form.QuanLy
{
    public partial class frmCV_QL_ChiTietCongViec_ImportExcel : Form
    {
        public frmCV_QL_ChiTietCongViec_ImportExcel()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_QL_ChiTietCongViec_BandedGridview);
        }
        string FileName_Import = "";
        CV_QL_CongViecBLL clsCongViec = new CV_QL_CongViecBLL();
        bool CV_QL_ChiTietCongViecAdd = false;
        CV_QL_ChiTietCongViecBLL cls = new CV_QL_ChiTietCongViecBLL();

        #region Cho phép thực hiện thao tác CLICK phải chuột

        void Check_All_Click(object sender, EventArgs e)
        {
            CV_QL_ChiTietCongViec_BandedGridview.ClearSelection();
            CV_QL_ChiTietCongViec_BandedGridview.FocusedColumn = CV_QL_ChiTietCongViec_BandedGridview.Columns["CV_QL_ChiTietCongViecChon"];

            CV_QL_ChiTietCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_ChiTietCongViec_BandedGridview.RowCount; i++)
            {
                CV_QL_ChiTietCongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_ChiTietCongViecChon, true);
                CV_QL_ChiTietCongViec_BandedGridview.MoveNext();
            }
            CV_QL_ChiTietCongViec_BandedGridview.MoveFirst();
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
        void No_Check_All_Click(object sender, EventArgs e)
        {
            CV_QL_ChiTietCongViec_BandedGridview.ClearSelection();
            CV_QL_ChiTietCongViec_BandedGridview.FocusedColumn = CV_QL_ChiTietCongViec_BandedGridview.Columns["CV_QL_ChiTietCongViecChon"];

            CV_QL_ChiTietCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_ChiTietCongViec_BandedGridview.RowCount; i++)
            {
                CV_QL_ChiTietCongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_ChiTietCongViecChon, false);
                CV_QL_ChiTietCongViec_BandedGridview.MoveNext();
            }
            CV_QL_ChiTietCongViec_BandedGridview.MoveFirst();
        }
        //void Ghim_Trai_Click(object sender, EventArgs e)
        //{
        //    if (gridBand_Chung.Fixed == FixedStyle.Left)
        //    {
        //        gridBand_Chung.Fixed = FixedStyle.None;
        //    }
        //    else
        //    {
        //        gridBand_Chung.Fixed = FixedStyle.Left;
        //    }
        //}
        #endregion
        // lock input 
        private void Lock_Unlock_Control_Input(bool Lock_Control) //Khóa và mở khóa điều khiển nhập dữ liệu
        {
            if (BienToanCuc.Lock_NhapDuLieu == true)
            {

                this.CV_QL_ChiTietCongViec_TenCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_ChiTietCongViec_CacBuocCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_ChiTietCongViec_MoTaBuocCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_ChiTietCongViec_FileDinhKem.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_ChiTietCongViec_MucDoKho.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_ChiTietCongViec_TongSoPhutThucHien.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_ChiTietCongViec_TongSoGioThucHien.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_ChiTietCongViec_TongSoNgayThucHien.OptionsColumn.ReadOnly = !Lock_Control;
            }
        }
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_QL_ChiTietCongViec_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_QL_ChiTietCongViec_barButtonItem_Import.Enabled = Lock_Control;
            CV_QL_ChiTietCongViec_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_QL_ChiTietCongViec_barButtonItem_Undo.Enabled = !Lock_Control;
        }
        // check nhap du lieu chua xong
        private bool KiemTra_NhapDuLieu()
        {
            CV_QL_ChiTietCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_ChiTietCongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViecChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TenCongViec))) ||

                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_MucDoKho)))
                        )
                    )
                {
                    return false;
                }
                CV_QL_ChiTietCongViec_BandedGridview.MoveNext();
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
            CV_QL_ChiTietCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_ChiTietCongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViecChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_QL_ChiTietCongViec_BandedGridview.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_QL_ChiTietCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_ChiTietCongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViecChon))) // == true
                {
                    return true;
                }
                CV_QL_ChiTietCongViec_BandedGridview.MoveNext();
            }
            return false;
        }


        private void frmCV_QL_ChiTietCongViec_ImportExcel_Load(object sender, EventArgs e)
        {
            // lookup muc do kho
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.DisplayMember = "CV_QL_CongViec_MucDoKho";
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.ValueMember = "CV_QL_CongViec_ID";
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.PopupWidth = 1400;
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.ShowFooter = false;
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Clear();
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 400));
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 350));
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 350));
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 350));
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 550));

            // load form ten cong viec
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.DisplayMember = "CV_QL_CongViec_TenCongViec";
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.ValueMember = "CV_QL_CongViec_ID";
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.PopupWidth = 1400;
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.ShowFooter = false;
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Clear();
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 450));
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 350));
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 350));
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 350));
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 550));

            CV_QL_ChiTietCongViec_GridControl.DataSource = null;
            CV_QL_ChiTietCongViec_barButtonItem_Luu.Enabled = false;
            CV_QL_ChiTietCongViec_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_QL_ChiTietCongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_QL_ChiTietCongViec_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_QL_ChiTietCongViec_ImportExcel_Load(sender, e);
        }

        private void CV_QL_ChiTietCongViec_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_QL_ChiTietCongViecAdd == true)
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
                    CV_QL_ChiTietCongViec_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_QL_ChiTietCongViec_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViecChon))) // == true
                        {
                            // gán vào đối tượng puhlic
                            CV_QL_ChiTietCongViecPublic Public = new CV_QL_ChiTietCongViecPublic();
                            Public.CV_QL_ChiTietCongViec_HienThi = true;
                            Public.CV_QL_ChiTietCongViec_SuDung = BienToanCuc.HT_USER_Ten;
                            Public.CV_QL_ChiTietCongViec_IDCongViec = Convert.ToInt32(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TenCongViec));
                            Public.CacBuocCongViec = CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_ChiTietCongViec_CacBuocCongViec);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_MoTaBuocCongViec))))
                            {
                                Public.MoTaBuocCongViec = CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_ChiTietCongViec_MoTaBuocCongViec);
                            }
                            Public.MucDoKho = CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_MucDoKho).ToString();
                            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_FileDinhKem))))
                            {
                                Public.FileDinhKem = null;
                            }
                            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TenFile))))
                            {
                                Public.TenFile = null;
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_FileDinhKem))))
                            {
                                //Public.FileDinhKem = (byte[])CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_FileDinhKem);
                                Public.FileDinhKem = ReadFile(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_FileDinhKem).ToString());
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TenFile))))
                            {
                                Public.TenFile = Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TenFile));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TongSoPhutThucHien))))
                            {
                                Public.SoPhutThucHien = Convert.ToDouble(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TongSoPhutThucHien));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TongSoGioThucHien))))
                            {
                                Public.SoGioThucHien = Convert.ToDouble(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TongSoGioThucHien));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TongSoNgayThucHien))))
                            {
                                Public.SoNgayThucHien = Convert.ToDouble(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TongSoNgayThucHien));
                            }
                            if (CV_QL_ChiTietCongViecAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_ChiTietCongViec_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_QL_ChiTietCongViec_Add(Public);
                            }
                        }
                        CV_QL_ChiTietCongViec_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_QL_ChiTietCongViecAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                }
                frmCV_QL_ChiTietCongViec_ImportExcel_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_QL_ChiTietCongViecAdd = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_QL_ChiTietCongViec_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_ChiTietCongViecAdd = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_QL_ChiTietCongViec_btnEdit_TenFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult dlgRes = dlg.ShowDialog();
            if (dlgRes != DialogResult.Cancel)
            {
                //FocusRowCell("CV_QL_CongViec_FileDinhKem");
                CV_QL_ChiTietCongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_ChiTietCongViec_FileDinhKem, dlg.FileName);
                FileInfo Ten_File = new FileInfo(dlg.FileName);
                //FocusRowCell("CV_QL_CongViec_TenFile");
                CV_QL_ChiTietCongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_ChiTietCongViec_TenFile, Ten_File.Name);
            }
        }

        private void CV_QL_ChiTietCongViec_barButtonItem_Print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadHamDungChung.PreviewPrintableComponent(CV_QL_ChiTietCongViec_GridControl, CV_QL_ChiTietCongViec_GridControl.LookAndFeel);
        }

        private void CV_QL_ChiTietCongViec_BandedGridview_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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

                //DXMenuItem Ghim_Trai = new DXMenuItem("Ghim/Nhả ghim nhóm cột bên trái");
                //Ghim_Trai.Shortcut = Shortcut.Ctrl3;
                //Ghim_Trai.Click += new EventHandler(Ghim_Trai_Click);
                //menu.Items.Add(Ghim_Trai);

            }
        }

        System.Data.DataTable dt = new System.Data.DataTable();
        private void CV_QL_ChiTietCongViec_barButtonItem_Import_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                dt.Columns.Add("Tên công việc", typeof(string));
                dt.Columns.Add("Các bước công việc", typeof(string));
                dt.Columns.Add("Mô tả bước công việc", typeof(string));
                dt.Columns.Add("Mức độ khó", typeof(string));
                dt.Columns.Add("Tổng số phút thực hiện", typeof(string));
                dt.Columns.Add("Tổng số giờ thực hiện", typeof(string));
                dt.Columns.Add("Tổng số ngày thực hiện", typeof(string));
                //----Start SqlDataReader----- Đọc dữ liệu trả về trên nhiều dòng
                OleDbDataReader dr = command.ExecuteReader();

                while (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        int id = -1;
                        // get id từ tên import vào
                        string tenCongViec = dr["Tên công việc"].ToString();
                        CV_QL_CongViecPublic Cv = new CV_QL_CongViecPublic();
                        Cv.TenCongViec = tenCongViec;
                        DataTable dttenCongViec = clsCongViec.CV_QL_CongViec_ReturnID_fromTen(Cv);
                        for (int i = 0; i < dttenCongViec.Rows.Count; i++)
                        {
                            id = Convert.ToInt32(dttenCongViec.Rows[i]["CV_QL_CongViec_ID"].ToString());
                        }
                            dt.Rows.Add(
                                        new object[] 
                                            {   
                                                //dr["Chọn"],
                                                id,
                                                dr["Các bước công việc"],
                                                dr["Mô tả bước công việc"],
                                                dr["Mức độ khó"],
                                                dr["Tổng số phút thực hiện"],
                                                dr["Tổng số giờ thực hiện"],
                                                dr["Tổng số ngày thực hiện"]
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
                dt.Columns.Add("CV_QL_ChiTietCongViecChon", typeof(bool), "True");
                dt.Columns.Add("CV_QL_ChiTietCongViec_ID", typeof(int));
                ////
                CV_QL_ChiTietCongViec_GridControl.DataSource = dt;

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
            CV_QL_ChiTietCongViecAdd = true;
            Lock_Unlock_Control_Input(true); //Mở khóa điều khiển nhập dữ liệu
            Lock_Unlock_Control(false); //Mở khóa lưu dữ liệu
        }
    }
}
