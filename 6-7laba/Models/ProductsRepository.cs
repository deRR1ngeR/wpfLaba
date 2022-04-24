using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.IO;

using Newtonsoft.Json;


namespace _6_7laba.Models
{
    public class ProductsRepository
    {
        private readonly string pathData = @"D:\Мои проекты\4 сем\ООП\6-7laba\6-7laba\Services\Sneakers.json";
        private readonly string pathFilter= @"D:\Мои проекты\4 сем\ООП\6-7laba\6-7laba\Services\Filter.json";

        private ObservableCollection<Products> SneakersList = new ObservableCollection<Products>();
        private ObservableCollection<string> FilterList = new ObservableCollection<string>();



        public ProductsRepository()
        {
            SneakersList = new ObservableCollection<Products>();
        }
        public ObservableCollection<Products> GetSneakers()
        {
            return SneakersList;
        }
        public Products? GetItemById(uint id)
        {
            return SneakersList.Where(SneakersList => SneakersList.Id == id).FirstOrDefault();
        }
        public ObservableCollection<string> GetFilter()
        {
            return FilterList;
        }
        public void AddItem(Products item)
        {
            bool Flag =false;
            SneakersList.Add(item);
        foreach(var item2 in FilterList)
            {
                if (item.Company == item2)
                {
                    Flag = true;
                    break;
                }
            }
            if (Flag != true)
            {
                FilterList.Add(item.Company);
            }
            
           CommitData();
        }
        public void LocalCommit()
        {
            Products[] tmpl = new Products[SneakersList.Count()];
            SneakersList.CopyTo(tmpl, 0);
            SneakersList.Clear();
            FilterList.Clear();
            for (int i = 0; i < tmpl.Length; i++)
            {
            bool Flag =false;

                SneakersList.Add(tmpl[i]);
                foreach (var item2 in FilterList)
                {
                    if (tmpl[i].Company == item2)
                    {
                        Flag = true;
                        break;
                    }
                }
                if (Flag != true)
                {
                    FilterList.Add(tmpl[i].Company);
                }
            }

            

            CommitData();
        }
        public void CommitData()
        {
            using(StreamWriter writer = File.CreateText(pathData))
            {
                string output = JsonConvert.SerializeObject(SneakersList);
                writer.Write(output);
            }
            using (StreamWriter writer = File.CreateText(pathFilter))
            {
                string output = JsonConvert.SerializeObject(FilterList);
                writer.Write(output);
            }
        }
       
        public void OutputData()
        {
            var FileExists = File.Exists(pathData);
            if (!FileExists)
            {
                File.CreateText(pathData).Dispose();
                
            }
            else
            {

                using (var reader = File.OpenText(pathData))
                {
                var FileText = reader.ReadToEnd();

                    SneakersList = JsonConvert.DeserializeObject<ObservableCollection<Products>>(FileText);
                    
                }
            }
            
           
        }
        public void  OutputFilter()
        {
            var FileExists = File.Exists(pathFilter);
            if (!FileExists)
            {
                File.CreateText(pathFilter).Dispose();

            }
            else
            {
                using (var reader = File.OpenText(pathFilter))
                {
                    var FileText = reader.ReadToEnd();

                    FilterList = JsonConvert.DeserializeObject<ObservableCollection<string>>(FileText);
                }
            }
          
        }

        public void DeleteSneaker(uint id)
        {
            foreach(var item in SneakersList)
            {
                if(item.Id == id)
                {
                    SneakersList.Remove(item);
                    break;
                }
            }
            LocalCommit();
        }
    }
}
