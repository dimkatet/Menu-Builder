using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Menu_Builder
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Stack<string>[] dbProducts;
        public MainPage()
        {
            this.InitializeComponent();
            /*using (DishContext db = new DishContext())
            {
                var size = db.Dishes.ToList().Count;
                string str = null;
                dbProducts = new Stack<string>[size];
                var d = db.Dishes.ToArray();
                for(int i = 0; i < size; i++)
                {
                    str = null;
                    for(int j = 0; j < d[i].Products.Length; j++)
                    {
                        if (d[i].Products.ToString()[j] == ',')
                        {
                            if (dbProducts[i] == null)
                                dbProducts[i] = new Stack<string>();
                            dbProducts[i].Push(str);
                            str = null;
                            continue;
                        }
                        str += d[i].Products.ToString()[j];
                    }
                }
                Write();
            } */
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "searchDish")
                {
                    NavView.SelectedItem = item;
                    NavView_Navigate(item as NavigationViewItem);
                    break;
                }
            }
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
            NavView_Navigate(item as NavigationViewItem);
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            //var dialog = new MessageDialog("AAAA");
            switch (item.Tag)
            {
                case "searchDish":
                    ContentFrame.Navigate(typeof(SearchPage));
                    break;

                case "menuBuilding":
                    ContentFrame.Navigate(typeof(BuildingPage));
                    break;

                case "dishesList":
                    ContentFrame.Navigate(typeof(ListPage));
                    break;
            }
        }

        private async void Write()
        {
            string text = null;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile helloFile = await localFolder.CreateFileAsync("hello.txt", CreationCollisionOption.ReplaceExisting);
            foreach(Stack<string> st in dbProducts)
            {
                text = null;
                int size = st.Count;
                var arr = st.ToArray();
                for(int i = 0; i < size; i++)
                {
                    text += arr[i];
                    text += " ";
                }
                await FileIO.WriteTextAsync(helloFile, text);
            }
        }
    }
}