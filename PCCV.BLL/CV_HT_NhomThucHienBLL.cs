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
    public class CV_HT_NhomThucHienBLL
    {
        CV_HT_NhomThucHienData cls = new CV_HT_NhomThucHienData();
         public DataTable LoadCV_HT_NhomThucHien_LoadAll()
        {
            return cls.LoadCV_HT_NhomThucHien_LoadAll();
         }
         public int CV_HT_NhomThucHien_Del(CV_HT_NhomThucHienPublic Public)
         {
             return cls.CV_HT_NhomThucHien_Del(Public);
         }
         public int CV_HT_NhomThucHien_Add(CV_HT_NhomThucHienPublic Public)
         {
             return cls.CV_HT_NhomThucHien_Add(Public);
         }
         public int CV_HT_NhomThucHien_Edit(CV_HT_NhomThucHienPublic Public)
         {
             return cls.CV_HT_NhomThucHien_Edit(Public);
         }
    }
}
