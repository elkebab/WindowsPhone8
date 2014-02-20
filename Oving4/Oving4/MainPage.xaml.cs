using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Oving4.Resources;

namespace Oving4
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
        
        int klikk = 0;
        int antFeil = 0;

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            klikk++;
            teller.Text = klikk.ToString("N0");   
        }


        private void reset_Click(object sender, RoutedEventArgs e)
        {
            klikk = 0;
            teller.Text = klikk.ToString("N0");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            teller.Text = klikk.ToString("N0");
        }

        private void figur_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            double x = e.CumulativeManipulation.Translation.X;
            double y = e.CumulativeManipulation.Translation.Y;
            if (-142 < x && x < 142 && -176 < y && y < 176)
            {
                beveger.X = x;
                beveger.Y = y;
            }
            else
            {
                beveger.X = 0;
                beveger.Y = 0;
                MessageBox.Show("Ikke flytt figuren utenfor det grå feltet!\nDette var din feil nr: " + (++antFeil));
            }
        }
    }
}