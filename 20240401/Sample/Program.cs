string[][] data = args?.FirstOrDefault()?.Split('\n').Select(x => x.Split(',')).ToArray() ?? new string[0][];
data = int.TryParse(args?.Skip(1).FirstOrDefault(), out var index) ? data.OrderBy(x => x?.Skip(index).FirstOrDefault()).ToArray() : data;
Console.WriteLine(string.Join("\n", data.Select(x => string.Join(",", x))));
