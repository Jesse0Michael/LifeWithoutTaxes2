using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using PSS.Installers;

namespace PleaseWait
{
    public partial class PleaseWaitForm : Form
    {
        public PleaseWaitForm()
        {
            InitializeComponent();

            // Customize game title and please wait text here
            this.Text = "Twin Blades";
            this.labelPleaseWait.Text = "Please wait while Twin Blades initializes. This can take a couple of minutes.";

            // Customize the form so it looks like a modal dialog box with an indeterminate progress bar
            this.ControlBox = false; // remove the 'close window' button
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
            this.progressBar.Style = ProgressBarStyle.Marquee; // indeterminate progress bar

            // TODO: comment this line out and change the project type to "Windows Application" to test this form
            this.backgroundWorker.RunWorkerAsync();
        }

        /// <summary>
        /// Checks that XnaInstaller.exe is running at regular intervals. 
        /// Note that we cannot use the Process.Exited event because XnaInstaller.exe will run with elevated privileges on Vista/7
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (XnaInstallerHelper.IsXnaInstallerRunning())
            {
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Called when XnaInstaller.exe has exited.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
    }
}
