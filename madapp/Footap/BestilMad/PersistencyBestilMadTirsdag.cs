using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Newtonsoft.Json;

namespace Footap.BestilMad
{
    class PersistencyBestilMadTirsdag
    {
        private static string JsonFileName = "BestilMadTirsdagGemmer.dat";

        public static async void SaveTirsdagAsJsonAsync (List<BestilMad> BestilMadTirsdag)
        {
            string tirsdagJsonString = JsonConvert.SerializeObject(BestilMadTirsdag);

            SerializeTirsdagFileAsync(tirsdagJsonString , JsonFileName);
        }

        public static async Task<List<BestilMad>> LoadTirsdagFromJsonAsync ()
        {
            string tirsdagJsonString = await DeserializeTirsdagFileAsync(JsonFileName);
            if (tirsdagJsonString != null)
                return (List<BestilMad>)JsonConvert.DeserializeObject(tirsdagJsonString , typeof(List<BestilMad>));
            return null;
        }



        private static async void SerializeTirsdagFileAsync (string tirsdagJsonString , string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName , CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile , tirsdagJsonString);

        }


        private static async Task<string> DeserializeTirsdagFileAsync (string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
            {
                PersistencyBestilMadTirsdag.MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Tirsdag before trying to Save for the first time" , "File not Found");
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
