using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Uneti.Public;

namespace Uneti.Data
{
    public class CV_QL_NhanSuData
    {

        ClsKetNoi cls = new ClsKetNoi();
        
        public DataTable LoadCV_QL_NhanSu_LoadUser()
        {
            return cls.LayDuLieu("SP_CV_QL_NhanSu_Select");
        }

        public DataTable CV_QL_NhanSu_ReturnID(CV_QL_NhanSuPublic Public)
        {
            int thamso = 1;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@HT_USER_Ten";
            giatri[0] = Public.CV_QL_NhanSu_HoTen;
            return cls.LayDuLieu("SP_CV_QL_NhanSu_ReturnID", bien, giatri, thamso);
        }

        public int CV_QL_NhanSu_Del(CV_QL_NhanSuPublic Public)
        {
            int thamso = 4;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_NhanSu_ID";
            bien[1] = "@CV_QL_NhanSu_DateEditor";
            bien[2] = "@HT_USER_Editor";
            bien[3] = "@CV_QL_NhanSu_SuDung";
            giatri[0] = Public.CV_QL_NhanSu_ID;
            giatri[1] = Public.CV_QL_NhanSu_DateEditor;
            giatri[2] = Public.HT_USER_Editor;
            giatri[3] = Public.CV_QL_NhanSu_SuDung;
            return cls.Update("SP_CV_QL_NhanSu_Del", bien, giatri, thamso);
        }
        public int CV_QL_NhanSu_Add(CV_QL_NhanSuPublic Public)
        {
            int thamso = 10;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_NhanSu_MaNhanSu";
            bien[1] = "@CV_QL_NhanSu_HoTen";
            bien[2] = "@CV_QL_NhanSu_DonVi";
            bien[3] = "@CV_QL_NhanSu_NhomThucHien";
            bien[4] = "@CV_QL_NhanSu_TrinhDo";
            bien[5] = "@CV_QL_NhanSu_KhaNangChuyenMon";
            bien[6] = "@HT_USER_Create";
            bien[7] = "@CV_QL_NhanSu_DateCreate";
            bien[8] = "@CV_QL_NhanSu_HienThi";
            bien[9] = "@CV_QL_NhanSu_SuDung";

            giatri[0] = Public.CV_QL_NhanSu_MaNhanSu;
            giatri[1] = Public.CV_QL_NhanSu_HoTen;
            giatri[2] = Public.CV_QL_NhanSu_DonVi;
            giatri[3] = Public.CV_QL_NhanSu_NhomThucHien;
            giatri[4] = Public.CV_QL_NhanSu_TrinhDo;
            giatri[5] = Public.CV_QL_NhanSu_KhaNangChuyenMon;
            giatri[6] = Public.HT_USER_Create;
            giatri[7] = Public.CV_QL_NhanSu_DateCreate;
            giatri[8] = Public.CV_QL_NhanSu_HienThi;
            giatri[9] = Public.CV_QL_NhanSu_SuDung;

            return cls.Update("SP_CV_QL_NhanSu_Insert", bien, giatri, thamso);
        }
        public int CV_QL_NhanSu_Edit(CV_QL_NhanSuPublic Public)
        {
            int thamso = 13;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];

            bien[0] = "@CV_QL_NhanSu_MaNhanSu";
            bien[1] = "@CV_QL_NhanSu_HoTen";
            bien[2] = "@CV_QL_NhanSu_DonVi";
            bien[3] = "@CV_QL_NhanSu_NhomThucHien";
            bien[4] = "@CV_QL_NhanSu_TrinhDo";
            bien[5] = "@CV_QL_NhanSu_KhaNangChuyenMon";
            bien[6] = "@HT_USER_Create";
            bien[7] = "@CV_QL_NhanSu_DateCreate";
            bien[8] = "@HT_USER_Editor";
            bien[9] = "@CV_QL_NhanSu_DateEditor";
            bien[10] = "@CV_QL_NhanSu_HienThi";
            bien[11] = "@CV_QL_NhanSu_SuDung";
            bien[12] = "@CV_QL_NhanSu_ID";

            giatri[0] = Public.CV_QL_NhanSu_MaNhanSu;
            giatri[1] = Public.CV_QL_NhanSu_HoTen;
            giatri[2] = Public.CV_QL_NhanSu_DonVi;
            giatri[3] = Public.CV_QL_NhanSu_NhomThucHien;
            giatri[4] = Public.CV_QL_NhanSu_TrinhDo;
            giatri[5] = Public.CV_QL_NhanSu_KhaNangChuyenMon;
            giatri[6] = Public.HT_USER_Create;
            giatri[7] = Public.CV_QL_NhanSu_DateCreate;
            giatri[8] = Public.HT_USER_Editor;
            giatri[9] = Public.CV_QL_NhanSu_DateEditor;
            giatri[10] = Public.CV_QL_NhanSu_HienThi;
            giatri[11] = Public.CV_QL_NhanSu_SuDung;
            giatri[12] = Public.CV_QL_NhanSu_ID;

            return cls.Update("SP_CV_QL_NhanSu_Edit", bien, giatri, thamso);
        }
    }
}
