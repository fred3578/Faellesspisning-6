using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace Footap.BestilMad
{
    class BestilVM
    {
        public int HusNr { get; set; }
        public int VoksenAntalMandag { get; set; }
        public int UngAntalMandag { get; set; }
        public int BarnAntalMandag { get; set; }
        public int SpaedAntalMandag { get; set; }

        public int VoksenAntalTirsdag { get; set; }
        public int UngAntalTirsdag { get; set; }
        public int BarnAntalTirsdag { get; set; }
        public int SpaedAntalTirsdag { get; set; }

        public int VoksenAntalOnsdag { get; set; }
        public int UngAntalOnsdag { get; set; }
        public int BarnAntalOnsdag { get; set; }
        public int SpaedAntalOnsdag { get; set; }

        public int VoksenAntalTorsdag { get; set; }
        public int UngAntalTorsdag { get; set; }
        public int BarnAntalTorsdag { get; set; }
        public int SpaedAntalTorsdag { get; set; }

        public RelayBestilMad HentNoget { get; set; }
        public RelayBestilMad GemNoget { get; set; }
        public RelayBestilMad HentListe { get; set; }

        public double PrisPrHusstand { get; set; }
        public List<BestilMad> BestilMadMandag { get; set; }

        public List<BestilMad> BestilMadTirsdag { get; set; }
        public List<BestilMad> BestilMadOnsdag { get; set; }
        public List<BestilMad> BestilMadTorsdag { get; set; }
        public List<BestilMad> BestilMadSpecial { get; set; }
        public ObservableCollection<PrisPrHus> KvitteringsListe { get; set; }
        public ObservableCollection<Maaltid> MaaltiderNu { get; set; }


        public BestilVM ()
        {
            HentNoget = new RelayBestilMad(LoadAlleDageMinusSpecial);
            GemNoget = new RelayBestilMad(BestilKnapAlleDageMinusSpecialTest);
            HentListe = new RelayBestilMad(loadpris);
            BestilMadMandag = new List<BestilMad>();
            BestilMadTirsdag = new List<BestilMad>();
            BestilMadOnsdag = new List<BestilMad>();
            BestilMadTorsdag = new List<BestilMad>();
            BestilMadSpecial = new List<BestilMad>();
            PrisPrHusstand = VoksenAntalMandag * 1 + UngAntalMandag * 0.5 * BarnAntalMandag * 0.25;
            KvitteringsListe = new ObservableCollection<PrisPrHus>();
            MaaltiderNu = new ObservableCollection<Maaltid>();


        }

        public void BestilKnapAlleDageMinusSpecialTest()
        {
            LoadAlleDageMinusSpecial();
            Add();
            SaveAlleDageMinusSpecial();
        }
        public void Add ()
        {
            BestilMadMandag.Add(new BestilMad(HusNr , VoksenAntalMandag , UngAntalMandag , BarnAntalMandag , SpaedAntalMandag));
            BestilMadTirsdag.Add(new BestilMad(HusNr , VoksenAntalTirsdag , UngAntalTirsdag , BarnAntalTirsdag , SpaedAntalTirsdag));
            BestilMadOnsdag.Add(new BestilMad(HusNr , VoksenAntalOnsdag , UngAntalOnsdag , BarnAntalOnsdag , SpaedAntalOnsdag));
            BestilMadTorsdag.Add(new BestilMad(HusNr , VoksenAntalTorsdag , UngAntalTorsdag , BarnAntalTorsdag , SpaedAntalTorsdag));


        }
        private async void SaveAlleDageMinusSpecial ()
        {
            BestiltMadJson ugesmad = new BestiltMadJson(BestilMadMandag , BestilMadTirsdag , BestilMadOnsdag , BestilMadTorsdag);
            PersistencyBmJSON.SaveAsJsonAsync(ugesmad);
            //PersistencyBestilMadMandag.SaveMandagAsJsonAsync(BestilMadMandag);
            //PersistencyBestilMadTirsdag.SaveTirsdagAsJsonAsync(BestilMadTirsdag);
            //PersistencyBestilMadOnsdag.SaveOnsdagAsJsonAsync(BestilMadOnsdag);
            //PersistencyBestilMadTorsdag.SaveTorsdagAsJsonAsync(BestilMadTorsdag);
        }

        private async void LoadAlleDageMinusSpecial ()
        {
            await Task.Delay(1000);
            var bmJSONs = await PersistencyBmJSON.LoadFromJsonAsync();
            if (bmJSONs != null)
                BestilMadMandag.Clear();

            
            foreach (var bestilMad in bmJSONs.MandagsMad)
            {
                BestilMadMandag.Add(bestilMad);
            }

            BestilMadTirsdag.Clear();

            foreach (var bestilMad in bmJSONs.TirsdagsMad)
            {
                BestilMadTirsdag.Add(bestilMad);
            }

            BestilMadOnsdag.Clear();

            foreach (var bestilMad in bmJSONs.OnsdagsMad)
            {
                BestilMadOnsdag.Add(bestilMad);
            }

            BestilMadTorsdag.Clear();

            foreach (var bestilMad in bmJSONs.TorsdagsMad)
            {
                BestilMadTorsdag.Add(bestilMad);
            }
            await Task.Delay(1000);

        }

        //Nu indhentes infomationerne der skal bruges til udregningskoden

     
        

        


        public async void loadpris()
        {
           
            {
                await Task.Delay(1000);
                var MaaltiderNu = await PersistencyMaaltid.LoadNotesFromJsonAsync();
                await Task.Delay(100);
                var bmJSONs = await PersistencyBmJSON.LoadFromJsonAsync();
                if (bmJSONs != null)
                    BestilMadMandag.Clear();

                double antalsamledekuverter = 0;

                foreach (var bestilMad in bmJSONs.MandagsMad)
                {
                    BestilMadMandag.Add(bestilMad);
                    antalsamledekuverter = antalsamledekuverter + bestilMad.GetKuverter();

                }

                BestilMadTirsdag.Clear();

                foreach (var bestilMad in bmJSONs.TirsdagsMad)
                {
                    BestilMadTirsdag.Add(bestilMad);
                    antalsamledekuverter = antalsamledekuverter + bestilMad.GetKuverter();
                    
                }

                BestilMadOnsdag.Clear();

                foreach (var bestilMad in bmJSONs.OnsdagsMad)
                {
                    BestilMadOnsdag.Add(bestilMad);
                    antalsamledekuverter = antalsamledekuverter + bestilMad.GetKuverter();

                }

                BestilMadTorsdag.Clear();

                foreach (var bestilMad in bmJSONs.TorsdagsMad)
                {
                    BestilMadTorsdag.Add(bestilMad);
                    antalsamledekuverter = antalsamledekuverter + bestilMad.GetKuverter();

                }
                await Task.Delay(1000);

                LoadFood();

                await Task.Delay(100);

                double SamledePrisForUgen = 0;

                foreach (Maaltid M in MaaltiderNu)
                {
                    SamledePrisForUgen = SamledePrisForUgen + M.MadUdgift;

                }

                double KuvertPris = SamledePrisForUgen/antalsamledekuverter;


                List<PrisPrHus>   boliger = new List<PrisPrHus>();

                UdregnPrDag(KuvertPris,boliger,BestilMadMandag);
                UdregnPrDag(KuvertPris , boliger , BestilMadTirsdag);
                UdregnPrDag(KuvertPris , boliger , BestilMadOnsdag);
                UdregnPrDag(KuvertPris , boliger , BestilMadTorsdag);

                KvitteringsListe.Clear();
                foreach (var b in boliger)
                {
                         KvitteringsListe.Add(b);
                }

                //KvitteringsListe.Add(BestilMadOnsdag);
                //KvitteringsListe.Add(BestilMadTorsdag);
                //KvitteringsListe.Add(BestilMadTirsdag);
            }


            // Start af udregningskode



            //foreach (BestilMad prisPrHusstand in BestilMadMandag)
            //{




            //    StringWriter stringWriter = new StringWriter();
            //    stringWriter.Write($"{KvitteringsListe} /n Husnummer: {HusNr} /n Skal betale: {PrisPrHusstand}");


            //}

            //foreach (var HusNr in BestilMadTirsdag)
            //{
            //    PrisPrHusstand = VoksenAntalTirsdag*1 + UngAntalTirsdag*0.5*BarnAntalTirsdag*0.25;

            //    StringWriter stringWriter = new StringWriter();
            //    stringWriter.Write(PrisPrHusstand);

            //}

            //foreach (var husNr in BestilMadOnsdag)
            //{
            //    PrisPrHusstand = VoksenAntalOnsdag*1 + UngAntalOnsdag*0.5*BarnAntalOnsdag*0.25;

            //    StringWriter stringWriter = new StringWriter();
            //    stringWriter.Write(PrisPrHusstand);
            //    stringWriter.Write(PrisPrHusstand);
            //}
            //foreach (var husNr in BestilMadTorsdag)
            //{
            //    PrisPrHusstand = VoksenAntalTorsdag*1 + UngAntalTorsdag*0.5*BarnAntalTorsdag*0.25;

            //    StringWriter stringWriter = new StringWriter();
            //    stringWriter.Write(PrisPrHusstand);
            //    stringWriter.Write(PrisPrHusstand);
            //}


            // Gammel kode fra da vi havde 4 filer at gemme listerne i (1 for hver, bemærk, at de kalder på hver deres Persistency class).
            //var uges = await PersistencyBmJSON.LoadFromJsonAsync();
            //if (uges != null)
            //{
            //    ugesmad.Clear();

            //    foreach (var uge in uges)
            //    {
            //        ugesmad.Add(uges);
            //    }

            //var mandags = await PersistencyBestilMadMandag.LoadMandagFromJsonAsync();
            //if (mandags != null)
            //{
            //    BestilMadMandag.Clear();

            //    foreach (var mandag in mandags)
            //    {
            //        BestilMadMandag.Add(mandag);

            //    }

            //}
            //await Task.Delay(100);
            //var tirsdags = await PersistencyBestilMadTirsdag.LoadTirsdagFromJsonAsync();
            //if (tirsdags != null)
            //{
            //    BestilMadTirsdag.Clear();

            //    foreach (var tirsdag in tirsdags)
            //    {
            //        BestilMadTirsdag.Add(tirsdag);

            //    }

            //}
            //await Task.Delay(100);
            //var onsdags = await PersistencyBestilMadOnsdag.LoadOnsdagFromJsonAsync();
            //if (onsdags != null)
            //{
            //    BestilMadOnsdag.Clear();

            //    foreach (var onsdag in onsdags)
            //    {
            //        BestilMadOnsdag.Add(onsdag);

            //    }

            //}
            //await Task.Delay(100);
            //var torsdags = await PersistencyBestilMadTorsdag.LoadTorsdagFromJsonAsync();
            //if (torsdags != null)
            //{
            //    BestilMadTorsdag.Clear();

            //    foreach (var torsdag in torsdags)
            //    {
            //        BestilMadTorsdag.Add(torsdag);

            //    }

            //}}


        }

        private async void LoadFood ()
        {
            await Task.Delay(1000);
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

        private void UdregnPrDag(double KuvertPris, List<PrisPrHus> boliger, List<BestilMad> madliste)
        {
            foreach (BestilMad P in madliste)
            {
                PrisPrHus p = new PrisPrHus()
                {
                    Nr = P.HusNr,
                    Pris = P.GetKuverter() * KuvertPris
                };


                if (!boliger.Contains(p))
                {
                    if (p != null)
                    {
                        KvitteringsListe.Add(p);
                    }
                }
                else
                {
                    PrisPrHus ph = boliger.Find(b => b.Nr == P.HusNr);
                    ph.Pris = ph.Pris + p.Pris;
                }
            }
        }

       
    }

}