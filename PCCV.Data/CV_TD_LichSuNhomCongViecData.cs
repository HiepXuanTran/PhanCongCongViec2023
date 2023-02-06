using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PCCV.Public;
namespace PCCV.Data
{
    public class CV_TD_LichSuNhomCongViecData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadLichSuCongViec_LoadAll()
        {
            return cls.LayDuLieu("SP_CV_TD_LichSuNhomCongViec_Select");
        }
        public DataTable LoadLichSuCongViec_Load1(CV_QL_NhomCongViecPublic Public)
        {
            int thamso = 1;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@CV_TD_LichSuCongViecID";
            giatri[0] = Public.CV_QL_NhomCongViec_ID;
            return cls.LayDuLieu("SP_CV_TD_LichSuNhomCongViec_Select1", bien, giatri, thamso);
        }
    }
}
