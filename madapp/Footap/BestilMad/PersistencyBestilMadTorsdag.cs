using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Newtonsoft.Json;

namespace Footap.BestilMad
{
    class PersistencyBestilMadTorsdag
    {
        private static string JsonFileName = "BestilMadTorsdagGemmer.dat";

        public static async void SaveTorsdagAsJsonAsync (List<BestilMad> BestilMadTorsdag)
        {
            string torsdagJsonString = JsonConvert.SerializeObject(BestilMadTorsdag);

            SerializeTorsdagFileAsync(torsdagJsonString , JsonFileName);
        }

        public static async Task<List<BestilMad>> LoadTorsdagFromJsonAsync ()
        {
            string torsdagJsonString = await DeserializeTorsdagFileAsync(JsonFileName);
            if (torsdagJsonString != null)
                return (List<BestilMad>)JsonConvert.DeserializeObject(torsdagJsonString , typeof(List<BestilMad>));
            return null;
        }



        private static async void SerializeTorsdagFileAsync (string torsdagJsonString , string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName , CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile , torsdagJsonString);

        }


        private static async Task<string> DeserializeTorsdagFileAsync (string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
            {
                PersistencyBestilMadTorsdag.MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Torsdag before trying to Save for the first time" , "File not Found");
                return null;
            }
        }


        private class MessageDialogHelper
        {
            public static async void Show (string content , string title)
            {
                MessageDialog messageDialog = new MessageDialog(content , title);
                await messageDialog.ShowAsync();
            }
        }
    }
}
