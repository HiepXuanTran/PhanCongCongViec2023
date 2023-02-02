using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PCCV.Public;
namespace PCCV.Data
{
    public class CV_HT_VaiTroCongViecData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadCV_HT_VaiTroCongViec_LoadAll()
        {
            return cls.LayDuLieu("CV_HT_VaiTroCongViec_Select");
        }

        public int CV_HT_VaiTroCongViec_Del(CV_HT_VaiTroCongViecPublic Public)
        {
            int thamso = 4;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_VaiTroCongViec_ID";
            bien[1] = "@CV_HT_VaiTroCongViec_DateEditor";
            bien[2] = "@HT_USER_Editor";
            bien[3] = "@CV_HT_VaiTroCongViec_SuDung";
            giatri[0] = Public.CV_HT_VaiTroCongViec_ID;
            giatri[1] = Public.CV_HT_VaiTroCongViec_DateEditor;
            giatri[2] = Public.HT_USER_Editor;
            giatri[3] = Public.CV_HT_VaiTroCongViec_SuDung;
            return cls.Update("CV_HT_VaiTroCongViec_Del", bien, giatri, thamso);
        }
        public int CV_HT_VaiTroCongViec_Add(CV_HT_VaiTroCongViecPublic Public)
        {
            int thamso = 7;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_VaiTroCongViec_VaiTroTrongCongViec";
            bien[1] = "@CV_HT_VaiTroCongViec_MoTa";
            bien[2] = "@CV_HT_VaiTroCongViec_GhiChu";
            bien[3] = "@HT_USER_Create";
            bien[4] = "@CV_HT_VaiTroCongViec_DateCreate";
            bien[5] = "@CV_HT_VaiTroCongViec_HienThi";
            bien[6] = "@CV_HT_VaiTroCongViec_SuDung";

            giatri[0] = Public.CV_HT_VaiTroCongViec_VaiTroTrongCongViec;
            giatri[1] = Public.CV_HT_VaiTroCongViec_MoTa;
            giatri[2] = Public.CV_HT_VaiTroCongViec_GhiChu;
            giatri[3] = Public.HT_USER_Create;
            giatri[4] = Public.CV_HT_VaiTroCongViec_DateCreate;
            giatri[5] = Public.CV_HT_VaiTroCongViec_HienThi;
            giatri[6] = Public.CV_HT_VaiTroCongViec_SuDung;

            return cls.Update("CV_HT_VaiTroCongViec_Insert", bien, giatri, thamso);
        }
        public int CV_HT_VaiTroCongViec_Edit(CV_HT_VaiTroCongViecPublic Public)
        {
            int thamso = 10;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];

            bien[0] = "@CV_HT_VaiTroCongViec_VaiTroTrongCongViec";
            bien[1] = "@CV_HT_VaiTroCongViec_MoTa";
            bien[2] = "@CV_HT_VaiTroCongViec_GhiChu";
            bien[3] = "@HT_USER_Create";
            bien[4] = "@CV_HT_VaiTroCongViec_DateCreate";
            bien[5] = "@HT_USER_Editor";
            bien[6] = "@CV_HT_VaiTroCongViec_DateEditor";
            bien[7] = "@CV_HT_VaiTroCongViec_HienThi";
            bien[8] = "@CV_HT_VaiTroCongViec_SuDung";
            bien[9] = "@CV_HT_VaiTroCongViec_ID";

            giatri[0] = Public.CV_HT_VaiTroCongViec_VaiTroTrongCongViec;
            giatri[1] = Public.CV_HT_VaiTroCongViec_MoTa;
            giatri[2] = Public.CV_HT_VaiTroCongViec_GhiChu;
            giatri[3] = Public.HT_USER_Create;
            giatri[4] = Public.CV_HT_VaiTroCongViec_DateCreate;
            giatri[5] = Public.HT_USER_Editor;
            giatri[6] = Public.CV_HT_VaiTroCongViec_DateEditor;
            giatri[7] = Public.CV_HT_VaiTroCongViec_HienThi;
            giatri[8] = Public.CV_HT_VaiTroCongViec_SuDung;
            giatri[9] = Public.CV_HT_VaiTroCongViec_ID;

            return cls.Update("CV_HT_VaiTroCongViec_Edit", bien, giatri, thamso);
        }
    }
}
