using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCCV.Public;
using System.Data;
using System.Data.SqlClient;

namespace PCCV.Data
{
    public class CV_QL_CongViecData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadCV_QL_CongViec()
        {
            return cls.LayDuLieu("SP_CV_QL_CongViec_Select");
        }
        public SqlDataReader LoadCV_QL_CongViec_Load_R_Para_File(CV_QL_CongViecPublic Public)
        {
            int thamso = 1;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_CongViec_Id";
            giatri[0] = Public.Id;
            return cls.LayDuLieu_R("SP_CV_QL_CongViec_Load_R_Para_File", bien, giatri, thamso);
        }
        //public DataTable LoadNhanSu()
        //{
        //    return cls.LayDuLieu("CV_QL_CongViec_NhomThucHien_CbEdit");
        //}
        public int CV_QL_CongViec_Del(CV_QL_CongViecPublic Public)
        {
            int thamso = 4;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_CongViec_ID";
            bien[1]="@HT_USER_Editor";
            bien[2]="@CV_QL_CongViec_DateEditor";
            bien[3] = "@CV_QL_CongViec_SuDung";
            giatri[0] = Public.Id;
            giatri[1] = Public.HT_USER_Editor;
            giatri[2] = Public.CV_QL_CongViec_DateEditor;
            giatri[3] = Public.CV_QL_CongViec_SuDung;
            return cls.Update("SP_CV_QL_CongViec_Del", bien, giatri, thamso);
        }
        public int CV_QL_CongViec_Add(CV_QL_CongViecPublic Public)
        {
            int thamso = 18;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_CongViec_TenLoaiCongViec";
            bien[1] = "@CV_QL_CongViec_TenNhomCongViec1";
            bien[2] = "@CV_QL_CongViec_TenNhomCongViec2";
            bien[3] = "@CV_QL_CongViec_TenCongViec";
            bien[4] = "@CV_QL_CongViec_ChiTietCongViec";
            bien[5] = "@CV_QL_CongViec_MoTaCongViec";
            bien[6] = "@CV_QL_CongViec_NhomThucHien";
            bien[7] = "@CV_QL_CongViec_MucDoKho";
            bien[8] = "@CV_QL_CongViec_FileDinhKem";
            bien[9] = "@CV_QL_CongViec_KhaNangChuyenMon";
            bien[10] = "@CV_QL_CongViec_TongSoPhutThucHien";
            bien[11] = "@CV_QL_CongViec_TongSoGioThucHien";
            bien[12] = "@CV_QL_CongViec_TongSoNgayThucHien";
            bien[13] = "@HT_USER_Create";
            bien[14] = "@CV_QL_CongViec_DateCreate";
            bien[15] = "@CV_QL_CongViec_HienThi";
            bien[16] = "@CV_QL_CongViec_SuDung";
            bien[17] = "@CV_QL_CongViec_TenFile";


            giatri[0] = Public.TenLoaiCongViec;
            giatri[1] = Public.TenNhomCongViec1;
            giatri[2] = Public.TenNhomCongViec2;
            giatri[3] = Public.TenCongViec;
            giatri[4] = Public.ChiTietCongViec;
            giatri[5] = Public.MoTaCongViec;
            giatri[6] = Public.NhomThucHien;
            giatri[7] = Public.MucDoKho;
            giatri[8] = Public.FileDinhKem;
            giatri[9] = Public.KhaNangChuyenMon;
            giatri[10] = Public.SoPhutThucHien;
            giatri[11] = Public.SoGioThucHien;
            giatri[12] = Public.SoNgayThucHien;
            giatri[13] = Public.HT_USER_Create;
            giatri[14] = Public.CV_QL_CongViec_DateCreate;
            giatri[15] = Public.CV_QL_CongViec_HienThi;
            giatri[16] = Public.CV_QL_CongViec_SuDung;
            giatri[17] = Public.TenFile;
            return cls.Update("SP_CV_QL_CongViec_Insert", bien, giatri, thamso);
        }
        public int CV_QL_CongViec_Edit(CV_QL_CongViecPublic Public)
        {
            int thamso = 21;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_CongViec_TenLoaiCongViec";
            bien[1] = "@CV_QL_CongViec_TenNhomCongViec1";
            bien[2] = "@CV_QL_CongViec_TenNhomCongViec2";
            bien[3] = "@CV_QL_CongViec_TenCongViec";
            bien[4] = "@CV_QL_CongViec_ChiTietCongViec";
            bien[5] = "@CV_QL_CongViec_MoTaCongViec";
            bien[6] = "@CV_QL_CongViec_NhomThucHien";
            bien[7] = "@CV_QL_CongViec_MucDoKho";
            bien[8] = "@CV_QL_CongViec_FileDinhKem";
            bien[9] = "@CV_QL_CongViec_KhaNangChuyenMon";
            bien[10] = "@CV_QL_CongViec_TongSoPhutThucHien";
            bien[11] = "@CV_QL_CongViec_TongSoGioThucHien";
            bien[12] = "@CV_QL_CongViec_TongSoNgayThucHien";
            bien[13] = "@HT_USER_Create";
            bien[14] = "@CV_QL_CongViec_DateCreate";
            bien[15] = "@HT_USER_Editor";
            bien[16] = "@CV_QL_CongViec_DateEditor";
            bien[17] = "@CV_QL_CongViec_HienThi";
            bien[18] = "@CV_QL_CongViec_SuDung";
            bien[19] = "@CV_QL_CongViec_ID";
            bien[20] = "@CV_QL_CongViec_TenFile";
            giatri[0] = Public.TenLoaiCongViec;
            giatri[1] = Public.TenNhomCongViec1;
            giatri[2] = Public.TenNhomCongViec2;
            giatri[3] = Public.TenCongViec;
            giatri[4] = Public.ChiTietCongViec;
            giatri[5] = Public.MoTaCongViec;
            giatri[6] = Public.NhomThucHien;
            giatri[7] = Public.MucDoKho;
            giatri[8] = Public.FileDinhKem;
            giatri[9] = Public.KhaNangChuyenMon;
            giatri[10] = Public.SoPhutThucHien;
            giatri[11] = Public.SoGioThucHien;
            giatri[12] = Public.SoNgayThucHien;
            giatri[13] = Public.HT_USER_Create;
            giatri[14] = Public.CV_QL_CongViec_DateCreate;
            giatri[15] = Public.HT_USER_Editor;
            giatri[16] = Public.CV_QL_CongViec_DateEditor;
            giatri[17] = Public.CV_QL_CongViec_HienThi;
            giatri[18] = Public.CV_QL_CongViec_SuDung;
            giatri[19] = Public.Id;
            giatri[20] = Public.TenFile;
            return cls.Update("SP_CV_QL_CongViec_Update", bien, giatri, thamso);
        }
    }
}
