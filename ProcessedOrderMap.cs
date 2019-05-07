using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace DataProcessor
{
    class ProcessedOrderMap : ClassMap<ProcessedOrder>
    {
        public ProcessedOrderMap()
        {
            AutoMap();

            Map(m => m.Customer).Name("CustomerNumber");
            Map(m => m.Amount).Name("Quantity").TypeConverter<RomanTypeConverter>();
        }
    }
}

