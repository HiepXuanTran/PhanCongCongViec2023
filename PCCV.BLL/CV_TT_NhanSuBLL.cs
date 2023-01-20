using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCCV.Data;
using PCCV.Public;
using System.Data;
using System.Data.SqlClient;

namespace PCCV.BLL
{
    public class CV_TT_NhanSuBLL
    {
        CV_TT_NhanSuData cls = new CV_TT_NhanSuData();
        public DataTable LoadCV_TT_NhanSu_LoadAll()
        {
            return cls.LoadCV_TT_NhanSu_LoadAll();
        }
        public int CV_TT_NhanSu_Del(CV_TT_NhanSuPublic Public)
        {
            return cls.CV_TT_NhanSu_Del(Public);
        }
        public int CV_TT_NhanSu_Add(CV_TT_NhanSuPublic Public)
        {
            return cls.CV_TT_NhanSu_Add(Public);
        }
        public int CV_TT_NhanSu_Edit(CV_TT_NhanSuPublic Public)
        {
            return cls.CV_TT_NhanSu_Edit(Public);
        }
    }
}
