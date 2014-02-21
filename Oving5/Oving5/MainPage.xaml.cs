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
            statusTextBlock.Text = "Mottar data.";

            Vector3 acceleration = accelerometerReading.Acceleration;

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

        private void PanoramaItem_LostFocus(object sender, RoutedEventArgs e)
        {       
            if (acc != null)
            {
                acc.Stop();
                statusTextBlock.Text = "Akselerometeret har stanset.";
            }        
        } 
    }
}