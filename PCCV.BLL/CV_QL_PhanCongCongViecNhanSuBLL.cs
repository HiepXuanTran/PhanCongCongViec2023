using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PCCV.Public;
using PCCV.Data;
using System.Data;
using System.Data.SqlClient;

namespace PCCV.BLL
{
    public class CV_QL_PhanCongCongViecNhanSuBLL
    {
        CV_QL_PhanCongCongViecNhanSuData cls = new CV_QL_PhanCongCongViecNhanSuData();
        public DataTable LoadCV_QL_PhanCongCongViecNhanSu()
        {
            return cls.LoadCV_QL_PhanCongCongViecNhanSu();
        }
        public int CV_QL_PhanCongCongViecNhanSu_Insert(CV_QL_PhanCongCongViecNhanSuPublic Public)
        {
            return cls.CV_QL_PhanCongCongViecNhanSu_Insert(Public);
        }
        public int CV_QL_PhanCongCongViecNhanSu_Del(CV_QL_PhanCongCongViecNhanSuPublic Public)
        {
            return cls.CV_QL_PhanCongCongViecNhanSu_Del(Public);
        }
        public int CV_QL_PhanCongCongViecNhanSu_Update(CV_QL_PhanCongCongViecNhanSuPublic Public)
        {
            return cls.CV_QL_PhanCongCongViecNhanSu_Update(Public);
        }
    }
}
