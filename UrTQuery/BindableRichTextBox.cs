using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace UrTQuery
{
    public class BindableRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register("Document", typeof(FlowDocument), typeof(BindableRichTextBox), new FrameworkPropertyMetadata (null, OnDocumentChanged));

        public new FlowDocument Document
        {
            get
            {
                return (FlowDocument)GetValue(DocumentProperty);
            }

            set
            {
                SetValue(DocumentProperty, value);
                value.PageWidth = Width;
            }
        }

        public static void OnDocumentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var rtb = (RichTextBox)obj;
            //((FlowDocument)(args.NewValue)).
            rtb.Document = (FlowDocument)args.NewValue;
        }
    }
}
