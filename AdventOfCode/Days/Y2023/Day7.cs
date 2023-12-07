#region Copyright Nortech Management Ltd. 2023
/*
 All rights are reserved. Reproduction or transmission in whole or in part, in
 any form or by any means, electronic, mechanical or otherwise, is prohibited
 without the prior written consent of the copyright owner.
*/
#endregion

using AdventOfCode.Helper;

namespace AdventOfCode.Days.Y2023
{
    enum Rank 
    { 
        HighCard,
        Pair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    internal class Hand : IComparable<Hand>
    {
        public string Line { get; set; }
        public Rank HandQuality { get; set; }
        public int Bid { get; set; }

        public Hand(string line)
        {

            var parts = line.Trim().Split(" ");
            Line = parts[0].Trim();
            if (Line.Length != 5)
            {
                throw new ArgumentException("Invalid hand size");
            }
            
            Bid = int.Parse(parts[1].Trim());

            if (ContainsNOf(line, 5))
            {

            }
            else if (ContainsNOf(Line, 4))
            {
                HandQuality = Rank.FourOfAKind;
            }
            else if (ContainsNOf(Line, 3) && ContainsNOf(Line, 2))
            {
                HandQuality = Rank.FullHouse;
            }
            else if (ContainsNOf(Line, 3))
            {
                HandQuality = Rank.ThreeOfAKind;
            }
            else if (ContainsTwoPairs(Line))
            {
                HandQuality = Rank.TwoPair;
            }
            else if (ContainsNOf(Line, 2))
            {
                HandQuality = Rank.Pair;
            }
            else
            {
                HandQuality = Rank.HighCard;
            }
        }

        private bool ContainsTwoPairs(string line)
        {
            if (line == "75588")
            {
                Console.WriteLine(line);
            }
            for (int i = 0; i < 5; ++i)
            {
                if (Line.Count(x => x == Line[i]) == 2)
                {
                    var rest = Line.Substring(i + 1);
                    for (int j =  0; j < rest.Length;++j)
                    {
                        if (rest.Count(y => y == rest[j]) == 2)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool ContainsNOf(string line, int count)
        {
            for (int i = 0; i < 5; ++i)
            {
                if (Line.Count(x => x == Line[i]) == count){
                    return true;
                }
            }
            return false;
        }

        public int CompareTo(Hand other)
        {
            // This is a horrible abuse of enums
            if (this.HandQuality < other.HandQuality) 
            {
                return -1;
            }
            else if (this.HandQuality > other.HandQuality)
            {
                return 1;
            }
            else
            {
                List<char> values = new List<char>
                {
                    'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2', '1'
                };
                
                for (int i = 0; i < 5; ++i) 
                {
                    if (values.IndexOf(this.Line[i]) < values.IndexOf(other.Line[i]))
                    {
                        return 1;
                    }
                    else if (values.IndexOf(this.Line[i]) > values.IndexOf(other.Line[i]))
                    {
                        return -1;
                    }
                }
                return 0;
            }
        }
    }

    internal class Day7 : IDay
    {
        public long Run(RunConfig config)
        {

            var data = DataReader.ReadData(this, config);

            var hands = data.Select(x => new Hand(x)).ToList();

            hands.Sort();
            foreach (var h in hands)
            {
                Console.WriteLine(h.Line + "  " + h.HandQuality.ToString());
            }
            int total = 0;
            for (int i = 0; i < hands.Count(); ++i)
            {
                total += hands[i].Bid * (i + 1);
            }
            return total;
        }
    }
}
