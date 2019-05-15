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
                    productsBox.Items.Add(p.Title);
                }
                dishesList.ItemsSource = db.Dishes.ToList();
            }
        }

        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            dishesList.ItemsSource = null;
            dishesList.Items.Clear();
            using (DishContext db = new DishContext())
            {
                var dishes = db.Dishes.ToList();
                foreach(Dish d in dishes)
                {
                    string sKey = d.Products;
                    if (sKey.IndexOf(productsBox.SelectedValue.ToString()) >= 0 && d.Category == categoryBox.SelectedItem.ToString())
                    {
                        dishesList.Items.Add(d);
                    }
                }
            }
        }
    }
}
