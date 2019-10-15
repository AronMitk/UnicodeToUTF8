using System;
using System.Collections.Generic;
using System.Text;

namespace UTFConverterLibrary
{
    public class Converter
    {
        AdditionalSymbols additionalSymbols = null;

        #region MainFunctions
        public void ConvertFile(string input, string output)
        {
            FileProcessor fileProcessor = new FileProcessor();
            fileProcessor.ProcessFile(input, output, this);
        }

        public void AddAdditionalSymbols(string input)
        {
            if(input != null)
            {
                FileProcessor fileProcessor = new FileProcessor();
                additionalSymbols = fileProcessor.ProcessAdditionalSymbolsFile(input);
            }
        }
        #endregion


        public Byte[] ConvertSymbol(int symbol)
        {
           
            symbol = GetConvertation(additionalSymbols != null, symbol);

            //Gauname Unicode reiksme
            string value = symbol.ToString("X");

            string[] values = GetConvertedBinaryValues(GetBinaryValue(value), GetBytesNumber(value));

            values = SplitBytesIntoChunks(values);

            Byte[] bytes = ConvertToBytes(values);

            return bytes;
        }

        #region
        static string GetBinaryValue(string value)
        {
            //Convert Hex into binary code
            string binaryval = Convert.ToString(Convert.ToInt32(value, 16), 2);
            return binaryval;
        }

        static int GetBytesNumber(string value)
        {
            //Get bytes number
            int a = int.Parse(value, System.Globalization.NumberStyles.HexNumber);

            if (0 <= a && a <= 127) return 1;
            else if (80 <= a && a <= 2047) return 2;
            else if (2048 <= a && a <= 65535) return 3;
            else if (65536 <= a && a <= 1114111) return 4;

            return -1;
        }
        #endregion

        static string[] SplitBytesIntoChunks(string[] values)
        {
            string[] output = new string[values.Length * 2];
            int i = 0;
            foreach (string value in values)
            {
                output[i] = value.Substring(0, 4);
                i++;
                output[i] = value.Substring(4, 4);
                i++;
            }
            return output;
        }

        static string[] GetConvertedBinaryValues(string value, int k)
        {
            if (k != -1)
            {
                //Console.WriteLine("VVV: " + value + " K: " + k);

                string[] output = new string[k];

                int valLength = value.Length - 1;

                for (int n = k - 1; 0 <= n; n--)
                {
                    char[] symbols = new char[8];
                    if (n != 0)
                    {
                        symbols[0] = '1';
                        symbols[1] = '0';
                    }
                    else if (n == 0 && k == 1)
                    {
                        symbols[0] = '0';
                    }
                    else if (n == 0 && k == 2)
                    {
                        symbols[0] = '1';
                        symbols[1] = '1';
                        symbols[2] = '0';
                    }
                    else if (n == 0 && k == 3)
                    {
                        symbols[0] = '1';
                        symbols[1] = '1';
                        symbols[2] = '1';
                        symbols[3] = '0';
                    }
                    else if (n == 0 && k == 4)
                    {
                        symbols[0] = '1';
                        symbols[1] = '1';
                        symbols[2] = '1';
                        symbols[3] = '1';
                        symbols[4] = '0';
                    }

                    for (int m = symbols.Length - 1; 0 <= m; m--)
                    {
                        if ((symbols[m] != '0' && symbols[m] != '1') && valLength >= 0)
                        {
                            symbols[m] = value[valLength];
                            valLength--;
                        }
                        else if ((symbols[m] != '0' && symbols[m] != '1') && valLength < 0)
                        {
                            symbols[m] = '0';
                        }
                        else if ((symbols[m] == '0' || symbols[m] == '1') && valLength >= 0)
                        {
                        }
                    }
                    output[n] = new string(symbols);
                }

                return output;
            }
            else return null;
        }

        static Byte[] ConvertToBytes(string[] values)
        {
            Byte[] bytes = new Byte[values.Length / 2];
            string hexValue = "";
            for (int i = 0; i < values.Length; i++)
            {
                hexValue = hexValue + BinaryToHex(values[i]);
                if (i % 2 == 1)
                {
                    bytes[i / 2] = Convert.ToByte(hexValue, 16);
                    hexValue = "";
                }

            }

            return bytes;
        }

        static string BinaryToHex(string value)
        {
            return Convert.ToInt32(value, 2).ToString("X");
        }

        int GetConvertation(bool exists, int symbol)
        {
            if (0 <= symbol && symbol <= 127)
            {
                return symbol;
            }
            else if(exists)
            {
                AdditionalSymbol additional = additionalSymbols.GetByCode(symbol);
                if (additional != null)
                {
                    symbol = (int)additional.Symbol;
                }
            }

            return symbol;
        }
    }
}
