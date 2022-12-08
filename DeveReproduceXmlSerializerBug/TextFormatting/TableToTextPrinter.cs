using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeveReproduceXmlSerializerBug.TextFormatting
{
    public static class TableToTextPrinter
    {
        public static string TableToText(List<List<string>> content)
        {
            var longestColumns = content.Where(t => t != null).Max(t => t.Count);
            var longestPerColumn = Enumerable.Repeat(0, longestColumns).ToList();
            for (var rowNumber = 0; rowNumber < content.Count; rowNumber++)
            {
                var row = content[rowNumber];
                if (row != null)
                {
                    for (var columnNumber = 0; columnNumber < longestColumns; columnNumber++)
                    {
                        if (columnNumber < row.Count)
                        {
                            var item = row[columnNumber]?.Length;
                            if (longestPerColumn[columnNumber] < item)
                            {
                                longestPerColumn[columnNumber] = item.HasValue ? item.Value : 0;
                            }
                        }
                    }
                }
            }

            var totalTableWidth = longestPerColumn.Sum() + longestColumns * 3 + 1;

            var sb = new StringBuilder();

            for (var rowNumber = 0; rowNumber < content.Count; rowNumber++)
            {
                var row = content[rowNumber];
                if (row != null)
                {
                    var lastToWriteWasEmpty = true;
                    var hasBeenNonEmpty = false;
                    for (var columnNumber = 0; columnNumber < longestColumns; columnNumber++)
                    {
                        var item = string.Empty; ;
                        if (columnNumber < row.Count)
                        {
                            item = row[columnNumber];
                            if (item == null)
                            {
                                //If a null value is provided, make sure we set it back to ""
                                item = string.Empty;
                            }
                        }

                        var toWrite = PadBoth(item, longestPerColumn[columnNumber]);

                        if (hasBeenNonEmpty || (lastToWriteWasEmpty || !string.IsNullOrEmpty(item)))
                        {
                            sb.Append('|');
                        }
                        else
                        {
                            sb.Append(' ');
                        }
                        sb.Append($" {toWrite} ");
                        if (hasBeenNonEmpty == false)
                        {
                            if (lastToWriteWasEmpty == true && !string.IsNullOrEmpty(item))
                            {
                                hasBeenNonEmpty = true;
                            }
                            lastToWriteWasEmpty = !string.IsNullOrEmpty(item);

                        }
                    }

                    sb.Append("|");
                    sb.AppendLine();
                }
                else
                {
                    sb.AppendLine(string.Empty.PadRight(totalTableWidth, '-'));
                }
            }

            return sb.ToString();
        }

        private static string PadBoth(string source, int length)
        {
            var spaces = length - source.Length;
            var padLeft = spaces / 2 + source.Length;
            return source.PadLeft(padLeft).PadRight(length);
        }
    }
}
