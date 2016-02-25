using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace UrTQuery
{
    public static class TextOperations
    {
        public static Brush[] Colors =
        {
            Brushes.Black,
            Brushes.Red,
            Brushes.Green,
            Brushes.Yellow,
            Brushes.Blue,
            Brushes.Cyan,
            Brushes.Purple,
            Brushes.White,
            Brushes.Orange,
            Brushes.Olive
        };

        public static Brush ConvertoToBrush(char c)
        {
            var Byte = Encoding.ASCII.GetBytes(c.ToString(CultureInfo.InvariantCulture));
            return Colors[(Byte[0] + 2) % Colors.Count()];
        }

        public static Paragraph ConvertToParagraph(string text)
        {
            var paragraph = new Paragraph();
            var tmp = text.Split('^');

            paragraph.Inlines.Add(new Run(tmp[0])
            {
                Foreground = Brushes.White
            });
            for (var i = 1; i < tmp.Count(); i++)
            {
                if (tmp[i].Length <= 0)
                {
                    var j = 1;
                    while (tmp[i + j].Length <= 0) j++;
                    tmp[i + j] = tmp[i + j].Insert(0, "^");
                    continue;
                }
                paragraph.Inlines.Add(new Run(tmp[i].Substring(1))
                {
                    Foreground = ConvertoToBrush(tmp[i][0])
                });
            }
            return paragraph;
        }

        public static FlowDocument ConvertToFlowDocument(string text)
        {
            var flowDocument = new FlowDocument();
            flowDocument.Blocks.Add(ConvertToParagraph(text));
            return flowDocument;
        }
        public static RichTextBox ConvertToRichTextBox(string text)
        {
            return new RichTextBox { Document = ConvertToFlowDocument(text) };
        }
    }
}
