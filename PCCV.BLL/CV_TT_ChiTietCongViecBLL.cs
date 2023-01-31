using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCCV.Data;
using PCCV.Public;
using System.Data.SqlClient;
using System.Data;

namespace PCCV.BLL
{
    public class CV_TT_ChiTietCongViecBLL
    {
        CV_TT_ChiTietCongViecData cls = new CV_TT_ChiTietCongViecData();
        public DataTable LoadCV_TT_ChiTietCongViec()
        {
            return cls.LoadCV_TT_ChiTietCongViec();
        }
        public int CV_TT_ChiTietCongViec_Del(CV_TT_ChiTietCongViecPublic Public)
        {
            return cls.CV_TT_ChiTietCongViec_Del(Public);
        }
        public int CV_TT_ChiTietCongViec_Add(CV_TT_ChiTietCongViecPublic Public)
        {
            return cls.CV_TT_ChiTietCongViec_Add(Public);
        }
        public int CV_TT_ChiTietCongViec_Edit(CV_TT_ChiTietCongViecPublic Public)
        {
            return cls.CV_TT_ChiTietCongViec_Edit(Public);
        }
    }
}
