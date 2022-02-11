using Lab3.Database;
using Lab3.Entity;
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
using Microsoft.Toolkit.Uwp.UI.Controls;
using System.Globalization;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Lab3.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListPersonalTransactionPage : Page
    {
        private DatabaseInitialize database = new DatabaseInitialize();
        private List<PersonalTransaction> listTransaction;
        private PersonalTransaction personal;
        private string checkedStartDate;
        private string checkedEndDate;

        public ListPersonalTransactionPage()
        {
            this.InitializeComponent();
            this.Loaded += ListPersonalTransactionPage_Loaded;
        }

        private void ListPersonalTransactionPage_Loaded(object sender, RoutedEventArgs e)
        {
            ListData.ItemsSource = database.ListData();
        }

        private void CreateTransactionButton(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.CreatePersonalTransactionPage));
        }

        private void ListDataGridTransaction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            personal = ListData.SelectedItem as PersonalTransaction;
            if (personal != null)
            {
                btnName.Text = personal.Name.ToString();
                btnDescription.Text = personal.Description.ToString();
                btnMoney.Text = Convert.ToDouble(personal.Money).ToString("#,###", cul.NumberFormat) + " đ";
                btnCreatedDate.Text = personal.CreatedDate.ToString("dd-MM-yyyy");
                btnCategory.Text = personal.Category.ToString();

            }
            else
            {
                btnName.Text = "Waiting";
                btnDescription.Text = "Waiting";
                btnMoney.Text = "Waiting";
                btnCreatedDate.Text = "Waiting";
                btnCategory.Text = "Waiting";
            }
        }

        private void FindByCategory(object sender, RoutedEventArgs e)
        {
            personal = null;
            btnName.Text = "Waiting";
            btnDescription.Text = "Waiting";
            btnMoney.Text = "Waiting";
            btnCreatedDate.Text = "Waiting";
            btnCategory.Text = "Waiting";
            List<PersonalTransaction> listTransactionByCategory = database.FindByCategory(Convert.ToInt32(searchCategory.Text));
            ListData.ItemsSource = listTransactionByCategory;
            btnTotalMoney.Text = database.totalMoney.ToString();
        }

        private void ResetList(object sender, RoutedEventArgs e)
        {
            btnName.Text = "Waiting";
            btnDescription.Text = "Waiting";
            btnMoney.Text = "Waiting";
            btnCreatedDate.Text = "Waiting";
            btnCategory.Text = "Waiting";
            ListData.ItemsSource = listTransaction;
            btnTotalMoney.Text = database.totalMoney.ToString();
        }

        private void FindByStartDateAndEndDate(object sender, RoutedEventArgs e)
        {

            personal = null;
            btnName.Text = "Waiting";
            btnDescription.Text = "Waiting";
            btnMoney.Text = "Waiting";
            btnCreatedDate.Text = "Waiting";
            btnCategory.Text = "Waiting";
            checkedStartDate = startDate.Date.ToString("yyyy-dd-MM");
            checkedEndDate = endDate.Date.ToString("yyyy-dd-MM");
            ListData.ItemsSource = database.FindByStartDateAndEndDate(checkedStartDate, checkedEndDate);
            btnTotalMoney.Text = database.totalMoney.ToString();
        }
    }
}
