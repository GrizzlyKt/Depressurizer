using System;
using System.IO;

namespace Depressurizer.Helpers
{
    internal static class Storage
    {
        internal static class File
        {
            #region Properties

            /// <summary>
            ///     Path to %appdata%\Depressurizer\settings.json
            /// </summary>
            internal static string Settings => Path.Combine(Folder.Depressurizer, "settings.json");

            #endregion
        }

        internal static class Folder
        {
            #region Public Properties

            /// <summary>
            ///     Path to %appdata%
            /// </summary>
            public static string AppData
            {
                get
                {
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    return path;
                }
            }

            /// <summary>
            ///     Path to %appdata%\Depressurizer
            /// </summary>
            public static string Depressurizer
            {
                get
                {
                    string path = Path.Combine(AppData, "Depressurizer");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    return path;
                }
            }

            #endregion
        }
    }
}
