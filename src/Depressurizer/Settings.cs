using System.IO;
using Depressurizer.Helpers;
using Newtonsoft.Json;

namespace Depressurizer
{
    /// <summary>
    ///     Depressurizer Settings Controller
    /// </summary>
    internal sealed class Settings
    {
        #region Static Fields

        private static readonly object SyncRoot = new object();

        private static volatile Settings _instance;

        #endregion

        #region Constructors and Destructors

        private Settings() { }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Depressurizer Settings Instance
        /// </summary>
        public static Settings Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }

                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new Settings();
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Loads a saved instance of Settings from the default location.
        /// </summary>
        public void Load()
        {
            lock (SyncRoot)
            {
                if (!File.Exists(Storage.File.Settings))
                {
                    return;
                }

                using (StreamReader reader = File.OpenText(Storage.File.Settings))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    _instance = (Settings) serializer.Deserialize(reader, typeof(Settings));
                }
            }
        }

        /// <summary>
        ///     Saves the current instance to the default location.
        /// </summary>
        public void Save()
        {
            lock (SyncRoot)
            {
                using (StreamWriter writer = File.CreateText(Storage.File.Settings))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, _instance);
                }
            }
        }

        #endregion
    }
}
