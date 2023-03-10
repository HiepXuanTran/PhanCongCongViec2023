using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uneti.Data;
using Uneti.Public;
using System.Data;
using System.Data.SqlClient;

namespace Uneti.BLL
{
    public class CV_QL_NhanSuBLL
    {
        CV_QL_NhanSuData cls = new CV_QL_NhanSuData();

        public DataTable LoadCV_QL_NhanSu_LoadUser()
        {
            return cls.LoadCV_QL_NhanSu_LoadUser();
        }

        public int CV_QL_NhanSu_Del(CV_QL_NhanSuPublic Public)
        {
            return cls.CV_QL_NhanSu_Del(Public);
        }
        public int CV_QL_NhanSu_Add(CV_QL_NhanSuPublic Public)
        {
            return cls.CV_QL_NhanSu_Add(Public);
        }
        public int CV_QL_NhanSu_Edit(CV_QL_NhanSuPublic Public)
        {
            return cls.CV_QL_NhanSu_Edit(Public);
        }

        public DataTable CV_QL_NhanSu_ReturnID(CV_QL_NhanSuPublic Public)
        {
            return cls.CV_QL_NhanSu_ReturnID(Public);
        }
    }
}
