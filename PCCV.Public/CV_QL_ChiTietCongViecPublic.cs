using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uneti.Public
{
    public class CV_QL_ChiTietCongViecPublic
    {
        int _Id;

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        int _CV_QL_ChiTietCongViec_IDCongViec;

        public int CV_QL_ChiTietCongViec_IDCongViec
        {
            get { return _CV_QL_ChiTietCongViec_IDCongViec; }
            set { _CV_QL_ChiTietCongViec_IDCongViec = value; }
        }

        string _CacBuocCongViec;

        public string CacBuocCongViec
        {
            get { return _CacBuocCongViec; }
            set { _CacBuocCongViec = value; }
        }
        string _MoTaBuocCongViec;

        public string MoTaBuocCongViec
        {
            get { return _MoTaBuocCongViec; }
            set { _MoTaBuocCongViec = value; }
        }
        string _MucDoKho;

        public string MucDoKho
        {
            get { return _MucDoKho; }
            set { _MucDoKho = value; }
        }
        string _TenFile;

        public string TenFile
        {
            get { return _TenFile; }
            set { _TenFile = value; }
        }
        byte[] _FileDinhKem;

        public byte[] FileDinhKem
        {
            get { return _FileDinhKem; }
            set { _FileDinhKem = value; }
        }
       
        double _SoPhutThucHien;

        public double SoPhutThucHien
        {
            get { return _SoPhutThucHien; }
            set { _SoPhutThucHien = value; }
        }
        double _SoGioThucHien;

        public double SoGioThucHien
        {
            get { return _SoGioThucHien; }
            set { _SoGioThucHien = value; }
        }
        double _SoNgayThucHien;

        public double SoNgayThucHien
        {
            get { return _SoNgayThucHien; }
            set { _SoNgayThucHien = value; }
        }
        int _HT_USER_Create;

        public int HT_USER_Create
        {
            get { return _HT_USER_Create; }
            set { _HT_USER_Create = value; }
        }
        DateTime _CV_QL_ChiTietCongViec_DateCreate;

        public DateTime CV_QL_ChiTietCongViec_DateCreate
        {
            get { return _CV_QL_ChiTietCongViec_DateCreate; }
            set { _CV_QL_ChiTietCongViec_DateCreate = value; }
        }
        int _HT_USER_Editor;

        public int HT_USER_Editor
        {
            get { return _HT_USER_Editor; }
            set { _HT_USER_Editor = value; }
        }
        DateTime _CV_QL_ChiTietCongViec_DateEditor;

        public DateTime CV_QL_ChiTietCongViec_DateEditor
        {
            get { return _CV_QL_ChiTietCongViec_DateEditor; }
            set { _CV_QL_ChiTietCongViec_DateEditor = value; }
        }
        string _CV_QL_ChiTietCongViec_SuDung;

        public string CV_QL_ChiTietCongViec_SuDung
        {
            get { return _CV_QL_ChiTietCongViec_SuDung; }
            set { _CV_QL_ChiTietCongViec_SuDung = value; }
        }
        bool _CV_QL_ChiTietCongViec_HienThi;

        public bool CV_QL_ChiTietCongViec_HienThi
        {
            get { return _CV_QL_ChiTietCongViec_HienThi; }
            set { _CV_QL_ChiTietCongViec_HienThi = value; }
        }
    }
}
