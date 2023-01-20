using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace PCCV.Data
{
    public class CV_HT_LoaiCongViecData
    {
        clsKetNoi cls = new clsKetNoi();
        public DataTable LoadCV_HT_LoaiCongViec_LoadAll()
        {
            return cls.LayDuLieu("CV_HT_LoaiCongViec_Select");
        }
    }
}
