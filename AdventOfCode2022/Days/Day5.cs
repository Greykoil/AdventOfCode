using AdventOfCode2022.Helper;

namespace AdventOfCode2022.Days
{
    class Day5 : IDay
    {
        public long Run(RunConfig config)
        {
            
            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);

            var stacks = new List<Stack<char>>();

            stacks.Add(CreateStack(new List<char> { 'H', 'B', 'V', 'W', 'N', 'M', 'L', 'P' }));
            stacks.Add(CreateStack(new List<char> { 'M','Q','H' }));
            stacks.Add(CreateStack(new List<char> { 'N', 'D', 'B', 'G', 'F', 'Q', 'M', 'L' }));
            stacks.Add(CreateStack(new List<char> { 'Z', 'T', 'F', 'Q', 'M', 'W', 'G' }));
            stacks.Add(CreateStack(new List<char> { 'M', 'T', 'H', 'P' }));
            stacks.Add(CreateStack(new List<char> { 'C', 'B', 'M', 'J', 'D', 'H', 'G', 'T' }));
            stacks.Add(CreateStack(new List<char> { 'M', 'N', 'B', 'F', 'V', 'R' }));
            stacks.Add(CreateStack(new List<char> { 'P', 'L', 'H', 'M', 'R', 'G', 'S' }));
            stacks.Add(CreateStack(new List<char> { 'P', 'D', 'B', 'C', 'N' }));


            List<Tuple<int, int, int>> instructions = new List<Tuple<int, int, int>>();

            foreach (string line in data)
            {
                var parts = line.Split(' ');
                instructions.Add(new Tuple<int, int, int>(
                    int.Parse(parts[1]),
                    int.Parse(parts[3]), 
                    int.Parse(parts[5])));
            }

            foreach (var inst in instructions)
            {
                RunInstruction(stacks, inst);
            }

            string ans = "";
            foreach (var stack in stacks)
            {
                ans += stack.Pop();
            }

            return 0;
        }

        private Stack<char> CreateStack(List<char> list)
        {
            var st = new Stack<char>();
                
            foreach (var item in list)
            {
                st.Push(item);
            }
            return st;
        }

        private void RunInstruction(List<Stack<char>> stacks, Tuple<int, int, int> inst)
        {
            Stack<char> tempStack = new Stack<char>();
            for (int i = 0; i < inst.Item1; ++i)
            {
                var item = stacks[inst.Item2 - 1].Pop();
                tempStack.Push(item);
            }

            foreach(var item in tempStack)
            {
                stacks[inst.Item3 - 1].Push(item);
            }
        }
    }
}
