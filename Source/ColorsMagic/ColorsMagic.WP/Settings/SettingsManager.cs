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
using Windows.Storage.Search;
using JetBrains.Annotations;

namespace ColorsMagic.WP.Settings
{
    public sealed class SettingsManager
    {
        private const string SettingsFileName = "settings.xml";

        public static readonly SettingsManager Instance = new SettingsManager();

        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(ProgramData));
        private static readonly StorageFolder SettingsFolder = ApplicationData.Current.RoamingFolder;

        private ProgramData _currentSettings = new ProgramData();

        private SettingsManager()
        {
        }

        [ItemNotNull]
        public async Task<ProgramData> GetCurrentData()
        {
            var allFiles = await SettingsFolder.GetFilesAsync();

            if (!allFiles.Any(sf => string.Equals(sf.Name, SettingsFileName, StringComparison.Ordinal)))
            {
                _currentSettings = new ProgramData();

                return _currentSettings;
            }

            var settingsFile = await SettingsFolder.GetFileAsync(SettingsFileName);

            using (var fileStream = await settingsFile.OpenReadAsync())
            {
                using (var stream = fileStream.AsStreamForRead())
                {
                    _currentSettings = (ProgramData)Serializer.Deserialize(stream);
                }

                return _currentSettings;
            }
        }

        public async Task SaveCurrentAsync()
        {
            var settingsFile = await SettingsFolder.CreateFileAsync(SettingsFileName, CreationCollisionOption.ReplaceExisting);

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
