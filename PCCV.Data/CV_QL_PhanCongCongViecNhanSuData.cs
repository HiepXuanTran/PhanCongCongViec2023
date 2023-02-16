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
            return cls.LayDuLieu("SP_CV_QL_PhanCongCongViecNhanSu_Select");
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
            return cls.Update("SP_CV_QL_PhanCongCongViecNhanSu_Del", bien, giatri, thamso);
        }
        public int CV_QL_PhanCongCongViecNhanSu_Insert(CV_QL_PhanCongCongViecNhanSuPublic Public)
        {
            int thamso = 14;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_PhanCongCongViecNhanSu_IDCongViec";
            bien[1] = "@CV_QL_PhanCongCongViecNhanSu_IDNhanSuPhuTrach";
            bien[2] = "@CV_QL_PhanCongCongViecNhanSu_NgayBatDau";
            bien[3] = "@CV_QL_PhanCongCongViecNhanSu_NgayKetThuc";
            bien[4] = "@CV_QL_PhanCongCongViecNhanSu_DanhGia";
            bien[5] = "@CV_QL_PhanCongCongViecNhanSu_LyDo";
            bien[6] = "@HT_USER_Create";
            bien[7] = "@CV_QL_PhanCongCongViecNhanSu_DateCreate";
            bien[8] = "@CV_QL_PhanCongCongViecNhanSu_HienThi";
            bien[9] = "@CV_QL_PhanCongCongViecNhanSu_SuDung";
            bien[10] = "@CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien";
            bien[11] = "@CV_QL_PhanCongCongViecNhanSu_IDNhanSuThucHien";
            bien[12] = "@CV_QL_PhanCongCongViecNhanSu_IDNhanSuPhoiHop";
            bien[13] = "@CV_QL_PhanCongCongViecNhanSu_IDNhanSuKiemTra";

            giatri[0] = Public.CV_QL_PhanCongCongViecNhanSu_IDCongViec;
            giatri[1] = Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuPhuTrach;
            giatri[2] = Public.CV_QL_PhanCongCongViecNhanSu_NgayBatDau;
            giatri[3] = Public.CV_QL_PhanCongCongViecNhanSu_NgayKetThuc;
            giatri[4] = Public.CV_QL_PhanCongCongViecNhanSu_DanhGia;
            giatri[5] = Public.CV_QL_PhanCongCongViecNhanSu_LyDo;
            giatri[6] = Public.HT_USER_Create;
            giatri[7] = Public.CV_QL_PhanCongCongViecNhanSu_DateCreate;
            giatri[8] = Public.CV_QL_PhanCongCongViecNhanSu_HienThi;
            giatri[9] = Public.CV_QL_PhanCongCongViecNhanSu_SuDung;
            giatri[10] = Public.CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien;
            giatri[11] = Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuThucHien;
            giatri[12] = Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuPhoiHop;
            giatri[13] = Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuKiemTra;
            return cls.Update("SP_CV_QL_PhanCongCongViecNhanSu_Insert", bien, giatri, thamso);
        }
        public int CV_QL_PhanCongCongViecNhanSu_Update(CV_QL_PhanCongCongViecNhanSuPublic Public)
        {
            int thamso = 17;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_PhanCongCongViecNhanSu_IDCongViec";
            bien[1] = "@CV_QL_PhanCongCongViecNhanSu_IDNhanSuPhuTrach";
            bien[2] = "@CV_QL_PhanCongCongViecNhanSu_NgayBatDau";
            bien[3] = "@CV_QL_PhanCongCongViecNhanSu_NgayKetThuc";
            bien[4] = "@CV_QL_PhanCongCongViecNhanSu_DanhGia";
            bien[5] = "@CV_QL_PhanCongCongViecNhanSu_LyDo";
            bien[6] = "@HT_USER_Create";
            bien[7] = "@CV_QL_PhanCongCongViecNhanSu_DateCreate";
            bien[8] = "@CV_QL_PhanCongCongViecNhanSu_HienThi";
            bien[9] = "@CV_QL_PhanCongCongViecNhanSu_SuDung";
            bien[10] = "@CV_QL_PhanCongCongViecNhanSu_Id";
            bien[11] = "@HT_USER_Editor";
            bien[12] = "@CV_QL_PhanCongCongViecNhanSu_DateEditor";
            bien[13] = "@CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien";
            bien[14] = "@CV_QL_PhanCongCongViecNhanSu_IDNhanSuThucHien";
            bien[15] = "@CV_QL_PhanCongCongViecNhanSu_IDNhanSuPhoiHop";
            bien[16] = "@CV_QL_PhanCongCongViecNhanSu_IDNhanSuKiemTra";
            giatri[0] = Public.CV_QL_PhanCongCongViecNhanSu_IDCongViec;
            giatri[1] = Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuPhuTrach;
            giatri[2] = Public.CV_QL_PhanCongCongViecNhanSu_NgayBatDau;
            giatri[3] = Public.CV_QL_PhanCongCongViecNhanSu_NgayKetThuc;
            giatri[4] = Public.CV_QL_PhanCongCongViecNhanSu_DanhGia;
            giatri[5] = Public.CV_QL_PhanCongCongViecNhanSu_LyDo;
            giatri[6] = Public.HT_USER_Create;
            giatri[7] = Public.CV_QL_PhanCongCongViecNhanSu_DateCreate;
            giatri[8] = Public.CV_QL_PhanCongCongViecNhanSu_HienThi;
            giatri[9] = Public.CV_QL_PhanCongCongViecNhanSu_SuDung;
            giatri[10] = Public.CV_QL_PhanCongCongViecNhanSu_ID;
            giatri[11] = Public.HT_USER_Editor;
            giatri[12] = Public.CV_QL_PhanCongCongViecNhanSu_DateEditor;
            giatri[13] = Public.CV_QL_PhanCongCongViecNhanSu_DiaDiemThucHien;
            giatri[14] = Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuThucHien;
            giatri[15] = Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuPhoiHop;
            giatri[16] = Public.CV_QL_PhanCongCongViecNhanSu_IDNhanSuKiemTra;
            return cls.Update("SP_CV_QL_PhanCongCongViecNhanSu_Update", bien, giatri, thamso);
        }
    }
}
