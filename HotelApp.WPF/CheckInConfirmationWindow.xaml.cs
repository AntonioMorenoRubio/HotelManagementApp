using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using HotelLibrary.Interfaces;
using HotelLibrary.Models;

namespace HotelApp.WPF
{
    /// <summary>
    /// Lógica de interacción para CheckInConfirmationWindow.xaml
    /// </summary>
    public partial class CheckInConfirmationWindow : Window
    {
        private readonly IDatabaseData db;
        public ReservationFullModel? Reservation { get; set; } = null;


        public CheckInConfirmationWindow(IDatabaseData db)
        {
            this.db = db;
            InitializeComponent();
        }

        public void PopulateData(ReservationFullModel reservation)
        {
            Reservation = reservation;
            FirstNameLabel.Content = reservation.FirstName;
            LastNameLabel.Content = reservation.LastName;
            RoomTypeLabel.Content = reservation.Title;
            RoomNumberLabel.Content = reservation.RoomNumber;
            StartDateLabel.Content = reservation.StartDate.ToShortDateString();
            EndDateLabel.Content = reservation.EndDate.ToShortDateString();
            PriceLabel.Content = string.Format("{0:C}", reservation.TotalCost);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            db.CheckInClientInReservation(Reservation.Id);
            Close();
        }
    }
}
