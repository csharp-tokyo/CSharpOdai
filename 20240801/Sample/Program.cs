string[] d = args[0].Split('-');
string r = string.Join("/", (string[])[d[2], d[0], d[1]]);
Console.WriteLine(r);
