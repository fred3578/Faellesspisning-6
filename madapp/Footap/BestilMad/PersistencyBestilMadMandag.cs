using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Newtonsoft.Json;

namespace Footap.BestilMad
{
    class PersistencyBestilMadMandag

    {
        private static string JsonFileName = "BestilMadMandagGemmer.dat";

        public static async void SaveMandagAsJsonAsync (List<BestilMad> BestilMadMandag)
        {
            string mandagJsonString = JsonConvert.SerializeObject(BestilMadMandag);
           
            SerializeMandagFileAsync(mandagJsonString, JsonFileName);
        }

        public static async Task<List<BestilMad>> LoadMandagFromJsonAsync ()
        {
            string mandagJsonString = await DeserializeMandagFileAsync(JsonFileName);
            if (mandagJsonString != null)
                return (List<BestilMad>)JsonConvert.DeserializeObject(mandagJsonString, typeof(List<BestilMad>));
            return null;
        }



        private static async void SerializeMandagFileAsync (string mandagJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName , CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile , mandagJsonString);
            
        }


        private static async Task<string> DeserializeMandagFileAsync (string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException ex)
            {
                PersistencyBestilMadMandag.MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Mandag before trying to Save for the first time" , "File not Found");
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
