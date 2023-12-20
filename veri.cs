using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace sinemaOtomasyonu
{
    internal class veri
    {
        public static string komut(string sql) {
            string mesaj = "";

            //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\burak\Desktop\sinemaOtomasyonu\veri\sinema.accdb;Persist Security Info=True
            //string currentDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\burak\\Desktop\\sinemaOtomasyonu\\veri\\sinema.accdb");
            OleDbCommand cmd = con.CreateCommand();
            con.Open();
            cmd.CommandText = sql;
            cmd.Connection = con;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)

            {
                mesaj = ex.ToString();
            }
            con.Close();
            return mesaj;
        }
        public static DataTable select(string sql){
            DataTable dt = new DataTable();
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\burak\\Desktop\\sinemaOtomasyonu\\veri\\sinema.accdb");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(sql, con);
            da.Fill(dt);
            con.Close();
            con.Dispose();
            da.Dispose();
            return dt;
        }

    }
}
