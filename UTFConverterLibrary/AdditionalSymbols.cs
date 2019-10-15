using System.Collections.Generic;
using System.Text;

namespace UTFConverterLibrary
{
    class AdditionalSymbols
    {
        List<AdditionalSymbol> AdditionalSymbolsList;

        public AdditionalSymbols()
        {
            AdditionalSymbolsList = new List<AdditionalSymbol>();
        }

        public void Add(AdditionalSymbol a)
        {
            AdditionalSymbolsList.Add(a);
        }

        public AdditionalSymbol GetByCode(int i)
        {
            foreach(AdditionalSymbol a in AdditionalSymbolsList)
            {
                if(a.Code == i)
                {
                    return a;
                }
            }
            return null;
        }
    }
}