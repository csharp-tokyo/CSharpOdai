using (System.Data.DataTable dt = new())
using (System.Data.DataColumn col = new("a") { Expression = args.FirstOrDefault() })
{
    dt.Columns.Add(col); dt.Rows.Add(dt.NewRow());
    Console.WriteLine(dt.Rows[0][0]);
}
