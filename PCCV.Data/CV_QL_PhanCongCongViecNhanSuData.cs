using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCCV.Public;
using System.Data;
using System.Data.SqlClient;
namespace PCCV.Data
{
    public class CV_QL_PhanCongCongViecNhanSuData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadCV_QL_PhanCongCongViecNhanSu()
        {
            return cls.LayDuLieu("CV_QL_PhanCongCongViecNhanSu_Select");
        }
        public int CV_QL_PhanCongCongViecNhanSu_Del(CV_QL_PhanCongCongViecNhanSuPublic Public)
        {
            int thamso = 4;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_PhanCongCongViecNhanSu_ID";
            bien[1] = "@CV_QL_PhanCongCongViecNhanSu_DateEditor";
            bien[2] = "@HT_USER_Editor";
            bien[3] = "@CV_QL_PhanCongCongViecNhanSu_SuDung";
            giatri[0] = Public.CV_QL_PhanCongCongViecNhanSu_ID;
            giatri[1] = Public.CV_QL_PhanCongCongViecNhanSu_DateEditor;
            giatri[2] = Public.HT_USER_Editor;
            giatri[3] = Public.CV_QL_PhanCongCongViecNhanSu_SuDung;
            return cls.Update("CV_QL_PhanCongCongViecNhanSu_Del", bien, giatri, thamso);
        }
        public int CV_QL_PhanCongCongViecNhanSu_Insert(CV_QL_PhanCongCongViecNhanSuPublic Public)
        {
            int thamso = 18;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_PhanCongCongViecNhanSu_TenLoaiCongViec";
            bien[1] = "@CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec1";
            bien[2] = "@CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec2";
            bien[3] = "@CV_QL_PhanCongCongViecNhanSu_TenCongViec";
            bien[4] = "@CV_QL_PhanCongCongViecNhanSu_MucDoKho";
            bien[5] = "@CV_QL_PhanCongCongViecNhanSu_NhanSuPhuTrach";
            bien[6] = "@CV_QL_PhanCongCongViecNhanSu_NhanSuThucHien";
            bien[7] = "@CV_QL_PhanCongCongViecNhanSu_NhanSuPhoiHop";
            bien[8] = "@CV_QL_PhanCongCongViecNhanSu_NhanSuKiemTra";
            bien[9] = "@CV_QL_PhanCongCongViecNhanSu_NgayBatDau";
            bien[10] = "@CV_QL_PhanCongCongViecNhanSu_NgayKetThuc";
            bien[11] = "@CV_QL_PhanCongCongViecNhanSu_DanhGia";
            bien[12] = "@CV_QL_PhanCongCongViecNhanSu_LyDo";
            bien[13] = "@HT_USER_Create";
            bien[14] = "@CV_QL_PhanCongCongViecNhanSu_DateCreate";
            bien[15] = "@CV_QL_PhanCongCongViecNhanSu_HienThi";
            bien[16] = "@CV_QL_PhanCongCongViecNhanSu_SuDung";
            bien[17] = "@CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien";

            giatri[17] = Public.CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien;
            giatri[0] = Public.CV_QL_PhanCongCongViecNhanSu_TenLoaiCongViec;
            giatri[1] = Public.CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec1;
            giatri[2] = Public.CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec2;
            giatri[3] = Public.CV_QL_PhanCongCongViecNhanSu_TenCongViec;
            giatri[4] = Public.CV_QL_PhanCongCongViecNhanSu_MucDoKho;
            giatri[5] = Public.CV_QL_PhanCongCongViecNhanSu_NhanSuPhuTrach;
            giatri[6] = Public.CV_QL_PhanCongCongViecNhanSu_NhanSuThucHien;
            giatri[7] = Public.CV_QL_PhanCongCongViecNhanSu_NhanSuPhoiHop;
            giatri[8] = Public.CV_QL_PhanCongCongViecNhanSu_NhanSuKiemTra;
            giatri[9] = Public.CV_QL_PhanCongCongViecNhanSu_NgayBatDau;
            giatri[10] = Public.CV_QL_PhanCongCongViecNhanSu_NgayKetThuc;
            giatri[11] = Public.CV_QL_PhanCongCongViecNhanSu_DanhGia;
            giatri[12] = Public.CV_QL_PhanCongCongViecNhanSu_LyDo;
            giatri[13] = Public.HT_USER_Create;
            giatri[14] = Public.CV_QL_PhanCongCongViecNhanSu_DateCreate;
            giatri[15] = Public.CV_QL_PhanCongCongViecNhanSu_HienThi;
            giatri[16] = Public.CV_QL_PhanCongCongViecNhanSu_SuDung;
            return cls.Update("CV_QL_PhanCongCongViecNhanSu_Insert", bien, giatri, thamso);
        }
        public int CV_QL_PhanCongCongViecNhanSu_Update(CV_QL_PhanCongCongViecNhanSuPublic Public)
        {
            int thamso = 21;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_PhanCongCongViecNhanSu_TenLoaiCongViec";
            bien[1] = "@CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec1";
            bien[2] = "@CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec2";
            bien[3] = "@CV_QL_PhanCongCongViecNhanSu_TenCongViec";
            bien[4] = "@CV_QL_PhanCongCongViecNhanSu_MucDoKho";
            bien[5] = "@CV_QL_PhanCongCongViecNhanSu_NhanSuPhuTrach";
            bien[6] = "@CV_QL_PhanCongCongViecNhanSu_NhanSuThucHien";
            bien[7] = "@CV_QL_PhanCongCongViecNhanSu_NhanSuPhoiHop";
            bien[8] = "@CV_QL_PhanCongCongViecNhanSu_NhanSuKiemTra";
            bien[9] = "@CV_QL_PhanCongCongViecNhanSu_NgayBatDau";
            bien[10] = "@CV_QL_PhanCongCongViecNhanSu_NgayKetThuc";
            bien[11] = "@CV_QL_PhanCongCongViecNhanSu_DanhGia";
            bien[12] = "@CV_QL_PhanCongCongViecNhanSu_LyDo";
            bien[13] = "@HT_USER_Create";
            bien[14] = "@CV_QL_PhanCongCongViecNhanSu_DateCreate";
            bien[15] = "@CV_QL_PhanCongCongViecNhanSu_HienThi";
            bien[16] = "@CV_QL_PhanCongCongViecNhanSu_SuDung";
            bien[17] = "@CV_QL_PhanCongCongViecNhanSu_Id";
            bien[18] = "@HT_USER_Editor";
            bien[19] = "@CV_QL_PhanCongCongViecNhanSu_DateEditor";
            bien[20] = "@CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien";
            giatri[20] = Public.CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien;
            giatri[17] = Public.CV_QL_PhanCongCongViecNhanSu_ID;
            giatri[18] = Public.HT_USER_Editor;
            giatri[19] = Public.CV_QL_PhanCongCongViecNhanSu_DateEditor;
            giatri[0] = Public.CV_QL_PhanCongCongViecNhanSu_TenLoaiCongViec;
            giatri[1] = Public.CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec1;
            giatri[2] = Public.CV_QL_PhanCongCongViecNhanSu_TenNhomCongViec2;
            giatri[3] = Public.CV_QL_PhanCongCongViecNhanSu_TenCongViec;
            giatri[4] = Public.CV_QL_PhanCongCongViecNhanSu_MucDoKho;
            giatri[5] = Public.CV_QL_PhanCongCongViecNhanSu_NhanSuPhuTrach;
            giatri[6] = Public.CV_QL_PhanCongCongViecNhanSu_NhanSuThucHien;
            giatri[7] = Public.CV_QL_PhanCongCongViecNhanSu_NhanSuPhoiHop;
            giatri[8] = Public.CV_QL_PhanCongCongViecNhanSu_NhanSuKiemTra;
            giatri[9] = Public.CV_QL_PhanCongCongViecNhanSu_NgayBatDau;
            giatri[10] = Public.CV_QL_PhanCongCongViecNhanSu_NgayKetThuc;
            giatri[11] = Public.CV_QL_PhanCongCongViecNhanSu_DanhGia;
            giatri[12] = Public.CV_QL_PhanCongCongViecNhanSu_LyDo;
            giatri[13] = Public.HT_USER_Create;
            giatri[14] = Public.CV_QL_PhanCongCongViecNhanSu_DateCreate;
            giatri[15] = Public.CV_QL_PhanCongCongViecNhanSu_HienThi;
            giatri[16] = Public.CV_QL_PhanCongCongViecNhanSu_SuDung;
            return cls.Update("CV_QL_PhanCongCongViecNhanSu_Update", bien, giatri, thamso);
        }
    }
}
