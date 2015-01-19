using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Little_Stat
{
    class RollGenerator
    {
        /// <summary>
        /// Returns a normally distributed number around the input with a max of arounnd 25%
        /// </summary>
        /// <param name="INPUT">Input to be offset</param>
        /// <returns></returns>
        public float Roll(float INPUT)
        {
            // Box-Muller transform
            double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            
            double result = (mean + stdDev * randStdNormal); //random normal(mean,stdDev^2)

            return (float) (INPUT + ((INPUT / 100) * result));
        }

        /*
         * No more methods here
         */

        /*
         * Local variable declarations
         */
        double mean = 0.0;
        double stdDev = 10;
        Random rand = new Random();

    }
}
