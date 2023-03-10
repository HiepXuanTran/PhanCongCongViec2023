using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uneti.Public;
using Uneti.Data;
using System.Data;
using System.Data.SqlClient;

namespace Uneti.BLL
{
    public class CV_PC_PhanCongCongViecNhanSuBLL
    {
        CV_PC_PhanCongCongViecNhanSuData cls = new CV_PC_PhanCongCongViecNhanSuData();
        public DataTable LoadCV_PC_PhanCongCongViecNhanSu()
        {
            return cls.LoadCV_PC_PhanCongCongViecNhanSu();
        }
        public int CV_PC_PhanCongCongViecNhanSu_Insert(CV_PC_PhanCongCongViecNhanSuPublic Public)
        {
            return cls.CV_PC_PhanCongCongViecNhanSu_Insert(Public);
        }
        public int CV_PC_PhanCongCongViecNhanSu_Del(CV_PC_PhanCongCongViecNhanSuPublic Public)
        {
            return cls.CV_PC_PhanCongCongViecNhanSu_Del(Public);
        }
        public int CV_PC_PhanCongCongViecNhanSu_Update(CV_PC_PhanCongCongViecNhanSuPublic Public)
        {
            return cls.CV_PC_PhanCongCongViecNhanSu_Update(Public);
        }
    }
}
