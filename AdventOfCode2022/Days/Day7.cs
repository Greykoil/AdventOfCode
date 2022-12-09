using AdventOfCode2022.Helper;

namespace AdventOfCode2022.Days
{

    class AocFile
    {
        public string Name { get; set; }
        public long Size { get; set; }
    }

    class Directory
    {
        public string Name { get; set; }

        public Directory Parent { get; set; }

        public List<Directory> Subdirectories { get; set; } = new List<Directory>();

        public List<AocFile> Files { get; set; } = new List<AocFile>();

        public long Size()
        {
            long size = Subdirectories.Sum(x => x.Size()) + Files.Sum(x => x.Size);

            return size;
        }
    }

    class Day7 : IDay
    {
        public long Run(RunConfig config)
        {
            
            Console.WriteLine($"Start running {GetType()}");

            IEnumerable<string> data = DataReader.ReadData(this, config);

            Directory parent = GenerateFileStructure(data);

            long smallestDelete = long.MaxValue;
            var current = parent;

            long totalSpace = 70000000;
            long requiredSpace = 30000000;

            long availableSpace = totalSpace - parent.Size();
            long spaceToFree = requiredSpace - availableSpace;

            FineSmallestGreaterThan(parent, spaceToFree, ref smallestDelete);

            return smallestDelete;
        }

        private void FineSmallestGreaterThan(Directory current, long spaceToFree, ref long smallestDelete)
        {
            if (current.Size() >= spaceToFree && current.Size() < smallestDelete)
            {
                smallestDelete = current.Size();
            }

            foreach (var item in current.Subdirectories)
            {
                FineSmallestGreaterThan(item, spaceToFree, ref smallestDelete);
            }
        }

        private long DirSizeCheck(Directory current)
        {
            long size = 0;
            if (current.Size() < 100000)
            {
                size += current.Size();
            }

            foreach (var item in current.Subdirectories)
            {
                size += DirSizeCheck(item);
            }
            return size;
        }


        private Directory GenerateFileStructure(IEnumerable<string> data)
        {
            Directory topLevel = new Directory()
            {
                Name = "/",
                Parent = null
            };


            Directory current = topLevel;
            foreach (string line in data)
            {
                if (line.StartsWith("$ cd"))
                {
                    string dir = line.Split()[2];
                    if (dir == "/")
                    {
                        current = topLevel;
                    }
                    else if (dir == "..")
                    {
                        current = current.Parent;
                    }
                    else if (current.Subdirectories.Any(x => x.Name == dir))
                    {
                        current = current.Subdirectories.First(x => x.Name == dir);
                    }
                    else
                    {
                        var newDir = new Directory()
                        {
                            Name = dir,
                            Parent = current
                        };
                        current = newDir;
                    }
                }
                else if (line.StartsWith("$ ls"))
                {
                    // Don't need to do anything in this case
                }
                else if (line.StartsWith("dir"))
                {
                    string dirName = line.Split()[1];
                    if (!current.Subdirectories.Any(x => x.Name == dirName))
                    {
                        current.Subdirectories.Add(new Directory()
                        {
                            Name = dirName,
                            Parent = current
                        });
                    }
                }

                else
                {
                    var parts = line.Split();
                    long length = long.Parse(parts[0]);
                    string name = parts[1];
                    if (!current.Files.Any(x => x.Name == name))
                    {
                        current.Files.Add(new AocFile
                        {
                            Name = name,
                            Size = length
                        });
                    }
                }
            }


            return topLevel;
        }
    }
}
