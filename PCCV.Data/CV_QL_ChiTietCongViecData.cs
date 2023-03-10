using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uneti.Public;
using System.Data;
using System.Data.SqlClient;

namespace Uneti.Data
{
    public class CV_QL_ChiTietCongViecData
    {
        ClsKetNoi cls = new ClsKetNoi();
        public DataTable LoadCV_QL_ChiTietCongViec()
        {
            return cls.LayDuLieu("SP_CV_QL_ChiTietCongViec_Select");
        }
       
        public int CV_QL_ChiTietCongViec_Del(CV_QL_ChiTietCongViecPublic Public)
        {
            int thamso = 5;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_ChiTietCongViec_ID";
            bien[1] = "@CV_QL_ChiTietCongViec_DateEditor";
            bien[2] = "@HT_USER_Editor";
            bien[3] = "@CV_QL_ChiTietCongViec_SuDung";
            bien[4] = "@CV_QL_ChiTietCongViec_IDCongViec";
            giatri[0] = Public.Id;
            giatri[1] = Public.CV_QL_ChiTietCongViec_DateEditor;
            giatri[2] = Public.HT_USER_Editor;
            giatri[3] = Public.CV_QL_ChiTietCongViec_SuDung;
            giatri[4] = Public.CV_QL_ChiTietCongViec_IDCongViec;
            return cls.Update("SP_CV_QL_ChiTietCongViec_Del", bien, giatri, thamso);
        }
        public SqlDataReader LoadCV_QL_ChiTietCongViec_Load_R_Para_File(CV_QL_ChiTietCongViecPublic Public)
        {
            int thamso = 1;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_QL_ChiTietCongViec_Id";
            giatri[0] = Public.Id;
            return cls.LayDuLieu_R("SP_CV_QL_ChiTietCongViec_Load_R_Para_File", bien, giatri, thamso);
        }
        public int CV_QL_ChiTietCongViec_Add(CV_QL_ChiTietCongViecPublic Public)
        {
            int thamso = 13;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "CV_QL_ChiTietCongViec_IDCongViec";
            bien[1] = "@CV_QL_ChiTietCongViec_CacBuocCongViec";
            bien[2] = "@CV_QL_ChiTietCongViec_MoTaBuocCongViec";
            bien[3] = "@CV_QL_ChiTietCongViec_MucDoKho";
            bien[4] = "@CV_QL_ChiTietCongViec_TenFile";
            bien[5] = "@CV_QL_ChiTietCongViec_FileDinhKem";
            bien[6] = "@CV_QL_ChiTietCongViec_TongSoPhutThucHien";
            bien[7] = "@CV_QL_ChiTietCongViec_TongSoGioThucHien";
            bien[8] = "@CV_QL_ChiTietCongViec_TongSoNgayThucHien";
            bien[9] = "@HT_USER_Create";
            bien[10] = "@CV_QL_ChiTietCongViec_DateCreate";
            bien[11] = "@CV_QL_ChiTietCongViec_HienThi";
            bien[12] = "@CV_QL_ChiTietCongViec_SuDung";



            giatri[0] = Public.CV_QL_ChiTietCongViec_IDCongViec;
            giatri[1] = Public.CacBuocCongViec;
            giatri[2] = Public.MoTaBuocCongViec;
            giatri[3] = Public.MucDoKho;
            giatri[4] = Public.TenFile;
            giatri[5] = Public.FileDinhKem;
            giatri[6] = Public.SoPhutThucHien;
            giatri[7] = Public.SoGioThucHien;
            giatri[8] = Public.SoNgayThucHien;
            giatri[9] = Public.HT_USER_Create;
            giatri[10] = Public.CV_QL_ChiTietCongViec_DateCreate;
            giatri[11] = Public.CV_QL_ChiTietCongViec_HienThi;
            giatri[12] = Public.CV_QL_ChiTietCongViec_SuDung;

            return cls.Update("SP_CV_QL_ChiTietCongViec_Insert", bien, giatri, thamso);
        }
        public int CV_QL_ChiTietCongViec_Edit(CV_QL_ChiTietCongViecPublic Public)
        {
            int thamso = 16;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];

            bien[0] = "@CV_QL_ChiTietCongViec_ID";
            bien[1] = "@CV_QL_ChiTietCongViec_IDCongViec";
            bien[2] = "@CV_QL_ChiTietCongViec_CacBuocCongViec";
            bien[3] = "@CV_QL_ChiTietCongViec_MoTaBuocCongViec";
            bien[4] = "@CV_QL_ChiTietCongViec_MucDoKho";
            bien[5] = "@CV_QL_ChiTietCongViec_TenFile";
            bien[6] = "@CV_QL_ChiTietCongViec_FileDinhKem";
            bien[7] = "@CV_QL_ChiTietCongViec_TongSoPhutThucHien";
            bien[8] = "@CV_QL_ChiTietCongViec_TongSoGioThucHien";
            bien[9] = "@CV_QL_ChiTietCongViec_TongSoNgayThucHien";
            bien[10] = "@HT_USER_Create";
            bien[11] = "@CV_QL_ChiTietCongViec_DateCreate";
            bien[12] = "@HT_USER_Editor";
            bien[13] = "@CV_QL_ChiTietCongViec_DateEditor";
            bien[14] = "@CV_QL_ChiTietCongViec_HienThi";
            bien[15] = "@CV_QL_ChiTietCongViec_SuDung";

            

            giatri[0] = Public.Id;
            giatri[1] = Public.CV_QL_ChiTietCongViec_IDCongViec;
            giatri[2] = Public.CacBuocCongViec;
            giatri[3] = Public.MoTaBuocCongViec;
            giatri[4] = Public.MucDoKho;
            giatri[5] = Public.TenFile;
            giatri[6] = Public.FileDinhKem;
            giatri[7] = Public.SoPhutThucHien;
            giatri[8] = Public.SoGioThucHien;
            giatri[9] = Public.SoNgayThucHien;
            giatri[10] = Public.HT_USER_Create;
            giatri[11] = Public.CV_QL_ChiTietCongViec_DateCreate;
            giatri[12] = Public.HT_USER_Editor;
            giatri[13] = Public.CV_QL_ChiTietCongViec_DateEditor;
            giatri[14] = Public.CV_QL_ChiTietCongViec_HienThi;
            giatri[15] = Public.CV_QL_ChiTietCongViec_SuDung;
            return cls.Update("SP_CV_QL_ChiTietCongViec_Update", bien, giatri, thamso);
        }
    }
}
