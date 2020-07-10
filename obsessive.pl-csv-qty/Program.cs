using CsvHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using obsessive.pl_csv_qty.DAL;

namespace obsessive.pl_csv_qty
{
 class CSV
    {
      
        public string sku { get; set; }
        public string ean { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string qty { get; set; }
    }

    class Program

    {
        public static bool PobierzCSV()
        {


            try
            {
                WebClient downloadWC = new WebClient();
                //   downloadWC.Credentials = new System.Net.NetworkCredential("nattalex@nattalex.pl", "Nattalex");
                byte[] buff = downloadWC.DownloadData("https://obsessive.com/productsexport/stocks.csv");

                using (System.IO.FileStream fs = new FileStream(@"C:\szkolenie\stocks.csv", FileMode.Create))
                {
                    fs.Write(buff, 0, buff.Length);
                    fs.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
                return false;
            }
        }
      

        static void Main(string[] args)
        {
            string path = (@"C:\szkolenie\stocks.csv");
            //  PobierzCSV();
            load aa = new load();
         var ss =   aa.loadCsvFile(path);
            foreach(var gg in ss)
            {
                //string[] columns = gg.Split(';');
                //foreach(var zzss in columns)
                //{
                string ggs = gg;

                //   string qty = zzss[0];

                //}
                string[] col = ggs.Split(';');
                string ean = col[1];
                string qty = col[4];
                string qtyclean = qty.Trim(new Char[] { '"', '\'', '.', 'P', 'o', 'w', 'y', 'ż', 'e','j' });
                string eanclean = ean.Trim(new char[] { '"' });
                int qtyin = Int32.Parse(qtyclean);
                SqlEntity sql = new SqlEntity();
                sql.TWRXML_TWR_EAN = eanclean;
                sql.TWRXML_ILOSC = qtyin;

                sqlmetods ins = new sqlmetods();
                ins.Insert(sql);


            }
            
           
            // for set encoding
            // StreamReader sr = new StreamReader(@"file.csv", Encoding.GetEncoding(1250));
           
        }
    }
}
