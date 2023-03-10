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
namespace Uneti.Online.form.HeThong
{
    public partial class frmCV_HT_KhaNangChuyenMon : Form
    {
        public frmCV_HT_KhaNangChuyenMon()
        {
            InitializeComponent();
        }
        CV_HT_KhaNangChuyenMonBLL clsKhaNangChuyenMon = new CV_HT_KhaNangChuyenMonBLL();
        bool CV_HT_KhaNangChuyenMonAdd = false;
        bool CV_HT_KhaNangChuyenMonEdit = false;
        private void Lock_Unlock_Control_Input(bool Lock_Control) //Khóa và mở khóa điều khiển nhập dữ liệu
        {
            if (BienToanCuc.Lock_NhapDuLieu == true)
            {
                this.CV_HT_KhaNangChuyenMon_TenKhaNang.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_HT_KhaNangChuyenMon_MoTa.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_HT_KhaNangChuyenMon_GhiChu.OptionsColumn.ReadOnly = !Lock_Control;
            }
        }
        private void TraVe_DongDLChon()
        {
            CV_HT_KhaNangChuyenMon_BandedGridView.MoveFirst();
            for (int i = 0; i < CV_HT_KhaNangChuyenMon_BandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMonChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_HT_KhaNangChuyenMon_BandedGridView.MoveNext();
            }
        }
        private void frmCV_HT_KhaNangChuyenMon_Load(object sender, EventArgs e)
        {
            CV_HT_KhaNangChuyenMon_gridControl.DataSource = clsKhaNangChuyenMon.LoadCV_HT_KhaNangChuyenMon_LoadAll();
            CV_HT_KhaNangChuyenMon_barButtonItem_Luu.Enabled = false;
            CV_HT_KhaNangChuyenMon_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_HT_KhaNangChuyenMon_BandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private bool KiemTra_NhapDuLieu()
        {
            CV_HT_KhaNangChuyenMon_BandedGridView.MoveFirst();
            for (int i = 0; i < CV_HT_KhaNangChuyenMon_BandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMonChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_TenKhaNang)))
                        )
                    )
                {
                    return false;
                }
                CV_HT_KhaNangChuyenMon_BandedGridView.MoveNext();
            }
            return true;
        }

        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_HT_KhaNangChuyenMon_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_HT_KhaNangChuyenMon_barButtonItem_Them.Enabled = Lock_Control;
            CV_HT_KhaNangChuyenMon_barButtonItem_Sua.Enabled = Lock_Control;
            CV_HT_KhaNangChuyenMon_barButtonItem_Xoa.Enabled = Lock_Control;
            CV_HT_KhaNangChuyenMon_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_HT_KhaNangChuyenMon_barButtonItem_Undo.Enabled = !Lock_Control;
        }
        // hàm trả về dòng dữ liệu đang tương tác
        private void TraVe_DongDangTuongTac(int DongDangTuongTac)
        {
            CV_HT_KhaNangChuyenMon_BandedGridView.MoveFirst();
            for (int i = 0; i < CV_HT_KhaNangChuyenMon_BandedGridView.RowCount; i++)
            {
                if (i == DongDangTuongTac)
                {
                    break;
                }
                CV_HT_KhaNangChuyenMon_BandedGridView.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_HT_KhaNangChuyenMon_BandedGridView.MoveFirst();
            for (int i = 0; i < CV_HT_KhaNangChuyenMon_BandedGridView.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMonChon))) // == true
                {
                    return true;
                }
                CV_HT_KhaNangChuyenMon_BandedGridView.MoveNext();
            }
            return false;
        }
        private void CV_HT_KhaNangChuyenMon_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_HT_KhaNangChuyenMon_Load(sender, e);
        }

        private void CV_HT_KhaNangChuyenMon_barButtonItem_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_HT_KhaNangChuyenMonAdd = true;
            CV_HT_KhaNangChuyenMonEdit = false;
            CV_HT_KhaNangChuyenMon_BandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }
        

        private void CV_HT_KhaNangChuyenMon_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMonChon))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_HT_KhaNangChuyenMonAdd = false;
                CV_HT_KhaNangChuyenMonEdit = true;
                Lock_Unlock_Control_Input(true); //lock input
                Lock_Unlock_Control(false); // lock nut nhap hien thi nut luu
            }
        }

        private void CV_HT_KhaNangChuyenMon_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                // dong tuong tac
                int DongTuongTac = 0;
                // tránh click nhầm
                int kq = -1;
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xoá?", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
                {
                    //Xoá từng dòng đã check
                    CV_HT_KhaNangChuyenMon_BandedGridView.MoveFirst();
                    for (int i = 0; i < CV_HT_KhaNangChuyenMon_BandedGridView.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMonChon))) // == true
                        {
                            DongTuongTac = i;
                            string s = Convert.ToString(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_Id));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_Id))))
                            {
                                // gan gia tri vao bien 
                                CV_HT_KhaNangChuyenMonPublic Public = new CV_HT_KhaNangChuyenMonPublic();
                                Public.CV_HT_KhaNangChuyenMon_Id = Convert.ToInt32(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_Id));
                                Public.HT_USER_Edit = BienToanCuc.HT_USER_ID;
                                Public.CV_HT_KhaNangChuyenMon_DateEdit = DateTime.Now;
                                Public.CV_HT_KhaNangChuyenMon_SuDung = BienToanCuc.HT_USER_Ten;
                                kq = clsKhaNangChuyenMon.CV_HT_KhaNangChuyenMon_Del(Public);
                            }
                            else
                            {
                                MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        CV_HT_KhaNangChuyenMon_BandedGridView.MoveNext();
                    }
                }
                TraVe_DongDangTuongTac(DongTuongTac);
                if (kq > 0)
                {
                    MessageBox.Show("Xoá Thành Công!");
                    frmCV_HT_KhaNangChuyenMon_Load(sender, e); // load lai form
                }
                else
                {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
        }

        private void CV_HT_KhaNangChuyenMon_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_HT_KhaNangChuyenMonAdd = false;
            CV_HT_KhaNangChuyenMonEdit = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_HT_KhaNangChuyenMon_barButtonItem_Print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadHamDungChung.PreviewPrintableComponent(CV_HT_KhaNangChuyenMon_gridControl, CV_HT_KhaNangChuyenMon_gridControl.LookAndFeel);
        }

        private void CV_HT_KhaNangChuyenMon_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_HT_KhaNangChuyenMonAdd == true || CV_HT_KhaNangChuyenMonEdit == true)
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
                    CV_HT_KhaNangChuyenMon_BandedGridView.MoveFirst();
                    for (int i = 0; i < CV_HT_KhaNangChuyenMon_BandedGridView.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMonChon))) // == true
                        {
                            // gán vào đối tượng puhlic
                            CV_HT_KhaNangChuyenMonPublic Public = new CV_HT_KhaNangChuyenMonPublic();
                            Public.CV_HT_KhaNangChuyenMon_TenKhaNang = Convert.ToString(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_TenKhaNang));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_MoTa))))
                            {
                                Public.CV_HT_KhaNangChuyenMon_MoTa = Convert.ToString(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_MoTa));
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_GhiChu))))
                            {
                                Public.CV_HT_KhaNangChuyenMon_GhiChu = Convert.ToString(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_GhiChu));
                            }
                            Public.CV_HT_KhaNangChuyenMon_SuDung = BienToanCuc.HT_USER_Ten;
                            Public.CV_HT_KhaNangChuyenMon_HienThi = true;
                            if (CV_HT_KhaNangChuyenMonAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_HT_KhaNangChuyenMon_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = clsKhaNangChuyenMon.CV_HT_KhaNangChuyenMon_Add(Public);
                            }

                            if (CV_HT_KhaNangChuyenMonEdit == true)
                            {
                                Public.HT_USER_Create = Convert.ToInt32(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(HT_USER_Create));
                                Public.HT_USER_Edit = BienToanCuc.HT_USER_ID;
                                Public.CV_HT_KhaNangChuyenMon_DateCreate = Convert.ToDateTime(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_DateCreate));
                                Public.CV_HT_KhaNangChuyenMon_DateEdit = DateTime.Now;
                                Public.CV_HT_KhaNangChuyenMon_Id = Convert.ToInt32(CV_HT_KhaNangChuyenMon_BandedGridView.GetFocusedRowCellValue(CV_HT_KhaNangChuyenMon_Id));
                                kq = clsKhaNangChuyenMon.CV_HT_KhaNangChuyenMon_Update(Public);
                            }
                        }
                        CV_HT_KhaNangChuyenMon_BandedGridView.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_HT_KhaNangChuyenMonAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                    else if (CV_HT_KhaNangChuyenMonEdit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_HT_KhaNangChuyenMon_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_HT_KhaNangChuyenMonAdd = false;
                CV_HT_KhaNangChuyenMonEdit = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
