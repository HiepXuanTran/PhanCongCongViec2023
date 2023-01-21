using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PCCV.Public;
using PCCV.BLL;

namespace PhanCongCongViec.form.HeThong
{
    public partial class frmCV_HT_MucDoKho : Form
    {
        public frmCV_HT_MucDoKho()
        {
            InitializeComponent();
        }
        bool CV_HT_MucDoKhoAdd = false;
        bool CV_HT_MucDoKhoEdit = false;
        CV_HT_MucDoKhoBLL clsMucDoKho = new CV_HT_MucDoKhoBLL();
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
                this.CV_HT_MucDoKho_DoKhoCongViec.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_HT_MucDoKho_Mota.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_HT_MucDoKho_GhiChu.OptionsColumn.ReadOnly = !Lock_Control;
            }
        }
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_HT_MucDoKho_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_HT_MucDoKho_barButtonItem_Them.Enabled = Lock_Control;
            CV_HT_MucDoKho_barButtonItem_Sua.Enabled = Lock_Control;
            CV_HT_MucDoKho_barButtonItem_Xoa.Enabled = Lock_Control;
            CV_HT_MucDoKho_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_HT_MucDoKho_barButtonItem_Undo.Enabled = !Lock_Control;
        }

        // check nhap du lieu chua xong
        private bool KiemTra_NhapDuLieu()
        {
            CV_HT_MucDoKho_BandedGridView.MoveFirst();
            for (int i = 0; i < CV_HT_MucDoKho_BandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKhoChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_DoKhoCongViec)))
                        )
                    )
                {
                    return false;
                }
                CV_HT_MucDoKho_BandedGridView.MoveNext();
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
            CV_HT_MucDoKho_BandedGridView.MoveFirst();
            for (int i = 0; i < CV_HT_MucDoKho_BandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKhoChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_HT_MucDoKho_BandedGridView.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_HT_MucDoKho_BandedGridView.MoveFirst();
            for (int i = 0; i < CV_HT_MucDoKho_BandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKhoChon))) // == true
                {
                    return true;
                }
                CV_HT_MucDoKho_BandedGridView.MoveNext();
            }
            return false;
        }
        private void frmCV_HT_MucDoKho_Load(object sender, EventArgs e)
        {
            CV_HT_MucDoKho_GridControl.DataSource = clsMucDoKho.LoadCV_HT_MucDoKho_LoadAll();
            CV_HT_MucDoKho_barButtonItem_Luu.Enabled = false;
            CV_HT_MucDoKho_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_HT_MucDoKho_BandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_HT_MucDoKho_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_HT_MucDoKho_Load(sender, e);
        }

        private void CV_HT_MucDoKho_barButtonItem_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_HT_MucDoKhoAdd = true;
            CV_HT_MucDoKhoEdit = false;
            CV_HT_MucDoKho_BandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void CV_HT_MucDoKho_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_Id))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_HT_MucDoKhoAdd = false;
                CV_HT_MucDoKhoEdit = true;
                Lock_Unlock_Control_Input(true); //lock input
                Lock_Unlock_Control(false); // lock nut nhap hien thi nut luu
            }
        }

        private void CV_HT_MucDoKho_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    CV_HT_MucDoKho_BandedGridView.MoveFirst();
                    for (int i = 0; i < CV_HT_MucDoKho_BandedGridView.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKhoChon))) // == true
                        {
                            string s = Convert.ToString(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_Id));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_Id))))
                            {
                                // gan gia tri vao bien 
                                //CV_HT_LoaiCongViecPublic Public = new CV_HT_LoaiCongViecPublic();
                                CV_HT_MucDoKhoPublic Public = new CV_HT_MucDoKhoPublic();
                                Public.CV_HT_MucDoKho_Id = Convert.ToInt32(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_Id));
                                Public.HT_USER_Edit = BienToanCuc.HT_USER_ID;
                                Public.CV_HT_MucDoKho_DateEdit = DateTime.Now;
                                Public.CV_HT_MucDoKho_Sudung = BienToanCuc.HT_USER_Ten;
                                kq = clsMucDoKho.CV_HT_MucDoKho_Del(Public);
                            }
                            else
                            {
                                MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        CV_HT_MucDoKho_BandedGridView.MoveNext();
                    }
                }
                TraVe_DongDLChon();
                if (kq > 0)
                {
                    MessageBox.Show("Xoá Thành Công!");
                    frmCV_HT_MucDoKho_Load(sender, e); // load lai form
                }
                else
                {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
        }

        private void CV_HT_MucDoKho_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_HT_MucDoKhoAdd == true || CV_HT_MucDoKhoEdit == true)
                {
                    bool ki = KiemTra();
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
                    CV_HT_MucDoKho_BandedGridView.MoveFirst();
                    for (int i = 0; i < CV_HT_MucDoKho_BandedGridView.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKhoChon))) // == true
                        {
                            // gán vào đối tượng puhlic
                            CV_HT_MucDoKhoPublic Public = new CV_HT_MucDoKhoPublic();
                            Public.CV_HT_MucDoKho_DoKhoCongViec = Convert.ToString(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_DoKhoCongViec));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_Mota))))
                            {
                                Public.CV_HT_MucDoKho_Mota = Convert.ToString(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_Mota));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_GhiChu))))
                            {
                                Public.CV_HT_MucDoKho_GhiChu = Convert.ToString(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_GhiChu));
                            }
                            Public.CV_HT_MucDoKho_Sudung = BienToanCuc.HT_USER_Ten;
                            Public.CV_HT_MucDoKho_HienThi = true;
                            if (CV_HT_MucDoKhoAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_HT_MucDoKho_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = clsMucDoKho.CV_HT_MucDoKho_Add(Public);
                            }

                            if (CV_HT_MucDoKhoEdit == true)
                            {
                                Public.HT_USER_Create = Convert.ToInt32(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(HT_USER_Create));
                                Public.HT_USER_Edit = BienToanCuc.HT_USER_ID;
                                Public.CV_HT_MucDoKho_DateCreate = Convert.ToDateTime(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_DateCreate));
                                Public.CV_HT_MucDoKho_DateEdit = DateTime.Now;
                                Public.CV_HT_MucDoKho_Id = Convert.ToInt32(CV_HT_MucDoKho_BandedGridView.GetFocusedRowCellValue(CV_HT_MucDoKho_Id));
                                kq = clsMucDoKho.CV_HT_MucDoKho_Update(Public);
                            }
                        }
                        CV_HT_MucDoKho_BandedGridView.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_HT_MucDoKhoAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                    else if (CV_HT_MucDoKhoEdit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_HT_MucDoKho_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_HT_MucDoKhoAdd = false;
                CV_HT_MucDoKhoEdit = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_HT_MucDoKho_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_HT_MucDoKhoAdd = false;
            CV_HT_MucDoKhoEdit = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }


    }
}
