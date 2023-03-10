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
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;

namespace Uneti.Online.form.QuanLy
{
    public partial class frmCV_QL_NhanSu : Form
    {
        public frmCV_QL_NhanSu()
        {
            InitializeComponent();
            new MultiSelectionEditingHelper(CV_QL_NhanSu_BandedGridview);
        }
        CV_QL_NhanSuBLL cls = new CV_QL_NhanSuBLL();
        bool CV_QL_NhanSuEdit = false;
        bool CV_QL_NhanSuAdd = false;
        CV_HT_KhaNangChuyenMonBLL clsKhaNangChuyenMon = new CV_HT_KhaNangChuyenMonBLL();
        CV_HT_NhomThucHienBLL clsNhomThucHien = new CV_HT_NhomThucHienBLL();

        #region Cho phép thực hiện thao tác CLICK phải chuột

        void Check_All_Click(object sender, EventArgs e)
        {
            CV_QL_NhanSu_BandedGridview.ClearSelection();
            CV_QL_NhanSu_BandedGridview.FocusedColumn = CV_QL_NhanSu_BandedGridview.Columns["CV_QL_NhanSuChon"];

            CV_QL_NhanSu_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_NhanSu_BandedGridview.RowCount; i++)
            {
                CV_QL_NhanSu_BandedGridview.SetFocusedRowCellValue(CV_QL_NhanSuChon, true);
                CV_QL_NhanSu_BandedGridview.MoveNext();
            }
            CV_QL_NhanSu_BandedGridview.MoveFirst();
        }
        void No_Check_All_Click(object sender, EventArgs e)
        {
            CV_QL_NhanSu_BandedGridview.ClearSelection();
            CV_QL_NhanSu_BandedGridview.FocusedColumn = CV_QL_NhanSu_BandedGridview.Columns["CV_QL_NhanSuChon"];

            CV_QL_NhanSu_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_NhanSu_BandedGridview.RowCount; i++)
            {
                CV_QL_NhanSu_BandedGridview.SetFocusedRowCellValue(CV_QL_NhanSuChon, false);
                CV_QL_NhanSu_BandedGridview.MoveNext();
            }
            CV_QL_NhanSu_BandedGridview.MoveFirst();
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
                this.CV_QL_NhanSu_MaNhanSu.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_NhanSu_HoTen.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_NhanSu_DonVi.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_NhanSu_NhomThucHien.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_NhanSu_KhaNangChuyenMon.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_NhanSu_TrinhDo.OptionsColumn.ReadOnly = !Lock_Control;
                this.CV_QL_NhanSu_KhaNangChuyenMon.OptionsColumn.ReadOnly = !Lock_Control;
            }
        }
        // mở khoá nút lưu vs undo
        private void Lock_Unlock_Control(Boolean Lock_Control) //Khóa và mở khóa điều khiển chức năng
        {
            CV_QL_NhanSu_barButtonItem_Refresh.Enabled = Lock_Control;
            CV_QL_NhanSu_barButtonItem_Add.Enabled = Lock_Control;
            CV_QL_NhanSu_barButtonItem_Sua.Enabled = Lock_Control;
            CV_QL_NhanSu_barButtonItem_Xoa.Enabled = Lock_Control;
            CV_QL_NhanSu_barButtonItem_Luu.Enabled = !Lock_Control;
            CV_QL_NhanSu_barButtonItem_Undo.Enabled = !Lock_Control;
        }

        // check nhap du lieu chua xong
        private bool KiemTra_NhapDuLieu()
        {
            CV_QL_NhanSu_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_NhanSu_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSuChon)) &&
                        (
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_MaNhanSu))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_HoTen))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_DonVi))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_NhomThucHien))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_TrinhDo))) ||
                            string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_KhaNangChuyenMon)))
                        )
                    )
                {
                    return false;
                }
                CV_QL_NhanSu_BandedGridview.MoveNext();
            }
            return true;
        }

        //trả về dòng đang chọn đầu tiên
        private void TraVe_DongDLChon()
        {
            CV_QL_NhanSu_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_NhanSu_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSuChon)))
                {
                    //Trả con trỏ về vị trí được chọn
                    break;
                }
                CV_QL_NhanSu_BandedGridview.MoveNext();
            }
        }
        // hàm kiểm tra check ô để sửa
        private bool KiemTra()
        {
            CV_QL_NhanSu_BandedGridview.MoveFirst();
            for (int i = 0; i < CV_QL_NhanSu_BandedGridview.RowCount; i++)
            {
                if (Convert.ToBoolean(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSuChon))) // == true
                {
                    return true;
                }
                CV_QL_NhanSu_BandedGridview.MoveNext();
            }
            return false;
        }

        private void frmCV_QL_NhanSu_Load(object sender, EventArgs e)
        {

            //lookup edit kha nang chuyen mon
            CV_QL_NhanSu_LookupEdit_KhaNangChuyenMon.DataSource = clsKhaNangChuyenMon.LoadCV_HT_KhaNangChuyenMon_LoadAll();
            CV_QL_NhanSu_LookupEdit_KhaNangChuyenMon.DisplayMember = "CV_HT_KhaNangChuyenMon_TenKhaNang";
            CV_QL_NhanSu_LookupEdit_KhaNangChuyenMon.ValueMember = "CV_HT_KhaNangChuyenMon_Id";
            CV_QL_NhanSu_LookupEdit_KhaNangChuyenMon.PopupWidth = 900;
            CV_QL_NhanSu_LookupEdit_KhaNangChuyenMon.ShowFooter = false;
            CV_QL_NhanSu_LookupEdit_KhaNangChuyenMon.Columns.Clear();
            CV_QL_NhanSu_LookupEdit_KhaNangChuyenMon.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_KhaNangChuyenMon_TenKhaNang", "Khả năng chuyên môn", 300));
            CV_QL_NhanSu_LookupEdit_KhaNangChuyenMon.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_KhaNangChuyenMon_MoTa", "Mô tả", 300));
            CV_QL_NhanSu_LookupEdit_KhaNangChuyenMon.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_KhaNangChuyenMon_GhiChu", "Ghi chú", 300));


            CV_QL_NhanSu_LookupEdit_NhomThucHien.DataSource = clsNhomThucHien.LoadCV_HT_NhomThucHien_LoadAll();
            CV_QL_NhanSu_LookupEdit_NhomThucHien.DisplayMember = "CV_HT_NhomThucHien_TenNhomThucHien";
            CV_QL_NhanSu_LookupEdit_NhomThucHien.ValueMember = "CV_HT_NhomThucHien_ID";
            CV_QL_NhanSu_LookupEdit_NhomThucHien.PopupWidth = 900;
            CV_QL_NhanSu_LookupEdit_NhomThucHien.ShowFooter = false;
            CV_QL_NhanSu_LookupEdit_NhomThucHien.Columns.Clear();
            CV_QL_NhanSu_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_NhomThucHien_TenNhomThucHien", "Nhóm thực hiện", 450));
            CV_QL_NhanSu_LookupEdit_NhomThucHien.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("CV_HT_NhomThucHien_GhiChu", "Ghi chú", 450));

            CV_QL_NhanSu_GridControl.DataSource = cls.LoadCV_QL_NhanSu_LoadUser();
            CV_QL_NhanSu_barButtonItem_Luu.Enabled = false;
            CV_QL_NhanSu_barButtonItem_Undo.Enabled = false;
            Lock_Unlock_Control_Input(false); // lock input
            CV_QL_NhanSu_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void CV_QL_NhanSu_barButtonItem_Xoa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    CV_QL_NhanSu_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_QL_NhanSu_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSuChon))) // == true
                        {
                            string s = Convert.ToString(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_ID));
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_ID))))
                            {
                                CV_QL_NhanSuPublic Public = new CV_QL_NhanSuPublic();
                                Public.CV_QL_NhanSu_ID = Convert.ToInt32(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_ID));
                                Public.CV_QL_NhanSu_DateEditor = DateTime.Now;
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_NhanSu_SuDung = BienToanCuc.HT_USER_Ten;
                                kq = cls.CV_QL_NhanSu_Del(Public);
                            }
                            else
                            {
                                MessageBox.Show("Xin mời chọn lại dữ liệu trên lưới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        CV_QL_NhanSu_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon();
                if (kq > 0)
                {
                    MessageBox.Show("Xoá Thành Công!");
                    frmCV_QL_NhanSu_Load(sender, e); // load lai form
                }
                else
                {
                    MessageBox.Show("Xoá Thất bại!");
                }
            }
        }

        private void CV_QL_NhanSu_barButtonItem_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCV_QL_NhanSu_Load(sender, e);
        }

        private void CV_QL_NhanSu_barButtonItem_Add_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_NhanSuAdd = true;
            CV_QL_NhanSuEdit = false;
            CV_QL_NhanSu_BandedGridview.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            Lock_Unlock_Control_Input(true);
            Lock_Unlock_Control(false);
        }

        private void CV_QL_NhanSu_barButtonItem_Sua_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (KiemTra() == false)
            {
                MessageBox.Show("Bạn phải chọn dữ liệu", "Thông báo!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // check mã công việc xem có chọn đúng dòng có dữ liệu chưa 
            if (string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_ID))))
            {
                MessageBox.Show("Bạn phải lựa chọn lại dữ liệu trên lưới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.None);
                return;
            }
            else
            {
                CV_QL_NhanSuAdd = false;
                CV_QL_NhanSuEdit = true;
                Lock_Unlock_Control_Input(true); //lock input
                Lock_Unlock_Control(false); // lock nut nhap hien thi nut luu
            }
        }

        private void CV_QL_NhanSu_barButtonItem_Luu_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int kq = -1;
            try
            {
                if (CV_QL_NhanSuAdd == true || CV_QL_NhanSuEdit == true)
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
                    CV_QL_NhanSu_BandedGridview.MoveFirst();
                    for (int i = 0; i < CV_QL_NhanSu_BandedGridview.RowCount; i++)
                    {
                        if (Convert.ToBoolean(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSuChon))) // == true
                        {
                            // gán vào đối tượng puhlic
                            CV_QL_NhanSuPublic Public = new CV_QL_NhanSuPublic();
                            Public.CV_QL_NhanSu_HienThi = true;
                            Public.CV_QL_NhanSu_SuDung = BienToanCuc.HT_USER_Ten;
                            Public.CV_QL_NhanSu_MaNhanSu = CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_MaNhanSu).ToString();
                            Public.CV_QL_NhanSu_HoTen = CV_QL_NhanSu_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_NhanSu_HoTen);
                            Public.CV_QL_NhanSu_DonVi = CV_QL_NhanSu_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_NhanSu_DonVi);

                            if (!string.IsNullOrWhiteSpace(Convert.ToString(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_NhomThucHien))))
                            {
                                Public.CV_QL_NhanSu_NhomThucHien = Convert.ToInt32(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_NhomThucHien));
                            }
                            Public.CV_QL_NhanSu_TrinhDo = CV_QL_NhanSu_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_NhanSu_TrinhDo);
                            Public.CV_QL_NhanSu_KhaNangChuyenMon = CV_QL_NhanSu_BandedGridview.GetFocusedRowCellDisplayText(CV_QL_NhanSu_KhaNangChuyenMon);
                            if (CV_QL_NhanSuAdd == true)
                            {
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_NhanSu_DateCreate = DateTime.Now;
                                Public.HT_USER_Create = BienToanCuc.HT_USER_ID;
                                kq = cls.CV_QL_NhanSu_Add(Public);
                            }

                            if (CV_QL_NhanSuEdit == true)
                            {
                                Public.HT_USER_Create = Convert.ToInt32(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(HT_USER_Create));
                                Public.HT_USER_Editor = BienToanCuc.HT_USER_ID;
                                Public.CV_QL_NhanSu_DateCreate = Convert.ToDateTime(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_DateCreate));
                                Public.CV_QL_NhanSu_DateEditor = DateTime.Now;
                                Public.CV_QL_NhanSu_ID = Convert.ToInt32(CV_QL_NhanSu_BandedGridview.GetFocusedRowCellValue(CV_QL_NhanSu_ID));
                                kq = cls.CV_QL_NhanSu_Edit(Public);
                            }
                        }
                        CV_QL_NhanSu_BandedGridview.MoveNext();
                    }
                }
                TraVe_DongDLChon(); //Trả về dòng chọn đầu tiên
                if (kq > 0)
                {
                    if (CV_QL_NhanSuAdd == true)
                    {
                        MessageBox.Show("Thêm Thành Công!");
                    }
                    else if (CV_QL_NhanSuEdit == true)
                    {
                        MessageBox.Show("Sửa Thành Công!");
                    }
                }
                frmCV_QL_NhanSu_Load(sender, e);
                Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
                Lock_Unlock_Control(true); //Mở khóa toàn bộ
                CV_QL_NhanSuAdd = false;
                CV_QL_NhanSuEdit = false;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CV_QL_NhanSu_barButtonItem_Undo_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CV_QL_NhanSuAdd = false;
            CV_QL_NhanSuEdit = false;
            Lock_Unlock_Control(true); // lock nut luu vs undo
            Lock_Unlock_Control_Input(false); //Khóa điều khiển nhập dữ liệu
        }

        private void CV_QL_NhanSu_barButtonItem_In_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadHamDungChung.PreviewPrintableComponent(CV_QL_NhanSu_GridControl, CV_QL_NhanSu_GridControl.LookAndFeel);
        }

        private void CV_QL_NhanSu_BandedGridview_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
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
