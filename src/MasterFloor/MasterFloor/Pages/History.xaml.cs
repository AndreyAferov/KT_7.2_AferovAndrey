using MasterFloor.Classes;
using MasterFloor.Data;
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

namespace MasterFloor.Pages
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Page
    {
        private readonly Data.Partner _selectedPartner;
        private Partner _partner;
        public History(Partner partner)
        {
            _partner = partner;
            InitializeComponent();
            LoadHistoryData();
        }

        private void LoadHistoryData()
        {
            var historyData = from product in Data.MasterFloorsEntities.GetContext().PartnerProducts
                              join type in Data.MasterFloorsEntities.GetContext().TypeProduct
                              on product.Production equals type.Id
                              where product.PartnerName == _partner.Id
                              select new
                              {
                                  Productionn = type.Name,
                                  CountOfProduction = product.CountProduct,
                                  DateOfSale = product.DateSell
                              };

            HistoryDataGrid.ItemsSource = historyData.ToList();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ListPage());
        }
    }
}
