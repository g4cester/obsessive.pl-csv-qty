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

              PobierzCSV();
           

            load file = new load();
         var items =   file.loadCsvFile(path);
            foreach(var item in items)
            {

                string line = item;

             

                
                string[] col = line.Split(';');
                string ean = col[1];
                string qty = col[4];
                string qtyclean = qty.Trim(new Char[] { '"', '\'', '.', 'P', 'o', 'w', 'y', 'ż', 'e','j' });
                string eanclean = ean.Trim(new char[] { '"' });
                int qtyint = Int32.Parse(qtyclean);
                SqlEntity sql = new SqlEntity();
                sql.TWRXML_TWR_EAN = eanclean;
                sql.TWRXML_ILOSC = qtyint;

                sqlmetods check = new sqlmetods();
                var twrids = check.CheckTwrID(eanclean);
                if (twrids.Count==0)
                {

                    sqlmetods ins = new sqlmetods();
                    ins.Insert(sql);
                }
                else
                {
                    foreach(var twrid in twrids)
                    {
                        sqlmetods up = new sqlmetods();
                        up.UpdateQty(twrid.TWRXML_ID, qtyint);
                    }
                   
                }




            }
            
           
           
        }
    }
}
