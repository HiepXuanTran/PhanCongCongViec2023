using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCCV.Public;
using System.Data;
using System.Data.SqlClient;
namespace PCCV.Data
{
    public class CV_PC_PhanCongCongViecNhanSuData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadCV_PC_PhanCongCongViecNhanSu()
        {
            return cls.LayDuLieu("SP_CV_PC_PhanCongCongViecNhanSu_Select");
        }
        public int CV_PC_PhanCongCongViecNhanSu_Del(CV_PC_PhanCongCongViecNhanSuPublic Public)
        {
            int thamso = 4;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_PC_PhanCongCongViecNhanSu_ID";
            bien[1] = "@CV_PC_PhanCongCongViecNhanSu_DateEditor";
            bien[2] = "@HT_USER_Editor";
            bien[3] = "@CV_PC_PhanCongCongViecNhanSu_SuDung";
            giatri[0] = Public.CV_PC_PhanCongCongViecNhanSu_ID;
            giatri[1] = Public.CV_PC_PhanCongCongViecNhanSu_DateEditor;
            giatri[2] = Public.HT_USER_Editor;
            giatri[3] = Public.CV_PC_PhanCongCongViecNhanSu_SuDung;
            return cls.Update("SP_CV_PC_PhanCongCongViecNhanSu_Del", bien, giatri, thamso);
        }
        public int CV_PC_PhanCongCongViecNhanSu_Insert(CV_PC_PhanCongCongViecNhanSuPublic Public)
        {
            int thamso = 12;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_PC_PhanCongCongViecNhanSu_IDCongViec";
            bien[1] = "@CV_PC_PhanCongCongViecNhanSu_IDVaiTro";
            bien[2] = "@CV_PC_PhanCongCongViecNhanSu_NgayBatDau";
            bien[3] = "@CV_PC_PhanCongCongViecNhanSu_NgayKetThuc";
            bien[4] = "@CV_PC_PhanCongCongViecNhanSu_DanhGia";
            bien[5] = "@CV_PC_PhanCongCongViecNhanSu_LyDo";
            bien[6] = "@HT_USER_Create";
            bien[7] = "@CV_PC_PhanCongCongViecNhanSu_DateCreate";
            bien[8] = "@CV_PC_PhanCongCongViecNhanSu_HienThi";
            bien[9] = "@CV_PC_PhanCongCongViecNhanSu_SuDung";
            bien[10] = "@CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien";
            bien[11] = "@CV_PC_PhanCongCongViecNhanSu_IDUser";

            giatri[0] = Public.CV_PC_PhanCongCongViecNhanSu_IDCongViec;
            giatri[1] = Public.CV_PC_PhanCongCongViecNhanSu_IDVaiTro;
            giatri[2] = Public.CV_PC_PhanCongCongViecNhanSu_NgayBatDau;
            giatri[3] = Public.CV_PC_PhanCongCongViecNhanSu_NgayKetThuc;
            giatri[4] = Public.CV_PC_PhanCongCongViecNhanSu_DanhGia;
            giatri[5] = Public.CV_PC_PhanCongCongViecNhanSu_LyDo;
            giatri[6] = Public.HT_USER_Create;
            giatri[7] = Public.CV_PC_PhanCongCongViecNhanSu_DateCreate;
            giatri[8] = Public.CV_PC_PhanCongCongViecNhanSu_HienThi;
            giatri[9] = Public.CV_PC_PhanCongCongViecNhanSu_SuDung;
            giatri[10] = Public.CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien;
            giatri[11] = Public.CV_PC_PhanCongCongViecNhanSu_IDUser;
            return cls.Update("SP_CV_PC_PhanCongCongViecNhanSu_Insert", bien, giatri, thamso);
        }
        public int CV_PC_PhanCongCongViecNhanSu_Update(CV_PC_PhanCongCongViecNhanSuPublic Public)
        {
            int thamso = 15;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_PC_PhanCongCongViecNhanSu_IDCongViec";
            bien[1] = "@CV_PC_PhanCongCongViecNhanSu_IDVaiTro";
            bien[2] = "@CV_PC_PhanCongCongViecNhanSu_NgayBatDau";
            bien[3] = "@CV_PC_PhanCongCongViecNhanSu_NgayKetThuc";
            bien[4] = "@CV_PC_PhanCongCongViecNhanSu_DanhGia";
            bien[5] = "@CV_PC_PhanCongCongViecNhanSu_LyDo";
            bien[6] = "@HT_USER_Create";
            bien[7] = "@CV_PC_PhanCongCongViecNhanSu_DateCreate";
            bien[8] = "@CV_PC_PhanCongCongViecNhanSu_HienThi";
            bien[9] = "@CV_PC_PhanCongCongViecNhanSu_SuDung";
            bien[10] = "@CV_PC_PhanCongCongViecNhanSu_Id";
            bien[11] = "@HT_USER_Editor";
            bien[12] = "@CV_PC_PhanCongCongViecNhanSu_DateEditor";
            bien[13] = "@CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien";
            bien[14] = "@CV_PC_PhanCongCongViecNhanSu_IDUser";
            giatri[0] = Public.CV_PC_PhanCongCongViecNhanSu_IDCongViec;
            giatri[1] = Public.CV_PC_PhanCongCongViecNhanSu_IDVaiTro;
            giatri[2] = Public.CV_PC_PhanCongCongViecNhanSu_NgayBatDau;
            giatri[3] = Public.CV_PC_PhanCongCongViecNhanSu_NgayKetThuc;
            giatri[4] = Public.CV_PC_PhanCongCongViecNhanSu_DanhGia;
            giatri[5] = Public.CV_PC_PhanCongCongViecNhanSu_LyDo;
            giatri[6] = Public.HT_USER_Create;
            giatri[7] = Public.CV_PC_PhanCongCongViecNhanSu_DateCreate;
            giatri[8] = Public.CV_PC_PhanCongCongViecNhanSu_HienThi;
            giatri[9] = Public.CV_PC_PhanCongCongViecNhanSu_SuDung;
            giatri[10] = Public.CV_PC_PhanCongCongViecNhanSu_ID;
            giatri[11] = Public.HT_USER_Editor;
            giatri[12] = Public.CV_PC_PhanCongCongViecNhanSu_DateEditor;
            giatri[13] = Public.CV_PC_PhanCongCongViecNhanSu_DiaDiemThucHien;
            giatri[14] = Public.CV_PC_PhanCongCongViecNhanSu_IDUser;
            return cls.Update("SP_CV_PC_PhanCongCongViecNhanSu_Update", bien, giatri, thamso);
        }
    }
}
