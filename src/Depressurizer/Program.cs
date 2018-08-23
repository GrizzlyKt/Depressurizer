using System;
using System.Windows.Forms;

namespace Depressurizer
{
    internal static class Program
    {
        #region Properties

        private static Settings Settings => Settings.Instance;

        #endregion

        #region Methods

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += OnApplicationExit;

            Settings.Load();

            Application.Run(new MainForm());
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            Settings.Save();
        }

        #endregion
    }
}
