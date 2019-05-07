using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DataProcessor
{
    internal class CsvFileProcessor
    {
        public string InputFilePath { get; }
        public string OutputFilePath { get; }

        public CsvFileProcessor(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }

        internal void Process()
        {
            using (StreamReader input = File.OpenText(InputFilePath))
            using (CsvReader csvReader = new CsvReader(input))
            {
                IEnumerable<dynamic> records = csvReader.GetRecords<dynamic>();

                csvReader.Configuration.TrimOptions = TrimOptions.Trim;
                csvReader.Configuration.Comment = '@'; // Default is #
                csvReader.Configuration.AllowComments = true;

                foreach (var record in records)
                {
                    Console.WriteLine(record.OrderNumber);
                    Console.WriteLine(record.CustomerNumber);
                    Console.WriteLine(record.Description);
                    Console.WriteLine(record.Quantity);                        
                }
            }
        }
    }
}