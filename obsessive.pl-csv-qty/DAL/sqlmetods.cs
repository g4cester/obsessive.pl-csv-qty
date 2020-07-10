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


    }

}
