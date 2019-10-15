using System;
using System.Collections.Generic;
using System.Text;

namespace UTFConverterLibrary
{
    class AdditionalSymbol
    {
        public char Symbol { get; set; }
        public int Code { get; set; }

        public AdditionalSymbol()
        {

        }

        public AdditionalSymbol(char symbol, int code)
        {
            Symbol = symbol;
            Code = code;
        }
    }
}
