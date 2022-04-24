using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

namespace _6_7laba
{
    public partial class App : Application
    {
        private static ResourceDictionary GetResourceDictionary(string cultureTitle)
        {
            ResourceDictionary dict = new ResourceDictionary();
            switch (cultureTitle)
            {
                case "en-US":
                    dict.Source = new Uri($"Resources\\Lang.{cultureTitle}.xaml", UriKind.Relative);
                    break;
                default:
                    dict.Source = new Uri($"Resources\\Lang.ru-RU.xaml", UriKind.Relative);
                    break;
            }
            return dict;
        }
        private static void setResourceDictionary(Window window, ResourceDictionary dict)
        {
            var dictionariesToDelete = new List<ResourceDictionary>();
            foreach (var dictionary in window.Resources.MergedDictionaries)
                if (new Regex("Lang.*?.xaml").IsMatch(dictionary.Source.ToString()))
                    dictionariesToDelete.Add(dictionary);

            foreach (var dictionary in dictionariesToDelete)
                window.Resources.MergedDictionaries.Remove(dictionary);

            window.Resources.MergedDictionaries.Add(dict);
        }
        public static void SetLanguageDictionary(Window window, string cultureTitle)
        {
            var dict = GetResourceDictionary(cultureTitle);

            setResourceDictionary(window, dict);
            foreach (Window childWindow in window.OwnedWindows)
            {
                setResourceDictionary(childWindow, dict);
            }
        }
    }
}
