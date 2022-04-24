using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using _6_7laba.Models;
using System.Globalization;
using System.Windows.Resources;

namespace _6_7laba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ProductsRepository Sneakers { get; set; } = new ProductsRepository();
        private ObservableCollection<Products> filter = new ObservableCollection<Products>();
        public List<string> Langs = new List<string>() { "ru-RU", "en-US" };
        public MainWindow()
        {
            InitializeComponent();

            Sneakers.OutputData();
            Sneakers.OutputFilter();
            sneakerList.ItemsSource = Sneakers.GetSneakers();
            Filter.ItemsSource = Sneakers.GetFilter();

            InitLangsBox();

            List<string> styles = new List<string> { "Light", "Dark" };
            StyleBox.SelectionChanged += ThemeChange;
            StyleBox.ItemsSource = styles;
            StyleBox.SelectedItem = "Dark";

         
        }
        
        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            string style = StyleBox.SelectedItem as string;
            // определяем путь к файлу ресурсов
            var uri = new Uri("Resources/" + style +"Theme" + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
        private void InitLangsBox()
        {
            LanguageComboBox.ItemsSource = Langs;
        }
        private void SetCursor()
        {
            
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("/img/arrow.cur", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;
        }
        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

            var newWindow = new AddingWindow();
            newWindow.Owner = this;
            newWindow.Show();
        }
        private void MainWindow_Loaded(object sender, EventArgs e)
        {
            SetCursor();
        }
        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter = new ObservableCollection<Products>();
            foreach (var item in Sneakers.GetSneakers())
            {
                var sneak = Filter.SelectedItem.ToString();
                if (sneak == item.Company)
                {
                    filter.Add(item);
                }

            }

            sneakerList.ItemsSource = filter;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Text_From.Text = "";
            Text_To.Text = "";
            sneakerList.ItemsSource = Sneakers.GetSneakers();
        }

        private void Change_button_click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = (Products)sneakerList.SelectedItem;
                var newWindow = new ChangeWindow(selectedItem.Id);
                newWindow.Owner = this;
                newWindow.Show();
            }
            catch(Exception er)
            {
                MessageBox.Show("Выберите элемент, который хотите изменить.");
            }
        }

        private void Delete_button_click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = (Products)sneakerList.SelectedItem;
                Sneakers.DeleteSneaker(selectedItem.Id);
            }
            catch(Exception er)
            {
                MessageBox.Show("Выберите элемент, который хотите удалить.");

            }

        }

        private void Search_button_click(object sender, RoutedEventArgs e)
        {
            string searchString = SearchField.Text;
            Regex regex = new Regex(searchString, RegexOptions.IgnoreCase);
            var filter = new List<Products>();
            foreach (var phone in Sneakers.GetSneakers())
            {
                if (regex.IsMatch(phone.Title))
                    filter.Add(phone);
            }
            sneakerList.ItemsSource = filter;
       
        }
        public void PriceFilter()
        {
            var list = new List<Products>();
            decimal from, to;
            try
            {
                if (Text_To.Text == "" && Text_From.Text == "")
                {
                    sneakerList.ItemsSource = Sneakers.GetSneakers();

                }
                else
                {
                    foreach (var item in Sneakers.GetSneakers())
                    {

                        if (Text_To.Text == "")
                        {
                            to = Decimal.MaxValue;
                            if (item.Price >= Convert.ToDecimal(Text_From.Text) && item.Price <= Convert.ToDecimal(to))
                            {
                                list.Add(item);
                            }
                        }
                        else if (Text_From.Text == "")
                        {
                            from = -1;
                            if (item.Price >= Convert.ToDecimal(from) && item.Price <= Convert.ToDecimal(Text_To.Text))
                            {
                                list.Add(item);
                            }
                        }

                        else if (item.Price >= Convert.ToDecimal(Text_From.Text) && item.Price <= Convert.ToDecimal(Text_To.Text))
                        {
                            list.Add(item);
                        }

                    }
                    sneakerList.ItemsSource = list;

                }
            }
            catch (Exception er)
            {

            }

        }
        private void Block_from_changed(object sender, TextChangedEventArgs e)
        {
            PriceFilter();
        }

        private void Block_to_changed(object sender, TextChangedEventArgs e)
        {
            PriceFilter();
        }
        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           App.SetLanguageDictionary(this, LanguageComboBox.SelectedItem.ToString());
        }

        private void customControl_Click(object sender, RoutedEventArgs e)
        {
            txtBlock.Text = "You have just click your custom control";
        }
    }
}
