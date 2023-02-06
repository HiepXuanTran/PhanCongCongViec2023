using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PCCV.Public;
namespace PCCV.Data
{
    public class CV_HT_LoaiCongViecData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadCV_HT_LoaiCongViec_LoadAll()
        {
            return cls.LayDuLieu("SP_CV_HT_LoaiCongViec_Select");
        }
        public int CV_HT_LoaiCongViec_Del(CV_HT_LoaiCongViecPublic Public)
        {
            int thamso = 4;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_LoaiCongViec_Id";
            bien[1] = "@CV_HT_LoaiCongViec_DateEdit";
            bien[2] = "@HT_USER_Editor";
            bien[3] = "@CV_QL_LoaiCongViec_SuDung";
            giatri[0] = Public.CV_HT_LoaiCongViec_Id;
            giatri[1] = Public.CV_HT_LoaiCongViec_DateEdit;
            giatri[2] = Public.HT_USER_Edit;
            giatri[3] = Public.CV_HT_LoaiCongViec_SuDung;
            return cls.Update("SP_CV_HT_LoaiCongViec_Del", bien, giatri, thamso);
        }

        public int CV_HT_LoaiCongViec_Add(CV_HT_LoaiCongViecPublic Public)
        {
            int thamso = 7;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_LoaiCongViec_TenLoaiCongViec";
            bien[1] = "@CV_HT_LoaiCongViec_Mota";
            bien[2] = "@CV_HT_LoaiCongViec_GhiChu";
            bien[3] = "@HT_USER_Create";
            bien[4] = "@CV_HT_LoaiCongViec_DateCreate";
            bien[5] = "@CV_HT_LoaiCongViec_HienThi";
            bien[6] = "@CV_HT_LoaiCongViec_SuDung";
            giatri[0] = Public.CV_HT_LoaiCongViec_TenLoaiCongViec;
            giatri[1] = Public.CV_HT_LoaiCongViec_Mota;
            giatri[2] = Public.CV_HT_LoaiCongViec_GhiChu;
            giatri[3] = Public.HT_USER_Create;
            giatri[4] = Public.CV_HT_LoaiCongViec_DateCreate;
            giatri[5] = Public.CV_HT_LoaiCongViec_HienThi;
            giatri[6] = Public.CV_HT_LoaiCongViec_SuDung;
            return cls.Update("SP_CV_HT_LoaiCongViec_Insert", bien, giatri, thamso);
        }
        public int CV_HT_LoaiCongViec_Update(CV_HT_LoaiCongViecPublic Public)
        {
            int thamso = 10;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_LoaiCongViec_TenLoaiCongViec";
            bien[1] = "@CV_HT_LoaiCongViec_Mota";
            bien[2] = "@CV_HT_LoaiCongViec_GhiChu";
            bien[3] = "@HT_USER_Create";
            bien[4] = "@CV_HT_LoaiCongViec_DateCreate";
            bien[5] = "@CV_HT_LoaiCongViec_HienThi";
            bien[6] = "@CV_HT_LoaiCongViec_SuDung";
            bien[7] = "@CV_HT_LoaiCongViec_DateEdit";
            bien[8] = "@HT_USER_Edit";
            bien[9] = "@CV_HT_LoaiCongViec_Id";
            giatri[0] = Public.CV_HT_LoaiCongViec_TenLoaiCongViec;
            giatri[1] = Public.CV_HT_LoaiCongViec_Mota;
            giatri[2] = Public.CV_HT_LoaiCongViec_GhiChu;
            giatri[3] = Public.HT_USER_Create;
            giatri[4] = Public.CV_HT_LoaiCongViec_DateCreate;
            giatri[5] = Public.CV_HT_LoaiCongViec_HienThi;
            giatri[6] = Public.CV_HT_LoaiCongViec_SuDung;
            giatri[7] = Public.CV_HT_LoaiCongViec_DateEdit;
            giatri[8] = Public.HT_USER_Edit;
            giatri[9] = Public.CV_HT_LoaiCongViec_Id;
            return cls.Update("SP_CV_HT_LoaiCongViec_Update", bien, giatri, thamso);
        }

    }
}
