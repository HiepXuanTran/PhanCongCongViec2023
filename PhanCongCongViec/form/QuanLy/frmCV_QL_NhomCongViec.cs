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
using DevExpress.XtraBars;
namespace PhanCongCongViec.form.QuanLy
{
    public partial class frmCV_QL_NhomCongViec : Form
    {
        public frmCV_QL_NhomCongViec()
        {
            InitializeComponent();
        }

        bool CV_QL_NhomCongViecAdd = false;
        bool CV_QL_NhomCongViecEdit = false;
        CV_QL_NhomCongViecBLL cls = new CV_QL_NhomCongViecBLL();

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
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_QL_NhomCongViec_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_QL_NhomCongViec_barButtonItem_Them.Enabled = Lock_Control;
            CV_QL_NhomCongViec_barButtonItem_Sua.Enabled = Lock_Control;
            CV_QL_NhomCongViec_barButtonItem_Xoa.Enabled = Lock_Control;
            CV_QL_NhomCongViec_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_QL_NhomCongViec_barButtonItem_Undo.Enabled = !Lock_Control;
        }
        //Kiểm tra nhập đủ dữ liệu vào các ô chưa
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
        //trả về dòng đang chọn đầu tiên
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

        //public void UnVisibleControls()
        //{
        //    BarButtonItem[] items = new BarButtonItem[] { barButtonItem_Them, barButtonItem_Copy, barButtonItem_Sua, barButtonItem_Xoa, barButtonItem_Luu };

        //    foreach (BarButtonItem item in items)
        //    {
        //        item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        //    }
        //}

        //public void VisibleControls()
        //{
        //    BarButtonItem[] items = new BarButtonItem[] { CV_QL_NhomCongViec_barButtonItem_Them, CV_QL_NhomCongViec_barButtonItem_Sua, CV_QL_NhomCongViec_barButtonItem_Xoa, CV_QL_NhomCongViec_barButtonItem_Luu };
        //    if (BienToanCuc.MaNguoiDung == "0")
        //    {
        //        //Quyền quản trị               
        //        foreach (BarButtonItem item in items)
        //        {
        //            item.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        //        }
        //    }
        //    else
        //    {
        //        Public_HT_PQ_USER.HT_USER_ID = int.Parse("0" + BienToanCuc.MaNguoiDung);
        //        //Public.HT_ROOT_Form = this.Name.Replace("btn", "frm");
        //        Public_HT_PQ_USER.HT_ROOT_Form = this.Name;
        //        Public_HT_PQ_USER.HT_ROOT_Active = true;

        //        SqlDataReader dr = cls_HT_PQ_USER.LoadHT_PQ_USER_R_MaND(Public_HT_PQ_USER);
        //        dr.Read();

        //        //Toàn quyền - Quyền xem                
        //        foreach (BarButtonItem item in items)
        //        {
        //            if (Convert.ToBoolean(dr[0]) == true)
        //            {
        //                item.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        //            }
        //            if (Convert.ToBoolean(dr[1]) == true)
        //            {
        //                item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        //            }
        //        }

        //        //Quyền thêm
        //        if (Convert.ToBoolean(dr[2]) == true)
        //        {
        //            barButtonItem_Them.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        //            barButtonItem_Copy.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        //            barButtonItem_Luu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        //        }

        //        //Quyền xóa
        //        if (Convert.ToBoolean(dr[3]) == true)
        //        {
        //            barButtonItem_Xoa.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        //            barButtonItem_Luu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        //        }

        //        //Quyền sửa
        //        if (Convert.ToBoolean(dr[4]) == true)
        //        {
        //            barButtonItem_Sua.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        //            barButtonItem_Luu.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        //        }
        //        dr.Dispose();
        //        dr.Close();
        //    }
        //}
        private void frmCV_QL_NhomCongViec_Load(object sender, EventArgs e)
        {
            CV_QL_NhomCongViec_GridControl.DataSource = cls.LoadCV_QL_NhomCongViec_LoadAll();
            CV_QL_NhomCongViec_barButtonItem_Luu.Enabled = false;
            CV_QL_NhomCongViec_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false);
            CV_QL_NhomCongViec_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_QL_NhomCongViec_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_QL_NhomCongViec_Load(sender, e);
        }

        private void CV_QL_NhomCongViec_barButtonItem_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_NhomCongViecAdd = true;
            CV_QL_NhomCongViecEdit = false;
            CV_QL_NhomCongViec_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void CV_QL_NhomCongViec_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // check xem đã chọn dữ liệu trên lưới chưa
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_ID))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_QL_NhomCongViecAdd = false;
                CV_QL_NhomCongViecEdit = true;
                Lock_Unlock_Control_Input(true);
                Lock_Unlock_Control(false);
            }
        }

        private void CV_QL_NhomCongViec_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // tránh click nhầm
            int kq = -1;
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                //Xoá từng dòng đã check
                CV_QL_NhomCongViec_bandedGridView.MoveFirst();
                for (int i = 0; i < CV_QL_NhomCongViec_bandedGridView.RowCount; i++)
                {
                    if (Convert.ToBoolean(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellValue(CV_QL_NhomCongViecChon))) // == true
                    {
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_ID))))
                        {
                            CV_QL_NhomCongViecPublic Public = new CV_QL_NhomCongViecPublic();
                            Public.CV_QL_NhomCongViec_ID = Convert.ToInt32(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellValue(CV_QL_NhomCongViec_ID));
                            Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                            Public.CV_QL_NhomCongViec_DateEditor = DateTime.Now;
                            Public.CV_QL_NhomCongViec_SuDung = BienToanCuc.HT_USER_Ten;
                            kq = cls.CV_QL_NhomCongViec_Del(Public);
                        }
                        else
                        {
                            MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    CV_QL_NhomCongViec_bandedGridView.MoveNext();
                }
            }
            TraVe_DongDLChon();
            if (kq > 0)
            {
                MessageBox.Show("Xoá Thành Công!");
                frmCV_QL_NhomCongViec_Load(sender, e);
            }
        }

        private void CV_QL_NhomCongViec_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_QL_NhomCongViecAdd == true || CV_QL_NhomCongViecEdit == true)
                {
                    bool s = KiemTra();
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
                            if (CV_QL_NhomCongViecAdd == true)
                            {
                                Public.CV_QL_NhomCongViec_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_QL_NhomCongViec_Add(Public);
                            }

                            if (CV_QL_NhomCongViecEdit == true)
                            {
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_NhomCongViec_DateCreate = Convert.ToDateTime(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellValue(CV_QL_NhomCongViec_DateCreate));
                                Public.CV_QL_NhomCongViec_DateEditor = DateTime.Now;
                                Public.CV_QL_NhomCongViec_ID = Convert.ToInt32(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_ID));
                                kq = cls.CV_QL_NhomCongViec_Edit(Public);
                            }
                        }
                        CV_QL_NhomCongViec_bandedGridView.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_QL_NhomCongViecAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                    else if (CV_QL_NhomCongViecEdit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_QL_NhomCongViec_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_QL_NhomCongViecAdd = false;
                CV_QL_NhomCongViecEdit = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_QL_NhomCongViec_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_NhomCongViecAdd = false;
            CV_QL_NhomCongViecEdit = false;
            Lock_Unlock_Control(true);
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

    }
}
