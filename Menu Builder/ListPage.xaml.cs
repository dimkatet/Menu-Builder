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
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using Windows.Storage.Pickers;
using static System.Diagnostics.Process;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Menu_Builder
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ListPage : Page
    {
        public ListPage()
        {
            this.InitializeComponent();
        }

        public void ListPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (DishContext db = new DishContext())
            {
                foreach(Product p in db.Products)
                {
                    if(p.Amount > 0)
                        productsBox.Items.Add(p.Title);
                }
                dishesList.ItemsSource = db.Dishes.ToList();
            }
        }

        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            dishesList.ItemsSource = null;
            dishesList.Items.Clear();
            string sCategory = null;
            switch(categoryBox.SelectedIndex)
            {
                case 0:
                    sCategory = "Первое блюдо";
                    break;
                case 1:
                    sCategory = "Второе блюдо";
                    break;
                case 2:
                    sCategory = "Закуска";
                    break;
                case 3:
                    sCategory = "Десерт";
                    break;
            }
            using (DishContext db = new DishContext())
            {
                var dishes = db.Dishes.ToList();
                foreach(Dish d in dishes)
                {
                    string sKey = d.Products;
                    if (sKey.IndexOf(productsBox.SelectedValue.ToString()) >= 0 && d.Category == sCategory)
                    {
                        dishesList.Items.Add(d);
                    }
                }
            }
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile file = await storageFolder.CreateFileAsync("Products.txt", Windows.Storage.CreationCollisionOption.OpenIfExists);
            var list = dishesList.Items.ToList();
            await Windows.Storage.FileIO.WriteTextAsync(file, "");
            foreach (Dish d in list)
            {
                await Windows.Storage.FileIO.AppendTextAsync(file, d.Title + "    " + d.Category + "    " + d.Products + "\n");
            }
            /*FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.ComputerFolder;
            System.Diagnostics.Process.Start(storageFolder.Path); */
        }
    }
}
