﻿#region LICENSE

//     This file (Program.cs) is part of Depressurizer.
//     Copyright (C) 2011 Steve Labbe
//     Copyright (C) 2017 Theodoros Dimos
//     Copyright (C) 2017 Martijn Vegter
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Windows.Forms;
using NDesk.Options;
using Rallion;

namespace Depressurizer
{
    internal static class Program
    {
        #region Static Fields

        public static AppLogger Logger;

        #endregion

        #region Methods

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FatalError.InitializeHandler();

            Logger = new AppLogger();
            Logger.Level = LoggerLevel.None;
            Logger.DateFormat = "HH:mm:ss'.'ffffff";

            Logger.MaxFileSize = 2000000;
            Logger.MaxBackup = 1;
            Logger.FileNameTemplate = "Depressurizer.log";

            Settings.Instance.Load();

            Logger.Write(LoggerLevel.Info, GlobalStrings.Program_ProgramInitialized, Logger.Level);

            AutomaticModeOptions autoOpts = ParseAutoOptions(args);

            if (autoOpts != null)
            {
                Logger.Write(LoggerLevel.Info, "Automatic mode set, loading automatic mode form.");
                Logger.WriteObject(LoggerLevel.Verbose, autoOpts, "Automatic Mode Options:");
                Application.Run(new AutomaticModeForm(autoOpts));
            }
            else
            {
                Logger.Write(LoggerLevel.Info, "Automatic mode not set, loading main form.");
                Application.Run(new FormMain());
            }

            Settings.Instance.Save();

            Logger.Write(LoggerLevel.Info, GlobalStrings.Program_ProgramClosing);
            Logger.EndSession();
        }

        private static AutomaticModeOptions ParseAutoOptions(string[] args)
        {
            AutomaticModeOptions config = new AutomaticModeOptions();
            bool auto = false;

            OptionSet opts = new OptionSet
            {
                {
                    "auto", v => auto = true
                },
                {
                    "p|profile=", v => config.CustomProfile = v
                },
                {
                    "checksteam", v => config.CheckSteam = v != null
                },
                {
                    "closesteam", v => config.CloseSteam = v != null
                },
                {
                    "updatelib", v => config.UpdateGameList = v != null
                },
                {
                    "import", v => config.ImportSteamCategories = v != null
                },
                {
                    "updatedblocal", v => config.UpdateAppInfo = v != null
                },
                {
                    "updatedbhltb", v => config.UpdateHltb = v != null
                },
                {
                    "updatedbweb", v => config.ScrapeUnscrapedGames = v != null
                },
                {
                    "savedb", v => config.SaveDBChanges = v != null
                },
                {
                    "saveprofile", v => config.SaveProfile = v != null
                },
                {
                    "export", v => config.ExportToSteam = v != null
                },
                {
                    "launch", v => config.SteamLaunch = SteamLaunchType.Normal
                },
                {
                    "launchbp", v => config.SteamLaunch = SteamLaunchType.BigPicture
                },
                {
                    "tolerant", v => config.TolerateMinorErrors = v != null
                },
                {
                    "quiet", v => config.AutoClose = AutoCloseType.UnlessError
                },
                {
                    "silent", v => config.AutoClose = AutoCloseType.Always
                },
                {
                    "all", v => config.ApplyAllAutoCats = v != null
                },
                {
                    "<>", v => config.AutoCats.Add(v)
                }
            };

            opts.Parse(args);

            return auto ? config : null;
        }

        #endregion
    }
}
