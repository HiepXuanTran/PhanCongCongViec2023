using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PCCV.Public;
namespace PCCV.Data
{
    public class CV_HT_UuTienCongViecData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadCV_HT_UuTienCongViec_LoadAll()
        {
            return cls.LayDuLieu("SP_CV_HT_UuTienCongViec_Select");
        }

        public int CV_HT_UuTienCongViec_Del(CV_HT_UuTienCongViecPublic Public)
        {
            int thamso = 4;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_UuTienCongViec_ID";
            bien[1] = "@CV_HT_UuTienCongViec_DateEditor";
            bien[2] = "@HT_USER_Editor";
            bien[3] = "@CV_HT_UuTienCongViec_SuDung";
            giatri[0] = Public.CV_HT_UuTienCongViec_ID;
            giatri[1] = Public.CV_HT_UuTienCongViec_DateEditor;
            giatri[2] = Public.HT_USER_Editor;
            giatri[3] = Public.CV_HT_UuTienCongViec_SuDung;
            return cls.Update("SP_CV_HT_UuTienCongViec_Del", bien, giatri, thamso);
        }
        public int CV_HT_UuTienCongViec_Add(CV_HT_UuTienCongViecPublic Public)
        {
            int thamso = 7;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_UuTienCongViec_LoaiUuTien";
            bien[1] = "@CV_HT_UuTienCongViec_MoTa";
            bien[2] = "@CV_HT_UuTienCongViec_GhiChu";
            bien[3] = "@HT_USER_Create";
            bien[4] = "@CV_HT_UuTienCongViec_DateCreate";
            bien[5] = "@CV_HT_UuTienCongViec_HienThi";
            bien[6] = "@CV_HT_UuTienCongViec_SuDung";

            giatri[0] = Public.CV_HT_UuTienCongViec_LoaiUuTien;
            giatri[1] = Public.CV_HT_UuTienCongViec_MoTa;
            giatri[2] = Public.CV_HT_UuTienCongViec_GhiChu;
            giatri[3] = Public.HT_USER_Create;
            giatri[4] = Public.CV_HT_UuTienCongViec_DateCreate;
            giatri[5] = Public.CV_HT_UuTienCongViec_HienThi;
            giatri[6] = Public.CV_HT_UuTienCongViec_SuDung;

            return cls.Update("SP_CV_HT_UuTienCongViec_Insert", bien, giatri, thamso);
        }
        public int CV_HT_UuTienCongViec_Edit(CV_HT_UuTienCongViecPublic Public)
        {
            int thamso = 10;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];

            bien[0] = "@CV_HT_UuTienCongViec_LoaiUuTien";
            bien[1] = "@CV_HT_UuTienCongViec_MoTa";
            bien[2] = "@CV_HT_UuTienCongViec_GhiChu";
            bien[3] = "@HT_USER_Create";
            bien[4] = "@CV_HT_UuTienCongViec_DateCreate";
            bien[5] = "@HT_USER_Editor";
            bien[6] = "@CV_HT_UuTienCongViec_DateEditor";
            bien[7] = "@CV_HT_UuTienCongViec_HienThi";
            bien[8] = "@CV_HT_UuTienCongViec_SuDung";
            bien[9] = "@CV_HT_UuTienCongViec_ID";

            giatri[0] = Public.CV_HT_UuTienCongViec_LoaiUuTien;
            giatri[1] = Public.CV_HT_UuTienCongViec_MoTa;
            giatri[2] = Public.CV_HT_UuTienCongViec_GhiChu;
            giatri[3] = Public.HT_USER_Create;
            giatri[4] = Public.CV_HT_UuTienCongViec_DateCreate;
            giatri[5] = Public.HT_USER_Editor;
            giatri[6] = Public.CV_HT_UuTienCongViec_DateEditor;
            giatri[7] = Public.CV_HT_UuTienCongViec_HienThi;
            giatri[8] = Public.CV_HT_UuTienCongViec_SuDung;
            giatri[9] = Public.CV_HT_UuTienCongViec_ID;

            return cls.Update("SP_CV_HT_UuTienCongViec_Edit", bien, giatri, thamso);
        }
    }
}
