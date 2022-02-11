using Lab3.Database;
using Lab3.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Lab3.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreatePersonalTransactionPage : Page
    {
        public CreatePersonalTransactionPage()
        {
            this.InitializeComponent();
        }

        private async void Create_Personal_Transaction(object sender, RoutedEventArgs e)
        {
            var personalTransaction = new PersonalTransaction()
            {
                Name = Name.Text,
                Description = Description.Text,
                Money = int.Parse(Money.Text),
                CreatedDate = DateTime.Parse(CreateDate.Text),
                Category = int.Parse(Category.Text)
            };

            var database = new DatabaseInitialize();
            var result = database.InsertData(personalTransaction);
            var dialog = new ContentDialog();
            if (result)
            {
                dialog.Title = "Success";
                dialog.Content = "Insert data success !!";
                dialog.PrimaryButtonText = "Close";
                await dialog.ShowAsync();
            } else
            {
                dialog.Title = "Failed";
                dialog.Content = "Insert data failed !!";
                dialog.PrimaryButtonText = "Close";
                await dialog.ShowAsync();
            }

        }

        private void Redirect_List(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.ListPersonalTransactionPage));
        }
    }
}
