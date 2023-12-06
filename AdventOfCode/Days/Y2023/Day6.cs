#region Copyright Nortech Management Ltd. 2023
/*
 All rights are reserved. Reproduction or transmission in whole or in part, in
 any form or by any means, electronic, mechanical or otherwise, is prohibited
 without the prior written consent of the copyright owner.
*/
#endregion

using AdventOfCode.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Y2023
{
    internal class Day6 : IDay
    {
        public long Run(RunConfig config)
        {
            List<Tuple<int, long>> values = null;

            if (config.UseSimpleData)
            {
                values = new List<Tuple<int, long>>()
                {
                    new Tuple<int, long> (71530, 940200)
                };
            } 
            else
            {
                values = new List<Tuple<int, long>>()
                {
                    new Tuple<int, long> (59796575, 597123410321328)
                };
            }

            List<long> raceWinners = new List<long>();
            foreach (var race in values)
            {
                long raceOptions = 0;
                for (long i = 0; i <= race.Item1; ++i)
                {
                    if ((i * (race.Item1 - i)) > race.Item2)
                    {
                        ++raceOptions;
                    }
                }
                raceWinners.Add(raceOptions);
            }

            long total = 1;
            foreach (var item in raceWinners)
            {
                total *= item;
            }
            return total;
        }
    }
}
