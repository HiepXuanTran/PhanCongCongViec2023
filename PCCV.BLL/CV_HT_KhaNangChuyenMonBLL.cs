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
    public class CV_HT_KhaNangChuyenMonBLL
    {
        CV_HT_KhaNangChuyenMonData cls = new CV_HT_KhaNangChuyenMonData();
        public DataTable LoadCV_HT_KhaNangChuyenMon_LoadAll()
        {
            return cls.LoadCV_HT_KhaNangChuyenMon_LoadAll();
        }
        public int CV_HT_KhaNangChuyenMon_Del(CV_HT_KhaNangChuyenMonPublic Public)
        {
            return cls.CV_HT_KhaNangChuyenMon_Del(Public);
        }
        public int CV_HT_KhaNangChuyenMon_Add(CV_HT_KhaNangChuyenMonPublic Public)
        {
            return cls.CV_HT_KhaNangChuyenMon_Add(Public);
        }
        public int CV_HT_KhaNangChuyenMon_Update(CV_HT_KhaNangChuyenMonPublic Public)
        {
            return cls.CV_HT_KhaNangChuyenMon_Update(Public);
        }
    }
}
