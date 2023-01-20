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
namespace PhanCongCongVien.form.QuanLy
{
    public partial class frmCV_QL_CongViec : Form
    {
        public frmCV_QL_CongViec()
        {
            InitializeComponent();
        }
        bool CV_QL_CongViecEdit = false;
        bool CV_QL_CongViecAdd = false;
        // lock 6 cột cuối
        CV_QL_CongViecBLL cls = new CV_QL_CongViecBLL();
        private void Lock_Control_Input_Always()
        {
            this.HT_USER_Create.OptionsColumn.ReadOnly = true;
            this.HT_USER_Editor.OptionsColumn.ReadOnly = true;
            this.CV_QL_CongViec_DateCreate.OptionsColumn.ReadOnly = true;
            this.CV_QL_CongViec_DateEditor.OptionsColumn.ReadOnly = true;
            this.CV_QL_CongViec_HienThi.OptionsColumn.ReadOnly = true;
            this.CV_QL_CongViec_SuDung.OptionsColumn.ReadOnly = true;

        }
        // lock input 
        private void Lock_Unlock_Control_Input(bool Lock_Control) //Khóa và mở khóa điều khiển nhập dữ liệu
        {
            if (BienToanCuc.Lock_NhapDuLieu == true)
            {
                this.CV_QL_CongViec_TenLoaiCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_TenNhomCongViec1.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_TenNhomCongViec2.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_TenCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_CongViec_ChiTietCongViec.OptionsColumn.ReadOnly = !Lock_Control;
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
            barButtonItem_Refresh.Enabled = Lock_Control;
            barButtonItem_Add.Enabled = Lock_Control;
            barButtonItem_Sua.Enabled = Lock_Control;
            barButtonItem_Xoa.Enabled = Lock_Control;
            barButtonItem_Luu.Enabled = !Lock_Control;
            barButtonItem_Undo.Enabled = !Lock_Control;
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

        private void frmCV_QL_CongViec_Load(object sender, EventArgs e)
        {
            // load lookupedit nhan su 
            CV_QL_NhomThucHien_LookupEdit.DataSource = cls.LoadNhanSu();
            CV_QL_NhomThucHien_LookupEdit.DisplayMember = "CV_TT_NhanSu_NhomThucHien";
            CV_QL_NhomThucHien_LookupEdit.ValueMember = "CV_TT_NhanSu_NhomThucHien";
            CV_QL_NhomThucHien_LookupEdit.PopupWidth = 50;
            CV_QL_NhomThucHien_LookupEdit.ShowFooter = false;
            CV_QL_NhomThucHien_LookupEdit.Columns.Clear();
            CV_QL_NhomThucHien_LookupEdit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_TT_NhanSu_NhomThucHien", "Tên nhóm", 50));

            // load form nhan su
            gridControl1.DataSource = cls.LoadCV_QL_CongViec();
            barButtonItem_Luu.Enabled = false;
            barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false);
            Lock_Control_Input_Always();
            CV_QL_CongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            // Gán tạm Thông tin người dùng
            BienToanCuc.HT_USER_ID = 1;
            BienToanCuc.HT_USER_Ten = "Trần Xuân Hiệp";
        }

        private void barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_QL_CongViec_Load(sender, e);
        }

        // btn them
        private void barButtonItem_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_CongViecAdd = true;
            CV_QL_CongViecEdit = false;
            CV_QL_CongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // check xem đã chọn dữ liệu trên lưới chưa
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
                Lock_Unlock_Control_Input(true);
                Lock_Unlock_Control(false);
            }
        }

        private void barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ID))))
                            {
                                CV_QL_CongViecPublic Public = new CV_QL_CongViecPublic();
                                Public.Id = Convert.ToInt32(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_ID));
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
                    frmCV_QL_CongViec_Load(sender, e);
                }
                else {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
            
            
        }

        private void barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                            Public.CV_QL_CongViec_HienThi1 = true;
                            Public.CV_QL_CongViec_SuDung1 = BienToanCuc.HT_USER_Ten;
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
                            if ( string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem))))
                            {
                                    Public.FileDinhKem = null;
                               // Public.FileDinhKem.SetValue(null,0);
                            }
                            else{
                                Public.FileDinhKem = (byte[])CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem);
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
                                Public.CV_QL_CongViec_DateCreate1 = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_QL_CongViec_Add(Public);
                            }

                            if (CV_QL_CongViecEdit == true)
                            {
                                Public.HT_USER_Create = Convert.ToInt32(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(HT_USER_Create));
                                Public.HT_USER_Editor1 = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_CongViec_DateCreate1 = Convert.ToDateTime(CV_QL_CongViec_BandedGridview.GetFocusedRowCellValue(CV_QL_CongViec_DateCreate));
                                Public.CV_QL_CongViec_DateEditor1 = DateTime.Now;
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

        private void barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            CV_QL_CongViecAdd = false;
            CV_QL_CongViecEdit = false;
            Lock_Unlock_Control(true);
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_QL_FIleDinhKem_BtnEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult dlgRes = dlg.ShowDialog();
            if (dlgRes != DialogResult.Cancel)
            {
                CV_QL_CongViec_BandedGridview.SetFocusedRowCellValue(CV_QL_CongViec_FileDinhKem, ReadFile(dlg.FileName));
            }
        }
    }
}
