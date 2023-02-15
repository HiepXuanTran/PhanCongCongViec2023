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
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;

namespace PhanCongCongViec.form.QuanLy

{
    public partial class frmCV_QL_ChiTietCongViec : Form
    {
        public frmCV_QL_ChiTietCongViec()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_QL_ChiTietCongViec_BandedGridview);
        }

        CV_QL_CongViecBLL clsCongViec = new CV_QL_CongViecBLL();
        bool CV_QL_ChiTietCongViecEdit = false;
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
            CV_QL_ChiTietCongViec_barButtonItem_Add.Enabled = Lock_Control;
            CV_QL_ChiTietCongViec_barButtonItem_Sua.Enabled = Lock_Control;
            CV_QL_ChiTietCongViec_barButtonItem_Xoa.Enabled = Lock_Control;
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

        private void CV_QL_ChiTietCongViec_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_QL_ChiTietCongViec_Load(sender, e);
        }

        private void frmCV_QL_ChiTietCongViec_Load(object sender, EventArgs e)
        {
            // lookup muc do kho
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.DisplayMember = "CV_QL_CongViec_MucDoKho";
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.ValueMember = "CV_QL_CongViec_ID";
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.PopupWidth = 400;
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.ShowFooter = false;
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Clear();
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 200));
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_QL_ChiTietCongViec_lookupEdit_MucDoKho.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));

            // load form ten cong viec
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.DataSource = clsCongViec.LoadCV_QL_CongViec();
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.DisplayMember = "CV_QL_CongViec_TenCongViec";
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.ValueMember = "CV_QL_CongViec_ID";
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.PopupWidth = 400;
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.ShowFooter = false;
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Clear();
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenLoaiCongViec", "Tên loại công việc", 200));
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec1", "Tên nhóm công việc 1", 150));
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenNhomCongViec2", "Tên nhóm công việc 2", 150));
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_TenCongViec", "Tên công việc", 150));
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MucDoKho", "Độ khó công việc", 150));
            CV_QL_ChiTietCongViec_lookupEdit_TenCongViec.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_QL_CongViec_MoTaCongViec", "Mô tả công việc", 150));

            CV_QL_ChiTietCongViec_GridControl.DataSource = cls.LoadCV_QL_ChiTietCongViec();
            CV_QL_ChiTietCongViec_barButtonItem_Luu.Enabled = false;
            CV_QL_ChiTietCongViec_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_QL_ChiTietCongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_QL_ChiTietCongViec_barButtonItem_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_ChiTietCongViecAdd = true;
            CV_QL_ChiTietCongViecEdit = false;
            CV_QL_ChiTietCongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void CV_QL_ChiTietCongViec_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_ID))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_QL_ChiTietCongViecAdd = false;
                CV_QL_ChiTietCongViecEdit = true;
                Lock_Unlock_Control_Input(true); //lock input
                Lock_Unlock_Control(false); // lock nut nhap hien thi nut luu
            }
        }

        private void CV_QL_ChiTietCongViec_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    CV_QL_ChiTietCongViec_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_QL_ChiTietCongViec_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViecChon))) // == true
                        {
                            string s = Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_ID));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_ID))))
                            {
                                CV_QL_ChiTietCongViecPublic Public = new CV_QL_ChiTietCongViecPublic();
                                Public.Id = Convert.ToInt32(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_ID));
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_ChiTietCongViec_DateEditor = DateTime.Now;
                                Public.CV_QL_ChiTietCongViec_SuDung = BienToanCuc.HT_USER_Ten;
                                Public.CV_QL_ChiTietCongViec_IDCongViec = Convert.ToInt32(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_TenCongViec));
                                kq = cls.CV_QL_ChiTietCongViec_Del(Public);
                            }
                            else
                            {
                                MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        CV_QL_ChiTietCongViec_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon();
                if (kq > 0)
                {
                    MessageBox.Show("Xoá Thành Công!");
                    frmCV_QL_ChiTietCongViec_Load(sender, e); // load lai form
                }
                else
                {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
        }

        private void CV_QL_ChiTietCongViec_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_QL_ChiTietCongViecAdd == true || CV_QL_ChiTietCongViecEdit == true)
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

                            if (CV_QL_ChiTietCongViecEdit == true)
                            {
                                Public.HT_USER_Create = Convert.ToInt32(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(HT_USER_Create));
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_ChiTietCongViec_DateCreate = Convert.ToDateTime(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_DateCreate));
                                Public.CV_QL_ChiTietCongViec_DateEditor = DateTime.Now;
                                Public.Id = Convert.ToInt32(CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_ChiTietCongViec_ID));
                                kq = cls.CV_QL_ChiTietCongViec_Edit(Public);
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
                    else if (CV_QL_ChiTietCongViecEdit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_QL_ChiTietCongViec_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_QL_ChiTietCongViecAdd = false;
                CV_QL_ChiTietCongViecEdit = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_QL_ChiTietCongViec_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_ChiTietCongViecAdd = false;
            CV_QL_ChiTietCongViecEdit = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_QL_ChiTietCongViec_btnEdit_TenFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
        //    if (KiemTra() == false)
        //    {
        //        MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }
        //    OpenFileDialog dlg = new OpenFileDialog();
        //    DialogResult dlgRes = dlg.ShowDialog();
        //    if (dlgRes != DialogResult.Cancel)
        //    {
        //        FileInfo Ten_File = new FileInfo(dlg.FileName);
        //        CV_QL_ChiTietCongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_ChiTietCongViec_TenFile, Ten_File.Name);
        //        CV_QL_ChiTietCongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_ChiTietCongViec_FileDinhKem, ReadFile(dlg.FileName));
        //    }
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

        private void CV_QL_ChiTietCongViec_barButtonItem_In_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void CV_QL_ChiTietCongViec_barButtonItem_DownloadDinhKem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_ChiTietCongViecPublic Public = new CV_QL_ChiTietCongViecPublic();
            try
            {
                //Start - Download
                Public.Id = int.Parse("0" + CV_QL_ChiTietCongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_ChiTietCongViec_ID));

                SqlDataReader dr = cls.LoadCV_QL_ChiTietCongViec_Load_R_Para_File(Public);
                dr.Read();

                string TenFile = "";
                CV_QL_ChiTietCongViec_SaveFileDinhKem.FileName = Convert.ToString(dr["CV_QL_ChiTietCongViec_TenFile"].ToString());

                DialogResult DR = CV_QL_ChiTietCongViec_SaveFileDinhKem.ShowDialog();
                if (DR == DialogResult.OK)
                {
                    TenFile = CV_QL_ChiTietCongViec_SaveFileDinhKem.FileName;
                    //Get File data from dataset row.
                    byte[] FileDinhKem = (byte[])dr["CV_QL_ChiTietCongViec_FileDinhKem"];

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

        private void frmCV_QL_ChiTietCongViec_Load_1(object sender, EventArgs e)
        {

        }
    }
}



