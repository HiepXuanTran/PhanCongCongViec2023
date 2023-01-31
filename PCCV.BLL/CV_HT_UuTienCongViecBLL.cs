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
    public class CV_HT_UuTienCongViecBLL
    {
        CV_HT_UuTienCongViecData cls = new CV_HT_UuTienCongViecData();
        public DataTable LoadCV_HT_UuTienCongViec_LoadAll()
        {
            return cls.LoadCV_HT_UuTienCongViec_LoadAll();
        }
        public int CV_HT_UuTienCongViec_Del(CV_HT_UuTienCongViecPublic Public)
        {
            return cls.CV_HT_UuTienCongViec_Del(Public);
        }
        public int CV_HT_UuTienCongViec_Add(CV_HT_UuTienCongViecPublic Public)
        {
            return cls.CV_HT_UuTienCongViec_Add(Public);
        }
        public int CV_HT_UuTienCongViec_Edit(CV_HT_UuTienCongViecPublic Public)
        {
            return cls.CV_HT_UuTienCongViec_Edit(Public);
        }
    }
}
