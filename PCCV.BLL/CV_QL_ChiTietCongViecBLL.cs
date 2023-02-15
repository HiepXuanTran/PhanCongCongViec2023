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
    public class CV_QL_ChiTietCongViecBLL
    {
        CV_QL_ChiTietCongViecData cls = new CV_QL_ChiTietCongViecData();
        public DataTable LoadCV_QL_ChiTietCongViec()
        {
            return cls.LoadCV_QL_ChiTietCongViec();
        }
        public int CV_QL_ChiTietCongViec_Del(CV_QL_ChiTietCongViecPublic Public)
        {
            return cls.CV_QL_ChiTietCongViec_Del(Public);
        }
        public int CV_QL_ChiTietCongViec_Add(CV_QL_ChiTietCongViecPublic Public)
        {
            return cls.CV_QL_ChiTietCongViec_Add(Public);
        }
        public int CV_QL_ChiTietCongViec_Edit(CV_QL_ChiTietCongViecPublic Public)
        {
            return cls.CV_QL_ChiTietCongViec_Edit(Public);
        }
        public SqlDataReader LoadCV_QL_ChiTietCongViec_Load_R_Para_File(CV_QL_ChiTietCongViecPublic Public)
        {
            return cls.LoadCV_QL_ChiTietCongViec_Load_R_Para_File(Public);
        }
    }
}
