using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Picture_Editor
{
    class ToolsFactory
    {
        static public Double DMStoDegrees(Double[] dmsValues)
        {
            Double degrees;
            Double minutes;
            Double seconds;
            Double decimalDegrees;

            degrees = dmsValues[0];
            minutes = dmsValues[1];
            seconds = dmsValues[2];

            decimalDegrees = degrees + minutes / 60 + seconds / 3600;

            return decimalDegrees;

        }
    }
}
