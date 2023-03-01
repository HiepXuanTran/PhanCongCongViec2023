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
    public class CV_HT_VaiTroCongViecBLL
    {
        CV_HT_VaiTroCongViecData cls = new CV_HT_VaiTroCongViecData();
        public DataTable LoadCV_HT_VaiTroCongViec_LoadAll()
        {
            return cls.LoadCV_HT_VaiTroCongViec_LoadAll();
        }
        public int CV_HT_VaiTroCongViec_Del(CV_HT_VaiTroCongViecPublic Public)
        {
            return cls.CV_HT_VaiTroCongViec_Del(Public);
        }
        public int CV_HT_VaiTroCongViec_Add(CV_HT_VaiTroCongViecPublic Public)
        {
            return cls.CV_HT_VaiTroCongViec_Add(Public);
        }
        public int CV_HT_VaiTroCongViec_Edit(CV_HT_VaiTroCongViecPublic Public)
        {
            return cls.CV_HT_VaiTroCongViec_Edit(Public);
        }
        public DataTable CV_HT_VaiTroCongViec_ReturnID(CV_HT_VaiTroCongViecPublic Public)
        {
            return cls.CV_HT_VaiTroCongViec_ReturnID(Public);
        }
    }
}
