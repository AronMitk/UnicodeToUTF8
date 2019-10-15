using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace UTFConverterLibrary
{
    class FileProcessor
    {
        public void ProcessFile(string input, string output, Converter converter)
        {
            Console.OutputEncoding = Encoding.UTF8;

            EncodingProvider encPr = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(encPr);

            using (StreamReader iFile = new StreamReader(input, Encoding.GetEncoding("windows-1252")))
            {
                FileStream stream = new FileStream(output, FileMode.Create);
                using (StreamWriter oFile = new StreamWriter(stream))
                    while (!iFile.EndOfStream)
                    {
                        //Gauname simbolio desimtaine israiska
                        int c = iFile.Read();
                       // Console.WriteLine(c);
                       
                        //Gauname konvertuota simboli
                        Byte[] bytes = converter.ConvertSymbol(c);

                        //Isvedame
                        oFile.Write(Encoding.UTF8.GetString(bytes));
                        
                        //Console.Write(Encoding.UTF8.GetString(bytes));
                    }
            }
        }

        public AdditionalSymbols ProcessAdditionalSymbolsFile(string input)
        {
            AdditionalSymbols additionalSymbols = new AdditionalSymbols();

            using (StreamReader iFile = new StreamReader(input))
            {
                while (!iFile.EndOfStream)
                {
                    string line = iFile.ReadLine();
                    string[] values = line.Split(new char[] { ';' });

                    if (values[0] == "")
                    {
                        char ch = '\0';
                        int code = int.Parse(values[1]);
                        additionalSymbols.Add(new AdditionalSymbol(ch, code));
                    }
                    else
                    {
                        char ch = char.Parse(values[0]);
                        int code = int.Parse(values[1]);
                        
                        additionalSymbols.Add(new AdditionalSymbol(ch, code));
                    }
                    
                }
            }

            return additionalSymbols;
        }
    }
}
