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
    public partial class AddingWindow : Window
    {
        private 
            string imgPath = @"D:\Мои проекты\4 сем\ООП\6-7laba\6-7laba\img\";
        public AddingWindow()
        {
            InitializeComponent();

        }

        public void NewElementCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            uint maxId = 0;
            
            var Sneaker = new Products();
          var list = MainWindow.Sneakers.GetSneakers();
          if(list != null)
          foreach(var t in list)
          {
              if (t.Id >maxId)
              {
                  maxId = t.Id;
              }
          }
            try { 
                Sneaker.Id = maxId + 1;

                Sneaker.Title = TitleFiled.Text;
                Sneaker.ImagePath = imgPath + ImageFiled.Text + ".jpg";
                Sneaker.Company = CompanyFiled.Text;
                Sneaker.Price = decimal.Parse(PriceFiled.Text);
                Sneaker.Description = DescriptFiled.Text;
                Sneaker.Rating = (float)RateFiled.Value;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             MainWindow.Sneakers.AddItem(Sneaker);
            
            this.Close();
               


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
