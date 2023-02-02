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
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;

namespace PhanCongCongViec.form.ThongTin
{
    public partial class frmCV_TT_NhanSu : Form
    {
        public frmCV_TT_NhanSu()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_TT_NhanSu_BandedGridview);
        }
        CV_TT_NhanSuBLL cls = new CV_TT_NhanSuBLL();
        bool CV_TT_NhanSuEdit = false;
        bool CV_TT_NhanSuAdd = false;

        #region Cho phép thực hiện thao tác CLICK phải chuột

        void Check_All_Click(object sender, EventArgs e)
        {
            CV_TT_NhanSu_BandedGridview.ClearSelection();
            CV_TT_NhanSu_BandedGridview.FocusedColumn = CV_TT_NhanSu_BandedGridview.Columns["CV_TT_NhanSu_MaNhanSu"];

            CV_TT_NhanSu_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_TT_NhanSu_BandedGridview.RowCount; i++)
            {
                CV_TT_NhanSu_BandedGridview.SetFocusedRowCellValue(CV_TT_NhanSuChon, true);
                CV_TT_NhanSu_BandedGridview.MoveNext();
            }
            CV_TT_NhanSu_BandedGridview.MoveFirst();
        }
        void No_Check_All_Click(object sender, EventArgs e)
        {
            CV_TT_NhanSu_BandedGridview.ClearSelection();
            CV_TT_NhanSu_BandedGridview.FocusedColumn = CV_TT_NhanSu_BandedGridview.Columns["CV_TT_NhanSu_HoTen"];

            CV_TT_NhanSu_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_TT_NhanSu_BandedGridview.RowCount; i++)
            {
                CV_TT_NhanSu_BandedGridview.SetFocusedRowCellValue(CV_TT_NhanSuChon, false);
                CV_TT_NhanSu_BandedGridview.MoveNext();
            }
            CV_TT_NhanSu_BandedGridview.MoveFirst();
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
                this.CV_TT_NhanSu_MaNhanSu.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_NhanSu_HoTen.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_NhanSu_DonVi.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_NhanSu_NhomThucHien.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_NhanSu_KhaNangChuyenMon.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_NhanSu_TrinhDo.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_TT_NhanSu_KhaNangChuyenMon.OptionsColumn.ReadOnly = !Lock_Control;
            }
        }
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_TT_NhanSu_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_TT_NhanSu_barButtonItem_Add.Enabled = Lock_Control;
            CV_TT_NhanSu_barButtonItem_Sua.Enabled = Lock_Control;
            CV_TT_NhanSu_barButtonItem_Xoa.Enabled = Lock_Control;
            CV_TT_NhanSu_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_TT_NhanSu_barButtonItem_Undo.Enabled = !Lock_Control;
        }

        // check nhap du lieu chua xong
        private bool KiemTra_NhapDuLieu()
        {
            CV_TT_NhanSu_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_TT_NhanSu_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSuChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_MaNhanSu))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_HoTen))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_DonVi))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_NhomThucHien))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_TrinhDo))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_KhaNangChuyenMon)))
                        )
                    )
                {
                    return false;
                }
                CV_TT_NhanSu_BandedGridview.MoveNext();
            }
            return true;
        }

        //trả về dòng đang chọn đầu tiên
        private void TraVe_DongDLChon()
        {
            CV_TT_NhanSu_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_TT_NhanSu_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSuChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_TT_NhanSu_BandedGridview.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_TT_NhanSu_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_TT_NhanSu_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSuChon))) // == true
                {
                    return true;
                }
                CV_TT_NhanSu_BandedGridview.MoveNext();
            }
            return false;
        }

        private void frmCV_TT_NhanSu_Load(object sender, EventArgs e)
        {
            CV_TT_NhanSu_GridControl.DataSource = cls.LoadCV_TT_NhanSu_LoadAll();
            CV_TT_NhanSu_barButtonItem_Luu.Enabled = false;
            CV_TT_NhanSu_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_TT_NhanSu_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_TT_NhanSu_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    CV_TT_NhanSu_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_TT_NhanSu_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSuChon))) // == true
                        {
                            string s = Convert.ToString(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_ID));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_ID))))
                            {
                                CV_TT_NhanSuPublic Public = new CV_TT_NhanSuPublic();
                                Public.CV_TT_NhanSu_ID = Convert.ToInt32(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_ID));
                                kq = cls.CV_TT_NhanSu_Del(Public);
                            }
                            else
                            {
                                MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        CV_TT_NhanSu_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon();
                if (kq > 0)
                {
                    MessageBox.Show("Xoá Thành Công!");
                    frmCV_TT_NhanSu_Load(sender, e); // load lai form
                }
                else
                {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
        }

        private void CV_TT_NhanSu_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_TT_NhanSu_Load(sender, e);
        }

        private void CV_TT_NhanSu_barButtonItem_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_TT_NhanSuAdd = true;
            CV_TT_NhanSuEdit = false;
            CV_TT_NhanSu_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void CV_TT_NhanSu_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_ID))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_TT_NhanSuAdd = false;
                CV_TT_NhanSuEdit = true;
                Lock_Unlock_Control_Input(true); //lock input
                Lock_Unlock_Control(false); // lock nut nhap hien thi nut luu
            }
        }

        private void CV_TT_NhanSu_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_TT_NhanSuAdd == true || CV_TT_NhanSuEdit == true)
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
                    CV_TT_NhanSu_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_TT_NhanSu_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSuChon))) // == true
                        {
                            // gán vào đối tượng puhlic
                            CV_TT_NhanSuPublic Public = new CV_TT_NhanSuPublic();
                            Public.CV_TT_NhanSu_HienThi = true;
                            Public.CV_TT_NhanSu_SuDung = BienToanCuc.HT_USER_Ten;
                            Public.CV_TT_NhanSu_MaNhanSu = CV_TT_NhanSu_BandedGridview.GetFocusedRowCellDisplayText(CV_TT_NhanSu_MaNhanSu);
                            Public.CV_TT_NhanSu_HoTen = CV_TT_NhanSu_BandedGridview.GetFocusedRowCellDisplayText(CV_TT_NhanSu_HoTen);
                            Public.CV_TT_NhanSu_DonVi = CV_TT_NhanSu_BandedGridview.GetFocusedRowCellDisplayText(CV_TT_NhanSu_DonVi);

                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_NhomThucHien))))
                            {
                                Public.CV_TT_NhanSu_NhomThucHien = Convert.ToInt32(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_NhomThucHien));
                            }
                            Public.CV_TT_NhanSu_TrinhDo = CV_TT_NhanSu_BandedGridview.GetFocusedRowCellDisplayText(CV_TT_NhanSu_TrinhDo);
                            Public.CV_TT_NhanSu_KhaNangChuyenMon = CV_TT_NhanSu_BandedGridview.GetFocusedRowCellDisplayText(CV_TT_NhanSu_KhaNangChuyenMon);
                            if (CV_TT_NhanSuAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_TT_NhanSu_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_TT_NhanSu_Add(Public);
                            }

                            if (CV_TT_NhanSuEdit == true)
                            {
                                Public.HT_USER_Create = Convert.ToInt32(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(HT_USER_Create));
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_TT_NhanSu_DateCreate = Convert.ToDateTime(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_DateCreate));
                                Public.CV_TT_NhanSu_DateEditor = DateTime.Now;
                                Public.CV_TT_NhanSu_ID = Convert.ToInt32(CV_TT_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_TT_NhanSu_ID));
                                kq = cls.CV_TT_NhanSu_Edit(Public);
                            }
                        }
                        CV_TT_NhanSu_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_TT_NhanSuAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                    else if (CV_TT_NhanSuEdit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_TT_NhanSu_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_TT_NhanSuAdd = false;
                CV_TT_NhanSuEdit = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_TT_NhanSu_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_TT_NhanSuAdd = false;
            CV_TT_NhanSuEdit = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_TT_NhanSu_barButtonItem_In_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadHamDungChung.PreviewPrintableComponent(CV_TT_NhanSu_GridControl, CV_TT_NhanSu_GridControl.LookAndFeel);
        }

        private void CV_TT_NhanSu_BandedGridview_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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
