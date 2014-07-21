using System;

namespace UrTQuery
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        { 
            System.Windows.Forms.Application.EnableVisualStyles();
#if !DEBUG
            try
            {
#endif
                System.Windows.Forms.Application.Run(new MainForm());
#if !DEBUG
            }
            catch (Exception ex)
            {
                System.Windows.Forms.Application.Run(new ErrorForm(ex));
            }
            
#endif
                return;
        }
    }
}
