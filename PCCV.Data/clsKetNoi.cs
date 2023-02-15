using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace PCCV.Data
{
    class clsKetNoi
    {
        SqlConnection con = new SqlConnection();
        string ketnoi = ConfigurationManager.ConnectionStrings["project"].ConnectionString;
        public bool ClsKetNoi_CSDL_KiemTra()
        {
            con.ConnectionString = ketnoi; //ketnoi;
            {
                try
                {
                    //con.Open();
                    return true;
                }
                catch
                //catch (SqlException)
                {
                    return false;
                }
            }
        }

        public clsKetNoi()
        {

            if (IsServerConnected() == false)
            {
                Close();
            }

        }
        public void Close()
        {
            con.Dispose();
        }
        public bool IsServerConnected()
        {
            con.ConnectionString = ketnoi + ";pooling=true;Max Pool Size=9999;MultipleActiveResultSets=True"; //ketnoi;
            {
                try
                {
                    con.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
        // lay du lieu reader
        public SqlDataReader LayDuLieu_R(string sql, string[] name, object[] value, int Nparameter)
        {
            SqlCommand command = new SqlCommand(sql, con);
            command.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < Nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            command.Dispose();
            return command.ExecuteReader();
        }

        // lay du lieu khong tham so 
        public DataTable LayDuLieu(string sql)
        {
            SqlCommand command = new SqlCommand(sql, con);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 0;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            command.Dispose();
            adapter.Dispose();
            dt.Dispose();
            return dt;
        }
        // laasy du lieu co tham so
        public DataTable LayDuLieu(string sql, string[] name, object[] value, int Nparameter)
        {
            SqlCommand command = new SqlCommand(sql, con);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 0;
            for (int i = 0; i < Nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            command.Dispose();
            adapter.Dispose();
            dt.Dispose();
            return dt;
        }
        // thay doi du lieu khong tham so
        public int Update(string sql)
        {
            SqlCommand command = new SqlCommand(sql, con);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 0;
            command.Dispose();
            return command.ExecuteNonQuery();
        }
        // thay doi du lieu co tham so
        public int Update(string sql, string[] name, object[] value, int Nparameter)
        {
            SqlCommand command = new SqlCommand(sql, con);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 0;
            for (int i = 0; i < Nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            command.Dispose();
            return command.ExecuteNonQuery();
        }
        // het 
    }
}

