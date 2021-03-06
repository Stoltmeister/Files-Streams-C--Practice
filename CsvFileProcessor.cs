﻿using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            using (StreamWriter output = File.CreateText(OutputFilePath))
            using (var csvWriter = new CsvWriter(output))
            {
                IEnumerable<ProcessedOrder> records = csvReader.GetRecords<ProcessedOrder>();

                csvReader.Configuration.TrimOptions = TrimOptions.Trim;
                csvReader.Configuration.Comment = '@'; // Default is #
                csvReader.Configuration.AllowComments = true;
                csvReader.Configuration.RegisterClassMap<ProcessedOrderMap>();

                //csvWriter.WriteRecords(records);

                csvWriter.WriteHeader<ProcessedOrder>();
                csvWriter.NextRecord();

                var recordsArray = records.ToArray();
                for (int i= 0; i < recordsArray.Length; i++)
                {
                    csvWriter.WriteField(recordsArray[i].OrderNumber);
                    csvWriter.WriteField(recordsArray[i].Customer);
                    csvWriter.WriteField(recordsArray[i].Amount);

                    bool isLastRecord = i == recordsArray.Length - 1;

                    if (!isLastRecord)
                    {
                        csvWriter.NextRecord();
                    }
                }

            }
        }
    }
}