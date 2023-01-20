using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PCCV.Public;

namespace PCCV.Data
{
    public class CV_TT_NhanSuData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadCV_TT_NhanSu_LoadAll()
        {
            return cls.LayDuLieu("CV_TT_NhanSu_Select");
        }
        public int CV_TT_NhanSu_Del(CV_TT_NhanSuPublic Public)
        {
            int thamso = 1;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_TT_NhanSu_ID";
            giatri[0] = Public.CV_TT_NhanSu_ID;
            return cls.Update("CV_TT_NhanSu_Del", bien, giatri, thamso);
        }
        public int CV_TT_NhanSu_Add(CV_TT_NhanSuPublic Public)
        {
            int thamso = 12;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_TT_NhanSu_MaNhanSu";
            bien[1] = "@CV_TT_NhanSu_HoTen";
            bien[2] = "@CV_TT_NhanSu_DonVi";
            bien[3] = "@CV_TT_NhanSu_NhomThucHien";
            bien[4] = "@CV_TT_NhanSu_TrinhDo";
            bien[5] = "@CV_TT_NhanSu_KhaNangChuyenMon";
            bien[6] = "@HT_USER_Create";
            bien[7] = "@CV_TT_NhanSu_DateCreate";
            bien[8] = "@HT_USER_Editor";
            bien[9] = "@CV_TT_NhanSu_DateEditor";
            bien[10] = "@CV_TT_NhanSu_HienThi";
            bien[11] = "@CV_TT_NhanSu_SuDung";

            giatri[0] = Public.CV_TT_NhanSu_MaNhanSu;
            giatri[1] = Public.CV_TT_NhanSu_HoTen;
            giatri[2] = Public.CV_TT_NhanSu_DonVi;
            giatri[3] = Public.CV_TT_NhanSu_NhomThucHien;
            giatri[4] = Public.CV_TT_NhanSu_TrinhDo;
            giatri[5] = Public.CV_TT_NhanSu_KhaNangChuyenMon;
            giatri[6] = Public.HT_USER_Create;
            giatri[7] = Public.CV_TT_NhanSu_DateCreate;
            giatri[8] = Public.HT_USER_Editor;
            giatri[9] = Public.CV_TT_NhanSu_DateEditor;
            giatri[10] = Public.CV_TT_NhanSu_HienThi;
            giatri[11] = Public.CV_TT_NhanSu_SuDung;

            return cls.Update("CV_TT_NhanSu_Insert", bien, giatri, thamso);
        }
        public int CV_TT_NhanSu_Edit(CV_TT_NhanSuPublic Public)
        {
            int thamso = 13;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_TT_NhanSu_MaNhanSu";
            bien[1] = "@CV_TT_NhanSu_HoTen";
            bien[2] = "@CV_TT_NhanSu_DonVi";
            bien[3] = "@CV_TT_NhanSu_NhomThucHien";
            bien[4] = "@CV_TT_NhanSu_TrinhDo";
            bien[5] = "@CV_TT_NhanSu_KhaNangChuyenMon";
            bien[6] = "@HT_USER_Create";
            bien[7] = "@CV_TT_NhanSu_DateCreate";
            bien[8] = "@HT_USER_Editor";
            bien[9] = "@CV_TT_NhanSu_DateEditor";
            bien[10] = "@CV_TT_NhanSu_HienThi";
            bien[11] = "@CV_TT_NhanSu_SuDung";
            bien[12] = "@CV_TT_NhanSu_ID";

            giatri[0] = Public.CV_TT_NhanSu_MaNhanSu;
            giatri[1] = Public.CV_TT_NhanSu_HoTen;
            giatri[2] = Public.CV_TT_NhanSu_DonVi;
            giatri[3] = Public.CV_TT_NhanSu_NhomThucHien;
            giatri[4] = Public.CV_TT_NhanSu_TrinhDo;
            giatri[5] = Public.CV_TT_NhanSu_KhaNangChuyenMon;
            giatri[6] = Public.HT_USER_Create;
            giatri[7] = Public.CV_TT_NhanSu_DateCreate;
            giatri[8] = Public.HT_USER_Editor;
            giatri[9] = Public.CV_TT_NhanSu_DateEditor;
            giatri[10] = Public.CV_TT_NhanSu_HienThi;
            giatri[11] = Public.CV_TT_NhanSu_SuDung;
            giatri[11] = Public.CV_TT_NhanSu_ID;

            return cls.Update("CV_TT_NhanSu_Edit", bien, giatri, thamso);
        }
    }
}
