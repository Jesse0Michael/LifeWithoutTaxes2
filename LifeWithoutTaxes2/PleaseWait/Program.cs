using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PleaseWait
{
    // TODO: make sure to use the 'public' modifier here
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PleaseWaitForm());
        }
    }
}
