using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PCCV.Data;
using PCCV.Public;
namespace PCCV.BLL
{
    public class CV_HT_LoaiCongViecBLL
    {
        CV_HT_LoaiCongViecData cls = new CV_HT_LoaiCongViecData();
        public DataTable LoadCV_HT_LoaiCongViec_LoadAll()
        {
            return cls.LoadCV_HT_LoaiCongViec_LoadAll();
        }
    }
}
