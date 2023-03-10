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
    public class CV_HT_MucDoKhoBLL
    {
        CV_HT_MucDoKhoData cls = new CV_HT_MucDoKhoData();
        public DataTable LoadCV_HT_MucDoKho_LoadAll()
        {
            return cls.LoadCV_HT_MucDoKho_LoadAll();
        }
        public int CV_HT_MucDoKho_Del(CV_HT_MucDoKhoPublic Public)
        {
            return cls.CV_HT_MucDoKho_Del(Public);
        }
        public int CV_HT_MucDoKho_Add(CV_HT_MucDoKhoPublic Public)
        {
            return cls.CV_HT_MucDoKho_Add(Public);
        }
        public int CV_HT_MucDoKho_Update(CV_HT_MucDoKhoPublic Public)
        {
            return cls.CV_HT_MucDoKho_Update(Public);
        }

    }
}
