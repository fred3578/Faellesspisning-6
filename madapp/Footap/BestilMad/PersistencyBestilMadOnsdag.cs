using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Newtonsoft.Json;

namespace Footap.BestilMad
{
    class PersistencyBestilMadOnsdag
    {
        private static string JsonFileName = "BestilMadOnsdagGemmer.dat";

        public static async void SaveOnsdagAsJsonAsync (List<BestilMad> BestilMadOnsdag)
        {
            string onsdagJsonString = JsonConvert.SerializeObject(BestilMadOnsdag);

            SerializeOnsdagFileAsync(onsdagJsonString , JsonFileName);
        }

        public static async Task<List<BestilMad>> LoadOnsdagFromJsonAsync ()
        {
            string onsdagJsonString = await DeserializeOnsdagFileAsync(JsonFileName);
            if (onsdagJsonString != null)
                return (List<BestilMad>)JsonConvert.DeserializeObject(onsdagJsonString , typeof(List<BestilMad>));
            return null;
        }



        private static async void SerializeOnsdagFileAsync (string onsdagJsonString , string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName , CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile , onsdagJsonString);

        }


        private static async Task<string> DeserializeOnsdagFileAsync (string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
            {
                PersistencyBestilMadOnsdag.MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Notes before trying to Onsdag for the first time" , "File not Found");
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
