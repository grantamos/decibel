using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Byteopia.Helpers
{
    public static class RoamingStorage
    {
        public async static Task<bool> SaveObjectToRoamingFolder<T>(string filename, T o) where T : class
        {
            var appData = ApplicationData.Current.RoamingFolder;
            string jsonData = JSON.SeralizeObject<T>(o);
            StorageFile sampleFile = await appData.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(sampleFile, jsonData);
            return true;
        }

        public async static Task<T> GetObjectFromRoamingFolder<T>(string filename) where T : class
        {
            var appData = ApplicationData.Current.RoamingFolder;
            StorageFile sampleFile = await appData.GetFileAsync(filename);
            string jsonData = await FileIO.ReadTextAsync(sampleFile);
            T o = JSON.Deserialize<T>(jsonData);
            return o;
        }

        public async static Task<bool> FileExists(string fileName)
        {
            var appData = ApplicationData.Current.RoamingFolder;
            try
            {
                await appData.GetFileAsync(fileName);
            }
            catch (FileNotFoundException e)
            {
                return false;
            }
            return true;
        }
    }
}
