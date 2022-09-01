using Microsoft.UI.Xaml;
using System;

namespace AnalogClockUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        double hours, mins, secs;

        //To reach the value 1 in the clock, seconds pointer would need 5 secs.
        //So, for each second the value of the secs pointer should be incremented by 1/5 (0.2)
        const double secsIncrement = 0.2;

        //To reach the value 1 in the clock, mins pointer would need 5 mins which is 5*60=300 sec.
        //So, for each second the value of the mins pointer should be incremented by 1/300 (0.0033)
        const double minsIncrement = 0.0033;

        //To reach the value 1 in the clock, the hours pointer would need 60 mins, ie, 60*60=3600 sec.
        //So, for each second the value of the hours pointer should be incremented by 1/3600 (0.000278)
        const double hoursIncrement = 0.000278;

        DispatcherTimer timer;
        public MainWindow()
        {
            this.InitializeComponent();

            double currentSec = DateTime.Now.Second;
            double currentMin = DateTime.Now.Minute;
            double currentHour = DateTime.Now.Hour;

            //The current seconds value must be divided by 5 and set to the value of the pointer.
            //For example, if the current seconds value is 30, the secs pointer should point to 6.
            secs = currentSec / 5;

            //The current min value must be divided by 5 and set to the value of the pointer.
            //For example, if the current mins is 30, the min pointer should point to 6.
            mins = currentMin / 5 + currentSec * minsIncrement;

            //Converting 24 hours format into 12 hours format
            if (currentHour >= 12)
                currentHour -= 12;

            //Hour value can be directly assigned to the variable, along with converted mins and secs values to hour. 
            hours = currentHour + currentMin / 60 + ((currentSec / 60) / 60);

            timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            secs += secsIncrement;
            this.secsPointer.Value = secs;

            if(this.secsPointer.Value>=12)
            {
                this.secsPointer.EnableAnimation = false;
                this.secs = secsIncrement;
                this.secsPointer.Value = secs;
                this.secsPointer.EnableAnimation = true;
            }

            mins += minsIncrement;
            this.minsPointer.Value = mins;

            if (this.minsPointer.Value >= 12)
            {
                this.mins = minsIncrement;
                this.minsPointer.Value = mins;
            }

            hours += hoursIncrement;
            this.hoursPointer.Value = hours;

            if (this.hoursPointer.Value >= 12)
            {
                this.hours = hoursIncrement;
                this.hoursPointer.Value = hours;
            }
        }
    }
}
