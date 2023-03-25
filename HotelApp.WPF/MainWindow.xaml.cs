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
using HotelManagementLibrary.Data;
using HotelManagementLibrary.Databases;
using HotelManagementLibrary.Interfaces;
using HotelManagementLibrary.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IDatabaseData db;
        List<ReservationFullModel> reservations = new List<ReservationFullModel>();

        public MainWindow(IDatabaseData db)
        {
            this.db = db;
            InitializeComponent();
            reservations = db.GetTodayBookings();
            ReservationList.ItemsSource = reservations;
        }

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationList.SelectedItem != null)
            {
                var checkInForm = App.serviceProvider.GetService<CheckInConfirmationWindow>();
                var model = (ReservationFullModel) ReservationList.SelectedItem;

                
                checkInForm.Show();
            }
        }

        private void LastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            reservations.Clear();
            if (LastNameTextBox.Text.Length > 0 )
                reservations = db.GetTodayBookingsByLastName(LastNameTextBox.Text);
            else
                reservations = db.GetTodayBookings();
            ReservationList.ItemsSource = reservations;
        }
    }
}
