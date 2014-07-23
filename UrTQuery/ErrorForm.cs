using System;
using System.Drawing;
using System.Windows.Forms;
using UrTQuery.Properties;

namespace UrTQuery
{
    public partial class ErrorForm : Form
    {
        Exception EX;
        bool full = false;
        Label error = new Label();
        Label label = new Label();
        Button Copy = new Button();

        public ErrorForm(Exception ex)
        {
            EX = ex;

            // dunno if i need this 2 things
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;

            this.Name = "Error";
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.Icon = Resources.Azuresol_Sketchy_Quake_3;
            this.AutoSize = true;
            this.ShowIcon = true;
            this.MaximizeBox = false;
            this.Text = "Fatal Error";
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            Button Show = new Button();
            Button Close = new Button();

            label.Text = EX.Message;
            label.AutoSize = true;
            label.Location = new Point(5, 5);
            this.Controls.Add(label);

            Copy.AutoSize = true;
            Copy.Click += Copy_Click;
            Copy.Text = "Copy to Clipboard";
            Copy.Location = new Point(5, label.Location.Y + label.Height + 5);
            this.Controls.Add(Copy);

            Show.AutoSize = true;
            Show.Click += Show_Click;
            Show.Text = "Show Full Error";
            Show.Location = new Point(Copy.Location.X + Copy.Width + 5, label.Location.Y + label.Height + 5);
            this.Controls.Add(Show);

            Close.AutoSize = true;
            Close.Click += Close_Click;
            Close.Text = "Close";
            Close.Location = new Point(Show.Location.X + Show.Width + 5, label.Location.Y + label.Height + 5);
            this.Controls.Add(Close);

            error.Text = EX.ToString();
            error.AutoSize = true;
            error.Location = new Point(5, Close.Location.Y + Close.Height + 10);
            error.Visible = false;
            this.Controls.Add(error);

        }

        void Copy_Click(object sender, EventArgs e)
        {
            if (full)
            {
                Clipboard.SetText(EX.ToString());
            }
            else
            {
                Clipboard.SetText(EX.Message);
            }
        }

        void Show_Click(object sender, EventArgs e)
        {
            if (full)
            {
                ((Button)sender).Text = "Show Full Error";
                error.Visible = false;
                full = false;
            }
            else
            {
                ((Button)sender).Text = "Hide Full Error";
                error.Visible = true;
                full = true;
            }
        }

        void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
