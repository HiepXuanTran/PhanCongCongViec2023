using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PCCV.Public;
using PCCV.BLL;
using PhanCongCongVien.form;
namespace PhanCongCongVien
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
        private void frmCV_QL_NhomCongViec_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = cls.LoadCV_QL_NhomCongViec_LoadAll();
            barButtonItem_Luu.Enabled = false;
            barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false);
            Lock_Control_Input_Always();
            CV_QL_NhomCongViec_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            // Gán tạm Thông tin người dùng
           BienToanCuc.HT_USER_ID = 1;
           BienToanCuc.HT_USER_Ten = "Trần Xuân Hiệp" ;
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
        //luôn luôn khoá 6 cột cuối
        private void Lock_Control_Input_Always()
        {
            this.HT_USER_Create.OptionsColumn.ReadOnly = true;
            this.HT_USER_Editor.OptionsColumn.ReadOnly = true;
            this.CV_QL_NhomCongViec_DateCreate.OptionsColumn.ReadOnly = true;
            this.CV_QL_NhomCongViec_DateEditor.OptionsColumn.ReadOnly = true;
            this.CV_QL_NhomCongViec_HienThi.OptionsColumn.ReadOnly = true;
            this.CV_QL_NhomCongViec_SuDung.OptionsColumn.ReadOnly = true;

        }
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            barButtonItem_Refresh.Enabled = Lock_Control;
            barButtonItem_Them.Enabled = Lock_Control;
            barButtonItem_Sua.Enabled = Lock_Control;
            barButtonItem_Xoa.Enabled = Lock_Control;
            barButtonItem_Luu.Enabled = !Lock_Control;
            barButtonItem_Undo.Enabled = !Lock_Control;
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

        private void barButtonItem_Them_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_NhomCongViecAdd = true;
            CV_QL_NhomCongViecEdit = false;
            CV_QL_NhomCongViec_bandedGridView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
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

        private void barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                            Public.CV_QL_NhomCongViec_ID1 = Convert.ToInt32(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellValue(CV_QL_NhomCongViec_ID));
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

        private void barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_NhomCongViecAdd = false;
            CV_QL_NhomCongViecEdit = false;
            Lock_Unlock_Control(true);
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_QL_NhomCongViecAdd == true || CV_QL_NhomCongViecEdit == true)
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
                            
                            
                            
                            Public.CV_QL_NhomCongViec_HienThi1 = true;
                            Public.CV_QL_NhomCongViec_SuDung1 = BienToanCuc.HT_USER_Ten;
                            Public.CV_QL_NhomCongViec_TenNhomCongViec11 = CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_TenNhomCongViec1);
                            Public.CV_QL_NhomCongViec_TenNhomCongViec21 = CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_TenNhomCongViec2);
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_MoTa))))
                            {
                                Public.CV_QL_NhomCongViec_MoTa1 = CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_MoTa);
                            }
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_GhiChu))))
                            {
                                Public.CV_QL_NhomCongViec_GhiChu1 = CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_GhiChu);
                            }
                            if (CV_QL_NhomCongViecAdd == true)
                            {
                                Public.CV_QL_NhomCongViec_DateCreate1 = DateTime.Now;
                                Public.HT_USER_Create1 = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_QL_NhomCongViec_Add(Public);
                            }

                            if (CV_QL_NhomCongViecEdit == true)
                            {
                                Public.HT_USER_Editor1 = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_NhomCongViec_DateCreate1 = Convert.ToDateTime(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellValue(CV_QL_NhomCongViec_DateCreate));
                                Public.CV_QL_NhomCongViec_DateEditor1 = DateTime.Now;
                                 Public.CV_QL_NhomCongViec_ID1 = Convert.ToInt32(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellDisplayText(CV_QL_NhomCongViec_ID));
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

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_QL_NhomCongViec_Load(sender, e);
        }

//<<<<<<< HEAD
        //private void gridControl1_Click(object sender, EventArgs e)
        //{

//=======
        private void CV_QL_NhomCongViec_Edit_Click(object sender, EventArgs e)
        {
            BienToanCuc.idCongViec = Convert.ToInt32(CV_QL_NhomCongViec_bandedGridView.GetFocusedRowCellValue(CV_QL_NhomCongViec_ID));
            frmCV_TD_LichSuCongViec m_frmCV_TD_LichSuCongViec = new frmCV_TD_LichSuCongViec();
            m_frmCV_TD_LichSuCongViec.ShowDialog();
//>>>>>>> fbd2cbb60ce32ac92e1d2d09fce8143fc4c3ca2c
        }
    }
}
