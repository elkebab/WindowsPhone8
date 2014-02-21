using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Phone.Maps.Controls;
using System.Device.Location;
using Windows.Devices.Geolocation;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Oving5
{
    public partial class MainPage : PhoneApplicationPage
    {
        private Accelerometer acc; 
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            if (!Accelerometer.IsSupported)
            {
                statusTextBlock.Text = "Enheten støtter ikke akselerometer!";
                startButton.IsEnabled = false;
                stopButton.IsEnabled = false;
            }
            visMinPos(); 
        }

        private async void visMinPos()
        {
            // Hent min posisjon 
            Geolocator myGeolocator = new Geolocator();
            Geoposition myGeoposition = await myGeolocator.GetGeopositionAsync();
            Geocoordinate myGeocoordinate = myGeoposition.Coordinate;
            GeoCoordinate myGeoCoordinate = Koordinat.Konverter(myGeocoordinate);

            // Sentrer kartet til min posisjon 
            this.kart.Center = myGeoCoordinate;
            this.kart.ZoomLevel = 13;

            // Lag en liten sirkel 
            Ellipse myCircle = new Ellipse();
            myCircle.Fill = new SolidColorBrush(Colors.Red);
            myCircle.Height = 15;
            myCircle.Width = 15;
            myCircle.Opacity = 50;

            // Legg sirkelen i et eget lag 
            MapOverlay myLocationOverlay = new MapOverlay();
            myLocationOverlay.Content = myCircle;
            myLocationOverlay.PositionOrigin = new System.Windows.Point(0.5, 0.5);
            myLocationOverlay.GeoCoordinate = myGeoCoordinate;

            // Vis laget sammen med kartet 
            MapLayer myLocationLayer = new MapLayer();
            myLocationLayer.Add(myLocationOverlay);
            kart.Layers.Add(myLocationLayer); 
        } 

       
        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            if (acc == null) 
            { 
                acc = new Accelerometer(); 
                acc.TimeBetweenUpdates = TimeSpan.FromMilliseconds(20); 
                acc.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(accelerometer_CurrentValueChanged); 
            }

            try
            {
                statusTextBlock.Text = "Starter akselerometer.";
                acc.Start();
            }
            catch (InvalidOperationException ex)
            {
                statusTextBlock.Text = "Klarte ikke starte akselerometer.";
            }
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            if (acc != null)
            {
                acc.Stop();
                statusTextBlock.Text = "Akselerometeret har stanset.";
            } 
        }

        void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            Dispatcher.BeginInvoke(() => UpdateUI(e.SensorReading));
        }

        private void UpdateUI(AccelerometerReading accelerometerReading)
        {
            Vector3 acceleration = accelerometerReading.Acceleration;

            double tot = Math.Sqrt(acceleration.X * acceleration.X + acceleration.Y * acceleration.Y + acceleration.Z * acceleration.Z);
            
            statusfelt2.Text = "Kombinert G-kraft: " + tot.ToString("0.00");

            double grense = 2;
            if (tot > grense)
            {
                statusTextBlock.Text = "Skyhøy G! Over 2G!";
                System.Threading.Thread.Sleep(333); //legger inn sleep for testformål
            }
            else if (acceleration.Z < -0.97)
            {
                statusTextBlock.Text = "Skjerm er vendt opp.";
            }
            else
            {
                statusTextBlock.Text = "Mottar data.";
            }

            // Vis de numeriske verdiene. 
            xTextBlock.Text = "X: " + acceleration.X.ToString("0.00");
            yTextBlock.Text = "Y: " + acceleration.Y.ToString("0.00");
            zTextBlock.Text = "Z: " + acceleration.Z.ToString("0.00");

            // Vis verdiene grafisk. 
            xLine.X2 = xLine.X1 + acceleration.X * 200;
            yLine.Y2 = yLine.Y1 - acceleration.Y * 200;
            zLine.X2 = zLine.X1 - acceleration.Z * 100;
            zLine.Y2 = zLine.Y1 + acceleration.Z * 100;
        }
    }
}