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
        /// Returns a random number between 0 and 99
        /// </summary>
        /// <returns></returns>
        public int RollPercentile()
        {
            return rand.Next(0,99);
        }


        /// <summary>
        /// Returns a normally distributed number between -50 and 50
        /// </summary>
        /// <returns></returns>
        public int RollNormal()
        {
            // Box-Muller transform
            double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
            double u2 = rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
           
            return (int) (mean + stdDev * randStdNormal); //random normal(mean,stdDev^2)
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
