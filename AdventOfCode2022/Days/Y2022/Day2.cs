using AdventOfCode.Helper;

namespace AdventOfCode.Days.Y2022
{

    enum GameValue
    {
        Rock,
        Paper,
        Scissors
    };

    class Day2 : IDay
    {
        public long Run(RunConfig config)
        {
            
            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);

            var rounds = new List<Tuple<GameValue, GameValue>>();
            
            foreach (var line in data)
            {
                GameValue opponent = GameValue.Rock;
                GameValue player = GameValue.Rock;

                switch (line[0])
                {
                    case 'A':
                        opponent = GameValue.Rock;
                        switch (line[2])
                        {
                            case 'X':
                                player = GameValue.Scissors;
                                break;
                            case 'Y':
                                player = GameValue.Rock;
                                break;
                            case 'Z':
                                player = GameValue.Paper;
                                break;
                        }
                        break;
                    case 'B':
                        opponent = GameValue.Paper;
                        switch (line[2])
                        {
                            case 'X':
                                player = GameValue.Rock;
                                break;
                            case 'Y':
                                player = GameValue.Paper;
                                break;
                            case 'Z':
                                player = GameValue.Scissors;
                                break;
                        }
                        break;
                    case 'C':
                        opponent = GameValue.Scissors;
                        switch (line[2])
                        {
                            case 'X':
                                player = GameValue.Paper;
                                break;
                            case 'Y':
                                player = GameValue.Scissors;
                                break;
                            case 'Z':
                                player = GameValue.Rock;
                                break;
                        }
                        break;
                }

               
                rounds.Add(new Tuple<GameValue, GameValue>(opponent, player));
            }

            int score = 0;

            foreach (var round in rounds)
            {
                switch (round.Item2)
                {
                    case GameValue.Rock:
                        score += 1;
                        break;
                    case GameValue.Paper:
                        score += 2;
                        break;
                    case GameValue.Scissors:
                        score += 3;
                        break;
                }
                score += RoundScore(round);

            }

            return score;
        }

        private int RoundScore(Tuple<GameValue, GameValue> round)
        {
            switch (round.Item2)
            {
                case GameValue.Rock:
                    switch (round.Item1)
                    {
                        case GameValue.Rock:
                            return 3;
                        case GameValue.Paper:
                            return 0;
                        case GameValue.Scissors:
                            return 6;
                    }
                    break;
                case GameValue.Paper:
                    switch (round.Item1)
                    {
                        case GameValue.Rock:
                            return 6;
                        case GameValue.Paper:
                            return 3;
                        case GameValue.Scissors:
                            return 0;
                    }
                    break;
                case GameValue.Scissors:
                    switch (round.Item1)
                    {
                        case GameValue.Rock:
                            return 0;
                        case GameValue.Paper:
                            return 6;
                        case GameValue.Scissors:
                            return 3;
                    }
                    break;
            }
            throw new ArgumentException();
        }
    }
}
