using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;
using JetBrains.Annotations;

namespace ColorsMagic.WP.Settings
{
    public sealed class SettingsManager
    {
        private const string SettingsFileName = "settings.xml";

        public static readonly SettingsManager Instance = new SettingsManager();

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ProgramData));

        private ProgramData _currentSettings = new ProgramData();

        private SettingsManager()
        {
        }

        [ItemNotNull]
        public async Task<ProgramData> GetCurrentData()
        {
            var settingsFile = await GetSettingsFile().ConfigureAwait(false);

            if (ReferenceEquals(settingsFile, null))
            {
                _currentSettings = new ProgramData();

                return _currentSettings;
            }

            using (var fileStream = await settingsFile.OpenReadAsync())
            {
                using (var stream = fileStream.AsStreamForRead())
                {
                    _currentSettings = (ProgramData) Serializer.Deserialize(stream);
                }

                return _currentSettings;
            }
        }

        private static async Task<StorageFile> GetSettingsFile()
        {
            var appRoamingFolder = ApplicationData.Current.RoamingFolder;

            return await appRoamingFolder.GetFileAsync(SettingsFileName);
        }

        public async Task SaveCurrentAsync()
        {
            var settingsFile = await GetSettingsFile().ConfigureAwait(false);

            using (var fileStream = await settingsFile.OpenTransactedWriteAsync())
            {
                using (var stream = fileStream.Stream)
                {
                    using (var legacyStream = stream.AsStreamForWrite())
                    {
                        legacyStream.SetLength(0);

                        Serializer.Serialize(legacyStream, _currentSettings);
                    }
                }

                await fileStream.CommitAsync();
            }
        }
    }
}
