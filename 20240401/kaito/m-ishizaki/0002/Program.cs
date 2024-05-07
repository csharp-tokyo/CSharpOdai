Console.WriteLine(RkSoftware.CsvParser.CsvWriter.Write(RkSoftware.CsvParser.CsvReader.Read(args?.FirstOrDefault() ?? "").OrderBy(x => int.TryParse(args?.Skip(1).FirstOrDefault(), out var index) ? x.Skip(index).FirstOrDefault() : null).ToArray()));

