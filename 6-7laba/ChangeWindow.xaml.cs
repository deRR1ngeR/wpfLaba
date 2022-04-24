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
using System.Windows.Shapes;
using _6_7laba.Models;
namespace _6_7laba
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ChangeWindow : Window
    {
        public uint SneakId { get; set; }
        public ChangeWindow(uint Id) : this()
        {
            InitializeComponent();
            SneakId = Id;
            InitForm();
        }

        public ChangeWindow()
        {
        }

        private void InitForm()
        {
            Products Sneak = MainWindow.Sneakers.GetItemById(SneakId);
            TitleFiled.Text = Sneak.Title;
            CompanyFiled.Text = Sneak.Company;
            DescriptFiled.Text = Sneak.Description;
            ImageFiled.Text = Sneak.ImagePath;
            PriceFiled.Text = Sneak.Price.ToString();
            RateFiled.Value = Sneak.Rating;
        }
        public void ChangeElementCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var canNullablePhone = MainWindow.Sneakers.GetItemById(SneakId);
            if (canNullablePhone == null) return;
      
            var Sneak = canNullablePhone;
            Sneak.Title = TitleFiled.Text;
            Sneak.Company = CompanyFiled.Text;
            Sneak.Description = DescriptFiled.Text;
            Sneak.ImagePath = ImageFiled.Text;
            Sneak.Price = decimal.Parse(PriceFiled.Text);
            Sneak.Rating = (float)RateFiled.Value;


            MainWindow.Sneakers.LocalCommit();
            MainWindow.Sneakers.CommitData();
            this.Close();
        }

    }
}
