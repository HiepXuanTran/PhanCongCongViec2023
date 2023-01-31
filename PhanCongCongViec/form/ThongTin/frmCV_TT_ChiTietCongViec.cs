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

namespace PhanCongCongViec.form.ThongTin
{
    public partial class frmCV_TT_ChiTietCongViec : Form
    {
        public frmCV_TT_ChiTietCongViec()
        {
            InitializeComponent();
        }


        bool CV_TT_ChiTietCongViecEdit = false;
        bool CV_TT_ChiTietCongViecAdd = false;
        CV_TT_ChiTietCongViecBLL cls = new CV_TT_ChiTietCongViecBLL();
        // lock input 
        private void Lock_Unlock_Control_Input(bool Lock_Control) //Khóa và mở khóa điều khiển nhập dữ liệu
        {
            if (BienToanCuc.Lock_NhapDuLieu == true)
            {
                
                this.CV_TT_ChiTietCongViec_TenCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_ChiTietCongViec_CacBuocCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_ChiTietCongViec_MoTaBuocCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_ChiTietCongViec_FileDinhKem.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_ChiTietCongViec_MucDoKho.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_ChiTietCongViec_TongSoPhutThucHien.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_ChiTietCongViec_TongSoGioThucHien.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_ChiTietCongViec_TongSoNgayThucHien.OptionsColumn.ReadOnly = !Lock_Control;
            }
        }
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_TT_ChiTietCongViec_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_TT_ChiTietCongViec_barButtonItem_Add.Enabled = Lock_Control;
            CV_TT_ChiTietCongViec_barButtonItem_Sua.Enabled = Lock_Control;
            CV_TT_ChiTietCongViec_barButtonItem_Xoa.Enabled = Lock_Control;
            CV_TT_ChiTietCongViec_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_TT_ChiTietCongViec_barButtonItem_Undo.Enabled = !Lock_Control;
        }

        // check nhap du lieu chua xong
        private bool KiemTra_NhapDuLieu()
        {
            CV_TT_ChiTietCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_TT_ChiTietCongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViecChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_TenCongViec))) ||
                            
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_MucDoKho)))
                        )
                    )
                {
                    return false;
                }
                CV_TT_ChiTietCongViec_BandedGridview.MoveNext();
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
            CV_TT_ChiTietCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_TT_ChiTietCongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViecChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_TT_ChiTietCongViec_BandedGridview.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_TT_ChiTietCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_TT_ChiTietCongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViecChon))) // == true
                {
                    return true;
                }
                CV_TT_ChiTietCongViec_BandedGridview.MoveNext();
            }
            return false;
        }

        private void CV_TT_ChiTietCongViec_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_TT_ChiTietCongViec_Load(sender, e);
        }

        private void frmCV_TT_ChiTietCongViec_Load(object sender, EventArgs e)
        {
            CV_TT_ChiTietCongViec_GridControl.DataSource = cls.LoadCV_TT_ChiTietCongViec();
            CV_TT_ChiTietCongViec_barButtonItem_Luu.Enabled = false;
            CV_TT_ChiTietCongViec_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_TT_ChiTietCongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_TT_ChiTietCongViec_barButtonItem_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_TT_ChiTietCongViecAdd = true;
            CV_TT_ChiTietCongViecEdit = false;
            CV_TT_ChiTietCongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void CV_TT_ChiTietCongViec_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_ID))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_TT_ChiTietCongViecAdd = false;
                CV_TT_ChiTietCongViecEdit = true;
                Lock_Unlock_Control_Input(true); //lock input
                Lock_Unlock_Control(false); // lock nut nhap hien thi nut luu
            }
        }

        private void CV_TT_ChiTietCongViec_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    CV_TT_ChiTietCongViec_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_TT_ChiTietCongViec_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViecChon))) // == true
                        {
                            string s = Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_ID));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_ID))))
                            {
                                CV_TT_ChiTietCongViecPublic Public = new CV_TT_ChiTietCongViecPublic();
                                Public.Id = Convert.ToInt32(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_ID));
                                kq = cls.CV_TT_ChiTietCongViec_Del(Public);
                            }
                            else
                            {
                                MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        CV_TT_ChiTietCongViec_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon();
                if (kq > 0)
                {
                    MessageBox.Show("Xoá Thành Công!");
                    frmCV_TT_ChiTietCongViec_Load(sender, e); // load lai form
                }
                else
                {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
        }

        private void CV_TT_ChiTietCongViec_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_TT_ChiTietCongViecAdd == true || CV_TT_ChiTietCongViecEdit == true)
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
                    CV_TT_ChiTietCongViec_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_TT_ChiTietCongViec_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViecChon))) // == true
                        {
                            // gán vào đối tượng puhlic
                            CV_TT_ChiTietCongViecPublic Public = new CV_TT_ChiTietCongViecPublic();
                            Public.CV_TT_ChiTietCongViec_HienThi = true;
                            Public.CV_TT_ChiTietCongViec_SuDung = BienToanCuc.HT_USER_Ten;                            
                            Public.TenCongViec = CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_TT_ChiTietCongViec_TenCongViec);
                            Public.CacBuocCongViec = CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_TT_ChiTietCongViec_CacBuocCongViec);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_MoTaBuocCongViec))))
                            {
                                Public.MoTaBuocCongViec = CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_TT_ChiTietCongViec_MoTaBuocCongViec);
                            }                            
                            Public.MucDoKho = CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_MucDoKho).ToString();
                            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_FileDinhKem))))
                            {
                                Public.FileDinhKem = null;
                            }
                            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_TenFile))))
                            {
                                Public.TenFile = null;
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_FileDinhKem))))
                            {
                                Public.FileDinhKem = (byte[])CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_FileDinhKem);
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_TenFile))))
                            {
                                Public.TenFile = Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_TenFile));
                            }          
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_TongSoPhutThucHien))))
                            {
                                Public.SoPhutThucHien = Convert.ToDouble(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_TongSoPhutThucHien));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_TongSoGioThucHien))))
                            {
                                Public.SoGioThucHien = Convert.ToDouble(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_TongSoGioThucHien));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_TongSoNgayThucHien))))
                            {
                                Public.SoNgayThucHien = Convert.ToDouble(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_TongSoNgayThucHien));
                            }
                            if (CV_TT_ChiTietCongViecAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_TT_ChiTietCongViec_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_TT_ChiTietCongViec_Add(Public);
                            }

                            if (CV_TT_ChiTietCongViecEdit == true)
                            {
                                Public.HT_USER_Create = Convert.ToInt32(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(HT_USER_Create));
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_TT_ChiTietCongViec_DateCreate = Convert.ToDateTime(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_DateCreate));
                                Public.CV_TT_ChiTietCongViec_DateEditor = DateTime.Now;
                                Public.Id = Convert.ToInt32(CV_TT_ChiTietCongViec_BandedGridview.GetFocusedRowCellValue(CV_TT_ChiTietCongViec_ID));
                                kq = cls.CV_TT_ChiTietCongViec_Edit(Public);
                            }
                        }
                        CV_TT_ChiTietCongViec_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_TT_ChiTietCongViecAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                    else if (CV_TT_ChiTietCongViecEdit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_TT_ChiTietCongViec_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_TT_ChiTietCongViecAdd = false;
                CV_TT_ChiTietCongViecEdit = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_TT_ChiTietCongViec_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_TT_ChiTietCongViecAdd = false;
            CV_TT_ChiTietCongViecEdit = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_TT_ChiTietCongViec_btnEdit_TenFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
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
                FileInfo Ten_File = new FileInfo(dlg.FileName);
                CV_TT_ChiTietCongViec_BandedGridview.SetFocusedRowCellValue(CV_TT_ChiTietCongViec_TenFile, Ten_File.Name);
                CV_TT_ChiTietCongViec_BandedGridview.SetFocusedRowCellValue(CV_TT_ChiTietCongViec_FileDinhKem, ReadFile(dlg.FileName));
            }
        }
    }
}



