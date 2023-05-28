using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mod_lab10_ufo
{
    internal class ImplementFunc
    {
        public static int factorial(int x)
        {
            if (x == 0)
                return 1;
            else
                return x * factorial(x - 1);
        }

        public static double Sin(int accuracy, double x)
        {
            double sin = 0.0;
            for (int n = 1; n <= accuracy; n++)
            {
                sin = sin + Math.Pow(-1, n - 1) * Math.Pow(x, 2 * n - 1) / factorial(2 * n - 1);
            }
            return sin;
        }

        public static double Cos(int accuracy, double x)
        {
            double cos = 0.0;
            for (int n = 1; n <= accuracy; n++)
            {
                cos = cos + Math.Pow(-1, n - 1) * Math.Pow(x, 2 * n - 2) / factorial(2 * n - 2);
            }
            return cos;
        }

        public static double Arctg(int accuracy, double x)
        {
            double arctg = 0;

            if (-1 <= x && x <= 1)
            {
                for (int n = 1; n <= accuracy; n++)
                {
                    arctg = arctg + Math.Pow(-1, n - 1) * Math.Pow(x, 2 * n - 1) / (2 * n - 1);
                }
            }
            else
            {
                if (x > 1)
                {
                    arctg = arctg + Math.PI / 2;
                    for (int n = 0; n < accuracy; n++)
                    {
                        arctg = arctg - Math.Pow(-1, n) / ((2 * n + 1) * Math.Pow(x, 2 * n + 1));
                    }
                }
                else
                {
                    arctg = arctg - Math.PI / 2;
                    for (int n = 0; n < accuracy; n++)
                    {
                        arctg = arctg - Math.Pow(-1, n) / ((2 * n + 1) * Math.Pow(x, 2 * n + 1));
                    }
                }
            }
            return arctg;
        }
    }
}
