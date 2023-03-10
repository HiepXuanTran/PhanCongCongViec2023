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
    public class CV_HT_LoaiCongViecBLL
    {
        CV_HT_LoaiCongViecData cls = new CV_HT_LoaiCongViecData();
        public DataTable LoadCV_HT_LoaiCongViec_LoadAll()
        {
            return cls.LoadCV_HT_LoaiCongViec_LoadAll();
        }
        public int CV_HT_LoaiCongViec_Add(CV_HT_LoaiCongViecPublic Public)
        {
            return cls.CV_HT_LoaiCongViec_Add(Public);
        }
        public int CV_HT_LoaiCongViec_Update(CV_HT_LoaiCongViecPublic Public)
        {
            return cls.CV_HT_LoaiCongViec_Update(Public);
        }
        public int CV_HT_LoaiCongViec_Del(CV_HT_LoaiCongViecPublic Public)
        {
            return cls.CV_HT_LoaiCongViec_Del(Public);
        }
    }
}
