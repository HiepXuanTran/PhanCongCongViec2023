﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCCV.Public;
using System.Data;
using System.Data.SqlClient;

namespace PCCV.Data
{
    public class CV_TT_ChiTietCongViecData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadCV_TT_ChiTietCongViec()
        {
            return cls.LayDuLieu("CV_TT_ChiTietCongViec_Select");
        }
       
        public int CV_TT_ChiTietCongViec_Del(CV_TT_ChiTietCongViecPublic Public)
        {
            int thamso = 1;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_TT_ChiTietCongViec_ID";
            giatri[0] = Public.Id;
            return cls.Update("CV_TT_ChiTietCongViec_Del", bien, giatri, thamso);
        }
        public int CV_TT_ChiTietCongViec_Add(CV_TT_ChiTietCongViecPublic Public)
        {
            int thamso = 13;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_TT_ChiTietCongViec_TenCongViec";
            bien[1] = "@CV_TT_ChiTietCongViec_CacBuocCongViec";
            bien[2] = "@CV_TT_ChiTietCongViec_MoTaBuocCongViec";
            bien[3] = "@CV_TT_ChiTietCongViec_MucDoKho";
            bien[4] = "@CV_TT_ChiTietCongViec_TenFile";
            bien[5] = "@CV_TT_ChiTietCongViec_FileDinhKem";
            bien[6] = "@CV_TT_ChiTietCongViec_TongSoPhutThucHien";
            bien[7] = "@CV_TT_ChiTietCongViec_TongSoGioThucHien";
            bien[8] = "@CV_TT_ChiTietCongViec_TongSoNgayThucHien";
            bien[9] = "@HT_USER_Create";
            bien[10] = "@CV_TT_ChiTietCongViec_DateCreate";
            bien[11] = "@CV_TT_ChiTietCongViec_HienThi";
            bien[12] = "@CV_TT_ChiTietCongViec_SuDung";



            giatri[0] = Public.TenCongViec;
            giatri[1] = Public.CacBuocCongViec;
            giatri[2] = Public.MoTaBuocCongViec;
            giatri[3] = Public.MucDoKho;
            giatri[4] = Public.TenFile;
            giatri[5] = Public.FileDinhKem;
            giatri[6] = Public.SoPhutThucHien;
            giatri[7] = Public.SoGioThucHien;
            giatri[8] = Public.SoNgayThucHien;
            giatri[9] = Public.HT_USER_Create;
            giatri[10] = Public.CV_TT_ChiTietCongViec_DateCreate;
            giatri[11] = Public.CV_TT_ChiTietCongViec_HienThi;
            giatri[12] = Public.CV_TT_ChiTietCongViec_SuDung;

            return cls.Update("CV_TT_ChiTietCongViec_Insert", bien, giatri, thamso);
        }
        public int CV_TT_ChiTietCongViec_Edit(CV_TT_ChiTietCongViecPublic Public)
        {
            int thamso = 16;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];

            bien[0] = "@CV_TT_ChiTietCongViec_ID";
            bien[1] = "@CV_TT_ChiTietCongViec_TenCongViec";
            bien[2] = "@CV_TT_ChiTietCongViec_CacBuocCongViec";
            bien[3] = "@CV_TT_ChiTietCongViec_MoTaBuocCongViec";
            bien[4] = "@CV_TT_ChiTietCongViec_MucDoKho";
            bien[5] = "@CV_TT_ChiTietCongViec_TenFile";
            bien[6] = "@CV_TT_ChiTietCongViec_FileDinhKem";
            bien[7] = "@CV_TT_ChiTietCongViec_TongSoPhutThucHien";
            bien[8] = "@CV_TT_ChiTietCongViec_TongSoGioThucHien";
            bien[9] = "@CV_TT_ChiTietCongViec_TongSoNgayThucHien";
            bien[10] = "@HT_USER_Create";
            bien[11] = "@CV_TT_ChiTietCongViec_DateCreate";
            bien[12] = "@HT_USER_Editor";
            bien[13] = "@CV_TT_ChiTietCongViec_DateEditor";
            bien[14] = "@CV_TT_ChiTietCongViec_HienThi";
            bien[15] = "@CV_TT_ChiTietCongViec_SuDung";

            

            giatri[0] = Public.Id;
            giatri[1] = Public.TenCongViec;
            giatri[2] = Public.CacBuocCongViec;
            giatri[3] = Public.MoTaBuocCongViec;
            giatri[4] = Public.MucDoKho;
            giatri[5] = Public.TenFile;
            giatri[6] = Public.FileDinhKem;
            giatri[7] = Public.SoPhutThucHien;
            giatri[8] = Public.SoGioThucHien;
            giatri[9] = Public.SoNgayThucHien;
            giatri[10] = Public.HT_USER_Create;
            giatri[11] = Public.CV_TT_ChiTietCongViec_DateCreate;
            giatri[12] = Public.HT_USER_Editor;
            giatri[13] = Public.CV_TT_ChiTietCongViec_DateEditor;
            giatri[14] = Public.CV_TT_ChiTietCongViec_HienThi;
            giatri[15] = Public.CV_TT_ChiTietCongViec_SuDung;
            
            
            return cls.Update("CV_TT_ChiTietCongViec_Update", bien, giatri, thamso);
        }
    }
}
