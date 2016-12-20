using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Footap.BestilMad;
using Newtonsoft.Json;

namespace Footap
{
    class MaaltidViewModel : INotifyPropertyChanged
    {
        public string Dag { get; set; }
        public string Ret { get; set; }
        public double MadUdgift { get; set; }
        public Maaltid SelectedItem { get; set; }
        public Maaltid SelectedItem2 { get; set; }
        public RelayMaaltid HentMaaltid { get; set; }
        public RelayMaaltid GemMaaltid { get; set; }
        public ObservableCollection<BestilMad.BestilMad> BestilMadMandag { get; set; }
        public ObservableCollection<BestilMad.BestilMad> BestilMadTirsdag { get; set; }
        public ObservableCollection<BestilMad.BestilMad> BestilMadOnsdag { get; set; }
        public ObservableCollection<BestilMad.BestilMad> BestilMadTorsdag { get; set; }




        public ObservableCollection<Maaltid> MaaltiderNu { get; set; }
        public ObservableCollection<Maaltid> MaaltiderNext { get; set; }
        public int HusNr { get; private set; }
        public int VoksenAntalMandag { get; private set; }
        public int VoksenAntalTirsdag { get; private set; }
        public int VoksenAntalOnsdag { get; private set; }
        public int VoksenAntalTorsdag { get; private set; }
        public int UngAntalMandag { get; private set; }
        public int UngAntalTirsdag { get; private set; }
        public int UngAntalOnsdag { get; private set; }
        public int UngAntalTorsdag { get; private set; }
        public int BarnAntalMandag { get; private set; }
        public int BarnAntalTirsdag { get; private set; }
        public int BarnAntalOnsdag { get; private set; }
        public int BarnAntalTorsdag { get; private set; }
        public int SpaedAntalMandag { get; private set; }
        public int SpaedAntalTirsdag { get; private set; }
        public int SpaedAntalTorsdag { get; private set; }
        public int SpaedAntalOnsdag { get; private set; }

        public MaaltidViewModel()
        {
            HentMaaltid = new RelayMaaltid(LoadFood);
            GemMaaltid = new RelayMaaltid(SaveFood);
            MaaltiderNu = new ObservableCollection<Maaltid>();
            MaaltiderNu.Add(new Maaltid("Mandag", "Kylling med Korhansovs", 30.5));
            MaaltiderNext = new ObservableCollection<Maaltid>();
            MaaltiderNext.Add(new Maaltid("Torsdag", "Fiskefars med konkylieknas", 1337));
            BestilMadMandag = new ObservableCollection<BestilMad.BestilMad>();
           
        }

        public void AddDenneUge()
        {
            MaaltiderNu.Add(new Maaltid(Dag, Ret, MadUdgift));
            OnPropertyChanged();
        }

        public void AddNyeUge()
        {
            MaaltiderNext.Add(new Maaltid(Dag, Ret, MadUdgift));
            OnPropertyChanged();
        }


        public void AddBestil ()
        {
            BestilMadMandag.Add(new BestilMad.BestilMad(HusNr , VoksenAntalMandag , UngAntalMandag , BarnAntalMandag , SpaedAntalMandag));
            BestilMadTirsdag.Add(new BestilMad.BestilMad(HusNr , VoksenAntalTirsdag , UngAntalTirsdag , BarnAntalTirsdag , SpaedAntalTirsdag));
            BestilMadOnsdag.Add(new BestilMad.BestilMad(HusNr , VoksenAntalOnsdag , UngAntalOnsdag , BarnAntalOnsdag , SpaedAntalOnsdag));
            BestilMadTorsdag.Add(new BestilMad.BestilMad(HusNr , VoksenAntalTorsdag , UngAntalTorsdag , BarnAntalTorsdag , SpaedAntalTorsdag));


        }

        public void Remove()
        {
            if (SelectedItem != null)
            {
                MaaltiderNext.Remove(SelectedItem);
                OnPropertyChanged();
            }
            else if (SelectedItem2 != null)
            {
                MaaltiderNu.Remove(SelectedItem2);
                OnPropertyChanged();
            }
        


      
    }
        public void Move ()
        {
            if (SelectedItem != null)
            {
                MaaltiderNu.Add(SelectedItem);
                MaaltiderNext.Remove(SelectedItem);
            }
        }
        private async void LoadFood ()
        {
            var notes = await PersistencyMaaltid.LoadNotesFromJsonAsync();
            if (notes != null)
            {
                MaaltiderNu.Clear();
                foreach (var note in notes)
                {
                    MaaltiderNu.Add(note);
                }

            }

        }

        private async void SaveFood ()
        {
            PersistencyMaaltid.SaveNotesAsJsonAsync(MaaltiderNu);
        }



        private async void LoadBestil ()
        {
            var bestil = await PersistencyBmJSON.LoadFromJsonAsync();
            if (bestil != null)
            {
                BestilMadMandag.Clear();
                foreach (BestilMad.BestilMad bestils in bestil)
                {
                    BestilMadMandag.Add(bestils);
                }

            }

        }

        #region PropertyChangeSupport

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
