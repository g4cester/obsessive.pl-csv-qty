using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace obsessive.pl_csv_qty.DAL
{
   public class load
    {
        public List<string> loadCsvFile(string filePath)
        {
            var reader = new StreamReader(File.OpenRead(filePath));
            List<string> searchList = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                string row = line.Substring(1, 1);
                if (row == "s")
                {

                }
                else
                {
                    searchList.Add(line);
                }
                
           
            }
            return searchList;
        }
    }
}
