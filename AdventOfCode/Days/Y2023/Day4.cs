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
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days.Y2023
{

    internal class ScratchCard 
    {
        public int Number { get; set; }
        public List<int> WinningNums { get; set; } = new List<int>();
        
        public ScratchCard(string line) 
        {
            var parts = line.Trim().Split(":");
            var foo = parts[0].Substring(5, parts[0].Length - 5);
            Number = int.Parse(foo);// parts[0].Substring(4, parts[0].Length - 5));

            var numberParts = parts[1].Split("|");
            var winningNums = numberParts[0].Split(" ").ToList().Where(x => x != "");
            var actualNums = numberParts[1].Split(" ").ToList().Where(x => x != "");

            var matching = actualNums.Where(x => winningNums.Contains(x));
            for (int i = 0; i < matching.Count(); ++i)
            {
                WinningNums.Add(Number + 1 + i);
            }
        }

        public int Score(List<ScratchCard> cards)
        {
            return 1 + WinningNums.Sum(x => cards[x - 1].Score(cards));
        }
    }


    internal class Day4 : IDay
    {
        public long Run(RunConfig config)
        {
            var data = DataReader.ReadData(this, config);
            var cards = data.Select(x => new ScratchCard(x)).ToList();
            int total = 0;
            foreach (var card in cards)
            {
                total += card.Score(cards);
            }
            return total;
        }
    }
}
