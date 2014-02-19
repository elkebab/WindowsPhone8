using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Oving3.Resources;

namespace Oving3
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void KalkulerMinutter()
        {
            int startTime1 = int.Parse(startTime.Text);
            int startMin1 = int.Parse(startMin.Text);

            int sluttTime1 = int.Parse(sluttTime.Text);
            int sluttMin1 = int.Parse(sluttMin.Text);

            int startMins = startTime1 * 60 + startMin1;
            int sluttMins = sluttTime1 * 60 + sluttMin1;

            int resultat;

            //tester om nytt døgn
            if (startMins < sluttMins)
            {
                resultat = sluttMins - startMins;
            }
            else
            {
                resultat = sluttMins + (24 * 60 - startMins); 
            }
            resultatTxt.Text = "Resultat: " + resultat.ToString() + " min";
        }

        private void regnut_Click(object sender, RoutedEventArgs e)
        {
            KalkulerMinutter();
        }

        private void nullstill_Click(object sender, RoutedEventArgs e)
        {
            startTime.Text = "00";
            startMin.Text = "00";
            sluttTime.Text = "00";
            sluttMin.Text = "00";
        }

        private void startTime_GotFocus(object sender, RoutedEventArgs e)
        {
            startTime.Text = "";
        }

        private void startMin_GotFocus(object sender, RoutedEventArgs e)
        {
            startMin.Text = "";
        }

        private void sluttTime_GotFocus(object sender, RoutedEventArgs e)
        {
            sluttTime.Text = "";
        }

        private void sluttMin_GotFocus(object sender, RoutedEventArgs e)
        {
            sluttMin.Text = "";
        }
        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}