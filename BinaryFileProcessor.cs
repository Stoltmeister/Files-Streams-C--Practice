﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor
{
    class BinaryFileProcessor
    {
        public string InputFilePath { get; }
        public string OutputFilePath { get; }

        public BinaryFileProcessor(string inputFilePath, string outputFilePath)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }

        public void Process()
        {
            using (FileStream inputFileStream = File.Open(InputFilePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader binaryStreamReader = new BinaryReader(inputFileStream))
            using (FileStream outputFileStream = File.Create(OutputFilePath))
            using (BinaryWriter binaryStreamWriter = new BinaryWriter(outputFileStream))
            {
                int largest = 0;

                while (binaryStreamReader.BaseStream.Position < binaryStreamReader.BaseStream.Length)
                {
                    int currentByte = binaryStreamReader.ReadByte();

                    binaryStreamWriter.Write(currentByte);

                    if (currentByte > largest)
                    {
                        largest = currentByte;
                    }
                }

                binaryStreamWriter.Write(largest);
            }
        }

    }
}
