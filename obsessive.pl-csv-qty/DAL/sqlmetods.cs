using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using obsessive.pl_csv_qty.DAL;

namespace obsessive.pl_csv_qty.DAL
{

    public class sqlmetods
    {
        public virtual bool Insert(SqlEntity e)
        {
            CDN_demoEntities ent = new CDN_demoEntities();

            var plcRecord = ent.Set<ZZSPE_TWRXML_OBS>();

            plcRecord.Add(new ZZSPE_TWRXML_OBS() { TWRXML_TWR_EAN = e.TWRXML_TWR_EAN, TWRXML_ILOSC = e.TWRXML_ILOSC });
            // var plcRec = ent.Set<ZZ_TO_PLC>();
            //....
            //  plcRec.Add(newf ZZ_TO_PLC() { MSG_ID = 2});

            return ent.SaveChanges() > 0;

        }
        public virtual bool UpdateQty(int id, int ilosc)
        {
            CDN_demoEntities ent = new CDN_demoEntities();
            ZZSPE_TWRXML_OBS plcRecord = ent.ZZSPE_TWRXML_OBS.Where(e => e.TWRXML_ID == id).First();
            plcRecord.TWRXML_ILOSC = ilosc;

            return ent.SaveChanges() > 0;


        }

        public virtual List<SqlEntity> CheckTwrID(string twr_ean)

        {
            CDN_demoEntities ent = new CDN_demoEntities();
            List<SqlEntity> outcome = new List<SqlEntity>();
            List<ZZSPE_TWRXML_OBS> result = ent.ZZSPE_TWRXML_OBS.Where(e => e.TWRXML_TWR_EAN == twr_ean).ToList();
            foreach (ZZSPE_TWRXML_OBS item in result)
            {
                SqlEntity sqte = new SqlEntity();
                sqte.TWRXML_ID = item.TWRXML_ID;
                sqte.TWRXML_TWR_EAN = item.TWRXML_TWR_EAN;
                sqte.TWRXML_ILOSC = item.TWRXML_ILOSC.Value;

                outcome.Add(sqte);

            }

            return outcome;




        }
    }
}
