using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Uneti.Data;
using Uneti.Public;
namespace Uneti.BLL
{
    public class CV_QL_NhomCongViecBLL
    {
        CV_QL_NhomCongViecData cls = new CV_QL_NhomCongViecData();
        public DataTable LoadCV_QL_NhomCongViec_LoadAll()
        {
            return cls.LoadCV_QL_NhomCongViec_LoadAll();
        }
        public int CV_QL_NhomCongViec_Del(CV_QL_NhomCongViecPublic Public)
        {
            return cls.CV_QL_NhomCongViec_Del(Public);
        }
        public int CV_QL_NhomCongViec_Add(CV_QL_NhomCongViecPublic Public)
        {
            return cls.CV_QL_NhomCongViec_Add(Public);
        }
        public int CV_QL_NhomCongViec_Edit(CV_QL_NhomCongViecPublic Public)
        {
            return cls.CV_QL_NhomCongViec_Edit(Public);
        }
    }
}
