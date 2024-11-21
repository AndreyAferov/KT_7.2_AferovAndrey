using MasterFloor.Classes;
using MasterFloor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Page
    {
        public string FlagAddorEdit = "default";
        public Data.Partner _currentpartner = new Data.Partner();
        public AddEdit(Data.Partner partner)
        {
            InitializeComponent();
            if (partner != null)
            {
                _currentpartner = partner;
                FlagAddorEdit = "edit";
            }
            else
            {
                FlagAddorEdit = "add";
            }
            DataContext = _currentpartner;

            Init();
        }
        public void Init()
        {
            try
            {
                IdTextBox.Visibility = Visibility.Hidden;
                var list = Data.MasterFloorsEntities.GetContext().PartnetName.ToList();
                ComboPar.ItemsSource = list;


                if (FlagAddorEdit == "add")
                {
                    ComboPar.SelectedItem = null;
                    NameTextBox.Text = string.Empty;
                    RatingTextBox.Text = string.Empty;
                    FIOTextBox.Text = string.Empty;
                    PhoneTextBox.Text = string.Empty;
                    EmailTextBox.Text = string.Empty;
                }
                else if (FlagAddorEdit == "edit")
                {
                    NameTextBox.Text = _currentpartner.OrganizeName.NameOrg.ToString();
                    RatingTextBox.Text = _currentpartner.Rate.ToString();
                    RegionTextBox.Text = _currentpartner.Adress1.Regions1.NameRegion;
                    CityTextBox.Text = _currentpartner.Adress1.Countrys.NameCountry.ToString();
                    StreetTextBox.Text = _currentpartner.Adress1.Streets.NameStreet.ToString();
                    HouseNumTextBox.Text = _currentpartner.Adress1.HouseNum.ToString();
                    IndexTextBox.Text = _currentpartner.Adress1.CountryIndex.NameCountry.ToString();
                    FIOTextBox.Text = _currentpartner.DirectorName.FIO;
                    PhoneTextBox.Text = _currentpartner.PhoneNumber;
                    EmailTextBox.Text = _currentpartner.Email;
                    IdTextBox.Text = _currentpartner.Id.ToString();
                    ComboPar.SelectedItem = Data.MasterFloorsEntities.GetContext().PartnetName
                        .FirstOrDefault(d => d.Id == _currentpartner.Partner1);
                }
            }
            catch 
            {
            MessageBox.Show("Ошибка","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StringBuilder errors = new StringBuilder();
                if (string.IsNullOrEmpty(NameTextBox.Text))
                {
                    errors.AppendLine("Введите наименование");
                }
                if (ComboPar.SelectedItem == null)
                {
                    errors.AppendLine("Выберите партнера");
                }
                if (string.IsNullOrEmpty(RatingTextBox.Text))
                {
                    errors.AppendLine("Введите рейтинг");
                }
                else if (!int.TryParse(RatingTextBox.Text, out int rating) || rating < 0)
                {
                    errors.AppendLine("Рейтинг должен быть целым неотрицательным числом.");
                }
                if (string.IsNullOrEmpty(RegionTextBox.Text))
                {
                    errors.AppendLine("Введите Регион");
                }
                if (string.IsNullOrEmpty(CityTextBox.Text))
                {
                    errors.AppendLine("Введите Город");
                }
                if (string.IsNullOrEmpty(StreetTextBox.Text))
                {
                    errors.AppendLine("Введите Улицу");
                }
                if (string.IsNullOrEmpty(HouseNumTextBox.Text))
                {
                    errors.AppendLine("Введите номер дома");
                }
                else if (!int.TryParse(HouseNumTextBox.Text, out int rating) || rating < 0)
                {
                    errors.AppendLine("Дом должен быть целым неотрицательным числом.");
                }
                if (string.IsNullOrEmpty(IndexTextBox.Text))
                {
                    errors.AppendLine("Введите индекс");
                }
                if (string.IsNullOrEmpty(FIOTextBox.Text))
                {
                    errors.AppendLine("Введите ФИО");
                }
                if (string.IsNullOrEmpty(PhoneTextBox.Text))
                {
                    errors.AppendLine("Введите номер телефона");
                }
                if (string.IsNullOrEmpty(EmailTextBox.Text))
                {
                    errors.AppendLine("Введите Email");
                }

                if (errors.Length > 0)
                {
                    MessageBox.Show(errors.ToString(), "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var selectedCategory = ComboPar.SelectedItem as Data.PartnetName;
                if (selectedCategory == null)
                {
                    MessageBox.Show("Выберите партнера", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                _currentpartner.Partner1 = selectedCategory.Id;
                _currentpartner.Rate = RatingTextBox.Text;
                _currentpartner.PhoneNumber = PhoneTextBox.Text;
                _currentpartner.Email = EmailTextBox.Text;

                var searchDirector = (from item in Data.MasterFloorsEntities.GetContext().DirectorName
                                      where item.FIO == FIOTextBox.Text
                                      select item).FirstOrDefault();
                if (searchDirector != null)
                {
                    _currentpartner.Director = searchDirector.id;
                }
                else
                {
                    Data.DirectorName directors = new Data.DirectorName()
                    {
                        FIO = FIOTextBox.Text
                    };
                    Data.MasterFloorsEntities.GetContext().DirectorName.Add(directors);
                    Data.MasterFloorsEntities.GetContext().SaveChanges();
                    _currentpartner.Director = directors.id;
                }

                var searchPartnerName = Data.MasterFloorsEntities.GetContext().OrganizeName
                                        .FirstOrDefault(item => item.NameOrg == NameTextBox.Text);

                if (searchPartnerName != null)
                {
                    _currentpartner.Name = searchPartnerName.Id;
                }
                else
                {
                    var partnerName = new Data.OrganizeName
                    {
                        NameOrg = NameTextBox.Text
                    };

                    Data.MasterFloorsEntities.GetContext().OrganizeName.Add(partnerName);
                    Data.MasterFloorsEntities.GetContext().SaveChanges();

                    _currentpartner.Name = partnerName.Id;
                }


                int houseNum = int.Parse(HouseNumTextBox.Text);

                var address = MasterFloorsEntities.GetContext().Adress
                    .FirstOrDefault(a => a.Regions.NameRegion == RegionTextBox.Text &&
                                         a.Countrys.NameCountry == CityTextBox.Text &&
                                         a.Streets.NameStreet == StreetTextBox.Text &&
                                         a.HouseNum == houseNum &&
                                         a.CountryIndex.NameCountry == IndexTextBox.Text);

                if (address == null)
                {
                    address = new Adress
                    {
                        Regions = new Regions { NameRegion = RegionTextBox.Text },
                        Countrys = new Countrys { NameCountry = CityTextBox.Text },
                        Streets = new Streets { NameStreet = StreetTextBox.Text },
                        HouseNum = houseNum,
                        CountryIndex = new CountryIndex { NameCountry = IndexTextBox.Text }
                    };

                    MasterFloorsEntities.GetContext().Adress.Add(address);
                    MasterFloorsEntities.GetContext().SaveChanges();
                }

                _currentpartner.Adress1 = address;


                if (address.Id != 0)
                {
                    _currentpartner.Adress1 = address;
                }
                else
                {
                    MessageBox.Show("Ошибка сохранения адреса.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (FlagAddorEdit == "add")
                {
                    Data.MasterFloorsEntities.GetContext().Partner.Add(_currentpartner);
                    MessageBox.Show("Успешно добавлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Data.MasterFloorsEntities.GetContext().SaveChanges();
                    Manager.MainFrame.Navigate(new ListPage());
                }
                else if (FlagAddorEdit == "edit")
                {
                    MessageBox.Show("Успешно сохранено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    Data.MasterFloorsEntities.GetContext().SaveChanges();
                    Manager.MainFrame.Navigate(new ListPage());
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ListPage());
        }
    }
}
