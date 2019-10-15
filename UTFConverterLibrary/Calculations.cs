using System;
using System.Collections.Generic;
using System.Text;

namespace UTFConverterLibrary
{
    class Calculations
    {
        public int Calculate(int c)
        {
            if(c <= 127)
            {
                return c;
            }

            return 0;
        }
    }
}
