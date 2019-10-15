using System;
using System.IO;
using System.Text;
using UTFConverterLibrary;
namespace UTFConverter_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Converter converter = new Converter();
            converter.AddAdditionalSymbols("codepage457.txt");
            converter.ConvertFile(@"C:\Users\Arnas\Downloads\wetransfer-617773\UTF LAB\386intel.txt", "file.txt");
        }
    }
}
