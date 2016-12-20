using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Footap.Annotations;


namespace Footap
{
    class BeborereVm : INotifyPropertyChanged 
    {
        public ObservableCollection<Beborere> Beboreres { get; set; }
        public string Name { get; set; }
        public int Alder { get; set; }
        public int HusNr { get; set; }
        public ObservableCollection<Hus> Huses { get; set; }
        public int ListHusNr { get; set; }
        public int SelectedIndex { get; set; }

        public RelayCommand AddBeborerCommand { get; set; }
        public RelayCommand RemoveBeborerCommand { get; set; }
        public RelayCommand GetBeborerCommand { get; set; }
        public RelayCommand SaveBeborerCommand { get; set; }



       

        public BeborereVm()
       {
          AddBeborerCommand = new RelayCommand(Add);
          RemoveBeborerCommand = new RelayCommand(Remove);
           GetBeborerCommand = new RelayCommand(LoadBeboreres);
            SaveBeborerCommand = new RelayCommand(SaveBeboreres);

            Huses = new ObservableCollection<Hus>();
            Beboreres = new ObservableCollection<Beborere>();
            Beboreres.Add(new Beborere("Jens", 22, 74));
          

        }

        //public int[] HusNrArray = new int[23]
        //{
        //     72, 74, 76, 78, 80, 82, 84, 86, 88, 90, 92, 94, 96, 98, 100, 102, 104, 106, 108, 110, 112, 114, 116
        //};

        //public void mycode()
        //{
        //    List<int> HusNr02 = new List<int>();
        //    HusNr02.Add(55);
        //}




        //public int HusNr()
        //{
        //    for (int i = 0; i < HusNrArray.Length; i++)
        //    {
        //        return i;
        //    }
        ////    foreach (int Husnr in HusNrArray)
        ////    {
        ////        return Husnr;
        ////    }
        //}

       

        private async void LoadBeboreres()
        {
            var Beboreress = await PersistencyService.LoadBeborereFromJsonAsync();
            if (Beboreress != null)
            {
                //Beboreres.Clear();
                foreach (var Beborer in Beboreress)
                {
                    Beboreres.Add(Beborer);
                }

            }

        }

 public void Add()
        {
            Beboreres.Add(new Beborere(Name, Alder, HusNr));

        }



        public void Remove()
        {
            

            
            if (SelectedIndex != null)
            {
                Beboreres.RemoveAt(SelectedIndex);
                OnPropertyChanged();
            }
               
                  
           
            
        }

        private async void SaveBeboreres()
        {
            
            PersistencyService.SaveNotesAsJsonAsync(Beboreres);
        }


        //public override string ToString()
        //{
        //    for (int i = 0; i < HusNrArray.Length; i++)
        //    {
        //        HusNr01 = i;

        //    }
        //    return "HusNr {0}";
        //}
        #region PropertyChangedSupport
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }

   
}
