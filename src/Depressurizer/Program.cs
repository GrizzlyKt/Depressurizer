using System;
using System.Windows.Forms;
using Depressurizer.Dialogs;

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
            if (string.IsNullOrWhiteSpace(Settings.SteamPath))
            {
                using (SteamPathDialog dialog = new SteamPathDialog())
                {
                    dialog.ShowDialog();

                    Settings.SteamPath = dialog.Path;
                    Settings.Save();
                }
            }

            Application.Run(new MainForm());
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            Settings.Save();
        }

        #endregion
    }
}
