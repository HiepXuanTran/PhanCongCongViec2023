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
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;

namespace PhanCongCongViec.form.HeThong
{
    public partial class frmCV_HT_UuTienCongViec : Form
    {
        public frmCV_HT_UuTienCongViec()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_HT_UuTienCongViec_BandedGridview);
        }

        CV_HT_UuTienCongViecBLL cls = new CV_HT_UuTienCongViecBLL();
        bool CV_HT_UuTienCongViecEdit = false;
        bool CV_HT_UuTienCongViecAdd = false;

        #region Cho phép thực hiện thao tác CLICK phải chuột

        void Check_All_Click(object sender, EventArgs e)
        {
            CV_HT_UuTienCongViec_BandedGridview.ClearSelection();
            CV_HT_UuTienCongViec_BandedGridview.FocusedColumn = CV_HT_UuTienCongViec_BandedGridview.Columns["CV_HT_UuTienCongViecChon"];

            CV_HT_UuTienCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_HT_UuTienCongViec_BandedGridview.RowCount; i++)
            {
                CV_HT_UuTienCongViec_BandedGridview.SetFocusedRowCellValue(CV_HT_UuTienCongViecChon, true);
                CV_HT_UuTienCongViec_BandedGridview.MoveNext();
            }
            CV_HT_UuTienCongViec_BandedGridview.MoveFirst();
        }
        void No_Check_All_Click(object sender, EventArgs e)
        {
            CV_HT_UuTienCongViec_BandedGridview.ClearSelection();
            CV_HT_UuTienCongViec_BandedGridview.FocusedColumn = CV_HT_UuTienCongViec_BandedGridview.Columns["CV_HT_UuTienCongViecChon"];

            CV_HT_UuTienCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_HT_UuTienCongViec_BandedGridview.RowCount; i++)
            {
                CV_HT_UuTienCongViec_BandedGridview.SetFocusedRowCellValue(CV_HT_UuTienCongViecChon, false);
                CV_HT_UuTienCongViec_BandedGridview.MoveNext();
            }
            CV_HT_UuTienCongViec_BandedGridview.MoveFirst();
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

        private void Lock_Unlock_Control_Input(bool Lock_Control) //Khóa và mở khóa điều khiển nhập dữ liệu
        {
            if (BienToanCuc.Lock_NhapDuLieu == true)
            {
                this.CV_HT_UuTienCongViec_GhiChu.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_HT_UuTienCongViec_MoTa.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_HT_UuTienCongViec_LoaiUuTien.OptionsColumn.ReadOnly = !Lock_Control;
            }
        }
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_HT_UuTienCongViec_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_HT_UuTienCongViec_barButtonItem_Add.Enabled = Lock_Control;
            CV_HT_UuTienCongViec_barButtonItem_Sua.Enabled = Lock_Control;
            CV_HT_UuTienCongViec_barButtonItem_Xoa.Enabled = Lock_Control;
            CV_HT_UuTienCongViec_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_HT_UuTienCongViec_barButtonItem_Undo.Enabled = !Lock_Control;
        }

        // check nhap du lieu chua xong
        private bool KiemTra_NhapDuLieu()
        {
            CV_HT_UuTienCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_HT_UuTienCongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViecChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViec_LoaiUuTien))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViec_MoTa)))
                        )
                    )
                {
                    return false;
                }
                CV_HT_UuTienCongViec_BandedGridview.MoveNext();
            }
            return true;
        }

        //trả về dòng đang chọn đầu tiên
        private void TraVe_DongDLChon()
        {
            CV_HT_UuTienCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_HT_UuTienCongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViecChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_HT_UuTienCongViec_BandedGridview.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_HT_UuTienCongViec_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_HT_UuTienCongViec_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViecChon))) // == true
                {
                    return true;
                }
                CV_HT_UuTienCongViec_BandedGridview.MoveNext();
            }
            return false;
        }

        private void frmCV_HT_UuTienCongViec_Load(object sender, EventArgs e)
        {
            CV_HT_UuTienCongViec_GridControl.DataSource = cls.LoadCV_HT_UuTienCongViec_LoadAll();
            CV_HT_UuTienCongViec_barButtonItem_Luu.Enabled = false;
            CV_HT_UuTienCongViec_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_HT_UuTienCongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_HT_UuTienCongViec_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_HT_UuTienCongViec_Load(sender, e);
        }

        private void CV_HT_UuTienCongViec_barButtonItem_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_HT_UuTienCongViecAdd = true;
            CV_HT_UuTienCongViecEdit = false;
            CV_HT_UuTienCongViec_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void CV_HT_UuTienCongViec_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViec_ID))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_HT_UuTienCongViecAdd = false;
                CV_HT_UuTienCongViecEdit = true;
                Lock_Unlock_Control_Input(true); //lock input
                Lock_Unlock_Control(false); // lock nut nhap hien thi nut luu
            }
        }

        private void CV_HT_UuTienCongViec_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    CV_HT_UuTienCongViec_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_HT_UuTienCongViec_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViecChon))) // == true
                        {
                            string s = Convert.ToString(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViec_ID));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViec_ID))))
                            {
                                CV_HT_UuTienCongViecPublic Public = new CV_HT_UuTienCongViecPublic();
                                Public.CV_HT_UuTienCongViec_ID = Convert.ToInt32(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViec_ID));
                                Public.CV_HT_UuTienCongViec_DateEditor = DateTime.Now;
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_HT_UuTienCongViec_SuDung = BienToanCuc.HT_USER_Ten;
                                kq = cls.CV_HT_UuTienCongViec_Del(Public);
                            }
                            else
                            {
                                MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        CV_HT_UuTienCongViec_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon();
                if (kq > 0)
                {
                    MessageBox.Show("Xoá Thành Công!");
                    frmCV_HT_UuTienCongViec_Load(sender, e); // load lai form
                }
                else
                {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
        }

        private void CV_HT_UuTienCongViec_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_HT_UuTienCongViecAdd == true || CV_HT_UuTienCongViecEdit == true)
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
                    CV_HT_UuTienCongViec_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_HT_UuTienCongViec_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViecChon))) // == true
                        {
                            // gán vào đối tượng puhlic
                            CV_HT_UuTienCongViecPublic Public = new CV_HT_UuTienCongViecPublic();
                            Public.CV_HT_UuTienCongViec_HienThi = true;
                            Public.CV_HT_UuTienCongViec_SuDung = BienToanCuc.HT_USER_Ten;
                            Public.CV_HT_UuTienCongViec_LoaiUuTien = CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_HT_UuTienCongViec_LoaiUuTien);
                            Public.CV_HT_UuTienCongViec_MoTa = CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_HT_UuTienCongViec_MoTa);
                            Public.CV_HT_UuTienCongViec_GhiChu = CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellDisplayText(CV_HT_UuTienCongViec_GhiChu);

                           
                            if (CV_HT_UuTienCongViecAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_HT_UuTienCongViec_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_HT_UuTienCongViec_Add(Public);
                            }

                            if (CV_HT_UuTienCongViecEdit == true)
                            {
                                Public.HT_USER_Create = Convert.ToInt32(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(HT_USER_Create));
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_HT_UuTienCongViec_DateCreate = Convert.ToDateTime(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViec_DateCreate));
                                Public.CV_HT_UuTienCongViec_DateEditor = DateTime.Now;
                                Public.CV_HT_UuTienCongViec_ID = Convert.ToInt32(CV_HT_UuTienCongViec_BandedGridview.GetFocusedRowCellValue(CV_HT_UuTienCongViec_ID));
                                kq = cls.CV_HT_UuTienCongViec_Edit(Public);
                            }
                        }
                        CV_HT_UuTienCongViec_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_HT_UuTienCongViecAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                    else if (CV_HT_UuTienCongViecEdit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_HT_UuTienCongViec_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_HT_UuTienCongViecAdd = false;
                CV_HT_UuTienCongViecEdit = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_HT_UuTienCongViec_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_HT_UuTienCongViecAdd = false;
            CV_HT_UuTienCongViecEdit = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_HT_UuTienCongViec_barButtonItem_In_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadHamDungChung.PreviewPrintableComponent(CV_HT_UuTienCongViec_GridControl, CV_HT_UuTienCongViec_GridControl.LookAndFeel);
        }

        private void CV_HT_UuTienCongViec_BandedGridview_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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
    }
}
