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
    

    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();
            Windows.UI.ViewManagement.ApplicationView appView = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView();
            // минимальный размер 300х250
            appView.SetPreferredMinSize(new Size(300, 250));
        }

        private void SearchPage_Loaded(object sender, RoutedEventArgs e)
        {
            using (DishContext db = new DishContext())
            {
                dishesList.ItemsSource = db.Dishes.ToList();
            }
        }

        private void productsSearch()
        {
            
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SearchPanel_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && searchPanel.Text != null)
                using (DishContext db = new DishContext())
                {
                    dishesList.ItemsSource = null;
                    dishesList.Items.Clear();
                    var dishes = db.Dishes.ToList();
                    Dish dish = new Dish();
                    foreach (Dish d in dishes)
                    {
                        string sKey = null;
                        switch(searchKey.SelectedIndex)
                        {
                            case 0:
                                sKey = d.Title;
                                break;
                            case 1:
                                sKey = d.Category;
                                break;
                            case 2:
                                sKey = d.Products;
                                break;
                        }
                        if(sKey != null)
                            if (sKey.IndexOf(searchPanel.Text) >= 0)
                            {
                                dishesList.Items.Add(d);
                            }
                    }
                }
        }
    }
}
