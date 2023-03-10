using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Uneti.Public;
using Uneti.Data;
namespace Uneti.BLL
{
    public class CV_TD_LichSuCongViecBLL
    {
        CV_TD_LichSuNhomCongViecData cls = new CV_TD_LichSuNhomCongViecData();
        public DataTable LoadLichSuCongViec_LoadAll()
        {
            return cls.LoadLichSuCongViec_LoadAll();
        }
        public DataTable LoadLichSuCongViec_Load1(CV_QL_NhomCongViecPublic Public)
        {
            return cls.LoadLichSuCongViec_Load1(Public);
        }
    }
}
