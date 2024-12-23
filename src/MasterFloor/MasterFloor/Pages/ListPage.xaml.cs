﻿using MasterFloor.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterFloor.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public ListPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var partners = Data.MasterFloorsEntities.GetContext().Partner.ToList();
            var partnerDiscounts = (from partner in partners
                                    join product in Data.MasterFloorsEntities.GetContext().PartnerProducts
                                    on partner.Id equals product.PartnerName into productGroup
                                    from product in productGroup.DefaultIfEmpty()
                                    group product by partner into g
                                    select new
                                    {
                                        Partner = g.Key,
                                        Discount = CalculateDiscount(g.Sum(p => p?.CountProduct ?? 0))
                                    }).ToList();
            Listing.ItemsSource = partnerDiscounts;
        }

        private int CalculateDiscount(int totalCount)
        {
            if (String.IsNullOrEmpty(totalCount.ToString())) return 0;
            if (totalCount < 10000) return 0;
            if (totalCount >= 10000 && totalCount < 50000) return 5;
            if (totalCount >= 50000 && totalCount < 300000) return 10;
            return 15;
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdit(null));
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            Button historyButton = sender as Button;
            var partnerData = historyButton.DataContext;

            if (partnerData != null)
            {
                var partner = (partnerData as dynamic).Partner;
                Classes.Manager.MainFrame.Navigate(new History(partner));
            }
            else
            {
                MessageBox.Show("Ошибка!","Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button editButton = sender as Button;
            var partnerData = editButton.DataContext;
            if (partnerData != null)
            {
                var partner = (partnerData as dynamic).Partner;
                Classes.Manager.MainFrame.Navigate(new Pages.AddEdit(partner));
            }
        }
    }
}
