using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCCV.Public;
using System.Data;
using System.Data.SqlClient;
namespace PCCV.Data
{
    public class CV_HT_MucDoKhoData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadCV_HT_MucDoKho_LoadAll()
        {
            return cls.LayDuLieu("SP_CV_HT_MucDoKho_Select");
        }
        public int CV_HT_MucDoKho_Del(CV_HT_MucDoKhoPublic Public)
        {
            int thamso = 4;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_MucDoKho_Id";
            bien[1] = "@CV_HT_MucDoKho_DateEdit";
            bien[2] = "@HT_USER_Editor";
            bien[3] = "@CV_HT_MucDoKho_SuDung";
            giatri[0] = Public.CV_HT_MucDoKho_Id;
            giatri[1] = Public.CV_HT_MucDoKho_DateEdit;
            giatri[2] = Public.HT_USER_Edit;
            giatri[3] = Public.CV_HT_MucDoKho_Sudung;
            return cls.Update("SP_CV_HT_MucDoKho_Del", bien, giatri, thamso);
        }
        public int CV_HT_MucDoKho_Add(CV_HT_MucDoKhoPublic Public)
        {
            int thamso = 7;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_MucDoKho_DoKhoCongViec";
            bien[1] = "@CV_HT_MucDoKho_Mota";
            bien[2] = "@CV_HT_MucDoKho_GhiChu";
            bien[3] = "@HT_USER_Create";
            bien[4] = "@CV_HT_MucDoKho_DateCreate";
            bien[5] = "@CV_HT_MucDoKho_Sudung";
            bien[6] = "@CV_HT_MucDoKho_HienThi";
            giatri[0] = Public.CV_HT_MucDoKho_DoKhoCongViec;
            giatri[1] = Public.CV_HT_MucDoKho_Mota;
            giatri[2] = Public.CV_HT_MucDoKho_GhiChu;
            giatri[3] = Public.HT_USER_Create;
            giatri[4] = Public.CV_HT_MucDoKho_DateCreate;
            giatri[5] = Public.CV_HT_MucDoKho_Sudung;
            giatri[6] = Public.CV_HT_MucDoKho_HienThi;
            return cls.Update("SP_CV_HT_MucDoKho_Insert", bien, giatri, thamso);
        }

        public int CV_HT_MucDoKho_Update(CV_HT_MucDoKhoPublic Public)
        {
            int thamso = 10;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_HT_MucDoKho_DoKhoCongViec";
            bien[1] = "@CV_HT_MucDoKho_Mota";
            bien[2] = "@CV_HT_MucDoKho_GhiChu";
            bien[3] = "@HT_USER_Create";
            bien[4] = "@HT_USER_Edit";
            bien[5] = "@CV_HT_MucDoKho_DateCreate";
            bien[6] = "@CV_HT_MucDoKho_DateEdit";
            bien[7] = "@CV_HT_MucDoKho_Sudung";
            bien[8] = "@CV_HT_MucDoKho_HienThi";
            bien[9] = "@CV_HT_MucDoKho_Id";
            giatri[0] = Public.CV_HT_MucDoKho_DoKhoCongViec;
            giatri[1] = Public.CV_HT_MucDoKho_Mota;
            giatri[2] = Public.CV_HT_MucDoKho_GhiChu;
            giatri[3] = Public.HT_USER_Create;
            giatri[4] = Public.HT_USER_Edit;
            giatri[5] = Public.CV_HT_MucDoKho_DateCreate;
            giatri[6] = Public.CV_HT_MucDoKho_DateEdit;
            giatri[7] = Public.CV_HT_MucDoKho_Sudung;
            giatri[8] = Public.CV_HT_MucDoKho_HienThi;
            giatri[9] = Public.CV_HT_MucDoKho_Id;
            return cls.Update("SP_CV_HT_MucDoKho_Update", bien, giatri, thamso);
        }
    }
}
