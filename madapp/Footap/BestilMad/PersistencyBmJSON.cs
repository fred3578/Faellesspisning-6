using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Newtonsoft.Json;

namespace Footap.BestilMad
{
    class PersistencyBmJSON

    {
        private static string JsonFileName = "BmJSON.dat";

        public static async void SaveAsJsonAsync (BestiltMadJson bmjson)
        {
            string mandagJsonString = JsonConvert.SerializeObject(bmjson);
           
            SerializeMandagFileAsync(mandagJsonString, JsonFileName);
        }

        public static async Task<BestiltMadJson> LoadFromJsonAsync ()
        {
            string mandagJsonString = await DeserializeMandagFileAsync(JsonFileName);
            if (mandagJsonString != null)
                return (BestiltMadJson)JsonConvert.DeserializeObject(mandagJsonString, typeof(BestiltMadJson));
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
                PersistencyBmJSON.MessageDialogHelper.Show("Loading for the first time? - Try Add and Save some Mandag before trying to Save for the first time" , "File not Found");
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
