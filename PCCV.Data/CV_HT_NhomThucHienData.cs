using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Uneti.Public;

namespace Uneti.Data
{
    public class CV_HT_NhomThucHienData
    {

        ClsKetNoi cls = new ClsKetNoi();
        public DataTable LoadCV_HT_NhomThucHien_LoadAll()
        {
            return cls.LayDuLieu("SP_CV_HT_NhomThucHien_Select");
        }

        public int CV_HT_NhomThucHien_Del(CV_HT_NhomThucHienPublic Public)
        {
            int thamso = 4;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_NhomThucHien_ID";
            bien[1] = "@CV_HT_NhomThucHien_DateEditor";
            bien[2] = "@HT_USER_Editor";
            bien[3] = "@CV_HT_NhomThucHien_SuDung";
            giatri[0] = Public.CV_HT_NhomThucHien_ID;
            giatri[1] = Public.CV_HT_NhomThucHien_DateEditor;
            giatri[2] = Public.HT_USER_Editor;
            giatri[3] = Public.CV_HT_NhomThucHien_SuDung;
            return cls.Update("SP_CV_HT_NhomThucHien_Del", bien, giatri, thamso);
        }
        public int CV_HT_NhomThucHien_Add(CV_HT_NhomThucHienPublic Public)
        {
            int thamso = 6;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_NhomThucHien_TenNhomThucHien";
            bien[1] = "@CV_HT_NhomThucHien_GhiChu";
            bien[2] = "@HT_USER_Create";
            bien[3] = "@CV_HT_NhomThucHien_DateCreate";
            bien[4] = "@CV_HT_NhomThucHien_HienThi";
            bien[5] = "@CV_HT_NhomThucHien_SuDung";

            giatri[0] = Public.CV_HT_NhomThucHien_TenNhomThucHien;
            giatri[1] = Public.CV_HT_NhomThucHien_GhiChu;
            giatri[2] = Public.HT_USER_Create;
            giatri[3] = Public.CV_HT_NhomThucHien_DateCreate;
            giatri[4] = Public.CV_HT_NhomThucHien_HienThi;
            giatri[5] = Public.CV_HT_NhomThucHien_SuDung;

            return cls.Update("SP_CV_HT_NhomThucHien_Insert", bien, giatri, thamso);
        }
        public int CV_HT_NhomThucHien_Edit(CV_HT_NhomThucHienPublic Public)
        {
            int thamso = 9;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];

            bien[0] = "@CV_HT_NhomThucHien_TenNhomThucHien";
            bien[1] = "@CV_HT_NhomThucHien_GhiChu";
            bien[2] = "@HT_USER_Create";
            bien[3] = "@CV_HT_NhomThucHien_DateCreate";
            bien[4] = "@HT_USER_Editor";
            bien[5] = "@CV_HT_NhomThucHien_DateEditor";
            bien[6] = "@CV_HT_NhomThucHien_HienThi";
            bien[7] = "@CV_HT_NhomThucHien_SuDung";
            bien[8] = "@CV_HT_NhomThucHien_ID";

            giatri[0] = Public.CV_HT_NhomThucHien_TenNhomThucHien;
            giatri[1] = Public.CV_HT_NhomThucHien_GhiChu;
            giatri[2] = Public.HT_USER_Create;
            giatri[3] = Public.CV_HT_NhomThucHien_DateCreate;
            giatri[4] = Public.HT_USER_Editor;
            giatri[5] = Public.CV_HT_NhomThucHien_DateEditor;
            giatri[6] = Public.CV_HT_NhomThucHien_HienThi;
            giatri[7] = Public.CV_HT_NhomThucHien_SuDung;
            giatri[8] = Public.CV_HT_NhomThucHien_ID;

            return cls.Update("SP_CV_HT_NhomThucHien_Edit", bien, giatri, thamso);
        }
    }
}
