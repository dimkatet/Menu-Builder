using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Menu_Builder
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class BuildingPage : Page
    {
        enum CATEGORIES
        {
            FIRST_DISH,
            SECOND_DISH,
            SNACK,
            DESSERT
        }

        CATEGORIES categories = CATEGORIES.FIRST_DISH;

        public BuildingPage()
        {
            this.InitializeComponent();
            /*if (firstDish != null)
                tBlock0.Text = firstDish;
            if (secondDish != null)
                tBlock1.Text = secondDish;
            if (snack != null)
                tBlock2.Text = snack;
            if (desert != null)
                tBlock3.Text = desert; */
        }

        public void BuildingPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (DishContext db = new DishContext())
            {
                var dishes = db.Dishes.ToList();
                foreach (Dish d in dishes)
                {
                   if (d.Category == "Первое блюдо")
                   {
                      dishesList.Items.Add(d);
                   }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dishesList.Items.Clear();
            categories = CATEGORIES.FIRST_DISH;
            button0.IsChecked = true;
            button1.IsChecked = false;
            button2.IsChecked = false;
            button3.IsChecked = false;
            using (DishContext db = new DishContext())
            {
                var dishes = db.Dishes.ToList();
                foreach (Dish d in dishes)
                {
                   if (d.Category == "Первое блюдо")
                   {
                      dishesList.Items.Add(d);
                   }
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dishesList.Items.Clear();
            categories = CATEGORIES.SECOND_DISH;
            button0.IsChecked = false;
            button1.IsChecked = true;
            button2.IsChecked = false;
            button3.IsChecked = false;
            using (DishContext db = new DishContext())
            {
                var dishes = db.Dishes.ToList();
                foreach (Dish d in dishes)
                {
                    if (d.Category == "Второе блюдо")
                    {
                        dishesList.Items.Add(d);
                    }
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dishesList.Items.Clear();
            categories = CATEGORIES.SNACK;
            button0.IsChecked = false;
            button1.IsChecked = false;
            button2.IsChecked = true;
            button3.IsChecked = false;
            using (DishContext db = new DishContext())
            {
                var dishes = db.Dishes.ToList();
                foreach (Dish d in dishes)
                {
                    if (d.Category == "Закуска")
                    {
                        dishesList.Items.Add(d);
                    }
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            dishesList.Items.Clear();
            categories = CATEGORIES.DESSERT;
            button0.IsChecked = false;
            button1.IsChecked = false;
            button2.IsChecked = false;
            button3.IsChecked = true;
            using (DishContext db = new DishContext())
            {
                var dishes = db.Dishes.ToList();
                foreach (Dish d in dishes)
                {
                    if (d.Category == "Десерт")
                    {
                        dishesList.Items.Add(d);
                    }
                }
            }
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (dishesList.SelectedItem == null)
                return;
            Dish dish = dishesList.SelectedItem as Dish;
            switch (categories)
            {
                case CATEGORIES.FIRST_DISH:
                    tBlock0.Text = dish.Title;
                    //firstDish = tBlock0.Text;
                    break;
                case CATEGORIES.SECOND_DISH:
                    tBlock1.Text = dish.Title;
                    //secondDish = tBlock1.Text;
                    break;
                case CATEGORIES.SNACK:
                    tBlock2.Text = dish.Title;
                    //snack = tBlock2.Text;
                    break;
                case CATEGORIES.DESSERT:
                    tBlock3.Text = dish.Title;
                    //desert = tBlock3.Text;
                    break;
            }
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            switch (categories)
            {
                case CATEGORIES.FIRST_DISH:
                    tBlock0.Text = "";
                    break;
                case CATEGORIES.SECOND_DISH:
                    tBlock1.Text = "";
                    break;
                case CATEGORIES.SNACK:
                    tBlock2.Text = "";
                    break;
                case CATEGORIES.DESSERT:
                    tBlock3.Text = "";
                    break;
            }
        }

        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile file = await storageFolder.CreateFileAsync("menu.txt", Windows.Storage.CreationCollisionOption.OpenIfExists);
            await Windows.Storage.FileIO.WriteTextAsync(file, "Первое блюдо:\n");
            await Windows.Storage.FileIO.AppendTextAsync(file, tBlock0.Text + "\n\n");
            await Windows.Storage.FileIO.AppendTextAsync(file, "Второе блюдо:\n");
            await Windows.Storage.FileIO.AppendTextAsync(file, tBlock1.Text + "\n\n");
            await Windows.Storage.FileIO.AppendTextAsync(file, "Закуска:\n");
            await Windows.Storage.FileIO.AppendTextAsync(file, tBlock2.Text + "\n\n");
            await Windows.Storage.FileIO.AppendTextAsync(file, "Десерт:\n");
            await Windows.Storage.FileIO.AppendTextAsync(file, tBlock3.Text + "\n\n");
            await Windows.Storage.FileIO.AppendTextAsync(file, "Количество человек:\n");
            await Windows.Storage.FileIO.AppendTextAsync(file, slider.Value.ToString());
        }
    }
}
