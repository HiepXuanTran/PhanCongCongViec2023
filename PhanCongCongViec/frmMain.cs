using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace PhanCongCongViec
{
    public partial class frmMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadMain.HienThiCV_QL_CongViec();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadMain.HienThiCV_QL_NhomCongViec();
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadMain.HienThiCV_HT_LoaiCongViec();
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            LoadMain.HienThiCV_HT_MucDoKho();
        }
    }
}