// データの読み込みと加工
var (header, data) = ReadAndOrder(args.FirstOrDefault() ?? string.Empty, args.Skip(1).FirstOrDefault() ?? string.Empty);

// 結果の出力 ヘッダー行とデータ行
Console.WriteLine(string.Join(",", header.Select(Escape)));
Console.WriteLine(string.Join("\n", data.Select(line => string.Join(",", line.Select(Escape)))));


////
// == 以降メソッド ==
////

// データの読み込みと加工メソッド
static (string[], string[][]) ReadAndOrder(string csv, string orderCellName)
{
    // データの読み込みとヘッダー行の抽出
    var headerAndData = Read(csv).ToArray();
    var header = headerAndData.FirstOrDefault() ?? Array.Empty<string>();

    // ソートする列のインデックスを産出
    var orderCellIndex = header.Index().Where(x => string.Compare(x.Item, orderCellName) == 0).Select(x => x.Index + 1).FirstOrDefault() - 1;

    // ソートしつつデータ部の抽出
    var data = headerAndData.Skip(1).OrderBy(x => (-1 < orderCellIndex && orderCellIndex < x.Length) ? x[orderCellIndex] : "").ToArray();

    return (header, data);
}

// 読み込みメソッド CsvHelper 使用
static IEnumerable<string[]> Read(string csv)
{
    using var sr = new StringReader(csv);
    using var helper = new CsvHelper.CsvReader(sr, System.Globalization.CultureInfo.CurrentCulture);
    while (helper.Read())
        yield return Enumerable.Range(0, helper.Parser.Count).Select(i => helper.Parser[i]).ToArray();
}

// 結果出力時のエスケープ
static string Escape(string cell) =>
    (cell.Contains('"') || cell.Contains('\'') || cell.Contains(',') || cell.Contains('\n'))
    ? $"\"{cell.Replace("\"", "\"\"")}\""
    : cell;
